using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class VerifyHSNSACCode : System.Web.UI.Page
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
            tblcode.Visible = false;
        }
    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select Id,CodeCategory,Remarks,HSN_SAC_Code,Remarks as code from HSN_SAC_Codes where Status='1'", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select Id,CodeCategory,Remarks,HSN_SAC_Code,Remarks as code from HSN_SAC_Codes where Status='2'", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select Id,CodeCategory,Remarks,HSN_SAC_Code,Remarks as code from HSN_SAC_Codes where Status='3'", con);

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
        string id = GridView1.DataKeys[e.NewEditIndex]["Id"].ToString();
        string HSNCode = GridView1.DataKeys[e.NewEditIndex]["HSN_SAC_Code"].ToString();
        ViewState["Tblid"] = id;
        ViewState["HSNCODE"] = HSNCode;
        filltable();
        tblcode.Visible = true;

    }
    public void filltable()
    {
            da = new SqlDataAdapter("select Id,CodeCategory,CodeType,HSN_SAC_Code,HSNCategory,Remarks,CGSTRate,SGSTRate,IGSTRate,Remarks from HSN_SAC_Codes where ID='"+ ViewState["Tblid"].ToString() + "' and HSN_SAC_Code='"+ ViewState["HSNCODE"].ToString() + "'", con);

        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            txtcodecategory.Text = ds.Tables["data"].Rows[0]["CodeCategory"].ToString();
            if (txtcodecategory.Text == "Service")
            {
                Trhsncategory.Visible = false;
            }
            else {
                Trhsncategory.Visible = true;
                txthsncategory.Text = ds.Tables["data"].Rows[0]["HSNCategory"].ToString();
            }
            txthsncode.Text = ds.Tables["data"].Rows[0]["HSN_SAC_Code"].ToString();
            txtcgstrate.Text = ds.Tables["data"].Rows[0]["CGSTRate"].ToString();
            txtsgstrate.Text = ds.Tables["data"].Rows[0]["SGSTRate"].ToString();
            txtigstrate.Text = ds.Tables["data"].Rows[0]["IGSTRate"].ToString();
            txtremarks.Text = ds.Tables["data"].Rows[0]["Remarks"].ToString();

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_CreateHSNSACCodes", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", ViewState["Tblid"].ToString());
            cmd.Parameters.AddWithValue("@HsnCode", ViewState["HSNCODE"].ToString());
            cmd.Parameters.AddWithValue("@CGSTRate", txtcgstrate.Text);
            cmd.Parameters.AddWithValue("@SGSTRate", txtsgstrate.Text);
            cmd.Parameters.AddWithValue("@IGSTRate", txtigstrate.Text);
            cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
            cmd.Parameters.AddWithValue("@Submittype", "Verify");
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Sucessfull Verified" || msg == "Sucessfull Approved")
            {
                JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                fillgrid();
            }
            else
                JavaScript.UPAlert(Page, msg);
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_CreateHSNSACCodes", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["Id"].ToString();
            cmd.Parameters.Add("@HsnCode", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["HSN_SAC_Code"].ToString();
            cmd.Parameters.AddWithValue("@Submittype", "Delete");
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@roles", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                fillgrid();
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
}