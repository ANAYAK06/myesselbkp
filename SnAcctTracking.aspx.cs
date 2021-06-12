using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class SnAcctTracking : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }

    public void fillgrid()
    {
        da = new SqlDataAdapter("sp_GetStatus", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.Fill(ds, "getstatus");
        grdstatus.DataSource = ds.Tables["getstatus"];
        grdstatus.DataBind();

    }
    protected void grdstatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text != "0")  //Sr Accountant
            {
                    if (e.Row.Cells[1].Text == "DCA Budget Amend")
                        e.Row.Cells[2].Attributes.Add("onclick", "OpenNewPage('SrAccountant','DCA Budget Amend')");

                    if (e.Row.Cells[1].Text == "PF Payment")
                        e.Row.Cells[2].Attributes.Add("onclick", "OpenNewPage('SrAccountant','PF Payment')");

            }
            if (e.Row.Cells[3].Text != "0")  //Project Manager
            {
                if (e.Row.Cells[1].Text == "ClientPO")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','ClientPO')");
                if (e.Row.Cells[1].Text == "AmendClientPO")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','AmendClientPO')");
                if (e.Row.Cells[1].Text == "SPPO")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','SPPO')");
                if (e.Row.Cells[1].Text == "AmendSPPO")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','AmendSPPO')");
                if (e.Row.Cells[1].Text == "Supplier Invoice")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','Supplier Invoice')");
                if(e.Row.Cells[1].Text=="SPPOClosed")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','SPPOClosed')");

                if (e.Row.Cells[1].Text == "CC Budget Assign")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','CC Budget Assign')");
                if (e.Row.Cells[1].Text == "CC Budget Amend")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','CC Budget Amend')");
                if (e.Row.Cells[1].Text == "DCA Budget Assign")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','DCA Budget Assign')");
                if (e.Row.Cells[1].Text == "DCA Budget Amend")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','DCA Budget Amend')");
                if (e.Row.Cells[1].Text == "SP Invoice")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','SP Invoice')");
                if (e.Row.Cells[1].Text == "Indent")
                    e.Row.Cells[3].Attributes.Add("onclick", "OpenNewPage('PM','Indent')");

            }
            if (e.Row.Cells[4].Text != "0") //CSK
            {
                //if (e.Row.Cells[1].Text == "Amend Client PO")
                //    e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('SAdmin','AmendClientPO')");
                if (e.Row.Cells[1].Text == "SPPO")
                    e.Row.Cells[4].Attributes.Add("onclick", "OpenNewPage('CSK','SPPO')");
                if (e.Row.Cells[1].Text == "AmendSPPO")
                    e.Row.Cells[4].Attributes.Add("onclick", "OpenNewPage('CSK','AmendSPPO')");
                if (e.Row.Cells[1].Text == "GeneralInvoice")
                    e.Row.Cells[4].Attributes.Add("onclick", "OpenNewPage('CSK','GeneralInvoice')");
                if (e.Row.Cells[1].Text == "Supplier Invoice")
                    e.Row.Cells[4].Attributes.Add("onclick", "OpenNewPage('CSK','Supplier Invoice')");
                if (e.Row.Cells[1].Text == "Supplier Invoice")
                    e.Row.Cells[4].Attributes.Add("onclick", "OpenNewPage('CSK','Supplier Invoice')");
                if (e.Row.Cells[1].Text == "Indent")
                    e.Row.Cells[4].Attributes.Add("onclick", "OpenNewPage('CSK','Indent')");
            }
            if (e.Row.Cells[5].Text != "0") //PUM
            {
                if (e.Row.Cells[1].Text == "SPPO")
                    e.Row.Cells[5].Attributes.Add("onclick", "OpenNewPage('PUM','SPPO')");
                if (e.Row.Cells[1].Text == "AmendSPPO")
                    e.Row.Cells[5].Attributes.Add("onclick", "OpenNewPage('PUM','AmendSPPO')");
                if (e.Row.Cells[1].Text == "Supplier Invoice")
                    e.Row.Cells[5].Attributes.Add("onclick", "OpenNewPage('PUM','Supplier Invoice')");
                if (e.Row.Cells[1].Text == "Indent")
                    e.Row.Cells[5].Attributes.Add("onclick", "OpenNewPage('PUM','Indent')");
            }
            if (e.Row.Cells[6].Text != "0")  //CMC
            {
                if (e.Row.Cells[1].Text == "SPPO")
                    e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('CMC','SPPO')");
                if (e.Row.Cells[1].Text == "AmendSPPO")
                    e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('CMC','AmendSPPO')");
                if (e.Row.Cells[1].Text == "Bank")
                    e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('CMC','GeneralInvoice')");
                if (e.Row.Cells[1].Text == "Supplier Invoice")
                    e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('CMC','Supplier Invoice')");
                if (e.Row.Cells[1].Text == "SP Invoice")
                    e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('CMC','SP Invoice')");
                if (e.Row.Cells[1].Text == "Indent")
                    e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('CMC','Indent')");
            }

            if (e.Row.Cells[7].Text != "0")  //HoAdmin
            {
                if (e.Row.Cells[1].Text == "ClientPO")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','ClientPO')");
                if (e.Row.Cells[1].Text == "Amend Client PO")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','AmendClientPO')");
                if (e.Row.Cells[1].Text == "SPPO")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','SPPO')");
                if (e.Row.Cells[1].Text == "AmendSPPO")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','AmendSPPO')");

                if (e.Row.Cells[1].Text == "GeneralInvoice")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','GeneralInvoice')");

                if (e.Row.Cells[1].Text == "Supplier Invoice")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','Supplier Invoice')");

                if (e.Row.Cells[1].Text == "SPPOClosed")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','SPPOClosed')");

                if (e.Row.Cells[1].Text == "CC Budget Assign")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','CC Budget Assign')");

                if (e.Row.Cells[1].Text == "CC Budget Amend")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','CC Budget Amend')");

                if (e.Row.Cells[1].Text == "DCA Budget Assign")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','DCA Budget Assign')");

                if (e.Row.Cells[1].Text == "SP Invoice")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','SP Invoice')");

                if(e.Row.Cells[1].Text=="DCA Budget Amend")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','DCA Budget Amend')");

                if (e.Row.Cells[1].Text == "Term Loan Payment")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','Term Loan Payment')");

                if (e.Row.Cells[1].Text == "PF Payment")
                    e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('HoAdmin','PF Payment')");

                if (e.Row.Cells[1].Text == "Bank Payment")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','Bank Payment')");

                if (e.Row.Cells[1].Text == "Salary Payment")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','Salary Payment')");

                if (e.Row.Cells[1].Text == "NPCC DCA Budget Amend")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','NPCC DCA Budget Amend')");

                if (e.Row.Cells[1].Text == "Bank Payments")
                    e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','Bank Payments')");

                 if (e.Row.Cells[1].Text == "Bank Receipt")
                     e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('HoAdmin','Bank Receipt')");

            }
            if (e.Row.Cells[8].Text != "0") //SAdmin
            {
                if (e.Row.Cells[1].Text == "SPPO")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','SPPO')");
                if (e.Row.Cells[1].Text == "AmendSPPO")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','AmendSPPO')");
                if (e.Row.Cells[1].Text == "GeneralInvoice")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','GeneralInvoice')");
                if (e.Row.Cells[1].Text == "Supplier Invoice")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','Supplier Invoice')");
                if (e.Row.Cells[1].Text == "SPPOClosed")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','SPPOClosed')");
                if (e.Row.Cells[1].Text == "CC Budget Assign")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','CC Budget Assign')");
                if (e.Row.Cells[1].Text == "CC Budget Amend")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','CC Budget Amend')");
                if (e.Row.Cells[1].Text == "DCA Budget Assign")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','DCA Budget Assign')");
                if (e.Row.Cells[1].Text == "SP Invoice")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','SP Invoice')");
                if (e.Row.Cells[1].Text == "DCA Budget Amend")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','DCA Budget Amend')");

                if (e.Row.Cells[1].Text == "Term Loan Payment")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','Term Loan Payment')");

                if (e.Row.Cells[1].Text == "PF Payment")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','PF Payment')");

                if (e.Row.Cells[1].Text == "Bank Payment")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','Bank Payment')");

                if (e.Row.Cells[1].Text == "Salary Payment")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','Salary Payment')");

                if (e.Row.Cells[1].Text == "NPCC DCA Budget Amend")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','NPCC DCA Budget Amend')");

                if (e.Row.Cells[1].Text == "Bank Payments")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','Bank Payments')");
                if (e.Row.Cells[1].Text == "Bank Receipt")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','Bank Receipt')");
                if (e.Row.Cells[1].Text == "Indent")
                    e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('SAdmin','Indent')");
            }
            if (e.Row.Cells[9].Text != "0") //CMD
            {
                if (e.Row.Cells[1].Text == "CC Budget Amend")
                    e.Row.Cells[9].Attributes.Add("onclick", "OpenNewPage('CMD','CC Budget Amend')");
                if (e.Row.Cells[1].Text == "CC Budget Assign")
                    e.Row.Cells[9].Attributes.Add("onclick", "OpenNewPage('CMD','CC Budget Assign')");
            }
        
        }
    }
}