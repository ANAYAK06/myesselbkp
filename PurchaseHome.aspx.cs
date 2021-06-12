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

public partial class PurchaseHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
       Essel childmaster = (Essel)this.Master;
       HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
       lbl.Attributes.Add("class", "active");
       if (Session["user"] == null)
           Response.Redirect("SessionExpire.aspx");
       

       if (!IsPostBack)
       {
           if (Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "Project Manager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "StoreKeeper")
           {
               fillhome();
           }
       }
      
    }
    public void fillhome()
    {
        try
        {
           
           if (Session["roles"].ToString() == "PurchaseManager")
               da = new SqlDataAdapter("select ('Indent is Raised From '+ cc_code +' '+remarks +','+' The Indent No is '+ indent_no)  Remarks,'1'as [status] from indents where status='4' union all select ('Indent is Raised From '+ cc_code + ' and it is wait for raise the PO')  Remarks,'3'as [status] from indents where status='6'", con);

           else if (Session["roles"].ToString() == "Central Store Keeper")
               da = new SqlDataAdapter("select ('Indent is Raised From '+ cc_code +' '+remarks +','+' The Indent No is '+ indent_no)  Remarks,'1'as [status] from indents where status='2' union all select ('PO is raised from'+cc_code+' and The PO no is: '+ po_no)Remarks,'2'as [status] from [purchase_details] where status='2' and (cc_code='CC-33' or cc_code='CCC')", con);
               //da = new SqlDataAdapter("select ('Indent is Raised From '+ cc_code +' '+remarks +','+' The Indent No is '+ indent_no)  Remarks,'1'as [status] from indents where status='2' union all select ('PO is raised from'+cc_code+' and The PO no is: '+ po_no)Remarks,'2'as [status] from [purchase_details] where status='2' and (cc_code='CC-33' or cc_code='CCC') union all select ('Invoice is raised from '+p.cc_code )Remarks,'3' as [status] from mr_report m  join pending_invoice p on m.po_no=p.po_no where m.status='5'", con);
       
            else if (Session["roles"].ToString() == "Project Manager")
               da = new SqlDataAdapter("select ('Indent is Raised From '+ cc_code +' '+remarks +','+' The Indent No is '+ indent_no)  Remarks,'1'as [status] from indents where status='1' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') union all select ('PO is raised from'+cc_code+''+remarks)Remarks,'2'as [status] from [Purchase_details] where status='2' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "' and  status='5')", con);
               
            else if (Session["roles"].ToString() == "Chief Material Controller")
               da = new SqlDataAdapter("select ('Indent is Raised From '+ cc_code +' '+remarks +','+' The Indent No is '+ indent_no)  Remarks,'1'as [status] from indents where status='5' union all select ('PO is raised from'+cc_code+' and The PO no is: '+ po_no)Remarks,'2'as [status] from [purchase_details] where status='1'", con);
               //da = new SqlDataAdapter("select ('Indent is Raised From '+ cc_code +' '+remarks +','+' The Indent No is '+ indent_no)  Remarks,'1'as [status] from indents where status='5' union all select ('PO is raised from'+cc_code+' and The PO no is: '+ po_no)Remarks,'2'as [status] from [purchase_details] where status='1' union all select ('Invoice is raised from'+p.cc_code)Remarks,'3' as [status] from mr_report m  join pending_invoice p on m.po_no=p.po_no where m.status='5'", con);

           else if (Session["roles"].ToString() == "SuperAdmin")
               da = new SqlDataAdapter("select ('Indent is Raised From '+ cc_code +' '+remarks +','+' The Indent No is '+ indent_no)  Remarks,'1'as [status] from indents union all select ('PO is raised from'+cc_code+''+remarks)Remarks,'2'as [status] from [purchase_details] where status='2'", con);
           else if (Session["roles"].ToString() == "StoreKeeper")
               da = new SqlDataAdapter("select ('PO is raised from'+cc_code+' and The PO no is: '+ po_no)Remarks,'2'as [status] from [purchase_details] where status='2' and cc_code='" + Session["cc_code"].ToString() + "'", con);
           

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
  
}

