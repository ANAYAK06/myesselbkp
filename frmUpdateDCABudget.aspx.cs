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
using System.Web.Services;
using System.Data.SqlClient;

public partial class Admin_frmUpdateDCABudget : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 36);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");        
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("sp_Update_DcaBudget", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Type", rbtntype.SelectedValue);
        cmd.Parameters.AddWithValue("@CCCode", ddlCCcode.SelectedValue);
        cmd.Parameters.AddWithValue("@Dca", ddlDCAcode.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
        cmd.Parameters.AddWithValue("@Amount", txtBudget.Text);
        cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
        con.Open();
        try
        {
            JavaScript.AlertAndRedirect(cmd.ExecuteScalar().ToString(), Request.Url.ToString());
        }
        catch (Exception ex)
        {
            JavaScript.Alert(Utilities.CatchException(ex));
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}

