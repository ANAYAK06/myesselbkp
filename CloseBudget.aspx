<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="CloseBudget.aspx.cs"
    Inherits="CloseBudget" Title="Close Budget" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function validate() {
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            if (GridView1 != null) {
                var isValid = false;
                var j = 0;
                for (var i = 1; i < GridView1.rows.length; i++) {
                    var inputs = GridView1.rows[i].getElementsByTagName('input');
                    if (inputs != null) {
                        if (inputs[0].type == "checkbox") {
                            if (inputs[0].checked) {
                                isValid = true;
                                j = j + 1;

                            }
                        }
                    }
                }
                if (parseInt(j) == 0) {
                    alert("Please select atleast one checkbox");
                    return false;
                }
            }
        }
    </script>

    <script type="text/javascript">
        function validataion() {

            var objs = new Array("<%=ddlcctype.ClientID %>", "<%=ddltype.ClientID %>", "<%=ddlyear.ClientID %>")
            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top" align="center">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="estbl" width="400px">
                            <tr style="border: 1px solid #000">
                                <th valign="top" align="center" colspan="2">
                                    <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Close Budget"></asp:Label>
                                </th>
                            </tr>
                            <tr id="tr" runat="server" align="center">
                                <td align="center" colspan="1">
                                    <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Cost Center Type"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcctype" runat="server" AutoPostBack="true" CssClass="esddown" ToolTip="CC Type"
                                        OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged" Width="130px">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Performing</asp:ListItem>
                                        <asp:ListItem>Non-Performing</asp:ListItem>
                                        <asp:ListItem>Capital</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trtype" runat="server">
                                <td align="center" style="width: 120px">
                                    <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Sub Type" ></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="130px" CssClass="esddown"
                                        AutoPostBack="true" onselectedindexchanged="ddltype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Service</asp:ListItem>
                                        <asp:ListItem Value="2">Trading</asp:ListItem>
                                        <asp:ListItem Value="3">Manufacturing</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="tryear" runat="server" align="center">
                                <td align="center">
                                    <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" 
                                        AutoPostBack="true"  runat="server" ToolTip="Year" 
                                        onselectedindexchanged="ddlyear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                   <%-- <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlyear"
                                        ParentControlID="ddlcctype" ServicePath="EsselServices.asmx" LoadingText="Please Wait"
                                        Category="Year" ServiceMethod="FinancialYear" PromptText="Select Year">
                                    </cc1:CascadingDropDown>--%>
                                </td>
                            </tr>
                            <tr align="center" id="btn" runat="server">
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                        Text="View"  OnClick="btnAssign_Click" OnClientClick="javascript:return validataion()"/>
                                    <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Reset" OnClick="btnCancel1_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="600px">
                            <tr align="center">
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                        PagerStyle-CssClass="grid pagerbar" PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        Width="100%" ShowFooter="false" EmptyDataText="There no records">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cc_code" HeaderText="CC_code" />
                                            <asp:TemplateField HeaderText="CC Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="idl" runat="server" Text='<%# Bind("cc_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budget" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="id2" runat="server" Text='<%# Bind("budget_amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Consumed Budget" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="id3" runat="server" Text='<%# Bind("consumed_budget") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="id4" runat="server" Text='<%# Bind("balance") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="center">
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="button" Style="font-size: small"
                                        Text="Close" OnClientClick="javascript:return validate();" OnClick="btnsubmit_Click1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

   

</asp:Content>
