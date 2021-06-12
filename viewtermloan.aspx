<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="viewtermloan.aspx.cs"
    Inherits="viewtermloan" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px" align="center">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <%-- <% <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>%>--%>
                <table>
                    <tr>
                        <td align="center">
                            <table class="estbl" width="400px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" align="center">
                                        <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View Term Loan"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="estbl" width="400px">
                                            <tr id="type" runat="server">
                                                <td align="right" style="width: 40px">
                                                    <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Agency Code"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 40px">
                                                    <asp:DropDownList ID="ddlagencycode" runat="server" Width="105px" CssClass="esddown">
                                                    </asp:DropDownList>
                                                    <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlagencycode"
                                                        ServicePath="cascadingDCA.asmx" PromptText="Select Agency Code" Category="dca"
                                                        LoadingText="Please Wait" ServiceMethod="Agencycode">
                                                    </cc1:CascadingDropDown>
                                                </td>
                                                <td align="center" style="width: 40px">
                                                    <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Loan Number"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 40px">
                                                    <asp:DropDownList ID="ddlloanno" runat="server" Width="105px" CssClass="esddown">
                                                    </asp:DropDownList>
                                                    <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlloanno"
                                                        ServicePath="cascadingDCA.asmx" ParentControlID="ddlagencycode" PromptText="Select Loan No"
                                                        Category="dd1" LoadingText="Please Wait" ServiceMethod="loanno">
                                                    </cc1:CascadingDropDown>
                                                </td>
                                            </tr>
                                            <tr id="Tr1" runat="server">
                                                <td align="center" style="width: 40px">
                                                    <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 40px">
                                                    <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server">
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
                                                <td align="center" style="width: 40px">
                                                    <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 40px">
                                                    <asp:DropDownList ID="ddlyear" runat="server" Width="105px" CssClass="esddown">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr align="center" id="btn" runat="server">
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                            Text="View" OnClick="btnAssign_Click" />
                                        <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                            Text="Reset" OnClick="btnCancel1_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table id="Table1" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                <asp:GridView ID="GridView1" runat="server" CssClass="grid-content" AutoGenerateColumns="false"
                                    HeaderStyle-CssClass="grid-header" BorderColor="Black" GridLines="Both" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                    RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                    Width="100%" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="There Is No Records"
                                    OnRowCreated="GridView1_RowCreated">
                                    <Columns>
                                        <asp:BoundField DataField="agencycode" HeaderText="Agency Code" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="loanno" HeaderText="Loan Number" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="bank_name" HeaderText="Bank Name" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="date" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="modeofpay" HeaderText="ModeOfPay" HeaderStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="chequenumber" HeaderText="Cheque Number" HeaderStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="credit" HeaderText="Credit" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Processingamt" HeaderText="Processing Charge" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Principle" HeaderText="Principle" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="interest" HeaderText="Interest" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                </asp:GridView>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <table id="header" runat="server" class="estbl" width="90%">
                                            <tr>
                                                <td width="400px">
                                                    <asp:Label ID="Label6" runat="server" Text="Total Paid Principle"></asp:Label>
                                                </td>
                                                <td width="400px">
                                                    <asp:Label ID="Label9" runat="server" Text="Total Paid Interest"></asp:Label>
                                                </td>
                                                <td width="200px">
                                                    <asp:Label ID="Label10" runat="server" Text="Total Payable Principle"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="400px">
                                                    <asp:Label ID="Label7" runat="server"></asp:Label>
                                                </td>
                                                <td width="400px">
                                                    <asp:Label ID="Label8" runat="server"></asp:Label>
                                                </td>
                                                <td width="200px">
                                                    <asp:Label ID="Label19" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trexcel" runat="server">
                                    <td align="left" colspan="3">
                                        <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                                        <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                            OnClick="btnExcel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <%--<%</ContentTemplate>
                </asp:UpdatePanel>%>--%>
            </td>
        </tr>
    </table>
</asp:Content>
