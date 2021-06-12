<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyAssetSalePayment.aspx.cs" Inherits="VerifyAssetSalePayment" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
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
                                            <asp:BoundField DataField="request_No" ItemStyle-HorizontalAlign="Center" HeaderText="Transaction No" />
                                            <asp:BoundField DataField="Item_Code" ItemStyle-HorizontalAlign="Center" HeaderText="Item Code" />
                                            <asp:BoundField DataField="Payment_Type" ItemStyle-HorizontalAlign="Center" HeaderText="Payment Type" />
                                            <asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Center" HeaderText="Amount" />
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
                                                <asp:Label ID="lblheader" Width="680px" CssClass="esfmhead" runat="server" Text="Approve/Reject Asset CreditPayment"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="680px">
                                        <tr id="trmiscclient" runat="server" align="center">
                                            <td colspan="2">
                                                <asp:Label ID="Label17" runat="server" Width="100px" CssClass="eslbl" Font-Size="XX-Small"
                                                    Text="Transaction No"></asp:Label>
                                                <asp:Label ID="lbltranno" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="Label19" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Item Code and Specification:-"></asp:Label>
                                                <asp:Label ID="lblitemcode" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server" align="center">
                                            <td colspan="2">
                                                <asp:Label ID="Label1" runat="server" Width="100px" CssClass="eslbl" Font-Size="XX-Small"
                                                    Text="Buyer Name:- "></asp:Label>
                                                <asp:Label ID="lblbuyername" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" align="center">
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Font-Size="XX-Small" Height="50px"
                                                    Text="Buyer Address:- "></asp:Label>
                                                <asp:Label ID="lblbuyeradd" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trbankpayment" runat="server">
                                            <td colspan="4">
                                                <table align="center" class="estbl" width="680px" runat="server" id="paymentdetails">
                                                    <tr>
                                                        <th align="center" colspan="4">
                                                            Payment Details
                                                        </th>
                                                    </tr>
                                                    <tr id="bank" runat="server">
                                                        <td style="width: 100px">
                                                            <asp:Label ID="Label16" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Bank:"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:Label ID="lblbank" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:Label ID="lab" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Date"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="ModeofPay" runat="server">
                                                        <td style="width: 50px">
                                                            <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Mode Of Pay:"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:Label ID="lblmodeofpay" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 50px">
                                                            <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="No:"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:Label ID="lblno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50px">
                                                            <asp:Label ID="Label7" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Remarks:"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:TextBox ID="txtdesc" CssClass="estbox" Width="175px" runat="server" Enabled="false"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 50px">
                                                            <asp:Label ID="Label8" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Amount:"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px">
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
                                                <asp:Button ID="btnreject" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Reject" OnClick="btnreject_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="vertical-align: middle;" id="tblverifysupplierandsp" runat="server"
                            cellspacing="0" width="600px" align="center">
                            <tr>
                                <td>
                                    <table border="1px solid" cellpadding="0" cellspacing="0" width="580px">
                                        <tbody>
                                            <tr>
                                                <td valign="top">
                                                    <table border="0" class="fields" width="680px">
                                                        <tbody>
                                                            <tr align="center">
                                                                <td colspan="4" valign="top" class=" item-group" width="680px">
                                                                    <table border="0" class="fields" width="680px">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label>
                                                                                        Transaction No
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <asp:HiddenField ID="HiddenField" runat="server" />
                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                    <asp:TextBox ID="txttransaction" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                </td>
                                                                                <td class="label" width="1%">
                                                                                    <label>
                                                                                        Payment Type
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                    <asp:TextBox ID="txttype" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                </td>
                                                                                <td class="label" width="1%">
                                                                                    <label for="journal_id">
                                                                                        Name
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                    <asp:TextBox ID="txtname" Enabled="false" runat="server" Width="150px" CssClass="char"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" align="center">
                                                                                    <label class="help" id="Label2" runat="server" style="font-weight:bold">
                                                                                        Asset Details
                                                                                    </label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" align="center">
                                                                                    <asp:GridView ID="grdassetdetails" runat="server" CssClass="mGrid" AllowPaging="false"
                                                                                        AllowSorting="True" AutoGenerateColumns="False" Width="680px" CellPadding="4"
                                                                                        ForeColor="#333333" GridLines="None" ShowFooter="false" Font-Size="Small">
                                                                                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Request_No" HeaderText="Request No" />
                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                            <asp:BoundField DataField="BookValue_Date" HeaderText="Book Value Date" HtmlEncode="false" />
                                                                                            <asp:BoundField DataField="Actuall_Amt" HeaderText="Actuall Amount" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                            <asp:BoundField DataField="Selling_Amt" HeaderText="Selling Amount" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                            <asp:BoundField DataField="Balance_Amt" HeaderText="Balance Amount" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                        </Columns>
                                                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                                        <%-- <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />--%>
                                                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                                    <asp:GridView ID="grd" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                                                                                        AutoGenerateColumns="False" Width="680px" CellPadding="4" ForeColor="#333333"
                                                                                        GridLines="None" ShowFooter="true" Font-Size="Small" OnRowDataBound="grd_RowDataBound">
                                                                                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                                                                                        <Columns>
                                                                                            <%--<asp:CommandField ShowSelectButton="True" />--%>
                                                                                            <asp:BoundField DataField="invoiceno" HeaderText="InvoiceNo" InsertVisible="False"
                                                                                                ReadOnly="True" />
                                                                                            <asp:BoundField DataField="cc_code" HeaderText="CC CODE" />
                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA CODE" />
                                                                                            <asp:BoundField DataField="vendor_id" HeaderText="Vendor ID" />
                                                                                            <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                            <asp:BoundField DataField="netamount" HeaderText="Net Amount" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                            <asp:BoundField DataField="tds" HeaderText="TDS" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                            <asp:BoundField DataField="retention" HeaderText="Retention" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                            <asp:BoundField DataField="hold" HeaderText="Hold" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                            <asp:BoundField DataField="Amount" HeaderText="Paid" DataFormatString="{0:#,##,##,###.00}"
                                                                                                HtmlEncode="false" />
                                                                                        </Columns>
                                                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                                        <%-- <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />--%>
                                                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" valign="top" class=" item-notebook" width="780px">
                                                                    <div id="Div2" class="notebook" style="display: block;">
                                                                        <div class="notebook-pages">
                                                                            <div class="notebook-page notebook-page-active">
                                                                                <div align="center">
                                                                                    <span class="tab-title" style="font-weight: bold">Payment Details</span>
                                                                                    <table border="0" class="fields" width="780px">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help" id="Label4" runat="server">
                                                                                                        Buyer Name
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="item item-char" width="35%" valign="middle">
                                                                                                    <span class="filter_item">
                                                                                                        <asp:TextBox ID="txtbuyername" runat="server" Enabled="false" CssClass="char">
                                                                                                        </asp:TextBox>
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td width="1%">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Payment Date
                                                                                                    </label>
                                                                                                    :
                                                                                                </td>
                                                                                                <td width="35%" valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtpaymentdate" onKeyDown="preventBackspace();" Enabled="false" onpaste="return false;"
                                                                                                        onkeypress="return false;" MaxLength="11" ToolTip="Paid Date" runat="server"
                                                                                                        CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td width="1%">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Request No:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td width="35%" valign="middle" class="item item-char">
                                                                                                    <span class="filter_item">
                                                                                                        <asp:TextBox ID="txtrequestno" runat="server" Enabled="false" CssClass="char"></asp:TextBox>
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td width="1%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Item Code
                                                                                                    </label>
                                                                                                    :
                                                                                                </td>
                                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtitemcode" runat="server" Enabled="false" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td width="1%">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                </td>
                                                                                                <td width="1%">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Amount:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtdebitamount" ToolTip="Amount" Enabled="false" runat="server"
                                                                                                        CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td width="1%">
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr style="border-bottom: 1px solid black">
                                                                <td align="center">
                                                                    <asp:Label ID="Label5" runat="server" CssClass="red"></asp:Label>
                                                                    <asp:Button ID="btnasstApprove" runat="server" Text="Approve" OnClick="btnasstApprove_Click" CssClass="button" />
                                                                     <asp:Button ID="btnasstreject" runat="server" Text="Reject" CssClass="button" OnClick="btnasstreject_Click" />                                                                   
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
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
