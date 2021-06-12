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

public partial class VerifyCreditPayment : System.Web.UI.Page
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
            filladvance();
            trdetail.Visible = false;
        }
    }
    public void filladvance()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                da = new SqlDataAdapter("select i.PO_NO,i.CC_Code,Amount,i.InvoiceNo,b.Bank_Name,b.ID,i.Invoice_Id from Invoice i join BankBook b on i.InvoiceNo=b.InvoiceNo where i.Status='Credit Pending' and b.status='1'", con);
                da.Fill(ds, "filladvance");
                gridInvcredit.DataSource = ds.Tables["filladvance"];
                gridInvcredit.DataBind();
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void gridInvcredit_SelectedIndexChanged(object sender, EventArgs e)
    {
        trdetail.Visible = true;
        string InvoiceNo = gridInvcredit.SelectedValue.ToString();
        try
        {
            da = new SqlDataAdapter("select i.[CC_Code],i.[PaymentType],i.[InvoiceNo],[invoice_date],i.[PO_NO],cast([BasicValue] as decimal(18,2))[BasicValue],cast([ServiceTax] as decimal(18,2))[ServiceTax],cast([EDcess] as decimal(18,2))[EDcess],cast([HEDcess] as decimal(18,2))[HEDcess],cast([ExciseDuty] as decimal(18,2))[ExciseDuty],cast([Frieght] as decimal(18,2))[Frieght],cast([Insurance] as decimal(18,2))[Insurance],cast([Total] as decimal(18,2))[Total],cast([Salestax] as decimal(18,2))[Salestax],[RA_NO],[Client_id],subclient_id,(Select cc_subtype from cost_center where cc_code=i.cc_code),invoicetype,cast(TDS as decimal(18,2))TDS,cast(i.Retention as decimal(18,2))Retention,cast(i.WCT as decimal(18,2))WCT,cast(i.Advance as decimal(18,2))Advance,cast(i.Hold as decimal(18,2))Hold,cast(i.AnyOther as decimal(18,2))AnyOther,cast(i.NetExciseDuty as decimal(18,2))NetExciseDuty,cast(i.NetEDcess as decimal(18,2))NetEDcess,cast(i.NetHEDcess as decimal(18,2))NetHEDcess,cast(i.NetSalesTax as decimal(18,2))NetSalesTax,cast(i.NetServiceTax as decimal(18,2))NetServiceTax,[Bank_Name],[Description],[ModeOfPay],[No],cast([Credit] as decimal(18,2))Credit,REPLACE(CONVERT(VARCHAR(11),b.[Date], 106), ' ', '-')Date,NetFrieght,NetInsurance from invoice i join BankBook b on i.InvoiceNo=b.InvoiceNo where  i.Status='Credit Pending' and i.InvoiceNo='" + InvoiceNo + "' and b.status='1'", con);
            da.Fill(ds, "Details");
            if (ds.Tables["Details"].Rows.Count > 0)
            {
                
                string type = ds.Tables["Details"].Rows[0].ItemArray[1].ToString();
                string pono = ds.Tables["Details"].Rows[0].ItemArray[4].ToString();
                da = new SqlDataAdapter("select MobilisationAdv from po where po_no='" + pono + "'", con);
                da.Fill(ds, "Advance");
                if (ds.Tables["Advance"].Rows.Count > 0 && ds.Tables["Advance"].Rows[0][0].ToString() != "")
                {
                if (ds.Tables["Advance"].Rows[0][0].ToString() == "Yes")
                    txtadvance.Enabled = true;
                else
                    txtadvance.Enabled = false;
                }
                else
                    txtadvance.Enabled = true;               

               // hfcctype.Value = ds.Tables["Details"].Rows[0].ItemArray[17].ToString();

                txtcccode.Text = ds.Tables["Details"].Rows[0].ItemArray[0].ToString();
                txtindt.Text = ds.Tables["Details"].Rows[0].ItemArray[3].ToString();
                txtinvoice.Text = ds.Tables["Details"].Rows[0].ItemArray[2].ToString();
                txtpono.Text = ds.Tables["Details"].Rows[0].ItemArray[4].ToString();
                checkadvance();
                txtbasic.Text = ds.Tables["Details"].Rows[0].ItemArray[5].ToString();
                txted.Text = ds.Tables["Details"].Rows[0].ItemArray[7].ToString().Replace(".0000", ".00");
                txthed.Text = ds.Tables["Details"].Rows[0].ItemArray[8].ToString().Replace(".0000", ".00");
                txttotal.Text = ds.Tables["Details"].Rows[0].ItemArray[12].ToString().Replace(".0000", ".00");
                txtra.Text = ds.Tables["Details"].Rows[0].ItemArray[14].ToString();
                txtclientid.Text = ds.Tables["Details"].Rows[0].ItemArray[15].ToString();
                txtsubclient.Text = ds.Tables["Details"].Rows[0].ItemArray[16].ToString();
                txttds.Text = ds.Tables["Details"].Rows[0].ItemArray[19].ToString();
                txtretention.Text = ds.Tables["Details"].Rows[0].ItemArray[20].ToString();
                txtwct.Text = ds.Tables["Details"].Rows[0].ItemArray[21].ToString();
                txtadvance.Text = ds.Tables["Details"].Rows[0].ItemArray[22].ToString();
                txthold.Text = ds.Tables["Details"].Rows[0].ItemArray[23].ToString();
                txtother.Text = ds.Tables["Details"].Rows[0].ItemArray[24].ToString();
                
                CascadingDropDown9.SelectedValue = ds.Tables["Details"].Rows[0].ItemArray[30].ToString();
                txtdesc.Text = ds.Tables["Details"].Rows[0].ItemArray[31].ToString();
                CascadingDropDown1.SelectedValue = ds.Tables["Details"].Rows[0].ItemArray[32].ToString();
                txtcheque.Text = ds.Tables["Details"].Rows[0].ItemArray[33].ToString();
                txtamt.Text = ds.Tables["Details"].Rows[0].ItemArray[34].ToString();
                txtdate.Text = ds.Tables["Details"].Rows[0].ItemArray[35].ToString();
                ViewState["InvoiceType"] = ds.Tables["Details"].Rows[0].ItemArray[1].ToString();
                hftypeofpay.Value = ds.Tables["Details"].Rows[0].ItemArray[1].ToString();
                

                if (ds.Tables["Details"].Rows[0].ItemArray[17].ToString() == "Service")
                {

                    txttax.Text = ds.Tables["Details"].Rows[0].ItemArray[6].ToString();
                    lbltax.Text = "Service Tax";
                    Label1.Text = "Net Service Tax";
                    txtnetex.Text = ds.Tables["Details"].Rows[0].ItemArray[29].ToString();
                }
                else
                {
                    txtfre.Enabled = true;
                    txtex.Enabled = true;
                    txtins.Enabled = true;
                    txttax.Text = ds.Tables["Details"].Rows[0].ItemArray[13].ToString();
                    txtex.Text = ds.Tables["Details"].Rows[0].ItemArray[9].ToString();
                    txtfre.Text = ds.Tables["Details"].Rows[0].ItemArray[10].ToString();
                    txtins.Text = ds.Tables["Details"].Rows[0].ItemArray[11].ToString();
                    txtnetfre.Text = ds.Tables["Details"].Rows[0].ItemArray[36].ToString();
                    txtnetins.Text = ds.Tables["Details"].Rows[0].ItemArray[37].ToString();

                    lbltax.Text = "Sales Tax";
                    lblnettax.Text = "Sales Tax";
                    txtnettax.Text = ds.Tables["Details"].Rows[0].ItemArray[28].ToString();
                    Label1.Text = "Excise duty";
                    txtnetex.Text = ds.Tables["Details"].Rows[0].ItemArray[25].ToString();
                    Label4.Text = "Excise duty";
                }
                if (ds.Tables["Details"].Rows[0].ItemArray[18].ToString() == "SEZ/Service Tax exumpted Invoice" && ds.Tables["Details"].Rows[0].ItemArray[1].ToString() == "Invoice Service")
                {
                    txtnettax.Text = "0";

                    txtNetED.Text = "0";
                    txtNetHED.Text = "0";
                    txttax.Text = "0";
                    txttax.Enabled = false;
                    txted.Enabled = false;
                    txthed.Enabled = false;

                    txtnettax.Enabled = false;
                    txtnetex.Enabled = false;
                    txtNetED.Enabled = false;
                    txtNetHED.Enabled = false;
                    txtnetfre.Enabled = false;
                    txtnetins.Enabled = false;
                    txtex.Enabled = false;
                    txtfre.Enabled = false;
                    txtins.Enabled = false;
                    txtex.Text = "0";
                    txtfre.Text = "0";
                    txtins.Text = "0";
                    Label1.Text = "Service Tax";

                    Label4.Text = "Excise duty";
                    ViewState["ServiceInvoice"] = "";
                }

                else if (ds.Tables["Details"].Rows[0].ItemArray[18].ToString() == "VAT/Material Supply" && ds.Tables["Details"].Rows[0].ItemArray[1].ToString() == "Invoice Service")
                {
                    txttax.Text = ds.Tables["Details"].Rows[0].ItemArray[13].ToString();
                    txtfre.Text = ds.Tables["Details"].Rows[0].ItemArray[10].ToString();
                    txtins.Text = ds.Tables["Details"].Rows[0].ItemArray[11].ToString();
                    lbltax.Text = "Sales Tax";

                    NETDetails.Visible = true;
                    txtNetED.Enabled = false;
                    txtNetHED.Enabled = false;
                    txtnetex.Enabled = false;

                    txtnetex.Text = "0";
                    txtNetED.Text = "0";
                    txtNetHED.Text = "0";

                    netex.Visible = true;
                    Label1.Text = "Service Tax";
                    txtnetex.Text = ds.Tables["Details"].Rows[0].ItemArray[29].ToString();
                    txtnettax.Text = ds.Tables["Details"].Rows[0].ItemArray[28].ToString();
                    txtnetfre.Text = ds.Tables["Details"].Rows[0].ItemArray[36].ToString();
                    txtnetins.Text = ds.Tables["Details"].Rows[0].ItemArray[37].ToString();

                    Label4.Text = "Service Tax";
                    txted.Enabled = false;
                    txthed.Enabled = false;
                    txtex.Enabled = false;
                    ViewState["ServiceInvoice"] = ds.Tables["Details"].Rows[0].ItemArray[18].ToString();
                    hf2.Value = ds.Tables["Details"].Rows[0].ItemArray[18].ToString();
                    txted.Text = "0";
                    txthed.Text = "0";
                    txtex.Text = "0";
                }
                else
                {
                    ViewState["ServiceInvoice"] = "";
                    NETDetails.Visible = true;
                    netex.Visible = true;
                    txttax.Enabled = true;
                    txted.Enabled = true;
                    txthed.Enabled = true;
                    txtnettax.Enabled = true;
                    txtnettax.Text = ds.Tables["Details"].Rows[0].ItemArray[28].ToString();
                    txtNetED.Enabled = true;
                    txtNetED.Text = ds.Tables["Details"].Rows[0].ItemArray[26].ToString();
                    txtNetHED.Enabled = true;
                    txtNetHED.Text = ds.Tables["Details"].Rows[0].ItemArray[27].ToString();
                    Label1.Text = "Excise duty";
                    txtnetex.Text = ds.Tables["Details"].Rows[0].ItemArray[25].ToString();
                    Label4.Text = "Excise duty";
                }
                if (ds.Tables["Details"].Rows[0].ItemArray[18].ToString() == "Service Tax Invoice" && ds.Tables["Details"].Rows[0].ItemArray[1].ToString() == "Invoice Service")
                {
                    txtex.Enabled = false;
                    txtfre.Enabled = false;
                    txtins.Enabled = false;

                    txtex.Text = "0";
                    txtfre.Text = "0";
                    txtins.Text = "0";
                    Label4.Text = "Excise duty";
                    Label1.Text = "Net Service Tax";
                    txtnetfre.Enabled = false;
                    txtnetins.Enabled = false;
                    txtnettax.Enabled = false;
                    txtnettax.Text = "0";
                    txtnetfre.Text = "0";
                    txtnetins.Text = "0";
                    txtnetex.Enabled = true;
                    txtnetex.Text = ds.Tables["Details"].Rows[0].ItemArray[29].ToString();
                    txtNetED.Text = ds.Tables["Details"].Rows[0].ItemArray[26].ToString();
                    txtNetHED.Text = ds.Tables["Details"].Rows[0].ItemArray[27].ToString();
                }
            }

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    public void checkadvance()
    {
        da = new SqlDataAdapter("select MobilisationAdv from po where po_no='" + txtpono.Text + "'", con);
        da.Fill(ds, "MOb");
        if (ds.Tables["MOb"].Rows.Count > 0 && ds.Tables["MOb"].Rows[0][0].ToString() == "No")
        {
            txtadvance.Enabled = false;
            txtadvance.Text = "0";
        }
        else
        {
            txtadvance.Enabled = true;
            da = new SqlDataAdapter("select isnull(sum(advance),0)totaldeduction from invoice where PaymentType!='Advance' and cc_code='" + txtcccode.Text + "' and status not in ('cancel') and po_no='" + txtpono.Text + "';select isnull(sum(total),0)totalcredit from invoice where PaymentType='Advance'  and cc_code='" + txtcccode.Text + "' and po_no='" +txtpono.Text + "'", con);
            da.Fill(ds, "total");
            if (ds.Tables["total"].Rows.Count > 0)
                hftotaldeduct.Value = ds.Tables["total"].Rows[0][0].ToString();
            else
                hftotaldeduct.Value = "0";
            if (ds.Tables["total1"].Rows.Count > 0)
                hftotalcredit.Value = ds.Tables["total1"].Rows[0][0].ToString();
            else
                hftotalcredit.Value = "0";
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        try
        {
            BankDateCheck objdate = new BankDateCheck();
            if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedItem.Text))
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Invoice_Credit";

                cmd.Parameters.AddWithValue("@InvoiceNo", gridInvcredit.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                cmd.Parameters.AddWithValue("@RA_NO", txtra.Text);
                cmd.Parameters.AddWithValue("@Total", txttotal.Text);
                cmd.Parameters.AddWithValue("@Retention", txtretention.Text);
                cmd.Parameters.AddWithValue("@Advance", txtadvance.Text);
                cmd.Parameters.AddWithValue("@CCCode", txtcccode.Text);
                cmd.Parameters.AddWithValue("@PO_NO", txtpono.Text);
                cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                cmd.Parameters.AddWithValue("@TDS", txttds.Text);
                cmd.Parameters.AddWithValue("@WCT", txtwct.Text);
                cmd.Parameters.AddWithValue("@Hold", txthold.Text);
                cmd.Parameters.AddWithValue("@AnyOther", txtother.Text);
                cmd.Parameters.AddWithValue("@clientid", txtclientid.Text);
                cmd.Parameters.AddWithValue("@subclientid", txtsubclient.Text);
                cmd.Parameters.AddWithValue("@paytype", "Verify");

                if (ViewState["InvoiceType"].ToString() == "Trading Supply" || ViewState["InvoiceType"].ToString() == "Manufacturing")
                {

                    cmd.Parameters.AddWithValue("@ExciseDuty", txtex.Text);
                    cmd.Parameters.AddWithValue("@Frieght", txtfre.Text);
                    cmd.Parameters.AddWithValue("@Insurance", txtins.Text);
                    cmd.Parameters.AddWithValue("@NetExciseDuty", txtnetex.Text);
                    cmd.Parameters.AddWithValue("@NetFrieght", txtnetfre.Text);
                    cmd.Parameters.AddWithValue("@NetInsurance", txtnetins.Text);
                    cmd.Parameters.AddWithValue("@Netsalestax", txtnettax.Text);
                    cmd.Parameters.AddWithValue("@salestax", txttax.Text);

                }
                else if (ViewState["InvoiceType"].ToString() == "Invoice Service")
                {
                    if (ViewState["ServiceInvoice"].ToString() == "VAT/Material Supply")
                    {
                        cmd.Parameters.AddWithValue("@Frieght", txtfre.Text);
                        cmd.Parameters.AddWithValue("@Insurance", txtins.Text);
                        cmd.Parameters.AddWithValue("@NetFrieght", txtnetfre.Text);
                        cmd.Parameters.AddWithValue("@NetInsurance", txtnetins.Text);
                        cmd.Parameters.AddWithValue("@Netsalestax", txtnettax.Text);
                        cmd.Parameters.AddWithValue("@salestax", txttax.Text);
                        cmd.Parameters.AddWithValue("@type", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@NetServiceTax", txtnetex.Text);
                        cmd.Parameters.AddWithValue("@ServiceTax", txttax.Text);
                    }
                }

                cmd.Parameters.AddWithValue("@EDcess", txted.Text);
                cmd.Parameters.AddWithValue("@HEDcess", txthed.Text);

                cmd.Parameters.AddWithValue("@NetEDcess", txtNetED.Text);
                cmd.Parameters.AddWithValue("@NetHEDcess", txtNetHED.Text);
                cmd.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
                cmd.Parameters.AddWithValue("@PaymentType", ViewState["InvoiceType"]);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                // cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                JavaScript.UPAlertRedirect(Page, msg, "VerifyCreditPayment.aspx");
                con.Close();
            }
            else
            {
                JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void gridInvcredit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_Rejectcreditinvoice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Bankid", SqlDbType.VarChar).Value = gridInvcredit.DataKeys[e.RowIndex]["ID"].ToString();
            cmd.Parameters.Add("@Invid", SqlDbType.VarChar).Value = gridInvcredit.DataKeys[e.RowIndex]["Invoice_Id"].ToString();
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, msg, "VerifyCreditPayment.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
            filladvance();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
}