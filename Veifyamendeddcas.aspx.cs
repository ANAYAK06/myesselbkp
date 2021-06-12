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
public partial class Veifyamendeddcas : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esseldb"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                tr.Visible = false;
                tryear.Visible = false;
             
            }
            Tr1.Visible = false;
            GridView2.Visible = false;
            ////trtype.Visible = false;
            ////pcc.Visible = false;
            ////tryear.Visible = false;
            ////btn.Visible = false;
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
       
        amenddca();

    }
    public void amenddca()
    {
        try
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                //if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                //    //da = new SqlDataAdapter("Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='2' and year='" + ddlyear.SelectedItem.Text + "'", con);
                //    da = new SqlDataAdapter("Select i.*,dt.status from(Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='2' and year='" + ddlyear.SelectedItem.Text + "')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);

          //if (ddlcctype.SelectedItem.Text == "Performing")
                    //da = new SqlDataAdapter("Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='2'", con);
                    da = new SqlDataAdapter("Select DISTINCT i.*,dt.status from(Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit,d.mapdca_code from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and  cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and status='2')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);
            }

            else if (Session["roles"].ToString() == "HoAdmin")
            {
                if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                    //da = new SqlDataAdapter("Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='2' and year='" + ddlyear.SelectedItem.Text + "'", con);
                    da = new SqlDataAdapter("Select DISTINCT i.*,dt.status from(Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit,d.mapdca_code from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='2' and year='" + ddlyear.SelectedItem.Text + "')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);

                else if (ddlcctype.SelectedItem.Text == "Performing")
                    //da = new SqlDataAdapter("Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='2'", con);
                    da = new SqlDataAdapter("Select DISTINCT i.*,dt.status from(Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit,d.mapdca_code from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='2A')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);

            }
            else if (Session["roles"].ToString() == "SuperAdmin")
            {

                if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                    //da = new SqlDataAdapter("Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='3' and year='" + ddlyear.SelectedItem.Text + "'", con);
                    da = new SqlDataAdapter("Select DISTINCT i.*,dt.status from(Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit,d.mapdca_code from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='3' and year='" + ddlyear.SelectedItem.Text + "')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);
                else if (ddlcctype.SelectedItem.Text == "Performing")
                    //da = new SqlDataAdapter("Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='3'", con);
                    da = new SqlDataAdapter("Select DISTINCT i.*,dt.status from(Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit,d.mapdca_code from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='3')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);

            }
            //else if (Session["roles"].ToString() == "Chairman Cum Managing Director")
            //{

            //    if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
            //        //da = new SqlDataAdapter("Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='3' and year='" + ddlyear.SelectedItem.Text + "'", con);
            //        da = new SqlDataAdapter("Select i.*,dt.status from(Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='3A' and year='" + ddlyear.SelectedItem.Text + "')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);
            //    else if (ddlcctype.SelectedItem.Text == "Performing")
            //        //da = new SqlDataAdapter("Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='3'", con);
            //        da = new SqlDataAdapter("Select i.*,dt.status from(Select id,A.dca_code,D.dca_name,Replace(isnull(credit,0),'.0000','.00')credit,Replace(isnull(debit,0),'.0000','.00')debit from Amendbudget_DCA A join DCA D on A.dca_code=d.dca_code where cc_code='" + ddlCCcode.SelectedValue + "' and status='3A')i join dcatype dt ON i.dca_code=dt.dca_code where dt.cc_type IN (SELECT cc_subtype from cost_center where cc_code='" + ddlCCcode.SelectedValue + "')", con);

            //}
            da.Fill(ds, "Amenddca");
            GridView2.DataSource = ds.Tables["Amenddca"];
            GridView2.DataBind();
            if (ds.Tables["Amenddca"].Rows.Count > 0)
            {
                Tr1.Visible = true;
                GridView2.Visible = true;
            }
            else
            {
                Tr1.Visible = false;
                GridView2.Visible = true;
                GridView2.EmptyDataText = "There is no Data";
            }
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        if (ddlcctype.SelectedItem.Text == "Performing")
        {
            ////lblyear.Visible = false;
            ////ddlyear.Visible = false;

        }
        else
        {
            ////lblyear.Visible = true;
            ////ddlyear.Visible = true;

        }
        try
        {
            string dcas = "";
            string Amounts = "";
            string ids = "";
            string Amendtypes = "";
            foreach (GridViewRow record in GridView2.Rows)
            {

                CheckBox c1 = (CheckBox)record.FindControl("chkSelect");

                if (c1.Checked)
                {
                    ids = ids + GridView2.DataKeys[record.RowIndex]["id"].ToString() + ",";
                    dcas = dcas + record.Cells[1].Text + ",";
                    if (record.Cells[3].Text != "0.00")
                    {
                        Amendtypes = Amendtypes + "Credit,";
                        Amounts = Amounts + record.Cells[3].Text + ",";
                        Add = Add + Convert.ToDecimal(record.Cells[3].Text);
                    }
                    else if (record.Cells[4].Text != "0.00")
                    {
                        Subtract = Subtract + Convert.ToDecimal(record.Cells[4].Text);
                        Amounts = Amounts + record.Cells[4].Text + ",";
                        Amendtypes = Amendtypes + "Debit,";
                    }
                }
            }
            cmd1 = new SqlCommand("DcaAmendedStatusCheck_sp", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@ids", ids);
            cmd1.Parameters.AddWithValue("@DCACodes", dcas);
            cmd1.Parameters.AddWithValue("@roles", Session["roles"].ToString());
            cmd1.Connection = con;
            con.Open();
            string msg1 = cmd1.ExecuteScalar().ToString();
            con.Close();
            if (msg1 == "Sucessfull")
            {

                cmd = new SqlCommand("sp_Amend_DcaBudget", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CCCode", ddlCCcode.SelectedValue);
                cmd.Parameters.AddWithValue("@DCACodes", dcas);
                if (ddlcctype.SelectedItem.Text != "Performing")
                {
                    if (Session["roles"].ToString() != "Project Manager")
                        cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedItem.Text);
                }
                cmd.Parameters.AddWithValue("@Amounts", Amounts);
                cmd.Parameters.AddWithValue("@AmendTypes", Amendtypes);
                cmd.Parameters.AddWithValue("@Add", Add);
                cmd.Parameters.AddWithValue("@Subtract", Subtract);
                cmd.Parameters.AddWithValue("@ids", ids);
                cmd.Parameters.AddWithValue("@User", Session["user"].ToString());
                if (Session["roles"].ToString() == "Project Manager")
                {
                    cmd.Parameters.AddWithValue("@CCType", "Performing");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CCType", ddlcctype.SelectedItem.Text);
                }
                cmd.Parameters.AddWithValue("@roles", Session["roles"].ToString());
                cmd.Connection = con;
                con.Open();

                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                ////string msg = "Verified";
                if (msg == "Verified")
                {
                    JavaScript.UPAlert(Page, msg);

                }
                else
                {
                    JavaScript.UPAlert(Page, msg);
                }

                amenddca();
                type();
                LoadYear();
                cctype();
                Tr1.Visible = false;
                GridView2.Visible = false;
            }
            else
            {
                JavaScript.UPAlert(Page, msg1);
                amenddca();
            }


        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlcctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        cctype();
    }

    public void cctype()
    {
        try
        {
            //ddltype.SelectedIndex = 0;
            if (Session["roles"].ToString() == "Project Manager")
            {
                ddlcctype.DataTextField = "Performing";
                trtype.Visible = true;
                tryear.Visible = false;
            }
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                //lblyear.Visible = false;
                //ddlyear.Visible = false;
                trtype.Visible = true;
                ////tryear.Visible = false;
                ////pcc.Visible = false;
                tryear.Visible = false;
                ////btn.Visible = false;
            }
            else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
            {
                if (Session["roles"].ToString() == "HoAdmin")
                {
                    da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='2' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New')", con);
                }
                else
                {
                    if (Session["roles"].ToString() == "SuperAdmin")
                    {
                        if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
                            da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New')", con);
                        else if (ddlcctype.SelectedItem.Text == "Performing")
                            da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New')", con);
                    }
                    else
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='1' and c.CC_type='" + ddlcctype.SelectedItem.Text + "'  and c.status in ('Old','New')", con);
                    }

                }
                da.Fill(ds, "SERVICECC");
                ddlCCcode.DataTextField = "Name";
                ddlCCcode.DataValueField = "cc_code";
                ddlCCcode.DataSource = ds.Tables["SERVICECC"];
                ddlCCcode.DataBind();
                ddlCCcode.Items.Insert(0, "Select Cost Center");
                trtype.Visible = false;
                tryear.Visible = true;
                ////pcc.Visible = true;
                ////btn.Visible = false;
                Tr1.Visible = false;
                GridView2.Visible = false;

            }
            //lblyear.Visible = true;
            //ddlyear.Visible = true;
            else if (ddlcctype.SelectedItem.Text == "Select")
            {
                trtype.Visible = false;
                ////pcc.Visible = false;
                tryear.Visible = false;
                ////btn.Visible = false;
                Tr1.Visible = false;
                GridView2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Veifyamendeddcas.aspx");
    }
    private decimal Add = (decimal)0.0;
    private decimal Subtract = (decimal)0.0;
    
 
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            cmd.Connection = con;
            cmd = new SqlCommand("delete from [Amendbudget_DCA] where id='" + GridView2.DataKeys[e.RowIndex]["id"].ToString() + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i == 1)
                JavaScript.UPAlert(Page, "Deleted");
            else
                JavaScript.UPAlert(Page, "Failed");
            con.Close();
            amenddca();

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
  
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Credit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "credit"));
                Debit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = String.Format(" {0:#,##,##,###.00}", Credit);
                e.Row.Cells[4].Text = String.Format(" {0:#,##,##,###.00}", Debit);

            }
           
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    private decimal Credit = (decimal)0.0;
    private decimal Debit = (decimal)0.0;
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        type();

    }

    public void type()
    {
        try
        {
            if (ddltype.SelectedItem.Text != "Select")
            {
                if (Session["roles"].ToString() == "Project Manager")
                {
                    if (ddltype.SelectedItem.Text == "Service")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where c.cc_code in (Select cc_code from cc_user where user_name='" + Session["user"].ToString() + "') and y.Status='2' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Service'", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Trading")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='2' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Trading'", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Manufacturing")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='2' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Manufacturing'", con);
                    }
                    da.Fill(ds, "SERVICECC");
                    ddlCCcode.DataTextField = "Name";
                    ddlCCcode.DataValueField = "cc_code";
                    ddlCCcode.DataSource = ds.Tables["SERVICECC"];
                    ddlCCcode.DataBind();
                    ddlCCcode.Items.Insert(0, "Select Cost Center");
                    ////pcc.Visible = true;
                    ////tryear.Visible = false;
                    ////btn.Visible = false; ;
                    Tr1.Visible = false;
                    GridView2.Visible = false;

                }
                else if (Session["roles"].ToString() == "HoAdmin")
                {
                    if (ddltype.SelectedItem.Text == "Service")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='2A' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Service'", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Trading")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='2A' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Trading'", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Manufacturing")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='2A' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Manufacturing'", con);
                    }
                    da.Fill(ds, "SERVICECC");
                    ddlCCcode.DataTextField = "Name";
                    ddlCCcode.DataValueField = "cc_code";
                    ddlCCcode.DataSource = ds.Tables["SERVICECC"];
                    ddlCCcode.DataBind();
                    ddlCCcode.Items.Insert(0, "Select Cost Center");
                    ////pcc.Visible = true;
                    ////tryear.Visible = false;
                    ////btn.Visible = false; ;
                    Tr1.Visible = false;
                    GridView2.Visible = false;

                }
                else if (Session["roles"].ToString() == "SuperAdmin")
                {
                    if (ddltype.SelectedItem.Text == "Service")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Service'", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Trading")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Trading'", con);
                    }
                    else if (ddltype.SelectedItem.Text == "Manufacturing")
                    {
                        da = new SqlDataAdapter("select distinct c.cc_code,c.cc_code+' , '+c.cc_name+'' Name from Cost_Center c join Amendbudget_DCA y on c.cc_code=y.cc_code where y.Status='3' and c.CC_type='Performing'  and c.status in ('Old','New') and cc_subtype='Manufacturing'", con);
                    }
                    da.Fill(ds, "SERVICECC");
                    ddlCCcode.DataTextField = "Name";
                    ddlCCcode.DataValueField = "cc_code";
                    ddlCCcode.DataSource = ds.Tables["SERVICECC"];
                    ddlCCcode.DataBind();
                    ddlCCcode.Items.Insert(0, "Select Cost Center");
                    ////pcc.Visible = true;
                    ////tryear.Visible = false;
                    ////btn.Visible = false;
                    Tr1.Visible = false;
                    GridView2.Visible = false;

                }
               
            }
            else if (ddltype.SelectedItem.Text == "Select")
            {
                ////tryear.Visible = false;
                ////pcc.Visible = false;
                ////tryear.Visible = false;
                ////btn.Visible = false;
                Tr1.Visible = false;
                GridView2.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }
    }
    protected void ddlCCcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCCcode.SelectedItem.Text != "Select Cost Center")
        {
            if (Session["roles"].ToString() == "Project Manager")
            {
                Tr1.Visible = false;
                GridView2.Visible = false;
            }
            if (ddlcctype.SelectedItem.Text == "Performing")
            {
                ////btn.Visible = true;
                ////tryear.Visible = false;
                Tr1.Visible = false;
                GridView2.Visible = false;
            }
            else if (ddlcctype.SelectedItem.Text == "Non-Performing" || ddlcctype.SelectedItem.Text == "Capital")
            {
                ////btn.Visible = true;
                ////tryear.Visible = true;
                Tr1.Visible = false;
                GridView2.Visible = false;
                LoadYear();
            }
        }
        else
        {
            ////tryear.Visible = false;
            ////btn.Visible = false;
            Tr1.Visible = false;
            GridView2.Visible = false;
        }
       
    }
    public void LoadYear()
    {
        try
        {
            ddlyear.Items.Clear();
            da = new SqlDataAdapter("Select distinct year from Amendbudget_DCA where cc_code='" + ddlCCcode.SelectedValue + "' order by year asc", con);
            da.Fill(ds, "year");
            if (ds.Tables["year"].Rows.Count > 0)
            {
                btn.Visible = true;
            }
            ddlyear.DataTextField = "year";
            ddlyear.DataValueField = "year";
            ddlyear.DataSource = ds.Tables["year"];
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, "Select Year");
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
        }

    }
   
}

   
