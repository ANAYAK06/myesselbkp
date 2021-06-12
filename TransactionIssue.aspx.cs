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


public partial class TransactionIssue : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Warehouse");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
       //esselDal RoleCheck = new esselDal();
       // int rec = 0;
       //  rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 15);
       // if (rec == 0)
       //     Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            checkapprovals();
            FillGrid();

        }
    }
    public void FillGrid()
    {
        try
        {
            da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')basic_price,units,Replace(il.quantity,'.00','')quantity,isnull([Available Qty],0)[Available Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,(Select quantity from master_data where cc_code='" + Session["cc_code"].ToString() + "' and item_code=it.item_code)as [Available Qty] from [Daily Issued Items]it where transaction_id is null and U_Id is null and cc_code='" + Session["cc_code"].ToString() + "' )il on i.item_code=il.item_code", con);
            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();

            if (ds.Tables["fill"].Rows.Count > 0)
            {
                btnvisible.Style.Add("visibility", "visible");
                tbldesc.Style.Add("visibility", "visible");
            }
            else
            {
                btnvisible.Style.Add("visibility", "hidden");
                tbldesc.Style.Add("visibility", "hidden");
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
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
                cmd.CommandText = "insert into [Daily Issued Items](item_code,cc_code)values(@itemcode,@CCCode)";
                cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = txtSearch.Text;
                cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
            }
            else
            {
                string result = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
                string result1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
                da = new SqlDataAdapter("Select item_code from item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','5','5A','2A')", con);
                da.Fill(ds, "search");
                if (ds.Tables["search"].Rows.Count > 0)
                {
                    cmd.CommandText = "insert into [Daily Issued Items](item_code,cc_code)values(@itemcode,@CCCode)";
                    cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = ds.Tables["search"].Rows[0].ItemArray[0].ToString();
                    cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();

                }

            }
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (i == 1)
            {

            }

            FillGrid();
            (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
            txtSearch.Text = String.Empty;

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

   
  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Qty = "";
            string Date = txtdate.Text;

           
            foreach (GridViewRow record in GridView1.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                if ((record.FindControl("ChkSelect") as CheckBox).Checked)
                {
                    ids = ids + GridView1.DataKeys[record.RowIndex]["id"].ToString() + ",";
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
                JavaScript.UPAlertRedirect(Page,msg, "TransactionIssue.aspx");
            else
                JavaScript.UPAlert(Page,msg);
           
            

            con.Close();
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
                cmd.CommandText = "delete from [Daily Issued Items] where id='" + id + "'";
                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();
                FillGrid();
            }
        }


    }
    protected void ddlindenttype_SelectedIndexChanged(object sender, EventArgs e)
    {

        cascadingDCA cs = new cascadingDCA();
        cs.values(ddlindenttype.SelectedValue);

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
