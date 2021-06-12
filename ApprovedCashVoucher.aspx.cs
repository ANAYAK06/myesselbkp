using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;
using System.Data;
using System.Xml.Linq;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Text;
using System.Collections;

public partial class ApprovedCashVoucher : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Default.aspx");

        if (Session["user"] == null)
        {
            Response.Redirect("~/common/login.aspx");
        }
        if (!IsPostBack)
        {
          

            if (Request.QueryString["Voucherid"] == null)
            {
                JavaScript.CloseWindow();
            }

            else
            {
           
                //ViewState["voucherid"] = Request.QueryString["Voucherid"].ToString().Remove(Request.QueryString["Voucherid"].ToString().LastIndexOf(","));
                //ViewState["condition"] = Request.QueryString["Voucherid"].ToString().Substring(Request.QueryString["Voucherid"].ToString().LastIndexOf(",") + 1);
                LoadCC();
                CashVouchers();
                ViewState["check"] = Request.QueryString[1].ToString();
            }
        }
    }

   

    public void CashVouchers()
    {
        try
        {


            da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,particulars,name,cc_code,paid_against,dca_code,sub_dca,debit,transaction_type,vendor_id,REPLACE(CONVERT(VARCHAR(11),convert(datetime,modifiedby_date, 9),106), ' ', '-')as paiddate,paymenttype  from cash_pending where  id='" + Request.QueryString["Voucherid"].ToString() + "'", con);
            da.Fill(ds, "voucherinfo");

            if (ds.Tables["voucherinfo"].Rows.Count > 0)
            {


                txtdate.Text = ds.Tables["voucherinfo"].Rows[0][0].ToString();
                txtcashdesc.Text = ds.Tables["voucherinfo"].Rows[0][1].ToString();
                txtcashname.Text = ds.Tables["voucherinfo"].Rows[0][2].ToString();
                txtcashcc.Text = ds.Tables["voucherinfo"].Rows[0][3].ToString();
                ddlcashcc.SelectedItem.Text = ds.Tables["voucherinfo"].Rows[0][4].ToString();
                casccashdca.SelectedValue = ds.Tables["voucherinfo"].Rows[0][5].ToString();
                casccashsub.SelectedValue = ds.Tables["voucherinfo"].Rows[0][6].ToString();
                txtcashdebit.Text = ds.Tables["voucherinfo"].Rows[0][7].ToString();
                txttransaction.Text = ds.Tables["voucherinfo"].Rows[0][8].ToString();
                ViewState["Vendor"] =ds.Tables["voucherinfo"].Rows[0][9].ToString();
                if (ds.Tables["voucherinfo"].Rows[0][8].ToString() == "Debit")
                {
                    ddlcashcc.Visible = false;
                    lblpaidagainst.Visible = false;
                    Chkcc1.Visible = false;
                    
                }
                else
                {
                    ddlcashcc.Visible = true;
                    Chkcc1.Visible = true;
                    lblpaidagainst.Visible = true;
                }
                txtpaiddate.Text = ds.Tables["voucherinfo"].Rows[0][10].ToString();
                if (ds.Tables["voucherinfo"].Rows[0][9].ToString() != "")
                {
                    paymenttype();
                    laodvendor(ds.Tables["voucherinfo"].Rows[0][3].ToString(), ds.Tables["voucherinfo"].Rows[0][4].ToString());
                    ddlvendor.SelectedItem.Text = ds.Tables["voucherinfo"].Rows[0][9].ToString();
                    ddltype.SelectedValue = ds.Tables["voucherinfo"].Rows[0][11].ToString();
                    tblvendor.Visible = true;
                    tblpo.Visible = true;
                    txtpaiddate.Visible = false;
                    lblpaiddate.Visible = false;
                    CheckBox2.Visible = false;
                    grd.Visible = true;
                    CheckBox1.Visible = false;
                    fillpo(ds.Tables["voucherinfo"].Rows[0][9].ToString(), ds.Tables["voucherinfo"].Rows[0][11].ToString());
                }
                else
                {
                    txtpaiddate.Visible = true;
                    lblpaiddate.Visible = true;
                    CheckBox2.Visible = true;
                    tblvendor.Visible = false;
                    tblpo.Visible = false;
                    grd.Visible = false;
                    CheckBox1.Visible = true;

                }
            }
            else
            {
                showalert(Page, "This Voucher is already approved");

            }
            //return ds.Tables["voucherInfo"].Rows;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        fillpop();

    }
   
    public void fillpop()
    {
        if (ddltype.SelectedItem.Text == "Invoice")
            da = new SqlDataAdapter("select invoiceno,total,NetAmount,balance,REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date from pending_invoice where po_no='" + ddlvenpo1.SelectedValue + "' and convert(datetime,invoice_date)<='" + txtdate.Text + "' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and status not in('1','2','2A') and balance!='0' ", con);
        else if (ddltype.SelectedItem.Text == "Retention")
            da = new SqlDataAdapter("select invoiceno,total,NetAmount,retention_balance as [balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date from pending_invoice where po_no='" + ddlvenpo1.SelectedValue + "' and convert(datetime,invoice_date)<='" + txtdate.Text + "' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and Retention_balance!='0' ", con);
        else if (ddltype.SelectedItem.Text == "Hold")
            da = new SqlDataAdapter("select invoiceno,total,NetAmount,hold_balance as [balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date from pending_invoice where po_no='" + ddlvenpo1.SelectedValue + "' and convert(datetime,invoice_date)<='" + txtdate.Text + "' and vendor_id='" + ddlvendor.SelectedItem.Text + "'  and hold_balance!='0' ", con);

        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {

            grd.DataSource = ds.Tables["fill"];
            grd.DataBind();
            btncashApprove.Visible = true;
        }
        else
        {
            btncashApprove.Visible = false;
            grd.DataSource = null;
            grd.DataBind();
            JavaScript.UPAlert(Page, "There is no Payables");
        }


    }

    public void showalert(Page page, string Message)
    {
        string script = @"alert('" + Message + "');window.close();window.opener.location.href ='SrAccountantInbox.Aspx?Voucherid=" + Request.QueryString[0] + "&CCCode=" + Request.QueryString[1] + "&DCACode=" + Request.QueryString[2] + "&SDCACode=" + Request.QueryString[3] + "&Date=" + Request.QueryString[4] + "&Month=" + Request.QueryString[5] + "&Year=" + Request.QueryString[6] + "';";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Alert", script, true);
    }
    public void showalert1(Page page, string Message)
    {
        string script = @"alert('" + Message + "');window.close();window.opener.location.href ='SrAccountantInbox.Aspx';";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Alert", script, true);
    }
    protected void btncashApprove_Click1(object sender, EventArgs e)
    {
        try
        {


            if (Session["user"] != null)
            {

                //cascvendor.SelectedValue = "";
                if (ViewState["Vendor"].ToString() != "")
                {
                    grd.Visible = true;
                    string invoicenos = "";

                    foreach (GridViewRow rec in grd.Rows)
                    {
                        if ((rec.FindControl("chkSelect") as CheckBox).Checked)
                        {
                            invoicenos = invoicenos + grd.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";

                        }
                    }
                    if (invoicenos != "")
                    {
                        cmd = new SqlCommand("sp_vendorpayment_bycash", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@invoicenos", invoicenos);
                        cmd.Parameters.AddWithValue("@PO_NO", ddlvenpo1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@PaidDate", txtdate.Text);
                        cmd.Parameters.AddWithValue("@type", ddltype.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@TransactionType", txttransaction.Text);
                        cmd.Parameters.AddWithValue("@Date", txtdate.Text);

                        cmd.Parameters.AddWithValue("@CC_Code", txtcashcc.Text);
                        cmd.Parameters.AddWithValue("@Amount", txtcashdebit.Text);
                        cmd.Parameters.AddWithValue("@Dca", ddlcashdca.SelectedValue);
                        if (ddlcashsub.SelectedItem.Text != "")
                        {
                            cmd.Parameters.AddWithValue("@SubDca", ddlcashsub.SelectedItem.Text);
                        }
                        cmd.Parameters.AddWithValue("@Name", txtcashname.Text);
                        cmd.Parameters.AddWithValue("@Description", txtcashdesc.Text);
                        if (ddlvendor.SelectedItem.Text != "Select Vendor")
                        {
                            cmd.Parameters.AddWithValue("@Vendorid", ddlvendor.SelectedValue);
                        }
                        if (txttransaction.Text == "Paid Against")
                        {
                            cmd.Parameters.AddWithValue("@CC_Code1", ddlcashcc.SelectedItem.Text);
                        }
                        cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["Voucherid"].ToString());

                        con.Open();
                        string msg = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if (Request.QueryString[1].ToString() == "" && Request.QueryString[2].ToString() == "" && Request.QueryString[3].ToString() == "" && Request.QueryString[4].ToString() == "Select Date" && Request.QueryString[5].ToString() == "0" && Request.QueryString[6].ToString() == "Any Year")
                        {
                            showalert1(Page, msg);
                        }
                        else
                        {
                            showalert(Page, msg);
                        }
                        //if (msg == "Cash Flow Completed SuccessFully")
                        //{
                        //    showalert(Page, msg);
                        //    //Response.Redirect("SrAccountantInbox.Aspx?" + "Value=" + ViewState["condition"].ToString()); 
                            

                        //}
                        //else
                        //{
                        //    showalert1(Page, msg);
                        //}
                    }


                }
                else
                {
                    grd.Visible = false;
                    cmd = new SqlCommand("sp_CashFlow_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PaidDate", txtpaiddate.Text);
                    cmd.Parameters.AddWithValue("@TransactionType", txttransaction.Text);
                    cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                    cmd.Parameters.AddWithValue("@CC_Code", txtcashcc.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtcashdebit.Text);
                    cmd.Parameters.AddWithValue("@Dca", ddlcashdca.SelectedValue);
                    if (ddlcashsub.SelectedItem.Text != "")
                    {
                        cmd.Parameters.AddWithValue("@SubDca", ddlcashsub.SelectedItem.Text);
                    }
                    cmd.Parameters.AddWithValue("@Name", txtcashname.Text);
                    cmd.Parameters.AddWithValue("@Description", txtcashdesc.Text);
                    if (txttransaction.Text == "Paid Against")
                    {
                        cmd.Parameters.AddWithValue("@CC_Code1", ddlcashcc.SelectedItem.Text);
                    }
                    cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["Voucherid"].ToString());
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if (Request.QueryString[1].ToString() == "" && Request.QueryString[2].ToString() == "" && Request.QueryString[3].ToString() == "" && Request.QueryString[4].ToString() == "Select Date" && Request.QueryString[5].ToString() == "0" && Request.QueryString[6].ToString() == "Any Year")
                    {
                        showalert1(Page, msg);
                    }
                    else
                    {
                        showalert(Page, msg);
                    }
                    ////if (msg == "Credited SuccessFully" || msg == "Approved SuccessFully")
                    ////{
                    ////    showalert(Page, msg);
                    ////    //Response.Redirect("SrAccountantInbox.Aspx?" + "Value=" + ViewState["condition"].ToString());
                       

                    ////}
                    ////else
                    ////{
                    ////    showalert1(Page, msg);
                    ////}
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
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
   
   
   
    public void fillpo(string vendor, string paymenttype)
    {
        ddlvenpo1.Items.Clear();
        if (paymenttype.ToString() == "Invoice")
            da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + ddlvendor.SelectedItem.Text+ "' and (cc_code='" + txtcashcc.Text + "' or cc_code='" +ddlcashcc.SelectedItem.Text+ "')", con);
        else if (paymenttype.ToString() == "Retention")
            da = new SqlDataAdapter("select distinct po_no from pending_invoice where Retention_balance!='0' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and (cc_code='" + txtcashcc.Text + "' or cc_code='" + ddlcashcc.SelectedItem.Text + "') ", con);
        else if (paymenttype.ToString() == "Hold")
            da = new SqlDataAdapter("select distinct po_no from pending_invoice where Hold_balance!='0' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and (cc_code='" + txtcashcc.Text + "' or cc_code='" + ddlcashcc.SelectedItem.Text + "') ", con);

        da.Fill(ds, "po");
        if (ds.Tables["po"].Rows.Count > 0)
        {
            ddlvenpo1.DataTextField = "po_no";
            ddlvenpo1.DataValueField = "po_no";
            ddlvenpo1.DataSource = ds.Tables["po"];
            ddlvenpo1.DataBind();
            ddlvenpo1.Items.Insert(0, "Select PO");
        }
        else
            ddlvenpo1.Items.Insert(0, "Select PO");
    }
    public void paymenttype()
    {
        ddltype.Items.Clear();
        ddltype.Items.Add("Select Payment Type");
        ddltype.Items.Add("Invoice");
        ddltype.Items.Add("Retention");
        ddltype.Items.Add("Hold");
    }

    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        grd.DataSource = null;
        grd.DataBind();
        fillpo(ddlvendor.SelectedItem.Text, ddltype.SelectedItem.Text);

    }
    private void laodvendor(string cc_code,string paidagainst)
    {
        da = new SqlDataAdapter("Select distinct vendor_id from pending_invoice where (cc_code='" + txtcashcc.Text + "' or cc_code='" + ddlcashcc.SelectedItem.Text + "')", con);
        da.Fill(ds, "fillvendor");
        ddlvendor.DataTextField = "vendor_id";
        ddlvendor.DataValueField = "vendor_id";
        ddlvendor.DataSource = ds.Tables["fillvendor"];
        ddlvendor.DataBind();
        ddlvendor.Items.Insert(0, "Select Vendor");

    }
    private void LoadCC()
    {
        da = new SqlDataAdapter("Select cc_code from Cost_Center where status in ('Old','New')", con);
        da.Fill(ds, "fillCC");
        ddlcashcc.DataTextField = "cc_code";
        ddlcashcc.DataValueField = "cc_code";
        ddlcashcc.DataSource = ds.Tables["fillCC"];
        ddlcashcc.DataBind();
        ddlcashcc.Items.Insert(0, "Select Cost Center");

    }
    protected void ddlvenpo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpop();
    }
    protected void ddlcashcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        
            laodvendor(txtcashcc.Text, ddlcashcc.SelectedItem.Text);
        
    }
}
