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


public partial class VerifyFDInterest : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
            tblinvoice.Visible = false;
        }

    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter(" select sc.Tran_no,sc.FDR,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,replace(bk.credit,'.0000','.00')as Amount,bk.bank_name,bk.description from FD_Intrest sc join bankbook bk on sc.Tran_no=bk.transaction_no where sc.status='I1'", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter(" select sc.Tran_no,sc.FDR,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,replace(bk.credit,'.0000','.00')as Amount,bk.bank_name,bk.description from FD_Intrest sc join bankbook bk on sc.Tran_no=bk.transaction_no where sc.status='I2'", con);
            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewEditIndex]["Tran_no"].ToString();
        string fdr = GridView1.DataKeys[e.NewEditIndex]["FDR"].ToString();
        ViewState["tranid"] = id;
        ViewState["Fdr"] = fdr;
        filltable();
        tblinvoice.Visible = true;

    }
    public void filltable()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            da = new SqlDataAdapter("select sc.fdr,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,replace(sc.intrest_amount,'.0000','.00')as IntAmount,sc.ded_cccode,sc.ded_dcacode,sc.ded_sdcacode,replace(sc.ded_amount,'.0000','.00')as DedAmount,bk.bank_name,bk.modeofpay,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as bankdate,bk.no,bk.description,replace(bk.credit,'.0000','.00')as credit from FD_Intrest sc join bankbook bk on sc.tran_no=bk.transaction_no where sc.tran_no='" + ViewState["tranid"].ToString() + "' and sc.status='I1'", con);
        else if (Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("select sc.fdr,REPLACE(CONVERT(VARCHAR(11),sc.date, 106), ' ', '-')as date,replace(sc.intrest_amount,'.0000','.00')as IntAmount,sc.ded_cccode,sc.ded_dcacode,sc.ded_sdcacode,replace(sc.ded_amount,'.0000','.00')as DedAmount,bk.bank_name,bk.modeofpay,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as bankdate,bk.no,bk.description,replace(bk.credit,'.0000','.00')as credit from FD_Intrest sc join bankbook bk on sc.tran_no=bk.transaction_no where sc.tran_no='" + ViewState["tranid"].ToString() + "' and sc.status='I2'", con);

        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            txtfdrnoInt.Text = ds.Tables["data"].Rows[0]["fdr"].ToString();
            txtintdate.Text = ds.Tables["data"].Rows[0]["date"].ToString();
            txtintrestamt.Text = ds.Tables["data"].Rows[0]["IntAmount"].ToString();
            cccode(ds.Tables["data"].Rows[0]["ded_cccode"].ToString());
            dcaname(ds.Tables["data"].Rows[0]["ded_dcacode"].ToString());
            sdcaname(ds.Tables["data"].Rows[0]["ded_sdcacode"].ToString());
            txtdedamount.Text = ds.Tables["data"].Rows[0]["DedAmount"].ToString();
            txtbankname.Text = ds.Tables["data"].Rows[0]["bank_name"].ToString();
            txtmodeofpay.Text = ds.Tables["data"].Rows[0]["modeofpay"].ToString();
            txtbankdate.Text = ds.Tables["data"].Rows[0]["bankdate"].ToString();
            txtcheque.Text = ds.Tables["data"].Rows[0]["no"].ToString();
            txtdesc.Text = ds.Tables["data"].Rows[0]["description"].ToString();
            txtamt.Text = ds.Tables["data"].Rows[0]["credit"].ToString();
        }
    }
    public void cccode(string code)
    {
        da = new SqlDataAdapter("select (cc_code+' , '+cc_name) as name from cost_center where cc_code='" + code + "'", con);
        da.Fill(ds, "ccs");
        if (ds.Tables["ccs"].Rows.Count > 0)
        {
            txtcccodeded.Text = ds.Tables["ccs"].Rows[0]["name"].ToString();
        }
        else
        {
            txtcccodeded.Text = "";
        }
    }

    public void dcaname(string code)
    {
        da = new SqlDataAdapter("select (mapdca_code+' , '+dca_name) as name from dca where dca_code='" + code + "'", con);
        da.Fill(ds, "dcas");
        if (ds.Tables["dcas"].Rows.Count > 0)
        {
            txtdcaded.Text = ds.Tables["dcas"].Rows[0]["name"].ToString();
        }
        else
        {
            txtdcaded.Text = "";
        }
    }
    public void sdcaname(string code)
    {
        da = new SqlDataAdapter("select (mapsubdca_code+' , '+subdca_name) as name from subdca where subdca_code='" + code + "'", con);
        da.Fill(ds, "sdcas");
        if (ds.Tables["sdcas"].Rows.Count > 0)
        {
            txtsdcacodeded.Text = ds.Tables["sdcas"].Rows[0]["name"].ToString();
        }
        else
        {
            txtsdcacodeded.Text = "";
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = false;
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FDIntrest";
            cmd.Parameters.AddWithValue("@TransactionNo", ViewState["tranid"].ToString());
            cmd.Parameters.AddWithValue("@Fdrno", ViewState["Fdr"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Verified")
            {
                JavaScript.UPAlertRedirect(Page, "Verified Successfully", "verifyFDInterest.aspx");
                fillgrid();
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, msg, "verifyFDInterest.aspx");
            }
            con.Close();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_RejectFdInterest", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tranid", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["Tran_no"].ToString();
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, msg, "VerifyFDInterest.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
            fillgrid();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
}