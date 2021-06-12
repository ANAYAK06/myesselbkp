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

public partial class verifyloandetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd= new SqlCommand();
    DataSet ds = new DataSet();     
    string id;


    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {
          
            if ((Request.QueryString["id"] == null) && (Request.QueryString["session"] == null))
            {
                JavaScript.CloseWindow();
            }
            else
            {
                filldata();
            }
        }
        
    }

    public void filldata()
    {
        if (Request.QueryString["session"] == "HoAdmin")
        {
            da = new SqlDataAdapter("select t.id,t.Agencycode,t.Loanno,t.LoanPurpose,t.Amount,t.balance,t.Interestrate,t.No_of_Installments,REPLACE(CONVERT(VARCHAR(11),t.inststartdate, 106), ' ', '-')as inststartdate,REPLACE(CONVERT(VARCHAR(11),t.Instenddate, 106), ' ', '-')as Instenddate,b.Bank_name,b.No,t.loantype,REPLACE(CONVERT(VARCHAR(11),b.date, 106), ' ', '-')as date,b.ModeOfPay,REPLACE(CONVERT(VARCHAR(11),t.Createddate, 106), ' ', '-')as Createddate from TermLoan t left join bankbook b on b.loanno=t.loanno  where t.id='" + Request.QueryString["id"].ToString() + "' and t.status='1'", con);
        }
        else
        {
            da = new SqlDataAdapter("select t.id,t.Agencycode,t.Loanno,t.LoanPurpose,t.Amount,t.balance,t.Interestrate,t.No_of_Installments,REPLACE(CONVERT(VARCHAR(11),t.inststartdate, 106), ' ', '-')as inststartdate,REPLACE(CONVERT(VARCHAR(11),t.Instenddate, 106), ' ', '-')as Instenddate,b.Bank_name,b.No,t.loantype,REPLACE(CONVERT(VARCHAR(11),b.date, 106), ' ', '-')as date,b.ModeOfPay,REPLACE(CONVERT(VARCHAR(11),t.Createddate, 106), ' ', '-')as Createddate from TermLoan t left join bankbook b on b.loanno=t.loanno  where t.id='" + Request.QueryString["id"].ToString() + "' and t.status='2'", con);
        }
        da.Fill(ds, "check");
        if (ds.Tables["check"].Rows.Count > 0)
        {
            CascadingDropDown4.SelectedValue = ds.Tables["check"].Rows[0].ItemArray[1].ToString();
            txtloanno.Text = ds.Tables["check"].Rows[0].ItemArray[2].ToString();
            txtloanpurpose.Text = ds.Tables["check"].Rows[0].ItemArray[3].ToString();
            txtamount.Text = ds.Tables["check"].Rows[0].ItemArray[4].ToString();
            //txtprincple.Text = ds.Tables["check"].Rows[0].ItemArray[5].ToString();
            txtinterestrate.Text = ds.Tables["check"].Rows[0].ItemArray[6].ToString();
            txtnoofinst.Text = ds.Tables["check"].Rows[0].ItemArray[7].ToString();
            txtinststartdate.Text = ds.Tables["check"].Rows[0].ItemArray[8].ToString();
            txtinstenddate.Text = ds.Tables["check"].Rows[0].ItemArray[9].ToString();
            CascadingDropDown2.SelectedValue = ds.Tables["check"].Rows[0].ItemArray[10].ToString();
            txtcheque.Text = ds.Tables["check"].Rows[0].ItemArray[11].ToString();
            ddlloantype.SelectedValue = ds.Tables["check"].Rows[0].ItemArray[12].ToString();
            CascadingDropDown1.SelectedValue = ds.Tables["check"].Rows[0].ItemArray[14].ToString();
            txtdate.Text = ds.Tables["check"].Rows[0].ItemArray[13].ToString();
            txtapplydate.Text = ds.Tables["check"].Rows[0].ItemArray[15].ToString();
            if (ds.Tables["check"].Rows[0].ItemArray[12].ToString() == "For Capital")
            {
                pay.Visible = true;
                ModeofPay.Visible = true;
            }
            else
            {
                pay.Visible = false;
                ModeofPay.Visible = false;
            }
            agencyname(ds.Tables["check"].Rows[0].ItemArray[1].ToString());
        }
        else
        {
            JavaScript.POPUPClosing("Already Approved");
        }


    }
    protected void btnAddagency_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "sp_termloan_Credit";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Loanno", txtloanno.Text));
        cmd.Parameters.Add(new SqlParameter("@agencycode", ddlagencycode.SelectedItem.Text));
        cmd.Parameters.Add(new SqlParameter("@bankname", ddlbankname.SelectedItem.Text));
        cmd.Parameters.Add(new SqlParameter("@modeofpay", ddlpayment.SelectedValue));
        cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));
        cmd.Parameters.Add(new SqlParameter("@chequeno", txtcheque.Text));
        cmd.Parameters.Add(new SqlParameter("@Loandescription", txtloanpurpose.Text));
        cmd.Parameters.Add(new SqlParameter("@Amount", txtamount.Text));
        cmd.Parameters.Add(new SqlParameter("@interestrate", txtinterestrate.Text));
        cmd.Parameters.Add(new SqlParameter("@noofinst", txtnoofinst.Text));
        cmd.Parameters.Add(new SqlParameter("@inststartdate", txtinststartdate.Text));
        cmd.Parameters.Add(new SqlParameter("@instenddate", txtinstenddate.Text));
        cmd.Parameters.Add(new SqlParameter("@Agencyname", txtagencyname.Text));
        cmd.Parameters.Add(new SqlParameter("@Loantype", ddlloantype.SelectedItem.Text));
        cmd.Parameters.Add(new SqlParameter("@User", Session["user"].ToString()));
        cmd.Parameters.Add(new SqlParameter("@Roles", Session["roles"].ToString()));
        cmd.Parameters.Add(new SqlParameter("@applydate", txtapplydate.Text));
        cmd.Connection = con;
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        if (msg == "Sucessfull")
        {
            JavaScript.POPUPClosing("Successfully Verified");

        }
        else
        {
            JavaScript.Alert("Verify Failed");
        }

    }
    protected void btnresetagency_Click(object sender, EventArgs e)
    {
        JavaScript.CloseWindow();
    }
    protected void ddlagencycode_SelectedIndexChanged(object sender, EventArgs e)
    {
        agencyname(ddlagencycode.SelectedItem.Text);
       
    }
    protected void ddlloantype_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        if (ddlloantype.SelectedItem.Text == "For Capital")
        {
            pay.Visible = true;
            ModeofPay.Visible = true;
        }
        else
        {
            //name.Visible = false;
            pay.Visible = false;
            ModeofPay.Visible =  false;

        }
      
    }
    public void agencyname(string s)
    {
        da = new SqlDataAdapter("select agencyname from agency where agencycode='" + s + "'", con);
        da.Fill(ds, "kishore");
        if (ds.Tables["kishore"].Rows.Count > 0)
        {
            txtagencyname.Text = ds.Tables["kishore"].Rows[0].ItemArray[0].ToString();
        }
    }
}
