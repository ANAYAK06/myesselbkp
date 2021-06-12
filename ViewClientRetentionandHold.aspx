<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewClientRetentionandHold.aspx.cs" Inherits="ViewClientRetentionandHold" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddltypeofpay.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
      
    </script>

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
                        <table class="estbl" width="660px">
                            <tr align="center">
                                <th colspan="4">
                                    View Client Hold and Retention
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    Category of Payment:
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddltypeofpay" runat="server" ToolTip="Type of Payment" CssClass="esddown"
                                        Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddltypeofpay_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Retention</asp:ListItem>
                                        <asp:ListItem Value="2">Hold</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Client ID:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlclientid" runat="server" AutoPostBack="true" ToolTip="ClientID"
                                        Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Subclient ID:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsubclientid" AutoPostBack="true" runat="server" ToolTip="ClientID"
                                        Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="CC Code:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" AutoPostBack="true"
                                        Width="180px" CssClass="esddown" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="PO NO:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlpo" runat="server" ToolTip="PO No" AutoPostBack="true" Width="110px"
                                        CssClass="esddown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnview" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="View" OnClientClick="javascript:return validate()" OnClick="btnview_Click" />
                                    <asp:Button ID="btnCancel" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td class="grid-content">
                                        <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                DataKeyNames="InvoiceNo" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                RowStyle-CssClass=" grid-row char grid-row-odd" ShowFooter="true" EmptyDataText="There is no records"
                                                OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="PO_NO" HeaderText="PO NO" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="client_id" HeaderText="clientid" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="subclient_id" HeaderText="Subclientid" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="cc_code" HeaderText="CC Code" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="bank_name" HeaderText="Bank Name" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Total" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Received Amount" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="balance" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                            </asp:GridView>
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
