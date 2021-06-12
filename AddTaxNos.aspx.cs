using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddTaxNos : System.Web.UI.Page
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
        hfrole.Value = Session["roles"].ToString();
      
    }
   
    public void check()
    {
       
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                btn.Visible = true;
                btnAdd.Text = "Add";
                btnCancel.Text = "Reset";
                traddcc.Visible = true;
                trupdatecc.Visible = false;
                Label1.Text = "ADD VAT/TIN/SALES TAX NUMBER";
                clear();
            }
            else if ((Session["roles"].ToString() == "HoAdmin") || (Session["roles"].ToString() == "SuperAdmin"))
            {
                btn.Visible = true;
                btnAdd.Text = "Update";
                btnCancel.Text = "Cancel";
                trupdatecc.Visible = true;
                traddcc.Visible = false;
                Label1.Text = "UPDATE VAT/TIN/SALES TAX NUMBER";
                clear();
            }
            else
            {
                btn.Visible = false;
                trupdatecc.Visible = false;
                traddcc.Visible = false;
                Label1.Text = "ADD/UPDATE VAT/TIN/SALES TAX NUMBER";
            }
        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        cmd.Connection = con;
        cmd = new SqlCommand("AddTaxno_sp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@vatno", SqlDbType.VarChar).Value = txtvat.Text;
        //cmd.Parameters.Add("@tinno", SqlDbType.VarChar).Value = txttin.Text;
        cmd.Parameters.Add("@staxno", SqlDbType.VarChar).Value = txtstax.Text;
        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = ddlstate.SelectedItem.Text;
        cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = txtarea.Text;
        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Add";
            cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = ddlCCcode.SelectedValue;
        }
        else if ((Session["roles"].ToString() == "HoAdmin")||(Session["roles"].ToString() == "SuperAdmin"))
        {
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Update";
            cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = txtcccode.Text;
        }
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
        cmd.Parameters.Add("@roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        if (msg == "Successfull")
        {
            JavaScript.UPAlertRedirect(Page, msg, "AddTaxNos.aspx");
        }
        else
        {
            JavaScript.UPAlert(Page, msg);
        }
    }

    public void clear()
    {
        ddlstate.SelectedIndex = 0;
        txtarea.Text = "";
        txtstax.Text = "";
        //txttin.Text = "";
        txtvat.Text = "";
        if (Session["roles"].ToString() != "Sr.Accountant")

            CascadingDropDown4.SelectedValue = "Select Cost Center";

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
                da = new SqlDataAdapter("Select id,vat,cc_code,Excise from tax_nos where status='1'", con);
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                da = new SqlDataAdapter("Select id,vat,cc_code,Excise from tax_nos where status='2'", con);
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
            da = new SqlDataAdapter("Select cc_code,vat,Excise,state,area from tax_nos where status='1' and id='" + id.ToString() + "'", con);
        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            da = new SqlDataAdapter("Select cc_code,vat,Excise,state,area from tax_nos where status='2' and id='" + id.ToString() + "'", con);
        }
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count > 0)
        {
            txtcccode.Text = ds.Tables["data"].Rows[0].ItemArray[0].ToString();
            txtvat.Text = ds.Tables["data"].Rows[0].ItemArray[1].ToString();
            //txttin.Text = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
            txtstax.Text = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
            ddlstate.SelectedItem.Text = ds.Tables["data"].Rows[0].ItemArray[3].ToString();
            txtarea.Text = ds.Tables["data"].Rows[0].ItemArray[4].ToString();
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