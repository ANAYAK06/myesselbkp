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

public partial class detailviewservicetaxpayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["Date"] == null))
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

        da = new SqlDataAdapter("DetailedtaxReport_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@Regno", SqlDbType.VarChar).Value = Request.QueryString["Regno"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTime.ParseExact(Request.QueryString["Date"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);       
        da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = Request.QueryString["Taxtype"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@type", SqlDbType.VarChar).Value = Request.QueryString["type"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Fdate", SqlDbType.VarChar).Value = Request.QueryString["Fdate"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Todate", SqlDbType.VarChar).Value = Request.QueryString["Todate"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Datetype", SqlDbType.VarChar).Value = Request.QueryString["datetype"].ToString();
        da.Fill(ds, "filldata");
        if (ds.Tables["filldata"].Rows.Count > 0)
        {   
            GridView1.DataSource = ds.Tables["filldata"];
            GridView1.DataBind();
            lbldate.Text = Request.QueryString["Date"].ToString();
            Label1.Text = Request.QueryString["Taxtype"].ToString();
        }
        else
        {
            JavaScript.CloseWindow();
        }
    }
    private decimal Total = (decimal)0.0;
    private decimal PTAX = (decimal)0.0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (Request.QueryString["type"].ToString() == "1")
            {
                e.Row.Cells[3].Text = "Client Name";
                if (Request.QueryString["Taxtype"].ToString() == "ServiceTax")
                    e.Row.Cells[4].Text = "Service tax Amount";
                else  if (Request.QueryString["Taxtype"].ToString() == "Excise")
                    e.Row.Cells[4].Text = "Excises Amount";
            }
            else if (Request.QueryString["type"].ToString() == "2")
            {
                e.Row.Cells[3].Text = "Vendor Name";
                if (Request.QueryString["Taxtype"].ToString() == "ServiceTax")
                    e.Row.Cells[4].Text = "Service tax Amount";
                else if (Request.QueryString["Taxtype"].ToString() == "Excise")
                    e.Row.Cells[4].Text = "Excises Amount";
            }
            else if (Request.QueryString["type"].ToString() == "3")
            {
                if (Request.QueryString["Taxtype"].ToString() == "ServiceTax")
                        e.Row.Cells[4].Text = "Service tax Amount";
                else if (Request.QueryString["Taxtype"].ToString() == "Excise")
                        e.Row.Cells[4].Text = "Excises Amount";
                e.Row.Cells[3].Text = "Penel interest  Amount";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Tax"));
            if (Request.QueryString["type"].ToString() == "3")
            {
                PTAX += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "name"));
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = Total.ToString();
            if (Request.QueryString["type"].ToString() == "3")
            {
                e.Row.Cells[3].Text = PTAX.ToString();
            }
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        fillgrid();

        Response.ClearContent();
        Response.Buffer = true;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls",  Label1.Text +" Report "));
        Context.Response.Charset = "";
       // string headerrow = @" <table> <tr > <td align='center' colspan='10'><u><h2>CASH /BANK DEBIT DETAIL OF THE DAY<h2></u> </td> </tr></table>";
        string headerrow = @" <table><tr><td colspan='2'></td><td align='center' colspan='2'><h3> " + Label1.Text + " Detailview Report" + " <h3></td><td align='left' colspan='4'><h3> Date : " + " " + lbldate.Text + "</h3> </td></tr></table> ";
        string headerow3 = @" <table> <tr > <td align='center' colspan='10'> </td> </tr></table>";

        Response.Write(headerrow);
        //Response.Write(tablerow1);
        Response.Write(headerow3);
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();       
    }
}