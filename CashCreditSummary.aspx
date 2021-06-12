<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="CashCreditSummary.aspx.cs"
    Inherits="CashCreditSummary" Title="Untitled Page" %>

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
                <table width="700px">
                    <tr>
                        <td align="center">
                            <table width="400px" class="estbl estbl">
                                <tr>
                                    <th>
                                        Cash Flow Credit Summary
                                    </th>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year"
                                            AutoPostBack="True" onchange="SetDynamicKey('dp4',this.value);" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                ShowFooter="true" FooterStyle-HorizontalAlign="Right" OnRowCreated="GridView1_RowCreated"
                                OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Paymenttype" ItemStyle-Width="600px" HeaderText="CREDIT AGAINST"
                                        HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CurrentFY" HeaderText="AGAINST CURRENT FY" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev1" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev2" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev3" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev4" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-Width="100px" />
                                </Columns>
                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                <PagerStyle CssClass="grid pagerbar" />
                                <HeaderStyle CssClass="grid-header" />
                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
