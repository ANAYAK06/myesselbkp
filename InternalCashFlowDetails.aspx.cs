using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Configuration;

public partial class InternalCashFlowDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (Request.QueryString["Type"] == null)
            {
                JavaScript.CloseWindow();

            }

            else
            {
                
                filldata();
               
            
            }
        }

    }
    public void filldata()
    {
        if (Request.QueryString["Type"].ToString() == "1")
        {
            trcccode.Visible = false;
            if (Request.QueryString["cccode"].ToString() != "CCC ")
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,cc_code,description,replace(amount,'.0000','.00')as amount from internalcashflow where cc_code='" + Request.QueryString["cccode"].ToString() + "' and convert(datetime,date) between '04/01/" + Request.QueryString["year1"].ToString() + "' and '03/31/" + Request.QueryString["year2"].ToString() + "' order by date desc", con);
            else
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,cc_code,description,replace(amount,'.0000','.00')as amount from internalcashflow where convert(datetime,date) between '04/01/" + Request.QueryString["year1"].ToString() + "' and '03/31/" + Request.QueryString["year2"].ToString() + "' order by date desc ", con);

        }
        else if (Request.QueryString["Type"].ToString() == "2")
        {
            filldropdown();
            trcccode.Visible = true;
            da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,cc_code,description,replace(amount,'.0000','.00')as amount from internalcashflow where convert(datetime,date) between '04/01/" + Request.QueryString["year1"].ToString() + "' and '03/31/" + Request.QueryString["year2"].ToString() + "' order by date desc", con);

        }
        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
        }
        else
        {
            JavaScript.CloseWindow();
        }
    }

    private decimal Amount = (decimal)0.0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = Amount.ToString();
        }
    }

    public void filldropdown()
    {
        SqlDataAdapter da = new SqlDataAdapter("select distinct CC_code from internalcashflow", con);
        da.Fill(ds, "Dropdown");
        ddlpopcccode.DataTextField = "CC_code";
        ddlpopcccode.DataValueField = "CC_code";
        ddlpopcccode.DataSource = ds.Tables["Dropdown"];
        ddlpopcccode.DataBind();
        ddlpopcccode.Items.Insert(0, "Select CC Code");
        ddlpopcccode.Items.Insert(1, "Select All");
       
    }
    protected void ddlpopcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlpopcccode.SelectedItem.Text!="Select All")
        da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,cc_code,description,replace(amount,'.0000','.00')as amount from internalcashflow where convert(datetime,date) between '04/01/" + Request.QueryString["year1"].ToString() + "' and '03/31/" + Request.QueryString["year2"].ToString() + "' and cc_code='" + ddlpopcccode.SelectedItem.Text + "' order by date desc", con);
        else
            da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,cc_code,description,replace(amount,'.0000','.00')as amount from internalcashflow where convert(datetime,date) between '04/01/" + Request.QueryString["year1"].ToString() + "' and '03/31/" + Request.QueryString["year2"].ToString() + "'  order by date desc", con);
        da.Fill(ds, "select");
        if (ds.Tables["select"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["select"];
            GridView1.DataBind();
        }
        else
        {
            JavaScript.CloseWindow();
        }
    }
}
