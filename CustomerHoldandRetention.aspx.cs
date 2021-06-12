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

public partial class CustomerHoldandRetention : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        if (!IsPostBack)
        {
            paymentdetails.Visible = false;
        }
    }
    protected void ddltypeofpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlclientid.Items.Clear();
        ddlsubclientid.Items.Clear();
        ddlpo.Items.Clear();
        ddlcccode.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        paymentdetails.Visible = false;
        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Retention")
                da = new SqlDataAdapter("select distinct client_id from invoice where retention_balance!='0' and client_id is not null and (ret_status is null or ret_status='2A' or ret_status='NULL')", con);
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
                da = new SqlDataAdapter("select distinct client_id from invoice where hold_balance!='0' and client_id is not null and (Hold_Status is null or Hold_Status='2A' or Hold_Status='NULL')", con);

            da.Fill(ds, "client");

            ddlclientid.DataTextField = "client_id";
            ddlclientid.DataValueField = "client_id";
            ddlclientid.DataSource = ds.Tables["client"];
            ddlclientid.DataBind();
            ddlclientid.Items.Insert(0, "select");
            ddlsubclientid.Items.Insert(0, "select");
            ddlcccode.Items.Insert(0, "select");
            ddlcccode.Items.Insert(1, "select All");
            ddlpo.Items.Insert(0, "select");
            ddlpo.Items.Insert(1, "select All");
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        
    }
  
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlcccode.Items.Clear();
            ddlpo.Items.Clear();
            GridView1.DataSource = null;
            GridView1.DataBind();
            paymentdetails.Visible = false;
            if (ddltypeofpay.SelectedItem.Text == "Retention")
                da = new SqlDataAdapter("select   distinct i.cc_code, i.cc_code+ ',' +c.cc_name as ccname  from invoice i join cost_center c on i.cc_code=c.cc_code where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and retention_balance!='0' and (ret_status is null or ret_status='2A' or ret_status='NULL')", con);
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
                da = new SqlDataAdapter("select   distinct i.cc_code, i.cc_code+ ',' +c.cc_name as ccname  from invoice i join cost_center c on i.cc_code=c.cc_code where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and hold_balance!='0' and (Hold_Status is null or Hold_Status='2A' or Hold_Status='NULL')", con);

            da.Fill(ds, "cccode");

            ddlcccode.DataTextField = "ccname";
            ddlcccode.DataValueField = "cc_code";
            ddlcccode.DataSource = ds.Tables["cccode"];
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "select");
            ddlcccode.Items.Insert(1, "select All");
            ddlpo.Items.Insert(0, "select");
            ddlpo.Items.Insert(1, "select All");
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpo.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        paymentdetails.Visible = false;
        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Retention")
                da = new SqlDataAdapter("select distinct po_no from invoice where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and cc_code='" + ddlcccode.SelectedValue + "'  and retention_balance!='0' and (ret_status is null or ret_status='2A' or ret_status='NULL')", con);
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
                da = new SqlDataAdapter("select distinct po_no from invoice where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and cc_code='" + ddlcccode.SelectedValue + "'  and hold_balance!='0' and (Hold_Status is null or Hold_Status='2A' or Hold_Status='NULL')", con);

            da.Fill(ds, "pono");
         
                ddlpo.DataTextField = "po_no";
                ddlpo.DataValueField = "po_no";
                ddlpo.DataSource = ds.Tables["pono"];
                ddlpo.DataBind();
                ddlpo.Items.Insert(0, "select");
                ddlpo.Items.Insert(1, "select All");
        }

     
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    
    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpo.Items.Clear();
        ddlcccode.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        paymentdetails.Visible = false;
        try
        {
            ddlsubclientid.Items.Clear();
            ddlcccode.Items.Clear();
            ddlpo.Items.Clear();
            if (ddltypeofpay.SelectedItem.Text == "Retention")
                da = new SqlDataAdapter("select distinct subclient_id from invoice where  client_id='" + ddlclientid.SelectedItem.Text + "' and retention_balance!='0' and (ret_status is null or ret_status='2A' or ret_status='NULL')", con);
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
                da = new SqlDataAdapter("select distinct subclient_id from invoice where  client_id='" + ddlclientid.SelectedItem.Text + "' and hold_balance!='0' and (Hold_Status is null or Hold_Status='2A' or Hold_Status='NULL')", con);

            da.Fill(ds, "subclient");

            ddlsubclientid.DataTextField = "subclient_id";
            ddlsubclientid.DataValueField = "subclient_id";
            ddlsubclientid.DataSource = ds.Tables["subclient"];
            ddlsubclientid.DataBind();
            ddlcccode.Items.Insert(0, "select");
            ddlcccode.Items.Insert(1, "select All");
            ddlpo.Items.Insert(0, "select");
            ddlpo.Items.Insert(1, "select All");
            ddlsubclientid.Items.Insert(0, "select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
       
        string condition = "";

        try
        {
            if (ddlclientid.SelectedItem.Text != "select")
            {
                condition = condition + " and client_id='" + ddlclientid.SelectedItem.Text + "'";

            }
            if (ddlsubclientid.SelectedItem.Text != "select")
            {
                condition = condition + " and subclient_id='" + ddlsubclientid.SelectedItem.Text + "'";

            }
            if (ddlcccode.SelectedValue != "" && ddlcccode.SelectedValue != "select All")
            {
                condition = condition + " and cc_code='" + ddlcccode.SelectedValue + "'";

            }
            if (ddlpo.SelectedItem.Text != "select" && ddlpo.SelectedItem.Text != "select All")
            {
                condition = condition + " and po_no='" + ddlpo.SelectedItem.Text + "'";

            }
            if (ddltypeofpay.SelectedItem.Text == "Retention")
            {

                da = new SqlDataAdapter("select InvoiceNo,PO_NO,REPLACE(CONVERT(VARCHAR(11),Invoice_Date, 106), ' ', '-')Invoice_Date,replace(isnull(retention_balance,0),'.0000','.00')Amount from invoice where retention_balance<>0 and (ret_status is null or ret_status='2A' or ret_status='NULL') " + condition + "", con);
            }
            if (ddltypeofpay.SelectedItem.Text == "Hold")
            {
                da = new SqlDataAdapter("select InvoiceNo,PO_NO,REPLACE(CONVERT(VARCHAR(11),Invoice_Date, 106), ' ', '-')Invoice_Date,replace(isnull(hold_balance,0),'.0000','.00')Amount from invoice where hold_balance<>0 and (Hold_Status is null or Hold_Status='2A' or Hold_Status='NULL') " + condition + "", con);

            }
            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                paymentdetails.Visible = true;
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BankDateCheck objdate = new BankDateCheck();
            if (objdate.IsBankDateCheck(txtdate.Text, ddlbankname.SelectedItem.Text))
            {
                string invoicenos = "";
                foreach (GridViewRow rec in GridView1.Rows)
                {
                    if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                        invoicenos = invoicenos + GridView1.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                }
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                if (ddltypeofpay.SelectedItem.Text == "Retention")
                {
                    cmd.CommandText = "sp_ClientRetention_payment";
                }
                else if (ddltypeofpay.SelectedItem.Text == "Hold")
                {
                    cmd.CommandText = "sp_ClientHold_payment";
                }
                cmd.Parameters.AddWithValue("@invoicenos", invoicenos);
                cmd.Parameters.AddWithValue("@Bank_Name", ddlbankname.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
                cmd.Parameters.AddWithValue("@date", txtdate.Text);
                cmd.Parameters.AddWithValue("@PaymentType", ddltypeofpay.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "";
                JavaScript.UPAlertRedirect(Page, msg, "CustomerHoldandRetention.aspx");
                con.Close();
            }
            else
            {
                JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
            }
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
    
}
