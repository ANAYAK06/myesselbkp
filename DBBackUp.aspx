<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="DBBackUp.aspx.cs" Inherits="Admin_DBBackUp" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="500px" align="center">
    <tr>
        <th>
            Click Here Take DataBase Back Up
        </th>
    </tr>
    <tr>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Back Up" 
                onclick="Button1_Click" />
        </td>
    </tr>
</table>
</asp:Content>

