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
using System.Web.Configuration;
using System.Data.SqlClient;



public partial class ViewWorkInProgress : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        if (!IsPostBack)
        {
            clientid();
            LoadYear();
        }
    }

    public void LoadYear()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Select Year");
    }
    public void clientid()
    {
        try
        {
            da = new SqlDataAdapter("select distinct client_id from work_progress where status='2' and client_id is not null", con);

            da.Fill(ds, "client");
            if (ds.Tables["client"].Rows.Count > 0)
            {
                ddlclientid.DataTextField = "client_id";
                ddlclientid.DataValueField = "client_id";
                ddlclientid.DataSource = ds.Tables["client"];
                ddlclientid.DataBind();
                ddlclientid.Items.Insert(0, "select");

            }
            else
            {
                ddlclientid.Items.Insert(0, "select");
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlcccode.Items.Clear();
            da = new SqlDataAdapter("select distinct i.cc_code, i.cc_code+ ',' +c.cc_name as ccname  from work_progress i join cost_center c on i.cc_code=c.cc_code where  subclient_id='" + ddlsubclientid.SelectedItem.Text + "' and i.status='2'", con);

            da.Fill(ds, "cccode");
            if (ds.Tables["cccode"].Rows.Count > 0)
            {
                ddlcccode.DataTextField = "ccname";
                ddlcccode.DataValueField = "cc_code";
                ddlcccode.DataSource = ds.Tables["cccode"];
                ddlcccode.DataBind();
                ddlcccode.Items.Insert(0, "select");

            }
            else
            {
                ddlcccode.Items.Insert(0, "select");
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
   

    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
             ddlsubclientid.Items.Clear();
             ddlcccode.Items.Clear();
            da = new SqlDataAdapter("select distinct subclient_id from work_progress where  client_id='" + ddlclientid.SelectedItem.Text + "' and status='2'", con);

            da.Fill(ds, "subclient");
            if (ds.Tables["subclient"].Rows.Count > 0)
            {
                ddlsubclientid.DataTextField = "subclient_id";
                ddlsubclientid.DataValueField = "subclient_id";
                ddlsubclientid.DataSource = ds.Tables["subclient"];
                ddlsubclientid.DataBind();
               
                ddlsubclientid.Items.Insert(0, "select");
            }
            else
            {
               
                ddlsubclientid.Items.Insert(0, "select");
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {

        try
        {
            string condition = "";
            if (ddlyear.SelectedIndex != 0)
            {

                condition = condition + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

            }

            if (ddlclientid.SelectedItem.Text != "select")
            {
                condition = condition + " and client_id='" +ddlclientid.SelectedItem.Text + "'";

            }
            if (ddlsubclientid.SelectedItem.Text != "select")
            {
                condition = condition + " and subclient_id='" + ddlsubclientid.SelectedItem.Text + "'";

            }
            if (ddlcccode.SelectedValue != "")
            {
                condition = condition + " and cc_code='" + ddlcccode.SelectedValue + "'";

            }


            da = new SqlDataAdapter("Select client_id,subclient_id,cc_code,pono,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-') as [Date],description,amount from [work_progress] where id>0 and status='2' "+condition+" ", con);
            da.Fill(ds, "InProgress");
            GridView1.DataSource = ds.Tables["InProgress"];
            GridView1.DataBind();
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);
        }
    }
    private decimal Amount = (decimal)0.0;
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkInProgress.aspx");
    }
}
