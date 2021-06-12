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

public partial class HRVerticalMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            if (Session["roles"].ToString() == "Accountant")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
                
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
                
            }
            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
            
            }
            else if (Session["roles"].ToString() == "HoAdmin")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
               
            }
            else if (Session["roles"].ToString() == "HR")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
            }
            else if (Session["roles"].ToString() == "Project Manager")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
            }
            else if (Session["roles"].ToString() == "Sr.Accountant")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
            }
            else if (Session["roles"].ToString() == "StoreKeeper")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                employeeregister.Visible = true;
                ViewEmployeeDetails.Visible = true;
                Employeeleft.Visible = false;

            }
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {

                employeeregister.Visible = true;
                ViewEmployeeDetails.Visible = true;
                Employeeleft.Visible = true;
            }
            else if (Session["roles"].ToString() == "HRGM")
            {
                employeeregister.Visible = true;
                ViewEmployeeDetails.Visible = true;
                Employeeleft.Visible = false;
            }
            else if (Session["roles"].ToString() == "Hr.Asst")
            {
                employeeregister.Visible = true;
                ViewEmployeeDetails.Visible = true;
                Employeeleft.Visible = false;
            }
            else if (Session["roles"].ToString() == "HR")
            {
                employeeregister.Visible = true;
                ViewEmployeeDetails.Visible = true;
                Employeeleft.Visible = false;
            }
            else if (Session["roles"].ToString() == "Auditor")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
            }
            else if (Session["roles"].ToString() == "SupportUser" || Session["roles"].ToString() == "SupportAdmin")
            {
                employeeregister.Visible = false;
                ViewEmployeeDetails.Visible = false;
                Employeeleft.Visible = false;
            }
        }
        else 
        {
            Response.Redirect("SessionExpire.aspx");
        }
    


    }
}
