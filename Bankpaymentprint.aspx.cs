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

public partial class Bankpaymentprint : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (Request.QueryString["id"] == null)
        {
            JavaScript.CloseWindow();
        }
        else
        {
            LoadReport();
        }
    }
    public void LoadReport()
    {
        da = new SqlDataAdapter("Select distinct BT.Transaction_No,BT.party_name,REPLACE(CONVERT(VARCHAR(11),BT.Created_Date, 106), ' ', '-')date,BT.Bank_Name,BT.Mode_Of_Pay,BT.Cheque_No,BT.Amount,BT.created_by,b.CC_Code,b.DCA_Code,b.Sub_DCA,b.Description from [BankTransactions]BT join [bankbook]b on BT.Transaction_No = b.Transaction_No where BT.Transaction_No='" + Request.QueryString["id"].ToString() + "'", con);
        da.Fill(ds, "rep");
        if (ds.Tables["rep"].Rows.Count > 0)
        {
            //ViewState["Check"] = ds.Tables["rep"].Rows[0]["created_by"].ToString();
            lblvoucherid.Text = ds.Tables["rep"].Rows[0]["Transaction_No"].ToString();
            lbldate.Text = ds.Tables["rep"].Rows[0]["Date"].ToString();
            lblcccode.Text = ds.Tables["rep"].Rows[0]["CC_Code"].ToString();
            lblDca.Text = ds.Tables["rep"].Rows[0]["DCA_Code"].ToString();
            lblsubdca.Text = ds.Tables["rep"].Rows[0]["Sub_DCA"].ToString();
            lblmodeofapy.Text = ds.Tables["rep"].Rows[0]["Mode_Of_Pay"].ToString();
            lblbname.Text = ds.Tables["rep"].Rows[0]["Bank_Name"].ToString();
            lblcno.Text = ds.Tables["rep"].Rows[0]["Cheque_No"].ToString();
            lblperticular.Text = ds.Tables["rep"].Rows[0]["Description"].ToString();
            lblTotal.Text = ds.Tables["rep"].Rows[0]["Amount"].ToString();
            lblamount.Text = ds.Tables["rep"].Rows[0]["Amount"].ToString();
            //lblapproveby.Text = ds.Tables["rep"].Rows[0]["created_by"].ToString();
            getname(ds.Tables["rep"].Rows[0]["created_by"].ToString());
            lblpayto.Text = ds.Tables["rep"].Rows[0]["party_name"].ToString();
            
        }
        else
        {
            lblvoucherid.Text = "-";
            lbldate.Text = "-";
            lblcccode.Text = "-";
            lblDca.Text = "-";
            lblsubdca.Text = "-";
            lblmodeofapy.Text = "-";
            lblbname.Text = "-";
            lblcno.Text = "-";
            lblperticular.Text = "-";
            lblTotal.Text = "-";
            lblamount.Text = "-";
            lblapproveby.Text = "-";
        }


    }

    public void getname(string id)
    {
        da = new SqlDataAdapter("select first_name,last_name from register r join [BankTransactions]b on r.user_name=b.created_by where b.created_by='" + id.ToString() + "'", con);
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            lblapproveby.Text = ds.Tables["data"].Rows[0][0].ToString() + "  " + ds.Tables["data"].Rows[0][1].ToString();
        }

    }
}
