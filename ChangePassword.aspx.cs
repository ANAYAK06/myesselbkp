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

public partial class ChangePassword : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            btnsubmit.Attributes.Add("onclick", "return changepwd()");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {

            loginbll objLgnDal = new loginbll();
            string encrpOPwd = objLgnDal.encryptPass(txtold.Text);
            string encrpNPwd = objLgnDal.encryptPass(txtnew.Text);

            con.Open();

            cmd = new SqlCommand("UPDATE REGISTER SET pwd='" + encrpNPwd + "' WHERE User_Name='" + Session["user"].ToString() + "' and pwd = '" + encrpOPwd + "'", con);
            bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (j == true)
            {
                showalert("Password Changed Successfully");
            }
            else
            {
                showalert("Please Enter Correct OldPassword");

            }

            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
    public void showalert(string message)
    {
        Label mylabel = new Label();
        mylabel.Text = "<script language='javascript'>window.alert('" + message + "')</script>";
        Page.Controls.Add(mylabel);
    }
}
