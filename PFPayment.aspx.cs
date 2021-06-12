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

public partial class PFPayment : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            LoadYear();
            tblpayment.Visible = false;
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

    protected void btngo_Click(object sender, EventArgs e)
    {
        fillgrid();
        ddlcheque.Visible = false;
        txtamt.Text = "";
    }
    private void fillgrid()
    {
      
        if (ddlyear.SelectedIndex != 0)
        {
            if (ddlmonth.SelectedIndex != 0)
            {
                string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;

                da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,cc_code,sub_dca,bank_name,Debit from bankbook where paymenttype='PF' and status='0B'  and   datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy, con);
            }

        }
        da.Fill(ds, "PFfill");
        if (ds.Tables["PFfill"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["PFfill"];
            GridView1.DataBind();
            tblpayment.Visible = true;
            btn.Visible = true;
            //CascadingDropDown1.SelectedValue = ds.Tables["PFfill"].Rows[0][4].ToString();
        }
        else
        {
            Response.Redirect("PFPayment.aspx");
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BankDateCheck objdate = new BankDateCheck();
            if (objdate.IsBankDateCheck(txtdate.Text, ddlbank.SelectedItem.Text))
            {
                string ids = "";

                foreach (GridViewRow rec in GridView1.Rows)
                {

                    if ((rec.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        ids = ids + GridView1.DataKeys[rec.RowIndex]["id"].ToString() + ",";
                    }

                }
                cmd = new SqlCommand("sp_PFPayment", con);
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
                }
                cmd.Parameters.AddWithValue("@modeofpay", ddlpayment.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@bank", ddlbank.SelectedItem.Text);
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
    public void chequeno()
    {
        try
        {
            ddlcheque.Items.Clear();
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
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PFPayment.aspx");
    }
    protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
    {
        chequeno();
    }
    protected void ddlpayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlpayment.SelectedItem.Text == "Cheque")
            {
                ddlcheque.Visible = true;
                txtcheque.Visible = false;
                chequeno();
            }
            else
            {
                ddlcheque.Visible = false;
                txtcheque.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
}
