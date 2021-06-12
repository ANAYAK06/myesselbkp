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


public partial class CreateUnsecuredLoan : System.Web.UI.Page
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
            txtunsecurednoumber.Visible = false;
            ddlunsecuredloanno.Visible = false;
            lbldatetext.Text = "Loan Date";
            Label2.Visible = false;
            txtintrestrate.Visible = true;
            Label6.Visible = true;
            tbldeduction.Visible = false;
            ddlcheque.Visible = false;
            txtcheque.Visible = true;
            //Trledger.Visible = false;
        }
        hfdate.Value = "";   
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue == "New")
        {
            
            tbldeduction.Visible = false;
            Label2.Visible = true;
            txtunsecurednoumber.Visible = true;
            ddlunsecuredloanno.Visible = false;
            txtname.Enabled = true;
            lbldatetext.Text = "Loan Date";
            txtintrestrate.Visible = true;
            Label6.Visible = true;
            Trledger.Visible = true;
            clear();

        }
        else if (ddltype.SelectedValue == "Additional")
        {
            tbldeduction.Visible = false;
            Label2.Visible = true;
            txtunsecurednoumber.Visible = false;
            ddlunsecuredloanno.Visible = true;
            clear();
            lbldatetext.Text = "Additional Loan Date";
            txtintrestrate.Visible = true;
            Label6.Visible = true;
            Trledger.Visible = false;
            filladditionalunsecuredloannos();
            
        }
        else if (ddltype.SelectedValue == "Return")
        {
            tbldeduction.Visible = true;
            Label2.Visible = true;
            txtunsecurednoumber.Visible = false;
            ddlunsecuredloanno.Visible = true;
            clear();
            lbldatetext.Text = "Loan Return Date";
            txtintrestrate.Visible = false;
            Label6.Visible = false;
            Trledger.Visible = false;
            filladditionalunsecuredloannos();           

        }
        else
        {
            tbldeduction.Visible = false;
            Label2.Visible = false;
            txtunsecurednoumber.Visible = false;
            ddlunsecuredloanno.Visible = false;
            txtname.Enabled = true;
            lbldatetext.Text = "Loan Date";
            txtintrestrate.Visible = true;
            Label6.Visible = true;
            clear();
        }
        fillbankpaymenttypewithcheque(ddltype.SelectedValue);
    }
    public void filladditionalunsecuredloannos()
    {
        ds.Clear();
        da = new SqlDataAdapter("Select distinct Loan_no from AddUnsecuredLoan where status='A3' and Type='New'", con);
        da.Fill(ds, "loannos");
        if (ds.Tables["loannos"].Rows.Count > 0)
        {
            ddlunsecuredloanno.DataTextField = "Loan_no";
            ddlunsecuredloanno.DataValueField = "Loan_no";
            ddlunsecuredloanno.DataSource = ds.Tables["loannos"];
            ddlunsecuredloanno.DataBind();
            ddlunsecuredloanno.Items.Insert(0, "Select Loan Nos");
        }
    }   
    protected void ddlunsecuredloanno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue == "Additional" || ddltype.SelectedValue == "Return")
        {
            if (ddlunsecuredloanno.SelectedValue != "Select Loan Nos")
            {
                da = new SqlDataAdapter("Select name,CAST(Intrest_Rate AS Decimal(20,2))as Intrest_Rate from AddUnsecuredLoan where loan_no='" + ddlunsecuredloanno.SelectedValue + "' and status='A3' and Type='New'", con);
                da.Fill(ds, "details");
                if (ds.Tables["details"].Rows.Count > 0)
                {
                    txtname.Text = ds.Tables["details"].Rows[0]["name"].ToString();
                    txtintrestrate.Text = ds.Tables["details"].Rows[0]["Intrest_Rate"].ToString();
                    txtname.Enabled = false;
                    da = new SqlDataAdapter("Select top 1 REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')as Date from AddUnsecuredLoan where loan_no='" + ddlunsecuredloanno.SelectedValue + "' and status='A3'", con);
                    da.Fill(ds, "date");
                    if (ds.Tables["date"].Rows.Count > 0)
                    {
                        hfdate.Value = ds.Tables["date"].Rows[0]["Date"].ToString();
                    }

                }
            }
            else
            {
                clear();
            }
        }
        else
        {
            clear();
        }
    }
    public void clear()
    {
        txtunsecurednoumber.Text = "";
        txtname.Text = "";
        txtintrestrate.Text = "";
    }
    public string Ledgername = string.Empty;
    public string Subgroup = string.Empty;
    public string Baldate = string.Empty;
    public string OpeningBal = string.Empty;
    public string Paymenttype = string.Empty;
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
                    cmd.CommandText = "sp_CreateUnsecuredLoan";
                    cmd.Parameters.AddWithValue("@Date", txtloandate.Text);
                    cmd.Parameters.AddWithValue("@Types", ddltype.SelectedValue);
                    if (ddltype.SelectedValue == "New")
                    {
                        cmd.Parameters.AddWithValue("@LoanNo", txtunsecurednoumber.Text);
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
                    else if (ddltype.SelectedValue == "Additional" || ddltype.SelectedValue == "Return")
                        cmd.Parameters.AddWithValue("@LoanNo", ddlunsecuredloanno.SelectedValue);
                    cmd.Parameters.AddWithValue("@Name", txtname.Text);
                    if (ddltype.SelectedValue != "Return")
                        cmd.Parameters.AddWithValue("@IntrestRate", txtintrestrate.Text);
                    if (ddltype.SelectedValue == "Return")
                    {
                        if (ddlselection.SelectedValue == "Yes")
                        {
                            cmd.Parameters.AddWithValue("@DCA", ddldeddca.SelectedValue);
                            cmd.Parameters.AddWithValue("@SDCA", ddldedsdca.SelectedValue);
                            cmd.Parameters.AddWithValue("@DedAmt", txtdedamount.Text);
                        }
                    }
                    cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
                    cmd.Parameters.AddWithValue("@From", ddlfrom.SelectedValue);
                    cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@PaymentType", "Unsecured Loan");
                    cmd.Parameters.AddWithValue("@Bankdate", txtdate.Text);
                    if (ddlpayment.SelectedItem.Text == "Cheque")
                    {
                        cmd.Parameters.AddWithValue("@No", ddlcheque.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@chequeid", ddlcheque.SelectedValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                    }
                    cmd.Parameters.AddWithValue("@BankAmount", txtamt.Text);
                    cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                  //  string msg = "";
                    if (msg == "Generated Successfully" || msg == "Ledger Created")                    
                    {
                        JavaScript.UPAlertRedirect(Page, "Submitted", "CreateUnsecuredLoan.aspx");
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
        Response.Redirect("CreateUnsecuredLoan.aspx");
    }

    public static string GetFinancialYear(string cdate)
    {
        string finyear = "";
        DateTime dt = Convert.ToDateTime(cdate);
        int m = dt.Month;
        int y = dt.Year;
        if (m > 3)
        {
            finyear = y.ToString() + "-" + Convert.ToString((y + 1)).Substring(2, 2);          
        }
        else
        {
            finyear = Convert.ToString((y - 1))+ "-" + y.ToString().Substring(2, 2);
        }
        return finyear;
    }
    protected void ddlselection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlselection.SelectedValue == "Yes")
        {
            if (txtloandate.Text != "")
            {
                txtloandate.Enabled = false;
                ds.Clear();
                da = new SqlDataAdapter("    select d.dca_code,(d.mapdca_code+' , '+d.dca_name) as dca from yearly_dcabudget yd join dca d on yd.dca_code=d.dca_code where cc_code='CC-12' and yd.dca_code='DCA-45' and year='" + GetFinancialYear(txtloandate.Text) + "'", con);
                da.Fill(ds, "dcas");
                if (ds.Tables["dcas"].Rows.Count > 0)
                {
                    ddldeddca.DataTextField = "dca";
                    ddldeddca.DataValueField = "dca_code";
                    ddldeddca.DataSource = ds.Tables["dcas"];
                    ddldeddca.DataBind();
                    ddldeddca.Items.Insert(0, "Select DCA Code");
                }
                ddldeddca.Enabled = true;
                ddldedsdca.Enabled = true;
                txtdedamount.Enabled = true;

            }
            else
            {
                txtloandate.Enabled = true;
                ddlselection.SelectedValue = "Select";
                JavaScript.UPAlert(Page,"Please Select Date");
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        }
        if ((ddlselection.SelectedValue == "No") || (ddlselection.SelectedValue == "Select"))
        {
            txtloandate.Enabled = true;
            ddldeddca.SelectedValue="Select DCA Code";
            ddldedsdca.SelectedValue="Select SDCA";
            ddldeddca.Enabled = false;
            ddldedsdca.Enabled = false;  
            txtdedamount.Enabled = false;
            txtdedamount.Text = "";
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        }
    }
    protected void ddlddldeddca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddldedsdca.Items.Clear();
            da = new SqlDataAdapter("SELECT (mapsubdca_code+' ,' +subdca_name)as name, subdca_code from subdca  where dca_code='" + ddldeddca.SelectedValue + "' ", con);
            da.Fill(ds, "sdcaTypeI");
            if (ds.Tables["sdcaTypeI"].Rows.Count > 0)
            {
                ddldedsdca.DataTextField = "name";
                ddldedsdca.DataValueField = "subdca_code";
                ddldedsdca.DataSource = ds.Tables["sdcaTypeI"];
                ddldedsdca.DataBind();
                ddldedsdca.Items.Insert(0, "Select SDCA");
            }
            else
            {
                ddldedsdca.Items.Insert(0, "Select SDCA");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void fillbankpaymenttypewithcheque(string type)
    {
        ddlpayment.Items.Clear();
        ddlpayment.Items.Add("-Select-");
        if(type=="Return")
        ddlpayment.Items.Add("Cheque");
        ddlpayment.Items.Add("RTGS/E-transfer");
        ddlpayment.Items.Add("DD");       
    }
    protected void ddlpayment_SelectedIndexChanged(object sender, EventArgs e)
    {
      
            if (ddlpayment.SelectedItem.Text == "Cheque")
            {
                ddlcheque.Visible = true;
                txtcheque.Visible = false;
            }
            else
            {
                ddlcheque.Visible = false;
                txtcheque.Visible = true;
            }
        
    }
    protected void ddlfrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcheque.Items.Clear();
        da = new SqlDataAdapter("select cn.chequeno,cn.id from cheque_nos cn join cheque_Master cm on cn.chequeid=cm.chequeid where cm.bankname='" + ddlfrom.SelectedItem.Text + "' and cn.status='2'", con);
        da.Fill(ds, "chequeno");
        if (ds.Tables["chequeno"].Rows.Count > 0)
        {
            ddlcheque.DataSource = ds.Tables["chequeno"];
            ddlcheque.DataTextField = "chequeno";
            ddlcheque.DataValueField = "id";
            ddlcheque.DataBind();
            ddlcheque.Items.Insert(0, "Select");
        }
        else
        {
            ddlcheque.Items.Insert(0, "Select");
        }
    }
}