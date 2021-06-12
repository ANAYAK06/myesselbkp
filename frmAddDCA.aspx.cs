using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Data.SqlClient;

public partial class Admin_frmAddDCA : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    SqlDataAdapter da1 = new SqlDataAdapter();

    string dcaCode, dcaName, date, subdca, subdcaname, itc, itcname, dcac, itc1;
    private static string dca_code;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 23);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");

        if (!IsPostBack)
        {
            populatetaxes();
            ccspan.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
            clear();
        }
        else
        {
            ccspan.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
        }
        
    }

    [WebMethod]
    public static string GetDCADetails(string DCA)
    {
        string Values = "";

        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter();
        daWB = new SqlDataAdapter("Select cc_type from dcatype dt join dca d on dt.dca_code=d.dca_code  where dt.dca_code='" + DCA + "' and status='Active'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "DCA");

        for (int i = 0; i < dsWB.Tables["DCA"].Rows.Count; i++)
        {
            Values += "|" + dsWB.Tables["DCA"].Rows[i].ItemArray[0].ToString();
        }

        return Values;
    }
    [WebMethod]
    public static string GetPaymentDetails(string DCA)
    {
        string Values = "";

        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter();
        daWB = new SqlDataAdapter("Select paymenttype from DCA_Paymenttype where dca_code='" + DCA + "' and status='Active'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "Pay");

        for (int i = 0; i < dsWB.Tables["Pay"].Rows.Count; i++)
        {
            Values += "|" + dsWB.Tables["Pay"].Rows[i].ItemArray[0].ToString();
        }

        return Values;
    }
    [WebMethod]
    public static string Gettaxes(string DCA)
    {
        string Values = "";

        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter();
        daWB = new SqlDataAdapter("Select tax_nos from taxdca where dca_code='" + DCA + "' and status='Active'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "nos");

        for (int i = 0; i < dsWB.Tables["nos"].Rows.Count; i++)
        {
            Values += "|" + dsWB.Tables["nos"].Rows[i].ItemArray[0].ToString();
        }
        return Values;

    }
    [WebMethod]
    public static string Gettaxetypes(string DCA)
    {
        string Values = "";

        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter();
        daWB = new SqlDataAdapter("Select Tax_Type,dca.Nature_Type FROM dbo.dca where dca_code='" + DCA + "'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "types");

        if (dsWB.Tables["types"].Rows.Count > 0)
        {
            Values = dsWB.Tables["types"].Rows[0].ItemArray[0].ToString() + "|" + dsWB.Tables["types"].Rows[0].ItemArray[1].ToString();
        }

        return Values;
    }
    [WebMethod]
    public static string Getdcaname(string DCA)
    {
        string Values = "";
        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter();
        daWB = new SqlDataAdapter("Select dca_name FROM dbo.dca where dca_code='" + DCA + "'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "names");

        if (dsWB.Tables["names"].Rows.Count > 0)
        {
            Values = dsWB.Tables["names"].Rows[0].ItemArray[0].ToString();
        }
        return Values;
    }
    public void showalert(string message)
    {
        string script = "alert(\"" + message + "\");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
    }

    public void filldcacode()
    {
        int dcacode;
        //da = new SqlDataAdapter("select distinct dca_code  from dca where dca_code not in ('DCA-Excise','DCA-SRTX','DCA-VAT')", con); Expense
        da = new SqlDataAdapter("select distinct dca_code  from dca where dca_type in ('Expense')", con);
        da.Fill(ds, "dcacode");
        if (ds.Tables[0].Rows.Count != 0)
        {
            if (rbtntype.SelectedIndex == 0)
            {
                if (rbtndcatype.SelectedIndex == 0)
                {
                    txtDcaCode.Width= Unit.Pixel(40);
                    dcacode = ds.Tables[0].Rows.Count;
                    if (ds.Tables[0].Rows.Count > 43)
                    {
                        dcacode = dcacode + 2;
                    }
                    else
                    {
                        dcacode = dcacode + 1;
                    }
                    txtDcaCode.Text = Convert.ToString(dcacode);
                    txtDcaCode.Enabled = false;
                    //  ddldca.Focus();
                }
                else if (rbtndcatype.SelectedIndex == 1)
                {
                    txtDcaCode.Width = Unit.Pixel(400);
                    txtDcaCode.Text = "";
                    txtDcaCode.Enabled = true;
                }
            }
        }
        else
        {
            if (rbtntype.SelectedIndex == 0)
            {
                if (rbtndcatype.SelectedIndex == 0)
                {
                    txtDcaCode.Width = Unit.Pixel(40);
                    txtDcaCode.Text = "01";
                    txtDcaCode.Enabled = false;
                }
                else if (rbtndcatype.SelectedIndex == 1)
                {
                    txtDcaCode.Width = Unit.Pixel(400);
                    txtDcaCode.Text = "";
                    txtDcaCode.Enabled = true;
                }
            }
        }
    }

    protected void update_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void clear()
    {
        txtDcaName.Text = "";
        txtsubdca.Text = "";
        txtsdname.Text = "";
        ddleditdca.SelectedValue = "";
        ddlsdcode.SelectedValue = "";
        CascadingDropDown3.SelectedValue = "";
        CascadingDropDown4.SelectedValue = "";
        chklist.ClearSelection();
        chkpaymenttype.ClearSelection();
    }

    protected void btnAddDCA_Click(object sender, EventArgs e)
    {
        dcaCode = "DCA-" + txtDcaCode.Text.ToUpper();
        dcaName = txtDcaName.Text;
        subdcaname = txtsdname.Text;
        itc = ddlit.SelectedValue;
        DateTime obj = new DateTime();
        obj = DateTime.Today;
        date = obj.ToString();

        dcac = ddldca.SelectedItem.Text;
        subdca = dcac + " ." + txtsubdca.Text;
        try
        {
            if (rbtntype.SelectedIndex == 0)
            {
                string CCTypes = "";
                string Types = "";
                string Paymenttypes = "";
                string Taxnos = "";
                for (int i = 0; i < chklist.Items.Count; i++)
                {
                    if (chklist.Items[i].Selected)
                    {
                        CCTypes += chklist.Items[i].Value + ",";
                    }
                    else
                    {
                        Types += chklist.Items[i].Value + ",";
                    }
                }
                for (int j = 0; j < chkpaymenttype.Items.Count; j++)
                {
                    if (chkpaymenttype.Items[j].Selected)
                    {
                        Paymenttypes += chkpaymenttype.Items[j].Value + ",";
                    }
                }
                if (rbtndcatype.SelectedIndex == 1)
                {

                    for (int k = 0; k < chktaxnos.Items.Count; k++)
                    {
                        if (chktaxnos.Items[k].Selected)
                        {
                            Taxnos += chktaxnos.Items[k].Value + ",";
                        }
                    }
                }
                cmd.Connection = con;
                cmd = new SqlCommand("AddDCA_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CCTypes", SqlDbType.VarChar).Value = CCTypes;
                cmd.Parameters.Add("@Types", SqlDbType.VarChar).Value = Types;
                cmd.Parameters.Add("@Paymenttypes", SqlDbType.VarChar).Value = Paymenttypes;
                cmd.Parameters.Add("@DCACode", SqlDbType.VarChar).Value = dcaCode;
                cmd.Parameters.Add("@DCAName", SqlDbType.VarChar).Value = dcaName;
                cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = Session["user"].ToString();
                cmd.Parameters.Add("@ITCode", SqlDbType.VarChar).Value = itc;
                cmd.Parameters.Add("@DcaType", SqlDbType.VarChar).Value = rbtndcatype.SelectedValue;
                if (rbtndcatype.SelectedIndex == 1)
                {
                    cmd.Parameters.Add("@TaxType", SqlDbType.VarChar).Value = ddltaxtype.SelectedValue;
                    cmd.Parameters.Add("@NatureType", SqlDbType.VarChar).Value = ddlnatureoftax.SelectedValue;
                    cmd.Parameters.Add("@Taxnos", SqlDbType.VarChar).Value = Taxnos;
                }
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg = "Successfull1";
                if (msg == "Successfull")
                {
                    JavaScript.UPAlertRedirect(Page, msg, "frmAddDCA.aspx");
                    trdca.Visible = false;
                    sdca.Visible = false;
                    sdname.Visible = false;
                    dcacode.Visible = false;
                    dcaname.Visible = false;
                    SubType.Visible = false;
                    trpaymenttype.Visible = false;
                    it.Visible = false;

                }
                else
                {
                    JavaScript.UPAlertRedirect(Page, msg, "frmAddDCA.aspx");
                }

            }
            else if (rbtntype.SelectedIndex == 1)
            {

                cmd.Connection = con;
                string str1, str2;
                da = new SqlDataAdapter("select 1 exist from subdca where dca_code='" + ddldca.SelectedValue + "' AND subdca_code='" + subdca + "'", con);
                da.Fill(ds, "check");
                if (ds.Tables["check"].Rows.Count == 0)
                {
                    str1 = "insert into subdca(dca_code,subdca_code,subdca_name,date,it_code,mapsubdca_code) values(@dcacode,@subdcacode,@subdcaname,@date,@itcode,@subdcacode)";
                    str2 = "Update dca set it_code='' where dca_code=@dcacode";
                    cmd.CommandText = str1 + str2;
                    cmd.Parameters.Add("@dcacode", SqlDbType.VarChar).Value = ddldca.SelectedValue;
                    cmd.Parameters.Add("@subdcacode", SqlDbType.VarChar).Value = subdca;
                    cmd.Parameters.Add("@subdcaname", SqlDbType.VarChar).Value = subdcaname;
                    cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = date;
                    cmd.Parameters.Add("@itcode", SqlDbType.VarChar).Value = itc;
                    con.Open();
                    int j = Convert.ToInt32(cmd.ExecuteNonQuery());
                    //int j = 0;
                    if (j > 0)
                    {
                        JavaScript.UPAlertRedirect(Page, "Successfull", "frmAddDCA.aspx");
                        trdca.Visible = false;
                        sdca.Visible = false;
                        sdname.Visible = false;
                        dcacode.Visible = false;
                        dcaname.Visible = false;
                        SubType.Visible = false;
                        trpaymenttype.Visible = false;
                        it.Visible = false;
                    }
                    else
                    {
                        JavaScript.UPAlertRedirect(Page, "Failed", "frmAddDCA.aspx");
                    }
                }
                else
                {
                    JavaScript.UPAlertRedirect(Page, "SUBDCA CODE ALREADY EXIST", "frmAddDCA.aspx");
                }
            }
            clear();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());

        }
        finally
        {
            con.Close();
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string Taxnos = "";
        try
        {
            if (rbtnedit.SelectedIndex == 0)
            {
                string CCTypes = "";

                for (int i = 0; i < chkedit.Items.Count; i++)
                {
                    if (chkedit.Items[i].Selected)
                    {
                        CCTypes += chkedit.Items[i].Value + ",";
                    }

                }
                string PaymentTypes = "";
                for (int j = 0; j < chkeditttype.Items.Count; j++)
                {
                    if (chkeditttype.Items[j].Selected)
                    {
                        PaymentTypes += chkeditttype.Items[j].Value + ",";
                    }
                }
                if (rbtndcatypep.SelectedIndex == 1)
                {

                    for (int m = 0; m < chktaxnosp.Items.Count; m++)
                    {
                        if (chktaxnosp.Items[m].Selected)
                        {
                            Taxnos += chktaxnosp.Items[m].Value + ",";
                        }
                    }
                }
                cmd.Connection = con;
                cmd = new SqlCommand("UpdateDCA_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CC_Types", SqlDbType.VarChar).Value = CCTypes.ToString();
                cmd.Parameters.Add("@CC_PaymentTypes", SqlDbType.VarChar).Value = PaymentTypes.ToString();
                cmd.Parameters.Add("@DCACode", SqlDbType.VarChar).Value = ddleditdca.SelectedValue;
                cmd.Parameters.Add("@DCAName", SqlDbType.VarChar).Value = txteditdcaname.Text;
                cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = Session["user"].ToString();
                if (rbtndcatypep.SelectedIndex == 1)
                {
                    cmd.Parameters.Add("@CC_TaxNos", SqlDbType.VarChar).Value = Taxnos.ToString();
                }
                con.Open();
                int k = cmd.ExecuteNonQuery();
                //int k = 1;
                if (k >= 1)
                {
                    JavaScript.UPAlertRedirect(Page, "Sucessfully Updated", "frmAddDCA.aspx");

                }
            }
            else if (rbtnedit.SelectedIndex == 1)
            {
                cmd.Connection = con;
                cmd.CommandText = "Update subdca set subdca_name='" + txtname.Text + "' where subdca_code='" + ddlsdcode.SelectedValue + "'";
                con.Open();
                int j = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (j > 0)
                {
                    JavaScript.UPAlertRedirect(Page, "Successfull", "frmAddDCA.aspx");
                }
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Failed", "frmAddDCA.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());

        }
        finally
        {
            con.Close();
        }

    }


    protected void ddldca_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CascadingDropDown4.SelectedValue != "0:::Select DCA:::")
        {
            int subdca;
            da1 = new SqlDataAdapter("select subdca.subdca_code,dca_name  from dca join  subdca on dca.dca_code=subdca.dca_code where dca.dca_code='" + ddldca.SelectedValue + "'", con);
            da1.Fill(ds1, "subdcacode");
            if (ds1.Tables[0].Rows.Count != 0)
            {
                subdca = ds1.Tables[0].Rows.Count;
                subdca = subdca + 1;
                txtsubdca.Text = Convert.ToString(subdca);
                txtsubdca.Enabled = false;
                dcacode.Visible = false;
                dcaname.Visible = true;
                txtDcaName.Text = ds1.Tables["subdcacode"].Rows[0][1].ToString();
                SubType.Visible = false;
                trpaymenttype.Visible = false;
            }
            else
            {
                txtsubdca.Text = "1";
                //ddldca.Focus();
                txtsubdca.Enabled = false;
                dcacode.Visible = false;
                dcaname.Visible = false;
                SubType.Visible = false;
                trpaymenttype.Visible = false;

            }
        }
    }

    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntype.SelectedIndex == 0)
        {
            dcacode.Visible = false;
            sdca.Visible = false;
            sdname.Visible = false;
            dcaname.Visible = false;
            trdca.Visible = false;
            it.Visible = false;
            btn.Visible = false;
            SubType.Visible = false;
            trpaymenttype.Visible = false;
            trdcatype.Visible = true;
            trtaxtype.Visible = false;
            trnatureoftax.Visible = false;
            trtaxnos.Visible = false;
        }
        else if (rbtntype.SelectedIndex == 1)
        {

            dcacode.Visible = false;
            sdca.Visible = true;
            sdname.Visible = true;
            dcaname.Visible = true;
            trdca.Visible = true;
            it.Visible = true;
            btn.Visible = true;
            SubType.Visible = false;
            trpaymenttype.Visible = false;
            trdcatype.Visible = false;
            trtaxtype.Visible = false;
            trnatureoftax.Visible = false;
            trtaxnos.Visible = false;
        }
    }

    protected void rbtndcatype_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldcacode();
        if (rbtndcatype.SelectedIndex == 0)
        {
            dcacode.Visible = true;
            sdca.Visible = false;
            sdname.Visible = false;
            dcaname.Visible = true;
            trdca.Visible = false;
            it.Visible = true;
            btn.Visible = true;
            SubType.Visible = true;
            trpaymenttype.Visible = true;
            trdcatype.Visible = true;
            trtaxtype.Visible = false;
            trnatureoftax.Visible = false;
            trtaxnos.Visible = false;
        }
        else if (rbtndcatype.SelectedIndex == 1)
        {
            dcacode.Visible = true;
            sdca.Visible = false;
            sdname.Visible = false;
            dcaname.Visible = true;
            trdca.Visible = false;
            it.Visible = true;
            btn.Visible = true;
            SubType.Visible = true;
            trpaymenttype.Visible = true;
            trdcatype.Visible = true;
            trtaxtype.Visible = true;
            trnatureoftax.Visible = true;
            trtaxnos.Visible = true;

        }
    }
    public void populatetaxes()
    {
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "TaxNosFinder_sp";
        cmd.Connection = con;
        con.Open();
        using (SqlDataReader sdr = cmd.ExecuteReader())
        {
            while (sdr.Read())
            {
                ListItem item = new ListItem();
                item.Text = sdr["No"].ToString();
                item.Value = sdr["No"].ToString();
                chktaxnos.Items.Add(item);
                chktaxnosp.Items.Add(item);
            }
            sdr.Close();
        }
        con.Close();
    }
    protected void rbtndcatypep_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (rbtndcatypep.SelectedIndex == 0)
        {
            da = new SqlDataAdapter("Select * from dca where dca_type='Expense'", con);
        }
        else if (rbtndcatypep.SelectedIndex == 1)
        {
            da = new SqlDataAdapter("Select * from dca where dca_type='Tax'", con);
        }
        else
        {
            da = new SqlDataAdapter("Select * from dca ", con);
        }
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddleditdca.DataSource = dt;
        ddleditdca.DataBind();
        ddleditdca.DataTextField = "mapdca_code";
        ddleditdca.DataValueField = "dca_code";
        ddleditdca.DataBind();
        ddleditdca.Items.Insert(0, new ListItem("Select DCA", "0"));
        if (rbtndcatypep.SelectedIndex == 0)
        {
            trtaxtype_p.Visible = false;
            trnatureoftax_p.Visible = false;
            treditsudca.Visible = false;
            treditsubdcaname.Visible = false;
            trtaxnos_p.Visible = false;
            treditdca.Visible = true;
            trdcaname.Visible = true;
            tredittype.Visible = true;
            treditpaymenttype.Visible = true;
            trbtnupdate.Visible = true;           
            chkedit.ClearSelection();
            chkeditttype.ClearSelection();
            txteditdcaname.Text = "";
        }
        else if (rbtndcatypep.SelectedIndex == 1)
        {

            trtaxtype_p.Visible = true;
            trnatureoftax_p.Visible = true;
            treditsudca.Visible = false;
            treditsubdcaname.Visible = false;
            trtaxnos_p.Visible = true;
            trdcaname.Visible = true;
            tredittype.Visible = true;
            treditpaymenttype.Visible = true;
            trbtnupdate.Visible = true;
            chktaxnosp.ClearSelection();
            chkedit.ClearSelection();
            chkeditttype.ClearSelection();
            treditdca.Visible = true;
            txteditdcaname.Text = "";
        }
    }
    protected void rbtnedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        rbtndcatypep.ClearSelection();
        if (rbtnedit.SelectedIndex == 1)
        {
            da = new SqlDataAdapter("Select * from dca ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddleditdca.DataSource = dt;
            ddleditdca.DataBind();
            ddleditdca.DataTextField = "mapdca_code";
            ddleditdca.DataValueField = "dca_code";
            ddleditdca.DataBind();
            ddleditdca.Items.Insert(0, new ListItem("Select DCA", "0"));
        }
        if (rbtnedit.SelectedIndex == 0)
        {
            trdcatype_p.Visible = false;
            trtaxtype_p.Visible = false;
            trnatureoftax_p.Visible = false;
            trdcaname.Visible = false;
            trtaxnos_p.Visible = false;
            tredittype.Visible = false;
            treditpaymenttype.Visible = false;
            trdcatype_p.Visible = true;
            treditdca.Visible = false;
            treditsudca.Visible = false;
            treditsubdcaname.Visible = false;
            trbtnupdate.Visible = false;

        }
        else if (rbtnedit.SelectedIndex == 1)
        {
            trdcatype_p.Visible = false;
            trtaxtype_p.Visible = false;
            trnatureoftax_p.Visible = false;
            trdcaname.Visible = false;
            trtaxnos_p.Visible = false;
            tredittype.Visible = false;
            treditpaymenttype.Visible = false;
            rbtndcatype.Visible = false;            
            trdcatype_p.Visible = false;
            treditdca.Visible = true;
            treditsudca.Visible = true;
            treditsubdcaname.Visible = true;
            trbtnupdate.Visible = true;
        }
       
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (rbtnedit.SelectedIndex == 1)
        {
            da = new SqlDataAdapter("Select * from subdca where dca_code='" + ddleditdca.SelectedValue + "' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlsdcode.DataSource = dt;
            ddlsdcode.DataBind();
            ddlsdcode.DataTextField = "mapsubdca_code";
            ddlsdcode.DataValueField = "subdca_code";
            ddlsdcode.DataBind();
            ddlsdcode.Items.Insert(0, new ListItem("Select Sub DCA", "0"));
            trdcatype_p.Visible = false;
            trtaxtype_p.Visible = false;
            trnatureoftax_p.Visible = false;
            trdcaname.Visible = false;
            trtaxnos_p.Visible = false;
            tredittype.Visible = false;
            treditpaymenttype.Visible = false;
        }
        else
        {
            if (rbtndcatypep.SelectedIndex == 1)
            {
                trdcatype_p.Visible = true;
                trtaxtype_p.Visible = true;
                trnatureoftax_p.Visible = true;
                trdcaname.Visible = true;
                trtaxnos_p.Visible = true;
                tredittype.Visible = true;
                treditpaymenttype.Visible = true;
            }
            if (rbtndcatypep.SelectedIndex == 0)
            {
                trdcatype_p.Visible = true;
                trtaxtype_p.Visible = false;
                trnatureoftax_p.Visible = false;
                trdcaname.Visible = true;
                trtaxnos_p.Visible = false;
                tredittype.Visible = true;
                treditpaymenttype.Visible = true;
            }
           
        }
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "upindent", "javascript:GetDCA()", true);
    }
   
}