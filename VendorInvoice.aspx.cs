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

public partial class VendorInvoice : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 8);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            checkapprovals();
            txttotal.Attributes.Add("readonly", "1");
            txtnetAmount.Attributes.Add("readonly", "2");
            txtexduty.Enabled = false;
            txtEdc.Enabled = false;
            txtHEdc.Enabled = false;
            txtvatamt.Enabled = false;
            ddlExcno.Enabled = false;
            ddlvatno.Enabled = false;
        
          
        }
        

    }
    public void fillexcise(string s)
    {
        try
        {

            da = new SqlDataAdapter("select ServiceTaxno as Excise_no from ServiceTaxMaster where Status='3' union all select Excise_no from ExciseMaster where Status='3'", con);

            da.Fill(ds, "Excise_no");

            ddlExcno.DataValueField = "Excise_no";
            ddlExcno.DataTextField = "Excise_no";
            ddlExcno.DataSource = ds.Tables["Excise_no"];
            ddlExcno.DataBind();
            ddlExcno.Items.Insert(0, "Select");



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
    protected void ddlmrr_SelectedIndexChanged(object sender, EventArgs e)
     {
        try
        {
            if (ddlmrr.SelectedItem.Text != "Select MRR NO")
            {
                da = new SqlDataAdapter("Select vendor_id,i.* from (Select vendor_name,p.po_no,(Select CC_SubType from cost_center where cc_code=p.cc_code) as cc_type from purchase_details p join mr_report m on p.po_no=m.po_no where mrr_no='" + ddlmrr.SelectedItem.Text + "') i join vendor v on v.vendor_name=i.vendor_name where v.status='2'", con);
                da.Fill(ds, "mrrinfo");
                if (ds.Tables["mrrinfo"].Rows.Count > 0)
                {
                    da = new SqlDataAdapter("Select distinct dca_code,subdca_code from (Select item_code,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code from [Recieved Items]where po_no='" + ds.Tables["mrrinfo"].Rows[0].ItemArray[2].ToString() + "')r on i.item_code=substring(r.item_code,1,8)", con);
                    da.Fill(ds, "filldca");
                    txtvendorid.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[0].ToString();
                    txtvendorname.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[1].ToString();
                    txtpo.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[2].ToString();
                    txtdca.Text = ds.Tables["filldca"].Rows[0].ItemArray[0].ToString();
                    txtsubdca.Text = ds.Tables["filldca"].Rows[0].ItemArray[1].ToString();
                    fillgrid();
                    fillexcise(ds.Tables["mrrinfo"].Rows[0].ItemArray[3].ToString()); 
                  
                }
            }
            else
            {
              
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillgrid()
    {
        try
        {
            da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + txtpo.Text + "'  ", con);
            da.Fill(ds, "po_id");
            if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878)        
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + txtpo.Text + "' and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
            else
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + txtpo.Text + "' and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillcmc");
                gridcmc.DataSource = ds.Tables["fillcmc"];
                gridcmc.DataBind();
                if (excisecheck.Checked == true)
                {
                    txtexduty.Enabled = true;
                    txtEdc.Enabled = true;
                    txtHEdc.Enabled = true;
                    ddlExcno.Enabled = true;

                }
                if (excisecheck.Checked == false)
                {
                    txtexduty.Enabled = false;
                    txtEdc.Enabled = false;
                    txtHEdc.Enabled = false;
                    ddlExcno.Enabled = false;
                }
                if (VATcheck.Checked == true)
                {
                    txtvatamt.Enabled = true;
                    ddlvatno.Enabled = true;
                }
                if (VATcheck.Checked == false)
                {
                    txtvatamt.Enabled = false;
                    ddlvatno.Enabled = false;
                }

        }



        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    
   
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int i=0;
            foreach (GridViewRow record in gridcmc.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {


                    cmd.Connection = con;
                    string id = gridcmc.DataKeys[record.RowIndex]["id"].ToString();
                    string amount = (record.FindControl("txtbasic") as TextBox).Text;

                    string staxamt = (record.FindControl("txtsalestax") as TextBox).Text;
                    if (VATcheck.Checked == false)
                        cmd.CommandText = "Update [Recieved Items] Set basic_price='" + amount + "', STaxPercent='" + staxamt + "' where id='" + id + "'";
                    if (VATcheck.Checked == true)
                        cmd.CommandText = "Update [Recieved Items] Set basic_price='" + amount + "' where id='" + id + "'";

                    con.Open();
                    i = Convert.ToInt32(cmd.ExecuteNonQuery());
                    i=i+1;
                    con.Close();

                }
                
            }
            da = new SqlDataAdapter(" select p.CC_code from Purchase_details p join MR_Report m on p.Po_no=m.PO_no where MRR_no='" + ddlmrr.SelectedItem.Text + "'", con);
            da.Fill(ds, "CCinfo");
            string cc_code = ds.Tables["CCinfo"].Rows[0][0].ToString();


            if (i > 0)
            {
                string result = "";
                if (Session["roles"].ToString() == "Central Store Keeper")
                {

                    cmd1 = new SqlCommand("checkbudget", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Clear();
                    cmd1.Parameters.AddWithValue("@Total", txttotal.Text);
                    cmd1.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                    cmd1.Parameters.AddWithValue("@Frieght", txtfre.Text);
                    cmd1.Parameters.AddWithValue("@Insurance", txtinsurance.Text);
                    cmd1.Parameters.AddWithValue("@salestax", txttax.Text);
                    cmd1.Parameters.AddWithValue("@PoNo", txtpo.Text);
                    cmd1.Parameters.AddWithValue("@CCCode", cc_code);
                    cmd1.Parameters.AddWithValue("@DCA_Code", txtdca.Text);
                    cmd1.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                    cmd1.Parameters.AddWithValue("@type", "2");
                    cmd1.Parameters.AddWithValue("@vtype", "1");

                    cmd1.Connection = con;
                    con.Open();
                    result = cmd1.ExecuteScalar().ToString();

                    con.Close();
                }
                else if (Session["roles"].ToString() == "StoreKeeper")
                {
                    result = "False";
                }

                if (result == "sufficent" || result == "False")
                {
                cmd.CommandText = "sp_pending_invoice";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VendorId", txtvendorid.Text);
                cmd.Parameters.AddWithValue("@Name", txtvendorname.Text);
                cmd.Parameters.AddWithValue("@invoiceno", txtin.Text);
                cmd.Parameters.AddWithValue("@mrrno", ddlmrr.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CCCode", cc_code);
                cmd.Parameters.AddWithValue("@DCA_Code", txtdca.Text);
                cmd.Parameters.AddWithValue("@Sub_DCA", txtsubdca.Text);
                if (ddlExcno.SelectedValue != "Select")
                    cmd.Parameters.AddWithValue("@Exciseno", ddlExcno.SelectedValue);
                if (ddlvatno.SelectedValue != "Select")
                    cmd.Parameters.AddWithValue("@VATno", ddlvatno.SelectedValue);
                cmd.Parameters.AddWithValue("@ExciseDuty", txtexduty.Text);
                cmd.Parameters.AddWithValue("@EDcess", txtEdc.Text);
                cmd.Parameters.AddWithValue("@HEDcess", txtHEdc.Text);
                cmd.Parameters.AddWithValue("@Frieght", txtfre.Text);
                cmd.Parameters.AddWithValue("@Insurance", txtinsurance.Text);
                cmd.Parameters.AddWithValue("@Advance", txtAdvance.Text);
                cmd.Parameters.AddWithValue("@Hold", txthold.Text);
                cmd.Parameters.AddWithValue("@AnyOther", txtother.Text);
                cmd.Parameters.AddWithValue("@amount", txtnetAmount.Text);
                cmd.Parameters.AddWithValue("@PO_NO", txtpo.Text);
                cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                cmd.Parameters.AddWithValue("@Total", txttotal.Text);
                cmd.Parameters.AddWithValue("@description", txtindesc.Text);
                cmd.Parameters.AddWithValue("@Salestax", txttax.Text);
                cmd.Parameters.AddWithValue("@VATAmount", txtvatamt.Text);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@PaymentType", ddlvendortype.SelectedItem.Text);
                //New parameter created date 16-May-2016 Cr-ENH-MAR-010-2016 STARTS
                cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                //New parameter created date 16-May-2016 Cr-ENH-MAR-010-2016 ENDS

                cmd.Connection = con;
                con.Open();

                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "Invoice Inserted")
                    {
                    JavaScript.UPAlertRedirect(Page,msg, Request.Url.ToString());
                    }
                else
                    {
                    JavaScript.UPAlert(Page,msg);
            }
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
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void gridcmc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtbasic = (TextBox)e.Row.FindControl("txtbasic");
                 TextBox txttax = (TextBox)e.Row.FindControl("txtsalestax");
            txtbasic.Attributes.Add("onkeyup","calculate('"+e.Row.Cells[2].Text+"',this.value)");
            if (VATcheck.Checked==false)
            {
                txttax.Attributes.Add("onkeyup", "STaxcalculate('" + e.Row.Cells[2].Text + "',this.value)");
                txttax.Enabled = true;
            }
            else
            {
                txttax.Enabled = false;
            }
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
}
