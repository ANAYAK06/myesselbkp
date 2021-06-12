using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
public partial class CertificatePreview : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //var type = Request.QueryString["type"].ToString();
            if (Request.QueryString["Type"] == "1")
            {
                tbl.Visible = false;

            }
            if (Request.QueryString["Type"] == "2")
            {
                tbl.Visible = true;

            }

            if (Request.QueryString["id"] != "0")
            {
                Image1.ImageUrl = "ShowImageHandler.ashx?ID=" + Request.QueryString["ID"].ToString() + "";
            }
            else
            {
                da = new SqlDataAdapter("Select Employee_id,ID,FileName from Employees_UploadDocuments where ed_uid='" + Session["id"].ToString() + "' and filename='" + Request.QueryString["value"].ToString() + "'", con);
                da.Fill(ds, "loadimg");
                if (ds.Tables["loadimg"].Rows.Count > 0)
                {
                    Image1.ImageUrl = "ShowImageHandler.ashx?ID=" + ds.Tables["loadimg"].Rows[0][1].ToString() + "";
                    string value = Request.QueryString["value"].ToString();
                }
                else
                {
                    string value = Request.QueryString["value"].ToString();
                }
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
        HttpFileCollection fileCollection = HttpContext.Current.Request.Files;

        for (int i = 0; i < fileCollection.Count; i++)
        {

            HttpPostedFile uploadfile = fileCollection[i];
            string fileName = Path.GetFileName(uploadfile.FileName);
            if (uploadfile.ContentLength > 0)
            {
                cmd.Parameters.Clear();
                byte[] imageBytes = new byte[uploadfile.ContentLength];
                uploadfile.InputStream.Read(imageBytes, 0, uploadfile.ContentLength);
                if (Request.QueryString["ID"].ToString() != "0")
                {
                    cmd.CommandText = "update employees_uploaddocuments set Image=@Image where id='" + Request.QueryString["ID"].ToString() + "'";
                    cmd.Parameters.AddWithValue("@Image", imageBytes);
                }
                else
                {
                    cmd.CommandText = "INSERT INTO employees_uploaddocuments(Employee_id,FileName,Image,status)VALUES (@Employee_id,@FileName,@Image,@status)";
                    cmd.Parameters.AddWithValue("@Employee_id", Session["Employee_id"].ToString());
                    cmd.Parameters.AddWithValue("@FileName", Request.QueryString["value"].ToString());
                    cmd.Parameters.AddWithValue("@Image", imageBytes);
                    cmd.Parameters.AddWithValue("@status", "1");
                }           
                cmd.Connection = con;
                con.Open();            
                bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (j == true)
                {
                    //JavaScript.UPAlertRedirect(Page, "Sucessfully Updated", "CertificatePreview.aspx");
                    showalert("Sucessfully Updated");
                }
               
                con.Close();



               
            }
        }
    }
    public void showalert(string message)
    {
        Label myalertlabel = new Label();
        myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "');window.close();</script>";
        Page.Controls.Add(myalertlabel);
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        JavaScript.CloseWindow();
    }
}