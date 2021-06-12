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
using AjaxControlToolkit;

public partial class Issue : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("WareHouse");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 14);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager")
        {
            ddlcccode.Visible = true;
            lblcccode.Visible = true;
        }
        else
        {
            ddlcccode.Visible = false;
            lblcccode.Visible = false;
        }
        if (!IsPostBack)
        {
            FillGrid();
            LoadYear();
            //checkpercent();
            hfrole.Value = Session["roles"].ToString();
        }
    }
    public void LoadYear()
    {
        for (int i = 2005; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Any Year");
    }

    private void FillGrid()
    {

        if (Session["roles"].ToString() == "Central Store Keeper")
            da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc, remarks,status,T.id  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where (status='2') and type='1' order by T.status asc", con);
        else if (Session["roles"].ToString() == "PurchaseManager")
            da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc,remarks,status,T.id  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where  (status='2') and  type='1' order by T.status asc", con);
        else if (Session["roles"].ToString() == "StoreKeeper")
            da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc,remarks,status,T.id  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where status='4' and recieved_cc='" + Session["cc_code"].ToString() + "' and type='1' order by T.status asc", con);
        else if (Session["roles"].ToString() == "Project Manager")
            da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc,remarks,status,T.id  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where (status='3') and recieved_cc in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and type='1' order by T.status asc", con);
        else if (Session["roles"].ToString() == "Chief Material Controller")
            da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc,remarks,status,T.id  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where (status='2' or status='1' or status='1A')  and type='1' order by T.status asc", con);
        else if (Session["roles"].ToString() == "SuperAdmin")
            da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc,remarks,status,T.id  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where status='4'  and type='1' order by T.status asc", con);



        da.Fill(ds, "central");
        GridView1.DataSource = ds.Tables["central"];


        GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
        GridView1.DataBind();
    }




    protected void ddlpagecount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterissue();

        }


        else
        {
            FillGrid();
        }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterissue();

        }


        else
        {
            FillGrid();
        }
    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //--- For Paging ---------
        GridViewRow row = GridView1.BottomPagerRow;

        if (row == null)
        {
            return;
        }

        //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
        Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
        Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

        //if (lblPages != null)
        //{
        lblCurrent.Text = GridView1.PageCount.ToString();
        //}

        //if (lblCurrent != null)
        //{
        int currentPage = GridView1.PageIndex + 1;
        lblPages.Text = currentPage.ToString();
        //-- For First and Previous ImageButton
        if (GridView1.PageIndex == 0)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnFirst")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnPrev")).Visible = false;



        }

        //-- For Last and Next ImageButton
        if (GridView1.PageIndex + 1 == GridView1.PageCount)
        {
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnLast")).Visible = false;

            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Enabled = false;
            ((ImageButton)GridView1.BottomPagerRow.FindControl("btnNext")).Visible = false;


        }
    }
    protected void btnFirst_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnPrev_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void btnNext_Command(object sender, CommandEventArgs e)
    {

        Paginate(sender, e);
    }
    protected void btnLast_Command(object sender, CommandEventArgs e)
    {
        Paginate(sender, e);
    }
    protected void Paginate(object sender, CommandEventArgs e)
    {
        // Get the Current Page Selected
        int iCurrentIndex = GridView1.PageIndex;

        switch (e.CommandArgument.ToString().ToLower())
        {
            case "first":
                GridView1.PageIndex = 0;
                break;
            case "prev":
                if (GridView1.PageIndex != 0)
                {
                    GridView1.PageIndex = iCurrentIndex - 1;
                }
                break;
            case "next":
                GridView1.PageIndex = iCurrentIndex + 1;
                break;
            case "last":
                GridView1.PageIndex = GridView1.PageCount;
                break;
        }

        //Populate the GridView Control
        if (ddlyear.SelectedIndex != 0 || ddlMonth.SelectedIndex != 0 || ddlcccode.SelectedValue != "")
        {
            filterissue();

        }


        else
        {
            FillGrid();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        filterissue();
    }
    public void filterissue()
    {
        try
        {
            string Condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlMonth.SelectedIndex != 0)
                {
                    string yy = (ddlMonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    Condition = Condition + " and datepart(mm,transfer_date)=" + ddlMonth.SelectedValue + " and datepart(yy,transfer_date)=" + yy;
                }
                Condition = Condition + "and convert(datetime,transfer_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
            }
            if (Session["roles"].ToString() == "Central Store Keeper" || Session["roles"].ToString() == "PurchaseManager" || Session["roles"].ToString() == "Chief Material Controller" || Session["roles"].ToString() == "SuperAdmin" || Session["roles"].ToString() == "Project Manager")
            {
                if (ddlcccode.SelectedValue != "Select Cost Center")
                {
                    Condition = Condition + " and recieved_cc='" + ddlcccode.SelectedValue + "'";
                }
            }
            else
            {
                Condition = Condition + "and recieved_cc='" + Session["cc_code"].ToString() + "'";
            }

            if (Session["roles"].ToString() == "Central Store Keeper")
                da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc, remarks,status  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no   where T.id>0 and (status='4' or status='2') and type='1' " + Condition + " order by T.status asc", con);
            else if (Session["roles"].ToString() == "PurchaseManager")
                da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc, remarks,status  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where T.id>0 and (status='4' or status='2') and type='1' " + Condition + " order by T.status asc", con);
            else if (Session["roles"].ToString() == "StoreKeeper")
                da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc, remarks,status  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where T.id>0 and status='4' and type='1' " + Condition + " order by T.status asc", con);
            else if (Session["roles"].ToString() == "Project Manager")
                da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc, remarks,status  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where T.id>0 and (status='3' or status='4') and type='1' " + Condition + " order by T.status asc", con);
            else if (Session["roles"].ToString() == "Chief Material Controller")
                da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc, remarks,status  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where (status='4' or status='2' or status='1')  and type='1'" + Condition + " order by T.status asc", con);
            else if (Session["roles"].ToString() == "SuperAdmin")
                da = new SqlDataAdapter("SELECT  distinct T.ref_no as [Ref No],REPLACE(CONVERT(VARCHAR(11),convert(datetime,transfer_date,101),106), ' ', '-')as transfer_date ,i.cc_code,recieved_cc,remarks,status  from [Transfer info] T join [items transfer]i on T.Ref_no=i.ref_no where status='4'  and type='1'" + Condition + " order by T.status asc", con);


            da.Fill(ds, "issues");
            GridView1.DataSource = ds.Tables["issues"];
            GridView1.PageSize = Convert.ToInt32(ddlpagecount.SelectedItem.Text);
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }



    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Indent.aspx");
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image img = (Image)e.Row.FindControl("Image1");
            HiddenField hf = (HiddenField)e.Row.FindControl("h1");
            if (Session["roles"].ToString() == "Chief Material Controller")
            {
                if (hf.Value == "1A")
                    img.Visible = true;
                else
                    img.Visible = false;
            }

            else if (hf.Value == "1" || hf.Value == "3")
                img.Visible = true;
            else
                img.Visible = false;
        }
    }

    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            string Qty = "";
            string Amount = "";
            decimal Amount1 = 0;
            string dep1 = "";
            string DepAmounts = "";
            decimal Amount2 = 0;

            foreach (GridViewRow record in Grdeditpopup.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");
                da = new SqlDataAdapter("Select indent_no from indent_list where id='" + Grdeditpopup.DataKeys[record.RowIndex]["id"].ToString() + "'", con);
                da.Fill(ds, "indentno");

                if (ds.Tables["indentno"].Rows.Count > 0)
                {
                    if (c1.Checked && (record.FindControl("txtqty") as TextBox).Text != "")
                    {

                        ids = ids + Grdeditpopup.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        if (record.Cells[5].Text != "DCA-27")
                        {
                            Amount = Amount + ((Convert.ToDecimal(record.Cells[7].Text)) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text)) + ",";

                            if (record.Cells[5].Text == "DCA-11")
                                Amount11 = Amount11 + Convert.ToDecimal(Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text) * Convert.ToDecimal((record.Cells[7].Text)));
                            else if (record.Cells[5].Text == "DCA-41")
                                Amount41 = Amount41 + Convert.ToDecimal(Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text) * Convert.ToDecimal((record.Cells[7].Text)));
                        }
                        else
                        {
                            Amount = Amount + ",";

                        }

                    }
                }
            }
            if (Session["roles"].ToString() == "Project Manager")
            {

                cmd.Connection = con;
                cmd.CommandText = "Update [Transfer Info] Set status='4' where ref_no='" + ViewState["ref_no"].ToString() + "'";
                con.Open();
                bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (i == true)
                    JavaScript.UPAlertRedirect(Page, "Sucessfull", "Issue.aspx");
                else
                    JavaScript.UPAlertRedirect(Page, "Failed", "Issue.aspx");
                con.Close();

            }
            else if (Session["roles"].ToString() == "PurchaseManager")
            {
                cmd = new SqlCommand("IssuedItems_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@Qtys", Qty);
                cmd.Parameters.AddWithValue("@Amounts", Amount);
                cmd.Parameters.AddWithValue("@Amount11", Amount11);
                cmd.Parameters.AddWithValue("@Amount41", Amount41);
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@Roles", "PurchaseManager");
                cmd.Parameters.AddWithValue("@date", ViewState["date"].ToString());
                cmd.Parameters.AddWithValue("@REFNo", ViewState["ref_no"].ToString());
                cmd.Connection = con;
                con.Open();

                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg != "Sucessfull")
                    JavaScript.UPAlert(Page, msg);
                else
                    JavaScript.UPAlertRedirect(Page, msg, "Issue.aspx");
            }

            else if (Session["roles"].ToString() == "Chief Material Controller")
            {
                foreach (GridViewRow record in grdpopcmc.Rows)
                {
                    CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                    if (c1.Checked && record.Cells[5].Text != "DCA-27")
                    {

                        ids = ids + grdpopcmc.DataKeys[record.RowIndex]["id"].ToString() + ",";
                        Qty = Qty + (record.FindControl("txtqty") as TextBox).Text + ",";
                        {
                            if ((record.FindControl("ddldep") as DropDownList).SelectedValue != "Full Value")
                            {
                                string Dep = (record.FindControl("ddldep") as DropDownList).SelectedValue;
                                dep1 = dep1 + Convert.ToDecimal(Convert.ToDecimal(Dep)) + ",";
                                Amt = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                                decimal i = Convert.ToDecimal(record.Cells[10].Text) - Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text);
                                DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amt) - (((Convert.ToDecimal(Amt)) * (Convert.ToDecimal(Dep))) / 100)) + ",";
                                Amount1 = Amount1 + Convert.ToDecimal(Convert.ToDecimal(Amt) - (((Convert.ToDecimal(Amt)) * (Convert.ToDecimal(Dep))) / 100));
                                EffAmount = EffAmount + (((Convert.ToDecimal(Amt)) * (Convert.ToDecimal(Dep))) / 100) + (Convert.ToDecimal(Convert.ToDecimal(i) * Convert.ToDecimal(record.Cells[7].Text)));


                            }
                            else if ((record.FindControl("ddldep") as DropDownList).SelectedValue == "Full Value")
                            {
                                Amount2 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));
                                DepAmounts = DepAmounts + Convert.ToDecimal(Convert.ToDecimal(Amount2)) + ",";
                                Amount1 = Amount1 + Convert.ToDecimal(Convert.ToDecimal(Amount2));

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
                cmd.Parameters.AddWithValue("@Amount", Amount1);
                cmd.Parameters.AddWithValue("@Days", ddlDays.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
                cmd.Parameters.AddWithValue("@ReferenecNo", ViewState["ref_no"].ToString());
                cmd.Parameters.AddWithValue("@role", Session["roles"].ToString());
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "Sucessfull")
                    JavaScript.UPAlertRedirect(Page, msg, "Issue.aspx");
                else
                    JavaScript.UPAlert(Page, msg);

            }
        }





        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    private decimal Amount = (decimal)0.0;
    private decimal Amount11 = (decimal)0.0;
    private decimal Amount41 = (decimal)0.0;
   
    private decimal EffAmount = (decimal)0.0;

    public void mdlfillgrd()
    {
        if (Session["roles"].ToString() == "Chief Material Controller")
        {
            da = new SqlDataAdapter("Select K.*,isnull(j.quantity,0) as [Available Qty] from (Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,units,isnull(Replace(basic_price,'.0000','.00'),0)as basic_price,[Issued Qty],il.remarks,csk_percent,replace(csk_dep,'.0000','.00') as csk_dep ,transfer_date,replace((ISNULL(il.basic_price,0)*ISNULL(il.quantity,0)),'.0000','.00')as qty from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code )i right outer join (Select it.id,item_code,quantity as [Issued Qty],it.basic_price,ti.remarks,csk_percent,csk_dep,ti.transfer_date,it.quantity from [Transfer Info]ti join [Items transfer]it on ti.ref_no=it.ref_no where status='1A' and type='1' AND it.ref_no='" + ViewState["ref_no"].ToString() + "')il on i.item_code=il.item_code)k join (Select item_code,quantity from master_data where cc_code =(Select distinct tin.cc_code from [Transfer Info]tin join [Items transfer]itt on tin.ref_no=itt.ref_no where status='1A' and type='1'))j on j.item_code=k.item_code", con);
            da.Fill(ds, "filling");
            grdpopcmc.DataSource = ds.Tables["filling"];
            grdpopcmc.DataBind();
            if (ds.Tables["filling"].Rows.Count > 0)
            {
                txtremarks.Text = ds.Tables["filling"].Rows[0].ItemArray[9].ToString();
                txtdate.Text = ds.Tables["filling"].Rows[0].ItemArray[12].ToString();
            }
            else
            {
                txtremarks.Text = "";
                txtdate.Text = "";
            }

        }
        else
        {
            da = new SqlDataAdapter("Select   il.id,il.item_code,item_name,specification,dca_code,subdca_code,basic_price,units,il.requiredqty,il.quantity,isnull([Available qty],0) [Available Qty] from (Select item_code,item_name,specification,dca_code,subdca_code,basic_price,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,requiredqty,quantity,(Select quantity from [New Items] where cc_code='CC-33' and item_code=it.item_code)as [Available Qty] from [items transfer]it where  ref_no='" + ViewState["ref_no"].ToString() + "' )il on i.item_code=substring(il.item_code,1,8)", con);

            da.Fill(ds, "idinfo1");

            Grdeditpopup.DataSource = ds.Tables["idinfo1"];
            Grdeditpopup.DataBind();

        }
    }
    public void mdlfillview()
    {

        grdpopcmc.Visible = false;
       
            da = new SqlDataAdapter("Select il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(il.quantity,'.00','')quantity,item_status,Replace(il.amount,'.00','')amount,transfer_date from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select item_code,quantity,item_status,isnull(Amount,0)Amount,transfer_date from [items transfer]it join [Transfer Info]ti on it.ref_no=ti.ref_no where  ti.ref_no='" + ViewState["ref_no"].ToString() + "' )il on i.item_code=substring(il.item_code,1,8)", con);
            da.Fill(ds, "fillview");
            gridviewpopup.DataSource = ds.Tables["fillview"];
            gridviewpopup.DataBind();
            grdbill.DataSource = ds.Tables["fillview"];
            lbldate.Text = ds.Tables["fillview"].Rows[0].ItemArray[9].ToString();
            grdbill.DataBind();
        



    }


    //Select il.item_code,item_name,specification,dca_code,subdca_code,units,Replace(il.quantity,'.00','')quantity,Replace(il.amount,'.00','')amount from (Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select item_code,quantity,Amount from [items transfer] where  ref_no='" + ViewState["refno"].ToString() + "' )il on i.item_code=il.item_code


    protected void Grdeditpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int rowIndex = Convert.ToInt32(e.Row.DataItemIndex) + 1;
            for (int i = 1; i < e.Row.Cells.Count - 1; i++)
            {
                //EventHandler evtHandler = new EventHandler();
                //evtHandler = "updateValue(" + GridView1.ClientID + "," + rowIndex + ")";
                //((TextBox)e.Row.FindControl("TextBox" + i.ToString())).Attributes.Add("onblur", evtHandler);
            }
           
            
        }
    }
    protected void GridView1_RowEditing2(object sender, GridViewEditEventArgs e)
    {

        string refno = GridView1.DataKeys[e.NewEditIndex]["Ref No"].ToString();
        string date = GridView1.Rows[e.NewEditIndex].Cells[4].Text;
        txtrecieptdate.Text = GridView1.Rows[e.NewEditIndex].Cells[4].Text;
        ViewState["ref_no"] = refno;
        ViewState["date"] = date;
        HiddenField hf = (HiddenField)GridView1.Rows[e.NewEditIndex].Cells[7].FindControl("h1");
        printing.Visible = false;
        print.Visible = false;
        if (hf.Value == "1" && Session["roles"].ToString() == "Project Manager")/* this is for purchasemanager*/
        {

            popindents.Show();
            mdlfillgrd();
            tblsearch.Visible = true;
            Grdeditpopup.Visible = true;
            gridviewpopup.Visible = false;
            grdpopcmc.Visible = false;
            cmcdetails.Visible = false;
            btnu.Visible = true;
            print.Visible = false;
        }
        if (hf.Value == "1A" && Session["roles"].ToString() == "Chief Material Controller")/* cmc*/
        {

            popindents.Show();
            mdlfillgrd();
            tblsearch.Visible = false;
            Grdeditpopup.Visible = false;
            grdpopcmc.Visible = true;
            cmcdetails.Visible = true;
            gridviewpopup.Visible = false;

            btnu.Visible = true;
            print.Visible = false;
        }
        if (hf.Value == "3")/* this is for Project Manager*/
        {


            tblsearch.Visible = false;
            Grdeditpopup.Visible = false;
            gridviewpopup.Style.Add("visibility", "visible");
            btnu.Visible = true;
            grdpopcmc.Visible = false;
            cmcdetails.Visible = false;
            mdlfillview();
            popindents.Show();
        }
        else if (hf.Value == "2" || hf.Value == "4")
        {

            popindents.Hide();
        }

    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string refno = GridView1.SelectedValue.ToString();
        
        GridViewRow id =  GridView1.SelectedRow;
        string CCCode = id.Cells[6].Text;
        txtrecieptdate.Text = id.Cells[4].Text;
        ViewState["ToCC"] = CCCode;
        print.Visible = true;
        ViewState["ref_no"] = refno;
        tblsearch.Visible = false;
        Grdeditpopup.Visible = false;
        gridviewpopup.Visible = true;
        CCInfo();
        pname();
        btnu.Visible = false;
        printing.Visible = true;
        mdlfillview();
        popindents.Show();
        //da = new SqlDataAdapter("Select indent_no from indents where cc_code='" + GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text + "' and status='3'", con);
        //da.Fill(ds, "clearence");
        //if (ds.Tables["clearence"].Rows.Count > 0)
        //{
        //    string msg = "You Should give clearence to IndentNO:'" + ds.Tables["clearence"].Rows[0].ItemArray[0].ToString() + "'";
        //    JavaScript.UPAlertRedirect(Page,msg,"Inbox.aspx");

        //}
        //else
        //{
        //    da = new SqlDataAdapter("select status from [Transfer Info] where ref_no='" + refno + "'", con);
        //    da.Fill(ds, "check");
        //    if ("1" == ds.Tables["check"].Rows[0].ItemArray[0].ToString())
        //        btnu.Visible = true;
        //    else
        //        btnu.Visible = false;
        //    mdlfillgrd();
        //    popindents.Show();
        //}
    }


    protected void Grdeditpopup_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            TextBox txtqty = (TextBox)e.Row.FindControl("txtqty");
            if (e.Row.Cells[9].Text == e.Row.Cells[10].Text)
                txtqty.Attributes.Add("readonly", "1");

        }
      
    }

    protected void gridviewpopup_RowDataBound(object sender, GridViewRowEventArgs e)
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

    public void CCInfo()
    {
        try
        {
            da = new SqlDataAdapter("select cc_name,address from Cost_Center where cc_code='" + ViewState["ToCC"].ToString() + "'", con);
            da.Fill(ds, "CCInfo");
            
            lbldespto.Text = ds.Tables["CCInfo"].Rows[0].ItemArray[0].ToString();
            lbldesptoadd.Text = ds.Tables["CCInfo"].Rows[0].ItemArray[1].ToString();
            lblchallanno.Text = ViewState["ref_no"].ToString();
            //lbldate.Text = ViewState["date"].ToString();

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
            //da = new SqlDataAdapter("select first_name+' ' +last_name from employee_data r join user_roles u on r.User_Name=u.User_Name where u.Roles ='PurchaseManager'", con);
            da = new SqlDataAdapter("Select first_name,last_name from employee_data r join [transfer info]u on r.user_name=u.created_by where u.ref_no='" + ViewState["ref_no"].ToString() + "'", con);
            da.Fill(ds, "pnameinfo");
            if (ds.Tables["pnameinfo"].Rows.Count > 0)
            {

                lblpurchasemanagername.Text = ds.Tables["pnameinfo"].Rows[0][0].ToString() + "  " + ds.Tables["pnameinfo"].Rows[0][1].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    private decimal Amt = (decimal)0.0;
    private decimal EffAmt = (decimal)0.0;

    protected void grdpopcmc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            CascadingDropDown cc = (CascadingDropDown)e.Row.FindControl("CascadingDropDown5");
            HiddenField h1 = (HiddenField)e.Row.FindControl("hfchk");
            cc.SelectedValue = h1.Value;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "qty"));
            EffAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "csk_dep"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[13].Text = String.Format("Rs. {0:#,##,##,###.00}", Amt);
            e.Row.Cells[14].Text = String.Format("Rs. {0:#,##,##,###.00}", EffAmt);

        }
    }

    public void checkpercent()
    {
        da = new SqlDataAdapter("Select issue_percent from semiasset_dep", con);
        da.Fill(ds, "Find");
        hfcheck.Value = ds.Tables["Find"].Rows[0].ItemArray[0].ToString();
    }
}
