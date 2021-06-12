using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class ViewStockReconcilationReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
       divGvsemiassets.Visible = false;
       tblGvconsumable.Visible = false;
       tblassets.Visible = false;        
        

    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        da = new SqlDataAdapter("StockReconcilation_sp", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = ddltype.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@CCCode", SqlDbType.VarChar).Value = ddlcccode.SelectedValue;
        da.SelectCommand.Parameters.AddWithValue("@User", SqlDbType.VarChar).Value = Session["user"].ToString();
        da.Fill(ds, "report");
        if (ddltype.SelectedValue == "0")
        {
            Gvselectall.Visible = true;
            Gvsemiassets.Visible = false;
            Gvconsumable.Visible = false;
            Gvassets.Visible = false;
            divGvsemiassets.Visible = false;
            tblGvconsumable.Visible = false;
            if (ds.Tables["report"].Rows.Count > 0)
            {
                Gvselectall.DataSource = ds.Tables["report"];
                Gvselectall.DataBind();
                //ScriptManager.RegisterStartupScript(Page, GetType(), "Javascript", "gridviewScroll();", true);
                calculateall();
                Label10.Visible = true;
            }
            else
            {
                Label10.Visible = false;
                Gvselectall.DataSource = null;
                Gvselectall.DataBind();
            }
        }
        else if (ddltype.SelectedValue == "2")
        {
            Gvselectall.Visible = false;
            Gvsemiassets.Visible = true;
            Gvconsumable.Visible = false;
            Gvassets.Visible = false;
            Label10.Visible = false;
            
            tblGvconsumable.Visible = false;
            Gvsemiassets.DataSource = ds.Tables["report"];
            Gvsemiassets.DataBind();
            if (ds.Tables["report"].Rows.Count > 0)
            {
                calculationsemiassets();
                divGvsemiassets.Visible = true;
            }
            else
            {
                divGvsemiassets.Visible = false;
            }
        }
        else if (ddltype.SelectedValue == "3" || ddltype.SelectedValue == "4")
        {
            Gvselectall.Visible = false;
            Gvsemiassets.Visible = false;
            Gvconsumable.Visible = true;
            Gvassets.Visible = false;
            divGvsemiassets.Visible = false;
            Label10.Visible = false;
           
            Gvconsumable.DataSource = ds.Tables["report"];
            Gvconsumable.DataBind();
            if (ds.Tables["report"].Rows.Count > 0)
            {
                calculationconsumable();
                tblGvconsumable.Visible = true;
            }
            else
            {
                tblGvconsumable.Visible = false;
            }
        }
        else if (ddltype.SelectedValue == "1")
        {
            Gvselectall.Visible = false;
            Gvsemiassets.Visible = false;
            Gvconsumable.Visible = false;
            Gvassets.Visible = true;
            tblassets.Visible = true;
            divGvsemiassets.Visible = false;
            tblGvconsumable.Visible = false;
            Label10.Visible = false;
            Gvassets.DataSource = ds.Tables["report"];
            Gvassets.DataBind();
            if (ds.Tables["report"].Rows.Count > 0)
            calculationassets();            
        }
    }

    private decimal balancestockatallcc = (decimal)0.0;
    protected void Gvselectall_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow gvRow = e.Row;
        if (gvRow.RowType == DataControlRowType.Header)
        {          
            GridViewRow gvrow = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell cell0 = new TableCell();
            cell0.Text = "Item Code";
            cell0.HorizontalAlign = HorizontalAlign.Center;
            cell0.ColumnSpan = 1;
            cell0.RowSpan = 2;
          
            TableCell cell1 = new TableCell();
            cell1.Text = "Item Name";
            cell1.HorizontalAlign = HorizontalAlign.Center;
            cell1.ColumnSpan = 1;
            cell1.RowSpan = 2;

            TableCell cell2 = new TableCell();
            cell2.Text = "Specification";
            cell2.HorizontalAlign = HorizontalAlign.Center;
            cell2.ColumnSpan = 1;
            cell2.RowSpan = 2;

            TableCell cell3 = new TableCell();
            cell3.Text = "BasicPrice";
            cell3.HorizontalAlign = HorizontalAlign.Center;
            cell3.ColumnSpan = 1;
            cell3.RowSpan = 2;

            TableCell units = new TableCell();
            units.Text = "Units";
            units.HorizontalAlign = HorizontalAlign.Center;
            units.ColumnSpan = 1;
            units.RowSpan = 2;

            TableCell cell4 = new TableCell();
            cell4.Text = "TOTAL RECEIPT AT CC";
            cell4.HorizontalAlign = HorizontalAlign.Center;
            cell4.ColumnSpan = 4;
            cell4.RowSpan = 1;

            TableCell cell5 = new TableCell();
            cell5.Text = "TOTAL  OUT FROM CC";
            cell5.HorizontalAlign = HorizontalAlign.Center;
            cell5.ColumnSpan = 5;
            cell5.RowSpan = 1;

            TableCell cell6 = new TableCell();
            cell6.Text = "Balance Stock at CC";
            cell6.HorizontalAlign = HorizontalAlign.Center;
            cell6.ColumnSpan = 1;
            cell6.RowSpan = 2;

            TableCell cell7 = new TableCell();
            cell7.Text = "Amount Of Consumed at CC";
            cell7.HorizontalAlign = HorizontalAlign.Center;
            cell7.ColumnSpan = 1;
            cell7.RowSpan = 2;

            TableCell cell8 = new TableCell();
            cell8.Text = "Balance Stock Amt at CC";
            cell8.HorizontalAlign = HorizontalAlign.Center;
            cell8.ColumnSpan = 1;
            cell8.RowSpan = 2;

            TableCell cell9 = new TableCell();
            cell9.Text = "Amount Of Damage";
            cell9.HorizontalAlign = HorizontalAlign.Center;
            cell9.ColumnSpan = 1;
            cell9.RowSpan = 2;

            TableCell cell10 = new TableCell();
            cell10.Text = "Comments On Balance";
            cell10.HorizontalAlign = HorizontalAlign.Center;
            cell10.ColumnSpan = 1;
            cell10.RowSpan = 2;

            TableCell cell11 = new TableCell();
            cell11.Text = "CC Store Status";
            cell11.HorizontalAlign = HorizontalAlign.Center;
            cell11.ColumnSpan = 1;
            cell11.RowSpan = 2;


            gvrow.Cells.Add(cell0);
            gvrow.Cells.Add(cell1);
            gvrow.Cells.Add(cell2);
            gvrow.Cells.Add(cell3);
            gvrow.Cells.Add(units);
            gvrow.Cells.Add(cell4);
            gvrow.Cells.Add(cell5);
            gvrow.Cells.Add(cell6);
            gvrow.Cells.Add(cell7);
            gvrow.Cells.Add(cell8);
            gvrow.Cells.Add(cell9);
            gvrow.Cells.Add(cell10);
            gvrow.Cells.Add(cell11);
            Gvselectall.Controls[0].Controls.AddAt(0, gvrow);
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = true;
            e.Row.Cells[6].Visible = true;
            e.Row.Cells[7].Visible = true;
            e.Row.Cells[8].Visible = true;
            e.Row.Cells[9].Visible = true;
            e.Row.Cells[10].Visible = true;
            e.Row.Cells[11].Visible = true;
            e.Row.Cells[12].Visible = true;
            e.Row.Cells[13].Visible = true;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[1].ToolTip = (e.Row.DataItem as DataRowView)["Item_name"].ToString();
            //e.Row.Cells[2].ToolTip = (e.Row.DataItem as DataRowView)["Specification"].ToString();
          
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='lightgrey'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            string cell5 = e.Row.Cells[5].Text;                                                 //Recived From CentralStore
            string cell6 = e.Row.Cells[6].Text;                                                 //Recieved From OtherCC
            string cell7 = e.Row.Cells[7].Text;                                                 //Purchase At CC
            decimal sum = decimal.Parse(cell5) + decimal.Parse(cell6) + decimal.Parse(cell7);   //Recived From CentralStore + Recieved From OtherCC + Purchase At CC
            e.Row.Cells[8].Text = sum.ToString();                                               //Total Recieved at CC

            string cell9 = e.Row.Cells[9].Text;                                                 //Transfer To CentralStore
            string cell10 = e.Row.Cells[10].Text;                                               //Transfer To OtherCC
            string cell11 = e.Row.Cells[11].Text;                                               //Lost & Damages
            string cell12 = e.Row.Cells[12].Text;                                               //Total Out From CC
            decimal totalsum = decimal.Parse(cell9) + decimal.Parse(cell10) + decimal.Parse(cell11) + decimal.Parse(cell12);  //Transfer To CentralStore + Transfer To OtherCC + Lost & Damages + Total Out From CC
            e.Row.Cells[13].Text = totalsum.ToString();                                         //Balance Stock at CC

            decimal balancestock = sum - totalsum;                                              //(Total Recieved at CC)-(Total Out From CC)
            e.Row.Cells[14].Text = balancestock.ToString();                                     //Balance Stock at CC

            string cell3 = e.Row.Cells[3].Text;                                                 //BasicPrice
            decimal amountconsumedatcc = decimal.Parse(cell11) * decimal.Parse(cell3);          //(Issued For CC Consumption)+(BasicPrice)
            e.Row.Cells[15].Text = String.Format("{0:#,##,##,###.00}", amountconsumedatcc);                               //Balance Stock Amt at CC

            decimal balancestockatcc = balancestock * decimal.Parse(cell3);                     //(Balance Stock at CC)*(Total Out From CC)
            e.Row.Cells[16].Text = String.Format("{0:#,##,##,###.00}", balancestockatcc);                                 //Amount Of Damage     

            decimal lostanddamage = decimal.Parse(cell12) * decimal.Parse(cell3);               //(Total Out From CC)*(BasicPrice)
            e.Row.Cells[17].Text = String.Format("{0:#,##,##,###.00}", lostanddamage);         //Amount Of Damage
            balancestockatallcc += Convert.ToDecimal((e.Row.Cells[16].Text));
           
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ViewState["allsummary"] = String.Format("{0:#,##,##,###.00}", balancestockatallcc);            
        }
       
    }   
    protected void Gvsemiassets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region header

        GridViewRow gvRow = e.Row;
        if (gvRow.RowType == DataControlRowType.Header)
        {
            GridViewRow gvrow = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell cell0 = new TableCell();
            cell0.Text = "Item Code";
            cell0.HorizontalAlign = HorizontalAlign.Center;
            cell0.ColumnSpan = 1;
            cell0.RowSpan = 2;

            TableCell cell1 = new TableCell();
            cell1.Text = "Item Name";
            cell1.HorizontalAlign = HorizontalAlign.Center;
            cell1.ColumnSpan = 1;
            cell1.RowSpan = 2;

            TableCell cell2 = new TableCell();
            cell2.Text = "Specification";
            cell2.HorizontalAlign = HorizontalAlign.Center;
            cell2.ColumnSpan = 1;
            cell2.RowSpan = 2;

            TableCell cell3 = new TableCell();
            cell3.Text = "Sub-Dca";
            cell3.HorizontalAlign = HorizontalAlign.Center;
            cell3.ColumnSpan = 1;
            cell3.RowSpan = 2;

            TableCell units = new TableCell();
            units.Text = "Units";
            units.HorizontalAlign = HorizontalAlign.Center;
            units.ColumnSpan = 1;
            units.RowSpan = 2;

            TableCell cell4 = new TableCell();
            cell4.Text = "TOTAL RECEIPT AT CC";
            cell4.HorizontalAlign = HorizontalAlign.Center;
            cell4.ColumnSpan = 4;
            cell4.RowSpan = 1;

            TableCell cell5 = new TableCell();
            cell5.Text = "TOTAL  OUT FROM CC";
            cell5.HorizontalAlign = HorizontalAlign.Center;
            cell5.ColumnSpan = 3;
            cell5.RowSpan = 1;

            TableCell cell6 = new TableCell();
            cell6.Text = "Balance Stock At CC";
            cell6.HorizontalAlign = HorizontalAlign.Center;
            cell6.ColumnSpan = 1;
            cell6.RowSpan = 2;

            TableCell cell7 = new TableCell();
            cell7.Text = "LostDamaged Accepted";
            cell7.HorizontalAlign = HorizontalAlign.Center;
            cell7.ColumnSpan = 1;
            cell7.RowSpan = 2;

            TableCell cell8 = new TableCell();
            cell8.Text = "Basic Price";
            cell8.HorizontalAlign = HorizontalAlign.Center;
            cell8.ColumnSpan = 1;
            cell8.RowSpan = 2;

            TableCell cell9 = new TableCell();
            cell9.Text = "Amount Against TotalRecieved";
            cell9.HorizontalAlign = HorizontalAlign.Center;
            cell9.ColumnSpan = 1;
            cell9.RowSpan = 2;

            TableCell cell10 = new TableCell();
            cell10.Text = "Amt Against Total Returned/Transferd From CC";
            cell10.HorizontalAlign = HorizontalAlign.Center;
            cell10.ColumnSpan = 1;
            cell10.RowSpan = 2;

            TableCell cell11 = new TableCell();
            cell11.Text = "Amt Of Balance Stock At CC";
            cell11.HorizontalAlign = HorizontalAlign.Center;
            cell11.ColumnSpan = 1;
            cell11.RowSpan = 2;

            TableCell cell12 = new TableCell();
            cell12.Text = "Amt Of Lost&Damage";
            cell12.HorizontalAlign = HorizontalAlign.Center;
            cell12.ColumnSpan = 1;
            cell12.RowSpan = 2;

            TableCell cell13 = new TableCell();
            cell13.Text = "Comments On Balance";
            cell13.HorizontalAlign = HorizontalAlign.Center;
            cell13.ColumnSpan = 1;
            cell13.RowSpan = 2;

            TableCell cell14 = new TableCell();
            cell14.Text = "CC Store Status";
            cell14.HorizontalAlign = HorizontalAlign.Center;
            cell14.ColumnSpan = 1;
            cell14.RowSpan = 2;


            gvrow.Cells.Add(cell0);
            gvrow.Cells.Add(cell1);
            gvrow.Cells.Add(cell2);
            gvrow.Cells.Add(cell3);
            gvrow.Cells.Add(units);
            gvrow.Cells.Add(cell4);
            gvrow.Cells.Add(cell5);
            gvrow.Cells.Add(cell6);
            gvrow.Cells.Add(cell7);
            gvrow.Cells.Add(cell8);
            gvrow.Cells.Add(cell9);
            gvrow.Cells.Add(cell10);
            gvrow.Cells.Add(cell11);
            gvrow.Cells.Add(cell12);
            gvrow.Cells.Add(cell13);
            gvrow.Cells.Add(cell14);
            Gvsemiassets.Controls[0].Controls.AddAt(0, gvrow);
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = true;
            e.Row.Cells[6].Visible = true;
            e.Row.Cells[7].Visible = true;
            e.Row.Cells[8].Visible = true;
            e.Row.Cells[9].Visible = true;
            e.Row.Cells[10].Visible = true;
            e.Row.Cells[11].Visible = true;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
        }
        #endregion
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='lightgrey'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            string cell5 = e.Row.Cells[5].Text;                                                 //Recived From CentralStore
            string cell6 = e.Row.Cells[6].Text;                                                 //Recieved From OtherCC
            string cell7 = e.Row.Cells[7].Text;                                                 //Purchase At CC
            decimal sum = decimal.Parse(cell5) + decimal.Parse(cell6) + decimal.Parse(cell7);   //(Recived From CentralStore) + (Recieved From OtherCC) + (Purchase At CC)
            e.Row.Cells[8].Text = sum.ToString();                                               //Total Recieved at CC

            string cell9 = e.Row.Cells[9].Text;                                                 //Transfer To CentralStore
            string cell10 = e.Row.Cells[10].Text;                                               //Transfer To OtherCC
           
            decimal sum1 = decimal.Parse(cell9) + decimal.Parse(cell10);                        //(Transfer To CentralStore)+(Transfer To OtherCC)
            e.Row.Cells[11].Text = sum1.ToString();                                             //Total Out From CC

            string cell13 = e.Row.Cells[13].Text;
            decimal balstock = sum - (sum1 + decimal.Parse(cell13));                                              //(Total Recieved at CC)-(Total Out From CC)
            e.Row.Cells[12].Text = balstock.ToString();                                     //Balance Stock at CC

            string cell14 = e.Row.Cells[14].Text;                                               //BasicPrice
            string cell8 = e.Row.Cells[8].Text;                                                 //Total Recieved at CC
            decimal balancestockatcc = decimal.Parse(cell8) * decimal.Parse(cell14);            //(Total Recieved at CC) * (BasicPrice)
            e.Row.Cells[15].Text = String.Format("{0:#,##,##,###.00}", balancestockatcc);      //BalanceStock at cc

            string cell11 = e.Row.Cells[11].Text;                                               //Total Out From CC
            decimal RetTrans = decimal.Parse(cell11) * decimal.Parse(cell14);                   //(Total Out From CC)*(BasicPrice) 
            e.Row.Cells[16].Text = String.Format("{0:#,##,##,###.00}", RetTrans);               //Amt Against Total Returned/Transferd From CC

            string cell12 = e.Row.Cells[12].Text;                                               //Balance Stock at cc
            decimal balancestock = decimal.Parse(cell12) * decimal.Parse(cell14);               //(Balance Stock at cc)*(BasicPrice)
            e.Row.Cells[17].Text = String.Format("{0:#,##,##,###.00}", balancestock);           //Amt Of Balance Stock At CC

                                                           //LostDamaged Accepted
            decimal LostDamageamt = decimal.Parse(cell13) * decimal.Parse(cell14);              //(LostDamaged Accepted)*(BasicPrice)
            e.Row.Cells[18].Text = String.Format("{0:#,##,##,###.00}", LostDamageamt);          //Amt Of Lost&Damage

            balancestockatccsemi += Convert.ToDecimal((e.Row.Cells[15].Text));
            RetTranssemi += Convert.ToDecimal((e.Row.Cells[16].Text));
            balancestocksemi += Convert.ToDecimal((e.Row.Cells[17].Text));  
            LostDamageamtsemi += Convert.ToDecimal((e.Row.Cells[18].Text));

            string For = "";
            string type = "2";
            if (e.Row.Cells[5].Text != "0")
            {
                For = "RecivedFromCentralStore";
                e.Row.Cells[5].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");
            }
            if (e.Row.Cells[6].Text != "0")
            {
                For = "RECEIVEDFROMOTHERCC";
                e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");
            }
            if (e.Row.Cells[7].Text != "0")
            {
                For = "PURCHASEDATCC";
                e.Row.Cells[7].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }
            if (e.Row.Cells[9].Text != "0")
            {
                For = "TRANSFEREDTOCENTRALSTORE";
                e.Row.Cells[9].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }
            if (e.Row.Cells[10].Text != "0")
            {
                For = "TRANSFERTOOTHERCC";
                e.Row.Cells[10].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }
            if (e.Row.Cells[13].Text != "0")
            {
                For = "LOSTANDDAMAGESREPORTACCPETED";
                e.Row.Cells[13].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[15].Text = String.Format("Rs. {0:#,##,##,###.00}", balancestockatccsemi);
            e.Row.Cells[16].Text = String.Format("Rs. {0:#,##,##,###.00}", RetTranssemi);
            e.Row.Cells[17].Text = String.Format("Rs. {0:#,##,##,###.00}", balancestocksemi);
            e.Row.Cells[18].Text = String.Format("Rs. {0:#,##,##,###.00}", LostDamageamtsemi);
            ViewState["balancestocksemi"] = balancestocksemi;
            ViewState["LostDamageamtsemi"] = LostDamageamtsemi;
        }
    }
    public void calculationsemiassets()
    {
        lblstockbalance.Text = ViewState["balancestocksemi"].ToString();
        lbldamaged.Text = ViewState["LostDamageamtsemi"].ToString();
        lblnetbalancestock.Text = Convert.ToDecimal(Convert.ToDecimal(lblstockbalance.Text) - Convert.ToDecimal(lbldamaged.Text)).ToString();
    }
    private decimal balancestockatccsemi = (decimal)0.0;
    private decimal RetTranssemi = (decimal)0.0;
    private decimal balancestocksemi = (decimal)0.0;
    private decimal LostDamageamtsemi = (decimal)0.0;
    
    
    protected void Gvconsumable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow gvRow = e.Row;
        if (gvRow.RowType == DataControlRowType.Header)
        {
            GridViewRow gvrow = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell cell0 = new TableCell();
            cell0.Text = "Item Code";
            cell0.HorizontalAlign = HorizontalAlign.Center;
            cell0.ColumnSpan = 1;
            cell0.RowSpan = 2;

            TableCell cell1 = new TableCell();
            cell1.Text = "Item Name";
            cell1.HorizontalAlign = HorizontalAlign.Center;
            cell1.ColumnSpan = 1;
            cell1.RowSpan = 2;

            TableCell cell2 = new TableCell();
            cell2.Text = "Specification";
            cell2.HorizontalAlign = HorizontalAlign.Center;
            cell2.ColumnSpan = 1;
            cell2.RowSpan = 2;

            TableCell units = new TableCell();
            units.Text = "Units";
            units.HorizontalAlign = HorizontalAlign.Center;
            units.ColumnSpan = 1;
            units.RowSpan = 2;

            TableCell cell4 = new TableCell();
            cell4.Text = "TOTAL RECEIPT AT CC";
            cell4.HorizontalAlign = HorizontalAlign.Center;
            cell4.ColumnSpan = 4;
            cell4.RowSpan = 1;

            TableCell cell5 = new TableCell();
            cell5.Text = "TOTAL  OUT FROM CC";
            cell5.HorizontalAlign = HorizontalAlign.Center;
            cell5.ColumnSpan = 4;
            cell5.RowSpan = 1;

            TableCell cell6 = new TableCell();
            cell6.Text = "Balance Stock At CC";
            cell6.HorizontalAlign = HorizontalAlign.Center;
            cell6.ColumnSpan = 1;
            cell6.RowSpan = 2;

            TableCell cell7 = new TableCell();
            cell7.Text = "LostDamaged Accepted";
            cell7.HorizontalAlign = HorizontalAlign.Center;
            cell7.ColumnSpan = 1;
            cell7.RowSpan = 2;

            TableCell cell8 = new TableCell();
            cell8.Text = "Basic Price";
            cell8.HorizontalAlign = HorizontalAlign.Center;
            cell8.ColumnSpan = 1;
            cell8.RowSpan = 2;

            //TableCell cell9 = new TableCell();
            //cell9.Text = "Amount Against TotalRecieved";
            //cell9.HorizontalAlign = HorizontalAlign.Center;
            //cell9.ColumnSpan = 1;
            //cell9.RowSpan = 2;

            TableCell cell9 = new TableCell();
            cell9.Text = "Amt Against Total Recieved At CC";
            cell9.HorizontalAlign = HorizontalAlign.Center;
            cell9.ColumnSpan = 1;
            cell9.RowSpan = 2;

            TableCell cell10 = new TableCell();
            cell10.Text = "Amt Of Consumed Items At CC";
            cell10.HorizontalAlign = HorizontalAlign.Center;
            cell10.ColumnSpan = 1;
            cell10.RowSpan = 2;

            TableCell cell11 = new TableCell();
            cell11.Text = "Amt Against Total Transfered To Other CC";
            cell11.HorizontalAlign = HorizontalAlign.Center;
            cell11.ColumnSpan = 1;
            cell11.RowSpan = 2;

            TableCell cell12 = new TableCell();
            cell12.Text = "Amt Of Balance Stock At CC";
            cell12.HorizontalAlign = HorizontalAlign.Center;
            cell12.ColumnSpan = 1;
            cell12.RowSpan = 2;

            TableCell cell13 = new TableCell();
            cell13.Text = "Amt Of Lost And Damage";
            cell13.HorizontalAlign = HorizontalAlign.Center;
            cell13.ColumnSpan = 1;
            cell13.RowSpan = 2;

            TableCell cell14 = new TableCell();
            cell14.Text = "Comments On Balance";
            cell14.HorizontalAlign = HorizontalAlign.Center;
            cell14.ColumnSpan = 1;
            cell14.RowSpan = 2;

            TableCell cell15 = new TableCell();
            cell15.Text = "CC Store Status";
            cell15.HorizontalAlign = HorizontalAlign.Center;
            cell15.ColumnSpan = 1;
            cell15.RowSpan = 2;


            gvrow.Cells.Add(cell0);  //cell0
            gvrow.Cells.Add(cell1);  //cell1
            gvrow.Cells.Add(cell2);  //cell2
            //gvrow.Cells.Add(cell3);
            gvrow.Cells.Add(units);  //cell3
            gvrow.Cells.Add(cell4);  //cell 4,5,6,7
            gvrow.Cells.Add(cell5);  //cell 8,9,10,11
            gvrow.Cells.Add(cell6);  //cell 12
            gvrow.Cells.Add(cell7);  //cell 13
            gvrow.Cells.Add(cell8);  //cell 14
            gvrow.Cells.Add(cell9);  //cell 15
            gvrow.Cells.Add(cell10); //cell 16
            gvrow.Cells.Add(cell11); //cell 17
            gvrow.Cells.Add(cell12); //cell 18
            gvrow.Cells.Add(cell13); //cell 19
            gvrow.Cells.Add(cell14); //cell 20
            gvrow.Cells.Add(cell15); //cell 21
            //gvrow.Cells.Add(cell16);
           
            Gvconsumable.Controls[0].Controls.AddAt(0, gvrow);
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = true;
            e.Row.Cells[5].Visible = true;
            e.Row.Cells[6].Visible = true;
            e.Row.Cells[7].Visible = true;
            e.Row.Cells[8].Visible = true;
            e.Row.Cells[9].Visible = true;
            e.Row.Cells[10].Visible = true;
            e.Row.Cells[11].Visible = true;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='lightgrey'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            string cell4 = e.Row.Cells[4].Text;                                                 //Recived From CentralStore
            string cell5 = e.Row.Cells[5].Text;                                                 //Recieved From OtherCC
            string cell6 = e.Row.Cells[6].Text;                                                 //Purchase At CC
            decimal sum = decimal.Parse(cell4) + decimal.Parse(cell5) + decimal.Parse(cell6);   //Recived From CentralStore + Recieved From OtherCC + Purchase At CC
            e.Row.Cells[7].Text = sum.ToString();                                               //Total Recieved at CC


            string cell8 = e.Row.Cells[8].Text;                                                 //Transfer To CentralStore
            string cell9 = e.Row.Cells[9].Text;                                                 //Transfer To OtherCC
            string cell10 = e.Row.Cells[10].Text;                                               //Issued For CC Consumption
            decimal TotalOutFromCC = decimal.Parse(cell8) + decimal.Parse(cell9) + decimal.Parse(cell10);   //Transfer To CentralStore + Transfer To OtherCC + Issued For CC Consumption
            e.Row.Cells[11].Text = TotalOutFromCC.ToString();                                   //Total Out From CC


            string cell7 = e.Row.Cells[7].Text;                                                 //Total Recieved at CC
            string cell11 = e.Row.Cells[11].Text;
            string cell13 = e.Row.Cells[13].Text; //Total Out From CC
            decimal balancestockatcc = decimal.Parse(cell7) - (decimal.Parse(cell11) + decimal.Parse(cell13));            //Total Recieved at CC - Total Out From CC-lostanddamaged
            e.Row.Cells[12].Text = balancestockatcc.ToString();                                 //Balance Stock at CC

            string cell14 = e.Row.Cells[14].Text;                                               //Basic Price
            decimal AMOUNTAGAINSTTOTALRECEIVEDATCC = decimal.Parse(cell7) * decimal.Parse(cell14); //Total Recieved at CC * Basic Price
            e.Row.Cells[15].Text = String.Format("{0:#,##,##,###.00}", AMOUNTAGAINSTTOTALRECEIVEDATCC); //Amount Against TotalRecieved

            decimal AMOUNTOFCONSUMEDITEMSATCC = decimal.Parse(cell10) * decimal.Parse(cell14);  //Issued For CC Consumption *  Basic Price
            e.Row.Cells[16].Text = String.Format("{0:#,##,##,###.00}", AMOUNTOFCONSUMEDITEMSATCC); //Amt Of Consumed Items At CC

            decimal AMOUNTAGAINSTTOTALTRANSFERREDTOOTHERCC = (decimal.Parse(cell8) + decimal.Parse(cell9)) * decimal.Parse(cell14); //(Transfer To CentralStore + Transfer To OtherCC) * Basic Price
            e.Row.Cells[17].Text = String.Format("{0:#,##,##,###.00}", AMOUNTAGAINSTTOTALTRANSFERREDTOOTHERCC);    //Amt Against Total Transfered To Other CC

            string cell12 = e.Row.Cells[12].Text;                                               //Balance Stock at CC
            decimal AMOUNTOFBALANCESTOCKATCC = decimal.Parse(cell12) * decimal.Parse(cell14);   //Balance Stock at CC * Basic Price
            e.Row.Cells[18].Text = String.Format("{0:#,##,##,###.00}", AMOUNTOFBALANCESTOCKATCC);//Amt Of Balance Stock At CC

            //string cell13 = e.Row.Cells[13].Text;                                               //LostDamaged Accepted
            decimal AMOUNTOFLOSTANDDAMAGE = decimal.Parse(cell13) * decimal.Parse(cell14);      //LostDamaged Accepted * Basic Price
            e.Row.Cells[19].Text = String.Format("{0:#,##,##,###.00}", AMOUNTOFLOSTANDDAMAGE);  //Amt Of Lost And Damage

            AMOUNTAGAINSTTOTALRECEIVEDATCC_CON += Convert.ToDecimal((e.Row.Cells[15].Text));
            AMOUNTOFCONSUMEDITEMSATCC_CON += Convert.ToDecimal((e.Row.Cells[16].Text));
            AMOUNTAGAINSTTOTALTRANSFERREDTOOTHERCC_CON += Convert.ToDecimal((e.Row.Cells[17].Text));
            AMOUNTOFBALANCESTOCKATCC_CON += Convert.ToDecimal((e.Row.Cells[18].Text));
            AMOUNTOFLOSTANDDAMAGE_CON += Convert.ToDecimal((e.Row.Cells[19].Text));

            string For = "";
            string type = "3";
            if (e.Row.Cells[4].Text != "0")
            {
                For = "RecivedFromCentralStore";
                e.Row.Cells[4].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");
            }
            if (e.Row.Cells[5].Text != "0")
            {
                For = "RECEIVEDFROMOTHERCC";
                e.Row.Cells[5].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");
            }
            if (e.Row.Cells[6].Text != "0")
            {
                For = "PURCHASEDATCC";
                e.Row.Cells[6].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }
            if (e.Row.Cells[8].Text != "0")
            {
                For = "TRANSFEREDTOCENTRALSTORE";
                e.Row.Cells[8].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }
            if (e.Row.Cells[9].Text != "0")
            {
                For = "TRANSFERTOOTHERCC";
                e.Row.Cells[9].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }
            if (e.Row.Cells[10].Text != "0")
            {
                For = "IssuedForCCConsumption";
                e.Row.Cells[10].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }
            if (e.Row.Cells[13].Text != "0")
            {
                For = "LOSTANDDAMAGESREPORTACCPETED";
                e.Row.Cells[13].Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

            }
            Gvconsumable.Columns[19].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[15].Text = String.Format("Rs. {0:#,##,##,###.00}", AMOUNTAGAINSTTOTALRECEIVEDATCC_CON);
            e.Row.Cells[16].Text = String.Format("Rs. {0:#,##,##,###.00}", AMOUNTOFCONSUMEDITEMSATCC_CON);
            e.Row.Cells[17].Text = String.Format("Rs. {0:#,##,##,###.00}", AMOUNTAGAINSTTOTALTRANSFERREDTOOTHERCC_CON);
            e.Row.Cells[18].Text = String.Format("Rs. {0:#,##,##,###.00}", AMOUNTOFBALANCESTOCKATCC_CON);
            e.Row.Cells[19].Text = String.Format("Rs. {0:#,##,##,###.00}", AMOUNTOFLOSTANDDAMAGE_CON);
            ViewState["balancestocksemi"] = e.Row.Cells[15].Text;
            ViewState["lblconsumedatcc"] = e.Row.Cells[16].Text;
            ViewState["lbltransfered"] = e.Row.Cells[17].Text;
            ViewState["lblbalancestock"] = e.Row.Cells[18].Text;
            ViewState["lbllostdamaged"] = e.Row.Cells[19].Text;
        }
    }

    public void calculationconsumable()
    {
        lbltotalrecieved.Text = ViewState["balancestocksemi"].ToString();
        lblconsumedatcc.Text = ViewState["lblconsumedatcc"].ToString();
        lbltransfered.Text = ViewState["lbltransfered"].ToString();
        lblbalancestock.Text = ViewState["lblbalancestock"].ToString();
        lbllostdamaged.Text = ViewState["lbllostdamaged"].ToString();
    }
    private decimal AMOUNTAGAINSTTOTALRECEIVEDATCC_CON = (decimal)0.0;
    private decimal AMOUNTOFCONSUMEDITEMSATCC_CON = (decimal)0.0;
    private decimal AMOUNTAGAINSTTOTALTRANSFERREDTOOTHERCC_CON = (decimal)0.0;
    private decimal AMOUNTOFBALANCESTOCKATCC_CON = (decimal)0.0;
    private decimal AMOUNTOFLOSTANDDAMAGE_CON = (decimal)0.0;

    protected void Gvassets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         GridViewRow gvRow = e.Row;
         if (gvRow.RowType == DataControlRowType.Header)
         {
             GridViewRow gvrow = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
             TableCell cell0 = new TableCell();
             cell0.Text = "Item Code";
             cell0.HorizontalAlign = HorizontalAlign.Center;
             cell0.ColumnSpan = 1;
             cell0.RowSpan = 2;

             TableCell cell1 = new TableCell();
             cell1.Text = "Item Name";
             cell1.HorizontalAlign = HorizontalAlign.Center;
             cell1.ColumnSpan = 1;
             cell1.RowSpan = 2;

             TableCell cell2 = new TableCell();
             cell2.Text = "Specification";
             cell2.HorizontalAlign = HorizontalAlign.Center;
             cell2.ColumnSpan = 1;
             cell2.RowSpan = 2;

             TableCell cell3 = new TableCell();
             cell3.Text = "Price In The System";
             cell3.HorizontalAlign = HorizontalAlign.Center;
             cell3.ColumnSpan = 1;
             cell3.RowSpan = 2;

             TableCell cell4 = new TableCell();
             cell4.Text = "Quantity";
             cell4.HorizontalAlign = HorizontalAlign.Center;
             cell4.ColumnSpan = 1;
             cell4.RowSpan = 2;

             TableCell cell5 = new TableCell();
             cell5.Text = "Total Reciept At CC";
             cell5.HorizontalAlign = HorizontalAlign.Center;
             cell5.ColumnSpan = 4;
             cell5.RowSpan = 1;

             TableCell cell6 = new TableCell();
             cell6.Text = "CC Store Status";
             cell6.HorizontalAlign = HorizontalAlign.Center;
             cell6.ColumnSpan =1;
             cell6.RowSpan = 2;

             gvrow.Cells.Add(cell0);
             gvrow.Cells.Add(cell1);
             gvrow.Cells.Add(cell2);
             gvrow.Cells.Add(cell3);
             gvrow.Cells.Add(cell4);
             gvrow.Cells.Add(cell5);
             gvrow.Cells.Add(cell6);


             Gvassets.Controls[0].Controls.AddAt(0, gvrow);
             e.Row.Cells[0].Visible = false;
             e.Row.Cells[1].Visible = false;
             e.Row.Cells[2].Visible = false;
             e.Row.Cells[3].Visible = false;
             e.Row.Cells[4].Visible = false;
             e.Row.Cells[5].Visible = true;
             e.Row.Cells[6].Visible = true;
             e.Row.Cells[7].Visible = true;
             e.Row.Cells[8].Visible = true;
             e.Row.Cells[9].Visible = false;
            
         }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='lightgrey'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            if (e.Row.Cells[6].Text != "&nbsp;")
            {
                e.Row.Cells[7].Text = "0";
               
            }
            else
            {
                e.Row.Cells[7].Text = "1";
                AssetCost += Convert.ToDecimal((e.Row.Cells[3].Text));
            }
            string type = "1";
            string For = "";
            e.Row.Attributes.Add("onclick", "OpenNewPage('" + e.Row.Cells[0].Text + "','" + type.ToString() + "','" + For.ToString() + "','" + ddlcccode.SelectedValue + "')");

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = String.Format("Rs. {0:#,##,##,###.00}", AssetCost);
            ViewState["assetamt"] = e.Row.Cells[3].Text;
        }

    }
    private decimal AssetCost = (decimal)0.0;
    public void calculationassets()
    {
        lblassets.Text = "RECONCILAITION STATUS ASSET COSTING  " + ViewState["assetamt"].ToString() + "  STILL PENDING AT CC";
    }
    public void calculateall()
    {
        Label10.Text = "AMOUNT OF MATERIAL STOCK AT SITE  IS " + ViewState["allsummary"].ToString() + " ";
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", Label4.Text + " Report "));
        Context.Response.Charset = "";
        // string headerrow = @" <table> <tr > <td align='center' colspan='10'><u><h2>CASH /BANK DEBIT DETAIL OF THE DAY<h2></u> </td> </tr></table>";
        string headerrow = @" <table><tr><td colspan='2'></td><td align='center' colspan='3'><h3> " + Label4.Text + " Detailview Report" + " <h3></td><td align='left' colspan='3'></td></tr></table> ";
        string headerow3 = @" <table> <tr > <td align='center' colspan='10'> </td> </tr></table>";

        Response.Write(headerrow);
        //Response.Write(tablerow1);
        Response.Write(headerow3);
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        trreport.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
    
}