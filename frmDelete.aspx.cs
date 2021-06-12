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

public partial class Admin_frmDelete : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 40);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
        if (!IsPostBack)
        {
            LoadYear();
            
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
    public void fillgrid()
    {
        try
        {
            da = new SqlDataAdapter("Select i.[InvoiceNo],[Invoice_Date],i.[CC_Code],CAST(BasicValue AS Decimal(10,2))[BasicValue],CAST(NetServiceTax AS Decimal(10,2))as [servicetax],CAST(NetEdcess AS Decimal(10,2))as[Edcess],CAST(NetHedcess AS Decimal(10,2))as[Hedcess],CAST(tds AS Decimal(10,2))as [tds] ,CAST(retention AS Decimal(10,2))as[retention],CAST(hold AS Decimal(10,2))as[hold],cast(amount as decimal(10,2))as [Net Amount],[bank_name][Bank],[Date] from bankbook b join invoice i on b.invoiceno=i.invoiceno where i.po_no='" + ddlpo.SelectedValue + "'", con);
            da.Fill(ds, "onlycc");
            mgrid.DataSource = ds.Tables["onlycc"];
            mgrid.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    
    }
    public void showalert(string message)
    {
        string script = "alert(\"" + message + "\");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
       mgrid.Visible = true;
                string invoicenos = "";
                foreach (GridViewRow rec in mgrid.Rows)
                {
                    if ((rec.FindControl("chkSelect") as CheckBox).Checked)
                        invoicenos = invoicenos + mgrid.DataKeys[rec.RowIndex]["InvoiceNo"].ToString() + ",";
                }
                if (invoicenos != "")
                {
                    cmd = new SqlCommand("sp_BankAmount_RollBack", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@invoicenos", invoicenos);
                    con.Open();
                    showalert(cmd.ExecuteScalar().ToString());
                    con.Close();
                }

        
        fillgrid();

    }



    protected void mgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        mgrid.PageIndex = e.NewPageIndex;

        fillgrid();
    }

}
