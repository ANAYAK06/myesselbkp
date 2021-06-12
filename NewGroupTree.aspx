<%@ Page Title="ViewGroupTree" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="NewGroupTree.aspx.cs" Inherits="NewGroupTree" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="750px" style="border: 1px solid #000">
                    <tr style="border: 1px solid #000">
                        <th valign="top" align="center" style="background: #D3D3D3">
                            <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text="View Group Tree Structure"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td align="left" style="background:#FFFFFF">
                            <table>
                                <asp:TreeView ID="tvresult" BackColor="White" runat="server" Font-Bold="true" ForeColor="Black"
                                    Target="_blank">
                                </asp:TreeView>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
