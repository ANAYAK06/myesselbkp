<%@ Page Title="View Tax Numbers" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewTaxnumbers.aspx.cs" Inherits="ViewTaxnumbers" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
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
                        <table style="width: 700px">
                            <tr align="center">
                                <td>
                                    <table class="estbl" width="370px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" align="center" colspan="2">
                                                <asp:Label ID="Label1" runat="server" Text="View Tax Registration" CssClass="eslbl"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="Tr3" runat="server">
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Tax Type" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddltype" Width="200px" ToolTip="Tax Type" runat="server" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Sales/Vat</asp:ListItem>
                                                    <asp:ListItem>Excise</asp:ListItem>
                                                    <asp:ListItem>Service</asp:ListItem>
                                                    <asp:ListItem>GST</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" Text="Tax Registration Number " CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlno" CssClass="esddown" Width="200px" runat="server" ToolTip="Type"
                                                    OnSelectedIndexChanged="ddlno_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table class="estbl" width="470px" align="center" id="tbl" runat="server">
                            <tr id="traddcc" runat="server">
                                <td style="width: 150px" align="center">
                                    <asp:Label ID="Label9" runat="server" Text="Registered Name" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtregistername" runat="server" Enabled="false" Width="200px" ToolTip="Registered Name"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trunder" runat="server">
                                <td style="width: 250px" align="center">
                                    <asp:Label ID="Label10" runat="server" Text="Registred Under" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 420px;">
                                    <asp:TextBox ID="txtregunder" runat="server" Enabled="false" Width="200px" ToolTip="Registred Under"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trupdatecc" runat="server">
                                <td style="width: 250px" align="center">
                                    <asp:Label ID="Label4" runat="server" Text="Registration Number" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtregnumber" runat="server" Width="200px" Enabled="false" ToolTip="Registration Number"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="dcacode" runat="server">
                                <td style="width: 250px" align="center">
                                    <asp:Label ID="Label2" runat="server" Text="Registered Address" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtresaddress" runat="server" Width="200px" Enabled="false" TextMode="MultiLine"
                                        ToolTip="Registered Address" CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td style="width: 150px" align="center">
                                    <asp:Label ID="Label5" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtcomissionerate" runat="server" Width="200px" Enabled="false"
                                        ToolTip="Commissionerate" CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="Tr2" runat="server">
                                <td style="width: 150px" align="center">
                                    <asp:Label ID="Label6" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtdivision" runat="server" Width="200px" ToolTip="Division" Enabled="false"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="sdca" runat="server">
                                <td align="center">
                                    <asp:Label ID="Label7" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtrange" runat="server" Width="200px" ToolTip="Range" Enabled="false"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trjrd" runat="server">
                                <td style="width: 150px" align="center">
                                    <asp:Label ID="Label11" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 420px;">
                                    <asp:TextBox ID="txtjurs" runat="server" Width="200px" ToolTip="Juridiction" Enabled="false"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="sdname" runat="server">
                                <td align="center">
                                    <asp:Label ID="Label8" runat="server" Text="District" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtdistrict" runat="server" Width="200px" ToolTip="District" Enabled="false"
                                        CssClass="esddown" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblstate" runat="server" Text="State" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtstate" runat="server" Width="200px" ToolTip="District" Enabled="false"
                                        CssClass="esddown" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trdesc" runat="server">
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text="Description" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtdesc" runat="server" Width="200px" ToolTip="Description" Enabled="false"
                                        CssClass="esddown" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="estbl" id="tblgst" runat="server" align="center" width="470px">
                            <tr id="trtradename" runat="server">
                                <td style="width: 150px" align="left">
                                    <asp:Label ID="Label14" runat="server" Text="Trade Name" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txttradename" runat="server" Enabled="false" Width="200px" ToolTip="Trade Name"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trlegalname" runat="server">
                                <td style="width: 150px" align="left">
                                    <asp:Label ID="Label15" runat="server" Text="Legal Name" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtlegalname" runat="server" Enabled="false" Width="200px" Height="20px"
                                        ToolTip="Legal Name" CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trregdate" runat="server">
                                <td style="width: 250px" align="left">
                                    <asp:Label ID="Label17" runat="server" Text="Registration Date" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtregdate" CssClass="estbox" Enabled="false" onpaste="return false;"
                                        onkeypress="return false;" runat="server" ToolTip="Registration Date" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trnature" runat="server">
                                <td style="width: 250px" align="left">
                                    <asp:Label ID="Label18" runat="server" Text="Nature of Business" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtnature" runat="server" Enabled="false" Width="200px" ToolTip="Nature of Business"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trregadress" runat="server">
                                <td style="width: 150px" align="left">
                                    <asp:Label ID="Label19" runat="server" Text="Registration Address" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtRegAdress" runat="server" Enabled="false" Width="200px" TextMode="MultiLine"
                                        ToolTip="Registration Address" CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trstatejuridiction" runat="server">
                                <td style="width: 150px" align="left">
                                    <asp:Label ID="Label20" runat="server" Text="State of Jurisdiction" CssClass="eslbl"></asp:Label>
                                </td>
                                <td valign="top" align="center" style="width: 400px;">
                                    <asp:TextBox ID="txtjurisdiction" runat="server" Enabled="false" Width="200px" ToolTip="State of Jurisdiction"
                                        CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trcircle" runat="server">
                                <td style="width: 150px" align="left">
                                    <asp:Label ID="Label21" runat="server" Text="Ward/Circle/Sector" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtward" runat="server" Enabled="false" Width="200px" Height="20px"
                                        ToolTip="Ward/Circle/Sector" CssClass="esddown"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trstatecode" runat="server">
                                <td style="width: 150px" align="left">
                                    <asp:Label ID="Label22" runat="server" Text="State Code" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtdstatecode" runat="server" Enabled="false" Width="200px" ToolTip="State Code"
                                        CssClass="esddown" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trstate" runat="server">
                                <td style="width: 150px" align="left">
                                    <asp:Label ID="Label23" runat="server" Text="State" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtstates" runat="server" Enabled="false" Width="200px" CssClass="esddown"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
