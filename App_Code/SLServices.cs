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
/// Summary description for SLServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class SLServices : System.Web.Services.WebService {
    string strConnection = ConfigurationManager.AppSettings["esselDB"];


    [WebMethod]
    public string cctype(string cctype)
    {
        HttpContext.Current.Cache.Insert("dat1", cctype, null, DateTime.MaxValue, TimeSpan.FromMinutes(20));
        return cctype;
    }
    [WebMethod]
    public CascadingDropDownNameValue[] clientid(string knownCategoryValues, string category)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = System.Data.CommandType.Text;
        if (HttpContext.Current.Cache["dat"].ToString() == "Invoice Service")
            cmd.CommandText = "Select client_id from po where  SUBSTRING(client_id,1,2)='SC' and cc_code='" + knownCategoryValues.Replace("undefined:", "").Replace(";", "") + "'";
        else if (HttpContext.Current.Cache["dat"].ToString() == "Trading Supply")
            cmd.CommandText = "Select client_id from client where  SUBSTRING(client_id,1,2)='TC'";
        else if (HttpContext.Current.Cache["dat"].ToString() == "Manufacturing")
        {
            if (knownCategoryValues.Replace("undefined:", "").Replace(";", "") != "Select Cost Center")
                cmd.CommandText = "Select ci.client_id from client ci join po p on ci.client_id=p.client_id where p.cc_code='" + HttpContext.Current.Cache["dat1"].ToString() + "'";
            else
                cmd.CommandText = "Select client_id from po where  SUBSTRING(client_id,1,2)='SC' and SUBSTRING(client_id,1,2)='TC' and cc_code='" + knownCategoryValues.Replace("undefined:", "").Replace(";", "") + "'";
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
    public CascadingDropDownNameValue[] clientidnew(string knownCategoryValues, string category)
    {

        SqlConnection con = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandType = System.Data.CommandType.Text;
        if (HttpContext.Current.Cache["dat"].ToString() == "Invoice Service")
            cmd.CommandText = "Select distinct p.client_id,c.client_name from po p join client c on p.client_id=c.client_id where  SUBSTRING(p.client_id,1,2)='SC' and cc_code='" + knownCategoryValues.Replace("undefined:", "").Replace(";", "") + "'";
        else if (HttpContext.Current.Cache["dat"].ToString() == "Trading Supply")
            cmd.CommandText = "Select distinct client_id,Client_name from client where  SUBSTRING(client_id,1,2)='TC'";
        else if (HttpContext.Current.Cache["dat"].ToString() == "Manufacturing")
        {
            if (knownCategoryValues.Replace("undefined:", "").Replace(";", "") != "Select Cost Center")
                cmd.CommandText = "Select distinct ci.client_id,ci.Client_name from client ci join po p on ci.client_id=p.client_id where p.cc_code='" + HttpContext.Current.Cache["dat1"].ToString() + "'";
            else
                cmd.CommandText = "Select distinct p.client_id,c.client_name from po p join client c on p.client_id=c.client_id where  SUBSTRING(p.client_id,1,2)='SC' and SUBSTRING(p.client_id,1,2)='TC' and cc_code='" + knownCategoryValues.Replace("undefined:", "").Replace(";", "") + "'";
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
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        //cccodes.Add(new CascadingDropDownNameValue("Select Cost Center", "0"));
        // cccodes.Add(new CascadingDropDownNameValue("Select All", "0"));

        foreach (DataRow ccRow in objCC.Tables[0].Rows)
        {
            string CCID = ccRow["client_id"].ToString();
            string CCName = ccRow["client_name"].ToString();
            string CC = CCID + " , " + CCName;
            cccodes.Add(new CascadingDropDownNameValue(CC, CCID));
            //string CCName = ccRow["client_id"].ToString();
            //cccodes.Add(new CascadingDropDownNameValue(CCName, CCID));
        }
        return cccodes.ToArray();


    }

}
