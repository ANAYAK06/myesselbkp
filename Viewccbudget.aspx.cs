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


public partial class Viewccbudget : System.Web.UI.Page
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
            if (Session["roles"].ToString() == "Project Manager")
            { 
             cctypevisible.Visible=false;
             trvisible.Visible = false;
            }
            ////trvisible.Visible = false;
            gridviewccbudget.Visible = false;
            ////trtype.Visible = false;
            ////btn.Visible = false;
            tbl.Visible = false;
        }

    }

    public void fillgrid()
    {

        if (Session["roles"].ToString() == "Project Manager")
        {

            if (ddltype.SelectedItem.Text == "Service")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and b.status not in ('1A') and cc_subtype='Service'  group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
            else if (ddltype.SelectedItem.Text == "Trading")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1A') and c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and cc_subtype='Trading'  group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
            else if (ddltype.SelectedItem.Text == "Manufacturing")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1A') and c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and cc_subtype='Manufacturing'  group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);

        }

        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            if (Session["roles"].ToString() == "HoAdmin")
            {
                if (ddltype.SelectedItem.Text == "Service")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','2','2A') and cc_subtype='Service'  group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','2','2A') and cc_subtype='Trading'  group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','2','2A') and cc_subtype='Manufacturing'  group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
            }

            else if (Session["roles"].ToString() == "SuperAdmin")
            {
                if (ddltype.SelectedItem.Text == "Service")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','1A','2A') and cc_subtype='Service' group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','1A','2A') and cc_subtype='Trading' group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','1A','2A') and cc_subtype='Manufacturing' group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
            }
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                if (ddltype.SelectedItem.Text == "Service")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','1A','2') and cc_subtype='Service' group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
                else if (ddltype.SelectedItem.Text == "Trading")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','1A','2') and cc_subtype='Trading' group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
                else if (ddltype.SelectedItem.Text == "Manufacturing")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status not in ('1','1A','2') and cc_subtype='Manufacturing' group by c.cc_code,c.cc_name,b.[Status],c.CC_type,c.status order by b.Status desc", con);
            }
        }
        else if (ddlcctype.SelectedItem.Text == "Non-Performing")
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.year,b.status,c.cc_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type ='Non-Performing' and b.year='" + ddlyear.SelectedItem.Text + "' and b.status  not in ('2','2A') group by c.cc_code,c.cc_name,b.budget_amount,b.balance,b.year,b.status,c.cc_type,c.status", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.year,b.status,c.cc_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type ='Non-Performing' and b.year='" + ddlyear.SelectedItem.Text + "' and b.status not in ('1','2A') group by c.cc_code,c.cc_name,b.budget_amount,b.balance,b.year,b.status,c.cc_type,c.status", con);
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.year,b.status,c.cc_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type ='Non-Performing' and b.year='" + ddlyear.SelectedItem.Text + "' and b.status not in ('1','2') group by c.cc_code,c.cc_name,b.budget_amount,b.balance,b.year,b.status,c.cc_type,c.status", con);

        }
        else if (ddlcctype.SelectedItem.Text == "Capital")
        {
            if (Session["roles"].ToString() == "HoAdmin")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.year,b.status,c.cc_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type ='Capital' and b.year='" + ddlyear.SelectedItem.Text + "' and b.status  not in ('2','2A') group by c.cc_code,c.cc_name,b.budget_amount,b.balance,b.year,b.status,c.cc_type,c.status", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.year,b.status,c.cc_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type ='Capital' and b.year='" + ddlyear.SelectedItem.Text + "' and b.status not in ('1','2A') group by c.cc_code,c.cc_name,b.budget_amount,b.balance,b.year,b.status,c.cc_type,c.status", con);
            else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull(b.balance,0)),'.0000','.00') as Balance,b.year,b.status,c.cc_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type ='Capital' and b.year='" + ddlyear.SelectedItem.Text + "' and b.status not in ('1','2') group by c.cc_code,c.cc_name,b.budget_amount,b.balance,b.year,b.status,c.cc_type,c.status", con);

        }
        da.Fill(ds, "gridview");
        if (ds.Tables["gridview"].Rows.Count > 0)
        {
            gridviewccbudget.DataSource = ds.Tables["gridview"];
            gridviewccbudget.DataBind();
        }
        else
        {
            gridviewccbudget.DataSource = null;
            gridviewccbudget.DataBind();
        }

    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        {
            trvisible.Visible = true;
        }
        else if (ddlcctype.SelectedItem.Text == "Performing")
        {
            trvisible.Visible = false;
        }
        

        gridviewccbudget.Visible = true;
        fillgrid();
    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridviewccbudget.Visible = false;
        ddltype.SelectedIndex = 0;
        if (ddlcctype.SelectedItem.Text != "Performing")
        {
            CascadingDropDown1.SelectedValue = "";
            btn.Visible = true;
            trvisible.Visible = true;
            trtype.Visible = false;

        }
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            
             trvisible.Visible = false;
             trtype.Visible = true;
            ///btn.Visible = false;
        }
        if (ddlcctype.SelectedItem.Text == "Select")
        {
            ////trvisible.Visible = false;
            ////trtype.Visible = false;
            ////btn.Visible = false;
        }
    }
    protected void gridviewccbudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            if (e.Row.Cells[5].Text == "1" || e.Row.Cells[5].Text == "1A" || e.Row.Cells[5].Text == "2A")
            {
                e.Row.Cells[5].Text = "Pending for verfication";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            }
            else if (e.Row.Cells[5].Text == "2" || e.Row.Cells[5].Text == "2A")
            {
                e.Row.Cells[5].Text = "Pending for Approval";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            }
            else if ((e.Row.Cells[5].Text == "6") || (e.Row.Cells[5].Text == "3") || (e.Row.Cells[5].Text == "4") || (e.Row.Cells[5].Text == "5"))
            {
                e.Row.Cells[5].Text = "Approved Budget";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
            }
            else if (e.Row.Cells[5].Text == "Pending for verfication")
            {
                e.Row.Cells[5].Text = "Pending for verfication";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            }
            else if (e.Row.Cells[5].Text == "Pending for Approval")
            {
                e.Row.Cells[5].Text = "Pending for Approval";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gridviewccbudget.EditIndex == e.Row.RowIndex)
            {
                RequiredFieldValidator reqfldVal1 = new RequiredFieldValidator();
                reqfldVal1.ID = "RequiredValidator2";
                reqfldVal1.ControlToValidate = "txtbal";
                reqfldVal1.ErrorMessage = "Amount required";
                reqfldVal1.SetFocusOnError = true;
                e.Row.Cells[4].Controls.Add(reqfldVal1);

            }
        }

    }
    protected void gridviewccbudget_RowEditing(object sender, GridViewEditEventArgs e)
    {

        GridViewRow gvr = (GridViewRow)gridviewccbudget.Rows[e.NewEditIndex];
        string s = gvr.Cells[5].Text;
        if (s == "Pending for verfication" || s == "Pending for Approval")
        {
            gridviewccbudget.EditIndex = e.NewEditIndex;
            fillgrid();
        }
    }
    protected void gridviewccbudget_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string ccode= (gridviewccbudget.DataKeys[e.RowIndex].Value.ToString());
        //TextBox bal = gridviewccbudget.Rows[e.RowIndex].FindControl("txtbal") as TextBox;
        //gridviewccbudget.EditIndex = -1;
        //con.Open();
        //if (Session["roles"].ToString() == "HoAdmin")
        //{
        //    if (ddlcctype.SelectedItem.Text == "Performing")
        //        cmd = new SqlCommand("update budget_cc set budget_amount='" + bal.Text + "', balance='" + bal.Text + "',status='2' where cc_code='" + ccode + "'", con);
        //    else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        //        cmd = new SqlCommand("update budget_cc set budget_amount='" + bal.Text + "', balance='" + bal.Text + "',status='2' where cc_code='" + ccode + "' and year='" + ddlyear.SelectedItem.Text + "'", con);

        //}
        //else
        //{
        //    if (ddlcctype.SelectedItem.Text == "Performing")
        //        cmd = new SqlCommand("update budget_cc set budget_amount='" + bal.Text + "', balance='" + bal.Text + "',status='3' where cc_code='" + ccode + "'", con);
        //    else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        //        cmd = new SqlCommand("update budget_cc set budget_amount='" + bal.Text + "', balance='" + bal.Text + "',status='3' where cc_code='" + ccode + "' and year='" + ddlyear.SelectedItem.Text + "'", con);

        //}

        //if (cmd.ExecuteNonQuery() == 1)
        //{
        //    JavaScript.UPAlert(Page, "Successfully");
        //}
        //else
        //{
        //    JavaScript.UPAlert(Page, "Failed");
        //}
        //con.Close();
        //fillgrid();
    }
    protected void gridviewccbudget_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridviewccbudget.EditIndex = -1;
        fillgrid();
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("Viewccbudget.aspx");
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedItem.Text == "Select")
        {
            btn.Visible = false;
            gridviewccbudget.Visible = false;
        }
        else if (ddltype.SelectedItem.Text != "Select")
        {
            btn.Visible = true;
            gridviewccbudget.Visible = false;
        }
    }
    protected void gridviewccbudget_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["cc_code"] = gridviewccbudget.SelectedValue.ToString();
        filltable(gridviewccbudget.SelectedValue.ToString());
    }

    public void filltable(string cccode)
    {
        try
        {
             if (Session["roles"].ToString() == "Project Manager")
                  da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and c.CC_type='Performing' and b.status  in ('1') and  b.cc_code='" + cccode + "'", con);
             if (ddlcctype.SelectedItem.Text == "Performing")
             {
                if (Session["roles"].ToString() == "HoAdmin")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status  in ('1A') and b.cc_code='" + cccode + "'", con);
                else if (Session["roles"].ToString() == "SuperAdmin")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status  in ('2') and b.cc_code='" + cccode + "'", con);
                else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Performing' and b.status  in ('2A') and b.cc_code='" + cccode + "'", con);

            }
            else if (ddlcctype.SelectedItem.Text == "Non-Performing")
            {
                if (Session["roles"].ToString() == "HoAdmin")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Non-Performing' and b.status  in ('1') and b.cc_code='" + cccode + "' and b.year='" + ddlyear.SelectedItem.Text + "'", con);
                else if (Session["roles"].ToString() == "SuperAdmin")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Non-Performing' and b.status  in ('2') and b.cc_code='" + cccode + "' and b.year='" + ddlyear.SelectedItem.Text + "'", con);
                else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Non-Performing' and b.status  in ('2A') and b.cc_code='" + cccode + "' and b.year='" + ddlyear.SelectedItem.Text + "'", con);

            }
            else if(ddlcctype.SelectedItem.Text == "Capital")
            {
                if (Session["roles"].ToString() == "HoAdmin")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Capital' and b.status  in ('1') and b.cc_code='" + cccode + "' and b.year='" + ddlyear.SelectedItem.Text + "'", con);
                else if (Session["roles"].ToString() == "SuperAdmin")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Capital' and b.status  in ('2') and b.cc_code='" + cccode + "' and b.year='" + ddlyear.SelectedItem.Text + "'", con);
                else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace((isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace((isnull(b.balance,0)),'.0000','.00') as Balance,b.[Status],c.CC_type,c.status as cstatus from Cost_Center c join budget_cc b on c.cc_code=b.cc_code where c.CC_type='Capital' and b.status  in ('2A') and b.cc_code='" + cccode + "' and b.year='" + ddlyear.SelectedItem.Text + "'", con);

            }
            da.Fill(ds, "budgetinfo");
            if (ds.Tables["budgetinfo"].Rows.Count > 0)
            {

                tbl.Visible = true;
                if (Session["roles"].ToString() == "Project Manager")
                {
                    txtbudgetamt.Enabled = false;
                    txtbudgetamt.Text = ds.Tables["budgetinfo"].Rows[0][2].ToString();
                }
                else
                {
                    txtbudgetamt.Text = ds.Tables["budgetinfo"].Rows[0][2].ToString();
                }
                lblcccode.Text = ds.Tables["budgetinfo"].Rows[0][0].ToString();
                lblccname.Text = ds.Tables["budgetinfo"].Rows[0][1].ToString();
                lblbalance.Text = ds.Tables["budgetinfo"].Rows[0][3].ToString();
                lblverify.Text = ds.Tables["budgetinfo"].Rows[0][0].ToString();

            }
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());

        }
    }
    protected void btnverifyccbudget_Click(object sender, EventArgs e)
    {
        //string ccode = (gridviewccbudget.DataKeys[e.RowIndex].Value.ToString());
        //TextBox bal = gridviewccbudget.Rows[e.RowIndex].FindControl("txtbal") as TextBox;
        //gridviewccbudget.EditIndex = -1;
        string ccode = "";
        con.Open();
        if (Session["roles"].ToString() == "Project Manager")
        {
            //foreach (GridViewRow row1 in gridviewccbudget.Rows)
            //{
            //    if (lblverify.Text == gridviewccbudget.DataKeys[row1.RowIndex].Value.ToString())
            //    {
            //        ccode = gridviewccbudget.DataKeys[row1.RowIndex].Value.ToString();
            //    }
            //}
            ccode = lblcccode.Text;
            //if (ddlcctype.SelectedItem.Text == "Performing")
                cmd = new SqlCommand("update budget_cc set status='1A' where cc_code='" + ccode + "'", con);
            //else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
            //    cmd = new SqlCommand("update budget_cc set status='1A' where cc_code='" + ccode + "' and year='" + ddlyear.SelectedItem.Text + "'", con);
        }

        else if (Session["roles"].ToString() == "HoAdmin")
        {
            //foreach (GridViewRow row1 in gridviewccbudget.Rows)
            //{
            //    if (lblverify.Text == gridviewccbudget.DataKeys[row1.RowIndex].Value.ToString())
            //    {
            //        ccode = gridviewccbudget.DataKeys[row1.RowIndex].Value.ToString();
            //    }
            //}
            ccode = lblcccode.Text;
            if (ddlcctype.SelectedItem.Text == "Performing")
                cmd = new SqlCommand("update budget_cc set budget_amount='" + txtbudgetamt.Text + "', balance='" + txtbudgetamt.Text + "',status='2' where cc_code='" + ccode + "'", con);
            else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                cmd = new SqlCommand("update budget_cc set budget_amount='" + txtbudgetamt.Text + "', balance='" + txtbudgetamt.Text + "',status='2' where cc_code='" + ccode + "' and year='" + ddlyear.SelectedItem.Text + "'", con);

        }
        else if (Session["roles"].ToString() == "SuperAdmin")
        {
            //foreach (GridViewRow row1 in gridviewccbudget.Rows)
            //{
            //    if (lblverify.Text == gridviewccbudget.DataKeys[row1.RowIndex].Value.ToString())
            //    {
            //        ccode = gridviewccbudget.DataKeys[row1.RowIndex].Value.ToString();
            //    }
            //}
            ccode = lblcccode.Text;
            if (ddlcctype.SelectedItem.Text == "Performing")
                cmd = new SqlCommand("update budget_cc set budget_amount='" + txtbudgetamt.Text + "', balance='" + txtbudgetamt.Text + "',status='2A' where cc_code='" + ccode + "'", con);
            else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                cmd = new SqlCommand("update budget_cc set budget_amount='" + txtbudgetamt.Text + "', balance='" + txtbudgetamt.Text + "',status='2A' where cc_code='" + ccode + "' and year='" + ddlyear.SelectedItem.Text + "'", con);

        }
        else
        {
            //foreach (GridViewRow row1 in gridviewccbudget.Rows)
            //{
            //    if (lblverify.Text == gridviewccbudget.DataKeys[row1.RowIndex].Value.ToString())
            //    {
            //        ccode = gridviewccbudget.DataKeys[row1.RowIndex].Value.ToString();
            //    }
            //}
            ccode = lblcccode.Text;
            if (ddlcctype.SelectedItem.Text == "Performing")
                cmd = new SqlCommand("update budget_cc set budget_amount='" + txtbudgetamt.Text + "', balance='" + txtbudgetamt.Text + "',status='3' where cc_code='" + ccode + "'", con);
            else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                cmd = new SqlCommand("update budget_cc set budget_amount='" + txtbudgetamt.Text + "', balance='" + txtbudgetamt.Text + "',status='3' where cc_code='" + ccode + "' and year='" + ddlyear.SelectedItem.Text + "'", con);

        }

        if (cmd.ExecuteNonQuery() == 1)
        {
            JavaScript.UPAlert(Page, "Successfully");
            
            tbl.Visible = false;
        }
        else
        {
            JavaScript.UPAlert(Page, "Failed");
        }
        con.Close();
        fillgrid();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Viewccbudget.aspx");

    }
}
