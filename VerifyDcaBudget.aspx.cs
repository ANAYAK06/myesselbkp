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


public partial class VerifyDcaBudget : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            hf1.Value = Session["roles"].ToString();
            if (Session["roles"].ToString() == "Project Manager")
            {
                tr.Visible = false;
                year.Visible = false;
               
            }
            btnsubmit.Visible = false;
            header.Visible = false;
           
        }
        cascadingDCA cs = new cascadingDCA();
        cs.Budgetpcc("undefined:" + ddlcctype.SelectedItem.Text + ";", "dd", ddlcctype.SelectedItem.Text);

    }

    private void fillgrid()
    {
        try
        {
                if (Session["roles"].ToString() == "Project Manager")
                    cmd1 = new SqlCommand("Select distinct i.*,dt.status from(select y.dcayearly_id,d.dca_code ,d.dca_name ,CAST(y.budget_dca_yearly AS Decimal(20,2))as budget_dca_yearly,d.mapdca_code  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and y.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and y.status='1')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);
                else if (Session["roles"].ToString() == "HoAdmin")
                  {
                    if (ddlcctype.SelectedItem.Text == "Performing")
                      {
                        cmd1 = new SqlCommand("Select distinct i.*,dt.status from(select y.dcayearly_id,d.dca_code ,d.dca_name ,CAST(y.budget_dca_yearly AS Decimal(20,2))as budget_dca_yearly,d.mapdca_code  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and y.status='1A')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);
                      }
                    else
                     {
                        cmd1 = new SqlCommand("Select distinct i.*,dt.status from(select y.dcayearly_id,d.dca_code ,d.dca_name ,CAST(y.budget_dca_yearly AS Decimal(20,2))as budget_dca_yearly,d.mapdca_code from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and y.year='" + ddlyear.SelectedItem.Text + "' and y.status='1')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);
                     }
                  }
                else 
                  {
                   if (ddlcctype.SelectedItem.Text == "Performing")
                     {      
                        cmd1 = new SqlCommand("Select distinct i.*,dt.status from(select y.dcayearly_id,d.dca_code ,d.dca_name ,CAST(y.budget_dca_yearly AS Decimal(20,2))as budget_dca_yearly,d.mapdca_code  from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and y.status='2')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);
                     }
                   else
                     {
                        cmd1 = new SqlCommand("Select distinct i.*,dt.status from(select y.dcayearly_id,d.dca_code ,d.dca_name ,CAST(y.budget_dca_yearly AS Decimal(20,2))as budget_dca_yearly,d.mapdca_code from  yearly_dcabudget y join dca d on y.dca_code=d.dca_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and y.year='" + ddlyear.SelectedItem.Text + "' and y.status='2')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);
                     }
                  }
           

            da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                header.Visible = false;
                trbtnsubmit.Visible = false;
                GridView1.EmptyDataText = "There is no Data";
                this.GridView1.Rows[0].Visible = false;
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
            header.Visible = true;
            // btngridsubmit.Visible = true;
            if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
            {

                if (ddlyear.SelectedValue != "" && ddlCCcode.SelectedValue != "")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,CAST(d.budget_amount AS Decimal(20,2)) as budget_amount,CAST(d.balance AS Decimal(20,2)) as balance ,d.year,CAST((isnull(d.budget_amount,0)-isnull(d.balance,0)) AS Decimal(20,2)) as[Consumed] from Cost_Center c join budget_cc d on c.cc_code=d.cc_code where c.cc_code='" + ddlCCcode.SelectedValue + "' and d.year='" + ddlyear.SelectedItem.Text + "'", con);
            }
            else
            { 
                if(Session["roles"].ToString()=="Project Manager")
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,CAST(d.budget_amount AS decimal(20,2)) as budget_amount,CAST(d.balance AS Decimal(20,2)) as balance ,d.year,CAST((isnull(d.budget_amount,0)-isnull(d.balance,0)) AS Decimal(20,2)) as[Consumed] from Cost_Center c join budget_cc d on c.cc_code=d.cc_code where c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and c.cc_code='" + ddlCCcode.SelectedValue + "'", con);
                else
                    da = new SqlDataAdapter("select c.cc_code,c.cc_name,CAST(d.budget_amount AS Decimal(20,2)) as budget_amount,CAST(d.balance AS Decimal(20,2)) as balance ,d.year,CAST((isnull(d.budget_amount,0)-isnull(d.balance,0)) AS Decimal(20,2)) as[Consumed] from Cost_Center c join budget_cc d on c.cc_code=d.cc_code where c.cc_code='" + ddlCCcode.SelectedValue + "'", con);

            }
            da.Fill(ds, "normalview");
            if (ds.Tables["normalview"].Rows.Count > 0)
            {
                Label1.Text = ds.Tables["normalview"].Rows[0][0].ToString();
                Label5.Text = ds.Tables["normalview"].Rows[0][1].ToString();
                Label6.Text = ds.Tables["normalview"].Rows[0][2].ToString().Replace(".0000",".00");
                Label7.Text = ds.Tables["normalview"].Rows[0][3].ToString().Replace(".0000", ".00");
                Label8.Text = ds.Tables["normalview"].Rows[0][4].ToString();
                Label9.Text = ds.Tables["normalview"].Rows[0][5].ToString().Replace(".0000", ".00");
                h1.Value = ds.Tables["normalview"].Rows[0][2].ToString();
                h2.Value = ds.Tables["normalview"].Rows[0][3].ToString();
                if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                {
                    Label8.Visible = true;
                    Label10.Visible = true;
                }
                else
                {
                    Label8.Visible = false;
                    Label10.Visible = false;
                }
                    
            }
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;

        fillgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;

        fillgrid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox Amount = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtbudget");
            cmd = new SqlCommand("sp_Verfiy_DcaBudget", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["dcayearly_id"].ToString());
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@type", "Verify");
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            JavaScript.UPAlert(Page, msg);
            con.Close();
            fillgrid();

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
            string ids = "";
            string Amounts = "";
            foreach (GridViewRow record in GridView1.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    ids = ids + GridView1.DataKeys[record.RowIndex]["dcayearly_id"].ToString() + ",";
                    //Amounts = Amounts + (record.FindControl("txtbudget") as TextBox).Text + ",";
                    Amounts = Amounts + (record.FindControl("lblbankname") as Label).Text + ",";
                   // Amounts = Amounts + record.Cells[4].Text + ",";
                }


            }
            cmd = new SqlCommand("sp_Verfiy_DcaBudget", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ids", ids);
            cmd.Parameters.AddWithValue("@Amounts", Amounts);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
            if (Session["roles"].ToString() == "Project Manager")
            {
                cmd.Parameters.AddWithValue("@CCType", "Performing");
            }
            else
            {
                cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
            }
            cmd.Parameters.AddWithValue("@type", "Verify");
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            //string msg = "ss";
            con.Close();
            JavaScript.UPAlert(Page, msg);
            getCCBudget();
            fillgrid();
            type();
            cctype();
            LoadYear();
            header.Visible = false;
            grid.Visible = false;
            trbtnsubmit.Visible = false;
            ddlCCcode.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        
        getCCBudget();
        fillgrid();
        //ddlcctype.Enabled = false;
        //CascadingDropDown1.Enabled = false;
        //CascadingDropDown2.Enabled = false;
        btnsubmit.Visible = true;
        header.Visible = true;
        grid.Visible = true;
        if (Session["roles"].ToString() == "Project Manager")
        {
            GridView1.FooterRow.Visible = false;
        }
        trbtnsubmit.Visible = true;
        ////if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        ////{
        ////    lblyear.Visible = true;
        ////    ddlyear.Visible = true;
        ////}
        ////else
        ////{
        ////    lblyear.Visible = false;
        ////    ddlyear.Visible = false;
        ////}
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            SqlDataAdapter fda = new SqlDataAdapter();
            DataSet fds = new DataSet();
            if (Session["roles"].ToString() != "Project Manager")
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                  
                    DropDownList ddldca = (DropDownList)e.Row.FindControl("ddldetailhead");
                    
                    if (ddlcctype.SelectedItem.Text == "Non-Performing")
                        fda = new SqlDataAdapter("Select distinct d.dca_code,dc.mapdca_code from dcatype d left  join yearly_dcabudget y on d.dca_code=y.dca_code join dca dc on dc.dca_code=d.dca_code where  d.dca_code not in (select dca_code from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "') and cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "') and d.status='Active'", con);
                    else if (ddlcctype.SelectedItem.Text == "Capital")
                        fda = new SqlDataAdapter("Select distinct d.dca_code,dc.mapdca_code from dcatype d left  join yearly_dcabudget y on d.dca_code=y.dca_code join dca dc on dc.dca_code=d.dca_code where  d.dca_code not in (select dca_code from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "' and year='" + ddlyear.SelectedItem.Text + "') and cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "') and d.status='Active'", con);

                    else if (ddlcctype.SelectedItem.Text == "Performing")
                        fda = new SqlDataAdapter("Select distinct d.dca_code,dc.mapdca_code from dcatype d left  join yearly_dcabudget y on d.dca_code=y.dca_code join dca dc on dc.dca_code=d.dca_code where  d.dca_code not in (select dca_code from yearly_dcabudget where cc_code='" + ddlCCcode.SelectedValue + "') and  cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "') and d.status='Active'", con);
                    fda.Fill(fds, "DCACodes");
                    ddldca.DataTextField = "mapdca_code";
                    ddldca.DataValueField = "dca_code";
                    ddldca.DataSource = fds.Tables["DCACodes"];
                    ddldca.DataBind();
                    ddldca.Items.Insert(0, "Select DCA");

                }
            }
            if (Session["roles"].ToString() == "Project Manager")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //LinkButton lnkBtn = (LinkButton)e.Row.Cells[5].Controls[0]; //here use the cell no in which your edit command button is there.
                    //lnkBtn.Enabled = false;//write a logic to disable or enable according to privilages.
                    //LinkButton lnkBtn1 = (LinkButton)e.Row.Cells[4].Controls[0];
                    //lnkBtn1.Enabled = false;
                    //LinkButton lnkbtn2 = (LinkButton)e.Row.Cells[3].Controls[0];
                    //lnkbtn2.Enabled = false;
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[4].Enabled = false;
                    e.Row.Cells[3].Enabled = false;
                    //GridView1.Columns[4].Visible = false;
                }
            }
            if (Session["roles"].ToString() != "Sr.Accountant")
            {

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }
            }
           
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }


    }
    protected void btnbud_Click(object sender, EventArgs e)
    {

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ADD")
            {
                string dcacode = ((DropDownList)GridView1.FooterRow.FindControl("ddldetailhead")).SelectedValue + ",";
                string budamount = ((TextBox)GridView1.FooterRow.FindControl("txtbud")).Text + ",";
                string dcaname = ((Label)GridView1.FooterRow.FindControl("Label5")).Text;
                Amount = Amount + Convert.ToDecimal((GridView1.FooterRow.FindControl("txtbud") as TextBox).Text);

                cmd = new SqlCommand("sp_Assign_DcaBudget", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CCCode", ddlCCcode.SelectedValue);
                cmd.Parameters.AddWithValue("@DCACodes", dcacode);
                if (ddlcctype.SelectedItem.Text != "Performing")
                {
                    cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedItem.Text);
                }
                cmd.Parameters.AddWithValue("@Amounts", budamount);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@DCAAmount", Amount);
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                if (Session["roles"].ToString() == "HoAdmin")
                {
                    cmd.Parameters.AddWithValue("@status", "1A");

                }
                else
                {
                    cmd.Parameters.AddWithValue("@status", "2");


                }
                cmd.Connection = con;
                con.Open();

                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "DCA Budget Assigned Successfully")
                    JavaScript.UPAlert(Page, msg);
                else
                    JavaScript.UPAlert(Page, msg);
                getCCBudget();
                fillgrid();


            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    private decimal Amount = (decimal)0.0;
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //string Amount = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtbudget")).Text;
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            HiddenField Amt = (HiddenField)row.FindControl("h3");
            cmd = new SqlCommand("sp_Verfiy_DcaBudget", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ids", GridView1.DataKeys[e.RowIndex]["dcayearly_id"].ToString() + ",");
            cmd.Parameters.AddWithValue("@Amounts", (Amt.Value).ToString() + ",");
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Roles", Session["roles"].ToString());
            cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@type", "delete");
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            JavaScript.UPAlert(Page, msg);
            con.Close();
            getCCBudget();
            fillgrid();
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Verifydcabudget.aspx");
    }
    protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)GridView1.Rows[e.RowIndex];
        string dcayearlyid = GridView1.DataKeys[e.RowIndex]["dcayearly_id"].ToString();
        TextBox newamt = (TextBox)gvr.FindControl("txtbudget");
        cmd = new SqlCommand("sp_budgetchk", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@yearlyid", dcayearlyid);
        cmd.Parameters.AddWithValue("@newamt", newamt.Text);
        cmd.Parameters.AddWithValue("@cccode", ddlCCcode.SelectedValue);
        cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
        cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        if (msg == "Updated")
        {
            JavaScript.UPAlert(Page, msg);
        }
        else
        {
            JavaScript.UPAlert(Page, msg);
        }
        
        con.Close();
        getCCBudget();
        GridView1.EditIndex = -1;
        fillgrid();

    }
    protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgrid();

    }
    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        trccode.Visible = true;
        type();
     
    }

    public void type()
    {
        //if (ddlcctype.SelectedItem.Text == "Performing")
        //{
        //    if (ddltype.SelectedItem.Text != "Select")
        //    {
                if (Session["roles"].ToString() == "Project Manager")
                {
                    if (ddltype.SelectedItem.Text != "Select")
                    {
                        if (ddltype.SelectedItem.Text == "Service")
                        {
                            da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4' and c.CC_type='Performing' and c.status in ('Old','New')and c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and cc_subtype='Service' ", con);
                        }
                        else if (ddltype.SelectedItem.Text == "Trading")
                        {
                            da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Trading' ", con);
                        }
                        else if (ddltype.SelectedItem.Text == "Manufacturing")
                        {
                            da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Manufacturing' ", con);
                        }

                         da.Fill(ds, "SERVICECC");
                         ddlCCcode.DataTextField = "Name";
                         ddlCCcode.DataValueField = "cc_code";
                         ddlCCcode.DataSource = ds.Tables["SERVICECC"];
                         ddlCCcode.DataBind();
                         ddlCCcode.Items.Insert(0, "Select Cost Center");
                         trbtnsubmit.Visible = false;
                         header.Visible = false;
                         grid.Visible = false;
                    }
                }
         if (ddlcctype.SelectedItem.Text == "Performing")
          {
            if (ddltype.SelectedItem.Text != "Select")
              {
                if (Session["roles"].ToString() == "HoAdmin")
                {
                    if (ddltype.SelectedItem.Text == "Service")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4A' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Service' ", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Trading")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4A' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Trading' ", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Manufacturing")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4A' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Manufacturing' ", con);
                    }

                }
                else if (Session["roles"].ToString() == "SuperAdmin")
                {
                    if (ddltype.SelectedItem.Text == "Service")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='5' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Service'", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Trading")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='5' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Trading'", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Manufacturing")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='5' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Manufacturing'", con);
                    }

                }
                da.Fill(ds, "SERVICECC");
                ddlCCcode.DataTextField = "Name";
                ddlCCcode.DataValueField = "cc_code";
                ddlCCcode.DataSource = ds.Tables["SERVICECC"];
                ddlCCcode.DataBind();
                ddlCCcode.Items.Insert(0, "Select Cost Center");
                ////trccode.Visible = true;
                ////year.Visible = false;
                ////btn.Visible = false;
                trbtnsubmit.Visible = false;
                header.Visible = false;
                grid.Visible = false;
            }
            else
            {
                ////trtype.Visible = true;
                ////trccode.Visible = false;
                ////year.Visible = false;
                ////btn.Visible = false;
                ////header.Visible = false;
                ////grid.Visible=false;
                trbtnsubmit.Visible = false;
                header.Visible = false;
                grid.Visible = false;
            }
        }
             
    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {

        cctype();
        
    }

    public void cctype()
    {
        if (Session["roles"].ToString() == "Project Manager")
        {
            ddlcctype.DataTextField="Performing";
            trtype.Visible = true;
            //trccode.Visible = false;
            year.Visible = false;
            //btn.Visible = false;
            //ddltype.SelectedIndex = 0;
            //trbtnsubmit.Visible = false;
            grid.Visible = false;
            header.Visible = false;
            trbtnsubmit.Visible = false;
        }
        else
        {
            if (ddlcctype.SelectedItem.Text == "Select")
            {
                trtype.Visible = true;
                trccode.Visible = true;
                year.Visible = true;
                btn.Visible = true;
                trbtnsubmit.Visible = false;
                grid.Visible = false;
                header.Visible = false;
                ddlCCcode.SelectedIndex = 0;
                ddlyear.SelectedIndex = 0;
                ddltype.SelectedIndex = 0;

            }
            else if (ddlcctype.SelectedItem.Text == "Performing")
            {
                trtype.Visible = true;
                trccode.Visible = false;
                year.Visible = false;
                //btn.Visible = false;
                ddltype.SelectedIndex = 0;
               
                //trbtnsubmit.Visible = false;
                grid.Visible = false;
                header.Visible = false;
                trbtnsubmit.Visible = false;

            }
            else if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
            {

                if (Session["roles"].ToString() == "HoAdmin")
                {
                    da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='4' and c.CC_type='" + ddlcctype.SelectedItem.Text + "' and c.status in ('Old','New')", con);
                }
                else if (Session["roles"].ToString() == "SuperAdmin")
                {
                    da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='5' and c.CC_type='" + ddlcctype.SelectedItem.Text + "' and c.status in ('Old','New') ", con);
                }
                da.Fill(ds, "SC");
                ddlCCcode.DataTextField = "Name";
                ddlCCcode.DataValueField = "cc_code";
                ddlCCcode.DataSource = ds.Tables["SC"];
                ddlCCcode.DataBind();
                ddlCCcode.Items.Insert(0, "Select Cost Center");
                trtype.Visible = false;
                trccode.Visible = true;
                year.Visible = true;
                //btn.Visible = false;
                grid.Visible = false;
                header.Visible = false;
                trbtnsubmit.Visible = false;

            }
        }
       
    }

    protected void ddlCCcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            ////year.Visible = false;
            ////btn.Visible = true;
            grid.Visible = false;
            header.Visible = false;
            trbtnsubmit.Visible = false;

        }
        if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
        {
            
            year.Visible = true;
            LoadYear();
            ////btn.Visible = true;
            grid.Visible = false;
            header.Visible = false;
            trbtnsubmit.Visible = false;
        }
    }


    public void LoadYear()
    {
        ddlyear.Items.Clear();
        if (Session["roles"].ToString() == "Project Manager")
            da = new SqlDataAdapter("Select year from budget_cc where status='4' and cc_code='" + ddlCCcode.SelectedValue + "'", con);
        else if (Session["roles"].ToString() == "HoAdmin")
        {
           if(ddlcctype.SelectedItem.Text == "Performing")
                 da = new SqlDataAdapter("Select year from budget_cc where status='4A' and cc_code='" + ddlCCcode.SelectedValue + "'", con);
           else  if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
                 da = new SqlDataAdapter("Select year from budget_cc where status='4' and cc_code='" + ddlCCcode.SelectedValue + "'", con);
        }
        else if (Session["roles"].ToString() == "SuperAdmin")

            da = new SqlDataAdapter("Select year from budget_cc where status='5' and cc_code='" + ddlCCcode.SelectedValue + "'", con);
        da.Fill(ds, "year");
        ddlyear.DataTextField = "year";
        ddlyear.DataValueField = "year";
        ddlyear.DataSource = ds.Tables["year"];
        ddlyear.DataBind();
        ddlyear.Items.Insert(0, "Select Year");
    }
}

