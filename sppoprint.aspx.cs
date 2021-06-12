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

public partial class sppoprint : System.Web.UI.Page
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
            printgrid();
        }
    }
    public void printgrid()
    {
        da = new SqlDataAdapter("Select unit,rate,quantity,remarks,replace(po_value,'.0000','.00') as po_value from sppo where pono='" + Request.QueryString["pono"].ToString() + "' ", con);
        da.Fill(ds, "grid");
        if (ds.Tables["grid"].Rows.Count > 0)
        {
            GridView2.DataSource = ds.Tables["grid"];
            GridView2.DataBind();
            grid();
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
    }
    public void grid()
    {
        da = new SqlDataAdapter("select v.vendor_id,v.vendor_name,v.vendor_phone,v.address,s.pono,s.vendor_id,s.cc_code,s.dca_code,s.subdca_code,replace((isnull(s.po_value,0)),'.oooo','.oo')as po_value,REPLACE(CONVERT(VARCHAR(11),s.po_date, 106), ' ', '-')as po_date from SPPO s  join vendor v on v.vendor_id=s.vendor_id where pono='" + Request.QueryString["pono"].ToString() + "' ", con);
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            lblvenname.Text = ds.Tables["data"].Rows[0]["vendor_name"].ToString();
            lblvenaddress.Text = ds.Tables["data"].Rows[0]["address"].ToString();
            lblphone.Text = ds.Tables["data"].Rows[0]["vendor_phone"].ToString();
            lblcccode.Text = ds.Tables["data"].Rows[0]["cc_code"].ToString();
            lbldcacode.Text = ds.Tables["data"].Rows[0]["dca_code"].ToString();
            lblvendorcode.Text = ds.Tables["data"].Rows[0]["vendor_id"].ToString();
            lblpono.Text = ds.Tables["data"].Rows[0]["pono"].ToString();
            lblpodate.Text = ds.Tables["data"].Rows[0]["po_date"].ToString();
           
        }
        
    }
}
