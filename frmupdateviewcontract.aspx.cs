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

public partial class frmupdateviewcontract : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");

        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");

       esselDal RoleCheck = new esselDal();
        int rec = 0;
         rec = RoleCheck.RoleCheck(Session["roles"].ToString(), 27);
        if (rec == 0)
            Response.Redirect("Menucontents.aspx");
    }
    protected void ddlpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = "";

            da = new SqlDataAdapter("select project_name,client_name,division,c.cc_code,start_date,c.end_date,c.natureofjob,c.projectmanager_name,projectmanager_contactno,customer_name,po_basicvalue,po_servicetaxvalue,po_totalvalue,amdbasic,amdtax,amdtotal,po.po_no,po_basicvalue+isnull(amdbasic,0) as totalbasic,po_servicetaxvalue+isnull(amdtax,0) as totaltax,po_totalvalue+isnull(amdtotal,0) as  total from contract c join po on c.po_no=po.po_no left outer join (select Sum(po_amendedbasicvalue)as amdbasic ,Sum(po_amendedservicetaxvalue)as amdtax ,Sum(po_amendedtotalvalue)as amdtotal,po_no from po_amended GROUP BY po_no) amd on po.po_no=amd.po_no where po.po_no='" + ddlpo.SelectedValue + "'", con);
            da.Fill(ds, "vcontract");
            if (ds.Tables["vcontract"].Rows.Count >= 1)
            {

                txtpname.Text = ds.Tables["vcontract"].Rows[0].ItemArray[0].ToString();
                txtclientname.Text = ds.Tables["vcontract"].Rows[0].ItemArray[1].ToString();
                txtdivision.Text = ds.Tables["vcontract"].Rows[0].ItemArray[2].ToString();
                txtcc.Text = ds.Tables["vcontract"].Rows[0].ItemArray[3].ToString();
                txtstartdate.Text = ds.Tables["vcontract"].Rows[0].ItemArray[4].ToString();
                txtenddate.Text = ds.Tables["vcontract"].Rows[0].ItemArray[5].ToString();
                txtnoj.Text = ds.Tables["vcontract"].Rows[0].ItemArray[6].ToString();
                txtpmname.Text = ds.Tables["vcontract"].Rows[0].ItemArray[7].ToString();
                txtpmcno.Text = ds.Tables["vcontract"].Rows[0].ItemArray[8].ToString();
                txtcustname.Text = ds.Tables["vcontract"].Rows[0].ItemArray[9].ToString();
                //txtpobasicvalue.Text = ds.Tables["vcontract"].Rows[0].ItemArray[10].ToString().Replace(".0000", ".00");
                //txtposervicetax.Text = ds.Tables["vcontract"].Rows[0].ItemArray[11].ToString().Replace(".0000", ".00");
                //txtpototal.Text = ds.Tables["vcontract"].Rows[0].ItemArray[12].ToString().Replace(".0000", ".00");
               
            }
          
            
        }

        catch (Exception ex)
        {

            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            string str1 = "";
            string str2 = "";
            string str3 = "";
            str1 = str1 + "update contract set project_name='" + txtpname.Text + "',client_name='" + txtclientname.Text + "',division='" + txtdivision.Text + "',";
            str1 = str1 + "cc_code='"+txtcc.Text+"',start_date='" + txtstartdate.Text + "',end_date='" + txtenddate.Text + "',natureofjob='" + txtnoj.Text + "',projectmanager_name='" + txtpmname.Text + "',";
            str1 = str1 + "projectmanager_contactno ='" + txtpmcno.Text + "',customer_name='" + txtcustname.Text + "' where po_no='" + ddlpo.SelectedValue + "'";
           // str2 = str2 + "update po set po_basicvalue='" + txtpobasicvalue.Text + "',po_servicetaxvalue='" + txtposervicetax.Text + "',po_totalvalue='" + txtpototal.Text + "'where po_no='" + ddlpo.SelectedValue + "'";
            cmd.CommandText = str1 + str2;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            bool j = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (j == true)
            {
                showalert("Update Contract Sucessfully");
            }
            else
            {
                showalert("Update Contract Failed");
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
        myalertlabel.Text = "<script language='javascript'>window.alert('" + message + "')</script>";
        Page.Controls.Add(myalertlabel);
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        CascadingDropDown3.SelectedValue = "";
        txtpname.Text = "";
        txtclientname.Text = "";
        txtdivision.Text = "";
        txtstartdate.Text = "";
        txtenddate.Text = "";
        txtnoj.Text = "";
        txtpmname.Text = "";
        txtpmcno.Text = "";
        txtcustname.Text = "";
        //txtpobasicvalue.Text = "";
        //txtposervicetax.Text = "";
        //txtpototal.Text = "";
        txtcc.Text = "";
    }
}
