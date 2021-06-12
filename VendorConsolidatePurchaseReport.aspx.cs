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
public partial class VendorConsolidatePurchaseReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filltaxnos();
            //trexcel.Visible = false;
        }
    }
    public void filltaxnos()
    {
        da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id where gm.Status='3'", con);
        ds = new DataSet();
        da.Fill(ds, "gstmain");
        if (ds.Tables["gstmain"].Rows.Count > 0)
        {
            ddltaxno.Items.Clear();
            ddltaxno.DataSource = ds.Tables["gstmain"];
            ddltaxno.DataTextField = "name";
            ddltaxno.DataValueField = "Tax_nos";
            ddltaxno.DataBind();
            ddltaxno.Items.Insert(0, new ListItem("Select"));
            ddltaxno.Items.Insert(1, new ListItem("Select All"));
        }
        else
        {
            ddltaxno.Items.Clear();
            ddltaxno.Items.Insert(0, new ListItem("Select"));
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
        da = new SqlDataAdapter("StockPurchaseConsolidate_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@TaxNos", SqlDbType.VarChar).Value = ddltaxno.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@FromDate", SqlDbType.VarChar).Value = txtfrom.Text;
        da.SelectCommand.Parameters.AddWithValue("@ToDate", SqlDbType.VarChar).Value = txtto.Text;
        da.Fill(ds, "stockreportC");
        if (ds.Tables["stockreportC"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["stockreportC"];
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
   
    private decimal Amount = (decimal)0.0;    
    private decimal IgstAmt = (decimal)0.0;
    private decimal CgstAmt = (decimal)0.0;    
    private decimal SgstAmt = (decimal)0.0;
    private decimal Other = (decimal)0.0;
    private decimal Frieght = (decimal)0.0;
    private decimal Deduction = (decimal)0.0;
    private decimal Total = (decimal)0.0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[5].Text == "&nbsp;")
                Amount += Convert.ToDecimal(0);
            else
                Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InvoiceAmount"));

            if (e.Row.Cells[6].Text == "&nbsp;")
                IgstAmt += Convert.ToDecimal(0);
            else
                IgstAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "IGST"));

            if (e.Row.Cells[7].Text == "&nbsp;")
                CgstAmt += Convert.ToDecimal(0);
            else
                CgstAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CGST"));

            if (e.Row.Cells[8].Text == "&nbsp;")
                SgstAmt += Convert.ToDecimal(0);
            else
                SgstAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SGST"));

            if (e.Row.Cells[9].Text == "&nbsp;")
                Other += Convert.ToDecimal(0);
            else
                Other += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Other"));

            if (e.Row.Cells[10].Text == "&nbsp;")
                Deduction += Convert.ToDecimal(0);
            else
                Deduction += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Deduction"));

            if (e.Row.Cells[11].Text == "&nbsp;")
                Frieght += Convert.ToDecimal(0);
            else
                Frieght += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Frieght"));

            e.Row.Cells[12].Text = String.Format("{0:0.##}", Double.Parse(e.Row.Cells[5].Text.ToString()) + Double.Parse(e.Row.Cells[6].Text.ToString()) + Double.Parse(e.Row.Cells[7].Text.ToString()) + Double.Parse(e.Row.Cells[8].Text.ToString()) + Double.Parse(e.Row.Cells[9].Text.ToString()) + Double.Parse(e.Row.Cells[10].Text.ToString()) + Double.Parse(e.Row.Cells[11].Text.ToString()));
            Total += Convert.ToDecimal(e.Row.Cells[12].Text);

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", IgstAmt);
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", CgstAmt);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", SgstAmt);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Other);
            e.Row.Cells[10].Text = String.Format("Rs. {0:#,##,##,###.00}", Deduction);
            e.Row.Cells[11].Text = String.Format("Rs. {0:#,##,##,###.00}", Frieght);
            e.Row.Cells[12].Text = String.Format("Rs. {0:#,##,##,###.00}", Total);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.AllowPaging = false;
        fillgrid();
        if (GridView1.DataSource != null)
        {
            Context.Response.ClearContent();
            Context.Response.ContentType = "application/ms-excel";
            Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "StockPurchase Report"));
            Context.Response.Charset = "";
            System.IO.StringWriter stringwriter = new System.IO.StringWriter();
            HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
            GridView1.RenderControl(htmlwriter);
            Context.Response.Write(stringwriter.ToString());
            Context.Response.End();
        }

    }
}