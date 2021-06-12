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



public partial class BasicPriceUpdation : System.Web.UI.Page
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
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Warehouse");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        //esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 55);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
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
		}
        tblprint.Visible = false;
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
        string pono = GridView1.DataKeys[e.NewEditIndex]["po_no"].ToString();
        ViewState["po_no"] = pono;
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
                    cmd = new SqlCommand("checkbudget", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PoNo", ViewState["po_no"].ToString());
                    cmd.Parameters.AddWithValue("@Vtype", '3');

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
        fillexcise(ViewState["code"].ToString());
        ddlExcno.Enabled = false;
        ddlvatno.Enabled = false;
        btnPrint.Visible = false;
        if (hf.Value == "5")/*This is for siteincharge  */
        {
            fillpopview();
            btnreject.Visible = false;
            lbltitle.Text = "Verify Basic Price";
            Grdeditpopup.Visible = true;
            gridcmc.Visible = false;
            grdcentral.Visible = false;
            InvoiceiInfo.Visible = false;
            btnmdlupd.Text = "Verified";
            trbtn.Visible = true;
            popreports.Show();
        }
        else if (hf.Value == "6")/*This is for central storekeeper */
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
            {
                clear();
                fillpopview();
                btnreject.Visible = true;
                lbltitle.Text = "Verify Basic Price";
                Grdeditpopup.Visible = false;
                gridcmc.Visible = false;
                grdcentral.Visible = true;
                InvoiceiInfo.Visible = true;
                InvoiceInfo();
                btnmdlupd.Text = "Verified";
                btnmdlupd.Visible = true;
                popreports.Show();
                //trexcisecreditpurchase.Visible = false;
                //trvatcreditpurchase.Visible = false;
            }
        }
        else if (hf.Value == "7")/*This is for CMC */
        {
            if (Session["roles"].ToString() == "Chief Material Controller")
            {
                clear();
                btnreject.Visible = true;                              
                gridcmc.Visible = true;
                Grdeditpopup.Visible = false;
                grdcentral.Visible = false;
                InvoiceiInfo.Visible = true;
                fillpopup();
                InvoiceInfo();
                lbltitle.Text = "Approve Basic Price";
                btnmdlupd.Visible = true;
                btnmdlupd.Text = "Approved";
                popreports.Show();
            }

        }
        else if (hf.Value == "7A")/*This is for Super Admin */
        {
            if (Session["roles"].ToString() == "SuperAdmin")
            {
                clear();
                btnreject.Visible = true;                
                gridcmc.Visible = true;
                Grdeditpopup.Visible = false;
                grdcentral.Visible = false;
                InvoiceiInfo.Visible = true;
                fillpopup();
                InvoiceInfo();
                lbltitle.Text = "Approve Basic Price";
                btnmdlupd.Visible = true;
                btnmdlupd.Text = "Approved";
                popreports.Show();
            }

        }
        else if (hf.Value == "8")
        {
            popreports.Hide();
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

            string pono = GridView1.SelectedValue.ToString();
            ViewState["po_no"] = pono;
            Viewpop();
            btnmdlupd.Visible = false;
            btnreject.Visible = false;
            InvoiceiInfo.Visible = true;
            InvoiceInfo();
            lbltitle.Text = "";
            Grdeditpopup.Visible = true;
            gridcmc.Visible = false;
            grdcentral.Visible = false;
            popreports.Show();
            if (Session["roles"].ToString() != "SuperAdmin")
                da = new SqlDataAdapter("select * from pending_invoice where paymenttype='Supplier' and status in ('2A','4') and po_no='" + pono + "'", con);
            else
                da = new SqlDataAdapter("select * from pending_invoice where paymenttype='Supplier' and status='4' and po_no='" + pono + "'", con);
            da.Fill(ds, "checkpo");
            if (ds.Tables["checkpo"].Rows.Count > 0)
            {
                hfprint.Value = "show";
                btnPrint.Visible = true;
                tblprint.Visible = true;
            }
            else
            {
                hfprint.Value = "hide";
                btnPrint.Visible = false;
                tblprint.Visible = false;
            }


        }



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
            else if (Session["roles"].ToString() == "Central Store Keeper"  && hf.Value == "7A")
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
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillpopup()
    {
        try
        {

            //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
            //da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],STaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*STaxPercent/100),0)[Tax Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select substring(item_code,1,8)item_code,SUM(quantity)quantity,basic_price,STaxpercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "' group by substring(r.Item_code,1,8),r.basic_price,r.STaxPercent  )il on i.item_code=substring(il.item_code,1,8)", con);
            da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],STaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*STaxPercent/100),0)[Tax Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select r.id, substring(item_code,1,8)item_code,SUM(quantity)quantity,basic_price,STaxpercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "' group by r.id,substring(r.Item_code,1,8),r.basic_price,r.STaxPercent  )il on i.item_code=substring(il.item_code,1,8)ORDER BY il.id asc", con);
            da.Fill(ds, "fillCMC");
            gridcmc.DataSource = ds.Tables["fillCMC"];
            gridcmc.DataBind();
            da = new SqlDataAdapter("SELECT SUM(netamount) from pending_invoice where po_no='" + ViewState["po_no"].ToString() + "'", con);
            da.Fill(ds, "netamount");
            nettxt.Text = " " + ds.Tables["netamount"].Rows[0][0].ToString().Replace(".0000", ".00");


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillpopview()
    {
        if (Session["roles"].ToString() == "Project Manager")
        {
            ViewState["checkview"] = "UnChecked";
            btnPrint.Visible = false;
            //tblprint.Visible = false;
            da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
            da.Fill(ds, "po_id");
            if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878) 
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
            else
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
            da.Fill(ds, "fill");
            Grdeditpopup.DataSource = ds.Tables["fill"];
            Grdeditpopup.DataBind();
            da = new SqlDataAdapter("SELECT SUM(netamount) from pending_invoice where po_no='" + ViewState["po_no"].ToString() + "'", con);
            da.Fill(ds, "netamount");
            nettxt.Text = " " + ds.Tables["netamount"].Rows[0][0].ToString().Replace(".0000", ".00");
        }
        else if (Session["roles"].ToString() == "Central Store Keeper")
        {
            da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
            da.Fill(ds, "po_id");
            if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878) 
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],STaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*STaxPercent/100),0)[Tax Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price,STaxpercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
            else
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice],(isnull(il.quantity,0)*isnull(il.basic_price,0))[Amount],STaxPercent,Round(((isnull(il.quantity,0)*isnull(il.basic_price,0))*STaxPercent/100),0)[Tax Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price,STaxpercent from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=il.item_code", con);
            da.Fill(ds, "fill");
            grdcentral.DataSource = ds.Tables["fill"];
            grdcentral.DataBind();
            da = new SqlDataAdapter("SELECT SUM(netamount) from pending_invoice where po_no='" + ViewState["po_no"].ToString() + "'", con);
            da.Fill(ds, "netamount");
            nettxt.Text = " " + ds.Tables["netamount"].Rows[0][0].ToString().Replace(".0000", ".00");
        }
    }
    public void Viewpop()
    {
        ViewState["checkview"] = "Checked";        
        da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
        da.Fill(ds, "po_id");
        if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878) 
             da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
        else
            da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],Replace(il.basic_price,'.0000','.00') as[newbasicprice] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + ViewState["po_no"].ToString() + "' and item_code=r.item_code)as [Raised Qty],quantity,basic_price from [recieved Items]r where po_no='" + ViewState["po_no"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
        da.Fill(ds, "fill");
        Grdeditpopup.DataSource = ds.Tables["fill"];
        Grdeditpopup.DataBind();
        Grdeditpopupp.DataSource = ds.Tables["fill"];
        Grdeditpopupp.DataBind();
        da = new SqlDataAdapter("SELECT SUM(netamount) from pending_invoice where po_no='" + ViewState["po_no"].ToString() + "'", con);
        da.Fill(ds, "netamount");
        nettxtp.Text = " " + ds.Tables["netamount"].Rows[0][0].ToString().Replace(".0000", ".00");
        nettxt.Text = " " + ds.Tables["netamount"].Rows[0][0].ToString().Replace(".0000", ".00");
       
    }
    public static string vatamount = "";
    public static string vatno = "";
    public static string ExciseNo = "";
    public static string Exciseduty = "";
    public static string Edcess = "";
    public static string Hedcess = "";
    public void clear()
    {
        excisecheck.Checked = false;
        VATcheck.Checked = false;
        ddlExcno.SelectedValue = "Select";
        txtexduty.Text = "";
        txtEdc.Text = "";
        txtHEdc.Text = "";
        txtvatamt.Text = "";
        ddlvatno.SelectedValue = "Select";
    }
    public void InvoiceInfo()
    {

        da = new SqlDataAdapter("select po_no,invoiceno,invoice_date,basicvalue,exciseduty,frieght,insurance,total,salestax,advance,hold,anyother,description,netamount,vendor_name,edcess,hedcess,REPLACE(CONVERT(VARCHAR(11),convert(datetime,p.Inv_Making_Date, 9),106), ' ', '-')as Inv_Making_Date from pending_invoice p join vendor v on p.vendor_id=v.vendor_id where po_no='" + ViewState["po_no"].ToString() + "' and paymenttype='Supplier';select exciseduty,edcess,hedcess from pending_invoice where po_no='" + ViewState["po_no"].ToString() + "' and paymenttype='Excise Duty';select VAT  from pending_invoice where po_no='" + ViewState["po_no"].ToString() + "' and paymenttype='VAT' ", con);
        da.Fill(ds, "ininfo");
        txtpo.Text = ds.Tables["ininfo"].Rows[0][0].ToString();
        txtin.Text = ds.Tables["ininfo"].Rows[0][1].ToString();
        txtindt.Text = ds.Tables["ininfo"].Rows[0][2].ToString();
        txtindt.Enabled = false;
        txtbasic.Text = ds.Tables["ininfo"].Rows[0][3].ToString().Replace(".0000", ".00");
        txttax.Text = ds.Tables["ininfo"].Rows[0][8].ToString().Replace(".0000", ".00");
        txtindtmk.Text = ds.Tables["ininfo"].Rows[0][17].ToString();
        //if (txtindtmk.Text == "")
        //    txtindtmk.Text = txtindt.Text;
        //Print
        txtpop.Text = ds.Tables["ininfo"].Rows[0][0].ToString();
        txtinp.Text = ds.Tables["ininfo"].Rows[0][1].ToString();
        txtindtp.Text = ds.Tables["ininfo"].Rows[0][2].ToString();
        txtbasicp.Text = ds.Tables["ininfo"].Rows[0][3].ToString().Replace(".0000", ".00");
        txttaxp.Text = ds.Tables["ininfo"].Rows[0][8].ToString().Replace(".0000", ".00");
        txtindtmkp.Text = ds.Tables["ininfo"].Rows[0][17].ToString();
        //if (txtindtmkp.Text == "")
        //    txtindtmkp.Text = txtindtp.Text;
        if (ds.Tables["ininfo1"].Rows.Count > 0)
        {
            txtexduty.Text = ds.Tables["ininfo1"].Rows[0][0].ToString().Replace(".0000", ".00");
            txtEdc.Text = ds.Tables["ininfo1"].Rows[0][1].ToString();
            txtHEdc.Text = ds.Tables["ininfo1"].Rows[0][2].ToString();
            //Print
            txtexdutyp.Text = ds.Tables["ininfo1"].Rows[0][0].ToString().Replace(".0000", ".00");
            txtEdcp.Text = ds.Tables["ininfo1"].Rows[0][1].ToString();
            txtHEdcp.Text = ds.Tables["ininfo1"].Rows[0][2].ToString();
            txtexduty.Enabled = true;
            txtEdc.Enabled = true;
            txtHEdc.Enabled = true;
            ddlExcno.Enabled = true;
        }
        else
        {
            txtexduty.Enabled = false;
            txtEdc.Enabled = false;
            txtHEdc.Enabled = false;
            ddlvatno.Enabled = false;
        }
        txtfre.Text = ds.Tables["ininfo"].Rows[0][5].ToString().Replace(".0000", ".00");
        txtinsurance.Text = ds.Tables["ininfo"].Rows[0][6].ToString().Replace(".0000", ".00");
        txttotal.Text = ds.Tables["ininfo"].Rows[0][7].ToString().Replace(".0000", ".00");
        //Print
        txtfrep.Text = ds.Tables["ininfo"].Rows[0][5].ToString().Replace(".0000", ".00");
        txtinsurancep.Text = ds.Tables["ininfo"].Rows[0][6].ToString().Replace(".0000", ".00");
        txttotalp.Text = ds.Tables["ininfo"].Rows[0][7].ToString().Replace(".0000", ".00");
        if (ds.Tables["ininfo2"].Rows.Count > 0)
        {
            txtvatamt.Text = ds.Tables["ininfo2"].Rows[0][0].ToString().Replace(".0000", ".00");
      
            txtvatamt.Enabled = true;
            ddlvatno.Enabled = true;
            //print
            txtvatamtp.Text = ds.Tables["ininfo2"].Rows[0][0].ToString().Replace(".0000", ".00");
          
        }
        else
        {
            txtvatamt.Enabled = false;
            ddlvatno.Enabled = false;
          
        }
        txtAdvance.Text = ds.Tables["ininfo"].Rows[0][9].ToString().Replace(".0000", ".00");
        txthold.Text = ds.Tables["ininfo"].Rows[0][10].ToString().Replace(".0000", ".00");
        txtother.Text = ds.Tables["ininfo"].Rows[0][11].ToString().Replace(".0000", ".00");
        txtindesc.Text = ds.Tables["ininfo"].Rows[0][12].ToString();
        txtnetAmount.Text = ds.Tables["ininfo"].Rows[0][13].ToString().Replace(".0000", ".00");
        txtname.Text = ds.Tables["ininfo"].Rows[0][14].ToString();
        //print
        txtAdvancep.Text = ds.Tables["ininfo"].Rows[0][9].ToString().Replace(".0000", ".00");
        txtholdp.Text = ds.Tables["ininfo"].Rows[0][10].ToString().Replace(".0000", ".00");
        txtotherp.Text = ds.Tables["ininfo"].Rows[0][11].ToString().Replace(".0000", ".00");
        txtindescp.Text = ds.Tables["ininfo"].Rows[0][12].ToString();
        txtnetAmountp.Text = ds.Tables["ininfo"].Rows[0][13].ToString().Replace(".0000", ".00");
        txtnamep.Text = ds.Tables["ininfo"].Rows[0][14].ToString();


        da = new SqlDataAdapter("Select ExciseReg_No from  Excise_Account where PO_NO='" + ViewState["po_no"].ToString() + "';Select VatReg_No from VAT_Account where PO_NO='" + ViewState["po_no"].ToString() + "';Select SrtxReg_No from  ServiceTax_Account where PO_NO='" + ViewState["po_no"].ToString() + "'", con);
        da.Fill(ds, "VATReg");


        if (ds.Tables["VATReg"].Rows.Count > 0)
        {
            excisecheck.Checked = true;
            ddlExcno.SelectedValue = ds.Tables["VATReg"].Rows[0][0].ToString();
            //print
            ddlExcnop.Text = ds.Tables["VATReg"].Rows[0][0].ToString();
            ExciseNo = ds.Tables["VATReg"].Rows[0][0].ToString();
            Exciseduty = ds.Tables["ininfo"].Rows[0][4].ToString().Replace(".0000", ".00");
            Edcess = ds.Tables["ininfo"].Rows[0][15].ToString().Replace(".0000", ".00");
            Hedcess = ds.Tables["ininfo"].Rows[0][16].ToString().Replace(".0000", ".00");
        } 
        if (ds.Tables["VATReg1"].Rows.Count > 0)
        {
            ddlvatno.SelectedValue = ds.Tables["VATReg1"].Rows[0][0].ToString();
            //print
            ddlvatnop.Text = ds.Tables["VATReg1"].Rows[0][0].ToString();
            vatno = ds.Tables["VATReg1"].Rows[0][0].ToString();
            vatamount = ds.Tables["ininfo"].Rows[0][8].ToString().Replace(".0000", ".00");
            VATcheck.Checked = true;
        }
        if (ds.Tables["VATReg2"].Rows.Count > 0)
        {
            excisecheck.Checked = true;
            ddlExcno.SelectedValue= ds.Tables["VATReg2"].Rows[0][0].ToString();
            //print
            ddlExcnop.Text = ds.Tables["VATReg2"].Rows[0][0].ToString();
            ExciseNo = ds.Tables["VATReg2"].Rows[0][0].ToString();            
            Exciseduty = ds.Tables["ininfo"].Rows[0][4].ToString().Replace(".0000", ".00");
            Edcess = ds.Tables["ininfo"].Rows[0][15].ToString().Replace(".0000", ".00");
            Hedcess = ds.Tables["ininfo"].Rows[0][16].ToString().Replace(".0000", ".00");
        }
      
      
  

    }
   
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("BasicPriceUpdation.aspx");
    }
    protected void btnmdlupd_Click(object sender, EventArgs e)
     {
        try
        {
            string id = "";
            string Amt = "";
            string itemcode = "";
            string invoiveamt = "";
            string type = "";
            if (Session["roles"].ToString() == "Project Manager")
            {

                cmd.CommandText = "Update [MR_Report] set status='6' where po_no='" + ViewState["po_no"].ToString() + "' AND status != 'Rejected'  ";

                cmd.Connection = con;
                con.Open();
                bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (i == true)
                    JavaScript.UPAlertRedirect(Page, "Sucessfull", "BasicPriceUpdation.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Failed", "BasicPriceUpdation.aspx");
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
                        Amount = Amount + Convert.ToDecimal((record.FindControl("txtbasic") as TextBox).Text) * Convert.ToDecimal(record.Cells[8].Text);
                        
                    }
                }

                cmd1 = new SqlCommand("checkbudget", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@Total", txttotal.Text);
                cmd1.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                cmd1.Parameters.AddWithValue("@Frieght", txtfre.Text);
                cmd1.Parameters.AddWithValue("@Insurance", txtinsurance.Text);
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
                cmd = new SqlCommand("Invoiceverfication by CSK_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", id);
                cmd.Parameters.AddWithValue("@NewAmts", Amt);
                cmd.Parameters.AddWithValue("@InvoiceAmount", Amount);
                cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                cmd.Parameters.AddWithValue("@Frieght", txtfre.Text);
                cmd.Parameters.AddWithValue("@Insurance", txtinsurance.Text);
                if (ddlExcno.SelectedValue != "Select")

                    cmd.Parameters.AddWithValue("@Exciseno", ddlExcno.SelectedValue);
                if (ddlvatno.SelectedValue != "Select")
                    cmd.Parameters.AddWithValue("@VATno", ddlvatno.SelectedValue);
                cmd.Parameters.AddWithValue("@Exciseduty", txtexduty.Text);
                cmd.Parameters.AddWithValue("@EDcess", txtEdc.Text);
                cmd.Parameters.AddWithValue("@HEDcess", txtHEdc.Text);
                cmd.Parameters.AddWithValue("@salestax", txttax.Text);
                cmd.Parameters.AddWithValue("@Vatamt", txtvatamt.Text);
                cmd.Parameters.AddWithValue("@Advance", txtAdvance.Text);
                cmd.Parameters.AddWithValue("@Hold", txthold.Text);
                cmd.Parameters.AddWithValue("@Other", txtother.Text);
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
                con.Close();
                if (msg == "Sucessfull")
                    JavaScript.UPAlertRedirect(Page, msg, "BasicPriceUpdation.aspx");
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
                foreach (GridViewRow record in gridcmc.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                    if (c1.Checked)
                    {
                        itemcode = itemcode + gridcmc.DataKeys[record.RowIndex]["item_code"].ToString() + ",";
                        Amt = Amt + (record.FindControl("txtbasic") as TextBox).Text + ",";
                        invoiveamt = invoiveamt + (record.FindControl("txtinvoiceprice") as TextBox).Text + ",";
                        type = (record.Cells[1].Text);
                       
                    }
                }
                cmd = new SqlCommand("BasicPriceUpdation_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", id);
                cmd.Parameters.AddWithValue("@itemcodes", itemcode);
                cmd.Parameters.AddWithValue("@NewAmts", Amt);
                cmd.Parameters.AddWithValue("@type", type.Substring(0, 1));
                cmd.Parameters.AddWithValue("@invoiceAmts", invoiveamt);
                cmd.Parameters.AddWithValue("@Advance", txtAdvance.Text);
                cmd.Parameters.AddWithValue("@Hold", txthold.Text);
                cmd.Parameters.AddWithValue("@Other", txtother.Text);
                cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                if (ddlExcno.SelectedValue != "Select")
                    cmd.Parameters.AddWithValue("@Exciseno", ddlExcno.SelectedValue);
                if (ddlvatno.SelectedValue != "Select")
                    cmd.Parameters.AddWithValue("@VATno", ddlvatno.SelectedValue);
                cmd.Parameters.AddWithValue("@Exciseduty", txtexduty.Text);
                cmd.Parameters.AddWithValue("@EDcess", txtEdc.Text);
                cmd.Parameters.AddWithValue("@HEDcess", txtHEdc.Text);

                cmd.Parameters.AddWithValue("@Vatamt", txtvatamt.Text);
                cmd.Parameters.AddWithValue("@Frieght", txtfre.Text);
                cmd.Parameters.AddWithValue("@Insurance", txtinsurance.Text);
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@NetAmount", txtnetAmount.Text);
                cmd.Parameters.AddWithValue("@SalesTax", txttax.Text);
                cmd.Parameters.AddWithValue("@Total", txttotal.Text);
                cmd.Parameters.AddWithValue("@PoNo", ViewState["po_no"].ToString());
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                //New parameter created date 17-May-2016 Cr-ENH-MAR-010-2016 STARTS
                cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                //New parameter created date 17-May-2016 Cr-ENH-MAR-010-2016 ENDS

                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "Sucessfull")
                {
                    InvoiceInfo();
                    Viewpop();
                    tblprint.Visible = true;
                    JavaScript.UPAlert(Page, msg);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>myFunction();</script>", false);
                    popreports.Hide();
                    filterbasicprice(s1);
                }
                else
                {
                    tblprint.Visible = false;
                    JavaScript.UPAlert(Page, msg);
                    
                    popreports.Hide();
                    filterbasicprice(s1);
                    
                }
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

            if (e.Row.Cells[7].Text == "0")
                txtbasic.Attributes.Add("readonly", "1");

        }
    }
   
 
    private decimal Amount11 = (decimal)0.0;
    private decimal Amount27 = (decimal)0.0;
    private decimal Amount41 = (decimal)0.0;
    private decimal Amount11A = (decimal)0.0;
    private decimal Amount27A = (decimal)0.0;
    private decimal Amount41A = (decimal)0.0;
    private decimal Amount = (decimal)0.0;
    //private decimal TAmount = (decimal)0.0;
    private decimal STaxAmount = (decimal)0.0;
   
    protected void Grdeditpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal qty;
        decimal price;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["checkview"] != null)
            {
                if (ViewState["checkview"].ToString() != "Checked")
                {
                    Grdeditpopup.Columns[11].Visible = false;
                    HiddenField hf1 = (HiddenField)e.Row.FindControl("h1");
                    if (e.Row.Cells[9].Text != hf1.Value)
                    {
                        e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    Grdeditpopup.Columns[11].Visible = true;
                    qty = Convert.ToDecimal(e.Row.Cells[8].Text);
                    price = Convert.ToDecimal(e.Row.Cells[9].Text);
                    e.Row.Cells[11].Text = Convert.ToDecimal(qty * price).ToString();
                }
            }
            //TAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "newbasicprice"));
        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[9].Text = String.Format("Rs.{0:#,##,##,###.00}", TAmount);

        //}
        
    }
    protected void Grdeditpopupp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal qty;
        decimal price;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["checkview"] != null)
            {
                Grdeditpopupp.Columns[11].Visible = true;
                qty = Convert.ToDecimal(e.Row.Cells[8].Text);
                price = Convert.ToDecimal(e.Row.Cells[9].Text);
                e.Row.Cells[11].Text = Convert.ToDecimal(qty * price).ToString();
                Grdeditpopupp.Columns[10].Visible = false;
                Grdeditpopupp.Columns[0].Visible = false;
            }
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



    public void fillexcise(string s)
    {
        try
        {

            da = new SqlDataAdapter("Select CC_SubType from cost_center where cc_code='" +s+ "'", con);
            da.Fill(ds, "CC_SubType");
            if (ds.Tables["CC_SubType"].Rows.Count > 0)
            {
                if (ds.Tables["CC_SubType"].Rows[0][0].ToString() == "Service")
                {
                    da = new SqlDataAdapter("select ServiceTaxno as Excise_no from ServiceTaxMaster where Status='3'", con);
                }
                else if (ds.Tables["CC_SubType"].Rows[0][0].ToString() == "Capital")
                {
                    da = new SqlDataAdapter("select ServiceTaxno as Excise_no from ServiceTaxMaster where Status='3' union all select Excise_no from ExciseMaster where Status='3'", con);
                }
                else
                {
                    da = new SqlDataAdapter("select Excise_no from ExciseMaster where Status='3'", con);
                }
                da.Fill(ds, "Excise_no");

                ddlExcno.DataValueField = "Excise_no";
                ddlExcno.DataTextField = "Excise_no";
                ddlExcno.DataSource = ds.Tables["Excise_no"];
                ddlExcno.DataBind();
                ddlExcno.Items.Insert(0, "Select");
            }


            da = new SqlDataAdapter("select RegNo from [Saletax/VatMaster] where Status='3'", con);
            da.Fill(ds1);

            ddlvatno.DataValueField = ds1.Tables[0].Columns["RegNo"].ToString();
            ddlvatno.DataSource = ds1.Tables[0];
            ddlvatno.DataBind();
            ddlvatno.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void VATcheck_CheckedChanged(object sender, EventArgs e)
    {
        

    }


    protected void excisecheck_CheckedChanged1(object sender, EventArgs e)
    {
        

    }
    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {
            
            cmd = new SqlCommand("reject_invoice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@invoiceNo", txtin.Text);
            cmd.Parameters.AddWithValue("@invoice_date", txtindt.Text);
            cmd.Parameters.AddWithValue("@total", txttotal.Text);
            cmd.Parameters.AddWithValue("@Advance", txtAdvance.Text);
            cmd.Parameters.AddWithValue("@Hold", txthold.Text);
            cmd.Parameters.AddWithValue("@AnyOther", txtother.Text);
            cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
            if (ddlExcno.SelectedValue != "Select")

                cmd.Parameters.AddWithValue("@Exciseno", ddlExcno.SelectedValue);
            if (ddlvatno.SelectedValue != "Select")
                cmd.Parameters.AddWithValue("@VATno", ddlvatno.SelectedValue);
            cmd.Parameters.AddWithValue("@Exciseduty", txtexduty.Text);
            cmd.Parameters.AddWithValue("@EDcess", txtEdc.Text);
            cmd.Parameters.AddWithValue("@HEDcess", txtHEdc.Text);

            cmd.Parameters.AddWithValue("@VATAmount", txtvatamt.Text);
            cmd.Parameters.AddWithValue("@Frieght", txtfre.Text);
            cmd.Parameters.AddWithValue("@Insurance", txtinsurance.Text);
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@NetAmount", txtnetAmount.Text);
            cmd.Parameters.AddWithValue("@PO_NO", ViewState["po_no"].ToString());
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtname.Text);
            cmd.Parameters.AddWithValue("@description", txtindesc.Text);


            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Rejected")
            {
                JavaScript.UPAlert(Page, msg);
                popreports.Hide();
                filterbasicprice(s1);
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
                popreports.Hide();
                filterbasicprice(s1);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
}
