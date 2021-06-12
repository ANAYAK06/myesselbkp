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
using System.Drawing;


public partial class verifyitemcode : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            btnupd.Attributes.Add("onclick", "return validate();");
            GridView2.Visible = true;
            tritemcode.Visible = false;
            tramend.Visible = false;
            fillunits();
            fillgrid();
            Category();
            if (Session["roles"].ToString() == "SuperAdmin")
            {
                tramend.Visible = true;
                fillgrid2();

            }

            btnupd.Visible = false;
            upditem.Visible = true;
            newitem.Visible = true;
        }
    }         
         
    protected void GridView2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int index = GridView2.SelectedIndex;
        tritemcode.Visible = true;
        ViewState["id"] = GridView2.SelectedValue.ToString();        
        string Description = GridView2.DataKeys[GridView2.SelectedIndex].Values["transaction_no"].ToString();
        lowestAmt(Description);
        popitems.Show();
        filltable(GridView2.SelectedValue.ToString(),Description);
        btnupd.Visible = true;       

    }
    public void lowestAmt(string Amt)
    {
        da = new SqlDataAdapter("select top 1 id from ItemCode_Suppliers where Transaction_No='" + Amt + "' order by Rate asc", con);
        da.Fill(ds, "lowest");
        ViewState["lowest"] = ds.Tables["lowest"].Rows[0].ItemArray[0].ToString();
    }
    protected void GVSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            string myDataKey = rowView["id"].ToString();
            if (myDataKey == ViewState["lowest"].ToString())
            {
                e.Row.Cells[2].BackColor = Color.Green;
                //e.Row.Cells[3].fontc = Color.Green;
            }
            else
            {
                e.Row.Cells[2].BackColor = Color.Red;
            }
        }

    }
    public void Category()
    {
        da = new SqlDataAdapter("Select distinct category_name,category_code,category_code+'  ,'+category_name Name  from itemcode_creation", con);
        da.Fill(ds, "category");
        lstcategory.DataTextField = "Name";
        lstcategory.DataValueField = "Name";
        lstcategory.DataSource = ds.Tables["category"];
        lstcategory.DataBind();
        ddldca.SelectedIndex = 0;
        ddlSub.SelectedIndex = 0;     
    }
    protected void lstcategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (lstcategory.SelectedValue != "")
        {
            da = new SqlDataAdapter("Select category_name from itemcode_creation where category_code='" + lstcategory.SelectedItem.Text.Split(',')[0].Replace(" ", string.Empty) + "' and category_name ='" + lstcategory.SelectedItem.Text.Split(',')[1] + "'", con);

            da.Fill(ds, "category");
            if (ds.Tables["category"].Rows.Count > 0)
            {
                txtcode.Text = lstcategory.SelectedValue;
                txtcname.Text = ds.Tables["category"].Rows[0].ItemArray[0].ToString();
                txtcname.Enabled = false;
                txtgcode.Text = "";
                txtgname.Text = "";
                txtsubgroupcode.Text = "";
            }
            else
                txtcname.Enabled = true;

            if (lstcategory.SelectedItem.Text.Split(',')[0].Replace(" ", string.Empty) == "1")
            {
                ddldca.SelectedValue = "DCA-27";
                ddldca.Enabled = false;
                fillSubdca();
            }
            else if (lstcategory.SelectedItem.Text.Split(',')[0].Replace(" ", string.Empty) == "2" || lstcategory.SelectedItem.Text.Split(',')[0].Replace(" ", string.Empty) == "3")
            {
                ddldca.SelectedValue = "DCA-11";
                fillSubdca();
                ddldca.Enabled = false;
            }
            else if (lstcategory.SelectedItem.Text.Split(',')[0].Replace(" ", string.Empty) == "4")
            {
                ddldca.SelectedValue = "DCA-24";
                fillSubdca();
                ddldca.Enabled = false;
            } 
        }
        majorgroup();

    }
    public void majorgroup()
    {
        lstgccode.ClearSelection();
        txtgcode.Text = "";
        txtgname.Text = "";
        da = new SqlDataAdapter("Select majorgroup_code+' ('+majorgroup_name+')' as majorgroupname,majorgroup_code from itemcode_creation where category_code='" + txtcode.Text.Split(',')[0] + "' and category_name='" + txtcode.Text.Split(',')[1] + "' ", con);  //category_code='" + txtcode.Text + "'
        da.Fill(ds, "majorgroup");
        lstgccode.DataTextField = "majorgroupname";
        lstgccode.DataValueField = "majorgroup_code";
        lstgccode.DataSource = ds.Tables["majorgroup"];
        lstgccode.DataBind();
    }
    protected void ddldca_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSubdca();
        ddldca.Enabled = true;

    }
    public void fillSubdca()
    {
        da = new SqlDataAdapter("Select subdca_code,subdca_name+' ('+subdca_code+')' Name,subdca_name from subdca where dca_code='" + ddldca.SelectedValue + "' order by subdca_code", con);
        da.Fill(ds, "sub");
        ddlSub.DataSource = ds.Tables["sub"];
        ddlSub.DataTextField = "Name";
        ddlSub.DataValueField = "Subdca_code";
        ddlSub.DataBind();
        ddlSub.Items.Insert(0, "Select Sub DCA");
    }
    protected void lstgccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        txtgcode.Text = lstgccode.SelectedValue;
        if (lstgccode.SelectedValue != "")
        {
            da = new SqlDataAdapter("Select majorgroup_name from itemcode_creation where majorgroup_code='" + lstgccode.SelectedValue + "'", con);

            da.Fill(ds, "gccode");
            if (ds.Tables["gccode"].Rows.Count > 0)
            {
                txtgname.Text = ds.Tables["gccode"].Rows[0].ItemArray[0].ToString();
            }
        }
        subgroup();

    }
    public void subgroup()
    {
        if (txtgcode.Text != "")
        {
            da = new SqlDataAdapter("select distinct substring(item_code,4,3) as[item_code] from item_codes where substring(item_code,2,2)='" + lstgccode.SelectedValue + "'", con);
            da.Fill(ds, "subgroup");
            lstsubgroup.DataTextField = "item_code";
            lstsubgroup.DataValueField = "item_code";
            lstsubgroup.DataSource = ds.Tables["subgroup"];
            lstsubgroup.DataBind();
        }

    }
    public void filldca()
    {

        da = new SqlDataAdapter("Select dca_code,dca_name+' ('+dca_code+')' Name,dca_name from dca where dca_code in ('DCA-11','DCA-27','DCA-24')order by dca_code", con);
        //da = new SqlDataAdapter("Select dca_code,dca_name+' ('+dca_code+')' Name,dca_name from dca order by dca_code", con);
        da.Fill(ds, "dca");
        ddldca.DataSource = ds.Tables["dca"];
        ddldca.DataTextField = "Name";
        ddldca.DataValueField = "dca_code";
        ddldca.DataBind();
        ddldca.Items.Insert(0, "Select DCA");
    }
    protected void lstsubgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstsubgroup.SelectedValue != "")
        {
            txtsubgroupcode.Text = lstsubgroup.SelectedItem.Text;
            specification();
        }
    }
    public void fillunits()
    {
        da = new SqlDataAdapter("select distinct units from item_codes", con);
        DataSet ds1 = new DataSet();
        da.Fill(ds1, "units");
        lstunits.DataTextField = "units";
        lstunits.DataValueField = "units";
        lstunits.DataSource = ds1.Tables["units"];
        lstunits.DataBind();
    }
    protected void lstunits_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstunits.SelectedValue != "")
            txtunits.Text = lstunits.SelectedItem.Text;
    }  
    public void specification()
    {
        if (txtsubgroupcode.Text != "")
        {            
            da = new SqlDataAdapter("select TOP 1 specification_code,specification from item_codes where substring(item_code,2,2)='" + txtgcode.Text + "' and substring(item_code,4,3)='" + lstsubgroup.SelectedItem.Text + "' ORDER BY id desc", con);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "specification");
            txtspecifcationname.Text = ds1.Tables["specification"].Rows[0].ItemArray[1].ToString();
            if (Convert.ToDecimal(ds1.Tables["specification"].Rows[0].ItemArray[0].ToString()) >= 9)
                txtspecification.Text = (Convert.ToDecimal(ds1.Tables["specification"].Rows[0].ItemArray[0].ToString()) + 1).ToString();
            else
                txtspecification.Text = '0' + (Convert.ToDecimal(ds1.Tables["specification"].Rows[0].ItemArray[0].ToString()) + 1).ToString();
            txtspecification.Enabled = false;
            ds1.Reset();
        }
    }
     public void filltable(string id,string tranno)
    {
        try
        {
            da = new SqlDataAdapter("SELECT status FROM item_codes where id='" + ViewState["id"] + "'", con);
            da.Fill(ds, "status");
            string hfstatus = ds.Tables["status"].Rows[0][0].ToString();
            if (hfstatus == "1A")
                da = new SqlDataAdapter("Select replace(basic_price,'.0000','.00')as basic_price,specification_code,specification,units,item_name,substring(item_code,1,1)as categorycode,(SELECT distinct ic.category_name FROM itemcode_creation ic join item_codes i on category_code=SUBSTRING(i.item_code,1,1) and Majorgroup_Code=SUBSTRING(i.item_code,2,2) where i.id='" + ViewState["id"] + "')as categoryname, substring(item_code,2,2)as majorgroupcode,(SELECT TOP 1 ic.majorgroup_name FROM itemcode_creation ic join item_codes i on majorgroup_code=substring(item_code,2,2) where i.id='" + ViewState["id"] + "')as majorgroup_name,dca_code,subdca_code,SUBSTRING(item_code,4,3)as subgroupcode,hsncode from item_codes where id='" + id + "'", con);
            else          
               da = new SqlDataAdapter("SELECT replace(i.basic_price,'.0000','.00')as basic_price,specification_code,specification,units,item_name,SUBSTRING(i.item_code,1,1) as category_code,category_name,SUBSTRING(i.item_code,2,2) as majorgroup_code,majorgroup_name,dca_code,subdca_code,SUBSTRING(i.item_code,4,3) as subgroup_code,hsncode FROM item_codes i where status not IN ('4','5','5A') AND i.id='" + ViewState["id"] + "'", con);
            da.Fill(ds, "iteminfo");
            if (ds.Tables["iteminfo"].Rows.Count > 0)
            {
                txtcode.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[5].ToString();
                txtcname.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[6].ToString();
                txtgcode.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[7].ToString();
                txtgname.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[8].ToString();
                txtspecification.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[1].ToString();
                txtspecifcationname.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[2].ToString();
                txtitemname.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[4].ToString();
                txtunits.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[3].ToString();
                txtbasicprice.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[0].ToString();
                //gethsncode();
                da = new SqlDataAdapter("select HSN_SAC_Code as code,Remarks from HSN_SAC_Codes where HSNCategory ='" + ds.Tables["iteminfo"].Rows[0].ItemArray[6].ToString() + "' and codetype='HSN' and status='Approved'", con);
                da.Fill(ds, "hsn1");
                ddlhsncode.DataSource = ds.Tables["hsn1"];
                ddlhsncode.DataTextField = "code";
                ddlhsncode.DataValueField = "code";
                ddlhsncode.DataBind();
                ddlhsncode.Items.Insert(0, "Select");
                if (ds.Tables["iteminfo"].Rows[0].ItemArray[12].ToString() != "")
                {
                    ddlhsncode.SelectedValue = ds.Tables["iteminfo"].Rows[0].ItemArray[12].ToString();
                    if (ddlhsncode.SelectedValue != "Select")
                    {
                        da = new SqlDataAdapter("select Remarks from HSN_SAC_Codes where HSN_SAC_Code ='" + ds.Tables["iteminfo"].Rows[0].ItemArray[12].ToString() + "' and HSNCategory ='" + ds.Tables["iteminfo"].Rows[0].ItemArray[6].ToString() + "' and codetype='HSN' and status='Approved'", con);
                        da.Fill(ds, "Remarks");
                        txthsnremarks.Text = ds.Tables["Remarks"].Rows[0].ItemArray[0].ToString();
                    }
                }
                else
                {
                    ddlhsncode.SelectedIndex = 0;
                    txthsnremarks.Text = "";
                }

                da = new SqlDataAdapter("Select dca_code,dca_name+' ('+dca_code+')' Name,dca_name from dca where dca_code='" + ds.Tables["iteminfo"].Rows[0].ItemArray[9].ToString() + "'order by dca_code", con);
                da.Fill(ds, "dca");
                ddldca.DataSource = ds.Tables["dca"];
                ddldca.DataTextField = "Name";
                ddldca.DataValueField = "dca_code";
                ddldca.DataBind();
                ddldca.Items.Insert(0, "Select DCA");
                ddldca.SelectedItem.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[9].ToString();

                da = new SqlDataAdapter("Select subdca_code,subdca_name+' ('+subdca_code+')' Name from subdca where dca_code='" + ddldca.SelectedValue + "' order by subdca_code", con);
                da.Fill(ds, "sub");
                ddlSub.DataTextField = "Name";
                ddlSub.DataValueField = "Subdca_code";
                ddlSub.DataSource = ds.Tables["sub"];
                ddlSub.DataBind();
                ddlSub.Items.Insert(0, "Select Sub DCA");
                ddlSub.SelectedItem.Text = ds.Tables["iteminfo"].Rows[0][10].ToString();               
                txtsubgroupcode.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[11].ToString();
                if (tranno != "")
                {
                    da = new SqlDataAdapter("Select * from ItemCode_Suppliers where transaction_no='" + tranno + "' order by id asc", con);
                    da.Fill(ds, "sup");
                    GVSupplier.DataSource = ds.Tables["sup"];
                    GVSupplier.DataBind();

                    da = new SqlDataAdapter("Select * from ItemCode_Remarks where transaction_no='" + tranno + "' order by id asc", con);
                    da.Fill(ds, "Rem");                   
                    GVUsers.DataSource = ds.Tables["Rem"];
                    GVUsers.DataBind();
                }
            }
        }
          catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
     }
    
    protected void ddlhsncode_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select Remarks  from HSN_SAC_Codes where HSN_SAC_Code ='" + ddlhsncode.SelectedValue + "' ", con);
        DataSet ds1 = new DataSet();
        da.Fill(ds1, "hsnname");
        if (ds1.Tables["hsnname"].Rows.Count > 0)
        {
            txthsnremarks.Text = ds1.Tables["hsnname"].Rows[0].ItemArray[0].ToString();
        }
        else
        {
            txthsnremarks.Text = "";
        }
    }
    public int j = 0;
     protected void GVUsers_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             da = new SqlDataAdapter("select (first_name+'  '+middle_name+'  '+last_name)as Name from Employee_Data where USER_NAME='" + GVUsers.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'", con);
             da.Fill(ds, "Name");
             if (ds.Tables["Name"].Rows.Count > 0)
             {
                 e.Row.Cells[2].Text = ds.Tables["Name"].Rows[j].ItemArray[0].ToString();
             }
             j = j + 1;
         }
        
     }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {       
        try
        {
            cmd.Connection = con;
            cmd = new SqlCommand("sp_deleteitemcode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", GridView2.DataKeys[e.RowIndex]["id"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfull")
                JavaScript.UPAlert(Page, msg);
            else
                JavaScript.UPAlert(Page, msg);
            con.Close();

           fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
   
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        fillgrid2();
    }
    protected void Gridview2_Rowediting(object sender, GridViewEditEventArgs e)
    {       
       
    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
            {                
                //da = new SqlDataAdapter("select  id,item_name,Item_code,Basic_Price,specification_code,specification,substring(item_code,2,2)as majorgroup_code,majorgroup_name,dca_code,subdca_code from Item_codes where status='1'", con);
                da = new SqlDataAdapter("SELECT i.id,i.item_code,i.basic_price,specification_code,specification,units,item_name,SUBSTRING(i.item_code,1,1) as category_code,category_name,SUBSTRING(i.item_code,2,2) as majorgroup_code,majorgroup_name,(SELECT dca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as dca_code,(SELECT subdca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as subdca_code,SUBSTRING(i.item_code,4,3) as subgroup_code,transaction_no FROM item_codes i where status='1' ORDER BY i.id desc", con);
                da.Fill(ds, "FillIn");
                GridView2.DataSource = ds.Tables["FillIn"];
                GridView2.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                GridView2.DataBind();
            }
            if (Session["roles"].ToString() == "PurchaseManager")
            {
                //da = new SqlDataAdapter("select  id,item_name,Item_code,Basic_Price,specification_code,specification,substring(item_code,2,2)as majorgroup_code,majorgroup_name,dca_code,subdca_code from Item_codes where status='1'", con);
                da = new SqlDataAdapter("SELECT i.id,i.item_code,i.basic_price,specification_code,specification,units,item_name,SUBSTRING(i.item_code,1,1) as category_code,category_name,SUBSTRING(i.item_code,2,2) as majorgroup_code,majorgroup_name,(SELECT dca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as dca_code,(SELECT subdca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as subdca_code,SUBSTRING(i.item_code,4,3) as subgroup_code,transaction_no FROM item_codes i where status in('A1','A2') ORDER BY i.id desc", con);
                da.Fill(ds, "FillIn");
                GridView2.DataSource = ds.Tables["FillIn"];
                GridView2.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                GridView2.DataBind();
            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
                da = new SqlDataAdapter("SELECT i.id,i.item_code,i.basic_price,specification_code,specification,units,item_name,SUBSTRING(i.item_code,1,1) as category_code,category_name,SUBSTRING(i.item_code,2,2) as majorgroup_code,majorgroup_name,(SELECT dca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as dca_code,(SELECT subdca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as subdca_code,SUBSTRING(i.item_code,4,3) as subgroup_code,transaction_no FROM item_codes i where status in ('2','1')  ORDER BY i.id desc", con);
                da.Fill(ds, "FillIn");
                GridView2.DataSource = ds.Tables["FillIn"];
                GridView2.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                GridView2.DataBind();
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                da = new SqlDataAdapter("SELECT i.id,i.item_code,i.basic_price,specification_code,specification,units,item_name,SUBSTRING(i.item_code,1,1) as category_code,category_name,SUBSTRING(i.item_code,2,2) as majorgroup_code,majorgroup_name,(SELECT dca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as dca_code,(SELECT subdca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as subdca_code,SUBSTRING(i.item_code,4,3) as subgroup_code,transaction_no FROM item_codes i where status='3' ORDER BY i.id desc", con);
                da.Fill(ds, "FillIn");
                GridView2.DataSource = ds.Tables["FillIn"];
                GridView2.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                GridView2.DataBind();
            }           
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void GridView1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        tritemcode.Visible = true;
        ViewState["id"] = GridView1.SelectedValue.ToString();
        popitems.Show();
        filltable(GridView1.SelectedValue.ToString(),"");
        btnupd.Visible = true;
    }
    public void fillgrid2()
    {
        try
        {
            //da = new SqlDataAdapter("select  id,item_name,Item_code,Basic_Price,specification_code,specification,substring(item_code,2,2)as majorgroup_code,dca_code,subdca_code from Item_codes  where status='1A'", con);
            da = new SqlDataAdapter("SELECT i.id,i.item_code,i.basic_price,specification_code,specification,units,item_name,SUBSTRING(i.item_code,1,1) as category_code,(SELECT distinct category_name FROM itemcode_creation where category_code=SUBSTRING(i.item_code,1,1) and Majorgroup_Code=SUBSTRING(i.item_code,2,2)) as category_name,SUBSTRING(i.item_code,2,2) as majorgroup_code,(SELECT majorgroup_name FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as majorgroup_name,(SELECT dca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as dca_code,(SELECT subdca_code FROM itemcode_creation where majorgroup_code=SUBSTRING(i.item_code,2,2) and category_code=SUBSTRING(i.item_code,1,1)) as subdca_code,SUBSTRING(i.item_code,4,3) as subgroup_code FROM item_codes i where status='1A' ORDER BY i.id desc", con);
            da.Fill(ds, "FillIn1");
            GridView1.DataSource = ds.Tables["FillIn1"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
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
        lblPages.Text = GridView1.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = GridView1.PageIndex + 1;
        lblCurrent.Text = currentPage.ToString();

        //-- For First and Previous ImageButton
        if (GridView1.PageIndex == 0)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst2")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst2")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev2")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev2")).Visible = false;


        }

        //-- For Last and Next ImageButton
        if (GridView1.PageIndex + 1 == GridView1.PageCount)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast2")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast2")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext2")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext2")).Visible = false;


        }
    }
    protected void btnFirst2_Command(object sender, CommandEventArgs e)
    {
        Grid1Paginate(sender, e);
    }
    protected void btnPrev2_Command(object sender, CommandEventArgs e)
    {
        Grid1Paginate(sender, e);
    }
    protected void btnNext2_Command(object sender, CommandEventArgs e)
    {

        Grid1Paginate(sender, e);
    }
    protected void btnLast2_Command(object sender, CommandEventArgs e)
    {
        Grid1Paginate(sender, e);
    }
    protected void Grid1Paginate(object sender, CommandEventArgs e)
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

        fillgrid2();


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid2();
    }
    protected void Gridview1_Rowediting(object sender, GridViewEditEventArgs e)
    {
      
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string itemcode = txtcode.Text + txtgcode.Text + txtsubgroupcode.Text + txtspecification.Text;
        try
        {           
            da = new SqlDataAdapter("ItemCodeCreation_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;           
            da.SelectCommand.Parameters.AddWithValue("@ItemCode", SqlDbType.VarChar).Value = itemcode;
            da.SelectCommand.Parameters.AddWithValue("@CategoryCode", SqlDbType.VarChar).Value = txtcode.Text;
            da.SelectCommand.Parameters.AddWithValue("@CategoryName", SqlDbType.VarChar).Value = txtcname.Text;
            da.SelectCommand.Parameters.AddWithValue("@MajorgroupCode", SqlDbType.VarChar).Value = txtgcode.Text;
            da.SelectCommand.Parameters.AddWithValue("@MajorgroupName", SqlDbType.VarChar).Value = txtgname.Text;
            da.SelectCommand.Parameters.AddWithValue("@DCACode", SqlDbType.VarChar).Value = ddldca.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@ItemName", SqlDbType.VarChar).Value = txtitemname.Text;
            da.SelectCommand.Parameters.AddWithValue("@SpecificationName", SqlDbType.VarChar).Value = txtspecifcationname.Text;
            da.SelectCommand.Parameters.AddWithValue("@SubDca", SqlDbType.VarChar).Value = ddlSub.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@units", SqlDbType.VarChar).Value = txtunits.Text;
            da.SelectCommand.Parameters.AddWithValue("@basicprice", SqlDbType.Money).Value = txtbasicprice.Text;
            da.SelectCommand.Parameters.AddWithValue("@SubgroupCode", SqlDbType.VarChar).Value = txtsubgroupcode.Text;
            da.SelectCommand.Parameters.AddWithValue("@SpecificationCode", SqlDbType.VarChar).Value = txtspecification.Text;
            da.SelectCommand.Parameters.AddWithValue("@role", SqlDbType.VarChar).Value = Session["roles"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = ViewState["id"];
            da.SelectCommand.Parameters.AddWithValue("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Remark", SqlDbType.VarChar).Value = txtremarks.Text;
            da.SelectCommand.Parameters.AddWithValue("@HSNCode", SqlDbType.VarChar).Value = ddlhsncode.SelectedValue;            
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "Update";
            da.Fill(ds, "approval");            
            if (ds.Tables["approval"].Rows[0].ItemArray[0].ToString() == "Item code succefully Generated")
            {
                if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller")
                    JavaScript.UPAlertRedirect(Page, "Item code succefully Verified with" + " " + ds.Tables["approval"].Rows[0].ItemArray[1].ToString() + " " + ds.Tables["approval"].Rows[0].ItemArray[2].ToString() + " " + ds.Tables["approval"].Rows[0].ItemArray[3].ToString(), "verifyitemcode.aspx");
                if (Session["roles"].ToString() == "SuperAdmin")
                    JavaScript.UPAlertRedirect(Page, "Item code succefully Approved with" + " " + ds.Tables["approval"].Rows[0].ItemArray[1].ToString() + " " + ds.Tables["approval"].Rows[0].ItemArray[2].ToString() + " " + ds.Tables["approval"].Rows[0].ItemArray[3].ToString(), "verifyitemcode.aspx");
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, ds.Tables["approval"].Rows[0].ItemArray[0].ToString(),"verifyitemcode.aspx");
            }
            con.Close();
            fillgrid();

            if (Session["roles"].ToString() == "Chief Material Controller")
           
                fillgrid2();
           }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
}

