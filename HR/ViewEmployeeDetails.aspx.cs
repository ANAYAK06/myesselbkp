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

public partial class ViewEmployeeDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    public void fillgrid()
    {
        try
        {
            if(Session["roles"].ToString()=="Hr.Asst")
                da = new SqlDataAdapter("Select Employee_id,id,first_name+' '+middle_name+' '+last_name as Name,employee_status,[Appointed/Designated as] as Designation,(case when status in ('Hold','Reject') then status else Employee_Status end)as [Status] from employee_data where status in ('4','Activate')  order by id desc", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select Employee_id,id,first_name+' '+middle_name+' '+last_name as Name,employee_status,[Appointed/Designated as] as Designation,(case when status in ('Hold by CMD','Reject by SA') then status else Employee_Status end)as [Status] from employee_data where status in ('3','4','Hold by SA','Reject by SA','Activate')  order by id desc", con);
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                da = new SqlDataAdapter("Select Employee_id,id,first_name+' '+middle_name+' '+last_name as Name,employee_status,[Appointed/Designated as] as Designation,(case when status in ('Hold by CMD','Reject by CMD') then status else Employee_Status end)as [Status] from employee_data where status in ('3A','4','Hold by CMD','Reject by CMD','Activate')  order by id desc", con);
          
            da.Fill(ds, "employeedata");
            if (ds.Tables["employeedata"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["employeedata"];
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();

    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //--- For Paging ---------
        GridViewRow row = GridView1.BottomPagerRow;

        if (row == null)
        {
            return;
        }

        //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
        Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
        Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

        //if (lblPages != null)
        //{
        lblCurrent.Text = GridView1.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = GridView1.PageIndex + 1;
        lblPages.Text = currentPage.ToString();

        //-- For First and Previous ImageButton
        if (GridView1.PageIndex == 0)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Visible = false;



        }

        //-- For Last and Next ImageButton
        if (GridView1.PageIndex + 1 == GridView1.PageCount)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Visible = false;


        }
    }
    protected void btnFirst_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnPrev_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnNext_Command(object sender, CommandEventArgs e)
    {

        Paginate(sender, e);
    }
    protected void btnLast_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void Paginate(object sender, CommandEventArgs e)
    {
        // Get the Current Page Selected
        int iCurrentIndex = GridView1.PageIndex;

        switch (e.CommandArgument.ToString().ToLower())
        {
            case "first":
                GridView1.PageIndex = 0;
                break;
            case "prev":
                if (GridView1.PageIndex != 0)
                {
                    GridView1.PageIndex = iCurrentIndex - 1;
                }
                break;
            case "next":
                GridView1.PageIndex = iCurrentIndex + 1;
                break;
            case "last":
                GridView1.PageIndex = GridView1.PageCount;
                break;
        }

        fillgrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Image img = (Image)e.Row.FindControl("Image1");
            //HiddenField hf = (HiddenField)e.Row.FindControl("h1");
            //if (Session["roles"].ToString() == "Hr.Asst")
            //    img.Visible = false;
            //else if (Session["roles"].ToString() == "HR" && hf.Value == "1")
            //    img.Visible = true;
            //else if (Session["roles"].ToString() == "HR" && hf.Value != "1")
            //    img.Visible = false;
            //else if (Session["roles"].ToString() == "HRGM" && hf.Value == "2")
            //    img.Visible = true;
            //else if (Session["roles"].ToString() == "HRGM" && hf.Value != "2")
            //    img.Visible = false;
            ////else if (Session["roles"].ToString() == "SuperAdmin" && hf.Value == "3")
            ////    img.Visible = true;
            ////else if (Session["roles"].ToString() == "SuperAdmin" && hf.Value != "3")
            ////    img.Visible = false;
            //else if (Session["roles"].ToString() == "Chairman Cum Managing Director" && hf.Value == "3")
            //    img.Visible = true;
            //else if (Session["roles"].ToString() == "Chairman Cum Managing Director" && hf.Value != "3")
            //    img.Visible = false;

            HiddenField hf = (HiddenField)e.Row.FindControl("h1");
            HyperLink hlnk = (HyperLink)e.Row.FindControl("HyperLink1");
            if (hf.Value != "Existing")
            {

                hlnk.Visible = true;
            }
            else
            {
                hlnk.Visible = false;
            }

        }
    }
}