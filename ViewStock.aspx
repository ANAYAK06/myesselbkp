<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ViewStock.aspx.cs"
    Inherits="ViewStock" Title="View Stock - Essel Projects Pvt.Ltd." EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function searchvalidate() {

            var objs = new Array("<%=ddlsearch.ClientID %>", "<%=txtSearch.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var status = document.getElementById("<%=ddlitemstatus.ClientID %>").value;
            var ctrlstatus = document.getElementById("<%=ddlitemstatus.ClientID %>");

            var ccode = document.getElementById("<%=ddlcccode.ClientID %>").value;
            var ccodectrl = document.getElementById("<%=ddlcccode.ClientID %>");
            if (ccode == "Select Cost Center") {
                window.alert("Select  Cost Center");
                ccodectrl.focus();
                return false;
            }
            else if (ccode == "CC-33" && status == "Select") {
                window.alert("Select  Stock Type");
                ctrlstatus.focus();
                return false;
            }
            else if (status != "0" && ccode != "CC-33") {
                window.alert("Select Stock");
                ctrlstatus.focus();
                return false;
            }





        }
    </script>
    <script language="javascript">
        function print() {
            //            w=window.open();
            //            w.document.write('<html><body onload="window.print()">'+content+'</body></html>'); 
            //            w.document.close(); 
            //            setTimeout(function(){w.close();},10);             
            //            return false;
            var grid_obj = document.getElementById("<%=tblpo.ClientID %>");
            //	var grid_obj = document.getElementById(grid_ID);
            //            if (grid_obj != null) {
            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();
            //		new_window.close();
            //            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function restrictComma() {
            if (event.keyCode == 188) {
                alert("Can't Enter Comma");
                event.returnValue = false;
            }
        }
        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode == 8 || charCode == 46) return false;

            return true;
        }

        function ClearTextboxes() {
            document.getElementById("<%=txtSearch.ClientID %>").value = '';

        }

           
    </script>
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
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>View Stock <a class="help"
                                    href="" title=""><small>Help</small> </a>
                            </h1>
                            <table width="90%">
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
                                                                <asp:UpdatePanel ID="update" UpdateMode="Conditional" runat="server">
                                                                    <ContentTemplate>
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
                                                                                                    <input style="display: none;" type="checkbox" id="Checkbox1" name="" class="grid-domain-selector">
                                                                                                </div>
                                                                                            </div>
                                                                                            <table border="0" class="fields" width="100%">
                                                                                                <tbody>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </div>
                                                                                        <td class="label search_filters search_fields" colspan="4" align="center">
                                                                                            <table class="search_table">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td class="item item-char" valign="middle" width="" colspan="1">
                                                                                                            <asp:Label ID="Label1" runat="server" Text="Search Type"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="item item-char" valign="middle" align="center">
                                                                                                            <span class="filter_item">
                                                                                                                <asp:DropDownList ID="ddlsearch" ToolTip="Select View Type" CssClass="char" runat="server"
                                                                                                                    OnSelectedIndexChanged="ddlsearch_SelectedIndexChanged" AutoPostBack="true">
                                                                                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                                                    <asp:ListItem Value="0">Normal View</asp:ListItem>
                                                                                                                    <asp:ListItem Value="1">Detail View</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                </tr>
                                                                                <tr id="filters" runat="server">
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
                                                                                                        <asp:Label ID="lblmonth" runat="server" Text="Category"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <span class="filter_item">
                                                                                                            <asp:DropDownList ID="ddlCategory" CssClass="char" runat="server" AutoPostBack="true">
                                                                                                            </asp:DropDownList>
                                                                                                            <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlCategory"
                                                                                                                ServicePath="cascadingDCA.asmx" Category="ddc" LoadingText="Please Wait" ServiceMethod="Category">
                                                                                                            </cc1:CascadingDropDown>
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
                                                                                                    <asp:Label ID="lblYear" runat="server" Text="Major Group"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="item item-selection" valign="middle">
                                                                                                    <asp:DropDownList ID="ddlMajorgroup" CssClass="filter_item" runat="server" OnSelectedIndexChanged="ddlMajorgroup_SelectedIndexChanged"
                                                                                                        AutoPostBack="true">
                                                                                                    </asp:DropDownList>
                                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlMajorgroup"
                                                                                                        ServicePath="cascadingDCA.asmx" ParentControlID="ddlCategory" Category="ddm"
                                                                                                        LoadingText="Please Wait" ServiceMethod="MajorGroup">
                                                                                                    </cc1:CascadingDropDown>
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
                                                                                                    <asp:Label ID="lblcccode" runat="server" Text="Sub Group"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="item item-selection" valign="middle">
                                                                                                    <asp:DropDownList ID="ddlSubgroup" CssClass="filter_item" runat="server">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="filter" runat="server">
                                                                                    <td class="item search_filters item-filtersgroup" valign="top">
                                                                                        <div class="filters-group">
                                                                                            <div style="display: none;">
                                                                                                <div class="filter-a">
                                                                                                    <button type="button" class="filter_with_icon active" title="">
                                                                                                        <img src="" width="16" height="16" alt="">
                                                                                                        Active
                                                                                                    </button>
                                                                                                    <input style="display: none;" type="checkbox" id="Checkbox2" name="" class="grid-domain-selector">
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
                                                                                            <tr>
                                                                                                <td class="item item-selection" valign="middle" width="" colspan="2">
                                                                                                    <asp:Label ID="Label5" runat="server" Text="CC Code"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="item item-selection" valign="middle">
                                                                                                    <asp:DropDownList ID="ddlcccode" CssClass="filter_item" runat="server">
                                                                                                    </asp:DropDownList>
                                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlcccode"
                                                                                                        ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="StoreCC2">
                                                                                                    </cc1:CascadingDropDown>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td class="label search_filters search_fields" id="tdstock" runat="server">
                                                                                        <table class="search_table">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="item item-char" valign="middle" width="" colspan="1">
                                                                                                        <asp:Label ID="Label3" runat="server" Text="Item Status"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="item item-char" valign="middle">
                                                                                                        <span class="filter_item">
                                                                                                            <asp:DropDownList ID="ddlitemstatus" CssClass="char" runat="server">
                                                                                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                                                <asp:ListItem Value="0">Stock</asp:ListItem>
                                                                                                                <asp:ListItem Value="1">New Stock</asp:ListItem>
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
                                                                                                    <%-- <asp:Label ID="Label4" runat="server" Text="Year"></asp:Label>--%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="item item-selection" valign="middle">
                                                                                                </td>
                                                                                                <td width="1%" nowrap="true">
                                                                                                    <div class="filter-a">
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="Searchbox" runat="server">
                                                                                    <td class="label search_filters search_fields" colspan="4">
                                                                                        <table class="search_table">
                                                                                            <tr>
                                                                                                <td class="item m2o_search" valign="middle">
                                                                                                    <asp:TextBox ID="txtSearch" CssClass="m2o_search" ToolTip="Item Search" runat="server"
                                                                                                        Style="background-image: url(images/search_grey.png); background-position: right;
                                                                                                        background-repeat: no-repeat; border-color: #CBCCCC; text-transform: uppercase;"
                                                                                                        onkeydown="restrictComma();return isNumberKey(event,this);"></asp:TextBox>
                                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList1"
                                                                                                        ServicePath="cascadingDCA.asmx" TargetControlID="txtSearch" UseContextKey="True"
                                                                                                        CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                                        MinimumPrefixLength="1" CompletionListElementID="listPlacement">
                                                                                                    </cc1:AutoCompleteExtender>
                                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="Search here.."
                                                                                                        WatermarkCssClass="watermarked" TargetControlID="txtSearch" runat="server">
                                                                                                    </cc1:TextBoxWatermarkExtender>
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
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="view_form_options" width="100%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <asp:Button ID="btnSearch" runat="server" Text="Submit" Height="22px" CssClass="button"
                                                                                    OnClick="btnSearch_Click" OnClientClick="javascript:return searchvalidate()" />&nbsp;&nbsp;
                                                                                <asp:Button ID="btnReset" runat="server" Text="Reset" Height="22px" CssClass="button"
                                                                                    OnClick="btnReset_Click" />
                                                                                <asp:Button ID="Button1" runat="server" CssClass="button" Height="22px" Width="120px"
                                                                                    OnClientClick="javascript:return ClearTextboxes();" Text="Clear SearchBox" />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="grid" runat="server">
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
                                                                                                <table id="_terp_list_grid" class="grid" width="740px" cellspacing="0" cellpadding="0">
                                                                                                    <asp:GridView ID="GridView1" Width="100%" GridLines="None" runat="server" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" ShowFooter="true"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                        PagerStyle-CssClass="grid pagerbar" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="item_code"
                                                                                                        OnDataBound="GridView1_DataBound" AllowPaging="true" EmptyDataText="There Is No Record With Your Selection Criteria">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-VerticalAlign="Bottom">
                                                                                                                <ItemTemplate>
                                                                                                                    <%-- <%#Eval("item_code") %>--%>
                                                                                                                    <asp:Label ID="lblitemcode" runat="server" Text='<%#Bind("item_code") %>'></asp:Label>
                                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="200px"
                                                                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Left"
                                                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" ItemStyle-Width="80px" />
                                                                                                            <asp:BoundField DataField="basic_price" HeaderText="Purchase Price" ItemStyle-Width="20px"
                                                                                                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                                                                                            <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-Width="20px" />
                                                                                                            <asp:BoundField DataField="Quantity" HeaderText="Qty" HeaderStyle-Width="5px" ItemStyle-Width="5px"
                                                                                                                FooterStyle-Width="5px" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <asp:BoundField DataField="Totalvalue" HeaderText="Total Value" FooterStyle-HorizontalAlign="Right"
                                                                                                                HeaderStyle-Width="45px" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="45px"
                                                                                                                FooterStyle-Width="45px" ItemStyle-HorizontalAlign="Right" />
                                                                                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
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
                                                                                                    <asp:GridView ID="Gv" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                                                                                        TabIndex="1" Width="100%" ShowFooter="False" CellPadding="4" ForeColor="#333333"
                                                                                                        GridLines="Both">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Bottom">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblitemcode" runat="server" Text='<%#Bind("item_code") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Left"
                                                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Left"
                                                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                            <asp:BoundField DataField="basic_price" HeaderText="Purchase Price" ItemStyle-HorizontalAlign="Right"
                                                                                                                HeaderStyle-HorizontalAlign="Right" />
                                                                                                            <asp:BoundField DataField="Units" HeaderText="Units" />
                                                                                                            <asp:BoundField DataField="Quantity" HeaderText="Qty" FooterStyle-Width="5px" ItemStyle-HorizontalAlign="Center" />
                                                                                                            <asp:BoundField DataField="Totalvalue" HeaderText="Total Value" FooterStyle-HorizontalAlign="Right"
                                                                                                                HeaderStyle-HorizontalAlign="Right" FooterStyle-Width="45px" ItemStyle-HorizontalAlign="Right" />
                                                                                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                                                                        </Columns>
                                                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                    </asp:GridView>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="pagerbar" id="printbtn" runat="server">
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnprint" runat="server" Text="Print" OnClick="btnprint_Click" />
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
                                                        <tr valign="bottom" id="tblpo" runat="server" style="display: none;">
                                                            <td align="center">
                                                                <table width="100%" class="pestbl">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="100%" class="pestbl">
                                                                                <tr style="border: 1px solid #000">
                                                                                    <td colspan="2" align="center">
                                                                                        <asp:Label ID="lblcode" runat="server" Font-Bold="true" Font-Size="Small" Text=""></asp:Label>
                                                                                    </td>
                                                                                    <%-- <td colspan="1">
                                                                                        <asp:Label ID="lbldate" runat="server" Font-Bold="true" Font-Size="Small" Text=""></asp:Label>
                                                                                    </td>--%>
                                                                                </tr>
                                                                                <tr style="border: 1px solid #000">
                                                                                    <td colspan="2" style="border: 1px solid #000">
                                                                                        <asp:GridView CssClass="" ID="gridprint" Width="100%" runat="server" AutoGenerateColumns="false">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="item_code" HeaderText="Item Code" ItemStyle-Width="200px"
                                                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                                                <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="200px"
                                                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                                                <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                <asp:BoundField DataField="basic_price" HeaderText="Purchase Price" ItemStyle-Width="20px" />
                                                                                                <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-Width="20px" />
                                                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="20px" />
                                                                                                <asp:BoundField DataField="Totalvalue" HeaderText="Total Value" ItemStyle-Width="20px" />
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                    <tr id="trexcel" runat="server">
                        <td style="width: 150px;">
                        </td>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                OnClick="btnExcel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
