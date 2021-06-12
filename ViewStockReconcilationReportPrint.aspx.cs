using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ViewStockReconcilationReportPrint : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["Type"] == "1")
            {
                assets();
            }
            if (Request.QueryString["Type"] == "2")
            {
                semiassets();
            }
            if (Request.QueryString["Type"] == "3" || Request.QueryString["Type"] == "4")
            {
                Consumable();
            }
        }
    }

    public void semiassets()
    {
        if (Request.QueryString["For"].ToString() == "RecivedFromCentralStore")
        {
            lblheader.Text = "Recived From Central Store";
            da = new SqlDataAdapter("SELECT ti1.recieved_cc as recieved_cc ,it1.quantity,it1.ref_no as No,Convert(varchar(19),convert(datetime,ti1.recieved_date),106)as Date,'' as po_no,ti1.remarks,it1.item_code FROM [items transfer] it1 JOIN [transfer info]ti1 ON ti1.ref_no=it1.ref_no WHERE ti1.cc_code='CC-33' AND ti1.recieved_cc='" + Request.QueryString["cccode"].ToString() + "' AND ti1.status='4' and it1.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "RECEIVEDFROMOTHERCC")
        {
            lblheader.Text = "Recived From Other CC";
            da = new SqlDataAdapter("SELECT ti2.recieved_cc as recieved_cc ,it2.quantity,it2.ref_no as No,REPLACE(REPLACE(CONVERT(VARCHAR,ti2.recieved_date ,106), ' ','-'), ',','')as Date,'' as po_no,ti2.remarks,it2.item_code  FROM [items transfer] it2 JOIN [transfer info]ti2 ON ti2.ref_no=it2.ref_no WHERE ti2.cc_code NOT IN ('CC-33')and ti2.cc_code NOT IN ('" + Request.QueryString["cccode"].ToString() + "') AND ti2.recieved_cc='" + Request.QueryString["cccode"].ToString() + "' AND ti2.status='6' and it2.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "PURCHASEDATCC")
        {
            lblheader.Text = "Purchase At CC";
            da = new SqlDataAdapter("SELECT pd.cc_code as recieved_cc ,ri.quantity,mr.mrr_no as [No],mr.recieved_date as [Date],ri.po_no,pd.remarks,ri.item_code FROM [recieved items] ri JOIN mr_report mr on ri.po_no=mr.po_no join purchase_details pd On pd.po_no=ri.po_no WHERE pd.cc_code='" + Request.QueryString["cccode"].ToString() + "' and ri.item_code ='" + Request.QueryString["id"].ToString() + "' and  mr.status!='Rejected'", con);
        }
        else if (Request.QueryString["For"].ToString() == "TRANSFEREDTOCENTRALSTORE")
        {
            lblheader.Text = "Transfered To Central Store";
            da = new SqlDataAdapter("SELECT ti3.recieved_cc,it3.quantity,it3.ref_no as [No],Convert(varchar(19),convert(datetime,ti3.recieved_date),106)as Date,'' as po_no,ti3.remarks,it3.item_code FROM [items transfer] it3 JOIN [transfer info]ti3 ON ti3.ref_no=it3.ref_no WHERE ti3.recieved_cc='CC-33' and ti3.cc_code='" + Request.QueryString["cccode"].ToString() + "' AND it3.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "TRANSFERTOOTHERCC")
        {
            lblheader.Text = "Transfer To Other CC";
            da = new SqlDataAdapter("SELECT ti4.recieved_cc,it4.quantity,it4.ref_no as [No],Convert(varchar(19),convert(datetime,ti4.recieved_date),106)as Date,'' as po_no,ti4.remarks,it4.item_code FROM [items transfer] it4 JOIN [transfer info]ti4 ON ti4.ref_no=it4.ref_no WHERE ti4.recieved_cc NOT IN ('CC-33')and ti4.recieved_cc NOT IN ('" + Request.QueryString["cccode"].ToString() + "') and ti4.cc_code='" + Request.QueryString["cccode"].ToString() + "' AND it4.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "LOSTANDDAMAGESREPORTACCPETED")
        {
            lblheader.Text = "Lost and Damage Report Accepted";
            da = new SqlDataAdapter("SELECT ldr.cc_code as recieved_cc,ldi.quantity,ldr.ref_no as [No],Convert(varchar(19),convert(datetime,ldr.date),106)as Date,'' as po_no,ldi.cmcremarks as remarks,ldi.item_code from [lost/damaged_items]ldi JOIN [lost/damaged report]ldr ON ldi.ref_no=ldr.ref_no WHERE ldr.status='3' AND ldi.cc_code='" + Request.QueryString["cccode"].ToString() + "' AND ldi.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        da.Fill(ds);
        Gvdetails.DataSource = ds;
        Gvdetails.DataBind();

    }
    protected void Gvdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if ((Request.QueryString["For"].ToString() == "RecivedFromCentralStore") || (Request.QueryString["For"].ToString() == "RECEIVEDFROMOTHERCC"))
            {
                e.Row.Cells[1].Text = "Recieved CC";
                e.Row.Cells[3].Text = "Reference No";
                e.Row.Cells[4].Visible = false;
            }
            if (Request.QueryString["For"].ToString() == "PURCHASEDATCC")
            {
                e.Row.Cells[1].Text = "Recieved CC";
                e.Row.Cells[3].Text = "MRR No";
                e.Row.Cells[4].Visible = true;
            }
            if (Request.QueryString["For"].ToString() == "TRANSFEREDTOCENTRALSTORE")
            {
                e.Row.Cells[1].Text = "Recieved CC";
                e.Row.Cells[3].Text = "Reference No";
                e.Row.Cells[4].Visible = false;
            }
            if (Request.QueryString["For"].ToString() == "TRANSFERTOOTHERCC")
            {
                e.Row.Cells[1].Text = "Recieved CC";
                e.Row.Cells[3].Text = "Reference No";
                e.Row.Cells[4].Visible = false;
            }
            if (Request.QueryString["For"].ToString() == "LOSTANDDAMAGESREPORTACCPETED")
            {
                e.Row.Cells[1].Text = "Recieved CC";
                e.Row.Cells[3].Text = "Reference No";
                e.Row.Cells[4].Visible = false;
            }
            if (Request.QueryString["For"].ToString() == "IssuedForCCConsumption")
            {
                e.Row.Cells[1].Text = "CC Code";
                e.Row.Cells[3].Text = "Reference No";
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[5].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((Request.QueryString["For"].ToString() == "RecivedFromCentralStore") || (Request.QueryString["For"].ToString() == "RECEIVEDFROMOTHERCC"))
            {
                e.Row.Cells[4].Visible = false;
            }
            if (Request.QueryString["For"].ToString() == "PURCHASEDATCC")
            {
                e.Row.Cells[4].Visible = true;
            }
            if (Request.QueryString["For"].ToString() == "TRANSFEREDTOCENTRALSTORE")
            {
                e.Row.Cells[4].Visible = false;
            }
            if (Request.QueryString["For"].ToString() == "TRANSFERTOOTHERCC")
            {
                e.Row.Cells[4].Visible = false;
            }
            if (Request.QueryString["For"].ToString() == "LOSTANDDAMAGESREPORTACCPETED")
            {
                e.Row.Cells[4].Visible = false;
            }
            if (Request.QueryString["For"].ToString() == "IssuedForCCConsumption")
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[5].Visible = false;
            }
        }
    }
    public void Consumable()
    {       
        if (Request.QueryString["For"].ToString() == "RecivedFromCentralStore")
        {
            lblheader.Text = "Received From Central Store";
            da = new SqlDataAdapter("SELECT ti1.recieved_cc as recieved_cc ,it1.quantity,it1.ref_no as No,Convert(varchar(19),convert(datetime,ti1.recieved_date),106)as Date,'' as po_no,ti1.remarks,it1.item_code FROM [items transfer] it1 JOIN [transfer info]ti1 ON ti1.ref_no=it1.ref_no WHERE ti1.cc_code='CC-33' AND ti1.recieved_cc='" + Request.QueryString["cccode"].ToString() + "' AND ti1.status='4' and it1.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "RECEIVEDFROMOTHERCC")
        {
            lblheader.Text = "Received From Other CC";
            da = new SqlDataAdapter("SELECT ti2.recieved_cc as recieved_cc ,it2.quantity,it2.ref_no as No,REPLACE(REPLACE(CONVERT(VARCHAR,ti2.recieved_date ,106), ' ','-'), ',','')as Date,'' as po_no,ti2.remarks,it2.item_code  FROM [items transfer] it2 JOIN [transfer info]ti2 ON ti2.ref_no=it2.ref_no WHERE ti2.cc_code NOT IN ('CC-33')and ti2.cc_code NOT IN ('" + Request.QueryString["cccode"].ToString() + "') AND ti2.recieved_cc='" + Request.QueryString["cccode"].ToString() + "' AND ti2.status='6' and it2.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "PURCHASEDATCC")
        {
            lblheader.Text = "Purchase At CC";
            da = new SqlDataAdapter("SELECT pd.cc_code as recieved_cc ,ri.quantity,mr.mrr_no as [No],mr.recieved_date as [Date],ri.po_no,pd.remarks,ri.item_code FROM [recieved items] ri JOIN mr_report mr on ri.po_no=mr.po_no join purchase_details pd On pd.po_no=ri.po_no WHERE pd.cc_code='" + Request.QueryString["cccode"].ToString() + "' and ri.item_code ='" + Request.QueryString["id"].ToString() + "' and  mr.status!='Rejected'", con);
        }
        else if (Request.QueryString["For"].ToString() == "TRANSFEREDTOCENTRALSTORE")
        {
            lblheader.Text = "Transfered To Central Store";
            da = new SqlDataAdapter("SELECT ti3.recieved_cc,it3.quantity,it3.ref_no as [No],Convert(varchar(19),convert(datetime,ti3.recieved_date),106)as Date,'' as po_no,ti3.remarks,it3.item_code FROM [items transfer] it3 JOIN [transfer info]ti3 ON ti3.ref_no=it3.ref_no WHERE ti3.recieved_cc='CC-33' and ti3.cc_code='" + Request.QueryString["cccode"].ToString() + "' AND it3.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "TRANSFERTOOTHERCC")
        {
            lblheader.Text = "Transfer To Other CC";
            da = new SqlDataAdapter("SELECT ti4.recieved_cc,it4.quantity,it4.ref_no as [No],Convert(varchar(19),convert(datetime,ti4.recieved_date),106)as Date,'' as po_no,ti4.remarks,it4.item_code FROM [items transfer] it4 JOIN [transfer info]ti4 ON ti4.ref_no=it4.ref_no WHERE ti4.recieved_cc NOT IN ('CC-33')and ti4.recieved_cc NOT IN ('" + Request.QueryString["cccode"].ToString() + "') and ti4.cc_code='" + Request.QueryString["cccode"].ToString() + "' AND it4.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "LOSTANDDAMAGESREPORTACCPETED")
        {
            lblheader.Text = "Lost and Damage Report Accepted";
            da = new SqlDataAdapter("SELECT ldr.cc_code as recieved_cc,ldi.quantity,ldr.ref_no as [No],Convert(varchar(19),convert(datetime,ldr.date),106)as Date,'' as po_no,ldi.cmcremarks as remarks,ldi.item_code from [lost/damaged_items]ldi JOIN [lost/damaged report]ldr ON ldi.ref_no=ldr.ref_no WHERE ldr.status='3' AND ldi.cc_code='" + Request.QueryString["cccode"].ToString() + "' AND ldi.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }
        else if (Request.QueryString["For"].ToString() == "IssuedForCCConsumption")
        {
            lblheader.Text = "Issue For CC Consumption";
            da = new SqlDataAdapter("SELECT cc_code as recieved_cc,quantity,Transaction_id as [No],''as Date,'' as po_no,'' as remarks,item_code FROM [daily issued items]dii WHERE dii.cc_code='" + Request.QueryString["cccode"].ToString() + "' AND dii.item_code='" + Request.QueryString["id"].ToString() + "'", con);
        }

        da.Fill(ds);
        Gvdetails.DataSource = ds;
        Gvdetails.DataBind();
       
    }
    public void assets()
    {
        da = new SqlDataAdapter("select i.* from (SELECT ti.id,it.ref_no,it.item_code,remarks,REPLACE(REPLACE(Convert(varchar(19),convert(datetime,ti.recieved_date),106), ' ','-'), ',','')as RecievedDate,REPLACE(REPLACE(Convert(varchar(19),convert(datetime,ti.transfer_date),106), ' ','-'), ',','')as transfer_date from  [transfer info] ti JOIN  [items transfer] it ON ti.ref_no=it.ref_no where it.item_code='" + Request.QueryString["id"].ToString() + "' AND it.cc_code='" + Request.QueryString["cccode"].ToString() + "'	UNION	SELECT ti.id,it.ref_no,it.item_code,remarks,REPLACE(REPLACE(Convert(varchar(19),convert(datetime,ti.recieved_date),106), ' ','-'), ',','') as RecievedDate,REPLACE(REPLACE(Convert(varchar(19),convert(datetime,ti.transfer_date),106), ' ','-'), ',','')as transfer_date from  [transfer info] ti JOIN  [items transfer] it ON ti.ref_no=it.ref_no where it.item_code='" + Request.QueryString["id"].ToString() + "' AND ti.recieved_cc='" + Request.QueryString["cccode"].ToString() + "')i order by i.id asc", con);
        da.Fill(ds);
        Grdassets.DataSource = ds;
        Grdassets.DataBind();        
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {        
        Response.ClearContent();
        Response.Buffer = true;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", lblheader.Text + " Report "));
        Context.Response.Charset = "";
        // string headerrow = @" <table> <tr > <td align='center' colspan='10'><u><h2>CASH /BANK DEBIT DETAIL OF THE DAY<h2></u> </td> </tr></table>";
        string headerrow = @" <table><tr><td colspan='2'></td><td align='center' colspan='2'><h3> " + lblheader.Text + " DETAILVIEW REPORT" + " <h3></td><td align='left' colspan='4'></td></tr></table> ";
        string headerow3 = @" <table> <tr > <td align='center' colspan='10'> </td> </tr></table>";

        Response.Write(headerrow);
        //Response.Write(tablerow1);
        Response.Write(headerow3);
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        tbldetail.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
}