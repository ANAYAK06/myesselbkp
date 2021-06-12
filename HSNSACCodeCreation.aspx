<%@ Page Title="HSN/SAC Code Creation" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="HSNSACCodeCreation.aspx.cs" Inherits="HSNSACCodeCreation" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/Javascript">
        function isNumberKeycgst(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = document.getElementById("<%=txtcgstrate.ClientID %>").value.length;
                var index = document.getElementById("<%=txtcgstrate.ClientID %>").value.indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
        function isNumberKeysgst(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = document.getElementById("<%=txtsgstrate.ClientID %>").value.length;
                var index = document.getElementById("<%=txtsgstrate.ClientID %>").value.indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
        function isNumberKeyigst(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = document.getElementById("<%=txtigstrate.ClientID %>").value.length;
                var index = document.getElementById("<%=txtigstrate.ClientID %>").value.indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
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
                                        <asp:Label ID="Label1" runat="server" Text="" CssClass="eslbl">HSN/SAC CodeCreation And Updation</asp:Label>
                                    </th>
                                </tr>
                                <tr valign="top" id="trtable" runat="server">
                                    <td align="center">
                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="estbl" width="470px">
                                                    <tr align="center">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Mode of Transction"
                                                                RepeatDirection="Horizontal" runat="server" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Selected="True"
                                                                    Value="0">Creation</asp:ListItem>
                                                                <asp:ListItem Value="1">Updation</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="Trtypecategory" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Type Category" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlcodecategory" Width="200px" ToolTip="Type Category" runat="server" OnSelectedIndexChanged="ddlcodecategory_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                <asp:ListItem Value="Goods">Goods</asp:ListItem>
                                                                <asp:ListItem Value="Service">Service</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="Trhsncategory" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="HSN Category" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlhsncategory" Width="200px" ToolTip="HSN Category" runat="server" OnSelectedIndexChanged="ddlhsncategory_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                <asp:ListItem Value="Assets">Assets</asp:ListItem>
                                                                <asp:ListItem Value="Semi Assets/Consumables">Semi Assets/Consumables</asp:ListItem>
                                                                <asp:ListItem Value="Consumables">Consumables</asp:ListItem>
                                                                <asp:ListItem Value="Bought Out Items">Bought Out Items</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trhsncode" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label9" runat="server" Text="HSN/SAC Code" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txthsncode" runat="server" Width="200px" ToolTip="HSN/SAC Code"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trhsncodedropdown" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label7" runat="server" Text="HSN/SAC Code" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:DropDownList ID="ddlhsncodes" CssClass="esddown" ToolTip="HSN/SAC Code"
                                                                Width="200px" runat="server" OnSelectedIndexChanged="ddlhsncodes_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trcgstrate" runat="server">
                                                        <td style="width: 250px" align="center">
                                                            <asp:Label ID="Label2" runat="server" Text="CGST Rate" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtcgstrate" runat="server" Width="200px" onkeypress="return isNumberKeycgst(event)"
                                                                ToolTip="CGST Rate" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trsgstrate" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label5" runat="server" Text="SGST Rate" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtsgstrate" runat="server" Width="200px" ToolTip="SGST Rate" onkeypress="return isNumberKeysgst(event)"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trigstrate" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label6" runat="server" Text="IGST Rate" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtigstrate" runat="server" Width="200px" ToolTip="IGST Rate" onkeypress="return isNumberKeyigst(event)" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trhsnrem" runat="server">
                                                        <td style="width: 250px" align="center">
                                                            <asp:Label ID="Label4" runat="server" Text="Remarks" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 400px;">
                                                            <asp:TextBox ID="txtremarks" runat="server" Width="200px" TextMode="MultiLine" ToolTip="Remarks"
                                                                CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="btn" runat="server">
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnAdd" runat="server" Text="Submit" OnClientClick="javascript:return validate();"
                                                                CssClass="esbtn" OnClick="btnAdd_Click" />
                                                            <%--OnClick="btnAdd_Click"--%>
                                                           <%-- <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="esbtn" />--%>
                                                            <%--OnClick="btnCancel_Click"--%>
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
    <script type="text/javascript">
        function validate() {
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                var objs = new Array("<%=ddlcodecategory.ClientID %>")
                if (!CheckInputs(objs)) {
                    return false;
                }
                var codecategory = document.getElementById("<%=ddlcodecategory.ClientID %>").value;
                if (codecategory == "Goods") {
                    var objs = new Array("<%=ddlhsncategory.ClientID %>")
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }
                var objs = new Array("<%=txthsncode.ClientID %>","<%=txtcgstrate.ClientID %>","<%=txtsgstrate.ClientID %>","<%=txtigstrate.ClientID %>","<%=txtremarks.ClientID %>")
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                debugger;
                var objs = new Array("<%=ddlcodecategory.ClientID %>")
                if (!CheckInputs(objs)) {
                    return false;
                }
                var codecategory = document.getElementById("<%=ddlcodecategory.ClientID %>").value;
                if (codecategory == "Goods") {
                    var objs = new Array("<%=ddlhsncategory.ClientID %>")
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }
                var type = document.getElementById("<%=ddlhsncodes.ClientID %>").value;
                if (type == "Select") {
                    alert("Select HSN/SAC Code");
                    return false;
                }
                var objs = new Array("<%=txtcgstrate.ClientID %>","<%=txtsgstrate.ClientID %>","<%=txtigstrate.ClientID %>","<%=txtremarks.ClientID %>")
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
        }
    </script>
</asp:Content>

