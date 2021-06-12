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




public partial class Admin_frmViewBankFlow : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    decimal grdTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       //esselDal RoleCheck = new esselDal();
       // int rec = 0;
       //  rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 46);
       // if (rec == 0)
       //     Response.Redirect("Menucontents.aspx");

        if (!IsPostBack)
        {
            trpaytype.Visible = false;
            trcctype.Visible = false;
            cc.Visible = false;
            Dca.Visible = false;
            PO.Visible = false;
          
            LoadYear();
        }
    }
   
    private void FillGrid()
    {
        try
        {
            da = new SqlDataAdapter(ViewState["search"].ToString(), con);
            da.Fill(ds, "bank");
            GridView1.DataSource = ds.Tables["bank"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
       
        
    }

    protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void ddltypeofpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcctype.SelectedIndex = 0;
        ddltype.SelectedIndex = 0;
        Clear();
        subtype();
        subpayment();
    }

    protected void ddlsubtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadpo();
        Clear();
        ddlcctype.SelectedIndex = 0;
        ddltype.SelectedIndex = 0;
        if (ddlsubtype.SelectedItem.Text == "Service Provider" || ddlsubtype.SelectedItem.Text == "Supplier" || ddlsubtype.SelectedItem.Text == "General" || ddlsubtype.SelectedItem.Text == "All Debits")
        {
            trcctype.Visible = true;
            cc.Visible = false;
            lblpono.Visible = false;
            ddlpo.Visible = false;
            ddlyear.Visible = false;
            ddlmonth.Visible = false;
            PO.Visible = false;
            ddlpo.Items.Clear();
            //Dca.Visible = true;

        }
        else
        {
            ddlpo.Items.Clear();
            trcctype.Visible = false;
            cc.Visible = false;
            Dca.Visible = false;
            lblpono.Visible = false;
            ddlpo.Visible = false;
            ddlyear.Visible = true;
            ddlmonth.Visible = true;
            PO.Visible = true;
        }
    }

    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        paymenttype();
    }

    private void paymenttype()
    {
       
        Clear();
        if (rbtntype.SelectedIndex == 0)
        {
            cc.Visible = false;
            PO.Visible = false;
            Dca.Visible = false;
            trcctype.Visible = false;
            ddltypeofpay.Items.Clear();
            ddlsubtype.Visible = false;
            paytype.Visible = true;
            ddlsub.Visible = false;
            ddlsub.Visible = false;
            //ddlpo.Visible = false;           
            trpaytype.Visible = true;
            ddltypeofpay.Items.Add("-Select-");
            ddltypeofpay.Items.Add("Select All Credits");
            ddltypeofpay.Items.Add("All Pending Bills");
        }
        else if (rbtntype.SelectedIndex == 1)
        {
            cc.Visible = false;
            PO.Visible = false;
            Dca.Visible = false;
            trcctype.Visible = false;
            ddltypeofpay.Items.Clear();
            ddlsubtype.Visible = false;
            paytype.Visible = true;
            ddlsub.Visible = false;
            trpaytype.Visible = true;
            ddlsub.Visible = false;
            ddltypeofpay.Items.Add("-Select-");
            ddltypeofpay.Items.Add("Trade purchasing");
            ddltypeofpay.Items.Add("Service");
        }
        else
        {
            //PO.Visible = true;
            //paytype.Visible = false;            
            //trpaytype.Visible = false;
            //trcctype.Visible = false;
            //cc.Visible = false;
            lblpono.Visible = false;
            ddlpo.Visible = false;
            ddlyear.Visible = true;
            ddlmonth.Visible = true;
            trpaytype.Visible = false;
            trcctype.Visible = false;
            cc.Visible = false;
            Dca.Visible = false;
            PO.Visible = true;
        }
    }

    private void subtype()
    {

        ddlsubtype.Visible = false;
        //ddlpono.Visible = false;
        Clear();
        //if (ddltypeofpay.SelectedItem.Text == "Invoice Service" || ddltypeofpay.SelectedItem.Text == "Retention" || ddltypeofpay.SelectedItem.Text == "Advance" || ddltypeofpay.SelectedItem.Text == "Trading Supply" || ddltypeofpay.SelectedItem.Text == "Trade Purchasing" || ddltypeofpay.SelectedItem.Text=="All Pending Bills")
        //{
        //    cc.Visible = true;
        //    Dca.Visible = false;
        //    ddlpo.Visible = true;
        //}
        //else
        //{
        //    cc.Visible = true;
        //    Dca.Visible = true;
        //    ddlsub.Visible = false;
        //}

        if (ddltypeofpay.SelectedIndex != 0)
        {
            ddlsubtype.Items.Clear();
            ddlsub.Visible = false;
            if (ddltypeofpay.SelectedItem.Text == "Service")
            {
                ddlsub.Visible = false;
                ddlsubtype.Visible = true;
                ddlsubtype.Items.Add("-Select Service Type-");
                ddlsubtype.Items.Add("All Debits");
                ddlsubtype.Items.Add("General");
                ddlsubtype.Items.Add("Withdraw");
                ddlsubtype.ToolTip = "Service Type";
                PO.Visible = false;
            }
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Filter();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void Filter()
    {
        string Condition = "";
        string Condition1 = "";
        string Condition2 = "";
        if (rbtntype.SelectedIndex == 0)
        {
            if (ddltypeofpay.SelectedIndex == 1)
            {
                if (ddlsub.SelectedIndex == 1)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],[RA_NO],i.[CC_Code],CAST(BasicValue AS Decimal(25,2))as [BasicValue],CAST(NetServiceTax AS  Decimal(25,2))as [servicetax],Cast(Netsalestax as decimal(15,2))as [salestax],cast(NetExciseDuty as  Decimal(25,2)) as [Exciseduty],cast(Netfrieght as  Decimal(25,2))as[Frieght],cast(NetInsurance as  Decimal(25,2)) as [Insurance],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Advance AS  Decimal(25,2))as[Advance],CAST(hold AS  Decimal(25,2))as[hold],CAST(Anyother AS  Decimal(25,2))as[Anyother],CAST(Amount AS  Decimal(25,2))[Amount]  from bankbook b join invoice i on (b.invoiceno=i.invoiceno or b.transaction_no=i.transaction_no) where i.status='credit' and b.paymenttype in ('Invoice Service','Trading Supply','Advance','Retention','Hold','Manufacturing')";
                }
                else if (ddlsub.SelectedIndex == 2)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],[RA_NO],i.[CC_Code],CAST(BasicValue AS Decimal(25,2))as [BasicValue],CAST(NetServiceTax AS  Decimal(25,2))as [servicetax],Cast(Netsalestax as decimal(15,2))as [salestax],cast(NetExciseDuty as  Decimal(25,2)) as [Exciseduty],cast(Netfrieght as  Decimal(25,2))as[Frieght],cast(NetInsurance as  Decimal(25,2)) as [Insurance],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Advance AS  Decimal(25,2))as[Advance],CAST(hold AS  Decimal(25,2))as[hold],CAST(Anyother AS  Decimal(25,2))as[Anyother],CAST(Amount AS  Decimal(25,2))[Amount]  from bankbook b join invoice i on (b.invoiceno=i.invoiceno or b.transaction_no=i.transaction_no) where i.status='credit' and b.paymenttype in ('Invoice Service','Trading Supply','Advance')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS Decimal(25,2))[TotalBasic],cast(sum(NetServiceTax) as decimal(25,2))[Total ServiceTax],cast(sum(NetSalestax) as decimal(25,2)) as [Sales Tax],cast(sum(NetExciseDuty) as  Decimal(25,2))as[Total Excixeduty],cast(sum(NetEDcess)as decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as decimal(25,2))[Total Hedcess],cast(sum(tds)as decimal(25,2))[Total TDS],CAST(sum(retention) AS Decimal(25,2))as[retention],CAST(sum(Advance) AS Decimal(25,2))as[Advance],CAST(sum(hold) AS Decimal(25,2))as[Hold],CAST(sum(Anyother) AS Decimal(25,2))as[Anyother],cast(sum(amount) as decimal(25,2))[Total Net Amount]  from bankbook b join invoice i on (b.invoiceno=i.invoiceno or b.transaction_no=i.transaction_no) where  i.status='credit' and b.paymenttype in ('Invoice Service','Trading Supply','Advance')";

                }
                else if (ddlsub.SelectedIndex == 3)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],i.[CC_Code],CAST(BasicValue AS Decimal(25,2))[BasicValue],cast(NetExciseDuty as  Decimal(25,2)) as [Exciseduty],CAST(NetServiceTax AS  Decimal(25,2))as [servicetax],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Advance AS  Decimal(25,2))as[Advance],CAST(hold AS  Decimal(25,2))as[hold],CAST(Anyother AS  Decimal(25,2))as[Anyother],cast(amount as  Decimal(25,2))as [Net Amount],[bank_name][Bank],REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],i.[Status] from bankbook b join invoice i on b.invoiceno=i.invoiceno where i.status='credit' and invoicetype in ('Service Tax Invoice')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(NetExciseDuty) as decimal(25,2))as[Total Excixeduty],cast(sum(NetServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(NetEDcess)as  Decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as  Decimal(25,2))[Total Hedcess],cast(sum(tds)as  Decimal(25,2))[Total TDS],CAST(sum(retention) AS  Decimal(25,2))as[retention],CAST(sum(Advance) AS Decimal(25,2))as[Advance],CAST(sum(hold) AS Decimal(25,2))as[Hold],CAST(sum(Anyother) AS Decimal(25,2))as[Anyother],cast(sum(amount) as  Decimal(25,2))[Total Amount] from bankbook b join invoice i on b.invoiceno=i.invoiceno where  i.status='credit' and invoicetype in ('Service Tax Invoice')";
                }
                else if (ddlsub.SelectedIndex == 4)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],i.[CC_Code],CAST(BasicValue AS Decimal(25,2))[BasicValue],cast(NetExciseDuty as  Decimal(25,2)) as [Exciseduty],CAST(NetServiceTax AS  Decimal(25,2))as [servicetax],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Advance AS  Decimal(25,2))as[Advance],CAST(hold AS  Decimal(25,2))as[hold],CAST(Anyother AS  Decimal(25,2))as[Anyother],cast(amount as  Decimal(25,2))as [Net Amount],[bank_name][Bank],REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],i.[Status] from bankbook b join invoice i on b.invoiceno=i.invoiceno where  i.status='credit' and invoicetype in ('SEZ/Service Tax exumpted Invoice')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(NetExciseDuty) as decimal(25,2))as[Total Excixeduty],cast(sum(NetServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(NetEDcess)as  Decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as  Decimal(25,2))[Total Hedcess],cast(sum(tds)as  Decimal(25,2))[Total TDS],CAST(sum(retention) AS  Decimal(25,2))as[retention],CAST(sum(Advance) AS Decimal(25,2))as[Advance],CAST(sum(hold) AS Decimal(25,2))as[Hold],CAST(sum(Anyother) AS Decimal(25,2))as[Anyother],cast(sum(amount) as  Decimal(25,2))[Total Amount] from bankbook b join invoice i on b.invoiceno=i.invoiceno where i.status='credit' and invoicetype in ('SEZ/Service Tax exumpted Invoice')";
                }
                else if (ddlsub.SelectedIndex == 5)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],[RA_NO],i.[CC_Code],CAST(BasicValue AS Decimal(25,2))as [BasicValue],Cast(Netsalestax as decimal(15,2))as [salestax],cast(NetExciseDuty as  Decimal(25,2)) as [Exciseduty],cast(Netfrieght as  Decimal(25,2))as[Frieght],cast(NetInsurance as  Decimal(25,2)) as [Insurance],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Amount AS  Decimal(25,2))[Amount]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where b.paymenttype in ('Trading Supply') and i.status='credit'";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(NetSalestax) as decimal(25,2)) as [Sales Tax],cast(sum(NetEDcess)as  Decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as  Decimal(25,2))[Total Hedcess],cast(sum(Netfrieght) as  Decimal(25,2)) as[Total Freight],cast(sum(NetExciseDuty) as  Decimal(25,2))as[Total Excixeduty], cast(sum(tds)as  Decimal(25,2))[Total TDS],CAST(sum(retention) AS  Decimal(25,2))as[retention]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where b.paymenttype in ('Trading Supply') and i.status='credit'";
                }
                else if (ddlsub.SelectedIndex == 6)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],[RA_NO],i.[CC_Code],CAST(BasicValue AS Decimal(25,2))as [BasicValue],Cast(Netsalestax as decimal(15,2))as [salestax],cast(NetExciseDuty as  Decimal(25,2)) as [Exciseduty],cast(Netfrieght as  Decimal(25,2))as[Frieght],cast(NetInsurance as  Decimal(25,2)) as [Insurance],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Amount AS  Decimal(25,2))[Amount]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where b.paymenttype in ('Manufacturing') and i.status='credit'";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(NetSalestax) as decimal(25,2)) as [Sales Tax],cast(sum(NetEDcess)as  Decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as  Decimal(25,2))[Total Hedcess],cast(sum(Netfrieght) as  Decimal(25,2)) as[Total Freight],cast(sum(NetExciseDuty) as  Decimal(25,2))as[Total Excixeduty], cast(sum(tds)as  Decimal(25,2))[Total TDS],CAST(sum(retention) AS  Decimal(25,2))as[retention]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where b.paymenttype in ('Manufacturing') and i.status='credit'";
                }
                else if (ddlsub.SelectedIndex == 7)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],i.[CC_Code],CAST(BasicValue AS Decimal(25,2))[BasicValue],CAST(NetServiceTax AS  Decimal(25,2))as [servicetax],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Advance AS  Decimal(25,2))as[Advance],CAST(hold AS  Decimal(25,2))as[hold],CAST(Anyother AS  Decimal(25,2))as[Anyother],cast(amount as  Decimal(25,2))as [Net Amount],[bank_name][Bank],REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],i.[Status] from bankbook b join invoice i on (b.invoiceno=i.invoiceno or b.transaction_no=i.transaction_no) where b.paymenttype in ('Advance') and i.status='credit'";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(NetServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(NetEDcess)as  Decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as  Decimal(25,2))[Total Hedcess],cast(sum(tds)as  Decimal(25,2))[Total TDS],CAST(sum(retention) AS  Decimal(25,2))as[retention],CAST(sum(Advance) AS Decimal(25,2))as[Advance],CAST(sum(hold) AS Decimal(25,2))as[Hold],CAST(sum(Anyother) AS Decimal(25,2))as[Anyother],cast(sum(amount) as  Decimal(25,2))[Total Amount] from bankbook b join invoice i on (b.invoiceno=i.invoiceno or b.transaction_no=i.transaction_no) where b.paymenttype in ('Advance') and i.status='credit'";
                }
                else if (ddlsub.SelectedIndex == 8)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],i.[CC_Code],CAST(BasicValue AS  Decimal(25,2))[BasicValue],CAST(NetServiceTax AS  Decimal(25,2))as [servicetax],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Advance AS  Decimal(25,2))as[Advance],CAST(hold AS  Decimal(25,2))as[hold],CAST(Anyother AS  Decimal(25,2))as[Anyother],cast(amount as  Decimal(25,2))as [Net Amount],[bank_name][Bank],REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],i.[Status] from bankbook b join invoice i on b.invoiceno=i.invoiceno where b.paymenttype in ('Retention')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(NetServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(NetEDcess)as  Decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as  Decimal(25,2))[Total Hedcess],cast(sum(tds)as  Decimal(25,2))[Total TDS],CAST(sum(retention) AS  Decimal(25,2))as[retention],CAST(sum(Advance) AS Decimal(25,2))as[Advance],CAST(sum(hold) AS Decimal(25,2))as[Hold],CAST(sum(Anyother) AS Decimal(25,2))as[Anyother],cast(sum(amount) as  Decimal(25,2))[Total Amount] from bankbook b join invoice i on b.invoiceno=i.invoiceno where b.paymenttype in ('Retention')";

                }
                else if (ddlsub.SelectedIndex == 9)
                {
                    Condition = Condition + "Select i.[InvoiceNo],i.[PO_NO],[Invoice_Date],i.[CC_Code],CAST(BasicValue AS  Decimal(25,2))[BasicValue],CAST(NetServiceTax AS  Decimal(25,2))as [servicetax],CAST(NetEdcess AS  Decimal(25,2))as[Edcess],CAST(NetHedcess AS  Decimal(25,2))as[Hedcess],CAST(tds AS  Decimal(25,2))as [tds] ,CAST(retention AS  Decimal(25,2))as[retention],CAST(Advance AS  Decimal(25,2))as[Advance],CAST(hold AS  Decimal(25,2))as[hold],CAST(Anyother AS  Decimal(25,2))as[Anyother],cast(amount as  Decimal(25,2))as [Net Amount],[bank_name][Bank],REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],i.[Status] from bankbook b join invoice i on b.invoiceno=i.invoiceno where b.paymenttype in ('Hold')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(NetServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(NetEDcess)as  Decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as  Decimal(25,2))[Total Hedcess],cast(sum(tds)as  Decimal(25,2))[Total TDS],CAST(sum(retention) AS  Decimal(25,2))as[retention],CAST(sum(Advance) AS Decimal(25,2))as[Advance],CAST(sum(hold) AS Decimal(25,2))as[Hold],CAST(sum(Anyother) AS Decimal(25,2))as[Anyother],cast(sum(amount) as  Decimal(25,2))[Total Amount] from bankbook b join invoice i on b.invoiceno=i.invoiceno where b.paymenttype in ('Hold')";

                }
                else if (ddlsub.SelectedIndex == 10)
                {
                    if (ddlsubtype.SelectedIndex == 1)
                    {
                        Condition = Condition + "Select REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],[Bank_Name],[ModeOfPay],[No],[FDR],CAST(Principle AS  Decimal(25,2))[Principle],CAST(Interest AS  Decimal(25,2))[Interest],CAST(Credit AS  Decimal(25,2))[Amount]  from bankbook i where i.paymenttype='FD'";

                    }
                    else if (ddlsubtype.SelectedIndex == 2)
                    {
                        Condition = Condition + "Select REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],[CC_Code],[Sub_DCA],Name,[Description],[Bank_Name],[ModeOfPay],[No],CAST(Credit AS  Decimal(25,2))[Amount] from bankbook i where i.paymenttype='SD'";
                    }
                }
                else if (ddlsub.SelectedIndex == 11)
                {
                    Condition = Condition + "Select REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],[Bank_Name],[Description],[ModeOfPay],[No],CAST(Credit AS  Decimal(25,2))[Amount]  from bankbook i where i.paymenttype='Unsecured Loan' and i.credit is not null";
                    Condition1 = Condition1 + "Select CAST(Sum(Credit) AS  Decimal(25,2))[Amount]from bankbook i where i.paymenttype='Unsecured Loan' and i.credit is not null";

                }
            }
            else if (ddltypeofpay.SelectedIndex == 2)
            {
                if (ddlsub.SelectedIndex == 1)
                {
                    Condition = Condition + "Select [InvoiceNo],[PO_NO],[Invoice_Date],[CC_Code],CAST(BasicValue AS  Decimal(25,2))[BasicValue],CAST(ServiceTax AS  Decimal(25,2))as [servicetax],CAST(Edcess AS  Decimal(25,2))as[Edcess],CAST(Hedcess AS  Decimal(25,2))as[Hedcess],cast(Total as  Decimal(25,2)) as [Total] from invoice i where status in ('1','2')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(ServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(EDcess)as  Decimal(25,2))[Edcess],cast(sum(Hedcess)as  Decimal(25,2))[Total Hedcess],cast(Sum(Total) as  Decimal(25,2)) as [Total Amount] from invoice i Where status in ('1','2')";
                }
                else if (ddlsub.SelectedIndex == 2)
                {
                    Condition = Condition + "Select [InvoiceNo],[PO_NO],[Invoice_Date],[CC_Code],CAST(BasicValue AS  Decimal(25,2))[BasicValue],CAST(ServiceTax AS  Decimal(25,2))as [servicetax],CAST(Edcess AS  Decimal(25,2))as[Edcess],CAST(Hedcess AS  Decimal(25,2))as[Hedcess],cast(Total as  Decimal(25,2)) as [Total] from invoice i where status in ('1','2') and invoicetype in ('Service Tax Invoice')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(ServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(EDcess)as  Decimal(25,2))[Edcess],cast(sum(Hedcess)as  Decimal(25,2))[Total Hedcess],cast(Sum(Total) as  Decimal(25,2)) as [Total Amount] from invoice i Where status in ('1','2') and invoicetype in ('Service Tax Invoice')";
                }
                else if (ddlsub.SelectedIndex == 3)
                {
                    Condition = Condition + "Select [InvoiceNo],[PO_NO],[Invoice_Date],[CC_Code],CAST(BasicValue AS  Decimal(25,2))[BasicValue],CAST(ServiceTax AS  Decimal(25,2))as [servicetax],CAST(Edcess AS  Decimal(25,2))as[Edcess],CAST(Hedcess AS  Decimal(25,2))as[Hedcess],cast(Total as  Decimal(25,2)) as [Total] from invoice i where status in ('1,'2') and invoicetype in ('SEZ/Service Tax exumpted Invoice')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(ServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(EDcess)as  Decimal(25,2))[Edcess],cast(sum(Hedcess)as  Decimal(25,2))[Total Hedcess],cast(Sum(Total) as  Decimal(25,2)) as [Total Amount] from invoice i Where status in ('1','2') and invoicetype in ('SEZ/Service Tax exumpted Invoice')";
                }
                else if (ddlsub.SelectedIndex == 4)
                {
                    Condition = Condition + "Select [InvoiceNo],[PO_NO],[Invoice_Date],[CC_Code],CAST(BasicValue AS  Decimal(25,2))[BasicValue],CAST(ServiceTax AS  Decimal(25,2))as [servicetax],CAST(Edcess AS  Decimal(25,2))as[Edcess],CAST(Hedcess AS  Decimal(25,2))as[Hedcess],cast(Total as  Decimal(25,2)) as [Total] from invoice i where status in ('1,'2') and i.paymenttype in ('Trading Supply')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(ServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(EDcess)as  Decimal(25,2))[Edcess],cast(sum(Hedcess)as  Decimal(25,2))[Total Hedcess],cast(Sum(Total) as  Decimal(25,2)) as [Total Amount] from invoice i Where status in ('1','2') and i.paymenttype in ('Trading Supply')";

                }
                else if (ddlsub.SelectedIndex == 5)
                {
                    Condition = Condition + "Select [InvoiceNo],[PO_NO],[Invoice_Date],[CC_Code],CAST(BasicValue AS  Decimal(25,2))[BasicValue],CAST(ServiceTax AS  Decimal(25,2))as [servicetax],CAST(Edcess AS  Decimal(25,2))as[Edcess],CAST(Hedcess AS  Decimal(25,2))as[Hedcess],cast(Total as  Decimal(25,2)) as [Total] from invoice i where status in ('1','2') and i.paymenttype in ('Manufacturing')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(ServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(EDcess)as  Decimal(25,2))[Edcess],cast(sum(Hedcess)as  Decimal(25,2))[Total Hedcess],cast(Sum(Total) as  Decimal(25,2)) as [Total Amount] from invoice i Where status in ('1','2') and i.paymenttype in ('Manufacturing')";

                }
                else if (ddlsub.SelectedIndex == 6)
                {
                    Condition = Condition + "Select [InvoiceNo],[PO_NO],[Invoice_Date],[CC_Code],CAST(BasicValue AS  Decimal(25,2))[BasicValue],CAST(ServiceTax AS  Decimal(25,2))as [servicetax],CAST(Edcess AS  Decimal(25,2))as[Edcess],CAST(Hedcess AS  Decimal(25,2))as[Hedcess],cast(Total as  Decimal(25,2)) as [Total] from invoice i where status in ('1','2') and i.paymenttype in ('Advance')";
                    Condition1 = Condition1 + "select CAST(sum(Basicvalue) AS  Decimal(25,2))[TotalBasic],cast(sum(ServiceTax) as  Decimal(25,2))[Total ServiceTax],cast(sum(EDcess)as  Decimal(25,2))[Edcess],cast(sum(Hedcess)as  Decimal(25,2))[Total Hedcess],cast(Sum(Total) as  Decimal(25,2)) as [Total Amount] from invoice i Where status in ('1','2') and i.paymenttype in ('Advance')";
                }
            }
        }
        if (rbtntype.SelectedIndex == 1)
        {
            if (ddltypeofpay.SelectedIndex == 2)
            {
                if (ddlsubtype.SelectedIndex == 1)
                {
                    Condition = Condition + "Select [Invoiceno],REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],[CC_Code],[DCA_Code],[Sub_DCA],Name,[Bank_Name],[ModeOfPay],[No],CAST(Debit AS  Decimal(25,2)) [Amount] from bankbook i where (status='Debited' or status='3')";
                    Condition1 = Condition1 + "Select Round(sum(isnull(Debit,0)),2) as [Total Debit] from bankbook i where (status='Debited' or Status='3')";
                }
                else if (ddlsubtype.SelectedIndex == 2)
                {
                    Condition = Condition + "Select [Invoiceno],REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],[CC_Code],[DCA_Code],[Sub_DCA],Name,[Bank_Name],[ModeOfPay],[No],CAST(Debit AS  Decimal(25,2)) [Amount] from bankbook i where paymenttype='General'";
                    Condition1 = Condition1 + "Select CAST(sum(Credit) AS  Decimal(25,2))[Total Credit],cast(sum(Debit) as  Decimal(25,2)) as [Total Debit] from bankbook i where paymenttype='General'";
                }
                else if (ddlsubtype.SelectedIndex == 3)
                {
                    Condition = Condition + "Select [Invoiceno],REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')[Date],[Bank_Name],[Description],[ModeOfPay],[No],CAST(Debit AS  Decimal(25,2))[Amount] from bankbook i where i.paymenttype='Withdraw'";
                    Condition1 = Condition1 + "Select cast(sum(Debit) as  Decimal(25,2)) as [Total Debit] from bankbook i where paymenttype='Withdraw'";

                }
            }
        }
        if (rbtntype.SelectedIndex == 2)
        {
            Condition = Condition + "select REPLACE(CONVERT(VARCHAR(11),Date, 106), ' ', '-')date,bank_name,description,Credit,debit from bankbook where PaymentType='Transfer'";
            Condition1 = Condition1 + "select Round((sum(isnull(Credit,0))),0)[Total Credit],Round((sum(isnull(Debit,0))),0) [Total Debit] from bankbook where PaymentType='Transfer'";
        }
        if (Condition != "" || Condition1 != "")
        {
            if (rbtntype.SelectedIndex == 0)
            {
                if (ddlcccode.SelectedIndex != 0 && ddlcccode.SelectedIndex != 1)
                {

                    Condition = Condition + " and i.cc_code='" + ddlcccode.SelectedValue + "'";
                    Condition1 = Condition1 + " and  i.cc_code='" + ddlcccode.SelectedValue + "'";
                    Condition2 = Condition2 + " and  i.cc_code='" + ddlcccode.SelectedValue + "'";

                }
                if (ddlcccode.SelectedIndex == 1)
                {
                    // if(ddlcctype.SelectedItem.Text
                    if (ddlcctype.SelectedIndex == 1)
                    {
                        Condition = Condition + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "' and cc_subtype='" + ddltype.SelectedItem.Text + "') ";
                        Condition1 = Condition1 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "' and cc_subtype='" + ddltype.SelectedItem.Text + "')";
                        Condition2 = Condition2 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "' and cc_subtype='" + ddltype.SelectedItem.Text + "')";

                    }
                    else if (ddlcctype.SelectedIndex == 2)
                    {
                        Condition = Condition + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";
                        Condition1 = Condition1 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";
                        Condition2 = Condition2 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";

                    }
                    else if (ddlcctype.SelectedIndex == 3)
                    {
                        Condition = Condition + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";
                        Condition1 = Condition1 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";
                        Condition2 = Condition2 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";

                    }
                }
                if (ddlsubtype.SelectedIndex != 1 && ddlsubtype.SelectedIndex != 2 && ddlsub.SelectedIndex != 8)
                {
                    if (ddlyear.SelectedIndex != 0)
                    {
                        if (ddlmonth.SelectedIndex != 0)
                        {
                            string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;

                            Condition = Condition + "and ( (invoice_date is null and datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + ") or (invoice_date is not null and datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy + "))";
                            Condition1 = Condition1 + "and ( (invoice_date is null and datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + ") or (invoice_date is not null and datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy + "))";
                            Condition2 = Condition2 + "and ( (invoice_date is null and datepart(mm,b.date)=" + ddlmonth.SelectedValue + " and datepart(yy,b.date)=" + yy + ") or (invoice_date is not null and datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy + "))";
                           //Condition = Condition + " and (datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy;
                           // Condition1 = Condition1 + " and datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy;
                           // Condition2 = Condition2 + " and datepart(mm,invoice_date)=" + ddlmonth.SelectedValue + " and datepart(yy,invoice_date)=" + yy;
                        }
                        else
                        {
                            Condition = Condition + "and (invoice_date is not null and convert(datetime,invoice_date)between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "') or ( invoice_date is null and convert(datetime,b.date)between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "')";
                            Condition1 = Condition1 + "and (invoice_date is not null and convert(datetime,invoice_date)between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "') or ( invoice_date is null and convert(datetime,b.date)between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "')";
                            Condition2 = Condition2 + "and (invoice_date is not null and convert(datetime,invoice_date)between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "') or ( invoice_date is null and convert(datetime,b.date)between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "')";
                           // Condition = Condition + " and convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                           // Condition1 = Condition1 + " and convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                           // Condition2 = Condition2 + " and convert(datetime,invoice_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                        }
                    }
                   
                    if (ddlpo.SelectedIndex!= 0 && ddlpo.SelectedIndex != 1)
                    {
                        Condition = Condition + " and   i.po_no='" + ddlpo.SelectedValue + "'";
                        Condition1 = Condition1 + " and  i.po_no='" + ddlpo.SelectedValue + "'";
                        Condition2 = Condition2 + " and  i.po_no='" + ddlpo.SelectedValue + "'";
                    }
                }
                else
                {
                    if (ddlyear.SelectedIndex != 0)
                    {
                        if (ddlmonth.SelectedIndex != 0)
                       {
                            string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                            Condition = Condition + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;
                            Condition1 = Condition1 + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;
                            Condition2 = Condition2 + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;
                        }
                        else
                        {
                            Condition = Condition + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                            Condition1 = Condition1 + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                            Condition2 = Condition2 + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                        }
                    }
                }
            }
            else if (rbtntype.SelectedIndex == 1)
            {
                if (ddlcccode.SelectedIndex != 0 && ddlcccode.SelectedIndex != 1 && ddlsubtype.SelectedIndex != 3)
                {
                    Condition = Condition + " and i.cc_code='" + ddlcccode.SelectedValue + "'";
                    Condition1 = Condition1 + " and i.cc_code='" + ddlcccode.SelectedValue + "'";
                }  
                if (ddlcccode.SelectedIndex == 1 && ddlsubtype.SelectedIndex != 3)
                {
                    if (ddlcctype.SelectedIndex == 1)
                    {
                        Condition = Condition + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "' and cc_subtype='" + ddltype.SelectedItem.Text + "') ";
                        Condition1 = Condition1 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "' and cc_subtype='" + ddltype.SelectedItem.Text + "')";
                    }
                    else if (ddlcctype.SelectedIndex == 2)
                    {
                        Condition = Condition + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";
                        Condition1 = Condition1 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";
                    }
                    else if (ddlcctype.SelectedIndex == 3)
                    {
                        Condition = Condition + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";
                        Condition1 = Condition1 + " and i.cc_code in (Select cc_code from cost_center where cc_type='" + ddlcctype.SelectedItem.Text + "')";

                    }
                }
                if (ddlyear.SelectedIndex != 0)
                {
                    if (ddlmonth.SelectedIndex != 0)
                    {
                        string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                        Condition = Condition + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;
                        Condition1 = Condition1 + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;

                    }
                    else
                    {
                        Condition = Condition + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                        Condition1 = Condition1 + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                    }
                }
                           
                if (ddldetailhead.SelectedItem.Text != "Select All" && ddlsubtype.SelectedIndex != 3)
                {
                    Condition = Condition + " and DCA_Code='" + ddldetailhead.SelectedValue + "'";
                    Condition1 = Condition1 + " and DCA_Code='" + ddldetailhead.SelectedValue + "'";
                }
                if (ddlsubdetail.SelectedItem.Text != "Select Sub DCA" && ddlsubtype.SelectedIndex != 3)
                {
                        Condition = Condition + " and Sub_DCA='" + ddlsubdetail.SelectedValue + "'";
                        Condition1 = Condition1 + " and Sub_DCA='" + ddlsubdetail.SelectedValue + "'";
                }
            }
            else if (rbtntype.SelectedIndex == 2)
            {
                if (ddlyear.SelectedIndex != 0)
                {
                    if (ddlmonth.SelectedIndex != 0)
                    {
                        string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                        Condition = Condition + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;
                        Condition1 = Condition1 + " and datepart(mm,date)=" + ddlmonth.SelectedValue + " and datepart(yy,date)=" + yy;

                    }
                    else
                    {
                        Condition = Condition + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                        Condition1 = Condition1 + " and convert(datetime,date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                    }
                }
            }
        }
        if (rbtntype.SelectedIndex == 0 && ddltypeofpay.SelectedIndex == 1 && ddlsub.SelectedIndex == 1)
        {
            GridView3.Visible = true;
            string Condition3 = "";
            string Condition4 = "";
            Condition4 = "SELECT CAST(sum(j.totalbasic) AS Decimal(25,2))[TotalBasic],cast(sum(j.[Total ServiceTax]) as decimal(25,2))[Total ServiceTax],cast(sum(j.[Sales Tax]) as decimal(25,2)) as [Sales Tax],cast(sum(j.[Total Excixeduty]) as decimal(25,2))as[Total Excixeduty],cast(sum(j.[Total Edcess])as decimal(25,2))[Total Edcess],cast(sum(j.[Total Hedcess])as decimal(25,2))[Total Hedcess],cast(sum(j.[Total TDS])as decimal(25,2))[Total TDS],CAST(sum(j.retention) AS Decimal(25,2))as[retention],CAST(sum(j.Advance) AS Decimal(25,2))as[Advance],CAST(sum(j.hold) AS Decimal(25,2))as[Hold],CAST(sum(j.Anyother) AS Decimal(25,2))as[Anyother],cast(sum(j.[Total Net Amount]) as decimal(25,2))[Total Net Amount]  FROM (select CAST(sum(Basicvalue) AS Decimal(25,2))[TotalBasic],cast(sum(NetServiceTax) as decimal(25,2))[Total ServiceTax],cast(sum(NetSalestax) as decimal(25,2)) as [Sales Tax],cast(sum(NetExciseDuty) as  Decimal(25,2))as[Total Excixeduty],cast(sum(NetEDcess)as decimal(25,2))[Total Edcess],cast(sum(NetHedcess)as decimal(25,2))[Total Hedcess],cast(sum(tds)as decimal(25,2))[Total TDS],CAST(sum(retention) AS Decimal(25,2))as[retention],CAST(sum(Advance) AS Decimal(25,2))as[Advance],CAST(sum(hold) AS Decimal(25,2))as[Hold],CAST(sum(Anyother) AS Decimal(25,2))as[Anyother],cast(sum(amount) as decimal(25,2))[Total Net Amount]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where  i.status='credit' and b.paymenttype in ('Invoice Service','Trading Supply','Advance','Manufacturing') " + Condition1 + " UNION  select CAST(sum(0) AS Decimal(25,2))[TotalBasic],cast(sum(0) as decimal(25,2))[Total ServiceTax],cast(sum(0) as decimal(25,2)) as [Sales Tax],cast(sum(0) as  Decimal(25,2))as[Total Excixeduty],cast(sum(0)as decimal(25,2))[Total Edcess],cast(sum(0)as decimal(25,2))[Total Hedcess],cast(sum(0)as decimal(25,2))[Total TDS],CAST(sum(0) AS Decimal(25,2))as[retention],CAST(sum(0) AS Decimal(25,2))as[Advance],CAST(sum(0) AS Decimal(25,2))as[Hold],CAST(sum(0) AS Decimal(25,2))as[Anyother],cast(sum(b.credit) as decimal(25,2))[Total Net Amount]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where  b.paymenttype in ('Retention','Hold')  " + Condition1 + ")J";

            Condition3 = "Select 'Total Net Credit Against Advance' as [Credit Summary],cast(isnull(sum(b.credit),0)-isnull((Sum(NetServiceTax)+SUM(NetEdcess)+SUM(NetHedcess)),0) as decimal(25,2))[Total Net Amount]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where i.status='credit' and b.paymenttype in ('Advance') " + Condition2 + " Union All Select 'Total Net Credit Against Invoice' as [Description],cast(isnull(sum(b.credit),0)-isnull((Sum(NetServiceTax)+SUM(NetEdcess)+SUM(NetHedcess)),0) as decimal(25,2))[Total Net Amount]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where i.status='credit' and b.paymenttype in ('Invoice Service','Trading Supply','Manufacturing') " + Condition2 + " UNION All  Select 'Total Net Credit Against Hold' as [Description],cast(isnull(sum(b.credit),0) as decimal(25,2))[Total Net Amount]   from bankbook b join invoice i on b.invoiceno=i.invoiceno where  b.paymenttype in ('Hold') " + Condition2 + " UNION ALL Select 'Total Net Credit Against Retention' as [Description],cast(isnull(sum(b.credit),0) as decimal(25,2))[Total Net Amount]   from bankbook b join invoice i on b.invoiceno=i.invoiceno where  b.paymenttype in ('Retention') " + Condition2 + " UNION ALL Select 'Total' as [Description],cast(0 as decimal(25,2))[Total Net Amount]  UNION ALL Select 'Total ServiceTax Credit' as [Description],CAST(isnull((Sum(NetServiceTax)+SUM(NetEdcess)+SUM(NetHedcess)),0) AS  Decimal(25,2))as [servicetax]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where  i.status='credit' and b.paymenttype in ('Invoice Service','Trading Supply','Advance','Manufacturing') " + Condition2 + " UNION ALL Select 'Total SalesTax Credit' as [Description],CAST(isnull(Sum(NetSalesTax),0) AS  Decimal(25,2))as [Salestax]  from bankbook b join invoice i on b.invoiceno=i.invoiceno where  i.status='credit' and b.paymenttype in ('Invoice Service','Trading Supply','Advance','Manufacturing') " + Condition2 + " UNION ALL Select 'Total AnyOther Tax Credit' as [Description],CAST(0 AS  Decimal(25,2))as [Salestax]";

            da = new SqlDataAdapter(Condition + ";" + Condition4 + ";" + Condition3, con);
            da.Fill(ds, "bank");
            if (ds.Tables["bank"].Rows.Count > 0)
            {
                ViewState["search"] = Condition;
                GridView1.DataSource = ds.Tables["bank"];
                GridView1.DataBind();
                GridView2.DataSource = ds.Tables["bank1"];
                GridView2.DataBind();
                GridView3.DataSource = ds.Tables["bank2"];
                GridView3.DataBind();
            }
        }
        else
        {
            GridView3.Visible = false;
            da = new SqlDataAdapter(Condition + ";" + Condition1, con);
            da.Fill(ds, "bank");
            if (ds.Tables["bank"].Rows.Count > 0)
            {
                ViewState["search"] = Condition;
                GridView1.DataSource = ds.Tables["bank"];
                GridView1.DataBind();
                GridView2.DataSource = ds.Tables["bank1"];
                GridView2.DataBind();
             
            }
        }
       
    }
    public string TypeOfPayment
    {
        get
        {
            if (rbtntype.SelectedIndex != 2)
            {
                if (ddltypeofpay.SelectedValue != "Refund" && ddltypeofpay.SelectedValue != "Service" && ddltypeofpay.SelectedValue!="Select All Credits" && ddltypeofpay.SelectedIndex != 0)
                    
                    return ddltypeofpay.SelectedValue;
                else if ((ddltypeofpay.SelectedValue == "Refund" || ddltypeofpay.SelectedValue == "Service") && ddlsubtype.SelectedIndex != 0)
                    
                    return ddlsubtype.SelectedValue;
                else if (((ddltypeofpay.SelectedValue == "Select All Credits" || ddlsub.SelectedValue != "Refund") && ddlsub.SelectedValue == "Unsecured Loan") && ddltypeofpay.SelectedIndex != 0)

                    return ddlsub.SelectedValue;
                else if ((ddlsub.SelectedValue == "Refund") && ddlsub.SelectedIndex != 0)

                    return ddlsubtype.SelectedValue;
                else if ((ddltypeofpay.SelectedValue == "Select All Credits" && ddlsub.SelectedValue != "Unsecured Loan") && ddltypeofpay.SelectedIndex != 0)
                    return ddlsub.SelectedValue;

              
                else
                    return "";
            }
            else
            {
                return "Transfer";
            }
        }
        set
        {
            //strUsername = value;
        }


    }




  
    private void Fillsumgrid()
    {
        try
        {
            da = new SqlDataAdapter(ViewState["sum"].ToString(), con);
            da.Fill(ds, "sum");
            GridView2.DataSource = ds.Tables["sum"];
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

   

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmviewbankflow.aspx");
    }
    public void subpayment()
    {
        ddlsub.Items.Clear();
        cc.Visible = false;
        PO.Visible = false;
        Dca.Visible = false;
        ddlsub.Visible = true;
        if (ddltypeofpay.SelectedIndex == 1 && rbtntype.SelectedIndex==0)
        {
            ddlsub.Items.Add("--Select--");
            ddlsub.Items.Add("All Credits");
            ddlsub.Items.Add("All Invoice");
            ddlsub.Items.Add("Service Tax Invoice");
            ddlsub.Items.Add("SEZ/Service Tax exumpted Invoice");
            ddlsub.Items.Add("Trading Supply");
            ddlsub.Items.Add("Manufacturing");
            ddlsub.Items.Add("Advance");
            ddlsub.Items.Add("Retention");
            ddlsub.Items.Add("Hold");
            ddlsub.Items.Add("Refund");
            ddlsub.Items.Add("Unsecured Loan");
        }
        else if (ddltypeofpay.SelectedIndex == 2 && rbtntype.SelectedIndex == 0)
        {
            ddlsub.Items.Add("--Select--");
            ddlsub.Items.Add("All Pending Invoice");
            ddlsub.Items.Add("Service Tax Invoice");
            ddlsub.Items.Add("SEZ/Service Tax exumpted Invoice");
            ddlsub.Items.Add("Trading Supply");
            ddlsub.Items.Add("Manufacturing");
            ddlsub.Items.Add("Advance");
            ddlsub.Items.Add("Retention");
            ddlsub.Items.Add("Hold");

        }
        else
        {
            ddlsub.Visible = false;
        }
    }
    public void Refund()
    {
        if (ddlsub.SelectedItem.Text == "Refund")
        {
            ddlsubtype.Visible = true;
            ddlpo.Visible = false;
            lblpono.Visible = false;
            ddlpo.Items.Clear();
            ddlsubtype.Items.Add("-Select Refund Type-");
            ddlsubtype.Items.Add("FD");
            ddlsubtype.Items.Add("SD");
            ddlsubtype.ToolTip = "Refund Type";
        
        }
        else if (ddlsub.SelectedItem.Text == "Unsecured Loan")
        {
            ddlpo.Visible = false;
            ddlpo.Items.Clear();
        }
        else
        {
            ddlpo.Visible = true;
            ddlsubtype.Visible = false;
            ddlpo.Items.Clear();
        }
    
    
    }


    protected void ddlsub_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Refund();
        ddlcctype.SelectedIndex = 0;
        ddltype.SelectedIndex = 0;
        Clear();
        if (ddlsub.SelectedItem.Text == "All Credits" || ddlsub.SelectedItem.Text == "All Invoice" || ddlsub.SelectedItem.Text == "Service Tax Invoice" || ddlsub.SelectedItem.Text == "SEZ/Service Tax exumpted Invoice" || ddlsub.SelectedItem.Text == "Trading Supply" || ddlsub.SelectedItem.Text == "Manufacturing" || ddlsub.SelectedItem.Text == "Advance" || ddlsub.SelectedItem.Text == "Retention" || ddlsub.SelectedItem.Text == "Hold" || ddlsub.SelectedItem.Text == "All Pending Invoice")
        {
            trcctype.Visible = true;
            cc.Visible = false;
            ddlsubtype.Visible = false;
            PO.Visible = false;
            ddlpo.Items.Clear();
        }
        else if (ddlsub.SelectedItem.Text == "Refund")
        {
            cc.Visible = false;
            trcctype.Visible = false;
            PO.Visible = false;
            ddlpo.Items.Clear();
            ddlsubtype.Visible = true;
            ddlsubtype.Items.Clear();
            ddlsubtype.Items.Add("-Select Refund Type-");
            ddlsubtype.Items.Add("FD");
            ddlsubtype.Items.Add("SD");
            ddlsubtype.ToolTip = "Refund Type";
        }
        else if (ddlsub.SelectedItem.Text == "Unsecured Loan")
        {
            trcctype.Visible = false;
            cc.Visible = false;
            PO.Visible = true;
            ddlpo.Items.Clear();
            //lblpono.Visible = false;
            //ddlpo.Visible = false;
            //lblmonth.Visible = true;
            //ddlmonth.Visible = true;
            //lblyear.Visible = true;
            //ddlyear.Visible = true;
            ddlsubtype.Visible = false;

        }
    }
    public void Clear()
    {
        //CascadingDropDown4.SelectedValue = "";
        //CascadingDropDown1.SelectedValue = "";
        CascadingDropDown2.SelectedValue = "";
        CascadingDropDown3.SelectedValue = "";
        ddlmonth.SelectedItem.Text = "Select Month";
        ddlyear.SelectedIndex = 0;
        //ddlcccode.SelectedIndex = 0;
        //ddlpo.SelectedIndex = 0;
       
      
    
    }
   

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
        if (ddltype.SelectedItem.Text != "Select")
        {
            
            if (ddltype.SelectedItem.Text == "Service")
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_type='Performing' and status in ('Old','New') and cc_subtype='Service' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
            else if (ddltype.SelectedItem.Text == "Trading")
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_type='Performing' and status in ('Old','New') and cc_subtype='Trading' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
            else if (ddltype.SelectedItem.Text == "Manufacturing")
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_type='Performing' and status in ('Old','New') and cc_subtype='Manufacturing' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
            da.Fill(ds, "SERVICECC");
            if (ds.Tables["SERVICECC"].Rows.Count > 0)
            {
                cc.Visible = true;
                ddlpo.Items.Clear();
                ddlcccode.DataTextField = "Name";
                ddlcccode.DataValueField = "cc_code";
                ddlcccode.DataSource = ds.Tables["SERVICECC"];
                ddlcccode.DataBind();
                ddlcccode.Items.Insert(0, "Select Cost Center");
                ddlcccode.Items.Insert(1, "Select All");
                if (rbtntype.SelectedIndex == 0)
                {
                    Dca.Visible = false;
                    lblpono.Visible = true;
                    ddlpo.Visible = true;
                    ddlyear.Visible = true;
                    ddlmonth.Visible = true;
                    PO.Visible = true;


                }
                else if (rbtntype.SelectedIndex == 1)
                {
                    Dca.Visible = true;
                    lblpono.Visible = false;
                    ddlpo.Visible = false;
                    ddlyear.Visible = true;
                    ddlmonth.Visible = true;
                    PO.Visible = true;

                }
            }
            else
            {
                //ddlcccode.DataSource = null;
                //ddlcccode.DataBind();
                //ddlcccode.Items.Insert(0, "Select Cost Center");
                ddlcccode.Items.Clear();
                cc.Visible = false;
                lblpono.Visible = false;
                ddlpo.Visible = false;
                ddlyear.Visible = false;
                ddlmonth.Visible = false;
                PO.Visible = false;

            }
        }
        else if (ddltype.SelectedItem.Text == "Select")
        {
            //da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_type='Performing' and status in ('Old','New')", con);
            //da.Fill(ds, "SERVICECC");
            //ddlcccode.DataTextField = "Name";
            //ddlcccode.DataValueField = "cc_code";
            //ddlcccode.DataSource = ds.Tables["SERVICECC"];
            //ddlcccode.DataBind();
            //ddlcccode.Items.Insert(0, "Select Cost Center");
            //cc.Visible = true;
            //PO.Visible = true;

            ddlcccode.DataSource = null;
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "Select Cost Center");
            cc.Visible = false;
            lblpono.Visible = false;
            ddlpo.Visible = false;
            ddlyear.Visible = false;
            ddlmonth.Visible = false;
            PO.Visible = false;
        }



    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        ddltype.SelectedIndex = 0;
        Clear();
        if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
        {
            Label10.Visible = false;
            ddltype.Visible = false;
            if (rbtntype.SelectedIndex == 0)
            {
                cc.Visible = false;
                Dca.Visible = false;
                lblpono.Visible = false;
                ddlpo.Visible = false;
                ddlyear.Visible = false;
                ddlmonth.Visible = false;
                PO.Visible = false;
                ddlpo.Items.Clear();
            }
            else if (rbtntype.SelectedIndex == 1)
            {
                cc.Visible = true;
                Dca.Visible = true;
                lblpono.Visible = false;
                ddlpo.Visible = false;
                ddlyear.Visible = true;
                ddlmonth.Visible =  true;
                PO.Visible = true;
                ddlpo.Items.Clear();
            }
            da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_type in ('"+ddlcctype.SelectedItem.Text+"') and status in ('Old','New')", con);
            da.Fill(ds, "CC");
            ddlcccode.DataTextField = "Name";
            ddlcccode.DataValueField = "cc_code";
            ddlcccode.DataSource = ds.Tables["CC"];
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "Select Cost Center");
            ddlcccode.Items.Insert(1, "Select All");
        }
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            Label10.Visible = true;
            ddltype.Visible = true;
            cc.Visible = false;
            Dca.Visible = false;
            lblpono.Visible = false;
            ddlpo.Visible = false;
            ddlyear.Visible = false;
            ddlmonth.Visible = false;
            PO.Visible = false;
            ddlpo.Items.Clear();
        }
        if (ddlcctype.SelectedItem.Text == "Select")
        {
            Label10.Visible = false;
            ddltype.Visible = false;
            cc.Visible = false;
            Dca.Visible = false;
            lblpono.Visible = false;
            ddlpo.Visible = false;
            ddlyear.Visible = false;
            ddlmonth.Visible = false;
            PO.Visible = false;
            ddlpo.Items.Clear();
        }
    }


   
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedIndex == 0)
        {
            ddlpo.Items.Clear();
            if (ddlcccode.SelectedItem.Text != "Select Cost Center" && ddlcccode.SelectedItem.Text != "Select All")
            {
                da = new SqlDataAdapter("Select distinct po_no from invoice where cc_code='" + ddlcccode.SelectedValue + "'", con);
                da.Fill(ds, "CreditPO");
                if (ds.Tables["CreditPO"].Rows.Count > 0)
                {
                    PO.Visible = true;
                    ddlpo.DataTextField = "po_no";
                    ddlpo.DataValueField = "po_no";
                    ddlpo.DataSource = ds.Tables["CreditPO"];
                    ddlpo.DataBind();
                    ddlpo.Items.Insert(0, "Select PO");
                    ddlpo.Items.Insert(1, "Select All");
                }
                else
                {
                    ddlpo.DataSource = null;
                    ddlpo.DataBind();
                    ddlpo.Items.Insert(0, "Select PO");
                    PO.Visible = false;
                }
            }
            else if (ddlcccode.SelectedItem.Text == "Select All")
            {
                ddlpo.DataSource = null;
                ddlpo.DataBind();
                ddlpo.Items.Insert(0, "Select PO");
                ddlpo.Items.Insert(1, "Select All");
                PO.Visible = true;
            }
            else if (ddlcccode.SelectedItem.Text == "Select Cost Center")
            {
                ddlpo.DataSource = null;
                ddlpo.DataBind();
                ddlpo.Items.Insert(0, "Select PO");
                PO.Visible = false;

            }
        }
        else {
            ddlpo.Items.Clear();
            //PO.Visible = false;
            PO.Visible = true;
        }
    }
    private decimal Amount = (decimal)0.0;
    private decimal GrandTotal = (decimal)0.0;
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "Total Net Credit Against Advance" || e.Row.Cells[0].Text == "Total Net Credit Against Invoice" || e.Row.Cells[0].Text == "Total Net Credit Against Hold" || e.Row.Cells[0].Text == "Total Net Credit Against Retention")
            {
                Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Net Amount"));
            }
            if (e.Row.Cells[0].Text == "Total")
            {
                e.Row.Cells[1].Text = Amount.ToString();
                e.Row.Cells[1].BackColor = System.Drawing.Color.LightBlue;
                e.Row.Cells[0].BackColor = System.Drawing.Color.LightBlue;
            }
            GrandTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Net Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Grand Total";
            e.Row.Cells[1].Text = String.Format("Rs. {0:#,##,##,###.00}", GrandTotal);

        }
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.AllowPaging = false;
        FillGrid();
        Filter();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Bankflow"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        GridView2.RenderControl(htmlwriter);
        GridView3.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }


}
              