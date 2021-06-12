<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="MRReport.aspx.cs" Inherits="MRReport" Title="MR Report - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript">
        function closepopup() {
            $find('mdlreport').hide();


        }

        function showpopup() {
            $find('mdlreport').show();

        }
        
       

    </script>
    <script language="javascript">
        function validate() {
            var GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");

            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }


                }
            }
            document.getElementById("<%=Btnrjct.ClientID %>").style.display = 'none';
            return true;

            document.getElementById("<%=btnmdlupd.ClientID %>").style.display = 'none';
            return true;

        } 
    </script>
    <script language="javascript">
        function validate1() {
            var GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");

            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }


                }
            }
            var response = confirm("Do you want to Close the PO");
            if (response) {
                document.getElementById("<%=btnpurchase.ClientID %>").style.display = 'none';
                return true;
            }
            else {


                return true;
            }


        } 
    </script>
    <script language="javascript">


        function validatesearch() {


            var cccode = document.getElementById("<%=ddlcccode.ClientID %>").value;
            var ccode = document.getElementById("<%=ddlcccode.ClientID %>");


            if (cccode == "") {
                window.alert("Please Select Cost Center");
                ccode.focus();
                return false;
            }
        }
    </script>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>View MR Reports <a class="help"
                                    href="" title=""><small>Help</small> </a>
                            </h1>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <div id="body_form">
                                            <div>
                                                <div id="server_logs">
                                                </div>
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="search_filter_data">
                                                                    <table border="0" class="fields" width="100%">
                                                                        <tr>
                                                                            <td class="item search_filters item-filtersgroup" valign="top">
                                                                                <div class="filters-group">
                                                                                    <div style="display: none;">
                                                                                        <div class="filter-a">
                                                                                            <button type="button" class="filter_with_icon active" title="">
                                                                                                <img src="" width="16" height="16" alt="">
                                                                                                Active
                                                                                            </button>
                                                                                            <input style="display: none;" type="checkbox" id="filter_241" name="" class="grid-domain-selector">
                                                                                        </div>
                                                                                    </div>
                                                                                    <table border="0" class="fields" width="100%">
                                                                                        <tbody>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle" width="" colspan="1">
                                                                                                <asp:Label ID="lblmonth" runat="server" Text="Month"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle">
                                                                                                <span class="filter_item">
                                                                                                    <asp:DropDownList ID="ddlMonth" CssClass="char" runat="server">
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
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table">
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle" width="" colspan="2">
                                                                                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle">
                                                                                            <asp:DropDownList ID="ddlyear" CssClass="filter_item" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td width="1%" nowrap="true">
                                                                                            <div class="filter-a">
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table">
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle" width="" colspan="2">
                                                                                            <asp:Label ID="lblcccode" runat="server" Text="CC Code"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle">
                                                                                            <asp:DropDownList ID="ddlcccode" CssClass="filter_item" runat="server">
                                                                                            </asp:DropDownList>
                                                                                            <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                                                ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="codename"
                                                                                                PromptText="Select Cost Center">
                                                                                            </cc1:CascadingDropDown>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" class="item search_filters item-group" valign="top">
                                                                                <div class="group-expand">
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="view_form_options" width="100%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click"
                                                                                    OnClientClick="javascript:return validatesearch()" />&nbsp;&nbsp;
                                                                                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" OnClick="btnReset_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
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
                                                                                                                <h2>
                                                                                                                    Reports</h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                                <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button">
                                                                                                                <%--<asp:Button ID="btnnew" CssClass="button" runat="server" Text="New" OnClientClick="javascript:location.href('RaiseIndent.aspx')" />--%>
                                                                                                            </td>
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
                                                                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                                                                                    <asp:GridView ID="GridView1" GridLines="None" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                        PagerStyle-CssClass="grid pagerbar" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                                                        OnDataBound="GridView1_DataBound" DataKeyNames="po_no" OnRowEditing="GridView1_RowEditing2"
                                                                                                        OnRowDataBound="GridView1_RowDataBound" EmptyDataText="There Is No Records" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                                                                        <Columns>
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                                                                                EditImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                            <asp:TemplateField HeaderText="MRR No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-VerticalAlign="Bottom">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--   <%#Eval("MRR_no") %>--%>
                                                                                                                    <asp:Label ID="lblmrr" runat="server" Text='<%#Eval("MRR_no") %>'></asp:Label>
                                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="po_no" HeaderText="PO No" />
                                                                                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                                                                            <asp:BoundField DataField="Recieved_date" HeaderText="Recieved Date" />
                                                                                                            <asp:BoundField DataField="remarks" HeaderText="Description" />
                                                                                                            <asp:BoundField DataField="POStatus" HeaderText="PO Status" />
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("status")%>' />
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
                                                                                                <cc1:ModalPopupExtender ID="popreports" BehaviorID="mdlreport" runat="server" TargetControlID="btnModalPopUp"
                                                                                                    PopupControlID="pnlreport" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                                <asp:Panel ID="pnlreport" runat="server" Style="display: none;">
                                                                                                    <table width="800px" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td width="13" valign="bottom">
                                                                                                                <img src="images/leftc.jpg">
                                                                                                            </td>
                                                                                                            <td class="pop_head" align="left" id="viewrep" runat="server">
                                                                                                                <div class="popclose">
                                                                                                                    <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                                                                </div>
                                                                                                                <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label>
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
                                                                                                                    height: 300px;">
                                                                                                                    <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                                                                        <ContentTemplate>
                                                                                                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
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
                                                                                                                                        <table border="0" class="fields" width="100%" id="tbl" runat="server">
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="5" class="item search_filters item-group" valign="top">
                                                                                                                                                    <div class="group-expand">
                                                                                                                                                    </div>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                        <table>
                                                                                                                                            <tr id="CST" runat="server">
                                                                                                                                                <td colspan="2">
                                                                                                                                                    <asp:GridView ID="Grdeditpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                        PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                        DataKeyNames="Item_code" ShowFooter="true" OnRowDataBound="Grdeditpopup_RowDataBound">
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="100px" />
                                                                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                                                                            <asp:BoundField DataField="Last Purchased Price" HeaderText="Basic Price" HeaderStyle-Width="50px"
                                                                                                                                                                ItemStyle-Width="50px" />
                                                                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                            <asp:BoundField DataField="Raised Qty" HeaderText="Raised Qty" ItemStyle-Width="25px" />
                                                                                                                                                            <asp:BoundField DataField="quantity" HeaderText="Recieved Qty" ItemStyle-Width="25px" />
                                                                                                                                                        </Columns>
                                                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                                    </asp:GridView>
                                                                                                                                                    <%--<asp:GridView ID="gridcmc" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                        PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                        DataKeyNames="id" OnRowDataBound="gridcmc_RowDataBound">
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="100px" />
                                                                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                            <asp:BoundField DataField="Quantity" HeaderText="Receieved Qty" ItemStyle-Width="25px" />
                                                                                                                                                            <asp:BoundField DataField="basic_price" HeaderText="Last Purchased Price" ItemStyle-Width="25px" />
                                                                                                                                                            <asp:TemplateField HeaderText="New Purchased Price">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtbasic" runat="server" Width="100px" Text='<%#Bind("newbasicprice") %>'
                                                                                                                                                                         onkeypress='IsNumeric1(event)'></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                              <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("newbasicprice")%>' />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                        </Columns>
                                                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                                    </asp:GridView>--%>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td height="20px">
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="4" align="center">
                                                                                                                                                    <%-- <asp:GridView ID="grideviewpopup" runat="server" Width="780px" CssClass="grid-content"
                                                                                                                                                        HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                        BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar">
                                                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                                                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                                                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                                                                    </asp:GridView>--%>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                                                                                                                        <asp:Button ID="btnmdlupd" runat="server" Text="Verified" CssClass="button" OnClick="btnmdlupd_Click"
                                                                                                                                            OnClientClick="javascript:return validate();" />
                                                                                                                                        &nbsp; &nbsp; &nbsp;
                                                                                                                                        <asp:Button ID="Btnrjct" runat="server" Text="Reject" CssClass="button" OnClick="Btnrjct_Click"
                                                                                                                                            OnClientClick="javascript:return validate();" visible="false" />
                                                                                                                                        <asp:Button ID="btnpurchase" runat="server" CssClass="button" OnClientClick="javascript:return validate1();"
                                                                                                                                            Text="Approved" OnClick="btnpurchase_Click" />
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
