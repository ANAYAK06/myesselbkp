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


public partial class selfinvoiceformatforgeneralpayment : System.Web.UI.Page
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
        if (Request.QueryString["Voucherid"] == null)
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
        da = new SqlDataAdapter("Select Invoiceno,cc_code,dca_code,subdca_code,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,Description,name,replace(amount,'.0000','.00')as amount,Mode_of_Pay from GeneralInvoices where invoiceno='" + Request.QueryString["Voucherid"].ToString() + "'", con);
        da.Fill(ds, "rep");
        if (ds.Tables["rep"].Rows.Count > 0)
        {


            txtadviceno.Text = ds.Tables["rep"].Rows[0]["Invoiceno"].ToString();
            txtcccode.Text = ds.Tables["rep"].Rows[0]["cc_code"].ToString();
            txtdca.Text = ds.Tables["rep"].Rows[0]["dca_code"].ToString();
            txtsubdca.Text = ds.Tables["rep"].Rows[0]["subdca_code"].ToString();
            txtpaymentdate.Text = ds.Tables["rep"].Rows[0]["date"].ToString();
            txtdesc.Text = ds.Tables["rep"].Rows[0]["Description"].ToString();
            txtpartyname.Text = ds.Tables["rep"].Rows[0]["name"].ToString();
            txtpaidamount.Text = ds.Tables["rep"].Rows[0]["amount"].ToString();
            //New lable created date 15-June-2016 Cr-ENH-APR-002-2016 by kishore STARTS 
            lblpaymenttype.Text = "Mode of Payment :- " + ds.Tables["rep"].Rows[0]["Mode_of_Pay"].ToString();
            //New lable created date 15-June-2016 Cr-ENH-APR-002-2016 by kishore End 
        }

    }
}
