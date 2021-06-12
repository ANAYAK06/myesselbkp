using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Essel : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd;
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        //timeout1.TimeoutMinutes = HttpContext.Current.Session.Timeout;
        //timeout1.AboutToTimeoutMinutes = HttpContext.Current.Session.Timeout - Convert.ToInt32(ConfigurationManager.AppSettings["AboutToTimeOutMinutes"].ToString());       
        LoadSession();
        Alert.Visible = false;
        message();

        if (!IsPostBack)
        {
            pendingapprovals();
        }
    }
    private void LoadSession()
    {

        if (Session["user"] != null)
        {
            string user = Session["user"].ToString();
            //string sel = "Select user_name,first_name from register where user_name='" + user + "'"
            string sel = "Select i.user_name,first_name,cc_code,roles from (Select r.user_name,first_name,cc_code from employee_data r left outer join cc_user u on r.user_name=u.user_name)i join user_roles u on i.user_name=u.user_name where i.user_name='" + user + "'";

            cmd = new SqlCommand(sel, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                if (dr["first_name"].ToString().Length != 0 && dr["cc_code"].ToString().Length != 0 &&  Session["roles"].ToString().Length != 0)
                {
                    if (dr["roles"].ToString() == "Project Manager")
                    {
                        FirstName.InnerText = Session["user"].ToString();
                        lbtnLogout.Text = "Logout";
                        FirstName.InnerText = dr["first_name"].ToString()+ " / " + Session["roles"].ToString();
                    }
                    else
                    {
                        FirstName.InnerText = Session["user"].ToString();
                        lbtnLogout.Text = "Logout";
                        FirstName.InnerText = dr["first_name"].ToString() + " / " + dr["cc_code"].ToString() + " / " + Session["roles"].ToString();
                    
                    }
                }
                else
                {
                    FirstName.InnerText = Session["user"].ToString();

                    lbtnLogout.Text = "Logout";

                    //hypLnkRegister.Visible = false;

                    FirstName.InnerText = dr["first_name"].ToString() + " / " + "Raipur Office" + " / " + Session["roles"].ToString();
                }

                dr.Close();
            }
            con.Close();
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session.Remove("user");
        Session.Remove("roles");
        Session.Remove("CC_CODE");
        Response.Redirect("~/default.aspx");
    }
    public void message()
    {        

        if (Session["user"] == null)
        {
            Response.Redirect("http://myessel.esselprojects.com/SessionExpire.aspx", false);

        }

        else if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
        {
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
            da = new SqlDataAdapter("sp_verifydate", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@mydate", SqlDbType.DateTime).Value = myDate.ToString();
            da.Fill(ds, "Date");
            if (ds.Tables["Date"].Rows.Count > 0)
            {
                Alert.Visible = true;
            }
        }

    }
    public void pendingapprovals()
    {
        cmd = new SqlCommand("PendingApprovals", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (Session["roles"].ToString() == "Central Store Keeper"||Session["roles"].ToString() == "StoreKeeper")        
            cmd.Parameters.AddWithValue("@CCCode", Session["cc_code"].ToString());

        cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
        cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        pen.InnerText = i.ToString();
    }
}
