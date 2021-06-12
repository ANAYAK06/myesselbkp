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
using System.Net.Mail;


public partial class Estimation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    string f = ConfigurationManager.AppSettings["mailfrom"].ToString();
    string t = ConfigurationManager.AppSettings["mailto"].ToString();
    string s = ConfigurationManager.AppSettings["mailcc"].ToString();
    string id = ConfigurationManager.AppSettings["mailid"].ToString();
    string psw = ConfigurationManager.AppSettings["mailpassword"].ToString();
    string smtpaddress = ConfigurationManager.AppSettings["smtpaddress"].ToString();

    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    string Path = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
           
            rbtntype.SelectedIndex = 0;
            crno();
        }


    }
  

    public void fillpopup()
    {
        //DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
        //string s1 = "insert into EstimatedHours(Cr_IssueNo,Type,BrdTime,TdTime,DevelopmentTime,Description,status,date) values (@no,@type,@BRD,@td,@Developtime,@description,@status,@date)";
        //string s2 = "update esselcr set status=@status where crno=@no";
        //string s3 = "update esselissue set  status=@status where issueno=@no";
        //string s4 = s1 + ';' + s2 + ';' + s3;
        try
        {
            cmd = new SqlCommand("sp_Estimated", con);
            cmd.CommandType = CommandType.StoredProcedure;
            ViewState["no"] = ddlno.SelectedValue;
            cmd.Parameters.AddWithValue("@no", ddlno.SelectedValue);
            cmd.Parameters.AddWithValue("@type", rbtntype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@BRD", txtbrd.Text);
            cmd.Parameters.AddWithValue("@td", txtfd.Text);
            cmd.Parameters.AddWithValue("@Developtime", txtdevelopment.Text);
            cmd.Parameters.AddWithValue("@description", txtdesc.Text);
          
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Estmation Submited")
            {
                JavaScript.AlertAndRedirect(msg, "Estimation.aspx");
            }
            else
            {
                JavaScript.Alert("Failed to Submit"); ;
            }
            con.Close();
          
           // sendmail();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    public void description()
    {

        da = new SqlDataAdapter("select Description from esselcr where crno='" + ddlno.SelectedItem.Text + "'", con);
        //da = new SqlDataAdapter("select crno from esselcr where crno not in(select cr_issueno from estimatedhours) and status='2' order by crno", con);
        da.Fill(ds, "desc");
        ViewState["description"] = ds.Tables["desc"].Rows[0]["Description"].ToString();
    }
    public void sendmail()
    {
        description();
        try
        {
            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            //Msg.From = new MailAddress(f.ToString());
            Msg.From = new MailAddress(f, "Essel-IT Support");
            // Recipient e-mail address.
            Msg.To.Add(t.ToString());
            Msg.CC.Add(s.ToString());
           // Msg.Subject = "New " + ddltype.SelectedItem.Text + " raised by " + Session["roles"].ToString();
            Msg.Subject = "Estimation For " + ViewState["no"].ToString();
            //Msg.Body = "Hi ,\n\n New " + ddltype.SelectedItem.Text + "," + ViewState["NO"].ToString() + " '" + ddlsetstatus.SelectedItem.Text + "' by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["description"].ToString() + " \n\n   ";
            Msg.Body = "Hi ,\n\n Estimation For " + ViewState["no"].ToString() + "  \n\n Description : " + ViewState["description"].ToString() +  "\n\n\n Thanks \n Essel Projects.";

            Msg.DeliveryNotificationOptions.ToString();
            // your remote SMTP server IP.
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

    public void upload()
    {

        try
        {
            //Code to check if user has selected any file on the form
            if (!(brdUpload.HasFile))
            {
                JavaScript.Alert("Please choose BRD File to upload");

            }
            else
            {
               Path = "c:/inetpub/vhosts/esselprojects.com/subdomains/quality/httpdocs/uploads/" + brdUpload.FileName + "";
               // brdUpload.SaveAs(Path);
               // Path = "~/HtmlFile/" + brdUpload.FileName;
                brdUpload.SaveAs(MapPath(Path));

            }
            if (!(tdUpload.HasFile))
            {

                JavaScript.Alert("Please choose TD File to upload");

            }
            else
            {
                Path = "c:/inetpub/vhosts/esselprojects.com/subdomains/quality/httpdocs/uploads/" + tdUpload.FileName + "";
                //tdUpload.SaveAs(Path);
                //Path = "~/HtmlFile/" + tdUpload.FileName;
                tdUpload.SaveAs(MapPath(Path));
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }


    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtntype.SelectedIndex == 0)
            {
                crno();

            }
            else
            {
                Issueno();
                //brd.Visible = false;
                //td.Visible = false;
                //lbldevelopment.Text = "Resolve Time";
               
            }
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
            DataSet ds1 = new DataSet();
            ds1.Clear();
            ddlno.Items.Clear();
            da = new SqlDataAdapter("select crno from esselcr where  status='2' order by crno", con);
            //da = new SqlDataAdapter("select crno from esselcr where crno not in(select cr_issueno from estimatedhours) and status='2' order by crno", con);
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
            DataSet ds1 = new DataSet();
            ds1.Clear();
            ddlno.Items.Clear();
            da = new SqlDataAdapter("select  distinct Issueno from esselIssue where  status='2' and issueno is not null order by Issueno", con);
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            fillpopup();
            upload();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnreject_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Estimation.aspx");
    }
}
