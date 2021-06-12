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
using System.Web.Services;

public partial class Indent : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 2);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        //if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
        if (Session["roles"].ToString() != "StoreKeeper")
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
        btndeleteitemcodes.Visible = false;
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
        if (Session["roles"].ToString() == "Central Store Keeper")
            da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.Pmremarks, CHARINDEX('$', i.Pmremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status] from indents i where (i.status='2') order by i.status asc", con);
        else if (Session["roles"].ToString() == "Chief Material Controller")
            da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.purmremark, CHARINDEX('$', i.purmremark) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status] from indents i where (i.status='5')  order by i.status asc", con);
        else if (Session["roles"].ToString() == "PurchaseManager")
            da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT( i.Cskremarks, CHARINDEX('$',  i.Cskremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status] from indents i where (i.status in ('4','3')) order by i.status asc", con);
        else if (Session["roles"].ToString() == "Project Manager")
            da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.Remarks, CHARINDEX('$', i.Remarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status  as [Status]from indents i where i.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and (i.status='1') order by i.status asc", con);
        else if (Session["roles"].ToString() == "StoreKeeper")
            da = new SqlDataAdapter("SELECT  distinct i.id, i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.Remarks, CHARINDEX('$', i.Remarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status  as [Status]from indents i where i.cc_code='" + Session["cc_code"].ToString() + "' order by i.status asc", con);
        else if (Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("SELECT  distinct i.id,i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.cmcremarks, CHARINDEX('$', i.cmcremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status  as [Status]from indents i where (i.status='6A')  order by i.status asc", con);
        da.Fill(ds, "central");
        GridView1.DataSource = ds.Tables["central"];
        GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
        GridView1.DataBind();

    }


    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterindents();

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
            filterindents();

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
            filterindents();

        }


        else
        {
            FillGrid();
        }
    }



    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{
    //    LinkButton lnkCustomerID = sender as LinkButton;
    //    string strCustomerID = lnkCustomerID.Text;
    //    da = new SqlDataAdapter("SELECT distinct i.id,indentraise_date as [Date],i.Indent_No as[Indent No],i.CC_Code as [CC Code] from indents i full outer join indent_list il on i.indent_no=il.indent_no", con);
    //    da.Fill(ds, "idinfo");

    //    GridView2.DataSource = ds.Tables["idinfo"];
    //    GridView2.DataBind();
    //    popindents.Show();
    //    txtSearch.Text = "";
    //}

    protected void GridView1_RowEditing2(object sender, GridViewEditEventArgs e)
    {
        if (Session["roles"].ToString() != "StoreKeeper")
        {

            da = new SqlDataAdapter("Select IndentType from indents where indent_no='" + GridView1.DataKeys[e.NewEditIndex]["Indent No"].ToString() + "'", con);
            da.Fill(ds, "type");
            Hftype.Value = ds.Tables["type"].Rows[0].ItemArray[0].ToString();
            string indentno = GridView1.DataKeys[e.NewEditIndex]["Indent No"].ToString();
            ViewState["indentno"] = indentno;
            txtpopindno.Text = ViewState["indentno"].ToString();
            txtpopcccode.Text = GridView1.Rows[e.NewEditIndex].Cells[4].Text;
            popindents.Show();
            tbl.Visible = true;
            grideviewpopup.Visible = false;
            approveind.Visible = true;
            viewind.Visible = false;
            lbltitle.Text = "Verify Indent";
            lbltype.Text = ds.Tables["type"].Rows[0].ItemArray[0].ToString();
            ddlindenttype.SelectedIndex = 0;
            txtSearch.Text = "";
            HiddenField hf = (HiddenField)GridView1.Rows[e.NewEditIndex].Cells[8].FindControl("h1");
            da = new SqlDataAdapter("select top 1 substring(Item_code,1,1) from Indent_list where Indent_no='" + GridView1.DataKeys[e.NewEditIndex]["Indent No"].ToString() + "' order by ID desc", con);
            da.Fill(ds, "indenttypecheck");
            if (ds.Tables["indenttypecheck"].Rows[0].ItemArray[0].ToString() == "4")
            {
                ddlindenttype.Items.FindByValue("Partially Purchase").Enabled = false;
                ddlindenttype.Items.FindByValue("Assets Issue").Enabled = false;
                ddlindenttype.Items.FindByValue("Assets Transfer").Enabled = false;
                ddlindenttype.Items.FindByValue("SemiAssets/Consumable Transfer").Enabled = false;
                ddlindenttype.Items.FindByValue("Full Issue").Enabled = false;
                ddlindenttype.Items.FindByValue("SemiAssets/Consumable Issue").Enabled = true;
            }
            else
            {
                ddlindenttype.Items.FindByValue("Partially Purchase").Enabled = true;
                ddlindenttype.Items.FindByValue("Assets Issue").Enabled = true;
                ddlindenttype.Items.FindByValue("Assets Transfer").Enabled = true;
                ddlindenttype.Items.FindByValue("SemiAssets/Consumable Transfer").Enabled = true;
                ddlindenttype.Items.FindByValue("Full Issue").Enabled = true;
                ddlindenttype.Items.FindByValue("SemiAssets/Consumable Issue").Enabled = true;
            }

            if (hf.Value == "1")/*This is for Project Manager*/
            {

                grideditpopup2.Visible = true;
                Grdeditpopup.Visible = false;
                gridcentral.Visible = false;
                addbtn.Visible = true;
                btnmdlupd.Text = "Verified";
                //btnmdlupd.Visible = true;
                //btncentral.Visible = false;
                tblccode.Visible = false;
                Table1.Visible = true;
                mdlfillgrd();
                tbltype.Visible = false;
                btndeleteitemcodes.Visible = false;

            }
            else if (hf.Value == "2")/* this is for  central storekeeper*/
            {

                grideditpopup2.Visible = false;
                Grdeditpopup.Visible = false;
                gridcentral.Visible = true;
                addbtn.Visible = false;
                btnmdlupd.Text = "Send";
                //btncentral.Visible = true;
                Table1.Visible = false;
                tblccode.Visible = true;
                tdcccode.Visible = true;
                mdlfillgrd();
                tblSearch.Visible = false;
                tbltype.Visible = false;
                btndeleteitemcodes.Visible = false;

            }
            else if (hf.Value == "4" || hf.Value == "3")/* this is for purchasemanager*/
            {
                grideditpopup2.Visible = false;
                gridcentral.Visible = false;
                Grdeditpopup.Visible = true;
                tblSearch.Visible = false;
                addbtn.Visible = false;
                btnmdlupd.Visible = true;
                btnmdlupd.Text = "Submit";
                tblccode.Visible = false;
                lbltitle.Text = "Verify Indent";
                Table1.Visible = true;
                mdlfillgrd();
                tbltype.Visible = true;
                btndeleteitemcodes.Visible = true;

            }
            else if (hf.Value == "5")/* this is for CMC*/
            {
                grideditpopup2.Visible = false;
                gridcentral.Visible = false;
                Grdeditpopup.Visible = false;
                grideviewpopup.Visible = true;
                addbtn.Visible = false;
                btnmdlupd.Text = "Approved";
                lbltitle.Text = "Approve Indent";
                btnmdlupd.Visible = true;
                tblccode.Visible = false;
                Table1.Visible = false;
                popcmc();
                tbltype.Visible = false;
                btndeleteitemcodes.Visible = false;
            }
            else if (hf.Value == "6A")/* this is for SuperAdmin*/
            {
                grideditpopup2.Visible = false;
                gridcentral.Visible = false;
                Grdeditpopup.Visible = false;
                grideviewpopup.Visible = true;
                addbtn.Visible = false;
                btnmdlupd.Text = "Approved";
                lbltitle.Text = "Approve Indent";
                btnmdlupd.Visible = true;
                tblccode.Visible = false;
                Table1.Visible = false;
                popcmc();
                tbltype.Visible = false;
                btndeleteitemcodes.Visible = false;
            }

            else if (hf.Value == "7")
            {
                popindents.Hide();
                tbltype.Visible = false;
                btndeleteitemcodes.Visible = false;
            }
        }
        else
            popindents.Hide();
    }
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        //da = new SqlDataAdapter("Select item_code from indent_list where indent_no='" + GridView1.DataKeys[e.RowIndex]["Indent No"].ToString() + "'", con);
        //da.Fill(ds, "indent");

        //if (ds.Tables["indent"].Rows.Count > 0)
        //{
        //    da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')basic_price,units,il.quantity,replace(il.amount,'.0000','.00')amount from (Select item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,quantity,replace(amount,'.0000','.00')amount from indent_list where  indent_no='" + GridView1.DataKeys[e.RowIndex]["Indent No"].ToString() + "' )il on i.item_code=il.item_code", con);
        //}
        //da.Fill(ds, "viewindent");

        //grideviewpopup.DataSource = ds.Tables["viewindent"];
        //grideviewpopup.DataBind();
        string indentno = GridView1.DataKeys[e.RowIndex]["Indent No"].ToString();
        ViewState["indentno"] = indentno;
        txtpopindno.Text = ViewState["indentno"].ToString();
        txtpopcccode.Text = GridView1.Rows[e.RowIndex].Cells[4].Text;
        popindents.Show();
        tbl.Visible = false;
        grideviewpopup.Visible = true;
        Grdeditpopup.Visible = false;
        grideditpopup2.Visible = false;
        grdcmc.Visible = false;
        gridcentral.Visible = false;
        addbtn.Visible = false;
        btnmdlupd.Visible = false;
        approveind.Visible = false;
        Table1.Visible = false;
        tblccode.Visible = false;
        viewind.Visible = true;
        viewpopup();
    }
    public void viewpopup()
    {
        da = new SqlDataAdapter("select status from Indents where indent_no='" + ViewState["indentno"].ToString() + "'", con);
        da.Fill(ds, "indent");
        string status = ds.Tables["indent"].Rows[0][0].ToString();
        if (status == "6" || status == "7")

            da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')as basic_price,units,il.quantity,replace(il.amount,'.0000','.00')amount from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (select distinct j.Item_code,sum(isnull(j.Quantity,0)) as quantity,(sum(isnull(j.Quantity,0))*isnull(j.Basic_Price,0))amount,j.Basic_Price from (select id,item_code,quantity,replace(amount,'.0000','.00')amount,Basic_Price  from (select id,item_code,quantity,replace(amount,'.0000','.00')amount,Basic_Price  from indent_list where indent_no='" + ViewState["indentno"].ToString() + "' Union select id,item_code,quantity,replace(amount,'.0000','.00')amount,Basic_Price  as poqty from po_details where indent_no='" + ViewState["indentno"].ToString() + "')i	)j where j.Quantity<>0 group by j.Item_code,j.Basic_Price,j.amount)il on i.item_code=il.item_code", con);
        else
            da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')as basic_price,units,il.quantity,replace(il.amount,'.0000','.00')amount from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,quantity,replace(amount,'.0000','.00')amount,Basic_Price from indent_list where  indent_no='" + ViewState["indentno"].ToString() + "' )il on i.item_code=il.item_code;select Remarks,purmremark,Cskremarks,cmcremarks,pmremarks  from indents where indent_no='" + ViewState["indentno"].ToString() + "'", con);
        da.Fill(ds, "viewindent");
        tb2.Visible = false;
        grideviewpopup.DataSource = ds.Tables["viewindent"];
        grideviewpopup.DataBind();


    }
    public void popcmc()
    {
        da = new SqlDataAdapter("Select G.id,G.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')basic_price,units,G.quantity,replace(G.amount,'.0000','.00')amount,isnull(G.IssueQuantity,0)[IssuedStock],isnull([NewStockQty],0)[IssuedNewStock] from(Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select S.id,S.item_code,S.quantity,S.amount,[IssueQuantity],F.Quantity as[NewStockQty],Basic_Price  from (Select il.id,il.item_code,il.quantity,il.amount,T.Quantity as [IssueQuantity],Basic_Price from (Select id,item_code,quantity,replace(amount,'.0000','.00')amount,Basic_Price from indent_list where  indent_no='" + ViewState["indentno"].ToString() + "'  and Quantity>0 )il full outer join (Select item_code,Quantity from [Transfer Info]ti join [Items Transfer]it on ti.ref_no=it.ref_no where indent_no='" + ViewState["indentno"].ToString() + "' and item_status='Stock')T on il.item_code=T.item_code)S full outer join (Select item_code,Quantity from [Transfer Info]tii join [Items Transfer]itt on tii.ref_no=itt.ref_no where indent_no='" + ViewState["indentno"].ToString() + "' and item_status='New Stock')F on S.item_code=F.item_code)G on  i.item_code=G.item_code;select LEFT(Pmremarks, CHARINDEX('$', Pmremarks) - 1) AS Pmremarks,RIGHT(Pmremarks, CHARINDEX('$', REVERSE(Pmremarks)) - 1) AS PMName,LEFT(Cskremarks, CHARINDEX('$', Cskremarks) - 1) AS Cskremarks,RIGHT(Cskremarks, CHARINDEX('$', REVERSE(Cskremarks)) - 1) AS CSKName,LEFT(purmremark, CHARINDEX('$', purmremark) - 1) AS purmremark,RIGHT(purmremark, CHARINDEX('$', REVERSE(purmremark)) - 1) AS purmname,LEFT(cmcremarks, CHARINDEX('$', cmcremarks) - 1) AS cmcremarks,RIGHT(cmcremarks, CHARINDEX('$', REVERSE(cmcremarks)) - 1) AS cmcname,LEFT(Remarks, CHARINDEX('$', Remarks) - 1) AS Remarks,RIGHT(Remarks, CHARINDEX('$', REVERSE(Remarks)) - 1) AS UserName from indents where indent_no='" + ViewState["indentno"].ToString() + "'", con);
        da.Fill(ds, "fillcmc");
        grdcmc.DataSource = ds.Tables["fillcmc"];
        grdcmc.DataBind();
        txtskremark.Text = ds.Tables["fillcmc1"].Rows[0][8].ToString();
        Lblskname.Text = ds.Tables["fillcmc1"].Rows[0][9].ToString();
        txtpmremark.Text = ds.Tables["fillcmc1"].Rows[0][0].ToString();
        Lblpmname.Text = ds.Tables["fillcmc1"].Rows[0][1].ToString();
        txtcskremark.Text = ds.Tables["fillcmc1"].Rows[0][2].ToString();
        Lblcskname.Text = ds.Tables["fillcmc1"].Rows[0][3].ToString();
        txtpurmremark.Text = ds.Tables["fillcmc1"].Rows[0][4].ToString();
        Lblpurname.Text = ds.Tables["fillcmc1"].Rows[0][5].ToString();


        txtskremark.Enabled = false;
        txtpmremark.Enabled = false;
        txtcskremark.Enabled = false;
        txtpurmremark.Enabled = false;
        if (Session["roles"].ToString() == "Chief Material Controller")
        {
            txtsaremark.Visible = false;
            lblsaremark.Visible = false;
        }
        else
        {

            txtcmcremark.Text = ds.Tables["fillcmc1"].Rows[0][6].ToString();
            Lblcmcname.Text = ds.Tables["fillcmc1"].Rows[0][7].ToString();

            txtcmcremark.Enabled = false;
            txtsaremark.Visible = true;
            lblsaremark.Visible = true;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        filterindents();
    }

    public void filterindents()
    {
        try
        {
            string Condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    Condition = Condition + " and datepart(mm,indentraise_date)=" + ddlMonth.SelectedValue + " and datepart(yy,indentraise_date)=" + yy;
                }
                Condition = Condition + "and convert(datetime,indentraise_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
            }
            if (Session["roles"].ToString() != "StoreKeeper")
            {
                if (ddlcccode.SelectedValue != "")
                    Condition = Condition + " and i.cc_code='" + ddlcccode.SelectedValue + "'";

            }
            else
            {
                Condition = Condition + "and i.cc_code='" + Session["cc_code"].ToString() + "'";
            }
            if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("SELECT  distinct i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.Pmremarks, CHARINDEX('$', i.Pmremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status]  from indents i  where i.id>0 and (i.status='2' or i.status='7') " + Condition + " order by i.status asc", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("SELECT  distinct i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.purmremark, CHARINDEX('$', i.purmremark) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status]  from indents i  where i.id>0 and (i.status='5' or i.status='7') " + Condition + " order by i.status asc", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("SELECT  distinct i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT( i.Cskremarks, CHARINDEX('$',  i.Cskremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status]  from indents i  where i.id>0 and (i.status='4' or i.status='7') " + Condition + " order by i.status asc", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("SELECT  distinct i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.Remarks, CHARINDEX('$', i.Remarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status]  from indents i  where i.id>0 and (i.status='1' or i.status='7') " + Condition + " order by i.status asc", con);

            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("SELECT  distinct i.id,i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.cmcremarks, CHARINDEX('$', i.cmcremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status]  from indents i  where i.id>0 and (i.status='6A' or i.status='7') " + Condition + " order by i.status asc", con);
            else if (Session["roles"].ToString() == "StoreKeeper")
                da = new SqlDataAdapter("SELECT  distinct i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.Remarks, CHARINDEX('$', i.Remarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status as [Status]  from indents i  where i.id>0 " + Condition + " order by i.status asc", con);


            da.Fill(ds, "indents");
            GridView1.DataSource = ds.Tables["indents"];
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
        Response.Redirect("Indent.aspx");
    }

    protected void Grdeditpopup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = Grdeditpopup.DataKeys[e.RowIndex]["id"].ToString();
            cmd.Connection = con;
            cmd.CommandText = "delete from indent_list where id='" + id + "'";
            con.Open();

            cmd.ExecuteNonQuery();
            con.Close();
            FillGrid();
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
            Image img = (Image)e.Row.FindControl("Image1");
            HiddenField hf = (HiddenField)e.Row.FindControl("h1");
            Label lblindentno = (Label)e.Row.FindControl("lablindentno");
            if (e.Row.Cells[7].Text == "0.00" && hf.Value == "7")
            {
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM po_details where indent_no='" + lblindentno.Text + "'", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count > 0)
                {
                    da = new SqlDataAdapter("select Replace(Sum(isnull(Amount,0)),'.0000','.00') from po_details where indent_no='" + lblindentno.Text + "'", con);
                    da.Fill(ds, "indentno");
                    e.Row.Cells[7].Text = ds.Tables["indentno"].Rows[0][0].ToString();
                    ds.Tables["indentno"].Reset();
                }
            }
            if ((hf.Value == "1" || hf.Value == "2" || hf.Value == "3" || hf.Value == "4" || hf.Value == "5" || hf.Value == "6A") && Session["roles"].ToString() != "StoreKeeper")
                img.Visible = true;
            else
                img.Visible = false;

        }
    }

    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Qty = "";
            string Pids = "";
            string PQty = "";
            string Sids = "";
            string SQty = "";
            string SAmt = "";
            string Amt = "";
            string Bprice = "";

            if (Session["roles"].ToString() != "SuperAdmin")
                da = new SqlDataAdapter("select first_name+middle_name+last_name from Employee_Data where user_name='" + Session["user"].ToString() + "'", con);
            else
                da = new SqlDataAdapter("select first_name from Register where user_name='" + Session["user"].ToString() + "'", con);
            da.Fill(ds, "username");

            if (Session["roles"].ToString() == "Project Manager")
            {

                foreach (GridViewRow record in grideditpopup2.Rows)
                {
                    da = new SqlDataAdapter("Select indent_no from indent_list where id='" + grideditpopup2.DataKeys[record.RowIndex]["id"].ToString() + "'", con);
                    da.Fill(ds, "indentno");

                    if (ds.Tables["indentno"].Rows.Count > 0)
                    {
                        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                        if (c1.Checked)
                        {

                            Sids = Sids + grideditpopup2.DataKeys[record.RowIndex]["id"].ToString() + ",";
                            SQty = SQty + (record.FindControl("txtqty") as TextBox).Text + ",";
                            SAmt = SAmt + (record.FindControl("txtamount") as TextBox).Text + ",";
                            if (record.Cells[5].Text == "DCA-27")
                                Amount27 = Amount27 + Convert.ToDecimal((record.FindControl("txtamount") as TextBox).Text);
                            else if (record.Cells[5].Text == "DCA-11")
                                Amount11 = Amount11 + Convert.ToDecimal((record.FindControl("txtamount") as TextBox).Text);
                            else if (record.Cells[5].Text == "DCA-41")
                                Amount41 = Amount41 + Convert.ToDecimal((record.FindControl("txtamount") as TextBox).Text);
                            else if (record.Cells[5].Text == "DCA-24")
                                Amount24 = Amount24 + Convert.ToDecimal((record.FindControl("txtamount") as TextBox).Text);
                        }
                    }

                }

                if (Amount27 != 0 || Amount11 != 0 || Amount41 != 0 || Amount24 != 0)
                {
                    cmd = new SqlCommand("IndentForIncharge_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ids", Sids);
                    cmd.Parameters.AddWithValue("@NewQtys", SQty);
                    cmd.Parameters.AddWithValue("@NewAmts", SAmt);
                    //cmd.Parameters.AddWithValue("@CCCode", Session["cc_code"].ToString());
                    cmd.Parameters.AddWithValue("@Amount11", Amount11);
                    cmd.Parameters.AddWithValue("@Amount27", Amount27);
                    cmd.Parameters.AddWithValue("@Amount41", Amount41);
                    cmd.Parameters.AddWithValue("@Amount24", Amount24);
                    cmd.Parameters.AddWithValue("@Indent_No", ds.Tables["indentno"].Rows[0].ItemArray[0].ToString());
                    cmd.Parameters.AddWithValue("@Pmremarks", txtpmremark.Text + '$' + ds.Tables["username"].Rows[0][0].ToString());
                    cmd.Connection = con;
                    con.Open();

                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "True";
                    con.Close();
                    if (msg == "Verified Sucessfully")
                        JavaScript.UPAlertRedirect(Page, msg, "Indent.aspx");
                    else
                        JavaScript.UPAlert(Page, msg);
                }
                else
                {
                    cmd.CommandText = "Update Indents set status='Closed' where indent_no='" + ViewState["indentno"].ToString() + "'";
                    cmd.Connection = con;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                        JavaScript.UPAlertRedirect(Page, "The Indent is Closed Sucessfull", "Indent.aspx");
                    else
                        JavaScript.UPAlert(Page, "The Indent is Closed Sucessfull");
                }

            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                if (ddlindenttype.SelectedItem.Text == "Assets Issue" || ddlindenttype.SelectedItem.Text == "Assets Transfer")
                {
                    tdcccode.Visible = false;
                    cmd = new SqlCommand("IndentTypeCheck_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@indenttype", ddlindenttype.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@indentno", ViewState["indentno"].ToString());
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if (msg == "Please give clearence to Previous Indent")
                        JavaScript.UPAlertRedirect(Page, msg, "Indent.aspx");
                    else
                        Response.Redirect("AssetsTransfer.aspx", false);


                }

                else
                {
                    foreach (GridViewRow record in gridcentral.Rows)
                    {
                        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                        if (ddlindenttype.SelectedItem.Text == "Partially Purchase" || ddlindenttype.SelectedItem.Text == "Full Purchase" || ddlindenttype.SelectedItem.Text == "Full Issue")
                        {
                            if (c1.Checked && (record.FindControl("txtqty") as TextBox).Text != "" && ddlindenttype.SelectedItem.Text != "Full Purchase")
                            {

                                ids = ids + gridcentral.DataKeys[record.RowIndex]["id"].ToString() + ",";
                                Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                                Amt = Amt + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(Convert.ToDecimal(record.Cells[9].Text) - Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text))) + ",";
                                TotalQty = TotalQty + Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text);
                                Bprice = Bprice + Convert.ToDecimal(record.Cells[7].Text) + ",";
                            }
                        }

                        else
                        {
                            if (c1.Checked)
                            {

                                ids = ids + gridcentral.DataKeys[record.RowIndex]["id"].ToString() + ",";
                                Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                                Bprice = Bprice + Convert.ToDecimal(record.Cells[7].Text) + ",";

                            }
                        }
                    }

                    cmd = new SqlCommand("UpdateIndent_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ids", ids);
                    cmd.Parameters.AddWithValue("@Nids", ids);
                    cmd.Parameters.AddWithValue("@NewQtys", Qty);
                    cmd.Parameters.AddWithValue("@Newids", ids);
                    cmd.Parameters.AddWithValue("@NQty", Qty);
                    cmd.Parameters.AddWithValue("@Basics", Bprice);
                    cmd.Parameters.AddWithValue("@Amounts", Amt);
                    cmd.Parameters.AddWithValue("@Totalqty", TotalQty);
                    cmd.Parameters.AddWithValue("@CCCode", Session["cc_code"].ToString());
                    cmd.Parameters.AddWithValue("@TransferCC", ddlpopcccode.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@IndentType", ddlindenttype.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
                    cmd.Parameters.AddWithValue("@Indent_No", ViewState["indentno"].ToString());
                    cmd.Parameters.AddWithValue("@cskremarks", txtcskremark.Text + '$' + ds.Tables["username"].Rows[0][0].ToString());

                    cmd.Connection = con;
                    con.Open();

                    string msg = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if (msg != "Sucessfull")
                        JavaScript.UPAlert(Page, msg);

                    else if (msg == "Sucessfull" && ddlindenttype.SelectedItem.Text == "Full Purchase" && TotalQty == 0)
                        JavaScript.UPAlertRedirect(Page, msg, "Indent.aspx");
                    else if (msg == "Sucessfull" && ddlindenttype.SelectedItem.Text == "Full Issue")
                        JavaScript.UPAlertRedirect(Page, msg, "Indent.aspx");
                    //else if (msg == "Sucessfull" && ddlindenttype.SelectedItem.Text == "Partially Purchase" && TotalQty == 0)
                    //    JavaScript.UPAlertRedirect(Page, msg, "IssueFromCentralStore.aspx");
                    else if (msg == "Sucessfull" && (ddlindenttype.SelectedItem.Text == "SemiAssets/Consumable Transfer" || ddlindenttype.SelectedItem.Text == "SemiAssets/Consumable Issue" || ddlindenttype.SelectedItem.Text == "Partially Purchase"))
                        JavaScript.UPAlertRedirect(Page, msg, "TransferOut.aspx");

                    else
                        JavaScript.UPAlert(Page, msg);
                }
            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {

                da = new SqlDataAdapter("Select indenttype from indents where indent_no='" + ViewState["indentno"].ToString() + "'", con);
                da.Fill(ds, "indenttype");
                if (ds.Tables["indenttype"].Rows.Count > 0)
                {
                    foreach (GridViewRow record in Grdeditpopup.Rows)
                    {
                        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");


                        if (c1.Checked && (record.FindControl("txtqty") as TextBox).Text != "")
                        {

                            Pids = Pids + Grdeditpopup.DataKeys[record.RowIndex]["id"].ToString() + ",";
                            PQty = PQty + (record.FindControl("txtqty") as TextBox).Text + ",";
                            Bprice = Bprice + Convert.ToDecimal(record.Cells[7].Text) + ",";
                            if (ds.Tables["indenttype"].Rows[0].ItemArray[0].ToString() == "Full Issue")
                                Amt = Amt + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(Convert.ToDecimal(record.Cells[9].Text))) + ",";
                            else
                                Amt = Amt + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(Convert.ToDecimal(record.Cells[9].Text) - Convert.ToDecimal(Convert.ToDecimal(record.Cells[10].Text) + Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text)))) + ",";
                            TotalQty = TotalQty + Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text);

                        }
                    }
                }
                cmd = new SqlCommand("UpdateIndent_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", Pids);
                cmd.Parameters.AddWithValue("@NewQtys", PQty);
                cmd.Parameters.AddWithValue("@Newids", Pids);
                cmd.Parameters.AddWithValue("@NQty", PQty);
                cmd.Parameters.AddWithValue("@Basics", Bprice);
                cmd.Parameters.AddWithValue("@Amounts", Amt);
                cmd.Parameters.AddWithValue("@CCCode", "CC-33");
                cmd.Parameters.AddWithValue("@IndentType", ds.Tables["indenttype"].Rows[0][0].ToString());
                cmd.Parameters.AddWithValue("@Totalqty", TotalQty);
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@Indent_No", ViewState["indentno"].ToString());
                cmd.Parameters.AddWithValue("@purmremark", txtpurmremark.Text + '$' + ds.Tables["username"].Rows[0][0].ToString());
                cmd.Connection = con;
                con.Open();

                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "";
                con.Close();
                if (msg != "Sucessfull")
                    JavaScript.UPAlert(Page, msg);
                else if (msg == "Sucessfull" && TotalQty == 0)
                    JavaScript.UPAlertRedirect(Page, msg, "Indent.aspx");
                else if (msg == "Sucessfull" && TotalQty != 0)
                    JavaScript.UPAlertRedirect(Page, msg, "IssueFromCentralStore.aspx");


            }

            else if (Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
            {

                foreach (GridViewRow record in grdcmc.Rows)
                {

                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                    if (record.Cells[5].Text == "DCA-27")
                        Amount27A = Amount27A + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text));
                    else if (record.Cells[5].Text == "DCA-11")
                        Amount11A = Amount11A + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text));
                    else if (record.Cells[5].Text == "DCA-41")
                        Amount41A = Amount41A + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text));
                    else if (record.Cells[5].Text == "DCA-24")
                        Amount24A = Amount24A + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text));

                    if (c1.Checked && (record.FindControl("txtqty") as TextBox).Text != "")
                    {

                        Sids = Sids + grdcmc.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        SQty = SQty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        SAmt = SAmt + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text)) + ",";

                        if (record.Cells[5].Text == "DCA-27")
                            Amount27 = Amount27 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                        else if (record.Cells[5].Text == "DCA-11")
                            Amount11 = Amount11 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                        else if (record.Cells[5].Text == "DCA-41")
                            Amount41 = Amount41 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                        else if (record.Cells[5].Text == "DCA-24")
                            Amount24 = Amount24 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                    }
                    else
                    {

                        if (record.Cells[5].Text == "DCA-27")
                            Amount27 = Amount27 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text));
                        else if (record.Cells[5].Text == "DCA-11")
                            Amount11 = Amount11 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text));
                        else if (record.Cells[5].Text == "DCA-41")
                            Amount41 = Amount41 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text));
                        else if (record.Cells[5].Text == "DCA-24")
                            Amount24 = Amount24 + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text));
                    }
                    string s = ((record.FindControl("txtqty") as TextBox).Text);
                    if (s == "")
                    {
                        TotalQty = TotalQty + Convert.ToDecimal(record.Cells[9].Text);
                    }
                    else if (s == "0")
                    {
                        TotalQty = TotalQty + Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text);
                    }
                    else
                    {
                        TotalQty = TotalQty + Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text);
                    }

                }
                cmd = new SqlCommand("IndentApprovalbyCMC_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ids", Sids);
                cmd.Parameters.AddWithValue("@Qtys", SQty);
                cmd.Parameters.AddWithValue("@NewAmts", SAmt);
                cmd.Parameters.AddWithValue("@Amount11", Amount11);
                cmd.Parameters.AddWithValue("@Amount27", Amount27);
                cmd.Parameters.AddWithValue("@Amount41", Amount41);
                cmd.Parameters.AddWithValue("@Amount24", Amount24);
                cmd.Parameters.AddWithValue("@Amount11A", Amount11A);
                cmd.Parameters.AddWithValue("@Amount27A", Amount27A);
                cmd.Parameters.AddWithValue("@Amount41A", Amount41A);
                cmd.Parameters.AddWithValue("@Amount24A", Amount24A);
                cmd.Parameters.AddWithValue("@Totalqty", TotalQty);
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@IndentNo", ViewState["indentno"].ToString());
                cmd.Parameters.AddWithValue("@cmcremarks", txtcmcremark.Text + '$' + ds.Tables["username"].Rows[0][0].ToString());
                cmd.Parameters.AddWithValue("@saremarks", txtsaremark.Text + '$' + ds.Tables["username"].Rows[0][0].ToString());
                cmd.Connection = con;
                con.Open();

                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                //if (Session["roles"].ToString() == "Chief Material Controller")
                //{
                if (msg == "Sucessfull" || msg == "The Indent is Closed Sucessfully")
                    JavaScript.UPAlertRedirect(Page, msg, "Indent.aspx");
                else if (msg != "Sucessfull")
                    JavaScript.UPAlert(Page, msg);
                //}
                //else
                //{
                //    if (msg == "Sucessfull" || msg == "The Indent is Closed Sucessfully")
                //        JavaScript.UPAlertRedirect(Page, msg, "Inbox.aspx");
                //    else if (msg != "Sucessfull")
                //        JavaScript.UPAlert(Page, msg);
                //}

            }

        }





        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }


    private decimal Amount11 = (decimal)0.0;
    private decimal Amount27 = (decimal)0.0;
    private decimal Amount41 = (decimal)0.0;
    private decimal Amount24 = (decimal)0.0;
    private decimal Amount11A = (decimal)0.0;
    private decimal Amount27A = (decimal)0.0;
    private decimal Amount41A = (decimal)0.0;
    private decimal Amount24A = (decimal)0.0;
    private decimal Amount = (decimal)0.0;
    private decimal TotalQty = (decimal)0.0;
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            cmd.Connection = con;
            con.Open();
            char c = txtSearch.Text[0];
            if (char.IsNumber(c))
            {
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_code='" + txtSearch.Text + "' and status in ('4','5','5A','2A')", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)
                {
                    da = new SqlDataAdapter("SELECT top 1 il.item_code,it_code FROM indent_list il join item_codes ic on il.item_code=ic.item_code join subdca sd on ic.Subdca_Code=sd.subdca_code where indent_no ='" + ViewState["indentno"].ToString() + "' order by il.Id asc ", con);
                    da.Fill(ds, "itemcodes");

                    da = new SqlDataAdapter("Select dca_code from (Select item_code,item_name,specification,dca_code,subdca_code,units,isnull(basic_price,0)basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,isnull(quantity,0)quantity,(Select isnull(quantity,0)quantity from Master_data where cc_code=(Select cc_code from indents where indent_no='" + ViewState["indentno"].ToString() + "') and Item_code=k.item_code)AvailQty from indent_list k where  indent_no='" + ViewState["indentno"].ToString() + "')il on i.item_code=il.item_code group by Dca_Code ; SELECT ic.Dca_Code,sd.it_code FROM item_codes ic join subdca sd  on ic.Subdca_Code=sd.subdca_code where item_code='" + txtSearch.Text.ToUpper() + "'", con);
                    da.Fill(ds, "checkdca");
                    if ((ds.Tables["itemcodes"].Rows.Count == 0) || (ds.Tables["itemcodes"].Rows[0].ItemArray[1].ToString() == ds.Tables["checkdca1"].Rows[0].ItemArray[1].ToString()))
                    {
                        if ((ds.Tables["checkdca"].Rows.Count == 0) || (ds.Tables["checkdca"].Rows[0].ItemArray[0].ToString() == ds.Tables["checkdca1"].Rows[0].ItemArray[0].ToString()))
                        {
                            cmd.CommandText = "insert into indent_list(item_code,indent_no)values(@itemcode,@indentno)";
                            cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = txtSearch.Text.ToUpper();
                            //cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                            cmd.Parameters.Add("@indentno", SqlDbType.VarChar).Value = ViewState["indentno"].ToString();
                            int i = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (i == 1)
                            {

                            }
                            mdlfillgrd();
                            txtSearch.Text = "";
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Please add same DCA items only");
                            txtSearch.Text = "";
                        }
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, "Please add only  " + ds.Tables["itemcodes"].Rows[0].ItemArray[1].ToString() + "  IT Code Items only in the Indent");
                        txtSearch.Text = "";
                    }
                }
                else
                {

                    JavaScript.UPAlert(Page, "Invalid itemcode");
                    txtSearch.Text = "";
                }
            }
            else
            {
                int n = txtSearch.Text.IndexOf(",");
                if (n != -1)
                {
                    string result = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
                    string result1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','5','5A','2A')", con);
                    da.Fill(ds, "check");
                    if (ds.Tables["check"].Rows.Count == 1)
                    {
                        da = new SqlDataAdapter("Select item_code from item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','5','5A','2A')", con);
                        da.Fill(ds, "search");
                        if (ds.Tables["search"].Rows.Count > 0)
                        {
                            da = new SqlDataAdapter("SELECT top 1 il.item_code,it_code FROM indent_list il join item_codes ic on il.item_code=ic.item_code join subdca sd on ic.Subdca_Code=sd.subdca_code where indent_no ='" + ViewState["indentno"].ToString() + "' order by il.Id asc ", con);
                            da.Fill(ds, "itemcodess");

                            da = new SqlDataAdapter("Select dca_code from (Select item_code,item_name,specification,dca_code,subdca_code,units,isnull(basic_price,0)basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,isnull(quantity,0)quantity,(Select isnull(quantity,0)quantity from Master_data where cc_code=(Select cc_code from indents where indent_no='" + ViewState["indentno"].ToString() + "') and Item_code=k.item_code)AvailQty from indent_list k where  indent_no='" + ViewState["indentno"].ToString() + "')il on i.item_code=il.item_code group by Dca_Code; SELECT ic.Dca_Code,sd.it_code FROM item_codes ic join subdca sd  on ic.Subdca_Code=sd.subdca_code where item_code='" + ds.Tables["search"].Rows[0].ItemArray[0].ToString() + "'", con);
                            da.Fill(ds, "checkdcas");
                            if ((ds.Tables["itemcodess"].Rows.Count == 0) || (ds.Tables["itemcodess"].Rows[0].ItemArray[1].ToString() == ds.Tables["checkdcas1"].Rows[0].ItemArray[1].ToString()))
                            {
                                if ((ds.Tables["checkdcas"].Rows.Count == 0) || (ds.Tables["checkdcas"].Rows[0].ItemArray[0].ToString() == ds.Tables["checkdcas1"].Rows[0].ItemArray[0].ToString()))
                                {
                                    //cmd.CommandText = "insert into indent_list(item_code)values(@itemcode)";
                                    cmd.CommandText = "insert into indent_list(item_code,indent_no)values(@itemcode,@indentno)";
                                    cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = ds.Tables["search"].Rows[0].ItemArray[0].ToString();
                                    //cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                                    cmd.Parameters.Add("@indentno", SqlDbType.VarChar).Value = ViewState["indentno"].ToString();
                                    int i = Convert.ToInt32(cmd.ExecuteScalar());
                                    con.Close();
                                    if (i == 1)
                                    {

                                    }
                                    mdlfillgrd();
                                    txtSearch.Text = "";
                                }
                                else
                                {
                                    JavaScript.UPAlert(Page, "Please add same DCA items only");
                                    txtSearch.Text = "";
                                }
                            }
                            else
                            {
                                JavaScript.UPAlert(Page, "Please add only  " + ds.Tables["itemcodess"].Rows[0].ItemArray[1].ToString() + "  IT Code Items only in the Indent");
                                txtSearch.Text = "";
                            }
                        }
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, "Invalid Specification");
                        txtSearch.Text = "";
                    }
                }

            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void mdlfillgrd()
    {
        try
        {

            if (Session["roles"].ToString() == "Project Manager")
            {
                //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity,Replace(il.amount,'.00','')amount,isnull(AvailQty,0)Availqty from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,isnull(quantity,0)quantity,isnull(Amount,0)[Amount],(Select isnull(quantity,0)quantity from Master_data where cc_code=(Select cc_code from indents where indent_no='" + ViewState["indentno"].ToString() + "') and Item_code=k.item_code)AvailQty from indent_list k where  indent_no='" + ViewState["indentno"].ToString() + "' )il on i.item_code=il.item_code", con);
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(Basic_Price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity,CAST(ISNULL(il.quantity,0)*isnull(basic_price,0)as decimal(20,2))amount,isnull(AvailQty,0)Availqty from (Select item_code,item_name,specification,dca_code,subdca_code,units,isnull(basic_price,0)basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,isnull(quantity,0)quantity,(Select isnull(quantity,0)quantity from Master_data where cc_code=(Select cc_code from indents where indent_no='" + ViewState["indentno"].ToString() + "') and Item_code=k.item_code)AvailQty from indent_list k where  indent_no='" + ViewState["indentno"].ToString() + "' )il on i.item_code=il.item_code;select LEFT(Remarks, CHARINDEX('$', Remarks) - 1) AS Remarks,RIGHT(Remarks, CHARINDEX('$', REVERSE(Remarks)) - 1) AS UserName from indents where indent_no='" + ViewState["indentno"].ToString() + "'", con);
                da.Fill(ds, "idinfo1");

                grideditpopup2.DataSource = ds.Tables["idinfo1"];
                grideditpopup2.DataBind();
                txtskremark.Text = ds.Tables["idinfo11"].Rows[0][0].ToString();
                Lblskname.Text = ds.Tables["idinfo11"].Rows[0][1].ToString();
                txtskremark.Enabled = false;
                txtcmcremark.Visible = false;
                txtpurmremark.Visible = false;
                txtcskremark.Visible = false;
                txtsaremark.Visible = false;
                lblcmcremark.Visible = false;
                lblcskremark.Visible = false;
                lblpurmremark.Visible = false;
                lblsaremark.Visible = false;

            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity,CAST(ISNULL(il.quantity,0)*isnull(basic_price,0)as decimal(20,2))amount,isnull(AvailQty,0)Availqty,isnull(NewQty,0)Newqty from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i  right outer join (Select id,substring(item_code,1,8)item_code,isnull(quantity,0)quantity,[Amount],isnull(Basic_Price,0)[Basic_Price],(Select sum(isnull(quantity,0))quantity from Master_data where cc_code= '" + Session["cc_code"].ToString() + "' and substring(item_code,1,8)=k.item_code)AvailQty,(Select isnull(quantity,0)quantity from [New Items] where cc_code='" + Session["cc_code"].ToString() + "' and Item_code=k.item_code)Newqty from indent_list k where  indent_no='" + ViewState["indentno"].ToString() + "')il on i.item_code=il.item_code;select LEFT(Pmremarks, CHARINDEX('$', Pmremarks) - 1) AS Pmremarks,RIGHT(Pmremarks, CHARINDEX('$', REVERSE(Pmremarks)) - 1) AS PMName,LEFT(Remarks, CHARINDEX('$', Remarks) - 1) AS Remarks,RIGHT(Remarks, CHARINDEX('$', REVERSE(Remarks)) - 1) AS UserName from indents where indent_no='" + ViewState["indentno"].ToString() + "'", con);
                da.Fill(ds, "info1");
                gridcentral.DataSource = ds.Tables["info1"];
                gridcentral.DataBind();
                txtskremark.Text = ds.Tables["info11"].Rows[0][2].ToString();
                txtpmremark.Text = ds.Tables["info11"].Rows[0][0].ToString();
                Lblskname.Text = ds.Tables["info11"].Rows[0][3].ToString();
                Lblpmname.Text = ds.Tables["info11"].Rows[0][1].ToString();
                txtpmremark.Enabled = false;
                txtskremark.Enabled = false;
                txtcmcremark.Visible = false;
                txtsaremark.Visible = false;
                txtcskremark.Enabled = true;
                txtpurmremark.Visible = false;
                lblcmcremark.Visible = false;
                lblsaremark.Visible = false;
                lblpurmremark.Visible = false;


            }

            else
            {
                da = new SqlDataAdapter("Select ref_no from [Transfer Info] where indent_no='" + ViewState["indentno"].ToString() + "'", con);
                da.Fill(ds, "refno");
                if (ds.Tables["refno"].Rows.Count > 0)
                {
                    da = new SqlDataAdapter("Select S.id,S.item_code,S.item_name,S.specification,S.dca_code,S.subdca_code,S.basic_price,S.units,(isnull(S.quantity,0)+isnull(it.quantity,0)) as [Raised Qty],isnull(it.quantity,0) as[Issued Qty],S.Amount,isnull([Available Qty] ,0)[Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity,Replace(il.amount,'.00','')amount,[Available Qty] from ((Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,quantity,[Amount],(Select quantity from [New Items] where Item_code=i.item_code)[Available Qty],Basic_Price from indent_list i where  indent_no='" + ViewState["indentno"].ToString() + "' and i.Quantity>0 )il on i.item_code=il.item_code))S  left outer join (Select item_code,Quantity from [items transfer] where ref_no='" + ds.Tables["refno"].Rows[0].ItemArray[0].ToString() + "') it on S.item_code=it.item_code;select LEFT(Pmremarks, CHARINDEX('$', Pmremarks) - 1) AS Pmremarks,RIGHT(Pmremarks, CHARINDEX('$', REVERSE(Pmremarks)) - 1) AS PMName,LEFT(Cskremarks, CHARINDEX('$', Cskremarks) - 1) AS Cskremarks,RIGHT(Cskremarks, CHARINDEX('$', REVERSE(Cskremarks)) - 1) AS CSKName,LEFT(Remarks, CHARINDEX('$', Remarks) - 1) AS Remarks,RIGHT(Remarks, CHARINDEX('$', REVERSE(Remarks)) - 1) AS UserName from indents where indent_no='" + ViewState["indentno"].ToString() + "'", con);
                }
                else
                {
                    da = new SqlDataAdapter("Select S.id,S.item_code,S.item_name,S.specification,S.dca_code,S.subdca_code,S.basic_price,S.units,(isnull(S.quantity,0)+isnull(it.quantity,0)) as [Raised Qty],isnull(it.quantity,0) as[Issued Qty],S.Amount,isnull([Available Qty] ,0)[Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity,Replace(il.amount,'.00','')amount,[Available Qty] from ((Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,quantity,[Amount],(Select quantity from [New Items] where Item_code=i.item_code)[Available Qty],Basic_Price from indent_list i where  indent_no='" + ViewState["indentno"].ToString() + "' and i.Quantity>0 )il on i.item_code=il.item_code))S  left outer join (Select item_code,Quantity from [items transfer] where ref_no='0') it on S.item_code=it.item_code;select LEFT(Pmremarks, CHARINDEX('$', Pmremarks) - 1) AS Pmremarks,RIGHT(Pmremarks, CHARINDEX('$', REVERSE(Pmremarks)) - 1) AS PMName,LEFT(Cskremarks, CHARINDEX('$', Cskremarks) - 1) AS Cskremarks,RIGHT(Cskremarks, CHARINDEX('$', REVERSE(Cskremarks)) - 1) AS CSKName,LEFT(Remarks, CHARINDEX('$', Remarks) - 1) AS Remarks,RIGHT(Remarks, CHARINDEX('$', REVERSE(Remarks)) - 1) AS UserName from indents where indent_no='" + ViewState["indentno"].ToString() + "'", con);
                }
                da.Fill(ds, "info1");
                Grdeditpopup.DataSource = ds.Tables["info1"];
                Grdeditpopup.DataBind();

                if (ds.Tables["info11"].Rows.Count > 0)
                {
                    txtskremark.Text = ds.Tables["info11"].Rows[0][4].ToString();
                    Lblskname.Text = ds.Tables["info11"].Rows[0][5].ToString();
                    txtpmremark.Text = ds.Tables["info11"].Rows[0][0].ToString();
                    Lblpmname.Text = ds.Tables["info11"].Rows[0][1].ToString();
                    txtcskremark.Text = ds.Tables["info11"].Rows[0][2].ToString();
                    Lblcskname.Text = ds.Tables["info11"].Rows[0][3].ToString();
                    txtcmcremark.Visible = false;
                    txtsaremark.Visible = false;
                    txtcskremark.Enabled = false;
                    txtpmremark.Enabled = false;
                    txtskremark.Enabled = false;
                    lblcmcremark.Visible = false;
                    lblsaremark.Visible = false;
                }


            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }



    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //Select S.id,S.item_code,S.item_name,S.specification,S.dca_code,S.subdca_code,S.basic_price,S.units,(isnull(S.quantity,0)+isnull(it.quantity,0)) as [Raised Qty],isnull(it.quantity,0) as[Issued Qty],S.rate,S.Amount from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity,Replace(il.rate,'.00','')rate,Replace(il.amount,'.00','')amount from 
    //((Select item_code,item_name,specification,dca_code,subdca_code,basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join 
    //(Select id,item_code,quantity,Replace(rate,'.00','')rate,(quantity*rate)as[Amount] from indent_list where  indent_no='CC-42/2011-12/3' )il on i.item_code=il.item_code))S  left outer join (Select item_code,Quantity from [items transfer] where ref_no='4.65705e+009') it on S.item_code=it.item_code


    protected void Grdeditpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int rowIndex = Convert.ToInt32(e.Row.DataItemIndex) + 1;
            for (int i = 1; i < e.Row.Cells.Count - 1; i++)
            {
                //EventHandler evtHandler = new EventHandler();
                //evtHandler = "updateValue(" + GridView1.ClientID + "," + rowIndex + ")";
                //((TextBox)e.Row.FindControl("TextBox" + i.ToString())).Attributes.Add("onblur", evtHandler);
            }
        }
    }
    protected void Grdeditpopup_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            TextBox txtqty = (TextBox)e.Row.FindControl("txtqty");
            if (e.Row.Cells[9].Text == e.Row.Cells[10].Text)
                txtqty.Attributes.Add("readonly", "1");





        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[12].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow record in grideditpopup2.Rows)
        //{
        //    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

        //    if (c1.Checked)
        //    {
        //        string id = grideditpopup2.DataKeys[record.RowIndex]["id"].ToString();
        //        cmd.Connection = con;
        //        cmd.CommandText = "delete from indent_list where id='" + id + "'";
        //        con.Open();

        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        //FillGrid();
        //        mdlfillgrd();
        //    }

        //}
        string ids = "";
        foreach (GridViewRow record in grideditpopup2.Rows)
        {
            if ((record.FindControl("chkSelect") as CheckBox).Checked)
            {
                ids = ids + grideditpopup2.DataKeys[record.RowIndex]["id"].ToString() + ",";
            }
        }
        if (ids != "")
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_DeleteIndentItems";
            cmd.Parameters.Add(new SqlParameter("@Ids", ids));
            cmd.Parameters.Add(new SqlParameter("@Indentno", ViewState["indentno"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@Role", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "IndentlistDelete")
            {
                mdlfillgrd();
            }
            else
            {
                popindents.Hide();
                JavaScript.UPAlertRedirect(Page, "The Indent Deleted Sucessfull", "Indent.aspx");
            }

        }
        else
        {
            JavaScript.UPAlert(Page, "Select items to delete");
        }

    }
    protected void gridcentral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (GridView1.Rows[0].Cells[8].Text == "Transfer")
        {
            e.Row.Cells[10].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

        }
    }
    protected void grideditpopup2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

        }
    }
    protected void ddlsearchtype_SelectedIndexChanged(object sender, EventArgs e)
    {

        cascadingDCA cs = new cascadingDCA();
        //cs.values(ddlsearchtype.SelectedValue);
        if (ddlsearchtype.SelectedValue != "3" && ddlsearchtype.SelectedValue != "Select Type" && ddlsearchtype.SelectedValue != "4")
        {
            cs.values(ddlsearchtype.SelectedValue);
        }
        else if (ddlsearchtype.SelectedValue == "3")
        {
            cs.values("'3'");
        }
        else if (ddlsearchtype.SelectedValue == "4")
        {
            cs.values("'4'");
        }


    }
    protected void ddlindenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlindenttype.SelectedItem.Text == "Assets Issue" || ddlindenttype.SelectedItem.Text == "Assets Transfer")
        {
            tdcccode.Visible = false;
            cmd = new SqlCommand("IndentTypeCheck_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@indenttype", ddlindenttype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@indentno", ViewState["indentno"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Please give clearence to Previous Indent")
                JavaScript.UPAlertRedirect(Page, msg, "Indent.aspx");
            else
                Response.Redirect("AssetsTransfer.aspx", false);


        }
        else if (ddlindenttype.SelectedItem.Text == "SemiAssets/Consumable Transfer")
        {

            tdcccode.Visible = true;
        }
        else
        {
            tdcccode.Visible = false;


        }

    }
    protected void grdcmc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

        }
    }
    //private decimal TotalSumQty = (decimal)0.0;
    //private decimal TotalActQty = (decimal)0.0;
    protected void btndelteitemcodes_Click(object sender, EventArgs e)
    {
        try
        {

            string Pids = "";
            string PQty = "";
            string Amt = "";
            string Bprice = "";
            string SumQty = "";
            if (Session["roles"].ToString() == "PurchaseManager")
            {
                foreach (GridViewRow record in Grdeditpopup.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                    if (c1.Checked)
                    {
                        Pids = Pids + Grdeditpopup.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        PQty = PQty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        Bprice = Bprice + Convert.ToDecimal(record.Cells[7].Text) + ",";
                        SumQty = SumQty + Convert.ToDecimal(Convert.ToDecimal(record.Cells[9].Text) - Convert.ToDecimal(Convert.ToDecimal(record.Cells[10].Text))) + ",";
                        //TotalSumQty = TotalSumQty + Convert.ToDecimal(Convert.ToDecimal(record.Cells[9].Text)+Convert.ToDecimal(Convert.ToDecimal(record.Cells[10].Text)));


                    }

                }
                cmd = new SqlCommand("DeleteIndentItems_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", Pids);
                cmd.Parameters.AddWithValue("@SumQtys", SumQty);
                cmd.Parameters.AddWithValue("@BasicPrices", Bprice);
                //cmd.Parameters.AddWithValue("@TotalSumQty", TotalSumQty);
                cmd.Parameters.AddWithValue("@CCCode", txtpopcccode.Text);
                //cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@Indent_No", ViewState["indentno"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "";
                con.Close();
                if (msg != "Sucessfull")
                    JavaScript.UPAlert(Page, msg);
                else if (msg == "Sucessfull")
                    JavaScript.UPAlertRedirect(Page, msg, "Indent.aspx");

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }



}
//Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity,(quantity*basic_price)[Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join 
//(Select id,item_code,quantity from indent_list where  indent_no='CC-42/2011-12/3' )il on i.item_code=il.item_code
