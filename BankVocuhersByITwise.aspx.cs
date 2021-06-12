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

public partial class BankVocuhersByITwise : System.Web.UI.Page
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
            fillBank();
            //ViewState["condition"] = " and REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')='" + System.DateTime.Now.ToShortDateString() + "'";
            //fillgrid();
            trexcel.Visible = false;
            tblheader.Visible = false;
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
    public void fillBank()
    {
        try
        {
            da = new SqlDataAdapter("Select bank_name from bank_branch where status='3'", con);
            da.Fill(ds, "BankName");
            ddlbank.DataTextField = "bank_name";
            ddlbank.DataValueField = "bank_name";
            ddlbank.DataSource = ds.Tables["BankName"];
            ddlbank.DataBind();
            ddlbank.Items.Insert(0, "Select Bank Name");
            ddlbank.Items.Insert(1, "Select All");

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void fillgrid()
    {
        try{
        string condition = "";
        if (ddlyear.SelectedIndex != 0)
        {
            if (ddlmonth.SelectedIndex != 0)
            {
                string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                condition = condition + " and  datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy;
            }
            else
            {
                condition = condition + " and convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

            }
        }

        if (ddlbank.SelectedItem.Text != "Select All")
        {
            condition = condition + " and b.bank_name='" + ddlbank.SelectedItem.Text + "'";

        }
        if (ddlcccode.SelectedValue != "")
        {
            condition = condition + " and b.cc_code='" + ddlcccode.SelectedValue + "'";

        }
        //da = new SqlDataAdapter("select bank_name,description,modeofpay,No,cc_code,it_code,paymenttype,name,CAST(isnull(debit,0) AS Decimal(25,2))debit,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')date,REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-')modifieddate from bankbook where  paymenttype in ('Term Loan','PF','Salary','General','Unsecured Loan','Service Provider','Supplier','Retention','TDS') and dca_code not in ('DCA-SRTX') and  status in ('3','Debited') " + condition.ToString() + "", con);
        da = new SqlDataAdapter("Select i.* from(select bank_name,description,modeofpay,No,cc_code,it_code,paymenttype,name,CAST(isnull(debit,0) AS Decimal(25,2))debit,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')date,REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-')modifieddate from bankbook b where  paymenttype in ('Term Loan','PF','Salary','General','Unsecured Loan') and  status in ('3','Debited') and it_code='" + ddlit.SelectedItem.Text + "'" + condition.ToString() + " union select b.bank_name,b.description,b.modeofpay,b.No,b.cc_code,p.it_code,b.paymenttype,b.name,CAST(isnull(b.debit,0) AS Decimal(25,2))debit,REPLACE(CONVERT(VARCHAR(11),b.date, 106), ' ', '-')date,REPLACE(CONVERT(VARCHAR(11),b.modifieddate, 106), ' ', '-')modifieddate from bankbook b join pending_invoice p on b.invoiceno=p.invoiceno where b.paymenttype in ('Service Provider','Supplier','Retention','TDS')  and  b.status in ('3','Debited')and p.it_code='" + ddlit.SelectedItem.Text + "'" + condition.ToString() + ")i order by CONVERT(DATETIME,CONVERT(VARCHAR(11), Date)) asc", con);
        da.Fill(ds, "ITReport");
        if (ds.Tables["ITReport"].Rows.Count > 0)
        {
            Total = Convert.ToDecimal(ds.Tables["ITReport"].Compute("sum(debit)", string.Empty));
            GridView1.DataSource = ds.Tables["ITReport"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
            trexcel.Visible = true;
            tblheader.Visible = true;
            
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            trexcel.Visible = false;
            tblheader.Visible = false;
        }
          }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    private decimal Total = (decimal)0.0;

    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
       fillgrid();
    }


    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //--- For Paging ---------
        GridViewRow row = GridView1.BottomPagerRow;

        if (row == null)
        {
            return;
        }

        //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
        Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
        Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

        //if (lblPages != null)
        //{
        lblCurrent.Text = GridView1.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = GridView1.PageIndex + 1;
        lblPages.Text = currentPage.ToString();

        //-- For First and Previous ImageButton
        if (GridView1.PageIndex == 0)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Visible = false;



        }

        //-- For Last and Next ImageButton
        if (GridView1.PageIndex + 1 == GridView1.PageCount)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Visible = false;


        }
    }
    protected void btnFirst_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnPrev_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnNext_Command(object sender, CommandEventArgs e)
    {

        Paginate(sender, e);
    }
    protected void btnLast_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void Paginate(object sender, CommandEventArgs e)
    {
        // Get the Current Page Selected
        int iCurrentIndex = GridView1.PageIndex;

        switch (e.CommandArgument.ToString().ToLower())
        {
            case "first":
                GridView1.PageIndex = 0;
                break;
            case "prev":
                if (GridView1.PageIndex != 0)
                {
                    GridView1.PageIndex = iCurrentIndex - 1;
                }
                break;
            case "next":
                GridView1.PageIndex = iCurrentIndex + 1;
                break;
            case "last":
                GridView1.PageIndex = GridView1.PageCount;
                break;
        }
        fillgrid();
        //Populate the GridView Control
       
    }
    protected void btnsubmit_Click1(object sender, EventArgs e)
    {
        fillgrid();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Text = Total.ToString();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
   

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.AllowPaging = false;
        fillgrid();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "BankVouchers By ITWise"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
}
