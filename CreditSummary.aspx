<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="CreditSummary.aspx.cs"
    Inherits="CreditSummary" Title="Untitled Page" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table>
                    <tr>
                        <td align="center" colspan="8">
                            <table width="800px" class="estbl ">
                                <tr>
                                    <th align="center">
                                        BANK FLOW CREDIT SUMMARY
                                    </th>
                                </tr>
                                <tr>
                                    <td align="center">
                                        Select year:
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                GridLines="Both" AlternatingRowStyle-CssClass="alt" FooterStyle-BackColor="LightGray"
                                AutoGenerateColumns="true" ShowFooter="true" FooterStyle-HorizontalAlign="Right"
                                OnRowDataBound="GridView1_RowDataBound" ondatabound="GridView1_DataBound">
                                <%--<Columns>
                                    <asp:BoundField DataField="Paymenttype" ItemStyle-Width="600px" HeaderText="CREDIT AGAINST"
                                        HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CurrentFY" HeaderText="AGAINST CURRENT FY" HeaderStyle-Width="100px"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev1" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev2" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev3" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Prev4" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                                </Columns>--%>
                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                <PagerStyle CssClass="grid pagerbar" />
                                <HeaderStyle CssClass="grid-header" />
                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                            </asp:GridView>
                        </td>
                    </tr>
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
