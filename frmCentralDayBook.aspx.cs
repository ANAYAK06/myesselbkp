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

public partial class Admin_frmCentralDayBook : System.Web.UI.Page
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

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 38);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        //else if (Session["roles"].ToString() != "HoAdmin" || Session["roles"].ToString() != "SuperAdmin")
        //    Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            try
            {
            
                fillgrid();
            }
            catch (Exception ex)
            {
                Utilities.CatchException(ex);
            }
        }
    }

    public void fillgrid()
    {
        da = new SqlDataAdapter("select CC_Code,CC_Name,replace(balance,'.0000','.00') Balance from cost_center where balance<>0", con);
        da.Fill(ds, "central");
        GridView2.DataSource = ds.Tables["central"];
        GridView2.DataBind();
        da = new SqlDataAdapter("select isnull(Balance,0) from CompanyBalance", con);
        da.Fill(ds, "bal");
        lblbal.Text = ds.Tables["bal"].Rows[0][0].ToString().Replace(".0000", ".00");
        da = new SqlDataAdapter("select Sum(balance) from cost_center", con);
        da.Fill(ds, "totbal");
        lbltot.Text = "Total Balance with CostCenters "+ds.Tables["totbal"].Rows[0][0].ToString().Replace(".0000", ".00");
        da = new SqlDataAdapter("select TransferCC,sum(debit) as balance from cash_Transfer where Status not in ('Rejected','3')group by TransferCC", con);
        da.Fill(ds, "debit");
        GridView3.DataSource = ds.Tables["debit"];
        GridView3.DataBind();
    }

    protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}
