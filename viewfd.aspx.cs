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

public partial class viewfd : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Default.aspx");
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
        ddlyear.Items.Insert(0, "Select Year");
    }
    public void fillgrid()
    {
        try
        {

            //da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [Date],Description,ISNULL(Cast(credit AS VARCHAR(10)), 'No Credit')as credit,ISNULL(Cast(debit AS VARCHAR(10)), 'No Debit')as debit from bankbook where paymenttype='Unsecured Loan'", con);
            da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Description, replace(isnull(credit,0),'.0000','.00')[Credit],replace(isnull(debit,0),'.0000','.00')[Debit] from bankbook where (paymenttype='FD' or dca_code='DCA-37')" + ViewState["Query1"].ToString() + " order by date asc", con);
            da.Fill(ds, "grid");
            if (ds.Tables["grid"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["grid"];
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    condition = condition + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;

                }
                else
                {
                    condition = condition + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                }
                ViewState["Query1"] = condition;
                fillgrid();
            }

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    private decimal Amount = (decimal)0.0;
    private decimal Amount1 = (decimal)0.0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "credit"));
            Amount1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);
            e.Row.Cells[3].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount1);
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
        string s = " View Fixeddeposit.xls";
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", s));
        Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
        fillgrid();
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
