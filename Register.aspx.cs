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

public partial class Register : System.Web.UI.Page
{
    public string username, firstname, lastname, password, sex, phoneno, email;
    string s1 = "pending";
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnRegister.Attributes.Add("onclick", "return registrationvalidation()");
        }
    }


    // checking user name  starts
    [WebMethod]
    public static bool IsUserAvailable(string username)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlCommand cmd = new SqlCommand("select count(*) from register where user_name ='" + username + "'");
        cmd.Connection = con;
        con.Open();
        int j = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        if (j == 1)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    // checking username ends

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        loginbll regBll = new loginbll();
        username = txtUsername.Text;
        firstname = txtFName.Text;
        lastname = txtLName.Text;
        password = txtPwd.Text;
        phoneno = txtPhoneno.Text;
        email = txtEmail.Text;
        sex = ddlSex.SelectedItem.Text;

        int res = 0;
        try
        {
            res = regBll.RegInsert(username, firstname, lastname, password, phoneno, email, sex, s1);
            if (res == 1)
            {
                JavaScript.AlertAndRedirect("Successfully Registered", "register.aspx");               
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        
    }
    public void showalert(string message)
    {
        Label mylabel = new Label();
        mylabel.Text="<script language='javascript'>window.alert('"+message+"')</script>";
        Page.Controls.Add(mylabel);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtUsername.Text = "";
        txtFName.Text = "";
        txtLName.Text = "";
        txtPwd.Text = "";
        txtCPwd.Text = "";
        txtPhoneno.Text = "";
        txtEmail.Text = "";
        ddlSex.SelectedValue = "--select--";
    }
}
