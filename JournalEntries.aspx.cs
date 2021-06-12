using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class JournalEntries : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridview();

        }
    }
    private void FillDropDownList(DropDownList ddl)
    {
        da = new SqlDataAdapter("select id,name from ledger where status='3'", con);
        da.Fill(ds, "ldrnames");
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddl.DataSource = dt;
        ddl.DataBind();
        ddl.DataTextField = "name";
        ddl.DataValueField = "id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select Ledger Name", "0"));
    }
    protected void BindGridview()
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add("rowid", typeof(int));
        dt.Columns.Add("LedgerName", typeof(string));
        dt.Columns.Add("ledgertype", typeof(string));
        dt.Columns.Add("LedgerAmount", typeof(string));       
        DataRow dr = dt.NewRow();
        //dr["rowid"] = 1;
        dr["LedgerName"] = string.Empty;
        dr["ledgertype"] = string.Empty;
        dr["LedgerAmount"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["Curtbl"] = dt;
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
        DropDownList lednames = (DropDownList)gvDetails.Rows[0].Cells[1].FindControl("ddlledgername");
        FillDropDownList(lednames);
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
                    DropDownList ddlledname = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("ddlledgername");
                    DropDownList dllledtype = (DropDownList)gvDetails.Rows[rowIndex].Cells[2].FindControl("ddlledgertype");
                    TextBox txtamt = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtAmt");                    
                    drCurrentRow = dt.NewRow();
                    //drCurrentRow["rowid"] = i + 1;
                    dt.Rows[i - 1]["LedgerName"] = ddlledname.SelectedValue;
                    dt.Rows[i - 1]["ledgertype"] = dllledtype.SelectedValue;
                    dt.Rows[i - 1]["LedgerAmount"] = txtamt.Text;
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
                    DropDownList ddlledname = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("ddlledgername");
                    FillDropDownList(ddlledname);
                    DropDownList dllledtype = (DropDownList)gvDetails.Rows[rowIndex].Cells[2].FindControl("ddlledgertype");
                    TextBox txtamt = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtAmt");
                    ddlledname.SelectedValue = dt.Rows[i]["LedgerName"].ToString();
                    dllledtype.SelectedValue = dt.Rows[i]["ledgertype"].ToString();
                    txtamt.Text = dt.Rows[i]["LedgerAmount"].ToString();
                    rowIndex++;
                    
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


        }
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
                        gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetOldData();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:calculateamt()", true);
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
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblcreditamt.Text == lbldebitamt.Text)
            {
                string Lids = "";
                string LTypes = "";
                string Lamts = "";
                foreach (GridViewRow record in gvDetails.Rows)
                {
                    DropDownList ledids = (DropDownList)record.FindControl("ddlledgername");
                    DropDownList ledtypes = (DropDownList)record.FindControl("ddlledgertype");
                    TextBox txtamts = (TextBox)record.FindControl("txtAmt");
                    string Lid = ledids.Text;
                    string Ltype = ledtypes.Text;
                    string amt =txtamts.Text;
                    if (Lid != "" && Ltype != "" && amt != "")
                    {
                        Lids = Lids + Lid + ",";
                        LTypes = LTypes + Ltype + ",";
                        Lamts = Lamts + amt + ",";
                    }
                    
                }
                cmd = new SqlCommand("sp_createjournal", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LIds", Lids);
                cmd.Parameters.AddWithValue("@LTypes", LTypes);
                cmd.Parameters.AddWithValue("@LAmts", Lamts);
                cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                cmd.Parameters.AddWithValue("@Narration", txtdesc.Text);
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "Voucher Inserted1";
                con.Close();
                if (msg == "Successfull")
                    JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                else
                    JavaScript.UPAlert(Page, msg);

            }
            else
            {
                JavaScript.UPAlert(Page, "Credit and Debit Amount are not matching");
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
}