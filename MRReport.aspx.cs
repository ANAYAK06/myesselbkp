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


public partial class MRReport : System.Web.UI.Page
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
        if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager")
        {
            if (Session["roles"].ToString() == "Project Manager")
                Btnrjct.Visible = true;

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
        if (Session["roles"].ToString() == "StoreKeeper")
            da = new SqlDataAdapter("Select M.id,MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where cc_code='"+Session["cc_code"].ToString()+"' and M.status not in('1','2','3','8') order by M.status asc", con);
        else if (Session["roles"].ToString() == "Project Manager")
            da = new SqlDataAdapter("Select M.id,MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and M.status not in ('2','3','8')  order by M.status asc", con);
        else if (Session["roles"].ToString() == "Central Store Keeper")
            da = new SqlDataAdapter("Select m.Id,MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where M.status not in ('1','3','8') order by M.status asc", con);
        else if (Session["roles"].ToString() == "PurchaseManager")
            da = new SqlDataAdapter("Select m.id,MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where M.status not in ('1','2','8') order by M.status asc", con);
        else if (Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("Select m.id,MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where M.status not in ('1','2','3','8') order by M.status asc", con);
        da.Fill(ds, "central");
        GridView1.DataSource = ds.Tables["central"];
        GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
        GridView1.DataBind();
    }

    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filtermrreport();

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
            filtermrreport();

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
            filtermrreport();

        }


        else
        {
            FillGrid();
        }
    }
    protected void GridView1_RowEditing2(object sender, GridViewEditEventArgs e)
    {
        string pono = GridView1.DataKeys[e.NewEditIndex]["po_no"].ToString();
        ViewState["po_no"] = pono;

        HiddenField hf = (HiddenField)GridView1.Rows[e.NewEditIndex].Cells[8].FindControl("h1");
        ViewState["Status"] = GridView1.Rows[e.NewEditIndex].Cells[7].Text;
        lbltitle.Text = "Verify MR Report";
        if (Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "PurchaseManager")
        {
            checkapprovals(GridView1.Rows[e.NewEditIndex].Cells[4].Text);
        }
        if (hf.Value == "1" || hf.Value == "2")/*This is for Project Manager and central storekeeper and purchasemanager*/
        {
            fillpopup();
            Grdeditpopup.Visible = true;
            //gridcmc.Visible = false;
            btnmdlupd.Visible = true;
            btnpurchase.Visible = false;
            popreports.Show();
        }
        else if (hf.Value == "3")
        {

            fillpopup();
            btnmdlupd.Visible = false;
            btnpurchase.Visible = true;
            Grdeditpopup.Visible = true;
            popreports.Show();
            //popreports.Hide();
        }




        else if (hf.Value == "4")
        {
            popreports.Hide();
        }

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex != -1)
        {
            string pono = GridView1.SelectedValue.ToString();
            ViewState["po_no"] = pono;

            //HiddenField hf = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].Cells[7].FindControl("h1");
            fillpopup();
            btnmdlupd.Visible = false;
            Btnrjct.Visible = false;
            btnpurchase.Visible = false;
            lbltitle.Text = "View MR Report";
            //grideviewpopup.Visible = true;
            Grdeditpopup.Visible = true;
            //gridcmc.Visible = false;
            popreports.Show();
        }
           
            
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image img = (Image)e.Row.FindControl("Image1");
            Label lbl = (Label)e.Row.FindControl("lblmrr");
            HiddenField hf = (HiddenField)e.Row.FindControl("h1");
            if (hf.Value == "1" && e.Row.Cells[7].Text == "3")
            {
                e.Row.Cells[7].Text = "Close";
                lbl.Visible = true;
                img.Visible = true;
            }
            else if ((hf.Value == "1"||hf.Value == "2" || hf.Value == "3") && e.Row.Cells[7].Text != "3")
            {
                e.Row.Cells[7].Text = "";
                lbl.Visible = false;
                img.Visible = true;
            }
            else
            {
                e.Row.Cells[7].Text = "Close";
                lbl.Visible = true;
                img.Visible = false;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        filtermrreport();
    }
    public void filtermrreport()
    {
        try
        {
            string Condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    Condition = Condition + " and datepart(mm,M.recieved_date)=" + ddlMonth.SelectedValue + " and datepart(yy,M.recieved_date)=" + yy;
                }
                Condition = Condition + "and convert(datetime,M.recieved_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
            }
            if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager")
            {
                if (ddlcccode.SelectedValue != "")
                    Condition = Condition + " and cc_code='" + ddlcccode.SelectedValue + "'";

            }
            else
            {
                Condition = Condition + "and cc_code='" + Session["cc_code"].ToString() + "'";
            }
            if (Session["roles"].ToString() == "StoreKeeper")
                da = new SqlDataAdapter("Select MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where  M.status not in('1','2','3') " + Condition + " order by M.status asc", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("Select MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where  M.status not in ('2','3') " + Condition + " order by M.status asc", con);
            else if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("Select MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where M.status not in ('1','3')" + Condition + " order by M.status asc", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("Select MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where M.status not in ('1','2') " + Condition + " order by M.status asc", con);

            else if (Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select MRR_no,M.Po_no,cc_code,m.Recieved_date,m.Remarks,p.Status as [POStatus],M.status from [MR_Report] M join Purchase_details p on M.PO_no=p.Po_no where M.status not in ('1','2','3') " + Condition + " order by M.status asc", con);

            da.Fill(ds, "indents");
            GridView1.DataSource = ds.Tables["indents"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }


        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillpopup()
    {
        try
        {
            da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
            da.Fill(ds, "po_id");
            if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878)
                da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,[Raised Qty],isnull(quantity,0)as quantity,replace(isnull(il.basic_price,0),'.0000','.00') as [Last Purchased Price] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (select item_code,sum(CONVERT(Decimal(18,4),Quantity)) as [Raised Qty],(select SUM(quantity) from [Recieved Items] where PO_no='" + ViewState["po_no"].ToString() + "' and substring(Item_code,1,8)=inl.Item_code)[quantity],COALESCE(New_Basicprice,Basic_Price)as Basic_Price from PO_details inl join Purchase_details p on inl.Po_no=p.Po_no where p.Po_no='" + ViewState["po_no"].ToString() + "' group by item_code,Basic_Price,New_Basicprice)il on i.item_code=substring(il.item_code,1,8)", con);
            else
                da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,[Raised Qty],isnull(quantity,0)as quantity,replace(isnull(il.basic_price,0),'.0000','.00') as [Last Purchased Price] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (select item_code,sum(quantity) as [Raised Qty],(select SUM(quantity) from [Recieved Items] where PO_no='" + ViewState["po_no"].ToString() + "' and substring(Item_code,1,8)=inl.Item_code)[quantity],COALESCE(New_Basicprice,Basic_Price)as Basic_Price from Indent_list inl join Purchase_details p on inl.Indent_no=p.Indent_no where p.Po_no='" + ViewState["po_no"].ToString() + "' group by item_code,Basic_Price,New_Basicprice)il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fill");
                Grdeditpopup.DataSource = ds.Tables["fill"];
                Grdeditpopup.DataBind();          
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    //public void fillviewpop()
    //{
    //    da = new SqlDataAdapter("Select il.id,il.item_code as [Item Code],item_name as[Item Name],specification,dca_code as [DCA Code],subdca_code as [Sub DCA],units,basic_price as [Last Purchased Price],[Raised Qty],il.quantity as[Recieved Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no  where po_no='" + ViewState["pono"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + ViewState["pono"].ToString() + "')il on i.item_code=il.item_code", con);
    //    da.Fill(ds, "fillview");
    //    Grdeditpopup.DataSource = ds.Tables["fillview"];
    //    Grdeditpopup.DataBind();
    
    //}
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("MRReport.aspx");
    }
    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        string itemcode = "";
        string RQty = "";

        try
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                foreach (GridViewRow record in Grdeditpopup.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                    if (c1.Checked)
                    {
                        itemcode = itemcode + Grdeditpopup.DataKeys[record.RowIndex]["Item_code"].ToString() + ",";
                        RQty = RQty + Convert.ToDecimal(record.Cells[9].Text) +"," ;               
                    }
                }
                da = new SqlDataAdapter("VendorStockUpdation_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@itemcodes", itemcode);
                da.SelectCommand.Parameters.AddWithValue("@NewQtys", RQty);             
                da.SelectCommand.Parameters.AddWithValue("@PONo", ViewState["po_no"].ToString());
                da.SelectCommand.Parameters.AddWithValue("@role", Session["roles"].ToString());
                da.Fill(ds, "stockupdated");
                if (ds.Tables["stockupdated"].Rows[0][0].ToString() == "Sucessfull")
                    JavaScript.UPAlertRedirect(Page, "Sucessfull", "MRReport.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Failed", "MRReport.aspx");
                
            }

                //if (ViewState["Status"].ToString() == "Close")
                //    cmd.CommandText = "Update [MR_Report] set status='4' where po_no='" + ViewState["po_no"].ToString() + "'";
                //else
                //    cmd.CommandText = "Update [MR_Report] set status='2' where po_no='" + ViewState["po_no"].ToString() + "'";

                //cmd.Connection = con;
                //con.Open();
                //bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                //if (i == true)
                //    JavaScript.UPAlertRedirect(Page, "Sucessfull", "MRReport.aspx");
                //else
                //    JavaScript.UPAlertRedirect(Page, "Failed", "MRReport.aspx");
                //con.Close();
    
            if (Session["roles"].ToString() == "Central Store Keeper")
            {
                cmd.CommandText = "Update [MR_Report] set status='3' where po_no='" + ViewState["po_no"].ToString() + "' AND status!= 'Rejected'";
                cmd.Connection = con;
                con.Open();
                bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (i == true)
                    JavaScript.UPAlertRedirect(Page, "Sucessfull", "MRReport.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Failed", "MRReport.aspx");
              
                con.Close();
            }
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void gridcmc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtbasic = (TextBox)e.Row.FindControl("txtbasic");
            if (e.Row.Cells[8].Text == "0")
                txtbasic.Attributes.Add("readonly", "1");

        }
    }
    protected void Grdeditpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[8].Text != e.Row.Cells[9].Text)
            if (Convert.ToDouble(e.Row.Cells[8].Text) != Convert.ToDouble(e.Row.Cells[9].Text))
                e.Row.Cells[9].Style.Add("background-color", "red");
        }
    }
    protected void btnpurchase_Click(object sender, EventArgs e)
    {
        try
        {
            string itemcodes = "";
            foreach (GridViewRow record in Grdeditpopup.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    itemcodes = itemcodes + Grdeditpopup.DataKeys[record.RowIndex]["Item_code"].ToString() + ",";
                    Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));

                    //if (record.Cells[4].Text == "DCA-11")
                    //    Amount11 = Amount11 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));
                    //else if (record.Cells[4].Text == "DCA-41")
                    //    Amount41 = Amount41 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));
                    //else if (record.Cells[4].Text == "DCA-27")
                    //    Amount27 = Amount27 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));
                    //else if (record.Cells[4].Text == "DCA-24")
                    //    Amount24 = Amount24 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));
                    if (record.Cells[4].Text == "DCA-11")
                    {
                        if (record.Cells[9].Text != "0")
                        {
                            Amount11 = Amount11 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));
                            //Amount11 = Amount11 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * (Convert.ToDecimal(record.Cells[8].Text) - Convert.ToDecimal(record.Cells[9].Text)));
                        }
                    }
                    else if (record.Cells[4].Text == "DCA-41")
                    {
                        if (record.Cells[9].Text != "0")
                        {
                            Amount41 = Amount41 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));
                            //Amount41 = Amount41 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * (Convert.ToDecimal(record.Cells[8].Text) - Convert.ToDecimal(record.Cells[9].Text)));
                        }
                    }
                    else if (record.Cells[4].Text == "DCA-27")
                    {
                        if (record.Cells[9].Text != "0")
                        {
                            Amount27 = Amount27 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));
                            //Amount27 = Amount27 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * (Convert.ToDecimal(record.Cells[8].Text) - Convert.ToDecimal(record.Cells[9].Text)));
                        }
                    }
                    else if (record.Cells[4].Text == "DCA-24")
                    {
                        if (record.Cells[9].Text != "0")
                        {
                            Amount24 = Amount24 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * Convert.ToDecimal(record.Cells[9].Text));
                            //Amount24 = Amount24 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[6].Text) * (Convert.ToDecimal(record.Cells[8].Text) - Convert.ToDecimal(record.Cells[9].Text)));
                        }
                    }
                }
            }
            cmd = new SqlCommand("MRReport Clearence_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@itemcodes", itemcodes);
            cmd.Parameters.AddWithValue("@PONo", ViewState["po_no"].ToString());
            cmd.Parameters.AddWithValue("@Amount11", Amount11);
            cmd.Parameters.AddWithValue("@Amount41", Amount41);
            cmd.Parameters.AddWithValue("@Amount27", Amount27);
            cmd.Parameters.AddWithValue("@Amount24", Amount24);
            cmd.Parameters.AddWithValue("@POAmount", Amount);
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg != "Sucessfull")
                JavaScript.UPAlert(Page, msg);
            else
                JavaScript.UPAlertRedirect(Page, msg, "MRReport.aspx");
          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    private decimal Amount11 = (decimal)0.0;
    private decimal Amount41 = (decimal)0.0;
    private decimal Amount27 = (decimal)0.0;
    private decimal Amount24 = (decimal)0.0;
    private decimal Amount25 = (decimal)0.0;
    private decimal Amount = (decimal)0.0;
    protected void Btnrjct_Click(object sender, EventArgs e)
    {     
        try
        {           
                da = new SqlDataAdapter("MR_Reject", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;       
                da.SelectCommand.Parameters.AddWithValue("@PONo", ViewState["po_no"].ToString());
                da.Fill(ds, "MRReject");
                if (ds.Tables["MRReject"].Rows[0][0].ToString() == "Successfully Rejected")
                    JavaScript.UPAlertRedirect(Page, "Successfully Rejected", "MRReport.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Failed", "MRReport.aspx");          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    public void checkapprovals(string cc)
    {

        da = new SqlDataAdapter("SELECT TOP 1 * from  closedcost_center where cc_code = '" + cc + "' and status not in('Rejected') ORDER by id desc", con);
        da.Fill(ds, "check");
        if (ds.Tables["check"].Rows.Count > 0)
        {
            if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "1")
            {
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                    JavaScript.AlertAndRedirect("Temporary Closed store approval pending at project Manager", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                    JavaScript.AlertAndRedirect("Store Re-open approval pending at project Manager", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                    JavaScript.AlertAndRedirect("Store permanent closed approval pending at project Manager", "WareHouseHome.aspx");
            }
            if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "2")
            {
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                    JavaScript.AlertAndRedirect("Temporary Closed store approval pending at central store keeper", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                    JavaScript.AlertAndRedirect("Store Re-open approval pending at central store keeper", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                    JavaScript.AlertAndRedirect("Store permanent closed approval pending at central store keeper", "WareHouseHome.aspx");
            }
            if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "3")
            {
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                    JavaScript.AlertAndRedirect("Store is in Temporary Closing Mode", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                    JavaScript.AlertAndRedirect("Store permanent closed approval pending at chief Material Controller", "WareHouseHome.aspx");
            }
            if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "4")
            {
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                    JavaScript.AlertAndRedirect("Store is Already closed", "WareHouseHome.aspx");
            }
        }

    }
}
