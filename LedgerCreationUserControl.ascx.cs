using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Collections;

using System.Data;
using System.Linq;

using System.Web.Security;

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
 
public partial class LedgerCreationUserControl : System.Web.UI.UserControl
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    public event EventHandler evt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGroups();
        }
       // evt(this, e);

    }
    public void FillGroups()
    {
        try
        {
            da = new SqlDataAdapter("Select i.* from (Select  CONVERT(varchar(10), id)as id,Name from Sub_Group where status='3'union all select Group_id as id,group_name as Name from mastergroups where status='3')i order by i.Name asc", con);
            da.Fill(ds, "Subgroups");
            ddlsubgroup.DataTextField = "Name";
            ddlsubgroup.DataValueField = "id";
            ddlsubgroup.DataSource = ds.Tables["Subgroups"];
            ddlsubgroup.DataBind();
            ddlsubgroup.Items.Insert(0, "Select Sub-Groups");
        }
        catch (Exception ex)
        {

        }
    }
    //public class LedgerControls : LedgerCreationUserControl
    //{
    //    public string LedgerName
    //    {
    //        get { return txtledgername.Text; }
    //        set { txtledgername.Text = value; }
    //    }
    //    public string LedgerName
    //    {
    //        get { return ddlsubgroup.SelectedValue; }
    //        set { ddlsubgroup.SelectedValue = value; }
    //    }
    //    public string BalDate
    //    {
    //        get { return txtledbaldate.Text; }
    //        set { txtledbaldate.Text = value; }
    //    }
    //    public string OpeningBalance
    //    {
    //        get { return txtopeningbal.Text; }
    //        set { txtopeningbal.Text = value; }
    //    }
    //    public string Rbtntype
    //    {
    //        get { return rbtnpaymenttype.SelectedValue; }
    //        set { rbtnpaymenttype.SelectedValue = value; }
    //    }
    //}
}