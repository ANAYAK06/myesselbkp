using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class VerifyCreditPayments : System.Web.UI.Page
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
                tdrefundtype.Visible = false;
                trmiscclient.Visible = false;
            }
        }

    }
    public void fillgrid()
    {
        try
        {
            tblverifycredits.Visible = false;
            da = new SqlDataAdapter("select Id,Bank_Name,Description,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,PaymentType from BankBook where status='1A' and PaymentType not in ('Hold','Retention') order by id desc ", con);
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
        decimal total;
        try
        {
            ViewState["Creditid"] = gvcredits.SelectedValue.ToString();
            da = new SqlDataAdapter("sp_viewcredits", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", SqlDbType.VarChar).Value = gvcredits.SelectedValue.ToString();
            da.Fill(ds, "FillDetails");
            if (ds.Tables["FillDetails"].Rows.Count > 0)
            {
                trmiscclient.Visible = false;
                tdrefundtype.Visible = true;
                tblverifycredits.Visible = true;
                if (ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString() == "FD" || ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString() == "SD")
                {
                    Label2.Visible = true;
                    lblrefundtype.Visible = true;

                    Label1.Visible = true;
                    lblfdrno.Visible = true;

                    Label18.Visible = false;
                    lblmistype.Visible = false;
                 
                    lblcategoryofpayment.Text = "Refund";
                    lblrefundtype.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString();
                    if (ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString() == "FD")
                    {

                        Label1.Visible = true;
                        lblfdrno.Visible = true;
                        Label2.Visible = true;
                        lblrefundtype.Visible = true;
                        lblfdrno.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[1].ToString();
                    }
                    else if (ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString() == "SD")
                    {

                        Label1.Visible = false;
                        lblfdrno.Visible = false;
                        Label2.Visible = true;
                        lblrefundtype.Visible = true;
                        
                    }                    
                }
                else
                {
                    Label2.Visible = false;
                    lblrefundtype.Visible = false;
                    Label1.Visible = false;
                    lblfdrno.Visible = false;
                    lblcategoryofpayment.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString();
                    
                    Label18.Visible = false;
                    lblmistype.Visible = false;
                    
                }
                lblcccodepri.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[2].ToString();
                lbldcacodepri.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[3].ToString();
                lblsdcacodepri.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[4].ToString();
                lblamountpri.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[5].ToString().Replace(".0000", ".00");
                if (ds.Tables["FillDetails1"].Rows.Count > 0)
                {
                    trintrestlbl.Visible = true;
                    trintrestcodes.Visible = true;
                    trinterstheading.Visible = true;
                    if (ds.Tables["FillDetails2"].Rows.Count > 0)
                    {
                        trmiscclient.Visible = true;
                        Label1.Visible = false;
                        lblfdrno.Visible = false;
                        Label2.Visible = false;
                        lblrefundtype.Visible = false;
                        Label18.Visible = true;
                        lblmistype.Visible = true;
                        Labelheading.Text = "Deduction";
                        lblmistype.Text = "Intrest From Clients";
                        lblcccodeint.Text = ds.Tables["FillDetails2"].Rows[0].ItemArray[0].ToString();
                        lbldcacodeint.Text = ds.Tables["FillDetails2"].Rows[0].ItemArray[1].ToString();
                        lblsdcacodeint.Text = ds.Tables["FillDetails2"].Rows[0].ItemArray[2].ToString();
                        lblamountint.Text = ds.Tables["FillDetails2"].Rows[0].ItemArray[3].ToString().Replace(".0000", ".00");
                        if (ds.Tables["FillDetails2"].Rows[0].ItemArray[4].ToString() != "")
                        {
                            string client = ds.Tables["FillDetails2"].Rows[0].ItemArray[4].ToString().Split(',')[1];
                            client = client.Replace(" ", String.Empty);
                            string subclient = ds.Tables["FillDetails2"].Rows[0].ItemArray[4].ToString().Split(',')[2];
                            subclient = subclient.Replace(" ", String.Empty);
                            da = new SqlDataAdapter("select client_name from client where client_id='" + client + "'; select branch from subclient where subclient_id='" + subclient + "'", con);
                            da.Fill(ds, "CL");
                            lblclient.Text = ds.Tables["FillDetails2"].Rows[0].ItemArray[4].ToString().Split(',')[1] + " [" + ds.Tables["CL"].Rows[0].ItemArray[0].ToString() + "]";
                            lblsubclient.Text = ds.Tables["FillDetails2"].Rows[0].ItemArray[4].ToString().Split(',')[2] + " [" + ds.Tables["CL1"].Rows[0].ItemArray[0].ToString() + "]";
                            lblamountpri.Text = Convert.ToDecimal(Convert.ToDecimal(ds.Tables["FillDetails"].Rows[0][5].ToString()) + Convert.ToDecimal(ds.Tables["FillDetails2"].Rows[0][3].ToString())).ToString();
                        }                    
                        total = Convert.ToDecimal(lblamountpri.Text) - Convert.ToDecimal(lblamountint.Text);                        
                    }
                    else
                    {
                        if (lblcategoryofpayment.Text == "Misc Taxable Receipt")
                        {
                            Label18.Visible = true;
                            lblmistype.Visible = true;
                            Labelheading.Text = "Interest If Any";
                            lblmistype.Text = "Intrest From Others";
                        }                        
                        Labelheading.Text = "Interest If Any";                
                        lblcccodeint.Text = ds.Tables["FillDetails1"].Rows[0].ItemArray[0].ToString();
                        lbldcacodeint.Text = ds.Tables["FillDetails1"].Rows[0].ItemArray[1].ToString();
                        lblsdcacodeint.Text = ds.Tables["FillDetails1"].Rows[0].ItemArray[2].ToString();
                        lblamountint.Text = ds.Tables["FillDetails1"].Rows[0].ItemArray[3].ToString().Replace(".0000", ".00");
                        total = Convert.ToDecimal(lblamountpri.Text) + Convert.ToDecimal(lblamountint.Text);
                    }

                }
                else
                {
                    trinterstheading.Visible = false;
                    trintrestlbl.Visible = false;
                    trintrestcodes.Visible = false;
                    total = Convert.ToDecimal(lblamountpri.Text) + Convert.ToDecimal(0);
                }
                if (ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString() == "Other Refunds" || ds.Tables["FillDetails"].Rows[0].ItemArray[0].ToString() == "FD")
                {
                    trname.Visible = false;
                }
                else
                {
                    trname.Visible = true;
                    lblname.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[11].ToString();
                }
                lblbank.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[6].ToString();
                lblmodeofpay.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[7].ToString();
                DateTime dt = Convert.ToDateTime(ds.Tables["FillDetails"].Rows[0].ItemArray[8].ToString());
                var creation_date = String.Format("{0:dd-MMM-yyyy}", dt);
                lbldate.Text = creation_date;
                lblno.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[9].ToString();
                txtdesc.Text = ds.Tables["FillDetails"].Rows[0].ItemArray[10].ToString();
                lblamount.Text = total.ToString();

            }
            else
            {
                tblverifycredits.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {

        btnapprove.Visible = false;
        cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.CommandText = "sp_Bank_Credit_Approval";
            cmd.Parameters.AddWithValue("@Id", ViewState["Creditid"].ToString());  
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            //string msg = "";
            if (msg == "Credited Successfully")
            {
                JavaScript.UPAlertRedirect(Page, "Approved Successfully", "VerifyCreditPayments.aspx");
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
    protected void btnreject_Click(object sender, EventArgs e)
    {

        btnapprove.Visible = false;
        cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.CommandText = "sp_Bank_Credit_ApprovalReject";
            cmd.Parameters.AddWithValue("@Id", ViewState["Creditid"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Rejected Successfully")
            {
                JavaScript.UPAlertRedirect(Page, "Rejected Successfully", "VerifyCreditPayments.aspx");
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

}