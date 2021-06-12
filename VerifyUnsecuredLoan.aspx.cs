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

public partial class VerifyUnsecuredLoan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
            tblinvoice.Visible = false;
            trdedhead.Visible = false;
            trdedbody.Visible = false;
            Trledger.Visible = false;
        }
    }

    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter(" select sc.transaction_no,sc.name,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,replace(sc.Amount,'.0000','.00')as Amount,bk.bank_name,bk.description,sc.loan_no,sc.type from AddUnsecuredLoan sc join bankbook bk on sc.transaction_no=bk.transaction_no where sc.status in ('A1','A1R')", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter(" select sc.transaction_no,sc.name,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,replace(sc.Amount,'.0000','.00')as Amount,bk.bank_name,bk.description,sc.loan_no,sc.type from AddUnsecuredLoan sc join bankbook bk on sc.transaction_no=bk.transaction_no where sc.status in ('A2','A2R')", con);
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                
                GridView1.DataSource = ds.Tables["fill"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewEditIndex]["transaction_no"].ToString();
        ViewState["id"] = id;
        ViewState["Type"] = GridView1.DataKeys[e.NewEditIndex]["type"].ToString();
        filltable();
        tblinvoice.Visible = true;

    }
    public void filltable()
    {
        da = new SqlDataAdapter("select sc.Type,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,sc.name,replace(sc.Amount,'.0000','.00')as Amount,bk.bank_name,bk.modeofpay,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as bankdate,bk.no,bk.description,replace(bk.credit,'.0000','.00')as credit,sc.loan_no,sc.Intrest_Rate,replace(bk.debit,'.0000','.00')as debit,sc.Deduction_CCCode,sc.Deduction_DCA,sc.Deduction_SDCA,sc.Deduction_Amount,COALESCE(bk.credit,bk.debit)as BankAmt,sc.id from AddUnsecuredLoan sc join bankbook bk on sc.transaction_no=bk.transaction_no where sc.transaction_no='" + ViewState["id"].ToString() + "'", con);
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
           
            txtloantype.Text = ds.Tables["data"].Rows[0]["Type"].ToString();
            txtunsecurednoumber.Text = ds.Tables["data"].Rows[0]["loan_no"].ToString();
            txtloandate.Text = ds.Tables["data"].Rows[0]["date"].ToString();
            txtname.Text = ds.Tables["data"].Rows[0]["name"].ToString();
            if (txtloantype.Text == "New")
            {
                Trledger.Visible = true;
                da = new SqlDataAdapter("SELECT id,Ledger_Type,(CASE WHEN Payment_Type='Debit' Then '0' Else '1' End)Payment_Type,Name,NatureGroupId,Group_id,Amount,Code,REPLACE(CONVERT(VARCHAR(11),Balance_Date, 106), ' ', '-') as Balance_Date FROM Ledger WHERE Code='" + ds.Tables["data"].Rows[0]["id"].ToString() + "' and Ledger_Type='Unsecured Loan'", con);
                da.Fill(ds, "Ledgerinfo");
                if (ds.Tables["Ledgerinfo"].Rows.Count > 0)
                {
                    TextBox txtledgername = (TextBox)this.Ledger.FindControl("txtledgername");
                    DropDownList ddlsubgroup = (DropDownList)this.Ledger.FindControl("ddlsubgroup");
                    TextBox txtledbaldate = (TextBox)this.Ledger.FindControl("txtledbaldate");
                    TextBox txtopeningbal = (TextBox)this.Ledger.FindControl("txtopeningbal");
                    RadioButtonList rbtnpaymenttype = (RadioButtonList)this.Ledger.FindControl("rbtnpaymenttype");
                    ViewState["LedgerID"] = ds.Tables["Ledgerinfo"].Rows[0]["id"].ToString();
                    txtledgername.Text = ds.Tables["Ledgerinfo"].Rows[0]["Name"].ToString();
                    ddlsubgroup.SelectedValue = ds.Tables["Ledgerinfo"].Rows[0]["Group_id"].ToString();
                    txtledbaldate.Text = ds.Tables["Ledgerinfo"].Rows[0]["Balance_Date"].ToString();
                    txtopeningbal.Text = ds.Tables["Ledgerinfo"].Rows[0]["Amount"].ToString();
                    rbtnpaymenttype.SelectedValue = ds.Tables["Ledgerinfo"].Rows[0]["Payment_Type"].ToString();
                    txtledgername.Enabled = false;
                    ddlsubgroup.Enabled = false;
                    txtledbaldate.Enabled = false;
                    txtopeningbal.Enabled = false;
                    rbtnpaymenttype.Enabled = false;
                }
            }
            else
            {
                Trledger.Visible = false;
            }
            if (txtloantype.Text != "Return")
                txtintrestrate.Text = ds.Tables["data"].Rows[0]["Intrest_Rate"].ToString();
            txtamount.Text = ds.Tables["data"].Rows[0]["Amount"].ToString();
            txtbank.Text = ds.Tables["data"].Rows[0]["bank_name"].ToString();
            txtpaymenttype.Text = ds.Tables["data"].Rows[0]["modeofpay"].ToString();
            txtdate.Text = ds.Tables["data"].Rows[0]["bankdate"].ToString();
            txtcheque.Text = ds.Tables["data"].Rows[0]["no"].ToString();
            txtdesc.Text = ds.Tables["data"].Rows[0]["description"].ToString();
            txtamt.Text = ds.Tables["data"].Rows[0]["BankAmt"].ToString();
            ViewState["LedgerID"] = ds.Tables["data"].Rows[0]["id"].ToString();
            if (ds.Tables["data"].Rows[0]["Deduction_CCCode"].ToString() != "")
            {
                trdedhead.Visible = true;
                trdedbody.Visible = true;
                lbldedcheck.Text = "Yes";
                dcaname(ds.Tables["data"].Rows[0]["Deduction_DCA"].ToString());
                sdcaname(ds.Tables["data"].Rows[0]["Deduction_SDCA"].ToString());                
                txtdedamt.Text = ds.Tables["data"].Rows[0]["Deduction_Amount"].ToString();
            }
            else
            {
                trdedhead.Visible = true;
                trdedbody.Visible = false;
                lbldedcheck.Text = "No";
            }
        }
    }
    public string Ledgername = string.Empty;
    public string Subgroup = string.Empty;
    public string Baldate = string.Empty;
    public string OpeningBal = string.Empty;
    public string Paymenttype = string.Empty;
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = false;
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_CreateUnsecuredLoan";
            cmd.Parameters.AddWithValue("@Types", ViewState["Type"].ToString());
            cmd.Parameters.AddWithValue("@TransactionNo", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@LedgerId", ViewState["LedgerID"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Verified")
            {
                JavaScript.UPAlertRedirect(Page, "Verified Successfully", "VerifyUnsecuredLoan.aspx");
                fillgrid();
            }
            else
            {
                btnupdate.Visible = true;
                JavaScript.UPAlert(Page, msg);
            }
            con.Close();

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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_RejectUnsecuredLoan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tranid", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["transaction_no"].ToString();
            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["type"].ToString();
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
           
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, msg, "VerifyUnsecuredLoan.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
            fillgrid();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void dcaname(string code)
    {
        da = new SqlDataAdapter("select (mapdca_code+' , '+dca_name) as name from dca where dca_code='" + code + "'", con);
        da.Fill(ds, "dcas");
        if (ds.Tables["dcas"].Rows.Count > 0)
        {
            txtdeddca.Text = ds.Tables["dcas"].Rows[0]["name"].ToString();
        }
        else
        {
            txtdeddca.Text = "";
        }
    }
    public void sdcaname(string code)
    {
        da = new SqlDataAdapter("select (mapsubdca_code+' , '+subdca_name) as name from subdca where subdca_code='" + code + "'", con);
        da.Fill(ds, "sdcas");
        if (ds.Tables["sdcas"].Rows.Count > 0)
        {
            txtdedsdca.Text =  ds.Tables["sdcas"].Rows[0]["name"].ToString();
        }
        else
        {
            txtdedsdca.Text = "";
        }
    }

}