<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="CashDebitSummary.aspx.cs"
    Inherits="CashDebitSummary" Title="Cash Debit Summary" %>

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
                <table width="700px">
                    <tr>
                        <td align="center">
                            <table width="400px" class="estbl estbl">
                                <tr>
                                    <th>
                                        Cash Flow Debit Summary
                                    </th>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Select No.of years:
                                        <asp:DropDownList ID="ddlnumberofyears" CssClass="esddown" Width="105px" runat="server"
                                            ToolTip="No.of years">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:Button ID="btnview" runat="server" CssClass="esbtn" Text="View" OnClick="btnview_Click" />
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <asp:Panel ID="Panel1" runat="server">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="true"
                                     OnRowDataBound="GridView1_RowDataBound" CssClass="grid-content"
                                    BorderColor="White" ShowFooter="true" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                    RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                    FooterStyle-BackColor="DarkGray">
                                    
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="tblexpences" runat="server" align="right">
                            <td align="center">
                                <table class="estbl" align="right">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="YEAR CLOSING CC BALANCE"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:GridView ID="GridView2" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    runat="server" AutoGenerateColumns="false" Width="250px" AllowPaging="false"
                                    ShowFooter="true" OnRowDataBound="GridView2_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="10px"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="CC Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px"
                                            DataField="CC Code" />
                                        <asp:BoundField HeaderText="Closing Balance" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px"
                                            DataField="Balance" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
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
