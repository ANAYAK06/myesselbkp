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
using System.Web.Configuration;

public partial class ViewClientAdvance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            FillClient();
        }

    }
    
    public void FillClient()
    {
        ddlclientid.Items.Clear();
        ddlsubclientid.Items.Clear();
        ddlpo.Items.Clear();
        ddlcccode.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            da = new SqlDataAdapter("select distinct client_id from invoice where   paymenttype='Advance' and status!='cancel'", con);
            da.Fill(ds, "client");

            ddlclientid.DataTextField = "client_id";
            ddlclientid.DataValueField = "client_id";
            ddlclientid.DataSource = ds.Tables["client"];
            ddlclientid.DataBind();
            ddlclientid.Items.Insert(0, "select");
            ddlsubclientid.Items.Insert(0, "select");
            ddlcccode.Items.Insert(0, "select");
            ddlpo.Items.Insert(0, "select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpo.Items.Clear();
        ddlcccode.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();

        try
        {
            da = new SqlDataAdapter("select   distinct i.cc_code, i.cc_code+ ',' +c.cc_name as ccname  from invoice i join cost_center c on i.cc_code=c.cc_code where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and paymenttype='Advance' ", con);
            da.Fill(ds, "cccode");

            ddlcccode.DataTextField = "ccname";
            ddlcccode.DataValueField = "cc_code";
            ddlcccode.DataSource = ds.Tables["cccode"];
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "select");
            ddlpo.Items.Insert(0, "select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpo.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            da = new SqlDataAdapter("select distinct po_no from invoice where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and cc_code='" + ddlcccode.SelectedValue + "' and paymenttype='Advance' ", con);

            da.Fill(ds, "pono");

            ddlpo.DataTextField = "po_no";
            ddlpo.DataValueField = "po_no";
            ddlpo.DataSource = ds.Tables["pono"];
            ddlpo.DataBind();
            ddlpo.Items.Insert(0, "select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsubclientid.Items.Clear();
        ddlcccode.Items.Clear();
        ddlpo.Items.Clear();
        ddlcccode.Items.Insert(0, "select");
        ddlpo.Items.Insert(0, "select");
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            da = new SqlDataAdapter("select distinct subclient_id from invoice where  client_id='" + ddlclientid.SelectedItem.Text + "' and paymenttype='Advance' ", con);

            da.Fill(ds, "subclient");

            ddlsubclientid.DataTextField = "subclient_id";
            ddlsubclientid.DataValueField = "subclient_id";
            ddlsubclientid.DataSource = ds.Tables["subclient"];
            ddlsubclientid.DataBind();

            ddlsubclientid.Items.Insert(0, "select");


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        string condition = "";

        if (ddlclientid.SelectedItem.Text != "select")
        {
            condition = condition + " and client_id='" + ddlclientid.SelectedItem.Text + "'";

        }
        if (ddlsubclientid.SelectedItem.Text != "select")
        {
            condition = condition + " and subclient_id='" + ddlsubclientid.SelectedItem.Text + "'";

        }
        if (ddlcccode.SelectedValue != "select")
        {
            condition = condition + " and cc_code='" + ddlcccode.SelectedValue + "'";

        }
        if (ddlpo.SelectedItem.Text != "select")
        {
            condition = condition + " and po_no='" + ddlpo.SelectedItem.Text + "'";

        }
        da = new SqlDataAdapter("select invoiceno,po_no,REPLACE(CONVERT(VARCHAR(11),invoice_date, 106), ' ', '-')as invoice_date,cc_code,ra_no,Replace(basicvalue,'.0000','.00') as Credit,Replace(isnull(null,0),'0','0.00')[Debit] ,client_id,subclient_id from invoice where  paymenttype='Advance' and status!='Cancel'" + condition + " union all select invoiceno,po_no,REPLACE(CONVERT(VARCHAR(11),invoice_date, 106), ' ', '-')as invoice_date,cc_code,ra_no,Replace(isnull(null,0),'0','0.00')[Debit],Replace(advance,'.0000','.00') as debit,client_id,subclient_id from invoice where paymenttype!='Advance' and advance!='0' and status!='Cancel'" + condition + "", con);
        da.Fill(ds, "fill");
        GridView1.DataSource = ds.Tables["fill"];
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Credit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "credit"));
            Debit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", Credit);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Debit);

        }
        
    }
    private decimal Credit = (decimal)0.0;
    private decimal Debit = (decimal)0.0;
}
