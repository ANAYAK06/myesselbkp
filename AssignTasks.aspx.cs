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
using System.IO;
using System.Text;

public partial class AssignTasks : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    string s1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            statusname();
            LoadYear();
            SupportUser();
        }

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }
    public void LoadYear()
    {
        for (int i = 2005; i < System.DateTime.Now.Year + 1; i++)
        {
            ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
        }
        ddlyear.Items.Insert(0, "Select Year");
    }
    public void statusname()
    {
        try
        {
            ddlstatus.Items.Clear();
            da = new SqlDataAdapter("select status,status_name from esselcr_status where status in('4','10') order by status_name", con);
            da.Fill(ds, "fillstatus");
            ddlstatus.DataTextField = "status_name";
            ddlstatus.DataValueField = "status";
            ddlstatus.DataSource = ds.Tables["fillstatus"];
            ddlstatus.DataBind();
            ddlstatus.Items.Insert(0, "Status");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddltype.SelectedItem.Text == "CR")
        //{
        //    da = new SqlDataAdapter("select c.crno as no,REPLACE(CONVERT(VARCHAR(11),e.Date, 106), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.comment from esselcr c join  EstimatedHours e on e.cr_issueno=c.crno join esselcr_status s on s.status=e.status where s.status_name='" + ddlstatus.SelectedItem.Text + "'", con);
        //}
        //else if (ddltype.SelectedItem.Text == "ISSUE")
        //{
        //    da = new SqlDataAdapter("select c.issueno as no,REPLACE(CONVERT(VARCHAR(11),e.Date, 106), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.comment from esselissue c join  EstimatedHours e on e.cr_issueno=c.issueno join esselcr_status s on s.status=e.status where s.status_name='" + ddlstatus.SelectedItem.Text + "'", con);
        //}
        //        da.Fill(ds, "fillno");
        //        ddlstatus.DataTextField = "no";
        //        ddlstatus.DataValueField = "no";
        //        ddlstatus.DataSource = ds.Tables["fillno"];
        //        ddlstatus.DataBind();
        //        ddlstatus.Items.Insert(0, "Select");
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    condition = condition + " and datepart(mm,a.date)=" + ddlMonth.SelectedValue + " and datepart(yy,a.date)=" + yy;

                }
                else
                {
                    condition = condition + " and convert(datetime,a.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                }
               
            }
            if (ddlstatus.SelectedItem.Text != "Select")
            {
                condition = condition + " and status_name='" + ddlstatus.SelectedItem.Text + "'";
            }
             ViewState["condition"] = condition;
             s1 = ViewState["condition"].ToString();
             fillgrid(s1);
        }
        catch(Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void fillgrid(string s1)
    {
        this.upgrid.Update();
        try
        {
            if (ddltype.SelectedItem.Text == "CR")
            {
                if (ddlstatus.SelectedItem.Text == "Approved For Design")
                {
                    da = new SqlDataAdapter("select c.crno as no,c.crtype,e.AssignTo,REPLACE(CONVERT(VARCHAR(20),e.Date, 100), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.comment from esselcr c join  EstimatedHours e on e.cr_issueno=c.crno join esselcr_status s on s.status=e.status  where c.id>0  " + ViewState["condition"].ToString() + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "AssignTo")
                {
                    da = new SqlDataAdapter("select c.crno as no,c.crtype,e.AssignTo,REPLACE(CONVERT(VARCHAR(20),e.AssignDate, 100), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.remarks comment from esselcr c join  EstimatedHours e on e.cr_issueno=c.crno join esselcr_status s on s.status=e.status  where c.id>0  " + ViewState["condition"].ToString() + "  ", con);
                }
                else
                {
                    da = new SqlDataAdapter("select c.crno as no,c.crtype,e.AssignTo,REPLACE(CONVERT(VARCHAR(20),e.Date, 100), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.comment from esselcr c join  EstimatedHours e on e.cr_issueno=c.crno join esselcr_status s on s.status=e.status  where c.id>0  " + ViewState["condition"].ToString() + " ", con);
                }
            }
            else if (ddltype.SelectedItem.Text == "ISSUE")
            {
                if (ddlstatus.SelectedItem.Text == "Approved For Design")
                {
                    da = new SqlDataAdapter("select c.issueno as no,c.crtype,e.AssignTo,REPLACE(CONVERT(VARCHAR(20),e.Date, 100), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.comment from esselissue c join  EstimatedHours e on e.cr_issueno=c.issueno join esselcr_status s on s.status=e.status  where c.id>0  " + ViewState["condition"].ToString() + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "AssignTo")
                {
                    da = new SqlDataAdapter("select c.issueno as no,c.crtype,e.AssignTo,REPLACE(CONVERT(VARCHAR(20),e.AssignDate, 100), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.remarks comment from esselissue c join  EstimatedHours e on e.cr_issueno=c.issueno join esselcr_status s on s.status=e.status  where c.id>0  " + ViewState["condition"].ToString() + " ", con);
                }
                else
                {
                    da = new SqlDataAdapter("select c.issueno as no,c.crtype,e.AssignTo,REPLACE(CONVERT(VARCHAR(20),e.Date, 106), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.comment from esselissue c join  EstimatedHours e on e.cr_issueno=c.issueno join esselcr_status s on s.status=e.status  where c.id>0  " + ViewState["condition"].ToString() + " ", con);
                }
            }
            da.Fill(ds, "fill");
            //ViewState["no"] = ds.Tables["fill"].Rows[0]["no"].ToString();
           
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        ViewState["id"] = GridView1.Rows[e.NewEditIndex].Cells[0].Text;
        poppo.Show();
        SupportUser();
        fillpopup();
    }
    public void fillpopup()
    {
        DataSet ds2 = new DataSet();
        ds2.Clear();
        txtremarks.Text = "";
        if (ddltype.SelectedItem.Text == "CR")
        {
            da = new SqlDataAdapter("select c.crno as no,c.crtype,REPLACE(CONVERT(VARCHAR(20),e.Estmationverify_date, 100), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.comment from esselcr c join  EstimatedHours e on e.cr_issueno=c.crno join esselcr_status s on s.status=e.status  where  c.crno='" + ViewState["id"].ToString()+ "' " + ViewState["condition"].ToString() + " ", con);
        }
        else if (ddltype.SelectedItem.Text == "ISSUE")
        {
            da = new SqlDataAdapter("select c.issueno as no,c.crtype,REPLACE(CONVERT(VARCHAR(20),e.Estmationverify_date, 100), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,c.description,e.comment from esselissue c join  EstimatedHours e on e.cr_issueno=c.issueno join esselcr_status s on s.status=e.status  where c.issueno='" + ViewState["id"].ToString() + "' " + ViewState["condition"].ToString() + " ", con);
        }
        da.Fill(ds2, "fillpopup");
        if (ds2.Tables["fillpopup"].Rows.Count > 0)
        {
            lbleno.Text = ds2.Tables["fillpopup"].Rows[0]["no"].ToString();
            lbletype.Text = ds2.Tables["fillpopup"].Rows[0]["crtype"].ToString();
            lblbrd.Text = ds2.Tables["fillpopup"].Rows[0]["brdtime"].ToString();
            lbltd.Text = ds2.Tables["fillpopup"].Rows[0]["tdtime"].ToString();
            lbldevelopmenttime.Text = ds2.Tables["fillpopup"].Rows[0]["developmenttime"].ToString();
        }
    }
    public void SupportUser()
    {
        ddlnames.Items.Clear();
        da = new SqlDataAdapter("select username from itmsregister where user_type='SupportUser' order by username ", con);
        da.Fill(ds, "SupportUser");
        ddlnames.DataTextField = "username";
        ddlnames.DataValueField = "username";
        ddlnames.DataSource = ds.Tables["SupportUser"];
        ddlnames.DataBind();
       // ViewState["SupportUser"] = ds.Tables["SupportUser"].Rows[0]["username"].ToString();
        ddlnames.Items.Insert(0, "Select");
    }
    protected void btnassign_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("sp_AssignTo", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@no", lbleno.Text);
        cmd.Parameters.AddWithValue("@username", ddlnames.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@remarks", txtremarks.Text);
        cmd.Connection = con;
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        if (msg == "Assigned Sucessfully")
        {
            poppo.Hide();
            JavaScript.UPAlert(Page, msg);
        }
        else
        {
            poppo.Hide();

            JavaScript.UPAlert(Page, msg);
        }

        fillgrid(s1);

        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["roles"].ToString() == "SuperAdmin")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlstatus.SelectedItem.Text == "Approved For Design" || ddlstatus.SelectedItem.Text == "AssignTo")
                {
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[2].Visible = false;

                }
              
                else
                {
                    GridView1.Columns[2].Visible = true;
                    GridView1.Columns[8].Visible = false;
                }
            }
        }
    }
}

 //CHECKS WHETHER EMAILID IS ENTERED OR NOT 
