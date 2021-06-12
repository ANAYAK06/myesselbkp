<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="TransitDays.aspx.cs"
    Inherits="TransitDays" Title="View Transit Days" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .TestStyle
        {
            font: 9pt verdana;
            font-weight: bold;
            color: white;
            background-color: blue;
            border-radius: 15px;
            width: 100px;
        }
    </style>
    <script type="text/javascript">
        function closepopup() {
            $find('mdlindent').hide();
        }
        function showpopup() {
            $find('mdlindent').show();
        }
        function validation() {
            var objs = new Array("<%=ddlDays.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnok.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript">
        function validate() {

            document.getElementById("<%=btnyes.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript">
        function validate1() {

            document.getElementById("<%=btnno.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript">
        function validate2() {

            document.getElementById("<%=btnupdok.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript">
        function validate3() {

            document.getElementById("<%=btnupdno.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Extend Transit Days<a class="help"
                                    href="" title="Corporate Intelligence..."><small>Help</small> </a>
                            </h1>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <div id="body_form">
                                            <div>
                                                <div id="server_logs">
                                                </div>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="box-a list-a">
                                                                            <div class="inner">
                                                                                <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr class="pagerbar">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                                <table class="pager-table">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td class="pager-cell">
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                                <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button">
                                                                                                            </td>
                                                                                                            <td class="pager-cell" style="width: 90%" valign="middle">
                                                                                                                <div class="pager">
                                                                                                                    <div align="right">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="grid-content">
                                                                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0"
                                                                                                    style="background: none;">
                                                                                                    <asp:GridView ID="grdviewtransit" GridLines="None" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        DataKeyNames="Ref_no" OnRowEditing="grdviewtransit_RowEditing" EmptyDataText="There Is No Records">
                                                                                                        <Columns>
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" EditImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                            <asp:TemplateField HeaderText="Ref No">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("Ref_no") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="indent_no" HeaderText="Indent No" />
                                                                                                            <asp:BoundField DataField="cc_code" HeaderText="Sending CC" />
                                                                                                            <asp:BoundField DataField="recieved_cc" HeaderText="Recieved CC" />
                                                                                                            <asp:BoundField DataField="transfer_date" HeaderText="Transfer Date" />
                                                                                                            <asp:BoundField DataField="Expecteddate" HeaderText="Expected Date" />
                                                                                                        </Columns>
                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                    </asp:GridView>
                                                                                                    <cc1:modalpopupextender id="poppo" behaviorid="mdlindent" runat="server" targetcontrolid="btnModalPopUp"
                                                                                                        popupcontrolid="pnlraisepo" backgroundcssclass="modalBackground1" dropshadow="false" />
                                                                                                    <asp:Panel ID="pnlraisepo" runat="server" Style="display: none;">
                                                                                                        <table id="Table1" width="400px" border="0" align="center" runat="server" cellpadding="0"
                                                                                                            cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td width="13" valign="bottom">
                                                                                                                    <img src="images/leftc.jpg">
                                                                                                                </td>
                                                                                                                <td class="pop_head" align="left">
                                                                                                                    <div class="popclose">
                                                                                                                        <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                                                                    </div>
                                                                                                                    <asp:Label ID="lblviewpo" runat="server" Text=""></asp:Label>
                                                                                                                </td>
                                                                                                                <td width="13" valign="bottom">
                                                                                                                    <img src="images/rightc.jpg">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td bgcolor="#FFFFFF">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                                <td height="180" valign="top" class="popcontent">
                                                                                                                    <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                                                                        height: 150px;">
                                                                                                                        <asp:UpdatePanel ID="upslots" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upslots" runat="server">
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
                                                                                                                                <table style="vertical-align: middle;" align="center">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <table id="tbl1" width="100%" runat="server">
                                                                                                                                                <tr>
                                                                                                                                                    <td style="height: 30px">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td width="100%" align="center">
                                                                                                                                                        <asp:Label ID="Label1" runat="server" Text="THE INDENT NO:-" CssClass="char"></asp:Label>
                                                                                                                                                        <asp:Label ID="lblindno" runat="server" Text="" CssClass="char" Font-Bold="true"></asp:Label>
                                                                                                                                                        <asp:Label ID="Label3" runat="server" Text="IS NOT RECEIVED AT:-" CssClass="char"></asp:Label>
                                                                                                                                                        <asp:Label ID="lblrecivedcc" runat="server" Text="" CssClass="char" Font-Bold="true"></asp:Label>
                                                                                                                                                        <asp:Label ID="lbladdress" runat="server" Text="WITHIN THE TRANSIT PERIOD.WEATHER YOU WANT TO EXTEND THE TRANSIT PERIOD ? "
                                                                                                                                                            CssClass="char"></asp:Label>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="height: 10px">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td align="center" id="btn" runat="server">
                                                                                                                                                        <asp:Button ID="btnyes" runat="server" Text="Yes" CssClass="button" OnClick="btnyes_Click"
                                                                                                                                                            OnClientClick="javascript:return validate()" />
                                                                                                                                                        <asp:Button ID="btnno" runat="server" Text="No" CssClass="button" OnClick="btnno_Click"
                                                                                                                                                            OnClientClick="javascript:return validate1()" />
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                            <table id="tbl2" width="100%" runat="server">
                                                                                                                                                <tr>
                                                                                                                                                    <td style="height: 30px">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td>
                                                                                                                                                        <span class="filter_item">
                                                                                                                                                            <asp:DropDownList ID="ddlDays" ToolTip="No of Transit Days" runat="server" CssClass="esddown">
                                                                                                                                                                <asp:ListItem Value="Select No of Days">Select No of Days</asp:ListItem>
                                                                                                                                                                <asp:ListItem>1</asp:ListItem>
                                                                                                                                                                <asp:ListItem>2</asp:ListItem>
                                                                                                                                                                <asp:ListItem>3</asp:ListItem>
                                                                                                                                                                <asp:ListItem>4</asp:ListItem>
                                                                                                                                                                <asp:ListItem>5</asp:ListItem>
                                                                                                                                                                <asp:ListItem>6</asp:ListItem>
                                                                                                                                                                <asp:ListItem>7</asp:ListItem>
                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                        </span>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="height: 10px">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td colspan="2" align="center" style="padding: 1px">
                                                                                                                                                        <asp:Button ID="btnok" runat="server" Text="Ok" CssClass="button" OnClick="btnok_Click"
                                                                                                                                                            OnClientClick="javascript:return validation();" />
                                                                                                                                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="button" OnClick="btncancel_Click" />
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                            <table id="Tbl3" width="100%" runat="server">
                                                                                                                                                <tr>
                                                                                                                                                    <td style="height: 30px">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td width="100%" align="center">
                                                                                                                                                        <asp:Label ID="lbldes" runat="server" Text="ARE YOU SURE YOU WANT TO UPDATE STOCK AT:-"></asp:Label>
                                                                                                                                                        <asp:Label ID="lblupstock" runat="server" Text="" CssClass="char" Font-Bold="true"></asp:Label>
                                                                                                                                                        <asp:Label ID="lb" runat="server" Text="AND INDENT NO:-" CssClass="char"></asp:Label>
                                                                                                                                                        <asp:Label ID="lblind" runat="server" Text="" CssClass="char" Font-Bold="true"></asp:Label>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="height: 10px">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td colspan="2" align="center" style="padding: 1px">
                                                                                                                                                        <asp:Button ID="btnupdok" runat="server" Text="Yes" CssClass="button" OnClick="btnupdok_Click"
                                                                                                                                                            OnClientClick="javascript:return validate2()" />
                                                                                                                                                        <asp:Button ID="btnupdno" runat="server" Text="No" CssClass="button" OnClick="btnupdno_Click"
                                                                                                                                                            OnClientClick="javascript:return validate3()" />
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                                <table style="vertical-align: middle;" align="center">
                                                                                                                                    <tr>
                                                                                                                                        <td align="center" id="btnview" runat="server">
                                                                                                                                            <asp:Button ID="btnclick" runat="server" Text="View Details" CssClass="TestStyle"
                                                                                                                                                OnClick="btnclick_Click" />
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </ContentTemplate>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                                <td bgcolor="#FFFFFF">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="pagerbar">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
