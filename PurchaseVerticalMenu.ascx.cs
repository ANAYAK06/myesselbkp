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

public partial class PurchaseVerticalMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["user"] != null)
        {
            if (Session["roles"].ToString() == "StoreKeeper")
            {
             
               
                Raise_Indent.Visible=true;
                Raise_Po.Visible=false;
                View_Indent.Visible=true;
                View_Po.Visible=true;
                View_Stock.Visible=true;
                View_Vendor.Visible=true;
                Add_Items.Visible = true;
                Verify_Items.Visible = false;
                Approved_Items.Visible = true;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=true;
                //Invoice_Entry.Visible=true;
                //Invoice_Verification.Visible=false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = false;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = true;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = true;

            }
            else if (Session["roles"].ToString() == "Sr.Accountant")
            {
               
              
                
                Raise_Indent.Visible=false;
                Raise_Po.Visible=false;
                View_Indent.Visible=false;
                View_Po.Visible=false;
                View_Stock.Visible=false;
                View_Vendor.Visible=true;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=true;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=false;
                //Invoice_Verification.Visible=false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = true;
                trverifysppo.Visible = false;
                trclosepo.Visible = true;
                trspreport.Visible = true;
                tramenedsppo.Visible = true;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = true;
            }
            else if (Session["roles"].ToString() == "Accountant")
            {
                Raise_Indent.Visible=false;
                Raise_Po.Visible=false;
                View_Indent.Visible=false;
                View_Po.Visible=false;
                View_Stock.Visible=false;
                View_Vendor.Visible=true;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=false;
                //Invoice_Verification.Visible=false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = false;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = false;
            }
            else if (Session["roles"].ToString() == "Project Manager")
            {
               
                Raise_Indent.Visible=false;
                Raise_Po.Visible=false;
                View_Indent.Visible=true;
                View_Po.Visible=true;
                View_Stock.Visible=true;
                View_Vendor.Visible=true;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=false;
                //Invoice_Verification.Visible=true;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = true;
                trclosepo.Visible = true;
                trspreport.Visible = true;
                tramenedsppo.Visible = true;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = true;
                trsupplierporeport.Visible = true;
            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {
               
                Raise_Indent.Visible=false;
                Raise_Po.Visible=true;
                View_Indent.Visible=true;
                View_Po.Visible=true;
                View_Stock.Visible=true;
                View_Vendor.Visible=true;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = true;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=false;
                //Invoice_Verification.Visible=false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = true;
                trclosepo.Visible = false;
                trspreport.Visible = true;
                tramenedsppo.Visible = true;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = true;
                trsupplierporeport.Visible = true;
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                Raise_Indent.Visible=true;
                Raise_Po.Visible=false;
                View_Indent.Visible=true;
                View_Po.Visible=true;
                View_Stock.Visible=true;
                View_Vendor.Visible=true;
                Add_Items.Visible = true;
                Verify_Items.Visible = true;
                Approved_Items.Visible = true;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=true;
                //Invoice_Entry.Visible=true;
                //Invoice_Verification.Visible=true;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = false;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = true;
                Invoice_VerificationGST.Visible = true;
                trsupplierporeport.Visible = true;

            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
               
                Raise_Indent.Visible=false;
                Raise_Po.Visible=false;
                View_Indent.Visible=true;
                View_Po.Visible=true;
                View_Stock.Visible=true;
                View_Vendor.Visible=true;
                Add_Items.Visible = true;
                Verify_Items.Visible = true;
                Approved_Items.Visible = true;
                Amend_Items.Visible = true;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=false;
                //Invoice_Verification.Visible=true;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = true;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = true;
                trsupplierporeport.Visible = true;
            }
            else if (Session["roles"].ToString() == "HoAdmin")
            {
                
                Raise_Indent.Visible=false;
                Raise_Po.Visible=false;
                View_Indent.Visible=false;
                View_Po.Visible=false;
                View_Stock.Visible=true;
                View_Vendor.Visible=true;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=true;
                //Invoice_Verification.Visible=false;
                SPInvoice_Verification.Visible = true;
                trsppo.Visible = false;
                trverifysppo.Visible = true;
                trclosepo.Visible = true;
                trspreport.Visible = true;
                tramenedsppo.Visible = true;
                trverifyvendor.Visible = true;
                Invoice_EntryGST.Visible = true;
                Invoice_VerificationGST.Visible = true;
                trsupplierporeport.Visible = true;
            }
            else if (Session["roles"].ToString() == "HR")
            {
                
                Raise_Po.Visible=false;
                View_Indent.Visible=false;
                View_Po.Visible=false;
                View_Stock.Visible=false;
                View_Vendor.Visible=true;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=false;
                //Invoice_Verification.Visible=false;
                Raise_Indent.Visible = false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = false;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = false;
            }
            else if (Session["roles"].ToString() == "HR.Asst")
            {
               
                Raise_Indent.Visible=false;
                Raise_Po.Visible=false;
                View_Indent.Visible=false;
                View_Po.Visible=false;
                View_Stock.Visible=true;
                View_Vendor.Visible=true;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=false;
                //Invoice_Verification.Visible=false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = false;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = false;
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                
                Raise_Indent.Visible=false;
                Raise_Po.Visible=false;
                View_Indent.Visible=true;
                View_Po.Visible=true;
                View_Stock.Visible=true;
                View_Vendor.Visible=true;
                Add_Items.Visible = false;
                Verify_Items.Visible = true;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible=false;
                Receive_Items.Visible=false;
                //Invoice_Entry.Visible=false;
                //Invoice_Verification.Visible=true;
                SPInvoice_Verification.Visible = true;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = true;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = true;

            }
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                Raise_Indent.Visible = false;
                Raise_Po.Visible = false;
                View_Indent.Visible = false;
                View_Po.Visible = false;
                View_Stock.Visible = true;
                View_Vendor.Visible = false;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible = false;
                Receive_Items.Visible = false;
                //Invoice_Entry.Visible = false;
                //Invoice_Verification.Visible = false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = true;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = true;
            }
            else if (Session["roles"].ToString() == "Auditor")
            {

                Raise_Indent.Visible = false;
                Raise_Po.Visible = false;
                View_Indent.Visible = false;
                View_Po.Visible = false;
                View_Stock.Visible = false;
                View_Vendor.Visible = false;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible = false;
                Receive_Items.Visible = false;
                //Invoice_Entry.Visible = false;
                //Invoice_Verification.Visible = false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = false;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = false;
            }
            else if (Session["roles"].ToString() == "SupportUser" || Session["roles"].ToString() == "SupportAdmin")
            {

                Raise_Indent.Visible = false;
                Raise_Po.Visible = false;
                View_Indent.Visible = false;
                View_Po.Visible = false;
                View_Stock.Visible = false;
                View_Vendor.Visible = false;
                Add_Items.Visible = false;
                Verify_Items.Visible = false;
                Approved_Items.Visible = false;
                Amend_Items.Visible = false;
                Add_Vendor.Visible = false;
                Receive_Items.Visible = false;
                //Invoice_Entry.Visible = false;
                //Invoice_Verification.Visible = false;
                SPInvoice_Verification.Visible = false;
                trsppo.Visible = false;
                trverifysppo.Visible = false;
                trclosepo.Visible = false;
                trspreport.Visible = false;
                tramenedsppo.Visible = false;
                trverifyvendor.Visible = false;
                Invoice_EntryGST.Visible = false;
                Invoice_VerificationGST.Visible = false;
                trsupplierporeport.Visible = false;
            }

        }
        else 
        {
            Response.Redirect("SessionExpire.aspx");
        }
    }
   
}
