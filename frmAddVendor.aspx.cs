// CODING HISTORY//
// Date             Tracking No              Name            Comments
// 08-Jan-3013      INC-JAN-014             KISHORE         This requirement is for  update in verifyvendor

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
using System.Collections.Generic;


public partial class Admin_frmAddVendor : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    DataSet Objds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 5);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (!IsPostBack)
        {
            //if (Request.QueryString["Vendorid"] != null)
            //loadvendor(Request.QueryString["Vendorid"].ToString());
            tbladdvendor.Visible = true;
            tblverifyvendor.Visible = false;
            //tblstateu.Visible = false;
        }
    }

    private void loadvendor(string p)
    {
        da = new SqlDataAdapter("select * from vendor  where  vendor_id=@vendorid", con);
        da.SelectCommand.Parameters.AddWithValue("@vendorid", p);
        da.Fill(ds, "vendorinfo");

        if (ds.Tables["vendorinfo"].Rows.Count > 0)
        {
            lblvendor.Text = "Update " + p + " Vendor Details";
            btnAddVendor.Visible = false;
            btnUpdate.Visible = true;
            btnCancel.Text = "Cancel";
            ddlVType.Enabled = false;
            txtvatpan.ToolTip = lblvatpan.Text = "PAN No";
            txttintax.ToolTip = lbltintax.Text = "ServiceTax No";
            txtcstpf.ToolTip = lblcstpf.Text = "PF Reg No";

            ddlVType.Visible = false;
            lblvtype.Visible = true;
            lblvtype.Text = ds.Tables["vendorinfo"].Rows[0]["vendor_type"].ToString();
            txtVName.Text = ds.Tables["vendorinfo"].Rows[0]["vendor_name"].ToString();
            txtAddress.Text = ds.Tables["vendorinfo"].Rows[0]["address"].ToString();
            txtpno.Text = ds.Tables["vendorinfo"].Rows[0]["vendor_phone"].ToString();
            txtmno.Text = ds.Tables["vendorinfo"].Rows[0]["vendor_mobile"].ToString();
            if (ds.Tables["vendorinfo"].Rows[0]["vendor_type"].ToString() == "Service Provider")
            {
                txtvatpan.Text = ds.Tables["vendorinfo"].Rows[0]["pan_no"].ToString();
                txtcstpf.Text = ds.Tables["vendorinfo"].Rows[0]["pf_no"].ToString();
                txttintax.Text = ds.Tables["vendorinfo"].Rows[0]["servicetax_no"].ToString();
            }
            else
            {
                txtvatpan.Text = ds.Tables["vendorinfo"].Rows[0]["vat_no"].ToString();
                txttintax.Text = ds.Tables["vendorinfo"].Rows[0]["tin_no"].ToString();
                txtcstpf.Text = ds.Tables["vendorinfo"].Rows[0]["cst_no"].ToString();
            }
        }
    }

    protected void btnAddVendor_Click1(object sender, EventArgs e)
    {
        try
        {
            string States = "";
            string GSTNos = "";           
            foreach (GridViewRow record in gvother.Rows)
            {
                if (rbtngst.SelectedValue == "Yes")
                {
                    if (gvother != null)
                    {
                        if ((record.FindControl("chkgst") as CheckBox).Checked)
                        {
                            States = States + (record.FindControl("ddlstates") as DropDownList).SelectedValue + ",";
                            GSTNos = GSTNos + (record.FindControl("txtregno") as TextBox).Text + ",";
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
            cmd.Parameters.AddWithValue("@States", States);
            cmd.Parameters.AddWithValue("@GSTNos", GSTNos);
            cmd.Parameters.AddWithValue("@Name", txtVName.Text);
            cmd.Parameters.AddWithValue("@Type", ddlVType.SelectedValue);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Phone", txtpno.Text);
            cmd.Parameters.AddWithValue("@Mobile", txtmno.Text);
            if (ddlVType.SelectedValue == "Service Provider")
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
            cmd.Parameters.AddWithValue("@status", "INSERT");
            // 08-JAN-2013 - KISHORE - INC-JAN-014 - Below parameter "@status" is required in SP for check weather it is ADD or UPDATE  - END
            // 20-Apr-2016 - KISHORE - ENH-DEC-001-2015 - START
            cmd.Parameters.AddWithValue("@Bankname", txtbankname.Text);
            cmd.Parameters.AddWithValue("@Accountno", txtacno.Text);
            cmd.Parameters.AddWithValue("@ifsccode", txtifsc.Text);
            // 20-Apr-2016 - KISHORE - ENH-DEC-001-2015 - END
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "SC";
            JavaScript.UPAlertRedirect(Page,msg,"frmAddVendor.aspx");
           
          
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

        Response.Redirect("frmAddVendor.aspx");

    }

    protected void update_Click(object sender, EventArgs e)
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
            cmd.Parameters.AddWithValue("@Venid", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@States", Statesu);
            cmd.Parameters.AddWithValue("@GSTNos", GSTNosu);
            cmd.Parameters.AddWithValue("@Name", txtVNameupdate.Text);
            cmd.Parameters.AddWithValue("@Type", lblvtypeupdate.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddressupdate.Text);
            cmd.Parameters.AddWithValue("@Phone", txtpnoupdate.Text);
            cmd.Parameters.AddWithValue("@Mobile", txtmnoupdate.Text);
            if (lblvtypeupdate.Text == "Service Provider")
            {
                cmd.Parameters.AddWithValue("@TaxNo", txttintaxupdate.Text);
                cmd.Parameters.AddWithValue("@PanNo", txtvatpanupdate.Text);
                cmd.Parameters.AddWithValue("@PFNO", txtcstpfupdate.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@VatNo", txtvatpanupdate.Text);
                cmd.Parameters.AddWithValue("@CSTNo", txtcstpfupdate.Text);
                cmd.Parameters.AddWithValue("@TinNo", txttintaxupdate.Text);
            }
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@status", "UpdateSnAcct");
            cmd.Parameters.AddWithValue("@Bankname", txtbanknameupdate.Text);
            cmd.Parameters.AddWithValue("@Accountno", txtacnoupdate.Text);
            cmd.Parameters.AddWithValue("@ifsccode", txtifscupdate.Text);
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "SC";
            if (msg == "Vendor Updated Successfully")
            {
                JavaScript.UPAlertRedirect(Page, "Updated Successfully", "frmAddVendor.aspx");
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, "Updation Failed", "frmAddVendor.aspx");
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
    protected void rbtnadd_CheckedChanged(object sender, System.EventArgs e)
    {
        if (rbtnadd.Checked == true)
        {
            tbladdvendor.Visible = true;
            tblverifyvendor.Visible = false;
        }
        else
        {
            tbladdvendor.Visible = false;
            tblverifyvendor.Visible = true;
        }

    }
    protected void rbtnupdate_CheckedChanged(object sender, System.EventArgs e)
    {
        if (rbtnupdate.Checked == true)
        {
            tbladdvendor.Visible = false;
            tblverifyvendor.Visible = true;
            lblvatpanupdate.Text = "VAT No";
            lbltintaxupdate.Text = "TIN No";
            lblcstpfupdate.Text = "CST No";
            txtVNameupdate.Text = "";
            txtpnoupdate.Text = "";
            txtmnoupdate.Text = "";
            txtAddressupdate.Text = "";
            lblvtypeupdate.Text = "";
            txtbanknameupdate.Text = "";
            txtacnoupdate.Text = "";
            txtifscupdate.Text = "";
            txtvatpanupdate.Text = "";
            txtcstpfupdate.Text = "";
            txttintaxupdate.Text = "";
            txtvendorname.Enabled = true;
            txtvendorname.Text = "";
            //ddlvendorname.Items.Insert(0, new ListItem("Select Vendor", "0"));
        }
        else
        {
            tbladdvendor.Visible = true;
            tblverifyvendor.Visible = false;
        }
    }
    //protected void ddlupdatevendor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //da = new SqlDataAdapter("select (vendor_id +' , '+ vendor_name)as name ,vendor_id from vendor  where vendor_type='" + ddlupdatevendor.SelectedValue + "' and status='2'", con);
    //    //da.Fill(ds, "vendorinformation");
    //    //if (ds.Tables["vendorinformation"].Rows.Count > 0)
    //    //{
    //        //ddlvendorname.DataSource = ds.Tables["vendorinformation"];
    //        //ddlvendorname.DataTextField = "name";
    //        //ddlvendorname.DataValueField = "vendor_id";
    //        //ddlvendorname.DataBind();
    //        if (ddlupdatevendor.SelectedValue == "Service Provider")
    //        {
    //            lblvatpanupdate.Text = "PAN No";
    //            lbltintaxupdate.Text = "ServiceTax No";
    //            lblcstpfupdate.Text = "PF Reg No";
    //            ViewState["sp"] = "Service Provider";
    //        }
    //        else
    //        {
    //            lblvatpanupdate.Text = "VAT No";
    //            lbltintaxupdate.Text = "TIN No";
    //            lblcstpfupdate.Text = "CST No";
    //        }
    //        //ddlvendorname.Items.Insert(0, new ListItem("Select Vendor", "0"));
        
    //    //}

    //}
    //protected void ddlvendorname_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    loadvendor();
    //}

    public void loadvendor()
    {
        try
        {
            da = new SqlDataAdapter("select vendor_name,vendor_phone,vendor_mobile,servicetax_no,vat_no,pan_no,tin_no,address,cst_no,vendor_type,pf_no,bank_name,Account_no,ifsc_code,Gst_Applicable,vendor_id from vendor where vendor_id='" + ViewState["id"].ToString() + "'", con);
            da.Fill(ds, "vendorinfo");
            if (ds.Tables["vendorinfo"].Rows.Count > 0)
            {

                txtVNameupdate.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[0].ToString();
                txtpnoupdate.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[1].ToString();
                txtmnoupdate.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[2].ToString();
                txtAddressupdate.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[7].ToString();
                lblvtypeupdate.Text = ds.Tables["vendorinfo"].Rows[0].ItemArray[9].ToString();
                txtbanknameupdate.Text = ds.Tables["vendorinfo"].Rows[0]["bank_name"].ToString();
                txtacnoupdate.Text = ds.Tables["vendorinfo"].Rows[0]["Account_no"].ToString();
                txtifscupdate.Text = ds.Tables["vendorinfo"].Rows[0]["ifsc_code"].ToString();
                if (ds.Tables["vendorinfo"].Rows[0]["vendor_type"].ToString() == "Service Provider")
                {
                    txtvatpanupdate.Text = ds.Tables["vendorinfo"].Rows[0]["pan_no"].ToString();
                    txtcstpfupdate.Text = ds.Tables["vendorinfo"].Rows[0]["pf_no"].ToString();
                    txttintaxupdate.Text = ds.Tables["vendorinfo"].Rows[0]["servicetax_no"].ToString();
                    //lblvatpanupdate.Text = "PAN No";
                    //lbltintaxupdate.Text = "ServiceTax No";
                    //lblcstpfupdate.Text = "PF Reg No";
                }
                else
                {
                    //ViewState["sprovider"] = ds.Tables["vendorinfo"].Rows[0]["vendor_type"].ToString();
                    txtvatpanupdate.Text = ds.Tables["vendorinfo"].Rows[0]["vat_no"].ToString();
                    txtcstpfupdate.Text = ds.Tables["vendorinfo"].Rows[0]["tin_no"].ToString();
                    txttintaxupdate.Text = ds.Tables["vendorinfo"].Rows[0]["cst_no"].ToString();
                    //lblvatpanupdate.Text = "VAT No";
                    //lbltintaxupdate.Text = "TIN No";
                    //lblcstpfupdate.Text = "CST No";
                }
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
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        txtvendorname.Enabled = true;
        Response.Redirect("frmAddVendor.aspx");

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> Searchvendors(string prefixText)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        con.Open();
        SqlCommand cmd = new SqlCommand("select (vendor_id +' , '+ vendor_name)as name ,vendor_id from vendor  where  vendor_name like @Name + '%' and status='2'", con);
        cmd.Parameters.AddWithValue("@Name", prefixText);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> CountryNames = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            CountryNames.Add(dt.Rows[i][0].ToString());
        }
        return CountryNames;
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        //tblstateu.Visible = true;
        var pyear = txtvendorname.Text;
        var listSplit = pyear.Split(',');
        var id = listSplit[0];
        txtvendorname.Enabled = false;
        ViewState["id"] = id;
        da = new SqlDataAdapter("select vendor_type from vendor  where vendor_id='" + id.ToString() + "' and status='2'", con);
        da.Fill(ds, "vendorinformation");
        if (ds.Tables["vendorinformation"].Rows.Count > 0)
        {
            if (ds.Tables["vendorinformation"].Rows[0].ItemArray[0].ToString() == "Service Provider")
            {
                lblvatpanupdate.Text = "PAN No";
                lbltintaxupdate.Text = "ServiceTax No";
                lblcstpfupdate.Text = "PF Reg No";
                ViewState["sp"] = "Service Provider";
            }
            else
            {
                lblvatpanupdate.Text = "VAT No";
                lbltintaxupdate.Text = "TIN No";
                lblcstpfupdate.Text = "CST No";
            }
        }
        loadvendor();
    }

    protected void statedetails()
    {
        try
        {
            foreach (GridViewRow other in gvother.Rows)
            {
                DropDownList ddlstate = (DropDownList)other.FindControl("ddlstates");
                da = new SqlDataAdapter("select State_Id,state from [states]", con);
                ds = new DataSet();
                da.Fill(ds, "gst");
                if (ds.Tables["gst"].Rows.Count > 0)
                {
                    ddlstate.Enabled = true;
                    ddlstate.Items.Clear();
                    ddlstate.DataSource = ds.Tables["gst"];
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
    protected void btnaddgst_Click(object sender, EventArgs e)
    {

        AddNewotherRow();
        statedetails();
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
    }

    #region GST START
    protected void rbtngst_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbtngst.SelectedIndex == 0)
        {
                trothergrid.Style["display"] = "block";               
                BindotherGridview();
                otherdcalist();
        }
        else
        {
            gvother.DataSource = null;
            gvother.DataBind();           
            trothergrid.Style["display"] = "none";        
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
    }
    public void loaddca()
    {
        foreach (GridViewRow record in gvother.Rows)
        {
            DropDownList states = (DropDownList)record.FindControl("ddlstates");
            if (states.SelectedValue == "")
            {
                states.Items.Insert(0, new ListItem("Select", "Select"));
                states.DataBind();
            }
        }
    }
    protected void BindotherGridview()
    {
        DataTable dtt = new DataTable();
        dtt.Columns.Add("S.No", typeof(int));
        dtt.Columns.Add("chkgst", typeof(string));
        dtt.Columns.Add("ddlstates", typeof(string));
        dtt.Columns.Add("txtregno", typeof(string));
        dtt.Columns.Add("ddlstatesid", typeof(string));   
        DataRow dr = dtt.NewRow();
        dr["chkgst"] = string.Empty;
        dr["ddlstates"] = "Select";       
        dr["txtregno"] = string.Empty;
        dr["ddlstatesid"] = "Select";      
        dtt.Rows.Add(dr);
        ViewState["Curtblother"] = dtt;
        gvother.DataSource = dtt;
        gvother.DataBind();
    }
    private void AddNewotherRow()
    {
        int rowIndex = 0;
        if (ViewState["Curtblother"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtblother"];
            DataRow drCurrentRow = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    DropDownList ddlstates = (DropDownList)gvother.Rows[rowIndex].Cells[1].FindControl("ddlstates");
                    TextBox txtregno = (TextBox)gvother.Rows[rowIndex].Cells[2].FindControl("txtregno");
                    drCurrentRow = dt.NewRow();
                    dt.Rows[i - 1]["ddlstates"] = ddlstates.SelectedItem.Text;
                    dt.Rows[i - 1]["txtregno"] = txtregno.Text;
                    dt.Rows[i - 1]["ddlstatesid"] = ddlstates.SelectedValue;                 
                    rowIndex++;
                }
                dt.Rows.Add(drCurrentRow);
                ViewState["Curtblother"] = dt;
                gvother.DataSource = dt;
                gvother.DataBind();
            }
        }
        SetOldotherData();
    }
    private void SetOldotherData()
    {
        int rowIndex = 0;
        if (ViewState["Curtblother"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtblother"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvother.Rows[rowIndex].Cells[0].FindControl("chkgst");
                    DropDownList ddlstate = (DropDownList)gvother.Rows[rowIndex].Cells[1].FindControl("ddlstates");
                    TextBox txtreg = (TextBox)gvother.Rows[rowIndex].Cells[2].FindControl("txtregno");
                    if (i < dt.Rows.Count - 1)
                    {
                        ddlstate.SelectedItem.Text = dt.Rows[i]["ddlstates"].ToString();
                        txtreg.Text = dt.Rows[i]["txtregno"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void gvother_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblother"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtblother"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtblother"] = dt;
                    gvother.DataSource = dt;
                    gvother.DataBind();
                    for (int i = 0; i < gvother.Rows.Count - 1; i++)
                    {
                        gvother.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetOldotherData();
                    otherdcalist();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    string dca = "";
    protected void otherdcalist()
    {
        try
        {
            foreach (GridViewRow other in gvother.Rows)
            {
                DropDownList ddlstates = (DropDownList)other.FindControl("ddlstates");
                da = new SqlDataAdapter("select State_Id,state from [states]", con);
                da.Fill(ds, "GSTStates");
                if (ds.Tables["GSTStates"].Rows.Count > 0)
                {
                    ddlstates.DataValueField = "State_Id";
                    ddlstates.DataTextField = "state";
                    ddlstates.DataSource = ds.Tables["GSTStates"];
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
    protected void btnaddothers_Click(object sender, EventArgs e)
    {

        AddNewotherRow();
        otherdcalist();
    }
    public int ot = 0;
    protected void gvother_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (ViewState["Curtblother"] != null)
                {
                    DataTable Objdt = ViewState["Curtblother"] as DataTable;
                    if (Objdt.Rows[ot]["ddlstates"].ToString() != "Select" && Objdt.Rows[ot]["ddlstates"].ToString() != "")
                    {
                        DropDownList ddlstates = (DropDownList)e.Row.FindControl("ddlstates");
                        TextBox txtregno = (TextBox)e.Row.FindControl("txtregno");
                        da = new SqlDataAdapter("select State_Id,state from [states]", con);
                        da.Fill(ds, "GSTStates");
                        if (ds.Tables["GSTStates"].Rows.Count > 0)
                        {
                            ddlstates.DataValueField = "State_Id";
                            ddlstates.DataTextField = "state";
                            ddlstates.DataSource = ds.Tables["GSTStates"];
                            ddlstates.DataBind();
                            ddlstates.Items.Insert(0, "Select");
                            ddlstates.SelectedValue = Objdt.Rows[ot]["ddlstatesid"].ToString();
                        }
                        txtregno.Text = Objdt.Rows[ot]["txtregno"].ToString();
                    }
                }
                ot = ot + 1;
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    #endregion GST END

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
