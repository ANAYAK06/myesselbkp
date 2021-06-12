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

public partial class Admin_frmUpdateCashVoucher : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("~/common/login.aspx");
        }

        if (Session["roles"].ToString() == "SuperAdmin")
        {
            if (Request.QueryString["Voucherid"] == null)
            {
                JavaScript.CloseWindow();
            }
            else
            {
                viewvoucher();
            }
        }

        else
        {
                JavaScript.CloseWindow();
        }
        lblcc.Visible = false;
    }
    public void viewvoucher()
    {
        if (!IsPostBack)
        {
            da = new SqlDataAdapter("select name,date,particulars,dca_code,sub_dca,voucher_id,debit,cc_code from cash_book where id='" + Request.QueryString["Voucherid"].ToString() + "'", con);
            da.Fill(ds, "viewcash");
            if (ds.Tables["viewcash"].Rows.Count > 0)
            {
                txtname.Text = ds.Tables["viewcash"].Rows[0][0].ToString();
                txtdate.Text = String.Format("{0:MM/dd/yyyy}", ds.Tables["viewcash"].Rows[0][1]);
                txtparticular.Text = ds.Tables["viewcash"].Rows[0][2].ToString();
                CascadingDropDown2.SelectedValue = ds.Tables["viewcash"].Rows[0][3].ToString();
                CascadingDropDown3.SelectedValue = ds.Tables["viewcash"].Rows[0][4].ToString();
                txtvoucherid.Text = ds.Tables["viewcash"].Rows[0][5].ToString();
                txtAmount.Text = ds.Tables["viewcash"].Rows[0][6].ToString();
                lblcc.Text = ds.Tables["viewcash"].Rows[0][7].ToString();
                lblcc.Visible = false;
            }
            else
            {
                JavaScript.AlertAndRedirect("Invalid", "frmApprovecashvoucher.aspx");
            }
        }
    }
    
   
    public void showalert(string message)
    {
        Label myalertlabel = new Label();
        myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "')</script>";
        Page.Controls.Add(myalertlabel);
    }
    protected void reset_Click(object sender, EventArgs e)
    {
        txtvoucherid.Text = "";
        txtname.Text = "";
        txtdate.Text = "";
        txtparticular.Text = "";
       

    }
    protected void update_Click(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "SuperAdmin")
        {
            cmd = new SqlCommand("sp_Update_CashVoucher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@TransactionType", "Debit");
            cmd.Parameters.AddWithValue("@Date", txtdate.Text);
            //cmd.Parameters.AddWithValue("@CC_Code", lblcc.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
            //cmd.Parameters.AddWithValue("@Dca", ddldetailhead.SelectedValue);
            //if (ddlsubdetail.SelectedItem.Text != "")
            //    cmd.Parameters.AddWithValue("@SubDca", ddlsubdetail.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Name", txtname.Text);
            cmd.Parameters.AddWithValue("@Description", txtparticular.Text);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@voucherid", txtvoucherid.Text);
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //con.Close();
            //lblAlert.Text = msg;

            JavaScript.AlertAndRedirect(msg, "frmUpdateCashVoucher.aspx");
            //showalert(cmd.ExecuteScalar().ToString());
            con.Close();
            lblcc.Visible = false;
        }
        else if (Session["roles"].ToString() == "Sr.Accountant")
        {
            
            cmd.Connection=con;
            cmd = new SqlCommand("update cash_book set name='"+txtname.Text+ "',particulars='"+txtparticular.Text+"' where id='" + Request.QueryString["Voucherid"].ToString() + "'",con);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k == 1)
            {
                JavaScript.Alert("Updated Sucessfully");
            }
            else
            {
                JavaScript.Alert("Updating Failed");
            }
            con.Close();

        }
        else
        {
            JavaScript.Alert("You Are Not Authorized to Delete Cash Voucher");
        }
    }
  
}
