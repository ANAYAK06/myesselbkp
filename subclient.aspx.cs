using System;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class subclient : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    string date;

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            fillclient();
        }

    }
    protected void btnAddClient_Click(object sender, EventArgs e)
    {
        string obj = DateTime.Now.ToShortDateString();
        date = obj.ToString();

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
            cmd = new SqlCommand("sp_clients", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@States", States);
            cmd.Parameters.AddWithValue("@GSTNos", GSTNos);
            cmd.Parameters.Add("@phoneno", SqlDbType.VarChar).Value = txtPsnphoneno.Text;
            cmd.Parameters.Add("@personname", SqlDbType.VarChar).Value = txtpersonname.Text;
            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = txtBranch.Text;
            cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = date;
            cmd.Parameters.Add("@TINNO", SqlDbType.VarChar).Value = txttin.Text;
            cmd.Parameters.Add("@PANNO", SqlDbType.VarChar).Value = txtpan.Text;
            cmd.Parameters.Add("@TANNO", SqlDbType.VarChar).Value = txttan.Text;
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "2";
            cmd.Parameters.Add("@Clientid", SqlDbType.VarChar).Value = ddlclientid.SelectedValue;
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Sucessfully Inserted")
            {
                JavaScript.UPAlertRedirect(Page,msg, "subclient.aspx");
            }
            else
            {
                JavaScript.UPAlertRedirect(Page,msg, "subclient.aspx");
            }
        }
        catch (Exception ex)
        {

            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }

    public void fillclient()
    {
        try
        {
            da = new SqlDataAdapter("Select client_id,(client_id+' , '+Client_name)as name from client where status='2' ", con);
            da.Fill(ds, "client");
            if (ds.Tables["client"].Rows.Count > 0)
            {
                ddlclientid.DataTextField = "name";
                ddlclientid.DataValueField = "client_id";
                ddlclientid.DataSource = ds.Tables["client"];
                ddlclientid.DataBind();
                ddlclientid.Items.Insert(0, "Select");
            }
            else
            {
                ddlclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ddlclientid.SelectedItem.Text = "Select";
    
        txtBranch.Text = "";
        txtpersonname.Text = "";
        txtpersonname.Text = "";
        txtPsnphoneno.Text = "";
     
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
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
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
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
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
            // ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:check()", true);
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
}
