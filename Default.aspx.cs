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
using System.Text;
using AjaxControlToolkit;
using System.Net.Mail;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    string f = ConfigurationManager.AppSettings["mailfrom"].ToString();
    string t = ConfigurationManager.AppSettings["mailcr"].ToString();
    string id = ConfigurationManager.AppSettings["mailid"].ToString();
    string psw = ConfigurationManager.AppSettings["mailpassword"].ToString();
    string smtpaddress = ConfigurationManager.AppSettings["smtpaddress"].ToString();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnsubmit.Attributes.Add("onclick", "return loginvalidation()");

        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {

            loginbll lgnBll = new loginbll();

            DataSet ds = new DataSet();
            //usersLogin usrlgn = new usersLogin();

            lgnBll.Username = txtusername.Text;
            lgnBll.Password = txtpwd.Text;
            lgnBll.Role = "";
            ds = lgnBll.checkLogin(lgnBll);
            // string res = objLgnbl.loginBll(uname, pwd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if ("Pending" == ds.Tables[0].Rows[0].ItemArray[8].ToString())
                {
                    ModalPopupExtender1.Hide();
                    ModalPopupExtender.Show();
                }
                else
                {
                    DataSet ds1 = new DataSet();
                    Session["user"] = txtusername.Text;
                    Session["created_by"] = txtusername.Text;
                    Session["modified_by"] = txtusername.Text;
                    Session["modified_date"] = System.DateTime.Now;
                    ds1 = lgnBll.checkRoles(lgnBll);
                    if (ds1.Tables[0].Rows.Count >= 1)
                    {
                        string role = ds1.Tables[0].Rows[0][0].ToString();
                        if (role == "Accountant/StoreKeeper" || role == "Sr.Accountant/Central Store Keeper")
                        {
                            Session["CC_CODE"] = ds1.Tables[0].Rows[0][1].ToString();
                            ViewState["role"] = role;
                            filldrop();
                            ModalPopupExtender1.Show();
                            ModalPopupExtender.Hide();
                            //Session["CC_CODE"] = ds1.Tables[0].Rows[0][1].ToString();
                            //Response.Redirect("MenuContents.aspx", false);
                        }
                        if (role == "Project Manager")
                        {
                            Session["roles"] = role;
                            Response.Redirect("MenuContents.aspx", false);
                        }
                        else if (role != "Accountant/StoreKeeper" && role != "Sr.Accountant/Central Store Keeper" && role != "Project Manager")
                        {
                            Session["CC_CODE"] = ds1.Tables[0].Rows[0][1].ToString();
                            Session["roles"] = role;
                            Response.Redirect("MenuContents.aspx", false);
                        }
                    }
                    else
                    {
                        lblAlert1.Text = "You Are Not Been Authorised By Admin To Access Account. Wait For Authorization..... ";
                    }
                }
            }
            else if (ds.Tables[0].Rows.Count < 1)
            {
                da = new SqlDataAdapter("select username,user_type,password from itmsregister where username='"+txtusername.Text+"'", con);
                da.Fill(ds, "itms");
                if (ds.Tables["itms"].Rows.Count > 0)
                {

                    if (txtusername.Text == ds.Tables["itms"].Rows[0]["username"].ToString() && txtpwd.Text == ds.Tables["itms"].Rows[0]["password"].ToString())
                    {
                        Session["user"] = ds.Tables["itms"].Rows[0]["username"].ToString();
                        Session["roles"] = ds.Tables["itms"].Rows[0]["user_type"].ToString();
                        Response.Redirect("MenuContents.aspx", false);
                    }
                }
            }
            else
            {

                lblAlert.Text = "Invalid User Name or Password";
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {

        }
    }
    protected void OkButton_Click(object sender, EventArgs e)
    {
        try
        {
            loginbll objLgnDal = new loginbll();
            string encrpOPwd = objLgnDal.encryptPass(txtold.Text);
            string encrpNPwd = objLgnDal.encryptPass(txtnew.Text);

            con.Open();

            cmd = new SqlCommand("UPDATE REGISTER SET pwd='" + encrpNPwd + "',status='Approved' WHERE User_Name='" + txtusername.Text + "' and pwd = '" + encrpOPwd + "'", con);
            bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (j == true)
            {

                JavaScript.UPAlertRedirect(Page,"Password Changed Successfully", "Default.aspx");

            }
            else
            {
                JavaScript.UPAlert(Page,"Please Enter Correct OldPassword");

            }

            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnmdlsubmit_Click(object sender, EventArgs e)
    {
        Session["roles"] = ddlrole.SelectedItem.Text;
        Response.Redirect("MenuContents.aspx", false);
    }
    public void filldrop()
    {
        try
        {
            ddlrole.Items.Clear();
            if (ViewState["role"].ToString() == "Sr.Accountant/Central Store Keeper")
            {
                ddlrole.Items.Add("Select Role");
                ddlrole.Items.Add("Sr.Accountant");
                ddlrole.Items.Add("Central Store Keeper");
            }
            else
            {
                ddlrole.Items.Add("Select Role");
                ddlrole.Items.Add("Accountant");
                ddlrole.Items.Add("StoreKeeper");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public string decrypttPass(string pwd)
    {
        try
        {           
            string decryptpwd = string.Empty;

            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(pwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;

        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    
    {
        try
        {
            string pwd = "";
            string decrptwd = "";
            string mailid="";
             
            DataSet ds = new DataSet();
            da = new SqlDataAdapter("sp_forgot_password", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@User_Name", SqlDbType.VarChar).Value = txtuserid.Text;
            da.SelectCommand.Parameters.AddWithValue("@mail_id", SqlDbType.VarChar).Value = txtemailid.Text;
            da.Fill(ds, "validation");
            if (ds.Tables["validation"].Rows.Count > 0)
            {

                if (ds.Tables["validation"].Rows[0][0].ToString() == "INVALID MAIL ID" || ds.Tables["validation"].Rows[0][0].ToString() == "INVALID USER NAME")
                {
                    lblvmsg.Text = ds.Tables["validation"].Rows[0][0].ToString();
                    ModalPopupExtender2.Show();
                }
                else
                {

                    try
                    {
                        pwd = ds.Tables["validation"].Rows[0][0].ToString();
                        decrptwd = decrypttPass(pwd);
                        MailMessage Msg = new MailMessage();
                        Msg.From = new MailAddress("it-support@esselprojects.com", "Essel Support");
                        Msg.To.Add(txtemailid.Text);
                        Msg.Subject = "Password Recovery";
                        Msg.Body = "Hi,";
                        Msg.Body += "<div>";
                        Msg.Body += "<table cellpadding='4' cellspacing='0'>";
                        Msg.Body += "<tr><td colspan='2' style='align:center'><div>  Your login Details: </div></td></tr>";
                        Msg.Body += "<tr><td colspan='2' style='align:center'><div> User id  :  " + txtuserid.Text + "</div></td></tr>";
                        Msg.Body += "<tr><td colspan='2' style='align:center'><div> password : " + decrptwd.ToString() + "</div></td></tr>";
                        Msg.Body += "</table>";
                        Msg.Body += "</div>";
                        Msg.IsBodyHtml = true;
                        Msg.DeliveryNotificationOptions.ToString();
                        //SmtpClient smtp = new SmtpClient();
                        //smtp.Host = smtpaddress;
                        //smtp.Port = 587;
                        //smtp.Credentials = new System.Net.NetworkCredential(id.ToString(), psw.ToString());
                        //smtp.EnableSsl = true;
                        //smtp.Send(Msg);
                        SmtpClient smtp = new SmtpClient("127.0.0.1");
                        smtp.Port = 25;
                        smtp.Send(Msg);
                        Msg = null;

                    }
                    catch (Exception ex)
                    {
                        Response.Write("error is" + ex);
                    }

                    
                    JavaScript.UPAlertRedirect(Page, "Your password has been sent to your registered mailid, please check", "Default.aspx");
                }
                //JavaScript.UPAlertRedirect(Page, "Your password hasbeen sent to your registered mailid, please check", "Default.aspx");
            }


        }
        catch (Exception)
        {
        }
        finally
        {
             
        }
    }

    
}
