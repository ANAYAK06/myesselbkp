using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class VatRegistration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            btn.Visible = false;
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        cmd.Connection = con;
        cmd = new SqlCommand("AddVATReg_sp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@RegName", SqlDbType.VarChar).Value = txtregnme.Text;
        cmd.Parameters.Add("@RegUnder", SqlDbType.VarChar).Value = ddlregunder.SelectedValue;
        cmd.Parameters.Add("@RegNumber", SqlDbType.VarChar).Value = txtregnum.Text;
        cmd.Parameters.Add("@RegAddress", SqlDbType.VarChar).Value = txtregAdd.Text;
        cmd.Parameters.Add("@Jurisdiction", SqlDbType.VarChar).Value = txtjurs.Text;
        cmd.Parameters.Add("@Circle", SqlDbType.VarChar).Value = txtcircle.Text;
        cmd.Parameters.Add("@Jointcircle", SqlDbType.VarChar).Value = txtjointcirlce.Text;
        cmd.Parameters.Add("@district", SqlDbType.VarChar).Value = txtdistrict.Text;
        cmd.Parameters.Add("@commissionarate", SqlDbType.VarChar).Value = txtcommision.Text;
        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = ddlstate.SelectedItem.Text;
        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtdesc.Text;
        
        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Add";
            
        }
        else if ((Session["roles"].ToString() == "HoAdmin") || (Session["roles"].ToString() == "SuperAdmin"))
        {
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Update";
            
        }
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
        cmd.Parameters.Add("@roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        if (msg == "Successfull")
        {
            JavaScript.UPAlertRedirect(Page, msg, "VatRegistration.aspx");
        }
        else
        {
            JavaScript.UPAlert(Page, msg);
        }
    }


    public void check()
    {

        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            btn.Visible = true;
            btnAdd.Text = "Add";
            btnCancel.Text = "Reset";

            Label1.Text = "SALES TAX/VAT REGISTRATION FORM";
            clear();
        }
        else if ((Session["roles"].ToString() == "HoAdmin") || (Session["roles"].ToString() == "SuperAdmin"))
        {
            btn.Visible = true;
            btnAdd.Text = "Update";
            btnCancel.Text = "Cancel";


            Label1.Text = "UPDATE SALES TAX/VAT REGISTRATION FORM";
            clear();
        }
        else
        {
            btn.Visible = false;

            Label1.Text = "UPDATE SALES TAX/VAT REGISTRATION FORM";
        }

    }

    public void clear()
    {
        ddlstate.SelectedIndex = 0;
        txtregnme.Text="";
        ddlregunder.SelectedIndex = 0;
        txtregnum.Text="";
        txtregAdd.Text="";
        txtjurs.Text="";
        txtcircle.Text="";
        txtjointcirlce.Text="";
        txtdistrict.Text="";
        txtcommision.Text="";
        txtdesc.Text = "";
        

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
    public void filldata()
    {
        try
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                da = new SqlDataAdapter("select id,RegName,RegNo,CommissionRate,State from [Saletax/VatMaster] where status='1'", con);
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                da = new SqlDataAdapter("select id,RegName,RegNo,CommissionRate,State from [Saletax/VatMaster] where status='2'", con);
            }
            da.Fill(ds, "taxinfo");
            gridupdate.DataSource = ds.Tables["taxinfo"];
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
            da = new SqlDataAdapter("select RegName,RegUnder,RegNo,RegAddress,Jurisdiction,Circle,JointCircle,District,CommissionRate,State,Description from [Saletax/VatMaster] where status='1' and id='" + id.ToString() + "'", con);
        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            da = new SqlDataAdapter("Select RegName,RegUnder,RegNo,RegAddress,Jurisdiction,Circle,JointCircle,District,CommissionRate,State,Description from [Saletax/VatMaster] where status='2' and id='" + id.ToString() + "'", con);
        }
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {

            txtregnme.Text = ds.Tables["data"].Rows[0].ItemArray[0].ToString();
            ddlregunder.SelectedItem.Text = ds.Tables["data"].Rows[0].ItemArray[1].ToString();
            txtregnum.Text = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
            txtregAdd.Text = ds.Tables["data"].Rows[0].ItemArray[3].ToString();
            txtjurs.Text = ds.Tables["data"].Rows[0].ItemArray[4].ToString();
            txtcircle.Text = ds.Tables["data"].Rows[0].ItemArray[5].ToString();
            txtjointcirlce.Text = ds.Tables["data"].Rows[0].ItemArray[6].ToString();
            txtdistrict.Text = ds.Tables["data"].Rows[0].ItemArray[7].ToString();
            txtcommision.Text = ds.Tables["data"].Rows[0].ItemArray[8].ToString();
            ddlstate.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[9].ToString();
            txtdesc.Text = ds.Tables["data"].Rows[0].ItemArray[10].ToString();
        }
    }
    protected void gridupdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string id = gridupdate.SelectedValue.ToString();
            filltable(id);
            trtable.Visible = true;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
}