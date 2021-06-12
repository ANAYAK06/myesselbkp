<%@ Page Title="Group Creation" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="CreateGroups.aspx.cs" Inherits="CreateGroups" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .FormatRadioButtonList label {
            margin-right: 30px;
        }
    </style>
    <script type="text/javascript">
        function validate() {
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                var objs = new Array("<%=ddlnatureofgroup.ClientID %>")
                if (!CheckInputs(objs)) {
                    return false;
                }
                var GridView = document.getElementById("<%=gvDetails.ClientID %>");
                if (GridView != null) {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (rowCount == 1) {
                            if (GridView.rows(rowCount).cells(1).children[0].value == "") {
                                window.alert("Please Insert Atleast One Group");
                                return false;
                            }
                        }
                        else if (rowCount > 1) {
                            if (GridView.rows(rowCount).cells(1).children[0].value == "") {
                                window.alert("Please Insert Group Name or Delete the row..");
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
        function validateupdate() {
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                var GridView = document.getElementById("<%=gvupdate.ClientID %>");

                if (GridView != null) {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (GridView.rows(rowCount).cells(0).children(0).checked == true) {
                            if ((GridView.rows(rowCount).cells(2).children[0].value) == "") {
                                window.alert("Add Group Name");
                                return false;
                            }
                        }


                    }
                }
            }
        }

    </script>
    <script type="text/javascript">
        function radioMe(e, CurrentGridRowCheckBoxListID) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;
            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById(CurrentGridRowCheckBoxListID);
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
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
                                    <table class="estbl" width="540px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text="Add/Update Groups"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="estbl" width="540px">
                                                    <tr align="center">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Mode of Transction"
                                                                RepeatDirection="Horizontal" runat="server" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Selected="True"
                                                                    Value="0">Add Group</asp:ListItem>
                                                                <asp:ListItem Value="1">Update Groups</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Nature Of Group"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlnatureofgroup" CssClass="esddown" runat="server" ToolTip="Nature of Group"
                                                                OnSelectedIndexChanged="ddlnatureofgroup_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <span class="starSpan">*</span>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="group" TargetControlID="ddlnatureofgroup"
                                                                ServiceMethod="NatureGroup" ServicePath="cascadingDCA.asmx" PromptText="Select Group Nature">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tbladdgroups" style="border: 1px solid #000" runat="server" width="550px">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvDetails" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="550px" GridLines="None" ShowFooter="true" OnRowDeleting="gvDetails_RowDeleting" OnRowCreated="gvDetails_RowCreated">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group Name" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtGName" runat="server" Width="190px" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Group Name Required"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup='valGroup1' ControlToValidate="txtGName"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <%-- <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAdd" runat="server" ValidationGroup="valGroup1" OnClick="btnAdd_Click"
                                                                    ImageUrl="~/images/imgadd1.gif" />
                                                               
                                                            </FooterTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="For Gross Profit Calculation" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBoxList ID="chkProfitcalc" runat="server" CssClass="FormatRadioButtonList" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Y" />
                                                                    <asp:ListItem Text="No" Value="N" />
                                                                </asp:CheckBoxList>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAdd" runat="server" ValidationGroup="valGroup1" OnClick="btnAdd_Click"
                                                                    ImageUrl="~/images/imgadd1.gif" />
                                                                <%--  <asp:Button ID="btnAdd" runat="server" ValidationGroup='valGroup1' Text="Add Group"  OnClick="btnAdd_Click" />--%>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" OnClientClick="javascript:return validate();"
                                                    Style="font-size: small" Text="Submit" OnClick="btnAssign_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="550px" style="border: 1px solid #000">
                                        <tr id="tblupdategroups" runat="server">
                                            <td>
                                                <asp:GridView ID="gvupdate" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="550px" GridLines="None" DataKeyNames="id" ShowFooter="true" OnRowDeleting="gvDetails_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:TemplateField HeaderText="Group Name" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtGName" Text='<%# Bind("group_name") %>' runat="server" Width="190px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="center" id="trbtnupdate" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnupdate" runat="server" OnClientClick="javascript:return validateupdate();"
                                                    CssClass="esbtn" Style="font-size: small" Text="Submit" OnClick="btnupdate_Click" />
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
