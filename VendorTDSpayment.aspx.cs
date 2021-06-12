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
using System.Drawing;


public partial class VendorTDSpayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            tblbtn.Visible = false;
            ddlcheque.Visible = false;
            trpaymentdetails.Visible = false;
            fillcostcenter();
            trcategory.Visible = false;
        }

    }
    public void fillcostcenter()
    {
        da = new SqlDataAdapter("select (cc_code+' , '+cc_name)as name,cc_code from Cost_Center", con);
        da.Fill(ds, "cc");
        if (ds.Tables["cc"].Rows.Count > 0)
        {

            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataTextField = "name";
            ddlCCcode.DataSource = ds.Tables["cc"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
            ddlCCcode.Items.Insert(1, "Select All");
        }
        else
        {
            ddsdcaitcode.Items.Insert(0, "Select");
        }
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcategory.SelectedValue == "SubDca")
        {
            da = new SqlDataAdapter("SELECT (subdca_code+' , '+subdca_name)as name ,subdca_code from subdca where dca_code='DCA-45'", con);
            da.Fill(ds, "sdcas");
            if (ds.Tables["sdcas"].Rows.Count > 0)
            {
                trcategory.Visible = true;
                ddsdcaitcode.DataValueField = "subdca_code";
                ddsdcaitcode.DataTextField = "name";
                ddsdcaitcode.DataSource = ds.Tables["sdcas"];
                ddsdcaitcode.DataBind();
                ddsdcaitcode.Items.Insert(0, "Select");
                ddsdcaitcode.Items.Insert(1, "Select All");
            }
            else
            {
                trcategory.Visible = false;
                ddsdcaitcode.Items.Insert(0, "Select");
            }
        }
        else if (ddlcategory.SelectedValue == "ItCode")
        {
            da = new SqlDataAdapter("select (i.it_code+' , '+i.it_name)as name ,i.it_code FROM it i join subdca sd on i.it_code=sd.it_code where sd.dca_code='DCA-45'", con);
            da.Fill(ds, "its");
            if (ds.Tables["its"].Rows.Count > 0)
            {
                trcategory.Visible = true;
                ddsdcaitcode.DataValueField = "it_code";
                ddsdcaitcode.DataTextField = "name";
                ddsdcaitcode.DataSource = ds.Tables["its"];
                ddsdcaitcode.DataBind();
                ddsdcaitcode.Items.Insert(0, "Select");
                ddsdcaitcode.Items.Insert(1, "Select All");
            }
            else
            {
                trcategory.Visible = false;
                ddsdcaitcode.Items.Insert(0, "Select");
            }
        }

        else if (ddlcategory.SelectedValue == "SelectAll" || ddlcategory.SelectedValue == "Select")
        {
            trcategory.Visible = false;
        }
    }
    public void fillgrid()
    {
        try
        {
            da = new SqlDataAdapter("usp_viewvendortds", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CCCode", ddlCCcode.SelectedValue);
            da.SelectCommand.Parameters.AddWithValue("@Category", ddlcategory.SelectedValue);
            da.SelectCommand.Parameters.AddWithValue("@itsdca", ddsdcaitcode.SelectedValue);
            da.SelectCommand.Parameters.AddWithValue("@FromDate", txtfrom.Text);
            da.SelectCommand.Parameters.AddWithValue("@ToDate", txtto.Text);     
            da.SelectCommand.Parameters.AddWithValue("@Type", "Payment");
            da.Fill(ds, "tdsData");
            if (ds.Tables["tdsData"].Rows.Count > 0)
            {
                trpaymentdetails.Visible = true;
                tblbtn.Visible = true;
                gvvendortds.DataSource = ds.Tables["tdsData"];
                gvvendortds.DataBind();                
            }
            else
            {
                trpaymentdetails.Visible = false;
                tblbtn.Visible = false;
                gvvendortds.EmptyDataText = "No Data Avaliable for the selection criteria";
                gvvendortds.DataSource = null;
                gvvendortds.DataBind();              
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        Clear();
        fillgrid();
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
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Check()", true);
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
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Check()", true);
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rec in gvvendortds.Rows)
        {
            CheckBox c1 = (CheckBox)rec.FindControl("chkSelect");
            CheckBox chkAll = (CheckBox)gvvendortds.HeaderRow.FindControl("chkAll");
        }
    }
    private decimal Basic = (decimal)0.0;
    private decimal Tds = (decimal)0.0;
    private decimal MTDS = (decimal)0.0;
    protected void gvvendortds_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TextBox txttds = (TextBox)e.Row.FindControl("TdsDedAmount");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double dbvalue = Convert.ToDouble(txttds.Text);
            txttds.Text = String.Format("{0:0.00}", dbvalue);
            Basic += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Basicvalue"));
            Tds += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TdsAmount"));
            MTDS += Convert.ToDecimal(txttds.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = String.Format("Rs. {0:0.00}", Basic);
            e.Row.Cells[7].Text = String.Format("Rs. {0:0.00}", Tds);
            e.Row.Cells[8].Text = String.Format("Rs. {0:0.00}", MTDS);
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        BankDateCheck objdate = new BankDateCheck();
        if (objdate.IsBankDateCheck(txtdate.Text, ddlfrom.SelectedItem.Text))
        {
            btnsubmit.Visible = false;
            try
            {
                string ids = "";
                string Amts = "";
                foreach (GridViewRow rec in gvvendortds.Rows)
                {
                    if ((rec.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        ids = ids + gvvendortds.DataKeys[rec.RowIndex]["Id"].ToString() + ",";
                        Amts = Amts + (rec.FindControl("TdsDedAmount") as TextBox).Text + ",";
                    }
                }
                if (ids != "")
                {
                    cmd1 = new SqlCommand("usp_checkTDSbudget", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@ids", ids);
                    cmd1.Parameters.AddWithValue("@TdsAmounts", Amts);
                    cmd1.Connection = con;
                    con.Open();
                    string result = cmd1.ExecuteScalar().ToString();
                    con.Close();
                    if (result == "sufficent")
                    {
                        da = new SqlDataAdapter("sp_TDS_payment", con);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Ids", ids);
                        da.SelectCommand.Parameters.AddWithValue("@TdsAmounts", Amts);
                        da.SelectCommand.Parameters.AddWithValue("@date", txtdate.Text);
                        da.SelectCommand.Parameters.AddWithValue("@TotalAmount", txtamt.Text);
                        da.SelectCommand.Parameters.AddWithValue("@Description", txtdesc.Text);
                        da.SelectCommand.Parameters.AddWithValue("@ModeOfPay", ddlpayment.SelectedItem.Text);
                        da.SelectCommand.Parameters.AddWithValue("@Bank_Name", ddlfrom.SelectedItem.Text);
                        if (ddlpayment.SelectedItem.Text == "Cheque")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@No", ddlcheque.SelectedItem.Text);
                            da.SelectCommand.Parameters.AddWithValue("@chequeid", ddlcheque.SelectedValue);
                        }
                        else
                        {
                            da.SelectCommand.Parameters.AddWithValue("@No", txtcheque.Text);
                        }
                        da.SelectCommand.Parameters.AddWithValue("@User", Session["user"].ToString());
                        da.Fill(ds, "InvoiceTranNo");
                        if (ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[0].ToString() == "Sucessfull")
                        {
                            JavaScript.UPAlertRedirect(Page, "TDS Payment Invoice Generated Sucessfully. The Transaction No is: " + ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[1].ToString(), "VendorTDSpayment.aspx");
                        }
                        else
                        {
                            btnsubmit.Visible = true;
                            JavaScript.UPAlert(Page, ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[1].ToString());
                            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Check()", true);
                        }
                    }
                    else
                    {
                        btnsubmit.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "up", "javascript:Check()", true);
                        JavaScript.UPAlert(Page, result);
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
        else
        {
            JavaScript.UPAlert(Page, "Your selected date is not before than the bank account opening date");
        }
    }
    //public void showalertprint(string message, string id)
    //{
    //    string script = @"window.alert('" + message + "');window.location ='VendorTDSpayment.aspx';window.open('Bankpaymentprint.aspx?id=" + id + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');";
    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert", script, true);

    //}

    public void Clear()
    {
        txtdate.Text = "";
        txtdesc.Text = "";
        txtamt.Text = "";
        txtcheque.Text = "";
        txtcheque.Visible = true;
        ddlcheque.Visible = false;
        CascadingDropDown9.SelectedValue = "";
        CascadingDropDown1.SelectedValue = "";
        gvvendortds.DataSource = null;
        gvvendortds.DataBind();
    }

}


//showalertprint("Sucessfull", ds.Tables["InvoiceTranNo"].Rows[0].ItemArray[1].ToString());
//Clear();
//trgrid.Visible = false;
//trpaymentdetails.Visible = false;
//tblbtn.Visible = false;
//Response.Redirect("VendorTDSpayment.aspx");
