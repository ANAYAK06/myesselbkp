using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Inbox2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;

    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Default.aspx");
        if (Session["roles"].ToString() == "Sr.Accountant")
            Response.Redirect("SrAccountantInbox.aspx");
        else if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
        {
            if (!IsPostBack)
                FillGrid();

        }
    }
    public void FillGrid()
    {

        try
        {
            this.UpdatePanel1.Update();
            if (Session["roles"].ToString() == "HoAdmin")
             // da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='1' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='1')i where i.id>0 order by i.date", con);
                da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='1'  union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='1')i where i.id>0  order by i.date", con);

            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='2'  union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='2')i where i.id>0 order by i.date", con);
               //da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='2' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='2')i where i.id>0 order by i.date", con);
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director") //Added ENH-JUN-002-2016
                da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='2A'  union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='2A')i where i.id>0  order by i.date", con);
                //da = new SqlDataAdapter("Select i.* from (select Transaction_No as id,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as Date,Amount,'A' as type  from BankTransactions where status='2' union all select ID,CC_Code,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,Debit as Amount,'B' as type from BankBook where PaymentType='Supplier' and Loanno is not null and status='2A')i where i.id>0 order by i.date", con);
            da.Fill(ds, "FillIn");
            GridView1.DataSource = ds.Tables["FillIn"];


            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }



    }
    public void Bankvouchers(string bid)
    {
        try
        {
            da = new SqlDataAdapter("Select paymenttype from bankbook where Transaction_No=@id", con);
            da.SelectCommand.Parameters.AddWithValue("@id", bid);
            da.Fill(ds, "type");

            if (ds.Tables["type"].Rows.Count > 0)
            {
                txttype.Text = ds.Tables["type"].Rows[0][0].ToString();
            }
            if (txttype.Text != "General" && txttype.Text != "Salary" && txttype.Text != "PF" && txttype.Text != "Unsecured Loan" && txttype.Text != "Withdraw" && txttype.Text != "Term Loan" && txttype.Text != "Transfer")
            {
                da = new SqlDataAdapter("Select b.name,REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as date,mode_of_pay,b.description,b.bank_name,amount,b.no from BankTransactions bt join bankbook b on bt.Transaction_No=b.Transaction_No where bt.Transaction_No=@id", con);
                da.SelectCommand.Parameters.AddWithValue("@id", bid);
                da.Fill(ds, "General");

                if (ds.Tables["General"].Rows.Count > 0)
                {
                    //cascbank.Enabled = false;
                    if (txttype.Text == "Service Provider" || txttype.Text == "Supplier" )
                    {
                        txtpaymentdate.Enabled = false;
                    }
                    else
                    {
                        txtpaymentdate.Enabled = true;
                    }
                    txttransaction.Text = bid.ToString();
                    if(txttype.Text!="TDS")
                    txtname.Text = ds.Tables["General"].Rows[0][0].ToString();
                    txtpaymentdate.Text = ds.Tables["General"].Rows[0][1].ToString();
                    txtremarks.Text = ds.Tables["General"].Rows[0][3].ToString();
                    ddlmodeofpay.Text = ds.Tables["General"].Rows[0][2].ToString();
                    txtcascbank.Text = ds.Tables["General"].Rows[0][4].ToString();
                    txtdebitamount.Text = ds.Tables["General"].Rows[0][5].ToString().Replace(".0000", ".00");
                    txtno.Text = ds.Tables["General"].Rows[0][6].ToString();
                    ViewState["bank"] = ds.Tables["General"].Rows[0][4].ToString();
                    txtcascbank.Visible = true;
                    lblbank.Visible = true;
                    Chkbank.Visible = true;
                    General.Visible = false;
                    generalPay.Visible = false;
                    HyperLink2.Visible = true;
                    trbank.Visible = false;
                    //txtpaymentdate.Enabled = false;
                }
            }
            else
            {
                if (txttype.Text == "General" || txttype.Text == "Unsecured Loan" || txttype.Text == "Withdraw" || txttype.Text == "Term Loan")
                    da = new SqlDataAdapter("Select paymenttype,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,modeofpay,description,bank_name,debit,cc_code,dca_code,sub_dca,no,REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-')as Paiddate,principle,interest from  Bankbook where Transaction_No=@id", con);
                else if (txttype.Text == "Salary" || txttype.Text == "PF")
                    da = new SqlDataAdapter("Select paymenttype,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as modifieddate,modeofpay,description,bank_name,debit,cc_code,dca_code,sub_dca,no,REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-')as Paiddate from  Bankbook where Transaction_No=@id", con);
                else if (txttype.Text == "Transfer")
                    da = new SqlDataAdapter("SELECT b.paymenttype,bt.party_name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,b.modeofpay,b.description,b.bank_name,b.debit,b.no,REPLACE(CONVERT(VARCHAR(11),modifieddate, 106), ' ', '-')as Paiddate FROM bankbook b JOIN banktransactions bt ON b.transaction_no=bt.transaction_no WHERE b.Transaction_No=@id", con);
                da.SelectCommand.Parameters.AddWithValue("@id", bid);
                da.Fill(ds, "General1");
                if (ds.Tables["General1"].Rows.Count > 0)
                {
                    txttransaction.Text = bid.ToString();
                    //cascbank.Enabled = false;
                    txttype.Text = ds.Tables["General1"].Rows[0][0].ToString();

                    txtname.Text = ds.Tables["General1"].Rows[0][1].ToString();
                    ddlmodeofpay.Text = ds.Tables["General1"].Rows[0][3].ToString();
                    txtremarks.Text = ds.Tables["General1"].Rows[0][4].ToString();

                    txtdebitamount.Text = ds.Tables["General1"].Rows[0][6].ToString();

                    if (txttype.Text == "Transfer")
                    {
                        trbank.Visible = true;
                        txtno.Text = ds.Tables["General1"].Rows[0][7].ToString();
                        Txtfrom.Text = ds.Tables["General1"].Rows[0][5].ToString();
                        TxtTo.Text = ds.Tables["General1"].Rows[1][5].ToString();
                        lblbank.Visible = false;
                        txtcascbank.Visible = false;
                        Chkbank.Visible = false;
                        ViewState["bank2"] = ds.Tables["General1"].Rows[1][5].ToString();
                    }

                    else
                    {
                        txtcascbank.Visible = true;
                        lblbank.Visible = true;
                        Chkbank.Visible = true;
                        trbank.Visible = false;
                        txtno.Text = ds.Tables["General1"].Rows[0][10].ToString();
                        ddlbankcc.Text = ds.Tables["General1"].Rows[0][7].ToString().Replace(".0000", ".00");
                        ddlbankdca.Text = ds.Tables["General1"].Rows[0][8].ToString();
                        ddlbanksub.Text = ds.Tables["General1"].Rows[0][9].ToString();
                        txtcascbank.Text = ds.Tables["General1"].Rows[0][5].ToString();
                    }

                    if (txttype.Text != "Withdraw" && txttype.Text != "Term Loan" && txttype.Text != "Transfer")
                    {
                        //txtpaymentdate.Enabled = false;
                        txtpaymentdate.Text = ds.Tables["General1"].Rows[0][11].ToString();
                        txtVindt.Text = ds.Tables["General1"].Rows[0][2].ToString();
                        txtpaymentdate.Text = ds.Tables["General1"].Rows[0][11].ToString();
                        generalPay.Visible = true;
                    }
                    else if (txttype.Text == "Withdraw" || txttype.Text == "Transfer")
                    {
                        txtpaymentdate.Text = ds.Tables["General1"].Rows[0][2].ToString();
                        generalPay.Visible = false;

                    }
                    else if (txttype.Text == "Term Loan")
                    {
                        txtpaymentdate.Text = ds.Tables["General1"].Rows[0][2].ToString();
                        txtVindt.Text = ds.Tables["General1"].Rows[0][2].ToString();
                        txtVPO.Text = ds.Tables["General1"].Rows[0][12].ToString();
                        txtVInno.Text = ds.Tables["General1"].Rows[0][13].ToString();
                        Label8.Text = "Principle";
                        Label4.Text = "Interest";
                        generalPay.Visible = true;
                        //txtpaymentdate.Enabled = true;


                    }
                    General.Visible = true;

                    HyperLink2.Visible = false;
                    ViewState["bank1"] = ds.Tables["General1"].Rows[0][5].ToString();


                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        //finally {
        //    ds.Tables["General1"].Dispose();
        //    ds.Tables["General"].Dispose();
        //}

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string type = "";
            da = new SqlDataAdapter("Select paymenttype from BankBook where Transaction_No='" + GridView1.DataKeys[e.RowIndex]["id"].ToString() + "'", con);
            da.Fill(ds, "CheckType");
            type = ds.Tables["CheckType"].Rows[0].ItemArray[0].ToString();

            if (type != "Service Provider" && type != "Supplier")
            {
                HiddenField hf = (HiddenField)GridView1.Rows[e.RowIndex].Cells[8].FindControl("hf1");
                if (hf.Value == "A" || hf.Value == "B")
                {
                    cmd.Connection = con;
                    cmd = new SqlCommand("sp_MakingOfVendorPayment_Reject", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["id"].ToString());
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    if (hf.Value == "A")
                        cmd.Parameters.AddWithValue("@Type", "A");
                    else if (hf.Value == "B")
                        cmd.Parameters.AddWithValue("@Type", "B");
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "Rejected";
                    if (msg == "Rejected")
                        JavaScript.UPAlertRedirect(Page, msg, "Inbox2.aspx");
                    else
                        JavaScript.UPAlertRedirect(Page, msg, "Inbox2.aspx");
                    con.Close();
                    FillGrid();
                }
            }
            else if (type == "Service Provider" || type == "Supplier")
            {
                HiddenField hf = (HiddenField)GridView1.Rows[e.RowIndex].Cells[8].FindControl("hf1");
                if (hf.Value == "A")
                {
                    cmd.Connection = con;
                    cmd = new SqlCommand("sp_ServiceProviderpayment_Rejectsp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["id"].ToString());
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "Rejected";
                    if (msg == "Rejected")
                        JavaScript.UPAlertRedirect(Page, msg, "Inbox2.aspx");
                    else
                        JavaScript.UPAlertRedirect(Page, msg, "Inbox2.aspx");
                    con.Close();
                    FillGrid();
                }

            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure " +
                               "you want to delete this record?')) return true;";


                }
            }
        }

    }
    protected void btnFirst_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnPrev_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnNext_Command(object sender, CommandEventArgs e)
    {

        Paginate(sender, e);
    }
    protected void btnLast_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void Paginate(object sender, CommandEventArgs e)
    {
        // Get the Current Page Selected
        int iCurrentIndex = GridView1.PageIndex;

        switch (e.CommandArgument.ToString().ToLower())
        {
            case "first":
                GridView1.PageIndex = 0;
                break;
            case "prev":
                if (GridView1.PageIndex != 0)
                {
                    GridView1.PageIndex = iCurrentIndex - 1;
                }
                break;
            case "next":
                GridView1.PageIndex = iCurrentIndex + 1;
                break;
            case "last":
                GridView1.PageIndex = GridView1.PageCount;
                break;
        }

        //Populate the GridView Control
        FillGrid();
    }
    protected void btnbankApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (HiddenField.Value == "A")
            {

                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                if (txttype.Text != "Transfer" && txttype.Text != "TDS") //General
                {
                    if (txttype.Text == "General" || txttype.Text == "Unsecured Loan" || txttype.Text == "Withdraw" || txttype.Text == "Term Loan" || txttype.Text == "Salary" || txttype.Text == "PF")
                    {
                        cmd.CommandText = "sp_Bank_Debit";
                        cmd.Parameters.AddWithValue("@Bank_Name", ViewState["bank1"].ToString());//Indian Overseas Bank
                    }
                    else
                    {
                        cmd.CommandText = "sp_InvoiceDebit";
                        cmd.Parameters.AddWithValue("@Bank_Name", ViewState["bank"].ToString()); //Indian Overseas Bank
                    }
                    cmd.Parameters.AddWithValue("@InvoiceNo", txtVInno.Text);//null
                    cmd.Parameters.AddWithValue("@PO_NO", txtVPO.Text);//null
                    cmd.Parameters.AddWithValue("@CCCode", ddlbankcc.Text);//null
                    cmd.Parameters.AddWithValue("@DCA_Code", ddlbankdca.Text);//null
                    cmd.Parameters.AddWithValue("@Sub_DCA", ddlbanksub.Text);//null

                    cmd.Parameters.AddWithValue("@Description", txtremarks.Text); //test 1
                    cmd.Parameters.AddWithValue("@ModeOfPay", ddlmodeofpay.Text); //"Cash"
                    cmd.Parameters.AddWithValue("@No", txtno.Text);//"00811"
                    cmd.Parameters.AddWithValue("@PaymentType", txttype.Text);//"General"
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());//32D20003097
                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());//SuperAdmin
                    cmd.Parameters.AddWithValue("@Amount", txtdebitamount.Text);//"50000.0000"
                    cmd.Parameters.AddWithValue("@date", txtpaymentdate.Text);//"06-Feb-2016"
                    cmd.Parameters.AddWithValue("@name", txtname.Text);//"Self at Bhilai Fab Unit"
                    cmd.Parameters.AddWithValue("@id", GridView1.SelectedValue.ToString());//9584834602

                }
                else if (txttype.Text == "Transfer")
                {
                    cmd.CommandText = "sp_transfered_money";
                    cmd.Parameters.AddWithValue("@Description", txtremarks.Text);
                    cmd.Parameters.AddWithValue("@ModeOfPay", ddlmodeofpay.Text);
                    cmd.Parameters.AddWithValue("@No", txtno.Text);
                    cmd.Parameters.AddWithValue("@PaymentType", txttype.Text);
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                    cmd.Parameters.AddWithValue("@Amount", txtdebitamount.Text);
                    cmd.Parameters.AddWithValue("@date", txtpaymentdate.Text);
                    cmd.Parameters.AddWithValue("@name", txtname.Text);
                    cmd.Parameters.AddWithValue("@id", GridView1.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@From", ViewState["bank1"].ToString());
                    cmd.Parameters.AddWithValue("@To", ViewState["bank2"].ToString());
                }
                else if (txttype.Text == "TDS")
                {
                    cmd.CommandText = "sp_InvoiceDebit_TDS";
                    cmd.Parameters.AddWithValue("@Bank_Name", ViewState["bank"].ToString());
                    cmd.Parameters.AddWithValue("@Description", txtremarks.Text);
                    cmd.Parameters.AddWithValue("@ModeOfPay", ddlmodeofpay.Text);
                    cmd.Parameters.AddWithValue("@No", txtno.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtdebitamount.Text);
                    cmd.Parameters.AddWithValue("@date", txtpaymentdate.Text);
                    cmd.Parameters.AddWithValue("@id", GridView1.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                }
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "Sucessfull";
                con.Close();
                if (msg == "Sucessfull")
                {
                    FillGrid();
                    JavaScript.UPAlert(Page, msg);
                    mdlpopbank.Hide();
                    Clear();
                }
                else
                {
                    JavaScript.UPAlert(Page, msg);

                }
            }
            else if (HiddenField.Value == "B")
            {
                string s1 = "";
                string s2 = "";
                string s3 = "";
                cmd.Connection = con;
                if (Session["roles"].ToString() == "HoAdmin")
                {
                    s1 = "update bankbook set status='2',ModifiedBy='" + Session["user"].ToString() + "' where id='" + GridView1.SelectedValue.ToString() + "'";
                    s2 = "insert into bankbook_log (status,ModifiedBy,tbl_id,ModifiedDate_System,Type,[User_id])values('2','" + Session["user"].ToString() + "','" + GridView1.SelectedValue.ToString() + "',getdate(),'Update','" + Session["user"].ToString() + "')";
                    s3 = "update VendorPaid_Log set status='2' where transaction_no='" + GridView1.SelectedValue.ToString() + "'";

                }
                else
                {
                    s1 = "update bankbook set status='3',ModifiedBy='" + Session["user"].ToString() + "' where id='" + GridView1.SelectedValue.ToString() + "'";
                    s2 = "insert into bankbook_log (status,ModifiedBy,tbl_id,ModifiedDate_System,Type,[User_id])values('3','" + Session["user"].ToString() + "','" + GridView1.SelectedValue.ToString() + "',getdate(),'Update','" + Session["user"].ToString() + "')";
                    s3 = "update VendorPaid_Log set status='3' where transaction_no='" + GridView1.SelectedValue.ToString() + "'";
                }
                cmd.CommandText = s1 + s2+ s3;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                con.Close();
                if (j == true)
                {
                    JavaScript.UPAlert(Page, "Sucessfull");
                }
                else
                {
                    JavaScript.UPAlert(Page, "Failed");
                }
                mdlpopbank.Hide();
                Clear();
                FillGrid();

            }




        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }


    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex != -1)
        {
            try
            {
                string session = Session["roles"].ToString();
                string id = GridView1.SelectedValue.ToString();
                HiddenField hf = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].Cells[8].FindControl("hf1");
                if (Session["roles"].ToString() == "HoAdmin")
                {
                    if (hf.Value == "A")
                    {
                        Clear();
                        checkboxclear();
                        tblbank.Visible = true;
                        Bankvouchers(id);
                        mdlpopbank.Show();
                        HiddenField.Value = hf.Value;
                    }
                    else if (hf.Value == "L")
                    {
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('verifyloandetails.aspx?id=" + id + " &session=" + session + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');", true);
                    }
                    else if (hf.Value == "B")
                    {
                        Clear();
                        Termaloan(id);
                        checkboxclear();
                        tblbank.Visible = true;
                        mdlpopbank.Show();
                        HiddenField.Value = hf.Value;
                    }
                }
                else if (Session["roles"].ToString() == "SuperAdmin")
                {

                    if (hf.Value == "A")
                    {
                        Clear();
                        checkboxclear();
                        tblbank.Visible = true;
                        Bankvouchers(id);
                        mdlpopbank.Show();
                        HiddenField.Value = hf.Value;
                    }
                    else if (hf.Value == "L")
                    {
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('verifyloandetails.aspx?id=" + id + " &session=" + session + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');", true);
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('inbox2.aspx?id=" + id + " &session=" + session + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');", true);
                    }
                    else if (hf.Value == "B")
                    {
                        Clear();
                        Termaloan(id);
                        checkboxclear();
                        tblbank.Visible = true;
                        mdlpopbank.Show();
                        HiddenField.Value = hf.Value;
                    }
                }

                else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                {

                    if (hf.Value == "A")
                    {
                        Clear();
                        checkboxclear();
                        tblbank.Visible = true;
                        Bankvouchers(id);
                        mdlpopbank.Show();
                        HiddenField.Value = hf.Value;
                    }
                    else if (hf.Value == "L")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('Inbox2.aspx?id=" + id + " &session=" + session + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');", true);
                    }
                    else if (hf.Value == "B")
                    {
                        Clear();
                        Termaloan(id);
                        checkboxclear();
                        tblbank.Visible = true;
                        mdlpopbank.Show();
                        HiddenField.Value = hf.Value;
                    }
                }

            }
            catch (Exception ex)
            {
                Utilities.CatchException(ex);
                JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
            }
        }
    }
    public void Clear()
    {
        txttype.Text = "";
        txtname.Text = "";
        //cascbankcc.SelectedValue = "";
        //cascbankdca.SelectedValue = "";
        //cascbanksub.SelectedValue = "";
        txtVPO.Text = "";
        txtVInno.Text = "";
        txtVindt.Text = "";
        //cascbank.SelectedValue = "";
        txtpaymentdate.Text = "";
        //cascbank.SelectedValue = "";
        ddlmodeofpay.Text = "";
        //txtno.SelectedItem.Text = "";
        txtremarks.Text = "";
        txtdebitamount.Text = "";
        ddlbanksub.Text = "";
        ddlbankdca.Text = "";
        ddlbankcc.Text = "";
        txtcascbank.Text = "";
    }
    public void checkboxclear()
    {
        Chkdate.Checked = false;
        chkdebitamount.Checked = false;
        chkno.Checked = false;
        chkpaymode.Checked = false;
        chkremarks.Checked = false;
        Chkbank.Checked = false;
        Checkfrom.Checked = false;
        Checkto.Checked = false;

    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //--- For Paging ---------
        GridViewRow row = GridView1.BottomPagerRow;
        if (row == null)
        {
            return;
        }

        //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
        Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
        Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

        //if (lblPages != null)
        //{
        lblPages.Text = GridView1.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = GridView1.PageIndex + 1;
        lblCurrent.Text = currentPage.ToString();

        //-- For First and Previous ImageButton
        if (GridView1.PageIndex == 0)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Visible = false;



        }

        //-- For Last and Next ImageButton
        if (GridView1.PageIndex + 1 == GridView1.PageCount)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Visible = false;


        }
    }

    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillGrid();
    }
    public void Termaloan(string id)
    {
        try
        {
            General.Visible = true;
            generalPay.Visible = true;
            trbank.Visible = false;
            Label8.Text = "Po NO";
            Label4.Text = "Invoice No";

            //da = new SqlDataAdapter("Select b.bank_name,b.description,b.debit,p.invoice_date,b.date,b.cc_code,b.dca_code,b.sub_dca,b.invoiceno,b.name,p.po_no from bankbook b join pending_invoice p on b.invoiceno=p.invoiceno where b.id=@id", con);
            da = new SqlDataAdapter("Select b.bank_name,b.description,b.debit,REPLACE(CONVERT(VARCHAR(11),p.invoice_date, 106), ' ', '-')as invoice_date,REPLACE(CONVERT(VARCHAR(11),b.date, 106), ' ', '-')as date,b.cc_code,b.dca_code,b.sub_dca,b.invoiceno,b.name,p.po_no from bankbook b join pending_invoice p on b.invoiceno=p.invoiceno where b.id=@id", con);
            da.SelectCommand.Parameters.AddWithValue("@id", id);
            da.Fill(ds, "3rdparty");
            if (ds.Tables["3rdparty"].Rows.Count > 0)
            {
                txtcascbank.Text = ds.Tables["3rdparty"].Rows[0][0].ToString();
                txtremarks.Text = ds.Tables["3rdparty"].Rows[0][1].ToString();
                txtdebitamount.Text = ds.Tables["3rdparty"].Rows[0][2].ToString().Replace(".0000", ".00");
                txtVindt.Text = ds.Tables["3rdparty"].Rows[0][3].ToString();
                txtpaymentdate.Text = ds.Tables["3rdparty"].Rows[0][4].ToString();
                ddlbankcc.Text = ds.Tables["3rdparty"].Rows[0][5].ToString();
                ddlbankdca.Text = ds.Tables["3rdparty"].Rows[0][6].ToString();
                ddlbanksub.Text = ds.Tables["3rdparty"].Rows[0][7].ToString();
                txtVInno.Text = ds.Tables["3rdparty"].Rows[0][8].ToString();
                txtname.Text = ds.Tables["3rdparty"].Rows[0][9].ToString();
                txtVPO.Text = ds.Tables["3rdparty"].Rows[0][10].ToString();
                txttransaction.Text = id;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
}