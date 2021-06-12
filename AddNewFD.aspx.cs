using System;
using System.Collections.Generic;
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
using System.Drawing;

public partial class AddNewFD : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tblopenclose.Visible = false;
            tblintrest.Visible = false;
            tblbtn.Visible = false;
            trbalamt.Visible = false;
            
        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedValue == "Openclose")
        {

            if (ddltype.SelectedValue == "Open")
            {
                trbalamt.Visible = false;
                Trledger.Visible = true;
                FillGroups();
                txtfdrno.Visible = true;
                ddlfdrno.Visible = false;
                trviewpaymentdetails.Visible = false;
                trdeductionDetails.Visible = false;
                trmaturity.Visible = false;
                lblclosingdate.Visible = false;
                txtclosingdate.Visible = false;
                //txtfddate.Enabled = true;
                txtclosingdate.Enabled = false;
                txtfromdate.Enabled = true;
                txttodate.Enabled = true;
                txtrateofintrest.Enabled = true;
                txtamount.Enabled = true;
                thpaymendetails.InnerText = "Payment Details";
                txtfrombank.Visible = false;
                ddlfrom.Visible = true;
                Session["ServiceMethodFD"] = "Open";
                clear();
            }
            else if (ddltype.SelectedValue == "Closed" || ddltype.SelectedValue == "Partially")
            {
                trbalamt.Visible = true;
                Trledger.Visible = false;
                txtfdrno.Visible = false;
                ddlfdrno.Visible = true;
                trviewpaymentdetails.Visible = true;
                trdeductionDetails.Visible = true;
                trmaturity.Visible = true;
                lblclosingdate.Visible = true;
                txtclosingdate.Visible = true;
                //txtfddate.Enabled = false;
                txtclosingdate.Enabled = true;
                txtfromdate.Enabled = false;
                txttodate.Enabled = false;
                txtrateofintrest.Enabled = false;
                txtamount.Enabled = false;
                thpaymendetails.InnerText = "Reciept Details";
                txtfrombank.Visible = true;
                ddlfrom.Visible = false;
                if (ddltype.SelectedValue == "Closed")
                    Session["ServiceMethodFD"] = "Closed";
                if (ddltype.SelectedValue == "Partially")
                    Session["ServiceMethodFD"] = "Partially";
                ddlcheque.Visible = false;
                fillFD();
                clear();
            }         
            else
            {
                trbalamt.Visible = false;
                Trledger.Visible = false;
                txtfdrno.Visible = false;
                ddlfdrno.Visible = false;
                trviewpaymentdetails.Visible = false;
                trdeductionDetails.Visible = false;
                trmaturity.Visible = false;
                lblclosingdate.Visible = false;
                txtclosingdate.Visible = false;
                //txtfddate.Enabled = true;
                txtclosingdate.Enabled = false;
                txtfromdate.Enabled = true;
                txttodate.Enabled = true;
                txtrateofintrest.Enabled = true;
                txtamount.Enabled = true;
                thpaymendetails.InnerText = "Payment Details";
                txtfrombank.Visible = false;
                ddlfrom.Visible = true;
                Session["ServiceMethodFD"] = "Open";
                clear();
            }
        }
    }
    public void FillGroups()
    {
        try
        {
            da = new SqlDataAdapter("Select i.* from (Select  CONVERT(varchar(10), id)as id,Name from Sub_Group where status='3'union all select Group_id as id,group_name as Name from mastergroups where status='3')i order by i.Name asc", con);
            da.Fill(ds, "Subgroups");
            ddlsubgroup.DataTextField = "Name";
            ddlsubgroup.DataValueField = "id";
            ddlsubgroup.DataSource = ds.Tables["Subgroups"];
            ddlsubgroup.DataBind();
            ddlsubgroup.Items.Insert(0, "Select Sub-Groups");
        }
        catch (Exception ex)
        {

        }
    }
    public void clear()
    {
        if (rbtntype.SelectedValue == "Openclose")
        {
            txtfdrno.Text = "";
            ddlfdrno.SelectedValue = "Select FDR";
            //txtfddate.Text = "";
            txtclosingdate.Text = "";
            txtfromdate.Text = "";
            txttodate.Text = "";
            txtrateofintrest.Text = "";
            txtamount.Text = "";
            txtfrombank1.Text = "";
            txtpayment1.Text = "";
            txtcheque1.Text = "";
            txtdesc1.Text = "";
            txtamt1.Text = "";
            txtmaturity.Text = "";
            txtintrest.Text = "";
            gvanyother.DataSource = null;
            gvanyother.DataBind();
            txtdedvalue.Text = "";
            ddlfrom.SelectedIndex = 0;
            ddlpayment.SelectedIndex = 0;
            txtdate.Text = "";
            txtcheque.Text = "";
            txtdesc.Text = "";
            txtamt.Text = "";
            hfdedtotal.Value = "0";
            txtbalamt.Text = "";
            //ddlcheque.SelectedIndex = 0;
        }
    }
    protected void ddlfdrno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfdrno.SelectedValue != "Select FDR")
        {
            da = new SqlDataAdapter("SELECT distinct fc.fdr from fd_claim fc  where fc.status not in ('3','Rejected') and fc.fdr='" + ddlfdrno.SelectedValue + "'", con);
            da.Fill(ds, "check");
            if (ds.Tables["check"].Rows.Count==0)
            {
                da = new SqlDataAdapter("select top 1 af.id as AFID,bk.id as BKID,af.status,REPLACE(CONVERT(VARCHAR(11),af.date, 106), ' ', '-')as date,af.FDR,REPLACE(CONVERT(VARCHAR(11),af.Fromdate, 106), ' ', '-')as Fromdate,REPLACE(CONVERT(VARCHAR(11),af.todate, 106), ' ', '-')as Todate,replace(af.RateofIntrest,'.0000','.00')as Intrest,replace(af.Amount,'.0000','.00')as Amount,bk.bank_name,bk.ModeofPay,bk.PaymentType,REPLACE(CONVERT(VARCHAR(11),bk.date, 106), ' ', '-')as Bankdate,bk.no,bk.description,replace(bk.credit,'.0000','.00')as bankamt from AddFD af join bankbook bk on af.fdr=bk.fdr where af.FDR='" + ddlfdrno.SelectedValue + "' and bk.status='3' order by BKID asc", con);
                da.Fill(ds, "data");
                //txtfddate.Text = ds.Tables["data"].Rows[0]["date"].ToString();
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
                txtfrombank.Text = ds.Tables["data"].Rows[0]["bank_name"].ToString();
                da = new SqlDataAdapter("select replace(sum(amount),'.0000','.00') as amt from fd_claim where type='Principle' and fdr='" + ddlfdrno.SelectedValue + "' and status !='Rejected'", con);
                da.Fill(ds, "balamt");
                if (ds.Tables["balamt"].Rows[0]["amt"].ToString()!="NULL")
                {
                    string bal = ds.Tables["balamt"].Rows[0]["amt"].ToString();
                    if (bal == "")
                        bal = "0";
                    txtbalamt.Text = (Convert.ToDecimal(txtamount.Text)-Convert.ToDecimal(bal)).ToString();
                }
                else
                {
                    txtbalamt.Text = ds.Tables["data"].Rows[0]["Amount"].ToString();
                }
                
            }
            else
            {
                string msg = "FDR No:-  " + ddlfdrno.SelectedValue + "  is Under Processing";
                JavaScript.UPAlert(Page, msg);
                ddlfdrno.SelectedIndex = 0;
            }

        }
        else
        {
            clear();
        }
    }
    public void fillFD()
    {
        ds.Clear();
        da = new SqlDataAdapter("Select distinct FDR from AddFD where Approval_Status='3' and status in ('Open','Partially') ", con);
        da.Fill(ds, "FDRNos");
        if (ds.Tables["FDRNos"].Rows.Count > 0)
        {
            ddlfdrno.DataTextField = "FDR";
            ddlfdrno.DataValueField = "FDR";
            ddlfdrno.DataSource = ds.Tables["FDRNos"];
            ddlfdrno.DataBind();
            ddlfdrno.Items.Insert(0, "Select FDR");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                if (rbtntype.SelectedValue == "Openclose")
                {
                    if (ddltype.SelectedValue == "Open")
                    {
                        int result;
                        BankDateCheck objdate = new BankDateCheck();
                        if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedValue))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_CreateFD";
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("dd-MMM-yyyy"));
                            cmd.Parameters.AddWithValue("@Types", ddltype.SelectedValue);
                            if (ddltype.SelectedValue == "Open")
                                cmd.Parameters.AddWithValue("@Fdrno", txtfdrno.Text);
                            else if (ddltype.SelectedValue == "Closed")
                                cmd.Parameters.AddWithValue("@Fdrno", ddlfdrno.SelectedValue);
                            cmd.Parameters.AddWithValue("@FromDate", txtfromdate.Text);
                            cmd.Parameters.AddWithValue("@Todate", txttodate.Text);
                            cmd.Parameters.AddWithValue("@IntrestRate", txtrateofintrest.Text);
                            cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
                            cmd.Parameters.AddWithValue("@From", ddlfrom.SelectedValue);  //Bank Name
                            cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                            if (ddlpayment.SelectedItem.Text == "Cheque")
                            {
                                cmd.Parameters.AddWithValue("@No", ddlcheque.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@chequeid", ddlcheque.SelectedValue);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@No", ddlcheque.SelectedItem.Text);
                            }
                            cmd.Parameters.AddWithValue("@PaymentType", "FD");
                            cmd.Parameters.AddWithValue("@Bankdate", txtdate.Text);
                            //cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                            cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());

                            cmd.Parameters.AddWithValue("@LedgerName", txtledgername.Text);
                            if (int.TryParse(ddlsubgroup.SelectedValue, out result))
                            {
                                cmd.Parameters.AddWithValue("@Grouptype", "subgroup");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Grouptype", "mastergroup");
                            }
                            cmd.Parameters.AddWithValue("@Subgroup", ddlsubgroup.SelectedValue);
                            cmd.Parameters.AddWithValue("@BalanceDate", txtledbaldate.Text);
                            cmd.Parameters.AddWithValue("@OpeningBal", txtopeningbal.Text);
                            cmd.Parameters.AddWithValue("@BalanceAt", rbtnpaymenttype.SelectedItem.Text);
                            cmd.Connection = con;
                            con.Open();
                            string msg = cmd.ExecuteScalar().ToString();
                            //string msg = "";
                            if (msg == "Generated Successfully")
                            {
                                JavaScript.UPAlertRedirect(Page, "Submitted", "AddNewFD.aspx");
                            }
                            else
                            {
                                JavaScript.UPAlert(Page, msg);
                            }
                            con.Close();
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
                        }
                    }
                    else if (ddltype.SelectedValue == "Closed" || ddltype.SelectedValue == "Partially")
                    {
                        decimal maturity = Convert.ToDecimal(txtmaturity.Text);
                        decimal intrest = Convert.ToDecimal(txtintrest.Text);
                        decimal total = maturity + intrest;
                        if ((total > Convert.ToDecimal(txtdedvalue.Text)) && (Convert.ToDecimal(txtamt.Text) != 0))
                        {
                            string deductiondcas = "";
                            string deductionamt = "";
                            string deductionsdcas = "";
                            foreach (GridViewRow record in gvanyother.Rows)
                            {
                                if (rbtnothercharges.SelectedValue == "Yes")
                                {
                                    if (gvanyother != null)
                                    {
                                        if ((record.FindControl("chkSelectother") as CheckBox).Checked)
                                        {
                                            deductiondcas = deductiondcas + gvanyother.DataKeys[record.RowIndex]["dca_code"].ToString() + ",";
                                            deductionsdcas = deductionsdcas + (record.FindControl("ddlsdca") as DropDownList).SelectedValue + ",";
                                            deductionamt = deductionamt + (record.FindControl("txtotheramount") as TextBox).Text + ",";
                                        }
                                        else
                                        {
                                            JavaScript.UPAlert(Page, "Please Verify Deduction");
                                            break;
                                        }
                                    }
                                }
                            }
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_ClaimFD";
                            cmd.Parameters.AddWithValue("@Date", txtclosingdate.Text);
                            cmd.Parameters.AddWithValue("@Fdrno", ddlfdrno.SelectedValue);
                            cmd.Parameters.AddWithValue("@Principle", txtmaturity.Text);
                            cmd.Parameters.AddWithValue("@Intrest", txtintrest.Text);
                            cmd.Parameters.AddWithValue("@Amount", txtamt.Text);

                            cmd.Parameters.AddWithValue("@From", txtfrombank.Text);  //Bank Name
                            cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@Bankdate", txtdate.Text);
                            cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                            cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());

                            cmd.Parameters.AddWithValue("@Deductiondcas", deductiondcas);
                            cmd.Parameters.AddWithValue("@Deductionsdcas", deductionsdcas);
                            cmd.Parameters.AddWithValue("@Deductiondcaamts", deductionamt);

                            cmd.Parameters.AddWithValue("@Status", ddltype.SelectedValue);

                            cmd.Connection = con;
                            con.Open();
                            string msg = cmd.ExecuteScalar().ToString();
                            //string msg = "";
                            if (msg == "Generated Successfully")
                            {
                                JavaScript.UPAlertRedirect(Page, "Submitted", "AddNewFD.aspx");
                            }
                            else
                            {
                                JavaScript.UPAlert(Page, msg);
                            }
                            con.Close();
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Amount Con not in negative");
                        }
                    }
                }

                else if (rbtntype.SelectedValue == "Intrest")
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_FDIntrest";                    
                    cmd.Parameters.AddWithValue("@Fdrno", ddlfdrnoInt.SelectedValue);
                    cmd.Parameters.AddWithValue("@Date", txtintdate.Text);
                    cmd.Parameters.AddWithValue("@IntAmount", txtintrestamt.Text);
                    cmd.Parameters.AddWithValue("@DedCCode", ddlcccodeded.SelectedValue);
                    cmd.Parameters.AddWithValue("@DedDCACode", ddldcaded.SelectedValue);
                    cmd.Parameters.AddWithValue("@DedSdcaCode", ddlsdcacodeded.SelectedValue);
                    cmd.Parameters.AddWithValue("@DedAmount", txtdedamount.Text);
                    cmd.Parameters.AddWithValue("@From", ddldedfrombank.SelectedValue);  //Bank Name
                    cmd.Parameters.AddWithValue("@ModeOfPay", ddlmodeofpay.SelectedItem.Text);                   
                    cmd.Parameters.AddWithValue("@PaymentType", "FDIntrest");
                    cmd.Parameters.AddWithValue("@Bankdate", txtbankdate.Text);
                    cmd.Parameters.AddWithValue("@No", txtno.Text);
                    cmd.Parameters.AddWithValue("@Description", txtdeddescription.Text);
                    cmd.Parameters.AddWithValue("@BankAmount", txttotalamt.Text);
                    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                    cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    //string msg = "";
                    if (msg == "Generated Successfully")
                    {
                        JavaScript.UPAlertRedirect(Page, "Submitted", "AddNewFD.aspx");
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, msg);
                    }
                    con.Close();
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddNewFD.aspx");
    }

    protected void rbtnothercharges_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnothercharges.SelectedIndex == 0)
        {

            tdaddanyother.Visible = true;
            tdddlanyotherdcas.Visible = true;
            TextBox1.Enabled = true;
            txtaddothers.Enabled = true;
            fillanyotherdcas();
        }
        else
        {
            tdddlanyotherdcas.Visible = true;
            tdaddanyother.Visible = true;
            tranyothergrid.Visible = false;
            TextBox1.Enabled = false;
            txtaddothers.Enabled = false;
            TextBox1.Text = "";
            gvanyother.DataSource = null;
            gvanyother.DataBind();
            //hfothers.Value = "0";
        }
        txtdedvalue.Text = "0";
        hfdedtotal.Value = "0";
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        //checkescisevat();
    }
    public void fillanyotherdcas()
    {
        if (rbtnothercharges.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("select distinct dc.dca_code, (dc.mapdca_code+' , '+dc.dca_name) as name  from yearly_dcabudget yd join dca dc on yd.dca_code=dc.dca_code where cc_code='CCC'", con);
            ds = new DataSet();
            da.Fill(ds, "fillothdcas");
            ddlanyotherdcas.DataSource = ds.Tables["fillothdcas"];
            ddlanyotherdcas.DataTextField = "name";
            ddlanyotherdcas.DataValueField = "dca_code";
            ddlanyotherdcas.DataBind();
        }        
    }
    protected void ddlanyotherdcas_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name = "";
        for (int i = 0; i < ddlanyotherdcas.Items.Count; i++)
        {
            if (ddlanyotherdcas.Items[i].Selected)
            {
                string s1 = ddlanyotherdcas.Items[i].Text;
                string[] DCACode = s1.Split(',');
                name += DCACode[0] + ",";
            }
        }
        TextBox1.Text = name;
        txtdedvalue.Text = "0";
        hfdedtotal.Value = "0";
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        //checkescisevat();
        //checkothertooltip();
    }
     protected void ddlanyotherdcas_DataBound(object sender, EventArgs e)
    {
        foreach (ListItem i in this.ddlanyotherdcas.Items)
        {
            string s = i.Text;       
            string[] words = s.Split(',');
            //foreach (string word in words)
            //{
            //    Console.WriteLine(word);
            //}
            da = new SqlDataAdapter("select dca_name from dca d join yearly_dcabudget yd on yd.dca_code=d.dca_code where mapdca_code='" + words[0] + "'", con);
            ds = new DataSet();
            da.Fill(ds,"namesother");
            i.Attributes.Add("Title", ds.Tables["namesother"].Rows[0].ItemArray[0].ToString());
        }
    }
     protected void Submit(object sender, EventArgs e)
     {
         string s1 = string.Empty;
         s1 = TextBox1.Text.Replace(",", "','");
         string s2 = s1.Substring(0, s1.Length - 3);
         fillgridsdca(s2);
         TextBox1.Text = "";
         txtdedvalue.Text = "0";
         hfdedtotal.Value = "0";
         ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);        

     }
     public void fillgridsdca(string dcas)
     {
         tranyothergrid.Visible = true;
         da = new SqlDataAdapter("select (mapdca_code+','+dca_name)as mapdca_code,dca_code  FROM dca where mapdca_code in ('" + dcas + "')", con);
         ds = new DataSet();
         da.Fill(ds);
         if (rbtnothercharges.SelectedIndex == 0 && TextBox1.Text != "")
         {
             gvanyother.DataSource = ds.Tables[0];
             gvanyother.DataBind();
         }
         else
         {
             gvanyother.DataSource = null;
             gvanyother.DataBind();
         }

     }
     protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             DataRowView rowView = (DataRowView)e.Row.DataItem;
             string DataKey = rowView["dca_code"].ToString();
             DropDownList sdca = (e.Row.FindControl("ddlsdca") as DropDownList);
             //da = new SqlDataAdapter("select subdca_code,mapsubdca_code from subdca where dca_code='" + DataKey + "'", con);
             da = new SqlDataAdapter("select (subdca_code+','+subdca_name)as mapsubdca_code,subdca_code from subdca where dca_code='" + DataKey + "'", con);
             ds = new DataSet();
             da.Fill(ds);
             sdca.DataSource = ds.Tables[0];
             sdca.DataTextField = "mapsubdca_code";
             sdca.DataValueField = "subdca_code";
             sdca.DataBind();
             sdca.Items.Insert(0, new ListItem("Select SDCA"));

         }
     }
     protected void ddlsdca_DataBound(object sender, EventArgs e)
     {
         DropDownList ddl = sender as DropDownList;
         if (ddl != null)
         {
             foreach (ListItem li in ddl.Items)
             {
                 li.Attributes["title"] = li.Text;
             }
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
     protected void ddlfrom_SelectedIndexChanged(object sender, EventArgs e)
     {
         ddlcheque.Items.Clear();
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
         else
         {
             ddlcheque.Items.Insert(0, "Select");
         }
     }

     protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (rbtntype.SelectedValue == "Openclose")
         {
             Trledger.Visible = false;
             tblopenclose.Visible = true;
             tblintrest.Visible = false;
             tblbtn.Visible = true;
             txtfdrno.Visible = false;
             ddlfdrno.Visible = false;
             trviewpaymentdetails.Visible = false;
             trdeductionDetails.Visible = false;
             trmaturity.Visible = false;
             lblclosingdate.Visible = false;
             txtclosingdate.Visible = false;
             thpaymendetails.InnerText = "Payment Details";
             txtfrombank.Visible = false;
             ddlfrom.Visible = true;
             ddlcheque.Visible = false;
             //Session["ServiceMethodFD"] = "Open";
         }
         else if (rbtntype.SelectedValue == "Intrest")
         {
             Trledger.Visible = false;
             tblopenclose.Visible = false;
             tblintrest.Visible = true;
             tblbtn.Visible = true;
             txtintdate.Enabled = true;
             fillintrestFD();
             fillinterestcc();
         }
         else
         {
             Trledger.Visible = false;
             tblopenclose.Visible = false;
             tblintrest.Visible = false;
             tblbtn.Visible = false;
         }
     }
     public void fillintrestFD()
     {

         ds.Clear();
         da = new SqlDataAdapter("Select distinct FDR from AddFD where Approval_Status='3' and status in ('Open','Partially') ", con);
         da.Fill(ds, "FDRNosint");
         if (ds.Tables["FDRNosint"].Rows.Count > 0)
         {
             ddlfdrnoInt.DataTextField = "FDR";
             ddlfdrnoInt.DataValueField = "FDR";
             ddlfdrnoInt.DataSource = ds.Tables["FDRNosint"];
             ddlfdrnoInt.DataBind();
             ddlfdrnoInt.Items.Insert(0, "Select FDR");
         }
     }
     public void fillinterestcc()
     {
         ddlcccodeded.Items.Clear();
         da = new SqlDataAdapter("Select cc_code,cc_name,(cc_code+','+cc_name) as [Name] from  cost_center where cc_code='CCC' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(CC_Code,'CC-','')) END ASC", con);
         da.Fill(ds, "CCTypeI");
         if (ds.Tables["CCTypeI"].Rows.Count > 0)
         {
             ddlcccodeded.DataTextField = "Name";
             ddlcccodeded.DataValueField = "cc_code";
             ddlcccodeded.DataSource = ds.Tables["CCTypeI"];
             ddlcccodeded.DataBind();
             ddlcccodeded.Items.Insert(0, "Select Cost Center");
         }
         else
         {
             ddlcccodeded.Items.Clear();
             ddlcccodeded.Items.Insert(0, "Select Cost Center");
         }
     }
     protected void ddlcccodeded_SelectedIndexChanged(object sender, EventArgs e)
     {
         try
         {
             if (txtintdate.Text != "")
             {
                 if (ddlcccodeded.SelectedIndex != 0)
                 {
                     txtintdate.Enabled = false;
                     ddldcaded.Items.Clear();
                     da = new SqlDataAdapter("TaxDcas_sp", con);
                     da.SelectCommand.CommandType = CommandType.StoredProcedure;
                     da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtintdate.Text;
                     da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccodeded.SelectedValue;
                     da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "FDIntrest";
                     da.Fill(ds, "dcaTypeI");
                     if (ds.Tables["dcaTypeI"].Rows.Count > 0)
                     {
                         ddldcaded.DataTextField = "name";
                         ddldcaded.DataValueField = "dca_code";
                         ddldcaded.DataSource = ds.Tables["dcaTypeI"];
                         ddldcaded.DataBind();
                         ddldcaded.Items.Insert(0, "Select DCA");
                     }
                     else
                     {
                         ddldcaded.Items.Clear();
                         ddldcaded.Items.Insert(0, "Select DCA");
                         ddlsdcacodeded.Items.Clear();
                         ddlsdcacodeded.Items.Insert(0, "Select SDCA");
                     }
                 }
                 else
                 {
                     txtintdate.Enabled = true;
                     ddlsdcacodeded.Items.Clear();
                     ddlsdcacodeded.Items.Insert(0, "Select SDCA");
                     ddldcaded.Items.Clear();
                     ddldcaded.Items.Insert(0, "Select DCA");
                 }
             }
             else
             {
                 ddlcccodeded.SelectedIndex = 0;
                 txtintdate.Enabled = true;
                 JavaScript.UPAlert(Page,"Please select date before select Cost center");
             }
         }
         catch (Exception ex)
         {
             Utilities.CatchException(ex);
         }
     }
     protected void ddldcaded_SelectedIndexChanged(object sender, EventArgs e)
     {
         try
         {
             ddlsdcacodeded.Items.Clear();
             da = new SqlDataAdapter("SELECT mapsubdca_code, subdca_code, (mapsubdca_code+' , '+subdca_name)as name from subdca  where dca_code='" + ddldcaded.SelectedValue + "' ", con);
             da.Fill(ds, "sdcaTypeI");
             if (ds.Tables["sdcaTypeI"].Rows.Count > 0)
             {

                 ddlsdcacodeded.DataTextField = "name";
                 ddlsdcacodeded.DataValueField = "subdca_code";
                 ddlsdcacodeded.DataSource = ds.Tables["sdcaTypeI"];
                 ddlsdcacodeded.DataBind();
                 ddlsdcacodeded.Items.Insert(0, "Select SDCA");
             }
             else
             {
                 ddlsdcacodeded.Items.Clear();
                 ddlsdcacodeded.Items.Insert(0, "Select SDCA");
             }
         }
         catch (Exception ex)
         {
             Utilities.CatchException(ex);
         }
     }
     protected void ddlfdrnoInt_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddlfdrnoInt.SelectedValue != "Select FDR")
         {
             da = new SqlDataAdapter("SELECT distinct fc.fdr from fd_claim fc  where fc.status not in ('3','Rejected') and fc.fdr='" + ddlfdrnoInt.SelectedValue + "'", con);
             da.Fill(ds, "checki");
             if (ds.Tables["checki"].Rows.Count > 0)
             {
                 string msg = "FDR No:-  " + ddlfdrnoInt.SelectedValue + "  is Under Processing";
                 JavaScript.UPAlert(Page,msg );
                 ddlfdrnoInt.SelectedIndex = 0;
             }
         }
           
     }
}