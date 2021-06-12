using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class VerifyJournalEntries : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
        {
            lblheading.Text = "Verify Journal Entries";
            btnapprove.Text = "Verify";
            //lblheader.Text = "Verify Journals";
            Title = "Verify Journal";
        }
        else
        {
            lblheading.Text = "Approve Journal Entries";
            //lblheader.Text = "Approve Journals";
            btnapprove.Text = "Approve";
            Title = "Approve Journal";
        }
        if (!IsPostBack)
        {
            fillgrid();
            tblverifyjornals.Visible = false;
            //checkcashledger();
        }
    }

    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select transaction_id,sum(isnull(credit,0))as credit,sum(isnull(debit,0))as debit,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date from Journal_Entries where Status ='1'  GROUP by transaction_id,date ", con);
            else
                da = new SqlDataAdapter("select transaction_id,sum(isnull(credit,0))as credit,sum(isnull(debit,0))as debit,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date from Journal_Entries where Status ='2'  GROUP by transaction_id,date ", con);

            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                gvjournals.DataSource = ds.Tables["fill"];
                gvjournals.DataBind();
            }
            else
            {
                gvjournals.EmptyDataText = "No Data avaliable for the selection criteria";
                gvjournals.DataSource = null;
                gvjournals.DataBind();
                //btn.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void gvjournals_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            da = new SqlDataAdapter("sp_viewjournals", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@jvno", SqlDbType.VarChar).Value = gvjournals.SelectedValue.ToString();
            da.SelectCommand.Parameters.AddWithValue("@roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
            da.Fill(ds, "FillDetails");
            if (ds.Tables["FillDetails"].Rows.Count > 0)
            {
                ViewState["Journalid"]= gvjournals.SelectedValue.ToString();
                lblheader.Text = gvjournals.SelectedValue.ToString();
                tblverifyjornals.Visible = true;
                gvjournalapproval.DataSource = ds.Tables["FillDetails"];
                gvjournalapproval.DataBind();
                tblbtnupdate.Visible = true;
            }
            else
            {
                ViewState["Journalid"] = null;
                tblverifyjornals.Visible = false;
                gvjournalapproval.DataSource = null;
                gvjournalapproval.DataBind();
                tblbtnupdate.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    decimal credit = 0;
    decimal debit = 0;
    protected void gvjournalapproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            debit += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Debit"));
            credit += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Credit"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = String.Format("{0:n}", debit);
            e.Row.Cells[4].Text = String.Format("{0:n}", credit);
            ViewState["Debit"] = e.Row.Cells[3].Text;
            ViewState["Credit"] = e.Row.Cells[4].Text;
            if (ViewState["Credit"].ToString() == ViewState["Debit"].ToString())
            {
                e.Row.Cells[3].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.White;

            }
           else
            {
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Journalid"] != null)
            {
                if (ViewState["Credit"].ToString() == ViewState["Debit"].ToString())
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    if (Session["roles"].ToString() == "HoAdmin")
                        cmd.CommandText = "Update Journal_Entries set status='2',Modified_by='" + Session["user"].ToString() + "',Modified_Date=getdate() where transaction_id='" + ViewState["Journalid"].ToString() + "' and status='1'";
                    else
                        cmd.CommandText = "Update Journal_Entries set status='3',Modified_by='" + Session["user"].ToString() + "',Modified_Date=getdate() where transaction_id='" + ViewState["Journalid"].ToString() + "' and status='2'";

                    con.Open();
                    int i = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();
                    if (i > 0)
                    {
                        if (Session["roles"].ToString() == "HoAdmin")
                            JavaScript.UPAlertRedirect(Page, "Verified", Request.Url.ToString());
                        else
                            JavaScript.UPAlertRedirect(Page, "Approved", Request.Url.ToString());
                    }
                }
                else
                {
                    JavaScript.UPAlert(Page,"Something Went wrong please reject the JV");
                }
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, "Failed", Request.Url.ToString());
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
    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Journalid"] != null)
            {
                cmd = new SqlCommand();
                cmd.Connection = con;
                if (Session["roles"].ToString() == "HoAdmin")
                    cmd.CommandText = "Update Journal_Entries set status='Rejected',Modified_by='" + Session["user"].ToString() + "',Modified_Date=getdate() where transaction_id='" + ViewState["Journalid"].ToString() + "' and status='1'";
                else
                    cmd.CommandText = "Update Journal_Entries set status='Rejected',Modified_by='" + Session["user"].ToString() + "',Modified_Date=getdate() where transaction_id='" + ViewState["Journalid"].ToString() + "' and status='2'";

                con.Open();
                int i = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
                if (i > 0)
                {
                    JavaScript.UPAlertRedirect(Page, "Rejected", Request.Url.ToString());
                }
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, "Failed", Request.Url.ToString());
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