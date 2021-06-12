using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class AddSubGroups : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //tbladdsubgroups.Visible = true;
            //btn.Visible = true;
            //tblupdatesubgroups.Visible = false;
            //btnupdate.Visible = false;
            BindGridview();
        }
    }

    protected void BindGridview()
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add("rowid", typeof(int));
        dt.Columns.Add("GroupName", typeof(string));
        DataRow dr = dt.NewRow();
        //dr["rowid"] = 1;
        dr["GroupName"] = string.Empty;
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
                    TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtGName");
                    drCurrentRow = dt.NewRow();
                    //drCurrentRow["rowid"] = i + 1;
                    dt.Rows[i - 1]["GroupName"] = txtname.Text;
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
                    TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtGName");
                    txtname.Text = dt.Rows[i]["GroupName"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
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
            string Gnames = "";
            foreach (GridViewRow record in gvDetails.Rows)
            {
                TextBox txtgname = (TextBox)record.FindControl("txtGName");
                string name = txtgname.Text;
                if (name != "")
                {
                    Gnames = Gnames + name + ",";
                }
            }
            cmd = new SqlCommand("sp_AddSubGroups", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Names", Gnames);
            if (rbtnsubgroups.SelectedIndex == 0)
            {
                cmd.Parameters.AddWithValue("@GroupID", ddlgroups.SelectedValue);
                cmd.Parameters.AddWithValue("@Type", "1");
            }
            if (rbtnsubgroups.SelectedIndex == 1)
            {
                cmd.Parameters.AddWithValue("@ChildGroupID", ddlchildgroup.SelectedValue);
                cmd.Parameters.AddWithValue("@Type", "2");
            }

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