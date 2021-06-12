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

public partial class DCAsummary : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["Type"] == null))
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
        da = new SqlDataAdapter("DCAReport_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = Request.QueryString["CCCode"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@DCACode", SqlDbType.VarChar).Value = Request.QueryString["DCACode"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = Request.QueryString["Type"].ToString();
        if (Request.QueryString["Type"].ToString() == "2")
        {
            da.SelectCommand.Parameters.AddWithValue("@SubType", SqlDbType.VarChar).Value = Request.QueryString["SubType"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Datetype", SqlDbType.VarChar).Value = Request.QueryString["Datetype"].ToString();
        }
        da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = Request.QueryString["Year"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = Request.QueryString["Year1"].ToString();
        da.Fill(ds, "filldata");
        if (ds.Tables["filldata"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["filldata"];
            GridView1.DataBind();
        }
        else
        {
            JavaScript.CloseWindow();
        }
      
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid2 = (GridView)sender;
            GridViewRow HeaderRow2 = new GridViewRow(-1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header2 = new TableCell();
            Cell_Header2.Text = Request.QueryString["DCACode"].ToString()+"  DETAIL VIEW"; 
                //"DCA DETAIL VIEW";
            Cell_Header2.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header2.ColumnSpan = 11;
            Cell_Header2.RowSpan = 1;
            HeaderRow2.Cells.Add(Cell_Header2);

            GridView1.Controls[0].Controls.AddAt(0, HeaderRow2);

            GridView HeaderGrid3 = (GridView)sender;
            GridViewRow HeaderRow3 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header3 = new TableCell();
            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Invoice Date";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Paid Date";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Description";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            HeaderRow3.Cells.Add(Cell_Header3);
            Cell_Header3.RowSpan = 2;

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Internal Transfer";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = " Cash Debit Payments";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);



            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Invoice Through Bank Debit Payments";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Total";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.RowSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "IT Code";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);




            GridView1.Controls[0].Controls.AddAt(1, HeaderRow3);


        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Intcredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "internalcredit"));
            Intdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "internaldebit"));
            Cashpaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cashpaid"));
            Cashpayable += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cashpayable"));
            Invoicepaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "invoicepaid"));
            Invoicepayable += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "invoicepayable"));
            TotalDebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Debit"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = Intcredit.ToString();
            e.Row.Cells[4].Text = Intdebit.ToString();
            e.Row.Cells[5].Text = Cashpaid.ToString();
            e.Row.Cells[6].Text = Cashpayable.ToString();
            e.Row.Cells[7].Text = Invoicepaid.ToString();
            e.Row.Cells[8].Text = Invoicepayable.ToString();
            e.Row.Cells[9].Text = TotalDebit.ToString();
            lbltotal.Text = Convert.ToDecimal(TotalDebit - Intcredit).ToString();

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
        string s = Request.QueryString["DCACode"].ToString() + "Summary.xls";
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", s));
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
            gvrow.BackColor = Color.White;
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

    private decimal Intcredit = (decimal)0.0;
    private decimal Intdebit = (decimal)0.0;
    private decimal Cashpaid = (decimal)0.0;
    private decimal Cashpayable = (decimal)0.0;
    private decimal Invoicepaid = (decimal)0.0;
    private decimal Invoicepayable = (decimal)0.0;
    private decimal TotalDebit = (decimal)0.0;
}
