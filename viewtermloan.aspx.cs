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



public partial class viewtermloan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
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
        header.Visible = false;
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string condition = "";
            string condition1 = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;

                    condition = condition + " and  datepart(mm,modifieddate)=" + ddlmonth.SelectedValue + " and datepart(yy,modifieddate)=" + yy;
                }
                else
                {
                    condition = condition + " and convert(datetime,modifieddate) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                }
            }

            if (ddlagencycode.SelectedItem.Text != "Select All")
            {
                condition = condition + " and agencycode='" + ddlagencycode.SelectedItem.Text + "'";
                condition1 = condition1 + " and agencycode='" + ddlagencycode.SelectedItem.Text + "'";

            }
            if (ddlloanno.SelectedItem.Text != "Select All")
            {
                condition = condition + " and b.loanno='" + ddlloanno.SelectedItem.Text + "'";
                condition1 = condition1 + " and loanno='" + ddlloanno.SelectedItem.Text + "'";

            }


            ViewState["condition"] = condition;
            ViewState["condition1"] = condition1;
            GridView1.PageIndex = 1;
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
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
    public void fillgrid()
    {
        try
        {
            // da = new SqlDataAdapter("select t.agencycode,t.loanno,b.bank_name,REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-') as paiddate,b.modeofpay,b.no as chequenumber,replace(sum(isnull(b.credit,0)),'.0000','.00')credit,replace(isnull(b.debit,0),'.0000','.00')debit from termloan t join bankbook b on t.loanno=b.loanno where b.id>0 " + ViewState["condition"].ToString() + " group by t.agencycode,t.loanno,b.debit,b.bank_name,b.modifieddate,b.modeofpay,b.no", con);
            da = new SqlDataAdapter("select i.* from (select t.agencycode,t.loanno,b.bank_name,REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-') as date,b.modeofpay,b.no as chequenumber,replace(sum(isnull(b.credit,0)),'.0000','.00')credit,replace(sum(isnull(b.principle,0)),'.0000','.00')principle,replace(sum(isnull(b.interest,0)),'.0000','.00')interest,'0'as Processingamt from termloan t join bankbook b on t.loanno=b.loanno where b.id>0 and paymenttype='Term Loan' and (b.status!='Rejected' OR b.status IS null) " + ViewState["condition"].ToString() + " group by t.agencycode,t.loanno,b.debit,b.bank_name,b.modifieddate,b.modeofpay,b.no,b.principle,b.interest union all select t.agencycode,t.loanno,b.bank_name,REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-') as paiddate,b.modeofpay,b.no as chequenumber,replace(sum(isnull(b.debit,0)),'.0000','.00')credit,replace(sum(isnull(b.principle,0)),'.0000','.00')principle,replace(sum(isnull(b.interest,0)),'.0000','.00')interest,replace(sum(isnull(t.processing_amt,0)),'.0000','.00')Processingamt from termloan t join bankbook b on t.loanno=b.loanno where b.id>0 and paymenttype='Supplier' and b.status!='Rejected' " + ViewState["condition"].ToString() + " group by t.agencycode,t.loanno,b.debit,b.bank_name,b.modifieddate,b.modeofpay,b.no,b.principle,b.interest)i", con);


            da.Fill(ds, "termloan");
            if (ds.Tables["termloan"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["termloan"];
                GridView1.DataBind();
                total();
                trexcel.Visible = true;
            }
            else
            {
                GridView1.DataSource = ds.Tables["termloan"];
                GridView1.DataBind();
                trexcel.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        
        
    }
    public void total()
    {

        da = new SqlDataAdapter("Select Sum(isnull(principle,0))as principle,Sum(isnull(interest,0))as interest,'' as balance,'' as amount from bankbook b join termloan t on t.loanno=b.loanno  where  (principle!='0' or interest!='0') and b.loanno is not null and b.status!='Rejected' " + ViewState["condition"].ToString() + " union  select '' as principle,'' as interest, sum(isnull(balance,0))as balance,sum(isnull(amount,0))as amount from termloan where loanno is not null and ((status='3' and loantype='For Capital') or (status='4' and loantype='For Purchase Of Assets')) " + ViewState["condition1"].ToString() + "", con);
        da.Fill(ds, "total");
        if(ds.Tables["total"].Rows.Count>0)
        {
            Label7.Text = ds.Tables["total"].Rows[1][0].ToString();
            Label8.Text = ds.Tables["total"].Rows[1][1].ToString();             
            if (ds.Tables["total"].Rows.Count > 1)
            {
                Label19.Text = ds.Tables["total"].Rows[0][2].ToString();
            }
        }
        header.Visible = true;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = true;
            e.Row.Cells[9].Visible = true;              
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Credit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "credit"));
            prcamt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Processingamt"));
            principle += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "principle"));
            interest += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "interest"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Credit);
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", prcamt);
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", principle);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", interest);
        }
    }
    private decimal Credit = (decimal)0.0;
    private decimal principle = (decimal)0.0;
    private decimal interest = (decimal)0.0;
    private decimal prcamt = (decimal)0.0;
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("viewtermloan.aspx");
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
                GridView HeaderGrid1 = (GridView)sender;
                GridViewRow HeaderRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Cell_Header1 = new TableCell();
                Cell_Header1.Text = "Ledger";
                Cell_Header1.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header1.ColumnSpan = 10;
                Cell_Header1.RowSpan = 1;
                HeaderRow1.Cells.Add(Cell_Header1);


                GridView1.Controls[0].Controls.AddAt(0, HeaderRow1);
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderRow = new GridViewRow(0,1, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Cell_Header = new TableCell();
                Cell_Header.Text = "Agency Code";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "Loan Number";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "Bank Name";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "Date";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "Mode Of Pay";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "Cheque Number";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);
               

                Cell_Header = new TableCell();
                Cell_Header.Text = "Credit";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "Processing Charge";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 1;
                Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);


                Cell_Header = new TableCell();
                Cell_Header.Text = "Debit";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                Cell_Header.RowSpan = 1;
                HeaderRow.Cells.Add(Cell_Header);

               
                GridView1.Controls[0].Controls.AddAt(1, HeaderRow);
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
        string s = ddlyear.SelectedValue + ".xls";
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", s));
        Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.IO.StringWriter sw =
         new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw =
           new System.Web.UI.HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
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
