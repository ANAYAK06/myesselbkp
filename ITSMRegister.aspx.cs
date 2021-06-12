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

public partial class ITSMRegister : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            UserType();
        }
    }
    public void UserType()
    {
        da = new SqlDataAdapter("select User_Type from UserType order by user_type", con);
        da.Fill(ds, "UserType");
        ddlusertype.DataTextField = "User_Type";
        ddlusertype.DataValueField = "User_Type";
        ddlusertype.DataSource = ds.Tables["UserType"];
        ddlusertype.DataBind();
        ddlusertype.Items.Insert(0, "Select");
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_ITMSRegister", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usertype", ddlusertype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPwd.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@phoneno", txtphno.Text);
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Register Sucessfully")
            {
                JavaScript.AlertAndRedirect(msg, "ITSMRegister.aspx");
            }
            else
            {
                JavaScript.Alert(msg);
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ITSMRegister.aspx");

    }
}
