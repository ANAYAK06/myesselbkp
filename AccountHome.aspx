<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AccountHome.aspx.cs" Inherits="AccountHome" Title="Account Home - Essel Projects Pvt. Ltd." %>
<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        a.blueRow
        {
            background-color: #AECFF0;
            color: Blue;
            text-decoration: none;
        }
        a.grayRow
        {
            background-color: #F8F8F8;
            color: Gray;
            text-decoration: underline;
        }
    </style>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top" style="width: 750px">
                <%--<marquee>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <table style="border-bottom: 1px solid #000; width: 100%;">
                            <tr class="<%#(Container.ItemIndex+1)%2==0?"blueRow":"grayRow" %>">
            <td>
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td style="color:white;background-color:#336699; font-size:14px;">
                                    <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text=". This PO"></asp:Label>
                                    <asp:Label ID="Label38" runat="server" CssClass="eslbl"  Text='<%#Eval("po_no") %>'></asp:Label>
                                    <asp:Label ID="lbltext" runat="server" CssClass="eslbl"  Text=" is have 60 days to complete"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                </marquee>--%>
                <marquee>
                <table style="border-bottom: 1px solid #000;">
                    <tr>
                        <td>
                        </td>
                        <td style="color: white; background-color: #336699; font-size: 14px;">
                            <asp:Label ID="lbl" runat="server" CssClass="eslbl"></asp:Label>
                        </td>
                    </tr>
                </table></marquee>
            </td>
        </tr>
    </table>
</asp:Content>

