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
using System.Web.Services.Protocols;
using System.IO;
using System.Text;
using AjaxControlToolkit;
using System.Collections.Specialized;
using System.Web.Services;


public partial class ViewsVendorPo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlDataAdapter da1 = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGridviewpo();
            Trgvusers.Visible = false; 
        }
    }
         private void FillGridviewpo()
    {
        try
        {
            //if (Session["roles"].ToString() == "Chief Material Controller")
            //    da = new SqlDataAdapter("SELECT id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,status from purchase_details p  where (status='1' or status='2') order by status asc", con);
            //else if (Session["roles"].ToString() == "PurchaseManager")
            //    da = new SqlDataAdapter("SELECT id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,status from purchase_details p where  (status='2') order by status asc", con);
            //else if (Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "StoreKeeper" || Session["roles"].ToString() == "Central Store Keeper")
            //    da = new SqlDataAdapter("SELECT id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,status from purchase_details p where  (status='2') and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') order by status asc", con);
        if (Session["roles"].ToString() == "SuperAdmin")
                //da = new SqlDataAdapter("SELECT id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,status from purchase_details p  where  (status='1A' or status='2') order by status asc", con); //Old Query
            da = new SqlDataAdapter("SELECT id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,status,Type,Approved_Users from purchase_details p  where  (status='1A') order by status asc", con);


            da.Fill(ds, "central");
            grdviewpo.DataSource = ds.Tables["central"];
            grdviewpo.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            grdviewpo.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
         protected void grdviewpo_RowEditing(object sender, GridViewEditEventArgs e)
         {
             try
             {
                 HiddenField hf = (HiddenField)grdviewpo.Rows[e.NewEditIndex].Cells[9].FindControl("h1");
                 Label lbl = (Label)grdviewpo.Rows[e.NewEditIndex].Cells[0].FindControl("lblpo");
                 string indentno = grdviewpo.DataKeys[e.NewEditIndex]["indent_no"].ToString();
                 string type = grdviewpo.DataKeys[e.NewEditIndex]["Type"].ToString();
                 hfpotype.Value = type;
                 ViewState["po_no"] = lbl.Text;
                 ViewState["status"] = hf.Value;
                 ViewState["indentno"] = indentno;
                 if (grdviewpo.DataKeys[e.NewEditIndex]["Approved_Users"].ToString() != "")
                 {
                     Trgvusers.Visible = true;
                     string rolesamend = grdviewpo.DataKeys[e.NewEditIndex]["Approved_Users"].ToString().Replace("'", " ");
                     da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + rolesamend + "',',')", con);
                     da.Fill(ds, "splitrole");
                     DataTable dtra = ds.Tables["splitrole"];
                     ViewState["Curtblroles"] = dtra;
                     gvusers.DataSource = dtra;
                     gvusers.DataBind();
                 }
                 else
                 {
                     ViewState["Curtblroles"] = null;
                     gvusers.DataSource = null;
                     gvusers.DataBind();
                 }
                 if (grdviewpo.DataKeys[e.NewEditIndex]["Type"].ToString() != "PO")
                 {
                     if (ViewState["status"].ToString() == "1" || ViewState["status"].ToString() == "1A")
                     {
                         tdvencode.Visible = true;
                         btn.Visible = true;
                         print.Visible = false;
                         lblviewpo.Text = "Verify PO";
                     }
                     else if (ViewState["status"].ToString() == "3")
                     {
                         btn.Visible = false;
                         print.Visible = true;
                         tdvencode.Visible = false;
                         lblviewpo.Text = "View PO";
                         Trgvusers.Visible = false;
                     }
                     else
                     {

                         pname();
                         btn.Visible = false;
                         print.Visible = true;
                         tdvencode.Visible = false;
                         lblviewpo.Text = "View PO";
                         Trgvusers.Visible = false;
                     }
                 }
                 if (grdviewpo.DataKeys[e.NewEditIndex]["Type"].ToString() == "PO")
                 {
                     if (ViewState["status"].ToString() == "2")
                     {
                         btn.Visible = false;                         
                         print.Visible = true;                       
                         grdterms.Visible = false;                         
                         grdbillpo.Visible = false;
                     }
                     else if (ViewState["status"].ToString() == "3")
                     {
                         btn.Visible = false;
                         grdterms.Visible = false;
                         grdbillpo.Visible = false;
                         print.Visible = true;
                     }
                     else
                     {
                         btn.Visible = false;                         
                         print.Visible = false;                        
                         grdterms.Visible = true;                         
                         grdbillpo.Visible = true;
                     }
                     trdobtns.Visible = false;
                     trpobtns.Visible = true;
                     tblpo.Visible = true;
                     tbldo.Visible = false;
                     FillGrid1();
                     pname();
                 }
                 else
                 {
                     trdobtns.Visible = true;
                     trpobtns.Visible = false;
                     tblpo.Visible = false;
                     tbldo.Visible = true;
                     FillGridpo();
                     pname();
                 }
                 poppo.Show();
             }
             catch (Exception ex)
             {
                 Utilities.CatchException(ex);
                 JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
             }
         }
         public int j = 0;
         public int k = 1;
         protected void gvusers_RowDataBound(object sender, GridViewRowEventArgs e)
         {
             try
             {

                 if (e.Row.RowType == DataControlRowType.DataRow)
                 {
                     if (ViewState["Curtblroles"] != null)
                     {
                         DataTable Objdt = ViewState["Curtblroles"] as DataTable;
                         if (Objdt.Rows[j][0].ToString() != "")
                         {
                             da = new SqlDataAdapter("select (first_name+'  '+middle_name+'  '+last_name)as name,USER_NAME from Employee_Data where USER_NAME='" + Objdt.Rows[j][0].ToString() + "'", con);
                             da.Fill(ds, "userroles");
                             if (k == 1)
                             {
                                 e.Row.Cells[1].Text = "Purchase Manager";
                                 e.Row.Cells[0].Text = "Prepared By";
                             }
                             if (k == 2)
                             {
                                 e.Row.Cells[1].Text = "Chief Material Controller";
                                 e.Row.Cells[0].Text = "Verified By";
                             }
                             //if (k == 3)
                             //{
                             //    e.Row.Cells[1].Text = "HoAdmin";
                             //    e.Row.Cells[0].Text = "Verified By";
                             //}
                             //e.Row.Cells[1].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                             e.Row.Cells[2].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                             //e.Row.Cells[3].Text = ds.Tables["userroles"].Rows[j].ItemArray[2].ToString();

                         }
                     }
                     j = j + 1;
                     k = k + 1;
                 }

             }
             catch (Exception ex)
             {
                 JavaScript.UPAlert(Page, Utilities.CatchException(ex));
             }

         }
         private void FillGrid1()
         {
             try
             {
                 da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
                 da.Fill(ds, "po_idp");
                 if (Convert.ToInt32(ds.Tables["po_idp"].Rows[0][0].ToString()) > 1878)
                     //da = new SqlDataAdapter("Select vendor_name,po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],ref_no,ref_date,remarks,item_name,specification,units,Replace(j.quantity,'.00','')quantity,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno from (Select item_code,item_name,specification,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i  join (Select item_code,quantity,vendor_name,p.po_no,po_date,ref_no,ref_date,remarks,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno from  Po_details il join purchase_details p on il.Po_no=p.Po_no  where  p.Po_no='" + ViewState["po_no"].ToString() + "')j  on i.item_code=j.item_code", con);
                     da = new SqlDataAdapter("Select vendor_name,po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],ref_no,ref_date,remarks,item_name,specification,units,Replace(j.quantity,'.00','')quantity,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno,j.item_code,cast(j.New_Basicprice as decimal(10,2))as New_Basicprice,cast(Quoted_price as decimal(10,2))as Quoted_price,cast(basic_price as decimal(10,2))as basic_price,cast(Amt as decimal(10,2))as Amt from (Select item_code,item_name,specification,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i  join (Select item_code,quantity,vendor_name,p.po_no,po_date,ref_no,ref_date,remarks,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno,New_Basicprice,Quoted_Price,basic_price,(New_Basicprice*Quantity)as Amt from  Po_details il join purchase_details p on il.Po_no=p.Po_no  where  p.Po_no='" + ViewState["po_no"].ToString() + "')j  on i.item_code=j.item_code", con);
                 else
                    // da = new SqlDataAdapter("Select vendor_name,po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],ref_no,ref_date,remarks,item_name,specification,units,Replace(j.quantity,'.00','')quantity,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno from (Select item_code,item_name,specification,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i  join (Select item_code,quantity,vendor_name,po_no,po_date,ref_no,ref_date,remarks,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno from  indent_list il join purchase_details p on il.indent_no=p.indent_no where  p.indent_no='" + ViewState["indentno"].ToString() + "' and quantity!='0' )j  on i.item_code=j.item_code", con);
                     da = new SqlDataAdapter("Select vendor_name,po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],ref_no,ref_date,remarks,item_name,specification,units,Replace(j.quantity,'.00','')quantity,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno,j.item_code,cast(j.New_Basicprice as decimal(10,2))as New_Basicprice,cast(Quoted_price as decimal(10,2))as Quoted_price,cast(basic_price as decimal(10,2))as basic_price,cast(Amt as decimal(10,2))as Amt from (Select item_code,item_name,specification,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i  join (Select item_code,quantity,vendor_name,po_no,po_date,ref_no,ref_date,remarks,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno,New_Basicprice,Quoted_Price,basic_price,(New_Basicprice*Quantity)as Amt from  indent_list il join purchase_details p on il.indent_no=p.indent_no where  p.indent_no='" + ViewState["indentno"].ToString() + "' and quantity!='0' )j  on i.item_code=j.item_code", con);
                 da.Fill(ds, "pop");
                 if (ds.Tables["pop"].Rows.Count > 0)
                 {
                     da = new SqlDataAdapter("Select address from vendor where vendor_name='" + ds.Tables["pop"].Rows[0].ItemArray[0].ToString() + "' and vendor_type='Supplier'", con);
                     da.Fill(ds, "addressp");
                     if (ds.Tables["addressp"].Rows.Count > 0)
                     {

                         CascadingDropDown1.SelectedValue = ds.Tables["pop"].Rows[0].ItemArray[0].ToString();
                         hfvendor.Value = ds.Tables["pop"].Rows[0].ItemArray[0].ToString();

                         //lblname.Text = ds.Tables["po"].Rows[0].ItemArray[0].ToString() + ","+"</br>" + ds.Tables["address"].Rows[0].ItemArray[0].ToString();
                         string output = ds.Tables["pop"].Rows[0].ItemArray[0].ToString() + Environment.NewLine + ds.Tables["addressp"].Rows[0].ItemArray[0].ToString();
                         lblnamepo.Text = output;
                         txtponopo.Text = ds.Tables["pop"].Rows[0].ItemArray[1].ToString();
                         txtpodatepo.Text = ds.Tables["pop"].Rows[0].ItemArray[2].ToString();
                         txtrefnopo.Text = ds.Tables["pop"].Rows[0].ItemArray[3].ToString();
                         txtrefdatepo.Text = ds.Tables["pop"].Rows[0].ItemArray[4].ToString();
                         //txtrecievedccpo.Text = ds.Tables["pop"].Rows[0].ItemArray[10].ToString();
                         //txtrecieveddatepo.Text = ds.Tables["pop"].Rows[0].ItemArray[11].ToString();
                         lblSaddresspo.Text = ds.Tables["pop"].Rows[0].ItemArray[12].ToString();
                         lblSaddress2po.Text = ds.Tables["pop"].Rows[0].ItemArray[13].ToString();
                         lblCpersonpo.Text = ds.Tables["pop"].Rows[0].ItemArray[14].ToString();
                         lblMobileNumpo.Text = ds.Tables["pop"].Rows[0].ItemArray[15].ToString();
                         lblinvoiceAddpo.Text = ds.Tables["pop"].Rows[0].ItemArray[16].ToString();
                         lblinvoiceAdd2po.Text = ds.Tables["pop"].Rows[0].ItemArray[17].ToString();
                         lblinvgstpo.Text = ds.Tables["pop"].Rows[0].ItemArray[18].ToString();
                         lblinvMobileNumpo.Text = ds.Tables["pop"].Rows[0].ItemArray[19].ToString();
                         grdbillpo.DataSource = ds.Tables["pop"];
                         grdbillpo.DataBind();
                         string remarks = "";
                         remarks = ds.Tables["pop"].Rows[0].ItemArray[5].ToString();
                         da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + remarks + "','$')", con);
                         da.Fill(ds, "split");
                         grdterms.DataSource = ds.Tables["split"];
                         grdterms.DataBind();
                         ViewState["Curtblterm"] = ds.Tables["split"];
                     }
                 }


             }
             catch (Exception ex)
             {
                 Utilities.CatchException(ex);
                 JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
             }
         }
         private void FillGridpo()
         {
             try
             {
                 da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + ViewState["po_no"].ToString() + "'  ", con);
                 da.Fill(ds, "po_id");
                 if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878)
                     da = new SqlDataAdapter("Select vendor_name,po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],ref_no,ref_date,remarks,item_name,specification,units,Replace(j.quantity,'.00','')quantity,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno from (Select item_code,item_name,specification,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i  join (Select item_code,quantity,vendor_name,p.po_no,po_date,ref_no,ref_date,remarks,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno from  Po_details il join purchase_details p on il.Po_no=p.Po_no  where  p.Po_no='" + ViewState["po_no"].ToString() + "')j  on i.item_code=j.item_code", con);
                 else
                     da = new SqlDataAdapter("Select vendor_name,po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],ref_no,ref_date,remarks,item_name,specification,units,Replace(j.quantity,'.00','')quantity,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno from (Select item_code,item_name,specification,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i  join (Select item_code,quantity,vendor_name,po_no,po_date,ref_no,ref_date,remarks,Recieved_cc,Recieved_date,SiteAddress,SiteAddress2,ContactPerson,mobilenum,InvAddress,InvAddress2,Invgstno,InvMobileno from  indent_list il join purchase_details p on il.indent_no=p.indent_no where  p.indent_no='" + ViewState["indentno"].ToString() + "' and quantity!='0' )j  on i.item_code=j.item_code", con);
                 da.Fill(ds, "po");
                 if (ds.Tables["po"].Rows.Count > 0)
                 {
                     da = new SqlDataAdapter("Select address from vendor where vendor_name='" + ds.Tables["po"].Rows[0].ItemArray[0].ToString() + "' and vendor_type='Supplier'", con);
                     da.Fill(ds, "address");
                     if (ds.Tables["address"].Rows.Count > 0)
                     {

                         //txtaddress.Text =ds.Tables["info"].Rows[0].ItemArray[0].ToString()+"\n"+ds.Tables["info"].Rows[0].ItemArray[1].ToString(); 

                         //lbladdress.Text = ds.Tables["address"].Rows[0].ItemArray[0].ToString();
                         CascadingDropDown1.SelectedValue = ds.Tables["po"].Rows[0].ItemArray[0].ToString();
                         hfvendor.Value = ds.Tables["po"].Rows[0].ItemArray[0].ToString();

                         //lblname.Text = ds.Tables["po"].Rows[0].ItemArray[0].ToString() + ","+"</br>" + ds.Tables["address"].Rows[0].ItemArray[0].ToString();
                         string output = ds.Tables["po"].Rows[0].ItemArray[0].ToString() + Environment.NewLine + ds.Tables["address"].Rows[0].ItemArray[0].ToString();
                         lblname.Text = output;
                         txtpono.Text = ds.Tables["po"].Rows[0].ItemArray[1].ToString();
                         txtpodate.Text = ds.Tables["po"].Rows[0].ItemArray[2].ToString();
                         txtrefno.Text = ds.Tables["po"].Rows[0].ItemArray[3].ToString();
                         txtrefdate.Text = ds.Tables["po"].Rows[0].ItemArray[4].ToString();
                         txtremarks.Text = ds.Tables["po"].Rows[0].ItemArray[5].ToString();
                         txtrecievedcc.Text = ds.Tables["po"].Rows[0].ItemArray[10].ToString();
                         txtrecieveddate.Text = ds.Tables["po"].Rows[0].ItemArray[11].ToString();
                         lblSaddress.Text = ds.Tables["po"].Rows[0].ItemArray[12].ToString();
                         lblSaddress2.Text = ds.Tables["po"].Rows[0].ItemArray[13].ToString();
                         lblCperson.Text = ds.Tables["po"].Rows[0].ItemArray[14].ToString();
                         lblMobileNum.Text = ds.Tables["po"].Rows[0].ItemArray[15].ToString();
                         lblinvoiceAdd.Text = ds.Tables["po"].Rows[0].ItemArray[16].ToString();
                         lblinvoiceAdd2.Text = ds.Tables["po"].Rows[0].ItemArray[17].ToString();
                         lblinvgst.Text = ds.Tables["po"].Rows[0].ItemArray[18].ToString();
                         lblinvMobileNum.Text = ds.Tables["po"].Rows[0].ItemArray[19].ToString();
                         grdbill.DataSource = ds.Tables["po"];
                         grdbill.DataBind();
                     }
                 }


             }
             catch (Exception ex)
             {
                 Utilities.CatchException(ex);
                 JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
             }
         }
         protected void grdviewpo_PageIndexChanging(object sender, GridViewPageEventArgs e)
         {

             grdviewpo.PageIndex = e.NewPageIndex;

            
                 filtergrid();
            
                 FillGridviewpo();
         
         }
         public void filtergrid()
         {
             try
             {
                 string Condition = "";
                 //grdviewpo.Dispose();
                 //if (ddlyear.SelectedIndex != 0)
                 //{
                 //    if (ddlMonth.SelectedIndex != 0)
                 //    {
                 //        string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                 //        Condition = Condition + " and datepart(mm,po_date)=" + ddlMonth.SelectedValue + " and datepart(yy,po_date)=" + yy;
                 //    }
                 //    Condition = Condition + "and convert(datetime,po_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                 //}
                 //if (Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Project Manager")
                 //{
                 //    if (ddlcccode.SelectedValue != "")
                 //    {
                 //        Condition = Condition + " and cc_code='" + ddlcccode.SelectedValue + "'";
                 //    }
                 //}
                 //else
                 //{
                 //    Condition = Condition + "and cc_code='" + Session["cc_code"].ToString() + "'";
                 //}

         if (Session["roles"].ToString() == "SuperAdmin")
                     da = new SqlDataAdapter("SELECT  po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,status from purchase_details p where (status='1A' or status='2' or status='3')" + Condition + "  order by status asc", con);

                 da.Fill(ds, "indents");

                 grdviewpo.DataSource = ds.Tables["indents"];
                 grdviewpo.DataBind();
                 grdviewpo.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                 poppo.Hide();
             }
             catch (Exception ex)
             {
                 Utilities.CatchException(ex);
                 JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
             }

         }
         protected void btnmdlupdpo_Click(object sender, EventArgs e)
         {
             string MDescs = "";
             try
             {
                 foreach (GridViewRow record in grdterms.Rows)
                 {
                     if ((record.FindControl("chkSelectterms") as CheckBox).Checked)
                     {
                         TextBox txtterm = (TextBox)record.FindControl("txtterms");
                         string termdesc = txtterm.Text;
                         if (termdesc != "")
                         {
                             MDescs = MDescs + termdesc + "$";
                         }
                     }
                 }
                 cmd = new SqlCommand("sp_vendorPoNew", con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@vendorname", ddlvendor.SelectedItem.Text);
                 cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                 cmd.Parameters.AddWithValue("@pono", ViewState["po_no"].ToString());
                 cmd.Parameters.AddWithValue("@Terms", MDescs);
                 cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                 con.Open();
                 //string msg = "";
                 string msg = cmd.ExecuteScalar().ToString();
                 con.Close();
                 if (msg == "Verified")
                     JavaScript.UPAlertRedirect(Page, msg, "VendorPO.aspx");
                 else
                 {
                     JavaScript.UPAlert(Page, msg);
                 }

             }

             catch (Exception ex)
             {
                 Utilities.CatchException(ex);
             }
         }
         protected void grdviewpo_RowDataBound(object sender, GridViewRowEventArgs e)
         {
             //--- For Paging ---------
             GridViewRow row = grdviewpo.BottomPagerRow;

             if (row == null)
             {
                 return;
             }

             //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
             Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
             Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

             //if (lblPages != null)
             //{
             lblCurrent.Text = grdviewpo.PageCount.ToString();
             //}

             //if (lblCurrent != null)
             //{
             int currentPage = grdviewpo.PageIndex + 1;
             lblPages.Text = currentPage.ToString();

             //-- For First and Previous ImageButton
             if (grdviewpo.PageIndex == 0)
             {
                 ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
                 ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnFirst")).Visible = false;

                 ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
                 ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnPrev")).Visible = false;



             }

             //-- For Last and Next ImageButton
             if (grdviewpo.PageIndex + 1 == grdviewpo.PageCount)
             {
                 ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnLast")).Enabled = false;
                 ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnLast")).Visible = false;

                 ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                 ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnNext")).Visible = false;


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
        int iCurrentIndex = grdviewpo.PageIndex;

        switch (e.CommandArgument.ToString().ToLower())
        {
            case "first":
                grdviewpo.PageIndex = 0;
                break;
            case "prev":
                if (grdviewpo.PageIndex != 0)
                {
                    grdviewpo.PageIndex = iCurrentIndex - 1;
                }
                break;
            case "next":
                grdviewpo.PageIndex = iCurrentIndex + 1;
                break;
            case "last":
                grdviewpo.PageIndex = grdviewpo.PageCount;
                break;
        }
    }
    public void pname()
    {
        try
        {
            da = new SqlDataAdapter("select first_name+' ' +last_name from employee_data r join user_roles u on r.User_Name=u.User_Name where u.Roles ='PurchaseManager'", con);
            da.Fill(ds, "pnameinfo");
            if (ds.Tables["pnameinfo"].Rows.Count > 0)
            {

                lblpurchasemanagername.Text = ds.Tables["pnameinfo"].Rows[0][0].ToString();
                lblpurchasemanagernamepo.Text = ds.Tables["pnameinfo"].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_vendorPo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vendorname", ddlvendor.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@pono", ViewState["po_no"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Verified")
                JavaScript.UPAlertRedirect(Page, msg, "VendorPO.aspx");
            else
            {
                JavaScript.UPAlert(Page, msg);
            }

            ////SqlCommand cmd = new SqlCommand();
            ////cmd.Connection = con;
            ////if (ddlvendor.SelectedItem.Text == "Select Vendor")
            ////{
            ////    if (Session["roles"].ToString() == "Chief Material Controller")
            ////        cmd.CommandText = "if((select isnull(sum(amount),0) from purchase_details where [po_no]='" + ViewState["po_no"].ToString() + "') < (select limit from [voucher limits] where id='4')) Begin Update [Purchase_details] set status='2' where po_no='" + ViewState["po_no"].ToString() + "' end else begin Update [Purchase_details] set status='1A' where po_no='" + ViewState["po_no"].ToString() + "' end";
            ////    else
            ////        cmd.CommandText = "Update [Purchase_details] set status='2' where po_no='" + ViewState["po_no"].ToString() + "'";
            ////}
            ////else
            ////{
            ////    if (Session["roles"].ToString() == "Chief Material Controller")
            ////        cmd.CommandText = "if((select isnull(sum(amount),0) from purchase_details where [po_no]='" + ViewState["po_no"].ToString() + "') < (select limit from [voucher limits] where id='4')) Begin Update [Purchase_details] set status='2' where po_no='" + ViewState["po_no"].ToString() + "' end else begin Update [Purchase_details] set status='1A', vendor_name='" + ddlvendor.SelectedItem.Text + "' where po_no='" + ViewState["po_no"].ToString() + "' end";
            ////    else
            ////        cmd.CommandText = "Update [Purchase_details] set vendor_name='" + ddlvendor.SelectedItem.Text + "', status='2' where po_no='" + ViewState["po_no"].ToString() + "'";


            ////}

            ////con.Open();
            ////int i = cmd.ExecuteNonQuery();
            ////con.Close();
            ////if (i == 1)
            ////    JavaScript.UPAlertRedirect(Page, "Sucessfull", "VendorPO.aspx");
            ////else
            ////    JavaScript.UPAlertRedirect(Page, "Failed", "VendorPO.aspx");
            ////lblname.Text = ddlvendor.SelectedItem.Text;
            ////if (ddlvendor.SelectedItem.Text != "Select Vendor")
            ////    lbladdress.Text = Session["address"].ToString();
            ////Session.Remove("address");
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void grdviewpo_DataBound(object sender, EventArgs e)
    {
        //--- For Paging ---------
        GridViewRow row = grdviewpo.BottomPagerRow;

        if (row == null)
        {
            return;
        }

        //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
        Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
        Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

        //if (lblPages != null)
        //{
        lblCurrent.Text = grdviewpo.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = grdviewpo.PageIndex + 1;
        lblPages.Text = currentPage.ToString();

        //-- For First and Previous ImageButton
        if (grdviewpo.PageIndex == 0)
        {
            ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnPrev")).Visible = false;



        }

        //-- For Last and Next ImageButton
        if (grdviewpo.PageIndex + 1 == grdviewpo.PageCount)
        {
            ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnLast")).Enabled = false;
            ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnLast")).Visible = false;

            ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            ((ImageButton)grdviewpo.BottomPagerRow.FindControl("btnNext")).Visible = false;


        }
    }
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
            filtergrid();
            FillGridviewpo();
    }
    protected void grdviewpo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd = new SqlCommand("Supplierpo_Reject_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IndentNo", grdviewpo.DataKeys[e.RowIndex]["indent_no"].ToString());
            cmd.Parameters.AddWithValue("@PONO", grdviewpo.DataKeys[e.RowIndex]["po_no"].ToString());
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@Type", grdviewpo.DataKeys[e.RowIndex]["Type"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            JavaScript.UPAlertRedirect(Page, msg, "VendorPO.aspx");           
            con.Close();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
    private decimal TotalAmt = (decimal)0.0;
    protected void grdbillpo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = 0;
            e.Row.Cells[1].Visible = false;
            StringBuilder sDetails = new StringBuilder();
            sDetails.Append("<span><h5>Price Details</h5></span>");
            da1 = new SqlDataAdapter("select top 5 Vendor_name,cc_code,isnull(basic_price,0) as basic_price  from [Recieved Items] ri join Purchase_details pd on ri.PO_no=pd.Po_no where Item_code='" + e.Row.Cells[2].Text + "' and basic_price !=0 order by ri.Id desc", con);
            da1.Fill(ds, "data");
            DataTable dt = new DataTable();
            da1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                sDetails.Append("<div><p><strong>Vendor Name   ||   CC Code   ||  Amount </strong></p></div>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sDetails.Append("<p>" + dt.Rows[i]["Vendor_name"] + "   ||   " + dt.Rows[i]["cc_code"] + "   ||   " + dt.Rows[i]["basic_price"].ToString().Replace(".0000", ".00") + "</p>");
                    rowIndex++;
                }
                // BIND MOUSE EVENT (TO CALL JAVASCRIPT FUNCTION), WITH EACH ROW OF THE GRID.                  
                e.Row.Cells[2].Attributes.Add("onmouseover", "MouseEvents(this, event, '" + sDetails.ToString() + "')");
                e.Row.Cells[2].Attributes.Add("onmouseout", "MouseEvents(this, event, '" +
                DataBinder.Eval(e.Row.DataItem, "item_code").ToString() + "')");
            }
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amt"));

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[10].Text = String.Format("{0:N2}", TotalAmt);
            e.Row.Cells[10].Font.Bold = true;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;

        }
    }
    #region Terms and Conditions start
    private void AddNewRowterm()
    {
        int rowIndex = 0;

        if (ViewState["Curtblterm"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterm"];
            DataRow drCurrentRow = null;
            if (dtt.Rows.Count > 0)
            {
                for (int i = 1; i <= dtt.Rows.Count; i++)
                {
                    TextBox txtterms = (TextBox)grdterms.Rows[rowIndex].Cells[2].FindControl("txtterms");
                    drCurrentRow = dtt.NewRow();
                    dtt.Rows[i - 1]["splitdata"] = txtterms.Text;
                    rowIndex++;
                }
                dtt.Rows.Add(drCurrentRow);
                ViewState["Curtblterm"] = dtt;
                grdterms.DataSource = dtt;
                grdterms.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState Value is Null");
        }
        SetOldDataterm();
    }
    private void SetOldDataterm()
    {
        int rowIndex = 0;
        if (ViewState["Curtblterm"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterm"];
            if (dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)grdterms.Rows[rowIndex].Cells[0].FindControl("chkSelectterms");
                    TextBox txtterms = (TextBox)grdterms.Rows[rowIndex].Cells[2].FindControl("txtterms");
                    txtterms.Text = dtt.Rows[i]["splitdata"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    protected void btnAddterm_Click(object sender, EventArgs e)
    {
        AddNewRowterm();
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
    }
    protected void grdterms_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblterm"] != null)
            {
                DataTable dtt = (DataTable)ViewState["Curtblterm"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dtt.Rows.Count > 1)
                {
                    dtt.Rows.Remove(dtt.Rows[rowIndex]);
                    drCurrentRow = dtt.NewRow();
                    ViewState["Curtblterm"] = dtt;
                    grdterms.DataSource = dtt;
                    grdterms.DataBind();
                    for (int i = 0; i < grdterms.Rows.Count - 1; i++)
                    {
                        grdterms.Rows[i].Cells[1].Text = Convert.ToString(i + 1);
                    }
                    SetOldDataterm();
                }
            }
            //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
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
    #endregion Terms and Conditions End
}



