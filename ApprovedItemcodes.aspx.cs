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

public partial class ApprovedItemcodes : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            //fillgrid3();
            ddlitemtype.Visible = false;
            ddlitemstatus.SelectedIndex = 0;         
          
       
        }
    }
    //protected void GridView1_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid3();
    //}
    //protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid3();
    //}
    public void fillgrid3()
    {
        try
        {
            da = new SqlDataAdapter("select TOP(10) id,item_code,item_name,units,basic_price,specification,subdca_code,status from item_codes where  modified_date <> 'null' and  status = '4' ORDER BY modified_date desc", con);
            da.Fill(ds, "fillitem");
            GridView1.DataSource = ds.Tables["fillitem"];
            //GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    public void fillgrid4()
    {
        try
        {
            da = new SqlDataAdapter("select TOP(10) id,item_code,item_name,units,basic_price,specification,subdca_code,status from item_codes where  modified_date <> 'null' and  status = '2A' ORDER BY modified_date desc", con);
            da.Fill(ds, "fillitem");
            GridView1.DataSource = ds.Tables["fillitem"];
            //GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }   
    
    protected void GridView1_RowEditing2(object sender, GridViewEditEventArgs e)
    {   
        string id = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
        HiddenField hf = (HiddenField)GridView1.Rows[e.NewEditIndex].Cells[7].FindControl("h1");
        if (hf.Value == "4")
        {
            da = new SqlDataAdapter("update item_codes set status='5' where id= '" + id + "' ", con);
            da.Fill(ds, "update");
            con.Close();
            fillgrid3();
        }
        else if (hf.Value == "2A")
        {
            da = new SqlDataAdapter("update item_codes set status='5A' where id= '" + id + "' ", con);
            da.Fill(ds, "update");
            con.Close();
            fillgrid4();
        }
    }
    
    protected void ddlitemstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlitemtype.Visible = true;
        ddlitemtype.SelectedIndex = 0;
        newitem.Text = "";
        GridView1.Visible = false;
        GridView2.Visible = false;


    }
    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlitemstatus.SelectedItem.Text == "Pending for Approval")
            {
                GridView1.Visible = false;
                GridView2.Visible = true;
                if (ddlitemstatus.SelectedItem.Text == "Pending for Approval" && ddlitemtype.SelectedItem.Text == "New Item Codes")
                {
                    da = new SqlDataAdapter("select TOP(10) id,item_code,item_name,units,basic_price,specification,subdca_code from item_codes where created_date <> 'null' and status in ('1','2','3')  ORDER BY Created_date desc", con);
                    newitem.Text="New Item Code";
                   
                }

                else if (ddlitemstatus.SelectedItem.Text == "Pending for Approval" && ddlitemtype.SelectedItem.Text == "Amended Item Codes")
                {
                    da = new SqlDataAdapter("select TOP(10) id,item_code,item_name,units,basic_price,specification,subdca_code from item_codes where  modified_date <> 'null' and  status = '1A' ORDER BY modified_date desc", con);
                    newitem.Text = "Amended Item Code";
                }
                
                da.Fill(ds, "fillpending");
                GridView2.DataSource = ds.Tables["fillpending"];
                //GridView2.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                GridView2.DataBind();
            }
            else if (ddlitemstatus.SelectedItem.Text == "Approved Item Codes")
            {
                GridView2.Visible = false;
                GridView1.Visible = true;
                if (ddlitemstatus.SelectedItem.Text == "Approved Item Codes" && ddlitemtype.SelectedItem.Text == "New Item Codes")
                {
                    da = new SqlDataAdapter("select TOP(10) id,item_code,item_name,units,basic_price,specification,subdca_code,status from item_codes where  modified_date <> 'null' and  status = '4' ORDER BY modified_date desc", con);
                    newitem.Text = "New Item Code";
                }
                else if (ddlitemstatus.SelectedItem.Text == "Approved Item Codes" && ddlitemtype.SelectedItem.Text == "Amended Item Codes")
                {
                    newitem.Text = "Amended Item Code";
                    da = new SqlDataAdapter("select TOP(10) id,item_code,item_name,units,basic_price,specification,subdca_code,status from item_codes where  modified_date <> 'null' and  status = '2A' ORDER BY modified_date desc", con);
                }
                da.Fill(ds, "fillapproved");
                GridView1.DataSource = ds.Tables["fillapproved"];
                //GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
                GridView1.DataBind();

            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("SELECT item_code,item_name,specification,basic_price,units,dca_code,subdca_code FROM item_codes where status in ('4','5','2A','5A')", con);
        da.Fill(ds, "itemcodes");

        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", " Available Itemcodes in Essel " ));
        Context.Response.Charset = "";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.DataSource = ds.Tables[0];
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        Context.Response.Write(stringWrite.ToString());
        Context.Response.End();   
    }
}