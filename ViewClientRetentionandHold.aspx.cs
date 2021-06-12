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
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;



public partial class ViewClientRetentionandHold : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
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
            trexcel.Visible = false;
        }

    }
    protected void ddltypeofpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlclientid.Items.Clear();
        ddlsubclientid.Items.Clear();
        ddlpo.Items.Clear();
        ddlcccode.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Retention")
                da = new SqlDataAdapter("select distinct client_id from invoice where   retention!='0' and client_id is not null and status!='cancel'", con);
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
                da = new SqlDataAdapter("select distinct client_id from invoice where   hold!='0' and client_id is not null and status!='cancel'", con);

            da.Fill(ds, "client");

            ddlclientid.DataTextField = "client_id";
            ddlclientid.DataValueField = "client_id";
            ddlclientid.DataSource = ds.Tables["client"];
            ddlclientid.DataBind();
            ddlclientid.Items.Insert(0, "select");
            ddlsubclientid.Items.Insert(0, "select");
            ddlcccode.Items.Insert(0, "select");
            ddlpo.Items.Insert(0, "select");
          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpo.Items.Clear();
        ddlcccode.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();

        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Retention")
                da = new SqlDataAdapter("select   distinct i.cc_code, i.cc_code+ ',' +c.cc_name as ccname  from invoice i join cost_center c on i.cc_code=c.cc_code where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and retention!='0' ", con);
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
                da = new SqlDataAdapter("select   distinct i.cc_code, i.cc_code+ ',' +c.cc_name as ccname  from invoice i join cost_center c on i.cc_code=c.cc_code where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and hold!='0' ", con);

            da.Fill(ds, "cccode");

            ddlcccode.DataTextField = "ccname";
            ddlcccode.DataValueField = "cc_code";
            ddlcccode.DataSource = ds.Tables["cccode"];
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "select");
            ddlpo.Items.Insert(0, "select");
          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpo.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Retention")
                da = new SqlDataAdapter("select distinct po_no from invoice where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and cc_code='" + ddlcccode.SelectedValue + "' and retention!='0' ", con);
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
                da = new SqlDataAdapter("select distinct po_no from invoice where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and cc_code='" + ddlcccode.SelectedValue + "' and hold!='0' ", con);

            da.Fill(ds, "pono");

            ddlpo.DataTextField = "po_no";
            ddlpo.DataValueField = "po_no";
            ddlpo.DataSource = ds.Tables["pono"];
            ddlpo.DataBind();
            ddlpo.Items.Insert(0, "select");
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsubclientid.Items.Clear();
        ddlcccode.Items.Clear();
        ddlpo.Items.Clear();
        ddlcccode.Items.Insert(0, "select");
        ddlpo.Items.Insert(0, "select");
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Retention")
                da = new SqlDataAdapter("select distinct subclient_id from invoice where  client_id='" + ddlclientid.SelectedItem.Text + "' and retention!='0' ", con);
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
                da = new SqlDataAdapter("select distinct subclient_id from invoice where  client_id='" + ddlclientid.SelectedItem.Text + "' and hold!='0' ", con);

            da.Fill(ds, "subclient");

            ddlsubclientid.DataTextField = "subclient_id";
            ddlsubclientid.DataValueField = "subclient_id";
            ddlsubclientid.DataSource = ds.Tables["subclient"];
            ddlsubclientid.DataBind();

            ddlsubclientid.Items.Insert(0, "select");


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        string condition = "";



        if (ddlclientid.SelectedItem.Text != "select")
        {
            condition = condition + " and client_id='" + ddlclientid.SelectedItem.Text + "'";

        }
        if (ddlsubclientid.SelectedItem.Text != "select")
        {
            condition = condition + " and subclient_id='" + ddlsubclientid.SelectedItem.Text + "'";

        }
        if (ddlcccode.SelectedValue != "select")
        {
            condition = condition + " and i.cc_code='" + ddlcccode.SelectedValue + "'";

        }
        if (ddlpo.SelectedItem.Text != "select")
        {
            condition = condition + " and i.po_no='" + ddlpo.SelectedItem.Text + "'";

        }
        if (ddltypeofpay.SelectedItem.Text == "Retention")
        {

            da = new SqlDataAdapter("select i.InvoiceNo,i.PO_NO,i.client_id,i.subclient_id,i.cc_code,b.bank_name,replace(isnull(b.credit,0),'.0000','.00')Amount,REPLACE(CONVERT(VARCHAR(11),b.Date, 106), ' ', '-')Date,b.description,isnull(retention_balance,0) as [Balance],replace(isnull(Retention,0),'.0000','.00')Total from invoice i left join  bankbook b on i.InvoiceNo=b.InvoiceNo where id>0  and b.paymenttype='" + ddltypeofpay.SelectedItem.Text + "'" + condition + " union  select InvoiceNo,PO_NO,client_id,subclient_id,cc_code,'' as [bank_name],'0' as [Amount],'' as [Date],'' as [description],isnull(retention_balance,0) as [Balance],replace(isnull(Retention,0),'.0000','.00')Total from invoice i  where  retention_balance=retention " + condition + " ", con);

        }
        if (ddltypeofpay.SelectedItem.Text == "Hold")
        {

            da = new SqlDataAdapter("select i.InvoiceNo,i.PO_NO,i.client_id,i.subclient_id,i.cc_code,b.bank_name,replace(isnull(b.credit,0),'.0000','.00')Amount,REPLACE(CONVERT(VARCHAR(11),b.Date, 106), ' ', '-')Date,b.description,isnull(hold_balance,0) as [Balance],replace(isnull(hold,0),'.0000','.00')Total from invoice i left join  bankbook b on i.InvoiceNo=b.InvoiceNo where id>0 " + condition + " and b.paymenttype='" + ddltypeofpay.SelectedItem.Text + "' union  select InvoiceNo,PO_NO,client_id,subclient_id,cc_code,'' as [bank_name],'0' as [Amount],'' as [Date],'' as [description],isnull(hold_balance,0) as [Balance],replace(isnull(hold,0),'.0000','.00')Total from invoice i  where  hold_balance=hold " + condition + " ", con);

        }

        da.Fill(ds, "fill");
        GridView1.DataSource = ds.Tables["fill"];
        GridView1.DataBind();
        trexcel.Visible = true;
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Total);
            e.Row.Cells[10].Text = String.Format("Rs. {0:#,##,##,###.00}", Balance);
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddltypeofpay.SelectedItem.Text == "Retention")
            {
                e.Row.Cells[10].Text = "Balance Ret Amount";
                e.Row.Cells[8].Text = "Total Ret Amount";
            }
            else if (ddltypeofpay.SelectedItem.Text == "Hold")
            {
                e.Row.Cells[10].Text = "Balance Hold Amount";
                e.Row.Cells[8].Text = "Total Hold Amount";
            }
        }
    }
    private decimal Amount = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
    private decimal Total = (decimal)0.0;
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string s = ddlcccode.SelectedValue + " Status.xls";
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

