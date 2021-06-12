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
using System.Web.Services;
using AjaxControlToolkit;
using System.Collections.Generic;


public partial class StockLedger : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            trnorcrds.Visible = false;
            clear();
            grid.Visible = false;
            Tr2.Visible = false;
        }
    }
   
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["BalQty"] = null;
            string itemcode;
            string type="";
            char first = txtitemname.Text[0];

            if (char.IsLetter(first))
            {
                string input = txtitemname.Text;
                itemcode = input.Substring(input.IndexOf('|') + 1);
            }
            else
                itemcode = txtitemname.Text;

           // ViewState["itemcode"] = itemcode;

            da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_code='" + itemcode.Substring(0, 8) + "' and status in ('4','5','5A','2A')", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)
                {
                    
                    if (rbtntype.SelectedIndex == 0)
                        type = "1";
                    else if (rbtntype.SelectedIndex == 1)
                        type = "2";

                    da = new SqlDataAdapter("StockLedger_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@dateFrom", txtfrom.Text);
                    da.SelectCommand.Parameters.AddWithValue("@dateTo", txtTo.Text);
                    da.SelectCommand.Parameters.AddWithValue("@CCCode", ddlcccode.SelectedValue);
                    da.SelectCommand.Parameters.AddWithValue("@itemcode", itemcode);
                    da.SelectCommand.Parameters.AddWithValue("@type", type);
                    da.Fill(ds, "fill");
                    da.Fill(ds, "fill1");

                    if (ds.Tables["fill"].Rows.Count > 0)
                    {
                        trnorcrds.Visible = false;
                        grid.Visible = true;
                        Tr2.Visible = true;
                        var result = (from a in ds.Tables["fill"].AsEnumerable() select new { item_code = a["item_code"].ToString(), item_name = a["item_name"].ToString(), specification = a["specification"].ToString(), units = a["units"].ToString() }).Distinct().ToList();

                        //var result2 = (from b in ds.Tables["fill"].AsEnumerable() select new { item_code = b["item_code"].ToString(), basic_price = b["basic_price"].ToString(), Issuedqty = b["Issuedqty"].ToString(), recieved_date = b["recieved_date"].ToString(), remarks = b["remarks"].ToString(), Recievedqty = b["Recievedqty"].ToString(), No = b["No"].ToString() }).Distinct().ToList();

                        grd.DataSource = result;
                        grd.DataBind();
                        grddetail.DataSource = ds.Tables["fill"];
                        grddetail.DataBind();
                    }
                    else
                    {
                        ds.Clear();
                        trnorcrds.Visible = true;
                        grid.Visible = false;
                        Tr2.Visible = false;
                    }
                }
                else
                {
                    grid.Visible = false;
                    Tr2.Visible = false;
                    rbtntype.ClearSelection();
                    JavaScript.UPAlert(Page, "Invalid itemcode");
                    (UpdatePanel1.FindControl("txtitemname") as TextBox).Text = String.Empty;
                    txtitemname.Text = String.Empty;
                    
                }
    }    
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["datefrom"] = txtfrom.Text;
        Session["dateto"] = txtTo.Text;
        Session["cccode"] = ddlcccode.SelectedValue;
        txtitemname.Text = "";
        rbtntype.ClearSelection();
        grid.Visible = false;
        Tr2.Visible = false;
    }
    private decimal balstock = (decimal)0.0;
    private decimal closestock = (decimal)0.0;
    protected void grddetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
            decimal RQ = Convert.ToDecimal(e.Row.Cells[5].Text.ToString());
            decimal IQ = Convert.ToDecimal(e.Row.Cells[6].Text.ToString());
           
            if (ViewState["BalQty"] == null)
              {
                  balstock = ((Convert.ToDecimal(ViewState["OpenStock"])+ RQ) - IQ);
                  if (balstock < 0)                
                      e.Row.Cells[7].Text = "0";               
                  else
                      e.Row.Cells[7].Text = balstock.ToString();

                  ViewState["BalQty"] = e.Row.Cells[7].Text;
              }
              else
              {
                  e.Row.Cells[7].Text = (Convert.ToDecimal(ViewState["BalQty"]) + RQ - IQ).ToString();
                  ViewState["BalQty"] = e.Row.Cells[7].Text;
              } 
           
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var result1 = (from a in ds.Tables["fill1"].AsEnumerable() select new { OpenStock = a["OpenStock"].ToString(), CloseStock = a["CloseStock"].ToString() }).Distinct().ToList();
            e.Row.Cells[4].Text= result1.ToList()[0].OpenStock.ToString();
            ViewState["OpenStock"] = e.Row.Cells[4].Text;
            closestock=  Convert.ToDecimal(result1.ToList()[0].CloseStock.ToString());
            if (closestock < 0)
                e.Row.Cells[5].Text = "0";
            else
                e.Row.Cells[5].Text = closestock.ToString();
        }
    }
    public void clear()
    {
        txtfrom.Text = "";
        txtitemname.Text = "";
        txtTo.Text = "";
        ddlcccode.SelectedItem.Text = "Select Cost Center";
        rbtntype.ClearSelection();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    { 
        Response.ClearContent();
        Response.Buffer = true;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Stockledger Report "));
        Context.Response.Charset = "";
         string headerrow = @" <table> <tr > <td align='center' colspan='8'><u><h2>Stock Ledger<h2></u> </td> </tr></table>";
        //string headerrow = @" <table><tr><td colspan='2'></td><td align='center' colspan='2'><h3> " + Label1.Text + " Detailview Report" + " <h3></td><td align='left' colspan='4'><h3> Date : " + " " + lbldate.Text + "</h3> </td></tr></table> ";
        //string headerow3 = @" <table> <tr > <td align='center' colspan='10'> </td> </tr></table>";

        Response.Write(headerrow);
        //Response.Write(tablerow1);
       // Response.Write(headerow3);
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        grddetail.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
       
    }
}