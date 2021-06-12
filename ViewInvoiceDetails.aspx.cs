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

public partial class ViewInvoiceDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();  
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (Request.QueryString["transactionno"] == null)
        {
            JavaScript.CloseWindow();
        }
        else
        {
            LoadReport();
        }
    }
    public void LoadReport()
    {
        try
        {
            da = new SqlDataAdapter("select DISTINCT PaymentType from BankBook where Transaction_No='" + Request.QueryString["transactionno"].ToString() + "'", con);
            da.Fill(ds, "Typecheck");
            if (ds.Tables["Typecheck"].Rows[0].ItemArray[0].ToString() != "TDS")
            {
                grd.Visible = true;
                gvtds.Visible = false;
                da = new SqlDataAdapter("Select b.invoiceno,p.cc_code,p.dca_code,p.vendor_id,p.total,p.tds,p.retention,p.netamount,p.hold,b.debit from pending_invoice p join bankbook b on p.invoiceno=b.invoiceno where b.Transaction_No='" + Request.QueryString["transactionno"].ToString() + "'", con);
                da.Fill(ds, "fillgrid");
                grd.DataSource = ds.Tables["fillgrid"];
                grd.DataBind();
            }
            else
            {
                grd.Visible = false;
                gvtds.Visible = true;
                da = new SqlDataAdapter("Select p.Po_No,b.invoiceno,p.cc_code,p.dca_code,p.Subdca_Code,v.vendor_id,p.Amount,b.Debit as DebitAmount from Vendor_Taxes p join bankbook b on p.invoiceno=b.invoiceno join pending_invoice v on v.InvoiceNo=p.InvoiceNo where b.Transaction_No='" + Request.QueryString["transactionno"].ToString() + "' and  p.Tax_Type='Deduction' ORDER by b.id asc", con);
                da.Fill(ds, "fillgridtds");
                gvtds.DataSource = ds.Tables["fillgridtds"];
                gvtds.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    private decimal Amt = (decimal)0.0;
    private decimal DAmt = (decimal)0.0;  
    protected void gvtds_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            DAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DebitAmount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = String.Format("{0:0.00}", Amt);
            e.Row.Cells[7].Text = String.Format("{0:0.00}", DAmt);
           
        }
    }
}
