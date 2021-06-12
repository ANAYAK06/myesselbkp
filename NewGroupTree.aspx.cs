using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class NewGroupTree : System.Web.UI.Page
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
        cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "sp_treeview";
        cmd.Connection = con;
        con.Open();
        var reader = cmd.ExecuteReader();
        var myTable = new DataTable("myTable");
        myTable.Columns.Add("id", typeof(string));
        myTable.Columns.Add("name", typeof(string));
        myTable.Columns.Add("parentid", typeof(string));
        while (reader.Read())
        {
            myTable.Rows.Add(new[]
            {
                reader["id"].ToString(),
            reader["name"].ToString(),
            reader["parentid"].ToString()
                                   
            });
        }
        myTable.AcceptChanges();
        ds = new DataSet();
        ds.Tables.Add(myTable);
        ds.AcceptChanges();
        var parentid = (from myrow in ds.Tables[0].AsEnumerable()
                        select myrow["parentid"]).FirstOrDefault();

        CreateTreeViewDataTable(ds.Tables[0], Convert.ToString(parentid), null);
        cmd.Dispose();
        con.Close();
    }
    private void CreateTreeViewDataTable(DataTable dt, string parentId, TreeNode parentNode)
    {
        DataRow[] drs=dt.Select(string.Format("parentid ='{0}'",parentId));
        foreach (DataRow i in drs)
        {
            var newNode = new TreeNode(i["name"].ToString(), i["id"].ToString());
            if (parentNode == null)
            {
                tvresult.Nodes.Add(newNode);
            }
            else
            {
                parentNode.ChildNodes.Add(newNode);
            }
            CreateTreeViewDataTable(dt, Convert.ToString(i["id"]), newNode);
        }
        tvresult.ExpandAll();
        
    }
}