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
using System.Drawing;

public partial class AssetSaleReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            trexcel.Visible = false;
            trgrid.Visible = false;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AssetSaleReport.aspx");
    }
    public void fillgrid()
    {
        try
        {
            da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),ap.date, 106), ' ', '-')as [Date],ass.buyer_name,ass.request_no,ass.item_code,payment_type,ap.amount,ap.status from asset_payment ap join asset_sale ass on ass.id=ap.assetsale_id and convert(datetime,ap.Date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'", con);
            da.Fill(ds, "central");
            if (ds.Tables["central"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["central"];
                GridView1.DataBind();
                trexcel.Visible = true;
                trgrid.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                trexcel.Visible = false;
                trgrid.Visible = false;
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    private decimal MainAmount = (decimal)0.0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text == "1")
                {
                    e.Row.Cells[6].Text = "Waiting For HO Admin Approval";
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Orange;
                }
                if (e.Row.Cells[6].Text == "2")
                {
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[6].Text = "Approved";
                }
                else
                {
                   e.Row.Cells[6].Text = "Rejected";
                   e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                }              
            }            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
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
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Asset Sold Report"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
   

    
}