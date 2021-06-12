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


public partial class VerifyAdvancePayment : System.Web.UI.Page
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
                da = new SqlDataAdapter("select i.PO_NO,i.CC_Code,Amount,i.Transaction_No,b.Bank_Name from Invoice i join BankBook b on i.Transaction_No=b.Transaction_No where i.Status='1' and b.status='1'", con);
                da.Fill(ds, "filladvance");
                gridadvcredit.DataSource = ds.Tables["filladvance"];
                gridadvcredit.DataBind();
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void gridadvcredit_SelectedIndexChanged(object sender, EventArgs e)
    {
        trdetail.Visible = true;
        string transaction_no = gridadvcredit.SelectedValue.ToString();
       
        da = new SqlDataAdapter("select i.[PO_NO],i.[CC_Code],client_name,cast([BasicValue] as decimal(18,2))BasicValue,cast([ExciseDuty] as decimal(18,2))ExciseDuty,cast([EDcess] as decimal(18,2))EDcess,cast([HEDcess] as decimal(18,2))HEDcess,cast([ServiceTax] as decimal(18,2))ServiceTax,cast([SalesTax] as decimal(18,2))SalesTax,cast([Total] as decimal(18,2))Total,cast(TDS as decimal(18,2))TDS,cast(WCT as decimal(18,2))WCT,cast(Hold as decimal(18,2))Hold,cast(AnyOther as decimal(18,2))AnyOther,cast(NetExciseDuty as decimal(18,2))NetExciseDuty,cast(NetEDcess as decimal(18,2))NetEDcess,cast(NetHEDcess as decimal(18,2))NetHEDcess,cast(NetServiceTax as decimal(18,2))NetServiceTax,cast(NetSalesTax as decimal(18,2))NetSalesTax,i.[PaymentType],i.client_id,i.subclient_id,s.branch,[Bank_Name],[Description],[ModeOfPay],[No],cast([Credit] as decimal(18,2))Credit,REPLACE(CONVERT(VARCHAR(11),b.[Date], 106), ' ', '-')Date,cc.cc_subtype from Invoice i join BankBook b on i.Transaction_No=b.Transaction_No left outer join client c on c.client_id=i.client_id left outer join subclient s on s.subclient_id=i.subclient_id LEFT OUTER JOIN cost_center cc ON i.cc_code=cc.cc_code where  i.Status='1' and i.Transaction_No='" + transaction_no + "' ", con);
        da.Fill(ds, "Advancedetails");
        txtpono.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[0].ToString();
        txtcccode.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[1].ToString();
        lblclientname.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[2].ToString();
        txtadvbasic.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[3].ToString();
        txtex.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[4].ToString();
        txted.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[5].ToString();
        txthed.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[6].ToString();
        txtAdvStax.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[7].ToString();
        txtAdvsaltax.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[8].ToString();
        TxtAdvtotal.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[9].ToString();
        TxtAdvtds.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[10].ToString();
        TxtAdvwct.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[11].ToString();
        Txtadvhold.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[12].ToString();
        TxtAdvother.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[13].ToString();
        txtnetex.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[14].ToString();
        txtNetED.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[15].ToString();
        txtNetHED.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[16].ToString();
        txtnetadvsevtax.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[17].ToString();
        txtnetadvsaltax.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[18].ToString();
        txtclientid.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[20].ToString();
        txtsubclient.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[21].ToString();
        lblsubclientname.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[22].ToString();
        CascadingDropDown9.SelectedValue = ds.Tables["Advancedetails"].Rows[0].ItemArray[23].ToString();
        txtdesc.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[24].ToString();
        CascadingDropDown1.SelectedValue = ds.Tables["Advancedetails"].Rows[0].ItemArray[25].ToString();
        txtcheque.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[26].ToString();
        txtamt.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[27].ToString();
        txtdate.Text = ds.Tables["Advancedetails"].Rows[0].ItemArray[28].ToString();
        hfcctype.Value = ds.Tables["Advancedetails"].Rows[0].ItemArray[29].ToString();
        fillexcise();
        if (hfcctype.Value != "Service")
        {
            txtex.Enabled = true;
            txtAdvStax.Enabled = false;
            txtnetex.Enabled = true;
            txtnetadvsevtax.Enabled = false;
            lblservtaxnum.Text = "Excise No:";
            da = new SqlDataAdapter("Select ExciseReg_No from  Excise_Account where invoiceno='" + transaction_no + "';Select VatReg_No from VAT_Account where invoiceno='" + transaction_no + "'", con);
            da.Fill(ds, "VATReg");
            if (ds.Tables["VATReg"].Rows.Count > 0)           
                ddlservicetax.SelectedValue = ds.Tables["VATReg"].Rows[0][0].ToString();
            if (ds.Tables["VATReg1"].Rows.Count > 0)
                ddlvatno.SelectedValue = ds.Tables["VATReg1"].Rows[0][0].ToString();
           
        }
        else
        {
            txtnetadvsevtax.Enabled = true;
            txtAdvStax.Enabled = true;
            txtex.Enabled = false;
            txtnetex.Enabled = false;
            da = new SqlDataAdapter("Select SrtxReg_No from  ServiceTax_Account where invoiceno='" + transaction_no + "';Select VatReg_No from VAT_Account where invoiceno='" + transaction_no + "'", con);
            da.Fill(ds, "VATReg");
            if (ds.Tables["VATReg"].Rows.Count > 0)         
                ddlservicetax.SelectedValue = ds.Tables["VATReg"].Rows[0][0].ToString();
            if (ds.Tables["VATReg1"].Rows.Count > 0)
                ddlvatno.SelectedValue = ds.Tables["VATReg1"].Rows[0][0].ToString();
           
        }
        txtamt.Enabled = false;
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
                cmd.Parameters.AddWithValue("@Transaction_no", gridadvcredit.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CCCode", txtcccode.Text);
                cmd.Parameters.AddWithValue("@PO_NO", txtpono.Text);
                cmd.Parameters.AddWithValue("@BasicValue", txtadvbasic.Text);
                cmd.Parameters.AddWithValue("@TDS", TxtAdvtds.Text);
                cmd.Parameters.AddWithValue("@WCT", TxtAdvwct.Text);
                cmd.Parameters.AddWithValue("@Hold", Txtadvhold.Text);
                cmd.Parameters.AddWithValue("@AnyOther", TxtAdvother.Text);
                cmd.Parameters.AddWithValue("@category", "Advance");
                cmd.Parameters.AddWithValue("@clientid", txtclientid.Text);
                cmd.Parameters.AddWithValue("@subclientid", txtsubclient.Text);
                if (hfcctype.Value == "Service")
                {
                    cmd.Parameters.AddWithValue("@NetServiceTax", txtnetadvsevtax.Text);
                    cmd.Parameters.AddWithValue("@ServiceTax", txtAdvStax.Text);
                    if (ddlservicetax.SelectedItem.Text != "Select")
                        cmd.Parameters.AddWithValue("@Srtxno", ddlservicetax.SelectedItem.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExciseDuty", txtex.Text);
                    cmd.Parameters.AddWithValue("@NetExciseDuty", txtnetex.Text);
                    if (ddlservicetax.SelectedItem.Text != "Select")
                        cmd.Parameters.AddWithValue("@exciseno", ddlservicetax.SelectedItem.Text);
                }
                if (ddlvatno.SelectedItem.Text != "Select")
                    cmd.Parameters.AddWithValue("@vatno", ddlvatno.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Netsalestax", txtnetadvsaltax.Text);
                cmd.Parameters.AddWithValue("@salestax", txtAdvsaltax.Text);
                cmd.Parameters.AddWithValue("@Total", TxtAdvtotal.Text);
                cmd.Parameters.AddWithValue("@paytype", "Verify");
                cmd.Parameters.AddWithValue("@EDcess", txted.Text);
                cmd.Parameters.AddWithValue("@HEDcess", txthed.Text);
                cmd.Parameters.AddWithValue("@NetEDcess", txtNetED.Text);
                cmd.Parameters.AddWithValue("@NetHEDcess", txtNetHED.Text);
                cmd.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
                cmd.Parameters.AddWithValue("@PaymentType", "Advance");
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                JavaScript.UPAlertRedirect(Page, msg, "VerifyAdvancePayment.aspx");
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
    public void fillexcise()
    {
        try
        {
            if (hfcctype.Value != "Service")
                da = new SqlDataAdapter("select Excise_no from ExciseMaster where Status='3'", con);
            else
                da = new SqlDataAdapter("select ServiceTaxno as Excise_no from ServiceTaxMaster where Status='3'", con);

            da.Fill(ds, "Excise_no");

            ddlservicetax.DataValueField = "Excise_no";
            ddlservicetax.DataTextField = "Excise_no";
            ddlservicetax.DataSource = ds.Tables["Excise_no"];
            ddlservicetax.DataBind();
            ddlservicetax.Items.Insert(0, "Select");

            da = new SqlDataAdapter("select RegNo from [Saletax/VatMaster] where Status='3'", con);
            da.Fill(ds, "VAT_No");

            ddlvatno.DataValueField = "RegNo";
            ddlvatno.DataTextField = "RegNo";
            ddlvatno.DataSource = ds.Tables["VAT_No"];
            ddlvatno.DataBind();
            ddlvatno.Items.Insert(0, "Select");            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
}