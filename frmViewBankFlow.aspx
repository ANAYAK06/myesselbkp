<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmViewBankFlow.aspx.cs"
    Inherits="Admin_frmViewBankFlow" Title="Bank Flow- Essel Projects Pvt. Ltd."
    EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style9
        {
            height: 25px;
        }
    </style>
    <script language="javascript">
        function validate() {
            var objs = new Array("<%=ddltypeofpay.ClientID %>", "<%=ddlsubtype.ClientID %>", "<%=ddlsub.ClientID %>", "<%=ddlcctype.ClientID %>", "<%=ddltype.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlpo.ClientID %>", "<%=ddldetailhead.ClientID %>");
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
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <table class="estbl eslbl" width="660px">
                                <tr>
                                    <th align="center" class="style9">
                                        View Bank Flow
                                    </th>
                                </tr>
                                <tr>
                                    <td class="innertab">
                                        <table>
                                            <tr>
                                                <td>
                                                    Transaction Type:
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Transction Type"
                                                        AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                        CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                                        <asp:ListItem>Credit</asp:ListItem>
                                                        <asp:ListItem>Debit</asp:ListItem>
                                                        <asp:ListItem>Transfer</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table id="paytype" visible="false" runat="server" class="innertab">
                                                    <tr id="trpaytype" runat="server">
                                                        <td>
                                                            Category of Payment:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddltypeofpay" runat="server" ToolTip="Type of Payment" AutoPostBack="true"
                                                                CssClass="esddown" OnSelectedIndexChanged="ddltypeofpay_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlsub" runat="server" ToolTip="Type" CssClass="esddown" OnSelectedIndexChanged="ddlsub_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlsubtype" runat="server" ToolTip="Type" AutoPostBack="true"
                                                                CssClass="esddown" OnSelectedIndexChanged="ddlsubtype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trcctype" runat="server">
                                                        <td align="center" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Cost Center Type"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddlcctype" AutoPostBack="true" runat="server" ToolTip="Cost Center"
                                                                Width="200px" CssClass="esddown" OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Performing</asp:ListItem>
                                                                <asp:ListItem>Non-Performing</asp:ListItem>
                                                                <asp:ListItem>Capital</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center" style="width: 150px">
                                                            <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Sub Type"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="200px" CssClass="esddown"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Service</asp:ListItem>
                                                                <asp:ListItem Value="2">Trading</asp:ListItem>
                                                                <asp:ListItem Value="3">Manufacturing</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table class="innertab" visible="false" runat="server" id="cc">
                                                                <tr>
                                                                    <td>
                                                                        CC-Code:
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="175px"
                                                                            CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="400px">
                                                                        <table width="100%" class="innertab" runat="server" id="Dca">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbldca" runat="server" CssClass="eslbl" Text="DCA"></asp:Label>
                                                                                </td>
                                                                                <td align="left" valign="top">
                                                                                    <asp:DropDownList ID="ddldetailhead" CssClass="esddown" runat="server" Width="105px"
                                                                                        onchange="SetDynamicKey('dp3',this.value);" />
                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown2" TargetControlID="ddldetailhead" ServicePath="cascadingDCA.asmx"
                                                                                        Category="dca" LoadingText="Loading DCA" ServiceMethod="viewcash" runat="server">
                                                                                    </cc1:CascadingDropDown>
                                                                                    <asp:Label ID="Label3" class="ajaxspan" runat="server"></asp:Label>
                                                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                                                                        TargetControlID="Label3" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                        ServiceMethod="GetDCAName">
                                                                                    </cc1:DynamicPopulateExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblsubdca" CssClass="eslbl" runat="server" Text="Sub DCA"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlsubdetail" CssClass="esddown" Width="105px" runat="server"
                                                                                        onchange="SetDynamicKey('dp4',this.value);" />
                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown3" TargetControlID="ddlsubdetail" ServicePath="cascadingDCA.asmx"
                                                                                        Category="dca" ParentControlID="ddldetailhead" LoadingText="Loading DCA" ServiceMethod="viewsubdca"
                                                                                        runat="server">
                                                                                    </cc1:CascadingDropDown>
                                                                                    <asp:Label ID="Label4" class="ajaxspan" runat="server"></asp:Label>
                                                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender4" BehaviorID="dp4" runat="server"
                                                                                        TargetControlID="Label4" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                        ServiceMethod="GetSubDCAName">
                                                                                    </cc1:DynamicPopulateExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="PO" runat="server">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblpono" CssClass="eslbl" runat="server" Text="PO NO"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlpo" CssClass="esddown" Width="105px" runat="server" ToolTip="PO">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Monthly"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server" ToolTip="PO">
                                                                            <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                                                            <asp:ListItem Value="1">Jan</asp:ListItem>
                                                                            <asp:ListItem Value="2">Feb</asp:ListItem>
                                                                            <asp:ListItem Value="3">Mar</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">Sep</asp:ListItem>
                                                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="yearly"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="PO">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="rbtntype" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                            Text="View" OnClientClick="return validate();" OnClick="btnsubmit_Click" />&nbsp
                                        <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small"
                                            OnClick="btncancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt" AllowPaging="True" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging1"
                                Font-Size="X-Small" Font-Names="Verdana" HeaderStyle-Font-Size="11px" PageSize="100"
                                CellPadding="5">
                                <FooterStyle Font-Bold="true" ForeColor="White" BackColor="#424242" HorizontalAlign="Left" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView2" runat="server" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                GridLines="None">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView3" runat="server" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                GridLines="None" ShowFooter="true" OnRowDataBound="GridView3_RowDataBound">
                                <FooterStyle Font-Bold="true" ForeColor="White" BackColor="#424242" HorizontalAlign="Left" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <tr id="trexcel" runat="server">
                <td align="left">
                    <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                    <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                        OnClick="btnExcel_Click" />
                </td>
            </tr>
        </tr>
    </table>
</asp:Content>
