using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class VerifyAssetSalePayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                fillgrid();
                tblverifycredits.Visible = false;
                trbankpayment.Visible = false;
                tblverifysupplierandsp.Visible = false;
            }
        }
    }
    public void fillgrid()
    {
        try
        {
            tblverifycredits.Visible = false;
            //da = new SqlDataAdapter("select asp.id,asl.request_No,asp.Item_Code,asp.Payment_Type,cast(asp.Amount as decimal(20,2))as Amount from Asset_Sale asl join Asset_Payment asp on asl.request_No=asp.request_No where asp.Status='1' and asl.status!='Rejected'", con);
            da = new SqlDataAdapter("Select i.* from (select asp.id,asl.request_No,asp.Item_Code,asp.Payment_Type,cast(asp.Amount as decimal(20,2))as Amount,asp.Date from Asset_Sale asl join Asset_Payment asp on asl.request_No=asp.request_No where asp.Status='1' and asl.status!='Rejected' and asp.Payment_Type='BankPayment' union all select Tran_No as id,asl.request_No,asp.Item_Code,asp.Payment_Type,sum(cast(asp.Amount as decimal(20,2)))as Amount,asp.Date from Asset_Sale asl join Asset_Payment asp on asl.request_No=asp.request_No where asp.Status='1' and asl.status!='Rejected' and asp.Payment_Type='Service Provider' group by Tran_No,asl.request_No,asp.Item_Code,asp.Payment_Type,asp.Date union all select Tran_No as id,asl.request_No,asp.Item_Code,asp.Payment_Type,sum(cast(asp.Amount as decimal(20,2)))as Amount,asp.Date from Asset_Sale asl join Asset_Payment asp on asl.request_No=asp.request_No where asp.Status='1' and asl.status!='Rejected' and asp.Payment_Type='Supplier' group by Tran_No,asl.request_No,asp.Item_Code,asp.Payment_Type,asp.Date)i order by i.date", con);

            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                gvcredits.DataSource = ds.Tables["fill"];
                gvcredits.DataBind();
            }
            else
            {
                gvcredits.EmptyDataText = "No Data avaliable";
                gvcredits.DataSource = null;
                gvcredits.DataBind();
                //btn.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void gvcredits_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Creditid"] = gvcredits.SelectedValue.ToString();
            ViewState["PaymentType"] = this.gvcredits.SelectedRow.Cells[4].Text;

            if (ViewState["PaymentType"].ToString() == "BankPayment")
            {
                da = new SqlDataAdapter("sp_viewassetcredits", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", SqlDbType.VarChar).Value = gvcredits.SelectedValue.ToString();
                da.Fill(ds, "FillDetails");
                if (ds.Tables["FillDetails"].Rows.Count > 0)
                {
                    tblverifycredits.Visible = true;
                    tblverifysupplierandsp.Visible = false;
                    lbltranno.Text = ds.Tables["FillDetails1"].Rows[0].ItemArray[0].ToString();
                    lblitemcode.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString();
                    lblbuyername.Text = ds.Tables["FillDetails1"].Rows[0].ItemArray[1].ToString();
                    lblbuyeradd.Text = ds.Tables["FillDetails1"].Rows[0].ItemArray[2].ToString();
                    if (ds.Tables["FillDetails2"].Rows[0].ItemArray[0].ToString() == "BankPayment")
                    {
                        trbankpayment.Visible = true;
                        lblbank.Text = ds.Tables["FillDetails3"].Rows[0].ItemArray[1].ToString();
                        lbldate.Text = ds.Tables["FillDetails3"].Rows[0].ItemArray[3].ToString();
                        lblmodeofpay.Text = ds.Tables["FillDetails3"].Rows[0].ItemArray[2].ToString();
                        lblno.Text = ds.Tables["FillDetails3"].Rows[0].ItemArray[4].ToString();
                        txtdesc.Text = ds.Tables["FillDetails3"].Rows[0].ItemArray[5].ToString();
                        lblamount.Text = ds.Tables["FillDetails3"].Rows[0].ItemArray[0].ToString();
                    }

                }
                else
                {
                    tblverifycredits.Visible = false;
                    tblverifysupplierandsp.Visible = false;

                }

            }

            else if (ViewState["PaymentType"].ToString() == "Service Provider" || ViewState["PaymentType"].ToString() == "Supplier")
            {

                tblverifysupplierandsp.Visible = true;
                //da = new SqlDataAdapter("select Tran_No as id,asl.request_No,asp.Item_Code,asp.Payment_Type,sum(cast(asp.Amount as decimal(20,2)))as Amount,asp.Date from Asset_Sale asl join Asset_Payment asp on asl.request_No=asp.request_No where asp.Status='1' and asl.status!='Rejected' and asp.Payment_Type='" + ViewState["PaymentType"].ToString() + "' and Tran_No='" + ViewState["Creditid"].ToString() + "' group by Tran_No,asl.request_No,asp.Item_Code,asp.Payment_Type,asp.Date", con);
                da = new SqlDataAdapter("select Tran_No as id,asl.request_No,asp.Item_Code,asp.Payment_Type,sum(cast(asp.Amount as decimal(20,2)))as Amount,REPLACE(CONVERT(VARCHAR(11), asp.Date, 106), ' ', '-')as Date,asp.Name ,asl.Buyer_Name from Asset_Sale asl join Asset_Payment asp on asl.request_No=asp.request_No where asp.Status='1' and asl.status!='Rejected' and asp.Payment_Type='" + ViewState["PaymentType"].ToString() + "' and Tran_No='" + ViewState["Creditid"].ToString() + "' group by Tran_No,asl.request_No,asp.Item_Code,asp.Payment_Type,asp.Date,asp.Name,asl.Buyer_Name", con);
                da.SelectCommand.Parameters.AddWithValue("@id", ViewState["Creditid"].ToString());
                da.Fill(ds, "General");
                if (ds.Tables["General"].Rows.Count > 0)
                {
                    txttransaction.Text = ViewState["Creditid"].ToString();
                    txttype.Text = ViewState["PaymentType"].ToString();
                    txtname.Text = ds.Tables["General"].Rows[0][6].ToString();
                    txtbuyername.Text = ds.Tables["General"].Rows[0][7].ToString();
                    txtpaymentdate.Text = ds.Tables["General"].Rows[0][5].ToString();
                    txtrequestno.Text = ds.Tables["General"].Rows[0][1].ToString();
                    txtitemcode.Text = ds.Tables["General"].Rows[0][2].ToString();
                    txtdebitamount.Text = ds.Tables["General"].Rows[0][4].ToString();

                    da = new SqlDataAdapter("Select b.invoiceno,p.cc_code,p.dca_code,p.vendor_id,p.total,p.tds,p.retention,p.netamount,p.hold,b.Amount from pending_invoice p join Asset_Payment b on p.invoiceno=b.invoiceno where b.Tran_No='" + ViewState["Creditid"].ToString() + "'", con);
                    da.Fill(ds, "fillgrid");
                    grd.DataSource = ds.Tables["fillgrid"];
                    grd.DataBind();

                    da = new SqlDataAdapter("select request_no,Item_code,REPLACE(CONVERT(VARCHAR(11),BookValue_Date, 106), ' ', '-')as BookValue_Date,Selling_Amt,Balance_Amt,Actuall_Amt from asset_sale where request_no='" + ds.Tables["General"].Rows[0][1].ToString() + "' and item_code='" + ds.Tables["General"].Rows[0][2].ToString() + "'", con);
                    da.Fill(ds, "fillgridasset");
                    grdassetdetails.DataSource = ds.Tables["fillgridasset"];
                    grdassetdetails.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = String.Format("{0:#,##,##,###.00}", balance);
        }
    }  
    private decimal balance = (decimal)0.0;
    protected void btnapprove_Click(object sender, EventArgs e)
    {

        btnapprove.Visible = false;       
        try
        {
            if (ViewState["PaymentType"].ToString() == "BankPayment")
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Bank_Credit_AssetApproval";
                cmd.Parameters.AddWithValue("@Id", ViewState["Creditid"].ToString());
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                //string msg = "";
                if (msg == "Credited Successfully")
                {
                    JavaScript.UPAlertRedirect(Page, "Approved Successfully", "VerifyAssetSalePayment.aspx");
                }
                else
                {
                    JavaScript.UPAlert(Page, "Failed");
                }
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
    protected void btnreject_Click(object sender, EventArgs e)
    {

        btnapprove.Visible = false;
      
        try
        {
            if (ViewState["PaymentType"].ToString() == "BankPayment")
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Bank_Credit_AssetApprovalReject";
                cmd.Parameters.AddWithValue("@Id", ViewState["Creditid"].ToString());
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "Rejected Successfully")
                {
                    JavaScript.UPAlertRedirect(Page, "Rejected Successfully", "VerifyAssetSalePayment.aspx");
                }
                else
                {
                    JavaScript.UPAlert(Page, "Failed");
                }
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
    protected void btnasstApprove_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InvoiceDebitAsset";
            cmd.Parameters.AddWithValue("@RequestNo", txtrequestno.Text);
            cmd.Parameters.AddWithValue("@Item_code", txtitemcode.Text);
            cmd.Parameters.AddWithValue("@Transaction_no", txttransaction.Text);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "Sucessfull";
            con.Close();
            if (msg == "Sucessfull")
            {
                JavaScript.UPAlertRedirect(Page, "Approved Successfully", "VerifyAssetSalePayment.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, "Failed");
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
    protected void btnasstreject_Click(object sender, EventArgs e)
    {
        try
        {
            cmd.Connection = con;
            cmd = new SqlCommand("sp_ServiceProviderpaymentAsset_Rejectsp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", ViewState["Creditid"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Amount", txtdebitamount.Text);
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "Rejected";
            if (msg == "Rejected")
                JavaScript.UPAlertRedirect(Page, msg, "VerifyAssetSalePayment.aspx");
            else
                JavaScript.UPAlertRedirect(Page, msg, "VerifyAssetSalePayment.aspx");
          
            

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
}