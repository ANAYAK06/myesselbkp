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


public partial class AssignDCABudget : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esseldb"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Essel childmaster = (Essel)this.Master;
            HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
            lbl.Attributes.Add("class", "active");

            if (Session["user"] == null)
                Response.Redirect("SessionExpire.aspx");

            if (!IsPostBack)
            {
                header.Visible = false;
                btngridsubmit.Visible = false;
                trtype.Visible = false;
                trcccode.Visible = false;
                tryear.Visible = false;
                trgrid.Visible = false;
                btn.Visible = false;
                //LoadYear();
            }
            cascadingDCA cs = new cascadingDCA();

            cs.Budgetpcc("undefined:" + ddlcctype.SelectedItem.Text + ";", "dd", ddlcctype.SelectedItem.Text);
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void LoadYear()
    {
        ddlyear.Items.Clear();
        if (Session["roles"].ToString() == "Sr.Accountant")
            da = new SqlDataAdapter("Select year from budget_cc where status='3' and cc_code='"+ ddlCCcode.SelectedValue +"'", con);
        else if (Session["roles"].ToString() == "HoAdmin")
            da = new SqlDataAdapter("Select year from budget_cc where status='4' and cc_code='" + ddlCCcode.SelectedValue + "'", con);
        else if (Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("Select year from budget_cc where status='5' and cc_code='" + ddlCCcode.SelectedValue + "'", con);
        da.Fill(ds, "year");
        ddlyear.DataTextField = "year";
        ddlyear.DataValueField = "year";
        ddlyear.DataSource = ds.Tables["year"];
        ddlyear.DataBind();
        ddlyear.Items.Insert(0, "Select Year");
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
    protected void Accordion1_ItemDataBound(object sender, AjaxControlToolkit.AccordionItemEventArgs e)
    {
        if (e.ItemType == AjaxControlToolkit.AccordionItemType.Header)
        {
            da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(d.budget_amount,'.0000','.00') as budget_amount,replace(d.balance,'.0000','.00') as balance,d.year from Cost_Center c join budget_cc d on c.cc_code=d.cc_code where c.cc_code='" + ddlCCcode.SelectedValue + "'", con);
            da.Fill(ds, "prev");


        }
        
    }
    public void getCCBudget()
    {
        header.Visible = true;
        btngridsubmit.Visible = true;
        if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        {

            if (ddlyear.SelectedValue != "" && ddlCCcode.SelectedValue != "")
                da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(d.budget_amount,'.0000','.00') as budget_amount,replace(d.balance,'.0000','.00') as balance ,d.year from Cost_Center c join budget_cc d on c.cc_code=d.cc_code where c.cc_code='" + ddlCCcode.SelectedValue + "' and d.year='" + ddlyear.SelectedItem.Text + "'", con);
        }
        else 
        {
            da = new SqlDataAdapter("select c.cc_code,c.cc_name,replace(d.budget_amount,'.0000','.00') as budget_amount,replace(d.balance,'.0000','.00') as balance ,d.year from Cost_Center c join budget_cc d on c.cc_code=d.cc_code where c.cc_code='" + ddlCCcode.SelectedValue + "'", con);
        }
        da.Fill(ds, "normalview");
        if (ds.Tables["normalview"].Rows.Count > 0)
        {
            Label1.Text = ds.Tables["normalview"].Rows[0][0].ToString();
            Label5.Text = ds.Tables["normalview"].Rows[0][1].ToString();
            Label6.Text = ds.Tables["normalview"].Rows[0][2].ToString();
            Label7.Text = ds.Tables["normalview"].Rows[0][3].ToString();
            Label8.Text = ds.Tables["normalview"].Rows[0][4].ToString();
            h1.Value = ds.Tables["normalview"].Rows[0][2].ToString();
            if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
            {
                Label8.Visible = true;
                Label9.Visible = true;
            }
            else
            {
                Label8.Visible = false;
                Label9.Visible = false;
            }
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        trgrid.Visible = true;
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            lblyear.Visible = false;
            ddlyear.Visible = false;
        }

        getCCBudget();
        da = new SqlDataAdapter("select distinct d.dca_code,dca_name,d.mapdca_code from dca d join dcatype dt on d.dca_code=dt.dca_code WHERE cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "') and dt.status='Active' order by d.dca_code asc", con);
        da.Fill(ds, "grid");
        GridView1.DataSource = ds.Tables["grid"];
        GridView1.DataBind();
    }
    protected void btngridsubmit_Click(object sender, EventArgs e)
    {
        
        try
        {
            string sdca = "";
            string Amounts = "";

            foreach (GridViewRow record in GridView1.Rows)
            {
                if ((record.FindControl("txtamount") as TextBox).Text != "")
                {
                    sdca = sdca + GridView1.DataKeys[record.RowIndex]["dca_code"].ToString() + ",";
                    Amounts = Amounts + (record.FindControl("txtamount") as TextBox).Text + ",";
                    Amount = Amount + Convert.ToDecimal((record.FindControl("txtamount") as TextBox).Text);
                }
            }



            cmd = new SqlCommand("sp_Assign_DcaBudget", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CCCode", Label1.Text);
            cmd.Parameters.AddWithValue("@DCACodes", sdca);
            cmd.Parameters.AddWithValue("@Year", Label8.Text);
            cmd.Parameters.AddWithValue("@Amounts", Amounts);
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@DCAAmount", Amount);
            cmd.Parameters.AddWithValue("@status", "1");
            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd.Connection = con;
            con.Open();

            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "DCA Budget Assigned Successfully")
                JavaScript.UPAlertRedirect(Page, msg, "AssignDCABudget.aspx");
            else
                JavaScript.UPAlert(Page, msg);



        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
    }
    private decimal Amount = (decimal)0.0;

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddltype.SelectedItem.Text != "Select")
        {
            if (ddltype.SelectedItem.Text == "Service")
                da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Service' ", con);
            else if (ddltype.SelectedItem.Text == "Trading")
                da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Trading' ", con);
            else if (ddltype.SelectedItem.Text == "Manufacturing")
                da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='Performing' and c.status in ('Old','New') and cc_subtype='Manufacturing' ", con);
            da.Fill(ds, "SERVICECC");
            ddlCCcode.DataTextField = "Name";
            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataSource = ds.Tables["SERVICECC"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
            trcccode.Visible = true;
            trgrid.Visible = false;
         
        }
        else if(ddltype.SelectedItem.Text == "Select")
        {
            trcccode.Visible = false;
            tryear.Visible = false;
            btn.Visible = false;
            trgrid.Visible = false;
            
           
        }
    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            btn.Visible = false;
            ddltype.SelectedValue = "Select";
            trtype.Visible = true;
            tryear.Visible = false;
            trgrid.Visible = false;
            trcccode.Visible = false;

        }
        else if (ddlcctype.SelectedItem.Text == "Non-Performing")
        {
            btn.Visible = false;
            trtype.Visible = false;
            trcccode.Visible = true;
            tryear.Visible = false;
            trgrid.Visible = false;
            da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='" + ddlcctype.SelectedItem.Text + "' and c.status in ('Old','New')  ", con);
            da.Fill(ds, "CC");
            ddlCCcode.DataTextField = "Name";
            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataSource = ds.Tables["CC"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
        }
        else if (ddlcctype.SelectedItem.Text == "Capital")
        {
            btn.Visible = false;
            trtype.Visible = false;
            trcccode.Visible = true;
            tryear.Visible = false;
            trgrid.Visible = false;
            da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='" + ddlcctype.SelectedItem.Text + "' and c.status in ('Old','New') ", con);
            da.Fill(ds, "CC");
            ddlCCcode.DataTextField = "Name";
            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataSource = ds.Tables["CC"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
        }
        else
        {
            trcccode.Visible = false;
            trtype.Visible = false;
            tryear.Visible = false;
            trgrid.Visible = false;
            btn.Visible = false;
        }
    }



    protected void ddlCCcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            tryear.Visible = false;
            btn.Visible = true;
            trgrid.Visible = false;
            
        }
        if ((ddlcctype.SelectedItem.Text == "Non-Performing") || (ddlcctype.SelectedItem.Text == "Capital"))
        {
            LoadYear();
            tryear.Visible = true;
            btn.Visible = true;
            trgrid.Visible = false;
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AssignDCABudget.aspx");
    }
}
