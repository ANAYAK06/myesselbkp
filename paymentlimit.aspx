<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="paymentlimit.aspx.cs"
    Inherits="paymentlimit" Title="Untitled Page" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table width="60%" align="center">
                    <tr>
                        <td class="item item-selection" align="center" width="" colspan="1">
                            <asp:Label ID="Label2" runat="server" Text="Present Voucher limit is:"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <asp:GridView ID="GridView1" runat="server" Width="50%" AutoGenerateColumns="false"
                            HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" ShowFooter="true"
                            HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                            FooterStyle-BackColor="DarkGray" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LimitName" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="idl" runat="server" Text='<%# Bind("LimitName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Limit" ItemStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="limit" runat="server" Text='<%# Bind("limit") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("limit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="ID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Id" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Id" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:CommandField HeaderText="Edit" ShowEditButton="true" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
