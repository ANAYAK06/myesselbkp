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

public partial class VerifyShareCapital : System.Web.UI.Page
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
        }
    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter(" select sc.transaction_no,sc.name,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,replace(sc.Amount,'.0000','.00')as Amount,bk.bank_name,bk.description from AddShareCapital sc join bankbook bk on sc.transaction_no=bk.transaction_no where sc.status='1'", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter(" select sc.transaction_no,sc.name,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,replace(sc.Amount,'.0000','.00')as Amount,bk.bank_name,bk.description from AddShareCapital sc join bankbook bk on sc.transaction_no=bk.transaction_no where sc.status='2'", con);
            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_RejectShareCapital", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tranid", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["transaction_no"].ToString();
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, msg, "VerifyShareCapital.aspx");
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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewEditIndex]["transaction_no"].ToString();
        ViewState["id"] = id;
        filltable();
        tblinvoice.Visible = true;

    }
    public void filltable()
    {
        try
        {
            da = new SqlDataAdapter("select sc.Type,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,sc.name as Name,replace(sc.Amount,'.0000','.00')as Amount,bk.bank_name,bk.modeofpay,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as bankdate,bk.no,bk.description,replace(bk.credit,'.0000','.00')as credit from AddShareCapital sc join bankbook bk on sc.transaction_no=bk.transaction_no where sc.transaction_no='" + ViewState["id"].ToString() + "'", con);
            da.Fill(ds, "data");
            txttype.Text = ds.Tables["data"].Rows[0]["Type"].ToString();
            txtsharedate.Text = ds.Tables["data"].Rows[0]["date"].ToString();
            txtshareholdername.Text = ds.Tables["data"].Rows[0]["Name"].ToString();
            txtamount.Text = ds.Tables["data"].Rows[0]["Amount"].ToString();
            txtbankname.Text = ds.Tables["data"].Rows[0]["bank_name"].ToString();
            txtmodeofpay.Text = ds.Tables["data"].Rows[0]["modeofpay"].ToString();
            txtbankdate.Text = ds.Tables["data"].Rows[0]["bankdate"].ToString();
            txtcheque.Text = ds.Tables["data"].Rows[0]["no"].ToString();
            txtdesc.Text = ds.Tables["data"].Rows[0]["description"].ToString();
            txtamt.Text = ds.Tables["data"].Rows[0]["credit"].ToString();
            if (ds.Tables["data"].Rows[0]["Type"].ToString() == "New")
            {
                Trledger.Visible = true;
                da = new SqlDataAdapter("SELECT id,Ledger_Type,(CASE WHEN Payment_Type='Debit' Then '0' Else '1' End)Payment_Type,Name,NatureGroupId,Group_id,Amount,Code,REPLACE(CONVERT(VARCHAR(11),Balance_Date, 106), ' ', '-') as Balance_Date FROM Ledger WHERE Code='" + ds.Tables["data"].Rows[0]["Name"].ToString() + "' and Ledger_Type='Capital'", con);
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
        }
        catch (Exception ex)
        { 
        
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
            int result;
           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_CreateShareCapital";
            cmd.Parameters.AddWithValue("@TransactionNo", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@Types", txttype.Text);
            if (txttype.Text == "New")
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
                cmd.Parameters.AddWithValue("@LedgerId", ViewState["LedgerID"].ToString());
            }
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Verified")
            {
                JavaScript.UPAlertRedirect(Page, "Verified Successfully", "verifyShareCapital.aspx");
                fillgrid();
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, msg, "verifyShareCapital.aspx");
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
}