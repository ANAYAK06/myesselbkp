// CODING HISTORY//
// Date             Tracking No              Name            Comments
// 08-Jan-3013      INC-JAN-014             KISHORE         This requirement is for  Delete in verifyvendor

using System;
using System.Collections.Generic;
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


public partial class VerifyVendor : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet Objds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            fillgrid();
            tblvendor.Visible = false;
        }
    }
    public void fillgrid()
    {
        try
        {
            ds.Clear();
            da = new SqlDataAdapter("select id,vendor_name,vendor_phone,servicetax_no,vat_no,pan_no,tin_no,address,cst_no from vendor where status='1'", con);
            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
   
    
   
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
            try
            {
                string Statesu = "";
                string GSTNosu = "";
                foreach (GridViewRow record in gvotheru.Rows)
                {
                    if (rbtngstu.SelectedValue == "Yes")
                    {
                        if (gvotheru != null)
                        {
                            if ((record.FindControl("chkgstu") as CheckBox).Checked)
                            {
                                Statesu = Statesu + (record.FindControl("ddlstatesu") as DropDownList).SelectedValue + ",";
                                GSTNosu = GSTNosu + (record.FindControl("txtregnou") as TextBox).Text + ",";
                            }
                            else
                            {
                                JavaScript.UPAlert(Page, "Please Verify GST Nos");
                                break;
                            }
                        }
                    }
                }              
                cmd = new SqlCommand("sp_Insert_Vendor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@States", Statesu);
                cmd.Parameters.AddWithValue("@GSTNos", GSTNosu);
                cmd.Parameters.AddWithValue("@Name", txtVName.Text);
                cmd.Parameters.AddWithValue("@Type", lblvtype.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Phone", txtpno.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtmno.Text);
                if (lblvtype.Text == "Service Provider")
                {
                    cmd.Parameters.AddWithValue("@TaxNo", txttintax.Text);
                    cmd.Parameters.AddWithValue("@PanNo", txtvatpan.Text);
                    cmd.Parameters.AddWithValue("@PFNO", txtcstpf.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@VatNo", txtvatpan.Text);
                    cmd.Parameters.AddWithValue("@CSTNo", txtcstpf.Text);
                    cmd.Parameters.AddWithValue("@TinNo", txttintax.Text);
                }
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                // 08-JAN-2013 - KISHORE - INC-JAN-014 - Below parameter "@status" is required in SP for check weather it is ADD or UPDATE  - START
                cmd.Parameters.AddWithValue("@status", "UPDATE");
                cmd.Parameters.AddWithValue("@id", ViewState["id"].ToString());
                // 08-JAN-2013 - KISHORE - INC-JAN-014 - Below parameter "@status" is required in SP for check weather it is ADD or UPDATE  - END
                // 20-Apr-2016 - KISHORE - ENH-DEC-001-2015 - START
                cmd.Parameters.AddWithValue("@Bankname", txtbankname.Text);
                cmd.Parameters.AddWithValue("@Accountno", txtacno.Text);
                cmd.Parameters.AddWithValue("@ifsccode", txtifsc.Text);
                // 20-Apr-2016 - KISHORE - ENH-DEC-001-2015 - END

                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                JavaScript.UPAlertRedirect(Page,msg, "VerifyVendor.aspx");
            }
            catch (Exception ex)
            {
                Utilities.CatchException(ex);
            }
            finally
            {
                con.Close();
            }
            fillgrid();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        tblvendor.Visible = false;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string id = GridView1.SelectedValue.ToString();
            tblvendor.Visible = true;
            filldata(id);
            ViewState["id"] = GridView1.SelectedValue.ToString();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void filldata(string id)
    {
        try
        {
            da = new SqlDataAdapter("select vendor_name,vendor_phone,vendor_mobile,servicetax_no,vat_no,pan_no,tin_no,address,cst_no,vendor_type,pf_no,bank_name,Account_no,ifsc_code,vendor_id,Gst_Applicable from vendor where id='" + id + "'", con);
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
                txtbankname.Text = ds.Tables["vendorinfo"].Rows[0]["bank_name"].ToString();
                txtacno.Text = ds.Tables["vendorinfo"].Rows[0]["Account_no"].ToString();
                txtifsc.Text = ds.Tables["vendorinfo"].Rows[0]["ifsc_code"].ToString();
                ViewState["vid"] = ds.Tables["vendorinfo"].Rows[0]["vendor_id"].ToString();
                if (ds.Tables["vendorinfo"].Rows[0]["Gst_Applicable"].ToString() == "Yes")
                {
                    da = new SqlDataAdapter("select st.State_id,st.state,vcg.gst_no from Vendor_Client_GstNos vcg join States st on vcg.state_id=st.State_Id where Ven_Supp_Client_id= '" + ds.Tables["vendorinfo"].Rows[0]["vendor_id"].ToString() + "'", con);
                    da.Fill(Objds, "InvoiceInfo3");
                    if (Objds.Tables["InvoiceInfo3"].Rows.Count > 0)
                    {
                        DataTable dtother = new DataTable();
                        dtother.Columns.Add("S.No", typeof(int));
                        dtother.Columns.Add("chkgstu", typeof(string));
                        dtother.Columns.Add("ddlstatesu", typeof(string));
                        dtother.Columns.Add("txtregnou", typeof(string));
                        dtother.Columns.Add("ddlstatesidu", typeof(string));
                        for (int i = 0; i < Objds.Tables["InvoiceInfo3"].Rows.Count; i++)
                        {
                            DataRow dr = dtother.NewRow();
                            dr["chkgstu"] = string.Empty;
                            dr["ddlstatesu"] = Objds.Tables["InvoiceInfo3"].Rows[i]["state"].ToString();
                            dr["txtregnou"] = Objds.Tables["InvoiceInfo3"].Rows[i]["gst_no"].ToString();
                            dr["ddlstatesidu"] = Objds.Tables["InvoiceInfo3"].Rows[i]["State_id"].ToString();
                            dtother.Rows.Add(dr);
                        }
                        rbtngstu.SelectedValue = "Yes";
                        ViewState["Curtblotheru"] = dtother;
                        gvotheru.DataSource = dtother;
                        gvotheru.DataBind();
                        trothergridu.Style["display"] = "block";
                    }
                    else
                    {
                        rbtngstu.SelectedValue = "No";
                        gvotheru.DataSource = null;
                        gvotheru.DataBind();
                        trothergridu.Style["display"] = "none";
                    }
                }
                else
                {
                    rbtngstu.SelectedValue = "No";
                    gvotheru.DataSource = null;
                    gvotheru.DataBind();
                    trothergridu.Style["display"] = "none";
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    // 08-JAN-2013 - KISHORE - INC-JAN-014 - Below added btndelete_click for delete  - START

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_Insert_Vendor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VID", ViewState["vid"].ToString());
            cmd.Parameters.AddWithValue("@Type", lblvtype.Text);
            cmd.Parameters.AddWithValue("@status", "Delete");
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            JavaScript.UPAlertRedirect(Page, msg, "VerifyVendor.aspx");
            fillgrid();
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    // 08-JAN-2013 - KISHORE - INC-JAN-014 - Above added btndelete_click for delete  - END
    #region GST START for update
    protected void statedetailsu()
    {
        try
        {
            foreach (GridViewRow other in gvotheru.Rows)
            {
                DropDownList ddlstate = (DropDownList)other.FindControl("ddlstatesu");
                da = new SqlDataAdapter("select State_Id,state from [states]", con);
                ds = new DataSet();
                da.Fill(ds, "gstu");
                if (ds.Tables["gstu"].Rows.Count > 0)
                {
                    ddlstate.Enabled = true;
                    ddlstate.Items.Clear();
                    ddlstate.DataSource = ds.Tables["gstu"];
                    ddlstate.DataTextField = "state";
                    ddlstate.DataValueField = "State_Id";
                    ddlstate.DataBind();
                    ddlstate.Items.Insert(0, new ListItem("Select"));
                }
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void btnaddgstu_Click(object sender, EventArgs e)
    {

        AddNewotherRowu();
        statedetailsu();
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
    }


    protected void rbtngstu_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbtngstu.SelectedIndex == 0)
        {
            trothergridu.Style["display"] = "block";
            BindotherGridviewu();
            otherdcalistu();
        }
        else
        {
            gvotheru.DataSource = null;
            gvotheru.DataBind();
            trothergridu.Style["display"] = "none";
        }
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
    }
    public void loaddcau()
    {
        foreach (GridViewRow record in gvotheru.Rows)
        {
            DropDownList states = (DropDownList)record.FindControl("ddlstatesu");
            if (states.SelectedValue == "")
            {
                states.Items.Insert(0, new ListItem("Select", "Select"));
                states.DataBind();
            }
        }
    }
    protected void BindotherGridviewu()
    {
        DataTable dtt = new DataTable();
        dtt.Columns.Add("S.No", typeof(int));
        dtt.Columns.Add("chkgstu", typeof(string));
        dtt.Columns.Add("ddlstatesu", typeof(string));
        dtt.Columns.Add("txtregnou", typeof(string));
        dtt.Columns.Add("ddlstatesidu", typeof(string));
        DataRow dr = dtt.NewRow();
        dr["chkgstu"] = string.Empty;
        dr["ddlstatesu"] = "Select";
        dr["txtregnou"] = string.Empty;
        dr["ddlstatesidu"] = "Select";
        dtt.Rows.Add(dr);
        ViewState["Curtblotheru"] = dtt;
        gvotheru.DataSource = dtt;
        gvotheru.DataBind();
    }
    private void AddNewotherRowu()
    {
        int rowIndex = 0;
        if (ViewState["Curtblotheru"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtblotheru"];
            DataRow drCurrentRow = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    DropDownList ddlstates = (DropDownList)gvotheru.Rows[rowIndex].Cells[1].FindControl("ddlstatesu");
                    TextBox txtregno = (TextBox)gvotheru.Rows[rowIndex].Cells[2].FindControl("txtregnou");
                    drCurrentRow = dt.NewRow();
                    dt.Rows[i - 1]["ddlstatesu"] = ddlstates.SelectedItem.Text;
                    dt.Rows[i - 1]["txtregnou"] = txtregno.Text;
                    dt.Rows[i - 1]["ddlstatesidu"] = ddlstates.SelectedValue;
                    rowIndex++;
                }
                dt.Rows.Add(drCurrentRow);
                ViewState["Curtblotheru"] = dt;
                gvotheru.DataSource = dt;
                gvotheru.DataBind();
            }
        }
        SetOldotherDatau();
    }
    private void SetOldotherDatau()
    {
        int rowIndex = 0;
        if (ViewState["Curtblotheru"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtblotheru"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvotheru.Rows[rowIndex].Cells[0].FindControl("chkgstu");
                    DropDownList ddlstate = (DropDownList)gvotheru.Rows[rowIndex].Cells[1].FindControl("ddlstatesu");
                    TextBox txtreg = (TextBox)gvotheru.Rows[rowIndex].Cells[2].FindControl("txtregnou");
                    if (i < dt.Rows.Count - 1)
                    {
                        ddlstate.SelectedItem.Text = dt.Rows[i]["ddlstatesu"].ToString();
                        txtreg.Text = dt.Rows[i]["txtregnou"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void gvotheru_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblotheru"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtblotheru"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtblotheru"] = dt;
                    gvotheru.DataSource = dt;
                    gvotheru.DataBind();
                    for (int i = 0; i < gvotheru.Rows.Count - 1; i++)
                    {
                        gvotheru.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetOldotherDatau();
                    otherdcalistu();
                }
            }
            //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    string dcau = "";
    protected void otherdcalistu()
    {
        try
        {
            foreach (GridViewRow other in gvotheru.Rows)
            {
                DropDownList ddlstates = (DropDownList)other.FindControl("ddlstatesu");
                da = new SqlDataAdapter("select State_Id,state from [states]", con);
                da.Fill(ds, "GSTStatesu");
                if (ds.Tables["GSTStatesu"].Rows.Count > 0)
                {
                    ddlstates.DataValueField = "State_Id";
                    ddlstates.DataTextField = "state";
                    ddlstates.DataSource = ds.Tables["GSTStatesu"];
                    ddlstates.DataBind();
                    ddlstates.Items.Insert(0, "Select");
                }
                else
                {

                    ddlstates.Items.Insert(0, "Select");
                }
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void btnaddothersu_Click(object sender, EventArgs e)
    {

        AddNewotherRowu();
        otherdcalistu();
    }
    public int otu = 0;
    protected void gvotheru_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (ViewState["Curtblotheru"] != null)
                {
                    DataTable Objdt = ViewState["Curtblotheru"] as DataTable;
                    if (Objdt.Rows[otu]["ddlstatesu"].ToString() != "Select" && Objdt.Rows[otu]["ddlstatesu"].ToString() != "")
                    {
                        DropDownList ddlstates = (DropDownList)e.Row.FindControl("ddlstatesu");
                        TextBox txtregno = (TextBox)e.Row.FindControl("txtregnou");
                        da = new SqlDataAdapter("select State_Id,state from [states]", con);
                        da.Fill(ds, "GSTStatesu");
                        if (ds.Tables["GSTStatesu"].Rows.Count > 0)
                        {
                            ddlstates.DataValueField = "State_Id";
                            ddlstates.DataTextField = "state";
                            ddlstates.DataSource = ds.Tables["GSTStatesu"];
                            ddlstates.DataBind();
                            ddlstates.Items.Insert(0, "Select");
                            ddlstates.SelectedValue = Objdt.Rows[otu]["ddlstatesidu"].ToString();
                        }
                        txtregno.Text = Objdt.Rows[otu]["txtregnou"].ToString();
                    }
                }
                otu = otu + 1;
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    #endregion GST END for update
}
