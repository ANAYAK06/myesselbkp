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
using System.Drawing;
using System.Net.Mail;

public partial class EsselCrReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    string f = ConfigurationManager.AppSettings["mailfrom"].ToString();
    string t = ConfigurationManager.AppSettings["mailto"].ToString();
    string t1 = ConfigurationManager.AppSettings["mailcr"].ToString();
    string s = ConfigurationManager.AppSettings["mailcc"].ToString();
    string id = ConfigurationManager.AppSettings["mailid"].ToString();
    string psw = ConfigurationManager.AppSettings["mailpassword"].ToString();
    string smtpaddress = ConfigurationManager.AppSettings["smtpaddress"].ToString();

    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    string s1 = "";
    string s2 = "";
    float total = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            LoadYear();
            statusname();
            eststatus();
            setstatus();
            SupportUser();
            //issuestatus();
            btnExcel.Visible = false;
        }
      
     
         
    }
    public void LoadYear()
    {
        for (int i = 2010; i < System.DateTime.Now.Year + 1; i++)
        {
            ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
        }
        ddlyear.Items.Insert(0, "Any Year");
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        fill(s1);
    }
    public void fill(string s1)
    {
        try
        {
            string condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    condition = condition + " and datepart(mm,a.date)=" + ddlMonth.SelectedValue + " and datepart(yy,a.date)=" + yy;

                }
                else
                {
                    condition = condition + " and convert(datetime,a.date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";

                }

            }
            if (ddlstatus.SelectedItem.Text != "Select All")
            {
                condition = condition + " and status_name='" + ddlstatus.SelectedItem.Text + "'";
            }
            ViewState["condition"] = condition;
            s1 = ViewState["condition"].ToString();
            if (ddltype.SelectedItem.Text == "CR")
            {
                if (ddlstatus.SelectedItem.Text == "Submited")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,null,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Approved")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status  where a.id>0  " + s1 + " ", con);

                }
                else if (ddlstatus.SelectedItem.Text == "Estimated")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);

                }
                else if (ddlstatus.SelectedItem.Text == "Change Estimation")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);

                }
                else if (ddlstatus.SelectedItem.Text == "Estimate Hold")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);

                }
                else if (ddlstatus.SelectedItem.Text == "Approved For Design")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Estmationverify_date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);

                }
                else if (ddlstatus.SelectedItem.Text == "AssignTo")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.AssignDate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);

                }
                else if (ddlstatus.SelectedItem.Text == "Task Started")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.TaskStartdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Task Completed")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.TaskEnddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Under UAT")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.QualityTransferdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "UAT Approved")
                {
                    da = new SqlDataAdapter("select distinct  a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.QualityApproveddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Transfer To Production")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.ProductionTrasferdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Approved In Production ")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.ProductionApproveddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Select All")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status   where a.id>0 and a.status='1'  union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status   where a.id>0 and a.status='2' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='9' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='9' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Estmationverify_date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='10' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Assigndate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='4' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.TaskStartdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='11' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.TaskEnddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='12' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.QualityTransferdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='13' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.QualityApproveddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='14' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.ProductionTrasferdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='15' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.ProductionApproveddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='16' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Estmationverify_date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='9A' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Estmationverify_date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='9B' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status   where a.id>0 and a.status='7'  union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status   where a.id>0 and a.status='8' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.breakdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='3' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.breakdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno  where a.id>0 and e.status='5' union select a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.breakdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from Esselcr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.crno where a.id>0 and e.status='6'", con);
                }
                else
                {
                    da = new SqlDataAdapter("select distinct a.id,a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status  where a.id>0  " + s1 + " ", con);
                }
            }
            else if (ddltype.SelectedItem.Text == "ISSUE")
            {
                //da = new SqlDataAdapter("select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,null as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status  where a.id>0 " + s1 + " ", con);
                if (ddlstatus.SelectedItem.Text == "Submited")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Approved")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status  where a.id>0  " + s1 + " ", con);

                }
                else if (ddlstatus.SelectedItem.Text == "Estimated")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.date, 100) AS Date,a.description as Description,null as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Approved For Design")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Estmationverify_date, 100) AS Date,a.description as Description,isnull(developmenttime,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);

                }
                else if (ddlstatus.SelectedItem.Text == "AssignTo")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.AssignDate, 100) AS Date,a.description as Description,isnull(developmenttime,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Task Started")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.TaskStartdate, 100) AS Date,a.description as Description,isnull(developmenttime,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Task Completed")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.TaskEnddate, 100) AS Date,a.description as Description,isnull(developmenttime,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Under UAT")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.QualityTransferdate, 100) AS Date,a.description as Description,isnull(developmenttime,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "UAT Approved")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.QualityTransferdate, 100) AS Date,a.description as Description,isnull(developmenttime,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Transfer To Production")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.ProductionTrasferdate, 100) AS Date,a.description as Description,isnull(developmenttime,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Approved In Production")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.ProductionApproveddate, 100) AS Date,a.description as Description,isnull(developmenttime,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0  " + s1 + " ", con);
                }
                else if (ddlstatus.SelectedItem.Text == "Select All")
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status   where a.id>0 and a.status='1'  union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status   where a.id>0 and a.status='2' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='9' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='9' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Estmationverify_date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='10' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Assigndate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='4' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.TaskStartdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='11' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.TaskEnddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='12' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.QualityTransferdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='13' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.QualityApproveddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='14' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.ProductionTrasferdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='15' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.ProductionApproveddate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='16' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Estmationverify_date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='9A' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.Estmationverify_date, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='9B' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status   where a.id>0 and a.status='7'  union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status   where a.id>0 and a.status='8' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.breakdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='3' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.breakdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno  where a.id>0 and e.status='5' union select a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), e.breakdate, 100) AS Date,a.description as Description,(select sum(isnull(brdtime,0)+isnull(tdtime,0)+isnull(developmenttime,0))from estimatedhours  where cr_issueno=e.cr_issueno) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status join estimatedhours e on e.cr_issueno=a.issueno where a.id>0 and e.status='6'", con);
                }
                else
                {
                    da = new SqlDataAdapter("select distinct a.id,a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.modified_date, 100) AS Date,a.description as Description,isnull(null,0) as Hours,b.status_name as Status,a.comment,a.priority,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status  where a.id>0  " + s1 + " ", con);
                }
            }
            da.Fill(ds, "fill");
            this.upd.Update();
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                btnExcel.Visible = true;
                GridView1.DataSource = ds.Tables["fill"];
                GridView1.DataBind();
            }
            else
            {
                btnExcel.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Hours"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblamount = (Label)e.Row.FindControl("lblTotal");
                lblamount.Text = total.ToString();
            }


            if ( Session["roles"].ToString() == "Chairman Cum Managing Director")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (ddlstatus.SelectedItem.Text == "Submited" || ddlstatus.SelectedItem.Text == "Client Hold")
                    {
                        GridView1.Columns[0].Visible = true;
                        GridView1.Columns[10].Visible = false;
                    }
                    else if (ddlstatus.SelectedItem.Text == "AssignTo" || ddlstatus.SelectedItem.Text == "Approved For Design" || ddlstatus.SelectedItem.Text == "Canceled" || ddlstatus.SelectedItem.Text == "Select All")
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = false;
                    }
                    
                    else if ((ddlstatus.SelectedItem.Text == "Estimated") && ddltype.SelectedItem.Text == "ISSUE")
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = false;
                    }

                    else if (ddlstatus.SelectedItem.Text == "Approved" || ddlstatus.SelectedItem.Text == "Estimated" || ddlstatus.SelectedItem.Text == "Estimate Hold" || ddlstatus.SelectedItem.Text == "Change Estimation" || ddlstatus.SelectedItem.Text == "Under UAT"  || ddlstatus.SelectedItem.Text == "Task Completed")
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = false;
                    }
                }
            }
            else if (Session["roles"].ToString() == "SupportAdmin" || Session["roles"].ToString() == "SupportUser")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    if ((ddlstatus.SelectedItem.Text == "Approved" && ddltype.SelectedItem.Text == "CR"))
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = false;
                    }
                    else if (ddlstatus.SelectedItem.Text == "Canceled" || ddlstatus.SelectedItem.Text == "Under UAT" || ddlstatus.SelectedItem.Text == "AssignTo" || ddlstatus.SelectedItem.Text == "Transfer To Production" || ddlstatus.SelectedItem.Text == "Task Completed" || ddlstatus.SelectedItem.Text == "Submited" ||  ddlstatus.SelectedItem.Text == "Client Hold" || ddlstatus.SelectedItem.Text == "Estimated" || ddlstatus.SelectedItem.Text == "Select All")
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = false;
                    }

                    else if (ddlstatus.SelectedItem.Text == "Approved" && ddltype.SelectedItem.Text == "ISSUE")
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = true;
                    }
                    else if (ddlstatus.SelectedItem.Text == "Approved For Design" && ddltype.SelectedItem.Text == "CR")
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = true;
                    }
                    else if (ddlstatus.SelectedItem.Text == "Change Estimation" && ddltype.SelectedItem.Text == "CR")
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[10].Visible = false;
                    }
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView1.Columns[0].Visible = false;
                    GridView1.Columns[10].Visible = false;
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
        poppo.Show();
        int id = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex]["id"].ToString());
        ViewState["id"] = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex]["id"].ToString());
        GridViewRow editingRow = GridView1.Rows[e.NewEditIndex];
        Label lblstatus = (Label)GridView1.FindControl("status");
        trestchange.Visible = true;
        trok.Visible = true;
        if (ddlstatus.SelectedItem.Text == "Submited")
        {
            fillgrid();
        }
        else if (ddlstatus.SelectedItem.Text == "Estimated" || ddlstatus.SelectedItem.Text == "Client Hold" || ddlstatus.SelectedItem.Text == "Estimate Hold")
        {
            fillgrid();
            fillestimated();
            txtcomment.Text = "";
        }
        else if (ddlstatus.SelectedItem.Text == "Change Estimation")
        {
            fillgrid();
            fillestimated();

            if (Session["roles"].ToString() == "SupportAdmin" || Session["roles"].ToString() == "SupportUser")
            {
                txtbrd.Enabled = true;
                txttd.Enabled = true;
                txtdevelopment.Enabled = true;
                trestchange.Visible = false;
                trok.Visible = true;
            }
            else
            {
                txtbrd.Enabled = false;
                txttd.Enabled = false;
                txtdevelopment.Enabled = false;
                trestchange.Visible = false;
                trok.Visible = false;
            }
        }
        else if (ddlstatus.SelectedItem.Text == "Approved" && (Session["roles"].ToString() == "SupportUser" || Session["roles"].ToString() == "SupportAdmin"))
        {
            changestatus();
        }
        else if (ddlstatus.SelectedItem.Text == "Approved" && Session["roles"].ToString() == "Chairman Cum Managing Director")
        {
            fillapprovegrid();
        }
        else if (ddlstatus.SelectedItem.Text == "Task Completed" || ddlstatus.SelectedItem.Text == "Approved For Design" || ddlstatus.SelectedItem.Text == "Under UAT" || ddlstatus.SelectedItem.Text == "Transfer To Production")
        {
            changestatus();

        }
        ddleststatus.SelectedIndex = 0;
    }

    public void fillapprovegrid()
    {
       
        ddlpriority.SelectedIndex = 3;
        txtcomment.Text = "";

        
        try
        {
            if (ddltype.SelectedItem.Text == "CR")
            {
                ddlcrtype.SelectedIndex = 1;
                da = new SqlDataAdapter("select a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date,100) AS Date,a.description as Description,b.status_name as Status,r.user_name,r.Roles,a.crtype,a.Raised_Role from EsselCr a join EsselCr_Status b on a.status = b.status join user_roles r on a.Raised_Role=r.user_name where a.id='" + ViewState["id"].ToString() + "' and status_name='" + ddlstatus.SelectedItem.Text + "' ", con);
            }
            else if (ddltype.SelectedItem.Text == "ISSUE")
            {
                ddlcrtype.SelectedIndex = 2;
                da = new SqlDataAdapter("select a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date, 100) AS Date,a.description as Description,b.status_name as Status,r.user_name,r.roles,a.crtype,a.Raised_Role from EsselIssue a join EsselCr_Status b on a.status = b.status  join user_roles r on a.Raised_Role=r.user_name where a.id='" + ViewState["id"].ToString() + "' and status_name='" + ddlstatus.SelectedItem.Text + "'", con);
            }
            da.Fill(ds, "filldata");
            if (ds.Tables["filldata"].Rows.Count > 0)
            {
                poppo.Show();
                tblpopup.Visible = true;
                tblestimate.Visible = false;
                tblstatus.Visible = false;
                lblno.Text = ds.Tables["filldata"].Rows[0]["CR/ISSUE No"].ToString();
                ViewState["name"] = ds.Tables["filldata"].Rows[0]["user_name"].ToString();
                name();
                lblraised.Text = ViewState["name1"].ToString() + " , " + ds.Tables["filldata"].Rows[0]["Roles"].ToString();
                txtdesc.Text = ds.Tables["filldata"].Rows[0]["description"].ToString();
                ViewState["description"] = txtdesc.Text;
                ViewState["NO"] = ds.Tables["filldata"].Rows[0]["CR/ISSUE No"].ToString();
                // ddlcrtype.SelectedItem.Text = ds.Tables["filldata"].Rows[0]["crtype"].ToString();
            }
            else
            {
                poppo.Hide();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillgrid()
    {

        ddlpriority.SelectedIndex = 3;
        txtcomment.Text = "";
        ddlsetstatus.SelectedIndex = 1;

        try
        {
            if (ddltype.SelectedItem.Text == "CR")
            {
                ddlcrtype.SelectedIndex = 1;
                da = new SqlDataAdapter("select a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date,100) AS Date,a.description as Description,b.status_name as Status,r.user_name,r.Roles,a.crtype,a.Raised_Role from EsselCr a join EsselCr_Status b on a.status = b.status join user_roles r on a.Raised_Role=r.user_name where a.id='" + ViewState["id"].ToString() + "' and status_name='" + ddlstatus.SelectedItem.Text + "' ", con);
         
            }
            else if (ddltype.SelectedItem.Text == "ISSUE")
            {

                ddlcrtype.SelectedIndex = 2;
                da = new SqlDataAdapter("select a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date, 100) AS Date,a.description as Description,b.status_name as Status,r.user_name,r.roles,a.crtype,a.Raised_Role from EsselIssue a join EsselCr_Status b on a.status = b.status  join user_roles r on a.Raised_Role=r.user_name where a.id='" + ViewState["id"].ToString() + "' and status_name='" + ddlstatus.SelectedItem.Text + "'", con);
            }
            da.Fill(ds, "filldata");
            if (ds.Tables["filldata"].Rows.Count > 0)
            {
                poppo.Show();
                tblpopup.Visible = true;
                tblestimate.Visible = false;
                tblstatus.Visible = false;
                lblno.Text = ds.Tables["filldata"].Rows[0]["CR/ISSUE No"].ToString();
                ViewState["name"] = ds.Tables["filldata"].Rows[0]["Raised_Role"].ToString();
                name();
                lblraised.Text = ViewState["name1"].ToString() + " , " + ds.Tables["filldata"].Rows[0]["Roles"].ToString();
                txtdesc.Text = ds.Tables["filldata"].Rows[0]["description"].ToString();
                ViewState["description"] = txtdesc.Text;
                ViewState["NO"] = ds.Tables["filldata"].Rows[0]["CR/ISSUE No"].ToString();
                // ddlcrtype.SelectedItem.Text = ds.Tables["filldata"].Rows[0]["crtype"].ToString();
            }
            else
            {
                poppo.Hide();
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void changestatus()
    {
        try
        {
            txtremarks.Text = "";
            if (ddltype.SelectedItem.Text == "CR")
            {
               // da = new SqlDataAdapter("select a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date,100) AS Date,a.description as Description,b.status_name as Status,r.user_name,r.Roles,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status join user_roles r on a.role=r.user_name where a.id='" + ViewState["id"].ToString() + "' and status_name='" + ddlstatus.SelectedItem.Text + "' ", con);
                da = new SqlDataAdapter("select a.crno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date,100) AS Date,a.description as Description,b.status_name as Status,a.crtype from EsselCr a join EsselCr_Status b on a.status = b.status  where a.id='" + ViewState["id"].ToString() + "' and status_name='" + ddlstatus.SelectedItem.Text + "' ", con);
            }
            else if (ddltype.SelectedItem.Text == "ISSUE")
            {
                da = new SqlDataAdapter("select a.issueno as [CR/ISSUE No],CONVERT(VARCHAR(20), a.date, 100) AS Date,a.description as Description,b.status_name as Status,a.crtype from EsselIssue a join EsselCr_Status b on a.status = b.status  where a.id='" + ViewState["id"].ToString() + "' and status_name='" + ddlstatus.SelectedItem.Text + "'", con);
            }
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                ddlfillstatus.Items.Clear();
                poppo.Show();
                if (ddltype.SelectedItem.Text == "ISSUE")
                {
                    issuestatus();
                    tblpopup.Visible = false;
                    tblestimate.Visible = false;
                    tblstatus.Visible = true;
                    truser.Visible = true;
                }
                else if (ddltype.SelectedItem.Text == "CR")
                {
                    issuestatus();
                    tblpopup.Visible = false;
                    tblestimate.Visible = false;
                    tblstatus.Visible = true;
                }
                lblCategoryno.Text = ds.Tables["fill"].Rows[0]["CR/ISSUE No"].ToString();
                ViewState["Categoryno"] = ds.Tables["fill"].Rows[0]["CR/ISSUE No"].ToString();
                ViewState["Desc"] = ds.Tables["fill"].Rows[0]["Description"].ToString();
                //ViewState["Priority"] = ds.Tables["fill"].Rows[0]["Priority"].ToString();

                ddlfillstatus.Items.Insert(0, "status");
                if (ddlstatus.SelectedItem.Text == "Under UAT")
                {
                    ddlfillstatus.Items.Clear();
                    ddlfillstatus.Items.Insert(0, "status");
                    truser.Visible = false;
                    ddlfillstatus.Items.Insert(1, "UAT Approved");
                }
                else if (ddlstatus.SelectedItem.Text == "Transfer To Production")
                {
                    ddlfillstatus.Items.Clear();
                    ddlfillstatus.Items.Insert(0, "status");
                    truser.Visible = false;
                    ddlfillstatus.Items.Insert(1, "Approved In Production");
                }
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlstatus.SelectedItem.Text == "Submited" || ddlstatus.SelectedItem.Text == "Approved" || ddlstatus.SelectedItem.Text == "Client Hold")
            {
                if (ddlsetstatus.SelectedItem.Text == "Approved")
                {
                    cmd = new SqlCommand("sp_EsselcrsVerify", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", ViewState["id"].ToString());
                    cmd.Parameters.AddWithValue("@Type", ddlcrtype.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@no", ViewState["NO"].ToString());
                    cmd.Parameters.AddWithValue("@Priority", ddlpriority.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@desc", txtdesc.Text);
                    cmd.Parameters.AddWithValue("@raised_role", ViewState["name"].ToString());
                    cmd.Parameters.AddWithValue("@comment", txtcomment.Text);
                    cmd.Parameters.AddWithValue("@status", ddlsetstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                }
                else if (ddlsetstatus.SelectedItem.Text == "Canceled" || ddlsetstatus.SelectedItem.Text == "Client Hold")
                {
                    cmd = new SqlCommand("HoldorCancel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = ddltype.SelectedItem.Text;
                    cmd.Parameters.Add("@desc", SqlDbType.VarChar).Value = txtdesc.Text;
                    cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = txtcomment.Text;
                    cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddlsetstatus.SelectedValue;
                    cmd.Parameters.Add("@no", SqlDbType.VarChar).Value = ViewState["NO"].ToString();
                }
            }
            //if (ddlsetstatus.SelectedItem.Text == "Approved" && ddlstatus.SelectedItem.Text == "Approved")
            //{
            //    cmd = new SqlCommand("sp_ReVerify", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@Type", ddlcrtype.SelectedItem.Text);
            //    cmd.Parameters.AddWithValue("@no", ViewState["NO"].ToString());
            //    cmd.Parameters.AddWithValue("@Priority", ddlpriority.SelectedItem.Text);
            //    cmd.Parameters.AddWithValue("@desc", txtdesc.Text);
            //    cmd.Parameters.AddWithValue("@raised_role", ViewState["name"].ToString());
            //    cmd.Parameters.AddWithValue("@comment", txtcomment.Text);
            //    cmd.Parameters.AddWithValue("@status", ddlsetstatus.SelectedValue);
            //    cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
            //}
          
            ViewState["descs"] = txtdesc.Text;
            ViewState["Priority"] = ddlpriority.SelectedItem.Text;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfull")
            {
                poppo.Hide();
                JavaScript.UPAlert(Page, msg);
                this.upd.Update();
                fill(s1);
                if (ddlcrtype.SelectedItem.Text != ddltype.SelectedItem.Text)
                {
                    no();
                }
                sendmail();
            }
            else
            {
                poppo.Hide();
                JavaScript.UPAlert(Page, msg);
                this.upd.Update();
                fill(s1);
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }
    }
    public void no()
    {

        try
        {
            if (ddlcrtype.SelectedItem.Text == "ISSUE")
            {
                da = new SqlDataAdapter("select top 1 issueno as no from esselissue order by id desc", con);
            }
            else
            {
                da = new SqlDataAdapter("select top 1 crno as no from esselcr order by id desc", con);
            }
            da.Fill(ds, "no");
            //lbllabel.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
            if (ds.Tables["no"].Rows.Count > 0)
            {
                ViewState["newno"] = ds.Tables["no"].Rows[0].ItemArray[0].ToString();
            }
            else
            {
                ViewState["newno"] = null;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void name()
    {
        try
        {
            da = new SqlDataAdapter("select (first_name+' '+middle_name+' '+last_name) as name from Employee_Data where user_name='" + ViewState["name"].ToString() + "'", con);
            da.Fill(ds, "name");
            //lbllabel.Text = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
            if (ds.Tables["name"].Rows.Count > 0)
            {
                ViewState["name1"] = ds.Tables["name"].Rows[0].ItemArray[0].ToString();
            }
            else
            {
                ViewState["name1"] = "Pradeep";
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void sendmail()
    {
        try
        {
            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            ////Msg.From = new MailAddress(f.ToString());
            Msg.From = new MailAddress(f, "Essel-IT Support");
            // Recipient e-mail address.
            Msg.To.Add(t.ToString());
            Msg.To.Add(t1.ToString());
            Msg.CC.Add(s.ToString());

            // Msg.Body = "Hi ,\n\n New " + ddltype.SelectedItem.Text + "," + ViewState["NO"].ToString() + " Approved by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["description"].ToString() + " \n\n   ";
            if (ddlstatus.SelectedItem.Text == "Submited")
            {
                if (ddlsetstatus.SelectedItem.Text == "Approved")
                {
                    if (ddlcrtype.SelectedItem.Text == ddltype.SelectedItem.Text)
                    {
                        Msg.Subject = "New " + ddlcrtype.SelectedItem.Text + " " + ViewState["NO"].ToString() + " Approved by " + Session["roles"].ToString() + " ";
                        Msg.Body = "Hi ,\n\n New " + ddlcrtype.SelectedItem.Text + " " + ViewState["NO"].ToString() + ", " + ddlsetstatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["descs"].ToString() + " \n\n Priority :" + ViewState["Priority"].ToString() + "";
                    }
                    else
                    {
                        Msg.Subject = "New " + ddlcrtype.SelectedItem.Text + " " + ViewState["newno"].ToString() + "  Approved by " + Session["roles"].ToString() + " ";
                        Msg.Body = "Hi ,\n\n New " + ddlcrtype.SelectedItem.Text + " " + ViewState["newno"].ToString() + " , " + ddlsetstatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["descs"].ToString() + " \n\n   ";
                    }
                }
                else if (ddlsetstatus.SelectedItem.Text == "Client Hold")
                {
                    Msg.Subject = "New " + ddlcrtype.SelectedItem.Text + " " + ViewState["NO"].ToString() + " Hold by " + Session["roles"].ToString();
                    Msg.Body = "Hi ,\n\n New " + ddlcrtype.SelectedItem.Text + " " + ViewState["NO"].ToString() + ", " + ddlsetstatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["descs"].ToString() + " \n\n   ";
                }

                else if (ddlsetstatus.SelectedItem.Text == "Canceled")
                {
                    Msg.Subject = "New " + ddlcrtype.SelectedItem.Text + " " + ViewState["NO"].ToString() + " Cancelled by " + Session["roles"].ToString();
                    Msg.Body = "Hi ,\n\n New " + ddlcrtype.SelectedItem.Text + " " + ViewState["NO"].ToString() + ", " + ddlsetstatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["descs"].ToString() + " \n\n   ";
                }
            }
           
            else if (ddlstatus.SelectedItem.Text == "Approved")
            {
                if ((ddltype.SelectedItem.Text != ddlcrtype.SelectedItem.Text) && ddlstatus.SelectedItem.Text == "Approved")
                {
                    Msg.Subject = "" + ddltype.SelectedItem.Text + " " + ViewState["NO"].ToString() + " changed to  " + ddlcrtype.SelectedItem.Text + "  " + ViewState["newno"].ToString() + "  by " + Session["roles"].ToString();
                    Msg.Body = "Hi ,\n\n  " + ddlcrtype.SelectedItem.Text + " " + ViewState["newno"].ToString() + " " + ddlsetstatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["descs"].ToString() + " \n\n Priority :" + ViewState["Priority"].ToString() + "   ";

                }
                else
                {
                    Msg.Subject = "" + ddlcrtype.SelectedItem.Text + " " + ViewState["NO"].ToString() + " " + ddlsetstatus.SelectedItem.Text + " by  " + Session["roles"].ToString();
                    Msg.Body = "Hi ,\n\n  " + ddlcrtype.SelectedItem.Text + " " + ViewState["NO"].ToString() + " " + ddlsetstatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["descs"].ToString() + " \n\n Priority :" + ViewState["Priority"].ToString() + "     ";


                }

            }
            else if (ddlstatus.SelectedItem.Text == "Under UAT")
            {
                Msg.Subject = "" + ddltype.SelectedItem.Text + " " + ViewState["Categoryno"].ToString() + "  " + ddlfillstatus.SelectedItem.Text + " by " + Session["roles"].ToString();
                Msg.Body = "Hi ,\n\n  " + ddltype.SelectedItem.Text + "," + ViewState["Categoryno"].ToString() + " " + ddlfillstatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["Desc"].ToString() + " \n\n   ";
            }
            else if (ddlstatus.SelectedItem.Text == "Transfer To Production")
            {
                Msg.Subject = "" + ddltype.SelectedItem.Text + "" + ViewState["Categoryno"].ToString() + "  " + ddlfillstatus.SelectedItem.Text + " by " + Session["roles"].ToString();
                Msg.Body = "Hi ,\n\n  " + ddltype.SelectedItem.Text + "," + ViewState["Categoryno"].ToString() + " " + ddlfillstatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["Desc"].ToString() + " \n\n   ";
            }
            else if (ddlstatus.SelectedItem.Text == "Estimated")
            {
                Msg.Subject = "" + ddltype.SelectedItem.Text + "" + ViewState["crno"].ToString() + " " + ddleststatus.SelectedItem.Text + " by " + Session["roles"].ToString();
                if (ViewState["comment"].ToString() != null)
                {
                    Msg.Body = "Hi ,\n\n  " + ddltype.SelectedItem.Text + "," + ViewState["crno"].ToString() + " " + ddleststatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["des"].ToString() + " \n\n Comment : " + ViewState["comment"].ToString() + "    ";
                }
                else
                {
                    Msg.Body = "Hi ,\n\n  " + ddltype.SelectedItem.Text + "," + ViewState["crno"].ToString() + " " + ddleststatus.SelectedItem.Text + " by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["des"].ToString() + " \n\n  ";

                }
            }
            else if (ddlstatus.SelectedItem.Text == "Estimate Hold" || ddlstatus.SelectedItem.Text == "Change Estimation")
            {

                Msg.Subject = "" + ddltype.SelectedItem.Text + " " + ViewState["crno"].ToString() + " Estimation changed ";
                if (ViewState["comment"].ToString() != null)
                {
                    Msg.Body = "Hi ,\n\n  " + ddltype.SelectedItem.Text + "," + ViewState["crno"].ToString() + "  \n\n Description : " + ViewState["des"].ToString() + " \n\n  Comment : " + ViewState["comment"].ToString() + "\n\n please Approve for further process \n\n\n Thanks \n SL Touch.";
                }
                else
                {
                    Msg.Body = "Hi ,\n\n  " + ddltype.SelectedItem.Text + "," + ViewState["crno"].ToString() + "  \n\n Description : " + ViewState["des"].ToString() + "\n\n please Approve for further process \n\n\n Thanks \n IndusTouch.";

                }

            }
            Msg.DeliveryNotificationOptions.ToString();
            // your remote SMTP server IP.
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = smtpaddress;
            //smtp.Port = 587;
            //smtp.Credentials = new System.Net.NetworkCredential(id.ToString(), psw.ToString());
            //smtp.EnableSsl = true;
            SmtpClient smtp = new SmtpClient("127.0.0.1");
            smtp.Port = 25;
            smtp.Send(Msg);
            Msg = null;

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
            statusname();
            btnExcel.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void statusname()
    {
        try
        {
            ddlstatus.Items.Clear();
            da = new SqlDataAdapter("select status,status_name from esselcr_status  order by status_name", con);
            da.Fill(ds, "fillstatus");
            ddlstatus.DataTextField = "status_name";
            ddlstatus.DataValueField = "status";
            ddlstatus.DataSource = ds.Tables["fillstatus"];
            ddlstatus.DataBind();
            ddlstatus.Items.Insert(0, "Select All");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void eststatus()
    {
        try
        {
            ddleststatus.Items.Clear();
            da = new SqlDataAdapter("select status,status_name from esselcr_status where status in('10','8','9A','9B') order by status_name", con);
            da.Fill(ds, "eststatus");
            ddleststatus.DataTextField = "status_name";
            ddleststatus.DataValueField = "status";
            ddleststatus.DataSource = ds.Tables["eststatus"];
            ddleststatus.DataBind();
            ddleststatus.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void setstatus()
    {
        try
        {
            ddlsetstatus.Items.Clear();
            da = new SqlDataAdapter("select status,status_name from esselcr_status where status in('2','7','8') order by status_name", con);
            da.Fill(ds, "setstatus");
            ddlsetstatus.DataTextField = "status_name";
            ddlsetstatus.DataValueField = "status";
            ddlsetstatus.DataSource = ds.Tables["setstatus"];
            ddlsetstatus.DataBind();
            ddlsetstatus.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    public void fillstatus()
    {
        try
        {
            ddlsetstatus.Items.Clear();
            da = new SqlDataAdapter("select status,status_name from esselcr_status where status in('14') order by status_name", con);
            da.Fill(ds, "status");
            ddlfillstatus.DataTextField = "status_name";
            ddlfillstatus.DataValueField = "status";
            ddlfillstatus.DataSource = ds.Tables["status"];
            ddlfillstatus.DataBind();
            ddlfillstatus.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        poppo.Show();
        //int id = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex]["id"].ToString());
        
        int index = GridView1.SelectedIndex;
        int id = Convert.ToInt32(GridView1.DataKeys[index].Value.ToString());
        ViewState["id"] = Convert.ToInt32(GridView1.DataKeys[index].Value.ToString());
        //GridViewRow editingRow = GridView1.Rows[e.NewEditIndex];
        Label lblstatus = (Label)GridView1.FindControl("status");
        trestchange.Visible = true;
        trok.Visible = true;
        if (ddlstatus.SelectedItem.Text == "Submited")
        {
            fillgrid();
        }
        else if (ddlstatus.SelectedItem.Text == "Estimated" || ddlstatus.SelectedItem.Text == "Client Hold" || ddlstatus.SelectedItem.Text == "Estimate Hold")
        {
            fillgrid();
            fillestimated();
            txtcomment.Text = "";
        }
        else if (ddlstatus.SelectedItem.Text == "Change Estimation")
        {
            fillgrid();
            fillestimated();

            if (Session["roles"].ToString() == "SupportAdmin" || Session["roles"].ToString() == "SupportUser")
            {
                txtbrd.Enabled = true;
                txttd.Enabled = true;
                txtdevelopment.Enabled = true;
                trestchange.Visible = false;
                trok.Visible = true;
            }
            else
            {
                txtbrd.Enabled = false;
                txttd.Enabled = false;
                txtdevelopment.Enabled = false;
                trestchange.Visible = false;
                trok.Visible = false;
            }
        }
        else if (ddlstatus.SelectedItem.Text == "Approved" && (Session["roles"].ToString() == "SupportUser" || Session["roles"].ToString() == "SupportAdmin"))
        {
            changestatus();
        }
        else if (ddlstatus.SelectedItem.Text == "Approved" && Session["roles"].ToString() == "Chairman Cum Managing Director")
        {
            fillapprovegrid();
        }
        else if (ddlstatus.SelectedItem.Text == "Task Completed" || ddlstatus.SelectedItem.Text == "Approved For Design" || ddlstatus.SelectedItem.Text == "Under UAT" || ddlstatus.SelectedItem.Text == "Transfer To Production")
        {
            changestatus();

        }
        ddleststatus.SelectedIndex = 0;

    }
    public void fillestimated()
    {
        txtecomment.Text = "";
        try
        {
            DataSet ds1 = new DataSet();
            ds1.Clear();
            if (ddltype.SelectedItem.Text == "CR")
            {
                da = new SqlDataAdapter("select c.crno as no,c.crtype,e.brdtime,e.tdtime,e.developmenttime,e.description,s.status_name from esselcr c join  EstimatedHours e on e.cr_issueno=c.crno join esselcr_status s on s.status=e.status where c.crno='" + ViewState["NO"].ToString() + "' ", con);
            }
            else
            {
                da = new SqlDataAdapter("select c.issueno as no,c.crtype,e.brdtime,e.tdtime,e.developmenttime,e.description,s.status_name from esselissue c join  EstimatedHours e on e.cr_issueno=c.issueno join esselcr_status s on s.status=e.status where c.issueno='" + ViewState["NO"].ToString() + "' ", con);

            }
            da.Fill(ds1, "fillestimated");
            if (ds1.Tables["fillestimated"].Rows.Count > 0)
            {
                txtcomment.Text = "";
                lbledesc.Text = "";
                tblpopup.Visible = false;
                tblestimate.Visible = true;
                tblstatus.Visible = false;
                truser.Visible = false;
                h1.Value = ds1.Tables["fillestimated"].Rows[0]["no"].ToString();
                lbleno.Text = ds1.Tables["fillestimated"].Rows[0]["no"].ToString();
                ViewState["ENHNO"] = ds1.Tables["fillestimated"].Rows[0]["no"].ToString();
                lbletype.Text = ds1.Tables["fillestimated"].Rows[0]["crtype"].ToString();
                txtbrd.Text = ds1.Tables["fillestimated"].Rows[0]["brdtime"].ToString();
                txttd.Text = ds1.Tables["fillestimated"].Rows[0]["tdtime"].ToString();
                txtdevelopment.Text = ds1.Tables["fillestimated"].Rows[0]["developmenttime"].ToString();
                lbledesc.Text = ds1.Tables["fillestimated"].Rows[0]["description"].ToString();
                issuestatus();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_VerifyEstimation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@no", lbleno.Text);
            cmd.Parameters.AddWithValue("@BRD", txtbrd.Text);
            cmd.Parameters.AddWithValue("@td", txttd.Text);
            cmd.Parameters.AddWithValue("@Developtime", txtdevelopment.Text);
            cmd.Parameters.AddWithValue("@comment", txtecomment.Text);
            if (ddleststatus.SelectedItem.Text == "Approved For Design" || ddleststatus.SelectedItem.Text == "Estimate Hold" || ddleststatus.SelectedItem.Text == "Change Estimation" || ddleststatus.SelectedItem.Text == "Canceled")
            {
                cmd.Parameters.AddWithValue("@status", ddleststatus.SelectedValue);
            }
            else
            {
                cmd.Parameters.AddWithValue("@status", "9");
            }
            ViewState["crno"] = lbleno.Text;
            ViewState["comment"] = txtecomment.Text;
            ViewState["des"] = txtdesc.Text;
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfull")
            {
                JavaScript.UPAlert(Page, msg);
                this.upd.Update();
                poppo.Hide();
                fill(s1);
                sendmail();
            }
            else
            {
                JavaScript.UPAlert(Page, "Failed to Submit"); ;
                poppo.Hide();
            }
            con.Close();
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
            cmd = new SqlCommand("sp_ChangeStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;

            ddlfillstatus.Items.Insert(1, "UAT Approved");

            if (ddlfillstatus.SelectedItem.Text == "UAT Approved")
            {
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "14";
            }
            else if (ddlfillstatus.SelectedItem.Text == "Approved In Production")
            {
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "16";
            }
            else if (ddlstatus.SelectedItem.Text == "Approved")
            {
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddlfillstatus.SelectedValue;
            }
            else if (ddlstatus.SelectedItem.Text == "Approved For Design")
            {
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddlfillstatus.SelectedValue;
            }
            cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = txtremarks.Text;
            cmd.Parameters.Add("@SupportUser", SqlDbType.VarChar).Value = ddlusername.SelectedItem.Text;
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = ddltype.SelectedItem.Text;
            cmd.Parameters.Add("@no", SqlDbType.VarChar).Value = ViewState["Categoryno"].ToString();
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfull")
            {
                poppo.Hide();
                JavaScript.UPAlert(Page, msg);
                this.upd.Update();
                fill(s1);
                if ((ddlstatus.SelectedItem.Text == "Approved" && ddlfillstatus.SelectedItem.Text == "") || ddlfillstatus.SelectedItem.Text == "UAT Approved" || ddlfillstatus.SelectedItem.Text == "Approved In Production")
                {
                    sendmail();
                }
            }
            else
            {
                poppo.Hide();
                JavaScript.UPAlert(Page, msg);
            }

            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
        finally
        {
            con.Close();
        }

    }
    public void SupportUser()
    {
        try
        {
            ddlusername.Items.Clear();
            da = new SqlDataAdapter("select username from itmsregister where user_type='SupportUser' order by username ", con);
            da.Fill(ds, "SupportUser");
            ddlusername.DataTextField = "username";
            ddlusername.DataValueField = "username";
            ddlusername.DataSource = ds.Tables["SupportUser"];
            ddlusername.DataBind();
            ddlusername.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    public void issuestatus()
    {
        try
        {
            ddlfillstatus.Items.Clear();

            da = new SqlDataAdapter("select status,status_name from esselcr_status where status in('4') order by status_name", con);

            da.Fill(ds, "status");
            ddlfillstatus.DataTextField = "status_name";
            ddlfillstatus.DataValueField = "status";
            ddlfillstatus.DataSource = ds.Tables["status"];
            ddlfillstatus.DataBind();
            //  ddlfillstatus.Items.Insert(0, "select");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    protected void btntd_Click(object sender, EventArgs e)
    {

        string no = ViewState["ENHNO"].ToString();
        if (no != null)
        {
            string name = "c:/inetpub/vhosts/esselprojects.com/subdomains/quality/httpdocs/uploads/TD-" + no + ".docx";
            //string name = "D:/Essel(17-07-12)/uploads/TD-" + ViewState["ENHNO"].ToString() +".docx";

            if (File.Exists(name))
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.docx", "TD-" + no));
                //Response.TransmitFile(name);
                Response.WriteFile(name);
                Response.End();
            }
            else
            {
                //Response.Write("<script>alert("There is no file")</script>");
                //JavaScript.UPAlert(Page, "There is no file");
                showalert("There is no doc file");
            }
        }
        else
        {
            JavaScript.UPAlert(Page, "There is no file");
        }

    }
    protected void btnbrd_Click(object sender, EventArgs e)
    {
       

        string no = ViewState["ENHNO"].ToString();
        if (no != null)
        {
           string name = "c:/inetpub/vhosts/esselprojects.com/subdomains/quality/httpdocs/uploads/BRD-" + no + ".docx";
            //string name = "~/uploads/BRD-" + ViewState["ENHNO"].ToString() + ".docx";
           //string name = "D:/Essel(17-07-12)/uploads/BRD-" + ViewState["ENHNO"].ToString() + ".docx";
            if (File.Exists(name))
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.docx", "BRD-" + no));
                //Response.TransmitFile(Server.MapPath(name));
                Response.WriteFile(name);
                Response.End();
            }
            else
            {                
                //JavaScript.UPAlert(Page, "There is no file");
                showalert("There is no doc file");
            }
        }
        else
        {
          JavaScript.UPAlert(Page,"There is no file");
        }

    }
    public void showalert(string message)
    {
        Label mylabel = new Label();
        //message = "There is no doc file";
        mylabel.Text = "<script language='javascript'>window.alert('" + message + "')</script>";
        Page.Controls.Add(mylabel);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        fill(s1);
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Report"));
        Context.Response.Charset = "";
        GridView1.AllowPaging = false;
        GridView1.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
}