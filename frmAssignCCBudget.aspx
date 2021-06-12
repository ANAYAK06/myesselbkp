<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmAssignCCBudget.aspx.cs"
    Inherits="frmAssignCCBudget" EnableEventValidation="false" Title="Assign CostCenter Budget - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function ccbudget() {
            if (document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex != 0 && document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex != 1) {
                if (document.getElementById("<%=ddlCCcode.ClientID %>").selectedIndex != 0 && document.getElementById("<%=ddlyear.ClientID %>").selectedIndex != 0) {
                    var argu = document.getElementById("<%=ddlCCcode.ClientID %>").value + "|" + document.getElementById("<%=ddlyear.ClientID %>").value;
                    SetDynamicKey('dp2', argu);
                }
                else {
                    document.getElementById("<%=lblccbud.ClientID %>").innerHTML = "";
                }
            }
        }

        function validate() {
            var objs = new Array("<%=ddlCCcode.ClientID %>", "<%=txtBudget.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex == 1) {
                if (document.getElementById("<%=ddltype.ClientID %>").selectedIndex == 0) {
                    alert("Select Sub Type");
                    document.getElementById("<%=ddltype.ClientID %>").focus();
                    return false;
                }
            }
            else if (document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex == 2 || document.getElementById("<%=ddlcctype.ClientID %>").selectedIndex == 3) {
                if (document.getElementById("<%=ddlyear.ClientID %>").selectedIndex == 0) {
                    alert("Select Year");
                    document.getElementById("<%=ddlyear.ClientID %>").focus();
                    return false;
                }
            }
            else {
                var bud = document.getElementById("<%=lblccbud.ClientID %>").innerHTML;
                var nbud = document.getElementById("<%=txtBudget.ClientID %>").value;
                if (bud != "Budget Not Assigned" && bud != "") {
                    alert("Budget Already Assigned to this CC");
                    return false;

                }
            }
            document.getElementById("<%=btnAssign.ClientID %>").style.display = 'none';
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
                <table style="width: 700px">
                    <tr valign="top">
                        <td align="center">
                            <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                                        <ProgressTemplate>
                                            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                    left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                    <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                                    <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                </div>
                                            </asp:Panel>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <table class="estbl" width="400px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="2" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Cost Center Budget Form"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="tr" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Cost Center Type"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddlcctype" runat="server" ToolTip="Cost Center" Width="175px"
                                                    CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Performing</asp:ListItem>
                                                    <asp:ListItem>Non-Performing</asp:ListItem>
                                                    <asp:ListItem>Capital</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trtype" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label16" CssClass="eslbl" runat="server" Text="Sub Type"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="175px" CssClass="esddown"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Service</asp:ListItem>
                                                    <asp:ListItem>Trading</asp:ListItem>
                                                    <asp:ListItem>Manufacturing</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trccode" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddlCCcode" AutoPostBack="true" Width="175px" CssClass="esddown"
                                                    onchange="SetDynamicKey('dp1',this.value);ccbudget();" runat="server" ToolTip="Cost Center">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="year" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="175px" runat="server" ToolTip="Year"
                                                    onchange="ccbudget();">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlyear"
                                                    ServicePath="EsselServices.asmx" LoadingText="Please Wait" Category="Year" ServiceMethod="FinancialYear1"
                                                    PromptText="Select Year">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="lblccbud" runat="server" class="ajaxspan"></asp:Label>
                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp2" runat="server"
                                                    TargetControlID="lblccbud" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                    ServiceMethod="GetCCBudget">
                                                </cc1:DynamicPopulateExtender>
                                            </td>
                                        </tr>
                                        <tr id="trbudget" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Budget Amount"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:TextBox ID="txtBudget" CssClass="estbox" runat="server" Width="175px" ToolTip="Budget Amount"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="trbutton" runat="server">
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Text="Assign Budget" OnClientClick="javascript:return validate();"
                                                    OnClick="btnAssign_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server"
                                                        CssClass="esbtn" Text="Reset" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblAlert" CssClass="eslblalert" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
