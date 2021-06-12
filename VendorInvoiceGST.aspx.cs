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
using System.Text;

public partial class VendorInvoiceGST : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataAdapter da1 = new SqlDataAdapter();

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

        if (!IsPostBack)
        {
            checkapprovals();
            txtnetAmount.Attributes.Add("readonly", "2");
            tdddlanyotherdcas.Visible = false;
            tranyothergrid.Visible = false;
            tdaddanyother.Visible = false;
            tddeduction.Visible = false;
            tddedadd.Visible = false;
            trtransportheader.Visible = false;
            trtransportlbl.Visible = false;
            trtransportdata.Visible = false;
            txttcsamount.Enabled = false;
            clear();
        }
    }
    protected void ddlmrr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlmrr.SelectedItem.Text != "")
            {
                clear();
                da = new SqlDataAdapter("Select vendor_id,i.* from (Select vendor_name,p.po_no,(Select CC_SubType from cost_center where cc_code=p.cc_code) as cc_type,p.cc_code from purchase_details p join mr_report m on p.po_no=m.po_no where mrr_no='" + ddlmrr.SelectedItem.Text + "') i join vendor v on v.vendor_name=i.vendor_name  where v.status='2' and vendor_type='Supplier'", con);
                da.Fill(ds, "mrrinfo");
                if (ds.Tables["mrrinfo"].Rows.Count > 0)
                {
                    da = new SqlDataAdapter("Select distinct dca_code,subdca_code,r.Item_code from (Select item_code,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code from [Recieved Items]where po_no='" + ds.Tables["mrrinfo"].Rows[0].ItemArray[2].ToString() + "')r on i.item_code=substring(r.item_code,1,8)", con);
                    da.Fill(ds, "filldca");
                    txtvendorid.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[0].ToString();
                    txtvendorname.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[1].ToString();
                    txtpo.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[2].ToString();
                    txtdca.Text = ds.Tables["filldca"].Rows[0].ItemArray[0].ToString();
                    rbtndeduction.Enabled = true;
                    rbtnothercharges.Enabled = true;
                    ViewState["MRR"] = ddlmrr.SelectedItem.Text;
                    ViewState["costcenter"] = ds.Tables["mrrinfo"].Rows[0].ItemArray[2].ToString().Split('/')[0];
                    ViewState["itemcode"] = ds.Tables["filldca"].Rows[0].ItemArray[2].ToString().Substring(0, 1);
                    fillvendorgstnos(ds.Tables["mrrinfo"].Rows[0].ItemArray[0].ToString());
                    
                }
                //checkescisevat();
                //rbtnsalestype.SelectedIndex = -1;
                //rbtnexcisetype.SelectedIndex = -1;
            }
            else
            {
                clear();

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillvendorgstnos(string venid)
    {
        da = new SqlDataAdapter("select (vc.Gst_No+' , '+s.State)as state,vc.Gst_No from Vendor_Client_GstNos vc join States s on vc.State_Id=s.State_Id where Ven_Supp_Client_id='" + venid + "' and status='2'", con);
        ds = new DataSet();
        da.Fill(ds, "vengst");
        if (ds.Tables["vengst"].Rows.Count > 0)
        {
            ddlvengstnos.Items.Clear();
            ddlvengstnos.DataSource = ds.Tables["vengst"];
            ddlvengstnos.DataTextField = "state";
            ddlvengstnos.DataValueField = "Gst_No";
            ddlvengstnos.DataBind();
            ddlvengstnos.Items.Insert(0, new ListItem("Select"));
            fillgstnos();
        }
        else
        {
            ddlvengstnos.Items.Clear();
            ddlvengstnos.Items.Insert(0, new ListItem("Select"));           
        }
    }   
    protected void ddlgstnos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            taxvalues();
            if (ddlvengstnos.SelectedItem.Text != "")
            {
                if (Session["roles"].ToString() == "Central Store Keeper")
                {
                    da = new SqlDataAdapter("select (select State_Id from GSTmaster WHERE gst_no='" + ddlgstnos.SelectedValue + "')(select State_Id from Vendor_Client_GstNos where gst_no='" + ddlvengstnos.SelectedValue + "')", con);
                    ds = new DataSet();
                    da.Fill(ds, "checkstate");
                    if (ds.Tables["checkstate"].Rows[0].ItemArray[0].ToString() == ds.Tables["checkstate1"].Rows[0].ItemArray[0].ToString())
                    {
                        ViewState["StatesCheck"] = "Equal";
                    }
                    else
                    {
                        ViewState["StatesCheck"] = "NotEqual";
                    }
                    fillgrid();
                }
                else
                {
                    //da = new SqlDataAdapter("select (select State_Id from Cost_Center WHERE cc_code='" + ViewState["costcenter"].ToString() + "')(select State_Id from Vendor_Client_GstNos where gst_no='" + ddlvengstnos.SelectedValue + "')", con);
                    da = new SqlDataAdapter("select (select State_Id from GSTmaster WHERE gst_no='" + ddlgstnos.SelectedValue + "')(select State_Id from Vendor_Client_GstNos where gst_no='" + ddlvengstnos.SelectedValue + "')", con);
                    ds = new DataSet();
                    da.Fill(ds, "checkstates");
                    if (ds.Tables["checkstates"].Rows[0].ItemArray[0].ToString() == ds.Tables["checkstates1"].Rows[0].ItemArray[0].ToString())
                    {
                        ViewState["StatesCheck"] = "Equal";
                    }
                    else
                    {
                        ViewState["StatesCheck"] = "NotEqual";
                    }
                    fillgrid();
                }
            }
            else
            {
                clear();
            }
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillgrid()
    {
        try
        {
            da = new SqlDataAdapter("select id from Purchase_details where Po_no='" + txtpo.Text + "'  ", con);
            da.Fill(ds, "po_id");
            if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878)
                //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + txtpo.Text + "' and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
                //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (select p.id,Item_code,quantity,cc_code from [purchase_details]p join [recieved Items]r on p.Po_no=r.PO_no where p.Po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
                //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(basic_price,'.0000','.00') as [basic_price]  from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic  join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i  right outer join (Select id,item_code,(Select COALESCE(new_basicprice,basic_price) from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + txtpo.Text + "'  and item_code=r.item_code)as [basic_price],(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + txtpo.Text + "'  and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(basic_price,'.0000','.00') as [basic_price],Type from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic  join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i  right outer join (Select id,item_code,(Select COALESCE(new_basicprice,basic_price)from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + txtpo.Text + "'  and item_code=substring(r.item_code,1,8))as [basic_price],(Select Type from  [purchase_details] where Po_no='" + txtpo.Text + "'  and item_code=r.item_code)as [Type],(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + txtpo.Text + "'  and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
            else
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + txtpo.Text + "' and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
            //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (select p.id,Item_code,quantity,cc_code from [purchase_details]p join [recieved Items]r on p.Po_no=r.PO_no where p.Po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
            da.Fill(ds, "fillcmc");
            gridcmc.DataSource = ds.Tables["fillcmc"];
            gridcmc.DataBind();
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
            //trtransportheader.Visible = true;
            //trtransportlbl.Visible = true;
            //trtransportdata.Visible = true;
            //ddltrandca.Items.Clear();
            //ddltransdca.Items.Clear();
            //TransAmount.Text = "0";
            //ddltrandca.Items.Insert(0, new ListItem("Select DCA"));
            //ddltransdca.Items.Insert(0, new ListItem("Select SDCA"));
            filldca();
        }



        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            string otherdcas = "";
            string othersdcas = "";
            string otherdcasamt = "";
            string deductiondcas = "";
            string deductionsdcas = "";
            string deductionamt = "";
            foreach (GridViewRow record in gridcmc.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    cmd.Connection = con;
                    string id = gridcmc.DataKeys[record.RowIndex]["id"].ToString();
                    string amount = (record.FindControl("txtbasic") as TextBox).Text;
                    string sgstamt = (record.FindControl("txtsgst") as TextBox).Text;
                    string cgstamt = (record.FindControl("txtcgst") as TextBox).Text;
                    string igstamt = (record.FindControl("txtigst") as TextBox).Text;
                    cmd.CommandText = "Update [Recieved Items] Set basic_price='" + amount + "', CGSTPercent='" + cgstamt + "',SGSTPercent='" + sgstamt + "',IGSTPercent='" + igstamt + "' where id='" + id + "'";
                    con.Open();
                    i = Convert.ToInt32(cmd.ExecuteNonQuery());
                    i = i + 1;
                    con.Close();

                }

            }
            da = new SqlDataAdapter(" select p.CC_code from Purchase_details p join MR_Report m on p.Po_no=m.PO_no where MRR_no='" + ddlmrr.SelectedItem.Text + "'", con);
            da.Fill(ds, "CCinfo");
            string cc_code = ds.Tables["CCinfo"].Rows[0][0].ToString();
            if (i > 0)
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
                if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "StoreKeeper")
                {
                    cmd1 = new SqlCommand("checkbudgetgst", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Clear();
                    //cmd1.Parameters.AddWithValue("@Total", txttotal.Text);
                    cmd1.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                    cmd1.Parameters.AddWithValue("@PoNo", txtpo.Text);
                    cmd1.Parameters.AddWithValue("@CCCode", cc_code);
                    cmd1.Parameters.AddWithValue("@DCA_Code", txtdca.Text);
                    cmd1.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                    cmd1.Parameters.AddWithValue("@type", "2");
                    cmd1.Parameters.AddWithValue("@vtype", "1");
                    cmd1.Parameters.AddWithValue("@Otherdcas", otherdcas);
                    cmd1.Parameters.AddWithValue("@Otherdcaamts", otherdcasamt);
                    cmd1.Parameters.AddWithValue("@GSTtaxamt", txtgst.Text);
                    cmd1.Parameters.AddWithValue("@GSTtaxtype", rbtngsttype.SelectedValue);
                    cmd1.Parameters.AddWithValue("@Transportamt", TransAmount.Text);
                    cmd1.Parameters.AddWithValue("@Transportdca", ddltrandca.SelectedValue);
                    cmd1.Parameters.AddWithValue("@Invoicetype", "New");
                    cmd1.Connection = con;
                    con.Open();
                    result = cmd1.ExecuteScalar().ToString();
                    con.Close();
                }
                //else if (Session["roles"].ToString() == "StoreKeeper")
                //{
                //    result = "False";
                //}
                if (result == "sufficent")
                {
                    //cmd.CommandText = "sp_pending_invoice";
                    cmd.CommandText = "sp_pending_invoice_TAXGST";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VendorId", txtvendorid.Text);
                    cmd.Parameters.AddWithValue("@Name", txtvendorname.Text);
                    cmd.Parameters.AddWithValue("@invoiceno", txtin.Text);
                    cmd.Parameters.AddWithValue("@mrrno", ddlmrr.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@CCCode", cc_code);
                    cmd.Parameters.AddWithValue("@DCA_Code", txtdca.Text);
                    cmd.Parameters.AddWithValue("@amount", txtnetAmount.Text);
                    cmd.Parameters.AddWithValue("@PO_NO", txtpo.Text);
                    cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                    cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                    cmd.Parameters.AddWithValue("@PaymentType", ddlvendortype.SelectedItem.Text);
                    //New parameter created date 16-May-2016 Cr-ENH-MAR-010-2016 STARTS
                    cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                    //New parameter created date 16-May-2016 Cr-ENH-MAR-010-2016 ENDS

                    cmd.Parameters.AddWithValue("@Otherdcas", otherdcas);
                    cmd.Parameters.AddWithValue("@Othersdcas", othersdcas);
                    cmd.Parameters.AddWithValue("@Otherdcaamts", otherdcasamt);
                    cmd.Parameters.AddWithValue("@Deductiondcas", deductiondcas);
                    cmd.Parameters.AddWithValue("@Deductionsdcas", deductionsdcas);
                    cmd.Parameters.AddWithValue("@Deductiondcaamts", deductionamt);

                    cmd.Parameters.AddWithValue("@GSTtaxtype", rbtngsttype.SelectedValue);
                    cmd.Parameters.AddWithValue("@Taxno", ddlgstnos.SelectedValue);
                    cmd.Parameters.AddWithValue("@VendorGstno", ddlvengstnos.SelectedValue);
                    cmd.Parameters.AddWithValue("@Cgst", txtcgst.Text);
                    cmd.Parameters.AddWithValue("@Sgst", txtsgst.Text);
                    cmd.Parameters.AddWithValue("@Igst", txtigst.Text);
                    cmd.Parameters.AddWithValue("@GSTtaxamt", txtgst.Text);
                    //Transport section added on date 10thSept2018 Starts

                    cmd.Parameters.AddWithValue("@Transdca", ddltrandca.SelectedValue);
                    cmd.Parameters.AddWithValue("@Transsdca", ddltransdca.SelectedValue);
                    cmd.Parameters.AddWithValue("@Transamt", TransAmount.Text);
                    cmd.Parameters.AddWithValue("@Transcgstamt", transcgstAmt.Text);
                    cmd.Parameters.AddWithValue("@Transsgstamt", transsgstamt.Text);
                    cmd.Parameters.AddWithValue("@Transigstamt", transigstamount.Text);
                    //Transport ends
                    //TCS Starts
                    if (ddltcsapplicable.SelectedItem.Value == "Yes")
                    {
                        cmd.Parameters.AddWithValue("@Tcsamt", txttcsamount.Text);
                    }
                    //TCS Ends
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "Invoice Inserted";
                    con.Close();
                    if (msg == "Invoice Inserted")
                    {
                        JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, msg);
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
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
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
            string checktype = gridcmc.DataKeys[e.Row.RowIndex].Values[2].ToString();
            if (checktype == "PO")
            {
                txtbasic.Enabled = false;
            }
            else
            {
                txtbasic.Enabled = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
            TextBox txtcgst = (TextBox)e.Row.FindControl("txtcgst");
            TextBox txtsgst = (TextBox)e.Row.FindControl("txtsgst");
            Label lblcgstamt = (Label)e.Row.FindControl("lblcgstamt");
            Label lblsgstamt = (Label)e.Row.FindControl("lblamt");
            TextBox txtigst = (TextBox)e.Row.FindControl("txtigst");
            Label lbligstamt = (Label)e.Row.FindControl("lbligstamt");
            if (ViewState["StatesCheck"].ToString() == "Equal")
            {
                txtcgst.Text = Convert.ToInt16(0).ToString();
                lblcgstamt.Text = Convert.ToInt16(0).ToString();
                txtsgst.Text = Convert.ToInt16(0).ToString();
                lblsgstamt.Text = Convert.ToInt16(0).ToString();
                txtcgst.Enabled = true;
                txtsgst.Enabled = true;
                txtigst.Text = Convert.ToInt16(0).ToString();
                lbligstamt.Text = Convert.ToInt16(0).ToString();
                txtigst.Enabled = false;
                //New Code for transport
                Transcgstpercent.Text = Convert.ToInt16(0).ToString();                
                transcgstAmt.Text = Convert.ToInt16(0).ToString();
                transsgstpercent.Text = Convert.ToInt16(0).ToString();
                transsgstamt.Text = Convert.ToInt16(0).ToString();
                transigstpercent.Text = Convert.ToInt16(0).ToString();
                transigstamount.Text = Convert.ToInt16(0).ToString();
                Transcgstpercent.Enabled = true;
                transsgstpercent.Enabled = true;
                transigstpercent.Enabled = false;


            }
            if (ViewState["StatesCheck"].ToString() == "NotEqual")
            {
                txtcgst.Text = Convert.ToInt16(0).ToString();
                lblcgstamt.Text = Convert.ToInt16(0).ToString();
                txtsgst.Text = Convert.ToInt16(0).ToString();
                lblsgstamt.Text = Convert.ToInt16(0).ToString();
                txtcgst.Enabled = false;
                txtsgst.Enabled = false;
                txtigst.Text = Convert.ToInt16(0).ToString();
                lbligstamt.Text = Convert.ToInt16(0).ToString();
                txtigst.Enabled = true;
                //New Code for transport
                Transcgstpercent.Text = Convert.ToInt16(0).ToString();
                transcgstAmt.Text = Convert.ToInt16(0).ToString();
                transsgstpercent.Text = Convert.ToInt16(0).ToString();
                transsgstamt.Text = Convert.ToInt16(0).ToString();
                transigstpercent.Text = Convert.ToInt16(0).ToString();
                transigstamount.Text = Convert.ToInt16(0).ToString();
                Transcgstpercent.Enabled = false;
                transsgstpercent.Enabled = false;
                transigstpercent.Enabled = true;
            }

            int rowIndex = 0;            
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

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {           
            Label lblcgstfotter = (Label)e.Row.FindControl("lblfootercgstfooter");
            Label lblsgstfotter = (Label)e.Row.FindControl("Label3");
            Label lblcgstcamtfotter = (Label)e.Row.FindControl("lblcgstamountfooter");
            Label lblsgstamtfotter = (Label)e.Row.FindControl("Label4");
            Label lbligstfotter = (Label)e.Row.FindControl("lblfooterigstfooter");
            Label lbligstcamtfotter = (Label)e.Row.FindControl("lbligstamountfooter");
            if (ViewState["StatesCheck"].ToString() == "Equal")
            {
                //lblcgstfotter.Text = Convert.ToInt16(0).ToString();
                //lblsgstfotter.Text = Convert.ToInt16(0).ToString();
                lblcgstcamtfotter.Text = Convert.ToInt16(0).ToString();
                lblsgstamtfotter.Text = Convert.ToInt16(0).ToString();
                //lbligstfotter.Text = Convert.ToInt16(0).ToString();
                lbligstcamtfotter.Text = Convert.ToInt16(0).ToString();

            }
            if (ViewState["StatesCheck"].ToString() == "NotEqual")
            {
                //lblcgstfotter.Text = Convert.ToInt16(0).ToString();
                //lblsgstfotter.Text = Convert.ToInt16(0).ToString();
                lblcgstcamtfotter.Text = Convert.ToInt16(0).ToString();
                lblsgstamtfotter.Text = Convert.ToInt16(0).ToString();
                //lbligstfotter.Text = Convert.ToInt16(0).ToString();
                lbligstcamtfotter.Text = Convert.ToInt16(0).ToString();
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
    protected void rbtngsttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlmrr.SelectedItem.Text != "")
        //{
        //    if (rbtngsttype.SelectedValue == "Creditable")
        //        da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id and cc.cc_code='CCC' and gm.Status='3'", con);
        //    else
        //        da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id where gm.Status='3' and  cc.cc_code='" + ViewState["costcenter"].ToString() + "'", con);
        //    ds = new DataSet();
        //    da.Fill(ds, "gstmain");
        //    if (ds.Tables["gstmain"].Rows.Count > 0)
        //    {
        //        ddlgstnos.Items.Clear();
        //        ddlgstnos.DataSource = ds.Tables["gstmain"];
        //        ddlgstnos.DataTextField = "name";
        //        ddlgstnos.DataValueField = "Tax_nos";
        //        ddlgstnos.DataBind();
        //        ddlgstnos.Items.Insert(0, new ListItem("Select"));
        //    }
        //    else
        //    {
        //        ddlgstnos.Items.Clear();
        //        ddlgstnos.Items.Insert(0, new ListItem("Select"));
        //    }
        //}
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
    }
    public void fillgstnos()
    {
        if (ddlmrr.SelectedItem.Text != "")
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
            {
                if (ViewState["itemcode"].ToString() == "1")
                {
                    da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id where gm.Status='3'", con);
                }
                else
                {
                    //da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where gm.Status='3' and  cc.cc_code='" + ViewState["costcenter"].ToString() + "'and td.Status='Active' union select DISTINCT Gst_No as Tax_nos,(Gst_No+' , '+st.State)as state  from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No JOIN States st on st.State_Id=cc.State_Id where td.Dca_Code in ('DCA-GST-CR','DCA-GST-NON-CR') and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3' ORDER by gst_no", con);
                    da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where gm.Status='3' and  cc.cc_code='" + ViewState["costcenter"].ToString() + "'and td.Status='Active' union select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id where gm.Status='3'", con);
                }
            }
            else
            {
                //da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id where gm.Status='3' and  cc.cc_code='" + ViewState["costcenter"].ToString() + "'", con);
                //da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where gm.Status='3' and  cc.cc_code='" + ViewState["costcenter"].ToString() + "' and td.Status='Active' union select DISTINCT Gst_No as Tax_nos,(Gst_No+' , '+st.State)as state  from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No JOIN States st on st.State_Id=cc.State_Id where td.Dca_Code in ('DCA-GST-CR','DCA-GST-NON-CR') and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3' ORDER by gst_no", con);
                da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where gm.Status='3' and  cc.cc_code='" + ViewState["costcenter"].ToString() + "' and td.Status='Active' union select DISTINCT gst_no as Tax_nos, (gm.GST_No+' , '+st.State)as name from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join States st on st.State_Id=gm.State_Id where gm.Status='3'", con);

            }
            ds = new DataSet();
            da.Fill(ds, "gstmain");
            if (ds.Tables["gstmain"].Rows.Count > 0)
            {
                ddlgstnos.Items.Clear();
                ddlgstnos.DataSource = ds.Tables["gstmain"];
                ddlgstnos.DataTextField = "name";
                ddlgstnos.DataValueField = "Tax_nos";
                ddlgstnos.DataBind();
                ddlgstnos.Items.Insert(0, new ListItem("Select"));
            }
            else
            {
                ddlgstnos.Items.Clear();
                ddlgstnos.Items.Insert(0, new ListItem("Select"));
            }
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
    }
   
    protected void rbtnothercharges_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnothercharges.SelectedIndex == 0)
        {

            tdaddanyother.Visible = true;
            tdddlanyotherdcas.Visible = true;
            fillanyotherdcas();
        }
        else
        {
            tdddlanyotherdcas.Visible = false;
            tdaddanyother.Visible = false;
            tranyothergrid.Visible = false;
            TextBox1.Text = "";
            gvanyother.DataSource = null;
            gvanyother.DataBind();
            hfothers.Value = "0";
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
        //checkescisevat();
    }
    protected void rbtndeduction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtndeduction.SelectedIndex == 0)
        {

            tddeduction.Visible = true;
            tddedadd.Visible = true;
            fillanyotherdcas();
        }
        else
        {
            tddeduction.Visible = false;
            tddedadd.Visible = false;
            trdeductiongrid.Visible = false;
            TextBox2.Text = "";
            gvdeduction.DataSource = null;
            gvdeduction.DataBind();
            hfdeduction.Value = "0";
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
        //checkescisevat();
    }
    public void fillanyotherdcas()
    {
        if (rbtnothercharges.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("TaxDcasNew_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["MRR"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["costcenter"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "SupplierOthers";
            ds = new DataSet();
            da.Fill(ds, "otherdcas");
            if (ds.Tables["otherdcas"].Rows.Count > 0)
            {              
                ddlanyotherdcas.DataSource = ds.Tables["otherdcas"];
                ddlanyotherdcas.DataTextField = "mapdca_code";
                ddlanyotherdcas.DataValueField = "dca_code";
                ddlanyotherdcas.DataBind();
            }
        }
        if (rbtndeduction.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("TaxDcasNew_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["MRR"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["costcenter"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "SupplierDeductions";
            ds = new DataSet();
            da.Fill(ds, "deddcas");
            if (ds.Tables["deddcas"].Rows.Count > 0)
            {              
                ddldeduction.DataSource = ds.Tables["deddcas"];
                ddldeduction.DataTextField = "mapdca_code";
                ddldeduction.DataValueField = "dca_code";
                ddldeduction.DataBind();
            }
        }
    }
    protected void ddlanyotherdcas_DataBound(object sender, EventArgs e)
    {
        foreach (ListItem i in this.ddlanyotherdcas.Items)
        {
            da = new SqlDataAdapter("select dca_name from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where mapdca_code='" + i.Text + "'", con);
            ds = new DataSet();
            da.Fill(ds, "namesother");
            i.Attributes.Add("Title", ds.Tables["namesother"].Rows[0].ItemArray[0].ToString());
        }
    }
    public void checkothertooltip()
    {
        foreach (ListItem i in this.ddlanyotherdcas.Items)
        {
            da = new SqlDataAdapter("select dca_name from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where mapdca_code='" + i.Text + "'", con);
            ds = new DataSet();
            da.Fill(ds, "namesother");
            i.Attributes.Add("Title", ds.Tables["namesother"].Rows[0].ItemArray[0].ToString());
        }
    }
    protected void ddldeduction_DataBound(object sender, EventArgs e)
    {
        foreach (ListItem j in this.ddldeduction.Items)
        {
            da = new SqlDataAdapter("select dca_name from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where mapdca_code='" + j.Text + "'", con);
            ds = new DataSet();
            da.Fill(ds, "namesded");
            j.Attributes.Add("Title", ds.Tables["namesded"].Rows[0].ItemArray[0].ToString());
        }
    }
    public void checkdeductiontooltip()
    {
        foreach (ListItem j in this.ddldeduction.Items)
        {
            da = new SqlDataAdapter("select dca_name from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where mapdca_code='" + j.Text + "'", con);
            ds = new DataSet();
            da.Fill(ds, "namesded");
            j.Attributes.Add("Title", ds.Tables["namesded"].Rows[0].ItemArray[0].ToString());
        }
    }
    protected void ddlanyotherdcas_SelectedIndexChanged(object sender, EventArgs e)
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
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
        //checkescisevat();
        checkothertooltip();
    }
    protected void ddldeduction_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        for (int i = 0; i < ddldeduction.Items.Count; i++)
        {
            if (ddldeduction.Items[i].Selected)
            {
                name1 += ddldeduction.Items[i].Text + ",";
            }
        }
        TextBox2.Text = name1;
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
        //checkescisevat();
        checkdeductiontooltip();
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            string DataKey = rowView["dca_code"].ToString();
            DropDownList sdca = (e.Row.FindControl("ddlsdca") as DropDownList);
            //da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + DataKey + "'", con);
            da = new SqlDataAdapter("select (subdca_code+','+subdca_name)as mapsubdca_code,subdca_code from subdca where dca_code='" + DataKey + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            sdca.DataSource = ds.Tables[0];
            sdca.DataTextField = "mapsubdca_code";
            sdca.DataValueField = "subdca_code";
            sdca.DataBind();
            sdca.Items.Insert(0, new ListItem("Select SDCA"));

        }
    }
    protected void ddlsdcaded_DataBound(object sender, EventArgs e)
    {
        DropDownList ddlded = sender as DropDownList;
        if (ddlded != null)
        {
            foreach (ListItem li in ddlded.Items)
            {
                li.Attributes["title"] = li.Text;
            }
        }
    }
    protected void OnRowDataBoundgvdeduction(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            string DataKey = rowView["dca_code"].ToString();
            DropDownList sdca = (e.Row.FindControl("ddlsdca") as DropDownList);
            //da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + DataKey + "'", con);
            da = new SqlDataAdapter("select (subdca_code+','+subdca_name)as mapsubdca_code,subdca_code from subdca where dca_code='" + DataKey + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            sdca.DataSource = ds.Tables[0];
            sdca.DataTextField = "mapsubdca_code";
            sdca.DataValueField = "subdca_code";
            sdca.DataBind();
            sdca.Items.Insert(0, new ListItem("Select SDCA"));

        }
    }
    protected void ddlsdca_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        if (ddl != null)
        {
            foreach (ListItem li in ddl.Items)
            {
                li.Attributes["title"] = li.Text;
            }
        }
    }
    protected void Submit(object sender, EventArgs e)
    {
        string s1 = string.Empty;
        s1 = TextBox1.Text.Replace(",", "','");
        string s2 = s1.Substring(0, s1.Length - 3);
        fillgridsdca(s2);
        TextBox1.Text = "";
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
        //checkescisevat();

    }
    public void clear()
    {

        gvanyother.DataSource = null;
        gvanyother.DataBind();
        gvdeduction.DataSource = null;
        gvdeduction.DataBind();
        gridcmc.DataSource = null;
        gridcmc.DataBind();

        rbtndeduction.ClearSelection();
        rbtnothercharges.ClearSelection();

        tdddlanyotherdcas.Visible = false;
        tdaddanyother.Visible = false;


        tddeduction.Visible = false;
        tddedadd.Visible = false;
        txtnetAmount.Text = "";
    }

    public void taxvalues()
    {
        //ddltrandca.SelectedIndex = 0;
        //ddltransdca.SelectedIndex = 0;
        TransAmount.Text = "0";
        Transcgstpercent.Text = "0";
        transcgstAmt.Text = "0";
        transsgstpercent.Text = "0";
        transsgstamt.Text = "0";
        transigstpercent.Text = "0";
        transigstamount.Text = "0";
        hftranamount.Value = "0";
        hftrancgst.Value = "0";
        hftransgst.Value = "0";
        hftranigst.Value = "0";
        txtcgst.Text = "0";
        txtsgst.Text = "0";
        txtigst.Text = "0";
        txtgst.Text = "0";
        txttotal.Text = "0";
        txtnetAmount.Text = "0";
    }
    protected void Submitdeduction(object sender, EventArgs e)
    {
        string s1 = string.Empty;
        s1 = TextBox2.Text.Replace(",", "','");
        string s2 = s1.Substring(0, s1.Length - 3);
        fillgridded(s2);
        TextBox2.Text = "";
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);

    }
    public void fillgridsdca(string dcas)
    {
        tranyothergrid.Visible = true;
        //da = new SqlDataAdapter("select mapdca_code,dca_code from dca where mapdca_code in ('" + dcas + "')", con);
        da = new SqlDataAdapter("select (mapdca_code+','+dca_name)as mapdca_code,dca_code  FROM dca where mapdca_code in ('" + dcas + "')", con);
        ds = new DataSet();
        da.Fill(ds);
        if (rbtnothercharges.SelectedIndex == 0 && TextBox1.Text != "")
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
        da = new SqlDataAdapter("select (mapdca_code+','+dca_name)as mapdca_code,dca_code  FROM dca where mapdca_code in ('" + dcas + "')", con);
        ds = new DataSet();
        da.Fill(ds);
        if (rbtndeduction.SelectedIndex == 0 && TextBox2.Text != "")
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

    public void filldca()
    {
        da = new SqlDataAdapter("select distinct mapdca_code,yd.dca_code from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where dca_type='Expense'  and cc_code='" + ViewState["costcenter"].ToString() + "' and yd.dca_code='DCA-12'", con);
        ds = new DataSet();
        da.Fill(ds, "filltrandca");
        if (ds.Tables["filltrandca"].Rows.Count > 0)
        {
            trtransportheader.Visible = true;
            trtransportlbl.Visible = true;
            trtransportdata.Visible = true;
            ddltrandca.Items.Clear();
            ddltransdca.Items.Clear();
            TransAmount.Text = "0";
            ddltrandca.Items.Insert(0, new ListItem("Select DCA"));
            ddltransdca.Items.Insert(0, new ListItem("Select SDCA"));
            //ddltrandca.Enabled = true;
            //ddltransdca.Enabled = true;
            //TransAmount.Enabled = true;
            //Transcgstpercent.Enabled = true;
            //transsgstpercent.Enabled = true;
            //transigstpercent.Enabled = true;
            ddltrandca.DataSource = ds.Tables["filltrandca"];
            ddltrandca.DataTextField = "mapdca_code";
            ddltrandca.DataValueField = "dca_code";
            ddltrandca.DataBind();
            ddltrandca.Items.Insert(0, new ListItem("Select DCA"));
        }
        else
        {
            trtransportheader.Visible = false;
            trtransportlbl.Visible = false;
            trtransportdata.Visible = false;
            ddltrandca.Items.Clear();
            ddltransdca.Items.Clear();
            TransAmount.Text = "0";
            ddltrandca.Items.Insert(0, new ListItem("Select DCA"));
            ddltransdca.Items.Insert(0, new ListItem("Select SDCA"));
            //ddltrandca.Enabled = false;
            //ddltransdca.Enabled = false;
            //TransAmount.Enabled = false;
            //Transcgstpercent.Enabled = false;
            //transsgstpercent.Enabled = false;
            //transigstpercent.Enabled = false;
        }
       
    }
    protected void ddltrandca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddltrandca.SelectedIndex != 0)
            {
                da = new SqlDataAdapter("select subdca_code from subdca where dca_code='" + ddltrandca.SelectedValue + "'", con);
                ds = new DataSet();
                da.Fill(ds, "filltransdca");
                ddltransdca.DataSource = ds.Tables["filltransdca"];
                ddltransdca.DataTextField = "subdca_code";
                ddltransdca.DataValueField = "subdca_code";
                ddltransdca.DataBind();
                ddltransdca.Items.Insert(0, new ListItem("Select SDCA"));
            }
            else
            {
                JavaScript.UPAlert(Page, "Please select transport DCA");
                ddltransdca.Items.Insert(0, new ListItem("Select SDCA"));
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
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
    protected void ddltcsapplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txttcsamount.Text = "0";
            if (ddltcsapplicable.SelectedItem.Value != "Select")
            {
                if (ddltcsapplicable.SelectedItem.Value == "Yes")
                {
                    txttcsamount.Enabled = true;
                }
                else
                {
                    txttcsamount.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
                    //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:tcacal()", true);
                }
            }
            else
            {
                txttcsamount.Enabled = false;
                txttcsamount.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
                //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:tcacal()", true);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
    }


}