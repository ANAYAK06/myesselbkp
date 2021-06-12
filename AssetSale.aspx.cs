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

public partial class AssetSale : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
            {
                fillassetitems();
                btnvisible.Visible = false;
                tbldesc.Visible = false;
            }
        }
    }

    public void fillassetitems()
    {
        da = new SqlDataAdapter("select md.id,(md.item_code+' , '+ic.item_name+' , '+Specification)as name from Master_data md join Item_codes ic on SUBSTRING(md.item_code,1,8)=ic.Item_code  where CC_code='CC-33' and md.Item_code like '1%' and md.status='Active' order by md.Item_code", con);
        ds = new DataSet();
        da.Fill(ds, "assetitems");
        if (ds.Tables["assetitems"].Rows.Count > 0)
        {
            ddlasset.DataSource = ds.Tables["assetitems"];
            ddlasset.DataTextField = "name";
            ddlasset.DataValueField = "id";
            ddlasset.DataBind();
            ddlasset.Items.Insert(0, new ListItem("Select"));
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("select md.id as id,md.Item_code,ic.item_name,ic.Specification,ic.Dca_Code,ic.Subdca_Code,cast(md.Basicprice as decimal(20,2))as Basicprice,md.Status from Master_data md join Item_codes ic on SUBSTRING(md.item_code,1,8)=ic.Item_code  where md.id='" + ddlasset.SelectedValue + "'", con);
            da.Fill(ds, "grid");
            if(ds.Tables["grid"].Rows.Count>0)
            {
                GridView1.DataSource = ds.Tables["grid"];
                GridView1.DataBind();
                btnvisible.Visible = true;
                tbldesc.Visible = true;
                hfbasic.Value = ds.Tables["grid"].Rows[0].ItemArray[6].ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnvisible.Visible = false;
                tbldesc.Visible = false;
                hfbasic.Value = Convert.ToInt32(0).ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string id = "";
            string itemcode = "";
            foreach (GridViewRow record in GridView1.Rows)
            {
                id = GridView1.DataKeys[record.RowIndex]["id"].ToString();
                itemcode = record.Cells[1].Text;
            }
            cmd = new SqlCommand("AssetSale_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@ItemCode", itemcode);
            cmd.Parameters.AddWithValue("@Date", txtdates.Text);
            cmd.Parameters.AddWithValue("@ValuationDate", txtassvaluedate.Text);
            cmd.Parameters.AddWithValue("@ActuallAmt", txtassetamount.Text);
            cmd.Parameters.AddWithValue("@SellingAmt", txtasstsellingamt.Text);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtname.Text);
            cmd.Parameters.AddWithValue("@Address", txtaddress.Text);
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Sucessfull")
                JavaScript.UPAlertRedirect(Page, msg, "AssetSale.aspx");
            else
                JavaScript.UPAlert(Page, msg);
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
}