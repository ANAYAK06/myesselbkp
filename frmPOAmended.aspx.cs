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
using System.Web.Services;

public partial class Admin_frmPOAmended : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    string POno, AmdPono, AmdDate, AmdBasic, AmdTax, AmdTotal, date;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 29);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            btnPOAmended.Attributes.Add("onclick", "return POAmd()");
            ddlpo.Style.Add("visibility", "visible");
            ddlamdpo.Style.Add("visibility", "hidden");
            
        }
       

    }

    [WebMethod]
    public static string IsPOAvailable(string po_no)
    {

        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter("select po_no from po where po_no='" + po_no + "'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "checkDCA");
        conWB.Open();

        if (dsWB.Tables["checkDCA"].Rows.Count > 0)
        {
            string pono = dsWB.Tables[0].Rows[0].ItemArray[0].ToString();

            return pono;

        }

        else
        {
            string Npono = "PO is Not defined";
            return Npono;
        }

    }

    [WebMethod]
    public static string IsAmdAvailable(string po_no, string amdpo_no)
    {
       
        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter("select po_no,po_amendedno from po_amended  where po_no='" + po_no + "' and po_amendedno='" + amdpo_no + "'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "checkSubDCA");
        conWB.Open();
        if (dsWB.Tables["checkSubDCA"].Rows.Count >= 1)
        {
            string amd = dsWB.Tables[0].Rows[0].ItemArray[1].ToString();
            return amd;

        }

        else
        {
            string Namd ="Available";

            return Namd;
        }


    }

    protected void btnPOAmended_Click(object sender, EventArgs e)
    {
        POno = ddlpo.SelectedItem.Text;
        AmdPono = txtAmdPoNo.Text;
        AmdDate = txtAmdDate.Text;
        AmdBasic = txtAmdBasic.Text;
        AmdTax = txtAmdtax.Text;
        AmdTotal = txtTotal.Text;
        string obj = DateTime.Now.ToShortDateString();
        date = obj.ToString();
        try
        {
            da = new SqlDataAdapter("Select po_no from po_amended where po_amendedno=@AmdPOno", con);
            da.SelectCommand.Parameters.Add("@AmdPOno", SqlDbType.VarChar).Value = AmdPono;
            da.Fill(ds, "exsiting");
            if (ds.Tables["exsiting"].Rows.Count == 0)
            {
                cmd.Connection = con;
                cmd.CommandText = "insert into po_amended(po_no,po_amendedno,po_ammendedate,po_amendedbasicvalue,po_amendedservicetaxvalue,po_amendedtotalvalue,date)values(@POno,@AmdPOno,@AmdDate,@AmdBaisc,@AmdTax,@AmdTotal,@date)";
                cmd.Parameters.Add("@POno", SqlDbType.VarChar).Value = POno;
                cmd.Parameters.Add("@AmdPOno", SqlDbType.VarChar).Value = AmdPono;
                cmd.Parameters.Add("@AmdDate", SqlDbType.VarChar).Value = AmdDate;
                cmd.Parameters.Add("@AmdBaisc", SqlDbType.Money).Value = AmdBasic;
                cmd.Parameters.Add("@AmdTax", SqlDbType.Money).Value = AmdTax;
                cmd.Parameters.Add("@AmdTotal", SqlDbType.Money).Value = AmdTotal;
                cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = date;
                con.Open();
                int j = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (j == 1)
                {

                    showalert("Successfully Amended");
                }
                else
                {
                    showalert("Amending Failed");
                }

                con.Close();
            }
            else
            {
                JavaScript.Alert("Amended no already exist with " + ds.Tables["exsiting"].Rows[0]["po_no"].ToString());
            }
            
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }

    }  
        public void showalert(string message)
    {
        Label myalertlabel = new Label();
        myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "')</script>";
        Page.Controls.Add(myalertlabel);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {

            CascadingDropDown1.SelectedValue = "";
            CascadingDropDown3.SelectedValue = "";

            txtAmdPoNo.Text = "";
            txtAmdDate.Text = "";
            txtAmdBasic.Text = "";
            txtAmdtax.Text = "";
            txtTotal.Text = "";

        
        
        }


        protected void update_Click(object sender, EventArgs e)
        {
            try
         {
             cmd = new SqlCommand("update po_amended set po_no='"+ddlpo.SelectedItem.Text+"', po_ammendedate='" + txtAmdDate.Text + "',po_amendedno='" + txtAmdPoNo.Text + "',po_amendedbasicvalue='" + txtAmdBasic.Text + "',po_amendedservicetaxvalue='" + txtAmdtax.Text + "',po_amendedtotalvalue='" + txtTotal.Text + "' where po_amendedno='" +ddlamdpo.SelectedValue + "'", con);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            int k = cmd.ExecuteNonQuery();
            if (k == 1)
            {
                showalert("PO Amended Updated Sucessfully");
            }
            else
            {
                showalert("PO Amended Updated Updating Failed");
            }
        }
        catch (Exception ex)
        {

            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
            
        }

        protected void ddlamdpo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ddlamdpo.SelectedValue!="Select Amd PO")
            {
                da = new SqlDataAdapter("select po_ammendedate,po_amendedno,po_amendedbasicvalue,po_amendedservicetaxvalue,po_amendedtotalvalue from po_amended where po_amendedno='" +ddlamdpo.SelectedValue + "' and po_no='"+ddlpo.SelectedValue+"'", con);
                da.Fill(ds, "pono");
                txtAmdDate.Text = ds.Tables["pono"].Rows[0][0].ToString();
                txtAmdPoNo.Text = ds.Tables["pono"].Rows[0][1].ToString();
                txtAmdBasic.Text = ds.Tables["pono"].Rows[0][2].ToString();
                txtAmdtax.Text = ds.Tables["pono"].Rows[0][3].ToString();
                txtTotal.Text = ds.Tables["pono"].Rows[0][4].ToString();
                ddlamdpo.Style.Add("visibility", "visible");
                txtAmdPoNo.Style.Add("visibility", "hidden");
                btnPOAmended.Style.Add("visibility", "hidden");
                update.Style.Add("visibility", "visible");
                btnUpdCancel.Style.Add("visibility", "visible");
                btnCancel.Style.Add("visibility", "hidden");

            }
            
        }
        
}
