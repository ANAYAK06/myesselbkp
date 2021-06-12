<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="BankVocuhersByITwise.aspx.cs"
    Inherits="BankVocuhersByITwise" Title="Bank Vouchers By IT Wise" EnableEventValidation="false" %>

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
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 700px">
                            <tr>
                                <td align="center">
                                    <table width="400px" class="estbl">
                                        <tr>
                                            <th align="center">
                                                Overall IT Status Per Year
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table id="paytype" runat="server" class="innertab">
                                                    <tr>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label8" runat="server" Text="IT Code" CssClass="eslbl"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:DropDownList ID="ddlit" runat="server" onchange="SetDynamicKey('dp3',this.value);"
                                                                            Width="100px" CssClass="esddown">
                                                                        </asp:DropDownList>
                                                                        <cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" TargetControlID="ddlit"
                                                                            ServicePath="cascadingDCA.asmx" Category="sub1" LoadingText="Please Wait" ServiceMethod="itcode">
                                                                        </cc1:CascadingDropDown>
                                                                        <asp:Label ID="lblitcode" class="ajaxspan" runat="server"></asp:Label>
                                                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp3" runat="server"
                                                                            TargetControlID="lblitcode" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                            ServiceMethod="GetITName">
                                                                        </cc1:DynamicPopulateExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Bank"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlbank" CssClass="esddown" Width="105px" runat="server" ToolTip="Month">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label4" CssClass="esddown" runat="server" Text="CC Code"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:DropDownList ID="ddlcccode" CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);"
                                                                            runat="server" Width="150px">
                                                                        </asp:DropDownList>
                                                                        <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlcccode"
                                                                            ServicePath="cascadingDCA.asmx" Category="cc" LoadingText="Loading CC" ServiceMethod="codename"
                                                                            PromptText="Select All">
                                                                        </cc1:CascadingDropDown>
                                                                        <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp1" runat="server"
                                                                            TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                            ServiceMethod="GetCCName">
                                                                        </cc1:DynamicPopulateExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Monthly"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server" ToolTip="Month">
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
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblyear" CssClass="esddown" runat="server" Text="yearly"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                                    Text="View" OnClick="btnsubmit_Click1" />&nbsp
                                                <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%--<tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true"
                                GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging1" Font-Size="Small">
                                <Columns>
                                    <asp:BoundField DataField="date" HeaderText="Voucher Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="modifieddate" HeaderText="Paid Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="Paymenttype" HeaderText="Payment Type" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="Modeofpay" HeaderText="Mode Of Pay" />
                                    <asp:BoundField DataField="No" HeaderText="No" />
                                    <asp:BoundField DataField="CC_Code" HeaderText="CC_Code" />
                                    <asp:BoundField DataField="it_Code" HeaderText="IT Code" />
                                    <asp:BoundField DataField="Debit" HeaderText="Debit" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>--%>
                        </table>
                        <%-- <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr class="pagerbar">
                                    <td class="pagerbar-cell" align="right">
                                        <table class="pager-table" id="tblheader" runat="server">
                                            <tbody>
                                                <tr>
                                                    <td class="pager-cell">
                                                        <h2>
                                                            Bank Vouchers</h2>
                                                    </td>
                                                    <td class="loading-list" style="display: none;">
                                                        <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                    </td>
                                                    <td class="pager-cell-button">
                                                        <td class="pager-cell" style="width: 90%" valign="middle">
                                                            <div class="pager">
                                                                <div align="right">
                                                                    <asp:Label ID="Label2" CssClass="item item-char" runat="server" Text="Change Limit:"></asp:Label>
                                                                    <asp:DropDownList ID="ddlpagecount" runat="server" OnSelectedIndexChanged="ddlpagecount_SelectedIndexChanged"
                                                                        AutoPostBack="true">
                                                                        <asp:ListItem Selected="True">10</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>50</asp:ListItem>
                                                                        <asp:ListItem>100</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="grid-content">
                                        <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                CssClass="grid-content" BorderColor="Black" HeaderStyle-CssClass="grid-header"
                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                PagerStyle-CssClass="grid pagerbar" AllowPaging="true" ShowFooter="true" OnDataBound="GridView1_DataBound"
                                                OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="date" HeaderText="Voucher Date" DataFormatString="{0:dd/MM/yyyy}"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="modifieddate" ItemStyle-Wrap="false" HeaderText="Paid Date"
                                                        DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="Paymenttype" HeaderText="Payment Type" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Wrap="false"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="bank_name" HeaderText="Bank Name" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Modeofpay" HeaderText="Mode Of Pay" />
                                                    <asp:BoundField DataField="No" HeaderText="No" />
                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC Code" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="it_Code" HeaderText="IT Code" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-HorizontalAlign="Right" />
                                                </Columns>
                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                <PagerTemplate>
                                                    <asp:ImageButton ID="btnFirst" runat="server" Height="15px" ImageUrl="~/images/pager_first.png"
                                                        CommandArgument="First" CommandName="Page" OnCommand="btnFirst_Command" />&nbsp;
                                                    <asp:ImageButton ID="btnPrev" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_left.png"
                                                        CommandArgument="Prev" OnCommand="btnPrev_Command" />
                                                    <asp:Label ID="lblpages" runat="server" Text="" Height="15px" CssClass="item item-char"></asp:Label>
                                                    of
                                                    <asp:Label ID="lblCurrent" runat="server" Text="Label" Height="15px" CssClass="item item-char"></asp:Label>
                                                    <asp:ImageButton ID="btnNext" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_right.png"
                                                        CommandArgument="Next" OnCommand="btnNext_Command" />&nbsp;
                                                    <asp:ImageButton ID="btnLast" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_last.png"
                                                        CommandArgument="Last" OnCommand="btnLast_Command" />
                                                </PagerTemplate>
                                            </asp:GridView>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trexcel" runat="server">
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Convert To Excel :-"></asp:Label>
                                        <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                            OnClick="btnExcel_Click" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
