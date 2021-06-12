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
using AjaxControlToolkit;



public partial class chequebook : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        
        if (Session["roles"].ToString() == "HoAdmin")
        {

            tblgrid.Visible = true;
            tblcheque.Visible = false;
            if (!IsPostBack)
            {
                fillgrid();

            }
            

        }
        else if (Session["roles"].ToString() == "Sr.Accountant")
        {
            tblgrid.Visible = false;
            tblcheque.Visible = true;

        }
        else
        {
            tblgrid.Visible = false;
            tblcheque.Visible = false;
        }
        
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                BankDateCheck objdate = new BankDateCheck();
                if (objdate.IsBankDateCheck(txtdate.Text, ddlbankname.SelectedItem.Text))
                {
                    cmd = new SqlCommand("sp_chequebook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bankname", ddlbankname.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@issuedate", txtdate.Text);
                    cmd.Parameters.AddWithValue("@from", txtform.Text);
                    cmd.Parameters.AddWithValue("@to", txtTo.Text);
                    cmd.Parameters.AddWithValue("@description", txtdesc.Text);
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();

                    if (msg == "Sucessfull")
                        JavaScript.UPAlertRedirect(Page, msg, "chequebook.aspx");

                    else
                        JavaScript.UPAlert(Page, msg);

                    con.Close();
                }
                else
                {
                    JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
       
    }

    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                da = new SqlDataAdapter("select cm.chequeid,cm.bankname,REPLACE(CONVERT(VARCHAR(11),cm.issuedate, 106), ' ', '-')as issuedate,cm.description,(select Min(chequeno) from Cheque_Nos where chequeid=cm.chequeid)[From],(select MAX(chequeno) from Cheque_Nos where chequeid=cm.chequeid)[To] from cheque_Master cm where cm.status='1'", con);
                da.Fill(ds, "fill");
                GridView1.DataSource = ds.Tables["fill"];
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
   
   }
   
  
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            GridViewRow gvr = (GridViewRow)GridView1.Rows[e.RowIndex];

            string chequeid = GridView1.DataKeys[e.RowIndex]["chequeid"].ToString();
            DropDownList txtbankname = (DropDownList)gvr.FindControl("txtbankname");
            TextBox txtissuedate = (TextBox)gvr.FindControl("txtissuedate");
            TextBox textFrom = (TextBox)gvr.FindControl("textFrom");
            TextBox textTo = (TextBox)gvr.FindControl("textTo");
            TextBox txtdescription = (TextBox)gvr.FindControl("txtdescription");
            
            cmd = new SqlCommand("sp_Editchequebook", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@chkid", chequeid);
            cmd.Parameters.AddWithValue("@bankname", txtbankname.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@issuedate", txtissuedate.Text);
            cmd.Parameters.AddWithValue("@from", textFrom.Text);
            cmd.Parameters.AddWithValue("@to", textTo.Text);
            cmd.Parameters.AddWithValue("@description", txtdescription.Text);
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Updated Sucessfully")
                JavaScript.UPAlertRedirect(Page,msg, "chequebook.aspx");
            else
                JavaScript.UPAlert(Page,msg);
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
        GridView1.EditIndex = -1;
        fillgrid();
       

    }
    protected void fromValidate(object source, ServerValidateEventArgs args)
    {
        //args.IsValid = (args.Value.Length < 6);
        if (args.Value.Length == 6)
            args.IsValid = true;
       
        else
            args.IsValid = false;
        
    }
    
  

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgrid();
    }
   
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CascadingDropDown cc = (CascadingDropDown)e.Row.FindControl("CascadingDropDown9");
                HiddenField h1 = (HiddenField)e.Row.FindControl("h1");
                cc.SelectedValue = h1.Value;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int chequeid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex]["chequeid"].ToString());
            string s1, s2;
            s1 = "delete from cheque_Master where chequeid='" + chequeid + "'";
            s2 = "delete from Cheque_Nos where chequeid='" + chequeid + "'";
            cmd.Connection = con;
            cmd.CommandText = s1 + s2;
            con.Open();
            int j = cmd.ExecuteNonQuery();
            con.Close();
            if (j >= 1)
            {
                JavaScript.UPAlertRedirect(Page,"Sucessfully Deleted", "chequebook.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page,"Deletion Failed");
            }
            GridView1.EditIndex = -1;
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
   
}
