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

public partial class StockPurchaseReport : System.Web.UI.Page
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
        da = new SqlDataAdapter("StockPurchase_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@TaxNos", SqlDbType.VarChar).Value = ddltaxno.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@FromDate", SqlDbType.VarChar).Value = txtfrom.Text;
        da.SelectCommand.Parameters.AddWithValue("@ToDate", SqlDbType.VarChar).Value = txtto.Text;
        da.Fill(ds, "stockreport");
        if (ds.Tables["stockreport"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["stockreport"];
            GridView1.DataBind();            
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();           
        }

    }
    private decimal Quantity = (decimal)0.0;
    private decimal Rate = (decimal)0.0;
    private decimal Amount = (decimal)0.0;
    private decimal Igstpercent = (decimal)0.0;
    private decimal IgstAmt = (decimal)0.0;
    private decimal IgstFAmt = (decimal)0.0;
    private decimal Cgstpercent = (decimal)0.0;
    private decimal CgstAmt = (decimal)0.0;
    private decimal CgstFAmt = (decimal)0.0;
    private decimal Sgstpercent = (decimal)0.0;
    private decimal SgstAmt = (decimal)0.0;
    private decimal SgstFAmt = (decimal)0.0;
    private decimal Ftotal = (decimal)0.0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            if (e.Row.Cells[12].Text == "&nbsp;")
                Igstpercent += Convert.ToDecimal(0);
            else
                Igstpercent += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TIGSTRate"));
        
            if (e.Row.Cells[14].Text == "&nbsp;")
                Cgstpercent += Convert.ToDecimal(0);
            else
                Cgstpercent += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TCGSTRate"));
            
            if (e.Row.Cells[16].Text == "&nbsp;")
                Sgstpercent += Convert.ToDecimal(0);
            else
                Sgstpercent += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TSGSTRate"));

            e.Row.Cells[11].Text = String.Format("{0:0.##}", Double.Parse(e.Row.Cells[9].Text.ToString())* Double.Parse(e.Row.Cells[10].Text.ToString()));
            Amount += Convert.ToDecimal(e.Row.Cells[11].Text);

            IgstAmt = ((Convert.ToDecimal(e.Row.Cells[11].Text))* (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TIGSTRate"))/100));
            IgstFAmt += IgstAmt;

            e.Row.Cells[13].Text = String.Format("{0:0.##}", Double.Parse(IgstAmt.ToString()));
            CgstAmt = ((Convert.ToDecimal(e.Row.Cells[11].Text)) * (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TCGSTRate")) / 100));
            CgstFAmt += CgstAmt;

            e.Row.Cells[15].Text = String.Format("{0:0.##}", Double.Parse(CgstAmt.ToString()));
            SgstAmt = ((Convert.ToDecimal(e.Row.Cells[11].Text)) * (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TSGSTRate")) / 100));
            SgstFAmt += CgstAmt;

            e.Row.Cells[17].Text = String.Format("{0:0.##}", Double.Parse(SgstAmt.ToString()));
            e.Row.Cells[18].Text= String.Format("{0:0.##}", Double.Parse(e.Row.Cells[11].Text.ToString()) + Double.Parse(e.Row.Cells[13].Text.ToString()) + Double.Parse(e.Row.Cells[15].Text.ToString()) + Double.Parse(e.Row.Cells[17].Text.ToString()));
            Ftotal += Convert.ToDecimal(e.Row.Cells[18].Text);

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);
            e.Row.Cells[13].Text = String.Format("Rs. {0:#,##,##,###.00}", IgstFAmt);
            e.Row.Cells[15].Text = String.Format("Rs. {0:#,##,##,###.00}", CgstFAmt);
            e.Row.Cells[17].Text = String.Format("Rs. {0:#,##,##,###.00}", SgstFAmt);
            e.Row.Cells[18].Text = String.Format("Rs. {0:#,##,##,###.00}", Ftotal);
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