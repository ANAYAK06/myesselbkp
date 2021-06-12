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

public partial class Closepoprint : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (Request.QueryString["pono"] == null)
        {
            JavaScript.CloseWindow();
        }
        else
        {
            LoadReport();
        }
    }
    public void LoadReport()
    {
        da = new SqlDataAdapter("select * from Amend_sppo where pono='" + Request.QueryString["pono"].ToString() + "'", con);
        da.Fill(ds, "amend");
        if (ds.Tables["amend"].Rows.Count > 0)
            da = new SqlDataAdapter("select top 1 remarks from Amend_sppo where pono='" + Request.QueryString["pono"].ToString() + "' order by id desc;select (po_value+isnull(sum(Amended_amount),0))as po_value from Amend_sppo a join sppo s on s.pono=a.pono where s.pono='" + Request.QueryString["pono"].ToString() + "' group by po_value", con);
        else
            da = new SqlDataAdapter("select remarks from sppo where pono='" + Request.QueryString["pono"].ToString() + "';select po_value from sppo where pono='" + Request.QueryString["pono"].ToString() + "'", con);

        da.Fill(ds, "remarks");

        da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,balance,cc_code,dca_code,Subdca_code,isnull(SADescription,'')Description from SPPO where pono='" + Request.QueryString["pono"].ToString() + "' and status='Closed' ", con);
        da.Fill(ds, "rep");
        if (ds.Tables["rep"].Rows.Count > 0)
        {
            txtpono.Text = ds.Tables["rep"].Rows[0]["pono"].ToString();
            txtpodate.Text = ds.Tables["rep"].Rows[0]["po_date"].ToString();
            txtpovalue.Text = ds.Tables["remarks1"].Rows[0]["po_value"].ToString();
            txtbalance.Text = ds.Tables["rep"].Rows[0]["balance"].ToString();
            txtcccode.Text = ds.Tables["rep"].Rows[0]["cc_code"].ToString();
            txtdcacode.Text = ds.Tables["rep"].Rows[0]["dca_code"].ToString();
            txtsdca.Text = ds.Tables["rep"].Rows[0]["Subdca_code"].ToString();
            lblremarks.Text = ds.Tables["remarks"].Rows[0]["remarks"].ToString();
            lbldesc.Text = ds.Tables["rep"].Rows[0]["Description"].ToString();
            txtclsdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }

    }   
}