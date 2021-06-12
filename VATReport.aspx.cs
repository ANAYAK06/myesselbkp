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
public partial class VATReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            trtypeselection.Visible = false;
            dateselection.Visible = false;
            trvatnoselection.Visible = false;
            date.Visible = false;
            trbtn.Visible = false;
            FillVat();
            trexcel.Visible = false;
        }
    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        if (rbtntype.SelectedIndex == 0)
        {
            trtypeselection.Visible = false;
            dateselection.Visible = true;
            trvatnoselection.Visible = true;
            date.Visible = true;
            trbtn.Visible = true;
        }
        else if (rbtntype.SelectedIndex == 1)
        {
            trtypeselection.Visible = true;
            dateselection.Visible = true;
            trvatnoselection.Visible = true;
            date.Visible = true;
            trbtn.Visible = true;
        }
        else
        {
            trtypeselection.Visible = false;
            dateselection.Visible = false;
            trvatnoselection.Visible = false;
            date.Visible = false;
            trbtn.Visible = false;
        }
    }
    
    public void FillVat()
    {
        try
        {

            da = new SqlDataAdapter("select RegNo from [Saletax/VatMaster] where Status='3'", con);
            da.Fill(ds, "Excise/VAT");
            if (ds.Tables["Excise/VAT"].Rows.Count > 0)
            {

                ddlvatno.DataValueField = "RegNo";
                ddlvatno.DataTextField = "RegNo";
                ddlvatno.DataSource = ds.Tables["Excise/VAT"];
                ddlvatno.DataBind();
                ddlvatno.Items.Insert(0, "Select");
                ddlvatno.Items.Insert(1, "Select All");
            }
            else
            {
             
                ddlvatno.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid2 = (GridView)sender;
            GridViewRow HeaderRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header2 = new TableCell();

            Cell_Header2.Text = "View ServiceTax";

            Cell_Header2.Text = "SALES TAX/VAT REPORT ";

            Cell_Header2.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header2.ColumnSpan = 16;
            Cell_Header2.RowSpan = 1;
            HeaderRow2.Cells.Add(Cell_Header2);

            GridView1.Controls[0].Controls.AddAt(0, HeaderRow2);

            GridView HeaderGrid3 = (GridView)sender;
            GridViewRow HeaderRow3 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header3 = new TableCell();
            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Date";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 3;
          
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Claimed SalesTax/VAT";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 6;

            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Paid SalesTax/VAT";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 8;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Balance";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 3;
            HeaderRow3.Cells.Add(Cell_Header3);
            GridView1.Controls[0].Controls.AddAt(1, HeaderRow3);

            GridView HeaderGrid4 = (GridView)sender;
            GridViewRow HeaderRow4 = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header4 = new TableCell();
            Cell_Header4 = new TableCell();

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "InvoiceNo";

            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "CustomerName";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;


            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "Client VAT/TIN";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;

            Cell_Header4 = new TableCell();

            Cell_Header4.Text = "BasicValue";

            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "Exciseduty";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "NetSales Tax/VAT";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "ToVendor";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 6;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 1;


            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "ToGovernment";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 2;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 1;
            GridView1.Controls[0].Controls.AddAt(2, HeaderRow4);
        }
    }
      private decimal Total = (decimal)0.0;
      private decimal Basic = (decimal)0.0;
      private decimal Excise = (decimal)0.0;
      private decimal VAT = (decimal)0.0;
      private decimal VendorBasic = (decimal)0.0;
      private decimal VendorExcise = (decimal)0.0;
      private decimal VendorVAT = (decimal)0.0;
 
      private decimal Govt = (decimal)0.0;
      private decimal PTax = (decimal)0.0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            e.Row.Cells[1].Visible = false;

            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;          
            e.Row.Cells[15].Visible = false;
          
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "&nbsp;")
                Basic += Convert.ToDecimal(0);
            else
                Basic += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Client Basic"));
            if (e.Row.Cells[5].Text == "&nbsp;")
                Excise += Convert.ToDecimal(0);
            else
                Excise += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Client Excise"));
            if (e.Row.Cells[6].Text == "&nbsp;")
                VAT += Convert.ToDecimal(0);
            else
                VAT += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Client VAT"));
            if (e.Row.Cells[11].Text == "&nbsp;")
                VendorExcise += Convert.ToDecimal(0);
            else
                VendorExcise += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Vendor Excise"));
            if (e.Row.Cells[12].Text == "&nbsp;")
                VendorVAT += Convert.ToDecimal(0);
            else
                VendorVAT += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Vendor VAT"));
            if (e.Row.Cells[15].Text == "&nbsp;")
            {
                Total += Convert.ToDecimal(0);
                e.Row.Cells[15].ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
                e.Row.Cells[15].ForeColor = System.Drawing.Color.Red;
            }
            //Basic += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Client Basic"));
            //Excise += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Client Excise"));
            //VAT += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Client VAT"));
            VendorBasic += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Vendor Basic"));
            //VendorExcise += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Vendor Excise"));
            //VendorVAT += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Vendor VAT"));
            Govt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Govt"));
            PTax += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PTax"));
            
            //if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"))>=0)
            //    e.Row.Cells[15].ForeColor = System.Drawing.Color.Black;
            //else
            //    e.Row.Cells[15].ForeColor = System.Drawing.Color.Red;
            //Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = String.Format("Rs. {0:#,##,##,###.00}", Basic);
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", Excise);
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", VAT);
            e.Row.Cells[10].Text = String.Format("Rs. {0:#,##,##,###.00}", VendorBasic);
            e.Row.Cells[11].Text = String.Format("Rs. {0:#,##,##,###.00}", VendorExcise);
            e.Row.Cells[12].Text = String.Format("Rs. {0:#,##,##,###.00}", VendorVAT);
            e.Row.Cells[13].Text = String.Format("Rs. {0:#,##,##,###.00}", Govt);
            e.Row.Cells[14].Text = String.Format("Rs. {0:#,##,##,###.00}", PTax);
            e.Row.Cells[15].Text = String.Format("Rs. {0:#,##,##,###.00}", Total);
           
        }
        
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
        da = new SqlDataAdapter("VATReportNew", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //if (rbtntype.SelectedIndex == 0)
        //{
            if (rbtninvdate.Checked == true)
            {
                da.SelectCommand.Parameters.AddWithValue("@Datetype", "InvoiceDate");
            }
            if (rbtninvmkdate.Checked == true)
            {
                da.SelectCommand.Parameters.AddWithValue("@Datetype", "InvoiceMakingDate");
            }

            if (ddlvatno.SelectedItem.Text == "Select All")
            {
                da.SelectCommand.Parameters.AddWithValue("@Type", "2");
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@RegNo", ddlvatno.SelectedItem.Text);
                da.SelectCommand.Parameters.AddWithValue("@Type", "1");
            }
            da.SelectCommand.Parameters.AddWithValue("@FromDate", txtfrom.Text);
            da.SelectCommand.Parameters.AddWithValue("@Todate", txtto.Text);
            da.SelectCommand.Parameters.AddWithValue("@Report", rbtntype.SelectedValue);
            da.SelectCommand.Parameters.AddWithValue("@Noncr", ddltype.SelectedValue);


            da.Fill(ds, "VATData");
            if (ds.Tables["VATData"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["VATData"];
                GridView1.DataBind();
                trexcel.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                trexcel.Visible = false;
            }
        //}
        //else
        //{
        //}
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.AllowPaging = false;       
        fillgrid();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "VAT"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }

    public void clear()
    {
        txtfrom.Text = "";
        txtto.Text = "";
    }

}