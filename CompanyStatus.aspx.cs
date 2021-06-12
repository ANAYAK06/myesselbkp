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
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;


public partial class CompanyStatus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    SqlDataAdapter da1 = null;
    DataView dv;
    //DataRow dr;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       //esselDal RoleCheck = new esselDal();
       // int rec = 0;
       //  rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 52);
       // if (rec == 0)
       //     Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            //ddlPrevReceivables.Attributes.Add("onclick", "extender(this.options[this.selectedIndex].value);");
            //ddlReceivables.Attributes.Add("onchange", "alert(this.options[this.selectedIndex].value);");
            //ddlPrevReceivables.Attributes.Add("onchange", "alert(this.options[this.selectedIndex].value);");
            //ddltotal.Attributes.Add("onchange", "alert(this.options[this.selectedIndex].value);");
            LoadYear();
         
            tblexpences.Style.Add("visibility", "hidden");
            trexcel.Style.Add("visibility", "hidden");
            Button1.Style.Add("visibility", "hidden");
           // viewinternalcashflow.Style.Add("visibility", "hidden"); 
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
        ddlyear.Items.Insert(0, "Any Year");
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlcccode.SelectedValue == "CC-03" || ddlcccode.SelectedValue == "CC-07" || ddlcccode.SelectedValue == "CC-12" || ddlcccode.SelectedValue == "CC-33")
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
                Cell_Header3.RowSpan = 3;
                HeaderRow3.Cells.Add(Cell_Header3);

                Cell_Header3 = new TableCell();
                Cell_Header3.Text = "DCA Code";
                Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header3.ColumnSpan = 1;
                HeaderRow3.Cells.Add(Cell_Header3);
                Cell_Header3.RowSpan = 3;

                Cell_Header3 = new TableCell();
                Cell_Header3.Text = "Current Year " + ddlyear.SelectedItem.Text;
                Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header3.ColumnSpan = 9;
                HeaderRow3.Cells.Add(Cell_Header3);
                Cell_Header3.RowSpan = 1;


             

                GridView1.Controls[0].Controls.AddAt(1, HeaderRow3);

                GridView HeaderGrid4 = (GridView)sender;
                GridViewRow HeaderRow4 = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Cell_Header4 = new TableCell();

                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Paid";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 3;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 1;

                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Payable";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 3;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 1;


                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Total";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 3;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 1;
             


                GridView1.Controls[0].Controls.AddAt(2, HeaderRow4);
            }
            else
            {
                GridView HeaderGrid2 = (GridView)sender;
                GridViewRow HeaderRow2 = new GridViewRow(-1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Cell_Header2 = new TableCell();
                Cell_Header2.Text = "CONSOLIDATE CASH FLOW SUMMARY";
                Cell_Header2.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header2.ColumnSpan = 15;
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
                Cell_Header3.RowSpan = 3;
                HeaderRow3.Cells.Add(Cell_Header3);

                Cell_Header3 = new TableCell();
                Cell_Header3.Text = "DCA Code";
                Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header3.ColumnSpan = 1;
                HeaderRow3.Cells.Add(Cell_Header3);
                Cell_Header3.RowSpan = 3;

                Cell_Header3 = new TableCell();
                Cell_Header3.Text = "Current Year " + ddlyear.SelectedItem.Text;
                Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header3.ColumnSpan = 9;
                HeaderRow3.Cells.Add(Cell_Header3);
                Cell_Header3.RowSpan = 1;


                Cell_Header3 = new TableCell();
                Cell_Header3.Text = "Up To Previous Year";
                Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header3.ColumnSpan = 4;
                HeaderRow3.Cells.Add(Cell_Header3);
                Cell_Header3.RowSpan = 1;



                GridView1.Controls[0].Controls.AddAt(1, HeaderRow3);

                GridView HeaderGrid4 = (GridView)sender;
                GridViewRow HeaderRow4 = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Cell_Header4 = new TableCell();
                Cell_Header4 = new TableCell();

                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Paid";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 3;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 1;

                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Payable";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 3;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 1;


                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Total";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 3;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 1;

                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Paid";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 1;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 2;

                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Payable";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 1;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 2;


                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Total";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 1;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 2;

                Cell_Header4 = new TableCell();
                Cell_Header4.Text = "Cu Total";
                Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header4.ColumnSpan = 1;
                HeaderRow4.Cells.Add(Cell_Header4);
                Cell_Header4.RowSpan = 2;


                GridView1.Controls[0].Controls.AddAt(2, HeaderRow4);

            }
        }
      

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Cash += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PCash"));
            Cheque += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PCheque"));
            PaidTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PPaidTotal"));
            CashPending += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PCashPending"));
            ChequePending += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PChequePending1"));
            PendingTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingTotal"));
            CashTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PCash"))) +(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PCashPending")));
            ChequeTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PCheque"))) + (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PChequePending1")));
            GrandTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pTotal")));
            LastPaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PrevPaid"));
            LastPayable += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PrevPayable"));
            LastTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LastTotal"));
            CuTotal = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pTotal"))) + (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LastTotal")));         
            CumulativeTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pTotal"))) + (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LastTotal")));
            Label lblcu = (Label)e.Row.FindControl("lblTotal4");
            lblcu.Text = CuTotal.ToString();
            //if (e.Row.Cells[1].Text != "OVRHD" && e.Row.Cells[1].Text != "DEPRCN")
            //{
            //    CuurentTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pTotal")));
            //    UptoPreviousTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LastTotal"));
            //    EntireTotal += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pTotal"))) + (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LastTotal")));
            //}
            e.Row.Attributes.Add("style", "cursor: pointer");
            e.Row.Attributes.Add("onclick", "window.open('DCAsummary.aspx?CCCode=" + ViewState["ccode"].ToString() + " &DCACode=" + e.Row.Cells[1].Text + "&Year=" + ViewState["Yr1"].ToString() + " &Year1=" + ViewState["Yr2"].ToString() + "&Type=1','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
            if (ddlcccode.SelectedValue == "CC-03" || ddlcccode.SelectedValue == "CC-07" || ddlcccode.SelectedValue == "CC-12" || ddlcccode.SelectedValue == "CC-33")
            {
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
            }
            else
            {
                e.Row.Cells[11].Visible = true;
                e.Row.Cells[12].Visible = true;
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblprevpayable");

            e.Row.Cells[2].Text = Cash.ToString();
            e.Row.Cells[3].Text = Cheque.ToString();
            e.Row.Cells[4].Text = PaidTotal.ToString();
            e.Row.Cells[5].Text = CashPending.ToString();
            e.Row.Cells[6].Text = ChequePending.ToString();
            e.Row.Cells[7].Text = PendingTotal.ToString();
            e.Row.Cells[8].Text = CashTotal.ToString();
            e.Row.Cells[9].Text = ChequeTotal.ToString();
            e.Row.Cells[10].Text = GrandTotal.ToString();
            if (ddlcccode.SelectedValue == "CC-03" || ddlcccode.SelectedValue == "CC-07" || ddlcccode.SelectedValue == "CC-12" || ddlcccode.SelectedValue == "CC-33")
            {
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
            }
            else
            {
                e.Row.Cells[11].Visible = true;
                e.Row.Cells[12].Visible = true;
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
                e.Row.Cells[11].Text = LastPaid.ToString();
                lbl.Text = LastPayable.ToString();
                e.Row.Cells[13].Text = LastTotal.ToString();
                e.Row.Cells[14].Text = CumulativeTotal.ToString();
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (ddlcccode.SelectedValue == "CC-03" || ddlcccode.SelectedValue == "CC-07" || ddlcccode.SelectedValue == "CC-12" || ddlcccode.SelectedValue == "CC-33")
            {
            }
            else
            {
                Label l = (Label)e.Row.FindControl("lblprevpayable");
                PrevPayables = Convert.ToDecimal(Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[0][0].ToString()) + Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[1][0].ToString()) + Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[2][0].ToString())) - Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[3][0].ToString());
                PrevTds = Convert.ToDecimal(l.Text) - PrevPayables;
                l.Attributes.Add("onmouseover", "showDetails(event,'" + PrevPayables + "','" + PrevTds + "','" + PrevRetention + "','" + PrevHold + "','" + PrevAnyother + "','" + PrevAdvance + "','" + PrevNetDue + "','1')");
                //l.Attributes.Add("onmouseover", "showpayableDetails(event,'" + PrevPayables + "'," + PrevNetDue + "','1')");
                l.Attributes.Add("onmouseout", "hideTooltip(event)");
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            try
            {
                //ReciptValue();
                GridViewRow rowFooter = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal);
                Literal newCells = new Literal();

                //newCells.Text = "Non Perform Cost Centers Expences:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Nonperformbank"].ToString()) + Convert.ToDecimal(ViewState["Nonperformcash"].ToString()) + Convert.ToDecimal(ViewState["Nonperformcash1"].ToString()) + Convert.ToDecimal(ViewState["Nvendorpayment"].ToString())) + ",Performing CostCenters Expences:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["performbank"].ToString()) + Convert.ToDecimal(ViewState["performcash"].ToString()) + Convert.ToDecimal(ViewState["performcash1"].ToString()) + Convert.ToDecimal(ViewState["performcash2"].ToString()) + Convert.ToDecimal(ViewState["Pvendorpayment"].ToString())) + "";
                    //,Recievable: '"+ Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString())+Convert.ToDecimal(ViewState["Retention"].ToString()))+'"";
                TableCell footerCell = new TableCell();
                footerCell.Controls.Add(newCells);
                footerCell.ColumnSpan = e.Row.Cells.Count;
                rowFooter.Cells.Add(footerCell);
                rowFooter.Visible = true;
                footerCell.HorizontalAlign = HorizontalAlign.Center;
                GridView1.Controls[0].Controls.AddAt(GridView1.Controls[0].Controls.Count, rowFooter);

                GridViewRow rowFooter1 = new GridViewRow(1, 0, DataControlRowType.Footer, DataControlRowState.Normal);
                Literal newCells1 = new Literal();
                //newCells1.Text ="Net Status :" + (Convert.ToDecimal(ViewState["Credit"].ToString()) - Convert.ToDecimal(e.Row.Cells[4].Text.Replace(".0000", "")));
                //newCells1.Text = "Net Reciept: " + ViewState["Invoice"].ToString() + ",Total Recievable: " + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())) + ", Invoice Recievable: " + ViewState["CreditPending"].ToString() + ",Retention:" + ViewState["Retention"].ToString() + ",Tds:" + ViewState["Tds"].ToString() + " ,Hold:" + ViewState["Hold"].ToString() + "";

                TableCell footerCell1 = new TableCell();
                footerCell1.Controls.Add(newCells1);
                footerCell1.ColumnSpan = e.Row.Cells.Count;
                rowFooter1.Cells.Add(footerCell1);
                rowFooter1.Visible = true;
                footerCell1.HorizontalAlign = HorizontalAlign.Center;

                GridView1.Controls[0].Controls.AddAt(GridView1.Controls[0].Controls.Count, rowFooter1);


                GridViewRow rowFooter2 = new GridViewRow(2, 1, DataControlRowType.Footer, DataControlRowState.Normal);
                Literal newCells2 = new Literal();
                //decimal i = Convert.ToDecimal(e.Row.Cells[4].Text.Replace("Rs.", "").Replace(",", "").Replace(".00", "")) - (Convert.ToDecimal(ds1.Tables["OverHead"].Rows[0].ItemArray[0]) + Convert.ToDecimal(ds1.Tables["OverHead"].Rows[0].ItemArray[1]));
                //newCells2.Text = "Gross Status:" + Convert.ToDecimal((Convert.ToDecimal(ViewState["Credit"].ToString()) + Convert.ToDecimal(ViewState["CreditPending"].ToString())) - (Convert.ToDecimal(e.Row.Cells[10].Text.Replace(".0000", ""))));
                TableCell footerCell2 = new TableCell();
                footerCell2.Controls.Add(newCells2);
                footerCell2.ColumnSpan = e.Row.Cells.Count;
                rowFooter2.Cells.Add(footerCell2);
                rowFooter2.Visible = true;
                footerCell2.HorizontalAlign = HorizontalAlign.Center;
                GridView1.Controls[0].Controls.AddAt(GridView1.Controls[0].Controls.Count, rowFooter2);

            }


            catch (Exception ex)
            {
                Utilities.CatchException(ex);
                JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
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
    private decimal ChequeTotal = (decimal)0.0;
    private decimal GrandTotal = (decimal)0.0;
    private decimal CuTotal = (decimal)0.0;
    private decimal LastPaid = (decimal)0.0;
    private decimal LastPayable = (decimal)0.0;
    private decimal LastTotal = (decimal)0.0;
    private decimal CumulativeTotal = (decimal)0.0;

    private decimal pTotal = (decimal)0.0;
    private decimal UptoPrevTotal = (decimal)0.0;
    private decimal EntireTotal = (decimal)0.0;


    
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            PrevRecieveandPaybles();
            da = new SqlDataAdapter("Cost Center Status_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@cccode", SqlDbType.DateTime).Value = ddlcccode.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";

            da.Fill(ds, "CCStatus");
            ViewState["ccode"] = ddlcccode.SelectedValue;
            ViewState["Yr1"] = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            ViewState["Yr2"] = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            pTotal = Convert.ToDecimal(ds.Tables["CCStatus"].Compute("sum(pTotal)", string.Empty));
            UptoPrevTotal = Convert.ToDecimal(ds.Tables["CCStatus"].Compute("sum(LastTotal)", string.Empty));
            EntireTotal = Convert.ToDecimal(ds.Tables["CCStatus"].Compute("sum(pTotal)", string.Empty)) + Convert.ToDecimal(ds.Tables["CCStatus"].Compute("sum(LastTotal)", string.Empty));
            //GridView1.DataSource = ds.Tables["CCStatus"];
            //GridView1.DataBind();
            ReciptValue();
            PreviousExpences();
            if (ddlcccode.SelectedValue != "CC-03" && ddlcccode.SelectedValue != "CC-07" && ddlcccode.SelectedValue != "CC-12" && ddlcccode.SelectedValue != "CC-33" && ddlcccode.SelectedValue != "CCC")
            {
                Dep();
                DataRow newRow = ds.Tables["CCStatus"].NewRow();
                newRow["dca_name"] = "Company Over heads";
                newRow["dca"] = "OVRHD";
                newRow["pcash"] = 0;
                newRow["pcheque"] = 0;
                newRow["ppaidTotal"] = 0;
                newRow["pCashpending"] = 0;
                newRow["pChequepending1"] = 0;
                newRow["PendingTotal"] = 0;
                newRow["pCashTotal"] = 0;
                newRow["pChequeTotal"] = 0;
                newRow["pTotal"] = Convert.ToDecimal(ViewState["Label11"].ToString()).ToString();
                newRow["PrevPaid"] = 0;
                newRow["PrevPayable"] = 0;
                newRow["LastTotal"] = Convert.ToDecimal(PrevOverhead.ToString()).ToString();
                ds.Tables["CCStatus"].Rows.Add(newRow);
                DataRow newRow1 = ds.Tables["CCStatus"].NewRow();
                newRow1["dca_name"] = "Depreciation On Asset at PCC";
                newRow1["dca"] = "DEPRCN";
                newRow1["pcash"] = 0;
                newRow1["pcheque"] = 0;
                newRow1["ppaidTotal"] = 0;
                newRow1["pCashpending"] = 0;
                newRow1["pChequepending1"] = 0;
                newRow1["PendingTotal"] = 0;
                newRow1["pCashTotal"] = 0;
                newRow1["pChequeTotal"] = 0;
                newRow1["pTotal"] = Convert.ToDecimal(ViewState["CurrentDep"]).ToString();
                newRow1["PrevPaid"] = 0;
                newRow1["PrevPayable"] = 0;
                newRow1["LastTotal"] = Convert.ToDouble(ViewState["PrevDep"]).ToString();
                ds.Tables["CCStatus"].Rows.Add(newRow1);

                GridView1.DataSource = ds.Tables["CCStatus"];
                GridView1.DataBind();
                Label20.Text = (Convert.ToDecimal(Label7.Text) - Convert.ToDecimal(GridView1.FooterRow.Cells[14].Text)).ToString();
                Label13.Text = (Convert.ToDecimal(Label3.Text) - Convert.ToDecimal(GridView1.FooterRow.Cells[10].Text)).ToString();
                Label14.Text = (Convert.ToDecimal(Label4.Text) - Convert.ToDecimal(GridView1.FooterRow.Cells[13].Text)).ToString();


                tblexpences.Style.Add("visibility", "visible");
              
                Button1.Style.Add("visibility", "visible");
                //viewinternalcashflow.Style.Add("visibility", "visible");
            }
            else
            {
                GridView1.DataSource = ds.Tables["CCStatus"];
                GridView1.DataBind();
            }
            trexcel.Style.Add("visibility", "visible");
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
        ddlPrevReceivables.Items.Clear();
        //ddloverhead.Items.Clear();
        ddltotal.Items.Clear();
        da = new SqlDataAdapter("Cost Center OverHead_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.DateTime).Value = ddlcccode.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";

        da.Fill(ds, "CreditInfo");
        ViewState["Invoice"] = ds.Tables["CreditInfo"].Rows[0][0].ToString().Replace(".0000",".00");
        ViewState["Retention"] = ds.Tables["CreditInfo"].Rows[1][0].ToString().Replace(".0000", ".00");
        ViewState["Tds"] = ds.Tables["CreditInfo"].Rows[2][0].ToString().Replace(".0000", ".00");
        ViewState["Hold"] = ds.Tables["CreditInfo"].Rows[3][0].ToString().Replace(".0000", ".00");
        ViewState["CreditPending"] = Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[4][0].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[28][0].ToString());
        ViewState["Nonperformbank"] = ds.Tables["CreditInfo"].Rows[5][0].ToString().Replace(".0000", ".00");
        ViewState["Nonperformcash"] = ds.Tables["CreditInfo"].Rows[6][0].ToString().Replace(".0000", ".00");
        ViewState["Nonperformcash1"] = ds.Tables["CreditInfo"].Rows[7][0].ToString().Replace(".0000", ".00");
        ViewState["performbank"] = ds.Tables["CreditInfo"].Rows[8][0].ToString().Replace(".0000", ".00");
        ViewState["performcash"] = ds.Tables["CreditInfo"].Rows[9][0].ToString().Replace(".0000", ".00");
        ViewState["performcash1"] = ds.Tables["CreditInfo"].Rows[10][0].ToString().Replace(".0000", ".00");
        ViewState["performcash2"] = ds.Tables["CreditInfo"].Rows[11][0].ToString().Replace(".0000", ".00");
        ViewState["Pvendorpayment"] = ds.Tables["CreditInfo"].Rows[12][0].ToString().Replace(".0000", ".00");
        ViewState["Nvendorpayment"] = ds.Tables["CreditInfo"].Rows[13][0].ToString().Replace(".0000", ".00");
        ViewState["PrevInvoice"] = ds.Tables["CreditInfo"].Rows[14][0].ToString().Replace(".0000", ".00");
        ViewState["PrevRetention"] = ds.Tables["CreditInfo"].Rows[15][0].ToString().Replace(".0000", ".00");
        ViewState["PrevTds"] = ds.Tables["CreditInfo"].Rows[16][0].ToString().Replace(".0000", ".00");
        ViewState["PrevHold"] = ds.Tables["CreditInfo"].Rows[17][0].ToString().Replace(".0000", ".00");
        ViewState["PrevCreditPending"] = Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[18][0].ToString())+Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[29][0].ToString());
        ViewState["Nonperformcash2"] = ds.Tables["CreditInfo"].Rows[19][0].ToString().Replace(".0000", ".00");
        ViewState["Nonperformcash3"] = ds.Tables["CreditInfo"].Rows[20][0].ToString().Replace(".0000", ".00");
        ViewState["performcash3"] = ds.Tables["CreditInfo"].Rows[21][0].ToString().Replace(".0000", ".00");
        ViewState["CCbank"] = ds.Tables["CreditInfo"].Rows[22][0].ToString().Replace(".0000", ".00");
        ViewState["CCCash1"] = ds.Tables["CreditInfo"].Rows[23][0].ToString().Replace(".0000", ".00");
        ViewState["CCCash2"] = ds.Tables["CreditInfo"].Rows[24][0].ToString().Replace(".0000", ".00");
        ViewState["CCCash3"] = ds.Tables["CreditInfo"].Rows[25][0].ToString().Replace(".0000", ".00");
        ViewState["CCCash4"] = ds.Tables["CreditInfo"].Rows[26][0].ToString().Replace(".0000", ".00");
        ViewState["CCVendor"] = ds.Tables["CreditInfo"].Rows[27][0].ToString().Replace(".0000", ".00");
        ViewState["CurrentBasic"] = ds.Tables["CreditInfo"].Rows[30][0].ToString().Replace(".0000", ".00");
        ViewState["PrevBasic"] = ds.Tables["CreditInfo"].Rows[31][0].ToString().Replace(".0000", ".00");

        NonExpences = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Nonperformbank"].ToString()) + Convert.ToDecimal(ViewState["Nonperformcash"].ToString()) + Convert.ToDecimal(ViewState["Nonperformcash1"].ToString()) + Convert.ToDecimal(ViewState["Nvendorpayment"].ToString()) + Convert.ToDecimal(ViewState["Nonperformcash2"].ToString()) + Convert.ToDecimal(ViewState["Nonperformcash3"].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[32][0].ToString())));
        PerExpences = (Convert.ToDecimal(Convert.ToDecimal(ViewState["performbank"].ToString()) + Convert.ToDecimal(ViewState["performcash"].ToString()) + Convert.ToDecimal(ViewState["performcash1"].ToString()) + Convert.ToDecimal(ViewState["performcash2"].ToString()) + Convert.ToDecimal(ViewState["Pvendorpayment"].ToString()) + Convert.ToDecimal(ViewState["performcash3"].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[33][0].ToString())));

        //ddloverhead.Items.Insert(0, "");
        //ddloverhead.Items.Insert(1, "Non performing CC Expences:" +NonExpences.ToString() + "");
        //ddloverhead.Items.Insert(2, "Performing CC Expences:" +PerExpences.ToString() + "");
        OverHead(NonExpences.ToString(), PerExpences.ToString());
        //ddloverhead.Items.Insert(3, "OverHead:" + ViewState["CurrentOverHead"].ToString() + "");
        if (ddlyear.SelectedIndex != 1)
        {
           string overhead;
           if (PerExpences != 0)            
                 overhead = String.Format("{0:0.00}", (Convert.ToDecimal((NonExpences / PerExpences) * 100)));
            else
                 overhead = String.Format("{0:0.00}", (Convert.ToDecimal(0 * 100)));
                //Label2.Text = "CO. OVERHEAD @" + lbloverheadpercentage.Text + "% ON TOTAL OUT FLOW";
                CCAmount = (Convert.ToDecimal(Convert.ToDecimal(ViewState["CCbank"].ToString()) + Convert.ToDecimal(ViewState["CCCash1"].ToString()) + Convert.ToDecimal(ViewState["CCCash2"].ToString()) + Convert.ToDecimal(ViewState["CCCash3"].ToString()) + Convert.ToDecimal(ViewState["CCCash4"].ToString()) + Convert.ToDecimal(ViewState["CCVendor"].ToString()) + Convert.ToDecimal(ds.Tables["CreditInfo"].Rows[34][0].ToString())));
                Expence = (Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(pTotal) * Convert.ToDecimal(ViewState["CurrentOverHead"].ToString())) / 100));
                ViewState["Label11"] = Expence.ToString();
                ViewState["Label10"] = (Expence + Convert.ToDecimal(pTotal)).ToString();
                Button1.Attributes.Add("onclick", "return overhead('" + ViewState["CurrentOverHead"].ToString() + "')");
                //viewinternalcashflow.Attributes.Add("onclick", "window.open('InternalCashFlowDetails.aspx?cccode=" + ViewState["ccode"].ToString() + " &year1=" + ViewState["Yr1"].ToString() + " &year2=" + ViewState["Yr2"].ToString() + " &Type=1','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");

        }
        else if (ddlyear.SelectedIndex == 1)
        {

           // lbloverheadpercentage.Text = "15";
            //Label2.Text = "CO. OVERHEAD @15% ON TOTAL OUT FLOW";
            Expence = (Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(pTotal) * 15) / 100));
            ViewState["Label10"] = (Expence + Convert.ToDecimal(pTotal)).ToString();
            ViewState["Label11"] = Expence.ToString();
            Button1.Attributes.Add("onclick", "window.alert('The OverHead Percentage is 15');");

        }
        //Label11.Text = ViewState["Invoice"].ToString();
        //Label3.Text = ViewState["CurrentBasic"].ToString();
        //Label4.Text = ViewState["PrevBasic"].ToString();
        //Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
        if (ddlcccode.SelectedValue == "CC-32" && ddlyear.SelectedIndex != 1)
        {
            //Label11.Text = ViewState["Invoice"].ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 11784700).ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 11784700).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 8644742)).ToString();

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(11784700)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(11784700)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
           // viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-37" && ddlyear.SelectedIndex != 1)
        {
            //Label11.Text = ViewState["Invoice"].ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:"+Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 18422388).ToString()+"");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 18422388).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 8644742)).ToString();

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(18422388)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(18422388)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
           // viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-35" && ddlyear.SelectedIndex != 1)
        {
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 8644742).ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 8644742).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");


            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(8644742)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(8644742)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 18422388)).ToString();
        }
        else if (ddlcccode.SelectedValue == "CC-38" && ddlyear.SelectedIndex != 1)
        {
            //Label11.Text = ViewState["Invoice"].ToString();
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 3298689)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 3298689).ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 3298689).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(3298689)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(3298689)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;

        }
        else if (ddlcccode.SelectedValue == "CC-41" && ddlyear.SelectedIndex != 1)
        {
            //Label11.Text = ViewState["Invoice"].ToString();
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 3812158)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 3812158).ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 3812158).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(3812158)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(3812158)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
           // viewinternalcashflow.Visible = true;

        }
        else if (ddlcccode.SelectedValue == "CC-42" && ddlyear.SelectedIndex != 1)
        {
            //Label11.Text = ViewState["Invoice"].ToString();

            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 5763378)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 5763378).ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 5763378).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(5763378)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(5763378)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
           // viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-43" && ddlyear.SelectedIndex != 1)
        {
            //Label11.Text = ViewState["Invoice"].ToString();

            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 6742803)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 6742803).ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 6742803).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(6742803)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(6742803)).ToString();

            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-44" && ddlyear.SelectedIndex != 1)
        {
            //Label11.Text = ViewState["Invoice"].ToString();

            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 622465)).ToString();

            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 622465).ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");


            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 622465).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(622465)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(622465)).ToString();

            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-30" && ddlyear.SelectedIndex != 1)
        {
            //Label11.Text = ViewState["Invoice"].ToString();

            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 42698757)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 42698757).ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 42698757).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(42698757)).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(42698757)).ToString();

            tblexpences.Visible = true;
            Button1.Visible = true;
           // viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-32" && ddlyear.SelectedIndex == 1)
        {
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 11784700)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 8644742)).ToString();

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 11784700).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(11784700)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(11784700)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-37" && ddlyear.SelectedIndex == 1)
        {
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 18422388)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 8644742)).ToString();

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 18422388).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(18422388)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(18422388)).ToString();

            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-35" && ddlyear.SelectedIndex == 1)
        {
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 18422388)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 8644742)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");


            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 8644742).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(8644742)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(8644742)).ToString();

            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-38" && ddlyear.SelectedIndex == 1)
        {
            ddlReceivables.Items.Insert(0, "");
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 3298689)).ToString();
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 3298689)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 3298689).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(3298689)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(3298689)).ToString();

            tblexpences.Visible = true;
            Button1.Visible = true;
           // viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-41" && ddlyear.SelectedIndex == 1)
        {
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 3812158)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 3812158)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");
            
            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 3812158).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(3812158)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(3812158)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;

        }
        else if (ddlcccode.SelectedValue == "CC-42" && ddlyear.SelectedIndex == 1)
        {
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 5763378)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 5763378)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 5763378).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(5763378)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(5763378)).ToString();

            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;
        }
        else if (ddlcccode.SelectedValue == "CC-43" && ddlyear.SelectedIndex == 1)
        {
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 6742803)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 6742803)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 6742803).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(6742803)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(6742803)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;

        }
        else if (ddlcccode.SelectedValue == "CC-44" && ddlyear.SelectedIndex == 1)
        {
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 622465)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 622465)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 622465).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(622465)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(622465)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;

        }
        else if (ddlcccode.SelectedValue == "CC-30" && ddlyear.SelectedIndex == 1)
        {
            //Label14.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 42698757)).ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + (Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + 42698757)).ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString()) + 42698757).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(42698757)).ToString();
            Label4.Text = (Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString()) + Convert.ToDecimal(42698757)).ToString();
            tblexpences.Visible = true;
            Button1.Visible = true;
            //viewinternalcashflow.Visible = true;

        }
        else if (ddlcccode.SelectedValue == "CC-03" || ddlcccode.SelectedValue == "CC-07" || ddlcccode.SelectedValue == "CC-12" || ddlcccode.SelectedValue == "CC-33")
        {
            tblexpences.Visible = false;
            Button1.Visible = false;
            //viewinternalcashflow.Visible = false;
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString())).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = ViewState["PrevBasic"].ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();


        }
        else if (ddlcccode.SelectedValue == "CCC")
        {
            //viewinternalcashflow.Visible = true;
            tblexpences.Visible = false;
            Button1.Visible = false;
        
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString())).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = ViewState["PrevBasic"].ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();


        }
        else
        {
            //Label11.Text = ViewState["Invoice"].ToString();
            //Label14.Text = ViewState["PrevInvoice"].ToString();
            ddlReceivables.Items.Insert(0, "");
            ddlReceivables.Items.Insert(1, "Recieved:" + ViewState["Invoice"].ToString() + "");
            ddlReceivables.Items.Insert(2, "Receivebles:" + ViewState["CreditPending"].ToString() + "");
            ddlReceivables.Items.Insert(3, "Retention:" + ViewState["Retention"].ToString() + "");
            ddlReceivables.Items.Insert(4, "Tds:" + ViewState["Tds"].ToString() + "");
            ddlReceivables.Items.Insert(5, "Hold:" + ViewState["Hold"].ToString() + "");

            ddlPrevReceivables.Items.Insert(0, "");
            ddlPrevReceivables.Items.Insert(1, "Recieved:" + ViewState["PrevInvoice"].ToString() + "");
            ddlPrevReceivables.Items.Insert(2, "Receivebles:" + ViewState["PrevCreditPending"].ToString() + "");
            ddlPrevReceivables.Items.Insert(3, "Retention:" + ViewState["PrevRetention"].ToString() + "");
            ddlPrevReceivables.Items.Insert(4, "Tds:" + ViewState["PrevTds"] + "");
            ddlPrevReceivables.Items.Insert(5, "Hold:" + ViewState["PrevHold"].ToString() + "");

            ddltotal.Items.Insert(0, "");
            ddltotal.Items.Insert(1, "Recieved:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(ViewState["PrevInvoice"].ToString())).ToString() + "");
            ddltotal.Items.Insert(2, "Receivebles:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevCreditPending"].ToString())).ToString() + "");
            ddltotal.Items.Insert(3, "Retention:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString())).ToString() + "");
            ddltotal.Items.Insert(4, "Tds:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString())).ToString() + "");
            ddltotal.Items.Insert(5, "Hold:" + Convert.ToDecimal(Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString())).ToString() + "");

            Label3.Text = ViewState["CurrentBasic"].ToString();
            Label4.Text = ViewState["PrevBasic"].ToString();
            Label7.Text = (Convert.ToDecimal(ViewState["CurrentBasic"].ToString()) + Convert.ToDecimal(ViewState["PrevBasic"].ToString())).ToString();

            tblexpences.Visible = true;
            Button1.Visible = true;
           // viewinternalcashflow.Visible = true;
        }

      

    }

    private void PreviousExpences()
    {


        for (int i = Convert.ToInt32(ddlyear.SelectedValue.Substring(0, 4)); i > 2009; i--)
        {
            da = new SqlDataAdapter("PreviousYears Gross Expences_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.DateTime).Value = ddlcccode.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = i.ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (i - 1).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";
            da.Fill(ds, "PrevInfo");
            Amount = Amount + Convert.ToDecimal(ds.Tables["PrevInfo"].Rows[0][0].ToString());
            Amount1 = Amount1 + Convert.ToDecimal(ds.Tables["PrevInfo"].Rows[0][0].ToString());
            PrevOverhead = PrevOverhead + Convert.ToDecimal(ds.Tables["PrevInfo"].Rows[0][1].ToString());
            ds.Tables["PrevInfo"].Reset();
        }
        //ViewState["Label16"] = Amount.ToString();
        //ViewState["Label5"] = PrevOverhead.ToString();

        Amount = (Convert.ToDecimal(ViewState["Label11"].ToString()) + Convert.ToDecimal(PrevOverhead.ToString()));

        //Label20.Text = (Convert.ToDecimal(Convert.ToDecimal(ViewState["PrevCreditPending"].ToString()) + Convert.ToDecimal(ViewState["PrevRetention"].ToString()) + Convert.ToDecimal(ViewState["PrevTds"].ToString()) + Convert.ToDecimal(ViewState["PrevHold"].ToString()) + Convert.ToDecimal(ViewState["CreditPending"].ToString()) + Convert.ToDecimal(ViewState["Retention"].ToString()) + Convert.ToDecimal(ViewState["Tds"].ToString()) + Convert.ToDecimal(ViewState["Hold"].ToString()) + Convert.ToDecimal(ViewState["Invoice"].ToString()) + Convert.ToDecimal(Label14.Text)) - Convert.ToDecimal(Label18.Text)).ToString();
        Label20.Text = (Convert.ToDecimal(Label7.Text) - Convert.ToDecimal((Amount1 + Convert.ToDecimal(ViewState["Label10"].ToString())))).ToString();
        Label13.Text = (Convert.ToDecimal(Label3.Text) - Convert.ToDecimal(ViewState["Label10"].ToString())).ToString();
        Label14.Text = (Convert.ToDecimal(Label4.Text) - Convert.ToDecimal(Amount1.ToString())).ToString();
    }
    private decimal Amount = (decimal)0.0;
    private decimal Amount1 = (decimal)0.0;
    private decimal CCAmount = (decimal)0.0;
    private decimal Expence = (decimal)0.0;
    private decimal NonExpences = (decimal)0.0;
    private decimal PerExpences = (decimal)0.0;
    private decimal PrevOverhead = (decimal)0.0;
    private decimal PrevRecieveables = (decimal)0.0;
    private decimal PrevPayables = (decimal)0.0;
    private decimal PrevTds = (decimal)0.0;
    private decimal PrevRetention = (decimal)0.0;
    private decimal PrevHold = (decimal)0.0;
    private decimal PrevAnyother = (decimal)0.0;
    private decimal PrevAdvance = (decimal)0.0;
    private decimal PrevNetDue = (decimal)0.0;
    public void PrevRecieveandPaybles()
    {
        da = new SqlDataAdapter("PreviousPayablesandRecieveables_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.DateTime).Value = ddlcccode.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
        da.Fill(ds, "PayablesandRecieveables");// PrevTds
        PrevRecieveables = Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[4][0].ToString().Replace(".0000",".00"));
        PrevRetention = Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[5][0].ToString().Replace(".0000", ".00"));
        PrevTds = Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[6][0].ToString().Replace(".0000", ".00"));
        PrevHold = Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[7][0].ToString().Replace(".0000", ".00"));
        PrevAnyother = Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[8][0].ToString().Replace(".0000", ".00"));
        PrevAdvance = Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[9][0].ToString().Replace(".0000", ".00"));
        PrevNetDue =(Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[10][0].ToString())+Convert.ToDecimal(ds.Tables["PayablesandRecieveables"].Rows[11][0].ToString()))- (PrevRecieveables + PrevTds + PrevRetention + PrevHold + PrevAnyother + PrevAdvance);
        if (ddlcccode.SelectedValue != "CC-03" && ddlcccode.SelectedValue != "CC-07" && ddlcccode.SelectedValue != "CC-12" && ddlcccode.SelectedValue != "CC-33" && ddlcccode.SelectedValue != "CCC")
        {
            ddlPrevReceivables.Attributes.Add("onmouseover", "showDetails(event,'" + PrevRecieveables + "','" + PrevTds + "','" + PrevRetention + "','" + PrevHold + "','" + PrevAnyother + "','" + PrevAdvance + "','" + PrevNetDue + "','2')");
            ddlPrevReceivables.Attributes.Add("onmouseout", "hideTooltip(event)");
        }
       
    }
    public void OverHead(string NonExpence, string PerExpence)
    {
        da = new SqlDataAdapter("OverHead_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@NonPerform", SqlDbType.Money).Value = NonExpence.ToString();
        da.SelectCommand.Parameters.AddWithValue("@Perform", SqlDbType.Money).Value = PerExpence.ToString();
        da.Fill(ds, "CurrentOverHead");
        ViewState["CurrentOverHead"] = ds.Tables["CurrentOverHead"].Rows[0][0].ToString();
    }
     private decimal depvalue = (decimal)0.0;
    public void Dep()
    {       

        da = new SqlDataAdapter("Asset_depreciation", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.DateTime).Value = ddlcccode.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@Fyyear", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "3";
        da.Fill(ds, "dep");
        if (ddlcccode.SelectedValue == "CC-38")
        {
            depvalue = Convert.ToDecimal(453554.5) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-41")
        {
            depvalue = Convert.ToDecimal(434282) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-42")
        {
            depvalue = Convert.ToDecimal(906643.5) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-43")
        {
            depvalue = Convert.ToDecimal(801335) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-44")
        {
            depvalue = Convert.ToDecimal(203105) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-45")
        {
            depvalue = Convert.ToDecimal(63264.5) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-49")
        {
            depvalue = Convert.ToDecimal(519636.5) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-50")
        {
            depvalue = Convert.ToDecimal(97872) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-51")
        {
            depvalue = Convert.ToDecimal(241825) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-52")
        {
            depvalue = Convert.ToDecimal(58314.5) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-53")
        {
            depvalue = Convert.ToDecimal(92345.5) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-54")
        {
            depvalue = Convert.ToDecimal(257680.209) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-57")
        {
            depvalue = Convert.ToDecimal(46175) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else if (ddlcccode.SelectedValue == "CC-58")
        {
            depvalue = Convert.ToDecimal(51685.147) + Convert.ToDecimal(ds.Tables["dep"].Rows[0][1].ToString());
            ViewState["PrevDep"] = depvalue;
        }
        else
            ViewState["PrevDep"] = ds.Tables["dep"].Rows[0][1].ToString();
      
           ViewState["CurrentDep"] = ds.Tables["dep"].Rows[0][0].ToString();

    }
    protected void btnWord_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.AllowPaging = false;
        GridView1.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
        GridView1.DataBind();
        Response.ClearContent();
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.pdf"));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Charset = "";
      
        Response.ContentType = "application/pdf";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
        Panel1.RenderControl(htw);
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string s=ddlcccode.SelectedValue+" Status.xls";
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
            gvrow.BackColor = Color.White;
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

    ////protected void viewinternalcashflow_Click(object sender, EventArgs e)
    ////{
    ////    ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('InternalCashFlowDetails.aspx?cccode=" + ViewState["ccode"].ToString() + " &year1=" + ViewState["Yr1"].ToString() + " &year2=" + ViewState["Yr2"].ToString() + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');", true);
    ////}
}
//select (case when c.DCA_code is not null then c.DCA_code  when ch.dca_code is not null then ch.dca_code else cas.DCA_code end ) dca,(isnull(cash,0)+isnull(cheque,0)) total,chk from (Select DCA_code,isnull(sum(balance),0) cash from pending_invoice where status='Debit Pending' group by dca_code) c full outer join (Select DCA_code,isnull(sum(debit),0) cheque from bankbook where status='Debit pending' group by dca_code) ch on c.dca_code=ch.dca_code full outer join (Select DCA_code,isnull(sum(debit),0) chk from cash_pending where status='pending' group by dca_code) cas on ch.dca_code=cas.dca_code or c.dca_code=cas.dca_code

//select cc_code,(case when c.DCA_code is null then ch.DCA_code else c.DCA_code end) dca,cash,cheque,(isnull(cash,0)+isnull(cheque,0)) total from (Select DCA_code,isnull(sum(debit),0) cash from cash_book group by dca_code) c full outer join (Select DCA_code,isnull(sum(debit),0) cheque from bankbook group by dca_code) ch on c.dca_code=ch.dca_code", con);

//select (case when s.dca is not null then dca else DCA_code end) dca,pending,cash,cheque,paid from ((select (case when c.DCA_code is null then ch.DCA_code else c.DCA_code end) dca,cash,cheque,(isnull(cash,0)+isnull(cheque,0)) paid from (Select DCA_code,isnull(sum(debit),0) cash from cash_book group by dca_code) c full outer join (Select DCA_code,isnull(sum(debit),0) cheque from bankbook group by dca_code) ch on c.dca_code=ch.dca_code)) s full outer join (Select DCA_code,isnull(sum(debit),0) pending from cash_pending where status='pending' group by dca_code) d on s.dca=d.dca_code order by dca
//select (case when s.dca is not null then dca else DCA_code end) dca,Cashpending,cash,cheque,paidTotal from ((select (case when c.DCA_code is null then ch.DCA_code else c.DCA_code end) dca,cash,cheque,(isnull(cash,0)+isnull(cheque,0)) PaidTotal from (Select DCA_code,isnull(sum(debit),0) cash from cash_book group by dca_code) c full outer join (Select DCA_code,isnull(sum(debit),0) cheque from bankbook group by dca_code) ch on c.dca_code=ch.dca_code)) s full outer join (Select DCA_code,isnull(sum(debit),0) Cashpending from cash_pending where status='pending' group by dca_code) d on s.dca=d.dca_code full outer join (Select (case when c.DCA_code is null then ch.DCA_code else c.DCA_code end) dca,isnull(sum(balance),0) cash from pending_invoice where status='Debit Pending' group by dca_code) c full outer join 
//(Select DCA_code,isnull(sum(debit),0) cheque from bankbook where status='Debit pending' group by dca_code)




//select (case when s.dca is not null then dca else DCA_code end) dca,Cashpending,cash,cheque,paidTotal,ChequePending from ((select (case when c.DCA_code is null then ch.DCA_code else c.DCA_code end) dca,cash,cheque,(isnull(cash,0)+isnull(cheque,0)) PaidTotal from (Select DCA_code,isnull(sum(debit),0) cash from cash_book group by dca_code) c full outer join (Select DCA_code,isnull(sum(debit),0) cheque from bankbook group by dca_code) ch on c.dca_code=ch.dca_code)) s full outer join (Select DCA_code,isnull(sum(debit),0) Cashpending from cash_pending where status='pending' group by dca_code) d on s.dca=d.dca_code full outer join ((Select (isnull(cashp,0)+isnull(chequep,0)) Chequepending from (select isnull(sum(debit),0) cashp from pending_invoice where status='Debit Pending' group by dca_code) chp full outer join 
//(Select DCA_code,isnull(sum(debit),0) chequep from bankbook where status='Debit pending' group by dca_code)chequep on chp.dca_code=cashp.dca_code))p on d.dca_code=p.dca_code



//select d.dca_name,res.* from (select (case when p.dca is not null then p.dca else pd.dca end) dca,Cash,Cheque,PaidTotal,CashPending,ChequePending,PendingTotal from (select (case when c.DCA_code is null then ch.DCA_code else c.DCA_code end) dca,Cash,Cheque,(isnull(cash,0)+isnull(cheque,0)) PaidTotal from (Select DCA_code,isnull(sum(debit),0) cash from cash_book group by dca_code) c full outer join (Select DCA_code,isnull(sum(debit),0) cheque from bankbook group by dca_code) ch on c.dca_code=ch.dca_code) p full outer join (select (case when cp.DCA_code is null then chp.DCA_code else cp.DCA_code end) dca,CashPending,ChequePending,(isnull(CashPending,0)+isnull(ChequePending,0)) PendingTotal from (Select DCA_code,isnull(sum(balance),0) CashPending from pending_invoice where status='Debit Pending' group by dca_code) cp full outer join (Select DCA_code,isnull(sum(debit),0) ChequePending from bankbook where status='Debit pending' group by dca_code) chp on cp.dca_code=chp.dca_code) pd on p.dca=pd.dca) res join dca d on res.dca=d.dca_code order by dca


//select d.dca_name,res.* from (select (case when p.dca is not null then p.dca else pd.dca end) dca,Cash,Cheque,PaidTotal,CashPending,ChequePending,PendingTotal from (select (case when c.DCA_code is null then ch.DCA_code else c.DCA_code end) dca,Cash,Cheque,(isnull(cash,0)+isnull(cheque,0)) PaidTotal from (Select DCA_code,isnull(sum(debit),0) cash from cash_book group by dca_code) c full outer join (Select DCA_code,isnull(sum(debit),0) cheque from bankbook group by dca_code) ch on c.dca_code=ch.dca_code) p full outer join (select (case when cp.DCA_code is null then chp.DCA_code else cp.DCA_code end) dca,CashPending,ChequePending,(isnull(CashPending,0)+isnull(ChequePending,0)) PendingTotal from (Select DCA_code,isnull(sum(balance),0) CashPending from pending_invoice where status='Debit Pending' group by dca_code) cp full outer join (Select DCA_code,isnull(sum(debit),0) ChequePending from bankbook where status='Debit pending' group by dca_code) chp on cp.dca_code=chp.dca_code) pd on p.dca=pd.dca) res join dca d on res.dca=d.dca_code order by dca


//select (case when d.cc is not null then d.cc else i.cc_code end) cc,BankDebit,CashDebit,Credit from
//(select (case when b.cc_Code is not null then b.cc_code else c.cc_code end) cc,cashdebit,bankdebit
//from (Select CC_Code,SUM(Debit) BankDebit from Bankbook where cc_code not in ('CC-07','CC-12','CC-03','CCC') group by CC_code) b full outer join
//(Select CC_Code,SUM(Debit)CashDebit from cash_book  where cc_code not in ('CC-07','CC-12','CC-03','CCC') group by CC_code) c on b.cc_code=c.cc_code) d full outer join
//(select cc_code,SUM(Amount) Credit from invoice where cc_code not in ('CC-07','CC-12','CC-03','CCC') group by CC_code) i on
//d.cc=i.cc_code


//select (select SUM(debit) from bankbook where cc_code not in ('CC-07','CC-12','CC-03','CCC')and CC) bankdebit,
//(select SUM(debit) from cash_book where cc_code not in ('CC-07','CC-12','CC-03','CCC')) cashkdebit,
//(Select SUM(Amount) from invoice where cc_code not in ('CC-07','CC-12','CC-03','CCC')) credit


//Select d.dca_name,V.* from
//(Select (case when U.dca is not null then U.dca else [2009-10 Expenc].dca_code end)dca,Replace(PCash2,'.0000','.00')[PCash3],Replace(PCheque2,'.0000','.00')[PCheque3],Replace(PPaidtotal2,'.0000','.00')PPaidtotal3,Replace(PCashPending2,'.0000','.00')[PCashPending3],Replace(PChequePending2,'.0000','.00')[PChequePending3],Replace(PPendingTotal2,'.0000','.00')PPendingTotal3,Replace(PCashTotal2,'.0000','.00')[PCashTotal3],Replace(PchequeTotal2,'.0000','.00')[PchequeTotal3],Replace(PTotal2,'.0000','.00')[PTotal3],Replace(isnull(Paid,0)+isnull([2009-10 Expence],0),'.0000','.00')[Paid1],Replace(LChequePending,'.0000','.00')[LChequePending1] from 
//(Select (case when M.dca is not null then M.dca else Lpe1.dca end)dca,Replace(PCash1,'.0000','.00')[PCash2],Replace(PCheque1,'.0000','.00')[PCheque2],Replace(PPaidtotal1,'.0000','.00')PPaidtotal2,Replace(PCashPending1,'.0000','.00')[PCashPending2],Replace(PChequePending1+isnull(ChequePending2,0),'.0000','.00')[PChequePending2],Replace(PPendingTotal1,'.0000','.00')PPendingTotal2,Replace(PCashTotal1,'.0000','.00')[PCashTotal2],Replace(PchequeTotal1,'.0000','.00')[PchequeTotal2],Replace(PTotal1,'.0000','.00')[PTotal2],Replace((isnull(LCash,0)+isnull(LPCash,0)+isnull(LCheque,0)),'.0000','.00')[Paid],Replace((isnull(LChequePending,0)+isnull(LChequePending1,0)),'.0000','.00')[LChequePending] from 
//(Select (case when N.dca is not null then N.dca else Lpe.dca end)dca,Replace(PCash,'.0000','.00')[PCash1],Replace(PCheque,'.0000','.00')[PCheque1],Replace(PPaidtotal,'.0000','.00')PPaidtotal1,Replace(PCashPending,'.0000','.00')[PCashPending1],Replace(PChequePending,'.0000','.00')[PChequePending1],Replace(PPendingTotal,'.0000','.00')PPendingTotal1,Replace(PCashTotal,'.0000','.00')[PCashTotal1],Replace(PchequeTotal,'.0000','.00')[PchequeTotal1],Replace(PTotal,'.0000','.00')[PTotal1],LCash,LPCash,LCheque,LChequePending from 
//(Select (case when S.dca is not null then S.dca else Lpd.dca end)dca,Replace(Cash,'.0000','.00')[PCash],Replace(Cheque,'.0000','.00')[PCheque],Replace(Paidtotal,'.0000','.00')PPaidtotal,Replace(CashPending,'.0000','.00')[PCashPending],Replace(ChequePending,'.0000','.00')[PChequePending],Replace(PendingTotal,'.0000','.00')PPendingTotal,Replace(CashTotal,'.0000','.00')[PCashTotal],Replace(chequeTotal,'.0000','.00')[PchequeTotal],Replace(Total,'.0000','.00')[PTotal],LCash,LPCash from 
//(Select (case when res.dca is not null then res.dca else rec.dca end)dca,Replace((isnull(Cash,0)+isnull(Cash1,0)),'.0000','.00')[Cash],Replace((isnull(Cheque,0)+isnull(ChequePending1,0)),'.0000','.00')[Cheque],Replace((isnull(Cash,0)+isnull(Cheque,0)+isnull(Cash1,0)+isnull(ChequePending1,0)),'.0000','.00')Paidtotal,Replace(isnull(CashPending,0),'.0000','.00')[CashPending],Replace((isnull(ChequePending,0)),'.0000','.00')[ChequePending],Replace((isnull(CashPending,0)+isnull(ChequePending,0)),'.0000','.00')PendingTotal,Replace((isnull(cash,0)+isnull(cashpending,0)+isnull(Cash1,0)),'.0000','.00')[CashTotal],Replace((isnull(cheque,0)+isnull(chequepending,0)+isnull(ChequePending1,0)),'.0000','.00')[chequeTotal],Replace((isnull(cash,0)+isnull(cashpending,0)+isnull(cash1,0)+isnull(cheque,0)+isnull(chequepending,0)+isnull(chequepending1,0)),'.0000','.00')[Total] from 
//(select (case when p.dca is not null then p.dca else pd.dca end) dca,Cash,Cheque,PaidTotal,CashPending,ChequePending,PendingTotal from 
//(select (case when c.DCA_code is null then ch.DCA_code else c.DCA_code end) dca,Cash,Cheque,(isnull(cash,0)+isnull(cheque,0)) PaidTotal from 
//(Select DCA_code,isnull(sum(debit),0) cash from cash_book where cc_code='CC-49' and paid_against is null and convert(datetime,date) between '04/01/2011' and '03/31/2012' group by dca_code) c full outer join 
//(Select DCA_code,isnull(sum(debit),0) cheque from bankbook where cc_code='CC-49' and status in ('debited','3') and paymenttype in ('General','Salary','SD') and convert(datetime,modifieddate) between '04/01/2011' and '03/31/2012' group by dca_code) ch on c.dca_code=ch.dca_code) p full outer join 
//(select (case when cp.DCA_code is null then chp.DCA_code else cp.DCA_code end) dca,CashPending,ChequePending,(isnull(CashPending,0)+isnull(ChequePending,0)) PendingTotal from (Select DCA_code,isnull(sum(debit),0) CashPending from cash_pending where cc_code='CC-49' and convert(datetime,date) between '04/01/2011' and '03/31/2012' group by dca_code) cp full outer join 
//(Select DCA_code,isnull(sum(debit),0) ChequePending from bankbook where status in('Debit pending','1','2') and cc_code='CC-49' and paymenttype in ('General','Salary','SD') and convert(datetime,date) between '04/01/2011' and '03/31/2012' group by dca_code) chp on cp.dca_code=chp.dca_code) pd on p.dca=pd.dca) res full outer join 
//(select (case when chp1.DCA_code is null then pc.DCA_code else chp1.DCA_code end) dca,ChequePending1,Cash1 from (Select DCA_code,isnull(sum(Total),0)-(isnull(sum(servicetax),0)+isnull(sum(tds_balance),0)+isnull(sum(retention_balance),0)) ChequePending1 from pending_invoice where balance='0' and cc_code='CC-49' and  convert(datetime,invoice_date) between '04/01/2010' and '03/31/2011' group by dca_code) chp1 full outer join 
//(Select DCA_code,isnull(sum(debit),0) Cash1 from cash_book where paid_against='CC-49' and convert(datetime,date) < '04/01/2011' group by dca_code) pc on chp1.dca_code=pc.dca_code)rec on res.dca=rec.dca)S full outer join
//(select (case when Lc.DCA_code is null then LPc.DCA_code else Lc.DCA_code end) dca,LCash,LPCash from (Select DCA_code,isnull(sum(debit),0) LCash from cash_book where cc_code='CC-49' and convert(datetime,date) < '04/01/2011' and paid_against is null group by dca_code) Lc full outer join 
//(Select DCA_code,isnull(sum(debit),0) LPCash from cash_book where paid_against='CC-49' and convert(datetime,date) < '04/01/2011' group by dca_code) LPc on Lc.dca_code=LPc.dca_code)Lpd on S.dca=Lpd.dca)N full outer join 
//(select (case when Lch.DCA_code is null then Lchp.DCA_code else Lch.DCA_code end) dca,LCheque,LChequePending from (Select DCA_code,isnull(sum(debit),0) LCheque from bankbook where cc_code='CC-49' and status in ('debited','3') and paymenttype in ('General','Salary','SD') and convert(datetime,modifieddate) < '04/01/2011' group by dca_code) Lch full outer join 
//(Select DCA_code,isnull(sum(Total),0)-(isnull(sum(servicetax),0)+isnull(sum(tds_balance),0)+isnull(sum(retention_balance),0)) LChequePending from pending_invoice where balance='0' and cc_code='CC-49' and convert(datetime,date)  < '04/01/2011' group by dca_code) Lchp on Lch.dca_code=Lchp.dca_code) Lpe on Lpe.dca=N.dca)M full outer join 
//(select (case when chpe.DCA_code is null then chpe.DCA_code else Lchp.DCA_code end) dca,ChequePending2,LChequePending1 from (Select DCA_code,isnull(sum(balance),0)+isnull(sum(tds_balance),0)+isnull(sum(retention_balance),0) ChequePending2 from pending_invoice where cc_code='CC-49'   and convert(datetime,invoice_date) < '04/01/2011' group by dca_code) chpe full outer join 
//(Select DCA_code,(isnull(sum(balance),0)+isnull(sum(tds_balance),0)+isnull(sum(retention_balance),0)) LChequePending1 from pending_invoice where cc_code='CC-49' and  convert(datetime,date) between '04/01/2011' and '03/31/2012' group by dca_code) Lchp on chpe.dca_code=Lchp.dca_code) Lpe1 on Lpe1.dca=M.dca)U full outer join 
//(Select DCA_code,isnull(Sum(cash),0)+isnull(Sum(cheque),0) [2009-10 Expence] from [2009-10 Expences] where  cc_code='CC-49'  group by dca_code)[2009-10 Expenc] on [2009-10 Expenc].dca_code=U.dca)V join dca d on V.dca=d.dca_code



