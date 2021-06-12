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

public partial class InvoiceVerficationNew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    DataSet ds1 = new DataSet();
    SqlCommand cmd1 = new SqlCommand();
    SqlCommand cmd2 = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            tblinvoiceverification.Visible = false;
            fillgrid();
        }
        if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Chief Material Controller")
        {
            trtaxbtns.Visible = false;
            trbtncess.Visible = false;
            rbtndeductioncharges.Enabled = false;
            rbtnothercharges.Enabled = false;
        }
        else
        {
            trtaxbtns.Visible = true;
            trbtncess.Visible = true;
            rbtndeductioncharges.Enabled = true;
            rbtnothercharges.Enabled = true;
        }

    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount,po_no from pending_invoice where status='A0' and paymenttype not in ('Supplier','Excise Duty','Service provider VAT','Service Tax','VAT' ) and SubType='Service Provider' order by id desc", con);

            else if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount,po_no from pending_invoice where status='A1' and paymenttype not in ('Supplier','Excise Duty','Service provider VAT','Service Tax','VAT' ) and SubType='Service Provider' order by id desc", con);
            
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount,po_no from pending_invoice where status='A2' and paymenttype not in ('Supplier','Excise Duty','Service provider VAT','Service Tax','VAT' ) and SubType='Service Provider' order by id desc", con);

            else if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount,po_no from pending_invoice where status='1' and paymenttype not in ('Supplier','Excise Duty','Service provider VAT','Service Tax','VAT' ) and SubType='Service Provider' order by id desc", con);

            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount,po_no from pending_invoice where ((status='2' and paymenttype in ('Service Provider'))) and SubType='Service Provider'  order by id desc", con);
            da.Fill(ds, "FillIn");
            GridView1.DataSource = ds.Tables["FillIn"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (GridView1.SelectedIndex != -1)
    //    {
           
    //            string InvoiceNo = GridView1.SelectedValue.ToString();
    //            FillInvoiceInfo(InvoiceNo);
           
    //    }
    //}
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string pono = GridView1.DataKeys[e.NewEditIndex]["po_no"].ToString();
        string cccode = GridView1.DataKeys[e.NewEditIndex]["cc_code"].ToString();
        string invoiceno = GridView1.DataKeys[e.NewEditIndex]["invoiceno"].ToString();       
        cmd = new SqlCommand("checkamendbudgetServiceprovider", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PoNo", pono);
        cmd.Parameters.AddWithValue("@CCCode", cccode);
        cmd.Parameters.AddWithValue("@InvNo", invoiceno); 
        cmd.Connection = con;
        con.Open();
        string result = cmd.ExecuteScalar().ToString();
        con.Close();
        if (result == "success")
        {
            da = new SqlDataAdapter("select cc_type from cost_center where cc_code='" + GridView1.DataKeys[e.NewEditIndex]["cc_code"].ToString() + "'", con);
            da.Fill(ds, "cctype");
            ViewState["ctype"] = ds.Tables["cctype"].Rows[0][0].ToString();
            FillInvoiceInfo(invoiceno);
            
        }
        else
        {
            JavaScript.UPAlert(Page, result);
        }
    }
    public void FillInvoiceInfo(string Invoiceno)
    {
        try
        {
            da = new SqlDataAdapter("GetserviceproviderInvoice", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Flag", "Get Seleted Invoice");
            da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", Invoiceno);
            DataSet Objds = new DataSet();
            da.Fill(Objds, "InvoiceInfo");
            if (Objds.Tables["InvoiceInfo"].Rows.Count > 0)
            {
                lblvenid.Text = Objds.Tables["InvoiceInfo"].Rows[0]["vendor_id"].ToString();
                lblvendorname.Text = Objds.Tables["InvoiceInfo"].Rows[0]["name"].ToString();
                lblpono.Text = Objds.Tables["InvoiceInfo"].Rows[0]["PO_NO"].ToString();
                lblinvno.Text = Objds.Tables["InvoiceInfo"].Rows[0]["InvoiceNo"].ToString();
                lblccode.Text = Objds.Tables["InvoiceInfo"].Rows[0]["CC_Code"].ToString();
                lbldcacode.Text = Objds.Tables["InvoiceInfo"].Rows[0]["DCA_Code"].ToString();
                lblsdcacode.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Sub_DCA"].ToString();
                txtindt.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Invoice_Date"].ToString();
                txtindtmk.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Inv_Making_Date"].ToString(); 
                txtbasic.Text = Objds.Tables["InvoiceInfo"].Rows[0]["BasicValue"].ToString().Replace(".0000",".00");
                txtinvvalue.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Total"].ToString().Replace(".0000", ".00");
                txtdescription.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Description"].ToString();
                txtretention.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Retention"].ToString().Replace(".0000", ".00");
                txthold.Text = Objds.Tables["InvoiceInfo"].Rows[0]["Hold"].ToString().Replace(".0000", ".00");
                txtnetamt.Text = Objds.Tables["InvoiceInfo"].Rows[0]["netamount"].ToString().Replace(".0000", ".00");
                if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                {
                    txtindt.Enabled = false;
                    txtindtmk.Enabled = false;
                    txtbasic.Enabled = false;
                    txtinvvalue.Enabled = false;
                    txtdescription.Enabled = false;
                    txtretention.Enabled = false;
                    txthold.Enabled = false;
                    txtnetamt.Enabled = false;
                }

                if (Objds.Tables["InvoiceInfo1"].Rows.Count > 0)
                {
                    DataTable dttaxes = new DataTable();
                    dttaxes.Columns.Add("S.No", typeof(int));
                    dttaxes.Columns.Add("chkSelecttaxes", typeof(string));
                    dttaxes.Columns.Add("ddltype", typeof(string));
                    dttaxes.Columns.Add("ddltaxdca", typeof(string));
                    dttaxes.Columns.Add("ddltaxsdca", typeof(string));
                    dttaxes.Columns.Add("ddltaxnos", typeof(string));
                    dttaxes.Columns.Add("txttaxamount", typeof(string));
                    dttaxes.Columns.Add("ddltaxdcacode", typeof(string));
                    dttaxes.Columns.Add("ddltaxSubdcacode", typeof(string));
                    dttaxes.Columns.Add("ddltaxnumbers", typeof(string));
                    for (int i = 0; i < Objds.Tables["InvoiceInfo1"].Rows.Count; i++)
                    {
                        DataRow dr = dttaxes.NewRow();                       
                        dr["chkSelecttaxes"] = string.Empty;
                        dr["ddltype"] = Objds.Tables["InvoiceInfo1"].Rows[i]["Tax_Type"].ToString();
                        dr["ddltaxdca"] = Objds.Tables["InvoiceInfo1"].Rows[i]["Dca_Code"].ToString();
                        dr["ddltaxsdca"] = Objds.Tables["InvoiceInfo1"].Rows[i]["Subdca_Code"].ToString();
                        dr["ddltaxnos"] = Objds.Tables["InvoiceInfo1"].Rows[i]["RegdNo"].ToString();
                        dr["txttaxamount"] = Objds.Tables["InvoiceInfo1"].Rows[i]["amount"].ToString().Replace(".0000", ".00");
                        dr["ddltaxdcacode"] = Objds.Tables["InvoiceInfo1"].Rows[i]["dcamain"].ToString();
                        dr["ddltaxSubdcacode"] = Objds.Tables["InvoiceInfo1"].Rows[i]["sdcamain"].ToString();
                        dr["ddltaxnumbers"] = Objds.Tables["InvoiceInfo1"].Rows[i]["RegdNo"].ToString();
                        dttaxes.Rows.Add(dr);
                    }
                    ViewState["Curtbl"] = dttaxes;
                    ViewState["Checktaxes"] = dttaxes;
                    gvtaxes.DataSource = dttaxes;
                    gvtaxes.DataBind();
                    loadtaxtype();
                    traddtaxes.Style["display"] = "block";
                    btnaddtax.Visible = false;
                    lbltaxes.Text = Objds.Tables["InvoiceInfo2"].Rows[0]["Taxes"].ToString().Replace(".0000", ".00");
                }
                else
                {
                    traddtaxes.Style["display"] = "none";
                    btnaddtax.Visible = true;
                    btnremovetax.Visible = false;
                }
                if (Objds.Tables["InvoiceInfo7"].Rows.Count > 0)
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
                    for (int i = 0; i < Objds.Tables["InvoiceInfo7"].Rows.Count; i++)
                    {
                        DataRow dr = dtcess.NewRow();
                        dr["chkSelectcess"] = string.Empty;
                        dr["ddltypecess"] = Objds.Tables["InvoiceInfo7"].Rows[i]["Tax_Type"].ToString();
                        dr["ddlcessdca"] = Objds.Tables["InvoiceInfo7"].Rows[i]["DCA_Code"].ToString();
                        dr["ddlcesssdca"] = Objds.Tables["InvoiceInfo7"].Rows[i]["SubDCA_Code"].ToString();
                        dr["ddlcessnos"] = Objds.Tables["InvoiceInfo7"].Rows[i]["RegdNo"].ToString();
                        dr["txtcessamount"] = Objds.Tables["InvoiceInfo7"].Rows[i]["amount"].ToString().Replace(".0000", ".00");
                        dr["ddlcessdcacode"] = Objds.Tables["InvoiceInfo7"].Rows[i]["DCA_Code"].ToString();
                        dr["ddlcessSubdcacode"] = Objds.Tables["InvoiceInfo7"].Rows[i]["SubDCA_Code"].ToString();
                        dr["ddlcessnumbers"] = Objds.Tables["InvoiceInfo7"].Rows[i]["RegdNo"].ToString();
                        dtcess.Rows.Add(dr);
                    }
                    ViewState["Curtblcess"] = dtcess;
                    ViewState["Checkcess"] = dtcess;
                    gvcess.DataSource = dtcess;
                    gvcess.DataBind();
                    loadcesstype();
                    traddcess.Style["display"] = "block";
                    btnaddcess.Visible = false;
                    txtcess.Text = Objds.Tables["InvoiceInfo8"].Rows[0][0].ToString().Replace(".0000", ".00");
                }
                else
                {
                    btnaddcess.Visible = true;
                    btnremovecess.Visible = false;
                }
                if (Objds.Tables["InvoiceInfo3"].Rows.Count > 0)
                {
                    DataTable dtother = new DataTable();
                    dtother.Columns.Add("S.No", typeof(int));
                    dtother.Columns.Add("chkother", typeof(string));
                    dtother.Columns.Add("ddlotherdca", typeof(string));
                    dtother.Columns.Add("ddlothersdca", typeof(string));
                    dtother.Columns.Add("txtotheramount", typeof(string));
                    dtother.Columns.Add("ddlotherdcacode", typeof(string));
                    dtother.Columns.Add("ddlotherSubdcacode", typeof(string));
                    for (int i = 0; i < Objds.Tables["InvoiceInfo3"].Rows.Count; i++)
                    {
                        DataRow dr = dtother.NewRow();
                        dr["chkother"] = string.Empty;                       
                        dr["ddlotherdca"] = Objds.Tables["InvoiceInfo3"].Rows[i]["Dca_Code"].ToString();
                        dr["ddlothersdca"] = Objds.Tables["InvoiceInfo3"].Rows[i]["Subdca_Code"].ToString();                       
                        dr["txtotheramount"] = Objds.Tables["InvoiceInfo3"].Rows[i]["amount"].ToString().Replace(".0000", ".00");
                        dr["ddlotherdcacode"] = Objds.Tables["InvoiceInfo3"].Rows[i]["Dca_Code"].ToString();
                        dr["ddlotherSubdcacode"] = Objds.Tables["InvoiceInfo3"].Rows[i]["Subdca_Code"].ToString();
                        dtother.Rows.Add(dr);
                    }
                    ViewState["Curtblother"] = dtother;
                    ViewState["Otherdcachk"] = dtother;
                    gvother.DataSource = dtother;
                    gvother.DataBind();
                    loaddca();
                    trothergrid.Style["display"] = "block";
                    rbtnothercharges.SelectedIndex = 0;
                    txtother.Text = Objds.Tables["InvoiceInfo4"].Rows[0][0].ToString().Replace(".0000", ".00");
                }
                else
                {
                    rbtnothercharges.SelectedIndex = 1;
                    trothergrid.Style["display"] = "none";
                    txtother.Text = "0";
                }
                if (Objds.Tables["InvoiceInfo5"].Rows.Count > 0)
                {
                    DataTable dtded = new DataTable();
                    dtded.Columns.Add("S.No", typeof(int));
                    dtded.Columns.Add("chkdeduction", typeof(string));
                    dtded.Columns.Add("ddldeductiondca", typeof(string));
                    dtded.Columns.Add("ddldeductionsdca", typeof(string));
                    dtded.Columns.Add("txtdeductionamount", typeof(string));
                    dtded.Columns.Add("ddldeductiondcacode", typeof(string));
                    dtded.Columns.Add("ddldeductionSubdcacode", typeof(string));
                    for (int i = 0; i < Objds.Tables["InvoiceInfo5"].Rows.Count; i++)
                    {
                        DataRow dr = dtded.NewRow();
                        dr["chkdeduction"] = string.Empty;
                        dr["ddldeductiondca"] = Objds.Tables["InvoiceInfo5"].Rows[i]["Dca_Code"].ToString();
                        dr["ddldeductionsdca"] = Objds.Tables["InvoiceInfo5"].Rows[i]["Subdca_Code"].ToString();
                        dr["txtdeductionamount"] = Objds.Tables["InvoiceInfo5"].Rows[i]["amount"].ToString().Replace(".0000", ".00");
                        dr["ddldeductiondcacode"] = Objds.Tables["InvoiceInfo5"].Rows[i]["Dca_Code"].ToString();
                        dr["ddldeductionSubdcacode"] = Objds.Tables["InvoiceInfo5"].Rows[i]["Subdca_Code"].ToString();
                        dtded.Rows.Add(dr);
                    }
                    ViewState["Curtbldeduction"] = dtded;
                    ViewState["deddcachk"] = dtded;
                    gvdeduction.DataSource = dtded;
                    gvdeduction.DataBind();
                    loaddeddca();
                    trdeductiongrid.Style["display"] = "block";
                    rbtndeductioncharges.SelectedIndex = 0;
                    txtdeduction.Text = Objds.Tables["InvoiceInfo6"].Rows[0][0].ToString().Replace(".0000", ".00");
                }
                else
                {
                    rbtndeductioncharges.SelectedIndex = 1;
                    trdeductiongrid.Style["display"] = "none";
                    txtdeduction.Text = "0";
                }
                da = new SqlDataAdapter("Select Approved_Users from pending_invoice where po_no='" + Objds.Tables["InvoiceInfo"].Rows[0]["PO_NO"].ToString() + "' and InvoiceNo='" + Objds.Tables["InvoiceInfo"].Rows[0]["InvoiceNo"].ToString() + "'", con);
                da.Fill(ds, "roles");
                if (ds.Tables["roles"].Rows.Count > 0 && ds.Tables["roles"].Rows[0].ItemArray[0].ToString() != "")
                {
                    Trgvusers.Visible = true;
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
                    Trgvusers.Visible = false;
                    gvusers.DataSource = null;
                    gvusers.DataBind();
                }
                tblinvoiceverification.Visible = true;
            }
        }
        catch (Exception ex)
        {
            //tblinvoiceverification.Visible = true;
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
                        da = new SqlDataAdapter("select Roles, (first_name+'  '+middle_name+'  '+last_name)as name ,ur.User_Name from user_roles ur join Employee_Data ed on ur.User_Name=ed.User_Name where ur.User_Name='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da.Fill(ds, "userroles");
                        //if (ds.Tables["userroles"].Rows[j].ItemArray[0].ToString() == "Sr.Accountant")
                        //    e.Row.Cells[0].Text = "Prepared By";                      
                        //if (ds.Tables["userroles"].Rows[j].ItemArray[0].ToString() == "PurchaseManager")
                        //    e.Row.Cells[0].Text = "Verified By";
                        //if (ds.Tables["userroles"].Rows[j].ItemArray[0].ToString() == "HoAdmin")
                        //    e.Row.Cells[0].Text = "Verified By";
                        if (k == 1)
                        {
                            e.Row.Cells[1].Text = "Sr.Accountant";
                            e.Row.Cells[0].Text = "Prepared By";
                        }
                        else if (k == 2 && ViewState["ctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "Project Manager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 3 && ViewState["ctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "PurchaseManager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 4 && ViewState["ctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "Chief Material Controller";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 5 && ViewState["ctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "HoAdmin";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 2 && (ViewState["ctype"].ToString() == "Non-Performing" || ViewState["ctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[1].Text = "PurchaseManager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 3 && (ViewState["ctype"].ToString() == "Non-Performing" || ViewState["ctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[1].Text = "Chief Material Controller";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 4 && (ViewState["ctype"].ToString() == "Non-Performing" || ViewState["ctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[1].Text = "HoAdmin";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        //e.Row.Cells[1].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                        e.Row.Cells[2].Text = ds.Tables["userroles"].Rows[j].ItemArray[1].ToString();
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
    #region taxstarts
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
                DropDownList types = (DropDownList)record.FindControl("ddltype");
                if (types.SelectedValue == "")
                {
                    types.Items.Insert(0, new ListItem("Select", "Select"));
                    types.Items.Insert(1, new ListItem("Creditable", "Creditable"));
                    types.Items.Insert(2, new ListItem("Non-Creditable", "Non-Creditable"));
                    types.DataBind();
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
        if (ViewState["Checktaxes"] == null)
        {
            ViewState["Curtbl"] = dt;
            gvtaxes.DataSource = dt;
            gvtaxes.DataBind();
            loadtaxtype();
        }
        else
        {
            ViewState["Curtbl"] = ViewState["Checktaxes"] as DataTable;
            gvtaxes.DataSource = ViewState["Curtbl"] as DataTable;
            gvtaxes.DataBind();
        }
        
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
        if (Session["roles"].ToString() != "PurchaseManager")
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
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatetaxes();Total();", true);
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
                //da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + ddltaxdca.SelectedValue + "' and STATUS='Active'", con);
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
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatetaxes();Total();", true);

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
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatetaxes();Total();", true);
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
                            if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                            {
                                ddltaxdca.Enabled = false;
                            }
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
                        
                       // da = new SqlDataAdapter("select DISTINCT Tax_nos from taxdca where dca_code='" + Objdt.Rows[i]["ddltaxdcacode"].ToString() + "' and STATUS='Active'", con);
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
                        ddltaxnos.Items.Clear();
                        ddltaxnos.DataSource = ds.Tables["taxvatdca"];
                        ddltaxnos.DataTextField = "Tax_nos";
                        ddltaxnos.DataValueField = "Tax_nos";
                        ddltaxnos.DataBind();
                        ddltaxnos.Items.Insert(0, new ListItem("Select"));
                        ddltaxnos.SelectedValue = Objdt.Rows[i]["ddltaxnumbers"].ToString();
                        txttaxamount.Text = Objdt.Rows[i]["txttaxamount"].ToString();
                        if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                        {
                            ddltaxSubdca.Enabled = false;
                            ddltaxnos.Enabled = false;
                            txttaxamount.Enabled = false;
                            ddltype.Enabled = false;
                            
                        }

                    }
                }
                i = i + 1;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                {
                    Button addbtn = (Button)e.Row.FindControl("btnAdd");
                    addbtn.Visible = false;
                }
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
        //ViewState["Curtblcess"] = dt;
        if (ViewState["Checkcess"] == null)
        {
            ViewState["Curtblcess"] = dt;
            gvcess.DataSource = dt;
            gvcess.DataBind();
            loadcesstype();
        }
        else
        {
            ViewState["Curtblcess"] = ViewState["Checkcess"] as DataTable;
            gvcess.DataSource = ViewState["Curtblcess"] as DataTable;
            gvcess.DataBind();
        }
        //gvcess.DataSource = dt;
        //gvcess.DataBind();
        
    }
    protected void btnaddcess_Click(object sender, EventArgs e)
    {

        AddNewcessRow();
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatecess();Total();", true);
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
            if (Session["roles"].ToString() != "PurchaseManager")
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
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatecess();Total();", true);
            }
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
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatecess();Total();", true);
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
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatecess();Total();", true);
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
                        //ddltypecess.SelectedValue = Objdt.Rows[c]["ddltypecess"].ToString();
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
                            if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                            {
                                ddlcessdca.Enabled = false;
                            }
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
                        if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                        {
                            ddlcessnos.Enabled = false;
                            ddlcessSubdca.Enabled = false;
                            txtcesamount.Enabled = false;
                            ddltypecess.Enabled = false;
                        }
                    }
                }
                c = c + 1;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                {
                    Button addbtn = (Button)e.Row.FindControl("btnAddcess");
                    addbtn.Visible = false;
                }
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
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculateother();Total()", true);
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
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Total()", true);
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
        if (ViewState["Otherdcachk"] == null)
        {
            ViewState["Curtblother"] = dtt;
            gvother.DataSource = dtt;
            gvother.DataBind();
        }
        else
        {
            ViewState["Curtblother"] = ViewState["Otherdcachk"] as DataTable;
            gvother.DataSource = ViewState["Curtblother"] as DataTable;
            gvother.DataBind();
        }
       

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
            if (Session["roles"].ToString() != "PurchaseManager")
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
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculateother();Total()", true);
            }
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
                da = new SqlDataAdapter("select subdca_code,(mapsubdca_code+' , '+subdca_name)as mapsubdca_code from subdca where dca_code='" + ddlotherdca.SelectedValue + "'", con);
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
                            if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                            {
                                ddlotherdca.Enabled = false;
                            }
                            ddlotherdca.Items.Clear();
                            ddlotherdca.DataSource = ds.Tables["otherdcas"];
                            ddlotherdca.DataTextField = "mapdca_code";
                            ddlotherdca.DataValueField = "dca_code";
                            ddlotherdca.DataBind();
                            ddlotherdca.Items.Insert(0, new ListItem("Select"));
                            ddlotherdca.SelectedValue = Objdt.Rows[ot]["ddlotherdcacode"].ToString();
                        }
                        DropDownList ddlotherSubdca = (DropDownList)e.Row.FindControl("ddlothersdca");
                        da = new SqlDataAdapter("select subdca_code,(mapsubdca_code+' , '+subdca_name)as mapsubdca_code from subdca where dca_code='" + Objdt.Rows[ot]["ddlotherdcacode"].ToString() + "'", con);
                        ds = new DataSet();
                        da.Fill(ds, "othersdcas");
                        ddlotherSubdca.DataSource = ds.Tables["othersdcas"];
                        ddlotherSubdca.DataTextField = "mapsubdca_code";
                        ddlotherSubdca.DataValueField = "subdca_code";
                        ddlotherSubdca.DataBind();
                        ddlotherSubdca.Items.Insert(0, new ListItem("Select"));
                        ddlotherSubdca.SelectedValue = Objdt.Rows[ot]["ddlotherSubdcacode"].ToString();
                        txtotheramount.Text = Objdt.Rows[ot]["txtotheramount"].ToString();
                        if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                        {
                            ddlotherSubdca.Enabled = false;
                            txtotheramount.Enabled = false;                            
                        }
                    }
                }
                ot = ot + 1;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                {
                    Button addbtn = (Button)e.Row.FindControl("btnaddothers");
                    addbtn.Visible = false;
                }
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
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatededuction();Total();", true);
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
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Total();", true);
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
        if (ViewState["deddcachk"] == null)
        {
            ViewState["Curtbldeduction"] = dtt;
            gvdeduction.DataSource = dtt;
            gvdeduction.DataBind();
        }
        else
        {
            ViewState["Curtbldeduction"] = ViewState["deddcachk"] as DataTable;
            gvdeduction.DataSource = ViewState["Curtbldeduction"] as DataTable;
            gvdeduction.DataBind();
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
            if (Session["roles"].ToString() != "PurchaseManager")
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
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculatededuction();Total();", true);
            }
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
                            if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                            {
                                ddldeductiondca.Enabled = false;
                            }
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
                        if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                        {
                            ddldeductionsdca.Enabled = false;
                            txtdedamount.Enabled = false;                           
                        }
                    }
                }
                dd = dd + 1;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Project Manager")
                {
                    Button addbtn = (Button)e.Row.FindControl("btnadddeduction");
                    addbtn.Visible = false;
                }
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
                string result1 = "";
                cmd2 = new SqlCommand("checkamendbudgetServiceprovider", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Clear();
                cmd2.Parameters.AddWithValue("@PoNo", lblpono.Text);
                cmd2.Parameters.AddWithValue("@CCCode", lblccode.Text);
                cmd2.Parameters.AddWithValue("@InvNo", lblinvno.Text);
                cmd2.Connection = con;
                con.Open();
                result1 = cmd2.ExecuteScalar().ToString();
                con.Close();
                if (result1 == "success")
                {
                    cmd.CommandText = "sp_Approve_invoice_New";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VendorId", lblvenid.Text);
                    cmd.Parameters.AddWithValue("@VendorName", lblvendorname.Text);
                    cmd.Parameters.AddWithValue("@PO_NO", lblpono.Text);
                    cmd.Parameters.AddWithValue("@CCCode", lblccode.Text);
                    cmd.Parameters.AddWithValue("@DCA_Code", lbldcacode.Text);
                    if (lblsdcacode.Text != "")
                        cmd.Parameters.AddWithValue("@Sub_DCA", lblsdcacode.Text);
                    cmd.Parameters.AddWithValue("@InvoiceNo", lblinvno.Text);
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

                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "invalid";
                    con.Close();
                    if (msg == "Sucessfull")
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
                    JavaScript.UPAlert(Page, result1);
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

    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        cmd.Connection = con;
        cmd = new SqlCommand("SPInvoice_RejectNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["id"].ToString());
        cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        JavaScript.UPAlert(Page, msg);
        con.Close();
        fillgrid();

    }

    //private void ChangeControlStatus()
    //{
    //    if (Session["roles"].ToString() == "PurchaseManager")
    //    {
    //        txtindt.Enabled = false;
    //        txtindtmk.Enabled = false;
    //        //ddlgstnos.Enabled = false;          
    //        txtindtmk.Enabled = false;
    //        txtbasic.Enabled = false;
    //        txttotal.Enabled = false;
    //        rbtnothercharges.Enabled = false;
    //        TextBox1.Enabled = false;
    //        rbtndeduction.Enabled = false;
    //        TextBox2.Enabled = false;
    //        txtnetAmount.Enabled = false;
    //    }

    //}
}