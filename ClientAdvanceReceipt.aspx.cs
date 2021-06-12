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


public partial class ClientAdvanceReceipt : System.Web.UI.Page
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
            clientid();
        }
    }
    public void fillCC(string subclient)
    {
        try
        {

            //ValuesClear();
            da = new SqlDataAdapter("select distinct p.cc_code,cc_name,(p.cc_code+','+cc_name) as [Name] from po p join Cost_Center cc on p.CC_code=cc.cc_code where subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and MobilisationAdv='Yes' and p.status='3'", con);
            da.Fill(ds, "CCType");
            if (ds.Tables["CCType"].Rows.Count > 0)
            {
                ddlcccode.DataTextField = "Name";
                ddlcccode.DataValueField = "cc_code";
                ddlcccode.DataSource = ds.Tables["CCType"];
                ddlcccode.DataBind();
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
            else
            {
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("select po_no,CC_SubType from po p join Cost_Center cc on p.CC_code=cc.cc_code where MobilisationAdv='Yes' and p.CC_code='" + ddlcccode.SelectedValue + "' and p.status='3'", con);
            da.Fill(ds, "PO");
            ddlpo.DataTextField = "po_no";
            ddlpo.DataValueField = "po_no";
            ddlpo.DataSource = ds.Tables["PO"];
            ddlpo.DataBind();
            ddlpo.Items.Insert(0, "Select PO");
            if (ds.Tables["PO"].Rows.Count > 0)
            {
                trpo.Visible = true;
            }
            else
            {
                trpo.Visible = false;
            }
           
        }
        catch (Exception ex)
        {

        }
    }
    public void clientid()
    {
        ddlclientid.Items.Clear();
        try
        {
            da = new SqlDataAdapter("select distinct client_id from po where MobilisationAdv='Yes' and status='3'", con);

            da.Fill(ds, "client");
            if (ds.Tables["client"].Rows.Count > 0)
            {
                ddlclientid.DataTextField = "client_id";
                ddlclientid.DataValueField = "client_id";
                ddlclientid.DataSource = ds.Tables["client"];
                ddlclientid.DataBind();
                ddlclientid.Items.Insert(0, "Select");
                ddlsubclientid.Items.Insert(0, "Select");
                subclientid(ddlclientid.DataTextField);
            }
            else
            {
                ddlclientid.Items.Insert(0, "Select");
                ddlsubclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void subclientid(string clientid)
    {
       ddlsubclientid.Items.Clear();
        try
        {
            da = new SqlDataAdapter("select distinct subclient_id from po where MobilisationAdv='Yes' and client_id='" + ddlclientid.SelectedItem.Text + "' and status='3'", con);
            da.Fill(ds, "subclient");
            if (ds.Tables["subclient"].Rows.Count > 0)
            {
                ddlsubclientid.DataTextField = "subclient_id";
                ddlsubclientid.DataValueField = "subclient_id";
                ddlsubclientid.DataSource = ds.Tables["subclient"];
                ddlsubclientid.DataBind();
                ddlsubclientid.Items.Insert(0, "Select");
                fillCC(ddlsubclientid.DataValueField);
            }
            else
            {
                ddlsubclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clientname();
        subclientid();
    }
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        subclientname();
        fillCC();
    }
   
    public void subclientid()
    {
        ddlsubclientid.Items.Clear();
        try
        {
           
            da = new SqlDataAdapter("select distinct subclient_id from po where MobilisationAdv='Yes' and client_id='" + ddlclientid.SelectedItem.Text + "' and status='3'", con);           
            da.Fill(ds, "subclient");
            if (ds.Tables["subclient"].Rows.Count > 0)
            {
                ddlsubclientid.DataTextField = "subclient_id";
                ddlsubclientid.DataValueField = "subclient_id";
                ddlsubclientid.DataSource = ds.Tables["subclient"];
                ddlsubclientid.DataBind();
                ddlsubclientid.Items.Insert(0, "Select");
            }
            else
            {
                ddlsubclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void subclientname()
    {
       try
        {
            da = new SqlDataAdapter("select branch  from subclient where subclient_id='" + ddlsubclientid.SelectedItem.Text + "'", con);
            da.Fill(ds, "subclientname");
            da.Fill(ds, "subclientname");
            if (ds.Tables["subclientname"].Rows.Count > 0)

                Label9.Text = ds.Tables["subclientname"].Rows[0].ItemArray[0].ToString();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void fillCC()
    {
        try
        {           
            ddlcccode.Items.Clear();
            //ddlpono.Items.Clear();
            //ddlinno.Items.Clear();
            //ValuesClear();          
            da = new SqlDataAdapter("select distinct p.cc_code,cc_name,(p.cc_code+','+cc_name) as [Name] from po p join Cost_Center cc on p.CC_code=cc.cc_code where subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and MobilisationAdv='Yes' and p.status='3'", con);
            da.Fill(ds, "CCType");
            if (ds.Tables["CCType"].Rows.Count > 0)
            {
                ddlcccode.DataTextField = "Name";
                ddlcccode.DataValueField = "cc_code";

                ddlcccode.DataSource = ds.Tables["CCType"];
                ddlcccode.DataBind();
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
            else
            {
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void btnAddTax_Click(object sender, EventArgs e)
    {
        btnaddtax.Visible = false;
        btnremovetax.Visible = true;
        traddtaxes.Style["display"] = "block";
        BindtaxGridview();

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
                da = new SqlDataAdapter("TaxDcas_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = selectedtype;
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtpodate.Text;
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
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
            DropDownList ddltaxsdca = (DropDownList)currentRow.FindControl("ddltaxsdca");
            DropDownList ddltaxnos = (DropDownList)currentRow.FindControl("ddltaxnos");
            ViewState["TAX"] = ddltaxdca.SelectedValue;
            if (ddltaxdca != null && ddltaxdca.SelectedIndex > 0 && ddltaxsdca != null)
            {
                da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + ddltaxdca.SelectedValue + "'", con);
                ds = new DataSet();
                da.Fill(ds, "Taxsdcas");
                ddltaxsdca.DataSource = ds.Tables["Taxsdcas"];
                ddltaxsdca.DataTextField = "mapsubdca_code";
                ddltaxsdca.DataValueField = "subdca_code";
                ddltaxsdca.DataBind();
                ddltaxsdca.Items.Insert(0, new ListItem("Select"));
                da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + ddltaxdca.SelectedValue + "' and STATUS='Active'", con);
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
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
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
                        da = new SqlDataAdapter("TaxDcas_sp", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = selectedtype;
                        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtpodate.Text;
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
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
                        da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + Objdt.Rows[i]["ddltaxdcacode"].ToString() + "'", con);
                        ds = new DataSet();
                        da.Fill(ds, "Taxsdcas");
                        ddltaxSubdca.DataSource = ds.Tables["Taxsdcas"];
                        ddltaxSubdca.DataTextField = "mapsubdca_code";
                        ddltaxSubdca.DataValueField = "subdca_code";
                        ddltaxSubdca.DataBind();
                        ddltaxSubdca.Items.Insert(0, new ListItem("Select"));
                        ddltaxSubdca.SelectedValue = Objdt.Rows[i]["ddltaxSubdcacode"].ToString();
                        DropDownList ddltaxnos = (DropDownList)e.Row.FindControl("ddltaxnos");
                        da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + Objdt.Rows[i]["ddltaxdcacode"].ToString() + "' and STATUS='Active'", con);
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
                da = new SqlDataAdapter("TaxDcas_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = selectedtype;
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtpodate.Text;
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
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
        txtpodate.Enabled = false;
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
        txtpodate.Enabled = true;
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);

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
                        da = new SqlDataAdapter("TaxDcas_sp", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = selectedtype;
                        da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtpodate.Text;
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
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
    #region Deduction START
    protected void rbtndeductioncharges_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbtndeductioncharges.SelectedIndex == 0)
        {
            if (txtpodate.Text != "")
            {
                txtpodate.Enabled = false;
                trdeductiongrid.Style["display"] = "block";
                hfdeduction.Value = Convert.ToInt16(0).ToString();
                txtdeduction.Text = Convert.ToInt16(0).ToString();
                BinddeductionGridview();
                deductiondcalist();
            }
        }
        else
        {
            txtpodate.Enabled = true;
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
        ViewState["Curtbldeduction"] = dtt;
        gvdeduction.DataSource = dtt;
        gvdeduction.DataBind();
        BindIsotherCC();
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
                //DropDownList ddldeductiondca = (DropDownList)ded.FindControl("ddldeductiondca");
                DropDownList ddldeductiondca = (DropDownList)ded.FindControl("ddldeductiondca");
                DropDownList ddlothercc = (DropDownList)ded.FindControl("ddlothercc");
                DropDownList ddlccode = (DropDownList)ded.FindControl("ddlccode");
                if (ddlothercc.SelectedValue != "Select")
                {


                    da = new SqlDataAdapter("TaxDcas_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtpodate.Text;
                    //da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                    //da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Deduction";
                    if (ddlothercc.SelectedValue == "No")
                    {
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                    }
                    if (ddlothercc.SelectedValue == "Yes")
                    {
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlccode.SelectedValue;
                    }
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
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtpodate.Text;
                if (ddlothercc.SelectedValue == "No")
                {
                    da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                }
                if (ddlothercc.SelectedValue == "Yes")
                {
                    da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlccodes.SelectedValue;
                }
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
                    DropDownList ddlisother = (DropDownList)e.Row.FindControl("ddlothercc");
                    ddlisother.Items.Insert(0, new ListItem("Select", "Select"));
                    ddlisother.Items.Insert(1, new ListItem("Yes", "Yes"));
                    ddlisother.Items.Insert(2, new ListItem("No", "No"));
                    DropDownList ddldeductiondca = (DropDownList)e.Row.FindControl("ddldeductiondca");
                    DropDownList ddlccode = (DropDownList)e.Row.FindControl("ddlccode");
                    da = new SqlDataAdapter("TaxDcas_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtpodate.Text;
                    //da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                    //da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Deduction";
                    if (ddlisother.SelectedValue == "Yes")
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                    else if (ddlisother.SelectedValue == "No")
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlccode.SelectedValue;
                    else
                        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
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

                    }
                    DataTable Objdt = ViewState["Curtbldeduction"] as DataTable;
                    if (Objdt.Rows[dd]["ddldeductiondca"].ToString() != "Select" && Objdt.Rows[dd]["ddldeductiondca"].ToString() != "" && Objdt.Rows[dd]["ddldeductionsdca"].ToString() != "Select" && Objdt.Rows[dd]["ddldeductionsdca"].ToString() != "")
                    {

                        ddlisother.SelectedValue = Objdt.Rows[dd]["ddlisother"].ToString();


                        DropDownList ddldeductioncc = (DropDownList)e.Row.FindControl("ddlccode");
                        TextBox txtdedamount = (TextBox)e.Row.FindControl("txtdeductionamount");
                        //ddldeductiondca.SelectedValue = Objdt.Rows[dd]["ddldeductiondcacode"].ToString();
                        DropDownList ddldeductiondcas = (DropDownList)e.Row.FindControl("ddldeductiondca"); 
                        if (Objdt.Rows[dd]["ddlisother"].ToString() == "Yes")
                        {
                            da = new SqlDataAdapter("select distinct cc_code,(cc_code+' , '+cc_name)as name from cost_center  where cc_type='Performing' and status in ('Old','New') and cc_code !='" + ddlcccode.SelectedValue + "' ", con);
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
                            ddldeductioncc.Items.Insert(0, new ListItem(ddlcccode.SelectedValue, ddlcccode.SelectedValue));
                            ddldeductioncc.DataBind();
                        }
                        ddldeductioncc.SelectedValue = Objdt.Rows[dd]["ddlothercccode"].ToString();
                        ddldeductiondcas.SelectedValue = Objdt.Rows[dd]["ddldeductiondcacode"].ToString();
                        DropDownList ddldeductionsdca = (DropDownList)e.Row.FindControl("ddldeductionsdca");
                        da = new SqlDataAdapter("select distinct subdca_code,(mapsubdca_code+' , '+subdca_name)as mapsubdca_code from subdca where dca_code='" + Objdt.Rows[dd]["ddldeductiondcacode"].ToString() + "'", con);
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
    protected void ddlothercc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlothercc = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddlothercc.NamingContainer;
            DropDownList ddlothercccode = (DropDownList)currentRow.FindControl("ddlccode");
            if (ddlothercc.SelectedValue == "Yes")
            {
                da = new SqlDataAdapter("select distinct cc_code,(cc_code+' , '+cc_name)as name from cost_center  where cc_code !='" + ddlcccode.SelectedValue + "' ", con);
                da.Fill(ds, "OtherCC");
                if (ds.Tables["OtherCC"].Rows.Count > 0)
                {
                    ddlothercccode.Items.Clear();
                    ddlothercccode.DataSource = ds.Tables["OtherCC"];
                    ddlothercccode.DataValueField = "cc_code";
                    ddlothercccode.DataTextField = "name";
                    ddlothercccode.DataBind();
                    ddlothercccode.Items.Insert(0, new ListItem("Select", "Select"));
                }
            }
            else if (ddlothercc.SelectedValue == "No")
            {
                ddlothercccode.Items.Clear();
                ddlothercccode.Items.Insert(0, new ListItem("Select", "Select"));
                ddlothercccode.Items.Insert(1, new ListItem(ddlcccode.SelectedValue, ddlcccode.SelectedValue));
                ddlothercccode.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion Deduction END
    protected void ddlpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //da = new SqlDataAdapter("Select po_date from po where client_id='" + ddlclientid.SelectedItem.Text + "' and subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and status='3'", con);
            //da.Fill(ds, "PO");
            if (ddlpo.SelectedItem.Text != "Select PO")
            {
               

                trdeduction.Visible = true;
                trtaxes.Visible = true;
                trcess.Visible = true;
                paymentdetails.Visible = true;
                btn.Visible = true;
            }
            else
            {
                trdeduction.Visible = false;
                trtaxes.Visible = false;
                trcess.Visible = false;
                btn.Visible = false;
                paymentdetails.Visible = false;
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
            cmd.CommandText = "sp_ClientAdvanceInvoiceRecieving";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PO_NO", ddlpo.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@RA_NO", txtra.Text);
            cmd.Parameters.AddWithValue("@CCCode", ddlcccode.SelectedValue);
            cmd.Parameters.AddWithValue("@Invoice_Date", txtpodate.Text);
            cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
            
           

            cmd.Parameters.AddWithValue("@Dcas", Dcas);
            cmd.Parameters.AddWithValue("@SubDcas", Subdcas);
            cmd.Parameters.AddWithValue("@CCCodes", CCCodes);
            cmd.Parameters.AddWithValue("@Amounts", Amounts);         

            cmd.Parameters.AddWithValue("@taxtypes", taxtypes);
            cmd.Parameters.AddWithValue("@taxdcas", taxdcas);
            cmd.Parameters.AddWithValue("@taxsdcas", taxsdcas);
            cmd.Parameters.AddWithValue("@taxnos", taxnos);
            cmd.Parameters.AddWithValue("@taxamounts", taxamounts);
            cmd.Parameters.AddWithValue("@taxmapdcas", taxmapdcacodes);

            cmd.Parameters.AddWithValue("@cesstypes", cesstypes);
            cmd.Parameters.AddWithValue("@cessdcas", cessdcas);
            cmd.Parameters.AddWithValue("@cesssdcas", cesssdcas);
            cmd.Parameters.AddWithValue("@cessnos", cessnos);
            cmd.Parameters.AddWithValue("@cessamounts", cessamounts);

            cmd.Parameters.AddWithValue("@ClientId", ddlclientid.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@SubClientId", ddlsubclientid.SelectedItem.Text);

            cmd.Parameters.AddWithValue("@InvTotal", Convert.ToString(Convert.ToDouble(txtbasic.Text) + Convert.ToDouble(lbltaxes.Text)+Convert.ToDouble(txtcess.Text)));
            cmd.Parameters.AddWithValue("@BankName", ddlfrom.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Date", txtdate.Text);
            cmd.Parameters.AddWithValue("@No", txtcheque.Text);
            cmd.Parameters.AddWithValue("@Remarks", txtdesc.Text);
            cmd.Parameters.AddWithValue("@Amount", txtamt.Text);         
          
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Type", "Submit");

            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            // string msg = "invalid";
            con.Close();
            if (msg == "Invoice Inserted")
            {
                JavaScript.UPAlertRedirect(Page, "Advance Reciept Submitted", Request.Url.ToString());
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
        }

        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
}