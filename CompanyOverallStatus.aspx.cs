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

public partial class CompanyOverallStatus : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    SqlDataAdapter da1 = null;
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
            tblexpences.Style.Add("visibility", "hidden");
            trexcel.Visible = false;
            year.Visible = false;
            datesearch.Visible = false;
            date.Visible = false;
        }
    }
   
    public void LoadYear()
    {
        for (int i = 2010; i <= System.DateTime.Now.Year; i++)
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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid2 = (GridView)sender;
            GridViewRow HeaderRow2 = new GridViewRow(-1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header2 = new TableCell();
            Cell_Header2.Text = "CONSOLIDATE CASH FLOW SUMMARY";
            Cell_Header2.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header2.ColumnSpan = 11;
            Cell_Header2.RowSpan = 1;
            HeaderRow2.Cells.Add(Cell_Header2);

            GridView1.Controls[0].Controls.AddAt(0, HeaderRow2);

            GridView HeaderGrid3 = (GridView)sender;
            GridViewRow HeaderRow3 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header3 = new TableCell();
            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "DCA Name";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 2;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "DCA Code";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            HeaderRow3.Cells.Add(Cell_Header3);
            Cell_Header3.RowSpan = 2;

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Paid";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 3;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Payable";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 3;
            HeaderRow3.Cells.Add(Cell_Header3);



            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Total";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 3;
            HeaderRow3.Cells.Add(Cell_Header3);
            GridView1.Controls[0].Controls.AddAt(1, HeaderRow3);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Cash += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cash"));
            Cheque += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cheque"));
            PaidTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PaidTotal"));
            CashPending += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CashPending"));
            ChequePending += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ChequePending"));
            PendingTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingTotal"));
            CashTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cash"))) + (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CashPending")));
            ChequeTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cheque"))) + (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ChequePending")));
            GrandTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total")));

            e.Row.Attributes.Add("style", "cursor: pointer");
            if (e.Row.Cells[1].Text != "DCA-39")
            {
                if (txtfrom.Text == "" && txtto.Text == "")
                {
                    e.Row.Attributes.Add("onclick", "window.open('DCAsummary.aspx?CCCode=0 &DCACode=" + e.Row.Cells[1].Text + "&Year=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + " &Year1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "&Type=2&SubType=1&Datetype=0','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                }
                else if (txtfrom.Text != "" && txtto.Text != "")
                {
                    if (ddldatesearch.SelectedItem.Text == "Invoice Date")
                        e.Row.Attributes.Add("onclick", "window.open('DCAsummary.aspx?CCCode=0 &DCACode=" + e.Row.Cells[1].Text + "&Year=" + txtfrom.Text + " &Year1=" + txtto.Text + "&Type=2&SubType=2&Datetype=1','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                    else if (ddldatesearch.SelectedItem.Text == "Paid Date")
                        e.Row.Attributes.Add("onclick", "window.open('DCAsummary.aspx?CCCode=0 &DCACode=" + e.Row.Cells[1].Text + "&Year=" + txtfrom.Text + " &Year1=" + txtto.Text + "&Type=2&SubType=2&Datetype=2','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                }
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = Cash.ToString();
            e.Row.Cells[3].Text = Cheque.ToString();
            e.Row.Cells[4].Text = PaidTotal.ToString();
            e.Row.Cells[5].Text = CashPending.ToString();
            e.Row.Cells[6].Text = ChequePending.ToString();
            e.Row.Cells[7].Text = PendingTotal.ToString();
            e.Row.Cells[8].Text = CashTotal.ToString();
            e.Row.Cells[9].Text = ChequeTotal.ToString();
            e.Row.Cells[10].Text = GrandTotal.ToString();
        }
      
    }
    private decimal Cash = (decimal)0.0;
    private decimal Cheque = (decimal)0.0;
    private decimal PaidTotal = (decimal)0.0;
    private decimal CashPending = (decimal)0.0;
    private decimal ChequePending = (decimal)0.0;
    private decimal PendingTotal = (decimal)0.0;
    private decimal CashTotal = (decimal)0.0;
    private decimal ChequeTotal = (decimal)0.0;
    private decimal GrandTotal = (decimal)0.0;



    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
      
            da = new SqlDataAdapter("Cost Center Status_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (txtfrom.Text == "" && txtto.Text == "")
            {
                da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
                da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
                da.SelectCommand.Parameters.AddWithValue("@SubType", SqlDbType.VarChar).Value = "1";

            }
            else if (txtfrom.Text != "" && txtto.Text != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = txtfrom.Text;
                da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = txtto.Text;
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
                if (ddldatesearch.SelectedItem.Text == "Invoice Date")
                    da.SelectCommand.Parameters.AddWithValue("@SubType", SqlDbType.VarChar).Value = "2";
                else if (ddldatesearch.SelectedItem.Text == "Paid Date")
                    da.SelectCommand.Parameters.AddWithValue("@SubType", SqlDbType.VarChar).Value = "3";
                da.SelectCommand.Parameters.AddWithValue("@FY", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
            }
            da.Fill(ds, "CCStatus");
            if (ds.Tables["CCStatus"].Rows[0][0].ToString() == "Two dates are not with in one financial year")
            {
                JavaScript.Alert("Two dates are not with in one financial year");
                GridView1.DataSource = null;
                GridView1.DataBind();

                //ReciptValue();
                tblexpences.Style.Add("visibility", "hidden");
                trexcel.Visible = false;

            }
            else
            {
                GridView1.DataSource = ds.Tables["CCStatus"];
                GridView1.DataBind();

                ReciptValue();
                tblexpences.Style.Add("visibility", "visible");
                trexcel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }


    public void ReciptValue()
    {
        ddlReceivables.Items.Clear();

        da = new SqlDataAdapter("Cost Center OverHead_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        if (txtfrom.Text == "" && txtto.Text == "")
        {
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();

            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
            da.SelectCommand.Parameters.AddWithValue("@SubType", SqlDbType.VarChar).Value = "1";
        }
        else if (txtfrom.Text != "" && txtto.Text != "")
        {
            da.SelectCommand.Parameters.AddWithValue("@FromDate", SqlDbType.VarChar).Value = txtfrom.Text;
            da.SelectCommand.Parameters.AddWithValue("@ToDate", SqlDbType.VarChar).Value = txtto.Text;
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
            da.SelectCommand.Parameters.AddWithValue("@SubType", SqlDbType.VarChar).Value = "2";
        }
        da.Fill(ds, "CreditInfo");
        ViewState["Basic"] = ds.Tables["CreditInfo"].Rows[0][0].ToString().Replace(".0000", ".00");
        ViewState["Retention"] = ds.Tables["CreditInfo"].Rows[1][0].ToString().Replace(".0000", ".00");
        ViewState["Tds"] = ds.Tables["CreditInfo"].Rows[2][0].ToString().Replace(".0000", ".00");
        ViewState["Hold"] = ds.Tables["CreditInfo"].Rows[3][0].ToString().Replace(".0000", ".00");
        ViewState["CreditPending"] = Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[4][0].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[11][0].ToString());
        ViewState["Asset"] = ds.Tables["CreditInfo"].Rows[5][0].ToString().Replace(".0000", ".00");
        ViewState["Loan"] = ds.Tables["CreditInfo"].Rows[6][0].ToString().Replace(".0000", ".00");
        ViewState["FD"] = ds.Tables["CreditInfo"].Rows[7][0].ToString().Replace(".0000", ".00");
        ViewState["cash"] = (Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[8][0].ToString()) - Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[9][0].ToString())).ToString();
        ViewState["Invoice"] = ds.Tables["CreditInfo"].Rows[10][0].ToString().Replace(".0000", ".00");
        ViewState["WCT"] = ds.Tables["CreditInfo"].Rows[12][0].ToString().Replace(".0000", ".00");

        Label11.Text = ViewState["Basic"].ToString();
        Label2.Text = ds.Tables["CreditInfo"].Rows[13][0].ToString().Replace(".0000", ".00");
        Label5.Text = Convert.ToDecimal(Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[15][0].ToString().Replace(".0000", ".00")) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[16][0].ToString().Replace(".0000", ".00"))).ToString();
        Label10.Text = (Convert.ToDecimal(ViewState["Basic"].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[13][0].ToString().Replace(".0000", ".00")) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[15][0].ToString().Replace(".0000", ".00")) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[16][0].ToString().Replace(".0000", ".00"))).ToString();
        Label13.Text = ds.Tables["CreditInfo"].Rows[14][0].ToString().Replace(".0000", ".00");
        Label15.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Basic"].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[13][0].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[15][0].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[16][0].ToString())) - Convert.ToDecimal(Convert.ToDecimal(GridView1.FooterRow.Cells[10].Text) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[14][0].ToString()))).ToString();
        ddlReceivables.Items.Insert(0, "Recieved:" + ViewState["Invoice"].ToString() + "");

        ddlReceivables.Items.Insert(1, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
        ddlReceivables.Items.Insert(2, "Retention:" + ViewState["Retention"].ToString() + "");
        ddlReceivables.Items.Insert(3, "Tds:" + ViewState["Tds"].ToString() + "");
        ddlReceivables.Items.Insert(4, "Hold:" + ViewState["Hold"].ToString() + "");
        ddlReceivables.Items.Insert(5, "WCT:" + ViewState["WCT"].ToString() + "");
        //ddlReceivables.Items.Insert(4, "Asset:" + ViewState["Asset"].ToString() + "");
        //ddlReceivables.Items.Insert(5, "Unsecured Loan:" + ViewState["Loan"].ToString() + "");
        //ddlReceivables.Items.Insert(6, "FD:" + ViewState["FD"].ToString() + "");
        //ddlReceivables.Items.Insert(7, "Cash:" + ViewState["cash"].ToString() + "");
        //viewinternalcashflow.Attributes.Add("onclick", "window.open('InternalCashFlowDetails.aspx?year1=" + ViewState["Yr1"].ToString() + " &year2=" + ViewState["Yr2"].ToString() + " &Type=2','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string s = ddlyear.SelectedItem.Text + " Company Overall Status.xls";
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", s));
        Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
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

    protected void ddlrpttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrpttype.SelectedItem.Text == "Yearwise")
        {
            year.Visible = true;
            datesearch.Visible = false;
            date.Visible = false;
        }
        else if (ddlrpttype.SelectedItem.Text == "Datewise")
        {
            year.Visible = false;
            datesearch.Visible = true;
            date.Visible = true;
        }
    }
}
