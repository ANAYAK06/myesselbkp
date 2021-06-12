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
using System.IO;
using System.Globalization;
using System.Net.Mail;
using System.Net;

public partial class EmployeeRegister : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd=new SqlCommand();
    DataSet ds = new DataSet();
    SqlCommand cmd1 = new SqlCommand();
    SqlCommand cmd2 = new SqlCommand();
   
    DataTable dt = new DataTable();
    DataTable fdt = new DataTable();
    DataTable dtemp = new DataTable();
    DataTable dtemp1 = new DataTable();
    DataTable dtquali = new DataTable();
    DataTable dtsal = new DataTable();
    DataTable dbsal = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        
        if (!IsPostBack)
        {
         
            if (Session["roles"].ToString() == "Hr.Asst")
            {
                TabContainer1.Tabs[3].Visible = false;
                ddlstatus.Enabled = false;
                ddlstatus.SelectedItem.Text = "Submit";
                ddlstatus.SelectedItem.Value = "4";
            }
            else
            {
                TabContainer1.Tabs[3].Visible = false;
                
            }
            //Session["emptype"] = ddlempstatus.SelectedValue;

            DataColumn dc = new DataColumn("id");
            DataColumn dc1 = new DataColumn("Children_name");
            DataColumn dc2 = new DataColumn("Children_dob");
            DataColumn dc3 = new DataColumn("Age");
            DataColumn dc4 = new DataColumn("Gender");
            DataColumn dc5 = new DataColumn("Martial_status");
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            Session["fillchildern"] = dt;


            DataColumn fdc = new DataColumn("id");
            DataColumn fdc1 = new DataColumn("Name");
            DataColumn fdc2 = new DataColumn("Dob");
            DataColumn fdc3 = new DataColumn("Age");
            DataColumn fdc4 = new DataColumn("Gender");
            DataColumn fdc5 = new DataColumn("relation");
            DataColumn fdc6 = new DataColumn("Workno");
            DataColumn fdc7 = new DataColumn("Mobileno");

            fdt.Columns.Add(fdc);
            fdt.Columns.Add(fdc1);
            fdt.Columns.Add(fdc2);
            fdt.Columns.Add(fdc3);
            fdt.Columns.Add(fdc4);
            fdt.Columns.Add(fdc5);
            fdt.Columns.Add(fdc6);
            fdt.Columns.Add(fdc7);
            Session["fillfamilydetails"] = fdt;

            DataColumn dco5 = new DataColumn("id");
            DataColumn dco1 = new DataColumn("Organization_name");
            DataColumn dco2 = new DataColumn("From");
            DataColumn dco3 = new DataColumn("To");
            DataColumn dco4 = new DataColumn("Roledesignation");
            dtemp.Columns.Add(dco5);
            dtemp.Columns.Add(dco1);
            dtemp.Columns.Add(dco2);
            dtemp.Columns.Add(dco3);
            dtemp.Columns.Add(dco4);
            Session["fillhistory"] = dtemp;

            DataColumn dcol = new DataColumn("id");
            DataColumn dcol1 = new DataColumn("Class");
            DataColumn dcol2 = new DataColumn("University_Name");
            DataColumn dcol3 = new DataColumn("From");
            DataColumn dcol4 = new DataColumn("To");
            DataColumn dcol5 = new DataColumn("Percentage");
            dtemp1.Columns.Add(dcol);
            dtemp1.Columns.Add(dcol1);
            dtemp1.Columns.Add(dcol2);
            dtemp1.Columns.Add(dcol3);
            dtemp1.Columns.Add(dcol4);
            dtemp1.Columns.Add(dcol5);
            Session["fillquali"] = dtemp1;

            DataColumn dcl1 = new DataColumn("id");
            DataColumn dcl = new DataColumn("Technical_Skills");
            DataColumn dcl2 = new DataColumn("experience");
            dtquali.Columns.Add(dcl1);
            dtquali.Columns.Add(dcl);
            dtquali.Columns.Add(dcl2);
            Session["fillTquali"] = dtquali;

            DataColumn dcoll6 = new DataColumn("id");
            DataColumn dcoll1 = new DataColumn("Component_Name");
            ////DataColumn dcoll2 = new DataColumn("");
            DataColumn dcoll3 = new DataColumn("Percentage");
            DataColumn dcoll4 = new DataColumn("Monthly");
            DataColumn dcoll5 = new DataColumn("Yearly");
            dtsal.Columns.Add(dcoll6);
            dtsal.Columns.Add(dcoll1);
            //dtsal.Columns.Add(dcoll2);
            dtsal.Columns.Add(dcoll3);
            dtsal.Columns.Add(dcoll4);
            dtsal.Columns.Add(dcoll5);
            Session["fillsalary"] = dtsal;

            DataColumn dbcoldes = new DataColumn("Component_Name");
            //DataColumn dbcl = new DataColumn("");
            DataColumn dbcolperc = new DataColumn("Percentage");
            DataColumn dbcolmonth = new DataColumn("Monthly");
            DataColumn dbcolyear = new DataColumn("Yearly");
            dbsal.Columns.Add(dbcoldes);
            //dbsal.Columns.Add(dbcl);
            dbsal.Columns.Add(dbcolperc);
            dbsal.Columns.Add(dbcolmonth);
            dbsal.Columns.Add(dbcolyear);
            Session["SessionTable5"] = dbsal;


            btncancel.Text = "Cancel";


            //BindGrid();
            //BindGridemp();
            //BindQuali();
            //BindTQuali();
            //BindSalary();

            txtdate.Text = DateTime.Now.ToString();


            if (Request.QueryString["Type"] == "1") //To View Employee Details
            {
                FillDepartment();
                BindGrid();
                BindGridemp();
                BindQuali();
                BindTQuali();
                // BindSalary();
                trdetails1.Visible = false;
                trroleid.Visible = false;
                trpwd.Visible = false;
                hlnk.Visible = false;
                lnkbtn.Visible = false;
                appointtype.Visible = false;

                // trempjoin.Visible = false;
                hdftype.Value = Request.QueryString["Type"].ToString();
                Session["id"] = Request.QueryString["id"];
                trRadioButtonList1.Visible = false;
                trstatus.Visible = false;
                btnsubmit.Visible = false;
                btncancel.Text = "Close";
                grdfamilydetails.ShowFooter = false;
                grdTqualifications.ShowFooter = false;
                grdqualification.ShowFooter = false;
                grdhistory.ShowFooter = false;
                grdchildren.ShowFooter = false;
                grdsalarybreakup.ShowFooter = false;
                upduploads.Visible = true;
                newuploads.Visible = false;
                joiningcategory.Visible = false;
                Joiningtype.Visible = false;
                btnview.Visible = false;
                troldid.Visible = false;
                viewform(Request.QueryString["Type"].ToString());
                fildocuments();
                //if (Session["roles"].ToString() == "Hr.Asst")
                //    TabPanelSalary.Visible = false;
                //else
                //    TabPanelSalary.Visible = true;
            }
            else if (Request.QueryString["Type"] == "2" && Request.QueryString["status"].ToString() != "Existing") //To Edit/Approved Employee Details
            {
                FillDepartment();
                Bindempfamily();
                BindGrid();
                BindGridemp();
                BindQuali();
                BindTQuali();
                // BindSalary();

                trdetails1.Visible = false;
                appointtype.Visible = false;
                hdftype.Value = Request.QueryString["Type"].ToString();
                trRadioButtonList1.Visible = false;
                Session["id"] = Request.QueryString["id"];
                trstatus.Visible = true;
                btnsubmit.Visible = true;
                btncancel.Text = "Cancel";
                grdTqualifications.ShowFooter = true;
                grdqualification.ShowFooter = true;
                grdhistory.ShowFooter = true;
                grdchildren.ShowFooter = true;
                grdsalarybreakup.ShowFooter = true;
                upduploads.Visible = true;
                newuploads.Visible = false;
                joiningcategory.Visible = false;
                Joiningtype.Visible = false;
                btnview.Visible = false;
                troldid.Visible = false;
                viewform(Request.QueryString["Type"].ToString());
                fildocuments();
                //if (Session["roles"].ToString() == "Hr.Asst")
                //    TabPanelSalary.Visible = false;
                //else
                //    TabPanelSalary.Visible = true;
            }
            else if (Request.QueryString["Type"] == "3")//To Create a NEW employee
            {
                Bindempfamily();
                BindGrid();
                BindGridemp();
                BindQuali();
                BindTQuali();
                // BindSalary();
                RadioButtonList1.Items[1].Selected = true;
                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    appointtype.Visible = true;
                    trroleid.Visible = true;
                    trpwd.Visible = true;
                    lblpwd.Text = CreatePassword();
                    lbldpwd.Text = CreatePassword();
                }
                else
                {
                    trroleid.Visible = false;
                    trpwd.Visible = false;
                    appointtype.Visible = false;
                }
                trdetails.Visible = false;
                trdetails1.Visible = false;
                troldid.Visible = false;
                trempjoin.Visible = true;
                //trgrdhistory.Visible = false;
                hdftype.Value = Request.QueryString["Type"].ToString();
                Session["id"] = Request.QueryString["id"];
                //if (Session["roles"].ToString() == "Hr.Asst")
                //    TabPanelSalary.Visible = false;
                //else
                //    TabPanelSalary.Visible = true;
                //hdftype.Value = Request.QueryString["Type"].ToString();
                //Session["Employee_id"] = Request.QueryString["employeeid"];
                //trRadioButtonList1.Visible = true;
                //trstatus.Visible = true;
                //btnsubmit.Visible = true;
                //btncancel.Text = "Cancel";
                //grdTqualifications.ShowFooter = true;
                //grdqualification.ShowFooter = true;
                //grdhistory.ShowFooter = true;
                //grdchildren.ShowFooter = true;
                //grdsalarybreakup.ShowFooter = true;
                //upduploads.Visible = false;
                //newuploads.Visible = true;
                //grdTqualifications.DataSource = ReturnEmptyDataTableTQualifi();
                //grdTqualifications.DataBind();
                //grdqualification.DataSource = ReturnEmptyDataTableQualifi();
                //grdqualification.DataBind();
                //grdhistory.DataSource = ReturnEmptyDataTableemp();
                //grdhistory.DataBind();
                //grdchildren.DataSource = ReturnEmptyDataTable();
                //grdchildren.DataBind();
                //grdsalarybreakup.DataSource = ReturnEmptyDataTablesalary();
                //grdsalarybreakup.DataBind();
            }
            else
            {
                //BindGrid();
                //BindGridemp();
                //BindQuali();
                //BindTQuali();
                //BindSalary();
                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    trroleid.Visible = true;
                    trpwd.Visible = true;
                    lblpwd.Text = CreatePassword();
                    lbldpwd.Text = CreatePassword();
                    appointtype.Visible = true;
                }
                else
                {
                    trroleid.Visible = false;
                    trpwd.Visible = false;
                    appointtype.Visible = false;
                }
                trdetails.Visible = false;
                trdetails1.Visible = false;

                troldid.Visible = false;

                hdftype.Value = "3";
                Session["id"] = "0";
            }
         }
        hdfroles.Value = Session["roles"].ToString();
       

    }
    protected void ddlempstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlempstatus.SelectedValue == "ReJoin")
        {
            troldid.Visible = true;
            getempoldids();
            
        }
        else
        {
            troldid.Visible = false;
        }
    }
    protected void ddlfgender_SelectedIndexChanged(object sender, EventArgs e)
    {
        string MGender = ((DropDownList)grdfamilydetails.FooterRow.FindControl("ddlfgender")).SelectedItem.Text;
        DropDownList ddlRelation = ((DropDownList)grdfamilydetails.FooterRow.FindControl("ddlRelation")) as DropDownList;
        ddlRelation.Items.Clear();
        if (MGender == "Male" && ddlsex.SelectedItem.Text == "Male" && ddlmartialst.SelectedItem.Text == "Single")
        {           
            ddlRelation.Items.Add("Select");
            ddlRelation.Items.Add("Father");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");
        }
        else if (MGender == "Male" && ddlsex.SelectedItem.Text == "Male" && ddlmartialst.SelectedItem.Text != "Single")
        {
            ddlRelation.Items.Add("Select");
            ddlRelation.Items.Add("Father");
            ddlRelation.Items.Add("Son");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");
        }
        else if (MGender == "Female" && ddlsex.SelectedItem.Text == "Male" && ddlmartialst.SelectedItem.Text == "Single")
        {
            ddlRelation.Items.Add("Select");         
            ddlRelation.Items.Add("Mother");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");
          
        }
        else if (MGender == "Female" && ddlsex.SelectedItem.Text == "Male" && ddlmartialst.SelectedItem.Text == "Single")
        {
            ddlRelation.Items.Add("Select");        
            ddlRelation.Items.Add("Mother");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");
        }
        else if (MGender == "Female" && ddlsex.SelectedItem.Text == "Male" && ddlmartialst.SelectedItem.Text != "Single")
        {
            ddlRelation.Items.Add("Select");
            ddlRelation.Items.Add("Wife");
            ddlRelation.Items.Add("Mother");
            ddlRelation.Items.Add("Daughter");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");
        }
        else if (MGender == "Male" && ddlsex.SelectedItem.Text == "Female" && ddlmartialst.SelectedItem.Text == "Single")
        {
            ddlRelation.Items.Add("Select");       
            ddlRelation.Items.Add("Father");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");
         
        }
        else if (MGender == "Male" && ddlsex.SelectedItem.Text == "Female" && ddlmartialst.SelectedItem.Text != "Single")
        {
            ddlRelation.Items.Add("Select");
            ddlRelation.Items.Add("Husband");
            ddlRelation.Items.Add("Father");
            ddlRelation.Items.Add("Son");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");
        }
        else if (MGender == "Female" && ddlsex.SelectedItem.Text == "Female" && ddlmartialst.SelectedItem.Text == "Single")
        {
            ddlRelation.Items.Add("Select");
            ddlRelation.Items.Add("Mother");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");           
        }
        else if (MGender == "Female" && ddlsex.SelectedItem.Text == "Female" && ddlmartialst.SelectedItem.Text != "Single")
        {
            ddlRelation.Items.Add("Select");
            ddlRelation.Items.Add("Mother");
            ddlRelation.Items.Add("Wife");
            ddlRelation.Items.Add("Brother");
            ddlRelation.Items.Add("Sister");
            ddlRelation.Items.Add("Guardian");
        }
    }
    private void getempoldids()
    {
        da = new SqlDataAdapter("SELECT employee_id FROM employee_data where status='Left' AND employee_status='Left'", con);
        da.Fill(ds, "empoldids");
        if (ds.Tables["empoldids"].Rows.Count > 0)
        {
            ddloldid.DataTextField = "employee_id";
            ddloldid.DataValueField = "employee_id";
            ddloldid.DataSource = ds.Tables["empoldids"];
            ddloldid.DataBind();
            ddloldid.Items.Insert(0, "Select Old EmloyeeID");
        }
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        btnview.Enabled = false;
       if (ddlempstatus.SelectedValue == "ReJoin")
        {
            Bindempfamily();
            BindGrid();
            BindGridemp();
            BindQuali();
            BindTQuali();
          //  BindSalary();

            trdetails.Visible = true;
            Session["Employee_id"] = ddloldid.SelectedValue;
            hdftype.Value = "4";
            trRadioButtonList1.Visible = true;
            trstatus.Visible = true;
            btnsubmit.Visible = true;
            btncancel.Text = "Cancel";
            grdTqualifications.ShowFooter = true;
            grdqualification.ShowFooter = true;
            grdhistory.ShowFooter = true;
            grdchildren.ShowFooter = true;
            grdsalarybreakup.ShowFooter = true;
            upduploads.Visible = true;
            newuploads.Visible = false;
            viewform("4");
            //grdTqualifications.DataSource = ReturnEmptyDataTableTQualifi();
            //grdTqualifications.DataBind();
            //grdqualification.DataSource = ReturnEmptyDataTableQualifi();
            //grdqualification.DataBind();
            //grdhistory.DataSource = ReturnEmptyDataTableemp();
            //grdhistory.DataBind();
            //grdchildren.DataSource = ReturnEmptyDataTable();
            //grdchildren.DataBind();
            //grdsalarybreakup.DataSource = ReturnEmptyDataTablesalary();
            //grdsalarybreakup.DataBind();
            fildocuments();
            
        }
        else if (ddlempstatus.SelectedValue == "New")
        {
            FillDepartment();
            if (ddlappointmenttype.SelectedItem.Text == "Normal")
            {
                trdetails.Visible = true;
                trdetails1.Visible = false;
                Bindempfamily();

                BindGrid();
                BindGridemp();
                BindQuali();
                BindTQuali();

                grdsalarybreakup.ShowFooter = true;
                grdsalarybreakup.DataSource = ReturnEmptyDataTablesalary();
                grdsalarybreakup.DataBind();
                //}
                //trgrdhistory.Visible = false;
                trRadioButtonList1.Visible = true;
                trstatus.Visible = true;
                btnsubmit.Visible = true;
                btncancel.Text = "Cancel";
                grdTqualifications.ShowFooter = true;
                grdqualification.ShowFooter = true;
                grdhistory.ShowFooter = true;
                grdchildren.ShowFooter = true;

                upduploads.Visible = false;
                newuploads.Visible = true;
            }
            else if (ddlappointmenttype.SelectedItem.Text == "Direct")
            {
                trdetails.Visible = false;
                trdetails1.Visible = true;
                trstatus.Visible = true;
                btnsubmit.Visible = true;
                btncancel.Text = "Cancel";
            }
            
           
          
        }
    }

    public void fillgrid()
    {
        DataTable dt = new DataTable();
        for (int i = 1; i <= 1; i++)
        dt.Rows.Add(i.ToString());
        grdchildren.DataSource = dt;
        grdchildren.DataBind();

    }
    public void fillgrid1()
    {
        DataTable dt1 = new DataTable();
        for (int i = 1; i <= 1; i++)
        dt1.Rows.Add(i.ToString());
        grdhistory.DataSource = dt1;
        grdhistory.DataBind();

    }
    public void fillgrid2()
    {
        DataTable dt2 = new DataTable();
        for (int i = 1; i <= 1; i++)
            dt2.Rows.Add(i.ToString());
        grdqualification.DataSource = dt2;
        grdqualification.DataBind();
    }
    
   protected void btnnext1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        if (ddlmartialst.SelectedItem.Text != "Single")
        {
            TabContainer1.ActiveTabIndex = 2;
            txtact.Text = "2";
        }
        else
        {
            TabContainer1.ActiveTabIndex = 3;
            txtact.Text = "3";
        }
        
    }
    protected void btnnext2_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        TabContainer1.ActiveTabIndex = 3;
        txtact.Text = "3";

    }
    protected void btnnext3_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        TabContainer1.ActiveTabIndex = 4;
        txtact.Text = "4";
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        TabContainer1.ActiveTabIndex = 3;
        txtact.Text = "3";
    }
    protected void btnback1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        if (ddlmartialst.SelectedItem.Text != "Single")
        {
            TabContainer1.ActiveTabIndex = 2;
            txtact.Text = "2";
        }
        else
        {
            TabContainer1.ActiveTabIndex = 0;
            txtact.Text = "0";
        }
    }

    protected void btnback2_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        TabContainer1.ActiveTabIndex = 1;
        txtact.Text = "1";
    }
    public DataTable ReturnEmptyDataTablefamily()
    {
        DataTable dtMenu1 = new DataTable();
        DataColumn ID = new DataColumn("id", typeof(System.Int32));
        dtMenu1.Columns.Add(ID);
        DataColumn name = new DataColumn("name", typeof(System.String));
        dtMenu1.Columns.Add(name);
        DataColumn Dob = new DataColumn("Dob", typeof(System.String));
        dtMenu1.Columns.Add(Dob);
        DataColumn age = new DataColumn("Age", typeof(System.String));
        dtMenu1.Columns.Add(age);
        DataColumn gender = new DataColumn("Gender", typeof(System.String));
        dtMenu1.Columns.Add(gender);
        DataColumn relation = new DataColumn("relation", typeof(System.String));
        dtMenu1.Columns.Add(relation);
        DataColumn workno = new DataColumn("Workno", typeof(System.String));
        dtMenu1.Columns.Add(workno);
        DataColumn mobno = new DataColumn("Mobileno", typeof(System.String));
        dtMenu1.Columns.Add(mobno);
        DataRow datatRow1 = dtMenu1.NewRow();
       dtMenu1.Rows.Add(datatRow1);
        return dtMenu1;
    }
    public void Bindempfamily()
    {
        grdfamilydetails.DataSource = ReturnEmptyDataTablefamily();
        grdfamilydetails.DataBind();
        grdfamilydetails.Rows[0].Visible = false;
    }
    public DataTable ReturnEmptyDataTable()
    {
        DataTable dtMenu = new DataTable();
        DataColumn dcMenuID = new DataColumn("id", typeof(System.Int32));
        dtMenu.Columns.Add(dcMenuID);
        DataColumn name = new DataColumn("Children_name", typeof(System.String));
        dtMenu.Columns.Add(name);
        DataColumn dob = new DataColumn("Children_dob", typeof(System.String));
        dtMenu.Columns.Add(dob);
        DataColumn age = new DataColumn("Age", typeof(System.String));
        dtMenu.Columns.Add(age);
        DataColumn Gender = new DataColumn("Gender", typeof(System.String));
        dtMenu.Columns.Add(Gender);
        DataColumn Status = new DataColumn("Martial_status", typeof(System.String));
        dtMenu.Columns.Add(Status);
        DataRow datatRow = dtMenu.NewRow();
        dtMenu.Rows.Add(datatRow);
        return dtMenu;
   }
    public void BindGrid()
    {
        grdchildren.DataSource = ReturnEmptyDataTable();
        grdchildren.DataBind();
        grdchildren.Rows[0].Visible = false;

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            string Mname = ((TextBox)grdchildren.FooterRow.FindControl("txtfname1")).Text;
            string Mdob = ((TextBox)grdchildren.FooterRow.FindControl("txtfdob")).Text;
            string Mage = ((TextBox)grdchildren.FooterRow.FindControl("txtfage")).Text;
            string MGender = ((DropDownList)grdchildren.FooterRow.FindControl("ddlfgender")).SelectedItem.Text;
            string MStatus = ((DropDownList)grdchildren.FooterRow.FindControl("ddlfmartialstatus")).SelectedItem.Text;
            if (Mname != "" && Mdob != "" && Mage != "" && MGender != "Select" && MStatus != "Select")
            {
                const string key = "emp";
                DataTable dt1 = Session[key] as DataTable;
                dt = (DataTable)Session["fillchildern"];
                DataRow dr2 = dt.NewRow();
                dr2[0] = 0;
                dr2[1] = Mname;
                dr2[2] = Mdob;
                dr2[3] = Mage;
                dr2[4] = MGender;
                dr2[5] = MStatus;
                dt.Rows.Add(0,Mname, Mdob, Mage, MGender, MStatus);
                Session[key] = dt;
                grdchildren.DataSource = dt;
                grdchildren.DataBind();
            }
            else
            {
                JavaScript.UPAlert(Page, "Please Enter All Required Data");
            }
        }
        catch
        {
        }
    }

    protected void btnfamilyadd_Click(object sender, EventArgs e)
    {
        try
        {
            string Mname = ((TextBox)grdfamilydetails.FooterRow.FindControl("txtname")).Text;
            string Mdob = ((TextBox)grdfamilydetails.FooterRow.FindControl("txtfdob")).Text;
            string Mage = ((TextBox)grdfamilydetails.FooterRow.FindControl("txtfage")).Text;
            string MGender = ((DropDownList)grdfamilydetails.FooterRow.FindControl("ddlfgender")).SelectedItem.Text;
            string Relation = ((DropDownList)grdfamilydetails.FooterRow.FindControl("ddlRelation")).SelectedItem.Text;
            string Workph = ((TextBox)grdfamilydetails.FooterRow.FindControl("txtfwno1")).Text;
            string Mobph = ((TextBox)grdfamilydetails.FooterRow.FindControl("txtfbobno")).Text;
            if (Mname != "" && Mdob != "" && Mage != "" && MGender != "Select" && Relation != "Select" && Workph !="" && Mobph !="")
            {
                const string key = "empfamily";
                DataTable dt1 = Session[key] as DataTable;
                dt = (DataTable)Session["fillfamilydetails"];
                DataRow dr2 = dt.NewRow();
                dr2[0] = 0;
                dr2[1] = Mname;
                dr2[2] = Mdob;
                dr2[3] = Mage;
                dr2[4] = MGender;
                dr2[5] = Relation;
                dr2[6] = Workph;
                dr2[7] = Mobph;
                dt.Rows.Add(0, Mname, Mdob, Mage, MGender, Relation,Workph,Mobph);
                Session[key] = dt;
                grdfamilydetails.DataSource = dt;
                grdfamilydetails.DataBind();
            }
            else
            {
                JavaScript.UPAlert(Page, "Please Enter All Required Data");
            }
        }
        catch
        {
        }
    }
    protected void grdchildren_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdchildren.Rows[e.RowIndex];
        TextBox textname = (TextBox)row.FindControl("txtname");
        int i = grdchildren.Rows.Count;
        DataTable dt1 = new DataTable();
        int index = Convert.ToInt32(e.RowIndex);
        const string key = "emp";
        dt1 = Session["fillchildern"] as DataTable;
        dt1.Rows.RemoveAt(e.RowIndex);
        if (i!= 1)
        {
           
            this.grdchildren.DataSource = dt1;
            grdchildren.DataBind();
        }
        else
        {
            BindGrid();
          
        }
    }
    protected void grdfamilydetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdfamilydetails.Rows[e.RowIndex];
        TextBox textname = (TextBox)row.FindControl("txtname");
        int i = grdfamilydetails.Rows.Count;
        DataTable dt1 = new DataTable();
        int index = Convert.ToInt32(e.RowIndex);
        const string key = "emp";
        dt1 = Session["fillfamilydetails"] as DataTable;
        dt1.Rows.RemoveAt(e.RowIndex);
        if (i != 1)
        {

            this.grdfamilydetails.DataSource = dt1;
            grdfamilydetails.DataBind();
        }
        else
        {
            Bindempfamily();

        }
    }
    public DataTable ReturnEmptyDataTableemp()
    {
        DataTable dtMenu1 = new DataTable();
        DataColumn ID = new DataColumn("id", typeof(System.Int32));
        dtMenu1.Columns.Add(ID);
        DataColumn Org = new DataColumn("Organization_name", typeof(System.String));
        dtMenu1.Columns.Add(Org);
        DataColumn From = new DataColumn("From", typeof(System.String));
        dtMenu1.Columns.Add(From);
        DataColumn To = new DataColumn("To", typeof(System.String));
        dtMenu1.Columns.Add(To);
        DataColumn Des = new DataColumn("Roledesignation", typeof(System.String));
        dtMenu1.Columns.Add(Des);
        DataRow datatRow1 = dtMenu1.NewRow();
        dtMenu1.Rows.Add(datatRow1);
        return dtMenu1;
    }
    public void BindGridemp()
    {
        grdhistory.DataSource = ReturnEmptyDataTableemp();
        grdhistory.DataBind();
        grdhistory.Rows[0].Visible = false;
    }

    protected void btninsert_Click(object sender, EventArgs e)
    {
        try
        {
            string org = ((TextBox)grdhistory.FooterRow.FindControl("txtfnameorg")).Text;
            string from = ((TextBox)grdhistory.FooterRow.FindControl("txtffrom")).Text;
            string to = ((TextBox)grdhistory.FooterRow.FindControl("txtfto")).Text;
            string disignation = ((TextBox)grdhistory.FooterRow.FindControl("txtfdesig")).Text;
            if (org != "" && from != "" && to != "" && disignation != "")
            {
                const string key1 = "emp1";
                DataTable dt2 = Session[key1] as DataTable;
                dtemp = (DataTable)Session["fillhistory"];
                DataRow dr3 = dtemp.NewRow();
                dr3[0] = 0;
                dr3[1] = org;
                dr3[2] = from;
                dr3[3] = to;
                dr3[4] = disignation;
                dtemp.Rows.Add(0,org, from, to, disignation);
                Session[key1] = dtemp;
                grdhistory.DataSource = dtemp;
                grdhistory.DataBind();
                RadioButtonList1.SelectedValue = "Yes";
                grdhistory.Visible = true;
            }
            else
            {
                JavaScript.UPAlert(Page, "Please Enter All Required Data");
            }
        }
        catch
        {
        }
    }

 
    protected void grdhistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdhistory.Rows[e.RowIndex];
        TextBox organistion = (TextBox)row.FindControl("txtnameorg");
        int i = grdhistory.Rows.Count;
        DataTable dt2 = new DataTable();
        int index = Convert.ToInt32(e.RowIndex);
        const string key1 = "emp1";
        dt2 = Session["fillhistory"] as DataTable;
        dt2.Rows.RemoveAt(index);
        if (i != 1)
        {

            this.grdhistory.DataSource = dt2;
            grdhistory.DataBind();
        }
        else
        {
            BindGridemp();
        }
    }
    public DataTable ReturnEmptyDataTableQualifi()
    {
        DataTable dtMenu2 = new DataTable();
        DataColumn id1 = new DataColumn("id", typeof(System.String));
        dtMenu2.Columns.Add(id1);
        DataColumn Clas = new DataColumn("Class", typeof(System.String));
        dtMenu2.Columns.Add(Clas);
        DataColumn Uni = new DataColumn("University_Name", typeof(System.String));
        dtMenu2.Columns.Add(Uni);
        DataColumn From1 = new DataColumn("From", typeof(System.String));
        dtMenu2.Columns.Add(From1);
        DataColumn To1 = new DataColumn("To", typeof(System.String));
        dtMenu2.Columns.Add(To1);
        DataColumn Pers = new DataColumn("Percentage", typeof(System.String));
        dtMenu2.Columns.Add(Pers);
        DataRow datatRow1 = dtMenu2.NewRow();
        dtMenu2.Rows.Add(datatRow1);
        return dtMenu2;
    }
    public void BindQuali()
    {
        grdqualification.DataSource = ReturnEmptyDataTableQualifi();
        grdqualification.DataBind();
        grdqualification.Rows[0].Visible = false;
    }

    protected void btninsert1_Click(object sender, EventArgs e)
    {
        try
        {
            string cls = ((DropDownList)grdqualification.FooterRow.FindControl("ddlfclass")).SelectedValue.ToString();
            string uni = ((TextBox)grdqualification.FooterRow.FindControl("txtfnameunversty")).Text;
            string frm = ((TextBox)grdqualification.FooterRow.FindControl("txtffrom1")).Text;
            string toquali = ((TextBox)grdqualification.FooterRow.FindControl("txtfto1")).Text;
            string pers = ((TextBox)grdqualification.FooterRow.FindControl("txtfpers")).Text;
            if (cls != "" && uni != "" && frm != "" && toquali != "" && pers != "" && cls != "---Select---")
            {
                const string key2 = "emp2";
                DataTable dt2 = Session[key2] as DataTable;
                dtemp1 = (DataTable)Session["fillquali"];
                DataRow dr3 = dtemp1.NewRow();
                dr3[0] = 0;
                dr3[1] = cls;
                dr3[2] = uni;
                dr3[3] = frm;
                dr3[4] = toquali;
                dr3[5] = pers;
                dtemp1.Rows.Add(0,cls, uni, frm, toquali, pers);
                Session[key2] = dtemp1;
                grdqualification.DataSource = dtemp1;
                grdqualification.DataBind();
            }
            else
            {
                JavaScript.UPAlert(Page, "Please Enter All Required Data");
            }
        }
        catch
        {
        }
    }

    
   
    protected void btnback3_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        TabContainer1.ActiveTabIndex = 4;
        txtact.Text = "4";
    }
    protected void btnnext4_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        TabContainer1.ActiveTabIndex = 5;
        txtact.Text = "5";
    }
    
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.Value == "Yes")
        {
            trgrdhistory.Visible = true;
        }
        else
        {
            trgrdhistory.Visible = false;
        }
    }


    protected void grdqualifications_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdqualification.Rows[e.RowIndex];
        TextBox cls1 = (TextBox)row.FindControl("txtclass");
        int i = grdqualification.Rows.Count;
        DataTable dt2 = new DataTable();
        int index = Convert.ToInt32(e.RowIndex);
        const string key2 = "emp2";
        dt2 = Session["fillquali"] as DataTable;
        dt2.Rows.RemoveAt(index);
        if (i != 1)
        {
            this.grdqualification.DataSource = dt2;
            grdqualification.DataBind();
        }
        else
        {
            BindQuali();
        }
    }

    protected void btnback4_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        TabContainer1.ActiveTabIndex = 0;
        txtact.Text = "0";
    }

    public DataTable ReturnEmptyDataTableTQualifi()
    {
        DataTable dtMenu3 = new DataTable();
        DataColumn id = new DataColumn("id", typeof(System.String));
        dtMenu3.Columns.Add(id);
        DataColumn skl = new DataColumn("Technical_Skills", typeof(System.String));
        dtMenu3.Columns.Add(skl);
        DataColumn exp = new DataColumn("experience", typeof(System.String));
        dtMenu3.Columns.Add(exp);
        DataRow datatRow2 = dtMenu3.NewRow();
        dtMenu3.Rows.Add(datatRow2);
        return dtMenu3;
    }
    public void BindTQuali()
    {
        grdTqualifications.DataSource = ReturnEmptyDataTableTQualifi();
        grdTqualifications.DataBind();
        grdTqualifications.Rows[0].Visible = false;
    }

    protected void btninsert2_Click(object sender, EventArgs e)
    {
        try
        {
            string skl1 = ((DropDownList)grdTqualifications.FooterRow.FindControl("ddlfskills")).SelectedValue.ToString();
            string exp1 = ((DropDownList)grdTqualifications.FooterRow.FindControl("ddlexperience")).SelectedValue.ToString();
            if (skl1 != "" && skl1 != "---Select---" && skl1 != "0" && exp1 != "---Select---")
            {
                const string key3 = "emp3";
                DataTable dt3 = Session[key3] as DataTable;
                dtquali = (DataTable)Session["fillTquali"];
                DataRow dr3 = dtquali.NewRow();
                dr3[0] = 0;
                dr3[1] = skl1;
                dr3[2] = exp1;
                dtquali.Rows.Add(0,skl1, exp1);
                Session[key3] = dtquali;
                grdTqualifications.DataSource = dtquali;
                grdTqualifications.DataBind();
            }
            else
            {
                JavaScript.UPAlert(Page, "Please Enter All Required Data");
            }
        }
        catch
        {
        }
    }



    protected void grdTqualifications_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdTqualifications.Rows[e.RowIndex];
        TextBox cls1 = (TextBox)row.FindControl("txtskills");
        int i = grdTqualifications.Rows.Count;
        DataTable dt2 = new DataTable();
        int index = Convert.ToInt32(e.RowIndex);
        const string key3 = "emp3";
        dt2 = Session["fillTquali"] as DataTable;
        dt2.Rows.RemoveAt(index);
        if (i != 1)
        {
            Session["fillTquali"] = dt2;
            this.grdTqualifications.DataSource = dt2;
            grdTqualifications.DataBind();
        }
        else
        {
            BindTQuali();


        }
    }

    protected void grdqualification_DataBound(object sender, EventArgs e)
    {

        var ddl2 = (DropDownList)grdqualification.FooterRow.FindControl("ddlfclass");
        SqlDataAdapter da = new SqlDataAdapter("select Degree_Name from DegreesMaster", con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddl2.DataSource = ds;
        ddl2.DataTextField = "Degree_Name";
        ddl2.DataValueField = "Degree_Name";
        ddl2.DataBind();
        ddl2.Items.Insert(0, new ListItem("---Select---", "0"));

        return;
    }
    protected void grdTqualifications_DataBound(object sender, EventArgs e)
    {

        var ddl3 = (DropDownList)grdTqualifications.FooterRow.FindControl("ddlfskills");
        SqlDataAdapter da = new SqlDataAdapter("select Technical_Skills from TQualiMaster",con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddl3.DataSource = ds;
        ddl3.DataTextField = "Technical_Skills";
        ddl3.DataValueField = "Technical_Skills";
        ddl3.DataBind();
        ddl3.Items.Insert(0, new ListItem("---Select---", "0"));
        if (ViewState["Flag"] == "1")
            ddl3.Items.Add(txtdepartment.Text);
        return;
    }
    public int count = 0;
    public int rcount = 0;
    protected void grdfamilydetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
           
            if (Request.QueryString["Type"] == "1")
            {
                e.Row.Cells[9].Visible = false;
            }
            
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Button btndelete = e.Row.FindControl("btndelete") as Button;
            TextBox txthnme = e.Row.FindControl("txthname") as TextBox;
            TextBox txthdob = e.Row.FindControl("txthdob") as TextBox;
            TextBox txthage = e.Row.FindControl("txthage") as TextBox;
            CheckBox chk = e.Row.FindControl("chkSelect") as CheckBox;
            chk.Attributes.Add("onclick", "Select(this,1," + count + ")");
            count = count + 1;
            if (txthnme.Text=="" && txthdob.Text=="" && txthage.Text=="")
                btndelete.Visible = false;
            else
                btndelete.Visible = true;
        }
       
    }
    protected void grdTqualifications_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

            if (Request.QueryString["Type"] == "1")
            {
                e.Row.Cells[4].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btndelete = e.Row.FindControl("btndelete2") as Button;
            TextBox txtskills = e.Row.FindControl("txtskills") as TextBox;
            TextBox txtexperience = e.Row.FindControl("txtexperience") as TextBox;

            if (txtskills.Text == "" && txtexperience.Text == "")
                btndelete.Visible = false;
            else
                btndelete.Visible = true;
        }
    }
    protected void grdqualification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Request.QueryString["Type"] == "1")
            {
                e.Row.Cells[7].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btndelete = e.Row.FindControl("btndelete1") as Button;
            TextBox txtclass = e.Row.FindControl("txtclass") as TextBox;
            TextBox txtnameunversty = e.Row.FindControl("txtnameunversty") as TextBox;
            TextBox txtfrom1 = e.Row.FindControl("txtfrom1") as TextBox;
            TextBox txtto1 = e.Row.FindControl("txtto1") as TextBox;
            if (txtclass.Text == "" && txtnameunversty.Text == "" && txtfrom1.Text == "" && txtto1.Text == "")
                btndelete.Visible = false;
            else
                btndelete.Visible = true;
        }
    }
    protected void grdchildren_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Request.QueryString["Type"] == "1")
            {
                e.Row.Cells[7].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btndelete = e.Row.FindControl("btndelete") as Button;
            TextBox txtname = e.Row.FindControl("txtname") as TextBox;
            TextBox txtdob = e.Row.FindControl("txtdob") as TextBox;
            TextBox txtage = e.Row.FindControl("txtage") as TextBox;
            TextBox txtgender = e.Row.FindControl("txtgender") as TextBox;
            CheckBox chk = e.Row.FindControl("chkSelect") as CheckBox;

            chk.Attributes.Add("onclick", "Select(this,2," + rcount + ")");
            rcount = rcount + 1;
            if (txtname.Text == "" && txtdob.Text == "" && txtage.Text == "" && txtgender.Text == "")
                btndelete.Visible = false;
            else
                btndelete.Visible = true;
        }
    }
    protected void grdhistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Request.QueryString["Type"] == "1")
            {
                e.Row.Cells[6].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btndelete = e.Row.FindControl("btndelete") as Button;
            TextBox txtnameorg = e.Row.FindControl("txtnameorg") as TextBox;
            TextBox txtfrom = e.Row.FindControl("txtfrom") as TextBox;
            TextBox txtto = e.Row.FindControl("txtto") as TextBox;
            TextBox txtdesig = e.Row.FindControl("txtdesig") as TextBox;
            if (txtnameorg.Text == "" && txtfrom.Text == "" && txtto.Text == "" && txtdesig.Text == "")
                btndelete.Visible = false;
            else
                btndelete.Visible = true;
        }
    }

    protected void grdsalarybreakup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Request.QueryString["Type"] == "1")
            {
                e.Row.Cells[6].Visible = false;                  
            }
        }


    }
    protected void txtbasicy_TextChanged(object sender, EventArgs e)
    {
        NumberStyles style = NumberStyles.AllowDecimalPoint;
        txtbasicm.Enabled = false;
        decimal yearbasic = Decimal.Parse(String.Format("{0:.00}", Convert.ToDecimal(txtbasicy.Text)), style);
        decimal monthlybasic;
        if (txtbasicy.Text != "0.00" || txtbasicy.Text != "")
        {
            monthlybasic = yearbasic / 12;
            txtbasicm.Text = String.Format("{0:.00}", monthlybasic);
            txtgrossm.Text = String.Format("{0:.00}", monthlybasic);
            txtgrossy.Text = String.Format("{0:.00}", yearbasic);
          
        }
        ////txtbasicy.Enabled = false;
        ////ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "Total();", true);
    }
    
    public DataTable ReturnEmptyDataTablesalary()
    {
        DataTable dtMenusal = new DataTable();
        DataColumn ID = new DataColumn("id", typeof(System.Int32));
        dtMenusal.Columns.Add(ID);
        DataColumn desc = new DataColumn("Component_Name", typeof(System.String));
        dtMenusal.Columns.Add(desc);
        //DataColumn chk = new DataColumn("", typeof(System.String));
        //dtMenusal.Columns.Add(chk);
        DataColumn perc = new DataColumn("Percentage", typeof(System.String));
        dtMenusal.Columns.Add(perc);
        DataColumn monthly = new DataColumn("Monthly", typeof(System.String));
        dtMenusal.Columns.Add(monthly);
        DataColumn yearly = new DataColumn("Yearly", typeof(System.String));
        dtMenusal.Columns.Add(yearly);
        DataRow datatRow1 = dtMenusal.NewRow();
        dtMenusal.Rows.Add(datatRow1);
        return dtMenusal;
    }
    //public void BindSalary()
    //{
    //    grdsalarybreakup.DataSource = ReturnEmptyDataTablesalary();
    //    grdsalarybreakup.DataBind();
    //}

    protected void grdsalarybreakup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdsalarybreakup.Rows[e.RowIndex];
        TextBox des = (TextBox)row.FindControl("txtdescription");
        int i = grdsalarybreakup.Rows.Count;
        DataTable dt2 = new DataTable();
        int index = Convert.ToInt32(e.RowIndex);
        const string key2 = "empsal";
        dt2 = Session["fillsalary"] as DataTable;
        dt2.Rows.RemoveAt(index);
        if (i != 1)
        {
            Session["fillsalary"] = dt2;
            this.grdsalarybreakup.DataSource = dt2;
            grdsalarybreakup.DataBind();
        }
        else
        {
            //BindSalary();
        }
    }
    protected void btninsertrecord_Click(object sender, EventArgs e)
    {
        try
        {
            string id = ((DropDownList)grdsalarybreakup.FooterRow.FindControl("ddlfdescription")).SelectedItem.Value;
            string des = ((DropDownList)grdsalarybreakup.FooterRow.FindControl("ddlfdescription")).SelectedItem.Text;
            string per = ((TextBox)grdsalarybreakup.FooterRow.FindControl("txtfpercentage")).Text;
            string mnth = string.Format("{0:.##}",Convert.ToDecimal(((TextBox)grdsalarybreakup.FooterRow.FindControl("txtfmonthly")).Text));
            string year = string.Format("{0:.##}",Convert.ToDecimal(((TextBox)grdsalarybreakup.FooterRow.FindControl("txtfyearly")).Text));
            var mstatus = ddlmartialst.SelectedItem.Text;
               DataTable dtfillsal = (DataTable)Session["fillsalary"];
            int i=0;
               foreach (DataRow row in dtfillsal.Rows)
               {
                   TextBox txtmonthly = grdsalarybreakup.Rows[i].FindControl("txtmonthly") as TextBox;
                   TextBox txtyearly = grdsalarybreakup.Rows[i].FindControl("txtyearly") as TextBox;
                   row[3] = txtmonthly.Text;
                   row[4] = txtyearly.Text;
                   dtfillsal.AcceptChanges();
                   row.SetModified();
                   i = i + 1;
               }

            if (des != "" && mnth != "" && year != "" && des != "---Select---")
            {

                if (per != "")
                {
                    const string key2 = "empsal";
                    DataTable dt2 = Session[key2] as DataTable;
                    dtemp1 = (DataTable)Session["fillsalary"];
                    DataRow dr3 = dtemp1.NewRow();
                    dr3[0] = id;
                    dr3[1] = des;
                    //dr3[2] = "";
                    dr3[2] = per;
                    dr3[3] = string.Format("{0:.##}", Convert.ToDecimal(mnth));
                    dr3[4] = string.Format("{0:.##}", Convert.ToDecimal(year));
                    dtemp1.Rows.Add(id, des, per, mnth, year);
                    Session[key2] = dtemp1;
                    grdsalarybreakup.DataSource = dtemp1;
                    grdsalarybreakup.DataBind();
                }
                else
                {
                    const string key2 = "empsal";
                    DataTable dt2 = Session[key2] as DataTable;
                    dtemp1 = (DataTable)Session["fillsalary"];
                    DataRow dr3 = dtemp1.NewRow();
                    dr3[0] = id;
                    dr3[1] = des;
                    //dr3[2] = "";
                    dr3[2] = "0";
                    dr3[3] = string.Format("{0:.##}", Convert.ToDecimal(mnth));
                    dr3[4] = string.Format("{0:.##}", Convert.ToDecimal(year));
                    dtemp1.Rows.Add(id, des, "0", mnth, year);
                    Session[key2] = dtemp1;
                    grdsalarybreakup.DataSource = dtemp1;
                    grdsalarybreakup.DataBind();

                }

                //ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "Total();", true);
            }
            else
            {
                JavaScript.UPAlert(Page, "Please Enter All Required Data");
            }
        }
        catch
        {
        }
    }
    protected void grdsalarybreakup_DataBound(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            var percentage = (TextBox)grdsalarybreakup.FooterRow.FindControl("txtfpercentage");
            var mnthly = (TextBox)grdsalarybreakup.FooterRow.FindControl("txtfmonthly");
            var yarly = (TextBox)grdsalarybreakup.FooterRow.FindControl("txtfyearly");
            percentage.Enabled = false;
            mnthly.Enabled = true;
            yarly.Enabled = true;
            var ddl5 = (DropDownList)grdsalarybreakup.FooterRow.FindControl("ddlfdescription");
            SqlCommand cmd = new SqlCommand("     id,Component_Name from salaryBreakUp_components", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddl5.DataSource = ds;
            ddl5.DataTextField = "Component_Name";
            ddl5.DataValueField = "id";
            ddl5.DataBind();
            ddl5.Items.Insert(0, new ListItem("---Select---", "0"));
            con.Close();
            da = new SqlDataAdapter("select cast(Staff_Pfemployer as decimal(10,2))Staff_Pfemployer from hr_rules where dbo.FinancialYear(getdate())=Fiacialyear and Fiacialyear is not null", con);
            da.Fill(ds, "pfvalue");
            if (ds.Tables["pfvalue"].Rows.Count > 0)
            {
                pfemployerper.Value = ds.Tables["pfvalue"].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        { 
        
        }
        return;
    }
    protected void btnsubmit_Click1(object sender, EventArgs e)
    {
        int j = 0;
        string names = "";
        string dob = "";
        string age = "";
        string gender = "";
        string maritalstatus = "";
        string organisation = "";
        string from = "";
        string to = "";
        string roleordesig = "";
        string cls1 = "";
        string uni1 = "";
        string from1 = "";
        string to1 = "";
        string per1 = "";
        string skil = "";
        string exp = "";
        string description = "";
        string Percentage = "";
        string monthlysal = "";
        string yearlysal = "";
        string cid = "";
        string relation = "";
        string workno = "";
        string mobno = "";
        loginbll objbl = new loginbll();
        if (Request.QueryString["Type"] == "1" || Request.QueryString["Type"] == "2")
        {
            EmployeeDetails();
        }
        else
        {
            if (ddlstatus.SelectedItem.Value != "0")
             {
                da = new SqlDataAdapter("EmployeeRegistration_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@firstname", SqlDbType.VarChar).Value = txtfname.Text;
                da.SelectCommand.Parameters.AddWithValue("@lastname", SqlDbType.VarChar).Value = txtlname.Text;
                da.SelectCommand.Parameters.AddWithValue("@middlename", SqlDbType.VarChar).Value = txtmidddle.Text;
                da.SelectCommand.Parameters.AddWithValue("@empdob", SqlDbType.VarChar).Value = txtempdob.Text;
                da.SelectCommand.Parameters.AddWithValue("@empage", SqlDbType.VarChar).Value = txtempage.Text;
                da.SelectCommand.Parameters.AddWithValue("@martialstatus", SqlDbType.VarChar).Value = ddlmartialst.SelectedValue;
                da.SelectCommand.Parameters.AddWithValue("@sex", SqlDbType.VarChar).Value = ddlsex.SelectedValue;
                da.SelectCommand.Parameters.AddWithValue("@nominee", SqlDbType.VarChar).Value = txtnomineename.Text;
                da.SelectCommand.Parameters.AddWithValue("@relation", SqlDbType.VarChar).Value = txtrelation.Text;
                da.SelectCommand.Parameters.AddWithValue("@NomineeAge", SqlDbType.VarChar).Value = txtnage.Text;
                da.SelectCommand.Parameters.AddWithValue("@NomineeDOB", SqlDbType.VarChar).Value = txtndob.Text;
                da.SelectCommand.Parameters.AddWithValue("@permanentaddress", SqlDbType.VarChar).Value = txtadd.Text;
                da.SelectCommand.Parameters.AddWithValue("@temporaryaddress", SqlDbType.VarChar).Value = txttempaddress.Text;
                da.SelectCommand.Parameters.AddWithValue("@phone", SqlDbType.VarChar).Value = txtwph1.Text +"-"+ txtwph2.Text;
                da.SelectCommand.Parameters.AddWithValue("@mobile", SqlDbType.VarChar).Value = txtmph1.Text;
                da.SelectCommand.Parameters.AddWithValue("@birthplace", SqlDbType.VarChar).Value = txtbirthplace.Text;
                da.SelectCommand.Parameters.AddWithValue("@mailid", SqlDbType.VarChar).Value = txtemail.Text;               
                da.SelectCommand.Parameters.AddWithValue("@category", SqlDbType.VarChar).Value = ddlcategory.SelectedValue;
                da.SelectCommand.Parameters.AddWithValue("@appointordesc", SqlDbType.VarChar).Value = ddlrole.SelectedItem.Text;
                da.SelectCommand.Parameters.AddWithValue("@joiningdate", SqlDbType.VarChar).Value = txtjdate.Text;
                da.SelectCommand.Parameters.AddWithValue("@transitdays", SqlDbType.Int).Value = ddltransitdays.SelectedItem.Value;
                da.SelectCommand.Parameters.AddWithValue("@roleid", SqlDbType.VarChar).Value = txtroleid.Text;
                da.SelectCommand.Parameters.AddWithValue("@pwd", SqlDbType.VarChar).Value = objbl.encryptPass(lblpwd.Text);

                da.SelectCommand.Parameters.AddWithValue("@department", SqlDbType.VarChar).Value = ddldepartment.SelectedValue;
                da.SelectCommand.Parameters.AddWithValue("@jobtype", SqlDbType.VarChar).Value = ddlcat.SelectedValue;
                if(ddlmartialst.SelectedItem.Text!="Single")
                da.SelectCommand.Parameters.AddWithValue("@marriagedate", SqlDbType.VarChar).Value = txtdom.Text;
               
               da.SelectCommand.Parameters.AddWithValue("@Roles", Session["roles"].ToString());

                if(ddlempstatus.SelectedValue=="ReJoin")
                {
                    da.SelectCommand.Parameters.AddWithValue("@employeestatus", "Rejoin");
                    da.SelectCommand.Parameters.AddWithValue("@Type","4");
                    da.SelectCommand.Parameters.AddWithValue("@empoldid", SqlDbType.VarChar).Value = ddloldid.SelectedValue;
                  
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
                    da.SelectCommand.Parameters.AddWithValue("@Type","3");
                }
                if (Session["roles"].ToString() == "Hr.Asst")
                {
                    da.SelectCommand.Parameters.AddWithValue("@approvestatus", "1");
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@AccountNO", SqlDbType.VarChar).Value = txtacnumber.Text;
                    da.SelectCommand.Parameters.AddWithValue("@BankName", SqlDbType.VarChar).Value = txtbankname.Text;
                    da.SelectCommand.Parameters.AddWithValue("@BankAddress", SqlDbType.VarChar).Value = txtbankaddress.Text;
                    da.SelectCommand.Parameters.AddWithValue("@IFSCCode", SqlDbType.VarChar).Value = txtifsccode.Text;
                    da.SelectCommand.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                }
                da.SelectCommand.Parameters.AddWithValue("@CUser", Session["user"].ToString());
                da.SelectCommand.Parameters.AddWithValue("@CDate",SqlDbType.DateTime).Value=DateTime.Now.ToString();
                da.SelectCommand.Parameters.AddWithValue("@Comments",SqlDbType.VarChar).Value = txtcomment.Text;

            
                    if (RadioButtonList1.SelectedItem.Value == "Yes")
                    {
                        da.SelectCommand.Parameters.AddWithValue("@employeexpe", "Experience");
                    }
                    else if (RadioButtonList1.SelectedItem.Value == "No")
                    {
                        da.SelectCommand.Parameters.AddWithValue("@employeexpe", "Fresher");
                    }
               
                da.Fill(ds, "empdata");

                con.Close();
                if (ds.Tables["empdata"].Rows[0].ItemArray[0].ToString() == "Inserted Sucessfully")
                {
                    string empid = ds.Tables["empdata"].Rows[0].ItemArray[1].ToString();
                    foreach (GridViewRow record in grdfamilydetails.Rows)
                    {
                        const string key = "empfamily";
                        DataTable dt4 = Session[key] as DataTable;
                        if (dt4 != null)
                        {
                            names = dt4.Rows[record.DataItemIndex].ItemArray[1].ToString();
                            dob = dt4.Rows[record.DataItemIndex].ItemArray[2].ToString();
                            age = dt4.Rows[record.DataItemIndex].ItemArray[3].ToString();
                            gender = dt4.Rows[record.DataItemIndex].ItemArray[4].ToString();
                            relation = dt4.Rows[record.DataItemIndex].ItemArray[5].ToString();
                            workno = dt4.Rows[record.DataItemIndex].ItemArray[6].ToString();
                            mobno = dt4.Rows[record.DataItemIndex].ItemArray[7].ToString();
                            if (names != "")
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "EmployeeFamilyDetails_sp";
                                //cmd.CommandText = "Insert into Employees_childrendetails (Employee_id,Children_name,Children_dob,Age,Gender,Martial_status,Created_by,Created_date,status)" + "values(@Employee_id,@children_name,@children_dob,@Age,@gender,@Martial_status,@CUser,@CDate,@status)";
                                cmd.Parameters.AddWithValue("@ED_FId", empid);
                                cmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = names;
                                cmd.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = dob;
                                cmd.Parameters.AddWithValue("@Age", SqlDbType.VarChar).Value = age;
                                cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar).Value = gender;
                                cmd.Parameters.AddWithValue("@Relation", SqlDbType.VarChar).Value = relation;
                                cmd.Parameters.AddWithValue("@Workno", SqlDbType.VarChar).Value = workno;
                                cmd.Parameters.AddWithValue("@Mobileno", SqlDbType.VarChar).Value = mobno;
                                cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                                if (Session["roles"].ToString() == "Hr.Asst")
                                {
                                    cmd.Parameters.AddWithValue("@approvestatus", "1");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                                }
                                if (ddlempstatus.SelectedValue == "ReJoin")
                                {
                                    cmd.Parameters.AddWithValue("@Type", "4");
                                    cmd.Parameters.AddWithValue("@empoldid", SqlDbType.VarChar).Value = ddloldid.SelectedValue;
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Type", "3");
                                }
                                cmd.Connection = con;
                                con.Open();
                                bool k = Convert.ToBoolean(cmd.ExecuteNonQuery());
                                if (k != true)
                                {
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.CommandText = "Deleteemployeedetails_sp";
                                    cmd1.Parameters.AddWithValue("@id", empid);
                                    cmd1.Connection = con;
                                    con.Open();
                                    cmd1.ExecuteNonQuery();
                                    con.Close();
                                }
                                con.Close();
                            }
                        }
                    }

                    foreach (GridViewRow rec1 in grdqualification.Rows)
                    {
                        const string key2 = "emp2";
                        DataTable dt5 = Session[key2] as DataTable;
                        if (dt5 != null)
                        {
                            cls1 = dt5.Rows[rec1.DataItemIndex].ItemArray[1].ToString();
                            uni1 = dt5.Rows[rec1.DataItemIndex].ItemArray[2].ToString();
                            from1 = dt5.Rows[rec1.DataItemIndex].ItemArray[3].ToString();
                            to1 = dt5.Rows[rec1.DataItemIndex].ItemArray[4].ToString();
                            per1 = dt5.Rows[rec1.DataItemIndex].ItemArray[5].ToString();
                            if (cls1 != "")
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "EmployeeQualification_sp";
                                cmd.Parameters.AddWithValue("@ED_QId", empid);
                                cmd.Parameters.AddWithValue("@Class", SqlDbType.VarChar).Value = cls1;
                                cmd.Parameters.AddWithValue("@University_name", SqlDbType.VarChar).Value = uni1;
                                cmd.Parameters.AddWithValue("@From", SqlDbType.VarChar).Value = from1;
                                cmd.Parameters.AddWithValue("@To", SqlDbType.VarChar).Value = to1;
                                cmd.Parameters.AddWithValue("@Percentage", SqlDbType.VarChar).Value = per1;
                                cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                                if (Session["roles"].ToString() == "Hr.Asst")
                                {
                                    cmd.Parameters.AddWithValue("@approvestatus", "1");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                                }
                                if (ddlempstatus.SelectedValue == "ReJoin")
                                {
                                    cmd.Parameters.AddWithValue("@Type", "4");
                                    cmd.Parameters.AddWithValue("@empoldid",SqlDbType.VarChar).Value=ddloldid.SelectedValue;
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Type", "3");
                                }

                                cmd.Connection = con;
                                con.Open();
                                bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                                if (i != true)
                                {
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.CommandText = "Deleteemployeedetails_sp";
                                    cmd1.Parameters.AddWithValue("@id", empid);
                                    cmd1.Connection = con;
                                    con.Open();
                                    cmd1.ExecuteNonQuery();
                                    con.Close();
                                }
                                con.Close();
                            }
                        }
                    }

                    foreach (GridViewRow rec2 in grdTqualifications.Rows)
                    {
                        const string key3 = "emp3";
                        DataTable dt6 = Session[key3] as DataTable;
                        if (dt6 != null)
                        {
                            skil = dt6.Rows[rec2.DataItemIndex].ItemArray[1].ToString();
                            exp = dt6.Rows[rec2.DataItemIndex].ItemArray[2].ToString();

                            if (skil != "")
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "EmployeeTQualification_sp";
                                cmd.Parameters.AddWithValue("@ED_TQId", empid);
                                cmd.Parameters.AddWithValue("@Technical_Skills", SqlDbType.VarChar).Value = skil;
                                cmd.Parameters.AddWithValue("@Experience", SqlDbType.VarChar).Value = exp;
                                cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                                if (Session["roles"].ToString() == "Hr.Asst")
                                {
                                    cmd.Parameters.AddWithValue("@approvestatus", "1");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                                }
                                if (ddlempstatus.SelectedValue == "ReJoin")
                                {
                                    cmd.Parameters.AddWithValue("@Type", "4");
                                    cmd.Parameters.AddWithValue("@empoldid",SqlDbType.VarChar).Value=ddloldid.SelectedValue;
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Type", "3");
                                }
                                //cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = null;
                                //cmd.Parameters.AddWithValue("@MDate", SqlDbType.VarChar).Value=null;

                                cmd.Connection = con;
                                con.Open();
                                bool m = Convert.ToBoolean(cmd.ExecuteNonQuery());
                                if (m != true)
                                {
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.CommandText = "Deleteemployeedetails_sp";
                                    cmd1.Parameters.AddWithValue("@id", empid);
                                    cmd1.Connection = con;
                                    con.Open();
                                    cmd1.ExecuteNonQuery();
                                    con.Close();
                                }
                                con.Close();
                            }
                        }
                    }

                    if (ddlmartialst.SelectedItem.Text != "Single")
                    {
                        foreach (GridViewRow record in grdchildren.Rows)
                        {
                            const string key = "emp";
                            DataTable dt4 = Session[key] as DataTable;
                            if (dt4 != null)
                            {
                                names = dt4.Rows[record.DataItemIndex].ItemArray[1].ToString();
                                dob = dt4.Rows[record.DataItemIndex].ItemArray[2].ToString();
                                age = dt4.Rows[record.DataItemIndex].ItemArray[3].ToString();
                                gender = dt4.Rows[record.DataItemIndex].ItemArray[4].ToString();
                                maritalstatus = dt4.Rows[record.DataItemIndex].ItemArray[5].ToString();

                                if (names != "")
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "EmployeeChildrens_sp";
                                    //cmd.CommandText = "Insert into Employees_childrendetails (Employee_id,Children_name,Children_dob,Age,Gender,Martial_status,Created_by,Created_date,status)" + "values(@Employee_id,@children_name,@children_dob,@Age,@gender,@Martial_status,@CUser,@CDate,@status)";
                                    cmd.Parameters.AddWithValue("@ED_CId", empid);
                                    cmd.Parameters.AddWithValue("@children_name", SqlDbType.VarChar).Value = names;
                                    //da.SelectCommand.Parameters.AddWithValue("@username",SqlDbType.VarChar).Value=
                                    cmd.Parameters.AddWithValue("@children_dob", SqlDbType.VarChar).Value = dob;
                                    cmd.Parameters.AddWithValue("@Age", SqlDbType.VarChar).Value = age;
                                    cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar).Value = gender;
                                    cmd.Parameters.AddWithValue("@Martial_status", SqlDbType.VarChar).Value = maritalstatus;
                                    cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                    cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                    cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                    cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                    cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                                    if (Session["roles"].ToString() == "Hr.Asst")
                                    {
                                        cmd.Parameters.AddWithValue("@approvestatus", "1");
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                                    }
                                    if (ddlempstatus.SelectedValue == "ReJoin")
                                    {
                                        cmd.Parameters.AddWithValue("@Type", "4");
                                        cmd.Parameters.AddWithValue("@empoldid", SqlDbType.VarChar).Value=ddloldid.SelectedValue;
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Type", "3");
                                    }
                                    cmd.Connection = con;
                                    con.Open();
                                    bool k = Convert.ToBoolean(cmd.ExecuteNonQuery());
                                    if (k != true)
                                    {
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.CommandText = "Deleteemployeedetails_sp";
                                        cmd1.Parameters.AddWithValue("@id", empid);
                                        cmd1.Connection = con;
                                        con.Open();
                                        cmd1.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    con.Close();
                                }
                            }
                        }
                    }
                    if (RadioButtonList1.SelectedItem.Value == "Yes")
                    {
                        foreach (GridViewRow rec in grdhistory.Rows)
                        {
                            const string key2 = "emp1";
                            DataTable dt8 = Session[key2] as DataTable;
                            if (dt8 != null)
                            {
                                organisation = dt8.Rows[rec.DataItemIndex].ItemArray[1].ToString();
                                from = dt8.Rows[rec.DataItemIndex].ItemArray[2].ToString();
                                to = dt8.Rows[rec.DataItemIndex].ItemArray[3].ToString();
                                roleordesig = dt8.Rows[rec.DataItemIndex].ItemArray[4].ToString();

                                if (organisation != "")
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "EmployeeHistory_sp";
                                    cmd.Parameters.AddWithValue("@ED_HId", empid);
                                    cmd.Parameters.AddWithValue("@Organization_name", SqlDbType.VarChar).Value = organisation;
                                    cmd.Parameters.AddWithValue("@From", SqlDbType.VarChar).Value = from;
                                    cmd.Parameters.AddWithValue("@To", SqlDbType.VarChar).Value = to;
                                    cmd.Parameters.AddWithValue("@Roledesignation", SqlDbType.VarChar).Value = roleordesig;
                                    cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                    cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                    cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                    cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                    cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                                    if (Session["roles"].ToString() == "Hr.Asst")
                                    {
                                        cmd.Parameters.AddWithValue("@approvestatus", "1");
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                                    }
                                    if (ddlempstatus.SelectedValue == "ReJoin")
                                    {
                                        cmd.Parameters.AddWithValue("@Type", "4");
                                        cmd.Parameters.AddWithValue("@empoldid", SqlDbType.VarChar).Value = ddloldid.SelectedValue;
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Type", "3");
                                    }
                                    cmd.Connection = con;
                                    con.Open();
                                    bool l = Convert.ToBoolean(cmd.ExecuteNonQuery());
                                    if (l != true)
                                    {
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.CommandText = "Deleteemployeedetails_sp";
                                        cmd1.Parameters.AddWithValue("@id", empid);
                                        cmd1.Connection = con;
                                        con.Open();
                                        cmd1.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    con.Close();
                                }
                            }
                        }
                    }

                   
                    if (ddlempstatus.SelectedValue == "ReJoin")
                    {
                         //HttpFileCollection fileCollection = Request.Files;
                         //for (int i = 0; i < fileCollection.Count; i++)
                         //{
                         //    HttpPostedFile uploadfile = fileCollection[i];
                         //    string fileName = Path.GetFileName(uploadfile.FileName);
                         //    string[] s = new string[13] { "Photo", "BankDetails", "SSLC", "Inter", "Degree", "PG", "Other", "JobApplication", "BioData", "IDProof", "Form2A", "Form2B", "Form11" };
                         //    if (uploadfile.ContentLength > 0)
                         //    {
                        cmd.Parameters.Clear();
                         //        byte[] imageBytes = new byte[uploadfile.ContentLength];
                         //        uploadfile.InputStream.Read(imageBytes, 0, uploadfile.ContentLength);

                                 cmd.CommandType = CommandType.StoredProcedure;
                                 cmd.CommandText = "Employees_UploadDocuments_sp";
                                 cmd.Parameters.AddWithValue("@ED_UId", empid);

                                 //cmd.Parameters.AddWithValue("@FileName", s.ToList()[i]);
                                 //cmd.Parameters.AddWithValue("@Image", SqlDbType.VarBinary).Value = imageBytes;

                                 cmd.Parameters.AddWithValue("@Type", "4");
                                 cmd.Parameters.AddWithValue("@empoldid", SqlDbType.VarChar).Value = ddloldid.SelectedValue;
                                 cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();

                                 if (Session["roles"].ToString() == "Hr.Asst")
                                 {
                                     cmd.Parameters.AddWithValue("@approvestatus", "1");
                                     cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                     cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value =Convert.ToDateTime(DateTime.Now.ToString());
                                     cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                     cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                 }
                                 else
                                 {
                                     cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                                     cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                     cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                     cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                     cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                                 }
                                 cmd.Connection = con;
                                 con.Open();
                                 bool y = Convert.ToBoolean(cmd.ExecuteNonQuery());
                                 if (y != true)
                                 {
                                     SqlCommand cmd1 = new SqlCommand();
                                     cmd1.CommandType = CommandType.StoredProcedure;
                                     cmd1.CommandText = "Deleteemployeedetails_sp";
                                     cmd1.Parameters.AddWithValue("@id", empid);
                                     cmd1.Connection = con;
                                     con.Open();
                                     cmd1.ExecuteNonQuery();
                                     con.Close();
                                 }
                                 con.Close();
                         //    }
                         //}
                    }
                    else
                    {
                    
                        HttpFileCollection fileCollection = Request.Files;
                        for (int i = 0; i < fileCollection.Count; i++)
                        {
                            HttpPostedFile uploadfile = fileCollection[i];
                            string fileName = Path.GetFileName(uploadfile.FileName);
                            string[] s = new string[14] { "Photo", "BankDetails", "SSLC", "Inter", "PreDegree", "Degree", "PG", "Other", "JobApplication", "BioData", "IDProof", "Form2A", "Form2B", "Form11" };
                            if (uploadfile.ContentLength > 0)
                            {
                                cmd.Parameters.Clear();
                                byte[] imageBytes = new byte[uploadfile.ContentLength];
                                uploadfile.InputStream.Read(imageBytes, 0, uploadfile.ContentLength);

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "Employees_UploadDocuments_sp";
                                cmd.Parameters.AddWithValue("@ED_UId", empid);
                                cmd.Parameters.AddWithValue("@FileName", s.ToList()[i]);
                                cmd.Parameters.AddWithValue("@Image", SqlDbType.VarBinary).Value = imageBytes;
                                cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                cmd.Parameters.AddWithValue("@CDate", SqlDbType.VarChar).Value = DateTime.Now.ToString();
                                cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                                cmd.Parameters.AddWithValue("@MDate", SqlDbType.VarChar).Value = DateTime.Now.ToString();
                                cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                               
                                if (Session["roles"].ToString() == "Hr.Asst")
                                {
                                    cmd.Parameters.AddWithValue("@approvestatus", "1");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                                }
                                //if (ddlempstatus.SelectedValue == "ReJoin")
                                //{
                                //    cmd.Parameters.AddWithValue("@Type", "4");
                                //    cmd.Parameters.AddWithValue("@empoldid", SqlDbType.VarChar).Value = ddloldid.SelectedValue;
                                //}
                                //else
                                //{
                                    cmd.Parameters.AddWithValue("@Type", "3");
                                //}
                                cmd.Connection = con;
                                con.Open();
                                bool p = Convert.ToBoolean(cmd.ExecuteNonQuery());
                                if (p != true)
                                {
                                    SqlCommand cmd1 = new SqlCommand();
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.CommandText = "Deleteemployeedetails_sp";
                                    cmd1.Parameters.AddWithValue("@id", empid);
                                    cmd1.Connection = con;
                                    con.Open();
                                    cmd1.ExecuteNonQuery();
                                    con.Close();
                                }
                                con.Close();
                            }
                        }
                    }
                    if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                    {
                        Sendmail();
                    }
                    JavaScript.UPAlertRedirect(Page, "Inserted Sucessfully...", "ViewEmployeeDetails.aspx");
                }
            }
           else if (ddlstatus.SelectedItem.Value == "0")
            {
                JavaScript.UPAlert(Page, "You Must Select Employee Status! ");
            }
       }
   }


    private void EmployeeDetails()
    {
        int k = 0;
        string description = "";
        string Percentage = "";
        string monthlysal = "";
        string yearlysal = "";
        string id4 = "";
        string ids14 = "";
        loginbll objbl = new loginbll();

        da = new SqlDataAdapter("UpdateEmployeeRegistration_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@firstname", SqlDbType.VarChar).Value = txtfname.Text;
        da.SelectCommand.Parameters.AddWithValue("@lastname", SqlDbType.VarChar).Value = txtlname.Text;
        da.SelectCommand.Parameters.AddWithValue("@middlename", SqlDbType.VarChar).Value = txtmidddle.Text;
        da.SelectCommand.Parameters.AddWithValue("@empdob", SqlDbType.VarChar).Value = txtempdob.Text;
        da.SelectCommand.Parameters.AddWithValue("@empage", SqlDbType.VarChar).Value = txtempage.Text;
        da.SelectCommand.Parameters.AddWithValue("@martialstatus", SqlDbType.VarChar).Value = ddlmartialst.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@sex", SqlDbType.VarChar).Value = ddlsex.SelectedValue;
        if (ddlmartialst.SelectedItem.Text != "Single")
        {
            da.SelectCommand.Parameters.AddWithValue("@marriagedate", SqlDbType.VarChar).Value = txtdom.Text;
        }
        da.SelectCommand.Parameters.AddWithValue("@nominee", SqlDbType.VarChar).Value = txtnomineename.Text;
        da.SelectCommand.Parameters.AddWithValue("@relation", SqlDbType.VarChar).Value = txtrelation.Text;
        da.SelectCommand.Parameters.AddWithValue("@NomineeAge", SqlDbType.VarChar).Value = txtnage.Text;
        da.SelectCommand.Parameters.AddWithValue("@NomineeDOB", SqlDbType.VarChar).Value = txtndob.Text;
        da.SelectCommand.Parameters.AddWithValue("@permanentaddress", SqlDbType.VarChar).Value = txtadd.Text;
        da.SelectCommand.Parameters.AddWithValue("@temporaryaddress", SqlDbType.VarChar).Value = txttempaddress.Text;
        da.SelectCommand.Parameters.AddWithValue("@phone", SqlDbType.VarChar).Value = txtwph1.Text + "-" + txtwph2.Text;
        da.SelectCommand.Parameters.AddWithValue("@mobile", SqlDbType.VarChar).Value = txtmph1.Text;
        da.SelectCommand.Parameters.AddWithValue("@birthplace", SqlDbType.VarChar).Value = txtbirthplace.Text;
        da.SelectCommand.Parameters.AddWithValue("@mailid", SqlDbType.VarChar).Value = txtemail.Text;
        da.SelectCommand.Parameters.AddWithValue("@category", SqlDbType.VarChar).Value = ddlcategory.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@appointordesc", SqlDbType.VarChar).Value = ddlrole.SelectedItem.Text;
        da.SelectCommand.Parameters.AddWithValue("@joiningdate", SqlDbType.VarChar).Value = txtjdate.Text;
        da.SelectCommand.Parameters.AddWithValue("@transitdays", SqlDbType.Int).Value = ddltransitdays.SelectedItem.Value;
        da.SelectCommand.Parameters.AddWithValue("@department", SqlDbType.VarChar).Value = ddldepartment.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@roleid", SqlDbType.VarChar).Value = txtroleid.Text;
        da.SelectCommand.Parameters.AddWithValue("@pwd", SqlDbType.VarChar).Value = objbl.encryptPass(lblpwd.Text);
        da.SelectCommand.Parameters.AddWithValue("@jobtype", SqlDbType.VarChar).Value = ddlcat.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@Emphistorystatus", SqlDbType.VarChar).Value = RadioButtonList1.SelectedItem.Text;
        if (Session["roles"].ToString() == "Hr.Asst")
        {
            da.SelectCommand.Parameters.AddWithValue("@approvestatus", "1");
        }
        else
        {
            da.SelectCommand.Parameters.AddWithValue("@AccountNO", SqlDbType.VarChar).Value = txtacnumber.Text;
            da.SelectCommand.Parameters.AddWithValue("@BankName", SqlDbType.VarChar).Value = txtbankname.Text;
            da.SelectCommand.Parameters.AddWithValue("@BankAddress", SqlDbType.VarChar).Value = txtbankaddress.Text;
            da.SelectCommand.Parameters.AddWithValue("@IFSCCode", SqlDbType.VarChar).Value = txtifsccode.Text;
            da.SelectCommand.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
        }
        da.SelectCommand.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
        da.SelectCommand.Parameters.AddWithValue("@comments", SqlDbType.VarChar).Value = txtcomment.Text;
        da.SelectCommand.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
        da.SelectCommand.Parameters.AddWithValue("@id", Session["id"].ToString());
        if (ddlstatus.SelectedValue == "2" || ddlstatus.SelectedValue == "3")
        {
            if (ddlstatus.SelectedValue == "2")
            {
                if (Session["roles"].ToString() == "HR")
                    da.SelectCommand.Parameters.AddWithValue("@Status", "Hold by HR");
                if (Session["roles"].ToString() == "HRGM")
                    da.SelectCommand.Parameters.AddWithValue("@Status", "Hold by HRGM");
                if (Session["roles"].ToString() == "SuperAdmin")
                    da.SelectCommand.Parameters.AddWithValue("@Status", "Hold by SA");
                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                    da.SelectCommand.Parameters.AddWithValue("@Status", "Hold by CMD");
                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
                }
            }
            else if (ddlstatus.SelectedValue == "3")
            {
                if (Session["roles"].ToString() == "HR")
                    da.SelectCommand.Parameters.AddWithValue("@Status", "Reject by HR");
                if (Session["roles"].ToString() == "HRGM")
                    da.SelectCommand.Parameters.AddWithValue("@Status", "Reject by HRGM");
                if (Session["roles"].ToString() == "SuperAdmin")
                    da.SelectCommand.Parameters.AddWithValue("@Status", "Reject by SA");
                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                    da.SelectCommand.Parameters.AddWithValue("@Status", "Reject by CMD");

                if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
                }
            }
        }
        else
        {
            if (Session["roles"].ToString() == "HR")
            {
                da.SelectCommand.Parameters.AddWithValue("@Status", "2");
                da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
            }
            else if (Session["roles"].ToString() == "HRGM")
            {
                da.SelectCommand.Parameters.AddWithValue("@Status", "3");
                da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
            }
            if (Session["roles"].ToString() == "SuperAdmin")
            {
                da.SelectCommand.Parameters.AddWithValue("@Status", "3A");
                da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
            }
            if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                da.SelectCommand.Parameters.AddWithValue("@Status", "4");
                da.SelectCommand.Parameters.AddWithValue("@employeestatus", "Existing");
            }
            if (Session["roles"].ToString() == "Hr.Asst")
            {
                da.SelectCommand.Parameters.AddWithValue("@Status", "1");
                da.SelectCommand.Parameters.AddWithValue("@employeestatus", "New");
            }
        }
        da.Fill(ds, "updatemployees");


        //HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
        //for (int i = 0; i < fileCollection.Count; i++)
        //{
        //    HttpPostedFile uploadfile = fileCollection[i];
        //    string fileName = Path.GetFileName(uploadfile.FileName);
        //    if (uploadfile.ContentLength > 0)
        //    {
        //        cmd.Parameters.Clear();
        //        byte[] imageBytes = new byte[uploadfile.ContentLength];
        //        uploadfile.InputStream.Read(imageBytes, 0, uploadfile.ContentLength);
        //        cmd.CommandText = "INSERT INTO employees_uploaddocuments(Employee_id,FileName,Image,status)VALUES (@Employee_id,@FileName,@Image,@status)";
        //        cmd.Parameters.AddWithValue("@Employee_id", SqlDbType.VarChar).Value = Session["Employee_id"].ToString();
        //        cmd.Parameters.AddWithValue("@FileName", "SalarySheet");
        //        cmd.Parameters.AddWithValue("@Image", imageBytes);
        //        cmd.Parameters.AddWithValue("@status", "2");
        //        cmd.Connection = con;
        //        con.Open();
        //        k = k + Convert.ToInt32(cmd.ExecuteNonQuery());
        //        con.Close();
        //    }
        //}
        getfamilydetails();
        getempquali();
        getempTquali();
        getemphistory();


        if (ddlmartialst.SelectedItem.Value == "Married")
        {
            getempchildrens();
            if (Session["roles"].ToString() == "Hr.Asst")
            {
                UPpanelAlertRedirect(Page, "Employee details was modified sucessfully", "viewemployeedetails.aspx");
            }
        }
        if (Session["roles"].ToString() == "Chairman Cum Managing Director")
        {
            Sendmail();
        }
        UPpanelAlertRedirect(Page, "Employee details was modified sucessfully", "viewemployeedetails.aspx");
    }

    private void getempchildrens()
    {
        string names = "";
        string dob = "";
        string age = "";
        string gender = "";
        string maritalstatus = "";
        string id2 = "";
        string ids = "";
        foreach (GridViewRow rec2 in grdchildren.Rows)
        {
            if (grdchildren.DataKeys[rec2.RowIndex]["id"].ToString() != "")
            {
                ids = ids + grdchildren.DataKeys[rec2.RowIndex]["id"].ToString() + ",";
            }
            id2 = id2 + grdchildren.DataKeys[rec2.RowIndex]["id"].ToString() + ",";
            names = names + (rec2.FindControl("txtname") as TextBox).Text + ",";
            dob = dob + (rec2.FindControl("txtdob") as TextBox).Text + ",";
            age = age + (rec2.FindControl("txtage") as TextBox).Text + ",";
            gender = gender + (rec2.FindControl("txtgender") as TextBox).Text + ",";
            maritalstatus = maritalstatus + (rec2.FindControl("txtmstatus") as TextBox).Text + ",";
        }
          if (id2 != "" || id2!=",")
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmployeeChildrens_sp";
                cmd.Parameters.AddWithValue("@ids2", id2);
                cmd.Parameters.AddWithValue("@ids", ids);
                //cmd.CommandText = "Insert into Employees_childrendetails (Employee_id,Children_name,Children_dob,Age,Gender,Martial_status,Created_by,Created_date,status)" + "values(@Employee_id,@children_name,@children_dob,@Age,@gender,@Martial_status,@CUser,@CDate,@status)";
                cmd.Parameters.AddWithValue("@ED_CId", SqlDbType.VarChar).Value = Session["id"].ToString();
                cmd.Parameters.AddWithValue("@children_name", SqlDbType.VarChar).Value = names;
                //da.SelectCommand.Parameters.AddWithValue("@username",SqlDbType.VarChar).Value=
                cmd.Parameters.AddWithValue("@children_dob", SqlDbType.VarChar).Value = dob;
                cmd.Parameters.AddWithValue("@Age", SqlDbType.VarChar).Value = age;
                cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar).Value = gender;
                cmd.Parameters.AddWithValue("@Martial_status", SqlDbType.VarChar).Value = maritalstatus;
                cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                if (Session["roles"].ToString() == "Hr.Asst")
                {
                    cmd.Parameters.AddWithValue("@approvestatus", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                }
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
               
                con.Close();

            }
        
    }

    private void getemphistory()
    {
        string organisation = "";
        string from = "";
        string to = "";
        string roleordesig = "";
        string id3 = "";
        string ids="";
       
            foreach (GridViewRow rec3 in grdhistory.Rows)
            {
                if (grdhistory.DataKeys[rec3.RowIndex]["id"].ToString() != "")
                {
                    ids = ids + grdhistory.DataKeys[rec3.RowIndex]["id"].ToString() + ",";
                }
                id3 = id3 + grdhistory.DataKeys[rec3.RowIndex]["id"].ToString() + ",";
                organisation = organisation + (rec3.FindControl("txtnameorg") as TextBox).Text + ",";
                from = from + (rec3.FindControl("txtfrom") as TextBox).Text + ",";
                to = to + (rec3.FindControl("txtto") as TextBox).Text + ",";
                roleordesig = roleordesig + (rec3.FindControl("txtdesig") as TextBox).Text + ",";
            }



            if (id3 != "" || id3 != ",")
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmployeeHistory_sp";
                cmd.Parameters.AddWithValue("@ids3", id3);
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@ED_HId", SqlDbType.VarChar).Value = Session["id"].ToString();
                cmd.Parameters.AddWithValue("@Organization_name", SqlDbType.VarChar).Value = organisation;
                cmd.Parameters.AddWithValue("@From", SqlDbType.VarChar).Value = from;
                cmd.Parameters.AddWithValue("@To", SqlDbType.VarChar).Value = to;
                cmd.Parameters.AddWithValue("@Roledesignation", SqlDbType.VarChar).Value = roleordesig;
                cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();           
                cmd.Parameters.AddWithValue("@rbtnstatus", SqlDbType.VarChar).Value = RadioButtonList1.SelectedItem.Text;
                //if (Session["roles"].ToString() == "Hr.Asst")
                //{
                //    cmd.Parameters.AddWithValue("@Type", Request.QueryString["Type"].ToString());

                //}
                if (Session["roles"].ToString() == "Hr.Asst")
                {
                    cmd.Parameters.AddWithValue("@approvestatus", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                }
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
       
        
    }

    private void getempTquali()
    {
        string skil = "";
        string exp = "";
        string id = "";
        string ids = "";
        foreach (GridViewRow rec in grdTqualifications.Rows)
        {
            if (grdTqualifications.DataKeys[rec.RowIndex]["id"].ToString() != "")
            {
                ids = ids +  grdTqualifications.DataKeys[rec.RowIndex]["id"].ToString() + ",";
            }
            id = id + grdTqualifications.DataKeys[rec.RowIndex]["id"].ToString() + ",";
            skil = skil + (rec.FindControl("txtskills") as TextBox).Text + ",";
            exp = exp + (rec.FindControl("txtexperience") as TextBox).Text + ",";
        }
           if (id != "" || id!=",")
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmployeeTQualification_sp";
                 cmd.Parameters.AddWithValue("@ids", id);
                 cmd.Parameters.AddWithValue("@Tids", ids);
                 cmd.Parameters.AddWithValue("@ED_TQId", SqlDbType.VarChar).Value = Session["id"].ToString();
                cmd.Parameters.AddWithValue("@Technical_Skills", SqlDbType.VarChar).Value = skil;
                cmd.Parameters.AddWithValue("@Experience", SqlDbType.VarChar).Value = exp;
                cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                if (Session["roles"].ToString() == "Hr.Asst")
                {
                    cmd.Parameters.AddWithValue("@approvestatus", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                
                con.Close();
            }

        
    }

    private void getfamilydetails()
    {
        try
        {
            string names = "";
            string dob = "";
            string age = "";
            string gender = "";
            string relation = "";
            string workno = "";
            string mobno = "";
            string ids = "";
            string fids = "";
            foreach (GridViewRow record in grdfamilydetails.Rows)
            {
                if (grdfamilydetails.DataKeys[record.RowIndex]["id"].ToString() != "")
                {
                    ids = ids + grdfamilydetails.DataKeys[record.RowIndex]["id"].ToString() + ",";
                }
                fids = fids + grdfamilydetails.DataKeys[record.RowIndex]["id"].ToString() + ",";
                names = names + (record.FindControl("txthname") as TextBox).Text + ",";
                dob = dob + (record.FindControl("txthdob") as TextBox).Text + ",";
                age = age + (record.FindControl("txthage") as TextBox).Text + ",";
                gender = gender + (record.FindControl("txthgender") as TextBox).Text + ",";
                relation = relation + (record.FindControl("txthrelation") as TextBox).Text + ",";
                workno = workno + (record.FindControl("txthwno1") as TextBox).Text + ",";
                mobno = mobno + (record.FindControl("txthmobno") as TextBox).Text + ",";
               
            }
            if (fids != "" || fids != ",")
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmployeeFamilyDetails_sp";
                //cmd.CommandText = "Insert into Employees_childrendetails (Employee_id,Children_name,Children_dob,Age,Gender,Martial_status,Created_by,Created_date,status)" + "values(@Employee_id,@children_name,@children_dob,@Age,@gender,@Martial_status,@CUser,@CDate,@status)";
                cmd.Parameters.AddWithValue("@ids2", fids);
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@ED_FId", Session["id"].ToString());
                cmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = names;
                cmd.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = dob;
                cmd.Parameters.AddWithValue("@Age", SqlDbType.VarChar).Value = age;
                cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar).Value = gender;
                cmd.Parameters.AddWithValue("@Relation", SqlDbType.VarChar).Value = relation;
                cmd.Parameters.AddWithValue("@Workno", SqlDbType.VarChar).Value = workno;
                cmd.Parameters.AddWithValue("@Mobileno", SqlDbType.VarChar).Value = mobno;
                cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
                if (Session["roles"].ToString() == "Hr.Asst")
                {
                    cmd.Parameters.AddWithValue("@approvestatus", "1");
                }    
                else
                {
                    cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
                }
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        { 
        
        }
         
    }


 private void getempquali()
    {
        
       string cls1 = "";
        string uni1 = "";
        string from1 = "";
        string to1 = "";
        string per1 = "";
        string ids = "";
        


        foreach (GridViewRow rec1 in grdqualification.Rows)
        {
            if (grdqualification.DataKeys[rec1.RowIndex]["id"].ToString() != "")
            {
                ids = ids + grdqualification.DataKeys[rec1.RowIndex]["id"].ToString() + "," ;
            }
            id1 = id1 + grdqualification.DataKeys[rec1.RowIndex]["id"].ToString() + ",";
            cls1 = cls1 + (rec1.FindControl("txtclass") as TextBox).Text + ",";
            uni1 = uni1 + (rec1.FindControl("txtnameunversty") as TextBox).Text + ",";
            from1 = from1 + (rec1.FindControl("txtfrom1") as TextBox).Text + ",";
            to1 = to1 + (rec1.FindControl("txtto1") as TextBox).Text + ",";
            per1 = per1 + (rec1.FindControl("txtpers") as TextBox).Text + ",";
        }
        if (id1 != "" || id1 != ",")
        {
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EmployeeQualification_sp";
            cmd.Parameters.AddWithValue("@ids1", id1);
            cmd.Parameters.AddWithValue("@ids", ids);
            cmd.Parameters.AddWithValue("@ED_QId", SqlDbType.VarChar).Value = Session["id"].ToString();
            cmd.Parameters.AddWithValue("@Class", SqlDbType.VarChar).Value = cls1;
            cmd.Parameters.AddWithValue("@University_name", SqlDbType.VarChar).Value = uni1;
            cmd.Parameters.AddWithValue("@From", SqlDbType.VarChar).Value = from1;
            cmd.Parameters.AddWithValue("@To", SqlDbType.VarChar).Value = to1;
            cmd.Parameters.AddWithValue("@Percentage", SqlDbType.VarChar).Value = per1;
            cmd.Parameters.AddWithValue("@CUser", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@MUser", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Parameters.AddWithValue("@MDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
            if (Session["roles"].ToString() == "Hr.Asst")
            {
                cmd.Parameters.AddWithValue("@approvestatus", "1");
            }
            else
            {
               cmd.Parameters.AddWithValue("@approvestatus", ddlstatus.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = Request.QueryString["Type"].ToString();
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        
    }
    public static void UPpanelAlertRedirect(Page page, string Message, string TargetUrl)
    {
        string script = @"alert('" + Message + "');window.close();window.location ='" + TargetUrl + "';";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Alert", script, true);
    }

   enum Certificates
    {
        Photo,
        BankDetails,
        SSLC,
        Inter,
        PreDegree,
        Degree,
        PG,
        Other,
        JobApplication,
        BioData,
        IDProof,
        Form2A,
        Form2B,
        Form11
    };

    public void fildocuments()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select Employee_id,ID,FileName from Employees_UploadDocuments where ED_UId='" + Session["id"].ToString() + "' order by id", con);
        da.Fill(ds, "certficates");
        //GridView1.DataSource = ds.Tables["Table"];
        //GridView1.DataBind();
        for (int i = 0; i < ds.Tables["certficates"].Rows.Count; i++)
        {
            if (Certificates.Photo.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkphoto.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString()+"&Type="+Request.QueryString["Type"];
            }
            if (Certificates.BankDetails.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkbank.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.SSLC.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkssc.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.Inter.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkinter.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.PreDegree.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkpredegree.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.Degree.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {

                lnkdegree.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.PG.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkpg.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.Other.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkother.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.JobApplication.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkjb.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.BioData.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkbiodata.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.IDProof.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkid.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.Form2A.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkform21.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.Form2B.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkform22.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
            if (Certificates.Form11.ToString() == ds.Tables["certficates"].Rows[i].ItemArray[2].ToString())
            {
                lnkform11.NavigateUrl = "~/HR/CertificatePreview.aspx?id=" + ds.Tables["certficates"].Rows[i].ItemArray[1].ToString() + "&Type=" + Request.QueryString["Type"];
            }
        }
    }

    private void viewform(string Type)
    {
        da = new SqlDataAdapter("EmployeeDetailsRetrieving_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = Session["id"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@role", Session["roles"].ToString());
        if (Session["roles"].ToString() == "HR" || Session["roles"].ToString() == "Hr.Asst")
        {
            da.SelectCommand.Parameters.AddWithValue("@type", Type);
        }
        da.Fill(ds, "employees");
        if (ds.Tables["employees"].Rows.Count > 0)
        {
            if (ds.Tables["employees"].Rows[0]["Date_of_joining"].ToString() != "")
            {
                DateTime startdate = Convert.ToDateTime(ds.Tables["employees"].Rows[0]["Date_of_joining"].ToString());
                DateTime enddate = Convert.ToDateTime(ds.Tables["employees"].Rows[0]["Date_of_joiningTo"].ToString());
                double numberofdays = (enddate - startdate).TotalDays;

                ddltransitdays.SelectedValue = Convert.ToInt32(numberofdays).ToString();
            }
            if (Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "HRGM" || Session["roles"].ToString() == "Chairman Cum Managing Director" || (Session["roles"].ToString() == "HR" && Type == "1"))
            {
                if (Session["roles"].ToString() == "Chairman Cum Managing Director" && (Type == "2" || Type == "3"))
                {
                    lblpwd.Text = CreatePassword();
                    lbldpwd.Text = CreatePassword();

                    trroleid.Visible = true;
                    trpwd.Visible = true;
                }
                else if (Session["roles"].ToString() == "Chairman Cum Managing Director" && Type == "1")
                {
                    lblpwd.Text = ds.Tables["employees"].Rows[0]["pwd"].ToString();
                    //trroleid.Visible = true;
                    //trpwd.Visible = true;
                }
                else
                {
                    trroleid.Visible = false;
                    trpwd.Visible = false;
                }
                ddlmartialst.SelectedValue = ds.Tables["employees"].Rows[0][3].ToString();
                // txtwhname.Text = ds.Tables["employees"].Rows[0][4].ToString();
                txtemail.Text = ds.Tables["employees"].Rows[0][5].ToString();
                txtmph1.Text = ds.Tables["employees"].Rows[0][6].ToString();

                txtnomineename.Text = ds.Tables["employees"].Rows[0][7].ToString();
                txtndob.Text = ds.Tables["employees"].Rows[0]["Nominee_DOB"].ToString();
                txtnage.Text = ds.Tables["employees"].Rows[0]["Nominee_Age"].ToString();
                txtjdate.Text = ds.Tables["employees"].Rows[0][8].ToString();
                if (Request.QueryString["Type"] == "1") //To View Employee Details
                {
                    ddlcategory.SelectedValue = ds.Tables["employees"].Rows[0][9].ToString();
                    ddlcategory.Enabled = false;
                }
                else {
                    ddlcategory.SelectedValue = ds.Tables["employees"].Rows[0][9].ToString();
                    ddlcategory.Enabled = true;
                }
                CascadingDropDown2.SelectedValue = ds.Tables["employees"].Rows[0][10].ToString();
                ddldepartment.SelectedValue = ds.Tables["employees"].Rows[0][11].ToString();
                ddlcat.Text = ds.Tables["employees"].Rows[0][12].ToString();

                //txtresig.Text = ds.Tables["employees"].Rows[0][15].ToString();
                //txtreason.Text = ds.Tables["employees"].Rows[0][16].ToString();
               
                txtfname.Text = ds.Tables["employees"].Rows[0][20].ToString();
                txtmidddle.Text = ds.Tables["employees"].Rows[0][21].ToString();
                txtlname.Text = ds.Tables["employees"].Rows[0][22].ToString();
                ddlsex.SelectedValue = ds.Tables["employees"].Rows[0][24].ToString();
                txtempdob.Text = ds.Tables["employees"].Rows[0][23].ToString();
                txtempage.Text = ds.Tables["employees"].Rows[0][25].ToString();

                ViewState["martiallstatus"] = ds.Tables["employees"].Rows[0][3].ToString();
                ViewState["employeeexperience"] = ds.Tables["employees"].Rows[0][18].ToString();

               // filsalary();
            }
            else if (Session["roles"].ToString() == "HR" && Type == "2")
            {
                txtjdate.Text = ds.Tables["employees"].Rows[0][8].ToString();
                ddlcategory.SelectedValue = ds.Tables["employees"].Rows[0][9].ToString();
                CascadingDropDown2.SelectedValue = ds.Tables["employees"].Rows[0][10].ToString();
               
                ddldepartment.SelectedValue = ds.Tables["employees"].Rows[0][11].ToString();
                ddlcat.SelectedValue = ds.Tables["employees"].Rows[0][12].ToString();
                //txtreason.Text = ds.Tables["employees"].Rows[0][16].ToString();
                //txtresig.Text = ds.Tables["employees"].Rows[0][15].ToString();
                txtfname.Text = ds.Tables["employees"].Rows[0][20].ToString();
                // txtmname.Text = ds.Tables["employees"].Rows[0][21].ToString();
                txtlname.Text = ds.Tables["employees"].Rows[0][22].ToString();
                txtempdob.Text = ds.Tables["employees"].Rows[0][23].ToString();
                txtempage.Text = ds.Tables["employees"].Rows[0][25].ToString();
                //txtempstatus.Text = ds.Tables["employees"].Rows[0][19].ToString();
                ddlsex.SelectedValue = ds.Tables["employees"].Rows[0][24].ToString();
                txtnomineename.Text = ds.Tables["employees"].Rows[0][7].ToString();
                txtndob.Text = ds.Tables["employees"].Rows[0]["Nominee_DOB"].ToString();
                txtnage.Text = ds.Tables["employees"].Rows[0]["Nominee_Age"].ToString();
                // txtmage.Text = ds.Tables["employees"].Rows[0][28].ToString();
                ViewState["martiallstatus"] = ds.Tables["employees"].Rows[0][3].ToString();
                ViewState["employeeexperience"] = ds.Tables["employees"].Rows[0][18].ToString();
               // filsalary();
            }
            else if (Session["roles"].ToString() == "Hr.Asst")
            {
                ddlmartialst.SelectedValue = ds.Tables["employees"].Rows[0][3].ToString();
                // txtwhname.Text = ds.Tables["employees"].Rows[0][4].ToString();
                txtemail.Text = ds.Tables["employees"].Rows[0][5].ToString();
                txtmph1.Text = ds.Tables["employees"].Rows[0][6].ToString();

                txtnomineename.Text = ds.Tables["employees"].Rows[0][7].ToString();
                txtndob.Text = ds.Tables["employees"].Rows[0]["Nominee_DOB"].ToString();
                txtnage.Text = ds.Tables["employees"].Rows[0]["Nominee_Age"].ToString();
                txtjdate.Text = ds.Tables["employees"].Rows[0][8].ToString();
                if (Request.QueryString["Type"] == "1") //To View Employee Details
                {
                    ddlcategory.SelectedValue = ds.Tables["employees"].Rows[0][9].ToString();
                    ddlcategory.Enabled = false;
                }
                else {
                    ddlcategory.SelectedValue = ds.Tables["employees"].Rows[0][9].ToString();
                    ddlcategory.Enabled = true;

                }
                CascadingDropDown2.SelectedValue = ds.Tables["employees"].Rows[0][10].ToString();
                ddldepartment.SelectedValue = ds.Tables["employees"].Rows[0][11].ToString();
                ddlcat.Text = ds.Tables["employees"].Rows[0][12].ToString();

                //txtresig.Text = ds.Tables["employees"].Rows[0][15].ToString();
                //txtreason.Text = ds.Tables["employees"].Rows[0][16].ToString();
               
                txtfname.Text = ds.Tables["employees"].Rows[0][20].ToString();
                txtmidddle.Text = ds.Tables["employees"].Rows[0][21].ToString();
                txtlname.Text = ds.Tables["employees"].Rows[0][22].ToString();
                ddlsex.SelectedValue = ds.Tables["employees"].Rows[0][24].ToString();
                txtempdob.Text = ds.Tables["employees"].Rows[0][23].ToString();
                txtempage.Text = ds.Tables["employees"].Rows[0][25].ToString();

                ViewState["martiallstatus"] = ds.Tables["employees"].Rows[0][3].ToString();
                ViewState["employeeexperience"] = ds.Tables["employees"].Rows[0][18].ToString();
            }
        }
        
        //ViewState["martiallstatus"] = ds.Tables["employees"].Rows[0][3].ToString();
        //ViewState["employeeexperience"] = ds.Tables["employees"].Rows[0][18].ToString();
        fillFamilydetails();
        fillquali();
        fillTquali();
        filchildren();
        filhistroy();
        filpersonal();
    }

    private void filsalary()
    {
        int cid = 0;
        string des="";
        Decimal perc=0;
        string mnth="";
        string year="";
        var vargrsm="";
        var vargrsy="";
        var varnetm="";
        var varnety="";
        var vardedm="0";
        var vardedy="0";
        int intid=0;

        da = new SqlDataAdapter("select Account_NO,Bank_Name,Bank_Address,IFSC_Code from EmployeeBank_Details where Employee_ID='" + Session["Employee_id"].ToString() + "'", con);
        da.Fill(ds, "Employeebankdetails");
        if (ds.Tables["Employeebankdetails"].Rows.Count > 0)
        {
            txtacnumber.Text = ds.Tables["Employeebankdetails"].Rows[0][0].ToString();
            txtbankname.Text = ds.Tables["Employeebankdetails"].Rows[0][1].ToString();
            txtbankaddress.Text = ds.Tables["Employeebankdetails"].Rows[0][2].ToString();
            txtifsccode.Text = ds.Tables["Employeebankdetails"].Rows[0][3].ToString();
        }

        da = new SqlDataAdapter("Select id,Employee_Id,yearly_Basic,Component_Name,isnull(Percentage,0) as Percentage,Monthly,Yearly from EmployeeSalary_structure where Employee_id='" + Session["Employee_id"].ToString() + "' and Component_Name<>14 ", con);
        //da = new SqlDataAdapter("Select Component_Name,Percentage,Monthly,Yearly from EmployeeSalary_structure where Employee_id='" + Session["Employee_id"].ToString() + "' ", con);
        da.Fill(ds, "EmployeeSalary");
     
        if (ds.Tables["EmployeeSalary"].Rows.Count == 0)
        {
            grdsalarybreakup.DataSource = ReturnEmptyDataTablesalary();
            //Session["fillsalary"] = ReturnEmptyDataTablesalary();
            grdsalarybreakup.DataBind();
        }
        else
            {

                txtgrossy.Text=string.Format("{0:.##}", Convert.ToDecimal(ds.Tables["EmployeeSalary"].Rows[0][2].ToString()));
                txtgrossm.Text =string.Format("{0:.##}", Convert.ToDecimal(txtgrossy.Text) / 12);
                        
           

            for (int i = 0; i < ds.Tables["EmployeeSalary"].Rows.Count; i++)
            {
                txtbasicy.Text = ds.Tables["EmployeeSalary"].Rows[i][2].ToString();
                txtbasicm.Text = string.Format("{0:.##}", Convert.ToDecimal(txtbasicy.Text) / 12);
                intid=Convert.ToInt32(ds.Tables["EmployeeSalary"].Rows[i][0].ToString());
                vargrsm=txtgrossm.Text;
                vargrsy=txtgrossy.Text;
          
                if (vardedm == "")
                {
                    vardedm ="0";
                }
                if (vardedy == "")
                {
                    vardedy = "0";
                }

                cid = Convert.ToInt32(ds.Tables["EmployeeSalary"].Rows[i][3].ToString());
                if (cid != 0)
                {
                    da = new SqlDataAdapter("Select Component_Name from salaryBreakUp_components where id='" + cid + "' ", con);
                    da.Fill(ds, "componentname");
                    des = ds.Tables["componentname"].Rows[0][0].ToString();
                    ds.Tables["componentname"].Clear();
                }
                perc = Convert.ToDecimal(ds.Tables["EmployeeSalary"].Rows[i][4].ToString());
                if (perc != 0)
                {
                    mnth = string.Format("{0:.##}", (Convert.ToDecimal(txtbasicm.Text) * (perc / 100)));
                    year = string.Format("{0:.##}",(Convert.ToDecimal(txtbasicy.Text) * (perc / 100)));
                }
                else
                {
                    perc = 0;
                    mnth = string.Format("{0:.##}",Convert.ToDecimal(ds.Tables["EmployeeSalary"].Rows[i][5].ToString()));
                    year = string.Format("{0:.##}", Convert.ToDecimal(ds.Tables["EmployeeSalary"].Rows[i][6].ToString()));
                }

                const string key8 = "empsal";
                DataTable dt8 = Session[key8] as DataTable;
                dbsal = (DataTable)Session["fillsalary"];

                DataRow dr8 = dbsal.NewRow();
                dr8[0] = cid;
                dr8[1] = des;
                //dr8[2] = "";
                dr8[2] = perc;
                dr8[3] = mnth;
                dr8[4] = year;

                dbsal.Rows.Add(cid,des,perc, mnth, year);
                if(des=="PF" || des=="ESI")
                {
                    txtgrossm.Text = string.Format("{0:.##}",Convert.ToDecimal(vargrsm));
                    txtgrossy.Text = string.Format("{0:.##}", Convert.ToDecimal(vargrsy));
                    //txtdeductionsm.Text = Convert.ToString(Convert.ToDecimal(vardedm) + Convert.ToDecimal(mnth));
                    //txtdeductionsy.Text = Convert.ToString(Convert.ToDecimal(vardedy) + Convert.ToDecimal(year));
                    vardedm = string.Format("{0:.##}",(Convert.ToDecimal(vardedm) + Convert.ToDecimal(mnth)));
                    vardedy = string.Format("{0:.##}",(Convert.ToDecimal(vardedy) + Convert.ToDecimal(year)));
                 
                
                }
                else
                {
                    txtgrossm.Text = string.Format("{0:.##}",(Convert.ToDecimal(vargrsm) + Convert.ToDecimal(mnth)));
                    txtgrossy.Text = string.Format("{0:.##}",(Convert.ToDecimal(vargrsy) + Convert.ToDecimal(year)));
                  
                }

                Session[key8] = dbsal;
                Session["fillsalary"] = dbsal;
                grdsalarybreakup.DataSource = dbsal;
                grdsalarybreakup.DataBind();
            }
        }
    }

    public void filpersonal()
    {
        da = new SqlDataAdapter("select Father_Name,Father_dob,Mother_name,Mother_dob,Martial_status,date_of_marriage,[wife/husband_name],[wife/husband_of_birth],Grandfathers_name,Mothersfather_name,Address,Present_address,phone_no,Mobile_no,Birth_place,mail_id,Nominee_Name,Relation from employee_data  where id='" + Session["id"].ToString() + "' ", con);
        da.Fill(ds, "employeepersonal");
        if (ds.Tables["employeepersonal"].Rows.Count > 0)
        {
         
            //txtmname.Text = ds.Tables["employeepersonal"].Rows[0][2].ToString();
            //txtmdob.Text = ds.Tables["employeepersonal"].Rows[0][3].ToString();
            ddlmartialst.SelectedValue = ds.Tables["employeepersonal"].Rows[0][4].ToString();
            if (ds.Tables["employeepersonal"].Rows[0][4].ToString() != "Single")
            {
                TrDom.Visible = true;
                txtdom.Text = ds.Tables["employeepersonal"].Rows[0][5].ToString();
            }
            else
            {
                TrDom.Visible = false;
            }
            //txtwhname.Text = ds.Tables["employeepersonal"].Rows[0][6].ToString();
            //txtwhdob.Text = ds.Tables["employeepersonal"].Rows[0][7].ToString();
            //txtgfather.Text = ds.Tables["employeepersonal"].Rows[0][8].ToString();
            //txtmfname.Text = ds.Tables["employeepersonal"].Rows[0][9].ToString();
            txtadd.Text = ds.Tables["employeepersonal"].Rows[0][10].ToString();
            txttempaddress.Text = ds.Tables["employeepersonal"].Rows[0][11].ToString();
            if (ds.Tables["employeepersonal"].Rows[0][12].ToString() != "")
            {
                txtwph1.Text = ds.Tables["employeepersonal"].Rows[0][12].ToString().Substring(0, ds.Tables["employeepersonal"].Rows[0][12].ToString().IndexOf('-'));
                int strLength = ds.Tables["employeepersonal"].Rows[0][12].ToString().IndexOf('-') + 1;
                txtwph2.Text = ds.Tables["employeepersonal"].Rows[0][12].ToString().Substring(strLength, ds.Tables["employeepersonal"].Rows[0][12].ToString().Length - (strLength));
            }
            txtmph1.Text = ds.Tables["employeepersonal"].Rows[0][13].ToString();
            txtbirthplace.Text = ds.Tables["employeepersonal"].Rows[0][14].ToString();
            txtemail.Text = ds.Tables["employeepersonal"].Rows[0][15].ToString();
            //txtnominee.Text = ds.Tables["employeepersonal"].Rows[0][16].ToString();
            txtrelation.Text = ds.Tables["employeepersonal"].Rows[0][17].ToString();
        }
    }

    public void fillTquali()
    {
        da = new SqlDataAdapter("select id,Technical_Skills,Experience from Employee_TQualifications where ED_TQId='" + Session["id"].ToString() + "' ", con);
        //da = new SqlDataAdapter("select Technical_Skills,Experience from Employee_TQualifications where Employee_id='" + Session["Employee_id"].ToString() + "' ", con);
        da.Fill(ds, "employeeTqualifications");
        //da.Fill(ds, "employeeTqualifications1");
        if (ds.Tables["employeeTqualifications"].Rows.Count == 0)
        {
            //trtqualiheader.Visible = false;
            grdTqualifications.DataSource = ReturnEmptyDataTableTQualifi();
            //Session["fillTquali"] = ReturnEmptyDataTableTQualifi();
            grdTqualifications.DataBind();

        }
        else
        {
            Session["fillTquali"] = ds.Tables["employeeTqualifications"];
            grdTqualifications.DataSource = ds.Tables["employeeTqualifications"];
            grdTqualifications.DataBind();
        }
    }
    public void fillquali()
    {
        //DataSet ds1 = new DataSet();
        da = new SqlDataAdapter(" select id,Class,University_name,[From],[To],Percentage from Employee_Qualifications where ED_QId='" + Session["id"].ToString() + "' ", con);
        //SqlDataAdapter da1 = new SqlDataAdapter(" select Class,University_name,[From],[To],Percentage from Employee_Qualifications where Employee_id='" + Session["Employee_id"].ToString() + "' ", con);

        da.Fill(ds, "employeequalification");
        //da1.Fill(ds1, "employeequalification1");
        if (ds.Tables["employeequalification"].Rows.Count == 0)
        {

            //trqualiheader.Visible = false;
            grdqualification.DataSource = ReturnEmptyDataTableQualifi();
            //Session["fillquali"] = ReturnEmptyDataTableQualifi();
            grdqualification.DataBind();
        }
        else
        {
            Session["fillquali"] = ds.Tables["employeequalification"];
            grdqualification.DataSource = ds.Tables["employeequalification"];
            grdqualification.DataBind();
        }
    }
    public void fillFamilydetails()
    {
        //DataSet ds1 = new DataSet();
        da = new SqlDataAdapter(" select id,Name,DOB,Age,Gender,Relation,Workno,Mobileno from Employee_FamilyInfo where ED_FId='" + Session["id"].ToString() + "' ", con);
        //SqlDataAdapter da1 = new SqlDataAdapter(" select Class,University_name,[From],[To],Percentage from Employee_Qualifications where Employee_id='" + Session["Employee_id"].ToString() + "' ", con);

        da.Fill(ds, "employeefamilyinfo");
        //da1.Fill(ds1, "employeequalification1");
        if (ds.Tables["employeefamilyinfo"].Rows.Count == 0)
        {
            //trqualiheader.Visible = false;
            grdfamilydetails.DataSource = ReturnEmptyDataTablefamily();
            //Session["fillquali"] = ReturnEmptyDataTableQualifi();
            grdfamilydetails.DataBind();
        }
        else
        {
            Session["fillfamilydetails"] = ds.Tables["employeefamilyinfo"];
            grdfamilydetails.DataSource = ds.Tables["employeefamilyinfo"];
            grdfamilydetails.DataBind();
        }
    }
    public void filhistroy()
    {
        da = new SqlDataAdapter("select id,Organization_name,[From],[To],Roledesignation from Employee_History where ED_HId='" + Session["id"].ToString() + "' ", con);
        //da = new SqlDataAdapter("select Organization_name,[From],[To],Roledesignation from Employee_History where Employee_id='" + Session["Employee_id"].ToString() + "' ", con);
        da.Fill(ds, "employeehistory");
        //da.Fill(ds, "employeehistory1");
        if (ds.Tables["employeehistory"].Rows.Count == 0)
        {
            trRadioButtonList1.Visible = true;
            RadioButtonList1.SelectedIndex = 1;
            grdhistory.DataSource = ReturnEmptyDataTableemp();
            //Session["fillhistory"] = ReturnEmptyDataTableemp();
            grdhistory.DataBind();
            grdhistory.Style.Add("display", "none");
        }
        else
        {
            trRadioButtonList1.Visible = true;
            RadioButtonList1.SelectedIndex = 0;
            Session["fillhistory"] = ds.Tables["employeehistory"];
            grdhistory.DataSource = ds.Tables["employeehistory"];
            grdhistory.DataBind();
        }
    }

    public void filchildren()
    {
        da = new SqlDataAdapter("select id,Children_name,Children_dob,Age,Gender,Martial_status from Employees_childrendetails where ED_CId='" + Session["id"].ToString() + "' ", con);
        //da = new SqlDataAdapter("select Children_name,Children_dob,Age,Gender,Martial_status from Employees_childrendetails where Employee_id='" + Session["Employee_id"].ToString() + "' ", con);
        da.Fill(ds, "childrendetails");
        //da.Fill(ds, "childrendetails1");
        if (ds.Tables["childrendetails"].Rows.Count == 0)
        {
            grdchildren.DataSource = ReturnEmptyDataTable();
            //Session["fillchildern"] = ReturnEmptyDataTable();
            grdchildren.DataBind();
        }
        else
        {
            Session["fillchildern"] = ds.Tables["childrendetails"];
            grdchildren.DataSource = ds.Tables["childrendetails"];
            grdchildren.DataBind();
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Type"] == "3" || Request.QueryString["Type"] == "4")
        {
           Response.Redirect("EmployeeRegister.aspx");
        }
        else
        {
            Response.Redirect("ViewEmployeeDetails.aspx");
        }
    }
    protected void grdsalarybreakup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public object id1 { get; set; }
    protected void ddlmartialst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmartialst.SelectedItem.Text != "Single")
        {
            TrDom.Visible = true;
        }
        else
        {
            TrDom.Visible = false;
        }
    }
    public string CreatePassword()
    {
        int length = 8;
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        System.Text.StringBuilder res = new System.Text.StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
       
        return res.ToString();
    }
    public void DirectSendmail()
    {
        try
        {
            string smtpaddress = ConfigurationManager.AppSettings["smtpaddress"].ToString();
            string id = ConfigurationManager.AppSettings["mailid"].ToString();
            string psw = ConfigurationManager.AppSettings["mailpassword"].ToString();
            string f = ConfigurationManager.AppSettings["mailfrom"].ToString();

            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(f);
            Msg.To.Add(txtdemail.Text);
            Msg.Subject = "Role Id";
            Msg.Body = "Hi,";
            Msg.Body += "<div>";
            Msg.Body += "<table cellpadding='4' cellspacing='0'>";
           
                Msg.Body += "<tr><td colspan='2' style='align:center'><div>The Role Id is: " + txtdroleid.Text + "</div></td></tr>";

            
            Msg.Body += "</table>";
            Msg.Body += "</div>";
            Msg.IsBodyHtml = true;
            Msg.DeliveryNotificationOptions.ToString();
            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtpaddress;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(id.ToString(), psw.ToString());
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            Msg = null;

        }
        catch (Exception ex)
        {
            Response.Write("error is" + ex);
        }
    }
    public void Sendmail()
    {
        try
        {
            string smtpaddress = ConfigurationManager.AppSettings["smtpaddress"].ToString();
            string id = ConfigurationManager.AppSettings["mailid"].ToString();
            string psw = ConfigurationManager.AppSettings["mailpassword"].ToString();
            string f = ConfigurationManager.AppSettings["mailfrom"].ToString();

            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(f);
            Msg.To.Add(txtemail.Text);
            Msg.Subject = "Role Id";
            Msg.Body = "Hi,";
            Msg.Body += "<div>";
            Msg.Body += "<table cellpadding='4' cellspacing='0'>";
           
                Msg.Body += "<tr><td colspan='2' style='align:center'><div>The Role Id is: " + txtroleid.Text + "</div></td></tr>";
            
            Msg.Body += "</table>";
            Msg.Body += "</div>";
            Msg.IsBodyHtml = true;
            Msg.DeliveryNotificationOptions.ToString();
            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtpaddress;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(id.ToString(), psw.ToString());
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            Msg = null;

        }
        catch (Exception ex)
        {
            Response.Write("error is" + ex);
        }
    }
    protected void hlnk_Click(object sender, EventArgs e)
    {
        popindents.Show();
        ViewState["Flag"] = "2";
        UpdatePanel3.Update();
        UpdatePanel2.Update();
        UpdatePanel1.Update();
        txtdepartment.Text = "";
    }
    protected void hlnkdbtn_Click(object sender, EventArgs e)
    {
        popext1.Show();
        ViewState["Flag"] = "2";
        UpdatePanel4.Update();
       
        txtdepartment.Text = "";
    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        popindents.Show();
        ViewState["Flag"] = "1";
        UpdatePanel3.Update();
        UpdatePanel2.Update();
        UpdatePanel1.Update();
        txtdepartment.Text = "";
    }
    protected void btndsave_Click(object sender, EventArgs e)
    {
        UpdatePanel4.Update();
        if (ViewState["Flag"].ToString() == "2")
        {
            ddlddepartment.Items.Clear();
            ddlddepartment.Items.Add(txtddepartment.Text);
            DataTable dt = Session["deprtments"] as DataTable;
            DataRow datatRow = dt.NewRow();
            datatRow["Department_Name"] = txtddepartment.Text;
            dt.Rows.Add(datatRow);
            ddlddepartment.DataValueField = "Department_Name";
            ddlddepartment.DataTextField = "Department_Name";
            ddlddepartment.DataSource = dt;
            ddlddepartment.DataBind();
            ddlddepartment.Items.Insert(0, new ListItem("Select", "0"));
            Session["deprtments"] = dt;
        }
        popext1.Hide();

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        UpdatePanel3.Update();
        UpdatePanel2.Update();
        UpdatePanel1.Update();
        updTqualification.Update();
        if (ViewState["Flag"].ToString() == "1")
        {
            var ddl3 = (DropDownList)grdTqualifications.FooterRow.FindControl("ddlfskills");
            ddl3.Items.Add(txtdepartment.Text);
        }
        else if (ViewState["Flag"].ToString() == "2")
        {
            ddldepartment.Items.Clear();
            ddldepartment.Items.Add(txtdepartment.Text);
            DataTable dt = Session["deprtments"] as DataTable;
            DataRow datatRow = dt.NewRow();
            datatRow["Department_Name"] = txtdepartment.Text;
            dt.Rows.Add(datatRow);
            ddldepartment.DataValueField = "Department_Name";
            ddldepartment.DataTextField = "Department_Name";
            ddldepartment.DataSource =dt;
            ddldepartment.DataBind();
            ddldepartment.Items.Insert(0, new ListItem("Select", "0"));
            Session["deprtments"] = dt;
        }
        popindents.Hide();
      
        
    }

    public void FillDepartment()
    {
        try
        {
            da = new SqlDataAdapter("Select Department_Name from Departments order by Depart_Id asc", con);
            da.Fill(ds, "deprtments");
            Session["deprtments"] = ds.Tables["deprtments"];
            if (ds.Tables["deprtments"].Rows.Count > 0)
            {

                if (ddlappointmenttype.SelectedItem.Text == "Direct")
                {
                    ddlddepartment.DataValueField = "Department_Name";
                    ddlddepartment.DataTextField = "Department_Name";
                    ddlddepartment.DataSource = ds.Tables["deprtments"];
                    ddlddepartment.DataBind();
                    ddlddepartment.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddldepartment.DataValueField = "Department_Name";
                    ddldepartment.DataTextField = "Department_Name";
                    ddldepartment.DataSource = ds.Tables["deprtments"];
                    ddldepartment.DataBind();
                    ddldepartment.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                if (ddlappointmenttype.SelectedItem.Text == "Normal")
                {
                    ddldepartment.Items.Insert(0, new ListItem("Select", "0"));
                }
                else if (ddlappointmenttype.SelectedItem.Text == "Direct")
                {
                    ddlddepartment.Items.Insert(0, new ListItem("Select", "0"));

                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddldgender_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldgender.SelectedItem.Text == "Male" && ddldsex.SelectedItem.Text == "Male" && ddldmartialst.SelectedItem.Text == "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Father");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");
            }
            else if (ddldgender.SelectedItem.Text == "Male" && ddldsex.SelectedItem.Text == "Male" && ddldmartialst.SelectedItem.Text != "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Father");
                ddldRelation.Items.Add("Son");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");
            }
            else if (ddldgender.SelectedItem.Text == "Female" && ddldsex.SelectedItem.Text == "Male" && ddldmartialst.SelectedItem.Text == "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Mother");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");

            }
            else if (ddldgender.SelectedItem.Text == "Female" && ddldsex.SelectedItem.Text == "Male" && ddldmartialst.SelectedItem.Text == "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Mother");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");
            }
            else if (ddldgender.SelectedItem.Text == "Female" && ddldsex.SelectedItem.Text == "Male" && ddldmartialst.SelectedItem.Text != "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Wife");
                ddldRelation.Items.Add("Mother");
                ddldRelation.Items.Add("Daughter");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");
            }
            else if (ddldgender.SelectedItem.Text == "Male" && ddldsex.SelectedItem.Text == "Female" && ddldmartialst.SelectedItem.Text == "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Father");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");

            }
            else if (ddldgender.SelectedItem.Text == "Male" && ddldsex.SelectedItem.Text == "Female" && ddldmartialst.SelectedItem.Text != "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Husband");
                ddldRelation.Items.Add("Father");
                ddldRelation.Items.Add("Son");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");
            }
            else if (ddldgender.SelectedItem.Text == "Female" && ddldsex.SelectedItem.Text == "Female" && ddldmartialst.SelectedItem.Text == "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Mother");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");
            }
            else if (ddldgender.SelectedItem.Text == "Female" && ddldsex.SelectedItem.Text == "Female" && ddldmartialst.SelectedItem.Text != "Single")
            {
                ddldRelation.Items.Add("Select");
                ddldRelation.Items.Add("Mother");
                ddldRelation.Items.Add("Wife");
                ddldRelation.Items.Add("Brother");
                ddldRelation.Items.Add("Sister");
                ddldRelation.Items.Add("Guardian");
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btndsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            loginbll objbl = new loginbll();

            da = new SqlDataAdapter("DirectEmployeeRegistration_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@firstname", SqlDbType.VarChar).Value = txtdfname.Text;
            da.SelectCommand.Parameters.AddWithValue("@lastname", SqlDbType.VarChar).Value = txtdlname.Text;
            da.SelectCommand.Parameters.AddWithValue("@middlename", SqlDbType.VarChar).Value = txtdmname.Text;
            da.SelectCommand.Parameters.AddWithValue("@empdob", SqlDbType.VarChar).Value = txtdndob.Text;
            da.SelectCommand.Parameters.AddWithValue("@empage", SqlDbType.VarChar).Value = txtdnage.Text;
            da.SelectCommand.Parameters.AddWithValue("@martialstatus", SqlDbType.VarChar).Value = ddldmartialst.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@sex", SqlDbType.VarChar).Value = ddldsex.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@permanentaddress", SqlDbType.VarChar).Value = txtdpaddress.Text;
            da.SelectCommand.Parameters.AddWithValue("@mobile", SqlDbType.VarChar).Value = txtdmobileno.Text;
            da.SelectCommand.Parameters.AddWithValue("@mailid", SqlDbType.VarChar).Value = txtdemail.Text;
            da.SelectCommand.Parameters.AddWithValue("@category", SqlDbType.VarChar).Value = ddlcategory.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@appointordesc", SqlDbType.VarChar).Value = ddldrole.SelectedItem.Text;
            da.SelectCommand.Parameters.AddWithValue("@roleid", SqlDbType.VarChar).Value = txtdroleid.Text;
            da.SelectCommand.Parameters.AddWithValue("@pwd", SqlDbType.VarChar).Value = objbl.encryptPass(lblpwd.Text);
            da.SelectCommand.Parameters.AddWithValue("@department", SqlDbType.VarChar).Value = ddlddepartment.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@jobtype", SqlDbType.VarChar).Value = ddldjobpos.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@nominee", SqlDbType.VarChar).Value = txtdnominee.Text;
            da.SelectCommand.Parameters.AddWithValue("@Relation", SqlDbType.VarChar).Value = ddldRelation.SelectedItem.Text;
            if (ddldrole.SelectedItem.Text == "Project Manager")
            {
                da.SelectCommand.Parameters.AddWithValue("@CostCenter", SqlDbType.VarChar).Value = ddlcc.SelectedValue;

            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@CostCenter", SqlDbType.VarChar).Value = "CC-12";

            }
            if (ddldmartialst.SelectedItem.Text == "Married")
            {
                da.SelectCommand.Parameters.AddWithValue("@Spoucename", SqlDbType.VarChar).Value = txtspousename.Text;
            }
            da.SelectCommand.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
            da.SelectCommand.Parameters.AddWithValue("@Type", "3");
            da.SelectCommand.Parameters.AddWithValue("@CUser", Session["user"].ToString());
            da.SelectCommand.Parameters.AddWithValue("@CDate", SqlDbType.DateTime).Value = DateTime.Now.ToString();
            da.Fill(ds, "empdata");
            DirectSendmail();
            JavaScript.UPAlertRedirect(Page, "Inserted Sucessfully...", "ViewEmployeeDetails.aspx");
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddldmartialst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldmartialst.SelectedItem.Text != "Married")
        {
            spouce.Visible = false;
        }
        else
        {
            spouce.Visible = true;
        }
    }
}
