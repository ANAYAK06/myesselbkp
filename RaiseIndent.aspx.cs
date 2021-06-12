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


public partial class RaiseIndent : System.Web.UI.Page
{
    
     SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 1);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (Session["roles"].ToString() == "Central Store Keeper")
            cc.Visible = true;
        else
            cc.Visible = false;
      
        if (!IsPostBack)
        {
            checkapprovals();
            FillGrid();

        }
        hf1.Value = Session["roles"].ToString();
    }
    public void  FillGrid()
    {
        try
        {
            //da = new SqlDataAdapter("Select item_code from indent_list where indent_no is null", con);
            //da.Fill(ds, "indent");

            //if (ds.Tables["indent"].Rows.Count > 0)
            //{
            if (Session["roles"].ToString() == "Central Store Keeper")

                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')basic_price,units,Replace(il.quantity,'.00','')quantity,Replace(il.Amount,'.00','')Amount from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,Replace(Amount,'.00','')Amount from indent_list where indent_no is null and (cc_code='CC-33' or cc_code='CCC' ))il on i.item_code=il.item_code order by il.id asc ", con);
            else
                da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,replace(basic_price,'.0000','.00')basic_price,units,Replace(il.quantity,'.00','')quantity,Replace(il.Amount,'.00','')Amount from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,Replace(Amount,'.00','')Amount from indent_list where indent_no is null and cc_code='" + Session["cc_code"].ToString() + "' )il on i.item_code=il.item_code order by il.id asc", con);

            da.Fill(ds, "fill");
            GridView1.DataSource = ds.Tables["fill"];
            GridView1.DataBind();

            if (ds.Tables["fill"].Rows.Count > 0)
            {
                btnvisible.Style.Add("visibility", "visible");
                description.Style.Add("visibility", "visible");
            }
            else
            {
                btnvisible.Style.Add("visibility", "hidden");
                description.Style.Add("visibility", "hidden");
            }
            //}
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
      
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
                da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_code='" + txtSearch.Text + "' and status in ('4','5','5A','2A')", con);
                da.Fill(ds, "checks");
                if (ds.Tables["checks"].Rows.Count == 1)
                {
                    if (Session["roles"].ToString() != "Central Store Keeper")
                        da = new SqlDataAdapter("SELECT top 1 il.item_code,it_code FROM indent_list il join item_codes ic on il.item_code=ic.item_code join subdca sd on ic.Subdca_Code=sd.subdca_code where indent_no is null and CC_code='" + Session["cc_code"].ToString() + "' order by il.Id asc ", con);
                    else
                        da = new SqlDataAdapter("SELECT top 1 il.item_code,it_code FROM indent_list il join item_codes ic on il.item_code=ic.item_code join subdca sd on ic.Subdca_Code=sd.subdca_code where indent_no is null and (cc_code='CC-33' or cc_code='CCC') order by il.Id asc ", con);
                    da.Fill(ds, "itemcodes");

                    if (Session["roles"].ToString() != "Central Store Keeper")
                        da = new SqlDataAdapter("Select distinct Dca_Code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,Replace(Amount,'.00','')Amount from indent_list where indent_no is null and cc_code='" + Session["cc_code"].ToString() + "' )il on i.item_code=il.item_code group by dca_code; SELECT ic.Dca_Code,sd.it_code FROM item_codes ic join subdca sd  on ic.Subdca_Code=sd.subdca_code where item_code='" + txtSearch.Text.ToUpper() + "'", con);
                    else
                        da = new SqlDataAdapter("Select distinct Dca_Code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,Replace(Amount,'.00','')Amount from indent_list where indent_no is null and (cc_code='CC-33' or cc_code='CCC' ))il on i.item_code=il.item_code group by dca_code ; SELECT ic.Dca_Code,sd.it_code FROM item_codes ic join subdca sd  on ic.Subdca_Code=sd.subdca_code where item_code='" + txtSearch.Text.ToUpper() + "'", con);
                    da.Fill(ds, "checkdca");
                    if ((ds.Tables["itemcodes"].Rows.Count == 0) || (ds.Tables["itemcodes"].Rows[0].ItemArray[1].ToString() == ds.Tables["checkdca1"].Rows[0].ItemArray[1].ToString()))
                    {
                        if ((ds.Tables["checkdca"].Rows.Count == 0) || (ds.Tables["checkdca"].Rows[0].ItemArray[0].ToString() == ds.Tables["checkdca1"].Rows[0].ItemArray[0].ToString()))
                        {
                            cmd.CommandText = "insert into indent_list(item_code,cc_code)values(@itemcode,@CCCode)";
                            cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = txtSearch.Text.ToUpper();
                            if (Session["roles"].ToString() == "Central Store Keeper")
                                cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                            else
                            {
                                cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                            }
                            int i = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (i == 1)
                            {

                            }

                            FillGrid();
                            (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                            txtSearch.Text = String.Empty;
                        }
                        else
                        {
                            JavaScript.UPAlert(Page, "Please add same DCA items only");
                            (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                            txtSearch.Text = String.Empty;
                        }
                    }
                    else
                    {
                        JavaScript.UPAlert(Page, "Please add only  " + ds.Tables["itemcodes"].Rows[0].ItemArray[1].ToString() + "  IT Code Items only in the Indent");
                        (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                        txtSearch.Text = String.Empty;
                    }

                }
                else
                {
                    JavaScript.UPAlert(Page, "Invalid itemcode");
                    (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                    txtSearch.Text = String.Empty;
                }

            }
            else
            {
                int n = txtSearch.Text.IndexOf(",");
                if (n != -1)
                {
                    string result = txtSearch.Text.Remove(txtSearch.Text.LastIndexOf(","));
                    string result1 = txtSearch.Text.Substring(txtSearch.Text.LastIndexOf(",") + 1);
                    da = new SqlDataAdapter("SELECT 1 as IsExists FROM item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','2A','5','5A')", con);
                    da.Fill(ds, "check");
                    if (ds.Tables["check"].Rows.Count == 1)
                    {

                        da = new SqlDataAdapter("Select item_code from item_codes where item_name='" + result + "' and specification='" + result1 + "' and status in ('4','2A','5','5A')", con);
                        da.Fill(ds, "search");
                        if (ds.Tables["search"].Rows.Count > 0)
                        {
                            if (Session["roles"].ToString() != "Central Store Keeper")
                                da = new SqlDataAdapter("SELECT top 1 il.item_code,it_code FROM indent_list il join item_codes ic on il.item_code=ic.item_code join subdca sd on ic.Subdca_Code=sd.subdca_code where indent_no is null and CC_code='" + Session["cc_code"].ToString() + "' order by il.Id asc ", con);
                            else
                                da = new SqlDataAdapter("SELECT top 1 il.item_code,it_code FROM indent_list il join item_codes ic on il.item_code=ic.item_code join subdca sd on ic.Subdca_Code=sd.subdca_code where indent_no is null and (cc_code='CC-33' or cc_code='CCC') order by il.Id asc ", con);
                            da.Fill(ds, "itemcodess");

                            if (Session["roles"].ToString() != "Central Store Keeper")
                                da = new SqlDataAdapter("Select distinct Dca_Code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,Replace(Amount,'.00','')Amount from indent_list where indent_no is null and cc_code='" + Session["cc_code"].ToString() + "' )il on i.item_code=il.item_code group by dca_code;  SELECT ic.Dca_Code,sd.it_code FROM item_codes ic join subdca sd  on ic.Subdca_Code=sd.subdca_code where item_code='" + ds.Tables["search"].Rows[0].ItemArray[0].ToString() + "'", con);
                            else
                                da = new SqlDataAdapter("Select distinct Dca_Code from (Select item_code,item_name,specification,dca_code,subdca_code,isnull(basic_price,0)basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select id,item_code,Replace(quantity,'.00','')quantity,Replace(Amount,'.00','')Amount from indent_list where indent_no is null and (cc_code='CC-33' or cc_code='CCC' ))il on i.item_code=il.item_code group by dca_code ;  SELECT ic.Dca_Code,sd.it_code FROM item_codes ic join subdca sd  on ic.Subdca_Code=sd.subdca_code where item_code='" + ds.Tables["search"].Rows[0].ItemArray[0].ToString() + "'", con);
                            da.Fill(ds, "checkdcas");
                            if ((ds.Tables["itemcodess"].Rows.Count == 0) || (ds.Tables["itemcodess"].Rows[0].ItemArray[1].ToString() == ds.Tables["checkdcas1"].Rows[0].ItemArray[1].ToString()))
                            {
                                if ((ds.Tables["checkdcas"].Rows.Count == 0) || (ds.Tables["checkdcas"].Rows[0].ItemArray[0].ToString() == ds.Tables["checkdcas1"].Rows[0].ItemArray[0].ToString()))
                                {
                                    cmd.CommandText = "insert into indent_list(item_code,cc_code)values(@itemcode,@CCCode)";
                                    cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = ds.Tables["search"].Rows[0].ItemArray[0].ToString();
                                    if (Session["roles"].ToString() == "Central Store Keeper")
                                        cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                                    else
                                    {
                                        cmd.Parameters.Add("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                                    }
                                    int j = Convert.ToInt32(cmd.ExecuteScalar());
                                    con.Close();
                                    if (j == 1)
                                    {

                                    }

                                    FillGrid();
                                    (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                                    txtSearch.Text = String.Empty;
                                }
                                else
                                {
                                    JavaScript.UPAlert(Page, "Please add same DCA items only");
                                    (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                                    txtSearch.Text = String.Empty;
                                }
                            }
                            else
                            {
                                JavaScript.UPAlert(Page, "Please add only  " + ds.Tables["itemcodess"].Rows[0].ItemArray[1].ToString() + "  IT Code Items only in the Indent");
                                (UpdatePanel1.FindControl("txtSearch") as TextBox).Text = String.Empty;
                                txtSearch.Text = String.Empty;
                            }
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
      
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure " +
                               "you want to delete this record?')) return true;";
                }
            }
        }
      
    }
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = GridView1.DataKeys[e.RowIndex]["id"].ToString();
            cmd.Connection = con;
            cmd.CommandText = "delete from indent_list where id='" + id + "'";
            con.Open();

            cmd.ExecuteNonQuery();
            con.Close();
            FillGrid();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Date = txtdate.Text;
            da = new SqlDataAdapter("select first_name+middle_name+last_name from Employee_Data where user_name='" + Session["user"].ToString() + "'", con);
            da.Fill(ds, "username");
            foreach (GridViewRow record in GridView1.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                if (c1.Checked)
                {
                    string id = "";
                    id = GridView1.DataKeys[record.RowIndex]["id"].ToString();
                    if (id != null)
                    {
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        //string Units = (record.FindControl("ddlunit") as DropDownList).SelectedValue;
                        string Qty = (record.FindControl("txtqty") as TextBox).Text;
                        //string Rate = (record.FindControl("txtrate") as TextBox).Text;
                        string Amount = (record.FindControl("txtamount") as TextBox).Text;
                        cmd.CommandText = "Update indent_list set quantity='" + Qty + "',Amount='" + Amount + "',basic_price='" + record.Cells[7].Text + "' where id='" + id + "'";
                        con.Open();
                        int i = Convert.ToInt32(cmd.ExecuteNonQuery());
                        con.Close();
                    }
                }
            }
            foreach (GridViewRow record in GridView1.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                if ((record.FindControl("ChkSelect") as CheckBox).Checked)
                {
                    ids = ids + GridView1.DataKeys[record.RowIndex]["id"].ToString() + ",";
                }
            }
            if (ids != "")
            {
                da = new SqlDataAdapter("RaiseIndent_sp", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ids", SqlDbType.VarChar).Value = ids;
                if (Session["roles"].ToString() == "Central Store Keeper")
                {
                    da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = Session["cc_code"].ToString();
                }
                da.SelectCommand.Parameters.AddWithValue("@Date", SqlDbType.VarChar).Value = Date;
                da.SelectCommand.Parameters.AddWithValue("@User", SqlDbType.VarChar).Value = Session["user"].ToString();
                da.SelectCommand.Parameters.AddWithValue("@Remarks", SqlDbType.VarChar).Value = txtdesc.Text + '$' + ds.Tables["username"].Rows[0][0].ToString();

            }
            da.Fill(ds, "raiseindent");
            if (ds.Tables["raiseindent"].Rows[0].ItemArray[0].ToString() == "Indent Generated Sucessfully")
            {
                JavaScript.UPAlertRedirect(Page, "Indent Generated Sucessfully. The Indent No is: " + ds.Tables["raiseindent"].Rows[0].ItemArray[1].ToString(), "RaiseIndent.aspx");
            }
            else
            {
                JavaScript.UPAlert(Page, ds.Tables["raiseindent"].Rows[0].ItemArray[0].ToString());
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow record in GridView1.Rows)
        {
            CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

            if (c1.Checked)
            {
                string id = GridView1.DataKeys[record.RowIndex]["id"].ToString();
                cmd.Connection = con;
                cmd.CommandText = "delete from indent_list where id='" + id + "'";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
        }
       
        FillGrid();
    }
    protected void ddlindenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        cascadingDCA cs = new cascadingDCA();
        if (ddlindenttype.SelectedValue != "3" && ddlindenttype.SelectedValue != "4")
        {
            cs.values(ddlindenttype.SelectedValue);
        }
        else if (ddlindenttype.SelectedValue == "3")
        {
            cs.values("'3'");
        }
        else if (ddlindenttype.SelectedValue == "4")
        {
            cs.values("'4'");
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
