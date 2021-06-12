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
public partial class SiteStorePermanentClose : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["CCCode"] != null)
            {
                Label1.Text = Request.QueryString["CCCode"].ToString() + " Permanent Store Closing Items";
                da = new SqlDataAdapter("select md.item_code,ic.item_name,ic.specification,md.quantity from Master_data md join Item_codes ic on SUBSTRING(md.item_code,1,8)=ic.item_code where CC_code='" + Request.QueryString["CCCode"].ToString() + "' and Quantity !=0 order by md.Item_code asc", con);
                da.Fill(ds, "close");
                gvccpermanentclosed.DataSource = ds.Tables["close"];
                gvccpermanentclosed.DataBind();
            }
        }
    }

    protected void gvccpermanentclosed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox issuefromcc = (TextBox)e.Row.Cells[4].FindControl("txtissuefromcc");
            TextBox lostordamaged = (TextBox)e.Row.Cells[5].FindControl("txtlostordamaged");
            TextBox transfertocs = (TextBox)e.Row.Cells[6].FindControl("txttransfertocs");
            if (e.Row.Cells[0].Text.ToString().Substring(0, 1) == "1" || e.Row.Cells[0].Text.ToString().Substring(0, 1) == "2")
            {
                issuefromcc.Text = "0";
                issuefromcc.Enabled = false;
                lostordamaged.Enabled = true;
                transfertocs.Enabled = true;

            }
            if (e.Row.Cells[0].Text.ToString().Substring(0, 1) == "3")
            {
                issuefromcc.Enabled = true;
                lostordamaged.Enabled = true;
                transfertocs.Enabled = true;
            }
            balanceqty += Convert.ToDecimal((e.Row.Cells[3].Text));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = String.Format("{0:0.##}", balanceqty);
        }
    }
    private decimal balanceqty = (decimal)0.0;

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string itemcodes = "";
            string issuefromcc = "";
            string lostordamaged = "";
            string transfertocs = "";
            if (Request.QueryString["Role"].ToString() == "StoreKeeper")
            {
                foreach (GridViewRow rec in gvccpermanentclosed.Rows)
                {
                    TextBox issue = (TextBox)rec.FindControl("txtissuefromcc");
                    TextBox lost = (TextBox)rec.FindControl("txtlostordamaged");
                    TextBox transfer = (TextBox)rec.FindControl("txttransfertocs");
                    if (issue.Text == "")
                        issue.Text = "0";
                    if (lost.Text == "")
                        lost.Text = "0";
                    if (transfer.Text == "")
                        transfer.Text = "0";
                    itemcodes = itemcodes + (rec.Cells[0].Text) + ",";
                    issuefromcc = issuefromcc + issue.Text + ",";
                    lostordamaged = lostordamaged + lost.Text + ",";
                    transfertocs = transfertocs + transfer.Text + ",";
                }                
                cmd = new SqlCommand("SiteStorePermanentclose_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CCCode", Request.QueryString["CCCode"].ToString());
                cmd.Parameters.AddWithValue("@itemcodes", itemcodes);
                cmd.Parameters.AddWithValue("@issuefromccs", issuefromcc);
                cmd.Parameters.AddWithValue("@lostordamages", lostordamaged);
                cmd.Parameters.AddWithValue("@transfertocss", transfertocs);
                cmd.Parameters.AddWithValue("@role", Request.QueryString["Role"].ToString());
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "Sucessfull")
                {
                    JavaScript.POPUPClosing("Sucessfull");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
                }
                else
                    JavaScript.Alert("Insertion Failed");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    
}