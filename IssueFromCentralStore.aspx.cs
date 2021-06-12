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


public partial class IssueFromCentralStore : System.Web.UI.Page
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
        //rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 13);
        //if (rec == 0)
        //    Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {

            FillGrid();
            //CCInfo();
            //mdlfillview();
            //pname();
            //tblprinting.Visible = false;
        }
        DateTime myDate = DateTime.Now.AddHours(11).AddMinutes(30).AddSeconds(8);
        txtdate.Text = myDate.ToString();
    }

   
    public void FillGrid()
    {
        try
        {

            //da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,basic_price,il.quantity,(quantity*basic_price)[Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,quantity,cc_code from [Items Transfer] where ref_no is null and type='1')il on i.item_code=il.item_code", con);
            if (Session["roles"].ToString() == "Central Store Keeper")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,replace(basic_price,'.0000','.00')as basic_price,il.quantity,(quantity*basic_price)[Amount] from (Select item_code,item_name,specification,dca_code,subdca_code,basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity,ti.cc_code from [transfer info]ti join [Items Transfer]it on ti.ref_no=it.ref_no join indents id ON ti.indent_no=id.indent_no where ti.status='1' and type='1' and id.indenttype IN ('Partially Purchase'))il on i.item_code=il.item_code", con);
                da.Fill(ds, "fill");
                grdtransferout.DataSource = ds.Tables["fill"];
                grdtransferout.DataBind();

            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,replace(basic_price,'.0000','.00')as basic_price ,il.quantity from (Select item_code,item_name,specification,dca_code,subdca_code,basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity,ti.cc_code from [transfer info]ti join [Items Transfer]it on ti.ref_no=it.ref_no where status='1' and type='1')il on i.item_code=il.item_code", con);
                da.Fill(ds, "fill");
                grdissued.DataSource = ds.Tables["fill"];
                grdissued.DataBind();

            }
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                btnconfirm.Style.Add("visibility", "visible");
                trdesc.Style.Add("visibility", "visible");
            }
            else
            {
                btnconfirm.Style.Add("visibility", "hidden");
                trdesc.Style.Add("visibility", "hidden");
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    private decimal Amt = (decimal)0.0;
 
    protected void btnconfirm_Click(object sender, EventArgs e)
     {
         try
         {

             string ids = "";
             //string Amount = "";
             string RQtys = "";
             string Qtys = "";
             string s = "";
            string dep1 = "";
             //string DepAmounts = "";
             string Amount1 = "";
             string Amt = "";
            string Amount2 = "";
         
            
             //if (ViewState["type"].ToString() == "1")
             //{
             if (Session["roles"].ToString() == "Central Store Keeper")
             {

                 foreach (GridViewRow record in grdtransferout.Rows)
                 {
                     CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                     if (c1.Checked)
                     {
                         
                         ids = ids + grdtransferout.DataKeys[record.RowIndex]["id"].ToString() + ",";
                         s = record.Cells[2].Text.Substring(0, 1);
                                                 
                         if ((record.FindControl("ddldep") as DropDownList).SelectedValue != "Full Value")
                         {
                            string Dep = (record.FindControl("ddldep") as DropDownList).SelectedValue;
                             dep1 = dep1 + Convert.ToDecimal(Convert.ToDecimal(Dep)) + ",";
                             Amt = (record.FindControl("lbldepamount") as Label).Text;
                             
                             Amount1 = Amount1 + Convert.ToDecimal(Convert.ToDecimal(Amt) - (((Convert.ToDecimal(Amt)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                             Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amt) - (((Convert.ToDecimal(Amt)) * (Convert.ToDecimal(Dep))) / 100));
                         }

                         else if ((record.FindControl("ddldep") as DropDownList).SelectedValue == "Full Value")
                         {
                            
                             Amount2 = (record.FindControl("lbldepamount") as Label).Text;
                             Amount1 = Amount1 + Convert.ToDecimal(Convert.ToDecimal(Amount2)) + ",";
                             Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(Amount2));

                         }

                         
                         if (record.Cells[5].Text == "DCA-27")
                         {

                             Amount27 = Amount27 + Convert.ToDecimal(Convert.ToDecimal(Amount) - (((Convert.ToDecimal(Amount)) * (Convert.ToDecimal(Dep))) / 100));
                             Amount27A = Amount27A + (((Convert.ToDecimal(Amount)) * (Convert.ToDecimal(Dep))) / 100);

                         }
                         else if (record.Cells[5].Text == "DCA-11")
                         {
                             Amount11 = Amount11 + Convert.ToDecimal(Convert.ToDecimal(Amount) - (((Convert.ToDecimal(Amount)) * (Convert.ToDecimal(Dep))) / 100));
                             Amount11A = Amount11A + (((Convert.ToDecimal(Amount)) * (Convert.ToDecimal(Dep))) / 100);
                             Amount11B = Amount11B + Convert.ToDecimal(Amount);


                         }
                         else if (record.Cells[5].Text == "DCA-41")
                         {
                             Amount41 = Amount41 + Convert.ToDecimal(Convert.ToDecimal(Amount) - (((Convert.ToDecimal(Amount)) * (Convert.ToDecimal(Dep))) / 100));
                             Amount41A = Amount41A + (((Convert.ToDecimal(Amount)) * (Convert.ToDecimal(Dep))) / 100);
                             Amount41B = Amount41B + Convert.ToDecimal(Amount);

                         }



                     }
                 }
             }
             else
             {
                 foreach (GridViewRow record in grdissued.Rows)
                 {
                     CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                     if (c1.Checked)
                     {

                         if (record.Cells[5].Text == "DCA-11")
                         {
                             Amount11B = Amount11B + Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text);
                         }
                         else if (record.Cells[5].Text == "DCA-41")
                         {
                             Amount41B = Amount41B + Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[9].Text);

                         }
                     }
                 }
             }
             da = new SqlDataAdapter("Issue from central store_sp", con);
             da.SelectCommand.CommandType = CommandType.StoredProcedure;
             da.SelectCommand.Parameters.AddWithValue("@ids", ids);

             da.SelectCommand.Parameters.AddWithValue("@amounts", Amount);
             da.SelectCommand.Parameters.AddWithValue("@amount27", Amount27);/*Amount27,Amount11,Amount41 for credit to CC-33 due to the reason of csk calculate Depreciation for old items*/
             da.SelectCommand.Parameters.AddWithValue("@amount11", Amount11);
             da.SelectCommand.Parameters.AddWithValue("@amount41", Amount41);
             da.SelectCommand.Parameters.AddWithValue("@amount27A", Amount27A);/*Amount27A,Amount11A,Amount41A for credit to which CC raise the indent due to the reason of csk calculate Depreciation for old items*/
             da.SelectCommand.Parameters.AddWithValue("@amount11A", Amount11A);
             da.SelectCommand.Parameters.AddWithValue("@amount41A", Amount41A);
             da.SelectCommand.Parameters.AddWithValue("@amount11B", Amount11B);
             da.SelectCommand.Parameters.AddWithValue("@amount41B", Amount41B);
             da.SelectCommand.Parameters.AddWithValue("@cc_code", Session["cc_code"].ToString());
             da.SelectCommand.Parameters.AddWithValue("@Days", ddlDays.SelectedValue);
             da.SelectCommand.Parameters.AddWithValue("@date1", txtdate.Text);
             da.SelectCommand.Parameters.AddWithValue("@roles", Session["roles"].ToString());
             da.SelectCommand.Parameters.AddWithValue("@remarks", txtdesc.Text);
             da.SelectCommand.Parameters.AddWithValue("@user", Session["user"].ToString());
             if (s != "1")
                 da.SelectCommand.Parameters.AddWithValue("@itemtype", "1");
             else
                 da.SelectCommand.Parameters.AddWithValue("@itemtype", "2");
             da.Fill(ds, "IssuefromCentral");
             if (ds.Tables["IssuefromCentral"].Rows[0][0].ToString() == "Sucessfull")
             {
                 lblchallanno.Text = ds.Tables["IssuefromCentral"].Rows[0][1].ToString();
                 JavaScript.AlertAndRedirect("Sucessfull", "Issue.aspx");
             }
             else if (ds.Tables["IssuefromCentral"].Rows[0][0].ToString() == "Invalid date")
                 JavaScript.Alert("Invalid date");
             else
                 JavaScript.Alert("Failed");
             con.Close();
             FillGrid();
         }
        
         catch (Exception ex)
         {
             Utilities.CatchException(ex);
         }
    }
    protected void grdtransferout_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DepAmount += Convert.ToDecimal(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "basic_price")) * Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantity")));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("Label2");
            lbl.Text = DepAmount.ToString().Replace(".0000", ".00");
        }
    }
    private decimal DepAmount = (decimal)0.0;
    private decimal Amount11 = (decimal)0.0;
    private decimal Amount27 = (decimal)0.0;
    private decimal Amount41 = (decimal)0.0;
    private decimal Amount11A = (decimal)0.0;
    private decimal Amount27A = (decimal)0.0;
    private decimal Amount41A = (decimal)0.0;
    private decimal Amount11B = (decimal)0.0;
    private decimal Amount41B = (decimal)0.0;
    private decimal Dep = (decimal)0.0;
    private decimal Amount = (decimal)0.0;
    

    public void showalert(string message)
    {
        Label myalertlabel = new Label();
        Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "1", "IssueFromCentralStore.aspx");

        myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "');window.location='IssueFromCentralStore.aspx';print()</script>";
        Page.Controls.Add(myalertlabel);
    }
    public void mdlfillview()
    {


        da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(il.quantity,'.00','')quantity,item_status,Replace(il.amount,'.00','')amount,transfer_date from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select item_code,quantity,item_status,isnull(Amount,0)Amount,transfer_date from [items transfer]it join [Transfer Info]ti on it.ref_no=ti.ref_no where  status='1' and type='1' )il on i.item_code=il.item_code", con);
        da.Fill(ds, "fillview");
        grdbill.DataSource = ds.Tables["fillview"];
        lbldate.Text = txtdate.Text;
        grdbill.DataBind();



    }
    public void CCInfo()
    {
        try
        {
            da = new SqlDataAdapter("select cc_name,address from Cost_Center where cc_code=(Select distinct recieved_cc from [Transfer info]ti join [Items Transfer]it on ti.ref_no=it.ref_no where status='1' and type='1')", con);
            da.Fill(ds, "CCInfo");

            lbldespto.Text = ds.Tables["CCInfo"].Rows[0].ItemArray[0].ToString();
            lbldesptoadd.Text = ds.Tables["CCInfo"].Rows[0].ItemArray[1].ToString();
          
            

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void pname()
    {
        try
        {
            da = new SqlDataAdapter("select first_name+' ' +last_name from employee_data r join user_roles u on r.User_Name=u.User_Name where u.Roles ='PurchaseManager'", con);
            da.Fill(ds, "pnameinfo");
            if (ds.Tables["pnameinfo"].Rows.Count > 0)
            {

                lblpurchasemanagername.Text = ds.Tables["pnameinfo"].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
}
