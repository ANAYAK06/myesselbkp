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
using System.Data.Odbc;
using System.IO;

public partial class ITDepPercentage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadYear();
            btnSave.Visible = false;
        }
    }
    public void LoadYear()
    {
        for (int i = 2010; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string subdca = "";
            string percentage = "";
            foreach (GridViewRow record in GridView1.Rows)
            {
                if ((record.FindControl("percentage") as TextBox).Text != "")
                {
                    subdca = subdca + (record.FindControl("subdca") as Label).Text + ",";
                    percentage = percentage + (record.FindControl("percentage") as TextBox).Text + ",";
                }
            }
            if (subdca != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ITDepper_sp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FYear", ddlyear.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@type", "Set");
                cmd.Parameters.AddWithValue("@subdcas", subdca);
                cmd.Parameters.AddWithValue("@percentages", percentage);
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();              
                if (msg == "Sucessfull")
                    JavaScript.UPAlertRedirect(Page, msg, "ITDepPercentage.aspx");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlyear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        var pyear = ddlyear.SelectedItem.Text;
        var listSplit = pyear.Split('-');
        var year1 = int.Parse(listSplit[0]);
        var year2 = int.Parse(listSplit[1]);
        string prevyear = Convert.ToString(((year1) - 1) + "-" + ((year2) - 1)).ToString();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "ITDepper_sp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FYear", ddlyear.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@PrevYear", prevyear.ToString());
        cmd.Parameters.AddWithValue("@type", "Get");
        cmd.Connection = con;
        con.Open();
        da.SelectCommand = cmd;
        da.Fill(ds,"ITDep");
        if (ds.Tables["ITDep"].Rows.Count > 0)
        {
            if (ds.Tables["ITDep"].Rows[0]["type"].ToString() == "2")
                btnSave.Visible = true;
            else
                btnSave.Visible = false;
            GridView1.DataSource = ds;
            GridView1.DataBind();
           
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    foreach (GridViewRow gvr in GridView1.Rows)
        //    {
        //        if (((TextBox)gvr.FindControl("percentage")).Text == "")
        //        {
        //            ((TextBox)gvr.FindControl("percentage")).Enabled = true;
        //        }
        //        else
        //            ((TextBox)gvr.FindControl("percentage")).Enabled = false;
        //    }
        //}
    }
}