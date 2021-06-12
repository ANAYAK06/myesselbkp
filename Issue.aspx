<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Issue.aspx.cs"
    Inherits="Issue" Title="View Issue Items - Essel Projects Pvt.Ltd." EnableEventValidation="false" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        function Print() {

            document.body.offsetHeight;
            window.print();
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

        function showToolTip(e, text) {
            if (document.all) e = event;
            var obj = document.getElementById('bubble_tooltip');
            var obj2 = document.getElementById('bubble_tooltip_content');
            var obj3 = document.getElementById('bubble_tooltip_content');
            obj2.innerHTML = text;
            // obj3.innerHTML=text2;
            obj.style.display = 'block';
            var st = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
            if (navigator.userAgent.toLowerCase().indexOf('safari') >= 0) st = 0;
            var leftPos = e.clientX - 100;
            if (leftPos < 0) leftPos = 0;
            obj.style.left = leftPos + 'px';
            obj.style.top = e.clientY - obj.offsetHeight - 1 + st + 'px';
        }

        function hideToolTip() {
            document.getElementById('bubble_tooltip').style.display = 'none';

        }
        function extender(source, eventArgs) {
            //alert(eventArgs.get_text())
            var text = eventArgs.get_value();

            if (text != "") {
                showToolTip(event, eventArgs.get_value());
                CCcodeCheckerTimer = setTimeout("hideToolTip();", 4000);
            }
            else {
                showToolTip(event, "0");
                CCcodeCheckerTimer = setTimeout("hideToolTip();", 4000);
            }
        }
        
        


    </script>
    <script language="javascript">
        function calpurchase() {
            grd = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var amt = 0;
            var total = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                if (grd.rows(rowCount).cells(4).innerText != "DCA-27") {
                    if (!isNaN(grd.rows(rowCount).cells(10).children[0].value)) {
                        amt = (Number(grd.rows(rowCount).cells[6].innerHTML)) * Number(grd.rows(rowCount).cells[10].children[0].value);
                        grd.rows(rowCount).cells(11).innerHTML = amt;
                        total += Number(grd.rows(rowCount).cells(11).innerHTML);
                    }

                }
            }
            grd.rows[grd.rows.length - 1].cells[11].innerHTML = total;



        }
        function validate() {
            var GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var GridView1 = document.getElementById("<%=gridviewpopup.ClientID %>");
            var GridView3 = document.getElementById("<%=grdpopcmc.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID %>").value;
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    var remainQty = parseInt(parseInt(GridView.rows(rowCount).cells(8).innerText) - parseInt(GridView.rows(rowCount).cells(9).innerText));
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }

                    else if (remainQty < parseInt(GridView.rows(rowCount).cells(10).children[0].value)) {
                        window.alert("You are not able to raise quantity");
                        GridView.rows(rowCount).cells(10).children[0].value = "";
                        calpurchase();
                        GridView.rows(rowCount).cells(10).children[0].focus();
                        return false;
                    }
                    else if (parseInt(GridView.rows(rowCount).cells(12).children[0].value) < parseInt(GridView.rows(rowCount).cells(10).children[0].value)) {
                        window.alert("You Can't give Quantity more than Available Quantity");
                        GridView.rows(rowCount).cells(10).children(0).value = "";
                        calpurchase();
                        GridView.rows(rowCount).cells(10).children(0).focus();
                        return false;
                    }

                }
            }
            if (GridView3 != null) {
                var date = document.getElementById("<%=txtdate.ClientID %>").value;
                var datectrl = document.getElementById("<%=txtdate.ClientID %>");
                var remarks = document.getElementById("<%=txtremarks.ClientID %>").value;
                var remarksctrl = document.getElementById("<%=txtremarks.ClientID %>");
                for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                    if (GridView3.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }


                    else if (GridView3.rows(rowCount).cells(11).children(0).selectedIndex == 0 && GridView3.rows(rowCount).cells(4).innerText != "DCA-27") {
                        window.alert("Please Calculate Depreciation");
                        GridView3.rows(rowCount).cells(10).children(0).focus();
                        calculatecmc();
                        return false;
                    }


                }
                if (date == "") {
                    window.alert("Please Enter Date");
                    datectrl.focus();
                    return false;
                }

                var tdate = document.getElementById("<%=ddlDays.ClientID %>").value;
                var tdatectrl = document.getElementById("<%=ddlDays.ClientID %>");
                if (tdate == "Select No of Days") {
                    window.alert("Please Select Transit Days");
                    tdatectrl.focus();
                    return false;
                }

                if (remarks == "") {
                    window.alert("Please Enter Remarks");
                    remarksctrl.focus();
                    return false;
                }
            }
            else if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }


                }
            }

            document.getElementById("<%=btnmdlupd.ClientID %>").style.display = 'none';
            return true;
        }

        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {

                //      var rqty=GridView.rows(rowCount).cells(10).children[0].value;
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                    //    theEvent.preventDefault();
                }
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
    <script language="javascript">
        function calculatecmc() {
            grd = document.getElementById("<%=grdpopcmc.ClientID %>");
            var amt = 0;
            var amt1 = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                if (grd.rows(rowCount).cells(11).children(0).selectedIndex != 0) {
                    var dep = grd.rows(rowCount).cells(11).children(0).value;
                    var Amount1 = parseFloat(grd.rows(rowCount).cells(6).innerText.replace(/,/g, "")) * parseFloat(grd.rows(rowCount).cells(10).children(0).value.replace(/,/g, ""));

                    if (dep != "Full Value" && grd.rows(rowCount).cells(4).innerText != "DCA-27") {
                        var Amount = parseFloat(Amount1) * parseFloat(dep) / 100;
                        grd.rows(rowCount).cells(13).innerText = parseFloat(Amount1) - parseFloat(Amount);
                        grd.rows(rowCount).cells(12).innerText = Amount1;
                    }
                    else if (dep != "--Select--" && grd.rows(rowCount).cells(4).innerText == "DCA-27") {
                        window.alert("Depreciation Value is not applicable for Asset Items");
                        grd.rows(rowCount).cells(11).children(0).value = "--Select--";
                        return false;
                    }

                    else if (dep == "Full Value" && grd.rows(rowCount).cells(4).innerText != "DCA-27") {
                        grd.rows(rowCount).cells[13].innerText = Amount1;
                    }


                    amt += Number(grd.rows(rowCount).cells[13].innerText);
                    amt1 += Number(grd.rows(rowCount).cells[12].innerText);
                    grd.rows[grd.rows.length - 1].cells[12].innerHTML = amt1;

                    grd.rows[grd.rows.length - 1].cells[13].innerHTML = amt;


                }
            }
        }
        

    </script>
    <script>
        function percentcheck() {

            var GridView1 = document.getElementById("<%=grdpopcmc.ClientID %>");
            var check = document.getElementById("<%=hfcheck.ClientID %>").value;
            var role = document.getElementById("<%=hfrole.ClientID %>").value;

            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    var itemtype = GridView1.rows(rowCount).cells(1).innerHTML.substring(0, 1);
                    var percent = GridView1.rows(rowCount).cells(11).children(0).value;
                    if (itemtype == 2 && percent > check && role == "Chief Material Controller" && percent != "--Select--" && percent != "Full Value") {
                        window.alert("You can not increase depriciation value above  " + check + "%");
                        GridView1.rows(rowCount).cells(11).children(0).value = "--Select--";
                        GridView1.rows(rowCount).cells(13).children(0).innerHTML = 0;
                        return false;
                        calculatecmc();
                    }
                }
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
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Search: Issued Items <a class="help"
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
                                                                                            <%--  <br />
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
                                                                                                                    Issues</h2>
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
                                                                                                <asp:HiddenField ID="hfrole" runat="server" />
                                                                                                <asp:HiddenField ID="hfcheck" runat="server" />
                                                                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                                                                                    <asp:GridView ID="GridView1" Width="100%" GridLines="None" runat="server" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                                                        OnDataBound="GridView1_DataBound" DataKeyNames="Ref No" OnRowDataBound="GridView1_RowDataBound"
                                                                                                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged1" OnRowEditing="GridView1_RowEditing2">
                                                                                                        <Columns>
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                                                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                                                                                EditImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                                                            <asp:TemplateField HeaderText="Refrence No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                                                                                ItemStyle-VerticalAlign="Bottom">
                                                                                                                <ItemTemplate>
                                                                                                                    <%#Eval("Ref No") %>
                                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="transfer_date" HeaderText="Issue Date" />
                                                                                                            <asp:BoundField DataField="cc_code" HeaderText="Issued From" />
                                                                                                            <asp:BoundField DataField="recieved_cc" HeaderText="Recieved CC" />
                                                                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
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
                                                                                                    <table width="800px" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td width="13" valign="bottom">
                                                                                                                <img src="images/leftc.jpg">
                                                                                                            </td>
                                                                                                            <td class="pop_head" align="left" id="approveind" runat="server">
                                                                                                                <div class="popclose">
                                                                                                                    <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                                                                </div>
                                                                                                                Verify Issue Slip
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
                                                                                                                                    <div>
                                                                                                                                        <img src="images/load.gif" /></div>
                                                                                                                                </ProgressTemplate>
                                                                                                                            </asp:UpdateProgress>
                                                                                                                            <table style="vertical-align: middle;" align="center">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table border="0" class="fields" width="100%" id="tblsearch" runat="server">
                                                                                                                                            <tr>
                                                                                                                                                <td class="label search_filters search_fields" align="center" colspan="7" width="80%">
                                                                                                                                                    <table class="search_table">
                                                                                                                                                        <tr style="height: 18px">
                                                                                                                                                            <td colspan="2" align="center">
                                                                                                                                                                <div id="bubble_tooltip">
                                                                                                                                                                    <div class="bubble_top">
                                                                                                                                                                    </div>
                                                                                                                                                                    <div class="bubble_middle">
                                                                                                                                                                        <span id="bubble_tooltip_content"></span>
                                                                                                                                                                    </div>
                                                                                                                                                                    <div class="bubble_bottom">
                                                                                                                                                                    </div>
                                                                                                                                                                </div>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td class="item m2o_search" valign="middle">
                                                                                                                                                                <asp:TextBox ID="txtSearch" CssClass="m2o_search" runat="server" Style="background-image: url(images/search_grey.png);
                                                                                                                                                                    background-position: right; background-repeat: no-repeat; border-color: #CBCCCC;
                                                                                                                                                                    font-size: smaller;"></asp:TextBox>
                                                                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList"
                                                                                                                                                                    ServicePath="cascadingDCA.asmx" TargetControlID="txtSearch" UseContextKey="True"
                                                                                                                                                                    CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                                                                                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                                                                                                    MinimumPrefixLength="1" CompletionListElementID="listPlacement" OnClientItemSelected="extender">
                                                                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSearch"
                                                                                                                                                                    WatermarkText="Please search here..">
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
                                                                                                                                        <table id="indnodate" runat="server">
                                                                                                                                            <tr>
                                                                                                                                                <td id="td1" runat="server" style="width: 100%" align="center">
                                                                                                                                                    <asp:Label ID="Label1" runat="server" Text="Reciept Date:- " CssClass="char"></asp:Label>
                                                                                                                                                    <asp:TextBox ID="txtrecieptdate" runat="server" Width="100px" Enabled="false" CssClass="char"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                        <table>
                                                                                                                                            <table>
                                                                                                                                                <tr>
                                                                                                                                                    <td colspan="4">
                                                                                                                                                        <asp:GridView ID="Grdeditpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                            PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                            DataKeyNames="id" ShowFooter="true" OnRowDataBound="Grdeditpopup_RowDataBound1">
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
                                                                                                                                                                <asp:BoundField DataField="basic_price" HeaderText="Basic Price" ItemStyle-Width="25px" />
                                                                                                                                                                <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                                <asp:BoundField DataField="requiredqty" HeaderText="Requested Qty" ItemStyle-Width="25px" />
                                                                                                                                                                <asp:BoundField DataField="quantity" HeaderText="Issued Qty" ItemStyle-Width="25px" />
                                                                                                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:TextBox ID="txtqty" runat="server" onkeypress='IsNumeric1(event)' onkeyup="calpurchase();"
                                                                                                                                                                            Width="50px"></asp:TextBox>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:Label ID="lblamount" runat="server"></asp:Label>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                    <FooterTemplate>
                                                                                                                                                                        <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
                                                                                                                                                                    </FooterTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                <asp:TemplateField>
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("Available qty")%>' />
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                            </Columns>
                                                                                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                            <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                            <HeaderStyle CssClass="grid-header" />
                                                                                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                                        </asp:GridView>
                                                                                                                                                        <asp:GridView ID="grdpopcmc" Width="780px" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                            PagerStyle-CssClass="grid pagerbar" DataKeyNames="id" ShowFooter="true" BorderColor="White"
                                                                                                                                                            RowStyle-CssClass=" grid-row char grid-row-odd" OnRowDataBound="grdpopcmc_RowDataBound">
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
                                                                                                                                                                <asp:BoundField DataField="basic_price" HeaderText="Basic Price" ItemStyle-Width="25px" />
                                                                                                                                                                <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                                <asp:BoundField DataField="Available qty" HeaderText="Avaliable Qty" />
                                                                                                                                                                <asp:BoundField DataField="Issued Qty" HeaderText="Requested Qty" />
                                                                                                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:TextBox ID="txtqty" runat="server" Width="50px" Enabled="false" Text='<%# Eval("Issued Qty")  %>'></asp:TextBox>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                <asp:TemplateField HeaderText="Depreciation">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:DropDownList ID="ddldep" CssClass="char" runat="server" onchange="calculatecmc(this.value);">
                                                                                                                                                                            <%-- <asp:ListItem>--Select--</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>Full Value</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>10</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>20</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>30</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>40</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>50</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>60</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>70</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>80</asp:ListItem>
                                                                                                                                                                            <asp:ListItem>90</asp:ListItem>--%>
                                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                        <cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" TargetControlID="ddldep"
                                                                                                                                                                            ServicePath="cascadingDCA.asmx" Category="ddd" LoadingText="Please Wait" ServiceMethod="Depissuevalues"
                                                                                                                                                                            PromptText="--Select--">
                                                                                                                                                                        </cc1:CascadingDropDown>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                <asp:TemplateField HeaderText="Before Depreciation">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:Label ID="lbldamount" runat="server" Text='<%# Eval("qty")  %>'></asp:Label>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                    <FooterTemplate>
                                                                                                                                                                        <asp:Label ID="Label2" runat="server" Text="0.00"></asp:Label>
                                                                                                                                                                    </FooterTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                <asp:TemplateField HeaderText="After Depreciation">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:Label ID="lblamount" runat="server" Text='<%# Eval("csk_dep")  %>'></asp:Label>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                    <FooterTemplate>
                                                                                                                                                                        <asp:Label ID="Label3" runat="server" Text="0.00"></asp:Label>
                                                                                                                                                                    </FooterTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                <asp:TemplateField>
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <asp:HiddenField ID="hfchk" runat="server" Value='<%#Eval("csk_percent")%>' />
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                            </Columns>
                                                                                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                            <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                            <HeaderStyle CssClass="grid-header" />
                                                                                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                                        </asp:GridView>
                                                                                                                                                        <asp:GridView ID="gridviewpopup" Width="800px" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                            PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                            ShowFooter="true" OnRowDataBound="gridviewpopup_RowDataBound">
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
                                                                                                                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                                                                                                                                <asp:BoundField DataField="item_status" HeaderText="Item Type" />
                                                                                                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                                                                                                            </Columns>
                                                                                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                            <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                            <HeaderStyle CssClass="grid-header" />
                                                                                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                                        </asp:GridView>
                                                                                                                                                        <tr id="cmcdetails" runat="server">
                                                                                                                                                            <td style="width: 100%">
                                                                                                                                                                <table class="search_table" align="center">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td class="item item-selection" align="left" width="">
                                                                                                                                                                            <asp:Label ID="Label4" runat="server" Text="Date"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="item item-selection" id="tddayslabel" runat="server" align="left" width="">
                                                                                                                                                                            <asp:Label ID="lblremark" runat="server" Text="Transit Days"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="item item-selection" align="left" width="">
                                                                                                                                                                            <asp:Label ID="Label13" runat="server" Text="Remarks"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td class="item item-selection" valign="top" align="left">
                                                                                                                                                                            <asp:TextBox ID="txtdate" runat="server" CssClass="char" Width="100px"></asp:TextBox>
                                                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                                                                                                PopupButtonID="TextBox1">
                                                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="item item-selection" align="left" width="" runat="server" id="tddays"
                                                                                                                                                                            valign="top">
                                                                                                                                                                            <asp:DropDownList ID="ddlDays" CssClass="char" ToolTip="Select No of Days" Width="150px"
                                                                                                                                                                                runat="server">
                                                                                                                                                                                <asp:ListItem Value="Select No of Days">Select No of Days</asp:ListItem>
                                                                                                                                                                                <asp:ListItem>1</asp:ListItem>
                                                                                                                                                                                <asp:ListItem>2</asp:ListItem>
                                                                                                                                                                                <asp:ListItem>3</asp:ListItem>
                                                                                                                                                                                <asp:ListItem>4</asp:ListItem>
                                                                                                                                                                                <asp:ListItem>5</asp:ListItem>
                                                                                                                                                                                <asp:ListItem>6</asp:ListItem>
                                                                                                                                                                                <asp:ListItem>7</asp:ListItem>
                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="item item-selection" valign="top" align="left">
                                                                                                                                                                            <asp:TextBox ID="txtremarks" TextMode="MultiLine" Font-Size="Small" runat="server"
                                                                                                                                                                                MaxLength="200" Width="450px"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </table>
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
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td>
                                                                                                                                                                            <table id="Table2" width="100%" class="pestbl" style="border: 1px solid #000;" runat="server">
                                                                                                                                                                                <tr style="border: 1px solid  #000;">
                                                                                                                                                                                    <td align="center" colspan="2">
                                                                                                                                                                                        <asp:Label ID="Label11" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="Essel Projects Pvt Ltd."></asp:Label>
                                                                                                                                                                                        <br />
                                                                                                                                                                                        <%-- <asp:Label ID="Label33" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                                                                                        Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,"></asp:Label>
                                                                                                                                                                                    <br />
                                                                                                                                                                                    <asp:Label ID="lblpo" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                                                                                                                                        Text="Durg- 490026 (Chhattisgarh)"></asp:Label>--%>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid  #000;">
                                                                                                                                                                                    <td align="center" colspan="2">
                                                                                                                                                                                        <asp:Label ID="Label122" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                                                                                                                                            Text="Issue Slip"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid  #000;">
                                                                                                                                                                                    <td align="left" colspan="1" style="border: 1px solid #000; width: 50%">
                                                                                                                                                                                        <asp:Label ID="Label134" runat="server" CssClass="peslbl" Font-Bold="false" Text="Issue Slip No:"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lblchallanno" runat="server" CssClass="peslbl" Font-Bold="false" Text=""></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td align="left" colspan="1" style="border: 1px solid #000; width: 50%">
                                                                                                                                                                                        <asp:Label ID="Label14" runat="server" CssClass="peslbl" Font-Bold="false" Text="Date:"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lbldate" runat="server" CssClass="peslbl" Font-Bold="false" Text=""></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid #000; height: 100px">
                                                                                                                                                                                    <td width="50%" align="left" style="border: 1px solid #000" valign="top">
                                                                                                                                                                                        <asp:Label ID="lblname" runat="server" Text="Despatched From:" Width="100%" CssClass="pestbox"
                                                                                                                                                                                            Font-Underline="true"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lbldespfrom" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lbldespfromadd" runat="server" Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,Durg- 490026 (Chhattisgarh)."
                                                                                                                                                                                            Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td align="left" width="100%" style="border: 1px solid #000" valign="top">
                                                                                                                                                                                        <asp:Label ID="Label5" runat="server" Text="Despatched To:" Width="100%" CssClass="pestbox"
                                                                                                                                                                                            Font-Underline="true"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lbldespto" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lbldesptoadd" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr>
                                                                                                                                                                                    <td colspan="2" align="left" style="border: 1px solid #000">
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label88" CssClass="peslbl1" runat="server" Text="">The under mentioned old and used erection items / tools / equipments are being dispatched through </asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label6" CssClass="peslbl1" runat="server" Text="">Mr.</asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txtname" Width="39%" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                                                                                            runat="server"></asp:TextBox>
                                                                                                                                                                                        <asp:Label ID="Label10" CssClass="peslbl1" runat="server" Text="">by vehicle/ truck No. </asp:Label>
                                                                                                                                                                                        <asp:TextBox ID="txttruckno" Width="39%" CssClass="pestbox" Style="border: None;
                                                                                                                                                                                            border-bottom: 1px solid #000" runat="server"></asp:TextBox>
                                                                                                                                                                                        <asp:Label ID="Label12" CssClass="peslbl1" runat="server" Text="">to our project site on stock transfer basis.  These items / tools / equipments are for </asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label7" CssClass="peslbl1" runat="server" Font-Underline="true" Text="">OUR OWN USE ON RETURNABLE BASIS AND NOT FOR SALE.</asp:Label>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid #000">
                                                                                                                                                                                    <td colspan="2" style="border: 1px solid #000">
                                                                                                                                                                                        <asp:GridView CssClass="" ID="grdbill" Width="100%" runat="server" AutoGenerateColumns="false">
                                                                                                                                                                                            <Columns>
                                                                                                                                                                                                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" HeaderText="Item Name" />
                                                                                                                                                                                                <asp:BoundField DataField="specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="200px" HeaderText="Specification" />
                                                                                                                                                                                                <asp:BoundField DataField="units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px" HeaderText="Units" />
                                                                                                                                                                                                <asp:BoundField DataField="quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                                                                                                                                    ItemStyle-Width="50px" HeaderText="Quantity" />
                                                                                                                                                                                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-BackColor="White" ItemStyle-HorizontalAlign="Left">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:TextBox ID="txtremarks" TextMode="MultiLine" CssClass="peslbl1" runat="server"
                                                                                                                                                                                                            Width="150px" Text="" Style="border: None" MaxLength="25"></asp:TextBox>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                            </Columns>
                                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                                    </td>
                                                                                                                                                                                </tr>
                                                                                                                                                                                <tr style="border: 1px solid #000; height: 100px">
                                                                                                                                                                                    <td width="50%" align="left" style="border: 1px solid #000" valign="top">
                                                                                                                                                                                        <asp:Label ID="Label8" Font-Bold="true" runat="server" Text="Despatched by:" Width="100%"
                                                                                                                                                                                            CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label9" runat="server" Text="" Width="100%" Height="90px" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label16" runat="server" Text="" Width="100%" CssClass="pestbox">(Name & Signature)</asp:Label>
                                                                                                                                                                                        &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblpurchasemanagername" runat="server" Style="vertical-align: middle"
                                                                                                                                                                                            Text=""></asp:Label>)
                                                                                                                                                                                    </td>
                                                                                                                                                                                    <td align="left" width="100%" style="border: 1px solid #000" valign="top">
                                                                                                                                                                                        <asp:Label ID="Label111" Font-Bold="true" runat="server" Text="Recieved By:" Width="100%"
                                                                                                                                                                                            CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label15" runat="server" Text="" Width="100%" Height="90px" CssClass="pestbox"></asp:Label>
                                                                                                                                                                                        <asp:Label ID="Label17" runat="server" Text="" Width="100%" CssClass="pestbox">(Name & Signature)</asp:Label>
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
                                                                                                                                                <tr>
                                                                                                                                                    <td height="20px">
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr id="btnu" runat="server">
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                                                                                                                        <asp:Button ID="btnmdlupd" runat="server" Text="Verified" CssClass="button" OnClientClick="javascript:return validate()"
                                                                                                                                            OnClick="btnmdlupd_Click" />
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
