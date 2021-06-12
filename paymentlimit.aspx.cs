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


public partial class paymentlimit : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Tools");
        //lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        
        
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    cmd.CommandText = "Update [voucher limits] set limit='" + txtvoucher.Text + "' where id='1'";
    //    cmd.Connection = con;
    //    con.Open();
    //    int i=cmd.ExecuteNonQuery();
    //    if (i == 1)
    //        JavaScript.AlertAndRedirect("Updated","paymentlimit.aspx");
    //    else
    //        JavaScript.Alert("Updation Failed");
    //    con.Close();
    //}

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        
        fillgrid();
        //cmd.CommandText = "Update [voucher limits] set limit='' where id='1'";
        //cmd.Connection = con;
        //con.Open();
        //int i = cmd.ExecuteNonQuery();
        //if (i == 1)
        //    JavaScript.AlertAndRedirect("Updated", "paymentlimit.aspx");
        //else
        //    JavaScript.Alert("Updation Failed");
        //con.Close();
           
       
    }

    private void fillgrid()
    {
        da = new SqlDataAdapter("select id,'Transaction Limit'[LimitName],replace(limit,'.0000','.00')limit from [voucher limits] where id='1' union all select id,'Invoice Limit'[LimitName],replace(limit,'.0000','.00')limit from [voucher limits] where id='2'  union all select id,'Indent Limit'[LimitName],replace(limit,'.0000','.00')limit from [voucher limits] where id='3'  union all select id,'PO Limit'[LimitName],replace(limit,'.0000','.00')limit from [voucher limits] where id='4'  union all select id,'General Invoice Limit'[LimitName],replace(limit,'.0000','.00')limit from [voucher limits] where id='5'", con);
        da.Fill(ds, "limit");
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridViewRow gvr = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label lb = (Label)gvr.FindControl("id");
        TextBox tbox1 = (TextBox)gvr.FindControl("limit");
        tbox1.Focus();
        
        GridView1.EditIndex = -1;
        con.Open();
        
        cmd = new SqlCommand("update [voucher limits] set limit='" + tbox1.Text + "' where id='"+lb.Text+"'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label lb = (Label)gvr.FindControl("lblid");
        con.Open();
        cmd = new SqlCommand("delete from [voucher limits] where id='" + lb.Text + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        fillgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
}
