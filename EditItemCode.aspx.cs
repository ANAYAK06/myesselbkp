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
using System.Data;
using System.Collections.Generic;


public partial class EditItemCode : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();   
    protected void Page_Load(object sender, EventArgs e)
    {
         Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 9);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {


        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        try
        {

            da = new SqlDataAdapter("Select Category_code,icc.category_name,majorgroup_code,icc.majorgroup_name,dca_code,subdca_code,specification_code,specification,item_name,units,replace(basic_price,'.0000','.00') as basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code where item_code='" + txtSearch.Text + "' and status in ('4','5','5A')", con);
            da.Fill(ds, "iteminfo");
            if (ds.Tables["iteminfo"].Rows.Count > 0)
            {
                txtcategorycode.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[0].ToString();
                txtcategoryname.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[1].ToString();
                txtgroupcode.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[2].ToString();
                txtgroupname.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[3].ToString();
                CascadingDropDown1.SelectedValue = ds.Tables["iteminfo"].Rows[0].ItemArray[4].ToString();
                CascadingDropDown3.SelectedValue = ds.Tables["iteminfo"].Rows[0].ItemArray[5].ToString();
                txtspecificationCode.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[6].ToString();
                txtspecificationp.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[7].ToString();
                txtname.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[8].ToString();
                txtunitsp.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[9].ToString();
                txtbasicpricep.Text = ds.Tables["iteminfo"].Rows[0].ItemArray[10].ToString();
                //txtsubgroupcodep.Text = ddlitem.SelectedItem.Text.Substring(3, 3);
                txtsubgroupcodep.Text = txtSearch.Text.Substring(3, 3);
                string basic = ds.Tables["iteminfo"].Rows[0].ItemArray[10].ToString();
                hfbasic.Value = basic;
            }
            else
            {
                JavaScript.UPAlert(Page, "Invalid Code");
            }
            
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("ExistingItemCodeEdit_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemCode", txtSearch.Text);
            cmd.Parameters.AddWithValue("@ItemName", txtname.Text);
            cmd.Parameters.AddWithValue("@SpecificationName", txtspecificationp.Text);
            cmd.Parameters.AddWithValue("@DCACode", ddldetailhead.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@SubDca", ddlsubdetail.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@units", txtunitsp.Text);
            cmd.Parameters.AddWithValue("@basicprice", txtbasicpricep.Text);
            cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Sucessfull")
                JavaScript.UPAlertRedirect(Page, msg, "EditItemCode.aspx");
            else
            {                
                JavaScript.UPAlert(Page, msg);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> Searchitemcodes(string prefixText, int count)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT item_code from item_codes where item_code like '" + prefixText + "%' and status in ('4','5','5A')";
            cmd.Parameters.AddWithValue("@SearchText", prefixText);
            cmd.Connection = conn;
            conn.Open();
            List<string> itemcodes = new List<string>();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    itemcodes.Add(sdr["item_code"].ToString());
                }
            }
            conn.Close();
            return itemcodes;
        }

    }
}