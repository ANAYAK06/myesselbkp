using System;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class frmAddnewClientPO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
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
            CCdetails.Visible = false;
            rbtbl.Visible = false;
            tblnewpo.Visible = false;
            tblAmendpo.Visible = false;
        }
    }
    
    public void clientid()
    {
        ddlclientid.Items.Clear();
        ddlsubclientid.Items.Clear();
        try
        {
            da = new SqlDataAdapter("Select client_id,(client_id+' , '+Client_name)as name from client order by client_id", con);
            da.Fill(ds, "client");
            ddlclientid.DataTextField = "name";
            ddlclientid.DataValueField = "client_id";
            ddlclientid.DataSource = ds.Tables["client"];
            ddlclientid.DataBind();
            ddlclientid.Items.Insert(0, "select");
            ddlsubclientid.Items.Insert(0, "select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void PONO()
    {
        ddlpo.Items.Clear();        
        try
        {
            da = new SqlDataAdapter("select po_no from po where cc_code='" + ddlCC.SelectedValue + "' union select po_no from contract where cc_code='" + ddlCC.SelectedValue + "'", con);
            //select p.po_no from po p left outer join contract c on c.po_no=p.po_no where c.cc_code='"+ddlCC.SelectedValue+"'", con);
            da.Fill(ds, "pono");
            ddlpo.DataTextField = "po_no";
            ddlpo.DataValueField = "po_no";
            ddlpo.DataSource = ds.Tables["pono"];
            ddlpo.DataBind();
            ddlpo.Items.Insert(0, "select");           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("Select client_name from client where client_id='" + ddlclientid.SelectedValue + "' ", con);
        da.Fill(ds, "clientname");
        lblclientid.Text = ds.Tables["clientname"].Rows[0].ItemArray[0].ToString();
        lblsubclientid.Text = "";
        ddlsubclientid.Items.Clear();
        try
        {
            da = new SqlDataAdapter("select distinct subclient_id,(subclient_id+' , '+branch)as name  from subclient where  client_id='" + ddlclientid.SelectedValue + "' ", con);

            da.Fill(ds, "subclient");
            if (ds.Tables["subclient"].Rows.Count > 0)
            {
                ddlsubclientid.DataTextField = "name";
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

    protected void ddlCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        CCdetails.Visible = true;
        rbtbl.Visible = true;
        tblnewpo.Visible = false;
        tblAmendpo.Visible = false;
        clear();
        rbtnPOtype.ClearSelection();
        da = new SqlDataAdapter("select d.cc_code,d.cc_name,d.cc_type,d.CC_SubType,d.final_offerno,d.final_offerdate,ref_no,ref_date,project_name,projectmanager_name,projectmanager_contactno,address,cc_inchargename,cc_incharge_phno,cc_phoneno from Cost_Center d left outer join contract c on c.cc_code=d.cc_code where d.cc_code='" + ddlCC.SelectedValue + "'", con);
        da.Fill(ds, "details");
        if (ds.Tables["details"].Rows.Count > 0)
        {
            ccode.Text = ds.Tables["details"].Rows[0].ItemArray[0].ToString();
            ccname.Text = ds.Tables["details"].Rows[0].ItemArray[1].ToString();
            lblcctype.Text = ds.Tables["details"].Rows[0].ItemArray[2].ToString();
            lblccsubtype.Text = ds.Tables["details"].Rows[0].ItemArray[3].ToString();
            epplfinalofferno.Text = ds.Tables["details"].Rows[0].ItemArray[4].ToString();
            finalofferdate.Text = ds.Tables["details"].Rows[0].ItemArray[5].ToString();
            refno.Text = ds.Tables["details"].Rows[0].ItemArray[6].ToString();
            date.Text = ds.Tables["details"].Rows[0].ItemArray[7].ToString();
            lblpname.Text = ds.Tables["details"].Rows[0].ItemArray[8].ToString();
            piname.Text = ds.Tables["details"].Rows[0].ItemArray[9].ToString();
            cno.Text = ds.Tables["details"].Rows[0].ItemArray[10].ToString();
            add.Text = ds.Tables["details"].Rows[0].ItemArray[11].ToString();
            incname.Text = ds.Tables["details"].Rows[0].ItemArray[12].ToString();
            inphno.Text = ds.Tables["details"].Rows[0].ItemArray[13].ToString();
            phno.Text = ds.Tables["details"].Rows[0].ItemArray[14].ToString();
            addres.Text = ds.Tables["details"].Rows[0].ItemArray[11].ToString();
            if (ds.Tables["details"].Rows[0].ItemArray[3].ToString() == "Service")
            {
                Lblsertax.Text = "Service Tax";
                lblamsertax.Text = "Service Tax";
            }
            else
            {
                Lblsertax.Text = "Excise Duty";
                lblamsertax.Text = "Excise Duty";

            }
        }
    }
    protected void rbtnPOtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        if (rbtnPOtype.SelectedIndex == 0)
        {

            clientid();
            tblnewpo.Visible = true;
            tblAmendpo.Visible = false;
        }
        if (rbtnPOtype.SelectedIndex == 1)
        {
            PONO();
            tblnewpo.Visible = false;
            tblAmendpo.Visible = true;
        }
    }
    protected void btnAddPO_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("Add_ClientPo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@po_date", SqlDbType.VarChar).Value = txtStartdate.Text;
            cmd.Parameters.Add("@po_no", SqlDbType.VarChar).Value = txtPO.Text;
            cmd.Parameters.Add("@po_basicvalue", SqlDbType.Money).Value = txtBasic.Text;
            cmd.Parameters.Add("@po_servicetaxvalue", SqlDbType.Money).Value = txtTax.Text;
            cmd.Parameters.Add("@CC_code", SqlDbType.VarChar).Value = ddlCC.SelectedValue;
            cmd.Parameters.Add("@client_id", SqlDbType.VarChar).Value = ddlclientid.SelectedValue;
            cmd.Parameters.Add("@subclient_id", SqlDbType.VarChar).Value = ddlsubclientid.SelectedValue;
            cmd.Parameters.Add("@POComdate", SqlDbType.VarChar).Value = txtEnddate.Text;
            cmd.Parameters.Add("@MobilisationAdv", SqlDbType.VarChar).Value = ddlmob.SelectedItem.Text;
            cmd.Parameters.Add("@RABill", SqlDbType.VarChar).Value = txtRabill.Text;
            cmd.Parameters.Add("@PaydueofRABill", SqlDbType.VarChar).Value = txtpaybills.Text;
            //cmd.Parameters.Add("@POsalestax", SqlDbType.Money).Value = txtsaltax.Text;
            cmd.Parameters.Add("@POGST", SqlDbType.Money).Value = txtgst.Text;
            cmd.Parameters.Add("@AdvSettment", SqlDbType.VarChar).Value = txtadvsett.Text;
            cmd.Parameters.Add("@user",SqlDbType.VarChar).Value=Session["user"].ToString();
            //cmd.Parameters.Add("@Roles",SqlDbType.VarChar).Value= Session["roles"].ToString();
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Add";
            cmd.Connection = con;
            con.Open();

            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            JavaScript.UPAlertRedirect(Page, msg, "frmAddnewClientPO.aspx");    

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
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("Select branch from subclient where subclient_id='" + ddlsubclientid.SelectedValue + "' ", con);
        da.Fill(ds, "subclientname");
        if (ds.Tables["subclientname"].Rows.Count > 0)
            lblsubclientid.Text = ds.Tables["subclientname"].Rows[0].ItemArray[0].ToString();
    }
    protected void ddlpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select top 1 po_amendedno from po_amended where po_no='" + ddlpo.SelectedItem.Text + "' order by po_amendedno desc;select cast(SUM(ISNULL(po_basicvalue,0)+isnull(po_servicetaxvalue,0)+isnull(POsalestax,0)+isnull(PO_Gst,0))as decimal(18,2))as povalue from po where po_no='" + ddlpo.SelectedItem.Text + "';SELECT cast(SUM(ISNULL(po_amendedtotalvalue,0))as decimal(18,2))as amdvalue from po_amended where po_no='" + ddlpo.SelectedItem.Text + "'", con);
        DataSet ds1 = new DataSet();
        da.Fill(ds1, "amendpono");
        if (ds1.Tables["amendpono2"].Rows.Count > 0 && ds1.Tables["amendpono2"].Rows[0][0].ToString()!="")
        {
           txtprPOvalue.Text = Convert.ToString(Convert.ToDouble(ds1.Tables["amendpono1"].Rows[0][0].ToString()) + Convert.ToDouble(ds1.Tables["amendpono2"].Rows[0][0].ToString()));
        }
        else
        {
            txtprPOvalue.Text = ds1.Tables["amendpono1"].Rows[0][0].ToString();
        }
       
        if (ds1.Tables["amendpono"].Rows.Count > 0)
        {
            if (Convert.ToDecimal(ds1.Tables["amendpono"].Rows[0].ItemArray[0].ToString()) >= 9)
                Txtamndpo.Text = (Convert.ToDecimal(ds1.Tables["amendpono"].Rows[0].ItemArray[0].ToString()) + 1).ToString();
            else
                Txtamndpo.Text = '0' + (Convert.ToDecimal(ds1.Tables["amendpono"].Rows[0].ItemArray[0].ToString()) + 1).ToString();
        }
        else
            Txtamndpo.Text="01";
        Txtamndpo.Enabled = false;
        ds1.Reset();       

    }

    protected void btnPOAmended_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("Add_ClientPo", con);
            cmd.CommandType = CommandType.StoredProcedure;         
            cmd.Parameters.Add("@po_no", SqlDbType.VarChar).Value = ddlpo.SelectedItem.Text;
            cmd.Parameters.Add("@Amendpo_no", SqlDbType.VarChar).Value = Txtamndpo.Text;
            cmd.Parameters.Add("@POAmndComdate", SqlDbType.VarChar).Value = txtAmdDate.Text;
            cmd.Parameters.Add("@poAmndvalue", SqlDbType.Money).Value = txtamPOvalue.Text;
            cmd.Parameters.Add("@poAmndservtax", SqlDbType.Money).Value = txtAmdserv.Text;
            //cmd.Parameters.Add("@POAmndsaltax", SqlDbType.Money).Value = txtAmsal.Text;
            cmd.Parameters.Add("@POAmndgst", SqlDbType.Money).Value = txtAmgst.Text;
            cmd.Parameters.Add("@CC_code", SqlDbType.VarChar).Value = ddlCC.SelectedValue;
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
            //cmd.Parameters.Add("@Roles", SqlDbType.VarChar).Value = Session["roles"].ToString();
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "Amend";
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            JavaScript.UPAlertRedirect(Page, msg, "frmAddnewClientPO.aspx");

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
    public void clear()
    {
        txtAmdDate.Text = "";
        Txtamndpo.Text = "";
        txtamPOvalue.Text = "";
        txtAmdserv.Text = "";
        txtAmgst.Text = "";
        txtprPOvalue.Text = "";
        txtrevPOValue.Text = "";
        txttotalamend.Text = "";
    }
    protected void Btnreset_Click(object sender, EventArgs e)
    {
        ddlpo.Items.Clear();
        clear();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtStartdate.Text = "";
        txtPO.Text = "";
        txtBasic.Text = "";
        txtTax.Text = "";
        txtEnddate.Text = "";
        txtRabill.Text = "";
        txtpaybills.Text = "";
        txtgst.Text = "";
        txtadvsett.Text = "";
        txtTotal.Text = "";       

    }
    protected void ddlmob_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmob.SelectedValue != "1")
            txtadvsett.Enabled = false;
       
        else
            txtadvsett.Enabled = true;
    }
}