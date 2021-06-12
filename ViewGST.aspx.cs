using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;
public partial class ViewGST : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGST();
        }
    }
    public void BindGST()
    {
        try
        {
            da = new SqlDataAdapter("Select GST_No from gstmaster where status='3'", con);
            da.Fill(ds, "GST");
            ddlGstno.DataValueField = "GST_No";
            ddlGstno.DataTextField = "GST_No";
            ddlGstno.DataSource = ds.Tables["GST"];
            ddlGstno.DataBind();
            ddlGstno.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {

        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            
            GridView HeaderGrid2 = (GridView)sender;
            GridViewRow HeaderRow2 = new GridViewRow(-1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header2 = new TableCell();
            Cell_Header2.Text = "GST Report";
            Cell_Header2.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header2.ColumnSpan = 25;
            Cell_Header2.RowSpan = 1;
            
            HeaderRow2.Cells.Add(Cell_Header2);

            HeaderRow2.Attributes["style"] = "padding: 4px 2px; color: #fff; background: #424242 url(./images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size: 1.5em;";
            
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow2);

            GridView HeaderGrid3 = (GridView)sender;
            GridViewRow HeaderRow3 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header3 = new TableCell();
            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);

            HeaderRow3.Attributes["style"] = "padding: 4px 2px; color: white; background: #424242 url(./images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size: 1.4em;";
            GridView1.Controls[0].Controls.AddAt(1, HeaderRow3);

            GridView HeaderGrid4 = (GridView)sender;
            GridViewRow HeaderRow4 = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header4 = new TableCell();
            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "CLAIMED GST";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 7;
            Cell_Header4.RowSpan = 1;
          
            HeaderRow4.Cells.Add(Cell_Header4);

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "PAID GST";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 12;
            Cell_Header4.RowSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            Cell_Header4.RowSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);

            HeaderRow4.Attributes["style"] = "padding: 4px 2px; color: white; background: #424242 url(./images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size: 1em;";

            GridView1.Controls[0].Controls.AddAt(2, HeaderRow4);

            GridView HeaderGrid5 = (GridView)sender;
            GridViewRow HeaderRow5 = new GridViewRow(0,3, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header5 = new TableCell();
            Cell_Header5 = new TableCell();
            Cell_Header5.Text = "";
            Cell_Header5.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header5.ColumnSpan = 1;
            Cell_Header5.RowSpan = 1;
            HeaderRow5.Cells.Add(Cell_Header5);

            Cell_Header5 = new TableCell();
            Cell_Header5.Text = "";
            Cell_Header5.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header5.ColumnSpan = 1;
            Cell_Header5.RowSpan = 1;
            HeaderRow5.Cells.Add(Cell_Header5);

            Cell_Header5 = new TableCell();
            Cell_Header5.Text = "";
            Cell_Header5.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header5.ColumnSpan = 7;
            Cell_Header5.RowSpan = 1;
            HeaderRow5.Cells.Add(Cell_Header5);


            Cell_Header5 = new TableCell();
            Cell_Header5.Text = "TO VENDOR";
            Cell_Header5.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header5.ColumnSpan = 7;
            Cell_Header5.RowSpan = 1;
            HeaderRow5.Cells.Add(Cell_Header5);    



            Cell_Header5 = new TableCell();
            Cell_Header5.Text = "TO GOVT";
            Cell_Header5.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header5.ColumnSpan = 4;
            Cell_Header5.RowSpan = 1;
            HeaderRow5.Cells.Add(Cell_Header5);

            Cell_Header5 = new TableCell();
            Cell_Header5.Text = "PENAL INTEREST";
            Cell_Header5.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header5.ColumnSpan = 1;
            Cell_Header5.RowSpan = 1;
            HeaderRow5.Cells.Add(Cell_Header5);

            Cell_Header5 = new TableCell();
            Cell_Header5.Text = "BALANACE";
            Cell_Header5.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header5.ColumnSpan = 1;
            Cell_Header5.RowSpan = 1;
            HeaderRow5.Cells.Add(Cell_Header5);

            HeaderRow5.Attributes["style"] = "padding: 4px 2px; color: white; background: #424242 url(./images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size: 0.9em;";

            GridView1.Controls[0].Controls.AddAt(3, HeaderRow5);
        }
    }
    public void BindGSTGrid(string datetype)
    {
        try
        {
            da = new SqlDataAdapter("GetGST_SP", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@From", txtfrom.Text);
            da.SelectCommand.Parameters.AddWithValue("@To", txtto.Text);
            da.SelectCommand.Parameters.AddWithValue("@GSTNos", ddlGstno.SelectedValue);
            da.SelectCommand.Parameters.AddWithValue("@Type", datetype);
            da.Fill(ds, "ViewGST");
            if (ds.Tables["ViewGST"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["ViewGST"];
                GridView1.DataBind();
                ViewState["ViewGST"] = ds.Tables["ViewGST"];
            }
            else
            {
                GridView1.EmptyDataText = "There is no records";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        BindGSTGrid(btnView.Text);
    }
    protected void btnView1_Click(object sender, EventArgs e)
    {
        BindGSTGrid(btnview1.Text);
    }
    private decimal ClientGST = (decimal)0.0;
   
    private decimal VendorGST = (decimal)0.0;
  
    private decimal GovtGST = (decimal)0.0;
    private decimal XLClientGST = (decimal)0.0;

    private decimal XLVendorGST = (decimal)0.0;

    private decimal XLGovtGST = (decimal)0.0;
   

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes["style"] = "padding: 4px 2px; color: white; background: #424242 url(./images/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size: 0.9em;";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ClientGST += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ClientIGST")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ClientCGST")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ClientSGST"));
            VendorGST += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "VendorIGST")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "VendorCGST")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "VendorSGST"));
            GovtGST += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GovtIGST")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GovtCGST")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GovtSGST"));
            e.Row.Cells[21].Text=(ClientGST-(VendorGST+GovtGST)).ToString();

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    public void RendertoExcel()
    {
        
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {

        try
        {

            if (ViewState["ViewGST"] != null)
            {

                StringBuilder sb = new StringBuilder();

                //string text = System.IO.File.ReadAllText(@"D:\KK\NewApplicationAfterGST\SL\GST_REPORT.HTML");

                string Path = Server.MapPath("~/App_Data/GST_REPORT.html");
             
                string text = System.IO.File.ReadAllText(Path);
                
                sb.Append(text.ToString());
                DataTable dtobj = ViewState["ViewGST"] as DataTable;
                for (int i = 0; i < dtobj.Rows.Count ; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='left' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][0].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='left' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][1].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='left' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][2].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='left' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][3].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='left' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][4].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][5].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000' ><br>" + dtobj.Rows[i][6].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000' ><br>" + dtobj.Rows[i][7].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000' ><br>" + dtobj.Rows[i][8].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='left' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][9].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='left' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][10].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='left' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + dtobj.Rows[i][11].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000' ><br>" + dtobj.Rows[i][12].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000'  ><br>" + dtobj.Rows[i][13].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000'  ><br>" + dtobj.Rows[i][14].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000'  ><br>" + dtobj.Rows[i][15].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000'  ><br>" + dtobj.Rows[i][16].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000' ><br>" + dtobj.Rows[i][17].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000'  ><br>" + dtobj.Rows[i][18].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000' ><br>" + dtobj.Rows[i][19].ToString() + "</font></td>");
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000' ><br>" + dtobj.Rows[i][20].ToString() + "</font></td>");
                    XLClientGST += Convert.ToDecimal(dtobj.Rows[i]["ClientIGST"].ToString()) + Convert.ToDecimal(dtobj.Rows[i]["ClientCGST"].ToString()) + Convert.ToDecimal(dtobj.Rows[i]["ClientSGST"].ToString());
                    XLVendorGST += Convert.ToDecimal(dtobj.Rows[i]["VendorIGST"].ToString()) + Convert.ToDecimal(dtobj.Rows[i]["VendorCGST"].ToString()) + Convert.ToDecimal(dtobj.Rows[i]["VendorSGST"].ToString());
                    XLGovtGST += Convert.ToDecimal(dtobj.Rows[i]["GovtIGST"].ToString()) + Convert.ToDecimal(dtobj.Rows[i]["GovtCGST"].ToString()) + Convert.ToDecimal(dtobj.Rows[i]["GovtSGST"].ToString());
                    sb.Append("<td style='border-top: 1px solid #ffffff; border-bottom: 1px solid #ffffff; border-left: 1px solid #ffffff; border-right: 1px solid #ffffff1 height='20' align='right' valign=bottom bgcolor='#8497B0'><font color='#000000'><br>" + (XLClientGST - (XLVendorGST + XLGovtGST)).ToString() + "</font></td>");

                    sb.Append("</tr>");
                }
                sb.Append("</table></body></html>");
                Context.Response.ClearContent();
                Context.Response.ContentType = "application/ms-excel";

                Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", txtfrom.Text + ',' + txtto.Text));
                Context.Response.Charset = "";

                Context.Response.Write(sb.ToString());
                Context.Response.End();

            }
        }
        catch (Exception ex)
        {
            //Utilities.CatchException(ex);
            //JavaScript.Alert(ex.ToString());
        }
    }
}