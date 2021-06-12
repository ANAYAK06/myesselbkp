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

public partial class frmviewbankbook : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    DataSet ds = new DataSet();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {

        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        if (!IsPostBack)
        {
            tbldetails.Visible = false;
            fillgrid();
        }




    }
    public void fillgrid()
    {
        da = new SqlDataAdapter("select bank_name,accholder_name,acc_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,date,101),106), ' ', '-')as date,replace(isnull(minimum_balance,0),'.0000','.00')minimum_balance,bank_location,replace(isnull(balance,0),'.0000','.00')balance,bank_id from bank_branch where status='3'", con);
        da.Fill(ds, "bankbook");
        GridView1.DataSource = ds.Tables["bankbook"];
        GridView1.DataBind();

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
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View Budget"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
    private decimal Balance = (decimal)0.0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "balance"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Text = String.Format("{0:#,##,##,###.00}", Balance);
            if (Convert.ToDecimal(e.Row.Cells[7].Text) < 0)
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                tbldetails.Visible = true;
                da = new SqlDataAdapter("Select accholder_name,acc_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,date,101),106), ' ', '-')as date,minimum_balance,bank_location,isnull(balance,0),status,bank_name from bank_branch where bank_id='" + GridView1.DataKeys[e.NewEditIndex]["bank_id"].ToString() + "'", con);
                da.Fill(ds, "bankdetails");
                if (ds.Tables["bankdetails"].Rows.Count > 0)
                {
                    lblbankname.Text = ds.Tables["bankdetails"].Rows[0]["bank_name"].ToString();
                    lblacctholder.Text = ds.Tables["bankdetails"].Rows[0][0].ToString();
                    lblacctno.Text = ds.Tables["bankdetails"].Rows[0][1].ToString();
                    lblacopeningdate.Text = ds.Tables["bankdetails"].Rows[0][2].ToString();
                    txtminbal.Text = ds.Tables["bankdetails"].Rows[0][3].ToString().Replace(".0000", ".00");
                    lbllocation.Text = ds.Tables["bankdetails"].Rows[0][4].ToString();
                    ViewState["bank_id"] = GridView1.DataKeys[e.NewEditIndex]["bank_id"].ToString();
                    ViewState["Balance"] = ds.Tables["bankdetails"].Rows[0][5].ToString().Replace(".0000", ".00");                   
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void update_Click(object sender, EventArgs e)
    {
        try
        {
            bool positive = Convert.ToDecimal(ViewState["Balance"].ToString()) >= 0;
            bool negative = Convert.ToDecimal(ViewState["Balance"].ToString()) < 0;
            if (positive == true)
            {
                if (Convert.ToDecimal(ViewState["Balance"].ToString()) >= Convert.ToDecimal(txtminbal.Text))
                {
                    cmd.Connection = con;
                    cmd.CommandText = "Update bank_branch set minimum_balance=" + txtminbal.Text + " where bank_id=" + ViewState["bank_id"].ToString() + "";
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        JavaScript.AlertAndRedirect("Minimum balance updated sucessfully for Bank  " + lblbankname.Text, "frmviewbankbook.aspx");
                    }
                    else
                    {
                        JavaScript.AlertAndRedirect("Minimum balance updation failed for Bank  " + lblbankname.Text, "frmviewbankbook.aspx");
                    }
                }
                else
                {
                    JavaScript.AlertAndRedirect("Can not Exceed minimum balance with current bank balance for Bank  " + lblbankname.Text, "frmviewbankbook.aspx");
                }
            }
            if (negative == true)
            {
                //if (ViewState["bank_id"].ToString() != "12")
                //{
                    if (Convert.ToDecimal(ViewState["Balance"].ToString()) >= Convert.ToDecimal(txtminbal.Text))
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "Update bank_branch set minimum_balance=" + txtminbal.Text + " where bank_id=" + ViewState["bank_id"].ToString() + "";
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                        {
                            JavaScript.AlertAndRedirect("Minimum balance updated sucessfully for Bank  " + lblbankname.Text, "frmviewbankbook.aspx");
                        }
                        else
                        {
                            JavaScript.AlertAndRedirect("Minimum balance updation failed for Bank  " + lblbankname.Text, "frmviewbankbook.aspx");
                        }
                    }
                    else
                    {
                        JavaScript.AlertAndRedirect("Can not Exceed minimum balance with current bank balance for Bank  " + lblbankname.Text, "frmviewbankbook.aspx");
                    }
                //}
                //else
                //{
                //    cmd.Connection = con;
                //    cmd.CommandText = "Update bank_branch set minimum_balance=" + txtminbal.Text + " where bank_id=" + ViewState["bank_id"].ToString() + "";
                //    con.Open();
                //    int i = cmd.ExecuteNonQuery();
                //    if (i == 1)
                //    {
                //        JavaScript.AlertAndRedirect("Minimum balance updated sucessfully for Bank  " + lblbankname.Text, "frmviewbankbook.aspx");
                //    }
                //    else
                //    {
                //        JavaScript.AlertAndRedirect("Minimum balance updation failed for Bank  " + lblbankname.Text, "frmviewbankbook.aspx");
                //    }
                //}
            }
        }
        catch (Exception ex)
        {

        }
    }
}
