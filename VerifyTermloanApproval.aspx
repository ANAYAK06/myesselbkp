<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyTermloanApproval.aspx.cs" Inherits="VerifyTermloanApproval" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table>
                    <tr style="border: 1px solid #000">
                        <th valign="top" align="center" style="background: #D3D3D3">
                            <asp:Label ID="lblheading" CssClass="esfmhead" runat="server" Text=""></asp:Label>
                        </th>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:GridView ID="GridView1" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                DataKeyNames="id" BorderColor="White" EmptyDataText="There is no records" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                        SelectImageUrl="~/images/iconset-b-edit.gif" />
                                    <asp:BoundField DataField="id" Visible="true" ItemStyle-Width="100px" HeaderText="Transaction No" />
                                    <asp:BoundField DataField="cc_code" ItemStyle-Width="75px" HeaderText="CC Code" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="loanpurpose" ItemStyle-HorizontalAlign="Center" HeaderText="Description"
                                        ItemStyle-Width="200px" />
                                    <asp:BoundField DataField="inststartdate" ItemStyle-HorizontalAlign="Right" HeaderText="Date"
                                        ItemStyle-Width="75px" />
                                    <asp:BoundField DataField="amount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}"
                                        HeaderText="Amount" ItemStyle-Width="75px" />
                                </Columns>
                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table id="tblloandetails" runat="server" style="width: 700px; border: 1px solid #000">
                    <tr valign="top">
                        <td align="center">
                            <table style="width: 700px">
                                <tr valign="top">
                                    <td align="center">
                                        <table class="estbl" width="700px">
                                            <tr style="border: 1px solid #000">
                                                <th colspan="4">
                                                    <asp:Label ID="itform" CssClass="esfmhead" Width="550px" runat="server" Text="Approve Term Loan"></asp:Label>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Loan Type"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlloantype" Enabled="false" Width="200px" ToolTip="Loan Type"
                                                        runat="server">
                                                        <asp:ListItem>Select Loan Type</asp:ListItem>
                                                        <asp:ListItem>For Capital</asp:ListItem>
                                                        <asp:ListItem>For Purchase Of Assets</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblloanno" CssClass="eslbl" runat="server" Text="Loan No"></asp:Label>
                                                </td>
                                                <td style="width: 300px">
                                                    <asp:TextBox ID="txtloanno" CssClass="estbox" Enabled="false" ToolTip="Loan No" Width="100%"
                                                        runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblagencycode" CssClass="eslbl" runat="server" Text="Agency Code"></asp:Label>
                                                </td>
                                                <td colspan="1">
                                                    <asp:TextBox ID="txtagencycode" Enabled="false" CssClass="estbox" Width="100%" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblagencyname" class="ajaxspan" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Loan Applied Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtapplydate" ToolTip="Apply Date" Enabled="false" CssClass="estbox"
                                                        Width="100%" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="Disbursal Amount"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdisposalamt" CssClass="estbox" Enabled="false" ToolTip="Disbursal Amount"
                                                        Width="100%" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="Processing Charge"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtprocessingcrg" CssClass="estbox" Enabled="false" ToolTip="Processing Charge"
                                                        Width="100%" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="right">
                                                    <asp:Label ID="lblamount" CssClass="eslbl" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                                <td colspan="1">
                                                    <asp:TextBox ID="txtamount" CssClass="estbox" ToolTip="Amount" Enabled="false" Width="100%"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Installment Start Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtinststartdate" ToolTip="Installment Start Date" Enabled="false"
                                                        CssClass="estbox" Width="100%" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label6" CssClass="eslbl" runat="server" Text="Installment End Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtinstenddate" ToolTip="Installment End Date" CssClass="estbox"
                                                        Width="100%" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="No of Installments"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnoofinst" CssClass="estbox" Enabled="false" ToolTip="No Of Installments"
                                                        Width="100%" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Interest Rate"></asp:Label>
                                                </td>
                                                <td colspan="1">
                                                    <asp:TextBox ID="txtinterestrate" CssClass="estbox" Enabled="false" ToolTip="Interest Rate"
                                                        Width="100%" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="ModeofPay" runat="server">
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="Bank Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtbankname" ToolTip="Date" Enabled="false" CssClass="estbox" Width="100%"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdate" ToolTip="Date" Enabled="false" CssClass="estbox" Width="100%"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Pay" runat="server">
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="Cheque No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtchequeno" CssClass="estbox" Enabled="false" ToolTip="Cheque No"
                                                        Width="100%" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="PayMode" runat="server" CssClass="eslbl" Text="Mode Of Pay"></asp:Label>
                                                </td>
                                                <td colspan="1">
                                                    <asp:TextBox ID="txtpayment" CssClass="estbox" Enabled="false" Width="100%" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label7" CssClass="eslbl" runat="server" Text="Loan Purpose"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtloanpurpose" CssClass="estbox" Enabled="false" ToolTip="Loan Purpose"
                                                        TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Button ID="btnverifyTL" CssClass="esbtn" runat="server" Text="Verify" OnClick="btnverifyTL_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnrejectagency" runat="server" CssClass="esbtn" OnClick="btnrejectagency_Click"
                                                        Text="Reject" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
