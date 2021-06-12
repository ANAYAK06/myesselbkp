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
using AjaxControlToolkit;

public partial class Admin_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    int no = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 44);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");

        if (!IsPostBack)
        {
            CC.Visible = false;
            dca.Visible = false;
            year.Visible = false;
            btn.Visible = false;
            LoadYear();
        }
           
       
    }
    public void LoadYear()
    {
        for (int i = 2005; i < System.DateTime.Now.Year+1; i++)
        {
            ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        paymenttype();
    }
    private void paymenttype()
    {


        if (rbtntype.SelectedIndex == 0)
        {

            if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Project Manager")
            {
           
                CC.Visible = true;
                dca.Visible = false;
                year.Visible = true;
                btn.Visible = true;
                Accordion1.Visible = true;
                Accordion2.Visible = false;
            }
            else if (Session["roles"].ToString() == "Accountant" || Session["roles"].ToString() == "StoreKeeper")
            {
                CC.Visible = false;
                dca.Visible = false;
                year.Visible = false;
                btn.Visible = false;
                Accordion1.Visible = true;
                Accordion2.Visible = false;
                JavaScript.Alert("You are not authorized");
               

            }
        }


        else if (rbtntype.SelectedIndex == 1)
        {
            if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Project Manager")
            {
              
                CC.Visible = true;
                dca.Visible = true;
                year.Visible = true;
                btn.Visible = true;
                Accordion1.Visible = false;
                Accordion2.Visible = true;
               
            }
            else if (Session["roles"].ToString() == "Accountant" || Session["roles"].ToString() == "StoreKeeper")
            {
                CC.Visible = false;
                dca.Visible = true;
                year.Visible = true;
                btn.Visible = true;
                Accordion1.Visible = false;
                Accordion2.Visible = true;
            }
        }
        else
        {

        }

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        getCCBudget();
    }
    public void getCCBudget()
    {
        try
        {

            if (rbtntype.SelectedIndex == 0)
            {
                if (ddlyear.SelectedValue != "" && ddlCCcode.SelectedValue != "")
                    da = new SqlDataAdapter("Select B.CC_Code,CC_Name,Year,Replace(budget_amount,'.0000','.00') Budget,Replace(B.Balance,'.0000','.00') Balance from Cost_Center c join Budget_cc B on c.CC_Code=B.CC_code where b.cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "'", con);
                else if (ddlyear.SelectedValue != "")
                    da = new SqlDataAdapter("Select B.CC_Code,CC_Name,Year,Replace(budget_amount,'.0000','.00') Budget,Replace(B.Balance,'.0000','.00') Balance from Cost_Center c join Budget_cc B on c.CC_Code=B.CC_code where year='" + ddlyear.SelectedItem.Text + "'", con);
                else
                    da = new SqlDataAdapter("Select B.CC_Code,CC_Name,Year,Replace(budget_amount,'.0000','.00') Budget,Replace(B.Balance,'.0000','.00') Balance from Cost_Center c join Budget_cc B on c.CC_Code=B.CC_code", con);
                da.Fill(ds, "normalview");
                Accordion1.DataSource = ds.Tables["normalview"].DefaultView;
                Accordion1.DataBind();
                Accordion2.Visible = false;
            }
            else if (rbtntype.SelectedIndex == 1)
            {
                if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Project Manager")
                    da = new SqlDataAdapter("Select B.CC_Code,CC_Name,Year,Replace(budget_dca_yearly,'.0000','.00') Budget,Replace(B.dca_yearly_bal,'.0000','.00') Balance from Cost_Center c join yearly_dcabudget B on c.CC_Code=B.CC_code where b.cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "' and b.dca_code='" + ddldetailhead.SelectedValue + "'", con);
                else
                    da = new SqlDataAdapter("Select B.CC_Code,CC_Name,Year,Replace(budget_dca_yearly,'.0000','.00') Budget,Replace(B.dca_yearly_bal,'.0000','.00') Balance from Cost_Center c join yearly_dcabudget B on c.CC_Code=B.CC_code where b.cc_code='" + Session["cc_code"].ToString() + "' and year='" + ddlyear.SelectedItem.Text + "' and b.dca_code='" + ddldetailhead.SelectedValue + "'", con);
                da.Fill(ds, "detailview");
                Accordion2.DataSource = ds.Tables["detailview"].DefaultView;
                Accordion2.DataBind();
                Accordion1.Visible = false;
               
            }
          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void Accordion1_ItemDataBound(object sender, AjaxControlToolkit.AccordionItemEventArgs e)
    {
        if (e.ItemType == AjaxControlToolkit.AccordionItemType.Header)
        {
            Label lblbal = ((Label)e.AccordionItem.FindControl("lblbal"));
            Label lblprev = ((Label)e.AccordionItem.FindControl("Label3"));


            lblbal.Text = "Unassigned Budget";
            da = new SqlDataAdapter("Select (isnull(Sum(budget_dca_yearly),0)-isnull(Sum(dca_yearly_bal),0)) as[Prev] from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "' and year!='" + ddlyear.SelectedItem.Text + "' and year<'" + ddlyear.SelectedItem.Text + "'", con);
            da.Fill(ds, "prev");
            if (ds.Tables["prev"].Rows.Count > 0)
            {
                lblprev.Text = ds.Tables["prev"].Rows[0].ItemArray[0].ToString();
            }
        }
          
        if (e.ItemType == AjaxControlToolkit.AccordionItemType.Content)
        {
            try
            {
              
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
               
                sqlAdapter = new SqlDataAdapter("select d.Dca_Name as[DCA Name],rec.* from (select (case when c.DCA_code is  null then ch.DCA_code else c.DCA_code end) [DCACode],Replace(isnull(Previousconsumed,0),'.0000','.00')[Prevoius Consumed],Replace(isnull(budget_dca_yearly,0),'.0000','.00')[Current Year Budget],Replace(isnull(budget_dca_yearly,0)-isnull(dca_yearly_bal,0),'.0000','.00')[Current Year Consumed Budget],Replace(isnull(dca_yearly_bal,0),'.0000','.00')[Current Year Balance] from (Select DCA_code,(isnull(sum(budget_dca_yearly),0)-isnull(sum(dca_yearly_bal),0))Previousconsumed from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "' and year!='" + ddlyear.SelectedItem.Text + "' and year<'" + ddlyear.SelectedItem.Text + "' group by dca_code) c full outer join (Select DCA_code,isnull(budget_dca_yearly,0)[budget_dca_yearly],isnull(dca_yearly_bal,0)[dca_yearly_bal] from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "') ch on c.dca_code=ch.dca_code)rec join dca d on rec.[DCACode]=d.dca_code order by [DCACode]", con);
                DataSet myDataset = new DataSet();
                sqlAdapter.Fill(myDataset);
                GridView grd = (GridView)e.AccordionItem.FindControl("GridView2");
                grd.DataSource = myDataset;
                grd.DataBind();
            }

            catch (Exception ex)
            {
                Utilities.CatchException(ex);
            }
        }
    }
    protected void Accordion2_ItemDataBound(object sender, AjaxControlToolkit.AccordionItemEventArgs e)
    {
        if (e.ItemType == AjaxControlToolkit.AccordionItemType.Header)
        {
            Label lblbal = ((Label)e.AccordionItem.FindControl("lblbal"));
            Label lblprev = ((Label)e.AccordionItem.FindControl("Label3"));


            lblbal.Text = "Balance Budget";
            if (Session["roles"].ToString() == "Accountant" || Session["roles"].ToString() == "StoreKeeper")
            {
                da = new SqlDataAdapter("Select (isnull(Sum(budget_dca_yearly),0)-isnull(Sum(dca_yearly_bal),0)) as[Prev] from yearly_dcabudget where cc_code='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedValue + "' and year!='" + ddlyear.SelectedItem.Text + "' and year<'" + ddlyear.SelectedItem.Text + "'", con);
            }
            else
            {
                da = new SqlDataAdapter("Select (isnull(Sum(budget_dca_yearly),0)-isnull(Sum(dca_yearly_bal),0)) as[Prev] from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "'and dca_code='" + ddldetailhead.SelectedValue + "' and year!='" + ddlyear.SelectedItem.Text + "' and year<'" + ddlyear.SelectedItem.Text + "'", con);

            }
            da.Fill(ds, "prevd");
            if (ds.Tables["prevd"].Rows.Count > 0)
            {
                lblprev.Text = ds.Tables["prevd"].Rows[0].ItemArray[0].ToString();

            }
        }
               
           

        
        if (e.ItemType == AjaxControlToolkit.AccordionItemType.Content)
        {
            try
            {

                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Project Manager")
                {
                    if (ddlyear.SelectedValue != "" && ddldetailhead.SelectedItem.Text == "DCA-11")
                        sqlAdapter = new SqlDataAdapter("Select REPLACE(CONVERT(VARCHAR(11),i.date, 106), ' ', '-')Date,i.particulars as[Description],Replace(i.Credit,'.0000','.00')Credit,Replace(i.Debit,'.0000','.00')Debit from (Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_pending where cc_code='" + ddlCCcode.SelectedValue + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and  convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and paid_against is null Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_pending where paid_against='" + ddlCCcode.SelectedValue + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_book where cc_code='" + ddlCCcode.SelectedValue + "' and  dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and paid_against is null Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_book where paid_against='" + ddlCCcode.SelectedValue + "' and  dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select Date,description,isnull(credit,0)credit,isnull(debit,0)debit from bankbook where cc_code='" + ddlCCcode.SelectedValue + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and (paymenttype='General' or paymenttype='Salary') Union All Select Invoice_date,description,isnull(null,0),isnull(basicvalue,0)basicvalue from pending_invoice where cc_code='" + ddlCCcode.SelectedValue + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and (status='Debited' or status='Debit Pending') Union All Select indentraise_date,remarks,isnull(null,0),isnull((Select Sum(isnull(Amount,0)) from indent_list where indent_no=i.indent_no),0)Amount from indents i where  cc_code='" + ddlCCcode.SelectedValue + "' and  convert(datetime,indentraise_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select transfer_date,remarks,isnull((Select Sum(isnull(Amount,0)) from [items transfer] where ref_no=T.ref_no),0)Amount,isnull(null,0) from [Transfer Info]T where  cc_code='" + ddlCCcode.SelectedValue + "'and convert(datetime,transfer_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select transfer_date,remarks,isnull(null,0),isnull((Select Sum(isnull(Amount,0)) from [items transfer] where ref_no=T.ref_no),0)Amount from [Transfer Info]T where  recieved_cc='" + ddlCCcode.SelectedValue + "'and convert(datetime,transfer_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "')i order by date desc", con);
                    else
                        sqlAdapter = new SqlDataAdapter("Select REPLACE(CONVERT(VARCHAR(11),i.date, 106), ' ', '-')Date,i.particulars as[Description],Replace(i.Credit,'.0000','.00')Credit,Replace(i.Debit,'.0000','.00')Debit from (Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_pending where cc_code='" + ddlCCcode.SelectedValue + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and  convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and paid_against is null Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_pending where paid_against='" + ddlCCcode.SelectedValue + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_book where cc_code='" + ddlCCcode.SelectedValue + "' and  dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and paid_against is null Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_book where paid_against='" + ddlCCcode.SelectedValue + "' and  dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select Date,description,isnull(credit,0)credit,isnull(debit,0)debit from bankbook where cc_code='" + ddlCCcode.SelectedValue + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and (paymenttype='General' or paymenttype='Salary') Union All Select Invoice_date,description,isnull(null,0),isnull(basicvalue,0)basicvalue from pending_invoice where cc_code='" + ddlCCcode.SelectedValue + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and (status='Debited' or status='Debit Pending'))i order by date desc", con);


                }
                else if (Session["roles"].ToString() == "Accountant" || Session["roles"].ToString() == "StoreKeeper")
                {
                    if (ddldetailhead.SelectedItem.Text == "DCA-11")
                        sqlAdapter = new SqlDataAdapter("Select REPLACE(CONVERT(VARCHAR(11),i.date, 106), ' ', '-')Date,i.particulars as[Description],Replace(i.Credit,'.0000','.00')Credit,Replace(i.Debit,'.0000','.00')Debit from (Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_pending where cc_code='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and  convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and paid_against is null Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_pending where paid_against='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_book where cc_code='" + Session["cc_code"].ToString() + "' and  dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and paid_against is null Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_book where paid_against='" + Session["cc_code"].ToString() + "' and  dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select Date,description,isnull(credit,0)credit,isnull(debit,0)debit from bankbook where cc_code='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and (paymenttype='General' or paymenttype='Salary') Union All Select Invoice_date,description,isnull(null,0),isnull(basicvalue,0)basicvalue from pending_invoice where cc_code='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and (status='Debited' or status='Debit Pending') Union All Select indentraise_date,remarks,isnull(null,0),isnull((Select Sum(isnull(Amount,0)) from indent_list where indent_no=i.indent_no),0)Amount from indents i where  cc_code='" + Session["cc_code"].ToString() + "' and  convert(datetime,indentraise_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select transfer_date,remarks,isnull((Select Sum(isnull(Amount,0)) from [items transfer] where ref_no=T.ref_no),0)Amount,isnull(null,0) from [Transfer Info]T where  cc_code='" + Session["cc_code"].ToString() + "'and convert(datetime,transfer_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select transfer_date,remarks,isnull(null,0),isnull((Select Sum(isnull(Amount,0)) from [items transfer] where ref_no=T.ref_no),0)Amount from [Transfer Info]T where  recieved_cc='" + Session["cc_code"].ToString() + "'and convert(datetime,transfer_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "')i order by date desc", con);
                    else
                        sqlAdapter = new SqlDataAdapter("Select REPLACE(CONVERT(VARCHAR(11),i.date, 106), ' ', '-')Date,i.particulars as[Description],Replace(i.Credit,'.0000','.00')Credit,Replace(i.Debit,'.0000','.00')Debit from (Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_pending where cc_code='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and  convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and paid_against is null Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_pending where paid_against='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_book where cc_code='" + Session["cc_code"].ToString() + "' and  dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and paid_against is null Union All Select Date,particulars,isnull(credit,0)credit,isnull(debit,0)debit from cash_book where paid_against='" + Session["cc_code"].ToString() + "' and  dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' Union All Select Date,description,isnull(credit,0)credit,isnull(debit,0)debit from bankbook where cc_code='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and (paymenttype='General' or paymenttype='Salary') Union All Select Invoice_date,description,isnull(null,0),isnull(basicvalue,0)basicvalue from pending_invoice where cc_code='" + Session["cc_code"].ToString() + "' and dca_code='" + ddldetailhead.SelectedItem.Text + "' and convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "' and (status='Debited' or status='Debit Pending'))i order by date desc", con);

                }


                DataSet myDataset = new DataSet();
                sqlAdapter.Fill(myDataset);
                GridView grd = (GridView)e.AccordionItem.FindControl("GridView1");
                grd.DataSource = myDataset;
                grd.DataBind();
            }

            catch (Exception ex)
            {
                Utilities.CatchException(ex);
            }
        }
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;

            string behaviorID = "pce_" + (no++).ToString() + e.Row.RowIndex;
            pce.BehaviorID = behaviorID;

            Image img = (Image)e.Row.FindControl("Image1");

            string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
            string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

            img.Attributes.Add("onmouseover", OnMouseOverScript);
            img.Attributes.Add("onmouseout", OnMouseOutScript);
        }
        

    }


//select distinct(case when s.date is not null then s.date else pd.date end) date,Cash,Cheque,CashPending,ChequePending from 
//(select (case when c.date is null then ch.date else c.date end) date,cash,cheque,(isnull(cash,0)+isnull(cheque,0)) paid from 
//(Select date,isnull(sum(debit),0) cash from cash_book where convert(datetime,date) between '04/01/2011' and '03/31/2012' group by date ) c full outer join 
//(Select date,isnull(sum(debit),0) cheque from bankbook  where convert(datetime,date) between '04/01/2011' and '03/31/2012' and invoiceno is null and paymenttype!='Withdraw'and paymenttype!='Transfer' group by date) ch on c.date=ch.date) s full outer join 
//(Select (case when cp.date is null then chp.invoice_date else cp.date end)date,CashPending,ChequePending from 
//(Select date,isnull(sum(Debit),0) CashPending from cash_pending where status='Pending' and convert(datetime,date) between '04/01/2011' and '03/31/2012' Group by date)cp full outer join 
//(Select invoice_date,isnull(sum(Basicvalue),0) ChequePending from pending_invoice where convert(datetime,invoice_date) between '04/01/2011' and '03/31/2012' group by invoice_date) chp on cp.date=chp.invoice_date) pd on s.date=pd.date order by date asc

//    select distinct(case when s.date is not null then s.date else pd.date end) date,(isnull(cash,0)+isnull(cheque,0)+isnull(cashpending,0)+isnull(chequepending,0))[Amount] from 
//(select (case when c.date is null then ch.date else c.date end) date,cash,cheque,(isnull(cash,0)+isnull(cheque,0)) paid from 
//(Select date,isnull(sum(debit),0) cash from cash_book where convert(datetime,date) between '04/01/2011' and '03/31/2012' group by date ) c full outer join 
//(Select date,isnull(sum(debit),0) cheque from bankbook  where convert(datetime,date) between '04/01/2011' and '03/31/2012' and invoiceno is null and paymenttype!='Withdraw'and paymenttype!='Transfer' group by date) ch on c.date=ch.date) s full outer join 
//(Select (case when cp.date is null then chp.invoice_date else cp.date end)date,CashPending,ChequePending from 
//(Select date,isnull(sum(Debit),0) CashPending from cash_pending where status='Pending' and convert(datetime,date) between '04/01/2011' and '03/31/2012' Group by date)cp full outer join 
//(Select invoice_date,isnull(sum(Basicvalue),0) ChequePending from pending_invoice where convert(datetime,invoice_date) between '04/01/2011' and '03/31/2012' group by invoice_date) chp on cp.date=chp.invoice_date) pd on s.date=pd.date order by date asc

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            previous += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Prevoius Consumed"));
            Budget += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Current Year Budget"));
            Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Current Year Balance"));
            currentconsumed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Current Year Consumed Budget"));
        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = String.Format("{0:#,##,##,###.00}", previous);
            e.Row.Cells[3].Text = String.Format("{0:#,##,##,###.00}", Budget);
            e.Row.Cells[5].Text = String.Format("{0:#,##,##,###.00}", Balance);
            e.Row.Cells[4].Text = String.Format("{0:#,##,##,###.00}", currentconsumed);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            credit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "credit"));
            debit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = String.Format("Rs. {0:#,##,##,###.00}", credit);
            e.Row.Cells[3].Text = String.Format("Rs. {0:#,##,##,###.00}", debit);
        }
    }
    
    private decimal Budget = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
    private decimal credit = (decimal)0.0;
    private decimal debit = (decimal)0.0;
    private decimal previous = (decimal)0.0;
    private decimal currentconsumed = (decimal)0.0;
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
//  Select Date,particulars,credit,debit from cash_pending where cc_code='CC-42' and dca_code='DCA-11' and  convert(datetime,date) between '04/01/2011' and '03/31/2012' and paid_against is null 
//Union All 
//Select Date,particulars,credit,debit from cash_pending where paid_against='CC-42' and dca_code='DCA-11' and  convert(datetime,date) between '04/01/2011' and '03/31/2012' 
//Union All 
//Select Date,particulars,credit,debit from cash_book where cc_code='CC-42' and  dca_code='DCA-11' and convert(datetime,date) between '04/01/2011' and '03/31/2012' and paid_against is null 
//Union All 
//Select Date,particulars,credit,debit from cash_book where paid_against='CC-42' and  dca_code='DCA-11' and convert(datetime,date) between '04/01/2011' and '03/31/2012' 
//Union All 
//Select Date,description,credit,debit from bankbook where cc_code='CC-42' and dca_code='DCA-11' and (paymenttype='General' or paymenttype='Salary') 
//Union All 
//Select Invoice_date,description,null,basicvalue from pending_invoice where cc_code='CC-42' and dca_code='DCA-11' and convert(datetime,invoice_date) between '04/01/2011' and '03/31/2012' and (status='Debited' or status='Debit Pending') 
//Union All 
//Select indentraise_date,remarks,null,(Select Sum(isnull(Amount,0)) from indent_list where indent_no=i.indent_no)Amount from indents i where  cc_code='CC-42' and convert(datetime,indentraise_date) between '04/01/2011' and '03/31/2012' 
//Union All 
//Select transfer_date,remarks,(Select Sum(isnull(Amount,0)) from [items transfer] where ref_no=T.ref_no)Amount,null from [Transfer Info]T where cc_code='CC-42'  and convert(datetime,transfer_date) between '04/01/2011' and '03/31/2012'
//Union All 
//Select transfer_date,remarks,null,(Select Sum(isnull(Amount,0)) from [items transfer] where ref_no=T.ref_no)Amount from [Transfer Info]T where recieved_cc='CC-42'  and convert(datetime,transfer_date) between '04/01/2011' and '03/31/2012'

}
