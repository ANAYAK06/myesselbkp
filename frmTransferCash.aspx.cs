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
using System.Web.Services;
public partial class frmTransferCash : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            Label1.Text = "Cash Transfer Form";
            Label2.Visible = false;
            trlist.Visible = false;
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                Label1.Text = "Central Day Book Form";
                Label2.Visible = true;
                ddltransfertype.Items.Remove(ddltransfertype.Items.FindByValue("CentralDayBook"));
                fillgrid();
            }
        
        }

    }
    
    protected void ddltransfertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            if (ddltransfertype.SelectedItem.Text == "Bank")
            {
                trlist.Visible = true;
                lbllist.Text = "Bank";


                da = new SqlDataAdapter("select bank_name,bank_id FROM bank_branch ", con);
                da.Fill(ds, "bankname");

                ddllist.DataTextField = "bank_name";
                ddllist.DataValueField = "bank_id";
                ddllist.DataSource = ds.Tables["bankname"];
                ddllist.DataBind();
                ddllist.Items.Insert(0, "Select Bank");


            }
            else if (ddltransfertype.SelectedItem.Text == "CostCenter")
            {
                trlist.Visible = true;
                lbllist.Text = "Cost Center";
                da = new SqlDataAdapter("select cc_code,cc_name from cost_center where cc_code NOT in ('" + Session["cc_code"].ToString() + "') GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC ", con);
                da.Fill(ds, "costcenter");

                ddllist.DataTextField = "cc_code";
                ddllist.DataValueField = "cc_code";
                ddllist.DataSource = ds.Tables["costcenter"];
                ddllist.DataBind();
                ddllist.Items.Insert(0, "Select Cost Center");
            }

            else
            {
                trlist.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    public void fillgrid()
    {
        
        da = new SqlDataAdapter("select isnull(Balance,0) from CompanyBalance", con);
        da.Fill(ds, "bal");
        lblbal.Text = ds.Tables["bal"].Rows[0][0].ToString().Replace(".0000", ".00");
        da = new SqlDataAdapter("select Sum(balance) from cost_center", con);
        da.Fill(ds, "totbal");
        lbltot.Text = "Total Balance with CostCenters: " + ds.Tables["totbal"].Rows[0][0].ToString().Replace(".0000", ".00");
        da = new SqlDataAdapter("SELECT SUM(debit) FROM cash_transfer where type=1 and status NOT IN ('Rejected','3')", con);
        da.Fill(ds, "debit");
        lbldebitbal.Text = "Debit Pending Balance with CostCenters: " + ds.Tables["debit"].Rows[0][0].ToString().Replace(".0000", ".00");
        
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        try
        {
            cmd.Connection = con;

            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                cmd = new SqlCommand("Central_Day_Book_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                cmd = new SqlCommand("transfercash_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
            }

            cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = ddltransfertype.SelectedItem.Text;
            if (ddltransfertype.SelectedItem.Text == "Bank")
            {
                cmd.Parameters.Add("@bank", SqlDbType.VarChar).Value = ddllist.SelectedItem.Text;
            }
            else if (ddltransfertype.SelectedItem.Text == "CostCenter")
            {
                cmd.Parameters.Add("@costcentre", SqlDbType.VarChar).Value = ddllist.SelectedItem.Text;
            }


            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Parameters.Add("@roles", SqlDbType.VarChar).Value = Session["roles"].ToString();

            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtdesc.Text;
            cmd.Parameters.Add("@Amount", SqlDbType.Money).Value = txtamt.Text;
            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = txtdt.Text;


            con.Open();
            if (ddltransfertype.SelectedItem.Text == "Bank")
            {
                BankDateCheck objdate = new BankDateCheck();
                if (objdate.IsBankDateCheck(txtdt.Text, ddllist.SelectedItem.Text))
                {
                    string msg = cmd.ExecuteScalar().ToString();
                    if (msg == "Successfull")
                    {
                        JavaScript.UPAlertRedirect(Page, msg, "frmTransferCash.aspx");
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, msg);
                    }
                }
                else
                {
                    JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
                }
            }
            else
            {
                string msg = cmd.ExecuteScalar().ToString();
                if (msg == "Successfull")
                {
                    JavaScript.UPAlertRedirect(Page, msg, "frmTransferCash.aspx");
                }
                else
                {
                    JavaScript.UPAlert(Page, msg);
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }
    }
   
}