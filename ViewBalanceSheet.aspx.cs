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

public partial class ViewBalanceSheet : System.Web.UI.Page
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
            btncollapse.Visible = false;
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
           
            tvexpn.Nodes.Clear();
            tvinn.Nodes.Clear();
            
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
           
            //var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
            da = new SqlDataAdapter("Sp_GetProfitandLossreport", con);

            da.SelectCommand.CommandTimeout = 120;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Year", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue)).ToString();
            da.SelectCommand.Parameters.AddWithValue("@Year1", SqlDbType.VarChar).Value = (Convert.ToInt32(ddlyear.SelectedValue) + 1).ToString();
            da.SelectCommand.Parameters.AddWithValue("@ReportType", SqlDbType.VarChar).Value = "BalanceSheet";

            da.Fill(ds, "BalSheet");

            if (ds.Tables["BalSheet"].Rows.Count > 0 || ds.Tables["BalSheet1"].Rows.Count > 0 || ds.Tables["BalSheet2"].Rows.Count > 0 || ds.Tables["BalSheet3"].Rows.Count > 0)
            {
                tblbody.Visible = true;
                btncollapse.Visible = true;
                btnexpand.Visible = true;
                Session["BalSheet"] = ds.Tables["BalSheet"];
                
               
                if (ds.Tables[3].Rows.Count > 0)
                {

                    var readerN20N = ds.Tables[3].CreateDataReader();
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
                if (ds.Tables[2].Rows.Count > 0)
                {

                    var readerN30N = ds.Tables[2].CreateDataReader();
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
                if (ds.Tables[4].Rows[0]["NetProfit"].ToString()!="0.00000")
                {

                    SpnNetProfit.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", ds.Tables[4].Rows[0]["NetProfit"].ToString()).Replace("Rs.", "").Replace("₹", "");
                    SpnTotalNetProfit.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", NetExpnces + Convert.ToDecimal(ds.Tables[4].Rows[0]["NetProfit"].ToString())).Replace("Rs.", "").Replace("₹", "");
                    SpnTotalNetLoss.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}",  SpnTotalNetIn).Replace("Rs.", "").Replace("₹", "");
                    spnnetprofittext.Visible = true;
                    spnnetlosstext.Visible = false;
                    SpnNetProfit.Visible = true;
                    SpnNetLoss.Visible = false;
                }
                else
                {

                    SpnNetLoss.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", ds.Tables[4].Rows[0]["NetLoss"].ToString()).Replace("Rs.", "").Replace("₹", "");
                    SpnTotalNetLoss.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", NetIncome + Convert.ToDecimal(ds.Tables[4].Rows[0]["NetLoss"].ToString())).Replace("Rs.", "").Replace("₹", "");
                    SpnTotalNetProfit.InnerText = "Rs. " + string.Format(cultureInfo, "{0:C}", NetExpnces.ToString()).Replace("Rs.", "").Replace("₹", "");
                    spnnetprofittext.Visible = false;
                    spnnetlosstext.Visible = true;
                    SpnNetProfit.Visible = false;
                    SpnNetLoss.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    #region Bind Tree views
   
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
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=10 width:'100' style='height:15.0pt;overflow-wrap:break-word;'><span style='color:#000000;font-size:x-small;word-wrap: break-all'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year=" + Year + "&Year1=" + Year1 + "' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
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
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 style='height:15.0pt;overflow-wrap:break-word;'>" + i["name"].ToString() + "</td><td  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


                    //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='100' ><col width='100' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                    //sb.Append("<tr height=20 style='height:15.0pt'><td   height=20  style='height:15.0pt;'>" + i["SubGroupName"].ToString() + "</td><td  style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='150' style='border-collapse: collapse;table-layout: fixed;border-color:transparent'><col width='150' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='150' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=10 width:'100'  style='height:15.0pt;'><span style='color:#000000;font-size:x-small;word-wrap: break-all'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year=" + Year + "&Year1=" + Year1 + "' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");




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
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:x-small;word-wrap: break-all'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year=" + Year + "&Year1=" + Year1 + "' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
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
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20  style='height:15.0pt;'><span style='color:#000000;font-size:x-small;word-wrap: break-all'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year=" + Year + "&Year1=" + Year1 + "' target='_blank'>" + i["name"].ToString() + "</a></span></td><td   style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");


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
    protected void btncollapse_Click(object sender, EventArgs e)
    {
       
        tvexpn.CollapseAll();
        tvinn.CollapseAll();
    }
    protected void btnexpand_Click(object sender, EventArgs e)
    {
       
        tvexpn.ExpandAll();
        tvinn.ExpandAll();
    }
}