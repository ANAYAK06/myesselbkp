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
using AjaxControlToolkit;
using System.Web.Services;
public partial class ViewTaskTime : System.Web.UI.Page
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
            
            SupportUser();
            no();
            LoadYear();
           
        }

    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        try
        {
            string condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    condition = condition + " and datepart(mm,date)=" + ddlMonth.SelectedValue + " and datepart(yy,date)=" + yy;

                }
                else
                {
                    condition = condition + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                }

            }
            if (ddlnames.SelectedItem.Text != "Select All")
            {
                condition = condition + " and SupportUser='" + ddlnames.SelectedItem.Text + "'";
            }
            if (ddlno.SelectedItem.Text != "Select All")
            {
                condition = condition + " and No='" + ddlno.SelectedValue + "'";
            }
            ViewState["condition"] = condition;
            s1 = ViewState["condition"].ToString();
            fillgrid();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillgrid()
    {
        da = new SqlDataAdapter("select no,sum(time)as Time,supportuser,type from  tasktime where id>0  " + s1 + "  group by no,supportuser,type   ", con);
        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    public void SupportUser()
    {
        try
        {
            ddlnames.Items.Clear();
            da = new SqlDataAdapter("select username from itmsregister where user_type='SupportUser' order by username ", con);
            da.Fill(ds, "SupportUser");
            ddlnames.DataTextField = "username";
            ddlnames.DataValueField = "username";
            ddlnames.DataSource = ds.Tables["SupportUser"];
            ddlnames.DataBind();
            ddlnames.Items.Insert(0, "SupportUser");

            ddlno.Items.Insert(0, "Select");
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void no()
    {
        try
        {
            ds.Clear();
            ddlno.Items.Clear();
            da = new SqlDataAdapter("select distinct e.cr_issueno  from EstimatedHours e join TaskTime t on t.no=e.cr_issueno where t.supportuser='" + ddlnames.SelectedItem.Text + "' and e.status >='10' order by e.cr_issueno", con);
            da.Fill(ds, "number");
            ddlno.DataTextField = "cr_issueno";
            ddlno.DataValueField = "cr_issueno";
            ddlno.DataSource = ds.Tables["number"];
            ddlno.DataBind();
            ddlno.Items.Insert(0, "Select");
            ddlno.Items.Insert(1, "Select All");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlnames_SelectedIndexChanged(object sender, EventArgs e)
    {
        no();
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    public void LoadYear()
    {
        for (int i = 2005; i < System.DateTime.Now.Year + 1; i++)
        {
            ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
        }
        ddlyear.Items.Insert(0, "Select Year");
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;

            string behaviorID = "indus_" + e.Row.RowIndex;
            pce.BehaviorID = behaviorID;

            Label lblno = (Label)e.Row.FindControl("lblno");

            string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
            string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

            lblno.Attributes.Add("onmouseover", OnMouseOverScript);
            lblno.Attributes.Add("onmouseout", OnMouseOutScript);
        }
    }
    [WebMethod]
    public static string GetDynamicContent(string contextKey)
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        //  string constr = "Server=TestServer;Database=SampleDatabase;uid=test;pwd=test;";
        string query = "select no,time,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,supportuser from  tasktime where no = '" + contextKey + "'";

        SqlDataAdapter da = new SqlDataAdapter(query, constr);
        DataTable table = new DataTable();

        da.Fill(table);

        StringBuilder b = new StringBuilder();

        b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ");
        b.Append("width:700px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
        b.Append("<tr><td colspan='4' style='background-color:#336699; color:White;'>");
        b.Append("<b>Details</b>"); b.Append("</td></tr>");
        b.Append("<tr style=''><td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>NO</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>Time</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>Date</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;'><b>supportuser</b></td></tr>");
        b.Append("<tr>");
        for (int i = 0; i < table.Rows.Count; i++)
        {
          
            b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[i]["no"].ToString() + "</td>");
            b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[i]["time"].ToString() + "</td>");
            b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[i]["date"].ToString() + "</td>");
            b.Append("<td>" + table.Rows[i]["supportuser"].ToString() + "</td>");
            b.Append("</tr>");
        }
        b.Append("</table>");



        return b.ToString();
    }
   
}
