<%@ Page Title="Excise Registration Form" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="ExciseRegistrationForm.aspx.cs" Inherits="ExciseRegistrationForm" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=txtregistername.ClientID %>", "<%=txtregnumber.ClientID %>", "<%=txtresaddress.ClientID %>", "<%=txtcomissionerate.ClientID %>", "<%=txtdivision.ClientID %>", "<%=txtrange.ClientID %>", "<%=txtdistrict.ClientID %>", "<%=ddlstate.ClientID %>", "<%=txtdesc.ClientID%>>");
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
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="width: 700px">
                    <tr align="center">
                        <td>
                            <table class="estbl" width="370px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                    </th>
                                </tr>
                                <tr id="trgrid" runat="server" width="470px">
                                    <td align="center">
                                        <asp:GridView ID="gridupdate" runat="server" Width="470px" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                            PagerStyle-CssClass="grid pagerbar" BorderColor="Black" EmptyDataText="No Records to verify"
                                            RowStyle-CssClass=" grid-row char grid-row-odd" GridLines="Both" DataKeyNames="Id"
                                            OnSelectedIndexChanged="gridupdate_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-Width="15px"
                                                    SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                <asp:BoundField DataField="Id" Visible="false" />
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:BoundField DataField="Excise_no" HeaderText="Excise No" />
                                                <asp:BoundField DataField="District" HeaderText="District" ItemStyle-Width="100px" />
                                                <asp:BoundField DataField="State" HeaderText="State" />
                                            </Columns>
                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                            <PagerStyle CssClass="grid pagerbar" />
                                            <HeaderStyle CssClass="grid-header" />
                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr valign="top" id="trtable" runat="server">
                                    <td align="center">
                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="estbl" width="470px">
                                                    <tr id="traddcc" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label9" runat="server" Text="Registered Name" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtregistername" runat="server" Width="200px" ToolTip="Registered Name"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trupdatecc" runat="server">
                                                        <td style="width: 250px" align="center">
                                                            <asp:Label ID="Label4" runat="server" Text="Registration Number" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtregnumber" runat="server" Width="200px" ToolTip="Registration Number"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="dcacode" runat="server">
                                                        <td style="width: 250px" align="center">
                                                            <asp:Label ID="Label2" runat="server" Text="Registered Address" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtresaddress" runat="server" Width="200px" TextMode="MultiLine"
                                                                ToolTip="Registered Address" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr1" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label5" runat="server" Text="Commissionerate" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtcomissionerate" runat="server" Width="200px" ToolTip="Commissionerate"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr2" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label6" runat="server" Text="Division" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtdivision" runat="server" Width="200px" ToolTip="Division" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="sdca" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Range" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtrange" runat="server" Width="200px" ToolTip="Range" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="sdname" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="District" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtdistrict" runat="server" Width="200px" ToolTip="District" CssClass="esddown"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr3" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="State" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlstate" Width="200px" ToolTip="State" runat="server">
                                                                <asp:ListItem>Select State</asp:ListItem>
                                                                <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                                <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                                <asp:ListItem>Assam</asp:ListItem>
                                                                <asp:ListItem>Bihar</asp:ListItem>
                                                                <asp:ListItem>Chhatisgarh</asp:ListItem>
                                                                <asp:ListItem>Goa</asp:ListItem>
                                                                <asp:ListItem>Gujarat</asp:ListItem>
                                                                <asp:ListItem>Haryana</asp:ListItem>
                                                                <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                                <asp:ListItem>Jammu & Kashmir</asp:ListItem>
                                                                <asp:ListItem>Jharkhand</asp:ListItem>
                                                                <asp:ListItem>Karnataka</asp:ListItem>
                                                                <asp:ListItem>Kerala</asp:ListItem>
                                                                <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                                <asp:ListItem>Maharashtra</asp:ListItem>
                                                                <asp:ListItem>Manipur</asp:ListItem>
                                                                <asp:ListItem>Meghalaya</asp:ListItem>
                                                                <asp:ListItem>Mizoram</asp:ListItem>
                                                                <asp:ListItem>Nagaland</asp:ListItem>
                                                                <asp:ListItem>Orissa</asp:ListItem>
                                                                <asp:ListItem>Punjab</asp:ListItem>
                                                                <asp:ListItem>Rajasthan</asp:ListItem>
                                                                <asp:ListItem>Sikkim</asp:ListItem>
                                                                <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                                <asp:ListItem>Tripura</asp:ListItem>
                                                                <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                                <asp:ListItem>Uttaranchal</asp:ListItem>
                                                                <asp:ListItem>West Bengal</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trdesc" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Description" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtdesc" runat="server" Width="200px" ToolTip="Description" CssClass="esddown" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="btn" runat="server">
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnAdd" runat="server" Text="" OnClientClick="javascript:return validate();"
                                                                CssClass="esbtn" OnClick="btnAdd_Click" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="" CssClass="esbtn" OnClick="btnCancel_Click" />
                                                            <%--<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="esbtn" OnClick="btndelete_Click" />--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
