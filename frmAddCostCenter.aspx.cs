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
using System.Web.Services;
using System.Data.SqlClient;



public partial class Admin_frmAddCostCenter : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommandBuilder cmb = new SqlCommandBuilder();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlDataReader dr;

    string cctype, ccCode, epplfinalofferno, finalofferdate,refno,refdate, ccName, ccinchargename, ccinchargepno, addr, ccphone, voucher, day;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["key"] == "yes")
        {
            MasterPageFile = "~/MasterPage.master";
        }
        else
        {
            MasterPageFile = "~/Essel.master";
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["key"] == "yes")
        {
            ww.Visible = false;
        }
        else
        {
            ww.Visible = true;
        }
        //Essel childmaster = (Essel)this.Master;
        //HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        //lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        //esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 21);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            Fillstates();
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                txtccCode.Enabled = true;
                btnAddCC.Attributes.Add("onclick", "return validate()");
                btnAddCC.Style.Add("visibility", "visible");
                btnCancel.Style.Add("visibility", "visible");
                ddlcctype.Style.Add("visibility", "visible");
                tbl.Style.Add("visibility", "visible");
                btnAddCC.Text = "Add CC";
                
                //trtype.Style.Add("visibility","hidden");

            }
            else if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin")
            {


                btnAddCC.Attributes.Add("onclick", "return check()");
                txtccCode.Enabled = false;
                ddlcctype.Style.Add("visibility", "hidden");
                tbl.Style.Add("visibility", "hidden");
                tdservicetype.Style.Add("display", "none");
                tdddlservicetype.Style.Add("display", "none");
                btnAddCC.Text = "Verify CC";
                fill();
            }


        }
        

    }
    private void fill()
    {
        if (Session["roles"].ToString() == "HoAdmin")
            da = new SqlDataAdapter("select cc_code,cc_name,cc_inchargename,address from Cost_Center where status='1'", con);
        else if (Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("select cc_code,cc_name,cc_inchargename,address from Cost_Center where status='2'", con);

        cmb = new SqlCommandBuilder(da);
        da.Fill(ds, "Cost_Center");
        GridView1.DataSource = ds.Tables["Cost_Center"];
        GridView1.DataBind();
    }
 
    [WebMethod]
    public static string IsCCAvailable(string cc_code)
    {
        string cc = "CC-" + cc_code;
        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter("select cc_code,cc_name from Cost_Center where cc_code='" + cc + "'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "checkcc");
        conWB.Open();
        if (dsWB.Tables["checkcc"].Rows.Count > 0)
        {
            string Ncc_code = dsWB.Tables[0].Rows[0].ItemArray[0].ToString();
            string Ncc_name = dsWB.Tables[0].Rows[0].ItemArray[1].ToString();
            string Tcc_code = "CC Code is already exists";
            return Tcc_code;// +Ncc_name;
        }

        else
        {
            string NewCC = cc + " is Available";

            return NewCC;

        }

    }


    protected void btnAddCC_Click(object sender, EventArgs e)
    {
        //update.Visible = false;
        ccCode = "CC-" + txtccCode.Text;
        ccName = txtccName.Text;
        finalofferdate = Txtfinalofrdate.Text;
        //DateTime obj = new DateTime();
        //obj = DateTime.Today;
        //date = obj.ToString();
        ccinchargename = txtinname.Text;
        ccinchargepno = incphone.Text;
        addr = address.Text;
        ccphone = phoneno.Text;
        voucher = txtvoucher.Text;
        day = txtday.Text;
        // OfferNo=Txtfinalofrno.Text;
        refno = txtrefno.Text;
        refdate = txtrefdate.Text;
        try
        {
            if (Session["roles"].ToString() == "Sr.Accountant")
            {
                cmd.Connection = con;
                if (ddlcctype.SelectedItem.Text == "Performing")
                {
                    if (ddltype.SelectedItem.Text == "Service" || ddltype.SelectedItem.Text == "Trading")
                    {
                        cmd.CommandText = "insert into Cost_Center(cc_code,cc_name,final_offerdate,cc_inchargename,cc_incharge_phno,address,cc_phoneno,voucher_limit,day_limit,cc_type,status,final_offerno,ref_no,ref_date,cc_subtype,CCService_Type,state_id) values(@costcentercode,@costcentername,@finalofferdate,@costinchargename,@costinchargephoneno,@costaddress,@costphoneno,@voucher,@day,@ccType,@status,@epplfinalofferno,@referenceno,@referencedate,@servicetype,@CCServiceType,@State)";
                    }
                    else if (ddltype.SelectedItem.Text == "Manufacturing")
                    {
                        cmd.CommandText = "insert into Cost_Center(cc_code,cc_name,final_offerdate,cc_inchargename,cc_incharge_phno,address,cc_phoneno,voucher_limit,day_limit,cc_type,status,final_offerno,ref_no,ref_date,cc_subtype,state_id) values(@costcentercode,@costcentername,@finalofferdate,@costinchargename,@costinchargephoneno,@costaddress,@costphoneno,@voucher,@day,@ccType,@status,@epplfinalofferno,@referenceno,@referencedate,@servicetype,@State)";
                    }
                }
                else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                {
                    cmd.CommandText = "insert into Cost_Center(cc_code,cc_name,final_offerdate,cc_inchargename,cc_incharge_phno,address,cc_phoneno,voucher_limit,day_limit,cc_type,status,final_offerno,ref_no,ref_date,cc_subtype,state_id) values(@costcentercode,@costcentername,@finalofferdate,@costinchargename,@costinchargephoneno,@costaddress,@costphoneno,@voucher,@day,@ccType,@status,@epplfinalofferno,@referenceno,@referencedate,@servicetype,@State)";
                }
                cmd.Parameters.Add("@costcentercode", SqlDbType.VarChar).Value = ccCode;
                cmd.Parameters.Add("@costcentername", SqlDbType.VarChar).Value = ccName;
                cmd.Parameters.Add("@finalofferdate", SqlDbType.VarChar).Value = finalofferdate;
                cmd.Parameters.Add("@costinchargename", SqlDbType.VarChar).Value = ccinchargename;
                cmd.Parameters.Add("@costinchargephoneno", SqlDbType.VarChar).Value = ccinchargepno;
                cmd.Parameters.Add("@costaddress", SqlDbType.VarChar).Value = addr;
                cmd.Parameters.Add("@costphoneno", SqlDbType.VarChar).Value = ccphone;
                cmd.Parameters.Add("@voucher", SqlDbType.Money).Value = voucher;
                cmd.Parameters.Add("@day", SqlDbType.Money).Value = day;
                cmd.Parameters.Add("@ccType", SqlDbType.VarChar).Value = ddlcctype.SelectedItem.Text;
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "1";
                cmd.Parameters.Add("@epplfinalofferno", SqlDbType.VarChar).Value = Txtfinalofrno.Text;
                cmd.Parameters.Add("@referenceno", SqlDbType.VarChar).Value = txtrefno.Text;
                cmd.Parameters.Add("@referencedate", SqlDbType.VarChar).Value = txtrefdate.Text;
                cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = ddlstate.SelectedValue;

                if (ddlcctype.SelectedItem.Text == "Performing")
                {
                    cmd.Parameters.Add("@servicetype", SqlDbType.VarChar).Value = ddltype.SelectedItem.Text;
                    if (ddltype.SelectedItem.Text == "Service")
                    {
                        string ServiceType = string.Empty;
                        for (int i = 0; i < ddlservicetype.Items.Count; i++)
                        {
                            if (ddlservicetype.Items[i].Selected)
                            {
                                ServiceType += ddlservicetype.Items[i].Value + ",";
                            }                            
                        }
                        cmd.Parameters.Add("@CCServiceType", SqlDbType.VarChar).Value = ServiceType;
                    }
                }
                else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                {
                    cmd.Parameters.Add("@servicetype", SqlDbType.VarChar).Value = ddlcctype.SelectedItem.Text;
                }


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
                //bool j = true;
                if (j == true)
                {

                    JavaScript.UPAlertRedirect(Page,"Cost Center Successfully Added", "frmAddCostCenter.aspx");
                    
                }
                else
                {
                    showalert("Assign New Cost Center Failed");
                }
                con.Close();
            }
            else if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin")
            {
                cmd.Connection = con;
                string ServiceType = string.Empty;
                for (int i = 0; i < ddlservicetype.Items.Count; i++)
                {
                    if (ddlservicetype.Items[i].Selected)
                    {
                        ServiceType += ddlservicetype.Items[i].Value + ",";
                    }                    
                }
                if (Session["roles"].ToString() == "HoAdmin")
                {
                    if (Label13.Text == "Performing")
                    {
                        if (ddltype.SelectedItem.Text == "Service" || ddltype.SelectedItem.Text == "Trading")
                        {
                            cmd.CommandText = "update Cost_Center set cc_name='" + txtccName.Text + "',final_offerdate='" + Txtfinalofrdate.Text + "',cc_inchargename='" + txtinname.Text + "',cc_incharge_phno='" + incphone.Text + "',address='" + address.Text + "',cc_phoneno='" + phoneno.Text + "',voucher_limit='" + txtvoucher.Text + "',day_limit='" + txtday.Text + "',final_offerno='" + Txtfinalofrno.Text + "',ref_no='" + txtrefno.Text + "',ref_date='" + txtrefdate.Text + "',cc_subtype='" + ddltype.SelectedItem.Text + "',ccservice_type='" + ServiceType + "',status='2',state_id='" + ddlstate.SelectedValue + "' where cc_code='" + txtccCode.Text + "'";
                        }
                        else if (ddltype.SelectedItem.Text == "Manufacturing")
                        {
                            cmd.CommandText = "update Cost_Center set cc_name='" + txtccName.Text + "',final_offerdate='" + Txtfinalofrdate.Text + "',cc_inchargename='" + txtinname.Text + "',cc_incharge_phno='" + incphone.Text + "',address='" + address.Text + "',cc_phoneno='" + phoneno.Text + "',voucher_limit='" + txtvoucher.Text + "',day_limit='" + txtday.Text + "',final_offerno='" + Txtfinalofrno.Text + "',ref_no='" + txtrefno.Text + "',ref_date='" + txtrefdate.Text + "',cc_subtype='" + ddltype.SelectedItem.Text + "',status='2',state_id='" + ddlstate.SelectedValue + "' where cc_code='" + txtccCode.Text + "'";
                        }
                    }
                    else
                    {
                        cmd.CommandText = "update Cost_Center set cc_name='" + txtccName.Text + "',final_offerdate='" + Txtfinalofrdate.Text + "',cc_inchargename='" + txtinname.Text + "',cc_incharge_phno='" + incphone.Text + "',address='" + address.Text + "',cc_phoneno='" + phoneno.Text + "',voucher_limit='" + txtvoucher.Text + "',day_limit='" + txtday.Text + "',final_offerno='" + Txtfinalofrno.Text + "',ref_no='" + txtrefno.Text + "',ref_date='" + txtrefdate.Text + "',cc_subtype='" + Label13.Text + "',status='2',state_id='" + ddlstate.SelectedValue + "' where cc_code='" + txtccCode.Text + "'";
                    }
                }
                else if (Session["roles"].ToString() == "SuperAdmin")
                {
                    if (Label13.Text == "Performing")
                    {
                        if (ddltype.SelectedItem.Text == "Service" || ddltype.SelectedItem.Text == "Trading")
                        {
                            cmd.CommandText = "update Cost_Center set cc_name='" + txtccName.Text + "',final_offerdate='" + Txtfinalofrdate.Text + "',cc_inchargename='" + txtinname.Text + "',cc_incharge_phno='" + incphone.Text + "',address='" + address.Text + "',cc_phoneno='" + phoneno.Text + "',voucher_limit='" + txtvoucher.Text + "',day_limit='" + txtday.Text + "',final_offerno='" + Txtfinalofrno.Text + "',ref_no='" + txtrefno.Text + "',ref_date='" + txtrefdate.Text + "',cc_subtype='" + ddltype.SelectedItem.Text + "',ccservice_type='" + ServiceType + "',status='New',state_id='" + ddlstate.SelectedValue + "' where cc_code='" + txtccCode.Text + "'";
                        }
                        else if (ddltype.SelectedItem.Text == "Manufacturing")
                        {
                            cmd.CommandText = "update Cost_Center set cc_name='" + txtccName.Text + "',final_offerdate='" + Txtfinalofrdate.Text + "',cc_inchargename='" + txtinname.Text + "',cc_incharge_phno='" + incphone.Text + "',address='" + address.Text + "',cc_phoneno='" + phoneno.Text + "',voucher_limit='" + txtvoucher.Text + "',day_limit='" + txtday.Text + "',final_offerno='" + Txtfinalofrno.Text + "',ref_no='" + txtrefno.Text + "',ref_date='" + txtrefdate.Text + "',cc_subtype='" + ddltype.SelectedItem.Text + "',status='New',state_id='" + ddlstate.SelectedValue + "' where cc_code='" + txtccCode.Text + "'";
                        }
                    }
                    else
                    {
                        cmd.CommandText = "update Cost_Center set cc_name='" + txtccName.Text + "',final_offerdate='" + Txtfinalofrdate.Text + "',cc_inchargename='" + txtinname.Text + "',cc_incharge_phno='" + incphone.Text + "',address='" + address.Text + "',cc_phoneno='" + phoneno.Text + "',voucher_limit='" + txtvoucher.Text + "',day_limit='" + txtday.Text + "',final_offerno='" + Txtfinalofrno.Text + "',ref_no='" + txtrefno.Text + "',ref_date='" + txtrefdate.Text + "',cc_subtype='" + Label13.Text + "',status='New',state_id='" + ddlstate.SelectedValue + "' where cc_code='" + txtccCode.Text + "'";

                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
               // bool j = false;
                if (j == true)
                {

                    showalert("Cost Center Successfully updated");
                    tbl.Style.Add("visibility", "hidden");
                }
                else
                {
                    showalert("New Cost Center updation Failed");
                }
                con.Close();
                fill();
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


    public void showalert(string message)
    {       
        string script = "alert(\"" + message + "\");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnAddCC.Style.Add("visibility", "visible");
        btnCancel.Style.Add("visibility", "visible");
        clear();

    }
    public void clear()
    {
        //CascadingDropDown1.SelectedValue = "";
        txtccCode.Text = "";
        txtccName.Text = "";
        txtinname.Text = "";
        incphone.Text = "";
        //lblAlert.Text = "";
        address.Text = "";
        phoneno.Text = "";
        txtvoucher.Text = "";
        txtday.Text = "";
        Txtfinalofrno.Text = "";
        txtrefno.Text = "";
        txtrefdate.Text = "";
        ddltype.SelectedItem.Text = "Select";
        ddlcctype.SelectedItem.Text = "Select";


    }
    //      protected void update_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //update.Visible = true;
    //        //panel2.Visible = true;
    //        string name = txtccName.Text;
    //        string incharge = txtinname.Text;
    //        string inchphone = incphone.Text;
    //        string add = address.Text;
    //        string phone = phoneno.Text;
    //        string voucher = txtvoucher.Text;
    //        string day = txtday.Text;
    //        cmd = new SqlCommand("update cost_center set cc_name ='" + name + "',cc_inchargename = '" + incharge + "',cc_incharge_phno='" + inchphone + "',address = '" + add + "',cc_phoneno='" + phone + "',voucher_limit='" + txtvoucher.Text + "',day_limit='" + txtday.Text + "',cc_type='"+ddlcctype.SelectedItem.Text+"' where cc_code ='" + ddlca.SelectedValue + "'", con);
    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        }
    //        con.Open();

    //        int k = cmd.ExecuteNonQuery();
    //        if (k == 1)
    //        {
    //            showalert("Cost Center Updated Sucessfully");
    //        }
    //        else
    //        {
    //            showalert("Cost Center Updating Failed");
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        Utilities.CatchException(ex);
    //        JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}


    //protected void ddlca_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlca.SelectedItem.Text != "Select CC")
    //        {
    //            da = new SqlDataAdapter("select cc_code,cc_name,date,cc_inchargename,cc_incharge_phno,address,cc_phoneno,voucher_limit,day_limit,cc_type from cost_center where cc_code = '" + ddlca.SelectedValue + "'", con);
    //            da.Fill(ds, "ddlca");
    //            txtccName.Text = ds.Tables["ddlca"].Rows[0].ItemArray[1].ToString();
    //            txtinname.Text = ds.Tables["ddlca"].Rows[0].ItemArray[3].ToString();
    //            incphone.Text = ds.Tables["ddlca"].Rows[0].ItemArray[4].ToString();
    //            address.Text = ds.Tables["ddlca"].Rows[0].ItemArray[5].ToString();
    //            phoneno.Text = ds.Tables["ddlca"].Rows[0].ItemArray[6].ToString();
    //            txtvoucher.Text = ds.Tables["ddlca"].Rows[0].ItemArray[7].ToString();
    //            txtday.Text = ds.Tables["ddlca"].Rows[0].ItemArray[8].ToString();
    //            ddlcctype.SelectedItem.Text = ds.Tables["ddlca"].Rows[0].ItemArray[9].ToString();

    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        Utilities.CatchException(ex);
    //        JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tbl.Style.Add("visibility", "visible");
            con.Open();
            cmd = new SqlCommand("select cc_type,final_offerno,final_offerdate,ref_no,ref_date,cc_code,cc_name,cc_inchargename,cc_incharge_phno,address,cc_phoneno,voucher_limit,day_limit,cc_subtype,CCservice_Type,cc.state_id from Cost_Center cc join states st on cc.State_Id=st.state_id where cc_code='" + GridView1.SelectedValue.ToString() + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows[0][0].ToString() == "Non-Performing" || ds.Tables[0].Rows[0][0].ToString() == "Capital")
            {
                trtype.Visible = false;
                trcc.Visible = false;
                trrefno.Visible = false;
            }
            else
            {
                trtype.Visible = true;
                trcc.Visible = true;
                trrefno.Visible = true;
                if (ds.Tables[0].Rows[0][13].ToString() == "Service")
                {
                    tdservicetype.Style.Add("display", "block");
                    tdddlservicetype.Style.Add("display", "block");
                    string[] ServiceType = ds.Tables[0].Rows[0][14].ToString().Split(new[] { "," }, StringSplitOptions.None);
                   
                    for (int i = 0; i < ddlservicetype.Items.Count; i++)
                    {
                        for (int k = 0; k < ServiceType.Length; k++)
                        {
                            if (ddlservicetype.Items[i].Text == ServiceType[k].ToString())
                            {
                                ddlservicetype.Items[i].Selected = true;
                            }
                        }
                    }
                  //  ddlservicetype.SelectedItem.Text = ds.Tables[0].Rows[0][14].ToString();
                    ddlservicetype.Enabled = false;
                }
            }
            Label13.Text = ds.Tables[0].Rows[0][0].ToString();
            Txtfinalofrno.Text = ds.Tables[0].Rows[0][1].ToString();
            Txtfinalofrdate.Text = ds.Tables[0].Rows[0][2].ToString();
            txtrefno.Text = ds.Tables[0].Rows[0][3].ToString();
            txtrefdate.Text = ds.Tables[0].Rows[0][4].ToString();
            txtccCode.Text = ds.Tables[0].Rows[0][5].ToString();
            txtccName.Text = ds.Tables[0].Rows[0][6].ToString();
            txtinname.Text = ds.Tables[0].Rows[0][7].ToString();
            incphone.Text = ds.Tables[0].Rows[0][8].ToString();
            address.Text = ds.Tables[0].Rows[0][9].ToString();
            phoneno.Text = ds.Tables[0].Rows[0][10].ToString();
            txtvoucher.Text = ds.Tables[0].Rows[0][11].ToString().Replace(".0000",".00");
            txtday.Text = ds.Tables[0].Rows[0][12].ToString().Replace(".0000", ".00");
            if (Label13.Text == "Performing")
            ddltype.SelectedValue = ds.Tables[0].Rows[0][13].ToString();
            ddlstate.SelectedValue = ds.Tables[0].Rows[0][15].ToString();
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


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "delete from Cost_Center where cc_code='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            cmd.Connection = con;
            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                JavaScript.UPAlert(Page,"Deleted Successfully");
                
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
        fill();
    }
    public void Fillstates()
    {
        try
        {

            da = new SqlDataAdapter("select state_id,state from [states]", con);
            da.Fill(ds, "GSTStates");
            if (ds.Tables["GSTStates"].Rows.Count > 0)
            {
                ddlstate.DataValueField = "state_id";
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
    
}




