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
/// Summary description for veninfo
/// </summary>
public class veninfo
{
	public veninfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
 
    public static void GridView_Row_Merger(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow currentRow = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];
 
            for (int i = 0; i <13; i++)
            {
                if (currentRow.Cells[1].Text == previousRow.Cells[1].Text)
                {
                    previousRow.Cells[17].Visible = false;
                    currentRow.Cells[17].RowSpan = previousRow.Cells[17].RowSpan + 1;

                    for (int j = 0; j < 13; j++)
                    {
                        if (previousRow.Cells[j].RowSpan < 2)
                            currentRow.Cells[j].RowSpan = 2;
                        else
                            currentRow.Cells[j].RowSpan = previousRow.Cells[j].RowSpan + 1;
                        previousRow.Cells[j].Visible = false;
                        //previousRow.Cells[17].Visible = false;

                    }
                }
                else
                {
                    previousRow.Cells[17].Visible = true;

                }

                
            }
        }
    }
   
}


