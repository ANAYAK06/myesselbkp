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


public partial class AddAgency : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAddagency_Click(object sender, EventArgs e)
    {
        try
        {
          
            cmd.Connection = con;
            da = new SqlDataAdapter("select id from agency", con);
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count == 0)
            {
                cmd.CommandText = "insert into Agency(Agencycode,Agencyname,Address) values('TL100',@Agencyname,@Address)";
            }
            else
            {
                cmd.CommandText = "insert into Agency(Agencycode,Agencyname,Address) Select 'TL'+convert(varchar,max(id)+1),@Agencyname,@Address from agency";

            }
            cmd.Parameters.Add("@Agencyname", SqlDbType.VarChar).Value = txtagencyname.Text;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtagencyaddress.Text;
          
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (j == true)
            {
                JavaScript.UPAlertRedirect(Page,"Successfully Added", "AddAgency.aspx");
               

            }
            else
            {
                JavaScript.UPAlert(Page,"Failed");
            }
            con.Close();
        }

        catch (Exception ex)
        {

            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
}
