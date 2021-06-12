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
using System.Text;
using AjaxControlToolkit;
using System.Collections.Specialized;
using System.Web.Services;
using System.Web.Services.Protocols;
//using System.Drawing;

public partial class ViewStock : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataAdapter da = null;
    decimal amount;
    string qty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("Default.aspx");
        if (!IsPostBack)
        {
            //fillgrid();
            //ViewState["Condition"] = "and i.item_code is not null";
            Searchbox.Visible = false;
            filter.Visible = false;
            filters.Visible = false;
            printbtn.Visible = false;
            //tdstock.Visible = false;
            //ddlitemstatus.Visible = false;
            //grid.Visible = false;
            trexcel.Visible = false;
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache); 

    }
    protected void ddlsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsearch.SelectedValue == "0")
        {
            Searchbox.Visible = true;
            filter.Visible = true;
            filters.Visible = false;
            if (Session["roles"].ToString() == "StoreKeeper" || Session["roles"].ToString() == "SiteIncharge")
            {
                filter.Visible = false;
            }
            else
            {
                filter.Visible = true;
            }
        }
        else if (ddlsearch.SelectedValue == "1")
        {
            Searchbox.Visible = false;
            filter.Visible = true;
            filters.Visible = true;
            if (Session["roles"].ToString() == "StoreKeeper" || Session["roles"].ToString() == "SiteIncharge")
            {
                filter.Visible = false;
               
            }
            else
            {
                filter.Visible = true;
            }
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
        fillgrid(GridView1);
    }
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid(GridView1);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
   { 
        
        try
        {
            string Condition = "";
            if (ddlsearch.SelectedValue == "1")
            {
                if (ddlCategory.SelectedItem.Text!= "Select Category")
                    Condition = Condition + "and substring(il.item_code,1,1)='" + ddlCategory.SelectedValue + "'";
                if (ddlMajorgroup.SelectedItem.Text != "Select MajorGroup")
                    Condition = Condition + "and substring(il.item_code,2,2)='" + ddlMajorgroup.SelectedValue + "'";
                if (ddlSubgroup.SelectedItem.Text != "Select Subgroup")
                    Condition = Condition + "and substring(il.item_code,4,3)='" + ddlSubgroup.SelectedValue + "'";
                if (Session["roles"].ToString() == "StoreKeeper")
                    Condition = Condition + "and cc_code='" + Session["cc_code"].ToString() + "'";
                else
                {
                    if (ddlcccode.SelectedValue != "Select All")
                        Condition = Condition + "and cc_code='" + ddlcccode.SelectedValue + "'";
                    else if (ddlcccode.SelectedValue == "Select All" && Session["roles"].ToString() == "Project Manager")
                        Condition = Condition + "and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')";

                }
                ViewState["Condition"] = Condition;
                GridView1.SelectedIndex = 1;
                printbtn.Visible = true;
                trexcel.Visible = true;
                fillgrid(GridView1);
                //fillgrid(Gv);
                FillGrid1();
            
            }
            else if (ddlsearch.SelectedValue == "0")
            {
                if (Session["roles"].ToString() == "StoreKeeper")
                {
                    Condition = Condition + "and cc_code='" + Session["cc_code"].ToString() + "'";
                  
                }
                else
                {
                    if (ddlcccode.SelectedValue != "Select All")
                        Condition = Condition + "and cc_code='" + ddlcccode.SelectedValue + "'";
                    else if (ddlcccode.SelectedValue == "Select All" && Session["roles"].ToString() == "Project Manager")
                        Condition = Condition + "and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')";
                    

                }
                if (txtSearch.Text != "")
                {
                    char c = txtSearch.Text[0];
                    if (char.IsNumber(c))
                    {
                        da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_code='" + txtSearch.Text + "' and status in ('4','5','5A','2A')", con);
                        da.Fill(ds, "checks");
                        if (ds.Tables["checks"].Rows.Count == 1)
                        {
                            Condition = Condition + " and substring(il.item_code,1,8)='" + txtSearch.Text + "'";
                            ViewState["Condition"] = Condition;
                            GridView1.SelectedIndex = 1;
                            printbtn.Visible = true;
                            trexcel.Visible = true;
                            fillgrid(GridView1);
                            //fillgrid(Gv);
                            FillGrid1();
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Invalid itemcode");
                            (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                            txtSearch.Text = String.Empty;

                        }
                    }

                    else
                    {
                        int n = txtSearch.Text.IndexOf(",");
                        if (n != -1)
                        {
                            string res = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
                            string res1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
                            da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_name='" + res + "' and specification='" + res1 + "' and status in ('4','5','5A','2A')", con);
                            da.Fill(ds, "check");
                            if (ds.Tables["check"].Rows.Count == 1)
                            {
                                string result = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
                                string result1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
                                da = new SqlDataAdapter("Select item_code from item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','5','5A','2A')", con);
                                da.Fill(ds, "search");
                                if (ds.Tables["search"].Rows.Count > 0)
                                    Condition = Condition + " and substring(il.item_code,1,8)='" + ds.Tables["search"].Rows[0].ItemArray[0].ToString() + "'";
                                ViewState["Condition"] = Condition;
                                GridView1.SelectedIndex = 1;
                                printbtn.Visible = true;
                                trexcel.Visible = true;
                                fillgrid(GridView1);
                                //fillgrid(Gv);
                                FillGrid1();
                            }

                            else
                            {
                                JavaScript.UPAlert(Page, "Invalid Specification");
                                //(UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                                txtSearch.Text = String.Empty;
                            }

                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Invalid Specification");
                            //(UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                            txtSearch.Text = String.Empty;
                        }

                    }
                }
            }
            
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewStock.aspx");

    }
    public void fillgrid(GridView gridControl)
    {
        if (ddlCategory.SelectedItem.Text == "Assets")
        {
            if (ddlcccode.SelectedValue != "CC-33")
                da = new SqlDataAdapter("Select UPPER(il.item_code) as[item_code],i.item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,i.units,il.quantity,Round((basic_price*il.quantity),2)[Totalvalue],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity,cc_code,status from master_data)il on i.item_code=substring(il.Item_code,1,8) where il.item_code is not null " + ViewState["Condition"] + " And il.Quantity>0 and il.status='Active' order by il.item_code desc", con);
            else if (ddlcccode.SelectedValue == "CC-33" && ddlitemstatus.SelectedItem.Text == "New Stock")
                da = new SqlDataAdapter("Select UPPER(il.item_code) as[item_code],i.item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,i.units,il.quantity,Round((basic_price*il.quantity),2)[Totalvalue],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity,cc_code from [new items])il on i.item_code=il.item_code where il.item_code is not null " + ViewState["Condition"] + " And il.Quantity>0 order by il.item_code desc", con);
            else if (ddlcccode.SelectedValue == "CC-33" && ddlitemstatus.SelectedItem.Text == "Stock")
                da = new SqlDataAdapter("Select UPPER(il.item_code) as[item_code],i.item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,i.units,il.quantity,Round((basic_price*il.quantity),2)[Totalvalue],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity,cc_code,status from master_data)il on i.item_code=substring(il.Item_code,1,8) where il.item_code is not null " + ViewState["Condition"] + " And il.Quantity>0 and il.status='Active' order by il.item_code desc", con);

            else if (ddlcccode.SelectedValue == "CC-33")
                da = new SqlDataAdapter("Select UPPER(il.item_code) as[item_code],i.item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,i.units,il.quantity,Round((basic_price*il.quantity),2)[Totalvalue],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity,cc_code from master_data union all Select item_code,quantity,cc_code from [new items])il on i.item_code=il.item_code where il.item_code is not null " + ViewState["Condition"] + " And il.Quantity>0 order by il.item_code desc", con);
        }
        else
        {
            if (ddlcccode.SelectedValue != "CC-33")
                da = new SqlDataAdapter("Select UPPER(il.item_code) as[item_code],i.item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,i.units,il.quantity,Round((basic_price*il.quantity),2)[Totalvalue],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity,cc_code,status from master_data)il on i.item_code=substring(il.Item_code,1,8) where il.item_code is not null " + ViewState["Condition"] + " And il.Quantity>0  order by il.item_code desc", con);
            else if (ddlcccode.SelectedValue == "CC-33" && ddlitemstatus.SelectedItem.Text == "New Stock")
                da = new SqlDataAdapter("Select UPPER(il.item_code) as[item_code],i.item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,i.units,il.quantity,Round((basic_price*il.quantity),2)[Totalvalue],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity,cc_code from [new items])il on i.item_code=il.item_code where il.item_code is not null " + ViewState["Condition"] + " And il.Quantity>0 order by il.item_code desc", con);
            else if (ddlcccode.SelectedValue == "CC-33" && ddlitemstatus.SelectedItem.Text == "Stock")
                da = new SqlDataAdapter("Select UPPER(il.item_code) as[item_code],i.item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,i.units,il.quantity,Round((basic_price*il.quantity),2)[Totalvalue],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity,cc_code,status from master_data)il on i.item_code=substring(il.Item_code,1,8) where il.item_code is not null " + ViewState["Condition"] + " And il.Quantity>0  order by il.item_code desc", con);

            else if (ddlcccode.SelectedValue == "CC-33")
                da = new SqlDataAdapter("Select UPPER(il.item_code) as[item_code],i.item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,i.units,il.quantity,Round((basic_price*il.quantity),2)[Totalvalue],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity,cc_code from master_data union all Select item_code,quantity,cc_code from [new items])il on i.item_code=il.item_code where il.item_code is not null " + ViewState["Condition"] + " And il.Quantity>0 order by il.item_code desc", con);

        
        }

        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count <= 0)
        {
            printbtn.Visible = false;
            trexcel.Visible = false;
        }
            //GridView1.DataSource = ds.Tables["data"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            //GridView1.DataBind();
            gridControl.DataSource = ds.Tables["data"];
            gridControl.DataBind();
            
    }
    
    

    protected void ddlSubgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        
       
    }
    protected void ddlMajorgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select distinct substring(item_code,4,3) as[item_code] from item_codes where substring(item_code,1,1)='" + ddlCategory.SelectedValue + "' and substring(item_code,2,2)='" + ddlMajorgroup.SelectedValue + "'", con);
        da.Fill(ds, "filldrop");
        ddlSubgroup.DataTextField = "item_code";
        ddlSubgroup.DataValueField = "item_code";
        ddlSubgroup.DataSource = ds.Tables["filldrop"];
        ddlSubgroup.DataBind();
        ddlSubgroup.Items.Insert(0, "Select Subgroup");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
           
            Image img = (Image)e.Row.FindControl("Image1");
            //string itemcode = GridView1.DataKeys[e.Row.DataItemIndex].Value.ToString();
            Label lbl = (Label)e.Row.FindControl("lblitemcode");
            if (ddlcccode.SelectedValue == "CC-33" && ddlitemstatus.SelectedItem.Text == "Select")
            {
                da = new SqlDataAdapter("Select * from [New Items] where item_code='" + lbl.Text + "' and quantity!='0'", con);
                da.Fill(ds1, "ImageEnable");
                if (ds1.Tables["ImageEnable"].Rows.Count > 0)
                    img.Visible = true;
                else
                    img.Visible = false;
            }
            else
                img.Visible = false;


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //TotalAmount();
            e.Row.Cells[8].Text = Convert.ToDecimal(ds.Tables["data"].Compute("sum(Totalvalue)", string.Empty)).ToString();

        }
           
      
    }

    public void TotalAmount()
    {

        try {
            if (ddlCategory.SelectedItem.Text == "Assets")
            {
                if (ddlitemstatus.SelectedItem.Text == "Stock" && ddlcccode.SelectedValue == "CC-33")
                    da = new SqlDataAdapter("select sum(isnull(il.quantity,0))[Quantity],sum(basic_price*il.quantity)[Total] from Item_codes i join Master_data il on i.Item_code=substring(il.Item_code,1,8) where  il.Item_code  is not null  " + ViewState["Condition"] + " and il.status='Active'", con);
                else if (ddlitemstatus.SelectedItem.Text == "New Stock" && ddlcccode.SelectedValue == "CC-33")
                    da = new SqlDataAdapter("select sum(isnull(il.quantity,0))[Quantity],sum(basic_price*il.quantity)[Total] from Item_codes i join [new items] il on i.Item_code=il.Item_code where  il.Item_code  is not null  " + ViewState["Condition"] + "", con);
                else
                {
                    da = new SqlDataAdapter("select sum(isnull(il.quantity,0))[Quantity],sum(basic_price*il.quantity)[Total] from Item_codes i join Master_data il on i.Item_code=substring(il.Item_code,1,8) where  il.Item_code  is not null  " + ViewState["Condition"] + " and il.status='Active'", con);

                }
            }
            else
            {
                if (ddlitemstatus.SelectedItem.Text == "Stock" && ddlcccode.SelectedValue == "CC-33")
                    da = new SqlDataAdapter("select sum(isnull(il.quantity,0))[Quantity],sum(basic_price*il.quantity)[Total] from Item_codes i join Master_data il on i.Item_code=substring(il.Item_code,1,8) where  il.Item_code  is not null  " + ViewState["Condition"] + "", con);
                else if (ddlitemstatus.SelectedItem.Text == "New Stock" && ddlcccode.SelectedValue == "CC-33")
                    da = new SqlDataAdapter("select sum(isnull(il.quantity,0))[Quantity],sum(basic_price*il.quantity)[Total] from Item_codes i join [new items] il on i.Item_code=il.Item_code where  il.Item_code  is not null  " + ViewState["Condition"] + "", con);
                else
                {
                    da = new SqlDataAdapter("select sum(isnull(il.quantity,0))[Quantity],sum(basic_price*il.quantity)[Total] from Item_codes i join Master_data il on i.Item_code=substring(il.Item_code,1,8) where  il.Item_code  is not null  " + ViewState["Condition"] + "", con);

                }

            }
            da.Fill(ds, "Total");
            if (ds.Tables["Total"].Rows.Count > 0)
            {
                 //qty = ds.Tables["Total"].Rows[0].ItemArray[0].ToString();
                 amount = Convert.ToDecimal(ds.Tables["Total"].Rows[0].ItemArray[1].ToString());
            }
         
        
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void FillGrid1()
    {
        gridprint.DataSource = ds.Tables["data"];
        gridprint.DataBind();
        lblcode.Text = ddlcccode.SelectedValue;
        //lbldate.Text = DateTime.Now.ToString("d/MM/yyyy");
    }
  
    protected void btnprint_Click(object sender, EventArgs e)
    {
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        tblpo.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    //public override void VerifyRenderingInServerForm(Control CC)
    //{
    //    /*Verifies that the control is rendered */
    //    return;
    //}
   
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Gv.AllowPaging = false;
        fillgrid(Gv);
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Stock Report"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        Gv.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
  
}
