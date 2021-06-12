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

public partial class frmpendinginvoicenew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataAdapter da = null;
    SqlCommand cmd1 = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        if (!IsPostBack)
        {
            btnremovetax.Visible = false;
            btnremovecess.Visible = false;
            laodvendor();
            tbldetails.Visible = false;
            trothergrid.Style["display"] = "none";
            trdeductiongrid.Style["display"] = "none";
           //ViewState["rowcount"] = 1;
            //traddtaxes.visible
        }

    }
    public void laodvendor()
    {
        //da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='Service Provider' and status='2' order by name asc", con);
        da = new SqlDataAdapter("Select DISTINCT v.vendor_id,v.vendor_name+' ('+v.vendor_id+')' Name from vendor v JOIN sppo sp ON v.vendor_id=sp.vendor_id where v.vendor_type='Service Provider' and v.status='2' and sp.STATUS='3' order by name asc", con);
        da.Fill(ds, "vendor");
        ddlvendor.DataTextField = "Name";
        ddlvendor.DataValueField = "vendor_id";
        ddlvendor.DataSource = ds.Tables["vendor"];
        ddlvendor.DataBind();
        ddlvendor.Items.Insert(0, "Select Vendor");
    }
    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tbldetails.Visible = false;
            da = new SqlDataAdapter("Select distinct pono from SPPO where status='3'  and vendor_id='" + ddlvendor.SelectedValue + "'", con);
            da.Fill(ds, "fill");
            ddlpo.DataTextField = "pono";
            ddlpo.DataValueField = "pono";
            ddlpo.DataSource = ds.Tables["fill"];
            ddlpo.DataBind();
            ddlpo.Items.Insert(0, "Select PO");
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    protected void ddlpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select s.cc_code,dca_code,subdca_code,vendor_name v,s.Balance,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date, c.cc_type, c.CC_SubType from   sppo s join  vendor v on s.vendor_id=v.vendor_id join Cost_Center c on s.cc_code=c.cc_code  where s.pono='" + ddlpo.SelectedItem.Text + "'", con);
        da.Fill(ds, "pono");
        if (ds.Tables["pono"].Rows.Count > 0)
        {
            clear();
            tbldetails.Visible = true;           
            lblccode.Text = ds.Tables["pono"].Rows[0][0].ToString();
            lbldcacode.Text = ds.Tables["pono"].Rows[0][1].ToString();
            lblsdcacode.Text = ds.Tables["pono"].Rows[0][2].ToString();
        }
        else
        {
            clear();
            tbldetails.Visible = false;
            lblccode.Text = "";
            lbldcacode.Text = "";
            lblsdcacode.Text = "";
        }

    }

    #region taxstarts
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
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
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
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblccode.Text;
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
                    ViewState["Tax"]= ddltaxdca.SelectedValue;
                }
                else
                {
                    ddltaxdca.DataSource = null;
                    ddltaxdca.DataBind();
                    ddltaxdca.SelectedIndex = 0;
                    ddltaxsdca.SelectedIndex = 0;
                    ddltaxnos.SelectedIndex = 0;
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
                da = new SqlDataAdapter("select subdca_code,(mapsubdca_code+' , '+subdca_name)as mapsubdca_code from subdca where dca_code='" + ddltaxdca.SelectedValue + "'", con);
                ds = new DataSet();
                da.Fill(ds, "Taxsdcas");
                ddltaxsdca.DataSource = ds.Tables["Taxsdcas"];
                ddltaxsdca.DataTextField = "mapsubdca_code";
                ddltaxsdca.DataValueField = "subdca_code";
                ddltaxsdca.DataBind();
                ddltaxsdca.Items.Insert(0, new ListItem("Select"));
                if (ddltaxdca.SelectedValue != "DCA-GST-NON-CR" && ddltaxdca.SelectedValue != "DCA-GST-CR")
                    da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + ddltaxdca.SelectedValue + "' and STATUS='Active'", con);
                else
                    if (lblccode.Text == "CCC")
                        da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3'", con);
                    else

                        da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='" + lblccode.Text + "' and gm.Status='3' UNION select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3' ORDER by gst_no", con);

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
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblccode.Text;
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
                        if (ddltaxdca.SelectedValue != "DCA-GST-NON-CR" && ddltaxdca.SelectedValue != "DCA-GST-CR")
                            da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + ddltaxdca.SelectedValue + "' and STATUS='Active'", con);
                        else
                            if (lblccode.Text=="CCC")
                                da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3'", con);
                            else
                                da = new SqlDataAdapter("select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='" + lblccode.Text + "' and gm.Status='3' UNION select DISTINCT gst_no as Tax_nos from GSTmaster gm JOIN Cost_Center cc on gm.State_Id=cc.State_Id join TaxDca td on td.Tax_Nos=gm.GST_No where td.Dca_Code='" + ddltaxdca.SelectedValue + "' and td.Status='Active' and cc.cc_code='CCC' and gm.Status='3' ORDER by gst_no", con);

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
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblccode.Text;
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
                    ddlcessdca.SelectedIndex = 0;
                    ddlcesssdca.SelectedIndex = 0;
                    ddlcessnos.SelectedIndex = 0;
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
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblccode.Text;
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

    #region OTHERS START
    protected void rbtnothercharges_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbtnothercharges.SelectedIndex == 0)
        {
            if (txtindt.Text != "")
            {
                txtindt.Enabled = false;
                trothergrid.Style["display"] = "block";
                hfother.Value = Convert.ToInt16(0).ToString();
                txtother.Text = Convert.ToInt16(0).ToString();
                BindotherGridview();
                otherdcalist();
            }
        }
        else
        {
            gvother.DataSource = null;
            gvother.DataBind();
            txtindt.Enabled = true;
            trothergrid.Style["display"] = "none";
            hfother.Value = Convert.ToInt16(0).ToString();
            txtother.Text = Convert.ToInt16(0).ToString();
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        }
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:CalculateCMC_SA()", true);
    }
    public void loaddca()
    {
        foreach (GridViewRow record in gvother.Rows)
        {
            DropDownList other = (DropDownList)record.FindControl("ddlotherdca");
            if (other.SelectedValue == "")
            {
                other.Items.Insert(0, new ListItem("Select", "Select"));                
                other.DataBind();
            }
        }
    }
    protected void BindotherGridview()
    {
        DataTable dtt = new DataTable();
        dtt.Columns.Add("S.No", typeof(int));
        dtt.Columns.Add("chkother", typeof(string));
        dtt.Columns.Add("ddlotherdca", typeof(string));
        dtt.Columns.Add("ddlothersdca", typeof(string));
        dtt.Columns.Add("txtotheramount", typeof(string));
        dtt.Columns.Add("ddlotherdcacode", typeof(string));
        dtt.Columns.Add("ddlotherSubdcacode", typeof(string));        
        DataRow dr = dtt.NewRow();
        //dr["rowid"] = 1;
        dr["chkother"] = string.Empty;       
        dr["ddlotherdca"] = "Select";
        dr["ddlothersdca"] = "Select";
        dr["txtotheramount"] = string.Empty;
        dr["ddlotherdcacode"] = "Select";
        dr["ddlotherSubdcacode"] = "Select";
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
                    DropDownList ddlotherdca = (DropDownList)gvother.Rows[rowIndex].Cells[1].FindControl("ddlotherdca");
                    DropDownList ddlothersdca = (DropDownList)gvother.Rows[rowIndex].Cells[2].FindControl("ddlothersdca");                   
                    TextBox txtotheramount = (TextBox)gvother.Rows[rowIndex].Cells[3].FindControl("txtotheramount");
                    drCurrentRow = dt.NewRow();                  
                    dt.Rows[i - 1]["ddlotherdca"] = ddlotherdca.SelectedItem.Text;
                    dt.Rows[i - 1]["ddlothersdca"] = ddlothersdca.SelectedItem.Text;                   
                    dt.Rows[i - 1]["txtotheramount"] = txtotheramount.Text;
                    dt.Rows[i - 1]["ddlotherdcacode"] = ddlotherdca.SelectedValue;                   
                    dt.Rows[i - 1]["ddlotherSubdcacode"] = ddlothersdca.SelectedValue;
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
                    CheckBox chk = (CheckBox)gvother.Rows[rowIndex].Cells[0].FindControl("chkother");
                    DropDownList ddlotherdca = (DropDownList)gvother.Rows[rowIndex].Cells[1].FindControl("ddlotherdca");
                    DropDownList ddlothersdca = (DropDownList)gvother.Rows[rowIndex].Cells[2].FindControl("ddlothersdca");
                    TextBox txtotheramount = (TextBox)gvother.Rows[rowIndex].Cells[3].FindControl("txtotheramount");
                  
                    if (i < dt.Rows.Count - 1)
                    {
                        ddlotherdca.SelectedItem.Text = dt.Rows[i]["ddlotherdca"].ToString();
                        ddlothersdca.SelectedItem.Text = dt.Rows[i]["ddlothersdca"].ToString();
                        txtotheramount.Text = dt.Rows[i]["txtotheramount"].ToString();
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
           ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:calculateother()", true);
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
                DropDownList ddlotherdca = (DropDownList)other.FindControl("ddlotherdca");
                da = new SqlDataAdapter("TaxDcasNew_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtindt.Text;
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblccode.Text;
                da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Others";
                ds = new DataSet();
                da.Fill(ds, "otherdcas");
                if (ds.Tables["otherdcas"].Rows.Count > 0)
                {
                    ddlotherdca.Enabled = true;
                    ddlotherdca.Items.Clear();
                    ddlotherdca.DataSource = ds.Tables["otherdcas"];
                    ddlotherdca.DataTextField = "mapdca_code";
                    ddlotherdca.DataValueField = "dca_code";
                    ddlotherdca.DataBind();
                    ddlotherdca.Items.Insert(0, new ListItem("Select"));
                }
                else
                {
                    ddlotherdca.DataSource = null;
                    ddlotherdca.DataBind();
                    ddlotherdca.Items.Insert(0, new ListItem("Select"));
                    ddlotherdca.Enabled = false;
                    ddlotherdca.CssClass = "class";
                }         
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void ddlotherdca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlotherdca = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddlotherdca.NamingContainer;
            DropDownList ddlothersdca = (DropDownList)currentRow.FindControl("ddlothersdca");           
            if (ddlotherdca != null && ddlotherdca.SelectedIndex > 0 && ddlothersdca != null)
            {
                da = new SqlDataAdapter("select (mapsubdca_code+' , '+subdca_name)as mapsubdca_code,subdca_code from subdca where dca_code='" + ddlotherdca.SelectedValue + "'", con);
                ds = new DataSet();
                da.Fill(ds, "Othersdcas");
                ddlothersdca.DataSource = ds.Tables["Othersdcas"];
                ddlothersdca.DataTextField = "mapsubdca_code";
                ddlothersdca.DataValueField = "subdca_code";
                ddlothersdca.DataBind();
                ddlothersdca.Items.Insert(0, new ListItem("Select"));                
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
                    if (Objdt.Rows[ot]["ddlotherdca"].ToString() != "Select" && Objdt.Rows[ot]["ddlotherdca"].ToString() != "" && Objdt.Rows[ot]["ddlothersdca"].ToString() != "Select" && Objdt.Rows[ot]["ddlothersdca"].ToString() != "")
                    {
                        DropDownList ddlotherdca = (DropDownList)e.Row.FindControl("ddlotherdca");
                        TextBox txtotheramount = (TextBox)e.Row.FindControl("txtotheramount");                      
                        da = new SqlDataAdapter("TaxDcasNew_sp", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;                       
                        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtindt.Text;
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblccode.Text;
                        da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Others";
                        ds = new DataSet();
                        da.Fill(ds, "otherdcas");
                        if (ds.Tables["otherdcas"].Rows.Count > 0)
                        {
                            ddlotherdca.Enabled = true;
                            ddlotherdca.Items.Clear();
                            ddlotherdca.DataSource = ds.Tables["otherdcas"];
                            ddlotherdca.DataTextField = "mapdca_code";
                            ddlotherdca.DataValueField = "dca_code";
                            ddlotherdca.DataBind();
                            ddlotherdca.Items.Insert(0, new ListItem("Select"));
                            ddlotherdca.SelectedValue = Objdt.Rows[ot]["ddlotherdcacode"].ToString();
                        }
                        DropDownList ddlotherSubdca = (DropDownList)e.Row.FindControl("ddlothersdca");
                        da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + Objdt.Rows[ot]["ddlotherdcacode"].ToString() + "'", con);
                        ds = new DataSet();
                        da.Fill(ds, "othersdcas");
                        ddlotherSubdca.DataSource = ds.Tables["othersdcas"];
                        ddlotherSubdca.DataTextField = "mapsubdca_code";
                        ddlotherSubdca.DataValueField = "subdca_code";
                        ddlotherSubdca.DataBind();
                        ddlotherSubdca.Items.Insert(0, new ListItem("Select"));
                        ddlotherSubdca.SelectedValue = Objdt.Rows[ot]["ddlotherSubdcacode"].ToString();
                        txtotheramount.Text = Objdt.Rows[ot]["txtotheramount"].ToString();
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
    #endregion OTHERS END

    #region Deduction START
    protected void rbtndeductioncharges_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbtndeductioncharges.SelectedIndex == 0)
        {
            if (txtindt.Text != "")
            {
                txtindt.Enabled = false;
                trdeductiongrid.Style["display"] = "block";
                hfdeduction.Value = Convert.ToInt16(0).ToString();
                txtdeduction.Text = Convert.ToInt16(0).ToString();
                BinddeductionGridview();
                deductiondcalist();
            }
        }
        else
        {
            gvdeduction.DataSource = null;
            gvdeduction.DataBind();
            txtindt.Enabled = true;
            trdeductiongrid.Style["display"] = "none";
            hfdeduction.Value = Convert.ToInt16(0).ToString();
            txtdeduction.Text = Convert.ToInt16(0).ToString();
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
        dtt.Columns.Add("ddldeductiondca", typeof(string));
        dtt.Columns.Add("ddldeductionsdca", typeof(string));
        dtt.Columns.Add("txtdeductionamount", typeof(string));
        dtt.Columns.Add("ddldeductiondcacode", typeof(string));
        dtt.Columns.Add("ddldeductionSubdcacode", typeof(string));
        DataRow dr = dtt.NewRow();
        //dr["rowid"] = 1;
        dr["chkdeduction"] = string.Empty;
        dr["ddldeductiondca"] = "Select";
        dr["ddldeductionsdca"] = "Select";
        dr["txtdeductionamount"] = string.Empty;
        dr["ddldeductiondcacode"] = "Select";
        dr["ddldeductionSubdcacode"] = "Select";
        dtt.Rows.Add(dr);
        ViewState["Curtbldeduction"] = dtt;
        gvdeduction.DataSource = dtt;
        gvdeduction.DataBind();
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
                    DropDownList ddldeductiondca = (DropDownList)gvdeduction.Rows[rowIndex].Cells[1].FindControl("ddldeductiondca");
                    DropDownList ddldeductionsdca = (DropDownList)gvdeduction.Rows[rowIndex].Cells[2].FindControl("ddldeductionsdca");
                    TextBox txtdeductionamount = (TextBox)gvdeduction.Rows[rowIndex].Cells[3].FindControl("txtdeductionamount");
                    drCurrentRow = dt.NewRow();
                    dt.Rows[i - 1]["ddldeductiondca"] = ddldeductiondca.SelectedItem.Text;
                    dt.Rows[i - 1]["ddldeductionsdca"] = ddldeductionsdca.SelectedItem.Text;
                    dt.Rows[i - 1]["txtdeductionamount"] = txtdeductionamount.Text;
                    dt.Rows[i - 1]["ddldeductiondcacode"] = ddldeductiondca.SelectedValue;
                    dt.Rows[i - 1]["ddldeductionSubdcacode"] = ddldeductionsdca.SelectedValue;
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
                    DropDownList ddldeductiondca = (DropDownList)gvdeduction.Rows[rowIndex].Cells[1].FindControl("ddldeductiondca");
                    DropDownList ddldeductionsdca = (DropDownList)gvdeduction.Rows[rowIndex].Cells[2].FindControl("ddldeductionsdca");
                    TextBox txtdeductionamount = (TextBox)gvdeduction.Rows[rowIndex].Cells[3].FindControl("txtdeductionamount");

                    if (i < dt.Rows.Count - 1)
                    {
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
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:calculatededuction()", true);
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
                da = new SqlDataAdapter("TaxDcasNew_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtindt.Text;
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblccode.Text;
                da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Deductions";
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
                    DataTable Objdt = ViewState["Curtbldeduction"] as DataTable;
                    if (Objdt.Rows[dd]["ddldeductiondca"].ToString() != "Select" && Objdt.Rows[dd]["ddldeductiondca"].ToString() != "" && Objdt.Rows[dd]["ddldeductionsdca"].ToString() != "Select" && Objdt.Rows[dd]["ddldeductionsdca"].ToString() != "")
                    {
                        DropDownList ddldeductiondca = (DropDownList)e.Row.FindControl("ddldeductiondca");
                        TextBox txtdedamount = (TextBox)e.Row.FindControl("txtdeductionamount");
                        da = new SqlDataAdapter("TaxDcasNew_sp", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtindt.Text;
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblccode.Text;
                        da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Deductions";
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
                            ddldeductiondca.SelectedValue = Objdt.Rows[dd]["ddldeductiondcacode"].ToString();
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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string taxtypes = "";
            string taxdcas = "";
            string taxsdcas = "";
            string taxnos = "";
            string taxamounts = "";

            string cesstypes = "";
            string cessdcas = "";
            string cesssdcas = "";
            string cessnos = "";
            string cessamounts = "";

            string otherdcas = "";
            string othersdcas = "";
            string otherdcasamt = "";

            string deductiondcas = "";
            string deductionsdcas = "";
            string deductionamt = "";
            foreach (GridViewRow record in gvtaxes.Rows)
            {
                if (gvtaxes != null)
                {
                    if ((record.FindControl("chkSelecttaxes") as CheckBox).Checked)
                    {
                        taxtypes = taxtypes + (record.FindControl("ddltype") as DropDownList).SelectedValue + ",";
                        taxdcas = taxdcas + (record.FindControl("ddltaxdca") as DropDownList).SelectedValue + ",";
                        taxsdcas = taxsdcas + (record.FindControl("ddltaxsdca") as DropDownList).SelectedValue + ",";
                        taxnos = taxnos + (record.FindControl("ddltaxnos") as DropDownList).SelectedValue + ",";
                        taxamounts = taxamounts + (record.FindControl("txttaxamount") as TextBox).Text + ",";
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, "Please Verify Taxes");
                        break;
                    }
                }
            }
            foreach (GridViewRow record in gvcess.Rows)
            {
                if (gvcess != null)
                {
                    if ((record.FindControl("chkSelectcess") as CheckBox).Checked)
                    {
                        cesstypes = cesstypes + (record.FindControl("ddltypecess") as DropDownList).SelectedValue + ",";
                        cessdcas = cessdcas + (record.FindControl("ddlcessdca") as DropDownList).SelectedValue + ",";
                        cesssdcas = cesssdcas + (record.FindControl("ddlcesssdca") as DropDownList).SelectedValue + ",";
                        cessnos = cessnos + (record.FindControl("ddlcessnos") as DropDownList).SelectedValue + ",";
                        cessamounts = cessamounts + (record.FindControl("txtcessamount") as TextBox).Text + ",";
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, "Please Verify Cess");
                        break;
                    }
                }
            }
            foreach (GridViewRow record in gvother.Rows)
            {
                if (rbtnothercharges.SelectedValue == "Yes")
                {
                    if (gvother != null)
                    {
                        if ((record.FindControl("chkother") as CheckBox).Checked)
                        {
                            otherdcas = otherdcas + (record.FindControl("ddlotherdca") as DropDownList).SelectedValue + ",";
                            othersdcas = othersdcas + (record.FindControl("ddlothersdca") as DropDownList).SelectedValue + ",";
                            otherdcasamt = otherdcasamt + (record.FindControl("txtotheramount") as TextBox).Text + ",";
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Please Verify Other");
                            break;
                        }
                    }
                }
            }

            foreach (GridViewRow record in gvdeduction.Rows)
            {
                if (rbtndeductioncharges.SelectedValue == "Yes")
                {
                    if (gvdeduction != null)
                    {
                        if ((record.FindControl("chkdeduction") as CheckBox).Checked)
                        {
                            deductiondcas = deductiondcas + (record.FindControl("ddldeductiondca") as DropDownList).SelectedValue + ",";
                            deductionsdcas = deductionsdcas + (record.FindControl("ddldeductionsdca") as DropDownList).SelectedValue + ",";
                            deductionamt = deductionamt + (record.FindControl("txtdeductionamount") as TextBox).Text + ",";
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Please Verify Deduction");
                            break;
                        }
                    }
                }
            }

            string result = "";

            cmd1 = new SqlCommand("checkbudgetsp", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Clear();
            cmd1.Parameters.AddWithValue("@CCCode", lblccode.Text);
            cmd1.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);

            cmd1.Parameters.AddWithValue("@taxtypes", taxtypes);
            cmd1.Parameters.AddWithValue("@taxdcas", taxdcas);
            cmd1.Parameters.AddWithValue("@taxamounts", taxamounts);

            cmd1.Parameters.AddWithValue("@cesstypes", cesstypes);
            cmd1.Parameters.AddWithValue("@cessdcas", cessdcas);
            cmd1.Parameters.AddWithValue("@cessamounts", cessamounts);

            cmd1.Parameters.AddWithValue("@Otherdcas", otherdcas);
            cmd1.Parameters.AddWithValue("@Otherdcaamts", otherdcasamt);


            cmd1.Connection = con;
            con.Open();
            result = cmd1.ExecuteScalar().ToString();
            con.Close();

            if (result == "sufficent")
            {
                cmd.CommandText = "sp_pending_invoice_serviceprovider";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VendorId", ddlvendor.SelectedValue);
                cmd.Parameters.AddWithValue("@PO_NO", ddlpo.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CCCode", lblccode.Text);
                cmd.Parameters.AddWithValue("@DCA_Code", lbldcacode.Text);
                if (lblsdcacode.Text != "")
                    cmd.Parameters.AddWithValue("@Sub_DCA", lblsdcacode.Text);
                cmd.Parameters.AddWithValue("@InvoiceNo", txtin.Text);
                cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);

                cmd.Parameters.AddWithValue("@taxtypes", taxtypes);
                cmd.Parameters.AddWithValue("@taxdcas", taxdcas);
                cmd.Parameters.AddWithValue("@taxsdcas", taxsdcas);
                cmd.Parameters.AddWithValue("@taxnos", taxnos);
                cmd.Parameters.AddWithValue("@taxamounts", taxamounts);

                cmd.Parameters.AddWithValue("@cesstypes", cesstypes);
                cmd.Parameters.AddWithValue("@cessdcas", cessdcas);
                cmd.Parameters.AddWithValue("@cesssdcas", cesssdcas);
                cmd.Parameters.AddWithValue("@cessnos", cessnos);
                cmd.Parameters.AddWithValue("@cessamounts", cessamounts);

                cmd.Parameters.AddWithValue("@Otherdcas", otherdcas);
                cmd.Parameters.AddWithValue("@Othersdcas", othersdcas);
                cmd.Parameters.AddWithValue("@Otherdcaamts", otherdcasamt);

                cmd.Parameters.AddWithValue("@Deductiondcas", deductiondcas);
                cmd.Parameters.AddWithValue("@Deductionsdcas", deductionsdcas);
                cmd.Parameters.AddWithValue("@Deductiondcaamts", deductionamt);

                cmd.Parameters.AddWithValue("@InvTotal", txtinvvalue.Text);
                cmd.Parameters.AddWithValue("@Description", txtdescription.Text);
                cmd.Parameters.AddWithValue("@Retention", txtretention.Text);
                cmd.Parameters.AddWithValue("@Hold", txthold.Text);

                cmd.Parameters.AddWithValue("@Amount", txtnetamt.Text);

                cmd.Parameters.AddWithValue("@PaymentType", "Service Provider");
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "invalid";
                con.Close();
                if (msg == "Invoice Inserted")
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
                JavaScript.UPAlert(Page, result);
            }

        }

        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        clear();
        btnremovetax.Visible = false;
        btnremovecess.Visible = false;
        laodvendor();
        tbldetails.Visible = false;
        trothergrid.Style["display"] = "none";
        trdeductiongrid.Style["display"] = "none";
        ddlpo.SelectedIndex = 0;
    }

    public void clear()
    {
        txtin.Text = "";
        txtindt.Text = "";
        txtindtmk.Text = "";
        txtbasic.Text = "";
        gvtaxes.DataSource = null;
        gvtaxes.DataBind();
        lbltaxes.Text = "0";
        gvcess.DataSource = null;
        gvcess.DataBind();
        txtcess.Text = "0";
        rbtnothercharges.SelectedIndex = -1;
        gvother.DataSource = null;
        gvother.DataBind();
        txtother.Text = "0";
        rbtndeductioncharges.SelectedIndex = -1;
        gvdeduction.DataSource = null;
        gvdeduction.DataBind();
        txtdeduction.Text = "0";
        txtinvvalue.Text = "";
        txtdescription.Text = "";
        txtretention.Text = "";
        txthold.Text = "";
        txtnetamt.Text = "";
        btnremovetax.Visible = false;
        btnremovecess.Visible = false;
        btnaddtax.Visible = true;
        btnaddcess.Visible = true;
    }
   
}