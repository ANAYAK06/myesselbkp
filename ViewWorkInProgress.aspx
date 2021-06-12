<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewWorkInProgress.aspx.cs" Inherits="ViewWorkInProgress" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type"text/javascript">
        function validate() {
            var objs = new Array("<%=ddlclientid.ClientID %>", "<%=ddlsubclientid.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlyear.ClientID %>");
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
                                    View Work In Progress
                                </th>
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
                                    <asp:DropDownList ID="ddlsubclientid" AutoPostBack="true" runat="server" ToolTip="SubClientID"
                                        Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="CC Code:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="180px"
                                        CssClass="esddown">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Year"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlyear" runat="server" ToolTip="year" Width="110px" CssClass="esddown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnview" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="View" OnClientClick="javascript:return validate()" OnClick="btnview_Click" />
                                    <asp:Button ID="btnCancel" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Cancel" onclick="btnCancel_Click" />
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
                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                ShowFooter="true" EmptyDataText="There is no records" 
                                                onrowdatabound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="client_id" HeaderText="clientid" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="subclient_id" HeaderText="Subclientid" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="cc_code" HeaderText="CC Code" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="pono" HeaderText="PO NO" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                                </Columns>
                                            </asp:GridView>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
