using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Text;
public partial class GetProfitandLossDetailedview : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        {
            Loaddatatotreeview();
        }
    }
    private void Loaddatatotreeview()
    {
        IEnumerable<DataRow> results;
        if (Session["ViewP&L"] != null)

            results = (from a in (Session["ViewP&L"] as DataTable).AsEnumerable() where a.Field<string>("GroupId") == Request.QueryString["GroupId"] orderby a.Field<string>("ParentID") ascending select a);
        else
            results = (from a in (Session["BalSheet"] as DataTable).AsEnumerable() where a.Field<string>("GroupId") == Request.QueryString["GroupId"] orderby a.Field<string>("ParentID") ascending select a);

        var parentid = (from myrow in results.AsEnumerable()
                        select myrow.ItemArray[4]).FirstOrDefault();

        if (results.ToList().Count > 0)
        {
            DataTable Objdt = results.CopyToDataTable();
            //string Path = Server.MapPath("~/App_Data/P&LDetailview_Fomat.htm");

            //string text = System.IO.File.ReadAllText(Path);
            CreateTreeViewDataTable(Objdt, Convert.ToString(parentid), null);
        }
    }
    
    private void CreateTreeViewDataTable(DataTable dt, string parentId, TreeNode parentNode)
    {
        var cultureInfo = new CultureInfo("en-IN");

        DataRow[] drs = dt.Select("ParentID = '" + parentId + "'");
      
        int j = 0;
        foreach (DataRow i in drs)
        {
            StringBuilder sb = new StringBuilder();
            Decimal Value = 0;
            //var newNode = new TreeNode(i["SubGroupName"].ToString() + "     " + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", ""), i["SubGroupId"].ToString());

            if (i["LedgerID"].ToString() == "0")
            {
                var ObjExp = (from a in (dt as DataTable).AsEnumerable() where a.Field<string>("ParentID") == i["SubGroupId"].ToString() group a by a.Field<string>("ParentID") into g select new { SubGroupId = g.Key, SubGroupName = g.First().ItemArray[3], Value = g.Sum(x => Convert.ToDecimal(x.ItemArray[8])) });
               
                Value = ObjExp.ToList()[0].Value;
            }
            if (parentNode == null)
            {
                //sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='650' style='border-collapse: collapse;table-layout: fixed;'><col width='532' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='118' style='mso-width-source: userset; mso-width-alt: 4169; '>");

                if (i["LedgerID"].ToString() == "0")
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='650' style='border-collapse: collapse;table-layout: fixed;'><col width='532' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='118' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td  height=20 class='xl6320676' style='height:15.0pt;'>" + i["SubGroupName"].ToString() + "</td><td class='xl6320676' style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='512' style='border-collapse: collapse;table-layout: fixed;'><col width='412' style='mso-width-source: userset; mso-width-alt: 20150; '><col width='100' style='mso-width-source: userset; mso-width-alt: 4169; '>");
                    sb.Append("<tr height=20 style='height:15.0pt'><td height=20 class='xl6320676' style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year=" + Session["Year"].ToString() + "&Year1=" + Session["Year1"].ToString() + "' target='_blank'>" + i["LedgerName"].ToString() + "</a></span></td><td class='xl6320676'  style='border-left:none' align='right'>" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }


                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                tvresult.Nodes.Add(newNode);
                CreateTreeViewDataTable(dt, Convert.ToString(i["SubGroupId"]), newNode);
            }
            else
            {
                if (i["LedgerID"].ToString() == "0")
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='650' ><col width='532' ><col width='118'>");

                    sb.Append("<tr height=20 style='height:15.0pt'><td   height=20 class='xl6320676' style='height:15.0pt;'>" + i["SubGroupName"].ToString() + "</td><td class='xl6320676' style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", Value).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }
                else
                {
                    sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='512' ><col width='412' ><col width='100'>");

                    sb.Append("<tr height=20 style='height:15.0pt'><td   height=20 class='xl6320676' style='height:15.0pt;'><span style='color:#000000;font-size:small'><a href='ViewLedgerFromPandL.aspx?LedgerID=" + i["LedgerID"].ToString() + "&Year=" + Session["Year"].ToString() + "&Year1=" + Session["Year1"].ToString() + "' target='_blank'>" + i["LedgerName"].ToString() + "</a></span></td><td class='xl6320676' style='border-left:none' align='right' >" + string.Format(cultureInfo, "{0:C}", i["Value"]).Replace("Rs.", "").Replace("₹", "") + "</td></tr></table>");
                }

                var newNode = new TreeNode(sb.ToString(), i["LedgerID"].ToString());
                parentNode.ChildNodes.Add(newNode);
                CreateTreeViewDataTable(dt, Convert.ToString(i["SubGroupId"]), newNode);

            }
           
           
        }

      

        tvresult.CollapseAll();

    }
}