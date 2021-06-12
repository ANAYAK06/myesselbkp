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

public partial class BasicPriceUpdationNew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();

    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    string s1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "SuperAdmin")
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
            LoadYear();
            FillGrid();
            popup.Visible = false;
            ChangeControlStatus();
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

        if (Session["roles"].ToString() == "Project Manager")
            da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='5') and p.paymenttype='Supplier' and p.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') order by p.cc_code asc", con);
        else if (Session["roles"].ToString() == "Central Store Keeper")
            da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status in ('6','7A')) and p.paymenttype='Supplier' order by m.Status,m.PO_no asc", con);
        else if (Session["roles"].ToString() == "Chief Material Controller")
            da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status in('7','7A')) and p.paymenttype='Supplier'  order by  m.Status,m.PO_no asc", con);
        else if (Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='7A') and p.paymenttype='Supplier'  order by p.cc_code asc", con);

        da.Fill(ds, "central");
        GridView1.DataSource = ds.Tables["central"];
        GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
        GridView1.DataBind();
    }
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterbasicprice(s1);

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
            filterbasicprice(s1);

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
            filterbasicprice(s1);
        }
        else
        {
            FillGrid();
        }
    }
    protected void GridView1_RowEditing2(object sender, GridViewEditEventArgs e)
    {
        clearviewstate();
        btnPrint.Visible = false;
        ChangeControlStatus();
        string pono = GridView1.DataKeys[e.NewEditIndex]["po_no"].ToString();
        ViewState["po_no"] = pono;
        ViewState["cccode"] = GridView1.DataKeys[e.NewEditIndex]["cc_code"].ToString();
        ViewState["mrr"] = GridView1.DataKeys[e.NewEditIndex]["MRR_no"].ToString();
        string Invoice = GridView1.Rows[e.NewEditIndex].Cells[4].Text;
        da = new SqlDataAdapter("CheckBalance_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@invno", SqlDbType.VarChar).Value = Invoice;
        da.Fill(ds, "Check");
        if (ds.Tables["Check"].Rows.Count > 0)
        {
            if (ds.Tables["check"].Rows[0].ItemArray[0].ToString() != "0.00")
            {
                hbasiprice.Value = ds.Tables["Check"].Rows[0][0].ToString().Replace(".0000", ".00");
                hfpoamount.Value = ds.Tables["Check"].Rows[0][1].ToString().Replace(".0000", ".00");
                if (Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
                {
                    cmd = new SqlCommand("checkamendbudget", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PoNo", ViewState["po_no"].ToString());
                    cmd.Parameters.AddWithValue("@CCCode", GridView1.DataKeys[e.NewEditIndex]["cc_code"].ToString());
                    cmd.Connection = con;
                    con.Open();
                    string result = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if (result == "success")
                    {
                        
                        ViewState["invoiceno"] = GridView1.Rows[e.NewEditIndex].Cells[4].Text;
                        hfrole.Value = Session["roles"].ToString();
                        HiddenField hf = (HiddenField)GridView1.Rows[e.NewEditIndex].Cells[8].FindControl("h1");
                        ViewState["code"] = GridView1.Rows[e.NewEditIndex].Cells[6].Text;
                        if (hf.Value == "5")/*This is for siteincharge  */
                        {
                            fillpopview();
                            btnreject.Visible = false;
                            lbltitle.Text = "Verify Basic Price";
                            Grdeditpopup.Visible = true;
                            gridcmc.Visible = false;
                            grdcentral.Visible = false;                           
                            btnmdlupd.Text = "Verified";
                            popup.Visible = true;
                            InvoiceInfo();
                        }
                        else if (hf.Value == "6")/*This is for central storekeeper */
                        {
                            if (Session["roles"].ToString() == "Central Store Keeper")
                            {
                                fillpopview();
                                btnreject.Visible = true;
                                lbltitle.Text = "Verify Basic Price";
                                Grdeditpopup.Visible = false;
                                gridcmc.Visible = false;
                                grdcentral.Visible = true;
                                InvoiceInfo();
                                popup.Visible = true;
                                btnmdlupd.Text = "Verified";
                                btnmdlupd.Visible = true;

                            }
                        }
                        else if (hf.Value == "7")/*This is for CMC */
                        {
                            if (Session["roles"].ToString() == "Chief Material Controller")
                            {
                                btnreject.Visible = true;
                                gridcmc.Visible = true;
                                Grdeditpopup.Visible = false;
                                grdcentral.Visible = false;
                                InvoiceInfo();
                                fillpopup();                                
                                popup.Visible = true;
                                lbltitle.Text = "Approve Basic Price";
                                btnmdlupd.Visible = true;
                                btnmdlupd.Text = "Approved";
                            }

                        }
                        else if (hf.Value == "7A")/*This is for Super Admin */
                        {
                            if (Session["roles"].ToString() == "SuperAdmin")
                            {
                               
                                btnreject.Visible = true;
                                gridcmc.Visible = true;
                                Grdeditpopup.Visible = false;
                                grdcentral.Visible = false;
                                InvoiceInfo();
                                fillpopup();                                
                                popup.Visible = true;
                                lbltitle.Text = "Approve Basic Price";
                                btnmdlupd.Visible = true;
                                btnmdlupd.Text = "Approved";
                            }

                        }
                        else if (hf.Value == "8")
                        {
                            //popreports.Hide();
                        }

                    }
                    else
                    {
                        JavaScript.UPAlert(Page, result);
                    }
                }
            }
            else
            {
                JavaScript.UPAlert(Page, "No Budget Available");
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex != -1)
        {
            ViewState["cccode"] = GridView1.DataKeys[GridView1.SelectedIndex].Values["cc_code"];
            ViewState["mrr"] = GridView1.DataKeys[GridView1.SelectedIndex].Values["MRR_no"]; 
            string pono = GridView1.SelectedValue.ToString();
            ViewState["po_no"] = pono;
            Viewpop();
            btnmdlupd.Visible = false;
            btnreject.Visible = false;
            lbltitle.Text = "";
            Grdeditpopup.Visible = true;
            gridcmc.Visible = false;
            grdcentral.Visible = false;
            ChangeControlStatusforprint();
            if (Session["roles"].ToString() != "SuperAdmin")
                da = new SqlDataAdapter("select * from pending_invoice where paymenttype='Supplier' and status in ('2A','4') and po_no='" + pono + "'", con);
            else
                da = new SqlDataAdapter("select * from pending_invoice where paymenttype='Supplier' and status='4' and po_no='" + pono + "'", con);
            da.Fill(ds, "checkpo");
            if (ds.Tables["checkpo"].Rows.Count > 0)
            {
                //hfprint.Value = "show";
                btnPrint.Visible = true;
                //tblprint.Visible = true;
            }
            else
            {
                //hfprint.Value = "hide";
                btnPrint.Visible = true;
                //tblprint.Visible = false;
            }
        }
    }
    public void Viewpop()
    {
        da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
        da.Fill(ds, "po_id");
        if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878)
            da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],ETaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*ETaxPercent/100),0)[ETaxAmount] ,STaxPercent,(((isnull(il.quantity,0)*isnull(il.basic_price,0))+((isnull(il.quantity,0)*ISNULL(il.basic_price,0))*ETaxPercent/100))*STaxPercent/100)as STaxAmount from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price,r.STaxpercent,r.ETaxPercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
        else
            da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
        da.Fill(ds, "fill");
        Grdeditpopup.DataSource = ds.Tables["fill"];
        Grdeditpopup.DataBind();
        popup.Visible = true;
        InvoiceInfo();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image img = (Image)e.Row.FindControl("Image1");
            HiddenField hf = (HiddenField)e.Row.FindControl("h1");
            if (hf.Value == "5")
                img.Visible = true;
            else if (Session["roles"].ToString() == "Central Store Keeper" && hf.Value == "6")
                img.Visible = true;
            else if (Session["roles"].ToString() == "Central Store Keeper" && hf.Value == "7A")
                img.Visible = false;
            else if (Session["roles"].ToString() == "Chief Material Controller" && hf.Value == "7")
                img.Visible = true;
            else if (Session["roles"].ToString() == "Chief Material Controller" && hf.Value == "7A")
                img.Visible = false;
            else if (Session["roles"].ToString() == "SuperAdmin" && hf.Value == "7A")
                img.Visible = true;
            else
                img.Visible = false;

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        filterbasicprice(s1);
    }
    public void filterbasicprice(string s1)
    {
        try
        {
            string Condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    Condition = Condition + " and datepart(mm,invoice_date)=" + ddlMonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy;
                }
                Condition = Condition + "and convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
            }
            if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "SuperAdmin")
            {
                if (ddlcccode.SelectedValue != "")
                    Condition = Condition + " and p.cc_code='" + ddlcccode.SelectedValue + "'";

            }
            else
            {
                Condition = Condition + "and p.cc_code='" + Session["cc_code"].ToString() + "'";
            }
            ViewState["condition"] = Condition;
            s1 = ViewState["condition"].ToString();

            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='5' or m.status='8') and p.paymenttype='Supplier'" + Condition + " order by m.status asc", con);
            else if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='6' or m.status='8' or m.status='7A') and p.paymenttype='Supplier'" + Condition + " order by  m.Status,m.PO_no asc", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='7' or m.status='8' or m.status='7A') and p.paymenttype='Supplier'" + Condition + " order by  m.Status,m.PO_no asc", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='7A' or m.status='8') and p.paymenttype='Supplier'" + Condition + " order by m.status asc", con);

            da.Fill(ds, "indents");
            this.UpdatePanel1.Update();
            GridView1.DataSource = ds.Tables["indents"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }


        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillpopup()
    {
        try
        {

            //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
            //da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],STaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*STaxPercent/100),0)[Tax Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select substring(item_code,1,8)item_code,SUM(quantity)quantity,basic_price,STaxpercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "' group by substring(r.Item_code,1,8),r.basic_price,r.STaxPercent  )il on i.item_code=substring(il.item_code,1,8)", con);
            //below syntax is old before excise tax added to query
            //da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],STaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*STaxPercent/100),0)[Tax Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select r.id, substring(item_code,1,8)item_code,SUM(quantity)quantity,basic_price,STaxpercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "' group by r.id,substring(r.Item_code,1,8),r.basic_price,r.STaxPercent  )il on i.item_code=substring(il.item_code,1,8)ORDER BY il.id asc", con);
            da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],ETaxPercent,(((isnull(il.quantity,0)*isnull(il.basic_price,0))*ETaxPercent/100))[ETaxAmount] ,STaxPercent,(((isnull(il.quantity,0)*isnull(il.basic_price,0))+((isnull(il.quantity,0)*ISNULL(il.basic_price,0))*ETaxPercent/100))*STaxPercent/100)as STaxAmount from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select r.id, substring(item_code,1,8)item_code,SUM(quantity)quantity,basic_price,STaxpercent ,r.ETaxPercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "' group by r.id,substring(r.Item_code,1,8),r.basic_price,r.STaxPercent,r.ETaxPercent  )il on i.item_code=substring(il.item_code,1,8)ORDER BY il.id asc", con);
            da.Fill(ds, "fillCMC");
            gridcmc.DataSource = ds.Tables["fillCMC"];
            gridcmc.DataBind();
            //da = new SqlDataAdapter("SELECT SUM(netamount) from pending_invoice where po_no='" + ViewState["po_no"].ToString() + "'", con);
            //da.Fill(ds, "netamount");
            //nettxt.Text = " " + ds.Tables["netamount"].Rows[0][0].ToString().Replace(".0000", ".00");

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillpopview()
    {
        if (Session["roles"].ToString() == "Project Manager")
        {

            da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
            da.Fill(ds, "po_id");
            if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878)
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],ETaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*ETaxPercent/100),0)[ETaxAmount] ,STaxPercent,(((isnull(il.quantity,0)*isnull(il.basic_price,0))+((isnull(il.quantity,0)*ISNULL(il.basic_price,0))*ETaxPercent/100))*STaxPercent/100)as STaxAmount from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price,r.STaxpercent,r.ETaxPercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
            else
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
            da.Fill(ds, "fill");
            Grdeditpopup.DataSource = ds.Tables["fill"];
            Grdeditpopup.DataBind();
        }
        else if (Session["roles"].ToString() == "Central Store Keeper")
        {
            da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
            da.Fill(ds, "po_id");
            if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878)
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],ETaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*ETaxPercent/100),0)[ETaxAmount] ,STaxPercent,(((isnull(il.quantity,0)*isnull(il.basic_price,0))+((isnull(il.quantity,0)*ISNULL(il.basic_price,0))*ETaxPercent/100))*STaxPercent/100)as STaxAmount from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price,r.STaxpercent,r.ETaxPercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
            else
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],STaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*STaxPercent/100),0)[Tax Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price,STaxpercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
            da.Fill(ds, "fill");
            grdcentral.DataSource = ds.Tables["fill"];
            grdcentral.DataBind();
            //da = new SqlDataAdapter("SELECT SUM(netamount) from pending_invoice where po_no='" + ViewState["po_no"].ToString() + "'", con);
            //da.Fill(ds, "netamount");
            //nettxt.Text = " " + ds.Tables["netamount"].Rows[0][0].ToString().Replace(".0000", ".00");
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("BasicPriceUpdationNew.aspx");
    }
    private decimal Amount = (decimal)0.0;
    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        try
        {
            string id = "";
            string Amt = "";
            string itemcode = "";
            string invoiveamt = "";
            string type = "";
            string otherdcas = "";
            string othersdcas = "";
            string otherdcasamt = "";
            string deductiondcas = "";
            string deductionsdcas = "";
            string deductionamt = "";
            string exciseamt = "";
            string vatamt = "";
            if (Session["roles"].ToString() == "Project Manager")
            {

                cmd.CommandText = "Update [MR_Report] set status='6' where po_no='" + ViewState["po_no"].ToString() + "' AND status != 'Rejected'  ";
                cmd.Connection = con;
                con.Open();
                bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                //bool i = false;
                if (i == true)
                    JavaScript.UPAlertRedirect(Page, "Sucessfull", "BasicPriceUpdationnew.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Failed", "BasicPriceUpdationnew.aspx");
                con.Close();


            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {

                foreach (GridViewRow record in grdcentral.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                    if (c1.Checked)
                    {
                        id = id + grdcentral.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        Amt = Amt + (record.FindControl("txtbasic") as TextBox).Text + ",";
                        //Amount = Amount + Convert.ToDecimal((record.FindControl("txtbasic") as TextBox).Text) * Convert.ToDecimal(record.Cells[8].Text);
                    }
                }

                cmd1 = new SqlCommand("checkbudget", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Total", txttotal.Text);
                cmd1.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                cmd1.Parameters.AddWithValue("@salestax", txttax.Text);
                cmd1.Parameters.AddWithValue("@PoNo", ViewState["po_no"].ToString());
                cmd1.Parameters.AddWithValue("@type", '1');
                cmd1.Parameters.AddWithValue("@vtype", '1');
                cmd1.Connection = con;
                con.Open();
                string result = cmd1.ExecuteScalar().ToString();
                con.Close();
                if (result == "sufficent")
                {
                    cmd = new SqlCommand("Invoiceverfication by CSK_New_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ids", id);
                    cmd.Parameters.AddWithValue("@NewAmts", Amt);
                    cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                    cmd.Parameters.AddWithValue("@Exciseduty", txtexduty.Text);
                    cmd.Parameters.AddWithValue("@salestax", txttax.Text);
                    cmd.Parameters.AddWithValue("@NetAmount", txtnetAmount.Text);
                    cmd.Parameters.AddWithValue("@Total", txttotal.Text);
                    cmd.Parameters.AddWithValue("@PoNo", ViewState["po_no"].ToString());
                    cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                    //New parameter created date 17-May-2016 Cr-ENH-MAR-010-2016 STARTS
                    cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                    //New parameter created date 17-May-2016 Cr-ENH-MAR-010-2016 ENDS
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "Sucessfull1";
                    con.Close();
                    if (msg == "Sucessfull")
                        JavaScript.UPAlertRedirect(Page, msg, "BasicPriceUpdationnew.aspx");
                    else
                        JavaScript.UPAlert(Page, msg);
                }
                else
                {
                    JavaScript.UPAlert(Page, result);
                }
            }

            else if (Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
            {

                foreach (GridViewRow record in gvanyother.Rows)
                {

                    if ((record.FindControl("chkSelectother") as CheckBox).Checked)
                    {
                        otherdcas = otherdcas + gvanyother.DataKeys[record.RowIndex]["dca_code"].ToString() + ",";
                        othersdcas = othersdcas + (record.FindControl("ddlsdca") as DropDownList).SelectedValue + ",";
                        otherdcasamt = otherdcasamt + (record.FindControl("txtotheramount") as TextBox).Text + ",";
                    }
                }

                foreach (GridViewRow record in gvdeduction.Rows)
                {

                    if ((record.FindControl("chkSelectdeduction") as CheckBox).Checked)
                    {
                        deductiondcas = deductiondcas + gvdeduction.DataKeys[record.RowIndex]["dca_code"].ToString() + ",";
                        deductionsdcas = deductionsdcas + (record.FindControl("ddlsdca") as DropDownList).SelectedValue + ",";
                        deductionamt = deductionamt + (record.FindControl("txtamountdeduction") as TextBox).Text + ",";
                    }
                }
                string result = "";

                cmd1 = new SqlCommand("checkbudget", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Clear();
                //cmd1.Parameters.AddWithValue("@Total", txttotal.Text);
                cmd1.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                cmd1.Parameters.AddWithValue("@PoNo", txtpo.Text);
                cmd1.Parameters.AddWithValue("@CCCode", ViewState["cccode"].ToString());
                cmd1.Parameters.AddWithValue("@DCA_Code", txtdca.Text);
                cmd1.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                cmd1.Parameters.AddWithValue("@type", "2");
                cmd1.Parameters.AddWithValue("@vtype", "1");
                cmd1.Parameters.AddWithValue("@Otherdcas", otherdcas);
                cmd1.Parameters.AddWithValue("@Otherdcaamts", otherdcasamt);
                if (rbtnsalestype.SelectedValue != "")
                {
                    cmd1.Parameters.AddWithValue("@Salestaxtype", rbtnsalestype.SelectedValue);
                }
                cmd1.Parameters.AddWithValue("@Salestaxdca", ddlvatdca.SelectedValue);
                cmd1.Parameters.AddWithValue("@Salestaxamt", txttax.Text);
                if (rbtnexcisetype.SelectedValue != "")
                {
                    cmd1.Parameters.AddWithValue("@Excisetaxtype", rbtnexcisetype.SelectedValue);
                }
                cmd1.Parameters.AddWithValue("@Excisetaxdca", ddlexcisedca.SelectedValue);
                cmd1.Parameters.AddWithValue("@Excisetaxamt", txtexduty.Text);
                cmd1.Connection = con;
                con.Open();
                result = cmd1.ExecuteScalar().ToString();
                con.Close();
                if (result == "sufficent")
                {
                    foreach (GridViewRow record in gridcmc.Rows)
                    {
                        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                        if (c1.Checked)
                        {
                            itemcode = itemcode + gridcmc.DataKeys[record.RowIndex]["item_code"].ToString() + ",";
                            Amt = Amt + (record.FindControl("txtbasic") as TextBox).Text + ",";
                            invoiveamt = invoiveamt + (record.FindControl("txtinvoiceprice") as TextBox).Text + ",";
                            exciseamt = exciseamt + (record.FindControl("lblexamt") as TextBox).Text + ",";
                            vatamt = vatamt + (record.Cells[14].Text) + ",";
                            type = (record.Cells[1].Text);

                        }
                    }
                    cmd = new SqlCommand("BasicPriceUpdationnew_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@itemcodes", itemcode);
                    cmd.Parameters.AddWithValue("@NewAmts", Amt);
                    cmd.Parameters.AddWithValue("@type", type.Substring(0, 1));
                    cmd.Parameters.AddWithValue("@invoiceAmts", invoiveamt);


                    cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                    cmd.Parameters.AddWithValue("@NetAmount", txtnetAmount.Text);

                    cmd.Parameters.AddWithValue("@PO_NO", ViewState["po_no"].ToString());
                    cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                    //New parameter created date 17-May-2016 Cr-ENH-MAR-010-2016 STARTS
                    cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                    //New parameter created date 17-May-2016 Cr-ENH-MAR-010-2016 ENDS

                    cmd.Parameters.AddWithValue("@Otherdcas", otherdcas);
                    cmd.Parameters.AddWithValue("@Othersdcas", othersdcas);
                    cmd.Parameters.AddWithValue("@Otherdcaamts", otherdcasamt);
                    cmd.Parameters.AddWithValue("@Deductiondcas", deductiondcas);
                    cmd.Parameters.AddWithValue("@Deductionsdcas", deductionsdcas);
                    cmd.Parameters.AddWithValue("@Deductiondcaamts", deductionamt);
                    if (rbtnsalestype.SelectedValue != "" && ddlvatdca.SelectedValue != "select" && ddlvatsdca.SelectedValue != "select" && ddlvattaxnos.SelectedValue != "Select Tax No")
                    {
                        cmd.Parameters.AddWithValue("@Salestaxtype", rbtnsalestype.SelectedValue);
                        cmd.Parameters.AddWithValue("@Salestaxdca", ddlvatdca.SelectedValue);
                        cmd.Parameters.AddWithValue("@Salestaxsdca", ddlvatsdca.SelectedValue);
                        cmd.Parameters.AddWithValue("@VATno", ddlvattaxnos.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@VATAmount", txttax.Text);
                    if (rbtnexcisetype.SelectedValue != "" && ddlexcisedca.SelectedValue != "select" && ddlexcisesdca.SelectedValue != "select" && ddlexcisetaxnos.SelectedValue != "Select Tax No")
                    {
                        cmd.Parameters.AddWithValue("@Excisetaxtype", rbtnexcisetype.SelectedValue);
                        cmd.Parameters.AddWithValue("@Excisetaxdca", ddlexcisedca.SelectedValue);
                        cmd.Parameters.AddWithValue("@Excisetaxsdca", ddlexcisesdca.SelectedValue);
                        cmd.Parameters.AddWithValue("@Exciseno", ddlexcisetaxnos.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@ExciseDuty", txtexduty.Text);
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "Sucessfull";
                    con.Close();
                    if (msg == "Sucessfull")
                    {
                        //InvoiceInfo();
                        //Viewpop();
                        //tblprint.Visible = true;
                        btnmdlupd.Visible = false;
                        btnreject.Visible = false;
                        //popup.Visible = false;
                        JavaScript.UPAlert(Page, msg);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>myFunction();</script>", false);
                        //popreports.Hide();
                        //filterbasicprice(s1);
                    }
                    else
                    {
                        //tblprint.Visible = false;
                        JavaScript.UPAlert(Page, msg);
                        //popup.Visible = true;
                        btnmdlupd.Visible = true;
                        btnreject.Visible = true;
                        btnPrint.Visible = false;
                        filterbasicprice(s1);

                    }
                    JavaScript.UPAlert(Page, "OVER");
                }
                else
                {
                    JavaScript.UPAlert(Page, result);
                }
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnhide_Click(object sender, EventArgs e)
    {
        popup.Visible = false;
        filterbasicprice(s1);
    }


    //private decimal Amount11 = (decimal)0.0;
    //private decimal Amount27 = (decimal)0.0;
    //private decimal Amount41 = (decimal)0.0;
    //private decimal Amount11A = (decimal)0.0;
    //private decimal Amount27A = (decimal)0.0;
    //private decimal Amount41A = (decimal)0.0;
    //private decimal Amount = (decimal)0.0;
    ////private decimal TAmount = (decimal)0.0;
    //private decimal STaxAmount = (decimal)0.0;

    //protected void Grdeditpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    decimal qty;
    //    decimal price;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (ViewState["checkview"] != null)
    //        {
    //            if (ViewState["checkview"].ToString() != "Checked")
    //            {
    //                Grdeditpopup.Columns[11].Visible = false;
    //                HiddenField hf1 = (HiddenField)e.Row.FindControl("h1");
    //                if (e.Row.Cells[9].Text != hf1.Value)
    //                {
    //                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
    //                }
    //            }
    //            else
    //            {
    //                Grdeditpopup.Columns[11].Visible = true;
    //                qty = Convert.ToDecimal(e.Row.Cells[8].Text);
    //                price = Convert.ToDecimal(e.Row.Cells[9].Text);
    //                e.Row.Cells[11].Text = Convert.ToDecimal(qty * price).ToString();
    //            }
    //        }
    //        //TAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "newbasicprice"));
    //    }
    //    //if (e.Row.RowType == DataControlRowType.Footer)
    //    //{
    //    //    e.Row.Cells[9].Text = String.Format("Rs.{0:#,##,##,###.00}", TAmount);

    //    //}

    //}
    //protected void Grdeditpopupp_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    decimal qty;
    //    decimal price;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (ViewState["checkview"] != null)
    //        {
    //            Grdeditpopupp.Columns[11].Visible = true;
    //            qty = Convert.ToDecimal(e.Row.Cells[8].Text);
    //            price = Convert.ToDecimal(e.Row.Cells[9].Text);
    //            e.Row.Cells[11].Text = Convert.ToDecimal(qty * price).ToString();
    //            Grdeditpopupp.Columns[10].Visible = false;
    //            Grdeditpopupp.Columns[0].Visible = false;
    //        }
    //    }
    //}
    //protected void gridcmc_RowDataBound1(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        TextBox txtinvoiceprice = (TextBox)e.Row.FindControl("txtinvoiceprice");
    //        TextBox txtbasic = (TextBox)e.Row.FindControl("txtbasic");
    //        //txtinvoiceprice.Enabled = false;
    //        //txtbasic.Enabled = false;
    //        if (txtinvoiceprice.Text != txtbasic.Text)
    //        //txtbasic.Style.Add("background-color", "Red");
    //        {
    //            txtinvoiceprice.ForeColor = System.Drawing.Color.White;
    //            txtbasic.ForeColor = System.Drawing.Color.Black;
    //            txtinvoiceprice.Style.Add("background-color", "Red");

    //        }

    //    }

    //}
    //protected void grdcentral_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        TextBox txtbasic = (TextBox)e.Row.FindControl("txtbasic");
    //        if (e.Row.Cells[9].Text != txtbasic.Text)
    //            txtbasic.ForeColor = System.Drawing.Color.Red;
    //        else
    //            txtbasic.ForeColor = System.Drawing.Color.Green;
    //    }


    //}
    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {

            cmd = new SqlCommand("reject_invoice_new", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PO_NO", ViewState["po_no"].ToString());
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Rejected")
            {
                //JavaScript.UPAlert(Page, msg);      
                JavaScript.UPAlertRedirect(Page, msg, "BasicPriceUpdationnew.aspx");
                filterbasicprice(s1);
            }
            else
            {
                JavaScript.UPAlert(Page, msg);               
                filterbasicprice(s1);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void rbtnsalestype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillvatdetails(rbtnsalestype.SelectedValue);
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);
    }
    public void fillvatdetails(string value)
    {
        if (rbtnsalestype.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("select mapdca_code,dca_code from dca where dca_type='Tax' and tax_type='" + value.ToString() + "' and mapdca_code='DCA-VAT-CR'", con);
        }
        else
        {
            da = new SqlDataAdapter("select mapdca_code,dca_code from dca where dca_type='Tax' and tax_type='" + value.ToString() + "' and mapdca_code='DCA-VAT-NON-CR'", con);
        }
        ds = new DataSet();
        da.Fill(ds, "vatdca");
        if (ds.Tables["vatdca"].Rows.Count > 0)
        {
            ddlvatdca.DataSource = ds.Tables["vatdca"];
            ddlvatdca.DataTextField = "mapdca_code";
            ddlvatdca.DataValueField = "dca_code";
            ddlvatdca.DataBind();
            ddlvatdca.Items.Insert(0, new ListItem("Select"));
            if (ViewState["vatdca"] != null)
            {
                //da = new SqlDataAdapter("select dca_code from dca where Dca_Type = 'Tax' and mapdca_code='"+ ViewState["vatdca"].ToString() + "'", con);
                //DataSet dss = new DataSet();
                //da.Fill(dss, "vatdcamain");
                //ddlvatdca.SelectedValue = dss.Tables["vatdcamain"].Rows[0].ItemArray[0].ToString();
                //ddlvatdca.SelectedItem.Text = ViewState["vatdca"].ToString();
                ddlvatdca.SelectedValue = ViewState["vatdca"].ToString();
                ViewState["vatdca"] = null;
            }
            fillsalessdca(ds.Tables["vatdca"].Rows[0].ItemArray[1].ToString());
        }
        else
        {
            ddlvatdca.Items.Clear();
            ddlvatsdca.Items.Clear();
            ddlvattaxnos.Items.Clear();
            ddlvatdca.Items.Insert(0, new ListItem("Select"));
            ddlvatsdca.Items.Insert(0, new ListItem("Select"));
            ddlvattaxnos.Items.Insert(0, new ListItem("Select"));
        }
    }
    public void fillsalessdca(string dca)
    {
        da = new SqlDataAdapter("select mapsubdca_code,subdca_code from subdca where dca_code='" + dca + "'", con);
        ds = new DataSet();
        da.Fill(ds, "vatsdca");
        if (ds.Tables["vatsdca"].Rows.Count > 0)
        {
            ddlvatsdca.DataSource = ds.Tables["vatsdca"];
            ddlvatsdca.DataTextField = "mapsubdca_code";
            ddlvatsdca.DataValueField = "subdca_code";
            ddlvatsdca.DataBind();
            ddlvatsdca.Items.Insert(0, new ListItem("Select"));
            if (ViewState["vatsdca"] != null)
            {
                //da = new SqlDataAdapter("select subdca_code from subdca where mapsubdca_code='"+ ViewState["vatsdca"] + "'", con);
                //ds = new DataSet();
                //da.Fill(ds, "vatsdcamain");
                //ddlvatsdca.SelectedValue = ds.Tables["vatsdcamain"].Rows[0].ItemArray[0].ToString();
                // ddlvatsdca.SelectedItem.Text = ViewState["vatsdca"].ToString();
                ddlvatsdca.SelectedValue = ViewState["vatsdca"].ToString();
                ViewState["vatsdca"] = null;
            }
            fillsalestaxnos(dca);
        }
        else
        {
            ddlvatsdca.Items.Clear();
            ddlvattaxnos.Items.Clear();
            ddlvatsdca.Items.Insert(0, new ListItem("Select"));
            ddlvattaxnos.Items.Insert(0, new ListItem("Select"));
        }
    }
    public void fillsalestaxnos(string dca1)
    {
        da = new SqlDataAdapter("select Tax_nos from taxdca where dca_code='" + dca1 + "'", con);
        ds = new DataSet();
        da.Fill(ds, "taxvatdca");
        if (ds.Tables["taxvatdca"].Rows.Count > 0)
        {
            ddlvattaxnos.DataSource = ds.Tables["taxvatdca"];
            ddlvattaxnos.DataTextField = "Tax_nos";
            ddlvattaxnos.DataValueField = "Tax_nos";
            ddlvattaxnos.DataBind();
            if (ViewState["vattaxno"] != null)
            {
                ddlvattaxnos.SelectedValue = ViewState["vattaxno"].ToString();               
                ViewState["vattaxno"] = null;
            }
            ddlvattaxnos.Items.Insert(0, new ListItem("Select TaxNos"));
        }
        else
        {
            ddlvattaxnos.Items.Clear();
            ddlvattaxnos.Items.Insert(0, new ListItem("Select TaxNos"));
        }
    }
    protected void rbtnexcisetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillexcisedetails(rbtnexcisetype.SelectedValue);
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);
    }
    public void fillexcisedetails(string value)
    {
        if (rbtnexcisetype.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("select mapdca_code,dca_code from dca where dca_type='Tax' and tax_type='" + value.ToString() + "' and mapdca_code='DCA-EXCISE-CR'", con);
        }
        else
        {
            da = new SqlDataAdapter("select mapdca_code,dca_code from dca where dca_type='Tax' and tax_type='" + value.ToString() + "' and mapdca_code='DCA-EXCISE-NON-CR'", con);
        }
        ds = new DataSet();
        da.Fill(ds, "excdca");
        if (ds.Tables["excdca"].Rows.Count > 0)
        {
            ddlexcisedca.DataSource = ds.Tables["excdca"];
            ddlexcisedca.DataTextField = "mapdca_code";
            ddlexcisedca.DataValueField = "dca_code";
            ddlexcisedca.DataBind();
            ddlexcisedca.Items.Insert(0, new ListItem("Select"));
            if (ViewState["excisedca"] != null)
            {
                //da = new SqlDataAdapter("select dca_code from dca where Dca_Type = 'Tax' and mapdca_code='" + ViewState["excisedca"].ToString() + "'", con);
                //DataSet dsexc = new DataSet();
                //da.Fill(dsexc, "excdcamain");
                //ddlexcisedca.SelectedValue = dsexc.Tables["excdcamain"].Rows[0].ItemArray[0].ToString();
                //ddlexcisedca.SelectedItem.Text= ViewState["excisedca"].ToString();
                ddlexcisedca.SelectedValue = ViewState["excisedca"].ToString();
                ViewState["excisedca"] = null;
            }
            fillexcisesdca(ds.Tables["excdca"].Rows[0].ItemArray[1].ToString());

        }
        else
        {
            ddlexcisedca.Items.Clear();
            ddlexcisesdca.Items.Clear();
            ddlexcisetaxnos.Items.Clear();
            ddlexcisedca.Items.Insert(0, new ListItem("Select"));
            ddlexcisesdca.Items.Insert(0, new ListItem("Select"));
            ddlexcisetaxnos.Items.Insert(0, new ListItem("Select"));
        }
    }
    public void fillexcisesdca(string dca)
    {
        da = new SqlDataAdapter("select mapsubdca_code,subdca_code from subdca where dca_code='" + dca + "'", con);
        ds = new DataSet();
        da.Fill(ds, "excsdca");
        if (ds.Tables["excsdca"].Rows.Count > 0)
        {
            ddlexcisesdca.DataSource = ds.Tables["excsdca"];
            ddlexcisesdca.DataTextField = "mapsubdca_code";
            ddlexcisesdca.DataValueField = "subdca_code";
            ddlexcisesdca.DataBind();
            ddlexcisesdca.Items.Insert(0, new ListItem("Select"));
            if (ViewState["excisesdca"] != null)
            {
                //da = new SqlDataAdapter("select subdca_code from subdca where mapsubdca_code='" + ViewState["excisesdca"] + "'", con);
                //ds = new DataSet();
                //da.Fill(ds, "excsdcamain");
                //ddlexcisesdca.SelectedValue = ds.Tables["excsdcamain"].Rows[0].ItemArray[0].ToString();
                //ddlexcisesdca.SelectedItem.Text = ViewState["excisesdca"].ToString();
                ddlexcisesdca.SelectedValue = ViewState["excisesdca"].ToString();
                ViewState["excisesdca"] = null;
            }
            fillexcisetaxnos(dca);
        }
        else
        {
            ddlexcisesdca.Items.Clear();
            ddlexcisetaxnos.Items.Clear();
            ddlexcisesdca.Items.Insert(0, new ListItem("Select"));
            ddlexcisetaxnos.Items.Insert(0, new ListItem("Select"));
        }
    }
    public void fillexcisetaxnos(string dca1)
    {
        da = new SqlDataAdapter("select Tax_nos from taxdca where dca_code='" + dca1 + "'", con);
        ds = new DataSet();
        da.Fill(ds, "taxexcdca");
        if (ds.Tables["taxexcdca"].Rows.Count > 0)
        {
            ddlexcisetaxnos.DataSource = ds.Tables["taxexcdca"];
            ddlexcisetaxnos.DataTextField = "Tax_nos";
            ddlexcisetaxnos.DataValueField = "Tax_nos";
            ddlexcisetaxnos.DataBind();
            ddlexcisetaxnos.Items.Insert(0, new ListItem("Select Tax No"));
               if (ViewState["excisetaxno"] != null)
            {
                ddlexcisetaxnos.SelectedValue = ViewState["excisetaxno"].ToString();
                ViewState["excisetaxno"] = null;
            }
        }
        else
        {
            ddlexcisetaxnos.Items.Clear();
            ddlexcisetaxnos.Items.Insert(0, new ListItem("Select Tax No"));
        }
    }
    protected void rbtnothercharges_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnothercharges.SelectedIndex == 0)
        {
            tdddlanyotherdcas.Visible = true;
            tranyothergrid.Visible = true;
            fillanyotherdcas();
            gvanyother.DataSource = ViewState["dtothercharges1"] as DataTable;
            gvanyother.DataBind();
        }
        else
        {
            tdddlanyotherdcas.Visible = false;
            tranyothergrid.Visible = false;
            TextBox1.Text = "";
            gvanyother.DataSource = null;
            gvanyother.DataBind();
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);
    }
    
    public void fillother_dcas()
    {
        string checkedotherdcanames = "";
        da = new SqlDataAdapter("select distinct vt.dca_code from vendor_taxes vt join pending_invoice p on vt.invoiceno=p.InvoiceNo where p.po_no='" + ViewState["po_no"].ToString() + "' and vt.type='Other'", con);
        da.Fill(ds, "checkedotherdcas");
        if (ds.Tables["checkedotherdcas"].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables["checkedotherdcas"].Rows.Count; i++)
            {
                checkedotherdcanames = checkedotherdcanames + ds.Tables["checkedotherdcas"].Rows[i].ItemArray[0].ToString() + ",";
                ddlanyotherdcas.Items.FindByValue(ds.Tables["checkedotherdcas"].Rows[i].ItemArray[0].ToString()).Selected = true;
            }
            TextBox1.Text = checkedotherdcanames;
            fillgridsdcacmc();
        }
    }
    public void fillgridsdcacmc()
    {
        tranyothergrid.Visible = true;
        DataTable Objdt = ViewState["dtother"] as DataTable;
        string[] result = TextBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string dca in result)
        {
            var objresult = Objdt.AsEnumerable().Where(r => r.Field<string>("dca_code") == dca).Select(x => new { x });
            if (objresult.ToList().Count == 0)
            {
                da = new SqlDataAdapter("select mapdca_code,dca_code from dca where mapdca_code in ('" + dca + "')", con);
                ds = new DataSet();
                da.Fill(ds, "newdca");
                if (ds.Tables["newdca"].Rows.Count > 0)
                {
                    DataRow toInsert = Objdt.NewRow();
                    toInsert["dca_code"] = ds.Tables["newdca"].Rows[0]["dca_code"];
                    toInsert["subdca_code"] = "";
                    toInsert["amount"] = 0.0;
                    Objdt.Rows.Add(toInsert);
                }
            }
        }
        if (rbtnothercharges.SelectedIndex == 0 && TextBox1.Text != "")
        {
            gvanyother.DataSource = Objdt;
            gvanyother.DataBind();
        }
    }
    protected void rbtndeduction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtndeduction.SelectedIndex == 0)
        {
            tddeduction.Visible = true;
            trdeductiongrid.Visible = true;
            //fillanydeductiondcas();
            fillanyotherdcas();
            gvdeduction.DataSource = ViewState["dtdedcharges"] as DataTable;
            gvdeduction.DataBind();
        }
        else
        {
            tddeduction.Visible = false;
            trdeductiongrid.Visible = false;
            TextBox2.Text = "";
            gvdeduction.DataSource = null;
            gvdeduction.DataBind();

        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);
    }
    public void fillanyotherdcas()
    {
        //da = new SqlDataAdapter("select mapdca_code,yd.dca_code from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where dca_type='Expense'  and cc_code='" + ViewState["cccode"].ToString() + "'", con);
        //ds = new DataSet();
        //da.Fill(ds, "dcas");
        if (rbtnothercharges.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("TaxDcas_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["mrr"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["cccode"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "SupplierOthers";
            ds = new DataSet();
            da.Fill(ds, "dcas");
            if (ds.Tables["dcas"].Rows.Count > 0)
            {
                ddlanyotherdcas.DataSource = ds.Tables["dcas"];
                ddlanyotherdcas.DataTextField = "mapdca_code";
                ddlanyotherdcas.DataValueField = "dca_code";
                ddlanyotherdcas.DataBind();
                fillother_dcas();
            }
        }
        if (rbtndeduction.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("TaxDcas_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["mrr"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["cccode"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "SupplierDeductions";
            ds = new DataSet();
            da.Fill(ds, "ddcas");
            if (ds.Tables["ddcas"].Rows.Count > 0)
            {
                ddldeduction.DataSource = ds.Tables["ddcas"];
                ddldeduction.DataTextField = "mapdca_code";
                ddldeduction.DataValueField = "dca_code";
                ddldeduction.DataBind();
                filldeduction_dcas();
            }
        }
    }
    public void fillanydeductiondcas()
    {
        //da = new SqlDataAdapter("select mapdca_code,yd.dca_code from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where dca_type='Expense'  and cc_code='" + ViewState["cccode"].ToString() + "'", con);
        //ds = new DataSet();
        //da.Fill(ds, "deddcas");
        if (rbtnothercharges.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("TaxDcas_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["mrr"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["cccode"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "SupplierOthers";
            ds = new DataSet();
            da.Fill(ds, "deddcas1");
            if (ds.Tables["deddcas1"].Rows.Count > 0)
            {
                ddlanyotherdcas.DataSource = ds.Tables["deddcas1"];
                ddlanyotherdcas.DataTextField = "mapdca_code";
                ddlanyotherdcas.DataValueField = "dca_code";
                ddlanyotherdcas.DataBind();
                fillother_dcas();
            }
        }
        if (rbtndeduction.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("TaxDcas_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["mrr"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["cccode"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "SupplierDeductions";
            ds = new DataSet();
            da.Fill(ds, "deddcas");
            if (ds.Tables["deddcas"].Rows.Count > 0)
            {
                ddldeduction.DataSource = ds.Tables["deddcas"];
                ddldeduction.DataTextField = "mapdca_code";
                ddldeduction.DataValueField = "dca_code";
                ddldeduction.DataBind();
                filldeduction_dcas();
            }
        }
    }
    public void filldeduction_dcas()
    {
        string checkeddeddcanames = "";
        da = new SqlDataAdapter("select distinct vt.dca_code from vendor_taxes vt join pending_invoice p on vt.invoiceno=p.InvoiceNo where p.po_no='" + ViewState["po_no"].ToString() + "' and vt.type='Deduction'", con);
        da.Fill(ds, "checkeddeddcas");
        if (ds.Tables["checkeddeddcas"].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables["checkeddeddcas"].Rows.Count; i++)
            {
                checkeddeddcanames = checkeddeddcanames + ds.Tables["checkeddeddcas"].Rows[i].ItemArray[0].ToString() + ",";
                ddldeduction.Items.FindByValue(ds.Tables["checkeddeddcas"].Rows[i].ItemArray[0].ToString()).Selected = true;
            }
            TextBox2.Text = checkeddeddcanames;
            fillgriddedsdcacmc();
        }
    }

    public void fillgriddedsdcacmc()
    {
        trdeductiongrid.Visible = true;
        DataTable Objdt = ViewState["dtdeduction"] as DataTable;
        string[] result = TextBox2.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string dca in result)
        {
            var objresult = Objdt.AsEnumerable().Where(r => r.Field<string>("dca_code") == dca).Select(x => new { x });
            if (objresult.ToList().Count == 0)
            {
                da = new SqlDataAdapter("select mapdca_code,dca_code from dca where mapdca_code in ('" + dca + "')", con);
                ds = new DataSet();
                da.Fill(ds, "newdca");
                if (ds.Tables["newdca"].Rows.Count > 0)
                {
                    DataRow toInsert = Objdt.NewRow();
                    toInsert["dca_code"] = ds.Tables["newdca"].Rows[0]["dca_code"];
                    toInsert["subdca_code"] = "";
                    toInsert["amount"] = 0.0;
                    Objdt.Rows.Add(toInsert);
                }
            }
        }
        if (rbtndeduction.SelectedIndex == 0 && TextBox2.Text != "")
        {
            gvdeduction.DataSource = Objdt;
            gvdeduction.DataBind();
        }
    }
    protected void ddlanyotherdcas_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["dtother"] != null)
        {
            string name = "";
            for (int i = 0; i < ddlanyotherdcas.Items.Count; i++)
            {
                if (ddlanyotherdcas.Items[i].Selected)
                {
                    name += ddlanyotherdcas.Items[i].Text + ",";
                }
            }

            TextBox1.Text = name;
            DataTable Objdt1 = new DataTable();
            if (ViewState["dtothercharges"] == null)
            {
                Objdt1 = (ViewState["dtother"] as DataTable).Copy();
                ViewState["dtothercharges"] = Objdt1;
            }

            Objdt1 = ViewState["dtothercharges"] as DataTable;
            DataTable Objdt = ViewState["dtother"] as DataTable;
            foreach (ListItem li in ddlanyotherdcas.Items)
            {
                if (li.Selected)
                {
                    var resultobj = Objdt.AsEnumerable().Where(x => x.Field<string>("dca_code") == li.Text).Select(x => new { x });
                    if (resultobj.ToList().Count == 0)
                    {
                        var objresult = Objdt1.AsEnumerable().Where(x => x.Field<string>("dca_code") == li.Text).Select(x => new { x });
                        if (objresult.ToList().Count > 0)
                        {
                            DataRow toInsert = Objdt.NewRow();
                            toInsert["mapdca_code"] = objresult.ToList()[0].x.ItemArray[0].ToString();
                            toInsert["dca_code"] = objresult.ToList()[0].x.ItemArray[3].ToString();
                            toInsert["subdca_code"] = objresult.ToList()[0].x.ItemArray[4].ToString();
                            toInsert["mapsubdca_code"] = objresult.ToList()[0].x.ItemArray[1].ToString();
                            toInsert["amount"] = objresult.ToList()[0].x.ItemArray[2].ToString();
                            Objdt.Rows.Add(toInsert);
                            ViewState["dtother"] = Objdt;
                        }
                        else
                        {
                            da = new SqlDataAdapter("select (mapdca_code+','+dca_name)as mapdca_code,dca_code from dca where mapdca_code in ('" + li.Text + "')", con);
                            ds = new DataSet();
                            da.Fill(ds, "newdca");
                            if (ds.Tables["newdca"].Rows.Count > 0)
                            {
                                DataRow toInsert = Objdt.NewRow();
                                toInsert["mapdca_code"] = ds.Tables["newdca"].Rows[0]["mapdca_code"];
                                toInsert["dca_code"] = ds.Tables["newdca"].Rows[0]["dca_code"];
                                toInsert["subdca_code"] = "";
                                toInsert["amount"] = 0.0;
                                Objdt.Rows.Add(toInsert);
                                ViewState["dtother"] = Objdt;
                            }
                        }
                    }
                }
                else
                {
                    if (name != "")
                    {
                        foreach (DataRow dr in Objdt.Rows)
                        {
                            if (dr["dca_code"].ToString() == li.Text)
                            {
                                dr.Delete();

                                ViewState["dtother"] = Objdt;

                            }
                        }
                        Objdt.AcceptChanges();
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, "Please Select No other Button");
                        rbtnothercharges.SelectedIndex = 1;
                        gvanyother.DataSource = null;
                        gvanyother.DataBind();  
                    }
                }

            }
            if (rbtnothercharges.SelectedIndex == 0 && TextBox1.Text != "")
            {
                gvanyother.DataSource = Objdt;
                gvanyother.DataBind();
            }
            //ViewState["fillgrdother"] = "Yes";
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);
        }
        else
        {
            //ViewState["fillgrdother"] = "No";
            string s1 = string.Empty;
            string s2 = string.Empty;
            string name = "";
            for (int i = 0; i < ddlanyotherdcas.Items.Count; i++)
            {
                if (ddlanyotherdcas.Items[i].Selected)
                {
                    name += ddlanyotherdcas.Items[i].Text + ",";
                }
            }
            TextBox3.Text = name;
            s1 = TextBox3.Text.Replace(",", "','");
            if (s1 != "")
            {
                s2 = s1.Substring(0, s1.Length - 3);
                TextBox1.Text = s2;
            }
            else
            {
                
                TextBox1.Text = "";
                s2 = "";
            }
            fillgridsdca(s2);          
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);

        }
    }
    protected void ddldeduction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["dtdeduction"] != null)
        {
            string name = "";
            for (int i = 0; i < ddldeduction.Items.Count; i++)
            {
                if (ddldeduction.Items[i].Selected)
                {
                    name += ddldeduction.Items[i].Text + ",";
                }
            }
            TextBox2.Text = name;
            DataTable Objdt1 = new DataTable();
            if (ViewState["dtdeductioncharges"] == null)
            {
                Objdt1 = (ViewState["dtdeduction"] as DataTable).Copy();
                ViewState["dtdeductioncharges"] = Objdt1;
            }

            Objdt1 = ViewState["dtdeductioncharges"] as DataTable;
            DataTable Objdt = ViewState["dtdeduction"] as DataTable;
            foreach (ListItem li in ddldeduction.Items)
            {
                if (li.Selected)
                {
                    var resultobj = Objdt.AsEnumerable().Where(x => x.Field<string>("dca_code") == li.Text).Select(x => new { x });
                    if (resultobj.ToList().Count == 0)
                    {
                        var objresult = Objdt1.AsEnumerable().Where(x => x.Field<string>("dca_code") == li.Text).Select(x => new { x });
                        if (objresult.ToList().Count > 0)
                        {
                            DataRow toInsert = Objdt.NewRow();
                            toInsert["mapdca_code"] = objresult.ToList()[0].x.ItemArray[0].ToString();
                            toInsert["dca_code"] = objresult.ToList()[0].x.ItemArray[3].ToString();
                            toInsert["subdca_code"] = objresult.ToList()[0].x.ItemArray[4].ToString();
                            toInsert["mapsubdca_code"] = objresult.ToList()[0].x.ItemArray[1].ToString();
                            toInsert["amount"] = objresult.ToList()[0].x.ItemArray[2].ToString();
                            Objdt.Rows.Add(toInsert);
                            ViewState["dtdeduction"] = Objdt;
                        }
                        else
                        {
                            da = new SqlDataAdapter("select distinct (mapdca_code+','+dca_name)as mapdca_code,dca_code from dca where mapdca_code in ('" + li.Text + "')", con);
                            ds = new DataSet();
                            da.Fill(ds, "newdca");
                            if (ds.Tables["newdca"].Rows.Count > 0)
                            {
                                DataRow toInsert = Objdt.NewRow();
                                toInsert["mapdca_code"] = ds.Tables["newdca"].Rows[0]["mapdca_code"];
                                toInsert["dca_code"] = ds.Tables["newdca"].Rows[0]["dca_code"];
                                toInsert["subdca_code"] = "";
                                toInsert["amount"] = 0.0;
                                Objdt.Rows.Add(toInsert);
                                ViewState["dtdeduction"] = Objdt;
                            }
                        }
                    }
                }
                else
                {
                    if (name != "")
                    {
                        foreach (DataRow dr in Objdt.Rows)
                        {
                            if (dr["dca_code"].ToString() == li.Text)
                            {
                                dr.Delete();
                                ViewState["dtdeduction"] = Objdt;
                            }
                        }
                        Objdt.AcceptChanges();
                    }
                    else
                    {
                        JavaScript.UPAlert(Page,"Please Select No Deduction Button");
                        rbtndeduction.SelectedIndex = 1;
                        gvdeduction.DataSource = null;
                        gvdeduction.DataBind();
                    }
                }

            }
            if (rbtndeduction.SelectedIndex == 0 && TextBox2.Text != "")
            {
                gvdeduction.DataSource = Objdt;
                gvdeduction.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);
            
        }
        else
        {
            
            string s1 = string.Empty;
            string s2 = string.Empty;
            string name1 = "";
            for (int i = 0; i < ddldeduction.Items.Count; i++)
            {
                if (ddldeduction.Items[i].Selected)
                {
                    name1 += ddldeduction.Items[i].Text + ",";
                }
            }
            TextBox4.Text = name1;
            s1 = TextBox4.Text.Replace(",", "','");
            if (s1 != "")
            {
                s2 = s1.Substring(0, s1.Length - 3);
                TextBox2.Text = s2;
            }
            else
            {
                TextBox2.Text = "";
                s2 = "";
            }
           
            fillgridded(s2);
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);           

        }
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkother = (CheckBox)e.Row.FindControl("chkSelectother");
            HiddenField hf1 = (HiddenField)e.Row.FindControl("hsubdcaother");
            TextBox otheramount = (TextBox)e.Row.FindControl("txtotheramount");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            string DataKey = rowView["dca_code"].ToString();
            DropDownList sdca = (e.Row.FindControl("ddlsdca") as DropDownList);
            //da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + DataKey + "'", con);
            da = new SqlDataAdapter("select distinct subdca_code,(subdca_code+','+subdca_name)as mapsubdca_code from subdca where dca_code='" + DataKey + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            sdca.DataSource = ds.Tables[0];
            sdca.DataTextField = "mapsubdca_code";
            sdca.DataValueField = "subdca_code";
            sdca.DataBind();
            sdca.Items.Insert(0, new ListItem("Select SDCA"));
            if (Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Central Store Keeper")
            {
                sdca.SelectedValue = hf1.Value;
                chkother.Checked = true;
                chkother.Enabled = false;
                sdca.Enabled = false;
                otheramount.Enabled = false;
            }
            else
            {
                sdca.SelectedValue = hf1.Value;
            }

        }
       
    }       
    protected void OnRowDataBoundgvdeduction(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkdeduction = (CheckBox)e.Row.FindControl("chkSelectdeduction");
            HiddenField hfded = (HiddenField)e.Row.FindControl("hsubdcadeduction");
            TextBox dedamount = (TextBox)e.Row.FindControl("txtamountdeduction");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            string DataKey = rowView["dca_code"].ToString();
            DropDownList sdca = (e.Row.FindControl("ddlsdca") as DropDownList);
            //da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + DataKey + "'", con);
            da = new SqlDataAdapter("select distinct subdca_code,(subdca_code+','+subdca_name)as mapsubdca_code from subdca where dca_code='" + DataKey + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            sdca.DataSource = ds.Tables[0];
            sdca.DataTextField = "mapsubdca_code";
            sdca.DataValueField = "subdca_code";
            sdca.DataBind();
            sdca.Items.Insert(0, new ListItem("Select SDCA"));
            if (Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Central Store Keeper")
            {
                sdca.SelectedValue = hfded.Value;
                chkdeduction.Checked = true;
                chkdeduction.Enabled = false;
                sdca.Enabled = false;
                dedamount.Enabled = false;
            }
            else
            {
                sdca.SelectedValue = hfded.Value;

            }
        }
       
    }  
    public void fillgridsdca(string dcas)
    {
        tranyothergrid.Visible = true;
        //da = new SqlDataAdapter("select mapdca_code,dca_code from dca where mapdca_code in ('" + dcas + "')", con);
        da = new SqlDataAdapter("select distinct (mapdca_code+','+dca_name)as mapdca_code,dca_code,0 as amount,''as subdca_code from dca where mapdca_code in ('" + dcas + "')", con);
        ds = new DataSet();
        da.Fill(ds,"chk");
        if (ds.Tables["chk"].Rows.Count > 0)
        {
            gvanyother.DataSource = ds.Tables[0];
            gvanyother.DataBind();           
        }
        else
        {            
            gvanyother.DataSource = null;
            gvanyother.DataBind();
        }

    }   
    public void fillgridded(string dcas)
    {
        trdeductiongrid.Visible = true;
        da = new SqlDataAdapter("select distinct (mapdca_code+','+dca_name)as mapdca_code,dca_code,0 as amount,'' as subdca_code from dca where mapdca_code in ('" + dcas + "')", con);
        ds = new DataSet();
        da.Fill(ds, "dedu");
        if (ds.Tables["dedu"].Rows.Count > 0)
        {
            gvdeduction.DataSource = ds.Tables[0];
            gvdeduction.DataBind();
        }
        else
        {
            gvdeduction.DataSource = null;
            gvdeduction.DataBind();
        }

    }
    private void ChangeControlStatus()
    {
        ddlvendortype.Enabled = false;
        txtmrr.Enabled = false;
        txtvendorid.Enabled = false;
        txtvendorname.Enabled = false;
        txtpo.Enabled = false;
        txtin.Enabled = false;
        txtindt.Enabled = false;
        rbtnsalestype.Enabled = true;
        rbtnexcisetype.Enabled = true;
        rbtnothercharges.Enabled = true;
        rbtndeduction.Enabled = true;

        if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Project Manager")
        {
            txtdca.Enabled = false;
            rbtnsalestype.Enabled = false;
            ddlvatdca.Enabled = false;
            ddlvatsdca.Enabled = false;
            ddlvattaxnos.Enabled = false;
            rbtnexcisetype.Enabled = false;
            ddlexcisedca.Enabled = false;
            ddlexcisesdca.Enabled = false;
            ddlexcisetaxnos.Enabled = false; 
            txtindtmk.Enabled = false;
            txtbasic.Enabled = false;
            txtexduty.Enabled = false;
            txttax.Enabled = false;
            txttotal.Enabled = false;
            rbtnothercharges.Enabled = false;
            TextBox1.Enabled = false;    
            rbtndeduction.Enabled = false;
            TextBox2.Enabled = false;
            txtnetAmount.Enabled = false;
        }

    }
    private void ChangeControlStatusforprint()
    {
        ddlvendortype.Enabled = false;
        txtmrr.Enabled = false;
        txtvendorid.Enabled = false;
        txtvendorname.Enabled = false;
        txtpo.Enabled = false;
        txtin.Enabled = false;
        txtindt.Enabled = false;
        rbtnsalestype.Enabled = false;
        rbtnexcisetype.Enabled = false;
        rbtnothercharges.Enabled = false;
        rbtndeduction.Enabled = false;
        txtdca.Enabled = false;
        rbtnsalestype.Enabled = false;
        ddlvatdca.Enabled = false;
        ddlvatsdca.Enabled = false;
        ddlvattaxnos.Enabled = false;
        rbtnexcisetype.Enabled = false;
        ddlexcisedca.Enabled = false;
        ddlexcisesdca.Enabled = false;
        ddlexcisetaxnos.Enabled = false;
        txtindtmk.Enabled = false;
        txtbasic.Enabled = false;
        txtexduty.Enabled = false;
        txttax.Enabled = false;
        txttotal.Enabled = false;
        rbtnothercharges.Enabled = false;
        TextBox1.Enabled = false;
        rbtndeduction.Enabled = false;
        TextBox2.Enabled = false;
        txtnetAmount.Enabled = false;
        gvanyother.Enabled = false;
        gvdeduction.Enabled = false;


    }
    public void clear()
    {

        gvanyother.DataSource = null;
        gvanyother.DataBind();
        gvdeduction.DataSource = null;
        gvdeduction.DataBind();
        //gridcmc.DataSource = null;
        //gridcmc.DataBind();
        rbtndeduction.ClearSelection();
        rbtnothercharges.ClearSelection();
        tdddlanyotherdcas.Visible = false; 
        tddeduction.Visible = false;
        txtnetAmount.Text = "";
        rbtndeduction.Enabled = false;
        rbtnexcisetype.Enabled = false;
        rbtnothercharges.Enabled = false;
        rbtnsalestype.Enabled = false;
    }
    decimal TAmount;
    protected void Grdeditpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal qty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Grdeditpopup.Columns[11].Visible = false;
            HiddenField hf1 = (HiddenField)e.Row.FindControl("h1");
            if (e.Row.Cells[9].Text != hf1.Value)
            {
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
            }
            TAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "newbasicprice"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = String.Format("Rs.{0:#,##,##,###.00}", TAmount);
        }

    }

    protected void gridcmc_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtinvoiceprice = (TextBox)e.Row.FindControl("txtinvoiceprice");
            TextBox txtbasic = (TextBox)e.Row.FindControl("txtbasic");
            //txtinvoiceprice.Enabled = false;
            //txtbasic.Enabled = false;
            if (txtinvoiceprice.Text != txtbasic.Text)
            //txtbasic.Style.Add("background-color", "Red");
            {
                txtinvoiceprice.ForeColor = System.Drawing.Color.White;
                txtbasic.ForeColor = System.Drawing.Color.Black;
                txtinvoiceprice.Style.Add("background-color", "Red");
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtexcisetax = (TextBox)e.Row.FindControl("txtexcisetax");            
            TextBox txttax = (TextBox)e.Row.FindControl("txtsalestaxpercent");
            TextBox lblexcamt = (TextBox)e.Row.FindControl("lblexamt");
            //Label lblvatamt = (Label)e.Row.FindControl("lblamt");
            if (ViewState["excchk"] != null)
            {
                if (ViewState["excchk"].ToString() == "False")
                {
                    txtexcisetax.Text = Convert.ToInt16(0).ToString();
                    lblexcamt.Text = Convert.ToInt16(0).ToString();
                    txtexcisetax.Enabled = false;
                    lblexcamt.Enabled = false;

                }
            }
            if (ViewState["vatchk"] != null)
            {
                if (ViewState["vatchk"].ToString() == "False")
                {
                    txttax.Text = Convert.ToInt16(0).ToString();
                    //lblvatamt.Text = Convert.ToInt16(0).ToString();
                    e.Row.Cells[14].Text= Convert.ToInt16(0).ToString();
                    txttax.Enabled = false;
                }
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblexcfotter = (Label)e.Row.FindControl("lblfooterexcisefooter");
            Label lblvatfotter = (Label)e.Row.FindControl("Label3");
            Label lblexcamtfotter = (Label)e.Row.FindControl("lblexamountfooter");
            //Label lblvatamtfotter = (Label)e.Row.FindControl("Label4");
            if (ViewState["excchk"] != null)
            {
                if (ViewState["excchk"].ToString() == "False")
                {
                    lblexcfotter.Text = Convert.ToInt16(0).ToString();
                    lblexcamtfotter.Text = Convert.ToInt16(0).ToString();

                }
            }
            if (ViewState["vatchk"] != null)
            {
                if (ViewState["vatchk"].ToString() == "False")
                {
                    lblvatfotter.Text = Convert.ToInt16(0).ToString();
                    e.Row.Cells[14].Text = Convert.ToInt16(0).ToString();
                    //lblvatamtfotter.Text = Convert.ToInt16(0).ToString();
                }
            }
        }
    }
    public void InvoiceInfo()
    {
        //da = new SqlDataAdapter("select po_no,invoiceno,invoice_date,basicvalue,exciseduty,frieght,insurance,total,salestax,advance,hold,anyother,description,netamount,vendor_name,edcess,hedcess,REPLACE(CONVERT(VARCHAR(11),convert(datetime,p.Inv_Making_Date, 9),106), ' ', '-')as Inv_Making_Date from pending_invoice p join vendor v on p.vendor_id=v.vendor_id where po_no='" + ViewState["po_no"].ToString() + "' and paymenttype='Supplier'; SELECT amount FROM vendor_taxes vt JOIN pending_invoice pi ON vt.InvoiceNo=pi.InvoiceNo WHERE pi.po_no= '" + ViewState["po_no"].ToString() + "' AND pi.paymenttype= 'Excise Duty'; SELECT amount FROM vendor_taxes vt JOIN pending_invoice pi ON vt.InvoiceNo=pi.InvoiceNo WHERE pi.po_no= '" + ViewState["po_no"].ToString() + "' AND pi.paymenttype= 'VAT'", con);
        da = new SqlDataAdapter("select po_no,invoiceno,invoice_date,basicvalue,exciseduty,frieght,insurance,total,salestax,advance,hold,anyother,description,netamount,vendor_name,edcess,hedcess,REPLACE(CONVERT(VARCHAR(11),convert(datetime,p.Inv_Making_Date, 9),106), ' ', '-')as Inv_Making_Date,p.paymenttype,v.vendor_id,p.DCA_Code FROM pending_invoice p join vendor v on p.vendor_id=v.vendor_id where po_no='" + ViewState["po_no"].ToString() + "' and paymenttype='Supplier';SELECT vt.dca_code, vt.Subdca_Code, vt.RegdNo, vt.Amount,vt.Tax_Type FROM vendor_taxes vt JOIN pending_invoice pi ON vt.InvoiceNo = pi.InvoiceNo WHERE pi.po_no = '" + ViewState["po_no"].ToString() + "' AND pi.paymenttype = 'VAT';SELECT vt.dca_code,vt.Subdca_Code,vt.RegdNo,vt.Amount,vt.Tax_Type FROM vendor_taxes vt JOIN pending_invoice pi ON vt.InvoiceNo = pi.InvoiceNo WHERE pi.po_no = '" + ViewState["po_no"].ToString() + "' AND pi.paymenttype = 'Excise Duty'", con);
        //da = new SqlDataAdapter("select po_no, invoiceno, invoice_date, basicvalue, exciseduty, frieght, insurance, total, salestax, advance, hold, anyother, description, netamount, vendor_name, edcess, hedcess, REPLACE(CONVERT(VARCHAR(11), convert(datetime, p.Inv_Making_Date, 9), 106), ' ', '-')as Inv_Making_Date,p.paymenttype,v.vendor_id,p.DCA_Code FROM pending_invoice p join vendor v on p.vendor_id = v.vendor_id where po_no = '" + ViewState["po_no"].ToString() + "' and paymenttype = 'Supplier'; SELECT distinct vt.dca_code, vt.Subdca_Code, vt.RegdNo, vt.Amount,vt.Tax_Type FROM vendor_taxes vt JOIN pending_invoice pi ON vt.Po_No = pi.PO_NO WHERE vt.po_no = '" + ViewState["po_no"].ToString() + "' AND vt.Type = 'VAT Tax'; SELECT distinct vt.dca_code,vt.Subdca_Code,vt.RegdNo,vt.Amount,vt.Tax_Type FROM vendor_taxes vt JOIN pending_invoice pi ON vt.Po_No = pi.PO_NO WHERE pi.po_no = '" + ViewState["po_no"].ToString() + "' AND vt.Type = 'Excise Tax'", con);
        da.Fill(ds, "ininfo");
        ddlvendortype.SelectedValue = ds.Tables["ininfo"].Rows[0][18].ToString();
        txtmrr.Text = ViewState["mrr"].ToString();
        txtvendorid.Text = ds.Tables["ininfo"].Rows[0][19].ToString();
        txtvendorname.Text = ds.Tables["ininfo"].Rows[0][14].ToString();
        txtdca.Text = ds.Tables["ininfo"].Rows[0][20].ToString();
        txtnetAmount.Text = ds.Tables["ininfo"].Rows[0][13].ToString().Replace(".0000", ".00");
        if (ds.Tables["ininfo1"].Rows.Count > 0)
        {
            rbtnsalestype.SelectedValue = ds.Tables["ininfo1"].Rows[0][4].ToString();
            ViewState["vatdca"] = ds.Tables["ininfo1"].Rows[0][0].ToString();
            ViewState["vatsdca"] = ds.Tables["ininfo1"].Rows[0][1].ToString();
            ViewState["vattaxno"] = ds.Tables["ininfo1"].Rows[0][2].ToString();
            txttax.Text = ds.Tables["ininfo1"].Rows[0][3].ToString().Replace(".0000", ".00");
            ViewState["vatchk"] = "True";
        }
        else
        {
            txttax.Text = Convert.ToInt16(0).ToString();
            ViewState["vatchk"] = "False";
        }
        if (ds.Tables["ininfo2"].Rows.Count>0)
        {
            rbtnexcisetype.SelectedValue = ds.Tables["ininfo2"].Rows[0][4].ToString();
            ViewState["excisedca"] = ds.Tables["ininfo2"].Rows[0][0].ToString();
            ViewState["excisesdca"] = ds.Tables["ininfo2"].Rows[0][1].ToString();
            ViewState["excisetaxno"] = ds.Tables["ininfo2"].Rows[0][2].ToString();
            txtexduty.Text = ds.Tables["ininfo2"].Rows[0][3].ToString().Replace(".0000", ".00");
            ViewState["excchk"] = "True";
        }
        else
        {
            txtexduty.Text = Convert.ToInt16(0).ToString();
            ViewState["excchk"] = "False";
        }
        //vatdca(rbtnsalestype.SelectedValue);
        //ddlvatdca.SelectedItem.Text = ViewState["vatdca"].ToString();
        //vatsdca(ddlvatdca.SelectedValue);
        //ddlvatsdca.SelectedItem.Text = ds.Tables["ininfo1"].Rows[0][1].ToString();
        //vatnos(ddlvatdca.SelectedValue);
        //ddlvattaxnos.SelectedItem.Text = ds.Tables["ininfo1"].Rows[0][2].ToString();

        //ddlexcisedca.SelectedItem.Text = ds.Tables["ininfo2"].Rows[0][0].ToString();
        //ddlexcisesdca.SelectedItem.Text = ds.Tables["ininfo2"].Rows[0][1].ToString();
        //ddlexcisetaxnos.SelectedItem.Text = ds.Tables["ininfo2"].Rows[0][2].ToString();
        txtpo.Text = ds.Tables["ininfo"].Rows[0][0].ToString();
        txtin.Text = ds.Tables["ininfo"].Rows[0][1].ToString();
        txtindt.Text = ds.Tables["ininfo"].Rows[0][2].ToString();

        if (txtindtmk.Text == "")
            txtindtmk.Text = txtindt.Text;
        else
            txtindtmk.Text = ds.Tables["ininfo"].Rows[0][17].ToString();
        txtbasic.Text = ds.Tables["ininfo"].Rows[0][3].ToString().Replace(".0000", ".00"); 
        txttotal.Text = Convert.ToString(Convert.ToDecimal(txtbasic.Text) + Convert.ToDecimal(txtexduty.Text) + Convert.ToDecimal(txttax.Text)); ;
        fillanyother(txtin.Text);
        filldeduction(txtin.Text);
        fillanyotherdcas();
        if (ViewState["vatchk"] != null)
        {
            if (ViewState["vatchk"].ToString() == "True")
            {
                fillvatdetails(rbtnsalestype.SelectedValue);
            }
        }
        if (ViewState["excchk"] != null)
        {
            if (ViewState["excchk"].ToString() == "True")
            {
                fillexcisedetails(rbtnexcisetype.SelectedValue);
            }
        }
        checkescisevat();
    }
   
    public void fillanyother(string invoiceno)
    {
        da = new SqlDataAdapter("SELECT DISTINCT 1 as IsExists FROM Vendor_Taxes where type='Other' and InvoiceNo='" + invoiceno + "'", con);
        da.Fill(ds, "checks");
        if (ds.Tables["checks"].Rows.Count == 1)
        {
            
            //da = new SqlDataAdapter("SELECT dca_code,subdca_code,amount FROM Vendor_Taxes where type='Other' and InvoiceNo='" + invoiceno + "'", con);
            da = new SqlDataAdapter("SELECT distinct (d.mapdca_code+','+d.dca_name)as mapdca_code,(vt.subdca_code+','+sd.subdca_name)as mapsubdca_code,amount,vt.dca_code,vt.subdca_code FROM dca d join Vendor_Taxes vt on d.dca_code=vt.Dca_Code join subdca sd on vt.Subdca_Code=sd.mapsubdca_code where type='Other' and InvoiceNo='" + invoiceno + "'", con);
            da.Fill(ds, "fillother");
            ViewState["dtother"] = ds.Tables["fillother"];
            ViewState["dtothercharges1"] = ds.Tables["fillother"];
            gvanyother.DataSource = ViewState["dtother"] as DataTable;
            gvanyother.DataBind();
            rbtnothercharges.SelectedValue = "Yes";          

        }
        else
        {
            gvanyother.DataSource = null;
            gvanyother.DataBind();
            rbtnothercharges.SelectedValue = "No";
        }
    }
    public void filldeduction(string invoiceno)
    {
        da = new SqlDataAdapter("SELECT DISTINCT 1 as IsExists FROM Vendor_Taxes where type='Deduction' and InvoiceNo='" + invoiceno + "'", con);
        da.Fill(ds, "checkded");
        if (ds.Tables["checkded"].Rows.Count == 1)
        {
            //da = new SqlDataAdapter("SELECT dca_code,subdca_code,amount FROM Vendor_Taxes where type='Deduction' and InvoiceNo='" + invoiceno + "'", con);
            da = new SqlDataAdapter("SELECT distinct (d.mapdca_code+','+d.dca_name)as mapdca_code,(vt.subdca_code+','+sd.subdca_name)as mapsubdca_code,amount,vt.dca_code,vt.subdca_code FROM dca d join Vendor_Taxes vt on d.dca_code=vt.Dca_Code join subdca sd on vt.Subdca_Code=sd.mapsubdca_code where type='Deduction' and InvoiceNo='" + invoiceno + "'", con);
            da.Fill(ds, "fillded");
            ViewState["dtdeduction"] = ds.Tables["fillded"];
            ViewState["dtdedcharges"] = ds.Tables["fillded"];
            gvdeduction.DataSource = ViewState["dtdeduction"] as DataTable;
            gvdeduction.DataBind();
            rbtndeduction.SelectedValue = "Yes";
        }
        else
        {
            gvdeduction.DataSource = null;
            gvdeduction.DataBind();
            rbtndeduction.SelectedValue = "No";
        }
    }
    protected void grdcentral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            TextBox txtbasic = (TextBox)e.Row.FindControl("txtbasic");
            if (e.Row.Cells[9].Text != txtbasic.Text)
                txtbasic.ForeColor = System.Drawing.Color.Red;
            else
                txtbasic.ForeColor = System.Drawing.Color.Green;
        }
    }

    public void checkescisevat()
    {
        if (ViewState["excchk"] != null)
        {
            if (ViewState["excchk"].ToString() == "True")
            {
                rbtnexcisetype.Enabled = true;
                ddlexcisedca.Enabled = true;
                ddlexcisesdca.Enabled = true;
                ddlexcisetaxnos.Enabled = true;
            }
            else
            {
                rbtnexcisetype.Enabled = false;
                ddlexcisedca.Enabled = false;
                ddlexcisesdca.Enabled = false;
                ddlexcisetaxnos.Enabled = false;
            }
        }
        if (ViewState["vatchk"] !=null)
        {
            if (ViewState["vatchk"].ToString() == "True")
            {
                rbtnsalestype.Enabled = true;
                ddlvatdca.Enabled = true;
                ddlvatsdca.Enabled = true;
                ddlvattaxnos.Enabled = true;
            }
            else
            {
                rbtnsalestype.Enabled = false;
                ddlvatdca.Enabled = false;
                ddlvatsdca.Enabled = false;
                ddlvattaxnos.Enabled = false;
            }
        }

    }

    public void clearviewstate()
    {
        ViewState["dtother"] = null;
        ViewState["dtothercharges1"] = null;
        ViewState["dtdeduction"] = null;
        ViewState["dtdedcharges"] = null;
        TextBox2.Text = "";
        TextBox1.Text = "";
    }

}