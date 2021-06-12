using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;


public partial class CreditPayments : System.Web.UI.Page
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
            fdr.Visible = false;
            trname.Visible = false;
            paymenttype();
            ddlcccodeint.Enabled = false;
            ddldcaint.Enabled = false;
            ddlsdcacodeint.Enabled = false;
            txtintrest.Enabled = false;
            txtintrest.Text = "";
            Trmisc.Visible = false;
            Tdmisc.Visible = false;
            trdeductioncontrols.Visible = false;
            trdedtitles.Visible = false;
            trdedheader.Visible = false;

        }
        if (txtchkdate.Text != "")
        {
            txtchkdate.Enabled = false;
        }
        else
        {
            txtchkdate.Enabled = true;
        }

    }
    private void paymenttype()
    {
        clear();
        ddltypeofpay.Items.Clear();
        ddlsubtype.Visible = false;

        paytype.Visible = true;
        ddltypeofpay.Items.Add("-Select-");
        ddltypeofpay.Items.Add("Refund");
        ddltypeofpay.Items.Add("Other Refunds");
        ddltypeofpay.Items.Add("Unsecured Loan");
        ddltypeofpay.Items.Add("Misc Taxable Receipt");
        //ddlcheque.Visible = false;
    }
    protected void ddltypeofpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        if (txtchkdate.Text != "")
        {
            if (ddltypeofpay.SelectedItem.Text == "Refund")
            {
                subtype();
                Trmisc.Visible = false;
                Tdmisc.Visible = false;
                fdr.Visible = false;
                ddlcccodepr.Enabled = true;
                ddldcapr.Enabled = true;
                ddlsdcacodepr.Enabled = true;
                txtprinciple.ReadOnly = false;
                trname.Visible = true;
                ddlsubtype.Visible = true;
                txtamt.Enabled = false;
                txtamt.Text = "";
                fillprinciplecc();
            }
            else if (ddltypeofpay.SelectedItem.Text == "Other Refunds")
            {
                fdr.Visible = false;
                Trmisc.Visible = false;
                Tdmisc.Visible = false;
                ddlcccodepr.Enabled = true;
                ddldcapr.Enabled = true;
                ddlsdcacodepr.Enabled = true;
                txtprinciple.ReadOnly = false;
                txtprinciple.Text = "";
                ddlselection.Enabled = true;
                trname.Visible = false;
                ddlsubtype.Visible = false;
                txtamt.Text = "";
                txtamt.Enabled = true;
                fillprinciplecc();
            }
            else if ((ddltypeofpay.SelectedItem.Text == "Unsecured Loan") || (ddltypeofpay.SelectedItem.Text == "Misc Taxable Receipt"))
            {
                fdr.Visible = false;
                Trmisc.Visible = false;
                Tdmisc.Visible = false;
                ddlcccodepr.Enabled = true;
                ddldcapr.Enabled = true;
                ddlsdcacodepr.Enabled = true;
                txtprinciple.ReadOnly = false;
                txtprinciple.Text = "";
                ddlselection.Enabled = true;
                ddlselection.SelectedValue = "Select";
                txtintrest.Text = "";
                txtintrest.ReadOnly = false;
                trname.Visible = true;
                ddlsubtype.Visible = true;
                ddlsubtype.Visible = false;
                txtamt.Enabled = true;
                txtamt.Text = "";
                fillprinciplecc();
                if (ddltypeofpay.SelectedItem.Text == "Misc Taxable Receipt")
                {
                    Trmisc.Visible = false;
                    Tdmisc.Visible = true;
                    //clientid();
                    //fillinterestdedcc();
                    //Tdmisc.Visible = true;
                    //trdeductioncontrols.Visible = true;
                    //trdedtitles.Visible = true;
                    //trdedheader.Visible = true;
                    //trintrestintcontrols.Visible = false;
                    //trintrestlables.Visible = false;
                    //trintrestselection.Visible = false;
                    //trintrestheader.Visible = false;

                }
            }
            else
            {
                fdr.Visible = false;
                ddlsubtype.Visible = false;
                trname.Visible = true;
                ddlsubtype.Visible = false;
                txtamt.ReadOnly = true;
            }
        }
        else
        {
            JavaScript.UPAlert(Page,"Please Select Date");
            ddltypeofpay.SelectedIndex = 0;
        }
    }
    public void clientid()
    {
        ddlclientid.Items.Clear();
        try
        {
            da = new SqlDataAdapter("select client_id,(client_name+'  ['+client_id+']')as name from client where status='2'", con);

            da.Fill(ds, "client");
            if (ds.Tables["client"].Rows.Count > 0)
            {
                ddlclientid.DataTextField = "name";
                ddlclientid.DataValueField = "client_id";
                ddlclientid.DataSource = ds.Tables["client"];
                ddlclientid.DataBind();
                ddlclientid.Items.Insert(0, "Select");
                ddlsubclientid.Items.Insert(0, "Select");
                
            }
            else
            {
                ddlclientid.Items.Insert(0, "Select");
                ddlsubclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        subclientid(ddlclientid.SelectedValue);
    }
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select branch from subclient where  subclient_id='" + ddlsubclientid.SelectedValue + "'", con);
            da.Fill(ds, "subclient1");
            if (ds.Tables["subclient1"].Rows.Count > 0)
            {
                txtname.Text = ds.Tables["subclient1"].Rows[0].ItemArray[0].ToString();
                txtname.Enabled = false;
            }
            else
            {
                txtname.Enabled = true;
                ddlsubclientid.Items.Insert(0, "Select");
            }
    }
    public void subclientid(string clientid)
    {
        ddlsubclientid.Items.Clear();
        try
        {
            da = new SqlDataAdapter("select subclient_id from subclient where  client_id='" + clientid + "'", con);
            da.Fill(ds, "subclient");
            if (ds.Tables["subclient"].Rows.Count > 0)
            {
                ddlsubclientid.DataTextField = "subclient_id";
                ddlsubclientid.DataValueField = "subclient_id";
                ddlsubclientid.DataSource = ds.Tables["subclient"];
                ddlsubclientid.DataBind();
                ddlsubclientid.Items.Insert(0, "Select");               
            }
            else
            {               
                ddlsubclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    private void subtype()
    {
        ddlsubtype.Visible = true;
        ddlsubtype.Items.Clear();
        ddlsubtype.Items.Add("-Select Refund Type-");
        ddlsubtype.Items.Add("FD");
        ddlsubtype.Items.Add("SD");
        ddlsubtype.ToolTip = "Refund Type";
    }
    protected void ddlsubtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsubtype.SelectedItem.Text == "FD")
        {
            ddlcccodepr.Enabled = true;
            ddldcapr.Enabled = true;
            ddlsdcacodepr.Enabled = true;
            txtprinciple.ReadOnly = false;
            fdr.Visible = true;
            ddlselection.Enabled = true;
            ddlcccodeint.Enabled = true;
            ddldcaint.Enabled = true;
            ddlsdcacodeint.Enabled = true;
            txtintrest.ReadOnly = false;
            trname.Visible = false;
        }
        else if (ddlsubtype.SelectedItem.Text == "SD")
        {
            ddlcccodepr.Enabled = true;
            ddldcapr.Enabled = true;
            ddlsdcacodepr.Enabled = true;
            txtprinciple.ReadOnly = false;
            fdr.Visible = false;
            ddlselection.Enabled = false;
            ddlcccodeint.Enabled = false;
            ddldcaint.Enabled = false;
            ddlsdcacodeint.Enabled = false;
            txtintrest.ReadOnly = true;
            trname.Visible = true;
            txtintrest.Text = "";
        }
        else
        {
            fdr.Visible = false;
            ddlselection.Enabled = false;
            ddlcccodeint.Enabled = false;
            ddldcaint.Enabled = false;
            ddlsdcacodeint.Enabled = false;
            txtintrest.ReadOnly = true;
            trname.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
    }
    public void fillprinciplecc()
    {
        ddlcccodepr.Items.Clear();
        da = new SqlDataAdapter("Select cc_code,cc_name,(cc_code+','+cc_name) as [Name] from  cost_center  GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
        da.Fill(ds, "CCType");
        if (ds.Tables["CCType"].Rows.Count > 0)
        {

            ddlcccodepr.DataTextField = "Name";
            ddlcccodepr.DataValueField = "cc_code";
            ddlcccodepr.DataSource = ds.Tables["CCType"];
            ddlcccodepr.DataBind();
            ddlcccodepr.Items.Insert(0, "Select Cost Center");
        }
        else
        {
            ddlcccodepr.Items.Insert(0, "Select Cost Center");
        }

    }
    protected void ddlcccodepr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddldcapr.Items.Clear();
            da = new SqlDataAdapter("TaxDcas_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtchkdate.Text;
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccodepr.SelectedValue;
            if (ddltypeofpay.SelectedValue == "Other Refunds")
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Refundsnew";
            else
            da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Refunds";
            //da = new SqlDataAdapter("SELECT distinct d.mapdca_code, d.dca_code,d.dca_name from dca d JOIN yearly_dcabudget yd ON d.dca_code=yd.dca_code AND d.dca_type='Expense' AND yd.cc_code='" + ddlcccodepr.SelectedValue + "' ", con);
            da.Fill(ds, "dcaType");
            if (ds.Tables["dcaType"].Rows.Count > 0)
            {
                
                ddldcapr.DataTextField = "mapdca_code";
                ddldcapr.DataValueField = "dca_code";
                ddldcapr.DataSource = ds.Tables["dcaType"];
                ddldcapr.DataBind();
                ddldcapr.Items.Insert(0, "Select DCA");
            }
            else
            {
                ddldcapr.Items.Insert(0, "Select DCA");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddldcapr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlsdcacodepr.Items.Clear();
            da = new SqlDataAdapter("SELECT distinct mapsubdca_code, subdca_code from subdca  where dca_code='" + ddldcapr.SelectedValue + "' ", con);
            da.Fill(ds, "sdcaType");
            if (ds.Tables["sdcaType"].Rows.Count > 0)
            {
                ddlsdcacodepr.DataTextField = "mapsubdca_code";
                ddlsdcacodepr.DataValueField = "subdca_code";
                ddlsdcacodepr.DataSource = ds.Tables["sdcaType"];
                ddlsdcacodepr.DataBind();
                ddlsdcacodepr.Items.Insert(0, "Select SDCA");
            }
            else
            {
                ddlsdcacodepr.Items.Insert(0, "Select SDCA");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    public void fillinterestcc()
    {
        ddlcccodeint.Items.Clear();
        da = new SqlDataAdapter("Select cc_code,cc_name,(cc_code+','+cc_name) as [Name] from  cost_center ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
        da.Fill(ds, "CCTypeI");
        if (ds.Tables["CCTypeI"].Rows.Count > 0)
        {
            
            ddlcccodeint.DataTextField = "Name";
            ddlcccodeint.DataValueField = "cc_code";
            ddlcccodeint.DataSource = ds.Tables["CCTypeI"];
            ddlcccodeint.DataBind();
            ddlcccodeint.Items.Insert(0, "Select Cost Center");
        }
        else
        {
            ddlcccodeint.Items.Insert(0, "Select Cost Center");
        }

    }
    protected void ddlcccodeint_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlselection.SelectedValue == "Yes")
            {
                ddldcaint.Items.Clear();
                //da = new SqlDataAdapter("SELECT distinct d.mapdca_code, d.dca_code,d.dca_name from dca d JOIN yearly_dcabudget yd ON d.dca_code=yd.dca_code AND d.dca_type='Expense' AND yd.cc_code='" + ddlcccodeint.SelectedValue + "' ", con);
                da = new SqlDataAdapter("TaxDcas_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtchkdate.Text;
                da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccodeint.SelectedValue;
                da.SelectCommand.Parameters.AddWithValue("@Taxtype", SqlDbType.VarChar).Value = "Refunds";
                da.Fill(ds, "dcaTypeI");
                if (ds.Tables["dcaTypeI"].Rows.Count > 0)
                {
                    
                    ddldcaint.DataTextField = "mapdca_code";
                    ddldcaint.DataValueField = "dca_code";

                    ddldcaint.DataSource = ds.Tables["dcaTypeI"];
                    ddldcaint.DataBind();
                    ddldcaint.Items.Insert(0, "Select DCA");
                }
                else
                {
                    ddldcaint.Items.Insert(0, "Select DCA");
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddldcaint_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlsdcacodeint.Items.Clear();
            da = new SqlDataAdapter("SELECT mapsubdca_code, subdca_code from subdca  where dca_code='" + ddldcaint.SelectedValue + "' ", con);
            da.Fill(ds, "sdcaTypeI");
            if (ds.Tables["sdcaTypeI"].Rows.Count > 0)
            {
               
                ddlsdcacodeint.DataTextField = "mapsubdca_code";
                ddlsdcacodeint.DataValueField = "subdca_code";
                ddlsdcacodeint.DataSource = ds.Tables["sdcaTypeI"];
                ddlsdcacodeint.DataBind();
                ddlsdcacodeint.Items.Insert(0, "Select SDCA");
            }
            else
            {
                ddlsdcacodeint.Items.Insert(0, "Select SDCA");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void clear()
    {
        //ddlsubtype.SelectedIndex = -1;
        ddlselection.SelectedIndex = -1;
        txtfdr.Text = "";
        ddlcccodepr.Items.Clear();
        ddldcapr.Items.Clear();
        ddlsdcacodepr.Items.Clear();
        ddlcccodepr.Items.Insert(0, "Select Cost Center");
        ddldcapr.Items.Insert(0, "Select DCA");
        ddlsdcacodepr.Items.Insert(0, "Select SDCA");
        txtprinciple.Text = "";
        ddlcccodeint.Items.Clear();
        ddldcaint.Items.Clear();
        ddlsdcacodeint.Items.Clear();
        ddlcccodeint.Items.Insert(0, "Select Cost Center");
        ddldcaint.Items.Insert(0, "Select DCA");
        ddlsdcacodeint.Items.Insert(0, "Select SDCA");
        txtintrest.Text = "";
        txtname.Text = "";
        ddlfrom.SelectedIndex = -1;
        ddlpayment.SelectedIndex = -1;
        txtdate.Text = "";
        txtcheque.Text = "";
        txtdesc.Text = "";
        txtamt.Text = "";
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string category = "";
        BankDateCheck objdate = new BankDateCheck();
        if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedItem.Text))
        {
            btnsubmit.Visible = false;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (ddltypeofpay.SelectedItem.Text == "Refund")
                {
                    category = ddlsubtype.SelectedItem.Text;
                }
                else
                {
                    category = ddltypeofpay.SelectedItem.Text;
                }
                cmd.CommandText = "sp_Bank_Credit_New";
                cmd.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Description", txtdesc.Text);
                cmd.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@No", txtcheque.Text);
                cmd.Parameters.AddWithValue("@Amount", txtamt.Text);
                cmd.Parameters.AddWithValue("@PaymentType", category);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@date", txtdate.Text);
                cmd.Parameters.AddWithValue("@datechk", txtchkdate.Text);
                if (category == "SD")
                {
                    cmd.Parameters.AddWithValue("@CC_Code", ddlcccodepr.SelectedValue);
                    cmd.Parameters.AddWithValue("@DCA_Code", ddldcapr.SelectedValue);
                    cmd.Parameters.AddWithValue("@Sub_DCA", ddlsdcacodepr.SelectedValue);
                    cmd.Parameters.AddWithValue("@Principle", txtprinciple.Text);
                    cmd.Parameters.AddWithValue("@Name", txtname.Text);
                }
                else if (category == "FD" || category == "Other Refunds")
                {
                    cmd.Parameters.AddWithValue("@FDR", txtfdr.Text);
                    cmd.Parameters.AddWithValue("@CC_Code", ddlcccodepr.SelectedValue);
                    cmd.Parameters.AddWithValue("@DCA_Code", ddldcapr.SelectedValue);
                    cmd.Parameters.AddWithValue("@Sub_DCA", ddlsdcacodepr.SelectedValue);
                    cmd.Parameters.AddWithValue("@Principle", txtprinciple.Text);
                    if (ddlselection.SelectedValue == "Yes")
                    {
                        cmd.Parameters.AddWithValue("@CC_CodeI", ddlcccodeint.SelectedValue);
                        cmd.Parameters.AddWithValue("@DCA_CodeI", ddldcaint.SelectedValue);
                        cmd.Parameters.AddWithValue("@Sub_DCAI", ddlsdcacodeint.SelectedValue);
                        cmd.Parameters.AddWithValue("@Interest", txtintrest.Text);
                    }

                }
                else if (category == "Unsecured Loan" || category == "Misc Taxable Receipt")
                {
                    if (category == "Misc Taxable Receipt")
                    {
                        if (ddlintresttype.SelectedValue == "1")
                        {
                            cmd.Parameters.AddWithValue("@Misctype", ddlintresttype.SelectedValue);
                            cmd.Parameters.AddWithValue("@ClientId", ddlclientid.SelectedValue);
                            cmd.Parameters.AddWithValue("@SubclientId", ddlsubclientid.SelectedValue);
                            cmd.Parameters.AddWithValue("@DedCCode", ddlcccodeded.SelectedValue);
                            cmd.Parameters.AddWithValue("@DedDCACode", ddldcaded.SelectedValue);
                            cmd.Parameters.AddWithValue("@DedSdcaCode", ddlsdcacodeded.SelectedValue);
                            cmd.Parameters.AddWithValue("@DedAmount", txtdedamount.Text);
                        }
                    }
                    cmd.Parameters.AddWithValue("@CC_Code", ddlcccodepr.SelectedValue);
                    cmd.Parameters.AddWithValue("@DCA_Code", ddldcapr.SelectedValue);
                    cmd.Parameters.AddWithValue("@Sub_DCA", ddlsdcacodepr.SelectedValue);
                    cmd.Parameters.AddWithValue("@Principle", txtprinciple.Text);
                    if (ddlselection.SelectedValue == "Yes")
                    {
                        cmd.Parameters.AddWithValue("@CC_CodeI", ddlcccodeint.SelectedValue);
                        cmd.Parameters.AddWithValue("@DCA_CodeI", ddldcaint.SelectedValue);
                        cmd.Parameters.AddWithValue("@Sub_DCAI", ddlsdcacodeint.SelectedValue);
                        cmd.Parameters.AddWithValue("@Interest", txtintrest.Text);
                    }
                    cmd.Parameters.AddWithValue("@Name", txtname.Text);
                }

                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "";
                if (msg == "Credit Submitted")
                {
                    JavaScript.UPAlertRedirect(Page, msg, "CreditPayments.aspx");
                }
                else
                {
                    JavaScript.UPAlert(Page, msg);
                    btnsubmit.Visible = true;
                }
                con.Close();
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
        else
        {
            JavaScript.UPAlert(Page, "Your seleced date is not before than the bank account opening date");
        }
    }
   
    protected void ddlselection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlselection.SelectedValue == "Yes")
        {
            ddlcccodeint.Enabled = true;
            ddldcaint.Enabled = true;
            ddlsdcacodeint.Enabled = true;           
            fillinterestcc();
            txtintrest.Enabled = true;
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        }
        if ((ddlselection.SelectedValue == "No") || (ddlselection.SelectedValue == "Select"))
        {
            ddlcccodeint.SelectedIndex = 0;
            ddldcaint.SelectedIndex = 0;
            ddlsdcacodeint.SelectedIndex = 0;          
            ddlcccodeint.Enabled = false;
            ddldcaint.Enabled = false;
            ddlsdcacodeint.Enabled = false;
            txtintrest.Enabled = false;
            txtintrest.Text = "";
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Total()", true);
        }
    }
    public void fillinterestdedcc()
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
            if (txtchkdate.Text != "")
            {
                if (ddlcccodeded.SelectedIndex != 0)
                {
                    txtchkdate.Enabled = false;
                    ddldcaded.Items.Clear();
                    da = new SqlDataAdapter("TaxDcas_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = txtchkdate.Text;
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
                    txtchkdate.Enabled = true;
                    ddlsdcacodeded.Items.Clear();
                    ddlsdcacodeded.Items.Insert(0, "Select SDCA");
                    ddldcaded.Items.Clear();
                    ddldcaded.Items.Insert(0, "Select DCA");
                }
            }
            else
            {
                ddlcccodeded.SelectedIndex = 0;
                txtchkdate.Enabled = true;
                JavaScript.UPAlert(Page, "Please select date before select Cost center");
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
    protected void ddlintresttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        clientid();
        fillinterestdedcc();
        Tdmisc.Visible = true;
        txtamt.Text = "";
        if (ddlintresttype.SelectedValue == "1")
        {
           
            Trmisc.Visible = true;
            trdeductioncontrols.Visible = true;
            trdedtitles.Visible = true;
            trdedheader.Visible = true;

            trintrestintcontrols.Visible = false;
            trintrestlables.Visible = false;
            trintrestselection.Visible = false;
            trintrestheader.Visible = false;
        }
        else
        {
            Trmisc.Visible = false;
            trdeductioncontrols.Visible = false;
            trdedtitles.Visible = false;
            trdedheader.Visible = false;

            trintrestintcontrols.Visible = true;
            trintrestlables.Visible = true;
            trintrestselection.Visible = true;
            trintrestheader.Visible = true;
        }
    }
}