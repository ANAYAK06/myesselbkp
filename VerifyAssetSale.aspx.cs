using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class VerifyAssetSale : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
            tblverifyasset.Visible = false;
        }
    }

    public void fillgrid()
    {
        try
        {
            tblverifyasset.Visible = false;
            if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("select id,Request_No as Req_no,REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')as Date,Item_code,cast(selling_amt as decimal(20,2))as selling_amt from asset_sale where status='1'", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("select id,Request_No as Req_no,REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')as Date,Item_code,cast(selling_amt as decimal(20,2))as selling_amt from asset_sale where status='2'", con);
            else if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select id,Request_No as Req_no,REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')as Date,Item_code,cast(selling_amt as decimal(20,2))as selling_amt from asset_sale where status='3'", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select id,Request_No as Req_no,REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')as Date,Item_code,cast(selling_amt as decimal(20,2))as selling_amt from asset_sale where status='4'", con);
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                da = new SqlDataAdapter("select id,Request_No as Req_no,REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')as Date,Item_code,cast(selling_amt as decimal(20,2))as selling_amt from asset_sale where status='5'", con);

            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                gvassetsales.DataSource = ds.Tables["fill"];
                gvassetsales.DataBind();
            }
            else
            {
                gvassetsales.EmptyDataText = "No Data avaliable";
                gvassetsales.DataSource = null;
                gvassetsales.DataBind();                
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void gvassetsales_SelectedIndexChanged(object sender, EventArgs e)
    {
        //decimal total;
        try
        {
            ViewState["Assetidid"] = gvassetsales.SelectedValue.ToString();
            ViewState["ItemCode"] = gvassetsales.SelectedRow.Cells[4].Text;
            da = new SqlDataAdapter("select md.id as id,md.Item_code,ic.item_name,ic.Specification,ic.Dca_Code,ic.Subdca_Code,cast(md.Basicprice as decimal(20,2))as Basicprice,md.Status from Master_data md join Item_codes ic on SUBSTRING(md.item_code,1,8)=ic.Item_code  where md.item_code='" + gvassetsales.SelectedRow.Cells[4].Text + "'", con);
            da.Fill(ds, "grid");
            if (ds.Tables["grid"].Rows.Count > 0)
            {
                tblverifyasset.Visible = true;
                GridView1.DataSource = ds.Tables["grid"];
                GridView1.DataBind();
                da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')as Date,REPLACE(CONVERT(VARCHAR(11),BookValue_Date, 106), ' ', '-')as ValueDate ,cast(Actuall_amt as decimal(20,2))as ActuallAmt,cast(Selling_Amt as decimal(20,2))as SellingAmt,Buyer_Name,Buyer_Address from asset_sale where ID='" + gvassetsales.SelectedValue.ToString() + "'", con);
                da.Fill(ds, "Details");
                txtdates.Text = ds.Tables["Details"].Rows[0]["Date"].ToString();
                txtassvaluedate.Text = ds.Tables["Details"].Rows[0]["ValueDate"].ToString();
                txtassetamount.Text = ds.Tables["Details"].Rows[0]["ActuallAmt"].ToString();
                txtasstsellingamt.Text = ds.Tables["Details"].Rows[0]["SellingAmt"].ToString();
                txtname.Text = ds.Tables["Details"].Rows[0]["Buyer_Name"].ToString();
                txtaddress.Text = ds.Tables["Details"].Rows[0]["Buyer_Address"].ToString();
                if ((float.Parse(ds.Tables["Details"].Rows[0]["ActuallAmt"].ToString(), System.Globalization.CultureInfo.InvariantCulture)) > (float.Parse(ds.Tables["Details"].Rows[0]["SellingAmt"].ToString(), System.Globalization.CultureInfo.InvariantCulture)))
                {
                    lbllosssaleamount.Text = ((float.Parse(ds.Tables["Details"].Rows[0]["ActuallAmt"].ToString(), System.Globalization.CultureInfo.InvariantCulture)) - (float.Parse(ds.Tables["Details"].Rows[0]["SellingAmt"].ToString(), System.Globalization.CultureInfo.InvariantCulture))).ToString();
                    lblprofitsaleamount.Text = "";
                }
                else
                {
                    lblprofitsaleamount.Text = ((float.Parse(ds.Tables["Details"].Rows[0]["SellingAmt"].ToString(), System.Globalization.CultureInfo.InvariantCulture)) - (float.Parse(ds.Tables["Details"].Rows[0]["ActuallAmt"].ToString(), System.Globalization.CultureInfo.InvariantCulture))).ToString();
                    lbllosssaleamount.Text = "";
                }
                da = new SqlDataAdapter("Select Approved_Users from asset_sale where id='" + ViewState["Assetidid"].ToString() + "'", con);
                da.Fill(ds, "roles");
                if (ds.Tables["roles"].Rows.Count > 0 && ds.Tables["roles"].Rows[0].ItemArray[0].ToString() != "")
                {
                    //Trgvusers.Visible = true;
                    string rolesamend = ds.Tables["roles"].Rows[0][0].ToString().Replace("'", " ");
                    da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + rolesamend + "',',')", con);
                    da.Fill(ds, "splitrole");
                    DataTable dtra = ds.Tables["splitrole"];
                    ViewState["Curtblroles"] = dtra;
                    gvusers.DataSource = dtra;
                    gvusers.DataBind();
                }
                else
                {
                    gvusers.DataSource = null;
                    gvusers.DataBind();
                }
            }
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {

        btnapprove.Visible = false;
        cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.CommandText = "sp_VerfiyAssetSale";
            cmd.Parameters.AddWithValue("@Id", ViewState["Assetidid"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            //string msg = "";
            if (msg == "Verified Successfully")
            {
                JavaScript.UPAlertRedirect(Page, "Verified Successfully", "VerifyAssetSale.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, "Failed");
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
    public int j = 0;
    public int k = 1;
    protected void gvusers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["Curtblroles"] != null)
                {
                    DataTable Objdt = ViewState["Curtblroles"] as DataTable;
                    if (Objdt.Rows[j][0].ToString() != "")
                    {
                        da = new SqlDataAdapter("select (first_name+'  '+middle_name+'  '+last_name)as name  from  Employee_Data  where User_Name='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da.Fill(ds, "userroles");

                           
                        if (k == 1)
                        {
                            e.Row.Cells[0].Text = "Created By";
                            e.Row.Cells[1].Text = "Central Store Keeper";
                        }
                        else if (k == 2)
                        {
                            e.Row.Cells[0].Text = "Verified By";
                            e.Row.Cells[1].Text = "Purchase  Manager";
                        }
                        else if (k == 3 )
                        {
                            e.Row.Cells[0].Text = "Verified By";
                            e.Row.Cells[1].Text = "Chief Material Controller";
                        }
                        else if (k == 4 )
                        {
                            e.Row.Cells[0].Text = "Verified By";
                            e.Row.Cells[1].Text = "HoAdmin";
                        }
                        else if (k == 5 )
                        {
                            e.Row.Cells[0].Text = "Verified By";
                            e.Row.Cells[1].Text = "SuperAdmin";
                        }                     
                        // e.Row.Cells[1].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                        e.Row.Cells[2].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                        //e.Row.Cells[3].Text = ds.Tables["userroles"].Rows[j].ItemArray[2].ToString();

                    }
                }
                j = j + 1;
                k = k + 1;
            }

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_RejectAssetSale", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32( ViewState["Assetidid"].ToString());
            cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = ViewState["ItemCode"].ToString();
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, "Rejected successfully", "verifyAssetSale.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, "Failed");
            }

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }

    }
}