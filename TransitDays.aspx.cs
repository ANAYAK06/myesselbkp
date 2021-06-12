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


public partial class TransitDays : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["user"] == null)
            {
                Response.Redirect("SessionExpire.aspx", false);

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

        if (!IsPostBack)
        {
            fillgrid();

        }
    }

    public void fillgrid()
    {
        try
        {
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
            da = new SqlDataAdapter("Select distinct indent_no,Ti.Ref_no,Ti.cc_code,recieved_cc,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,REPLACE(CONVERT(VARCHAR(11),convert(datetime,Expecteddate,101),106), ' ', '-')as Expecteddate from [Transfer Info]Ti join [Items Transfer]It on Ti.Ref_no=It.Ref_no   where  DATEDIFF(day, Ti.Transfer_Date, Ti.Expecteddate)< DATEDIFF(day, Ti.Transfer_Date, '" + myDate.ToString() + "') and Status='4' and ti.indent_no is not NULL and  [type]='2' union all Select distinct indent_no,Ti.Ref_no,Ti.cc_code,recieved_cc,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,REPLACE(CONVERT(VARCHAR(11),convert(datetime,Expecteddate,101),106), ' ', '-')as Expecteddate from [Transfer Info]Ti join [Items Transfer]It on Ti.Ref_no=It.Ref_no   where  DATEDIFF(day, Ti.Transfer_Date, Ti.Expecteddate)< DATEDIFF(day, Ti.Transfer_Date, '" + myDate.ToString() + "') and Status='3A' and ti.indent_no is  NULL and  [type]='2' union all Select distinct indent_no,Ti.Ref_no,Ti.cc_code,recieved_cc,REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date,REPLACE(CONVERT(VARCHAR(11),convert(datetime,Expecteddate,101),106), ' ', '-')as Expecteddate from [Transfer Info]Ti join [Items Transfer]It on Ti.Ref_no=It.Ref_no   where  DATEDIFF(day, Ti.Transfer_Date, Ti.Expecteddate)< DATEDIFF(day, Ti.Transfer_Date, '" + myDate.ToString() + "') and Status='2' and  [type]='1'", con);
            da.Fill(ds, "filldata");
            grdviewtransit.DataSource = ds.Tables["filldata"];
            grdviewtransit.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void checkapprovals(string cc)
    {

        da = new SqlDataAdapter("SELECT TOP 1 * from  closedcost_center where cc_code = '" + cc + "' and status not in('Rejected') ORDER by id desc", con);
        da.Fill(ds, "check");
        if (ds.Tables["check"].Rows.Count > 0)
        {
            if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "1")
            {
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                    JavaScript.AlertAndRedirect("Temporary Closed store approval pending at project Manager", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                    JavaScript.AlertAndRedirect("Store Re-open approval pending at project Manager", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                    JavaScript.AlertAndRedirect("Store permanent closed approval pending at project Manager", "WareHouseHome.aspx");
            }
            if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "2")
            {
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                    JavaScript.AlertAndRedirect("Temporary Closed store approval pending at central store keeper", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                    JavaScript.AlertAndRedirect("Store Re-open approval pending at central store keeper", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                    JavaScript.AlertAndRedirect("Store permanent closed approval pending at central store keeper", "WareHouseHome.aspx");
            }
            if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "3")
            {
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                    JavaScript.AlertAndRedirect("Store is in Temporary Closing Mode", "WareHouseHome.aspx");
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                    JavaScript.AlertAndRedirect("Store permanent closed approval pending at chief Material Controller", "WareHouseHome.aspx");
            }
            if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "4")
            {
                if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                    JavaScript.AlertAndRedirect("Store is Already closed", "WareHouseHome.aspx");
            }
        }

    }
    protected void grdviewtransit_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            checkapprovals(grdviewtransit.Rows[e.NewEditIndex].Cells[4].Text);
            ViewState["Refno"] = grdviewtransit.DataKeys[e.NewEditIndex]["Ref_no"].ToString();
            ViewState["expdate"] = grdviewtransit.Rows[e.NewEditIndex].Cells[6].Text;
            ViewState["indentno"] = grdviewtransit.Rows[e.NewEditIndex].Cells[2].Text;
            lblindno.Text = grdviewtransit.Rows[e.NewEditIndex].Cells[2].Text;
            lblrecivedcc.Text = grdviewtransit.Rows[e.NewEditIndex].Cells[4].Text;
            if (grdviewtransit.Rows[e.NewEditIndex].Cells[4].Text == "CC-33")
            {
                btnno.Visible = false;
            }
            else
            {
                btnno.Visible = true;
            }
            lblupstock.Text = grdviewtransit.Rows[e.NewEditIndex].Cells[4].Text;
            lblind.Text = grdviewtransit.Rows[e.NewEditIndex].Cells[2].Text;
            tbl1.Visible = true;
            tbl2.Visible = false;
            Tbl3.Visible = false;
            poppo.Show();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void btnyes_Click(object sender, EventArgs e)
    {
        tbl1.Visible = false;
        tbl2.Visible = true;
        Tbl3.Visible = false;

    }
    protected void btnno_Click(object sender, EventArgs e)
    {
        tbl1.Visible = false;
        tbl2.Visible = false;
        Tbl3.Visible = true;
    }

  

    protected void btnclick_Click(object sender, EventArgs e)
    {
        string url = "Transitdetails.aspx?refno=" + ViewState["Refno"].ToString() + "";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('" + url + "','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');", true);      
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        try
        {
            
            int days = Convert.ToInt32(ddlDays.SelectedItem.Text);
            cmd.Connection = con;
            cmd.CommandText = "update [Transfer Info] set expecteddate=DATEADD(DAY,  " + Convert.ToInt32(days.ToString()) + ", '" + ViewState["expdate"].ToString() + "') where Ref_no='" + ViewState["Refno"].ToString() + "'";
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                JavaScript.UPAlertRedirect(Page, "Date Extended", "TransitDays.aspx");
           
              
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, "Failed", "TransitDays.aspx");
            }
            fillgrid();
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        tbl1.Visible = true;
        tbl2.Visible = false;
        Tbl3.Visible = false;
    }
    protected void btnupdok_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
            cmd = new SqlCommand("stockupdationpopup_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Refno", ViewState["Refno"].ToString());
            cmd.Parameters.AddWithValue("@date", myDate);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Sucessfull")
            {
                JavaScript.UPAlertRedirect(Page, "Successfully Updated", "TransitDays.aspx");
           
               
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, msg, "TransitDays.aspx");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnupdno_Click(object sender, EventArgs e)
    {
        poppo.Hide();
    }
}
