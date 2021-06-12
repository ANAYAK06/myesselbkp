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

public partial class Admin_frmViewVendorServiceTax : System.Web.UI.Page
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
       //  rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 50);
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
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string Condition = "";

            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                    if (rbtnpaiddate.Checked == true)
                    {
                    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    if (ddltype.SelectedItem.Text == "Excise/Service Tax against Service")
                    {
                        Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Service Tax','Service Excise Duty','Service Provider')  and p.dca_code='DCA-SRTX' and datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + " order by b.date asc";
                    }
                    else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Trading")
                    {
                        Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Trading Service Tax','Trading Excise Duty')  and p.dca_code='DCA-SRTX' and datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + " order by b.date asc";
                    }
                    else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Manufacturing")
                    {
                        Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Manufacturing Service Tax','Manufacturing Excise Duty','Service Provider')  and p.dca_code='DCA-Excise' and datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + " order by b.date asc";
                    }
                }
                    else if (rbtninvmkdate.Checked == true)
                    {
                        string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                        if (ddltype.SelectedItem.Text == "Excise/Service Tax against Service")
                        {
                            //Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Service Tax','Service Excise Duty','Service Provider')  and p.dca_code='DCA-SRTX' and datepart(mm,p.Inv_Making_Date)=" + ddlmonth.SelectedValue + " and datepart(yy,p.Inv_Making_Date)=" + yy + " order by b.date asc";
                            Condition = Condition + "Select distinct p.invoiceno,p.inv_making_date as [date],p.po_no,p.vendor_id,p.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from pending_invoice p where p.paymenttype in ('Service Tax','Excise Duty')  and p.dca_code='DCA-SRTX' and datepart(mm,p.inv_making_date)=" + ddlmonth.SelectedValue + " and datepart(yy,p.inv_making_date)=" + yy + " order by p.inv_making_date asc";

                        }
                        else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Trading")
                        {
                            //Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Trading Service Tax','Trading Excise Duty')  and p.dca_code='DCA-SRTX' and datepart(mm,p.Inv_Making_Date)=" + ddlmonth.SelectedValue + " and datepart(yy,p.Inv_Making_Date)=" + yy + " order by b.date asc";
                            Condition = Condition + "Select distinct p.invoiceno,p.inv_making_date as [date],p.po_no,p.vendor_id,p.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from pending_invoice p  where p.paymenttype in ('Service Tax','Excise Duty')  and p.dca_code='DCA-SRTX' and datepart(mm,p.inv_making_date)=" + ddlmonth.SelectedValue + " and datepart(yy,p.inv_making_date)=" + yy + " order by p.inv_making_date asc";

                        }
                        else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Manufacturing")
                        {
                            //Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Manufacturing Service Tax','Manufacturing Excise Duty','Service Provider')  and p.dca_code='DCA-Excise' and datepart(mm,p.Inv_Making_Date)=" + ddlmonth.SelectedValue + " and datepart(yy,p.Inv_Making_Date)=" + yy + " order by b.date asc";
                            Condition = Condition + "Select distinct p.invoiceno,p.inv_making_date as [date],p.po_no,p.vendor_id,p.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from pending_invoice p  where p.paymenttype in ('Service Tax','Excise Duty')  and p.dca_code='DCA-Excise' and datepart(mm,p.inv_making_date)=" + ddlmonth.SelectedValue + " and datepart(yy,p.inv_making_date)=" + yy + " order by p.inv_making_date asc";

                        }
                    }
                }
                else
                {
                    if (rbtnpaiddate.Checked == true)
                {
                    if (ddltype.SelectedItem.Text == "Excise/Service Tax against Service")
                    {
                        Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and  convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Service Tax','Service Excise Duty','Service Provider') and p.dca_code='DCA-SRTX'  order by b.date asc";
                    }
                    else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Trading")
                    {
                        Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and  convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Trading Service Tax','Trading Excise Duty')  and p.dca_code='DCA-SRTX'  order by b.date asc";
                    }
                    else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Manufacturing")
                    {
                        Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and  convert(datetime,b.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'and p.paymenttype in ('Service Tax','Excise Duty')  and p.SubType in ('Manufacturing Service Tax','Manufacturing Excise Duty','Service Provider')  and p.dca_code='DCA-Excise'  order by b.date asc";
                    }
                    }
                    else if (rbtninvmkdate.Checked == true)
                    {
                        if (ddltype.SelectedItem.Text == "Excise/Service Tax against Service")
                        {
                            //Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and  convert(datetime,p.Inv_Making_Date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Service Tax','Service Excise Duty','Service Provider') and p.dca_code='DCA-SRTX'  order by b.date asc";
                            Condition = Condition + "Select distinct p.invoiceno,p.inv_making_date as [date],p.po_no,p.vendor_id,p.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from pending_invoice p where convert(datetime,p.inv_making_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and p.paymenttype in ('Service Tax','Excise Duty') and p.dca_code='DCA-SRTX'  order by p.inv_making_date asc";
                        }
                        else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Trading")
                        {
                            //Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and  convert(datetime,p.Inv_Making_Date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'and p.paymenttype in ('Service Tax','Excise Duty') and p.SubType in ('Trading Service Tax','Trading Excise Duty')  and p.dca_code='DCA-SRTX'  order by b.date asc";
                            Condition = Condition + "Select distinct p.invoiceno,p.inv_making_date as [date],p.po_no,p.vendor_id,p.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from pending_invoice p where convert(datetime,p.inv_making_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and p.paymenttype in ('Service Tax','Excise Duty') and p.dca_code='DCA-SRTX'  order by p.inv_making_date asc";
                        }
                        else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Manufacturing")
                        {
                            //Condition = Condition + "Select distinct b.invoiceno,b.date,b.po_no,p.vendor_id,b.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from bankbook b  join pending_invoice p on b.invoiceno=p.invoiceno where (b.status='Debited' or b.status='3' or b.status is null)  and  convert(datetime,p.Inv_Making_Date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'and p.paymenttype in ('Service Tax','Excise Duty')  and p.SubType in ('Manufacturing Service Tax','Manufacturing Excise Duty','Service Provider')  and p.dca_code='DCA-Excise'  order by b.date asc";
                            Condition = Condition + "Select distinct p.invoiceno,p.inv_making_date as [date],p.po_no,p.vendor_id,p.invoiceno,isnull(p.ServiceTax,0)as [ServiceTax],isnull(p.Edcess,0)as [Edcess],isnull(p.Hedcess,0)as [Hedcess],isnull(p.Exciseduty,0)as [Exciseduty],(isnull(Servicetax,0)+Isnull(edcess,0)+isnull(hedcess,0)+isnull(Exciseduty,0))Total from  pending_invoice p where convert(datetime,p.inv_making_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and p.paymenttype in ('Service Tax','Excise Duty')  and p.dca_code='DCA-Excise'  order by p.inv_making_date asc";
                        }
                    }

                }
            }


            ViewState["Condition"] = Condition;
            fillgrid(GridView1);
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    public void fillgrid(GridView gridControl)
    {
        try
        {
            da = new SqlDataAdapter(ViewState["Condition"].ToString(), con);
            da.Fill(ds, "Condition");
            //GridView1.DataSource = ds.Tables["Condition"].DefaultView.ToTable(true, "invoiceno");
            //GridView1.DataSource = ds.Tables["Condition"];
            //GridView1.DataBind();
            if (ds.Tables["Condition"].Rows.Count > 0)
            {
                gridControl.DataSource = ds.Tables["Condition"];
                gridControl.DataBind();
                trexcel.Visible = true;
            }
            else
            {
                gridControl.EmptyDataText = "No Data Avaliable For Selection Criteria";
                gridControl.DataSource = null;
                gridControl.DataBind();
                trexcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Servicetax += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Servicetax"));
            Edcess += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Edcess"));
            Hedcess += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hedcess"));
            Exciseduty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Exciseduty"));
            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hedcess")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Servicetax")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Edcess"))+ Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Exciseduty"));
           

        }
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (ddltype.SelectedItem.Text == "Excise/Service Tax against Service" || ddltype.SelectedItem.Text == "Excise/Service Tax against Trading")
        //    {
        //        e.Row.Cells[5].Text = "Servicetax";
        //    }
        //    else if (ddltype.SelectedItem.Text == "Excise/Service Tax against Manufacturing")
        //    {
        //        e.Row.Cells[5].Text = "Excise Duty";
        //    }
        //}
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", Servicetax);
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Edcess);
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", Hedcess);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Exciseduty);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Total);


        }
    }
    private decimal Servicetax = (decimal)0.0;
    private decimal Edcess = (decimal)0.0;
    private decimal Hedcess = (decimal)0.0;
    private decimal Exciseduty = (decimal)0.0;
    private decimal Total = (decimal)0.0;
    private decimal Servicetax2 = (decimal)0.0;
    private decimal Edcess2 = (decimal)0.0;
    private decimal Hedcess2 = (decimal)0.0;
    private decimal Exciseduty2 = (decimal)0.0;
    private decimal Total2 = (decimal)0.0;

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Servicetax2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Servicetax"));
            Edcess2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Edcess"));
            Hedcess2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hedcess"));
            Exciseduty2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Exciseduty"));
            Total2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hedcess")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Servicetax")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Edcess")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Exciseduty"));


        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", Servicetax2);
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Edcess2);
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", Hedcess2);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Exciseduty2);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Total2);


        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {

        fillgrid(GridView2);
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View vendor servicetax"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView2.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }

}
