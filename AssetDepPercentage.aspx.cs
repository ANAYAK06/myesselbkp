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
using System.Data.Odbc;
using System.IO;

public partial class AssetDepPercentage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fill();
        }

    }
    public void fill()
    {
        da = new SqlDataAdapter("select REPLACE(DepValue,'.0000','.00') from DepPercentage where modifieddate is null", con);
        da.Fill(ds, "limit");
        if (ds.Tables["limit"].Rows.Count > 0)
            txtolddep.Text = ds.Tables["limit"].Rows[0].ItemArray[0].ToString();
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "InsertDepValues";
        cmd.Parameters.AddWithValue("@DepValue", txtdep.Text);
        cmd.Connection = con;
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        {

            if (msg == "Sucessfull")
            {
                JavaScript.UPAlertRedirect(Page, msg, "AssetDepPercentage.aspx");
            }
        }
    }
}