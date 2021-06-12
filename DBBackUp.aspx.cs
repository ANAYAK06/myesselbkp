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

public partial class Admin_DBBackUp : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Session["user"] == null)
        {
            Response.Redirect("~/common/login.aspx");
            
        } 
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = String.Format("{0:dd-MM-yyyy}", System.DateTime.Now);
            SqlCommand cmd = new SqlCommand("BACKUP DATABASE EsselDB TO DISK = N'C:\\inetpub\\vhosts\\esselprojects.com\\subdomains\\myessel\\httpdocs\\DBBackUP\\" + filename + ".bak'" +
    "WITH NOFORMAT, NOINIT, NAME = N'EsselDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10", con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.CommandTimeout = 0;
            con.Close();
            Response.Write("Database back up completed succesfully. Download " + filename + ".bak from ftp");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
}
