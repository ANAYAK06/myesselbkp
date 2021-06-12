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
using System.IO;
using System.Text;
using AjaxControlToolkit;
using System.Web.Services;

public partial class TasksTime : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        if (!IsPostBack)
        {
            if (rbtntype.SelectedIndex == 1)
            {
                rbtntype.SelectedIndex = 1;
            }
            else
            {
                rbtntype.SelectedIndex = 0;
            }
            crno();
            trdesc.Visible = false;
            //SupportUser();
           
        }
     
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_TaskTime", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@no", ddlno.SelectedValue);
            cmd.Parameters.AddWithValue("@type", rbtntype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@time", txttime.Text);
            cmd.Parameters.AddWithValue("@date", txtdate.Text);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());

            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            
            if (msg == "Sucessfull")
            {
                clear();
                JavaScript.AlertAndRedirect(msg, "TasksTime.aspx");
            }
            else
            {
                JavaScript.Alert("Failed to Submit"); ;

            }
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void crno()
    {
        try
        {
           
            ds1.Clear();
            ddlno.Items.Clear();
            da = new SqlDataAdapter("select crno from esselcr where  status not in('1','2','7','8','15') order by crno", con);
            da.Fill(ds1, "number");
            ddlno.DataTextField = "crno";
            ddlno.DataValueField = "crno";
            ddlno.DataSource = ds1.Tables["number"];
            ddlno.DataBind();
            ddlno.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void Issueno()
    {
        try
        {
            
            ds1.Clear();
            ddlno.Items.Clear();
            da = new SqlDataAdapter("select  distinct Issueno from esselIssue where  status not in('1','7','8','15')  order by Issueno", con);
            da.Fill(ds1, "number");
            ddlno.DataTextField = "Issueno";
            ddlno.DataValueField = "Issueno";
            ddlno.DataSource = ds1.Tables["number"];
            ddlno.DataBind();
            ddlno.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    //public void SupportUser()
    //{
    //    try
    //    {
    //        ddluser.Items.Clear();
    //        da = new SqlDataAdapter("select username from itmsregister where user_type='SupportUser' order by username ", con);
    //        da.Fill(ds1, "SupportUser");
    //        ddluser.DataTextField = "username";
    //        ddluser.DataValueField = "username";
    //        ddluser.DataSource = ds1.Tables["SupportUser"];
    //        ddluser.DataBind();
    //        ddluser.Items.Insert(0, "SupportUser");
           

    //    }
    //    catch (Exception ex)
    //    {
    //        Utilities.CatchException(ex);
    //    }
    //}
    public void clear()
    {
        ddlno.SelectedItem.Text = "Select";
        //ddluser.SelectedItem.Text = "SupportUser";
        txttime.Text = "";
        txtdate.Text = "";
        txtdesc.Text = "";
    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtntype.SelectedIndex == 0)
            {
                crno();
                clear();
            }
            else
            {
                Issueno();
                clear();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlno_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
         {
             txtdesc.Text = "";
             da = new SqlDataAdapter("select description from esselcr where crno='"+ddlno.SelectedItem.Text+"' union select description from esselissue where issueno='"+ddlno.SelectedItem.Text+"'", con);
             da.Fill(ds1, "desc");
             if (ds1.Tables["desc"].Rows.Count > 0)
             {
                 txtdesc.Text = ds1.Tables["desc"].Rows[0]["description"].ToString();
                 trdesc.Visible = true;
             }
             else
             {

             }

         }
         catch (Exception ex)
         {
             Utilities.CatchException(ex);
         }
    }
}
