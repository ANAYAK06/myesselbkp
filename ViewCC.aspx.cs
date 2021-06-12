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
using System.Web.Services;
using System.Data.SqlClient;

public partial class ViewCC : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommandBuilder cmb = new SqlCommandBuilder();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlDataReader dr;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fill();
        }
    }
    public void fill()
    {
        da = new SqlDataAdapter("select cc_code,cc_name,replace(voucher_limit,'.0000','.00') as voucher_limit ,replace(day_limit,'.0000','.00') as day_limit  from Cost_Center where status in('New','old')", con);
        //cmb = new SqlCommandBuilder(da);
       
        da.Fill(ds, "cc");
        GridView1.DataSource = ds.Tables["cc"];
        GridView1.DataBind();


    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fill();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow rw = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label Lblcccode = (Label)GridView1.Rows[e.RowIndex].FindControl("Lblcccode");
        Label Lblccname = (Label)GridView1.Rows[e.RowIndex].FindControl("Lblccname");
        TextBox txtvoucher = (TextBox)rw.FindControl("txtvoucher");
        TextBox txtday = (TextBox)rw.FindControl("txtday");
       
        SqlCommand cmd = new SqlCommand("update Cost_Center set voucher_limit=@voucher,day_limit=@day where cc_code=@costcentercode", con);
        cmd.Parameters.Add("@costcentercode", SqlDbType.VarChar).Value = Lblcccode.Text;
        cmd.Parameters.Add("@voucher", SqlDbType.Money).Value = txtvoucher.Text;
        cmd.Parameters.Add("@day", SqlDbType.Money).Value = txtday.Text;
        con.Open();
         int i=cmd.ExecuteNonQuery();
         if (i == 1)
         {
             JavaScript.UPAlert(Page, "Updated Successfully");
         }
         else
         {
             JavaScript.UPAlert(Page, "Updation Failed");
         }
        GridView1.EditIndex = -1;
        
        fill();
 
    }


   
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fill();


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.EditIndex = -1;
        fill();

    }
}
