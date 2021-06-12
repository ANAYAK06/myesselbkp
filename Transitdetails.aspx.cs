using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

public partial class Transitdetails : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["refno"] == null))
            {
                JavaScript.CloseWindow();
            }
            else
            {
                fillgrid();
            }
        }

    }
    public void fillgrid()
    {
        da = new SqlDataAdapter("Select it.item_code,item_name,Specification,Units,quantity,Transfer_Date,Recieved_cc,ti.cc_code from [Transfer Info] ti join [Items Transfer] it on it.Ref_no=ti.Ref_no join Item_codes ic on ic. Item_code=Substring(it.item_code,1,8) where ti.ref_no='" + Request.QueryString["refno"].ToString() + "'", con);
        da.Fill(ds, "filldata");
        if (ds.Tables["filldata"].Rows.Count > 0)
        {
            indentgrid.DataSource = ds.Tables["filldata"];
            indentgrid.DataBind();               
          
        }
        else
        {
            JavaScript.CloseWindow();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        fillgrid();
        Response.ClearContent();
        Response.Buffer = true;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Transit Indent Details "));
        Context.Response.Charset = "";

        string headerrow = @" <table><tr><td colspan='2'></td><td align='center' colspan='2'><h3> Transit Indent Details" + " <h3></td><td align='left' colspan='4'><h3></h3> </td></tr></table> ";
        string headerow3 = @" <table> <tr > <td align='center' colspan='10'> </td> </tr></table>";

        Response.Write(headerrow);
        Response.Write(headerow3);
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        indentgrid.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();       
    }
}