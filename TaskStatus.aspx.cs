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
using System.Net.Mail;
using AjaxControlToolkit;
using System.Web.Services;

public partial class TaskStatus : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");


        if (!IsPostBack)
        {
            SupportUser();
            //LoadYear();
            statusname();
            fillstatus();
        }
    }
    public void SupportUser()
    {
        try
        {
            ddlnames.Items.Clear();
            da = new SqlDataAdapter("select username from itmsregister where user_type='SupportUser' order by username ", con);
            da.Fill(ds, "SupportUser");
            ddlnames.DataTextField = "username";
            ddlnames.DataValueField = "username";
            ddlnames.DataSource = ds.Tables["SupportUser"];
            ddlnames.DataBind();
            ddlnames.Items.Insert(0, "SupportUser");
            ddlnames.Items.Insert(1, "Select All");
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    //public void LoadYear()
    //{
    //    for (int i = 2005; i < System.DateTime.Now.Year + 1; i++)
    //    {
    //        ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
    //    }
    //    ddlyear.Items.Insert(0, "Select Year");
    //}

    protected void btnok_Click(object sender, EventArgs e)
    {
        fillgrid(s1,GridView1);

    }
    public void fillgrid(string s1, GridView gridControl)
    {
        try
        {
            string condition = "";
            //if (ddlyear.SelectedIndex != 0)
            //{
                //if ((txtdate1.Text) != null && (txtdate2.Text) != null)
                //{
                //   // string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                //    //condition = condition + " and datepart(mm,c.date)=" + ddlMonth.SelectedValue + " and datepart(yy,c.date)=" + yy;

                //}
                if((txtdate1.Text) != null && (txtdate2.Text) != null)
                {
                    condition = condition + " and convert(datetime,c.date) between '" + txtdate1.Text + "' and '" + txtdate2.Text + "'";

                }

            //}
            //if (ddltype.SelectedItem.Text != "Select All")
            //{
            //    condition = condition + " and crtype='" + ddltype.SelectedItem.Text + "' ";
            //}
            if (ddlnames.SelectedItem.Text != "Select All")
            {               
                condition = condition + " and Assignto='" + ddlnames.SelectedItem.Text + "'";
            }
            if (ddlfillstatus.SelectedItem.Text != "Select All")
            {
                condition = condition + " and status_name='" + ddlfillstatus.SelectedItem.Text + "'";
            }
            ViewState["condition"] = condition;
            s1 = ViewState["condition"].ToString();

            if (ddltype.SelectedItem.Text == "CR")
            {
                //da = new SqlDataAdapter("select Cr_IssueNo,c.crno as NO,c.crtype,REPLACE(CONVERT(VARCHAR(20),e.AssignDate, 100), ' ', '-')Date,sum(isnull(e.brdtime,0)+isnull(e.tdtime,0)+isnull(e.developmenttime,0)) as Hours,e.assignto,c.description,e.remarks comment,s.status_name from esselcr c join  EstimatedHours e on e.cr_issueno=c.crno join esselcr_status s on s.status=e.status  where  e.id>0 " + s1 + " group by Cr_IssueNo,c.crno,c.crtype,Date,Hours,e.assignto,c.description,e.remarks,s.status_name ", con);
                da = new SqlDataAdapter("select Cr_IssueNo,c.crno as NO,c.crtype,REPLACE(CONVERT(VARCHAR(20),e.AssignDate, 100), ' ', '-')Date,sum(isnull(e.brdtime,0)+isnull(e.tdtime,0)+isnull(e.developmenttime,0)) as developmenttime,e.assignto,c.description,e.remarks comment,s.status_name,e.Estmationverify_date,e.QualityTransferdate,e.QualityApproveddate,e.ProductionTrasferdate from esselcr c join  EstimatedHours e on e.cr_issueno=c.crno join esselcr_status s on s.status=e.status  where e.id>0  " + s1 + " group by Cr_IssueNo,c.crno,c.crtype,e.AssignDate,e.assignto,c.description,e.remarks,s.status_name,e.QualityTransferdate,e.QualityApproveddate,e.ProductionTrasferdate,e.Estmationverify_date", con);

            }
            else if (ddltype.SelectedItem.Text == "ISSUE")
            {
                da = new SqlDataAdapter("select Cr_IssueNo,c.issueno as NO,c.crtype,REPLACE(CONVERT(VARCHAR(20),e.AssignDate, 100), ' ', '-')Date,sum(isnull(e.brdtime,0)+isnull(e.tdtime,0)+isnull(e.developmenttime,0)) as developmenttime,e.assignto,c.description,e.remarks comment,s.status_name,null as Estmationverify_date,e.QualityTransferdate,e.QualityApproveddate,e.ProductionTrasferdate from esselissue c join  EstimatedHours e on e.cr_issueno=c.issueno join esselcr_status s on s.status=e.status  where e.id>0  " + s1 + " group by Cr_IssueNo,c.issueno,c.crtype,e.AssignDate,e.assignto,c.description,e.remarks,s.status_name,e.QualityTransferdate,e.QualityApproveddate,e.ProductionTrasferdate", con);
                //da = new SqlDataAdapter("select Cr_IssueNo,c.issueno as NO,c.crtype,REPLACE(CONVERT(VARCHAR(20),e.AssignDate, 100), ' ', '-')Date,e.brdtime,e.tdtime,e.developmenttime,e.assignto,c.description,e.remarks comment,s.status_name,e.QualityTransferdate,e.QualityApproveddate,e.ProductionTrasferdate from esselissue c join  EstimatedHours e on e.cr_issueno=c.issueno join esselcr_status s on s.status=e.status  where e.id>0  " + s1 + " ", con);

            }
           
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                //ViewState["no"] = ds.Tables["fill"].Rows[0]["NO"].ToString();
                //ViewState["description"] = ds.Tables["fill"].Rows[0]["description"].ToString();
                this.upd.Update();
                gridControl.DataSource = ds.Tables["fill"];
                gridControl.DataBind();
            }
            else
            {
                this.upd.Update();
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
    public void sendmail()
    {
        try
        {
            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            //Msg.From = new MailAddress(f.ToString());
            Msg.From = new MailAddress(f, "Essel-IT Support");
            // Recipient e-mail address.
            Msg.To.Add(t.ToString());
            Msg.To.Add(t1.ToString());
            Msg.CC.Add(s.ToString());

            // Msg.Body = "Hi ,\n\n New " + ddltype.SelectedItem.Text + "," + ViewState["NO"].ToString() + " Approved by " + Session["roles"].ToString() + " \n\n Description : " + ViewState["description"].ToString() + " \n\n   ";
            if (ddlstatus.SelectedItem.Text == "Under UAT")
            {
                Msg.Subject = " " + ViewState["no"].ToString() + " " + ddltype.SelectedItem.Text + " " + ddlstatus.SelectedItem.Text + "";
                Msg.Body = "Hi ,\n\n  " + ddltype.SelectedItem.Text + "," + ViewState["no"].ToString() + " " + ddlstatus.SelectedItem.Text + "   \n\n Description : " + ViewState["description"].ToString() + " \n\n  Please Test in quality and give approval " + "\n\n\n Thanks \n Essel Projects.";
            }
            else if (ddlstatus.SelectedItem.Text == "Transfer To Production")
            {
                Msg.Subject = "" + ddltype.SelectedItem.Text + " " + ViewState["no"].ToString() + " " + ddlstatus.SelectedItem.Text + "  ";
                Msg.Body = "Hi ,\n\n  " + ddltype.SelectedItem.Text + "," + ViewState["no"].ToString() + " " + ddlstatus.SelectedItem.Text + "   \n\n Description : " + ViewState["description"].ToString() + " " + "\n\n\n Thanks \n SLTouch.";
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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        poppo.Show();
        //int id = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex]["id"].ToString());
        //ViewState["no"] = GridView1.Rows[e.NewEditIndex].Cells[0].Text;
        //ViewState["id"] = id;
        statusname();
        lnlno.Text = GridView1.DataKeys[e.NewEditIndex]["Cr_IssueNo"].ToString();
        ViewState["no"] = GridView1.DataKeys[e.NewEditIndex]["Cr_IssueNo"].ToString();
        filldesc();
        txtremarks.Text = "";
        txttime.Text = "";

        if ((ddlfillstatus.SelectedItem.Text == "Task Completed" || ddlfillstatus.SelectedItem.Text == "UAT Approved" || ddlfillstatus.SelectedItem.Text == "Under UAT") && ddltype.SelectedItem.Text == "ISSUE")
            {
                time.Visible = true;
            }
            else
            {
                time.Visible = false;
            }
        
         
    }

    public void filldesc()
    {
        DataSet ds1 = new DataSet();
        ds1.Clear();
        if (ddltype.SelectedItem.Text == "CR")
        {
            da = new SqlDataAdapter("select description from esselcr where crno='" + ViewState["no"].ToString() + "' ", con);
        }
        else
        {
            da = new SqlDataAdapter("select description from esselissue where issueno='" + ViewState["no"].ToString() + "' ", con);

        }
        da.Fill(ds1, "fill");
        if (ds1.Tables["fill"].Rows.Count > 0)
        {
            ViewState["description"] = ds1.Tables["fill"].Rows[0]["description"].ToString();
        }
    }
    public void statusname()
    {
        try
        {
            ddlstatus.Items.Clear();
            da = new SqlDataAdapter("select status,status_name from esselcr_status where status in ('5','3','6','11','12','13','15') and status <> '" + ddlfillstatus.SelectedValue + "'    order by status_name", con);
            da.Fill(ds, "fillstatus");
            ddlstatus.DataTextField = "status_name";
            ddlstatus.DataValueField = "status";
            ddlstatus.DataSource = ds.Tables["fillstatus"];
            ddlstatus.DataBind();
            ddlstatus.Items.Insert(0, "Status");
            
            
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
            ddlstatus.Items.Clear();
            da = new SqlDataAdapter("select status,status_name from esselcr_status where status in ('5','4','3','6','11','12','13','14','15','16')  order by status_name", con);
            da.Fill(ds, "status");
            ddlfillstatus.DataTextField = "status_name";
            ddlfillstatus.DataValueField = "status";
            ddlfillstatus.DataSource = ds.Tables["status"];
            ddlfillstatus.DataBind();
            ddlfillstatus.Items.Insert(0, "Status");
            ddlfillstatus.Items.Insert(1, "Select All");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnstatus_Click(object sender, EventArgs e)
    {
        try
        {
            cmd = new SqlCommand("sp_ChangeStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddlstatus.SelectedValue;
            cmd.Parameters.Add("@issuetime", SqlDbType.VarChar).Value = txttime.Text;
            cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = txtremarks.Text;
            cmd.Parameters.Add("@no", SqlDbType.VarChar).Value = ViewState["no"].ToString();
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            if (msg == "Sucessfull")
            {
                poppo.Hide();
                JavaScript.UPAlert(Page, msg);
                if (ddlstatus.SelectedItem.Text == "Under UAT" || ddlstatus.SelectedItem.Text == "Transfer To Production")
                {
                    sendmail();
                }
                fillgrid(s1,GridView1);
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlfillstatus.SelectedItem.Text == "Select All")
                {
                    GridView1.Columns[8].Visible = false;
                }
                else
                {
                    GridView1.Columns[8].Visible = true;
                }
               
            }
          

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (Session["roles"].ToString() == "SuperAdmin")
            //    {
            //        GridView1.Columns[8].Visible = false;
            //        if (ddltype.SelectedItem.Text == "ISSUE")
            //        {
            //            GridView1.Columns[3].Visible = false;
            //            GridView1.Columns[4].Visible = false;
            //            GridView1.Columns[5].Visible = false;
            //        }
            //        else
            //        {
            //            GridView1.Columns[3].Visible = true;
            //            GridView1.Columns[4].Visible = true;
            //            GridView1.Columns[5].Visible = true;
                       
            //        }
            //    }
            //    else
            //    {
                   
            //        if (ddlfillstatus.SelectedItem.Text != "Approved In Production")
            //        {
            //            GridView1.Columns[8].Visible = false;
            //            if (ddltype.SelectedItem.Text == "ISSUE")
            //            {
            //                GridView1.Columns[3].Visible = false;
            //                GridView1.Columns[4].Visible = false;
            //                GridView1.Columns[5].Visible = false;
            //                GridView1.Columns[8].Visible = true;
            //            }
            //            else
            //            {
            //                GridView1.Columns[3].Visible = true;
            //                GridView1.Columns[4].Visible = true;
            //                GridView1.Columns[5].Visible = true;
            //                GridView1.Columns[8].Visible = false;

            //            }

            //        }
            //        else
            //        {
            //            GridView1.Columns[8].Visible = false;
            //            if (ddltype.SelectedItem.Text == "ISSUE")
            //            {
            //                GridView1.Columns[3].Visible = false;
            //                GridView1.Columns[4].Visible = false;
            //                GridView1.Columns[5].Visible = false;
            //            }
            //            else
            //            {
            //                GridView1.Columns[3].Visible = true;
            //                GridView1.Columns[4].Visible = true;
            //                GridView1.Columns[5].Visible = true;

            //            }

            //        }
            //    }
        //}
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;

            string behaviorID = "indus_" + e.Row.RowIndex;
            pce.BehaviorID = behaviorID;

            Label lblno = (Label)e.Row.FindControl("lblno");

            string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
            string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

            lblno.Attributes.Add("onmouseover", OnMouseOverScript);
            lblno.Attributes.Add("onmouseout", OnMouseOutScript);
        }
    }
 
    [WebMethod]
    public static string GetDynamicContent(string contextKey)
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
      //  string constr = "Server=TestServer;Database=SampleDatabase;uid=test;pwd=test;";
        string query = "select assignto,CONVERT(VARCHAR(20),TaskStartdate,100)TaskStartdate,CONVERT(VARCHAR(20),TaskEnddate,100)TaskEnddate,CONVERT(VARCHAR(20),QualityTransferdate,100)QualityTransferdate,CONVERT(VARCHAR(20),QualityApproveddate,100)QualityApproveddate,CONVERT(VARCHAR(20),ProductionTrasferdate,100)ProductionTrasferdate,CONVERT(VARCHAR(20),ProductionApproveddate,100)ProductionApproveddate from EstimatedHours where cr_issueno = '" + contextKey + "'";

        SqlDataAdapter da = new SqlDataAdapter(query, constr);
        DataTable table = new DataTable();

        da.Fill(table);

        StringBuilder b = new StringBuilder();

        b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ");
        b.Append("width:700px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
        b.Append("<tr><td colspan='7' style='background-color:#336699; color:White;'>");
        b.Append("<b>Details</b>"); b.Append("</td></tr>");
        b.Append("<tr style=''><td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>SupportUser</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>Start Date</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>End Date</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>UnderUAT Date</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>UAT Approved Date</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;border-right:solid 1px black;'><b>Production Date</b></td>");
        b.Append("<td style='width:100px;border-bottom:solid 1px black;'><b>Production Approved Date</b></td></tr>");
        b.Append("<tr>");
        b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[0]["assignto"].ToString() + "</td>");
        b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[0]["TaskStartdate"].ToString() + "</td>");
        b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[0]["TaskEnddate"].ToString() + "</td>");
        b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[0]["QualityTransferdate"].ToString() + "</td>");
        b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[0]["QualityApproveddate"].ToString() + "</td>");
        b.Append("<td style='border-right:solid 1px black;'>" + table.Rows[0]["ProductionTrasferdate"].ToString() + "</td>");
        b.Append("<td>" + table.Rows[0]["ProductionApproveddate"].ToString() + "</td>");
        b.Append("</tr>");
        b.Append("</table>");


        //b.Append("<tr>Start Date:</tr>");
        //b.Append("<tr>End Date:</tr>");
        //b.Append("<tr>UnderUAT Date:</tr>");
        //b.Append("<tr>UAT Approved Date:</tr>");
        //b.Append("<tr>Production Date:</tr>");
        //b.Append("<tr>Production Approved Date:</tr>");
        //b.Append("<tr>$" + table.Rows[0]["assignto"].ToString() + "</tr>");
        //b.Append("<tr>" + table.Rows[0]["TaskStartdate"].ToString() + "</tr>");
        //b.Append("<tr>" + table.Rows[0]["TaskEnddate"].ToString() + "</tr>");
        //b.Append("<tr>$" + table.Rows[0]["QualityTransferdate"].ToString() + "</tr>");
        //b.Append("<tr>" + table.Rows[0]["QualityApproveddate"].ToString() + "</tr>");
        //b.Append("<tr>" + table.Rows[0]["ProductionTrasferdate"].ToString() + "</tr>");
        //b.Append("<tr>$" + table.Rows[0]["ProductionApproveddate"].ToString() + "</tr>");

        return b.ToString();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
   

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView2.AllowPaging = false;
        fillgrid(s1,GridView2);
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Report"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView2.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
}