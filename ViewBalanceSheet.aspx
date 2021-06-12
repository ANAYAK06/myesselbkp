<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewBalanceSheet.aspx.cs" Inherits="ViewBalanceSheet" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/62gvam81_styles.css" rel="stylesheet" type="text/css" />
    <script src="Java_Script/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Java_Script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Java_Script/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <style>
        /* unvisited link */
        a:link
        {
            color: blue;
        }
        
        /* visited link */
        a:visited
        {
            color: green;
        }
        
        /* mouse over link */
        a:hover
        {
            color: hotpink;
        }
        
        /* selected link */
        a:active
        {
            color: blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Balance Sheet<a class="help"
                                    href="" title=""><small>Help</small> </a>
                            </h1>
                            <%--  <a href='#' class='gridViewToolTip' id="gridViewToolTipid">Sudheer</a><div id='tooltip' style='display: none;'></div>--%>
                            <table width="90%">
                                <tr>
                                    <td align="center">
                                        <div id="body_form">
                                            <div>
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="view_form_options" width="100%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">
                                                                                Financial Year:
                                                                                <asp:DropDownList ID="ddlyear" CssClass="char" runat="server" Width="100px" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnexpand" runat="server" Text="Expand All" CssClass="button" OnClick="btnexpand_Click" />
                                                                                <asp:Button ID="btncollapse" runat="server" Text="Collapse All" CssClass="button"
                                                                                    OnClick="btncollapse_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table width="750px" id="tblbody" runat="server">
                                <tr>
                                    <td colspan='3' height='49' align='center' valign="middle" bgcolor='#B7DEE8'>
                                        <b><span style='font-size: xx-large'>ESSEL Projects Pvt Ltd</span></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='3' height='34' align='center' valign="middle" bgcolor='#E6B9B8'>
                                        <b><span style='font-size: x-large'>BALANCE SHEET</span></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='3' height='20' align='left' valign="top" bgcolor='#B7DEE8'>
                                        <b><span style='font-size: larger' id="spnyear" runat="server"></span></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000;
                                        width: 355px' height='20' valign="bottom" bgcolor='#E6B9B8'>
                                        <div align="center" style="display: inline; text-align: center; float: inherit; width: 200px">
                                            <span style='color: #000000; font-size: larger; font-weight: bold;'>LIABILITIES
                                            </span>
                                        </div>
                                        <div align="center" style="display: inline; text-align: center; float: right">
                                            <span style='color: #000000; font-size: larger; font-weight: bold;'>Rs.</span>
                                        </div>
                                    </td>
                                    <td width='50px' style='border-top: 1px solid #000000; border-bottom: 1px solid #000000;'
                                        align="right" valign="bottom" bgcolor='#E6B9B8'>
                                        <span style='color: #000000; font-size: larger; font-weight: bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                    </td>
                                    <td style='border-top: 1px solid #000000; border-bottom: 1px solid #000000; width: 355px'
                                        align="right" valign="bottom" bgcolor='#E6B9B8'>
                                        <div align="left" style="display: inline; text-align: left; float: left">
                                            <span style='color: #000000; font-size: larger; font-weight: bold;'>ASSETS </span>
                                        </div>
                                        <div align="right" style="display: inline; text-align: right; float: right">
                                            <span style='color: #000000; font-size: larger; font-weight: bold;'>Rs.</span>
                                        </div>
                                    </td>
                                </tr>
                               
                                <tr style="position: static">
                                    <td height='20' align='left' valign="top" bgcolor='#B7DEE8' style="position: static">
                                        <asp:TreeView ID="tvexpn" runat="server" Font-Bold="true" ForeColor="Black" Target="_blank"
                                            ShowLines="true">
                                        </asp:TreeView>
                                    </td>
                                    <td width='40px' bgcolor='#B7DEE8'>
                                    </td>
                                    <td height='20' align='left' valign="top" bgcolor='#B7DEE8' style="position: static">
                                        <asp:TreeView ID="tvinn" runat="server" Font-Bold="true" ForeColor="Black" Target="_blank"
                                            ShowLines="true">
                                        </asp:TreeView>
                                    </td>
                                </tr>
                                <tr>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <hr align="right" width="25%" style="margin-left: auto; margin-right: auto; border-top: 1px dashed #000000;
                                            border-collapse: collapse" />
                                    </td>
                                    <td width='40px' bgcolor='#B7DEE8'>
                                    </td>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <hr align="right" width="25%" style="margin-left: auto; margin-right: auto; border-top: 1px dashed #000000;
                                            border-collapse: collapse" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <span style="color: #000000; font-size: x-small; font-weight: bold; font-style: normal;
                                            font-family: Tahoma; text-decoration: none;" id="SpnTotalNetExp" runat="server">
                                        </span>
                                    </td>
                                    <td width='40px' bgcolor='#B7DEE8'>
                                    </td>
                                    <td height='20' align="right" valign="bottom" bgcolor='#B7DEE8'>
                                        <span style="color: #000000; font-size: x-small; font-weight: bold; font-style: normal;
                                            font-family: Tahoma; text-decoration: none;" id="SpnTotalNetIn" runat="server">
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <div align="left" style="display: inline; text-align: left; float: left">
                                            <span style="color: #000000; font-size: x-small; font-weight: bold; font-style: normal;
                                                font-family: Tahoma; text-decoration: none;" id="spnnetprofittext" runat="server">
                                                P & L ACCOUNT </span>
                                        </div>
                                        <div align="right" style="display: inline;">
                                            <span style="color: #000000; font-size: x-small; font-weight: bold; font-style: normal;
                                                font-family: Tahoma; text-decoration: none;" id="SpnNetProfit" runat="server">
                                            </span>
                                        </div>
                                    </td>
                                    <td width='40px' bgcolor='#B7DEE8'>
                                    </td>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <div align="left" style="display: inline; text-align: left; float: left">
                                            <span style="color: #000000; font-size: x-small; font-weight: bold; font-style: normal;
                                                font-family: Tahoma; text-decoration: none;" id="spnnetlosstext" runat="server">
                                                P & L ACCOUNT </span>
                                        </div>
                                        <div align="right" style="display: inline;">
                                            <span style="color: #000000; font-size: x-small; font-weight: bold; font-style: normal;
                                                font-family: Tahoma; text-decoration: none;" id="SpnNetLoss" runat="server">
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <hr align="right" width="25%" style="margin-left: auto; margin-right: auto; border-bottom: 1px dashed #000000;
                                            border-collapse: collapse" />
                                    </td>
                                    <td width='40px' bgcolor='#B7DEE8'>
                                    </td>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <hr align="right" width="25%" style="margin-left: auto; margin-right: auto; border-bottom: 1px dashed #000000;
                                            border-collapse: collapse" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <span style="color: #000000; font-size: x-small; font-weight: bold; font-style: normal;
                                            font-family: Tahoma; text-decoration: none;" id="SpnTotalNetProfit" runat="server">
                                        </span>
                                    </td>
                                    <td width='40px' bgcolor='#B7DEE8'>
                                    </td>
                                    <td height='20' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <span style="color: #000000; font-size: x-small; font-weight: bold; font-style: normal;
                                            font-family: Tahoma; text-decoration: none;" id="SpnTotalNetLoss" runat="server">
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td height='10' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <hr align="right" width="25%" style="margin-left: auto; margin-right: auto; border-bottom: 1px dashed #000000;
                                            border-collapse: collapse" />
                                        <hr align="right" width="25%" style="margin-left: auto; margin-right: auto; border-top: 1px dashed #000000;
                                            border-collapse: collapse" />
                                    </td>
                                    <td width='40px' bgcolor='#B7DEE8'>
                                    </td>
                                    <td height='10' align='right' valign="bottom" bgcolor='#B7DEE8'>
                                        <hr align="right" width="25%" style="margin-left: auto; margin-right: auto; border-bottom: 1px dashed #000000;
                                            border-collapse: collapse" />
                                        <hr align="right" width="25%" style="margin-left: auto; margin-right: auto; border-top: 1px dashed #000000;
                                            border-collapse: collapse" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
