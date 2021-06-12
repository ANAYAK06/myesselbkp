using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

public partial class ViewClientInvoiceDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["Invoiceno"] == null))
            {
                JavaScript.CloseWindow();
            }
            else
            {
                try
                {
                    grid();
                    grid1();
                    Label1.Visible = false;

                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    public void grid()
    {
        da = new SqlDataAdapter("SELECT ct.DCA_Code AS [DCA Code],ct.SubDCA_Code AS [SDCA Code],CASE WHEN ct.ISCreditableTax=1 THEN 'Creditable' ELSE 'Non-Creditable' END AS Tax,ct.TaxType,ct.ITCode,CAST(ct.TaxValue AS DECIMAL(16,2))AS TaxValue FROM Client_Taxes ct WHERE ct.InvoiceNo='" + Request.QueryString["Invoiceno"].ToString() + "'", con);
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            gvclienttaxes.DataSource = ds.Tables["data"];
            gvclienttaxes.DataBind();

        }
        else
        {
            gridprint.Visible = false;
            trexcel.Visible = false;
            btnprint.Visible = false;
        }

    }
    public void grid1()
    {
        da = new SqlDataAdapter("SELECT CC_Code AS [CostCenter],DCA_Code AS [DCA Code],SubDCA_Code AS [SDCA Code],CAST(cd.Deducation_Value AS DECIMAL(16,2))AS [Deduction Value] FROM Client_Deducations cd WHERE cd.InvoiceNo='" + Request.QueryString["Invoiceno"].ToString() + "' order by id desc", con);
        da.Fill(ds, "data1");
        if (ds.Tables["data1"].Rows.Count > 0)
        {
            gvclientdeduction.DataSource = ds.Tables["data1"];
            gvclientdeduction.DataBind();
            Label1.Visible = true;
        }
        else
        {
            Label1.Visible = false;
        }

    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        grid();
        grid1();
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Client Invoice Detail View Invoice No " + Request.QueryString["Invoiceno"].ToString() + ""));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        print.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }
    private decimal ctaxes = (decimal)0.0;
    private decimal cded = (decimal)0.0;
    protected void gvclienttaxes_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            ctaxes += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxValue"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].Text = "Total";
            e.Row.Cells[5].Text = String.Format("{0:#,##,##,###.00}", ctaxes);
            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        }

    }
    protected void gvclientdeduction_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == Request.QueryString["ccode"].ToString())
            {
                
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FADBD8");
            }
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            cded += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Deduction Value"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total";
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].Text = String.Format("{0:#,##,##,###.00}", cded);
            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        }

    }
}