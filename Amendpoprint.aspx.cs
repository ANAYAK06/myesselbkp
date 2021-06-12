using System;
using System.Collections.Generic;
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

public partial class Amendpoprint : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (Request.QueryString["id"] == null)
        {
            JavaScript.CloseWindow();
        }
        else
        {
            filldata();
            fillgrid();
            pono();
        }
    }
    private void fillgrid()
    {
        try
        {
            //    if (Session["roles"].ToString() == "HoAdmin")
            //        //da = new SqlDataAdapter("select replace((isnull(p.po_value,0)),'.0000','.00')as [Po Value],replace((isnull(a.Amended_amount,0)),'.0000','.00')as [Amended Amount],a.remarks as [Remarks],replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.id='" + Request.QueryString["id"].ToString() + "'", con);
            //        da = new SqlDataAdapter("Select i.[PO Value] as [Previous PO Value],am.Amended_amount as [Amended Amount],am.remarks as Remarks,replace((isnull(i.[PO Value],0)+isnull(am.Amended_amount,0)),'.0000','.00')as [Revised PO Total] from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0)+isnull(amended_amount,0))[PO Value] from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,status from sppo)s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('2') and id not in ('" + Request.QueryString["id"].ToString() + "') group by pono)a on a.pono=s.pono))i join amend_sppo am on i.pono=am.pono where am.id in ('" + Request.QueryString["id"].ToString() + "')", con);
            //    else
            da = new SqlDataAdapter("Select i.[PO Value] as [Previous PO Value],am.Amended_amount as [Amended Amount],am.remarks as Remarks,replace((isnull(i.[PO Value],0)+isnull(am.Amended_amount,0)),'.0000','.00')as [Revised PO Total] from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0)+isnull(amended_amount,0))[PO Value] from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,status from sppo)s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('3') and id not in ('" + Request.QueryString["id"].ToString() + "') group by pono)a on a.pono=s.pono))i join amend_sppo am on i.pono=am.pono where am.id in ('" + Request.QueryString["id"].ToString() + "')", con);
            da.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count > 0)
            {

                GridView1.DataSource = ds.Tables["data"];
                GridView1.DataBind();


            }
            else
            {
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

    public void filldata()
    {
        da = new SqlDataAdapter("select  s.vendor_id,vendor_name,address,vendor_phone,cc_code,s.pono,REPLACE(CONVERT(VARCHAR(11),Amended_date, 106), ' ', '-')as Amended_date,dca_code from vendor v join SPPO s on v.vendor_id=s.vendor_id join Amend_sppo a on s.pono=a.pono where a.id='" + Request.QueryString["id"].ToString() + "'", con);
        da.Fill(ds, "view");
        if (ds.Tables["view"].Rows.Count > 0)
        {
            lblvenname.Text = ds.Tables["view"].Rows[0].ItemArray[1].ToString();
            lblvenaddress.Text = ds.Tables["view"].Rows[0].ItemArray[2].ToString();
            lblphone.Text = ds.Tables["view"].Rows[0].ItemArray[3].ToString();
            lblcccode.Text = ds.Tables["view"].Rows[0].ItemArray[4].ToString();
            lblpono.Text = ds.Tables["view"].Rows[0].ItemArray[5].ToString();
            lblpodate.Text = ds.Tables["view"].Rows[0].ItemArray[6].ToString();
            lbldcacode.Text = ds.Tables["view"].Rows[0].ItemArray[7].ToString();
            lblvendorcode.Text = ds.Tables["view"].Rows[0].ItemArray[0].ToString();
            ViewState["pono"] = ds.Tables["view"].Rows[0].ItemArray[5].ToString();
        }

    }
    public void pono()
    {
        da = new SqlDataAdapter("select count(pono) from amend_sppo where pono='" + ViewState["pono"].ToString() + "' and status in ('2','3')", con);
        da.Fill(ds, "count");
        if (ds.Tables["count"].Rows.Count > 0)
        {
            lblpocount.Text = ds.Tables["count"].Rows[0].ItemArray[0].ToString();
        }
    }
}
