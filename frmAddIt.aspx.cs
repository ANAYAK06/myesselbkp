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


public partial class Admin_frmAddIt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    SqlDataReader dr;
    string ITcode, ITname,ddlit,txtit;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 25);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        
        if (!IsPostBack)
        {
            btnAddIT.Attributes.Add("onclick", "return addit()");
            update.Attributes.Add("onclick", "return updateit()");
            ddlitcode.Style.Add("visibility", "hidden");
            txtitCode.Style.Add("visibility", "visible");
            update.Style.Add("visibility", "hidden");
            btnAddIT.Style.Add("visibility", "visible");
            btnUpdCancel.Style.Add("visibility", "hidden");
            btnCancel.Style.Add("visibility", "visible");
            TextBox1.Style.Add("visibility", "hidden");
        }
        //else
        //{
        //    panel2.Style.Add("visibility", "visible");
        //    panel1.Style.Add("visibility", "hidden");
        //    update.Style.Add("visibility", "visible");
        //    btnAddIT.Style.Add("visibility", "hidden");
        //    btnUpdCancel.Style.Add("visibility", "visible");
        //    btnCancel.Style.Add("visibility", "hidden");
        //}
    }

    [WebMethod]
    public static string IsITCodeAvailable(string itcode)
    {
        //string IT="IT-"+itcode;
        SqlConnection conwb = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter dawb = new SqlDataAdapter("select it_code from it where it_code='" + itcode + "'", conwb);
        DataSet dswb=new DataSet();
        dawb.Fill(dswb, "itcd");
        if (dswb.Tables["itcd"].Rows.Count > 0)
        {
            string itcod = dswb.Tables["itcd"].Rows[0].ItemArray[0].ToString();
            string it = "IT Code Already Exists";
            return it;
        }
        else
        {
            string itc = itcode;
            return itc;
        }
     }
    [WebMethod]
    public static string IsITHeadAvailable(string itname)
    {
        //string IT="IT-"+itcode;
        SqlConnection conwb = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter dawb = new SqlDataAdapter("select it_name from it where it_code='" + itname + "'", conwb);
        DataSet dswb = new DataSet();
        dawb.Fill(dswb, "itcd");
        if (dswb.Tables["itcd"].Rows.Count > 0)
        {
            string itcod = dswb.Tables["itcd"].Rows[0].ItemArray[0].ToString();
            string it = "IT Head Already Exists";
            return it;
            
            
        }
        else
        {
            string itc = itname;
            return itc;
        }
    }
    [WebMethod]
    public static string IsITHnameAvailable(string itname)
    {
        //string IT="IT-"+itcode;
        SqlConnection conwb = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter dawb = new SqlDataAdapter("select it_name from it where it_code='" + itname + "'", conwb);
        DataSet dswb = new DataSet();
        dawb.Fill(dswb, "itcd");
        if (dswb.Tables["itcd"].Rows.Count > 0)
        {
            string itcod = dswb.Tables["itcd"].Rows[0].ItemArray[0].ToString();

            return itcod;
        }
        else
        {
            string itc = "";
            return itc;
        }
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtitCode.Text = "";
        txtithead.Text = "";
    }
    
    protected void update_Click(object sender, EventArgs e)
    {
        ddlit = ddlitcode.SelectedValue;
        txtit = TextBox1.Text;
        string ss = "Update it set it_name='" + txtit + "' where it_code='"+ddlit+"'";
        cmd = new SqlCommand(ss, con);
        con.Open();
        bool k = Convert.ToBoolean(cmd.ExecuteNonQuery());
        if (k == true)
        {
            showalert("Updated Successfully");

        }
        else
        {
            showalert("Updation Failed");
        }

        con.Close();
    }
    protected void btnAddIT_Click(object sender, EventArgs e)
    {
        ITcode=txtitCode.Text;
        ITname=txtithead.Text;
        string sel="INSERT INTO IT(it_code,it_name) VALUES('"+ITcode+"','"+ITname+"')";
        cmd = new SqlCommand(sel, con);
        con.Open();
        bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
        if (j == true)
        {
            showalert("Successfully Inserted");
        }


    }
    public void showalert(string message)
    {
        Label mylable = new Label();
        mylable.Text = "<script language='javascript'>window.alert('" + message + "')</script>";
        Page.Controls.Add(mylable);
    }

    
}
