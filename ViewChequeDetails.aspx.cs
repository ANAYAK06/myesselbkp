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

public partial class ViewChequeDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
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
            trexcel.Visible = false;
        }
    }
    protected void ddlbankname_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        try
        {
            //da = new SqlDataAdapter("select cm.bankname as [bankname],REPLACE(CONVERT(VARCHAR(11),cm.issuedate, 106), ' ', '-')as [issuedate],cn.chequeno as [chequeno],bt.Party_Name ,REPLACE(CONVERT(VARCHAR(11),bt.Created_Date, 106), ' ', '-')as [Created_date],cn.status as [status] from cheque_Master cm  join Cheque_Nos cn on cm.chequeid=cn.chequeid join BankTransactions bt on bt.Cheque_No=cn.chequeno where cm.bankname='" + ddlbankname.SelectedItem.Text + "' and cn.status not in ('1') order by cn.chequeno ", con);
            da = new SqlDataAdapter("select cm.bankname as [bankname],REPLACE(CONVERT(VARCHAR(11),cm.issuedate, 106), ' ', '-')as [issuedate],cn.chequeno as [chequeno],bt.Party_Name ,REPLACE(CONVERT(VARCHAR(11),bt.Created_Date, 106), ' ', '-')as [Created_date],cn.status as [status] from cheque_Master cm  join Cheque_Nos cn on cm.chequeid=cn.chequeid left join BankTransactions bt on bt.Cheque_No=cn.chequeno where cm.bankname='" + ddlbankname.SelectedItem.Text + "' and bt.bank_name='" + ddlbankname.SelectedItem.Text + "' and cn.status not in ('1') and bt.mode_of_pay='Cheque'  union select cm.bankname as [bankname],REPLACE(CONVERT(VARCHAR(11),cm.issuedate, 106), ' ', '-')as [issuedate],cn.chequeno as [chequeno],null,null,cn.status as [status] from cheque_Master cm  join Cheque_Nos cn on cm.chequeid=cn.chequeid  where cm.bankname='" + ddlbankname.SelectedItem.Text + "'  and cn.status  in ('2')  order by cn.chequeno", con);

            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["fill"];
                GridView1.DataBind();
                trexcel.Visible = true;
            }
            else
            {
                GridView1.DataSource = "";
                GridView1.DataBind();
                
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "2")
                    e.Row.Cells[5].Text = "Unused";
                else if (e.Row.Cells[5].Text == "3")
                    e.Row.Cells[5].Text = "Used";
                //else if (e.Row.Cells[4].Text == "4")
                //    e.Row.Cells[4].Text = "Closed";


            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
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
        string s = ddlbankname.SelectedValue + ".xls";
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
