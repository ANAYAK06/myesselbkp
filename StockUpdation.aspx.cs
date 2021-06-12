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

public partial class StockUpdation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 7);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            checkapprovals();
            btn.Style.Add("visibility", "hidden");
        }
        //trdatedesc.Style.Add("visibility", "hidden");
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedItem.Text != "Select Updation Type")
        {
            if (ddlType.SelectedItem.Text == "From Vendor")
            {

                pono.Visible = true;
                refno.Visible = false;
            }
            else
            {

                pono.Visible = false;
                refno.Visible = true;
             }
            ModalPopupExtender1.Show();
        }
        else
        {
            ModalPopupExtender1.Hide();
        }
    }
  
    protected void btnok_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlType.SelectedItem.Text == "From Central Store")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(isnull(Amount,0),'.0000','.00')Amount,Replace(il.quantity,'.00','')[Issued Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select idl.id,idl.item_code,idl.quantity,Amount from [items transfer] idl join [Transfer info] pd on idl.ref_no=pd.ref_no  where  pd.ref_no='" + txtrefno.Text + "' and Status='2' and pd.recieved_cc='" + Session["cc_code"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillcentral");
                CentralGrid.DataSource = ds.Tables["fillcentral"];
                CentralGrid.DataBind();
                if (ds.Tables["fillcentral"].Rows.Count > 0)
                {
                    btn.Style.Add("visibility", "visible");
                    //trdatedesc.Style.Add("visibility", "hidden");
                }
                else
                {
                    btn.Style.Add("visibility", "hidden");
                    //trdatedesc.Style.Add("visibility", "hidden");
                }
            }

            else if (ddlType.SelectedItem.Text == "From Other Site Store" && Session["roles"].ToString() == "Central Store Keeper")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(isnull(Amount,0),'.0000','.00')Amount,Replace(il.quantity,'.00','')[Issued Qty],item_status from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select idl.id,idl.item_code,Amount,idl.quantity,item_status from [items transfer] idl join [Transfer info] pd on idl.ref_no=pd.ref_no  where  pd.ref_no='" + txtrefno.Text + "' and Status='4' and pd.recieved_cc='" + Session["cc_code"].ToString() + "' and [type]='2')il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillcc");
                grid.DataSource = ds.Tables["fillcc"];
                grid.DataBind();
                if (ds.Tables["fillcc"].Rows.Count > 0)
                {
                    //trdatedesc.Style.Add("visibility", "hidden");
                    btn.Style.Add("visibility", "visible");
                  
                }
                else
                {
                    //trdatedesc.Style.Add("visibility", "hidden");
                    btn.Style.Add("visibility", "hidden");
                }
            }
            else if (ddlType.SelectedItem.Text == "From Other Site Store" && Session["roles"].ToString() == "StoreKeeper")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(isnull(Amount,0),'.0000','.00')Amount,Replace(il.quantity,'.00','')[Issued Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select idl.id,idl.item_code,Amount,idl.quantity from [items transfer] idl join [Transfer info] pd on idl.ref_no=pd.ref_no  where  pd.ref_no='" + txtrefno.Text + "' and Status='4' and pd.recieved_cc='" + Session["cc_code"].ToString() + "' and [type]='2')il on i.item_code=substring(il.item_code,1,8)", con);
                da.Fill(ds, "fillcc");
                CentralGrid.DataSource = ds.Tables["fillcc"];
                CentralGrid.DataBind();
                if (ds.Tables["fillcc"].Rows.Count > 0)
                {
                    //trdatedesc.Style.Add("visibility", "hidden");
                    btn.Style.Add("visibility", "visible");

                }
                else
                {
                    //trdatedesc.Style.Add("visibility", "hidden");
                    btn.Style.Add("visibility", "hidden");
                }
            }
            else if (ddlType.SelectedItem.Text == "From Vendor")
            {
                
                da = new SqlDataAdapter("select id,vendor_name from Purchase_details where Po_no='" + txtpo.Text + "'  ", con);
                da.Fill(ds, "po_id");
                if (Convert.ToInt32(ds.Tables["po_id"].Rows[0][0].ToString()) > 1878)
                {
                    if (Session["roles"].ToString() == "Central Store Keeper")
                        da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,Replace(il.quantity,'.00','')[Raised Qty],Replace(il.amount,'.00','')amount,units from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select idl.id,idl.item_code,idl.quantity,idl.[Amount],COALESCE(New_Basicprice,Basic_Price)as Basic_Price from PO_details idl join purchase_details pd on idl.Po_no=pd.Po_no where  pd.Po_no='" + txtpo.Text + "' and Status='2'  and quantity!='0' and (pd.cc_code='CC-33' or pd.cc_code='CCC'))il on i.item_code=substring(il.item_code,1,8)", con);
                    else if (Session["roles"].ToString() == "StoreKeeper")
                        da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,Replace(il.quantity,'.00','')[Raised Qty],Replace(il.amount,'.00','')amount,units from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select idl.id,idl.item_code,idl.quantity,idl.[Amount],COALESCE(New_Basicprice,Basic_Price)as Basic_Price from PO_details idl join purchase_details pd on idl.Po_no=pd.Po_no where  pd.Po_no='" + txtpo.Text + "' and Status='2'  and quantity!='0' and pd.cc_code='" + Session["cc_code"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
                }
                else
                    if (Session["roles"].ToString() == "Central Store Keeper")
                        da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,Replace(il.quantity,'.00','')[Raised Qty],Replace(il.amount,'.00','')amount,units from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select idl.id,idl.item_code,idl.quantity,idl.[Amount],COALESCE(New_Basicprice,Basic_Price)as Basic_Price from indent_list idl join purchase_details pd on idl.indent_no=pd.indent_no  where  po_no='" + txtpo.Text + "' and Status='2'  and quantity!='0' and (pd.cc_code='CC-33' or pd.cc_code='CCC'))il on i.item_code=substring(il.item_code,1,8)", con);
                    else if (Session["roles"].ToString() == "StoreKeeper")
                        da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,Replace(il.quantity,'.00','')[Raised Qty],Replace(il.amount,'.00','')amount,units from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select idl.id,idl.item_code,idl.quantity,idl.[Amount],COALESCE(New_Basicprice,Basic_Price)as Basic_Price from indent_list idl join purchase_details pd on idl.indent_no=pd.indent_no  where  po_no='" + txtpo.Text + "' and Status='2'  and quantity!='0' and pd.cc_code='" + Session["cc_code"].ToString() + "')il on i.item_code=substring(il.item_code,1,8)", con);
              
                    da.Fill(ds, "fillgrid");
                    Vengrid.DataSource = ds.Tables["fillgrid"];
                    Vengrid.DataBind();
                if (ds.Tables["fillgrid"].Rows.Count > 0)
                {
                    btn.Style.Add("visibility", "visible");
                    lblvenname.Text = "Vendor Name :-  "+ds.Tables["po_id"].Rows[0][1].ToString();
                    //trdatedesc.Style.Add("visibility", "visible");
                }
                else
                {
                    btn.Style.Add("visibility", "hidden");
                    //trdatedesc.Style.Add("visibility", "hidden");
                }
               
            }
           
        }
     
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,Replace(il.quantity,'.00','')quantity,Replace(il.amount,'.00','')amount from (Select item_code,item_name,specification,dca_code,subdca_code,basic_price from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select idl.id,idl.item_code,idl.quantity,idl.[Amount] from [items transfer] idl join [Transfer info] pd on idl.ref_no=pd.ref_no  where  pd.ref_no='" + ViewState["refno"] + "' and Status='1' and pd.cc_code='" + Session["cc_code"].ToString() + "' and [type]='1')il on i.item_code=il.item_code", con);

    protected void btnupdateprint_Click(object sender, EventArgs e)
    {
        string Sids = "";
        string RQty = "";
        string Type= "";
        string Prices = "";
        //string units = "";
        if (ddlType.SelectedItem.Text == "From Vendor")
        {
            foreach (GridViewRow record in Vengrid.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {

                    Sids = Sids + Vengrid.DataKeys[record.RowIndex]["id"].ToString() + ",";
                    RQty = RQty + (record.FindControl("txtqty") as TextBox).Text + ",";
                    string Reqty=(record.FindControl("txtqty") as TextBox).Text;
                    Prices = Prices + Convert.ToDecimal(record.Cells[7].Text) + ",";
                    //units = units + (record.FindControl("ddlunit") as DropDownList).SelectedValue + ",";
                    if (Reqty != "")
                    {
                        ReceivedQty = ReceivedQty + Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text);
                    }

                    RaisedQty = RaisedQty + Convert.ToDecimal(record.Cells[8].Text);
                    //if (record.Cells[5].Text == "DCA-27")
                    //    Amount27 = Amount27 + Convert.ToDecimal(Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text) * Convert.ToDecimal((record.FindControl("txtPunit") as TextBox).Text));
                    //else if (record.Cells[5].Text == "DCA-11")
                    //    Amount11 = Amount11 + Convert.ToDecimal(Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text) * Convert.ToDecimal((record.FindControl("txtPunit") as TextBox).Text));
                    //else if (record.Cells[5].Text == "DCA-41")
                    //    Amount41 = Amount41 + Convert.ToDecimal(Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text) * Convert.ToDecimal((record.FindControl("txtPunit") as TextBox).Text));
                }


            }

            //cmd = new SqlCommand("VendorStockUpdation_sp", con);
            da = new SqlDataAdapter("VendorStockUpdation_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ids", Sids);
            da.SelectCommand.Parameters.AddWithValue("@NewQtys", RQty);
            da.SelectCommand.Parameters.AddWithValue("@AssetQtys", RQty);
            da.SelectCommand.Parameters.AddWithValue("@Prices", Prices);            
            da.SelectCommand.Parameters.AddWithValue("@CCCode", Session["cc_code"].ToString());
            da.SelectCommand.Parameters.AddWithValue("@Date", txtdate.Text);
            da.SelectCommand.Parameters.AddWithValue("@Remarks", txtremarks.Text);
            da.SelectCommand.Parameters.AddWithValue("@User", Session["user"].ToString());
            da.SelectCommand.Parameters.AddWithValue("@ReceievedQty", ReceivedQty);
            da.SelectCommand.Parameters.AddWithValue("@RaisedQty", RaisedQty);
            da.SelectCommand.Parameters.AddWithValue("@PONo", txtpo.Text);
            da.SelectCommand.Parameters.AddWithValue("@role", Session["roles"].ToString());
            //cmd.Parameters.AddWithValue("@Amount27", Amount27);
            //cmd.Parameters.AddWithValue("@Amount11", Amount11);
            //cmd.Parameters.AddWithValue("@Amount41", Amount41);
            //cmd.Connection = con;
            //con.Open();
            da.Fill(ds, "MRRNo");

            //string msg = cmd.ExecuteScalar().ToString();
            //if (msg != "Sucessfull")
            if (ReceivedQty == RaisedQty && ds.Tables["MRRNo"].Rows[0].ItemArray[0].ToString() == "Sucessfull")
                JavaScript.UPAlertRedirect(Page, "The MRR No is:" + ds.Tables["MRRNo"].Rows[0].ItemArray[1].ToString(), "StockUpdation.aspx");
            else if (ReceivedQty != RaisedQty && ds.Tables["MRRNo"].Rows[0].ItemArray[0].ToString() == "Sucessfull")
                JavaScript.UPAlertRedirect(Page, "Sucessfull", "StockUpdation.aspx");
            else
                JavaScript.UPAlert(Page, ds.Tables["MRRNo"].Rows[0].ItemArray[0].ToString());
           
            con.Close();

        }
        else if (ddlType.SelectedItem.Text == "From Central Store" || ddlType.SelectedItem.Text == "From Other Site Store")
        {
            if (Session["roles"].ToString() == "Central Store Keeper")
            {
                foreach (GridViewRow record in grid.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                    if (c1.Checked)
                    {

                        Sids = Sids + grid.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        RQty = RQty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        Type = Type + (record.FindControl("ddltype") as DropDownList).SelectedValue + ",";
                        TransferQty = TransferQty + Convert.ToDecimal(record.Cells[8].Text);
                        RecieveQty = RecieveQty + Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text);
                        //if (record.Cells[5].Text == "DCA-11")
                        //    Amount11 = Amount11 + Convert.ToDecimal(record.Cells[9].Text);
                        //else if (record.Cells[5].Text == "DCA-41")
                        //    Amount41 = Amount41 + Convert.ToDecimal(record.Cells[9].Text);

                    }
                }

            }
            else
            {
                foreach (GridViewRow record in CentralGrid.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                    if (c1.Checked)
                    {

                        Sids = Sids + CentralGrid.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        RQty = RQty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        Type = Type + (record.FindControl("ddltype") as DropDownList).SelectedValue + ",";
                        TransferQty = TransferQty + Convert.ToDecimal(record.Cells[8].Text);
                        RecieveQty = RecieveQty + Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text);
                        if (record.Cells[5].Text == "DCA-11")
                            Amount11 = Amount11 + Convert.ToDecimal(record.Cells[9].Text);
                        else if (record.Cells[5].Text == "DCA-41")
                            Amount41 = Amount41 + Convert.ToDecimal(record.Cells[9].Text);

                    }
                }
            
            }
            cmd = new SqlCommand("stockupdation from store_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ids", Sids);
            cmd.Parameters.AddWithValue("@NewQtys", RQty);
            cmd.Parameters.AddWithValue("@Refno", txtrefno.Text);
            cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
            cmd.Parameters.AddWithValue("@date", txtdate.Text);
            cmd.Parameters.AddWithValue("@CCCode", Session["cc_code"].ToString());
            cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@Types", Type);
            cmd.Parameters.AddWithValue("@UpdationType", ddlType.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Amount11", Amount11);
            cmd.Parameters.AddWithValue("@Amount41", Amount41);
            cmd.Connection = con;
            con.Open();
            string msg1 = cmd.ExecuteScalar().ToString();
            if (TransferQty == RecieveQty && msg1 == "Sucessfull")
                JavaScript.UPAlertRedirect(Page, msg1, "StockUpdation.aspx");
            else if (msg1 == "Invalid date")
                JavaScript.UPAlert(Page, msg1);
            else
                JavaScript.UPAlertRedirect(Page, msg1, "Lost or Damaged Report.aspx");
            con.Close();

        }
       
    }  



    
    private decimal Amount11 = (decimal)0.0;
    private decimal Amount27 = (decimal)0.0;
    private decimal Amount41 = (decimal)0.0;
    private decimal TransferQty = (decimal)0.0;
    private decimal RecieveQty = (decimal)0.0;
    private decimal ReceivedQty = (decimal)0.0;
    private decimal RaisedQty = (decimal)0.0;
    protected void Vengrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);
        }
    }
    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);
        }
    }
    private decimal Amount = (decimal)0.0;

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
                        JavaScript.AlertAndRedirect("Temporary Closed store approval pending at project Manager", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                        JavaScript.AlertAndRedirect("Store Re-open approval pending at project Manager", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at project Manager", "PurchaseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "2")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Temporary Closed store approval pending at central store keeper", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "Reopen")
                        JavaScript.AlertAndRedirect("Store Re-open approval pending at central store keeper", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at central store keeper", "PurchaseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "3")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "TemporaryClosed")
                        JavaScript.AlertAndRedirect("Store is in Temporary Closing Mode", "PurchaseHome.aspx");
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store permanent closed approval pending at chief Material Controller", "PurchaseHome.aspx");
                }
                if (ds.Tables["check"].Rows[0].ItemArray[3].ToString() == "4")
                {
                    if (ds.Tables["check"].Rows[0].ItemArray[2].ToString() == "PermanentClosed")
                        JavaScript.AlertAndRedirect("Store is Already closed", "PurchaseHome.aspx");
                }
            }
        }
    }
}
