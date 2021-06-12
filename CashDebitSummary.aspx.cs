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
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;

public partial class CashDebitSummary : System.Web.UI.Page
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
            da = new SqlDataAdapter("CashSummary_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Noofyears", SqlDbType.VarChar).Value = ddlnumberofyears.SelectedItem.Text;
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";          

            da.Fill(ds, "summary");
            ViewState["Columns"] = ds.Tables[0].Clone();
            string s = ddlyear.SelectedValue;
            GridView1.DataSource = ds.Tables["summary"];
            GridView1.DataBind();
            ClosingBalance();
        }
        catch (Exception ex)
        {

        }
    }
   
    public void ClosingBalance()
    {
        try
        {
            da = new SqlDataAdapter("ClosingBalance_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";
            da.Fill(ds, "ClosingBalance");
            if (ds.Tables["ClosingBalance"].Rows.Count > 0)
            {
                Label9.Text = ds.Tables["ClosingBalance"].Rows[0][0].ToString().Replace(".0000", ".00");
                da = new SqlDataAdapter("(select (case when c.cc_code is null then ch.cc_code else c.cc_code end) [CC Code],Debit,Credit,CONVERT(decimal(15,2),(Round((isnull(Credit,0)-isnull(Debit,0)),2)))[Balance] from(select cc_code,sum(isnull(debit,0))Debit from cash_book where convert(datetime,modifiedby_date) between '04/01/'+'" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/'+'" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'  group by cc_code) c full outer join (select cc_code,sum(isnull(credit,0))Credit from Cash_book where convert(datetime,Modifiedby_Date) between '04/01/'+'" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/'+'" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'  group by cc_code) ch on c.cc_code=ch.cc_code)", con);
                da.Fill(ds, "fillgrid2");
                GridView2.DataSource = ds.Tables["fillgrid2"];
                GridView2.DataBind();
                trexcel.Visible = true;
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("style", "cursor: pointer");
            int Prevyear = 0;
            int Prevyear1 = -1;
            DataTable objdt = ViewState["Columns"] as DataTable;
            for (int i = 2; i < e.Row.Cells.Count - 1; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                if (i == 2)
                {
                    e.Row.Cells[i].Attributes.Add("onclick", "window.open('CashDebitSummaryReport.aspx?DCACode=" + e.Row.Cells[0].Text + "&Year=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "&Year1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "&PrevYear=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + " &PrevYear1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                }
                else
                {
                    e.Row.Cells[i].Attributes.Add("onclick", "window.open('CashDebitSummaryReport.aspx?DCACode=" + e.Row.Cells[0].Text + "&Year=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "&Year1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "&PrevYear=" + objdt.Columns[i].ToString().Substring(0, 4) + " &PrevYear1=20" + objdt.Columns[i].ToString().Substring(5, 2) + "','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                }
                Prevyear = Prevyear + 1;
                Prevyear1 = Prevyear1 + 1;
            }
            e.Row.Cells[e.Row.Cells.Count - 1].HorizontalAlign = HorizontalAlign.Right;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            for (int i = 2; i < e.Row.Cells.Count; i++)
            {

                e.Row.Cells[i].Text = String.Format("Rs. {0:#,##,##,###.00}", Convert.ToInt32(ds.Tables[0].Compute("SUM([" + ds.Tables[0].Columns[i].ColumnName + "])", string.Empty)));

            }
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string s = ddlyear.SelectedValue + ".xls";
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", s));
        Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.IO.StringWriter sw =
         new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw =
           new System.Web.UI.HtmlTextWriter(sw);
        Panel1.RenderControl(htw);
        GridView1.AllowPaging = false;
        GridView1.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
        GridView1.DataBind();
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
        }
        int j = 1;
        //This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            gvrow.BackColor = System.Drawing.Color.White;
            if (j <= GridView1.Rows.Count)
            {
                if (j % 2 != 0)
                {
                    for (int k = 0; k < gvrow.Cells.Count; k++)
                    {
                        gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
                    }
                }
            }
            j++;
        }

        Response.Write(sw.ToString());
        Response.End();
    }

}
