<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AddAgency.aspx.cs"
    Inherits="AddAgency" Title="Untitled Page" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {

            var name = document.getElementById("<%=txtagencyname.ClientID %>").value;
            var address = document.getElementById("<%=txtagencyaddress.ClientID %>").value;

            if (name == "") {
                window.alert("Add Agency Name");
                document.getElementById("<%=txtagencyname.ClientID %>").focus();
                return false;
            }
            else if (address == "") {
                window.alert("Add Agency Address");
                document.getElementById("<%=txtagencyaddress.ClientID %>").focus();
                return false;
            }
            document.getElementById("<%=btnAddagency.ClientID %>").style.display = 'none';
            return true;


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
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table style="width: 700px">
                            <tr valign="top">
                                <td align="center">
                                    <table class="estbl" width="400px">
                                        <tr style="border: 1px solid #000">
                                            <th colspan="2">
                                                <asp:Label ID="itform" CssClass="esfmhead" runat="server" Text="Add Agency Form"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblagencyname" CssClass="eslbl" runat="server" Text="Agency Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtagencyname" CssClass="estbox" Width="100%" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblithead" CssClass="eslbl" runat="server" Text="Agency Address"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtagencyaddress" CssClass="estbox" Width="100%" runat="server"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="btnAddagency" CssClass="esbtn" OnClientClick="return validate();"
                                                    runat="server" Text="Add Agency" OnClick="btnAddagency_Click" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnresetagency" runat="server" CssClass="esbtn" Text="Reset" />
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
