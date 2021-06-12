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

public partial class Admin_frmEmployees : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("HumanResources");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 54);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            FillGrid();
        }
        if (GridView1.PageIndex==1 && GridView1.PageCount<=10)
        {
            ddlpagecount.Style.Add("visibility", "hidden");
            lbllimit.Style.Add("visibility", "hidden");
        }
        else
        {
            ddlpagecount.Style.Add("visibility", "visible");
            lbllimit.Style.Add("visibility", "visible");
        }

    }
    private void FillGrid()
    {
        da = new SqlDataAdapter("SELECT r.id,r.user_name,[first_name],[middle_name],[last_name],REPLACE(CONVERT(VARCHAR(11),date_of_joining, 106), ' ', '-')as[date_of_joining],[Category],[Department],[mail_id],[Mobile_no],a.roles,u.cc_code,[Employee_status] from Employee_data r left outer join CC_User u on r.User_Name=u.User_Name left outer join user_roles a on r.User_Name=a.User_Name", con);
        da.Fill(ds, "central");
        GridView1.DataSource = ds.Tables["central"];

        GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
        GridView1.DataBind();
    }
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.BottomPagerRow;

        if (row == null)
        {
            return;
        }

        Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
        Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");


        lblCurrent.Text = GridView1.PageCount.ToString();



        int currentPage = GridView1.PageIndex + 1;
        lblPages.Text = currentPage.ToString();




        if (GridView1.PageIndex == 0)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Visible = false;



        }

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

        FillGrid();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure " +
                               "you want to delete this record?')) return true;";
                }
            }
        }
      
       
    }

   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", "window.open('frmEmployeeData.aspx','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');", true);

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex]["id"].ToString();
        DeleteRecord(id);//call delete method
        FillGrid();//rebind grid to reflect changes made
    }
   
       
    
    private void DeleteRecord(string ID)
    {
        string sqlStatement = "DELETE FROM Employee_data WHERE id = @userID and status='Deactivate'";
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, con);
            cmd.Parameters.AddWithValue("@userID", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Deletion Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        finally
        {
            con.Close();
        }
    }
}
