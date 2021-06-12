using System;
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
public partial class CCAccrued : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Tools");
        lbl.Attributes.Add("class", "active");
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView3.EditIndex = e.NewEditIndex;

        BindGrid();
    }
    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView3.EditIndex = -1;

        BindGrid();
    }
    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (ReturnMenuDetails().Tables[0].Rows.Count > 0)
        {
            TextBox OverHead = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtohead");
            TextBox Depreciation = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtdep");

            cmd.CommandText = "Update [CC_AccruedValues] set Interest_Rate='" + OverHead.Text + "',CC_AccruedValue='" + Depreciation.Text + "',modified_by='"+Session["user"].ToString()+"',modified_date=getdate() where id='" + GridView3.DataKeys[e.RowIndex]["id"].ToString() + "'";
            cmd.Connection = con;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
                JavaScript.UPAlert(Page, "Updated Successfull");
            else
                JavaScript.UPAlert(Page, "Updating Failed");
            GridView3.EditIndex = -1;
            BindGrid();
        }
        else
        {
            GridView3.EditIndex = -1;
            BindGrid();
            JavaScript.UPAlert(Page, "Update is not possible");

        }
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //if (GridView3.Rows.Count != 1)
        //{
        DropDownList ddlcccode = (DropDownList)GridView3.Rows[e.RowIndex].FindControl("ddlCCcode");
        cmd.CommandText = "delete from [CC_AccruedValues] where id='" + GridView3.DataKeys[e.RowIndex]["id"].ToString() + "'";
        cmd.Connection = con;
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        if (i == 1)
            JavaScript.UPAlert(Page, "Deleted Successfull");
        else
            JavaScript.UPAlert(Page, "Deleting Failed");
        BindGrid();
        //}
        //else
        //{
        //    JavaScript.UPAlert(Page, "You are not able to delete");
        //}
    }
    public DataSet ReturnMenuDetails()
    {

        DataSet ds = new DataSet();
        using (SqlConnection sqlconn = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]))
        {
            SqlDataAdapter sqldap = new
            SqlDataAdapter("Select u.id,u.cc_code,cc_name,Interest_Rate,CC_AccruedValue from [CC_AccruedValues] u join cost_center c on u.cc_code=c.cc_code GROUP by u.cc_code,cc_name,Interest_Rate,CC_AccruedValue,u.id ORDER BY CASE WHEN u.cc_code='CCC' THEN u.cc_code end ASC ,CASE WHEN u.cc_code!='CCC' THEN CONVERT(INT,REPLACE(u.CC_Code,'CC-','')) END ASC", sqlconn);
            sqldap.Fill(ds);
        }
        return ds;
    }
    public DataTable ReturnEmptyDataTable()
    {
        DataTable dtMenu = new DataTable();
        DataColumn dcMenuID = new DataColumn("id", typeof(System.Int32));
        dtMenu.Columns.Add(dcMenuID);
        DataColumn dccode = new DataColumn("cc_code", typeof(System.String));
        dtMenu.Columns.Add(dccode);
        DataColumn dcMenuName = new DataColumn("cc_name", typeof(System.String));
        dtMenu.Columns.Add(dcMenuName);
        DataColumn dcoverhead = new DataColumn("Interest_Rate", typeof(System.String));
        dtMenu.Columns.Add(dcoverhead);
        DataColumn dcdepreciationvalue = new DataColumn("CC_AccruedValue", typeof(System.String));
        dtMenu.Columns.Add(dcdepreciationvalue);
        DataRow datatRow = dtMenu.NewRow();
        dtMenu.Rows.Add(datatRow);
        return dtMenu;
    }
    public void BindGrid()
    {

        if (ReturnMenuDetails().Tables[0].Rows.Count > 0)
        {
            GridView3.DataSource = ReturnMenuDetails();
        }
        else
        {
            GridView3.DataSource = ReturnEmptyDataTable();
        }
        GridView3.DataBind();
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        DropDownList ddlfcccode = (DropDownList)GridView3.FooterRow.FindControl("ddlfooterCCcode");
        TextBox txtoverhead = (TextBox)GridView3.FooterRow.FindControl("txtoverhead");
        TextBox txtdep = (TextBox)GridView3.FooterRow.FindControl("txtdepreciation");
        da = new SqlDataAdapter("SELECT 1 as IsExists FROM [CC_AccruedValues] where cc_code='" + ddlfcccode.SelectedItem.Text + "'", con);
        da.Fill(ds, "checkCC");
        if (ds.Tables["checkCC"].Rows.Count == 0)
        {           
            cmd.CommandText = "Insert Into [CC_AccruedValues](cc_code,Interest_Rate,CC_AccruedValue,created_by,created_date)values(@CCCode,@interest,@accvalue,@User,getdate())";
            cmd.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlfcccode.SelectedItem.Text;
            cmd.Parameters.AddWithValue("@interest", SqlDbType.VarChar).Value = txtoverhead.Text;
            cmd.Parameters.AddWithValue("@accvalue", SqlDbType.VarChar).Value = txtdep.Text;
            cmd.Parameters.AddWithValue("@User", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Connection = con;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
                JavaScript.UPAlert(Page, "Successfull");
            else
            {
                JavaScript.UPAlert(Page, "Fail");
            }
            GridView3.EditIndex = -1;
            BindGrid();
        }
        else
        {
            JavaScript.UPAlert(Page, "Cost Center already Exists");

        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (GridView3.EditIndex == e.Row.RowIndex)
            {
                RequiredFieldValidator reqfldVal1 = new RequiredFieldValidator();
                reqfldVal1.ID = "RequiredValidator2";
                reqfldVal1.ControlToValidate = "txtohead";
                reqfldVal1.ErrorMessage = "Interest rate  Required";
                reqfldVal1.SetFocusOnError = true;
                e.Row.Cells[3].Controls.Add(reqfldVal1);

                RequiredFieldValidator reqfldVal2 = new RequiredFieldValidator();
                reqfldVal2.ID = "RequiredValidator3";
                reqfldVal2.ControlToValidate = "txtdep";
                reqfldVal2.ErrorMessage = "Accrued Value Required";
                reqfldVal2.SetFocusOnError = true;
                e.Row.Cells[4].Controls.Add(reqfldVal2);

            }
        }
    }

}
