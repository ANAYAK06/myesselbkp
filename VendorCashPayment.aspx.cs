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
using System.Drawing;

public partial class VendorCashPayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tblserviceprovider.Visible = false;
            tblsupplier.Visible = false;
            tblbtn.Visible = false;
            ddlcccode.Visible = false;
            trpaymenttype.Visible = false;
            trdate.Visible = false;
        }
    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        tblserviceprovider.Visible = false;
        tblsupplier.Visible = false;
        tblbtn.Visible = false;
        ddlcccode.Visible = false;
        trpaymenttype.Visible = false;
        trdate.Visible = false;
        ddlvendortype.SelectedIndex = 0;
        if (rbtntype.SelectedValue == "Paid Against")
        {
            ddlcccode.Visible = true;
            trpaymenttype.Visible = true;
            trdate.Visible = true;
            CascadingDropDown4.SelectedValue = "Select Other CC";            
        }
        else if (rbtntype.SelectedValue == "Debit")
        {
            ddlcccode.Visible = false;
            trpaymenttype.Visible = true;
            trdate.Visible = true;
            ViewState["Debitcc"] = Session["cc_code"].ToString();
        }
        else
        {
            ddlcccode.Visible = false;
            tblserviceprovider.Visible = false;
            tblsupplier.Visible = false;
            tblbtn.Visible = false;
            trpaymenttype.Visible = false;
            trdate.Visible = false;
        }
    }
    protected void ddlvendortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvendortype.SelectedItem.Text == "Service Provider")
        {
            tblsupplier.Visible = false;
            if (rbtntype.SelectedValue == "Paid Against")
            {
                if (CascadingDropDown4.SelectedValue != "::::::")
                {
                    ViewState["Debitcc"] = ddlcccode.SelectedValue;
                    tblserviceprovider.Visible = true;
                    service();
                    LoadYear();
                }
                else
                {
                    tblserviceprovider.Visible = false;
                    ddlvendortype.SelectedIndex = 0;
                    JavaScript.UPAlert(Page,"Please Select Other CC Code");
                }
                trselection.Visible = false;
                trgrid.Visible = false;
                trname.Visible = false;
                trpaymentdetails.Visible = false;
                tblbtn.Visible = false;
                trgrid.Visible = false;
                trname.Visible = false;
                trpaymentdetails.Visible = false;
            }
            else if (rbtntype.SelectedValue == "Debit")
            {
                tblserviceprovider.Visible = true;
                service();
                LoadYear();
            }
            else
            {
                tblserviceprovider.Visible = false;
            }
        }
        else if (ddlvendortype.SelectedItem.Text == "Supplier")
        {
            tblserviceprovider.Visible = false;
            if (rbtntype.SelectedValue == "Paid Against")
            {
                if (CascadingDropDown4.SelectedValue != "::::::")
                {
                    ViewState["Debitcc"] = ddlcccode.SelectedValue;
                    tblsupplier.Visible = true;
                    LoadYears();
                    services();
                }
                else
                {
                    tblsupplier.Visible = false;
                    ddlvendortype.SelectedIndex = 0;
                    JavaScript.UPAlert(Page, "Please Select Other CC Code");
                }
                trselections.Visible = false;
                trgrids.Visible = false;
                trnames.Visible = false;
                trpaymentdetailss.Visible = false;
                tblbtn.Visible = false;               
            }
            else if (rbtntype.SelectedValue == "Debit")
            {
                tblsupplier.Visible = true;
                LoadYears();
                services();
            }
            else
            {
                tblsupplier.Visible = false;
            }
        }
        
    }
    #region service provider payment code starts
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
            //laodvendor();
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
            //da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='Service Provider' and status='2' order by name asc", con);
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
            trpaymentdetails.Visible = true;
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
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono where pi.balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Supplier','VAT') and pi.status in('3','Debit Pending')  and pi.cc_code='" + ViewState["Debitcc"].ToString() + "' ", con);
        else if (ddlservice.SelectedItem.Text == "TDS")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.tds_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax') and pi.cc_code='" + ViewState["Debitcc"].ToString() + "'", con);
        else if (ddlservice.SelectedItem.Text == "Retention")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.Retention_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax')  and pi.cc_code='" + ViewState["Debitcc"].ToString() + "'", con);
        else if (ddlservice.SelectedItem.Text == "Hold")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.Hold_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax') and pi.cc_code='" + ViewState["Debitcc"].ToString() + "'", con);
        da.Fill(ds, "po");
        ddlpo.DataTextField = "po_no";
        ddlpo.DataValueField = "po_no";
        ddlpo.DataSource = ds.Tables["po"];
        ddlpo.DataBind();
        ddlpo.Items.Insert(0, "Select PO");
        //ddlpo.Items.Insert(1, "Select All");
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
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where po_no='" + ddlpo.SelectedValue + "' and balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and SubType in ('Service Provider') and status in ('3', 'Debit Pending') order by invoice_date desc", con);
        else if ((ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment") && ddlpo.SelectedItem.Text == "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and SubType in ('Service Provider') and status in ('3', 'Debit Pending') order by invoice_date desc", con);
        else if (ddlservice.SelectedItem.Text == "Retention" && ddlpo.SelectedItem.Text != "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where paymenttype in ('Service Provider') and retention_balance!=0 and retention_balance is NOT NULL and po_no='" + ddlpo.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "' and SubType in ('Service Provider') and status in ('3','4', 'Debit Pending') order by invoice_date desc", con);
        else if (ddlservice.SelectedItem.Text == "Retention" && ddlpo.SelectedItem.Text == "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where paymenttype in ('Service Provider') and retention_balance!=0 and retention_balance is NOT NULL and vendor_id='" + ddlvendor.SelectedValue + "' and SubType in ('Service Provider') and status in ('3','4', 'Debit Pending') order by invoice_date desc", con);
        else if (ddlservice.SelectedItem.Text == "Hold" && ddlpo.SelectedItem.Text != "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where paymenttype in ('Service Provider') and hold_balance!=0 and hold_balance is NOT NULL and po_no='" + ddlpo.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "' and SubType in ('Service Provider') and status in ('3','4', 'Debit Pending') order by invoice_date desc", con);
        else if (ddlservice.SelectedItem.Text == "Hold" && ddlpo.SelectedItem.Text == "Select All")
            da = new SqlDataAdapter("select InvoiceNo,cc_code,DCA_Code,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount,0)as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status,isnull(Retention_balance,0)as[Retention],isnull(Hold_balance, 0) as[Hold],po_no from pending_invoice where paymenttype in ('Service Provider') and hold_balance!=0 and hold_balance is NOT NULL  and vendor_id='" + ddlvendor.SelectedValue + "' and SubType in ('Service Provider') and status in ('3','4', 'Debit Pending') order by invoice_date desc", con);
        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {
            grd.DataSource = ds.Tables["fill"];
            grd.DataBind();
            tblbtn.Visible = true;
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
            grd.EmptyDataText = "No Data Avaliable";            
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

        da = new SqlDataAdapter("select id,InvoiceNo,CC_Code,Dca_Code,Subdca_Code,RegdNo,Type,Amount,Balance,Tax_Type from Vendor_Taxes where status is null and InvoiceNo ='" + inv + "' and Po_No='" + pono + "' order by CASE WHEN [Type]='Deduction' THEN [Type] END,CASE WHEN [Type]='Other'  THEN [Type] END,CASE WHEN [Type]='VAT Tax' THEN [Type] END,CASE WHEN [Type]='Excise Tax'  THEN [Type] END", con);
        da.Fill(ds, "checkpo");
        if (ds.Tables["checkpo"].Rows.Count > 0)
        {
            Grdviewpopup.DataSource = ds.Tables["checkpo"];
            Grdviewpopup.DataBind();           
            popview.Show();
            
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
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Balance);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Amt);
        }
        ds1.Clear();
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rec in grd.Rows)
        {
            CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");
            CheckBox chkAll = (CheckBox)grd.HeaderRow.FindControl("chkAll");
        }
    }

    public void clear()
    {
        CascadingDropDown4.SelectedValue = "";
        txtdt.Text = "";
        txtpaiddate.Text = "";
        if (ddlvendortype.SelectedItem != null)
        ddlvendortype.SelectedIndex = 0;
        if (ddlservice.SelectedItem != null)
        ddlservice.SelectedIndex = 0;
        if (ddlvendor.SelectedItem != null)
        ddlvendor.SelectedIndex = 0;
        if (ddlmonth.SelectedItem != null)
        ddlmonth.SelectedIndex = 0;
        if (ddlyear.SelectedItem != null)
        ddlyear.SelectedIndex = 0;
        if (ddlpo.SelectedItem != null)
        ddlpo.SelectedIndex = 0;
        grd.DataSource = null;
        grd.DataBind();
        txtname.Text = "";
        txtdesc.Text = "";
        txtamt.Text = "";

        if (ddlservices.SelectedItem != null)
            ddlservices.SelectedIndex = 0;
        if (ddlvendors.SelectedItem != null)
            ddlvendors.SelectedIndex = 0;
        if (ddlmonths.SelectedItem != null)
            ddlmonths.SelectedIndex = 0;
        if (ddlyears.SelectedItem != null)
            ddlyears.SelectedIndex = 0;
        if (ddlpos.SelectedItem != null)
            ddlpos.SelectedIndex = 0;
        grds.DataSource = null;
        grds.DataBind();
        txtnames.Text = "";
        txtdescs.Text = "";
        txtamts.Text = "";
    }
    #endregion service provider payment code ends

    #region Supplier payment code STARTS

    private void services()
    {
        ddlservices.Items.Clear();
        ddlservices.Visible = true;
        ddlservices.Items.Add("-Select Service Type-");
        ddlservices.Items.Add("Invoice Payment");
        ddlservices.Items.Add("Advance Payment");

    }
    protected void ddlservices_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlservices.SelectedItem.Text != "-Select Service Type-")
        {
            laodvendors();
            venames.Visible = true;
            ddlvendors.Visible = true;
            trselections.Visible = false;
            ddlmonths.Visible = false;
            ddlyears.Visible = false;
            ddlpos.Visible = true;
            Button1s.Visible = true;
            trgrids.Visible = false;
            trnames.Visible = false;
            trpaymentdetailss.Visible = false;
            tblbtn.Visible = false;
        }

        else
        {
            laodvendors();
            trselections.Visible = false;
            trgrids.Visible = false;
            trnames.Visible = false;
            trpaymentdetailss.Visible = false;
            tblbtn.Visible = false;
            trgrids.Visible = false;
            trnames.Visible = false;
            trpaymentdetailss.Visible = false;

        }
    }
    private void laodvendors()
    {
        try
        {
            da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='Supplier' and status='2' order by name asc", con);
            da.Fill(ds, "vendor");
            ddlvendors.DataTextField = "Name";
            ddlvendors.DataValueField = "vendor_id";
            ddlvendors.DataSource = ds.Tables["vendor"];
            ddlvendors.DataBind();
            ddlvendors.Items.Insert(0, "Select Vendor");
            //ddlvendor.Items.Insert(1, "Select All");

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void ddlvendors_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvendors.SelectedIndex != 0)
        {
            ddlpos.Visible = true;
            Button1s.Visible = true;
            trnames.Visible = true;
            trselections.Visible = true;
            loadpos(ddlvendors.SelectedValue);
            da = new SqlDataAdapter("Select vendor_name from vendor where vendor_id='" + ddlvendors.SelectedValue + "'", con);
            da.Fill(ds, "name");
            if (ds.Tables["name"].Rows.Count > 0)
                txtnames.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
        }
        else
        {
            ddlpos.Visible = false;
            Button1s.Visible = false;
            trnames.Visible = false;
            trselections.Visible = false;
        }
    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {       
        tblserviceprovider.Visible = false;
        tblsupplier.Visible = false;
        tblbtn.Visible = false;         
        txtdt.Text = "";
        txtpaiddate.Text = "";
        if (ddlvendortype.SelectedItem != null)
            ddlvendortype.SelectedIndex = 0;
        if (ddlservice.SelectedItem != null)
            ddlservice.SelectedIndex = 0;
        if (ddlvendor.SelectedItem != null)
            ddlvendor.SelectedIndex = 0;
        if (ddlmonth.SelectedItem != null)
            ddlmonth.SelectedIndex = 0;
        if (ddlyear.SelectedItem != null)
            ddlyear.SelectedIndex = 0;
        if (ddlpo.SelectedItem != null)
            ddlpo.SelectedIndex = 0;
        grd.DataSource = null;
        grd.DataBind();
        txtname.Text = "";
        txtdesc.Text = "";
        txtamt.Text = "";

        if (ddlservices.SelectedItem != null)
            ddlservices.SelectedIndex = 0;
        if (ddlvendors.SelectedItem != null)
            ddlvendors.SelectedIndex = 0;
        if (ddlmonths.SelectedItem != null)
            ddlmonths.SelectedIndex = 0;
        if (ddlyears.SelectedItem != null)
            ddlyears.SelectedIndex = 0;
        if (ddlpos.SelectedItem != null)
            ddlpos.SelectedIndex = 0;
        grds.DataSource = null;
        grds.DataBind();
        txtnames.Text = "";
        txtdescs.Text = "";
        txtamts.Text = "";
        
    }
    private void loadpos(string q)
    {
        if (ddlservices.SelectedItem.Text == "Invoice Payment" || ddlservices.SelectedItem.Text == "Advance Payment")
        {
            da = new SqlDataAdapter("(select (case when cp.po_no is null then chp.po_no else cp.po_no end) po_no from (select distinct p.po_no from pending_invoice p left outer join mr_report mr ON p.po_no=mr.po_no where balance!='0' and vendor_id='" + q + "' and paymenttype in('Supplier') AND mr.status='8' and p.cc_code='" + ViewState["Debitcc"].ToString() + "') cp full outer join (select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype in('Supplier') and cc_code='" + ViewState["Debitcc"].ToString() + "') chp on cp.po_no=chp.po_no)", con);
        }

        da.Fill(ds, "po");
        ddlpos.DataTextField = "po_no";
        ddlpos.DataValueField = "po_no";
        ddlpos.DataSource = ds.Tables["po"];
        ddlpos.DataBind();
        ddlpos.Items.Insert(0, "Select PO");
        //ddlpos.Items.Insert(1, "Select All");
    }

    protected void Button1s_Click(object sender, EventArgs e)
    {
        fillgrids();
        trgrids.Visible = true;
        txtamts.Text = "";
    }
    private void fillgrids()
    {
        if (ddlpos.SelectedItem.Text != "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount, 0) as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11), convert(datetime, i.invoice_date, 101), 106), ' ', '-') as invoice_date, i.status, isnull(Retention_balance, 0) as[Retention], isnull(Hold_balance, 0) as[Hold],i.po_no from pending_invoice i where i.po_no = '" + ddlpos.SelectedValue + "' and balance!= 0  and vendor_id = '" + ddlvendors.SelectedValue + "' and i.paymenttype='Supplier'", con);
        }
        else if (ddlpos.SelectedItem.Text == "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount, 0) as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11), convert(datetime, i.invoice_date, 101), 106), ' ', '-') as invoice_date, i.status, isnull(Retention_balance, 0) as[Retention], isnull(Hold_balance, 0) as[Hold],i.po_no from pending_invoice i JOIN mr_report mr ON i.po_no = mr.po_no where balance!= 0  and vendor_id = '" + ddlvendors.SelectedValue + "' AND mr.status = '8' and i.paymenttype='Supplier'", con);
        }
        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {
            grds.DataSource = ds.Tables["fill"];
            grds.DataBind();
            trpaymentdetailss.Visible = true;
            tblbtn.Visible = true;
        }
        else
        {
            grds.DataSource = null;
            grds.DataBind();
            grds.EmptyDataText = "No Data Avaliable";
            trpaymentdetailss.Visible = false;
            tblbtn.Visible = false;
        }
    }

    private decimal Basics = (decimal)0.0;
    private decimal NetAmounts = (decimal)0.0;
    private decimal Balances = (decimal)0.0;

    protected void grds_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Basics += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BasicValue"));
            NetAmounts += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
            Balances += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Basics);
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", NetAmounts);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Balances);

        }
    }
    protected void grds_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grds.SelectedIndex != -1)
        {
            string invoicenos = grds.SelectedDataKey.Values["InvoiceNo"].ToString();
            string ponos = grds.SelectedDataKey.Values["po_no"].ToString();
            fillpops(invoicenos, ponos);
        }
    }
    public void fillpops(string inv, string pono)
    {
        string ints = inv + '%';
        da = new SqlDataAdapter("select id,InvoiceNo,CC_Code,Dca_Code,Subdca_Code,RegdNo,Type,Amount,Balance,Tax_Type from Vendor_Taxes where status is null and InvoiceNo like'" + ints + "' and Po_No='" + pono + "' order by CASE WHEN [Type]='Deduction' THEN [Type] END,CASE WHEN [Type]='Frieght'  THEN [Type] END,CASE WHEN [Type]='Other'  THEN [Type] END,CASE WHEN [Type]='VAT Tax' THEN [Type] END,CASE WHEN [Type]='Excise Tax'  THEN [Type] END", con);
        da.Fill(ds, "checkpos");
        if (ds.Tables["checkpos"].Rows.Count > 0)
        {
            Grdviewpopups.DataSource = ds.Tables["checkpos"];
            Grdviewpopups.DataBind();           
            popviews.Show();
        }
        else
        {
            Grdviewpopups.DataSource = null;
            Grdviewpopups.DataBind();
            popviews.Hide();
        }
    }
    private decimal Amts = (decimal)0.0;
    protected void Grdviewpopups_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            da = new SqlDataAdapter("select mapdca_code from dca where dca_code='" + Grdviewpopups.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' UNION ALL select mapsubdca_code FROM subdca WHERE subdca_code = '" + Grdviewpopups.DataKeys[e.Row.RowIndex].Values[1].ToString() + "'", con);
            da.Fill(ds1, "chkmapdcas");
            e.Row.Cells[3].Text = ds1.Tables["chkmapdcas"].Rows[0].ItemArray[0].ToString();
            e.Row.Cells[4].Text = ds1.Tables["chkmapdcas"].Rows[1].ItemArray[0].ToString();
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
                Balances -= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
                Amts -= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

            }
            else
            {
                Balances += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
                Amts += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }


        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Balances);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Amts);

        }
        ds1.Clear();
    }
    public void LoadYears()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyears.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyears.Items.Insert(0, "Any Year");
    }
    protected void chkAlls_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rec in grd.Rows)
        {
            CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");
            CheckBox chkAll = (CheckBox)grd.HeaderRow.FindControl("chkAlls");
        }
    }
    #endregion supplier payment code ENDS

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        btnsubmit.Visible = false;
        try
        {
            string invoicenos = "";
            if (ddlvendortype.SelectedValue == "Service Provider")
            {
                foreach (GridViewRow rec in grd.Rows)
                {
                    if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                        invoicenos = invoicenos + grd.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                }
                if (ddlservice.SelectedItem.Text == "Retention" || ddlservice.SelectedItem.Text == "Hold") // for service provider retention and hold
                {
                    da = new SqlDataAdapter("sp_CashFlow_vendorRetention_Hold", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;                    
                    //da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Retention");
                }

                if (ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment") // for service provider
                {
                    da = new SqlDataAdapter("sp_CashFlow_Vendor", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Paymentcategory", "Service Provider");
                   
                }
                da.SelectCommand.Parameters.AddWithValue("@Name", txtname.Text);
                da.SelectCommand.Parameters.AddWithValue("@Description", txtdesc.Text);
                da.SelectCommand.Parameters.AddWithValue("@Amount", txtamt.Text);
                da.SelectCommand.Parameters.AddWithValue("@PO_NO", ddlpo.SelectedItem.Text);
            }
            else if (ddlvendortype.SelectedValue == "Supplier") // for supplier
            {
                foreach (GridViewRow rec in grds.Rows)
                {
                    if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                        invoicenos = invoicenos + grds.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                }
                da = new SqlDataAdapter("sp_CashFlow_Supplier", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Paymentcategory", "Supplier");
                da.SelectCommand.Parameters.AddWithValue("@Name", txtnames.Text);
                da.SelectCommand.Parameters.AddWithValue("@Description", txtdescs.Text);
                da.SelectCommand.Parameters.AddWithValue("@Amount", txtamts.Text);
                da.SelectCommand.Parameters.AddWithValue("@PO_NO", ddlpos.SelectedItem.Text);
            }
            if (invoicenos != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@invoicenos", invoicenos);   
                da.SelectCommand.Parameters.AddWithValue("@User", Session["user"].ToString());
                string trantype = rbtntype.SelectedValue;
                da.SelectCommand.Parameters.AddWithValue("@TransactionType", trantype);
                da.SelectCommand.Parameters.AddWithValue("@Date", txtdt.Text);
                da.SelectCommand.Parameters.AddWithValue("@PaidDate", txtpaiddate.Text);
                da.SelectCommand.Parameters.AddWithValue("@CC_Code", Session["cc_code"].ToString());
                if (rbtntype.SelectedIndex == 1)
                    da.SelectCommand.Parameters.AddWithValue("@CC_Code1", ddlcccode.SelectedValue);
                if (ddlvendortype.SelectedItem.Text == "Service Provider")
                    da.SelectCommand.Parameters.AddWithValue("@Paymenttype", ddlservice.SelectedItem.Text);
                if (ddlvendortype.SelectedItem.Text == "Supplier")
                    da.SelectCommand.Parameters.AddWithValue("@Paymenttype", ddlservices.SelectedItem.Text);
               
                da.Fill(ds, "InvoiceTranNo");
                if (ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[0].ToString() == "Voucher Inserted")
                {
                    JavaScript.UPAlertRedirect(Page, "Voucher Inserted", Request.Url.ToString());
                }
                else
                {
                    JavaScript.UPAlert(Page, "Voucher Inserted Failed");
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
}