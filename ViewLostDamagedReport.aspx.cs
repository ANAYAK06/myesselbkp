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


public partial class ViewLostDamagedReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("WareHouse");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 18);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager")
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
        if (Session["roles"].ToString() == "StoreKeeper" || Session["roles"].ToString() == "Central Store Keeper")
            da = new SqlDataAdapter("SELECT  id,Date,Ref_no,cc_code,status from [lost/Damaged Report]where status='4' and cc_code='" + Session["cc_code"].ToString() + "' order by status asc", con);
        else if (Session["roles"].ToString() == "PurchaseManager")
            da = new SqlDataAdapter("SELECT  id,Date,Ref_no,cc_code,status from [lost/Damaged Report]where status='4' and cc_code='CC-33' order by status asc", con);
        else if (Session["roles"].ToString() == "Project Manager")
            da = new SqlDataAdapter("SELECT  id,Date,Ref_no,cc_code,status from [lost/Damaged Report]where (status='1') and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')order by status asc", con);
        else if (Session["roles"].ToString() == "Chief Material Controller")
            da = new SqlDataAdapter("SELECT  id,Date,Ref_no,cc_code,status from [lost/Damaged Report]where (status='2') order by status asc", con);
        else if (Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("SELECT  id,Date,Ref_no,cc_code,status from [lost/Damaged Report]where  status='3' order by status asc", con);

        da.Fill(ds, "central");
        GridView1.DataSource = ds.Tables["central"];
        GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
        GridView1.DataBind();
        
    }
    
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterlostdamagedreport();

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
            filterlostdamagedreport();

        }


        else
        {
            FillGrid();
        }
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
            filterlostdamagedreport();

        }


        else
        {
            FillGrid();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Image img = (Image)e.Row.FindControl("Image1");
            HiddenField hf = (HiddenField)e.Row.FindControl("h1");
            if (hf.Value == "1" || hf.Value == "2" || hf.Value == "3")
                img.Visible = true;
            else
                img.Visible = false;
        }
    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string refno = GridView1.SelectedValue.ToString();
        HiddenField hf = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].Cells[6].FindControl("h1");
        ViewState["date"] = GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text;
        lbltitle.Text = "View Lost Or Damage Report";
        ViewState["ref_no"] = refno;
        btnu.Visible = false;
        grdcmc.Visible = false;
        Grdeditpopup.Visible = false;
        grideviewpopup.Visible = true;
        print.Visible = true;
        mdlfillview();
        printgrid1();
        
        //lbldate.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[3].ToString();
        if (hf.Value == "3")
        {
            popindents.Show();
        }
      
    }
    protected void GridView1_RowEditing2(object sender, GridViewEditEventArgs e)
    {
       
        string refno = GridView1.DataKeys[e.NewEditIndex]["ref_no"].ToString();
        ViewState["ref_no"] = refno;
       
      
        btnu.Visible = true;
        HiddenField hf = (HiddenField)GridView1.Rows[e.NewEditIndex].Cells[6].FindControl("h1");

        if (hf.Value == "1")/*This is for Project Manager*/
        {
            grdcmc.Visible = false;
            grideviewpopup.Visible = false;
            Grdeditpopup.Visible = true;
            print.Visible = false;
            mdlfillgrd();
            lbltitle.Text = "Verify Lost Or Damage Report";
            btnmdlupd.Text = "Verified";
            popindents.Show();
            
            
        }
        else if (hf.Value == "2")/* this is for  Chief Material Controller*/
        {
            grideviewpopup.Visible = false;
            Grdeditpopup.Visible = false;
            grdcmc.Visible = true;
           
            print.Visible = false;
            mdlcmcfillgrd();
         
            lbltitle.Text = "Approve Lost Or Damage Report";
            btnmdlupd.Text = "Approve";
            popindents.Show();
          
        }
        else if (hf.Value == "3")/* this is for  SuperAdmin*/
        {
            grideviewpopup.Visible = false;
            Grdeditpopup.Visible = false;
            grdcmc.Visible = true;

            print.Visible = false;
            mdlcmcfillgrd();

            lbltitle.Text = "Approve Lost Or Damage Report";
            btnmdlupd.Text = "Approve";
            popindents.Show();
           
        }
     

        else 
        {
            popindents.Hide();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        filterlostdamagedreport();
    }
    public void filterlostdamagedreport()
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
                Condition = Condition + "and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
            }
            if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager")
            {
                if (ddlcccode.SelectedValue != "")
                    Condition = Condition + " and cc_code='" + ddlcccode.SelectedValue + "'";

            }
            else if (Session["roles"].ToString() == "StoreKeeper" )
            {
                Condition = Condition + "and cc_code='" + Session["cc_code"].ToString() + "'";
            }
            else
            {
                Condition = Condition + "and cc_code='CC-33'";
 
            }

            if (Session["roles"].ToString() == "StoreKeeper" || Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("SELECT  Date,Ref_no,cc_code,status from [lost/Damaged Report]where status='4'" + Condition + " order by status asc", con);
            else if(Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("SELECT  Date,Ref_no,cc_code,status from [lost/Damaged Report]where status='4'" + Condition + " order by status asc", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("SELECT  Date,Ref_no,cc_code,status from [lost/Damaged Report]where (status='1' or status='4') " + Condition + " order by status asc", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("SELECT  Date,Ref_no,cc_code,status from [lost/Damaged Report]where (status='2' or status='4') " + Condition + " order by status asc", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("SELECT  Date,Ref_no,cc_code,status from [lost/Damaged Report]where  (status='3' or status='4')" + Condition + " order by status asc", con);


            da.Fill(ds, "lost");
            GridView1.DataSource = ds.Tables["lost"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("viewlostDamagedReport.aspx");
    }

    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string cmcremarks = "";
            string Qtys = "";
            string types = "";
            string reporttypes = "";
            int mag = 0;
            cmd = new SqlCommand();
            cmd.Connection = con;

            da = new SqlDataAdapter("select first_name+middle_name+last_name from Employee_Data where user_name='" + Session["user"].ToString() + "'", con);
            da.Fill(ds, "username");


            if (Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager")
            {
                foreach (GridViewRow record in Grdeditpopup.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                    if (c1.Checked)
                    {
                        string id = Grdeditpopup.DataKeys[record.RowIndex]["id"].ToString();
                        string remarks = (record.FindControl("txtremarks") as TextBox).Text;
                        cmd.CommandText = "Update  [lost/Damaged_items] set remarks='" + remarks + '_' + '_' + '_' + '@' + ds.Tables["username"].Rows[0][0].ToString() + "' where id='" + id + "'";
                        con.Open();
                        int j = Convert.ToInt32(cmd.ExecuteNonQuery());
                        con.Close();
                    }

                }
                cmd.CommandText = "Update [lost/Damaged Report] set status='2' where ref_no='" + ViewState["ref_no"].ToString() + "'";
            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
                Int64 num = Convert.ToInt64(ViewState["ref_no"].ToString());

                while (num > 0)
                {
                    mag++;
                    num = num / 10;
                };
                if (mag == 5)
                {
                    foreach (GridViewRow record in grdcmc.Rows)
                    {
                        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                        if (c1.Checked)
                        {

                            string id = "";
                            ids = ids + grdcmc.DataKeys[record.RowIndex]["id"].ToString() + ",";
                            cmcremarks = cmcremarks + ((record.FindControl("txtcmcremarks") as TextBox).Text + '_' + '_' + '_' + '@' + ds.Tables["username"].Rows[0][0].ToString()) + ",";
                            //cmcremarks = cmcremarks + ((record.FindControl("txtcmcremarks") as TextBox).Text + '_' + '_' + '_' + '@' + ds.Tables["username"].Rows[0][0].ToString()) + ",";
                        }
                    }
                    cmd = new SqlCommand("Lost/Damaged Report_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ids", ids);
                    cmd.Parameters.AddWithValue("@remarks", cmcremarks);
                    cmd.Parameters.AddWithValue("@Reportstype", "1");
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@Refno", ViewState["ref_no"].ToString());
                    cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
                }
                else
                {
                    foreach (GridViewRow rec in grdcmc.Rows)
                    {
                        CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");

                        if (c1.Checked)
                        {


                            string id = grdcmc.DataKeys[rec.RowIndex]["id"].ToString();
                            cmcremarks = (rec.FindControl("txtcmcremarks") as TextBox).Text;
                            cmd.CommandText = "Update [lost/Damaged_items] set cmcremarks='" + cmcremarks + '_' + '_' + '_' + '@' + ds.Tables["username"].Rows[0][0].ToString() + "' where id='" + id + "'";
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }

                    }

                    cmd.CommandText = "Update [lost/Damaged Report] set status='3' where ref_no='" + ViewState["ref_no"].ToString() + "'";
                }
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                Int64 num = Convert.ToInt64(ViewState["ref_no"].ToString());

                while (num > 0)
                {
                    mag++;
                    num = num / 10;
                };
                if (mag == 5)
                {
                    foreach (GridViewRow record in grdcmc.Rows)
                    {
                        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                        if (c1.Checked)
                        {

                            string id = "";
                            ids = ids + grdcmc.DataKeys[record.RowIndex]["id"].ToString() + ",";
                            cmcremarks = cmcremarks + ((record.FindControl("txtcmcremarks") as TextBox).Text + '_' + '_' + '_' + '@' + ds.Tables["username"].Rows[0][0].ToString()) + ",";
                        }


                    }
                    cmd = new SqlCommand("Lost/Damaged Report_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ids", ids);
                    cmd.Parameters.AddWithValue("@remarks", cmcremarks);
                    cmd.Parameters.AddWithValue("@Reportstype", "1");
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@Refno", ViewState["ref_no"].ToString());
                    cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
                }
                else
                {
                    foreach (GridViewRow rec in grdcmc.Rows)
                    {
                        CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");

                        if (c1.Checked)
                        {
                            string id = grdcmc.DataKeys[rec.RowIndex]["id"].ToString();
                            cmcremarks = (rec.FindControl("txtcmcremarks") as TextBox).Text;
                            cmd.CommandText = "Update [lost/Damaged_items] set SARemarks='" + cmcremarks + '_' + '_' + '_' + '@' + ds.Tables["username"].Rows[0][0].ToString() + "' where id='" + id + "'";
            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }

                    }

                    cmd.CommandText = "Update [lost/Damaged Report] set status='4' where ref_no='" + ViewState["ref_no"].ToString() + "'";
                }
            }


            con.Open();
            int i = Convert.ToInt32(cmd.ExecuteNonQuery());
            con.Close();
            if (i >= 1)
            {
                JavaScript.UPAlertRedirect(Page,"Sucessfull", "viewlostDamagedReport.aspx");
               
            }
            else
            {
                JavaScript.UPAlertRedirect(Page,"Failed to approve", "viewlostDamagedReport.aspx");
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
       
    }
    public void mdlfillgrd()
    {
        da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,basic_price,Replace(il.quantity,'.00','')quantity,item_status,[Type] as[Type],remarks from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,item_status,Type,remarks from [Lost/Damaged_items] where ref_no='" + ViewState["ref_no"].ToString() + "' )il on i.item_code=substring(il.item_code,1,8)", con);

        da.Fill(ds, "idinfo1");

        Grdeditpopup.DataSource = ds.Tables["idinfo1"];
        Grdeditpopup.DataBind();


    }
    public void mdlcmcfillgrd()
    {
        da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,basic_price,Replace(il.quantity,'.00','')quantity,item_status,[Type] as[Type],remarks,cmcremarks from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,item_status,Type,remarks,cmcremarks from [Lost/Damaged_items] where ref_no='" + ViewState["ref_no"].ToString() + "' )il on i.item_code=substring(il.item_code,1,8)", con);

        da.Fill(ds, "cmcgrid");

        grdcmc.DataSource = ds.Tables["cmcgrid"];
        grdcmc.DataBind();


    }
    public void mdlfillview()
    {
        da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,basic_price,Replace(il.quantity,'.00','')quantity,item_status,[Type] as[Type],remarks,cmcremarks from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,Replace(quantity,'.00','')quantity,item_status,Type,remarks,cmcremarks from [Lost/Damaged_items] where ref_no='" + ViewState["ref_no"].ToString() + "' )il on i.item_code=substring(il.item_code,1,8)", con);

        da.Fill(ds, "fillview");

        grideviewpopup.DataSource = ds.Tables["fillview"];
        grideviewpopup.DataBind();


    }

    public void printgrid1()
    {
        da = new SqlDataAdapter("Select il.item_code,item_name,specification,basic_price,Replace(il.quantity,'.00','')quantity,item_status,[Type] as[Type],remarks,cmcremarks,cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,Replace(quantity,'.00','')quantity,item_status,Type,remarks,cmcremarks,cc_code from [Lost/Damaged_items] where ref_no='" + ViewState["ref_no"].ToString() + "' )il on i.item_code=substring(il.item_code,1,8)", con);
        da.Fill(ds, "print1");
        Griddetails.DataSource = ds.Tables["print1"];
        Griddetails.DataBind();
        Gridremarks.DataSource = ds.Tables["print1"];
        Gridremarks.DataBind();
        lblcode.Text = ds.Tables["print1"].Rows[0].ItemArray[9].ToString();
        lbldate.Text = ViewState["date"].ToString();
    }

    protected void grdcmc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Image img = (Image)e.Row.FindControl("Image1");
            //HiddenField hf = (HiddenField)e.Row.FindControl("h1");
            TextBox txt = (TextBox)e.Row.FindControl("txtcmcremarks");
            if (Session["roles"].ToString() == "Chief Material Controller")
            {
                e.Row.Cells[13].Visible = false;
            }
            else
            {
                e.Row.Cells[13].Visible = true;
            }
            if (Session["roles"].ToString() == "SuperAdmin")
            {
                txt.Enabled = false; 
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
           
            if (Session["roles"].ToString() == "Chief Material Controller")
            {
                e.Row.Cells[13].Visible = false;
            }
            else
            {
                e.Row.Cells[13].Visible = true;
            }
            
        }
    }
}
