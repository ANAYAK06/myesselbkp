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
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Collections.Specialized;
using AjaxControlToolkit;

public partial class Admin_frmviewcc : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        //loginbll RoleCheck = new loginbll();
        //int rec = 0;
        // rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 22);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            Panel2.Visible = false; 
            if (Session["roles"].ToString() == "Accountant")
            {
                //Button1.Visible = false;
                ddl.Visible = false;
                s1.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                loaddetails(Session["cc_code"].ToString());
            }           
        }       
        
    }

    private void loaddetails(string CCCODE)
    {


        //da = new SqlDataAdapter("select c.project_name,c.client_name,c.customer_name,c.natureofjob,c.po_no,c.projectmanager_name,c.projectmanager_contactno,d.cc_inchargename,d.cc_incharge_phno,d.cc_phoneno,d.address,d.address,d.cc_code,d.cc_name,d.final_offerno,REPLACE(CONVERT(VARCHAR(11),d.final_offerdate, 106), ' ', '-')as [final_offerdate],d.ref_no,REPLACE(CONVERT(VARCHAR(11),d.ref_date, 106), ' ', '-')as [ref_date],d.cc_type,c.po_date,cast(p.po_basicvalue as decimal(25,2))po_basicvalue from Cost_Center d left outer join contract c on d.cc_code=c.cc_code left outer join po p on c.po_no=p.po_no where d.cc_code ='" + CCCODE + "'", con);
        //da = new SqlDataAdapter("Select i.project_name,i.client_name,i.customer_name,i.natureofjob,i.po_no,i.projectmanager_name,i.projectmanager_contactno,i.cc_inchargename,i.cc_incharge_phno,i.cc_phoneno,i.address,i.cc_code,i.cc_name,i.final_offerno,REPLACE(CONVERT(VARCHAR(11),i.final_offerdate, 106), ' ', '-')as [final_offerdate],i.ref_no,REPLACE(CONVERT(VARCHAR(11),i.ref_date, 106), ' ', '-')as [ref_date],i.cc_type,i.po_date,cast((i.po_basicvalue+isnull(B.[Amd Value],0)) as decimal(25,2))po_basicvalue from (select c.project_name,c.client_name,c.customer_name,c.natureofjob,c.po_no,c.projectmanager_name,c.projectmanager_contactno,d.cc_inchargename,d.cc_incharge_phno,d.cc_phoneno,d.address,d.cc_code,d.cc_name,d.final_offerno,REPLACE(CONVERT(VARCHAR(11),d.final_offerdate, 106), ' ', '-')as [final_offerdate],d.ref_no,REPLACE(CONVERT(VARCHAR(11),d.ref_date, 106), ' ', '-')as [ref_date],d.cc_type,c.po_date,cast(p.po_basicvalue as decimal(25,2))po_basicvalue from Cost_Center d left outer join contract c on d.cc_code=c.cc_code left outer join po p on c.po_no=p.po_no where d.cc_code ='" + CCCODE + "')i left join (Select A.po_no,A.[Amd Value] from(select po_no,SUM(po_amendedbasicvalue) as [Amd Value] from po_amended group by po_no)A)B on i.po_no=B.PO_NO", con);
        da = new SqlDataAdapter("Select i.project_name,i.client_name,i.customer_name,i.natureofjob,i.po_no,i.projectmanager_name,i.projectmanager_contactno,i.cc_inchargename,i.cc_incharge_phno,i.cc_phoneno,i.address,i.cc_code,i.cc_name,i.final_offerno,REPLACE(CONVERT(VARCHAR(11),i.final_offerdate, 106), ' ', '-')as [final_offerdate],i.ref_no,REPLACE(CONVERT(VARCHAR(11),i.ref_date, 106), ' ', '-')as [ref_date],i.cc_type,i.po_date,cast((i.po_basicvalue+isnull(B.[Amd Value],0)) as decimal(25,2))po_basicvalue,i.cc_subtype from (select c.project_name,c.client_name,c.customer_name,c.natureofjob,c.po_no,c.projectmanager_name,c.projectmanager_contactno,d.cc_inchargename,d.cc_incharge_phno,d.cc_phoneno,d.address,d.cc_code,d.cc_name,d.final_offerno,REPLACE(CONVERT(VARCHAR(11),d.final_offerdate, 106), ' ', '-')as [final_offerdate],d.ref_no,REPLACE(CONVERT(VARCHAR(11),d.ref_date, 106), ' ', '-')as [ref_date],d.cc_type,c.po_date,cast(p.po_basicvalue as decimal(25,2))po_basicvalue,d.cc_subtype from Cost_Center d left outer join contract c on d.cc_code=c.cc_code left outer join po p on c.po_no=p.po_no where d.cc_code ='" + CCCODE + "')i left join (Select A.po_no,A.[Amd Value] from(select po_no,SUM(po_amendedbasicvalue) as [Amd Value] from po_amended group by po_no)A)B on i.po_no=B.PO_NO", con);
        da.Fill(ds, "details");
        if (ds.Tables["details"].Rows.Count >= 1 && ds.Tables["details"].Rows[0].ItemArray[17].ToString() == "Performing")
        {
            try
            {
                //pname.Text = ds.Tables["details"].Rows[0].ItemArray[0].ToString();
                //client.Text = ds.Tables["details"].Rows[0].ItemArray[1].ToString();
                //cust.Text = ds.Tables["details"].Rows[0].ItemArray[2].ToString();
                //noj.Text = ds.Tables["details"].Rows[0].ItemArray[3].ToString();
                //pono.Text = ds.Tables["details"].Rows[0].ItemArray[4].ToString();
                piname.Text = ds.Tables["details"].Rows[0].ItemArray[5].ToString();
                cno.Text = ds.Tables["details"].Rows[0].ItemArray[6].ToString();
                incname.Text = ds.Tables["details"].Rows[0].ItemArray[7].ToString();
                inphno.Text = ds.Tables["details"].Rows[0].ItemArray[8].ToString();
                phno.Text = ds.Tables["details"].Rows[0].ItemArray[9].ToString();
                addres.Text = ds.Tables["details"].Rows[0].ItemArray[10].ToString();
                add.Text = ds.Tables["details"].Rows[0].ItemArray[10].ToString();
                ccode.Text = ds.Tables["details"].Rows[0].ItemArray[11].ToString();
                ccname.Text = ds.Tables["details"].Rows[0].ItemArray[12].ToString();
                epplfinalofferno.Text=ds.Tables["details"].Rows[0].ItemArray[13].ToString();
                finalofferdate.Text = ds.Tables["details"].Rows[0].ItemArray[14].ToString();
                refno.Text = ds.Tables["details"].Rows[0].ItemArray[15].ToString();
                date.Text = ds.Tables["details"].Rows[0].ItemArray[16].ToString();
                lblcctype.Text = ds.Tables["details"].Rows[0].ItemArray[17].ToString();
                lblccsubtype.Text = ds.Tables["details"].Rows[0].ItemArray[20].ToString();
                Panel2.Visible = true;
                trcc.Visible = true;
                trrefno.Visible = true;
                Label29.Visible = true;
                lblccsubtype.Visible = true;
                //po.Visible = true;
                //trclient.Visible = true;
                //trnoj.Visible = true;
                trdl.Visible = true;
                if (ds.Tables["details"].Rows[0].ItemArray[4].ToString() != "")
                {
                    trpovalue.Visible = true;
                    lbltotalPOvalue.Text = (Convert.ToDecimal(ds.Tables["details"].Compute("sum(po_basicvalue)", string.Empty))).ToString();

                    dl.DataSource = ds.Tables["details"];
                    dl.DataBind();
                }
                else
                {
                    trpovalue.Visible = false;
                    dl.DataSource = null;
                    dl.DataBind();
                }
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
        else if (ds.Tables["details"].Rows.Count >= 1 && ds.Tables["details"].Rows[0].ItemArray[17].ToString() != "Performing")
        {
            try
            {
                //pname.Text = ds.Tables["details"].Rows[0].ItemArray[0].ToString();
                //client.Text = ds.Tables["details"].Rows[0].ItemArray[1].ToString();
                //cust.Text = ds.Tables["details"].Rows[0].ItemArray[2].ToString();
                //noj.Text = ds.Tables["details"].Rows[0].ItemArray[3].ToString();
                //pono.Text = ds.Tables["details"].Rows[0].ItemArray[4].ToString();
                piname.Text = ds.Tables["details"].Rows[0].ItemArray[5].ToString();
                cno.Text = ds.Tables["details"].Rows[0].ItemArray[6].ToString();
                incname.Text = ds.Tables["details"].Rows[0].ItemArray[7].ToString();
                inphno.Text = ds.Tables["details"].Rows[0].ItemArray[8].ToString();
                phno.Text = ds.Tables["details"].Rows[0].ItemArray[9].ToString();
                addres.Text = ds.Tables["details"].Rows[0].ItemArray[10].ToString();
                add.Text = ds.Tables["details"].Rows[0].ItemArray[10].ToString();
                ccode.Text = ds.Tables["details"].Rows[0].ItemArray[11].ToString();
                ccname.Text = ds.Tables["details"].Rows[0].ItemArray[12].ToString();
                epplfinalofferno.Text = ds.Tables["details"].Rows[0].ItemArray[13].ToString();
                finalofferdate.Text = ds.Tables["details"].Rows[0].ItemArray[14].ToString();
                refno.Text = ds.Tables["details"].Rows[0].ItemArray[15].ToString();
                date.Text = ds.Tables["details"].Rows[0].ItemArray[16].ToString();
                lblcctype.Text = ds.Tables["details"].Rows[0].ItemArray[17].ToString();
                lblccsubtype.Text = ds.Tables["details"].Rows[0].ItemArray[20].ToString();
                Panel2.Visible = true;
                trcc.Visible = false;
                trrefno.Visible = false;
                trdl.Visible = false;
                trpovalue.Visible = false;
                Label29.Visible = false;
                lblccsubtype.Visible = false;
                //po.Visible = false;
                //trclient.Visible = false;
                //trnoj.Visible = false;
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
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        loaddetails(ddl.SelectedValue);        
    }
}
