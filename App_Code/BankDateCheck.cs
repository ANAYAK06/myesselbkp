using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for BankDateCheck
/// </summary>
public class BankDateCheck
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["esselDB"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
   public bool Avail = false;
   public bool IsBankDateCheck(string Date, string Bankname)
   {
       da = new SqlDataAdapter("select * from bank_branch where bank_name='" + Bankname + "' and date<='" + Date + "'", con);
       da.Fill(ds, "Datecheck");
       if (ds.Tables["Datecheck"].Rows.Count > 0)
       {
           Avail = true;
       }
       else
       {
           Avail = false;
       }
       return Avail;
   }
   public bool IsBankDateCheck(string Date, string Frombank, string Tobank)
   {
       if (Frombank != "" && Tobank != "")
       {
           da = new SqlDataAdapter("select * from bank_branch where bank_name in ('" + Frombank + "','" + Tobank + "') and date<='" + Date + "'", con);
           da.Fill(ds, "Datecheck");
           if (ds.Tables["Datecheck"].Rows.Count ==2)
           {
               Avail = true;
           }
           else
           {
               Avail = false;
           }
       }
       else
       {
           da = new SqlDataAdapter("select * from bank_branch where bank_name in ('" + Frombank + "') and date<='" + Date + "'", con);
           da.Fill(ds, "Datecheck");
           if (ds.Tables["Datecheck"].Rows.Count > 0)
           {
               Avail = true;
           }
           else
           {
               Avail = false;
           }

       }
      
       return Avail;
   }
   public bool IsSPPODateCheck(string pono, string cccode, string date)
   {
       if (pono != "" && cccode != "")
       {
           da = new SqlDataAdapter("SELECT 1 as IsExists FROM Amend_sppo where pono= '" + pono + "' and status!='Rejected'", con);
           da.Fill(ds, "checkexists");
           if (ds.Tables["checkexists"].Rows.Count > 0)
           {
               da = new SqlDataAdapter("select top 1 * from Amend_sppo where pono= '" + pono + "' and  Amended_date > '" + date + "' and status!='Rejected' order by id desc", con);
               da.Fill(ds, "AmendDateCheck");
               if (ds.Tables["AmendDateCheck"].Rows.Count == 0)
               {
                   Avail = true;
               }
               else
               {
                   Avail = false;
               }

           }
           else
           {
               da = new SqlDataAdapter("select * from sppo where po_date > '" + date + "' and pono= '" + pono + "' and cc_code= '" + cccode + "' and status!='Rejected' ", con);
               da.Fill(ds, "podate");
               if (ds.Tables["podate"].Rows.Count == 0)
               {
                   Avail = true;
               }
               else
               {
                   Avail = false;
               }
           }

       }
       return Avail;
   }
}