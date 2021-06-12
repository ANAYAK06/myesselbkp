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



public partial class DCADetailviewReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["CCCode"] == null))
            {
                JavaScript.CloseWindow();
            }
            else
            {
                lblcccode.Text = Request.QueryString["CCCode"].ToString();
                try
                {
                    if (Convert.ToString(Request.QueryString["type"]) == "1")
                    {
                        lblbasicsubmited.Visible = false;
                        lblbasicrecipt.Visible = false;
                        lbldcacode.Text = Request.QueryString["DCACode"].ToString();
                        fillgrid();
                    }
                    else if (Convert.ToString(Request.QueryString["type"]) == "2")
                    {
                        lblbudget.Visible = false;
                        lblbasicrecipt.Visible = false;
                        grid();
                    }
                    else if (Convert.ToString(Request.QueryString["type"]) == "3")
                    {
                        lblbudget.Visible = false;
                        lblbasicsubmited.Visible = false;
                        grid1();
                    }
                }
                catch (Exception ex)
                {
 
                }
            }
        }
    }

    public void grid()
    {
        //da = new SqlDataAdapter("select Invoice_Date,PO_NO, isnull(basicvalue,0)-(isnull(edc,0)+isnull(tds,0)+isnull(retention,0)+isnull(hold,0)+isnull(advance,0)+isnull(anyother,0)) as BasicValue from invoice where cc_code='" + lblcccode.Text + "' and Status in ('credit','2') and invoiceno is not null", con);
        da = new SqlDataAdapter("select  REPLACE(CONVERT(VARCHAR(11),invoice_Date, 106), ' ', '-')as invoice_Date,invoiceno as InvoiceNo,PO_NO,BasicValue from invoice where CC_Code='" + lblcccode.Text + "' and Status not in ('cancel') and invoiceno is not null and paymentType != 'Advance' order by CONVERT(DATETIME,CONVERT(VARCHAR(11), invoice_Date)) desc", con);
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["data"];
            GridView1.DataBind();
        }
        else
        {
            gridprint.Visible = false;
            Trnet.Visible = false;
            trexcel.Visible = false;
            btnprint.Visible = false;
            lblnodata.Text = "There is no data";
        }
    
    }
    public void grid1()
    {
        da = new SqlDataAdapter("InvoiceDetailView_In_Budgetreport", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = Request.QueryString["CCCode"].ToString();
        da.Fill(ds, "data1");
        if (ds.Tables["data1"].Rows.Count > 0)
        {
            if (ds.Tables["data11"].Rows.Count > 0)
            {
                ViewState["DeductionAmt"] = ds.Tables["data11"].Rows[0].ItemArray[0].ToString();
                GridView2.DataSource = ds.Tables["data12"];
                GridView2.DataBind();
            }
            else
            {
                ViewState["DeductionAmt"] = 0;
                GridView2.DataSource = null;
                GridView2.DataBind();
            }

            GridView1.DataSource = ds.Tables["data1"];
            GridView1.DataBind();           
        }
        else
        {
            gridprint.Visible = false;
            Trnet.Visible = false;
            trexcel.Visible = false;
            btnprint.Visible = false;
            lblnodata.Text = "There is no data";
        }
    }

    public void fillgrid()
    {        
        da = new SqlDataAdapter("DCAbudgetsummary_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = Request.QueryString["CCCode"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@DCACode", SqlDbType.VarChar).Value = Request.QueryString["DCACode"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = Request.QueryString["Year"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = Request.QueryString["Year1"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@CCType", SqlDbType.VarChar).Value = Request.QueryString["CCType"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@role", SqlDbType.VarChar).Value = Request.QueryString["role"].ToString();
        da.Fill(ds, "filldata");
        if (ds.Tables["filldata"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["filldata"];
            GridView1.DataBind();
        }
        else
        {
            gridprint.Visible = false;
            Trnet.Visible = false;
            trexcel.Visible = false;
            btnprint.Visible = false;
            lblnodata.Text = "There is no data";
        }

      
    }
  
    private decimal Credit = (decimal)0.0;
    private decimal Cashdebit = (decimal)0.0;
    private decimal Chequedebit = (decimal)0.0;
    private decimal Indent = (decimal)0.0;
    private decimal PO = (decimal)0.0;
    private decimal Indentdebit = (decimal)0.0;
    private decimal POdebit = (decimal)0.0;
    private decimal ICF = (decimal)0.0;
    private decimal TotalDebit = (decimal)0.0;
    private decimal deduction = (decimal)0.0;
    private decimal deductionvalue = (decimal)0.0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(Request.QueryString["type"]) == "1")
            {
                if (Request.QueryString["DCACode"].ToString() == "DCA-11" || Request.QueryString["DCACode"].ToString() == "DCA-27")
                {
                    for (int i = 2; i <= 8; i++)
                    {
                        e.Row.Cells[i].Width = 85;
                    }
                    e.Row.Cells[1].Text = "Transaction ID & Description";

                    e.Row.Cells[2].Text = "Internal Credit";

                    e.Row.Cells[3].Text = "Direct Cash Debit";

                    e.Row.Cells[4].Text = "Indent Debit";

                    e.Row.Cells[5].Text = "PO Debit";

                    e.Row.Cells[6].Text = "Through Invoice Debit";

                    e.Row.Cells[7].Text = "Internal Debit";
                }                
                else
                {
                    for (int i = 2; i <= 5; i++)
                    {
                        e.Row.Cells[i].Width = 85;
                    }
                    e.Row.Cells[1].Text = "Transaction ID & Description";
                    e.Row.Cells[2].Text = "Internal Credit";
                    e.Row.Cells[3].Text = "Direct Cash Debit";
                    e.Row.Cells[4].Text = "SPPO Debit";
                    e.Row.Cells[5].Text = "Through Invoice Debit";
                    if (Request.QueryString["DCACode"].ToString() == "DCA-24")
                    {
                        e.Row.Cells[6].Text = "Indent Debit";
                        e.Row.Cells[7].Text = "PO Debit";
                        e.Row.Cells[8].Text = "Internal Debit";
                    }
                    else {
                        e.Row.Cells[6].Text = "Internal Debit";
                    }
                    

                }
            }


        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Wrap = false;
            if (Convert.ToString(Request.QueryString["type"]) == "1")
            {

                Credit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit"));
                Cashdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cash debit"));
                Chequedebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cheque debit"));
                if (Request.QueryString["DCACode"].ToString() == "DCA-11" || Request.QueryString["DCACode"].ToString() == "DCA-27")
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    Indent += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Indent"));
                    PO += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PO"));
                    ICF += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Internal Transfer"));
                }

                else
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    PO += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SPPO"));
                    if (Request.QueryString["DCACode"].ToString() == "DCA-24")
                    {
                        Indentdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Indent debit"));
                        POdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PO debit"));
                    }
                    ICF += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Internal Transfer"));
                }
                TotalDebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Debit"));
                //if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Debit")) == 0)
                //{
                //    e.Row.Visible = false;
                //}
            }
            else if (Convert.ToString(Request.QueryString["type"]) == "2")
            {
                Credit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BasicValue"));
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

            }
            else if (Convert.ToString(Request.QueryString["type"]) == "3")
            {
                Credit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit"));
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                

            }
        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (Convert.ToString(Request.QueryString["type"]) == "1")
            {
                if (Request.QueryString["DCACode"].ToString() == "DCA-11" || Request.QueryString["DCACode"].ToString() == "DCA-27")
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].Text = String.Format("{0:#,##,##,###.00}", Credit);
                    e.Row.Cells[3].Text = String.Format("{0:#,##,##,###.00}", Cashdebit);
                    e.Row.Cells[4].Text = String.Format("{0:#,##,##,###.00}", Indent);
                    e.Row.Cells[5].Text = String.Format("{0:#,##,##,###.00}", PO);
                    e.Row.Cells[6].Text = String.Format("{0:#,##,##,###.00}", Chequedebit);
                    e.Row.Cells[7].Text = String.Format("{0:#,##,##,###.00}", ICF);
                    e.Row.Cells[8].Text = String.Format("{0:#,##,##,###.00}", TotalDebit);
                    Label13.Text = Convert.ToDecimal(TotalDebit - Credit).ToString();
                    Label14.Visible = false;

                }
                else
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].Text = String.Format("{0:#,##,##,###.00}", Credit);
                    e.Row.Cells[3].Text = String.Format("{0:#,##,##,###.00}", Cashdebit);
                    e.Row.Cells[4].Text = String.Format("{0:#,##,##,###.00}", PO);
                    e.Row.Cells[5].Text = String.Format("{0:#,##,##,###.00}", Chequedebit);
                    if (Request.QueryString["DCACode"].ToString() == "DCA-24")
                    {
                        e.Row.Cells[6].Text = String.Format("{0:#,##,##,###.00}", Indentdebit);
                        e.Row.Cells[7].Text = String.Format("{0:#,##,##,###.00}", POdebit);
                        e.Row.Cells[8].Text = String.Format("{0:#,##,##,###.00}", ICF);
                        e.Row.Cells[9].Text = String.Format("{0:#,##,##,###.00}", TotalDebit);
                    }
                    else
                    {
                        e.Row.Cells[6].Text = String.Format("{0:#,##,##,###.00}", ICF);
                        e.Row.Cells[7].Text = String.Format("{0:#,##,##,###.00}", TotalDebit);
                    }
                    Label13.Text = Convert.ToDecimal(TotalDebit - Credit).ToString();
                    Label14.Visible = false;
                }
            }
            else if (Convert.ToString(Request.QueryString["type"]) == "2")
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].Text = "Total Basic Value";
                e.Row.Cells[2].Text = String.Format("{0:#,##,##,###.00}", Credit);
                Label12.Visible = false;
                Label14.Visible = false;
            }
            else if (Convert.ToString(Request.QueryString["type"]) == "3")
            {
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].Text = "Total Credit Value";
                e.Row.Cells[4].Text = String.Format("{0:#,##,##,###.00}", Credit);
                Label12.Visible = false;
                Label14.Visible = true;
                deduction = Credit + Convert.ToDecimal(ViewState["DeductionAmt"]);
                //Label13.Text = Convert.ToDecimal(Credit).ToString();
                Label13.Text =Convert.ToDecimal(ViewState["DeductionAmt"]) +" + "+ e.Row.Cells[4].Text +" = "+deduction;
                e.Row.Cells[4].ForeColor = Color.White;
                e.Row.Cells[4].BackColor = Color.Maroon;
                e.Row.Cells[3].ForeColor = Color.White;
                e.Row.Cells[3].BackColor = Color.Maroon;
                e.Row.Cells[2].BackColor = Color.Maroon;
                e.Row.Cells[1].BackColor = Color.Maroon;
                e.Row.Cells[0].BackColor = Color.Maroon;
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToString(Request.QueryString["type"]) == "1")
            {
                if (Convert.ToDecimal(e.Row.Cells[3].Text) < 0)
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            deductionvalue += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DeductionValue"));
        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Total Deduction Value";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Text = String.Format("{0:#,##,##,###.00}", deductionvalue);
            e.Row.Cells[0].BackColor = Color.Maroon;
            e.Row.Cells[1].BackColor = Color.Maroon;
            e.Row.Cells[2].BackColor = Color.Maroon;
            e.Row.Cells[1].ForeColor = Color.White;
            e.Row.Cells[2].ForeColor = Color.White;

        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";

        if (Convert.ToString(Request.QueryString["type"]) == "1")
        {
            
            fillgrid();
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Detail View Budget of " + lblcccode.Text + "under " + lbldcacode.Text));

        }
        else if (Convert.ToString(Request.QueryString["type"]) == "2")
        {
            grid();
            Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Detail View Budget of " + lblcccode.Text+""));

        }
        else if (Convert.ToString(Request.QueryString["type"]) == "3")
        {
            grid1();
            Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Detail View Budget of " + lblcccode.Text + ""));

        }
        Context.Response.Charset = "";       
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        print.RenderControl(htmlwriter);           
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();           

    }
}