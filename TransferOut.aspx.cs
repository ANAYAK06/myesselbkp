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


public partial class TransferOut : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("WareHouse");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        //esselDal RoleCheck = new esselDal();
        //int rec = 0;
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 12);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            checkapprovals();
            fillgrid();
           // checkpercent();
            hfrole.Value = Session["roles"].ToString();
        }

    }
    public void fillgrid()
    {             
        
        if (Session["roles"].ToString() == "StoreKeeper")
        {
            da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,[Issued Qty],isnull([Available qty],0) [Available Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,quantity as [Issued Qty],(Select isnull(Sum(quantity),0) from master_data where cc_code='" + Session["cc_code"].ToString() + "' and item_code=it.item_code and (Status='Active' or Status is null))as [Available Qty] from [Items Transfer]it where Ref_no is null and U_Id is null AND it.cc_code='" + Session["cc_code"].ToString() + "' and type='2')il on i.item_code=substring(il.item_code,1,8) order by il.id Asc", con);
            //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,[Issued Qty],isnull([Available qty],0) [Available Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,quantity as [Issued Qty],(Select quantity from master_data where cc_code='" + Session["cc_code"].ToString() + "' and item_code=it.item_code and (Status='Active' or Status is null))as [Available Qty] from [Items Transfer]it where Ref_no is null)il on i.item_code=substring(il.item_code,1,8) order by il.id Asc", con);
            da.Fill(ds, "fill");
            grdtransferout.DataSource = ds.Tables["fill"];
            grdtransferout.DataBind();
            grdpopcentral.Visible = false;
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                btnsubmit.Style.Add("visibility", "visible");
                tbldesc.Style.Add("visibility", "visible");
            }
            else
            {
                btnsubmit.Style.Add("visibility", "hidden");
                tbldesc.Style.Add("visibility", "hidden");
            }

        }
        else if (Session["roles"].ToString() == "Central Store Keeper")
        {
            //da = new SqlDataAdapter("Select K.*,isnull(j.quantity,0) as [Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,basic_price,[Issued Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity as [Issued Qty] from [Transfer Info]ti join [Items transfer]it on ti.ref_no=it.ref_no where status='1' and type='2')il on i.item_code=il.item_code)k join (Select item_code,quantity from master_data where cc_code =(Select distinct tin.cc_code from [Transfer Info]tin join [Items transfer]itt on tin.ref_no=itt.ref_no where status='1' and type='2'))j on j.item_code=k.item_code", con);
            da = new SqlDataAdapter("Select K.*,isnull(j.quantity,0) as [Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,isnull(Replace(basic_price,'.0000','.00'),0)as basic_price,[Issued Qty],type from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity as [Issued Qty],it.basic_price,it.type from [Transfer Info]ti join [Items transfer]it on ti.ref_no=it.ref_no where status='1')il on i.item_code=il.item_code)k join (Select item_code,quantity from master_data where cc_code =(Select distinct tin.cc_code from [Transfer Info]tin join [Items transfer]itt on tin.ref_no=itt.ref_no join indents id ON tin.indent_no=id.indent_no  where tin.status='1' and id.indenttype IN ('SemiAssets/Consumable Issue','SemiAssets/Consumable Transfer','Partially Purchase')  ))j on j.item_code=k.item_code", con);
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                ViewState["typecheck"] = ds.Tables["fill"].Rows[0].ItemArray[9].ToString();
                hftype.Value = ds.Tables["fill"].Rows[0].ItemArray[9].ToString();
            }
            grdpopcentral.DataSource = ds.Tables["fill"];
            grdpopcentral.DataBind();
            grdtransferout.Visible = false;
            tblSearch.Visible = false;
            btnadd.Visible = false;
            btnDelete.Visible = false;
            Button1.Visible = false;
            tddays.Visible = false;
            tddayslabel.Visible = false;
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                btnsubmit.Style.Add("visibility", "visible");
                tbldesc.Style.Add("visibility", "visible");
            }
            else
            {
                btnsubmit.Style.Add("visibility", "hidden");
                tbldesc.Style.Add("visibility", "hidden");
            }
        }
        ////else if (Session["roles"].ToString() == "Chief Material Controller")
        ////{
        ////    //da = new SqlDataAdapter("Select K.*,isnull(j.quantity,0) as [Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,basic_price,[Issued Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity as [Issued Qty] from [Transfer Info]ti join [Items transfer]it on ti.ref_no=it.ref_no where status='1' and type='2')il on i.item_code=il.item_code)k join (Select item_code,quantity from master_data where cc_code =(Select distinct tin.cc_code from [Transfer Info]tin join [Items transfer]itt on tin.ref_no=itt.ref_no where status='1' and type='2'))j on j.item_code=k.item_code", con);
        ////    //da = new SqlDataAdapter("Select K.*,isnull(j.quantity,0) as [Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,isnull(Replace(basic_price,'.0000','.00'),0)as basic_price,[Issued Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity as [Issued Qty],it.basic_price from [Transfer Info]ti join [Items transfer]it on ti.ref_no=it.ref_no where status='1A' and type='2')il on i.item_code=il.item_code)k join (Select item_code,quantity from master_data where cc_code =(Select distinct tin.cc_code from [Transfer Info]tin join [Items transfer]itt on tin.ref_no=itt.ref_no where status='1A' and type='2'))j on j.item_code=k.item_code", con);
        ////    da = new SqlDataAdapter("Select K.*,isnull(j.quantity,0) as [Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,isnull(Replace(basic_price,'.0000','.00'),0)as basic_price,[Issued Qty],il.remarks,csk_percent,replace(csk_dep,'.0000','.00') as csk_dep ,transfer_date,replace((ISNULL(il.basic_price,0)*ISNULL(il.quantity,0)),'.0000','.00')as qty from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity as [Issued Qty],it.basic_price,ti.remarks,csk_percent,csk_dep,ti.transfer_date,it.quantity from [Transfer Info]ti join [Items transfer]it on ti.ref_no=it.ref_no where status='1A' and type='2')il on i.item_code=il.item_code)k join (Select item_code,quantity from master_data where cc_code =(Select distinct tin.cc_code from [Transfer Info]tin join [Items transfer]itt on tin.ref_no=itt.ref_no where status='1A' and type='2'))j on j.item_code=k.item_code", con);
        ////    da.Fill(ds, "filling");
        ////    grdpopcmc.DataSource = ds.Tables["filling"];
        ////    grdpopcmc.DataBind();
        ////    if (ds.Tables["filling"].Rows.Count > 0)
        ////    {
        ////        txtremarks.Text = ds.Tables["filling"].Rows[0].ItemArray[9].ToString();
        ////        txtdate.Text = ds.Tables["filling"].Rows[0].ItemArray[12].ToString();
        ////    }
        ////    else
        ////    {
        ////        txtremarks.Text = "";
        ////        txtdate.Text = "";
        ////    }
        ////    grdtransferout.Visible = false;
        ////    tblSearch.Visible = false;
        ////    btnadd.Visible = false;
        ////    btnDelete.Visible = false;
        ////    tddays.Visible = true;
        ////    tddayslabel.Visible = true;
        ////    if (ds.Tables["filling"].Rows.Count > 0)
        ////    {
        ////        btnsubmit.Style.Add("visibility", "visible");
        ////        tbldesc.Style.Add("visibility", "visible");
        ////    }
        ////    else
        ////    {
        ////        btnsubmit.Style.Add("visibility", "hidden");
        ////        tbldesc.Style.Add("visibility", "hidden");
        ////    }
    }


        

    
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            cmd.Connection = con;
            con.Open();
            char c = txtSearch.Text[0];
            if (char.IsNumber(c))
            {
                if (txtSearch.Text.Substring(0, 1) == "1")
                {
                    da = new SqlDataAdapter("SELECT 1 as exist FROM [items transfer] it JOIN [transfer info] ti ON it.ref_no=ti.ref_no where item_code='" + txtSearch.Text + "' and STATUS=3", con);
                    da.Fill(ds, "Assets");
                    if (ds.Tables["Assets"].Rows.Count == 1)
                    {
                        JavaScript.UPAlert(Page, "The Asset item is Already Raised for Transfer");
                        (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                        txtSearch.Text = String.Empty;
                    }
                    else
                    {
                        da = new SqlDataAdapter("SELECT 1 as IsExists FROM master_data where item_code='" + txtSearch.Text + "' and CC_code='" + Session["cc_code"].ToString() + "'", con);
                        da.Fill(ds, "checks");
                        if (ds.Tables["checks"].Rows.Count == 1)
                        {
                            cmd.CommandText = "insert into [Items transfer](item_code,cc_code,type)values(@itemcode,@CCCode,@type)";
                            cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = txtSearch.Text;
                            cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "2";
                            int i = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (i == 1)
                            {

                            }

                            fillgrid();
                            txtSearch.Text = "";
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Invalid itemcode");
                            (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                            txtSearch.Text = String.Empty;
                        } 
                    }
                }
                else if (txtSearch.Text.Substring(0, 1) != "1")
                {                  

                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM master_data where item_code='" + txtSearch.Text + "' and CC_code='" + Session["cc_code"].ToString() + "'", con);
                    da.Fill(ds, "checks");
                    if (ds.Tables["checks"].Rows.Count == 1)
                    {
                        cmd.CommandText = "insert into [Items transfer](item_code,cc_code,type)values(@itemcode,@CCCode,@type)";
                        cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = txtSearch.Text;
                        cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "2";
                        int i = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        if (i == 1)
                        {

                        }

                        fillgrid();
                        txtSearch.Text = "";
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, "Invalid itemcode");
                        (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                        txtSearch.Text = String.Empty;
                    }

                }

            }
            else
            {
                int n = txtSearch.Text.IndexOf(",");
                if (n != -1)
                {
                    string result = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
                    string result1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','5','5A','2A')", con);
                    da.Fill(ds, "check");
                    if (ds.Tables["check"].Rows.Count == 1)
                    {
                        da = new SqlDataAdapter("Select m.item_code,substring(m.item_code,1,1) from item_codes i join master_data m on i.item_code=substring(m.item_code,1,8) where item_name='" + result + "' and specification='" + result1 + "'", con);
                        da.Fill(ds, "search");
                        if (ds.Tables["search"].Rows.Count > 0 && ds.Tables["search"].Rows[0].ItemArray[1].ToString() != "1")
                        {

                            cmd.CommandText = "insert into [Items transfer](item_code,cc_code,type)values(@itemcode,@CCCode,@type)";
                            cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = ds.Tables["search"].Rows[0].ItemArray[0].ToString();
                            cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "2";
                            int i = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (i == 1)
                            {

                            }

                            fillgrid();
                            txtSearch.Text = "";
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Please search through itemcode for asset items");
                        }
                       
                    }
                    else
                    {

                        JavaScript.UPAlert(Page, "Invalid Specification");
                        (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                        txtSearch.Text = String.Empty;

                    }
                }

            }
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow record in grdtransferout.Rows)
        {
            CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

            if (c1.Checked)
            {
                string id = grdtransferout.DataKeys[record.RowIndex]["id"].ToString();
                cmd.Connection = con;
                cmd.CommandText = "delete from [Items Transfer] where id='" + id + "'";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
               
            }
          
        }
        fillgrid();
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Qty = "";
            string status = "";
            string DepAmounts = "";
            string dep1 = "";
            string Amounts = "";
            if (Session["roles"].ToString() == "StoreKeeper")
            {
                foreach (GridViewRow record in grdtransferout.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                    if ((record.FindControl("ChkSelect") as CheckBox).Checked)
                    {
                        ids = ids + grdtransferout.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        status = status + (record.FindControl("ddlStatus") as DropDownList).SelectedValue + ",";

                    }
                }
                if (ids != "")
                {                  

                    da = new SqlDataAdapter("transferout_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.SelectCommand.Parameters.AddWithValue("@ids", SqlDbType.VarChar).Value = ids;
                    da.SelectCommand.Parameters.AddWithValue("@NewIds", SqlDbType.VarChar).Value = ids;
                    da.SelectCommand.Parameters.AddWithValue("@Qtys", SqlDbType.VarChar).Value = Qty;
                    da.SelectCommand.Parameters.AddWithValue("@Statuses", SqlDbType.VarChar).Value = status;
                    da.SelectCommand.Parameters.AddWithValue("@Date", SqlDbType.VarChar).Value = txtdate.Text;
                    da.SelectCommand.Parameters.AddWithValue("@Days", SqlDbType.Int).Value = ddlDays.SelectedValue;
                    da.SelectCommand.Parameters.AddWithValue("@cccode", SqlDbType.Int).Value = Session["cc_code"].ToString();
                    da.SelectCommand.Parameters.AddWithValue("@Recievedcc", SqlDbType.Int).Value = "CC-33";
                    da.SelectCommand.Parameters.AddWithValue("@Remarks", SqlDbType.Int).Value = txtremarks.Text;
                    da.SelectCommand.Parameters.AddWithValue("@User", SqlDbType.Int).Value = Session["user"].ToString();

                    da.Fill(ds, "raisetransfer");

                    if (ds.Tables["raisetransfer"].Rows[0].ItemArray[0].ToString() == "Transfer Raised Successfully")
                    {
                        JavaScript.UPAlertRedirect(Page, "Transfer Raised Successfully. The Transfer No is: " + ds.Tables["raisetransfer"].Rows[0].ItemArray[1].ToString(), "TransferOut.aspx");
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, ds.Tables["raisetransfer"].Rows[0].ItemArray[0].ToString());
                    }
                    con.Close();                 
                 
                }
            }
            else if (Session["roles"].ToString() == "Central Store Keeper")
            {
                foreach (GridViewRow record in grdpopcentral.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                    if (c1.Checked && record.Cells[5].Text != "DCA-27")
                    {

                        ids = ids + grdpopcentral.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        {
                            if (ViewState["typecheck"].ToString() == "2")
                            {
                                if ((record.FindControl("ddldep") as DropDownList).SelectedValue != "Full Value")
                                {
                                    string Dep = (record.FindControl("ddldep") as DropDownList).SelectedValue;
                                    dep1 = dep1 + Convert.ToDecimal(Convert.ToDecimal(Dep)) + ",";
                                    Amount1 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                                    Double i = Convert.ToDouble(record.Cells[10].Text) - Convert.ToDouble((record.FindControl("txtqty") as TextBox).Text);
                                    DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                                    Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100));
                                    Amounts = Amounts + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                                    EffAmount = EffAmount + (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100) + (Convert.ToDecimal(Convert.ToDecimal(i) * Convert.ToDecimal(record.Cells[7].Text)));


                                }
                                else if ((record.FindControl("ddldep") as DropDownList).SelectedValue == "Full Value")
                                {
                                    int dep3 = 0;
                                    Amount2 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                                    DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount2)) + ",";
                                    Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount2));
                                    ////Amounts = Amounts + Convert.ToDecimal(Convert.ToDecimal(Amount2) - (((Convert.ToDecimal(Amount2)) * (Convert.ToDecimal(dep3))) / 100)) + ",";
                                    Amounts = Amounts + 0 + ",";
                                    dep1 = dep1 + "Full Value" + ",";
                                }
                            }
                            else
                            {

                                if ((record.FindControl("ddlissuedep") as DropDownList).SelectedValue != "Full Value")
                                {
                                    string Dep = (record.FindControl("ddlissuedep") as DropDownList).SelectedValue;
                                    dep1 = dep1 + Convert.ToDecimal(Convert.ToDecimal(Dep)) + ",";
                                    Amount1 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                                    Double i = Convert.ToDouble(record.Cells[10].Text) - Convert.ToDouble((record.FindControl("txtqty") as TextBox).Text);
                                    DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                                    Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100));
                                    Amounts = Amounts + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                                    EffAmount = EffAmount + (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100) + (Convert.ToDecimal(Convert.ToDecimal(i) * Convert.ToDecimal(record.Cells[7].Text)));


                                }
                                else if ((record.FindControl("ddlissuedep") as DropDownList).SelectedValue == "Full Value")
                                {
                                    int dep3 = 0;
                                    Amount2 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                                    DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount2)) + ",";
                                    Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount2));
                                    Amounts = Amounts + Convert.ToDecimal(Convert.ToDecimal(Amount2) - (((Convert.ToDecimal(Amount2)) * (Convert.ToDecimal(dep3))) / 100)) + ",";
                                    dep1 = dep1 + "Full Value" + ",";
                                }
                            }
                        }
                      
                    }

                }
                cmd = new SqlCommand("Transfer from CentralStore_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@Qtys", Qty);
                cmd.Parameters.AddWithValue("@DepAmounts", DepAmounts);
                cmd.Parameters.AddWithValue("@EffAmount", EffAmount);
                cmd.Parameters.AddWithValue("@Amount", Amount);
               // cmd.Parameters.AddWithValue("@Days", ddlDays.SelectedValue);
                cmd.Parameters.AddWithValue("@Amounts", Amounts);
                cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
                cmd.Parameters.AddWithValue("@depcsks", dep1);
                cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "Sucessfull")
                    JavaScript.UPAlertRedirect(Page, msg, "TransferOut.aspx");
                else
                    JavaScript.UPAlert(Page, msg);
            
            }
            ////else if (Session["roles"].ToString() == "Chief Material Controller")
            ////{
            ////    foreach (GridViewRow record in grdpopcmc.Rows)
            ////    {
            ////        CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

            ////        if (c1.Checked && record.Cells[5].Text != "DCA-27")
            ////        {

            ////            ids = ids + grdpopcmc.DataKeys[record.RowIndex]["id"].ToString() + ",";
            ////            Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
            ////            {
            ////                if ((record.FindControl("ddldep") as DropDownList).SelectedValue != "Full Value")
            ////                {
            ////                    string Dep = (record.FindControl("ddldep") as DropDownList).SelectedValue;
            ////                    dep1 = dep1 + Convert.ToDecimal(Convert.ToDecimal(Dep)) + ",";
            ////                    Amount1 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
            ////                    int i = Convert.ToInt32(record.Cells[10].Text) - Convert.ToInt32((record.FindControl("txtqty") as TextBox).Text);
            ////                    DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
            ////                    Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount1) - (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100));
            ////                    EffAmount = EffAmount + (((Convert.ToDecimal(Amount1)) * (Convert.ToDecimal(Dep))) / 100) + (Convert.ToDecimal(Convert.ToDecimal(i) * Convert.ToDecimal(record.Cells[7].Text)));


            ////                }
            ////                else if ((record.FindControl("ddldep") as DropDownList).SelectedValue == "Full Value")
            ////                {
            ////                    Amount2 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
            ////                    DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount2)) + ",";
            ////                    Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount2));

            ////                }
            ////            }

            ////        }

            ////    }
            ////    cmd = new SqlCommand("Transfer from CentralStore_sp", con);
            ////    cmd.CommandType = CommandType.StoredProcedure;
            ////    cmd.Parameters.AddWithValue("@ids", ids);
            ////    cmd.Parameters.AddWithValue("@Qtys", Qty);
            ////    cmd.Parameters.AddWithValue("@DepAmounts", DepAmounts);
            ////    cmd.Parameters.AddWithValue("@EffAmount", EffAmount);
            ////    cmd.Parameters.AddWithValue("@Amount", Amount);
            ////    cmd.Parameters.AddWithValue("@Days", ddlDays.SelectedValue);
            ////    cmd.Parameters.AddWithValue("@Date", txtdate.Text);
            ////    cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
            ////    cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
            ////    cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            ////    cmd.Connection = con;
            ////    con.Open();
            ////    string msg = cmd.ExecuteScalar().ToString();
            ////    con.Close();
            ////    if (msg == "Sucessfull")
            ////        JavaScript.UPAlertRedirect(Page, msg, "TransferOut.aspx");
            ////    else
            ////        JavaScript.UPAlert(Page, msg);

            ////}
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }


    }
    protected void ddlindenttype_SelectedIndexChanged(object sender, EventArgs e)
    {

        cascadingDCA cs = new cascadingDCA();
        cs.values(ddlindenttype.SelectedValue);

    }

    public void checkpercent()
    {
        da = new SqlDataAdapter("Select issue_percent from semiasset_dep", con);
        da.Fill(ds, "Find");
        hfcheck.Value = ds.Tables["Find"].Rows[0].ItemArray[0].ToString();
    }
    private decimal Amount = (decimal)0.0;
    private decimal EffAmount = (decimal)0.0;
    private decimal EffAmount1 = (decimal)0.0;
    private decimal Amount1 = (decimal)0.0;
    private decimal Amount2 = (decimal)0.0;
    ////private decimal Amt = (decimal)0.0;
    ////private decimal EffAmt = (decimal)0.0;

    ////protected void grdpopcmc_RowDataBound(object sender, GridViewRowEventArgs e)
    ////{
    ////    if (e.Row.RowType == DataControlRowType.DataRow)
    ////    {
    ////        Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "qty"));
    ////        EffAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "csk_dep"));
    ////    }
    ////    if (e.Row.RowType == DataControlRowType.Footer)
    ////    {
    ////        e.Row.Cells[13].Text = String.Format("Rs. {0:#,##,##,###.00}", Amt);
    ////        e.Row.Cells[14].Text = String.Format("Rs. {0:#,##,##,###.00}", EffAmt);

    ////    }
    ////}
    protected void grdpopcentral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddldep = e.Row.FindControl("ddldep") as DropDownList;
            DropDownList ddlissuedep = e.Row.FindControl("ddlissuedep") as DropDownList;

            if (ViewState["typecheck"].ToString() == "1")
            {
                ddlissuedep.Visible = true;
                ddldep.Visible = false;
            }
            else
            {
                ddlissuedep.Visible = false;
                ddldep.Visible = true;
            }
           
        }

    }
    protected void grdtransferout_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            da = new SqlDataAdapter("select isnull(sum(quantity),0) from master_data where cc_code='" + Session["cc_code"].ToString() + "' and item_code='" + e.Row.Cells[2].Text + "' union all SELECT isnull(sum(quantity),0) FrOM [items transfer] it JOIN [transfer info] ti ON it.ref_no=ti.ref_no where item_code='" + e.Row.Cells[2].Text + "' and STATUS='3' and it.cc_code='" + Session["cc_code"].ToString() + "'", con);
            da.Fill(ds, "Qty");
            decimal s1 = Convert.ToDecimal(ds.Tables["Qty"].Rows[0].ItemArray[0].ToString());
            decimal s2 = Convert.ToDecimal(ds.Tables["Qty"].Rows[1].ItemArray[0].ToString());
            decimal S = s1 - s2;          
                e.Row.Cells[9].Text = Convert.ToString(S);
                ds.Tables["Qty"].Reset();
           
        }
    }
    public void checkapprovals()
    {
        if (Session["roles"].ToString() == "StoreKeeper")
        {
            da = new SqlDataAdapter("SELECT TOP 1 * from  closedcost_center where cc_code = '" + Session["CC_CODE"].ToString() + "' and status not in('Rejected') ORDER by id desc", con);
            da.Fill(ds, "check");
            if (ds.Tables["check"].Rows.Count > 0)
            {
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "1")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Temporary Closed store approval pending at project Manager", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                        JavaScript.AlertAndRedirect("Store Re-open approval pending at project Manager", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at project Manager", "WareHouseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "2")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Temporary Closed store approval pending at central store keeper", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                        JavaScript.AlertAndRedirect("Store Re-open approval pending at central store keeper", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at central store keeper", "WareHouseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "3")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Store is in Temporary Closing Mode", "WareHouseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at chief Material Controller", "WareHouseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "4")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store is Already closed", "WareHouseHome.aspx");
                }
            }
        }
    }
}
////Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(il.quantity,'.00','')quantity from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity from [Items Transfer] where Ref_no is null and cc_code='" + Session["cc_code"].ToString() + "')il on i.item_code=il.item_code