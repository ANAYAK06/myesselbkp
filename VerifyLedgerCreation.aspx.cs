using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class VerifyLedgerCreation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["roles"] != null)
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                lblheading.Text = "Verify Ledger";
                btnsubmit.Text = "Verify";
                lblheader.Text = "Verify Ledger";
                Title = "Verify Ledger";
            }
            else
            {
                lblheading.Text = "Approve Ledger";
                lblheader.Text = "Approve Ledger";
                btnsubmit.Text = "Approve";
                Title = "Approve Ledger";
            }
            if (!IsPostBack)
            {
                fillgrid();
                fillitcode();
                FillGroups();
                tblledger.Visible = false;
                checkcashledger();
            }
        }
        else
        {
            Response.Redirect("SessionExpire.aspx");
        }
        lblbankid.Visible = false;
    }
    protected void gvledgers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd.Connection = con;
            cmd = new SqlCommand("sp_LedgerDeletion", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", gvledgers.DataKeys[e.RowIndex]["id"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Rejected")
            {
                JavaScript.UPAlert(Page, msg);
                reset();
                tblledger.Visible = false;
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
            con.Close();
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select Id,Ledger_type,Name,Amount from Ledger where Status='1'", con);
            else
                da = new SqlDataAdapter("select Id,Ledger_type,Name,Amount from Ledger where Status='2'", con);

            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                gvledgers.DataSource = ds.Tables["fill"];
                gvledgers.DataBind();
            }
            else
            {
                gvledgers.EmptyDataText = "No Data avaliable for the selection criteria";
                gvledgers.DataSource = null;
                gvledgers.DataBind();
                //btn.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void gvledgers_SelectedIndexChanged(object sender, EventArgs e)
    {
        //tblledger.Visible = true;
        try
        {
            reset();
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Display()", true);
            da = new SqlDataAdapter("select ledger_type,payment_type,Invoice_Type,Name,coalesce(SubGroup_id,Group_id)as SubGroup_id,Amount,code,code,code,REPLACE(CONVERT(VARCHAR(11),Balance_Date, 106), ' ', '-') as Balance_Date,code,code from Ledger where ID='" + gvledgers.SelectedValue.ToString() + "'", con);
            da.Fill(ds, "ledgerinfo");
            if (ds.Tables["ledgerinfo"].Rows.Count > 0)
            {
                
                tblledger.Visible = true;
                rbtntype.Enabled = false;
                rbtnledgers.Enabled = false;
                rbtninvoicetype.Enabled = false;
                if ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() != "Cash Ledger") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() != "Bank Ledger") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() != "Term Loan Ledger") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() != "Invoice Ledger"))
                {
                    rbtntype.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    rbtntype.SelectedValue = "Other Ledgers";
                    rbtnledgers.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString();
                }
                rbtnpaymenttype.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[1].ToString();
                if (ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString() != "")
                {
                    if ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString() != "Service Tax Invoice") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString() != "SEZ/Service Tax exumpted Invoice") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString() != "VAT/Material Supply") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString() != "Trading Supply") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString() != "Manufacturing"))
                        rbtninvoicetype.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString();
                    else
                        ddltype.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString();
                }
                txtledgername.Text = ds.Tables["ledgerinfo"].Rows[0].ItemArray[3].ToString();
                txtledgername.Enabled = false;
                ddlsubgroup.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[4].ToString();
                txtopeningbal.Text = ds.Tables["ledgerinfo"].Rows[0].ItemArray[5].ToString().Replace(".0000",".00");
                if ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[6].ToString() != "") && ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "General") ||(ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "General Payable")))
                    ddlitcode.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[6].ToString();
                if ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[7].ToString() != "") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "Invoice Ledger") || (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "Client/Vendor Ledger"))
                {
                    if (ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString() == "Creditor")
                    {
                        da = new SqlDataAdapter("select vendor_type from vendor where vendor_id='" + ds.Tables["ledgerinfo"].Rows[0].ItemArray[7].ToString() + "'", con);
                        da.Fill(ds, "vendorname");
                        CascadingDropDown3.SelectedValue = ds.Tables["vendorname"].Rows[0].ItemArray[0].ToString();
                        CascadingDropDown4.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[7].ToString();
                    }
                }
                if ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[8].ToString() != "") && ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "Invoice Ledger") || (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "Client/Vendor Ledger")) && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[2].ToString() == "Debitor"))
                    CascadingDropDown5.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[8].ToString();              
                txtbaldate.Text = ds.Tables["ledgerinfo"].Rows[0].ItemArray[9].ToString();
                if ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[10].ToString() != "") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "Bank Ledger"))
                {
                    da = new SqlDataAdapter("select bank_id,(bank_name+',  ( '+bank_location+' )') as Name from bank_branch where bank_id='" + ds.Tables["ledgerinfo"].Rows[0].ItemArray[10].ToString() + "'", con);
                    da.Fill(ds, "bankname");
                    lblbankid.Text = ds.Tables["bankname"].Rows[0].ItemArray[0].ToString();
                    lblbankname.Text = ds.Tables["bankname"].Rows[0].ItemArray[1].ToString();
                }
                if ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[11].ToString() != "") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "Term Loan Ledger"))
                {
                   lbltermloan.Text = ds.Tables["ledgerinfo"].Rows[0].ItemArray[11].ToString();
                }
                if ((ds.Tables["ledgerinfo"].Rows[0].ItemArray[10].ToString() != "") && (ds.Tables["ledgerinfo"].Rows[0].ItemArray[0].ToString() == "Tax Ledger"))
                {
                    ddlitcode.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[10].ToString().Split(new string[] { "," }, StringSplitOptions.None).ToArray()[0];
                    CascadingDropDown6.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[10].ToString().Split(new string[] { "," }, StringSplitOptions.None).ToArray()[1];
                    ddlsubgroup.SelectedValue = ds.Tables["ledgerinfo"].Rows[0].ItemArray[4].ToString();
                        for (int i = 0; i < ddlsubgroup.Items.Count; i++)
                        {
                            if (ddlsubgroup.Items[i].Text != "CONSUMABLES")
                            {
                                ddlsubgroup.Items[i].Attributes.Add("disabled", "disabled");
                            }
                        }
                        for (int i = 0; i < ddlitcode.Items.Count; i++)
                        {
                            if (ddlitcode.Items[i].Value == "SGST" || ddlitcode.Items[i].Value == "IGST" || ddlitcode.Items[i].Value == "CGST")
                            {
                               // ddlitcode.Items[i].Attributes.Add("disabled", "disabled");
                            }
                            else
                            {
                                ddlitcode.Items[i].Attributes.Add("disabled", "disabled");
                            }
                        }
                }
             }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
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
            da = new SqlDataAdapter("Select CONVERT(varchar(10), id)as id,Name from Sub_Group where status='3'union all select Group_id as id,group_name as Name from mastergroups where status='3'", con);
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
                if (rbtninvoicetype.SelectedIndex == 0)
                    cmd.Parameters.AddWithValue("@Code", ddlvendorname.SelectedValue);
                else
                    cmd.Parameters.AddWithValue("@Code", ddlclient.SelectedValue);
            }
            if (rbtntype.SelectedIndex == 0 || rbtntype.SelectedIndex == 1)
            {
                cmd.Parameters.AddWithValue("@Code", ddlitcode.SelectedValue);
            }
            if (rbtntype.SelectedIndex == 3)
            {
                cmd.Parameters.AddWithValue("@Code", ddlitcode.SelectedValue + "," + ddlgstnos.SelectedValue);
            }
            if (rbtntype.SelectedIndex == 4)
            {
                if (rbtnledgers.SelectedIndex == 1)
                    cmd.Parameters.AddWithValue("@Code", lblbankid.Text);
                else if (rbtnledgers.SelectedIndex == 2)
                    cmd.Parameters.AddWithValue("@Code", lbltermloan.Text);
                else if (rbtnledgers.SelectedIndex == 3)
                    cmd.Parameters.AddWithValue("@InvoiceType", ddltype.SelectedItem.Text);
            }
            cmd.Parameters.AddWithValue("@BalanceDate", txtbaldate.Text);
            cmd.Parameters.AddWithValue("@Amount", txtopeningbal.Text);
            cmd.Parameters.AddWithValue("@PaymentType", rbtnpaymenttype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Id", gvledgers.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "Ledger Inserted1";
            con.Close();
            if (msg == "Verified Successfully" || msg == "Approved Successfully")
            {

                JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                reset();
                tblledger.Visible = false;
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
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
    public void reset()
    {
        rbtntype.ClearSelection();
        rbtninvoicetype.ClearSelection();
        txtledgername.Text = "";
        ddlsubgroup.SelectedIndex = 0;
        ddlitcode.SelectedIndex = 0;
        CascadingDropDown3.SelectedValue = "Select Vendor Type";
        CascadingDropDown4.SelectedValue = "Select Vendor Name";
        CascadingDropDown5.SelectedValue = "Select Client Name";
        txtopeningbal.Text = "";
        rbtnpaymenttype.ClearSelection();
        rbtnledgers.ClearSelection();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        reset();
        tblledger.Visible = false;
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
}