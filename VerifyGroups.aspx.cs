using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class VerifyGroups : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin")
        {
            lblheader.Text = "Verify Group";
            btnAssign.Text = "Verify";
        }
        else
        {
            lblheader.Text = "Approve Group";
            btnAssign.Text = "Approve";
        }
        if (!IsPostBack)
        {
            fillgrid();
        }
    }

    protected void gvgroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string cellval = gvgroups.DataKeys[e.RowIndex]["group_type"].ToString();
            if (cellval == "New")
            {
                cmd.Connection = con;
                cmd = new SqlCommand("sp_GroupDeletion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", gvgroups.DataKeys[e.RowIndex]["id"].ToString());
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "";
                if (msg == "Rejected")
                    JavaScript.UPAlert(Page, msg);
                else
                    JavaScript.UPAlert(Page, msg);
                con.Close();
                fillgrid();
            }
            else
            {
                JavaScript.UPAlert(Page, "You Cant Reject because this was Old Group");
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select mg.id,ng.NatureGroupName,mg.Group_Name,mg.group_type,mg.GrossProfitCalc from NatureOfGroup ng join MasterGroups mg on ng.NatureGroupId=mg.NatureGroupId where mg.status='1'", con);
            else
                da = new SqlDataAdapter("select mg.id,ng.NatureGroupName,mg.Group_Name,mg.group_type,mg.GrossProfitCalc from NatureOfGroup ng join MasterGroups mg on ng.NatureGroupId=mg.NatureGroupId where mg.status='2'", con);

            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                gvgroups.DataSource = ds.Tables["fill"];
                gvgroups.DataBind();
            }
            else
            {
                gvgroups.EmptyDataText = "No Data avaliable for the selection criteria";
                gvgroups.DataSource = null;
                gvgroups.DataBind();
                btn.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            foreach (GridViewRow record in gvgroups.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    ids = ids + gvgroups.DataKeys[record.RowIndex]["id"].ToString() + ",";
                }
            }
            cmd = new SqlCommand("sp_AddGroups", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ids", ids);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "Inserted1";
            con.Close();
            if (msg == "Verified Successfully" || msg == "Approved Successfully")
                JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
            else
                JavaScript.UPAlert(Page, msg);

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
    protected void gvgroups_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[6].Text == "Y")
            {
                e.Row.Cells[6].Text = "Yes";
            }
            else
            {
                e.Row.Cells[6].Text = "No";
            }

        }
    }
}