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

public partial class frmBankflow : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "HoAdmin")
                rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
            paytype.Visible = false;
            reset();
            month.Visible = false;
            LoadYear();          
            trclient.Visible = false;
            Trtaxnums.Visible = false;
         
        }
        btnsubmit.Visible = true;
      

    }
   
    protected void ddltypeofpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden");
        ddlsubclientid.Items.Clear();
        ddlcccode.Items.Clear();
        ddlinno.Items.Clear();
        ddlpono.Items.Clear();
        hfadvance.Value = ddltypeofpay.SelectedItem.Text;
        lblcccode.Text = "";
        Label9.Text = "";

        subtype();
        cascadingDCA cs = new cascadingDCA();
        
        //cs.clientid("", "client", );
        cs.Paymenttype(ddltypeofpay.SelectedItem.Text);
     
          ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1"); ClearTextBoxes(cph);

    }

    protected void ddlsubtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
          ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1"); ClearTextBoxes(cph);
        ddlvendor.Visible = false;
        ddlpo.Visible = false;
        Button1.Visible = false;
        service();
        if (ddlsubtype.SelectedIndex != 0)
        {
            trclient.Visible = false;
            ddlcheque.Visible = false;
            txtcheque.Visible = true;
            LoadForm(ddlsubtype.SelectedValue);

        }
        else
        {


        }
    }

    protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
          ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1"); ClearTextBoxes(cph);
        if (ddlservice.SelectedIndex != 0)
        {
            trclient.Visible = false;
            LoadServices(ddlservice.SelectedValue);

        }
        else
        {


        }
    }


    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        Trtaxnums.Visible = false;
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
        paymenttype();
          ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1"); ClearTextBoxes(cph);
    }

    private void paymenttype()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        reset();
        ddltypeofpay.Items.Clear();
        ddlsubtype.Visible = false;
     
        ddlvendor.Visible = false;
        ddlpo.Visible = false;
        ddlservice.Visible = false;
        paytype.Visible = true;
        month.Visible = false;
        trclient.Visible = false;
        ddlsubclientid.Items.Clear();
        ddlclientid.Items.Clear();
        if (rbtntype.SelectedIndex == 0)
        {
            ddltypeofpay.Items.Add("-Select-");
            ddltypeofpay.Items.Add("Invoice Service");
            ddltypeofpay.Items.Add("Trading Supply");
            ddltypeofpay.Items.Add("Manufacturing");
            ddltypeofpay.Items.Add("Advance");
            ddltypeofpay.Items.Add("Refund");
            ddltypeofpay.Items.Add("Other Refunds");
            ddltypeofpay.Items.Add("Unsecured Loan");
            ddltypeofpay.Items.Add("Misc Taxable Receipt");
            grd.Visible = false;
            ddlcheque.Visible = false;
          
        }
        else if (rbtntype.SelectedIndex == 1)
        {
            ddltypeofpay.Items.Add("-Select-");
            ddltypeofpay.Items.Add("Trade purchasing");
            ddltypeofpay.Items.Add("Service");
            grd.Visible = true;
            ddlcheque.Visible = true;
            txtcheque.Visible = false;
        }
        else
        {
            ddlcheque.Visible = false;
            txtcheque.Visible = true;
            paytype.Visible = false;
            LoadForm("Transfer");
        }
    }

    private void subtype()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        reset();
        ddlsubtype.Visible = false;
        ddlvendor.Visible = false;
        ddlservice.Visible = false;
        Button1.Visible = false;
        trclient.Visible = false;
        if (ddltypeofpay.SelectedIndex != 0)
        {
            ddlsubtype.Items.Clear();
            if (ddltypeofpay.SelectedItem.Text == "Refund")
            {
                ddlsubtype.Visible = true;
                ddlsubtype.Items.Add("-Select Refund Type-");
                ddlsubtype.Items.Add("FD");
                ddlsubtype.Items.Add("SD");
                ddlsubtype.ToolTip = "Refund Type";
            }
            else if (ddltypeofpay.SelectedItem.Text == "Service")
            {
                if (Session["roles"].ToString() != "HoAdmin")
                {
                    ddlsubtype.Visible = true;
                    ddlsubtype.Items.Add("-Select Service Type-");
                    //ddlsubtype.Items.Add("Service Provider");
                    //ddlsubtype.Items.Add("Supplier");
                    ddlsubtype.Items.Add("General");
                    //ddlsubtype.Items.Add("Unsecured Loan");
                    ddlsubtype.ToolTip = "Service Type";
                    if (Session["roles"].ToString() == "SuperAdmin")
                        ddlsubtype.Items.Remove("Withdraw");
                    else
                        ddlsubtype.Items.Add("Withdraw");     
    
                }
                else
                {
                    ddlsubtype.Visible = true;
                    ddlsubtype.Items.Add("-Select Service Type-");
                    //ddlsubtype.Items.Add("Withdraw");
                    ddlsubtype.Items.Add("Unsecured Loan");
                    ddlsubtype.ToolTip = "Service Type";
                }

            }
            else
            {
                LoadForm(ddltypeofpay.SelectedItem.Text);
            }
        }
    }
    private void service()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        reset();
        ddlservice.Items.Clear();
        trclient.Visible = false;
        if (ddlsubtype.SelectedItem.Text == "Service Provider")
        {
            ddlservice.Visible = true;
            ddlservice.Items.Add("-Select Service Type-");
            ddlservice.Items.Add("Invoice");
            ddlservice.Items.Add("TDS");
            //ddlservice.Items.Add("Retention");
            //ddlservice.Items.Add("Hold");
            //ddlservice.Items.Add("Service Tax");
            //ddlservice.Items.Add("Trading Service Tax");
            //ddlservice.Items.Add("Service provider VAT");
            //ddlservice.Items.Add("Manufacturing Service Tax");
        }
        else if (ddlsubtype.SelectedItem.Text == "Supplier")
        {
            ddlservice.Visible = true;
            ddlservice.Items.Add("-Select Service Type-");
            ddlservice.Items.Add("Invoice");
            ddlservice.Items.Add("Hold");
            ddlservice.Items.Add("Service Excise Duty");
            ddlservice.Items.Add("Trading Excise Duty");
            ddlservice.Items.Add("Manufacturing Excise Duty");

        }
    }
    private void LoadServices(string servicetype)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
        reset();
        paymentdetails.Visible = true;
        if (servicetype == "Invoice")
        {
            laodvendor(ddlsubtype.SelectedItem.Text);
        
            txtra.Visible = false;
            lblrano.Visible = false;
            lbledc.Visible = false;
            txtedc.Visible = false;
            Invoice.Visible = true;
            cc.Visible = true;
            Dca.Visible = true;
            txtpo.Visible = false;
            ddlpono.Visible = false;
            //txtin.Visible = true;
            ddlinno.Visible = true;
            name.Visible = true;
            lblfrombank.Text = "Bank :";
            grd.DataSource = null;
            grd.DataBind();
            Invoice.Visible = false;
            FD.Visible = false;
            month.Visible = true;
            //ddlpo.Visible = true;
            ddlmonth.Visible = false;
            ddlyear.Visible = false;
            cc.Visible = false;
            Button1.Visible = false;
            trclient.Visible = false;

        }
        else if (servicetype == "TDS")
        {
            laodvendor("Service Provider");
         
            grd.DataSource = null;
            grd.DataBind();
            FD.Visible = false;
            ddlpo.Visible = false;
            month.Visible = true;
            ddlmonth.Visible = true;
            ddlyear.Visible = true;
            cc.Visible = false;
            Button1.Visible = true;
            ddlvendor.Visible = false;
            trclient.Visible = false;
        }
        else if (servicetype == "Retention" || servicetype == "Hold")
        {
            laodvendor(ddlsubtype.SelectedItem.Text);
        
            grd.DataSource = null;
            grd.DataBind();
            FD.Visible = false;
            //ddlpo.Visible = true;
            month.Visible = true;
            ddlmonth.Visible = false;
            ddlyear.Visible = false;
            cc.Visible = false;
            Button1.Visible = false;
            trclient.Visible = false;

        }
    }

    private void LoadForm(string formtype)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        reset();
        paymentdetails.Visible = true;
        Trtaxnums.Visible = false;
        if (formtype == "Invoice Service" || formtype == "Advance")
        {
            clientid(formtype);
            //fillexcise();
            cc.Visible = true;
            ddlcccode.Visible = true;
            Label7.Visible = false;          
            Invoice.Visible = true;
            if (formtype == "Advance")
            {               
                tdinv.Visible = false;
                tdinv1.Visible = false;
                tdinv2.Visible = false;
                tdinvdate.Visible = false;
                Advance.Visible = true;
                trbasic.Visible = false;
                ex.Visible = false;
                trfreigt.Visible = false;
                advtotal.Visible = true;
                trtds.Visible = false;
                tradv.Visible = false;
                tradvtds.Visible = true;
                tradvhold.Visible = true;
                NETDetails.Visible = false;
                tradvexc.Visible = true;
                txtnetex.ToolTip = Label1.Text = "Excise Duty";
               
            }
            else
            {
               
                tdinv.Visible = true;
                tdinv1.Visible = true;
                tdinv2.Visible = true;
                tdinvdate.Visible = true;
                Advance.Visible = false;
                trbasic.Visible = true;               
                ex.Visible = true;
                trfreigt.Visible = true;
                advtotal.Visible = false;
                trtds.Visible = true;
                tradv.Visible = true;
                tradvtds.Visible = false;
                tradvhold.Visible = false;
                NETDetails.Visible = true;
                tradvexc.Visible = false;
                txtnetex.ToolTip = Label1.Text = "Service Tax";
            }
            month.Visible = false;
            NET.Visible = true;
            netex.Visible = true;
            bank.Visible = true;
            ModeofPay.Visible = true;
            txtpo.Visible = false;
            ddlvendor.Visible = false;
            ddlpono.Visible = true;
            ddlinno.Visible = true;
            txtra.Visible = true;
            lblrano.Visible = true;

            

            ex.Visible = true;

            lblfrombank.Text = "Bank :";
            grd.DataSource = null;
            grd.DataBind();
            txtinvoice.Visible = false;
            ddlinno.Visible = true;
            month.Visible = false;
            trclient.Visible = true;

        }

        else if (formtype == "Trading Supply" || formtype == "Manufacturing")
        {
            
            txttax.ToolTip = lbltax.Text = "Sales Tax";
            txtnettax.ToolTip = lblnettax.Text = "Sales Tax";

            txtnetex.ToolTip = Label1.Text = "Excise duty";
         
            lblrano.Visible = true;
            cc.Visible = true;
            ddlcccode.Visible = true;
            Invoice.Visible = true;
            month.Visible = false;
            NET.Visible = true;
            NETDetails.Visible = true;
            netex.Visible = true;

            Advance.Visible = false;
            advtotal.Visible = false;
            tradvtds.Visible = false;
            tradvhold.Visible = false;
            tradvexc.Visible = false;

            ex.Visible = true;
            bank.Visible = true;
            ModeofPay.Visible = true;
            txtpo.Visible = false;
            ddlvendor.Visible = false;
            ddlinno.Visible = true;
            txtra.Visible = true;
            lblfrombank.Text = "Bank :";
            grd.DataSource = null;
            grd.DataBind();
            txtinvoice.Visible = false;
            ddlinno.Visible = true;
            month.Visible = false;
            trclient.Visible = true;
            clientid(formtype);
        }
        else if (formtype == "Unsecured Loan" || formtype == "Misc Taxable Receipt")
        {
            name.Visible = true;
            trclient.Visible = false;
         
            lblfrombank.Text = "Bank :";
        }
        else if (formtype == "Trade purchasing")
        {
            txttax.ToolTip = lbltax.Text = "Sales Tax";

            cc.Visible = true;
            //ex.Visible = false;
            txtex.Enabled = false;
            txtfre.Enabled = false;
            txtins.Enabled = false;
            txtex.Text = "0.00";
            txtfre.Text = "0.00";
            txtins.Text = "0.00";


            month.Visible = false;
            txtra.ToolTip = lblrano.Text = "Our PO No";
            lblfrombank.Text = "Bank :";
            lblrano.Visible = true;
            txtra.Visible = true;
            Invoice.Visible = true;
            lbltds.Visible = false;
            txttds.Visible = false;
            lbledc.Visible = false;
            txtedc.Visible = false;
            laodvendor("Supplier");
            ddlvendor.Visible = true;
            txtpo.Visible = true;
            ddlpono.Visible = false;
            //ddlin.Visible = false;
            ddlinno.Visible = false;
            name.Visible = true;
            //ddlpo.Visible = true;
            month.Visible = false;
            cc.Visible = false;
            Button1.Visible = false;
            trclient.Visible = false;
        }
        else if (formtype == "Supplier")
        {
            laodvendor(ddlsubtype.SelectedItem.Text);

            txttax.ToolTip = lbltax.Text = (ddlsubtype.SelectedItem.Text == "Service Provider") ? "Service Tax" : "Sales Tax";
         
            txtra.Visible = false;
            lblrano.Visible = false;
            lbledc.Visible = false;
            txtedc.Visible = false;
            Invoice.Visible = true;
            cc.Visible = true;
            Dca.Visible = true;
            txtpo.Visible = false;
            ddlpono.Visible = false;
            //txtin.Visible = true;
            ddlinno.Visible = true;
            name.Visible = true;
            lblfrombank.Text = "Bank :";
            grd.DataSource = null;
            grd.DataBind();
            Invoice.Visible = false;
            FD.Visible = false;
            //ddlpo.Visible = true;
            month.Visible = true;
            ddlmonth.Visible = false;
            ddlyear.Visible = false;
            cc.Visible = false;
            ddlservice.Visible = true;
            Button1.Visible = false;
            trclient.Visible = false;

        }
        else if (formtype == "FD")
        {
            FD.Visible = true;
            grd.DataSource = null;
         
            grd.DataBind();
            lblfrombank.Text = "Bank :";
            ddlservice.Visible = false;
            trclient.Visible = false;
        }
        else if (formtype == "SD")
        {
            cc.Visible = true;
            name.Visible = true;
            Dca.Visible = true;
            grd.DataSource = null;
       
            grd.DataBind();
            lblfrombank.Text = "Bank :";
            ddlservice.Visible = false;
            trclient.Visible = false;
        }
        else if (formtype == "General")
        {
            Dca.Visible = true;
            name.Visible = true;
            grd.DataSource = null;
        
            grd.DataBind();
            lblfrombank.Text = "Bank :";
            ddlservice.Visible = false;
            month.Visible = false;
            trclient.Visible = false;
            ddlvendor.Visible = true;
            laodvendor(formtype);

        }
        else if (formtype == "Withdraw")
        {
            ddlservice.Visible = false;
       
            month.Visible = false;
            trclient.Visible = false;
            name.Visible = true;
            lblcc.Text = "";
            lbldca.Text = "";
            lblsubdca.Text = "";
        }
        else if (formtype == "Other Refunds")
        {
            FD.Visible = true;
       
            txtfdr.Visible = false;
            fdr.Visible = false;
            grd.DataSource = null;
            grd.DataBind();
            lblfrombank.Text = "Bank :";
            ddlservice.Visible = false;
            trclient.Visible = false;
        }
        else if (formtype == "Transfer")
        {
            lbltobank.Visible = true;
      
            ddltobank.Visible = true;
            grd.DataSource = null;
            grd.DataBind();
            lblfrombank.Text = "From :";
            month.Visible = false;
            trclient.Visible = false;
            name.Visible = true;
        }
    }

    private void laodvendor(string p)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        ddlvendor.Visible = true;
        try
        {
            if (p != "General")
            {
                da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='" + p + "' and status='2' order by name asc", con);
                da.Fill(ds, "vendor");
                ddlvendor.DataTextField = "Name";
                ddlvendor.DataValueField = "vendor_id";
                ddlvendor.DataSource = ds.Tables["vendor"];
                ddlvendor.DataBind();
                ddlvendor.Items.Insert(0, "Select Vendor");
                ddlvendor.Items.Insert(1, "Select All");
            }
            else
            {
                da = new SqlDataAdapter("Select invoiceno as vendor_id from GeneralInvoices where status='3' and Mode_of_Pay='Bank'", con);
                da.Fill(ds, "vendor");
                ddlvendor.DataTextField = "vendor_id";
                ddlvendor.DataValueField = "vendor_id";
                ddlvendor.DataSource = ds.Tables["vendor"];
                ddlvendor.DataBind();
                ddlvendor.Items.Insert(0, "Select Invoice");
            }           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }       
    }

    private void loadpo(string q)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        if (ddlservice.SelectedItem.Text == "Invoice")
        {
            if (ddlsubtype.SelectedItem.Text == "Supplier")
            {
                da = new SqlDataAdapter("(select (case when cp.po_no is null then chp.po_no else cp.po_no end) po_no from (select distinct p.po_no from pending_invoice p left outer join mr_report mr ON p.po_no=mr.po_no where balance!='0' and vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax','Service Provider') AND mr.status='8') cp full outer join (select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax','Service Provider')) chp on cp.po_no=chp.po_no)", con);
            }
            else
            {
                da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype not in('Excise Duty','Supplier','VAT') and status in('3','Debit Pending') ", con);
            }
        }
        else if (ddlservice.SelectedItem.Text == "TDS")
            da = new SqlDataAdapter("select distinct po_no from pending_invoice where tds_balance!='0' and vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax') ", con);
        else if (ddlservice.SelectedItem.Text == "Retention")
            da = new SqlDataAdapter("select distinct po_no from pending_invoice where Retention_balance!='0' and vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax') ", con);
        else if (ddlservice.SelectedItem.Text == "Hold")
            da = new SqlDataAdapter("select distinct po_no from pending_invoice where Hold_balance!='0' and vendor_id='" + q + "' and paymenttype not in('Excise Duty','Service Tax') ", con);
        //else if (ddlservice.SelectedItem.Text == "Service Excise Duty")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype='Excise Duty' and po_no in (Select PO_No from purchase_details where cc_code in (Select cc_code from cost_center where cc_subtype in ('Service','Non-Performing','Capital')))", con);
        //else if (ddlservice.SelectedItem.Text == "Trading Excise Duty")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype='Excise Duty' and po_no in (Select PO_No from purchase_details where cc_code in (Select cc_code from cost_center where cc_subtype='Trading'))", con);
        //else if (ddlservice.SelectedItem.Text == "Manufacturing Excise Duty")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype='Excise Duty' and po_no in (Select PO_No from purchase_details where cc_code in (Select cc_code from cost_center where cc_subtype='Manufacturing'))", con);
        //else if (ddlservice.SelectedItem.Text == "Service Tax")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype='Service Tax' and po_no in (Select PONo from SPPO where cc_code in (Select cc_code from cost_center where cc_subtype in ('Service','Non-Performing','Capital')))", con);
        //else if (ddlservice.SelectedItem.Text == "Trading Service Tax")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype='Service Tax' and po_no in (Select PONo from SPPO where cc_code in (Select cc_code from cost_center where cc_subtype='Trading'))", con);
        //else if (ddlservice.SelectedItem.Text == "Manufacturing Service Tax")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype='Service Tax' and po_no in (Select PONo from SPPO where cc_code in (Select cc_code from cost_center where cc_subtype='Manufacturing'))", con);
        //else if (ddlservice.SelectedItem.Text == "Service provider VAT")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + q + "' and paymenttype='Service provider VAT' and po_no in (Select PONo from SPPO where cc_code in (Select cc_code from cost_center where cc_subtype in ('Service','Non-Performing','Capital')))", con);
        da.Fill(ds, "po");
        ddlpo.DataTextField = "po_no";
        ddlpo.DataValueField = "po_no";
        ddlpo.DataSource = ds.Tables["po"];
        ddlpo.DataBind();
        ddlpo.Items.Insert(0, "Select PO");
        ddlpo.Items.Insert(1, "Select All");
    }


    public void reset()
    {
        txttax.ToolTip = lbltax.Text = "Service Tax";
        lblrano.Text = txtra.ToolTip = "RA No";
        NET.Visible = false;
        NETDetails.Visible = false;
        netex.Visible = false;
        cc.Visible = false;
        FD.Visible = false;
        Dca.Visible = false;
        Invoice.Visible = false;
        paymentdetails.Visible = false;
        name.Visible = false;
        //ex.Visible = false;
        txtex.Enabled = false;
        txtfre.Enabled = false;
        txtins.Enabled = false;

        txtex.Text = "0.00";
        txtfre.Text = "0.00";
        txtins.Text = "0.00";
                  

        bank.Visible = true;
        ModeofPay.Visible = true;
        lbltobank.Visible = false;
        ddltobank.Visible = false;
        lblfrombank.Text = "From :";
        lbltds.Visible = true;
        txttds.Visible = true;
        lbledc.Visible = true;
        txtedc.Visible = true;
        grd.DataSource = null;
        grd.DataBind();
        txtname.Text = "";
    }
    
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        BankDateCheck objdate = new BankDateCheck();
        if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedItem.Text,ddltobank.SelectedItem.Text))
        {
            if (Session["roles"].ToString() == "HoAdmin")
                rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden");
            btnsubmit.Visible = false;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            string category = TypeOfPayment;
            try
            {
                if (rbtntype.SelectedIndex == 2)
                {
                    if (category == "Transfer")
                    {
                        cmd.CommandText = "sp_transfered_money";
                        cmd.Parameters.Add(new SqlParameter("@From", ddlfrom.SelectedItem.Text));
                        cmd.Parameters.Add(new SqlParameter("@To", ddltobank.SelectedItem.Text));
                        cmd.Parameters.Add(new SqlParameter("@Amount", txtamt.Text));
                        cmd.Parameters.Add(new SqlParameter("@Description", txtdesc.Text));
                        cmd.Parameters.Add(new SqlParameter("@PaymentType", TypeOfPayment));
                        cmd.Parameters.Add(new SqlParameter("@User", Session["user"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));
                        cmd.Parameters.Add(new SqlParameter("@ModeOfPay", ddlpayment.SelectedItem.Text));
                        cmd.Parameters.Add(new SqlParameter("@roles", Session["roles"].ToString()));
                        if (ddlpayment.SelectedItem.Text == "Cheque")
                        {
                            cmd.Parameters.Add(new SqlParameter("@No", ddlcheque.SelectedItem.Text));
                            cmd.Parameters.Add(new SqlParameter("@chequeid", ddlcheque.SelectedValue));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@No", txtcheque.Text));
                        }

                        cmd.Parameters.AddWithValue("@Name", txtname.Text);
                        cmd.Connection = con;
                        con.Open();
                        string msg = cmd.ExecuteScalar().ToString();
                        JavaScript.UPAlertRedirect(Page, msg, "frmBankflow.aspx");
                        con.Close();
                    }
                }
                if (rbtntype.SelectedIndex == 0)
                {
                    if (category == "Unsecured Loan" || category == "FD" || category == "SD" || category == "Other Refunds" || category == "Misc Taxable Receipt")
                    {
                        cmd.CommandText = "sp_Bank_Credit";
                        cmd.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                        cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                        cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
                        cmd.Parameters.AddWithValue("@PaymentType", category);
                        cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                        cmd.Parameters.AddWithValue("@date", txtdate.Text);
                        if (category == "SD")
                        {
                            cmd.Parameters.AddWithValue("@CC_Code", lblcc.Text);
                            cmd.Parameters.AddWithValue("@DCA_Code", lbldca.Text);
                            cmd.Parameters.AddWithValue("@Sub_DCA", lblsubdca.Text);
                            cmd.Parameters.AddWithValue("@Name", txtname.Text);
                        }
                        else if (category == "FD" || category == "Other Refunds")
                        {
                            cmd.Parameters.AddWithValue("@FDR", txtfdr.Text);
                            cmd.Parameters.AddWithValue("@Principle", txtprinciple.Text);
                            cmd.Parameters.AddWithValue("@Interest", txtintrest.Text);
                        }
                        else if (category == "Unsecured Loan" || category == "Misc Taxable Receipt")
                        {
                            cmd.Parameters.AddWithValue("@Name", txtname.Text);
                        }

                        cmd.Connection = con;
                        con.Open();
                        string msg = cmd.ExecuteScalar().ToString();
                        JavaScript.UPAlertRedirect(Page, msg, "frmBankflow.aspx");
                        con.Close();
                    }
                    else if (category == "Invoice Service" || category == "Trading Supply" || category == "Advance" || category == "Manufacturing")
                    {
                        cmd.CommandText = "sp_Invoice_Credit";
                        cmd.Parameters.AddWithValue("@CCCode", ddlcccode.Text);
                        cmd.Parameters.AddWithValue("@PO_NO", ddlpono.Text);
                        cmd.Parameters.AddWithValue("@paytype", "Add");
                        if (category != "Advance")
                        {
                            cmd.Parameters.AddWithValue("@InvoiceNo", ddlinno.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                            cmd.Parameters.AddWithValue("@RA_NO", txtra.Text);
                            cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);

                            cmd.Parameters.AddWithValue("@Total", txttotal.Text);
                            cmd.Parameters.AddWithValue("@TDS", txttds.Text);
                            cmd.Parameters.AddWithValue("@WCT", txtedc.Text);
                            cmd.Parameters.AddWithValue("@Retention", txtretention.Text);
                            cmd.Parameters.AddWithValue("@Advance", txtadvance.Text);
                            cmd.Parameters.AddWithValue("@Hold", txthold.Text);
                            cmd.Parameters.AddWithValue("@AnyOther", txtother.Text);
                        }
                        else if (category == "Advance")
                        {
                            cmd.Parameters.AddWithValue("@BasicValue", txtadvbasic.Text);
                            cmd.Parameters.AddWithValue("@TDS", TxtAdvtds.Text);
                            cmd.Parameters.AddWithValue("@WCT", TxtAdvwct.Text);
                            cmd.Parameters.AddWithValue("@Hold", Txtadvhold.Text);
                            cmd.Parameters.AddWithValue("@AnyOther", TxtAdvother.Text);
                            //cmd.Parameters.AddWithValue("@category", "Advance");
                            cmd.Parameters.AddWithValue("@clientid", ddlclientid.SelectedValue);
                            cmd.Parameters.AddWithValue("@subclientid", ddlsubclientid.SelectedValue);
                            if (ddlvatno.SelectedItem.Text != "Select")
                                cmd.Parameters.AddWithValue("@vatno", ddlvatno.SelectedItem.Text);
                            if (ViewState["CC_subtype"].ToString() == "Service")
                            {
                                cmd.Parameters.AddWithValue("@NetServiceTax", txtnetadvsevtax.Text);
                                cmd.Parameters.AddWithValue("@ServiceTax", txtAdvStax.Text);
                                if (ddlservicetax.SelectedItem.Text != "Select")
                                    cmd.Parameters.AddWithValue("@Srtxno", ddlservicetax.SelectedItem.Text);

                                // cmd.Parameters.AddWithValue("@type", "Service"); 
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ExciseDuty", txtex.Text);
                                cmd.Parameters.AddWithValue("@NetExciseDuty", txtnetex.Text);
                                if (ddlExcno.SelectedItem.Text != "Select")
                                    cmd.Parameters.AddWithValue("@exciseno", ddlExcno.SelectedItem.Text);
                                //cmd.Parameters.AddWithValue("@type", "Excise"); 
                            }

                            cmd.Parameters.AddWithValue("@Netsalestax", txtnetadvsaltax.Text);
                            cmd.Parameters.AddWithValue("@salestax", txtAdvsaltax.Text);

                            cmd.Parameters.AddWithValue("@Total", TxtAdvtotal.Text);

                        }

                        if (category == "Trading Supply" || category == "Manufacturing")
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
                        else if (category == "Invoice Service")
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
                        cmd.Parameters.AddWithValue("@PaymentType", ddltypeofpay.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                        cmd.Parameters.AddWithValue("@name", txtname.Text);
                        cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));


                        cmd.Connection = con;
                        con.Open();
                        string msg = cmd.ExecuteScalar().ToString();
                        JavaScript.UPAlertRedirect(Page, msg, "frmBankflow.aspx");
                        con.Close();
                    }
                }
                if (rbtntype.SelectedIndex == 1)
                {
                    if (category == "Trade purchasing" || category == "Supplier" || category == "Service Provider")
                    {
                        string invoicenos = "";
                        foreach (GridViewRow rec in grd.Rows)
                        {
                            if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                                invoicenos = invoicenos + grd.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                        }
                        if (invoicenos != "")
                        {
                            if (ddlservice.SelectedItem.Text == "TDS")
                            {

                                da = new SqlDataAdapter("sp_TDSandRetention_payment", con);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@PaymentType", TypeOfPayment);

                            }
                            else if (ddlservice.SelectedItem.Text == "Retention")
                            {

                                da = new SqlDataAdapter("sp_VendorRetention_payment", con);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@PaymentType", TypeOfPayment);

                            }
                            else if (ddlservice.SelectedItem.Text == "Hold")
                            {
                                da = new SqlDataAdapter("sp_VendorHold_payment", con);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@PaymentType", TypeOfPayment);


                            }
                            else if (ddlservice.SelectedItem.Text == "Invoice")
                            {
                                da = new SqlDataAdapter("sp_Debit_pending", con);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@PaymentType", category);
                            }
                            //else if (ddlservice.SelectedItem.Text == "Service Tax" || ddlservice.SelectedItem.Text == "Trading Service Tax" || ddlservice.SelectedItem.Text == "Manufacturing Service Tax")
                            //{
                            //    da = new SqlDataAdapter("sp_Debit_pending", con);
                            //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            //    da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Service Tax");
                            //}
                            //else if (ddlservice.SelectedItem.Text == "Service provider VAT")
                            //{
                            //    da = new SqlDataAdapter("sp_Debit_pending", con);
                            //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            //    da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Service provider VAT");
                            //}
                            //else if (ddlservice.SelectedItem.Text == "Service Excise Duty" || ddlservice.SelectedItem.Text == "Trading Excise Duty" || ddlservice.SelectedItem.Text == "Manufacturing Excise Duty")
                            //{
                            //    da = new SqlDataAdapter("sp_Debit_pending", con);
                            //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            //    da.SelectCommand.Parameters.AddWithValue("@PaymentType", "Excise Duty");
                            //}
                        }

                        da.SelectCommand.Parameters.AddWithValue("@invoicenos", invoicenos);
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", lblcc.Text);
                        da.SelectCommand.Parameters.AddWithValue("@DCA_Code", lbldca.Text);
                        da.SelectCommand.Parameters.AddWithValue("@Sub_DCA", lblsubdca.Text);
                        da.SelectCommand.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                        da.SelectCommand.Parameters.AddWithValue("@Name", txtname.Text);
                        da.SelectCommand.Parameters.AddWithValue("@Description", txtdesc.Text);
                        da.SelectCommand.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                        if (ddlpayment.SelectedItem.Text == "Cheque")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@No", ddlcheque.SelectedItem.Text);
                            da.SelectCommand.Parameters.AddWithValue("@chequeid", ddlcheque.SelectedValue);
                        }
                        else
                        {
                            da.SelectCommand.Parameters.AddWithValue("@No", txtcheque.Text);
                        }
                        da.SelectCommand.Parameters.AddWithValue("@User", Session["user"].ToString());
                        da.SelectCommand.Parameters.AddWithValue("@Amount", txtamt.Text);
                        da.SelectCommand.Parameters.AddWithValue("@date", txtdate.Text);
                        da.Fill(ds, "InvoiceTranNo");
                        if (ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[0].ToString() == "Sucessfull")
                        {
                            showalertprint("Sucessfull", ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[1].ToString());
                            fillgrid();
                            Clear();
                        }
                    }
                    else if (category == "General" || category == "Withdraw" || category == "Unsecured Loan" || category == "Misc Taxable Receipt")
                    {
                        if (category == "General")
                        {
                            da = new SqlDataAdapter("sp_General_Payment", con);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@invoiceno", ddlvendor.SelectedItem.Text);
                            da.SelectCommand.Parameters.AddWithValue("@duedate", lblduedate.Text);
                        }
                        else if (category == "Withdraw")
                        {

                            da = new SqlDataAdapter("sp_Bank_Debit", con);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                        }
                        else if (category == "Unsecured Loan")
                        {
                            da = new SqlDataAdapter("sp_Unsecured_Loan", con);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        }
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", lblcc.Text);
                        da.SelectCommand.Parameters.AddWithValue("@Name", txtname.Text);
                        da.SelectCommand.Parameters.AddWithValue("@DCA_Code", lbldca.Text);
                        if (lblsubdca.Text != "")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@Sub_DCA", lblsubdca.Text);
                        }
                        da.SelectCommand.Parameters.AddWithValue("@PaymentType", TypeOfPayment);
                        da.SelectCommand.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                        da.SelectCommand.Parameters.AddWithValue("@Description", txtdesc.Text);
                        da.SelectCommand.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                        if (ddlpayment.SelectedItem.Text == "Cheque")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@No", ddlcheque.SelectedItem.Text);
                            da.SelectCommand.Parameters.AddWithValue("@chequeid", ddlcheque.SelectedValue);
                        }
                        else
                        {

                            da.SelectCommand.Parameters.AddWithValue("@No", txtcheque.Text);

                        }
                        da.SelectCommand.Parameters.AddWithValue("@User", Session["user"].ToString());
                        da.SelectCommand.Parameters.AddWithValue("@Amount", txtamt.Text);
                        da.SelectCommand.Parameters.AddWithValue("@date", txtdate.Text);
                        da.Fill(ds, "TranNo");
                        if (ds.Tables["TranNo"].Rows[0].ItemArray[0].ToString() == "Sucessfull")
                        {
                            showalertprint("Sucessfull", ds.Tables["TranNo"].Rows[0].ItemArray[1].ToString());
                            Clear();
                        }
                        else
                        {
                            btnsubmit.Visible = true;
                            JavaScript.UPAlert(Page, ds.Tables["TranNo"].Rows[0].ItemArray[0].ToString());
                        }

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
        else
        {
            JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
        }
    }

    public string TypeOfPayment
    {
        get
        {
            if (rbtntype.SelectedIndex != 2)
            {
                if (ddltypeofpay.SelectedValue != "Refund" && ddltypeofpay.SelectedValue != "Service" && ddltypeofpay.SelectedIndex != 0)
                    return ddltypeofpay.SelectedValue;
                else if ((ddltypeofpay.SelectedValue == "Refund" || ddltypeofpay.SelectedValue == "Service") && ddlsubtype.SelectedIndex != 0)
                    return ddlsubtype.SelectedValue;
                else
                    return "";
            }
            else
            {
                return "Transfer";
            }
        }
        set
        {
            //strUsername = value;
        }
    }

    public void showalert(string message)
    {
        string script = "alert(\"" + message + "\");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
    }
    protected void ddlinno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
        try
        {
              ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1"); ClearTextBoxes(cph);
            if (ddlinno.SelectedItem.Text != "Select Invoice")
            {
              
                da = new SqlDataAdapter("select [CC_Code],[PaymentType],[InvoiceNo],[invoice_date],[PO_NO],[BasicValue],[ServiceTax],[EDcess],[HEDcess],[ExciseDuty],[Frieght],[Insurance],[Total],[Salestax],[RA_NO],[Client_id],subclient_id,(Select cc_subtype from cost_center where cc_code=i.cc_code),invoicetype from invoice i where  status='2' and InvoiceNo='" + ddlinno.SelectedValue + "'", con);
                da.Fill(ds, "pending");
                if (ds.Tables["pending"].Rows.Count > 0)
                {
                    ddlpayment.SelectedItem.Text = ds.Tables["pending"].Rows[0].ItemArray[1].ToString();
                    string type = ddlpayment.SelectedItem.Text;
                    string pono = ds.Tables["pending"].Rows[0].ItemArray[4].ToString();

                    da = new SqlDataAdapter("select MobilisationAdv from po where po_no='"+pono+"'", con);
                    da.Fill(ds1, "Advance");
                    if (ds1.Tables["Advance"].Rows.Count > 0 && ds1.Tables["Advance"].Rows[0][0].ToString()!="")
                    {
                    if (ds1.Tables["Advance"].Rows[0][0].ToString() == "Yes")
                        txtadvance.Enabled = true;
                    else
                        {
                        txtadvance.Enabled = false;
                            txtadvance.Text = "0";
                        }
                    }
                    else
                        txtadvance.Enabled = true;
                    

                    ddlcccode.SelectedValue = ds.Tables["pending"].Rows[0].ItemArray[0].ToString();
                    txtindt.Text = ds.Tables["pending"].Rows[0].ItemArray[3].ToString();
                    ddlinno.SelectedValue = ds.Tables["pending"].Rows[0].ItemArray[2].ToString();
                    ddlpo.SelectedValue = ds.Tables["pending"].Rows[0].ItemArray[4].ToString();
                    txtbasic.Text = ds.Tables["pending"].Rows[0].ItemArray[5].ToString().Replace(".0000", ".00");
                    txted.Text = ds.Tables["pending"].Rows[0].ItemArray[7].ToString().Replace(".0000", ".00");
                    txthed.Text = ds.Tables["pending"].Rows[0].ItemArray[8].ToString().Replace(".0000", ".00");
                    txttotal.Text = ds.Tables["pending"].Rows[0].ItemArray[12].ToString().Replace(".0000", ".00");
                    txtra.Text = ds.Tables["pending"].Rows[0].ItemArray[14].ToString();
                    ddlclientid.SelectedItem.Text = ds.Tables["pending"].Rows[0].ItemArray[15].ToString();
                    ddlsubclientid.SelectedItem.Text = ds.Tables["pending"].Rows[0].ItemArray[16].ToString();

                    if (ds.Tables["pending"].Rows[0].ItemArray[17].ToString() == "Service")
                    {

                        txttax.Text = ds.Tables["pending"].Rows[0].ItemArray[6].ToString().Replace(".0000", ".00");
                        lbltax.Text = "Service Tax";
                        Label1.Text = "Net Service Tax";
                    }
                    else
                    {
                        txtfre.Enabled = true;
                        txtex.Enabled = true;
                        txtins.Enabled = true;
                        txttax.Text = ds.Tables["pending"].Rows[0].ItemArray[13].ToString().Replace(".0000", ".00");
                        txtex.Text = ds.Tables["pending"].Rows[0].ItemArray[9].ToString().Replace(".0000", ".00");
                        txtfre.Text = ds.Tables["pending"].Rows[0].ItemArray[10].ToString();
                        txtins.Text = ds.Tables["pending"].Rows[0].ItemArray[11].ToString();
                        lbltax.Text = "Sales Tax";
                        lblnettax.Text = "Sales Tax";
                        Label1.Text = "Excise duty";
                        Label4.Text = "Excise duty";
                    }
                    if (ds.Tables["pending"].Rows[0].ItemArray[18].ToString() == "SEZ/Service Tax exumpted Invoice" && ddltypeofpay.SelectedItem.Text == "Invoice Service")
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

                    else if (ds.Tables["pending"].Rows[0].ItemArray[18].ToString() == "VAT/Material Supply" && ddltypeofpay.SelectedItem.Text == "Invoice Service")
                    {
                        txttax.Text = ds.Tables["pending"].Rows[0].ItemArray[13].ToString().Replace(".0000", ".00");
                        txtfre.Text = ds.Tables["pending"].Rows[0].ItemArray[10].ToString();
                        txtins.Text = ds.Tables["pending"].Rows[0].ItemArray[11].ToString();
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
                        Label4.Text = "Service Tax";
                        txted.Enabled = false;
                        txthed.Enabled = false;
                        txtex.Enabled = false;
                        ViewState["ServiceInvoice"] = ds.Tables["pending"].Rows[0].ItemArray[18].ToString();
                        hf2.Value = ds.Tables["pending"].Rows[0].ItemArray[18].ToString();
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
                        txtNetED.Enabled = true;
                        txtNetHED.Enabled = true;
                        Label1.Text = "Excise duty";
                        Label4.Text = "Excise duty";
                    }
                    if (ds.Tables["pending"].Rows[0].ItemArray[18].ToString() == "Service Tax Invoice" && ddltypeofpay.SelectedItem.Text == "Invoice Service")
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
                    }
                }
               

            }
            else
            {
                ValuesClear();
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void fillgrid()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        if (ddlsubtype.SelectedItem.Text == "Service Provider")
        {
            if (ddlservice.SelectedItem.Text == "TDS")
            {
                if (ddlyear.SelectedIndex != 0)
                {
                    if (ddlmonth.SelectedIndex != 0)
                    {
                        string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                        //da = new SqlDataAdapter("select invoiceno,CC_code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where paymenttype='Service Provider' and tds_balance!=0 and datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy, con);
                        da = new SqlDataAdapter("select invoiceno,CC_code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where paymenttype='Service Provider' and tds_balance!=0 and datepart(mm,Inv_Making_Date)=" + ddlmonth.SelectedValue + " and datepart(yy,Inv_Making_Date)=" + yy, con);
                    }
                    else
                    {
                        //da = new SqlDataAdapter("select invoiceno,CC_code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where paymenttype='Service Provider' and tds_balance!=0 and  convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'", con);
                        da = new SqlDataAdapter("select invoiceno,CC_code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where paymenttype='Service Provider' and tds_balance!=0 and  convert(datetime,Inv_Making_Date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'", con);
                    }

                }
            }
            else if (ddlservice.SelectedItem.Text == "Invoice" && ddlpo.SelectedItem.Text != "Select All")
                da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where po_no='" + ddlpo.SelectedValue + "' and balance!=0   and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype in ('Service Provider','Service provider VAT','SERVICE TAX','Excise Duty') and status in ('3','Debit Pending')", con);
            else if (ddlservice.SelectedItem.Text == "Invoice" && ddlpo.SelectedItem.Text == "Select All")
                da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where  balance!=0   and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype in ('Service Provider','Service provider VAT','SERVICE TAX','Excise Duty') and status in ('3','Debit Pending')", con);

            else if (ddlservice.SelectedItem.Text == "Retention" && ddlpo.SelectedItem.Text != "Select All")
                da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where paymenttype='Service Provider' and retention_balance!=0 and po_no='" + ddlpo.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "'", con);
            else if (ddlservice.SelectedItem.Text == "Retention" && ddlpo.SelectedItem.Text == "Select All")
                da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where paymenttype='Service Provider' and retention_balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "'", con);
            else if (ddlservice.SelectedItem.Text == "Hold" && ddlpo.SelectedItem.Text != "Select All")
                da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where paymenttype='Service Provider' and hold_balance!=0 and po_no='" + ddlpo.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "'", con);
            else if (ddlservice.SelectedItem.Text == "Hold" && ddlpo.SelectedItem.Text == "Select All")
                da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where paymenttype='Service Provider' and hold_balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "'", con);
            //else if ((ddlservice.SelectedItem.Text == "Service Tax" || ddlservice.SelectedItem.Text == "Trading Service Tax" || ddlservice.SelectedItem.Text == "Manufacturing Service Tax") && ddlpo.SelectedItem.Text != "Select All")
            //    da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where po_no='" + ddlpo.SelectedValue + "' and balance!=0   and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Service Tax'", con);
            //else if (ddlservice.SelectedItem.Text == "Service Tax" && ddlpo.SelectedItem.Text == "Select All")
            //    da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where  balance!=0   and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Service Tax' and po_no in (Select PONo from SPPO where cc_code in (Select cc_code from cost_center where cc_type in ('Performing','Non-Performing','Capital') and (cc_subtype='Service' or cc_subtype is null)))", con);
            //else if (ddlservice.SelectedItem.Text == "Trading Service Tax" && ddlpo.SelectedItem.Text == "Select All")
            //    da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where  balance!=0   and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Service Tax' and po_no in (Select PONo from SPPO where cc_code in (Select cc_code from cost_center where cc_subtype='Trading'))", con);
            //else if (ddlservice.SelectedItem.Text == "Manufacturing Service Tax" && ddlpo.SelectedItem.Text == "Select All")
            //    da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where  balance!=0   and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Service Tax' and po_no in (Select PONo from SPPO where cc_code in (Select cc_code from cost_center where cc_subtype='Manufacturing'))", con);
            //else if (ddlservice.SelectedItem.Text == "Service provider VAT" && ddlpo.SelectedItem.Text != "Select All")
            //    da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where po_no='" + ddlpo.SelectedValue + "' and balance!=0   and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Service provider VAT'", con);
            //else if (ddlservice.SelectedItem.Text == "Service provider VAT" && ddlpo.SelectedItem.Text == "Select All")
            //    da = new SqlDataAdapter("select invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice where  balance!=0   and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Service provider VAT' and po_no in (Select PONo from SPPO where cc_code in (Select cc_code from cost_center where cc_type in ('Performing','Non-Performing','Capital') and (cc_subtype='Service' or cc_subtype is null)))", con);

        }
        else if (ddlsubtype.SelectedItem.Text == "Supplier" && ddlservice.SelectedItem.Text == "Invoice")
        {
            da = new SqlDataAdapter("GetInvoices", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@PONO", SqlDbType.VarChar).Value = ddlpo.SelectedValue;
            if (ddlpo.SelectedItem.Text != "Select All")
            {
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = '1';

            }
            else if (ddlpo.SelectedItem.Text == "Select All")
            {
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = '2';
            }
            da.SelectCommand.Parameters.AddWithValue("@VendorId", SqlDbType.VarChar).Value = ddlvendor.SelectedValue;


            //da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(total,0)as total ,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice i where i.po_no='" + ddlpo.SelectedValue + "' and balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Supplier'", con);
        }

        else if (ddlsubtype.SelectedItem.Text == "Supplier" && ddlservice.SelectedItem.Text == "Hold" && ddlpo.SelectedItem.Text != "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(total,0)as total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice i where i.po_no='" + ddlpo.SelectedValue + "' and hold_balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Supplier'", con);
        }
        else if (ddlsubtype.SelectedItem.Text == "Supplier" && ddlservice.SelectedItem.Text == "Hold" && ddlpo.SelectedItem.Text == "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(total,0)as total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice i where  hold_balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Supplier'", con);
        }
        else if (ddlsubtype.SelectedItem.Text == "Supplier" && (ddlservice.SelectedItem.Text == "Service Excise Duty" || ddlservice.SelectedItem.Text == "Trading Excise Duty" || ddlservice.SelectedItem.Text == "Manufacturing Excise Duty") && ddlpo.SelectedItem.Text != "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(total,0)as total ,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice i where i.po_no='" + ddlpo.SelectedValue + "' and balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Excise Duty'", con);
        }
        else if (ddlsubtype.SelectedItem.Text == "Supplier" && ddlservice.SelectedItem.Text == "Service Excise Duty" && ddlpo.SelectedItem.Text == "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(total,0)as total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice i where  balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Excise Duty' and po_no in (Select PO_No from purchase_details where cc_code in (Select cc_code from cost_center where cc_type in ('Performing','Non-Performing','Capital') and (cc_subtype='Service' or cc_subtype is null)))", con);
        }
        else if (ddlsubtype.SelectedItem.Text == "Supplier" && ddlservice.SelectedItem.Text == "Trading Excise Duty" && ddlpo.SelectedItem.Text == "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(total,0)as total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice i where  balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Excise Duty' and po_no in (Select PO_No from purchase_details where cc_code in (Select cc_code from cost_center where cc_subtype='Trading'))", con);
        }
        else if (ddlsubtype.SelectedItem.Text == "Supplier" && ddlservice.SelectedItem.Text == "Manufacturing Excise Duty" && ddlpo.SelectedItem.Text == "Select All")
        {
            da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,isnull(total,0)as total,isnull(NetAmount,0)as[NetAmount],isnull(tds_balance,0)as[TDS],isnull(Retention_balance,0)as[Retention],isnull(Hold_balance,0)as[Hold],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date,status from pending_invoice i where  balance!=0  and vendor_id='" + ddlvendor.SelectedValue + "' and paymenttype='Excise Duty' and po_no in (Select PO_No from purchase_details where cc_code in (Select cc_code from cost_center where cc_subtype='Manufacturing'))", con);
        }
        da.Fill(ds, "fill");
        grd.DataSource = ds.Tables["fill"];
        grd.DataBind();

    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataTable myTable = new DataTable();
        //myTable.Columns.Add("InvoiceNo");

        //DataRow myRow = null;
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            TDS += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS"));
            Retention += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Retention"));
            Hold += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hold"));
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = String.Format("Rs. {0:#,##,##,###.00}", Total);
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", NetAmount);
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", TDS);
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", Retention);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Hold);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Balance);

        }
    }
    private decimal Balance = (decimal)0.0;
    private decimal Total = (decimal)0.0;
    private decimal NetAmount = (decimal)0.0;
    private decimal TDS = (decimal)0.0;
    private decimal Retention = (decimal)0.0;
    private decimal Hold = (decimal)0.0;
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rec in grd.Rows)
        {
            CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");
            CheckBox chkAll = (CheckBox)grd.HeaderRow.FindControl("chkAll");
        }
    }
    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1"); ClearTextBoxes(cph);
        grd.DataSource = null;
        grd.DataBind();
        if (ddlsubtype.SelectedItem.Text != "General")
        {
            ddlpo.Visible = true;
            Button1.Visible = true;
            name.Visible = true;
            if (ddlvendor.SelectedIndex != 0)
            {
                loadpo(ddlvendor.SelectedValue);
                da = new SqlDataAdapter("Select vendor_name from vendor where vendor_id='" + ddlvendor.SelectedValue + "'", con);
                da.Fill(ds, "name");
                if (ds.Tables["name"].Rows.Count > 0)
                    txtname.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
            }
        }
        else
        {
            cc.Visible = true;
            ddlcccode.Visible = false;
            try
            {
                da = new SqlDataAdapter("Select Invoiceno,cc_code,dca_code,subdca_code,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,Description,name,replace(amount,'.0000','.00')as amount from GeneralInvoices where invoiceno='" + ddlvendor.SelectedItem.Text + "'", con);
                da.Fill(ds, "rep");
                if (ds.Tables["rep"].Rows.Count > 0)
                {
                    Label7.Visible = true;
                    lblcc.Text = ds.Tables["rep"].Rows[0]["cc_code"].ToString();
                    lbldca.Text = ds.Tables["rep"].Rows[0]["dca_code"].ToString();
                    lblsubdca.Text = ds.Tables["rep"].Rows[0]["subdca_code"].ToString();
                    lblduedate.Text = ds.Tables["rep"].Rows[0]["date"].ToString();
                    txtdesc.Text = ds.Tables["rep"].Rows[0]["Description"].ToString();
                    txtname.Text = ds.Tables["rep"].Rows[0]["name"].ToString();
                    txtamt.Text = ds.Tables["rep"].Rows[0]["amount"].ToString();
                    hf1.Value = ds.Tables["rep"].Rows[0]["amount"].ToString();
                    //txtamt.Attributes.Add("readonly", "1");
                }
            }
            catch (Exception ex)
            {
                Utilities.CatchException(ex);
                JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
            }
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

    [WebMethod]
    public static string IsvendorAvailable(string vendor)
    {
        SqlConnection conwb = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlCommand cmdwb = new SqlCommand("Select vendor_name from vendor where vendor_id='" + vendor + "'", conwb);
        SqlDataReader drwb;
        cmdwb.Connection = conwb;
        conwb.Open();
        drwb = cmdwb.ExecuteReader();
        drwb.Read();
        if (drwb.HasRows)
        {
            string vendorname = drwb["vendor_name"].ToString();
            return vendorname;
        }
        else
        {
            string vendorname = "";
            return vendorname;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
        txtamt.Text = "";

    }

    protected void ddlpayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        if (rbtntype.SelectedIndex == 1)
        {
            if (ddlpayment.SelectedItem.Text == "Cheque")
            {
                ddlcheque.Visible = true;
                txtcheque.Visible = false;
            }
            else
            {
                ddlcheque.Visible = false;
                txtcheque.Visible = true;
            }
        }
        else if (rbtntype.SelectedIndex == 2)
        {
            if (ddlpayment.SelectedItem.Text == "Cheque")
            {
                ddlcheque.Visible = true;
                txtcheque.Visible = false;
            }
            else
            {
                ddlcheque.Visible = false;
                txtcheque.Visible = true;

            }
        }
        else
        {
            ddlcheque.Visible = false;
            txtcheque.Visible = true;
        }
    }
    protected void ddlfrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        ddlcheque.Items.Clear();
        da = new SqlDataAdapter("select cn.chequeno,cn.id from cheque_nos cn join cheque_Master cm on cn.chequeid=cm.chequeid where cm.bankname='" + ddlfrom.SelectedItem.Text + "' and cn.status='2'", con);
        da.Fill(ds, "chequeno");
        if (ds.Tables["chequeno"].Rows.Count > 0)
        {
            ddlcheque.DataSource = ds.Tables["chequeno"];
            ddlcheque.DataTextField = "chequeno";
            ddlcheque.DataValueField = "id";
            ddlcheque.DataBind();
            ddlcheque.Items.Insert(0, "Select");
        }
        else
        {
            ddlcheque.Items.Insert(0, "Select");
        }
    }
   
    public void showalertprint(string message, string id)
    {
        string script = @"window.alert('" + message + "');window.location ='frmBankflow.aspx';window.open('Bankpaymentprint.aspx?id=" + id + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert", script, true);

    }
    public void clientid(string s)
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        ddlclientid.Items.Clear();
        try
        {
            if (s == "Invoice Service")
            {
                da = new SqlDataAdapter("Select client_id from client where SUBSTRING(client_id,1,2)='SC'", con);
            }
            else if (s == "Advance")
            {
                da = new SqlDataAdapter("select distinct client_id from po where MobilisationAdv='Yes' and status='3'",con);
            }
            else if (s == "Trading Supply")
            {
                da = new SqlDataAdapter("Select client_id from client where SUBSTRING(client_id,1,2)='TC'", con);
            }
            else
            {
                da = new SqlDataAdapter("Select client_id from client ", con);
            }
            da.Fill(ds, "client");
            if (ds.Tables["client"].Rows.Count > 0)
            {
                ddlclientid.DataTextField = "client_id";
                ddlclientid.DataValueField = "client_id";
                ddlclientid.DataSource = ds.Tables["client"];
                ddlclientid.DataBind();
                ddlclientid.Items.Insert(0, "Select");
                ddlsubclientid.Items.Insert(0, "Select");
            }
            else
            {
                ddlclientid.Items.Insert(0, "Select");
                ddlsubclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    public void subclientid()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        ddlsubclientid.Items.Clear();
        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Advance")
                da = new SqlDataAdapter("select distinct subclient_id from po where MobilisationAdv='Yes' and client_id='" + ddlclientid.SelectedItem.Text + "' and status='3'", con);
            else
            da = new SqlDataAdapter("Select subclient_id from subclient where client_id='" + ddlclientid.SelectedItem.Text + "' ", con);
            da.Fill(ds, "subclient");
            if (ds.Tables["subclient"].Rows.Count > 0)
            {
                ddlsubclientid.DataTextField = "subclient_id";
                ddlsubclientid.DataValueField = "subclient_id";
                ddlsubclientid.DataSource = ds.Tables["subclient"];
                ddlsubclientid.DataBind();
                ddlsubclientid.Items.Insert(0, "Select");
            }
            else
            {
                ddlsubclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void clientname()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
        try
        {
            da = new SqlDataAdapter("Select client_name from client where client_id='" + ddlclientid.SelectedItem.Text + "'", con);
            da.Fill(ds, "clientname");
            da.Fill(ds, "clientname");
            if (ds.Tables["clientname"].Rows.Count > 0)
            {
                txtname.Text = ds.Tables["clientname"].Rows[0].ItemArray[0].ToString();
                lblcccode.Text = ds.Tables["clientname"].Rows[0].ItemArray[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void subclientname()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden");

        try
        {
            da = new SqlDataAdapter("select branch  from subclient where subclient_id='" + ddlsubclientid.SelectedItem.Text + "'", con);
            da.Fill(ds, "subclientname");
            da.Fill(ds, "subclientname");
            if (ds.Tables["subclientname"].Rows.Count > 0)        
                
                Label9.Text = ds.Tables["subclientname"].Rows[0].ItemArray[0].ToString();
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }


    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        clientname();
        subclientid();      
    }

    public void Clear()
    {
        txtdate.Text = "";
        txtdesc.Text = "";
        txtamt.Text = "";
        txtcheque.Text = "";
        txtcheque.Visible = true;
        ddlcheque.Visible = false;
        CascadingDropDown9.SelectedValue = "";
        CascadingDropDown1.SelectedValue = "";
    }
   

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void fillCC()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
            Trtaxnums.Visible = false;
            ddlcccode.Items.Clear();
            ddlpono.Items.Clear();
            ddlinno.Items.Clear();    
          
            ValuesClear();
            if (ddltypeofpay.SelectedItem.Text == "Invoice Service")
            {
                da = new SqlDataAdapter("Select distinct i.cc_code,cc_name,(i.cc_code+','+cc_name) as [Name] from invoice i join cost_center c on i.cc_code=c.cc_code where subclient_id='" + ddlsubclientid.SelectedItem.Text + "'  and c.status in ('Old','New') and c.cc_subtype='Service' order by i.cc_code asc", con);
            }
            if (ddltypeofpay.SelectedItem.Text == "Advance")
            {
                da = new SqlDataAdapter("select distinct p.cc_code,cc_name,(p.cc_code+','+cc_name) as [Name] from po p join Cost_Center cc on p.CC_code=cc.cc_code where subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and MobilisationAdv='Yes' and p.status='3'", con);
            }

            else if (ddltypeofpay.SelectedItem.Text == "Trading Supply")
            {
               
                da = new SqlDataAdapter("Select distinct i.cc_code,cc_name,(i.cc_code+','+cc_name) as [Name] from invoice i join cost_center c on i.cc_code=c.cc_code where subclient_id='" + ddlsubclientid.SelectedItem.Text + "'  and c.status in ('Old','New') and c.cc_subtype='Trading' order by i.cc_code asc", con);

            }
            else if (ddltypeofpay.SelectedItem.Text == "Manufacturing")
            {
              
                da = new SqlDataAdapter("Select distinct i.cc_code,cc_name,(i.cc_code+','+cc_name) as [Name] from invoice i join cost_center c on i.cc_code=c.cc_code where subclient_id='" + ddlsubclientid.SelectedItem.Text + "'  and c.status in ('Old','New') and c.cc_subtype='Manufacturing' order by i.cc_code asc", con);

            }
            da.Fill(ds, "CCType");
            if (ds.Tables["CCType"].Rows.Count > 0)
            {
                ddlcccode.DataTextField = "Name";
                ddlcccode.DataValueField = "cc_code";

                ddlcccode.DataSource = ds.Tables["CCType"];
                ddlcccode.DataBind();
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
            else
            {
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
              ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1"); ClearTextBoxes(cph);
            ddlpono.Items.Clear();
            ddlinno.Items.Clear();
            ValuesClear();
            if (ddltypeofpay.SelectedItem.Text == "Advance")
            {
                da = new SqlDataAdapter("select po_no,CC_SubType from po p join Cost_Center cc on p.CC_code=cc.cc_code where MobilisationAdv='Yes' and p.CC_code='" + ddlcccode.SelectedValue + "' and p.status='3'", con);
                 da.Fill(ds, "CreditPo");
                ViewState["CC_subtype"]=ds.Tables["CreditPo"].Rows[0][1].ToString();
            }
            else if (ddltypeofpay.SelectedItem.Text != "Advance" || ddltypeofpay.SelectedItem.Text != "-Select-")
            {
            da = new SqlDataAdapter("Select distinct po_no from invoice where cc_code='" + ddlcccode.SelectedValue + "' and subclient_id='" + ddlsubclientid.SelectedItem.Text + "' AND paymenttype='"+ddltypeofpay.SelectedValue+"'", con);
            da.Fill(ds, "CreditPo");
            }
            if (ds.Tables["CreditPo"].Rows.Count > 0)
            {
                ddlpono.DataTextField = "po_no";
                ddlpono.DataValueField = "po_no";
                ddlpono.DataSource = ds.Tables["CreditPo"];
                ddlpono.DataBind();
                ddlpono.Items.Insert(0, "Select PO");
            }
            else
            {
                ddlpono.Items.Insert(0, "Select PO");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void ddlpono_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                rbtntype.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); 
       
            if (ddltypeofpay.SelectedItem.Text != "Advance")
            {

            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1"); ClearTextBoxes(cph);
            ddlinno.Items.Clear();
            da = new SqlDataAdapter("Select InvoiceNo from invoice where po_no ='" + ddlpono.SelectedItem.Text + "' and status='2' and cc_code='" + ddlcccode.SelectedValue + "' ", con);
            da.Fill(ds, "inno");
            if (ds.Tables["inno"].Rows.Count > 0)
            {
                ddlinno.DataTextField = "InvoiceNo";
                ddlinno.DataValueField = "InvoiceNo";
                ddlinno.DataSource = ds.Tables["inno"];
                ddlinno.DataBind();
                ddlinno.Items.Insert(0, "Select Invoice");
            }
            else
            {
                ddlinno.Items.Insert(0, "Select Invoice");
            }
                da = new SqlDataAdapter("select isnull(sum(advance),0)totaldeduction from invoice where PaymentType!='Advance' and cc_code='" + ddlcccode.SelectedValue + "' and status not in ('cancel','1','2') and po_no='" + ddlpono.SelectedValue + "';select isnull(sum(total),0)totalcredit from invoice where PaymentType='Advance'  and cc_code='" + ddlcccode.SelectedValue + "' and po_no='" + ddlpono.SelectedValue + "'", con);
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
            else
            {
                Trtaxnums.Visible = true;
                if (ViewState["CC_subtype"].ToString() != "Service")
                {                  
                    txtex.Enabled = true;
                    txtAdvStax.Enabled = false;
                    txtnetex.Enabled = true;
                    txtAdvStax.Text = "0";
                    txtnetadvsevtax.Text = "0";
                    txtnetadvsevtax.Enabled = false;
                    lblservtaxnum.Visible = false;
                    ddlservicetax.Visible = false;
                    lblExctaxnum.Visible = true;
                    ddlExcno.Visible = true;
                    fillexcise();
                    fillvat();
                    
                }
                else
                {
                    txtnetadvsevtax.Enabled = true;
                    txtAdvStax.Enabled = true;
                    txtex.Enabled = false;
                    txtex.Text = "0";
                    txtnetex.Text = "0";
                    txtnetex.Enabled = false;                   
                    lblservtaxnum.Visible = true;
                    ddlservicetax.Visible = true;
                    lblExctaxnum.Visible = false;
                    ddlExcno.Visible = false;
                    fillservice();
                    fillvat();
                }
            }

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void ValuesClear()
    {
        txtadvance.Text = "";
        txtamt.Text = "";
        txtbasic.Text = "";
        txtdate.Text = "";
        txtdesc.Text = "";
        txted.Text = "";
        txtedc.Text = "";
        txtex.Text = "";
        txtfdr.Text = "";
        txtfre.Text = "";
        txthed.Text = "";
        txthold.Text = "";
        txtindt.Text = "";
        txtins.Text = "";
        txtintrest.Text = "";
        txtinvoice.Text = "";
        txtname.Text = "";
        txtNetED.Text = "";
        txtnetex.Text = "";
        txtnetfre.Text = "";
        txtNetHED.Text = "";
        txtnetins.Text = "";
        txtnettax.Text = "";
        txtother.Text = "";
        txtprinciple.Text = "";
        txtra.Text = "";
        txtretention.Text = "";
        txttax.Text = "";
        txttds.Text = "";
        txttotal.Text = "";

    }
    public void ClearTextBoxes(Control control)
    {
     
        foreach (Control ctrlMain in control.Controls)
        {
            if (ctrlMain.HasControls()) ClearTextBoxes(ctrlMain);
            if (ctrlMain.ToString().Equals("System.Web.UI.WebControls.TextBox"))
            {
                TextBox textBox = (TextBox)ctrlMain;
                textBox.Text = "";
            }
        }
       
    }
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        subclientname();
        fillCC();
    }
    public void fillexcise()
    {
        da = new SqlDataAdapter("select Excise_no from ExciseMaster where Status='3'", con);
        da.Fill(ds, "Excise/VAT");

        if (ds.Tables["Excise/VAT"].Rows.Count > 0)
        {
            ddlExcno.DataValueField = "Excise_no";
            ddlExcno.DataTextField = "Excise_no";
            ddlExcno.DataSource = ds.Tables["Excise/VAT"];
            ddlExcno.DataBind();
            ddlExcno.Items.Insert(0, "Select");
        }
    }
    public void fillservice()
    {
        da = new SqlDataAdapter("select ServiceTaxno from [ServiceTaxMaster] where Status='3'", con);
        da.Fill(ds, "ServiceTax");

        if (ds.Tables["ServiceTax"].Rows.Count > 0)
        {
            ddlservicetax.DataValueField = "ServiceTaxno";
            ddlservicetax.DataTextField = "ServiceTaxno";
            ddlservicetax.DataSource = ds.Tables["ServiceTax"];
            ddlservicetax.DataBind();
            ddlservicetax.Items.Insert(0, "Select");
        } 
    }
    public void fillvat()
    {
        da = new SqlDataAdapter("select RegNo from [Saletax/VatMaster] where Status='3'", con);
        da.Fill(ds, "VatTax");

        if (ds.Tables["VatTax"].Rows.Count > 0)
        {
            ddlvatno.DataValueField = "RegNo";
            ddlvatno.DataTextField = "RegNo";
            ddlvatno.DataSource = ds.Tables["VatTax"];
            ddlvatno.DataBind();
            ddlvatno.Items.Insert(0, "Select");
        }
    }
    public static void DisableFormControls(ControlCollection ChildCtrls)
    {
        foreach (Control Ctrl in ChildCtrls)
        {
            if (Ctrl is TextBox)
                ((TextBox)Ctrl).Text = "";
            else if (Ctrl is DropDownList)
                ((DropDownList)Ctrl).Items.Clear();
            else
                DisableFormControls(Ctrl.Controls);
        }
    }
}
