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

public partial class AccountCreation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Tools");
        lbl.Attributes.Add("class", "active");
        
           
       
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 58);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
         cc.Visible = false;
            
    }
    protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrole.SelectedItem.Text == "Project Manager")
        {
            cc.Visible = false;
            grid.Visible = true;
            fillgrid();

        }
        else if (ddlrole.SelectedItem.Text == "Accountant" || ddlrole.SelectedItem.Text == "Central Store Keeper" || ddlrole.SelectedItem.Text == "StoreKeeper")
        {

            cc.Visible = true;
            grid.Visible = false;

        }
        else
        {
            
            cc.Visible = false;
            grid.Visible = false;
        }
    }
    public void fillgrid()
    {
        try 
        {
            da = new SqlDataAdapter("Select c.cc_code,c.cc_name,isnull(Name,'')Name   from (Select j.*, (first_name+'  '+last_name) as [Name] from (select  r.User_Name ,cc_code,Roles  from CC_User u join user_roles r on u.User_Name=r.User_Name)j join employee_data k on j.User_Name=k.User_Name   where j.Roles='Project Manager')i right outer join cost_center c on i.cc_code=c.cc_code", con);
            da.Fill(ds, "Pminfo");
            GridView1.DataSource = ds.Tables["Pminfo"];
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
            CheckBox c1 = (CheckBox)e.Row.FindControl("chkSelect");
            if (e.Row.Cells[3].Text != "&nbsp;")
            {
                e.Row.Enabled = false;
            }
            else
            {
                e.Row.Enabled = true;
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string cccode = "";

        byte[] enc_pass = new byte[txtpassword.Text.Length];
        enc_pass = System.Text.Encoding.UTF8.GetBytes(txtpassword.Text);
        string encodePassword = Convert.ToBase64String(enc_pass);


        cmd = new SqlCommand("accountcreation_sp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@empid", ddlemployeesid.SelectedValue);
        cmd.Parameters.AddWithValue("@loginid",txtloginid.Text );
        cmd.Parameters.AddWithValue("@password", encodePassword);
        cmd.Parameters.AddWithValue("@role ",ddlrole.SelectedItem.Text);
        if (ddlrole.SelectedItem.Text == "Project Manager")
        {
            foreach (GridViewRow rec in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)rec.FindControl("chkSelect");
                {
                    if (chk.Checked)
                    {
                        //cccode = cccode + GridView1.DataKeys[rec.RowIndex]["cc_code"].ToString() + ",";
                        //cmd.Parameters.AddWithValue("@CCCodes", cccode);
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandText = "insert into [CC_User]([User_Name],cc_code) values (@loginid,@CC_CODE)";
                        cmd1.Parameters.AddWithValue("@loginid",SqlDbType.VarChar).Value=txtloginid.Text;
                        cmd1.Parameters.AddWithValue("@CC_CODE",SqlDbType.VarChar).Value=GridView1.DataKeys[rec.RowIndex]["cc_code"].ToString();
                        cmd1.Connection = con;
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }
        }
        else
        {
            cmd.Parameters.AddWithValue("@CCCode", ddlcccode.SelectedItem.Text);
        }
       
        cmd.Connection = con;
        con.Open();
        string message = cmd.ExecuteScalar().ToString();
        if (message == "Successfull")
            JavaScript.UPAlertRedirect(Page, message, "AccountCreation.aspx");
        else
            JavaScript.UPAlertRedirect(Page, "Insertion Failed", "AccountCreation.aspx");

        con.Close();
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        loginbll objLgnDal = new loginbll();
        string encrpOPwd = objLgnDal.encryptPass(txtnewpassword.Text);
        cmd.CommandText = "Update register set pwd='" + encrpOPwd + "',status='Pending' where user_name=(Select user_name from employee_data where employee_id='" + ddlmdlemployee.SelectedValue + "')";
        cmd.Connection = con;
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        if (i == 1)
            JavaScript.UPAlertRedirect(Page, "Sucessfully Reset", "AccountCreation.aspx");
        else
            JavaScript.UPAlert(Page, "Failed");

    }
}


