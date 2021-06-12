using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Threading;

public partial class CloseTransferOut : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("WareHouse");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
       if (!IsPostBack)
        {
            fillgrid();
            tbltransferout.Visible = false;
            tbllostanddamages.Visible = false;
            tbldailyissue.Visible = false;
            hfrole.Value = Session["roles"].ToString();
        }

    }
    public void fillgrid()
    {

        if (Session["roles"].ToString() == "StoreKeeper")
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Sitestoretransfer_sp";
            cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["CC_CODE"].ToString();
            cmd.Connection = con;
            try
            {
                con.Open();
                Gvdetails.EmptyDataText = "No Records Found";
                Gvdetails.DataSource = cmd.ExecuteReader();
                Gvdetails.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                //con.Dispose();
            }
        }
    }
    protected void Gvdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[1].Text == "1") && (e.Row.Cells[3].Text == "TransferOut"))
            {
                e.Row.Cells[4].Text = e.Row.Cells[2].Text + "     Assets items quantity needs to be transfer for store close.";
            }
            if ((e.Row.Cells[1].Text == "2") && (e.Row.Cells[3].Text == "TransferOut"))
            {
                e.Row.Cells[4].Text = e.Row.Cells[2].Text + "     Semi Assests and Semiconsumable items quantity needs to be transfer for store close.";
            }
            if ((e.Row.Cells[1].Text == "3") && (e.Row.Cells[3].Text == "TransferOut"))
            {
                e.Row.Cells[4].Text = e.Row.Cells[2].Text + "     Consumable items quantity needs to be transfer for store close.";
            }
            if ((e.Row.Cells[1].Text == "1") && (e.Row.Cells[3].Text == "Lost/Damaged"))
            {
                e.Row.Cells[4].Text = e.Row.Cells[2].Text + "     Assets items quantity needs to be transfer for store close.";
            }
            if ((e.Row.Cells[1].Text == "2and3") && (e.Row.Cells[3].Text == "Lost/Damaged"))
            {
                e.Row.Cells[4].Text = e.Row.Cells[2].Text + "     Semi Assests,Semiconsumable and Consumable items quantity needs to be transfer for store close.";
            }
            if ((e.Row.Cells[1].Text == "3") && (e.Row.Cells[3].Text == "DailyIssue"))
            {
                e.Row.Cells[4].Text = e.Row.Cells[2].Text + "     Daily Issue items quantity needs to be transfer for store close.";
            }
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }   
    protected void Gvdetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "StoreKeeper")
        {
            //string id = Gvdetails.SelectedValue.ToString();
            string id = Gvdetails.DataKeys[Gvdetails.SelectedIndex].Values["code"].ToString();
            string type = Gvdetails.DataKeys[Gvdetails.SelectedIndex].Values["Type"].ToString();
            if (type.ToString() == "TransferOut")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,[Issued Qty],isnull([Available qty],0) [Available Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,quantity as [Issued Qty],(Select isnull(Sum(quantity),0) from master_data where cc_code='" + Session["cc_code"].ToString() + "' and item_code=it.item_code and (Status='Active' or Status is null))as [Available Qty] from [Items Transfer]it where Ref_no is null AND it.cc_code='" + Session["cc_code"].ToString() + "' and type='2' and U_Id is not null and SUBSTRING(item_code,1,1)='" + id + "')il on i.item_code=substring(il.item_code,1,8) order by il.id Asc", con);
                da.Fill(ds, "fills");
                if (ds.Tables["fills"].Rows.Count > 0)
                {
                    tbltransferout.Visible = true;
                    tbllostanddamages.Visible = false;
                    tbldailyissue.Visible = false;
                    fillgrid();
                    grdtransferout.Visible = true;
                    grdtransferout.DataSource = ds.Tables["fills"];
                    grdtransferout.DataBind();

                }
            }
            else if (type.ToString() == "Lost/Damaged")
            {
                string types = "";
                if (id.ToString() == "2and3")
                {
                    ViewState["Assets"] = "0";
                    types = "('2','3')";
                }
                else
                {
                    ViewState["Assets"] = "1";
                    types = "('1')";
                }
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(il.quantity,'.00','')quantity,isnull([type],'Select')[type] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,[type] from [Lost/Damaged_items] where ref_no is null and U_Id is not null and SUBSTRING(item_code,1,1)in " + types.ToString() + " and cc_code='" + Session["cc_code"].ToString() + "' )il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillld");
                if (ds.Tables["fillld"].Rows.Count > 0)
                {
                    tbltransferout.Visible = false;
                    tbllostanddamages.Visible = true;
                    tbldailyissue.Visible = false;
                    fillgrid();
                    GridView1.Visible = true;
                    GridView1.DataSource = ds.Tables["fillld"];
                    GridView1.DataBind();

                }
            }
            else if (type.ToString() == "DailyIssue")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')basic_price,units,Replace(il.quantity,'.00','')quantity,isnull([Available Qty],0)[Available Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,(Select quantity from master_data where cc_code='" + Session["cc_code"].ToString() + "' and item_code=it.item_code)as [Available Qty] from [Daily Issued Items]it where transaction_id is null and cc_code='" + Session["cc_code"].ToString() + "' and U_Id is not null)il on i.item_code=il.item_code", con);
                da.Fill(ds, "fillissue");
                if (ds.Tables["fillissue"].Rows.Count > 0)
                {
                    tbltransferout.Visible = false;
                    tbllostanddamages.Visible = false;
                    tbldailyissue.Visible = true;
                    fillgrid();
                    GridView2.Visible = true;
                    GridView2.DataSource = ds.Tables["fillissue"];
                    GridView2.DataBind();

                }
            }
        }
    
    }
    protected void grdtransferout_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            da = new SqlDataAdapter("select isnull(sum(quantity),0) from master_data where cc_code='" + Session["cc_code"].ToString() + "' and item_code='" + e.Row.Cells[2].Text + "' union all SELECT isnull(sum(quantity),0) FrOM [items transfer] it JOIN [transfer info] ti ON it.ref_no=ti.ref_no where item_code='" + e.Row.Cells[2].Text + "' and STATUS='3' and it.cc_code='" + Session["cc_code"].ToString() + "'", con);
            da.Fill(ds, "Qty");
            decimal s1 = Convert.ToDecimal(ds.Tables["Qty"].Rows[0].ItemArray[0].ToString());
            decimal s2 = Convert.ToDecimal(ds.Tables["Qty"].Rows[1].ItemArray[0].ToString());
            decimal S = s1 - s2;
            e.Row.Cells[9].Text = Convert.ToString(S);
            ds.Tables["Qty"].Reset();
            TextBox quantity = (TextBox)e.Row.FindControl("txtqty");
            quantity.Enabled = false;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Qty = "";
            string status = "";           
            if (Session["roles"].ToString() == "StoreKeeper")
            {
                foreach (GridViewRow record in grdtransferout.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                    if ((record.FindControl("ChkSelect") as CheckBox).Checked)
                    {
                        ids = ids + grdtransferout.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        status = status + (record.FindControl("ddlStatus") as DropDownList).SelectedValue + ",";
                    }
                }
                if (ids != "")
                {
                    da = new SqlDataAdapter("transferout_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ids", SqlDbType.VarChar).Value = ids;
                    da.SelectCommand.Parameters.AddWithValue("@NewIds", SqlDbType.VarChar).Value = ids;
                    da.SelectCommand.Parameters.AddWithValue("@Qtys", SqlDbType.VarChar).Value = Qty;
                    da.SelectCommand.Parameters.AddWithValue("@Statuses", SqlDbType.VarChar).Value = status;
                    da.SelectCommand.Parameters.AddWithValue("@Date", SqlDbType.VarChar).Value = txtdate.Text;
                    da.SelectCommand.Parameters.AddWithValue("@Days", SqlDbType.Int).Value = ddlDays.SelectedValue;
                    da.SelectCommand.Parameters.AddWithValue("@cccode", SqlDbType.Int).Value = Session["cc_code"].ToString();
                    da.SelectCommand.Parameters.AddWithValue("@Recievedcc", SqlDbType.Int).Value = "CC-33";
                    da.SelectCommand.Parameters.AddWithValue("@Remarks", SqlDbType.Int).Value = txtremarks.Text;
                    da.SelectCommand.Parameters.AddWithValue("@User", SqlDbType.Int).Value = Session["user"].ToString();
                    da.Fill(ds, "raisetransfer");
                    if (ds.Tables["raisetransfer"].Rows[0].ItemArray[0].ToString() == "Transfer Raised Successfully")
                    {
                        JavaScript.UPAlertRedirect(Page, "Transfer Raised Successfully. The Transfer No is: " + ds.Tables["raisetransfer"].Rows[0].ItemArray[1].ToString(), "CloseTransferOut.aspx");
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, ds.Tables["raisetransfer"].Rows[0].ItemArray[0].ToString());
                    }
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox quantity = (TextBox)e.Row.FindControl("txtqty");
            quantity.Enabled = false;
            //if (ViewState["Assets"].ToString() == "1")
            //{
            //    e.Row.Cells[8].Text = "1";
            //    quantity.Enabled = false;
            //}
        }

    }
    protected void btnsubmitld_Click(object sender, EventArgs e)
    {
        string ids = "";
        string Qtys = "";
        string types = "";
        string reporttypes = "";
        string remarks = "";
        try
        {
            foreach (GridViewRow record in GridView1.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    string id = "";
                    ids = ids + GridView1.DataKeys[record.RowIndex]["id"].ToString() + ",";
                    Qtys = Qtys + (record.FindControl("txtqty") as TextBox).Text + ",";
                    reporttypes = reporttypes + (record.FindControl("ddlreporttype") as DropDownList).SelectedValue + ",";
                    types = types + (record.FindControl("ddltype") as DropDownList).SelectedValue + ",";
                    remarks = remarks + (record.FindControl("txtremarksld") as TextBox).Text + ",";
                }
            }
            cmd = new SqlCommand("Lost/Damaged Report_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ids", ids);
            cmd.Parameters.AddWithValue("@Qtys", Qtys);
            cmd.Parameters.AddWithValue("@ReportTypes", reporttypes);
            cmd.Parameters.AddWithValue("@Types", types);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@Date", txtdateld.Text);
            cmd.Parameters.AddWithValue("@CCCode", Session["cc_code"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Refno", "");
            cmd.Parameters.AddWithValue("@Reportstype", "1");
            cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            JavaScript.UPAlertRedirect(Page, msg, "CloseTransferOut.aspx");
            Thread.Sleep(2000);
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox quantity = (TextBox)e.Row.FindControl("txtqty");
            quantity.Enabled = false;
          
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Qty = "";
            string Date = txtdateissue.Text;
            foreach (GridViewRow record in GridView2.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if ((record.FindControl("ChkSelect") as CheckBox).Checked)
                {
                    ids = ids + GridView2.DataKeys[record.RowIndex]["id"].ToString() + ",";
                    Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                }
            }
            if (ids != "")
            {
                cmd = new SqlCommand("DailyIssue_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@Qtys", Qty);
                cmd.Parameters.AddWithValue("@Nids", ids);
                cmd.Parameters.AddWithValue("@NQtys", Qty);
                cmd.Parameters.AddWithValue("@CCCode", Session["cc_code"].ToString());
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@Remarks", txtdesc.Text);
            }
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfull")
                JavaScript.UPAlertRedirect(Page, msg, "CloseTransferOut.aspx");
            else
                JavaScript.UPAlert(Page, msg);
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
}