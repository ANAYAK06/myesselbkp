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
using System.Collections.Generic;

public partial class TrackRecords : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);

    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
     
    protected void Page_Load(object sender, EventArgs e)
    {
         Essel childmaster = (Essel)this.Master;
         HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("WareHouse");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        //esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 2);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            clear();
        }
    }

    protected void ddlsearchopt_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        clear();
    }
   
    protected void BtnGo_Click(object sender, EventArgs e)   
    {

        tblinvoice.Visible = false;
        tblmrr.Visible = false;
        porpt.Visible = false;
        indentrpt.Visible = false;
        
        string Authorised = "";
        if (Session["roles"].ToString() == "StoreKeeper")
        {            
            if (ddlsearchopt.SelectedItem.Text == "Indent No" || ddlsearchopt.SelectedItem.Text == "PO Number")
            {
                if (txtSearch.Text.Split('/')[0] != "CCC")
                {
                    if (Session["cc_code"].ToString().Split('/')[0] != txtSearch.Text.Split('/')[0])
                        Authorised = "false";
                    else
                        Authorised = "True"; 
                }
                else
                     Authorised = "false";               
            }
            if (ddlsearchopt.SelectedItem.Text == "MRR Number")
            {

                da = new SqlDataAdapter("SELECT UPPER(po_no) FROM mr_report where mrr_no='" + txtSearch.Text + "'", con);
                da.Fill(ds, "pono");
                if (ds.Tables["pono"].Rows.Count > 0)
                {
                    string CCCode = ds.Tables["pono"].Rows[0][0].ToString();
                    //if (CCCode.Substring(0, 5) != Session["cc_code"].ToString().Substring(0, 5))
                    if (CCCode.Split('/')[0] != Session["cc_code"].ToString())
                        Authorised = "false";
                    else
                        Authorised = "True";
                }
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Invalid User", "TrackRecords.aspx");
                    txtSearch.Text = String.Empty;                    
                }
            }
            if (ddlsearchopt.SelectedItem.Text == "Invoice Number")
            {

                da = new SqlDataAdapter("SELECT po_no FROM pending_invoice where invoiceno='" + txtSearch.Text + "' UNION SELECT po_no FROM temp_pending_invoice where invoiceno='" + txtSearch.Text + "' ", con);
                da.Fill(ds, "invoice");
                if (ds.Tables["invoice"].Rows.Count > 0)
                {
                    string CCCode = ds.Tables["invoice"].Rows[0][0].ToString();
                    //if (CCCode.Substring(0, 5) != Session["cc_code"].ToString().Substring(0, 5))
                    if (CCCode.Split('/')[0] != Session["cc_code"].ToString())
                        Authorised = "false";
                    else
                        Authorised = "True";
                }
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Invalid User", "TrackRecords.aspx");
                    txtSearch.Text = String.Empty;                  
                }
            }
            if (ddlsearchopt.SelectedItem.Text == "Reference Number")
            {
                da = new SqlDataAdapter("SELECT indent_no,cc_code,recieved_cc FROM [transfer info] where ref_no='" + txtSearch.Text + "'", con);
                da.Fill(ds, "Transfer");
                string indentno = ds.Tables["Transfer"].Rows[0][0].ToString();
                string cc_code = ds.Tables["Transfer"].Rows[0][1].ToString();
                string received_cc = ds.Tables["Transfer"].Rows[0][2].ToString();
                if (ds.Tables["Transfer"].Rows.Count > 0)
                {

                    if (indentno != "NULL" && indentno != "")
                    {
                        //if (received_cc == Session["cc_code"].ToString().Substring(0, 5))
                        if (received_cc == Session["cc_code"].ToString())
                            Authorised = "True";
                        else
                            Authorised = "false";
                    }
                    else
                    {
                        //if (cc_code == Session["cc_code"].ToString().Substring(0, 5))
                        if (cc_code == Session["cc_code"].ToString())
                            Authorised = "True";
                        else
                            Authorised = "false";
                    }
                }
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Invalid User", "TrackRecords.aspx");
                    txtSearch.Text = String.Empty;             
                }
            }

        }
        else if(Session["roles"].ToString() == "Project Manager")
        {
            string user = Session["user"].ToString();
            if (ddlsearchopt.SelectedItem.Text == "Indent No" || ddlsearchopt.SelectedItem.Text == "PO Number")
            {

                //if (txtSearch.Text.Substring(0, 3) != "CCC")
                if (txtSearch.Text.Split('/')[0] != "CCC")
                {
                    //string cc_code = txtSearch.Text.Substring(0, 5);
                    string cc_code = txtSearch.Text.Split('/')[0];
                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM cc_user where cc_code='" + cc_code + "' AND USER_NAME='" + user + "'", con);
                    da.Fill(ds, "CC_code");
                    if (ds.Tables["CC_code"].Rows.Count == 1)
                        Authorised = "True";
                    else
                        Authorised = "false";
                }
                else
                    Authorised = "false";     
            }
            if (ddlsearchopt.SelectedItem.Text == "MRR Number")
            {

                da = new SqlDataAdapter("SELECT UPPER(po_no) FROM mr_report where mrr_no='" + txtSearch.Text + "'", con);
                da.Fill(ds, "pono");
                string CCCode = ds.Tables["pono"].Rows[0][0].ToString();
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM cc_user where cc_code='" + CCCode.Split('/')[0] + "' AND USER_NAME='" + user + "'", con);
                da.Fill(ds, "CC_code");
                if (ds.Tables["CC_code"].Rows.Count == 1)
                    Authorised = "True";
                else
                    Authorised = "false";
            }
            if (ddlsearchopt.SelectedItem.Text == "Invoice Number")
            {

                da = new SqlDataAdapter("SELECT po_no FROM pending_invoice where invoiceno='" + txtSearch.Text + "' UNION SELECT po_no FROM temp_pending_invoice where invoiceno='" + txtSearch.Text + "' ", con);
                da.Fill(ds, "invoice");
                string CCCode = ds.Tables["invoice"].Rows[0][0].ToString();
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM cc_user where cc_code='" + CCCode.Split('/')[0] + "' AND USER_NAME='" + user + "'", con);
                da.Fill(ds, "CC_code");
                if (ds.Tables["CC_code"].Rows.Count == 1)
                    Authorised = "True";
                else
                    Authorised = "false";
            }
            if (ddlsearchopt.SelectedItem.Text == "Reference Number")
            {
                da = new SqlDataAdapter("SELECT indent_no,cc_code,recieved_cc FROM [transfer info] where ref_no='" + txtSearch.Text + "'", con);
                da.Fill(ds, "Transfer");
                string indentno = ds.Tables["Transfer"].Rows[0][0].ToString();
                string cc_code = ds.Tables["Transfer"].Rows[0][1].ToString();
                string received_cc = ds.Tables["Transfer"].Rows[0][2].ToString();

                if (indentno != "NULL" && indentno != "")
                {                    
                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM cc_user where cc_code='" + received_cc + "' AND USER_NAME='" + user + "'", con);
                    da.Fill(ds, "CC_code");
                    if (ds.Tables["CC_code"].Rows.Count == 1)
                        Authorised = "True";
                    else
                        Authorised = "false";
                }
                else
                {
                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM cc_user where cc_code='" + cc_code + "' AND USER_NAME='" + user + "'", con);
                    da.Fill(ds, "CC_code");
                    if (ds.Tables["CC_code"].Rows.Count == 1)
                        Authorised = "True";
                    else
                        Authorised = "false";
                }
            }
 
        }
        else
            Authorised = "True";

        if (Authorised == "True")
        {
            if (ddlsearchopt.SelectedItem.Text == "Indent No")
            {
                DataTable dt = new DataTable();
                DataSet ds1 = new DataSet();
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM indents where indent_no='" + txtSearch.Text + "'", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)
                {
                    da = new SqlDataAdapter("SELECT status,indenttype FROM indents where indent_no='" + txtSearch.Text + "'", con);
                    da.Fill(ds, "status");
                    string hfstatus = ds.Tables["status"].Rows[0][0].ToString();
                    string hfindentype = ds.Tables["status"].Rows[0][1].ToString();
                    if (hfstatus == "1")
                    {
                        Label11.Text = "Indent Waiting at Project Manager Level";
                        getdetails();

                        tblmrr.Visible = false;
                        tblinvoice.Visible = false;
                        porpt.Visible = false;
                    }
                    else if (hfstatus == "2")
                    {
                        Label11.Text = "Indent Waiting at CSK Level";
                        getdetails();
                        tblmrr.Visible = false;
                        tblinvoice.Visible = false;
                        porpt.Visible = false;
                    }
                    else if (hfstatus == "2A")
                    {
                        Label11.Text = "Indent Waiting at CSK Level";
                        gettransferdetails();
                        tblmrr.Visible = false;
                        tblinvoice.Visible = false;
                        porpt.Visible = false;
                    }
                    else if (hfstatus == "3")
                    {
                        Label11.Text = "part of indent is at purchase manager Level";
                        getpartialpurchasedetails();
                    }
                    else if (hfstatus == "4")
                    {
                        Label11.Text = "Indent Waiting at Purchase Manager Level";
                        if (hfindentype == "Partially Purchase")
                            getpartialpurchasedetails();
                        else if (hfindentype == "Full Issue")                       
                            gettransferdetails();
                        else
                            getdetails();
                        tblmrr.Visible = false;
                        tblinvoice.Visible = false;
                        porpt.Visible = false;
                    }
                    else if (hfstatus == "5")
                    {
                        Label11.Text = "Indent Waiting at CMC Level";
                        if (hfindentype == "Partially Purchase")
                            getpartialpurchasedetails();
                        else
                            getdetails();
                        tblmrr.Visible = false;
                        tblinvoice.Visible = false;
                        porpt.Visible = false;
                    }
                    else if (hfstatus == "6A")
                    {
                        Label11.Text = "Indent Waiting at SA Level";
                        if (hfindentype == "Partially Purchase")
                            getpartialpurchasedetails();
                        else
                            getdetails();
                        tblmrr.Visible = false;
                        tblinvoice.Visible = false;
                        porpt.Visible = false;
                    }
                    else if (hfstatus == "6" || hfstatus == "7")
                    {
                        if (hfindentype == "Full Purchase" || hfindentype == "Purchase")                                           
                            getdetails();
                        else if (hfindentype == "Partially Purchase")
                            getpartialpurchasedetails();
                        else                 
                            gettransferdetails();                     
                    }
                    else if (hfstatus == "Closed")
                    {
                        Label11.Text = "Indent closed";
                        if (hfindentype == "Partially Purchase")
                            getpartialpurchasedetails();
                        else
                            getdetails();
                        tblmrr.Visible = false;
                        tblinvoice.Visible = false;
                        porpt.Visible = false;
                    }

                }
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Invalid IndentNo", "TrackRecords.aspx");
                    txtSearch.Text = String.Empty;                   
                }
            }

            else if (ddlsearchopt.SelectedItem.Text == "PO Number")
            {
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM purchase_details where po_no='" + txtSearch.Text + "'", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)                      
                    getdetails();               
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Invalid PONo", "TrackRecords.aspx");
                    txtSearch.Text = String.Empty;                  
                }
            }
            else if (ddlsearchopt.SelectedItem.Text == "MRR Number")
            {
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM mr_report where mrr_no='" + txtSearch.Text + "'", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)                                            
                    getdetails();              
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Invalid MRR No", "TrackRecords.aspx");
                    txtSearch.Text = String.Empty;                   
                }

            }
            else if (ddlsearchopt.SelectedItem.Text == "Invoice Number")
            {
                da = new SqlDataAdapter("SELECT po_no FROM pending_invoice where invoiceno='" + txtSearch.Text + "' UNION SELECT po_no FROM temp_pending_invoice where invoiceno='" + txtSearch.Text + "' ", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)                
                    getdetails();              
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Invalid Invoice No", "TrackRecords.aspx");
                    txtSearch.Text = String.Empty;                 
                }
            }
            else if (ddlsearchopt.SelectedItem.Text == "Reference Number")
            {
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM [transfer info] where ref_no='" + txtSearch.Text + "'", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)           
                    gettransferdetails();            
                else
                {
                    JavaScript.UPAlertRedirect(Page, "Invalid Transfer No", "TrackRecords.aspx"); 
                    txtSearch.Text = String.Empty;                
                }
            }
        }
        else if (Authorised == "false")
        {
            txtSearch.Text = String.Empty;
            JavaScript.UPAlertRedirect(Page, "Invalid User", "TrackRecords.aspx");         
        }
       
    }
    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string lblstatus = ((Label)e.Item.FindControl("Label52")).Text;
            if (lblstatus == "3")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Material received & PO Closed";
                porpt.Visible = true;
                tblmrr.Visible = true;
            }
            if (lblstatus == "2")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "PO Approval completed & Waiting for receving stock";
                porpt.Visible = true;
                tblmrr.Visible = false;
                tblinvoice.Visible = false;
            }
            if (lblstatus == "1")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "PO waiting at CMC Level";
                porpt.Visible = true;
                tblmrr.Visible = false;
                tblinvoice.Visible = false;
            }
            if (lblstatus == "1A")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "PO waiting at SA Level";
                porpt.Visible = true;
                tblmrr.Visible = false;
                tblinvoice.Visible = false;
            }

        }
    }
    protected void rptmrr_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {        
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string lblstatus = ((Label)e.Item.FindControl("Label52")).Text;
            if (lblstatus == "Rejected")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "MR Rejected";
                tblmrr.Visible = true;
                tblinvoice.Visible = false;
            }
            if (lblstatus == "8")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Invoice Entered against MR";
                tblmrr.Visible = true;    
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "7A")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Invoice Entered against MR";
                tblmrr.Visible = true;
                tblinvoice.Visible = true;   
            }
            if (lblstatus == "7")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Invoice Entered against MR";
                tblmrr.Visible = true;    
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "6A")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Invoice Entered against MR";
                tblmrr.Visible = true;
                tblinvoice.Visible = true;
            }
            if (lblstatus == "6B")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Invoice Entered against MR";
                tblmrr.Visible = true;
                tblinvoice.Visible = true;
            }
            if (lblstatus == "6")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Invoice Entered against MR";
                tblmrr.Visible = true;    
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "5")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Invoice Entered against MR ";
                tblmrr.Visible = true;    
                tblinvoice.Visible = true;            
            }
            if (lblstatus == "4")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "Waiting for Invoice entry";
                tblmrr.Visible = true;    
                tblinvoice.Visible = false;
            }
             
            if (lblstatus == "3")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "MR waiting at Purchase Manager Level";
                tblmrr.Visible = true;    
                tblinvoice.Visible = false;            
            }
            if (lblstatus == "2")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "MR waiting at CSK Level";
                tblmrr.Visible = true;    
                tblinvoice.Visible = false;            
            }
            if (lblstatus == "1")
            {
                ((Label)e.Item.FindControl("Label52")).Text = "MR waiting for approval of Project Manager";
                tblmrr.Visible = true;    
                tblinvoice.Visible = false;            
            }

        }
    }
    protected void rptinvoice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string lblstatus = ((Label)e.Item.FindControl("Label50")).Text;
            string bbkstatus = ((Label)e.Item.FindControl("Label62")).Text;
            if (bbkstatus == "1")
            {
                ((Label)e.Item.FindControl("Label62")).Text = "Payment against invoice waitng at HO Admin Level";
                tblinvoice.Visible = true;    
            }
            if (bbkstatus == "2")
            {
                ((Label)e.Item.FindControl("Label62")).Text = "Payment against invoice waiting at SA Level";
                tblinvoice.Visible = true;    
            }
            if (bbkstatus == "3")
            {
                ((Label)e.Item.FindControl("Label62")).Text = "Paid";
                tblinvoice.Visible = true;    
            }
            if (bbkstatus == "Rejected")
            {
                ((Label)e.Item.FindControl("Label62")).Text = "Paid";
                tblinvoice.Visible = true;    
            }

            if (lblstatus == "8")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Invoice Entered";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "7A")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Invoice waiting at SA Level";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "7")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Invoice waiting at HoAdmin Approval";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "6")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Invoice  waiting at CSK Level";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "6A")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Invoice  waiting at PurchaseManager Level";
                tblinvoice.Visible = true;
            }
            if (lblstatus == "6B")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Invoice  waiting at CMC Level";
                tblinvoice.Visible = true;
            }
            if (lblstatus == "5")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Invoice waiting at Project Manger Level";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "4")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Waiting for Invoice entry";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "3")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "MR waiting at Purchase Manager Level";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "2")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "MR waiting at CSK Level";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "1")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "MR waiting for approval of Project Manager";
                tblinvoice.Visible = true;    
            }
            if (lblstatus == "Reject")
            {
                ((Label)e.Item.FindControl("Label50")).Text = "Invoice Rejected";
                tblinvoice.Visible = true;    
            } 
 

        }

    }

    public void getdetails()
    {
        itrpt.Visible = false;
        //indentrpt.Visible = true;
        //porpt.Visible = true;
        //tblinvoice.Visible = true;
        //tblmrr.Visible = true;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "TrackOfNumbers_sp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@number", txtSearch.Text);
        cmd.Parameters.AddWithValue("@option", ddlsearchopt.SelectedItem.Text);
        cmd.Connection = con;
        con.Open();
      

        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            indentrpt.Visible = true;
            Label8.Text = dr["indentraise_date"].ToString();
            Label10.Text = dr["indenttype"].ToString();
            Label7.Text = dr["indent_no"].ToString();          
            if (dr["status"].ToString() == "6")
                Label11.Text = "Indent Waiting for Raising PO";
            else if (dr["status"].ToString() == "7")
                Label11.Text = "Indent Approval Completed";
        }            
        dr.NextResult();
        while (dr.Read())
        {
            Label9.Text = dr["Indentamt"].ToString();
        }
        dr.NextResult();
        while (dr.Read())
        {
            Label61.Text = dr["Indentamount"].ToString();
        }    
        dr.NextResult();
        if (dr.HasRows)
        {
           List<object>tempList = new List<object>();

            while (dr.Read())
            {
                tempList.Add(new { prop1 = (string)dr["po_no"].ToString(), prop2 = (DateTime)dr["po_date"], prop3 = (string)dr["status"].ToString(), prop4 = (string)dr["amount"].ToString() });

            }
            rpt.DataSource = tempList;
            rpt.DataBind();          
        }
        dr.NextResult();
        if (dr.HasRows)
        {
            List<object> tempList = new List<object>();

            while (dr.Read())
            {
                tempList.Add(new { propmr = (Int64)dr["mrr_no"], proprd = (string)dr["recieved_date"], propst = (string)dr["status"].ToString(), proppo = (string)dr["po_no"].ToString() });

            }
            rptmrr.DataSource = tempList;
            rptmrr.DataBind();
           
        }
        dr.NextResult();
        if (dr.HasRows)
        {
            List<object> tempList = new List<object>();

            while (dr.Read())
            {
                tempList.Add(new { propmr = (Int64)dr["MRR_no"], propinvoice = (string)dr["invoiceno"].ToString(), propind = (string)dr["Invoice_Date"].ToString(), propname = (string)dr["name"].ToString(), propbasic = (Decimal)dr["basicvalue"], propED = (Decimal)dr["exciseduty"], propVAT = (Decimal)dr["vat"], propNA = (Decimal)dr["netamount"], propst = (string)dr["status"].ToString(), propayst = (string)dr["bbkstatus"].ToString(), propchamount = (Decimal)dr["chequeamount"], propchequepay = (string)dr["bankname"].ToString(), propcashamt = (Decimal)dr["cashamount"], propcashpay = (string)dr["cashpayment"].ToString() });
            }
            rptinvoice.DataSource = tempList;
            rptinvoice.DataBind();
           
        }
       
    }
    public void clear()
    {
        indentrpt.Visible = false;
        itrpt.Visible = false;
        porpt.Visible = false;
        tblinvoice.Visible = false;
        tblmrr.Visible = false;    
    }

    public void getpartialpurchasedetails()
    {
        //indentrpt.Visible = true;
        //itrpt.Visible = true;
        //porpt.Visible = true;
        //tblinvoice.Visible = true;
        //tblmrr.Visible = true;
        
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "TrackOfNumbers_sp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@number", txtSearch.Text);
        cmd.Parameters.AddWithValue("@option", ddlsearchopt.SelectedItem.Text);
        cmd.Connection = con;
        con.Open();

        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            indentrpt.Visible = true;
            Label8.Text = dr["indentraise_date"].ToString();
            Label10.Text = dr["indenttype"].ToString();
            Label7.Text = dr["indent_no"].ToString();
            if (dr["status"].ToString() == "6")
                Label11.Text = "Indent Waiting for Raising PO";
            else if (dr["status"].ToString() == "7")
                Label11.Text = "Indent Approval Completed";
        }

        dr.NextResult();
        while (dr.Read())
        {
            Label9.Text = dr["Indentamt"].ToString();
        }
        dr.NextResult();
        while (dr.Read())
        {
            Label61.Text = dr["Indentamount"].ToString();
        }    
        dr.NextResult();
        if (dr.HasRows)
        {
            List<object> tempList = new List<object>();

            while (dr.Read())
            {
                tempList.Add(new { prop1 = (string)dr["po_no"].ToString(), prop2 = (DateTime)dr["po_date"], prop3 = (string)dr["status"].ToString(), prop4 = (string)dr["amount"].ToString() });

            }
            rpt.DataSource = tempList;
            rpt.DataBind();
        }
        dr.NextResult();
        if (dr.HasRows)
        {
            List<object> tempList = new List<object>();

            while (dr.Read())
            {
                tempList.Add(new { propmr = (Int64)dr["mrr_no"], proprd = (string)dr["recieved_date"], propst = (string)dr["status"].ToString(), proppo = (string)dr["po_no"].ToString() });

            }
            rptmrr.DataSource = tempList;
            rptmrr.DataBind();
        }
        dr.NextResult();
        if (dr.HasRows)
        {
            List<object> tempList = new List<object>();

            while (dr.Read())
            {

                tempList.Add(new { propmr = (Int64)dr["MRR_no"], propinvoice = (string)dr["invoiceno"].ToString(), propind = (string)dr["Invoice_Date"], propname = (string)dr["name"], propbasic = (Decimal)dr["basicvalue"], propED = (Decimal)dr["exciseduty"], propVAT = (Decimal)dr["vat"], propNA = (Decimal)dr["netamount"], propst = (string)dr["status"].ToString(), propayst = (string)dr["bbkstatus"].ToString(), propchamount = (Decimal)dr["chequeamount"], propchequepay = (string)dr["bankname"].ToString(), propcashamt = (Decimal)dr["cashamount"], propcashpay = (string)dr["cashpayment"].ToString() });
            }
            rptinvoice.DataSource = tempList;
            rptinvoice.DataBind();
        }
        dr.NextResult();
        while (dr.Read())
        {
            itrpt.Visible = true;
            Label14.Text = dr["ref_no"].ToString();
            Label16.Text = dr["transfer_date"].ToString();
            if (dr["type"].ToString() == "1")
                Label46.Text = "Issue";
            else
                Label46.Text = "Transfer";
            if (dr["status"].ToString() == "1")
                Label20.Text = "Waiting for CSK Approval";
            else if (dr["status"].ToString() == "1A")
                Label20.Text = "Waiting for CMC Approval";
            else if (dr["status"].ToString() == "2")
            {
                if (dr["type"].ToString() == "1")
                    Label20.Text = "Waiting for Recieving Stock";
                else
                    Label20.Text = "Waiting for Sending Stock";
            }
            else if (dr["status"].ToString() == "3")
                Label20.Text = "Waiting at Project Manager Level";
            else if (dr["status"].ToString() == "3A")
                Label20.Text = "Waiting at CSK Level";
            else if (dr["status"].ToString() == "3B")
                Label20.Text = "Waiting at CMC Level";
            else if (dr["status"].ToString() == "4")
            {
                if (dr["type"].ToString() == "1")
                    Label20.Text = "Issue Completed";
                else
                    Label20.Text = "Waiting for Recieving Stock";
            }
            else if (dr["status"].ToString() == "5")
            {
                if (Label7.Text != "" && Label7.Text != "NULL")
                    Label20.Text = "Waiting at Project Manager Level";
                else
                    Label20.Text = "Transfer Completed";
            }

            else if (dr["status"].ToString() == "6")
                Label20.Text = "Transfer Completed";
        }
        dr.NextResult();
        while (dr.Read())
        {
            Label18.Text = dr["issuedamt"].ToString();
            if (Label18.Text != "NULL" && Label18.Text  !="")
            Label9.Text =Convert.ToDecimal(Convert.ToDecimal(Label18.Text) + Convert.ToDecimal(Label9.Text)).ToString();
        }
        //con.Close();
    }

    public void gettransferdetails()
    {       
        itrpt.Visible = true;
        indentrpt.Visible = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "TrackOfNumbers_sp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@number", txtSearch.Text);
        cmd.Parameters.AddWithValue("@option", ddlsearchopt.SelectedItem.Text);       
        cmd.Connection = con;
        con.Open();
        SqlDataReader dr;
        dr = cmd.ExecuteReader();       
        while (dr.Read())
        {
           
                indentrpt.Visible = true;
                Label7.Text = dr["indent_no"].ToString();
                Label8.Text = dr["indentraise_date"].ToString();
                Label10.Text = dr["indenttype"].ToString();
                if (dr["status"].ToString() == "7")
                    Label11.Text = "Indent Approval Completed";
                else if (dr["status"].ToString() == "2A")
                    Label11.Text = "Indent Approval at CSK";          
        }

        dr.NextResult();
        while (dr.Read())
        {
            Label9.Text = dr["Indentamt"].ToString();
        }
        dr.NextResult();
        while (dr.Read())
        {
            Label14.Text = dr["ref_no"].ToString();
            Label16.Text = dr["transfer_date"].ToString();
            if (dr["type"].ToString() == "1")
                Label46.Text = "Issue";
            else
                Label46.Text = "Transfer";
            if (dr["status"].ToString() == "1")
                Label20.Text = "Waiting for CSK Approval";
            else if(dr["status"].ToString() == "1A")
                Label20.Text = "Waiting for CMC Approval";
            else if (dr["status"].ToString() == "2")
            {
                if (dr["type"].ToString() == "1")              
                    Label20.Text = "Waiting for Recieving Stock";
                else
                    Label20.Text = "Waiting for Sending Stock";
            }
            else if (dr["status"].ToString() == "3")
                Label20.Text = "Waiting at Project Manager Level";
            else if (dr["status"].ToString() == "3A")
                Label20.Text = "Waiting at CSK Level";
            else if (dr["status"].ToString() == "3B")
                Label20.Text = "Waiting at CMC Level";
            else if (dr["status"].ToString() == "4")
            {
                if (dr["type"].ToString() == "1")               
                    Label20.Text = "Issue Completed";
                else
                    Label20.Text = "Waiting for Recieving Stock";
            }
            else if (dr["status"].ToString() == "5")
            {
                if (Label7.Text != "" && Label7.Text != "NULL")
                     Label20.Text = "Waiting at Project Manager Level";                 
               else
                    Label20.Text = "Transfer Completed";
            }

            else if (dr["status"].ToString() == "6")
                Label20.Text = "Transfer Completed";
        }
        dr.NextResult();
        while (dr.Read())
        {
            Label18.Text = dr["issuedamt"].ToString();
        }

    }
}