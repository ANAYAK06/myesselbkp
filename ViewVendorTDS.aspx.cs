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


public partial class ViewVendorTDS : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);

    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
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
            //trexcel.Style.Add("display", "none");
            fillcostcenter();
            trcategory.Visible = false;
            oldpage.Visible = false;
        }

        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            oldpage.Visible = true;
        }
    }
    
    public void fillcostcenter()
    {
        da = new SqlDataAdapter("select (cc_code+' , '+cc_name)as name,cc_code from Cost_Center", con);
        da.Fill(ds, "cc");
        if (ds.Tables["cc"].Rows.Count > 0)
        {

            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataTextField = "name";
            ddlCCcode.DataSource = ds.Tables["cc"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
            ddlCCcode.Items.Insert(1, "Select All");
        }
        else
        {
            ddsdcaitcode.Items.Insert(0, "Select");
        }
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcategory.SelectedValue == "SubDca")
        {
            da = new SqlDataAdapter("SELECT (subdca_code+' , '+subdca_name)as name ,subdca_code from subdca where dca_code='DCA-45'", con);
            da.Fill(ds, "sdcas");
            if (ds.Tables["sdcas"].Rows.Count > 0)
            {
                trcategory.Visible = true;
                ddsdcaitcode.DataValueField = "subdca_code";
                ddsdcaitcode.DataTextField = "name";
                ddsdcaitcode.DataSource = ds.Tables["sdcas"];
                ddsdcaitcode.DataBind();
                ddsdcaitcode.Items.Insert(0, "Select");
                ddsdcaitcode.Items.Insert(1, "Select All"); 
            }
            else
            {
                trcategory.Visible = false;
                ddsdcaitcode.Items.Insert(0, "Select");
            }
        }
        else if (ddlcategory.SelectedValue == "ItCode")
        {
            da = new SqlDataAdapter("select (i.it_code+' , '+i.it_name)as name ,i.it_code FROM it i join subdca sd on i.it_code=sd.it_code where sd.dca_code='DCA-45'", con);
            da.Fill(ds, "its");
            if (ds.Tables["its"].Rows.Count > 0)
            {
                trcategory.Visible = true;
                ddsdcaitcode.DataValueField = "it_code";
                ddsdcaitcode.DataTextField = "name";
                ddsdcaitcode.DataSource = ds.Tables["its"];
                ddsdcaitcode.DataBind();
                ddsdcaitcode.Items.Insert(0, "Select");
                ddsdcaitcode.Items.Insert(1, "Select All"); 
            }
            else
            {
                trcategory.Visible = false;
                ddsdcaitcode.Items.Insert(0, "Select");
            }
        }

        else if (ddlcategory.SelectedValue == "SelectAll" || ddlcategory.SelectedValue == "Select")
        {
            trcategory.Visible = false;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["print"] != null)
        {
            if (ViewState["print"].ToString() == "ok")
            {
                gvvendortds.AllowPaging = false;
                fillgrid();
                Context.Response.ClearContent();
                Context.Response.ContentType = "application/ms-excel";

                Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", txtfrom.Text+','+ txtto.Text));
                Context.Response.Charset = "";
                System.IO.StringWriter stringwriter = new System.IO.StringWriter();
                HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
                gvvendortds.RenderControl(htmlwriter);
                gvtdsit.RenderControl(htmlwriter);
                Context.Response.Write(stringwriter.ToString());
                Context.Response.End();
            }
        }
        else
        {
            JavaScript.UPAlert(Page, "No report to convert in to excel");
        }

    }
    public void fillgrid()
     {
         try
         {
             da = new SqlDataAdapter("usp_viewvendortds", con);
             da.SelectCommand.CommandType = CommandType.StoredProcedure;
             da.SelectCommand.Parameters.AddWithValue("@CCCode", ddlCCcode.SelectedValue);
             da.SelectCommand.Parameters.AddWithValue("@Category", ddlcategory.SelectedValue);
             da.SelectCommand.Parameters.AddWithValue("@itsdca", ddsdcaitcode.SelectedValue);
             da.SelectCommand.Parameters.AddWithValue("@FromDate", txtfrom.Text);
             da.SelectCommand.Parameters.AddWithValue("@ToDate", txtto.Text);
             da.SelectCommand.Parameters.AddWithValue("@Type", "View");
             da.Fill(ds, "tdsData");
             if (ds.Tables["tdsData"].Rows.Count > 0)
             {
                
                 gvvendortds.DataSource = ds.Tables["tdsData"];
                 gvvendortds.DataBind();
                 gvtdsit.DataSource = ds.Tables["tdsData1"];
                 gvtdsit.DataBind();
                 gvtdsit.Visible = true;
                 trexcel.Style.Add("display", "block");
                 ViewState["print"] = "ok";
             }
             else
             {
                 gvvendortds.EmptyDataText = "No Data Avaliable for the selection criteria";
                 gvvendortds.DataSource = null;                
                 gvtdsit.DataSource = null;
                 gvvendortds.DataBind();
                 gvtdsit.Visible = false;
                 trexcel.Style.Add("display", "none");
                 ViewState["print"] = "Cancel";
             }
              
         }
         catch (Exception ex)
         {
             Utilities.CatchException(ex);
             JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
         }

     }
     protected void btnAssign_Click(object sender, EventArgs e)
     { 
         fillgrid();
     }
     private decimal Basic = (decimal)0.0;
     private decimal Tds = (decimal)0.0;
     private decimal TdsBal = (decimal)0.0;
     private decimal SUMTdsBal = (decimal)0.0;
     string oldValue = string.Empty;
     string newValue = string.Empty; 
     protected void gvvendortds_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         
         if (e.Row.RowType == DataControlRowType.DataRow)
         {

             if (e.Row.Cells[6].Text == "94C")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FADBD8");
             }
             if (e.Row.Cells[6].Text == "94I")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#E8DAEF");
             }
             if (e.Row.Cells[6].Text == "94J")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D6EAF8");
             }
             if (e.Row.Cells[6].Text == "92B")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D4EFDF");
             }
             if (e.Row.Cells[6].Text == "94A")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FCF3CF");
             }
             if (e.Row.Cells[6].Text == "94H")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F6DDCC");
             }
             Basic += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Basicvalue"));
             Tds += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TdsAmount"));
             TdsBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TdsAmountBal"));
         }
         if (e.Row.RowType == DataControlRowType.Footer)
         {
             e.Row.Cells[7].Text = String.Format("Rs. {0:#,##,##,###.00}", Basic);
             e.Row.Cells[8].Text = String.Format("Rs. {0:#,##,##,###.00}", Tds);
             e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", TdsBal);
             
         }
     }
     private decimal ITTdsBal = (decimal)0.0;
     private decimal ITBasic = (decimal)0.0;
     protected void gvtdsit_RowDataBound(object sender, GridViewRowEventArgs e)
     {

         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             if (e.Row.Cells[1].Text == "94C")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FADBD8");
             }
             if (e.Row.Cells[1].Text == "94I")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#E8DAEF");
             }
             if (e.Row.Cells[1].Text == "94J")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D6EAF8");
             }
             if (e.Row.Cells[1].Text == "92B")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D4EFDF");
             }
             if (e.Row.Cells[1].Text == "94A")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FCF3CF");
             }
             if (e.Row.Cells[1].Text == "94H")
             {
                 e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F6DDCC");
             }
             ITTdsBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TdsAmountBal"));
             ITBasic += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Basicvalue"));
         }
         if (e.Row.RowType == DataControlRowType.Footer)
         {
             e.Row.Cells[4].Text = String.Format("Rs. {0:#,##,##,###.00}", ITTdsBal);
             e.Row.Cells[3].Text = String.Format("Rs. {0:#,##,##,###.00}", ITBasic);
         }
     }
     protected void oldpage_Click(object sender, EventArgs e)
     {
         Response.Redirect("frmviewtdspayment.aspx");
     }
     //string currentId = "";
     //decimal subTotal = 0;
     //decimal total = 0;
     //decimal Basic = 0;
     //decimal BasicM = 0;
     //decimal TdsM = 0;
     //decimal Tds = 0;
     //int subTotalRowIndex = 0;
     //protected void OnRowCreated(object sender, GridViewRowEventArgs e)
     //{
     //    subTotal = 0;
     //    if (e.Row.RowType == DataControlRowType.DataRow)
     //    {
     //        if (e.Row.DataItem as DataRowView != null)
     //        {
     //            DataTable dtt = (e.Row.DataItem as DataRowView).DataView.Table;
     //            string itcode = dtt.Rows[e.Row.RowIndex]["it_code"].ToString();
     //            total += Convert.ToDecimal(dtt.Rows[e.Row.RowIndex]["TdsAmountBal"]);
     //            Basic += Convert.ToDecimal(dtt.Rows[e.Row.RowIndex]["Basicvalue"]);
     //            Tds += Convert.ToDecimal(dtt.Rows[e.Row.RowIndex]["TdsAmount"]);
     //            if (itcode != currentId)
     //            {
     //                if (e.Row.RowIndex > 0)
     //                {
     //                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
     //                    {
     //                        BasicM += Convert.ToDecimal(gvvendortds.Rows[i].Cells[7].Text);
     //                        TdsM += Convert.ToDecimal(gvvendortds.Rows[i].Cells[8].Text);
     //                        subTotal += Convert.ToDecimal(gvvendortds.Rows[i].Cells[9].Text);                            
     //                    }
     //                    this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
     //                    subTotalRowIndex = e.Row.RowIndex;
     //                }
     //                currentId = itcode;
     //            }
     //        }
     //    }
     //}

     //private void AddTotalRow(string labelText, string value)
     //{
     //    if (gvvendortds != null)
     //    {
     //        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
     //        row.BackColor = ColorTranslator.FromHtml("#B22222");
     //        row.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
     //        row.Cells.AddRange(new TableCell[10] { new TableCell (), //Empty Cell
     //        new TableCell (), //Empty Cell
     //        new TableCell (), //Empty Cell
     //        new TableCell (), //Empty Cell
     //        new TableCell (), //Empty Cell
     //        new TableCell (), //Empty Cell
     //        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
     //        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right },
     //        new TableCell (), //Empty Cell          
     //        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });
     //        gvvendortds.Controls[0].Controls.Add(row);
     //    }
     //}
     //protected void OnDataBound(object sender, EventArgs e)
     //{
     //    for (int i = subTotalRowIndex; i < gvvendortds.Rows.Count; i++)
     //    {
     //        BasicM += Convert.ToDecimal(gvvendortds.Rows[i].Cells[7].Text);
     //        subTotal += Convert.ToDecimal(gvvendortds.Rows[i].Cells[9].Text);
             
     //    }
     //    this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
     //    this.AddTotalRow("Total", BasicM.ToString("N2"));
     //}
}