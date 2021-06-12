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

public partial class Admin_frmCashflow : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        // put page load event code
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        trdcasdca.Visible = false;
        trspanlbl.Visible = false;
        //esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 37);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
        //rbtntype.SelectedIndex = 0;
     }

   

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void lnkViewreport_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("select isnull(max(id),0) from cash_book where created_by='" + Session["user"].ToString() + "'", con);
        con.Open();
        string id = cmd.ExecuteScalar().ToString();
        con.Close();
        if (id != "0")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "window.open('frmViewReport.aspx?Voucherid=" + id + "','Report','width=780,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');", true);
        }
        else
        {
            JavaScript.Alert("No Transactions Occured");
        }
    }
    [WebMethod]
    public static string IsVoucherAvailable(string dca_code, string date, string Amount, string name)
    {

        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter("select cc_code,dca_code,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,name,debit from cash_pending where cc_code='" + HttpContext.Current.Session["cc_code"].ToString() + "' and dca_code='" + dca_code + "'and date='" + date + "'and name='" + name + "' and debit='" + Amount + "'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "checkvoucher");
        conWB.Open();
        if (dsWB.Tables["checkvoucher"].Rows.Count > 0)
        {
            string s = HttpContext.Current.Session["cc_code"].ToString();
            string Nname = dsWB.Tables[0].Rows[0].ItemArray[3].ToString();
            string NAmount = dsWB.Tables[0].Rows[0].ItemArray[4].ToString();
            string NDate = dsWB.Tables[0].Rows[0].ItemArray[2].ToString();

         
            if (s == dsWB.Tables[0].Rows[0].ItemArray[0].ToString() && dca_code == dsWB.Tables[0].Rows[0].ItemArray[1].ToString() && date == dsWB.Tables[0].Rows[0].ItemArray[2].ToString() && name == dsWB.Tables[0].Rows[0].ItemArray[3].ToString() && Amount == dsWB.Tables[0].Rows[0].ItemArray[4].ToString().Replace(".0000", ""))
            {
                string msg = "You are already make the voucher to   " + Nname + "  of  " + NAmount + "  on  " + NDate + "  Do you want to continue?";
                return msg;
            }

            else
            {
                string Empty1 = "";
                return Empty1;

            }
        }
        else
        {
            string Empty = "";
            return Empty;
        }


    }
   
    [WebMethod]
    public static string IsPOPayAvailable(string PONO, string Paymenttype)
    {
        string Values = "";

        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter();
        //SqlDataAdapter daWB = new SqlDataAdapter("Select distinct dca_code,sub_dca,(Select isnull(Sum(balance),0) from pending_invoice where po_no='" + PONO + "')Balance from pending_invoice where po_no='" + PONO + "' ", conWB);
        if (Paymenttype == "Invoice")
            daWB = new SqlDataAdapter("Select p.dca_code,p.sub_dca,(isnull(p.balance,0)-isnull(c.debit,0))POBalance from (Select distinct po_no,dca_code,sub_dca,(Select isnull(Sum(balance),0) from pending_invoice where po_no='" + PONO + "' and paymenttype in ('Service Provider','Supplier'))Balance from pending_invoice where po_no='" + PONO + "')p full outer join (Select po_no,sum(debit)Debit from cash_pending where  paymenttype='Invoice' group by po_no)c on p.po_no=c.po_no where p.po_no='" + PONO + "'", conWB);
        else if (Paymenttype == "Retention")
            daWB = new SqlDataAdapter("Select p.dca_code,p.sub_dca,(isnull(p.balance,0)-isnull(c.debit,0))POBalance from (Select distinct po_no,dca_code,sub_dca,(Select isnull(Sum(retention_balance),0) from pending_invoice where po_no='" + PONO + "' and paymenttype in ('Service Provider','Supplier'))Balance from pending_invoice where po_no='" + PONO + "' and paymenttype in ('Service Provider','Supplier'))p full outer join (Select po_no,sum(debit)Debit from cash_pending where paymenttype='Retention' group by po_no)c on p.po_no=c.po_no where p.po_no='" + PONO + "'", conWB);
        else if (Paymenttype == "Hold")
            daWB = new SqlDataAdapter("Select p.dca_code,p.sub_dca,(isnull(p.balance,0)-isnull(c.debit,0))POBalance from (Select distinct po_no,dca_code,sub_dca,(Select isnull(Sum(hold_balance),0) from pending_invoice where po_no='" + PONO + "' and paymenttype in ('Service Provider','Supplier'))Balance from pending_invoice where po_no='" + PONO + "' and paymenttype in ('Service Provider','Supplier'))p full outer join (Select po_no,sum(debit)Debit from cash_pending where paymenttype='Hold' group by po_no)c on p.po_no=c.po_no where p.po_no='" + PONO + "'", conWB);
        else if (Paymenttype == "Service Tax")
            daWB = new SqlDataAdapter("Select p.dca_code,p.sub_dca,(isnull(p.balance,0)-isnull(c.debit,0))POBalance from (Select distinct po_no,dca_code,sub_dca,(Select isnull(Sum(balance),0) from pending_invoice where po_no='" + PONO + "' and paymenttype in ('Service Tax'))Balance from pending_invoice where po_no='" + PONO + "' and paymenttype in ('Service Tax'))p full outer join (Select po_no,sum(debit)Debit from cash_pending where paymenttype='Service Tax' group by po_no)c on p.po_no=c.po_no where p.po_no='" + PONO + "'", conWB);


        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "POPayAvailable");
        conWB.Open();
        if (dsWB.Tables["POPayAvailable"].Rows[0][2].ToString() == "0.0000")
        {
            return "There is no payble";
        }
        else
        {           
            Values = dsWB.Tables["POPayAvailable"].Rows[0].ItemArray[0].ToString();
            Values += "|" + dsWB.Tables["POPayAvailable"].Rows[0].ItemArray[1].ToString();
            Values += "|" + dsWB.Tables["POPayAvailable"].Rows[0].ItemArray[2].ToString().Replace(".0000", ".00");
            return Values;

            //dca.cash(""
        }


    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public static string IsDCAPayAvailable(string DCACode,string CCCode,string rbtntype)
    {

        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter();
        if(rbtntype.ToString()=="0")
        {
            daWB = new SqlDataAdapter("Select * from dcatype where dca_code='" + DCACode + "' and cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + HttpContext.Current.Session["cc_code"].ToString() + "') and status='Active';Select * from DCA_Paymenttype where dca_code='" + DCACode + "' and status='Active' and paymenttype='Direct payment'", conWB);
        }
        else if (rbtntype.ToString() == "1")
        {
            daWB = new SqlDataAdapter("Select * from dcatype where dca_code='" + DCACode + "' and cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + CCCode.ToString() + "') and status='Active';Select * from DCA_Paymenttype where dca_code='" + DCACode + "' and status='Active' and paymenttype='Direct payment'", conWB);
        }
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "DCAAvailable");
        conWB.Open();
        if (dsWB.Tables["DCAAvailable"].Rows.Count > 0 && dsWB.Tables[1].Rows.Count > 0)
        {
           
            return "Valid";
        }
        else
        {
            return "Invalid";
        }


    }
    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            string dca = "";
            if (ddlvendortype.SelectedItem.Text != "General Invoice" && ddlvendortype.SelectedItem.Text != "Select")
            {
            string s = Session["cc_code"].ToString();
            da = new SqlDataAdapter("Select voucher_limit from cost_center where cc_code='" + s + "'", con);
            da.Fill(ds, "check");
            if (ds.Tables["check"].Rows.Count > 0)
            {
                int i = Convert.ToInt32(ds.Tables["check"].Rows[0].ItemArray[0]);
                int j = Convert.ToInt32(txtamt.Text);
                if (ddlvendortype.SelectedValue == "Service Provider" || ddlvendortype.SelectedValue == "Supplier")
                {
                    dca = labeldcacode.Text;
                }
                else
                {
                    dca = ddldetailhead.SelectedItem.Text;
                }
                if ((i < j) || (ddlvendortype.SelectedIndex > 1 && ddlvendor.SelectedItem.Text != "Select Vendor") || ddlcccode.SelectedValue != "" || dca == "DCA-03")
                {
                    cmd = new SqlCommand("sp_CashFlow_Edit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                        if (ddlvendortype.SelectedItem.Text == "Service Provider" || ddlvendortype.SelectedItem.Text == "Supplier")
                    cmd.Parameters.AddWithValue("@PO_NO", ddlvenpo.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Paymenttype", ddltype.SelectedItem.Text);
                }
                else
                {

                    cmd = new SqlCommand("sp_Cashvoucher", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                string trantype = rbtntype.SelectedValue;
                cmd.Parameters.AddWithValue("@TransactionType", trantype);
                cmd.Parameters.AddWithValue("@Date", txtdt.Text);
                cmd.Parameters.AddWithValue("@PaidDate", txtpaiddate.Text);
                cmd.Parameters.AddWithValue("@CC_Code", Session["cc_code"].ToString());
                cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
                if (ddlvendortype.SelectedValue == "Service Provider" || ddlvendortype.SelectedValue == "Supplier")
                {
                    cmd.Parameters.AddWithValue("@Dca", labeldcacode.Text);
                    if (lblsubdcacode.Text != "")
                        cmd.Parameters.AddWithValue("@SubDca", lblsubdcacode.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Dca", ddldetailhead.SelectedItem.Text);
                    if (ddlsubdetail.SelectedItem.Text != "")
                        cmd.Parameters.AddWithValue("@SubDca", ddlsubdetail.SelectedItem.Text);
                }
               
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                if (ddlvendortype.SelectedIndex > 1 && ddlvendor.SelectedItem.Text != "Select Vendor")
                {
                    cmd.Parameters.AddWithValue("@Vendorid", ddlvendor.SelectedValue);

                }
                if (rbtntype.SelectedIndex == 1)
                    cmd.Parameters.AddWithValue("@CC_Code1", ddlcccode.SelectedValue);
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@Paymentcategory", ddlvendortype.SelectedItem.Text);
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "Inserted";
                con.Close();
                lblAlert.Text = msg;
                if (msg == "Voucher Inserted" || msg == "Approved SuccessFully")
                    JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                else
                {
                    fillpos();
                    JavaScript.UPAlert(Page, msg);
                }

                //JavaScript.AlertAndRedirect(msg, Request.Url.ToString());
            }
            else
            {
                JavaScript.Alert("There is no rows");

            }
        }
            else if (ddlvendortype.SelectedItem.Text == "General Invoice")
            {
                string trantype = rbtntype.SelectedValue;
                cmd = new SqlCommand("sp_GeneralInvoiceCashFlow", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransactionType", trantype);
                cmd.Parameters.AddWithValue("@CC_CodeSelf", Session["cc_code"].ToString());              
                cmd.Parameters.AddWithValue("@CC_CodeOther", ddlcccode.SelectedValue);
                cmd.Parameters.AddWithValue("@Paymentcategory", "General Invoice");
                cmd.Parameters.AddWithValue("@InvoiceNo", ddlinvoiceno.SelectedItem.Text);                
                cmd.Parameters.AddWithValue("@Date", txtdt.Text);
                cmd.Parameters.AddWithValue("@PaidDate", txtpaiddate.Text);
                cmd.Parameters.AddWithValue("@Dca", lbldcacode.Text);
                cmd.Parameters.AddWithValue("@SubDca", lblsdcacode.Text);              
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "Voucher Inserted1";
                con.Close();
                lblAlert.Text = msg;
                if (msg == "Voucher Inserted")
                    JavaScript.UPAlertRedirect(Page,msg, Request.Url.ToString());
                else
                    JavaScript.UPAlert(Page,msg);

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
  
    protected void ddlvendor_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("Select distinct po_no from pending_invoice where vendor_id='" + ddlvendor.SelectedValue + "' and (cc_code='" + Session["cc_code"].ToString() + "' or cc_code='" + ddlcccode.SelectedValue + "')", con);
            da.Fill(ds, "po_no");
            ddlvenpo.DataTextField = "po_no";
            ddlvenpo.DataValueField = "po_no";
            ddlvenpo.DataSource = ds.Tables["po"];
            ddlvenpo.DataBind();
            ddlvenpo.Items.Insert(0, "Select PO");
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlvenpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpos();
    }
    public void fillpos()
    {
        try
        {
            if (rbtntype.SelectedValue == "Debit")
            {
                ddlcccode.Visible = false;
            }
            else
            {
                ddlcccode.Visible = true;
            }
            if (ddlvendortype.SelectedValue == "Service Provider" || ddlvendortype.SelectedValue == "Supplier")
            {
                SqlDataAdapter daWB = new SqlDataAdapter();
                //SqlDataAdapter daWB = new SqlDataAdapter("Select distinct dca_code,sub_dca,(Select isnull(Sum(balance),0) from pending_invoice where po_no='" + PONO + "')Balance from pending_invoice where po_no='" + PONO + "' ", conWB);
                if (ddltype.SelectedValue == "Invoice")
                    daWB = new SqlDataAdapter("Select p.dca_code,p.sub_dca,(isnull(p.balance,0)-isnull(c.debit,0))POBalance from (Select distinct po_no,dca_code,sub_dca,(Select isnull(Sum(balance),0) from pending_invoice where po_no='" + ddlvenpo.SelectedValue + "' and paymenttype in ('Service Provider','Supplier'))Balance from pending_invoice where po_no='" + ddlvenpo.SelectedValue + "')p full outer join (Select po_no,sum(debit)Debit from cash_pending where  paymenttype='Invoice' group by po_no)c on p.po_no=c.po_no where p.po_no='" + ddlvenpo.SelectedValue + "'", con);
                else if (ddltype.SelectedValue == "Retention")
                    daWB = new SqlDataAdapter("Select p.dca_code,p.sub_dca,(isnull(p.balance,0)-isnull(c.debit,0))POBalance from (Select distinct po_no,dca_code,sub_dca,(Select isnull(Sum(retention_balance),0) from pending_invoice where po_no='" + ddlvenpo.SelectedValue + "' and paymenttype in ('Service Provider','Supplier'))Balance from pending_invoice where po_no='" + ddlvenpo.SelectedValue + "' and paymenttype in ('Service Provider','Supplier'))p full outer join (Select po_no,sum(debit)Debit from cash_pending where paymenttype='Retention' group by po_no)c on p.po_no=c.po_no where p.po_no='" + ddlvenpo.SelectedValue + "'", con);
                else if (ddltype.SelectedValue == "Hold")
                    daWB = new SqlDataAdapter("Select p.dca_code,p.sub_dca,(isnull(p.balance,0)-isnull(c.debit,0))POBalance from (Select distinct po_no,dca_code,sub_dca,(Select isnull(Sum(hold_balance),0) from pending_invoice where po_no='" + ddlvenpo.SelectedValue + "' and paymenttype in ('Service Provider','Supplier'))Balance from pending_invoice where po_no='" + ddlvenpo.SelectedValue + "' and paymenttype in ('Service Provider','Supplier'))p full outer join (Select po_no,sum(debit)Debit from cash_pending where paymenttype='Hold' group by po_no)c on p.po_no=c.po_no where p.po_no='" + ddlvenpo.SelectedValue + "'", con);
                else if (ddltype.SelectedValue == "Service Tax")
                    daWB = new SqlDataAdapter("Select p.dca_code,p.sub_dca,(isnull(p.balance,0)-isnull(c.debit,0))POBalance from (Select distinct po_no,dca_code,sub_dca,(Select isnull(Sum(balance),0) from pending_invoice where po_no='" + ddlvenpo.SelectedValue + "' and paymenttype in ('Service Tax'))Balance from pending_invoice where po_no='" + ddlvenpo.SelectedValue + "' and paymenttype in ('Service Tax'))p full outer join (Select po_no,sum(debit)Debit from cash_pending where paymenttype='Service Tax' group by po_no)c on p.po_no=c.po_no where p.po_no='" + ddlvenpo.SelectedValue + "'", con);
                daWB.Fill(ds, "filldca");
                trdcasdca.Visible = true;
                trcascadingdcasdca.Visible = true;
                CascadingDropDown2.Enabled = false;
                Label2.Text = "";
                CascadingDropDown3.Enabled = false;
                Label3.Text = "";
                trspan.Visible = false;
                trspanlbl.Visible = true;
                trlbldcasdca.Visible = false;
                labeldcacode.Text = ds.Tables["filldca"].Rows[0].ItemArray[0].ToString();
                lblsubdcacode.Text = ds.Tables["filldca"].Rows[0].ItemArray[1].ToString();
                lblspan.Text = "Net Payable is:  " + ds.Tables["filldca"].Rows[0].ItemArray[2].ToString();
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);

            }
            else
            {
                trdcasdca.Visible = false;
                trcascadingdcasdca.Visible = true;
                CascadingDropDown2.Enabled = true;
                Label2.Text = "";
                CascadingDropDown3.Enabled = true;
                Label3.Text = "";
                trspan.Visible = true;
                trspanlbl.Visible = false;
                trlbldcasdca.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlvendortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        if (ddlvendortype.SelectedItem.Text == "General Invoice")
        {
            if (rbtntype.SelectedValue == "Debit")
                da = new SqlDataAdapter("Select invoiceno from GeneralInvoices where cc_code='" + Session["cc_code"].ToString() + "' and status='3' and Mode_of_pay='Cash' ", con);
            else
                da = new SqlDataAdapter("Select invoiceno from GeneralInvoices where cc_code='" + ddlcccode.SelectedValue + "' and status='3' and Mode_of_pay='Cash' ", con);
            da.Fill(ds, "invoiceno");
            ddlinvoiceno.DataTextField = "invoiceno";
            ddlinvoiceno.DataValueField = "invoiceno";
            ddlinvoiceno.DataSource = ds.Tables["invoiceno"];
            ddlinvoiceno.DataBind();
            ddlinvoiceno.Items.Insert(0, "Invoice No");
        }
        if (rbtntype.SelectedValue == "Paid Against" && ddlvendortype.SelectedItem.Text == "General Invoice")
        {
            if (CascadingDropDown4.SelectedValue != "Select Other CC")
            {
                da = new SqlDataAdapter("Select invoiceno from GeneralInvoices where cc_code='" + ddlcccode.SelectedValue + "' and status='3' and Mode_of_pay='Cash' ", con);
                da.Fill(ds, "invoiceno");
                if (ds.Tables["invoiceno"].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
                    JavaScript.UPAlert(Page, "No Invoices Avaliable for the CC");
                    CascadingDropDown4.SelectedValue = "Select Other CC";
                    ddlvendortype.SelectedIndex = 0;
                    if (ddlinvoiceno.Visible == false)
                        ddlinvoiceno.SelectedIndex = 0;
                    CascadingDropDown2.SelectedValue = "Select DCA";
                    Label2.Text = "";
                    CascadingDropDown3.SelectedValue = "Select Sub DCA";
                    Label3.Text = "";
                }
            }
            else
            {

                ddlvendortype.SelectedIndex = 0;
                JavaScript.UPAlert(Page, "Please Select CostCenter");
            }

        }
       
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        CascadingDropDown4.SelectedValue = "Select Other CC";
        ddlvendortype.SelectedIndex = 0;
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
        clear();
    }
    protected void ddlinvoiceno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlinvoiceno.SelectedItem.Text != "Invoice No")
        {
            da = new SqlDataAdapter("Select invoiceno,dca_code,subdca_code,name,description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,amount from GeneralInvoices where invoiceno='" + ddlinvoiceno.SelectedItem.Text + "' and status='3' and Mode_of_pay='Cash' ", con);
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
                da = new SqlDataAdapter("Select dca_name from dca where dca_code='" + ds.Tables["fill"].Rows[0].ItemArray[1].ToString() + "';Select subdca_name from subdca where subdca_code='" + ds.Tables["fill"].Rows[0].ItemArray[2].ToString() + "'", con);
                da.Fill(ds, "dcasdca");                
                lbldcacode.Text = ds.Tables["fill"].Rows[0].ItemArray[1].ToString();
                lbldcadesc.Text = ds.Tables["dcasdca"].Rows[0].ItemArray[0].ToString();
                lblsdcacode.Text = ds.Tables["fill"].Rows[0].ItemArray[2].ToString();
                lblsdcadesc.Text = ds.Tables["dcasdca1"].Rows[0].ItemArray[0].ToString();
                txtdt.Text = ds.Tables["fill"].Rows[0].ItemArray[5].ToString();
                txtname.Text = ds.Tables["fill"].Rows[0].ItemArray[3].ToString();
                txtdesc.Text = ds.Tables["fill"].Rows[0].ItemArray[4].ToString();
                txtamt.Text = ds.Tables["fill"].Rows[0].ItemArray[6].ToString().Replace(".0000","");
                txtdt.Enabled = false;
                txtamt.Enabled = false;
               
            }
            
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
    }

    public void clear()
    {
        ddltype.SelectedIndex = 0;
        if(ddlinvoiceno.Visible==false)
        ddlinvoiceno.SelectedIndex = 0;
        txtdt.Text = "";
        txtpaiddate.Text = "";
        //CascadingDropDown1.SelectedValue = "Select Vendor";
        //CascadingDropDown5.SelectedValue = "Select PO";
        if (ddlvendor.Visible == false)
        ddlvendor.SelectedIndex = 0;
        if (ddlvenpo.Visible == false)
        ddlvenpo.SelectedIndex = 0;
        CascadingDropDown2.SelectedValue = "Select DCA";
        Label2.Text = "";
        CascadingDropDown3.SelectedValue = "Select Sub DCA";
        Label3.Text = "";
        lbldcacode.Text = "";
        lbldcadesc.Text = "";
        lblsdcacode.Text = "";
        lblsdcadesc.Text = "";
        txtname.Text = "";
        txtdesc.Text = "";
        txtamt.Text = "";
    }
    //protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rbtntype.SelectedValue == "Paid Against")
    //    {
            
    //    }

    //}
    public string fillvendor(string type)
    {
        da = new SqlDataAdapter("Select vendor_id,(vendor_name+' , '+vendor_id)as name from vendor where vendor_type='" + type + "' and status='2' order by vendor_name", con);
        da.Fill(ds, "vendornameid");
        if (ds.Tables["vendornameid"].Rows.Count > 0)
        {
            ddlvendor.DataTextField = "name";
            ddlvendor.DataValueField = "vendor_id";
            ddlvendor.DataSource = ds.Tables["vendornameid"];
            ddlvendor.DataBind();
            ddlvendor.Items.Insert(0, "Select Vendor");
        }
        
        return type;
    }

    public string fillpo(string po)
    {
        if (rbtntype.SelectedValue == "Debit")
        {
            da = new SqlDataAdapter("Select distinct po_no,name from pending_invoice where (balance!='0' or retention_balance!='0' or hold_balance!='0')  and vendor_id='" + po + "' and cc_code='" + Session["cc_code"].ToString() + "'", con);
        }
        else if (rbtntype.SelectedValue == "Paid Against")
        {
            da = new SqlDataAdapter("Select distinct po_no,name from pending_invoice where (balance!='0' or retention_balance!='0' or hold_balance!='0')  and vendor_id='" + po + "' and cc_code='" + ddlcccode.SelectedValue + "'", con);
        }
        da.Fill(ds, "vendorpo");
        if (ds.Tables["vendorpo"].Rows.Count > 0)
        {
            ddlvenpo.DataTextField = "po_no";
            ddlvenpo.DataValueField = "po_no";
            ddlvenpo.DataSource = ds.Tables["vendorpo"];
            ddlvenpo.DataBind();
            ddlvenpo.Items.Insert(0, "Select PO");
            txtname.Text = ds.Tables["vendorpo"].Rows[0].ItemArray[1].ToString();
        }
        else
        {
            CascadingDropDown2.SelectedValue = "Select DCA";
            Label2.Text = "";
            CascadingDropDown3.SelectedValue = "Select Sub DCA";
            Label3.Text = "";
            JavaScript.UPAlert(Page, "No PO Avaliable For The Vendor");
        }
        return po;
    }

    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvendor.SelectedIndex != 0)
        {
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
            fillpo(ddlvendor.SelectedValue);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((ddlvendortype.SelectedItem.Text == "Service Provider") || (ddlvendortype.SelectedItem.Text == "Supplier"))
        {
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel2", "javascript:Display()", true);
            fillvendor(ddlvendortype.SelectedItem.Text);
        }
    }
}
    
    

