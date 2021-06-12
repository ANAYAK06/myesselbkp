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

public partial class DetailviewAccruedInterest : System.Web.UI.Page
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
                fillgrid();
            }
        }
    }

    public void fillgrid()
    {

        da = new SqlDataAdapter("DetailedAccrued_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = Request.QueryString["CCCode"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = Request.QueryString["date"].ToString();
        if (Request.QueryString["type"].ToString() == "1")
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
        else if (Request.QueryString["type"].ToString() == "2")
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";
        da.Fill(ds, "filldata");
        if (ds.Tables["filldata"].Rows.Count > 0)
        {
            lblcccode.Text = Request.QueryString["CCCode"].ToString();
            lbldate.Text = Request.QueryString["date"].ToString();
            if (Request.QueryString["type"].ToString() == "2")
            {
                Label1.Text = "CASH /BANK CREDIT DETAIL OF THE DAY";
                GridView2.DataSource = ds.Tables["filldata"];
                GridView2.DataBind();
                btnExcel.Visible = false;
            }
            else
            {
                Label1.Text = "CASH /BANK DEBIT DETAIL OF THE DAY";
                GridView1.DataSource = ds.Tables["filldata"];
                GridView1.DataBind();
                btnExcel1.Visible = false;
            }
        }
        else
        {
            JavaScript.CloseWindow();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            cash += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cash"));
            bank += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bank"));
            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
           

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Text = cash.ToString();
            e.Row.Cells[8].Text = bank.ToString();
            e.Row.Cells[9].Text = Total.ToString();
           

        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InvoiceCredit"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          
            e.Row.Cells[6].Text = Total.ToString();


        }
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        fillgrid();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View Accrued Interest Debit"));
        Context.Response.Charset = "";
        string headerrow = @" <table> <tr > <td align='center' colspan='10'><u><h2>CASH /BANK DEBIT DETAIL OF THE DAY<h2></u> </td> </tr></table>";
        string tablerow1 = @" <table><tr><td align='center' colspan='3'><h3>CC Code : " +  lblcccode.Text +"</h3> </td><td colspan='4'></td><td align='center' colspan='3'><h3> Date : " + " " + lbldate.Text + "</h3> </td></tr></table> ";
        string headerow3 = @" <table> <tr > <td align='center' colspan='10'> </td> </tr></table>";

        Response.Write(headerrow);
        Response.Write(tablerow1);
        Response.Write(headerow3);
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();       
    }
    private decimal cash = (decimal)0.0;
    private decimal bank = (decimal)0.0;
    private decimal Total = (decimal)0.0;
    protected void btnExcel1_Click(object sender, ImageClickEventArgs e)
    {
        fillgrid();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View Accrued Interest Credit"));
        Context.Response.Charset = "";
        string headerrow = @" <table> <tr > <td align='center' colspan='7'><u><h2>CASH /BANK CREDIT DETAIL OF THE DAY<h2></u> </td> </tr></table>";
        string tablerow1 = @" <table><tr><td align='center' colspan='2'><h3>CC Code : " + lblcccode.Text + "</h3> </td><td colspan='3'></td><td align='center' colspan='2'><h3> Date : " + " " + lbldate.Text + "</h3> </td></tr></table> ";
        string headerow3 = @" <table> <tr > <td align='center' colspan='7'> </td> </tr></table>";

        Response.Write(headerrow);
        Response.Write(tablerow1);
        Response.Write(headerow3);
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView2.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }
}