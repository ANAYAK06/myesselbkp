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
using System.Web.Services;

public partial class VerifyclientPO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();  
    protected void Page_Load(object sender, EventArgs e)
    {      
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "HoAdmin")
                btnReject.Visible = true;
            else
                btnReject.Visible = false;

            fillAmendclientpo();
            fillnewclientpo();
        }
    }
    public void fillnewclientpo()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")            
                da = new SqlDataAdapter("select contract_id,po_no,CC_code,po_totalvalue,po_date from po where status='1' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            else if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select contract_id,po_no,CC_code,po_totalvalue,po_date from po where status='2'", con);
          
            da.Fill(ds, "Fillnewpo");
            gridnewpo.DataSource = ds.Tables["Fillnewpo"];            
            gridnewpo.DataBind();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillAmendclientpo()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select contract_id,po_amendedno,po_no,po_amendedtotalvalue,po_ammendedate from po_amended where status='1' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            else if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select contract_id,po_amendedno,po_no,po_amendedtotalvalue,po_ammendedate from po_amended where status='2'", con);
          
            da.Fill(ds, "Fillamendpo");
            gridamendpo.DataSource = ds.Tables["Fillamendpo"];
            gridamendpo.DataBind();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("Select client_name from client where client_id='" + ddlclientid.SelectedItem.Text + "' ", con);
        da.Fill(ds, "clientname");
        lblclientid.Text = ds.Tables["clientname"].Rows[0].ItemArray[0].ToString();
        lblsubclientid.Text = "";
        ddlsubclientid.Items.Clear();
        try
        {
            da = new SqlDataAdapter("select distinct subclient_id from subclient where  client_id='" + ddlclientid.SelectedItem.Text + "' ", con);

            da.Fill(ds, "subclient");
            if (ds.Tables["subclient"].Rows.Count > 0)
            {
                ddlsubclientid.DataTextField = "subclient_id";
                ddlsubclientid.DataValueField = "subclient_id";
                ddlsubclientid.DataSource = ds.Tables["subclient"];
                ddlsubclientid.DataBind();
                ddlsubclientid.Items.Insert(0, "select");
            }
            else
            {
                ddlsubclientid.Items.Insert(0, "select");
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("Select branch from subclient where subclient_id='" + ddlsubclientid.SelectedItem.Text + "' ", con);
        da.Fill(ds, "subclientname");
        if (ds.Tables["subclientname"].Rows.Count > 0)
            lblsubclientid.Text = ds.Tables["subclientname"].Rows[0].ItemArray[0].ToString();
    }
    protected void gridnewpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        popitems.Show();
        tblAmendpo.Visible = false;
        tblnewpo.Visible = true;
        ViewState["contract_id"] = gridnewpo.SelectedValue.ToString();


        da = new SqlDataAdapter("select contract_id,[po_date],[po_no],cast([po_basicvalue]as decimal(18,2))[po_basicvalue],cast([po_servicetaxvalue]as decimal(18,2))[po_servicetaxvalue],cast([po_totalvalue]as decimal(18,2))[po_totalvalue],client_id,subclient_id,POComdate,MobilisationAdv,RABill,PaydueofRABill,cast(PO_Gst as decimal(18,2))POgst,AdvSettment,p.CC_code,cc.CC_SubType from po p left outer join Cost_Center cc on p.CC_code=cc.cc_code where contract_id='" + gridnewpo.SelectedValue + "'", con);
        da.Fill(ds, "newpodetails");

        string clientid = ds.Tables["newpodetails"].Rows[0].ItemArray[6].ToString();

        da = new SqlDataAdapter("Select client_id from client order by client_id", con);
        da.Fill(ds, "client");
        ddlclientid.DataTextField = "client_id";
        ddlclientid.DataValueField = "client_id";
        ddlclientid.DataSource = ds.Tables["client"];
        ddlclientid.DataBind();
        ddlclientid.Items.Insert(0, "select");
        ddlclientid.SelectedItem.Text = clientid;

        da = new SqlDataAdapter("Select client_name from client where client_id='" + clientid + "' ", con);
        da.Fill(ds, "clientname");
        lblclientid.Text = ds.Tables["clientname"].Rows[0].ItemArray[0].ToString();

        da = new SqlDataAdapter("select distinct subclient_id from subclient where  client_id='" + clientid + "' ", con);

        da.Fill(ds, "subclient");

        ddlsubclientid.DataTextField = "subclient_id";
        ddlsubclientid.DataValueField = "subclient_id";
        ddlsubclientid.DataSource = ds.Tables["subclient"];
        ddlsubclientid.DataBind();
        ddlsubclientid.Items.Insert(0, "select");

        string subclientid = ds.Tables["newpodetails"].Rows[0].ItemArray[7].ToString();
        ddlsubclientid.SelectedItem.Text = subclientid;

        da = new SqlDataAdapter("Select branch from subclient where subclient_id='" + subclientid + "' ", con);
        da.Fill(ds, "subclientname");      
        lblsubclientid.Text = ds.Tables["subclientname"].Rows[0].ItemArray[0].ToString();

        txtStartdate.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[1].ToString();
        txtPO.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[2].ToString();
        txtBasic.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[3].ToString();
        txtTax.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[4].ToString();
        txtTotal.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[5].ToString();
        txtEnddate.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[8].ToString();
        ddlmob.SelectedValue= ds.Tables["newpodetails"].Rows[0].ItemArray[9].ToString();
        txtRabill.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[10].ToString();
        txtpaybills.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[11].ToString();
        txtgst.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[12].ToString();
        txtadvsett.Text = ds.Tables["newpodetails"].Rows[0].ItemArray[13].ToString();
        if (ds.Tables["newpodetails"].Rows[0].ItemArray[15].ToString() == "Service")  
             Lblsertax.Text = "Service Tax";
        else
            Lblsertax.Text = "Excise Duty";

        if(ds.Tables["newpodetails"].Rows[0].ItemArray[9].ToString()=="NO")
            txtadvsett.Enabled=false;

        if (Session["roles"].ToString() == "Project Manager")
        {
            DisableFormControls(Form.Controls);
        }
    }
    public static void DisableFormControls(ControlCollection ChildCtrls)
    {
        foreach (Control Ctrl in ChildCtrls)
        {
            if (Ctrl is TextBox)
                ((TextBox)Ctrl).Enabled = false;            
            else if (Ctrl is DropDownList)
                ((DropDownList)Ctrl).Enabled = false;
            else
                DisableFormControls(Ctrl.Controls);
        }
    }

    protected void gridamendpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblAmendpo.Visible = true;
        tblnewpo.Visible = false;
        ViewState["Amendcontract_id"] = gridamendpo.SelectedValue.ToString();

        popitems.Show();
        da = new SqlDataAdapter("select p.contract_id,p.po_no,po_amendedno,po_ammendedate,cast(po_amendedbasicvalue as decimal(18,2))po_amendedbasicvalue,cast(po_amendedservicetaxvalue as decimal(18,2))po_amendedservicetaxvalue,cast(po_amendedgstvalue as decimal(18,2))po_amendedgstvalue,cast(po_amendedtotalvalue as decimal(18,2))po_amendedtotalvalue,cast(Revisedpo_value as decimal(18,2))Revisedpo_value,p.CC_code,cc.CC_SubType,cast(a.po_totalvalue as decimal(18,2))po_totalvalue from po_amended p left outer join Cost_Center cc on p.CC_code=cc.cc_code left outer join po a on a.po_no=p.po_no where p.contract_id='" + gridamendpo.SelectedValue + "'", con);
        da.Fill(ds, "Amndpodetails");

        txtpono.Text = ds.Tables["Amndpodetails"].Rows[0].ItemArray[1].ToString();
        Txtamndpo.Text = ds.Tables["Amndpodetails"].Rows[0].ItemArray[2].ToString();
        txtAmdDate.Text = ds.Tables["Amndpodetails"].Rows[0].ItemArray[3].ToString();
        txtamPOvalue.Text = ds.Tables["Amndpodetails"].Rows[0].ItemArray[4].ToString();
        txtAmdserv.Text = ds.Tables["Amndpodetails"].Rows[0].ItemArray[5].ToString();
        txtAmgst.Text = ds.Tables["Amndpodetails"].Rows[0].ItemArray[6].ToString();
        txttotalamend.Text=ds.Tables["Amndpodetails"].Rows[0].ItemArray[7].ToString();
        txtrevPOValue.Text = ds.Tables["Amndpodetails"].Rows[0].ItemArray[8].ToString();
        if (ds.Tables["Amndpodetails"].Rows[0].ItemArray[10].ToString() == "Service")
             lblamsertax.Text = "Service Tax";
        else
            lblamsertax.Text = "Excise Duty";
        txtprPOvalue.Text = ds.Tables["Amndpodetails"].Rows[0].ItemArray[11].ToString();
    }

    protected void btnverifyPO_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("Add_ClientPo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@po_date", SqlDbType.VarChar).Value = txtStartdate.Text;
            cmd.Parameters.Add("@po_no", SqlDbType.VarChar).Value = txtPO.Text;
            cmd.Parameters.Add("@po_basicvalue", SqlDbType.Money).Value = txtBasic.Text;
            cmd.Parameters.Add("@po_servicetaxvalue", SqlDbType.Money).Value = txtTax.Text;
            cmd.Parameters.Add("@contact_id", SqlDbType.Int).Value = ViewState["contract_id"];
            cmd.Parameters.Add("@client_id", SqlDbType.VarChar).Value = ddlclientid.SelectedItem.Text;
            cmd.Parameters.Add("@subclient_id", SqlDbType.VarChar).Value = ddlsubclientid.SelectedItem.Text;
            cmd.Parameters.Add("@POComdate", SqlDbType.VarChar).Value = txtEnddate.Text;
            cmd.Parameters.Add("@MobilisationAdv", SqlDbType.VarChar).Value = ddlmob.SelectedItem.Text;
            cmd.Parameters.Add("@RABill", SqlDbType.VarChar).Value = txtRabill.Text;
            cmd.Parameters.Add("@PaydueofRABill", SqlDbType.VarChar).Value = txtpaybills.Text;
            //cmd.Parameters.Add("@POsalestax", SqlDbType.Money).Value = txtsaltax.Text;
            cmd.Parameters.Add("@POGST", SqlDbType.Money).Value = txtgst.Text;
            cmd.Parameters.Add("@AdvSettment", SqlDbType.VarChar).Value = txtadvsett.Text;
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Parameters.Add("@Roles",SqlDbType.VarChar).Value= Session["roles"].ToString();
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Update";
            cmd.Connection = con;
            con.Open();

            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Successful")
            {
                if (Session["roles"].ToString() == "Project Manager")
                    JavaScript.UPAlertRedirect(Page, "PO Verified successfully", "VerifyclientPO.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "PO Approved successfully", "VerifyclientPO.aspx");
            }

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }        
    }

    protected void btnverifyAmended_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("Add_ClientPo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@po_no", SqlDbType.VarChar).Value = txtpono.Text;
            cmd.Parameters.Add("@Amendpo_no", SqlDbType.VarChar).Value = Txtamndpo.Text;
            cmd.Parameters.Add("@Amndcontact_id", SqlDbType.Int).Value = ViewState["Amendcontract_id"];
            cmd.Parameters.Add("@POAmndComdate", SqlDbType.VarChar).Value = txtAmdDate.Text;
            cmd.Parameters.Add("@poAmndvalue", SqlDbType.Money).Value = txtamPOvalue.Text;
            cmd.Parameters.Add("@poAmndservtax", SqlDbType.Money).Value = txtAmdserv.Text;
            //cmd.Parameters.Add("@POAmndsaltax", SqlDbType.Money).Value = txtAmsal.Text;
            cmd.Parameters.Add("@POAmndgst", SqlDbType.Money).Value = txtAmgst.Text;
           // cmd.Parameters.Add("@CC_code", SqlDbType.VarChar).Value = ddlCC.SelectedValue;
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Parameters.Add("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "AmendUpdate";
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Amend Successful")
            {
                if (Session["roles"].ToString() == "Project Manager")
                    JavaScript.UPAlertRedirect(Page, "Amend PO Verified successfully", "VerifyclientPO.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Amend PO Approved successfully", "VerifyclientPO.aspx");   
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }
    }
    protected void ddlmob_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmob.SelectedValue != "Yes")
        {
            txtadvsett.Enabled = false;
            txtadvsett.Text = "";
        }
        else
            txtadvsett.Enabled = true;
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("Add_ClientPo", con);
            cmd.CommandType = CommandType.StoredProcedure;         
            cmd.Parameters.Add("@contact_id", SqlDbType.Int).Value = ViewState["contract_id"];
            cmd.Parameters.Add("@po_no", SqlDbType.VarChar).Value = txtPO.Text;
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Reject";
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Rejected")
                     JavaScript.UPAlertRedirect(Page, "PO Rejected successfully", "VerifyclientPO.aspx");           

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }        

    }
}