<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Essel.master" CodeFile="Support.aspx.cs" Inherits="Support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <title>Support</title>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    
        <table align="center" width="350">
            <tr>
                <td>
                    <br>
                    <br>
                </td>
            </tr>
            <tr height="50">
                <td>
                    <img src="images/logo.png">
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                    <br>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <p>
                        <font size="2" face="Verdana">At IndusTouch we always think of what's next way of applying
                            technology to make our day to day life more easier. We have thought of a way and
                            we are now on it. Day and night.</b> </font>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                    <br>
                    <br>
                </td>
            </tr>
            <%--  <tr>
                <td align="center">
                    <font size="2" face="Verdana"><b>Watch this space !!</font>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <font size="2" face="Verdana"><b>Soon you will find a all new way of doing things</font>
                    <img src="images/Smile.jpg" width="40" height="40"></b>
                </td>
            </tr>--%>
            <tr>
                <td align="center" id="tdregister" runat="server" colspan="2">
                    <asp:HyperLink ID="HyperLink3" Font-Underline="false" Font-Bold="true" ForeColor="Black"
                        runat="server" NavigateUrl="~/ITSMRegister.aspx">Register</asp:HyperLink>&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" id="tdcrissue" runat="server" colspan="2">
                    <asp:HyperLink ID="HyperLink1" Font-Underline="false" Font-Bold="true" ForeColor="Black"
                        runat="server" NavigateUrl="~/EsselCr.aspx">Add Cr/Issue</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td align="center" id="tdview" runat="server" colspan="2">
                    <asp:HyperLink ID="HyperLink6" Font-Underline="false" Font-Bold="true" ForeColor="Black"
                        runat="server" NavigateUrl="~/EsselCrReport.aspx">View Cr/Issue</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td align="center" id="tdestimation" runat="server" colspan="2">
                    <asp:HyperLink ID="HyperLink4" Font-Underline="false" Font-Bold="true" ForeColor="Black"
                        runat="server" NavigateUrl="~/Estimated.aspx">Task Estimation</asp:HyperLink>
                </td>
            </tr>
           <%-- <tr>
                <td align="center" id="tdtasks" runat="server" colspan="2">
                    <asp:HyperLink ID="HyperLink7" Font-Underline="false" Font-Bold="true" ForeColor="Black"
                        runat="server" NavigateUrl="~/AssignTasks.aspx">Assign/View Tasks</asp:HyperLink>
                </td>
            </tr>--%>
            <tr>
                <td align="center" id="tdtaskstatus" runat="server" colspan="2">
                    &nbsp;
                    <asp:HyperLink ID="HyperLink2" Font-Underline="false" Font-Bold="true" ForeColor="Black"
                        runat="server" NavigateUrl="~/TaskStatus.aspx">Change Tasks Status</asp:HyperLink>
                </td>
              
            </tr>
             <tr>
                <td align="center" id="tdentertime" runat="server" colspan="2">
                    &nbsp;
                    <asp:HyperLink ID="HyperLink5" Font-Underline="false" Font-Bold="true" ForeColor="Black"
                        runat="server" NavigateUrl="~/TasksTime.aspx">Enter Task Time</asp:HyperLink>
                </td>
              
            </tr>
            <tr>
                <td align="center" id="tdviewtasktime" runat="server" colspan="2">
                    &nbsp;
                    <asp:HyperLink ID="HyperLink8" Font-Underline="false" Font-Bold="true" ForeColor="Black"
                        runat="server" NavigateUrl="~/ViewTaskTime.aspx">View Task Time Details</asp:HyperLink>
                </td>
              
            </tr>
        </table>
        <table width="900">
            <tr align="right">
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="esbtn" 
                    onclick="btnback_Click"  />
            </tr>
        </table>
         <table width="900">
            <tr align="right">
                <asp:LinkButton ID="lbtnmail" runat="server" onclick="lbtnmail_Click">Send Mails</asp:LinkButton>
            </tr>
        </table>
   

</asp:Content>
