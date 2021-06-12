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

public partial class Admin_frmBankStatement : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
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
            Statement.Visible = false;
            trexcel.Visible = false;
            trlbl.Visible = false;
        }
    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        paymenttype();
    }
    private void paymenttype()
    {
       Statement.Visible= true;
       trlbl.Visible = false;

        if (rbtntype.SelectedIndex == 0)
        {
            paytype.Visible = true;
            Search.Visible = false;
            
        }
        else if (rbtntype.SelectedIndex == 1)
        {
            paytype.Visible = true;
            Search.Visible = true;
        }
        else
        {
            Statement.Visible = false;
        }
        
    }
    
    protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid(GridView1);
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Condition = "";
            string Calc = "";
            Condition = Condition + "and COALESCE(convert(datetime,modifieddate),convert(datetime,Date)) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            Calc = Calc + "and COALESCE(convert(datetime,modifieddate),convert(datetime,Date)) between '" + txtfrom.Text + "' and '" + txtto.Text + "'";
            if (rbtntype.SelectedIndex == 1)
            {
                if (txtSearch.Text.Substring(0, 2) == "DC")
                {
                    Condition = Condition + " and dca_code='" + txtSearch.Text + "'";
                    Calc = Calc + " and dca_code='" + txtSearch.Text + "'";
                }
                else
                {
                    Condition = Condition + " and paymenttype='" + txtSearch.Text + "'";
                    Calc = Calc + " and paymenttype='" + txtSearch.Text + "'";
                }
            }
            ViewState["Condition"] = Condition;
            ViewState["Calc"] = Calc;
            //GridView1.PageIndex = -1;
            FillGrid(GridView1);
            FillGrid1();
            trexcel.Visible = true;
            trlbl.Visible = true;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    private void FillGrid1()
    {
        da = new SqlDataAdapter("SELECT CAST(sum(Credit) AS Decimal(15,2))[Total Credit],CAST(sum(Debit) AS Decimal(15,2))[Total Debit] FROM [BankBook]  where id>0 " + ViewState["Calc"].ToString() + " and [Type] is null and bank_name='" + ddlfrom.SelectedItem.Text + "' and (status is null or status='Debited' or status='3' or status='Approved' or status='2A' or status='I3')", con);
        da.Fill(ds, "central1");
        GridView2.DataSource = ds.Tables["central1"];
        GridView2.DataBind();

    }
    private void FillGrid(GridView gridControl)
    {
        da = new SqlDataAdapter("SELECT COALESCE([modifiedDate],Date) as modifiedDate,[PaymentType],[Name],[Description],[ModeOfPay],[No],[CC_Code],[DCA_Code],replace([Credit],'.0000','.00') as Credit,replace([Debit],'.0000','.00') as Debit,replace([Balance],'.0000','.00') as Balance FROM [BankBook]  where  id>0 " + ViewState["Condition"].ToString() + " and [Type] is null and bank_name='" + ddlfrom.SelectedItem.Text + "' and (status is null or status='Debited' or status='3' or status='Approved' or status='2A' or status='I3') order by modifieddate desc,id desc", con);
        da.Fill(ds, "central");
        gridControl.DataSource = ds.Tables["central"];
        gridControl.DataBind();
        da = new SqlDataAdapter("select isnull(balance,0) from bank_branch where bank_name='" + ddlfrom.SelectedItem.Text + "'", con);
        da.Fill(ds, "totbal");
        lbltot.Text = "Available Balance " + ds.Tables["totbal"].Rows[0][0].ToString().Replace(".0000", ".00");

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmBankstatement.aspx");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView3.AllowPaging = false;
        FillGrid(GridView3);
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "BankStatement"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView3.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
}
