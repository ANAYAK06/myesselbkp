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


public partial class CloseBudget : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
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
        cascadingDCA cs = new cascadingDCA();
        cs.Budgetpcc("undefined:" + ddlcctype.SelectedItem.Text + ";", "dd", ddlcctype.SelectedItem.Text);
        if (!IsPostBack)
        {
            btnsubmit.Visible = false;
            trtype.Visible = false;
            tryear.Visible = false;
            btn.Visible = false;
        }

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
    
        fillgrid();
       
        //ddlyear.Enabled = false;
        //CascadingDropDown1.Enabled = false;
        //btnsubmit.Visible = false;
       
    }
    private void fillgrid()
    {
        if (ddlcctype.SelectedItem.Text != "Select")
        {
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                //da = new SqlDataAdapter("select b.cc_code,c.cc_name,sum(isnull(b.budget_amount,0))budget_amount,sum(isnull((b.budget_amount-b.balance),0))Consumed_Budget,sum(isnull(b.balance,0))Balance from budget_cc b,cost_center c where b.cc_code=c.cc_code and b.status='6' and c.cc_type='"+ddlcctype.SelectedItem.Text+"'group by b.cc_code,c.cc_name", con);
                //if (ddltype.SelectedItem.Text == "Service")
                //{
                da = new SqlDataAdapter("select i.cc_code as cc_code,i.cc_name,replace((i.budget_amount+isnull(Prev,0)),'.0000','.00') as budget_amount,replace(i.balance,'.0000','.00') as balance,replace(((i.budget_amount+isnull(Prev,0))-i.balance),'.0000','.00')  as Consumed_Budget from (select c.cc_code ,c.cc_name as cc_name,sum(isnull(d.budget_amount,0))as budget_amount,sum(isnull(d.balance,0))as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='" + ddlcctype.SelectedItem.Text + "' and c.cc_subtype='" + ddltype.SelectedItem.Text + "'  and d.status='6' group by c.cc_code,c.cc_name)i left outer join (Select cc_code,isnull(sum(cash)+sum(cheque),0)[Prev] from [2009-10 Expences] group by cc_code) E on i.cc_code=E.cc_code", con);
                //}
                //else if (ddltype.SelectedItem.Text == "Trading")
                //{
                //    da = new SqlDataAdapter("select i.cc_code as cc_code,i.cc_name,(i.budget_amount+isnull(Prev,0)) as budget_amount,i.balance,((i.budget_amount+isnull(Prev,0))-i.balance)  as Consumed_Budget from (select c.cc_code ,c.cc_name as cc_name,sum(isnull(d.budget_amount,0))as budget_amount,sum(isnull(d.balance,0))as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and d.status='6' group by c.cc_code,c.cc_name)i left outer join (Select cc_code,isnull(sum(cash)+sum(cheque),0)[Prev] from [2009-10 Expences] group by cc_code) E on i.cc_code=E.cc_code", con);

                //}
                //else if (ddltype.SelectedItem.Text == "Manufacturing")
                //{
                //    da = new SqlDataAdapter("select i.cc_code as cc_code,i.cc_name,(i.budget_amount+isnull(Prev,0)) as budget_amount,i.balance,((i.budget_amount+isnull(Prev,0))-i.balance)  as Consumed_Budget from (select c.cc_code ,c.cc_name as cc_name,sum(isnull(d.budget_amount,0))as budget_amount,sum(isnull(d.balance,0))as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='" + ddlcctype.SelectedItem.Text + "'   and d.status='6' group by c.cc_code,c.cc_name)i left outer join (Select cc_code,isnull(sum(cash)+sum(cheque),0)[Prev] from [2009-10 Expences] group by cc_code) E on i.cc_code=E.cc_code", con);

                //}

            }

            else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
            {
                da = new SqlDataAdapter("select b.cc_code,c.cc_name,replace(sum(isnull(b.budget_amount,0)),'.0000','.00')as budget_amount,replace(sum(isnull((b.budget_amount-b.balance),0)),'.0000','.00')as Consumed_Budget,replace(sum(isnull(b.balance,0)),'.0000','.00')as Balance from budget_cc b,cost_center c where b.cc_code=c.cc_code and b.status='6' and c.cc_type='" + ddlcctype.SelectedItem.Text + "'and b.year='" + ddlyear.SelectedItem.Text + "' group by b.cc_code,c.cc_name", con);

            }
            da.Fill(ds, "dca_budget");
            if (ds.Tables["dca_budget"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["dca_budget"];
                GridView1.DataBind();
                btnsubmit.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnsubmit.Visible = false;

            }

        }
    }
    protected void btnsubmit_Click1(object sender, EventArgs e)
    {
        foreach (GridViewRow record in GridView1.Rows)
        {
            CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
            if (c1.Checked)
            {
                cmd = new SqlCommand("closebudget", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CCCode", record.Cells[1].Text);
                cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
                if (ddlcctype.SelectedItem.Text != "Performing")
                {
                    cmd.Parameters.AddWithValue("@year", ddlyear.SelectedItem.Text);
                }

            }
        }
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        JavaScript.UPAlert(Page, msg);
        con.Close();
        fillgrid();
    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        btnsubmit.Visible = false;
        trtype.Visible = false;
        ddltype.SelectedIndex = 0;
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            tryear.Visible = false;
            trtype.Visible = true;
        }
        else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
        {
            //CadscaingDropDown1.Enabled = true;
            ddlyear.Enabled = true;
            tryear.Visible = true;
            trtype.Visible = false;
            LoadYear();
        }
        else
        {
            GridView1.Visible = false;
            btnsubmit.Visible = false;
            //CascadingDropDown1.Enabled = false;
            tryear.Visible = false;
            trtype.Visible = false;
            btn.Visible = false;
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("closebudget.aspx");
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        btn.Visible = false;
        btnsubmit.Visible = false;
        if (ddltype.SelectedItem.Text != "Select")
        {
            btn.Visible = true;
        }
        else
        {
          
            GridView1.Visible = false;
            btn.Visible = false;
            btnsubmit.Visible = false;
        }
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn.Visible = true;
        GridView1.Visible = false;
        btnsubmit.Visible = false;
    }
    public void LoadYear()
    {
        ddlyear.Items.Clear();
        if (Session["roles"].ToString() == "SuperAdmin")
        da = new SqlDataAdapter("select distinct year from budget_cc b join cost_center c on c.cc_code=b.cc_code where cc_type='"+ddlcctype.SelectedItem.Text+"' ", con);
        da.Fill(ds, "year");
        ddlyear.DataTextField = "year";
        ddlyear.DataValueField = "year";
        ddlyear.DataSource = ds.Tables["year"];
        ddlyear.DataBind();
        ddlyear.Items.Insert(0, "Select Year");
    }
}
