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

public partial class VerifyClaimFD : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
            tblclaimfd.Visible = false;
          
        }
    }
    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                //da = new SqlDataAdapter("select bk.bank_name,bk.FDR,bk.transaction_no,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as date,bk.description,CAST(bk.credit AS Decimal(20,2)) as Amount  from fd_claim fd  join bankbook bk on fd.transaction_no=bk.transaction_no and fd.fdr=bk.fdr where bk.status='1' group by bk.transaction_no,bk.bank_name,bk.FDR,bk.transaction_no,bk.date,bk.description,bk.credit", con);
                da = new SqlDataAdapter("select bk.bank_name,bk.FDR,bk.transaction_no,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as date,bk.description,CAST(bk.credit AS Decimal(20,2)) as Amount,af.status  from fd_claim fd  join bankbook bk on fd.transaction_no=bk.transaction_no and fd.fdr=bk.fdr join AddFD af on af.fdr=fd.fdr where bk.status='1' group by bk.transaction_no,bk.bank_name,bk.FDR,bk.transaction_no,bk.date,bk.description,bk.credit,af.status", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select bk.bank_name,bk.FDR,bk.transaction_no,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as date,bk.description,CAST(bk.credit AS Decimal(20,2)) as Amount,af.status  from fd_claim fd  join bankbook bk on fd.transaction_no=bk.transaction_no and fd.fdr=bk.fdr join AddFD af on af.fdr=fd.fdr where bk.status='2' group by bk.transaction_no,bk.bank_name,bk.FDR,bk.transaction_no,bk.date,bk.description,bk.credit,af.status", con);
            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string Tran = GridView1.DataKeys[e.NewEditIndex]["transaction_no"].ToString();
        ViewState["TRAN"] = Tran;
        ViewState["FDR"] = GridView1.DataKeys[e.NewEditIndex]["FDR"].ToString();
        filltable();
        tblclaimfd.Visible = true;

    }
    public void filltable()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            da = new SqlDataAdapter(" select af.id as AFID,bk.id as BKID,af.status,REPLACE(CONVERT(VARCHAR(11),af.date, 106), ' ', '-')as date,af.FDR,REPLACE(CONVERT(VARCHAR(11),af.Fromdate, 106), ' ', '-')as Fromdate,REPLACE(CONVERT(VARCHAR(11),af.todate, 106), ' ', '-')as Todate,replace(af.RateofIntrest,'.0000','.00')as Intrest,replace(af.Amount,'.0000','.00')as Amount,bk.bank_name,bk.ModeofPay,bk.PaymentType,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as Bankdate,bk.no,bk.description from AddFD af join bankbook bk on af.fdr=bk.fdr where af.Approval_status='3' and af.status in ('Closed','Partially') and af.fdr='" + ViewState["FDR"].ToString() + "' and bk.transaction_no is null; select bk.bank_name,bk.FDR,bk.transaction_no,REPLACE(CONVERT(VARCHAR(11),fd.date, 106), ' ', '-')as closingdate,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as date,bk.description,CAST(bk.credit AS Decimal(20,2)) as credit,bk.Modeofpay,bk.no from fd_claim fd  join bankbook bk on fd.transaction_no=bk.transaction_no and fd.fdr=bk.fdr where bk.transaction_no='" + ViewState["TRAN"].ToString() + "' group by bk.transaction_no,bk.bank_name,bk.FDR,bk.transaction_no,bk.date,bk.description,bk.credit,bk.Modeofpay,bk.no,fd.date;select CAST(Amount AS Decimal(20,2))as Maturity from fd_claim where transaction_no='" + ViewState["TRAN"].ToString() + "' and type='Principle' and status='1';select CAST(Amount AS Decimal(20,2))as Intrest from fd_claim where transaction_no='" + ViewState["TRAN"].ToString() + "' and type='Intrest' and status='1'; select CAST(isnull(sum(Amount),0) AS Decimal(20,2))as Ded from fd_claim where transaction_no='" + ViewState["TRAN"].ToString() + "' and type='Deduction' and status='1'; select fd.dca_code,(d.mapdca_code+' , '+d.dca_name)as DCA,fd.sub_dca,(sd.subdca_code+' , '+sd.subdca_name)as SubDca,CAST(fd.Amount AS Decimal(20,2)) as Amount from fd_claim fd join dca d on d.dca_code=fd.dca_code join subdca sd on sd.subdca_code=fd.sub_dca where transaction_no='" + ViewState["TRAN"].ToString() + "' and type='Deduction' and fd.status='1'", con);
        else if (Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter(" select af.id as AFID,bk.id as BKID,af.status,REPLACE(CONVERT(VARCHAR(11),af.date, 106), ' ', '-')as date,af.FDR,REPLACE(CONVERT(VARCHAR(11),af.Fromdate, 106), ' ', '-')as Fromdate,REPLACE(CONVERT(VARCHAR(11),af.todate, 106), ' ', '-')as Todate,replace(af.RateofIntrest,'.0000','.00')as Intrest,replace(af.Amount,'.0000','.00')as Amount,bk.bank_name,bk.ModeofPay,bk.PaymentType,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as Bankdate,bk.no,bk.description from AddFD af join bankbook bk on af.fdr=bk.fdr where af.Approval_status='3' and af.status in ('Closed','Partially') and af.fdr='" + ViewState["FDR"].ToString() + "' and bk.transaction_no is null; select bk.bank_name,bk.FDR,bk.transaction_no,REPLACE(CONVERT(VARCHAR(11),fd.date, 106), ' ', '-')as closingdate,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as date,bk.description,CAST(bk.credit AS Decimal(20,2)) as credit,bk.Modeofpay,bk.no from fd_claim fd  join bankbook bk on fd.transaction_no=bk.transaction_no and fd.fdr=bk.fdr where bk.transaction_no='" + ViewState["TRAN"].ToString() + "' group by bk.transaction_no,bk.bank_name,bk.FDR,bk.transaction_no,bk.date,bk.description,bk.credit,bk.Modeofpay,bk.no,fd.date;select CAST(Amount AS Decimal(20,2))as Maturity from fd_claim where transaction_no='" + ViewState["TRAN"].ToString() + "' and type='Principle' and status='2';select CAST(Amount AS Decimal(20,2))as Intrest from fd_claim where transaction_no='" + ViewState["TRAN"].ToString() + "' and type='Intrest' and status='2'; select CAST(isnull(sum(Amount),0) AS Decimal(20,2))as Ded from fd_claim where transaction_no='" + ViewState["TRAN"].ToString() + "' and type='Deduction' and status='2'; select fd.dca_code,(d.mapdca_code+' , '+d.dca_name)as DCA,fd.sub_dca,(sd.subdca_code+' , '+sd.subdca_name)as SubDca,CAST(fd.Amount AS Decimal(20,2)) as Amount from fd_claim fd join dca d on d.dca_code=fd.dca_code join subdca sd on sd.subdca_code=fd.sub_dca where transaction_no='" + ViewState["TRAN"].ToString() + "' and type='Deduction' and fd.status='2'", con);

        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows[0]["status"].ToString() == "Closed")
            txttype.Text = "Claim FD";
        if (ds.Tables["data"].Rows[0]["status"].ToString() == "Partially")
            txttype.Text = "Partially Claim FD";
        txtfddate.Text = ds.Tables["data"].Rows[0]["date"].ToString();
        txtclosingdate.Text = ds.Tables["data1"].Rows[0]["closingdate"].ToString();
        txtfdrno.Text = ds.Tables["data"].Rows[0]["FDR"].ToString();
        txtfromdate.Text = ds.Tables["data"].Rows[0]["Fromdate"].ToString();
        txttodate.Text = ds.Tables["data"].Rows[0]["Todate"].ToString();
        txtrateofintrest.Text = ds.Tables["data"].Rows[0]["Intrest"].ToString();
        txtamount.Text = ds.Tables["data"].Rows[0]["Amount"].ToString();
        txtfrombank1.Text = ds.Tables["data"].Rows[0]["bank_name"].ToString();
        txtpayment1.Text = ds.Tables["data"].Rows[0]["ModeofPay"].ToString();
        txtdate1.Text = ds.Tables["data"].Rows[0]["Bankdate"].ToString();
        txtcheque1.Text = ds.Tables["data"].Rows[0]["no"].ToString();
        txtdesc1.Text = ds.Tables["data"].Rows[0]["description"].ToString();
        txtamt1.Text = ds.Tables["data"].Rows[0]["Amount"].ToString();
        txtfrom.Text = ds.Tables["data1"].Rows[0]["bank_name"].ToString();
        txtpayment.Text = ds.Tables["data1"].Rows[0]["Modeofpay"].ToString();
        txtdate.Text = ds.Tables["data1"].Rows[0]["date"].ToString();
        txtcheque.Text = ds.Tables["data1"].Rows[0]["no"].ToString();
        txtdesc.Text = ds.Tables["data1"].Rows[0]["description"].ToString();
        txtamt.Text = ds.Tables["data1"].Rows[0]["credit"].ToString();
        if (ds.Tables["data2"].Rows.Count > 0)
            txtmaturity.Text = ds.Tables["data2"].Rows[0]["Maturity"].ToString();
        else
            txtmaturity.Text = "0";
        if (ds.Tables["data3"].Rows.Count > 0)
            txtintrest.Text = ds.Tables["data3"].Rows[0]["Intrest"].ToString();
        else
            txtintrest.Text = "0";
        da = new SqlDataAdapter("select replace(sum(amount),'.0000','.00') as amt from fd_claim where type='Principle' and fdr='" + txtfdrno.Text + "' and status not in ('Rejected','1','2')", con);
        da.Fill(ds, "balamt");
        if (ds.Tables["balamt"].Rows[0]["amt"].ToString() != "NULL")
        {
            string bal = ds.Tables["balamt"].Rows[0]["amt"].ToString();
            if (bal == "")
                bal = "0";
            txtbalamt.Text = (Convert.ToDecimal(txtamount.Text) - Convert.ToDecimal(bal)).ToString();
        }
        else
        {
            txtbalamt.Text = ds.Tables["data"].Rows[0]["Amount"].ToString();
        }
        if (ds.Tables["data5"].Rows.Count > 0)
        {
            gvanyother.DataSource = ds.Tables["data5"];
            gvanyother.DataBind();
            txtdedvalue.Text = ds.Tables["data4"].Rows[0]["Ded"].ToString();
        }
        else
        {
            gvanyother.DataSource = null;
            gvanyother.DataBind();
            txtdedvalue.Text = "0";
        }

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = false;
        try
        {
            string deductiondcas = "";
            string deductionamts = "";            
            foreach (GridViewRow record in gvanyother.Rows)
            {
                    if (gvanyother != null)
                    {
                        if ((record.FindControl("chkSelectother") as CheckBox).Checked)
                        {
                            deductiondcas = deductiondcas + gvanyother.DataKeys[record.RowIndex]["dca_code"].ToString() + ",";                           
                            deductionamts = deductionamts + record.Cells[3].Text + ",";
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Please Verify Deduction");
                            break;
                        }
                    }
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_ClaimFD";
            cmd.Parameters.AddWithValue("@Date", txtclosingdate.Text);
            cmd.Parameters.AddWithValue("@Fdrno", ViewState["FDR"].ToString());
            cmd.Parameters.AddWithValue("@TranNo", ViewState["TRAN"].ToString());
            cmd.Parameters.AddWithValue("@Deductiondcas", deductiondcas);
            cmd.Parameters.AddWithValue("@Deductiondcaamts", deductionamts);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "";
            if (msg == "Verified")
            {
                JavaScript.UPAlertRedirect(Page, "Verified Successfully", "VerifyClaimFD.aspx");
                fillgrid();
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
                btnupdate.Visible = true;
            }
            con.Close();

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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_RejectClaimFD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Fdrno", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["FDR"].ToString();
            cmd.Parameters.Add("@Tranno", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["transaction_no"].ToString();          
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, msg, "VerifyClaimFD.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
            fillgrid();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
}