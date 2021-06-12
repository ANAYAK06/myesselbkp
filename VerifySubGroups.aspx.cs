using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class VerifySubGroups : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            fillgrid();
            if (Session["roles"].ToString() == "HoAdmin")
            {
                lblheader.Text = "Verify Sub-Groups";
                btnAssign.Text = "Verify";
            }
            else
            {
                lblheader.Text = "Approve Sub-Groups";
                btnAssign.Text = "Approve";
            }
        }
    }

    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select sg.id,ng.NatureGroupName,mg.Group_Name,sg.Name,sg.subgroup_type from NatureOfGroup ng join MasterGroups mg on ng.NatureGroupId=mg.NatureGroupId join sub_Group sg on mg.Group_Id=sg.group_id where sg.status='1'", con);
            else
                da = new SqlDataAdapter("select sg.id,ng.NatureGroupName,mg.Group_Name,sg.Name,sg.subgroup_type from NatureOfGroup ng join MasterGroups mg on ng.NatureGroupId=mg.NatureGroupId join sub_Group sg on mg.Group_Id=sg.group_id where sg.status='2'", con);

            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                gvsubgroups.DataSource = ds.Tables["fill"];
                gvsubgroups.DataBind();
            }
            else
            {
                btn.Visible = false;
                gvsubgroups.EmptyDataText = "No Data avaliable";
                gvsubgroups.DataSource = null;
                gvsubgroups.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void gvsubgroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string cellval = gvsubgroups.DataKeys[e.RowIndex]["subgroup_type"].ToString();
            if (cellval == "New")
            {
                cmd.Connection = con;
                cmd = new SqlCommand("sp_SubGroupDeletion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", gvsubgroups.DataKeys[e.RowIndex]["id"].ToString());
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
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
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            foreach (GridViewRow record in gvsubgroups.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    ids = ids + gvsubgroups.DataKeys[record.RowIndex]["id"].ToString() + ",";
                }
            }
            cmd = new SqlCommand("sp_VerifySubGroups", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ids", ids);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
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
}