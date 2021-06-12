<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmAddContract.aspx.cs"
    Inherits="Admin_frmAddContract" EnableEventValidation="false" Title="Add Contract - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=txtProjname.ClientID %>", "<%=txtDivision.ClientID %>", "<%=txtClientname.ClientID %>", "<%=txtCostomername.ClientID %>", "<%=txtPMname.ClientID %>",
                                "<%=txtContactno.ClientID %>", "<%=txtStartdate.ClientID %>", "<%=txtEnddate.ClientID %>", "<%=ddlCCcode.ClientID %>", "<%=txtjob.ClientID %>");
            return CheckInputs(objs);
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
                <table>
                    <tr valign="top">
                        <td>
                            <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="estbl">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="4" align="center">
                                                <asp:Label ID="Label1" runat="server" CssClass="esfmhead" Text="Add Contract Form"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th valign="top" colspan="4" align="center">
                                                <asp:Label ID="Label2" runat="server" CssClass="esfmsubhead" Text="Project Information"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Project Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProjname" runat="server" CssClass="estbox" MaxLength="50" ToolTip="Project Name"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Division"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDivision" runat="server" CssClass="estbox" MaxLength="50" ToolTip="Division"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Client Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtClientname" runat="server" CssClass="estbox" MaxLength="50" ToolTip="Client Name"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Customer Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCostomername" runat="server" CssClass="estbox" MaxLength="50"
                                                    ToolTip="Customer Name"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Project Manager Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPMname" runat="server" CssClass="estbox" MaxLength="50" ToolTip="Project Manager Name"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Contact No" ToolTip="Contact"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContactno" runat="server" CssClass="estbox" ToolTip="Contact No"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="Start Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtStartdate" runat="server" onkeydown="return DateReadonly();"
                                                    CssClass="estbox" ToolTip="Strat Date"></asp:TextBox><span class="starSpan">*</span>&nbsp;<img
                                                        onclick="scwShow(document.getElementById('<%=txtStartdate.ClientID %>'),this);"
                                                        alt="" src="images/cal.gif" style="left: 3px; position: relative; top: 6px; width: 15px;
                                                        height: 15px;" id="cldrDob" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="End Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEnddate" runat="server" onkeydown="return DateReadonly();" CssClass="estbox"
                                                    ToolTip="End Date"></asp:TextBox><span class="starSpan">*</span>&nbsp;<img onclick="scwShow(document.getElementById('<%=txtEnddate.ClientID %>'),this);"
                                                        alt="" src="images/cal.gif" style="left: 3px; position: relative; top: 6px; width: 15px;
                                                        height: 15px;" id="cldrDob1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="CC Code"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlCCcode" CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);"
                                                    runat="server" ToolTip="Cost Center" OnSelectedIndexChanged="ddlCCcode_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlcccode"
                                                    ServicePath="cascadingDCA.asmx" Category="cc" LoadingText="Loading CC" ServiceMethod="costcode"
                                                    PromptText="Select Cost Center">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp1" runat="server"
                                                    TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                    ServiceMethod="GetCCName">
                                                </cc1:DynamicPopulateExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" CssClass="eslbl" Text="Nature Of Job"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtjob" runat="server" CssClass="estbox" TextMode="MultiLine" onkeypress="return imposeMaxLength(this,200);"
                                                    Width="333px" MaxLength="5" ToolTip="Nature of Job"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <%--  <tr>
                                            <th valign="top" colspan="4" align="center">
                                                <asp:Label ID="Label12" runat="server" CssClass="esfmsubhead" Text="Purchase Order Information"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="PO No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPO" runat="server" CssClass="estbox" ToolTip="PO No"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="PO Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPodate" runat="server" onkeydown="return DateReadonly();" CssClass="estbox"
                                                    Height="22px" MaxLength="50" ToolTip="PO Date"></asp:TextBox><span class="starSpan">*</span>&nbsp;<img
                                                        onclick="scwShow(document.getElementById('<%=txtPodate.ClientID %>'),this);"
                                                        alt="" src="images/cal.gif" style="left: 3px; position: relative; top: 6px; width: 15px;
                                                        height: 15px;" id="cldrDob2" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="Basic Value"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBasic" runat="server" CssClass="estbox" ToolTip="Basic Value"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTax" runat="server" CssClass="estbox" ToolTip="Service Tax"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnAddContract" runat="server" CssClass="esbtn" Text="Add Contract"
                                                    OnClientClick="javascript:return validate();" OnClick="btnAddContract_Click" />&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="esbtn" OnClick="btnCancel_Click" />
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
</asp:Content>
