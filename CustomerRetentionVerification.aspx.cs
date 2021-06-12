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
using System.Drawing;

public partial class CustomerRetentionVerification : System.Web.UI.Page
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
            da = new SqlDataAdapter("select bb.Transaction_No,REPLACE(CONVERT(VARCHAR(11),bb.date, 106), ' ', '-')as Date,(select CAST(SUM(credit) AS Decimal(20,2)) from bankbook where Transaction_No=bb.Transaction_No )as Amount,bb.Bank_Name,bb.Description from BankBook bb join Invoice iv on bb.Transaction_No=iv.Ret_TranNo and bb.status=iv.Ret_Status  where bb.status='1A' group by bb.Transaction_No,bb.Date,bb.Bank_Name,bb.Description", con);
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["fill"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void fillgriddetails(string transid)
    {
        try
        {
            da = new SqlDataAdapter("select iv.InvoiceNo,iv.PO_NO,iv.Invoice_Date,iv.retention_balance,bb.Credit from Invoice iv join BankBook bb on iv.InvoiceNo=bb.InvoiceNo where iv.Ret_TranNo='" + transid + "' and bb.Status='1A'", con);
            da.Fill(ds, "filldetails");
            if (ds.Tables["filldetails"].Rows.Count > 0)
            {
                Gvverification.DataSource = ds.Tables["filldetails"];
                Gvverification.DataBind();
                fillbankdetails(transid);
            }
            else
            {
                Gvverification.DataSource = null;
                Gvverification.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void fillbankdetails(string transid)
    {
        try
        {
            da = new SqlDataAdapter("select bb.Transaction_No,REPLACE(CONVERT(VARCHAR(11),bb.date, 106), ' ', '-')as Date,(select CAST(SUM(credit) AS Decimal(20,2)) from bankbook where Transaction_No=bb.Transaction_No )as Amount,bb.Bank_Name,bb.Description,bb.ModeOfPay,bb.no from BankBook bb join Invoice iv on bb.Transaction_No=iv.Ret_TranNo and bb.status=iv.Ret_Status  where bb.status='1A' and bb.Transaction_No='" + transid + "' group by bb.Transaction_No,bb.Date,bb.Bank_Name,bb.Description,bb.ModeOfPay,bb.no", con);
            da.Fill(ds, "fillbankdetails");
            if (ds.Tables["fillbankdetails"].Rows.Count > 0)
            {
                txtbank.Text = ds.Tables["fillbankdetails"].Rows[0]["Bank_Name"].ToString();
                txtpaymenttype.Text = ds.Tables["fillbankdetails"].Rows[0]["ModeOfPay"].ToString();
                txtdate.Text = ds.Tables["fillbankdetails"].Rows[0]["Date"].ToString();
                txtcheque.Text = ds.Tables["fillbankdetails"].Rows[0]["no"].ToString();
                txtdesc.Text = ds.Tables["fillbankdetails"].Rows[0]["Description"].ToString();
                txtamt.Text = ds.Tables["fillbankdetails"].Rows[0]["Amount"].ToString();
            }
          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewEditIndex]["Transaction_No"].ToString();
        ViewState["id"] = id;
        fillgriddetails(id);
        tblinvoice.Visible = true;

    }
    private decimal CrAmount = (decimal)0.0;
    private decimal RetAmount = (decimal)0.0;
    protected void Gvverification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToDecimal(e.Row.Cells[4].Text) != Convert.ToDecimal(e.Row.Cells[5].Text))
            {
                e.Row.ForeColor = Color.Red;
                
            }
            RetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "retention_balance"));
            CrAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = String.Format("Rs. {0:#,##,##,###.00}", RetAmount);
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", CrAmount);

        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
       
        try
        {
            btnupdate.Visible = false;
            string invoicenos = "";
            foreach (GridViewRow rec in Gvverification.Rows)
            {
                if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                    invoicenos = invoicenos + Gvverification.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
            }
            cmd = new SqlCommand();            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_ClientRetention_payment";
            cmd.Parameters.AddWithValue("@invoicenos", invoicenos);
            cmd.Parameters.AddWithValue("@Bank_Name", txtbank.Text);
            cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
            cmd.Parameters.AddWithValue("@ModeOfPay", txtpaymenttype.Text);
            cmd.Parameters.AddWithValue("@No", txtcheque.Text);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
            cmd.Parameters.AddWithValue("@date", txtdate.Text);
            cmd.Parameters.AddWithValue("@PaymentType", "Retention");
            cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@TranNo", ViewState["id"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Sucessfull")
            {
                fillgrid();
                JavaScript.UPAlertRedirect(Page, msg, "CustomerRetentionVerification.aspx");
            }
            else
            {
                btnupdate.Visible = true;
                JavaScript.UPAlert(Page, msg);
            }
            con.Close();
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
            cmd = new SqlCommand("sp_ClientRejectRetention_payment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tranid", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["Transaction_No"].ToString();
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, msg, "CustomerRetentionVerification.aspx");
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