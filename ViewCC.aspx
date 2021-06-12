<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewCC.aspx.cs" Inherits="ViewCC" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="Table1" style="width: 700px" runat="server">
                            <tr valign="top">
                                <td align="center">
                                    <table id="grid" class="grid-content" runat="server" align="center">
                                        <tr>
                                            <td id="Td1" align="center" colspan="2" runat="server">
                                                <asp:GridView ID="GridView1" AutoGenerateColumns="false" CssClass="grid-content"
                                                    DataKeyNames="cc_code" AllowPaging="false" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    PagerStyle-CssClass="grid pagerbar" EmptyDataText="There is no Records" runat="server"
                                                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                    OnPageIndexChanging="GridView1_PageIndexChanging">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="true" ShowCancelButton="true" ShowDeleteButton="false" />
                                                        <asp:TemplateField HeaderText="CC Code" HeaderStyle-BackColor="White" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lblcccode" runat="server" Text='<%# Eval("cc_code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CC Name" HeaderStyle-BackColor="White" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lblccname" runat="server" Text='<%# Eval("cc_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Voucher Limit" HeaderStyle-BackColor="White" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lblvlimit" runat="server" Text='<%#Eval("voucher_limit")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtvoucher" Text='<%# Bind("voucher_limit")%>' runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                                                    ControlToValidate="txtvoucher" ErrorMessage="Enter voucherlimit"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Day Limit" HeaderStyle-BackColor="White" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbldlimit" runat="server" Text='<%#Eval("day_limit")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtday" runat="server" Text='<%# Bind("day_limit")%>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="" runat="server"
                                                                    ControlToValidate="txtday" ErrorMessage="Enter daylimit"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
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
