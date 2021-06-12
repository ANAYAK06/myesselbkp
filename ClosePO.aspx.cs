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

public partial class ClosePO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        tblpodata.Visible = false;
        if (!IsPostBack)
        {
            ddlvendor.Items.Insert(0, "Select Vendor");
            ddlpo.Items.Insert(0, "Select PO");
        }
    }
    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select cc_type from cost_center where cc_code='" + ddlcccode.SelectedValue + "' ", con);
        da.Fill(ds, "cctype");

        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            da = new SqlDataAdapter("Select distinct pono from SPPO where status='3' and balance!='0' and cc_code='"+ddlcccode.SelectedValue+"' and vendor_id='" + ddlvendor.SelectedValue + "'", con);
        }
        else if (Session["roles"].ToString() == "Project Manager")
        {
            da = new SqlDataAdapter("Select distinct pono from SPPO where status='closed' and balance!='0' and cc_code='" + ddlcccode.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
        }
        else if (Session["roles"].ToString() == "HoAdmin")
        {
            if (ds.Tables["cctype"].Rows[0].ItemArray[0].ToString() == "Performing")
                da = new SqlDataAdapter("Select distinct pono from SPPO where status='closed1' and balance!='0' and cc_code='" + ddlcccode.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "'", con);
            else
                da = new SqlDataAdapter("Select distinct pono from SPPO where status='closed' and balance!='0' and cc_code='" + ddlcccode.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "'", con);
        }
        else
        {
            da = new SqlDataAdapter("Select distinct pono from SPPO where status='closed2' and balance!='0' and cc_code='" + ddlcccode.SelectedValue + "' and vendor_id='" + ddlvendor.SelectedValue + "'", con);
        }
        da.Fill(ds, "fill");
        ddlpo.DataTextField = "pono";
        ddlpo.DataValueField = "pono";
        ddlpo.DataSource = ds.Tables["fill"];
        ddlpo.DataBind();
        ddlpo.Items.Insert(0, "Select PO");

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        hfrole.Value = Session["roles"].ToString();
        tblpodata.Visible = true;
        btnclose.Enabled = true;
        btnreject.Enabled = true;    
        fillgrid();
    }
    public void fillusername() 
    {
        da = new SqlDataAdapter("select first_name from Register r join SPPO s on s.modified_by=r.User_Name where s.pono='" + ddlpo.SelectedItem.Text + "'", con);
        da.Fill(ds, "username");
        string username = ds.Tables["username"].Rows[0]["first_name"].ToString();
    }
    public void fillgrid()
    {
        da = new SqlDataAdapter("select cc_type from cost_center where cc_code='" + ddlcccode.SelectedValue + "' ", con);
        da.Fill(ds, "cctypecheck");
        da = new SqlDataAdapter("select * from Amend_sppo where pono='" + ddlpo.SelectedItem.Text + "'", con);
        da.Fill(ds, "amend");
        if (ds.Tables["amend"].Rows.Count > 0)
            da = new SqlDataAdapter("select top 1 remarks from Amend_sppo where pono='" + ddlpo.SelectedItem.Text + "' order by id desc;select (po_value+isnull(sum(Amended_amount),0))as po_value from Amend_sppo a join sppo s on s.pono=a.pono where s.pono='" + ddlpo.SelectedItem.Text + "' group by po_value", con);             
        else
            da = new SqlDataAdapter("select remarks from sppo where pono='" + ddlpo.SelectedItem.Text + "';select po_value from sppo where pono='" + ddlpo.SelectedItem.Text + "'", con);

        da.Fill(ds, "remarks");

        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            trpm.Visible = false;
            trho.Visible = false;
            trSA.Visible = false;
            da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,balance,cc_code,dca_code,Subdca_code from SPPO where vendor_id='" + ddlvendor.SelectedValue + "' and pono='" + ddlpo.SelectedItem.Text + "' and status='3'", con);
        }
        else if (Session["roles"].ToString() == "Project Manager")
        {
            da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,balance,cc_code,dca_code,Subdca_code,isnull(SADescription,'')SADescription,modified_date from SPPO where vendor_id='" + ddlvendor.SelectedValue + "' and pono='" + ddlpo.SelectedItem.Text + "' and status='closed'", con);
            txtSAdesc.Attributes.Add("onkeydown", "javascript:return false;return false;");
            trpm.Visible = true;
            trho.Visible = false;
            trSA.Visible = false;            
        }
        else if (Session["roles"].ToString() == "HoAdmin")
        {
            if (ds.Tables["cctypecheck"].Rows[0].ItemArray[0].ToString() == "Performing")
            {
                da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,balance,cc_code,dca_code,Subdca_code,isnull(SADescription,'')SADescription,isnull(PMDescription,'')PMDescription,modified_date from SPPO where vendor_id='" + ddlvendor.SelectedValue + "' and pono='" + ddlpo.SelectedItem.Text + "' and status='closed1'", con);
                trpm.Visible = true;
            }
            else
            {
                da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,balance,cc_code,dca_code,Subdca_code,isnull(SADescription,'')SADescription,isnull(PMDescription,'')PMDescription,modified_date from SPPO where vendor_id='" + ddlvendor.SelectedValue + "' and pono='" + ddlpo.SelectedItem.Text + "' and status='closed'", con);
                trpm.Visible = false;
            }
            txtSAdesc.Attributes.Add("onkeydown", "javascript:return false;return false;");
            txtPMdesc.Attributes.Add("onkeydown", "javascript:return false;return false;");

            trho.Visible = true;
            trSA.Visible = false;
        }
        else
        {
            da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,balance,cc_code,dca_code,Subdca_code,isnull(SADescription,'')SADescription,isnull(PMDescription,'')PMDescription,isnull(HODescription,'')HODescription,modified_date from SPPO S where vendor_id='" + ddlvendor.SelectedValue + "' and pono='" + ddlpo.SelectedItem.Text + "' and status='closed2'", con);
            txtSAdesc.Attributes.Add("onkeydown", "javascript:return false;return false;");
            txtPMdesc.Attributes.Add("onkeydown", "javascript:return false;return false;");
            txtHOdesc.Attributes.Add("onkeydown", "javascript:return false;return false;");
            if (ds.Tables["cctypecheck"].Rows[0].ItemArray[0].ToString() == "Performing")
                trpm.Visible = true;
            else
                trpm.Visible = false;
            trho.Visible = true;
            trSA.Visible = true;
        }
            da.Fill(ds, "filldata");  


        txtpono.Text = ds.Tables["filldata"].Rows[0]["pono"].ToString();
        txtpodate.Text = ds.Tables["filldata"].Rows[0]["po_date"].ToString();
        txtpovalue.Text = ds.Tables["remarks1"].Rows[0]["po_value"].ToString();
        txtbalance.Text = ds.Tables["filldata"].Rows[0]["balance"].ToString();
        txtcccode.Text = ds.Tables["filldata"].Rows[0]["cc_code"].ToString();
        txtdcacode.Text = ds.Tables["filldata"].Rows[0]["dca_code"].ToString();
        txtsdca.Text = ds.Tables["filldata"].Rows[0]["Subdca_code"].ToString();
        txtremarks.Text = ds.Tables["remarks"].Rows[0]["remarks"].ToString();     

        if (Session["roles"].ToString() == "Project Manager")
        {
            txtSAdesc.Text = ds.Tables["filldata"].Rows[0]["SADescription"].ToString();
            txtclsdate.Text = Convert.ToDateTime(ds.Tables["filldata"].Rows[0]["modified_date"]).ToString("dd-MMM-yyyy");
            lblSAname.Visible = true;
        }
        else if (Session["roles"].ToString() == "HoAdmin")
        {
            txtSAdesc.Text = ds.Tables["filldata"].Rows[0]["SADescription"].ToString();
            txtPMdesc.Text = ds.Tables["filldata"].Rows[0]["PMDescription"].ToString();
            txtclsdate.Text = Convert.ToDateTime(ds.Tables["filldata"].Rows[0]["modified_date"]).ToString("dd-MMM-yyyy");
            lblSAname.Visible = true;
            lblPMname.Visible = true;
        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            txtSAdesc.Text = ds.Tables["filldata"].Rows[0]["SADescription"].ToString();
            txtPMdesc.Text = ds.Tables["filldata"].Rows[0]["PMDescription"].ToString();
            txtHOdesc.Text = ds.Tables["filldata"].Rows[0]["HODescription"].ToString();
            txtclsdate.Text = Convert.ToDateTime(ds.Tables["filldata"].Rows[0]["modified_date"]).ToString("dd-MMM-yyyy");
            lblSAname.Visible = true;
            lblPMname.Visible = true;
            lblHOName.Visible = true;
        }

        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            btnreject.Visible = false;
            txtclsdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
    }    
   
    protected void btnclose_Click(object sender, EventArgs e)
    {
        btnclose.Enabled = false;
        btnreject.Enabled = false;
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("select first_name FROM Employee_Data WHERE user_name='" + Session["user"].ToString() + "'", con);
            da.Fill(ds, "user");

            cmd = new SqlCommand("sp_Closepo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PONO", txtpono.Text);
            cmd.Parameters.AddWithValue("@balance", txtbalance.Text);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@rejdate", txtclsdate.Text);
            if (Session["roles"].ToString() == "Sr.Accountant")
                cmd.Parameters.AddWithValue("@Desc", txtSAdesc.Text + "..............(" + ds.Tables["user"].Rows[0][0].ToString() + ")");
            else if (Session["roles"].ToString() == "Project Manager")
                cmd.Parameters.AddWithValue("@Desc", txtPMdesc.Text + "..............(" + ds.Tables["user"].Rows[0][0].ToString() + ")");
            else if (Session["roles"].ToString() == "HoAdmin")
                cmd.Parameters.AddWithValue("@Desc", txtHOdesc.Text + "..............(" + ds.Tables["user"].Rows[0][0].ToString() + ")");
            else
                cmd.Parameters.AddWithValue("@Desc", txtSdesc.Text + "..............("+ ds.Tables["user"].Rows[0][0].ToString()+")");   
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();

            if (msg == "PO Closed" || msg == "PO Closed Successfully")
                JavaScript.UPAlertRedirect(Page, msg, "ClosePO.aspx");
            else if (msg == "You could not close PO before Invoice approval")
            {
                JavaScript.UPAlert(Page, msg);
                txtSAdesc.Text = "";
            }
            else
                showalertprint("PO Closed", msg);
            con.Close();
            ddlpo.SelectedItem.Text = "Select PO";
            tblpodata.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }        
    }
    public void showalertprint(string message, string pono)
    {
        string script = @"window.alert('" + message + "');window.location ='ClosePO.aspx';window.open('Closepoprint.aspx?pono=" + pono + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert", script, true);
    }
    protected void btnreject_Click(object sender, EventArgs e)
    {
        btnclose.Enabled = false;
        btnreject.Enabled = false;
        cmd.Connection = con;
        cmd.CommandText = "update sppo set status='3',SADescription='',HODescription='',PMDescription='' where pono='" + txtpono.Text + "'";
        con.Open();
        int i = cmd.ExecuteNonQuery();
        if (i == 1)
            JavaScript.UPAlertRedirect(Page, "Successfully Rejected", "ClosePO.aspx");
        else
            JavaScript.UPAlert(Page, "Successfully Rejected");
        con.Close();
        ddlpo.SelectedItem.Text = "Select PO";
        tblpodata.Visible = false;
    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("select cc_type from cost_center where cc_code='" + ddlcccode.SelectedValue + "' ", con);
            da.Fill(ds, "cctypes");

            if (Session["roles"].ToString() == "Sr.Accountant")
                da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.cc_code='" + ddlcccode.SelectedValue + "' and s.status in('3') and Balance!='0' order by Name", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.cc_code='" + ddlcccode.SelectedValue + "' and s.status in('Closed') order by Name", con);
            else if (Session["roles"].ToString() == "HoAdmin")
            {
                if (ds.Tables["cctypes"].Rows[0].ItemArray[0].ToString() == "Performing")
                    da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.cc_code='" + ddlcccode.SelectedValue + "' and s.status in('Closed1') order by Name", con);
                else
                    da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.cc_code='" + ddlcccode.SelectedValue + "' and s.status in('Closed') order by Name", con);
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.cc_code='" + ddlcccode.SelectedValue + "' and s.status in('Closed2') order by Name", con);
            da.Fill(ds, "fill");
            ddlvendor.DataTextField = "Name";
            ddlvendor.DataValueField = "vendor_id";
            ddlvendor.DataSource = ds.Tables["fill"];
            ddlvendor.DataBind();
            ddlvendor.Items.Insert(0, "Select vendor");           
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
}
