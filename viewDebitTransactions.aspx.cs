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
using System.IO;
using System.Text;
using AjaxControlToolkit;
using System.Collections.Specialized;
using System.Web.Services;
using System.Web.Services.Protocols;

public partial class viewDebitTransactions : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    string gvUniqueID = String.Empty;
    int gvNewPageIndex = 0;
    int gvEditIndex = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            Statement.Visible = false;
            LoadYear();
        }


    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        paymenttype();
    }
    private void paymenttype()
    {
        Statement.Visible = true;

        if (rbtntype.SelectedIndex == 0)
        {
            paytype.Visible = false;
            Search.Visible = true;
        }
        else if (rbtntype.SelectedIndex == 1)
        {
            paytype.Visible = true;
            Search.Visible = false;
        }
        else
        {
            Statement.Visible = false;
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


    public void vendetails()
    {
        try
        {
            da = new SqlDataAdapter("select Transaction_No,CC_Code,Dca_Code,SDCA_Code,Party_Name,Bank_Name,Mode_Of_Pay,Cheque_No,Replace(isnull(Amount,0),'.0000','.00')as Amount,(Select first_name from register where user_name=B.created_by)Created_By,(Select first_name from register where user_name=b.Modified_By)Modified_By,REPLACE(CONVERT(VARCHAR(11),Created_Date, 106), ' ', '-')Created_Date  from BankTransactions B  where id>0  and status not in ('Rejected','4') " + ViewState["Condition"].ToString() + "", con);
            da.Fill(ds, "vendetail");
            GridView1.DataSource = ds.Tables["vendetail"];
            GridView1.DataBind();
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
            GridViewRow row = e.Row;

            if (row.DataItem == null)
            {
                return;
            }
            GridView gv = new GridView();
            gv = (GridView)row.FindControl("GridView2");
            DataSet ds1 = new DataSet();
            ds1.Reset();
            if (gv.UniqueID == gvUniqueID)
            {
                gv.PageIndex = gvNewPageIndex;
                gv.EditIndex = gvEditIndex;
                ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["Transaction_No"].ToString() + "','one');</script>");
            }
            da = new SqlDataAdapter("select InvoiceNo,CC_Code,DCA_Code,Sub_DCA,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,Replace(isnull(debit,0),'.0000','.00')as debit  from BankBook where Transaction_No='" + ((DataRowView)e.Row.DataItem)["Transaction_No"].ToString() + "'", con);
            da.Fill(ds1, "fillgrid2");
            gv.DataSource = ds1.Tables["fillgrid2"];
            gv.DataBind();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

        }
    }

    private decimal Amount = (decimal)0.0;
    private decimal grdTotal = 0;

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
            grdTotal = grdTotal + rowTotal;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotal");
            lbl.Text = grdTotal.ToString();
            grdTotal = 0;
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        vendetails();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        try
        {
            string Condition = "";


            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    Condition = Condition + "and datepart(mm,created_date)=" + ddlmonth.SelectedValue + " and datepart(yy,created_date)=" + yy;
                }
                else
                {
                    Condition = Condition + "and  convert(datetime,created_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                }
            }
            if (rbtntype.SelectedIndex == 0)
            {

                Condition = Condition + " and Transaction_No='" + txtSearch.Text + "'";
            }

            ViewState["Condition"] = Condition;

            GridView1.PageIndex = 1;
            vendetails();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
}
