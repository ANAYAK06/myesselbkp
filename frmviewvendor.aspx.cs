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
using AjaxControlToolkit;

public partial class Admin_frmviewvendor : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    SqlDataReader dr;
    public string venid, vendor, ccVid;
   

    protected void Page_Load(object sender, EventArgs e)
    {

        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
        {            
            Response.Redirect("SessionExpire.aspx");
        }

        if (!IsPostBack)
        {
            loadgrid();
            tblvendor.Visible = false;
        }
    }

    private void loadgrid()
    {
        if (ddlvtype.SelectedIndex == 0)
            da = new SqlDataAdapter("Select id,Vendor_Id,Vendor_name,Address,vendor_phone,vendor_mobile from vendor where status='2' order by vendor_id", con);
        else
            da = new SqlDataAdapter("Select id,Vendor_Id,Vendor_name,Address,vendor_phone,vendor_mobile from vendor where vendor_type='" + ddlvtype.SelectedValue + "' and status='2' order by vendor_id", con);
        da.Fill(ds, "vendor");

        GridView1.DataSource = ds.Tables["vendor"];
        GridView1.DataBind();
    }

    protected void ddlvtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }

    int no = 0;

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        loadgrid();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                string id = GridView1.SelectedValue.ToString();
                tblvendor.Visible = true;
                ViewState["id"] = GridView1.SelectedValue.ToString();
                loadvendor();
                loadgrid();                
            }
            else
            {
                JavaScript.Alert("You are not authorized person");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Set the edit index.
        GridView1.EditIndex = e.NewEditIndex;
        string id = GridView1.DataKeys[e.NewEditIndex].Values[0].ToString();
        fillpop(id);
    }
    public void fillpop(string id)
    {
        da = new SqlDataAdapter("select s.State as state, vcg.Gst_No FROM Vendor_Client_GstNos vcg join vendor v on vcg.Ven_Supp_Client_id=v.vendor_id join States s on s.State_Id=vcg.State_Id where v.id='" + id + "'", con);
        da.Fill(ds, "gstnos");
        if (ds.Tables["gstnos"].Rows.Count > 0)
        {
            Grdviewpopup.DataSource = ds.Tables["gstnos"];
            Grdviewpopup.DataBind();
            popitems.Show();
        }
        else
        {
            Grdviewpopup.DataSource = null;
            Grdviewpopup.DataBind();
            popitems.Hide();
        }
    }
    protected void update_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            if (lblvtype.Text == "Service Provider")
            {
                cmd.CommandText = "UPDATE [vendor] SET [vendor_name]=@Name,[address]=@Address,[vendor_phone]=@Phone,[vendor_mobile]=@Mobile,[servicetax_no]=@TaxNo,[pan_no]=@PanNo,[pf_no]=@PFNO,modified_by=@User,modified_date=getdate() WHERE id = '" + ViewState["id"].ToString() + "'";
                cmd.Parameters.AddWithValue("@TaxNo", txttintax.Text);
                cmd.Parameters.AddWithValue("@PanNo", txtvatpan.Text);
                cmd.Parameters.AddWithValue("@PFNO", txtcstpf.Text);
            }
            else
            {
                cmd.CommandText = "UPDATE [vendor] SET [vendor_name]=@Name,[address]=@Address,[vendor_phone]=@Phone,[vendor_mobile]=@Mobile,[tin_no]=@TinNo,[cst_no]=@CSTNo,[vat_no]=@VatNo,modified_by=@User,modified_date=getdate() WHERE id = '" + ViewState["id"].ToString() + "'";
                cmd.Parameters.AddWithValue("@VatNo", txtvatpan.Text);
                cmd.Parameters.AddWithValue("@CSTNo", txtcstpf.Text);
                cmd.Parameters.AddWithValue("@TinNo", txttintax.Text);
            }
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Name", txtVName.Text);
            //cmd.Parameters.AddWithValue("@vendorid", Request.QueryString["Vendorid"].ToString());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Phone", txtpno.Text);
            cmd.Parameters.AddWithValue("@Mobile", txtmno.Text);
            con.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                JavaScript.AlertAndRedirect("Updated Successfully", Request.Url.ToString());
            }
            else
            {
                JavaScript.Alert("Updated Failed");
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnCancel.Text == "Cancel")
            Response.Redirect("frmViewVendor.aspx");
        else
            Response.Redirect(Request.Url.ToString());
    }

  

    public void loadvendor()
    {
        try
        {
            da = new SqlDataAdapter("select vendor_name,vendor_phone,vendor_mobile,servicetax_no,vat_no,pan_no,tin_no,address,cst_no,vendor_type,pf_no from vendor where id='" + ViewState["id"].ToString() + "'", con);
            da.Fill(ds, "vendorinfo");
            if (ds.Tables["vendorinfo"].Rows.Count > 0)
            {

                txtVName.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[0].ToString();
                txtpno.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[1].ToString();
                txtmno.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[2].ToString();
                txtAddress.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[7].ToString();
                lblvtype.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[9].ToString();
                if (ds.Tables["vendorinfo"].Rows[0]["vendor_type"].ToString() == "Service Provider")
                {
                    txtvatpan.Text = ds.Tables["vendorinfo"].Rows[0]["pan_no"].ToString();
                    txtcstpf.Text = ds.Tables["vendorinfo"].Rows[0]["pf_no"].ToString();
                    txttintax.Text = ds.Tables["vendorinfo"].Rows[0]["servicetax_no"].ToString();
                    lblvatpan.Text = "PAN No";
                    lbltintax.Text = "ServiceTax No";
                    lblcstpf.Text = "PF Reg No";
                }
                else
                {
                    //ViewState["sprovider"] = ds.Tables["vendorinfo"].Rows[0]["vendor_type"].ToString();
                    txtvatpan.Text = ds.Tables["vendorinfo"].Rows[0]["vat_no"].ToString();
                    txttintax.Text = ds.Tables["vendorinfo"].Rows[0]["tin_no"].ToString();
                    txtcstpf.Text = ds.Tables["vendorinfo"].Rows[0]["cst_no"].ToString();
                    lblvatpan.Text = "VAT No";
                    lbltintax.Text = "TIN No";
                    lblcstpf.Text = "CST No";
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk=(LinkButton)e.Row.FindControl("id");
            e.Row.Cells[6].Visible = false;
            PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;

            string behaviorID = "pce_" + (no++).ToString() + e.Row.RowIndex;
            pce.BehaviorID = behaviorID;

            Image img = (Image)e.Row.FindControl("Image1");

            string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
            string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

            img.Attributes.Add("onmouseover", OnMouseOverScript);
            img.Attributes.Add("onmouseout", OnMouseOutScript);
            da = new SqlDataAdapter("select s.State as state, vcg.Gst_No FROM Vendor_Client_GstNos vcg join vendor v on vcg.Ven_Supp_Client_id=v.vendor_id join States s on s.State_Id=vcg.State_Id where v.vendor_id='" + e.Row.Cells[2].Text + "'", con);
            da.Fill(ds, "gstnoss");
            if (ds.Tables["gstnoss"].Rows.Count > 0)
            {
                e.Row.Cells[1].Enabled = true;
                lnk.Text = "Click";
                
            }
            else
            {
                e.Row.Cells[1].Enabled = false;
                lnk.Text = "";
                
            }
        }
    }
}
