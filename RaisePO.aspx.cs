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
using System.IO;
using System.Text;
using AjaxControlToolkit;
using System.Collections.Specialized;
using System.Web.Services;
using System.Web.Services.Protocols;


public partial class RaisePO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings ["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlDataAdapter da1 = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();

    DataSet ds = new DataSet();
    DataTable dtemp1 = new DataTable();
    DataTable dt = new DataTable();
    SqlCommand cmd1 = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        esselDal RoleCheck = new esselDal();
        int rec = 0;
        rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 3);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");

        if (!IsPostBack)
        {
            challantabledo.Visible = false;
            challantablepo.Visible = false;
            ddlindentno.Visible = true;
            fillpartindents();
            tblbtns.Visible = true;
            lblskremark.Visible = false;
            lblpmremark.Visible = false;
            lblcskremark.Visible = false;
            lblpurmremark1.Visible = false;
            lblcmcremark.Visible = false;
            lblsaremark.Visible = false;
        }      
        Response.Cache.SetCacheability(HttpCacheability.NoCache); 
      
    }   
   
    private void FillGrid1()
    {
        //const string key2 = "emp2";

        grdbill.DataSource = Session["SessionTable2"];
        grdbill.DataBind();
    }
    private void FillGridpo()
    {
        grdbillpo.DataSource = Session["SessionTable2"];
        grdbillpo.DataBind();
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "upindent", "javascript:Calculate()", true);
    }
    

  
    protected void btnprint_Click(object sender, EventArgs e)
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        da = new SqlDataAdapter("Select Vendor_name,address from vendor where vendor_id='"+ddlvendor.SelectedValue+"' and vendor_type='Supplier'", con);
        da.Fill(ds, "info");
        if (ds.Tables["info"].Rows.Count > 0)
        {

            //txtaddress.Text =ds.Tables["info"].Rows[0].ItemArray[0].ToString()+"\n"+ds.Tables["info"].Rows[0].ItemArray[1].ToString(); 
            lblname.Text = ds.Tables["info"].Rows[0].ItemArray[0].ToString();
            lbladdress.Text = ds.Tables["info"].Rows[0].ItemArray[1].ToString();
        }
        FillGrid1();
        challantabledo.Visible = true;
        challantablepo.Visible = false;
        //tblprinter.Visible = true;
        pname();
        if (ddlindent.SelectedItem.Text != "Select Indent")
        {
            //if (ddlindent.SelectedItem.Text.Split('/')[0] != "CCC")
            //    lblcc.Text = ddlindent.SelectedItem.Text.Split('/')[0];
            //else
                lblcc.Text = ddlindent.SelectedItem.Text.Split('/')[0];

        }
        else if (ddlindentno.SelectedItem.Text != "Select Indent No")
        {
            //if (ddlindentno.SelectedItem.Text.Split('/')[0] != "CCC")
            //    lblcc.Text = ddlindentno.SelectedItem.Text.Split('/')[0];
            //else
                lblcc.Text = ddlindentno.SelectedItem.Text.Split('/')[0];
        }
    }
    protected void btnsubmitpo_Click(object sender, EventArgs e)
    {

        da = new SqlDataAdapter("Select Vendor_name,address from vendor where vendor_id='" + ddlvendor.SelectedValue + "' and vendor_type='Supplier'", con);
        da.Fill(ds, "info");
        if (ds.Tables["info"].Rows.Count > 0)
        {
            lblnamepo.Text = ds.Tables["info"].Rows[0].ItemArray[0].ToString();
            lbladdresspo.Text = ds.Tables["info"].Rows[0].ItemArray[1].ToString();
        }
        FillGridpo();
        BindGridviewterm();
        challantabledo.Visible = false;
        challantablepo.Visible = true;        
        pnamepo();
        if (ddlindent.SelectedItem.Text != "Select Indent")
        {
            lblcc.Text = ddlindent.SelectedItem.Text.Split('/')[0];
        }
        else if (ddlindentno.SelectedItem.Text != "Select Indent No")
        {
            lblcc.Text = ddlindentno.SelectedItem.Text.Split('/')[0];
        }
    }
    protected void btncancelprint_Click(object sender, EventArgs e)
    {
        Response.Redirect("RaisePO.aspx");

    }
    protected void btncancelprintpo_Click(object sender, EventArgs e)
    {
        Response.Redirect("RaisePO.aspx");

    }
    protected void PrintAllPages(object sender, EventArgs e)
    {
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        tblpo.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

    }
    private decimal Amount = (decimal)0.0;
    protected void btnupdateprint_Click(object sender, EventArgs e)
    {
        string ids = "";
        string Qty = "";        
        string Amt = "";
        string Bprice = "";
        string item_code = "";

        try
        {      

            foreach (GridViewRow record in grdbill.Rows)
            {
                ids = ids + Convert.ToDecimal(record.Cells[1].Text) + ",";
                Qty = Qty + Convert.ToDecimal(record.Cells[7].Text) + ",";
                item_code = item_code +(record.Cells[2].Text) + ",";
                Amt = Amt + Convert.ToDecimal(Convert.ToDecimal(record.Cells[5].Text) * Convert.ToDecimal(record.Cells[7].Text)) + ",";
                Amount = Amount + Convert.ToDecimal(Convert.ToDecimal(record.Cells[5].Text) * Convert.ToDecimal(record.Cells[7].Text));
                Bprice = Bprice + Convert.ToDecimal(record.Cells[5].Text) + ",";

            }
            SqlCommand cmd = new SqlCommand();
         
            da = new SqlDataAdapter("RaisePO_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@podate", SqlDbType.DateTime).Value = txtpodate.Text;
            da.SelectCommand.Parameters.AddWithValue("@refno", SqlDbType.VarChar).Value = txtrefno.Text;
            da.SelectCommand.Parameters.AddWithValue("@refdate", SqlDbType.VarChar).Value = txtrefdate.Text;
            //if (lblcc.Text == "CCC")
            //    da.SelectCommand.Parameters.AddWithValue("@indent", SqlDbType.VarChar).Value = txtindent.Text.Split('/')[2];
            //else
            da.SelectCommand.Parameters.AddWithValue("@indent", SqlDbType.VarChar).Value = txtindent.Text.Split('/')[2];
            da.SelectCommand.Parameters.AddWithValue("@indentno", SqlDbType.VarChar).Value = txtindent.Text;
            da.SelectCommand.Parameters.AddWithValue("@vendorname", SqlDbType.VarChar).Value = lblname.Text;
            da.SelectCommand.Parameters.AddWithValue("@remarks", SqlDbType.VarChar).Value = txtremarks.Text;

            da.SelectCommand.Parameters.AddWithValue("@amount", SqlDbType.VarChar).Value = Amount;
            
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblcc.Text;
            da.SelectCommand.Parameters.AddWithValue("@DCACode", SqlDbType.VarChar).Value = ViewState["DCACode"].ToString();           
            da.SelectCommand.Parameters.AddWithValue("@User", SqlDbType.VarChar).Value = Session["user"].ToString();
            da.SelectCommand.Parameters.AddWithValue("@Recieved_cc", SqlDbType.VarChar).Value = txtrecieved_cc.Text;
            da.SelectCommand.Parameters.AddWithValue("@Recieved_date", SqlDbType.VarChar).Value = txtrecieved_date.Text;

            da.SelectCommand.Parameters.AddWithValue("@ids", SqlDbType.VarChar).Value = ids;
            da.SelectCommand.Parameters.AddWithValue("@Qty", SqlDbType.VarChar).Value = Qty;
            da.SelectCommand.Parameters.AddWithValue("@item_code", SqlDbType.VarChar).Value = item_code;
            da.SelectCommand.Parameters.AddWithValue("@Bprice", SqlDbType.VarChar).Value = Bprice;
            da.SelectCommand.Parameters.AddWithValue("@Amt", SqlDbType.VarChar).Value = Amt;
            da.SelectCommand.Parameters.AddWithValue("@SiteAddress", SqlDbType.VarChar).Value = txtSaddress.Text;
            da.SelectCommand.Parameters.AddWithValue("@SiteAddress2", SqlDbType.VarChar).Value = txtaddress2.Text;
            da.SelectCommand.Parameters.AddWithValue("@ContactPerson", SqlDbType.VarChar).Value = txtCPerson.Text;
            da.SelectCommand.Parameters.AddWithValue("@mobilenum", SqlDbType.VarChar).Value = txtMobileNum.Text;

            da.SelectCommand.Parameters.AddWithValue("@InvAddress", SqlDbType.VarChar).Value = txtinvadd1.Text;
            da.SelectCommand.Parameters.AddWithValue("@InvAddress2", SqlDbType.VarChar).Value = txtinvadd2.Text;
            da.SelectCommand.Parameters.AddWithValue("@Invgstno", SqlDbType.VarChar).Value = txtinvgst.Text;
            da.SelectCommand.Parameters.AddWithValue("@Invmobilenum", SqlDbType.VarChar).Value = txtinvmobileno.Text;

            da.Fill(ds, "pono");

            if (ds.Tables["pono"].Rows[0][0].ToString() == "Sucessfull")
            {
                txtpono.Text = ds.Tables["pono"].Rows[0][1].ToString();
                showalert("Sucessfull");
            }
            else
            {
                JavaScript.Alert(ds.Tables["pono"].Rows[0][0].ToString());
            }
            con.Close();
          
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    private decimal PAmount = (decimal)0.0;
    private decimal Standardamount = (decimal)0.0;
    protected void btnupdateprintpo_Click(object sender, EventArgs e)
    {      
        string PIds = "";
        string PItem_codes = "";
        string PBprices = "";
        string PQtys = "";
        string PAmts = "";
        string oldBprices = "";
        string PSAmts = "";
        string Quotedprices = "";
        string MDescs = "";
        try
        {

            foreach (GridViewRow record in grdbillpo.Rows)
            {
                PIds = PIds + Convert.ToDecimal(record.Cells[1].Text) + ",";
                PItem_codes = PItem_codes + (record.Cells[2].Text) + ",";
                oldBprices = oldBprices + (record.Cells[7].Text) + ",";
                PBprices = PBprices + (record.FindControl("txtpurprice") as TextBox).Text + ",";
                Quotedprices = Quotedprices + (record.FindControl("txtquprice") as TextBox).Text + ",";  
                PQtys = PQtys + Convert.ToDecimal(record.Cells[5].Text) + ",";
                PAmts = PAmts + Convert.ToDecimal(Convert.ToDecimal((record.FindControl("txtpurprice") as TextBox).Text) * Convert.ToDecimal(record.Cells[5].Text)) + ",";
                PSAmts = PSAmts + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[5].Text)) + ",";
                PAmount = PAmount + Convert.ToDecimal(Convert.ToDecimal((record.FindControl("txtpurprice") as TextBox).Text) * Convert.ToDecimal(record.Cells[5].Text));             
                Standardamount = Standardamount + Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal(record.Cells[5].Text));

            }
            foreach (GridViewRow record in grdterms.Rows)
            {
                if ((record.FindControl("chkSelectterms") as CheckBox).Checked)
                {
                    TextBox txtterm = (TextBox)record.FindControl("txtterms");
                    string termdesc = txtterm.Text;
                    if (termdesc != "")
                    {
                        MDescs = MDescs + termdesc + "$";
                    }
                }
            }
                cmd1 = new SqlCommand("checkpobudget", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@PIds", PIds);
                cmd1.Parameters.AddWithValue("@PItemcodes", PItem_codes);
                cmd1.Parameters.AddWithValue("@PBprices", PBprices);
                cmd1.Parameters.AddWithValue("@PQtys", PQtys);
                cmd1.Parameters.AddWithValue("@PAmts", PAmts);
                cmd1.Parameters.AddWithValue("@PAmount", PAmount); 
                cmd1.Parameters.AddWithValue("@Stamount", Standardamount);                
                cmd1.Parameters.AddWithValue("@IndentNo", txtindent.Text);
                cmd1.Parameters.AddWithValue("@DCACode", ViewState["DCACode"].ToString());
                cmd1.Connection = con;
                con.Open();
                string result = cmd1.ExecuteScalar().ToString();
                //string result = "";
                con.Close();
                if (result == "sufficent")
                {
                    SqlCommand cmd = new SqlCommand();

                    da = new SqlDataAdapter("RaisePONew_sp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@podate", SqlDbType.DateTime).Value = txtpodatepo.Text;
                    da.SelectCommand.Parameters.AddWithValue("@refno", SqlDbType.VarChar).Value = txtrefnopo.Text;
                    da.SelectCommand.Parameters.AddWithValue("@refdate", SqlDbType.VarChar).Value = txtrefdatepo.Text;
                    
                    da.SelectCommand.Parameters.AddWithValue("@indent", SqlDbType.VarChar).Value = txtindent.Text.Split('/')[2];
                    da.SelectCommand.Parameters.AddWithValue("@indentno", SqlDbType.VarChar).Value = txtindent.Text;
                    da.SelectCommand.Parameters.AddWithValue("@vendorname", SqlDbType.VarChar).Value = lblnamepo.Text;
                    da.SelectCommand.Parameters.AddWithValue("@remarks", SqlDbType.VarChar).Value = MDescs;

                    da.SelectCommand.Parameters.AddWithValue("@amount", SqlDbType.VarChar).Value = PAmount;
                    da.SelectCommand.Parameters.AddWithValue("@stamount", SqlDbType.VarChar).Value = Standardamount;

                    da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = lblcc.Text;
                    da.SelectCommand.Parameters.AddWithValue("@DcaCode", SqlDbType.VarChar).Value = ViewState["DCACode"].ToString();
                    da.SelectCommand.Parameters.AddWithValue("@User", SqlDbType.VarChar).Value = Session["user"].ToString();
                    //da.SelectCommand.Parameters.AddWithValue("@Recieved_cc", SqlDbType.VarChar).Value = txtrecieved_ccpo.Text;
                    //da.SelectCommand.Parameters.AddWithValue("@Recieved_date", SqlDbType.VarChar).Value = txtrecieved_datepo.Text;

                    da.SelectCommand.Parameters.AddWithValue("@ids", SqlDbType.VarChar).Value = PIds;
                    da.SelectCommand.Parameters.AddWithValue("@Qty", SqlDbType.VarChar).Value = PQtys;
                    da.SelectCommand.Parameters.AddWithValue("@item_code", SqlDbType.VarChar).Value = PItem_codes;
                    da.SelectCommand.Parameters.AddWithValue("@oldBprice", SqlDbType.VarChar).Value = oldBprices;
                    da.SelectCommand.Parameters.AddWithValue("@Bprice", SqlDbType.VarChar).Value = PBprices;
                    da.SelectCommand.Parameters.AddWithValue("@Quotedprice", SqlDbType.VarChar).Value = Quotedprices;
                    da.SelectCommand.Parameters.AddWithValue("@Amt", SqlDbType.VarChar).Value = PAmts;
                    da.SelectCommand.Parameters.AddWithValue("@SAmt", SqlDbType.VarChar).Value = PSAmts;
                    da.SelectCommand.Parameters.AddWithValue("@SiteAddress", SqlDbType.VarChar).Value = txtSaddresspo.Text;
                    da.SelectCommand.Parameters.AddWithValue("@SiteAddress2", SqlDbType.VarChar).Value = txtaddress2po.Text;
                    da.SelectCommand.Parameters.AddWithValue("@ContactPerson", SqlDbType.VarChar).Value = txtCPersonpo.Text;
                    da.SelectCommand.Parameters.AddWithValue("@mobilenum", SqlDbType.VarChar).Value = txtMobileNumpo.Text;

                    da.SelectCommand.Parameters.AddWithValue("@InvAddress", SqlDbType.VarChar).Value = txtinvadd1po.Text;
                    da.SelectCommand.Parameters.AddWithValue("@InvAddress2", SqlDbType.VarChar).Value = txtinvadd2po.Text;
                    da.SelectCommand.Parameters.AddWithValue("@Invgstno", SqlDbType.VarChar).Value = txtinvgstpo.Text;
                    da.SelectCommand.Parameters.AddWithValue("@Invmobilenum", SqlDbType.VarChar).Value = txtinvmobilenopo.Text;

                    da.Fill(ds, "pono");
                    //da.Fill(ds, "");

                    if (ds.Tables["pono"].Rows[0][0].ToString() == "Sucessfull")
                    {
                        txtponopo.Text = ds.Tables["pono"].Rows[0][1].ToString();
                        showalert("Sucessfull");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "upindent", "javascript:Calculate()", true);
                        JavaScript.Alert(ds.Tables["pono"].Rows[0][0].ToString());
                    }

                    con.Close();
                }
                else
                {
                    JavaScript.Alert(result);
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "upindent", "javascript:Calculate()", true);
                }

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void showalert(string message)
    {
        Label myalertlabel = new Label();
        Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "1", "RaisePO.aspx");
        myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "');window.location='RaisePO.aspx'</script>";

        //myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "');window.location='RaisePO.aspx';print()</script>";
        Page.Controls.Add(myalertlabel);
    }
    public void pname()
    {
        try
        {
            da = new SqlDataAdapter("select first_name+' ' +last_name from employee_data r join user_roles u on r.User_Name=u.User_Name where u.Roles ='PurchaseManager'", con);
            da.Fill(ds, "pnameinfo");
            if(ds.Tables["pnameinfo"].Rows.Count>0)
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
    public void pnamepo()
    {
        try
        {
            da = new SqlDataAdapter("select first_name+' ' +last_name from employee_data r join user_roles u on r.User_Name=u.User_Name where u.Roles ='PurchaseManager'", con);
            da.Fill(ds, "pnameinfopo");
            if (ds.Tables["pnameinfopo"].Rows.Count > 0)
            {
                lblpurchasemanagernamepo.Text = ds.Tables["pnameinfopo"].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillpartindents()
    {
        try
        {
            da = new SqlDataAdapter("Select distinct indent_no from PO_details where indent_no in (Select indent_no from indents where status not in ('7'))", con);
            da.Fill(ds, "indentno");           
            ddlindentno.DataTextField = "indent_no";
            ddlindentno.DataValueField = "indent_no";
            ddlindentno.DataSource = ds.Tables["indentno"];
            ddlindentno.DataBind();       
            ddlindentno.Items.Insert(0, "Select Indent No");            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }   
    protected void btnremaindmelater_Click(object sender, EventArgs e)
    {

        fillgridnew();
      
    }
    public void fillgridnew()
    {
        try
        {
            da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity from((Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,quantity,Basic_Price from indent_list i where  indent_no='" + ddlindent.SelectedItem.Text + "' AND quantity!=0 )il on i.item_code=il.item_code) ORDER BY id Asc;select LEFT(Remarks, CHARINDEX('$', Remarks) - 1) AS Remarks,RIGHT(Remarks, CHARINDEX('$', REVERSE(Remarks)) - 1) AS SKName,LEFT(Pmremarks, CHARINDEX('$', Pmremarks) - 1) AS Pmremarks,RIGHT(Pmremarks, CHARINDEX('$', REVERSE(Pmremarks)) - 1) AS PMName,LEFT(Cskremarks, CHARINDEX('$', Cskremarks) - 1) AS Cskremarks,RIGHT(Cskremarks, CHARINDEX('$', REVERSE(Cskremarks)) - 1) AS CSKName,LEFT(purmremark, CHARINDEX('$', purmremark) - 1) AS purmremark,RIGHT(purmremark, CHARINDEX('$', REVERSE(purmremark)) - 1) AS purmname,LEFT(cmcremarks, CHARINDEX('$', cmcremarks) - 1) AS cmcremarks,RIGHT(cmcremarks, CHARINDEX('$', REVERSE(cmcremarks)) - 1) AS cmcname,LEFT(Saremarks, CHARINDEX('$', Saremarks) - 1) AS saremarks,RIGHT(Saremarks, CHARINDEX('$', REVERSE(Saremarks)) - 1) AS saname from indents where indent_no='" + ddlindent.SelectedItem.Text + "'", con);
            da.Fill(ds, "modalpopup");
            Grditemspopup.DataSource = ds.Tables["modalpopup"];
            Grditemspopup.DataBind();
            popitems.Show();
            txtindent.Text = ddlindent.SelectedItem.Text;
            lblskremark.Visible = true;
            lblpmremark.Visible = true;
            lblcskremark.Visible = true;
            lblpurmremark1.Visible = true;
            lblcmcremark.Visible = true;
            lblsaremark.Visible = true;
            LblSkremarks.Text = ds.Tables["modalpopup1"].Rows[0][0].ToString();
            Lblskname.Text = ds.Tables["modalpopup1"].Rows[0][1].ToString();
            LblPmremarks.Text = ds.Tables["modalpopup1"].Rows[0][2].ToString();
            Lblpmname.Text = ds.Tables["modalpopup1"].Rows[0][3].ToString();
            LblCskremarks.Text = ds.Tables["modalpopup1"].Rows[0][4].ToString();
            Lblcskname.Text = ds.Tables["modalpopup1"].Rows[0][5].ToString();
            Lblpurmremark.Text = ds.Tables["modalpopup1"].Rows[0][6].ToString();
            Lblpurname.Text = ds.Tables["modalpopup1"].Rows[0][7].ToString();
            LdlCmcremarks.Text = ds.Tables["modalpopup1"].Rows[0][8].ToString();
            Lblcmcname.Text = ds.Tables["modalpopup1"].Rows[0][9].ToString();
            LblSaremarks.Text = ds.Tables["modalpopup1"].Rows[0][10].ToString();
            Lblsaname.Text = ds.Tables["modalpopup1"].Rows[0][11].ToString();
            tblbtns.Visible = false;
            trgrid.Visible = true;
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
 
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Canceledindent_sp";

            cmd.Parameters.AddWithValue("@Indent_No", ddlindentno.SelectedItem.Text);
            cmd.Connection = con;
            con.Open();
            string msg = cmd.ExecuteScalar().ToString();
            con.Close();
            if (msg == "Indent Succefully Closed")            
                JavaScript.UPAlertRedirect(Page, msg, "RaisePO.aspx");
            else
                JavaScript.UPAlert(Page, msg);

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
  
    private decimal Amount1 = (decimal)0.0;
    protected void btnmdlupd_Click(object sender, EventArgs e)
    {
        try
        {
            string Sids = "";
            string SQty = "";
            //string SAmt = "";
            string Sitem_code = "";
            string Sitem_name = "";
            string Sspec = "";
            string Sdca = "";
            string Ssubdca = "";
            string Sbprice = "";
            string Sunits = "";
            DataColumn dc1 = new DataColumn("id");
            DataColumn dc2 = new DataColumn("Item_code");
            DataColumn dc3 = new DataColumn("item_name");
            DataColumn dc4 = new DataColumn("Specification");
            DataColumn dc5 = new DataColumn("dca_code");
            DataColumn dc6 = new DataColumn("Subdca_code");
            DataColumn dc7 = new DataColumn("basic_price");
            DataColumn dc8 = new DataColumn("units");
            DataColumn dc9 = new DataColumn("quantity");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            foreach (GridViewRow record in Grditemspopup.Rows)
            {
                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                if (c1.Checked)
                {
                    Sids = Grditemspopup.DataKeys[record.RowIndex]["id"].ToString();
                    SQty = (record.FindControl("txtqty") as TextBox).Text;
                    Sitem_code = record.Cells[2].Text;
                    Sitem_name = record.Cells[3].Text;
                    Sspec = record.Cells[4].Text;
                    Sdca = record.Cells[5].Text;
                    Ssubdca = record.Cells[6].Text;
                    Sbprice = record.Cells[7].Text;
                    Sunits = record.Cells[8].Text;
                    Amount1 = Convert.ToDecimal(Convert.ToDecimal(record.Cells[7].Text) * Convert.ToDecimal((record.FindControl("txtqty") as TextBox).Text));


                    DataRow dr3 = dt.NewRow();
                    dr3[0] = Sids;
                    dr3[1] = Sitem_code;
                    dr3[2] = Sitem_name;
                    dr3[3] = Sspec;
                    dr3[4] = Sdca;
                    dr3[5] = Ssubdca;
                    dr3[6] = Sbprice;
                    dr3[7] = Sunits;
                    dr3[8] = SQty;
                    dt.Rows.Add(Sids, Sitem_code, Sitem_name, Sspec, Sdca, Ssubdca, Sbprice, Sunits, SQty);
                }
            }
            //da = new SqlDataAdapter("select  substring (indent_no,1,5) as cc_code FROM purchase_details where indent_no='" + ddlindent.SelectedItem.Text + "'", con);
            //da.Fill(ds, "labelcc");
            //lblcc.Text = ds.Tables["labelcc"].ToString();
            ViewState["DCACode"] = Sdca;
            Session["SessionTable2"] = dt;
            popitems.Hide();

            if (ddlindent.SelectedItem.Text != "Select Indent")
            {
                //if (ddlindent.SelectedItem.Text.Split('/')[0] != "CCC")
                //{
                //    lblcc.Text = ddlindent.SelectedItem.Text.Split('/')[0];                   
                //}
                //else
                //{
                lblcc.Text = ddlindent.SelectedItem.Text.Split('/')[0];
                //}

            }
            else if (ddlindentno.SelectedItem.Text != "Select Indent No")
            {
                //if (ddlindentno.SelectedItem.Text.Split('/')[0] != "CCC")
                //    lblcc.Text = ddlindentno.SelectedItem.Text.Split('/')[0];
                //else
                lblcc.Text = ddlindentno.SelectedItem.Text.Split('/')[0];
            }
            lblcc.Text = ddlindentno.SelectedItem.Text.Split('/')[0];
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    } 
  
   
    protected void grdbill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[5].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[5].Visible = false;

        }
    }
    protected void grdbillpo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowIndex = 0;
            e.Row.Cells[1].Visible = false;
            StringBuilder sDetails = new StringBuilder();
            sDetails.Append("<span><h5>Price Details</h5></span>");
            da1 = new SqlDataAdapter("select top 5 Vendor_name,cc_code,isnull(basic_price,0) as basic_price  from [Recieved Items] ri join Purchase_details pd on ri.PO_no=pd.Po_no where Item_code='" + e.Row.Cells[2].Text + "' and basic_price !=0 order by ri.Id desc", con);
            da1.Fill(ds, "data");
            DataTable dt = new DataTable();
            da1.Fill(dt);
            if (dt.Rows.Count > 0)
            {               
                sDetails.Append("<div><p><strong>Vendor Name   ||   CC Code   ||  Amount </strong></p></div>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sDetails.Append("<p>" + dt.Rows[i]["Vendor_name"] +"   ||   " +dt.Rows[i]["cc_code"] +"   ||   "+ dt.Rows[i]["basic_price"].ToString().Replace(".0000",".00") + "</p>");
                    rowIndex++;
                }
                // BIND MOUSE EVENT (TO CALL JAVASCRIPT FUNCTION), WITH EACH ROW OF THE GRID.                  
                    e.Row.Cells[2].Attributes.Add("onmouseover", "MouseEvents(this, event, '" + sDetails.ToString() + "')");
                    e.Row.Cells[2].Attributes.Add("onmouseout", "MouseEvents(this, event, '" +
                    DataBinder.Eval(e.Row.DataItem, "item_code").ToString() + "')");                
            }
          
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Visible = false;

        }
    }
    
    protected void btnyes_Click(object sender, EventArgs e)
    {
        try
        {

            da = new SqlDataAdapter("Select il.id,il.item_code,item_name,specification,dca_code,subdca_code,Replace(basic_price,'.00','')basic_price,units,Replace(il.quantity,'.00','')quantity from((Select item_code,item_name,specification,dca_code,subdca_code,units from item_codes ic join itemcode_creation icc on substring(ic.item_code,1,3)=icc.category_code+icc.majorgroup_code)i right outer join (Select id,item_code,quantity,Basic_Price from indent_list i where  indent_no='" + ddlindentno.SelectedItem.Text + "' AND quantity!=0  )il on i.item_code=il.item_code) ORDER BY id Asc;select LEFT(Remarks, CHARINDEX('$', Remarks) - 1) AS Remarks,RIGHT(Remarks, CHARINDEX('$', REVERSE(Remarks)) - 1) AS SKName,LEFT(Pmremarks, CHARINDEX('$', Pmremarks) - 1) AS Pmremarks,RIGHT(Pmremarks, CHARINDEX('$', REVERSE(Pmremarks)) - 1) AS PMName,LEFT(Cskremarks, CHARINDEX('$', Cskremarks) - 1) AS Cskremarks,RIGHT(Cskremarks, CHARINDEX('$', REVERSE(Cskremarks)) - 1) AS CSKName,LEFT(purmremark, CHARINDEX('$', purmremark) - 1) AS purmremark,RIGHT(purmremark, CHARINDEX('$', REVERSE(purmremark)) - 1) AS purmname,LEFT(cmcremarks, CHARINDEX('$', cmcremarks) - 1) AS cmcremarks,RIGHT(cmcremarks, CHARINDEX('$', REVERSE(cmcremarks)) - 1) AS cmcname,LEFT(Saremarks, CHARINDEX('$', Saremarks) - 1) AS saremarks,RIGHT(Saremarks, CHARINDEX('$', REVERSE(Saremarks)) - 1) AS saname from indents where indent_no='" + ddlindentno.SelectedItem.Text + "'", con);
            da.Fill(ds, "modalpopup");
            Grditemspopup.DataSource = ds.Tables["modalpopup"];
            Grditemspopup.DataBind();
            popitems.Show();
            lblskremark.Visible = true;
            lblpmremark.Visible = true;
            lblcskremark.Visible = true;
            lblpurmremark1.Visible = true;
            lblcmcremark.Visible = true;
            lblsaremark.Visible = true;
            txtindent.Text = ddlindentno.SelectedItem.Text;
            LblSkremarks.Text = ds.Tables["modalpopup1"].Rows[0][0].ToString();
            Lblskname.Text = ds.Tables["modalpopup1"].Rows[0][1].ToString();
            LblPmremarks.Text = ds.Tables["modalpopup1"].Rows[0][2].ToString();
            Lblpmname.Text = ds.Tables["modalpopup1"].Rows[0][3].ToString();
            LblCskremarks.Text = ds.Tables["modalpopup1"].Rows[0][4].ToString();
            Lblcskname.Text = ds.Tables["modalpopup1"].Rows[0][5].ToString();
            Lblpurmremark.Text = ds.Tables["modalpopup1"].Rows[0][6].ToString();
            Lblpurname.Text = ds.Tables["modalpopup1"].Rows[0][7].ToString();
            LdlCmcremarks.Text = ds.Tables["modalpopup1"].Rows[0][8].ToString();
            Lblcmcname.Text = ds.Tables["modalpopup1"].Rows[0][9].ToString();
            LblSaremarks.Text = ds.Tables["modalpopup1"].Rows[0][10].ToString();
            Lblsaname.Text = ds.Tables["modalpopup1"].Rows[0][11].ToString();
            tblbtns.Visible = false;
            trgrid.Visible = true;
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    #region Terms and Conditions start
    protected void BindGridviewterm()
    {
        DataTable dtt = new DataTable();
        dtt.Columns.Add("chkSelectterm", typeof(string));
        dtt.Columns.Add("Descriptionterm", typeof(string));
        DataRow dr = dtt.NewRow();
        dr["chkSelectterm"] = string.Empty;
        dr["Descriptionterm"] = string.Empty;
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
        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "upindent", "javascript:Calculate()", true);
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
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "upindent", "javascript:Calculate()", true);
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
} 
