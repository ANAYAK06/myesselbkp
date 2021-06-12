<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ViewExciseDuty.aspx.cs" Inherits="ViewExciseDuty" Title="Untitled Page" %>
<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function searchvalidate() {

            var objs = new Array("<%=ddlyear.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <table class="estbl" width="700px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" colspan="5" align="center">
                                        <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View Excise Duty"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server" ToolTip="PO">
                                            <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                            <asp:ListItem Value="1">Jan</asp:ListItem>
                                            <asp:ListItem Value="2">Feb</asp:ListItem>
                                            <asp:ListItem Value="3">Mar</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">Sep</asp:ListItem>
                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="starSpan">*</span>
                                    </td>
                                    <td valign="center" class="style10">
                                        <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                    </td>
                                    <td align="left" class="style11">
                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="130px">
                                        <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                            Text="View Excise Duty" OnClick="btnAssign_Click" OnClientClick="javascript:return searchvalidate()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="False"
                                OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Font-Size="X-Small"
                                CssClass="grid-content" BorderColor="Black" HeaderStyle-CssClass="grid-header"
                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                PagerStyle-CssClass="grid pagerbar" ShowFooter="true" FooterStyle-BackColor="White"
                                FooterStyle-Font-Bold="true">
                                <Columns>
                                    <asp:BoundField DataField="Date" HeaderText="Bill Date" DataFormatString="{0:dd/MM/yyyy}"
                                        FooterText="Total in Rs:" />
                                    <asp:BoundField DataField="Servicetax" HeaderText="Excise Duty" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Edcess" HeaderText="NetEdcess" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Hedcess" HeaderText="NetHedcess" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="TotalServiceTax" HeaderText="Excise Duty" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Vendor" HeaderText="TO Vendor" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Govt" HeaderText="Excise Duty to Govt" HeaderStyle-Wrap="false" ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="PTax" HeaderText="Excise Duty Penel Interest" HeaderStyle-Wrap="false" ItemStyle-Width="30px" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal1" runat="server" Text='<%#Bind("balance") %>' ForeColor='<%# Convert.ToDecimal(Eval("balance"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
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
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

