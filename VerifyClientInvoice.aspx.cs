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

public partial class VerifyClientInvoice : System.Web.UI.Page
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
            BindInvoices();
        }
    }
    public void BindInvoices()
    {
        try
        {
            da = new SqlDataAdapter("GetCustomerInvoice", con);
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


    #region taxstarts
    protected void ddltaxtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddltype = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddltype.NamingContainer;
            DropDownList ddltaxdca = (DropDownList)currentRow.FindControl("ddltaxdca");
            DropDownList ddltaxsdca = (DropDownList)currentRow.FindControl("ddltaxsdca");
            DropDownList ddltaxnos = (DropDownList)currentRow.FindControl("ddltaxnos");
            if (ddltype != null && ddltype.SelectedIndex > 0 && ddltaxdca != null)
            {
                string selectedtype = ddltype.SelectedValue;
                da = new SqlDataAdapter("TaxDcasNew_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = selectedtype;
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtindt.Text;
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblCCCode.Text;
                da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Tax";
                ds = new DataSet();
                da.Fill(ds, "taxdcas");
                if (ds.Tables["taxdcas"].Rows.Count > 0)
                {
                    ddltaxdca.Enabled = true;
                    ddltaxdca.Items.Clear();
                    ddltaxdca.DataSource = ds.Tables["taxdcas"];
                    ddltaxdca.DataTextField = "mapdca_code";
                    ddltaxdca.DataValueField = "dca_code";
                    ddltaxdca.DataBind();
                    ddltaxdca.Items.Insert(0, new ListItem("Select"));
                    ViewState["Tax"] = ddltaxdca.SelectedValue;
                }
                else
                {
                    ddltaxdca.DataSource = null;
                    ddltaxdca.DataBind();
                    ddltaxdca.Items.Insert(0, new ListItem("Select"));
                    ddltaxdca.Enabled = false;
                    ddltaxdca.CssClass = "class";
                    //tdinserttaxes.Visible = false;
                }

            }
            else if (ddltype.SelectedIndex == 0)
            {
                ddltaxdca.SelectedItem.Text = "Select";
                ddltaxsdca.SelectedItem.Text = "Select";
                ddltaxnos.SelectedItem.Text = "Select";

            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    public void loadtaxtype()
    {
        try
        {
            foreach (GridViewRow record in gvtaxes.Rows)
            {
                DropDownList type = (DropDownList)record.FindControl("ddltype");
                if (type.SelectedValue == "")
                {
                    type.Items.Insert(0, new ListItem("Select", "Select"));
                    type.Items.Insert(1, new ListItem("Creditable", "Creditable"));
                    type.Items.Insert(2, new ListItem("Non-Creditable", "Non-Creditable"));
                    type.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    protected void BindtaxGridview()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("S.No", typeof(int));
        dt.Columns.Add("chkSelecttaxes", typeof(string));
        dt.Columns.Add("ddltype", typeof(string));
        dt.Columns.Add("ddltaxdca", typeof(string));
        dt.Columns.Add("ddltaxsdca", typeof(string));
        dt.Columns.Add("ddltaxnos", typeof(string));
        dt.Columns.Add("txttaxamount", typeof(string));
        dt.Columns.Add("ddltaxdcacode", typeof(string));
        dt.Columns.Add("ddltaxSubdcacode", typeof(string));
        dt.Columns.Add("ddltaxnumbers", typeof(string));

        DataRow dr = dt.NewRow();
        //dr["rowid"] = 1;
        dr["chkSelecttaxes"] = string.Empty;
        dr["ddltype"] = "Select";
        dr["ddltaxdca"] = "Select";
        dr["ddltaxsdca"] = "Select";
        dr["ddltaxnos"] = "Select";
        dr["txttaxamount"] = string.Empty;
        dr["ddltaxdcacode"] = "Select";
        dr["ddltaxSubdcacode"] = "Select";
        dr["ddltaxnumbers"] = "Select";
        dt.Rows.Add(dr);

        ViewState["Curtbl"] = dt;
        gvtaxes.DataSource = dt;
        gvtaxes.DataBind();
        loadtaxtype();

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        AddNewtaxRow();

    }
    private void AddNewtaxRow()
    {
        int rowIndex = 0;
        if (ViewState["Curtbl"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbl"];
            DataRow drCurrentRow = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    DropDownList ddltype = (DropDownList)gvtaxes.Rows[rowIndex].Cells[2].FindControl("ddltype");
                    DropDownList ddltaxdca = (DropDownList)gvtaxes.Rows[rowIndex].Cells[3].FindControl("ddltaxdca");
                    DropDownList ddltaxsdca = (DropDownList)gvtaxes.Rows[rowIndex].Cells[4].FindControl("ddltaxsdca");
                    DropDownList ddltaxnos = (DropDownList)gvtaxes.Rows[rowIndex].Cells[5].FindControl("ddltaxnos");
                    TextBox txttaxamount = (TextBox)gvtaxes.Rows[rowIndex].Cells[6].FindControl("txttaxamount");
                    drCurrentRow = dt.NewRow();
                    //drCurrentRow["rowid"] = i + 1;
                    //dt.Rows[i - 1]["chkSelecttaxes"] = chk;
                    dt.Rows[i - 1]["ddltype"] = ddltype.SelectedItem.Text;
                    dt.Rows[i - 1]["ddltaxdca"] = ddltaxdca.SelectedItem.Text;
                    dt.Rows[i - 1]["ddltaxsdca"] = ddltaxsdca.SelectedItem.Text;
                    dt.Rows[i - 1]["ddltaxnos"] = ddltaxnos.SelectedItem.Text;
                    dt.Rows[i - 1]["txttaxamount"] = txttaxamount.Text;
                    dt.Rows[i - 1]["ddltaxdcacode"] = ddltaxdca.SelectedValue;
                    dt.Rows[i - 1]["ddltaxSubdcacode"] = ddltaxsdca.SelectedValue;
                    dt.Rows[i - 1]["ddltaxnumbers"] = ddltaxnos.SelectedValue;
                    rowIndex++;
                }
                dt.Rows.Add(drCurrentRow);
                ViewState["Curtbl"] = dt;
                gvtaxes.DataSource = dt;
                gvtaxes.DataBind();
            }
        }
        SetOldtaxData();
    }
    private void SetOldtaxData()
    {
        int rowIndex = 0;

        if (ViewState["Curtbl"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbl"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvtaxes.Rows[rowIndex].Cells[0].FindControl("chkSelecttaxes");
                    DropDownList ddltype = (DropDownList)gvtaxes.Rows[rowIndex].Cells[1].FindControl("ddltype");
                    DropDownList ddltaxdca = (DropDownList)gvtaxes.Rows[rowIndex].Cells[2].FindControl("ddltaxdca");
                    DropDownList ddltaxsdca = (DropDownList)gvtaxes.Rows[rowIndex].Cells[3].FindControl("ddltaxsdca");
                    DropDownList ddltaxnos = (DropDownList)gvtaxes.Rows[rowIndex].Cells[4].FindControl("ddltaxnos");
                    TextBox txttaxamount = (TextBox)gvtaxes.Rows[rowIndex].Cells[5].FindControl("txttaxamount");
                    ddltype.Items.Clear();
                    loadtaxtype();
                    if (i < dt.Rows.Count - 1)
                    {
                        ddltype.SelectedItem.Text = dt.Rows[i]["ddltype"].ToString();
                        ddltaxdca.SelectedItem.Text = dt.Rows[i]["ddltaxdca"].ToString();
                        ddltaxsdca.SelectedItem.Text = dt.Rows[i]["ddltaxsdca"].ToString();
                        ddltaxnos.SelectedItem.Text = dt.Rows[i]["ddltaxnos"].ToString();
                        txttaxamount.Text = dt.Rows[i]["txttaxamount"].ToString();
                    }
                    rowIndex++;

                }
            }
        }
    }
    protected void gvtaxes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["Curtbl"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbl"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["Curtbl"] = dt;
                gvtaxes.DataSource = dt;
                gvtaxes.DataBind();
                for (int i = 0; i < gvtaxes.Rows.Count - 1; i++)
                {
                    gvtaxes.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetOldtaxData();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:calculatetaxes()", true);
    }

    protected void ddltaxdca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddltaxdca = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddltaxdca.NamingContainer;
            DropDownList ddltaxtype = (DropDownList)currentRow.FindControl("ddltype");
            DropDownList ddltaxsdca = (DropDownList)currentRow.FindControl("ddltaxsdca");
            DropDownList ddltaxnos = (DropDownList)currentRow.FindControl("ddltaxnos");
            ViewState["TAX"] = ddltaxdca.SelectedValue;
            if (ddltaxdca != null && ddltaxdca.SelectedIndex > 0 && ddltaxsdca != null)
            {
                da = new SqlDataAdapter("select subdca_code ,(mapsubdca_code+' , '+subdca_name)as mapsubdca_code from subdca where dca_code='" + ddltaxdca.SelectedValue + "'", con);
                ds = new DataSet();
                da.Fill(ds, "Taxsdcas");
                ddltaxsdca.DataSource = ds.Tables["Taxsdcas"];
                ddltaxsdca.DataTextField = "mapsubdca_code";
                ddltaxsdca.DataValueField = "subdca_code";
                ddltaxsdca.DataBind();
                ddltaxsdca.Items.Insert(0, new ListItem("Select"));
                //da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + ddltaxdca.SelectedValue + "' and STATUS='Active'", con);
                if (ddltaxdca.SelectedValue != "DCA-GST-NON-CR" && ddltaxdca.SelectedValue != "DCA-GST-CR")
                    da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + ddltaxdca.SelectedValue + "' and STATUS='Active'", con);
                else
                    if (lblCCCode.Text == "CCC")
                        da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3'", con);
                    else
                        da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='" + lblCCCode.Text + "' and gm.Status='3' UNION select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3' union select gst_no as tax_nos from GSTmaster where State_Id='12' and Status='3' ORDER by gst_no", con);

                ds.Clear();
                ds = new DataSet();
                da.Fill(ds, "taxvatdca");
                if (ds.Tables["taxvatdca"].Rows.Count > 0)
                {
                    ddltaxnos.Items.Clear();
                    ddltaxnos.DataSource = ds.Tables["taxvatdca"];
                    ddltaxnos.DataTextField = "Tax_nos";
                    ddltaxnos.DataValueField = "Tax_nos";
                    ddltaxnos.DataBind();
                    ddltaxnos.Items.Insert(0, new ListItem("Select"));
                }
                else
                {
                    ddltaxnos.Items.Clear();
                    ddltaxnos.Items.Insert(0, new ListItem("Select TaxNos"));
                }
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
        finally
        {
            con.Close();
        }

    }
    protected void btnAddTax_Click(object sender, EventArgs e)
    {
        txtindt.Enabled = false;
        btnaddtax.Visible = false;
        btnremovetax.Visible = true;
        traddtaxes.Style["display"] = "block";
        BindtaxGridview();


    }
    private decimal TaxAmount = (decimal)0.0;
    protected void btnremovetaxes_Click(object sender, EventArgs e)
    {
        try
        {
            btnaddtax.Visible = true;
            btnremovetax.Visible = false;
            gvtaxes.DataSource = null;
            gvtaxes.DataBind();
            txtinvvalue.Text = (Convert.ToDecimal(txtinvvalue.Text) - Convert.ToDecimal(lbltaxes.Text)).ToString();
            hftaxtotal.Value = Convert.ToInt16(0).ToString();
            lbltaxes.Text = Convert.ToInt16(0).ToString();
            txtindt.Enabled = true;

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    public int i = 0;
    protected void gvtaxes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (ViewState["Curtbl"] != null)
                {
                    DataTable Objdt = ViewState["Curtbl"] as DataTable;
                    if (Objdt.Rows[i]["ddltype"].ToString() != "Select" && Objdt.Rows[i]["ddltype"].ToString() != "" && Objdt.Rows[i]["ddltaxdca"].ToString() != "Select" && Objdt.Rows[i]["ddltaxdca"].ToString() != "" && Objdt.Rows[i]["ddltaxsdca"].ToString() != "Select" && Objdt.Rows[i]["ddltaxsdca"].ToString() != "" && Objdt.Rows[i]["ddltaxnos"].ToString() != "Select" && Objdt.Rows[i]["ddltaxnos"].ToString() != "")
                    {


                        DropDownList ddltype = (DropDownList)e.Row.FindControl("ddltype");
                        ddltype.Items.Insert(0, new ListItem("Select", "Select"));
                        ddltype.Items.Insert(1, new ListItem("Creditable", "Creditable"));
                        ddltype.Items.Insert(2, new ListItem("Non-Creditable", "Non-Creditable"));
                        ddltype.SelectedValue = Objdt.Rows[i]["ddltype"].ToString();
                        //ddltype.SelectedItem.Text = Objdt.Rows[i]["ddltype"].ToString();
                        DropDownList ddltaxdca = (DropDownList)e.Row.FindControl("ddltaxdca");
                        TextBox txttaxamount = (TextBox)e.Row.FindControl("txttaxamount");
                        string selectedtype = Objdt.Rows[i]["ddltype"].ToString();
                        da = new SqlDataAdapter("TaxDcasNew_sp", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = selectedtype;
                        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtindt.Text;
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = txtindt.Text;
                        da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Tax";
                        ds = new DataSet();
                        da.Fill(ds, "taxdcas");
                        if (ds.Tables["taxdcas"].Rows.Count > 0)
                        {
                            ddltaxdca.Enabled = true;
                            ddltaxdca.Items.Clear();
                            ddltaxdca.DataSource = ds.Tables["taxdcas"];
                            ddltaxdca.DataTextField = "mapdca_code";
                            ddltaxdca.DataValueField = "dca_code";
                            ddltaxdca.DataBind();
                            ddltaxdca.Items.Insert(0, new ListItem("Select"));
                            ddltaxdca.SelectedValue = Objdt.Rows[i]["ddltaxdcacode"].ToString();
                        }
                        DropDownList ddltaxSubdca = (DropDownList)e.Row.FindControl("ddltaxsdca");
                        da = new SqlDataAdapter("select subdca_code,(mapsubdca_code+' , '+subdca_name)as mapsubdca_code from subdca where dca_code='" + Objdt.Rows[i]["ddltaxdcacode"].ToString() + "'", con);
                        ds = new DataSet();
                        da.Fill(ds, "Taxsdcas");
                        ddltaxSubdca.DataSource = ds.Tables["Taxsdcas"];
                        ddltaxSubdca.DataTextField = "mapsubdca_code";
                        ddltaxSubdca.DataValueField = "subdca_code";
                        ddltaxSubdca.DataBind();
                        ddltaxSubdca.Items.Insert(0, new ListItem("Select"));
                        ddltaxSubdca.SelectedValue = Objdt.Rows[i]["ddltaxSubdcacode"].ToString();
                        DropDownList ddltaxnos = (DropDownList)e.Row.FindControl("ddltaxnos");
                        //da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + Objdt.Rows[i]["ddltaxdcacode"].ToString() + "' and STATUS='Active'", con);
                        if (ddltaxdca.SelectedValue != "DCA-GST-NON-CR" && ddltaxdca.SelectedValue != "DCA-GST-CR" && ddltaxdca.SelectedValue != "DCA-TCS")
                            da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + ddltaxdca.SelectedValue + "' and STATUS='Active'", con);
                        else if (ddltaxdca.SelectedValue == "DCA-TCS")
                            da = new SqlDataAdapter("select coalesce(panno,'No Pan')as Tax_nos from client where status = '2' and client_id = '" + lblClientid.Text + "'", con);
                        else
                            if (lblCCCode.Text == "CCC")
                                da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3'", con);
                            else
                                da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='" + lblCCCode.Text + "' and gm.Status='3' UNION select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3' union select gst_no as tax_nos from GSTmaster where State_Id='12' and Status='3' ORDER by gst_no", con);

                        ds.Clear();
                        ds = new DataSet();
                        da.Fill(ds, "taxvatdca");
                        ddltaxnos.Items.Clear();
                        ddltaxnos.DataSource = ds.Tables["taxvatdca"];
                        ddltaxnos.DataTextField = "Tax_nos";
                        ddltaxnos.DataValueField = "Tax_nos";
                        ddltaxnos.DataBind();
                        ddltaxnos.Items.Insert(0, new ListItem("Select"));
                        ddltaxnos.SelectedValue = Objdt.Rows[i]["ddltaxnumbers"].ToString();
                        txttaxamount.Text = Objdt.Rows[i]["txttaxamount"].ToString();

                    }
                }
                i = i + 1;

            }

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    #endregion taxends

    #region cessstarts
    public void loadcesstype()
    {
        foreach (GridViewRow record in gvcess.Rows)
        {
            DropDownList type = (DropDownList)record.FindControl("ddltypecess");
            if (type.SelectedValue == "")
            {
                type.Items.Insert(0, new ListItem("Select", "Select"));
                type.Items.Insert(1, new ListItem("Creditable", "Creditable"));
                type.Items.Insert(2, new ListItem("Non-Creditable", "Non-Creditable"));
                type.DataBind();
            }
        }
    }
    protected void BindcessGridview()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("S.No", typeof(int));
        dt.Columns.Add("chkSelectcess", typeof(string));
        dt.Columns.Add("ddltypecess", typeof(string));
        dt.Columns.Add("ddlcessdca", typeof(string));
        dt.Columns.Add("ddlcesssdca", typeof(string));
        dt.Columns.Add("ddlcessnos", typeof(string));
        dt.Columns.Add("txtcessamount", typeof(string));
        dt.Columns.Add("ddlcessdcacode", typeof(string));
        dt.Columns.Add("ddlcessSubdcacode", typeof(string));
        dt.Columns.Add("ddlcessnumbers", typeof(string));
        DataRow dr = dt.NewRow();
        //dr["rowid"] = 1;
        dr["chkSelectcess"] = string.Empty;
        dr["ddltypecess"] = "Select";
        dr["ddlcessdca"] = "Select";
        dr["ddlcesssdca"] = "Select";
        dr["ddlcessnos"] = "Select";
        dr["txtcessamount"] = string.Empty;
        dr["ddlcessdcacode"] = "Select";
        dr["ddlcessSubdcacode"] = "Select";
        dr["ddlcessnumbers"] = "Select";
        dt.Rows.Add(dr);
        ViewState["Curtblcess"] = dt;
        gvcess.DataSource = dt;
        gvcess.DataBind();
        loadcesstype();
    }
    protected void btnaddcess_Click(object sender, EventArgs e)
    {

        AddNewcessRow();

    }
    private void AddNewcessRow()
    {
        int rowIndex = 0;
        if (ViewState["Curtblcess"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtblcess"];
            DataRow drCurrentRow = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    DropDownList ddlcesstype = (DropDownList)gvcess.Rows[rowIndex].Cells[2].FindControl("ddltypecess");
                    DropDownList ddlcessdca = (DropDownList)gvcess.Rows[rowIndex].Cells[3].FindControl("ddlcessdca");
                    DropDownList ddlcesssdca = (DropDownList)gvcess.Rows[rowIndex].Cells[4].FindControl("ddlcesssdca");
                    DropDownList ddlcessnos = (DropDownList)gvcess.Rows[rowIndex].Cells[5].FindControl("ddlcessnos");
                    TextBox txttaxamount = (TextBox)gvcess.Rows[rowIndex].Cells[6].FindControl("txtcessamount");
                    drCurrentRow = dt.NewRow();
                    //drCurrentRow["rowid"] = i + 1;
                    //dt.Rows[i - 1]["chkSelecttaxes"] = chk;
                    dt.Rows[i - 1]["ddltypecess"] = ddlcesstype.SelectedItem.Text;
                    dt.Rows[i - 1]["ddlcessdca"] = ddlcessdca.SelectedItem.Text;
                    dt.Rows[i - 1]["ddlcesssdca"] = ddlcesssdca.SelectedItem.Text;
                    dt.Rows[i - 1]["ddlcessnos"] = ddlcessnos.SelectedItem.Text;
                    dt.Rows[i - 1]["txtcessamount"] = txttaxamount.Text;
                    dt.Rows[i - 1]["ddlcessdcacode"] = ddlcessdca.SelectedValue;
                    dt.Rows[i - 1]["ddlcessSubdcacode"] = ddlcesssdca.SelectedValue;
                    dt.Rows[i - 1]["ddlcessnumbers"] = ddlcessnos.SelectedValue;
                    rowIndex++;
                }
                dt.Rows.Add(drCurrentRow);
                ViewState["Curtblcess"] = dt;
                gvcess.DataSource = dt;
                gvcess.DataBind();
            }
        }
        SetOldcessData();
    }
    private void SetOldcessData()
    {
        int rowIndex = 0;
        if (ViewState["Curtblcess"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtblcess"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gvcess.Rows[rowIndex].Cells[0].FindControl("chkSelectcess");
                    DropDownList ddltypecess = (DropDownList)gvcess.Rows[rowIndex].Cells[1].FindControl("ddltypecess");
                    DropDownList ddlcessdca = (DropDownList)gvcess.Rows[rowIndex].Cells[2].FindControl("ddlcessdca");
                    DropDownList ddlcesssdca = (DropDownList)gvcess.Rows[rowIndex].Cells[3].FindControl("ddlcesssdca");
                    DropDownList ddlcessnos = (DropDownList)gvcess.Rows[rowIndex].Cells[4].FindControl("ddlcessnos");
                    TextBox txtcessamount = (TextBox)gvcess.Rows[rowIndex].Cells[5].FindControl("txtcessamount");
                    ddltypecess.Items.Clear();
                    loadcesstype();
                    if (i < dt.Rows.Count - 1)
                    {
                        ddltypecess.SelectedItem.Text = dt.Rows[i]["ddltypecess"].ToString();
                        ddlcessdca.SelectedItem.Text = dt.Rows[i]["ddlcessdca"].ToString();
                        ddlcesssdca.SelectedItem.Text = dt.Rows[i]["ddlcesssdca"].ToString();
                        ddlcessnos.SelectedItem.Text = dt.Rows[i]["ddlcessnos"].ToString();
                        txtcessamount.Text = dt.Rows[i]["txtcessamount"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void gvcess_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblcess"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtblcess"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtblcess"] = dt;
                    gvcess.DataSource = dt;
                    gvcess.DataBind();
                    for (int i = 0; i < gvcess.Rows.Count - 1; i++)
                    {
                        gvcess.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetOldcessData();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:calculatecess()", true);
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    protected void ddltypecess_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddltypecess = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddltypecess.NamingContainer;
            DropDownList ddlcessdca = (DropDownList)currentRow.FindControl("ddlcessdca");
            DropDownList ddlcesssdca = (DropDownList)currentRow.FindControl("ddlcesssdca");
            DropDownList ddlcessnos = (DropDownList)currentRow.FindControl("ddlcessnos");
            if (ddltypecess != null && ddltypecess.SelectedIndex > 0 && ddlcessdca != null)
            {
                string selectedtype = ddltypecess.SelectedValue;
                da = new SqlDataAdapter("TaxDcasNew_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = selectedtype;
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtindt.Text;
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblCCCode.Text;
                da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Cess";
                ds = new DataSet();
                da.Fill(ds, "cessdcas");
                if (ds.Tables["cessdcas"].Rows.Count > 0)
                {
                    ddlcessdca.Enabled = true;
                    ddlcessdca.Items.Clear();
                    ddlcessdca.DataSource = ds.Tables["cessdcas"];
                    ddlcessdca.DataTextField = "mapdca_code";
                    ddlcessdca.DataValueField = "dca_code";
                    ddlcessdca.DataBind();
                    ddlcessdca.Items.Insert(0, new ListItem("Select"));
                }
                else
                {
                    ddlcessdca.DataSource = null;
                    ddlcessdca.DataBind();
                    ddlcessdca.Items.Insert(0, new ListItem("Select"));
                    ddlcessdca.Enabled = false;
                    ddlcessdca.CssClass = "class";
                }

            }
            else if (ddltypecess.SelectedIndex == 0)
            {
                ddlcessdca.SelectedItem.Text = "Select";
                ddlcesssdca.SelectedItem.Text = "Select";
                ddlcessnos.SelectedItem.Text = "Select";

            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void ddlcessdca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlcesdca = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddlcesdca.NamingContainer;
            DropDownList ddlcesssdca = (DropDownList)currentRow.FindControl("ddlcesssdca");
            DropDownList ddlcessnos = (DropDownList)currentRow.FindControl("ddlcessnos");
            if (ddlcesdca != null && ddlcesdca.SelectedIndex > 0 && ddlcesssdca != null)
            {
                da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + ddlcesdca.SelectedValue + "'", con);
                ds = new DataSet();
                da.Fill(ds, "Cesssdcas");
                ddlcesssdca.DataSource = ds.Tables["Cesssdcas"];
                ddlcesssdca.DataTextField = "mapsubdca_code";
                ddlcesssdca.DataValueField = "subdca_code";
                ddlcesssdca.DataBind();
                ddlcesssdca.Items.Insert(0, new ListItem("Select"));
                da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + ddlcesdca.SelectedValue + "' and STATUS='Active'", con);
                ds.Clear();
                ds = new DataSet();
                da.Fill(ds, "cesstaxdca");
                if (ds.Tables["cesstaxdca"].Rows.Count > 0)
                {
                    ddlcessnos.Items.Clear();
                    ddlcessnos.DataSource = ds.Tables["cesstaxdca"];
                    ddlcessnos.DataTextField = "Tax_nos";
                    ddlcessnos.DataValueField = "Tax_nos";
                    ddlcessnos.DataBind();
                    ddlcessnos.Items.Insert(0, new ListItem("Select"));
                }
                else
                {
                    ddlcessnos.Items.Clear();
                    ddlcessnos.Items.Insert(0, new ListItem("Select TaxNos"));
                }
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void btnaddcesss_Click(object sender, EventArgs e)
    {
        txtindt.Enabled = false;
        btnaddcess.Visible = false;
        btnremovecess.Visible = true;
        traddcess.Style["display"] = "block";
        BindcessGridview();

    }
    private decimal CessAmount = (decimal)0.0;
    protected void btnremovecesss_Click(object sender, EventArgs e)
    {
        btnaddcess.Visible = true;
        btnremovecess.Visible = false;
        gvcess.DataSource = null;
        gvcess.DataBind();
        txtinvvalue.Text = (Convert.ToDecimal(txtinvvalue.Text) - Convert.ToDecimal(txtcess.Text)).ToString();
        hfcesstotal.Value = Convert.ToInt16(0).ToString();
        txtcess.Text = Convert.ToInt16(0).ToString();
        txtindt.Enabled = true;
      
    }
    public int c = 0;
    protected void gvcess_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (ViewState["Curtblcess"] != null)
                {
                    DataTable Objdt = ViewState["Curtblcess"] as DataTable;
                    if (Objdt.Rows[c]["ddltypecess"].ToString() != "Select" && Objdt.Rows[c]["ddltypecess"].ToString() != "" && Objdt.Rows[c]["ddlcessdca"].ToString() != "Select" && Objdt.Rows[c]["ddlcessdca"].ToString() != "" && Objdt.Rows[c]["ddlcesssdca"].ToString() != "Select" && Objdt.Rows[c]["ddlcesssdca"].ToString() != "" && Objdt.Rows[c]["ddlcessnumbers"].ToString() != "Select" && Objdt.Rows[c]["ddlcessnumbers"].ToString() != "")
                    {
                        DropDownList ddltypecess = (DropDownList)e.Row.FindControl("ddltypecess");
                        ddltypecess.Items.Insert(0, new ListItem("Select", "Select"));
                        ddltypecess.Items.Insert(1, new ListItem("Creditable", "Creditable"));
                        ddltypecess.Items.Insert(2, new ListItem("Non-Creditable", "Non-Creditable"));
                        ddltypecess.SelectedValue = Objdt.Rows[c]["ddltypecess"].ToString();
                        ddltypecess.SelectedValue = Objdt.Rows[c]["ddltypecess"].ToString();
                        DropDownList ddlcessdca = (DropDownList)e.Row.FindControl("ddlcessdca");
                        TextBox txtcesamount = (TextBox)e.Row.FindControl("txtcessamount");
                        string selectedtype = Objdt.Rows[c]["ddltypecess"].ToString();
                        da = new SqlDataAdapter("TaxDcasNew_sp", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = selectedtype;
                        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtindt.Text;
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblCCCode.Text;
                        da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Cess";
                        ds = new DataSet();
                        da.Fill(ds, "Cessdcas");
                        if (ds.Tables["Cessdcas"].Rows.Count > 0)
                        {
                            ddlcessdca.Enabled = true;
                            ddlcessdca.Items.Clear();
                            ddlcessdca.DataSource = ds.Tables["Cessdcas"];
                            ddlcessdca.DataTextField = "mapdca_code";
                            ddlcessdca.DataValueField = "dca_code";
                            ddlcessdca.DataBind();
                            ddlcessdca.Items.Insert(0, new ListItem("Select"));
                            ddlcessdca.SelectedValue = Objdt.Rows[c]["ddlcessdcacode"].ToString();
                        }
                        DropDownList ddlcessSubdca = (DropDownList)e.Row.FindControl("ddlcesssdca");
                        da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + Objdt.Rows[c]["ddlcessdcacode"].ToString() + "'", con);
                        ds = new DataSet();
                        da.Fill(ds, "Cesssdcas");
                        ddlcessSubdca.DataSource = ds.Tables["Cesssdcas"];
                        ddlcessSubdca.DataTextField = "mapsubdca_code";
                        ddlcessSubdca.DataValueField = "subdca_code";
                        ddlcessSubdca.DataBind();
                        ddlcessSubdca.Items.Insert(0, new ListItem("Select"));
                        ddlcessSubdca.SelectedValue = Objdt.Rows[c]["ddlcessSubdcacode"].ToString();
                        DropDownList ddlcessnos = (DropDownList)e.Row.FindControl("ddlcessnos");
                        da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + Objdt.Rows[c]["ddlcessdcacode"].ToString() + "' and STATUS='Active'", con);
                        ds.Clear();
                        ds = new DataSet();
                        da.Fill(ds, "CessTaxdca");
                        ddlcessnos.Items.Clear();
                        ddlcessnos.DataSource = ds.Tables["CessTaxdca"];
                        ddlcessnos.DataTextField = "Tax_nos";
                        ddlcessnos.DataValueField = "Tax_nos";
                        ddlcessnos.DataBind();
                        ddlcessnos.Items.Insert(0, new ListItem("Select"));
                        ddlcessnos.SelectedValue = Objdt.Rows[c]["ddlcessnumbers"].ToString();
                        txtcesamount.Text = Objdt.Rows[c]["txtcessamount"].ToString();
                    }
                }
                c = c + 1;

            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    #endregion cessends
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlstatus.SelectedItem.Text == "Approve")
            {

                string taxtypes = "";
                string taxdcas = "";
                string taxsdcas = "";
                string taxnos = "";
                string taxamounts = "";
                string taxmapdcacodes = "";

                string cesstypes = "";
                string cessdcas = "";
                string cesssdcas = "";
                string cessnos = "";
                string cessamounts = "";

                foreach (GridViewRow record in gvtaxes.Rows)
                {
                    if ((record.FindControl("chkSelecttaxes") as CheckBox).Checked)
                    {
                        taxtypes = taxtypes + (record.FindControl("ddltype") as DropDownList).SelectedValue + ",";
                        taxdcas = taxdcas + (record.FindControl("ddltaxdca") as DropDownList).SelectedValue + ",";
                        taxmapdcacodes = taxmapdcacodes + (record.FindControl("ddltaxdca") as DropDownList).SelectedItem.Text + ",";
                        taxsdcas = taxsdcas + (record.FindControl("ddltaxsdca") as DropDownList).SelectedValue + ",";
                        taxnos = taxnos + (record.FindControl("ddltaxnos") as DropDownList).SelectedValue + ",";
                        taxamounts = taxamounts + (record.FindControl("txttaxamount") as TextBox).Text + ",";
                    }
                }
                foreach (GridViewRow record in gvcess.Rows)
                {
                    if ((record.FindControl("chkSelectcess") as CheckBox).Checked)
                    {
                        cesstypes = cesstypes + (record.FindControl("ddltypecess") as DropDownList).SelectedValue + ",";
                        cessdcas = cessdcas + (record.FindControl("ddlcessdca") as DropDownList).SelectedValue + ",";
                        cesssdcas = cesssdcas + (record.FindControl("ddlcesssdca") as DropDownList).SelectedValue + ",";
                        cessnos = cessnos + (record.FindControl("ddlcessnos") as DropDownList).SelectedValue + ",";
                        cessamounts = cessamounts + (record.FindControl("txtcessamount") as TextBox).Text + ",";
                    }
                }

                cmd.CommandText = "sp_ClientInvoice_Verification";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@PO_NO", lblpono.Text);
                cmd.Parameters.AddWithValue("@RA_NO", txtra.Text);
                cmd.Parameters.AddWithValue("@CCCode", lblCCCode.Text);
                cmd.Parameters.AddWithValue("@InvoiceNo", lblInvNo.Text);
                cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);

                cmd.Parameters.AddWithValue("@taxtypes", taxtypes);
                cmd.Parameters.AddWithValue("@taxdcas", taxdcas);
                cmd.Parameters.AddWithValue("@taxmapdcas", taxmapdcacodes);
                cmd.Parameters.AddWithValue("@taxsdcas", taxsdcas);
                cmd.Parameters.AddWithValue("@taxnos", taxnos);
                cmd.Parameters.AddWithValue("@taxamounts", taxamounts);

                cmd.Parameters.AddWithValue("@cesstypes", cesstypes);
                cmd.Parameters.AddWithValue("@cessdcas", cessdcas);
                cmd.Parameters.AddWithValue("@cesssdcas", cesssdcas);
                cmd.Parameters.AddWithValue("@cessnos", cessnos);
                cmd.Parameters.AddWithValue("@cessamounts", cessamounts);


                cmd.Parameters.AddWithValue("@InvTotal", txtinvvalue.Text);
                cmd.Parameters.AddWithValue("@Description", txtdescription.Text);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@InvoiceType", lblInvoiceCategory.Text);
                cmd.Parameters.AddWithValue("@Invoicesubtype", lblInvoicetype.Text);
                cmd.Parameters.AddWithValue("@ClientId", lblClientid.Text);
                cmd.Parameters.AddWithValue("@SubClientId", lblsubclientid.Text);
                cmd.Parameters.AddWithValue("@InvcoiceID", ViewState["Invoiceid"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                // string msg = "invalid";
                con.Close();
                if (msg == "Invoice Approved Sucessfully")
                {
                    JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                }
                else
                {
                    JavaScript.UPAlert(Page, msg);
                }
            }
            else
            {
                cmd = new SqlCommand("sp_ClientInvoice_Reject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceId", ViewState["Invoiceid"].ToString());
                cmd.Connection = con;
                con.Open();
                int Count = cmd.ExecuteNonQuery();
                con.Close();
                if (Count > 0)
                {
                    JavaScript.UPAlertRedirect(Page, "Rejected Sucessfully", "VerifyClientInvoice.aspx");
                }
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Reject Failed", "VerifyClientInvoice.aspx");
                }
            }
        }

        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {

    }
    protected void GvdInvoices_RowEditing(object sender, GridViewEditEventArgs e)
    {
        tblinv.Visible = true;
        tbldetails.Visible = true;
        tblview.Visible = false;
        FillInvoiceInfo(GvdInvoices.DataKeys[e.NewEditIndex]["Invoice_Id"].ToString());
        ViewState["Invoiceid"] = GvdInvoices.DataKeys[e.NewEditIndex]["Invoice_Id"].ToString();
    }
    public void FillInvoiceInfo(string InvoiceId)
    {
        try
        {
             da = new SqlDataAdapter("GetCustomerInvoice", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Flag", "Get Seleted Invoice");
            da.SelectCommand.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            DataSet Objds = new DataSet();
            da.Fill(Objds, "InvoiceInfo");
            if (Objds.Tables["InvoiceInfo"].Rows.Count > 0)
            {
                lblpono.Text = Objds.Tables["InvoiceInfo"].Rows[0]["PO_NO"].ToString();
                lblInvNo.Text = Objds.Tables["InvoiceInfo"].Rows[0]["InvoiceNo"].ToString();
                txtindt.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Invoice_Date"].ToString();
                txtindtmk.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Inv_Making_Date"].ToString();
                lblCCCode.Text = Objds.Tables["InvoiceInfo"].Rows[0]["CC_Code"].ToString();
                lblClientid.Text = Objds.Tables["InvoiceInfo"].Rows[0]["client_id"].ToString();
                lblsubclientid.Text = Objds.Tables["InvoiceInfo"].Rows[0]["subclient_id"].ToString();
                txtra.Text = Objds.Tables["InvoiceInfo"].Rows[0]["RA_NO"].ToString();
                txtbasic.Text = Objds.Tables["InvoiceInfo"].Rows[0]["BasicValue"].ToString();
                txtinvvalue.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Total"].ToString();
                lblInvoiceCategory.Text = Objds.Tables["InvoiceInfo"].Rows[0]["PaymentType"].ToString();
                lblInvoicetype.Text = Objds.Tables["InvoiceInfo"].Rows[0]["InvoiceType"].ToString();
                txtdescription.Text = Objds.Tables["InvoiceInfo"].Rows[0]["comments"].ToString();

                if (Objds.Tables["InvoiceInfo1"].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("S.No", typeof(int));
                    dt.Columns.Add("chkSelecttaxes", typeof(string));
                    dt.Columns.Add("ddltype", typeof(string));
                    dt.Columns.Add("ddltaxdca", typeof(string));
                    dt.Columns.Add("ddltaxsdca", typeof(string));
                    dt.Columns.Add("ddltaxnos", typeof(string));
                    dt.Columns.Add("txttaxamount", typeof(string));
                    dt.Columns.Add("ddltaxdcacode", typeof(string));
                    dt.Columns.Add("ddltaxSubdcacode", typeof(string));
                    dt.Columns.Add("ddltaxnumbers", typeof(string));
                    for (int i = 0; i < Objds.Tables["InvoiceInfo1"].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        //dr["rowid"] = 1;
                        dr["chkSelecttaxes"] = string.Empty;
                        dr["ddltype"] = Objds.Tables["InvoiceInfo1"].Rows[i]["ISCreditableTax"].ToString();
                        dr["ddltaxdca"] = Objds.Tables["InvoiceInfo1"].Rows[i]["DCA_Code"].ToString();
                        dr["ddltaxsdca"] = Objds.Tables["InvoiceInfo1"].Rows[i]["SubDCA_Code"].ToString();
                        dr["ddltaxnos"] = Objds.Tables["InvoiceInfo1"].Rows[i]["TaxNo"].ToString();
                        dr["txttaxamount"] = Objds.Tables["InvoiceInfo1"].Rows[i]["TaxValue"].ToString();
                        dr["ddltaxdcacode"] = Objds.Tables["InvoiceInfo1"].Rows[i]["DCA_Code"].ToString();
                        dr["ddltaxSubdcacode"] = Objds.Tables["InvoiceInfo1"].Rows[i]["SubDCA_Code"].ToString();
                        dr["ddltaxnumbers"] = Objds.Tables["InvoiceInfo1"].Rows[i]["TaxNo"].ToString();
                        dt.Rows.Add(dr);
                    }
                    ViewState["Curtbl"] = dt;
                    gvtaxes.DataSource = dt;
                    gvtaxes.DataBind();
                    loadtaxtype();
                    traddtaxes.Style["display"] = "block";
                    btnaddtax.Visible = false;
                    btnremovetax.Visible = true;
                    lbltaxes.Text = Objds.Tables["InvoiceInfo3"].Rows[0][0].ToString();
                }
                else
                {
                    btnaddtax.Visible = true;
                    btnremovetax.Visible = false;
                }
                if (Objds.Tables["InvoiceInfo2"].Rows.Count > 0)
                {
                    DataTable dtcess = new DataTable();
                    dtcess.Columns.Add("S.No", typeof(int));
                    dtcess.Columns.Add("chkSelectcess", typeof(string));
                    dtcess.Columns.Add("ddltypecess", typeof(string));
                    dtcess.Columns.Add("ddlcessdca", typeof(string));
                    dtcess.Columns.Add("ddlcesssdca", typeof(string));
                    dtcess.Columns.Add("ddlcessnos", typeof(string));
                    dtcess.Columns.Add("txtcessamount", typeof(string));
                    dtcess.Columns.Add("ddlcessdcacode", typeof(string));
                    dtcess.Columns.Add("ddlcessSubdcacode", typeof(string));
                    dtcess.Columns.Add("ddlcessnumbers", typeof(string));
                    for (int i = 0; i < Objds.Tables["InvoiceInfo2"].Rows.Count; i++)
                    {
                        DataRow dr = dtcess.NewRow();
                        dr["chkSelectcess"] = string.Empty;
                        dr["ddltypecess"] = Objds.Tables["InvoiceInfo2"].Rows[i]["ISCreditableTax"].ToString();
                        dr["ddlcessdca"] = Objds.Tables["InvoiceInfo2"].Rows[i]["DCA_Code"].ToString();
                        dr["ddlcesssdca"] = Objds.Tables["InvoiceInfo2"].Rows[i]["SubDCA_Code"].ToString();
                        dr["ddlcessnos"] = Objds.Tables["InvoiceInfo2"].Rows[i]["TaxNo"].ToString();
                        dr["txtcessamount"] = Objds.Tables["InvoiceInfo2"].Rows[i]["TaxValue"].ToString();
                        dr["ddlcessdcacode"] = Objds.Tables["InvoiceInfo2"].Rows[i]["DCA_Code"].ToString();
                        dr["ddlcessSubdcacode"] = Objds.Tables["InvoiceInfo2"].Rows[i]["SubDCA_Code"].ToString();
                        dr["ddlcessnumbers"] = Objds.Tables["InvoiceInfo2"].Rows[i]["TaxNo"].ToString();
                        dtcess.Rows.Add(dr);
                    }
                    ViewState["Curtblcess"] = dtcess;
                    gvcess.DataSource = dtcess;
                    gvcess.DataBind();
                    loadcesstype();
                    traddcess.Style["display"] = "block";
                    btnaddcess.Visible = false;
                    txtcess.Text = Objds.Tables["InvoiceInfo4"].Rows[0][0].ToString();
                }
                else
                {
                    btnaddcess.Visible = true;
                    btnremovecess.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { 
        
        }
    }


    protected void GvdInvoices_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblinv.Visible = false;
        tbldetails.Visible = false;
        tblview.Visible = true;
        ViewInvoiceInfo(GvdInvoices.SelectedValue.ToString());
        ViewState["Invoiceid"] = GvdInvoices.SelectedValue.ToString();
    }
    public void ViewInvoiceInfo(string InvoiceId)
    {
        try
        {
            da = new SqlDataAdapter("GetCustomerInvoice", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Flag", "Get Seleted Invoice");
            da.SelectCommand.Parameters.AddWithValue("@InvoiceId", InvoiceId);
            DataSet Objds = new DataSet();
            da.Fill(Objds, "InvoiceInfo");
            if (Objds.Tables["InvoiceInfo"].Rows.Count > 0)
            {
                lblviewpono.Text = Objds.Tables["InvoiceInfo"].Rows[0]["PO_NO"].ToString();
                lblviewinvoiceno.Text = Objds.Tables["InvoiceInfo"].Rows[0]["InvoiceNo"].ToString();
                lvlinvdate.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Invoice_Date"].ToString();
                lblinvmdate.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Inv_Making_Date"].ToString();
                lblviewcccode.Text = Objds.Tables["InvoiceInfo"].Rows[0]["CC_Code"].ToString();
                lblviewclientid.Text = Objds.Tables["InvoiceInfo"].Rows[0]["client_id"].ToString();
                lblviewsubclientid.Text = Objds.Tables["InvoiceInfo"].Rows[0]["subclient_id"].ToString();
                lblviewrano.Text = Objds.Tables["InvoiceInfo"].Rows[0]["RA_NO"].ToString();
                lblbasic.Text = Objds.Tables["InvoiceInfo"].Rows[0]["BasicValue"].ToString();
                lblinvoicevalue.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Total"].ToString();
                lblviewinvoicecategory.Text = Objds.Tables["InvoiceInfo"].Rows[0]["PaymentType"].ToString();
                lblviewinvoicetype.Text = Objds.Tables["InvoiceInfo"].Rows[0]["InvoiceType"].ToString();
                txtviewdescriptrion.Text = Objds.Tables["InvoiceInfo"].Rows[0]["comments"].ToString();

                if (Objds.Tables["InvoiceInfo1"].Rows.Count > 0)
                {
                    GridView1.DataSource = Objds.Tables["InvoiceInfo1"];
                    GridView1.DataBind();
                    trviewtax.Style["display"] = "block";
                    txttaxtotalview.Text = Objds.Tables["InvoiceInfo3"].Rows[0]["TaxValue"].ToString();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    txttaxtotalview.Text = "0";
                    trviewtax.Style["display"] = "none";
                }

                if (Objds.Tables["InvoiceInfo2"].Rows.Count > 0)
                {
                    GridView2.DataSource = Objds.Tables["InvoiceInfo2"];
                    GridView2.DataBind();
                    trviewcess.Style["display"] = "block";
                    txtcesstotalview.Text = Objds.Tables["InvoiceInfo4"].Rows[0]["TaxValue"].ToString();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                    trviewcess.Style["display"] = "none";
                    txtcesstotalview.Text = "0";
                    
                }
              
            }
        }
        catch (Exception ex)
        {

        }
    }

}