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

public partial class Admin_frmPayBill : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 43);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {

       
            LoadYear();
            paymentdetails.Visible = false;
            btn.Visible = false;
        }

    }
   
    public void LoadYear()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
        txtamt.Text = "";
        chequeno();
    }
    private void fillgrid()
    {

        if (ddlyear.SelectedIndex != 0)
        {
            if (ddlmonth.SelectedIndex != 0)
            {
                string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;

                da = new SqlDataAdapter("Select id,Date,cc_code,DCA_code,bank_name,Debit from bankbook where paymenttype='Salary'and status='0A' and bank_name='" + ddlbank.SelectedItem.Text + "' and   datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy, con);
            }

        }
        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
            paymentdetails.Visible = true;
            btn.Visible = true;
            txtcheque.Visible = false;
            ddlcheque.Visible = true;
            lblbank.Text = ddlbank.SelectedItem.Text;
        }
        else
        {
            Response.Redirect("frmPayBill.aspx");
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BankDateCheck objdate = new BankDateCheck();
            if (objdate.IsBankDateCheck(txtdate.Text, lblbank.Text))
            {
                string ids = "";

                foreach (GridViewRow rec in GridView1.Rows)
                {

                    if ((rec.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        ids = ids + GridView1.DataKeys[rec.RowIndex]["id"].ToString() + ",";
                    }

                }
                cmd = new SqlCommand("sp_paybillApprove", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@date", txtdate.Text);
                cmd.Parameters.AddWithValue("@remarks", txtdesc.Text);
                if (ddlpayment.SelectedItem.Text == "Cheque")
                {
                    cmd.Parameters.AddWithValue("@chequeno", ddlcheque.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@chequeid", ddlcheque.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@chequeno", txtcheque.Text);
                    cmd.Parameters.AddWithValue("@chequeid", "0");
                }
                cmd.Parameters.AddWithValue("@modeofpay", ddlpayment.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
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
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

        }
    }
    private decimal Amount = (decimal)0.0;


    public void chequeno()
    {

        da = new SqlDataAdapter("select cn.chequeno,cn.id from cheque_nos cn join cheque_Master cm on cn.chequeid=cm.chequeid where cm.bankname='" + ddlbank.SelectedItem.Text + "' and cn.status='2'", con);
        da.Fill(ds, "chequeno");
      
        if (ds.Tables["chequeno"].Rows.Count > 0)
        {
            ddlcheque.DataSource = ds.Tables["chequeno"];
            ddlcheque.DataTextField = "chequeno";
            ddlcheque.DataValueField = "id";
            ddlcheque.DataBind();
            ddlcheque.Items.Insert(0, "Select");
           
       }
    }



    protected void ddlpayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpayment.SelectedItem.Text == "Cheque")
        {
            txtcheque.Visible = false;
            ddlcheque.Visible = true;

        }
        else
        {
            txtcheque.Visible = true;
            ddlcheque.Visible = false;
        }
    }
}
