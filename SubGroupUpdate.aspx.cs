using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class SubGroupUpdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn.Visible = false;
        }

    }
    protected void ddlsubgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsubgroup.SelectedItem.Text != "")
        {
            da = new SqlDataAdapter("select sg.id,ng.NatureGroupName,mg.Group_Name,sg.Name from NatureOfGroup ng join MasterGroups mg on ng.NatureGroupId=mg.NatureGroupId join sub_Group sg on mg.Group_Id=sg.group_id where sg.status='3' and sg.Name='"+ddlsubgroup.SelectedItem.Text+"'", con);
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                gvsubgroups.DataSource = ds.Tables["fill"];
                gvsubgroups.DataBind();
                btn.Visible = true;
            }
        }
        else
        {
            btn.Visible = false;
            gvsubgroups.DataSource = null;
            gvsubgroups.DataBind();
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string name = "";
            string id = "";
            foreach (GridViewRow record in gvsubgroups.Rows)
            {
                TextBox txtgname = (TextBox)record.FindControl("txtSName");
                name = txtgname.Text;
                id = gvsubgroups.DataKeys[record.RowIndex]["id"].ToString();
               
            }
            cmd = new SqlCommand("sp_UpdateSubGroups", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Names", name);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "Voucher Inserted1";
            con.Close();
            if (msg == "Successfully Updated")
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