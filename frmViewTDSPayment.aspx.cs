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
using AjaxControlToolkit;


public partial class Admin_frmViewTDSPayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    int no = 0;
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
            trexcel.Visible = false;
        }
        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            newpage.Visible = true;
        }
        //GridView3.Visible = false;
    }
    public void getTDS()
    {
        string condition = "";
        if (ddlyear.SelectedIndex!=0)
        {
            if (ddlmonth.SelectedIndex != 0)
            {
                string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;

                condition = condition + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;

                
            }
            else
            {
                condition = condition + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

            }
        }

        da = new SqlDataAdapter("Select  CAST(sum(Debit) AS Decimal(10,2))[Debit] from BankBook where paymenttype='TDS'" + condition + "Group by Paymenttype ", con);
        da.Fill(ds, "cc");
        if (ds.Tables["cc"].Rows.Count > 0)
        {
            lblTotal.Text = " Total TDS Pament to Government  " + ds.Tables["cc"].Rows[0].ItemArray[0].ToString();
            
        }
        else
        {
            lblTotal.Text = " Total TDS Pament to Government NIL ";
        }
        fillgrid();
        //Accordion1.DataSource = ds.Tables["cc"].DefaultView;
        //Accordion1.DataBind();
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        getTDS();
    }
    //protected void Accordion1_ItemDataBound(object sender, AjaxControlToolkit.AccordionItemEventArgs e)
    //{
        
    //    if (e.ItemType == AjaxControlToolkit.AccordionItemType.Content)
    //    {
          

    //}
    public void fillgrid()
    {
        SqlDataAdapter sqlAdapter = new SqlDataAdapter();
        string condition1 = "";
        if (ddlyear.SelectedIndex != 0)
        {
            if (ddlmonth.SelectedIndex != 0)
            {
                string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                if (rbtnpaiddate.Checked == true)
                {
                    condition1 = condition1 + " and datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + " order by b.date desc";
                }
                else if (rbtninvmkdate.Checked == true)
                {
                    condition1 = condition1 + " and datepart(mm,p.Inv_Making_Date)=" + ddlmonth.SelectedValue + " and datepart(yy,p.Inv_Making_Date)=" + yy + " order by Inv_Making_Date desc";
                }
            }
            else
            {
                if (rbtnpaiddate.Checked == true)
                {
                    condition1 = condition1 + " and convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' order by b.date desc";
                }
                else if (rbtninvmkdate.Checked == true)
                {
                    condition1 = condition1 + " and convert(datetime,p.Inv_Making_Date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' order by Inv_Making_Date desc";
                }
            }
            ViewState["cond"] = condition1;
        }
        //string yy1 = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
        if (rbtnpaiddate.Checked == true)
        {
            sqlAdapter = new SqlDataAdapter("Select b.Date,b.InvoiceNo,p.vendor_id,b.cc_code,b.Dca_Code,'' as [Basic Amount],b.Debit,b.Bank_Name from pending_invoice p join BankBook b on p.invoiceno=b.invoiceno where b.status !='Rejected' and b.paymenttype='TDS'" + condition1 + "", con);
        }
        else if (rbtninvmkdate.Checked == true)
        {
            sqlAdapter = new SqlDataAdapter("Select p.Inv_Making_Date as Date,p.InvoiceNo,p.vendor_id,p.cc_code,p.Dca_Code,p.basicvalue as [Basic Amount],p.tds_balance as Debit ,'' as Bank_Name from pending_invoice p where tds_balance >0 and p.paymenttype='Service Provider'" + condition1 + "", con);
        }
        DataSet myDataset = new DataSet();
        sqlAdapter.Fill(myDataset);
        GridView2.DataSource = myDataset;
        GridView2.DataBind();
        trexcel.Visible = true;

    }
    public void grid3()
    {
        SqlDataAdapter da = new SqlDataAdapter();
        if (rbtnpaiddate.Checked == true)
        {
            da = new SqlDataAdapter("Select b.Date,b.InvoiceNo,p.vendor_id,b.cc_code,b.Dca_Code,'' as [Basic Amount],b.Debit,b.Bank_Name from pending_invoice p join BankBook b on p.invoiceno=b.invoiceno where b.paymenttype='TDS'" + ViewState["cond"].ToString() + "", con);
        }
        else if (rbtninvmkdate.Checked == true)
        {
            da = new SqlDataAdapter("Select b.Date,b.InvoiceNo,p.vendor_id,b.cc_code,b.Dca_Code,p.basicvalue as [Basic Amount],b.Debit,b.Bank_Name from pending_invoice p join BankBook b on p.invoiceno=b.invoiceno where b.paymenttype='TDS'" + ViewState["cond"].ToString() + "", con);
        }
        DataSet ds1 = new DataSet();
        da.Fill(ds,"fill");
        GridView3.DataSource = ds.Tables["fill"];
        GridView3.DataBind();
        
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    //public override void VerifyRenderingInServerForm(Control CC)
    //{
    //    /*Verifies that the control is rendered */
    //    return;
    //}

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView2.AllowPaging = false;
        fillgrid();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View TDS Payment"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView2.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (rbtnpaiddate.Checked == true)
                e.Row.Cells[5].Visible=false;
            if (rbtninvmkdate.Checked == true)
                 e.Row.Cells[5].Visible=true;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (rbtnpaiddate.Checked == true)
                e.Row.Cells[5].Visible = false;
            if (rbtninvmkdate.Checked == true)
                e.Row.Cells[5].Visible = true;
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (rbtnpaiddate.Checked == true)
                e.Row.Cells[5].Visible = false;
            if (rbtninvmkdate.Checked == true)
                e.Row.Cells[5].Visible = true;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (rbtnpaiddate.Checked == true)
                e.Row.Cells[5].Visible = false;
            if (rbtninvmkdate.Checked == true)
                e.Row.Cells[5].Visible = true;
        }
    }

    protected void newpage_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewVendorTDS.aspx");
    }
}
