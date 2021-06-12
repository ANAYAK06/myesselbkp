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

public partial class Lost_or_Damaged_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("WareHouse");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 17);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            checkapprovals();
            Fillgrid();
        }
        try
        {
           
            da = new SqlDataAdapter("Select Reporttype from [lost/Damaged_items] where ref_no is null and cc_code='" + Session["cc_code"].ToString() + "'", con);
            da.Fill(ds, "reporttype");
            if (ds.Tables["reporttype"].Rows.Count > 0)
            {
                string s = ds.Tables["reporttype"].Rows[0].ItemArray[0].ToString();
                if (s == "2")
                {
                    trbtns.Visible = false;
                    ViewState["rtype"] = s;
                }
                else
                {
                    trbtns.Visible = true;
                    ViewState["rtype"] = s;
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }


    }
    private void Fillgrid()
    {
        try
        {
            da = new SqlDataAdapter("Select distinct il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(il.quantity,'.00','')quantity,isnull([type],'Select')[type],(select isnull(Quantity,0) from Master_data where CC_code='" + Session["cc_code"].ToString() + "' and Item_code=i.Item_code)as avlqty from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,[type] from [Lost/Damaged_items] where ref_no is null and U_Id is null and cc_code='" + Session["cc_code"].ToString() + "' )il on i.item_code=substring(il.item_code,1,8)", con);
            
            da.Fill(ds, "report");
            ViewState["rowcount"] = ds.Tables[0].Rows.Count;
            GridView1.DataSource = ds.Tables["report"];
            GridView1.DataBind();
            if (ds.Tables["report"].Rows.Count > 0)
                trbtn.Style.Add("visibility", "visible");
            else
                trbtn.Style.Add("visibility", "hidden");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
   



    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            cmd.Connection = con;
            con.Open();
            char c = txtSearch.Text[0];
            if (char.IsNumber(c))
            {

                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM master_data where item_code='" + txtSearch.Text + "' and CC_code='" + Session["cc_code"].ToString() + "'", con);
                    da.Fill(ds, "checks");
                    if (ds.Tables["checks"].Rows.Count == 1)
                    {
                cmd.CommandText = "insert into [Lost/Damaged_items](item_code,cc_code,reporttype)values(@itemcode,@CCCode,'1')";
                cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = txtSearch.Text;
                cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                    }
            }
            else
            {
                string result = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
                string result1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
                da = new SqlDataAdapter("Select item_code from item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','5','5A','2A')", con);
                da.Fill(ds, "search");
                if (ds.Tables["search"].Rows.Count > 0)
                {
                    cmd.CommandText = "insert into  [Lost/Damaged_items](item_code,cc_code,reporttype)values(@itemcode,@CCCode,'1')";
                    cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = ds.Tables["search"].Rows[0].ItemArray[0].ToString();
                    cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                 
                }

            }
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (i == 1)
            {

            }

        Fillgrid();
            txtSearch.Text = "";
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow record in GridView1.Rows)
        {
            CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

            if (c1.Checked)
            {
                string id = GridView1.DataKeys[record.RowIndex]["id"].ToString();
                cmd.Connection = con;
                cmd.CommandText = "delete from [lost/damaged_items] where id='" + id + "'";
                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();
                Fillgrid();
            }
        }


    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string ids = "";
        string Qtys = "";
        string types = "";
        string reporttypes = "";
        string remarks = "";
        try
        {
            da = new SqlDataAdapter("select first_name+middle_name+last_name from Employee_Data where user_name='" + Session["user"].ToString() + "'", con);
            da.Fill(ds, "username");

            foreach (GridViewRow record in GridView1.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    //string id = "";
                    ids = ids + GridView1.DataKeys[record.RowIndex]["id"].ToString() + ",";
                    if (ddlindenttype.SelectedItem.Text != "Assets")
                    Qtys = Qtys + (record.FindControl("txtqty") as TextBox).Text + ",";
                    else
                        Qtys = Qtys + record.Cells[8].Text + ",";
                       

                    reporttypes = reporttypes + (record.FindControl("ddlreporttype") as DropDownList).SelectedValue + ",";
                    types = types + (record.FindControl("ddltype") as DropDownList).SelectedItem.Text + ",";
                    remarks = remarks + ((record.FindControl("txtremarks") as TextBox).Text + '_' + '_' + '_' + '@' + ds.Tables["username"].Rows[0][0].ToString()) + ",";
                }


            }
            cmd = new SqlCommand("Lost/Damaged Report_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ids", ids);
            cmd.Parameters.AddWithValue("@Qtys", Qtys);
            cmd.Parameters.AddWithValue("@ReportTypes", reporttypes);
            cmd.Parameters.AddWithValue("@Types", types);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@Date", txtdate.Text);
            cmd.Parameters.AddWithValue("@CCCode", Session["cc_code"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Refno", "");
            cmd.Parameters.AddWithValue("@Reportstype", ViewState["rtype"].ToString());
            cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            JavaScript.UPAlertRedirect(Page, msg, "Lost or Damaged Report.aspx");
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void ddlindenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        cascadingDCA cs = new cascadingDCA();
        cs.values(ddlindenttype.SelectedValue);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TextBox quantity = (TextBox)e.Row.FindControl("txtqty");
        DropDownList ddlitemtype = (DropDownList)e.Row.FindControl("ddltype");
        if (ViewState["rowcount"].ToString() != "0")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text.Substring(0, 1) == "1")
                {
                    e.Row.Cells[8].Text = "1";
                    quantity.Enabled = false;
                    ddlindenttype.SelectedIndex = 1;
                    ddlitemtype.Enabled = false;
                    ddlitemtype.SelectedValue = "2";
                }
            }
        }
        else
        {
            if (ddlindenttype.SelectedItem.Text == "Assets")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                e.Row.Cells[8].Text="1";
                quantity.Enabled = false;
                    ddlindenttype.SelectedIndex = 1;
                    ddlitemtype.Enabled = false;
                    ddlitemtype.SelectedValue = "2";
            }
        }

        }      
    }
    public void checkapprovals()
    {
        if (Session["roles"].ToString() == "StoreKeeper")
        {
            da = new SqlDataAdapter("SELECT TOP 1 * from  closedcost_center where cc_code = '" + Session["CC_CODE"].ToString() + "' and status not in('Rejected') ORDER by id desc", con);
            da.Fill(ds, "check");
            if (ds.Tables["check"].Rows.Count > 0)
            {
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "1")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Temporary Closed store approval pending at project Manager", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                        JavaScript.AlertAndRedirect("Store Re-open approval pending at project Manager", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at project Manager", "WareHouseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "2")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Temporary Closed store approval pending at central store keeper", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                        JavaScript.AlertAndRedirect("Store Re-open approval pending at central store keeper", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at central store keeper", "WareHouseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "3")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Store is in Temporary Closing Mode", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at chief Material Controller", "WareHouseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "4")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store is Already closed", "WareHouseHome.aspx");
                }
            }
        }
    }
}
