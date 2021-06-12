<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ViewAccountdetails.aspx.cs"
    Inherits="ViewAccountdetails" Title="View Account Details" EnableEventValidation="false" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function closepopup() {
            $find('mdledit').hide();


        }
        function showpopup() {
            $find('mdledit').show();

        }
    </script>
    <script language="javascript">
        function validate() {
            grid = document.getElementById("<%=GridView3.ClientID %>");

            if (grid.rows[grid.rows.length - 1].cells[1].innerText == "") {
                window.alert("Please Select Cost Center");

                return false;
            }
        }

    </script>
    <script language="javascript">

        function updcheck() {
            var role = document.getElementById("<%=ddlupdrole.ClientID %>").value;
            var ctrlrole = document.getElementById("<%=ddlupdrole.ClientID %>");
            var cccode = document.getElementById("<%=ddlupdcc.ClientID %>").value;
            var ctrlcccode = document.getElementById("<%=ddlupdcc.ClientID %>");
            if (role == "") {
                alert("Select Role");
                ctrlrole.focus();
                return false;

            }
            if ((role == "StoreKeeper" || role == "Accountant") && cccode == "") {

                alert("Select Cost Center");
                ctrlcccode.focus();
                return false;

            }
        }
    </script>
    <script language="javascript">
        function RoleChange() {

            var btn = document.getElementById("<%=Button2.ClientID %>");
            btn.click();
        }
   
    </script>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>View Account Details <a class="help"
                                    href="" title=""><small>Help</small> </a>
                            </h1>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <div id="body_form">
                                            <div>
                                                <div id="server_logs">
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>--%>
                                                            <div class="box-a list-a">
                                                                <div class="inner">
                                                                    <table id="Table1" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                        <tbody>
                                                                            <tr class="pagerbar">
                                                                                <td class="pagerbar-cell" align="right">
                                                                                    <table class="pager-table">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="pager-cell">
                                                                                                    <h2>
                                                                                                    </h2>
                                                                                                </td>
                                                                                                <%--<td class="loading-list" style="display: none;">
                                                                                                                <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                                                                            </td>--%>
                                                                                                <td class="pager-cell" style="width: 90%" valign="middle">
                                                                                                    <div class="pager">
                                                                                                        <div align="right">
                                                                                                            <asp:Label ID="Label2" CssClass="item item-char" runat="server" Text="Change Limit:"></asp:Label>
                                                                                                            <asp:DropDownList ID="ddlpagecount" runat="server" OnSelectedIndexChanged="ddlpagecount_SelectedIndexChanged"
                                                                                                                AutoPostBack="true">
                                                                                                                <asp:ListItem Selected="True">10</asp:ListItem>
                                                                                                                <asp:ListItem>20</asp:ListItem>
                                                                                                                <asp:ListItem>50</asp:ListItem>
                                                                                                                <asp:ListItem>100</asp:ListItem>
                                                                                                            </asp:DropDownList>
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
                                                                                    <table class="grid" width="100%" align="center">
                                                                                        <asp:GridView ID="GridView1" BorderColor="White" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                            CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                            RowStyle-CssClass=" grid-row char grid-row-odd" AllowPaging="true" PagerStyle-CssClass="grid pagerbar"
                                                                                            DataKeyNames="user_name" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                                                            OnPageIndexChanging="GridView1_PageIndexChanging" OnDataBound="GridView1_DataBound">
                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                            <Columns>
                                                                                                <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                    SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="200px" />
                                                                                                <asp:BoundField DataField="roles" HeaderText="Role" ItemStyle-Width="20px" />
                                                                                                <asp:TemplateField HeaderText="CC Code/Name" HeaderStyle-HorizontalAlign="Left">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:GridView ID="GridView2" runat="server" Width="250px" AutoGenerateColumns="false"
                                                                                                            CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                            RowStyle-CssClass=" grid-row char grid-row-odd" GridLines="None" ShowHeader="false"
                                                                                                            PagerStyle-CssClass="grid pagerbar">
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="cc_code" ItemStyle-Width="50px" />
                                                                                                                <asp:BoundField DataField="cc_name" ItemStyle-HorizontalAlign="Left" />
                                                                                                            </Columns>
                                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                        </asp:GridView>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                            <PagerTemplate>
                                                                                                <asp:ImageButton ID="btnFirst" runat="server" Height="15px" ImageUrl="~/images/pager_first.png"
                                                                                                    CommandArgument="First" CommandName="Page" OnCommand="btnFirst_Command" />&nbsp;
                                                                                                <asp:ImageButton ID="btnPrev" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_left.png"
                                                                                                    CommandArgument="Prev" OnCommand="btnPrev_Command" />
                                                                                                <asp:Label ID="lblpages" runat="server" Text="" Height="15px" CssClass="item item-char"></asp:Label>
                                                                                                of
                                                                                                <asp:Label ID="lblCurrent" runat="server" Text="Label" Height="15px" CssClass="item item-char"></asp:Label>
                                                                                                <asp:ImageButton ID="btnNext" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_right.png"
                                                                                                    CommandArgument="Next" OnCommand="btnNext_Command" />&nbsp;
                                                                                                <asp:ImageButton ID="btnLast" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_last.png"
                                                                                                    CommandArgument="Last" OnCommand="btnLast_Command" />
                                                                                            </PagerTemplate>
                                                                                        </asp:GridView>
                                                                                    </table>
                                                                                    <cc1:ModalPopupExtender ID="popedit" BehaviorID="mdledit" runat="server" TargetControlID="Button1"
                                                                                        PopupControlID="pnledit" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                    <asp:Panel ID="pnledit" runat="server" Style="display: none;">
                                                                                        <table width="600px" border="0" align="center" cellpadding="0" cellspacing="0" id="tblbank"
                                                                                            runat="server">
                                                                                            <tr>
                                                                                                <td width="13" valign="bottom">
                                                                                                    <img src="images/leftc.jpg">
                                                                                                </td>
                                                                                                <td class="pop_head" align="left" id="Td2" runat="server">
                                                                                                    <div class="popclose">
                                                                                                        <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                                                    </div>
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
                                                                                                    <div style="overflow: auto; margin-left: 10px; margin-right: 10px; height: 300px;">
                                                                                                        <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
                                                                                                                    <ProgressTemplate>
                                                                                                                        <div>
                                                                                                                            <img src="images/load.gif" /></div>
                                                                                                                    </ProgressTemplate>
                                                                                                                </asp:UpdateProgress>
                                                                                                                <table runat="server" style="vertical-align: middle;" align="center">
                                                                                                                    <tr id="uprole" runat="server">
                                                                                                                        <td class="label" width="1%">
                                                                                                                            <label for="passport_id">
                                                                                                                                Role
                                                                                                                            </label>
                                                                                                                            :
                                                                                                                        </td>
                                                                                                                        <td class="item item-char" valign="middle">
                                                                                                                            <asp:DropDownList ID="ddlupdrole" runat="server" onchange="RoleChange();" CssClass="selection selection_search readonlyfield"
                                                                                                                                ToolTip="Role">
                                                                                                                            </asp:DropDownList>
                                                                                                                            <%--<cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlupdrole"
                                                                                                                                ServiceMethod="role" ServicePath="cascadingDCA.asmx" Category="cc" PromptText="Select Role">
                                                                                                                            </cc1:CascadingDropDown>--%>
                                                                                                                            <asp:Button ID="Button2" runat="server" Style="display: none" OnClick="Button2_Click" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr id="updcc" runat="server">
                                                                                                                        <td class="label" width="1%">
                                                                                                                            <label for="passport_id">
                                                                                                                                CC Code
                                                                                                                            </label>
                                                                                                                            :
                                                                                                                        </td>
                                                                                                                        <td class="item item-char" valign="middle">
                                                                                                                            <asp:DropDownList ID="ddlupdcc" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                                                                ToolTip="CC Code">
                                                                                                                            </asp:DropDownList>
                                                                                                                            <cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" TargetControlID="ddlupdcc"
                                                                                                                                ServiceMethod="WebCC" ServicePath="cascadingDCA.asmx" Category="cc">
                                                                                                                            </cc1:CascadingDropDown>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr id="updgrid" runat="server">
                                                                                                                        <td colspan="5">
                                                                                                                            <table border="0" class="fields" width="100%">
                                                                                                                                <tr>
                                                                                                                                    <td colspan="2" class="item search_filters item-group" valign="top">
                                                                                                                                        <div class="group-expand">
                                                                                                                                        </div>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                            <table id="Table2" runat="server" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                                                                <tbody>
                                                                                                                                    <tr>
                                                                                                                                        <td class="grid-content" colspan="2" align="center">
                                                                                                                                            <asp:GridView ID="GridView3" BorderColor="White" Width="500px" runat="server" AutoGenerateColumns="false"
                                                                                                                                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                                                                DataKeyNames="c_id" OnRowEditing="GridView3_RowEditing" OnRowCancelingEdit="GridView3_RowCancelingEdit"
                                                                                                                                                OnRowUpdating="GridView3_RowUpdating" OnRowDeleting="GridView3_RowDeleting" OnRowCommand="GridView3_RowCommand"
                                                                                                                                                OnRowCreated="GridView3_RowCreated" ShowFooter="true" OnRowDataBound="GridView3_RowDataBound">
                                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:BoundField DataField="c_id" Visible="false" />
                                                                                                                                                    <asp:TemplateField HeaderText="CC Code">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblfcccode" runat="server" Text='<%#Eval("cc_code")%>'></asp:Label>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                        <EditItemTemplate>
                                                                                                                                                            <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" Width="125px"
                                                                                                                                                                CssClass="esddown">
                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                            <asp:RequiredFieldValidator ID="requiredDDL" runat="server" ControlToValidate="ddlCCcode"
                                                                                                                                                                ErrorMessage="Select CC Code" InitialValue="Select Role"></asp:RequiredFieldValidator>
                                                                                                                                                            <%-- <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlCCcode"
                                                                                                                                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="costcode"
                                                                                                                                                                PromptText="Select Cost Center">
                                                                                                                                                            </cc1:CascadingDropDown>--%>
                                                                                                                                                        </EditItemTemplate>
                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:DropDownList ID="ddlfooterCCcode" runat="server" ToolTip="Cost Center" Width="125px"
                                                                                                                                                                CssClass="esddown" onchange="SetDynamicKey('dp2',this.value);">
                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                            <%--<cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" TargetControlID="ddlfooterCCcode"
                                                                                                                                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="costcode"
                                                                                                                                                                PromptText="Select Cost Center">
                                                                                                                                                            </cc1:CascadingDropDown>--%>
                                                                                                                                                        </FooterTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblccname" runat="server" Text='<%#Eval("cc_name")%>'></asp:Label>
                                                                                                                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" runat="server" TargetControlID="lblccname"
                                                                                                                                                                ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx" ServiceMethod="GetCCName">
                                                                                                                                                            </cc1:DynamicPopulateExtender>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:Label ID="lblfccname" runat="server" Text=""></asp:Label>
                                                                                                                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp2" runat="server"
                                                                                                                                                                TargetControlID="lblfccname" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                                                                                                ServiceMethod="GetCCName">
                                                                                                                                                            </cc1:DynamicPopulateExtender>
                                                                                                                                                        </FooterTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:CommandField ShowEditButton="true" ShowCancelButton="true" ShowDeleteButton="true"
                                                                                                                                                        ControlStyle-Width="40px" />
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click"
                                                                                                                                                                OnClientClick="javascript:return validate();" />
                                                                                                                                                        </FooterTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                </Columns>
                                                                                                                                            </asp:GridView>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </tbody>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr id="btn" runat="server">
                                                                                                                        <td align="center" colspan="2">
                                                                                                                            <asp:Button ID="btnupd" runat="server" Height="18px" CssClass="button-a" Text="Update"
                                                                                                                                OnClick="btnupd_Click" OnClientClick="javascript:return updcheck();" />
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
                                                                                                    <asp:Button ID="Button1" runat="server" Text="" Style="display: none" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
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
                                                            <%--</ContentTemplate>
                                                            </asp:UpdatePanel>--%>
                                                        </td>
                                                    </tr>
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
