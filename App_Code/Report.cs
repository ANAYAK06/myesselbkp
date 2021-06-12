using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


/// <summary>
/// Summary description for Report
/// </summary>
public class Report
{
	public Report()
	{
		//
		// TODO: Add constructor logic here
		//
	}
  
    public  string date
    {
        get;
        set;
    }
    public string voucher_id
    {
        get;
        set;
    }
       
       
        public string cc_code
        {
            get;
            set;
        }
        public string sub_dca
        {
        get;
            set;

        
        }
        public string dca_code
        {
            get;
            set;


        }
    public string name
    {
        get;
        set;
    }
        public int debit
        {
            get;
            set;
        }
        public string credit
        {
            get;
            set;
        }
        public string particulars
        {
            get;
            set;
        }
        

      public string vendor_id
        {
            get;
            set;

        }
      public string balance
      {
          get;
          set;
      }

}
