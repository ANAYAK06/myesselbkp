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


public partial class serviceproviderpo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            BindGridview();
            BindGridviewterm();
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string Descs = "";
        string Units = "";
        string Qtys = "";
        string Rates = "";
        string Amts = "";
        string MDescs = "";
        string OurAmts = "";
        string PrwAmts = "";
        if (txttotalamt.Text != "" || txttotalamt.Text != "0")
        {
            foreach (GridViewRow record in gvDetails.Rows)
            {
                if ((record.FindControl("chkSelect") as CheckBox).Checked)
                {
                    TextBox txtdesc = (TextBox)record.FindControl("txtitemdesc");
                    string desc = txtdesc.Text;
                    TextBox txtunit = (TextBox)record.FindControl("txtunit");
                    string unit = txtunit.Text;
                    TextBox txtqty = (TextBox)record.FindControl("txtquantity");
                    string qty = txtqty.Text;
                    TextBox txtrate = (TextBox)record.FindControl("txtrate");
                    string rate = txtrate.Text;
                    TextBox txtouramt = (TextBox)record.FindControl("txtourrate");
                    string ouramt = txtouramt.Text;
                    TextBox txtprwamt = (TextBox)record.FindControl("txtprwrate");
                    string prwamt = txtprwamt.Text;
                    TextBox txtamt = (TextBox)record.FindControl("txtamount");
                    string amt = txtamt.Text;
                    if (desc != "" || unit != "" || qty != "" || rate != "" || amt != "" || ouramt != "" || prwamt != "")
                    {
                        Descs = Descs + desc + ",";
                        Units = Units + unit + ",";
                        Qtys = Qtys + qty + ",";
                        Rates = Rates + rate + ",";
                        OurAmts = OurAmts + ouramt + ",";
                        PrwAmts = PrwAmts + prwamt + ",";
                        Amts = Amts + amt + ",";
                    }
                }
            }
            foreach (GridViewRow record in grdterms.Rows)
            {
                if ((record.FindControl("chkSelectterms") as CheckBox).Checked)
                {
                    TextBox txtterm = (TextBox)record.FindControl("txtterms");
                    string termdesc = txtterm.Text;
                    if (termdesc != "")
                    {
                        MDescs = MDescs + termdesc + "$";
                    }
                }
            }
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_ServiceProviderPO";
            cmd.Parameters.Add(new SqlParameter("@vendorcode", ddlVType.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@cccode", ddlcccode.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@dcacode", ddldca.SelectedItem.Text));
            cmd.Parameters.Add(new SqlParameter("@subdcacode", ddlsubdca.SelectedItem.Text));
            cmd.Parameters.Add(new SqlParameter("@podate", txtpodate.Text));
            cmd.Parameters.Add(new SqlParameter("@povalue", txttotalamt.Text));
            //cmd.Parameters.Add(new SqlParameter("@unit", txtunit.Text));
            //cmd.Parameters.Add(new SqlParameter("@rate", txtrate.Text));
            //cmd.Parameters.Add(new SqlParameter("@quantity", txtquantity.Text));
            cmd.Parameters.Add(new SqlParameter("@Terms", MDescs));
            cmd.Parameters.Add(new SqlParameter("@User", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@roles", Session["roles"].ToString()));

            cmd.Parameters.Add(new SqlParameter("@Descs", Descs));
            cmd.Parameters.Add(new SqlParameter("@Units", Units));
            cmd.Parameters.Add(new SqlParameter("@Qtys", Qtys));
            cmd.Parameters.Add(new SqlParameter("@Rates", Rates));
            cmd.Parameters.Add(new SqlParameter("@Amts", Amts));
            cmd.Parameters.Add(new SqlParameter("@OurRates", OurAmts));
            cmd.Parameters.Add(new SqlParameter("@PRWRates", PrwAmts));

            cmd.Parameters.Add(new SqlParameter("@pocompetiondate", txtcompdate.Text));
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "PO Inserted")
                JavaScript.UPAlertRedirect(Page, msg, "serviceproviderpo.aspx");
            else
                JavaScript.UPAlert(Page, msg);


            con.Close();
        }
        else
        {
            JavaScript.UPAlert(Page, "PO Amount Can not be Zero");
        }

    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("serviceproviderpo.aspx");
    }
    protected void BindGridview()
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add("rowid", typeof(int));
        dt.Columns.Add("chkSelect", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("OurRate", typeof(string));
        dt.Columns.Add("PRWRate", typeof(string));
        dt.Columns.Add("Rate", typeof(string));
        dt.Columns.Add("Amount", typeof(string));
        DataRow dr = dt.NewRow();
        //dr["rowid"] = 1;
        dr["chkSelect"] = string.Empty;
        dr["Description"] = string.Empty;
        dr["Unit"] = string.Empty;
        dr["Quantity"] = string.Empty;
        dr["OurRate"] = string.Empty;
        dr["PRWRate"] = string.Empty;
        dr["Rate"] = string.Empty;
        dr["Amount"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["Curtbl"] = dt;
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }
    private void AddNewRow()
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
                    TextBox txtitemdesc = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtitemdesc");
                    TextBox txtunit = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtunit");
                    TextBox txtquantity = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtquantity");
                    TextBox txtourrate = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("txtourrate");
                    TextBox txtpwrrate = (TextBox)gvDetails.Rows[rowIndex].Cells[6].FindControl("txtprwrate");
                    TextBox txtrate = (TextBox)gvDetails.Rows[rowIndex].Cells[7].FindControl("txtrate");
                    TextBox txtamount = (TextBox)gvDetails.Rows[rowIndex].Cells[8].FindControl("txtamount");
                    drCurrentRow = dt.NewRow();
                    //drCurrentRow["rowid"] = i + 1;
                    dt.Rows[i - 1]["Description"] = txtitemdesc.Text;
                    dt.Rows[i - 1]["Unit"] = txtunit.Text;
                    dt.Rows[i - 1]["Quantity"] = txtquantity.Text;
                    dt.Rows[i - 1]["OurRate"] = txtourrate.Text;
                    dt.Rows[i - 1]["PRWRate"] = txtpwrrate.Text;
                    dt.Rows[i - 1]["Rate"] = txtrate.Text;
                    dt.Rows[i - 1]["Amount"] = txtamount.Text;
                    rowIndex++;
                }
                dt.Rows.Add(drCurrentRow);
                ViewState["Curtbl"] = dt;
                gvDetails.DataSource = dt;
                gvDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState Value is Null");
        }
        SetOldData();
    }
    private void SetOldData()
    {
        int rowIndex = 0;
        if (ViewState["Curtbl"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbl"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtGName");
                    CheckBox chk = (CheckBox)gvDetails.Rows[rowIndex].Cells[0].FindControl("chkSelect");
                    TextBox txtitemdesc = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtitemdesc");
                    TextBox txtunit = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtunit");
                    TextBox txtquantity = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtquantity");
                    TextBox txtourrate = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("txtourrate");
                    TextBox txtpwrrate = (TextBox)gvDetails.Rows[rowIndex].Cells[6].FindControl("txtprwrate");
                    TextBox txtrate = (TextBox)gvDetails.Rows[rowIndex].Cells[7].FindControl("txtrate");
                    TextBox txtamount = (TextBox)gvDetails.Rows[rowIndex].Cells[8].FindControl("txtamount");
                    if (i < dt.Rows.Count - 1)
                    {
                        //chk.Checked = false;
                        txtitemdesc.Text = dt.Rows[i]["Description"].ToString();
                        txtunit.Text = dt.Rows[i]["Unit"].ToString();
                        txtquantity.Text = dt.Rows[i]["Quantity"].ToString();
                        txtourrate.Text = dt.Rows[i]["OurRate"].ToString();
                        txtpwrrate.Text = dt.Rows[i]["PRWRate"].ToString();
                        txtrate.Text = dt.Rows[i]["Rate"].ToString();
                        txtamount.Text = dt.Rows[i]["Amount"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
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
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();
                    for (int i = 0; i < gvDetails.Rows.Count - 1; i++)
                    {
                        gvDetails.Rows[i].Cells[1].Text = Convert.ToString(i + 1);
                    }
                    SetOldData();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
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

    #region Terms and Conditions start
    protected void BindGridviewterm()
    {
        DataTable dtt = new DataTable();
        dtt.Columns.Add("chkSelectterm", typeof(string));
        dtt.Columns.Add("Descriptionterm", typeof(string));      
        DataRow dr = dtt.NewRow();
        dr["chkSelectterm"] = string.Empty;
        dr["Descriptionterm"] = string.Empty;        
        dtt.Rows.Add(dr);
        ViewState["Curtblterm"] = dtt;
        grdterms.DataSource = dtt;
        grdterms.DataBind();
    }
    private void AddNewRowterm()
    {
        int rowIndex = 0;

        if (ViewState["Curtblterm"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterm"];
            DataRow drCurrentRow = null;
            if (dtt.Rows.Count > 0)
            {
                for (int i = 1; i <= dtt.Rows.Count; i++)
                {
                    TextBox txtterms = (TextBox)grdterms.Rows[rowIndex].Cells[2].FindControl("txtterms");                  
                    drCurrentRow = dtt.NewRow();
                    dtt.Rows[i - 1]["Descriptionterm"] = txtterms.Text;                   
                    rowIndex++;
                }
                dtt.Rows.Add(drCurrentRow);
                ViewState["Curtblterm"] = dtt;
                grdterms.DataSource = dtt;
                grdterms.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState Value is Null");
        }
        SetOldDataterm();
    }
    private void SetOldDataterm()
    {
        int rowIndex = 0;
        if (ViewState["Curtblterm"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterm"];
            if (dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)grdterms.Rows[rowIndex].Cells[0].FindControl("chkSelectterms");
                    TextBox txtterms = (TextBox)grdterms.Rows[rowIndex].Cells[2].FindControl("txtterms");
                    txtterms.Text = dtt.Rows[i]["Descriptionterm"].ToString();                   
                    rowIndex++;
                }
            }
        }
    }
    protected void btnAddterm_Click(object sender, EventArgs e)
    {
        AddNewRowterm();
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
    }
    protected void grdterms_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblterm"] != null)
            {
                DataTable dtt = (DataTable)ViewState["Curtblterm"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dtt.Rows.Count > 1)
                {
                    dtt.Rows.Remove(dtt.Rows[rowIndex]);
                    drCurrentRow = dtt.NewRow();
                    ViewState["Curtblterm"] = dtt;
                    grdterms.DataSource = dtt;
                    grdterms.DataBind();
                    for (int i = 0; i < grdterms.Rows.Count - 1; i++)
                    {
                        grdterms.Rows[i].Cells[1].Text = Convert.ToString(i + 1);
                    }
                    SetOldDataterm();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
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
    #endregion Terms and Conditions End
}
