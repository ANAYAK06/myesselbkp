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

public partial class Admin_frmAddContract : System.Web.UI.Page
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

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 26);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");

        if (!IsPostBack)
        {
            btnAddContract.Attributes.Add("onclick", "return Addcontract()");
        }

    }


    protected void btnAddContract_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_Add_Contract", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@projectname", txtProjname.Text);
            cmd.Parameters.AddWithValue("@clientname", txtClientname.Text);
            cmd.Parameters.AddWithValue("@customername", txtCostomername.Text);
            cmd.Parameters.AddWithValue("@division", txtDivision.Text);
            cmd.Parameters.AddWithValue("@projmgrname", txtPMname.Text);
            cmd.Parameters.AddWithValue("@projmgrcontactno", txtContactno.Text);
            cmd.Parameters.AddWithValue("@startdate", txtStartdate.Text);
            cmd.Parameters.AddWithValue("@enddate", txtEnddate.Text);
            cmd.Parameters.AddWithValue("@cccode", ddlCCcode.SelectedValue);
            cmd.Parameters.AddWithValue("@natureofjob", txtjob.Text);
            //cmd.Parameters.AddWithValue("@pono", txtPO.Text);
            //cmd.Parameters.AddWithValue("@podate", txtPodate.Text);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            //if (txtBasic.Text != "")
            //    cmd.Parameters.AddWithValue("@Basic", txtBasic.Text);
            //if (txtTax.Text != "")
            //    cmd.Parameters.AddWithValue("@ServiceTax", txtTax.Text);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            //JavaScript.AlertAndRedirect(cmd.ExecuteScalar().ToString(),Request.Url.ToString());
            JavaScript.UPAlertRedirect(Page, cmd.ExecuteScalar().ToString(), Request.Url.ToString());
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    public void clear()
    {
        txtProjname.Text = "";
        txtClientname.Text = "";
        txtCostomername.Text = "";
        txtDivision.Text = "";
        txtPMname.Text = "";
        txtContactno.Text = "";
        txtStartdate.Text = "";
        txtEnddate.Text = "";
        ddlCCcode.SelectedValue = "";
        //txtPO.Text = "";
        txtjob.Text = "";

        //txtPodate.Text = "";
        //txtBasic.Text = "";
        //txtTax.Text = "";

    }


    protected void ddlCCcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //da = new SqlDataAdapter("Select cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedItem.Text + "'", con);
        //da.Fill(ds, "ccode");
        //if (ds.Tables["ccode"].Rows[0].ItemArray[0].ToString() == "Manufacturing")
        //{
        //    Label17.Text="Excise Duty";
                
        //}
        //else
        //{
        //    Label17.Text = "Service Tax";
        //}
    }
}
