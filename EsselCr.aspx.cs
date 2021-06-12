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
using System.Collections.Specialized;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Net.Mail;

public partial class EsselCr : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    
     string f = ConfigurationManager.AppSettings["mailfrom"].ToString();
     string t = ConfigurationManager.AppSettings["mailcr"].ToString();
     string id = ConfigurationManager.AppSettings["mailid"].ToString();
     string psw = ConfigurationManager.AppSettings["mailpassword"].ToString();
     string smtpaddress = ConfigurationManager.AppSettings["smtpaddress"].ToString();
     SqlDataAdapter da = new SqlDataAdapter();
     SqlCommand cmd = new SqlCommand();
     DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_Esselcrs";
            cmd.Parameters.Add(new SqlParameter("@Type", ddltype.SelectedItem.Text));
            cmd.Parameters.Add(new SqlParameter("@desc", txtdesc.Text));
            cmd.Parameters.Add(new SqlParameter("@roles", Session["user"].ToString()));
            cmd.Connection = con;
            con.Open();
            ViewState["description"] = txtdesc.Text;
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfully Inserted")
            {
                sendmail();
                JavaScript.AlertAndRedirect(msg, "EsselCr.aspx");
            }
            else
            {
                JavaScript.Alert("Insert Failed");
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void name()
    {
        try
        {
            da = new SqlDataAdapter("select (first_name+' '+middle_name+' '+last_name) as name from Employee_Data where user_name='" + Session["user"].ToString() + "'", con);
            da.Fill(ds, "name");
            //lbllabel.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
            if (ds.Tables["name"].Rows.Count > 0)
            {
                ViewState["name"] = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
            }
            else
            {
                ViewState["name"] = "Pradeep";
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    public void sendmail()
    {
        name();

        try
        {
            MailMessage Msg = new MailMessage();
            //Msg.From = new MailAddress(f.ToString());
            Msg.From = new MailAddress(f, "Essel-IT Support");
            Msg.To.Add(t.ToString());
            Msg.Subject = "New " + ddltype.SelectedItem.Text + " raised by " + Session["roles"].ToString();
            Msg.Body = "Hi Pradeep,\n\n New " + ddltype.SelectedItem.Text + " raised by " + ViewState["name"].ToString() + " , " + Session["roles"].ToString() + " \n\n Description : " + ViewState["description"].ToString() + " \n\n Please Approve it for further process " + "\n\n\n Thanks \n SL Touch.";
            Msg.DeliveryNotificationOptions.ToString();
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = smtpaddress;
            //smtp.Port = 587;
            //smtp.Credentials = new System.Net.NetworkCredential(id.ToString(), psw.ToString());
            //smtp.EnableSsl = true;
            SmtpClient smtp = new SmtpClient("127.0.0.1");
            smtp.Port = 25;
            smtp.Send(Msg);
            Msg = null;

        }
        catch (Exception ex)
        {
            Response.Write("error is" + ex);
        }
    }
}
