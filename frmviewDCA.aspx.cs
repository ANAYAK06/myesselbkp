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
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Collections.Specialized;
using AjaxControlToolkit;
using System.IO;
using System.Text;

public partial class Admin_frmviewDCA : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlDataReader dr;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["user"] == null)
        {            
            Response.Redirect("SessionExpire.aspx");
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        loadgrid();       

    }

    private void loadgrid()
    {
        //da = new SqlDataAdapter("Select d.dca_code,dca_name,subdca_code,subdca_name,isnull(s.it_code,d.it_code) it_code from dca d left outer join subdca s on d.dca_code=s.dca_code", con);
        da = new SqlDataAdapter("Select dca_code,mapdca_code,Dca_name,It_code from dca", con); 
        da.Fill(ds, "dca");
        GridView2.DataSource = ds.Tables["dca"];
        GridView2.DataBind();
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = new DataTable();
            da = new SqlDataAdapter("Select subdca_code,mapsubdca_code,subdca_name,it_code from subdca where dca_code='" + GridView2.DataKeys[e.Row.RowIndex]["dca_code"].ToString() + "'", con);
            da.Fill(dt);
            GridView gd = (GridView)e.Row.FindControl("GridView1");
            gd.DataSource = dt;
            gd.DataBind();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }


    protected void PrintAllPages(object sender, EventArgs e)
    {
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView2.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
    }

    
   

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        
        
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View DCA"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        loadgrid();
        GridView2.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
}
