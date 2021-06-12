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
using System.Drawing;

public partial class AssetSalePayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    DataSet ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                fillassetitems();
                btnvisible.Visible = false;
                tblbankpayment.Visible = false;
                tbldesc.Visible = false;
                trpaymentselection.Visible = false;
                tblserviceprovider.Visible = false;
                tblsupplier.Visible = false;
                tramount.Visible = false;
            }
        }
    }
    public void fillassetitems()
    {
        da = new SqlDataAdapter("select ass.Id,(ass.Request_no+' , '+ass.Item_code+' , '+ic.item_name+' , '+Specification)as name from Asset_Sale ass join Item_codes ic on SUBSTRING(ass.item_code,1,8)=ic.Item_code where ass.status='6'", con);
        ds = new DataSet();
        da.Fill(ds, "Trandetails");
        if (ds.Tables["Trandetails"].Rows.Count > 0)
        {
            ddlTranNo.DataSource = ds.Tables["Trandetails"];
            ddlTranNo.DataTextField = "name";
            ddlTranNo.DataValueField = "id";
            ddlTranNo.DataBind();
            ddlTranNo.Items.Insert(0, new ListItem("Select"));
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            //da = new SqlDataAdapter("select md.id as id,md.Item_code,ic.item_name,ic.Specification,ic.Dca_Code,ic.Subdca_Code,cast(md.Basicprice as decimal(20,2))as Basicprice,md.Status from Master_data md join Item_codes ic on SUBSTRING(md.item_code,1,8)=ic.Item_code  where md.id='" + ddlasset.SelectedValue + "'", con);
            da = new SqlDataAdapter("select ID,Request_No,Item_code,REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')as Date,REPLACE(CONVERT(VARCHAR(11),BookValue_Date, 106), ' ', '-')as BookValue_Date,cast(Selling_Amt as decimal(20,2))as Selling_Amt,Buyer_Name,Buyer_Address,cast(Balance_Amt as decimal(20,2))as Balance_Amt from Asset_Sale where status='6' and Balance_Amt !='0' and id='" + ddlTranNo.SelectedValue + "'", con);
            da.Fill(ds, "grid");
            if (ds.Tables["grid"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["grid"];
                GridView1.DataBind();
                trpaymentselection.Visible = true;
                tbldesc.Visible = true;
                hfremain.Value = ds.Tables["grid"].Rows[0]["Balance_Amt"].ToString();
                hfbookvaluedate.Value = ds.Tables["grid"].Rows[0]["BookValue_Date"].ToString();
                txtbuyername.Text = ds.Tables["grid"].Rows[0]["Buyer_Name"].ToString();
                txtbuyeraddress.Text = ds.Tables["grid"].Rows[0]["Buyer_Address"].ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                trpaymentselection.Visible = false;
                tbldesc.Visible = false;
                hfremain.Value = Convert.ToInt32(0).ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlselection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlselection.SelectedValue == "BankPayment")
        {
            tblbankpayment.Visible = true;
            tblserviceprovider.Visible = false;
            tblsupplier.Visible = false;
            tramount.Visible = false;
            btnvisible.Visible = true;
        }
        else if (ddlselection.SelectedValue == "ServiceProvider")
        {
            tblbankpayment.Visible = false;
            tblserviceprovider.Visible = true;
            tblsupplier.Visible = false;
            service();
            LoadYear();
            trselection.Visible = false;
            trname.Visible = false;
            tramount.Visible = false;
            btnvisible.Visible = false;
            grd.DataSource = null;
            grd.DataBind();
            txtamount.Text = "0";
            if (ddlvendor.Visible == true)
                ddlvendor.Items.Clear();
        }
        else if (ddlselection.SelectedValue == "Supplier")
        {
            tblbankpayment.Visible = false;
            tblserviceprovider.Visible = false;
            tblsupplier.Visible = true;
            trselectionsup.Visible = false;
            trnamesup.Visible = false;
            tramount.Visible = false;
            servicesup();
            LoadYearsup();
            btnvisible.Visible = false;
            grdsup.DataSource = null;
            grdsup.DataBind();
            txtamount.Text = "0";
            if (ddlvendorsup.Visible == true)
                ddlvendorsup.Items.Clear();
        }
        else
        {
            tblbankpayment.Visible = false;
            tblserviceprovider.Visible = false;
            tblsupplier.Visible = false;
            btnvisible.Visible = false;
            tramount.Visible = false;
            txtamount.Text = "0";
            //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        }
    }
    #region Serviceprovider
    private void service()
    {
        ddlservice.Items.Clear();
        ddlservice.Visible = true;
        ddlservice.Items.Add("-Select Service Type-");
        ddlservice.Items.Add("Invoice Payment");
        ddlservice.Items.Add("Advance Payment");
        //ddlservice.Items.Add("Retention");
        //ddlservice.Items.Add("Hold");
    }
    protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlservice.SelectedItem.Text != "-Select Service Type-")
        {
            laodvendor();
            vename.Visible = true;
            ddlvendor.Visible = true;
            trselection.Visible = false;
            ddlmonth.Visible = false;
            ddlyear.Visible = false;
            ddlpo.Visible = true;
            Button1.Visible = true;
            trgrid.Visible = false;
            trname.Visible = false;
        }

        else
        {
            laodvendor();
            trselection.Visible = false;
            trgrid.Visible = false;
            trname.Visible = false;            
            trgrid.Visible = false;
            trname.Visible = false;         
        }
    }
    private void laodvendor()
    {
        try
        {
            da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='Service Provider' and status='2' order by name asc", con);
            da.Fill(ds, "vendor");
            ddlvendor.DataTextField = "Name";
            ddlvendor.DataValueField = "vendor_id";
            ddlvendor.DataSource = ds.Tables["vendor"];
            ddlvendor.DataBind();
            ddlvendor.Items.Insert(0, "Select Vendor");
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
        if (ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono where pi.balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Supplier','VAT') and pi.status in('3','Debit Pending') AND sp.status not in('Closed','Closed2','Closed3','Rejected') ", con);
        else if (ddlservice.SelectedItem.Text == "TDS")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.tds_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax') AND sp.status not in('Closed','Closed2','Closed3','Rejected')", con);
        else if (ddlservice.SelectedItem.Text == "Retention")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.Retention_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax') AND sp.status not in('Closed','Closed2','Closed3','Rejected')", con);
        else if (ddlservice.SelectedItem.Text == "Hold")
            da = new SqlDataAdapter("select distinct pi.po_no from pending_invoice pi join SPPO sp on pi.PO_NO=sp.pono  where pi.Hold_balance!='0' and pi.vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax') AND sp.status not in('Closed','Closed2','Closed3','Rejected')", con);
        da.Fill(ds, "po");
        ddlpo.DataTextField = "po_no";
        ddlpo.DataValueField = "po_no";
        ddlpo.DataSource = ds.Tables["po"];
        ddlpo.DataBind();
        ddlpo.Items.Insert(0, "Select PO");
        ddlpo.Items.Insert(1, "Select All");

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
            tramount.Visible = true;
            btnvisible.Visible = true;
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
            grd.EmptyDataText = "No Data Avaliable";
            tramount.Visible = false;
            btnvisible.Visible = false;
        }
    }
    private decimal Basic = (decimal)0.0;
    private decimal Retention = (decimal)0.0;
    private decimal Hold = (decimal)0.0;
    private decimal NetAmount = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
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
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rec in grd.Rows)
        {
            CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");
            CheckBox chkAll = (CheckBox)grd.HeaderRow.FindControl("chkAll");
        }
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
    #endregion
    #region Supplier
    public void LoadYearsup()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyearsup.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyearsup.Items.Insert(0, "Any Year");
    }
    private void servicesup()
    {
        ddlservicesup.Items.Clear();
        ddlservicesup.Visible = true;
        ddlservicesup.Items.Add("-Select Service Type-");
        ddlservicesup.Items.Add("Invoice Payment");
        ddlservicesup.Items.Add("Advance Payment");

    }
    protected void ddlservicesup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlservicesup.SelectedItem.Text != "-Select Service Type-")
        {
            laodvendorsup();
            venamesup.Visible = true;
            ddlvendorsup.Visible = true;
            trselectionsup.Visible = false;
            ddlmonthsup.Visible = false;
            ddlyearsup.Visible = false;
            ddlposup.Visible = true;
            Button1sup.Visible = true;
            trgridsup.Visible = false;
            trnamesup.Visible = false;            
        }

        else
        {
            laodvendorsup();
            trselectionsup.Visible = false;
            trgridsup.Visible = false;
            trnamesup.Visible = false;            
            trgridsup.Visible = false;
            trnamesup.Visible = false;           
        }
    }
    private void laodvendorsup()
    {
        try
        {
            da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='Supplier' and status='2' order by name asc", con);
            da.Fill(ds, "vendor");
            ddlvendorsup.DataTextField = "Name";
            ddlvendorsup.DataValueField = "vendor_id";
            ddlvendorsup.DataSource = ds.Tables["vendor"];
            ddlvendorsup.DataBind();
            ddlvendorsup.Items.Insert(0, "Select Vendor");          

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void ddlvendorsup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvendorsup.SelectedIndex != 0)
        {
            ddlposup.Visible = true;
            Button1sup.Visible = true;
            trnamesup.Visible = true;
            trselectionsup.Visible = true;
            loadposup(ddlvendorsup.SelectedValue);
            da = new SqlDataAdapter("Select vendor_name from vendor where vendor_id='" + ddlvendorsup.SelectedValue + "'", con);
            da.Fill(ds, "name");
            if (ds.Tables["name"].Rows.Count > 0)
                txtnamesup.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
        }
        else
        {
            ddlposup.Visible = false;
            Button1sup.Visible = false;
            trnamesup.Visible = false;
            trselectionsup.Visible = false;
        }
    }
    private void loadposup(string q)
    {
        if (ddlservicesup.SelectedItem.Text == "Invoice Payment" || ddlservicesup.SelectedItem.Text == "Advance Payment")
        {
            da = new SqlDataAdapter("(select (case when cp.po_no is null then chp.po_no else cp.po_no end) po_no from (select distinct p.po_no from pending_invoice p left outer join mr_report mr ON p.po_no=mr.po_no where balance!='0' and vendor_id='" + q + "' and paymenttype in('Supplier') AND mr.status='8') cp full outer join (select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype in('Supplier')) chp on cp.po_no=chp.po_no)", con);
        }

        da.Fill(ds, "po");
        ddlposup.DataTextField = "po_no";
        ddlposup.DataValueField = "po_no";
        ddlposup.DataSource = ds.Tables["po"];
        ddlposup.DataBind();
        ddlposup.Items.Insert(0, "Select PO");
        ddlposup.Items.Insert(1, "Select All");
    }
    protected void Button1sup_Click(object sender, EventArgs e)
    {
        fillgridsup();
        trgridsup.Visible = true;
        //txtamt.Text = "";

    }
    private void fillgridsup()
    {
        if (ddlposup.SelectedItem.Text != "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount, 0) as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11), convert(datetime, i.invoice_date, 101), 106), ' ', '-') as invoice_date, i.status, isnull(Retention_balance, 0) as[Retention], isnull(Hold_balance, 0) as[Hold],i.po_no from pending_invoice i where i.po_no = '" + ddlposup.SelectedValue + "' and balance!= 0  and vendor_id = '" + ddlvendorsup.SelectedValue + "' and i.paymenttype='Supplier'", con);
        }
        else if (ddlposup.SelectedItem.Text == "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(BasicValue,0)as[BasicValue],isnull(NetAmount, 0) as[NetAmount],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11), convert(datetime, i.invoice_date, 101), 106), ' ', '-') as invoice_date, i.status, isnull(Retention_balance, 0) as[Retention], isnull(Hold_balance, 0) as[Hold],i.po_no from pending_invoice i JOIN mr_report mr ON i.po_no = mr.po_no where balance!= 0  and vendor_id = '" + ddlvendorsup.SelectedValue + "' AND mr.status = '8' and i.paymenttype='Supplier'", con);
        }
        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {
            grdsup.DataSource = ds.Tables["fill"];
            grdsup.DataBind();
            tramount.Visible = true;
            btnvisible.Visible = true;
        }
        else
        {
            grdsup.DataSource = null;
            grdsup.DataBind();
            grdsup.EmptyDataText = "No Data Avaliable";
            tramount.Visible = false;
            btnvisible.Visible = false;
           
        }
    }
    protected void grdsup_RowDataBound(object sender, GridViewRowEventArgs e)
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
    private decimal Basics = (decimal)0.0;
    private decimal NetAmounts = (decimal)0.0;
    private decimal Balances = (decimal)0.0;
    protected void grdsup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grdsup.SelectedIndex != -1)
        {
            string invoiceno = grdsup.SelectedDataKey.Values["InvoiceNo"].ToString();
            string pono = grdsup.SelectedDataKey.Values["po_no"].ToString();
            fillpopsup(invoiceno, pono);
        }
    }
    public void fillpopsup(string inv, string pono)
    {
        string ints = inv + '%';
       
        da = new SqlDataAdapter("select id,InvoiceNo,CC_Code,Dca_Code,Subdca_Code,RegdNo,Type,Amount,Balance,Tax_Type from Vendor_Taxes where status is null and InvoiceNo like'" + ints + "' and Po_No='" + pono + "' order by CASE WHEN [Type]='Deduction' THEN [Type] END,CASE WHEN [Type]='Frieght'  THEN [Type] END,CASE WHEN [Type]='Other'  THEN [Type] END,CASE WHEN [Type]='VAT Tax' THEN [Type] END,CASE WHEN [Type]='Excise Tax'  THEN [Type] END", con);
        da.Fill(ds, "checkpos");
        if (ds.Tables["checkpos"].Rows.Count > 0)
        {
            Grdviewpopupsup.DataSource = ds.Tables["checkpos"];
            Grdviewpopupsup.DataBind();           
            popviewsup.Show();
            
        }
        else
        {
            Grdviewpopupsup.DataSource = null;
            Grdviewpopupsup.DataBind();
            popviewsup.Hide();
        }
    }
    private decimal Amts = (decimal)0.0;
    protected void Grdviewpopupsup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            da = new SqlDataAdapter("select mapdca_code from dca where dca_code='" + Grdviewpopupsup.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' UNION ALL select mapsubdca_code FROM subdca WHERE subdca_code = '" + Grdviewpopupsup.DataKeys[e.Row.RowIndex].Values[1].ToString() + "'", con);
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
            //e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Basic);
            //e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", NetAmount);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Balances);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Amts);

        }
        ds1.Clear();
    }
    protected void chkAllsup_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rec in grdsup.Rows)
        {
            CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");
            CheckBox chkAll = (CheckBox)grdsup.HeaderRow.FindControl("chkAll");
        }
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlselection.SelectedValue == "BankPayment")
        {
            BankDateCheck objdate = new BankDateCheck();
            if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedItem.Text))
            {
                btnSave.Visible = false;
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    
                    cmd.CommandText = "sp_Bank_Credit_Asset";
                    cmd.Parameters.AddWithValue("@date", txtdate.Text);  //BankDate
                    cmd.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                    cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@Role", Session["roles"].ToString());
                    cmd.Parameters.AddWithValue("@Type", ddlselection.SelectedValue);
                    foreach (GridViewRow record in GridView1.Rows)
                    {
                        cmd.Parameters.AddWithValue("@Id", GridView1.DataKeys[record.RowIndex]["id"].ToString());
                    }                   
                    cmd.Parameters.AddWithValue("@datechk", txtdates.Text);    //Entry Date
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "";
                    if (msg == "Credit Submitted")
                    {
                        JavaScript.UPAlertRedirect(Page, msg, "AssetSalePayment.aspx");
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, msg);
                        btnSave.Visible = true;
                    }
                    con.Close();
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
        else if (ddlselection.SelectedValue == "ServiceProvider")             
        {
            btnSave.Visible = false;           
            try
            {
                string invoicenos = "";
                string assetid = "";
                foreach (GridViewRow rec in grd.Rows)
                {
                    if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                        invoicenos = invoicenos + grd.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                }
                foreach (GridViewRow record in GridView1.Rows)
                {
                    assetid = GridView1.DataKeys[record.RowIndex]["id"].ToString();
                    //da.SelectCommand.Parameters.AddWithValue("@AssetId", GridView1.DataKeys[record.RowIndex]["id"].ToString());                    
                }        
                if (invoicenos != "")
                {                   
                    if (ddlservice.SelectedItem.Text == "Retention")
                    {

                        //da = new SqlDataAdapter("sp_VendorRetention_payment", con);
                        //da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Service Provider");

                    }
                    else if (ddlservice.SelectedItem.Text == "Hold")
                    {
                        //da = new SqlDataAdapter("sp_VendorHold_payment", con);
                        //da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Service Provider");


                    }
                    else if (ddlservice.SelectedItem.Text == "Invoice Payment" || ddlservice.SelectedItem.Text == "Advance Payment")
                    {
                        da = new SqlDataAdapter("sp_Debit_Pending_AssetSale", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Service Provider");
                    }

                }
                
                da.SelectCommand.Parameters.AddWithValue("@invoicenos", invoicenos);
                da.SelectCommand.Parameters.AddWithValue("@AssetId", assetid);
                da.SelectCommand.Parameters.AddWithValue("@Amount", txtamount.Text);
                da.SelectCommand.Parameters.AddWithValue("@User", Session["user"].ToString());
                da.SelectCommand.Parameters.AddWithValue("@Name", txtname.Text); 
                da.SelectCommand.Parameters.AddWithValue("@date", txtdates.Text);
                da.Fill(ds, "InvoiceTranNo");
                if (ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[0].ToString() == "Sucessfull")
                {
                    JavaScript.UPAlertRedirect(Page, "Transaction No is: " + ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[1].ToString(), "AssetSalePayment.aspx");
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
        else if (ddlselection.SelectedValue == "Supplier")
        {
            btnSave.Visible = false;
            string assetid = "";
            try
            {
                string invoicenos = "";
                foreach (GridViewRow rec in grdsup.Rows)
                {
                    if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                        invoicenos = invoicenos + grdsup.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                }
                foreach (GridViewRow record in GridView1.Rows)
                {
                    assetid = GridView1.DataKeys[record.RowIndex]["id"].ToString();                              
                }  
                if (invoicenos != "")
                {
                    da = new SqlDataAdapter("sp_Debit_Pending_Assetsp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Supplier");
                    da.SelectCommand.Parameters.AddWithValue("@invoicenos", invoicenos);
                    da.SelectCommand.Parameters.AddWithValue("@AssetId", assetid);
                    da.SelectCommand.Parameters.AddWithValue("@Amount", txtamount.Text);
                    da.SelectCommand.Parameters.AddWithValue("@User", Session["user"].ToString());
                    da.SelectCommand.Parameters.AddWithValue("@Name", txtnamesup.Text);
                    da.SelectCommand.Parameters.AddWithValue("@date", txtdates.Text);
                    da.Fill(ds, "InvoiceTranNosup");
                    if (ds.Tables["InvoiceTranNosup"].Rows[0].ItemArray[0].ToString() == "Sucessfull")
                    {
                        JavaScript.UPAlertRedirect(Page, "Transaction No is: " + ds.Tables["InvoiceTranNosup"].Rows[0].ItemArray[1].ToString(), "AssetSalePayment.aspx");
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
}