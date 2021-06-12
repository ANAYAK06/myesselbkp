<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="TransferOut.aspx.cs" Inherits="TransferOut" Title="Transfer Out - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

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

        function Deletevalidate() {
            if (confirm("Are you sure,you want to delete selected records ?") == true)
                return true;
            else
                return false;
        }
        function searchvalidate() {
            var Search = document.getElementById("<%=txtSearch.ClientID %>").value;
            var GridView = document.getElementById("<%=grdtransferout.ClientID %>");
            var filter = document.getElementById("<%=ddlindenttype.ClientID %>").value;

            var Searchctrl = document.getElementById("<%=txtSearch.ClientID %>");
            if (Search == "Please search here..") {
                window.alert("Please Select Item");
                Searchctrl.focus();
                return false;
            }
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(1).innerHTML == Search) {
                        window.alert("Selected Item is already added");
                        Search = "";
                        Searchctrl.focus();
                        return false;
                    }
                }
            }
            GridView = document.getElementById("<%=grdtransferout.ClientID %>");
            var itemcode = document.getElementById("<%=txtSearch.ClientID %>").value.substring(0, 1);
            if (GridView != null) {
                var itemtype = GridView.rows(1).cells(1).innerHTML.substring(0, 1);
                if (itemtype == 1) {
                    if (itemcode != 1) {
                        alert("You are not able to add multiple DCA items");
                        document.getElementById("<%=txtSearch.ClientID %>").value = "";
                        return false;
                    }
                }
                if (itemtype != 1) {
                    if (itemcode == 1) {
                        alert("You are not able to add multiple DCA items");
                        document.getElementById("<%=txtSearch.ClientID %>").value = "";
                        return false;
                    }
                }
            }

        }
        function validate() {
            var date = document.getElementById("<%=txtdate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdate.ClientID %>");
            var remarks = document.getElementById("<%=txtremarks.ClientID %>").value;
            var remarksctrl = document.getElementById("<%=txtremarks.ClientID %>");
            var GridView = document.getElementById("<%=grdtransferout.ClientID %>");
            var GridView1 = document.getElementById("<%=grdpopcentral.ClientID %>");

            var role = document.getElementById("<%=hfrole.ClientID %>").value;


            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(7).children(0).selectedIndex == 0) {
                        window.alert("Please Select Item Status");
                        GridView.rows(rowCount).cells(7).children(0).focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(9).children[0].value == "") {
                        window.alert("Please Enter Quantity");
                        GridView.rows(rowCount).cells(9).children[0].focus();
                        return false;
                    }

                    else if (parseFloat(GridView.rows(rowCount).cells(8).innerHTML) < parseFloat(GridView.rows(rowCount).cells(9).children[0].value)) {
                        window.alert("You are not able to transfer more than the available quantity");
                        GridView.rows(rowCount).cells(9).children[0].focus();
                        GridView.rows(rowCount).cells(9).children[0].value = "";
                        return false;
                    }

                    else if (parseFloat(GridView.rows(rowCount).cells(9).children[0].value) == 0) {
                        window.alert("You are not able to transfer more than the available quantity");
                        GridView.rows(rowCount).cells(9).children[0].focus();
                        GridView.rows(rowCount).cells(9).children[0].value = "";
                        return false;
                    }


                }
            }
            if (GridView1 != null) {
                var hftype = document.getElementById("<%=hftype.ClientID %>").value;
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (hftype == 2) {
                        var dep = GridView1.rows(rowCount).cells(11).children(0).selectedIndex;
                    }
                    else {
                        var dep = GridView1.rows(rowCount).cells(11).children(1).selectedIndex;
                    }
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(10).children[0].value == "") {
                        window.alert("Please Enter Quantity");
                        GridView1.rows(rowCount).cells(10).children[0].focus();
                        return false;
                    }
                    else if (parseFloat(GridView1.rows(rowCount).cells(8).innerHTML) < parseFloat(GridView1.rows(rowCount).cells(10).children[0].value)) {
                        window.alert("You are not able to transfer more than the available quantity");
                        GridView1.rows(rowCount).cells(10).children[0].focus();
                        GridView1.rows(rowCount).cells(10).children[0].value = "";
                        return false;
                    }

                    else if (parseFloat(GridView1.rows(rowCount).cells(9).innerHTML) < parseFloat(GridView1.rows(rowCount).cells(10).children[0].value)) {
                        window.alert("You are not able to increase more than the Requested quantity");
                        GridView1.rows(rowCount).cells(10).children[0].value = "";
                        return false;
                    }

                    else if (dep == 0 && GridView1.rows(rowCount).cells(4).innerText != "DCA-27") {
                        window.alert("Please Calculate Depreciation");
                        if (hftype == 2) {
                            GridView1.rows(rowCount).cells(11).children(0).focus();
                        }
                        else {
                            GridView1.rows(rowCount).cells(11).children(1).focus();
                        }
                        return false;
                    }


                }
            }

            if (date == "") {
                window.alert("Please Enter Date");
                datectrl.focus();
                return false;
            }
            if (role == "Chief Material Controller") {
                var tdate = document.getElementById("<%=ddlDays.ClientID %>").value;
                var tdatectrl = document.getElementById("<%=ddlDays.ClientID %>");
                if (tdate == "Select No of Days") {
                    window.alert("Please Select Transit Days");
                    tdatectrl.focus();
                    return false;
                }
            }
            if (remarks == "") {
                window.alert("Please Enter Remarks");
                remarksctrl.focus();
                return false;
            }

            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;

        }

        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=grdtransferout.ClientID %>");
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
        //        function Checkqty() {
        //            GridView = document.getElementById("<%=grdtransferout.ClientID %>");
        //            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
        //                var qty = GridView.rows(rowCount).cells(7).innerHTML;
        //                var rqty = GridView.rows(rowCount).cells(9).children[0].value;

        //                if (parseInt(qty) > parseInt(rqty)) {
        //                    window.alert("Quantity Limit Exceed");
        //                    GridView.rows(rowCount).cells(9).children[0].focus();
        //                    GridView.rows(rowCount).cells(9).children[0].value == "";
        //                    return false;
        //                }
        //            }
        //        }
        function isrepeted() {
            gridview = document.getElementById("<%=grdtransferout.ClientID %>");
            var sel = "|";
            var cel = "|";
            for (var i = 1; i < gridview.rows.length - 1; i++) {
                var idx = gridview.rows[i].cells[1].innerHTML;
                var idy = gridview.rows[i].cells[7].children[0].selectedIndex;
                if (idy != 0) {
                    if (sel.indexOf("|" + idx + "|" && "|" + idy + "|") == -1) {
                        sel = sel + idx + "|";
                        cel = cel + idy + "|";
                    }
                    else {
                        alert(gridview.rows[i].cells[1].innerHTML + " Already Selected")
                        gridview.rows[i].cells[7].children[0].focus();
                        return false;
                    }
                }
            }
            return true;
        }

    </script>
    <script language="javascript">
        function calculate() {
            grd = document.getElementById("<%=grdpopcentral.ClientID %>");
            var hftype = document.getElementById("<%=hftype.ClientID %>").value;
            var amt = 0;
            var amt1 = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                if (grd.rows(rowCount).cells(11).children(1).selectedIndex != 0) {
                    if (hftype == 2) {
                        var dep = grd.rows(rowCount).cells(11).children(0).value;
                    }
                    else {
                        var dep = grd.rows(rowCount).cells(11).children(1).value;
                    }
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
                        if (hftype == 2) {
                            grd.rows(rowCount).cells[13].innerText = 0;
                        }
                        else {
                            grd.rows(rowCount).cells[13].innerText = Amount1;
                        }
                        grd.rows(rowCount).cells[12].innerText = Amount1;
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
            var GridView = document.getElementById("<%=grdpopcentral.ClientID %>");
            var check = document.getElementById("<%=hfcheck.ClientID %>").value;
            var role = document.getElementById("<%=hfrole.ClientID %>").value;
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    var itemtype = GridView.rows(rowCount).cells(1).innerHTML.substring(0, 1);
                    var percent = GridView.rows(rowCount).cells(11).children(0).value;
                    if (itemtype == 2 && parseInt(percent) > parseInt(check) && role == "Central Store Keeper" && percent != "--Select--") {
                        window.alert("You can not increase depriciation value above  " + check + "%");
                        GridView.rows(rowCount).cells(11).children(0).value = "--Select--";
                        GridView.rows(rowCount).cells(13).innerHTML = 0;
                        calculate();
                        return false;
                    }
                    else {
                        calculate();
                    }
                }
            }
            ////            if (GridView1 != null) {
            ////                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
            ////                    var itemtype = GridView1.rows(rowCount).cells(1).innerHTML.substring(0, 1);
            ////                    var percent = GridView1.rows(rowCount).cells(11).children(0).value;
            ////                    if (itemtype == 2 && percent > check && role == "Central Store Keeper" && percent != "--Select--" && percent != "Full Value") {
            ////                        window.alert("You can not increase depriciation value above  " + check + "%");
            ////                        GridView1.rows(rowCount).cells(11).children(0).value = "--Select--";
            ////                        return false;
            ////                    }
            ////                }
            ////            }
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
    <script language="javascript">
        function checkitemtype() {
            GridView = document.getElementById("<%=grdtransferout.ClientID %>");
            var itemtypes = document.getElementById("<%=ddlindenttype.ClientID %>").value;
            var itemtypectrl = document.getElementById("<%=ddlindenttype.ClientID %>");
            if (GridView != null) {
                var itemtype = GridView.rows(1).cells(1).innerHTML.substring(0, 1);
                if (itemtypes != itemtype) {
                    alert("You are not able to add multiple DCA items");

                    itemtypectrl.focus();
                    document.getElementById("<%=ddlindenttype.ClientID %>").value = "Select Type";
                    return false;
                }

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="750px">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Transfer Out <a class="help"
                                    href="" title=""><small>Help</small> </a>
                            </h1>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                        <ProgressTemplate>
                                            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                    left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                    <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                </div>
                                            </asp:Panel>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
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
                                                                        <div id="search_filter_data">
                                                                            <table class="search_table" id="tblSearch" runat="server">
                                                                                <tbody>
                                                                                    <tr height="73px">
                                                                                        <td colspan="1">
                                                                                        </td>
                                                                                        <td colspan="1" style="width: 100%" align="center">
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
                                                                                    <tr style="width: 100%">
                                                                                        <td style="width: 100px">
                                                                                            <asp:DropDownList ID="ddlindenttype" CssClass="char" runat="server" OnSelectedIndexChanged="ddlindenttype_SelectedIndexChanged"
                                                                                                onchange="checkitemtype();" AutoPostBack="true">
                                                                                                <asp:ListItem Value="Select Type">Select Type</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Assets</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Semi Assets/Consumables</asp:ListItem>
                                                                                                <asp:ListItem Value="3">Consumables</asp:ListItem>
                                                                                                <asp:ListItem Value="4">Bought Out Items</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="item item-char" valign="middle" style="width: 600px">
                                                                                            <asp:TextBox ID="txtSearch" CssClass="m2o_search" runat="server" Style="background-image: url(images/search_grey.png);
                                                                                                background-position: right; background-repeat: no-repeat; border-color: #CBCCCC;
                                                                                                font-size: smaller;" onkeydown="restrictComma();return isNumberKey(event,this);"></asp:TextBox>
                                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="TransferOutSearch"
                                                                                                ServicePath="cascadingDCA.asmx" TargetControlID="txtSearch" UseContextKey="True"
                                                                                                CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                                MinimumPrefixLength="1" CompletionListElementID="listPlacement" OnClientItemSelected="extender">
                                                                                            </cc1:AutoCompleteExtender>
                                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="Please search here.."
                                                                                                WatermarkCssClass="watermarked" TargetControlID="txtSearch" runat="server">
                                                                                            </cc1:TextBoxWatermarkExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
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
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
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
                                                                                                                    Transfer Out</h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                                <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button">
                                                                                                                <asp:Button ID="btnadd" runat="server" Text="Add" Height="18px" CssClass="button"
                                                                                                                    OnClick="btnadd_Click" OnClientClick="javascript:return searchvalidate()" />
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button" style="">
                                                                                                                <asp:Button ID="btnDelete" Height="18px" runat="server" Text="Delete" CssClass="button"
                                                                                                                    OnClick="btnDelete_Click" OnClientClick="return Deletevalidate();" />
                                                                                                            </td>
                                                                                                            <td class="pager-cell" style="width: 80%; padding-left: 5px;" valign="middle" align="left">
                                                                                                                <asp:Button ID="Button1" runat="server" CssClass="button" Height="18px" Width="120px"
                                                                                                                    OnClientClick="javascript:return ClearTextboxes();" Text="Clear SearchBox" />
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
                                                                                                    <asp:GridView ID="grdtransferout" BorderColor="White" Width="100%" runat="server"
                                                                                                        AutoGenerateColumns="False" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                        PagerStyle-CssClass="grid pagerbar" DataKeyNames="id" ShowFooter="true" OnRowDataBound="grdtransferout_RowDataBound">
                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                            <asp:TemplateField HeaderText="Item Status">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:DropDownList ID="ddlStatus" CssClass="char" runat="server" onblur="isrepeted();">
                                                                                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                                                                                        <asp:ListItem>Stock</asp:ListItem>
                                                                                                                        <asp:ListItem>New Stock</asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="" HeaderText="Avaliable Qty" />
                                                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtqty" runat="server" onkeypress='IsNumeric1(event)' Width="50px"
                                                                                                                        Text='<%# Eval("Issued Qty")  %>'></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("Available qty")%>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                    <asp:GridView ID="grdpopcentral" Width="780px" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                        PagerStyle-CssClass="grid pagerbar" DataKeyNames="id" ShowFooter="true" BorderColor="White"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" OnRowDataBound="grdpopcentral_RowDataBound">
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
                                                                                                                    <asp:TextBox ID="txtqty" runat="server" Width="50px" Text='<%# Eval("Issued Qty")  %>'></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Depreciation">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:DropDownList ID="ddldep" CssClass="char" runat="server" onchange="calculate(this.value);">
                                                                                                                    </asp:DropDownList>
                                                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddldep"
                                                                                                                        ServicePath="cascadingDCA.asmx" Category="ddd" LoadingText="Please Wait" ServiceMethod="Depvalues"
                                                                                                                        PromptText="--Select--">
                                                                                                                    </cc1:CascadingDropDown>
                                                                                                                    <asp:DropDownList ID="ddlissuedep" CssClass="char" runat="server" onchange="calculate(this.value);">
                                                                                                                    </asp:DropDownList>
                                                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" TargetControlID="ddlissuedep"
                                                                                                                        ServicePath="cascadingDCA.asmx" Category="ddd" LoadingText="Please Wait" ServiceMethod="Depissuevalues"
                                                                                                                        PromptText="--Select--">
                                                                                                                    </cc1:CascadingDropDown>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Before Depreciation">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lbldepamount" runat="server" Text=""></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <FooterTemplate>
                                                                                                                    <asp:Label ID="Label2" runat="server" Text="0.00"></asp:Label>
                                                                                                                </FooterTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="After Depreciation">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblamount" runat="server" Text=""></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <FooterTemplate>
                                                                                                                    <asp:Label ID="Label3" runat="server" Text="0.00"></asp:Label>
                                                                                                                </FooterTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                    </asp:GridView>
                                                                                                    <asp:HiddenField ID="hftype" runat="server" />
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <asp:HiddenField ID="hfcheck" runat="server" />
                                                                                        <asp:HiddenField ID="hfrole" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" class="fields" width="100%" id="tbldesc" runat="server">
                                                                            <tr>
                                                                                <td style="width: 100%">
                                                                                    <table class="search_table" align="center">
                                                                                        <tr>
                                                                                            <td class="item item-selection" align="left" width="">
                                                                                                <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                                                                            </td>
                                                                                            <td class="item item-selection" id="tddayslabel" runat="server" align="left" width="">
                                                                                                <asp:Label ID="lblremark" runat="server" Text="Transit Days"></asp:Label>
                                                                                            </td>
                                                                                            <td class="item item-selection" align="left" width="">
                                                                                                <asp:Label ID="Label4" runat="server" Text="Remarks"></asp:Label>
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
                                                                                <td class="label search_filters search_fields">
                                                                                    <table class="search_table" width="100%">
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
                                                                    </td>
                                                                </tr>
                                                                <tr class="pagerbar">
                                                                    <td class="pager-cell-button" align="center">
                                                                        <asp:Button ID="btnsubmit" Width="80px" Height="20px" runat="server" Text="Submit"
                                                                            CssClass="button" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
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
