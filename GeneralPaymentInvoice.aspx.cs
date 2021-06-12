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

public partial class GeneralPaymentInvoice : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esseldb"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");

        }
        if(!IsPostBack)
        {
            tdsubtype.Visible=false;
            tdtype.Visible = false;
            ddlno.Visible = false;
          
         
          
            ddlcctype.SelectedIndex = 0;
            ddlcccode.Items.Insert(0, "Select Cost Center");
            ddldetailhead.Items.Insert(0, "Select DCA Code");
            ddlsubdetail.Items.Insert(0, "Select SubDCA Code");
           
        }
    }
    public void fillexcise()
    {
        try
        {
            if (ddldetailhead.SelectedItem.Text == "DCA-Excise")
            da = new SqlDataAdapter("select Excise_no as no from ExciseMaster where Status='3'", con);
            else if (ddldetailhead.SelectedItem.Text == "DCA-VAT")
                da = new SqlDataAdapter("select RegNo as no from [Saletax/VatMaster] where Status='3'", con);
            else if (ddldetailhead.SelectedItem.Text == "DCA-SRTX")
                da = new SqlDataAdapter("select ServiceTaxno as no from ServiceTaxMaster where Status='3'", con);
            else if (ddldetailhead.SelectedItem.Text == "DCA-GST-CR")
                da = new SqlDataAdapter("select GST_No as no from GSTmaster where Status='3'", con);
            da.Fill(ds,"nos");
            if (ds.Tables["nos"].Rows.Count > 0)
            {

                ddlno.DataValueField = "no";
                ddlno.DataTextField = "no";
                ddlno.DataSource = ds.Tables["nos"];
                ddlno.DataBind();
                ddlno.Items.Insert(0, "Select");
            }
            else
            {
                ddlno.Items.Insert(0, "Select");
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GeneralInvoice_sp";
            cmd.Parameters.Add(new SqlParameter("@CCCode", ddlcccode.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@DCACode", ddldetailhead.SelectedItem.Text));
            if (Convert.ToInt32(ViewState["RowCount"].ToString())!=0)
            {
                cmd.Parameters.Add(new SqlParameter("@SubDCA_Code", ddlsubdetail.SelectedItem.Text));
            }
            cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));
            cmd.Parameters.Add(new SqlParameter("@Name", txtname.Text));
            cmd.Parameters.Add(new SqlParameter("@Remarks", txtremarks.Text));
            cmd.Parameters.Add(new SqlParameter("@Amount", txtamount.Text));
            if (ddldetailhead.SelectedItem.Text == "DCA-Excise")
            {
                cmd.Parameters.Add(new SqlParameter("@Exciseno", ddlno.SelectedItem.Text));
            }
            else if (ddldetailhead.SelectedItem.Text == "DCA-VAT")
            {
                cmd.Parameters.Add(new SqlParameter("@VATno", ddlno.SelectedItem.Text));
            }
            else if (ddldetailhead.SelectedItem.Text == "DCA-SRTX")
            {
                cmd.Parameters.Add(new SqlParameter("@SRTXno", ddlno.SelectedItem.Text));
            }
            else if (ddldetailhead.SelectedItem.Text == "DCA-GST-CR")
            {
                cmd.Parameters.Add(new SqlParameter("@GSTno", ddlno.SelectedItem.Text));
            }
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@roles", Session["roles"].ToString()));
            //New parameter created date 15-June-2016 Cr-ENH-APR-002-2016 by kishore STARTS 
            cmd.Parameters.Add(new SqlParameter("@paymentmode", ddlmodeofpay.SelectedItem.Text));
            //New parameter created date 15-June-2016 Cr-ENH-APR-002-2016 by kishore Ends 
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "You are not Assign the DCA Budget" || msg == "Insufficient DCA Budget" || msg == "This DCA is Under Amendment" || msg == "Invalid Date")
              JavaScript.UPAlert(Page,msg);
            else

             showalertprint("Succesfull",msg);
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
    public void showalertprint(string message, string Voucherid)
    {
        Label myalertlabel = new Label();
        Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "1", "selfinvoiceformatforgeneralpayment.aspx");
        myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "');window.location ='GeneralPaymentinvoice.aspx';window.open('selfinvoiceformatforgeneralpayment.aspx?Voucherid=" + Voucherid + "','Report','width=560,height=230,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');</script>";
        Page.Controls.Add(myalertlabel);
    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lbldca.Text = "";
            lblsubdca.Text = "";
            ddldetailhead.Items.Clear();
            ddlsubdetail.Items.Clear();
            ddldetailhead.Items.Insert(0, "Select DCA Code");
            ddlsubdetail.Items.Insert(0, "Select SubDCA Code");
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                tdsubtype.Visible = true;
                tdtype.Visible = true;
                ddltype.SelectedIndex = 0;
                ddlcccode.Items.Clear();
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
            else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
            {
                ddlcccode.Items.Clear();
                da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.cc_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                da.Fill(ds, "SERVICECC");
                ddlcccode.DataTextField = "Name";
                ddlcccode.DataValueField = "cc_code";
                ddlcccode.DataSource = ds.Tables["SERVICECC"];
                ddlcccode.DataBind();

                ddlcccode.Items.Insert(0, "Select Cost Center");
                tdsubtype.Visible = false;
                tdtype.Visible = false;
            }
            else if (ddlcctype.SelectedItem.Text == "Select")
            {
                ddlcccode.Items.Clear();
                ddlcccode.Items.Insert(0, "Select Cost Center");
                tdsubtype.Visible = false;
                tdtype.Visible = false;
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
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblsubdca.Text = "";
            lbldca.Text = "";
            ddldetailhead.Items.Clear();
            ddlsubdetail.Items.Clear();
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                if (ddltype.SelectedItem.Text == "Service")
                {
                    da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.cc_subtype='Service' and c.cc_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                }
                else if (ddltype.SelectedItem.Text == "Trading")
                {
                    da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.cc_subtype='Trading' and c.cc_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                }
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                {
                    da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.cc_subtype='Manufacturing'  and c.cc_type='" + ddlcctype.SelectedItem.Text + "' and c.status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                }
            }
            da.Fill(ds, "SERVICECC");
            ddlcccode.DataTextField = "Name";
            ddlcccode.DataValueField = "cc_code";
            ddlcccode.DataSource = ds.Tables["SERVICECC"];
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "Select Cost Center");
            lbldca.Text = "";
            ddldetailhead.Items.Insert(0, "Select DCA Code");
            ddlsubdetail.Items.Insert(0, "Select SubDCA Code");
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
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("GeneralPaymentInvoice.aspx");
    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlcctype.SelectedItem.Text == "Performing" || ddlcctype.SelectedItem.Text == "Non-Performing")
            //{
            //    da = new SqlDataAdapter("select distinct dca_code from dca where dca_code not in ('DCA-11','DCA-27','DCA-29','DCA-37','DCA-38','DCA-39','DCA-SRTX','DCA-Excise')", con);
            //}
            //else if (ddlcctype.SelectedItem.Text == "Capital")
            //{
            //    da = new SqlDataAdapter("select distinct dca_code from dca where dca_code in ('DCA-27','DCA-29','DCA-37','DCA-38','DCA-39','DCA-SRTX','DCA-Excise')", con);
            //}
            ////da = new SqlDataAdapter("Select distinct dca_code from dcatype where cc_type in (Select cc_subtype FROM cost_center where cc_code='" + ddlcccode.SelectedValue + "')  and status='Active'", con);
            da = new SqlDataAdapter("Select distinct d.dca_code from dcatype d join dca_paymenttype dp ON d.dca_code=dp.dca_code where cc_type in (Select cc_subtype FROM cost_center where cc_code='" + ddlcccode.SelectedValue + "')  and d.status='Active' AND dp.paymenttype='Direct payment' AND dp.status='Active'", con);
            da.Fill(ds, "DCA");
            ddldetailhead.DataTextField = "dca_code";
            ddldetailhead.DataValueField = "dca_code";
            ddldetailhead.DataSource = ds.Tables["DCA"];
            ddldetailhead.DataBind();
            ddldetailhead.Items.Insert(0, "Select DCA Code");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    
    }
    protected void ddldetailhead_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            lblsubdca.Text = "";
            ddlsubdetail.Items.Clear();
            da = new SqlDataAdapter("select dca_name from dca where dca_code='" + ddldetailhead.SelectedItem.Text + "';select distinct subdca_code from subdca where dca_code='" + ddldetailhead.SelectedItem.Text + "'", con);
           // da = new SqlDataAdapter("select distinct subdca_code from subdca where dca_code='" + ddldetailhead.SelectedItem.Text + "'", con);
            da.Fill(ds, "Dcaname");
           // da.Fill(ds, "subdcacode");
            if (ds.Tables["Dcaname"].Rows.Count > 0)
            {
                lbldca.Text = ds.Tables["Dcaname"].Rows[0]["dca_name"].ToString();
            }
            if (ds.Tables["Dcaname1"].Rows.Count > 0)
            {
                ViewState["RowCount"] = ds.Tables["Dcaname1"].Rows.Count;
                ddlsubdetail.DataTextField = "subdca_code";
                ddlsubdetail.DataValueField = "subdca_code";
                ddlsubdetail.DataSource = ds.Tables["Dcaname1"];
                ddlsubdetail.DataBind();
                ddlsubdetail.Items.Insert(0, "Select SubDCA Code");
                tdsub.Visible = true;
                tdsub1.Visible = true;
                if (ddldetailhead.SelectedItem.Text == "DCA-Excise" || ddldetailhead.SelectedItem.Text == "DCA-VAT" || ddldetailhead.SelectedItem.Text == "DCA-SRTX" || ddldetailhead.SelectedItem.Text == "DCA-GST-CR")
                {
                    fillexcise();
                    ddlno.Visible = true;
                }
               
                else
                {
                    ddlno.Visible = false;
                }
            }
            else if(ds.Tables["Dcaname1"].Rows.Count < 1)
            {
                tdsub.Visible = false;
                tdsub1.Visible = false;
                ddlno.Visible = false;            
                ViewState["RowCount"] =0;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
       
    }
    protected void ddlsubdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            da = new SqlDataAdapter("select subdca_name from subdca where subdca_code='"+ddlsubdetail.Text+"'", con);
            da.Fill(ds, "SubDcaname");
            if (ds.Tables["SubDcaname"].Rows.Count > 0)
            {
                lblsubdca.Text = ds.Tables["SubDcaname"].Rows[0]["subdca_name"].ToString();
            }
          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
       

    }
}
