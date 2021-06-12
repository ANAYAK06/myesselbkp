   
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
using AjaxControlToolkit;


public partial class frmpendinginvoice : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataAdapter da = null;

    string cccode,  invoice, date, pono, rano;    
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");       
       
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
       
        if (!IsPostBack)
        {
            paytype.Visible = false;
            trcredit.Visible = false;
            trdebit.Visible = false;
          
            Invoice.Visible = false;
            paybill.Visible = false;
            btn.Visible = false;
            name.Visible = false;
            Tr1.Visible = false;
            Srtxregno.Visible = false;           
            trnonmanufaturing.Visible = false;
        }

        grid.Visible = false;
     }

    [WebMethod]
    public static string IsvendorAvailable(string vendor)
    {
        SqlConnection conwb = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlCommand cmdwb = new SqlCommand("Select vendor_name from vendor where vendor_id='" + vendor + "'", conwb);
        SqlDataReader drwb;
        cmdwb.Connection = conwb;
        conwb.Open();
        drwb = cmdwb.ExecuteReader();
        drwb.Read();
        if (drwb.HasRows)
        {
            string vendorname = drwb["vendor_name"].ToString();
            return vendorname;
        }
        else
        {
            string vendorname = "";
            return vendorname;
        }
    }
    protected void ddltypeofpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedIndex == 0)
        {
            cascadingDCA cs = new cascadingDCA();
          
            cs.Paymenttype(ddltypeofpay.SelectedItem.Text);
            LoadForm(ddltypeofpay.SelectedItem.Text);
           
            fillCC();          
        }
        else
        {
            LoadForm(ddltypeofpay.SelectedItem.Text);
        }
       
    }

    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedIndex == 0)
        {
            CleartextBoxes();
            paymenttype();
            ddlclientid.Items.Clear();
            ddlsubclientid.Items.Clear();
        }
        else if (rbtntype.SelectedIndex == 1)
        {
            CleartextBoxes();
            Srtxregno.Visible = false;
            Tr1.Visible = false;
           
            paymenttype();
            ddlclientid.Items.Clear();
            ddlsubclientid.Items.Clear();

        }
    }

    private void paymenttype()
    {
        
        ddltypeofpay.Items.Clear();
        ddlvendor.Visible = false;
        ddlpo.Visible = false;
        paytype.Visible = true;
        ddltypeofpay.Visible = true;
        trdebit.Visible = false;
        trcredit.Visible = false;
        Invoice.Visible = false;
        name.Visible = false;
        btn.Visible = false;
        if (rbtntype.SelectedIndex == 0)
        {
            ddltypeofpay.Items.Add("-Select-");
            ddltypeofpay.Items.Add("Invoice Service");
            ddltypeofpay.Items.Add("Trading Supply");
            ddltypeofpay.Items.Add("Manufacturing");
         
            ddltype.Visible = false;
            lblpo1.Visible = true;
            txtpo.Visible = false;
            ddlpono.Visible = true;
           
            ddlvendor.Visible = false;
            ddlpo.Visible = false;
        }
        else if (rbtntype.SelectedIndex == 1)
        {            
            ddltypeofpay.Items.Add("-Select-");
            ddltypeofpay.Items.Add("Service Provider");
            //ddltypeofpay.Items.Add("Service Tax");
            //ddltypeofpay.Items.Add("Trading Service Tax");
            //ddltypeofpay.Items.Add("Manufacturing Service Tax");
            //ddltypeofpay.Items.Add("Service provider VAT");
            ddltypeofpay.Items.Add("Salary");
            ddltype.Visible = false;
            ddlpono.Visible = false;
            lblpo1.Visible = false;
            txtpo.Visible = false;            
        }
      
    }

    private void LoadForm(string formtype)
     {
        
        trcredit.Visible = false;
        if (formtype == "Invoice Service")
        {
            clientid();
            fillexcise();
            ddltype.Visible = true;
            trdebit.Visible = true;
            cc.Visible = true;
            Invoice.Visible = true;
            txtpo.Visible = false;
            ddlpono.Visible = true;
            ddlpono.Items.Clear();
            txtin.Visible = true;
            txtra.Visible = true;
            lblrano.Visible = true;
            grid.Visible = false;
            ddlpo.Visible = false;
            ex.Visible = false;
            basic.Visible = true;
            ed.Visible = true;
            SPBasic.Visible = false;
            sppoinvoice.Visible = false;
            deductionhead.Visible = true;
            deductionbody1.Visible = true;
            deductionbody2.Visible = true;
            name.Visible = true;
            btn.Visible = true;
            txtbasic.Visible = true;
            Label2.Visible = true;
        }
      
        else if (formtype == "Manufacturing" || formtype == "Trading Supply")
        {
            trnonmanufaturing.Visible = false;
            ddltype.Visible = false;
            trmanufaturing.Visible = true;
            sppoinvoice.Visible = false;
            clientid();
            fillexcise();
            trdebit.Visible = true;

            txtmex.Enabled = true;
            txtmed.Enabled = true;
            txtmhed.Enabled = true;
            Label13.Text = "Excise duty";
            cc.Visible = true;
            Invoice.Visible = true;
            ddlpo.Visible = false;
            SPBasic.Visible = false;
          
            grid.Visible = false;
            txtpo.Visible = false;
            txtin.Visible = true;
            ddlpono.Visible = true;
            ddlpono.Items.Clear();
            
          
         
            deductionhead.Visible = true;
            deductionbody1.Visible = true;
            deductionbody2.Visible = true;
            name.Visible = true;
            btn.Visible = true;
         
        }
        else if (formtype == "Service Provider")
        {
            laodvendor(ddltypeofpay.SelectedItem.Text);
            txttax.ToolTip = lbltax.Text = (ddltypeofpay.SelectedItem.Text == "Service Provider") ? "Service Tax" : "Sales Tax";
            txtra.Visible = false;
            lblrano.Visible = false;
            ddlpo.Visible = true;
            Invoice.Visible = true;
            cc.Visible = true;
            basic.Visible = false;
            txtpo.Visible = true;
            grid.Visible = false;
            txtin.Visible = true;
            name.Visible = true;
            lblpo1.Visible = false;
            txtpo.Visible = false;
            ddlvendor.Visible = true;
            ed.Visible = false;
            Tr1.Visible = false;
            Srtxregno.Visible = false;
            SPBasic.Visible = true;
            deductionhead.Visible = true;
            deductionbody1.Visible = true;
            deductionbody2.Visible = true;
            name.Visible = true;
            btn.Visible = true;
            trnonmanufaturing.Visible = true;
            trmanufaturing.Visible = false;
            sppoinvoice.Visible = true;
            //ddlspservice.Enabled = false;
            //ddlSPExcno.Enabled = false;
            //ddlSPvatno.Enabled = false;

            txtSPbasic.ToolTip = Label3.Text = "Basic Value";
            if (formtype == "Service Provider")
                ex.Visible = false;
            else if (formtype == "Excise Duty")
                ex.Visible = true;
        }
        else if (formtype == "Salary")
        {
            cc.Visible = false;
            Invoice.Visible = false;
            name.Visible = false;
            btn.Visible = true;
            ddlvendor.Visible = false;
            grid.Visible = true;
            lblpo1.Visible = false;
            txtpo.Visible = false;
            ddlpo.Visible = false;
            trdebit.Visible = false;
            SPBasic.Visible = false;
            paybill.Visible = true;
            Tr1.Visible = false;
            Srtxregno.Visible = false;
            sppoinvoice.Visible = false;
            fillgrid();

        }
       
    }
    public void fillgrid()
    {
        da = new SqlDataAdapter("Select id,cc_code as [CCCode],bank_name,debit from bankbook where status='0' and paymenttype='Salary'", con);
        da.Fill(ds, "griddata");
        if (ds.Tables["griddata"].Rows.Count > 0)
        {
            GridView1.DataSource = ds.Tables["griddata"];
        }
        else
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CCCode");
            dt.Columns.Add("bank_name");
            dt.Columns.Add("debit");
            for (int i = 1; i <= 75; i++)
                dt.Rows.Add(i.ToString());
            GridView1.DataSource = dt;
        }
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            CascadingDropDown cc = (CascadingDropDown)e.Row.FindControl("CascadingDropDown4");
            HiddenField h1 = (HiddenField)e.Row.FindControl("h1");
            cc.SelectedValue = h1.Value;
            CascadingDropDown bank = (CascadingDropDown)e.Row.FindControl("CascadingDropDown9");
            HiddenField h2 = (HiddenField)e.Row.FindControl("h2");
            bank.SelectedValue = h2.Value;
        }
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

    private void laodvendor(string p)
    {
        ddlvendor.Visible = true;
        if (p == "Service Provider")
            da = new SqlDataAdapter("Select vendor_id,vendor_name+' ('+vendor_id+')' Name from vendor where vendor_type='Service Provider' and status='2' order by name asc", con);
      
        da.Fill(ds, "vendor");
        ddlvendor.DataTextField = "Name";
        ddlvendor.DataValueField = "vendor_id";
        ddlvendor.DataSource = ds.Tables["vendor"];
        ddlvendor.DataBind();
        ddlvendor.Items.Insert(0, "Select Vendor");
    }
    protected void btnsubmit_Click1(object sender, EventArgs e)
    {
        try
        {
            cmd.Connection = con;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            string category = ddltypeofpay.SelectedItem.Text;

            if (category == "Invoice Service" || category == "Trading Supply" || category == "Manufacturing")   //Credit Selection
            {

                cmd.CommandText = "sp_credit_pending";
                if (category == "Invoice Service")
                {
                    if (ddltype.SelectedItem.Text == "VAT/Material Supply")
                    {
                        cmd.Parameters.AddWithValue("@RA_NO", txtmra.Text);
                        cmd.Parameters.AddWithValue("@Salestax", txtmtax.Text);
                        cmd.Parameters.AddWithValue("@Frieght", txtmfre.Text);
                        cmd.Parameters.AddWithValue("@Insurance", txtmins.Text);
                        cmd.Parameters.AddWithValue("@Total", txtmtotal.Text);
                        cmd.Parameters.AddWithValue("@BasicValue", txtmbasic.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
                        cmd.Parameters.AddWithValue("@ServiceTax", txttax.Text);
                        cmd.Parameters.AddWithValue("@EDcess", txted.Text);
                        cmd.Parameters.AddWithValue("@HEDcess", txthed.Text);
                        cmd.Parameters.AddWithValue("@Total", txttotal.Text);
                        cmd.Parameters.AddWithValue("@RA_NO", txtra.Text);
                    }
                    cmd.Parameters.AddWithValue("@InvoiceType", ddltype.SelectedItem.Text);
                    if (ddlservicetax.SelectedItem.Text != "Select")
                    {
                        if (ddltype.SelectedItem.Text == "VAT/Material Supply")
                            cmd.Parameters.AddWithValue("@vatno", ddlservicetax.SelectedItem.Text);
                        else
                            cmd.Parameters.AddWithValue("@Srtxno", ddlservicetax.SelectedItem.Text);
                    }
                }
                else if (category == "Manufacturing" || category == "Trading Supply")
                {
                    cmd.Parameters.AddWithValue("@RA_NO", txtmra.Text);
                    cmd.Parameters.AddWithValue("@BasicValue", txtmbasic.Text);
                    cmd.Parameters.AddWithValue("@ExciseDuty", txtmex.Text);
                    cmd.Parameters.AddWithValue("@EDcess", txtmed.Text);
                    cmd.Parameters.AddWithValue("@HEDcess", txtmhed.Text);
                    cmd.Parameters.AddWithValue("@Salestax", txtmtax.Text);
                    cmd.Parameters.AddWithValue("@Frieght", txtmfre.Text);
                    cmd.Parameters.AddWithValue("@Insurance", txtmins.Text);
                    cmd.Parameters.AddWithValue("@Total", txtmtotal.Text);
                    if (ddlvatno.SelectedItem.Text != "Select")
                    {
                        cmd.Parameters.AddWithValue("@vatno", ddlvatno.SelectedItem.Text);
                    }
                    if (ddlExcno.SelectedItem.Text != "Select")
                    {
                        cmd.Parameters.AddWithValue("@exciseno", ddlExcno.SelectedItem.Text);
                    }

                }
                cmd.Parameters.AddWithValue("@InvoiceNo", txtin.Text);
                cmd.Parameters.AddWithValue("@CCCode", ddlcccode.SelectedValue);
                cmd.Parameters.AddWithValue("@PO_NO", ddlpono.SelectedValue);
                cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                cmd.Parameters.AddWithValue("@clientid", ddlclientid.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@subclientid", ddlsubclientid.SelectedItem.Text);

                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@Type", "1");
                cmd.Parameters.AddWithValue("@PaymentType", ddltypeofpay.SelectedItem.Text);
                //New parameter created date 10-May-2016 Cr-ENH-MAR-010-2016 STARTS
                cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                //New parameter created date 10-May-2016 Cr-ENH-MAR-010-2016 ENDS
                cmd.Connection = con;
                con.Open();

                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());

            }
            else if (category == "Service Provider")   //Debit Selection
            {
                cmd.CommandText = "sp_pending_invoice";

                if (category == "Service Provider")
                {
                    cmd.Parameters.AddWithValue("@BasicValue", txtSPbasic.Text);
                    cmd.Parameters.AddWithValue("@CCCode", lblccode.Text);
                    cmd.Parameters.AddWithValue("@Retention", txtretention.Text);
                    cmd.Parameters.AddWithValue("@Advance", txtadvance.Text);
                    cmd.Parameters.AddWithValue("@Hold", txthold.Text);
                    cmd.Parameters.AddWithValue("@AnyOther", txtother.Text);
                    cmd.Parameters.AddWithValue("@TDS", txttds.Text);
                    cmd.Parameters.AddWithValue("@Total", txttotal.Text);

                    cmd.Parameters.AddWithValue("@DCA_Code", lbldcacode.Text);
                    if (lblsdcacode.Text != "")
                        cmd.Parameters.AddWithValue("@Sub_DCA", lblsdcacode.Text);
                    if (ddlSPExcno.Enabled == true)
                    {
                        cmd.Parameters.AddWithValue("@SPExciseNo", ddlSPExcno.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SPExciseduty", txtspexices.Text);
                    }
                    if (ddlspservice.Enabled == true)
                    {
                        cmd.Parameters.AddWithValue("@SPServiceNo", ddlspservice.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SPServicetax", txtspservice.Text);
                    }
                    if (ddlSPvatno.Enabled == true)
                    {
                        cmd.Parameters.AddWithValue("@SPVatNo", ddlSPvatno.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SPVat", txtspsales.Text);
                    }

                }

                cmd.Parameters.AddWithValue("@SPEdc", txtspedc.Text);
                cmd.Parameters.AddWithValue("@SPHedc", txtspheds.Text);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@amount", h1.Value);
                cmd.Parameters.AddWithValue("@PO_NO", ddlpo.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
                cmd.Parameters.AddWithValue("@VendorId", ddlvendor.SelectedValue);
                cmd.Parameters.AddWithValue("@invoiceno", txtin.Text);
                cmd.Parameters.AddWithValue("@description", txtdesc.Text);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@PaymentType", ddltypeofpay.SelectedItem.Text);
                //New parameter created date 10-May-2016 Cr-ENH-MAR-010-2016 STARTS
                cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                //New parameter created date 10-May-2016 Cr-ENH-MAR-010-2016 ENDS
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "invalid";
                con.Close();
                if (msg == "Invoice Inserted")
                {
                    JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                    cleartext();
                }
                else
                {
                    JavaScript.UPAlert(Page, msg);
                    cleartext();
                    filltaxnums();
                    ddlSPExcno.Enabled = false;
                    ddlspservice.Enabled = false;
                    ddlSPvatno.Enabled = false;
                }


            }
            else if (category == "Salary")
            {
                int count = 0;
                string bankname = string.Empty;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if ((row.FindControl("ddlcccode1") as DropDownList).SelectedItem.Text != "Select Cost Center" && (row.FindControl("ddlfrom") as DropDownList).SelectedItem.Text != "Select" && (row.FindControl("txtamount") as TextBox).Text != "")
                    {

                        string bank = (row.FindControl("ddlfrom") as DropDownList).SelectedValue;
                        BankDateCheck objdate = new BankDateCheck();
                        if (!objdate.IsBankDateCheck(txtdate.Text, bank))
                        {
                            count = count + 1;
                            bankname = bankname +" "+ bank+",";
                        }
                    }
                }
           
                if (count == 0)
                {
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        if ((r.FindControl("ddlcccode1") as DropDownList).SelectedItem.Text != "Select Cost Center" && (r.FindControl("ddlfrom") as DropDownList).SelectedItem.Text != "Select" && (r.FindControl("txtamount") as TextBox).Text != "")
                        {
                            try
                            {
                                string ddlbank = (r.FindControl("ddlfrom") as DropDownList).SelectedValue;

                                string ddlcode = (r.FindControl("ddlcccode1") as DropDownList).SelectedValue;
                                string amount = (r.FindControl("txtamount") as TextBox).Text;
                                da = new SqlDataAdapter("If not exists(select * from bankbook where bank_name='" + ddlbank + "' and cc_code='" + ddlcode + "' and paymenttype='Salary' and status='0')select 'Sucess' else select 'Fail' ", con);
                                da.Fill(ds, "ifexist");

                                if (ds.Tables["ifexist"].Rows[0][0].ToString() == "Sucess")
                                {
                                    cmd = new SqlCommand("INSERT INTO [BankBook] ([Bank_Name],[Debit],[Date],[CC_Code],[DCA_code],[sub_DCA],[it_code],[paymenttype],[Status])VALUES (@bank_name,@Amount,@date,@CCCode,@DCA_Code,@Sub_DCA,@ITCode,@PaymentType,@status)", con);
                                    cmd.Parameters.AddWithValue("@CCCode", (r.FindControl("ddlcccode1") as DropDownList).SelectedValue);
                                    cmd.Parameters.AddWithValue("@bank_name", (r.FindControl("ddlfrom") as DropDownList).SelectedValue);
                                    cmd.Parameters.AddWithValue("@Amount", (r.FindControl("txtamount") as TextBox).Text);
                                    cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                                    cmd.Parameters.AddWithValue("@DCA_Code", "DCA-02");
                                    cmd.Parameters.AddWithValue("@Sub_DCA", "DCA-02 .2");
                                    cmd.Parameters.AddWithValue("@ITCode", "2.15");
                                    cmd.Parameters.AddWithValue("@PaymentType", ddltypeofpay.SelectedItem.Text);
                                    cmd.Parameters.AddWithValue("@status", "0");
                                    con.Open();
                                    bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                                    con.Close();
                                }
                                ds.Tables["ifexist"].Reset();
                            }
                            catch (Exception ex)
                            {
                                Utilities.CatchException(ex);
                                JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
                            }
                        }
                    }
                    cmd = new SqlCommand("Paybill_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                    cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                    cmd.Connection = con;
                    con.Open();
                    string msg = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if (msg == "Sucessfull")
                        JavaScript.UPAlertRedirect(Page, msg, Request.Url.ToString());
                    else
                        JavaScript.UPAlert(Page, msg);

                }
                else
                {
                    string[] items = bankname.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries);
                    var distinctArray = items.Distinct().ToArray();
                    JavaScript.UPAlert(Page, "Your seleced date is not before than the" +string.Join(",",distinctArray.ToArray()) + "bank account opening date");
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
   

    public void showalert(string message)
    {
        string script = "alert(\"" + message + "\");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
    }
   
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd.Connection = con;
            cmd.CommandText = "delete from bankbook where id='"+GridView1.DataKeys[e.RowIndex]["id"].ToString()+"'";
            con.Open();
           int i= cmd.ExecuteNonQuery();
            if (i == 1)
                JavaScript.UPAlert(Page,"Deleted");
            else
                JavaScript.UPAlert(Page, "Failed");
            con.Close();
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }


    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        trcredit.Visible = false;
        ddlpo.Visible = true;
        Vendorname();
        cleartext();
        filltaxnums();

        if (ddltypeofpay.SelectedItem.Text == "Service Provider" )
        {
            da = new SqlDataAdapter("Select distinct pono from SPPO where status='3'  and vendor_id='" + ddlvendor.SelectedValue + "'", con);
            da.Fill(ds, "fill");
            ddlpo.DataTextField = "pono";
            ddlpo.DataValueField = "pono";
            ddlpo.DataSource = ds.Tables["fill"];
            ddlpo.DataBind();
            ddlpo.Items.Insert(0, "Select PO");
        }       
    }
    protected void ddlpo_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddltypeofpay.SelectedItem.Text == "Service Provider")
        {
            trcredit.Visible = true;
            cleartext();
            da = new SqlDataAdapter("select s.cc_code,dca_code,subdca_code,vendor_name v,s.Balance,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date, c.cc_type, c.CC_SubType from   sppo s join  vendor v on s.vendor_id=v.vendor_id join Cost_Center c on s.cc_code=c.cc_code  where s.pono='" + ddlpo.SelectedItem.Text + "'", con);
            da.Fill(ds, "pono");
            if (ds.Tables["pono"].Rows.Count > 0)
            {
                lblccode.Text = ds.Tables["pono"].Rows[0][0].ToString();
                lbldcacode.Text = ds.Tables["pono"].Rows[0][1].ToString();
                lblsdcacode.Text = ds.Tables["pono"].Rows[0][2].ToString();
             
                hfdate.Value = ds.Tables["pono"].Rows[0][5].ToString();
                hfbalance.Value = ds.Tables["pono"].Rows[0][4].ToString();

                hfspcctype.Value = ds.Tables["pono"].Rows[0][6].ToString();
                hfspccsubtype.Value = ds.Tables["pono"].Rows[0][7].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "SPPOTax", "SPPOTax();", true);

                filltaxnums();
            }
        }       
        else
        {
            trcredit.Visible = false;
        }
       
    }
    public void Vendorname()
    {
        da = new SqlDataAdapter("Select vendor_name from vendor where vendor_id='"+ddlvendor.SelectedValue+"'", con);
        da.Fill(ds, "name");
        txtname.Text = ds.Tables["name"].Rows[0][0].ToString();
    }
    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        subclientid();
        clientname();
    }
    public void CleartextBoxes()
    {
        foreach (Control Cleartext in this.Controls)
        {

            if (Cleartext is TextBox)
            {

                ((TextBox)Cleartext).Text = string.Empty;

            }

        }

    } 
    public void clientid()
    {
        ddlclientid.Items.Clear();
        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Invoice Service")
            {
                da = new SqlDataAdapter("Select client_id from client where SUBSTRING(client_id,1,2)='SC'", con);
            }
            else if (ddltypeofpay.SelectedItem.Text == "Trading Supply")
            {
                da = new SqlDataAdapter("Select client_id from client where SUBSTRING(client_id,1,2)='TC'", con);
            }
            else
            {
                da = new SqlDataAdapter("Select client_id from client ", con);

            }
            da.Fill(ds, "client");
            if (ds.Tables["client"].Rows.Count > 0)
            {
                ddlclientid.DataTextField = "client_id";
                ddlclientid.DataValueField = "client_id";
                ddlclientid.DataSource = ds.Tables["client"];
                ddlclientid.DataBind();
                ddlclientid.Items.Insert(0, "Select");
            }
            else
            {
                ddlclientid.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }


    }

    public void subclientid()
    {

        ddlsubclientid.Items.Clear();

        try
        {
            da = new SqlDataAdapter("Select subclient_id from subclient where client_id='" + ddlclientid.SelectedItem.Text + "' ", con);
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
    public void clientname()
    {
        try
        {
            da = new SqlDataAdapter("Select client_name from client where client_id='"+ddlclientid.SelectedItem.Text+"'", con);
            da.Fill(ds, "clientname");
            da.Fill(ds, "clientname");
            if (ds.Tables["clientname"].Rows.Count > 0)
            {
                txtname.Text = ds.Tables["clientname"].Rows[0].ItemArray[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    public void fillCC()
    {
        if (rbtntype.SelectedIndex == 0)
        {
            if (ddltypeofpay.SelectedItem.Text != "Select")
            {
                if (rbtntype.SelectedIndex == 0)
                {
                    Tr1.Visible = true;

                }
                if (ddltypeofpay.SelectedItem.Text == "Invoice Service")
                {
                    txttax.ToolTip = lbltax.Text = (ddltypeofpay.SelectedItem.Text == "Invoice Service") ? "Service Tax" : "Sales Tax";

                    da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_subtype='Service' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
                    ex.Visible = false;
                }
                else if (ddltypeofpay.SelectedItem.Text == "Trading Supply")
                {
                    txttax.ToolTip = lbltax.Text = (ddltypeofpay.SelectedItem.Text == "Service") ? "Service Tax" : "Sales Tax";

                    da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_subtype='Trading' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
                    ex.Visible = false;
                }
                else if (ddltypeofpay.SelectedItem.Text == "Manufacturing")
                {
                    txttax.ToolTip = lbltax.Text = (ddltypeofpay.SelectedItem.Text == "Service") ? "Service Tax" : "Sales Tax";

                    da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_subtype='Manufacturing' GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
                    ex.Visible = true;
                }
                else
                {
                    da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center GROUP by cc_code,cc_name ORDER BY CASE WHEN cc_code='CCC' THEN cc_code end ASC ,CASE WHEN cc_code!='CCC' THEN CONVERT(INT,REPLACE(cc_code,'CC-','')) END ASC", con);
                    ex.Visible = true;
                }
                da.Fill(ds, "SERVICECC");
                ddlcccode.DataTextField = "Name";
                ddlcccode.DataValueField = "cc_code";
                ddlcccode.DataSource = ds.Tables["SERVICECC"];
                ddlcccode.DataBind();
                ddlcccode.Items.Insert(0, "Select Cost Center");

            }
            else
            {
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
            if (ddltypeofpay.SelectedItem.Text == "Manufacturing" || ddltypeofpay.SelectedItem.Text == "Trading Supply")
            {
                trmanufaturing.Visible = true;
                trnonmanufaturing.Visible = false;
                Tr1.Visible = true;
                Srtxregno.Visible = false;
            }
            else
            {
                trmanufaturing.Visible = false;
                trnonmanufaturing.Visible = true;
                Tr1.Visible = false;
                ddlservicetax.ToolTip = Label17.Text = "Service Tax No";
                Srtxregno.Visible = true;

            }
        }
    }
    public void fillexcise()
    {

        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Trading Supply" || ddltypeofpay.SelectedItem.Text == "Manufacturing")
            {

                da = new SqlDataAdapter("select Excise_no from ExciseMaster where Status='3';select RegNo from [Saletax/VatMaster] where Status='3'", con);
                da.Fill(ds, "Excise/VAT");

                if (ds.Tables["Excise/VAT"].Rows.Count > 0)
                {
                    ddlExcno.DataValueField = "Excise_no";
                    ddlExcno.DataTextField = "Excise_no";
                    ddlExcno.DataSource = ds.Tables["Excise/VAT"];
                    ddlExcno.DataBind();
                    ddlExcno.Items.Insert(0, "Select");
                }
                if (ds.Tables["Excise/VAT1"].Rows.Count > 0)
                {
                    ddlvatno.DataValueField = "RegNo";
                    ddlvatno.DataTextField = "RegNo";
                    ddlvatno.DataSource = ds.Tables["Excise/VAT1"];
                    ddlvatno.DataBind();
                    ddlvatno.Items.Insert(0, "Select");
                }
                else
                {
                    ddlExcno.Items.Insert(0, "Select");
                    ddlvatno.Items.Insert(0, "Select");
                }
            }
            else if (ddltypeofpay.SelectedItem.Text == "Invoice Service")
            {
                if (ddltype.SelectedItem.Text == "VAT/Material Supply")
                {
                    da = new SqlDataAdapter("select RegNo from [Saletax/VatMaster] where Status='3'", con);
                    da.Fill(ds, "VatTax");
                    ddlservicetax.DataValueField = ds.Tables["VatTax"].Columns["RegNo"].ToString();
                    ddlservicetax.DataSource = ds.Tables["VatTax"];
                }
                else
                {
                    da = new SqlDataAdapter("select ServiceTaxno from [ServiceTaxMaster] where Status='3'", con);
                    da.Fill(ds, "ServiceTax");
                    ddlservicetax.DataValueField = ds.Tables["ServiceTax"].Columns["ServiceTaxno"].ToString();
                    ddlservicetax.DataSource = ds.Tables["ServiceTax"];
                }
                ddlservicetax.DataBind();
                ddlservicetax.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }


    }

    public void filltaxnums()
    {
        try
        {
            if (ddltypeofpay.SelectedItem.Text == "Service Provider")
            {
                da = new SqlDataAdapter("select ServiceTaxno from [ServiceTaxMaster] where Status='3';select Excise_no from ExciseMaster where Status='3';select distinct RegNo from [Saletax/VatMaster] where Status='3'", con);
                da.Fill(ds, "ServiceTax");

                ddlspservice.DataValueField = ds.Tables["ServiceTax"].Columns["ServiceTaxno"].ToString();
                ddlspservice.DataSource = ds.Tables["ServiceTax"];
                ddlspservice.DataBind();
                ddlspservice.Items.Insert(0, "Select");

                ddlSPExcno.DataValueField = ds.Tables["ServiceTax1"].Columns["Excise_no"].ToString();
                ddlSPExcno.DataSource = ds.Tables["ServiceTax1"];
                ddlSPExcno.DataBind();
                ddlSPExcno.Items.Insert(0, "Select");

                ddlSPvatno.DataValueField = ds.Tables["ServiceTax2"].Columns["RegNo"].ToString();
                ddlSPvatno.DataSource = ds.Tables["ServiceTax2"];
                ddlSPvatno.DataBind();
                ddlSPvatno.Items.Insert(0, "Select");
            }             
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedItem.Text == "Service Tax Invoice")
        {
            fillexcise();
            trnonmanufaturing.Visible = true;
            trmanufaturing.Visible = false;
            txttax.Enabled = true;
            txted.Enabled = true;
            txthed.Enabled = true;
            ddlservicetax.ToolTip = Label17.Text = "Service Tax No";
            Srtxregno.Visible = true;
          
        }
        else if (ddltype.SelectedItem.Text == "SEZ/Service Tax exumpted Invoice")
        {
            trnonmanufaturing.Visible = true;
            trmanufaturing.Visible = false;
            txttax.Enabled = false;
            txted.Enabled = false;
            txthed.Enabled = false;
            Srtxregno.Visible = false;
           
        }
        else if (ddltype.SelectedItem.Text == "Select")
        {
            txtbasic.Text = "";
        }
        else
        {
            txtmex.Enabled = false;
            Label13.Text = "Service Tax";
            txtmed.Enabled = false;
            txtmhed.Enabled = false;
            fillexcise();
            trmanufaturing.Visible = true;
            trnonmanufaturing.Visible = false;
            ddlservicetax.ToolTip = Label17.Text = "VAT TIN No";
           
        }
    }
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        clientname();
        creditpono();
        txtadvance.Text = "";
    }
    public void creditpono()
    {
        ddlpono.Items.Clear();

        da = new SqlDataAdapter("select po_no from po where cc_code='" + ddlcccode.SelectedValue + "' and client_id='" + ddlclientid.SelectedValue + "' and status='3' and subclient_id='" + ddlsubclientid.SelectedValue + "' union  select c.po_no from contract c join po p on p.po_no=c.po_no where c.cc_code='" + ddlcccode.SelectedValue + "' ", con);
        da.Fill(ds, "PONO");
        if (ds.Tables["PONO"].Rows.Count > 0)
        {
            ddlpono.DataTextField = "po_no";
            ddlpono.DataValueField = "po_no";
            ddlpono.DataSource = ds.Tables["PONO"];
            ddlpono.DataBind();
            ddlpono.Items.Insert(0, "select po");
            clientname();
        }
        else
        {
            ddlpono.Items.Insert(0, "select po");
        }
    }
    protected void ddlpono_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select MobilisationAdv from po where po_no='"+ddlpono.SelectedValue+"'",con);
        da.Fill(ds,"MOb");
        if (ds.Tables["MOb"].Rows[0][0].ToString() == "No")
        {
            txtadvance.Enabled = false;
            txtadvance.Text = "0";
        }
        else
        {
            txtadvance.Enabled = true;
            da = new SqlDataAdapter("select isnull(sum(advance),0)totaldeduction from invoice where PaymentType!='Advance' and cc_code='" + ddlcccode.SelectedValue + "' and status not in ('cancel') and po_no='" + ddlpono.SelectedValue + "';select isnull(sum(total),0)totalcredit from invoice where PaymentType='Advance'  and cc_code='" + ddlcccode.SelectedValue + "' and po_no='" + ddlpono.SelectedValue + "'", con);
            da.Fill(ds, "total");
            if (ds.Tables["total"].Rows.Count > 0)
                hftotaldeduct.Value = ds.Tables["total"].Rows[0][0].ToString();
            else
                hftotaldeduct.Value = "0";
            if (ds.Tables["total1"].Rows.Count > 0)
                hftotalcredit.Value = ds.Tables["total1"].Rows[0][0].ToString();
            else
                hftotalcredit.Value = "0";          
        }
       
    }
    public void cleartext()
    {
        txtSPbasic.Text = "";
        txtspexices.Text = "";
        txtspservice.Text = "";
        txtspsales.Text = "";
        txtspedc.Text = "";
        txtspheds.Text = "";
        txtdesc.Text = "";
        txtamt.Text = "";
       
    }

    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        creditpono();
    }
}
