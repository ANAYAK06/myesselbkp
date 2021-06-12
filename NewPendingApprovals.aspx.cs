using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
public partial class NewPendingApprovals : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "StoreKeeper")
            {
                filltransfergrid();
                fillissuegrid();
                AccordionPane1.Visible = false;
                AccordionPane2.Visible = false;
                AccordionPane3.Visible = false;
                AccordionPane4.Visible = false;
                AccordionPane5.Visible = true;
                AccordionPane6.Visible = true;
                AccordionPane7.Visible = false;
                AccordionPane8.Visible = false;
                AccordionPane9.Visible = false;
                AccordionPane10.Visible = false;
                AccordionPane11.Visible = false;
                AccordionPane12.Visible = false;
                AccordionPane13.Visible = false;
                AccordionPane14.Visible = false;
                AccordionPane15.Visible = false;
                AccordSpInv4rAppr.Visible = false;
                AccordAssetSale.Visible = false;
                AccordHSNCode.Visible = false;

            }
            else if (Session["roles"].ToString() == "Project Manager")
            {
                fillgrid();
                fillMRRgrid();
                fillInvoicegrid();
                filltransfergrid();
                fillissuegrid();
                fillsppogrid();
                fillamendsppogrid();
                fillCCclosegrid();
                filllostdamagedgrid();
                clientpo();
                closesppogrid();
                FillSpInvoice();
                AccordionPane1.Visible = true;
                AccordionPane2.Visible = false;
                AccordionPane3.Visible = true;
                AccordionPane4.Visible = true;
                AccordionPane5.Visible = true;
                AccordionPane6.Visible = true;
                AccordionPane7.Visible = true;
                AccordionPane8.Visible = true;
                AccordionPane9.Visible = true;
                AccordionPane10.Visible = true;
                AccordionPane11.Visible = false;
                AccordionPane12.Visible = false;
                AccordionPane13.Visible = false;
                AccordionPane14.Visible = true;
                AccordionPane15.Visible = true;
                AccordSpInv4rAppr.Visible = true;
                AccordAssetSale.Visible = false;
                AccordHSNCode.Visible = false;
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                fillgrid();
                fillMRRgrid();
                fillInvoicegrid();
                filltransfergrid();
                fillissuegrid();
                fillitemcodesgrid();
                //filllostdamagedgrid();
                fillCCclosegrid();

                AccordionPane1.Visible = true;
                AccordionPane2.Visible = false;
                AccordionPane3.Visible = true;
                AccordionPane4.Visible = true;
                AccordionPane5.Visible = true;
                AccordionPane6.Visible = true;
                AccordionPane7.Visible = false;
                AccordionPane8.Visible = false;
                AccordionPane9.Visible = false;
                AccordionPane10.Visible = false;
                AccordionPane11.Visible = false;
                AccordionPane12.Visible = true;
                AccordionPane13.Visible = false;
                AccordionPane14.Visible = false;
                AccordionPane15.Visible = true;
                AccordSpInv4rAppr.Visible = false;
                AccordAssetSale.Visible = false;
                AccordHSNCode.Visible = false;
            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {
                fillgrid();
                fillMRRgrid();
                fillsppogrid();
                fillamendsppogrid();
                fillInvoicegrid();
                FillSpInvoice();
                //fillsaleitems();
                fillitemcodesgrid();
                Fillassetsale();
                Fillhsnsaccode();
                AccordionPane1.Visible = true;
                AccordionPane2.Visible = false;
                AccordionPane3.Visible = true;
                AccordionPane4.Visible = true;
                AccordionPane5.Visible = false;
                AccordionPane6.Visible = false;
                AccordionPane7.Visible = true;
                AccordionPane8.Visible = true;
                AccordionPane9.Visible = false;
                AccordionPane10.Visible = false;
                AccordionPane11.Visible = false;
                AccordionPane12.Visible = true;
                AccordionPane13.Visible = false;
                AccordionPane14.Visible = false;
                AccordionPane15.Visible=false;
                AccordSpInv4rAppr.Visible = true;
                AccordAssetSale.Visible = true;
                AccordHSNCode.Visible = true;
            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
                fillgrid();
                fillPOgrid();
                fillInvoicegrid();
                filltransfergrid();
                fillissuegrid();
                fillitemcodesgrid();
                filllostdamagedgrid();
                fillCCclosegrid();
               // fillsaleitems();
                fillGeneralstock();
                Fillassetsale();
                fillsppogrid();
                fillamendsppogrid();
                FillSpInvoice();
                Fillhsnsaccode();
                AccordionPane1.Visible = true;
                AccordionPane2.Visible = true;
                AccordionPane3.Visible = false;
                AccordionPane4.Visible = true;
                AccordionPane5.Visible = true;
                AccordionPane6.Visible = true;
                AccordionPane7.Visible = true;
                AccordionPane8.Visible = true;
                AccordionPane9.Visible = false;
                AccordionPane10.Visible = false;
                AccordionPane11.Visible = true;
                AccordionPane12.Visible = true;
                AccordionPane13.Visible = false;
                AccordionPane14.Visible = true;
                AccordionPane15.Visible = true;
                AccordSpInv4rAppr.Visible = true;
                AccordAssetSale.Visible = true;
                AccordHSNCode.Visible = true;
            }       
        }
    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.Pmremarks, CHARINDEX('$', i.Pmremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status] from indents i where (i.status='2') order by i.status asc", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.purmremark, CHARINDEX('$', i.purmremark) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status] from indents i where (i.status='5')  order by i.status asc", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT( i.Cskremarks, CHARINDEX('$',  i.Cskremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status] from indents i where (i.status in ('4','3')) order by i.status asc", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.Remarks, CHARINDEX('$', i.Remarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status  as [Status]from indents i where i.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and (i.status='1') order by i.status asc", con);
            da.Fill(ds, "indent");
            if (ds.Tables["indent"].Rows.Count > 0)
            {
                indenttotal.Text = "(" + ds.Tables["indent"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["indent"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='indent.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["indent"].Rows[i]["Indent No"].ToString() + "</td><td>" + ds.Tables["indent"].Rows[i]["CC Code"].ToString() + "</td><td>" + ds.Tables["indent"].Rows[i]["Indent Date"].ToString() + "</td><td>" + ds.Tables["indent"].Rows[i]["Indent Cost"].ToString() + "</td><td>" + ds.Tables["indent"].Rows[i]["Description"].ToString() + "</td></tr>");
                }
                indenttbody.InnerHtml = sb.ToString();
            }
            else
            {
                indenttotal.Text = "( 0  Pendings)";
                tblIndents.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblindent";
                lblmsg.Text = "There is no records found";
                Pnlindents.Controls.Add(lblmsg);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }      
    }
    public void fillPOgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select id,Po_no,cc_code,Remarks,Po_date,Indent_no from Purchase_details where Status='1'", con);
            da.Fill(ds, "popending");
            if (ds.Tables["popending"].Rows.Count > 0)
            {
                pototal.Text = "(" + ds.Tables["popending"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["popending"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='VendorPO.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["popending"].Rows[i]["Po_no"].ToString() + "</td><td>" + ds.Tables["popending"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["popending"].Rows[i]["Indent_no"].ToString() + "</td><td>" + ds.Tables["popending"].Rows[i]["Po_date"].ToString() + "</td><td>" + ds.Tables["popending"].Rows[i]["Remarks"].ToString() + "</td></tr>");
                }
                Pobody.InnerHtml = sb.ToString();
            }
            else
            {
                pototal.Text = "( 0  Pendings)"; 
                tblpos.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblpo";
                lblmsg.Text = "There is no records found";
                pnlpos.Controls.Add(lblmsg);

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }     
    }
    public void fillMRRgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select m.id,mrr_no,m.po_no,m.recieved_date,m.Remarks From mr_report m join Purchase_details p on p.Po_no=m.PO_no where m.Status='1' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            else if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("select id,mrr_no,po_no,recieved_date,remarks From mr_report where status='2'", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select id,mrr_no,po_no,recieved_date,remarks From mr_report where status='3'", con);
            da.Fill(ds, "MRpending");
            if (ds.Tables["MRpending"].Rows.Count > 0)
            {
                Mrrtotal.Text = "(" + ds.Tables["MRpending"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["MRpending"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='MRReport.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["MRpending"].Rows[i]["mrr_no"].ToString() + "</td><td>" + ds.Tables["MRpending"].Rows[i]["po_no"].ToString() + "</td><td>" + ds.Tables["MRpending"].Rows[i]["recieved_date"].ToString() + "</td><td>" + ds.Tables["MRpending"].Rows[i]["remarks"].ToString() + "</td></tr>");
                }
                Mrrbody.InnerHtml = sb.ToString();
            }     
            else
            {
                Mrrtotal.Text = "( 0  Pendings)";
                tblmrr.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblmrr";
                lblmsg.Text = "There is no records found";
                pnlmrr.Controls.Add(lblmsg);
            }
     
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }        
    }
    public void fillInvoicegrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select p.id,invoiceno,invoice_date,cc_code,Replace(isnull(total,0),'.0000','.00')total,remarks from pending_invoice p join mr_report m on m.po_no=p.po_no where m.status='5' and p.paymenttype='Supplier' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            else if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("select p.id,invoiceno,invoice_date,cc_code,Replace(isnull(total,0),'.0000','.00')total,remarks from pending_invoice p join mr_report m on m.po_no=p.po_no where m.status='6' and p.paymenttype='Supplier'", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select p.id,invoiceno,invoice_date,cc_code,Replace(isnull(total,0),'.0000','.00')total,remarks from pending_invoice p join mr_report m on m.po_no=p.po_no where m.status='6A' and p.paymenttype='Supplier'", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select p.id,invoiceno,invoice_date,cc_code,Replace(isnull(total,0),'.0000','.00')total,remarks from pending_invoice p join mr_report m on m.po_no=p.po_no where (m.status in('6B')) and p.paymenttype='Supplier'", con);
            da.Fill(ds, "Invpending");
            if (ds.Tables["Invpending"].Rows.Count > 0)
            {
                invtotal.Text = "(" + ds.Tables["Invpending"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["Invpending"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='BasicPriceUpdationnewGST.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["Invpending"].Rows[i]["invoiceno"].ToString() + "</td><td>" + ds.Tables["Invpending"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["Invpending"].Rows[i]["total"].ToString() + "</td><td>" + ds.Tables["Invpending"].Rows[i]["invoice_date"].ToString() + "</td><td>" + ds.Tables["Invpending"].Rows[i]["remarks"].ToString() + "</td></tr>");
                }
                invoicebody.InnerHtml = sb.ToString();
            }            
            else
            {
                invtotal.Text = "( 0  Pendings)";
                tblsinvoice.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblSinvoice";
                lblmsg.Text = "There is no records found";
                pnlsinvoice.Controls.Add(lblmsg);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }        
    }
    public void filltransfergrid()
    {
        try
        {
            if (Session["roles"].ToString() == "StoreKeeper")
                da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where  Ti.cc_code='" + Session["cc_code"].ToString() + "' and type='2' and  status='2' union  select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info]TI join [Items Transfer]IT  on TI.ref_no=IT.ref_no   where  recieved_cc='" + Session["cc_code"].ToString() + "' and type='2' and status='4'  order by TI.status asc ", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select distinct i.* from(select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   TI.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and type='2' and (status='3') union select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   recieved_cc in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and type='2' and  (status='5'))i order by i.status asc ", con);
            else if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and status in ('1','3A') order by TI.status asc ", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no   where type='2' and status in ('1A','3B')  order by TI.status asc ", con);
            da.Fill(ds, "transfer");
            if (ds.Tables["transfer"].Rows.Count > 0)
            {
                transfertotal.Text = "(" + ds.Tables["transfer"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["transfer"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='Transfer.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["Transfer"].Rows[i]["ref_no"].ToString() + "</td><td>" + ds.Tables["Transfer"].Rows[i]["Transfer Out"].ToString() + "</td><td>" + ds.Tables["Transfer"].Rows[i]["Transfer In"].ToString() + "</td><td>" + ds.Tables["Transfer"].Rows[i]["transfer_date"].ToString() + "</td><td>" + ds.Tables["Transfer"].Rows[i]["Remarks"].ToString() + "</td></tr>");
                }
                transferbody.InnerHtml = sb.ToString();
            }           
            else
            {
                transfertotal.Text = "( 0  Pendings)";
                tblstransfer.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lbltransfer";
                lblmsg.Text = "There is no records found";
                pnltransfer.Controls.Add(lblmsg);
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }       
    }
    public void fillissuegrid()
    {
        try
        {
            if (Session["roles"].ToString() == "StoreKeeper")
                da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info]TI join [Items Transfer]IT  on TI.ref_no=IT.ref_no   where  recieved_cc='" + Session["cc_code"].ToString() + "' and type='1' and status='2'  order by TI.status asc ", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   recieved_cc in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and type='1' and  (status='3')  order by status asc ", con);
            else if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='1' and status in ('1') order by TI.status asc ", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no   where type='1' and status in ('1A')  order by TI.status asc ", con);
            da.Fill(ds1, "pendingIssue");
            if (ds1.Tables["pendingIssue"].Rows.Count > 0)
            {
                issuetotal.Text = "(" + ds1.Tables["pendingIssue"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds1.Tables["pendingIssue"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='Issue.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds1.Tables["pendingIssue"].Rows[i]["ref_no"].ToString() + "</td><td>" + ds1.Tables["pendingIssue"].Rows[i]["Transfer Out"].ToString() + "</td><td>" + ds1.Tables["pendingIssue"].Rows[i]["Transfer In"].ToString() + "</td><td>" + ds1.Tables["pendingIssue"].Rows[i]["transfer_date"].ToString() + "</td><td>" + ds1.Tables["pendingIssue"].Rows[i]["remarks"].ToString() + "</td></tr>");
                }
                Issuebody.InnerHtml = sb.ToString();
            }           
            else
            {
                issuetotal.Text = "( 0  Pendings)";
                tblissue.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblissue";
                lblmsg.Text = "There is no records found";
                pnlissue.Controls.Add(lblmsg);

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }      
    }
    public void fillsppogrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select id,pono,cc_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  status='1'", con);
            if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select id,pono,cc_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where status in ('1P','1NP')", con);
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select id,pono,cc_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where status in ('1A')", con);

            da.Fill(ds, "SPPO");
            if (ds.Tables["SPPO"].Rows.Count > 0)
            {
                sppototal.Text = "(" + ds.Tables["SPPO"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["SPPO"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='verifysppo.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["SPPO"].Rows[i]["pono"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["po_value"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["po_date"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["Remarks"].ToString() + "</td></tr>");
                }
                SPPObody.InnerHtml = sb.ToString();
            }
            else
            {
                sppototal.Text = "( 0  Pendings)";
                tblsppo.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblsppo";
                lblmsg.Text = "There is no records found";
                pnlsppo.Controls.Add(lblmsg);
            }
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }      
    }
    public void fillamendsppogrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select a.id,a.pono,cc_code,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as po_date,a.remarks from amend_sppo a join sppo sp on a.pono=sp.pono where cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  a.status='1'", con);
            if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select a.id,a.pono,cc_code,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as po_date,a.remarks from amend_sppo a join sppo sp on a.pono=sp.pono where a.status in ('1A')", con);
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select a.id,a.pono,cc_code,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as po_date,a.remarks from amend_sppo a join sppo sp on a.pono=sp.pono where a.status in ('1B')", con);
            da.Fill(ds, "ASPPO");
            if (ds.Tables["ASPPO"].Rows.Count > 0)
            {
                AMSPPOtotal.Text = "(" + ds.Tables["ASPPO"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["ASPPO"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='AmendSPPO.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["ASPPO"].Rows[i]["pono"].ToString() + "</td><td>" + ds.Tables["ASPPO"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["ASPPO"].Rows[i]["po_value"].ToString() + "</td><td>" + ds.Tables["ASPPO"].Rows[i]["po_date"].ToString() + "</td><td>" + ds.Tables["ASPPO"].Rows[i]["Remarks"].ToString() + "</td></tr>");
                }
                ASPPObody.InnerHtml = sb.ToString();
            }          
            else
            {
                AMSPPOtotal.Text = "( 0  Pendings)";
                tblamendsppo.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblAmendsppo";
                lblmsg.Text = "There is no records found";
                pnlamndsppo.Controls.Add(lblmsg);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void closesppogrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select id,pono,cc_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where status='closed' and balance!='0' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            da.Fill(ds, "CSPPO");
            if (ds.Tables["CSPPO"].Rows.Count > 0)
            {
                clsppototal.Text = "(" + ds.Tables["CSPPO"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["CSPPO"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='ClosePO.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["CSPPO"].Rows[i]["pono"].ToString() + "</td><td>" + ds.Tables["CSPPO"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["CSPPO"].Rows[i]["po_value"].ToString() + "</td><td>" + ds.Tables["CSPPO"].Rows[i]["po_date"].ToString() + "</td><td>" + ds.Tables["CSPPO"].Rows[i]["Remarks"].ToString() + "</td></tr>");
                }
                ClSPPObody.InnerHtml = sb.ToString();
            }           
            else
            {
                clsppototal.Text = "( 0  Pendings)";
                tblclsppo.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblclosepo";
                lblmsg.Text = "There is no records found";
                pnlcsppo.Controls.Add(lblmsg);
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }      
    }
    public void clientpo()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select contract_id,po_no,cc_code,CAST((isnull(po_totalvalue,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date from PO where cc_code in (Select cc_code from cc_user where status='1' and user_name='" + Session["user"].ToString() + "')", con);
            da.Fill(ds, "clientpo");
            if (ds.Tables["clientpo"].Rows.Count > 0)
            {
                clientpototal.Text = "(" + ds.Tables["clientpo"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["clientpo"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='VerifyclientPO.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["clientpo"].Rows[i]["po_no"].ToString() + "</td><td>" + ds.Tables["clientpo"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["clientpo"].Rows[i]["po_value"].ToString() + "</td><td>" + ds.Tables["clientpo"].Rows[i]["po_date"].ToString() + "</td><td>");
                }
                ClientPObody.InnerHtml = sb.ToString();
            }          
            else
            {
                clientpototal.Text = "( 0  Pendings)";
                tblclpo.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblclientpo";
                lblmsg.Text = "There is no records found";
                pnlClpo.Controls.Add(lblmsg);
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }        
    }
    public void fillGeneralstock()
    {
        try
        {
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select id,request_no,cc_code,REPLACE(CONVERT(VARCHAR(11),Req_date, 106), ' ', '-')as date,description  from StockUpdation  where status in ('1') ", con);
            da.Fill(ds, "Geninv");
            if (ds.Tables["Geninv"].Rows.Count > 0)
            {
                stocktotal.Text = "(" + ds.Tables["Geninv"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["Geninv"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='ViewStockUpdation.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["Geninv"].Rows[i]["request_no"].ToString() + "</td><td>" + ds.Tables["Geninv"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["Geninv"].Rows[i]["date"].ToString() + "</td><td>" + ds.Tables["Geninv"].Rows[i]["description"].ToString() + "</td><td>");
                }
                stockbody.InnerHtml = sb.ToString();
            }       
            else
            {
                stocktotal.Text = "( 0  Pendings)";
                tblstock.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblstock";
                lblmsg.Text = "There is no records found";
                pnlstock.Controls.Add(lblmsg);
            }

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }       
    }
    public void fillitemcodesgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("select id,item_code,item_name,basic_price,specification,Created_date as date  from item_codes where status='1'", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select id,item_code,item_name,basic_price,specification,Modified_date as date from item_codes where status in ('A1','A2')", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select id,item_code,item_name,basic_price,specification,Modified_date as date from item_codes where status in ('1','2')", con);
            da.Fill(ds, "itempending");
            if (ds.Tables["itempending"].Rows.Count > 0)
            {
                itemtotal.Text = "(" + ds.Tables["itempending"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["itempending"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='verifyitemcode.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["itempending"].Rows[i]["item_code"].ToString() + "</td><td>" + ds.Tables["itempending"].Rows[i]["item_name"].ToString() + "</td><td>" + ds.Tables["itempending"].Rows[i]["basic_price"].ToString() + "</td><td>" + ds.Tables["itempending"].Rows[i]["date"].ToString() + "</td><td>" + ds.Tables["itempending"].Rows[i]["specification"].ToString() + "</td><td>");
                }
                itemcodebody.InnerHtml = sb.ToString();
            }           
            else
            {
                itemtotal.Text = "( 0  Pendings)";
                tblitem.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblitem";
                lblmsg.Text = "There is no records found";
                pnlitem.Controls.Add(lblmsg);
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void filllostdamagedgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("SELECT  id,Date,Ref_no,cc_code from [lost/Damaged Report] where (status='1')  and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("SELECT id,Date,Ref_no,cc_code from [lost/Damaged Report] where (status='2') ", con);
            da.Fill(ds, "lostdamage");
            if (ds.Tables["lostdamage"].Rows.Count > 0)
            {
                 losttotal.Text = "(" + ds.Tables["lostdamage"].Rows.Count + "   Pendings )";
                 
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["lostdamage"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='ViewLostDamagedReport.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["lostdamage"].Rows[i]["Ref_no"].ToString() + "</td><td>" + ds.Tables["lostdamage"].Rows[i]["Date"].ToString() + "</td><td>" + ds.Tables["lostdamage"].Rows[i]["cc_code"].ToString() + "</td><td>");
                }
                Tlostdamage.InnerHtml = sb.ToString();
            }           
             else
             {
                 losttotal.Text = "( 0  Pendings)";
                 tbllost.Visible = false;
                 Label lblmsg = new Label();
                 lblmsg.ID = "lbllost";
                 lblmsg.Text = "There is no records found";
                 pnllost.Controls.Add(lblmsg);
             }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void fillCCclosegrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                da = new SqlDataAdapter("SELECT Id,CC_Code,REPLACE(CONVERT(NVARCHAR,Date, 106), ' ', '-')as Date,SK_Desc as remarks,close_type FROM ClosedCost_Center where status='1' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                da = new SqlDataAdapter("SELECT Id,CC_Code,REPLACE(CONVERT(NVARCHAR,Date, 106), ' ', '-')as Date,pm_Desc as remarks,close_type FROM ClosedCost_Center where status='2'", con);
            }
            else
            {
                da = new SqlDataAdapter("SELECT Id,CC_Code,REPLACE(CONVERT(NVARCHAR,Date, 106), ' ', '-')as Date,CSK_Desc as remarks,close_type FROM ClosedCost_Center where status='3' and Close_Type='PermanentClosed'", con);
            }
            da.Fill(ds, "CC_close");
            if (ds.Tables["CC_close"].Rows.Count > 0)
            {
                storeclosetotal.Text = "(" + ds.Tables["CC_close"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["CC_close"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='SiteStoreClosed.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["CC_close"].Rows[i]["Date"].ToString() + "</td><td>" + ds.Tables["CC_close"].Rows[i]["Date"].ToString() + "</td><td>" + ds.Tables["CC_close"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["CC_close"].Rows[i]["Remarks"].ToString() + "</td><td>" + ds.Tables["CC_close"].Rows[i]["close_type"].ToString() + "</td><td>");
                }
                TStoreclose.InnerHtml = sb.ToString();
            }
            else
            {
                storeclosetotal.Text = "(0  Pendings )";
                tblstore.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblstore";
                lblmsg.Text = "There is no records found";
                pnlstoreclose.Controls.Add(lblmsg);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void fillsaleitems()
    {
        try
        {
            if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("Select InvoiceNo,Created_date as InvoiceDate,PartyName,Remarks,Amount  from SoldItemsInfo where Status='1' and Amount!=0  order by id asc", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("Select InvoiceNo,Created_date as InvoiceDate,PartyName,Remarks,Amount  from SoldItemsInfo where Status='2' and Amount!=0 order by id asc", con);
            da.Fill(ds, "Scrapdata");
            if (ds.Tables["Scrapdata"].Rows.Count > 0)
            {
                scraptotal.Text = "(" + ds.Tables["Scrapdata"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["Scrapdata"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='ApproveSaleOfScrapItems.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["Scrapdata"].Rows[i]["InvoiceNo"].ToString() + "</td><td>" + ds.Tables["Scrapdata"].Rows[i]["Invoicedate"].ToString() + "</td><td>" + ds.Tables["Scrapdata"].Rows[i]["PartyName"].ToString() + "</td><td>" + ds.Tables["Scrapdata"].Rows[i]["Remarks"].ToString() + "</td><td>" + ds.Tables["Scrapdata"].Rows[i]["Amount"].ToString() + "</td><td>");
                }
                Scrapbody.InnerHtml = sb.ToString();
            }
            else
            {
                scraptotal.Text = "(0  pendings)";
                tblscrap.Visible = false;
                Label lblmsg = new Label();
                lblmsg.ID = "lblscrap";
                lblmsg.Text = "There is no records found";
                pnlscrapitems.Controls.Add(lblmsg);
            }           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void FillSpInvoice()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount from pending_invoice where status='A0' and paymenttype in ('Service Provider' ) and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') order by id desc", con);
            if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount from pending_invoice where status='A1' and paymenttype in ('Service Provider' ) order by id desc", con);
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount from pending_invoice where status='A2' and paymenttype in ('Service Provider' ) order by id desc", con);
             da.Fill(ds, "pending_invoice");
            if (ds.Tables["pending_invoice"].Rows.Count > 0)
            {
                lblSpInv4rApproval.Text = "(" + ds.Tables["pending_invoice"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["pending_invoice"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='InvoiceVerficationNew.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["pending_invoice"].Rows[i]["invoiceno"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["dca_code"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["Date"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["netamount"].ToString() + "</td>");

                }
                TbodySpInv4rAppr.InnerHtml = sb.ToString();
                //hlnkSpInv4rAppr.Visible = true;
            }
            else
            {
                lblSpInv4rApproval.ForeColor = System.Drawing.Color.GreenYellow;
                lblSpInv4rApproval.Text = "(0   Pendings )";
                //hlnkSpInv4rAppr.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void Fillassetsale()
    {
        try
        {
            if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select Id,CodeCategory,CodeType,HSN_SAC_Code,HSNCategory,Remarks,CGSTRate,SGSTRate,IGSTRate,Remarks from HSN_SAC_Codes where Status='1' order by id desc", con);
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select Id,CodeCategory,CodeType,HSN_SAC_Code,HSNCategory,Remarks,CGSTRate,SGSTRate,IGSTRate,Remarks from HSN_SAC_Codes where Status='2' order by id desc", con);
            da.Fill(ds, "hsncode");
            if (ds.Tables["hsncode"].Rows.Count > 0)
            {
                lblhsnapproval.Text = "(" + ds.Tables["hsncode"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["hsncode"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='VerifyHSNSACCode.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["hsncode"].Rows[i]["CodeCategory"].ToString() + "</td><td>" + ds.Tables["hsncode"].Rows[i]["HSN_SAC_Code"].ToString() + "</td><td>" + ds.Tables["hsncode"].Rows[i]["Remarks"].ToString() + "</td>");

                }
                Tbodyhsnsaccode.InnerHtml = sb.ToString();
                //hlnkSpInv4rAppr.Visible = true;
            }
            else
            {
                lblhsnapproval.ForeColor = System.Drawing.Color.GreenYellow;
                lblhsnapproval.Text = "(0   Pendings )";
                //hlnkSpInv4rAppr.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void Fillhsnsaccode()
    {
        try
        {
            if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select Request_No,Item_code,Buyer_Name,REPLACE(CONVERT(VARCHAR(11),BookValue_Date, 106), ' ', '-')as BookValue_Date,cast(selling_amt as decimal(20,2))as Selling_Amt from asset_sale where status='1' order by id desc", con);
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select Request_No,Item_code,Buyer_Name,REPLACE(CONVERT(VARCHAR(11),BookValue_Date, 106), ' ', '-')as BookValue_Date,cast(selling_amt as decimal(20,2))as Selling_Amt from asset_sale where status='2' order by id desc", con);
            da.Fill(ds, "assetsale");
            if (ds.Tables["assetsale"].Rows.Count > 0)
            {
                lblassetsale.Text = "(" + ds.Tables["assetsale"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["assetsale"].Rows.Count; i++)
                {
                    sb.Append("<tr><td><a href='VerifyAssetSale.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand' /></a></td><td>" + ds.Tables["assetsale"].Rows[i]["Request_No"].ToString() + "</td><td>" + ds.Tables["assetsale"].Rows[i]["Item_code"].ToString() + "</td><td>" + ds.Tables["assetsale"].Rows[i]["Buyer_Name"].ToString() + "</td><td>" + ds.Tables["assetsale"].Rows[i]["BookValue_Date"].ToString() + "</td><td>" + ds.Tables["assetsale"].Rows[i]["Selling_Amt"].ToString() + "</td>");

                }
                TbodyAccordAssetSale.InnerHtml = sb.ToString();
                //hlnkSpInv4rAppr.Visible = true;
            }
            else
            {
                lblassetsale.ForeColor = System.Drawing.Color.GreenYellow;
                lblassetsale.Text = "(0   Pendings )";
                //hlnkSpInv4rAppr.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
}