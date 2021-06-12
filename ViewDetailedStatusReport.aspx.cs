using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ViewDetailedStatusReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            fillgrid(); 
        }
    }

    public void fillgrid()
    {
        //............DCA BUDGET AMEND ------------
        if (Request.QueryString["Type"] == "SrAccountant" && Request.QueryString["Name"] == "DCA Budget Amend")
        {
            da = new SqlDataAdapter("select cc.cc_code,'DCA Budget Amend' as typeoftransaction,'' as RefNumber,ada.Dca_Code,d.dca_name as DCAName,coalesce(Credit,debit) as Amount from Amendbudget_DCA ada join Cost_Center cc on ada.CC_Code=cc.cc_code Join dca d on ada.Dca_Code=d.dca_code where cc.cc_type='Performing' and ada.Status='1'", con);
        }
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "DCA Budget Amend")
        {
            da = new SqlDataAdapter("select cc.cc_code,'DCA Budget Amend' as typeoftransaction,'' as RefNumber,ada.Dca_Code,d.dca_name as DCAName,cc.cc_name as Name,coalesce(Credit,debit) as Amount from Amendbudget_DCA ada join Cost_Center cc on ada.CC_Code=cc.cc_code Join dca d on ada.Dca_Code=d.dca_code where cc.cc_type='Performing' and ada.Status='2'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "DCA Budget Amend")
        {
            da = new SqlDataAdapter("select cc.cc_code,'DCA Budget Amend' as typeoftransaction,'' as RefNumber,ada.Dca_Code,d.dca_name as DCAName,cc.cc_name as Name,coalesce(Credit,debit) as Amount from Amendbudget_DCA ada join Cost_Center cc on ada.CC_Code=cc.cc_code Join dca d on ada.Dca_Code=d.dca_code where cc.cc_type='Performing' and ada.Status='2A' or cc.cc_type in ('Non-Performing','Capital') and ada.Status='2'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "DCA Budget Amend")
        {
            da = new SqlDataAdapter("select cc.cc_code,'DCA Budget Amend' as typeoftransaction,'' as RefNumber,ada.Dca_Code,d.dca_name as DCAName,cc.cc_name as Name,coalesce(Credit,debit) as Amount from Amendbudget_DCA ada join Cost_Center cc on ada.CC_Code=cc.cc_code Join dca d on ada.Dca_Code=d.dca_code where ada.Status='3'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "NPCC DCA Budget Amend")
        {
            da = new SqlDataAdapter("select cc.cc_code,'DCA Budget Amend' as typeoftransaction,'' as RefNumber,ada.Dca_Code,d.dca_name as DCAName,cc.cc_name as Name,coalesce(Credit,debit) as Amount from Amendbudget_DCA ada join Cost_Center cc on ada.CC_Code=cc.cc_code Join dca d on ada.Dca_Code=d.dca_code where cc.cc_type='Performing' and ada.Status='2'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "NPCC DCA Budget Amend")
        {
            da = new SqlDataAdapter("select cc.cc_code,'DCA Budget Amend' as typeoftransaction,'' as RefNumber,ada.Dca_Code,d.dca_name as DCAName,cc.cc_name as Name,coalesce(Credit,debit) as Amount from Amendbudget_DCA ada join Cost_Center cc on ada.CC_Code=cc.cc_code Join dca d on ada.Dca_Code=d.dca_code where cc.cc_type='Performing' and ada.Status='3'", con);
        }
        //-----------------------------------------------------------------
        //------------DCA Budget Assignment ------------
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "DCA Budget Assign")
        {
            da = new SqlDataAdapter("select YDB.cc_code,'DCA Budget Assign' as typeoftransaction,'' as RefNumber,d.dca_code,d.dca_name as DCAName,cc.cc_name as Name, YDB.budget_dca_yearly as Amount, YDB.status from yearly_dcabudget YDB Join Cost_Center CC on YDB.cc_code = CC.cc_code Join dca d on YDB.dca_code=d.dca_code  where YDB.status='1'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "DCA Budget Assign")
        {
            da = new SqlDataAdapter("select YDB.cc_code,'DCA Budget Assign' as typeoftransaction,'' as RefNumber,d.dca_code,d.dca_name as DCAName,cc.cc_name as Name, YDB.budget_dca_yearly as Amount, YDB.status from yearly_dcabudget YDB Join Cost_Center CC on YDB.cc_code = CC.cc_code Join dca d on YDB.dca_code=d.dca_code  where YDB.status='1A'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "DCA Budget Assign")
        {
            da = new SqlDataAdapter("select YDB.cc_code,'DCA Budget Assign' as typeoftransaction,'' as RefNumber,d.dca_code,d.dca_name as DCAName,cc.cc_name as Name, YDB.budget_dca_yearly as Amount, YDB.status from yearly_dcabudget YDB Join Cost_Center CC on YDB.cc_code = CC.cc_code Join dca d on YDB.dca_code=d.dca_code  where YDB.status='2'", con);
        }
        //if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "DCA Budget Amend")
        //{
        //    da = new SqlDataAdapter("select cc.cc_code,'DCA Budget Amend' as typeoftransaction,'' as RefNumber,ada.Dca_Code,d.dca_name as Name,coalesce(Credit,debit) as Amount from Amendbudget_DCA ada join Cost_Center cc on ada.CC_Code=cc.cc_code Join dca d on ada.Dca_Code=d.dca_code where cc.cc_type='Performing' and ada.Status='3'", con);
        //}
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "SPPO")
        {
            da = new SqlDataAdapter("select cc_code as CC_Code,'SPPO' as [TypeofTransaction],pono as RefNumber,v.vendor_name as Name,Balance as Amount,s.status from SPPO S INNER JOIN vendor V on s.vendor_id=v.vendor_id where s.status='1'", con);
        }
        if (Request.QueryString["Type"] == "PUM" && Request.QueryString["Name"] == "SPPO")
        {
            da = new SqlDataAdapter("select cc_code as CC_Code,'SPPO' as [TypeofTransaction],pono as RefNumber,v.vendor_name as Name,Balance as Amount,s.status from SPPO S INNER JOIN vendor V on s.vendor_id=v.vendor_id where s.status in ('1P','1NP')", con);
        }
        if (Request.QueryString["Type"] == "CMC" && Request.QueryString["Name"] == "SPPO")
        {
            da = new SqlDataAdapter("select cc_code as CC_Code,'SPPO' as [TypeofTransaction],pono as RefNumber,v.vendor_name as Name,Balance as Amount,s.status from SPPO S INNER JOIN vendor V on s.vendor_id=v.vendor_id where s.status='1A'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "SPPO")
        {
            da = new SqlDataAdapter("select cc_code as CC_Code,'SPPO' as [TypeofTransaction],pono as RefNumber,v.vendor_name as Name,Balance as Amount,s.status from SPPO S INNER JOIN vendor V on s.vendor_id=v.vendor_id where s.status='1AN'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "SPPO")
        {
            da = new SqlDataAdapter(" select cc_code as CC_Code,'SPPO' as [TypeofTransaction],pono as RefNumber,v.vendor_name as Name,Balance as Amount,s.status from SPPO S INNER JOIN vendor V on s.vendor_id=v.vendor_id where s.status='2'", con);
        }
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "AmendSPPO")
        {
            da = new SqlDataAdapter("select distinct sp.cc_code as CC_Code,'SPPO Amended' as [TypeofTransaction],asp.pono as RefNumber,v.vendor_name as Name,asp.Amended_amount as Amount,asp.status from Amend_sppo asp join SPPO sp on asp.pono=sp.pono join vendor v on sp.vendor_id=v.vendor_id where asp.status='1'", con);
        }
        if (Request.QueryString["Type"] == "PUM" && Request.QueryString["Name"] == "AmendSPPO")
        {
            da = new SqlDataAdapter("select distinct sp.cc_code as CC_Code,'SPPO Amended' as [TypeofTransaction],asp.pono as RefNumber,v.vendor_name as Name,asp.Amended_amount as Amount,asp.status from Amend_sppo asp join SPPO sp on asp.pono=sp.pono join vendor v on sp.vendor_id=v.vendor_id where asp.status='1A'", con);
        }
        if (Request.QueryString["Type"] == "CMC" && Request.QueryString["Name"] == "AmendSPPO")
        {
            da = new SqlDataAdapter("select distinct sp.cc_code as CC_Code,'SPPO Amended' as [TypeofTransaction],asp.pono as RefNumber,v.vendor_name as Name,asp.Amended_amount as Amount,asp.status from Amend_sppo asp join SPPO sp on asp.pono=sp.pono join vendor v on sp.vendor_id=v.vendor_id where asp.status='1B'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "AmendSPPO")
        {
            da = new SqlDataAdapter("select distinct sp.cc_code as CC_Code,'SPPO Amended' as [TypeofTransaction],asp.pono as RefNumber,v.vendor_name as Name,asp.Amended_amount as Amount,asp.status from Amend_sppo asp join SPPO sp on asp.pono=sp.pono join vendor v on sp.vendor_id=v.vendor_id where asp.status='1BN'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "AmendSPPO")
        {
            da = new SqlDataAdapter("select distinct sp.cc_code as CC_Code,'SPPO Amended' as [TypeofTransaction],asp.pono as RefNumber,v.vendor_name as Name,asp.Amended_amount as Amount,asp.status from Amend_sppo asp join SPPO sp on asp.pono=sp.pono join vendor v on sp.vendor_id=v.vendor_id where asp.status='2'", con);
        }
        //........ Added Client PO At PM -----
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "ClientPO")
        {
            da = new SqlDataAdapter("select p.CC_code as CC_Code,'Client PO' as [TypeofTransaction],po_no as RefNumber,c.client_name as Name,po_basicvalue as Amount,p.status from PO P INNER JOIN  client c on p.client_id=c.client_id where P.status='1'", con);
        }
        //........ Added Client PO At HOAdmin -----
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "ClientPO")
        {
            da = new SqlDataAdapter("select p.CC_code as CC_Code,'Client PO' as [TypeofTransaction],po_no as RefNumber,c.client_name as Name,po_basicvalue as Amount,p.status from PO P INNER JOIN  client c on p.client_id=c.client_id where P.status='2'", con);
        }
        //.................... Amend Client PO @ PM----
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "AmendClientPO")
        {
            da = new SqlDataAdapter("select p.CC_code as CC_Code,'Amend Client PO' as [TypeofTransaction],p.po_no as RefNumber,c.client_name as Name,po_basicvalue as Amount,p.status from PO P INNER JOIN  client c on p.client_id=c.client_id where P.status='1'", con);
        }
        //.................... Amend Client PO @ HoAdmin ----
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "AmendClientPO")
        {
            da = new SqlDataAdapter("select p.CC_code as CC_Code,'Amend Client PO' as [TypeofTransaction],p.po_no as RefNumber,c.client_name as Name,po_basicvalue as Amount,p.status from PO P INNER JOIN  client c on p.client_id=c.client_id where P.status='2'", con);
        }
        //.................... GeneralInvoice @ HoAdmin ----
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "GeneralInvoice")
        {
            da = new SqlDataAdapter("select cc_code,'GeneralInvoice' as [Type of Transaction],Invoiceno as RefNumber,Name,Amount as Amount,Status from GeneralInvoices where Status='1'", con);
        }
        //.................... GeneralInvoice @ Super Admin ----
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "GeneralInvoice")
        {
            da = new SqlDataAdapter("select cc_code,'GeneralInvoice' as [TypeofTransaction],Invoiceno as RefNumber,Name,Amount as Amount,Status from GeneralInvoices where Status='2'", con);
        }
        //......
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "AmendClientPO")
        {
            da = new SqlDataAdapter("select p.CC_code as CC_Code,'Amend Client PO' as [TypeofTransaction],p.po_no as RefNumber,c.client_name as Name,po_basicvalue as Amount,p.status from PO P INNER JOIN  client c on p.client_id=c.client_id where P.status='1'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "AmendClientPO")
        {
            da = new SqlDataAdapter("select p.CC_code as CC_Code,'Amend Client PO' as [TypeofTransaction],p.po_no as RefNumber,c.client_name as Name,po_basicvalue as Amount,p.status from PO P INNER JOIN  client c on p.client_id=c.client_id where P.status='2'", con);
        }
        //.................... Supplier Invoice @ PM ----
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "Supplier Invoice")
        {
            da = new SqlDataAdapter("select PI.cc_code,'Supplier Invoice' as typeoftransaction,PI.InvoiceNo as RefNumber,V.vendor_name as Name,NetAmount as Amount  from pending_invoice PI JOIN vendor V on PI.vendor_id=V.vendor_id JOIN MR_Report MR on PI.PO_NO=MR.PO_no where PI.paymenttype='Supplier' and MR.Status='5' order by cc_code", con);
        }
        //Supplier Invoice @ CSK
        if (Request.QueryString["Type"] == "CSK" && Request.QueryString["Name"] == "Supplier Invoice")
        {
            da = new SqlDataAdapter("select PI.cc_code,'Supplier Invoice' as typeoftransaction,PI.InvoiceNo as RefNumber,V.vendor_name as Name,NetAmount as Amount  from pending_invoice PI JOIN vendor V on PI.vendor_id=V.vendor_id JOIN MR_Report MR on PI.PO_NO=MR.PO_no where PI.paymenttype='Supplier' and MR.Status='6' order by cc_code ", con);
        }
        if (Request.QueryString["Type"] == "PUM" && Request.QueryString["Name"] == "Supplier Invoice")
        {
            da = new SqlDataAdapter("select PI.cc_code,'Supplier Invoice' as typeoftransaction,PI.InvoiceNo as RefNumber,V.vendor_name as Name,NetAmount as Amount  from pending_invoice PI JOIN vendor V on PI.vendor_id=V.vendor_id JOIN MR_Report MR on PI.PO_NO=MR.PO_no where PI.paymenttype='Supplier' and MR.Status='6A' order by cc_code ", con);
        }

        //Supplier Invoice @ CMC
        if (Request.QueryString["Type"] == "CMC" && Request.QueryString["Name"] == "Supplier Invoice")
        {
            da = new SqlDataAdapter("select PI.cc_code,'Supplier Invoice' as typeoftransaction,PI.InvoiceNo as RefNumber,V.vendor_name as Name,NetAmount as Amount  from pending_invoice PI JOIN vendor V on PI.vendor_id=V.vendor_id JOIN MR_Report MR on PI.PO_NO=MR.PO_no where PI.paymenttype='Supplier' and MR.Status='6B' order by cc_code ", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "Supplier Invoice")
        {
            da = new SqlDataAdapter("select PI.cc_code,'Supplier Invoice' as typeoftransaction,PI.InvoiceNo as RefNumber,V.vendor_name as Name,NetAmount as Amount  from pending_invoice PI JOIN vendor V on PI.vendor_id=V.vendor_id JOIN MR_Report MR on PI.PO_NO=MR.PO_no where PI.paymenttype='Supplier' and MR.Status='7' order by cc_code ", con);
        }

        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "Supplier Invoice")
        {
            da = new SqlDataAdapter("select PI.cc_code,'Supplier Invoice' as typeoftransaction,PI.InvoiceNo as RefNumber,V.vendor_name as Name,NetAmount as Amount  from pending_invoice PI JOIN vendor V on PI.vendor_id=V.vendor_id JOIN MR_Report MR on PI.PO_NO=MR.PO_no where PI.paymenttype='Supplier' and MR.Status='7A' order by cc_code ", con);
        }
        //................ Close SPPO @PM............
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "SPPOClosed")
        {
            da = new SqlDataAdapter("select S.cc_code,'SPPOClosed' as typeoftransaction,pono as RefNumber,V.vendor_name as Name,S.Balance as Amount,s.status from SPPO S join Cost_Center cc on S.cc_code=cc.cc_code Join vendor V on S.vendor_id=V.vendor_id where S.status='Closed' and cc.cc_type='Performing'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "SPPOClosed")
        {
            da = new SqlDataAdapter("select S.cc_code,'SPPOClosed' as typeoftransaction,pono as RefNumber,V.vendor_name as Name,S.Balance as Amount,s.status from SPPO S join Cost_Center cc on S.cc_code=cc.cc_code Join vendor V on S.vendor_id=V.vendor_id where S.status='Closed1' and cc.cc_type='Performing'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "SPPOClosed")
        {
            da = new SqlDataAdapter("select S.cc_code,'SPPOClosed' as typeoftransaction,pono as RefNumber,V.vendor_name as Name,S.Balance as Amount,s.status from SPPO S join Cost_Center cc on S.cc_code=cc.cc_code Join vendor V on S.vendor_id=V.vendor_id where S.status='Closed2' and cc.cc_type='Performing'", con);
        }


        // ---------- CC Budget  Assignment ----------------
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "CC Budget Assign")
        {
            da = new SqlDataAdapter("select bc.cc_code,'CC Budget  Assign' as typeoftransaction,cc.ref_no as RefNumber,cc.cc_name as Name,bc.budget_amount as Amount,bc.status from budget_cc bc Join Cost_Center cc on bc.cc_code = cc.cc_code where bc.status='1'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "CC Budget Assign")
        {
            da = new SqlDataAdapter("select bc.cc_code,'CC Budget  Assign' as typeoftransaction,cc.ref_no as RefNumber,cc.cc_name as Name,bc.budget_amount as Amount,bc.status from budget_cc bc Join Cost_Center cc on bc.cc_code = cc.cc_code where bc.status='1A'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "CC Budget Assign")
        {
            da = new SqlDataAdapter("select bc.cc_code,'CC Budget  Assign' as typeoftransaction,cc.ref_no as RefNumber,cc.cc_name as Name,bc.budget_amount as Amount,bc.status from budget_cc bc Join Cost_Center cc on bc.cc_code = cc.cc_code where bc.status='2'", con);
        }
        if (Request.QueryString["Type"] == "CMD" && Request.QueryString["Name"] == "CC Budget Assign")
        {
            da = new SqlDataAdapter("select bc.cc_code,'CC Budget  Assign' as typeoftransaction,cc.ref_no as RefNumber,cc.cc_name as Name,bc.budget_amount as Amount,bc.status from budget_cc bc Join Cost_Center cc on bc.cc_code = cc.cc_code where bc.status='2A'", con);
        }
        // ---------- CC Budget  Amendment ----------------
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "CC Budget Amend")
        {
            da = new SqlDataAdapter("select abc.cc_code,'CC Budget Amend' as typeoftransaction,'' as RefNumber,cc.cc_name as Name,coalesce(abc.Credit,abc.debit) as Amount,abc.status  from AmendBudget_cc Abc Join Cost_Center CC on abc.cc_code=CC.cc_code where cc.cc_type='Performing' and abc.Status='1'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "CC Budget Amend")
        {
            da = new SqlDataAdapter("select abc.cc_code,'CC Budget Amend' as typeoftransaction,'' as RefNumber,cc.cc_name as Name,coalesce(abc.Credit,abc.debit) as Amount,abc.status from AmendBudget_cc abc Join Cost_Center cc on abc.cc_code = cc.cc_code where abc.status='1A' and cc.cc_type='Performing' or abc.status='1' and cc.cc_type in ('Non-Performing','Capital')", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "CC Budget Amend")
        {
            da = new SqlDataAdapter("select abc.cc_code,'CC Budget Amend' as typeoftransaction,'' as RefNumber,cc.cc_name as Name,coalesce(abc.Credit,abc.debit) as Amount,abc.status from AmendBudget_cc abc Join Cost_Center cc on abc.cc_code = cc.cc_code where abc.status='2'", con);
        }
        if (Request.QueryString["Type"] == "CMD" && Request.QueryString["Name"] == "CC Budget Amend")
        {
            da = new SqlDataAdapter("select abc.cc_code,'CC Budget Amend' as typeoftransaction,'' as RefNumber,cc.cc_name as Name,coalesce(abc.Credit,abc.debit) as Amount,abc.status from AmendBudget_cc abc Join Cost_Center cc on abc.cc_code = cc.cc_code where abc.status='2A'", con);
        }
        //-------------- SP Invoice corrected ------------
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "SP Invoice")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'SP Invoice' as typeoftransaction,InvoiceNo as RefNumber,name as Name,NetAmount as Amount from pending_invoice where paymenttype='Service Provider' and status='A0'", con);
        }
        if (Request.QueryString["Type"] == "PUM" && Request.QueryString["Name"] == "SP Invoice")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'SP Invoice' as typeoftransaction,InvoiceNo as RefNumber,name as Name,NetAmount as Amount from pending_invoice where paymenttype='Service Provider' and status='A1'", con);
        }
        if (Request.QueryString["Type"] == "CMC" && Request.QueryString["Name"] == "SP Invoice")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'SP Invoice' as typeoftransaction,InvoiceNo as RefNumber,name as Name,NetAmount as Amount from pending_invoice where paymenttype='Service Provider' and status='A2'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "SP Invoice")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'SP Invoice' as typeoftransaction,InvoiceNo as RefNumber,name as Name,NetAmount as Amount from pending_invoice where paymenttype='Service Provider' and status='1'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "SP Invoice")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'SP Invoice' as typeoftransaction,InvoiceNo as RefNumber,name as Name,NetAmount as Amount from pending_invoice where paymenttype='Service Provider' and status='2'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "Bank Payments")
        {

            da = new SqlDataAdapter("select coalesce(CC_Code,'')as CC_Code,'Bank Payments' as typeoftransaction,CAST(Transaction_No as varchar(max)) as RefNumber,Description as Name,Amount from BankTransactions where status='1' union all select 'CCC' as CC_Code,'Bank Payments' as typeoftransaction,CAST(Id as varchar(max)) as RefNumber,loanpurpose as Name,amount as Amount from TermLoan where Status='1' union all select CC_Code,'Bank Payments' as typeoftransaction,CAST(Id as varchar(max)) as RefNumber,Description as Name,Debit as Amount from BankBook where PaymentType='Supplier' and Loanno is not null and status='1' union all select '' as CC_Code,'Bank Payments' as typeoftransaction,CAST(Loan_no as varchar(max)) as RefNumber ,b.Description as Name,b.Debit as Amount from bankbook b join AddUnsecuredLoan al on al.transaction_no=b.transaction_no and al.type='Return' where b.status='A1R' union all select CC_Code,'Bank Payments' as typeoftransaction,CAST(af.FDR as varchar(max)) as RefNumber,bk.Description as Name,bk.Debit as Amount from AddFD af join bankbook bk on af.fdr=bk.fdr where af.Approval_status='1'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "Bank Payments")
        {
            da = new SqlDataAdapter("select CC_Code,'Bank Payments' as typeoftransaction,CAST(Transaction_No as varchar(max)) as RefNumber,Description as Name,Amount from BankTransactions where status='2' union all select 'CCC' as cc_code,'Bank Payments' as typeoftransaction,CAST(Id as varchar(max)) as RefNumber,loanpurpose as Name,amount from TermLoan where Status='2' union all select CC_Code,'Bank Payments' as typeoftransaction,CAST(ID as varchar(max)) as RefNumber,Description as Name,Debit as Amount from BankBook where PaymentType='Supplier' and Loanno is not null and status='2' union all select '' as CC_Code,'Bank Payments' as typeoftransaction,CAST(Loan_no as varchar(max)) as RefNumber,b.Description as Name,b.Debit as Amount from bankbook b join AddUnsecuredLoan al on al.transaction_no=b.transaction_no and al.type='Return' where b.status='A2R' union all select CC_Code,'Bank Payments' as typeoftransaction,af.FDR as RefNumber,bk.Description as Name,bk.Debit as Amount from AddFD af join bankbook bk on af.fdr=bk.fdr where af.Approval_status='2'", con);
        }
        if (Request.QueryString["Type"] == "HoAdmin" && Request.QueryString["Name"] == "Bank Receipt")
        {
            da = new SqlDataAdapter("select b.CC_Code,'Bank Receipt' as typeoftransaction,CAST(i.InvoiceNo as varchar(max)) as RefNumber,b.Description as Name,b.Credit as Amount from BankBook b join Invoice i on b.InvoiceNo=i.InvoiceNo where Credit is not null and b.InvoiceNo is not null and b.PaymentType not in ('Advance','Retention') and b.status='1' union all  select bb.CC_Code,'Bank Receipt' as typeoftransaction,CAST(iv.InvoiceNo as varchar(max)) as RefNumber,bb.Description as Name,bb.Credit as Amount  from BankBook bb join Invoice iv on bb.Transaction_No=iv.Ret_TranNo and bb.status=iv.Ret_Status  where bb.status='1A' union all select '' as CC_Code,'Bank Receipt' as typeoftransaction,CAST(sc.Transaction_no as varchar(max)) as RefNumber,bk.Description as Name,bk.Credit as Amount from AddShareCapital sc join bankbook bk on sc.transaction_no=bk.transaction_no where sc.status='1' union all select fd.CC_Code,'Bank Receipt' as typeoftransaction,fd.FDR as RefNumber,bk.Description as Name,bk.Credit as Amount from fd_claim fd  join bankbook bk on fd.transaction_no=bk.transaction_no and fd.fdr=bk.fdr where bk.status='1' and fd.Type='Principle' union all select sc.Ded_CCCode as CC_Code,'Bank Receipt' as typeoftransaction,sc.FDR as RefNumber,bk.Description as Name,bk.Credit as Amount from FD_Intrest sc join bankbook bk on sc.Tran_no=bk.transaction_no where sc.status='I1' union all select '' as CC_Code,'Bank Receipt' as typeoftransaction,CAST(au.Loan_no as varchar(max)) as RefNumber,bb.Description as Name,bb.Credit as Amount from AddUnsecuredLoan au join BankBook bb on bb.Transaction_No=au.Transaction_no where au.Type in ('New','Additional') and au.status in ('A1')", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "Bank Receipt")
        {
            da = new SqlDataAdapter("select '' as CC_Code,'Bank Receipt' as typeoftransaction,CAST(sc.Transaction_no as varchar(max)) as RefNumber,bk.Description as Name,bk.Credit as Amount from AddShareCapital sc join bankbook bk on sc.transaction_no=bk.transaction_no where sc.status='2' union all select fd.CC_Code,'Bank Receipt' as typeoftransaction,CAST(fd.FDR as varchar(max)) as RefNumber,bk.Description as Name,bk.Credit as Amount from fd_claim fd  join bankbook bk on fd.transaction_no=bk.transaction_no and fd.fdr=bk.fdr where bk.status='2' and fd.Type='Principle' union all select sc.Ded_CCCode as CC_Code,'Bank Receipt' as typeoftransaction,CAST(sc.FDR as varchar(max)) as RefNumber,bk.Description as Name,bk.Credit as Amount from FD_Intrest sc join bankbook bk on sc.Tran_no=bk.transaction_no where sc.status='I2' union all select '' as CC_Code,'Bank Receipt' as typeoftransaction,CAST(au.Loan_no as varchar(max)) as RefNumber,bb.Description as Name,bb.Credit as Amount from AddUnsecuredLoan au join BankBook bb on bb.Transaction_No=au.Transaction_no where au.Type in ('New','Additional') and au.status in ('A2')", con);
        }
        //-------------- Indent Start ------------
        if (Request.QueryString["Type"] == "PM" && Request.QueryString["Name"] == "Indent")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'Indent' as typeoftransaction,Indent_No as RefNumber,'' as Name,(Select Sum(isnull(Amount,0)) from indent_list il where il.Indent_no=i.Indent_no group by il.Indent_no ) as Amount from indents i where  status='1'", con);
        }
        if (Request.QueryString["Type"] == "CSK" && Request.QueryString["Name"] == "Indent")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'Indent' as typeoftransaction,Indent_No as RefNumber,'' as Name,(Select Sum(isnull(Amount,0)) from indent_list il where il.Indent_no=i.Indent_no group by il.Indent_no ) as Amount from indents i where  status='2'", con);
        }
        if (Request.QueryString["Type"] == "PUM" && Request.QueryString["Name"] == "Indent")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'Indent' as typeoftransaction,Indent_No as RefNumber,'' as Name,(Select Sum(isnull(Amount,0)) from indent_list il where il.Indent_no=i.Indent_no group by il.Indent_no ) as Amount from indents i where  status in ('3','4')", con);
        }
        if (Request.QueryString["Type"] == "CMC" && Request.QueryString["Name"] == "Indent")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'Indent' as typeoftransaction,Indent_No as RefNumber,'' as Name,(Select Sum(isnull(Amount,0)) from indent_list il where il.Indent_no=i.Indent_no group by il.Indent_no ) as Amount from indents i where  status='5'", con);
        }
        if (Request.QueryString["Type"] == "SAdmin" && Request.QueryString["Name"] == "Indent")
        {
            da = new SqlDataAdapter("select distinct cc_code ,'Indent' as typeoftransaction,Indent_No as RefNumber,'' as Name,(Select Sum(isnull(Amount,0)) from indent_list il where il.Indent_no=i.Indent_no group by il.Indent_no ) as Amount from indents i where  status='6A'", con);
        }
        //----------Indent End-------------------------//
        da.Fill(ds, "Fill");
        lbltransctiontype.Text = ds.Tables["Fill"].Rows[0].ItemArray[1].ToString();
        grdstatus.DataSource = ds.Tables["Fill"];
        grdstatus.DataBind();
    }
    private decimal Amount = (decimal)0.0;
    protected void grdstatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Request.QueryString["Name"] == "DCA Budget Amend" || Request.QueryString["Name"] == "DCA Budget Assign")
        {
            if (e.Row.Cells[2].Text == "DCAName")
            {
                grdstatus.Columns[2].Visible = true;
                grdstatus.Columns[3].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

        }
    }
}