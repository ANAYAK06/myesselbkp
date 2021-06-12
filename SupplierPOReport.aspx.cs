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

public partial class SupplierPOReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            costcenter();
            LoadYear();
            btnSearch.Visible = true;
            Trgvusers.Visible = false;
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

    public void costcenter()
    {
        ddlcccode.Items.Clear();
        da = new SqlDataAdapter("select pd.cc_code,(pd.cc_code+', '+ cc.cc_name)as name from Purchase_details pd join Cost_Center cc on cc.cc_code=pd.cc_code GROUP by pd.cc_code,cc.cc_name ORDER BY CASE WHEN pd.cc_code='CCC' THEN pd.cc_code end ASC ,CASE WHEN pd.cc_code!='CCC' THEN CONVERT(INT,REPLACE(pd.CC_Code,'CC-','')) END ASC", con);
        da.Fill(ds, "ccdetails");
        ddlcccode.DataTextField = "Name";
        ddlcccode.DataValueField = "cc_code";
        ddlcccode.DataSource = ds.Tables["ccdetails"];
        ddlcccode.DataBind();
        ddlcccode.Items.Insert(0, "Select");
        //ddlcccode.Items.Insert(1, "Select All");
        btnSearch.Visible = true;
    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldca.Items.Clear();
        filldca();      
    }
    public void filldca()
    {
        try
        {
            if (ddlcccode.SelectedItem.Text != "")
                da = new SqlDataAdapter("select distinct dca_code from Purchase_details pd join vendor c on pd.Vendor_name=c.vendor_name where cc_code='" + ddlcccode.SelectedValue + "'", con);
            //else
            //    da = new SqlDataAdapter("select distinct dca_code from Purchase_details pd join vendor c on pd.Vendor_name=c.vendor_name where cc_code='" + ddlcccode.SelectedValue + "'and c.vendor_id='" + ddlvendor.SelectedValue + "'", con);

            da.Fill(ds, "filldca");
            ddldca.DataTextField = "dca_code";
            ddldca.DataValueField = "dca_code";
            ddldca.DataSource = ds.Tables["filldca"];
            ddldca.DataBind();
            ddldca.Items.Insert(0, "Select DCA");
            //ddlvendor.Items.Insert(1, "Select All");
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void ddldca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldca.SelectedValue != "Select")
            {
                //da = new SqlDataAdapter("select v.vendor_id,(v.vendor_id+' , '+pd.Vendor_name)as name from vendor v join Purchase_details pd on v.vendor_name=pd.Vendor_name where pd.cc_code='" + ddlcccode.SelectedValue + "' and (pd.status='1'or pd.status='1A' or pd.status='2') and v.vendor_type='Supplier'  order by pd.Vendor_name", con);
                da = new SqlDataAdapter("select  v.vendor_id,(v.vendor_id+' , '+pd.Vendor_name)as name from vendor v join Purchase_details pd on v.vendor_name=pd.Vendor_name where pd.cc_code='" + ddlcccode.SelectedValue + "' and (pd.status='1'or pd.status='1A' or pd.status='2'or pd.status='3') and v.vendor_type='Supplier' and pd.Dca_Code='" + ddldca.SelectedValue + "' group by vendor_id,pd.Vendor_name  order by pd.Vendor_name", con);
                da.Fill(ds, "fill");
                ddlvendor.DataTextField = "Name";
                ddlvendor.DataValueField = "vendor_id";
                ddlvendor.DataSource = ds.Tables["fill"];
                ddlvendor.DataBind();
                ddlvendor.Items.Insert(0, "Select vendor");
                if (ds.Tables["fill"].Rows.Count > 0)
                {
                    ddlvendor.Items.Insert(1, "Select All");
                }
            }
            else
            {
                JavaScript.Alert("Select Cost Center");
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        //filldca();
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    condition = condition + " and datepart(mm,po_date)=" + ddlmonth.SelectedValue + " and datepart(yy,po_date)=" + yy;
                }
                else
                {
                    condition = condition + " and convert(datetime,po_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                }
            }

            if (ddlvendor.SelectedItem.Text != "Select All")
            {
                condition = condition + " and v.vendor_id='" + ddlvendor.SelectedValue + "'";
            }
            if (ddldca.SelectedItem.Text != "Select DCA")
            {
                condition = condition + " and dca_code='" + ddldca.SelectedItem.Text + "'";
            }
            ViewState["Query1"] = condition;
            fillgrid();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }


    }
    public void fillgrid()
    {
        try
        {
            if (ddlcccode.SelectedItem.Text != "Select All")
            {
                if (Ddltype.SelectedItem.Value == "1")
                    da = new SqlDataAdapter("SELECT p.Id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,p.Status,Type,dca_code,v.vendor_name,p.Approved_Users from purchase_details p join vendor v on v.vendor_name=p.vendor_name  where (p.status='1' or p.status='1A' or p.status='2' or p.status='3') and cc_code='" + ddlcccode.SelectedValue + "' " + ViewState["Query1"].ToString() + " group by p.id,po_no,[po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,p.Status,Type,dca_code,v.vendor_name,p.Approved_Users order by convert(datetime, po_date, 106) asc  ", con);
                else if (Ddltype.SelectedItem.Value == "2")
                    da = new SqlDataAdapter("SELECT p.Id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,p.Status,Type,dca_code,v.vendor_name,p.Approved_Users from purchase_details p join vendor v on v.vendor_name=p.vendor_name  where  (p.status='1' or p.status='1A' or p.status='2') and cc_code='" + ddlcccode.SelectedValue + "' " + ViewState["Query1"].ToString() + " group by p.id,po_no,[po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,p.Status,Type,dca_code,v.vendor_name,p.Approved_Users order by convert(datetime, po_date, 106) asc ", con);
                else if (Ddltype.SelectedItem.Value == "3")
                    da = new SqlDataAdapter("SELECT p.Id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,p.Status,Type,dca_code,v.vendor_name,p.Approved_Users from purchase_details p join vendor v on v.vendor_name=p.vendor_name  where  (p.status='3') and cc_code='" + ddlcccode.SelectedValue + "' " + ViewState["Query1"].ToString() + " group by p.id,po_no,[po_date],p.indent_no,ref_no,ref_date,cc_code,remarks,amount,p.Status,Type,dca_code,v.vendor_name,p.Approved_Users order by convert(datetime, po_date, 106) asc ", con);
            }
            else if (ddlcccode.SelectedItem.Text == "Select All")
            {
                    //if (Ddltype.SelectedItem.Value == "1")
                    //    da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0)+isnull(amended_amount,0))[po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,s.remarks,s.status from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,status,vendor_id from sppo where status in('3','Closed','Closed1','Closed2','Closed3') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono ) where cc_code IN (Select cu.cc_code from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New')) " + ViewState["Query1"].ToString() + " ", con);
                    //else if (Ddltype.SelectedItem.Value == "2")
                    //    da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0)+isnull(amended_amount,0))[po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,s.remarks,s.status from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,status,vendor_id from sppo where status in('3','Closed','Closed1','Closed2') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono ) where cc_code IN (Select cu.cc_code from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New')) " + ViewState["Query1"].ToString() + "  ", con);
                    //else if (Ddltype.SelectedItem.Value == "3")
                    //    da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0)+isnull(amended_amount,0))[po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,s.remarks,s.status from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,status,vendor_id from sppo where status in('Closed3') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono )  where cc_code IN (Select cu.cc_code from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New')) " + ViewState["Query1"].ToString() + "", con);

            }


            da.Fill(ds, "vendor");
            if (ds.Tables["vendor"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["vendor"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    private decimal MainAmount = (decimal)0.0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[9].Text == "1" || e.Row.Cells[9].Text == "1A")
                {
                    //e.Row.Cells[0].Enabled = false;
                    print.Visible = false;
                }
                else
                {
                    //e.Row.Cells[0].Enabled = true;
                    print.Visible = true;
                }
                if (e.Row.Cells[9].Text == "1")
                {
                    e.Row.Cells[9].Text = "Under Process";
                    e.Row.Cells[10].Text = "Waiting for CMC Approval";
                }
                else if (e.Row.Cells[9].Text == "1A")
                {
                    e.Row.Cells[9].Text = "Under Process";
                    e.Row.Cells[10].Text = "Waiting for SA Approval";
                }
                else if (e.Row.Cells[9].Text == "2")
                {
                    e.Row.Cells[9].Text = "Running";
                    e.Row.Cells[10].Text = "Waiting For MRR";
                }
                else if (e.Row.Cells[9].Text == "3")
                {
                    e.Row.Cells[9].Text = "Closed";
                    e.Row.Cells[10].Text = "MRR Prepared";
                }
                MainAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "amount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = String.Format("Rs. {0:#,##,##,###.00}", MainAmount);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["po_no"] = GridView1.DataKeys[GridView1.SelectedIndex].Values["po_no"].ToString() ;
            ViewState["indentno"] = GridView1.DataKeys[GridView1.SelectedIndex].Values["indent_no"].ToString();
            string type = GridView1.DataKeys[GridView1.SelectedIndex].Values["type"].ToString();
            hfpotype.Value = type;
            if (GridView1.DataKeys[GridView1.SelectedIndex].Values["Approved_Users"].ToString() != "")
            {
                Trgvusers.Visible = true;
                string rolesamend = GridView1.DataKeys[GridView1.SelectedIndex].Values["Approved_Users"].ToString().Replace("'", " ");
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
            if (GridView1.DataKeys[GridView1.SelectedIndex].Values["type"].ToString() != "PO")
            {
                FillGridpo();
                pname();
                tblpo.Visible = false;
                tbldo.Visible = true;
                if (GridView1.SelectedRow.Cells[9].Text == "Under Process")
                {
                    print.Visible = false;
                }
                else
                {
                    print.Visible = true;
                }
            }
            else if (GridView1.DataKeys[GridView1.SelectedIndex].Values["type"].ToString() == "PO")
            {
                FillGrid1();
                pname();
                tblpo.Visible = true;
                tbldo.Visible = false;
                if (GridView1.SelectedRow.Cells[9].Text == "Under Process")
                {
                    print.Visible = false;
                }
                else
                {
                    print.Visible = true;
                }
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
                        if (k == 3)
                        {
                            e.Row.Cells[1].Text = "Super Admin";
                            e.Row.Cells[0].Text = "Approved By";
                        }
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
                    grdbillpoprint.DataSource = ds.Tables["pop"];
                    grdbillpoprint.DataBind();
                    string remarks = "";
                    remarks = ds.Tables["pop"].Rows[0].ItemArray[5].ToString();
                    da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + remarks + "','$')", con);
                    da.Fill(ds, "split");
                    grdtermsprint.DataSource = ds.Tables["split"];
                    grdtermsprint.DataBind();
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
    private decimal TotalAmt = (decimal)0.0;
    protected void grdbillpoprint_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amt"));

        }        
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Text = String.Format("{0:N2}", TotalAmt);
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

        }
    }
}