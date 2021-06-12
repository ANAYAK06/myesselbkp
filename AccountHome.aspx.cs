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

public partial class AccountHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
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
            if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager")
            {
                alertpo();
            }
        }
        
    }
    public void alertpo()
    {
        if (Session["roles"].ToString() != "Project Manager")
             da = new SqlDataAdapter("select po_no from po where status=3 and DATEDIFF(DAY,GETDATE(),POComdate)='60'", con);
        else
             da = new SqlDataAdapter("select po_no from po where status=3 and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);

        da.Fill(ds, "alert");
        if (ds.Tables["alert"].Rows.Count > 0)
        {           
            for (int i = 0; i < ds.Tables["alert"].Rows.Count; i++)
            {
                lbl.Text = lbl.Text +"  &#9733  "+ "This PO " + ds.Tables["alert"].Rows[0][0].ToString() + "  is have 60 days to complete" + "  &#9733  ";
            }
        }
        //Repeater1.DataSource = ds;
        //Repeater1.DataBind();


    }





    
}
