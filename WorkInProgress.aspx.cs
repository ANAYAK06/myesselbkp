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
using System.Web.Configuration;
using System.Data.SqlClient;


public partial class WorkInProgress : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();


    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");


        if (!IsPostBack)
        {
            fillgrid();

            newclientid();
            if (Session["roles"].ToString() == "HoAdmin")
            {

                tblgrid.Visible = true;
                tblfrm.Visible = false;
            }


            else if (Session["roles"].ToString() == "Sr.Accountant")
            {

                tblgrid.Visible = false;
                tblfrm.Visible = true;
            }
            else
            {
                tblgrid.Visible = false;
                tblfrm.Visible = false;
            }
        }

        
    }
   
   
    protected void btninsert_Click(object sender, EventArgs e)
    {

        try
        {


            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_WorkInProgress";
        
            cmd.Parameters.AddWithValue("@clientid", ddlclientid.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@subclientid", ddlsubclientid.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@cccode", ddlcccode.SelectedValue);
            cmd.Parameters.AddWithValue("@pono", ddlpo.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@date", txtdate.Text);
            cmd.Parameters.AddWithValue("@amount", txtamt.Text);
            cmd.Parameters.AddWithValue("@description", txtdesc.Text);
            if (Session["roles"].ToString() == "HoAdmin")
            {
                cmd.Parameters.AddWithValue("@id", ViewState["id"].ToString());
            }
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();

            JavaScript.UPAlertRedirect(Page, msg, "WorkInProgress.aspx");
            con.Close();
            fillgrid();



        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
    {

        newsubclient(ddlclientid.SelectedValue);
    }
    protected void ddlsubclientid_SelectedIndexChanged(object sender, EventArgs e)
    {

        newcccode(ddlsubclientid.SelectedValue);

    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {

        newpono(ddlsubclientid.SelectedValue, ddlcccode.SelectedValue);
    }
    public void fillgrid()
    {
        try
        {
            da = new SqlDataAdapter("select id,client_id,subclient_id,cc_code,pono, convert(varchar(11),date,101) as date,amount,description from work_progress where status='1'", con);
            da.Fill(ds, "fillgrid");
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
   
    protected void btnreset_Click(object sender, EventArgs e)
    {
       tblfrm.Visible = false;

    }
    
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewEditIndex]["id"].ToString();
        ViewState["id"] = id;
        tblfrm.Visible = true;
        filltable();
       
       
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)GridView1.Rows[e.RowIndex];
        string id = "";
        id = GridView1.DataKeys[e.RowIndex]["id"].ToString();
        con.Open();
        cmd = new SqlCommand("update work_progress set status='Reject' where id='" + id + "'", con);
        
        int j = cmd.ExecuteNonQuery();
        con.Close();
        if (j == 1)
        {
            JavaScript.UPAlertRedirect(Page, "Deleted", "WorkInProgress.aspx");
        }
        con.Close();
       

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }

    public void filltable()
    {
        try
        {

            da = new SqlDataAdapter(" select id,client_id,subclient_id,cc_code,pono, convert(varchar(11),date,101) as date,amount,description from work_progress where id='" + ViewState["id"] + "'", con);
            da.Fill(ds, "data");
            ddlclientid.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[1].ToString();
            txtdate.Text = ds.Tables["data"].Rows[0].ItemArray[5].ToString();
            txtamt.Text = ds.Tables["data"].Rows[0].ItemArray[6].ToString();
            txtdesc.Text = ds.Tables["data"].Rows[0].ItemArray[7].ToString();
            newsubclient(ds.Tables["data"].Rows[0].ItemArray[1].ToString());
            newcccode(ds.Tables["data"].Rows[0].ItemArray[2].ToString());
            newpono(ds.Tables["data"].Rows[0].ItemArray[2].ToString(), ds.Tables["data"].Rows[0].ItemArray[3].ToString());
            ddlsubclientid.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
            ddlcccode.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[3].ToString();
            ddlpo.SelectedValue = ds.Tables["data"].Rows[0].ItemArray[4].ToString();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);

        }
    }
   
    public void newclientid()
    {
        try
        {
            da = new SqlDataAdapter("select distinct client_id from invoice where client_id is not null", con);
            da.Fill(ds, "newclient");
            ddlclientid.DataTextField = "client_id";
            ddlclientid.DataValueField = "client_id";
            ddlclientid.DataSource = ds.Tables["newclient"];
            ddlclientid.DataBind();
            ddlclientid.Items.Insert(0, "Select Client");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void newsubclient(string clientid)
    {
        try
        {
            da = new SqlDataAdapter("select distinct subclient_id from invoice where client_id='" + clientid + "'", con);
            da.Fill(ds, "newsubclient");
            ddlsubclientid.DataTextField = "subclient_id";
            ddlsubclientid.DataValueField = "subclient_id";
            ddlsubclientid.DataSource = ds.Tables["newsubclient"];
            ddlsubclientid.DataBind();
            ddlsubclientid.Items.Insert(0, "Select SubClient");
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void newcccode(string subclientid)
    {

        try
        {
            da = new SqlDataAdapter("select   distinct i.cc_code, i.cc_code+ ',' +c.cc_name as ccname  from invoice i join cost_center c on i.cc_code=c.cc_code where  subclient_id='" + subclientid + "'", con);
            da.Fill(ds, "newcccode");
            ddlcccode.DataTextField = "ccname";
            ddlcccode.DataValueField = "cc_code";
            ddlcccode.DataSource = ds.Tables["newcccode"];
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "Select CostCenter");

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void newpono(string subclientid,string cccode)
    {
        
        try
        {
            da = new SqlDataAdapter("select distinct po_no from invoice where  subclient_id='" +subclientid + "' and cc_code='" + cccode + "'", con);
            da.Fill(ds, "newpono");
            ddlpo.DataTextField = "po_no";
            ddlpo.DataValueField = "po_no";
            ddlpo.DataSource = ds.Tables["newpono"];
            ddlpo.DataBind();
            ddlpo.Items.Insert(0, "Select PO");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkInProgress.aspx");
    }
}
