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

public partial class ViewAccountdetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Tools");
        lbl.Attributes.Add("class", "active");

       
        if (!IsPostBack)
        {
            fillgrid();

        }

    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Chairman Cum Managing Director")

                da = new SqlDataAdapter("select u.user_name,(first_name+' '+middle_name+' '+last_name)Name,roles from employee_data e join user_roles u on e.user_name=u.user_name", con);
            else
                da = new SqlDataAdapter("select u.user_name,(first_name+' '+middle_name+' '+last_name)Name,roles from employee_data e join user_roles u on e.user_name=u.user_name where u.roles in ('Accountant','Accountant/StoreKeeper','StoreKeeper')", con);

            da.Fill(ds, "Accountdetails");
            GridView1.DataSource = ds.Tables["Accountdetails"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = new DataTable();
            da = new SqlDataAdapter("Select u.cc_code,c.cc_name from cc_user u join cost_center c on u.cc_code=c.cc_code where user_name='" + GridView1.DataKeys[e.Row.RowIndex]["user_name"].ToString() + "' GROUP by u.cc_code,c.cc_name ORDER BY CASE WHEN u.cc_code='CCC' THEN u.cc_code end ASC ,CASE WHEN u.cc_code!='CCC' THEN CONVERT(INT,REPLACE(u.cc_code,'CC-','')) END ASC ", con);
            da.Fill(dt);
            GridView gd = (GridView)e.Row.FindControl("GridView2");
            gd.DataSource = dt;
            gd.DataBind();

        }
    }
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
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
        lblPages.Text = GridView1.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = GridView1.PageIndex + 1;
        lblCurrent.Text = currentPage.ToString();

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

        fillgrid();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex != -1)
        {
            GridView3.EditIndex = -1;
            filldropdown();
            string user_name = GridView1.SelectedValue.ToString();
            ViewState["user1"] = user_name;
            filldrop(user_name);
            popedit.Show();

        }
        else
        {
            JavaScript.UPAlert(Page, "hi");
        }
    }
    public void filldrop(string user_name)
    {
        try
        {
            da = new SqlDataAdapter("Select Roles,cc_code from user_roles u  left outer join cc_user c on u.user_name=c.user_name where u.user_name='" + user_name + "'", con);
            da.Fill(ds, "roleandcc");
            if (ds.Tables["roleandcc"].Rows.Count > 0)
            {

              
                ddlupdrole.SelectedValue = ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString();
                CascadingDropDown5.SelectedValue = ds.Tables["roleandcc"].Rows[0].ItemArray[1].ToString();
                ViewState["role"] = ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString();
                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    uprole.Style.Add("visibility", "Visible");
                    if (ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString() == "Project Manager")
                    {
                        updcc.Style.Add("visibility", "hidden");
                        Table2.Style.Add("visibility", "Visible");
                        btn.Style.Add("visibility", "Visible");
                        ViewState["user"] = user_name;
                        BindGrid();

                    }
                    else if (ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString() == "Accountant" || ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString() == "Accountant/StoreKeeper" || ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString() == "StoreKeeper")
                    {
                        ViewState["user"] = user_name;
                        BindGrid();
                        btn.Style.Add("visibility", "Visible");
                        Table2.Style.Add("visibility", "hidden");
                        updcc.Style.Add("visibility", "Visible");

                    }
                    else
                    {
                        ViewState["user"] = user_name;
                        BindGrid();
                        btn.Style.Add("visibility", "Visible");
                        Table2.Style.Add("visibility", "hidden");
                        updcc.Style.Add("visibility", "hidden");
                    }
                }
                else
                {
                    uprole.Style.Add("visibility", "hidden");
                    if (ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString() == "Accountant" || ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString() == "Accountant/StoreKeeper" || ds.Tables["roleandcc"].Rows[0].ItemArray[0].ToString() == "StoreKeeper")
                    {
                        ViewState["user"] = user_name;
                        updcc.Style.Add("visibility", "Visible");
                        btn.Style.Add("visibility", "Visible");
                        Table2.Style.Add("visibility", "hidden");
                    }
                    else
                    {
                        updcc.Style.Add("visibility", "hidden");
                        Table2.Style.Add("visibility", "hidden");
                        btn.Style.Add("visibility", "Visible");
                    }
                }
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    //public void BindGrid()
    //{
    //    try
    //    {
    //        //da = new SqlDataAdapter("select i.cc_code,c.cc_name from (select cc_code from cc_user where user_name=(Select user_name from employee_data where employee_id='"+ddlemployeename.SelectedValue+"'))i right join cost_center c on i.cc_code=c.cc_code", con);
    //        da = new SqlDataAdapter("Select u.c_id,u.cc_code,cc_name from cc_user u join cost_center c on u.cc_code=c.cc_code where user_name='" + ViewState["user"].ToString() + "'", con);

    //        da.Fill(ds, "uPminfo");
    //        GridView3.DataSource = ds.Tables["uPminfo"];
    //        GridView3.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        Utilities.CatchException(ex);
    //    }
    //}
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView3.EditIndex = e.NewEditIndex;

        BindGrid();
    }
    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView3.EditIndex = -1;

        BindGrid();
    }
    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (ReturnMenuDetails().Tables[0].Rows.Count > 0)
        {
            DropDownList ddlcccode = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlCCcode");

            cmd.CommandText = "Update cc_user set cc_code='" + ddlcccode.SelectedValue + "' where c_id='" + GridView3.DataKeys[e.RowIndex]["c_id"].ToString() + "'";
            cmd.Connection = con;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
                JavaScript.UPAlert(Page, "Updated Successfull");
            else
                JavaScript.UPAlert(Page, "Updating Failed");
            GridView3.EditIndex = -1;
            BindGrid();
        }
        else
        {
            GridView3.EditIndex = -1;
            BindGrid();
            JavaScript.UPAlert(Page, "Update is not possible");

        }
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        if (GridView3.Rows.Count != 1)
        {
            DropDownList ddlcccode = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlCCcode");
            cmd.CommandText = "delete cc_user where c_id='" + GridView3.DataKeys[e.RowIndex]["c_id"].ToString() + "'";
            cmd.Connection = con;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
                JavaScript.UPAlert(Page, "Deleted Successfull");
            else
                JavaScript.UPAlert(Page, "Deleting Failed");
            BindGrid();
        }
        else
        {
            JavaScript.UPAlert(Page, "You are not able to delete");
        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName.Equals("AddNew"))
        //{
        //    TextBox txtNewName = (TextBox)GridView1.dFindControl("txtNewName");
        //    DropDownList cmbNewGender = (DropDownList)GridView1.FooterRow.FindControl("cmbNewGender");
        //    TextBox txtNewCity = (TextBox)GridView1.FooterRow.FindControl("txtNewCity");
        //    TextBox txtNewState = (TextBox)GridView1.FooterRow.FindControl("txtNewState");
        //    DropDownList cmbNewType = (DropDownList)GridView1.FooterRow.FindControl("cmbNewType");

        //    customer.Insert(txtNewName.Text, cmbNewGender.SelectedValue, txtNewCity.Text, txtNewState.Text, cmbNewType.SelectedValue);
        //    FillCustomerInGrid();
        //}
    }

    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        DropDownList ddlfcccode = (DropDownList)GridView3.FooterRow.FindControl("ddlfooterCCcode");

        if (ViewState["role"].ToString() == "Project Manager")
        {
            int k = 0;
            foreach (GridViewRow rec in GridView3.Rows)
            {
                if (ddlfcccode.SelectedItem.Text == (rec.FindControl("lblfcccode") as Label).Text)
                {
                    k = k + 1;
                }
            }
            if (k == 0)
            {
                cmd.CommandText = "Insert Into cc_user(cc_code,user_name)values(@CCCode,@UserName)";
                cmd.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlfcccode.SelectedItem.Text;
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = ViewState["user"].ToString();
                cmd.Connection = con;
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i == 1)
                    JavaScript.UPAlert(Page, "Successfull");
                else
                {
                    JavaScript.UPAlert(Page, "Fail");
                }
            }
            else
            {
                JavaScript.UPAlert(Page, "CC is already exists");
            }
            GridView3.EditIndex = -1;
            BindGrid();

        }
        else
        {
            string s1 = "Insert Into cc_user(cc_code,user_name)values('" + ddlfcccode.SelectedItem.Text + "','" + ViewState["user"].ToString() + "')";
            string s2 = "update user_roles set roles='" + ddlupdrole.SelectedValue + "' where user_name='" + ViewState["user"].ToString() + "'";
            cmd.CommandText = s1 + s2;
            cmd.Connection = con;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 2)
                JavaScript.UPAlert(Page, "Successfull");
            else
            {
                JavaScript.UPAlert(Page, "Fail");
            }
            GridView3.EditIndex = -1;
            BindGrid();
        }
    }
    protected void btnupd_Click(object sender, EventArgs e)
    {
        string s1 = "";
        string s2 = "";
        string s3 = "";
        if (Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
        {
            if (ddlupdrole.SelectedItem.Text != "Project Manager")
            {
                cmd.Connection = con;

                s1 = "update user_roles set roles='" + ddlupdrole.SelectedValue + "' where user_name='" + ViewState["user1"].ToString() + "'";
                if (ddlupdrole.SelectedItem.Text == "Accountant" || ddlupdrole.SelectedItem.Text == "Accountant/StoreKeeper" || ddlupdrole.SelectedItem.Text == "StoreKeeper")
                {

                    s2 = "delete from  cc_user  where user_name='" + ViewState["user1"].ToString() + "'";
                    s3 = "Insert Into cc_user(cc_code,user_name)values('" + ddlupdcc.SelectedValue + "','" + ViewState["user1"].ToString() + "')";
                }
                else if (ddlupdrole.SelectedItem.Text == "Sr.Accountant/Central Store Keeper" || ddlupdrole.SelectedItem.Text == "Central Store Keeper")
                {
                    s2 = "delete from  cc_user  where user_name='" + ViewState["user1"].ToString() + "'";
                    s3 = "Insert Into cc_user(cc_code,user_name)values('CC-33','" + ViewState["user1"].ToString() + "')";

                }
                else
                {
                    s2 = "delete from  cc_user  where user_name='" + ViewState["user1"].ToString() + "'";

                }
                //cmd = new SqlCommand(s1+s2);

                cmd.CommandText = s1 + s2 + s3;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                con.Close();
                if (j == true)
                {
                    JavaScript.UPAlertRedirect(Page, "Updated", "viewAccountdetails.aspx");
                }
                else
                {

                    JavaScript.UPAlert(Page, "Updation Failed");
                }
            }
            else
            {
                // s1 = "Insert Into cc_user(cc_code,user_name)values('" + ddlupdrole.SelectedItem.Text + "','" + ViewState["user1"].ToString() + "')";
                s2 = "update user_roles set roles='" + ddlupdrole.SelectedItem.Text + "' where user_name='" + ViewState["user1"].ToString() + "'";
                cmd.CommandText = s2;
                cmd.Connection = con;
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i == 1)
                    JavaScript.UPAlertRedirect(Page, "Successfull", "ViewAccountdetails.aspx");
                else
                {
                    JavaScript.UPAlert(Page, "Fail");
                }
                GridView3.EditIndex = -1;
                BindGrid();

            }

        }
        else
        {
            cmd.Connection = con;
            s3 = "update cc_user set cc_code='" + ddlupdcc.SelectedValue + "' where user_name='" + ViewState["user1"].ToString() + "'";
            cmd.CommandText = s3;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            bool k = Convert.ToBoolean(cmd.ExecuteNonQuery());
            con.Close();
            if (k == true)
            {
                JavaScript.UPAlertRedirect(Page, "Updated", "viewAccountdetails.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, "Updation Failed");

            }

        }
    }
    public DataSet ReturnMenuDetails()
    {

        DataSet ds = new DataSet();
        using (SqlConnection sqlconn = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]))
        {
            SqlDataAdapter sqldap = new
            SqlDataAdapter("Select u.c_id,u.cc_code,cc_name from cc_user u join cost_center c on u.cc_code=c.cc_code where user_name='" + ViewState["user"].ToString() + "' order by u.cc_code", sqlconn);
            sqldap.Fill(ds);
        }
        return ds;
    }

    public DataTable ReturnEmptyDataTable()
    {
        DataTable dtMenu = new DataTable();
        DataColumn dcMenuID = new DataColumn("c_id", typeof(System.Int32));
        dtMenu.Columns.Add(dcMenuID);
        DataColumn dccode = new DataColumn("cc_code", typeof(System.String));
        dtMenu.Columns.Add(dccode);
        DataColumn dcMenuName = new DataColumn("cc_name", typeof(System.String));
        dtMenu.Columns.Add(dcMenuName);
        DataRow datatRow = dtMenu.NewRow();
        dtMenu.Rows.Add(datatRow);
        return dtMenu;
    }

    public void BindGrid()
    {

        if (ReturnMenuDetails().Tables[0].Rows.Count > 0)
        {
            GridView3.DataSource = ReturnMenuDetails();
        }
        else
        {
            GridView3.DataSource = ReturnEmptyDataTable();
        }
        GridView3.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ddlupdrole.SelectedItem.Text == "Accountant" || ddlupdrole.SelectedItem.Text == "Accountant/StoreKeeper" || ddlupdrole.SelectedItem.Text == "StoreKeeper")
        {

            updcc.Style.Add("visibility", "Visible");
            btn.Style.Add("visibility", "Visible");
            Table2.Style.Add("visibility", "hidden");
        }
        else if (ddlupdrole.SelectedItem.Text == "Project Manager")
        {
            updcc.Style.Add("visibility", "hidden");
            btn.Style.Add("visibility", "Visible");
            Table2.Style.Add("visibility", "Visible");
        }
        else
        {
            updcc.Style.Add("visibility", "hidden");
            btn.Style.Add("visibility", "Visible");
            Table2.Style.Add("visibility", "hidden");
        }
    }

    public void filldropdown()
    {
        da = new SqlDataAdapter("Select Role_Name from Roles", con);
        da.Fill(ds, "details");
        ddlupdrole.DataValueField = "Role_Name";
        ddlupdrole.DataTextField = "Role_Name";
        ddlupdrole.DataSource = ds.Tables["details"];
        ddlupdrole.DataBind();
        ddlupdrole.Items.Insert(0, "Select Role");

    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (GridView3.EditIndex == e.Row.RowIndex)
        //    {
        //        RequiredFieldValidator reqfldVal1 = new RequiredFieldValidator();
        //        reqfldVal1.ID = "RequiredValidator2";
        //        reqfldVal1.ControlToValidate = "ddlCCcode";
        //        reqfldVal1.ErrorMessage = "Cost Center required";
        //        reqfldVal1.SetFocusOnError = true;
        //        e.Row.Cells[1].Controls.Add(reqfldVal1);

        //    }
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlCCcode");
            if (ddl != null)
            {
                da = new SqlDataAdapter("Select cc_code from Cost_Center where cc_code not in (select cc_code from CC_User where user_name='" + ViewState["user1"].ToString() + "') and cc_type='Performing'", con);
                da.Fill(ds, "code");
                ddl.DataTextField = "cc_code";
                ddl.DataValueField = "cc_code";
                ddl.DataSource = ds.Tables["code"];
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Role");
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList ddf = (DropDownList)e.Row.FindControl("ddlfooterCCcode");
            da = new SqlDataAdapter("Select cc_code from Cost_Center where cc_code not in (select cc_code from CC_User where user_name='" + ViewState["user1"].ToString() + "') and cc_type='Performing'", con);
            da.Fill(ds, "codes");
            ddf.DataTextField = "cc_code";
            ddf.DataValueField = "cc_code";
            ddf.DataSource = ds.Tables["codes"];
            ddf.DataBind();
            ddf.Items.Insert(0, "Select Role");
        }
    }

}
