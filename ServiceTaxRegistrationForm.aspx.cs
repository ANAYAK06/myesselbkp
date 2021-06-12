﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class ServiceTaxRegistrationForm : System.Web.UI.Page
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
            check();
        }
        if (Session["roles"].ToString() == "Sr.Accountant")
        {

        }
        else
        {

            filldata();
            trgrid.Visible = true;
            trtable.Visible = false;
        }
    }

    public void check()
    {

        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            btn.Visible = true;
            //btndelete.Visible = false;
            btnAdd.Text = "Add";
            btnCancel.Text = "Reset";
            Label1.Text = "ADD SERVICETAX REGISTRATION FORM";

        }
        else if (Session["roles"].ToString() == "HoAdmin")
        {
            btn.Visible = true;
            //btndelete.Visible = false;
            btnAdd.Text = "Update";
            btnCancel.Text = "Cancel";
            Label1.Text = "UPDATE SERVICETAX REGISTRATION FORM";
        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            btn.Visible = true;
            //btndelete.Visible = true;
            btnAdd.Text = "Update";
            btnCancel.Text = "Cancel";
            Label1.Text = "UPDATE SERVICETAX REGISTRATION FORM";
        }
        else
        {
            btn.Visible = false;
            Label1.Text = "UPDATE SERVICETAX REGISTRATION FORM";
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        cmd.Connection = con;
        cmd = new SqlCommand("ServicetaxRegno_sp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Registername", SqlDbType.VarChar).Value = txtregistername.Text;
        cmd.Parameters.Add("@Registerno", SqlDbType.VarChar).Value = txtregnumber.Text;
        cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtresaddress.Text;
        cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = txtarea.Text;
        cmd.Parameters.Add("@Taluka", SqlDbType.VarChar).Value = txttaluka.Text;
        cmd.Parameters.Add("@POffice", SqlDbType.VarChar).Value = txtpoffice.Text;
        cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = txtdistrict.Text;
        cmd.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = txtpincode.Text;
        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = ddlstate.SelectedItem.Text;
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
        cmd.Parameters.Add("@roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtdesc.Text;
        

        if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin")
        {
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = ViewState["id"].ToString();
        }
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        if (msg == "Successfull")
        {
            JavaScript.UPAlertRedirect(Page, msg, "ServiceTaxRegistrationForm.aspx");
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
                da = new SqlDataAdapter("Select id,Name,Servicetaxno,District,State from ServiceTaxMaster where status='1'", con);
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                da = new SqlDataAdapter("Select id,Name,Servicetaxno,District,State from ServiceTaxMaster where status='2'", con);
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

    public void filltable(string id)
    {
        if (Session["roles"].ToString() == "HoAdmin")
        {
            da = new SqlDataAdapter("Select Id,Name,Address,Servicetaxno,Area,Taluka,PostOffice,Pincode,District,State,Description from ServiceTaxMaster where status='1' and id='" + id.ToString() + "'", con);
        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            da = new SqlDataAdapter("Select Id,Name,Address,Servicetaxno,Area,Taluka,PostOffice,Pincode,District,State,Description from ServiceTaxMaster where status='2' and id='" + id.ToString() + "'", con);
        }
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            txtregistername.Text = ds.Tables["data"].Rows[0].ItemArray[1].ToString();
            txtresaddress.Text = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
            txtregnumber.Text = ds.Tables["data"].Rows[0].ItemArray[3].ToString();
            txtarea.Text = ds.Tables["data"].Rows[0].ItemArray[4].ToString();
            txttaluka.Text = ds.Tables["data"].Rows[0].ItemArray[5].ToString();
            txtpoffice.Text = ds.Tables["data"].Rows[0].ItemArray[6].ToString();
            txtpincode.Text = ds.Tables["data"].Rows[0].ItemArray[7].ToString();
            txtdistrict.Text = ds.Tables["data"].Rows[0].ItemArray[8].ToString();
            txtdesc.Text=ds.Tables["data"].Rows[0].ItemArray[10].ToString();
            ddlstate.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[9].ToString();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            clear();
        }
        else
        {
            trtable.Visible = false;
        }
    }

    public void clear()
    {
        ddlstate.SelectedIndex = 0;
        txtregistername.Text = "";
        txtresaddress.Text = "";
        txtregnumber.Text = "";
        txtarea.Text = "";
        txttaluka.Text = "";
        txtpoffice.Text = "";
        txtpincode.Text = "";
        txtdistrict.Text = "";
        txtdesc.Text = "";

    }
}