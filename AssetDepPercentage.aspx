<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AssetDepPercentage.aspx.cs"
    Inherits="AssetDepPercentage" Title="Untitled Page" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=txtdep.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnSave.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr runat="server">
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td style="width: 90%" valign="top">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnl" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <h1>
                            CC Asset Depreciation<a class="help" href="/view_diagram/process?res_model=hr.employee&amp;res_id=False&amp;title=Employees"
                                title="Corporate Intelligence..."> <small>Help</small></a></h1>
                        <table align="center">
                            <tr>
                                <td style="height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Current Depreciation Rate  " CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center" style="width: 200px;">
                                    <asp:TextBox ID="txtolddep" runat="server" Width="150px" Enabled="false" CssClass=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="To be Changed As  " CssClass="eslbl"></asp:Label>
                                </td>
                                <td align="center" style="width: 200px;">
                                    <asp:TextBox ID="txtdep" runat="server" Width="150px" ToolTip="Asset Depriciation"
                                        onkeypress="javascript:return isNumberKey(event);" CssClass=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnSave" Height="18px" runat="server" Text="Submit" CssClass="button"
                                        OnClientClick="javascript:return validate() " OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
