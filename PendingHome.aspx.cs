using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

public partial class PendingHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;

    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "StoreKeeper")
            {

            }
            else if (Session["roles"].ToString() == "Project Manager")
            {

            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {

            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {

            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {

            }
            else if (Session["roles"].ToString() == "HoAdmin")
            {
                //AccordSpInv4rAppr.Visible = true;
                FillSpInvoice();
                FillSpAmend();
                FillAmendSPPO();
                //fillGenInvoice();
                FillGenPay();
                FillCostCenter();
                FillCashTransfer();
                Pay4rApproval();
                VerifyAdvPayment();
                VerifyCrdtPay();
                //fillgrid();
                fillBankCheque();
                FillClosePO();
                FillSupplier();
                //FillSpInv4rApp();
                AccordIndentApproval.Visible=false;
                AccordApproveDirectStock.Visible=false;
                AccordSupplier.Visible = true;
                AccordSuppPO.Visible = false;
                Accordother.Visible = true;
                AccordItemcode.Visible = false;
                Accordassetsale.Visible = true;
                AccordTRApproval.Visible = true;
                Fillothers();
                Fillunsecured();
                FillShareCapital();
                FillFdopen();
                FillFdclaim();
                Fillclientretnhold();
                Fillassetsale();
                TR4rApproval();
                AccordionPanehsn.Visible = false;

            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                FillSpInvoice();
                FillSpAmend();
                FillAmendSPPO();
                //fillGenInvoice();
                //FillCostCenter();
                Pay4rApproval();
                FillGenPay();
                FillClosePO();
                FillCostCenter();
                //fillBankCheque();
                FillIndent();
                FillStockUpdate();
                FillSupplier();
                fillgrid();
                //fillPOgrid();
                //VerifyAdvPayment();
                //FillSpInv4rApp();
                AccordAdvPay.Visible=false;
                AccordCshTransfer.Visible=false;
                AccordcrdtPay.Visible = false;
                Accordother.Visible = false;
                AccordItemcode.Visible = true;
                Accordassetsale.Visible = true;
                AccordTRApproval.Visible = true;
                Fillunsecured();
                FillShareCapital();
                FillFdopen();
                FillFdclaim();
                fillitemcodes();
                Fillassetsale();
                TR4rApproval();
                AccordionPanehsn.Visible = true;
                hsnapproval();
            }
        }
    }

    protected void FillSupplier()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='7') and p.paymenttype='Supplier'  order by p.cc_code asc;Select replace(isnull(SUM(p.Balance),0),'.0000','.00')AS Total from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='7') and p.paymenttype='Supplier'", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select mrr_no,p.po_no,invoiceno,invoice_date,p.cc_code,description,m.status from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='7A') and p.paymenttype='Supplier'  order by p.cc_code asc;Select replace(isnull(SUM(p.Balance),0),'.0000','.00')AS Total from mr_report m  join pending_invoice p on m.po_no=p.po_no where (m.status='7A') and p.paymenttype='Supplier'", con);
            da.Fill(ds, "Supplier");
            if (ds.Tables["Supplier"].Rows.Count > 0)
            {
                lblSupplier.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblSupplier.Text = "(" + ds.Tables["Supplier"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["Supplier1"].Rows[0].ItemArray[0].ToString();
                else
                    lblSupplier.Text = "(" + ds.Tables["Supplier"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["Supplier"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["Supplier"].Rows[i]["mrr_no"].ToString() + "</td><td>" + ds.Tables["Supplier"].Rows[i]["po_no"].ToString() + "</td><td>" + ds.Tables["Supplier"].Rows[i]["invoiceno"].ToString() + "</td><td>" + ds.Tables["Supplier"].Rows[i]["invoice_date"].ToString() + "</td><td>" + ds.Tables["Supplier"].Rows[i]["cc_code"].ToString() + "</td></tr>");
                }
                tbodySupplier.InnerHtml = sb.ToString();
                hlnksupplier.Visible = true;
            }
            else
            {
                lblSupplier.ForeColor = System.Drawing.Color.GreenYellow;
                lblSupplier.Text = "(0   Pendings )";
                hlnksupplier.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    
    }
    protected void Fillothers()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select cc_code,Bank_Name,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,Credit,PaymentType from BankBook where status='1A' and PaymentType not in ('Hold','Retention') order by id desc", con);
            da.Fill(ds, "others");
            if (ds.Tables["others"].Rows.Count > 0)
            {
                lblothers.ForeColor = System.Drawing.Color.Red;
                lblothers.Text = "(" + ds.Tables["others"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["others"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["others"].Rows[i]["date"].ToString() + "</td><td>" + ds.Tables["others"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["others"].Rows[i]["Bank_Name"].ToString() + "</td><td>" + ds.Tables["others"].Rows[i]["PaymentType"].ToString() + "</td><td>" + ds.Tables["others"].Rows[i]["Description"].ToString() + "</td><td>" + ds.Tables["others"].Rows[i]["Credit"].ToString() + "</td></tr>");
                }
                tbodyothers.InnerHtml = sb.ToString();
                hlnkothers.Visible = true;
            }
            else
            {
                lblothers.ForeColor = System.Drawing.Color.GreenYellow;
                lblothers.Text = "(0   Pendings )";
                hlnkothers.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void FillSpInvoice()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount from pending_invoice where status='1' and paymenttype not in ('Supplier','Excise Duty','Service provider VAT','Service Tax','VAT' ) order by id desc", con);

            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount from pending_invoice where ((status='2' and paymenttype in ('Service Provider')))  order by id desc ; Select (ISNULL(replace(SUM(netamount),'.0000','.00'),0))AS Amount from pending_invoice where ((status='2' and paymenttype in ('Service Provider'))) ", con);
            da.Fill(ds, "pending_invoice");
            if (ds.Tables["pending_invoice"].Rows.Count > 0)
            {
                lblSpInv4rApproval.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblSpInv4rApproval.Text = "(" + ds.Tables["pending_invoice"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["pending_invoice1"].Rows[0].ItemArray[0].ToString();
                else
                    lblSpInv4rApproval.Text = "(" + ds.Tables["pending_invoice"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["pending_invoice"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["pending_invoice"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["Description"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["Date"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["dca_code"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["sub_dca"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["invoiceno"].ToString() + "</td><td>" + ds.Tables["pending_invoice"].Rows[i]["netamount"].ToString() + "</td></tr>");
                }
                TbodySpInv4rAppr.InnerHtml = sb.ToString();
                hlnkSpInv4rAppr.Visible = true;
            }
            else
            {
                lblSpInv4rApproval.ForeColor = System.Drawing.Color.GreenYellow;
                lblSpInv4rApproval.Text = "(0   Pendings )";
                hlnkSpInv4rAppr.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void FillSpAmend()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select pono,s.vendor_id,cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks,vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.status='1AN'", con);

            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select pono,s.vendor_id,cc_code,dca_code,subdca_code,CAST((isnull(po_value,0)) AS Decimal(20,2))as po_value,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,remarks,vendor_name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.status='2' ; select replace(isnull(SUM(po_value),0),'.0000','.00')AS Total from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.status='2'", con);
            da.Fill(ds, "SPPO");
            if (ds.Tables["SPPO"].Rows.Count > 0)
            {
                lblsppoapprov.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblsppoapprov.Text = "(" + ds.Tables["SPPO"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["SPPO1"].Rows[0].ItemArray[0].ToString();
                else
                    lblsppoapprov.Text = "(" + ds.Tables["SPPO"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["SPPO"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["SPPO"].Rows[i]["pono"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["vendor_id"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["dca_code"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["subdca_code"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["po_value"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["po_date"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["remarks"].ToString() + "</td><td>" + ds.Tables["SPPO"].Rows[i]["vendor_name"].ToString() + "</td></tr>");
                }
                SpSPPO.InnerHtml = sb.ToString();
                hlnkSpSPPO.Visible = true;
            }

            else
            {
                lblsppoapprov.ForeColor = System.Drawing.Color.GreenYellow;
                lblsppoapprov.Text = "(0  Pendings)";
                hlnkSpSPPO.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void FillAmendSPPO()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)+isnull(amended_amount,0)) AS Decimal(20,2))[PO Value],vendor_name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(balance,0) AS Decimal(20,2))as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1BN'", con);
            else
                if (Session["roles"].ToString() == "SuperAdmin")
                    da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)+isnull(amended_amount,0)) AS Decimal(20,2))[PO Value],vendor_name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(balance,0) AS Decimal(20,2))as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='2'; SELECT CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))AS Amount FROM Amend_sppo [as] JOIN SPPO s ON [as].pono=s.pono WHERE [as].status in ('2')", con);
            da.Fill(ds, "AmendSPPO");
            if (ds.Tables["AmendSPPO"].Rows.Count > 0)
            {
                lblamndsppo.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblamndsppo.Text = "(" + ds.Tables["AmendSPPO"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["AmendSPPO1"].Rows[0].ItemArray[0].ToString();
                else
                    lblamndsppo.Text = "(" + ds.Tables["AmendSPPO"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["AmendSPPO"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td colspan='2'>" + ds.Tables["AmendSPPO"].Rows[i]["pono"].ToString() + "</td><td>" + ds.Tables["AmendSPPO"].Rows[i]["po_value"].ToString() + "</td><td>" + ds.Tables["AmendSPPO"].Rows[i]["Amended_date"].ToString() + "</td><td>" + ds.Tables["AmendSPPO"].Rows[i]["remarks"].ToString() + "</td><td>" + ds.Tables["AmendSPPO"].Rows[i]["vendor_name"].ToString() + "</td></tr>");
                }
                TbodyAmendSppo.InnerHtml = sb.ToString();
                hlnkAmendSppo.Visible = true;

            }
            else
            {
                lblamndsppo.ForeColor = System.Drawing.Color.GreenYellow;
                lblamndsppo.Text = "(0   Pendings )";
                hlnkAmendSppo.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void FillClosePO()
    {

        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select distinct c.cc_code,c.cc_name from cost_center c join SPPO s on c.cc_code=s.cc_code where c.status in ('Old','New') and s.status='Closed1' and cc_type='Performing' union all Select distinct c.cc_code,c.cc_name from cost_center c join SPPO s on c.cc_code=s.cc_code where c.status in ('Old','New') and s.status='Closed' and cc_type='Non-Performing'", con);
            if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select distinct c.cc_code,c.cc_name from cost_center c join SPPO s on c.cc_code=s.cc_code where c.status in ('Old','New') and s.status='Closed2' ; SELECT (ISNULL(replace(SUM(s.Balance),'.0000','.00'),0))AS amount FROM SPPO s JOIN Cost_Center cc  ON s.cc_code=cc.cc_code WHERE s.status='Closed2'", con);
            da.Fill(ds, "ClosePO");
            if (ds.Tables["ClosePO"].Rows.Count > 0)
            {
                lblClosePO.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblClosePO.Text = "(" + ds.Tables["ClosePO"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["ClosePO1"].Rows[0].ItemArray[0].ToString();
                else
                    lblClosePO.Text = "(" + ds.Tables["ClosePO"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["ClosePO"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["ClosePO"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["ClosePO"].Rows[i]["cc_name"].ToString() + "</td></tr>");
                }
                TbodyClosePO.InnerHtml = sb.ToString();
                hlnkClosePO.Visible = true;

            }
            else
            {
                lblClosePO.ForeColor = System.Drawing.Color.GreenYellow;
                lblClosePO.Text = "(0   Pendings )";
                hlnkClosePO.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    //protected void fillGenInvoice()
    //{
    //    try
    //    {
    //        if (Session["roles"].ToString() == "HoAdmin")
    //            da = new SqlDataAdapter("Select invoiceno,cc_code,dca_code,subdca_code,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,replace(amount,'.0000','.00')as amount,Mode_of_Pay from GeneralInvoices where status='1' ", con);
    //        else if (Session["roles"].ToString() == "SuperAdmin")
    //            da = new SqlDataAdapter("Select invoiceno,cc_code,dca_code,subdca_code,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,replace(amount,'.0000','.00')as amount,Mode_of_Pay from GeneralInvoices where status='2' ", con);
    //        da.Fill(ds, "GeneralInvoices");
    //        if (ds.Tables["GeneralInvoices"].Rows.Count > 0)
    //        {
    //            lblGenInv.Text = "(" + ds.Tables["GeneralInvoices"].Rows.Count + "   Pendings )";
    //            StringBuilder sb = new StringBuilder();
    //            for (int i = 0; i < ds.Tables["GeneralInvoices"].Rows.Count; i++)
    //            {
    //                sb.Append("<tr><td><a href='verifygeneralpayment.aspx' id='bottle'><img src='images/iconset-b-edit.gif' style='cursor:hand'/></a></td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["Invoiceno"].ToString() + "</td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["Date"].ToString() + "</td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["dca_code"].ToString() + "</td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["subdca_code"].ToString() + "</td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["Name"].ToString() + "</td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["Amount"].ToString() + "</td><td>" + ds.Tables["GeneralInvoices"].Rows[i]["Mode_of_Pay"].ToString() + "</td></tr>");
    //            }
    //            TbodyGenInvoice.InnerHtml = sb.ToString();
    //        }
    //        else
    //            lblGenInv.Text = "(0   Pendings )";
    //    }
    //    catch (Exception ex)
    //    {
    //        Utilities.CatchException(ex);
    //        JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
    //    }
    //}
    protected void FillCostCenter()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select cc_code,cc_name,cc_inchargename,address from Cost_Center where status='1'", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select cc_code,cc_name,cc_inchargename,address from Cost_Center where status='2'", con);
            da.Fill(ds, "CostCenter");
            if (ds.Tables["CostCenter"].Rows.Count > 0)
            {
                lblccverify.ForeColor = System.Drawing.Color.Red;
                lblccverify.Text = "(" + ds.Tables["CostCenter"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["CostCenter"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["CostCenter"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["CostCenter"].Rows[i]["cc_name"].ToString() + "</td><td>" + ds.Tables["CostCenter"].Rows[i]["cc_inchargename"].ToString() + "</td><td>" + ds.Tables["CostCenter"].Rows[i]["address"].ToString() + "</td></tr>");
                }
                TbodyCostCenter.InnerHtml = sb.ToString();
                hlnkCostCenter.Visible = true;
            }
            else
            {
                lblccverify.ForeColor = System.Drawing.Color.GreenYellow;
                lblccverify.Text = "(0   Pendings )";
                hlnkCostCenter.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void FillCashTransfer()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select id,category,REPLACE(CONVERT(VARCHAR(11),convert(datetime,voucherdate,101),106), ' ', '-') as voucherdate,description,debit FROM cash_transfer where status in ('2') order by id desc ", con);
            da.Fill(ds, "cash_transfer");
            if (ds.Tables["cash_transfer"].Rows.Count > 0)
            {
                lblcshtransfer.ForeColor = System.Drawing.Color.Red;
                lblcshtransfer.Text = "(" + ds.Tables["cash_transfer"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["cash_transfer"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["cash_transfer"].Rows[i]["id"].ToString() + "</td><td>" + ds.Tables["cash_transfer"].Rows[i]["category"].ToString() + "</td><td>" + ds.Tables["cash_transfer"].Rows[i]["voucherdate"].ToString() + "</td><td>" + ds.Tables["cash_transfer"].Rows[i]["description"].ToString() + "</td><td>" + ds.Tables["cash_transfer"].Rows[i]["debit"].ToString() + "</td></tr>");
                }
                TbodyCshTransfer.InnerHtml = sb.ToString();
                hlnkCshTransfer.Visible = true;
            }
            else
            {
                lblcshtransfer.ForeColor = System.Drawing.Color.GreenYellow;
                lblcshtransfer.Text = "(0   Pendings )";
                hlnkCshTransfer.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void Pay4rApproval()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='1' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='1')i where i.id>0  order by i.date", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='2' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='2')i where i.id>0  order by i.date ; select replace(isnull(SUM(amount),0),'.0000','.00')AS Total from (select Transaction_No as id,Amount from BankTransactions where status='2' union all select Id,amount from TermLoan where Status='2' union all select id,Debit as Amount from BankBook where PaymentType='Supplier' and Loanno is not null and status='2') x ", con);
            }
            da.Fill(ds, "TermLoan");

            if (ds.Tables["TermLoan"].Rows.Count > 0)
            {
                lblPay4rApproval.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblPay4rApproval.Text = "(" + ds.Tables["TermLoan"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["TermLoan1"].Rows[0].ItemArray[0].ToString();
                else
                    lblPay4rApproval.Text = "(" + ds.Tables["TermLoan"].Rows.Count + "   Pendings )";

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["TermLoan"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["TermLoan"].Rows[i]["CC_Code"].ToString() + "</td><td  align='centre'>" + ds.Tables["TermLoan"].Rows[i]["Description"].ToString() + "</td><td  align='centre'>" + ds.Tables["TermLoan"].Rows[i]["Date"].ToString() + "</td><td  align='centre'>" + ds.Tables["TermLoan"].Rows[i]["Amount"].ToString() + "</td></tr>");
                }
                Po4rApproval.InnerHtml = sb.ToString();
                hlnkPo4rApproval.Visible = true;
            }
            else
            {
                lblPay4rApproval.ForeColor = System.Drawing.Color.GreenYellow;
                lblPay4rApproval.Text = "(0   Pendings )";
                hlnkPo4rApproval.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void VerifyAdvPayment()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select i.PO_NO,i.CC_Code,Credit as Amount,i.InvoiceNo as Transaction_No,b.Bank_Name from Invoice i join BankBook b on i.InvoiceNo=b.InvoiceNo where i.Status='1' and b.status='1'", con);
            da.Fill(ds, "BankBook");

            if (ds.Tables["BankBook"].Rows.Count > 0)
            {
                lbladvpay.ForeColor = System.Drawing.Color.Red;
                lbladvpay.Text = "(" + ds.Tables["BankBook"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["BankBook"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["BankBook"].Rows[i]["Po_No"].ToString() + "</td><td>" + ds.Tables["BankBook"].Rows[i]["CC_Code"].ToString() + "</td><td>" + ds.Tables["BankBook"].Rows[i]["Amount"].ToString() + "</td><td>" + ds.Tables["BankBook"].Rows[i]["Transaction_No"].ToString() + "</td><td>" + ds.Tables["BankBook"].Rows[i]["Bank_Name"].ToString() + "</td></tr>");
                }
                TbodyAdvPay.InnerHtml = sb.ToString();
                hlnkAdvPay.Visible = true;
            }
            else
            {
                lbladvpay.ForeColor = System.Drawing.Color.GreenYellow;
                lbladvpay.Text = "(0   Pendings )";
                hlnkAdvPay.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void VerifyCrdtPay()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select i.PO_NO,i.CC_Code,Amount,i.InvoiceNo,b.Bank_Name from Invoice i join BankBook b on i.InvoiceNo=b.InvoiceNo where i.Status='Credit Pending' and b.status='1'", con);
            da.Fill(ds, "CreditPay");

            if (ds.Tables["CreditPay"].Rows.Count > 0)
            {
                lblcrdtpay.ForeColor = System.Drawing.Color.Red;
                lblcrdtpay.Text = "(" + ds.Tables["CreditPay"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["CreditPay"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["CreditPay"].Rows[i]["Po_No"].ToString() + "</td><td>" + ds.Tables["CreditPay"].Rows[i]["CC_Code"].ToString() + "</td><td>" + ds.Tables["CreditPay"].Rows[i]["InvoiceNo"].ToString() + "</td><td>" + ds.Tables["CreditPay"].Rows[i]["Bank_Name"].ToString() + "</td></tr>");
                }
                TbodyCrdtPay.InnerHtml = sb.ToString();
                hlnkCrdtPay.Visible = true;
            }
            else
            {
                lblcrdtpay.ForeColor = System.Drawing.Color.GreenYellow;
                lblcrdtpay.Text = "(0   Pendings )";
                hlnkCrdtPay.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    //public void fillgrid()
    //{

    //    try
    //    {
    //        this.UpdatePanel1.Update();
    //        if (Session["roles"].ToString() == "HoAdmin")
    //            da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='1' union all select Id,'CCC' as cc_code,loanpurpose,REPLACE(CONVERT(VARCHAR(11),inststartdate, 106), ' ', '-')as inststartdate,amount,'L' as type from TermLoan where Status='1' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='1')i where i.id>0  " + ViewState["condition"].ToString() + " order by i.date", con);

    //        else if (Session["roles"].ToString() == "SuperAdmin")
    //            da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='2' union all select Id,'CCC' as cc_code,loanpurpose,REPLACE(CONVERT(VARCHAR(11),inststartdate, 106), ' ', '-')as inststartdate,amount,'L' as type from TermLoan where Status='2' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='2')i where i.id>0 " + ViewState["condition"].ToString() + " order by i.date", con);

    //        da.Fill(ds, "FillIn");
    //        GridView1.DataSource = ds.Tables["FillIn"];


    //        GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
    //        GridView1.DataBind();
    //    }

    //    catch (Exception ex)
    //    {
    //        Utilities.CatchException(ex);
    //        JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
    //    }



    //}
    public void fillgrid()
    {

        try
        {
             if (Session["roles"].ToString() == "SuperAdmin")
                //da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='2' union all select Id,'CCC' as cc_code,loanpurpose,REPLACE(CONVERT(VARCHAR(11),inststartdate, 106), ' ', '-')as inststartdate,amount,'L' as type from TermLoan where Status='2' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='2')i where i.id>0 order by i.date", con);
                 da = new SqlDataAdapter("SELECT id, po_no,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as [po_date],p.indent_no as [Indent No],ref_no,ref_date,cc_code,remarks,amount from purchase_details p  where  (status='1A') order by status asc; SELECT Replace(Sum(isnull(Amount,0)),'.0000','.00')AS Total from purchase_details p  where  (status='1A')", con);
            da.Fill(ds, "SuupplierPO");
            if (ds.Tables["SuupplierPO"].Rows.Count > 0)
            {
                lblpototal.ForeColor = System.Drawing.Color.Red;
                lblpototal.Text = "(" + ds.Tables["SuupplierPO"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["SuupplierPO1"].Rows[0].ItemArray[0].ToString();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["SuupplierPO"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["SuupplierPO"].Rows[i]["po_no"].ToString() + "</td><td>" + ds.Tables["SuupplierPO"].Rows[i]["po_date"].ToString() + "</td><td>" + ds.Tables["SuupplierPO"].Rows[i]["Indent No"].ToString() + "</td><td>" + ds.Tables["SuupplierPO"].Rows[i]["ref_no"].ToString() + "</td><td>" + ds.Tables["SuupplierPO"].Rows[i]["ref_date"].ToString() + "</td><td>" + ds.Tables["SuupplierPO"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["SuupplierPO"].Rows[i]["remarks"].ToString() + "</td><td>" + ds.Tables["SuupplierPO"].Rows[i]["amount"].ToString() + "</td></tr>");
                }
                Pobody.InnerHtml = sb.ToString();
                hlnkPobody.Visible = true;
            }
            else
            {
                lblpototal.ForeColor = System.Drawing.Color.GreenYellow;
                lblpototal.Text = "(0   Pendings )";
                hlnkPobody.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }


    }
    public void fillBankCheque()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select cm.chequeid,cm.bankname,REPLACE(CONVERT(VARCHAR(11),cm.issuedate, 106), ' ', '-')as issuedate,cm.description,(select Min(chequeno) from Cheque_Nos where chequeid=cm.chequeid)[From],(select MAX(chequeno) from Cheque_Nos where chequeid=cm.chequeid)[To] from cheque_Master cm where cm.status='1'", con);

            //else if (Session["roles"].ToString() == "SuperAdmin")
            //    da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='2' union all select Id,'CCC' as cc_code,loanpurpose,REPLACE(CONVERT(VARCHAR(11),inststartdate, 106), ' ', '-')as inststartdate,amount,'L' as type from TermLoan where Status='2' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='2')i where i.id>0 order by i.date", con);

            da.Fill(ds, "cheque_Master");
            if (ds.Tables["cheque_Master"].Rows.Count > 0)
            {
                lblbankcheque.ForeColor = System.Drawing.Color.GreenYellow;
                lblbankcheque.Text = "(" + ds.Tables["cheque_Master"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["cheque_Master"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["cheque_Master"].Rows[i]["bankname"].ToString() + "</td><td>" + ds.Tables["cheque_Master"].Rows[i]["issuedate"].ToString() + "</td><td>" + ds.Tables["cheque_Master"].Rows[i]["description"].ToString() + "</td><td>" + ds.Tables["cheque_Master"].Rows[i]["From"].ToString() + "</td><td>" + ds.Tables["cheque_Master"].Rows[i]["To"].ToString() + "</td></tr>");
                }
                TbodyBankCheque.InnerHtml = sb.ToString();
                hlnkBankCheque.Visible = true;
            }
            else
            {
                lblbankcheque.ForeColor = System.Drawing.Color.GreenYellow;
                lblbankcheque.Text = "(0   Pendings )";
                hlnkBankCheque.Visible = false;
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    public void FillGenPay()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select invoiceno,cc_code,dca_code,subdca_code,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,replace(amount,'.0000','.00')as amount,Mode_of_Pay from GeneralInvoices where status='1' ", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select invoiceno,cc_code,dca_code,subdca_code,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,replace(amount,'.0000','.00')as amount,Mode_of_Pay from GeneralInvoices where status='2' ; Select replace(SUM(amount),'.0000','.00')as amount from GeneralInvoices where status='2'  ", con);
            da.Fill(ds, "GenInvoice");
            if (ds.Tables["GenInvoice"].Rows.Count > 0)
            {
                lblGenInv.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                {
                    lblGenInv.Text = "(" + ds.Tables["GenInvoice"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["GenInvoice1"].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    lblGenInv.Text = "(" + ds.Tables["GenInvoice"].Rows.Count + "   Pendings )";
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["GenInvoice"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["GenInvoice"].Rows[i]["Invoiceno"].ToString() + "</td><td>" + ds.Tables["GenInvoice"].Rows[i]["cc_code"].ToString() + "</td><td>" + ds.Tables["GenInvoice"].Rows[i]["dca_code"].ToString() + "</td><td>" + ds.Tables["GenInvoice"].Rows[i]["subdca_code"].ToString() + "</td><td>" + ds.Tables["GenInvoice"].Rows[i]["name"].ToString() + "</td><td>" + ds.Tables["GenInvoice"].Rows[i]["date"].ToString() + "</td><td>" + ds.Tables["GenInvoice"].Rows[i]["amount"].ToString() + "</td><td>" + ds.Tables["GenInvoice"].Rows[i]["Mode_Of_Pay"].ToString() + "</td></tr>");
                }
                TbodyGenInvoice.InnerHtml = sb.ToString();
                hlnkGenInvoice.Visible = true;
            }
            else
            {
                lblGenInv.ForeColor = System.Drawing.Color.GreenYellow;
                lblGenInv.Text = "(0   Pendings )";
                hlnkGenInvoice.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void FillIndent()
    {
        try
        {
            if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("SELECT distinct i.id,i.Indent_no as [Indent No],i.cc_code as [CC Code],i.indentraise_date as [Indent Date],LEFT(i.cmcremarks, CHARINDEX('$', i.cmcremarks) - 1) as [Description],(select Replace(Sum(isnull(Amount,0)),'.0000','.00') from indent_list where indent_no=i.indent_no) as[Indent Cost],i.status  as [Status]from indents i where (i.status='6A')  order by i.status asc ; SELECT Replace(Sum(isnull(Amount,0)),'.0000','.00')AS Total FROM Indent_list il JOIN Indents i ON i.Indent_no=il.Indent_no WHERE i.Status='6A' ", con);
            da.Fill(ds, "Indent");
            if (ds.Tables["Indent"].Rows.Count > 0)
            {
                lblIndentApprov.ForeColor = System.Drawing.Color.Red;
                lblIndentApprov.Text = "(" + ds.Tables["Indent"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["Indent1"].Rows[0].ItemArray[0].ToString();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["Indent"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["Indent"].Rows[i]["Indent No"].ToString() + "</td><td>" + ds.Tables["indent"].Rows[i]["CC Code"].ToString() + "</td><td>" + ds.Tables["indent"].Rows[i]["Indent Date"].ToString() + "</td><td>" + ds.Tables["indent"].Rows[i]["Description"].ToString() + "</td><td>" + ds.Tables["indent"].Rows[i]["Indent Cost"].ToString() + "</td></tr>");
                }
                TbodyIndentApprov.InnerHtml = sb.ToString();
                hlnkIndentApprov.Visible = true;
            }
            else
            {
                lblIndentApprov.ForeColor = System.Drawing.Color.GreenYellow;
                lblIndentApprov.Text = "(0   Pendings )";
                hlnkIndentApprov.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    public void FillStockUpdate()
    {
        try
        {
            if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select ID,Request_no,CC_code,Req_date,Description,status from StockUpdation where status in ('2')", con);
            da.Fill(ds, "VStockUpdate");
            if (ds.Tables["VStockUpdate"].Rows.Count > 0)
            {
                lblVerifyStockUpd.ForeColor = System.Drawing.Color.Red;
                lblVerifyStockUpd.Text = "(" + ds.Tables["VStockUpdate"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["VStockUpdate"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["VStockUpdate"].Rows[i]["Request_no"].ToString() + "</td><td>" + ds.Tables["VStockUpdate"].Rows[i]["CC_Code"].ToString() + "</td><td>" + ds.Tables["VStockUpdate"].Rows[i]["Req_Date"].ToString() + "</td><td>" + ds.Tables["VStockUpdate"].Rows[i]["Description"].ToString() + "</td></tr>");
                }
                TbodyVStockUpdate.InnerHtml = sb.ToString();
                hlnkVStockUpdate.Visible = true;
            }
            else
            {
                lblVerifyStockUpd.ForeColor = System.Drawing.Color.GreenYellow;
                lblVerifyStockUpd.Text = "(0   Pendings )";
                hlnkVStockUpdate.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void Fillunsecured()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.Loan_no,ul.Name,bb.Bank_Name,bb.Description,replace(ul.Amount,'.0000','.00') as Amount from AddUnsecuredLoan ul join BankBook bb on ul.Transaction_no=bb.Transaction_No and ul.Status=bb.status where ul.Status in ('A1','A1R')", con);
            if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.Loan_no,ul.Name,bb.Bank_Name,bb.Description,replace(ul.Amount,'.0000','.00') as Amount from AddUnsecuredLoan ul join BankBook bb on ul.Transaction_no=bb.Transaction_No and ul.Status=bb.status where ul.Status in ('A2','A2R');select replace(isnull(SUM(Amount),0),'.0000','.00')AS Total from AddUnsecuredLoan where Status in ('A2','A2R')", con);
            da.Fill(ds, "unsecured");
            if (ds.Tables["unsecured"].Rows.Count > 0)
            {
                lblunsecured.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                {
                    lblunsecured.Text = "(" + ds.Tables["unsecured"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["unsecured1"].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    lblunsecured.Text = "(" + ds.Tables["unsecured"].Rows.Count + "   Pendings )";
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["unsecured"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["unsecured"].Rows[i]["date"].ToString() + "</td><td>" + ds.Tables["unsecured"].Rows[i]["Loan_no"].ToString() + "</td><td>" + ds.Tables["unsecured"].Rows[i]["Name"].ToString() + "</td><td>" + ds.Tables["unsecured"].Rows[i]["Bank_Name"].ToString() + "</td><td>" + ds.Tables["unsecured"].Rows[i]["Description"].ToString() + "</td><td>" + ds.Tables["unsecured"].Rows[i]["Amount"].ToString() + "</td></tr>");
                }
                tbodyunsecured.InnerHtml = sb.ToString();
                hlnkunsecured.Visible = true;
            }
            else
            {
                lblunsecured.ForeColor = System.Drawing.Color.GreenYellow;
                lblunsecured.Text = "(0   Pendings )";
                hlnkunsecured.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void FillShareCapital()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.Name,bb.Bank_Name,bb.Description,replace(ul.Amount,'.0000','.00') as Amount from AddShareCapital ul join BankBook bb on ul.Transaction_no=bb.Transaction_No and ul.Status=bb.status where ul.Status='1'", con);
            if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.Name,bb.Bank_Name,bb.Description,replace(ul.Amount,'.0000','.00') as Amount from AddShareCapital ul join BankBook bb on ul.Transaction_no=bb.Transaction_No and ul.Status=bb.status where ul.Status='2';select replace(isnull(SUM(Amount),0),'.0000','.00')AS Total from AddShareCapital where Status='2'", con);
            da.Fill(ds, "sharecapital");
            if (ds.Tables["sharecapital"].Rows.Count > 0)
            {
                lblsharecapital.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                {
                    lblsharecapital.Text = "(" + ds.Tables["sharecapital"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["sharecapital1"].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    lblsharecapital.Text = "(" + ds.Tables["sharecapital"].Rows.Count + "   Pendings )";
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["sharecapital"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["sharecapital"].Rows[i]["date"].ToString() + "</td><td>" + ds.Tables["sharecapital"].Rows[i]["Name"].ToString() + "</td><td>" + ds.Tables["sharecapital"].Rows[i]["Bank_Name"].ToString() + "</td><td>" + ds.Tables["sharecapital"].Rows[i]["Description"].ToString() + "</td><td>" + ds.Tables["sharecapital"].Rows[i]["Amount"].ToString() + "</td></tr>");
                }
                tbodysharecapital.InnerHtml = sb.ToString();
                hlnksharecapital.Visible = true;
            }
            else
            {
                lblsharecapital.ForeColor = System.Drawing.Color.GreenYellow;
                lblsharecapital.Text = "(0   Pendings )";
                hlnksharecapital.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void FillFdopen()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.FDR,bb.Bank_Name,bb.Description,replace(ul.Amount,'.0000','.00') as Amount from AddFD ul join BankBook bb on ul.FDR=bb.FDR and ul.Approval_Status=bb.status where ul.Approval_Status='1' union all select REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.FDR,bb.Bank_Name,bb.Description,replace(bb.Credit,'.0000','.00') as Amount from FD_Intrest ul join BankBook bb on ul.FDR=bb.FDR and ul.status=bb.status where ul.status='I1'", con);
            if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.FDR,bb.Bank_Name,bb.Description,replace(ul.Amount,'.0000','.00') as Amount from AddFD ul join BankBook bb on ul.FDR=bb.FDR and ul.Approval_Status=bb.status where ul.Approval_Status='2' union all select REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.FDR,bb.Bank_Name,bb.Description,replace(bb.Credit,'.0000','.00') as Amount from FD_Intrest ul join BankBook bb on ul.FDR=bb.FDR and ul.status=bb.status where ul.status='I2' ;select replace(isnull(SUM(Amount),0),'.0000','.00')AS Total from AddFD where Approval_Status='2';select replace(isnull(SUM(intrest_Amount-Ded_Amount),0),'.0000','.00')AS Total from FD_Intrest where status='I2'", con);
            da.Fill(ds, "fdopen");
            if (ds.Tables["fdopen"].Rows.Count > 0)
            {
                lblfixedopen.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                {
                    decimal amt = Convert.ToDecimal(ds.Tables["fdopen1"].Rows[0].ItemArray[0].ToString()) + Convert.ToDecimal(ds.Tables["fdopen2"].Rows[0].ItemArray[0].ToString());
                    lblfixedopen.Text = "(" + ds.Tables["fdopen"].Rows.Count + "   Pendings ) Total Amount:-  " + amt;
                }
                else
                {
                    lblfixedopen.Text = "(" + ds.Tables["fdopen"].Rows.Count + "   Pendings )";
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["fdopen"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["fdopen"].Rows[i]["date"].ToString() + "</td><td>" + ds.Tables["fdopen"].Rows[i]["FDR"].ToString() + "</td><td>" + ds.Tables["fdopen"].Rows[i]["Bank_Name"].ToString() + "</td><td>" + ds.Tables["fdopen"].Rows[i]["Description"].ToString() + "</td><td>" + ds.Tables["fdopen"].Rows[i]["Amount"].ToString() + "</td></tr>");
                }
                tbodyfixedopen.InnerHtml = sb.ToString();
                if (Session["roles"].ToString() == "HoAdmin")
                    da = new SqlDataAdapter("select count(*) from AddFD ul join BankBook bb on ul.FDR=bb.FDR and ul.Approval_Status=bb.status where ul.Approval_Status='1'", con);
                if (Session["roles"].ToString() == "SuperAdmin")
                    da = new SqlDataAdapter("select count(*) from AddFD ul join BankBook bb on ul.FDR=bb.FDR and ul.Approval_Status=bb.status where ul.Approval_Status='2'", con);

                da.Fill(ds, "clfd");
                if (Convert.ToInt32(ds.Tables["clfd"].Rows[0].ItemArray[0].ToString()) > 0)
                {
                    hlnkfdopen.Visible = true;
                }
                else
                {
                    hlnkfdopen.Visible = false;
                }
                if (Session["roles"].ToString() == "HoAdmin")
                    da = new SqlDataAdapter("select count(*) from FD_Intrest ul join BankBook bb on ul.FDR=bb.FDR and ul.status=bb.status where ul.status='I1'", con);
                if (Session["roles"].ToString() == "SuperAdmin")
                    da = new SqlDataAdapter("select count(*) from FD_Intrest ul join BankBook bb on ul.FDR=bb.FDR and ul.status=bb.status where ul.status='I2'", con);
                da.Fill(ds, "clint");
                if (Convert.ToInt32(ds.Tables["clint"].Rows[0].ItemArray[0].ToString()) > 0)
                {
                    hlnkfdinterest.Visible = true;
                }
                else
                {
                    hlnkfdinterest.Visible = false;
                }
                //hlnkfdopen.Visible = true;
            }
            else
            {
                lblfixedopen.ForeColor = System.Drawing.Color.GreenYellow;
                lblfixedopen.Text = "(0   Pendings )";
                hlnkfdopen.Visible = false;
                hlnkfdinterest.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void FillFdclaim()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select distinct REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.FDR,bb.Bank_Name,bb.Description,replace(bb.Credit,'.0000','.00') as Amount from FD_Claim ul join BankBook bb on ul.Transaction_No=bb.Transaction_No and ul.Status=bb.status  where ul.Status='1' group by ul.Date,ul.FDR,bb.Bank_Name,bb.Description,ul.Amount,Credit", con);
            if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select distinct REPLACE(CONVERT(VARCHAR(11),ul.Date, 106), ' ', '-')as date,ul.FDR,bb.Bank_Name,bb.Description,replace(bb.Credit,'.0000','.00') as Amount from FD_Claim ul join BankBook bb on ul.Transaction_No=bb.Transaction_No and ul.Status=bb.status  where ul.Status='2' group by ul.Date,ul.FDR,bb.Bank_Name,bb.Description,ul.Amount,Credit;select replace(isnull(SUM(credit),0),'.0000','.00')AS Total from BankBook bb where bb.Status='2' and paymenttype='FD'", con);
            da.Fill(ds, "fdclaim");
            if (ds.Tables["fdclaim"].Rows.Count > 0)
            {
                lblfdclosed.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                {
                    lblfdclosed.Text = "(" + ds.Tables["fdclaim"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["fdclaim1"].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    lblfdclosed.Text = "(" + ds.Tables["fdclaim"].Rows.Count + "   Pendings )";
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["fdclaim"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["fdclaim"].Rows[i]["date"].ToString() + "</td><td>" + ds.Tables["fdclaim"].Rows[i]["FDR"].ToString() + "</td><td>" + ds.Tables["fdclaim"].Rows[i]["Bank_Name"].ToString() + "</td><td>" + ds.Tables["fdclaim"].Rows[i]["Description"].ToString() + "</td><td>" + ds.Tables["fdclaim"].Rows[i]["Amount"].ToString() + "</td></tr>");
                }
                tbodyfdclaim.InnerHtml = sb.ToString();
                hlnkfdclaim.Visible = true;
            }
            else
            {
                lblfdclosed.ForeColor = System.Drawing.Color.GreenYellow;
                lblfdclosed.Text = "(0   Pendings )";
                hlnkfdclaim.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    protected void Fillclientretnhold()
    {
        try
        {
            da = new SqlDataAdapter("select bb.PaymentType,REPLACE(CONVERT(VARCHAR(11),bb.date, 106), ' ', '-')as Date,(select CAST(SUM(credit) AS Decimal(20,2)) from bankbook where Transaction_No=bb.Transaction_No )as Amount,bb.Bank_Name,bb.Description from BankBook bb join Invoice iv on bb.Transaction_No=iv.Ret_TranNo and bb.status=iv.Ret_Status  where bb.status='1A' group by bb.Transaction_No,bb.Date,bb.Bank_Name,bb.Description,bb.PaymentType union all select bb.PaymentType,REPLACE(CONVERT(VARCHAR(11),bb.date, 106), ' ', '-')as Date,(select CAST(SUM(credit) AS Decimal(20,2)) from bankbook where Transaction_No=bb.Transaction_No )as Amount,bb.Bank_Name,bb.Description from BankBook bb join Invoice iv on bb.Transaction_No=iv.Hold_TranNo and bb.status=iv.Hold_Status  where bb.status='1A' group by bb.Transaction_No,bb.Date,bb.Bank_Name,bb.Description,bb.PaymentType", con);
            da.Fill(ds, "clrethold");
            if (ds.Tables["clrethold"].Rows.Count > 0)
            {
                lblretholdcount.Text = "(" + ds.Tables["clrethold"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["clrethold"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["clrethold"].Rows[i]["PaymentType"].ToString() + "</td><td>" + ds.Tables["clrethold"].Rows[i]["Date"].ToString() + "</td><td>" + ds.Tables["clrethold"].Rows[i]["Amount"].ToString() + "</td><td>" + ds.Tables["clrethold"].Rows[i]["Bank_Name"].ToString() + "</td><td>" + ds.Tables["clrethold"].Rows[i]["Description"].ToString() + "</td></tr>");
                }
                tbodyretenhold.InnerHtml = sb.ToString();
                da = new SqlDataAdapter("SELECT COUNT(*)as count FROM (SELECT DISTINCT Transaction_No FROM  BankBook WHERE PaymentType='Retention' and status='1A' ) as dt", con);
                da.Fill(ds, "clret");
                if (Convert.ToInt32(ds.Tables["clret"].Rows[0].ItemArray[0].ToString()) > 0)
                {
                    hlnkclretention.Visible = true;
                }
                else
                {
                    hlnkclretention.Visible = false;
                }
                da = new SqlDataAdapter("SELECT COUNT(*)as count FROM (SELECT DISTINCT Transaction_No FROM  BankBook WHERE PaymentType='Hold' and status='1A' ) as dt", con);
                da.Fill(ds, "clhold");
                if (Convert.ToInt32(ds.Tables["clhold"].Rows[0].ItemArray[0].ToString()) > 0)
                {
                    hlnkclhold.Visible = true;
                }
                else
                {
                    hlnkclhold.Visible = false;
                }
            }
            else
            {
                lblretholdcount.ForeColor = System.Drawing.Color.GreenYellow;
                lblretholdcount.Text = "(0   Pendings )";
                hlnkclretention.Visible = false;
                hlnkclhold.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void fillitemcodes()
    {
        try
        {
            if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select id,item_code,item_name,basic_price,specification,Modified_date as date from item_codes where status in ('3','1A') ; select count(*) from item_codes where status in ('3','1A')", con);
            da.Fill(ds, "itemcodes");
            if (ds.Tables["itemcodes"].Rows.Count > 0)
            {
                lblitemcodecount.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblitemcodecount.Text = "(" + ds.Tables["itemcodes"].Rows.Count + "   Pendings ) Total Pending ItemCodes:-  " + ds.Tables["itemcodes1"].Rows[0].ItemArray[0].ToString();
               
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["itemcodes"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["itemcodes"].Rows[i]["item_code"].ToString() + "</td><td>" + ds.Tables["itemcodes"].Rows[i]["item_name"].ToString() + "</td><td>" + ds.Tables["itemcodes"].Rows[i]["basic_price"].ToString() + "</td><td>" + ds.Tables["itemcodes"].Rows[i]["specification"].ToString() + "</td><td>" + ds.Tables["itemcodes"].Rows[i]["date"].ToString() + "</td></tr>");
                }
                itemcodebody.InnerHtml = sb.ToString();
                hlnkitemcodes.Visible = true;
            }
            else
            {
                lblitemcodecount.ForeColor = System.Drawing.Color.GreenYellow;
                lblitemcodecount.Text = "(0   Pendings )";
                hlnkitemcodes.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void Fillassetsale()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                //da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount from pending_invoice where status='1' and paymenttype not in ('Supplier','Excise Duty','Service provider VAT','Service Tax','VAT' ) order by id desc", con);
                da = new SqlDataAdapter("select Request_No,Item_code,Buyer_Name,REPLACE(CONVERT(VARCHAR(11),BookValue_Date, 106), ' ', '-')as BookValue_Date,cast(selling_amt as decimal(20,2))as Selling_Amt from asset_sale where status='3' order by id desc", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select Request_No,Item_code,Buyer_Name,REPLACE(CONVERT(VARCHAR(11),BookValue_Date, 106), ' ', '-')as BookValue_Date,cast(selling_amt as decimal(20,2))as Selling_Amt from asset_sale where status='4' order by id desc ; Select (ISNULL(replace(SUM(Selling_Amt),'.0000','.00'),0))AS Amount from asset_sale where status='4' ", con);
            da.Fill(ds, "assetsale");
            if (ds.Tables["assetsale"].Rows.Count > 0)
            {
                lblassetsalecount.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblassetsalecount.Text = "(" + ds.Tables["assetsale"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["assetsale1"].Rows[0].ItemArray[0].ToString();
                else
                    lblassetsalecount.Text = "(" + ds.Tables["assetsale"].Rows.Count + "   Pendings )";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["assetsale"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center'><td>" + ds.Tables["assetsale"].Rows[i]["Request_No"].ToString() + "</td><td>" + ds.Tables["assetsale"].Rows[i]["Item_code"].ToString() + "</td><td>" + ds.Tables["assetsale"].Rows[i]["Buyer_Name"].ToString() + "</td><td>" + ds.Tables["assetsale"].Rows[i]["BookValue_Date"].ToString() + "</td><td>" + ds.Tables["assetsale"].Rows[i]["Selling_Amt"].ToString() + "</td></tr>");
                }
                assetsalebody.InnerHtml = sb.ToString();
                hlnkassetsale.Visible = true;
            }
            else
            {
                lblassetsalecount.ForeColor = System.Drawing.Color.GreenYellow;
                lblassetsalecount.Text = "(0   Pendings )";
                hlnkassetsale.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void TR4rApproval()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select i.* from (select Id,'CCC' as cc_code,loanpurpose,REPLACE(CONVERT(VARCHAR(11),inststartdate, 106), ' ', '-')as inststartdate,balance as amount,'L' as type from TermLoan where Status='1' )i where i.id>0  order by i.inststartdate", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                da = new SqlDataAdapter("Select i.* from (select Id,'CCC' as cc_code,loanpurpose,REPLACE(CONVERT(VARCHAR(11),inststartdate, 106), ' ', '-')as inststartdate,balance as amount,'L' as type from TermLoan where Status='2' )i where i.id>0  order by i.inststartdate ; Select (ISNULL(replace(SUM(Amount),'.0000','.00'),0))AS Amount from TermLoan where Status='2'", con);
            }
            da.Fill(ds, "TL");

            if (ds.Tables["TL"].Rows.Count > 0)
            {
                lblTRApproval.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblTRApproval.Text = "(" + ds.Tables["TL"].Rows.Count + "   Pendings ) Total Amount:-  " + ds.Tables["TL1"].Rows[0].ItemArray[0].ToString();
                else
                    lblTRApproval.Text = "(" + ds.Tables["TL"].Rows.Count + "   Pendings )";

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["TL"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["TL"].Rows[i]["CC_Code"].ToString() + "</td><td  align='centre'>" + ds.Tables["TL"].Rows[i]["loanpurpose"].ToString() + "</td><td  align='centre'>" + ds.Tables["TL"].Rows[i]["inststartdate"].ToString() + "</td><td  align='centre'>" + ds.Tables["TL"].Rows[i]["Amount"].ToString() + "</td></tr>");
                }
                TR4rApprovals.InnerHtml = sb.ToString();
                hlnkTR4rApproval.Visible = true;
            }
            else
            {
                lblTRApproval.ForeColor = System.Drawing.Color.GreenYellow;
                lblTRApproval.Text = "(0   Pendings )";
                hlnkTR4rApproval.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void hsnapproval()
    {
        try
        {
           if (Session["roles"].ToString() == "SuperAdmin")
            {
                da = new SqlDataAdapter("Select i.* from (select Id,CodeCategory,Remarks,HSN_SAC_Code,Remarks as code from HSN_SAC_Codes where Status='3')i where i.id>0  order by i.Id desc ", con);
            }
            da.Fill(ds, "HSN");

            if (ds.Tables["HSN"].Rows.Count > 0)
            {
                lblhsncount.ForeColor = System.Drawing.Color.Red;
                if (Session["roles"].ToString() == "SuperAdmin")
                    lblhsncount.Text = "(" + ds.Tables["HSN"].Rows.Count + "   Pendings )";
               
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ds.Tables["HSN"].Rows.Count; i++)
                {
                    sb.Append("<tr align='center' ><td>" + ds.Tables["HSN"].Rows[i]["CodeCategory"].ToString() + "</td><td  align='centre'>" + ds.Tables["HSN"].Rows[i]["HSN_SAC_Code"].ToString() + "</td><td  align='centre'>" + ds.Tables["HSN"].Rows[i]["code"].ToString() + "</td></tr>");
                }
                hsnbody.InnerHtml = sb.ToString();
                hlnkhsnapproval.Visible = true;
            }
            else
            {
                lblTRApproval.ForeColor = System.Drawing.Color.GreenYellow;
                lblhsncount.Text = "(0   Pendings )";
                hlnkhsnapproval.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    
}