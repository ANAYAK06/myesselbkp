<%@ Page Title="Add/Update SubGroups" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="AddSubGroups.aspx.cs" Inherits="AddSubGroups" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
   
    <script type="text/javascript">
        function validate() {

            if (SelectedIndex("<%=rbtnsubgroups.ClientID %>") == 0) {
                var objs = new Array("<%=ddlgroups.ClientID %>")
                if (!CheckInputs(objs)) {
                    return false;
                }
                var GridView = document.getElementById("<%=gvDetails.ClientID %>");
                if (GridView != null) {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (rowCount == 1) {
                            if (GridView.rows(rowCount).cells(1).children[0].value == "") {
                                window.alert("Please Insert Atleast One Sub-Group");
                                return false;
                            }
                        }
                        else {
                            return true;
                        }
                    }
                }
            }

        }
    </script>
    <script type="text/javascript">

        function hide() {
            if (SelectedIndex("<%=rbtnsubgroups.ClientID %>") == 1) {
                var Trgroups = document.getElementById("<%=trgroups.ClientID %>");
                var Trsubgroups = document.getElementById("<%=trsubgroups.ClientID %>");
                Trgroups.style.display = 'none';
                Trsubgroups.style.display = 'block';

            }
            if (SelectedIndex("<%=rbtnsubgroups.ClientID %>") == 0) {
                var Trgroups = document.getElementById("<%=trgroups.ClientID %>");
                var Trsubgroups = document.getElementById("<%=trsubgroups.ClientID %>");
                Trgroups.style.display = 'block';
                Trsubgroups.style.display = 'none';

            }
        }
    </script>
    <script type="text/javascript">
        function check() {
            var Tbladdsubgroups = document.getElementById("<%=tbladdsubgroups.ClientID %>");
            var ddlgroups = document.getElementById("<%=ddlgroups.ClientID %>");
            var ddlchildgroup = document.getElementById("<%=ddlchildgroup.ClientID %>");
            if (ddlgroups.value != "" || ddlchildgroup.value != "") {
                Tbladdsubgroups.style.display = 'block';
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
                        <table cellpadding="0" cellspacing="0" width="700px">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="400px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text="Add Sub-Group"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="estbl" width="400px">
                                                    <tr align="center">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtnsubgroups" CssClass="esrbtn" Style="font-size: small"
                                                                RepeatDirection="Horizontal" runat="server" CellPadding="0" CellSpacing="0" onclick="hide();">
                                                                <asp:ListItem Selected="True" Value="0">Sub-Groups</asp:ListItem>
                                                                <asp:ListItem Value="1">Child-Groups</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trgroups" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Sub-Groups"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 350px">
                                                            <asp:DropDownList ID="ddlgroups" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Sub-Groups">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="groups" TargetControlID="ddlgroups"
                                                                ServiceMethod="Groups" ServicePath="cascadingDCA.asmx" PromptText="Select Groups">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                    <tr id="trsubgroups" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Groups"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 350px">
                                                            <asp:DropDownList ID="ddlchildgroup" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Groups">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="chgroups"
                                                                TargetControlID="ddlchildgroup" ServiceMethod="ChildGroups" ServicePath="cascadingDCA.asmx"
                                                                PromptText="Select Groups">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tbladdsubgroups" style="border: 1px solid #000;" runat="server" width="410px">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvDetails" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="400px" GridLines="None" ShowFooter="true" OnRowDeleting="gvDetails_RowDeleting">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="rowid" HeaderText="No" ReadOnly="true" />--%>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                            ItemStyle-HorizontalAlign="Center" FooterStyle-BackColor="White">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub-Group Names" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtGName" runat="server" Width="190px" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Sub-Group Name Req"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtGName"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" BackColor="White" />
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAdd" runat="server" ValidationGroup="valGroup12" OnClick="btnAdd_Click"
                                                                    ImageUrl="~/images/imgadd1.gif" />
                                                                <%-- <asp:Button ID="btnAdd" runat="server" ValidationGroup='valGroup12' Text="Add" CssClass="esbtn" OnClick="btnAdd_Click" />--%>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" FooterStyle-BackColor="White" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="btn" runat="server">
                                        <tr align="center">
                                            <td align="center">
                                                <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" OnClientClick="javascript:return validate();"
                                                    CssClass="esbtn" Style="font-size: small" Text="Submit" />
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
