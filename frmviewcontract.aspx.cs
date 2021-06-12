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

public partial class Admin_frmviewcontract : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd=new SqlCommand();
    SqlDataReader dr;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 28);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            btnView.Attributes.Add("onclick", "return txtbox()");
        }
        panel1.Visible = false;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
            try
            {
                panel1.Visible = true;

            if (ddlpo.SelectedValue != "0")
            {
                da = new SqlDataAdapter("select project_name,client_name,division,c.cc_code,start_date,c.end_date,c.natureofjob,c.projectmanager_name,projectmanager_contactno,customer_name,po_basicvalue,po_servicetaxvalue,po_totalvalue,amdbasic,amdtax,amdtotal,po.po_no,po_basicvalue+isnull(amdbasic,0) as totalbasic,po_servicetaxvalue+isnull(amdtax,0) as totaltax,po_totalvalue+isnull(amdtotal,0) as  total from contract c join po on c.po_no=po.po_no left outer join (select Sum(po_amendedbasicvalue)as amdbasic ,Sum(po_amendedservicetaxvalue)as amdtax ,Sum(po_amendedtotalvalue)as amdtotal,po_no from po_amended GROUP BY po_no) amd on po.po_no=amd.po_no where po.po_no='" + ddlpo.SelectedValue + "'", con);

                da.Fill(ds, "vcontract");
                if (ds.Tables["vcontract"].Rows.Count >= 1)
                {

                    pname.Text = ds.Tables["vcontract"].Rows[0].ItemArray[0].ToString();
                    clname.Text = ds.Tables["vcontract"].Rows[0].ItemArray[1].ToString();
                    div.Text = ds.Tables["vcontract"].Rows[0].ItemArray[2].ToString();
                    lblcccode.Text = ds.Tables["vcontract"].Rows[0].ItemArray[3].ToString();
                    sdate.Text = ds.Tables["vcontract"].Rows[0].ItemArray[4].ToString();
                    edate.Text = ds.Tables["vcontract"].Rows[0].ItemArray[5].ToString();
                    noj.Text = ds.Tables["vcontract"].Rows[0].ItemArray[6].ToString();
                    pmname.Text = ds.Tables["vcontract"].Rows[0].ItemArray[7].ToString();
                    pmcontact.Text = ds.Tables["vcontract"].Rows[0].ItemArray[8].ToString();
                    custname.Text = ds.Tables["vcontract"].Rows[0].ItemArray[9].ToString();
                    pobvalue.Text = ds.Tables["vcontract"].Rows[0].ItemArray[10].ToString().Replace(".0000", ".00");
                    poaservicetaxval.Text = ds.Tables["vcontract"].Rows[0].ItemArray[11].ToString().Replace(".0000", ".00");
                    pototal.Text = ds.Tables["vcontract"].Rows[0].ItemArray[12].ToString().Replace(".0000", ".00");
                    totbvalue.Text = ds.Tables["vcontract"].Rows[0].ItemArray[17].ToString().Replace(".0000", ".00");
                    tstvalue.Text = ds.Tables["vcontract"].Rows[0].ItemArray[18].ToString().Replace(".0000", ".00");
                    total.Text = ds.Tables["vcontract"].Rows[0].ItemArray[19].ToString().Replace(".0000", ".00");
                }
                  
            
                string str2 = "";
                str2 = str2 + "select po_amendedno,po_amendedbasicvalue,po_amendedservicetaxvalue,po_amendedtotalvalue,";
                str2 = str2 + "po_ammendedate from po_amended where po_no = '" + ddlpo.SelectedValue + "'";
                da = new SqlDataAdapter(str2, con);
                da.Fill(ds, "amended");
                //12,13,14,15;
                DataTable dtAmend = new DataTable();
                dtAmend = ds.Tables["amended"];
                //string dsStr = "ds.Tables["+"vcontract"+"].Rows[0].ItemArray[";
                //if (dtAmend.Rows.Count==0)
                //{
                //    p1.Visible = false;
                
                //}
                //else
                //{
                p1.Controls.Add(new LiteralControl("<table  width='700px' ><tr><th colspan=''>Amendment Form</th></tr><tr><td >"));
                if (dtAmend.Rows.Count >= 1)
                {
                    p1.Visible = true;
                    //p1.Controls.Add(new LiteralControl("<table ><tr><td>"));

                    for (int i = 0; i < dtAmend.Rows.Count; i++)
                    {
                        p1.Controls.Add(new LiteralControl("<table class='estbl' width='700'><tr style='border:1px solid #000'>"));
                        for (int j = 0; j < dtAmend.Columns.Count; j++)
                        {
                            Label l1 = new Label();
                            Label l2 = new Label();
                            l1.Text = dtAmend.Rows[i].ItemArray[j].ToString().Replace(".0000", ".00");
                            l1.CssClass = "eslbltext";
                            if (j == 0)
                            {
                                l2.Text = "Amended No";
                                l2.CssClass = "eslbl";
                            }
                            else if (j == 1)
                            {
                                l2.Text = "Basic Value ";
                                l2.CssClass = "eslbl";
                            }
                            else if (j == 2)
                            {
                                l2.Text = "Service Tax";
                                l2.CssClass = "eslbl";
                            }
                            else if (j == 3)
                            {
                                l2.Text = "Total Value";
                                l2.CssClass = "eslbl";
                            }
                            else if (j == 4)
                            {
                                //p1.Controls.Add(new LiteralControl("<td colspan='1'>"));
                                l2.Text = "Date";
                                
                                l2.CssClass = "eslbl";
                                p1.Controls.Add(new LiteralControl("<td colspan='2'>"));
                                p1.Controls.Add(new LiteralControl("</td>"));
                                
                            }
                            
                            p1.Controls.Add(new LiteralControl("<td width='88'  align='left'>"));
                            p1.Controls.Add(l2);
                            p1.Controls.Add(new LiteralControl("</td >"));
                            p1.Controls.Add(new LiteralControl("<td width='180'>"));
                            p1.Controls.Add(l1);
                            p1.Controls.Add(new LiteralControl("</td>"));
                            if (((j + 1) % 2) == 0 & j != 0)
                            {
                                p1.Controls.Add(new LiteralControl("</tr><tr>"));
                            }
                        }
                    }
                    p1.Controls.Add(new LiteralControl("</td></tr></table>"));
                }
                else
                {
                    p1.Visible = false;
                }
                p1.Controls.Add(new LiteralControl("</td></tr></table>"));
                //}
            }
        }
            catch (Exception ex)
            {
                Utilities.CatchException(ex);
                JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
            }
            finally
            {
                con.Close();
            }

       
    }
    public void showalert(string message)
    {
        Label mylabel = new Label();
        mylabel.Text = "<script language='javascript'>window.alert('" + message + "')</script>";
        Page.Controls.Add(mylabel);
    }
    
    protected void ddlCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("select po_no from po where cc_code='"+ddlCC.SelectedValue+"' union select po_no from contract where cc_code='"+ddlCC.SelectedValue+"'",con);
        da.Fill(ds,"pono");
        if (ds.Tables["pono"].Rows.Count > 0) 
        {
            ddlpo.DataTextField = "po_no";
            ddlpo.DataValueField = "po_no";
            ddlpo.DataSource = ds.Tables["pono"];
            ddlpo.DataBind();
            ddlpo.Items.Insert(0, "select po");
        }
    }
}
