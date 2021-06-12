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
using AjaxControlToolkit;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;

public partial class ViewAccruedInterest : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    DataSet ds = new DataSet();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadYear();
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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        fillgrid();

    }
    public void fillgrid()
    {
        try
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CCAccrued_interest";
            command.CommandTimeout = 100;
            if (ddlmonth.SelectedIndex != 0)
            {
                string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                command.Parameters.AddWithValue("@month", SqlDbType.VarChar).Value = ddlmonth.SelectedValue;
                command.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
                command.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = yy;
            }
            else
            {
                command.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
                command.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
                command.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";
            }
            command.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
            command.Connection = con;
            con.Open();
            SqlDataReader reader;
            reader= command.ExecuteReader();
            if (reader.HasRows)
            {
                //while (reader.Read())
                //{
                    GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                    GridView1.DataSource = reader;                   
                    GridView1.DataBind();
                //}

            }
            else
            {
                GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                GridView1.DataSource = reader;
                GridView1.DataBind();
            }
            reader.Close();           

            //da = new SqlDataAdapter("CCAccrued_interest", con);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //if (ddlmonth.SelectedIndex != 0)
            //{
            //    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
            //    da.SelectCommand.Parameters.AddWithValue("@month", SqlDbType.Int).Value = ddlmonth.SelectedValue;
            //    da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
            //    da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.Int).Value = yy;
            //}
            //else
            //{
            //    da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            //    da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            //    da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";
            //}
            //da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
         
            //da.Fill(ds, "AI");
            //GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            //GridView1.DataSource = ds.Tables["AI"];
            //GetPreviousInterest();
            //GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //--- For Paging ---------
        GridViewRow row = GridView1.BottomPagerRow;

        if (row == null)
        {
            return;
        }

        //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
        Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
        Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

        //if (lblPages != null)
        //{
        lblPages.Text = GridView1.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = GridView1.PageIndex + 1;
        lblCurrent.Text = currentPage.ToString();

        //-- For First and Previous ImageButton
        if (GridView1.PageIndex == 0)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Visible = false;



        }

        //-- For Last and Next ImageButton
        if (GridView1.PageIndex + 1 == GridView1.PageCount)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Visible = false;


        }
    }
    protected void btnFirst_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnPrev_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnNext_Command(object sender, CommandEventArgs e)
    {

        Paginate(sender, e);
    }
    protected void btnLast_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void Paginate(object sender, CommandEventArgs e)
    {
        // Get the Current Page Selected
        int iCurrentIndex = GridView1.PageIndex;

        switch (e.CommandArgument.ToString().ToLower())
        {
            case "first":
                GridView1.PageIndex = 0;
                break;
            case "prev":
                if (GridView1.PageIndex != 0)
                {
                    GridView1.PageIndex = iCurrentIndex - 1;
                }
                break;
            case "next":
                GridView1.PageIndex = iCurrentIndex + 1;
                break;
            case "last":
                GridView1.PageIndex = GridView1.PageCount;
                break;
        }

        //Populate the GridView Control
        fillgrid();
    }
    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
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
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View Accrued Interest"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        print.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            netrecieved += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "netrecieved"));
            netpaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "netpaid"));
            incf += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "incf"));
           // GetPreviousInterest();      
            // e.Row.Cells[5].Text = Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(e.Row.Cells[6].Text)).ToString();
           // e.Row.Cells[9].Text = Convert.ToDecimal(incf + Convert.ToDecimal(ds.Tables["PrevInterest"].Rows[0][0])).ToString();
            if (Convert.ToDecimal(e.Row.Cells[7].Text) < 0)
            {
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
            }
            e.Row.Cells[1].Attributes.Add("onclick", "window.open('DetailviewAccruedInterest.aspx?CCCode=" + ddlcccode.SelectedValue + "&date=" + e.Row.Cells[0].Text + "&type=2','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
            e.Row.Cells[3].Attributes.Add("onclick", "window.open('DetailviewAccruedInterest.aspx?CCCode=" + ddlcccode.SelectedValue + "&date=" + e.Row.Cells[0].Text + "&type=1','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");


        }
    }
    private decimal netrecieved = (decimal)0.0;
    private decimal netpaid = (decimal)0.0;
    private decimal incf = (decimal)0.0;
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    public void GetPreviousInterest()
    {
        da = new SqlDataAdapter("Select CAST(ISNULL(sum(incf),0) AS Decimal(25,2)) as[AccumulatedInterst] from #tbl_accruedinterest where cc_code='" + ddlcccode.SelectedValue + "' and convert(datetime,date) < '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "'", con);
        da.Fill(ds, "PrevInterest");
    }
}
