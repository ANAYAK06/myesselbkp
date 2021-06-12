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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;



public partial class UploadItems : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection("Data Source=184.168.84.199; Initial Catalog=ESSELDB; User ID=esseldbadmin; Password=E$$el@123$");
    //SqlConnection con1 = new SqlConnection("Data Source=184.168.84.199; Initial Catalog=esseldb; User ID=esseldbadmin; Password=E$$el@123$");
    //SqlConnection con1 = new SqlConnection("Data Source=INDUSTOU; Initial Catalog=essel200912; User ID=sa; Password=industouch");
    SqlCommand cmd = null;
    SqlDataAdapter da1;
    SqlDataReader dr;

    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    string p1, p2, p3, p4, p5, p6, p7, p8, count, venid, p9, p10;
    public string scode;
    string hid;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void venupload()
    {
        try
        {

            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = "c:/inetpub/vhosts/esselprojects.com/subdomains/quality/httpdocs/uploads/VSRN.xls";
                //string FilePath = "D:/New Folder/VSRN.xls";
                FileUpload1.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension);
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
      
    }
    private void Import_To_Grid(string FilePath, string Extension)
    {
        try
        {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, 1);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //string SheetName = dtExcelSchema.Rows[2]["TABLE_NAME"].ToString();
        connExcel.Close();
        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [CONSUMABLES$]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnupld_Click(object sender, EventArgs e)
    {
        venupload();


    }
    protected void btninsert_Click(object sender, EventArgs e)
    {



    }

    protected void ddlvendors_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btninsert_Click1(object sender, EventArgs e)
    {
        try
        {
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{


                //p1 = GridView1.Rows[i].Cells[0].Text.ToString().Substring(0, 1);//categorycode
                //p2 = GridView1.Rows[i].Cells[0].Text.ToString().Substring(1, 2);//major group code
                //p3 = GridView1.Rows[i].Cells[0].Text.ToString().Substring(3, 3);//subcategory code
                //p4 = GridView1.Rows[i].Cells[0].Text.ToString().Substring(6, 2);//specification code
                //p6 = GridView1.Rows[i].Cells[1].Text.ToString();//description
                //p7 = GridView1.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "null");//specification
                //p8 = GridView1.Rows[i].Cells[4].Text.ToString().Replace("&nbsp;", "0.00");//Basic price
                //p9 = GridView1.Rows[i].Cells[3].Text.ToString();//units
                //p10 = GridView1.Rows[i].Cells[0].Text.ToString();//itemcode
                //p1 = GridView1.Rows[i].Cells[0].Text.ToString();
                //p2 = GridView1.Rows[i].Cells[1].Text.ToString();
                //p3 = GridView1.Rows[i].Cells[2].Text.ToString();
                //p4 = GridView1.Rows[i].Cells[8].Text.ToString();
                //p1 = GridView1.Rows[i].Cells[0].Text.ToString();
                //p7 = GridView1.Rows[i].Cells[1].Text.ToString().Replace("&nbsp;", "null");
                //p8 = GridView1.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "0.00");
                //p9 = GridView1.Rows[i].Cells[3].Text.ToString();
                //p10 = GridView1.Rows[i].Cells[5].Text.ToString();



                //cmd = new SqlCommand("update invoice set client_id='" + p2 + "',subclient_id='" + p3 + "' where  invoiceno='" + p1 + "'", con1);

                //cmd.Connection = con1;
                //con1.Open();
                //cmd.ExecuteNonQuery();
                //con1.Close();
                //p1 = GridView1.Rows[i].Cells[0].Text.ToString().Substring(0, 1);//categorycode
                //p2 = GridView1.Rows[i].Cells[0].Text.ToString().Substring(1, 2);//major group code
                //p3 = GridView1.Rows[i].Cells[0].Text.ToString().Substring(3, 3);//subcategory code


                //p4 = GridView1.Rows[i].Cells[0].Text.ToString();//specification code
                //p6 = "CC-" + GridView1.Rows[i].Cells[1].Text.ToString();//description
                //p7 = GridView1.Rows[i].Cells[2].Text.ToString().Replace("&nbsp;", "0"); ;//specification
                //p8 = GridView1.Rows[i].Cells[4].Text.ToString().Replace("&nbsp;", "0.00");//Basic price
                //p9 = GridView1.Rows[i].Cells[3].Text.ToString();//units
                //p10 = GridView1.Rows[i].Cells[0].Text.ToString();//itemcode

                //cmd = new SqlCommand("INSERT INTO master_data VALUES('" + p4 + "','" + p6 + "','" + p7 + "')", con1);
                //int j = cmd.ExecuteNonQuery();
                //if (j == 1)
                //{

                //}
                //p1 = GridView1.Rows[i].Cells[0].Text.ToString();
                //p2 = GridView1.Rows[i].Cells[1].Text.ToString();
                //cmd = new SqlCommand("INSERT INTO [User Interfaces] VALUES('" + p2 + "','" + p1 + "')", con1);
                //int j = cmd.ExecuteNonQuery();
                //if (j == 1)
                //{

                //}

                //}
                //JavaScript.Alert("Successfully Inserted");
                string Itemcodes = "";
                string Qty = "";
                foreach (GridViewRow rw in GridView1.Rows)
                {  
                    Itemcodes = Itemcodes + rw.Cells[0].Text.ToString() + ",";
                    Qty = Qty + rw.Cells[5].Text.ToString() + ",";
                }
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Additems";
                cmd.Parameters.AddWithValue("@itemcodes", Itemcodes);
                cmd.Parameters.AddWithValue("@qtys", Qty);
                cmd.Connection = con1;
                con1.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con1.Close();
                if (msg == "Sucessfull")
                {
                    JavaScript.Alert("Successfully Updated");
                }
                else
                {
                    JavaScript.Alert("Updation Failed");
                }

            //}
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
  
}
