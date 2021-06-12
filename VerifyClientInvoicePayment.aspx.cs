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

public partial class VerifyClientInvoicePayment : System.Web.UI.Page
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
            BindInvoicePayments();
            BindBanks();
        }
    }
    public void BindInvoicePayments()
    {
        try
        {
            da = new SqlDataAdapter("sp_GetClientInvoicesPayments", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Flag", "Bind Invoices");        
            da.Fill(ds, "Invoices");
            if (ds.Tables["Invoices"].Rows.Count > 0)
            {
                GvdInvoices.DataSource = ds.Tables["Invoices"];
                GvdInvoices.DataBind();
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    public void BindBanks()
    {
        try
        {
            da = new SqlDataAdapter("select bank_name from bank_branch where Status='3'",con);          
            da.Fill(ds, "Banks");
            if (ds.Tables["Banks"].Rows.Count > 0)
            {
                ddlfrom.DataSource = ds.Tables["Banks"];
                ddlfrom.DataTextField = "bank_name";
                ddlfrom.DataValueField = "bank_name";
                ddlfrom.DataBind();
                ddlfrom.Items.Insert(0, new ListItem("Select"));
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    protected void GvdInvoices_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Ded"] = null;
        ViewInvoiceInfo(GvdInvoices.SelectedValue.ToString());
        ViewState["id"] = GvdInvoices.SelectedValue.ToString();
    }
    public void ViewInvoiceInfo(string Id)
    {
        try
        {
            da = new SqlDataAdapter("sp_GetClientInvoicesPayments", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Flag", "Get Seleted Invoice");
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            DataSet Objds = new DataSet();
            da.Fill(Objds, "InvoiceInfo");
            if (Objds.Tables["InvoiceInfo"].Rows.Count > 0)
            {
                trclient.Visible = true;
                trinv.Visible = true;
                trdebit.Visible = true;
                paytype.Visible = true;
                Invoice.Visible = true;
                lblindt.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Invoice_Date"].ToString();
                lblinmkgdate.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Inv_Making_Date"].ToString();
                lblclientid.Text = Objds.Tables["InvoiceInfo"].Rows[0]["client_id"].ToString();
                lblsubclient.Text = Objds.Tables["InvoiceInfo"].Rows[0]["subclient_id"].ToString();
                lblrano.Text = Objds.Tables["InvoiceInfo"].Rows[0]["RA_NO"].ToString();
                lblbasic.Text = Objds.Tables["InvoiceInfo"].Rows[0]["BasicValue"].ToString();
                lblcccode.Text = Objds.Tables["InvoiceInfo"].Rows[0]["CC_Code"].ToString();
                lblpo.Text = Objds.Tables["InvoiceInfo"].Rows[0]["PO_NO"].ToString();
                lblinno.Text = Objds.Tables["InvoiceInfo"].Rows[0]["InvoiceNo"].ToString();
                //txtinvvalue.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Total"].ToString();              
                //txtdescription.Text = Objds.Tables["InvoiceInfo"].Rows[0]["comments"].ToString();
                if (Objds.Tables["InvoiceInfo4"].Rows.Count > 0)
                {
                    ddlfrom.SelectedItem.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["Bank_Name"].ToString();
                    txtcheque.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["No"].ToString();
                    txtdate.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["DATE"].ToString();
                    txtdesc.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["Description"].ToString();
                    txtamt.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["Credit"].ToString();
                    txtretention.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["retention_balance"].ToString();
                    txthold.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["Hold_Balance"].ToString();
                    txtadvance.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["Advance"].ToString();
                }
                if (Objds.Tables["InvoiceInfo1"].Rows.Count > 0)
                {
                    gvtaxes.DataSource = Objds.Tables["InvoiceInfo1"];
                    gvtaxes.DataBind();
                    lbltaxes.Text = Objds.Tables["InvoiceInfo5"].Rows[0][0].ToString();

                    trviewtax.Visible = true;
                }
                else
                {
                    trviewtax.Visible = false;
                }

                if (Objds.Tables["InvoiceInfo2"].Rows.Count > 0)
                {
                    gvcess.DataSource = Objds.Tables["InvoiceInfo2"];
                    gvcess.DataBind();
                    txtcess.Text = Objds.Tables["InvoiceInfo6"].Rows[0][0].ToString();
                    trviewcess.Visible = true;
                }
                else
                {
                    trviewcess.Visible = false;
                }
                if (Objds.Tables["InvoiceInfo3"].Rows.Count > 0)
                {
                    rbtndeductioncharges.SelectedIndex = 0;
                    DataTable dtt = new DataTable();
                    dtt.Columns.Add("S.No", typeof(int));

                    dtt.Columns.Add("chkdeduction", typeof(string));
                    dtt.Columns.Add("ddlisother", typeof(string));
                    dtt.Columns.Add("ddlothercc", typeof(string));
                    dtt.Columns.Add("ddldeductiondca", typeof(string));
                    dtt.Columns.Add("ddldeductionsdca", typeof(string));
                    dtt.Columns.Add("txtdeductionamount", typeof(string));
                    dtt.Columns.Add("ddldeductiondcacode", typeof(string));
                    dtt.Columns.Add("ddldeductionSubdcacode", typeof(string));
                    dtt.Columns.Add("ddlothercccode", typeof(string));
                    for (int i = 0; i < Objds.Tables["InvoiceInfo3"].Rows.Count; i++)
                    {
                        DataRow dr = dtt.NewRow();
                        //dr["rowid"] = 1;
                        dr["chkdeduction"] = string.Empty;
                        if (Objds.Tables["InvoiceInfo3"].Rows[i]["CC_Code"].ToString() != lblcccode.Text)
                        {
                            dr["ddlisother"] = "Yes";
                        }
                        else
                        {
                            dr["ddlisother"] = "No";
                        }
                        dr["ddlothercc"] = Objds.Tables["InvoiceInfo3"].Rows[i]["CC_Code"].ToString();
                        dr["ddldeductiondca"] = Objds.Tables["InvoiceInfo3"].Rows[i]["DCA_Code"].ToString();
                        dr["ddldeductionsdca"] = Objds.Tables["InvoiceInfo3"].Rows[i]["SubDCA_Code"].ToString();
                        dr["txtdeductionamount"] = Objds.Tables["InvoiceInfo3"].Rows[i]["DeducationValue"].ToString();
                        dr["ddldeductiondcacode"] = Objds.Tables["InvoiceInfo3"].Rows[i]["DCA_Code"].ToString();
                        dr["ddldeductionSubdcacode"] = Objds.Tables["InvoiceInfo3"].Rows[i]["SubDCA_Code"].ToString();
                        dr["ddlothercccode"] = Objds.Tables["InvoiceInfo3"].Rows[i]["CC_Code"].ToString();
                       
                        dtt.Rows.Add(dr);
                    }
                    ViewState["Curtbldeduction"] = dtt;
                    ViewState["Ded"] = dtt;
                    gvdeduction.DataSource = dtt;
                    gvdeduction.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:calculatededuction()", true);
                }
                else
                {
                    rbtndeductioncharges.SelectedIndex = 1;
                }
               
                trdeduction.Visible = true;
                trret.Visible = true;
                bank.Visible = true;
                bank1.Visible = true;
                tramt.Visible = true;
                btn.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
    #region Deduction START
    protected void rbtndeductioncharges_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbtndeductioncharges.SelectedIndex == 0)
        {
            if (lblindt.Text != "")
            {
                
                trdeductiongrid.Style["display"] = "block";
                hfdeduction.Value = Convert.ToInt16(0).ToString();
                txtdeduction.Text = Convert.ToInt16(0).ToString();
                BinddeductionGridview();
                deductiondcalist();
            }
        }
        else
        {
          
            trdeductiongrid.Style["display"] = "none";
            hfdeduction.Value = Convert.ToInt16(0).ToString();
            txtdeduction.Text = Convert.ToInt16(0).ToString();
            gvdeduction.DataSource = null;
            gvdeduction.DataBind();            
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        }
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);
    }
    public void loaddeddca()
    {
        foreach (GridViewRow record in gvdeduction.Rows)
        {
            DropDownList ded = (DropDownList)record.FindControl("ddldeductiondca");
            if (ded.SelectedValue == "")
            {
                ded.Items.Insert(0, new ListItem("Select", "Select"));
                ded.DataBind();
            }
        }
    }
    protected void BinddeductionGridview()
    {
        DataTable dtt = new DataTable();
        dtt.Columns.Add("S.No", typeof(int));
        dtt.Columns.Add("chkdeduction", typeof(string));
        dtt.Columns.Add("ddlisother", typeof(string));
        dtt.Columns.Add("ddlothercc", typeof(string));
        dtt.Columns.Add("ddldeductiondca", typeof(string));
        dtt.Columns.Add("ddldeductionsdca", typeof(string));
        dtt.Columns.Add("txtdeductionamount", typeof(string));
        dtt.Columns.Add("ddldeductiondcacode", typeof(string));
        dtt.Columns.Add("ddldeductionSubdcacode", typeof(string));
        dtt.Columns.Add("ddlothercccode", typeof(string));
        DataRow dr = dtt.NewRow();
        //dr["rowid"] = 1;
        dr["chkdeduction"] = string.Empty;
        dr["ddlisother"] = "Select";
        dr["ddlothercc"] = "Select";
        dr["ddldeductiondca"] = "Select";
        dr["ddldeductionsdca"] = "Select";
        dr["txtdeductionamount"] = string.Empty;
        dr["ddldeductiondcacode"] = "Select";
        dr["ddldeductionSubdcacode"] = "Select";
        dr["ddlothercccode"] = "Select";
        dtt.Rows.Add(dr);
        //ViewState["Curtbldeduction"] = dtt;
        //gvdeduction.DataSource = dtt;
        //gvdeduction.DataBind();
        //BindIsotherCC();

        ViewState["Curtbldeduction"] = dtt;
        if (ViewState["Ded"] == null)
        {
            ViewState["Curtbldeduction"] = dtt;
            gvdeduction.DataSource = dtt;
            gvdeduction.DataBind();
            BindIsotherCC();
            
        }
        else
        {
            ViewState["Curtbldeduction"] = ViewState["Ded"] as DataTable;
            gvdeduction.DataSource = ViewState["Curtbldeduction"] as DataTable;
            gvdeduction.DataBind();
            BindIsotherCC();
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:calculatededuction()", true);
    }
    public void BindIsotherCC()
    {
        foreach (GridViewRow record in gvdeduction.Rows)
        {
            DropDownList type = (DropDownList)record.FindControl("ddlothercc");
            if (type.SelectedValue == "")
            {
                type.Items.Insert(0, new ListItem("Select", "Select"));
                type.Items.Insert(1, new ListItem("Yes", "Yes"));
                type.Items.Insert(2, new ListItem("No", "No"));
                type.DataBind();
            }
        }
    }
    private void AddNewdeductionRow()
    {
        int rowIndex = 0;

        if (ViewState["Curtbldeduction"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbldeduction"];
            DataRow drCurrentRow = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    DropDownList ddlisother = (DropDownList)gvdeduction.Rows[rowIndex].Cells[1].FindControl("ddlothercc");
                    DropDownList ddlothercc = (DropDownList)gvdeduction.Rows[rowIndex].Cells[2].FindControl("ddlccode");
                    DropDownList ddldeductiondca = (DropDownList)gvdeduction.Rows[rowIndex].Cells[3].FindControl("ddldeductiondca");
                    DropDownList ddldeductionsdca = (DropDownList)gvdeduction.Rows[rowIndex].Cells[4].FindControl("ddldeductionsdca");
                    TextBox txtdeductionamount = (TextBox)gvdeduction.Rows[rowIndex].Cells[5].FindControl("txtdeductionamount");
                    drCurrentRow = dt.NewRow();
                    dt.Rows[i - 1]["ddlisother"] = ddlisother.SelectedItem.Text;
                    dt.Rows[i - 1]["ddlothercc"] = ddlothercc.SelectedItem.Text;
                    dt.Rows[i - 1]["ddldeductiondca"] = ddldeductiondca.SelectedItem.Text;
                    dt.Rows[i - 1]["ddldeductionsdca"] = ddldeductionsdca.SelectedItem.Text;
                    dt.Rows[i - 1]["txtdeductionamount"] = txtdeductionamount.Text;
                    dt.Rows[i - 1]["ddldeductiondcacode"] = ddldeductiondca.SelectedValue;
                    dt.Rows[i - 1]["ddldeductionSubdcacode"] = ddldeductionsdca.SelectedValue;
                    dt.Rows[i - 1]["ddlothercccode"] = ddlothercc.SelectedValue;
                    rowIndex++;
                }
                dt.Rows.Add(drCurrentRow);
                ViewState["Curtbldeduction"] = dt;
                gvdeduction.DataSource = dt;
                gvdeduction.DataBind();
            }
        }
        SetOlddeductionData();
    }
    private void SetOlddeductionData()
    {
        int rowIndex = 0;
        if (ViewState["Curtbldeduction"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbldeduction"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox chkdeduction = (CheckBox)gvdeduction.Rows[rowIndex].Cells[0].FindControl("chkdeduction");
                    DropDownList ddlisother = (DropDownList)gvdeduction.Rows[rowIndex].Cells[1].FindControl("ddlothercc");
                    DropDownList ddlothercc = (DropDownList)gvdeduction.Rows[rowIndex].Cells[2].FindControl("ddlccode");
                    DropDownList ddldeductiondca = (DropDownList)gvdeduction.Rows[rowIndex].Cells[1].FindControl("ddldeductiondca");
                    DropDownList ddldeductionsdca = (DropDownList)gvdeduction.Rows[rowIndex].Cells[2].FindControl("ddldeductionsdca");
                    TextBox txtdeductionamount = (TextBox)gvdeduction.Rows[rowIndex].Cells[3].FindControl("txtdeductionamount");
                    BindIsotherCC();

                    if (i < dt.Rows.Count - 1)
                    {
                        ddlisother.SelectedItem.Text = dt.Rows[i]["ddlisother"].ToString();
                        ddlothercc.SelectedItem.Text = dt.Rows[i]["ddlothercc"].ToString();
                        ddldeductiondca.SelectedItem.Text = dt.Rows[i]["ddldeductiondca"].ToString();
                        ddldeductionsdca.SelectedItem.Text = dt.Rows[i]["ddldeductionsdca"].ToString();
                        txtdeductionamount.Text = dt.Rows[i]["txtdeductionamount"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void gvdeduction_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtbldeduction"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbldeduction"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtbldeduction"] = dt;
                    gvdeduction.DataSource = dt;
                    gvdeduction.DataBind();
                    for (int i = 0; i < gvdeduction.Rows.Count - 1; i++)
                    {
                        gvdeduction.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetOlddeductionData();
                    deductiondcalist();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:calculatededuction();Total();", true);
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    string ddca = "";
    protected void deductiondcalist()
    {
        try
        {
            foreach (GridViewRow ded in gvdeduction.Rows)
            {
                DropDownList ddldeductiondca = (DropDownList)ded.FindControl("ddldeductiondca");
                DropDownList ddlothercc = (DropDownList)ded.FindControl("ddlothercc");
                DropDownList ddlccode = (DropDownList)ded.FindControl("ddlccode");
                if (ddlothercc.SelectedValue != "Select")
                {
                    da = new SqlDataAdapter("TaxDcas_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = lblindt.Text;
                    if (ddlothercc.SelectedValue == "No")
                    {
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblcccode.Text;
                    }
                    if (ddlothercc.SelectedValue == "Yes")
                    {
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlccode.SelectedValue;
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "ClientDeductions";
                    ds = new DataSet();
                    da.Fill(ds, "deddcas");
                    if (ds.Tables["deddcas"].Rows.Count > 0)
                    {
                        ddldeductiondca.Enabled = true;
                        ddldeductiondca.Items.Clear();
                        ddldeductiondca.DataSource = ds.Tables["deddcas"];
                        ddldeductiondca.DataTextField = "mapdca_code";
                        ddldeductiondca.DataValueField = "dca_code";
                        ddldeductiondca.DataBind();
                        ddldeductiondca.Items.Insert(0, new ListItem("Select"));
                    }
                    else
                    {
                        ddldeductiondca.DataSource = null;
                        ddldeductiondca.DataBind();
                        ddldeductiondca.Items.Insert(0, new ListItem("Select"));
                        ddldeductiondca.Enabled = false;
                        ddldeductiondca.CssClass = "class";
                    }
                }

            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void ddlccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlccode = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddlccode.NamingContainer;
            DropDownList ddldeductiondca = (DropDownList)currentRow.FindControl("ddldeductiondca");
            DropDownList ddlothercc = (DropDownList)currentRow.FindControl("ddlothercc");
            DropDownList ddlccodes = (DropDownList)currentRow.FindControl("ddlccode");
            if (ddlothercc.SelectedValue != "Select")
            {

                da = new SqlDataAdapter("TaxDcas_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = lblindt.Text;
                if (ddlothercc.SelectedValue == "No")
                {
                    da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblcccode.Text;
                }
                if (ddlothercc.SelectedValue == "Yes")
                {
                    da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlccodes.SelectedValue;
                }
                da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "ClientDeductions";
                ds = new DataSet();
                da.Fill(ds, "deddcas");
                if (ds.Tables["deddcas"].Rows.Count > 0)
                {
                    ddldeductiondca.Enabled = true;
                    ddldeductiondca.Items.Clear();
                    ddldeductiondca.DataSource = ds.Tables["deddcas"];
                    ddldeductiondca.DataTextField = "mapdca_code";
                    ddldeductiondca.DataValueField = "dca_code";
                    ddldeductiondca.DataBind();
                    ddldeductiondca.Items.Insert(0, new ListItem("Select"));
                }
                else
                {
                    ddldeductiondca.DataSource = null;
                    ddldeductiondca.DataBind();
                    ddldeductiondca.Items.Insert(0, new ListItem("Select"));
                    ddldeductiondca.Enabled = false;
                    ddldeductiondca.CssClass = "class";
                }
            }


        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void ddldeductiondca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddldeductiondca = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddldeductiondca.NamingContainer;
            DropDownList ddldeductionsdca = (DropDownList)currentRow.FindControl("ddldeductionsdca");
            if (ddldeductiondca != null && ddldeductiondca.SelectedIndex > 0 && ddldeductionsdca != null)
            {
                da = new SqlDataAdapter("select subdca_code,(mapsubdca_code+' , '+subdca_name)as mapsubdca_code from subdca where dca_code='" + ddldeductiondca.SelectedValue + "'", con);
                ds = new DataSet();
                da.Fill(ds, "dedsdcas");
                ddldeductionsdca.DataSource = ds.Tables["dedsdcas"];
                ddldeductionsdca.DataTextField = "mapsubdca_code";
                ddldeductionsdca.DataValueField = "subdca_code";
                ddldeductionsdca.DataBind();
                ddldeductionsdca.Items.Insert(0, new ListItem("Select"));
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void btnadddeduction_Click(object sender, EventArgs e)
    {

        AddNewdeductionRow();
        deductiondcalist();
    }
    public int dd = 0;
    protected void gvdeduction_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["Curtbldeduction"] != null)
                {
                    DropDownList ddlisother = (DropDownList)e.Row.FindControl("ddlothercc");
                    ddlisother.Items.Insert(0, new ListItem("Select", "Select"));
                    ddlisother.Items.Insert(1, new ListItem("Yes", "Yes"));
                    ddlisother.Items.Insert(2, new ListItem("No", "No"));
                    DropDownList ddldeductiondca = (DropDownList)e.Row.FindControl("ddldeductiondca");
                    DropDownList ddlccode = (DropDownList)e.Row.FindControl("ddlccode");
                    da = new SqlDataAdapter("TaxDcas_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = lblindt.Text;
                    if (ddlisother.SelectedValue == "Yes")
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblcccode.Text;
                    else if (ddlisother.SelectedValue == "No")
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlccode.SelectedValue;
                    else
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblcccode.Text;
                    da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "ClientDeductions";
                    ds = new DataSet();
                    da.Fill(ds, "deductiondcas");
                    if (ds.Tables["deductiondcas"].Rows.Count > 0)
                    {
                        ddldeductiondca.Enabled = true;
                        ddldeductiondca.Items.Clear();
                        ddldeductiondca.DataSource = ds.Tables["deductiondcas"];
                        ddldeductiondca.DataTextField = "mapdca_code";
                        ddldeductiondca.DataValueField = "dca_code";
                        ddldeductiondca.DataBind();
                        ddldeductiondca.Items.Insert(0, new ListItem("Select"));

                    }
                    DataTable Objdt = ViewState["Curtbldeduction"] as DataTable;
                    if (Objdt.Rows[dd]["ddldeductiondca"].ToString() != "Select" && Objdt.Rows[dd]["ddldeductiondca"].ToString() != "" && Objdt.Rows[dd]["ddldeductionsdca"].ToString() != "Select" && Objdt.Rows[dd]["ddldeductionsdca"].ToString() != "")
                    {

                        ddlisother.SelectedValue = Objdt.Rows[dd]["ddlisother"].ToString();
                        DropDownList ddldeductioncc = (DropDownList)e.Row.FindControl("ddlccode");
                        TextBox txtdedamount = (TextBox)e.Row.FindControl("txtdeductionamount");
                        DropDownList ddldeductiondcas = (DropDownList)e.Row.FindControl("ddldeductiondca");
                        
                        if (Objdt.Rows[dd]["ddlisother"].ToString() == "Yes")
                        {
                            //da = new SqlDataAdapter("select distinct cc_code,(cc_code+' , '+cc_name)as name from cost_center  where  status in ('Old','New') and cc_code !='" + lblcccode.Text + "' ", con);
                            da = new SqlDataAdapter("select cc_code,(cc_code+' , '+cc_name)as name from cost_center  where status in ('Old','New') and cc_code !='" + lblcccode.Text + "' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC  ", con);
                            da.Fill(ds, "ISOtherCC1");
                            if (ds.Tables["ISOtherCC1"].Rows.Count > 0)
                            {
                                ddldeductioncc.DataSource = ds.Tables["ISOtherCC1"];
                                ddldeductioncc.DataValueField = "cc_code";
                                ddldeductioncc.DataTextField = "name";
                                ddldeductioncc.Items.Insert(0, new ListItem("Select"));
                                ddldeductioncc.DataBind();

                            }

                        }
                        else if (Objdt.Rows[dd]["ddlisother"].ToString() == "No")
                        {
                            ddldeductioncc.Items.Clear();
                            ddldeductioncc.Items.Insert(0, new ListItem("Select", "Select"));
                            ddldeductioncc.Items.Insert(0, new ListItem(lblcccode.Text, lblcccode.Text));
                            ddldeductioncc.DataBind();
                        }
                        ddldeductioncc.SelectedValue = Objdt.Rows[dd]["ddlothercccode"].ToString();
                        da = new SqlDataAdapter("TaxDcas_sp", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = lblindt.Text;
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddldeductioncc.SelectedValue;
                        da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "ClientDeductions";
                        ds = new DataSet();
                        da.Fill(ds, "deductiondcas1");
                        if (ds.Tables["deductiondcas1"].Rows.Count > 0)
                        {
                            ddldeductiondcas.Enabled = true;
                            ddldeductiondcas.Items.Clear();
                            ddldeductiondcas.DataSource = ds.Tables["deductiondcas1"];
                            ddldeductiondcas.DataTextField = "mapdca_code";
                            ddldeductiondcas.DataValueField = "dca_code";
                            ddldeductiondcas.DataBind();
                            ddldeductiondcas.Items.Insert(0, new ListItem("Select"));
                            ddldeductiondcas.SelectedValue = Objdt.Rows[dd]["ddldeductiondcacode"].ToString();

                        }
                        
                        DropDownList ddldeductionsdca = (DropDownList)e.Row.FindControl("ddldeductionsdca");
                        da = new SqlDataAdapter("select subdca_code,(mapsubdca_code+' , '+subdca_name)as mapsubdca_code from subdca where dca_code='" + Objdt.Rows[dd]["ddldeductiondcacode"].ToString() + "'", con);
                        ds = new DataSet();
                        da.Fill(ds, "deductionsdcas");
                        ddldeductionsdca.DataSource = ds.Tables["deductionsdcas"];
                        ddldeductionsdca.DataTextField = "mapsubdca_code";
                        ddldeductionsdca.DataValueField = "subdca_code";
                        ddldeductionsdca.DataBind();
                        ddldeductionsdca.Items.Insert(0, new ListItem("Select"));
                        ddldeductionsdca.SelectedValue = Objdt.Rows[dd]["ddldeductionSubdcacode"].ToString();
                        txtdedamount.Text = Objdt.Rows[dd]["txtdeductionamount"].ToString();
                    }
                }
                dd = dd + 1;

            }

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    #endregion Deduction END
    protected void ddlothercc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlothercc = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddlothercc.NamingContainer;
            DropDownList ddlothercccode = (DropDownList)currentRow.FindControl("ddlccode");
            if (ddlothercc.SelectedValue == "Yes")
            {
                //da = new SqlDataAdapter("select distinct cc_code,(cc_code+' , '+cc_name)as name from cost_center  where  status in ('Old','New') and cc_code !='" + lblcccode.Text + "' ", con);
                da = new SqlDataAdapter("select cc_code,(cc_code+' , '+cc_name)as name from cost_center  where status in ('Old','New') and cc_code !='" + lblcccode.Text + "' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC  ", con);
                da.Fill(ds, "OtherCC");
                if (ds.Tables["OtherCC"].Rows.Count > 0)
                {
                    ddlothercccode.Items.Clear();
                    ddlothercccode.DataSource = ds.Tables["OtherCC"];
                    ddlothercccode.DataValueField = "cc_code";
                    ddlothercccode.DataTextField = "name";
                    ddlothercccode.Items.Insert(0, new ListItem("Select", "Select"));
                    ddlothercccode.DataBind();
                }
            }
            else if (ddlothercc.SelectedValue == "No")
            {
                ddlothercccode.Items.Clear();
                ddlothercccode.Items.Insert(0, new ListItem("Select", "Select"));
                ddlothercccode.Items.Insert(0, new ListItem(lblcccode.Text, lblcccode.Text));
                ddlothercccode.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string CCCodes = "";
            string Dcas = "";
            string Subdcas = "";
            string Amounts = "";



            foreach (GridViewRow record in gvdeduction.Rows)
            {
                if ((record.FindControl("chkdeduction") as CheckBox).Checked)
                {
                    CCCodes = CCCodes + (record.FindControl("ddlccode") as DropDownList).SelectedValue + ",";
                    Dcas = Dcas + (record.FindControl("ddldeductiondca") as DropDownList).SelectedValue + ",";
                    Subdcas = Subdcas + (record.FindControl("ddldeductionsdca") as DropDownList).SelectedValue + ",";
                    Amounts = Amounts + (record.FindControl("txtdeductionamount") as TextBox).Text + ",";
                }
            }


            cmd.CommandText = "sp_ClientInvoiceRecieving";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Dcas", Dcas);
            cmd.Parameters.AddWithValue("@SubDcas", Subdcas);
            cmd.Parameters.AddWithValue("@CCCodes", CCCodes);
            cmd.Parameters.AddWithValue("@Amounts", Amounts);
            cmd.Parameters.AddWithValue("@Advance", txtadvance.Text);
            cmd.Parameters.AddWithValue("@Retention", txtretention.Text);
            cmd.Parameters.AddWithValue("@Hold", txthold.Text);
            cmd.Parameters.AddWithValue("@BankName", ddlfrom.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@InvDate", lblindt.Text);
            cmd.Parameters.AddWithValue("@Date", txtdate.Text);
            cmd.Parameters.AddWithValue("@No", txtcheque.Text);
            cmd.Parameters.AddWithValue("@Remarks", txtdesc.Text);
            cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
           
            cmd.Parameters.AddWithValue("@CCCode", lblcccode.Text);
            cmd.Parameters.AddWithValue("@InvoiceNo", lblinno.Text);
            
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Type", ddlstatus.SelectedItem.Text);

            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "invalid";
            con.Close();
            if (msg == "Invoice Credited Successfully" || msg == "Rejected Successfully")
            {
                JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
        }
        catch (Exception ex)
        {

        }
    }
}