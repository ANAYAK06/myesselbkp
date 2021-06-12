using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{

   
	public Utilities()
	{
		//
		// TODO: Add constructor logic here
		//       
    }

    #region ConectionString
    /// <summary>
    /// Returns an instance of connection string
    /// </summary>
    public static SqlConnection con()
    {
        return new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    }
    #endregion ConectionString

    #region ExceptionManagement
    /// <summary>
    /// Cathes the exceptions raised
    /// </summary>
    public static string CatchException(Exception ex)
    {
        string mailfrom = ConfigurationManager.AppSettings["mailfrom"].ToString();
        System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
        //"<tr><td>Description:</td><td>" + ex.InnerException + "</td></tr>" +
        string body = "<table><tr><td>Exception</td><td>" + ex.ToString() + "</td></tr>" +
            "<tr><td>Exception Type:</td><td>" + ex.GetType() + "</td></tr>" +
            "<tr><td>MESSAGE: " + ex.Message + "</td></tr>" +
             "<tr><td>SOURCE: </td><td>" + ex.Source + "</td></tr>" +
             "<tr><td>FORM: </td><td>" + HttpContext.Current.Request.Url.ToString() + "</td></tr>" +
             "<tr><td>QUERYSTRING: </td><td>" + HttpContext.Current.Request.QueryString.ToString() + "</td></tr>" +
             "<tr><td>TARGETSITE: </td><td>" + ex.TargetSite.ToString() + "</td></tr>" +
             "<tr><td>Method Name: </td><td>" + trace.GetFrame(0).GetMethod().Name + "</td></tr>" +
             "<tr><td>Line No: </td><td>" + trace.GetFrame(0).GetFileLineNumber().ToString() + "</td></tr>" +
             "<tr><td>Column No: </td><td>" + trace.GetFrame(0).GetFileColumnNumber().ToString() + "</td></tr>" +
             "<tr><td>STACKTRACE: </td><td>" + ex.StackTrace + "</td></tr></table>";
        SendMail(mailfrom, "An Error Raised at Essel Projects", body, null);
        HttpContext.Current.Response.Redirect("~/ErrorPage.aspx");
        return "Error Processing Request";
    }
    #endregion ExceptionManagement

    #region MailSending
    /// <summary>
    /// Method to send mails with attachments
    /// </summary>
    public static bool SendMail(string to, string subject, string body, Attachment[] atch)
    {
        MailMessage mail1 = new MailMessage();
        string mailfrom = ConfigurationManager.AppSettings["mailfrom"].ToString();
        try
        {
            mail1.From = new MailAddress(mailfrom, "Essel Projects Pvt. Ltd.");
            mail1.To.Add(to);
            mail1.IsBodyHtml = true;
            mail1.Subject = subject;
            mail1.Body = body;
            if (atch != null)
            {
                for (int a = 0; a < atch.Length; a++)
                {
                    if (atch[a] != null)
                        mail1.Attachments.Add(atch[a]);
                }
            }

            //.............Gmail server credentials
            //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);//465
            //smtp.EnableSsl = true;
            //SmtpClient smtp = new SmtpClient("relay-hosting.secureserver.net");
            //smtp.UseDefaultCredentials = true;
            //smtp.Credentials = new NetworkCredential("esselsupport@industouch.com", "industouch");            

            //.............General Mailing credentials
            SmtpClient smtp = new SmtpClient("127.0.0.1");
            smtp.Port = 25;

            smtp.Send(mail1);            

            return true;
        }
        catch (Exception ex)
        {
            //CatchException(ex);
            return false;
        }
        finally
        {
            mail1.Dispose();
        }
    }
    #endregion MailSending

    #region FinancialYear
    /// <summary>
    /// Returns the financial year for given date
    /// </summary>
    public static string FinancialYear(DateTime date)
    {
        if (date.Month < 4)
            return (date.Year - 1).ToString() + "-" + (date.Year).ToString().Substring(2, 2);
        else
            return date.Year.ToString() + "-" + (date.Year + 1).ToString().Substring(2, 2);
    }
    /// <summary>
    /// Returns the current financial year
    /// </summary>
    public static string CurrentFinancialYear()
    {
        if (System.DateTime.Now.Month < 4)
            return (System.DateTime.Now.Year - 1).ToString() + "-" + (System.DateTime.Now.Year).ToString().Substring(2, 2);
        else
            return System.DateTime.Now.Year.ToString() + "-" + (System.DateTime.Now.Year + 1).ToString().Substring(2, 2);
    }
    #endregion FinancialYear

    
}
