using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class GSTRegistrationForm : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fillstates();
        }
        if (Session["roles"].ToString() != "Sr.Accountant")
        {
            filldata();
            trgrid.Visible = true;
            trtable.Visible = false;
        }
        check();
    }
    public void check()
    {

        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            btn.Visible = true;           
            btnAdd.Text = "Add";
            btnCancel.Text = "Reset";
            Label1.Text = "ADD GST REGISTRATION FORM";
            Page.Title = "GST REGISTRATION FORM";
        }
        else if (Session["roles"].ToString() == "HoAdmin")
        {
            btn.Visible = true;           
            btnAdd.Text = "Update";
            btnCancel.Text = "Cancel";
            Label1.Text = "UPDATE GST REGISTRATION FORM";
            Page.Title = "UPDATE REGISTRATION FORM";
        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            btn.Visible = true;           
            btnAdd.Text = "Approve";
            btnCancel.Text = "Cancel";
            Label1.Text = "Approve GST REGISTRATION FORM";
            Page.Title = "Approve REGISTRATION FORM";
        }
        else
        {
            btn.Visible = false;
            Label1.Text = "UPDATE GST REGISTRATION FORM";
        }

    }
    public void Fillstates()
    {
        try
        {

            da = new SqlDataAdapter("select State_Id,state from [states]", con);
            da.Fill(ds, "GSTStates");
            if (ds.Tables["GSTStates"].Rows.Count > 0)
            {
                ddlstate.DataValueField = "State_Id";
                ddlstate.DataTextField = "state";
                ddlstate.DataSource = ds.Tables["GSTStates"];
                ddlstate.DataBind();
                ddlstate.Items.Insert(0, "Select");              
            }
            else
            {

                ddlstate.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        cmd.Connection = con;
        cmd = new SqlCommand("usp_GSTRegistration", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Tradename", SqlDbType.VarChar).Value = txttradename.Text;
        cmd.Parameters.Add("@Legalname", SqlDbType.VarChar).Value = txtlegalname.Text;
        cmd.Parameters.Add("@GST", SqlDbType.VarChar).Value = txtgstnumber.Text;
        cmd.Parameters.Add("@RegDate", SqlDbType.DateTime).Value = txtregdate.Text;
        cmd.Parameters.Add("@Nature", SqlDbType.VarChar).Value = txtnature.Text;
        cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtRegAdress.Text;
        cmd.Parameters.Add("@Juridiction", SqlDbType.VarChar).Value = txtjurisdiction.Text;
        cmd.Parameters.Add("@Ward", SqlDbType.VarChar).Value = txtward.Text;
        cmd.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = txtdstatecode.Text;
        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = ddlstate.SelectedValue;
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
        cmd.Parameters.Add("@roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
       
        if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin")
        {
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = ViewState["id"].ToString();
        }
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        //string msg = "Sucess";
        if (msg == "Successfull")
        {
            if (Session["roles"].ToString() == "Sr.Accountant")
                JavaScript.UPAlertRedirect(Page, "Submitted Successfully", "GSTRegistrationForm.aspx");
            if (Session["roles"].ToString() == "HoAdmin")
                JavaScript.UPAlertRedirect(Page, "Verified Successfully", "GSTRegistrationForm.aspx");
            if (Session["roles"].ToString() == "SuperAdmin")
                JavaScript.UPAlertRedirect(Page, "Approved Successfully", "GSTRegistrationForm.aspx");
        }
        else
        {
            JavaScript.UPAlert(Page, msg);
        }
    }
    public void filldata()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                da = new SqlDataAdapter("Select gm.id,LegalName,GST_no,st.state as state,REPLACE(REPLACE(CONVERT(VARCHAR,regd_Date ,106), ' ','-'), ',','')as regd_Date from gstmaster gm join states st ON st.State_Id=gm.state_id where status='1'", con);
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                da = new SqlDataAdapter("Select gm.id,LegalName,GST_no,st.state as state,REPLACE(REPLACE(CONVERT(VARCHAR,regd_Date ,106), ' ','-'), ',','')as regd_Date from gstmaster gm join states st ON st.State_Id=gm.state_id where status='2'", con);
            }
            da.Fill(ds, "invoicedata");
            gridupdate.DataSource = ds.Tables["invoicedata"];
            gridupdate.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void gridupdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string id = gridupdate.SelectedValue.ToString();
            ViewState["id"] = id;
            filltable(id);
            trtable.Visible = true;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void filltable(string id)
    {
        if (Session["roles"].ToString() == "HoAdmin")
        {
            da = new SqlDataAdapter("SELECT gm.id,TradeName,LegalName,GST_no,REPLACE(REPLACE(CONVERT(VARCHAR,Regd_date ,106), ' ','-'), ',','')as Regd_date,Nature_of_Business,RegdAddress,gm.state_id,state_of_jurisdiction,ward_circle_sector,state_code from GSTmaster gm join states st on st.State_Id=gm.state_id where status='1' and gm.id='" + id.ToString() + "'", con);
        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            da = new SqlDataAdapter("SELECT gm.id,TradeName,LegalName,GST_no,REPLACE(REPLACE(CONVERT(VARCHAR,Regd_date ,106), ' ','-'), ',','')as Regd_date,Nature_of_Business,RegdAddress,gm.state_id,state_of_jurisdiction,ward_circle_sector,state_code from GSTmaster gm join states st on st.State_Id=gm.state_id where status='2' and gm.id='" + id.ToString() + "'", con);
        }
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            txttradename.Text = ds.Tables["data"].Rows[0].ItemArray[1].ToString();
            txtlegalname.Text = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
            txtgstnumber.Text = ds.Tables["data"].Rows[0].ItemArray[3].ToString();
            txtregdate.Text = ds.Tables["data"].Rows[0].ItemArray[4].ToString();
            txtnature.Text = ds.Tables["data"].Rows[0].ItemArray[5].ToString();
            txtRegAdress.Text = ds.Tables["data"].Rows[0].ItemArray[6].ToString();
            ddlstate.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[7].ToString();
            txtjurisdiction.Text = ds.Tables["data"].Rows[0].ItemArray[8].ToString();
            txtward.Text = ds.Tables["data"].Rows[0].ItemArray[9].ToString();
            txtdstatecode.Text = ds.Tables["data"].Rows[0].ItemArray[10].ToString();
        }
    }
}