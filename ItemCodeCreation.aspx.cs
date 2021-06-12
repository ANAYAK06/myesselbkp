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
using AjaxControlToolkit;
using System.Data;
using System.Collections.Generic;


public partial class ItemCodeCreation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
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
        if (Session["roles"].ToString() == "Central Store Keeper")
            ddlCC.Visible = true;
        else if (Session["roles"].ToString() == "Chief Material Controller" ||  Session["roles"].ToString() == "SuperAdmin") 
        {
            btnadd.Visible = false;
            Btnaddindnt.Enabled = false;
            ddlCC.Visible = false;
        }
        else if (Session["roles"].ToString() == "StoreKeeper" || Session["roles"].ToString() == "Chief Material Controller")
        {
            Btnaddnew.Visible = false;
        }
        else ddlCC.Visible = false;     
        if (!IsPostBack)
        {
            trdesc.Visible = false;
            trdlbl.Visible = false;
            checkapprovals();
            filldca();
            fillSubdca();
            fillunits();
            Category();
            //majorgroup();
            Btnsbmt.Visible = false;
            Btnrmv.Visible = false;
            clear();
            //subgroup();
            //specification();
           // fillitemcodes();
            lstgccode.Attributes.Add("onclick", "setText(this.options[this.selectedIndex].value);");
            //lstcategory.Attributes.Add("onclick", "setText2(this.options[this.selectedIndex].value);");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "2", "<script type='text/javascript'> validate();</script>");
            //btnSave.Attributes.Add("onclick", "return validate();");
            fillgrid();
            //gvDetails.RowDataBound += new GridViewRowEventHandler(gvDetails_RowDataBound);
            //newitem.Visible = false;

        }
    }

    [WebMethod]
    public static string IsGroupnameAvailable(string majorgroupname,string categorycode)
    {
      
        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter("Select majorgroup_name from itemcode_creation where majorgroup_code='" + majorgroupname+ "' and category_code='"+categorycode+"'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "checkname");
        conWB.Open();
        if (dsWB.Tables["checkname"].Rows.Count > 0)
        {
            string Groupcodename = dsWB.Tables[0].Rows[0].ItemArray[0].ToString();
            return Groupcodename;
        }
             
        else
        {
            string groupname = majorgroupname + " is not available";

            return groupname;

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
    public void filldca()
    {
        //da = new SqlDataAdapter("Select dca_code,dca_name+' ('+dca_code+')' Name,dca_name from dca order by dca_code", con);
        da = new SqlDataAdapter("Select dca_code,dca_name+' ('+dca_code+')' Name,dca_name from dca where dca_code in ('DCA-11','DCA-27','DCA-24')order by dca_code", con);
        da.Fill(ds, "dca");
        ddldca.DataSource = ds.Tables["dca"];
        ddldca.DataTextField = "Name";
        ddldca.DataValueField = "dca_code";
        ddldca.DataBind();
        ddldca.Items.Insert(0, "Select DCA");
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
    protected void ddldca_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSubdca();
        ddldca.Enabled = true;

    }
    public void showalert(string message)
    {
        Label mylable = new Label();
        Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "1", "Java_Script/ccMessageBox.js");
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "2", "<script type='text/javascript'>ccMessageBox.show('Kishore', '" + message + "', 'Info', 400, 100,null);</script>");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "2", "<script type='text/javascript'>jalert('Message Box','" + message + "','Info', '400', '100','null');</script>");
    }


    public void majorgroup()
    {
        lstgccode.ClearSelection();
        txtgcode.Text = "";
        txtgname.Text = "";
        da = new SqlDataAdapter("Select majorgroup_code+' ('+majorgroup_name+')' as majorgroupname,majorgroup_code from itemcode_creation where category_code='" + txtcategory.Text.Split(',')[0] + "' and category_name='" + txtcategory.Text.Split(',')[1] + "'", con);
        da.Fill(ds, "majorgroup");
        lstgccode.DataTextField = "majorgroupname";
        lstgccode.DataValueField = "majorgroup_code";
        lstgccode.DataSource = ds.Tables["majorgroup"];
        lstgccode.DataBind();
    }
    public void subgroup()
    {
        if (txtgcode.Text != "")
        {
            da = new SqlDataAdapter("select distinct substring(item_code,4,3) as[item_code] from item_codes where substring(item_code,2,2)='" + lstgccode.SelectedValue + "' and substring(item_code,1,1)='" + txtcategory.Text + "'", con);
            da.Fill(ds, "subgroup");
            lstsubgroup.DataTextField = "item_code";
            lstsubgroup.DataValueField = "item_code";
            lstsubgroup.DataSource = ds.Tables["subgroup"];
            lstsubgroup.DataBind();
        }
       
    }
    public void specification()
    {
        if (txtsubgroupcode.Text != "")
        {
            //da = new SqlDataAdapter("select specification_code,specification from item_codes where substring(item_code,2,2)='" + txtgcode.Text + "' and substring(item_code,4,3)='" + lstsubgroup.SelectedItem.Text + "'", con);
            //DataSet ds1 = new DataSet();
            //da.Fill(ds1, "specification");
            //ViewState["specification"] = ds1.Tables["specification"].Rows[0].ItemArray[1].ToString();
            //lstspecification.DataTextField = "specification_code";
            //lstspecification.DataValueField = "specification_code";
            //lstspecification.DataSource = ds1.Tables["specification"];
            //lstspecification.DataBind();
            da = new SqlDataAdapter("select TOP 1 specification_code,specification from item_codes where substring(item_code,1,1)='" + txtcategory.Text + "' and substring(item_code,2,2)='" + txtgcode.Text + "' and substring(item_code,4,3)='" + lstsubgroup.SelectedItem.Text + "' ORDER BY specification_code desc", con);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "specification");
           // txtspecifcationname.Text = ds1.Tables["specification"].Rows[0].ItemArray[1].ToString();
            if (Convert.ToDecimal(ds1.Tables["specification"].Rows[0].ItemArray[0].ToString())>=9)
                txtspecification.Text = (Convert.ToDecimal(ds1.Tables["specification"].Rows[0].ItemArray[0].ToString()) + 1).ToString();
            else
                txtspecification.Text = '0'+(Convert.ToDecimal(ds1.Tables["specification"].Rows[0].ItemArray[0].ToString())+1).ToString();
            txtspecification.Enabled = false;
            ds1.Reset();
        }       
    }

    protected void lstgccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        subgroup();
        if (lstgccode.SelectedValue != "")
        {
            da = new SqlDataAdapter("Select majorgroup_name from itemcode_creation where majorgroup_code='" + txtgcode.Text + "'", con);

            da.Fill(ds, "gccode");
            if (ds.Tables["gccode"].Rows.Count > 0)
            {
                txtgname.Text = ds.Tables["gccode"].Rows[0].ItemArray[0].ToString();
            }
        }
        txtspecifcationname.Text = "";
        txtspecification.Text = "";
        txtsubgroupcode.Text = "";     
    }
    protected void lstsubgroup_SelectedIndexChanged(object sender, EventArgs e)
    {       
        if (lstsubgroup.SelectedValue != "")
        {
            txtsubgroupcode.Text = lstsubgroup.SelectedItem.Text;
            specification();
        }
    }
    //protected void lstspecification_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (lstspecification.SelectedValue != "")
    //        txtspecification.Text = lstspecification.SelectedItem.Text;
    //    txtspecifcationname.Text = ViewState["specification"].ToString();
    //}

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
    protected void ddlitem_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    } 
  
    protected void lstcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (lstcategory.SelectedValue != "")
        {
            da = new SqlDataAdapter("Select category_name from itemcode_creation where category_code='" + lstcategory.SelectedItem.Text.Split(',')[0].Replace(" ", string.Empty) + "' and category_name ='" + lstcategory.SelectedItem.Text.Split(',')[1] + "'", con);

            da.Fill(ds, "category");
            if (ds.Tables["category"].Rows.Count > 0)
            {
                txtcategory.Text = lstcategory.SelectedValue;
                txtcname.Text = ds.Tables["category"].Rows[0].ItemArray[0].ToString();
                txtcname.Enabled = false;
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
        gethsncode(lstcategory.SelectedItem.Text.Split(',')[1].ToString());
        ViewState["HSNTYPE"] = lstcategory.SelectedItem.Text.Split(',')[1].ToString();
        majorgroup();

    }
    public void gethsncode(string type)
    {
        da = new SqlDataAdapter("select HSN_SAC_Code as code from HSN_SAC_Codes where HSNCategory ='" + type + "' and codetype='HSN' and status='Approved' ", con);
        DataSet ds1 = new DataSet();
        da.Fill(ds1, "hsn");
        if (ds1.Tables["hsn"].Rows.Count > 0)
        {
            ddlhsncode.DataTextField = "code";
            ddlhsncode.DataValueField = "code";
            ddlhsncode.DataSource = ds1.Tables["hsn"];
            ddlhsncode.DataBind();
            ddlhsncode.Items.Insert(0, "Select");
        }
        else
        {
            ddlhsncode.DataSource = null;
            ddlhsncode.DataBind();
            ddlhsncode.Items.Insert(0, "Select");
        }
    }
    protected void ddlhsncode_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select Remarks from HSN_SAC_Codes where HSN_SAC_Code ='" + ddlhsncode.SelectedValue + "' and HSNCategory ='" + ViewState["HSNTYPE"].ToString() + "' ", con);
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

        protected void btngo_Click(object sender, EventArgs e)
    {
        
        da = new SqlDataAdapter("select id,Item_code,item_name,Specification,Units from item_codes where item_name like '" + txtitemname.Text + "'", con);
        da.Fill(ds, "modalpopup");
        Grditemspopup.DataSource = ds.Tables["modalpopup"];
        Grditemspopup.DataBind();
       
       // UpdatePanel2.Update();
        popitems.Show();      
    }
    protected void btnadd_Click(object sender, EventArgs e)     
    {
        string sids = "";
        string majorcode = "";
        foreach (GridViewRow record in Grditemspopup.Rows)
        {
            CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
            if (c1.Checked)
            {
                sids = Grditemspopup.DataKeys[record.RowIndex]["id"].ToString();
                majorcode = record.Cells[2].Text.Substring(1, 2);
                da = new SqlDataAdapter("select distinct i.Specification,i.Specification_code,i.Dca_Code,i.Subdca_Code,i.Units,i.Basic_Price,ic.category_code,ic.category_name,substring(i.item_code,4,3)as subgroup_code,ic.Majorgroup_Code,ic.Majorgroup_Name,i.item_code from item_codes i join itemcode_creation ic on SUBSTRING(i.Item_code,1,1)=ic.Category_Code where i.id='" + sids + "' AND ic.majorgroup_code='" + majorcode + "' ", con);
                da.Fill(ds, "itemcode");             
                txtspecifcationname.Text = ds.Tables["itemcode"].Rows[0].ItemArray[0].ToString();
                txtspecifcationname.Enabled = false;
                txtspecification.Text = ds.Tables["itemcode"].Rows[0].ItemArray[1].ToString();
                txtspecification.Enabled = false;
                ddldca.SelectedItem.Text = ds.Tables["itemcode"].Rows[0].ItemArray[2].ToString();
                ddldca.Enabled = false;
                ddlSub.SelectedItem.Text = ds.Tables["itemcode"].Rows[0].ItemArray[3].ToString();
                ddlSub.Enabled = false;
                txtunits.Text = ds.Tables["itemcode"].Rows[0].ItemArray[4].ToString();
                txtunits.Enabled = false;
                lstunits.Visible = false;
                txtbasicprice.Text = ds.Tables["itemcode"].Rows[0].ItemArray[5].ToString();
                txtbasicprice.Enabled = false;
                txtcategory.Text = ds.Tables["itemcode"].Rows[0].ItemArray[6].ToString();
                txtcategory.Enabled = false;
                DDExtender3.Enabled = false;
                FilteredTextBoxExtender3.Enabled = false;
                lstcategory.Visible = false;
                lstgccode.Visible = false;
                lstsubgroup.Visible = false;
                txtcname.Text = ds.Tables["itemcode"].Rows[0].ItemArray[7].ToString();
                txtcname.Enabled = false;
                txtsubgroupcode.Text = ds.Tables["itemcode"].Rows[0].ItemArray[8].ToString();
                txtsubgroupcode.Enabled = false;
                txtgcode.Text = ds.Tables["itemcode"].Rows[0].ItemArray[9].ToString();
                txtgcode.Enabled = false;
                txtgname.Text = ds.Tables["itemcode"].Rows[0].ItemArray[10].ToString();
                txtgname.Enabled = false;
                Btnaddnew.Enabled = false;
                lblitemcode.Text = ds.Tables["itemcode"].Rows[0].ItemArray[11].ToString();
                popitems.Hide();
            }

        }
    }
    public void fillgrid()
    {
        try
        {
            //if (Session["roles"].ToString() == "StoreKeeper")
            //    da = new SqlDataAdapter("select id,Item_code,item_name,Specification,Units,Basic_Price,Subdca_Code from item_codes where status='1B'", con);
            // Disabled for storekeeper as discussed with annop as on dated 26-Dec-2018
            if (Session["roles"].ToString() == "Central Store Keeper")
            {
                da = new SqlDataAdapter("select id,Item_code,item_name,Specification,Units,Basic_Price,Subdca_Code from item_codes where status='2B'", con);
                //if (Session["roles"].ToString() == "Chief Material Controller")
                //    da = new SqlDataAdapter("select id,Item_code,item_name,Specification,Units,Basic_Price,Subdca_Code from item_codes where status='3B'", con);
                ds.Clear();
                da.Fill(ds, "newitemcode");
                if (ds.Tables["newitemcode"].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables["newitemcode"];
                    GridView1.DataBind();
                    Btnsbmt.Visible = true;
                    Btnrmv.Visible = true;
                    newitem.Visible = true;
                    trdesc.Visible = true;
                    trdlbl.Visible = true;
                    Btnaddnew.Visible = false;
                    BindGridview();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    Btnsbmt.Visible = false;
                    Btnrmv.Visible = false;
                    newitem.Visible = false;
                    trdesc.Visible = false;
                    trdlbl.Visible = false;
                    Btnaddnew.Visible = true;
                }
            }
           

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void Btnaddnew_Click(object sender, EventArgs e)
    {
        string itemcode = txtcategory.Text + txtgcode.Text + txtsubgroupcode.Text + txtspecification.Text;

        if (itemcode.Length.ToString() == "8")
        {
            try
            {
                cmd = new SqlCommand("ItemCodeCreation_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemCode", itemcode);
                cmd.Parameters.AddWithValue("@ItemName", txtitemname.Text);
                cmd.Parameters.AddWithValue("@CategoryCode", txtcategory.Text);
                cmd.Parameters.AddWithValue("@CategoryName", txtcname.Text);
                cmd.Parameters.AddWithValue("@MajorgroupCode", txtgcode.Text);
                cmd.Parameters.AddWithValue("@MajorgroupName", txtgname.Text);
                cmd.Parameters.AddWithValue("@SubgroupCode", txtsubgroupcode.Text);
                cmd.Parameters.AddWithValue("@SpecificationCode", txtspecification.Text);
                cmd.Parameters.AddWithValue("@SpecificationName", txtspecifcationname.Text);
                cmd.Parameters.AddWithValue("@DCACode", ddldca.SelectedValue);
                cmd.Parameters.AddWithValue("@SubDca", ddlSub.SelectedValue);
                cmd.Parameters.AddWithValue("@units", txtunits.Text);
                cmd.Parameters.AddWithValue("@basicprice", txtbasicprice.Text);
                cmd.Parameters.AddWithValue("@HSNCode", ddlhsncode.SelectedValue);
                cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
                cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@Type", "Create");
                cmd.Connection = con;
                con.Open();

                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "Sucessfully Added";
                con.Close();
                if (msg == "Sucessfully Added")
                    JavaScript.UPAlertRedirect(Page, msg, "ItemCodeCreation.aspx");
                else
                {
                    lstgccode.Style.Add("visibility", "hidden");
                    lstsubgroup.Style.Add("visibility", "hidden");
                    //lstspecification.Style.Add("visibility", "hidden");
                    JavaScript.UPAlert(Page, msg);
                }
            }
            catch (Exception ex)
            {
                Utilities.CatchException(ex);
                JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
            }
        }
        else
        {
            JavaScript.UPAlert(Page, "Invalid Code");
        }
        fillgrid();
    }

    protected void Btnaddindnt_Click(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_code='" + lblitemcode.Text + "'", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "insert into indent_list(item_code,cc_code)values(@itemcode,@CCCode)";
                    cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = lblitemcode.Text;
                    if (Session["roles"].ToString() == "Central Store Keeper")
                        cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                    else
                    {
                        cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                    }
                    bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    if (i == true)
                    {
                        JavaScript.UPAlertRedirect(Page, "Added to Indent", "ItemCodeCreation.aspx");
                    }
                }
                else            
                    JavaScript.UPAlert(Page, "Invalid itemcode");
               
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void Btnrmv_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row1 in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)row1.FindControl("chkSelect1");
                cmd.Connection = con;
                cmd = new SqlCommand("sp_deleteitemcode", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (chk.Checked)
                {
                    cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[row1.RowIndex]["id"].ToString());
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    if (msg == "Sucessfull")
                        JavaScript.UPAlert(Page, msg);
                    else
                        JavaScript.UPAlert(Page, msg);
                    con.Close();
                }
            }
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void clear()
    {
        txtbasicprice.Text = "";
        txtcategory.Text = "";
        txtcname.Text = "";
        txtgcode.Text = "";
        txtgname.Text="";
        txtitemname.Text="";
        txtspecifcationname.Text="";
        txtspecification.Text="";
        txtsubgroupcode.Text="";
        txtunits.Text="";
        ddlSub.SelectedItem.Text="Select Sub DCA";
        ddldca.SelectedItem.Text = "Select DCA";
    }
    protected void Btnsbmt_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
            {
                string ids = "";
                string itemcodes = "";
                string Supnames = "";
                string Supmake = "";
                string Suprate = "";
                string Supcont = "";
                string Supmail = "";
                foreach (GridViewRow record in GridView1.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect1");
                    if (c1.Checked)
                    {
                        ids = ids + GridView1.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        itemcodes = itemcodes + record.Cells[2].Text + ",";
                    }
                }
                foreach (GridViewRow record in gvDetails.Rows)
                {

                    TextBox txtsname = (TextBox)record.FindControl("txtSupName");
                    string sname = txtsname.Text;
                    TextBox txtmake = (TextBox)record.FindControl("txtMake");
                    string make = txtmake.Text;
                    TextBox txtrate = (TextBox)record.FindControl("txtRate");
                    string rate = txtrate.Text;
                    TextBox txtcontno = (TextBox)record.FindControl("txtContractno");
                    string contract = txtcontno.Text;
                    TextBox txtemail = (TextBox)record.FindControl("txtEmail");
                    string email = txtemail.Text;
                    if (sname != "")
                    {
                        Supnames = Supnames + sname + ",";
                        Supmake = Supmake + make + ",";
                        Suprate = Suprate + rate + ",";
                        Supcont = Supcont + contract + ",";
                        Supmail = Supmail + email + ",";
                    }
                }

                if (ids != null)
                {
                    cmd.Connection = con;
                    cmd = new SqlCommand("ItemCodeCreation_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ids", ids);
                    cmd.Parameters.AddWithValue("@ItemCodes", itemcodes);
                    cmd.Parameters.AddWithValue("@SupplierNames", Supnames);
                    cmd.Parameters.AddWithValue("@Makes", Supmake);
                    cmd.Parameters.AddWithValue("@Rates", Suprate);
                    cmd.Parameters.AddWithValue("@Contractnos", Supcont);
                    cmd.Parameters.AddWithValue("@Emailss", Supmail);
                    cmd.Parameters.AddWithValue("@Remark", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
                    cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@Type", "Submit");
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    if (msg == "Sucessfully Submitted")
                    {
                        gvDetails.DataSource = null;
                        gvDetails.DataBind();
                        txtDescription.Text = "";
                        JavaScript.UPAlert(Page, msg);
                    }
                    else
                        JavaScript.UPAlert(Page, msg);
                    con.Close();
                }
                fillgrid();
            }
            else
            {
                JavaScript.UPAlert(Page, "You are not authorized person");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void checkapprovals()
    {
        if (Session["roles"].ToString() == "StoreKeeper")
        {
            da = new SqlDataAdapter("SELECT TOP 1 * from  closedcost_center where cc_code = '" + Session["CC_CODE"].ToString() + "' and status not in('Rejected') ORDER by id desc", con);
            da.Fill(ds, "check");
            if (ds.Tables["check"].Rows.Count > 0)
            {
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "1")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Temporary Closed store approval pending at project Manager", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                        JavaScript.AlertAndRedirect("Store Re-open approval pending at project Manager", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at project Manager", "PurchaseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "2")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Temporary Closed store approval pending at central store keeper", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                        JavaScript.AlertAndRedirect("Store Re-open approval pending at central store keeper", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at central store keeper", "PurchaseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "3")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Store is in Temporary Closing Mode", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at chief Material Controller", "PurchaseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "4")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store is Already closed", "PurchaseHome.aspx");
                }
            }
        }
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtbl"] = dt;
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();
                    for (int i = 0; i < gvDetails.Rows.Count - 1; i++)
                    {
                        gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    
                    SetOldData();
                }                
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }
    protected void BindGridview()
    {
        DataTable dt = new DataTable();        
        dt.Columns.Add("SupplierName", typeof(string));
        dt.Columns.Add("Make", typeof(string));
        dt.Columns.Add("Rate", typeof(string));
        dt.Columns.Add("Contractno", typeof(string));
        dt.Columns.Add("Email", typeof(string));
        DataRow dr = dt.NewRow();
        dr["SupplierName"] = string.Empty;
        dr["Make"] = string.Empty;
        dr["Rate"] = string.Empty;
        dr["Contractno"] = string.Empty;
        dr["Email"] = string.Empty;        
        dt.Rows.Add(dr);
        ViewState["Curtbl"] = dt;
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }
    private void AddNewRow()
    {
        int rowIndex = 0;

        if (ViewState["Curtbl"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbl"];
            DataRow drCurrentRow = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtsname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtSupName");
                    TextBox txtmake = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtMake");
                    TextBox txtrate = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtRate");                   
                    TextBox txtcontractno = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtContractno");
                    TextBox txtemail = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("txtEmail");               
                    drCurrentRow = dt.NewRow();
                    dt.Rows[i - 1]["SupplierName"] = txtsname.Text;
                    dt.Rows[i - 1]["Make"] = txtmake.Text;
                    dt.Rows[i - 1]["Rate"] = txtrate.Text;
                    dt.Rows[i - 1]["Contractno"] = txtcontractno.Text;
                    dt.Rows[i - 1]["Email"] = txtemail.Text;
                    rowIndex++;
                }
                dt.Rows.Add(drCurrentRow);
                ViewState["Curtbl"] = dt;
                gvDetails.DataSource = dt;
                gvDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState Value is Null");
        }
        SetOldData();
    }
    private void SetOldData()
    {
        int rowIndex = 0;
        if (ViewState["Curtbl"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbl"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox txtsname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtSupName");
                    TextBox txtmake = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtMake");
                    TextBox txtrate = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtRate");
                    TextBox txtcontractno = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtContractno");
                    TextBox txtemail = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("txtEmail");
                    txtsname.Text = dt.Rows[i]["SupplierName"].ToString();
                    txtmake.Text = dt.Rows[i]["Make"].ToString();
                    txtrate.Text = dt.Rows[i]["Rate"].ToString();
                    txtcontractno.Text = dt.Rows[i]["Contractno"].ToString();
                    txtemail.Text = dt.Rows[i]["Email"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    public int i = 0;
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if (ViewState["Curtbl"] != null)
                {
                    DataTable Objdt = ViewState["Curtbl"] as DataTable;
                    if (Objdt.Rows[i]["SupplierName"].ToString() != "" && Objdt.Rows[i]["Make"].ToString() != "" && Objdt.Rows[i]["Rate"].ToString() != "" && Objdt.Rows[i]["Contractno"].ToString() != "" && Objdt.Rows[i]["Email"].ToString() != "")
                    {
                        TextBox txtsupname = (TextBox)e.Row.FindControl("txtSupName");
                        txtsupname.Text = Objdt.Rows[i]["SupplierName"].ToString();

                        TextBox txtmake = (TextBox)e.Row.FindControl("txtMake");
                        txtmake.Text = Objdt.Rows[i]["Make"].ToString();

                        TextBox txtrate = (TextBox)e.Row.FindControl("txtRate");
                        txtrate.Text = Objdt.Rows[i]["Rate"].ToString();

                        TextBox txtcontract = (TextBox)e.Row.FindControl("txtContractno");
                        txtcontract.Text = Objdt.Rows[i]["Contractno"].ToString();

                        TextBox txtemail = (TextBox)e.Row.FindControl("txtEmail");
                        txtemail.Text = Objdt.Rows[i]["Email"].ToString();

                    }
                }                
                i = i + 1;
                if (i == 1)
                {
                    e.Row.Cells[6].Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }

  
}
     
