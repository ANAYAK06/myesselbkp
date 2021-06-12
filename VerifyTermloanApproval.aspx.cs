using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class VerifyTermloanApproval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;

    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
            tblloandetails.Visible = false;
        }
    }

    public void FillGrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select i.* from (select Id,'CCC' as cc_code,loanpurpose,REPLACE(CONVERT(VARCHAR(11),inststartdate, 106), ' ', '-')as inststartdate,balance as amount,'L' as type from TermLoan where Status='1')i where i.id>0  order by i.inststartdate", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select i.* from (select Id,'CCC' as cc_code,loanpurpose,REPLACE(CONVERT(VARCHAR(11),inststartdate, 106), ' ', '-')as inststartdate,balance as amount,'L' as type from TermLoan where Status='2' )i where i.id>0 order by i.inststartdate", con);
            da.Fill(ds, "FillIn");
            GridView1.DataSource = ds.Tables["FillIn"];
            GridView1.DataBind();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            ViewState["Id"] = GridView1.SelectedValue.ToString();
            if (Session["roles"].ToString() == "HoAdmin")
            {
                da = new SqlDataAdapter("select t.id,t.Agencycode,t.Loanno,t.LoanPurpose,cast( t.Amount as decimal(10,2)) as Amount,t.balance,t.Interestrate,t.No_of_Installments,REPLACE(CONVERT(VARCHAR(11),t.inststartdate, 106), ' ', '-')as inststartdate,REPLACE(CONVERT(VARCHAR(11),t.Instenddate, 106), ' ', '-')as Instenddate,b.Bank_name,b.No,t.loantype,REPLACE(CONVERT(VARCHAR(11),b.date, 106), ' ', '-')as date,b.ModeOfPay,REPLACE(CONVERT(VARCHAR(11),t.Createddate, 106), ' ', '-')as Createddate,cast(disposal_amt as decimal(10,2))as disposal_amt,cast(processing_amt as decimal(10,2)) as processing_amt from TermLoan t left join bankbook b on b.loanno=t.loanno  where t.id='" + GridView1.SelectedValue.ToString() + "' and t.status='1'", con);
            }
            else 
            {
                da = new SqlDataAdapter("select t.id,t.Agencycode,t.Loanno,t.LoanPurpose,cast( t.Amount as decimal(10,2)) as Amount,t.balance,t.Interestrate,t.No_of_Installments,REPLACE(CONVERT(VARCHAR(11),t.inststartdate, 106), ' ', '-')as inststartdate,REPLACE(CONVERT(VARCHAR(11),t.Instenddate, 106), ' ', '-')as Instenddate,b.Bank_name,b.No,t.loantype,REPLACE(CONVERT(VARCHAR(11),b.date, 106), ' ', '-')as date,b.ModeOfPay,REPLACE(CONVERT(VARCHAR(11),t.Createddate, 106), ' ', '-')as Createddate,cast(disposal_amt as decimal(10,2))as disposal_amt,cast(processing_amt as decimal(10,2)) as processing_amt from TermLoan t left join bankbook b on b.loanno=t.loanno  where t.id='" + GridView1.SelectedValue.ToString() + "' and t.status='2'", con);
            }
            da.Fill(ds, "check");
            if (ds.Tables["check"].Rows.Count > 0)
            {
                tblloandetails.Visible = true;
                txtagencycode.Text = ds.Tables["check"].Rows[0].ItemArray[1].ToString();                
                txtloanno.Text = ds.Tables["check"].Rows[0].ItemArray[2].ToString();
                txtloanpurpose.Text = ds.Tables["check"].Rows[0].ItemArray[3].ToString();
                txtamount.Text = ds.Tables["check"].Rows[0].ItemArray[5].ToString();
                //txtprincple.Text = ds.Tables["check"].Rows[0].ItemArray[5].ToString();
                txtinterestrate.Text = ds.Tables["check"].Rows[0].ItemArray[6].ToString();
                txtnoofinst.Text = ds.Tables["check"].Rows[0].ItemArray[7].ToString();
                txtinststartdate.Text = ds.Tables["check"].Rows[0].ItemArray[8].ToString();
                txtinstenddate.Text = ds.Tables["check"].Rows[0].ItemArray[9].ToString();
                //CascadingDropDown2.SelectedValue = ds.Tables["check"].Rows[0].ItemArray[10].ToString();
                txtchequeno.Text = ds.Tables["check"].Rows[0].ItemArray[11].ToString();
                ddlloantype.SelectedValue = ds.Tables["check"].Rows[0].ItemArray[12].ToString();
                txtbankname.Text = ds.Tables["check"].Rows[0].ItemArray[10].ToString();
                txtdate.Text = ds.Tables["check"].Rows[0].ItemArray[13].ToString();
                txtapplydate.Text = ds.Tables["check"].Rows[0].ItemArray[15].ToString();
                txtdisposalamt.Text = ds.Tables["check"].Rows[0].ItemArray[16].ToString();
                txtprocessingcrg.Text = ds.Tables["check"].Rows[0].ItemArray[17].ToString();
                txtpayment.Text = ds.Tables["check"].Rows[0].ItemArray[14].ToString();
                if (ds.Tables["check"].Rows[0].ItemArray[12].ToString() == "For Capital")
                {
                    Pay.Visible = true;
                    ModeofPay.Visible = true;
                }
                else
                {
                    Pay.Visible = false;
                    ModeofPay.Visible = false;
                }
                agencyname(ds.Tables["check"].Rows[0].ItemArray[1].ToString());
            }
            else
            {
                tblloandetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void agencyname(string s)
    {
        da = new SqlDataAdapter("select agencyname from agency where agencycode='" + s + "'", con);
        da.Fill(ds, "kishore");
        if (ds.Tables["kishore"].Rows.Count > 0)
        {
            lblagencyname.Text = ds.Tables["kishore"].Rows[0].ItemArray[0].ToString();
        }
    }

 
    protected void btnrejectagency_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_RejectTermloan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = ViewState["Id"].ToString();
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));          
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.AlertAndRedirect(msg, "VerifyTermloanApproval.aspx");
            }
            else
            {
                JavaScript.Alert(msg);
            }
            FillGrid();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnverifyTL_Click(object sender, EventArgs e)
    {
        if (txtdisposalamt.Text == "")
            txtdisposalamt.Text = "0";
        if (txtprocessingcrg.Text == "")
            txtprocessingcrg.Text = "0";
        cmd = new SqlCommand();
        cmd.CommandText = "sp_termloan_Credit";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Loanno", txtloanno.Text));
        cmd.Parameters.Add(new SqlParameter("@agencycode", txtagencycode.Text));
        cmd.Parameters.Add(new SqlParameter("@bankname", txtbankname.Text));
        cmd.Parameters.Add(new SqlParameter("@modeofpay", txtpayment.Text));
        cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));
        cmd.Parameters.Add(new SqlParameter("@chequeno", txtchequeno.Text));
        cmd.Parameters.Add(new SqlParameter("@Loandescription", txtloanpurpose.Text));
        cmd.Parameters.Add(new SqlParameter("@Amount", txtdisposalamt.Text));
        cmd.Parameters.Add(new SqlParameter("@interestrate", txtinterestrate.Text));
        cmd.Parameters.Add(new SqlParameter("@noofinst", txtnoofinst.Text));
        cmd.Parameters.Add(new SqlParameter("@inststartdate", txtinststartdate.Text));
        cmd.Parameters.Add(new SqlParameter("@instenddate", txtinstenddate.Text));
        cmd.Parameters.Add(new SqlParameter("@Agencyname", lblagencyname.Text));
        cmd.Parameters.Add(new SqlParameter("@Loantype", ddlloantype.SelectedItem.Text));
        cmd.Parameters.Add(new SqlParameter("@User", Session["user"].ToString()));
        cmd.Parameters.Add(new SqlParameter("@Roles", Session["roles"].ToString()));
        cmd.Parameters.Add(new SqlParameter("@applydate", txtapplydate.Text));
        cmd.Parameters.Add(new SqlParameter("@Balanceamount", (Convert.ToDecimal(txtdisposalamt.Text) + Convert.ToDecimal(txtprocessingcrg.Text)).ToString()));
        cmd.Parameters.Add(new SqlParameter("@LId", ViewState["Id"].ToString()));
      
        cmd.Connection = con;
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        //string msg = "";
        if (msg == "Sucessfull")
        {
            JavaScript.AlertAndRedirect(msg, "VerifyTermloanApproval.aspx");
        }
        else
        {
            JavaScript.Alert("Verify Failed");
        }
    }
}