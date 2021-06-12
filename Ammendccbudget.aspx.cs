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


public partial class Ammendccbudget : System.Web.UI.Page
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
            trvisible.Visible = false;
            gridammendcc.Visible = false;
            trtype.Visible = false;
            btn.Visible = false;
            tbl.Visible = false;
        }
       
    }

    public void fillgrid()
    {
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            if (ddltype.SelectedItem.Text == "Service")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(d.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(d.balance,0)),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Performing' and c.CC_SubType='Service' and d.status='6' GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
            else if (ddltype.SelectedItem.Text == "Trading")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(d.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(d.balance,0)),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Performing' and c.CC_SubType='Trading' and d.status='6' GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
            else if (ddltype.SelectedItem.Text == "Manufacturing")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(d.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(d.balance,0)),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Performing' and c.CC_SubType='Manufacturing' and d.status='6' GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);

        }
        else if (ddlcctype.SelectedItem.Text == "Non-Performing")
            da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(isnull(d.budget_amount,0),'.0000','.00')as budget_amount,replace(isnull(d.balance,0),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Non-Performing' and d.status='6' and d.year='" + ddlyear.SelectedItem.Text + "' GROUP by c.cc_code,c.cc_name,d.budget_amount,d.balance ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);
        else
            da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(isnull(d.budget_amount,0),'.0000','.00')as budget_amount,replace(isnull(d.balance,0),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Capital' and d.status='6' and d.year='" + ddlyear.SelectedItem.Text + "' GROUP by c.cc_code,c.cc_name,d.budget_amount,d.balance ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.cc_code,'CC-','')) END ASC", con);

       da.Fill(ds, "gridfill");
       gridammendcc.DataSource = ds.Tables["gridfill"];
       gridammendcc.DataBind();
       gridammendcc.Visible = true;

    }

    protected void gridammendcc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridammendcc.EditIndex = -1;
        fillgrid();
    }
    protected void gridammendcc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);

        //string ccode = (gridammendcc.DataKeys[e.RowIndex].Value.ToString());
        //GridViewRow row = (GridViewRow)gridammendcc.Rows[e.RowIndex];
        //TextBox txtamount = (TextBox)row.FindControl("txtamount");
        //DropDownList type = (DropDownList)row.FindControl("ddltype");
        //Label balance = (Label)row.FindControl("lblbalance");

        //con.Open();
        //if ((Convert.ToDecimal(balance.Text) < Convert.ToDecimal(txtamount.Text)) && type.SelectedItem.Text == "Substract")
        //{
        //    JavaScript.UPAlert(Page, "Insufficient Balance");
        //}
        //else
        //{
        //    //if (ddlcctype.SelectedItem.Text == "Performing")
        //    //{
        //    //    if (type.SelectedItem.Text == "Add")
        //    //        cmd = new SqlCommand("insert into amendbudget_cc (cc_code,credit,date,createdby,status)values(@cc_code,@credit,@date,@createdby,@status)", con);
        //    //    else if (type.SelectedItem.Text == "Substract")
        //    //        cmd = new SqlCommand("insert into amendbudget_cc (cc_code,debit,date,createdby,status)values(@cc_code,@debit,@date,@createdby,@status)", con);

        //    //}
        //    //else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        //    //{
        //    //    if (type.SelectedItem.Text == "Add")
        //    //        cmd = new SqlCommand("insert into amendbudget_cc (cc_code,credit,date,createdby,year,status)values(@cc_code,@credit,@date,@createdby,@year,@status)", con);
        //    //    else if (type.SelectedItem.Text == "Substract")
        //    //        cmd = new SqlCommand("insert into amendbudget_cc (cc_code,debit,date,createdby,year,status)values(@cc_code,@debit,@date,@createdby,@year,@status)", con);

        //    //}
        //    //cmd.Parameters.Add("@cc_code", SqlDbType.VarChar).Value = ccode;
        //    //cmd.Parameters.Add("@credit", SqlDbType.Money).Value = txtamount.Text;
        //    //cmd.Parameters.Add("@debit", SqlDbType.Money).Value = txtamount.Text;
        //    //cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = myDate.ToString();
        //    //cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["user"].ToString();
        //    //cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;
        //    //cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "1";

        //    cmd = new SqlCommand("sp_Amend_CCBudget", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@CCcode", ccode);
        //    cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
        //    cmd.Parameters.AddWithValue("@type", type.SelectedItem.Text);
        //    cmd.Parameters.AddWithValue("@year", ddlyear.SelectedItem.Text);
        //    cmd.Parameters.AddWithValue("@Amendtype", "1");
        //    if (type.SelectedItem.Text == "Add")
        //    {
        //        cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
        //    }
        //    cmd.Parameters.AddWithValue("@createdby", Session["user"].ToString());
        //    string msg = cmd.ExecuteScalar().ToString();
        //    if (msg == "Sucessfully Inserted")
        //    {
        //        JavaScript.UPAlert(Page, msg);
        //    }
        //    else
        //    {
        //        JavaScript.UPAlertRedirect(Page, msg, "Ammendccbudget.aspx");
        //    }

        //    con.Close();
        //    gridammendcc.EditIndex = -1;
        //    fillgrid();
        //}
    }
       
    
    protected void gridammendcc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridammendcc.EditIndex = e.NewEditIndex;
        fillgrid();

    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
        {
            fillgrid();
            trvisible.Visible = true;
            gridammendcc.Visible = true;
        }
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            fillgrid();
            trvisible.Visible = false;
            gridammendcc.Visible = true;
          
        }
        
       
    }
    protected void gridammendcc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gridammendcc.EditIndex == e.Row.RowIndex)
            {
                RequiredFieldValidator reqfldVal = new RequiredFieldValidator();
                reqfldVal.ID = "RequiredValidator1";
                reqfldVal.ControlToValidate = "ddltype";
                reqfldVal.ErrorMessage = "Select Type";
                reqfldVal.InitialValue = "Select";
                reqfldVal.SetFocusOnError = true;
                e.Row.Cells[4].Controls.Add(reqfldVal);

                RequiredFieldValidator reqfldVal1 = new RequiredFieldValidator();
                reqfldVal1.ID = "RequiredValidator2";
                reqfldVal1.ControlToValidate = "txtamount";
                reqfldVal1.ErrorMessage = "Amount required";
                reqfldVal1.SetFocusOnError = true;
                e.Row.Cells[5].Controls.Add(reqfldVal1);
            }
        }

       
    }

    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        ddltype.SelectedIndex = 0;
        if ((ddlcctype.SelectedItem.Text == "Non-Performing")||(ddlcctype.SelectedItem.Text == "Capital"))
        {
            trvisible.Visible = true;
            trtype.Visible = false;
            btn.Visible = true;
            gridammendcc.Visible = false;
        }
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            trvisible.Visible = false;
            trtype.Visible = true;
            btn.Visible = false;
            gridammendcc.Visible = false;
           
        }
        if (ddlcctype.SelectedItem.Text == "Select")
        {
            trvisible.Visible = false;
            trtype.Visible = false;
            btn.Visible = false;
            gridammendcc.Visible = false;
           
        }
        
      
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ammendccbudget.aspx");
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedItem.Text == "Select")
        {
            btn.Visible = false;
            trvisible.Visible = false;
            btn.Visible = false;
            gridammendcc.Visible = false;
        }
        if (ddltype.SelectedItem.Text == "Service")
        {
            btn.Visible = false;
            trvisible.Visible = false;
            btn.Visible = true;
            gridammendcc.Visible = false;
        }
        if (ddltype.SelectedItem.Text == "Trading")
        {
            btn.Visible = false;
            trvisible.Visible = false;
            btn.Visible = true;
            gridammendcc.Visible = false;
        }
        if (ddltype.SelectedItem.Text == "Manufacturing")
        {

            btn.Visible = false;
            trvisible.Visible = false;
            btn.Visible = true;
            gridammendcc.Visible = false;
        }
    }
    protected void gridammendcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["cc_code"] = gridammendcc.SelectedValue.ToString();
        filltable(gridammendcc.SelectedValue.ToString());
    }

    public void filltable(string cccode)
     {
        try
        {
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                if (ddltype.SelectedItem.Text == "Service")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(d.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(d.balance,0)),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Performing'  and c.cc_code= '" + cccode + " '  and c.CC_SubType='Service' and d.status='6'  group by c.cc_code,c.cc_name", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(d.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(d.balance,0)),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Performing' and c.cc_code=  '" + cccode + "' and c.CC_SubType='Trading' and d.status='6' group by c.cc_code,c.cc_name", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(d.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(d.balance,0)),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Performing' and c.cc_code= '" + cccode + "'  and c.CC_SubType='Manufacturing' and d.status='6' group by c.cc_code,c.cc_name", con);

            }
            else if (ddlcctype.SelectedItem.Text == "Non-Performing")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(isnull(d.budget_amount,0),'.0000','.00')as budget_amount,replace(isnull(d.balance,0),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Non-Performing' and c.cc_code= '" + cccode + "' and d.status='6' and d.year='" + ddlyear.SelectedItem.Text + "' group by c.cc_code,c.cc_name,d.budget_amount,d.balance", con);
            else
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(isnull(d.budget_amount,0),'.0000','.00')as budget_amount,replace(isnull(d.balance,0),'.0000','.00')as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Capital' and c.cc_code= '" + cccode + "' and d.status='6' and d.year='" + ddlyear.SelectedItem.Text + "' group by c.cc_code,c.cc_name,d.budget_amount,d.balance", con);

            da.Fill(ds, "budgetinfo2");

            if (ds.Tables["budgetinfo2"].Rows.Count > 0)
            {

                tbl.Visible = true;

                lblcccode.Text = ds.Tables["budgetinfo2"].Rows[0][0].ToString();
                lblbudgetamount.Text = ds.Tables["budgetinfo2"].Rows[0][2].ToString();
                lblccname.Text = ds.Tables["budgetinfo2"].Rows[0][1].ToString();
                lbltbbalance.Text = ds.Tables["budgetinfo2"].Rows[0][3].ToString();
                lblverify1.Text = ds.Tables["budgetinfo2"].Rows[0][0].ToString();
           }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());

        }
    }
    protected void btnammendccbudget_Click1(object sender, EventArgs e)
    {
        try
        {
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);


            cmd = new SqlCommand("sp_Amend_CCBudget", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CCcode", lblcccode.Text);
            cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@type", ddlamendtype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@year", ddlyear.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Amendtype", "1");
            if (ddlamendtype.SelectedItem.Text == "Add")
            {
                cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
            }
            cmd.Parameters.AddWithValue("@createdby", Session["user"].ToString());
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
          
            if (msg == "Sucessfully Inserted")
            {
                JavaScript.UPAlert(Page, msg);
                ddlamendtype.SelectedIndex = 0;
                txtamount.Text= "";
                tbl.Visible = false;
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, msg, "Ammendccbudget.aspx");
             
            }

            con.Close();
            //gridammendcc.EditIndex = -1;
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
        Response.Redirect("Ammendccbudget.aspx");
    }
}
