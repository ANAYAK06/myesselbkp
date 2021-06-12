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
public partial class VerifyNewFD : System.Web.UI.Page
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
            Trledger.Visible = false;
        }
    }
    public void fillgrid()
        {
        try
            {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter(" select af.id as AFID,bk.id as BKID,af.status,REPLACE(CONVERT(VARCHAR(11),af.date, 106), ' ', '-')as date,af.FDR,REPLACE(CONVERT(VARCHAR(11),af.Fromdate, 106), ' ', '-')as Fromdate,REPLACE(CONVERT(VARCHAR(11),af.todate, 106), ' ', '-')as Todate,replace(af.RateofIntrest,'.0000','.00')as Intrest,replace(af.Amount,'.0000','.00')as Amount,bk.bank_name,bk.ModeofPay,bk.PaymentType,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as Bankdate,bk.no,bk.description from AddFD af join bankbook bk on af.fdr=bk.fdr where af.Approval_status='1'", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select af.id as AFID,bk.id as BKID,af.status,REPLACE(CONVERT(VARCHAR(11),af.date, 106), ' ', '-')as date,af.FDR,REPLACE(CONVERT(VARCHAR(11),af.Fromdate, 106), ' ', '-')as Fromdate,REPLACE(CONVERT(VARCHAR(11),af.todate, 106), ' ', '-')as Todate,replace(af.RateofIntrest,'.0000','.00')as Intrest,replace(af.Amount,'.0000','.00')as Amount,bk.bank_name,bk.ModeofPay,bk.PaymentType,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as Bankdate,bk.no,bk.description from AddFD af join bankbook bk on af.fdr=bk.fdr where af.Approval_status='2'", con);
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
        string FDR = GridView1.DataKeys[e.NewEditIndex]["FDR"].ToString();
        ViewState["FDR"] = FDR;
        ViewState["AFID"] = GridView1.DataKeys[e.NewEditIndex]["AFID"].ToString();
        ViewState["BKID"] = GridView1.DataKeys[e.NewEditIndex]["BKID"].ToString();
        filltable();
        tblinvoice.Visible = true;

        }
    public void filltable()
    {
        da = new SqlDataAdapter("select af.id as AFID,bk.id as BKID,af.status,REPLACE(CONVERT(VARCHAR(11),af.date, 106), ' ', '-')as date,af.FDR,REPLACE(CONVERT(VARCHAR(11),af.Fromdate, 106), ' ', '-')as Fromdate,REPLACE(CONVERT(VARCHAR(11),af.todate, 106), ' ', '-')as Todate,replace(af.RateofIntrest,'.0000','.00')as Intrest,replace(af.Amount,'.0000','.00')as Amount,bk.bank_name,bk.ModeofPay,bk.PaymentType,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as Bankdate,bk.no,bk.description from AddFD af join bankbook bk on af.fdr=bk.fdr where af.FDR='" + ViewState["FDR"].ToString() + "' and af.id='" + ViewState["AFID"].ToString() + "'", con);
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows[0]["status"].ToString() == "Open")
        txtfdrtype.Text = "Open FD";
        if (ds.Tables["data"].Rows[0]["status"].ToString() == "Closed")
            txtfdrtype.Text = "Close FD";
        txtfddate.Text = ds.Tables["data"].Rows[0]["date"].ToString();
        txtfdrno.Text = ds.Tables["data"].Rows[0]["FDR"].ToString();
        txtfromdate.Text = ds.Tables["data"].Rows[0]["Fromdate"].ToString();
        txttodate.Text = ds.Tables["data"].Rows[0]["Todate"].ToString();
        txtrateofintrest.Text = ds.Tables["data"].Rows[0]["Intrest"].ToString();
        txtamount.Text = ds.Tables["data"].Rows[0]["Amount"].ToString();
        txtfrombank.Text = ds.Tables["data"].Rows[0]["bank_name"].ToString();
        txtpayment.Text = ds.Tables["data"].Rows[0]["ModeofPay"].ToString();
        txtdate.Text = ds.Tables["data"].Rows[0]["Bankdate"].ToString();
        txtcheque.Text = ds.Tables["data"].Rows[0]["no"].ToString();
        txtdesc.Text = ds.Tables["data"].Rows[0]["description"].ToString();
        txtamt.Text = ds.Tables["data"].Rows[0]["Amount"].ToString();
        if (ds.Tables["data"].Rows[0]["status"].ToString() == "Open")
        {
            Trledger.Visible = true;
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("SELECT id,Ledger_Type,(CASE WHEN Payment_Type='Debit' Then '0' Else '1' End)Payment_Type,Name,NatureGroupId,Group_id,replace(Amount,'.0000','.00')as Amount,Code,REPLACE(CONVERT(VARCHAR(11),Balance_Date, 106), ' ', '-') as Balance_Date FROM Ledger WHERE Code='" + ds.Tables["data"].Rows[0]["AFID"].ToString() + "' and Ledger_Type='FD' and status='1'", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("SELECT id,Ledger_Type,(CASE WHEN Payment_Type='Debit' Then '0' Else '1' End)Payment_Type,Name,NatureGroupId,Group_id,replace(Amount,'.0000','.00')as Amount,Code,REPLACE(CONVERT(VARCHAR(11),Balance_Date, 106), ' ', '-') as Balance_Date FROM Ledger WHERE Code='" + ds.Tables["data"].Rows[0]["AFID"].ToString() + "' and Ledger_Type='FD' and status='2'", con);

            da.Fill(ds, "Ledgerinfo");
            if (ds.Tables["Ledgerinfo"].Rows.Count > 0)
            {
                FillGroups();
                ViewState["LedgerID"] = ds.Tables["Ledgerinfo"].Rows[0]["id"].ToString();
                lblledgername.Text = ds.Tables["Ledgerinfo"].Rows[0]["Name"].ToString();
                ddlsubgroup.SelectedValue = ds.Tables["Ledgerinfo"].Rows[0]["Group_id"].ToString();
                lblledbaldate.Text = ds.Tables["Ledgerinfo"].Rows[0]["Balance_Date"].ToString();
                lblopeningbal.Text = ds.Tables["Ledgerinfo"].Rows[0]["Amount"].ToString();
                rbtnpaymenttype.SelectedValue = ds.Tables["Ledgerinfo"].Rows[0]["Payment_Type"].ToString();
                rbtnpaymenttype.Enabled = false;
                ddlsubgroup.Enabled = false;
            }
        }
    }
    public void FillGroups()
    {
        try
        {
            da = new SqlDataAdapter("Select i.* from (Select  CONVERT(varchar(10), id)as id,Name from Sub_Group where status='3'union all select Group_id as id,group_name as Name from mastergroups where status='3')i order by i.Name asc", con);
            da.Fill(ds, "Subgroups");
            ddlsubgroup.DataTextField = "Name";
            ddlsubgroup.DataValueField = "id";
            ddlsubgroup.DataSource = ds.Tables["Subgroups"];
            ddlsubgroup.DataBind();
            ddlsubgroup.Items.Insert(0, "Select Sub-Groups");
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
        {
        btnupdate.Visible = false;
        try
            {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_CreateFD";
            cmd.Parameters.AddWithValue("@Fdrno", ViewState["FDR"].ToString());
            cmd.Parameters.AddWithValue("@AFID", ViewState["AFID"].ToString());
            cmd.Parameters.AddWithValue("@BKID", ViewState["BKID"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@LedgerId", ViewState["LedgerID"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Verified")
                {
                JavaScript.UPAlertRedirect(Page, "Verified Successfully", "VerifyNewFD.aspx");
                fillgrid();
                }
            else
                {
                JavaScript.UPAlert(Page, msg);
                btnupdate.Visible = true;
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
            cmd = new SqlCommand("sp_RejectFD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Fdrno", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["FDR"].ToString();
            cmd.Parameters.Add("@AFID", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["AFID"].ToString();
            cmd.Parameters.Add("@BKID", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["BKID"].ToString();
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
                {
                JavaScript.UPAlertRedirect(Page, msg, "VerifyNewFD.aspx");
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