<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="SemiAssetDep.aspx.cs" Inherits="SemiAssetDep" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript">

        function validate() {
            var objs = new Array("<%=ddlissue.ClientID %>", "<%=ddltransfer.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }

            document.getElementById("<%=btnupdate.ClientID %>").style.display = 'none';
            return true; 
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table class="estbl" width="370px" align="center">
                    <tr style="border: 1px solid #000">
                        <th valign="top" colspan="4" align="center">
                            <asp:Label ID="Label4" runat="server" Text="Semi Asset/Consumable Depriciation" CssClass="eslbl"></asp:Label>
                        </th>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <tr id="treditdca" runat="server">
                                <td align="center" colspan="2">
                                    <asp:Label ID="Label11" runat="server" Text="Issue From CS Percent" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center" runat="server">
                                    <asp:DropDownList ID="ddlissue" Width="100px" ToolTip="Issue Percent" runat="server">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="Full Value">0</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="40">40</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="60">60</asp:ListItem>
                                        <asp:ListItem Value="70">70</asp:ListItem>
                                        <asp:ListItem Value="80">80</asp:ListItem>
                                        <asp:ListItem Value="90">90</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="treditsudca" runat="server">
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label13" runat="server" Text="Recieved From CC Percent" CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddltransfer" Width="100px" ToolTip="Transfer Percent" runat="server">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        
                                        <asp:ListItem  Value="10">10</asp:ListItem>
                                        <asp:ListItem  Value="20">20</asp:ListItem>
                                        <asp:ListItem  Value="30">30</asp:ListItem>
                                        <asp:ListItem  Value="40">40</asp:ListItem>
                                        <asp:ListItem  Value="50">50</asp:ListItem>
                                        <asp:ListItem  Value="60">60</asp:ListItem>
                                        <asp:ListItem  Value="70">70</asp:ListItem>
                                        <asp:ListItem  Value="80">80</asp:ListItem>
                                        <asp:ListItem  Value="90">90</asp:ListItem>
                                        <asp:ListItem  Value="Full Value">100</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr id="trbtnupdate" runat="server">
                        <td align="center" colspan="4">
                            <asp:Button ID="btnupdate" runat="server" Width="50px" OnClientClick="javascript:return validate()"
                                CssClass="esbtn" OnClick="btnupdate_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
