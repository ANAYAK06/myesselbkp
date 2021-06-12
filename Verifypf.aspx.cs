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

public partial class Verifypf : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
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
            fillgrid();
            tblinvoice.Visible = false;
        }

    }
    public void fillgrid()
    {
        try
        {
            //if (Session["roles"].ToString() == "HoAdmin")
            //{
            ds.Clear();
            da = new SqlDataAdapter("Select id,CC_Code,Sub_DCA,Bank_Name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,replace(Debit,'.0000','.00')debit from bankbook where paymenttype='PF' and status='0A'", con);
            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
            //}
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
  
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewEditIndex]["id"].ToString();
        ViewState["id"] = id;
        filltable();
        tblinvoice.Visible = true;

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_RejectPFgeneration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = GridView1.DataKeys[e.RowIndex]["id"].ToString();
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Rejected")
            {
                JavaScript.UPAlertRedirect(Page, msg, "Verifypf.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
            fillgrid();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void filltable()
    {
        da = new SqlDataAdapter("Select CC_Code,Sub_DCA,Bank_Name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as Date,replace(Debit,'.0000','.00')debit from bankbook where id='" + ViewState["id"].ToString() + "'", con);
        da.Fill(ds, "data");
        txtcccode.Text = ds.Tables["data"].Rows[0].ItemArray[0].ToString();
        txtsdca.Text = ds.Tables["data"].Rows[0].ItemArray[1].ToString();
        //txtbankname.Text = ds.Tables["data"].Rows[0].ItemArray[2].ToString();
        txtdate.Text = ds.Tables["data"].Rows[0].ItemArray[3].ToString();
        txtamount.Text = ds.Tables["data"].Rows[0].ItemArray[4].ToString();
    }

   
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = false;
        try
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_verifypf";
            cmd.Parameters.Add(new SqlParameter("@id", ViewState["id"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@NewAmt", txtamount.Text));
            cmd.Parameters.Add(new SqlParameter("@modified_date", txtdate.Text));
            cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            JavaScript.UPAlertRedirect(Page, msg, "Verifypf.aspx");

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
    protected void btnreset_Click(object sender, EventArgs e)
    {
        tblinvoice.Visible = false;
    }
    private decimal TAmount = (decimal)0.0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {           
            e.Row.Cells[4].Text = String.Format("Total : {0:#,##,##,###.00}", TAmount);
        }
    }
}
