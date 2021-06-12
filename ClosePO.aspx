<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ClosePO.aspx.cs"
    Inherits="ClosePO" Title="Close Purchase Order" EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function validate() {
            var objs = new Array("<%=ddlvendor.ClientID %>", "<%=ddlpo.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }

        function validate1() {
            var objs = new Array("<%=txtSAdesc.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID%>").value;
            if (role == "Sr.Accountant") {
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            else if (role = "Project Manager") {
                var objspm = new Array("<%=txtPMdesc.ClientID %>");
                if (!CheckInputs(objspm)) {
                    return false;
                }
            }
            else if (role = "HoAdmin") {
                var objsho = new Array("<%=txtHOdesc.ClientID %>");
                if (!CheckInputs(objsho)) {
                    return false;
                }
            }
            else if (role = "SuperAdmin") {
                var objsa = new Array("<%=txtSdesc.ClientID %>");
                if (!CheckInputs(objsa)) {
                    return false;
                }
            }
            document.getElementById("<%=btnclose.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td style="width: 750px">
                <asp:UpdatePanel ID="upd" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upd" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table>
                            <tr>
                                <th align="left" style="width: 750px; font-size: x-large;" colspan="4">
                                    Close SPPO
                                </th>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid; height: 30px;" align="center"
                                    colspan="2">
                                    <asp:Label ID="lblcccode" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                    &nbsp&nbsp
                                    <asp:DropDownList ID="ddlcccode" runat="server" CssClass="esddown" AutoPostBack="true"
                                        ToolTip="CC Code" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged" Width="200px">
                                    </asp:DropDownList>
                                    <cc1:CascadingDropDown ID="Cascadingcccode" runat="server" TargetControlID="ddlcccode"
                                        ServicePath="cascadingDCA.asmx" PromptText="Select CC Code" Category="cc" LoadingText="Please Wait"
                                        ServiceMethod="newcostcodenew">
                                    </cc1:CascadingDropDown>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid; height: 30px;" align="center"
                                    colspan="2">
                                    <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Vendor ID" Width="100px"></asp:Label>&nbsp&nbsp
                                    <asp:DropDownList ID="ddlvendor" AutoPostBack="true" Width="235px" CssClass="esddown"
                                        ToolTip="Select Vendor" runat="server" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged"
                                        Height="20px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid; height: 30px;" align="center"
                                    colspan="2">
                                    <asp:Label ID="lblpo" CssClass="eslbl" runat="server" Text="PO No" Width="125px"></asp:Label>
                                    &nbsp&nbsp
                                    <asp:DropDownList ID="ddlpo" runat="server" ToolTip="Po" Width="150px" CssClass="esddown">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hfrole" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="esbtn" Text="View" OnClick="btnSearch_Click"
                                        OnClientClick="javascript:return validate()" Style="font-size: small" />
                                </td>
                            </tr>
                        </table>
                        <table class="style1" id="tblpodata" runat="server">
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                    <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="PO No"></asp:Label>&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp
                                    <asp:TextBox ID="txtpono" CssClass="estbox" runat="server" ToolTip="PO NO" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                    <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="PO Date"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                    <asp:TextBox ID="txtpodate" CssClass="estbox" runat="server" ToolTip="PO Date" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                    <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                    &nbsp; &nbsp;
                                    <asp:TextBox ID="txtcccode" CssClass="estbox" runat="server" ToolTip="CC Code" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                    <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="DCA Code"></asp:Label>
                                    &nbsp; &nbsp;
                                    <asp:TextBox ID="txtdcacode" CssClass="estbox" runat="server" ToolTip="DCA Code"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                    <asp:Label ID="Label7" CssClass="eslbl" runat="server" Text="PO Value"></asp:Label>
                                    &nbsp;
                                    <asp:TextBox ID="txtpovalue" CssClass="estbox" runat="server" ToolTip="PO Value"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                    <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="SubDca Code"></asp:Label>
                                    &nbsp;
                                    <asp:TextBox ID="txtsdca" CssClass="estbox" runat="server" ToolTip="SubDca Code"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                    <asp:Label ID="Label6" CssClass="eslbl" runat="server" Text="Balance"></asp:Label>
                                    &nbsp;
                                    <asp:TextBox ID="txtbalance" CssClass="estbox" runat="server" ToolTip="Balance" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                                    <asp:Label ID="Label13" CssClass="eslbl" runat="server" Text="Closing Date"></asp:Label>
                                    &nbsp;
                                    <asp:TextBox ID="txtclsdate" CssClass="estbox" runat="server" ToolTip="Closing Date"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                    <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="PO Remarks" Style="color: Black;
                                        font-family: Arial; font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                    <asp:TextBox ID="txtremarks" CssClass="estbox" runat="server" ToolTip="PO Remarks"
                                        Style="color: #000000; overflow: auto; font-family: Tahoma; text-decoration: none;"
                                        TextMode="MultiLine" Height="40px" Width="100%" onkeydown="javascript:return false;return fasle;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                    <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="PO Closing Remarks"
                                        Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                    <asp:TextBox ID="txtSAdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                        Style="color: #000000; font-family: Tahoma; text-decoration: none;" TextMode="MultiLine"
                                        Width="100%" Height="40px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="right" colspan="4">
                                    <asp:Label ID="lblSAname" CssClass="eslbl" runat="server" Text="by Sr. Accountant"
                                        Style="color: Black; font-family: Arial; font-weight: bold" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trpm" runat="server">
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="PO Closing Remarks"
                                                    Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                <asp:TextBox ID="txtPMdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Style="color: #000000; font-family: Tahoma; text-decoration: none;" TextMode="MultiLine"
                                                    Width="100%" Height="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="right" colspan="4">
                                                <asp:Label ID="lblPMname" CssClass="eslbl" runat="server" Text="by Project Manager"
                                                    Style="color: Black; font-family: Arial; font-weight: bold" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trho" runat="server">
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="PO Closing Remarks by "
                                                    Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                <asp:TextBox ID="txtHOdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Style="color: #000000; font-family: Tahoma; text-decoration: none;" TextMode="MultiLine"
                                                    Width="100%" Height="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="right" colspan="4">
                                                <asp:Label ID="lblHOName" CssClass="eslbl" runat="server" Text="by HO Admin" Style="color: Black;
                                                    font-family: Arial; font-weight: bold" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trSA" runat="server">
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="PO Closing Remarks"
                                                    Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                                <asp:TextBox ID="txtSdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Style="color: #000000; font-family: Tahoma; text-decoration: none;" TextMode="MultiLine"
                                                    Width="100%" Height="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                                    &nbsp;
                                    <asp:Button ID="btnclose" runat="server" CssClass="esbtn" Style="font-size: small;"
                                        Text="Close" OnClick="btnclose_Click" OnClientClick="javascript:return validate1()" />
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                    <asp:Button ID="btnreject" runat="server" CssClass="esbtn" Style="font-size: small;"
                                        Text="Reject" OnClick="btnreject_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
