<%@ Page Title="Sub-Groups" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifySubGroups.aspx.cs" Inherits="VerifySubGroups" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            var GridView = document.getElementById("<%=gvsubgroups.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                }
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" class="grid" width="700px">
                            <tr>
                                <td align="center">
                                    <table width="400px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" align="center" style="background: #D3D3D3">
                                                <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                        <tr style="border: 1px solid #000">
                                            <td>
                                                <table>
                                                    <tr align="center">
                                                        <td colspan="2">
                                                            <asp:GridView ID="gvsubgroups" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                AutoGenerateColumns="False" BorderColor="white" CssClass="grid-content" DataKeyNames="id,subgroup_type"
                                                                GridLines="None" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                                RowStyle-CssClass=" grid-row char grid-row-odd" ShowFooter="false" Width="680px"
                                                                OnRowDeleting="gvsubgroups_RowDeleting">
                                                                <Columns>
                                                                 <asp:BoundField DataField="subgroup_type" Visible="false" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                                        ItemStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                    <asp:BoundField DataField="NatureGroupName" HeaderText="Nature of Group" />
                                                                    <asp:BoundField DataField="Group_Name" HeaderText="Group Name" />
                                                                    <asp:BoundField DataField="Name" HeaderText="Sub-Group Name" ItemStyle-ForeColor="DarkBlue" />
                                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/Delete.jpg" HeaderText="Reject"
                                                                        ItemStyle-Width="15px" ShowDeleteButton="true" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr id="btn" runat="server" align="center">
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                                Text="" OnClientClick="javascript:return validate();" OnClick="btnAssign_Click" />
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
