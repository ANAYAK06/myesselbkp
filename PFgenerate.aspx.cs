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
using System.Collections.Specialized;
using AjaxControlToolkit;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Text;



public partial class PFgenerate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esseldb"]);
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
            BindGrid();
            filldata();
           
        }
        gvdata.Visible = true;
    }

    public DataSet ReturnMenuDetails()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select id,CC_Code,Sub_DCA,replace(isnull(Debit,0),'.0000','.00')as debit,(Select sum(isnull(debit,0)) from bankbook where status='0' and paymenttype='PF')Total from bankbook where status='0' and paymenttype='PF'", con);

        da.Fill(ds,"PF");
        if (ds.Tables["PF"].Rows.Count > 0)
        {
            txttotal.Text = ds.Tables["PF"].Rows[0][4].ToString().Replace(".0000", ".00");
            hf1.Value = ds.Tables["PF"].Rows[0][4].ToString().Replace(".0000", ".00");
        }
        else
        {
            txttotal.Text = "0";
            hf1.Value = "0";
        }
        return ds;
    }

    public DataTable ReturnEmptyDataTable()
    {
        DataTable dtMenu = new DataTable();
        DataColumn id = new DataColumn("id", typeof(System.Int32));
        dtMenu.Columns.Add(id);
        DataColumn cccode = new DataColumn("CC_Code", typeof(System.String));
        dtMenu.Columns.Add(cccode);
        DataColumn dccode = new DataColumn("Sub_DCA", typeof(System.String));
        dtMenu.Columns.Add(dccode);
        DataColumn balance = new DataColumn("Debit", typeof(System.Int32));
        dtMenu.Columns.Add(balance);
        DataRow datatRow = dtMenu.NewRow();
        dtMenu.Rows.Add(datatRow);
        return dtMenu;
    }

    public void BindGrid()
    {
        try
        {

            if (ReturnMenuDetails().Tables[0].Rows.Count > 0)
            {
                filldata();
                txtdate.Enabled = false;
              
                gvdata.DataSource = ds;
            }
            else
            {

                namedate.Visible = true;
                gvdata.DataSource = ReturnEmptyDataTable();
            }
            gvdata.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            string CCcode = ((DropDownList)gvdata.FooterRow.FindControl("ddlcccode")).SelectedValue;
            string SDcacode = ((DropDownList)gvdata.FooterRow.FindControl("ddldetailhead")).SelectedItem.Text;
            string Amount = ((TextBox)gvdata.FooterRow.FindControl("txtamount")).Text;
            cmd = new SqlCommand("sp_PFgeneration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = CCcode.ToString();
            cmd.Parameters.Add("@Sdcacode", SqlDbType.VarChar).Value = SDcacode.ToString();
            cmd.Parameters.Add("@Amount", SqlDbType.Money).Value = Amount.ToString();
            //cmd.Parameters.Add("@bankname", SqlDbType.VarChar).Value = ddlbankname.SelectedValue;
            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = txtdate.Text;
            cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = Session["user"].ToString();
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucess")
            {
                JavaScript.UPAlert(Page, msg);
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }
            BindGrid();
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void gvdata_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = gvdata.DataKeys[e.RowIndex]["id"].ToString();
            if (id == "")
            {
                JavaScript.UPAlert(Page, "There is no data to Reject");
            }
            else
            {
                cmd = new SqlCommand("sp_RejectPFgeneration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = gvdata.DataKeys[e.RowIndex]["id"].ToString();
                cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@role", Session["roles"].ToString()));
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                if (msg == "Rejected")
                {
                    JavaScript.UPAlertRedirect(Page, msg,"PFgenerate.aspx");
                }
                else
                {
                    JavaScript.UPAlert(Page, msg);
                }
                BindGrid();
                
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder strSql = new StringBuilder(string.Empty);
            foreach (GridViewRow row in gvdata.Rows)
            {
                CheckBox cc = ((CheckBox)row.FindControl("chkSelect"));
                if (cc.Checked)
                {
                    string s = "";
                    s = s + gvdata.DataKeys[row.RowIndex]["id"].ToString();
                    string UP = "update bankbook set status='0A' where id='" + s + "'";
                    SqlCommand cmd2 = new SqlCommand(UP, con);
                    da = new SqlDataAdapter(cmd2);
                    da.Fill(ds);
                    strSql.Append(UP);
               }
            }
            JavaScript.UPAlertRedirect(Page, "Successfully Updated", "PFgenerate.aspx");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    public void filldata()
    {
        da = new SqlDataAdapter("select bank_name,REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as date from bankbook where PaymentType='PF' and status='0'", con);
        da.Fill(ds, "fill");
        if (ds.Tables["fill"].Rows.Count > 0)
        {
            //ddlbankname.SelectedValue = ds.Tables["fill"].Rows[0].ItemArray[0].ToString();
            txtdate.Text = ds.Tables["fill"].Rows[0].ItemArray[1].ToString();
        }
    }

    private decimal Amount = (decimal)0.0;

    protected void gvdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[1].Text!="")
        //    {
        //        Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
        //    }
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    txttotal.Text = Amount.ToString();
        //}

    }
}
