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
using System.Web.Services;
using System.Globalization;
public partial class ViewProfitandLossReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Essel childmaster = (Essel)this.Master;
        HtmlAnchor lbl = (HtmlAnchor)childmaster.FindControl("Accounting");
        lbl.Attributes.Add("class", "active");
        if (Session["user"] == null)
            Response.Redirect("Default.aspx");

        if (!IsPostBack)
        {
            tblbody.Visible = false;
            btncollapse.Visible= false;
            btnexpand.Visible = false;
            LoadYear();
           
        }
    }
    public decimal TotalExpnces = (decimal)0.0;
    public decimal TotalIncome = (decimal)0.0;
    public decimal GrossProfit = (decimal)0.0;
    public decimal GrossLoss = (decimal)0.0;
    public decimal NetExpnces = (decimal)0.0;
    public decimal NetIncome = (decimal)0.0;
    public decimal NetProfit = (decimal)0.0;
    public decimal NetLoss = (decimal)0.0;
    public string Year = string.Empty;
    public string Year1 = string.Empty;
    public void LoadYear()
    {
        for (int i = 2016; i <= System.DateTime.Now.Year; i++)
        {
            if (System.DateTime.Now.Month < 4 && System.DateTime.Now.Year == i)
            {
            }
            else
            {
                ddlyear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString().Substring(2, 2), i.ToString()));
            }

        }
        ddlyear.Items.Insert(0, "Select Year");
    }


    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tvexp.Nodes.Clear();
            tvin.Nodes.Clear();
            tvexpn.Nodes.Clear();
            tvinn.Nodes.Clear();
            SpnGlossValue.InnerText = string.Empty;
            Spntotalgrossvalue.InnerText = string.Empty;
            Spntotalinvalue.InnerText = string.Empty;
            SpngrosslossN.InnerText = string.Empty;
            SpnGlossValue.InnerText = string.Empty;
            Spntotalgrossvalue.InnerText = string.Empty;
            Spntotalinvalue.InnerText = string.Empty;
            SpngrosslossN.InnerText = string.Empty;
            SpnNetProfit.InnerText = string.Empty;
            SpnTotalNetProfit.InnerText = string.Empty;
            SpnTotalNetLoss.InnerText = string.Empty;
            SpnNetLoss.InnerText = string.Empty;
            SpnTotalNetLoss.InnerText = string.Empty;
            SpnTotalNetProfit.InnerText = string.Empty;
            var cultureInfo = new CultureInfo("en-IN");
            //var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
            Year = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            Year1 = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            Session["FYYear"] = ddlyear.SelectedItem.Text;
            Session["Year"] = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            Session["Year1"] = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            spnyear.InnerText = "For the period ended 31st March," + Year1;
            if (ddlyear.SelectedIndex != 0)
            {
                da = new SqlDataAdapter("Sp_GetProfitandLossreport", con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
                da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
                da.SelectCommand.Parameters.AddWithValue("@ReportType", SqlDbType.VarChar).Value = "ProfitAndLoss";
                da.Fill(ds, "ViewP&L");

                if (ds.Tables["ViewP&L"].Rows.Count > 0)
                {
                    tblbody.Visible = true;
                    btncollapse.Visible = true;
                    btnexpand.Visible = true;
                    Session["ViewP&L"] = ds.Tables["ViewP&L"];
                    if (ds.Tables[0].Rows.Count > 0)
                    {


                        var readerN20Y = ds.Tables[0].CreateDataReader();
                        var tblN20Y = new DataTable("tblN20Y");
                        tblN20Y.Columns.Add("id", typeof(string));
                        tblN20Y.Columns.Add("name", typeof(string));
                        tblN20Y.Columns.Add("parentid", typeof(string));
                        tblN20Y.Columns.Add("SubGroupId", typeof(string));
                        tblN20Y.Columns.Add("SubGroupName", typeof(string));
                        tblN20Y.Columns.Add("Value", typeof(string));
                        tblN20Y.Columns.Add("LedgerID", typeof(string));
                        tblN20Y.Columns.Add("GroupID", typeof(string));
                        while (readerN20Y.Read())
                        {
                            tblN20Y.Rows.Add(new[]
            {
                readerN20Y["id"].ToString(),
            readerN20Y["name"].ToString(),
            readerN20Y["parentid"].ToString(),
            readerN20Y["SubGroupId"].ToString(),
            readerN20Y["SubGroupName"].ToString(),
            readerN20Y["Value"].ToString(),
               readerN20Y["LedgerID"].ToString(),
                 readerN20Y["GroupID"].ToString()           
            });
                        }
                        tblN20Y.AcceptChanges();
                        ds1 = new DataSet();
                        ds1.Tables.Add(tblN20Y);
                        ds1.AcceptChanges();
                        var parentid = (from myrow in ds1.Tables[0].AsEnumerable()
                                        select myrow["parentid"]).FirstOrDefault();
                        var ExpSum = ds1.Tables[0].AsEnumerable().Where(o => Convert.ToInt32(o.ItemArray[6]) != 0).Sum(o => Convert.ToDecimal(o.ItemArray[5]));
                        TotalExpnces = ExpSum;
                        SpnExptotal.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", ExpSum.ToString()).Replace("Rs.", "").Replace("₹", "");

                        CreateTreeViewDataTableN20Y(ds1.Tables[0], Convert.ToString(parentid), null);

                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        var readerN30Y = ds.Tables[1].CreateDataReader();
                        var tblN30Y = new DataTable("tblN30Y");
                        tblN30Y.Columns.Add("id", typeof(string));
                        tblN30Y.Columns.Add("name", typeof(string));
                        tblN30Y.Columns.Add("parentid", typeof(string));
                        tblN30Y.Columns.Add("SubGroupId", typeof(string));
                        tblN30Y.Columns.Add("SubGroupName", typeof(string));
                        tblN30Y.Columns.Add("Value", typeof(string));
                        tblN30Y.Columns.Add("LedgerID", typeof(string));
                        tblN30Y.Columns.Add("GroupID", typeof(string));
                        while (readerN30Y.Read())
                        {
                            tblN30Y.Rows.Add(new[]
            {
                readerN30Y["id"].ToString(),
            readerN30Y["name"].ToString(),
            readerN30Y["parentid"].ToString(),
               readerN30Y["SubGroupId"].ToString(),
            readerN30Y["SubGroupName"].ToString(),
            readerN30Y["Value"].ToString(),
               readerN30Y["LedgerID"].ToString(),
           readerN30Y["GroupID"].ToString(),
                                   
            });
                        }
                        tblN30Y.AcceptChanges();
                        ds1 = new DataSet();
                        ds1.Tables.Add(tblN30Y);
                        ds1.AcceptChanges();
                        var parentid = (from myrow in ds1.Tables[0].AsEnumerable()
                                        select myrow["parentid"]).FirstOrDefault();
                        var InSum = ds1.Tables[0].AsEnumerable().Where(o => Convert.ToInt32(o.ItemArray[6]) != 0).Sum(o => Convert.ToDecimal(o.ItemArray[5]));
                        SpnIntotal.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", InSum.ToString()).Replace("₹", "");
                        TotalIncome = InSum;
                        CreateTreeViewDataTableN30Y(ds1.Tables[0], Convert.ToString(parentid), null);
                    }

                    if (TotalExpnces <= TotalIncome)
                    {

                        SpnGprofitValue.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", TotalIncome - TotalExpnces).Replace("Rs.", "").Replace("₹", "");
                        Spntotalgrossvalue.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", TotalIncome).Replace("Rs.", "").Replace("₹", "");
                        Spntotalinvalue.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", TotalIncome).Replace("Rs.", "").Replace("₹", "");
                        SpngrossProfitN.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", TotalIncome - TotalExpnces).Replace("Rs.", "").Replace("₹", "");
                        NetIncome = NetIncome + (TotalIncome - TotalExpnces);
                        Spngrosslosstxt.Visible = false;
                        SpngrossProfitText.Visible = true;
                        spngrossprofit.Visible = true;
                        spngrossloss.Visible = false;
                        SpnExptotal.Visible = true;
                        SpnIntotal.Visible = false;
                        SpngrosslossN.Visible = false;
                        SpngrossProfitN.Visible = true;
                        SpnGprofitValue.Visible = true;
                    }
                    else
                    {

                        SpnGlossValue.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", TotalExpnces - TotalIncome).Replace("Rs.", "").Replace("₹", "");
                        Spntotalgrossvalue.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", TotalExpnces).Replace("Rs.", "").Replace("₹", "");
                        Spntotalinvalue.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", TotalExpnces).Replace("Rs.", "").Replace("₹", "");
                        SpngrosslossN.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", TotalExpnces - TotalIncome).Replace("Rs.", "").Replace("₹", "");
                        NetExpnces = NetExpnces + (TotalExpnces - TotalIncome);
                        SpngrossProfitText.Visible = false;
                        spngrossprofit.Visible = false;
                        spngrossloss.Visible = true;
                        SpnExptotal.Visible = false;
                        SpnIntotal.Visible = true;
                        Spngrosslosstxt.Visible = true;
                        SpngrosslossN.Visible = true;
                        SpngrossProfitN.Visible = false;
                        SpnGprofitValue.Visible = false;
                    }


                    if (ds.Tables[2].Rows.Count > 0)
                    {

                        var readerN20N = ds.Tables[2].CreateDataReader();
                        var tblN20N = new DataTable("tblN20N");
                        tblN20N.Columns.Add("id", typeof(string));
                        tblN20N.Columns.Add("name", typeof(string));
                        tblN20N.Columns.Add("parentid", typeof(string));
                        tblN20N.Columns.Add("SubGroupId", typeof(string));
                        tblN20N.Columns.Add("SubGroupName", typeof(string));
                        tblN20N.Columns.Add("Value", typeof(string));
                        tblN20N.Columns.Add("LedgerID", typeof(string));
                        tblN20N.Columns.Add("GroupID", typeof(string));
                        while (readerN20N.Read())
                        {
                            tblN20N.Rows.Add(new[]
            {
                readerN20N["id"].ToString(),
            readerN20N["name"].ToString(),
            readerN20N["parentid"].ToString(),
               readerN20N["SubGroupId"].ToString(),
            readerN20N["SubGroupName"].ToString(),
            readerN20N["Value"].ToString(),
               readerN20N["LedgerID"].ToString(),
                readerN20N["GroupID"].ToString()   
                                   
            });
                        }
                        tblN20N.AcceptChanges();
                        ds1 = new DataSet();
                        ds1.Tables.Add(tblN20N);
                        ds1.AcceptChanges();
                        var parentid = (from myrow in ds1.Tables[0].AsEnumerable()
                                        select myrow["parentid"]).FirstOrDefault();
                        var NExpSum = ds1.Tables[0].AsEnumerable().Where(o => Convert.ToInt32(o.ItemArray[6]) != 0).Sum(o => Convert.ToDecimal(o.ItemArray[5]));
                        NetExpnces = NetExpnces + NExpSum;
                        SpnTotalNetExp.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", NetExpnces.ToString()).Replace("₹", "");
                        CreateTreeViewDataTableN20N(ds1.Tables[0], Convert.ToString(parentid), null);
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {

                        var readerN30N = ds.Tables[3].CreateDataReader();
                        var tblN30N = new DataTable("tblN30N");
                        tblN30N.Columns.Add("id", typeof(string));
                        tblN30N.Columns.Add("name", typeof(string));
                        tblN30N.Columns.Add("parentid", typeof(string));
                        tblN30N.Columns.Add("SubGroupId", typeof(string));
                        tblN30N.Columns.Add("SubGroupName", typeof(string));
                        tblN30N.Columns.Add("Value", typeof(string));
                        tblN30N.Columns.Add("LedgerID", typeof(string));
                        tblN30N.Columns.Add("GroupID", typeof(string));
                        while (readerN30N.Read())
                        {
                            tblN30N.Rows.Add(new[]
            {
                readerN30N["id"].ToString(),
            readerN30N["name"].ToString(),
            readerN30N["parentid"].ToString(),
               readerN30N["SubGroupId"].ToString(),
            readerN30N["SubGroupName"].ToString(),
            readerN30N["Value"].ToString(),
               readerN30N["LedgerID"].ToString(),         
                        readerN30N["GroupID"].ToString()                    
            });
                        }
                        tblN30N.AcceptChanges();
                        ds1 = new DataSet();
                        ds1.Tables.Add(tblN30N);
                        ds1.AcceptChanges();
                        var parentid = (from myrow in ds1.Tables[0].AsEnumerable()
                                        select myrow["parentid"]).FirstOrDefault();
                        var NInSum = ds1.Tables[0].AsEnumerable().Where(o => Convert.ToInt32(o.ItemArray[6]) != 0).Sum(o => Convert.ToDecimal(o.ItemArray[5]));
                        SpnTotalNetIn.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", NInSum.ToString()).Replace("₹", "");
                        NetIncome = NetIncome + NInSum;

                        CreateTreeViewDataTableN30N(ds1.Tables[0], Convert.ToString(parentid), null);
                    }
                    if (NetExpnces <= NetIncome)
                    {

                        SpnNetProfit.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", NetIncome - NetExpnces).Replace("Rs.", "").Replace("₹", "");
                        SpnTotalNetProfit.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", Convert.ToDecimal(SpnTotalNetExp.InnerText.Replace("Rs. ", "")) + (NetIncome - NetExpnces)).Replace("Rs.", "").Replace("₹", "");
                        SpnTotalNetLoss.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", NetIncome).Replace("Rs.", "").Replace("₹", "");
                        spnnetprofittext.Visible = true;
                        spnnetlosstext.Visible = false;
                        SpnTotalNetExp.Visible = true;
                        SpnTotalNetIn.Visible = false;
                    }
                    else
                    {

                        SpnNetLoss.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", NetExpnces - NetIncome).Replace("Rs.", "").Replace("₹", "");
                        SpnTotalNetLoss.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", (NetExpnces - NetIncome)).Replace("Rs.", "").Replace("₹", "");
                        SpnTotalNetProfit.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", Convert.ToDecimal(SpnTotalNetIn.InnerText.Replace("Rs. ", "")) + (NetExpnces - NetIncome)).Replace("Rs.", "").Replace("₹", "");
                        spnnetprofittext.Visible = false;
                        spnnetlosstext.Visible = true;
                        SpnTotalNetExp.Visible = false;
                        SpnTotalNetIn.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #region Bind Tree views
    private void CreateTreeViewDataTableN20Y(DataTable dt, string parentId, TreeNode parentNode)
    {
        var cultureInfo = new CultureInfo("en-IN");
        DataRow[] drs = dt.Select(string.Format("parentid ='{0}'", parentId));
        foreach (DataRow i in drs)
        {
            StringBuilder sb = new StringBuilder();
            Decimal Value = 0;
            if (i["LedgerID"].ToString() == "0")
            {
              
                if (i["Id"].ToString() == i["GroupId"].ToString())
                {
                    var ObjExp = (dt as DataTable).AsEnumerable().Where(o => Convert.ToString(o.ItemArray[7]) == i["Id"].ToString() && Convert.ToString(o.ItemArray[6]) != "0" ).Sum(o => Convert.ToDecimal(o.ItemArray[5]));
                    Value = ObjExp;
                }
                else
                {
                    var ObjExp = (from a in (dt as DataTable).AsEnumerable() where a.Field<string>("ParentID") == i["Id"].ToString() group a by a.Field<string>("ParentID") into g select new { SubGroupId = g.Key, SubGroupName = g.First().ItemArray[4], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[5])) });

                    if (ObjExp.ToList().Count > 0)
                    {
                        Value = ObjExp.ToList()[0].Value;
                    }
                }
               
            }
            if (parentNode == null)
            {

                if (i["LedgerID"].ToString() == "0")
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='250' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='250' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='250' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='250' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='250' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='250' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:x-small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                tvexp.Nodes.Add(newNode);
                CreateTreeViewDataTableN20Y(dt, Convert.ToString(i["id"]), newNode);

            }
            else
            {

                if (i["LedgerID"].ToString() == "0")
                {

                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='150' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='150' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='150' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' ><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'>" + i["SubGroupName"].ToString() + "</td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='150' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='150' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='150' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:x-small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' ><col width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                parentNode.ChildNodes.Add(newNode);
                CreateTreeViewDataTableN20Y(dt, Convert.ToString(i["id"]), newNode);
            }

        }
        tvexp.CollapseAll();

    }
    private void CreateTreeViewDataTableN30Y(DataTable dt, string parentId, TreeNode parentNode)
    {
        var cultureInfo = new CultureInfo("en-IN");
        DataRow[] drs = dt.Select(string.Format("parentid ='{0}'", parentId));
        foreach (DataRow i in drs)
        {
            StringBuilder sb = new StringBuilder();
            Decimal Value = 0;
            if (i["LedgerID"].ToString() == "0")
            {
                if (i["Id"].ToString() == i["GroupId"].ToString())
                {
                    var ObjExp = (dt as DataTable).AsEnumerable().Where(o => Convert.ToString(o.ItemArray[7]) == i["Id"].ToString() && Convert.ToString(o.ItemArray[6]) != "0").Sum(o => Convert.ToDecimal(o.ItemArray[5]));
                    Value = ObjExp;
                }
                else
                {
                    var ObjExp = (from a in (dt as DataTable).AsEnumerable() where a.Field<string>("ParentID") == i["Id"].ToString() group a by a.Field<string>("ParentID") into g select new { SubGroupId = g.Key, SubGroupName = g.First().ItemArray[4], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[5])) });

                    if (ObjExp.ToList().Count > 0)
                    {
                        Value = ObjExp.ToList()[0].Value;
                    }
                }

            }
            if (parentNode == null)
            {

                if (i["LedgerID"].ToString() == "0")
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='250' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='250' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='250' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='250' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='250' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='250' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:x-small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                tvin.Nodes.Add(newNode);
                CreateTreeViewDataTableN30Y(dt, Convert.ToString(i["id"]), newNode);

            }
            else
            {
                if (i["LedgerID"].ToString() == "0")
                {

                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='150' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='150' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='150' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' ><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'>" + i["SubGroupName"].ToString() + "</td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='150' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='150' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='150' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:x-small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' ><col width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                parentNode.ChildNodes.Add(newNode);
                CreateTreeViewDataTableN30Y(dt, Convert.ToString(i["id"]), newNode);
            }

        }
        tvin.CollapseAll();
    }
    private void CreateTreeViewDataTableN20N(DataTable dt, string parentId, TreeNode parentNode)
    {
        var cultureInfo = new CultureInfo("en-IN");
        DataRow[] drs = dt.Select(string.Format("parentid ='{0}'", parentId));
        foreach (DataRow i in drs)
        {
            StringBuilder sb = new StringBuilder();
            Decimal Value = 0;
            if (i["LedgerID"].ToString() == "0")
            {
                if (i["Id"].ToString() == i["GroupId"].ToString())
                {
                    var ObjExp = (dt as DataTable).AsEnumerable().Where(o => Convert.ToString(o.ItemArray[7]) == i["Id"].ToString() && Convert.ToString(o.ItemArray[6]) != "0").Sum(o => Convert.ToDecimal(o.ItemArray[5]));
                    Value = ObjExp;
                }
                else
                {
                    var ObjExp = (from a in (dt as DataTable).AsEnumerable() where a.Field<string>("ParentID") == i["Id"].ToString() group a by a.Field<string>("ParentID") into g select new { SubGroupId = g.Key, SubGroupName = g.First().ItemArray[4], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[5])) });

                    if (ObjExp.ToList().Count > 0)
                    {
                        Value = ObjExp.ToList()[0].Value;
                    }
                }
            }
            if (parentNode == null)
            {

                if (i["LedgerID"].ToString() == "0")
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='250' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='250' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='250' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='150' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='150' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='150' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                tvexpn.Nodes.Add(newNode);
                CreateTreeViewDataTableN20N(dt, Convert.ToString(i["id"]), newNode);

            }
            else
            {
                if (i["LedgerID"].ToString() == "0")
                {

                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' ><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'>" + i["SubGroupName"].ToString() + "</td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' ><col width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                parentNode.ChildNodes.Add(newNode);
                CreateTreeViewDataTableN20N(dt, Convert.ToString(i["id"]), newNode);
            }

        }
        tvexpn.CollapseAll();

    }
    private void CreateTreeViewDataTableN30N(DataTable dt, string parentId, TreeNode parentNode)
    {
        var cultureInfo = new CultureInfo("en-IN");
        DataRow[] drs = dt.Select(string.Format("parentid ='{0}'", parentId));
        foreach (DataRow i in drs)
        {
            StringBuilder sb = new StringBuilder();
            Decimal Value = 0;
            if (i["LedgerID"].ToString() == "0")
            {
                if (i["Id"].ToString() == i["GroupId"].ToString())
                {
                    var ObjExp = (dt as DataTable).AsEnumerable().Where(o => Convert.ToString(o.ItemArray[7]) == i["Id"].ToString() && Convert.ToString(o.ItemArray[6]) != "0").Sum(o => Convert.ToDecimal(o.ItemArray[5]));
                    Value = ObjExp;
                }
                else
                {
                    var ObjExp = (from a in (dt as DataTable).AsEnumerable() where a.Field<string>("ParentID") == i["Id"].ToString() group a by a.Field<string>("ParentID") into g select new { SubGroupId = g.Key, SubGroupName = g.First().ItemArray[4], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[5])) });

                    if (ObjExp.ToList().Count > 0)
                    {
                        Value = ObjExp.ToList()[0].Value;
                    }
                }

            }
            if (parentNode == null)
            {

                if (i["LedgerID"].ToString() == "0")
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='250' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='250' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='250' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='250' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='250' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='250' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                tvinn.Nodes.Add(newNode);
                CreateTreeViewDataTableN30N(dt, Convert.ToString(i["id"]), newNode);

            }
            else
            {
                if (i["LedgerID"].ToString() == "0")
                {

                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='150' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='150' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='150' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' ><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'>" + i["SubGroupName"].ToString() + "</td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='150' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='150' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='150' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' ><col width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year="+Year+"&Year1="+Year1+"' target='_blank'>" + i["name"].ToString() + "</a></span></td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                parentNode.ChildNodes.Add(newNode);
                CreateTreeViewDataTableN30N(dt, Convert.ToString(i["id"]), newNode);
            }

        }
        tvinn.CollapseAll();
    }
    #endregion
    public void GeneratePandL()
    {
        var cultureInfo = new CultureInfo("en-IN");
        DataSet Objds = (Session["ViewP&L"]) as DataSet;
        if (Objds.Tables["ViewP&L"].Rows.Count > 0)
        {

            StringBuilder sb = new StringBuilder();
            Session["FYYear"] = ddlyear.SelectedItem.Text;
            Session["Year"] = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            Session["Year1"] = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            sb.Append("<table><tr><td colspan='5' height='49' align='center' valign=middle bgcolor='#B7DEE8'><b><span style='font-size:xx-large'>ESSEL Projects Pvt Ltd</span></font></b></td></tr>");
            sb.Append("<tr><td colspan='5' height='34' align='center' valign=middle bgcolor='#E6B9B8'><b><span style='font-size:x-large'>PROFIT & LOSS ACCOUNT</span></b></td></tr>");
            sb.Append("<tr><td colspan='5' height='20' align='left' valign=top bgcolor='#B7DEE8'><b><span style='font-size:larger'>For the period ended 31st March, " + (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString() + "</span></b></td></tr>");
            sb.Append("<tr>");
            sb.Append("<td style='border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; ' height='20' align='left' valign=bottom bgcolor='#E6B9B8'><b><span style='color:#000000;font-size:larger'>EXPENDITURE</span></b></td>");
            sb.Append("<td style='border-top: 1px solid #000000; border-bottom: 1px solid #000000; ' align='right' valign=bottom bgcolor='#E6B9B8'><b><span style='color:#000000;font-size:larger'>Rs.</span></b></td>");
            sb.Append("<td  width='10px' style='border-top: 1px solid #000000; border-bottom: 1px solid #000000; ' align='center' valign=bottom bgcolor='#E6B9B8'></td>");

            sb.Append("<td style='border-top: 1px solid #000000; border-bottom: 1px solid #000000;' align='left' valign=bottom bgcolor='#E6B9B8'><b><span style='color:#000000;font-size:larger'>INCOME</span></b></td>");
            sb.Append("<td style='border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-right: 1px solid #000000' align='right' valign=bottom bgcolor='#E6B9B8'><b><span style='color:#000000;font-size:larger'>Rs.</span></b></td>");
            sb.Append("</tr>");

            var ObjExp = (from a in (Objds.Tables["ViewP&L"] as DataTable).AsEnumerable() where a.Field<string>("Grossprofitcalc") == "Y" && a.Field<string>("NatureGroupId") == "N20" group a by a.Field<string>("GroupId") into g select new { GroupId = g.Key, GroupName = g.First().ItemArray[1], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[8])) });
            var ObjIn = (from a in (Objds.Tables["ViewP&L"] as DataTable).AsEnumerable() where a.Field<string>("Grossprofitcalc") == "Y" && a.Field<string>("NatureGroupId") == "N30" group a by a.Field<string>("GroupId") into g select new { GroupId = g.Key, GroupName = g.First().ItemArray[1], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[8])) });
            var ObjNExp = (from a in (Objds.Tables["ViewP&L"] as DataTable).AsEnumerable() where a.Field<string>("Grossprofitcalc") == "N" && a.Field<string>("NatureGroupId") == "N20" group a by a.Field<string>("GroupId") into g select new { GroupId = g.Key, GroupName = g.First().ItemArray[1], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[8])) });
            var ObjNIn = (from a in (Objds.Tables["ViewP&L"] as DataTable).AsEnumerable() where a.Field<string>("Grossprofitcalc") == "N" && a.Field<string>("NatureGroupId") == "N30" group a by a.Field<string>("GroupId") into g select new { GroupId = g.Key, GroupName = g.First().ItemArray[1], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[8])) });

            for (int i = 0; i < ObjExp.ToList().Count; i++)
            {

                sb.Append("<tr >");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><ul id='" + ObjExp.ToList()[i].GroupId + "' class='treeview-red'></ul><li><span>" + ObjExp.ToList()[i].GroupName + "</span>");
                sb.Append("<td  align='left' bgcolor='#B7DEE8' style='width:300px'><ul><li id='m2'><span style='color: #000000; font-size: small'>" + ObjExp.ToList()[i].GroupName + "</span></li>");
                sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", ObjExp.ToList()[i].Value).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");

                TotalExpnces = TotalExpnces + Convert.ToDecimal(ObjExp.ToList()[i].Value);
                if (ObjIn.ToList().Count > i)
                {
                    sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br><a href='GetProfitandLossDetailedview.aspx?GroupId=" + ObjIn.ToList()[i].GroupId + "' target='_blank' class='gridViewToolTip'>" + ObjIn.ToList()[i].GroupName + " </a></span></td>");
                    sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", ObjIn.ToList()[i].Value).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                    TotalIncome = TotalIncome + Convert.ToDecimal(ObjIn.ToList()[i].Value);
                }
                else
                {
                    sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                    sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                }

                sb.Append("</tr>");
            }

            sb.Append("<tr>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 1px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>Rs.</span></td>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", TotalExpnces).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            if (TotalExpnces <= TotalIncome)
            {
                GrossProfit = TotalIncome - TotalExpnces;
                sb.Append("<tr>");
                sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>GROSS PROFIT</span></td>");
                sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", GrossProfit).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                sb.Append("</tr>");
            }
            else
            {
                GrossLoss = TotalExpnces - TotalIncome;
                sb.Append("<tr>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>GROSS LOSS</span></td>");
                sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", GrossLoss).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                sb.Append("</tr>");
            }
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 1px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 1px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>Rs.</span></td>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", Convert.ToDecimal(TotalExpnces + TotalIncome)).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>Rs.</span></td>");
            sb.Append("<td   height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", Convert.ToDecimal(TotalExpnces + TotalIncome)).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 2px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 2px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            sb.Append("<tr height='5px'><td colspan='5' width='10px' bgcolor='#B7DEE8'></td></tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-top: 2px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-top: 2px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            int NexpCount = ObjNExp.ToList().Count;
            int NInComeCount = ObjNIn.ToList().Count;
            if (TotalExpnces <= TotalIncome)
            {

                for (int i = 0; i < (NexpCount <= NInComeCount + 1 ? NInComeCount + 1 : NexpCount); i++)
                {
                    sb.Append("<tr >");
                    if (i == 0 && ObjNExp.ToList().Count > 0)
                    {
                        sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br><a href='GetProfitandLossDetailedview.aspx?GroupId=" + ObjNExp.ToList()[i].GroupId + "' target='_blank' class='gridViewToolTip'>" + ObjNExp.ToList()[i].GroupName + " </a></span></td>");
                        sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", ObjNExp.ToList()[i].Value).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                        sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
                        sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>GROSS PROFIT</span></td>");
                        sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", GrossProfit).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                        NetExpnces = NetExpnces + Convert.ToDecimal(ObjNExp.ToList()[0].Value);
                        NetIncome = NetIncome + GrossProfit;
                    }
                    else if (i == 0 && ObjNExp.ToList().Count == 0)
                    {
                        sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                        sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                        sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
                        sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>GROSS PROFIT</span></td>");
                        sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", GrossProfit).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                        NetExpnces = NetExpnces + 0;
                        NetIncome = NetIncome + GrossProfit;
                    }
                    else
                    {
                        if (ObjNExp.ToList().Count > i)
                        {
                            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br><a href='GetProfitandLossDetailedview.aspx?GroupId=" + ObjNExp.ToList()[i].GroupId + "' target='_blank' class='gridViewToolTip'>" + ObjNExp.ToList()[i].GroupName + " </a></span></td>");
                            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", ObjNExp.ToList()[i].Value).Replace("Rs.", "").Replace("₹", "") + "</span></td>");


                            NetExpnces = NetExpnces + Convert.ToDecimal(ObjNExp.ToList()[i].Value);
                        }
                        else
                        {
                            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");


                        }
                        sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
                        if (ObjNIn.ToList().Count + 1 > i)
                        {
                            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br><a  href='GetProfitandLossDetailedview.aspx?GroupId=" + ObjNIn.ToList()[i - 1].GroupId + "' target='_blank' class='gridViewToolTip'>" + ObjNIn.ToList()[i - 1].GroupName + " </a></span></td>");
                            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", ObjNIn.ToList()[i - 1].Value).Replace("Rs.", "").Replace("₹", "") + "</span></td>");


                            NetIncome = NetIncome + Convert.ToDecimal(ObjNIn.ToList()[i - 1].Value);

                        }
                        else
                        {
                            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");

                        }
                    }

                    sb.Append("</tr>");
                }

            }
            else
            {

                for (int i = 0; i < (NexpCount <= NInComeCount ? NInComeCount : NexpCount); i++)
                {
                    sb.Append("<tr>");
                    if (i == 0)
                    {
                        sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>GROSS LOSS</span></td>");
                        sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", GrossLoss).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                        sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");

                        NetExpnces = NetExpnces + GrossLoss;

                        if (ObjNIn.ToList().Count > 0)
                        {
                            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br><a href='GetProfitandLossDetailedview.aspx?GroupId=" + ObjNIn.ToList()[0].GroupId + "' target='_blank' class='gridViewToolTip'>" + ObjNIn.ToList()[0].GroupName + " </a></span></td>");
                            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", ObjNIn.ToList()[0].Value).Replace("Rs.", "").Replace("₹", "") + "</span></td>");

                            NetIncome = NetIncome + Convert.ToDecimal(ObjNIn.ToList()[0].Value);
                        }
                        sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                        sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                    }
                    else
                    {
                        sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br><a href='GetProfitandLossDetailedview.aspx?GroupId=" + ObjNExp.ToList()[i].GroupId + "' target='_blank' class='gridViewToolTip'>" + ObjNExp.ToList()[i].GroupName + " </a></span></td>");
                        sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", ObjNExp.ToList()[i].Value).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                        NetExpnces = NetExpnces + Convert.ToDecimal(ObjNExp.ToList()[i].Value);

                        if (ObjNIn.ToList().Count > i)
                        {
                            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br><a href='GetProfitandLossDetailedview.aspx?GroupId=" + ObjNIn.ToList()[i].GroupId + "' target='_blank' class='gridViewToolTip'>" + ObjNIn.ToList()[i].GroupName + " </a></span></td>");
                            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", ObjNIn.ToList()[i].Value).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                            NetIncome = NetIncome + Convert.ToDecimal(ObjNIn.ToList()[i].Value);

                        }
                        else
                        {
                            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                        }
                    }
                    sb.Append("</tr>");
                }
            }
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 1px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 1px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>Rs.</span></td>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", NetExpnces).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            if (NetExpnces <= NetIncome)
            {
                NetProfit = NetIncome - NetExpnces;
                sb.Append("<tr>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>NET PROFIT</span></td>");
                sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", NetProfit).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                sb.Append("</tr>");
            }
            else
            {
                NetLoss = NetExpnces - NetIncome;
                sb.Append("<tr>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
                sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
                sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>NET LOSS</span></td>");
                sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", NetLoss).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
                sb.Append("</tr>");
            }
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 1px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 1px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>Rs.</span></td>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", Convert.ToDecimal(NetExpnces + NetIncome)).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>Rs.</span></td>");
            sb.Append("<td   height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br>" + string.Format(cultureInfo, "{0:C}", Convert.ToDecimal(NetExpnces + NetIncome)).Replace("Rs.", "").Replace("₹", "") + "</span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 2px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-bottom: 2px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            sb.Append("<tr height='5px'><td colspan='5' width='10px' bgcolor='#B7DEE8'></td></tr>");
            sb.Append("<tr>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-top: 2px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  width='10px' bgcolor='#B7DEE8'></td>");
            sb.Append("<td  height='20' align='left' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("<td  style='border-top: 2px dashed #000000;border-collapse: collapse' height='20' align='right' valign=bottom bgcolor='#B7DEE8'><span style='color:#000000;font-size:small'><br></span></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            //htmlledger.InnerHtml = sb.ToString();
        }
    }
    protected void btncollapse_Click(object sender, EventArgs e)
    {
        tvexp.CollapseAll();
        tvin.CollapseAll();
        tvexpn.CollapseAll();
        tvinn.CollapseAll();
    }
    protected void btnexpand_Click(object sender, EventArgs e)
    {
        tvexp.ExpandAll();
        tvin.ExpandAll();
        tvexpn.ExpandAll();
        tvinn.ExpandAll();
    }
}