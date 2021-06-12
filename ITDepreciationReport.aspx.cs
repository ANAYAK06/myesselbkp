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

public partial class ITDepreciationReport : System.Web.UI.Page
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
        for (int i = 2010; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {

            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
                ViewState["date"] = (i + 1).ToString();
            }

        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
        try
        {
            da = new SqlDataAdapter("IT_Deppreciation", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Fyear", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["fill"];
                GridView1.DataBind();
                trexcel.Visible = true;
                DateTime dt = DateTime.Now;
                string year= dt.Year.ToString();
                string year1 = (dt.Year + 1).ToString().Substring(2, 2);

                string curr_Fyear = year + "-" + year1;
                if(ddlyear.SelectedItem.Text==curr_Fyear)
                    lblheader.Text = "DETAILS FOR FIXED ASSETS & DEPRECIATION UP TO  " + DateTime.Today.Date.ToString("dd/MM/yyyy");
                else
                    lblheader.Text = "DETAILS FOR FIXED ASSETS & DEPRECIATION FOR YEAR ENDED 31/03/" + ((Convert.ToInt32(ddlyear.SelectedItem.Text.Substring(0, 4))) + 1);
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                trexcel.Visible = false;
                lblheader.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    private decimal purchased_price = (decimal)0.0;
    private decimal FY_opening_bal = (decimal)0.0;
    private decimal Current_FY_Add = (decimal)0.0;
    private decimal Total = (decimal)0.0;
    private decimal Del_value = (decimal)0.0;
    private decimal Dep_value = (decimal)0.0;
    private decimal Bal_value = (decimal)0.0;
    private decimal Gain_Loss = (decimal)0.0;
    private decimal Close_bal = (decimal)0.0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[12].Text != ".00")
                {
                    decimal basicvalue = Convert.ToDecimal(e.Row.Cells[18].Text);
                    decimal deletion_value = Convert.ToDecimal(e.Row.Cells[12].Text);
                    decimal gain_loss = deletion_value - basicvalue;
                    e.Row.Cells[20].Text = (deletion_value - basicvalue).ToString();
                    e.Row.Cells[21].Text = ((basicvalue - deletion_value) + gain_loss).ToString();
                    Gain_Loss += Convert.ToDecimal((e.Row.Cells[20].Text));
                   // Close_bal += Convert.ToDecimal((e.Row.Cells[21].Text));
                }
                else
                {
                    e.Row.Cells[21].Text = e.Row.Cells[18].Text;
                }
                if (e.Row.Cells[7].Text == "&nbsp;")
                {
                    e.Row.Cells[7].Text = Convert.ToInt32("0").ToString();
                }
                purchased_price += Convert.ToDecimal((e.Row.Cells[7].Text));
                FY_opening_bal += Convert.ToDecimal((e.Row.Cells[8].Text));
                Current_FY_Add += Convert.ToDecimal((e.Row.Cells[9].Text));
                Total += Convert.ToDecimal((e.Row.Cells[10].Text));
                Del_value += Convert.ToDecimal((e.Row.Cells[12].Text));
                Dep_value += Convert.ToDecimal((e.Row.Cells[17].Text));
                Bal_value += Convert.ToDecimal((e.Row.Cells[18].Text));
                Close_bal += Convert.ToDecimal((e.Row.Cells[21].Text));
                //e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
               
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = String.Format(" {0:#,##,##,###.00}", purchased_price);
                e.Row.Cells[8].Text = String.Format(" {0:#,##,##,###.00}", FY_opening_bal);
                e.Row.Cells[9].Text = String.Format(" {0:#,##,##,###.00}", Current_FY_Add);
                e.Row.Cells[10].Text = String.Format(" {0:#,##,##,###.00}", Total);
                e.Row.Cells[12].Text = String.Format(" {0:#,##,##,###.00}", Del_value);
                e.Row.Cells[17].Text = String.Format(" {0:#,##,##,###.00}", Dep_value);
                e.Row.Cells[18].Text = String.Format(" {0:#,##,##,###.00}", Bal_value);

                e.Row.Cells[20].Text = String.Format(" {0:#,##,##,###.00}", Gain_Loss);
                e.Row.Cells[21].Text = String.Format(" {0:#,##,##,###.00}", Close_bal);      
            }
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell0 = new TableCell();
                cell0.Text = "SL No.";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                cell0.RowSpan = 2;

                TableCell cell1 = new TableCell();
                cell1.Text = "Transaction Id";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 1;
                cell1.RowSpan = 2;

                TableCell cell2 = new TableCell();
                cell2.Text = "Actual Invoice";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 1;
                cell2.RowSpan = 2;

                TableCell cell3 = new TableCell();
                cell3.Text = "Item Code";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 1;
                cell3.RowSpan = 2;

                TableCell assetname = new TableCell();
                assetname.Text = "Asset Name";
                assetname.HorizontalAlign = HorizontalAlign.Center;
                assetname.ColumnSpan = 1;
                assetname.RowSpan = 2;

                TableCell cell4 = new TableCell();
                cell4.Text = "Subdca Code";
                cell4.HorizontalAlign = HorizontalAlign.Center;
                cell4.ColumnSpan = 1;
                cell4.RowSpan = 2;

                //TableCell cell4 = new TableCell();
                //cell4.Text = "TOTAL RECEIPT AT CC";
                //cell4.HorizontalAlign = HorizontalAlign.Center;
                //cell4.ColumnSpan = 4;
                //cell4.RowSpan = 1;

                //TableCell cell5 = new TableCell();
                //cell5.Text = "TOTAL  OUT FROM CC";
                //cell5.HorizontalAlign = HorizontalAlign.Center;
                //cell5.ColumnSpan = 5;
                //cell5.RowSpan = 1;

                TableCell cell5 = new TableCell();
                cell5.Text = "Asset Category";
                cell5.HorizontalAlign = HorizontalAlign.Center;
                cell5.ColumnSpan = 1;
                cell5.RowSpan = 2;

                TableCell cell6 = new TableCell();
                cell6.Text = "Purchased Price";
                cell6.HorizontalAlign = HorizontalAlign.Center;
                cell6.ColumnSpan = 1;
                cell6.RowSpan = 2;

                TableCell cell7 = new TableCell();
                cell7.Text = "FY Opening Balance";
                cell7.HorizontalAlign = HorizontalAlign.Center;
                cell7.ColumnSpan = 1;
                cell7.RowSpan = 2;

                TableCell cell8 = new TableCell();
                cell8.Text = "Current FY Addition";
                cell8.HorizontalAlign = HorizontalAlign.Center;
                cell8.ColumnSpan = 1;
                cell8.RowSpan = 2;

                TableCell cell9 = new TableCell();
                cell9.Text = "Total Value";
                cell9.HorizontalAlign = HorizontalAlign.Center;
                cell9.ColumnSpan = 1;
                cell9.RowSpan = 2;

                TableCell cell10 = new TableCell();
                cell10.Text = "Date of Purchase";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.ColumnSpan = 1;
                cell10.RowSpan = 2;

                TableCell cell11 = new TableCell();
                cell11.Text = "Deleted Items Due to soldout Damaged/Scaraped";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.ColumnSpan = 3;
                cell11.RowSpan = 1;

                TableCell cell12 = new TableCell();
                cell12.Text = "Valid Days";
                cell12.HorizontalAlign = HorizontalAlign.Center;
                cell12.ColumnSpan = 1;
                cell12.RowSpan = 2;

                TableCell cell13 = new TableCell();
                cell13.Text = "Dep Percentage";
                cell13.HorizontalAlign = HorizontalAlign.Center;
                cell13.ColumnSpan = 1;
                cell13.RowSpan = 2;

                TableCell cell14 = new TableCell();
                cell14.Text = "Depreciation Value";
                cell14.HorizontalAlign = HorizontalAlign.Center;
                cell14.ColumnSpan = 1;
                cell14.RowSpan = 2;

                TableCell cell15 = new TableCell();
                cell15.Text = "Balance Value";
                cell15.HorizontalAlign = HorizontalAlign.Center;
                cell15.ColumnSpan = 1;
                cell15.RowSpan = 2;

                TableCell cell16 = new TableCell();
                cell16.Text = "Soldout/Scrapped";
                cell16.HorizontalAlign = HorizontalAlign.Center;
                cell16.ColumnSpan = 2;
                cell16.RowSpan = 1;

                TableCell cell17 = new TableCell();
                cell17.Text = "FY Closing Balance";
                cell17.HorizontalAlign = HorizontalAlign.Center;
                cell17.ColumnSpan = 1;
                cell17.RowSpan = 2;

                //TableCell cell18 = new TableCell();
                //cell18.Text = "Opening Date";
                //cell18.HorizontalAlign = HorizontalAlign.Center;
                //cell18.ColumnSpan = 1;
                //cell18.RowSpan = 2;

                //TableCell cell19 = new TableCell();
                //cell19.Text = "Closing Date";
                //cell19.HorizontalAlign = HorizontalAlign.Center;
                //cell19.ColumnSpan = 1;
                //cell19.RowSpan = 2;



                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                gvrow.Cells.Add(assetname);
                gvrow.Cells.Add(cell4);
                gvrow.Cells.Add(cell5);
                gvrow.Cells.Add(cell6);
                gvrow.Cells.Add(cell7);
                gvrow.Cells.Add(cell8);
                gvrow.Cells.Add(cell9);
                gvrow.Cells.Add(cell10);
                gvrow.Cells.Add(cell11);
                gvrow.Cells.Add(cell12);
                gvrow.Cells.Add(cell13);
                gvrow.Cells.Add(cell14);
                gvrow.Cells.Add(cell15);
                gvrow.Cells.Add(cell16);
                gvrow.Cells.Add(cell17);
                //gvrow.Cells.Add(cell18);
                //gvrow.Cells.Add(cell19);
                GridView1.Controls[0].Controls.AddAt(0, gvrow);
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = true;
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[19].Visible = true;
                e.Row.Cells[20].Visible = true;
                e.Row.Cells[21].Visible = false;
                //e.Row.Cells[22].Visible = false;
                //e.Row.Cells[23].Visible = false;
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        fillgrid();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "IT Depreciation Report"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

}