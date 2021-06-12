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

public partial class PendingApprovels : System.Web.UI.Page
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
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                fillgrid();
                fillMRRgrid();
                fillInvoicegrid();
                filltransfergrid();
                fillissuegrid();
                fillitemcodesgrid();
            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {
                fillgrid();
                fillMRRgrid();               
            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
                fillgrid();
                fillPOgrid();
                fillInvoicegrid();
                filltransfergrid();
                fillissuegrid();
                fillitemcodesgrid();
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
                Gridindent.DataSource = ds.Tables["indent"];
                Gridindent.DataBind();
            }
            else
                tblindent.Visible = false;                
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

        }
    }

    public void fillPOgrid()
    {
        try 
        {
            if (Session["roles"].ToString() == "Chief Material Controller")
              da=new SqlDataAdapter("select id,Po_no,cc_code,Remarks,Po_date,Indent_no from Purchase_details where Status='1'",con);
              da.Fill(ds, "popending");
             if (ds.Tables["popending"].Rows.Count > 0)
             {
                 GridPO.DataSource = ds.Tables["popending"];
                 GridPO.DataBind();
             }
             else
                 tblpo.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

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
                GridMrr.DataSource = ds.Tables["MRpending"];
                GridMrr.DataBind();
            }
            else
                tblmrr.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

        }
    }
    public void fillInvoicegrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select p.id,invoiceno,invoice_date,cc_code,Replace(isnull(total,0),'.0000','.00')total,remarks from pending_invoice p join mr_report m on m.po_no=p.po_no where m.status='5' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            else if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("select p.id,invoiceno,invoice_date,cc_code,Replace(isnull(total,0),'.0000','.00')total,remarks from pending_invoice p join mr_report m on m.po_no=p.po_no where m.status='6'", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select p.id,invoiceno,invoice_date,cc_code,Replace(isnull(total,0),'.0000','.00')total,remarks from pending_invoice p join mr_report m on m.po_no=p.po_no where m.status='7'", con);
                da.Fill(ds, "Invpending");
            if (ds.Tables["Invpending"].Rows.Count > 0)
            {
                GridInvoice.DataSource = ds.Tables["Invpending"];
                GridInvoice.DataBind();
            }
            else
                tblinv.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

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
                GridTransfer.DataSource = ds.Tables["transfer"];
                GridTransfer.DataBind();
            }
            else
                tbltransfer.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

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
                Gridissue.DataSource = ds1.Tables["pendingIssue"];
                Gridissue.DataBind();
            }
            else
                tblissue.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

        }
    }
    public void fillsppogrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select id,pono,cc_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks from SPPO where cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  status='1'", con);         
            da.Fill(ds, "SPPO");
            if (ds.Tables["SPPO"].Rows.Count > 0)
            {
                Gridsppo.DataSource = ds.Tables["SPPO"];
                Gridsppo.DataBind();
            }
            else
                tblsppo.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

        }
    }
    public void fillamendsppogrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select a.id,a.pono,cc_code,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as po_date,a.remarks from amend_sppo a join sppo sp on a.pono=sp.pono where cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  a.status='1'", con);
            da.Fill(ds, "ASPPO");
            if (ds.Tables["ASPPO"].Rows.Count > 0)
            {
                GridASppo.DataSource = ds.Tables["ASPPO"];
                GridASppo.DataBind();
            }
            else
                tblAsppo.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

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
                GridCSPPO.DataSource = ds.Tables["CSPPO"];
                GridCSPPO.DataBind();
            }
            else
                tblAsppo.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

        }
    }
    public void clientpo()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select contract_id,po_no,cc_code,CAST((isnull(po_totalvalue,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date from PO where cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            da.Fill(ds, "clientpo");
            if (ds.Tables["clientpo"].Rows.Count > 0)
            {
                grdiclientpo.DataSource = ds.Tables["clientpo"];
                grdiclientpo.DataBind();
            }
            else
                tblAsppo.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

        }
    }
    
    public void fillGeneralstock()
    {
        try
        {
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select id,request_no,cc_code,REPLACE(CONVERT(VARCHAR(11),Req_date, 106), ' ', '-')as date,description  from StockUpdation ", con);
            da.Fill(ds, "Geninv");
            if (ds.Tables["Geninv"].Rows.Count > 0)
            {
                GridStock.DataSource = ds.Tables["Geninv"];
                GridStock.DataBind();
            }
            else
                tblgeninv.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

        }
    }
    public void fillitemcodesgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("select id,item_code,item_name,basic_price,specification,Created_date as date  from item_codes where status='1'", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select id,item_code,item_name,basic_price,specification,Modified_date as date from item_codes where status in ('1','2')", con);
              da.Fill(ds, "itempending");
            if (ds.Tables["itempending"].Rows.Count > 0)
            {
                Griditems.DataSource = ds.Tables["itempending"];
                Griditems.DataBind();
            }
            else
                tblitem.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {

        }
    }

    protected void Gridindent_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("Indent.aspx");
    }

    protected void GridPO_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("VendorPO.aspx");
    }

    protected void GridMrr_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("MRReport.aspx");
    }
    protected void GridInvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("BasicPriceUpdationnewGST.aspx");
    }

    protected void GridTransfer_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("Transfer.aspx");
    }
    protected void Gridissue_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("Issue.aspx");
    }
    protected void Gridsppo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("verifysppo.aspx");
    }
    protected void GridStock_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("ViewStockUpdation.aspx");
    }
    protected void Griditems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("verifyitemcode.aspx");
    }
    protected void GridASppo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("AmendSPPO.aspx");
    }
    protected void GridCSPPO_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("ClosePO.aspx");
    }
    protected void grdiclientpo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("VerifyclientPO.aspx");
    }
}
