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



public partial class Transfer : System.Web.UI.Page
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
            Response.Redirect("Default.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 11);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "StoreKeeper")
            {
                checkapprovals(Session["CC_CODE"].ToString());
            }
            Fillviewtransfergrid();
            LoadYear();
            //checkpercent();
            hfrole.Value = Session["roles"].ToString();
        }
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
    private void Fillviewtransfergrid()
    {
        if (Session["roles"].ToString() == "StoreKeeper")
            da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where  Ti.cc_code='" + Session["cc_code"].ToString() + "' and type='2' and (status='2'  or status='4') union  select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info]TI join [Items Transfer]IT  on TI.ref_no=IT.ref_no   where  recieved_cc='" + Session["cc_code"].ToString() + "' and type='2' and status='6'  order by TI.status asc ", con);

        else if (Session["roles"].ToString() == "Project Manager")
            da = new SqlDataAdapter("select distinct i.* from(select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   TI.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and type='2' and (status='3' or status='3A') union select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   recieved_cc in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and type='2' and  (status='5' or status='4'))i order by i.status asc ", con);
        else if (Session["roles"].ToString() == "Central Store Keeper" )
            da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and (status='3A' or  status='4') order by TI.status asc ", con);

            //da = new SqlDataAdapter("select distinct TI.ref_no,transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and   (status='3A' or status='6')  union all select distinct TIN.ref_no,transfer_date,TIN.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TIN join [items transfer]ITT on TIN.ref_no=ITT.ref_no  where   type='2' and   status='3A' and recieved_cc='"+Session["cc_code"].ToString()+"' order by transfer_date desc", con);
        else if (Session["roles"].ToString() == "Chief Material Controller")
            da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no   where type='2' and (status='3B' or status='1A')  order by TI.status asc ", con);

        else if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and (status='4' or status='2')  union select distinct TI.id, TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and (status='4' or status='3A') order by TI.status asc ", con);
        da.Fill(ds, "fill");
        grdviewtransfer.DataSource = ds.Tables["fill"];
        grdviewtransfer.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
        grdviewtransfer.DataBind();
    }

    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filtertransfer();
        }

        else
        {
            Fillviewtransfergrid();
        }
    }


    protected void grdviewtransfer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grdviewtransfer.PageIndex = e.NewPageIndex;
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filtertransfer();

        }

        else
        {
            Fillviewtransfergrid();
        }
    }
    protected void grdviewtransfer_DataBound(object sender, EventArgs e)
    {
        //--- For Paging ---------
        GridViewRow row = grdviewtransfer.BottomPagerRow;

        if (row == null)
        {
            return;
        }

        //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
        Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
        Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

        //if (lblPages != null)
        //{
        lblCurrent.Text = grdviewtransfer.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = grdviewtransfer.PageIndex + 1;
        lblPages.Text = currentPage.ToString();

        //-- For First and Previous ImageButton
        if (grdviewtransfer.PageIndex == 0)
        {
            ((ImageButton)grdviewtransfer.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            ((ImageButton)grdviewtransfer.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            ((ImageButton)grdviewtransfer.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            ((ImageButton)grdviewtransfer.BottomPagerRow.FindControl("btnPrev")).Visible = false;

        }

        //-- For Last and Next ImageButton
        if (grdviewtransfer.PageIndex + 1 == grdviewtransfer.PageCount)
        {
            ((ImageButton)grdviewtransfer.BottomPagerRow.FindControl("btnLast")).Enabled = false;
            ((ImageButton)grdviewtransfer.BottomPagerRow.FindControl("btnLast")).Visible = false;

            ((ImageButton)grdviewtransfer.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            ((ImageButton)grdviewtransfer.BottomPagerRow.FindControl("btnNext")).Visible = false;


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
        int iCurrentIndex = grdviewtransfer.PageIndex;

        switch (e.CommandArgument.ToString().ToLower())
        {
            case "first":
                grdviewtransfer.PageIndex = 0;
                break;
            case "prev":
                if (grdviewtransfer.PageIndex != 0)
                {
                    grdviewtransfer.PageIndex = iCurrentIndex - 1;
                }
                break;
            case "next":
                grdviewtransfer.PageIndex = iCurrentIndex + 1;
                break;
            case "last":
                grdviewtransfer.PageIndex = grdviewtransfer.PageCount;
                break;
        }


        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filtertransfer(); 

        }


        else
        {
            Fillviewtransfergrid();
        }
    }
    protected void grdviewtransfer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grdviewtransfer.SelectedIndex != -1)
        {
            string refno = grdviewtransfer.SelectedValue.ToString();
            GridViewRow id = grdviewtransfer.SelectedRow;
            string CCCode = id.Cells[4].Text;
            string RecievedCC = id.Cells[5].Text;
            ViewState["ref_no"] = refno;
            ViewState["CCCode"] = CCCode;
            ViewState["RecievedCC"] = RecievedCC;
            txtrecieptdate.Text = id.Cells[3].Text;
            HiddenField hf = (HiddenField)grdviewtransfer.Rows[grdviewtransfer.SelectedIndex].Cells[7].FindControl("h1");
            approvetransfer.Visible = false;
            viewtransfer.Visible = true;
            btnmdlupd.Visible = false;
            grdpopedit.Visible = false;
            grdpopcentral.Visible = false;
            grdpopcmc.Visible = false;
            cmcdetails.Visible = false;
            grdpopview.Visible = true;
            print.Visible = true;

            fillpopupview();
            CCInfo();
            pname();
            mdlviewtransfer.Show();

        }
       
    }
    protected void grdviewtransfer_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
        string refno = grdviewtransfer.DataKeys[e.NewEditIndex]["ref_no"].ToString();
        ViewState["refno"] = refno;
        HiddenField hf = (HiddenField)grdviewtransfer.Rows[e.NewEditIndex].Cells[7].FindControl("h1");
        ViewState["hf"] = hf.Value;
        approvetransfer.Visible = true;
        viewtransfer.Visible = false;
        btnmdlupd.Visible = true;
        print.Visible = false;
        txtrecieptdate.Text = grdviewtransfer.Rows[e.NewEditIndex].Cells[3].Text;
        if (Session["roles"].ToString() == "Project Manager")
        {
            checkapprovals(grdviewtransfer.Rows[e.NewEditIndex].Cells[4].Text);
        }
       
        if (hf.Value == "3" && Session["roles"].ToString() == "Project Manager")/*This is for sender Project Manager*/
        {
            fillpopup();
            grdpopedit.Visible = true;
            grdpopview.Visible = false;
            grdpopcentral.Visible = false;
            grdpopcmc.Visible = false;
            cmcdetails.Visible = false;
            btnmdlupd.Text = "Verified";
            mdlviewtransfer.Show();

        }
        else if(hf.Value == "3A" && Session["roles"].ToString() == "Central Store Keeper")
        {
            fillpopup();
            grdpopedit.Visible = false;
            grdpopview.Visible = false;
            grdpopcentral.Visible = true;
            grdpopcmc.Visible = false;
            gridcmc.Visible = false;
            cmcdetails.Visible = false;
            btnmdlupd.Text = "Verified";
            mdlviewtransfer.Show();

        }
        else if (hf.Value == "1A" && Session["roles"].ToString() == "Chief Material Controller")/*This is for CMC*/
        {
            fillpopup();
            btnmdlupd.Text = "Verified";
            grdpopcmc.Visible = true;
            cmcdetails.Visible = true;
            grdpopcentral.Visible = false;
            gridcmc.Visible = false;
            grdpopedit.Visible = false;
            grdpopview.Visible = false;
            grdpopcentral.Visible = false;
            mdlviewtransfer.Show();

        }
        else if (hf.Value == "3B" && Session["roles"].ToString() == "Chief Material Controller")
        {
            fillpopup();
            grdpopedit.Visible = false;
            grdpopview.Visible = false;
            grdpopcentral.Visible = true;
            grdpopcmc.Visible = false;
            gridcmc.Visible = true;
            cmcdetails.Visible = false;
            btnmdlupd.Text = "Verified";
            mdlviewtransfer.Show();

        }
        else if (hf.Value == "2" && Session["roles"].ToString() == "StoreKeeper")/*This is for storekeeper*/
        {
            fillpopup();
            btnmdlupd.Text = "Verified";
            grdpopedit.Visible = true;
            grdpopview.Visible = false;
            grdpopcentral.Visible = false;
            grdpopcmc.Visible = false;
            gridcmc.Visible = false;
            cmcdetails.Visible = false;
            mdlviewtransfer.Show();

        }
        else if (hf.Value == "5" && Session["roles"].ToString() == "Project Manager")/*This is for recieve incharge*/
        {
            fillpopup();
            grdpopedit.Visible = false;
            grdpopcentral.Visible = false;
            grdpopview.Visible = true;
            grdpopcmc.Visible = false;
            gridcmc.Visible = false;
            cmcdetails.Visible = false;
            btnmdlupd.Text = "Verified";
            mdlviewtransfer.Show();
           
        }
        else if (hf.Value == "6")
        {
            mdlviewtransfer.Hide();
        }
        else if ((Session["roles"].ToString() == "Central Store Keeper") && (hf.Value == "4"))
        {
            Response.Redirect("StockUpdation.aspx");
        }
        else
        {
            mdlviewtransfer.Hide();
        }
                        
      
    }
    public void fillpopup()
    {
        try
        {
            if ((Session["roles"].ToString() == "Project Manager" && ViewState["hf"].ToString() == "3") || Session["roles"].ToString() == "StoreKeeper" )
            {
                da = new SqlDataAdapter("Select il.id,il.item_code ,item_name as [Item Name],specification as [Specification],dca_code as [DCA Code],subdca_code as [Sub DCA],units as [Units],quantity as [Transfered Qty],item_status from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,quantity,item_status  from [Items Transfer]it where Ref_no='" + ViewState["refno"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillin");
                grdpopedit.DataSource = ds.Tables["fillin"];
                grdpopedit.DataBind();
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,units,Replace(il.quantity,'.00','')quantity,item_status,Replace((basic_price*il.quantity),'.0000','.00')[Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,item_status from [Items Transfer] where Ref_no='" + ViewState["refno"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillcentral");
                grdpopcentral.DataSource = ds.Tables["fillcentral"];
                grdpopcentral.DataBind();
            }
            else if ((Session["roles"].ToString() == "Chief Material Controller") && (ViewState["hf"].ToString() != "1A"))
            {
                //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.0000','.00')basic_price,units,Replace(il.quantity,'.00','')quantity,item_status,Replace((basic_price*il.quantity),'.0000','.00')[Amount],csk_percent,replace(csk_dep,'.0000','.00')as csk_dep from (Select item_code,item_name,specification,dca_code,subdca_code,basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,item_status,csk_percent,csk_dep from [Items Transfer] where Ref_no='" + ViewState["refno"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,isnull(Replace(basic_price,'.0000','.00'),0)basic_price,units,Replace(il.quantity,'.00','')quantity,item_status,Replace((isnull(basic_price,0)*il.quantity),'.0000','.00')[Amount],isnull(csk_percent,0) as csk_percent ,isnull(csk_dep,0)as csk_dep from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0) basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,item_status,csk_percent,csk_dep from [Items Transfer] where Ref_no='" + ViewState["refno"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillcmc");
                gridcmc.DataSource = ds.Tables["fillcmc"];
                gridcmc.DataBind();
            }
            else if((Session["roles"].ToString() == "Project Manager" && ViewState["hf"].ToString() == "5" )|| Session["roles"].ToString() == "PurchaseManager") 
            {
                da = new SqlDataAdapter("Select il.item_code as [Item Code] ,item_name as [Item Name],specification as [Specification],dca_code as [DCA Code],subdca_code as [Sub DCA],units as [Units],isnull([Recieved Qty],0)+isnull([Lost Qty],0) [Transfered Qty],[Recieved Qty],item_status,[Lost Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select distinct it.Id,it.item_code,it.quantity as [Recieved Qty],it.item_status,(SELECT quantity from [lost/damaged_items] where ref_no='" + ViewState["refno"].ToString() + "') as [Lost Qty] from [Items Transfer]it  left join [lost/damaged_items] l on it.item_code=l.Item_code where it.Ref_no='" + ViewState["refno"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillincharge");
                grdpopview.DataSource = ds.Tables["fillincharge"];
                grdpopview.DataBind();
            }
            else if ((Session["roles"].ToString() == "Chief Material Controller") && (ViewState["hf"].ToString() == "1A"))
            {
                da = new SqlDataAdapter("Select K.*,isnull(j.quantity,0) as [Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,isnull(Replace(basic_price,'.0000','.00'),0)as basic_price,[Issued Qty],il.remarks,csk_percent,replace(csk_dep,'.0000','.00') as csk_dep ,transfer_date,replace((ISNULL(il.basic_price,0)*ISNULL(il.quantity,0)),'.0000','.00')as qty from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity as [Issued Qty],it.basic_price,ti.remarks,csk_percent,csk_dep,ti.transfer_date,it.quantity from [Transfer Info]ti join [Items transfer]it on ti.ref_no=it.ref_no where status='1A' and type='2' AND it.ref_no='" + ViewState["refno"].ToString() + "')il on i.item_code=il.item_code)k join (Select item_code,quantity from master_data where cc_code =(Select distinct tin.cc_code from [Transfer Info]tin join [Items transfer]itt on tin.ref_no=itt.ref_no where status='1A' and type='2' AND itt.ref_no='" + ViewState["refno"].ToString() + "'))j on j.item_code=k.item_code", con);
                da.Fill(ds, "filling");
                grdpopcmc.DataSource = ds.Tables["filling"];
                grdpopcmc.DataBind();
                if (ds.Tables["filling"].Rows.Count > 0)
                {
                    txtremarks.Text = ds.Tables["filling"].Rows[0].ItemArray[9].ToString();
                    txtdate.Text = ds.Tables["filling"].Rows[0].ItemArray[12].ToString();
                }
                else
                {
                    txtremarks.Text = "";
                    txtdate.Text = "";
                }

            }
           
           
            
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillpopupview()
    {
        gridcmc.Visible = false;
        try
        {
            da = new SqlDataAdapter("Select il.item_code as [Item Code],item_name as [Item Name],specification as [Specification],dca_code as [DCA Code],subdca_code as [Sub DCA],units as [Units],[Issued Qty] as [Transfered Qty],item_status as [Item Status],transfer_date from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity as [Issued Qty],item_status,transfer_date from [Items Transfer]it join [Transfer Info] ti on it.ref_no=ti.ref_no where it.Ref_no='" + ViewState["ref_no"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
            //da = new SqlDataAdapter("Select il.item_code as [Item Code],item_name as [Item Name],specification as [Specification],dca_code as [DCA Code],subdca_code as [Sub DCA],units as [Units],[Issued Qty] as [Transfered Qty],isnull([Issued Qty],0)-isnull([lost qty],0) [Recieved Qty],transfer_date from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code,quantity as [Issued Qty],(Select quantity from [Lost/Damaged_items] where cc_code='" + Session["cc_code"].ToString() + "' and item_code=it.item_code)as [lost qty],transfer_date from [Items Transfer]it join [Transfer Info] ti on it.ref_no=ti.ref_no where it.Ref_no='" + ViewState["ref_no"].ToString() + "')il on i.item_code=il.item_code", con);

            da.Fill(ds, "fillpopview");
            grdpopview.DataSource = ds.Tables["fillpopview"];
            lbldate.Text = ds.Tables["fillpopview"].Rows[0].ItemArray[7].ToString();
            grdpopview.DataBind();
            grdbill.DataSource = ds.Tables["fillpopview"];
            grdbill.DataBind();
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    
    
    }
   
    public void CCInfo()
    {
        try
        {
            da = new SqlDataAdapter("select cc_name,address from Cost_Center where cc_code='" + ViewState["CCCode"].ToString() + "' union all select cc_name,address from Cost_Center where cc_code='" + ViewState["RecievedCC"] + "'", con);
            da.Fill(ds, "CCInfo");
            lbldespfrom.Text = ds.Tables["CCInfo"].Rows[0].ItemArray[0].ToString();
            lbldespfromadd.Text = ds.Tables["CCInfo"].Rows[0].ItemArray[1].ToString();
            lbldespto.Text = ds.Tables["CCInfo"].Rows[1].ItemArray[0].ToString();
            lbldesptoadd.Text = ds.Tables["CCInfo"].Rows[1].ItemArray[1].ToString();
            lblchallanno.Text = ViewState["ref_no"].ToString();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Qtys = "";
            string status = "";
            string DepAmounts = "";
            string dep1 = "";
            decimal Amount2 = 0;
            string Qty = "";
            if (Session["roles"].ToString() == "Project Manager" && ViewState["hf"].ToString() == "3")
            {
                foreach (GridViewRow record in grdpopedit.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                    if (c1.Checked)
                    {

                        ids = ids + grdpopedit.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        
                    }
                }
                cmd = new SqlCommand("Stockupdation_ByIncharge_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@RefNo", ViewState["refno"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();

                JavaScript.UPAlertRedirect(Page, msg, "Transfer.aspx");
         
         

              
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                //if (ViewState["Status"] == "RedirectStockUpdation")
                //{
                //    JavaScript.UPAlert(Page, "StockUpdation.aspx");
                //}
                //else
                //{

                    foreach (GridViewRow record in grdpopcentral.Rows)
                    {
                        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                        if (c1.Checked && record.Cells[5].Text != "DCA-27")
                        {

                            ids = ids + grdpopcentral.DataKeys[record.RowIndex]["id"].ToString() + ",";

                            if ((record.FindControl("ddldep") as DropDownList).SelectedValue != "Full Value")
                            {
                                string Dep = (record.FindControl("ddldep") as DropDownList).SelectedValue;
                                dep1 = dep1 + Convert.ToDecimal(Convert.ToDecimal(Dep)) + ",";
                                string Amount1 = (record.FindControl("lbldepamount") as Label).Text;
                                DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                                Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100));
                                EffAmount = EffAmount + (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100);
                            }
                            else
                            {
                                string Amount1 = (record.FindControl("lbldepamount") as Label).Text;
                                DepAmounts = DepAmounts + 0 + ",";
                                Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount1));
                                dep1 = dep1 + "Full Value" + ",";
                            }
                        }

                    }
                    cmd = new SqlCommand("Transfer from SiteStore to CentralStore_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ids", ids);
                    cmd.Parameters.AddWithValue("@DepAmounts", DepAmounts);
                    cmd.Parameters.AddWithValue("@EffAmount", EffAmount);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@depcsks", dep1);
                    cmd.Parameters.AddWithValue("@RefNo", ViewState["refno"].ToString());
                    cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    con.Close();
                    JavaScript.UPAlertRedirect(Page, msg, "Transfer.aspx");
                  
                    //JavaScript.UPAlertRedirect(Page, msg, "StockUpdation.aspx");
                //}
            }

            else if ((Session["roles"].ToString() == "Chief Material Controller") && (ViewState["hf"].ToString() != "1A"))
            {
                foreach (GridViewRow record in gridcmc.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                    if (c1.Checked && record.Cells[5].Text != "DCA-27")
                    {

                        ids = ids + gridcmc.DataKeys[record.RowIndex]["id"].ToString() + ",";

                        if ((record.FindControl("ddldep") as DropDownList).SelectedValue != "Full Value")
                        {
                            string Dep = (record.FindControl("ddldep") as DropDownList).SelectedValue;
                            dep1 = dep1 + Convert.ToDecimal(Convert.ToDecimal(Dep)) + ",";
                            string Amount1 = (record.FindControl("lbldepamount") as Label).Text;
                            DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                            Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100));
                            EffAmount = EffAmount + (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100);
                        }
                        else
                        {
                            //string Dep = (record.FindControl("ddldep") as DropDownList).SelectedValue;
                            int dep3 = 0;
                            string Amount1 = (record.FindControl("lbldepamount") as Label).Text;
                            DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(0)) + ",";
                            Amount = Amount + 0;
                            EffAmount = EffAmount + (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(dep3))) / 100);
                           
                        }
                    }

                }
                cmd = new SqlCommand("Transfer from SiteStore to CentralStore_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@DepAmounts", DepAmounts);
                cmd.Parameters.AddWithValue("@EffAmount", EffAmount);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                //cmd.Parameters.AddWithValue("@depcsks", dep1);
                cmd.Parameters.AddWithValue("@RefNo", ViewState["refno"].ToString());
                cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                JavaScript.UPAlertRedirect(Page, msg, "Transfer.aspx");
                
            }
            else if ((Session["roles"].ToString() == "Chief Material Controller") && (ViewState["hf"].ToString() == "1A"))
            {
                foreach (GridViewRow record in grdpopcmc.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                    if (c1.Checked && record.Cells[5].Text != "DCA-27")
                    {

                        ids = ids + grdpopcmc.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        {
                            if ((record.FindControl("ddldep") as DropDownList).SelectedValue != "Full Value")
                            {
                                string Dep = (record.FindControl("ddldep") as DropDownList).SelectedValue;
                                dep1 = dep1 + Convert.ToDecimal(Convert.ToDecimal(Dep)) + ",";
                                Amt = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                                Double i = Convert.ToDouble(record.Cells[10].Text) - Convert.ToDouble((record.FindControl("txtqty") as TextBox).Text);
                                DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amt) - (((Convert.ToDecimal(Amt)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                                Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amt) - (((Convert.ToDecimal(Amt)) * (Convert.ToDecimal(Dep))) / 100));
                                EffAmount = EffAmount + (((Convert.ToDecimal(Amt)) * (Convert.ToDecimal(Dep))) / 100) + (Convert.ToDecimal(Convert.ToDecimal(i) * Convert.ToDecimal(record.Cells[7].Text)));


                            }
                            else if ((record.FindControl("ddldep") as DropDownList).SelectedValue == "Full Value")
                            {
                                Amount2 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                                DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount2)) + ",";
                                Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount2));

                            }
                        }

                    }

                }
                cmd = new SqlCommand("Transfer from CentralStore_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@Qtys", Qty);
                cmd.Parameters.AddWithValue("@DepAmounts", DepAmounts);
                cmd.Parameters.AddWithValue("@EffAmount", EffAmount);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@Days", ddlDays.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
                cmd.Parameters.AddWithValue("@ReferenecNo", ViewState["refno"].ToString());
                cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "Sucessfull")
                    JavaScript.UPAlertRedirect(Page, msg, "Transfer.aspx");
                else
                    JavaScript.UPAlert(Page, msg);

            }
            else if (Session["roles"].ToString() == "StoreKeeper")
            {
                cmd.CommandText = "Update [Transfer Info] set status='3',created_by='" + Session["user"].ToString() + "' where ref_no='" + ViewState["refno"].ToString() + "'";

                cmd.Connection = con;
                con.Open();
                bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (i == true)
                    JavaScript.UPAlertRedirect(Page, "Sucessfull", "Transfer.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Failed", "Transfer.aspx");
                con.Close();

            }
            else if ((Session["roles"].ToString() == "Project Manager" && ViewState["hf"].ToString() == "5") || Session["roles"].ToString() == "PurchaseManager")
            {
                cmd.Connection = con;
                cmd.CommandText = "Update [Transfer Info] Set status='6' where ref_no='" + ViewState["refno"].ToString() + "'";
                con.Open();
                bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (i == true)
                    JavaScript.UPAlertRedirect(Page, "Sucessfull", "Transfer.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Failed", "Transfer.aspx");
                con.Close();
            }
        }


        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    private decimal Amount = (decimal)0.0;
    private decimal EffAmount = (decimal)0.0;
    private decimal Amount1 = (decimal)0.0;
    private decimal EffAmount1 = (decimal)0.0;
   

    protected void grdviewtransfer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image img = (Image)e.Row.FindControl("Image1");
                HiddenField hf = (HiddenField)e.Row.FindControl("h1");
                       
                if (Session["roles"].ToString() != "Central Store Keeper" && Session["roles"].ToString() != "Chief Material Controller")
                {
                    if (hf.Value == "2" || hf.Value == "3" || hf.Value == "5")
                        img.Visible = true;
                    else
                        img.Visible = false;
                }
                else if (Session["roles"].ToString() == "Central Store Keeper")
                {
                    if (hf.Value == "3A" || hf.Value == "4")
                        img.Visible = true;
                    else
                        img.Visible = false;
                }
                else if (Session["roles"].ToString() == "Chief Material Controller")
                {
                    if (hf.Value == "3B" || hf.Value == "1A")
                        img.Visible = true;
                    else
                        img.Visible = false;
                }
            }
        }
    }
    protected void btnprintprivew_Click(object sender, EventArgs e)
    {
       
        
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        printing.RenderControl(hw);
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
        ClientScript.RegisterStartupScript(this.GetType(), "printing", sb.ToString());

    }
  
    public void pname()
    {
        try
        {
            //da = new SqlDataAdapter("Select i.* from (Select r.user_name,first_name,last_name from employee_data r join user_roles u on r.user_name=u.user_name  where u.Roles ='StoreKeeper')i join cc_user u on i.user_name=u.user_name where cc_code='"+ViewState["CCCode"]+"'", con);
            da = new SqlDataAdapter("Select first_name,last_name from employee_data r join [transfer info]u on r.user_name=u.created_by where u.ref_no='" + ViewState["ref_no"].ToString() + "'", con);
            da.Fill(ds, "pnameinfo");
            if (ds.Tables["pnameinfo"].Rows.Count > 0)
            {

                lblstorekeepername.Text = ds.Tables["pnameinfo"].Rows[0][0].ToString() + "  " + ds.Tables["pnameinfo"].Rows[0][1].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }


    protected void btnsearch_Click(object sender, EventArgs e)
    {
        filtertransfer();
    }
    public void filtertransfer()
    {
        try
        {
            string Condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    Condition = Condition + " and datepart(mm,transfer_date)=" + ddlMonth.SelectedValue + " and datepart(yy,transfer_date)=" + yy;
                }
                Condition = Condition + "and convert(datetime,transfer_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
            }
            if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
            {
                if (ddlcccode.SelectedValue != "")
                    Condition = Condition + " and TI.cc_code='" + ddlcccode.SelectedValue + "'";

            }

            if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("select distinct TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and (status='3A' or  status='5' or status='4')" + Condition + " order by TI.status asc ", con);
            else if (Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select distinct TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and recieved_cc='CC-33' and (status='6' or status='4' or status='2')" + Condition + "  union select distinct TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and (status='4' or status='6') " + Condition + " order by TI.status asc ", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select distinct TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where   type='2' and TI.cc_code='" + ddlcccode.SelectedValue + "' and  (status='3' or  status='6' or status='3A')" + Condition + " union select distinct TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where type='2' and recieved_cc='" + ddlcccode.SelectedValue + "' and (status='5' or status='6') " + Condition + "  order by Ti.status asc ", con);
            else if (Session["roles"].ToString() == "StoreKeeper")
                da = new SqlDataAdapter("select distinct TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info] TI join [items transfer]IT on TI.ref_no=IT.ref_no  where  Ti.cc_code='" + Session["cc_code"].ToString() + "' and type='2' and (status='2' or  status='6' or status='4')" + Condition + " union select TI.ref_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,TI.cc_code as[Transfer Out],recieved_cc as[Transfer In],remarks,status from [Transfer Info]TI join [Items Transfer]IT  on TI.ref_no=IT.ref_no   where  recieved_cc='" + Session["cc_code"].ToString() + "' and type='2' and status='6' " + Condition + "  order by TI.status asc", con);


            da.Fill(ds, "indents");
            grdviewtransfer.DataSource = ds.Tables["indents"];
            grdviewtransfer.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            grdviewtransfer.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }

    public void checkpercent()
    {
        da = new SqlDataAdapter("Select return_percent from semiasset_dep", con);
        da.Fill(ds, "Find");
        hfcheck.Value = ds.Tables["Find"].Rows[0].ItemArray[0].ToString();
    }

    protected void gridcmc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            CascadingDropDown cc = (CascadingDropDown)e.Row.FindControl("CascadingDropDown5");
            HiddenField h1 = (HiddenField)e.Row.FindControl("h1");
            cc.SelectedValue = h1.Value;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            EffAmount1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "csk_dep"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount1);
            e.Row.Cells[13].Text = String.Format("Rs. {0:#,##,##,###.00}", EffAmount1);

        }
    }

    private decimal Amt = (decimal)0.0;
    private decimal EffAmt = (decimal)0.0;

    protected void grdpopcmc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            CascadingDropDown cc = (CascadingDropDown)e.Row.FindControl("CascadingDropDown4");
            HiddenField h1 = (HiddenField)e.Row.FindControl("h1");
            cc.SelectedValue = h1.Value;
           
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "qty"));
            EffAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "csk_dep"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[13].Text = String.Format("Rs. {0:#,##,##,###.00}", Amt);
            e.Row.Cells[14].Text = String.Format("Rs. {0:#,##,##,###.00}", EffAmt);

        }
    }

    protected void grdpopcentral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = String.Format("Rs. {0:#,##,##,###.00}", Amt);
         
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
