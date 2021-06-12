using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class LedgerCreation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["roles"] != null)
        {
            if(!IsPostBack)
            {
                FillGroups();
                fillloanno();
                fillBankname();
                checkcashledger();
                fillitcode();
            }
        }
        else
        {
            Response.Redirect("SessionExpire.aspx");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int result;
            cmd = new SqlCommand("sp_CreateLedgerNew", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (rbtntype.SelectedIndex != 4)
            {
                cmd.Parameters.AddWithValue("@ledgerType", rbtntype.SelectedItem.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ledgerType", rbtnledgers.SelectedItem.Text);
            }
            if (rbtntype.SelectedIndex == 2)
                cmd.Parameters.AddWithValue("@InvoiceType", rbtninvoicetype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@LedgerName", txtledgername.Text);
            if (int.TryParse(ddlsubgroup.SelectedValue, out result))
            {
                cmd.Parameters.AddWithValue("@Grouptype", "subgroup");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Grouptype", "mastergroup");
            }
            cmd.Parameters.AddWithValue("@Subgroup", ddlsubgroup.SelectedValue);
            if (rbtntype.SelectedIndex == 2)
            {
                if(rbtninvoicetype.SelectedIndex==0)
                    cmd.Parameters.AddWithValue("@Code", ddlvendorname.SelectedValue);
                else
                    cmd.Parameters.AddWithValue("@Code", ddlclient.SelectedValue);
            }
            if (rbtntype.SelectedIndex == 0 || rbtntype.SelectedIndex == 1)
            {
                cmd.Parameters.AddWithValue("@Code", ddlitcode.SelectedValue);

            }
            if (rbtntype.SelectedIndex == 4)
            {
                if(rbtnledgers.SelectedIndex==1)
                    cmd.Parameters.AddWithValue("@Code", ddlbankname.SelectedValue);
                else if(rbtnledgers.SelectedIndex==2)
                    cmd.Parameters.AddWithValue("@Code", ddltermloan.SelectedValue);
                else if (rbtnledgers.SelectedIndex == 3)
                    cmd.Parameters.AddWithValue("@InvoiceType", ddltype.SelectedItem.Text);
            }
            if (rbtntype.SelectedIndex == 3)
            {
                cmd.Parameters.AddWithValue("@Code", ddlitcode.SelectedValue + "," + ddlgstnos.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@BalanceDate", txtbaldate.Text);
            cmd.Parameters.AddWithValue("@Amount", txtopeningbal.Text);          
            cmd.Parameters.AddWithValue("@PaymentType", rbtnpaymenttype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());           
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "Ledger Inserted1";
            con.Close();
            if (msg == "Ledger Created")
                JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
            else
            {
                JavaScript.UPAlert(Page, msg);
                tblbtnupdate.Visible = true;                
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
    private void fillloanno()
    {
        da = new SqlDataAdapter("select code from Ledger where status !='Rejected' and Ledger_Type='Term Loan Ledger'", con);
        da.Fill(ds, "checkloanno");
        DataTable dt = new DataTable();
        da.Fill(ds);
        dt = ds.Tables["checkloanno"];
        string str = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str = str + dt.Rows[i]["code"].ToString();
            str += (i < dt.Rows.Count - 1) ? "','" : string.Empty;
        }
        if (ds.Tables["checkloanno"].Rows.Count > 0)
        {
            da = new SqlDataAdapter("select loanno from TermLoan where loanno not in ('" + str + "') ", con);
        }
        else
        {
            da = new SqlDataAdapter("select loanno from TermLoan", con);
        }
        da.Fill(ds, "loan");
        ddltermloan.DataTextField = "loanno";
        ddltermloan.DataValueField = "loanno";
        ddltermloan.DataSource = ds.Tables["loan"];
        ddltermloan.DataBind();
        ddltermloan.Items.Insert(0, "Select Loan No");

    }
    private void fillBankname()
    {
        da = new SqlDataAdapter("select code from ledger where status !='Rejected' and Ledger_Type='Bank Ledger'", con);
        da.Fill(ds, "checkbank");
        DataTable dt1 = new DataTable();
        da.Fill(ds);
        dt1 = ds.Tables["checkbank"];
        string str = string.Empty;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            str = str + dt1.Rows[i]["code"].ToString();
            str += (i < dt1.Rows.Count - 1) ? "," : string.Empty;
        }

        if (ds.Tables["checkbank"].Rows.Count > 0)
        {
            da = new SqlDataAdapter("select bank_id,(bank_name+',  ( '+bank_location+' )')as Name from bank_branch where bank_id not in (" + str + ")", con);
        }
        else
        {
            da = new SqlDataAdapter("select bank_id,(bank_name+',  ( '+bank_location+' )')as Name from bank_branch", con);
        }
            da.Fill(ds, "bankname");
            ddlbankname.DataTextField = "Name";
            ddlbankname.DataValueField = "bank_id";
            ddlbankname.DataSource = ds.Tables["bankname"];
            ddlbankname.DataBind();
            ddlbankname.Items.Insert(0, "Select Bank Name");
    }  
    public void checkcashledger()
    {
        da = new SqlDataAdapter("select * from ledger where status !='Rejected' and Ledger_Type='Cash Ledger'", con);
        da.Fill(ds, "chkcashledger");
        if (ds.Tables["chkcashledger"].Rows.Count > 0)
        {
           rbtnledgers.Items[0].Enabled = false; 
        }
    }

    public void fillitcode()
    {
        da = new SqlDataAdapter("Select it_code,it_code +' (' +it_name + ')' as Name from it", con);
        da.Fill(ds, "itcode");
        ddlitcode.DataTextField = "Name";
        ddlitcode.DataValueField = "it_code";
        ddlitcode.DataSource = ds.Tables["itcode"];
        ddlitcode.DataBind();
        ddlitcode.Items.Insert(0, "Select IT Code");
    }
    public void FillGroups()
    {
        try
        {
            da = new SqlDataAdapter("Select  CONVERT(varchar(10), id)as id,Name from Sub_Group where status='3'union all select Group_id as id,group_name as Name from mastergroups where status='3'", con);
            da.Fill(ds, "Subgroups");
            ddlsubgroup.DataTextField = "Name";
            ddlsubgroup.DataValueField = "id";
            ddlsubgroup.DataSource = ds.Tables["Subgroups"];
            ddlsubgroup.DataBind();
            ddlsubgroup.Items.Insert(0, "Select Sub-Groups");
        }
        catch (Exception ex)
        { 
        
        }
    }


    protected void ddlitcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedItem.Text == "General")
        {
            da = new SqlDataAdapter("SELECT 1 as IsExists FROM Ledger where code='" + ddlitcode.SelectedValue + "' and ledger_Type='General' and status !='Rejected'", con);
            da.Fill(ds, "checksitcode");
            if (ds.Tables["checksitcode"].Rows.Count == 1)
            {
                JavaScript.UPAlert(Page, "The Selected IT Code is Exists in General Ledger");
                ddlitcode.SelectedIndex = 0;
            }
           
        }
        if (rbtntype.SelectedItem.Text == "General Payable")
        {
            da = new SqlDataAdapter("SELECT 1 as IsExists FROM Ledger where code='" + ddlitcode.SelectedValue + "' and ledger_Type='General Payable' and status !='Rejected'", con);
            da.Fill(ds, "checksitcode");
            if (ds.Tables["checksitcode"].Rows.Count == 1)
            {
                JavaScript.UPAlert(Page, "The Selected IT Code is Exists in General Payable Ledger");
                ddlitcode.SelectedIndex = 0;
            }
           
        }
       
      
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
        //if (rbtntype.SelectedItem.Text == "Tax Ledger")
        //{
        //    for (int i = 0; i < ddlsubgroup.Items.Count; i++)
        //    {
        //        if (ddlsubgroup.Items[i].Text != "CONSUMABLES")
        //        {
        //            ddlsubgroup.Items[i].Attributes.Add("disabled", "disabled");
        //        }
        //    }
        //}
    }
}