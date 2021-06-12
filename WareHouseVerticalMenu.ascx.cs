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

public partial class WareHouseVerticalMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            if (Session["roles"].ToString() == "StoreKeeper")
            {
                Inbox.Visible = true;
                Transfer_Out.Visible = true;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = true;
                Daily_Issue_Report.Visible = true;
                Daily_Issues.Visible = true;
                G_LD_Report.Visible = true;
                V_LD_Reports.Visible = true;
                MR_Report.Visible = true;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = true;
                trStockReconcillation.Visible = true;
                trstoreclosing.Visible = true;
                trapprovalcloseditems.Visible = true;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = false;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;

            }
            else if (Session["roles"].ToString() == "Sr.Accountant")
            {
                Inbox.Visible = false;
                
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = false;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = false;
                MR_Report.Visible = false;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = false;
                trStockReconcillation.Visible = false;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = true;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = true;
                trstockpurchasereport.Visible = true;
                trstockpurchaseconsolidatereport.Visible = true;

            }
            else if (Session["roles"].ToString() == "Accountant")
            {

                Inbox.Visible = false;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = false;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = false;
                MR_Report.Visible = false;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = false;
                trStockReconcillation.Visible = false;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = false;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;

            }
            else if (Session["roles"].ToString() == "Project Manager")
            {
                Inbox.Visible = true;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = true;
                Daily_Issue_Report.Visible = true;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = true;
                MR_Report.Visible = true;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = true;
                trStockReconcillation.Visible = true;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = true;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = true;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;

            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {
               
                Inbox.Visible = true;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = true;
                View_Issued_Items.Visible = true;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = true;
                V_LD_Reports.Visible = true;
                MR_Report.Visible = true;
                StkLedger.Visible = true;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = true;
                trStockReconcillation.Visible = true;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = true;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = true;
                trstockpurchasereport.Visible = true;
                trstockpurchaseconsolidatereport.Visible = true;

            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                Inbox.Visible = true;
                Transfer_Out.Visible = true;
                Issue_From_central_Store.Visible = true;
                View_Issued_Items.Visible = true;
                Daily_Issue_Report.Visible = true;
                Daily_Issues.Visible = true;
                G_LD_Report.Visible = true;
                V_LD_Reports.Visible = true;
                MR_Report.Visible = true;
                StkLedger.Visible = true;
                trassettransfer.Visible = true;
                Trstock.Visible = true;
                Trstkverify.Visible = false;
                Trtrack.Visible = true;
                trStockReconcillation.Visible = true;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = true;
                trassetsales.Visible = true;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = true;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;

            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
                Inbox.Visible = true;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = true;
                Daily_Issue_Report.Visible = true;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = true;
                MR_Report.Visible = true;
                StkLedger.Visible = true;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = true;
                Trtrack.Visible = true;
                trStockReconcillation.Visible = true;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = true;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = true;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = true;
                trstockpurchasereport.Visible = true;
                trstockpurchaseconsolidatereport.Visible = true;

            }
            else if (Session["roles"].ToString() == "HoAdmin")
            {
               
                Inbox.Visible = false;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = false;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = false;
                MR_Report.Visible = false;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = false;
                trStockReconcillation.Visible = false;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = true;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = true;
                trassetsalereport.Visible = true;
                trstockpurchasereport.Visible = true;
                trstockpurchaseconsolidatereport.Visible = true;

            }
            else if (Session["roles"].ToString() == "HR")
            {               
                Inbox.Visible = false;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = false;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = false;
                MR_Report.Visible = false;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = false;
                trStockReconcillation.Visible = false;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = false;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;

            }
            else if (Session["roles"].ToString() == "HR.Asst")
            {
               
                Inbox.Visible = false;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = false;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = false;
                MR_Report.Visible = false;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = false;
                trStockReconcillation.Visible = false;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = false;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;

            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                Inbox.Visible = true;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = true;
                Daily_Issue_Report.Visible = true;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = true;
                MR_Report.Visible = true;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = true;
                trStockReconcillation.Visible = true;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = true;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = true;
                trstockpurchasereport.Visible = true;
                trstockpurchaseconsolidatereport.Visible = true;

            }
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                Inbox.Visible = false;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = false;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = false;
                MR_Report.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = false;
                trStockReconcillation.Visible = true;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                StkLedger.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = true;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = true;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;
            }
            else if (Session["roles"].ToString() == "Auditor")
            {

                Inbox.Visible = false;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = false;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = false;
                MR_Report.Visible = false;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = false;
                trStockReconcillation.Visible = false;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = false;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;
            }
            else if (Session["roles"].ToString() == "SupportUser" || Session["roles"].ToString() == "SupportAdmin")
            {

                Inbox.Visible = false;
                Transfer_Out.Visible = false;
                Issue_From_central_Store.Visible = false;
                View_Issued_Items.Visible = false;
                Daily_Issue_Report.Visible = false;
                Daily_Issues.Visible = false;
                G_LD_Report.Visible = false;
                V_LD_Reports.Visible = false;
                MR_Report.Visible = false;
                StkLedger.Visible = false;
                trassettransfer.Visible = false;
                Trstock.Visible = false;
                Trstkverify.Visible = false;
                Trtrack.Visible = false;
                trStockReconcillation.Visible = false;
                trstoreclosing.Visible = false;
                trapprovalcloseditems.Visible = false;
                trstoreclosingapproval.Visible = false;
                trassetsales.Visible = false;
                trverifyassetsale.Visible = false;
                trassetsalespayment.Visible = false;
                trassetsaleverifypayment.Visible = false;
                trassetsalereport.Visible = false;
                trstockpurchasereport.Visible = false;
                trstockpurchaseconsolidatereport.Visible = false;
            }
        }
        else
        {
            Response.Redirect("SessionExpire.aspx");
        }
    }
}
