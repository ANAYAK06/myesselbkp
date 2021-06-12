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
using System.Web.Services.Protocols;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;



public partial class Admin_frmViewcashflow : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da=new SqlDataAdapter();
    SqlDataAdapter da1 = new SqlDataAdapter();
    SqlDataAdapter da2 = new SqlDataAdapter();

    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        //esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 45);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
        else
        {
            hfrole.Value = Session["roles"].ToString();
            if (Session["roles"].ToString() == "Accountant")
            {
                trcc.Visible = false;
                da = new SqlDataAdapter("select balance from cost_center where cc_code='" + Session["CC_CODE"].ToString() + "'", con);
                da.Fill(ds, "Accountant");
                lblbalance.Text = ds.Tables["Accountant"].Rows[0].ItemArray[0].ToString();
                lblavl.Visible = true;
                lblbalance.Visible = true;
            }
            else if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Project Manager")
            {
                trcc.Visible = true;
            }
            if (!IsPostBack)
            {
                if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager" ||  Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    ViewState["condition"] = " and REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')='" + System.DateTime.Now.ToShortDateString() + "'";
                }
                else
                {
                    ViewState["condition"] = " and cc_code='" + Session["cc_code"].ToString() + "' and REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')='" + System.DateTime.Now.ToShortDateString() + "'";
                }
                detailgridview(GridView1);
                total();
                LoadYear();
                trcc.Visible = false;
                dca.Visible = false;
                it.Visible = false;
                year.Visible = false;
                btn.Visible = false;
                sdca.Visible = false;
                paid.Visible = false;
                lblavl.Visible = false;
                lblbalance.Visible = false;
                trexcel.Visible = false;
            }
           
        }

        

    }
    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        paymenttype();
     
    }
    private void paymenttype()
    {
       

        if (rbtntype.SelectedIndex == 0)
        {
            if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                trcc.Visible = true;
                dca.Visible = true;
                it.Visible = false;
                year.Visible = true;
                btn.Visible = true;
                sdca.Visible = true;
                paid.Visible = true;

            }
            else
            {
                trcc.Visible = false;
                dca.Visible = true;
                it.Visible = false;
                year.Visible = true;
                btn.Visible = true;
                sdca.Visible = true;
                paid.Visible = true;

            }
        }
        else if (rbtntype.SelectedIndex == 1)
        {
            if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                trcc.Visible = true;
                dca.Visible = false;
                it.Visible = true;
                year.Visible = true;
                btn.Visible = true;
                sdca.Visible = false;
                paid.Visible = true;

            }
            else
            {
                trcc.Visible = false;
                dca.Visible = false;
                it.Visible = true;
                year.Visible = true;
                btn.Visible = true;
                sdca.Visible = false;
                paid.Visible = true;

            }


        }
        else
        {
        }

    }

  
    public void LoadYear()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            string condition = "";
            //if (ddlpaidagainst.SelectedValue == "Select All")
            //{
                //if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "SuperAdmin")
                //{
                //    if (ddlcccode.SelectedItem.Text != "Select All")
                //        condition = condition + " and cc_code='" + ddlcccode.SelectedValue + "' and paid_against is null";

                //}
                //else
                //{
                //    condition = condition + " and (cc_code='" + Session["cc_code"].ToString() + "' or paid_against='" + ddlcccode.SelectedValue + "')";

                //}
            //}
            //if (ddlpaidagainst.SelectedValue != "Select All")
            //{
                //if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "SuperAdmin")
                //{
                //    if (ddlcccode.SelectedItem.Text != "Select All")
                //        condition = condition + " and cc_code='" + ddlcccode.SelectedValue + "' and paid_against is null";

                //}
                //else
                //{
                //    condition = condition + " and cc_code='" + Session["cc_code"].ToString() + "'and paid_against is null";

                //}
            //}

            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    if (ddlDate.SelectedIndex != 0)
                        condition = condition + " and convert(datetime,modifiedby_date)='" + ddlmonth.SelectedValue + "/" + ddlDate.SelectedValue + "/" + yy + "'";
                    else
                    {
                        condition = condition + " and  datepart(mm,modifiedby_date)=" + ddlmonth.SelectedValue + " and datepart(yy,modifiedby_date)=" + yy;

                    }
                }
                else
                {
                    condition = condition + " and convert(datetime,modifiedby_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                }
            }

            if (ddldetailhead.SelectedItem.Text != "Select DCA" && ddldetailhead.SelectedItem.Text != "Select All" && rbtntype.SelectedIndex == 0)
            {
                condition = condition + " and dca_code='" + ddldetailhead.SelectedItem.Text + "'";

            }
            if (ddlsubdetail.SelectedItem.Text != "Select Sub DCA" && rbtntype.SelectedIndex == 0)
            {
                condition = condition + " and sub_dca='" + ddlsubdetail.SelectedItem.Text + "'";

            }
            if (ddlit.SelectedItem.Text != "Select IT" && rbtntype.SelectedIndex == 1)
            {
                condition = condition + " and it_code='" + ddlit.SelectedItem.Text + "'";

            }
            if (ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center" && ddlcccode.SelectedValue != "")
            {
                if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Chairman Cum Managing Director")
                {
                    ////condition = condition + " and paid_against='" + ddlcccode.SelectedValue + "'";  
                    if ((Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director") && (ddlcccode.SelectedValue == ddlpaidagainst.SelectedValue))
                    {
                        condition = condition + " and paid_against is null ";
                    }
                    else
                    {
                        condition = condition + " and paid_against='" + ddlpaidagainst.SelectedValue + "'";
                    }

                }
                else

                    condition = condition + " and cc_code='" + ddlpaidagainst.SelectedValue + "'";
            }
           

            if (ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center" && ddlcccode.SelectedValue == "")
            {
                if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Chairman Cum Managing Director")

                    condition = condition + " and paid_against='" + ddlpaidagainst.SelectedValue + "'";
                else
                {
                    if ((Session["roles"].ToString() == "Accountant") && (Session["CC_CODE"].ToString() == ddlpaidagainst.SelectedValue))
                    {
                        condition = condition + " and paid_against is null ";
                    }
                    else
                    {
                        condition = condition + " and paid_against='" + ddlpaidagainst.SelectedValue + "'";
                    }
                }
            }
            //if (ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center")
            //{
            //    condition = condition + " and paid_against='" + ddlpaidagainst.SelectedValue + "'";

            //}
          

            ViewState["condition"] = condition;

            GridView1.PageIndex = 1;
            detailgridview(GridView1);
            //OpeningBalance();
            total();
            trexcel.Visible = true;

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        
    }

    //public void OpeningBalance()
    //{
    //    string obcond = "";
    //    if (ddlyear.SelectedIndex != 0)
    //    {
    //        if (ddlmonth.SelectedIndex != 0)
    //        {
    //            string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                
    //            obcond = obcond + " and datepart(mm,modifiedby_date)=" + ddlmonth.SelectedValue + " and datepart(yy,modifiedby_date)=" + yy;
    //        }
    //        else
    //        {
    //            obcond = obcond + " and convert(datetime,modifiedby_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
    //        }
    //    }


    //    //obcond = obcond + ((ddlyear.SelectedItem.Text != "Select Year" && ddlyear.SelectedItem.Text != "Select All") ? " and year='" + ddlyear.SelectedItem.Text + "'" : "");
    //    //obcond = obcond + ((ddlmonth.SelectedIndex != 0) ? " and month='" + ddlmonth.SelectedItem.Text + "'" : "");

    //    if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SrAccountant")
    //    {
    //        if (ddlcccode.SelectedItem.Text != "Select All")
    //            obcond = obcond + " and cc_code='" + ddlcccode.SelectedValue + "'";
    //        obcond = "select CC_Code,Month,Year,REPLACE(convert(varchar(50),opening_balance),'.0000','.00') [Opening Balance] from cc_ob_cb  where id>0 " + obcond + " order by modifiedby_date desc";
    //    }
    //    else
    //    {
    //        obcond = "select Month,Year,REPLACE(convert(varchar(50),opening_balance),'.0000','.00') [Opening Balance] from cc_ob_cb  where cc_code='" + Session["cc_code"].ToString() + "' " + obcond + " order by modifiedby_date desc";
    //    }

    //    da = new SqlDataAdapter(obcond, con);
    //    da.Fill(ds, "opening");
    //    grdob.DataSource = ds.Tables["opening"];
    //    grdob.DataBind();
        
    //}

   protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        detailgridview(GridView1);
        total();

    }
   protected void detailgridview(GridView gridControl)
   {
       if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
       {
           if (ddlpaidagainst.SelectedValue == "Select Cost Center" && ddlcccode.SelectedValue != "")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "' and paid_against is null AND (debit!='0' OR convert(varchar,credit)<>'NULL') " + ViewState["condition"].ToString() + " union all Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and paid_against='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + " order by modifiedby_date desc,id desc", con);
           else if (ddlpaidagainst.SelectedValue != "Select Cost Center" && ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
               ////da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 and cc_code='" + ddlpaidagainst.SelectedValue + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + " order by modifiedby_date desc,id desc", con);
           else if (ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
           else if (ddlpaidagainst.SelectedValue == "Select All" && ddlcccode.SelectedValue != "")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
           else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue == "Select All")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') " + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
           else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and paid_against is not null " + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);



       }
       else if (Session["roles"].ToString() == "Project Manager")
       {
           if (ddlpaidagainst.SelectedValue == "Select Cost Center" && ddlcccode.SelectedValue != "")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + ddlcccode.SelectedValue + "' and paid_against is null " + ViewState["condition"].ToString() + " union all Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and paid_against='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + " order by modifiedby_date desc,id desc", con);
           else if (ddlpaidagainst.SelectedValue != "Select Cost Center" && ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
               ////da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 and cc_code='" + ddlpaidagainst.SelectedValue + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + " order by modifiedby_date desc,id desc", con);
           else if (ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
           else if (ddlpaidagainst.SelectedValue == "Select All" && ddlcccode.SelectedValue != "")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
           else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue == "Select All")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') " + ViewState["condition"].ToString() + " and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') order by modifiedby_date desc,id desc", con);
           else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and paid_against in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') " + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);

       }
       else
       {
           if (ddlpaidagainst.SelectedValue == "Select Cost Center")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + Session["cc_code"].ToString() + "'and paid_against is null " + ViewState["condition"].ToString() + " union all Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and paid_against='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + " order by modifiedby_date desc,id desc", con);
           else if (ddlpaidagainst.SelectedValue != "Select Cost Center" && ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + Session["cc_code"].ToString() + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);

           else if (ddlpaidagainst.SelectedValue != "Select All")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + Session["cc_code"].ToString() + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
           else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue == "Select All")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and cc_code='" + Session["cc_code"].ToString() + "'" + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);
           else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center")
               da = new SqlDataAdapter("Select id,REPLACE(CONVERT(VARCHAR(11),modifiedby_date, 106), ' ', '-')as [modifiedby_date],REPLACE(CONVERT(VARCHAR(11),date, 106), ' ', '-')as [date],Voucher_Id,Particulars,Name,CC_Code,paid_against,DCA_Code,Sub_DCA,IT_Code,REPLACE(convert(varchar(50),credit),'.0000','.00') Credit,REPLACE(convert(varchar(50),debit),'.0000','.00') Debit,REPLACE(convert(varchar(50),balance),'.0000','.00') Balance from cash_book where id>0 AND (debit!='0' OR convert(varchar,credit)<>'NULL') and paid_against is not null " + ViewState["condition"].ToString() + "order by modifiedby_date desc,id desc", con);


       }
       
       da.Fill(ds, "onlycc");
       gridControl.DataSource = ds.Tables["onlycc"];
       gridControl.DataBind();
       //GridView1.DataSource = ds.Tables["onlycc"];
       //GridView1.DataBind();
   }
    
   

   
    //select modifiedby_date,name,cc_code,paid_against from cash_book where  paid_against='CC-12'  and datepart(mm,modifiedby_date)='4' and datepart(yy,modifiedby_date)='2011'order by modifiedby_date
    public void total()
    {
        if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
        {
            if (ddlpaidagainst.SelectedValue == "Select Cost Center" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "'and paid_against is null " + ViewState["condition"].ToString() + " union all Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and paid_against='" + ddlcccode.SelectedValue + "' " + ViewState["condition"].ToString() + "", con);
            else if (ddlpaidagainst.SelectedValue != "Select Cost Center" && ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + " ", con);
            else if (ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "", con);
            else if (ddlpaidagainst.SelectedValue == "Select All" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "", con);
            else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue == "Select All")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 " + ViewState["condition"].ToString() + "", con);
            else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and paid_against is not null " + ViewState["condition"].ToString() + "", con);


        }
        else if (Session["roles"].ToString() == "Project Manager")
        {
            if (ddlpaidagainst.SelectedValue == "Select Cost Center" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "'and paid_against is null " + ViewState["condition"].ToString() + " union all Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and paid_against='" + ddlcccode.SelectedValue + "' " + ViewState["condition"].ToString() + "", con);
            else if (ddlpaidagainst.SelectedValue != "Select Cost Center" && ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "", con);
            else if (ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "", con);
            else if (ddlpaidagainst.SelectedValue == "Select All" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + ddlcccode.SelectedValue + "'" + ViewState["condition"].ToString() + "", con);
            else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue == "Select All")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 " + ViewState["condition"].ToString() + " and  cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and paid_against in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') " + ViewState["condition"].ToString() + "", con);

        }
        else
        {
            if (ddlpaidagainst.SelectedValue == "Select Cost Center")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + Session["cc_code"].ToString() + "' and paid_against is null " + ViewState["condition"].ToString() + " union all Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and paid_against='" + ddlcccode.SelectedValue + "' " + ViewState["condition"].ToString() + "", con);
            else if (ddlpaidagainst.SelectedValue != "Select Cost Center" && ddlpaidagainst.SelectedValue != "Select All" && ddlcccode.SelectedValue != "")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and paid_against='" + Session["cc_code"].ToString() + "'" + ViewState["condition"].ToString() + "", con);
            else if (ddlpaidagainst.SelectedValue != "Select All")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + Session["cc_code"].ToString() + "'" + ViewState["condition"].ToString() + "", con);
            else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue == "Select All")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and cc_code='" + Session["cc_code"].ToString() + "'" + ViewState["condition"].ToString() + "", con);
            else if (ddlcccode.SelectedValue == "" && ddlpaidagainst.SelectedValue != "Select All" && ddlpaidagainst.SelectedValue != "Select Cost Center")
                da = new SqlDataAdapter("Select CAST(isnull(sum(Credit),0) AS Decimal(20,2)),CAST(isnull(sum(Debit),0) AS Decimal(20,2)) from cash_book where id>0 and paid_against is not null " + ViewState["condition"].ToString() + "", con);

        }

        da.Fill(ds, "check");
        if (ds.Tables["check"].Rows.Count == 2)
        {
            int i = Convert.ToInt32(ds.Tables["check"].Rows[0].ItemArray[0]) + Convert.ToInt32(ds.Tables["check"].Rows[1].ItemArray[0]);
            int j = Convert.ToInt32(ds.Tables["check"].Rows[0].ItemArray[1]) + Convert.ToInt32(ds.Tables["check"].Rows[1].ItemArray[1]);
            lblCredit.Text = Convert.ToString(i);
            lblDebit.Text = Convert.ToString(j);
        }
        else
        {
            int i = Convert.ToInt32(ds.Tables["check"].Rows[0].ItemArray[0]);
            int j = Convert.ToInt32(ds.Tables["check"].Rows[0].ItemArray[1]);
            lblCredit.Text = Convert.ToString(i);
            lblDebit.Text = Convert.ToString(j);
        }
        
    
    }



    protected void btncancel_Click(object sender, EventArgs e)
    {
       
        CascadingDropDown3.SelectedValue = "";
        CascadingDropDown2.SelectedValue = "";
        CascadingDropDown1.SelectedValue = "";
        ddlmonth.SelectedValue = "Select Month";
        ddlDate.SelectedValue = "Select modifiedby_date";
        ddlyear.SelectedIndex = 0;
    }

    protected void ddlcccode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "Auditor" || Session["roles"].ToString() == "Sr.Accountant" || Session["roles"].ToString() == "Project Manager")
        {
            da = new SqlDataAdapter("select balance from cost_center where cc_code='" + ddlcccode.SelectedValue + "'", con);
            da.Fill(ds, "others");
            if (ds.Tables["others"].Rows.Count > 0)
            {
                lblbalance.Text = ds.Tables["others"].Rows[0].ItemArray[0].ToString().Replace(".0000",".00");
                lblavl.Visible = true;
                lblbalance.Visible = true;
            }
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView2.AllowPaging = false;
        detailgridview(GridView2);
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "View Cash Flow"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView2.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
        
    }
    
}
