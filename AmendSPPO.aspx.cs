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
using System.Drawing;

public partial class AmendSPPO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (Session["roles"].ToString() == "Sr.Accountant")
        {
            trAmendedposnno.Visible = true;
            tramendedpo.Visible = false;            
        }
        else
        {
            trAmendedposnno.Visible = false;
            tramendedpo.Visible = true;
        }

        if (!IsPostBack)
        {
            if (Session["roles"].ToString() != "Sr.Accountant")
            {
                fillgrid();
            }
            tblamendsppo.Visible = false;
            //BindGridview();
            tramounts.Visible = false;
            trbtns.Visible = false;
            btnremoveitem.Visible = false;
            btnadditem.Visible = false;
        }
        
        
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BankDateCheck objdate = new BankDateCheck();
            if (objdate.IsSPPODateCheck(ddlpono.SelectedItem.Text, ddlcccode.SelectedValue, txtpodate.Text))
            {
                string ADescs = "";
                string AUnits = "";
                string AQtys = "";
                string ARates = "";
                string AAmts = "";
                string ATypes = "";
                string NDescs = "";
                string NUnits = "";
                string NQtys = "";
                string NRates = "";
                string NAmts = "";
                string MDescs = "";
                string NDescsc = "";
                string OurAmts = "";
                string PrwAmts = "";
                if (Session["roles"].ToString() == "Sr.Accountant")
                {

                    string result = "";
                    foreach (GridViewRow record in gvDetailsnew.Rows)
                    {
                        if (gvDetailsnew != null)
                        {
                            TextBox txtdescnewc = (TextBox)record.FindControl("txtitemdescnew");
                            string descnewc = txtdescnewc.Text;
                            if (descnewc != "")
                            {
                                NDescsc = NDescsc + descnewc + ",";
                            }
                        }
                    }
                    cmd1 = new SqlCommand("CheckItemssp", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Clear();
                    cmd1.Parameters.AddWithValue("@Newdescs", NDescsc);
                    cmd1.Parameters.AddWithValue("@PONO", ddlpono.SelectedItem.Text);
                    cmd1.Parameters.AddWithValue("@CCCode", ddlcccode.SelectedValue);
                    cmd1.Parameters.AddWithValue("@SubstractAmt", txtreturnAmt.Text);
                    cmd1.Connection = con;
                    con.Open();
                    result = cmd1.ExecuteScalar().ToString();
                    con.Close();
                    if (result == "NotExists")
                    {
                        if (txtreturnAmt.Text != "0" || txtamendamt.Text != "0")
                        {
                            foreach (GridViewRow record in gvDetails.Rows)
                            {
                                DropDownList Type = (DropDownList)record.FindControl("ddltype");
                                if (Type.SelectedValue != "Select")
                                {

                                    TextBox txtdesc = (TextBox)record.FindControl("txtitemdesc");
                                    string desc = txtdesc.Text;
                                    TextBox txtunit = (TextBox)record.FindControl("txtunit");
                                    string unit = txtunit.Text;
                                    TextBox txtrate = (TextBox)record.FindControl("txtrate");
                                    string rate = txtrate.Text;
                                    DropDownList ddltype = (DropDownList)record.FindControl("ddltype");
                                    string type = ddltype.SelectedValue;
                                    TextBox txtqty = (TextBox)record.FindControl("txtamendquantity");
                                    string qty = txtqty.Text;
                                    TextBox txtamt = (TextBox)record.FindControl("txtamount");
                                    string amt = txtamt.Text;

                                    if (rate != "" || amt != "" || rate != "0" || amt != "0")
                                    {
                                        ADescs = ADescs + desc + ",";
                                        AUnits = AUnits + unit + ",";
                                        AQtys = AQtys + qty + ",";
                                        ARates = ARates + rate + ",";
                                        AAmts = AAmts + amt + ",";
                                        ATypes = ATypes + type + ",";
                                    }
                                }
                            }
                            foreach (GridViewRow record in gvDetailsnew.Rows)
                            {
                                if (gvDetailsnew != null)
                                {
                                    CheckBox c1 = (CheckBox)record.FindControl("chkSelectnew");
                                    if (c1.Checked)
                                    {
                                        TextBox txtdescnew = (TextBox)record.FindControl("txtitemdescnew");
                                        string descnew = txtdescnew.Text;
                                        TextBox txtunitnew = (TextBox)record.FindControl("txtunitnew");
                                        string unitnew = txtunitnew.Text;
                                        TextBox txtqtynew = (TextBox)record.FindControl("txtquantitynew");
                                        string qtynew = txtqtynew.Text;
                                        TextBox txtratenew = (TextBox)record.FindControl("txtratenew");
                                        string ratenew = txtratenew.Text;
                                        TextBox txtouramt = (TextBox)record.FindControl("txtourrate");
                                        string ouramt = txtouramt.Text;
                                        TextBox txtprwamt = (TextBox)record.FindControl("txtprwrate");
                                        string prwamt = txtprwamt.Text;
                                        TextBox txtamtnew = (TextBox)record.FindControl("txtamountnew");
                                        string amtnew = txtamtnew.Text;
                                        if (descnew != "" || unitnew != "" || qtynew != "" || ratenew != "" || amtnew != "" || ouramt != "" || prwamt != "")
                                        {
                                            NDescs = NDescs + descnew + ",";
                                            NUnits = NUnits + unitnew + ",";
                                            NQtys = NQtys + qtynew + ",";
                                            NRates = NRates + ratenew + ",";
                                            OurAmts = OurAmts + ouramt + ",";
                                            PrwAmts = PrwAmts + prwamt + ",";
                                            NAmts = NAmts + amtnew + ",";

                                        }
                                    }
                                }
                            }
                            foreach (GridViewRow record in grdterms.Rows)
                            {
                                CheckBox chkterm = (CheckBox)record.FindControl("chkSelectterms");
                                if (chkterm.Checked && chkterm.Enabled == true)
                                {
                                    TextBox txtterm = (TextBox)record.FindControl("txtterms");
                                    string termdesc = txtterm.Text;
                                    if (termdesc != "")
                                    {
                                        MDescs = MDescs + termdesc + "$";
                                    }
                                }
                            }
                            cmd = new SqlCommand("sp_ServiceProviderverifypo", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@pono", ddlpono.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@Amended_date", txtpodate.Text);
                            cmd.Parameters.AddWithValue("@Amended_amount", txtamendamt.Text);
                            cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                            cmd.Parameters.AddWithValue("@remarks", MDescs);
                            //cmd.Parameters.Add(new SqlParameter("@Terms", MDescs));
                            cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());

                            cmd.Parameters.Add(new SqlParameter("@ADescs", ADescs));
                            cmd.Parameters.Add(new SqlParameter("@AUnits", AUnits));
                            cmd.Parameters.Add(new SqlParameter("@AQtys", AQtys));
                            cmd.Parameters.Add(new SqlParameter("@ARates", ARates));
                            cmd.Parameters.Add(new SqlParameter("@AAmts", AAmts));
                            cmd.Parameters.Add(new SqlParameter("@ATypes", ATypes));

                            cmd.Parameters.Add(new SqlParameter("@NDescs", NDescs));
                            cmd.Parameters.Add(new SqlParameter("@NUnits", NUnits));
                            cmd.Parameters.Add(new SqlParameter("@NQtys", NQtys));
                            cmd.Parameters.Add(new SqlParameter("@NRates", NRates));
                            cmd.Parameters.Add(new SqlParameter("@NAmts", NAmts));
                            cmd.Parameters.Add(new SqlParameter("@OurRates", OurAmts));
                            cmd.Parameters.Add(new SqlParameter("@PRWRates", PrwAmts));

                            cmd.Parameters.Add(new SqlParameter("@ReturnAmt", txtreturnAmt.Text));
                            
                            con.Open();
                            string msg = cmd.ExecuteScalar().ToString();
                            //string msg = "passed";
                            if (msg == "PO Inserted")
                            {
                                JavaScript.UPAlertRedirect(Page, msg, "AmendSPPO.aspx");
                            }
                            else if (msg == "PO Verified")
                            {

                                JavaScript.UPAlertRedirect(Page, msg, "AmendSPPO.aspx");
                            }
                            else
                                JavaScript.UPAlert(Page, msg);
                            con.Close();
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Amended PO Amount Can not be Zero");
                        }

                    }
                    else
                    {
                        JavaScript.UPAlert(Page, result);
                    }
                }

            }
            else
            {
                JavaScript.UPAlert(Page, "Your seleced date is not valid");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }   
    public void showalert(string message)
    {
        string script = @"window.alert('" + message + "');window.location ='AmendSPPO.aspx';window.open('Amendpoprint.aspx?id=" + ViewState["id"].ToString() + "','Report','width=740,height=310,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert", script, true);
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("AmendSPPO.aspx");

    }
    //protected void ddlpono_SelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //    da = new SqlDataAdapter("select REPLACE(CONVERT(VARCHAR(11),created_date, 106), ' ', '-')as created_date from sppo where pono='" + ddlpono.SelectedItem.Text + "'", con);
    //    da.Fill(ds, "datecheck");
    //    if (ds.Tables["datecheck"].Rows.Count > 0)
    //    {
    //        h1.Value = ds.Tables["datecheck"].Rows[0].ItemArray[0].ToString();
    //    }
        

    //}
    private void fillgrid()
    {

        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='1'", con);
                da = new SqlDataAdapter("Select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)) AS Decimal(20,2))as [POTotal],vendor_name,'KK' as cc_type,i.cc_code from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)) AS Decimal(20,2))[PO Value],vendor_name,cc_code from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(balance,0) AS Decimal(20,2))as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name from sppo sp  join vendor v on sp.vendor_id=v.vendor_id and sp.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "'))s left outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1'", con);

            else if (Session["roles"].ToString() == "PurchaseManager")
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='1'", con);
                //da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)+isnull(amended_amount,0)) AS Decimal(20,2))[PO Value],vendor_name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(balance,0) AS Decimal(20,2))as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1A'", con);
                da = new SqlDataAdapter("Select  a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id,i.cc_type from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)) AS Decimal(20,2))[PO Value],vendor_name ,s.cc_code,s.vendor_id,s.cc_type from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(sp.balance,0) AS Decimal(20,2))as balance,sp.cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id,cc.cc_type from sppo sp  join vendor v on sp.vendor_id=v.vendor_id JOIN dbo.Cost_Center cc on sp.cc_code=cc.cc_code where cc_type='Performing')s left outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1A' UNION ALL Select  a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id,i.cc_type from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)+isnull(amended_amount,0)) AS Decimal(20,2))[PO Value],vendor_name ,s.cc_code,s.vendor_id,s.cc_type from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(sp.balance,0) AS Decimal(20,2))as balance,sp.cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id,cc.cc_type from sppo sp  join vendor v on sp.vendor_id=v.vendor_id JOIN dbo.Cost_Center cc on sp.cc_code=cc.cc_code  WHERE  cc_type='Non-Performing')s left outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1A'", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='1'", con);
                //da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)+isnull(amended_amount,0)) AS Decimal(20,2))[PO Value],vendor_name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(balance,0) AS Decimal(20,2))as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1A'", con);
                da = new SqlDataAdapter("Select  a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id,i.cc_type from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)) AS Decimal(20,2))[PO Value],vendor_name ,s.cc_code,s.vendor_id,s.cc_type from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(sp.balance,0) AS Decimal(20,2))as balance,sp.cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id,cc.cc_type from sppo sp  join vendor v on sp.vendor_id=v.vendor_id JOIN dbo.Cost_Center cc on sp.cc_code=cc.cc_code where cc_type='Performing')s left outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1B' UNION ALL Select  a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id,i.cc_type from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)+isnull(amended_amount,0)) AS Decimal(20,2))[PO Value],vendor_name ,s.cc_code,s.vendor_id,s.cc_type from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(sp.balance,0) AS Decimal(20,2))as balance,sp.cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id,cc.cc_type from sppo sp  join vendor v on sp.vendor_id=v.vendor_id JOIN dbo.Cost_Center cc on sp.cc_code=cc.cc_code  WHERE  cc_type='Non-Performing')s left outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1B'", con);
            else if (Session["roles"].ToString() == "HoAdmin")
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='1'", con);
                //da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)+isnull(amended_amount,0)) AS Decimal(20,2))[PO Value],vendor_name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(balance,0) AS Decimal(20,2))as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1A'", con);
                da = new SqlDataAdapter("Select  a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id,i.cc_type from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)) AS Decimal(20,2))[PO Value],vendor_name ,s.cc_code,s.vendor_id,s.cc_type from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(sp.balance,0) AS Decimal(20,2))as balance,sp.cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id,cc.cc_type from sppo sp  join vendor v on sp.vendor_id=v.vendor_id JOIN dbo.Cost_Center cc on sp.cc_code=cc.cc_code where cc_type='Performing')s left outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1BN' UNION ALL Select  a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id,i.cc_type from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)+isnull(amended_amount,0)) AS Decimal(20,2))[PO Value],vendor_name ,s.cc_code,s.vendor_id,s.cc_type from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(sp.balance,0) AS Decimal(20,2))as balance,sp.cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id,cc.cc_type from sppo sp  join vendor v on sp.vendor_id=v.vendor_id JOIN dbo.Cost_Center cc on sp.cc_code=cc.cc_code  WHERE  cc_type='Non-Performing')s left outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1BN'", con);

            else 
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='2'", con);
                //da = new SqlDataAdapter("select a.id,a.pono,i.[PO Value] po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0)+isnull(amended_amount,0))[PO Value] from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,status from sppo)s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='2'", con);
                da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)) AS Decimal(20,2))as [POTotal],vendor_name,''as cc_type from (Select (case when s.pono is not null then s.pono else a.pono end)pono,CAST((isnull(po_value,0)) AS Decimal(20,2))[PO Value],vendor_name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,CAST(isnull(po_value,0) AS Decimal(20,2)) as po_value,CAST(isnull(balance,0) AS Decimal(20,2))as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,CAST(isnull(sum(amended_amount),0) AS Decimal(20,2))amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='2'", con);

            da.Fill(ds, "limit");
            if (ds.Tables["limit"].Rows.Count > 0)
            {              
                ViewState["no"] = ds.Tables["limit"].Rows[0]["pono"].ToString();
                ViewState["podate"] = ds.Tables["limit"].Rows[0]["Amended_date"].ToString();               
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource =null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }  
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string result1 = "";
            cmd1 = new SqlCommand("CheckSubBalance_sp", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Clear();
            cmd1.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["id"].ToString());
            cmd1.Connection = con;
            con.Open();
            result1 = cmd1.ExecuteScalar().ToString();
            con.Close();
            if (result1 == "Avaliable")
            {
                DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
                cmd = new SqlCommand("sp_deleteamendsppo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex]["id"].ToString());
                cmd.Parameters.AddWithValue("@User", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@rejdate", myDate);
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                JavaScript.UPAlert(Page, msg);
                con.Close();
                fillgrid();
            }
            else
            {
                JavaScript.UPAlert(Page, result1);
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page, ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["roles"].ToString() == "Project Manager")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton lkb1 = e.Row.FindControl("linkDeleteCust") as LinkButton;
                //lkb1.Enabled = false;
                //e.Row.Cells[11].Enabled = false;
            } 
        }
    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {

        da = new SqlDataAdapter("select distinct pono from SPPO where dca_code in (select dca_code from dcatype where status='Active' and cc_type in (Select cc_subtype FROM cost_center where cc_code='" + ddlcccode.SelectedValue + "')) and vendor_id='" + ddlvendor.SelectedValue + "' and status='3' and cc_code='" + ddlcccode.SelectedValue + "'", con);
        da.Fill(ds, "pono");
        ddlpono.DataValueField = "pono";
        ddlpono.DataSource = ds.Tables["pono"];
        ddlpono.DataBind();
        ddlpono.Items.Insert(0, "Select PONO");

    }
    protected void ddlpono_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvendor.SelectedValue != "" && ddlcccode.SelectedValue != "" && ddlpono.SelectedValue != "")
        {
            da = new SqlDataAdapter("SELECT 1 as IsExists FROM amend_sppo  where pono='" + ddlpono.SelectedValue + "' and status in ('1','1A','1B','2')", con);
            da.Fill(ds, "checks");
            if (ds.Tables["checks"].Rows.Count == 0)
            {
                ViewInvoiceInfo(ddlvendor.SelectedValue, ddlcccode.SelectedValue, ddlpono.SelectedValue);
                tramounts.Visible = true;
                trbtns.Visible = true;
                btnadditem.Visible = true;
                hfreturnAmt.Value = "0";
                txtreturnAmt.Text = "0";
                hfamendamt.Value = "0";
                hfamendamt1.Value = "0";
                txtamendamt.Text = "0";
                hftotalpo.Value = "0";
                txttotalamt.Text = "0";
                da = new SqlDataAdapter("select CAST([PO_Value] AS Decimal(20,2))as po_value,CAST([balance] AS Decimal(20,2))as balance from sppo where pono='" + ddlpono.SelectedItem.Text + "' and cc_code='" + ddlcccode.SelectedValue + "';select CAST(isnull(sum([Amended_amount]),0)-ISNULL(SUM(Substract_Amt),0) AS Decimal(20,2))as AmendedAmount from amend_sppo where pono='" + ddlpono.SelectedItem.Text + "' and status='3' group by pono ", con);
                da.Fill(ds, "Amtcheck");
                if (ds.Tables["Amtcheck"].Rows.Count > 0)
                {
                    lblActpovalue.Text = ds.Tables["Amtcheck"].Rows[0].ItemArray[0].ToString();
                    lblActpobalance.Text = ds.Tables["Amtcheck"].Rows[0].ItemArray[1].ToString();
                    txttotalamt.Text = ds.Tables["Amtcheck"].Rows[0].ItemArray[0].ToString();
                    hftotalpo.Value = ds.Tables["Amtcheck"].Rows[0].ItemArray[0].ToString();
                    if (ds.Tables["Amtcheck1"].Rows.Count > 0)
                    {
                        hfamendvalue.Value = ds.Tables["Amtcheck1"].Rows[0].ItemArray[0].ToString();
                    }
                    else
                    {
                        hfamendvalue.Value = "0";
                    }
                    //hfsumpobal.Value = ((Convert.ToDecimal(hfamendvalue.Value) + Convert.ToDecimal(lblActpovalue.Text)).ToString());
                    hfsumpobal.Value = ds.Tables["Amtcheck"].Rows[0].ItemArray[0].ToString();
                    txttotalamt.Text = hfsumpobal.Value;
                }
                else
                {
                    lblActpovalue.Text = "";
                    lblActpobalance.Text = "";
                    txttotalamt.Text = "0";
                    hftotalpo.Value = "0";
                    hfamendvalue.Value = "0";
                }
            }
            else
            {
                ddlpono.SelectedIndex = 0;
                JavaScript.UPAlert(Page, "The Selected PO is on Amendment");
            }
        }
        else
        {
            JavaScript.UPAlert(Page, "Please Select vendorcode , cost center and po number");
        }
    }
    protected void ddlvendor_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("select distinct s.cc_code,s.cc_code+' ('+cc_name+')' Name,cc_name from SPPO s join Cost_Center c on s.cc_code=c.cc_code where vendor_id='" + ddlvendor.SelectedValue + "'", con);
            da.Fill(ds, "fill");
            ddlcccode.DataValueField = "cc_code";
            ddlcccode.DataTextField = "Name";
            ddlcccode.DataSource = ds.Tables["fill"];
            ddlcccode.DataBind();
            ddlcccode.Items.Insert(0, "Select CC Code");
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string Ids = "";      
        string Descs = "";
        string MDescs = "";

        try
        {
            DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);

            if (txpodate.Text != "" && txttotalamt.Text != "")
            {
                foreach (GridViewRow record in GridView2.Rows)
                {
                    if ((record.FindControl("chkSelectamditems") as CheckBox).Checked)
                    {
                        TextBox txtdesc = (TextBox)record.FindControl("txtdescamend");
                        string desc = txtdesc.Text;

                        if (desc != "")
                        {
                            Ids = Ids + GridView2.DataKeys[record.RowIndex]["itemid"].ToString() + ",";                            
                            Descs = Descs + desc + ",";
                        }
                    }
                }
                foreach (GridViewRow record in grdpodesc.Rows)
                {
                    if ((record.FindControl("chkSelectterms") as CheckBox).Checked)
                    {
                        TextBox txtterm = (TextBox)record.FindControl("txttermsamd");
                        string termdesc = txtterm.Text;
                        if (termdesc != "")
                        {
                            MDescs = MDescs + termdesc + "$";
                        }
                    }
                }
                cmd = new SqlCommand("sp_ServiceProviderverifypo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Ids", Ids));               
                cmd.Parameters.Add(new SqlParameter("@Descs", Descs));
                cmd.Parameters.Add(new SqlParameter("@Terms", MDescs));

                cmd.Parameters.Add(new SqlParameter("@id", ViewState["id"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@Amended_date", txpodate.Text));
                cmd.Parameters.Add(new SqlParameter("@PONO", lbpono.Text));
                cmd.Parameters.Add(new SqlParameter("@oldpo", ViewState["oldpovalue"].ToString()));
                //cmd.Parameters.Add(new SqlParameter("@remarks", txremarks.Text));
                cmd.Parameters.Add(new SqlParameter("@Amended_amount", txtamdamount.Text));
                cmd.Parameters.Add(new SqlParameter("@roles", Session["roles"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@modified_date", myDate));
                cmd.Parameters.Add(new SqlParameter("@user", Session["user"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@ReturnAmt", txtreturnAmt.Text));
                cmd.Parameters.Add(new SqlParameter("@Balpo_amount", txpovalue.Text));
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                //string msg="";
                if (msg == "PO Verified")
                    JavaScript.UPAlertRedirect(Page, msg, "AmendSPPO.aspx");
                
                else            
  
                    JavaScript.UPAlert(Page,msg);                    
             
                con.Close();
                GridView1.EditIndex = -1;
                fillgrid();
            }
            else
            {
                if (txpodate.Text == "")
                    txtpodate.Focus();
                JavaScript.UPAlert(Page,"Please Enter Value in Blank Fields");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        tblamendsppo.Visible = false;
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewSelectedIndex]["id"].ToString();
        ViewState["id"] = GridView1.DataKeys[e.NewSelectedIndex]["id"].ToString();
        ViewState["oldpovalue"] = GridView1.DataKeys[e.NewSelectedIndex]["po_value"].ToString();
        ViewState["CCType"] = GridView1.DataKeys[e.NewSelectedIndex]["cc_type"].ToString();
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='1'", con);
                da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2))po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0))[PO Value],vendor_name,s.cc_code,s.vendor_id from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1' and a.id='" + id + "'; select description,unit,quantity,cast(rate as decimal(10,2))as Rate,cast(Amount as decimal(10,2))as Amount,PO_Type,Item_status,id as itemid,isnull(CAST(ClientRate AS Decimal(20,2)),0) as ClientRate,isnull(CAST(PRWRate AS Decimal(20,2)),0) as PRWRate from SPPO_Items where Po_Id='" + id + "'; select CAST((isnull(Amended_Amount,0)) AS Decimal(20,2))as Amended_value,CAST((isnull(Substract_Amt,0)) AS Decimal(20,2))as Subst_value,remarks from Amend_sppo where Id='" + id + "' ", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='1'", con);
                da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0))[PO Value],vendor_name,s.cc_code,s.vendor_id from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1A' and a.id='" + id + "'; select description,unit,quantity,cast(rate as decimal(10,2))as Rate,cast(Amount as decimal(10,2))as Amount,PO_Type,Item_status,id as itemid,isnull(CAST(ClientRate AS Decimal(20,2)),0) as ClientRate,isnull(CAST(PRWRate AS Decimal(20,2)),0) as PRWRate from SPPO_Items where Po_Id='" + id + "' ;select CAST((isnull(Amended_Amount,0)) AS Decimal(20,2))as Amended_value,CAST((isnull(Substract_Amt,0)) AS Decimal(20,2))as Subst_value,remarks from Amend_sppo where Id='" + id + "' ", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='1'", con);
                da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0))[PO Value],vendor_name,s.cc_code,s.vendor_id from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1B' and a.id='" + id + "'; select description,unit,quantity,cast(rate as decimal(10,2))as Rate,cast(Amount as decimal(10,2))as Amount,PO_Type,Item_status,id as itemid,isnull(CAST(ClientRate AS Decimal(20,2)),0) as ClientRate,isnull(CAST(PRWRate AS Decimal(20,2)),0) as PRWRate from SPPO_Items where Po_Id='" + id + "' ;select CAST((isnull(Amended_Amount,0)) AS Decimal(20,2))as Amended_value,CAST((isnull(Substract_Amt,0)) AS Decimal(20,2))as Subst_value,remarks from Amend_sppo where Id='" + id + "' ", con);
            else if (Session["roles"].ToString() == "HoAdmin")
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='1'", con);
                da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0))[PO Value],vendor_name,s.cc_code,s.vendor_id from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='1BN' and a.id='" + id + "'; select description,unit,quantity,cast(rate as decimal(10,2))as Rate,cast(Amount as decimal(10,2))as Amount,PO_Type,Item_status,id as itemid,isnull(CAST(ClientRate AS Decimal(20,2)),0) as ClientRate,isnull(CAST(PRWRate AS Decimal(20,2)),0) as PRWRate from SPPO_Items where Po_Id='" + id + "' ;select CAST((isnull(Amended_Amount,0)) AS Decimal(20,2))as Amended_value,CAST((isnull(Substract_Amt,0)) AS Decimal(20,2))as Subst_value,remarks from Amend_sppo where Id='" + id + "' ", con);
            else
                //da = new SqlDataAdapter("select a.id,a.pono,replace((isnull(p.po_value,0)),'.0000','.00')as po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(p.po_value,0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from Amend_sppo a join SPPO p on a.pono=p.pono where a.status='2'", con);
                //da = new SqlDataAdapter("select a.id,a.pono,i.[PO Value] po_value,replace((isnull(a.Amended_amount,0)),'.0000','.00')as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,replace((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)),'.0000','.00')as [POTotal] from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0)+isnull(amended_amount,0))[PO Value] from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,status from sppo)s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='2'", con);
                da = new SqlDataAdapter("select a.id,a.pono,CAST(i.[PO Value] AS Decimal(20,2)) po_value,CAST((isnull(a.Amended_amount,0)) AS Decimal(20,2))as Amended_amount,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,CAST((isnull(i.[PO Value],0)+isnull(a.Amended_amount,0)) AS Decimal(20,2))as [POTotal],vendor_name,cc_code,i.vendor_id from (Select (case when s.pono is not null then s.pono else a.pono end)pono,(isnull(po_value,0))[PO Value],vendor_name,s.cc_code,s.vendor_id from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,vendor_name,sp.vendor_id from sppo sp  join vendor v on sp.vendor_id=v.vendor_id )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount from amend_sppo where status in ('3') group by pono)a on a.pono=s.pono))i join amend_sppo a on i.pono=a.pono where a.status='2' and a.id='" + id + "'; select description,unit,quantity,cast(rate as decimal(10,2))as Rate,cast(Amount as decimal(10,2))as Amount,PO_Type,Item_status,id as itemid,isnull(CAST(ClientRate AS Decimal(20,2)),0) as ClientRate,isnull(CAST(PRWRate AS Decimal(20,2)),0) as PRWRate from SPPO_Items where Po_Id='" + id + "' ;select CAST((isnull(Amended_Amount,0)) AS Decimal(20,2))as Amended_value,CAST((isnull(Substract_Amt,0)) AS Decimal(20,2))as Subst_value,remarks from Amend_sppo where Id='" + id + "' ", con);

            da.Fill(ds, "filltable");
            if (ds.Tables["filltable"].Rows.Count > 0)
            {
                da = new SqlDataAdapter("select cc_type from cost_center where cc_code='" + ds.Tables["filltable"].Rows[0]["cc_code"].ToString() + "'", con);
                da.Fill(ds, "cctype");
                ViewState["ctype"] = ds.Tables["cctype"].Rows[0][0].ToString();
                txpovalue.Enabled = false;
                txpovalue.Text = ds.Tables["filltable"].Rows[0]["Amended_amount"].ToString();
                lbvname.Text = ds.Tables["filltable"].Rows[0]["vendor_name"].ToString();
                lbpono.Text = ds.Tables["filltable"].Rows[0]["pono"].ToString();
                txpodate.Text = ds.Tables["filltable"].Rows[0]["Amended_date"].ToString();
                txoldpo.Text = ds.Tables["filltable"].Rows[0]["po_value"].ToString().Replace(".0000", ".00");
                //txpovalue.Text = ds.Tables["filltable"].Rows[0]["Amended_amount"].ToString();
                //lbtotal.Text = ds.Tables["filltable"].Rows[0]["Amended_amount"].ToString();
                //txtotal.Text = ds.Tables["filltable"].Rows[0]["POTotal"].ToString();
                //txremarks.Text = ds.Tables["filltable"].Rows[0]["remarks"].ToString();

                if (ds.Tables["filltable2"].Rows.Count > 0)
                {
                    txpovalue.Text = ((Convert.ToDecimal(ds.Tables["filltable2"].Rows[0][0].ToString())) - (Convert.ToDecimal(ds.Tables["filltable2"].Rows[0][1].ToString()))).ToString();//ds.Tables["filltable"].Rows[0]["Amended_amount"].ToString();
                    lbtotal.Text = ((Convert.ToDecimal(ds.Tables["filltable2"].Rows[0][0].ToString())) - (Convert.ToDecimal(ds.Tables["filltable2"].Rows[0][1].ToString()))).ToString(); ;
                    txtotal.Text = ((Convert.ToDecimal(txoldpo.Text)) + (Convert.ToDecimal(txpovalue.Text))).ToString();
                }
                else
                {
                    txpovalue.Text = ds.Tables["filltable"].Rows[0]["Amended_amount"].ToString();
                    lbtotal.Text = ds.Tables["filltable"].Rows[0]["Amended_amount"].ToString();
                    //txtotal.Text = (Convert.ToDecimal(txpovalue.Text) + Convert.ToDecimal(txoldpo.Text)).ToString();
                }
                da = new SqlDataAdapter("Getamendsppodetails", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@POID", SqlDbType.VarChar).Value = id;
                da.SelectCommand.Parameters.AddWithValue("@PONO", SqlDbType.VarChar).Value = ds.Tables["filltable"].Rows[0]["pono"].ToString();
                da.Fill(ds, "Checkamendpo");
                if (ds.Tables["Checkamendpo"].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables["Checkamendpo"];
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }

                if (ds.Tables["filltable2"].Rows.Count > 0)
                {
                    if (Session["roles"].ToString() == "Project Manager")
                    {
                        da = new SqlDataAdapter("Select remarks,id,status from sppo where pono='" + lbpono.Text + "' and status='3'; Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('3') order by id asc", con);
                    }
                    else if (Session["roles"].ToString() == "PurchaseManager")
                    {
                        if (ViewState["CCType"].ToString() == "Performing")
                            da = new SqlDataAdapter("Select remarks,id,status from sppo where pono='" + lbpono.Text + "' and status='3' ; Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('3') order by id asc", con);
                        else
                            da = new SqlDataAdapter("Select remarks,id,status  from sppo where pono='" + lbpono.Text + "' and status='3' ; Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('3') order by id asc", con);
                    }
                    else if (Session["roles"].ToString() == "Chief Material Controller")
                    {
                        if (ViewState["CCType"].ToString() == "Performing")
                            da = new SqlDataAdapter("Select remarks,id,status from sppo where pono='" + lbpono.Text + "' and status='3' ; Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('3') order by id asc", con);
                        else
                            da = new SqlDataAdapter("Select remarks,id,status  from sppo where pono='" + lbpono.Text + "' and status='3' ; Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('3') order by id asc", con);
                    }
                    else if (Session["roles"].ToString() == "HoAdmin")
                    {
                        if (ViewState["CCType"].ToString() == "Performing")
                            da = new SqlDataAdapter("Select remarks,id,status from sppo where pono='" + lbpono.Text + "' and status='3' ; Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('3') order by id asc", con);
                        else
                            da = new SqlDataAdapter("Select remarks,id,status ,'sppo' as Type from sppo where pono='" + lbpono.Text + "' and status='3' ; Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('3') order by id asc", con);
                    }
                    else if (Session["roles"].ToString() == "SuperAdmin")
                    {
                        da = new SqlDataAdapter("Select remarks from sppo where pono='" + lbpono.Text + "' and status='3' ; Select remarks from amend_sppo where pono='" + lbpono.Text + "' and status in ('3') order by id asc", con);
                    }
                    da.Fill(ds, "remarksadold");
                    string remarksamendold = "";
                    remarksamendold = ds.Tables["remarksadold"].Rows[0][0].ToString().Replace("'", " ").Replace(",", " ");
                    if (ds.Tables["remarksadold1"].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables["remarksadold1"].Rows.Count; i++)
                        {
                            remarksamendold = remarksamendold + "" + ds.Tables["remarksadold1"].Rows[i][0].ToString().Replace("'", " ").Replace(",", " ");
                        }
                    }
                    da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + remarksamendold + "','$')", con);
                    da.Fill(ds, "splitamendold");                    
                    grdpodescold.DataSource = ds.Tables["splitamendold"];
                    grdpodescold.DataBind();

                    if (Session["roles"].ToString() == "Project Manager")
                    {
                        da = new SqlDataAdapter("Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('1') order by id asc", con);
                    }
                    else if (Session["roles"].ToString() == "PurchaseManager")
                    {
                        if (ViewState["CCType"].ToString() == "Performing")
                            da = new SqlDataAdapter("Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('1A') order by id asc", con);
                        else
                            da = new SqlDataAdapter("Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('1') order by id asc", con);
                    }
                    else if (Session["roles"].ToString() == "Chief Material Controller")
                    {
                        if (ViewState["CCType"].ToString() == "Performing")
                            da = new SqlDataAdapter("Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('1B') order by id asc", con);
                        else
                            da = new SqlDataAdapter("Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('1B') order by id asc", con);
                    }
                    else if (Session["roles"].ToString() == "HoAdmin")
                    {
                        if (ViewState["CCType"].ToString() == "Performing")
                            da = new SqlDataAdapter("Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('1BN') order by id asc", con);
                        else
                            da = new SqlDataAdapter("Select remarks,id,status from amend_sppo where pono='" + lbpono.Text + "' and status in ('1BN') order by id asc", con);
                    }
                    else if (Session["roles"].ToString() == "SuperAdmin")
                    {
                        da = new SqlDataAdapter("Select remarks from amend_sppo where pono='" + lbpono.Text + "' and status in ('2') order by id asc", con);
                    }
                    da.Fill(ds, "remarksad");
                    string remarksamend = "";                    
                    if (ds.Tables["remarksad"].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables["remarksad"].Rows.Count; i++)
                        {
                            remarksamend = remarksamend + "" + ds.Tables["remarksad"].Rows[i][0].ToString().Replace("'", " ").Replace(",", " ");
                        }
                    }
                    da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + remarksamend + "','$')", con);
                    da.Fill(ds, "splitamend");
                    ViewState["Curtblterma"] = ds.Tables["splitamend"];
                    grdpodesc.DataSource = ds.Tables["splitamend"];
                    grdpodesc.DataBind();
                    
                    if (ds.Tables["filltable2"].Rows[0][1].ToString() != "")
                    {
                        txtamendpreviouspovalue.Text = ds.Tables["filltable2"].Rows[0][1].ToString();
                        txtamendpreviouspovalue.BackColor = Color.Red;
                    }
                    else
                    {
                        txtamendpreviouspovalue.Text = "0";
                        txtamendpreviouspovalue.BackColor = Color.Red;
                    }
                    if (ds.Tables["filltable2"].Rows[0][0].ToString() != "")
                    {
                        txtamdamount.Text = ds.Tables["filltable2"].Rows[0][0].ToString();
                        txtamdamount.BackColor = Color.Green;
                    }
                    else
                    {
                        txtamdamount.Text = "0";
                        txtamdamount.BackColor = Color.Green;
                    }
                    decimal txtamendpreviouspovalue1 = Convert.ToDecimal(txtamendpreviouspovalue.Text);
                    decimal txtamdamount1 = Convert.ToDecimal(txtamdamount.Text);
                  
                    if (txtamendpreviouspovalue1 > txtamdamount1)
                    {
                        txtamdpoval.Text = txtotal.Text;
                        //txtamdpoval.Text = ((Convert.ToDecimal(txpovalue.Text) + Convert.ToDecimal(txoldpo.Text)) - (Convert.ToDecimal(txtamendpreviouspovalue.Text))).ToString();
                        txtamdpoval.BackColor = Color.Red;
                    }
                    else if (txtamendpreviouspovalue1 < txtamdamount1)
                    {
                        txtamdpoval.Text = txtotal.Text;
                        //txtamdpoval.Text = ((Convert.ToDecimal(txpovalue.Text) + Convert.ToDecimal(txoldpo.Text)) - (Convert.ToDecimal(txtamendpreviouspovalue.Text))).ToString();
                        txtamdpoval.BackColor = Color.Green;
                    }
                    else
                    {
                        txtamdpoval.Text = ((Convert.ToDecimal(txpovalue.Text) + Convert.ToDecimal(txoldpo.Text)) - (Convert.ToDecimal(txtamendpreviouspovalue.Text))).ToString();
                        //txtamdpoval.Text = (Convert.ToDecimal(txtamdamount.Text) - Convert.ToDecimal(txtamendpreviouspovalue.Text)).ToString();
                    }
                }
                da = new SqlDataAdapter("Select Approved_Users from Amend_sppo where id='" + id + "'", con);
                da.Fill(ds, "roles");
                if (ds.Tables["roles"].Rows.Count > 0)
                {
                    string rolesamend = ds.Tables["roles"].Rows[0][0].ToString();
                    da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + rolesamend + "',',')", con);
                    da.Fill(ds, "splitrole");
                    DataTable dtra = ds.Tables["splitrole"];
                    ViewState["Curtblroles"] = dtra;
                    gvusers.DataSource = dtra;
                    gvusers.DataBind();
                }
            }
            else
            {
                tblamendsppo.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        finally
        {
            con.Close();
        }
        tblamendsppo.Visible = true;
       
    }
    public int j = 0;
    public int k = 1;
    protected void gvusers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["Curtblroles"] != null)
                {
                    DataTable Objdt = ViewState["Curtblroles"] as DataTable;
                    if (Objdt.Rows[j][0].ToString() != "")
                    {
                        //da = new SqlDataAdapter("select Roles, (first_name+'  '+middle_name+'  '+last_name)as name ,ur.User_Name from user_roles ur join Employee_Data ed on ur.User_Name=ed.User_Name where ur.User_Name='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da = new SqlDataAdapter("select (first_name+'  '+middle_name+'  '+last_name)as name,USER_NAME from Employee_Data where USER_NAME='" + Objdt.Rows[j][0].ToString() + "'", con);
                        da.Fill(ds, "userroles");                      
                        if (k == 1)
                        {
                            e.Row.Cells[1].Text = "Sr.Accountant";
                            e.Row.Cells[0].Text = "Prepared By";
                        }
                        else if (k == 2 && ViewState["ctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "Project Manager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 3 && ViewState["ctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "PurchaseManager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 4 && ViewState["ctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "Chief Material Controller";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 5 && ViewState["ctype"].ToString() == "Performing")
                        {
                            e.Row.Cells[1].Text = "HoAdmin";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 2 && (ViewState["ctype"].ToString() == "Non-Performing" || ViewState["ctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[1].Text = "PurchaseManager";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 3 && (ViewState["ctype"].ToString() == "Non-Performing" || ViewState["ctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[1].Text = "Chief Material Controller";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        else if (k == 4 && (ViewState["ctype"].ToString() == "Non-Performing" || ViewState["ctype"].ToString() == "Capital"))
                        {
                            e.Row.Cells[1].Text = "HoAdmin";
                            e.Row.Cells[0].Text = "Verified By";
                        }
                        //e.Row.Cells[1].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                        e.Row.Cells[2].Text = ds.Tables["userroles"].Rows[j].ItemArray[0].ToString();
                        //e.Row.Cells[3].Text = ds.Tables["userroles"].Rows[j].ItemArray[2].ToString();

                    }
                }
                j = j + 1;
                k = k + 1;
            }

        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
  
    #region Add items start
    //protected void BindGridview()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("S.No", typeof(int));
    //    dt.Columns.Add("chkSelectitems", typeof(string));
    //    dt.Columns.Add("Description", typeof(string));
    //    dt.Columns.Add("Unit", typeof(string));
    //    dt.Columns.Add("Quantity", typeof(string));
    //    dt.Columns.Add("Rate", typeof(string));
    //    dt.Columns.Add("ddltype", typeof(string));
    //    dt.Columns.Add("Amendqty", typeof(string));
    //    dt.Columns.Add("Amount", typeof(string));
    //    dt.Columns.Add("Id", typeof(string));
    //    DataRow dr = dt.NewRow();
    //    //dr["rowid"] = 1;
    //    dr["chkSelectitems"] = string.Empty;
    //    dr["Description"] = string.Empty;
    //    dr["Unit"] = string.Empty;
    //    dr["Quantity"] = string.Empty;
    //    dr["Rate"] = string.Empty;
    //    dr["ddltype"] = string.Empty;
    //    dr["Amendqty"] = string.Empty;
    //    dr["Amount"] = string.Empty;
    //    dr["Id"] = string.Empty;
    //    dt.Rows.Add(dr);
    //    ViewState["Curtbl"] = dt;
    //    gvDetails.DataSource = dt;
    //    gvDetails.DataBind();
    //    loadtype();
    //}
    //private void AddNewRow()
    //{
    //    int rowIndex = 0;

    //    if (ViewState["Curtbl"] != null)
    //    {
    //        DataTable dt = (DataTable)ViewState["Curtbl"];
    //        DataRow drCurrentRow = null;
    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int i = 1; i <= dt.Rows.Count; i++)
    //            {
    //                TextBox txtitemdesc = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtitemdesc");
    //                TextBox txtunit = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtunit");
    //                TextBox txtquantity = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtquantity");
    //                TextBox txtrate = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("txtrate");
    //                DropDownList ddltype = (DropDownList)gvDetails.Rows[rowIndex].Cells[6].FindControl("ddltype");
    //                TextBox txtamendquantity = (TextBox)gvDetails.Rows[rowIndex].Cells[7].FindControl("txtamendquantity");
    //                TextBox txtamount = (TextBox)gvDetails.Rows[rowIndex].Cells[8].FindControl("txtamount");

    //                drCurrentRow = dt.NewRow();
    //                //drCurrentRow["rowid"] = i + 1;
    //                dt.Rows[i - 1]["Description"] = txtitemdesc.Text;
    //                dt.Rows[i - 1]["Unit"] = txtunit.Text;
    //                dt.Rows[i - 1]["Quantity"] = txtquantity.Text;
    //                dt.Rows[i - 1]["Rate"] = txtrate.Text;
    //                dt.Rows[i - 1]["ddltype"] = ddltype.SelectedItem.Text;
    //                dt.Rows[i - 1]["Amendqty"] = txtamendquantity.Text;
    //                dt.Rows[i - 1]["Amount"] = txtamount.Text;
    //                rowIndex++;
    //            }
    //            dt.Rows.Add(drCurrentRow);
    //            ViewState["Curtbl"] = dt;
    //            gvDetails.DataSource = dt;
    //            gvDetails.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("ViewState Value is Null");
    //    }
    //    SetOldData();
    //}
    //private void SetOldData()
    //{
    //    int rowIndex = 0;
    //    if (ViewState["Curtbl"] != null)
    //    {
    //        DataTable dt = (DataTable)ViewState["Curtbl"];
    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                //TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtGName");
    //                CheckBox chk = (CheckBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("chkSelectitems");
    //                TextBox txtitemdesc = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtitemdesc");
    //                TextBox txtunit = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtunit");
    //                TextBox txtquantity = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtquantity");
    //                TextBox txtrate = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("txtrate");
    //                DropDownList ddltype = (DropDownList)gvDetails.Rows[rowIndex].Cells[6].FindControl("ddltype");
    //                TextBox txtamendquantity = (TextBox)gvDetails.Rows[rowIndex].Cells[7].FindControl("txtamendquantity");
    //                TextBox txtamount = (TextBox)gvDetails.Rows[rowIndex].Cells[8].FindControl("txtamount");
    //                //ddltype.Items.Clear();
    //                loadtype();
    //                if (i < dt.Rows.Count - 1)
    //                {
    //                    txtitemdesc.Text = dt.Rows[i]["Description"].ToString();
    //                    txtunit.Text = dt.Rows[i]["Unit"].ToString();
    //                    txtquantity.Text = dt.Rows[i]["Quantity"].ToString();
    //                    txtrate.Text = dt.Rows[i]["Rate"].ToString();
    //                    ddltype.SelectedItem.Text = dt.Rows[i]["ddltype"].ToString();
    //                    txtamendquantity.Text = dt.Rows[i]["Amendqty"].ToString();
    //                    txtamount.Text = dt.Rows[i]["Amount"].ToString();
    //                }
    //                rowIndex++;
    //            }
    //        }
    //    }
    //}
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    AddNewRow();
    //    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
    //}
    //protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        if (ViewState["Curtbl"] != null)
    //        {
    //            DataTable dt = (DataTable)ViewState["Curtbl"];
    //            DataRow drCurrentRow = null;
    //            int rowIndex = Convert.ToInt32(e.RowIndex);
    //            if (dt.Rows.Count > 1)
    //            {
    //                dt.Rows.Remove(dt.Rows[rowIndex]);
    //                drCurrentRow = dt.NewRow();
    //                ViewState["Curtbl"] = dt;
    //                gvDetails.DataSource = dt;
    //                gvDetails.DataBind();
    //                for (int i = 0; i < gvDetails.Rows.Count - 1; i++)
    //                {
    //                    gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
    //                }
    //                SetOldData();
    //            }
    //        }
    //        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
    //    }

    //    catch (Exception ex)
    //    {
    //        Utilities.CatchException(ex);
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}
    //private decimal Amount = (decimal)0.0;
    private decimal AdditionAmount = (decimal)0.0;
    private decimal SubstractionAmount = (decimal)0.0;


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string lsDataKeyValue = GridView2.DataKeys[e.Row.RowIndex].Values[0].ToString();
            TextBox txtamd = (TextBox)e.Row.FindControl("txtdescamend");
            if (lsDataKeyValue == "New")
            {
                txtamd.Enabled = true;
            }
            else
            {
                txtamd.Enabled = false;
            }
            //Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            if (e.Row.Cells[7].Text == "Subtract")
            {
                if (decimal.Parse(e.Row.Cells[8].Text) > decimal.Parse(e.Row.Cells[7].Text))
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Green;
                }
                e.Row.ForeColor = Color.Green;
                SubstractionAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            else
            {
                if (decimal.Parse(e.Row.Cells[8].Text) > decimal.Parse(e.Row.Cells[7].Text)) 
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                    //e.Row.Cells[8].Style.Add("text-decoration", "blink");
                    e.Row.Style.Add("text-decoration", "blink");
                }
                else
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Green;
                }
                e.Row.ForeColor = Color.Red;
                AdditionAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Text = String.Format("Rs. {0:#,##,##,###.00}", (AdditionAmount - SubstractionAmount));

        }
       
    }
    public void loadtype()
    {
        try
        {
            foreach (GridViewRow record in gvDetails.Rows)
            {
                DropDownList type = (DropDownList)record.FindControl("ddltype");
                if (type.SelectedValue == "")
                {
                    type.Items.Insert(0, new ListItem("Select", "Select"));
                    type.Items.Insert(1, new ListItem("Add", "Add"));
                    type.Items.Insert(2, new ListItem("Subtract", "Subtract"));
                    type.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    public int K = 0;
    public void ViewInvoiceInfo(string venid, string cc_code, string pono)
    {
        try
        {
            loadtype();
            da = new SqlDataAdapter("Getsppodetails", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Venid", venid);
            da.SelectCommand.Parameters.AddWithValue("@CC_Code", cc_code);
            da.SelectCommand.Parameters.AddWithValue("@PONO", pono);
            DataSet Objds = new DataSet();
            da.Fill(Objds, "InvoiceInfo");
            if (Objds.Tables["InvoiceInfo"].Rows.Count > 0)
            {
                DataTable dtitems = new DataTable();
                dtitems.Columns.Add("S.No", typeof(int));
                dtitems.Columns.Add("chkSelectitems", typeof(string));
                dtitems.Columns.Add("Description", typeof(string));
                dtitems.Columns.Add("Unit", typeof(string));
                dtitems.Columns.Add("Quantity", typeof(string));
                dtitems.Columns.Add("Rate", typeof(string));
                //dtitems.Columns.Add("ddltype", typeof(string));
                //dtitems.Columns.Add("Amendqty", typeof(string));
                dtitems.Columns.Add("Amount", typeof(string));
                //dtitems.Columns.Add("Id", typeof(string));
                for (int i = 0; i < Objds.Tables["InvoiceInfo"].Rows.Count; i++)
                {
                    DataRow dr = dtitems.NewRow();
                    dr["chkSelectitems"] = string.Empty;
                    dr["Description"] = Objds.Tables["InvoiceInfo"].Rows[i]["Remarks"].ToString();
                    dr["Unit"] = Objds.Tables["InvoiceInfo"].Rows[i]["Units"].ToString();
                    dr["Quantity"] = Objds.Tables["InvoiceInfo"].Rows[i]["quantity"].ToString();
                    dr["Rate"] = Objds.Tables["InvoiceInfo"].Rows[i]["Rate"].ToString();
                    //dr["Id"] = Objds.Tables["InvoiceInfo"].Rows[i]["Id"].ToString();
                    //dr["ddltype"] = Objds.Tables["InvoiceInfo"].Rows[i]["Type"].ToString();
                    //dr["Amendqty"] = Objds.Tables["InvoiceInfo"].Rows[i]["AmendQty"].ToString();
                    //dr["Amount"] = Objds.Tables["InvoiceInfo"].Rows[i]["Amt"].ToString();
                    dtitems.Rows.Add(dr);
                }
                ViewState["Curtbl"] = dtitems;
                gvDetails.DataSource = dtitems;
                gvDetails.DataBind();

            }
            else
            {
                gvDetails.DataSource = null;
                gvDetails.DataBind();
            }

            da = new SqlDataAdapter("Select remarks,id from sppo where pono='" + pono + "' and cc_code='" + cc_code + "' and vendor_id='" + venid + "' and status='3'; Select remarks,id from amend_sppo where pono='" + pono + "' and status='3' order by id asc", con);
            da.Fill(ds, "remarks");
            if (ds.Tables["remarks"].Rows.Count > 0)
            {
               
                string remarksamend = "";                
                remarksamend = ds.Tables["remarks"].Rows[0][0].ToString().Replace("'", " ").Replace(","," ");
                if (ds.Tables["remarks1"].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["remarks1"].Rows.Count; i++)
                    {
                        remarksamend = remarksamend + "" + ds.Tables["remarks1"].Rows[i][0].ToString().Replace("'", " "); ;
                    }
                }               
                da = new SqlDataAdapter("select * from dbo.fnSplitStringsppodesc('" + remarksamend + "','$')", con);
                da.Fill(ds, "splitremarks");
                DataTable dtterms = new DataTable();
                //dtterms = ds.Tables["splitremarks"];
                dtterms.Columns.Add("S.No", typeof(int));
                dtterms.Columns.Add("chkSelectterm", typeof(string));
                dtterms.Columns.Add("Descriptionterm", typeof(string));
                dtterms.Columns.Add("Id", typeof(string));
                for (int i = 0; i < ds.Tables["splitremarks"].Rows.Count; i++)
                {
                    DataRow dr = dtterms.NewRow();
                    dr["chkSelectterm"] = string.Empty;
                    dr["Descriptionterm"] = ds.Tables["splitremarks"].Rows[i]["splitdata"].ToString();
                    dr["Id"] = ds.Tables["remarks"].Rows[0]["Id"].ToString();
                    dtterms.Rows.Add(dr);                    
                }
                
                ViewState["Curtblterm"] = dtterms;
                grdterms.DataSource = dtterms;
                grdterms.DataBind();

            }
            else
            {
                BindGridviewterm();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public int c = 0;
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (ViewState["Curtbl"] != null)
                {
                    DataTable Objdt = ViewState["Curtbl"] as DataTable;
                    //DataSet ds = new DataSet();
                    //ds.Tables.Add(Objdt);
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectitems");
                    TextBox txtitemdesc = (TextBox)e.Row.FindControl("txtitemdesc");
                    TextBox txtunit = (TextBox)e.Row.FindControl("txtunit");
                    TextBox txtquantity = (TextBox)e.Row.FindControl("txtquantity");
                    TextBox txtrate = (TextBox)e.Row.FindControl("txtrate");
                    TextBox txtamendquantity = (TextBox)e.Row.FindControl("txtamendquantity");
                    DropDownList type = (DropDownList)e.Row.FindControl("ddltype");
                    type.Items.Insert(0, new ListItem("Select", "Select"));
                    type.Items.Insert(1, new ListItem("Add", "Add"));
                    type.Items.Insert(2, new ListItem("Subtract", "Subtract"));
                    if (Objdt.Rows[c]["Description"].ToString() != "" && Objdt.Rows[c]["Unit"].ToString() != "" && Objdt.Rows[c]["Quantity"].ToString() != "" && Objdt.Rows[c]["Rate"].ToString() != "")
                    {

                        //type.SelectedValue = Objdt.Rows[c]["ddltype"].ToString();
                        txtitemdesc.Text = Objdt.Rows[c]["Description"].ToString();
                        txtunit.Text = Objdt.Rows[c]["Unit"].ToString();
                        txtquantity.Text = Objdt.Rows[c]["Quantity"].ToString();
                        //txtamendquantity.Text = Objdt.Rows[c]["AmendQty"].ToString();
                        txtrate.Text = Objdt.Rows[c]["Rate"].ToString();
                        chk.Checked = true;
                        chk.Enabled = false;
                        txtitemdesc.Enabled = false;
                        txtunit.Enabled = false;
                        txtquantity.Enabled = false;
                        txtrate.Enabled = false;
                        txtamendquantity.Enabled = true;
                        e.Row.Cells[9].Visible = false;
                        type.Enabled = true;
                    }

                }
                c = c + 1;
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    #endregion Add items End
    #region Terms and Conditions start
    public int t = 0;
    protected void grdterms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (ViewState["Curtblterm"] != null)
                {
                    DataTable Objdt = ViewState["Curtblterm"] as DataTable;
                    //DataSet ds = new DataSet();
                    //ds.Tables.Add(Objdt);
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectterms");
                    TextBox txttermdesc = (TextBox)e.Row.FindControl("txtterms");                    
                    if (Objdt.Rows[t]["Descriptionterm"].ToString() != "")
                    {
                        txttermdesc.Text = Objdt.Rows[t]["Descriptionterm"].ToString();                       
                    }
                    if (Objdt.Rows[t]["id"].ToString() != "")
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                        //txttermdesc.Enabled = false;
                        e.Row.Cells[2].Attributes.Add("onKeyDown", "preventBackspace();");
                        e.Row.Cells[2].Attributes.Add("onpaste", "javascript:return false;");
                        e.Row.Cells[2].Attributes.Add("onkeypress", "javascript:return false;");                        
                        e.Row.Cells[3].Visible = false;                        
                    }
                    if (Objdt.Rows[t]["id"].ToString() == "")
                    {
                        chk.Checked = false;
                        chk.Enabled = true;
                        //txttermdesc.Enabled = true;
                        e.Row.Cells[2].Attributes.Add("readonly", "false");
                        e.Row.Cells[3].Visible = true;                       
                    }
                    
                }
                t = t + 1;               
            }
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }

    }
    protected void BindGridviewterm()
    {
        DataTable dtt = new DataTable();
        dtt.Columns.Add("S.No", typeof(int));
        dtt.Columns.Add("chkSelectterm", typeof(string));
        dtt.Columns.Add("Descriptionterm", typeof(string));
        dtt.Columns.Add("Id", typeof(string));
        DataRow dr = dtt.NewRow();
        dr["chkSelectterm"] = string.Empty;
        dr["Descriptionterm"] = string.Empty;
        dr["Id"] = string.Empty;
        dtt.Rows.Add(dr);
        ViewState["Curtblterm"] = dtt;
        grdterms.DataSource = dtt;
        grdterms.DataBind();
    }
    private void AddNewRowterm()
    {
        int rowIndex = 0;

        if (ViewState["Curtblterm"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterm"];
            DataRow drCurrentRow = null;
            if (dtt.Rows.Count > 0)
            {
                for (int i = 1; i <= dtt.Rows.Count; i++)
                {
                    TextBox txtterms = (TextBox)grdterms.Rows[rowIndex].Cells[2].FindControl("txtterms");
                    drCurrentRow = dtt.NewRow();
                    dtt.Rows[i - 1]["Descriptionterm"] = txtterms.Text;
                    rowIndex++;
                }
                dtt.Rows.Add(drCurrentRow);
                ViewState["Curtblterm"] = dtt;
                grdterms.DataSource = dtt;
                grdterms.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState Value is Null");
        }
        SetOldDataterm();
    }
    private void SetOldDataterm()
    {
        int rowIndex = 0;
        if (ViewState["Curtblterm"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterm"];
            if (dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)grdterms.Rows[rowIndex].Cells[0].FindControl("chkSelectterms");
                    TextBox txtterms = (TextBox)grdterms.Rows[rowIndex].Cells[2].FindControl("txtterms");
                    txtterms.Text = dtt.Rows[i]["Descriptionterm"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    protected void btnAddterm_Click(object sender, EventArgs e)
    {
        AddNewRowterm();
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculatenew()", true);
    }
    protected void grdterms_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblterm"] != null)
            {
                DataTable dtt = (DataTable)ViewState["Curtblterm"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dtt.Rows.Count > 1)
                {
                    dtt.Rows.Remove(dtt.Rows[rowIndex]);
                    drCurrentRow = dtt.NewRow();
                    ViewState["Curtblterm"] = dtt;
                    grdterms.DataSource = dtt;
                    grdterms.DataBind();
                    for (int i = 0; i < grdterms.Rows.Count - 1; i++)
                    {
                        grdterms.Rows[i].Cells[1].Text = Convert.ToString(i + 1);
                    }
                    SetOldDataterm();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
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
    #endregion Terms and Conditions End

    protected void btnremoveitems_Click(object sender, EventArgs e)
    {
        try
        {
            btnadditem.Visible = true;
            btnremoveitem.Visible = false;
            gvDetailsnew.DataSource = null;
            gvDetailsnew.DataBind();
            hfamendamt1.Value = Convert.ToInt16(0).ToString();
            txtamendamt.Text = Convert.ToInt16(0).ToString();
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculatenew()", true);
        }
        catch (Exception ex)
        {
            JavaScript.UPAlert(Page, Utilities.CatchException(ex));
        }
    }
    protected void btnAdditems_Click(object sender, EventArgs e)
    {
        btnadditem.Visible = false;
        btnremoveitem.Visible = true;
        btnadditems.Style["display"] = "block";
        BindGridviewnew();
    }
    protected void BindGridviewnew()
    {
        DataTable dt = new DataTable();       
        dt.Columns.Add("chkSelectnew", typeof(string));
        dt.Columns.Add("Descriptionnew", typeof(string));
        dt.Columns.Add("Unitnew", typeof(string));
        dt.Columns.Add("Quantitynew", typeof(string));
        dt.Columns.Add("OurRate", typeof(string));
        dt.Columns.Add("PRWRate", typeof(string));
        dt.Columns.Add("Ratenew", typeof(string));
        dt.Columns.Add("Amountnew", typeof(string));
        DataRow dr = dt.NewRow();    
        dr["chkSelectnew"] = string.Empty;
        dr["Descriptionnew"] = string.Empty;
        dr["Unitnew"] = string.Empty;
        dr["Quantitynew"] = string.Empty;
        dr["OurRate"] = string.Empty;
        dr["PRWRate"] = string.Empty;
        dr["Ratenew"] = string.Empty;
        dr["Amountnew"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["Curtblnew"] = dt;
        gvDetailsnew.DataSource = dt;
        gvDetailsnew.DataBind();
    }
    private void AddNewRownew()
    {
        int rowIndex = 0;

        if (ViewState["Curtblnew"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtblnew"];
            DataRow drCurrentRow = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox txtitemdesc = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[2].FindControl("txtitemdescnew");
                    TextBox txtunit = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[3].FindControl("txtunitnew");
                    TextBox txtquantity = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[4].FindControl("txtquantitynew");
                    TextBox txtourrate = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[5].FindControl("txtourrate");
                    TextBox txtpwrrate = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[6].FindControl("txtprwrate");
                    TextBox txtrate = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[7].FindControl("txtratenew");
                    TextBox txtamount = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[8].FindControl("txtamountnew");
                    drCurrentRow = dt.NewRow();                 
                    dt.Rows[i - 1]["Descriptionnew"] = txtitemdesc.Text;
                    dt.Rows[i - 1]["Unitnew"] = txtunit.Text;
                    dt.Rows[i - 1]["Quantitynew"] = txtquantity.Text;
                    dt.Rows[i - 1]["OurRate"] = txtourrate.Text;
                    dt.Rows[i - 1]["PRWRate"] = txtpwrrate.Text;
                    dt.Rows[i - 1]["Ratenew"] = txtrate.Text;
                    dt.Rows[i - 1]["Amountnew"] = txtamount.Text;
                    rowIndex++;
                }
                dt.Rows.Add(drCurrentRow);
                ViewState["Curtblnew"] = dt;
                gvDetailsnew.DataSource = dt;
                gvDetailsnew.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState Value is Null");
        }
        SetOldDatanew();
    }
    private void SetOldDatanew()
    {
        int rowIndex = 0;
        if (ViewState["Curtblnew"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtblnew"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtGName");
                    CheckBox chk = (CheckBox)gvDetailsnew.Rows[rowIndex].Cells[0].FindControl("chkSelectnew");
                    TextBox txtitemdesc = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[2].FindControl("txtitemdescnew");
                    TextBox txtunit = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[3].FindControl("txtunitnew");
                    TextBox txtquantity = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[4].FindControl("txtquantitynew");
                    TextBox txtourrate = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[5].FindControl("txtourrate");
                    TextBox txtpwrrate = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[6].FindControl("txtprwrate");
                    TextBox txtrate = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[7].FindControl("txtratenew");
                    TextBox txtamount = (TextBox)gvDetailsnew.Rows[rowIndex].Cells[8].FindControl("txtamountnew");
                    if (i < dt.Rows.Count - 1)
                    {
                        //chk.Checked = false;
                        txtitemdesc.Text = dt.Rows[i]["Descriptionnew"].ToString();
                        txtunit.Text = dt.Rows[i]["Unitnew"].ToString();
                        txtquantity.Text = dt.Rows[i]["Quantitynew"].ToString();
                        txtourrate.Text = dt.Rows[i]["OurRate"].ToString();
                        txtpwrrate.Text = dt.Rows[i]["PRWRate"].ToString();
                        txtrate.Text = dt.Rows[i]["Ratenew"].ToString();
                        txtamount.Text = dt.Rows[i]["Amountnew"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        AddNewRownew();
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculatenew()", true);
    }
    protected void gvDetailsnew_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblnew"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtblnew"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtblnew"] = dt;
                    gvDetailsnew.DataSource = dt;
                    gvDetailsnew.DataBind();
                    for (int i = 0; i < gvDetailsnew.Rows.Count - 1; i++)
                    {
                        gvDetailsnew.Rows[i].Cells[1].Text = Convert.ToString(i + 1);
                    }
                    SetOldDatanew();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculatenew()", true);
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

    #region Terms and Conditions start
    //protected void BindGridviewterm()
    //{
    //    DataTable dtt = new DataTable();
    //    dtt.Columns.Add("chkSelectterm", typeof(string));
    //    dtt.Columns.Add("splitdata", typeof(string)); //Descriptionterm
    //    DataRow dr = dtt.NewRow();
    //    dr["chkSelectterm"] = string.Empty;
    //    //dr["Descriptionterm"] = string.Empty;
    //    dr["splitdata"] = string.Empty;
    //    dtt.Rows.Add(dr);
    //    if (ViewState["Curtblterm"] == "")
    //    {
    //        ViewState["Curtblterm"] = dtt;
    //    }        

    //}
    private void AddNewRowterma()
    {
        int rowIndex = 0;

        if (ViewState["Curtblterma"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterma"];
            DataRow drCurrentRow = null;
            if (dtt.Rows.Count > 0)
            {
                for (int i = 1; i <= dtt.Rows.Count; i++)
                {
                    TextBox txtterms = (TextBox)grdpodesc.Rows[rowIndex].Cells[2].FindControl("txttermsamd");
                    drCurrentRow = dtt.NewRow();
                    dtt.Rows[i - 1]["splitdata"] = txtterms.Text;
                    rowIndex++;
                }
                dtt.Rows.Add(drCurrentRow);
                ViewState["Curtblterma"] = dtt;
                grdpodesc.DataSource = dtt;
                grdpodesc.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState Value is Null");
        }
        SetOldDataterma();
    }
    private void SetOldDataterma()
    {
        int rowIndex = 0;
        if (ViewState["Curtblterma"] != null)
        {
            DataTable dtt = (DataTable)ViewState["Curtblterma"];
            if (dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)grdpodesc.Rows[rowIndex].Cells[0].FindControl("chkSelectterms");
                    TextBox txtterms = (TextBox)grdpodesc.Rows[rowIndex].Cells[2].FindControl("txttermsamd");
                    txtterms.Text = dtt.Rows[i]["splitdata"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    protected void btnAddterma_Click(object sender, EventArgs e)
    {
        AddNewRowterma();
        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
    }
    protected void grdpodesc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtblterma"] != null)
            {
                DataTable dtt = (DataTable)ViewState["Curtblterma"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dtt.Rows.Count > 1)
                {
                    dtt.Rows.Remove(dtt.Rows[rowIndex]);
                    drCurrentRow = dtt.NewRow();
                    ViewState["Curtblterma"] = dtt;
                    grdpodesc.DataSource = dtt;
                    grdpodesc.DataBind();
                    for (int i = 0; i < grdpodesc.Rows.Count - 1; i++)
                    {
                        grdpodesc.Rows[i].Cells[1].Text = Convert.ToString(i + 1);
                    }
                    SetOldDataterma();
                }
            }
            //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "UpdatePanel1", "javascript:Calculate()", true);
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
    public int m = 0;
   
    #endregion Terms and Conditions End
}
