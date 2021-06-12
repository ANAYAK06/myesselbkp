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
public partial class VerifyClient : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet Objds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            fillgrid();
            tblverifyclient.Visible = false;

        }
    }
 
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string s1 = "";
            string s2 = "";          
            string cellval = GridView1.Rows[e.RowIndex].Cells[1].Text;          
            cmd.Connection = con;
            s1 = "delete from client where client_id='" + cellval + "'";
            s2 = "delete from Vendor_Client_GstNos where Ven_Supp_Client_id='" + cellval + "'";        
            cmd.CommandText = s1 + s2;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
            con.Close();
            if (j == true)
            {
                JavaScript.UPAlertRedirect(Page, "Sucessfully Deleted", "VerifyClient.aspx");
            }
            else
            {

                JavaScript.UPAlert(Page, "Deletion Failed");
            }           
            GridView1.EditIndex = -1;
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                da = new SqlDataAdapter("select ID,client_id,client_name,contact_personphoneno,tinno,panno,tanno,contact_person,address,date from client where status='1' ", con);
                da.Fill(ds, "fill");
                GridView1.DataSource = ds.Tables["fill"];
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string id = GridView1.SelectedValue.ToString();
            tblverifyclient.Visible = true;
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
            da = new SqlDataAdapter("select ID,client_id,client_name,contact_personphoneno,tinno,panno,tanno,contact_person,address,date,Gst_Applicable from client where status='1' AND id='" + id + "'", con);
            //da = new SqlDataAdapter("select vendor_name,vendor_phone,vendor_mobile,servicetax_no,vat_no,pan_no,tin_no,address,cst_no,vendor_type,pf_no,bank_name,Account_no,ifsc_code,vendor_id,Gst_Applicable from vendor where id='" + id + "'", con);
            da.Fill(ds, "vendorinfo");
            if (ds.Tables["vendorinfo"].Rows.Count > 0)
            {
                ViewState["ClientId"] = ds.Tables["vendorinfo"].Rows[0]["client_id"].ToString();
                txtClientName.Text = ds.Tables["vendorinfo"].Rows[0]["client_name"].ToString();
                txttin.Text = ds.Tables["vendorinfo"].Rows[0]["tinno"].ToString();
                txtpan.Text = ds.Tables["vendorinfo"].Rows[0]["panno"].ToString();
                txttan.Text = ds.Tables["vendorinfo"].Rows[0]["tanno"].ToString();
                txtPersonname.Text = ds.Tables["vendorinfo"].Rows[0]["contact_person"].ToString();
                txtPsnphoneno.Text = ds.Tables["vendorinfo"].Rows[0]["contact_personphoneno"].ToString();
                txtAddress.Text = ds.Tables["vendorinfo"].Rows[0]["address"].ToString();                
                if (ds.Tables["vendorinfo"].Rows[0]["Gst_Applicable"].ToString() == "Yes")
                {
                    da = new SqlDataAdapter("select st.State_id,st.state,vcg.gst_no from Vendor_Client_GstNos vcg join States st on vcg.state_id=st.State_Id where Ven_Supp_Client_id= '" + ds.Tables["vendorinfo"].Rows[0]["client_id"].ToString() + "'", con);
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
    protected void btnAddClient_Click(object sender, EventArgs e)
    {
      
        try
        {
            string States = "";
            string GSTNos = "";
            foreach (GridViewRow record in gvotheru.Rows)
            {
                if (rbtngstu.SelectedValue == "Yes")
                {
                    if (gvotheru != null)
                    {
                        if ((record.FindControl("chkgstu") as CheckBox).Checked)
                        {
                            States = States + (record.FindControl("ddlstatesu") as DropDownList).SelectedValue + ",";
                            GSTNos = GSTNos + (record.FindControl("txtregnou") as TextBox).Text + ",";
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Please Verify GST Nos");
                            break;
                        }
                    }
                }
            }

            cmd = new SqlCommand("sp_clients", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@States", States);
            cmd.Parameters.AddWithValue("@GSTNos", GSTNos);
            cmd.Parameters.Add("@clid", SqlDbType.VarChar).Value = ViewState["ClientId"].ToString();
            cmd.Parameters.Add("@clientname", SqlDbType.VarChar).Value = txtClientName.Text;
            cmd.Parameters.Add("@phoneno", SqlDbType.VarChar).Value = txtPsnphoneno.Text;
            cmd.Parameters.Add("@personname", SqlDbType.VarChar).Value = txtPersonname.Text;           
            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = txtAddress.Text;          
            cmd.Parameters.Add("@TINNO", SqlDbType.VarChar).Value = txttin.Text;
            cmd.Parameters.Add("@PANNO", SqlDbType.VarChar).Value = txtpan.Text;
            cmd.Parameters.Add("@TANNO", SqlDbType.VarChar).Value = txttan.Text;
            cmd.Parameters.AddWithValue("@roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "1";
            cmd.Parameters.Add("@Check", SqlDbType.VarChar).Value = "Verify";
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            ///string msg = "SSS";
            con.Close();
            if (msg == "Sucessfully Verified")
            {
                JavaScript.UPAlertRedirect(Page, msg, "VerifyClient.aspx");
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, msg, "VerifyClient.aspx");
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
