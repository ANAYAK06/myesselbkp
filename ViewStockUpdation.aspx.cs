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

public partial class ViewStockUpdation : System.Web.UI.Page
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
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 19);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");        
        if (!IsPostBack)
        {
            LoadYear();
            FillGrid();
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
       if (Session["roles"].ToString() == "Chief Material Controller" )
           da = new SqlDataAdapter("select ID,Request_no,CC_code,Req_date,Description,status from StockUpdation where status in ('1')", con);
       else if(Session["roles"].ToString() == "SuperAdmin")
           da = new SqlDataAdapter("select ID,Request_no,CC_code,Req_date,Description,status from StockUpdation where status in ('2')", con);
        da.Fill(ds, "stock");
        GridView1.DataSource = ds.Tables["stock"];

        GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
        GridView1.DataBind();
    }
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 )
        {
            filterstockreq();

        }

        else
        {
            FillGrid();
        }
    }
    public void filterstockreq()
    {
        try
        {
            string Condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    Condition = Condition + " and datepart(mm,Req_date)=" + ddlMonth.SelectedValue + " and datepart(yy,Req_date)=" + yy;
                }
                Condition = Condition + "and convert(datetime,Req_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
            }
            if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select ID,Request_no,CC_code,Req_date,Description,status from StockUpdation where status in ('1')", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select ID,Request_no,CC_code,Req_date,Description,status from StockUpdation where status in ('2')", con);
            da.Fill(ds, "viewstock");
            GridView1.DataSource = ds.Tables["viewstock"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
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
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0)
        {
            filterstockreq();

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
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0)
        {
            filterstockreq();

        }

        else
        {
            FillGrid();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        filterstockreq();
    }
    protected void GridView1_RowEditing2(object sender, GridViewEditEventArgs e)
    {
        string Request_no = GridView1.DataKeys[e.NewEditIndex]["Request_no"].ToString();
        ViewState["Request_no"] = Request_no;
        fillpopup();
        Grdeditpopup.Visible = true;           
        btnmdlupd.Visible = true;
        popreports.Show();
            
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex != -1)
        {
            string Request_no = GridView1.SelectedValue.ToString();
            ViewState["Request_no"] = Request_no;           
            fillpopup();
            btnmdlupd.Visible = false;          
            lbltitle.Text = "View Stock Updation";            
            Grdeditpopup.Visible = true;           
            popreports.Show();
        }
    }
    public void fillpopup()
    {
        try
        {
            da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,[Request Qty],replace(isnull(basic_price,0),'.0000','.00')as [Last Purchased Price],Amount,il.id,(select quantity from [master_data] where item_code=il.item_code and cc_code='CC-33') as[Avail_Qty],(select status from StockUpdation where Request_no='" + ViewState["Request_no"].ToString() + "') as [Status] from (Select item_code,item_name,specification,dca_code,subdca_code,units,Basic_Price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (select item_code,Amount,sum(quantity) as [Request Qty],id from StockUpdate_Items inl where Request_no='" + ViewState["Request_no"].ToString() + "' group by item_code,Amount,id)il on i.item_code=substring(il.item_code,1,8)", con);
            da.Fill(ds, "fill");
            Grdeditpopup.DataSource = ds.Tables["fill"];
            Grdeditpopup.DataBind();          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }



    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewStockUpdation.aspx");
    }
  
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image img = (Image)e.Row.FindControl("Image1");
            Label lbl = (Label)e.Row.FindControl("lblreq");
            HiddenField hf = (HiddenField)e.Row.FindControl("h1");

            if (hf.Value == "1" || hf.Value == "2")
            {
                lbl.Visible = true;
                img.Visible = true;           
            }           
           
        }
    }
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        cmd.Connection = con;
    //        con.Open();
    //        char c = txtSearch.Text[0];
    //        if (char.IsNumber(c))
    //        {
    //            da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_code='" + txtSearch.Text + "'", con);
    //            da.Fill(ds, "checks");
    //            if (ds.Tables["checks"].Rows.Count == 1)
    //            {
    //                cmd.CommandText = "insert into StockUpdate_Items(item_code,Request_no,ItemType) values(@itemcode,@Request_no,@ItemType)";
    //                cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = txtSearch.Text;
    //                cmd.Parameters.Add("@Request_no", SqlDbType.VarChar).Value = ViewState["Request_no"].ToString();
    //                cmd.Parameters.Add("@ItemType", SqlDbType.VarChar).Value = ddlsearchtype.SelectedItem.Text;
    //                int i = Convert.ToInt32(cmd.ExecuteScalar());
    //                con.Close();
    //                if (i == 1)
    //                {
    //                }
    //                fillpopup();
    //                (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
    //                txtSearch.Text = String.Empty;
    //            }
    //            else
    //            {
    //                JavaScript.UPAlert(Page, "Invalid itemcode");
    //                (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
    //                txtSearch.Text = String.Empty;
    //            }

    //        }
    //        else
    //        {
    //            int n = txtSearch.Text.IndexOf(",");
    //            if (n != -1)
    //            {
    //                string result = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
    //                string result1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
    //                da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_name='" + result + "' and specification='" + result1 + "'", con);
    //                da.Fill(ds, "check");
    //                if (ds.Tables["check"].Rows.Count == 1)
    //                {
    //                    da = new SqlDataAdapter("Select item_code from item_codes where item_name='" + result + "' and specification='" + result1 + "'", con);
    //                    da.Fill(ds, "search");
    //                    if (ds.Tables["search"].Rows.Count > 0)
    //                    {
    //                        cmd.CommandText = "insert into StockUpdate_Items(item_code,Request_no,ItemType) values(@itemcode,@Request_no,@ItemType)";
    //                        cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = ds.Tables["search"].Rows[0].ItemArray[0].ToString();
    //                        cmd.Parameters.Add("@Request_no", SqlDbType.VarChar).Value = ViewState["Request_no"].ToString();
    //                        cmd.Parameters.Add("@ItemType", SqlDbType.VarChar).Value = ddlsearchtype.SelectedItem.Text;

    //                        int j = Convert.ToInt32(cmd.ExecuteScalar());
    //                        con.Close();
    //                        if (j == 1)
    //                        {
    //                        }

    //                        fillpopup();
    //                        (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
    //                        txtSearch.Text = String.Empty;
    //                    }
    //                }
    //                else
    //                {
    //                    JavaScript.UPAlert(Page, "Invalid Specification");
    //                    (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
    //                    txtSearch.Text = String.Empty;
    //                }

    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Utilities.CatchException(ex);
    //    }
    //}
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow record in Grdeditpopup.Rows)
    //    {
    //        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
    //        if (c1.Checked)
    //        {
    //            string id = Grdeditpopup.DataKeys[record.RowIndex]["id"].ToString();
    //            cmd.Connection = con;
    //            cmd.CommandText = "delete from StockUpdate_Items where id='" + id + "'";
    //            con.Open();
    //            cmd.ExecuteNonQuery();
    //            con.Close();
    //        }
    //    }
    //    fillpopup();
    //}
    //protected void ddlsearchtype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    cascadingDCA cs = new cascadingDCA();
    //    cs.values(ddlsearchtype.SelectedValue);
    //}
   
    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        try
        {

            string Sids = "";
            string SQty = "";
            string SAmt = "";

            foreach (GridViewRow record in Grdeditpopup.Rows)
            {

                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                if (c1.Checked)
                {
                    Sids = Sids + Grdeditpopup.DataKeys[record.RowIndex]["id"].ToString() + ",";
                    SQty = SQty + (record.FindControl("txtqty") as TextBox).Text + ",";
                    SAmt = SAmt + (record.FindControl("txtamount") as TextBox).Text + ",";
                }


            }
            cmd = new SqlCommand("Stockupdate_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ids", Sids);
            cmd.Parameters.AddWithValue("@NewQtys", SQty);
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["User"].ToString());
            cmd.Parameters.AddWithValue("@NewAmts", SAmt);
            cmd.Parameters.AddWithValue("@Request_no", ViewState["Request_no"].ToString());
            cmd.Connection = con;
            con.Open();

            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Verified Sucessfully")
                JavaScript.UPAlertRedirect(Page, msg, "ViewStockUpdation.aspx");
            else
                JavaScript.UPAlert(Page, msg);

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void btnreject_Click(object sender, EventArgs e)
    {
        string Request_no = ViewState["Request_no"].ToString();
        cmd.Connection = con;
        cmd.CommandText = "delete from stockupdation where Request_no='" + Request_no + "'; delete from StockUpdate_Items where Request_no='" + Request_no + "'";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        JavaScript.UPAlertRedirect(Page, "Request Rejected Succesfully", "ViewStockUpdation.aspx");     

    }


    protected void Grdeditpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {          
         
            HiddenField hf = (HiddenField)e.Row.FindControl("h2");

            if (hf.Value == "1")
            {
                btnreject.Visible = false;
                e.Row.Cells[8].Enabled = false;
            }
            if (hf.Value == "2")
            {
                btnreject.Visible = true;
                e.Row.Cells[8].Enabled = true;
            }

        }
    }
}