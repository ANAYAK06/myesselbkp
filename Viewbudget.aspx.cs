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
using System.IO;
using System.Text;
using AjaxControlToolkit;
using System.Collections.Specialized;
using System.Web.Services;
using System.Web.Services.Protocols;


public partial class Viewbudget : System.Web.UI.Page
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
            trgridpercentage.Visible = false;
            trgridheader.Visible = false;
            LoadYear();
            header.Visible = false;
            dca.Visible = false;
            year.Visible = false;
            month.Visible = false;
            Trnet.Visible = false;
            trtype.Visible = false;
            if (Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Chairman Cum Managing Director")
            {

                Label6.Visible = true;
                Label9.Visible = true;
            }
            else
            {
                Label6.Visible = false;
                Label9.Visible = false;
            }
            if (Session["roles"].ToString() == "Chief Material Controller")
            {
                ddlcctype.Items.Remove("Non-Performing");
            }
            btnprint.Visible = false;
            trexcel.Visible = false;
            hf1.Value = Session["roles"].ToString();
            if (Session["roles"].ToString() == "Project Manager")
            {
                type.Style.Add("visibility", "hidden");
                trtype.Style.Add("visibility", "hidden");
                fillcc();
            }
            else
            {
                type.Style.Add("visibility", "visible");

            }

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
        ddlyear.Items.Insert(0, "Select Year");
    }
    public void fillheader()
    {
        try
        {
            if (Session["roles"].ToString() != "Project Manager")
            {
                if (ViewState["CCType"].ToString() == "Performing")
                    da = new SqlDataAdapter("select c.cc_code as ccode,c.cc_name as cname,round(sum(isnull(d.budget_amount,0)),2) as budget_amount,round(sum(isnull(d.balance,0)),2) as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Performing' and c.cc_code='" + ddlCCcode.SelectedValue + "'  and d.status='6' and cc_subtype='" + ddltype.SelectedItem.Text + "' group by c.cc_code,c.cc_name", con);
                else if (ViewState["CCType"].ToString() == "Non-Performing")
                    da = new SqlDataAdapter("select c.cc_code as ccode,c.cc_name as cname,isnull(d.budget_amount,0)as budget_amount,isnull(d.balance,0)as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Non-Performing' and d.year='" + ddlyear.SelectedItem.Text + "' and c.cc_code='" + ddlCCcode.SelectedValue + "' and d.status='6'  group by c.cc_code,c.cc_name,d.budget_amount,d.balance", con);
                else
                    da = new SqlDataAdapter("select c.cc_code as ccode,c.cc_name as cname,isnull(d.budget_amount,0)as budget_amount,isnull(d.balance,0)as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Capital' and d.year='" + ddlyear.SelectedItem.Text + "' and c.cc_code='" + ddlCCcode.SelectedValue + "' and d.status='6' group by  c.cc_code,c.cc_name,d.budget_amount,d.balance", con);
            }
            else
            {
                da = new SqlDataAdapter("select c.cc_code as ccode,c.cc_name as cname,sum(isnull(d.budget_amount,0))as budget_amount,sum(isnull(d.balance,0))as balance from cost_center c join budget_cc d on c.cc_code=d.cc_code  where c.CC_type='Performing' and c.cc_code='" + ddlCCcode.SelectedValue + "'  and d.status='6' group by c.cc_code,c.cc_name", con);
            }

            da.Fill(ds, "gridfill");
            if (ds.Tables["gridfill"].Rows.Count > 0)
            {

                double output6 = Convert.ToDouble(ds.Tables["gridfill"].Rows[0].ItemArray[2].ToString());
                double output7 = Convert.ToDouble(ds.Tables["gridfill"].Rows[0].ItemArray[3].ToString());
                Label1.Text = ds.Tables["gridfill"].Rows[0].ItemArray[0].ToString();
                Label5.Text = ds.Tables["gridfill"].Rows[0].ItemArray[1].ToString();
                Label6.Text = String.Format("{0:#,##,##,###.00}", output6);
                Label7.Text = String.Format("{0:#,##,##,###.00}", output7);
                Label9.Text = "Budget Assigned";
                Label10.Text = "Balance";
                header.Visible = true;
                Trnet.Visible = false;
                btnprint.Visible = true;
            }
            else
            {
                Trnet.Visible = false;
                header.Visible = false;
                btnprint.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void filldcaheader()
    {
        try
        {
            if (Session["roles"].ToString() != "Project Manager")
            {
                if (ViewState["CCType"].ToString() == "Performing")
                    da = new SqlDataAdapter("select c.cc_code as ccode,c.cc_name as cname,sum(isnull(d.budget_dca_yearly,0))as budget_amount,sum(isnull(d.dca_yearly_bal,0))as balance from cost_center c join yearly_dcabudget d on c.cc_code=d.cc_code   where c.CC_type='Performing' and c.cc_code='" + ddlCCcode.SelectedValue + "' and d.dca_code='" + ddldetailhead.SelectedItem.Text + "'  and d.status='3' and cc_subtype='" + ddltype.SelectedItem.Text + "' group by c.cc_code,c.cc_name", con);
                else if (ViewState["CCType"].ToString() == "Non-Performing")
                    da = new SqlDataAdapter("select c.cc_code as ccode,c.cc_name as cname,sum(isnull(d.budget_dca_yearly,0))as budget_amount,sum(isnull(d.dca_yearly_bal,0))as balance from cost_center c join yearly_dcabudget d on c.cc_code=d.cc_code   where c.CC_type='Non-Performing' and d.year='" + ddlyear.SelectedItem.Text + "' and c.cc_code='" + ddlCCcode.SelectedValue + "' and d.dca_code='" + ddldetailhead.SelectedItem.Text + "' and d.status='3' group by c.cc_code,c.cc_name,d.budget_dca_yearly,d.dca_yearly_bal", con);
                else
                    da = new SqlDataAdapter("select c.cc_code as ccode,c.cc_name as cname,sum(isnull(d.budget_dca_yearly,0))as budget_amount,sum(isnull(d.dca_yearly_bal,0))as balance from cost_center c join yearly_dcabudget d on c.cc_code=d.cc_code   where c.CC_type='Capital' and d.year='" + ddlyear.SelectedItem.Text + "' and c.cc_code='" + ddlCCcode.SelectedValue + "' and d.dca_code='" + ddldetailhead.SelectedItem.Text + "' and d.status='3' group by  c.cc_code,c.cc_name,d.budget_dca_yearly,d.dca_yearly_bal", con);
            }
            else
            {
                da = new SqlDataAdapter("select c.cc_code as ccode,c.cc_name as cname,sum(isnull(d.budget_dca_yearly,0))as budget_amount,sum(isnull(d.dca_yearly_bal,0))as balance from cost_center c join yearly_dcabudget d on c.cc_code=d.cc_code   where c.CC_type='Performing' and c.cc_code='" + ddlCCcode.SelectedValue + "' and d.dca_code='" + ddldetailhead.SelectedItem.Text + "'  and d.status='3' group by c.cc_code,c.cc_name", con);
            }
            da.Fill(ds, "griddcafill");
            if (ds.Tables["griddcafill"].Rows.Count > 0)
            {
                Label1.Text = ds.Tables["griddcafill"].Rows[0].ItemArray[0].ToString();
                Label5.Text = ds.Tables["griddcafill"].Rows[0].ItemArray[1].ToString();
                Label6.Text = ds.Tables["griddcafill"].Rows[0].ItemArray[2].ToString();
                Label7.Text = ds.Tables["griddcafill"].Rows[0].ItemArray[3].ToString();
                Label9.Text = "DCA Budget Assigned";
                Label10.Text = "Balance";
                header.Visible = true;

            }
            else
            {
                Trnet.Visible = false;
                header.Visible = false;
                btnprint.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    public void fillbody()
    {
        ds.Clear();
        try
        {
            if (Session["roles"].ToString() != "Project Manager")
            {
                if ((Session["roles"].ToString() == "Central Store Keeper") || (Session["roles"].ToString() == "StoreKeeper") || (Session["roles"].ToString() == "Accountant"))
                {
                    accruedinterest();
                    Assetdep();
                    if (ViewState["CCType"].ToString() == "Performing")
                        da = new SqlDataAdapter("Select Rec.[DCA Code],d.dca_name as [DCA Name],Rec.[DCA Budget],Rec.[Consumed Budget],Rec.[DCA Balance] from (Select (case when i.[DCA Code] is not null then i.[DCA Code] else E.Dca_code end)[DCA Code],(isnull([DCA Budget],0)+isnull([Prev],0)) as [DCA Budget],(isnull([Consumed Budget],0)+isnull([Prev],0)) as [Consumed Budget],isnull([DCA Balance],0)as [DCA Balance]  from (select  y.dca_code as [DCA Code],sum(isnull(y.budget_dca_yearly,0))as [DCA Budget],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget],sum(isnull(y.dca_yearly_bal,0))as [DCA Balance] from yearly_dcabudget y join Cost_Center c on y.cc_code=c.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and c.CC_type='Performing' AND c.cc_subtype='" + ddltype.SelectedItem.Text + "' and y.status='3' group by y.dca_code)i full outer join (select dca_code,(isnull(cash,0)+isnull(cheque,0))[Prev] from [2009-10 Expences] where cc_code='" + ddlCCcode.SelectedValue + "' and (cash is not null or cheque is not null))E on i.[DCA Code]=E.dca_code)Rec join dca d on Rec.[DCA Code]=d.dca_code union all Select 'Over Head' as [DCA Code],'Over Head' as [DCA Name],Round((((i.[DCA Budget]+isnull(prev,0))*i.OverHead)/100),2) as [Over Head],Round((((i.[Consumed Budget]+isnull(prev,0))*i.OverHead)/100),2) as [Consumed Budget],Round((((i.[DCA Budget]*i.OverHead)/100)-((i.[Consumed Budget]*i.OverHead)/100)),2) as [Balance Budget] from (select  sum(isnull(y.budget_dca_yearly,0)) as [DCA Budget],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget],OverHead,(select (Sum(isnull(cash,0))+Sum(isnull(cheque,0))) from [2009-10 Expences] where cc_code='" + ddlCCcode.SelectedValue + "')[Prev] from CCOverheadandDepreciation d join yearly_dcabudget y on d.cc_code=y.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and y.status='3' group by overhead)i  union all SELECT 'Depreciation Value' as [DCA Code],'Depreciation Value' as [DCA Name],depreciationvalue as [DCA Budget],'0' AS [consumed budget],'0' AS [Balance Budget] FROM CCOverheadandDepreciation where cc_code='" + ddlCCcode.SelectedValue + "' Union All Select i.[dca code],i.[dca name],isnull(i.[dca budget],0)[dca budget],isnull(i.[consumed budget],0)[consumed budget],'0' as [Balance Budget] from (Select 'Interest on CC' as [DCA Code],'Interest applicable negative cash status' as [DCA Name],(SELECT cc_accruedvalue FROM cc_accruedvalues WHERE cc_code='" + ddlCCcode.SelectedValue + "') as [DCA Budget],'0'as [Consumed Budget]) i", con);
                    else if (ViewState["CCType"].ToString() == "Non-Performing")
                        da = new SqlDataAdapter("select  d.dca_code as [DCA Code],d.dca_name as [DCA Name],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget] from dca d join yearly_dcabudget y on d.dca_code=y.dca_code join Cost_Center c on y.cc_code=c.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and c.CC_type='Non-Performing'and y.year='" + ddlyear.SelectedItem.Text + "' and y.status='3' group by d.dca_code,d.dca_name,y.dca_yearly_bal", con);
                    else
                        da = new SqlDataAdapter("select  d.dca_code as [DCA Code],d.dca_name as [DCA Name],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget] from dca d join yearly_dcabudget y on d.dca_code=y.dca_code join Cost_Center c on y.cc_code=c.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and c.CC_type='Capital'and y.year='" + ddlyear.SelectedItem.Text + "' and y.status='3' group by d.dca_code,d.dca_name,y.dca_yearly_bal", con);
                }
                else
                {
                    accruedinterest();
                    Assetdep();
                    if (ViewState["CCType"].ToString() == "Performing")
                        da = new SqlDataAdapter("Select Rec.[DCA Code],d.dca_name as [DCA Name],Rec.[DCA Budget],Rec.[Consumed Budget],Rec.[DCA Balance] from (Select (case when i.[DCA Code] is not null then i.[DCA Code] else E.Dca_code end)[DCA Code],(isnull([DCA Budget],0)+isnull([Prev],0)) as [DCA Budget],(isnull([Consumed Budget],0)+isnull([Prev],0)) as [Consumed Budget],isnull([DCA Balance],0)as [DCA Balance]  from (select  y.dca_code as [DCA Code],sum(isnull(y.budget_dca_yearly,0))as [DCA Budget],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget],sum(isnull(y.dca_yearly_bal,0))as [DCA Balance] from yearly_dcabudget y join Cost_Center c on y.cc_code=c.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and c.CC_type='Performing' AND c.cc_subtype='" + ddltype.SelectedItem.Text + "' and y.status='3' group by y.dca_code)i full outer join (select dca_code,(isnull(cash,0)+isnull(cheque,0))[Prev] from [2009-10 Expences] where cc_code='" + ddlCCcode.SelectedValue + "' and (cash is not null or cheque is not null))E on i.[DCA Code]=E.dca_code)Rec join dca d on Rec.[DCA Code]=d.dca_code union all Select 'Over Head' as [DCA Code],'Over Head' as [DCA Name],Round((((i.[DCA Budget]+isnull(prev,0))*i.OverHead)/100),2) as [Over Head],Round((((i.[Consumed Budget]+isnull(prev,0))*i.OverHead)/100),2) as [Consumed Budget],Round((((i.[DCA Budget]*i.OverHead)/100)-((i.[Consumed Budget]*i.OverHead)/100)),2) as [Balance Budget] from (select  sum(isnull(y.budget_dca_yearly,0)) as [DCA Budget],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget],OverHead,(select (Sum(isnull(cash,0))+Sum(isnull(cheque,0))) from [2009-10 Expences] where cc_code='" + ddlCCcode.SelectedValue + "')[Prev] from CCOverheadandDepreciation d join yearly_dcabudget y on d.cc_code=y.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and y.status='3' group by overhead)i  union all SELECT 'Depreciation Value' as [DCA Code],'Depreciation Value' as [DCA Name],depreciationvalue as [DCA Budget],'0' AS [consumed budget],'0' AS [Balance Budget] FROM CCOverheadandDepreciation where cc_code='" + ddlCCcode.SelectedValue + "' Union All Select i.[dca code],i.[dca name],isnull(i.[dca budget],0)[dca budget],isnull(i.[consumed budget],0)[consumed budget],'0' as [Balance Budget] from (Select 'Interest on CC' as [DCA Code],'Interest applicable negative cash status' as [DCA Name],(SELECT cc_accruedvalue FROM cc_accruedvalues WHERE cc_code='" + ddlCCcode.SelectedValue + "') as [DCA Budget],'0'as [Consumed Budget]) i", con);
                    else if (ViewState["CCType"].ToString() == "Non-Performing")
                        da = new SqlDataAdapter("select  d.dca_code as [DCA Code],d.dca_name as [DCA Name],sum(isnull(y.budget_dca_yearly,0))as [DCA Budget],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget],sum(isnull(y.dca_yearly_bal,0))as [DCA Balance] from dca d join yearly_dcabudget y on d.dca_code=y.dca_code join Cost_Center c on y.cc_code=c.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and c.CC_type='Non-Performing'and y.year='" + ddlyear.SelectedItem.Text + "' and y.status='3' group by d.dca_code,d.dca_name,y.budget_dca_yearly,y.dca_yearly_bal", con);
                    else
                        da = new SqlDataAdapter("select  d.dca_code as [DCA Code],d.dca_name as [DCA Name],sum(isnull(y.budget_dca_yearly,0))as [DCA Budget],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget],sum(isnull(y.dca_yearly_bal,0))as [DCA Balance] from dca d join yearly_dcabudget y on d.dca_code=y.dca_code join Cost_Center c on y.cc_code=c.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and c.CC_type='Capital'and y.year='" + ddlyear.SelectedItem.Text + "' and y.status='3' group by d.dca_code,d.dca_name,y.budget_dca_yearly,y.dca_yearly_bal", con);
                }
            }
            else if (Session["roles"].ToString() == "Project Manager")
            {
                accruedinterest();
                Assetdep();

                da = new SqlDataAdapter("Select Rec.[DCA Code],d.dca_name as [DCA Name],Rec.[DCA Budget],Rec.[Consumed Budget],Rec.[DCA Balance] from (Select (case when i.[DCA Code] is not null then i.[DCA Code] else E.Dca_code end)[DCA Code],(isnull([DCA Budget],0)+isnull([Prev],0)) as [DCA Budget],(isnull([Consumed Budget],0)+isnull([Prev],0)) as [Consumed Budget],isnull([DCA Balance],0)as [DCA Balance]  from (select  y.dca_code as [DCA Code],sum(isnull(y.budget_dca_yearly,0))as [DCA Budget],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget],sum(isnull(y.dca_yearly_bal,0))as [DCA Balance] from yearly_dcabudget y join Cost_Center c on y.cc_code=c.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and c.CC_type='Performing' and y.status='3' group by y.dca_code)i full outer join (select dca_code,(isnull(cash,0)+isnull(cheque,0))[Prev] from [2009-10 Expences] where cc_code='" + ddlCCcode.SelectedValue + "' and (cash is not null or cheque is not null))E on i.[DCA Code]=E.dca_code)Rec join dca d on Rec.[DCA Code]=d.dca_code union all Select 'Over Head' as [DCA Code],'Over Head' as [DCA Name],Round((((i.[DCA Budget]+isnull(prev,0))*i.OverHead)/100),2) as [Over Head],Round((((i.[Consumed Budget]+isnull(prev,0))*i.OverHead)/100),2) as [Consumed Budget],Round((((i.[DCA Budget]*i.OverHead)/100)-((i.[Consumed Budget]*i.OverHead)/100)),2) as [Balance Budget] from (select  sum(isnull(y.budget_dca_yearly,0)) as [DCA Budget],sum(isnull((y.budget_dca_yearly-y.dca_yearly_bal),0))as [Consumed Budget],OverHead,(select (Sum(isnull(cash,0))+Sum(isnull(cheque,0))) from [2009-10 Expences] where cc_code='" + ddlCCcode.SelectedValue + "')[Prev] from CCOverheadandDepreciation d join yearly_dcabudget y on d.cc_code=y.cc_code where y.cc_code='" + ddlCCcode.SelectedValue + "' and y.status='3' group by overhead)i  union all select 'Depreciation Value' as [DCA Code],'Depreciation Value' as [DCA Name],depreciationvalue as [DCA Budget],'0' as [Consumed Budget],'0' AS [Balance Budget] from CCOverheadandDepreciation where cc_code='" + ddlCCcode.SelectedValue + "' Union All Select i.[dca code],i.[dca name],isnull(i.[dca budget],0)[dca budget],isnull(i.[consumed budget],0)[consumed budget],'0' as [Balance Budget] from (Select 'Interest on CC' as [DCA Code],'Interest applicable negative cash status' as [DCA Name],(SELECT cc_accruedvalue FROM cc_accruedvalues WHERE cc_code='" + ddlCCcode.SelectedValue + "') as [DCA Budget],'0'as [Consumed Budget]) i", con);

            }
            da.Fill(ds, "bodyfill");
            ds.DefaultViewManager.DataViewSettings["bodyfill"].Sort = "order by year desc,order by Date desc,order by date desc";
            var i = ds.Tables["bodyfill"].Rows.Count.ToString();
            if (ds.Tables["bodyfill"].Rows.Count > 0)
            {
                trexcel.Visible = true;
                GridView1.DataSource = ds.Tables["bodyfill"];
                GridView1.DataBind();
                Trnet.Visible = false;
                btnprint.Visible = true;

            }
            else
            {
                trexcel.Visible = false;
                Trnet.Visible = false;
                btnprint.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void Dep()
    {
        try
        {
            da = new SqlDataAdapter("Select isnull(Sum(value),0) as [Dep] from (SELECT CAST(isnull(Sum(((isnull(i.NoOfdays,0))*Basic_Price*0.1)/100),0) as Decimal(25,2))[value] FROM (select (DATEDIFF(DAY,Received_date,Transfer_date)) NoOfdays,(SELECT master_data.basicprice from master_data where item_code=assetdep.item_code)as Basic_Price from AssetDep where cc_code='" + ddlCCcode.SelectedValue + "')i UNION ALL (select CAST(isnull(Sum(((isnull((DATEDIFF(DAY,date,getdate())),0))*basicprice*0.1)/100),0) as Decimal(25,2))[Assetvalue] from master_data where cc_code='" + ddlCCcode.SelectedValue + "'  and substring(item_code,1,1)='1') union all select CAST(isnull(sum(amount),0) as Decimal(25,2)) from assetstatic where cc_code='" + ddlCCcode.SelectedValue + "')j", con);
            da.Fill(ds, "Dep");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    public void accruedinterest()
    {
        if (ViewState["CCType"].ToString() == "Performing")
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CCAccrued_interest";
            command.CommandTimeout = 100;
            command.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlCCcode.SelectedValue;
            command.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "3";
            command.Connection = con;
            con.Open();
            SqlDataReader reader;
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ViewState["incf"] = reader["NewAccumulatedInterst"].ToString();
                }
            }
            reader.Close();
            con.Close();
        }
        else
            ViewState["incf"] = '0';
    }
    public void Assetdep()
    {
        if (ViewState["CCType"].ToString() == "Performing")
        {

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Asset_depreciation";
            command.Parameters.AddWithValue("@CCCode", SqlDbType.DateTime).Value = ddlCCcode.SelectedValue;
            command.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";

            command.Connection = con;
            con.Open();
            SqlDataReader reader;
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ViewState["Dep"] = reader["Depvalue"].ToString();
                }
            }
            reader.Close();
        }
        else
            ViewState["Dep"] = '0';
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["roles"].ToString() != "Project Manager")
            {
                ViewState["CCType"] = ddlcctype.SelectedItem.Text;
                if (ViewState["CCType"].ToString() == "Non-Performing" || ViewState["CCType"].ToString() == "Capital")
                {
                    year.Visible = true;
                    month.Visible = false;
                    Trnet.Visible = false;
                    fillheader();
                    ViewState["Yr1"] = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
                    ViewState["Yr2"] = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();

                }
                else if (ViewState["CCType"].ToString() == "Performing")
                {
                    year.Visible = false;
                    Trnet.Visible = false;
                    month.Visible = false;
                    fillheader();
                    ViewState["Yr1"] = "";
                    ViewState["Yr2"] = "";
                }
                fillbody();
                ViewState["code"] = ddlCCcode.SelectedValue;
                gridview();
                gridview1();


            }
            else
            {
                ViewState["CCType"] = "Performing";
                year.Visible = false;
                ddlcctype.Enabled = false;
                Trnet.Visible = false;
                month.Visible = false;
                ViewState["Yr1"] = "";
                ViewState["Yr2"] = "";
                fillheader();
                fillbody();
                ViewState["code"] = ddlCCcode.SelectedValue;
                gridview();
                gridview1();
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void visible()
    {
        try
        {
            type.Visible = true;
            pcc.Visible = true;
            if (Session["roles"].ToString() == "Project Manager")
            {
                year.Visible = false;
                Trnet.Visible = false;
            }

            month.Visible = false;
            btn.Visible = true;
            dca.Visible = false;
            btnprint.Visible = false;
            Trnet.Visible = false;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Viewbudget.aspx");

    }
    private decimal depvalue = (decimal)0.0;

    public void gridview()
    {
        if (ViewState["CCType"].ToString() == "Performing")
        {
            da = new SqlDataAdapter("Calcuating_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "2";
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["code"].ToString();


            da.Fill(ds, "tbl");
            if (ds.Tables["tbl"].Rows.Count > 0)
            {
                GridView2.DataSource = ds.Tables["tbl"];
                GridView2.DataBind();
                trgridheader.Visible = true;
                Tr1.Visible = true;
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                trgridheader.Visible = false;

            }

        }
        else
        {
            trgridheader.Visible = false;
            Tr1.Visible = false;
        }
    }

    public void gridview1()
    {

        da = new SqlDataAdapter("Calcuating_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ViewState["code"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@DCABudget", SqlDbType.Decimal).Value = Convert.ToDecimal(Convert.ToDecimal(Budget).ToString());
        da.SelectCommand.Parameters.AddWithValue("@currentconsume", SqlDbType.Decimal).Value = Convert.ToDecimal(Convert.ToDecimal(currentconsumed).ToString());

        da.Fill(ds, "tb");
        if (ds.Tables["tb"].Rows.Count > 0)
        {
            GridView3.DataSource = ds.Tables["tb"];
            GridView3.DataBind();
            trgridpercentage.Visible = true;
        }
        else
        {
            GridView3.DataSource = null;
            GridView3.DataBind();
            trgridpercentage.Visible = false;
        }
    }


    public int row = 0;
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[1].Text) - Convert.ToDecimal(ViewState["currentconsume"].ToString()));
            if (row == 0)
            {
                e.Row.Attributes.Add("onclick", "window.open('DCADetailviewReport.aspx?CCCode=" + ddlCCcode.SelectedValue + " &DCACode=" + e.Row.Cells[0].Text + "&Year=" + ViewState["Yr1"].ToString() + " &Year1=" + ViewState["Yr2"].ToString() + "&CCType=" + ViewState["CCType"].ToString() + "&role=" + Session["roles"].ToString() + "&type=2 ','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
            }
            else if (row == 1)
            {
                e.Row.Attributes.Add("onclick", "window.open('DCADetailviewReport.aspx?CCCode=" + ddlCCcode.SelectedValue + " &DCACode=" + e.Row.Cells[0].Text + "&Year=" + ViewState["Yr1"].ToString() + " &Year1=" + ViewState["Yr2"].ToString() + "&CCType=" + ViewState["CCType"].ToString() + "&role=" + Session["roles"].ToString() + "&type=3 ','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");

            }
            row = row + 1;
            if (Convert.ToDecimal(e.Row.Cells[2].Text) < 0)
            {
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((Session["roles"].ToString() == "Central Store Keeper") || (Session["roles"].ToString() == "StoreKeeper") || (Session["roles"].ToString() == "Project Manager") || (Session["roles"].ToString() == "Accountant"))
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    currentconsumed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Consumed Budget"));
                    Budget += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DCA Budget"));
                    e.Row.Attributes.Add("onclick", "window.open('DCADetailviewReport.aspx?CCCode=" + ddlCCcode.SelectedValue + " &DCACode=" + e.Row.Cells[0].Text + "&Year=" + ViewState["Yr1"].ToString() + " &Year1=" + ViewState["Yr2"].ToString() + "&CCType=" + ViewState["CCType"].ToString() + "&role=" + Session["roles"].ToString() + "&type=1 ','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                }
                if (e.Row.Cells[0].Text == "Depreciation Value")
                {

                    depvalue = Convert.ToDecimal(ViewState["Dep"].ToString());

                    if (ddlCCcode.SelectedValue == "CC-38")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(453554.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-41")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(434282) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-42")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(906643.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-43")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(801335) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-44")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(203105) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-45")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(63264.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-49")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(519636.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-50")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(97872) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-51")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(241825) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-52")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(58314.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-53")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(92345.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-54")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(257680.209) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-57")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(46175) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-58")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(51685.147) + depvalue).ToString();
                    else
                        e.Row.Cells[3].Text = depvalue.ToString();


                    e.Row.Cells[4].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(e.Row.Cells[3].Text));
                    currentconsumed += Convert.ToDecimal(e.Row.Cells[3].Text);
                    Balance += Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(e.Row.Cells[3].Text));

                    if (Convert.ToDecimal(e.Row.Cells[4].Text) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[0].Text == "Interest on CC")
                {
                    e.Row.Cells[3].Text = ViewState["incf"].ToString();
                    e.Row.Cells[4].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(ViewState["incf"].ToString()));
                    if (Convert.ToDecimal(e.Row.Cells[4].Text) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                    currentconsumed += Convert.ToDecimal(ViewState["incf"].ToString());
                    Balance += Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(ViewState["incf"].ToString()));
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].Text = String.Format("{0:#,##,##,###.00}", Budget);
                    ViewState["DCABudget"] = String.Format("{0:#,##,##,###.00}", Budget);
                    e.Row.Cells[3].Text = String.Format("{0:#,##,##,###.00}", currentconsumed);
                    ViewState["currentconsume"] = String.Format("{0:#,##,##,###.00}", currentconsumed);

                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    Budget += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DCA Budget"));


                    if (Convert.ToDecimal(e.Row.Cells[4].Text) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                    e.Row.Attributes.Add("onclick", "window.open('DCADetailviewReport.aspx?CCCode=" + ddlCCcode.SelectedValue + " &DCACode=" + e.Row.Cells[0].Text + "&Year=" + ViewState["Yr1"].ToString() + " &Year1=" + ViewState["Yr2"].ToString() + "&CCType=" + ViewState["CCType"].ToString() + "&role=" + Session["roles"].ToString() + "&type=1 ','Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'); return false;");
                }
                if (e.Row.Cells[0].Text == "Depreciation Value")
                {
                    depvalue = Convert.ToDecimal(ViewState["Dep"].ToString());

                    if (ddlCCcode.SelectedValue == "CC-38")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(453554.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-41")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(434282) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-42")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(906643.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-43")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(801335) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-44")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(203105) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-45")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(63264.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-49")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(519636.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-50")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(97872) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-51")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(241825) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-52")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(58314.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-53")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(92345.5) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-54")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(257680.209) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-57")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(46175) + depvalue).ToString();
                    else if (ddlCCcode.SelectedValue == "CC-58")
                        e.Row.Cells[3].Text = Convert.ToDecimal(Convert.ToDecimal(51685.147) + depvalue).ToString();
                    else
                        e.Row.Cells[3].Text = depvalue.ToString();


                    e.Row.Cells[4].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(e.Row.Cells[3].Text));
                    currentconsumed += Convert.ToDecimal(e.Row.Cells[3].Text);
                    Balance += Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(e.Row.Cells[3].Text));



                    if (Convert.ToDecimal(e.Row.Cells[4].Text) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[0].Text == "Interest on CC")
                {
                    e.Row.Cells[3].Text = ViewState["incf"].ToString();
                    e.Row.Cells[4].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(ViewState["incf"].ToString()));
                    if (Convert.ToDecimal(e.Row.Cells[4].Text) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                    currentconsumed += Convert.ToDecimal(ViewState["incf"].ToString());
                    Balance += Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(ViewState["incf"].ToString()));
                }
                if (e.Row.Cells[0].Text != "Depreciation Value" && e.Row.Cells[0].Text != "Interest on CC")
                {
                    currentconsumed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "consumed budget"));
                    Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DCA Balance"));
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].Text = String.Format("{0:#,##,##,###.00}", Budget);
                    ViewState["DCABudget"] = String.Format("{0:#,##,##,###.00}", Budget);
                    e.Row.Cells[3].Text = String.Format("{0:#,##,##,###.00}", currentconsumed);
                    ViewState["currentconsume"] = String.Format("{0:#,##,##,###.00}", currentconsumed);
                    e.Row.Cells[4].Text = String.Format("{0:#,##,##,###.00}", Balance);
                    if (Convert.ToDecimal(e.Row.Cells[4].Text) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    private decimal Budget = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
    private decimal currentconsumed = (decimal)0.0;
    private decimal Credit = (decimal)0.0;
    private decimal Cashdebit = (decimal)0.0;
    private decimal Chequedebit = (decimal)0.0;
    private decimal Indent = (decimal)0.0;
    private decimal PO = (decimal)0.0;
    private decimal ICF = (decimal)0.0;
    private decimal TotalDebit = (decimal)0.0;
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        //fillbody();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Normal View of Budget " + Label1.Text));
        Context.Response.Charset = "";

        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        tblgrid.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddltype.SelectedIndex = 0;
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            trtype.Visible = true;
            year.Visible = false;
        }
        else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
        {
            if ((Session["roles"].ToString() == "Accountant") || (Session["roles"].ToString() == "StoreKeeper"))
            {
                da = new SqlDataAdapter("select distinct u.cc_code,u.cc_code+' , '+c.cc_name+'' Name from CC_User u join Cost_Center c on u.cc_code=c.cc_code join budget_cc b on c.cc_code=b.cc_code where b.status='6' and c.cc_type='" + ddlcctype.SelectedItem.Text + "' and c.cc_code='" + Session["cc_code"].ToString() + "'  and c.status in ('Old','New')", con);
            }
            else
            {
                da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New')", con);

            }
            da.Fill(ds, "CC");
            ddlCCcode.DataTextField = "Name";
            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataSource = ds.Tables["CC"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
            trtype.Visible = false;
            year.Visible = true;

        }
        else
        {
            trtype.Visible = false;
            year.Visible = false;
        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            if ((Session["roles"].ToString() == "Accountant") || (Session["roles"].ToString() == "StoreKeeper"))
            {
                da = new SqlDataAdapter("select distinct u.cc_code,u.cc_code+' , '+c.cc_name+'' Name from CC_User u join Cost_Center c on u.cc_code=c.cc_code join budget_cc b on c.cc_code=b.cc_code where b.status='6' and c.cc_type='" + ddlcctype.SelectedItem.Text + "' and c.cc_code='" + Session["cc_code"].ToString() + "'  and c.status in ('Old','New') and c.cc_subtype='" + ddltype.SelectedItem.Text + "'", con);
            }
            else
            {
                //da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New') and c.cc_subtype='" + ddltype.SelectedItem.Text + "'", con);
                da = new SqlDataAdapter("select c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join budget_cc y on c.cc_code=y.cc_code where y.Status='6' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New') and c.cc_subtype='" + ddltype.SelectedItem.Text + "' GROUP by c.cc_code,c.cc_name ORDER BY CASE WHEN c.cc_code='CCC' THEN c.cc_code end ASC ,CASE WHEN c.cc_code!='CCC' THEN CONVERT(INT,REPLACE(c.CC_Code,'CC-','')) END ASC ", con);
            }
            da.Fill(ds, "CC");
            ddlCCcode.DataTextField = "Name";
            ddlCCcode.DataValueField = "cc_code";
            ddlCCcode.DataSource = ds.Tables["CC"];
            ddlCCcode.DataBind();
            ddlCCcode.Items.Insert(0, "Select Cost Center");
        }

    }
    public void fillcc()
    {
        //da = new SqlDataAdapter("select distinct u.cc_code,u.cc_code+' , '+c.cc_name+'' Name from CC_User u join Cost_Center c on u.cc_code=c.cc_code join budget_cc b on c.cc_code=b.cc_code where b.status='6'  and u.User_Name='" + Session["user"].ToString() + "' and c.status in ('Old','New')", con);
        da = new SqlDataAdapter("select  u.cc_code,u.cc_code+' , '+c.cc_name+'' Name from CC_User u join Cost_Center c on u.cc_code=c.cc_code join budget_cc b on c.cc_code=b.cc_code where b.status='6'  and u.User_Name='" + Session["user"].ToString() + "' and c.status in ('Old','New') GROUP by u.cc_code,c.cc_name ORDER BY CASE WHEN u.cc_code='CCC' THEN u.cc_code end ASC ,CASE WHEN u.cc_code!='CCC' THEN CONVERT(INT,REPLACE(u.CC_Code,'CC-','')) END ASC", con);
        da.Fill(ds, "CC");
        ddlCCcode.DataTextField = "Name";
        ddlCCcode.DataValueField = "cc_code";
        ddlCCcode.DataSource = ds.Tables["CC"];
        ddlCCcode.DataBind();
        ddlCCcode.Items.Insert(0, "Select Cost Center");

    }
    public void clear()
    {
        ddlcctype.SelectedIndex = 0;
        ddltype.SelectedIndex = 0;
        ddlCCcode.SelectedIndex = 0;
        CascadingDropDown3.SelectedValue = "";
        ddlmonth.SelectedIndex = 0;
        ddlyear.SelectedIndex = 0;
    }
}
