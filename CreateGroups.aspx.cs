using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class CreateGroups : System.Web.UI.Page
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
            tbladdgroups.Visible = true;
            tblupdategroups.Visible = false;
            trbtnupdate.Visible = false;
        }
    }

    protected void BindGridview()
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add("rowid", typeof(int));
        dt.Columns.Add("GroupName", typeof(string));
        dt.Columns.Add("chklist", typeof(string));
        DataRow dr = dt.NewRow();
        //dr["rowid"] = 1;
        dr["GroupName"] = string.Empty;
        dr["chklist"] = string.Empty;
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
                    CheckBoxList chkl=(CheckBoxList)gvDetails.Rows[rowIndex].Cells[2].FindControl("chkProfitcalc");
                    drCurrentRow = dt.NewRow();
                    //drCurrentRow["rowid"] = i + 1;
                    dt.Rows[i - 1]["GroupName"] = txtname.Text;
                    dt.Rows[i - 1]["chklist"] = chkl.SelectedValue;
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
                    CheckBoxList chkl = (CheckBoxList)gvDetails.Rows[rowIndex].Cells[2].FindControl("chkProfitcalc");
                    txtname.Text = dt.Rows[i]["GroupName"].ToString();
                    chkl.SelectedValue= dt.Rows[i]["chklist"].ToString();
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
            string chkvalues = "";
            foreach (GridViewRow record in gvDetails.Rows)
            {
                TextBox txtgname = (TextBox)record.FindControl("txtGName");
                CheckBoxList chkprofitcal = (CheckBoxList)record.FindControl("chkProfitcalc");
                string name = txtgname.Text;
                string chkvalue = chkprofitcal.SelectedValue;
                if (name != "" && chkvalue != "")
                {
                    Gnames = Gnames + name + ",";
                    chkvalues = chkvalues + chkvalue + ",";
                }
                else
                {
                    JavaScript.UPAlert(Page, "Invalid. Please select a checkbox to continue with changes For Group Name  " + name);
                    break;
                }
            }
            cmd = new SqlCommand("sp_AddGroups", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Names", Gnames);
            cmd.Parameters.AddWithValue("@ProfitValues", chkvalues);
            cmd.Parameters.AddWithValue("@NGroupID", ddlnatureofgroup.SelectedValue);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@Type", "1");
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
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedIndex == 0)
        {
            tbladdgroups.Visible = true;
            tblupdategroups.Visible = false;
            CascadingDropDown2.SelectedValue = "Select Group Nature";
        }
        if (rbtntype.SelectedIndex == 1)
        {
            tbladdgroups.Visible = false;
            tblupdategroups.Visible = true;
            CascadingDropDown2.SelectedValue = "Select Group Nature";
        }

    }
    protected void ddlnatureofgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedIndex == 1)
        {
            da = new SqlDataAdapter("Select id,group_name from [MasterGroups] where NatureGroupId='" + ddlnatureofgroup.SelectedValue + "' and status='3'", con);
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                tblupdategroups.Visible = true;
                trbtnupdate.Visible = true;
                gvupdate.DataSource = ds.Tables["fill"];
                gvupdate.DataBind();
            }
            else
            {
                tblupdategroups.Visible = false;
                trbtnupdate.Visible = false;
                gvupdate.EmptyDataText = "No Data avaliable for the selection criteria";
                gvupdate.DataSource = null;
                gvupdate.DataBind();
            }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Gnames = "";
            foreach (GridViewRow record in gvupdate.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    TextBox txtgname = (TextBox)record.FindControl("txtGName");
                    string name = txtgname.Text;
                    if (name != "")
                    {
                        ids = ids + gvupdate.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        Gnames = Gnames + name + ",";
                    }
                }
            }
            if (ids != "")
            {
                cmd = new SqlCommand("sp_AddGroups", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ids", ids);
                cmd.Parameters.AddWithValue("@Names", Gnames);
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@Type", "2");
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "Voucher Inserted1";
                con.Close();
                if (msg == "Updated Successfully")
                    JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                else
                    JavaScript.UPAlert(Page, msg);
            }
            else
            {
                JavaScript.UPAlert(Page, "Please Verify CheckBox Which You Want To Update");
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
    protected void gvDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow gridRow in gvDetails.Rows)
        {
            CheckBoxList checkboxlist = (CheckBoxList)(gvDetails.Rows[gridRow.RowIndex].Cells[2].FindControl("chkProfitcalc"));

            checkboxlist.Attributes.Add("onclick", "radioMe(event,'" + checkboxlist.ClientID + "');");
        }
    }
}