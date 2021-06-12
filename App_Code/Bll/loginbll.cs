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

/// <summary>
/// Summary description for loginbll
/// </summary>
public class loginbll
{

    private string strUsername;
    private string strPassword;
    private string strRole;
    

    public string Username
    {
        get
        {

            return strUsername;
        }
        set
        {
            strUsername = value;
        }
    }
    public string Password
    {
        get
        {

            return strPassword;
        }
        set
        {
            strPassword = value;
        }
    }
    public string Role
    {
        get
        {

            return strRole;
        }
        set
        {
            strRole = value;
        }
    }

    public string encryptPass(string pwd)
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

 


    public DataSet checkLogin(loginbll objlgnbll)
    {
        DataSet ds = new DataSet();
        loginDal objLgnDal = new loginDal();
        try
        {

            string encrpPwd = encryptPass(objlgnbll.Password);
            objlgnbll.Password = encrpPwd;
            objLgnDal.checkLogin(ref ds,objlgnbll.Username,encrpPwd);
            
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
        return ds;

    }



    public DataSet checkRoles(loginbll lgnBll)
    {
        DataSet ds = new DataSet();
        loginDal objLgnDal = new loginDal();
        try
        {
            objLgnDal.getRoles(ref ds,lgnBll.Username);
            
            

        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
        return ds;
    }

    public int RegInsert(string username, string firstname, string lastname, string password, string phoneno, string email, string sex,string s1)
    {

        loginDal regDal = new loginDal();
        int res = 0;
        try
        {

            string encp = encryptPass(password);
            string date = DateTime.Now.ToShortDateString();
            res = regDal.InsertQuery(username, firstname, lastname, encp, phoneno, email, sex, date, s1);
   


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        return res;


    }

    //public int RoleCheckBL(string role, int page)
    //{
    //    loginDal Rolecheckdal = new loginDal();
    //    int rec;
    //    rec = Rolecheckdal.RoleCheck(role, page);
    //    return rec;
    //}
}


