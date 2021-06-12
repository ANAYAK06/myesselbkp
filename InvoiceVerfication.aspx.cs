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


public partial class InvoiceVerfication : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    DataSet ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            fillgrid();
            filltaxnum();
            fillExnum();
            fillVatnum();
        }
    }
    

    public void fillgrid()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount from pending_invoice where status='1' and paymenttype not in ('Supplier','Excise Duty','Service provider VAT','Service Tax','VAT' ) order by id desc", con);
           
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("Select cc_code,description as [Description],id,invoice_date as [Date],dca_code,sub_dca,invoiceno,netamount from pending_invoice where ((status='2' and paymenttype in ('Service Provider')))  order by id desc", con);
            da.Fill(ds, "FillIn");
            GridView1.DataSource = ds.Tables["FillIn"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
       
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex != -1)
        {          
            string id = GridView1.SelectedValue.ToString();
            reset();
            Invoice(id);
            mdlpopbank.Show();
        }
    }


    protected void btninApprove_Click(object sender, EventArgs e)
    {
       
        try
        {
            cmd.Connection = con;
            cmd = new SqlCommand("sp_Approve_invoice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (txtvendortype.Text == "Service Provider")
            {

                cmd.Parameters.AddWithValue("@BasicValue", txtspbacis.Text);
                cmd.Parameters.AddWithValue("@TDS", txtsptds.Text);
                cmd.Parameters.AddWithValue("@Retention", txtsprention.Text);
                cmd.Parameters.AddWithValue("@Advance", txtspadvance.Text);
                cmd.Parameters.AddWithValue("@Hold", txtsphold.Text);
                cmd.Parameters.AddWithValue("@AnyOther", txtspanother.Text);
                cmd.Parameters.AddWithValue("@Total", txtspbacis.Text);
                cmd.Parameters.AddWithValue("@SpEdcess", txtserviceedcess.Text); 
                cmd.Parameters.AddWithValue("@SpHedess", txtservichedcess.Text);
                if (ddlspvat.Enabled == true)
                {
                cmd.Parameters.AddWithValue("@spvatno", ddlspvat.SelectedValue);
                cmd.Parameters.AddWithValue("@Spvat", txtspvat.Text);
                }
                if (ddlspexcise.Enabled == true)
                {
                cmd.Parameters.AddWithValue("@spexciseno", ddlspexcise.SelectedValue);
                cmd.Parameters.AddWithValue("@spexice", txtspexcise.Text);
                }
                if (ddlspservice.Enabled == true)
                {
                cmd.Parameters.AddWithValue("@spserviceno", ddlspservice.SelectedValue);
                cmd.Parameters.AddWithValue("@spservice", txtspserviceno.Text);
                }
                cmd.Parameters.AddWithValue("@spDescription", txtspdeccription.Text);
                cmd.Parameters.AddWithValue("@spnetamount", txtspnetamount.Text);
                //New parameter created date 19-May-2016 Cr-ENH-MAR-010-2016 STARTS
                cmd.Parameters.AddWithValue("@Inv_Making_Date", txtindtmk.Text);
                //New parameter created date 19-May-2016 Cr-ENH-MAR-010-2016 ENDS
                
            
            }
           
            else if (txtvendortype.Text == "Supplier")
            {
                cmd.Parameters.AddWithValue("@BasicValue", txtSupBasic.Text);
                cmd.Parameters.AddWithValue("@salestax", txtsalestax.Text);
                cmd.Parameters.AddWithValue("@TDS", txttds.Text);
                cmd.Parameters.AddWithValue("@Retention", txtretention.Text);
                cmd.Parameters.AddWithValue("@Advance", txtAdvance.Text);
                cmd.Parameters.AddWithValue("@Hold", txthold.Text);
                cmd.Parameters.AddWithValue("@AnyOther", txtother.Text);
                cmd.Parameters.AddWithValue("@Total", txttotal.Text);
              
            }

            else if (txtvendortype.Text == "Trading Service Tax")
            {
                cmd.Parameters.AddWithValue("@BasicValue", "0");
                cmd.Parameters.AddWithValue("@EDcess", txted.Text);
                cmd.Parameters.AddWithValue("@HEDcess", txthed.Text);
                cmd.Parameters.AddWithValue("@Total", txtnetAmount.Text);

                    cmd.Parameters.AddWithValue("@ServiceTaxNo", ddlservicetax.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@ServiceTax", txtservicetax.Text);

            }           
            cmd.Parameters.AddWithValue("@DCA_Code", ddlinvoicedca.Text);
            if (ddlinvoicesub.Text != "")
                cmd.Parameters.AddWithValue("@Sub_DCA", ddlinvoicesub.Text);
            cmd.Parameters.AddWithValue("@amount", txtnetAmount.Text);
            cmd.Parameters.AddWithValue("@name", txtvendorname.Text);
            cmd.Parameters.AddWithValue("@invoiceno", txtin.Text);
            cmd.Parameters.AddWithValue("@CCCode", ddlinvoicecc.Text);
            cmd.Parameters.AddWithValue("@PO_NO", txtpo.Text);
            cmd.Parameters.AddWithValue("@Invoice_Date", txtindt.Text);
            cmd.Parameters.AddWithValue("@Description", txtindesc.Text);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@VendorType", txtvendortype.Text);
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@VendorId", txtvendorid.Text);
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "inv";
            con.Close();
            JavaScript.UPAlertRedirect(Page, msg, "InvoiceVerfication.aspx");
           

            fillgrid();
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

    public static string serviceno = "";
    public static string exciseno = "";
    public void Invoice(string Vid)
    {
        string s = "";      
        da = new SqlDataAdapter("select invoiceno,invoice_date,cc_code,basicvalue,edcess,hedcess,servicetax,total,advance,hold,anyother,i.vendor_id,v.vendor_name,description,dca_code,sub_dca,salestax,po_no,paymenttype,tds,retention,description,netamount,(select cc_subtype from cost_center where cc_code in (Select cc_code from purchase_details where po_no=i.po_no)),exciseduty from pending_invoice i,vendor v where i.vendor_id= v.vendor_id and i.id=@id", con);
        da.SelectCommand.Parameters.AddWithValue("@id", Vid);
        da.Fill(ds, "pending");
        string no = ds.Tables["pending"].Rows[0].ItemArray[0].ToString();
        if (ds.Tables["pending"].Rows.Count > 0)
        {
            s = ds.Tables["pending"].Rows[0][18].ToString();
            if (s == "Service Provider")
            {

                da = new SqlDataAdapter("SELECT EDcess,HEDcess,ExciseDuty from pending_invoice where InvoiceNo='" + no + "/ED' and ExciseDuty is not null;SELECT EDcess,HEDcess,servicetax from pending_invoice where InvoiceNo='" + no + "/ST' and servicetax is not null;SELECT salestax from pending_invoice where InvoiceNo='" + no + "/VAT' and salestax is not null", con);
                da.Fill(ds, "invoiceno");

                if (ds.Tables["invoiceno"].Rows.Count > 0)
                {
                    txtserviceedcess.Enabled=true;
                    txtservichedcess.Enabled = true;
                    txtserviceedcess.Text = ds.Tables["invoiceno"].Rows[0]["EDcess"].ToString().Replace(".0000", ".00");
                    txtservichedcess.Text = ds.Tables["invoiceno"].Rows[0]["HEDcess"].ToString().Replace(".0000", ".00");
                    txtspexcise.Text = ds.Tables["invoiceno"].Rows[0]["ExciseDuty"].ToString().Replace(".0000", ".00");
                    da = new SqlDataAdapter("SELECT ExciseReg_No,Debit from excise_account where InvoiceNo='" + no + "/ED'", con);
                    da.Fill(ds, "excise");
                    if (ds.Tables["excise"].Rows.Count > 0)
                    {
                        ddlspexcise.Enabled = true;
                        txtspexcise.Enabled = true;
                        ddlspexcise.SelectedValue = ds.Tables["excise"].Rows[0]["ExciseReg_No"].ToString();
                        txtvendortype.Text = "Service Provider";
                        spexcise.Visible = true;
                        spservice.Visible = false;
                        ddlspvat.Enabled = false;
                        txtspvat.Enabled = false;
                    }
                    else
                    {
                        ddlspexcise.Enabled = false;
                        txtspexcise.Enabled = false;
                    }
                    if (ddlspexcise.SelectedIndex != 0)
                    {
                        txtserviceedcess.Enabled = true;
                        txtservichedcess.Enabled = true;
                    }
                    else
                    {
                        txtserviceedcess.Enabled = false;
                        txtservichedcess.Enabled = false;
                    }

                }
                else if (ds.Tables["invoiceno1"].Rows.Count > 0)
                {
                    txtserviceedcess.Enabled = true;
                    txtservichedcess.Enabled = true;
                    ddlspexcise.Enabled = false;
                    txtspexcise.Enabled = false;
                    txtserviceedcess.Text = ds.Tables["invoiceno1"].Rows[0]["EDcess"].ToString().Replace(".0000", ".00");
                    txtservichedcess.Text = ds.Tables["invoiceno1"].Rows[0]["HEDcess"].ToString().Replace(".0000", ".00");
                    txtspserviceno.Text = ds.Tables["invoiceno1"].Rows[0]["servicetax"].ToString().Replace(".0000", ".00");
                    da = new SqlDataAdapter("SELECT SrtxReg_No,Debit from ServiceTax_Account where InvoiceNo='" + no + "/ST'", con);
                    da.Fill(ds, "serivcetax");
                    if (ds.Tables["serivcetax"].Rows.Count > 0)
                    {
                        ddlspservice.Enabled = true;
                        txtspserviceno.Enabled = true;
                        ddlspservice.SelectedValue = ds.Tables["serivcetax"].Rows[0]["SrtxReg_No"].ToString();
                        spservice.Visible = true;
                        spexcise.Visible = false;
                        txtvendortype.Text = "Service Provider";
                       
                        if (ddlspvat.SelectedIndex != 0)
                        {
                            ddlspvat.Enabled = true;
                            txtspvat.Enabled = true;
                        }
                        else
                        {
                            ddlspvat.Enabled = false;
                            txtspvat.Enabled = false;
                        }
                        if (ddlspservice.SelectedIndex != 0)
                        {

                            txtserviceedcess.Enabled = true;
                            txtservichedcess.Enabled = true;

                        }
                        else
                        {
                            txtserviceedcess.Enabled = false;
                            txtservichedcess.Enabled = false;
                        }
                    }
                    else
                    {
                        spservice.Visible = false;
                        spexcise.Visible = true;
                    }
                }
                //else
                //{
                //    ddlspservice.Enabled = false;
                //    txtspserviceno.Enabled = false;
                //    txtserviceedcess.Enabled = false;
                //    txtservichedcess.Enabled = false;
                //    ddlspexcise.Enabled = false;
                //    txtspexcise.Enabled = false;

                //}

                if (ds.Tables["invoiceno2"].Rows.Count > 0)
                {

                    txtspvat.Text = ds.Tables["invoiceno2"].Rows[0]["salestax"].ToString().Replace(".0000", ".00");

                    da = new SqlDataAdapter("SELECT Debit,vatreg_no from vat_account where InvoiceNo='" + no + "/VAT'", con);
                    da.Fill(ds, "vatno");
                    if (ds.Tables["vatno"].Rows.Count > 0)
                    {
                        spvat.Visible = true;
                        ddlspvat.Enabled = true;
                        txtspvat.Enabled = true;
                        ddlspvat.SelectedValue = ds.Tables["vatno"].Rows[0]["vatreg_no"].ToString().Replace(".0000", ".00");
                        txtvendortype.Text = "Service provider VAT";
                        
                        if (ddlspexcise.SelectedIndex != 0)
                        {
                            ddlspexcise.Enabled = true;
                            txtspexcise.Enabled = true;
                        }
                        else
                        {
                            ddlspexcise.Enabled = false;
                            txtspexcise.Enabled = false; ;
                        }
                        if (((ddlspservice.SelectedIndex != 0) && (ddlspexcise.SelectedIndex != 0)) || ((ddlspservice.SelectedIndex == 0) && (ddlspexcise.SelectedIndex != 0)) || ((ddlspservice.SelectedIndex != 0) && (ddlspexcise.SelectedIndex == 0)))
                        {

                            txtserviceedcess.Enabled = true;
                            txtservichedcess.Enabled = true;
                        }
                        else
                        {
                            txtserviceedcess.Enabled = false;
                            txtservichedcess.Enabled = false;
                        }
                        ddlspservice.Enabled = false;
                        txtspserviceno.Enabled = false;

                    }
                    else
                    {
                        spvat.Visible = false;
                    }

                }


                }
                if ((ds.Tables["invoiceno"].Rows.Count <= 0) && (ds.Tables["invoiceno1"].Rows.Count <= 0) && (ds.Tables["invoiceno2"].Rows.Count <= 0))
                {
                    ddlspservice.Enabled = false;
                    txtspserviceno.Enabled = false;
                    ddlspexcise.Enabled = false;
                    txtspexcise.Enabled = false;
                    ddlspvat.Enabled = false;
                    txtspvat.Enabled = false;
                    if (txtserviceedcess.Text != "0.00")
                        txtserviceedcess.Enabled = true;
                    else
                        txtserviceedcess.Enabled = false;
                    if (txtservichedcess.Text != "0.00")
                        txtservichedcess.Enabled = true;
                    else
                        txtservichedcess.Enabled = false;
                }
               
                txtvendortype.Text = ds.Tables["pending"].Rows[0][18].ToString();
                txtSpbasic.Text = ds.Tables["pending"].Rows[0][3].ToString().Replace(".0000", ".00");
                txttotal.Text = ds.Tables["pending"].Rows[0][24].ToString().Replace(".0000", ".00");
                Supplier.Visible = false;
                Servicetax.Visible = false;
                deduction.Visible = false;
                deduction1.Visible = false;
                deduction2.Visible = false;
                deduction3.Visible = false;
                Excise.Visible = false;
                trsernum.Visible = false;
                trexcno.Visible = false;

                trvatno.Visible = false;
                Trvattotal.Visible = false;
                servicedecess.Visible = false;
                sppono.Visible = true;
                spbasic.Visible = true;
                //spvat.Visible = true;
                servicedecess.Visible = true;
                spdecess.Visible = true;
                spedecess1.Visible = true;
                spdecess2.Visible = true;
                spedcess3.Visible = true;
                other.Visible = false;

                Serviceprovider.Visible = false;
                trexcno.Visible = false;
                Supplier.Visible = false;
                Excise.Visible = false;
                trvatno.Visible = false;
                Trvattotal.Visible = false;
                Servicetax.Visible = false;


                da = new SqlDataAdapter("select PO_NO,InvoiceNo,Invoice_Date,BasicValue,tds,retention,Advance,Hold,AnyOther,Description,NetAmount,REPLACE(CONVERT(VARCHAR(11),inv_making_date, 106), ' ', '-')as InvMakeDate from pending_invoice where pending_invoice.id=@id", con);
                da.SelectCommand.Parameters.AddWithValue("@id", Vid);
                da.Fill(ds, "pending1");

                txtsppono.Text = ds.Tables["pending1"].Rows[0][0].ToString();
                txtspinvoice.Text = ds.Tables["pending1"].Rows[0][1].ToString();
                txtspinvoicedate.Text = ds.Tables["pending1"].Rows[0][2].ToString();
                txtspinvoicedate.Enabled = false;
                txtindtmk.Text = ds.Tables["pending1"].Rows[0][11].ToString();
                txtspbacis.Text = ds.Tables["pending1"].Rows[0][3].ToString().Replace(".0000", ".00");
                txtsptds.Text = ds.Tables["pending1"].Rows[0][4].ToString().Replace(".0000", ".00");
                txtsprention.Text = ds.Tables["pending1"].Rows[0][5].ToString().Replace(".0000", ".00");
                txtspadvance.Text = ds.Tables["pending1"].Rows[0][6].ToString().Replace(".0000", ".00");
                txtsphold.Text = ds.Tables["pending1"].Rows[0][7].ToString().Replace(".0000", ".00");
                txtspanother.Text = ds.Tables["pending1"].Rows[0][8].ToString().Replace(".0000", ".00");
                txtspdeccription.Text = ds.Tables["pending1"].Rows[0][9].ToString().Replace(".0000", ".00");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SPTotal1", "SPTotal1();", true);

                txtspnetamount.Text = ds.Tables["pending1"].Rows[0][10].ToString().Replace(".0000", ".00");
                
            }
            if (s == "Supplier")
            {
                txtSupBasic.Text = ds.Tables["pending"].Rows[0][3].ToString().Replace(".0000", ".00");
                txtsalestax.Text = ds.Tables["pending"].Rows[0][16].ToString().Replace(".0000", ".00");
                txttotal.Text = ds.Tables["pending"].Rows[0][7].ToString().Replace(".0000", ".00");
                Serviceprovider.Visible = false;
                Servicetax.Visible = false;
                Supplier.Visible = true;
                Excise.Visible = false;
                deduction.Visible = true;
                deduction1.Visible = true;
                deduction2.Visible = true;
                deduction3.Visible = true;
                trsernum.Visible = false;
                trexcno.Visible = false;
                servicedecess.Visible = false;
               
                sppono.Visible = false;
                spbasic.Visible = false;
                spvat.Visible = false;
                servicedecess.Visible = false;
                spdecess.Visible = false;
                spedecess1.Visible = false;
                spdecess2.Visible = false;
                spedcess3.Visible = false;

            }
            

            txtin.Text = ds.Tables["pending"].Rows[0][0].ToString();
            txtindt.Text = ds.Tables["pending"].Rows[0][1].ToString();
            ddlinvoicecc.Text = ds.Tables["pending"].Rows[0][2].ToString();
            //cascinvoicecc.SelectedValue = ds.Tables["pending"].Rows[0][2].ToString();
            txtedc.Text = ds.Tables["pending"].Rows[0][4].ToString().Replace(".0000", ".00");
            //txthed.Text = ds.Tables["pending"].Rows[0][5].ToString().Replace(".0000", ".00");
            txttotal.Text = ds.Tables["pending"].Rows[0][7].ToString().Replace(".0000", ".00");
            txtAdvance.Text = ds.Tables["pending"].Rows[0][8].ToString().Replace(".0000", ".00");
            txthold.Text = ds.Tables["pending"].Rows[0][9].ToString().Replace(".0000", ".00");
            txtother.Text = ds.Tables["pending"].Rows[0][10].ToString().Replace(".0000", ".00");
            txtvendorid.Text = ds.Tables["pending"].Rows[0][11].ToString();
            txtvendorname.Text = ds.Tables["pending"].Rows[0][12].ToString();
            //txtdesc1.Text = ds.Tables["pending"].Rows[0][13].ToString();
            ddlinvoicedca.Text = ds.Tables["pending"].Rows[0][14].ToString();
            ddlinvoicesub.Text = ds.Tables["pending"].Rows[0][15].ToString();
            //cascinvoicedca.SelectedValue = ds.Tables["pending"].Rows[0][14].ToString();
            //cascinvoicesub.SelectedValue = ds.Tables["pending"].Rows[0][15].ToString();
            txtpo.Text = ds.Tables["pending"].Rows[0][17].ToString();
            txttds.Text = ds.Tables["pending"].Rows[0][19].ToString().Replace(".0000", ".00");
            txtretention.Text = ds.Tables["pending"].Rows[0][20].ToString().Replace(".0000", ".00");
            txtindesc.Text = ds.Tables["pending"].Rows[0][21].ToString();
            txtnetAmount.Text = ds.Tables["pending"].Rows[0][22].ToString().Replace(".0000", ".00");
            
        
    }
    public void reset()
    {
        txtin.Text = "";
        txtindt.Text = "";
        ddlinvoicecc.Text = "";
        //cascinvoicecc.SelectedValue = "";
        txtSpbasic.Text = "";
        txtSupBasic.Text = "";
        txted.Text = "";
        txthed.Text = "";
        txttotal.Text = "";
        txtAdvance.Text = "";
        txthold.Text = "";
        txtother.Text = "";
        txtvendorid.Text = "";
        txtvendorname.Text = "";
        ddlinvoicedca.Text = "";
        ddlinvoicesub.Text = "";
        //cascinvoicedca.SelectedValue = "";
        //cascinvoicesub.SelectedValue = "";
        txtpo.Text = "";
        txttds.Text = "";
        txtretention.Text = "";
        txtindesc.Text = "";
        txtnetAmount.Text = "";
        txtsalestax.Text = "";
        txtservicetax.Text = "";
        txtexcise.Text = "";
        txtspserviceno.Text = "";
        txtspexcise.Text = "";
        txtspvat.Text = "";
        ddlspservice.SelectedIndex = 0;
        ddlspexcise.SelectedIndex = 0;
        ddlspvat.SelectedIndex = 0;
        txtserviceedcess.Text = "0.00";
        txtservichedcess.Text = "0.00";
      
    }
    public void filltaxnums(string PONo)
    {
        try
        {
            string type = "";

            da = new SqlDataAdapter("select cc_subtype from cost_center where cc_code in (Select cc_code from SPPO where pono='"+PONo.ToString()+"')", con);

            da.Fill(ds, "payment");
            if (ds.Tables["payment"].Rows.Count > 0)
            {
                type = ds.Tables["payment"].Rows[0][0].ToString();

                if (type == "Non-Performing" || type == "Service")
                {
                    da = new SqlDataAdapter("select ServiceTaxno from [ServiceTaxMaster] where Status='3'", con);
                    da.Fill(ds, "ServiceTax");

                
                    ddlservicetax.DataTextField = "ServiceTaxno";
                    ddlservicetax.DataValueField = "ServiceTaxno";
                    ddlservicetax.DataSource = ds.Tables["ServiceTax"];
                    ddlservicetax.DataBind();
                    ddlservicetax.Items.Insert(0, "Select");
                   
                    
                }

                else if (type == "Manufacturing")
                {
                    da = new SqlDataAdapter("select Excise_no from ExciseMaster where Status='3'", con);
                    da.Fill(ds, "Excise/VAT");

                    ddlExcno.DataValueField = "Excise_no";
                    ddlExcno.DataTextField = "Excise_no";
                    ddlExcno.DataSource = ds.Tables["Excise/VAT"];
                    ddlExcno.DataBind();
                    ddlExcno.Items.Insert(0, "Select");
                
                
                }
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }


    public void filltaxnum()
    {
        try
        {
           
                    da = new SqlDataAdapter("select ServiceTaxno from [ServiceTaxMaster] where Status='3'", con);
                    da.Fill(ds, "ServiceTax");


                    ddlservicetax.DataTextField = "ServiceTaxno";
                    ddlservicetax.DataValueField = "ServiceTaxno";
                    ddlservicetax.DataSource = ds.Tables["ServiceTax"];
                    ddlservicetax.DataBind();
                    ddlservicetax.Items.Insert(0, "Select");
                    ddlspservice.DataTextField = "ServiceTaxno";
                    ddlspservice.DataValueField = "ServiceTaxno";
                    ddlspservice.DataSource = ds.Tables["ServiceTax"];
                    ddlspservice.DataBind();
                    ddlspservice.Items.Insert(0, "Select");

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void fillVatnum()
    {
        try
        {

            da = new SqlDataAdapter("select distinct RegNo from [Saletax/VatMaster] where Status='3'", con);
            da.Fill(ds, "VAT");

            ddlvatno.DataValueField = "RegNo";
            ddlvatno.DataTextField = "RegNo";
            ddlvatno.DataSource = ds.Tables["VAT"];
            ddlvatno.DataBind();
            ddlvatno.Items.Insert(0, "Select");
            ddlspvat.DataValueField = "RegNo";
            ddlspvat.DataTextField = "RegNo";
            ddlspvat.DataSource = ds.Tables["VAT"];
            ddlspvat.DataBind();
            ddlspvat.Items.Insert(0, "Select");
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void fillExnum()
    {
        try
        {

            da = new SqlDataAdapter("select Excise_no from ExciseMaster where Status='3'", con);
            da.Fill(ds, "Excise/VAT");

            ddlExcno.DataValueField = "Excise_no";
            ddlExcno.DataTextField = "Excise_no";
            ddlExcno.DataSource = ds.Tables["Excise/VAT"];
            ddlExcno.DataBind();
            ddlExcno.Items.Insert(0, "Select");
            ddlspexcise.DataValueField = "Excise_no";
            ddlspexcise.DataTextField = "Excise_no";
            ddlspexcise.DataSource = ds.Tables["Excise/VAT"];
            ddlspexcise.DataBind();
            ddlspexcise.Items.Insert(0, "Select");


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        cmd.Connection = con;
        cmd = new SqlCommand("SPInvoice_Reject", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["id"].ToString());
        cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        JavaScript.UPAlert(Page, msg);
        con.Close();
        fillgrid();

    }
}
