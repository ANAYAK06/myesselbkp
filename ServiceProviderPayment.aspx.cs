﻿using System;
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

public partial class ServiceProviderPayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (!IsPostBack)
        {
            trselection.Visible = false;
            trgrid.Visible = false;
            trname.Visible = false;
            trpaymentdetails.Visible = false;
            tblbtn.Visible = false;
            ddlcheque.Visible = false;
            txtcheque.Visible = false;
            service();
        }

    }
    public void LoadYear()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    private void service()
    {
        ddlservice.Items.Clear();
        ddlservice.Visible = true;
        ddlservice.Items.Add("-Select Service Type-");
        ddlservice.Items.Add("Invoice Payment");
        ddlservice.Items.Add("Advance Payment");
        ddlservice.Items.Add("Retention");
        ddlservice.Items.Add("Hold");

    }
    protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlservice.SelectedItem.Text != "-Select Service Type-")
        {
            laodvendor(ddlservice.SelectedValue);
            vename.Visible = true;
            ddlvendor.Visible = true;
            trselection.Visible = false;
            ddlmonth.Visible = false;
            ddlyear.Visible = false;
            ddlpo.Visible = true;
            Button1.Visible = true;
            trgrid.Visible = false;
            trname.Visible = false;
            trpaymentdetails.Visible = false;
            tblbtn.Visible = false;
        }

        else
        {
            //laodvendor(ddlservice.SelectedValue);
            trselection.Visible = false;
            trgrid.Visible = false;
            trname.Visible = false;
            trpaymentdetails.Visible = false;
            tblbtn.Visible = false;
            trgrid.Visible = false;
            trname.Visible = false;
            trpaymentdetails.Visible = false;

        }
    }
    private void laodvendor(string type)
    {
        try
        {
            if (type == "Invoice Payment" || type == "Advance Payment")
            {
                da = new SqlDataAdapter("Select distinct pi.vendor_id,vendor_name+' ('+pi.vendor_id+')' Name from vendor v join pending_invoice pi on pi.vendor_id=v.vendor_id where vendor_type='Service Provider' and v.status='2' and pi.status  in ('3','4') and Balance !=0 order by name asc", con);
            }
            else if (type == "Retention")
            {
                da = new SqlDataAdapter("Select distinct pi.vendor_id,vendor_name+' ('+pi.vendor_id+')' Name from vendor v join pending_invoice pi on pi.vendor_id=v.vendor_id where vendor_type='Service Provider' and v.status='2' and pi.status  in ('3','4') and retention_balance !=0 order by name asc", con);
            }
            else if (type == "Hold")
            {
                da = new SqlDataAdapter("Select distinct pi.vendor_id,vendor_name+' ('+pi.vendor_id+')' Name from vendor v join pending_invoice pi on pi.vendor_id=v.vendor_id where vendor_type='Service Provider' and v.status='2' and pi.status  in ('3','4') and Hold_Balance !=0 order by name asc", con);
            }
            da.Fill(ds, "vendor");
            ddlvendor.DataTextField = "Name";
            ddlvendor.DataValueField = "vendor_id";
            ddlvendor.DataSource = ds.Tables["vendor"];
            ddlvendor.DataBind();
            ddlvendor.Items.Insert(0, "Select Vendor");
            //ddlvendor.Items.Insert(1, "Select All");

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvendor.SelectedIndex != 0)
        {
            ddlpo.Visible = true;
            Button1.Visible = true;
            trname.Visible = true;
            trselection.Visible = true;
            loadpo(ddlvendor.SelectedValue);
            da = new SqlDataAdapter("Select vendor_name from vendor where vendor_id='" + ddlvendor.SelectedValue + "'", con);
            da.Fill(ds, "name");
            if (ds.Tables["name"].Rows.Count > 0)
                txtname.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
        }
        else
        {
            ddlpo.Visible = false;
            Button1.Visible = false;
            trname.Visible = false;
            trselection.Visible = false;
        }
    }
    private void loadpo(string q)
    {

        //if (ddlservice.SelectedItem.Text == "Invoice")
        if (ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono where pi.balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Supplier','VAT') and pi.status in('3','Debit Pending')", con);
        else if (ddlservice.SelectedItem.Text == "TDS")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.tds_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax')", con);
        else if (ddlservice.SelectedItem.Text == "Retention")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.Retention_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax')", con);
        else if (ddlservice.SelectedItem.Text == "Hold")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.Hold_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax')", con);
        da.Fill(ds, "po");
        ddlpo.DataTextField = "po_no";
        ddlpo.DataValueField = "po_no";
        ddlpo.DataSource = ds.Tables["po"];
        ddlpo.DataBind();
        ddlpo.Items.Insert(0, "Select PO");
        ddlpo.Items.Insert(1, "Select All");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
        trgrid.Visible = true;
        txtamt.Text = "";

    }
    private void fillgrid()
    {
        if ((ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment") && ddlpo.SelectedItem.Text != "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where po_no='" + ddlpo.SelectedValue + "' and balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and SubType in ('Service Provider') and status in ('3', 'Debit Pending') order by invoice_date asc", con);
        else if ((ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment") && ddlpo.SelectedItem.Text == "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and SubType in ('Service Provider') and status in ('3', 'Debit Pending') order by invoice_date asc", con);
        else if (ddlservice.SelectedItem.Text == "Retention" && ddlpo.SelectedItem.Text != "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where paymenttype in ('Service Provider') and retention_balance!=0 and retention_balance is NOT NULL and po_no='" + ddlpo.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "' order by invoice_date asc", con);
        else if (ddlservice.SelectedItem.Text == "Retention" && ddlpo.SelectedItem.Text == "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where paymenttype in ('Service Provider') and retention_balance!=0 and retention_balance is NOT NULL and vendor_id='" + ddlvendor.SelectedValue + "' order by invoice_date asc", con);
        else if (ddlservice.SelectedItem.Text == "Hold" && ddlpo.SelectedItem.Text != "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where paymenttype in ('Service Provider') and hold_balance!=0 and hold_balance is NOT NULL and po_no='" + ddlpo.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "' order by invoice_date asc", con);
        else if (ddlservice.SelectedItem.Text == "Hold" && ddlpo.SelectedItem.Text == "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where paymenttype in ('Service Provider') and hold_balance!=0 and hold_balance is NOT NULL  and vendor_id='" + ddlvendor.SelectedValue + "' order by invoice_date asc", con);
        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {
            grd.DataSource = ds.Tables["fill"];
            grd.DataBind();
            trpaymentdetails.Visible = true;
            tblbtn.Visible = true;
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
            grd.EmptyDataText = "No Data Avaliable";
            trpaymentdetails.Visible = false;
            tblbtn.Visible = false;
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlservice.SelectedValue == "Retention")
            {
                e.Row.Cells[7].Visible = true;
            }
            else { e.Row.Cells[7].Visible = false; }
            if (ddlservice.SelectedValue == "Hold")
            {
                e.Row.Cells[8].Visible = true;
            }
            else { e.Row.Cells[8].Visible = false; }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Basic += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BasicValue"));
            Retention += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Retention"));
            Hold += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hold"));
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
            Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
            if (ddlservice.SelectedValue == "Retention")
            {
                e.Row.Cells[7].Visible = true;
            }
            else { e.Row.Cells[7].Visible = false; }
            if (ddlservice.SelectedValue == "Hold")
            {
                e.Row.Cells[8].Visible = true;
            }
            else { e.Row.Cells[8].Visible = false; }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = String.Format("{0:#,##,##,###.00}", Basic);
            e.Row.Cells[7].Text = String.Format("{0:#,##,##,###.00}", Retention);
            e.Row.Cells[8].Text = String.Format("{0:#,##,##,###.00}", Hold);
            e.Row.Cells[9].Text = String.Format("{0:#,##,##,###.00}", NetAmount);
            e.Row.Cells[10].Text = String.Format("{0:#,##,##,###.00}", Balance);
            if (ddlservice.SelectedValue == "Retention")
            {
                e.Row.Cells[7].Visible = true;
            }
            else { e.Row.Cells[7].Visible = false; }
            if (ddlservice.SelectedValue == "Hold")
            {
                e.Row.Cells[8].Visible = true;
            }
            else { e.Row.Cells[8].Visible = false; }

        }
    }
    private decimal Basic = (decimal)0.0;
    private decimal Retention = (decimal)0.0;
    private decimal Hold = (decimal)0.0;
    private decimal NetAmount = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rec in grd.Rows)
        {
            CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");
            CheckBox chkAll = (CheckBox)grd.HeaderRow.FindControl("chkAll");
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        BankDateCheck objdate = new BankDateCheck();
        if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedItem.Text))
        {
           
            btnsubmit.Visible = false;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            //string category = TypeOfPayment;
            try
            {
                string invoicenos = "";
                foreach (GridViewRow rec in grd.Rows)
                {
                    if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                        invoicenos = invoicenos + grd.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                }
                if (invoicenos != "")
                {
                    //if (ddlservice.SelectedItem.Text == "TDS")
                    //{

                    //    da = new SqlDataAdapter("sp_TDSandRetention_payment", con);
                    //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //    da.SelectCommand.Parameters.AddWithValue("@PaymentType", TypeOfPayment);

                    //}
                    if (ddlservice.SelectedItem.Text == "Retention")
                    {

                        da = new SqlDataAdapter("sp_VendorRetention_payment", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Service Provider");

                    }
                    else if (ddlservice.SelectedItem.Text == "Hold")
                    {
                        da = new SqlDataAdapter("sp_VendorHold_payment", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Service Provider");


                    }
                    else if (ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment")
                    {
                        da = new SqlDataAdapter("sp_Debit_Pending_New", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Service Provider");
                    }

                }

                da.SelectCommand.Parameters.AddWithValue("@invoicenos", invoicenos);
                //da.SelectCommand.Parameters.AddWithValue("@CCCode", lblcc.Text);
                //da.SelectCommand.Parameters.AddWithValue("@DCA_Code", lbldca.Text);
                //da.SelectCommand.Parameters.AddWithValue("@Sub_DCA", lblsubdca.Text);
                da.SelectCommand.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                da.SelectCommand.Parameters.AddWithValue("@Name", txtname.Text);
                da.SelectCommand.Parameters.AddWithValue("@Description", txtdesc.Text);
                da.SelectCommand.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                if (ddlpayment.SelectedItem.Text == "Cheque")
                {
                    da.SelectCommand.Parameters.AddWithValue("@No", ddlcheque.SelectedItem.Text);
                    da.SelectCommand.Parameters.AddWithValue("@chequeid", ddlcheque.SelectedValue);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@No", txtcheque.Text);
                }
                da.SelectCommand.Parameters.AddWithValue("@User", Session["user"].ToString());
                da.SelectCommand.Parameters.AddWithValue("@Amount", txtamt.Text);
                da.SelectCommand.Parameters.AddWithValue("@date", txtdate.Text);
                da.Fill(ds, "InvoiceTranNo");
                if (ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[0].ToString() == "Sucessfull")
                {
                    showalertprint("Sucessfull", ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[1].ToString());
                    fillgrid();
                    Clear();
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
        else
        {
            JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
        }
    }

    public void showalertprint(string message, string id)
    {
        string script = @"window.alert('" + message + "');window.location ='serviceproviderpayment.aspx';window.open('Bankpaymentprint.aspx?id=" + id + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert", script, true);

    }
    public void Clear()
    {
        txtdate.Text = "";
        txtdesc.Text = "";
        txtamt.Text = "";
        txtcheque.Text = "";
        txtcheque.Visible = true;
        ddlcheque.Visible = false;
        CascadingDropDown9.SelectedValue = "";
        CascadingDropDown1.SelectedValue = "";
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment")
        {
            if (grd.SelectedIndex != -1)
            {
                string invoiceno = grd.SelectedDataKey.Values["InvoiceNo"].ToString();
                string pono = grd.SelectedDataKey.Values["po_no"].ToString();
                fillpop(invoiceno, pono);
            }
        }
    }
    public void fillpop(string inv, string pono)
    {
       
        //da = new SqlDataAdapter("select ISNULL(SUM(Balance),0)as amt from Vendor_Taxes where type !='Deduction' and InvoiceNo ='" + inv + "' and Po_No='" + pono + "'", con);
        //da = new SqlDataAdapter("select ISNULL(SUM(Balance),0)as amt from Vendor_Taxes where  InvoiceNo ='" + inv + "' and Po_No='" + pono + "'", con);
        //da.Fill(ds, "Amt");
        //ViewState["Amt"] = ds.Tables["Amt"].Rows[0].ItemArray[0].ToString();
        da = new SqlDataAdapter("select id,InvoiceNo,CC_Code,Dca_Code,Subdca_Code,RegdNo,Type,Amount,Balance,Tax_Type from Vendor_Taxes where status is null and InvoiceNo ='" + inv + "' and Po_No='" + pono + "' order by CASE WHEN [Type]='Deduction' THEN [Type] END,CASE WHEN [Type]='Other'  THEN [Type] END,CASE WHEN [Type]='VAT Tax' THEN [Type] END,CASE WHEN [Type]='Excise Tax'  THEN [Type] END", con);
        da.Fill(ds, "checkpo");
        if (ds.Tables["checkpo"].Rows.Count > 0)
        {
            Grdviewpopup.DataSource = ds.Tables["checkpo"];
            Grdviewpopup.DataBind();
            //if (ViewState["Amt"] != null)
            //{
            //    if (Convert.ToDecimal(ViewState["Amt"].ToString()) >= 1)
            //    {
                    //ViewState["Amt"] = null;
                    popview.Show();
            //    }
            //}

        }
        else
        {
            Grdviewpopup.DataSource = null;
            Grdviewpopup.DataBind();
            popview.Hide();
        }
    }
    private decimal Amt = (decimal)0.0;
    protected void Grdviewpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            da = new SqlDataAdapter("select mapdca_code from dca where dca_code='" + Grdviewpopup.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' UNION ALL select mapsubdca_code FROM subdca WHERE subdca_code = '" + Grdviewpopup.DataKeys[e.Row.RowIndex].Values[1].ToString() + "'", con);
            da.Fill(ds1, "chkmapdca");
            e.Row.Cells[3].Text = ds1.Tables["chkmapdca"].Rows[0].ItemArray[0].ToString();
            e.Row.Cells[4].Text = ds1.Tables["chkmapdca"].Rows[1].ItemArray[0].ToString();
            if (e.Row.Cells[6].Text == "Creditable" || e.Row.Cells[6].Text == "Non-Creditable")
            {

            }
            else
            {
                e.Row.Cells[6].Text = "";
            }
            if (e.Row.Cells[7].Text == "Deduction")
            {
                e.Row.Cells[8].BackColor = Color.Red;
                e.Row.Cells[9].BackColor = Color.Red;
                Balance -= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
                Amt -= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            else
            {
                Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
                Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }


        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Basic);
            //e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", NetAmount);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Balance);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Amt);

        }
        ds1.Clear();
    }
}