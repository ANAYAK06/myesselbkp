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
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;
public partial class AssetDepreciation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    SqlDataAdapter da = null;
    SqlDataAdapter da1 = null;
    DataView dv;
    //DataRow dr;
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

        if (!IsPostBack)
        {
            LoadYear();
            trexcel.Visible = false;
        }
    }
  
    public void LoadYear()
    {
        for (int i = 2012; i <= System.DateTime.Now.Year; i++)
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            da = new SqlDataAdapter("Asset_depreciation", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.DateTime).Value = ddlcccode.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@Fyyear", SqlDbType.VarChar).Value = ddlyear.SelectedItem.Text;           
            da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = "1";
            da.Fill(ds, "fill");
            if (ds.Tables["fill"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["fill"];
                GridView1.DataBind();
                trexcel.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                trexcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AssetDepreciation.aspx", false);
    }
    public void getdep()
    {
        try
        {
            da = new SqlDataAdapter("AssetDepreciation", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.DateTime).Value = ddlcccode.SelectedValue;
            da.SelectCommand.Parameters.AddWithValue("@year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            da.Fill(ds, "fill1");           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CurrentDep += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DEPRECIATION VALUE FOR CURRENT FY"));
                PrevDep += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DEPRECIATION VALUE FOR PREVIOUS FY"));
                CuurentTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cudepvalue"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[6].Text = CurrentDep.ToString();
                e.Row.Cells[7].Text = PrevDep.ToString();
                if (ddlcccode.SelectedValue == "CC-38")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(453554.5) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(453554.5) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-41")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(434282) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(434282) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-42")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(906643.5) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(906643.5) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-43")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(801335) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(801335) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-44")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(203105) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(203105) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-45")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(63264.5) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(63264.5) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-49")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(519636.5) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(519636.5) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-50")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(97872) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(97872) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-51")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(241825) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(241825) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-52")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(58314.5) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(58314.5) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-53")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(92345.5) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(92345.5) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-54")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(257680.209) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(257680.209) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-57")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(46175) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(46175) + PrevDep).ToString();

                }
                else if (ddlcccode.SelectedValue == "CC-58")
                {
                    e.Row.Cells[7].Text = Convert.ToDecimal(Convert.ToDecimal(51685.147) + PrevDep).ToString();
                    e.Row.Cells[8].Text = Convert.ToDecimal(CurrentDep + Convert.ToDecimal(51685.147) + PrevDep).ToString();

                }
                else
                {
                    e.Row.Cells[7].Text = PrevDep.ToString();
                    e.Row.Cells[8].Text = CuurentTotal.ToString();

                }


            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    private decimal CurrentDep = (decimal)0.0;
    private decimal PrevDep = (decimal)0.0;
    private decimal CuurentTotal = (decimal)0.0;
    private decimal CumulativeTotal = (decimal)0.0;
    private decimal UptoPrevTotal = (decimal)0.0;

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Asset Depreciation"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GridView1.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
    //public void PrevDep()
    //{
    //    da = new SqlDataAdapter("Select ISNULL(sum(INCF),0) as[PrevInterest] from Accrued_Interest where cc_code='" + ddlcccode.SelectedValue + "' and convert(datetime,date) < '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "'", con);
    //    da.Fill(ds, "PrevAccDep");

    //}
}
