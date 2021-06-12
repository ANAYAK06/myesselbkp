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
/// Summary description for esselDal
/// </summary>
public class esselDal
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();

    

	public esselDal()
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
    public void InsertQuery(string username,string firstname,string lastname,string pwd,string cpwd,string phoneno,string email,string sex)
    {
        try
        {
            string Nuser = "";
            cmd.Connection = con;
            cmd.CommandText = "select count(user_Name) from register";
            con.Open();
            int j = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            j = j + 1;
            if (j >= 1 && j <= 9)
            {
                Nuser = "ESE0000" + j;
            }
            else if (j >= 10 && j <= 99)
            {
                Nuser = "ESE000" + j;
            }
            else if (j >= 100 && j <= 999)
            {
                Nuser = "ESE00" + j;
            }
            else if (j >= 1000 && j <= 9999)
            {
                Nuser = "ESE0" + j;
            }
           
            cmd.Connection = con;
            cmd.CommandText = "insert into Register (user_Name,first_name,last_name,pwd,cpwd,phone_no,mail_id,sex) values(@username,@firstname,@lastname,@pwd,@cpwd,@phoneno,@mailid,@sex)";
            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = Nuser;
            cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = firstname;
            cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = lastname;
            cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = pwd;
            cmd.Parameters.Add("@cpwd", SqlDbType.VarChar).Value = cpwd;
            cmd.Parameters.Add("phoneno", SqlDbType.VarChar).Value = phoneno;
            cmd.Parameters.Add("email", SqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("sex", SqlDbType.VarChar).Value = sex;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }
        
        catch (Exception ex)
        {
        }

    }
    public int RoleCheck(string role, int page)
    {
        da = new SqlDataAdapter("Select r.role_id,Page_id from roles r join [User Interfaces]u on r.role_id=u.role_id where role_name='" + role + "' and page_id='" + page + "'", con);
        da.Fill(ds, "pagecheck");
        return ds.Tables["pagecheck"].Rows.Count;

    }



    







}
