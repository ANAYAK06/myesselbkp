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



public partial class AmendDCABudget : System.Web.UI.Page
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
            header.Visible = false;
            dcalabel.Visible = false;
            trcccode.Visible = false;
            tryear.Visible = false;
            btn.Visible = false;
            griddetails.Visible = false;
            trtype.Visible = false;
        }

        Tr1.Visible = false;
    }
    [WebMethod]
    public static string checking(string cc, string year)
    {
        SqlConnection conWB = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
        SqlDataAdapter daWB = new SqlDataAdapter("select * from yearly_dcabudget where cc_code='" + cc + "' and year='" + year + "'", conWB);
        DataSet dsWB = new DataSet();
        daWB.Fill(dsWB, "checkdca");
        conWB.Open();
        if (dsWB.Tables["checkdca"].Rows.Count > 0)
        {
            return "Budget Assigned";
        }

        else
        {
            string Values = "";
            return Values;

        }


    }

  
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                tryear.Visible = false;
                Label9.Visible = false;
                header.Visible = true;
                GridView1.Visible = true;
                tramendlistheader.Visible = true;
                GridView2.Visible = true;
                griddetails.Visible = true;

            }
            else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
            {
                tryear.Visible = true;
                Label9.Visible = true;
                header.Visible = true;
                GridView1.Visible = true;
                GridView2.Visible = true;
                tramendlistheader.Visible = true;
                griddetails.Visible = true;
            }
            fillgrid();
            getCCBudget();
            dcalabel.Visible = true;
            amenddca();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    private void fillgrid()
    {
        try
        {
            if (ddlcctype.SelectedItem.Text == "Performing")
                ////da = new SqlDataAdapter("select d.dca_code ,d.dca_name ,replace(sum(y.budget_dca_yearly),'.0000','.00')as budget_dca_yearly,replace(sum(y.dca_yearly_bal),'.0000','.00')as dca_yearly_bal  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.status='3' and y.cc_code='" + ddlCCcode.SelectedValue + "' group by d.dca_code,d.dca_name", con);
                ////da = new SqlDataAdapter("Select i.*,dt.status from(select d.dca_code ,d.dca_name ,replace(sum(y.budget_dca_yearly),'.0000','.00')as budget_dca_yearly,replace(sum(y.dca_yearly_bal),'.0000','.00')as dca_yearly_bal  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.status='3' and y.cc_code='" + ddlCCcode.SelectedValue + "' group by d.dca_code,d.dca_name)i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "') ORDER by dt.dca_code asc ", con);
                da = new SqlDataAdapter("select i.*,dt.status from(select d.dca_code ,d.dca_name ,replace(sum(y.budget_dca_yearly),'.0000','.00')as budget_dca_yearly,replace(sum(y.dca_yearly_bal),'.0000','.00')as dca_yearly_bal,d.mapdca_code  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.status='3' and y.cc_code='" + ddlCCcode.SelectedValue + "' group by d.dca_code,d.dca_name,d.mapdca_code)i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "') and i.dca_code not in (select distinct Dca_Code from Amendbudget_DCA where cc_code='" + ddlCCcode.SelectedValue + "' and status='1')ORDER by dt.dca_code asc", con);
            else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
                //da = new SqlDataAdapter("select d.dca_code ,d.dca_name ,replace(y.budget_dca_yearly,'.0000','.00') as budget_dca_yearly,replace(y.dca_yearly_bal,'.0000','.00')as dca_yearly_bal  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.status='3' and y.cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "' group by d.dca_code,d.dca_name,y.budget_dca_yearly,y.dca_yearly_bal", con);
                //da = new SqlDataAdapter("Select i.*,dt.status from(select d.dca_code ,d.dca_name ,replace(y.budget_dca_yearly,'.0000','.00') as budget_dca_yearly,replace(y.dca_yearly_bal,'.0000','.00')as dca_yearly_bal  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.status='3' and y.cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "' group by d.dca_code,d.dca_name,y.budget_dca_yearly,y.dca_yearly_bal)i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "') ORDER by dt.dca_code asc", con);
                da = new SqlDataAdapter("Select i.*,dt.status from(select d.dca_code ,d.dca_name ,replace(y.budget_dca_yearly,'.0000','.00') as budget_dca_yearly,replace(y.dca_yearly_bal,'.0000','.00')as dca_yearly_bal,d.mapdca_code  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.status='3' and y.cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "' group by d.dca_code,d.dca_name,y.budget_dca_yearly,y.dca_yearly_bal,d.mapdca_code)i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')and i.dca_code not in (select distinct Dca_Code from Amendbudget_DCA where cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "' and status='1') ORDER by dt.dca_code asc", con);
            da.Fill(ds, "dca_budget");
            if (ds.Tables["dca_budget"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["dca_budget"];
                GridView1.DataBind();
                header.Visible = true;
                GridView1.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                header.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void getCCBudget()
    {
        try
        {

            // btngridsubmit.Visible = true;
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(d.budget_amount),'.0000','.00')as budgetamount, replace(sum(d.balance),'.0000','.00')as balance  from Cost_Center c join budget_cc d on c.cc_code=d.cc_code where c.cc_code='" + ddlCCcode.SelectedValue + "' and c.cc_subtype='" + ddltype.SelectedItem.Text + "'  group by c.cc_code,c.cc_name", con);
            }
            else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
            {
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(d.budget_amount,'.0000','.00') as budget_amount, replace(d.balance,'.0000','.00')as balance ,d.year from Cost_Center c join budget_cc d on c.cc_code=d.cc_code where c.cc_code='" + ddlCCcode.SelectedValue + "' and d.year='" + ddlyear.SelectedItem.Text + "'", con);
            }
            da.Fill(ds, "normalview");
            if (ds.Tables["normalview"].Rows.Count > 0)
            {
                Label1.Text = ds.Tables["normalview"].Rows[0][0].ToString();
                Label5.Text = ds.Tables["normalview"].Rows[0][1].ToString();
                Label6.Text = ds.Tables["normalview"].Rows[0][2].ToString();
                Label7.Text = ds.Tables["normalview"].Rows[0][3].ToString().Replace(".0000", ".00");
                if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
                {
                    Label8.Text = ds.Tables["normalview"].Rows[0][4].ToString();
                }
                h1.Value = ds.Tables["normalview"].Rows[0][2].ToString();
                h2.Value = ds.Tables["normalview"].Rows[0][3].ToString();

            }
            AmendDCAValue(Convert.ToDecimal(ds.Tables["normalview"].Rows[0][3].ToString()));
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        h3.Value = Convert.ToString(GridView1.EditIndex);
        string name = GridView1.DataKeys[e.NewEditIndex].Values[0].ToString();
        //Label txt = GridView1.Rows[e.NewEditIndex].FindControl("id") as Label;
        //string name = txt.Text;
        fillgrid();
        getCCBudget();
        amenddca();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
        getCCBudget();
        amenddca();
       
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //LinkButton edit1 = (LinkButton)GridView1.FindControl("Link");
            //edit1.Enabled=false;
            LinkButton edit1=((LinkButton)GridView1.Rows[e.RowIndex].Cells[8].Controls[0]);
            edit1.Visible = false;
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);

            string dcacode = (GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            TextBox txtamount = (TextBox)row.FindControl("txtbudget");
            DropDownList type = (DropDownList)row.FindControl("ddltype");

            con.Open();
            //if (ddlcctype.SelectedItem.Text == "Performing")
            //{
            //    if (type.SelectedItem.Text == "Add")
            //        cmd = new SqlCommand("insert into Amendbudget_DCA (cc_code,Dca_Code,credit,date,createdby,status)values(@cc_code,@dcacode,@credit,@date,@createdby,@status)", con);
            //    else if (type.SelectedItem.Text == "Substract")
            //        cmd = new SqlCommand("insert into Amendbudget_DCA (cc_code,Dca_Code,debit,date,createdby,status)values(@cc_code,@dcacode,@debit,@date,@createdby,@status)", con);

            //}
            //else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
            //{
            //    if (type.SelectedItem.Text == "Add")
            //        cmd = new SqlCommand("insert into Amendbudget_DCA (cc_code,Dca_Code,credit,date,createdby,year,status)values(@cc_code,@dcacode,@credit,@date,@createdby,@year,@status)", con);
            //    else if (type.SelectedItem.Text == "Substract")
            //        cmd = new SqlCommand("insert into Amendbudget_DCA (cc_code,Dca_Code,debit,date,createdby,year,status)values(@cc_code,@dcacode,@debit,@date,@createdby,@year,@status)", con);

            //}
            cmd = new SqlCommand("sp_Amend_CCBudget", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CCcode", SqlDbType.VarChar).Value = ddlCCcode.SelectedValue;
            cmd.Parameters.Add("@dcacode", SqlDbType.VarChar).Value = dcacode;
            if (type.SelectedItem.Text == "Add")
            {
                cmd.Parameters.Add("@Amount", SqlDbType.Money).Value = txtamount.Text;
            }
            else
            {
                cmd.Parameters.Add("@Amount", SqlDbType.Money).Value = txtamount.Text;
            }
            cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
            //cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = myDate.ToString();
            cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["user"].ToString();
            if (ddlcctype.SelectedItem.Text != "Performing")
            {
                cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
            }
            cmd.Parameters.AddWithValue("@type", type.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Amendtype", "2");
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfully Inserted")
            {
                JavaScript.UPAlert(Page, msg);
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }

            con.Close();
            GridView1.EditIndex = -1;
            fillgrid();
            amenddca();
            getCCBudget();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        
    }

    public void amenddca()
    {
        try
        {

            if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                da = new SqlDataAdapter("Select ad.id,ad.dca_code,d.dca_name,replace(credit,'.0000','.00') as credit,replace(debit,'.0000','.00') as debit,d.mapdca_code from Amendbudget_DCA ad join dca d on ad.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='1' and year='" + ddlyear.SelectedItem.Text + "'", con);
            else if (ddlcctype.SelectedItem.Text == "Performing")
                da = new SqlDataAdapter("Select ad.id,ad.dca_code,d.dca_name,replace(credit,'.0000','.00') as credit,replace(debit,'.0000','.00') as debit,d.mapdca_code from Amendbudget_DCA ad join dca d on ad.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='1'", con);

            da.Fill(ds, "Amenddca");

            if (ds.Tables["Amenddca"].Rows.Count > 0)
            {
                Tr1.Visible = true;
                GridView2.DataSource = ds.Tables["Amenddca"];
                GridView2.DataBind();
                tramendlistheader.Visible = true;
                GridView2.Visible = true;
                
            }
            else
            {
                Tr1.Visible = false;
                GridView2.DataSource = null;
                GridView2.DataBind();
                tramendlistheader.Visible = false;
                GridView2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (GridView1.EditIndex == e.Row.RowIndex)
                {
                    RequiredFieldValidator reqfldVal = new RequiredFieldValidator();
                    reqfldVal.ID = "RequiredValidator1";
                    reqfldVal.ControlToValidate = "ddltype";
                    reqfldVal.ErrorMessage = "Select Type";
                    reqfldVal.InitialValue = "Select";
                    reqfldVal.SetFocusOnError = true;
                    e.Row.Cells[5].Controls.Add(reqfldVal);


                    RequiredFieldValidator reqfldVal1 = new RequiredFieldValidator();
                    reqfldVal1.ID = "RequiredValidator2";
                    reqfldVal1.ControlToValidate = "txtbudget";
                    reqfldVal1.ErrorMessage = "Amount required";
                    reqfldVal1.SetFocusOnError = true;
                    e.Row.Cells[6].Controls.Add(reqfldVal1);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                DropDownList ddldca = (DropDownList)e.Row.FindControl("ddldetailhead");
                ddldca.Items.Clear();
                if (ddlcctype.SelectedItem.Text == "Non-Performing")
                    da = new SqlDataAdapter("Select distinct dt.dca_code,d.mapdca_code from dcatype  dt JOIN dca d ON dt.dca_code=d.dca_code where dt.dca_code not in (select dca_code from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "' union all select dca_code from Amendbudget_DCA where cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "') and cc_type='Non-Performing' and status='Active' AND dt.dca_code!='DCA-VAT' order by dt.dca_code asc", con);
                else if (ddlcctype.SelectedItem.Text == "Performing")
                    da = new SqlDataAdapter("Select distinct dt.dca_code ,d.mapdca_code  from dcatype dt JOIN dca d ON dt.dca_code=d.dca_code where dt.dca_code not in (select dca_code from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "'  union all select dca_code from Amendbudget_DCA where cc_code='" + ddlCCcode.SelectedValue + "' ) AND cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "' ) and status='Active' AND dt.dca_code!='DCA-VAT' order by dt.dca_code asc", con);
                else if (ddlcctype.SelectedItem.Text == "Capital")
                    da = new SqlDataAdapter("Select distinct dt.dca_code,d.mapdca_code from dcatype dt JOIN dca d ON dt.dca_code=d.dca_code where dt.dca_code not in (select dca_code from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "' union all select dca_code from Amendbudget_DCA where cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "') and cc_type='Capital' and status='Active' AND dt.dca_code!='DCA-VAT' order by dt.dca_code asc", con);

                da.Fill(ds, "DCACodes");
                ddldca.DataTextField = "mapdca_code";
                ddldca.DataValueField = "dca_code";
                ddldca.DataSource = ds.Tables["DCACodes"];
                ddldca.DataBind();
                ddldca.Items.Insert(0, "Select DCA");

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            tramendlistheader.Visible = true;
            GridView2.Visible = true;
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
            string dcacode = ((DropDownList)GridView1.FooterRow.FindControl("ddldetailhead")).SelectedValue;
            string budamount = ((TextBox)GridView1.FooterRow.FindControl("txtbud")).Text;

            //if (ddlcctype.SelectedItem.Text == "Performing")
            //    cmd = new SqlCommand("insert into Amendbudget_DCA (cc_code,Dca_Code,credit,date,createdby,status)values(@cc_code,@dcacode,@credit,@date,@createdby,@status)", con);
            //else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
            //    cmd = new SqlCommand("insert into Amendbudget_DCA (cc_code,Dca_Code,credit,date,createdby,year,status)values(@cc_code,@dcacode,@credit,@date,@createdby,@year,@status)", con);
            cmd = new SqlCommand("sp_Amend_CCBudget", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CCcode", SqlDbType.VarChar).Value = ddlCCcode.SelectedValue;
            cmd.Parameters.Add("@dcacode", SqlDbType.VarChar).Value = dcacode.ToString();
            cmd.Parameters.Add("@Amount", SqlDbType.Money).Value = budamount.ToString();
            cmd.Parameters.AddWithValue("@type", "Add");
            cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
            if (ddlcctype.SelectedItem.Text != "Performing")
            {
                cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
            }
            cmd.Parameters.AddWithValue("@Amendtype", "2");
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfully Inserted")
            {
                JavaScript.UPAlert(Page, msg);
                
            }
            else
            {
                JavaScript.UPAlert(Page, msg);
            }

            con.Close();
            ((DropDownList)GridView1.FooterRow.FindControl("ddldetailhead")).SelectedItem.Text = "Select DCA";
            ((TextBox)GridView1.FooterRow.FindControl("txtbud")).Text = "";
            fillgrid();
            amenddca();
            getCCBudget();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow record in GridView2.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    cmd.Connection = con;
                    cmd = new SqlCommand("update [Amendbudget_DCA] set status='2' where id='" + GridView2.DataKeys[record.RowIndex]["id"].ToString() + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            JavaScript.UPAlertRedirect(Page, "Sucess", "AmendDCABudget.aspx");
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd.Connection = con;
          
            Label lblcredit = (Label)GridView2.Rows[e.RowIndex].Cells[2].FindControl("lblcredit");
            Label lbldebit = (Label)GridView2.Rows[e.RowIndex].Cells[3].FindControl("lbldebit");
            if (lbldebit.Text != "")
            {
               decimal CCbalance = Convert.ToDecimal(h2.Value);
               decimal balance = CCbalance - Convert.ToDecimal(lbldebit.Text);
               if (balance < 0)
               {
                   JavaScript.UPAlert(Page, "Insufficient CC Balance");
               }
               else
               {
                   cmd = new SqlCommand("delete from [Amendbudget_DCA] where id='" + GridView2.DataKeys[e.RowIndex]["id"].ToString() + "'", con);
                   con.Open();
                   int j = cmd.ExecuteNonQuery();
                   if (j == 1)
                       JavaScript.UPAlert(Page, "Deleted");
                      
                   else
                       JavaScript.UPAlert(Page, "Failed");
                  
                   con.Close();
                   amenddca();
                   fillgrid();
                   getCCBudget();
               }
            }
            else
            {
                cmd = new SqlCommand("delete from [Amendbudget_DCA] where id='" + GridView2.DataKeys[e.RowIndex]["id"].ToString() + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                    JavaScript.UPAlert(Page, "Deleted");
                else
                    JavaScript.UPAlert(Page, "Failed");
                con.Close();
                amenddca();
                fillgrid();
                getCCBudget();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AmendDCABudget.aspx");
    }
    //protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlcctype.SelectedItem.Text == "Performing")
    //    {
    //        lblyear.Visible = false;
    //        ddlyear.Visible = false;
    //        Label9.Visible = false;
    //        tramendlistheader.Visible = false;
    //    }
    //    else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
    //    {
    //        lblyear.Visible = true;
    //        ddlyear.Visible = true;
    //        Label9.Visible = true;
    //        tramendlistheader.Visible = false;
    //    }

    //}

    public void LoadYear()
    {
        try
        {
            ddlyear.Items.Clear();
            da = new SqlDataAdapter("Select year from budget_cc where status='6' and cc_code='" + ddlCCcode.SelectedValue + "'", con);
            da.Fill(ds, "year");
            if (ds.Tables["year"].Rows.Count > 0)
            {
                btn.Visible = true;
            }
            ddlyear.DataTextField = "year";
            ddlyear.DataValueField = "year";
            ddlyear.DataSource = ds.Tables["year"];
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, "Select Year");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        
    }
    public void AmendDCAValue(decimal ccbalance)
    {
        try
        {
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                da = new SqlDataAdapter("select replace(isnull((SUM(isnull(credit,0))-SUM(isnull(debit,0))),0),'.0000','.00') Amendvalue from Amendbudget_DCA where CC_Code='" + ddlCCcode.SelectedValue + "' and Status not in ('4')", con);
            }
            else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
            {
                da = new SqlDataAdapter("select replace(isnull((SUM(isnull(credit,0))-SUM(isnull(debit,0))),0),'.0000','.00') Amendvalue from Amendbudget_DCA where CC_Code='" + ddlCCcode.SelectedValue + "' and Status not in ('4') and  year='" + ddlyear.SelectedItem.Text + "'", con);
            }
            da.Fill(ds, "amend");
            if (ds.Tables["amend"].Rows.Count > 0)
            {
                Label7.Text = Convert.ToDecimal(ccbalance - Convert.ToDecimal(ds.Tables["amend"].Rows[0][0].ToString())).ToString();
                h2.Value = Convert.ToDecimal(ccbalance - Convert.ToDecimal(ds.Tables["amend"].Rows[0][0].ToString())).ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddltype.SelectedIndex = 0;
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                trtype.Visible = true;
                tryear.Visible = false;
                GridView1.Visible = false;
                header.Visible = false;
                trcccode.Visible = false;
                tramendlistheader.Visible = false;
                GridView2.Visible = false;
            }
            else if (ddlcctype.SelectedItem.Text == "Non-Performing")
            {
                trtype.Visible = false;
                trcccode.Visible = true;
                tryear.Visible = false;
                btn.Visible = false;
                header.Visible = false;
                GridView1.Visible = false;
                tramendlistheader.Visible = false;
                GridView2.Visible = false;
                da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                da.Fill(ds, "CC");
                ddlCCcode.DataTextField = "Name";
                ddlCCcode.DataValueField = "cc_code";
                ddlCCcode.DataSource = ds.Tables["CC"];
                ddlCCcode.DataBind();
                ddlCCcode.Items.Insert(0, "Select Cost Center");
            }
            else if (ddlcctype.SelectedItem.Text == "Capital")
            {
                trtype.Visible = false;
                trcccode.Visible = true;
                tryear.Visible = false;
                btn.Visible = false;
                header.Visible = false;
                GridView1.Visible = false;
                tramendlistheader.Visible = false;
                GridView2.Visible = false;
                da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New') GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                da.Fill(ds, "CC");
                ddlCCcode.DataTextField = "Name";
                ddlCCcode.DataValueField = "cc_code";
                ddlCCcode.DataSource = ds.Tables["CC"];
                ddlCCcode.DataBind();
                ddlCCcode.Items.Insert(0, "Select Cost Center");
            }
            else if (ddlcctype.SelectedItem.Text == "Select")
            {
                trcccode.Visible = false;
                trtype.Visible = false;
                tryear.Visible = false;
                griddetails.Visible = false;
                btn.Visible = false;
                header.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlCCcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcctype.SelectedItem.Text == "Select")
            {
                tryear.Visible = false;
                btn.Visible = false;
                griddetails.Visible = false;
                header.Visible = false;


            }
            else if (ddlcctype.SelectedItem.Text == "Performing")
            {
                if (ddlCCcode.SelectedItem.Text == "Select Cost Center")
                {
                    btn.Visible = false;
                    griddetails.Visible = false;
                    header.Visible = false;
                    tryear.Visible = false;
                }
                else
                {
                    tryear.Visible = false;
                    btn.Visible = true;
                    griddetails.Visible = false;
                    header.Visible = false;
                }

            }
            else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
            {

                tryear.Visible = true;
                btn.Visible = false;
                griddetails.Visible = false;
                header.Visible = false;
                LoadYear();

            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
      
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddltype.SelectedItem.Text != "Select")
            {
                if (ddltype.SelectedItem.Text == "Service")

                    da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Service' GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Trading' GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Manufacturing' GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
                da.Fill(ds, "SERVICECC");
                ddlCCcode.DataTextField = "Name";
                ddlCCcode.DataValueField = "cc_code";
                ddlCCcode.DataSource = ds.Tables["SERVICECC"];
                ddlCCcode.DataBind();
                ddlCCcode.Items.Insert(0, "Select Cost Center");
                trcccode.Visible = true;
                tryear.Visible = false;
                btn.Visible = false;
                header.Visible = false;
                griddetails.Visible = false;
                //trccode.Visible = true;
                //year.Visible = false;
                //trbudget.Visible = true;
                //trbutton.Visible = true;
            }
            else
            {
                tryear.Visible = false;
                btn.Visible = false;
                header.Visible = false;
                griddetails.Visible = false;
                trcccode.Visible = false;
                //year.Visible = false;
                //trbudget.Visible = false;
                //trbutton.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }


    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        header.Visible = false;
        tramendlistheader.Visible = false;
        GridView2.Visible = false;

    }
}
