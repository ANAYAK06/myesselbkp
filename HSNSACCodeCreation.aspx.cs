using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class HSNSACCodeCreation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            Trhsncategory.Visible = true;
            trhsncodedropdown.Visible = false;
            ddlcodecategory.SelectedIndex = 0;
        }
    }

    protected void ddlcodecategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlcodecategory.SelectedValue == "Goods")
        {
            ddlhsncodes.Items.Insert(0, "Select");
            Trhsncategory.Visible = true;
            clear();
        }
        else if (ddlcodecategory.SelectedValue == "Service")
        {
            clear();
            Trhsncategory.Visible = false;
            fillhsncode();
        }
        else
        {            
            Trhsncategory.Visible = false;
        }
        

    }
    protected void ddlhsncategory_SelectedIndexChanged(object sender, EventArgs e)
    {       
        fillhsncode();
    }
        protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedIndex == 0)
        {
            ddlcodecategory.SelectedIndex = 0;
            trhsncode.Visible = true;
            trhsncodedropdown.Visible = false;
        }
        if (rbtntype.SelectedIndex == 1)
        {
            ddlcodecategory.SelectedIndex = 0;
            trhsncode.Visible = false;
            trhsncodedropdown.Visible = true;
            
        }
        clear();

    }
    protected void ddlhsncodes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlhsncodes.SelectedValue != "Select")
        {
            ds.Clear();

            da = new SqlDataAdapter("select HSNCategory,Remarks,CGSTRate,SGSTRate,IGSTRate,Remarks from HSN_SAC_Codes where CodeCategory='" + ddlcodecategory.SelectedValue + "'", con);
            da.Fill(ds, "hsncodesdetails");
            if (ds.Tables["hsncodesdetails"].Rows.Count > 0)
            {
                txtcgstrate.Text = ds.Tables["hsncodesdetails"].Rows[0].ItemArray[2].ToString();
                txtsgstrate.Text = ds.Tables["hsncodesdetails"].Rows[0].ItemArray[3].ToString();
                txtigstrate.Text = ds.Tables["hsncodesdetails"].Rows[0].ItemArray[4].ToString();
                txtremarks.Text = ds.Tables["hsncodesdetails"].Rows[0].ItemArray[5].ToString();
            }
        }
    }
        public void fillhsncode()
    {
        if (ddlcodecategory.SelectedValue != "Select")
        {
            //if (rbtntype.SelectedIndex == 1)
                //ddlhsncodes.SelectedItem.Text = "Select";
            txtcgstrate.Text = "";
            txtsgstrate.Text = "";
            txtigstrate.Text = "";
            txtremarks.Text = "";
            ds.Clear();
            if (ddlcodecategory.SelectedValue == "Goods")
                da = new SqlDataAdapter("select HSN_SAC_Code as code from HSN_SAC_Codes where CodeCategory='" + ddlcodecategory.SelectedValue + "' and HSNCategory='"+ ddlhsncategory.SelectedValue + "'  and Status='Approved'", con);
            else
                da = new SqlDataAdapter("select HSN_SAC_Code as code from HSN_SAC_Codes where CodeCategory='" + ddlcodecategory.SelectedValue + "' and Status='Approved'", con);
            da.Fill(ds, "hsncodes");
            if (ds.Tables["hsncodes"].Rows.Count > 0)
            {
                ddlhsncodes.DataTextField = "code";
                ddlhsncodes.DataValueField = "code";
                ddlhsncodes.DataSource = ds.Tables["hsncodes"];
                ddlhsncodes.DataBind();
                ddlhsncodes.Items.Insert(0, "Select");
            }
        }
    }
    public void clear()
    {
        //if (ddlcodecategory.SelectedValue != "Service")
        //    ddlcodecategory.SelectedIndex = 0;
        ddlhsncategory.SelectedIndex = 0;
        txthsncode.Text = "";        
        txtcgstrate.Text = "";
        txtsgstrate.Text = "";
        txtigstrate.Text = "";
        txtremarks.Text = "";

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("sp_CreateHSNSACCodes", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CodeCategory", ddlcodecategory.SelectedValue);
        if (ddlcodecategory.SelectedValue == "Goods")
            cmd.Parameters.AddWithValue("@CodeType", "HSN");
        else if (ddlcodecategory.SelectedValue == "Service")
            cmd.Parameters.AddWithValue("@CodeType", "SAC");
        else
            cmd.Parameters.AddWithValue("@CodeType", "");
        if (ddlcodecategory.SelectedValue == "Goods")
        cmd.Parameters.AddWithValue("@HSNCategory", ddlhsncategory.SelectedValue);
        if (rbtntype.SelectedValue == "0")
            cmd.Parameters.AddWithValue("@HsnCode", txthsncode.Text);
        else
            cmd.Parameters.AddWithValue("@HsnCode", ddlhsncodes.SelectedValue);

        cmd.Parameters.AddWithValue("@CGSTRate", txtcgstrate.Text);
        cmd.Parameters.AddWithValue("@SGSTRate", txtsgstrate.Text);
        cmd.Parameters.AddWithValue("@IGSTRate", txtigstrate.Text);
        cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
        cmd.Parameters.AddWithValue("@Submittype", rbtntype.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
        cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
       // string msg = "Voucher Inserted1";
        con.Close();
        if (msg == "Sucessfull" || msg == "Sucessfull Updated")
            JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
        else
            JavaScript.UPAlert(Page, msg);
    }
}