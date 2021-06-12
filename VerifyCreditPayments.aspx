<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyCreditPayments.aspx.cs" Inherits="VerifyCreditPayments" %>

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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr style="border: 1px solid #000">
                                <th valign="top" align="center" style="background: #D3D3D3">
                                    <asp:Label ID="lblheading" CssClass="esfmhead" runat="server" Text=""></asp:Label>
                                </th>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:GridView ID="gvcredits" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="False" BorderColor="white" CssClass="grid-content" DataKeyNames="Id"
                                        GridLines="None" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                        RowStyle-CssClass=" grid-row char grid-row-odd" ShowFooter="false" Width="680px"
                                        OnSelectedIndexChanged="gvcredits_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PaymentType" ItemStyle-HorizontalAlign="Center" HeaderText="Payment Type" />
                                            <asp:BoundField DataField="Bank_Name" ItemStyle-HorizontalAlign="Center" HeaderText="Bank Name" />
                                            <asp:BoundField DataField="Description" ItemStyle-HorizontalAlign="Center" HeaderText="Description" />
                                            <asp:BoundField DataField="Date" ItemStyle-HorizontalAlign="Center" HeaderText="Date" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="height: 25px">
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" id="tblverifycredits" runat="server" cellspacing="0" width="680px">
                            <tr align="center">
                                <td align="center">
                                    <table class="estbl" width="680px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" Width="590px" CssClass="esfmhead" runat="server" Text="Approve/Reject CreditPayment"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="680px">
                                        <tr>
                                            <td style="width: 200px;">
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Category of Payment:"></asp:Label></br>
                                                <asp:Label ID="lblcategoryofpayment" runat="server"></asp:Label>
                                            </td>
                                            <td id="tdrefundtype" runat="server" colspan="3">
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Refund Type:"></asp:Label>
                                                <asp:Label ID="lblrefundtype" runat="server"></asp:Label>
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="FDR No:"></asp:Label>
                                                <asp:Label ID="lblfdrno" runat="server"></asp:Label>
                                                <asp:Label ID="Label18" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Intrest Type:"></asp:Label>
                                                <asp:Label ID="lblmistype" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trmiscclient" runat="server" align="center">
                                            <td colspan="2">
                                                <asp:Label ID="Label17" runat="server" Width="50px" CssClass="eslbl" Font-Size="XX-Small"
                                                    Text="Client ID"></asp:Label>
                                                <asp:Label ID="lblclient" runat="server" ></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="Label19" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Sub Client "></asp:Label>
                                                <asp:Label ID="lblsubclient" runat="server"  ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Width="50px" CssClass="eslbl" Font-Size="XX-Small"
                                                    Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="DCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label11" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="SDCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="Amount"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="lblcccodepri" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldcacodepri" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblsdcacodepri" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblamountpri" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trinterstheading" runat="server">
                                            <th align="center" colspan="5">
                                                <asp:Label ID="Labelheading" runat="server" CssClass="eslbl" Width="50px" Font-Size="XX-Small"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr align="center" id="trintrestlbl" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Width="50px" Font-Size="XX-Small"
                                                    Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="DCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="SDCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label15" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="Amount"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center" id="trintrestcodes" runat="server">
                                            <td>
                                                <asp:Label ID="lblcccodeint" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldcacodeint" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblsdcacodeint" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblamountint" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trname" runat="server">
                                            <td colspan="2" align="right">
                                                <asp:Label ID="Label3" class="eslbl" runat="server" Text="Name:   "></asp:Label>
                                            </td>
                                            <td colspan="2" align="left">
                                                <asp:Label ID="lblname" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table align="center" class="estbl" width="100%" runat="server" id="paymentdetails">
                                                    <tr>
                                                        <th align="center" colspan="4">
                                                            Payment Details
                                                        </th>
                                                    </tr>
                                                    <tr id="bank" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Bank:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblbank" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lab" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="ModeofPay" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Mode Of Pay:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblmodeofpay" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="No:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Remarks:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Amount:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblamount" runat="server" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tblbtnupdate" runat="server" width="700px">
                                        <tr align="center">
                                            <td align="center">
                                                <asp:Button ID="btnapprove" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Approval" OnClick="btnapprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <%----%>
                                                <asp:Button ID="btnreject" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Reject" OnClick="btnreject_Click" />
                                                <%--OnClick="btnreject_Click"--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
