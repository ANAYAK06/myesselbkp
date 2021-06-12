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

/// <summary>
/// Summary description for cascadingDCA
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class cascadingDCA : System.Web.Services.WebService
{
    
    string strConnection = ConfigurationManager.AppSettings["esselDB"];
    
    /// <summary>
    /// WebMethod to populate country Dropdown
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <returns>countrynames</returns>
    /// 



    [WebMethod]
    public CascadingDropDownNameValue[] CC(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select cc_code,cc_name from cost_center where status in ('Old','New') GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "Select Cost Center"));
        cccodes.Add(new CascadingDropDownNameValue("Select All", "Select All"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }


    [WebMethod]
    public CascadingDropDownNameValue[] DCA(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select * from dca";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));
        dcacodes.Add(new CascadingDropDownNameValue("Select All", "1"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] subDCA(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["dca"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", dcaID);
        cmd.CommandText =
        "Select * from subdca where dca_code = @dcaid";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> subDCA =  new List<CascadingDropDownNameValue>();
       // subDCA.Add(new CascadingDropDownNameValue("Select Sub DCA", "0"));
        foreach (DataRow sdcRow in objSDca.Tables[0].Rows)
        {
            string sDcaID = sdcRow["subdca_code"].ToString();
            string sDcaName = sdcRow["subdca_code"].ToString();
            subDCA.Add(new CascadingDropDownNameValue(sDcaName, sDcaID));
        }
        return subDCA.ToArray();
    }


    [WebMethod]
    public CascadingDropDownNameValue[] yearly(string knownCategoryValues, string category)
    {
        string YID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> year = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        year.Add(new CascadingDropDownNameValue("Select All", "0"));


        string year1 = DateTime.Now.Year.ToString();
        int k = 0;
        for (int i = 2005; i <= Convert.ToInt32(year1); i++)
        {

            string j = i.ToString();
            k = k + 1;
            year.Add(new CascadingDropDownNameValue(j, k.ToString()));
        }
        return year.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] ISDCA(string knownCategoryValues, string category)
    {
        string ccID;
        string year = DateTime.Now.Year.ToString();
        StringDictionary cca =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        ccID = cca["cc"].ToString();
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@cc_code", ccID);

        cmd.CommandText = "select dca_code from yearly_dcabudget where cc_code=@cc_code and year='"+year+"'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] ISdc(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select * from dca";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }


    [WebMethod]
    public CascadingDropDownNameValue[] ISCC(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select cc_code from budget_cc";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] ISDCAyearly(string knownCategoryValues, string category)
    {
        string ccID;
        string year = DateTime.Now.Year.ToString();
        StringDictionary cca =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        ccID = cca["costcenter"].ToString();
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@cc_code", ccID);

        cmd.CommandText = "select dca_code from yearly_dcabudget where cc_code=@cc_code and year='" + year + "'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        //dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] vendor(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
       
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select vendor_name from vendor where vendor_type='Supplier' order by vendor_name asc";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_name"].ToString();
            string CCName = ccRow["vendor_name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] cash(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select * from dca";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        //dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] costcode(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select cc_code from Cost_Center where status in ('Old','New') and isnull(cc_status,0) not in ('TemporaryClosed','PermanentClosed','Approvals Waiting for Reopen') GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }


    [WebMethod]
    public CascadingDropDownNameValue[] role(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select Role_Name from Roles";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objRole = new DataSet();
        dAdapter.Fill(objRole);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> roles = new List<CascadingDropDownNameValue>();
        //roles.Add(new CascadingDropDownNameValue("Select Role", "0"));

        foreach (DataRow role in objRole.Tables[0].Rows)
        {
            string RID = role["Role_Name"].ToString();
            string RName = role["Role_Name"].ToString();
            roles.Add(new CascadingDropDownNameValue(RName, RID));
        }
        return roles.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] itcode(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select it_code from it";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select IT Code", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["it_code"].ToString();
            string CCName = ccRow["it_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] itname(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select it_code,it_name from it";


        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();


        List<CascadingDropDownNameValue> itname = new List<CascadingDropDownNameValue>();
        itname.Add(new CascadingDropDownNameValue("Select it_code", "Select it_name"));
        //itname.Add(new CascadingDropDownNameValue("Select All", "Select All"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string IT_CODE = ccRow["it_code"].ToString();
            string ITCODE = ccRow["it_code"].ToString();
            string ITNAME = ccRow["it_name"].ToString();

            string IT = ITCODE + " (" + ITNAME + ")";
            itname.Add(new CascadingDropDownNameValue(IT, IT_CODE));

        }
        return itname.ToArray();

    }
    [WebMethod]
    public CascadingDropDownNameValue[] ISVendor(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        string s1 = "Service Provider";
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select vendor_id from vendor where vendor_type='"+s1+"'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objve = new DataSet();
        dAdapter.Fill(objve);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> vendorcode = new List<CascadingDropDownNameValue>();
        //vendorcode.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow vendor in objve.Tables[0].Rows)
        {
            string vID = vendor["vendor_id"].ToString();
            string vName = vendor["vendor_id"].ToString();
            vendorcode.Add(new CascadingDropDownNameValue(vName, vID));
        }
        return vendorcode.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] ISClient(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select client_name from client";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objcl = new DataSet();
        dAdapter.Fill(objcl);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> Clientnames = new List<CascadingDropDownNameValue>();
        Clientnames.Add(new CascadingDropDownNameValue("Select Client", "0"));

        foreach (DataRow client in objcl.Tables[0].Rows)
        {
            string CID = client["client_name"].ToString();
            string CName = client["client_name"].ToString();
            Clientnames.Add(new CascadingDropDownNameValue(CName, CID));
        }
        return Clientnames.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] ISMonthly(string knownCategoryValues, string category)
    {
        string ccID;
        string month =String.Format("{0:MMM}",DateTime.Now);
        string year = DateTime.Now.Year.ToString();
        StringDictionary cca =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        ccID = cca["cc"].ToString();
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@cc_code", ccID);

        cmd.CommandText = "select dca_code from monthly_dcabudget where cc_code=@cc_code and year='"+year+"' and month='" + month + "'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        //dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] viewcash(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select * from dca";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));
        dcacodes.Add(new CascadingDropDownNameValue("Select All", "1"));
        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] viewsubdca(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["dca"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", dcaID);
        cmd.CommandText =
        "Select * from subdca where dca_code = @dcaid";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> subDCA = new List<CascadingDropDownNameValue>();
        subDCA.Add(new CascadingDropDownNameValue("Select Sub DCA", "0"));
        foreach (DataRow sdcRow in objSDca.Tables[0].Rows)
        {
            string sDcaID = sdcRow["subdca_code"].ToString();
            string sDcaName = sdcRow["subdca_code"].ToString();
            subDCA.Add(new CascadingDropDownNameValue(sDcaName, sDcaID));
        }
        return subDCA.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] date(string knownCategoryValues, string category)
    {
        string DID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary date =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> day = new List<CascadingDropDownNameValue>();
        day.Add(new CascadingDropDownNameValue("Select Date", "0"));

        //string date1 = DateTime.Now.Date.ToString();
        int k = 0;
        for (int i = 1; i <=31; i++)
        {

            string j = i.ToString();
            k = k + 1;
            day.Add(new CascadingDropDownNameValue(j, k.ToString()));
        }
        return day.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] ISvendor1(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        string s1 = "Material Supplier";
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select vendor_id from vendor where vendor_type='" + s1 + "'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_id"].ToString();
            string CCName = ccRow["vendor_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] payment(string knownCategoryValues, string category)
    {
      
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> payment = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        //payment.Add(new CascadingDropDownNameValue("Select", "0"));
        payment.Add(new CascadingDropDownNameValue("Cheque", "Cheque"));
        payment.Add(new CascadingDropDownNameValue("Cash", "Cash"));
        payment.Add(new CascadingDropDownNameValue("RTGS/E-transfer", "RTGS/E-transfer"));
        payment.Add(new CascadingDropDownNameValue("DD", "DD"));

      
        return payment.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] pay(string knownCategoryValues, string category)
    {

        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> pay = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        //payment.Add(new CascadingDropDownNameValue("Select", "0"));
        pay.Add(new CascadingDropDownNameValue("cheque", "1"));
        pay.Add(new CascadingDropDownNameValue("RTGS/E-transfer", "2"));
        

        return pay.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] typeofpay(string knownCategoryValues, string category)
    {

        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> typeofpay = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        //payment.Add(new CascadingDropDownNameValue("Select", "0"));
        typeofpay.Add(new CascadingDropDownNameValue("Invoice Service", "1"));
        typeofpay.Add(new CascadingDropDownNameValue("Invoice Supplier", "2"));
        typeofpay.Add(new CascadingDropDownNameValue("Retention", "3"));
        typeofpay.Add(new CascadingDropDownNameValue("Advance", "4"));
        typeofpay.Add(new CascadingDropDownNameValue("Refund", "5"));
        typeofpay.Add(new CascadingDropDownNameValue("Personal Loan", "6"));

        return typeofpay.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] typeofpayment(string knownCategoryValues, string category)
    {

        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> typeofpayment = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        //payment.Add(new CascadingDropDownNameValue("Select", "0"));
        typeofpayment.Add(new CascadingDropDownNameValue("Service Provider", "1"));
        typeofpayment.Add(new CascadingDropDownNameValue("Material Supplier", "2"));
        typeofpayment.Add(new CascadingDropDownNameValue("General", "3"));
        typeofpayment.Add(new CascadingDropDownNameValue("Withdrawl", "4"));


        
        return typeofpayment.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] refund(string knownCategoryValues, string category)
    {

        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> refund = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        //payment.Add(new CascadingDropDownNameValue("Select", "0"));
        refund.Add(new CascadingDropDownNameValue("FD", "1"));
        refund.Add(new CascadingDropDownNameValue("SD", "2"));


        return refund.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] from(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select bank_name from bank_branch where Status='3'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["bank_name"].ToString();
            string CCName = ccRow["bank_name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();

    }
    [WebMethod]
    public CascadingDropDownNameValue[] to(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select bank_name from bank_branch where Status='3'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["bank_name"].ToString();
            string CCName = ccRow["bank_name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] PoNo(string knownCategoryValues, string category)
    {


        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select po_no from po";

        
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["po_no"].ToString();
            string CCName = ccRow["po_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] amdpo(string knownCategoryValues, string category)
    {
        string poID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dds"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        cmd.CommandText =
        "Select po_amendedno from po_amended where po_no = @dcaid";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> subDCA = new List<CascadingDropDownNameValue>();
        // subDCA.Add(new CascadingDropDownNameValue("Select Sub DCA", "0"));
        foreach (DataRow sdcRow in objSDca.Tables[0].Rows)
        {
            string sDcaID = sdcRow["po_amendedno"].ToString();
            string sDcaName = sdcRow["po_amendedno"].ToString();
            subDCA.Add(new CascadingDropDownNameValue(sDcaName, sDcaID));
        }
        return subDCA.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] inno(string knownCategoryValues, string category)
    {
        string poID;
        string s1 = "Credit Pending";
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["ddd1"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        cmd.CommandText =
        "Select InvoiceNo from invoice where po_no = @dcaid and status='" + s1 + "'";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> subDCA = new List<CascadingDropDownNameValue>();
        // subDCA.Add(new CascadingDropDownNameValue("Select Sub DCA", "0"));
        foreach (DataRow sdcRow in objSDca.Tables[0].Rows)
        {
            string sDcaID = sdcRow["InvoiceNo"].ToString();
            string sDcaName = sdcRow["InvoiceNo"].ToString();
            subDCA.Add(new CascadingDropDownNameValue(sDcaName, sDcaID));
        }
        return subDCA.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] paytype(string knownCategoryValues, string category)
    {

        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> paytype = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        //payment.Add(new CascadingDropDownNameValue("Select", "0"));
        paytype.Add(new CascadingDropDownNameValue("Invoice Service", "1"));
        paytype.Add(new CascadingDropDownNameValue("Trading Supply", "2"));
        paytype.Add(new CascadingDropDownNameValue("Retention", "3"));
        paytype.Add(new CascadingDropDownNameValue("Advance", "4"));
        return paytype.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] Po(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd1"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);

        cmd.CommandText = "Select distinct po_no from invoice where cc_code='" + poID + "'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["po_no"].ToString();
            string CCName = ccRow["po_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] costcode1(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select cc_code from Cost_Center where status in ('Old','New')";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", ""));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] invoice(string knownCategoryValues, string category)
    {
        string s = "Debit Pending";
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select invoiceno from pending_invoice where status='" + s + "'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", ""));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["invoiceno"].ToString();
            string CCName = ccRow["invoiceno"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    //[WebMethod]
    //public string[] paymenttypesearch(string prefixText)
    //{
    //    SqlConnection con = new SqlConnection(strConnection);

    //    SqlCommand cmd = new SqlCommand(" select distinct dca_code from bankbook where dca_code like '" + prefixText + "%'", con);
    //    con.Open();
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    List<string> obj = new List<string>();
    //    while (dr.Read())
    //        obj.Add(dr["DCA_code"].ToString());
    //    dr.Close();
    //    con.Close();
    //    return (obj.ToArray());


    //}
    [WebMethod]
    public string[] paymenttypesearch(string prefixText)
    {
        SqlConnection con = new SqlConnection(strConnection);
        List<string> obj = new List<string>();

        if (prefixText != "d")
        {
            SqlCommand cmd = new SqlCommand("select distinct paymenttype from bankbook where paymenttype like '" + prefixText + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow ro in ds.Tables[0].Rows)
            {
                string paytype1 = ro["paymenttype"].ToString();
                obj.Add(paytype1);
            }
        }
        else
        {
            SqlCommand cmd = new SqlCommand("select distinct DCA_code from bankbook where dca_code like '" + prefixText + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow ro in ds.Tables[0].Rows)
            {
                string dcac = ro["DCA_code"].ToString();
                obj.Add(dcac);
            }
        }


        return (obj.ToArray());

    }

    [WebMethod]
    public CascadingDropDownNameValue[] WebCC(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select cc_code,cc_name from cost_center where status in ('Old','New') GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", ""));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CCCode = ccRow["cc_code"].ToString();
            //string CC = CCName + "(" + CCCode + ")";
            string CC = CCCode + "(" + CCName + ")";
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] venpo(string knownCategoryValues, string category, string contextKey)
    {
        string poID;

        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["vendor"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@poid", poID);

        cmd.CommandText = "Select distinct po_no from pending_invoice where (balance!='0' or retention_balance!='0' or hold_balance!='0')  and vendor_id=@poid and (cc_code='" + Session["cc_code"].ToString() + "' or cc_code='" + contextKey + "')";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["po_no"].ToString();
            string CCName = ccRow["po_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] vendor1(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select vendor_id from vendor";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_id"].ToString();
            string CCName = ccRow["vendor_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] Category(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select distinct category_name,category_code from itemcode_creation";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Category", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["category_code"].ToString();
            string CCName = ccRow["category_name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] MajorGroup(string knownCategoryValues, string category)
    {
        string poID;

        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["ddc"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@poid", poID);

        cmd.CommandText = "Select majorgroup_name,majorgroup_code from itemcode_creation where category_code=@poid";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select MajorGroup", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["majorgroup_code"].ToString();
            string CCName = ccRow["majorgroup_name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] Indent(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select *  from indents where Status='6' AND indent_no NOT in(SELECT indent_no from po_details)";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Indent", ""));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["indent_no"].ToString();
            string CCName = ccRow["indent_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] IndentCC(string knownCategoryValues, string category)
    {
        string poID;

        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["ind"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@poid", poID);

        cmd.CommandText = "Select cc_code from indents where indent_no=@poid";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", ""));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] Category1(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select distinct category_name,category_code from itemcode_creation";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Category", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["category_code"].ToString();
            string CCName = ccRow["category_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] Employeeid(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select employee_id from  employee_data where status='Deactivate'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["employee_id"].ToString();
            string CCName = ccRow["employee_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] MRRNo(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "StoreKeeper")
            cmd.CommandText = "Select mrr_no from [Mr_report]m join purchase_details p on m.po_no=p.po_no where m.status='4' and cc_code='" + Session["cc_code"].ToString() + "'";
        else if (Session["roles"].ToString() == "Central Store Keeper")
            cmd.CommandText = "Select mrr_no from [Mr_report]m join purchase_details p on m.po_no=p.po_no where m.status='4' and (cc_code='CC-33' or cc_code='CCC')";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select MRR No", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["mrr_no"].ToString();
            string CCName = ccRow["mrr_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();

    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetStock(string prefixText, int count)
    {
        List<string> names = new List<string>();
        char first = prefixText[0];
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();


        if (char.IsNumber(first))
        {
            cmd = new SqlCommand("SELECT i.item_code FROM item_codes ic LEFT OUTER JOIN (SELECT item_code FROM assetdep where cc_code='" + HttpContext.Current.Session["cccode"] + "' and convert(datetime,received_date)<='" + HttpContext.Current.Session["datefrom"] + "' AND convert(datetime,transfer_date)>='" + HttpContext.Current.Session["dateto"] + "' UNION SELECT item_code FROM [recieved items] where po_no IN (select m.po_no FROM mr_report m  join purchase_details p ON m.po_no=p.po_no where convert(datetime,m.recieved_date) BETWEEN '" + HttpContext.Current.Session["datefrom"] + "' AND '" + HttpContext.Current.Session["dateto"] + "' AND p.cc_code='" + HttpContext.Current.Session["cccode"] + "') UNION SELECT it.item_code FROM [transfer info] ti JOIN [items transfer] it ON ti.ref_no=it.ref_no WHERE convert(datetime,ti.recieved_date) BETWEEN '" + HttpContext.Current.Session["datefrom"] + "' AND '" + HttpContext.Current.Session["dateto"] + "' AND ti.recieved_cc='" + HttpContext.Current.Session["cccode"] + "')i ON SUBSTRING(i.item_code,1,8)=ic.item_code  where i.item_code LIKE '" + prefixText + "%'", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                    (reader["item_code"].ToString(),
                    reader["item_code"].ToString());
                    names.Add(item);
                }
                reader.Close();
            }
            con.Close();
        }
        else
        {
            cmd = new SqlCommand("SELECT ic.item_name,i.item_code FROM item_codes ic RIGHT OUTER JOIN (SELECT item_code FROM assetdep where cc_code='" + HttpContext.Current.Session["cccode"] + "' and convert(datetime,received_date)<='" + HttpContext.Current.Session["datefrom"] + "' AND convert(datetime,transfer_date)>='" + HttpContext.Current.Session["dateto"] + "' UNION SELECT item_code FROM [recieved items] where po_no IN (select m.po_no FROM mr_report m  join purchase_details p ON m.po_no=p.po_no where convert(datetime,m.recieved_date) BETWEEN '" + HttpContext.Current.Session["datefrom"] + "' AND '" + HttpContext.Current.Session["dateto"] + "' AND p.cc_code='" + HttpContext.Current.Session["cccode"] + "') UNION SELECT it.item_code FROM [transfer info] ti JOIN [items transfer] it ON ti.ref_no=it.ref_no WHERE convert(datetime,ti.recieved_date) BETWEEN '" + HttpContext.Current.Session["datefrom"] + "' AND '" + HttpContext.Current.Session["dateto"] + "' AND ti.recieved_cc='" + HttpContext.Current.Session["cccode"] + "')i ON SUBSTRING(i.item_code,1,8)=ic.item_code  where ic.item_name LIKE '" + prefixText + "%'", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    string ItemCode = reader["item_name"].ToString() + "|" + reader["item_code"].ToString();
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(ItemCode, ItemCode);
                    names.Add(item);
                }
                reader.Close();
            }
            con.Close();
        }
        return names.ToArray();
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();

        List<string> names = new List<string>();
        char first = prefixText[0];

        if (char.IsNumber(first))
        {
            if ((Session["roles"].ToString() == "StoreKeeper") || (Session["roles"].ToString() == "Accountant/StoreKeeper"))
            {
                cmd = new SqlCommand("SELECT i.item_code,i.units,(select quantity from master_data where item_code=i.item_code and cc_code='" + Session["cc_code"].ToString() + "') as[Quantity] from item_codes i where i.item_code like '" + prefixText + "%' and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A')", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_code"].ToString(),
                        "Available Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();
            }
            if (Session["roles"].ToString() == "Project Manager")
            {
                cmd = new SqlCommand("SELECT i.item_code,i.units from item_codes i where i.item_code like '" + prefixText + "%' and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A') ", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_code"].ToString(),
                         reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                try
                {
                    cmd = new SqlCommand("SELECT i.item_code,i.units,(select quantity from [master_data] where item_code=i.item_code and cc_code='CC-33') as[Quantity],(select quantity from [New Items] where item_code=i.item_code and cc_code='CC-33') as[Quantity1] from item_codes i where i.item_code like '" + prefixText + "%'and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A')", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    {
                        while (reader.Read())
                        {
                            string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                            (reader["item_code"].ToString(),
                            "Old Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString() + "," + "New Stock: " + reader["Quantity1"].ToString() + "\n" + reader["units"].ToString());

                            names.Add(item);
                        }
                        reader.Close();
                    }
                    con.Close();

                }
                catch (Exception ex)
                {
                    Utilities.CatchException(ex);
                }
            }

            else if (Session["roles"].ToString() == "PurchaseManager")
            {
                cmd = new SqlCommand("SELECT i.item_code,i.units,(select quantity from [New Items] where item_code=i.item_code and cc_code='CC-33') as[Quantity] from item_codes i where i.item_code like '" + prefixText + "%' and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A')", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_code"].ToString(),
                        "New Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();

            }
        }
        else
        {

            if ((Session["roles"].ToString() == "StoreKeeper") || (Session["roles"].ToString() == "Accountant/StoreKeeper"))
            {
                cmd = new SqlCommand("SELECT i.item_name,i.units,i.specification,(select quantity from master_data where item_code=i.item_code and cc_code='" + Session["cc_code"].ToString() + "') as[Quantity] from item_codes i where i.item_name like '" + prefixText + "%'and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A') ", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_name"].ToString() + "," + reader["specification"].ToString(),
                        "Available Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();
            }
            if (Session["roles"].ToString() == "Project Manager")
            {
                cmd = new SqlCommand("SELECT i.item_name,i.units,i.specification from item_codes i where i.item_name like '" + prefixText + "%'and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A') ", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                       (reader["item_name"].ToString() + "," + reader["specification"].ToString(),
                     reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                cmd = new SqlCommand("SELECT i.item_name,i.units,i.specification,(select quantity from [master_data] where item_code=i.item_code and cc_code='CC-33') as[Quantity],(select quantity from [New Items] where item_code=i.item_code and cc_code='CC-33') as[Quantity1] from item_codes i where i.item_name like '" + prefixText + "%'and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A') ", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_name"].ToString() + "," + reader["specification"].ToString(),
                        "Old Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString() + "," + "New Stock: " + reader["Quantity1"].ToString() + "\n" + reader["units"].ToString());

                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();

            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {
                cmd = new SqlCommand("SELECT i.item_name,i.units,i.specification,(select quantity from [New Items] where item_code=i.item_code and cc_code='CC-33') as[Quantity] from item_codes i where i.item_name like '" + prefixText + "%'and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A') ", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_name"].ToString() + "," + reader["specification"].ToString(),
                        "New Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();

            }
        }



        return names.ToArray();
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetItemnamelist(string prefixText, int count)
    {
        List<string> names = new List<string>();
        char first = prefixText[0];
        //if (char.IsNumber(first))
        //{
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();


        cmd = new SqlCommand("SELECT DISTINCT item_name from item_codes  where item_name like '%" + prefixText + "%' and status in ('4','5','5A','2A') ", con);
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        {
            while (reader.Read())
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                (reader["item_name"].ToString(),
                reader["item_name"].ToString());
                names.Add(item);
            }
            reader.Close();
        }
        con.Close();

        //}
        return names.ToArray();
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompletionList1(string prefixText, int count)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();

        List<string> names = new List<string>();


        char first = prefixText[0];

        if (char.IsNumber(first))
        {

            cmd = new SqlCommand("SELECT i.item_code,i.units from item_codes i where i.item_code like '" + prefixText + "%'  and i.status in ('4','5','5A','2A')", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                    (reader["item_code"].ToString(),
                     reader["units"].ToString());
                    names.Add(item);
                }
                reader.Close();
            }
            con.Close();
        }
        else
        {

            cmd = new SqlCommand("SELECT i.item_name,i.units,i.specification from item_codes i where i.item_name like '" + prefixText + "%' and i.status in ('4','5','5A','2A') ", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                    (reader["item_name"].ToString() + "," + reader["specification"].ToString(),
                     reader["units"].ToString());
                    names.Add(item);
                }
                reader.Close();
            }
            con.Close();
        }



        return names.ToArray();
    }


    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string values(string p)
    {
        Session["type"] = p;
        return p;
    }
    [WebMethod]
    public CascadingDropDownNameValue[] Employeepid(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select employee_id from  employee_data where status='4' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["employee_id"].ToString();
            string CCName = ccRow["employee_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] Employeecid(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select  distinct Employee_id from Employee_Data where  Employee_id  IN (SELECT Employee_id from employee_checklist) ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["employee_id"].ToString();
            string CCName = ccRow["employee_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]

    public CascadingDropDownNameValue[] Employeepsid(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        //if (Session["roles"].ToString() == "HRGM")
        //    cmd.CommandText = "Select employee_id from  EmployeeSalary_structure where status='3' ";
        //else if (Session["roles"].ToString() == "SuperAdmin")
        cmd.CommandText = "Select employee_id from  EmployeeSalary_structure where status='3' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["employee_id"].ToString();
            string CCName = ccRow["employee_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

      [WebMethod]
    public CascadingDropDownNameValue[] Employeepmsid(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        //if (Session["roles"].ToString() == "HRGM")
        //    cmd.CommandText = "Select employee_id from  EmployeeSalary_structure where status='3' ";
        //else if (Session["roles"].ToString() == "SuperAdmin")
        cmd.CommandText = "Select employee_id from  employeemonthlysalary where status='3' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["employee_id"].ToString();
            string CCName = ccRow["employee_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
      [WebMethod]
    public CascadingDropDownNameValue[] Employeesid(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "HRGM")
            cmd.CommandText = "Select employee_id from  EmployeeSalary_structure where status='3' ";
        else if (Session["roles"].ToString() == "SuperAdmin")
            cmd.CommandText = "Select employee_id from  EmployeeSalary_structure where status='4' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["employee_id"].ToString();
            string CCName = ccRow["employee_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

   

    [WebMethod]
    public CascadingDropDownNameValue[] Deactiveemployees(string knownCategoryValues, string category)
    {

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select (first_name+' '+last_name) as [Name],employee_id from employee_data where status='5' and Employee_id is not null";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["employee_id"].ToString();
            string CCName = ccRow["name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] Activeemployees(string knownCategoryValues, string category)
    {


        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select (first_name+' '+last_name) as [Name],employee_id from employee_data where status='Activate' and Employee_id is not null";


        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();


        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["employee_id"].ToString();
            string CCName = ccRow["name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] StoreCC(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "'";
        else
            cmd.CommandText = "Select cc_code from cost_center where status in ('Old','New')";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] SUBDetail(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select * from subdca";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["subdca_code"].ToString();
            string countryName = dRow["subdca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] CreditPo(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd1"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        //HttpContext.Current.Cache.Insert("dat", contextKey, null, DateTime.MaxValue, TimeSpan.FromSeconds(10));
        cmd.CommandText = "Select distinct po_no from invoice where cc_code='" + poID + "' and subclient_id='" + HttpContext.Current.Cache["SubClient"].ToString() + "' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["po_no"].ToString();
            string CCName = ccRow["po_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] StoreCC1(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "'";
        else
            cmd.CommandText = "Select cc_code from cost_center where status in ('Old','New')";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "Select Cost Center"));
        cccodes.Add(new CascadingDropDownNameValue("Select All", "Select All"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] Budgetpcc(string knownCategoryValues, string category, string contextKey)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        string s = knownCategoryValues.Remove(0, 10);
        string r = s.Remove(s.Length - 1);
        cmd.CommandType = System.Data.CommandType.Text;

        if (Session["roles"].ToString() == "Sr.Accountant")
            cmd.CommandText = "select distinct c.cc_code,c.cc_name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='" + r + "' and c.status in ('Old','New')";
        else if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "select distinct c.cc_code,c.cc_name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4' and c.CC_type='" + r + "' and c.status in ('Old','New')";
        else if (Session["roles"].ToString() == "HoAdmin")
            cmd.CommandText = "select distinct c.cc_code,c.cc_name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4A' and c.CC_type='" + r + "' and c.status in ('Old','New')";
        else if (Session["roles"].ToString() == "SuperAdmin")
            cmd.CommandText = "select distinct c.cc_code,c.cc_name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='5' and c.CC_type='" + r + "' and c.status in ('Old','New')";
        else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            cmd.CommandText = "select distinct c.cc_code,c.cc_name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='5A' and c.CC_type='" + r + "' and c.status in ('Old','New')";


        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();

        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }

        return cccodes.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] budgetcheck(string knownCategoryValues, string category, string contextKey)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;

        if (contextKey == "Performing")
            cmd.CommandText = "select cc_code,cc_name from cost_center where cc_code  not in (select cc_code from budget_cc) and cc_type='Performing' and  status in ('Old','New')";
        else if (contextKey == "Non-Performing")
            cmd.CommandText = "select cc_code,cc_name from cost_center where cc_type in ('Non-Performing') and status in ('Old','New')";
        else
            cmd.CommandText = "select cc_code,cc_name from cost_center where cc_type in ('Capital') and  status in ('Old','New')";


        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();

        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }

        return cccodes.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] Loadyear(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        if (Session["roles"].ToString() == "Sr.Accountant")
            cmd.CommandText = "Select year from budget_cc where status='3' and cc_code='" + poID + "'";
        else if (Session["roles"].ToString() == "HoAdmin")
            cmd.CommandText = "Select year from budget_cc where status='4' and cc_code='" + poID + "'";
        else if (Session["roles"].ToString() == "SuperAdmin")
            cmd.CommandText = "Select year from budget_cc where status='5' and cc_code='" + poID + "'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["year"].ToString();
            string CCName = ccRow["year"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] checkyear(string knownCategoryValues, string category)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select distinct year from budget_cc where status='6'";
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["year"].ToString();
            string CCName = ccRow["year"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] AmendBudgetCC(string knownCategoryValues, string category, string contextKey)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        string s = knownCategoryValues.Remove(0, 10);
        string r = s.Remove(s.Length - 1);
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandType = System.Data.CommandType.Text;
        if ((Session["roles"].ToString() == "Accountant") || (Session["roles"].ToString() == "StoreKeeper"))
            cmd.CommandText = "select distinct u.cc_code,c.cc_name from CC_User u join Cost_Center c on u.cc_code=c.cc_code join budget_cc b on c.cc_code=b.cc_code where b.status='6' and c.cc_type='" + r + "' and c.cc_code='" + Session["cc_code"].ToString() + "' and c.status in ('Old','New')";
        else if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "select distinct u.cc_code,c.cc_name from CC_User u join Cost_Center c on u.cc_code=c.cc_code join budget_cc b on c.cc_code=b.cc_code where b.status='6'  and u.User_Name='" + Session["user"].ToString() + "' and  c.status in ('Old','New')";
        else
            cmd.CommandText = "select distinct c.cc_code,c.cc_name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='" + r + "' and c.status in ('Old','New')";


        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();

        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }

        return cccodes.ToArray();

    }
    [WebMethod]
    public CascadingDropDownNameValue[] AmendLoadyear(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        cmd.CommandText = "Select year from budget_cc where status='6' and cc_code='" + poID + "'";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["year"].ToString();
            string CCName = ccRow["year"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] VerifyAmendBudgetCC(string knownCategoryValues, string category, string contextKey)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        string s = knownCategoryValues.Remove(0, 10);
        string r = s.Remove(s.Length - 1);
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "HoAdmin")
            cmd.CommandText = "select distinct c.cc_code,c.cc_name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='2' and c.CC_type='" + r + "' and c.status in ('Old','New')";
        else

            cmd.CommandText = "select distinct c.cc_code,c.cc_name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='" + r + "' and c.status in ('Old','New')";


        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();

        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }

        return cccodes.ToArray();

    }
    [WebMethod]
    public CascadingDropDownNameValue[] VerifyAmendLoadyear(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        cmd.CommandText = "Select distinct year from Amendbudget_DCA where cc_code='" + poID + "'";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["year"].ToString();
            string CCName = ccRow["year"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }



    [WebMethod]
    public CascadingDropDownNameValue[] Agencycode(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select Agencycode from Agency";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["Agencycode"].ToString();
            string CCName = ccRow["Agencycode"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] loanno(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["dca"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", dcaID);
        cmd.CommandText = "Select loanno from termloan where agencycode='" + dcaID + "' and status!='Rejected'";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> subDCA = new List<CascadingDropDownNameValue>();
        subDCA.Add(new CascadingDropDownNameValue("Select All", "0"));
        foreach (DataRow sdcRow in objSDca.Tables[0].Rows)
        {
            string sDcaID = sdcRow["loanno"].ToString();
            string sDcaName = sdcRow["loanno"].ToString();
            subDCA.Add(new CascadingDropDownNameValue(sDcaName, sDcaID));
        }
        return subDCA.ToArray();

    }
    [WebMethod]
    public CascadingDropDownNameValue[] venid(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select vendor_id,vendor_name from vendor where vendor_type='Service Provider' and status='2' order by vendor_name ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        // cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_id"].ToString();
            string CCName = ccRow["vendor_name"].ToString();
            string CCCode = ccRow["vendor_id"].ToString();
            //string CC = CCName + "(" + CCCode + ")";
            string CC = CCName + " ,  " + (CCCode);
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] performingcc(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = System.Data.CommandType.Text;


        cmd.CommandText = "select distinct cc_code from Cost_Center where CC_type='Performing' and status in ('Old','New')";


        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();

        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }

        return cccodes.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] spvendor(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select vendor_id from sppo where status='3'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        // cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_id"].ToString();
            string CCName = ccRow["vendor_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] sppo(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Sr.Accountant")
            cmd.CommandText = "Select pono from sppo where status='3' ";
        else if (Session["roles"].ToString() == "HoAdmin")
            cmd.CommandText = "Select pono from Amend_sppo where status='1' ";
        else if (Session["roles"].ToString() == "SuperAdmin")
            cmd.CommandText = "Select pono from Amend_sppo where status='2' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        // cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["pono"].ToString();
            string CCName = ccRow["pono"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] closesppo(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Sr.Accountant")
            cmd.CommandText = "Select distinct sp.vendor_id,v.vendor_name from sppo sp join vendor v on sp.vendor_id=v.vendor_id where sp.status='3' and balance!='0' order by v.vendor_name Asc ";
        else if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select distinct sp.vendor_id,v.vendor_name from sppo sp join vendor v on sp.vendor_id=v.vendor_id where sp.status='Closed' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') order by v.vendor_name Asc";
        else if (Session["roles"].ToString() == "HoAdmin")
            cmd.CommandText = "Select distinct sp.vendor_id,v.vendor_name from sppo sp join vendor v on sp.vendor_id=v.vendor_id where sp.status='Closed1' order by v.vendor_name Asc ";
        else if (Session["roles"].ToString() == "SuperAdmin")
            cmd.CommandText = "Select distinct sp.vendor_id,v.vendor_name from sppo sp join vendor v on sp.vendor_id=v.vendor_id where sp.status='Closed2' order by v.vendor_name Asc ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        // cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_id"].ToString();
            string CCName = ccRow["vendor_name"].ToString();
            string CCCode = ccRow["vendor_id"].ToString();
            string CC = CCName + " ,  " + CCCode;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] ven_id(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select distinct s.vendor_id,v.vendor_name from sppo s join vendor v on s.vendor_id=v.vendor_id ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_id"].ToString();
            string CCName = ccRow["vendor_name"].ToString();
            string CCCode = ccRow["vendor_id"].ToString();
            //string CC = CCName + "(" + CCCode + ")";
            string CC = CCName + " ,  " + CCCode;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] indent2A(string knownCategoryValues, string category)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select indent_no from indents where Status='2A' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        // cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["indent_no"].ToString();
            string CCName = ccRow["indent_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] TransferOutSearch(string prefixText, int count)
    {



        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();

        List<string> names = new List<string>();
        char first = prefixText[0];

        if (char.IsNumber(first))
        {
            if ((Session["roles"].ToString() == "StoreKeeper") || (Session["roles"].ToString() == "Accountant/StoreKeeper" || (Session["roles"].ToString() == "Central Store Keeper")))
            {
                if (Session["type"].ToString() == "1")
                    //cmd = new SqlCommand("select i.* from(SELECT m.item_code,i.units,m.Quantity from master_data m join item_codes i on substring(m.item_code,1,8)=i.item_code where m.item_code like '" + prefixText + "%' and substring(m.item_code,1,1)='" + Session["type"].ToString() + "'  and m.cc_code='" + Session["cc_code"].ToString() + "' and m.status='Active' and i.status in ('4','5','5A','2A') )i where i.item_code not in(select item_code from [Lost/Damaged_items] ld join [lost/Damaged Report] lr on ld.ref_no=lr.ref_no where lr.status not in ('4') union select item_code from [Items Transfer] it join [Transfer Info] ti on it.ref_no=ti.ref_no where (type='1' and status not in ('4')) or (type='2' and status not in ('6')) or (type='2' and indent_no is null and status not in ('5')))",con);
                    //select i.* from(SELECT m.item_code,i.units,m.Quantity from master_data m join item_codes i on substring(m.item_code,1,8)=i.item_code where m.item_code like '1%' and substring(m.item_code,1,1)='2'  and m.cc_code='CC-91' and m.status='Active' and i.status in ('4','5','5A','2A') )i where i.item_code not in(select item_code from [Lost/Damaged_items] ld join [lost/Damaged Report] lr on ld.ref_no=lr.ref_no where lr.status not in ('4') and ld.cc_code='CC-91' union select item_code from [Items Transfer] it join [Transfer Info] ti on it.ref_no=ti.ref_no where  Recieved_cc='CC-91' and((type='1' and status not in ('4')) or (type='2' and status not in ('6')) or (type='2' and indent_no is null and status not in ('5'))))[MODIFIED CODE BY KISHORE]
                    cmd = new SqlCommand("select i.* from(SELECT m.item_code,i.units,m.Quantity from master_data m join item_codes i on substring(m.item_code,1,8)=i.item_code where m.item_code like '" + prefixText + "%' and substring(m.item_code,1,1)='" + Session["type"].ToString() + "'  and m.cc_code='" + Session["cc_code"].ToString() + "' and m.status='Active' and i.status in ('4','5','5A','2A') )i where i.item_code not in(select item_code from [Lost/Damaged_items] ld join [lost/Damaged Report] lr on ld.ref_no=lr.ref_no where lr.status not in ('4') union select item_code from [Items Transfer] it join [Transfer Info] ti on it.ref_no=ti.ref_no where ((type='1' and status not in ('4')) or (type='2' and status not in ('6') and indent_no is not null) or (type='2' and indent_no is null and status not in ('5'))))", con);
                else
                    //cmd = new SqlCommand("select i.* from (SELECT i.item_code,i.units,m.quantity from item_codes i join master_data m on i.Item_code=m.Item_code where i.item_code like '" + prefixText + "%' and substring(i.item_code,1,1)='" + Session["type"].ToString() + "' and m.CC_code='" + Session["cc_code"].ToString() + "' and i.status in ('4','5','5A','2A') and quantity >0)i where i.item_code not in (select item_code from [Lost/Damaged_items] ld join [lost/Damaged Report] lr on ld.ref_no=lr.ref_no where lr.status not in ('4')  union select item_code from [Items Transfer] it join [Transfer Info] ti on it.ref_no=ti.ref_no where (type='1' and status not in ('4')) or (type='2' and status not in ('6')) or (type='2' and indent_no is null and status not in ('5')))", con);
                    cmd = new SqlCommand("select i.* from (SELECT i.item_code,i.units,m.quantity from item_codes i join master_data m on i.Item_code=m.Item_code where i.item_code like '" + prefixText + "%' and substring(i.item_code,1,1)='" + Session["type"].ToString() + "' and m.CC_code='" + Session["cc_code"].ToString() + "' and i.status in ('4','5','5A','2A') and quantity >0)i where i.item_code not in (select item_code from [Lost/Damaged_items] ld join [lost/Damaged Report] lr on ld.ref_no=lr.ref_no where lr.status not in ('4')  union select item_code from [Items Transfer] it join [Transfer Info] ti on it.ref_no=ti.ref_no where ((type='1' and status not in ('4')) or (type='2' and status not in ('6') and indent_no is not null) or (type='2' and indent_no is null and status not in ('5'))))", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_code"].ToString(),
                        "Available Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();
            }

        }


        else
        {

            if ((Session["roles"].ToString() == "StoreKeeper") || (Session["roles"].ToString() == "Accountant/StoreKeeper"))
            {
                //if (Session["type"].ToString() != "1")
                //cmd = new SqlCommand("SELECT i.item_name,i.units,i.specification,(select quantity from master_data where item_code=i.item_code and cc_code='" + Session["cc_code"].ToString() + "') as[Quantity] from item_codes i where i.item_name like '" + prefixText + "%'and substring(item_code,1,1)='" + Session["type"].ToString() + "' ", con);
                cmd = new SqlCommand("SELECT distinct i.item_name ,i.units,i.specification,m.quantity   FROM master_data m JOIN item_codes i ON i.item_code=substring(m.item_code,1,8) WHERE i.item_name like '" + prefixText + "%' and substring(i.item_code,1,1)='" + Session["type"].ToString() + "' AND m.cc_code='" + Session["cc_code"].ToString() + "' and i.status in ('4','5','5A','2A') ", con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_name"].ToString() + "," + reader["specification"].ToString(),
                        "Available Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();
            }



        }



        return names.ToArray();
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] codename(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select cu.cc_code,cc.cc_name from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New') GROUP by cu.cc_code,cc.cc_name ORDER BY CASE WHEN cu.cc_code='CCC' THEN cu.cc_code end ASC ,CASE WHEN cu.cc_code!='CCC' THEN CONVERT(INT,REPLACE(cu.CC_Code,'CC-','')) END ASC";
        else
            cmd.CommandText = "Select cc_code,cc_name from cost_center where status in ('Old','New') GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        cccodes.Add(new CascadingDropDownNameValue("Select All", "Select All"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            //string CCCode = ccRow["cc_code"].ToString();
            //string CC = CCName + "(" + CCCode + ")";
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] CCCodenames(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select cu.cc_code,cc.cc_name from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New') order by cu.cc_code";
        else
            cmd.CommandText = "Select cc_code,cc_name from cost_center where status in ('Old','New') order by cc_code";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
      //  cccodes.Add(new CascadingDropDownNameValue("Select All", "Select All"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            //string CCCode = ccRow["cc_code"].ToString();
            //string CC = CCName + "(" + CCCode + ")";
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] StoreCC2(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select cu.cc_code,cc.cc_name from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New') GROUP by cu.cc_code,cc.cc_name ORDER BY CASE WHEN cu.cc_code='CCC' THEN cu.cc_code end ASC ,CASE WHEN cu.cc_code!='CCC' THEN CONVERT(INT,REPLACE(cu.CC_Code,'CC-','')) END ASC";
        else
            cmd.CommandText = "Select cc_code,cc_name from cost_center c where status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.CC_Code,'CC-','')) END ASC";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "Select Cost Center"));
        cccodes.Add(new CascadingDropDownNameValue("Select All", "Select All"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();

            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            ////cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }


   
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] newcostcodenew(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = " select distinct i.cc_code,i.cc_name from (Select cu.cc_code,cc.cc_name from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New'))i join  SPPO s on i.cc_code=s.cc_code where s.status='Closed'";
        else if (Session["roles"].ToString() == "HoAdmin")
            cmd.CommandText = "Select distinct c.cc_code,c.cc_name from cost_center c join SPPO s on c.cc_code=s.cc_code where c.status in ('Old','New') and s.status='Closed1' and c.cc_type='Performing' union all Select distinct c.cc_code,c.cc_name from cost_center c join SPPO s on c.cc_code=s.cc_code where c.status in ('Old','New') and s.status='Closed' and c.cc_type='Non-Performing'";
        else if (Session["roles"].ToString() == "SuperAdmin")
            cmd.CommandText = "Select distinct c.cc_code,c.cc_name from cost_center c join SPPO s on c.cc_code=s.cc_code where c.status in ('Old','New') and s.status='Closed2'";
        else if (Session["roles"].ToString() == "Sr.Accountant")
            cmd.CommandText = "Select  cc_code,cc_name from cost_center c where status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.CC_Code,'CC-','')) END ASC ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
      [WebMethod]
      public CascadingDropDownNameValue[] newcostcode(string knownCategoryValues, string category)
      {
          //Create sql connection and sql command
          SqlConnection con = new SqlConnection(strConnection);
          SqlCommand cmd = new SqlCommand();
          cmd.Connection = con;
          cmd.CommandType = System.Data.CommandType.Text;
          cmd.CommandText = "Select c.cc_code,c.cc_name from cost_center c where status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.CC_Code,'CC-','')) END ASC";

          //Create dataadapter and fill the dataset
          SqlDataAdapter dAdapter = new SqlDataAdapter();
          dAdapter.SelectCommand = cmd;
          con.Open();
          DataSet objCC = new DataSet();
          dAdapter.Fill(objCC);
          con.Close();

          //create list and add items in it 
          //by looping through dataset table
          List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
          //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

          foreach (DataRow ccRow in objCC.Tables[0].Rows)
          {
              string CCID = ccRow["cc_code"].ToString();
              string CCName = ccRow["cc_name"].ToString();
              string CC = CCID + " , " + CCName;
              cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
              //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
          }
          return cccodes.ToArray();


      }
    [WebMethod]
    public string Paymenttype(string paymenttype)
    {
        HttpContext.Current.Cache.Insert("dat", paymenttype, null, DateTime.MaxValue, TimeSpan.FromMinutes(20));
        return paymenttype;
    }

   

    [WebMethod]
    public CascadingDropDownNameValue[] clientid(string knownCategoryValues, string category)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = System.Data.CommandType.Text;
        if (HttpContext.Current.Cache["dat"].ToString() == "Invoice Service")
            cmd.CommandText = "Select client_id from client where  SUBSTRING(client_id,1,2)='SC'";
        else if (HttpContext.Current.Cache["dat"].ToString() == "Trading Supply")
            cmd.CommandText = "Select client_id from client where  SUBSTRING(client_id,1,2)='TC'";
        else
            //cmd.CommandText = "Select client_id from client";
            cmd.CommandText = "Select ci.client_id from client ci join po p on ci.client_id=p.client_id where p.cc_code='" + HttpContext.Current.Cache["dat1"].ToString() + "'";


        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        // cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["client_id"].ToString();
            string CCName = ccRow["client_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] client_id(string knownCategoryValues, string category)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

      
            cmd.CommandText = "Select client_id from client";


        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        // cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["client_id"].ToString();
            string CCName = ccRow["client_id"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] subclientid(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["client"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", dcaID);
        cmd.CommandText = "select subclient_id from subclient where client_id='" + dcaID + "'";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> subDCA = new List<CascadingDropDownNameValue>();

        foreach (DataRow sdcRow in objSDca.Tables[0].Rows)
        {
            string CCID = sdcRow["subclient_id"].ToString();
            string CCName = sdcRow["subclient_id"].ToString();
            subDCA.Add(new CascadingDropDownNameValue(CCID, CCName));
        }
        return subDCA.ToArray();

    }
    [WebMethod]
    public CascadingDropDownNameValue[] CreditCC(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["subclient"].ToString();

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", dcaID);
        HttpContext.Current.Cache.Insert("SubClient", dcaID, null, DateTime.MaxValue, TimeSpan.FromMinutes(20));

        cmd.CommandText = "Select distinct i.cc_code,cc_name from invoice i join cost_center c on i.cc_code=c.cc_code where subclient_id='" + dcaID.ToString() + "' and c.status in ('Old','New') order by i.cc_code asc";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", ""));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CCCode = ccRow["cc_code"].ToString();
            //string CC = CCName + "(" + CCCode + ")";
            string CC = CCCode + "," + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] clientP0(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd1"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        //HttpContext.Current.Cache.Insert("dat", contextKey, null, DateTime.MaxValue, TimeSpan.FromSeconds(10));
        cmd.CommandText = "Select distinct po_no from invoice where   subclient_id='" + poID + "' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["po_no"].ToString();
            string CCName = ccRow["po_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] newcccode(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["dd1"].ToString();

        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", dcaID);
        HttpContext.Current.Cache.Insert("SubClient", dcaID, null, DateTime.MaxValue, TimeSpan.FromMinutes(20));

        cmd.CommandText = "Select distinct i.cc_code,cc_name from invoice i join cost_center c on i.cc_code=c.cc_code where subclient_id='" + dcaID.ToString() + "' and i.status!='cancel' order by i.cc_code asc";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", ""));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CCCode = ccRow["cc_code"].ToString();
            //string CC = CCName + "(" + CCCode + ")";
            string CC = CCCode + "," + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();


    }



    [WebMethod]
    public CascadingDropDownNameValue[] cccodepo(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd2"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        //HttpContext.Current.Cache.Insert("dat", contextKey, null, DateTime.MaxValue, TimeSpan.FromSeconds(10));
        cmd.CommandText = "Select distinct po_no from invoice where   cc_code='" + poID + "' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["po_no"].ToString();
            string CCName = ccRow["po_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] DepCC(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select cu.cc_code,cc.cc_name from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New') GROUP by cu.cc_code,cc.cc_name ORDER BY CASE WHEN cu.cc_code='CCC' THEN cu.cc_code end ASC ,CASE WHEN cu.cc_code!='CCC' THEN CONVERT(INT,REPLACE(cu.cc_code,'CC-','')) END ASC";
        else
            cmd.CommandText = "Select cc_code,cc_name from cost_center where cc_type='Performing' and status in ('Old','New') GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] ContractPo(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;

        cmd.CommandText = "Select  po_no from po";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["po_no"].ToString();
            string CCName = ccRow["po_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] CCOverHead(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select cc_code from Cost_Center where cc_code not in (select cc_code from CCOverheadandDepreciation) and cc_type='Performing' and status in ('Old','New')";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] vendorid(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select vendor_id,vendor_name from vendor where vendor_type='Supplier' and status='2' order by vendor_name asc";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_id"].ToString();
            string CCName = ccRow["vendor_name"].ToString();
            string CCCode = ccRow["vendor_id"].ToString();
            //string CC = CCName + "(" + CCCode + ")";
            string CC = CCName + " ,  " + CCCode;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));

        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] StoreCCnew(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select cc_code,cc_name from cc_user where user_name='" + Session["user"].ToString() + "'";
        else
            cmd.CommandText = "Select cc_code,cc_name from cost_center where status in ('Old','New')";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));


        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] newcash(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select * from dca";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        //dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["dca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] Polist(string knownCategoryValues, string category)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select  po_no from po";
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select PO", "1"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["po_no"].ToString();
            string CCName = ccRow["po_no"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] SPDCA(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd1"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        //HttpContext.Current.Cache.Insert("dat", contextKey, null, DateTime.MaxValue, TimeSpan.FromSeconds(10));
        cmd.CommandText = "Select distinct dca_code from dcatype where status='Active' and cc_type in (Select cc_subtype FROM cost_center where cc_code=@dcaid)";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["dca_code"].ToString();
            string CCName = ccRow["dca_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] SPDCA1(string knownCategoryValues, string category)
    {
        string poID;
        //Create sql connection and sql command
        StringDictionary po =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        poID = po["dd1"].ToString();
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", poID);
        //HttpContext.Current.Cache.Insert("dat", contextKey, null, DateTime.MaxValue, TimeSpan.FromSeconds(10));
        cmd.CommandText = "Select distinct dca_code from dcatype where status='Active' and cc_type in (Select cc_subtype FROM cost_center where cc_code=@dcaid) AND dca_code IN(SELECT dca_code from dca_paymenttype where paymenttype='Vendor through Payment' AND dca_paymenttype.status='Active')";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["dca_code"].ToString();
            string CCName = ccRow["dca_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] CCAccrued(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select cc_code from Cost_Center where cc_code not in (select cc_code from [CC_AccruedValues]) and cc_type='Performing'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_code"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();

    }

    [WebMethod]
    public CascadingDropDownNameValue[] Depvalues(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select return_percent from semiasset_dep";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        cccodes.Add(new CascadingDropDownNameValue(objCC.Tables[0].Rows[0][0].ToString(), objCC.Tables[0].Rows[0][0].ToString()));
        for (int i = Convert.ToInt32(objCC.Tables[0].Rows[0][0]); i < 100; i = i + 10)
        {
            if (i == 90)
            {
                cccodes.Add(new CascadingDropDownNameValue("Full Value", "Full Value"));

            }
            else
            {
                cccodes.Add(new CascadingDropDownNameValue((i + 10).ToString(), (i + 10).ToString()));

            }

        }
        return cccodes.ToArray();


    }

    [WebMethod]
    public CascadingDropDownNameValue[] Depissuevalues(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select issue_percent from semiasset_dep";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        cccodes.Add(new CascadingDropDownNameValue(objCC.Tables[0].Rows[0][0].ToString(), objCC.Tables[0].Rows[0][0].ToString()));
        for (int i = Convert.ToInt32(objCC.Tables[0].Rows[0][0]); i > 0; i = i - 10)
        {
            if (i == 10)
            {
                cccodes.Add(new CascadingDropDownNameValue("Full Value", "Full Value"));

            }
            else
            {
                cccodes.Add(new CascadingDropDownNameValue((i - 10).ToString(), (i - 10).ToString()));

            }

        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] checkyear1(string knownCategoryValues, string category)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "HoAdmin")
        {
            cmd.CommandText = "Select distinct year from budget_cc where status='1'";
        }
         else if (Session["roles"].ToString() == "SuperAdmin")
        {
            cmd.CommandText = "select distinct year from budget_cc where status='2'";
        }
        //Code fix start - INC-APR-001-2016
        else
        {
            cmd.CommandText = "select distinct year from budget_cc where status='2A'";
        }
        //Code fix end - INC-APR-001-2016
       
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["year"].ToString();
            string CCName = ccRow["year"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] newbank(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "HoAdmin")
        {
            cmd.CommandText = "select bank_name from bank_branch where Status='1'";
        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            cmd.CommandText = "select bank_name from bank_branch where Status in('2')";
        }
        else
        {
            cmd.CommandText = "select bank_name from bank_branch where Status in('2A','3')";
        }
        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Vendor", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["bank_name"].ToString();
            string CCName = ccRow["bank_name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] Allbanks(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select bank_name from bank_branch where Status='3'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> banknames = new List<CascadingDropDownNameValue>();
        banknames.Add(new CascadingDropDownNameValue("Select Bank", "0"));
        banknames.Add(new CascadingDropDownNameValue("Select All", "1"));
        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string BankID = ccRow["bank_name"].ToString();
            string BankName = ccRow["bank_name"].ToString();
            banknames.Add(new CascadingDropDownNameValue(BankName, BankID));
        }
        return banknames.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] ccTo(string knownCategoryValues, string category)
    {
        string ccfrom;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        ccfrom = dcaa["cc"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@ccfrom", ccfrom);
        cmd.CommandText = "SELECT cc_code FROM cost_center EXCEPT SELECT cc_code FROM cost_center where cc_code=@ccfrom";


        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> ccTos = new List<CascadingDropDownNameValue>();
        ccTos.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        foreach (DataRow sdcRow in objSDca.Tables[0].Rows)
        {
            string ccTosid = sdcRow["cc_code"].ToString();
            string ccTosName = sdcRow["cc_code"].ToString();
            ccTos.Add(new CascadingDropDownNameValue(ccTosName, ccTosid));
        }
        return ccTos.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] NatureGroup(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select NatureGroupId,NatureGroupName from natureofgroup ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["NatureGroupId"].ToString();
            string CCName = ccRow["NatureGroupName"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();

    }
    [WebMethod]
    public CascadingDropDownNameValue[] Groups(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select group_id,Group_Name from MasterGroups where status='3' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["group_id"].ToString();
            string CCName = ccRow["Group_Name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();

    }

    [WebMethod]
    public CascadingDropDownNameValue[] ChildGroups(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select id,Name from Sub_Group where status='3' ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["id"].ToString();
            string CCName = ccRow["Name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();

    }

    [WebMethod]
    public CascadingDropDownNameValue[] vendortype(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select vendor_type from vendor_type";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_type"].ToString();
            string CCName = ccRow["vendor_type"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCID, CCName));
        }
        return cccodes.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] vendornames(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["vendor_type"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@vtype", dcaID);
        cmd.CommandText = "select vendor_id,vendor_name from vendor where status='2' and vendor_type = @vtype";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        // subDCA.Add(new CascadingDropDownNameValue("Select Sub DCA", "0"));
        foreach (DataRow ccRow in objSDca.Tables[0].Rows)
        {
            string CCID = ccRow["vendor_id"].ToString();
            string CCName = ccRow["vendor_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] clientnames(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select client_id,client_name from client where status='2' order by client_id";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["client_id"].ToString();
            string CCName = ccRow["client_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] itcodename(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select it_code,it_name from it";
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();
        List<CascadingDropDownNameValue> itname = new List<CascadingDropDownNameValue>();
        //itname.Add(new CascadingDropDownNameValue("Select it_code", "Select it_name"));
        //itname.Add(new CascadingDropDownNameValue("Select All", "Select All"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string IT_CODE = ccRow["it_code"].ToString();
            string ITCODE = ccRow["it_code"].ToString();
            string ITNAME = ccRow["it_name"].ToString();

            string IT = ITCODE + " (" + ITNAME + ")";
            itname.Add(new CascadingDropDownNameValue(IT, IT_CODE));

        }
        return itname.ToArray();

    }
    [WebMethod]
    public CascadingDropDownNameValue[] AllGroups(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select CONVERT(varchar(10), id)as id,Name from Sub_Group where status='3'union all select Group_id as id,group_name as Name from mastergroups where status='3'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["id"].ToString();
            string CCName = ccRow["Name"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();

    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] stockrecon(string knownCategoryValues, string category)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        if (Session["roles"].ToString() == "Project Manager")
            cmd.CommandText = "Select cu.cc_code,cc.cc_name from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New') ORDER BY CASE WHEN cu.cc_code='CCC' THEN cu.cc_code end ASC ,CASE WHEN cu.cc_code!='CCC' THEN CONVERT(INT,REPLACE(cu.cc_code,'CC-','')) END ASC  ";
        else if (Session["roles"].ToString() == "StoreKeeper")
            cmd.CommandText = "Select top 1 cu.cc_code,cc.cc_name from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where cc.cc_code='" + Session["cc_code"].ToString() + "' and cc.status in ('Old','New')   ";
        else
            cmd.CommandText = "Select cc_code,cc_name from cost_center where status in ('Old','New') ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC  ";
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        if (Session["roles"].ToString() != "Project Manager" && Session["roles"].ToString() != "StoreKeeper")
            cccodes.Add(new CascadingDropDownNameValue("Select All", "Select All"));
        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return cccodes.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] ISdcnew(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select * from dca";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objDs = new DataSet();
        dAdapter.Fill(objDs);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> dcacodes = new List<CascadingDropDownNameValue>();
        dcacodes.Add(new CascadingDropDownNameValue("Select DCA", "0"));

        foreach (DataRow dRow in objDs.Tables[0].Rows)
        {
            string countryID = dRow["dca_code"].ToString();
            string countryName = dRow["mapdca_code"].ToString();
            dcacodes.Add(new CascadingDropDownNameValue(countryName, countryID));
        }
        return dcacodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] paymentrefund(string knownCategoryValues, string category)
    {

        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> payment = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        //payment.Add(new CascadingDropDownNameValue("Select", "0"));
        //payment.Add(new CascadingDropDownNameValue("Cheque", "Cheque"));
        payment.Add(new CascadingDropDownNameValue("Cash", "Cash"));
        payment.Add(new CascadingDropDownNameValue("RTGS/E-transfer", "RTGS/E-transfer"));
        payment.Add(new CascadingDropDownNameValue("DD", "DD"));


        return payment.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] performingccnew(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        //cmd.CommandText = "SELECT CC_Code FROM Cost_Center where CC_type='Performing' and status in ('Old','New') ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC  ";
        cmd.CommandText = "Select cc_code,cc_name from cost_center where CC_type='Performing' and status in ('Old','New') GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC  ";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();
        //create list and add items in it
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select All", "1"));
        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //string CCName = ccRow["cc_code"].ToString();
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();
    }

    
    [WebMethod]
    public CascadingDropDownNameValue[] GetGSTNos(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select GST_No from gstmaster where status='3'";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["GST_No"].ToString();
            string CCName = ccRow["GST_No"].ToString();
            cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();

    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] paymentFD(string knownCategoryValues, string category)
    {

        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary Yr =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        //dcaID = dcaa["dca"].ToString();
        //ddlyear.Items.Clear();
        //ddlyear.Items.Add("Select Year");
        List<CascadingDropDownNameValue> payment = new List<CascadingDropDownNameValue>();
        //year.Add(new CascadingDropDownNameValue("Select Year", "0"));
        //payment.Add(new CascadingDropDownNameValue("Select", "0"));
        if (Session["ServiceMethodFD"] !=null)
        {
            if (Session["ServiceMethodFD"].ToString() == "Open")
                payment.Add(new CascadingDropDownNameValue("Cheque", "Cheque"));
        }
        payment.Add(new CascadingDropDownNameValue("RTGS/E-transfer", "RTGS/E-transfer"));
        payment.Add(new CascadingDropDownNameValue("DD", "DD"));


        return payment.ToArray();
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] newcostcodeP(string knownCategoryValues, string category)
    {
        //Create sql connection and sql command
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "Select c.cc_code,c.cc_name from cost_center c where status in ('Old','New') and c.cc_code!='" + Session["cc_code"].ToString() + "' GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.CC_Code,'CC-','')) END ASC";

        //Create dataadapter and fill the dataset
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objCC = new DataSet();
        dAdapter.Fill(objCC);
        con.Close();

        //create list and add items in it 
        //by looping through dataset table
        List<CascadingDropDownNameValue> cccodes = new List<CascadingDropDownNameValue>();
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["cc_code"].ToString();
            string CCName = ccRow["cc_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }
    [WebMethod]
    public CascadingDropDownNameValue[] subclientidnew(string knownCategoryValues, string category)
    {
        string dcaID;
        //this stringdictionary contains has table with key value
        //pair of cooountry and countryID
        StringDictionary dcaa =
        AjaxControlToolkit.CascadingDropDown.
        ParseKnownCategoryValuesString(knownCategoryValues);
        dcaID = dcaa["client"].ToString();

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@dcaid", dcaID);
        cmd.CommandText = "select subclient_id,branch from subclient where client_id='" + dcaID + "'";

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        DataSet objSDca = new DataSet();
        dAdapter.Fill(objSDca);
        con.Close();
        List<CascadingDropDownNameValue> subDCA = new List<CascadingDropDownNameValue>();

        foreach (DataRow sdcRow in objSDca.Tables[0].Rows)
        {
            string CCID = sdcRow["subclient_id"].ToString();
            string CCName = sdcRow["branch"].ToString();
            string CC = CCID + " , " + CCName;
            subDCA.Add(new CascadingDropDownNameValue(CC, CCID));
        }
        return subDCA.ToArray();

    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompletionListfordailyissue(string prefixText, int count)
    {
        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();

        List<string> names = new List<string>();
        char first = prefixText[0];

        if (char.IsNumber(first))
        {
            if ((Session["roles"].ToString() == "StoreKeeper") || (Session["roles"].ToString() == "Accountant/StoreKeeper"))
            {
                //cmd = new SqlCommand("SELECT i.item_code,i.units,(select quantity from master_data where item_code=i.item_code and cc_code='" + Session["cc_code"].ToString() + "') as[Quantity] from item_codes i where i.item_code like '" + prefixText + "%' and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A')", con);
                cmd = new SqlCommand("select md.Item_code,ic.Units,md.Quantity from Master_data md join Item_codes ic on md.Item_code=ic.Item_code where  cc_code='" + Session["cc_code"].ToString() + "' and md.Item_code like '" + prefixText + "%' and  SUBSTRING(md.item_code,1,1) in (" + Session["type"].ToString() + ") and ic.status in ('4','5','5A','2A') and md.Quantity is not null and md.Quantity !='0'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_code"].ToString(),
                        "Available Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();
            }
        }
        else
        {

            if ((Session["roles"].ToString() == "StoreKeeper") || (Session["roles"].ToString() == "Accountant/StoreKeeper"))
            {
                //cmd = new SqlCommand("SELECT i.item_name,i.units,i.specification,(select quantity from master_data where item_code=i.item_code and cc_code='" + Session["cc_code"].ToString() + "') as[Quantity] from item_codes i where i.item_name like '" + prefixText + "%'and substring(i.item_code,1,1) in (" + Session["type"].ToString() + ") and i.status in ('4','5','5A','2A') ", con);
                cmd = new SqlCommand("select ic.item_name,ic.units,ic.specification,md.Quantity from Master_data md join Item_codes ic on md.Item_code=ic.Item_code where ic.item_name like '" + prefixText + "%' and substring(md.item_code,1,1) in (" + Session["type"].ToString() + ") and ic.status in ('4','5','5A','2A') and md.Quantity is not null and md.Quantity !='0'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                        (reader["item_name"].ToString() + "," + reader["specification"].ToString(),
                        "Available Stock: " + reader["Quantity"].ToString() + "\n" + reader["units"].ToString());
                        names.Add(item);
                    }
                    reader.Close();
                }
                con.Close();
            }
        }
        return names.ToArray();
    }

}


