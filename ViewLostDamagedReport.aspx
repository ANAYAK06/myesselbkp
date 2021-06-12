<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ViewLostDamagedReport.aspx.cs"
    Inherits="ViewLostDamagedReport" EnableEventValidation="false" Title="View Lost/Damaged Items - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .gridwidth
        {
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

        function redirect() {
            window.location.href = "Raise Indent.aspx";

        }

    </script>
    <script language="javascript">
        function print() {
            //            w=window.open();
            //            w.document.write('<html><body onload="window.print()">'+content+'</body></html>'); 
            //            w.document.close(); 
            //            setTimeout(function(){w.close();},10);             
            //            return false;
            var grid_obj = document.getElementById("<%=tblprinting.ClientID %>");
            //	var grid_obj = document.getElementById(grid_ID);
            //            if (grid_obj != null) {
            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();
            //		new_window.close();
            //            }
        }
    </script>
    <script language="javascript">
        function validate() {
            var GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var GridView1 = document.getElementById("<%=grdcmc.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                }
            }
            else if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(12).children[0].value == "") {
                        window.alert("Enter Remarks");
                        GridView1.rows(rowCount).cells(12).children[0].focus();
                        return false;
                    }
                }
            }

            document.getElementById("<%=btnmdlupd.ClientID %>").style.display = 'none';
            return true;
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
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>View: Lost\Damaged Items
                                <a class="help" href="" title=""><small>Help</small> </a>
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
                                                                                            <%--<br />
                                                                                            <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp1" runat="server"
                                                                                                TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                                ServiceMethod="GetCCName">
                                                                                            </cc1:DynamicPopulateExtender>--%>
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
                                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                                    CssClass="button" OnClientClick="javascript:return validatesearch()" />&nbsp;&nbsp;
                                                                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="button" />
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
                                                                                                                    View Report</h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                                <img src="/images/load.gif" width="16" height="16" title="loading...">
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
                                                                                                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" BorderColor="White" GridLines="None" HeaderStyle-CssClass="grid-header"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                        PagerStyle-CssClass="grid pagerbar" AllowPaging="true" PageSize="10" OnDataBound="GridView1_DataBound"
                                                                                                        DataKeyNames="ref_no" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowEditing="GridView1_RowEditing2"
                                                                                                        OnRowDataBound="GridView1_RowDataBound" EmptyDataText="There Is No Records" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
                                                                                                        <Columns>
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                                                                                EditImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                                                            <asp:TemplateField HeaderText="Reference No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-VerticalAlign="Bottom">
                                                                                                                <ItemTemplate>
                                                                                                                    <%#Eval("ref_no") %>
                                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="Date" HeaderText="Date" />
                                                                                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
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
                                                                                                <cc1:ModalPopupExtender ID="popindents" BehaviorID="mdlindent" runat="server" TargetControlID="btnModalPopUp"
                                                                                                    PopupControlID="pnlindent" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                                <asp:Panel ID="pnlindent" runat="server" Style="display: none;">
                                                                                                    <table width="600px" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td width="13" valign="bottom">
                                                                                                                <img src="images/leftc.jpg">
                                                                                                            </td>
                                                                                                            <td class="pop_head" align="left" id="approveind" runat="server">
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
                                                                                                                    height: 250px;">
                                                                                                                    <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                                                                        <ContentTemplate>
                                                                                                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
                                                                                                                                <ProgressTemplate>
                                                                                                                                    <div>
                                                                                                                                        <img src="images/load.gif" /></div>
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
                                                                                                                                        <table border="0" class="fields" width="100%" id="Table1" runat="server">
                                                                                                                                            <tr>
                                                                                                                                                <td class="item search_filters item-group" colspan="5" valign="top">
                                                                                                                                                    <div class="group-expand">
                                                                                                                                                    </div>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="4">
                                                                                                                                                    <asp:GridView ID="Grdeditpopup" Width="800px" BorderColor="White" runat="server"
                                                                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                                                                                                                        CssClass="grid-content" DataKeyNames="id" HeaderStyle-CssClass="grid-header"
                                                                                                                                                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                        ShowFooter="true">
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                            <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                                                                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                                                                                                                            <asp:BoundField DataField="item_status" HeaderText="Item Type" />
                                                                                                                                                            <asp:BoundField DataField="Type" HeaderText="Item Condition" />
                                                                                                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtremarks" runat="server" Text='<%#Bind("remarks") %>'
                                                                                                                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                        </Columns>
                                                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td height="20px">
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="2">
                                                                                                                                                    <asp:GridView ID="grdcmc" BorderColor="White" Width="800px" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                        AutoGenerateColumns="false" CssClass="grid-content" DataKeyNames="id" HeaderStyle-CssClass="grid-header"
                                                                                                                                                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                        ShowFooter="true" OnRowDataBound="grdcmc_RowDataBound">
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                            <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                                                                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                                                                                                                            <asp:BoundField DataField="item_status" HeaderText="Item Type" />
                                                                                                                                                            <asp:BoundField DataField="Type" HeaderText="Item Condition" />
                                                                                                                                                            <%--<asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Wrap="true" ItemStyle-Width="20px" />--%>
                                                                                                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtremk" runat="server" ReadOnly="true" Width="200px" Text='<%#Eval("Remarks") %>'
                                                                                                                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField HeaderText="CMC Remarks">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtcmcremarks" runat="server" Width="200px" Text='<%#Eval("cmcremarks") %>'
                                                                                                                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField HeaderText="SA Remarks">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtSAremarks" runat="server"  Width="200px"   Text='' TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                        </Columns>
                                                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="4" align="center">
                                                                                                                                                    <asp:GridView ID="grideviewpopup" runat="server" Width="780px" CssClass="grid-content"
                                                                                                                                                        HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                        BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                                                                        AutoGenerateColumns="true">
                                                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                                                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                                                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td id="print" runat="server" align="center">
                                                                                                                                                    <input class="buttonprint" type="button" onclick="print();" value="Print" title="Print Report">
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr valign="bottom" id="printing" runat="server" style="display: none">
                                                                                                                                                <td align="center">
                                                                                                                                                    <table width="100%" id="tblprinting" class="pestbl" style="border: 1px solid #000;"
                                                                                                                                                        runat="server">
                                                                                                                                                        <tr style="border: 1px solid  #000;">
                                                                                                                                                            <td>
                                                                                                                                                                <table id="Table5" width="100%" runat="server" class="pestbl">
                                                                                                                                                                    <tr style="border: 1px solid  #000;">
                                                                                                                                                                        <td align="center" colspan="2">
                                                                                                                                                                            <asp:Label ID="Label25" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                Text="Essel Projects Pvt Ltd."></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr style="border: 1px solid  #000;">
                                                                                                                                                                        <td align="center" colspan="2">
                                                                                                                                                                            <asp:Label ID="Label26" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                Text="LOST\DAMAGED REPORT"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr style="border: 1px solid #000;">
                                                                                                                                                                        <td width="50%" align="left" style="border: 1px solid #000" valign="top">
                                                                                                                                                                            <asp:Label ID="Label29" runat="server" Text="CC CODE:" Width="100px" Font-Bold="true"
                                                                                                                                                                                CssClass="pestbox" Font-Underline="false"></asp:Label>
                                                                                                                                                                            <asp:Label ID="lblcode" runat="server" Text="" Width="200px" CssClass="pestbox"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td align="left" width="50%" style="border: 1px solid #000" valign="top">
                                                                                                                                                                            <asp:Label ID="Label31" runat="server" Text="Date" Width="100px" Font-Bold="true"
                                                                                                                                                                                CssClass="pestbox" Font-Underline="false"></asp:Label>
                                                                                                                                                                            <asp:Label ID="lbldate" runat="server" Text="" Width="200px" CssClass="pestbox"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr style="border: 1px solid #000">
                                                                                                                                                                        <td colspan="2" style="border: 1px solid #000">
                                                                                                                                                                            <asp:GridView CssClass="" ID="Griddetails" Width="100%" runat="server" AutoGenerateColumns="false">
                                                                                                                                                                                <Columns>
                                                                                                                                                                                    <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="50px">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:BoundField DataField="item_code" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="110px" HeaderText="Item Code" />
                                                                                                                                                                                    <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                                                                                                                                    <asp:BoundField DataField="specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="150px" HeaderText="Specification" />
                                                                                                                                                                                    <asp:BoundField DataField="basic_price" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Standard Price" />
                                                                                                                                                                                    <asp:BoundField DataField="quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Quantity" />
                                                                                                                                                                                    <asp:BoundField DataField="item_status" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Item Status" />
                                                                                                                                                                                    <asp:BoundField DataField="Type" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="50px" HeaderText="Report Type" />
                                                                                                                                                                                </Columns>
                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr style="border: 1px solid #000">
                                                                                                                                                                        <td colspan="2" style="border: 1px solid #000">
                                                                                                                                                                            <asp:GridView CssClass="" ID="Gridremarks" Width="100%" runat="server" AutoGenerateColumns="false">
                                                                                                                                                                                <Columns>
                                                                                                                                                                                    <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="10px">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:BoundField DataField="item_code" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderText="Item Code" />
                                                                                                                                                                                    <asp:BoundField DataField="remarks" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="150px" HeaderText="Site Incharge Remarks" />
                                                                                                                                                                                    <asp:BoundField DataField="cmcremarks" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-Width="150px" HeaderText="CMC Remarks" />
                                                                                                                                                                                    <%--   <asp:TemplateField HeaderText="Site Incharge Remarks" HeaderStyle-BackColor="White"
                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:TextBox ID="txtsiremarks" MaxLength="25" CssClass="peslbl1" runat="server" Width="150px"
                                                                                                                                                                                                Text="" Style="border: None"></asp:TextBox>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderText="CMC Remarks" HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Left">
                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                            <asp:TextBox ID="txtcmcremarks" MaxLength="25" CssClass="peslbl1" runat="server"
                                                                                                                                                                                                Width="150px" Text="" Style="border: None"></asp:TextBox>
                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                    </asp:TemplateField>--%>
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
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr id="btnu" runat="server">
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                                                                                                                        <asp:Button ID="btnmdlupd" runat="server" Text="Submit" OnClientClick="javascript:return validate()"
                                                                                                                                            CssClass="button" OnClick="btnmdlupd_Click" />
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
                                                                                                        <%--   <tr>
                                                                                                            <td width="13" valign="bottom">
                                                                                                                <img src="images/ldcorner.jpg" style="background: url(images/icon.png) left -172px no-repeat;">
                                                                                                            </td>
                                                                                                            <td class="pop_head">
                                                                                                            </td>
                                                                                                            <td width="13" valign="bottom">
                                                                                                                <img src="images/rightdcorner.jpg" width="13" height="13" style="background: url(images/icon.png) -14px -172px no-repeat;">
                                                                                                            </td>
                                                                                                        </tr>--%>
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
