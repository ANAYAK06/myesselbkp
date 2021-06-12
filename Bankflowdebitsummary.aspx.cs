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
using System.Drawing;

public partial class Bankflowdebitsummary : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
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
            LoadYear();
            tblexpences.Visible = false;
            trexcel.Visible = false;
        }

    }
  
    public void LoadYear()
    {
        for (int i = 2009; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Select Year");
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("BankSummary_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Noofyears", SqlDbType.VarChar).Value = ddlnumberofyears.SelectedItem.Text;
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
            da.Fill(ds, "credit");

            ViewState["Columns"] = ds.Tables[0].Clone();
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            trexcel.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("style", "cursor: pointer");
         
            DataTable objdt = ViewState["Columns"] as DataTable;
            for (int i = 2; i < e.Row.Cells.Count - 1; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                if (i == 2)
                {
                    e.Row.Cells[i].Attributes.Add("onclick", "window.open('BankDebitSummary.aspx?DCACode=" + e.Row.Cells[0].Text + "&Year=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "&Year1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "&PrevYear=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + " &PrevYear1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                }
                else
                {
                    e.Row.Cells[i].Attributes.Add("onclick", "window.open('BankDebitSummary.aspx?DCACode=" + e.Row.Cells[0].Text + "&Year=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "&Year1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "&PrevYear=" + objdt.Columns[i].ToString().Substring(0, 4) + " &PrevYear1=20" + objdt.Columns[i].ToString().Substring(5, 2) + "','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                }
               
            }
            e.Row.Cells[e.Row.Cells.Count - 1].HorizontalAlign = HorizontalAlign.Right;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            //for (int i = 2; i < e.Row.Cells.Count; i++)
            //{

            //    e.Row.Cells[i].Text = String.Format("Rs. {0:#,##,##,###.00}", Convert.ToInt32(ds.Tables[0].Compute("SUM([" + ds.Tables[0].Columns[i].ColumnName + "])", string.Empty)));

            //}
        }
   
    }
    private decimal Cash = (decimal)0.0;
    private decimal Cheque = (decimal)0.0;
    private decimal PaidTotal = (decimal)0.0;
    private decimal CashPending = (decimal)0.0;
    private decimal ChequePending = (decimal)0.0;
    private decimal PendingTotal = (decimal)0.0;
    private decimal CashTotal = (decimal)0.0;
    private decimal ClosingTotal = (decimal)0.0;

    public void ClosingBalance()
    {
        try
        {
            da = new SqlDataAdapter("ClosingBalance_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
            da.Fill(ds, "ClosingBalance");
            if (ds.Tables["ClosingBalance"].Rows.Count > 0)
            {
                Label9.Text = ds.Tables["ClosingBalance"].Rows[0][0].ToString();
                da = new SqlDataAdapter("(select (case when c.bank_name is null then ch.bank_name else c.bank_name end) [Bank Name],Debit,Credit,Round((isnull(Credit,0)-isnull(Debit,0)),2)[Balance] from(select bank_name,sum(isnull(debit,0))Debit from bankbook where convert(datetime,ModifiedDate) between '04/01/'+'" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/'+'" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and (status is null or status='Debited' or status='3') and bank_name  in (select bank_name from bank_branch) and paymenttype in ('Service Provider','Supplier','Retention','TDS','Hold','Service Tax','Excise Duty','General','Service provider VAT','Unsecured Loan','Salary','Term Loan','PF','Withdraw') group by bank_name) c full outer join(select bank_name,sum(isnull(credit,0))Credit from bankbook where convert(datetime,ModifiedDate) between '04/01/'+'" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/'+'" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and bank_name  in (select bank_name from bank_branch)  and (status is null or status='Debited' or status='3') group by bank_name) ch on c.bank_name=ch.bank_name)", con);
                da.Fill(ds, "fillgrid2");
                GridView2.DataSource = ds.Tables["fillgrid2"];
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {

        }

    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ClosingTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = String.Format("Rs. {0:#,##,##,###.00}", ClosingTotal);

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string s = ddlyear.SelectedItem.Text + " Debit Summary.xls";
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", s));
        Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
        Panel2.RenderControl(htw);
        //GridView1.RenderControl(htw);
        GridView1.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
        GridView1.DataBind();
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#507CD1");
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
        }
        int j = 1;
        //This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            gvrow.BackColor = Color.White;
            if (j <= GridView1.Rows.Count)
            {
                if (j % 2 != 0)
                {
                    for (int k = 0; k < gvrow.Cells.Count; k++)
                    {
                        gvrow.Cells[k].Style.Add("background-color", "#FFFFFF");
                    }
                }
            }
            j++;
        }

        Response.Write(sw.ToString());
        Response.End();
    }
}
