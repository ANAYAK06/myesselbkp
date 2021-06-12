using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using System.Text;
using System.Collections;


public class JavaScript
{
    protected static Hashtable handlerPages = new Hashtable();
    private JavaScript()
    {
    }

    public static void Alert(string Message)
    {
        if (!(handlerPages.Contains(HttpContext.Current.Handler)))
        {
            Page currentPage = (Page)HttpContext.Current.Handler;
            if (!((currentPage == null)))
            {
                Queue messageQueue = new Queue();
                messageQueue.Enqueue(Message);
                handlerPages.Add(HttpContext.Current.Handler, messageQueue);
                currentPage.Unload += new EventHandler(CurrentPageUnload);
            }
        }
        else
        {
            Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
            queue.Enqueue(Message);
        }
    }

    private static void CurrentPageUnload(object sender, EventArgs e)
    {
        Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
        if (queue != null)
        {
            StringBuilder builder = new StringBuilder();
            int iMsgCount = queue.Count;
            builder.Append("<script language='javascript'>");
            string sMsg;
            while ((iMsgCount > 0))
            {
                iMsgCount = iMsgCount - 1;
                sMsg = System.Convert.ToString(queue.Dequeue());
                sMsg = sMsg.Replace("\"", "'");
                builder.Append("alert( \"" + sMsg + "\" );");
            }
            builder.Append("</script>");
            handlerPages.Remove(HttpContext.Current.Handler);
            HttpContext.Current.Response.Write(builder.ToString());
        }
    }

    public static void CloseWindow()
    {
        HttpContext.Current.Response.Write("<script type='text/javascript'>window.close();</script>");   
    }
    public static void OpenWindow()
    {
        HttpContext.Current.Response.Write("<script type='text/javascript'>window.close();</script>");
    }
    public static void AlertAndRedirect(string Message,string TargetUrl)
    {
        HttpContext.Current.Response.Write("<script type='text/javascript'>alert('" + Message + "');window.location ='" + TargetUrl + "';</script>");
    }

    public static void ClearForm()
    {
        HttpContext.Current.Response.Write("<script type='text/javascript'>history.go(-1);</script>");   
    }

    public static void UPAlert(Page page, string Message)
    {
        string script = @"alert('" + Message + "');";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Alert", script, true);
    }

    public static void UPAlertRedirect(Page page, string Message, string TargetUrl)
    {
        string script = @"alert('" + Message + "');window.location ='" + TargetUrl + "';";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Alert", script, true);
    }

    public static void UPAlertRedirect(string p, string p_2)
    {
        throw new NotImplementedException();
    }
    public static void POPUPClosing(string Message)
    {
        HttpContext.Current.Response.Write("<script type='text/javascript'>alert('" + Message + "');window.close();</script>");
    }
}

