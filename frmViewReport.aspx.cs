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

public partial class Accountant_frmViewReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet ds = new DataSet();     

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("SessionExpire.aspx");
        }
        if (Request.QueryString["Voucherid"] == null)
        {
            JavaScript.CloseWindow();
        }
        else
        {
            LoadReport();
        }
    }

    public void LoadReport()
    {
        da = new SqlDataAdapter("select c.date,c.voucher_id,c.paid_against,c.cc_code,c.sub_dca,c.dca_code,c.name,c.debit,c.particulars,c.balance,v.vendor_id from  cash_book c left outer join vendor v on c.vendor_id=v.vendor_id where c.id='" + Request.QueryString["Voucherid"].ToString() + "' and c.debit is not null", con);
        da.Fill(ds, "rep");
        if (ds.Tables["rep"].Rows.Count > 0)
        {
                lbldate.Text = String.Format("{0:dd/MM/yyyy}", ds.Tables["rep"].Rows[0]["date"]);
                if ("" == ds.Tables["rep"].Rows[0]["paid_against"].ToString())
                {
                    lblcccode.Text = ds.Tables["rep"].Rows[0]["cc_code"].ToString();
                }
                else
                {
                    lblcccode.Text = ds.Tables["rep"].Rows[0]["paid_against"].ToString();
                }
                
                lblvoucherid.Text = ds.Tables["rep"].Rows[0]["voucher_id"].ToString();
                lblvendorid.Text = ds.Tables["rep"].Rows[0]["vendor_id"].ToString();
                lblpayto.Text = ds.Tables["rep"].Rows[0]["name"].ToString();
                lblperticular.Text = ds.Tables["rep"].Rows[0]["particulars"].ToString();
                lblDca.Text = ds.Tables["rep"].Rows[0]["dca_code"].ToString();
                lblsubdca.Text = ds.Tables["rep"].Rows[0]["sub_dca"].ToString();
                lblamount.Text = ds.Tables["rep"].Rows[0]["debit"].ToString().Replace(".0000", ".00");
                lblTotal.Text = ds.Tables["rep"].Rows[0]["debit"].ToString().Replace(".0000", ".00");
                lblinwords.Text = ConvertNumberToWord(Convert.ToInt64(ds.Tables["rep"].Rows[0]["debit"])) + " Only";
        }
        else
        {
            lbldate.Text = "-";
            lblcccode.Text = "-";
            lblvoucherid.Text = "-";
            lblvendorid.Text = "-";
            lblpayto.Text = "-";
            lblperticular.Text = "-";
            lblDca.Text = "-";
            lblsubdca.Text = "-";
            lblamount.Text = "-";
            lblTotal.Text = "-";
            lblinwords.Text = "-";
        }
    }

    private string ConvertNumberToWord(long nNumber)
    {


        long CurrentNumber = nNumber;


        string sReturn = "";


        if (CurrentNumber >= 1000000000)
        {


            sReturn = sReturn + " " + GetWord(CurrentNumber / 1000000000, "Billion");


            CurrentNumber = CurrentNumber % 1000000000;


        }


        if (CurrentNumber >= 1000000)
        {


            sReturn = sReturn + " " + GetWord(CurrentNumber / 1000000, "Million");


            CurrentNumber = CurrentNumber % 1000000;


        }


        if (CurrentNumber >= 1000)
        {


            sReturn = sReturn + " " + GetWord(CurrentNumber / 1000, "Thousand");


            CurrentNumber = CurrentNumber % 1000;


        }


        if (CurrentNumber >= 100)
        {


            sReturn = sReturn + " " + GetWord(CurrentNumber / 100, "Hundred");


            CurrentNumber = CurrentNumber % 100;


        }


        if (CurrentNumber >= 20)
        {


            sReturn = sReturn + " " + GetWord(CurrentNumber, "");


            CurrentNumber = CurrentNumber % 10;


        }


        else if (CurrentNumber > 0)
        {


            sReturn = sReturn + " " + GetWord(CurrentNumber, "");


            CurrentNumber = 0;


        }


        return sReturn.Replace(" ", " ").Trim();


    }


    private string GetWord(long nNumber, string sPrefix)
    {


        long nCurrentNumber = nNumber;


        string sReturn = "";


        while (nCurrentNumber > 0)
        {


            if (nCurrentNumber > 100)
            {


                sReturn = sReturn + " " + GetWord(nCurrentNumber / 100, "Hundred");


                nCurrentNumber = nCurrentNumber % 100;


            }


            else if (nCurrentNumber > 20)
            {


                sReturn = sReturn + " " + GetTwentyWord(nCurrentNumber / 10);


                nCurrentNumber = nCurrentNumber % 10;


            }


            else
            {


                sReturn = sReturn + " " + GetLessThanTwentyWord(nCurrentNumber);


                nCurrentNumber = 0;


            }


        }


        sReturn = sReturn + " " + sPrefix;


        return sReturn;


    }


    private string GetTwentyWord(long nNumber)
    {


        string sReturn = "";


        switch (nNumber)
        {


            case 2:


                sReturn = "Twenty";


                break;


            case 3:


                sReturn = "Thirty";


                break;


            case 4:


                sReturn = "Forty";


                break;


            case 5:


                sReturn = "Fifty";


                break;


            case 6:


                sReturn = "Sixty";


                break;


            case 7:


                sReturn = "Seventy";


                break;


            case 8:


                sReturn = "Eighty";


                break;


            case 9:


                sReturn = "Ninety";


                break;


        }


        return sReturn;


    }


    private string GetLessThanTwentyWord(long nNumber)
    {


        string sReturn = "";


        switch (nNumber)
        {


            case 1:


                sReturn = "One";


                break;


            case 2:


                sReturn = "Two";


                break;


            case 3:


                sReturn = "Three";


                break;


            case 4:


                sReturn = "Four";


                break;


            case 5:


                sReturn = "Five";


                break;


            case 6:


                sReturn = "Six";


                break;


            case 7:


                sReturn = "Seven";


                break;


            case 8:


                sReturn = "Eight";


                break;


            case 9:


                sReturn = "Nine";


                break;


            case 10:


                sReturn = "Ten";


                break;


            case 11:


                sReturn = "Eleven";


                break;


            case 12:


                sReturn = "Twelve";


                break;


            case 13:


                sReturn = "Thirteen";


                break;


            case 14:


                sReturn = "Forteen";


                break;


            case 15:


                sReturn = "Fifteen";


                break;


            case 16:


                sReturn = "Sixteen";


                break;


            case 17:


                sReturn = "Seventeen";


                break;


            case 18:


                sReturn = "Eighteen";


                break;


            case 19:


                sReturn = "Nineteen";


                break;


        }


        return sReturn;


    }


}
