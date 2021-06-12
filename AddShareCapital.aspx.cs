using System;
using System.Collections.Generic;
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
using System.Drawing;

public partial class AddShareCapital : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtshareholdername.Visible = false;
            ddlshareholdername.Visible = false;
        
           // objldgr.evt += new EventHandler(userCntrl_evt); 
        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue == "New")
        {
            txtshareholdername.Visible = true;
            ddlshareholdername.Visible = false;
            Trledger.Visible = true;
        }
        else if (ddltype.SelectedValue == "Additional")
        {
            txtshareholdername.Visible = false;
            ddlshareholdername.Visible = true;
            Trledger.Visible = false;
            filladditionalcapital();
        }
        else
        {
            txtshareholdername.Visible = false;
            ddlshareholdername.Visible = false;
            Trledger.Visible = false;
        }
    }

    public void filladditionalcapital()
    {
        ds.Clear();
        da = new SqlDataAdapter("Select distinct name from AddShareCapital where status='3'", con);
        da.Fill(ds, "shareholdernames");
        if (ds.Tables["shareholdernames"].Rows.Count > 0)
        {
            ddlshareholdername.DataTextField = "name";
            ddlshareholdername.DataValueField = "name";
            ddlshareholdername.DataSource = ds.Tables["shareholdernames"];
            ddlshareholdername.DataBind();
            ddlshareholdername.Items.Insert(0, "Select ShareHolder Name");
        }
       
    }
  
    public string Ledgername = string.Empty;
    public string Subgroup = string.Empty;
    public string Baldate = string.Empty;
    public string OpeningBal = string.Empty;
    public string  Paymenttype = string.Empty;
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
      
  
        try
        {

            int result;
           
           
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                BankDateCheck objdate = new BankDateCheck();
                if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedValue))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_CreateShareCapital";
                    cmd.Parameters.AddWithValue("@Date", txtsharedate.Text);
                    cmd.Parameters.AddWithValue("@Types", ddltype.SelectedValue);
                    if (ddltype.SelectedValue == "New")
                    {
                        TextBox txtledgername = (TextBox)this.Ledger.FindControl("txtledgername");
                        DropDownList ddlsubgroup = (DropDownList)this.Ledger.FindControl("ddlsubgroup");
                        TextBox txtledbaldate = (TextBox)this.Ledger.FindControl("txtledbaldate");
                        TextBox txtopeningbal = (TextBox)this.Ledger.FindControl("txtopeningbal");
                        RadioButtonList rbtnpaymenttype = (RadioButtonList)this.Ledger.FindControl("rbtnpaymenttype");
                        Ledgername = txtledgername.Text;
                        Subgroup = ddlsubgroup.SelectedValue;
                        Baldate = txtledbaldate.Text;
                        OpeningBal = txtopeningbal.Text;
                        Paymenttype = rbtnpaymenttype.SelectedItem.Text;
                        cmd.Parameters.AddWithValue("@ShareHolderName", txtshareholdername.Text);
                        cmd.Parameters.AddWithValue("@LedgerName", Ledgername);
                        if (int.TryParse(Subgroup, out result))
                        {
                            cmd.Parameters.AddWithValue("@Grouptype", "subgroup");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Grouptype", "mastergroup");
                        }
                        cmd.Parameters.AddWithValue("@Subgroup", Subgroup);
                        cmd.Parameters.AddWithValue("@BalanceDate", Baldate);
                        cmd.Parameters.AddWithValue("@OpeningBal", OpeningBal);
                        cmd.Parameters.AddWithValue("@BalanceAt", Paymenttype);
                    }
                    else if (ddltype.SelectedValue == "Additional")
                        cmd.Parameters.AddWithValue("@ShareHolderName", ddlshareholdername.SelectedValue);
                    cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
                    cmd.Parameters.AddWithValue("@From", ddlfrom.SelectedValue);
                    cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@PaymentType", "ShareCapital");
                    cmd.Parameters.AddWithValue("@Bankdate", txtdate.Text);
                    cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                    cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());

                   
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                   // string msg = "";
                    if (msg == "Generated Successfully" || msg == "Ledger Created")
                    {
                        if (ddltype.SelectedValue == "New")
                        {
                            JavaScript.UPAlertRedirect(Page, "Share Holder Created Sucessfully", "AddShareCapital.aspx");
                        }
                        else
                        {
                            JavaScript.UPAlertRedirect(Page, "Share Value Updated Sucessfully", "AddShareCapital.aspx");
                        }
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, msg);
                    }
                    con.Close();
                }
                else
                {
                    JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddShareCapital.aspx");
    }
}