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
using System.Net.Mail;

public partial class Support : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    SqlDataReader read_Email;
    SqlDataReader read_no;

    string f = ConfigurationManager.AppSettings["mailfrom"].ToString();
    string t = ConfigurationManager.AppSettings["mailto"].ToString();
    string t1 = ConfigurationManager.AppSettings["mailcr"].ToString();
    string s = ConfigurationManager.AppSettings["mailcc"].ToString();
    string id = ConfigurationManager.AppSettings["mailid"].ToString();
    string psw = ConfigurationManager.AppSettings["mailpassword"].ToString();
    string smtpaddress = ConfigurationManager.AppSettings["smtpaddress"].ToString();

    string role = "";

    protected void page_PreInit(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        role = Session["roles"].ToString();
        


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
       
       
        finduser();
    }
    public void finduser()
    {
        try
        {
            da = new SqlDataAdapter("select * from itmsregister where username='" + Session["user"].ToString() + "'", con);
            da.Fill(ds, "username");
            if (ds.Tables["username"].Rows.Count > 0)
            {
                if (Session["roles"].ToString() == "Chairman Cum Managing Director" || Session["roles"].ToString() == "ClientAdmin")
                {
                    tdregister.Visible = false;
                    tdestimation.Visible = false;
                    //tdtasks.Visible = false;
                    tdview.Visible = true;
                    tdcrissue.Visible = true;
                    tdtaskstatus.Visible = false;
                    tdentertime.Visible = false;
                    tdviewtasktime.Visible = false;
                    lbtnmail.Visible = false;
                }
                else if (Session["roles"].ToString() == "SupportUser" || Session["roles"].ToString() == "SupportAdmin")
                {
                    //tdtasks.Visible = true;
                    tdview.Visible = true;
                    tdcrissue.Visible = false;
                    tdestimation.Visible = true;
                    tdregister.Visible = true;
                    tdtaskstatus.Visible = true;
                    tdentertime.Visible = true;
                    tdviewtasktime.Visible = true;
                    lbtnmail.Visible = true;
                }
                else if (Session["roles"].ToString() == "ClientUser")
                {
                    //tdtasks.Visible = false;
                    tdview.Visible = true;
                    tdcrissue.Visible = true;
                    tdestimation.Visible = false;
                    tdregister.Visible = false;
                    tdtaskstatus.Visible = false;
                    tdentertime.Visible = false;
                    tdviewtasktime.Visible = false;
                    lbtnmail.Visible = false;
                }
            }
            else if ( Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                tdregister.Visible = false;
                tdestimation.Visible = false;
                //tdtasks.Visible = false;
                tdview.Visible = true;
                tdcrissue.Visible = true;
                tdtaskstatus.Visible = false;
                tdentertime.Visible = false;
                tdviewtasktime.Visible = false;
                lbtnmail.Visible = false;

            }
            else if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "SuperAdmin")
            {                                                                                                                                                                                                                                                                                                                 
                tdregister.Visible = false;
                tdestimation.Visible = false;
                //tdtasks.Visible = false;
                tdview.Visible = true;
                tdcrissue.Visible = true;
                tdtaskstatus.Visible = false;
                tdentertime.Visible = false;
                tdviewtasktime.Visible = false;
                lbtnmail.Visible = false;
            }
            else
            {
                //tdtasks.Visible = false;
                tdview.Visible = false;
                tdcrissue.Visible = false;
                tdestimation.Visible = false;
                tdtaskstatus.Visible = false;
                tdregister.Visible = false;
                tdentertime.Visible = false;
                tdviewtasktime.Visible = false;
                lbtnmail.Visible = false;

            }
        }
        catch(Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        else
        {
            Response.Redirect("MenuContents.aspx");
        }
    }
    protected void lbtnmail_Click(object sender, EventArgs e)
    {
        try
        {            
            ArrayList list_no = new ArrayList();

            
            int j = 0;
            int i;
            //string Query1 = "select distinct e.mail_id from Employee_Data e join esselissue c on c.raised_role=e.user_name where c.status='13'";
            //string Query2 = "select distinct e.mail_id from Employee_Data e join esselcr c on c.raised_role=e.user_name where c.status='13'";
            //string Query3 = Query1+Query2;
            //da = new SqlDataAdapter("select distinct es.cr_issueno from  estimatedhours es join  esselissue i on i.issueno=es.cr_issueno where i.status='13' union all select distinct es.cr_issueno from  estimatedhours es join  esselcr i on i.crno=es.cr_issueno where i.status='13'", con);
            da = new SqlDataAdapter("select distinct e.mail_id from Employee_Data e join esselissue c on c.raised_role=e.user_name where c.status='13' union select distinct e.mail_id from Employee_Data e join esselcr c on c.raised_role=e.user_name where c.status='13'", con);

            da.Fill(ds, "fillemail");
            for (i = 0; i < ds.Tables["fillemail"].Rows.Count; i++)
            {
                con.Open();
                string no = "";
                //string Q1 = "select distinct c.crno from Employee_Data e join esselcr c on c.raised_role=e.user_name where c.status='13' and e.mail_id='" + ds.Tables["fillemail"].Rows[i][0].ToString() + "'";
                //string Q2 = "select distinct c.issueno from Employee_Data e join esselissue c on c.raised_role=e.user_name where c.status='13' and e.mail_id='" + ds.Tables["fillemail"].Rows[i][0].ToString() + "'";
                //string Q3 = Q1+Q2;
                SqlCommand cmd1 = new SqlCommand("select distinct es.cr_issueno from  estimatedhours es join  esselissue i on i.issueno=es.cr_issueno join Employee_Data e on i.raised_role=e.user_name  where i.status='13' and e.mail_id='" + ds.Tables["fillemail"].Rows[i][0].ToString() + "' union all select distinct es.cr_issueno from  estimatedhours es join  esselcr i on i.crno=es.cr_issueno join Employee_Data e on i.raised_role=e.user_name where i.status='13' and e.mail_id='" + ds.Tables["fillemail"].Rows[i][0].ToString() + "'", con);
                SqlDataReader read_no = cmd1.ExecuteReader();
                while (read_no.Read())
                {
                    //no = read_no.GetValue(j).ToString();
                    //list_no.Add(no);
                    j = j + 1 - 1;
                    no = no + read_no.GetValue(j).ToString() + ",";

                }
                read_no.NextResult();
                read_no.Close();
                con.Close();

                MailMessage mail = new MailMessage();
                // Attachment attach = new Attachment(fileAttachement.PostedFile.FileName);
                //mail.From = new MailAddress(f.ToString());
                mail.From = new MailAddress(f, "Essel-IT Support");
                mail.To.Add(ds.Tables["fillemail"].Rows[i][0].ToString());
                mail.CC.Add(t1.ToString());
                mail.Subject = "Some are pending for approval Under UAT";
                mail.Body = "Hi \n\n The below No's are pending for UAT approval\n\n +" + no + " \n\n\n Thanks \n Essel Projects.";
                mail.DeliveryNotificationOptions.ToString();
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = smtpaddress;
                //smtp.Port = 587;
                //smtp.Credentials = new System.Net.NetworkCredential(id.ToString(), psw.ToString());
                //smtp.EnableSsl = true;
                SmtpClient smtp = new SmtpClient("127.0.0.1");
                smtp.Port = 25;
                try
                {
                    smtp.Send(mail);
                }
                catch
                {
                    JavaScript.Alert("Failed to send emails");
                }
                mail = null;
            }
        }
        catch
        {
            JavaScript.Alert("Failed to send emails");
        }
    }
   
}
