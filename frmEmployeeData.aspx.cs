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

public partial class Admin_frmEmployeeData : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (Request.QueryString["id"] == null)
        {
            Edit.Visible = false;
            Submit.Visible = true;
            uid.Style.Add("visibility", "hidden");
            pwd.Style.Add("visibility", "hidden");
           
        }
        else if (Request.QueryString["type"] == "v")
        {
            viewuserinfo();
            uid.Style.Add("visibility", "hidden");
            pwd.Style.Add("visibility", "hidden");
            Edit.Visible = false;
            Submit.Visible = false;
            Cancel.Visible = false;


        }
        else
        {

            da = new SqlDataAdapter("Select Employee_status from employee_data where id='" + Request.QueryString["id"].ToString() + "'", con);
            da.Fill(ds, "estatus");

            if (ds.Tables["estatus"].Rows[0].ItemArray[0].ToString() == "New" || ds.Tables["estatus"].Rows[0].ItemArray[0].ToString() == "Left")
            {
                viewuserinfo();
                uid.Style.Add("visibility", "visible");
                pwd.Style.Add("visibility", "visible");
                Edit.Visible = true;
                Submit.Visible = false;
            }
            else if (Session["roles"].ToString() != "SuperAdmin")
            {
                viewuserinfo();
                uid.Style.Add("visibility", "hidden");
                pwd.Style.Add("visibility", "hidden");
                Edit.Visible = true;
                Submit.Visible = false;
                txtfname.Attributes.Add("readonly", "1");
                txtlname.Attributes.Add("readonly", "2");
                txtmidddle.Attributes.Add("readonly", "3");
                txtnname.Attributes.Add("readonly", "4");
                txtrelation.Attributes.Add("readonly", "5");
                txtdate.Attributes.Add("readonly", "6");
                txtjoin.Attributes.Add("readonly", "7");
                txtleave.Attributes.Add("readonly", "8");
                txtadd.Attributes.Add("readonly", "9");
                txtph.Attributes.Add("readonly", "10");
                txtph1.Attributes.Add("readonly", "11");
                txtph2.Attributes.Add("readonly", "12");
                txtemail.Attributes.Add("readonly", "13");
                txtreferrred.Attributes.Add("readonly", "14");
                txtfathername.Attributes.Add("readonly", "14");
                ddlstatus.Enabled = false;
                ddlgen.Enabled = false;
                ddldep.Enabled = false;
                ddlcat.Enabled = false;
            }
            else
            {
                viewuserinfo();
                uid.Style.Add("visibility", "hidden");
                pwd.Style.Add("visibility", "hidden");
                Edit.Visible = true;
                Submit.Visible = false;
            }
        }







        if (!IsPostBack)
        {
            Submit.Attributes.Add("onclick", "return registrationvalidation()");
            Edit.Attributes.Add("onclick", "return Registration()");
            //string sFilePath = "../images/" + "fc" + ds.Tables["userinfo"].Rows[0].ItemArray[1].ToString() + ".jpg";//UserImage path....
            //string sDeFilepath = "../images/photo.gif";//Default Image path...

            //bool blPath = System.IO.File.Exists(MapPath(sFilePath));//Checking whether UserImage exist or not....

            //if (blPath == true)
            //{
            //    img.ImageUrl = sFilePath;
            //}
            //else
            //{
            //    img.ImageUrl = sDeFilepath;

            //}

        }
    }
    public void viewuserinfo()
    {
        try
        {
            if (!IsPostBack)
            {
                da = new SqlDataAdapter("SELECT r.User_Name,[first_name],[Category],[Department],[mail_id],[phone_no],a.roles,u.cc_code,[status], REPLACE(CONVERT(VARCHAR(11),date_of_birth, 106), ' ', '-')as date_of_birth,father_name,relation,Address,referred_by,Employee_status,last_name,mobile_no,nominee_name,REPLACE(CONVERT(VARCHAR(11),date_of_joining, 106), ' ', '-')as[date_of_joining],REPLACE(CONVERT(VARCHAR(11),last_working_day, 106), ' ', '-')as[last_working_day],martial_status from Employee_Data r left outer join CC_User u on r.User_Name=u.User_Name left outer join user_roles a on r.User_Name=a.User_Name where r.id='" + Request.QueryString["id"].ToString() + "'", con);
                da.Fill(ds, "userinfo");
                if (ds.Tables["userinfo"].Rows.Count > 0)
                {

                    txtfname.Text = ds.Tables["userinfo"].Rows[0].ItemArray[1].ToString();
                    ddlcat.SelectedValue = ds.Tables["userinfo"].Rows[0].ItemArray[2].ToString();
                    ddldep.SelectedValue = ds.Tables["userinfo"].Rows[0].ItemArray[3].ToString();
                    txtemail.Text = ds.Tables["userinfo"].Rows[0].ItemArray[4].ToString();
                    //txtph.Text = ds.Tables["userinfo"].Rows[0].ItemArray[5].ToString().Substring(0, 5);
                    //txtph2.Text = ds.Tables["userinfo"].Rows[0].ItemArray[5].ToString().Replace(txtph.Text, txtph2.Text); ;
                    if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Sr.Accountant")
                    {
                        CascadingDropDown2.Enabled = false;
                        ddlrole.SelectedItem.Text = ds.Tables["userinfo"].Rows[0].ItemArray[6].ToString();

                        ViewState["roles"] = ds.Tables["userinfo"].Rows[0].ItemArray[6].ToString();

                    }
                    else if (Session["roles"].ToString() == "SuperAdmin")
                    {
                        CascadingDropDown2.SelectedValue = ds.Tables["userinfo"].Rows[0].ItemArray[6].ToString();
                        CascadingDropDown2.Enabled = true;
                    }
                    CascadingDropDown1.SelectedValue = ds.Tables["userinfo"].Rows[0].ItemArray[7].ToString();

                    txtdate.Text = ds.Tables["userinfo"].Rows[0].ItemArray[9].ToString();
                    txtfathername.Text = ds.Tables["userinfo"].Rows[0].ItemArray[10].ToString();
                    txtrelation.Text = ds.Tables["userinfo"].Rows[0].ItemArray[11].ToString();
                    txtadd.Text = ds.Tables["userinfo"].Rows[0].ItemArray[12].ToString();
                    txtreferrred.Text = ds.Tables["userinfo"].Rows[0].ItemArray[13].ToString();
                    ddlgen.SelectedValue = ds.Tables["userinfo"].Rows[0].ItemArray[14].ToString();
                    txtlname.Text = ds.Tables["userinfo"].Rows[0].ItemArray[15].ToString();
                    txtph1.Text = ds.Tables["userinfo"].Rows[0].ItemArray[16].ToString();
                    txtnname.Text = ds.Tables["userinfo"].Rows[0].ItemArray[17].ToString();
                    txtjoin.Text = ds.Tables["userinfo"].Rows[0].ItemArray[18].ToString();
                    txtleave.Text = ds.Tables["userinfo"].Rows[0].ItemArray[19].ToString();
                    ddlstatus.SelectedValue = ds.Tables["userinfo"].Rows[0].ItemArray[20].ToString();
                }
            }

        }
         catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }


       
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        try
        {
            string s1 = "";
            string s;
            string s2 = "";
            string s3 = "";
            string s4 = "";
            da = new SqlDataAdapter("SELECT r.User_Name from Employee_Data r left outer join CC_User u on r.User_Name=u.User_Name left outer join user_roles a on r.User_Name=a.User_Name where r.id='" + Request.QueryString["id"].ToString() + "'", con);
            da.Fill(ds, "user");


            s = ds.Tables["user"].Rows[0].ItemArray[0].ToString();
            if (ddlgen.SelectedItem.Text == "Existing")
            {
                cmd.Connection = con;
             
                    if (Session["roles"].ToString() == "SuperAdmin")
                    {
                        s2 = "update user_roles set Roles='" + ddlrole.SelectedItem.Text + "' where User_Name='" + s + "'";
                        s3 = "update Employee_data set first_name='" + txtfname.Text + "',last_name='" + txtlname.Text + "',middle_name='" + txtmidddle.Text + "',father_name='" + txtfathername.Text + "',phone_no='" + txtph.Text + "',mail_id='" + txtemail.Text + "',date_of_joining='" + txtjoin.Text + "',date_of_birth='" + txtdate.Text + "',nominee_name='" + txtnname.Text + "',relation='" + txtrelation.Text + "',category='" + ddlcat.SelectedItem.Text + "',department='" + ddldep.SelectedItem.Text + "',Address='" + txtadd.Text + "',mobile_no='" + txtph1.Text + "',referred_by='" + txtreferrred.Text + "',last_working_day='" + txtleave.Text + "',martial_status='" + ddlstatus.SelectedItem.Text + "',status='Activate' where id='" + Request.QueryString["id"].ToString() + "'";
                        s1 = "update CC_User set cc_code='" + ddlcccode.SelectedValue + "' where User_Name='" + s + "'";
                        cmd.CommandText = s1 + s2 + s3;
                        con.Open();
                        bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        if (j == true)
                        {
                            JavaScript.AlertAndRedirect("Updated", "frmEmployeeData.aspx");
                        }
                        else
                        {

                            JavaScript.Alert("Updation Failed");
                        }

                    }
                    else
                    {
                        if (ViewState["roles"].ToString() != "Project Manager")
                        {
                            s4 = "update CC_User set cc_code='" + ddlcccode.SelectedValue + "' where User_Name='" + s + "'";
                            cmd.CommandText = s4;
                            con.Open();
                            bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                            if (j == true)
                            {
                                JavaScript.AlertAndRedirect("Updated", "frmEmployeeData.aspx");
                            }
                            else
                            {

                                JavaScript.Alert("Updation Failed");
                            }

                        }
                        else 
                        {
                            JavaScript.CloseWindow();
                        }
                    
                    }
                   
             
            }

            else if (ddlgen.SelectedItem.Text == "Re-Joining")
            {
                byte[] enc_pass = new byte[txtpwd.Text.Length];
                enc_pass = System.Text.Encoding.UTF8.GetBytes(txtpwd.Text);
                string encodePass = Convert.ToBase64String(enc_pass);

                if (Session["roles"].ToString() == "SuperAdmin")
                {

                    cmd.Connection = con;

                    s1 = "Insert into CC_User(user_name,cc_code) values(@user_name,@cc_code)";
                    s2 = "Insert into user_roles(user_name,roles)values(@user_name,@roles)";
                    s3 = "update Employee_data set user_name='" + txtuserid.Text + "',first_name='" + txtfname.Text + "',last_name='" + txtlname.Text + "',middle_name='" + txtmidddle.Text + "',father_name='" + txtfathername.Text + "',phone_no='" + txtph.Text + "',mail_id='" + txtemail.Text + "',date_of_joining='" + txtdate.Text + "',date_of_birth='" + txtdate.Text + "',nominee_name='" + txtnname.Text + "',relation='" + txtrelation.Text + "',category='" + ddlcat.SelectedItem.Text + "',department='" + ddldep.SelectedItem.Text + "',Address='" + txtadd.Text + "',mobile_no='" + txtph1.Text + "',referred_by='" + txtreferrred.Text + "',last_working_day='" + txtleave.Text + "',status='Activate' where id='" + Request.QueryString["id"].ToString() + "'";
                    s4 = "Insert into Register(user_name,pwd)values(@user_name,@pwd)";
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = txtuserid.Text;
                    cmd.Parameters.Add("@cc_code", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                    cmd.Parameters.Add("@roles", SqlDbType.VarChar).Value = ddlrole.SelectedValue;
                    cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = encodePass;

                    cmd.CommandText = s1 + s2 + s3 + s4;
                    con.Open();
                    bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    if (j == true)
                    {
                        JavaScript.AlertAndRedirect("Updated", "frmEmployeeData.aspx");
                    }
                    else
                    {

                        JavaScript.Alert("Updation Failed");
                    }
                }
                else
                {
                    JavaScript.Alert("You are not authorized person");
                }

            }
            else if (ddlgen.SelectedItem.Text == "Left")
            {
                if (Session["roles"].ToString() == "SuperAdmin")
                {
                    cmd.Connection = con;

                    s1 = "Delete from CC_User  where User_Name='" + s + "'";
                    s2 = "Delete from user_roles where User_Name='" + s + "'";
                    s3 = "Delete from Register where User_Name='" + s + "'";
                    s4 = "Update Employee_data set last_working_day='" + txtleave.Text + "',employee_status='Left',status='Deactivate' where user_name='" + s + "'";

                    cmd.CommandText = s1 + s2 + s3 + s4;
                    con.Open();
                    bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    if (j == true)
                    {
                        JavaScript.AlertAndRedirect("Updated", "frmEmployeeData.aspx");
                    }
                    else
                    {

                        JavaScript.Alert("Updation Failed");
                    }
                }
                else
                {
                    JavaScript.Alert("You are not authorized person");
                }


            }


            else if (ddlgen.SelectedItem.Text == "New")
            {

                byte[] enc_pass = new byte[txtpwd.Text.Length];
                enc_pass = System.Text.Encoding.UTF8.GetBytes(txtpwd.Text);
                string encodePass = Convert.ToBase64String(enc_pass);

                if (Session["roles"].ToString() == "SuperAdmin")
                {

                    cmd.Connection = con;

                    s1 = "Insert into CC_User(user_name,cc_code) values(@user_name,@cc_code)";
                    s2 = "Insert into user_roles(user_name,roles)values(@user_name,@roles)";
                    s3 = "Insert into Register(user_name,pwd,first_name,status)values(@user_name,@pwd,@first_name,@status)";
                    s4 = "Update Employee_data set user_name='" + txtuserid.Text + "',employee_status='Existing',status='Activate' where id='" + Request.QueryString["id"].ToString() + "'";
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = txtuserid.Text;
                    cmd.Parameters.Add("@cc_code", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                    cmd.Parameters.Add("@roles", SqlDbType.VarChar).Value = ddlrole.SelectedValue;
                    cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = encodePass;
                    cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = txtfname.Text;
                    cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "Pending";
                    cmd.CommandText = s1 + s2 + s3 + s4;
                    con.Open();
                    bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    if (j == true)
                    {
                        JavaScript.AlertAndRedirect("Updated", "frmEmployeeData.aspx");
                    }
                    else
                    {

                        JavaScript.Alert("Updation Failed");
                    }
                }
                else
                {
                    JavaScript.Alert("You are not authorized person");
                }
            }




        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }



    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {


            //string strImage = "fc" + txtfname.Text.Replace(" ", "_") + ".jpg";
            //if (fluImage.PostedFile != null && fluImage.PostedFile.FileName != "")
            //{

            //    fluImage.SaveAs("F:\\INDUSTOUCH-ESSEL\\essel\\images\\" + strImage);
            //    fluImage.PostedFile.InputStream.Close();
            //}

            cmd.Connection = con;
            cmd.CommandText = "Insert into Employee_data(first_name,last_name,middle_name,father_name,phone_no,mail_id,date_of_joining,date_of_birth,nominee_name,relation,category,department,address,mobile_no,referred_by,Employee_status,status,martial_status)values(@first_name,@last_name,@middle_name,@father_name,@phoneNo,@mailid,@date,@dateofbirth,@nominee_name,@realtion,@category,@department,@Address,@mobile_no,@referred_by,@Employee_status,@status,@martialstatus)";
            cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = txtfname.Text;
            cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = txtlname.Text;
            cmd.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = txtmidddle.Text;
            cmd.Parameters.Add("@father_name", SqlDbType.VarChar).Value = txtfathername.Text;
            cmd.Parameters.Add("@phoneNo", SqlDbType.VarChar).Value = txtph.Text + txtph2.Text;
            cmd.Parameters.Add("@mailid", SqlDbType.VarChar).Value = txtemail.Text;
            cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = txtjoin.Text;
            cmd.Parameters.Add("@dateofbirth", SqlDbType.VarChar).Value = txtdate.Text;
            cmd.Parameters.Add("@nominee_name", SqlDbType.VarChar).Value = txtnname.Text;
            cmd.Parameters.Add("@realtion", SqlDbType.VarChar).Value = txtrelation.Text;
            cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = ddlcat.SelectedValue;
            cmd.Parameters.Add("@department", SqlDbType.VarChar).Value = ddldep.SelectedValue;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtadd.Text;
            cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar).Value = txtph1.Text;
            cmd.Parameters.Add("@referred_by", SqlDbType.VarChar).Value = txtreferrred.Text;
            cmd.Parameters.Add("@Employee_status", SqlDbType.VarChar).Value = "New";
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "Deactivate";
            cmd.Parameters.Add("@martialstatus", SqlDbType.VarChar).Value = ddlstatus.SelectedValue;
            //cmd.Parameters.Add("@img", SqlDbType.VarChar).Value = strImage;
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            con.Open();
            bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (j == true)
            {

                JavaScript.AlertAndRedirect("Sucess", "frmEmployeeData.aspx");
            }

            else
            {
                JavaScript.Alert("Fail");
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }


    }


    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmEmployeeData.aspx");
    }
}
