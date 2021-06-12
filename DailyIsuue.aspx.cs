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


public partial class DailyIsuue : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Warehouse");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 16);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Project Manager")
        {
            ddlcccode.Visible = true;
            lblcccode.Visible = true;
        }
        else
        {
            ddlcccode.Visible = false;
            lblcccode.Visible = false;
        }

        if (!IsPostBack)
        {
            FillGrid();
            LoadYear();
        }
       
    }
   
    public void LoadYear()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    private void FillGrid()
    {

        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("SELECT Transaction_id as[Transaction Id],date as[Date],cc_code as[CC Code],remarks as [Remarks] from [Daily Issue] where cc_code  in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') order by date desc", con);

            else if (Session["roles"].ToString() == "StoreKeeper")
                da = new SqlDataAdapter("SELECT Transaction_id as[Transaction Id],date as[Date],cc_code as[CC Code],remarks as [Remarks] from [Daily Issue] where  cc_code='" + Session["cc_code"].ToString() + "' order by date desc", con);
            else if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("SELECT Transaction_id as[Transaction Id],date as[Date],cc_code as[CC Code],remarks as [Remarks] from [Daily Issue]  order by date desc", con);

            da.Fill(ds, "central");
            GridView1.DataSource = ds.Tables["central"];


            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterdailyissue();

        }


        else
        {
            FillGrid();
        }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterdailyissue();

        }


        else
        {
            FillGrid();
        }
    }

    protected void lnkbtn_Click(object sender, EventArgs e)
    {

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

        //Populate the GridView Control
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterdailyissue();

        }


        else
        {
            FillGrid();
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Tr_id"] = GridView1.SelectedValue.ToString();
        GridViewRow id = GridView1.SelectedRow;
        string CCCode = id.Cells[2].Text;
        ViewState["CCCode"] = CCCode;
        try
        {
            da = new SqlDataAdapter("Select il.item_code as [Item Code],item_name as [Item Name],specification as [Specification],dca_code as [DCA Code],subdca_code as [Sub DCA],units as [Units],Replace(il.quantity,'.00','')Quantity from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select item_code,quantity from [Daily Issued Items] where  Transaction_id='" + GridView1.SelectedValue.ToString() + "' )il on i.item_code=il.item_code", con);
            da.Fill(ds, "fillview");
             grideviewpopup.DataSource = ds.Tables["fillview"];
            grideviewpopup.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        popdailyissue.Show();
        fillprint();
        pname();
    }

    public void fillprint()
    {
        da = new SqlDataAdapter("Select il.item_code as [Item Code],item_name as [Item Name],specification as [Specification],dca_code as [DCA Code],subdca_code as [Sub DCA],units as [Units],Replace(il.quantity,'.00','')Quantity ,date from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select item_code,quantity,[date] from [Daily Issued Items] d join [Daily Issue] di on d.Transaction_id=di.Transaction_id where  d.Transaction_id='" + ViewState["Tr_id"].ToString() + "' )il on i.item_code=il.item_code", con);
        da.Fill(ds, "print");
        txtcc.Text = ViewState["CCCode"].ToString();
        lblivno.Text = GridView1.SelectedValue.ToString();
        lbldate.Text = ds.Tables["print"].Rows[0].ItemArray[7].ToString();
        Gridprint.DataSource = ds.Tables["print"];
        Gridprint.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        filterdailyissue();
    }
    public void filterdailyissue()
    {
        try
        {
            string Condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    Condition = Condition + " and datepart(mm,date)=" + ddlMonth.SelectedValue + " and datepart(yy,date)=" + yy;
                }
                else
                {
                    Condition = Condition + "and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                }
            }
            if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Project Manager")
            {
                if (ddlcccode.SelectedValue != "Select Cost Center")
                {
                    Condition = Condition + " and cc_code='" + ddlcccode.SelectedValue + "'";
                }
            }
            else
            {
                Condition = Condition + "and cc_code='" + Session["cc_code"].ToString() + "'";
            }

            if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("SELECT Transaction_id as[Transaction Id],date as[Date],cc_code as[CC Code],remarks as [Remarks] from [Daily Issue] where  id>0 " + Condition + "", con);
            else if (Session["roles"].ToString() == "StoreKeeper")
                da = new SqlDataAdapter("SELECT Transaction_id as[Transaction Id],date as[Date],cc_code as[CC Code],remarks as [Remarks] from [Daily Issue] Where id>0 " + Condition + "", con);
            else if(Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("SELECT Transaction_id as[Transaction Id],date as[Date],cc_code as[CC Code],remarks as [Remarks] from [Daily Issue] Where id>0 " + Condition + "", con);



            da.Fill(ds, "issues");
            GridView1.DataSource = ds.Tables["issues"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyIssue.aspx");
    }

    public void pname()
    {
        try
        {
            //da = new SqlDataAdapter("Select i.* from (Select r.user_name,first_name,last_name from employee_data r join user_roles u on r.user_name=u.user_name  where u.Roles ='StoreKeeper')i join cc_user u on i.user_name=u.user_name where cc_code='" +  ViewState["CCCode"].ToString()+ "'", con);
            da = new SqlDataAdapter("Select first_name,last_name from employee_data r join [Daily Issue]u on r.user_name=u.created_by where u.Transaction_id='" + ViewState["Tr_id"].ToString() + "'", con);

            da.Fill(ds, "pnameinfo");
            if (ds.Tables["pnameinfo"].Rows.Count > 0)
            {

                lblstorekeepername.Text = ds.Tables["pnameinfo"].Rows[0][0].ToString() + "  " + ds.Tables["pnameinfo"].Rows[0][1].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
}
