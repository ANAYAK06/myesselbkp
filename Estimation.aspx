<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Estimation.aspx.cs" Inherits="Estimation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function validate() {
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                var objs = new Array("<%=ddlno.ClientID %>", "<%=txtbrd.ClientID %>", "<%=brdUpload.ClientID %>", "<%=txtfd.ClientID %>", "<%=tdUpload.ClientID %>", "<%=txtdevelopment.ClientID %>", "<%=txtdesc.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            else {
                var objs = new Array("<%=ddlno.ClientID %>", "<%=txtdevelopment.ClientID %>", "<%=txtdesc.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="900px">
        <tr>
            <td align="center">
                <table class="estbl" width="600px">
                    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <tr style="border: 1px solid #000">
                                <th valign="top" align="center" colspan="2">
                                    <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="ESSEL CR'S"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Type"
                                        AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                        CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                        <asp:ListItem>CR</asp:ListItem>
                                        <asp:ListItem>ISSUE</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="NO:-"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlno" runat="server" Width="150px" ToolTip="NO">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </table>
                <table class="estbl" width="600px" >
                    <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <tr id="brd" runat="server">
                                <td>
                                    <asp:Label ID="LBLBRD" runat="server" CssClass="eslbl" Text="BRD Time"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtbrd" runat="server" ToolTip="BRD Time"></asp:TextBox>
                                    <asp:FileUpload ID="brdUpload" runat="server" ToolTip="BRD File" />
                                </td>
                            </tr>
                            <tr id="td" runat="server">
                                <td>
                                    <asp:Label ID="LBLTD" runat="server" CssClass="eslbl" Text="TD Time"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtfd" runat="server" ToolTip="TD Time"></asp:TextBox>
                                    <asp:FileUpload ID="tdUpload" runat="server" ToolTip="TD File" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:Label ID="lbldevelopment" runat="server" CssClass="eslbl" Text="Development Time"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtdevelopment" runat="server" ToolTip="Development Time"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Description"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine" ToolTip="Description"
                                        Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="center" id="btn" runat="server" colspan="2">
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="button" OnClientClick="javascript:return validate();"
                                        OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnreject" runat="server" Text="Reset" CssClass="button" OnClick="btnreject_Click" />
                                </td>
                            </tr>
                            <asp:Label ID="lbllabel" runat="server" Visible="false"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

