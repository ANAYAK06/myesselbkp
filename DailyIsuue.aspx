<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="DailyIsuue.aspx.cs"
    Inherits="DailyIsuue" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function closepopup() {
            $find('mdldailyissue').hide();


        }
    </script>

    <script language="javascript">
        function print() {
            // w=window.open();
            // w.document.write('<html><body onload="window.print()">'+content+'</body></html>');
            // w.document.close();
            // setTimeout(function(){w.close();},10);
            // return false;
            var grid_obj = document.getElementById("<%=tblpo.ClientID %>");
            // var grid_obj = document.getElementById(grid_ID);

            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();
            // new_window.close();

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
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Search: Daily Issue <a class="help"
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
                                                                                            <br />
                                                                                            <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp1" runat="server"
                                                                                                TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                                ServiceMethod="GetCCName">
                                                                                            </cc1:DynamicPopulateExtender>
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
                                                                                                                </h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                                <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button">
                                                                                                                <%--<asp:Button ID="_terp_list_new" ToolTip="To Raise New Indent" runat="server" Text="New" PostBackUrl="~/Raise Indent.aspx" />
--%>
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
                                                                                                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                        PagerStyle-CssClass="grid pagerbar" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                                                        OnDataBound="GridView1_DataBound" DataKeyNames="Transaction id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                                                                        <Columns>
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                                                                            <asp:TemplateField HeaderText="Indent No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-VerticalAlign="Bottom">
                                                                                                                <ItemTemplate>
                                                                                                                    <%#Eval("Transaction id") %>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="CC Code" HeaderText="CC Code" />
                                                                                                            <asp:BoundField DataField="Date" HeaderText="Date" />
                                                                                                            <asp:BoundField DataField="remarks" HeaderText="Description" />
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
                                                                                                <cc1:ModalPopupExtender ID="popdailyissue" BehaviorID="mdldailyissue" runat="server"
                                                                                                    TargetControlID="btnModalPopUp" PopupControlID="pnldailyissue" BackgroundCssClass="modalBackground1"
                                                                                                    DropShadow="false" />
                                                                                                <asp:Panel ID="pnldailyissue" runat="server" Style="display: none;">
                                                                                                    <table width="800px" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td width="13" valign="bottom">
                                                                                                                <img src="images/leftc.jpg">
                                                                                                            </td>
                                                                                                            <td class="pop_head" align="left" id="viewind" runat="server">
                                                                                                                <div class="popclose">
                                                                                                                    <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                                                                </div>
                                                                                                                View Daily Issued Items
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
                                                                                                                    <table style="vertical-align: middle;" align="center">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table>
                                                                                                                                    <tr>
                                                                                                                                        <td colspan="4" align="center">
                                                                                                                                            <asp:GridView ID="grideviewpopup" runat="server" Width="780px" CssClass="grid-content"
                                                                                                                                                HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar">
                                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                                                            </asp:GridView>
                                                                                                                                            <tr>
                                                                                                                                                <td id="print" runat="server" align="center">
                                                                                                                                                    <input class="buttonSubmit" type="button" onclick="print();" value="Print" title="Print Report">
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr valign="bottom" style="display: none">
                                                                                                                                                <td align="center">
                                                                                                                                                    <table width="100%" id="tblpo" runat="server" class="pestbl" style="border: 1px solid #000;">
                                                                                                                                                        <tr>
                                                                                                                                                            <td>
                                                                                                                                                                <table width="100%" id="Table1" runat="server" class="pestbl">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td style="border: 1px solid  #000;">
                                                                                                                                                                            <table id="Table3" width="100%" runat="server">
                                                                                                                                                                                <tr style="border: 1px solid  #000;">
                                                                                                                                                                                    <td align="center" colspan="2">
                                                                                                                                                                                        <asp:Label ID="Label18" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="Essel Projects Pvt Ltd."></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid  #000;">
                                                                                                                                                                                    <td align="center" colspan="2">
                                                                                                                                                                                        <asp:Label ID="Label21" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="ISSUE VOUCHER"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid  #000;">
                                                                                                                                                                                    <td align="center" colspan="2">
                                                                                                                                                                                        <asp:Label ID="Label19" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="CC:-"></asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtcc" Width="10%" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                                                                                            runat="server"></asp:TextBox>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid #000;">
                                                                                                                                                                                    <td width="50%" align="left" style="border: 1px solid #000" valign="top">
                                                                                                                                                                                        <asp:Label ID="Label20" runat="server" Text="IV NO:" Width="100%" Font-Bold="true"
                                                                                                                                                                                            CssClass="pestbox" Font-Underline="false"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblivno" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td align="left" width="100%" style="border: 1px solid #000" valign="top">
                                                                                                                                                                                        <asp:Label ID="Label23" runat="server" Text="Date" Width="100%" Font-Bold="true"
                                                                                                                                                                                            CssClass="pestbox" Font-Underline="false"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lbldate" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td colspan="2" align="left" style="border: 1px solid #000">
                                                                                                                                                                                        <asp:Label ID="Label28" CssClass="peslbl1" runat="server" Text="">The under mentioned item(s) has/have been issued to various supervisors/labors for executing various jobs. </asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td colspan="2" style="border: 1px solid #000">
                                                                                                                                                                                        <asp:GridView CssClass="" ID="Gridprint" Width="100%" runat="server" AutoGenerateColumns="false">
                                                                                                                                                                                            <Columns>
                                                                                                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:BoundField DataField="Item Name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                                                                                                                                                <asp:BoundField DataField="Specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="150px" HeaderText="Specification" />
                                                                                                                                                                                                <asp:BoundField DataField="Units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px" HeaderText="Units" />
                                                                                                                                                                                                <asp:BoundField DataField="Quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px" HeaderText="Quantity" />
                                                                                                                                                                                            </Columns>
                                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid #000; height: 100px">
                                                                                                                                                                                    <td width="50%" align="left" style="border: 1px solid #000" valign="top">
                                                                                                                                                                                        <asp:Label ID="Label33" Font-Bold="true" runat="server" Text="Issued by:" Width="100%"
                                                                                                                                                                                            CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label34" runat="server" Text="" Width="100%" Height="90px" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label35" runat="server" Text="" Width="100%" CssClass="pestbox">(Signature of Store Keeper)</asp:Label>
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblstorekeepername" runat="server" Style="vertical-align: middle"
                                                                                                                                                                                            Text=""></asp:Label>)
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td align="left" width="100%" style="border: 1px solid #000" valign="top">
                                                                                                                                                                                        <asp:Label ID="Label36" Font-Bold="true" runat="server" Text="Verified By:" Width="100%"
                                                                                                                                                                                            CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label37" runat="server" Text="" Width="100%" Height="90px" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label38" runat="server" Text="" Width="100%" CssClass="pestbox">(Signature of Site Incharge)</asp:Label>
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
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
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
