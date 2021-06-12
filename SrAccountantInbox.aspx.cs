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
using System.Web.Configuration;

public partial class SrAccountantInbox : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"].ToString());
    SqlCommand cmd = new SqlCommand();
   
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            try
            {
                //lblcashdca.Visible = false;
                //lblcashsub.Visible = false;
                if (Session["roles"].ToString() == "Sr.Accountant")
                {
                    ddlvouchertype.SelectedValue = "1";
                    Label9.Text = "Verify Cash Transfer";
                    Btntransfer.Text = "Verify";
                    tblvoucher.Visible = false;
                    
                    LoadYear();
                    LoadCC();
                    trpopup.Visible = false;
                    
                    ViewState["condition"] = " and id!=''";
                    fillgrid(ViewState["condition"].ToString());
                }
                else if (Session["roles"].ToString() == "HoAdmin")
                {
                    Label9.Text = "Approve Cash Transfer";
                    Btntransfer.Text = "Approve";
                    trvouchertype.Visible = false;                   
                    trgrid1.Visible = false;
                    tblpayment.Visible = false;
                    tblvoucher.Visible = false;

                    fillgrid2();
                    
                }
            }
            catch (Exception ex)
            {
                Utilities.CatchException(ex);
            }
        }
        if (!IsPostBack)
        {
            trdropdowndca.Visible = false;
            trlabeldca.Visible = false;
        }

    }

    public void LoadYear()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    public void fillgrid(string s)
    {

        try
        {
            this.UpdatePanel1.Update();
            da = new SqlDataAdapter("Select i.cc_code,i.description,i.id,REPLACE(CONVERT(VARCHAR(11),i.date, 106), ' ', '-')as date,i.dca_code,i.sub_dca,coalesce(i.PaymentCategory,i.paymenttype)as PaymentCategory,i.Amount,i.Transaction_no from (select id,date,particulars as[Description],cc_code,dca_code,sub_dca,PaymentCategory,paymenttype,debit as [Amount],Transaction_no from cash_pending)i where i.id>0" + s + " order by i.date desc", con);
            da.Fill(ds, "FillIn");
            GridView1.DataSource = ds.Tables["FillIn"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }



    }

    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid(ViewState["condition"].ToString());
        fillgrid2();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        fillgrid(ViewState["condition"].ToString());

    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;

        fillgrid2();
        

    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        //foreach(GridViewRow rec in GridView1.Rows)
        //{
        //    string id = GridView1.Rows[rec.RowIndex]["id"].ToString();
        //JavaScript.UPAlertRedirect(Page, "Hi", "Inbox.aspx");
        //}
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

        fillgrid(ViewState["condition"].ToString());
    }
    protected void gobtn_Click(object sender, ImageClickEventArgs e)
    {
        trpopup.Visible = false;
        try
        {
            string condition = "";



            if (ddlcccode.SelectedValue != "")
            {
                condition = condition + " and cc_code='" + ddlcccode.SelectedValue + "'";
            }


            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    if (ddlDate.SelectedIndex != 0)
                        condition = condition + " and convert(datetime,i.date)='" + ddlMonth.SelectedValue + "/" + ddlDate.SelectedValue + "/" + yy + "'";
                    else
                    {
                        condition = condition + " and  datepart(mm,i.date)=" + ddlMonth.SelectedValue + " and datepart(yy,i.date)=" + yy;
                    }
                }
                else
                {
                    condition = condition + " and convert(datetime,i.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                }
            }

            if (ddldetailhead.SelectedItem.Text != "")
            {
                condition = condition + " and i.dca_code='" + ddldetailhead.SelectedItem.Text + "'";

            }
            if (ddlsubdetail.SelectedItem.Text != "")
            {
                condition = condition + " and i.sub_dca='" + ddlsubdetail.SelectedItem.Text + "'";

            }

            ViewState["condition"] = condition;
            ////h1.Value = condition;
            GridView1.PageIndex = 1;
            fillgrid(ViewState["condition"].ToString());


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlpagecount_SelectedIndexChanged1(object sender, EventArgs e)
    {

        fillgrid(ViewState["condition"].ToString());

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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (GridView1.Rows[e.RowIndex].Cells[7].Text == "Service Provider" || GridView1.Rows[e.RowIndex].Cells[7].Text == "Supplier")
            {
                cmd.Connection = con;
                cmd = new SqlCommand("sp_ServiceProviderpayment_Rejectsp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["Transaction_no"].ToString());
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                if (msg == "Rejected")
                    JavaScript.UPAlert(Page, msg);
                else
                    JavaScript.UPAlert(Page, msg);
                con.Close();
                fillgrid(ViewState["condition"].ToString());
            }
            else if (GridView1.Rows[e.RowIndex].Cells[7].Text == "Retention" || GridView1.Rows[e.RowIndex].Cells[7].Text == "Hold")
            {
                cmd.Connection = con;
                cmd = new SqlCommand("sp_VendorRetentionHold_Reject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["Transaction_no"].ToString());
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                if (msg == "Rejected")
                    JavaScript.UPAlert(Page, msg);
                else
                    JavaScript.UPAlert(Page, msg);
                con.Close();
                fillgrid(ViewState["condition"].ToString());
            }
            else
            {

                cmd.Connection = con;
                cmd = new SqlCommand("sp_CashVoucher_Reject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["id"].ToString());
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                if (msg == "Rejected")
                    JavaScript.UPAlert(Page, msg);
                else
                    JavaScript.UPAlert(Page, msg);
                con.Close();
                fillgrid(ViewState["condition"].ToString());

            }

           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        trpopup.Visible = true;
        Reset();
        checkboxclear();
        try
        {
            da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,particulars,name,cc_code,paid_against,dca_code,sub_dca,debit,transaction_type,vendor_id,(case when REPLACE(CONVERT(VARCHAR(11),convert(datetime,paid_date, 9),106), ' ', '-') is not null then REPLACE(CONVERT(VARCHAR(11),convert(datetime,paid_date, 9),106), ' ', '-') else REPLACE(CONVERT(VARCHAR(11),convert(datetime,modifiedby_date, 9),106), ' ', '-') end)as paiddate,paymenttype,invoiceno,paymentCategory,transaction_no  from cash_pending where  id='" + GridView1.SelectedValue.ToString() + "'", con);
            da.Fill(ds, "voucherinfo");
            if (ds.Tables["voucherinfo"].Rows.Count > 0)
            {
                txtdate.Text = ds.Tables["voucherinfo"].Rows[0][0].ToString();
                txtcashdesc.Text = ds.Tables["voucherinfo"].Rows[0][1].ToString();
                txtcashname.Text = ds.Tables["voucherinfo"].Rows[0][2].ToString();
                txtcashcc.Text = ds.Tables["voucherinfo"].Rows[0][3].ToString();
                hfpaymentcategory.Value=ds.Tables["voucherinfo"].Rows[0][13].ToString();
                hfpaymenttype.Value = ds.Tables["voucherinfo"].Rows[0][11].ToString();
                if (hfpaymentcategory.Value != "Service Provider" && hfpaymentcategory.Value != "Supplier")
                {
                    if (ds.Tables["voucherinfo"].Rows[0][11].ToString() == "Retention" || ds.Tables["voucherinfo"].Rows[0][11].ToString() == "Hold")
                    {
                        trdropdowndca.Visible = false;
                        trlabeldca.Visible = true;
                        txtdate.Enabled = false;
                        ddlcashcc.Enabled = false;
                        txtcashdebit.Enabled = false;
                        ddltype.Enabled = false;
                        ddlvendor.Enabled = false;
                        txtcashname.Enabled = false;
                        txtpaiddate.Enabled = false;
                    }
                    else
                    {
                        trdropdowndca.Visible = true;
                        trlabeldca.Visible = false;
                        txtdate.Enabled = true;
                        ddlcashcc.Enabled = true;
                        txtcashdebit.Enabled = true;
                        ddltype.Enabled = true;
                        ddlvendor.Enabled = true;
                        txtcashname.Enabled = true;
                        txtpaiddate.Enabled = true;
                    }
                    casccashdca.SelectedValue = ds.Tables["voucherinfo"].Rows[0][5].ToString();
                    casccashsub.SelectedValue = ds.Tables["voucherinfo"].Rows[0][6].ToString();
                }
                else
                {
                    trdropdowndca.Visible = false;
                    trlabeldca.Visible = true;
                    txtdate.Enabled = false;
                    ddlcashcc.Enabled = false;
                    txtcashdebit.Enabled = false;
                    ddltype.Enabled = false;
                    ddlvendor.Enabled = false;
                    txtcashname.Enabled = false;
                    txtpaiddate.Enabled = false;
                    lblcashdca.Text = ds.Tables["voucherinfo"].Rows[0][5].ToString();
                    lblcashsub.Text = ds.Tables["voucherinfo"].Rows[0][6].ToString();
                }
                txtcashdebit.Text = ds.Tables["voucherinfo"].Rows[0][7].ToString();
                txttransaction.Text = ds.Tables["voucherinfo"].Rows[0][8].ToString();
                ViewState["Vendor"] = ds.Tables["voucherinfo"].Rows[0][9].ToString();
                ViewState["inv"] = ds.Tables["voucherinfo"].Rows[0][12].ToString();
                if (ds.Tables["voucherinfo"].Rows[0][8].ToString() == "Debit")
                {
                    ddlcashcc.Visible = false;
                    lblpaidagainst.Visible = false;
                    Chkcc1.Visible = false;
                }
                else
                {
                    ddlcashcc.Visible = true;
                    ddlcashcc.SelectedValue = ds.Tables["voucherinfo"].Rows[0][4].ToString();
                    Chkcc1.Visible = true;
                    lblpaidagainst.Visible = true;
                }
                txtpaiddate.Text = ds.Tables["voucherinfo"].Rows[0][10].ToString();
                if (ds.Tables["voucherinfo"].Rows[0][9].ToString() != "")
                {
                    paymenttype();
                    laodvendor(ds.Tables["voucherinfo"].Rows[0][3].ToString(), ds.Tables["voucherinfo"].Rows[0][4].ToString());
                    ddlvendor.SelectedItem.Text = ds.Tables["voucherinfo"].Rows[0][9].ToString();
                    ddltype.SelectedValue = ds.Tables["voucherinfo"].Rows[0][11].ToString();
                    tblvendor.Visible = true;
                    //tblpo.Visible = true;
                    txtpaiddate.Visible = false;
                    lblpaiddate.Visible = false;
                    //CheckBox2.Visible = false;
                    grd.Visible = true;
                    CheckBox1.Visible = false;
                    //fillpo(ds.Tables["voucherinfo"].Rows[0][9].ToString(), ds.Tables["voucherinfo"].Rows[0][11].ToString());
                    da = new SqlDataAdapter("select DISTINCT PaymentType from BankBook where Transaction_No='" + ds.Tables["voucherinfo"].Rows[0][14].ToString() + "'", con);
                    da.Fill(ds, "Typecheck");
                    if (ds.Tables["Typecheck"].Rows[0].ItemArray[0].ToString() != "TDS")
                    {
                        grd.Visible = true;
                        da = new SqlDataAdapter("Select b.invoiceno,p.cc_code,p.dca_code,p.vendor_id,p.total,p.tds,p.retention,p.netamount,p.hold,b.debit from pending_invoice p join bankbook b on p.invoiceno=b.invoiceno where b.Transaction_No='" + ds.Tables["voucherinfo"].Rows[0][14].ToString() + "'", con);
                        da.Fill(ds, "fillgrid");
                        grd.DataSource = ds.Tables["fillgrid"];
                        grd.DataBind();
                    }
                }
                else
                {
                    txtpaiddate.Visible = true;
                    lblpaiddate.Visible = true;
                    //CheckBox2.Visible = true;
                    tblvendor.Visible = false;
                    //tblpo.Visible = false;
                    grd.Visible = false;
                    CheckBox1.Visible = true;

                }
            }
            else
            {
                JavaScript.UPAlert(Page, "This Voucher is already approved");

            }
            //return ds.Tables["voucherInfo"].Rows;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        //fillpop();

    }

    //public void fillpop()
    //{
    //    if (ddltype.SelectedItem.Text == "Invoice")
    //        da = new SqlDataAdapter("select invoiceno,total,NetAmount,balance,REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date from pending_invoice where po_no='" + ddlvenpo1.SelectedValue + "' and convert(datetime,invoice_date)<='" + txtdate.Text + "' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and status not in('1','2','2A') and balance!='0' ", con);
    //    else if (ddltype.SelectedItem.Text == "Retention")
    //        da = new SqlDataAdapter("select invoiceno,total,NetAmount,retention_balance as [balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date from pending_invoice where po_no='" + ddlvenpo1.SelectedValue + "' and convert(datetime,invoice_date)<='" + txtdate.Text + "' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and Retention_balance!='0' ", con);
    //    else if (ddltype.SelectedItem.Text == "Hold")
    //        da = new SqlDataAdapter("select invoiceno,total,NetAmount,hold_balance as [balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date from pending_invoice where po_no='" + ddlvenpo1.SelectedValue + "' and convert(datetime,invoice_date)<='" + txtdate.Text + "' and vendor_id='" + ddlvendor.SelectedItem.Text + "'  and hold_balance!='0' ", con);
    //    else if (ddltype.SelectedItem.Text == "Service Tax")
    //        da = new SqlDataAdapter("select invoiceno,total,NetAmount,balance as [balance],REPLACE(CONVERT(VARCHAR(11),convert(datetime,invoice_date,101),106), ' ', '-') as invoice_date from pending_invoice where po_no='" + ddlvenpo1.SelectedValue + "' and convert(datetime,invoice_date)<='" + txtdate.Text + "' and vendor_id='" + ddlvendor.SelectedItem.Text + "'  and balance!='0' and paymenttype='Service Tax'", con);


    //    da.Fill(ds, "fill");
    //    if (ds.Tables["fill"].Rows.Count > 0)
    //    {

    //        grd.DataSource = ds.Tables["fill"];
    //        grd.DataBind();
    //        btncashApprove.Visible = true;
    //    }
    //    else
    //    {
    //        btncashApprove.Visible = false;
    //        grd.DataSource = null;
    //        grd.DataBind();
    //        //JavaScript.UPAlert(Page, "There is no Payables");
    //    }


    //}


    protected void btncashApprove_Click1(object sender, EventArgs e)
    {
        try
        {
            if (Session["user"] != null)
            {
                if (ViewState["Vendor"].ToString() != "")
                {
                    da = new SqlDataAdapter("sp_vendorpayment_bycashNew", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;                   
                    da.SelectCommand.Parameters.AddWithValue("@TransactionType", SqlDbType.VarChar).Value = txttransaction.Text;
                    da.SelectCommand.Parameters.AddWithValue("@Date", SqlDbType.DateTime).Value = txtdate.Text;
                    da.SelectCommand.Parameters.AddWithValue("@Description", SqlDbType.VarChar).Value = txtcashdesc.Text;                   
                    da.SelectCommand.Parameters.AddWithValue("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
                    da.SelectCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = GridView1.SelectedValue.ToString();                   
                    da.Fill(ds, "vendorpayment");
                    if (ds.Tables["vendorpayment"].Rows[0].ItemArray[0].ToString() == "Cash Flow Completed SuccessFully")
                    {
                        JavaScript.UPAlert(Page, "Cash Flow Completed SuccessFully. The Voucher Id is: " + ds.Tables["vendorpayment"].Rows[0].ItemArray[1].ToString());
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, ds.Tables["vendorpayment"].Rows[0].ItemArray[0].ToString());
                    }
                    trpopup.Visible = false;
                    fillgrid(ViewState["condition"].ToString());

                }
                else
                {

                    da = new SqlDataAdapter("ISValidDCA_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ModeofTransaction", SqlDbType.DateTime).Value = txttransaction.Text;
                    da.SelectCommand.Parameters.AddWithValue("@DCACode", SqlDbType.VarChar).Value = ddlcashdca.SelectedValue;
                    da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = txtcashcc.Text;
                    da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "Cash Voucher";
                    if (txttransaction.Text == "Paid Against")
                    {
                        da.SelectCommand.Parameters.AddWithValue("@PaidAgainst", SqlDbType.VarChar).Value = ddlcashcc.SelectedItem.Text;
                    }
                    da.SelectCommand.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = GridView1.SelectedValue.ToString();
                    da.Fill(ds, "Isvalid");
                    if (ds.Tables["Isvalid"].Rows[0][0].ToString() == "Invalid DCA")
                    {
                        JavaScript.UPAlert(Page, ds.Tables["Isvalid"].Rows[0][0].ToString());
                    }
                    else if (ds.Tables["Isvalid"].Rows[0][0].ToString() == "Valid")
                    {

                        grd.Visible = false;
                        da = new SqlDataAdapter("sp_CashFlow_Insert", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@PaidDate", SqlDbType.DateTime).Value = txtpaiddate.Text;
                        da.SelectCommand.Parameters.AddWithValue("@TransactionType", SqlDbType.VarChar).Value = txttransaction.Text;
                        da.SelectCommand.Parameters.AddWithValue("@Date", SqlDbType.DateTime).Value = txtdate.Text;
                        da.SelectCommand.Parameters.AddWithValue("@CC_Code", SqlDbType.VarChar).Value = txtcashcc.Text;
                        da.SelectCommand.Parameters.AddWithValue("@Amount", SqlDbType.Money).Value = txtcashdebit.Text;
                        da.SelectCommand.Parameters.AddWithValue("@Dca", SqlDbType.VarChar).Value = ddlcashdca.SelectedValue;
                        if (ddlcashsub.SelectedItem.Text != "")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@SubDca", SqlDbType.VarChar).Value = ddlcashsub.SelectedItem.Text;
                        }
                        da.SelectCommand.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = txtcashname.Text;
                        da.SelectCommand.Parameters.AddWithValue("@Description", SqlDbType.VarChar).Value = txtcashdesc.Text;
                        if (txttransaction.Text == "Paid Against")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@CC_Code1", SqlDbType.VarChar).Value = ddlcashcc.SelectedItem.Text;
                        }
                        da.SelectCommand.Parameters.AddWithValue("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
                        da.SelectCommand.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = GridView1.SelectedValue.ToString();
                        da.SelectCommand.Parameters.AddWithValue("@invoiceno", SqlDbType.VarChar).Value = ViewState["inv"].ToString();
                        da.Fill(ds, "cashflowinsert");
                        if (ds.Tables["cashflowinsert"].Rows[0].ItemArray[0].ToString() == "Credited SuccessFully")
                        {
                            JavaScript.UPAlert(Page, "Voucher Credited SuccessFully. The Voucher Id is: " + ds.Tables["cashflowinsert"].Rows[0].ItemArray[1].ToString());
                        }
                        else if (ds.Tables["cashflowinsert"].Rows[0].ItemArray[0].ToString() == "Approved SuccessFully")
                        {
                            JavaScript.UPAlert(Page, "Voucher Approved SuccessFully. The Voucher Id is: " + ds.Tables["cashflowinsert"].Rows[0].ItemArray[1].ToString());
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, ds.Tables["cashflowinsert"].Rows[0].ItemArray[0].ToString());
                        }

                        trpopup.Visible = false;
                        fillgrid(ViewState["condition"].ToString());
                    }
                }
            }
            else
            {
                Response.Redirect("SessionExpire.aspx");
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
            if (ViewState["Vendor"].ToString() == "")
            {
                ds.Tables["Isvalid"].Clear();
            }
        }
    }



    public void fillpo(string vendor, string paymenttype)
    {

        //ddlvenpo1.Items.Clear();
        //if (paymenttype.ToString() == "Invoice")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and (cc_code='" + txtcashcc.Text + "' or cc_code='" + ddlcashcc.SelectedValue + "')", con);
        //else if (paymenttype.ToString() == "Retention")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where Retention_balance!='0' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and (cc_code='" + txtcashcc.Text + "' or cc_code='" + ddlcashcc.SelectedValue + "') ", con);
        //else if (paymenttype.ToString() == "Hold")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where Hold_balance!='0' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and (cc_code='" + txtcashcc.Text + "' or cc_code='" + ddlcashcc.SelectedValue + "') ", con);
        //else if (paymenttype.ToString() == "Service Tax")
        //    da = new SqlDataAdapter("select distinct po_no from pending_invoice where balance!='0' and vendor_id='" + ddlvendor.SelectedItem.Text + "' and (cc_code='" + txtcashcc.Text + "' or cc_code='" + ddlcashcc.SelectedValue + "') and paymenttype='Service Tax'", con);

        //da.Fill(ds, "po");
        //if (ds.Tables["po"].Rows.Count > 0)
        //{
        //    ddlvenpo1.DataTextField = "po_no";
        //    ddlvenpo1.DataValueField = "po_no";
        //    ddlvenpo1.DataSource = ds.Tables["po"];
        //    ddlvenpo1.DataBind();
        //    ddlvenpo1.Items.Insert(0, "Select PO");
        //}
        //else
        //    ddlvenpo1.Items.Insert(0, "Select PO");
    }
    public void paymenttype()
    {
        ddltype.Items.Clear();
        ddltype.Items.Add("Select Payment Type");
        ddltype.Items.Add("Invoice");
        ddltype.Items.Add("Retention");
        ddltype.Items.Add("Hold");
        ddltype.Items.Add("Service Tax");
    }

    //protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    grd.DataSource = null;
    //    grd.DataBind();
    //    fillpo(ddlvendor.SelectedItem.Text, ddltype.SelectedItem.Text);
    //    fillpop();

    //}
    private void laodvendor(string cc_code, string paidagainst)
    {
        da = new SqlDataAdapter("Select distinct pi.vendor_id from pending_invoice pi join vendor v on pi.vendor_id=v.vendor_id where (cc_code='" + txtcashcc.Text + "' or cc_code='" + ddlcashcc.SelectedValue + "') and v.status='2'", con);
        da.Fill(ds, "filltype");
        ddlvendor.DataTextField = "vendor_id";
        ddlvendor.DataValueField = "vendor_id";
        ddlvendor.DataSource = ds.Tables["filltype"];
        ddlvendor.DataBind();
        ddlvendor.Items.Insert(0, "Select Vendor");

    }
    private void LoadCC()
    {
        da = new SqlDataAdapter("Select cc_code from Cost_Center where status in ('Old','New')", con);
        da.Fill(ds, "fillCC");
        ddlcashcc.DataTextField = "cc_code";
        ddlcashcc.DataValueField = "cc_code";
        ddlcashcc.DataSource = ds.Tables["fillCC"];
        ddlcashcc.DataBind();
        ddlcashcc.Items.Insert(0, "Select Cost Center");

    }
    //protected void ddlvenpo1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillpop();
    //}
    protected void ddlcashcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["Vendor"].ToString() != "")
        {
            laodvendor(txtcashcc.Text, ddlcashcc.SelectedItem.Text);
            fillpo(ddlvendor.SelectedValue, ddltype.SelectedItem.Text);
            //fillpop();
        }
    }

    public void checkboxclear()
    {
        Chkdate.Checked = false;
        Chkamt.Checked = false;
        chkcc.Checked = false;
        Chkcc1.Checked = false;
        Chkdca.Checked = false;
        Chkdesc.Checked = false;
        Chkname.Checked = false;
        Chksub.Checked = false;
        chkvendor.Checked = false;
        //CheckBox2.Checked = false;
        CheckBox1.Checked = false;
    }
    public void Reset()
    {
        txttransaction.Text = "";
        txtdate.Text = "";
        txtpaiddate.Text = "";
        txtcashcc.Text = "";
        //ddlcashcc.SelectedValue = "";
        casccashdca.SelectedValue = "";
        casccashsub.SelectedValue = "";
        //ddlvendor.SelectedValue = "0";
        //ddlvenpo1.SelectedValue = "";
        txtcashname.Text = "";
        txtcashdesc.Text = "";
        txtcashdebit.Text = "";
        grd.DataSource = null;
        grd.DataBind();
        //ddltype.SelectedItem.Text = "";

    }

    protected void ddlvouchertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        trpopup.Visible = false;
        if (ddlvouchertype.SelectedValue == "1")
        {
            trgrid1.Visible = true;
            GridView1.Visible = true;            
            GridView2.Visible = false;
            tblvoucher.Visible = false;            
        }
        else
        {           
            trgrid1.Visible = false;
            GridView1.Visible = false;
            fillgrid2();
            GridView2.Visible = true;
            
        }
    }
  
    protected void GridView2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        tblvoucher.Visible = true;
        ViewState["id"] = GridView2.SelectedValue.ToString();
        filltable(GridView2.SelectedValue.ToString());       
    }
    public void filltable(string id)
    {
       
        try
        {

            da = new SqlDataAdapter("select id,category,REPLACE(CONVERT(VARCHAR(11),convert(datetime,voucherdate,101),106), ' ', '-') as voucherdate,description,CAST(isnull(debit,0) AS Decimal(20,2))debit ,receivedcc,receivedbank,transfercc,type FROM cash_transfer where  id='" + id.ToString() + "'", con);
                da.Fill(ds, "voucherinfo1");
                
                if (ds.Tables["voucherinfo1"].Rows.Count > 0)
                {
                    ViewState["type"] = ds.Tables["voucherinfo1"].Rows[0][8].ToString();
                    txtcategory.Text = ds.Tables["voucherinfo1"].Rows[0][1].ToString();
                    txtDesc.Text = ds.Tables["voucherinfo1"].Rows[0][3].ToString();
                    txtVoucherDate.Text = ds.Tables["voucherinfo1"].Rows[0][2].ToString();
                    txtdebit.Text = ds.Tables["voucherinfo1"].Rows[0][4].ToString();
                    hfdebit.Value = ds.Tables["voucherinfo1"].Rows[0][4].ToString();
                    lbltransfercc.Text = ds.Tables["voucherinfo1"].Rows[0][7].ToString();
                    if (txtcategory.Text == "Bank")
                    {
                        trlist.Visible = true;
                        lbllist.Text = "Bank";  
                      
                        da = new SqlDataAdapter("select bank_name,bank_id FROM bank_branch where status='3'", con);
                        da.Fill(ds, "bankname");
                        ddllist1.DataTextField = "bank_name";
                        ddllist1.DataValueField = "bank_name";
                        ddllist1.DataSource = ds.Tables["bankname"];
                        ddllist1.DataBind();
                        ddllist1.SelectedValue = ds.Tables["voucherinfo1"].Rows[0][6].ToString();

                    }
                    else if (txtcategory.Text == "CostCenter")
                    {
                        trlist.Visible = true;
                        lbllist.Text = "Cost Center";
                        da = new SqlDataAdapter("select cc_code,cc_name from cost_center", con);
                        da.Fill(ds, "costcenter");
                        ddllist1.DataTextField = "cc_code";
                        ddllist1.DataValueField = "cc_code";
                        ddllist1.DataSource = ds.Tables["costcenter"];
                        ddllist1.DataBind();
                        ddllist1.SelectedValue = ds.Tables["voucherinfo1"].Rows[0][5].ToString();
                    }

                    else
                    {
                        trlist.Visible = false;

                    }                
               
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillgrid2()
    {
        UpdatePanel1.Update();
        if (Session["roles"].ToString() == "Sr.Accountant")
            da = new SqlDataAdapter("select id,category,REPLACE(CONVERT(VARCHAR(11),convert(datetime,voucherdate,101),106), ' ', '-') as voucherdate,description,debit FROM cash_transfer where status='1' order by id desc ", con);
        else if (Session["roles"].ToString() == "HoAdmin")
            da = new SqlDataAdapter("select id,category,REPLACE(CONVERT(VARCHAR(11),convert(datetime,voucherdate,101),106), ' ', '-') as voucherdate,description,debit FROM cash_transfer where status in ('2') order by id desc ", con);
        da.Fill(ds, "fillg2");
        GridView2.DataSource = ds.Tables["fillg2"];
 
        GridView2.DataBind();
    }
    

    protected void Btntransfer_Click1(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["type"].ToString() == "2")
            {
                cmd = new SqlCommand("Central_Day_Book_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                cmd = new SqlCommand("transfercash_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
            }
            cmd.Parameters.AddWithValue("@Id", GridView2.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Amount", txtdebit.Text);
            cmd.Parameters.AddWithValue("@category", txtcategory.Text);
            //cmd.Parameters.AddWithValue("@transfercc", ddlcostcenter.SelectedValue);
            cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
            cmd.Parameters.AddWithValue("@date", txtVoucherDate.Text);
            if (txtcategory.Text == "Bank")
                cmd.Parameters.AddWithValue("@bank", ddllist1.SelectedValue);
            else if (txtcategory.Text == "CostCenter")
                cmd.Parameters.AddWithValue("@costcentre", ddllist1.SelectedValue);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            con.Open();
            if (txtcategory.Text == "Bank")
            {
                BankDateCheck objdate = new BankDateCheck();
                if (objdate.IsBankDateCheck(txtVoucherDate.Text, ddllist1.SelectedValue))
                {
                   // string msg = "kk";
                     string msg = cmd.ExecuteScalar().ToString();
                    if (msg == "Successfull")
                    {
                        JavaScript.UPAlert(Page, msg);
                        clear(txtcategory.Text);
                        tblvoucher.Visible = false;
                        fillgrid2();
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, msg);
                    }
                }
                else
                {
                    JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
                }
            }
            else
            {
                //string msg = "kk";
                string msg = cmd.ExecuteScalar().ToString();
                if (msg == "Successfull")
                {
                    JavaScript.UPAlert(Page, msg);
                    clear(txtcategory.Text);
                    tblvoucher.Visible = false;
                    fillgrid2();
                }
                else
                {
                    JavaScript.UPAlert(Page, msg);
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }
       
        

    }

      public void clear(string Transfertype)
      {
          txtVoucherDate.Text = "";
          txtcategory.Text = "";
          if (Transfertype != "CentralDayBook")
          {
              ddllist1.SelectedIndex = 0;
          }
          txtDesc.Text = "";
      }
      protected void GridView2_DataBound(object sender, EventArgs e)
      {
          //--- For Paging ---------
          GridViewRow row = GridView2.BottomPagerRow;

          if (row == null)
          {
              return;
          }

          //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
          Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
          Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

          //if (lblPages != null)
          //{
          lblPages.Text = GridView2.PageCount.ToString();
          //}

          //if (lblCurrent != null)
          //{
          int currentPage = GridView2.PageIndex + 1;
          lblCurrent.Text = currentPage.ToString();

          //-- For First and Previous ImageButton
          if (GridView2.PageIndex == 0)
          {
              ((ImageButton)GridView2.BottomPagerRow.FindControl("btnFirst1")).Enabled = false;
              ((ImageButton)GridView2.BottomPagerRow.FindControl("btnFirst1")).Visible = false;

              ((ImageButton)GridView2.BottomPagerRow.FindControl("btnPrev1")).Enabled = false;
              ((ImageButton)GridView2.BottomPagerRow.FindControl("btnPrev1")).Visible = false;



          }

          //-- For Last and Next ImageButton
          if (GridView2.PageIndex + 1 == GridView2.PageCount)
          {
              ((ImageButton)GridView2.BottomPagerRow.FindControl("btnLast1")).Enabled = false;
              ((ImageButton)GridView2.BottomPagerRow.FindControl("btnLast1")).Visible = false;

              ((ImageButton)GridView2.BottomPagerRow.FindControl("btnNext1")).Enabled = false;
              ((ImageButton)GridView2.BottomPagerRow.FindControl("btnNext1")).Visible = false;


          }
      }
      protected void btnFirst1_Command(object sender, CommandEventArgs e)
      {
          Grid2Paginate(sender, e);
      }
      protected void btnPrev1_Command(object sender, CommandEventArgs e)
      {
          Grid2Paginate(sender, e);
      }
      protected void btnNext1_Command(object sender, CommandEventArgs e)
      {

          Grid2Paginate(sender, e);
      }
      protected void btnLast1_Command(object sender, CommandEventArgs e)
      {
          Grid2Paginate(sender, e);
      }
      protected void Grid2Paginate(object sender, CommandEventArgs e)
      {
          // Get the Current Page Selected
          int iCurrentIndex = GridView2.PageIndex;

          switch (e.CommandArgument.ToString().ToLower())
          {
              case "first":
                  GridView2.PageIndex = 0;
                  break;
              case "prev":
                  if (GridView2.PageIndex != 0)
                  {
                      GridView2.PageIndex = iCurrentIndex - 1;
                  }
                  break;
              case "next":
                  GridView2.PageIndex = iCurrentIndex + 1;
                  break;
              case "last":
                  GridView2.PageIndex = GridView2.PageCount;
                  break;
          }

          //Populate the GridView Control

          fillgrid2();
          tblvoucher.Visible = false;
      }
      protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
      {
          try
          {
              cmd.Connection=con;
              cmd = new SqlCommand("sp_CashTransfer_Reject", con);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@id", GridView2.DataKeys[e.RowIndex]["id"].ToString());
              
              con.Open();
              string msg = cmd.ExecuteScalar().ToString();
              if (msg == "Rejected")
                  JavaScript.UPAlert(Page, msg);
              else
                  JavaScript.UPAlert(Page, msg);
              con.Close();

              fillgrid2();

         }
          catch (Exception ex)
          {
              Utilities.CatchException(ex);
          }

      }      
     
}
