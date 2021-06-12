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



public partial class Addloandetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    string S="";
    string S1 = "";
    string S2 = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAddagency_Click(object sender, EventArgs e)
    {
        try
        {
            if(txtdisposalamt.Text=="")
                txtdisposalamt.Text="0";
             if(txtprocessingcrg.Text=="")
                txtprocessingcrg.Text="0";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTermLoan_sp";
            cmd.Parameters.AddWithValue("@Agencycode", ddlagencycode.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Loanno", txtloanno.Text);
            cmd.Parameters.AddWithValue("@LoanPurpose", txtloanpurpose.Text);
            cmd.Parameters.AddWithValue("@DispAmount", txtdisposalamt.Text);
            cmd.Parameters.AddWithValue("@PrcAmount", txtprocessingcrg.Text);
            cmd.Parameters.AddWithValue("@Amount", txtdisposalamt.Text);
            cmd.Parameters.AddWithValue("@Principleamount", (Convert.ToDecimal(txtdisposalamt.Text) + Convert.ToDecimal(txtprocessingcrg.Text)).ToString());
            cmd.Parameters.AddWithValue("@Interestrate", txtinterestrate.Text);
            cmd.Parameters.AddWithValue("@No_of_Installments", txtnoofinst.Text);
            cmd.Parameters.AddWithValue("@BalanceInst", txtnoofinst.Text);
            cmd.Parameters.AddWithValue("@Inststartdate", txtinststartdate.Text);
            cmd.Parameters.AddWithValue("@Instenddate", txtinstenddate.Text);
            cmd.Parameters.AddWithValue("@Createddate", txtapplydate.Text);

            if (ddlloantype.SelectedItem.Text == "For Capital")
            {
                cmd.Parameters.AddWithValue("@chequeno", txtchequeno.Text);
                cmd.Parameters.AddWithValue("@Bankname", ddlbankname.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Modeofpay", ddlpayment.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", txtdate.Text);
            }

            cmd.Parameters.AddWithValue("@status", "1");
            cmd.Parameters.AddWithValue("@loantype", ddlloantype.SelectedItem.Text);            
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());            
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Successfully Added")
            {
                JavaScript.UPAlertRedirect(Page, "Successfully Added", "Addloandetails.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
            con.Close();
        }

        catch (Exception ex)
        {

            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnresetagency_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addloandetails.aspx");
    }
    protected void ddlloantype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlloantype.SelectedItem.Text == "For Capital")
        {
            Paymentdetails.Visible = true;
            Pay.Visible = true;
           
        }
        else if (ddlloantype.SelectedItem.Text == "For Purchase Of Assets")
        {
            Paymentdetails.Visible = false;
            Pay.Visible =  false;
        }
    }
}
