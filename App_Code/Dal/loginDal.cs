using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for loginDal
/// </summary>
public class loginDal
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
   
    
	public loginDal()
	{
		//
		// TODO: Add constructor logic here
		//
    }
        

    private string encryptPass(string pwd)
    {
        try
        {
            byte[] enc_pass = new byte[pwd.Length];
            enc_pass = System.Text.Encoding.UTF8.GetBytes(pwd);
            string encodePass = Convert.ToBase64String(enc_pass);
            return encodePass;


        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
    
    // --- Altaf Mohammed   10-11-2010 ---
    // --- function for inserting values into data base form registration table ---
    // Code Starts
    public int InsertQuery(string username, string firstname, string lastname, string encp, string phoneno, string email, string sex,string date,string s1)
    {
        int i = 0;
        try
        {
            cmd.Connection = con;
            cmd.CommandText = "insert into Register (user_Name,first_name,last_name,pwd,phone_no,mail_id,sex,date,status) values(@username,@firstname,@lastname,@pwd,@phoneno,@email,@sex,@date,@status)";
            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username ;
            cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = firstname;
            cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = lastname;
            cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = encp;
            cmd.Parameters.Add("@phoneno", SqlDbType.VarChar).Value = phoneno;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("@sex", SqlDbType.VarChar).Value = sex;
            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = date;
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "Pending";
           
            con.Open();
            i=Convert.ToInt32(cmd.ExecuteNonQuery());
            
            con.Close();
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }
        return i;

    }
    // code Ends









    public void checkLogin(ref DataSet ds,string user,string pass)
    {
        try
        {
            
            loginbll lgnbll = new loginbll();
            string uname = user;
            string encrpPwd = pass;
            
            da = new SqlDataAdapter("select  * from register where user_Name = '" + uname + "' and pwd = '" + encrpPwd +"'",con);
            
            da.Fill(ds);
       


        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }





    public void getRoles(ref DataSet ds1,string userN)
    {


        //da = new SqlDataAdapter("select a.roles,b.cc_code from user_roles a, cc_user b where a.User_Name = '" + userN + "' and b.User_name ='" + userN + "'", con);
        da = new SqlDataAdapter("select a.roles,b.cc_code from user_roles a left outer join  cc_user b on a.user_name=b.user_name where a.User_Name = '" + userN + "'", con);

        da.Fill(ds1);


        
    }

    //public int RoleCheck(string rolename, int page)
    //{
    //    try
    //    {
    //        da = new SqlDataAdapter("Select r.role_id,Page_id from roles r join [User Interfaces]u on r.role_id=u.role_id where role_name='" + rolename + "' and page_id='" + page + "'", con);
    //        da.Fill(ds, "pagecheck");
    //        return ds.Tables["pagecheck"].Rows.Count;
    //    }
    //    catch (Exception ex)
    //    {
    //        Utilities.CatchException(ex);
    //        JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
    //    }
    //}



    
}

