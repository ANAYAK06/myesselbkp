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
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Drawing;

public partial class CreditSummary : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataView dv;

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "Active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        //esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 52);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");

        if (!IsPostBack)
        {
            LoadYear();
            trexcel.Visible = false;

          
        }

    }

    private void LoadYear()
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
 
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
            e.Row.Attributes.Add("style", "cursor: pointer");
            int Prevyear = 0;
            int Prevyear1 = -1;
            if (e.Row.Cells[0].Text == "OPENING BALANCE")
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = "0";
            }
            DataTable objdt = ViewState["Columns"] as DataTable;
            for (int i = 1; i < e.Row.Cells.Count-1; i++)
            {
              e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
              if (i == 1)
              {
                  e.Row.Cells[i].Attributes.Add("onclick", "window.open('BankCreditSummary.aspx?Summarytype=" + e.Row.Cells[0].Text + "&Year=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "&Year1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "&PrevYear=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + " &PrevYear1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
              }
              else
              {


                  e.Row.Cells[i].Attributes.Add("onclick", "window.open('BankCreditSummary.aspx?Summarytype=" + e.Row.Cells[0].Text + "&Year=" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "&Year1=" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "&PrevYear=" + objdt.Columns[i+1].ToString().Substring(0, 4) + " &PrevYear1=20" + objdt.Columns[i+1].ToString().Substring(5, 2) + "','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");

              }
              Prevyear = Prevyear + 1;
              Prevyear1 = Prevyear1 + 1;
            }
            e.Row.Cells[e.Row.Cells.Count-1].HorizontalAlign = HorizontalAlign.Right;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total excluding Opening balance";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            EnumerableRowCollection<DataRow> query = from order in ds.Tables[0].AsEnumerable()
                                                     where order.Field<string>("Paymenttype") != "OPENING BALANCE"
                                                     select order;
            DataView view = query.AsDataView();
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {


                e.Row.Cells[i].Text = String.Format("Rs. {0:#,##,##,###.00}", Convert.ToInt32((view.ToTable().Compute("SUM([" + view.ToTable().Columns[i].ColumnName + "])", string.Empty))));

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

    
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {

        Response.ClearContent();
        Response.Buffer = true;
        string s = ddlyear.SelectedItem.Text + "Credit Summary Status.xls";
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", s));
        Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
       

        //'gets the current footer row to clone
        GridViewRow footer = GridView1.FooterRow;
        int numCells = footer.Cells.Count;

        GridViewRow newRow = new GridViewRow(footer.RowIndex + 1, -1, footer.RowType, footer.RowState);

        //'have to add in the right number of cells
        //'this also copies any styles over from the original footer
        for (int i = 0; i <= numCells - 1; i++)
        {
            TableCell emptyCell = new TableCell();
            if (i == 0)
            {
                emptyCell.HorizontalAlign = HorizontalAlign.Left;
                emptyCell.Text = "Total including Opening balance";
            }
            else
            {
                emptyCell.Text = String.Format("Rs. {0:#,##,##,###.00}", Convert.ToInt32(((ViewState["ds"] as DataTable).Compute("SUM([" + (ViewState["ds"] as DataTable).Columns[i].ColumnName + "])", string.Empty))));
            }
            newRow.Cells.Add(emptyCell);
        }

        //newRow.Cells[5].Text = "Total Discount:";
        //newRow.Cells(6).Text = "55.00";

        //'add new row to the gridview table, at the very bottom
        ((Table)GridView1.Controls[0]).Rows.Add(newRow);
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
        //Panel1.RenderControl(htw);
        GridView1.RenderControl(htw);
        
        
       // GridView1.AllowPaging = true;
       // GridView1.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
       // GridView1.DataBind();
        //Change the Header Row back to white color
       // GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //Applying stlye to gridview header cells
        //for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        //{
        //    GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
        //}
        //int j = 1;
        ////This loop is used to apply stlye to cells based on particular row
        //foreach (GridViewRow gvrow in GridView1.Rows)
        //{
        //    gvrow.BackColor = Color.White;
        //    if (j <= GridView1.Rows.Count)
        //    {
        //        if (j % 2 != 0)
        //        {
        //            for (int k = 0; k < gvrow.Cells.Count; k++)
        //            {
        //                gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
        //            }
        //        }
        //    }
        //    j++;
        //}

        Response.Write(sw.ToString());
        Response.End();

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
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";
            da.Fill(ds, "credit");
            ViewState["Columns"] = ds.Tables[0].Clone();
            ds.Tables[0].Columns.RemoveAt(0);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            trexcel.Visible = true;
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        GridView grid = (GridView)sender;

        //'gets the current footer row to clone
        GridViewRow footer = grid.FooterRow;
        int numCells = footer.Cells.Count;

        GridViewRow newRow = new GridViewRow(footer.RowIndex + 1, -1, footer.RowType, footer.RowState);

        //'have to add in the right number of cells
        //'this also copies any styles over from the original footer
        for (int i = 0; i <= numCells - 1; i++)
        {
            TableCell emptyCell = new TableCell();
            if (i == 0)
            {
                emptyCell.HorizontalAlign = HorizontalAlign.Left;
                emptyCell.Text = "Total including Opening balance";
            }
            else
            {
                emptyCell.Text = String.Format("Rs. {0:#,##,##,###.00}", Convert.ToInt32((ds.Tables[0].Compute("SUM([" + ds.Tables[0].Columns[i].ColumnName + "])", string.Empty))));
            }
            newRow.Cells.Add(emptyCell);
        }

        //newRow.Cells[5].Text = "Total Discount:";
        //newRow.Cells(6).Text = "55.00";

        //'add new row to the gridview table, at the very bottom
        ((Table)grid.Controls[0]).Rows.Add(newRow);
        ViewState["ds"] = ds.Tables[0];
    }
}
