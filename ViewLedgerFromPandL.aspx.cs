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
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;
using System.Web.Services;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
public partial class ViewLedgerFromPandL : System.Web.UI.Page
{
     SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
        {
          
            GetLedger(Request.QueryString["LedgerID"],Request.QueryString["Year"],Request.QueryString["Year1"]);
            trexcel.Visible = false;
        }
    }
    [WebMethod]
    public static string GetTransactiondetails(string Ledgertype, string LedgerName, string RecordedLedgerName)
    {
        StringBuilder sb = new StringBuilder();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]))
        {
            using (SqlCommand SqlCmd = new SqlCommand("GetLedgertransactiondetails", con))
            {
                SqlCmd.Connection = con;
                con.Open();
                SqlCmd.CommandType = CommandType.StoredProcedure;
                string[] items = RecordedLedgerName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SqlCmd.Parameters.AddWithValue("@VoucherNO", SqlDbType.VarChar).Value = items[1].Trim();
                SqlCmd.Parameters.AddWithValue("@ledgertype", SqlDbType.VarChar).Value = Ledgertype;
                SqlCmd.Parameters.AddWithValue("@LedgeId", SqlDbType.VarChar).Value = LedgerName;
                SqlCmd.Parameters.AddWithValue("@LedgerName", SqlDbType.VarChar).Value = items[0].Trim();
                SqlCmd.Parameters.AddWithValue("@IsCredit", SqlDbType.Bit).Value = items[2].Trim();
                SqlCmd.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = HttpContext.Current.Session["FYYear"].ToString();
                DataSet Objds = new DataSet();
                var adapter = new SqlDataAdapter(SqlCmd);

                adapter.Fill(Objds);


                for (int k = 0; k < Objds.Tables.Count; k++)
                {

                    if (Objds.Tables[k].Rows.Count > 0)
                    {

                        DataTable Objdt = Objds.Tables[k];
                        sb.Append("<table  class='display' style='border-collapse: collapse;'><tr>");
                        for (int i = 0; i < Objdt.Columns.Count; i++)
                        {
                            sb.Append("<td style='border: 1px solid black;' align='center'>" + Objdt.Columns[i].ColumnName + "</td>");
                        }

                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        for (int j = 0; j < Objdt.Columns.Count; j++)
                        {
                            sb.Append("<td style='border: 1px solid black;'>" + Objdt.Rows[0][j] + "</td>");

                        }
                        sb.Append("</tr></table>");
                    }
                }
                con.Close();
            }
        }
        return sb.ToString();
    }
      private decimal Credit = (decimal)0.0;
    private decimal Debit = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
    public string From;
    public string To;

    public void GetLedger(string LedgerID, string Year, string Year1)
    {
        da = new SqlDataAdapter("ViewLedger", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@LedgeId", SqlDbType.VarChar).Value = LedgerID;
        da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = Year;
        da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = Year1;
        da.SelectCommand.Parameters.AddWithValue("@FromDate", SqlDbType.DateTime).Value = "01-Apr-"+Year;
        da.SelectCommand.Parameters.AddWithValue("@ToDate", SqlDbType.DateTime).Value = "31-Mar-" + Year1;
        da.Fill(ds, "Viewledger");
        if (ds.Tables["Viewledger"].Rows.Count > 0)
        {
            From = "01-Apr-" + Year;
            To = "31-Mar-" + Year1;
            trexcel.Visible = true;
            string Ledgertype = "";
            string Bal = string.Empty;
           da = new SqlDataAdapter("select Name,id,ledger_type from ledger where status='3' and Id='"+LedgerID+"'", con);
            da.Fill(ds, "ledgernames");
            if (ds.Tables["ledgernames"].Rows.Count > 0)
            {
                Ledgertype = ds.Tables["ledgernames"].Rows[0].ItemArray[2].ToString();
                hf.Value = Ledgertype;
                hfledgername.Value = ds.Tables["ledgernames"].Rows[0].ItemArray[0].ToString();
            }



            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='example' class='display' style='width: 100%'><thead><tr><td colspan='7' align='center'><span style='font-size:xx-large'>ESSEL Projects Pvt Ltd</span></td></tr><tr><td colspan='7' align='center'><span style='font-size:x-large'>" + hfledgername.Value + "</span></td></tr><tr><td align='left' colspan='7'><span style='font-size:larger'>From: 04/01/" + From + "   <br>To:       03/31/" + To + "</span></td></tr><tr><th></th><th>Date</th><th>Particulars</th><th>V.No</th><th>Debit</th><th>Credit</th><th>Balance Rs.</th>");
            sb.Append("<tbody>");
            StringBuilder Sbpdf = new StringBuilder();
            Sbpdf = LedgerInPDF();
            //string text = System.IO.File.ReadAllText(@"D:\KK\NewApplicationAfterGST\SL\App_Data\Ledger_Format.HTML");

            string Path = Server.MapPath("~/App_Data/Ledger_Format.html");

            string text = System.IO.File.ReadAllText(Path);
            int[] Count = { 20, 40, 60, 80, 100, 120, 140, 160, 180, 200, 220, 240, 260, 280, 300 };
          
           


            for (int i = 0; i < ds.Tables["Viewledger"].Rows.Count; i++)
            {
                if (Ledgertype == "Invoice Ledger")
                {
                    Balance = Balance + Convert.ToDecimal(ds.Tables["Viewledger"].Rows[i][4].ToString() == "" ? "0" : ds.Tables["Viewledger"].Rows[i][4].ToString());
                    Balance = Balance - Convert.ToDecimal(ds.Tables["Viewledger"].Rows[i][5].ToString() == "" ? "0" : ds.Tables["Viewledger"].Rows[i][5].ToString());
                    if (Balance < 0)
                    {
                        Bal = Convert.ToString(Balance).Replace("-", "") + " DR";
                    }
                    else
                    {
                        Bal = Convert.ToString(Balance) + " CR";
                    }
                }

                else
                {
                    Balance = Balance - Convert.ToDecimal(ds.Tables["Viewledger"].Rows[i][4].ToString() == "" ? "0" : ds.Tables["Viewledger"].Rows[i][4].ToString());
                    Balance = Balance + Convert.ToDecimal(ds.Tables["Viewledger"].Rows[i][5].ToString() == "" ? "0" : ds.Tables["Viewledger"].Rows[i][5].ToString());
                    if (Balance < 0)
                    {
                        Bal = Convert.ToString(Balance).Replace("-", "") + " DR";
                    }
                    else
                    {
                        Bal = Convert.ToString(Balance) + " CR";
                    }
                }
                string Debit = string.Empty;
                string Credit = string.Empty;
                Boolean ISCredit;
                Debit = ds.Tables["Viewledger"].Rows[i][4].ToString() == "0.00" || ds.Tables["Viewledger"].Rows[i][4].ToString() == "0.0000" ? "" : ds.Tables["Viewledger"].Rows[i][4].ToString();
                Credit = ds.Tables["Viewledger"].Rows[i][5].ToString() == "0.00" || ds.Tables["Viewledger"].Rows[i][5].ToString() == "0.0000" ? "" : ds.Tables["Viewledger"].Rows[i][5].ToString();
                if (Credit != "0.0000" && Credit != "")
                {
                    ISCredit = true;
                }
                else
                {
                    ISCredit = false;
                }
                if (i % 2 == 0)
                {

                    sb.Append("<tr >");
                    sb.Append("<td class='details-control'  data-hidden='" + ds.Tables["Viewledger"].Rows[i][3].ToString() + "," + ds.Tables["Viewledger"].Rows[i][0].ToString() + "," + ISCredit + "'><img src='images/details_open.png' /></td>");
                    sb.Append("<td>" + ds.Tables["Viewledger"].Rows[i][1].ToString() + "</td>");
                    //sb.Append("<td style='border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000' align='left' valign=top bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br><a target='_blank' href='Generateledger.aspx?InvoiceNo=" + ds.Tables["Viewledger"].Rows[i][1].ToString() + "&Ledgertype=" + Ledgertype + "&LedgerName=" + ddlledgers.SelectedItem.Text + "&ISCredit=" + ISCredit + "' class='gridViewToolTip'>" + ds.Tables["Viewledger"].Rows[i][1].ToString() + "</a></span></td>");

                    sb.Append("<td>" + ds.Tables["Viewledger"].Rows[i][3].ToString() + "</td>");
                    sb.Append("<td><a href='#'    class='gridViewToolTip'>" + ds.Tables["Viewledger"].Rows[i][2].ToString() + "  </a></td>");

                    sb.Append("<td>" + Debit + "</td>");
                    sb.Append("<td>" + Credit + "</td>");
                    sb.Append("<td>" + Bal.Replace("-", "").ToString() + "</td>");
                    sb.Append("</tr>");


                }
                else
                {
                    sb.Append("<tr>");
                    sb.Append("<td class='details-control' data-hidden='" + ds.Tables["Viewledger"].Rows[i][3].ToString() + "," + ds.Tables["Viewledger"].Rows[i][0].ToString() + "," + ISCredit + "'><img src='images/details_open.png' /></td>");
                    sb.Append("<td>" + ds.Tables["Viewledger"].Rows[i][1].ToString() + "</td>");
                    //sb.Append("<td style='border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000' align='left' valign=top bgcolor='#FFFFFF'><span style='font-size:small'><br><a target='_blank' href='Generateledger.aspx?InvoiceNo=" + ds.Tables["Viewledger"].Rows[i][1].ToString() + "&Ledgertype=" + Ledgertype + "&LedgerName=" + ddlledgers.SelectedItem.Text + "&ISCredit=" + ISCredit + "' class='gridViewToolTip'>" + ds.Tables["Viewledger"].Rows[i][1].ToString() + "</a></span></td>");

                    sb.Append("<td>" + ds.Tables["Viewledger"].Rows[i][3].ToString() + "</td>");
                    sb.Append("<td><a href='#'  class='gridViewToolTip'>" + ds.Tables["Viewledger"].Rows[i][2].ToString() + "  </a></td>");
                    sb.Append("<td>" + Debit + "</td>");
                    sb.Append("<td>" + Credit + "</td>");
                    sb.Append("<td>" + Bal.Replace("-", "").ToString() + "</td>");
                    sb.Append("</tr>");


                }
                //if (Count.Contains(i))
                //{
                //    Sbpdf.Append("<tr><td colspan='6'><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:15pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>Page:1 of 1</span></p></td></tr>");
                //    Sbpdf.Append("<tr><td colspan='6'><p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:15pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>ESSEL PROJECTS PVT LTD</span></p></td></tr>");
                //    Sbpdf.Append("<tr><td colspan='4'  style=' border-right:none'><p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:12pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>" + ddlledgers.SelectedItem.Text + "</span></p></td><td align='left' colspan='2' style=' border-left:none'><span style='font-family:Courier New;font-size:12pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>From: 04/01/" + ddlyear.SelectedValue + "   <br>To:       03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1) + "</span></td></tr>");
                //    Sbpdf.Append("<tr><td  ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Date</span></p></td>");
                //    Sbpdf.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>V.No.</span></p></td>");
                //    Sbpdf.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>BK Code</span></p></td>");
                //    Sbpdf.Append("<td  ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Debit</span></p></td>");
                //    Sbpdf.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Credit</span></p></td>");
                //    Sbpdf.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Balance</span></p></td></tr>");

                //}
                Sbpdf.Append("<tr style='text-align: left; page-break-inside: auto; page-break-after: auto; page-break-before: avoid;line-height: 12pt; margin-top: 0pt; margin-bottom: 0pt;'>");
                Sbpdf.Append("<td style='font-family: Courier New; font-size: 8pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:75px;'>" + ds.Tables["Viewledger"].Rows[i][1].ToString() + "</td>");
                Sbpdf.Append("<td style='font-family: Courier New; font-size: 8pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:100px;' align='center'>" + ds.Tables["Viewledger"].Rows[i][3].ToString() + "</td>");
                Sbpdf.Append("<td style='font-family: Courier New; font-size: 8pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:275px;'>" + ds.Tables["Viewledger"].Rows[i][2].ToString() + "</td>");
                Sbpdf.Append("<td style='font-family: Courier New; font-size: 8pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:75px;' align='right'>" + Debit + "</td>");
                Sbpdf.Append("<td style='font-family: Courier New; font-size: 8pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:75px;' align='right'>" + Credit + "</td>");
                Sbpdf.Append("<td style='font-family: Courier New; font-size: 8pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:100px;' align='right'>" + Bal.Replace("-", "").ToString() + "</td>");
                Sbpdf.Append("</tr>");
            }
            Sbpdf.Append("</table>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            htmlledger.InnerHtml = sb.ToString();
            //htmlledger.InnerHtml = sb.ToString();
            Session["pdf"] = Sbpdf.ToString();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        string lbl = "(" + hfledgername.Value + ")" + "(" + From + " TO " + To + ")";
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", lbl));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        Div1.InnerHtml = Session["pdf"].ToString();
        Div1.RenderControl(htmlwriter);

        Context.Response.Write(Regex.Replace(stringwriter.ToString(), "</?(a|A).*?>", ""));
        Context.Response.End();
    }
    protected void btnpdf_Click(object sender, ImageClickEventArgs e)
    {
        Response.ContentType = "application/pdf";
        string lbl = "(" + hfledgername.Value + ")" + "(" + From + " TO " + To + ")";
        Response.AddHeader("content-disposition", "attachment;filename=" + lbl + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        Div1.InnerHtml = Session["pdf"].ToString();
        Div1.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString().Replace("\r", "").Replace("\n", "").Replace("  ", ""));
        Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0.0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();

        htmlparser.Parse(sr);

        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    protected void btnword_Click(object sender, ImageClickEventArgs e)
    {
        string lbl = "(" + hfledgername.Value + ")" + "(" + From + " TO " + To + ")";
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-doc";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.doc", lbl));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        Div1.InnerHtml = Session["pdf"].ToString();
        Div1.RenderControl(htmlwriter);

        Context.Response.Write(Regex.Replace(stringwriter.ToString(), "</?(a|A).*?>", ""));
        Context.Response.End();
    }
    public StringBuilder LedgerInPDF()
    {
        StringBuilder Objsb = new StringBuilder();
        Objsb.Append("<table style='width:700px' cellspacing=0 cellpadding=5 rules=all border=1 style=border-collapse: collapse;><tr><td colspan='6'><p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:15pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>ESSEL PROJECTS PVT LTD</span></p></td></tr>");
        Objsb.Append("<tr><td colspan='4'  style=' border-right:none'><p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:12pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>" + hfledgername.Value + "</span></p></td><td align='left' colspan='2' style=' border-left:none'><span style='font-family:Courier New;font-size:12pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>From: " + From + "   <br>To:" + To + "</span></td></tr>");
        Objsb.Append("<tr><td  ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Date</span></p></td>");
        Objsb.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Particulars</span></p></td>");
        Objsb.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>V.No.</span></p></td>");
        Objsb.Append("<td  ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Debit</span></p></td>");
        Objsb.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Credit</span></p></td>");
        Objsb.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Balance</span></p></td></tr>");

        return Objsb;
    }
}