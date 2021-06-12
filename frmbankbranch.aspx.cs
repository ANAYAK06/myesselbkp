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



public partial class Admin_frmbankbranch : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    SqlDataReader dr ;
    string bankname, accholdername, accno, date, minbalance, location;
    public static string role = "";
    protected void Page_Load(object sender, EventArgs e)
    {
      
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

      
        if (!IsPostBack)
        {
          
            //Fillbanks();
          
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                update.Visible = false;
                btnadd.Visible = true;
                btnreset.Visible = true;
                lblminbalance.Visible = false;
                txtminbal.Visible = false;
                Label7.Text = "Add Bank Details";
                GridView1.Visible = false;
                lblbankname.Visible = false;
            }
            if (Session["roles"].ToString() == "HoAdmin")
            {
                tbldetails.Visible = false;
                GridView1.Visible = true;
                BindBanks("1");
            }
            if (Session["roles"].ToString() == "SuperAdmin")
            {

                tbldetails.Visible = false;
                // notice(Session["roles"].ToString());
                GridView1.Visible = true;
                BindBanks("2");
            }
            if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {

                tbldetails.Visible = false;
                // notice(Session["roles"].ToString());
                GridView1.Visible = true;
                BindBanks("2A");
            }

            role = Session["roles"].ToString();
        }
       

    }

    public void BindBanks(string status)
    {
        try
        {

            this.up.Update();
            SqlDataAdapter objda = new SqlDataAdapter("select bank_id,bank_name,bank_location,acc_no,Date from bank_branch where status='" + status + "'", con);
            DataSet objds = new DataSet();
            objda.Fill(ds, "banksinfo");
            GridView1.DataSource = ds.Tables["banksinfo"];
            GridView1.DataBind();
            update.Visible = false;
            btnreset.Visible = false;
            this.up.Update();
        }
        catch (Exception ex)
        {

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure " +
                               "you want to delete this record?')) return true;";
                }
            }
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex != -1)
        {
            try
            {
                tbldetails.Visible=true;          
                update.Visible=true;
                btnadd.Visible=false;
                btnreset.Visible=true;
                lblbankname.Visible = true;
                txtbankname.Visible = false;
                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    lblminbalance.Visible=true;
                    txtminbal.Visible=true;
                }
                else
                {
                    lblminbalance.Visible=false;
                    txtminbal.Visible=false;
                }
                Label7.Text = "Verify Bank Details";
                string id = GridView1.SelectedValue.ToString();
                da = new SqlDataAdapter("Select accholder_name,acc_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,date,101),106), ' ', '-')as date,minimum_balance,bank_location,isnull(balance,0),status,bank_name from bank_branch where bank_id='" + id + "'", con);
                da.Fill(ds, "bankdetails");
                if (ds.Tables["bankdetails"].Rows.Count > 0)
                {
                    lblbankname.Text = ds.Tables["bankdetails"].Rows[0]["bank_name"].ToString();
                    txtacholder.Text = ds.Tables["bankdetails"].Rows[0][0].ToString();
                    txtaccno.Text = ds.Tables["bankdetails"].Rows[0][1].ToString();
                    txtacopening.Text = ds.Tables["bankdetails"].Rows[0][2].ToString();
                    //labelbalance.Text = ds.Tables["bankdetails"].Rows[0][5].ToString();
                    hfbalance.Value = ds.Tables["bankdetails"].Rows[0][5].ToString();
                    hfstatus.Value = ds.Tables["bankdetails"].Rows[0]["status"].ToString();
                    if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                    {
                        txtminbal.Text = ds.Tables["bankdetails"].Rows[0][3].ToString().Replace(".0000", ".00");
                    }
                    txtlocation.Text = ds.Tables["bankdetails"].Rows[0][4].ToString();
                    hfbankid.Value = id;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         try
            {
                cmd.Connection = con;
                cmd.CommandText = "delete from bank_branch where bank_id='" + GridView1.DataKeys[e.RowIndex]["bank_id"].ToString() + "'";             
                con.Open();
                bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (j == true)
                {
                    JavaScript.UPAlert(Page, "Bank Branch deleted sucessfully");
                    if (Session["roles"].ToString() == "HoAdmin")
                        BindBanks("1");
                    if (Session["roles"].ToString() == "SuperAdmin")
                        BindBanks("2");
                    if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                        BindBanks("2A");
                }
                else
                {
                    JavaScript.UPAlert(Page, "Bank Branch not deleted successfully");
                }
            }
            catch (Exception ex)
            {

            }
        
    
    }
    //public void Fillbanks()
    //{
    //    try
    //    {

    //        SqlDataAdapter dAdapter = new SqlDataAdapter("select bank_name from bank_branch where status='3'", con);
    //        DataSet objCC = new DataSet();
    //        dAdapter.Fill(objCC,"banks");
    //        lstbankname.DataTextField = "bank_name";
    //        lstbankname.DataValueField = "bank_name";
    //        lstbankname.DataSource = objCC.Tables["banks"];
    //        lstbankname.DataBind();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    protected void btnadd_Click(object sender, EventArgs e)
    {
        bankname=txtbankname.Text;
        accholdername=txtacholder.Text;
        accno= txtaccno.Text;
        date = txtacopening.Text;
        location = txtlocation.Text;
        try
        {
            da = new SqlDataAdapter("Select count(*) from bank_branch where bank_name='"+txtbankname.Text+"'", con);
            da.Fill(ds, "bankname");
            if (ds.Tables["bankname"].Rows[0][0].ToString() == "0")
            {
                cmd.Connection = con;
                cmd.CommandText = "insert into bank_branch (bank_name,accholder_name,acc_no,date,bank_location,status)values(@bankname,@accholder,@accno,@date,@banklocation,'1')";
                cmd.Parameters.Add("@bankname", SqlDbType.VarChar).Value = bankname;
                cmd.Parameters.Add("@accholder", SqlDbType.VarChar).Value = accholdername;
                cmd.Parameters.Add("@accno", SqlDbType.VarChar).Value = accno;
                cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = date;
                cmd.Parameters.Add("@banklocation", SqlDbType.VarChar).Value = location;
                con.Open();
                bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (j == true)
                {
                    JavaScript.UPAlert(Page, "Bank Branch Successfully Added");
                    clear();
                }
                else
                {
                    JavaScript.UPAlert(Page, "Bank Branch Not Inserted Successfully");
                }
            }
            else
            {
                JavaScript.UPAlert(Page, "This Bank Branch is already exists");
            }

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
     public void showalert(string message)
    {
        string script = "alert(\"" + message + "\");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);

    }
        public void clear()
        {
            if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
              
                txtminbal.Text = "";
            }
            else
            {
                txtbankname.Text = "";
            }
            txtacholder.Text = "";
            txtaccno.Text = "";
            txtacopening.Text = "";
            txtlocation.Text = "";
        }
    protected void  btnreset_Click(object sender, EventArgs e)
    {
          clear();
    }
  
    //protected void ddlbankname_SelectedIndexChanged(object sender, EventArgs e)
    //{
       
    //    try
    //    {
           
    //        if (ddlbankname.SelectedValue != "")
    //        {
    //            if (Session["roles"].ToString() == "HoAdmin")
    //            {
    //                da = new SqlDataAdapter("select accholder_name,acc_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,date,101),106), ' ', '-')as date,isnull(minimum_balance,0),bank_location,isnull(balance,0),status from bank_branch where bank_name='" + ddlbankname.SelectedValue + "' and status='1'", con);
    //            }
    //            else if (Session["roles"].ToString() == "SuperAdmin")
    //            {
    //                da = new SqlDataAdapter("select accholder_name,acc_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,date,101),106), ' ', '-')as date,minimum_balance,bank_location,isnull(balance,0),status from bank_branch where bank_name='" + ddlbankname.SelectedValue + "' and status in('2','3') ", con);
    //            }
    //            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
    //            {
    //                da = new SqlDataAdapter("select accholder_name,acc_no,REPLACE(CONVERT(VARCHAR(11),convert(datetime,date,101),106), ' ', '-')as date,minimum_balance,bank_location,isnull(balance,0),status from bank_branch where bank_name='" + ddlbankname.SelectedValue + "' and status in('2A','3') ", con);
    //            }
    //            da.Fill(ds, "bankdetails");
    //            txtacholder.Text = ds.Tables["bankdetails"].Rows[0][0].ToString();
    //            txtaccno.Text = ds.Tables["bankdetails"].Rows[0][1].ToString();
    //            txtacopening.Text =ds.Tables["bankdetails"].Rows[0][2].ToString();
    //            //labelbalance.Text = ds.Tables["bankdetails"].Rows[0][5].ToString();
    //            hfbalance.Value = ds.Tables["bankdetails"].Rows[0][5].ToString();
    //            hfstatus.Value = ds.Tables["bankdetails"].Rows[0]["status"].ToString();
    //            if (Session["roles"].ToString() == "Chairman Cum Managing Director")
    //            {
    //                txtminbal.Text = ds.Tables["bankdetails"].Rows[0][3].ToString().Replace(".0000", ".00");
    //            }
    //            txtlocation.Text = ds.Tables["bankdetails"].Rows[0][4].ToString();
    //        }
               
    //        else
    //        {
    //            clear();
    //        }
           
            
           
    //    }
    //    catch (Exception ex)
    //    {
    //        Utilities.CatchException(ex);
    //        JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
    //    }
           

    //}
    protected void update_Click(object sender, EventArgs e)
    {

        try
        {
            cmd.Connection = con;
           
            if (Session["roles"].ToString() == "HoAdmin")
            {
                cmd.CommandText = "update bank_branch set accholder_name='" + txtacholder.Text + "',acc_no='" + txtaccno.Text + "',date='" + txtacopening.Text + "',bank_location='" + txtlocation.Text + "',status='2',balance='0'   where bank_id='" + hfbankid.Value + "'";
            }
            if (Session["roles"].ToString() == "SuperAdmin")
            {
                cmd.CommandText = "update bank_branch set accholder_name='" + txtacholder.Text + "',acc_no='" + txtaccno.Text + "',date='" + txtacopening.Text + "',minimum_balance='" + txtminbal.Text + "',bank_location='" + txtlocation.Text + "',status='2A',balance='0' where bank_id='" + hfbankid.Value + "'";
            }
            if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                cmd.CommandText = "update bank_branch set accholder_name='" + txtacholder.Text + "',acc_no='" + txtaccno.Text + "',date='" + txtacopening.Text + "',minimum_balance='" + txtminbal.Text + "',bank_location='" + txtlocation.Text + "',status='3',balance='0'  where bank_id='" + hfbankid.Value + "'";
            }
            con.Open();
            bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (j == true)
            {
                JavaScript.UPAlert(Page, "Bank Branch Successfully updated");
                clear();
                //if (Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
                //{
                //    notice(Session["roles"].ToString());
                //}
                if (Session["roles"].ToString() == "HoAdmin")
                    BindBanks("1");
                if (Session["roles"].ToString() == "SuperAdmin")
                    BindBanks("2");
                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    BindBanks("2A");
                    lblminbalance.Visible=false;
                    txtminbal.Visible=false;

                }
                tbldetails.Visible = false;
                lblbankname.Visible = true;
                txtbankname.Visible = false;
            }
            else
            {
                JavaScript.UPAlert(Page, "Fail");
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
    //public void balance()
    //{
    //    da = new SqlDataAdapter("SELECT balance from bank_branch where bank_name='"+txtbankname.Text+"'", con);
    //    da.Fill(ds, "bankbalance");
    //    labelbalance.Text = ds.Tables["bankbalance"].Rows[0][0].ToString();
    //}

    public void notice(string role)
    {
        if (role == "SuperAdmin")
        da = new SqlDataAdapter("select bank_name,bank_location from bank_branch where status='2'", con);
        else if (role == "Chairman Cum Managing Director")
            da = new SqlDataAdapter("select bank_name,bank_location from bank_branch where status='2A'", con);
        da.Fill(ds, "bankname");
        if (ds.Tables["bankname"].Rows.Count > 0)
        {
            lblnotification.Text = "Waiting For Approval   " + ds.Tables["bankname"].Rows[0].ItemArray[0].ToString() + ds.Tables["bankname"].Rows[0].ItemArray[1].ToString();
        }
        else
        {
            lblnotification.Text = "No New Banks To Verify";
        }
    }
  
}

