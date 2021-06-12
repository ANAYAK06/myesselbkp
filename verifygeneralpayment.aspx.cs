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


public partial class verifygeneralpayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
   
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tblinvoice.Visible = false;
            fillgrid();
        }

    }
    public void fillexcise(string DCACode)
    {
        try
        {
            if (DCACode.ToString() == "DCA-Excise")
                da = new SqlDataAdapter("select Excise_no as no from ExciseMaster where Status='3'", con);
            else if (DCACode.ToString() == "DCA-VAT")
                da = new SqlDataAdapter("select RegNo as no from [Saletax/VatMaster] where Status='3'", con);
            else if (DCACode.ToString() == "DCA-SRTX")
                da = new SqlDataAdapter("select ServiceTaxno as no from ServiceTaxMaster where Status='3'", con);
            else if (DCACode.ToString() == "DCA-GST-CR")
                da = new SqlDataAdapter("select GST_No as no from GSTmaster where Status='3'", con);
            da.Fill(ds, "nos");
            if (ds.Tables["nos"].Rows.Count > 0)
            {

                ddlno.DataValueField = "no";
                ddlno.DataTextField = "no";
                ddlno.DataSource = ds.Tables["nos"];
                ddlno.DataBind();
                ddlno.Items.Insert(0, "Select");
            }
            else
            {
                ddlno.Items.Insert(0, "Select");
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
        da = new SqlDataAdapter("Select invoiceno,cc_code,dca_code,subdca_code,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,replace(amount,'.0000','.00')as amount,description,Mode_of_Pay from GeneralInvoices where invoiceno='" + ViewState["invno"].ToString() + "';Select ExciseReg_No from  Excise_Account where invoiceno='" + ViewState["invno"].ToString() + "';Select VatReg_No from VAT_Account where invoiceno='" + ViewState["invno"].ToString() + "';Select SrtxReg_No from ServiceTax_Account where invoiceno='" + ViewState["invno"].ToString() + "';Select GstReg_No from GST_Account where invoiceno='" + ViewState["invno"].ToString() + "'", con);
        da.Fill(ds, "data");
        txtinvno.Text = ds.Tables["data"].Rows[0].ItemArray[0].ToString();
        CascadingDropDown4.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[1].ToString();
        //CascadingDropDown1.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
        //CascadingDropDown3.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[3].ToString();
        lbldca.Text = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
        lblsubdca.Text = ds.Tables["data"].Rows[0].ItemArray[3].ToString();
        txtname.Text = ds.Tables["data"].Rows[0].ItemArray[4].ToString();
        txtdate.Text = ds.Tables["data"].Rows[0].ItemArray[5].ToString();
        txtamount.Text = ds.Tables["data"].Rows[0].ItemArray[6].ToString();
        txtremarks.Text = ds.Tables["data"].Rows[0].ItemArray[7].ToString();
        ddlmodeofpay.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[8].ToString();
        da = new SqlDataAdapter("select dca_name from dca where dca_code='" + ds.Tables["data"].Rows[0].ItemArray[2].ToString() + "' union all Select subdca_name from subdca where subdca_code='" + ds.Tables["data"].Rows[0].ItemArray[3].ToString() + "'", con);
        da.Fill(ds, "dcaname");
        lbldcaname.Text = ds.Tables["dcaname"].Rows[0].ItemArray[0].ToString();
        lblsdcaname.Text = ds.Tables["dcaname"].Rows[1].ItemArray[0].ToString();
        da = new SqlDataAdapter("Select Approved_Users from GeneralInvoices where Invoiceno='" + ds.Tables["data"].Rows[0].ItemArray[0].ToString() + "'", con);
        da.Fill(ds, "roles");
        if (ds.Tables["roles"].Rows.Count > 0 && ds.Tables["roles"].Rows[0].ItemArray[0].ToString() != "")
        {
            Trgvusers.Visible = true;
            string rolesamend = ds.Tables["roles"].Rows[0][0].ToString().Replace("'", " ");
            da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + rolesamend + "',',')", con);
            da.Fill(ds, "splitrole");
            DataTable dtra = ds.Tables["splitrole"];
            ViewState["Curtblroles"] = dtra;
            gvusers.DataSource = dtra;
            gvusers.DataBind();
        }
        else
        {
            Trgvusers.Visible = false;
            gvusers.DataSource = null;
            gvusers.DataBind();
        }
        if (ds.Tables["data"].Rows[0].ItemArray[2].ToString() == "DCA-Excise")
        {
            fillexcise(lbldca.Text);
            ddlno.SelectedValue = ds.Tables["data1"].Rows[0].ItemArray[0].ToString();
        
        }
        else if (ds.Tables["data"].Rows[0].ItemArray[2].ToString() == "DCA-VAT")
        {
            fillexcise(lbldca.Text);
            ddlno.SelectedValue = ds.Tables["data2"].Rows[0].ItemArray[0].ToString();
        
        }
        else if (ds.Tables["data"].Rows[0].ItemArray[2].ToString() == "DCA-SRTX")
        {
            fillexcise(lbldca.Text);
            ddlno.SelectedValue = ds.Tables["data3"].Rows[0].ItemArray[0].ToString();

        }
        else if (ds.Tables["data"].Rows[0].ItemArray[2].ToString() == "DCA-GST-CR")
        {
            fillexcise(lbldca.Text);
            ddlno.SelectedValue = ds.Tables["data4"].Rows[0].ItemArray[0].ToString();

        }
        else
        {
            ddlno.Visible = false;
        
        }

    }
    public int j = 0;
    public int k = 1;
    protected void gvusers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["Curtblroles"] != null)
                {
                    DataTable Objdt = ViewState["Curtblroles"] as DataTable;
                    if (Objdt.Rows[j][0].ToString() != "")
                    {
                        //da = new SqlDataAdapter("select Roles, (first_name+'  '+middle_name+'  '+last_name)as name ,ur.User_Name from user_roles ur join Employee_Data ed on ur.User_Name=ed.User_Name where ur.User_Name='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da = new SqlDataAdapter("select (first_name+'  '+middle_name+'  '+last_name)as name,USER_NAME from Employee_Data where USER_NAME='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da.Fill(ds, "userroles");
                        if (k == 1)
                        {
                            e.Row.Cells[1].Text = "Sr.Accountant";
                            e.Row.Cells[0].Text = "Prepared By";
                        }
                        if (k == 2)
                        {
                            e.Row.Cells[1].Text = "HoAdmin";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        //if (k == 3)
                        //{
                        //    e.Row.Cells[1].Text = "HoAdmin";
                        //    e.Row.Cells[0].Text = "Verified By";
                        //}
                        //e.Row.Cells[1].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                        e.Row.Cells[2].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                        //e.Row.Cells[3].Text = ds.Tables["userroles"].Rows[j].ItemArray[2].ToString();

                    }
                }
                j = j + 1;
                k = k + 1;
            }

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void gridgeneral_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string invoice = gridgeneral.DataKeys[e.NewEditIndex]["Invoiceno"].ToString();
        ViewState["invno"] = invoice;
        filltable();
        tblinvoice.Visible = true;
    }
    public void fillgrid()
      {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("Select invoiceno,cc_code,dca_code,subdca_code,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,replace(amount,'.0000','.00')as amount,Mode_of_Pay from GeneralInvoices where status='1' ", con);
            else
                da = new SqlDataAdapter("Select invoiceno,cc_code,dca_code,subdca_code,name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date,replace(amount,'.0000','.00')as amount,Mode_of_Pay from GeneralInvoices where status='2' ", con);

            da.Fill(ds, "fill");
            gridgeneral.DataSource = ds.Tables["fill"];
            gridgeneral.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = false;
        try
        {
            da = new SqlDataAdapter("ISValidDCA_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ModeofTransaction", SqlDbType.DateTime).Value = "Debit";
            da.SelectCommand.Parameters.AddWithValue("@DCACode", SqlDbType.VarChar).Value = lbldca.Text;
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "General Invoice";
            da.SelectCommand.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = txtinvno.Text;
            da.Fill(ds, "IsvalidDCA");
            if (ds.Tables["IsvalidDCA"].Rows[0][0].ToString() == "Invalid DCA")
            {
                JavaScript.UPAlert(Page, ds.Tables["IsvalidDCA"].Rows[0][0].ToString());
            }
            else if (ds.Tables["IsvalidDCA"].Rows[0][0].ToString() == "Valid")
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GeneralInvoiceapproval_sp";
                cmd.Parameters.Add(new SqlParameter("@Invno", txtinvno.Text));
                cmd.Parameters.Add(new SqlParameter("@CCCode", ddlcccode.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@DCACode", lbldca.Text));
                cmd.Parameters.Add(new SqlParameter("@SubDCA_Code", lblsubdca.Text));
                cmd.Parameters.Add(new SqlParameter("@date", txtdate.Text));
                cmd.Parameters.Add(new SqlParameter("@Name", txtname.Text));
                cmd.Parameters.Add(new SqlParameter("@Remarks", txtremarks.Text));
                cmd.Parameters.Add(new SqlParameter("@Amount", txtamount.Text));
                cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@roles", Session["roles"].ToString()));
                //New parameter created date 15-June-2016 Cr-ENH-APR-002-2016 by kishore STARTS 
                cmd.Parameters.Add(new SqlParameter("@paymentmode", ddlmodeofpay.SelectedItem.Text));
                //New parameter created date 15-June-2016 Cr-ENH-APR-002-2016 by kishore Ends
                if (lbldca.Text == "DCA-Excise")
                {
                    cmd.Parameters.Add(new SqlParameter("@Exciseno", ddlno.SelectedItem.Text));
                }
                else if (lbldca.Text == "DCA-VAT")
                {
                    cmd.Parameters.Add(new SqlParameter("@VATno", ddlno.SelectedItem.Text));
                }
                else if (lbldca.Text == "DCA-SRTX")
                {
                    cmd.Parameters.Add(new SqlParameter("@SRTXno", ddlno.SelectedItem.Text));
                }
                else if (lbldca.Text == "DCA-GST-CR")
                {
                    cmd.Parameters.Add(new SqlParameter("@GSTno", ddlno.SelectedItem.Text));
                }
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                JavaScript.UPAlertRedirect(Page, msg, "verifygeneralpayment.aspx");
               
               
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }

    }
    protected void gridgeneral_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //int invno = Convert.ToInt32(gridgeneral.DataKeys[e.RowIndex]["Invoiceno"].ToString());
            cmd = new SqlCommand("GeneralInvReject_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Invno", gridgeneral.DataKeys[e.RowIndex]["Invoiceno"].ToString());
            cmd.Parameters.AddWithValue("@Date", gridgeneral.Rows[e.RowIndex].Cells[2].Text);
            cmd.Parameters.AddWithValue("@CCCode", gridgeneral.Rows[e.RowIndex].Cells[3].Text);
            cmd.Parameters.AddWithValue("@DCACode", gridgeneral.Rows[e.RowIndex].Cells[4].Text);
            cmd.Parameters.AddWithValue("@Amount", gridgeneral.Rows[e.RowIndex].Cells[7].Text);
            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            JavaScript.UPAlertRedirect(Page, msg, "verifygeneralpayment.aspx");

            con.Close();
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        tblinvoice.Visible = false;
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        tblinvoice.Visible = false;

    }
}
