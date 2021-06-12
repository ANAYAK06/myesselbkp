<%@ WebHandler Language="C#" Class="ShowImageHandler" %>

using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.SessionState;

public class ShowImageHandler : IHttpHandler,IRequiresSessionState
{

    SqlConnection connStr = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["ID"] == null) return;
        {

            int pictureId =Convert.ToInt32(context.Request.QueryString["ID"]);
            cmd = new SqlCommand("Select Image from Employees_UploadDocuments Where ID = @id", connStr);
            cmd.Parameters.Add(new SqlParameter("@id", pictureId));
            connStr.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            {
                if (reader.Read())
                {
                    context.Response.BinaryWrite((Byte[])reader[reader.GetOrdinal("Image")]);
                    reader.Close();
                }
            }
        }
    }        
    public bool IsReusable
    {
        get 
        {
            return true;
        }
    }

}