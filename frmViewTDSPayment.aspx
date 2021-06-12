<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmViewTDSPayment.aspx.cs"
    Inherits="Admin_frmViewTDSPayment" EnableEventValidation="false" ValidateRequest="false"
    Title="View TDS Payment - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style9
        {
            width: 62px;
        }
        .style10
        {
            width: 87px;
        }
        .style11
        {
            width: 156px;
        }
    </style>
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
                        <td>
                            <table class="estbl" width="700px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" colspan="5" align="center">
                                        <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View TDS Payment"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center" valign="middle">
                                        <asp:RadioButton ID="rbtnpaiddate" runat="server" Text="Paid Date" GroupName="dt"
                                            Font-Bold="true" Font-Names="Courier New" Font-Size="Small" Checked="true" ForeColor="Black" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbtninvmkdate" runat="server" Text="Invoice Entry Date" GroupName="dt"
                                            Font-Bold="true" Font-Names="Courier New" Font-Size="Small" ForeColor="Black" />
                                    </td>
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
                                            Text="View TDS" OnClick="btnAssign_Click" />
                                         <asp:Button ID="newpage" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="New Report" OnClick="newpage_Click"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                          <%--  <cc1:Accordion ID="Accordion1" runat="server" TransitionDuration="100" FramesPerSecond="200"
                                OnItemDataBound="Accordion1_ItemDataBound" FadeTransitions="true" RequireOpenedPane="false"
                                ContentCssClass="accordionContent" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                Total TDS Pament to Government
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <%#Eval("Debit") %>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ContentTemplate>--%>
                            <asp:Label ID="lblTotal" runat="server" CssClass="accordionHeader" Width="98%"></asp:Label>
                            <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" Width="100%" EmptyDataText="NO DATA AVAILABLE"
                                BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false" Font-Size="Small">
                                        <Columns>
                                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="InvoiceNO" ItemStyle-Width="100px" HeaderText="Invoice No" />
                                            <asp:BoundField DataField="Vendor_id" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                HeaderText="Vendor ID" />
                                            <asp:BoundField DataField="CC_Code" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                HeaderText="CC Code" />
                                            <asp:BoundField DataField="DCA_Code" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                HeaderText="DCA Code" />
                                    <asp:BoundField DataField="Basic Amount" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                        HeaderText="Basic Amount" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="Debit" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                HeaderText="Amount" DataFormatString="{0:#,##,##,###.00}" />
                                            <asp:BoundField DataField="Bank_Name" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                                HeaderText="Bank Name" />
                                        </Columns>
                                        <RowStyle ForeColor="#000066" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" />
                                    </asp:GridView>
                            <%--   </ContentTemplate>
                            </cc1:Accordion>--%>
                        </td>
                    </tr>
                    <tr id="trxlgrid" runat="server">
                        <td>
                            <asp:GridView ID="GridView3" runat="server" OnRowDataBound="GridView3_RowDataBound"
                                BorderWidth="1px" AutoGenerateColumns="false" Font-Size="Small">
                                <Columns>
                                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="InvoiceNO" HeaderText="Invoice No" />
                                    <asp:BoundField DataField="Vendor_id" ItemStyle-HorizontalAlign="Right" HeaderText="Vendor ID" />
                                    <asp:BoundField DataField="CC_Code" ItemStyle-HorizontalAlign="Right" HeaderText="CC Code" />
                                    <asp:BoundField DataField="DCA_Code" ItemStyle-HorizontalAlign="Right" HeaderText="DCA Code" />
                                    <asp:BoundField DataField="Basic Amount" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"
                                        HeaderText="Basic Amount" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Debit" ItemStyle-HorizontalAlign="Right" HeaderText="Amount"
                                        DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Bank_Name" ItemStyle-HorizontalAlign="Right" HeaderText="Bank Name" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trexcel" runat="server">
                        <td align="left" colspan="2">
                            <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                OnClick="btnExcel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
