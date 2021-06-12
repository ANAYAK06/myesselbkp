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

public partial class WareHouseHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("WareHouse");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            scroll();
            if (Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "StoreKeeper" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Central Store Keeper")
            {
                fillhome();
            }
        }
    }
    public void fillhome()
    {
        try
        {

            ////select ti.cc_Code,ti.recieved_cc,ti.indent_no from [Transfer Info] ti join [Items Transfer] it on ti.Ref_no=it.Ref_no where ti.Status='1' and it.Type='1'


           if (Session["roles"].ToString() == "Chief Material Controller")
               da = new SqlDataAdapter("Select distinct ('Items Issued from '+ ti.cc_code+' to '+ recieved_cc)Remarks,'2'as [status] from [transfer info]ti left outer join [items transfer]it on ti.ref_no=it.ref_no where status='1A' and [type]='1' union all Select distinct ('Items Transfer From '+ ti.cc_code +' To '+recieved_cc)Remarks,'3'as [status] from [Transfer info]Ti join [Items Transfer]It on ti.ref_no=it.ref_no where status in ('1A','3B') and type='2'", con);
            else if (Session["roles"].ToString() == "StoreKeeper")
               da = new SqlDataAdapter("Select distinct ('Items Issued from '+ ti.cc_code+' to '+ recieved_cc)Remarks,'1'as [status] from [transfer info]ti left outer join [items transfer]it on ti.ref_no=it.ref_no where status='2' and [type]='1' and recieved_cc='" + Session["cc_code"].ToString() + "' union all Select distinct ('Items Transfer From '+ ti.cc_code +' To '+recieved_cc)Remarks,'3'as [status] from [Transfer info]Ti join [Items Transfer]It on ti.ref_no=it.ref_no where status='2' and type='2' and ti.cc_code='" + Session["cc_code"].ToString() + "' union all Select distinct ('Items Transfer From '+ ti.cc_code +' To '+recieved_cc)Remarks,'1'as [status] from [Transfer info]Ti join [Items Transfer]It on ti.ref_no=it.ref_no where status='4' and type='2' and recieved_cc='" + Session["cc_code"].ToString() + "'", con);
           else if (Session["roles"].ToString() == "Central Store Keeper")
               da = new SqlDataAdapter("Select distinct ('Items Transfer From '+ ti.cc_code +' To '+recieved_cc)Remarks,'3'as [status] from [Transfer info]Ti join [Items Transfer]It on ti.ref_no=it.ref_no where status='3A' and type='2' and recieved_cc='CC-33'", con);
           else if (Session["roles"].ToString() == "Project Manager")
               da = new SqlDataAdapter("Select distinct ('Items Issued from '+ ti.cc_code+' to '+ recieved_cc)Remarks,'2'as [status] from [transfer info]ti left outer join [items transfer]it on ti.ref_no=it.ref_no where status='3' and [type]='1' and recieved_cc in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') union all Select distinct ('Items Transfer From '+ ti.cc_code +' To '+recieved_cc)Remarks,'3'as [status] from [Transfer info]Ti join [Items Transfer]It on ti.ref_no=it.ref_no where status='3' and type='2' and ti.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') union all Select distinct ('Items Transfer From '+ ti.cc_code +' To '+recieved_cc)Remarks,'3'as [status] from [Transfer info]Ti join [Items Transfer]It on ti.ref_no=it.ref_no where status='5' and type='2' and recieved_cc in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);

            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkScrap");
            HiddenField hf1 = (HiddenField)e.Row.FindControl("hf1");
            lnkbtn.Attributes.Add("onclick", "return Getstatus('" + hf1.Value + "');");

        }
    }

    public void scroll()
    {
        if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
        {
            da = new SqlDataAdapter("SELECT TOP 1 id,modified_date from  closedcost_center where status in('3') and status not in('Rejected') and close_type in('TemporaryClosed','Reopen') ORDER by id desc", con);
            da.Fill(ds, "checking");
            if (ds.Tables["checking"].Rows.Count > 0)
            {
                DateTime _date = Convert.ToDateTime(ds.Tables["checking"].Rows[0].ItemArray[1].ToString());
                _date = _date.AddDays(5);
                if (DateTime.Now <= _date)
                {
                    string strSql = "SELECT close_type,status,cc_code,REPLACE(CONVERT(NVARCHAR,modified_Date, 106), ' ', '-')as modified_Date FROM closedcost_center where id='" + ds.Tables["checking"].Rows[0].ItemArray[0].ToString() + "'";
                    string strScrolling = "";
                    HtmlTableCell cellScrolling = new HtmlTableCell();
                    SqlCommand myComd = new SqlCommand(strSql, con);
                    SqlDataReader sqlRdr;
                    con.Open();
                    sqlRdr = myComd.ExecuteReader();
                    strScrolling = "<Marquee OnMouseOver='this.stop();' OnMouseOut='this.start();' direction='left' scrollamount='8' bgcolor='#FF9061' width='100%'>";
                    while (sqlRdr.Read())
                    {
                        strScrolling = strScrolling + " " + sqlRdr.GetValue(2).ToString() + " Store was " + sqlRdr.GetValue(0).ToString() + " on Date:-  " + sqlRdr.GetValue(3).ToString() + "<br/>";
                    }
                    strScrolling = strScrolling + "</Marquee>";
                    sqlRdr.Close();
                    if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
                    {
                    lblscroll.Text = strScrolling;
                        lblscroll.Visible = true;
                    }
                    else
                    {
                        lblscroll.Visible = false;
                    }
                }
            }
            da = new SqlDataAdapter("SELECT TOP 1 id,modified_date from  closedcost_center where status in('3') and status not in('Rejected') and close_type in('PermanentClosed') ORDER by id desc", con);
            da.Fill(ds, "checkings");
            if (ds.Tables["checkings"].Rows.Count > 0)
            {
                 DateTime _date = Convert.ToDateTime(ds.Tables["checkings"].Rows[0].ItemArray[1].ToString());
                _date = _date.AddDays(5);
                if (DateTime.Now <= _date)
                {
                    string strSql = "SELECT close_type,status,cc_code,REPLACE(CONVERT(NVARCHAR,modified_Date, 106), ' ', '-')as modified_Date FROM closedcost_center where id='" + ds.Tables["checkings"].Rows[0].ItemArray[0].ToString() + "'";
                    string strScrolling = "";
                    HtmlTableCell cellScrolling = new HtmlTableCell();
                    SqlCommand myComd = new SqlCommand(strSql, con);
                    SqlDataReader sqlRdr;
                    con.Open();
                    sqlRdr = myComd.ExecuteReader();
                    strScrolling = "<Marquee OnMouseOver='this.stop();' OnMouseOut='this.start();' direction='left' scrollamount='8' bgcolor='#FF9061' width='100%'>";
                    while (sqlRdr.Read())
                    {
                        strScrolling = strScrolling + " " + sqlRdr.GetValue(2).ToString() + " Store was " + sqlRdr.GetValue(0).ToString() + " on Date:-  " + sqlRdr.GetValue(3).ToString() + "<br/>";
                    }
                    strScrolling = strScrolling + "</Marquee>";
                    sqlRdr.Close();
                    if (Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin")
                    {
                    lblscroll.Text = strScrolling;
                        lblscroll.Visible = true;
                    }
                    else
                    {
                        lblscroll.Visible = false;
                    }
                }
            }
          
        }
       
    }
    
}
