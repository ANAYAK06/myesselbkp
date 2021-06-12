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
public partial class SemiAssetDep : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Tools");
        //lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
       
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                fill();
            }
        }
    }

    public void fill()
    {
        da = new SqlDataAdapter("Select return_percent,issue_percent from semiasset_dep", con);
        da.Fill(ds, "Find");
        if (ds.Tables["Find"].Rows.Count > 0)
        {
            ddlissue.SelectedValue = ds.Tables["Find"].Rows[0].ItemArray[1].ToString();
            ddltransfer.SelectedValue = ds.Tables["Find"].Rows[0].ItemArray[0].ToString();
            ViewState["check"] = "False";
            btnupdate.Text = "Update";
        }
        else
        {
            ViewState["check"] = "true";
            ddlissue.SelectedIndex = 0;
            ddltransfer.SelectedIndex = 0;
            btnupdate.Text = "Insert";
        }
        
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "Chairman Cum Managing Director")
        {
            cmd.Connection = con;
            if (ViewState["check"].ToString() == "true")
            {
                cmd.CommandText = "insert into semiasset_dep(return_percent,issue_percent,modified_date) values ('" + ddltransfer.SelectedValue + "','" + ddlissue.SelectedValue + "', getdate())";
            }
            else
            {
                cmd.CommandText = "Update semiasset_dep set return_percent='" + ddltransfer.SelectedValue + "',issue_percent='" + ddlissue.SelectedValue + "',modified_Date= getdate()";
            }
            con.Open();
            int j = Convert.ToInt32(cmd.ExecuteNonQuery());
            if (j > 0)
            {
                JavaScript.UPAlertRedirect(Page, "Successfull", "SemiAssetDep.aspx");
                fill();
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, "Failed", "SemiAssetDep.aspx");
            }
        }


    }
}