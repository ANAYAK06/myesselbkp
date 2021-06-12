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

public partial class VendorInvoiceNew : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            checkapprovals();
            txtnetAmount.Attributes.Add("readonly", "2");
            tdddlanyotherdcas.Visible = false;
            tranyothergrid.Visible = false;
            tdaddanyother.Visible = false;
            tddeduction.Visible = false;
            tddedadd.Visible = false;
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
                    da = new SqlDataAdapter("Select distinct dca_code,subdca_code from (Select item_code,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select item_code from [Recieved Items]where po_no='" + ds.Tables["mrrinfo"].Rows[0].ItemArray[2].ToString() + "')r on i.item_code=substring(r.item_code,1,8)", con);
                    da.Fill(ds, "filldca");
                    txtvendorid.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[0].ToString();
                    txtvendorname.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[1].ToString();
                    txtpo.Text = ds.Tables["mrrinfo"].Rows[0].ItemArray[2].ToString();
                    txtdca.Text = ds.Tables["filldca"].Rows[0].ItemArray[0].ToString();
                    //txtsubdca.Text = ds.Tables["filldca"].Rows[0].ItemArray[1].ToString();
                    var vr = hfexciseapplicable.Value;
                    fillgrid();
                    //fillexcise(ds.Tables["mrrinfo"].Rows[0].ItemArray[3].ToString());
                    rbtndeduction.Enabled = true;                   
                    rbtnothercharges.Enabled = true;                  
                    ViewState["MRR"] = ddlmrr.SelectedItem.Text;
                    ViewState["costcenter"] = ds.Tables["mrrinfo"].Rows[0].ItemArray[2].ToString().Split('/')[0];
                }
                checkescisevat();
                rbtnsalestype.SelectedIndex = -1;
                rbtnexcisetype.SelectedIndex = -1;
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
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [PO_details]inl join [purchase_details]p on inl.Po_no=p.Po_no where p.Po_no='" + txtpo.Text + "' and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
                //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (select p.id,Item_code,quantity,cc_code from [purchase_details]p join [recieved Items]r on p.Po_no=r.PO_no where p.Po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
            else
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,(Select quantity from [indent_list]inl join [purchase_details]p on inl.indent_no=p.indent_no where po_no='" + txtpo.Text + "' and item_code=r.item_code)as [Raised Qty],quantity from [recieved Items]r where po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
                //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,quantity,Replace(i.basic_price,'.0000','.00') as [basic_price],il.cc_code from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (select p.id,Item_code,quantity,cc_code from [purchase_details]p join [recieved Items]r on p.Po_no=r.PO_no where p.Po_no='" + txtpo.Text + "')il on i.item_code=substring(il.item_code,1,8)", con);
            da.Fill(ds, "fillcmc");
            gridcmc.DataSource = ds.Tables["fillcmc"];
            gridcmc.DataBind();            
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
                    string staxamt = (record.FindControl("txtsalestax") as TextBox).Text;
                    string etaxamt = (record.FindControl("txtexcisetax") as TextBox).Text;
                    cmd.CommandText = "Update [Recieved Items] Set basic_price='" + amount + "', STaxPercent='" + staxamt + "',ETaxPercent='" + etaxamt + "' where id='" + id + "'";
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
                if (Session["roles"].ToString() == "Central Store Keeper")
                {
                    cmd1 = new SqlCommand("checkbudget", con);
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
                    if (rbtnsalestype.SelectedValue != "")
                    {
                        cmd1.Parameters.AddWithValue("@Salestaxtype", rbtnsalestype.SelectedValue);
                    }
                    cmd1.Parameters.AddWithValue("@Salestaxdca", ddlvatdca.SelectedValue);
                    cmd1.Parameters.AddWithValue("@Salestaxamt", txttax.Text);
                    if (rbtnexcisetype.SelectedValue != "")
                    {
                        cmd1.Parameters.AddWithValue("@Excisetaxtype", rbtnexcisetype.SelectedValue);
                    }
                    cmd1.Parameters.AddWithValue("@Excisetaxdca", ddlexcisedca.SelectedValue);
                    cmd1.Parameters.AddWithValue("@Excisetaxamt", txtexduty.Text);
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
                    //cmd.CommandText = "sp_pending_invoice";
                    cmd.CommandText = "sp_pending_invoice_TAX";
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
                    //cmd.Parameters.AddWithValue("@Salestax", txttax.Text);
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
                    if (rbtnsalestype.SelectedValue != "" && ddlvatdca.SelectedValue != "select" && ddlvatsdca.SelectedValue != "select" && ddlvattaxnos.SelectedValue != "Select Tax No")
                    {
                        cmd.Parameters.AddWithValue("@Salestaxtype", rbtnsalestype.SelectedValue);
                        cmd.Parameters.AddWithValue("@Salestaxdca", ddlvatdca.SelectedValue);
                        cmd.Parameters.AddWithValue("@Salestaxsdca", ddlvatsdca.SelectedValue);
                        cmd.Parameters.AddWithValue("@VATno", ddlvattaxnos.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@VATAmount", txttax.Text);
                    if (rbtnexcisetype.SelectedValue != "" && ddlexcisedca.SelectedValue != "select" && ddlexcisesdca.SelectedValue != "select" && ddlexcisetaxnos.SelectedValue != "Select Tax No")
                    {
                        cmd.Parameters.AddWithValue("@Excisetaxtype", rbtnexcisetype.SelectedValue);
                        cmd.Parameters.AddWithValue("@Excisetaxdca", ddlexcisedca.SelectedValue);
                        cmd.Parameters.AddWithValue("@Excisetaxsdca", ddlexcisesdca.SelectedValue);
                        cmd.Parameters.AddWithValue("@Exciseno", ddlexcisetaxnos.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@ExciseDuty", txtexduty.Text);
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "Invoice Inserted1";
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
            TextBox txtexcisetax = (TextBox)e.Row.FindControl("txtexcisetax");
            TextBox txttax = (TextBox)e.Row.FindControl("txtsalestax");
            Label lblexcamt = (Label)e.Row.FindControl("lblexamt");
            Label lblvatamt = (Label)e.Row.FindControl("lblamt");
            if (hfexciseapplicable.Value == "No")
            {
                txtexcisetax.Text = Convert.ToInt16(0).ToString();
                lblexcamt.Text = Convert.ToInt16(0).ToString();
                txtexcisetax.Enabled = false;

            }
            if (hfvatapplicable.Value == "No")
            {
                txttax.Text = Convert.ToInt16(0).ToString();
                lblvatamt.Text = Convert.ToInt16(0).ToString();
                txttax.Enabled = false;
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblexcfotter = (Label)e.Row.FindControl("lblfooterexcisefooter");
            Label lblvatfotter = (Label)e.Row.FindControl("Label3");
            Label lblexcamtfotter = (Label)e.Row.FindControl("lblexamountfooter");
            Label lblvatamtfotter = (Label)e.Row.FindControl("Label4");
            if (hfexciseapplicable.Value == "No")
            {
                lblexcfotter.Text = Convert.ToInt16(0).ToString();
                lblexcamtfotter.Text = Convert.ToInt16(0).ToString();

            }
            if (hfvatapplicable.Value == "No")
            {
                lblvatfotter.Text = Convert.ToInt16(0).ToString();
                lblvatamtfotter.Text = Convert.ToInt16(0).ToString();
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
    protected void rbtnsalestype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////if (rbtnsalestype.SelectedIndex == 0)
        ////{
        ////    da = new SqlDataAdapter("select mapdca_code,dca_code from dca where dca_type='Tax' and tax_type='Creditable' and mapdca_code='DCA-VAT-CR'", con);
        ////    da = new SqlDataAdapter("select mapdca_code,dca_code from dca where dca_type='Tax' and tax_type='Creditable' and mapdca_code='DCA-VAT-CR'", con);
        ////}
        ////else
        ////{
        ////    //da = new SqlDataAdapter("select mapdca_code,dca_code from dca where dca_type='Tax' and tax_type='Non-Creditable' and mapdca_code='DCA-VAT-NON-CR'", con);
        ////    da = new SqlDataAdapter("select mapdca_code, yd.dca_code from dca d join yearly_dcabudget yd on yd.dca_code = d.dca_code where dca_type = 'Tax' and tax_type='Non-Creditable' and mapdca_code='DCA-VAT-NON-CR' and cc_code = '" + Session["CC_CODE"].ToString() + "'", con);
        ////}
        da = new SqlDataAdapter("vatexcisedcas_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = rbtnsalestype.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["MRR"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Sales";
        ds = new DataSet();
        da.Fill(ds, "vatdcas");
        if (ds.Tables["vatdcas"].Rows.Count > 0)
        {
            ddlvatdca.Items.Clear();
            ddlvatdca.DataSource = ds.Tables["vatdcas"];
            ddlvatdca.DataTextField = "mapdca_code";
            ddlvatdca.DataValueField = "dca_code";
            ddlvatdca.DataBind();
            ddlvatdca.Items.Insert(0, new ListItem("Select"));
            fillsalessdca(ds.Tables["vatdcas"].Rows[0].ItemArray[1].ToString());
        }
        else
        {
            ddlvatdca.Items.Clear();
            ddlvatsdca.Items.Clear();
            ddlvattaxnos.Items.Clear();
            ddlvatdca.Items.Insert(0, new ListItem("Select"));
            ddlvatsdca.Items.Insert(0, new ListItem("Select"));
            ddlvattaxnos.Items.Insert(0, new ListItem("Select"));
        }
       ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
    }
    public void fillsalessdca(string dca)
    {
        da = new SqlDataAdapter("select mapsubdca_code,subdca_code from subdca where dca_code='" + dca + "'", con);
        ds = new DataSet();
        da.Fill(ds, "vatsdca");
        if (ds.Tables["vatsdca"].Rows.Count > 0)
        {
            ddlvatsdca.Items.Clear();
            ddlvatsdca.DataSource = ds.Tables["vatsdca"];
            ddlvatsdca.DataTextField = "mapsubdca_code";
            ddlvatsdca.DataValueField = "subdca_code";
            ddlvatsdca.DataBind();
            ddlvatsdca.Items.Insert(0, new ListItem("Select"));
            fillsalestaxnos(dca);
        }
        else
        {
            ddlvatsdca.Items.Clear();
            ddlvattaxnos.Items.Clear();
            ddlvatsdca.Items.Insert(0, new ListItem("Select"));
            ddlvattaxnos.Items.Insert(0, new ListItem("Select"));
        }
    }
    public void fillsalestaxnos(string dca1)
    {
        da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + dca1 + "' and STATUS='Active'", con);
        ds.Clear();
        ds = new DataSet();
        da.Fill(ds, "taxvatdca");
        if (ds.Tables["taxvatdca"].Rows.Count > 0)
        {
            ddlvattaxnos.Items.Clear();
            ddlvattaxnos.DataSource = ds.Tables["taxvatdca"];
            ddlvattaxnos.DataTextField = "Tax_nos";
            ddlvattaxnos.DataValueField = "Tax_nos";
            ddlvattaxnos.DataBind();
            ddlvattaxnos.Items.Insert(0, new ListItem("Select TaxNos"));
        }
        else
        {
            ddlvattaxnos.Items.Clear();
            ddlvattaxnos.Items.Insert(0, new ListItem("Select TaxNos"));
        }
    }
    protected void rbtnexcisetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("vatexcisedcas_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = rbtnexcisetype.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["MRR"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Excise";
        ds = new DataSet();
        da.Fill(ds, "excdca");
        if (ds.Tables["excdca"].Rows.Count > 0)
        {
            ddlexcisedca.DataSource = ds.Tables["excdca"];
            ddlexcisedca.DataTextField = "mapdca_code";
            ddlexcisedca.DataValueField = "dca_code";
            ddlexcisedca.DataBind();
            ddlexcisedca.Items.Insert(0, new ListItem("Select"));
            fillexcisesdca(ds.Tables["excdca"].Rows[0].ItemArray[1].ToString());

        }
        else
        {
            ddlexcisedca.Items.Clear();
            ddlexcisesdca.Items.Clear();
            ddlexcisetaxnos.Items.Clear();
            ddlexcisedca.Items.Insert(0, new ListItem("Select"));
            ddlexcisesdca.Items.Insert(0, new ListItem("Select"));
            ddlexcisetaxnos.Items.Insert(0, new ListItem("Select"));
        }
       ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculate()", true);
    }
    public void fillexcisesdca(string dca)
    {
        da = new SqlDataAdapter("select mapsubdca_code,subdca_code from subdca where dca_code='" + dca + "'", con);
        ds = new DataSet();
        da.Fill(ds, "excsdca");
        if (ds.Tables["excsdca"].Rows.Count > 0)
        {
            ddlexcisesdca.DataSource = ds.Tables["excsdca"];
            ddlexcisesdca.DataTextField = "mapsubdca_code";
            ddlexcisesdca.DataValueField = "subdca_code";
            ddlexcisesdca.DataBind();
            ddlexcisesdca.Items.Insert(0, new ListItem("Select"));
            fillexcisetaxnos(dca);
        }
        else
        {
            ddlexcisesdca.Items.Clear();
            ddlexcisetaxnos.Items.Clear();
            ddlexcisesdca.Items.Insert(0, new ListItem("Select"));
            ddlexcisetaxnos.Items.Insert(0, new ListItem("Select"));
        }
    }
    public void fillexcisetaxnos(string dca1)
    {
        da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + dca1 + "' and STATUS='Active'", con);
        ds = new DataSet();
        da.Fill(ds, "taxexcdca");
        if (ds.Tables["taxexcdca"].Rows.Count > 0)
        {
            ddlexcisetaxnos.DataSource = ds.Tables["taxexcdca"];
            ddlexcisetaxnos.DataTextField = "Tax_nos";
            ddlexcisetaxnos.DataValueField = "Tax_nos";
            ddlexcisetaxnos.DataBind();
            ddlexcisetaxnos.Items.Insert(0, new ListItem("Select Tax No"));
        }
        else
        {
            ddlexcisetaxnos.Items.Clear();
            ddlexcisetaxnos.Items.Insert(0, new ListItem("Select Tax No"));
        }
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
        checkescisevat();
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
        checkescisevat();
    }
    public void fillanyotherdcas()
    {
        if (rbtnothercharges.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("TaxDcas_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["MRR"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["costcenter"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "SupplierOthers";
            ds = new DataSet();
            da.Fill(ds, "otherdcas");
            if (ds.Tables["otherdcas"].Rows.Count > 0)
            {
                //da = new SqlDataAdapter("select distinct mapdca_code,yd.dca_code from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where dca_type='Expense'  and cc_code='" + ViewState["costcenter"].ToString() + "'", con);
                //ds = new DataSet();
                //da.Fill(ds, "fillothdcas");
                ddlanyotherdcas.DataSource = ds.Tables["otherdcas"];
                ddlanyotherdcas.DataTextField = "mapdca_code";
                ddlanyotherdcas.DataValueField = "dca_code";
                ddlanyotherdcas.DataBind();
            }
        }
        if (rbtndeduction.SelectedIndex == 0)
        {
             da = new SqlDataAdapter("TaxDcas_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@MRR", SqlDbType.VarChar).Value = ViewState["MRR"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["costcenter"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "SupplierDeductions";
            ds = new DataSet();
            da.Fill(ds, "deddcas");
            if (ds.Tables["deddcas"].Rows.Count > 0)
            {
                //da = new SqlDataAdapter("select distinct mapdca_code,yd.dca_code from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where dca_type='Expense'  and cc_code='" + ViewState["costcenter"].ToString() + "'", con);
                //ds = new DataSet();
                //da.Fill(ds, "filldeddcas");
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
            da.Fill(ds,"namesother");
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
        checkescisevat();
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
       checkescisevat();
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
        checkescisevat();

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

        rbtndeduction.Enabled = false;
        //rbtnexcisetype.Enabled = false;
        rbtnothercharges.Enabled = false;
        //rbtnsalestype.Enabled = false;
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
    public void checkescisevat()
    {
        if (hfexciseapplicable.Value != "")
        {
            if (hfexciseapplicable.Value == "Yes")
            {
                rbtnexcisetype.Enabled = true;
                ddlexcisedca.Enabled = true;
                ddlexcisesdca.Enabled = true;
                ddlexcisetaxnos.Enabled = true;
            }
            else
            {
                rbtnexcisetype.Enabled = false;
                ddlexcisedca.Enabled = false;
                ddlexcisesdca.Enabled = false;
                ddlexcisetaxnos.Enabled = false;
            }
        }
        if (hfvatapplicable.Value != "")
        {
            if (hfvatapplicable.Value == "Yes")
            {
                rbtnsalestype.Enabled = true;
                ddlvatdca.Enabled = true;
                ddlvatsdca.Enabled = true;
                ddlvattaxnos.Enabled = true;
            }
            else
            {
                rbtnsalestype.Enabled = false;
                ddlvatdca.Enabled = false;
                ddlvatsdca.Enabled = false;
                ddlvattaxnos.Enabled = false;
            }
        }

    }
    
}