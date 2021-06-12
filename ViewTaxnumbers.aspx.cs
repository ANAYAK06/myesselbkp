using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ViewTaxnumbers : System.Web.UI.Page
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
            tbl.Visible = false;
            tblgst.Visible = false;
            //ddlno.Visible = false;
        }
    }

    public void Fill()
    {
        if (ddltype.SelectedItem.Text == "Sales/Vat")
            da = new SqlDataAdapter("select RegNo as no from [Saletax/VatMaster] where Status='3'", con);
        else if (ddltype.SelectedItem.Text == "Excise")
            da = new SqlDataAdapter("select Excise_no as no from ExciseMaster where Status='3'", con);
        else if (ddltype.SelectedItem.Text == "Service")
            da = new SqlDataAdapter("select ServiceTaxno as no from ServiceTaxMaster where Status='3'", con);
        else if (ddltype.SelectedItem.Text == "GST")
            da = new SqlDataAdapter("select GST_no as no from GSTmaster where Status='3'", con);
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

   
    public void FillData()
    {
        ds.Clear();
        if (ddltype.SelectedItem.Text == "Sales/Vat")
            da = new SqlDataAdapter("Select RegName,RegUnder,RegNo,RegAddress,Jurisdiction,Circle,JointCircle,District,CommissionRate,State,Description from [Saletax/VatMaster] where RegNo='" + ddlno.SelectedItem.Text + "'", con);
        else if (ddltype.SelectedItem.Text == "Excise")
            da = new SqlDataAdapter("Select Excise_no,Name,Address,Commissionerate,Division,Range,District,State,Description from ExciseMaster where Excise_no='" + ddlno.SelectedItem.Text + "'", con);
        else if (ddltype.SelectedItem.Text == "Service")
            da = new SqlDataAdapter("Select Name,ServiceTaxno,Address,Area,Taluka,PostOffice,Pincode,District,state,Description from ServiceTaxMaster where ServiceTaxno='" + ddlno.SelectedItem.Text + "'", con);
        else if (ddltype.SelectedItem.Text == "GST")
            da = new SqlDataAdapter("select gtm.TradeName,gtm.LegalName,REPLACE(CONVERT(VARCHAR(11),gtm.Regd_Date, 106), ' ', '-')as Regd_Date,gtm.Nature_of_business,gtm.RegdAddress,st.State,gtm.State_Of_Jurisdiction,gtm.Ward_Circle_Sector,gtm.State_Code from GSTmaster gtm JOIN States st on gtm.State_Id=st.State_Id WHERE gtm.gst_no='" + ddlno.SelectedItem.Text + "'", con);

        da.Fill(ds, "FillData");
        if (ds.Tables["FillData"].Rows.Count > 0)
        {
            if (ddltype.SelectedItem.Text == "Sales/Vat")
            {
                tbl.Visible = true;
                tblgst.Visible = false;
                txtregistername.Text = ds.Tables["FillData"].Rows[0][0].ToString();
                txtregunder.Text = ds.Tables["FillData"].Rows[0][1].ToString();
                txtregnumber.Text = ds.Tables["FillData"].Rows[0][2].ToString();
                txtresaddress.Text = ds.Tables["FillData"].Rows[0][3].ToString();
                txtjurs.Text = ds.Tables["FillData"].Rows[0][4].ToString();
                txtdivision.Text = ds.Tables["FillData"].Rows[0][5].ToString();
                txtrange.Text = ds.Tables["FillData"].Rows[0][6].ToString();
                txtdistrict.Text = ds.Tables["FillData"].Rows[0][7].ToString();
                txtcomissionerate.Text = ds.Tables["FillData"].Rows[0][8].ToString();
                txtstate.Text = ds.Tables["FillData"].Rows[0][9].ToString();
                txtdesc.Text = ds.Tables["FillData"].Rows[0][10].ToString();
            }
            if (ddltype.SelectedItem.Text == "Excise")
            {
                tbl.Visible = true;
                tblgst.Visible = false;
                txtregistername.Text = ds.Tables["FillData"].Rows[0][1].ToString();
                txtregnumber.Text = ds.Tables["FillData"].Rows[0][0].ToString();
                txtresaddress.Text = ds.Tables["FillData"].Rows[0][2].ToString();
                txtdivision.Text = ds.Tables["FillData"].Rows[0][4].ToString();
                txtrange.Text = ds.Tables["FillData"].Rows[0][5].ToString();
                txtdistrict.Text = ds.Tables["FillData"].Rows[0][6].ToString();
                txtcomissionerate.Text = ds.Tables["FillData"].Rows[0][3].ToString();
                txtstate.Text = ds.Tables["FillData"].Rows[0][7].ToString();
                txtdesc.Text = ds.Tables["FillData"].Rows[0][8].ToString();
            }
            if (ddltype.SelectedItem.Text == "Service")
            {
                tbl.Visible = true;
                tblgst.Visible = false;
                txtregistername.Text = ds.Tables["FillData"].Rows[0][0].ToString();
                txtregnumber.Text = ds.Tables["FillData"].Rows[0][1].ToString();
                txtresaddress.Text = ds.Tables["FillData"].Rows[0][2].ToString();
                txtjurs.Text = ds.Tables["FillData"].Rows[0][6].ToString();
                txtdivision.Text = ds.Tables["FillData"].Rows[0][4].ToString();
                txtrange.Text = ds.Tables["FillData"].Rows[0][5].ToString();
                txtdistrict.Text = ds.Tables["FillData"].Rows[0][7].ToString();
                txtcomissionerate.Text = ds.Tables["FillData"].Rows[0][3].ToString();
                txtstate.Text = ds.Tables["FillData"].Rows[0][8].ToString();
                txtdesc.Text = ds.Tables["FillData"].Rows[0][9].ToString();
            }
            if (ddltype.SelectedItem.Text == "GST")
            {
                tbl.Visible = false;
                tblgst.Visible = true;
                txttradename.Text = ds.Tables["FillData"].Rows[0][0].ToString();
                txtlegalname.Text = ds.Tables["FillData"].Rows[0][1].ToString();
                txtregdate.Text = ds.Tables["FillData"].Rows[0][2].ToString();
                txtnature.Text = ds.Tables["FillData"].Rows[0][3].ToString();
                txtRegAdress.Text = ds.Tables["FillData"].Rows[0][4].ToString();
                txtstates.Text = ds.Tables["FillData"].Rows[0][5].ToString();
                txtjurisdiction.Text = ds.Tables["FillData"].Rows[0][6].ToString();
                txtward.Text = ds.Tables["FillData"].Rows[0][7].ToString();
                txtdstatecode.Text = ds.Tables["FillData"].Rows[0][8].ToString();               
            }
        }

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tbl.Visible = false;
            tblgst.Visible = false;
            ddlno.Visible = true;
            if (ddltype.SelectedItem.Text == "Sales/Vat")
            {

                Fill();
                trunder.Visible = true;
                trjrd.Visible = true;
                Label11.Text = "Juridiction";
                Label5.Text = "Commissionerate";
                Label6.Text = "Circle";
                Label7.Text = "Joint Circle";
            }
            else if (ddltype.SelectedItem.Text == "Excise")
            {
                Fill();
                trunder.Visible = false;
                trjrd.Visible = false;
                Label5.Text = "Commissionerate";
                Label6.Text = "Division";
                Label7.Text = "Range";
            }
            else if (ddltype.SelectedItem.Text == "Service")
            {

                Fill();
                trunder.Visible = false;
                trjrd.Visible = true;
                Label11.Text = "PIN Code";
                Label5.Text = "Area";
                Label6.Text = "Taluka";
                Label7.Text = "Post Office";
            }
            if (ddltype.SelectedItem.Text == "GST")
            {
                Fill();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void ddlno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //tbl.Visible = true;
            FillData();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }


    }
  }