using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
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
public partial class HRHome : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("HumanResources");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        if (!IsPostBack)
        {
            
            if (Session["roles"].ToString() == "Hr.Asst"||Session["roles"].ToString() == "HR" || Session["roles"].ToString() == "HRGM" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                GridView1.Visible = false;
                GridView2.Visible = false;
                Label1.Visible = false;
                Label2.Visible = false;

                //fillhome();
                //if (Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
                //{
                //    filsalary();
                //}
                ////else if (Session["roles"].ToString() == "CMD")
                ////{
                ////    filsalary();
                ////}
                //else if (Session["roles"].ToString() == "HR" || Session["roles"].ToString() == "HRGM")
                //{
                //    GridView2.Visible = false;
                //}
            }
            else if (Session["roles"].ToString() == "HR.Asst")
            {
                GridView1.Visible = false;
                GridView2.Visible = false;
                Label1.Visible = false;
                Label2.Visible = false;
            }
        }
    }

    public void fillhome()
    {
        //try
        //{

        //    if (Session["roles"].ToString() == "HR")
        //        da = new SqlDataAdapter("select ('A new Employee '+first_name+' is Selected By HR Assistant and waiting for your Approval....') Remarks,'1'as [status] from Employee_Data where status='1' or status='6' ", con);
        //    else if (Session["roles"].ToString() == "HRGM")
        //        da = new SqlDataAdapter("select ('A new Employee '+first_name+' is Selected By HR  and waiting for your Approval....') Remarks,'2'as [status] from Employee_Data where status='2' or status='7' ", con);
        //    else if (Session["roles"].ToString() == "SuperAdmin")
        //        da = new SqlDataAdapter("select ('A new Employee '+first_name+' is Selected By HRGM  and waiting for your Approval....') Remarks,'3'as [status] from Employee_Data where status='3' or status='8' ", con);
        //    else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
        //        da = new SqlDataAdapter("select ('A new Employee '+first_name+' is Selected By SuperAdmin  and waiting for your Approval....') Remarks,'4'as [status] from Employee_Data where status='4' or status='9' ", con);
        //    else
        //        Label1.Visible = false;

        //    da.Fill(ds, "fill");
        //    if (ds.Tables["fill"].Rows.Count > 0)
        //    {
        //        GridView1.DataSource = ds.Tables["fill"];
        //        GridView1.DataBind();
        //    }
        //    else
        //        Label1.Visible = false;

        //}
        //catch (Exception ex)
        //{
        //    Utilities.CatchException(ex);
        //}
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkScrap");
        //    HiddenField hf1 = (HiddenField)e.Row.FindControl("hf1");
        //    lnkbtn.Attributes.Add("onclick", "return Getstatus('" + hf1.Value + "');");

        //}
    }

    public void filsalary()
    {
        //try
        //{

        //    if (Session["roles"].ToString() == "SuperAdmin")
        //        da = new SqlDataAdapter("select ('An Employee '+Employee_id+' salary structure is changed By HRGM  and waiting for your Approval....') Remarks,'4'as [status] from EmployeeSalary_structure where status='4' ", con);
        //    //else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
        //    //    da = new SqlDataAdapter("select ('An Employee '+Employee_id+' salary structure is changed By HRGM  and waiting for your Approval....') Remarks,'5'as [status] from EmployeeSalary_structure where status='5' ", con);
        //    else
        //        Label2.Visible = false;
        //    //else if (Session["roles"].ToString() == "CMD")
        //    //    da = new SqlDataAdapter("select ('A new Employee '+first_name+' is Selected By SuperAdmin  and waiting for your Approval....') Remarks,'2'as [status] from EmployeeSalary_structure where status='2' ", con);

        //    da.Fill(ds, "fill1");
        //    if (ds.Tables["fill1"].Rows.Count > 0)
        //    {
        //        GridView2.DataSource = ds.Tables["fill1"];
        //        GridView2.DataBind();
        //    }
        //    else
        //    {
        //        Label2.Visible = false;
        //    }

        //}
        //catch (Exception ex)
        //{
        //    Utilities.CatchException(ex);
        //}
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    LinkButton lnkbtn1 = (LinkButton)e.Row.FindControl("lnkScrap1");
        //    HiddenField hf2 = (HiddenField)e.Row.FindControl("hf2");
        //    lnkbtn1.Attributes.Add("onclick", "return Getsalstatus('" + hf2.Value + "');");

        //}
    }
}
