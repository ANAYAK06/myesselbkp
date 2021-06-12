using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class SiteStoreClosed : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        hfrole.Value = Session["roles"].ToString();
        if (!IsPostBack)
        {
            btncheck();
            tbltempclosed.Visible = false;
            tbltransferstock.Visible = false;
            labledisplay.Visible = false;
            lblcccode.Visible = false;
            dropdownbind();
            //Gvdetails.Visible = false;    
        }
    }
    public void dropdownbind()
    {
        ddltype.Items.Insert(0, new ListItem() { Text = "Select Status", Value = "Select Status" });
        if (Session["roles"].ToString() == "Project Manager")
        {
            ddltype.Items.Insert(1, new ListItem() { Text = "Accept", Value = "Approved" });
            ddltype.Items.Insert(2, new ListItem() { Text = "Reject", Value = "Rejected" });
        }
        else if (Session["roles"].ToString() == "Central Store Keeper")
        {
            ddltype.Items.Insert(1, new ListItem() { Text = "Verified", Value = "Approved" });
            ddltype.Items.Insert(2, new ListItem() { Text = "Rejected", Value = "Rejected" });
        }
        else 
        {
            ddltype.Items.Insert(1, new ListItem() { Text = "Approved ", Value = "Approved" });
            ddltype.Items.Insert(2, new ListItem() { Text = "Rejected", Value = "Rejected" });
        }
    }
    public void btncheck()
    {
        if (Session["roles"].ToString() == "StoreKeeper")
        {
            trbtns.Visible = true;
            Gvdetails.Visible = false;
            da = new SqlDataAdapter("SELECT distinct 1 as IsExists FROM ClosedCost_Center where cc_code='" + Session["CC_CODE"].ToString() + "'", con);
            da.Fill(ds, "checks");
            if (ds.Tables["checks"].Rows.Count >0)
            {
                da = new SqlDataAdapter("SELECT TOP 1 * from  closedcost_center where cc_code = '" + Session["CC_CODE"].ToString() + "' AND status in('1','2','4') and status not in('Rejected') ORDER by id desc", con);
                da.Fill(ds, "check");
                if (ds.Tables["check"].Rows.Count > 0)
                {
                    btntempcls.Enabled = false;
                    btnreopen.Enabled = false;
                    btncomclose.Enabled = false;
                }
                else
                {
                    da = new SqlDataAdapter("SELECT TOP 1 * from  closedcost_center where cc_code = '" + Session["CC_CODE"].ToString() + "' AND status in('3') and status not in('Rejected') ORDER by id desc", con);
                    da.Fill(ds, "checking");
                    if (ds.Tables["checking"].Rows.Count > 0)
                    {
                        if (ds.Tables["checking"].Rows[0].ItemArray[2].ToString() != "Reopen")
                        {
                            btntempcls.Enabled = false;
                            btnreopen.Enabled = true;
                            btncomclose.Enabled = false;
                        }
                        else
                        {
                            btntempcls.Enabled = true;
                            btnreopen.Enabled = false;
                            btncomclose.Enabled = true;
                        }
                    }
                    else
                    {
                        btntempcls.Enabled = true;
                        btnreopen.Enabled = false;
                        btncomclose.Enabled = true;
                    }
                }
            }
            else
            {
                btnreopen.Enabled = false;
            }
        }
        else
        {
            trbtns.Visible = false;
            if (Session["roles"].ToString() == "Project Manager")
            {
                da = new SqlDataAdapter("SELECT Id,CC_Code,REPLACE(CONVERT(NVARCHAR,Date, 106), ' ', '-')as Date,SK_Desc,pm_Desc,close_type,CSK_Desc FROM ClosedCost_Center where status='1' and cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "')", con);
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                da = new SqlDataAdapter("SELECT Id,CC_Code,REPLACE(CONVERT(NVARCHAR,Date, 106), ' ', '-')as Date,SK_Desc,pm_Desc,close_type,CSK_Desc FROM ClosedCost_Center where status='2'", con);
            }
            else
            {
                da = new SqlDataAdapter("SELECT Id,CC_Code,REPLACE(CONVERT(NVARCHAR,Date, 106), ' ', '-')as Date,SK_Desc,pm_Desc,close_type,CSK_Desc FROM ClosedCost_Center where status='3' and Close_Type='PermanentClosed'", con);
            }
            da.Fill(ds, "chkpjgrid");
            if (ds.Tables["chkpjgrid"].Rows.Count > 0)
            {
                Gvdetails.DataSource = ds.Tables["chkpjgrid"];
                Gvdetails.DataBind();
                Gvdetails.Visible = true;
                trbtns.Visible = false;
            }
            //tbltempclosed.Visible = false;
        }
    }
    protected void btntempcls_Click(object sender, EventArgs e)
    {
        btnsubmit.Text = "Temporary Close";
        ViewState["click"] = "Temporary Store Closed";
        btnclick();       
    }
    protected void btnreopen_Click(object sender, EventArgs e)
    {
        btnsubmit.Text = "Reopen Store";
        btnclick();      
    }
    public void btnclick()
    {
        da = new SqlDataAdapter("SiteStoreClosed_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = Session["CC_CODE"].ToString();
        da.SelectCommand.Parameters.AddWithValue("@Role", SqlDbType.VarChar).Value = Session["roles"].ToString();
        if ((btnsubmit.Text == "Temporary Close") || (btnsubmit.Text == "Reopen Store"))
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "TempClose-Reopen";
        else
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "PermanentClose";
        da.Fill(ds1, "storeclosed");
        if (ds1.Tables["storeclosed"].Rows.Count > 0)
        {
            Gvstoreclosed.DataSource = ds1.Tables["storeclosed"];
            Gvstoreclosed.DataBind();
            if (Session["roles"].ToString() == "StoreKeeper")
            {
                lbltype.Visible = false;
                ddltype.Visible = false;
            }
            else
            {
                lbltype.Visible = true;
                ddltype.Visible = true;
            }
            lbldate.Visible = true;
            txtdate.Visible = true;
            lbldescr.Visible = true;
            txtdesc.Visible = true;
            btnsubmit.Visible = true;
        }
        else
        {
            tbltempclosed.Visible = false;
        }
    }
    protected void Gvstoreclosed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "40px");
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='lightgrey'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            string cell5 = e.Row.Cells[5].Text;                                                 //Recived From CentralStore
            string cell6 = e.Row.Cells[6].Text;                                                 //Recieved From OtherCC
            string cell7 = e.Row.Cells[7].Text;                                                 //Purchase At CC
            decimal sum = decimal.Parse(cell5) + decimal.Parse(cell6) + decimal.Parse(cell7);   //Recived From CentralStore + Recieved From OtherCC + Purchase At CC
            e.Row.Cells[13].Text = sum.ToString();                                               //Total Recieved at CC

            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;

            string cell9 = e.Row.Cells[9].Text;                                                 //Transfer To CentralStore
            string cell10 = e.Row.Cells[10].Text;                                               //Transfer To OtherCC
            string cell11 = e.Row.Cells[11].Text;                                               //Lost & Damages
            string cell12 = e.Row.Cells[12].Text;                                               //Total Out From CC
            decimal totalsum = decimal.Parse(cell9) + decimal.Parse(cell10) + decimal.Parse(cell11) + decimal.Parse(cell12);  //Transfer To CentralStore + Transfer To OtherCC + Lost & Damages + Total Out From CC
            e.Row.Cells[14].Text = totalsum.ToString();                                         //Balance Stock at CC



            decimal balancestock = sum - totalsum;                                              //(Total Recieved at CC)-(Total Out From CC)
            e.Row.Cells[15].Text = balancestock.ToString();                                     //Balance Stock at CC
            //if (e.Row.Cells[15].Text == "0")
            //{
            //    e.Row.Visible = false;
            //}

            //string cell3 = e.Row.Cells[3].Text;                                                 //BasicPrice
            //decimal amountconsumedatcc = decimal.Parse(cell11) * decimal.Parse(cell3);          //(Issued For CC Consumption)+(BasicPrice)
            //e.Row.Cells[15].Text = String.Format("{0:#,##,##,###.00}", amountconsumedatcc);                               //Balance Stock Amt at CC

            //decimal balancestockatcc = balancestock * decimal.Parse(cell3);                     //(Balance Stock at CC)*(Total Out From CC)
            //e.Row.Cells[16].Text = String.Format("{0:#,##,##,###.00}", balancestockatcc);                                 //Amount Of Damage     

            //decimal lostanddamage = decimal.Parse(cell12) * decimal.Parse(cell3);               //(Total Out From CC)*(BasicPrice)
            //e.Row.Cells[17].Text = String.Format("{0:#,##,##,###.00}", lostanddamage);         //Amount Of Damage
            //balancestockatallcc += Convert.ToDecimal((e.Row.Cells[16].Text));
            TransferToCentralStore += Convert.ToDecimal((e.Row.Cells[9].Text));
            TransferToOtherCC += Convert.ToDecimal((e.Row.Cells[10].Text));
            IssuedForCCConsumption += Convert.ToDecimal((e.Row.Cells[11].Text));
            LostandDamages += Convert.ToDecimal((e.Row.Cells[12].Text));
            TotalRecievedatCC += Convert.ToDecimal((e.Row.Cells[13].Text));
            TotalOutFromCC += Convert.ToDecimal((e.Row.Cells[14].Text));
            BalanceStockatCC += Convert.ToDecimal((e.Row.Cells[15].Text));
           
        }
   
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = String.Format("{0:0.##}", TransferToCentralStore);
            e.Row.Cells[10].Text = String.Format("{0:0.##}", TransferToOtherCC);
            e.Row.Cells[11].Text = String.Format("{0:0.##}", IssuedForCCConsumption);
            e.Row.Cells[12].Text = String.Format("{0:0.##}", LostandDamages);
            e.Row.Cells[13].Text = String.Format("{0:0.##}", TotalRecievedatCC);
            e.Row.Cells[14].Text = String.Format("{0:0.##}", TotalOutFromCC);
            e.Row.Cells[15].Text = String.Format("{0:0.##}", BalanceStockatCC);
            if (e.Row.Cells[15].Text.ToString() == "0")
            {
                if (Session["roles"].ToString() == "StoreKeeper")
                {
                    tbltempclosed.Visible = true;
                    tbltransferstock.Visible = false;
                }
               
            }
            else
            {
                if (Session["roles"].ToString() == "StoreKeeper")
                {
                    if (btnsubmit.Text == "Temporary Close" || btnsubmit.Text == "Reopen Store")
                    {
                        tbltempclosed.Visible = true;
                        tbltransferstock.Visible = false;
                    }
                    if (btnsubmit.Text == "Close Store Permanently")
                    {
                        tbltempclosed.Visible = false;
                        tbltransferstock.Visible = true;
                    }
                }
               
            }
        }
    }

    private decimal TransferToCentralStore = (decimal)0.0;
    private decimal TransferToOtherCC = (decimal)0.0;
    private decimal IssuedForCCConsumption = (decimal)0.0;
    private decimal LostandDamages = (decimal)0.0;
    private decimal TotalRecievedatCC = (decimal)0.0;
    private decimal TotalOutFromCC = (decimal)0.0;
    private decimal BalanceStockatCC = (decimal)0.0;


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("SiteStoreClosedApproval_sp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (Session["roles"].ToString() == "StoreKeeper")
            cmd.Parameters.AddWithValue("@CCCode", Session["CC_CODE"].ToString());
        else
            cmd.Parameters.AddWithValue("@Id", ViewState["ID"].ToString());
        if (btnsubmit.Text == "Temporary Close")
        {
            cmd.Parameters.AddWithValue("@CloseType", "TemporaryClosed");
        }
        else if (btnsubmit.Text == "Reopen Store")
        {
            cmd.Parameters.AddWithValue("@CloseType", "Reopen");
        }
        else if (btnsubmit.Text == "Close Store Permanently")
        {
            cmd.Parameters.AddWithValue("@CloseType", "PermanentClosed");
        }
        if (Session["roles"].ToString() != "StoreKeeper")
        cmd.Parameters.AddWithValue("@Type", ddltype.SelectedValue);
        cmd.Parameters.AddWithValue("@Date", txtdate.Text);
        cmd.Parameters.AddWithValue("@Desc", txtdesc.Text);
        cmd.Parameters.AddWithValue("@Role", Session["roles"].ToString());
        cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
        cmd.Connection = con;
        con.Open();
        string msg = cmd.ExecuteScalar().ToString();
        con.Close();
        if (msg == "Successfull" || msg == "Approved")
        {
            tbltempclosed.Visible = false;
            tbltransferstock.Visible = false;
            if (Session["roles"].ToString() == "Project Manager")
            {
                JavaScript.UPAlertRedirect(Page, "Accepted", "SiteStoreClosed.aspx");
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                JavaScript.UPAlertRedirect(Page, "Verified", "SiteStoreClosed.aspx");
            }
            else
            {
                JavaScript.UPAlertRedirect(Page, "Approved", "SiteStoreClosed.aspx");
            }
        }
        else
        {
            JavaScript.UPAlertRedirect(Page, msg, "SiteStoreClosed.aspx");
        }
    }
    protected void Gvdetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (Session["roles"].ToString() != "StoreKeeper")
        {
            da = new SqlDataAdapter("SiteStoreClosed_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", SqlDbType.VarChar).Value = Gvdetails.DataKeys[e.NewEditIndex]["Id"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Role", SqlDbType.VarChar).Value = Session["roles"].ToString();
            if ((btnsubmit.Text == "Temporary Close") || (btnsubmit.Text == "Reopen Store"))
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "TempClose-Reopen";
            else
                da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "PermanentClose";
            da.Fill(ds, "storec");
            if (ds.Tables["storec"].Rows.Count > 0)
            {
                if (Gvdetails.DataKeys[e.NewEditIndex]["close_type"].ToString() == "TemporaryClosed")
                {
                    btnsubmit.Text = "Temporary Close";
                    labledisplay.Visible = true;
                    labledisplay.Text = "Temporary Store Closing For ";
                }
                if (Gvdetails.DataKeys[e.NewEditIndex]["close_type"].ToString() == "Reopen")
                {
                    btnsubmit.Text = "Reopen Store";
                    labledisplay.Visible = true;
                    labledisplay.Text = "Reopen Store Closing For ";
                }
                if (Gvdetails.DataKeys[e.NewEditIndex]["close_type"].ToString() == "PermanentClosed")
                {
                    btnsubmit.Text = "Close Store Permanently";
                    tbltempclosed.Visible = true;
                    labledisplay.Visible = true;
                    labledisplay.Text = "Permanent Store Closing For ";
                }
                tbltempclosed.Visible = true;
                tbltransferstock.Visible = false;
                Gvdetails.Visible = true;                
                lblcccode.Visible = true;                
                lblcccode.Text = Gvdetails.DataKeys[e.NewEditIndex]["CC_Code"].ToString();
                Gvstoreclosed.DataSource = ds.Tables["storec"];
                Gvstoreclosed.DataBind();
                ViewState["ID"] = Gvdetails.DataKeys[e.NewEditIndex]["Id"].ToString();
                txtdate.Text = Gvdetails.DataKeys[e.NewEditIndex]["Date"].ToString();
            }
            else
            {
                tbltempclosed.Visible = false;
                Gvdetails.Visible = false;
                labledisplay.Visible = false;
                lblcccode.Visible = false;
            }
        }

    }
    protected void Gvdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = false;
            }
            else
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tbltempclosed.Visible = true;
            if (Session["roles"].ToString() == "Project Manager")
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
               
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = false;
            }
            else
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = true;
            }

        }
    }
    protected void btncomclose_Click(object sender, EventArgs e)
    {
       
        da = new SqlDataAdapter("SiteStoreCompleteCloseCheck_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = Session["CC_CODE"].ToString();
        da.Fill(ds, "storeclosed");
        if (ds.Tables["storeclosed"].Rows[0].ItemArray[0].ToString() != "")
        {
            tbltempclosed.Visible = false;
            tbltransferstock.Visible = false;
            string msg = ds.Tables["storeclosed"].Rows[0].ItemArray[0].ToString();
            JavaScript.UPAlert(Page, msg);
        }
        else
        {
            btnsubmit.Text = "Close Store Permanently";
            ViewState["click"] = "Close Store Permanently";
            btnclick();
        }
    }
    protected void btntransferstock_Click(object sender, EventArgs e)
    {

        if (Session["roles"].ToString() == "StoreKeeper")
        {
            da = new SqlDataAdapter("Sitestoretransfer_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = Session["CC_CODE"].ToString();
            da.Fill(ds, "storeclosedchk");
            if (ds.Tables["storeclosedchk"].Rows.Count>0)
            {
               
                JavaScript.UPAlertRedirect(Page, "Approve the previous storeclosed items", "CloseTransferOut.aspx");
            }
            else
            {
                btnsubmit.Text = "Close Store Permanently";
                btnclick();
                string cc = Session["CC_CODE"].ToString();
                string url = "SiteStorePermanentClose.aspx?CCCode=" + cc + "&Role=" + Session["roles"].ToString() + "&Type=PermanentClose";
                string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=1000,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }

        }
    }
   
    
}