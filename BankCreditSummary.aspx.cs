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
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
public partial class BankCreditSummary : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["SummaryType"] == null))
            {
                JavaScript.CloseWindow();
            }
            else
            {
                filldata();
            }
        }


    }
    public void filldata()
    {
        da = new SqlDataAdapter("BankCreditAndDebitSummary_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@SummaryType", SqlDbType.VarChar).Value = Request.QueryString["Summarytype"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = Request.QueryString["Year"].ToString();      
        da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = Request.QueryString["Year1"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Prevyear", SqlDbType.VarChar).Value = Request.QueryString["PrevYear"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Prevyear1", SqlDbType.VarChar).Value = Request.QueryString["PrevYear1"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value ="1";
        da.Fill(ds, "filldata");
        if (ds.Tables["filldata"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["filldata"];
            GridView1.DataBind();
            ViewState["filldata"] = ds.Tables["filldata"];
        }
        else
        {
            JavaScript.CloseWindow();
        }

    }
  
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

          
            TotalCredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            e.Row.Cells[5].Text = TotalCredit.ToString();
         

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
      
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Credit Summary.xlsx"));
        Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        GridView1.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
        GridView1.DataBind();
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
        }
        int j = 1;
        //This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            gvrow.BackColor =System.Drawing.Color.White;
            if (j <= GridView1.Rows.Count)
            {
                if (j % 2 != 0)
                {
                    for (int k = 0; k < gvrow.Cells.Count; k++)
                    {
                        gvrow.Cells[k].Style.Add("background-color", "#FFFFFF");
                    }
                }
            }
            j++;
        }

        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnpdf_Click(object sender, ImageClickEventArgs e)
    {

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename="+Request.QueryString["Summarytype"].ToString()+" Bank Credit Summary.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
     
        //this.Page.RenderControl(hw);            
        string content = "";
        content = content + "<table cellspacing=0 cellpadding=5 rules=all border=1 style=border-collapse: collapse;><tr style=color: White; background-color: #DF5015; font-weight: bold;><th scope='col'>Invoice Date</th><th scope='col'>Recieved Date</th><th scope='col'>PO No</th><th scope='col'>Invoice No</th><th scope='col'>CC Code</th><th scope='col'>Amount</th></tr>";
        for (int i = 0; i < (ViewState["filldata"] as DataTable).Rows.Count; i++)
        {
            content = content + "<tr ><td >" + (ViewState["filldata"] as DataTable).Rows[i]["Invoice_Date"].ToString() + "</td><td>" + (ViewState["filldata"] as DataTable).Rows[i]["Date"].ToString() + "</td><td>" + (ViewState["filldata"] as DataTable).Rows[i]["po_no"].ToString() + "</td><td>" + (ViewState["filldata"] as DataTable).Rows[i]["InvoiceNo"].ToString() + "</td><td>" + (ViewState["filldata"] as DataTable).Rows[i]["CC_Code"].ToString() + "</td><td aign=right>" + (ViewState["filldata"] as DataTable).Rows[i]["Amount"].ToString() + "</td></tr>";
        }
        content = content + "</table>";
        divgrd.InnerHtml = content;
        divgrd.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString().Replace("\r", "").Replace("\n", "").Replace("  ", ""));
        Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0.0f);

        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
   
    }
   
    //public void CreatePDFFromHTMLFile(string HtmlStream, string FileName)
    //{
    //    try
    //    {
    //        object TargetFile = FileName;
    //        string ModifiedFileName = string.Empty;
    //        string FinalFileName = string.Empty;
    //        HTMLtoPDF.GeneratePDF.HtmlToPdfBuilder builder = new HTMLtoPDF.GeneratePDF.HtmlToPdfBuilder(iTextSharp.text.PageSize.A4);


    //        HTMLtoPDF.GeneratePDF.HtmlPdfPage first = builder.AddPage();
    //        first.AppendHtml(HtmlStream);
    //        byte[] file = builder.RenderPdf();
    //        System.IO.File.WriteAllBytes(TargetFile.ToString(), file);

    //        iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());


    //        //iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, "", "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
    //        //reader.Close();
    //        //if (File.Exists(TargetFile.ToString()))
    //        //    File.Delete(TargetFile.ToString());
    //        //FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
    //        //File.Copy(ModifiedFileName, FinalFileName);
    //        //if (File.Exists(ModifiedFileName))
    //        //    File.Delete(ModifiedFileName);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    private decimal TotalCredit = (decimal)0.0;
}