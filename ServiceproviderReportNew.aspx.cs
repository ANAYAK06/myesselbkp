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
using System.Drawing;

public partial class ServiceproviderReportNew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataSet dsn = new DataSet();
    SqlDataAdapter sdan = new SqlDataAdapter();
    int i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Purchase");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("SessionExpire.aspx");
        if (!IsPostBack)
        {
            LoadYear();
            btnSearch.Visible = false;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string condition = "";
            if (ddlyear.SelectedIndex != 0)
            {
                if (ddlmonth.SelectedIndex != 0)
                {
                    string yy = (ddlmonth.SelectedIndex < 4) ? (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() : ddlyear.SelectedValue;
                    condition = condition + " and datepart(mm,po_date)=" + ddlmonth.SelectedValue + " and datepart(yy,po_date)=" + yy;

                }
                else
                {
                    condition = condition + " and convert(datetime,po_date) between '04/01/" + (Convert.ToInt32(ddlyear.SelectedValue)).ToString() + "' and '03/31/" + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "'";
                }

            }

            if (ddlvendor.SelectedItem.Text != "Select All")
            {
                condition = condition + " and vendor_id='" + ddlvendor.SelectedValue + "'";
            }
            if (ddldca.SelectedItem.Text != "Select DCA")
            {
                condition = condition + " and dca_code='" + ddldca.SelectedValue + "'";
            }
            ViewState["Query1"] = condition;
            fillgrid();

        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }


    }
    public void fillgrid()
    {
        try
        {
            if (ddlcccode.SelectedItem.Text != "Select All")
            {
                if (Ddltype.SelectedItem.Value == "1")
                    da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id  where sp.status in('3','Closed','Closed1','Closed2','Closed3') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono )  where cc_code='" + ddlcccode.SelectedValue + "' " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);
                else if (Ddltype.SelectedItem.Value == "2")
                    da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id where sp.status in('3','Closed','Closed1','Closed2') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono )  where cc_code='" + ddlcccode.SelectedValue + "' " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);
                else if (Ddltype.SelectedItem.Value == "3")
                    da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id  where sp.status in('Closed3') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono )  where cc_code='" + ddlcccode.SelectedValue + "' " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);
            }
            else if (ddlcccode.SelectedItem.Text == "Select All")
            {
                if (Session["roles"].ToString() != "Project Manager")
                {
                    if (Ddltype.SelectedItem.Value == "1")
                        da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id  where sp.status in('3','Closed','Closed1','Closed2','Closed3') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono ) where cc_code !='Select All' " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);
                    else if (Ddltype.SelectedItem.Value == "2")
                        da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id  where sp.status in('3','Closed','Closed1','Closed2') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono ) where cc_code !='Select All' " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);
                    else if (Ddltype.SelectedItem.Value == "3")
                        da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id  where sp.status in('Closed3') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono )  where cc_code !='Select All' " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);
                }
                else
                    if (Ddltype.SelectedItem.Value == "1")
                        da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id  where sp.status in('3','Closed','Closed1','Closed2','Closed3') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono ) where cc_code IN (Select cu.cc_code from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New')) " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);
                    else if (Ddltype.SelectedItem.Value == "2")
                        da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id  where sp.status in('3','Closed','Closed1','Closed2') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono ) where cc_code IN (Select cu.cc_code from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New')) " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);
                    else if (Ddltype.SelectedItem.Value == "3")
                        da = new SqlDataAdapter("Select (case when s.pono is not null then s.pono else a.pono end)pono,s.po_date,(isnull(po_value,0))as [po_value],isnull(s.balance,0)balance,s.cc_code,s.dca_code,s.subdca_code,LEFT(s.remarks, CHARINDEX('$',remarks+'$')-1)as remarks,s.status,(s.vendor_id+' / '+s.vendor_name)as name from ((select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,isnull(po_value,0)po_value,isnull(balance,0)as balance,cc_code,dca_code,subdca_code,remarks,sp.status,sp.vendor_id,v.vendor_name from sppo sp join vendor v on v.vendor_id=sp.vendor_id  where sp.status from sppo where status in('Closed3') )s full outer join (select pono,isnull(sum(amended_amount),0)amended_amount,isnull(sum(substract_amt),0)as substract_amt from amend_sppo where status  in ('1','2','3') group by pono)a on a.pono=s.pono )  where cc_code IN (Select cu.cc_code from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New')) " + ViewState["Query1"].ToString() + " order by convert(datetime, s.po_date, 106) asc ", con);

            }


            da.Fill(ds, "vendor");
            GridView1.DataSource = ds.Tables["vendor"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlamendsppo.Visible = true;
            poppo.Show();
            GridViewRow id = GridView1.SelectedRow;
            ViewState["id"] = GridView1.SelectedValue.ToString();
            if (GridView1.SelectedRow.Cells[9].Text != "Closed")
            {
                tblclose.Visible = false;
                tblrun.Visible = true;
                da = new SqlDataAdapter("select s.pono,s.vendor_id,v.vendor_name,v.address,REPLACE(CONVERT(VARCHAR(11),s.po_date, 106), ' ', '-')as po_date,s.remarks,s.cc_code,s.dca_code,replace(s.po_value,.0000,.00) as po_value from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.pono='" + id.Cells[1].Text + "'", con);
                da.Fill(ds, "pono");
                if (ds.Tables["pono"].Rows.Count > 0)
                {
                    lblvenname.Text = ds.Tables["pono"].Rows[0][2].ToString();
                    lblvenaddress.Text = ds.Tables["pono"].Rows[0][3].ToString();
                    txtpono.Text = ds.Tables["pono"].Rows[0][0].ToString();
                    //txtpoamddate.Text = ds.Tables["pono"].Rows[0][4].ToString();
                    txtcccode.Text = ds.Tables["pono"].Rows[0][6].ToString();
                    txtdcacode.Text = ds.Tables["pono"].Rows[0][7].ToString();
                    txtvencode.Text = ds.Tables["pono"].Rows[0][1].ToString();
                }
                fillprintgrid(id.Cells[1].Text);
                fillprintgriditems(id.Cells[1].Text);
                fillprintgrid2(id.Cells[1].Text);
                fillprintgriditemsamend(id.Cells[1].Text);


            }
            else
            {
                tblrun.Visible = false;
                tblclose.Visible = true;
                da = new SqlDataAdapter("select * from Amend_sppo where pono='" + id.Cells[1].Text + "'", con);
                da.Fill(ds, "amend");
                if (ds.Tables["amend"].Rows.Count > 0)
                    da = new SqlDataAdapter("select top 1 remarks from Amend_sppo where pono='" + id.Cells[1].Text + "' order by id desc;select (po_value+isnull(sum(Amended_amount),0))as po_value from Amend_sppo a join sppo s on s.pono=a.pono where s.pono='" + id.Cells[1].Text + "' group by po_value", con);
                else
                    da = new SqlDataAdapter("select remarks from sppo where pono='" + id.Cells[1].Text + "';select po_value from sppo where pono='" + id.Cells[1].Text + "'", con);

                da.Fill(ds, "remarks");

                da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,balance,cc_code,dca_code,Subdca_code,isnull(SADescription,'')SADescription,isnull(PMDescription,'')PMDescription,isnull(HODescription,'')HODescription,isnull(sdescription,'')sdescription,s.modified_date,vendor_name from SPPO s join vendor v ON v.vendor_id=s.vendor_id where pono='" + id.Cells[1].Text + "' and s.status='Closed3' ", con);
                da.Fill(ds, "rep");
                if (ds.Tables["rep"].Rows.Count > 0)
                {
                    lblVname.Text = ds.Tables["rep"].Rows[0]["vendor_name"].ToString();
                    txtclsPONO.Text = ds.Tables["rep"].Rows[0]["pono"].ToString();
                    txtpodate.Text = ds.Tables["rep"].Rows[0]["po_date"].ToString();
                    txtpovalue.Text = ds.Tables["remarks1"].Rows[0]["po_value"].ToString();
                    txtbalance.Text = ds.Tables["rep"].Rows[0]["balance"].ToString();
                    txtClsCC.Text = ds.Tables["rep"].Rows[0]["cc_code"].ToString();
                    txtclsDca.Text = ds.Tables["rep"].Rows[0]["dca_code"].ToString();
                    txtsdca.Text = ds.Tables["rep"].Rows[0]["Subdca_code"].ToString();
                    lblremarks.Text = ds.Tables["remarks"].Rows[0]["remarks"].ToString();
                    lblSAdesc.Text = ds.Tables["rep"].Rows[0]["SADescription"].ToString();
                    lblPMremarks.Text = ds.Tables["rep"].Rows[0]["PMDescription"].ToString();
                    lblHoRemarks.Text = ds.Tables["rep"].Rows[0]["HODescription"].ToString();
                    lblSremarks.Text = ds.Tables["rep"].Rows[0]["sdescription"].ToString();
                    txtclsdate.Text = Convert.ToDateTime(ds.Tables["rep"].Rows[0]["modified_date"]).ToString("dd-MMM-yyyy");
                }
            }

        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    public void fillprintgrid(string pono)
    {
        try
        {
            //da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,Replace(isnull(po_value,0),'.0000','.00')po_value,LEFT(remarks, CHARINDEX('$',remarks+'$')-1)as rem,unit,Rate,quantity from SPPO where status in('3','Closed','Closed1','Closed2','Closed3')  and pono='" + pono + "' ", con);
            //da = new SqlDataAdapter("SELECT pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,Replace(isnull(po_value,0),'.0000','.00')po_value,unit,Rate,quantity,LTRIM(RTRIM(m.n.value('.[1]','varchar(8000)'))) AS Certs FROM(SELECT pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,Replace(isnull(po_value,0),'.0000','.00')po_value,unit,Rate,quantity,CAST('<XMLRoot><RowData>' + REPLACE(remarks,'$','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x FROM sppo where status in('3','Closed','Closed1','Closed2','Closed3')  and pono='" + pono + "')t CROSS APPLY x.nodes('/XMLRoot/RowData')m(n)", con);
            da = new SqlDataAdapter("select pono,REPLACE(CONVERT(VARCHAR(11),po_date, 106), ' ', '-')as po_date,Replace(isnull(po_value,0),'.0000','.00')po_value,replace(remarks,'$','<Br/>')as remarks,unit,Rate,quantity from SPPO where status in('3','Closed','Closed1','Closed2','Closed3')  and pono='" + pono + "' ", con);
            da.Fill(ds, "grid");
            ViewState["povalue"] = ds.Tables["grid"].Rows[0]["po_value"].ToString();
            ViewState["Curtblroles"] = ds.Tables["grid"];
            grdbill.DataSource = ds.Tables["grid"];
            grdbill.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillprintgriditems(string pono)
    {
        try
        {
            sdan = new SqlDataAdapter("select unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount from sppo_items where  pono='" + pono + "' and Type='Direct'", con);
            sdan.Fill(dsn, "items");
            grdpoitems.DataSource = dsn.Tables["items"];
            grdpoitems.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    public void fillprintgrid2(string pono)
    {
        try
        {
            //da = new SqlDataAdapter("select a.pono,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,a.remarks,Replace(isnull(Amended_amount,0),'.0000','.00')Amended_amount from SPPO s join Amend_sppo a on s.pono=a.pono where a.pono='" + pono + "' and a.status in ('1','2','3')", con);
            //da = new SqlDataAdapter("SELECT pono,REPLACE(CONVERT(VARCHAR(11),Amended_date, 106), ' ', '-')as Amended_date,Replace(isnull(Amended_amount,0),'.0000','.00')Amended_amount,LTRIM(RTRIM(m.n.value('.[1]','varchar(8000)'))) AS Certs FROM(SELECT a.pono,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,Replace(isnull(Amended_amount,0),'.0000','.00')Amended_amount,CAST('<XMLRoot><RowData>' + REPLACE(a.remarks,'$','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x FROM SPPO s join Amend_sppo a on s.pono=a.pono where a.pono='" + pono + "' and a.status in ('1','2','3'))t CROSS APPLY x.nodes('/XMLRoot/RowData')m(n) ", con);
            da = new SqlDataAdapter("select a.pono,REPLACE(CONVERT(VARCHAR(11),a.Amended_date, 106), ' ', '-')as Amended_date,replace(a.remarks,'$','<Br/>')as remarks,Replace(isnull(Amended_amount,0)-ISNULL(Substract_Amt,0),'.0000','.00')Amended_amount from SPPO s join Amend_sppo a on s.pono=a.pono where a.pono='" + pono + "' and a.status in ('1','2','3') order by a.id Asc", con);

            da.Fill(ds, "grid2");
            if (ds.Tables["grid2"].Rows.Count > 0)
            {
                ViewState["Curtblroles1"] = ds.Tables["grid2"];
                grdamendbill.DataSource = ds.Tables["grid2"];
                grdamendbill.DataBind();
            }
            else
            {
                //totalpovalue.Text = Convert.ToDecimal(ViewState["povalue"].ToString()).ToString();
                grdamendbill.DataSource = null;
                grdamendbill.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    public void fillprintgriditemsamend(string pono)
    {
        try
        {
            sdan = new SqlDataAdapter("select unit,CAST(rate AS Decimal(20,2)) as rate,quantity,Description,CAST((isnull(Amount,0)) AS Decimal(20,2))as Amount,po_type from sppo_items where  pono='" + pono + "' and Type='Amend' and status!='Rejected'", con);
            sdan.Fill(dsn, "itemsamend");
            if (dsn.Tables["itemsamend"].Rows.Count > 0)
            {
                grdpoitemsamend.DataSource = dsn.Tables["itemsamend"];
                grdpoitemsamend.DataBind();
            }
            else
            {
                grdpoitemsamend.DataSource = null;
                grdpoitemsamend.DataBind();
            }

            da = new SqlDataAdapter("select CAST((isnull(sum(Amended_amount),0)) AS Decimal(20,2))as Amended_amount,CAST((isnull(sum(Substract_Amt),0)) AS Decimal(20,2))as Substract_Amt from Amend_sppo where pono='" + pono + "' and status in ('1','2','3') group by pono", con);
            da.Fill(ds, "filltable2");
            if (ds.Tables["filltable2"].Rows.Count > 0)
            {
                if (ds.Tables["filltable2"].Rows[0][1].ToString() != "")
                {
                    txtamendpreviouspovalue.Text = ds.Tables["filltable2"].Rows[0][1].ToString();
                    txtamendpreviouspovalue.BackColor = Color.Red;
                }
                else
                {
                    txtamendpreviouspovalue.Text = "0";
                    txtamendpreviouspovalue.BackColor = Color.Red;
                }
                if (ds.Tables["filltable2"].Rows[0][0].ToString() != "")
                {
                    txtamdamount.Text = ds.Tables["filltable2"].Rows[0][0].ToString();
                    txtamdamount.BackColor = Color.Green;
                }
                else
                {
                    txtamdamount.Text = "0";
                    txtamdamount.BackColor = Color.Green;
                }
            }
            else
            {
                txtamendpreviouspovalue.Text = "0";
                txtamendpreviouspovalue.BackColor = Color.Red;
                txtamdamount.Text = "0";
                txtamdamount.BackColor = Color.Green;
            }
            decimal txtamendpreviouspovalue1 = Convert.ToDecimal(txtamendpreviouspovalue.Text);
            decimal txtamdamount1 = Convert.ToDecimal(txtamdamount.Text);
            if (txtamendpreviouspovalue1 > txtamdamount1)
            {
                //txtamdpoval.Text = (Convert.ToDecimal(txtamendpreviouspovalue.Text) - Convert.ToDecimal(txtamdamount.Text)).ToString();
                txtamdpoval.Text = ViewState["povalue"].ToString();
                txtamdpoval.BackColor = Color.Red;
            }
            if (txtamendpreviouspovalue1 < txtamdamount1)
            {
                //txtamdpoval.Text = (Convert.ToDecimal(txtamdamount.Text) - Convert.ToDecimal(txtamendpreviouspovalue.Text)).ToString();
                txtamdpoval.Text = ViewState["povalue"].ToString();
                txtamdpoval.BackColor = Color.Green;
            }
            else
            {
                //txtamdpoval.Text = (Convert.ToDecimal(txtamendpreviouspovalue.Text) + Convert.ToDecimal(txtamdamount.Text)).ToString();
                txtamdpoval.Text = ViewState["povalue"].ToString();
            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    public void filldropdown()
    {
        
        try
        {
            if (ddldca.SelectedValue != "Select")
            {
                if (ddlcccode.SelectedValue != "Select All")
                {
                    if (Session["roles"].ToString() == "Project Manager")
                        da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.dca_code='" + ddldca.SelectedValue + "' and s.cc_code='" + ddlcccode.SelectedValue + "' and  s.cc_code IN (Select cu.cc_code from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New')) and s.status in('3','Closed','Closed1','Closed2','Closed3') order by Name", con);
                    else
                        da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.dca_code='" + ddldca.SelectedValue + "' and s.cc_code='" + ddlcccode.SelectedValue + "' and  s.status in('3','Closed','Closed1','Closed2','Closed3') order by Name", con);
                }
                else
                {
                    if (Session["roles"].ToString() == "Project Manager")
                        da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.dca_code='" + ddldca.SelectedValue + "' and  s.cc_code IN (Select cu.cc_code from cc_user cu join Cost_Center cc on cu.cc_code=cc.cc_code where user_name='" + Session["user"].ToString() + "' and cc.status in ('Old','New')) and s.status in('3','Closed','Closed1','Closed2','Closed3') order by Name", con);
                    else
                        da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.dca_code='" + ddldca.SelectedValue + "' and  s.status in('3','Closed','Closed1','Closed2','Closed3') order by Name", con);
                }

                //else
                //    da = new SqlDataAdapter("select distinct s.vendor_id,v.vendor_name+' ('+s.vendor_id+')' Name from SPPO s join vendor v on s.vendor_id=v.vendor_id where s.cc_code='" + ddlcccode.SelectedValue + "' and s.status in('3','Closed','Closed1','Closed2','Closed3') order by Name", con);
                da.Fill(ds, "fill");
                ddlvendor.DataTextField = "Name";
                ddlvendor.DataValueField = "vendor_id";
                ddlvendor.DataSource = ds.Tables["fill"];
                ddlvendor.DataBind();
                ddlvendor.Items.Insert(0, "Select vendor");
                ddlvendor.Items.Insert(1, "Select All");
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    public void filldca()
    {
        try
        {
            if (ddlcccode.SelectedItem.Text == "Select All")
            {
                da = new SqlDataAdapter("select distinct sp.dca_code,(sp.dca_code+'  '+dca_name)as name from SPPO sp join dca d on d.dca_code=sp.dca_code order by sp.dca_code asc", con);
            }
            else
            {
                da = new SqlDataAdapter("select distinct sp.dca_code,(sp.dca_code+'  '+dca_name)as name from SPPO sp join dca d on d.dca_code=sp.dca_code  where cc_code='" + ddlcccode.SelectedValue + "'", con);
            }
            da.Fill(ds, "filldca");
            ddldca.DataTextField = "name";
            ddldca.DataValueField = "dca_code";
            ddldca.DataSource = ds.Tables["filldca"];
            ddldca.DataBind();
            ddldca.Items.Insert(0, "Select DCA");
            //ddlvendor.Items.Insert(1, "Select All");

        }


        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    protected void ddldca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldca.SelectedValue != "Select")
            {
                filldropdown();
            }
            else
            {
                JavaScript.Alert("Select Cost Center");
            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
        //filldca();

    }
    protected void ddlcccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //filldropdown();
        btnSearch.Visible = true;
        ddldca.Items.Clear();
        filldca();
    }
    
    protected void grdbill_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
               Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "po_value"));
                      
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount);

            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    protected void grdpoitems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                POAMT += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", POAMT);

            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }

    protected void grdamendbill_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Amount1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amended_amount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = String.Format("Rs. {0:#,##,##,###.00}", Amount1);
                //totalpovalue.Text = Convert.ToDecimal(Amount1 + Convert.ToDecimal(ds.Tables["grid"].Rows[0].ItemArray[2].ToString())).ToString();

            }


        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    private decimal AdditionAmount = (decimal)0.0;
    private decimal SubstractionAmount = (decimal)0.0;
    protected void grdpoitemsamend_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //POAMTAMEND += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                if (e.Row.Cells[5].Text == "Subtract")
                {                    
                    SubstractionAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                }
                else
                {                    
                    AdditionAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = String.Format("Rs. {0:#,##,##,###.00}", (AdditionAmount - SubstractionAmount));

            }
        }

        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }
    }
    private decimal Amount = (decimal)0.0;
    private decimal Amount1 = (decimal)0.0;
    private decimal MainAmount = (decimal)0.0;
    private decimal Balance = (decimal)0.0;
    private decimal POAMT = (decimal)0.0;
    private decimal POAMTAMEND = (decimal)0.0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[9].Text == "Closed1" || e.Row.Cells[9].Text == "Closed2" || e.Row.Cells[9].Text == "3" || e.Row.Cells[9].Text == "Closed")
                {
                    e.Row.Cells[9].Text = "Running";
                }
                else if (e.Row.Cells[9].Text == "Closed3")
                {
                    e.Row.Cells[9].Text = "Closed";
                }
                Balance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "balance"));
                MainAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "po_value"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = String.Format("Rs. {0:#,##,##,###.00}", Balance);
                e.Row.Cells[4].Text = String.Format("Rs. {0:#,##,##,###.00}", MainAmount);


            }
        }
        catch (Exception ex)
        {
            Utilities.CatchException(ex);
            JavaScript.Alert(ConfigurationManager.AppSettings["ExceptionMessage"].ToString());
        }

    }
    //protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    filldca();
    //}
    protected void Ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnoldreport_Click(object sender, EventArgs e)
    {

        Response.Redirect("ServiceproviderReport.aspx");

    }
}