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



public partial class AssetsTransfer : System.Web.UI.Page
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
       
            if (!IsPostBack)
            {
                tdmaingrid.Visible = false;
                trsubgrids1.Visible = false;
                trsubgrids2.Visible = false;
                trdescription.Visible = false;
                trbtnvisible.Visible = false;
                addbtn.Visible = false;
                ignore();
            }
       
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            tdmaingrid.Visible = true;
            da = new SqlDataAdapter("select item_code,quantity from indent_list where Indent_no='" + ddlindentno.SelectedItem.Text + "'", con);
            da.Fill(ds, "fillmaingrid");
            GridView1.DataSource = ds.Tables["fillmaingrid"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    public void ignore()
    {
        try
        {
            string  s2;
            s2 = "delete from [Items Transfer] where  Ref_no is null";
            cmd.Connection = con;
            cmd.CommandText = s2;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void btnignore_Click(object sender, EventArgs e)
    {
        try
        {
            string s1, s2;
            s1 = "update indents set status='2' where indent_no='" + ddlindentno.SelectedItem.Text + "'";
            s2 = "delete from [Items Transfer] where  Ref_no is null";
            cmd.Connection = con;
            cmd.CommandText = s1 + s2;
            con.Open();
            int j = cmd.ExecuteNonQuery();
            con.Close();
            if (j == 1)
            {
                JavaScript.UPAlertRedirect(Page, "Sucessfully Ignore", "AssetsTransfer.aspx");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        tdcccode.Visible = true;
        tdcccode1.Visible = true;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strScript = "uncheckOthers(" + ((CheckBox)e.Row.Cells[0].FindControl("CheckBox1")).ClientID + ");";
            ((CheckBox)e.Row.Cells[0].FindControl("CheckBox1")).Attributes.Add("onclick", strScript);
        }

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        addbtn.Visible = true;
        tdmaingrid.Visible = true;
        trsubgrids1.Visible = true;
        ds.Reset();
        foreach (GridViewRow gv in GridView1.Rows)
        {

            CheckBox chk = (CheckBox)gv.FindControl("CheckBox1");
            if (chk != null && chk.Checked)
            {
                ViewState["view"] = gv.Cells[1].Text;
                secondgrid();
                filldata();
            }
           
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            string itemcode = "";
            
            foreach (GridViewRow record in grdaddassets.Rows)
            {
                CheckBox chk = (CheckBox)record.FindControl("chkgrd2");
                if (chk.Checked)
                {
                    itemcode = grdaddassets.DataKeys[record.RowIndex]["item_code"].ToString();
                }
            }
            cmd.Connection = con;
            cmd.CommandText = "Insert into [Items Transfer](item_code,quantity,type)values(@itemcode,@quantity,@Type)";
            cmd.Parameters.Add("@itemcode", SqlDbType.VarChar).Value = itemcode;
            cmd.Parameters.Add("@quantity", SqlDbType.Float).Value = 1;
            if (ViewState["type"].ToString() == "Assets Issue")
                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = "1";
            else
                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = "2";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            secondgrid();
            filldata();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
  

    public void filldata()
    {
        try
        {
            trsubgrids2.Visible = true;
            da = new SqlDataAdapter("select id,item_code from [Items Transfer] where Ref_no is null", con);
            da.Fill(ds, "data");
            grdaddedassets.DataSource = ds.Tables["data"];
            grdaddedassets.DataBind();
            if (ds.Tables["data"].Rows.Count > 0)
            {
                trdescription.Visible = true;
                trbtnvisible.Visible = true;

            }
            else {
                trdescription.Visible = false;
                trbtnvisible.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void secondgrid()
    {
        try
        {
            ////da = new SqlDataAdapter("select  item_code from Master_data where CC_code='" + ddlcccode.SelectedItem.Text + "' and Item_code not in (select Item_code from [Items Transfer] where Ref_no is null )and SUBSTRING(item_code,1,8)='" + ViewState["view"].ToString() + "'", con);
            da = new SqlDataAdapter("Select item_code from master_data where substring(item_code,1,8)='" + ViewState["view"].ToString() + "' and cc_code='" + ddlcccode.SelectedValue + "' and status='Active'", con);
            da.Fill(ds, "ItemInfo");
            if (ds.Tables["ItemInfo"].Rows.Count > 0)
            {
                grdaddassets.DataSource = ds.Tables["ItemInfo"];
                grdaddassets.DataBind();
                
            }
            else
            {
                grdaddassets.DataSource = null;
                grdaddassets.EmptyDataText = "No Items Found";
                grdaddassets.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnremove_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (GridViewRow gv in grdaddedassets.Rows)
            {
                 CheckBox chk = (CheckBox)gv.FindControl("chkgrd3");
                 if (chk.Checked)
                 {
                     string id = grdaddedassets.DataKeys[gv.RowIndex]["id"].ToString();
                     cmd.Connection = con;
                     cmd.CommandText = "delete from [Items Transfer] where   id='" + id + "'";
                     con.Open();
                     cmd.ExecuteNonQuery();
                     con.Close();
                 }
            }
            filldata();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = "";
            foreach (GridViewRow record in grdaddedassets.Rows)
            {

                CheckBox chk = (CheckBox)record.FindControl("chkgrd3");
                if (chk.Checked)
                {
                    ids = ids + grdaddedassets.DataKeys[record.RowIndex]["id"].ToString() + ",";
                }

            }
            if (ids != null)
            {
                cmd = new SqlCommand("UpdateIndent_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@Indent_No", ddlindentno.SelectedItem.Text);
                if (ViewState["type"].ToString() == "Assets Issue")
                {
                    cmd.Parameters.AddWithValue("@CCCode", "CC-33");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CCCode", ddlcccode.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@date", txtdate.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtdesc.Text);
                cmd.Parameters.AddWithValue("@Days", ddlDays.SelectedValue);
                cmd.Parameters.AddWithValue("@user", Session["user"].ToString());
                cmd.Parameters.AddWithValue("@IndentType", ViewState["type"].ToString());
                cmd.Parameters.AddWithValue("@TotalQty", "1");
                cmd.Connection = con;
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg == "Sucessfull")
                    JavaScript.UPAlertRedirect(Page, msg, "AssetsTransfer.aspx");
                else
                    JavaScript.UPAlert(Page, msg);
                
              
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    
    protected void ddlindentno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("select indenttype,cc_code from indents where Indent_no='" + ddlindentno.SelectedItem.Text + "'", con);
            da.Fill(ds, "type");
            ViewState["type"] = ds.Tables["type"].Rows[0].ItemArray[0].ToString();
            if (ds.Tables["type"].Rows[0].ItemArray[0].ToString() == "Assets Issue")
            {
                ddlcccode.Items.Insert(0, "Select Cost Center");
                ddlcccode.Items.Insert(1, "CC-33");
            }
            else
            {
                da = new SqlDataAdapter("Select cc_code ,(cc_code + ', ' + cc_name) as cc_name  from cost_center where cc_code not in ('" + ds.Tables["type"].Rows[0].ItemArray[1].ToString() + "','CC-33')", con);
                da.Fill(ds, "fill");
                ddlcccode.DataTextField = "cc_name";
                ddlcccode.DataValueField = "cc_code";
                ddlcccode.DataSource = ds.Tables["fill"];
                ddlcccode.DataBind();
                ddlcccode.Items.Insert(0, "Select Cost Center");
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.UPAlert(Page,ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
}
