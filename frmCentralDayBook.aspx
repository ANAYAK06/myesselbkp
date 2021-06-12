<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmCentralDayBook.aspx.cs"
    Inherits="Admin_frmCentralDayBook" EnableEventValidation="false" Title="Central Day Book - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="653px">
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Unassigned Balance: "></asp:Label>
                            <asp:Label ID="lblbal" runat="server" CssClass="eslbl"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                AutoGenerateColumns="false" CssClass="mGrid" GridLines="None" OnPageIndexChanging="GridView3_PageIndexChanging"
                                PagerStyle-CssClass="pgr" PageSize="10" Width="400px">
                                <PagerStyle CssClass="pgr" />
                                <Columns>
                                    <asp:BoundField DataField="TransferCC" HeaderText="CC Code" />
                                    <asp:BoundField DataField="balance" HeaderText="Pending Debit Balance" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lbltot" CssClass="eslbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView2" runat="server" PagerStyle-CssClass="pgr" AutoGenerateColumns="false"
                                AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AllowPaging="True" GridLines="None"
                                PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging1" Width="400px">
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <Columns>
                                    <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                    <asp:BoundField DataField="CC_Name" HeaderText="Cost Center" />
                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
