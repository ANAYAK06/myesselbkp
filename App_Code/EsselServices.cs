using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Specialized;
using AjaxControlToolkit;
using System.Data;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Text;

/// <summary>
/// Summary description for EsselServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class EsselServices : System.Web.Services.WebService {

    public EsselServices () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetCCName(string contextKey)
    {
        string ccname;
        try
        {
            ccname = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select cc_name c from cost_center where cc_code=@CCCode", new SqlParameter("@CCCode", contextKey)).ToString();
        }
        catch
        {
            ccname = "";
        }
        return ccname;
    }

    [WebMethod]
    public string GetCCBudget(string contextKey)
    {
        string[] param = contextKey.Split('|');
        SqlParameter[] p = new SqlParameter[2];
        p[0] = new SqlParameter("@CCCode", param[0]);
        p[1] = new SqlParameter("@Year", param[1]);
        string Budget;
        try
        {
            Budget = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select balance from budget_cc where cc_code=@CCCode And Year=@Year", p).ToString().Replace(".0000", ".00");
        }
        catch(Exception ex)
        {
            Budget = "Budget Not Assigned";
        }
        return Budget;
    }

    [WebMethod]
    public string GetDCAName(string contextKey)
    {
        string dca;
        try
        {
            dca = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select dca_name from dca where dca_code=@DCACode", new SqlParameter("@DCACode", contextKey)).ToString();
        }
        catch
        {
            dca = "";
        }
        return dca;
    }

    [WebMethod]
    public string GetDCABudget(string contextKey)
    {
        string[] param=contextKey.Split('|');
        SqlParameter[] p = new SqlParameter[3];
        p[0] = new SqlParameter("@CCCode", param[0]);
        p[1] = new SqlParameter("@DCACode", param[1]);
        p[2] = new SqlParameter("@Year", param[2]);
        string Dcabud;
        try
        {
            Dcabud = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select dca_yearly_bal from yearly_dcabudget where dca_code=@DCACode and cc_code=@CCCode and Year=@Year", p).ToString().Replace(".0000", ".00");
        }
        catch
        {
            Dcabud = "Budget Not Assigned";
        }
        return Dcabud;
    }

    [WebMethod]
    public string GetSubDCAName(string contextKey)
    {
        string subdca;
        try
        {
            subdca = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select subdca_name from subdca where subdca_code=@SubDCA", new SqlParameter("@SubDCA", contextKey)).ToString();
        }
        catch
        {
            subdca = "";
        }
        return subdca;
    }

    [WebMethod]
    public string GetVendorName(string contextKey)
    {
        string subdca;
        try
        {
            subdca = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "Select vendor_name from vendor where vendor_id=@vendor", new SqlParameter("@vendor", contextKey)).ToString();
        }
        catch
        {
            subdca = "Vendor Not Found";
        }
        return subdca;
    }

    [WebMethod]
    public CascadingDropDownNameValue[] FinancialYear(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> years = new List<CascadingDropDownNameValue>();
        int y = System.DateTime.Now.Year - 2;
        y = (System.DateTime.Now.Month < 4) ? y - 1 : y;
        for (int i = y; i < y + 5; i++)
        {
            string year = i.ToString() + "-" + (i + 1).ToString().Substring(2, 2);
            years.Add(new CascadingDropDownNameValue(year, year));
        }
        return years.ToArray();
    }


    [WebMethod]
    public CascadingDropDownNameValue[] BudgetYear(string knownCategoryValues, string category)
    {

        //Create dataadapter and fill the dataset
        DataSet objDs = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "Select distinct Year from budget_cc order by Year");

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["Year"].ToString();
            string countryName = dRow["Year"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();

    }

    [WebMethod]
    public CascadingDropDownNameValue[] BudgetCC(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["Year"].ToString();

        //Create dataadapter and fill the dataset
        DataSet objDs = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "Select cc_code from budget_cc where Year=@Year", new SqlParameter("@Year", dcaID));

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["cc_code"].ToString();
            string countryName = dRow["cc_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();

    }

    [WebMethod]
    public CascadingDropDownNameValue[] BudgetDCA(string knownCategoryValues, string category)
    {
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

        SqlParameter[] p = new SqlParameter[2];
        p[0] = new SqlParameter("@CCCode",dcaa["CostCenter"].ToString());
        p[1] = new SqlParameter("@Year", dcaa["Year"].ToString());


        //Create dataadapter and fill the dataset
        DataSet objDs = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "Select d.dca_code from yearly_dcabudget y join dca d on y.dca_code=d.dca_code where Year=@Year and cc_code=@CCCode order by dca_code", p);

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();

    }

    [WebMethod]
    public CascadingDropDownNameValue[] BudgetSubDCA(string knownCategoryValues, string category)
    {
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

        SqlParameter[] p = new SqlParameter[3];
        p[0] = new SqlParameter("@CCCode", dcaa["CostCenter"].ToString());
        p[1] = new SqlParameter("@Year", dcaa["Year"].ToString());
        p[2] = new SqlParameter("@DCA", dcaa["DCA"].ToString());

        //Create dataadapter and fill the dataset
        DataSet objDs = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "Select d.subdca_code from yearly_dcabudget y join subdca d on y.dca_code=d.subdca_code where Year=@Year and cc_code=@CCCode and d.dca_code=@DCA order by subdca_code", p);

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["subdca_code"].ToString();
            string countryName = dRow["subdca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();

    }

    [WebMethod]
    public string GetSubDcaBudget(string contextKey)
    {
        string[] param = contextKey.Split('|');
        SqlParameter[] p = new SqlParameter[3];
        p[0] = new SqlParameter("@CCCode", param[0]);
        p[1] = new SqlParameter("@DCa", param[1]);
        p[2] = new SqlParameter("@Year", param[2]);

        DataTable table = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "Select d.subdca_code,subdca_name,Replace(budget_dca_yearly,'.0000','.00') Budget,Replace(dca_yearly_bal,'.0000','.00') Balance from subdca d join yearly_dcabudget y on d.subdca_code=y.dca_code where year=@Year and cc_code=@CCCode and d.dca_code=@DCa", p).Tables[0];


        StringBuilder b = new StringBuilder();

        if (table.Rows.Count > 0)
        {
            b.Append("<table border='1' style='background-color:#f3f3f3; border: #336699 3px solid; ");
            b.Append("width:450px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");

            b.Append("<tr><td colspan='4' style='background-color:#336699; color:white;'>");
            b.Append("<b>Sub DCA Budget: " + param[2] + "/" + param[0] + "/" + param[1] + "</b>"); b.Append("</td></tr>");
            b.Append("<tr><td style='width:80px;'><b>Sub&nbsp;DCA</b></td>");
            b.Append("<td style='width:80px;'><b>Sub&nbsp;DCA&nbsp;Name</b></td>");
            b.Append("<td><b>Budget</b></td><td><b>Balance</b></td></tr>");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                b.Append("<tr>");
                b.Append("<td>" + table.Rows[i]["subdca_code"].ToString() + "</td>");
                b.Append("<td>" + table.Rows[i]["subdca_name"].ToString() + "</td>");
                b.Append("<td>" + table.Rows[i]["Budget"].ToString() + "</td>");
                b.Append("<td>" + table.Rows[i]["Balance"].ToString() + "</td>");
                b.Append("</tr>");
            }
            b.Append("</table>");
        }
        else
        {
            b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ");
            b.Append("font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
            b.Append("<tr><td colspan='4' style='background-color:#336699; color:white;'>");
            b.Append("<b>No Sub DCA Budget</b></td></tr></table>");
        }

        return b.ToString();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] Vendors(string knownCategoryValues, string category, string contextKey)
    {
        DataTable objve = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "Select vendor_id,vendor_name from vendor where vendor_type=@vendortype and status='2'", new SqlParameter("@vendortype", contextKey)).Tables[0];

        //create list and add items in it 
        //by looping through datatable
        List<CascadingDropDownNameValue> vendorcode = new List<CascadingDropDownNameValue>();
        //vendorcode.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow vendor in objve.Rows)
        {
            string vID = vendor["vendor_id"].ToString();
            string vName = vendor["vendor_name"].ToString();
            string vcode = vendor["vendor_id"].ToString();
            string CC = vName + " ,  " + vcode;
            vendorcode.Add(new CascadingDropDownNameValue(CC, vID));

        }
        return vendorcode.ToArray();
    }
    [WebMethod]
    public string VendorDetails(string contextKey)
    {
        DataTable table = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "SELECT [vendor_name],[vendor_type],[servicetax_no],[vat_no],[pan_no],[tin_no],[pf_no],[cst_no],[bank_name],[Account_no],[IFSC_code] FROM [vendor] where id=@Id", new SqlParameter("@id", contextKey)).Tables[0];

        StringBuilder b = new StringBuilder();

        if (table.Rows.Count > 0)
        {
            //b.Append("<table  border='1' style='background-color:#f3f3f3; border: #336699 3px solid; ");
            //b.Append("width:450px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");

            //b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>");
            b.Append("<table class='estbl' cellspacing='0' cellpadding='6'>");

            b.Append("<tr><th colspan='6'>");
            b.Append(table.Rows[0]["vendor_name"].ToString()); b.Append("</th></tr>");

            if (table.Rows[0]["vendor_type"].ToString() == "Service Provider")
            {
                b.Append("<tr>");
                b.Append("<td><b>ServiceTax No</b></td>");
                b.Append("<td><b>PAN No</b></td>");
                b.Append("<td><b>PF Reg No</b></td>");
                b.Append("<td><b>Bank Name</b></td>");
                b.Append("<td><b>A/C No</b></td>");
                b.Append("<td><b>IFSC Code</b></td>");
                b.Append("</tr>");

                b.Append("<tr>");
                b.Append("<td>&nbsp;" + table.Rows[0]["servicetax_no"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["pan_no"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["pf_no"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["bank_name"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["Account_no"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["IFSC_code"].ToString() + "</td>");
                b.Append("</tr>");
            }
            else
            {
                b.Append("<tr>");
                b.Append("<td><b>VAT No</b></td>");
                b.Append("<td><b>TIN No</b></td>");
                b.Append("<td><b>CST No</b></td>");
                b.Append("<td><b>Bank Name</b></td>");
                b.Append("<td><b>A/C No</b></td>");
                b.Append("<td><b>IFSC Code</b></td>");
                b.Append("</tr>");

                b.Append("<tr>");
                b.Append("<td>&nbsp;" + table.Rows[0]["vat_no"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["tin_no"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["cst_no"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["bank_name"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["Account_no"].ToString() + "</td>");
                b.Append("<td>&nbsp;" + table.Rows[0]["IFSC_code"].ToString() + "</td>");
                b.Append("</tr>");
            }
            
            b.Append("</table>");
        }

        return b.ToString();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] VendorType(string knownCategoryValues, string category)
    {
        DataTable objve = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "Select Vendor_Type from vendor_type").Tables[0];

        //create list and add items in it 
        //by looping through datatable
        List<CascadingDropDownNameValue> vendorcode = new List<CascadingDropDownNameValue>();
        //vendorcode.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow vendor in objve.Rows)
        {
            string vID = vendor["Vendor_Type"].ToString();
            string vName = vendor["Vendor_Type"].ToString();
            vendorcode.Add(new CascadingDropDownNameValue(vName, vID));
        }
        return vendorcode.ToArray();
    }
    [WebMethod]
    public string GetAgencyName(string contextKey)
    {
        string Agencyname;
        try
        {
            Agencyname = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select agencyname from agency where agencycode=@agencycode", new SqlParameter("@agencycode", contextKey)).ToString();
        }
        catch
        {
            Agencyname = "";
        }
        return Agencyname;
    }

    [WebMethod]
    public string GetAgencyNameusingloanno(string contextKey)
    {
        string Agencyname;
        try
        {
            Agencyname = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select distinct y.agencyname from agency y join termloan t on y.agencycode=t.agencycode where t.loanno=@agencycode", new SqlParameter("@agencycode", contextKey)).ToString();
        }
        catch
        {
            Agencyname = "";
        }
        return Agencyname;
    }
    [WebMethod]
    public string venname(string contextKey)
    {
        string vendorname;
        try
        {
            vendorname = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select vendor_name from vendor where vendor_id=@vendorcode", new SqlParameter("@vendorcode", contextKey)).ToString();
        }
        catch
        {
            vendorname = "";
        }
        return vendorname;
    }
    [WebMethod]
    public string GetClientName(string contextKey)
    {
        string ccname;
        try
        {
            ccname = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select client_name  from client where client_id=@CCCode", new SqlParameter("@CCCode", contextKey)).ToString();
        }
        catch
        {
            ccname = "";
        }
        return ccname;
    }
    [WebMethod]
    public string GetSubClientName(string contextKey)
    {
        string ccname;
        try
        {
            ccname = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select (c.client_name+' , '+ sc.branch)as name  from subclient sc join client c on c.client_id=sc.client_id where sc.subclient_id=@CCCode", new SqlParameter("@CCCode", contextKey)).ToString();
        }
        catch
        {
            ccname = "";
        }
        return ccname;
    }
    [WebMethod]
    public string GetVendorNameAddress(string contextKey)
    {


        DataTable table = SqlHelper.ExecuteDataset(Utilities.con(), CommandType.Text, "select vendor_name,address from vendor  where vendor_name=@Vendorname", new SqlParameter("@Vendorname", contextKey)).Tables[0];
        if (table.Rows.Count > 0)
        {

        }

        return table.Rows[0]["vendor_name"].ToString() + "," + table.Rows[0]["address"].ToString();

    }
    [WebMethod]
    public string GetITName(string contextKey)
    {
        string Agencyname;
        try
        {
            Agencyname = SqlHelper.ExecuteScalar(Utilities.con(), CommandType.Text, "select it_name from IT where it_code=@ITcode", new SqlParameter("@ITcode", contextKey)).ToString();
        }
        catch
        {
            Agencyname = "";
        }
        return Agencyname;
    }

    [WebMethod]
    public CascadingDropDownNameValue[] FinancialYear1(string knownCategoryValues, string category)
    {

        List<CascadingDropDownNameValue> years = new List<CascadingDropDownNameValue>();
        for (int i = 2009; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                string year = i.ToString() + "-" + (i + 1).ToString().Substring(2, 2);
                years.Add(new CascadingDropDownNameValue(year, year));
            }
        }
        return years.ToArray();
    }


}

