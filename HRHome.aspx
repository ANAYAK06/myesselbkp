<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="HRHome.aspx.cs" Inherits="HRHome" Title="Untitled Page" %>
<%@ Register Src="~/HRVerticalMenu.ascx" TagName="Menu" TagPrefix="HRMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table width="900px">
        <tr>
            <td style="width: 150px">
                <HRMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

