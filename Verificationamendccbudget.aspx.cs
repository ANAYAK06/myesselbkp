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


public partial class Verificationamendccbudget : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    string s1 = "";
    string s2 = "";
    decimal amount;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                trvisible.Visible = false;
                trtype.Visible = true;
                ddl1.Visible = false;

            }
            //trvisible.Visible = false;
            //gv.Visible = false;
            // trtype.Visible = false;
            // btn.Visible = false;
            tbl.Visible = false;
        }
    }
    public void fillgrid()
    {

            if (Session["roles"].ToString() == "Project Manager")
            {
                if (ddltype.SelectedItem.Text == "Service")
                   da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Service' and c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  d.status ='1' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,replace(d.debit,'.0000','.00') as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Service' and c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  d.status ='1' and d.Debit is not null)", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Trading' and  d.status ='1' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,replace(d.debit,'.0000','.00') as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Trading' and  d.status ='1' and d.Debit is not null)", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status ='1' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,replace(d.debit,'.0000','.00') as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status ='1' and d.Debit is not null)", con);
            }
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                if (ddltype.SelectedItem.Text == "Service")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Service' and  d.status ='1A' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Service' and  d.status ='1A' and d.Debit is not null)", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Trading' and  d.status ='1A' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Trading' and  d.status ='1A' and d.Debit is not null)", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status ='1A' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status ='1A' and d.Debit is not null)", con);
             }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                if (ddltype.SelectedItem.Text == "Service")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Service' and  d.status ='2' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Service' and  d.status ='2' and d.Debit is not null)", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Trading' and  d.status ='2' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Trading' and  d.status ='2' and d.Debit is not null)", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status ='2' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status ='2' and d.Debit is not null)", con);
            }
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                if (ddltype.SelectedItem.Text == "Service")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Service' and  d.status ='2A' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Service' and  d.status ='2A' and d.Debit is not null)", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Trading' and  d.status ='2A' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Trading' and  d.status ='2A' and d.Debit is not null)", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status ='2A' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status ='2A' and d.Debit is not null)", con);
            }
        }
        else if (ddlcctype.SelectedItem.Text == "Non-Performing")
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='1'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='1'  and d.year='" + ddlyear.SelectedItem.Text + "' and debit is not null", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2'  and d.year='" + ddlyear.SelectedItem.Text + "' and debit is not null", con);
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2A'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2A'  and d.year='" + ddlyear.SelectedItem.Text + "' and debit is not null", con);

        }
        else if (ddlcctype.SelectedItem.Text == "Capital")
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='1'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='1'  and d.year='" + ddlyear.SelectedItem.Text + "' and debit is not null", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2'  and d.year='" + ddlyear.SelectedItem.Text + "' and debit is not null", con);
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2A'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2A'  and d.year='" + ddlyear.SelectedItem.Text + "' and debit is not null", con);


        }
        da.Fill(ds, "viewdetails");
        gv.DataSource = ds.Tables["viewdetails"];
        gv.DataBind();

    }



    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        fillgrid();

    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        fillgrid();

    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //    try
        //    {
        //        HiddenField hf = (HiddenField)gv.Rows[e.RowIndex].Cells[5].FindControl("h1");
        //        string id = (gv.DataKeys[e.RowIndex].Value.ToString());
        //        string ccode = gv.Rows[e.RowIndex].Cells[1].Text;
        //        TextBox credit = gv.Rows[e.RowIndex].FindControl("txtcredit") as TextBox;


        //        gv.EditIndex = -1;
        //        con.Open();
        //        if (Session["roles"].ToString() == "Project Manager")
        //        {
        //            if (ddlcctype.SelectedItem.Text == "Performing")
        //            {
        //                if (gv.Rows[e.RowIndex].Cells[3].Text == "Credit")
        //                    cmd = new SqlCommand("update AmendBudget_cc set status='1A' where id='" + id + "'", con);
        //                else if (gv.Rows[e.RowIndex].Cells[3].Text == "Debit")
        //                    cmd = new SqlCommand("update AmendBudget_cc set status='1A'  where id='" + id + "'", con);

        //            }
        //            else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        //            {
        //                if (gv.Rows[e.RowIndex].Cells[3].Text == "Credit")
        //                    cmd = new SqlCommand("update AmendBudget_cc set status='1A'  where id='" + id + "'", con);
        //                else if (gv.Rows[e.RowIndex].Cells[3].Text == "Debit")
        //                    cmd = new SqlCommand("update AmendBudget_cc setstatus='1A' where id='" + id + "'", con);

        //            }
        //            int i = cmd.ExecuteNonQuery();
        //            if (i == 1)
        //            {
        //                //JavaScript.UPAlertRedirect(Page, "Successfully", "Verificationamendccbudget.aspx");
        //                JavaScript.UPAlert(Page, "Successfully");

        //            }
        //            else
        //            {
        //                JavaScript.UPAlert(Page, "Failed");
        //            }

        //        }

        //        else if (Session["roles"].ToString() == "HoAdmin")
        //        {
        //            if (ddlcctype.SelectedItem.Text == "Performing")
        //            {
        //                if (gv.Rows[e.RowIndex].Cells[3].Text == "Credit")
        //                    cmd = new SqlCommand("update AmendBudget_cc set credit='" + credit.Text + "',status='2' where id='" + id + "'", con);
        //                else if (gv.Rows[e.RowIndex].Cells[3].Text == "Debit")
        //                    cmd = new SqlCommand("update AmendBudget_cc set debit='" + credit.Text + "',status='2'  where id='" + id + "'", con);

        //            }
        //            else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        //            {
        //                if (gv.Rows[e.RowIndex].Cells[3].Text == "Credit")
        //                    cmd = new SqlCommand("update AmendBudget_cc set credit='" + credit.Text + "',status='2'  where id='" + id + "'", con);
        //                else if (gv.Rows[e.RowIndex].Cells[3].Text == "Debit")
        //                    cmd = new SqlCommand("update AmendBudget_cc set debit='" + credit.Text + "',status='2' where id='" + id + "'", con);

        //            }
        //            int i = cmd.ExecuteNonQuery();
        //            if (i == 1)
        //            {
        //                //JavaScript.UPAlertRedirect(Page, "Successfully", "Verificationamendccbudget.aspx");
        //                JavaScript.UPAlert(Page, "Successfully");

        //            }
        //            else
        //            {
        //                JavaScript.UPAlert(Page, "Failed");
        //            }

        //        }
        //        else
        //        {
        //            cmd = new SqlCommand("sp_Amend_updateCCBudget", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = id;
        //            cmd.Parameters.AddWithValue("@CCcode", SqlDbType.VarChar).Value = ccode;
        //            cmd.Parameters.AddWithValue("@year", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
        //            cmd.Parameters.AddWithValue("@CCType", SqlDbType.VarChar).Value = ddlcctype.SelectedItem.Text;
        //            cmd.Parameters.AddWithValue("@Amount", SqlDbType.VarChar).Value = credit.Text;
        //            cmd.Parameters.AddWithValue("@type", SqlDbType.VarChar).Value = gv.Rows[e.RowIndex].Cells[3].Text;
        //            cmd.Parameters.AddWithValue("@ptype", SqlDbType.VarChar).Value = hf.Value;
        //            cmd.Connection = con;
        //            string msg = cmd.ExecuteScalar().ToString();
        //            if (msg == "Successfull")
        //                JavaScript.UPAlertRedirect(Page, msg, "Verificationamendccbudget.aspx");
        //            else
        //                JavaScript.UPAlert(Page, msg);

        //        }


        //        con.Close();
        //        fillgrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.CatchException(ex);
        //        JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        //    }


    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (Session["roles"].ToString() == "Project Manager")
        //{
        //    //e.Row.Enabled = false;

        //    e.Row.Cells[4].Enabled = false;
        //}
    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv.Visible = false;
        CascadingDropDown1.SelectedValue = "";
        ddltype.SelectedIndex = 0;
        if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
        {
            trvisible.Visible = true;
            trtype.Visible = false;
            // btn.Visible = true;
        }
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            trvisible.Visible = false;
            trtype.Visible = true;
            //btn.Visible = false;
        }
        ////if (ddlcctype.SelectedItem.Text == "Select")
        ////{
        ////    trvisible.Visible = false;
        ////    trtype.Visible = false;
        ////   // btn.Visible = false;
        ////}
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        //if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        //{
        //    trvisible.Visible = true;
        //}
        //if (ddlcctype.SelectedItem.Text == "Performing")
        //{
        //    trvisible.Visible = false;
        //}

        gv.Visible = true;
        fillgrid();
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddltype.SelectedItem.Text == "Select")
        //{
        //    btn.Visible = false;
        //    trvisible.Visible = false;
        //    btn.Visible = false;
        //    gv.Visible = false;
        //}
        //else
        //{

        //    btn.Visible = true;
        //    gv.Visible = false;
        //}

    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Verificationamendccbudget.aspx");
    }
    protected void gv_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["id"] = gv.SelectedValue.ToString();
        filltable1(gv.SelectedValue.ToString());
    }
    public void filltable1(string id)
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and d.id='" + id + "' and c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  d.status ='1' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,replace(d.debit,'.0000','.00') as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and d.id='" + id + "' and c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and  d.status ='1' and d.Debit is not null)", con);
            }
            else if (Session["roles"].ToString() == "HoAdmin")
            {
                if (ddlcctype.SelectedItem.Text == "Performing")
                {
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and d.id='" + id + "' and d.status ='1A' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where d.id='" + id + "' and c.CC_type='Performing'  and  d.status ='1A' and d.Debit is not null)", con);
                }
                else if (ddlcctype.SelectedItem.Text == "Non-Performing")
                {
                    da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='1' and d.id='" + id + "'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='1'  and d.year='" + ddlyear.SelectedItem.Text + "' and d.id='" + id + "' and debit is not null", con);
                }
                else
                {
                    da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='1' and d.id='" + id + "' and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='1'  and d.year='" + ddlyear.SelectedItem.Text + "'  and d.id='" + id + "' and debit is not null", con);
                }
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                if (ddlcctype.SelectedItem.Text == "Performing")
                {
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and d.id='" + id + "' and d.status ='2' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and d.id='" + id + "'  and  d.status ='2' and d.Debit is not null)", con);
                }
                else if (ddlcctype.SelectedItem.Text == "Non-Performing")
                {
                    da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2' and d.id='" + id + "'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2'  and d.year='" + ddlyear.SelectedItem.Text + "' and d.id='" + id + "' and debit is not null", con);
                }
                else
                {
                    da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2' and d.id='" + id + "' and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2'  and d.year='" + ddlyear.SelectedItem.Text + "' and d.id='" + id + "' and debit is not null", con);
                }

            }
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                if (ddlcctype.SelectedItem.Text == "Performing")
                {
                    da = new SqlDataAdapter("(select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and d.id='" + id + "' and d.status ='2A' and d.Credit is not null) union all (select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Performing' and d.id='" + id + "'  and  d.status ='2A' and d.Debit is not null)", con);
                }
                else if (ddlcctype.SelectedItem.Text == "Non-Performing")
                {
                    da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2A' and d.id='" + id + "'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2A'  and d.year='" + ddlyear.SelectedItem.Text + "' and d.id='" + id + "' and debit is not null", con);
                }
                else
                {
                    da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2A' and d.id='" + id + "' and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2A'  and d.year='" + ddlyear.SelectedItem.Text + "' and d.id='" + id + "' and debit is not null", con);
                }

            }
            //else if (ddlcctype.SelectedItem.Text == "Non-Performing")
            //{
            //    if (Session["roles"].ToString() == "HoAdmin")
            //        da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='1' and d.id='" + id + "'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='1'  and d.year='" + ddlyear.SelectedItem.Text + "' and d.id='" + id + "' and debit is not null", con);
            //    else if (Session["roles"].ToString() == "SuperAdmin")
            //        da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2' and d.id='" + id + "'  and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Non-Performing' and  d.status ='2'  and d.year='" + ddlyear.SelectedItem.Text + "' and d.id='" + id + "' and debit is not null", con);

            //}
            //else if (ddlcctype.SelectedItem.Text == "Capital")
            //{
            //    if (Session["roles"].ToString() == "HoAdmin")
            //        da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='1' and d.id='" + id + "' and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='1'  and d.year='" + ddlyear.SelectedItem.Text + "'  and d.id='" + id + "' and debit is not null", con);
            //    else if (Session["roles"].ToString() == "SuperAdmin")
            //        da = new SqlDataAdapter("select d.id,c.cc_code,c.cc_name,CAST(d.credit AS Decimal(20,2))as [Amount],d.year,d.status,c.CC_type,'Credit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2' and d.id='" + id + "' and d.year='" + ddlyear.SelectedItem.Text + "' and credit is not null union all select d.id,c.cc_code,c.cc_name,CAST(d.debit AS Decimal(20,2)) as [Amount],d.year,d.status,c.CC_type,'Debit' as [Type],c.status as coststatus from Cost_Center c join Amendbudget_cc d on c.cc_code=d.cc_code where c.CC_type='Capital' and  d.status ='2'  and d.year='" + ddlyear.SelectedItem.Text + "' and d.id='" + id + "' and debit is not null", con);
            //}
            da.Fill(ds, "budgetinfo1");
            if (ds.Tables["budgetinfo1"].Rows.Count > 0)
            {

                tbl.Visible = true;
                if (Session["roles"].ToString() == "Project Manager")
                {
                    txtamount.Enabled = false;
                    txtamount.Text = ds.Tables["budgetinfo1"].Rows[0][3].ToString();
                }
                else
                {
                    txtamount.Text = ds.Tables["budgetinfo1"].Rows[0][3].ToString();
                }
                lblcccode.Text = ds.Tables["budgetinfo1"].Rows[0][1].ToString();
                lblccname.Text = ds.Tables["budgetinfo1"].Rows[0][2].ToString();
                lblamendedtype.Text = ds.Tables["budgetinfo1"].Rows[0][7].ToString();
                labelverify.Text = ds.Tables["budgetinfo1"].Rows[0][0].ToString();
            }
            //else  if (ddlcctype.SelectedItem.Text == "Non-Performing"
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());

        }
    }
    //protected void btnverifyccbudget_Click(object sender, EventArgs e)
    //{

    //}
    protected void btnverifyccbudget_Click1(object sender, EventArgs e)
    {

        try
        {
            string id="";
            string Cellvalue="";
            string ptype = "";
            con.Open();
            if (Session["roles"].ToString() == "Project Manager")
            {

               // if (ddlcctype.SelectedItem.Text == "Performing")
                //{
                    foreach (GridViewRow row1 in gv.Rows)
                    {
                        if (labelverify.Text == gv.DataKeys[row1.RowIndex].Value.ToString())
                        {
                            id = gv.DataKeys[row1.RowIndex].Value.ToString();
                            //Cellvalue = row1.Cells[3].Text.ToString();
                        }
                    }
                    if (lblamendedtype.Text == "Credit")
                        cmd = new SqlCommand("update AmendBudget_cc set status='1A' where id='" + id + "'", con);
                    else if (lblamendedtype.Text == "Debit")
                        cmd = new SqlCommand("update AmendBudget_cc set status='1A'  where id='" + id + "'", con);

               // }
                //else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                //{
                //    foreach (GridViewRow row1 in gv.Rows)
                //    {
                //        if (labelverify.Text == gv.DataKeys[row1.RowIndex].Value.ToString())
                //        {
                //            id = gv.DataKeys[row1.RowIndex].Value.ToString();
                //            //Cellvalue = row1.Cells[3].Text.ToString();
                //        }
                //    }
                //    if (lblamendedtype.Text == "Credit")
                //        cmd = new SqlCommand("update AmendBudget_cc set status='1A'  where id='" + id + "'", con);
                //    else if (lblamendedtype.Text == "Debit")
                //        cmd = new SqlCommand("update AmendBudget_cc setstatus='1A' where id='" + id + "'", con);

                //}
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    //JavaScript.UPAlertRedirect(Page, "Successfully", "Verificationamendccbudget.aspx");
                    JavaScript.UPAlert(Page, "Successfully");
                    tbl.Visible = false;

                }
                else
                {
                    JavaScript.UPAlert(Page, "Failed");
                }
           }
            else if (Session["roles"].ToString() == "HoAdmin")
            {

                if (ddlcctype.SelectedItem.Text == "Performing")
                {
                    foreach (GridViewRow row1 in gv.Rows)
                    {
                        if (labelverify.Text == gv.DataKeys[row1.RowIndex].Value.ToString())
                        {
                            id = gv.DataKeys[row1.RowIndex].Value.ToString();
                            //Cellvalue = row1.Cells[3].Text.ToString();
                        }
                    }
                    if (lblamendedtype.Text == "Credit")
                        cmd = new SqlCommand("update AmendBudget_cc set credit='" + txtamount.Text + "',status='2' where id='" + id + "'", con);
                    else if (lblamendedtype.Text == "Debit")
                        cmd = new SqlCommand("update AmendBudget_cc set debit='" + txtamount.Text + "',status='2'  where id='" + id + "'", con);

                }
                else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                {
                    foreach (GridViewRow row1 in gv.Rows)
                    {
                        if (labelverify.Text == gv.DataKeys[row1.RowIndex].Value.ToString())
                        {
                            id = gv.DataKeys[row1.RowIndex].Value.ToString();
                            //Cellvalue = row1.Cells[3].Text.ToString();
                        }
                    }
                    if (lblamendedtype.Text == "Credit")
                        cmd = new SqlCommand("update AmendBudget_cc set credit='" + txtamount.Text + "',status='2'  where id='" + id + "'", con);
                    else if (lblamendedtype.Text == "Debit")
                        cmd = new SqlCommand("update AmendBudget_cc set debit='" + txtamount.Text + "',status='2' where id='" + id + "'", con);

                }
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    //JavaScript.UPAlertRedirect(Page, "Successfully", "Verificationamendccbudget.aspx");
                    JavaScript.UPAlert(Page, "Successfully");
                    tbl.Visible = false;

                }
                else
                {
                    JavaScript.UPAlert(Page, "Failed");
                }
            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {

                if (ddlcctype.SelectedItem.Text == "Performing")
                {
                    foreach (GridViewRow row1 in gv.Rows)
                    {
                        if (labelverify.Text == gv.DataKeys[row1.RowIndex].Value.ToString())
                        {
                            id = gv.DataKeys[row1.RowIndex].Value.ToString();
                            //Cellvalue = row1.Cells[3].Text.ToString();
                        }
                    }
                    if (lblamendedtype.Text == "Credit")
                        cmd = new SqlCommand("update AmendBudget_cc set credit='" + txtamount.Text + "',status='2A' where id='" + id + "'", con);
                    else if (lblamendedtype.Text == "Debit")
                        cmd = new SqlCommand("update AmendBudget_cc set debit='" + txtamount.Text + "',status='2A'  where id='" + id + "'", con);

                }
                else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                {
                    foreach (GridViewRow row1 in gv.Rows)
                    {
                        if (labelverify.Text == gv.DataKeys[row1.RowIndex].Value.ToString())
                        {
                            id = gv.DataKeys[row1.RowIndex].Value.ToString();
                            //Cellvalue = row1.Cells[3].Text.ToString();
                        }
                    }
                    if (lblamendedtype.Text == "Credit")
                        cmd = new SqlCommand("update AmendBudget_cc set credit='" + txtamount.Text + "',status='2A'  where id='" + id + "'", con);
                    else if (lblamendedtype.Text == "Debit")
                        cmd = new SqlCommand("update AmendBudget_cc set debit='" + txtamount.Text + "',status='2A' where id='" + id + "'", con);

                }
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    //JavaScript.UPAlertRedirect(Page, "Successfully", "Verificationamendccbudget.aspx");
                    JavaScript.UPAlert(Page, "Successfully");
                    tbl.Visible = false;

                }
                else
                {
                    JavaScript.UPAlert(Page, "Failed");
                }
            }

            else
            {
                foreach (GridViewRow row1 in gv.Rows)
                {
                    if (labelverify.Text == gv.DataKeys[row1.RowIndex].Value.ToString())
                    {
                        id = gv.DataKeys[row1.RowIndex].Value.ToString();
                        //Cellvalue = row1.Cells[3].Text.ToString();
                        ptype = ((HiddenField)row1.Cells[5].FindControl("h1")).Value;

                    }
                }

                cmd = new SqlCommand("sp_Amend_updateCCBudget", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = id;
                cmd.Parameters.AddWithValue("@CCcode", SqlDbType.VarChar).Value = lblcccode.Text;
                cmd.Parameters.AddWithValue("@year", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
                cmd.Parameters.AddWithValue("@CCType", SqlDbType.VarChar).Value = ddlcctype.SelectedItem.Text;
                cmd.Parameters.AddWithValue("@Amount", SqlDbType.VarChar).Value = txtamount.Text;
                cmd.Parameters.AddWithValue("@type", SqlDbType.VarChar).Value = lblamendedtype.Text;
                cmd.Parameters.AddWithValue("@ptype", SqlDbType.VarChar).Value = ptype;
                cmd.Connection = con;
                string msg = cmd.ExecuteScalar().ToString();
                if (msg == "Sucessfully Amended")
                {
                    JavaScript.UPAlert(Page, msg);
                    tbl.Visible = false;
                }
                else
                    JavaScript.UPAlertRedirect(Page, msg, "Verificationamendccbudget.aspx");

            }
              

            con.Close();
            fillgrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Verificationamendccbudget.aspx");
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            string id = "";
            string cccode1 = "";
            string amount1 = "";
            string ptype = "";
            string Cellvalue = "";
            cmd.Connection = con;
            con.Open();
            if (Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "HoAdmin" || Session["roles"].ToString() == "SuperAdmin" ||  Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
               cmd = new SqlCommand("Update [amendbudget_cc] set status='cancel' where id='" + gv.DataKeys[e.RowIndex]["id"].ToString() + "'", con);
               int i = cmd.ExecuteNonQuery();
               if (i == 1)
                   JavaScript.UPAlert(Page, "Deleted");
               else
                   JavaScript.UPAlert(Page, "Failed");
             }
            //else
            //{
            //    id = gv.DataKeys[e.RowIndex].Value.ToString();
            //    ptype = ((HiddenField)gv.Rows[e.RowIndex].Cells[5].FindControl("h1")).Value;
            //    cccode1 = gv.Rows[e.RowIndex].Cells[1].Text.ToString();
            //    amount1 = gv.Rows[e.RowIndex].Cells[4].Text.ToString();
            //    Cellvalue = gv.Rows[e.RowIndex].Cells[3].Text.ToString();

            //    cmd = new SqlCommand("sp_Amend_DeleteCCBudget", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = id;
            //    cmd.Parameters.AddWithValue("@CCcode", SqlDbType.VarChar).Value = cccode1;
            //    cmd.Parameters.AddWithValue("@year", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
            //    cmd.Parameters.AddWithValue("@CCType", SqlDbType.VarChar).Value = ddlcctype.SelectedItem.Text;
            //    cmd.Parameters.AddWithValue("@Amount", SqlDbType.VarChar).Value = amount1;
            //    cmd.Parameters.AddWithValue("@type", SqlDbType.VarChar).Value = Cellvalue;
            //    cmd.Parameters.AddWithValue("@ptype", SqlDbType.VarChar).Value = ptype;
            //    cmd.Connection = con;
            //    string msg = cmd.ExecuteScalar().ToString();
            //    if (msg == "Sucessfully Deleted")
            //    {
            //        JavaScript.UPAlertRedirect(Page, msg, "Verificationamendccbudget.aspx");
            //        //tbl.Visible = false;
            //    }
            //    else
            //        JavaScript.UPAlert(Page, msg);

            //}
            con.Close();
            //fillgrid();
            fillgrid();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
}


    


