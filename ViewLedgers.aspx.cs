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

public partial class ViewLedgers : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataAdapter da = null;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");


        if (Session["user"] == null)
            Response.Redirect("Default.aspx");
        if (!IsPostBack)
        {
            LoadYear();
            Fillledgers();
            trexcel.Visible = false;
        }
       // ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "InitializeToolTip();", true);
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
                string[] items = RecordedLedgerName.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries);
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
   
    public void LoadYear()
    {
        for (int i = 2016; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Select Year");
    }
    public void Fillledgers()
    {
        try
        {
            da = new SqlDataAdapter("select Name,id,ledger_type from ledger where status='3' order by name asc", con);
            da.Fill(ds, "ledgernames");
            if (ds.Tables["ledgernames"].Rows.Count > 0)
            {
                ddlledgers.DataTextField = "Name";
                ddlledgers.DataValueField = "id";
                ddlledgers.DataSource = ds.Tables["ledgernames"];
                ddlledgers.DataBind();
                ddlledgers.Items.Insert(0, "Select Ledger");
                Session["ledgernames"] = ds.Tables["ledgernames"];
               
            }

        }
        catch (Exception ex)
        { 
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            
            da = new SqlDataAdapter("ViewLedger", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@LedgeId", SqlDbType.VarChar).Value = ddlledgers.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            da.SelectCommand.Parameters.AddWithValue("@FromDate", SqlDbType.DateTime).Value = txtfrom.Text;
            da.SelectCommand.Parameters.AddWithValue("@ToDate", SqlDbType.DateTime).Value = txtto.Text;
            da.Fill(ds, "Viewledger");
            if (ds.Tables["Viewledger"].Rows.Count > 0)
            {
                trexcel.Visible = true;
                Session["FYYear"] = ddlyear.SelectedItem.Text;
                string Ledgertype = "";
                string Bal = string.Empty;
                var obj = (from a in (Session["ledgernames"] as DataTable).AsEnumerable() where a.Field<int>("id") == Convert.ToInt32(ddlledgers.SelectedValue) select a);

                if (obj.ToList().Count > 0)
                {
                    Ledgertype = obj.ToList()[0].ItemArray[2].ToString();
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<table id='example' class='display' style='width: 100%'><thead><tr><td colspan='7' align='center'><span style='font-size:xx-large'>ESSEL Projects Pvt Ltd</span></td></tr><tr><td colspan='7' align='center'><span style='font-size:x-large'>" + ddlledgers.SelectedItem.Text + "</span></td></tr><tr><td align='left' colspan='7'><span style='font-size:larger'>From: 04/01/" + ddlyear.SelectedValue + "   <br>To:       03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1) + "</span></td></tr><tr><th></th><th>Date</th><th>Particulars</th><th>V.No</th><th>Debit</th><th>Credit</th><th>Balance Rs.</th>");
                sb.Append("<tbody>");
                StringBuilder Sbpdf = new StringBuilder();
                Sbpdf = LedgerInPDF();
                //string text = System.IO.File.ReadAllText(@"D:\KK\NewApplicationAfterGST\SL\App_Data\Ledger_Format.HTML");

                string Path = Server.MapPath("~/App_Data/Ledger_Format.html");

                string text = System.IO.File.ReadAllText(Path);
                int[] Count = {20,40,60,80,100,120,140,160,180,200,220,240,260,280,300 };
                //sb.Append(text.ToString());
                hf.Value = Ledgertype;
                hfledgername.Value = ddlledgers.SelectedValue;

             
                for (int i = 0; i < ds.Tables["Viewledger"].Rows.Count; i++)
                {
                    if (Ledgertype == "Invoice Ledger")
                    {
                        Balance = Balance + Convert.ToDecimal(ds.Tables["Viewledger"].Rows[i][4].ToString() == "" ? "0" : ds.Tables["Viewledger"].Rows[i][4].ToString());
                        Balance = Balance - Convert.ToDecimal(ds.Tables["Viewledger"].Rows[i][5].ToString() == "" ? "0" : ds.Tables["Viewledger"].Rows[i][5].ToString());
                        if (Balance < 0)
                        {
                            Bal =  Convert.ToString(Balance).Replace("-", "")+ " DR";
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
        catch (Exception ex)
        {

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        string lbl = "(" + ddlledgers.SelectedItem.Text + ")" + "(" + txtfrom.Text + " TO " + txtto.Text + ")";
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", lbl));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        Div1.InnerHtml = Session["pdf"].ToString();
        Div1.RenderControl(htmlwriter);
       
        Context.Response.Write( Regex.Replace(stringwriter.ToString(), "</?(a|A).*?>", ""));
        Context.Response.End();
    }
    protected void btnword_Click(object sender, ImageClickEventArgs e)
    {
        string lbl = "(" + ddlledgers.SelectedItem.Text + ")" + "(" + txtfrom.Text + " TO " + txtto.Text + ")";
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
    public class ITextEvents : PdfPageEventHelper
    {

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;


        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


       
    }
    protected void btnpdf_Click(object sender, ImageClickEventArgs e)
    {
        Response.ContentType = "application/pdf";
        string lbl = "(" + ddlledgers.SelectedItem.Text + ")" + "(" + txtfrom.Text + " TO " + txtto.Text + ")";
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
    public StringBuilder LedgerInPDF()
    {
        StringBuilder Objsb = new StringBuilder();
        Objsb.Append("<table style='width:700px' cellspacing=0 cellpadding=5 rules=all border=1 style=border-collapse: collapse;><tr><td colspan='6'><p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:15pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>ESSEL PROJECTS PVT LTD</span></p></td></tr>");
        Objsb.Append("<tr><td colspan='4'  style=' border-right:none'><p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:12pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>" + ddlledgers.SelectedItem.Text + "</span></p></td><td align='left' colspan='2' style=' border-left:none'><span style='font-family:Courier New;font-size:12pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;'>From: 04/01/" + ddlyear.SelectedValue + "   <br>To:       03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1) + "</span></td></tr>");
        Objsb.Append("<tr><td  ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Date</span></p></td>");
        Objsb.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Particulars</span></p></td>");
        Objsb.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>V.No.</span></p></td>");
        Objsb.Append("<td  ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Debit</span></p></td>");
        Objsb.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Credit</span></p></td>");
        Objsb.Append("<td ><p style='text-align:center;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>Balance</span></p></td></tr>");

        return Objsb;
    }
    public string LedgerInWord()
    {
        string Body = string.Empty;
         string Ledgertype = "";
        try
        {
            DataTable objdt = ViewState["Viewledger"] as DataTable;
           
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Times New Roman;font-size:12pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;mso-spacerun:yes;'></span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;'>&#xa0;</span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;'>ESSEL PROJECTS PVT LTD</span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;'>&#xa0;</span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;mso-spacerun:yes;'>LEDGER&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;From : 01-04-" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "</span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;mso-spacerun:yes;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To : 31-03-" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "</span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;'>-------------------------------------------------------------------------------------------------------------------</span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;mso-spacerun:yes;'>&nbsp;&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;V.No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BkCode&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debit&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Credit&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Balance Rs.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;'>-------------------------------------------------------------------------------------------------------------------</span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'>"+ddlledgers.SelectedItem.Text+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:bold;font-style:normal;font-variant:normal;mso-spacerun:yes;'></span></p>";
            Body = Body + "<p style='text-align:left;page-break-inside:auto;page-break-after:auto;page-break-before:avoid;line-height:12pt;margin-top:0pt;margin-bottom:0pt;'><span style='font-family:Courier New;font-size:10pt;text-transform:none;font-weight:normal;font-style:normal;font-variant:normal;'>-------------------------------------------------------------------------------------------------------------------</span></p>";
            Body = Body + " <table class='Section0' style=width:900pxpx' id='tblledger'>";
            var obj = (from a in (ViewState["ledgernames"] as DataTable).AsEnumerable() where a.Field<int>("id") == Convert.ToInt32(ddlledgers.SelectedValue) select a);
           
            if (obj.ToList().Count > 0)
            {
                Ledgertype = obj.ToList()[0].ItemArray[2].ToString();
            }
            for (int i = 0; i < objdt.Rows.Count; i++)
            {
                string Bal = string.Empty;
                string Debit = objdt.Rows[i][3].ToString() == "0.00" ? "" : objdt.Rows[i][3].ToString();
                string Credit = objdt.Rows[i][4].ToString() == "0.00" ? "" : objdt.Rows[i][4].ToString();
                Body = Body + " <tr style='text-align: left; page-break-inside: auto; page-break-after: auto; page-break-before: avoid;line-height: 12pt; margin-top: 0pt; margin-bottom: 0pt;'>";
                Body = Body + "<td style='font-family: Courier New; font-size: 10pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:100px;'>" + objdt.Rows[i][0].ToString() + "</td>";
                Body = Body + "<td style='font-family: Courier New; font-size: 10pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:100px;' align='center'><a href='#' class='gridViewToolTip'>" + objdt.Rows[i][1].ToString() + "  </a> <div id='tooltip' style='display: none;'></div></td>";
                Body = Body + "<td style='font-family: Courier New; font-size: 10pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:300px;'>" + objdt.Rows[i][2].ToString() + "</td>";
                Body = Body + "<td style='font-family: Courier New; font-size: 10pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:100px;'>" + Debit + "</td>";
                Body = Body + "<td style='font-family: Courier New; font-size: 10pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:100px;'>" + Credit + "</td>";
                if (Ledgertype == "Invoice Ledger")
                {
                    Balance = Balance + Convert.ToDecimal(objdt.Rows[i][3].ToString() == "" ? "0" : objdt.Rows[i][3].ToString());
                    Balance = Balance - Convert.ToDecimal(objdt.Rows[i][4].ToString() == "" ? "0" : objdt.Rows[i][4].ToString());
                    if (Balance < 0)
                    {
                        Bal = "DR " + Convert.ToString(Balance).Replace("-", "") ;
                    }
                    else
                    {
                        Bal = "CR " + Convert.ToString(Balance);
                    }
                    Body = Body + "<td style='font-family: Courier New; font-size: 10pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:130px;' align='right'>" + Bal + "</td></tr>";
                }
               
                else
                {
                    Balance = Balance - Convert.ToDecimal(objdt.Rows[i][3].ToString() == "" ? "0" : objdt.Rows[i][3].ToString());
                    Balance = Balance + Convert.ToDecimal(objdt.Rows[i][4].ToString() == "" ? "0" : objdt.Rows[i][4].ToString());
                    if (Balance < 0)
                    {
                        Bal = "DR " + Convert.ToString(Balance).Replace("-", "");
                    }
                    else
                    {
                        Bal = "CR " + Convert.ToString(Balance);
                    }
                    Body = Body + "<td style='font-family: Courier New; font-size: 10pt; text-transform: none; font-weight: normal;font-style: normal; font-variant: normal; mso-spacerun: yes; width:130px;' align='right'>" + Bal + "</td></tr>";
                }



            }
            Body = Body + "</table>";
            Session["Body"] = Body;

          
        }
        catch (Exception ex)
        {
        }
        return Ledgertype;
            
    }
    private decimal Credit = (decimal)0.0;
    private decimal Debit = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
}