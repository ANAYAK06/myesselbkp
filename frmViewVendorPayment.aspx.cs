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
using System.IO;
using System.Text;
using AjaxControlToolkit;
using System.Collections.Specialized;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Drawing;

public partial class Admin_frmViewVendorPayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    string gvUniqueID = String.Empty;
    int gvNewPageIndex = 0;
    int gvEditIndex = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        //esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 49);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");

        if (!IsPostBack)
        {
            //LoadYear();
            ddlsubtype.Visible = false;
            ddlvendor.Visible = false;
            btnPrintCurrent.Visible = false;
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }
   
    public void subtype()
    {
        ddlvendor.Visible = false;
        ddlsubtype.Visible = true;

      if (ddltypeofpay.SelectedIndex == 1)
            {
                ddlsubtype.Visible = true;
                ddlsubtype.Items.Add("Service Type");
                ddlsubtype.Items.Add("Service Provider");
                ddlsubtype.Items.Add("Supplier");
               
            }
    
    }


    protected void ddlsubtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadvendor();
        ddlsubtype.Visible = true;
        payment.Visible = true;
    }

    private void loadvendor()
    {
        if (ddlsubtype.SelectedItem.Text == "Service Provider" || ddlsubtype.SelectedItem.Text == "Supplier")
        {
            ddlvendor.Visible = true;
            da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='" + ddlsubtype.SelectedItem.Text + "' and status='2' order by name asc", con);
            da.Fill(ds, "vendor");
            ddlvendor.DataTextField = "Name";
            ddlvendor.DataValueField = "vendor_id";
            ddlvendor.DataSource = ds.Tables["vendor"];
            ddlvendor.DataBind();
            ddlvendor.Items.Insert(0, "Select Vendor");
            ddlvendor.Items.Insert(1, "Select All");
          
        }
        else
        {
            ddlvendor.Visible = false;
        }
    }
   
    //public void LoadYear()
    //{
    //    for (int i = 2005; i <= System.DateTime.Now.Year; i++)
    //    {
    //        if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
    //        {
    //        }
    //        else
    //        {
    //            ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
    //        }

    //    }
    //    ddlyear.Items.Insert(0, "Any Year");
    //}
    private void loadpo(string q, string p)
    {

        if (ddlsubtype.SelectedItem.Text == "Service Provider")
        {
            da = new SqlDataAdapter("select distinct po_no from pending_invoice where paymenttype in ('" + ddlsubtype.SelectedItem.Text + "','Service Tax','Service provider VAT') and vendor_id='" + q + "' and cc_code='" + p + "'", con);
        }
        else  if (ddlsubtype.SelectedItem.Text == "Supplier")
        {
            da = new SqlDataAdapter("select distinct po_no from pending_invoice where paymenttype in ('" + ddlsubtype.SelectedItem.Text + "','Excise Duty','VAT') and vendor_id='" + q + "' and cc_code='" + p + "'", con);
        }

        else
        {
            da = new SqlDataAdapter("select distinct po_no from invoice where paymenttype='" + ddlsubtype.SelectedItem.Text + "'", con);

        }
        da.Fill(ds, "po");
        ddlpo.DataTextField = "po_no";
        ddlpo.DataValueField = "po_no";
        ddlpo.DataSource = ds.Tables["po"];
        ddlpo.DataBind();
        ddlpo.Items.Insert(0, "Select PO");
        ddlpo.Items.Insert(1, "Select All");
    }
    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvendor.SelectedIndex != 0)
        {
          
            loadCC(ddlvendor.SelectedValue);
            payment.Visible = true;
            ddlpo.Visible = true;
            ddlsubtype.Visible = true;
            ddltypeofpay.Visible = true;
            ddlvendor.Visible = true;
            cc.Visible = true;
            year.Visible = true;

        }
       
    }
    public void loadCC(string s)
    {
        //da = new SqlDataAdapter("Select distinct p.CC_code,(c.cc_code + ', ' + c.cc_name)as cc_name from pending_invoice p join Cost_Center c on p.cc_code =c.cc_code where vendor_id='" + s + "'", con);
        if (ddlvendor.SelectedValue != "Select All")
            da = new SqlDataAdapter("Select distinct p.CC_code,(c.cc_code + ', ' + c.cc_name)as cc_name from pending_invoice p join Cost_Center c on p.cc_code =c.cc_code where vendor_id='" + s + "'", con);
        else if (ddlvendor.SelectedValue == "Select All" && ddlsubtype.SelectedItem.Text == "Service Provider")
            da = new SqlDataAdapter("Select distinct p.CC_code,(c.cc_code + ', ' + c.cc_name)as cc_name from pending_invoice p join Cost_Center c on p.cc_code =c.cc_code where paymenttype in ('Service Provider','Service Tax','Service provider VAT') order by cc_code asc", con);
        else if (ddlvendor.SelectedValue == "Select All" && ddlsubtype.SelectedItem.Text == "Supplier")
            da = new SqlDataAdapter("Select distinct p.CC_code,(c.cc_code + ', ' + c.cc_name)as cc_name from pending_invoice p join Cost_Center c on p.cc_code =c.cc_code where paymenttype in ('Supplier','Excise Duty','VAT') order by cc_code asc", con);
        da.Fill(ds, "CC");
        ddlcccode.DataTextField = "cc_name";
        ddlcccode.DataValueField = "CC_code";
        ddlcccode.DataSource = ds.Tables["CC"];
        ddlcccode.DataBind();
        ddlcccode.Items.Insert(0, "Select CC Code");
        ddlcccode.Items.Insert(1, "Select All");

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        string Query1 = "";
        string Query2 = "";
        try
        {
            Query1 = Query1 + " and convert(datetime,a.invoice_date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";

            if (ddlcccode.SelectedValue != "Select Cost Center" && ddlcccode.SelectedValue != "Select All")
            {
                Query1 = Query1 + " and a.cc_code='" + ddlcccode.SelectedValue + "'";              
            }
            if (ddlpo.SelectedItem.Text != "Select PO" && ddlpo.SelectedItem.Text != "Select All")
            {
                Query1 = Query1 + " and  a.po_no='" + ddlpo.SelectedValue + "'";
                Query2 = Query2 + " and  po_no='" + ddlpo.SelectedValue + "'";
            }
            if (ddlvendor.SelectedItem.Text != "Select Vendor" && ddlvendor.SelectedItem.Text != "Select All")
            {
                Query1 = Query1 + " and vendor_id='" + ddlvendor.SelectedValue + "'";
            }
            ViewState["Query1"] = Query1;
            ViewState["Query2"] = Query2;
            veninfo1();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }

    protected void ddltypeofpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        subtype();
        if (ddltypeofpay.SelectedItem.Text == "Service")
        {
        ddlsubtype.Visible = true;
        ddlvendor.Visible = false;
        }
        else
        {
            ddlsubtype.Visible = false;
            ddlvendor.Visible = false;
        }
    }



    public void veninfo1()
    {
        try
        {
            if (ddlsubtype.SelectedItem.Text == "Service Provider")
            {
                //da = new SqlDataAdapter("(select (case when P.invoiceno is null then B.invoiceno else P.invoiceno end) Invoiceno,REPLACE(CONVERT(VARCHAR(50),CONVERT(DATETIME,CONVERT(VARCHAR(50), Invoice_date)), 106), ' ', '-')Invoice_date,cc_code,dca_code,Replace(isnull(Basicvalue,0),'.0000','.00')as [Basicvalue],Replace(isnull(Servicetax,0),'.0000','.00')as[Servicetax],Replace(isnull(total,0),'.0000','.00')as[total],Replace(isnull(tds,0),'.0000','.00')as[tds],Replace(isnull(retention,0),'.0000','.00')as[retention],Replace(isnull(advance,0),'.0000','.00')as[advance],Replace(isnull(hold,0),'.0000','.00')as[hold],Replace(isnull(anyother,0),'.0000','.00')as[anyother],Replace(isnull(netamount,0),'.0000','.00')as[netamount],Replace(isnull([TDS Balance],0),'.0000','.00')as[TDS Balance],Replace(isnull([Retention Balance],0),'.0000','.00')as[Retention Balance],Replace(isnull([Hold balance],0),'.0000','.00')as[Hold balance],Replace(isnull(balance,0),'.0000','.00')as[Invoice Balance],Replace(isnull(debit,0),'.0000','.00')as[Paid] from (SELECT a.invoice_date ,a.invoiceno ,a.cc_code ,a.dca_code,Replace(isnull(a.Basicvalue,0),'.0000','.00')as [Basicvalue],Replace(isnull(a.Servicetax,0),'.0000','.00')as[Servicetax],Replace(isnull(a.total,0),'.0000','.00')as[total],Replace(isnull(a.tds,0),'.0000','.00')as[tds],Replace(isnull(a.retention,0),'.0000','.00')as[retention],Replace(isnull(a.advance,0),'.0000','.00')as[advance],Replace(isnull(a.hold,0),'.0000','.00')as[hold],Replace(isnull(a.anyother,0),'.0000','.00')as[anyother],Replace(isnull(a.netamount,0),'.0000','.00')as[netamount],Replace(isnull(a.tds_balance,0),'.0000','.00')as[TDS Balance],Replace(isnull(a.retention_balance,0),'.0000','.00')as[Retention Balance],Replace(isnull(a.hold_balance,0),'.0000','.00')as[Hold balance],Replace(isnull(a.balance,0),'.0000','.00')as[balance] FROM pending_invoice AS a  WHERE   (a.paymenttype='Service Provider' or a.paymenttype='Service Tax' or  a.paymenttype='Service provider VAT')  " + ViewState["Query1"].ToString() + ")P full outer join(Select bk.invoiceno,Sum(isnull(debit,0))[Debit] from bankbook bk join pending_invoice [a] on bk.invoiceno=[a].invoiceno where (bk.status='Debited' or bk.status='1' or bk.status='2' or bk.status='3' or bk.status='Debit Pending' or bk.status is null) and ([a].paymenttype='Service Provider' or [a].paymenttype='Service Tax' or [a].paymenttype='Service provider VAT') " + ViewState["Query1"].ToString() + " group by [bk].invoiceno)B on P.invoiceno=B.invoiceno) order by CONVERT(DATETIME,CONVERT(VARCHAR(50), Invoice_Date)) asc", con);
                da = new SqlDataAdapter("(select (case when P.invoiceno is null then B.invoiceno else P.invoiceno end) Invoiceno,REPLACE(CONVERT(VARCHAR(50),CONVERT(DATETIME,CONVERT(VARCHAR(50), Invoice_date)), 106), ' ', '-')Invoice_date,cc_code,dca_code,Replace(isnull(Basicvalue,0),'.0000','.00')as [Basicvalue],Replace(isnull(Servicetax,0),'.0000','.00')as[Servicetax],Replace(isnull(total,0),'.0000','.00')as[total],Replace(ISNULL(amount,0),'.0000','.00')as[TaxAmt],Replace(isnull(tds,0),'.0000','.00')as[tds],Replace(isnull(retention,0),'.0000','.00')as[retention],Replace(isnull(advance,0),'.0000','.00')as[advance],Replace(isnull(hold,0),'.0000','.00')as[hold],Replace(isnull(anyother,0),'.0000','.00')as[anyother],Replace(isnull(netamount,0),'.0000','.00')as[netamount],Replace(isnull([TDS Balance],0),'.0000','.00')as[TDS Balance],Replace(isnull([Retention Balance],0),'.0000','.00')as[Retention Balance],Replace(isnull([Hold balance],0),'.0000','.00')as[Hold balance],Replace(isnull(balance,0),'.0000','.00')as[Invoice Balance],Replace(isnull(debit,0),'.0000','.00')as[Paid],name as vendorname from (SELECT a.invoice_date ,a.invoiceno ,a.cc_code ,a.dca_code,Replace(isnull(a.Basicvalue,0),'.0000','.00')as [Basicvalue],Replace(isnull(a.Servicetax,0),'.0000','.00')as[Servicetax],Replace(isnull(a.total,0),'.0000','.00')as[total],(SELECT ISNULL(SUM(amount),0)FROM Vendor_Taxes vt  WHERE type !='Deduction' AND vt.InvoiceNo=a.InvoiceNo" + ViewState["Query2"].ToString() + ")as amount,Replace(isnull(a.tds,0),'.0000','.00')as[tds],Replace(isnull(a.retention,0),'.0000','.00')as[retention],Replace(isnull(a.advance,0),'.0000','.00')as[advance],Replace(isnull(a.hold,0),'.0000','.00')as[hold],Replace(isnull(a.anyother,0),'.0000','.00')as[anyother],Replace(isnull(a.netamount,0),'.0000','.00')as[netamount],Replace(isnull(a.tds_balance,0),'.0000','.00')as[TDS Balance],Replace(isnull(a.retention_balance,0),'.0000','.00')as[Retention Balance],Replace(isnull(a.hold_balance,0),'.0000','.00')as[Hold balance],Replace(isnull(a.balance,0),'.0000','.00')as[balance],name FROM pending_invoice AS a  WHERE   (a.paymenttype='Service Provider' or a.paymenttype='Service Tax' or  a.paymenttype='Service provider VAT')  " + ViewState["Query1"].ToString() + ")P full outer join(Select bk.invoiceno,Sum(isnull(debit,0))[Debit] from bankbook bk join pending_invoice [a] on bk.invoiceno=[a].invoiceno where (bk.status='Debited' or bk.status='1' or bk.status='2' or bk.status='3' or bk.status='Debit Pending' or bk.status is null) and ([a].paymenttype='Service Provider' or [a].paymenttype='Service Tax' or [a].paymenttype='Service provider VAT') " + ViewState["Query1"].ToString() + " group by [bk].invoiceno)B on P.invoiceno=B.invoiceno) order by CONVERT(DATETIME,CONVERT(VARCHAR(50), Invoice_Date)) asc", con);
                da.Fill(ds, "veninfo");
                GridView1.DataSource = ds.Tables["veninfo"];
                GridView1.DataBind();
                if (ds.Tables["veninfo"].Rows.Count > 0)
                    btnPrintCurrent.Visible = true;
            }
            else if (ddlsubtype.SelectedItem.Text == "Supplier")
            {
                da = new SqlDataAdapter("(select (case when P.invoiceno is null then B.invoiceno else P.invoiceno end) Invoiceno,REPLACE(CONVERT(VARCHAR(50),CONVERT(DATETIME,CONVERT(VARCHAR(50), Invoice_date)), 106), ' ', '-')Invoice_date,cc_code,dca_code,Replace(isnull(Basicvalue,0),'.0000','.00')as [Basicvalue],Replace(isnull(Servicetax,0),'.0000','.00')as[Servicetax],Replace(isnull(total,0),'.0000','.00')as[total],Replace(ISNULL(amount,0),'.0000','.00')as[TaxAmt],Replace(isnull(tds,0),'.0000','.00')as[tds],Replace(isnull(retention,0),'.0000','.00')as[retention],Replace(isnull(advance,0),'.0000','.00')as[advance],Replace(isnull(hold,0),'.0000','.00')as[hold],Replace(isnull(anyother,0),'.0000','.00')as[anyother],Replace(isnull(netamount,0),'.0000','.00')as[netamount],Replace(isnull([TDS Balance],0),'.0000','.00')as[TDS Balance],Replace(isnull([Retention Balance],0),'.0000','.00')as[Retention Balance],Replace(isnull([Hold balance],0),'.0000','.00')as[Hold balance],Replace(isnull(balance,0),'.0000','.00')as[Invoice Balance],Replace(isnull(debit,0),'.0000','.00')as[Paid],name as vendorname from (SELECT a.invoice_date ,a.invoiceno ,a.cc_code ,a.dca_code,Replace(isnull(a.Basicvalue,0),'.0000','.00')as [Basicvalue],Replace(isnull(a.salestax,0),'.0000','.00')as[Servicetax],Replace(isnull(a.total,0),'.0000','.00')as[total],Replace(isnull(a.tds,0),'.0000','.00')as[tds],Replace(isnull(a.retention,0),'.0000','.00')as[retention],Replace(isnull(a.advance,0),'.0000','.00')as[advance],Replace(isnull(a.hold,0),'.0000','.00')as[hold],Replace(isnull(a.anyother,0),'.0000','.00')as[anyother],Replace(isnull(a.netamount,0),'.0000','.00')as[netamount],Replace(isnull(a.tds_balance,0),'.0000','.00')as[TDS Balance],Replace(isnull(a.retention_balance,0),'.0000','.00')as[Retention Balance],Replace(isnull(a.hold_balance,0),'.0000','.00')as[Hold balance],Replace(isnull(a.balance,0),'.0000','.00')as[balance],(SELECT ISNULL(SUM(amount),0)FROM Vendor_Taxes vt WHERE type !='Deduction' AND vt.InvoiceNo=a.InvoiceNo  " + ViewState["Query2"].ToString() + ")as amount,name FROM pending_invoice AS a  WHERE   (a.paymenttype='Supplier' or a.paymenttype='Excise Duty'  or a.paymenttype='VAT') " + ViewState["Query1"].ToString() + ")P full outer join(Select bk.invoiceno,Sum(isnull(debit,0))[Debit] from bankbook bk join pending_invoice [a] on bk.invoiceno=[a].invoiceno where (bk.status='Debited' or bk.status='1' or bk.status='2' or bk.status='3' or bk.status='Debit Pending' or bk.status is null) and ([a].paymenttype='Supplier' or [a].paymenttype='Excise Duty' or [a].paymenttype='VAT') " + ViewState["Query1"].ToString() + " group by [bk].invoiceno)B on P.invoiceno=B.invoiceno) order by CONVERT(DATETIME,CONVERT(VARCHAR(50), Invoice_Date)) asc", con);
                da.Fill(ds, "veninfo");
                GridView1.DataSource = ds.Tables["veninfo"];
                GridView1.DataBind();
                if (ds.Tables["veninfo"].Rows.Count > 0)
                    btnPrintCurrent.Visible = true;
            }

        }


        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
 
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ViewState["MainInv"] = e.Row.Cells[2].Text;
            GridViewRow row = e.Row;

            if (row.DataItem == null)
            {
                return;
            }
            GridView gv = new GridView();
            gv = (GridView)row.FindControl("GridView2");
            GridView gvtaxes = new GridView();
            gvtaxes = (GridView)row.FindControl("Grdviewpopup");          
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            ds1.Reset();
            ds2.Reset();
            if (gv.UniqueID == gvUniqueID)
            {
                gv.PageIndex = gvNewPageIndex;
                gv.EditIndex = gvEditIndex;                
                ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["invoiceno"].ToString() + "','one');</script>");
            }
            da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [Date],description,Bank_name,modeofpay,isnull(debit,0)[Debit] from BankBook where (status='Debited' or status='1' or status='2' or status='3' or status='Debit Pending' or status is null) and InvoiceNo='" + ((DataRowView)e.Row.DataItem)["invoiceno"].ToString() + "' union all select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [Date],(request_no+' , '+item_code) as description,name as bank_name,'Asset Sale' as modeofpay,isnull(Amount,0)[Debit]  from asset_payment where status='2' and invoiceNo='" + ((DataRowView)e.Row.DataItem)["invoiceno"].ToString() + "'", con);
            da.Fill(ds1, "fillgrid2");
            gv.DataSource = ds1.Tables["fillgrid2"];
            gv.DataBind();
            //da = new SqlDataAdapter("select id,InvoiceNo,CC_Code,Dca_Code,Subdca_Code,RegdNo,Type,Amount,Balance,Tax_Type from Vendor_Taxes where status is null and InvoiceNo ='" + ((DataRowView)e.Row.DataItem)["invoiceno"].ToString() + "' order by CASE WHEN [Type]='Deduction' THEN [Type] END,CASE WHEN [Type]='Other'  THEN [Type] END,CASE WHEN [Type]='VAT Tax' THEN [Type] END,CASE WHEN [Type]='Excise Tax'  THEN [Type] END ,CASE WHEN [Type]='Service Tax'  THEN [Type] END,CASE WHEN [Type]='Cess'  THEN [Type] END,CASE WHEN [Type]='GST'  THEN [Type] END", con);
            da = new SqlDataAdapter("select id,InvoiceNo,CC_Code,d.mapdca_code as Dca_Code,sd.mapsubdca_code as Subdca_Code,RegdNo,Type,Amount as tamt,Balance as tbal,vt.Tax_Type from Vendor_Taxes vt JOIN dca d ON d.dca_code=vt.Dca_Code JOIN subdca sd ON vt.Subdca_Code=sd.subdca_code where status is null and InvoiceNo ='" + ((DataRowView)e.Row.DataItem)["invoiceno"].ToString() + "' order by CASE WHEN [Type]='Deduction' THEN [Type] END,CASE WHEN [Type]='Other'  THEN [Type] END,CASE WHEN [Type]='VAT Tax' THEN [Type] END,CASE WHEN [Type]='Excise Tax'  THEN [Type] END ,CASE WHEN [Type]='Service Tax'  THEN [Type] END,CASE WHEN [Type]='Cess'  THEN [Type] END,CASE WHEN [Type]='GST'  THEN [Type] END", con);
            da.Fill(ds2, "checkpo");
            if (ds2.Tables["checkpo"].Rows.Count > 0)
            {
                
                gvtaxes.DataSource = ds2.Tables["checkpo"];
                gvtaxes.DataBind();              
               
            }
            else
            {
                gvtaxes.DataSource = null;
                gvtaxes.DataBind();               
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Basicvalue += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Basicvalue"));
            Servicetax += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Servicetax"));
            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
            TaxAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmt"));
            TDS += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tds"));
            Retention += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "retention"));
            Advance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "advance"));
            hold += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "hold"));
            anyother += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "anyother"));
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "netamount"));
            tdsbalance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tds balance"));
            retentionbalance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "retention balance"));
            holdbalance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "hold balance"));           
            paid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "paid"));
            Bal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "invoice balance"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Basicvalue);
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", Servicetax);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Total);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", TaxAmt);
            e.Row.Cells[10].Text = String.Format("Rs. {0:#,##,##,###.00}", TDS);
            e.Row.Cells[11].Text = String.Format("Rs. {0:#,##,##,###.00}", Retention);
            e.Row.Cells[12].Text = String.Format("Rs. {0:#,##,##,###.00}", Advance);
            e.Row.Cells[13].Text = String.Format("Rs. {0:#,##,##,###.00}", hold);
            e.Row.Cells[14].Text = String.Format("Rs. {0:#,##,##,###.00}", anyother);
            e.Row.Cells[15].Text = String.Format("Rs. {0:#,##,##,###.00}", NetAmount);
            e.Row.Cells[16].Text = String.Format("Rs. {0:#,##,##,###.00}", tdsbalance);
            e.Row.Cells[17].Text = String.Format("Rs. {0:#,##,##,###.00}", retentionbalance);
            e.Row.Cells[18].Text = String.Format("Rs. {0:#,##,##,###.00}", holdbalance);            
            e.Row.Cells[19].Text = String.Format("Rs. {0:#,##,##,###.00}", paid);
            e.Row.Cells[20].Text = String.Format("Rs. {0:#,##,##,###.00}", Bal);
        }


     
     
    }
    private decimal Basicvalue = (decimal)0.0;
    private decimal Servicetax = (decimal)0.0;
    private decimal Total = (decimal)0.0;
    private decimal TaxAmt = (decimal)0.0;
    private decimal TDS = (decimal)0.0;
    private decimal Retention = (decimal)0.0;
    private decimal Advance = (decimal)0.0;
    private decimal hold = (decimal)0.0;
    private decimal anyother = (decimal)0.0;
    private decimal NetAmount = (decimal)0.0;
    private decimal Amount = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
    private decimal tdsbalance = (decimal)0.0; 
    private decimal retentionbalance = (decimal)0.0;
    private decimal holdbalance = (decimal)0.0;
    private decimal paid = (decimal)0.0;
    private decimal grdTotal = 0;
    private decimal MastergrdTotal = 0;
    private decimal TAmt = (decimal)0.0;
    private decimal Bal = (decimal)0.0;
    private decimal TBal = (decimal)0.0;

    protected void Grdviewpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["Inv"] != null)
            {
                if (ViewState["Inv"].ToString() == "Change")
                {
                    TBal = 0;
                    TAmt = 0;
                }
            }
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
                TBal -= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tbal"));
                TAmt -= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tamt"));
            }
            else
            {
                TBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tbal"));
                TAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tamt"));
            }
            ViewState["Inv"] = "Same";
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", TBal);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", TAmt);
            ViewState["Inv"] = "Change";
        }        
    }

  
    protected void PrintAllPages(object sender, EventArgs e)
    {
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView1.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=10000,height=6000,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
   
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadpo(ddlvendor.SelectedValue, ddlcccode.SelectedValue);
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    //    decimal Debit = (decimal)0.0;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Debit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
    //    }
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        e.Row.Cells[3].Text = Debit.ToString();
    //    }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
            grdTotal = grdTotal + rowTotal;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotal");
            lbl.Text = grdTotal.ToString();
            grdTotal = 0;
        }

    }
}
