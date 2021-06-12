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


public partial class CashCreditSummary : System.Web.UI.Page
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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = "Against " + (Convert.ToInt32(ddlyear.SelectedValue) - 1).ToString() + "-" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString().Substring(2, 2);
            e.Row.Cells[3].Text = "Against " + (Convert.ToInt32(ddlyear.SelectedValue) - 2).ToString() + "-" + (Convert.ToInt32(ddlyear.SelectedValue) - 1).ToString().Substring(2, 2);
            e.Row.Cells[4].Text = "Against " + (Convert.ToInt32(ddlyear.SelectedValue) - 3).ToString() + "-" + (Convert.ToInt32(ddlyear.SelectedValue) - 2).ToString().Substring(2, 2);
            e.Row.Cells[5].Text = "Against " + (Convert.ToInt32(ddlyear.SelectedValue) - 4).ToString() + "-" + (Convert.ToInt32(ddlyear.SelectedValue) - 3).ToString().Substring(2, 2);
            e.Row.Cells[6].Text = "Against " + (Convert.ToInt32(ddlyear.SelectedValue) - 5).ToString() + "-" + (Convert.ToInt32(ddlyear.SelectedValue) - 4).ToString().Substring(2, 2);



        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Cash += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CurrentFY"));
            Cheque += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Prev"));
            PaidTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Prev1"));
            CashPending += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Prev2"));
            ChequePending += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Prev3"));
            PendingTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Prev4"));
            CashTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = String.Format("Rs. {0:#,##,##,###.00}", Cash);
            e.Row.Cells[2].Text = String.Format("Rs. {0:#,##,##,###.00}", Cheque);
            e.Row.Cells[3].Text = String.Format("Rs. {0:#,##,##,###.00}", PaidTotal);
            e.Row.Cells[4].Text = String.Format("Rs. {0:#,##,##,###.00}", CashPending);
            e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", ChequePending);
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", PendingTotal);
            e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", CashTotal);



        }

    }

    private decimal Cash = (decimal)0.0;
    private decimal Cheque = (decimal)0.0;
    private decimal PaidTotal = (decimal)0.0;
    private decimal CashPending = (decimal)0.0;
    private decimal ChequePending = (decimal)0.0;
    private decimal PendingTotal = (decimal)0.0;
    private decimal CashTotal = (decimal)0.0;
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("CashSummary_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year2", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) - 1).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year3", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) - 2).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year4", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) - 3).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year5", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) - 4).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Year6", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) - 5).ToString();
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";
        da.Fill(ds, "credit");

        dv = new DataView(ds.Tables[0], "", "Paymenttype", DataViewRowState.CurrentRows);
        if (ddlyear.SelectedItem.Text == "2010-11")
            dv.Table.Rows[1].Delete();
        else
            dv.Table.Rows[0].Delete();
        GridView1.DataSource = dv;
        GridView1.DataBind();
        trexcel.Visible = true;

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
