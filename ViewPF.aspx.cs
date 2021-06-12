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


public partial class ViewPF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

      
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
            }
            if (ddlcccode.SelectedValue != "" && ddlcccode.SelectedValue != "select All")
            {
                condition = condition + " and cc_code='" + ddlcccode.SelectedValue + "'";

            }
            if (ddldcacode.SelectedItem.Text != "Select All")
            {
                condition = condition + " and dca_code='" + ddldcacode.SelectedItem.Text + "'";

            }
            if (ddldetailhead.SelectedItem.Text != "Select All")
            {
                condition = condition + " and sub_dca='" + ddldetailhead.SelectedItem.Text + "'";

            }
            da = new SqlDataAdapter("select cc_code,sub_dca,bank_name,no,debit,description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-')as modifieddate from bankbook where paymenttype='PF' and status not in ('Rejected') " + condition + " order by id desc", con);
            da.Fill(ds, "grid");
            if (ds.Tables["grid"].Rows.Count > 0)
            {
                
                GridView1.DataSource = ds.Tables["grid"];
                GridView1.DataBind();
                trexcel.Visible = true;
                
            }
            else
            {
                GridView1.DataSource = "";
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
    private decimal Amount = (decimal)0.0;
    

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
          
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);
           
        }

    }
    protected void ddldcacode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldetailhead.Items.Clear();
        if (ddldcacode.SelectedItem.Text == "DCA-01")
        {
            ddldetailhead.Items.Add("Select SubDca");
            ddldetailhead.Items.Add("Select All");
            ddldetailhead.Items.Add("DCA-01 .5");
            ddldetailhead.Items.Add("DCA-01 .8");
        }
        else if (ddldcacode.SelectedItem.Text == "DCA-02")
        {
            ddldetailhead.Items.Add("Select SubDca");
            ddldetailhead.Items.Add("Select All");
            ddldetailhead.Items.Add("DCA-02 .5");
            ddldetailhead.Items.Add("DCA-02 .7");
        }
        else if (ddldcacode.SelectedItem.Text == "Select All")
        {
            ddldetailhead.Items.Add("Select SubDca");
            ddldetailhead.Items.Add("Select All");
        }
        else
        {
            ddldetailhead.Items.Add("Select SubDca");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {

        
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View PF"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        //GridView1.DataBind();
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

}
