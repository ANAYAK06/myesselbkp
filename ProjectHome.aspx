<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ProjectHome.aspx.cs" Inherits="ProjectHome" Title="Project Home - Essel Projects Pvt. Ltd." %>
<%@ Register Src="~/ProjectVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="900px">
        <tr>
            <td style="width: 150px">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

