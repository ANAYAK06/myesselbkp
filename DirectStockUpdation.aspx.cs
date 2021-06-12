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


public partial class DirectStockUpdation : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 1);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            FillGrid();
        }
        hf1.Value = Session["roles"].ToString();

    }
    public void FillGrid()
    {
        try
        {
            da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')basic_price,units,Replace(il.quantity,'.00','')quantity,Replace(il.Amount,'.00','')Amount,(select quantity from [master_data] where item_code=il.item_code and cc_code='CC-33') as[Avail_Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,Replace(Amount,'.00','')Amount from StockUpdate_Items where Request_no is null and cc_code='CC-33')il on i.item_code=il.item_code order by il.id asc", con);
          
            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();

            if (ds.Tables["fill"].Rows.Count > 0)
            {
                btnvisible.Style.Add("visibility", "visible");
                description.Style.Add("visibility", "visible");
            }
            else
            {
                btnvisible.Style.Add("visibility", "hidden");
                description.Style.Add("visibility", "hidden");
            }
           
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }


    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            cmd.Connection = con;
            con.Open();
            char c = txtSearch.Text[0];
            if (char.IsNumber(c))
            {
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_code='" + txtSearch.Text + "' and status in ('4','5','5A','2A') ", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)
                {
                    cmd.CommandText = "insert into StockUpdate_Items(item_code,cc_code,ItemType)values(@itemcode,@CCCode,@ItemType)";
                    cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = txtSearch.Text;                  
                    cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                    cmd.Parameters.Add("@ItemType", SqlDbType.VarChar).Value = ddlitemtype.SelectedItem.Text;                
                           
                    int i = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    if (i == 1)
                    {

                    }

                    FillGrid();
                    (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                    txtSearch.Text = String.Empty;
                }
                else
                {

                    JavaScript.UPAlert(Page, "Invalid itemcode");
                    (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                    txtSearch.Text = String.Empty;

                }

            }
            else
            {
                int n = txtSearch.Text.IndexOf(",");
                if (n != -1)
                {
                    string result = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
                    string result1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','5','5A','2A')", con);
                    da.Fill(ds, "check");
                    if (ds.Tables["check"].Rows.Count == 1)
                    {
                        da = new SqlDataAdapter("Select item_code from item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','5','5A','2A')", con);
                        da.Fill(ds, "search");
                        if (ds.Tables["search"].Rows.Count > 0)
                        {
                            cmd.CommandText = "insert into StockUpdate_Items(item_code,cc_code,ItemType)values(@itemcode,@CCCode,@ItemType)";
                            cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = ds.Tables["search"].Rows[0].ItemArray[0].ToString();
                            cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                            cmd.Parameters.Add("@ItemType", SqlDbType.VarChar).Value = ddlitemtype.SelectedItem.Text;
                           
                            int j = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (j == 1)
                            {

                            }

                            FillGrid();
                            (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                            txtSearch.Text = String.Empty;

                        }
                    }
                    else
                    {

                        JavaScript.UPAlert(Page, "Invalid Specification");
                        (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                        txtSearch.Text = String.Empty;
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Date = txtdate.Text;

            foreach (GridViewRow record in GridView1.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                if (c1.Checked)
                {

                    string id = "";
                    id = GridView1.DataKeys[record.RowIndex]["id"].ToString();
                    if (id != null)
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;                       
                        string Qty = (record.FindControl("txtqty") as TextBox).Text;                     
                        string Amount = (record.FindControl("txtamount") as TextBox).Text;
                        string CC_code = Session["cc_code"].ToString();
                        cmd.CommandText = "Update StockUpdate_Items set quantity='" + Qty + "',Amount='" + Amount + "',basic_price='" + record.Cells[7].Text + "',CC_code='" + CC_code + "' where id='" + id + "'";
                        con.Open();
                        int i = Convert.ToInt32(cmd.ExecuteNonQuery());
                        con.Close();
                    }

                }

            }
            foreach (GridViewRow record in GridView1.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                if ((record.FindControl("ChkSelect") as CheckBox).Checked)
                {
                    ids = ids + GridView1.DataKeys[record.RowIndex]["id"].ToString() + ",";
                }
            }
            if (ids != "")
            {             
                da = new SqlDataAdapter("Stockupdate_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ids", SqlDbType.VarChar).Value = ids;           
                da.SelectCommand.Parameters.AddWithValue("@Date", SqlDbType.VarChar).Value = Date;
                da.SelectCommand.Parameters.AddWithValue("@User", SqlDbType.VarChar).Value = Session["user"].ToString();
                da.SelectCommand.Parameters.AddWithValue("@description", SqlDbType.VarChar).Value = txtdesc.Text;
                da.SelectCommand.Parameters.AddWithValue("@ItemType", SqlDbType.VarChar).Value = ddlitemtype.SelectedItem.Text;
                da.SelectCommand.Parameters.AddWithValue("@roles", SqlDbType.VarChar).Value = Session["roles"].ToString();

            }
            da.Fill(ds, "stockupdate");
            if (ds.Tables["stockupdate"].Rows[0].ItemArray[0].ToString() == "Request Generated Sucessfully")
            {
                JavaScript.UPAlertRedirect(Page, "Request Generated Sucessfully. The Request No is: " + ds.Tables["stockupdate"].Rows[0].ItemArray[1].ToString(), "DirectStockUpdation.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, ds.Tables["stockupdate"].Rows[0].ItemArray[0].ToString());
            }
            con.Close();
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow record in GridView1.Rows)
        {
            CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

            if (c1.Checked)
            {
                string id = GridView1.DataKeys[record.RowIndex]["id"].ToString();
                cmd.Connection = con;
                cmd.CommandText = "delete from StockUpdate_Items where id='" + id + "'";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        FillGrid();
    }
    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        cascadingDCA cs = new cascadingDCA();
        cs.values(ddlitemtype.SelectedValue);
    }
}