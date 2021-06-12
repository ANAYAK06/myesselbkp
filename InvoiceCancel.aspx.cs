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


public partial class InvoiceCancel : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filldata();
            tredit.Visible = false;
        }

    }

    public void filldata()
    {
        try
        {
            da = new SqlDataAdapter("Select InvoiceNo,PO_NO,Invoice_Date,RA_NO,replace(BasicValue,'.0000','.00')BasicValue,replace(ServiceTax,'.0000','.00')ServiceTax,replace(Exciseduty,'.0000','.00')Exciseduty,replace(EDcess,'.0000','.00')EDcess,replace(HEDcess,'.0000','.00')HEDcess,replace(Total,'.0000','.00')Total,CC_Code from invoice where status in ('1','2')", con);
            da.Fill(ds, "invoicedata");
            gridinvoice.DataSource = ds.Tables["invoicedata"];
            gridinvoice.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow record in gridinvoice.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                if (c1.Checked)
                {
                    string invoiceno = gridinvoice.DataKeys[record.RowIndex]["InvoiceNo"].ToString();
                    cmd.Connection = con;
                    cmd.CommandText = "update invoice set status='cancel' where InvoiceNo='" + invoiceno + "'";
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    con.Close();
                    if (i == 1)
                        JavaScript.AlertAndRedirect("Cancelled", "InvoiceCancel.aspx");
                       
                    else
                        JavaScript.UPAlert(Page,"Failed");
                  
                }
            }

            filldata();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void gridinvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string invoiceno = gridinvoice.DataKeys[e.NewEditIndex]["InvoiceNo"].ToString();
            ViewState["indentno"] = invoiceno;
            tredit.Visible = true;
            filltable();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void fillexcise()
    {

        try
        {
           if (ViewState["CCType"].ToString() == "Manufacturing" || ViewState["CCType"].ToString() == "Trading Supply")
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
            else     if (ViewState["CCType"].ToString() == "Service")
            {
                da = new SqlDataAdapter("select ServiceTaxno from [ServiceTaxMaster] where Status='3'", con);
                da.Fill(ds, "ServiceTax");

                ddlservicetax.DataValueField = ds.Tables["ServiceTax"].Columns["ServiceTaxno"].ToString();
                ddlservicetax.DataSource = ds.Tables["ServiceTax"];
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
    public void filltable()
    {
        try
        {
            int i=0;
            int j=0;
            int k = 0;
            da = new SqlDataAdapter("Select InvoiceNo,PO_NO,Invoice_Date,RA_NO,replace(BasicValue,'.0000','.00')BasicValue,replace(ServiceTax,'.0000','.00')ServiceTax,replace(EDcess,'.0000','.00')EDcess,replace(HEDcess,'.0000','.00')HEDcess,replace(Total,'.0000','.00')Total,(Select cc_subtype from cost_center where cc_code=i.cc_code),replace(Exciseduty,'.0000','.00')Exciseduty,replace(frieght,'.0000','.00')frieght,replace(insurance,'.0000','.00')insurance,replace(salestax,'.0000','.00')salestax,invoicetype,client_id,subclient_id,cc_code,PaymentType,REPLACE(CONVERT(VARCHAR(11),inv_making_date, 106), ' ', '-')as InvMakeDate from invoice i where status in ('1','2') and InvoiceNo='" + ViewState["indentno"].ToString() + "';select vatReg_no from vat_account where InvoiceNo='" + ViewState["indentno"].ToString() + "'; select ExciseReg_no from excise_account where InvoiceNo='" + ViewState["indentno"].ToString() + "'; select SrtxReg_No from ServiceTax_Account where InvoiceNo='" + ViewState["indentno"].ToString() + "'", con);
            da.Fill(ds, "fill");
            txtinvoice.Text = ds.Tables["fill"].Rows[0][0].ToString();
            txtpono.Text = ds.Tables["fill"].Rows[0][1].ToString();
            txtindt.Text = ds.Tables["fill"].Rows[0][2].ToString();
            txtindtmk.Text = ds.Tables["fill"].Rows[0][19].ToString();
            txtra.Text = ds.Tables["fill"].Rows[0][3].ToString();
            txtbasic.Text = ds.Tables["fill"].Rows[0][4].ToString();

            txted.Text = ds.Tables["fill"].Rows[0][6].ToString();
            txthed.Text = ds.Tables["fill"].Rows[0][7].ToString();
            txttotal.Text = ds.Tables["fill"].Rows[0][8].ToString();
            ViewState["CCType"] = ds.Tables["fill"].Rows[0][9].ToString();
            hf1.Value = ds.Tables["fill"].Rows[0][9].ToString();
            hf2.Value = ds.Tables["fill"].Rows[0].ItemArray[14].ToString();
            fillCC(ds.Tables["fill"].Rows[0][9].ToString());

            ViewState["InvoiceType"] = ds.Tables["fill"].Rows[0].ItemArray[14].ToString();



            ddlcccode.SelectedValue = ds.Tables["fill"].Rows[0].ItemArray[17].ToString();
            if (ViewState["CCType"].ToString() == "Service")
            {

                clientid("Invoice Service");
                
                if (ds.Tables["fill"].Rows[0].ItemArray[14].ToString() == "Service Tax Invoice")
                {
                    txttax.Enabled = true;
                    txted.Enabled = true;
                    txthed.Enabled = true;
                    Srtxregno.Visible = true;
                    fillexcise();
                    //ex.Visible = false;
                    txtex.Enabled = false;
                    txtfre.Enabled = false;
                    txtins.Enabled = false;
                    txtex.Text = "0";
                    txtfre.Text = "0";
                    txtins.Text = "0";


                    Label1.Text = "Service Tax";
                    Label4.Text = "Excise duty";
                    txttax.Text = ds.Tables["fill"].Rows[0][5].ToString();
                   
                    Exno.Visible = false;
                }
                if (ds.Tables["fill"].Rows[0].ItemArray[14].ToString() == "SEZ/Service Tax exumpted Invoice")
                {
                    txttax.Enabled = false;
                    txted.Enabled = false;
                    txthed.Enabled = false;
                    txttax.Text = "0";
                    txted.Text = "0";
                    txthed.Text = "0";
                    Srtxregno.Visible = false;
                    //ex.Visible = false;
                    txtex.Enabled = false;
                    txtfre.Enabled = false;
                    txtins.Enabled = false;
                    txtex.Text = "0";
                    txtfre.Text = "0";
                    txtins.Text = "0";
                    Label1.Text = "Service Tax";
                    Label4.Text = "Excise duty";
                    //txttax.Text = ds.Tables["fill"].Rows[0][5].ToString();                  
                }
                if (ds.Tables["fill"].Rows[0].ItemArray[14].ToString() == "VAT/Material Supply" || ds.Tables["fill"].Rows[0].ItemArray[18].ToString() == "Trading Supply")
                {
                    da = new SqlDataAdapter("select RegNo from [Saletax/VatMaster] where Status='3'", con);
                    da.Fill(ds, "VATtax");
                    ddlvatno.DataValueField = "RegNo";
                    ddlvatno.DataTextField = "RegNo";
                    ddlvatno.DataSource = ds.Tables["VATtax"];
                    ddlvatno.DataBind();
                    ddlvatno.Items.Insert(0, "Select");

                    Label1.Text = "Sales/Vat Tax";
                    Label4.Text = "Service Tax";
                    txttax.Text = ds.Tables["fill"].Rows[0][13].ToString();
                    txtfre.Text = ds.Tables["fill"].Rows[0].ItemArray[11].ToString();
                    txtins.Text = ds.Tables["fill"].Rows[0].ItemArray[12].ToString();

                    Exno.Visible = true;
                    txttax.Enabled = true;
                    txted.Enabled = false;
                    txted.Text = "";
                    txthed.Text = "";
                    txthed.Enabled = false;
                    lblexno.Visible = false;
                    ddlExcno.Visible = false;
                    Srtxregno.Visible = false;
                    //ex.Visible = true;
                    txtex.Enabled = false;
                    txtex.Text = "";
                }         
               
            
            }

            else if (ViewState["CCType"].ToString() == "Manufacturing" || ViewState["CCType"].ToString() == "Trading Supply")
            {
                Exno.Visible = true;
                Srtxregno.Visible = false;
                //ex.Visible = true;
                fillexcise();
                lblexno.Visible = true;

                Label4.Text = "Excise duty";
                clientid(ViewState["CCType"].ToString());
                txttax.Text = ds.Tables["fill"].Rows[0][13].ToString();
                txtex.Text = ds.Tables["fill"].Rows[0].ItemArray[10].ToString();
                txtfre.Text = ds.Tables["fill"].Rows[0].ItemArray[11].ToString();
                txtins.Text = ds.Tables["fill"].Rows[0].ItemArray[12].ToString();
                Label1.Text = "Sales Tax";
            }

            ddlclientid.SelectedValue = ds.Tables["fill"].Rows[0].ItemArray[15].ToString();
            subclientid(ds.Tables["fill"].Rows[0].ItemArray[15].ToString());
            ddlsubclientid.SelectedValue = ds.Tables["fill"].Rows[0].ItemArray[16].ToString();
            if (ds.Tables["fill1"].Rows.Count > 0)
            {
                ddlvatno.SelectedItem.Text = ds.Tables["fill1"].Rows[0].ItemArray[0].ToString();
                txttax.Enabled = true;
                txted.Enabled = false;
                txthed.Enabled = false;
                txtex.Enabled = false;
                i = i + 1;
            }
            if (ds.Tables["fill2"].Rows.Count > 0)
            {

                ddlExcno.SelectedItem.Text = ds.Tables["fill2"].Rows[0].ItemArray[0].ToString();
                if (i == 0)
                {
                    txttax.Enabled = false;
                }
                txted.Enabled = true;
                txthed.Enabled = true;
                txtex.Enabled = true;
                j = j + 1;
            }
            if (ds.Tables["fill3"].Rows.Count > 0)
            {

                ddlservicetax.SelectedItem.Text = ds.Tables["fill3"].Rows[0].ItemArray[0].ToString();
                txttax.Enabled = true;
                txted.Enabled = true;
                txthed.Enabled = true;
                k = k + 1;
            }
            ddlExcno.Visible = true;
            ddlvatno.Visible = true;
            if (i == 0)
            {
                ddlvatno.Visible = false;
            }
            if (j == 0)
            {
                ddlExcno.Visible = false;
            }
          
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    public void fillCC(string CCType)
    {
        try
        {
            if (CCType.ToString() == "Service")
            {
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_subtype='Service'", con);
            }
            else if (CCType.ToString() == "Trading Supply")
            {
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_subtype='Trading'", con);
            }
            else if (CCType.ToString() == "Manufacturing")
            {
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center where cc_subtype='Manufacturing'", con);
            }
            else
            {
                da = new SqlDataAdapter("select cc_code,cc_code+' , '+cc_name+'' Name from cost_center", con);
                ex.Visible = true;
            }
            da.Fill(ds, "SERVICECC");
            ddlcccode.DataTextField = "Name";
            ddlcccode.DataValueField = "cc_code";
            ddlcccode.DataSource = ds.Tables["SERVICECC"];
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "Select Cost Center");

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

         
    }
    public void clientid(string InvoiceType)
    {
        ddlclientid.Items.Clear();
        try
        {
            //if (InvoiceType.ToString() == "Invoice Service")
            //{
            //    da = new SqlDataAdapter("Select client_id from client where SUBSTRING(client_id,1,2)='SC'", con);

            //}
            //else 
            if (InvoiceType.ToString() == "Trading Supply")
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

    public void subclientid(string clientid)
    {

        ddlsubclientid.Items.Clear();

        try
        {
            da = new SqlDataAdapter("Select subclient_id from subclient where client_id='" +clientid.ToString() + "' ", con);
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
    protected void btnedit_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_credit_pending";
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@CCCode", ddlcccode.SelectedValue);
            cmd.Parameters.AddWithValue("@clientid", ddlclientid.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@subclientid", ddlsubclientid.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@invoiceno", txtinvoice.Text);
            cmd.Parameters.AddWithValue("@originalinvoiceno", ViewState["indentno"].ToString());
            cmd.Parameters.AddWithValue("@PO_NO", txtpono.Text);
            cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
            cmd.Parameters.AddWithValue("@RA_NO", txtra.Text);
            cmd.Parameters.AddWithValue("@BasicValue", txtbasic.Text);
        
            cmd.Parameters.AddWithValue("@EDcess", txted.Text);
            cmd.Parameters.AddWithValue("@HEDcess", txthed.Text);
            cmd.Parameters.AddWithValue("@total", txttotal.Text);
            cmd.Parameters.AddWithValue("@InvoiceType", ViewState["InvoiceType"].ToString());
            cmd.Parameters.AddWithValue("@Type", "2");
            //New parameter created date 10-May-2016 Cr-ENH-MAR-010-2016 STARTS
            cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
            //New parameter created date 10-May-2016 Cr-ENH-MAR-010-2016 ENDS
            if (ViewState["CCType"].ToString() == "Manufacturing" || ViewState["CCType"].ToString() == "Trading Supply")
            {
             
                cmd.Parameters.AddWithValue("@ExciseDuty", txtex.Text);
                cmd.Parameters.AddWithValue("@Frieght", txtfre.Text);
                cmd.Parameters.AddWithValue("@Insurance", txtins.Text);
                cmd.Parameters.AddWithValue("@SalesTax", txttax.Text);
                cmd.Parameters.AddWithValue("@exciseno", ddlExcno.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@vatno", ddlvatno.SelectedItem.Text);  
            }
            else
            {
                if (ViewState["InvoiceType"].ToString() != "VAT/Material Supply")
                {
                    if (ViewState["InvoiceType"].ToString() == "Service Tax Invoice")
                    {
                        cmd.Parameters.AddWithValue("@Srtxno", ddlservicetax.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@ServiceTax", txttax.Text);
                    }
                    Exno.Visible = false;
                    ex.Visible = false;

                }
                if (ViewState["InvoiceType"].ToString() == "VAT/Material Supply")
                {
                    cmd.Parameters.AddWithValue("@vatno", ddlvatno.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Frieght", txtfre.Text);
                    cmd.Parameters.AddWithValue("@Insurance", txtins.Text);
                    cmd.Parameters.AddWithValue("@SalesTax", txttax.Text);
                }
              

            }
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Updated Sucessfully")
            {
                JavaScript.UPAlertRedirect(Page, msg, "InvoiceCancel.aspx");

            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void btncancelinedit_Click(object sender, EventArgs e)
    {
        tredit.Visible = false;
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {

        try
        {
            string S = "";
            string S1 = "";
            string S2 = "";
            string S3 = "";
            string S4 = "";
            S4 = "INSERT INTO Invoice_log ([InvoiceNo],[PO_NO],[RA_NO],[Invoice_Date],[CC_Code],[BasicValue],[ExciseDuty],[Frieght],[Insurance],[EDcess],[HEDcess],[ServiceTax],[SalesTax],[Total],[PaymentType],[Status],CreatedBy,client_id,subclient_id,InvoiceType) SELECT [InvoiceNo],[PO_NO],[RA_NO],[Invoice_Date],[CC_Code],[BasicValue],[ExciseDuty],[Frieght],[Insurance],[EDcess],[HEDcess],[ServiceTax],[SalesTax],[Total],[PaymentType],[Status],CreatedBy,client_id,subclient_id,InvoiceType FROM [Invoice] where InvoiceNo='" + ViewState["indentno"].ToString() + "'";
            S = "Delete from invoice where InvoiceNo='" + ViewState["indentno"].ToString() + "'";
            S1 = "Delete from ServiceTax_Account where invoiceno='" + ViewState["indentno"].ToString() + "'";
            S2 = "Delete from excise_account where invoiceno='" + ViewState["indentno"].ToString() + "'";
            S3 = "Delete from VAT_Account where invoiceno='" + ViewState["indentno"].ToString() + "'";
            cmd.CommandText = S4 + S + S1 + S2 + S3;
            cmd.Connection = con;
            con.Open();
            int i = Convert.ToInt32(cmd.ExecuteNonQuery().ToString());
            if (i > 0)
            {

                JavaScript.UPAlertRedirect(Page, "Deleted Sucessfully", "InvoiceCancel.aspx");
               
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
    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        subclientid(ddlclientid.SelectedItem.Text);
    }
 
    protected void gridinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (gridinvoice.SelectedIndex != -1)
            {
                string invoiceno = gridinvoice.SelectedValue.ToString();
                ViewState["indentno"] = invoiceno;
                tredit.Visible = true;
                filltable();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
}
