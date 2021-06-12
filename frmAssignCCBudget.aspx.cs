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
using System.Web.Services;

public partial class frmAssignCCBudget : System.Web.UI.Page
{
   
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            trtype.Visible = false;
            trccode.Visible = false;
            year.Visible = false;
            trbudget.Visible = false;
            trbutton.Visible = false;
        }

       //esselDal RoleCheck = new esselDal();
       // int rec = 0;
       //  rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 33);
       // if (rec == 0)
       //     Response.Redirect("Menucontents.aspx");  
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    public void clear()
    {
        //CascadingDropDown2.SelectedValue = "";
        CascadingDropDown4.SelectedValue = "";
        txtBudget.Text = "";
        lblAlert.Text = "";
        ddlcctype.SelectedIndex = 0;
        ddltype.SelectedIndex = 0;
        ddlCCcode.SelectedItem.Text = "Select Cost Center";

    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        DateTime mydate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
        cmd = new SqlCommand("sp_Insert_CCBudget", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CCCode", ddlCCcode.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
        cmd.Parameters.AddWithValue("@Amount", txtBudget.Text);
        cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
        cmd.Parameters.AddWithValue("@Date", mydate.ToString());
        con.Open();
        try
        {
            JavaScript.UPAlertRedirect(Page, cmd.ExecuteScalar().ToString(), Request.Url.ToString());
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
        finally
        {
            con.Close();
        }
    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcctype.SelectedItem.Text == "Select")
        {
            trtype.Visible = false;
            trccode.Visible = false;
            year.Visible = false;
            trbudget.Visible = false;
            trbutton.Visible = false;
            clear();
        }
        else if (ddlcctype.SelectedItem.Text == "Performing")
        {
            trtype.Visible = true;
            trccode.Visible = false;
            year.Visible = false;
            trbudget.Visible = false;
            trbutton.Visible = false;
           
        }
        else if (ddlcctype.SelectedItem.Text == "Non-Performing")
        {
            trtype.Visible = false;
            trccode.Visible = true;
            year.Visible = true;
            trbudget.Visible = true;
            trbutton.Visible = true;
            da = new SqlDataAdapter("select  cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_type in ('Non-Performing')  and status in ('Old','New')", con);
            da.Fill(ds, "CC");
            ddlCCcode.DataTextField = "Name";
            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataSource = ds.Tables["CC"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
        }
        else if (ddlcctype.SelectedItem.Text == "Capital")
        {
            trtype.Visible = false;
            trccode.Visible = true;
            year.Visible = true;
            trbudget.Visible = true;
            trbutton.Visible = true;
            da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_type in ('Capital')  and status in ('Old','New')", con);
            da.Fill(ds, "CC");
            ddlCCcode.DataTextField = "Name";
            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataSource = ds.Tables["CC"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
        }
      


    }
  
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedItem.Text != "Select")
        {
            if (ddltype.SelectedItem.Text == "Service")
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_code  not in (select cc_code from budget_cc) and cc_type='Performing' and status in ('Old','New') and cc_subtype='Service'", con);
            else if (ddltype.SelectedItem.Text == "Trading")
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_code  not in (select cc_code from budget_cc) and cc_type='Performing' and status in ('Old','New') and cc_subtype='Trading'", con);
            else if (ddltype.SelectedItem.Text == "Manufacturing")
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_code  not in (select cc_code from budget_cc) and cc_type='Performing' and status in ('Old','New') and cc_subtype='Manufacturing'", con);
            da.Fill(ds, "SERVICECC");
            ddlCCcode.DataTextField = "Name";
            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataSource = ds.Tables["SERVICECC"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
            trccode.Visible = true;
            year.Visible = false;
            trbudget.Visible = true;
            trbutton.Visible = true;
        }
        else
        {
            trccode.Visible = false;
            year.Visible = false;
            trbudget.Visible = false;
            trbutton.Visible = false;
        }


    }

}
