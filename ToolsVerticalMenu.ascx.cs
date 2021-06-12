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

public partial class ToolsVerticalMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            if (Session["roles"].ToString() == "StoreKeeper")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
               // accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "Sr.Accountant")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
              //  accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = true;
                ExciseRegistration.Visible = true;
                ServiceRegistration.Visible = true;
                ViewExciseVATRegistration.Visible = true;
                tritdep.Visible = false;
                TrGstregistration.Visible = true;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = true;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "Accountant")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
              //  accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "Project Manager")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
               // accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
              //  accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = true;
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
              //  accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = true;
               // accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = true;

            }
            else if (Session["roles"].ToString() == "HoAdmin")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = true;
               // accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = true;
                ExciseRegistration.Visible = true;
                ServiceRegistration.Visible = true;
                ViewExciseVATRegistration.Visible = true;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = true;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "HR")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
               // accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;

            }
            else if (Session["roles"].ToString() == "HR.Asst")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
              //  accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = true;
                //accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = true;
                ExciseRegistration.Visible = true;
                ServiceRegistration.Visible = true;
                ViewExciseVATRegistration.Visible = true;
                tritdep.Visible = true;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = true;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = true;
            }
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                voucherlimit.Visible = true;
                viewreports.Visible = true;
                //accountcreation.Visible = true;
                cashvoucherlimit.Visible = true;
                Overhead.Visible = true;
                AssetDep.Visible = true;
                ccaccrued.Visible = true;
                TrSemiAssetDep.Visible = true;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                exreg.Visible = false;
                tritdep.Visible = true;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "Auditor")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
              //  accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
            else if (Session["roles"].ToString() == "SupportUser" || Session["roles"].ToString() == "SupportAdmin")
            {
                voucherlimit.Visible = false;
                viewreports.Visible = false;
              //  accountcreation.Visible = false;
                cashvoucherlimit.Visible = false;
                Overhead.Visible = false;
                AssetDep.Visible = false;
                ccaccrued.Visible = false;
                TrSemiAssetDep.Visible = false;
                VATRegistration.Visible = false;
                ExciseRegistration.Visible = false;
                ServiceRegistration.Visible = false;
                ViewExciseVATRegistration.Visible = false;
                tritdep.Visible = false;
                TrGstregistration.Visible = false;
                TrGstApproval.Visible = false;
                Trhsnsaccodecreation.Visible = false;
                Trverifyhsnasncode.Visible = false;
            }
        }
        else
        {
            Response.Redirect("SessionExpire.aspx");
        }

    }
}
