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


public partial class Termloanpayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da= new SqlDataAdapter();
    DataSet ds = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            printformat.Visible = false;
            print.Visible = false;
            Principle.Visible = false;
            Interest.Visible = false;
            Installmentno.Visible = false;
            vednor.Visible = false;
            grid.Visible = false;
            Paymentdetail.Visible = false;
            agency.Visible = false;
            loan.Visible = false;
            trbutton.Visible = false;
            lblvendorname.Visible = false;
        }
       
    }

    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectiontype();
        clear();
        fillloanno(rbtntype.SelectedValue);
      
    }
    private void selectiontype()
    {
        agency.Visible = true;
        loan.Visible = true;
        trbutton.Visible = true;
        if (rbtntype.SelectedIndex == 0)
        {
            //Principle.Visible = false;
            //Interest.Visible = false;
            //Installmentno.Visible = false;
            //vednor.Visible = false;
            //grid.Visible = false;
            //ddlfrom.Visible = false;
            //txtbankname.Visible = true;
            //Paymentdetail.Visible = true;
            Principle.Visible = true;
            Interest.Visible = true;
            Installmentno.Visible = true;
            vednor.Visible = false;
            ddlfrom.Visible = true;
            txtbankname.Visible = false;
            grid.Visible = false;
            Paymentdetail.Visible = true;
            trbankname.Visible = true;
            tdlblmode.Visible = true;
            tdtxtmode.Visible = true;
            txtcheque.Visible = false;
            ddlcheque.Visible = false;
          
        }

        else if (rbtntype.SelectedIndex == 1)
        {
            Principle.Visible = false;
            Interest.Visible = false;
            Installmentno.Visible = false;
            vednor.Visible = true;
            grid.Visible = true;
            ddlfrom.Visible = true;
            txtbankname.Visible = false;
            Paymentdetail.Visible = true;
            laodvendor();
            trbankname.Visible = false;
            tdlblmode.Visible = false;
            tdtxtmode.Visible = false;
            lblvendorname.Visible = true;
         
         
            
        }
        //else if (rbtntype.SelectedIndex == 2)
        //{
            

        //}
    }
  
    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        ddlpo.Visible = true;
        Button1.Visible = true;
        if (ddlvendor.SelectedIndex != 0)
        {
            loadpo(ddlvendor.SelectedValue);
            da = new SqlDataAdapter("Select vendor_name from vendor where vendor_id='" + ddlvendor.SelectedValue + "'", con);
            da.Fill(ds, "name");
            //txtname.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
            lblvendorname.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();

        }


    }
    private void laodvendor()
    {
        ddlvendor.Visible = true;
        da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='Supplier' and status='2'", con);
        da.Fill(ds, "vendor");
        ddlvendor.DataTextField = "Name";
        ddlvendor.DataValueField = "vendor_id";
        ddlvendor.DataSource = ds.Tables["vendor"];
        ddlvendor.DataBind();
        ddlvendor.Items.Insert(0, "Select Vendor");
    }
  
    private void fillgrid()
    {
        da = new SqlDataAdapter("select i.invoiceno,CC_Code,DCA_CODE,vendor_id,total,isnull(NetAmount,0)as[NetAmount],isnull(TDS,0)as[TDS],isnull(Retention,0)as[Retention],isnull(Balance,0)as[Balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date from pending_invoice i where i.po_no='" + ddlpo.SelectedValue + "' and balance!=0 and (status='3' or status='Debit Pending') and vendor_id='" + ddlvendor.SelectedValue + "'", con);
        da.Fill(ds, "fill");
        grd.DataSource = ds.Tables["fill"];
        grd.DataBind();

    }
    private void fillloanno(string s)
    {
        //if (s == "Credit")
        //    da = new SqlDataAdapter("Select loanno from termloan where Loantype='For Capital' and balance!='0'", con);
        //else if (s == "Debit" || s == "Third Party")
        if (s == "Debit")
            da = new SqlDataAdapter("Select loanno from termloan where  balance!='0' and status !='Rejected'", con);
        else if (s == "Third Party")
            da = new SqlDataAdapter("Select loanno from termloan where status='3' and balance!='0' and loantype='For Purchase Of Assets'", con);
        da.Fill(ds, "loan");
        ddlcrloanno.DataTextField = "loanno";
        ddlcrloanno.DataValueField = "loanno";
        ddlcrloanno.DataSource = ds.Tables["loan"];
        ddlcrloanno.DataBind();
        ddlcrloanno.Items.Insert(0, "Select Loan No");

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
      

    }
     private void loadpo(string q)
    {
        da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and cc_code='CCC' and dca_code='DCA-27' and vendor_id='" + q + "'", con);
        da.Fill(ds, "po");
        ddlpo.DataTextField = "po_no";
        ddlpo.DataValueField = "po_no";
        ddlpo.DataSource = ds.Tables["po"];
        ddlpo.DataBind();
        ddlpo.Items.Insert(0, "Select PO");
    }


     protected void ddlcrloanno_SelectedIndexChanged(object sender, EventArgs e)
     {
         try 
         {
             da = new SqlDataAdapter("Select t.agencycode,loanpurpose,amount,Interestrate,BalanceInst,REPLACE(CONVERT(VARCHAR(11),Inststartdate, 106), ' ', '-')as [Inststartdate],balance,No_of_Installments,a.agencyname,REPLACE(CONVERT(VARCHAR(11),t.Createddate, 106), ' ', '-') as Createddate from termloan t join Agency a on t.Agencycode=a.Agencycode where loanno='" + ddlcrloanno.SelectedItem.Text + "'", con);
             da.Fill(ds, "loandetails");
             if (ds.Tables["loandetails"].Rows.Count > 0)
             {

                 txtcragencycode.Text = ds.Tables["loandetails"].Rows[0][0].ToString();
                 hfdate.Value = ds.Tables["loandetails"].Rows[0][9].ToString();
                 hfinstdate.Value = ds.Tables["loandetails"].Rows[0][5].ToString();

                 if (rbtntype.SelectedIndex == 0)
                 {
                     //txtcrloanpurpose.Text = ds.Tables["loandetails"].Rows[0][1].ToString();
                     //txtcramount.Text = ds.Tables["loandetails"].Rows[0][2].ToString().Replace(".0000", ".00");
                     //txtcheque.Text = ds.Tables["loandetails"].Rows[0][6].ToString();
                     //txtbankname.Text = ds.Tables["loandetails"].Rows[0][7].ToString();
                     ////CascadingDropDown9.SelectedValue = ds.Tables["loandetails"].Rows[0][7].ToString();
                     //txtcrloanpurpose.Enabled = false;
                     //txtcramount.Enabled = false;
                     //txtcheque.Enabled = false;
                     //CascadingDropDown9.Enabled = false;
                     hf1.Value = ds.Tables["loandetails"].Rows[0][3].ToString();
                     lblbalance.Text = ds.Tables["loandetails"].Rows[0][6].ToString().Replace(".0000", ".00");
                     Installments();
                     lblcragencyname.Text = ds.Tables["loandetails"].Rows[0][8].ToString();
                     
                    
                 }
                 else if (rbtntype.SelectedIndex == 1)
                 { 
                     txtcrloanpurpose.Text = ds.Tables["loandetails"].Rows[0][1].ToString();
                     txtcramount.Text = ds.Tables["loandetails"].Rows[0][2].ToString().Replace(".0000", ".00");
                     //txtcheque.Text = ds.Tables["loandetails"].Rows[0][6].ToString();
                     CascadingDropDown9.SelectedValue = ds.Tables["loandetails"].Rows[0][7].ToString();
                     txtcrloanpurpose.Enabled = false;
                     txtcramount.Enabled = false;
                   
                     lblcragencyname.Text = ds.Tables["loandetails"].Rows[0][8].ToString();
                 
                 }
             }
         }
         catch (Exception ex)
         {
             Utilities.CatchException(ex);
             JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
         }
       
     }

     public void Installments()
     {
         da = new SqlDataAdapter("Select isnull(max(InstNo),0)+1 as inst from bankbook where loanno='" + ddlcrloanno.SelectedItem.Text + "'", con);
         da.Fill(ds, "instno");
        
             if (ds.Tables["instno"].Rows.Count >= 0)
             {

                 ddldnoofinst.DataValueField = "inst";
                 ddldnoofinst.DataTextField = "inst";
                 ddldnoofinst.DataSource = ds.Tables["instno"];
                 ddldnoofinst.DataBind();
                 ddldnoofinst.Items.Insert(0, "Select Installment No");
                 //ddldnoofinst.Items.Add(new ListItem(i.ToString()));
             }
         
     }

  
     private decimal Balance = (decimal)0.0;
     private decimal Total = (decimal)0.0;
     private decimal NetAmount = (decimal)0.0;
     private decimal TDS = (decimal)0.0;
     private decimal Retention = (decimal)0.0;


     protected void grd_RowDataBound1(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {

             Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance"));
             Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
             TDS += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS"));
             Retention += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Retention"));
             NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
         }
         else if (e.Row.RowType == DataControlRowType.Footer)
         {
             e.Row.Cells[4].Text = String.Format(" {0:#,##,##,###.00}", Total);
             e.Row.Cells[5].Text = String.Format(" {0:#,##,##,###.00}", NetAmount);
             e.Row.Cells[6].Text = String.Format(" {0:#,##,##,###.00}", TDS);
             e.Row.Cells[7].Text = String.Format(" {0:#,##,##,###.00}", Retention);
             e.Row.Cells[8].Text = String.Format(" {0:#,##,##,###.00}", Balance);

         }

     }
     protected void btnsubmit_Click(object sender, EventArgs e)
     {

         try
         {
             print.Visible = false;
             cmd = new SqlCommand();
             cmd.CommandText = "sp_termloan";
             cmd.CommandType = CommandType.StoredProcedure;

             cmd.Parameters.Add(new SqlParameter("@type", rbtntype.SelectedValue.ToString()));
             cmd.Parameters.Add(new SqlParameter("@Loanno", ddlcrloanno.SelectedItem.Text));
             cmd.Parameters.Add(new SqlParameter("@agencycode", txtcragencycode.Text));
             cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));
             cmd.Parameters.Add(new SqlParameter("@Loandescription", txtcrloanpurpose.Text));
             cmd.Parameters.Add(new SqlParameter("@Amount", txtcramount.Text));
             cmd.Parameters.Add(new SqlParameter("@User", Session["user"].ToString()));

             if (rbtntype.SelectedIndex == 0)
             {
                 BankDateCheck objdate = new BankDateCheck();
                 if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedItem.Text))
                 {
                     cmd.Parameters.Add(new SqlParameter("@Noinst", ddldnoofinst.SelectedItem.Text));
                     cmd.Parameters.Add(new SqlParameter("@principalamount", txtdprincple.Text));
                     cmd.Parameters.Add(new SqlParameter("@interestamount", txtdinterestamount.Text));
                     if (ddlpayment.SelectedItem.Text == "Cheque")
                     {
                         cmd.Parameters.Add(new SqlParameter("@chequeno", ddlcheque.SelectedItem.Text));
                         cmd.Parameters.Add(new SqlParameter("@chequeid", ddlcheque.SelectedValue));
                     }
                     else
                         cmd.Parameters.Add(new SqlParameter("@chequeno", txtcheque.Text));

                     cmd.Parameters.Add(new SqlParameter("@modeofpay", ddlpayment.SelectedItem.Text));
                     cmd.Parameters.Add(new SqlParameter("@bankname", ddlfrom.SelectedItem.Text));
                     cmd.Parameters.Add(new SqlParameter("@instdesc", txtinstdesc.Text));
                     print.Visible = true;
                     fillform();

                 }
                 else
                 {
                     JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
                 }
             }
             else if (rbtntype.SelectedIndex == 1)
             {
                 string invoicenos = "";
                 foreach (GridViewRow rec in grd.Rows)
                 {
                     if ((rec.FindControl("ChkSelect") as CheckBox).Checked)
                         invoicenos = invoicenos + grd.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                 }
                 cmd.Parameters.AddWithValue("@invoicenos", invoicenos);
                 cmd.Parameters.Add(new SqlParameter("@venname", lblvendorname.Text));
                 print.Visible = false;

             }

             cmd.Connection = con;
             con.Open();
             string msg = cmd.ExecuteScalar().ToString();
             if (rbtntype.SelectedIndex == 1)
             {
                 JavaScript.UPAlertRedirect(Page, msg, "Termloanpayment.aspx");


             }
             else
             {
                 if (msg == "Invalid date")
                 {
                     JavaScript.UPAlert(Page, msg);
                     txtdate.Text = "";
                 }
                 else if (msg == "You are not Assign the DCA Budget" || msg == "Insufficient DCA Budget" || msg == "This DCA is Under Amendment")
                 {
                     JavaScript.UPAlert(Page, msg);
                 }
                 else
                 {

                     showalert("Sucessfull");
                     lbltranid.Text = msg.ToString();
                     printformat.Visible = true;
                 }
             }
             con.Close();
         }
         catch (Exception ex)
         {
             Utilities.CatchException(ex);
         }

     }

     public void showalert(string message)
     {
        
         //Label myalertlabel = new Label();
         string script = @"alert('" + message + "');print();window.location='Termloanpayment.aspx';";
         ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert", script, true);
         //myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "');window.location='TermloanPayment.aspx';print()</script>";
         //Page.Controls.Add(myalertlabel);
     }
     public void clear()
     {
       
         txtcragencycode.Text = "";
         txtcramount.Text = "";
         txtcrloanpurpose.Text = "";
         txtdate.Text = "";
         txtdinterestamount.Text = "";
         txtdprincple.Text = "";
       
         CascadingDropDown1.SelectedValue = "";
         CascadingDropDown9.SelectedValue = "";
         grd.DataSource = null;
         grd.DataBind();
        
     }

     public void fillform()
     {
         da = new SqlDataAdapter("select agencyname from Agency a join TermLoan t on a.Agencycode=t.Agencycode where Loanno='"+ddlcrloanno.SelectedItem.Text+"'", con);
         da.Fill(ds, "name");
         txtagencycode.Text =txtcragencycode.Text;
         txttermloanno.Text = ddlcrloanno.SelectedItem.Text;
         txtinstallmentno.Text = ddldnoofinst.SelectedItem.Text;
         txtpartyname.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
         txtoutstandingprinciple.Text =lblbalance.Text;
       
         txtprincipleamountininst.Text = txtdprincple.Text;
         txtinstalldate.Text = txtdate.Text;
         txtintamountinst.Text = txtdinterestamount.Text;
         txtprincipleamountdebitcc.Text = "CCC";
         txtprincipleamountdebitdca.Text = "DCA-41";
         txtinterestamountdebitcc.Text = "CC-12";
         txtinterestamountdebitdca.Text = "DCA-31";
         txtinterestamountdebitsdca.Text = "DCA-31 .3";
         txtbalanceaftertheinst.Text = (Convert.ToDecimal(lblbalance.Text) - Convert.ToDecimal(txtdprincple.Text)).ToString();
         txtdebitamountnowinst.Text = txtcramount.Text;
         //lblsubmittetby.Text =;

     }

     protected void ddlfrom_SelectedIndexChanged(object sender, EventArgs e)
     {
         da = new SqlDataAdapter("select cn.chequeno,cn.id from cheque_nos cn join cheque_Master cm on cn.chequeid=cm.chequeid where cm.bankname='" + ddlfrom.SelectedItem.Text + "' and cn.status='2'", con);
         da.Fill(ds, "chequeno");
         if (ds.Tables["chequeno"].Rows.Count > 0)
         {
             ddlcheque.DataSource = ds.Tables["chequeno"];
             ddlcheque.DataTextField = "chequeno";
             ddlcheque.DataValueField = "id";
             ddlcheque.DataBind();
             ddlcheque.Items.Insert(0, "Select");

         }
     }

     protected void ddlpayment_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddlpayment.SelectedItem.Text == "Cheque")
         {
             ddlcheque.Visible = true;
             txtcheque.Visible = false;
         }
         else
         {
             ddlcheque.Visible = false;
             txtcheque.Visible = true;
         }
     }
}
