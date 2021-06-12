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

public partial class ViewExciseDuty : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        //esselDal RoleCheck = new esselDal();
        // int rec = 0;
        //  rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 48);
        // if (rec == 0)
        //     Response.Redirect("Menucontents.aspx");

        if (!IsPostBack)
        {
            LoadYear();
            trexcel.Visible = false;
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

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            //GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell Cell_Header = new TableCell();
            //Cell_Header.Text = "Date";
            //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            //Cell_Header.ColumnSpan = 1;
            //Cell_Header.RowSpan = 2;
            //HeaderRow.Cells.Add(Cell_Header);

            //Cell_Header = new TableCell();
            //Cell_Header.Text = "Recieved From Client";
            //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            //Cell_Header.ColumnSpan = 4;
            //HeaderRow.Cells.Add(Cell_Header);

            //Cell_Header = new TableCell();
            //Cell_Header.Text = "Paid";
            //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            //Cell_Header.ColumnSpan = 2;
            //HeaderRow.Cells.Add(Cell_Header);

            //Cell_Header = new TableCell();
            //Cell_Header.Text = "Balance";
            //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            //Cell_Header.ColumnSpan = 1;
            //HeaderRow.Cells.Add(Cell_Header);
            //Cell_Header.RowSpan = 2;

            //GridView1.Controls[0].Controls.AddAt(0, HeaderRow);


            GridView HeaderGrid2 = (GridView)sender;
            GridViewRow HeaderRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header2 = new TableCell();
            Cell_Header2.Text = "View ExciseDuty";
            Cell_Header2.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header2.ColumnSpan = 9;
            Cell_Header2.RowSpan = 1;
            HeaderRow2.Cells.Add(Cell_Header2);

            GridView1.Controls[0].Controls.AddAt(0, HeaderRow2);

            GridView HeaderGrid3 = (GridView)sender;
            GridViewRow HeaderRow3 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header3 = new TableCell();
            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Date";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 3;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Recieved From Client";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 4;

            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Paid";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 3;
            HeaderRow3.Cells.Add(Cell_Header3);

            Cell_Header3 = new TableCell();
            Cell_Header3.Text = "Balance";
            Cell_Header3.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header3.ColumnSpan = 1;
            Cell_Header3.RowSpan = 3;
            HeaderRow3.Cells.Add(Cell_Header3);
            GridView1.Controls[0].Controls.AddAt(1, HeaderRow3);

            GridView HeaderGrid4 = (GridView)sender;
            GridViewRow HeaderRow4 = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header4 = new TableCell();
            Cell_Header4 = new TableCell();

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "NetExciseDuty";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "NetEdcess";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;


            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "NetHedcess";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "ExciseDuty";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;

            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "ToVendor";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 1;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 2;


            Cell_Header4 = new TableCell();
            Cell_Header4.Text = "ToGovernment";
            Cell_Header4.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header4.ColumnSpan = 2;
            HeaderRow4.Cells.Add(Cell_Header4);
            Cell_Header4.RowSpan = 1;



            GridView1.Controls[0].Controls.AddAt(2, HeaderRow4);




        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            e.Row.Cells[1].Visible = false;

            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ServiceTax += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Servicetax"));
            Edcess += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Edcess"));
            Hedcess += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hedcess"));
            TotalServiceTax += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalServiceTax"));
            Vendor += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Vendor"));
            Govt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Govt"));
            PTax += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PTax"));
            Balance += (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalServiceTax")) - (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Vendor")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Govt")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PTax"))));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = String.Format("{0:#,##,##,###.00}", ServiceTax);
            e.Row.Cells[2].Text = String.Format("{0:#,##,##,###.00}", Edcess);
            e.Row.Cells[3].Text = String.Format("{0:#,##,##,###.00}", Hedcess);
            e.Row.Cells[4].Text = String.Format("{0:#,##,##,###.00}", TotalServiceTax);
            e.Row.Cells[5].Text = String.Format("{0:#,##,##,###.00}", Vendor);
            e.Row.Cells[6].Text = String.Format("{0:#,##,##,###.00}", Govt);
            e.Row.Cells[7].Text = String.Format("{0:#,##,##,###.00}", PTax);
            e.Row.Cells[8].Text = String.Format("{0:#,##,##,###.00}", Balance);
            if (ServiceTax < 0)
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
            if (Edcess < 0)
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            if (Hedcess < 0)
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
            if (TotalServiceTax < 0)
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            if (Vendor < 0)
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            if (Govt < 0)
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
            if (PTax < 0)
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
            if (Balance < 0)
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;



        }
    }
    private decimal ServiceTax = (decimal)0.0;
    private decimal Edcess = (decimal)0.0;
    private decimal Hedcess = (decimal)0.0;
    private decimal TotalServiceTax = (decimal)0.0;
    private decimal Vendor = (decimal)0.0;
    private decimal Govt = (decimal)0.0;
    private decimal PTax = (decimal)0.0;
    private decimal Balance = (decimal)0.0;



    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string Condition = "";

            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    //Condition = Condition + "select(case when s.date is not null then s.date else d.date end) date,isnull(client,0)as[Client],isnull(vendor,0)as[Vendor],isnull(Govt,0)as[Govt],(isnull(client,0)-(isnull(vendor,0)+isnull(Govt,0)))[Balance] from ((Select  (case when c.invoice_date is  null then ch.invoice_date else c.invoice_date  end)date,Client,Vendor from (Select invoice_date,isnull(Sum(NetServicetax),0) client from invoice where status='Credit'and paymenttype='Invoice Service'and datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy + "  group by invoice_date) c full outer join (Select invoice_date,isnull(Sum(Servicetax),0) vendor from pending_invoice where status='Debited' and paymenttype='Service Provider'and datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy + "  group by invoice_date) ch on c.invoice_date=ch.invoice_date))s full outer join (Select date,isnull(Sum(debit),0) Govt from bankbook where dca_code='DCA-SRTX' and status='Debited' and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy + " group by date) d on s.date=d.date order by date";
                    //Condition = Condition + "select  (case when s.date is not null then s.date else d.modifieddate end) date,isnull(Servicetax,0)as[Servicetax],isnull(Edcess,0)as[Edcess],isnull(Hedcess,0)as[Hedcess],isnull(TotalServicetax,0)as[TotalServicetax],isnull(vendor,0)as[Vendor],isnull(Govt,0)as[Govt],(isnull(TotalServiceTax,0)-(isnull(vendor,0)+isnull(Govt,0)))[Balance] from ((Select  (case when c.date is  null then ch.date else c.date  end)date,Servicetax,Edcess,Hedcess,(isnull(Edcess,0)+isnull(Hedcess,0)+isnull(Servicetax,0))as[TotalServiceTax],Vendor from (Select b.date,isnull(Sum(i.NetServicetax),0)Servicetax,isnull(sum(i.NetEdcess),0)Edcess,isnull(sum(i.NetHedcess),0) Hedcess from invoice i join bankbook b on b.invoiceno=i.invoiceno where (b.paymenttype='Invoice Service' or b.paymenttype='Hold') and  datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + "  group by b.date) c full outer join (Select b.date,isnull(Sum(b.debit),0)as vendor from pending_invoice p join bankbook b on b.invoiceno=p.invoiceno where  p.paymenttype in ('Service Tax','Excise Duty') and (b.status='3' or b.status is null) and datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + "  group by b.date) ch on c.date=ch.date))s full outer join (Select modifieddate,isnull(Sum(debit),0) Govt from bankbook where dca_code='DCA-SRTX' and paymenttype='General' and status in ('Debited','3') and datepart(mm,modifieddate)=" + ddlmonth.SelectedValue + " and datepart(yy,modifieddate)=" + yy + "  group by modifieddate) d on s.date=d.modifieddate order by date";
                    Condition = Condition + "select  (case when Tax.date is not null then Tax.date else PTax.date end) date,isnull(Servicetax,0)as[Servicetax],isnull(Edcess,0)as[Edcess],isnull(Hedcess,0)as[Hedcess],isnull(TotalServicetax,0)as[TotalServicetax],isnull(vendor,0)as[Vendor],isnull(Govt,0)as[Govt],isnull(PTax,0)as[PTax],(isnull(TotalServiceTax,0)-(isnull(vendor,0)+isnull(Govt,0)+isnull(PTax,0)))[Balance] from (select  (case when s.date is not null then s.date else d.date end) date,isnull(Servicetax,0)as[Servicetax],isnull(Edcess,0)as[Edcess],isnull(Hedcess,0)as[Hedcess],isnull(TotalServicetax,0)as[TotalServicetax],isnull(vendor,0)as[Vendor],isnull(Govt,0)as[Govt],(isnull(TotalServiceTax,0)-(isnull(vendor,0)+isnull(Govt,0)))[Balance] from ((Select  (case when c.date is  null then ch.date else c.date  end)date,Servicetax,Edcess,Hedcess,(isnull(Edcess,0)+isnull(Hedcess,0)+isnull(Servicetax,0))as[TotalServiceTax],Vendor from (Select b.date,isnull(Sum(i.Exciseduty),0)Servicetax,isnull(sum(i.NetEdcess),0)Edcess,isnull(sum(i.NetHedcess),0) Hedcess from invoice i join bankbook b on b.invoiceno=i.invoiceno where  (b.paymenttype='Invoice Service' or b.paymenttype='Hold') and b.cc_code in (select cc_code from cost_center where cc_type='Performing' and cc_subtype in ('Manufacturing')) and   datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + " group by b.date) c full outer join (Select b.date,isnull(Sum(b.debit),0)as vendor from pending_invoice p join bankbook b on b.invoiceno=p.invoiceno where  (b.status='3' or b.status is null) and p.dca_code='DCA-Excise' and p.paymenttype in ('Service Tax','Excise Duty') and  datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + " group by b.date) ch on c.date=ch.date))s full outer join (Select date,isnull(Sum(debit),0) Govt from bankbook where dca_code='DCA-Excise' and sub_dca='DCA-Excise .1' and paymenttype='General' and status in ('Debited','3') and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy + " group by date) d on s.date=d.date)Tax full outer join (Select date,isnull(Sum(debit),0) PTax from bankbook where dca_code='DCA-Excise' and sub_dca='DCA-Excise .2' and paymenttype='General' and status in ('Debited','3') and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy + " group by date)PTax on Tax.date=PTax.date order by date";


                }
                else
                {
                    //Condition = Condition + "select  (case when s.date is not null then s.date else d.modifieddate end) date,isnull(Servicetax,0)as[Servicetax],isnull(Edcess,0)as[Edcess],isnull(Hedcess,0)as[Hedcess],isnull(TotalServicetax,0)as[TotalServicetax],isnull(vendor,0)as[Vendor],isnull(Govt,0)as[Govt],(isnull(TotalServiceTax,0)-(isnull(vendor,0)+isnull(Govt,0)))[Balance] from ((Select  (case when c.date is  null then ch.date else c.date  end)date,Servicetax,Edcess,Hedcess,(isnull(Edcess,0)+isnull(Hedcess,0)+isnull(Servicetax,0))as[TotalServiceTax],Vendor from (Select b.date,isnull(Sum(i.NetServicetax),0)Servicetax,isnull(sum(i.NetEdcess),0)Edcess,isnull(sum(i.NetHedcess),0) Hedcess from invoice i join bankbook b on b.invoiceno=i.invoiceno where  (b.paymenttype='Invoice Service' or b.paymenttype='Hold') and   convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' group by b.date) c full outer join (Select b.date,isnull(Sum(b.debit),0)as vendor from pending_invoice p join bankbook b on b.invoiceno=p.invoiceno where  (b.status='3' or b.status is null) and p.paymenttype in ('Service Tax','Excise Duty') and  convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' group by b.date) ch on c.date=ch.date))s full outer join (Select modifieddate,isnull(Sum(debit),0) Govt from bankbook where dca_code='DCA-SRTX' and paymenttype='General' and status in ('Debited','3') and convert(datetime,modifieddate) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' group by modifieddate) d on s.date=d.modifieddate order by date";

                    Condition = Condition + "select  (case when Tax.date is not null then Tax.date else PTax.date end) date,isnull(Servicetax,0)as[Servicetax],isnull(Edcess,0)as[Edcess],isnull(Hedcess,0)as[Hedcess],isnull(TotalServicetax,0)as[TotalServicetax],isnull(vendor,0)as[Vendor],isnull(Govt,0)as[Govt],isnull(PTax,0)as[PTax],(isnull(TotalServiceTax,0)-(isnull(vendor,0)+isnull(Govt,0)+isnull(PTax,0)))[Balance] from (select  (case when s.date is not null then s.date else d.date end) date,isnull(Servicetax,0)as[Servicetax],isnull(Edcess,0)as[Edcess],isnull(Hedcess,0)as[Hedcess],isnull(TotalServicetax,0)as[TotalServicetax],isnull(vendor,0)as[Vendor],isnull(Govt,0)as[Govt],(isnull(TotalServiceTax,0)-(isnull(vendor,0)+isnull(Govt,0)))[Balance] from ((Select  (case when c.date is  null then ch.date else c.date  end)date,Servicetax,Edcess,Hedcess,(isnull(Edcess,0)+isnull(Hedcess,0)+isnull(Servicetax,0))as[TotalServiceTax],Vendor from (Select b.date,isnull(Sum(i.Exciseduty),0)Servicetax,isnull(sum(i.NetEdcess),0)Edcess,isnull(sum(i.NetHedcess),0) Hedcess from invoice i join bankbook b on b.invoiceno=i.invoiceno where  (b.paymenttype='Invoice Service' or b.paymenttype='Hold') and b.cc_code in (select cc_code from cost_center where cc_type='Performing' and cc_subtype in ('Manufacturing')) and   convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' group by b.date) c full outer join (Select b.date,isnull(Sum(b.debit),0)as vendor from pending_invoice p join bankbook b on b.invoiceno=p.invoiceno where  (b.status='3' or b.status is null) and p.paymenttype in ('Service Tax','Excise Duty') and p.dca_code='DCA-Excise' and  convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' group by b.date) ch on c.date=ch.date))s full outer join (Select date,isnull(Sum(debit),0) Govt from bankbook where dca_code='DCA-Excise' and sub_dca='DCA-Excise .1' and paymenttype='General' and status in ('Debited','3') and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' group by date) d on s.date=d.date)Tax full outer join (Select date,isnull(Sum(debit),0) PTax from bankbook where dca_code='DCA-Excise' and sub_dca='DCA-Excise .2' and paymenttype='General' and status in ('Debited','3') and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' group by date)PTax on Tax.date=PTax.date order by date";


                }
            }


            ViewState["Condition"] = Condition;
            // JavaScript.Alert(Condition);
            fillgrid();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillgrid()
    {
        try
        {
            da = new SqlDataAdapter(ViewState["Condition"].ToString(), con);
            da.Fill(ds, "Condition");
            if (ds.Tables["Condition"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["Condition"];
                GridView1.DataBind();
                trexcel.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                trexcel.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.AllowPaging = false;
        fillgrid();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Service Tax Payment"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }
}
