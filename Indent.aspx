<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Indent.aspx.cs"
    Inherits="Indent" Title="Indent - Essel Projects Pvt.Ltd." EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
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
        function searchvalidate() {
            var Search = document.getElementById("<%=txtSearch.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=txtSearch.ClientID %>");
            if (Search == "Please search here.." || Search == "") {
                window.alert("Please Select an item");
                Searchctrl.focus();
                return false;
            }
            GridView = document.getElementById("<%=grideditpopup2.ClientID %>");
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
            document.getElementById("<%=btnAdd.ClientID %>").style.display = 'none';
            return true;
        }
        function validate() {
            var GridView = document.getElementById("<%=grideditpopup2.ClientID %>");
            var GridView1 = document.getElementById("<%=gridcentral.ClientID %>");
            var GridView2 = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var GridView3 = document.getElementById("<%=grdcmc.ClientID %>");


            if (GridView != null) {
                var pmremarks = document.getElementById("<%=txtpmremark.ClientID %>").value;
                if (pmremarks == "") {
                    window.alert("Please Enter Remarks");
                    document.getElementById("<%=txtpmremark.ClientID %>").focus();
                    return false;
                }
                else {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (GridView.rows(rowCount).cells(11).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView.rows(rowCount).cells(8).children[0].value == "" || GridView.rows(rowCount).cells(8).children[0].value == 0) {
                            window.alert("Please Enter Quantity");
                            return false;
                        }
                        else if (GridView.rows(rowCount).cells(9).children[0].value == "" || GridView.rows(rowCount).cells(9).children[0].value == 0) {
                            window.alert("Please Enter Amount");
                            GridView.rows(rowCount).cells(9).children[0].focus();
                            return false;
                        }
                        else if (GridView.rows[GridView.rows.length - 1].cells[10].innerHTML == "0.00") {
                            var response = confirm("Do you want to Close the Indent");
                            if (response) {
                                return true;

                            }
                            else {
                                return false;
                            }
                        }
                    }
                }
            }

            else if (GridView1 != null) {
                var objs = new Array("<%=ddlindenttype.ClientID %>");

                var IndentType = document.getElementById("<%=ddlindenttype.ClientID %>").value;
                var indentctrl = document.getElementById("<%=ddlindenttype.ClientID %>");
                var cskremarks = document.getElementById("<%=txtcskremark.ClientID %>").value;
                if (!CheckInputs(objs)) {
                    return false;
                }

                if (cskremarks == "") {
                    window.alert("Please Enter Remarks");
                    document.getElementById("<%=txtcskremark.ClientID %>").focus();
                    return false;
                }
                else {
                    for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                        if (GridView1.rows(rowCount).cells(13).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (IndentType == "SemiAssets/Consumable Issue") {

                            if (GridView1.rows(rowCount).cells(9).children[0].value == "") {
                                window.alert("Please Enter Quantity");
                                return false;
                            }
                            Checkqty();
                        }
                    }
                    if (IndentType == "SemiAssets/Consumable Transfer") {

                        var cc = document.getElementById("<%=ddlpopcccode.ClientID %>");
                        var ccctrl = document.getElementById("<%=ddlpopcccode.ClientID %>");
                        if (ccctrl.selectedIndex == 0) {
                            window.alert("Please Select Cost Center");
                            ccctrl.focus();
                            return false;
                        }
                    }
                }
            }
            else if (GridView2 != null) {
                var type = document.getElementById("<%=Hftype.ClientID %>").value;

                var pumremarks = document.getElementById("<%=txtpurmremark.ClientID %>").value;
                if (pumremarks == "") {
                    window.alert("Please Enter Remarks");
                    document.getElementById("<%=txtpurmremark.ClientID %>").focus();
                    return false;
                }
                else {
                    for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                        if (GridView2.rows(rowCount).cells(13).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (type == "Full Issue") {
                            if (GridView2.rows(rowCount).cells(10).children[0].value == "") {
                                window.alert("Please Enter Quantity");
                                return false;
                            }
                        }
                    }
                }
            }
            else if (GridView3 != null) {
                var username = '<%= Session["roles"] %>';
                if (username == "Chief Material Controller") {
                    var cmcmremarks = document.getElementById("<%=txtcmcremark.ClientID %>").value;
                    if (cmcmremarks == "") {
                        window.alert("Please Enter Remarks");
                        document.getElementById("<%=txtcmcremark.ClientID %>").focus();
                        return false;
                    }
                }
                else if (username == "SuperAdmin") {
                    var SAremarks = document.getElementById("<%=txtsaremark.ClientID %>").value;
                    if (SAremarks == "") {
                        window.alert("Please Enter Remarks");
                        document.getElementById("<%=txtsaremark.ClientID %>").focus();
                        return false;
                    }
                }

                for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                    if (GridView3.rows(rowCount).cells(13).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView3.rows[GridView3.rows.length - 1].cells[10].innerHTML == "0") {
                        var response = confirm("Do you want to Close the Indent");
                        if (response) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                }

            }

            document.getElementById("<%=btnmdlupd.ClientID %>").style.display = 'none';
            return true;
        }


        function validatedeleteitems() {            
            var GridView2 = document.getElementById("<%=Grdeditpopup.ClientID %>");
             var j = 0;
            if (GridView2 != null) {                
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    if (GridView2.rows(rowCount).cells(13).children(0).checked == true) {
                       j = j + 1;
                    }
                }
                if (parseInt(j) == 0) {
                    alert("Please select atleast one Itemcode for Remove");
                    return false;
                }
            }
            document.getElementById("<%=btnmdlupd.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function uncheckqty() {
            var GridView = document.getElementById("<%=grideditpopup2.ClientID %>");
            var GridView1 = document.getElementById("<%=gridcentral.ClientID %>");
            var GridView2 = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var GridView3 = document.getElementById("<%=grdcmc.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(11).children(0).checked == true) {
                        GridView.rows(rowCount).cells(8).children[0].disabled = true;
                    }
                    else {
                        GridView.rows(rowCount).cells(8).children[0].disabled = false;
                    }
                }
            }
            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(13).children(0).checked == true) {
                        GridView1.rows(rowCount).cells(9).children(0).disabled = true;
                    }
                    else {
                        GridView1.rows(rowCount).cells(9).children[0].disabled = false;
                    }
                }
            }
            if (GridView2 != null) {
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    if (GridView2.rows(rowCount).cells(13).children(0).checked == true) {
                        GridView2.rows(rowCount).cells(10).children[0].disabled = true;
                    }
                    else {
                        GridView2.rows(rowCount).cells(10).children[0].disabled = false;
                    }
                }
            }
            if (GridView3 != null) {
                for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                    if (GridView3.rows(rowCount).cells(13).children(0).checked == true) {
                        GridView3.rows(rowCount).cells(9).children[0].disabled = true;
                    }
                    else {
                        GridView3.rows(rowCount).cells(9).children[0].disabled = false;
                    }
                }
            }
        }

    </script>
    <script language="javascript">        /*This Script for Auto Calculation*/
        function calincharge() {
            grd = document.getElementById("<%=grideditpopup2.ClientID %>");

            var amt = 0;
            var total = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                if (Number(grd.rows(rowCount).cells[6].innerHTML) != 0.00) {
                    if (!isNaN(grd.rows(rowCount).cells(8).children[0].value)) {
                        amt = (Number(grd.rows(rowCount).cells[6].innerHTML)) * (Number(grd.rows(rowCount).cells[8].children[0].value));
                        grd.rows(rowCount).cells(9).children[0].value = amt;
                        total += Number(grd.rows(rowCount).cells(9).children[0].value);
                    }
                }
                else {
                    total += Number(grd.rows(rowCount).cells(9).children[0].value);
                }
            }
            grd.rows[grd.rows.length - 1].cells[9].innerHTML = total;
        }
        function calcentral() {
            grd = document.getElementById("<%=gridcentral.ClientID %>");
            var amt = 0;
            var total = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                if (!isNaN(grd.rows(rowCount).cells(9).children[0].value)) {
                    amt = (Number(grd.rows(rowCount).cells[6].innerHTML)) * ((Number(grd.rows(rowCount).cells[8].innerHTML)) - (Number(grd.rows(rowCount).cells[9].children[0].value)));
                    grd.rows(rowCount).cells(10).innerHTML = amt;
                    total += Number(grd.rows(rowCount).cells(10).innerHTML);
                }
            }

            grd.rows[grd.rows.length - 1].cells[10].innerHTML = total;
        }
        function calpurchase() {
            grd = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var amt = 0;
            var total = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                if (!isNaN(grd.rows(rowCount).cells(10).children[0].value)) {
                    amt = (Number(grd.rows(rowCount).cells[6].innerHTML)) * Number(Number(grd.rows(rowCount).cells[8].innerHTML) - Number(Number(grd.rows(rowCount).cells[9].innerHTML) + Number(grd.rows(rowCount).cells[10].children[0].value)));
                    grd.rows(rowCount).cells(11).innerHTML = amt;
                    total += Number(grd.rows(rowCount).cells(11).innerHTML);
                }
            }
            grd.rows[grd.rows.length - 1].cells[11].innerHTML = total;
        }
        function calCMC() {
            grd = document.getElementById("<%=grdcmc.ClientID %>");
            var amt = 0;
            var total = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                if (!isNaN(grd.rows(rowCount).cells(9).children[0].value)) {
                    if (grd.rows(rowCount).cells(9).children[0].value != "") {
                        amt = (grd.rows(rowCount).cells[6].innerHTML) * (grd.rows(rowCount).cells[9].children[0].value);
                    }
                    else {
                        amt = (grd.rows(rowCount).cells[6].innerHTML) * (grd.rows(rowCount).cells[8].innerHTML);
                    }
                    grd.rows(rowCount).cells(10).innerHTML = amt;
                    total += Number(grd.rows(rowCount).cells(10).innerHTML);
                }
            }
            grd.rows[grd.rows.length - 1].cells[10].innerHTML = total;
        }

    </script>
    <script language="javascript">        /*This Script for Numeric Check*/
        function IsNumeric3(evt) {
            GridView = document.getElementById("<%=grideditpopup2.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                }
            }
        }
        function IsNumeric2(evt) {
            GridView = document.getElementById("<%=gridcentral.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                }
            }
        }
        function IsNumeric4(evt) {
            GridView = document.getElementById("<%=grdcmc.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                }
            }
        }
    </script>
    <script language="javascript">        /*This script is for Quantity Checking*/
        function Checkqty() {
            GridView = document.getElementById("<%=gridcentral.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var rqty = GridView.rows(rowCount).cells(8).innerText;
                var iqty = GridView.rows(rowCount).cells(9).children[0].value;

                if (parseInt(rqty) < parseInt(iqty)) {
                    window.alert("You are not able to issue more quantity");
                    GridView.rows(rowCount).cells(9).children[0].focus();
                    GridView.rows(rowCount).cells(9).children[0].value = "";
                    calcentral();
                    return false;
                }
            }
        }
        function Checkqty1() {
            GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var rqty = GridView.rows(rowCount).cells(8).innerText;
                var cqty = GridView.rows(rowCount).cells(9).innerText;
                var iqty = GridView.rows(rowCount).cells(10).children[0].value;
                var bal = parseInt(rqty) - parseInt(cqty);
                if (parseInt(bal) < parseInt(iqty)) {
                    window.alert("You are not able to issue more quantity");
                    GridView.rows(rowCount).cells(10).children[0].focus();
                    GridView.rows(rowCount).cells(10).children[0].value = "";
                    calpurchase();
                    return false;
                }
            }
        }
        function checkpm() {
            GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var type = document.getElementById("<%=Hftype.ClientID %>").value;
            if (type == "Full Issue") {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    var rqty = GridView.rows(rowCount).cells(8).innerText;
                    var iqty = GridView.rows(rowCount).cells(10).children[0].value;
                    if (parseInt(rqty) != parseInt(iqty)) {
                        GridView.rows(rowCount).cells(10).children[0].value = GridView.rows(rowCount).cells(8).innerText;
                        GridView.rows(rowCount).cells(10).children[0].disabled = true;
                        calpurchase();
                        return false;
                    }
                }
            }
        }
        function Deletevalidate() {
            if (confirm("Are you sure,you want to delete selected records ?") == true) {

                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script language="javascript">
        function checkitemtype() {
            GridView = document.getElementById("<%=grideditpopup2.ClientID %>");
            var itemtypes = document.getElementById("<%=ddlsearchtype.ClientID %>").value;
            var itemtypectrl = document.getElementById("<%=ddlsearchtype.ClientID %>");
            if (GridView != null) {
                var itemtype = GridView.rows(1).cells(1).innerHTML.substring(0, 1);
                if (itemtypes != itemtype) {
                    alert("You are not able to add multiple DCA items");

                    itemtypectrl.focus();
                    document.getElementById("<%=ddlsearchtype.ClientID %>").value = "Select Type";
                    return false;
                }
            }
        }
    </script>
    <script language="javascript">
        function checkstatus() {
            GridView = document.getElementById("<%=gridcentral.ClientID %>");
            var itemtypes = document.getElementById("<%=ddlindenttype.ClientID %>").value;
            var itemtypectrl = document.getElementById("<%=ddlindenttype.ClientID %>");
            var cc = document.getElementById("<%=tdcccode.ClientID %>");
            if (GridView != null) {
                var itemtype = GridView.rows(1).cells(1).innerHTML.substring(0, 1);

                if ((itemtype == "1") && ((itemtypes == "Full Purchase") || (itemtypes == "Partially Purchase") || (itemtypes == "SemiAssets/Consumable Transfer") || (itemtypes == "SemiAssets/Consumable Issue") || (itemtypes == "Full Issue"))) {
                    alert("Please Select Asset Issue Or Asset Transfer");
                    itemtypectrl.focus();
                    document.getElementById("<%=ddlindenttype.ClientID %>").value = "Select";
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        GridView.rows(rowCount).cells(9).children[0].disabled = 'true';
                        calcentral();
                    }
                    return false;
                }

                else if ((itemtype == "2" || itemtype == "3") && ((itemtypes == "Assets Issue") || (itemtypes == "Assets Transfer"))) {
                    alert("Please Select Correct Indent Type");
                    itemtypectrl.focus();
                    document.getElementById("<%=ddlindenttype.ClientID %>").value = "Select";
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        GridView.rows(rowCount).cells(9).children[0].disabled = false;
                        calcentral();
                    }
                    return false;
                }
                else if ((itemtype == "2" || itemtype == "3") && itemtypes == "Full Issue") {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        var reqqty = GridView.rows(rowCount).cells(8).innerText;
                        var newstock = GridView.rows(rowCount).cells(12).innerText;
                        if (parseInt(reqqty) > parseInt(newstock)) {
                            alert("Raised quantity was not avaliable in new stock")
                            document.getElementById("<%=btnmdlupd.ClientID %>").style.visibility = 'hidden';
                        }
                        GridView.rows(rowCount).cells(9).children[0].disabled = true;
                        GridView.rows(rowCount).cells(9).children[0].innerText = "";
                        calcentral();
                    }
                    return false;
                }
                else if ((itemtype == "2") && itemtypes == "SemiAssets/Consumable Issue") {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        var reqqty = GridView.rows(rowCount).cells(8).innerText;
                        var newstock = GridView.rows(rowCount).cells(11).innerText;
                        if (parseInt(reqqty) > parseInt(newstock)) {
                            alert("Raised quantity was not avaliable in  stock")
                            document.getElementById("<%=btnmdlupd.ClientID %>").style.visibility = 'hidden';
                        }

                        GridView.rows(rowCount).cells(9).children[0].disabled = false;
                        calcentral();
                    }
                    return false;
                }

                else {
                    document.getElementById("<%=ddlindenttype.ClientID %>").value = itemtypes;
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (GridView.rows(rowCount).cells(9).children[0]) {
                            GridView.rows(rowCount).cells(9).children[0].disabled = false;
                            GridView.rows(rowCount).cells(9).children[0].innerText = "";
                            document.getElementById("<%=btnmdlupd.ClientID %>").style.visibility = 'visible';
                            calcentral();
                        }
                    }
                    return false;
                }

            }
        }

    </script>
    <script language="javascript">
        function checkvalidation() {
            GridView = document.getElementById("<%=gridcentral.ClientID %>");
            var itemtypes = document.getElementById("<%=ddlindenttype.ClientID %>").value;
            var ccode = document.getElementById("<%=ddlpopcccode.ClientID %>").value;
            var itemtypectrl = document.getElementById("<%=ddlindenttype.ClientID %>");
            var cc = document.getElementById("<%=tdcccode.ClientID %>");
            if (GridView != null) {

                if ((itemtypes == "SemiAssets/Consumable Transfer") && (ccode == "CC-33")) {
                    alert("Invalid CC Code");
                    document.getElementById("<%=ddlpopcccode.ClientID %>").selectedIndex = 0;
                }
                else if (itemtypes == "SemiAssets/Consumable Transfer") {
                    cc.disabled = false;
                }
                else {
                    cc.disabled = true;
                    document.getElementById("<%=ddlpopcccode.ClientID %>").selectedIndex = 0;
                }
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (itemtypes == "Full Purchase") {
                        GridView.rows(rowCount).cells(9).children[0].disabled = true;
                        GridView.rows(rowCount).cells(9).children[0].innerText = "";
                    }
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function rectionDoller() {
            if (event.keyCode == 36) {
                alert("Can't Enter Doller");
                event.returnvalue = false;
                return false;
            }
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
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>View Indent <a class="help"
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
                                                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                                                                                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" OnClick="btnReset_Click" />
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
                                                                                                        <h2>Indents</h2>
                                                                                                    </td>
                                                                                                    <td class="loading-list" style="display: none;">
                                                                                                        <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                                                                    </td>
                                                                                                    <td class="pager-cell-button">
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
                                                                                                OnDataBound="GridView1_DataBound" DataKeyNames="Indent No" OnRowDeleting="GridView1_RowDeleting1"
                                                                                                OnRowEditing="GridView1_RowEditing2" OnRowDataBound="GridView1_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" ShowDeleteButton="true" ItemStyle-Width="15px"
                                                                                                        DeleteImageUrl="~/images/fields-a-lookup-a.gif" />
                                                                                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                                                                        EditImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                                    <asp:TemplateField HeaderText="Indent No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                                                                        ItemStyle-VerticalAlign="Bottom">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lablindentno" runat="server" Text='<%#Eval("Indent No") %>' />
                                                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="CC Code" HeaderText="CC Code" />
                                                                                                    <asp:BoundField DataField="Indent Date" HeaderText="Indent Date" />
                                                                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                                                    <asp:BoundField DataField="Indent Cost" HeaderText="Indent Cost" />
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
                                                                                            <table width="900px" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td width="13" valign="bottom">
                                                                                                        <img src="images/leftc.jpg">
                                                                                                    </td>
                                                                                                    <td class="pop_head" align="left" id="viewind" runat="server">
                                                                                                        <div class="popclose">
                                                                                                            <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                                                        </div>
                                                                                                        View Indent
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
                                                                                                    <td bgcolor="#FFFFFF">&nbsp;
                                                                                                    </td>
                                                                                                    <td height="180" valign="top" class="popcontent">
                                                                                                        <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px; height: 400px;">
                                                                                                            <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
                                                                                                                        <ProgressTemplate>
                                                                                                                            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                                                                                                <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
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
                                                                                                                                <table border="0" class="fields" width="100%" id="Table1" runat="server">
                                                                                                                                    <tr id="mpcsk" runat="server">
                                                                                                                                        <td class="label search_filters search_fields" align="center" colspan="7" width="80%">
                                                                                                                                            <table class="search_table" id="tblSearch" runat="server">
                                                                                                                                                <tr style="height: 18px">
                                                                                                                                                    <td colspan="1"></td>
                                                                                                                                                    <td colspan="1" align="center"></td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 100px">
                                                                                                                                                        <asp:DropDownList ID="ddlsearchtype" CssClass="char" runat="server" OnSelectedIndexChanged="ddlsearchtype_SelectedIndexChanged"
                                                                                                                                                            AutoPostBack="true" onchange="checkitemtype();">
                                                                                                                                                            <asp:ListItem Value="Select Type">Select Type</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="1">Assets</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="2">Semi Assets/Consumables</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="3">Consumables</asp:ListItem>
                                                                                                                                                            <asp:ListItem Value="4">Bought Out Items</asp:ListItem>
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="item m2o_search" valign="middle">
                                                                                                                                                        <asp:TextBox ID="txtSearch" CssClass="m2o_search" runat="server" Style="background-image: url(images/search_grey.png); background-position: right; background-repeat: no-repeat; border-color: #CBCCCC; font-size: smaller;"
                                                                                                                                                            onkeydown="restrictComma();return isNumberKey(event,this);"></asp:TextBox>
                                                                                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList"
                                                                                                                                                            ServicePath="cascadingDCA.asmx" TargetControlID="txtSearch" UseContextKey="True"
                                                                                                                                                            CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                                                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                                                                                            MinimumPrefixLength="1" CompletionListElementID="listPlacement">
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
                                                                                                                                <table id="tblccode" runat="server">
                                                                                                                                    <tr>
                                                                                                                                        <td id="tdindenttype" runat="server">
                                                                                                                                            <asp:DropDownList ID="ddlindenttype" CssClass="char" ToolTip="Indent Type" runat="server"
                                                                                                                                                onchange="javascript:checkstatus();checkvalidation();">
                                                                                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                                                                                                <asp:ListItem>Full Purchase</asp:ListItem>
                                                                                                                                                <asp:ListItem>Partially Purchase</asp:ListItem>
                                                                                                                                                <asp:ListItem>Assets Issue</asp:ListItem>
                                                                                                                                                <asp:ListItem>Assets Transfer</asp:ListItem>
                                                                                                                                                <asp:ListItem>SemiAssets/Consumable Transfer</asp:ListItem>
                                                                                                                                                <asp:ListItem>Full Issue</asp:ListItem>
                                                                                                                                                <asp:ListItem>SemiAssets/Consumable Issue</asp:ListItem>
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </td>
                                                                                                                                        <td class="item item-selection" valign="middle" id="tdcccode" runat="server">
                                                                                                                                            <asp:DropDownList ID="ddlpopcccode" ToolTip="CC Code" CssClass="filter_item" onchange="javascript:checkvalidation();"
                                                                                                                                                runat="server">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                            <cc1:CascadingDropDown ID="CascadingCCode" runat="server" TargetControlID="ddlpopcccode"
                                                                                                                                                ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="costcode"
                                                                                                                                                PromptText="Select Cost Center">
                                                                                                                                            </cc1:CascadingDropDown>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                                <table id="tblview" runat="server">
                                                                                                                                    <tr>
                                                                                                                                        <td id="td1" runat="server" style="width: 200px" align="center">
                                                                                                                                            <asp:Label ID="Label1" runat="server" Text="CC Code:- " CssClass="char"></asp:Label>
                                                                                                                                            <asp:TextBox ID="txtpopcccode" runat="server" Width="80px" Enabled="false" CssClass="char"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                        <td class="item item-selection" valign="middle" id="td2" runat="server" colspan="2">
                                                                                                                                            <asp:Label ID="Label3" runat="server" Text="Indent No:- " CssClass="char"></asp:Label>
                                                                                                                                            <asp:TextBox ID="txtpopindno" runat="server" CssClass="char" Enabled="false" Width="150px"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                                <table id="tbltype" runat="server" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td id="td3" runat="server" align="center" colspan="3">
                                                                                                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="Small">Indent Type :-  </asp:Label>
                                                                                                                                            <asp:Label ID="lbltype" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                                <table>
                                                                                                                                    <tr id="addbtn" runat="server">
                                                                                                                                        <td class="pager-cell" align="left">
                                                                                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Font-Names="Tahoma" Text="Add Indents :-"></asp:Label>
                                                                                                                                            &nbsp;
                                                                                                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" OnClientClick="javascript:return searchvalidate()"
                                                                                                                                                OnClick="btnAdd_Click" />
                                                                                                                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" OnClientClick="return Deletevalidate();"
                                                                                                                                                OnClick="btnDelete_Click" />
                                                                                                                                            <asp:Button ID="Button1" runat="server" CssClass="button" Height="18px" Width="120px"
                                                                                                                                                OnClientClick="javascript:return ClearTextboxes();" Text="Clear SearchBox" />
                                                                                                                                        </td>
                                                                                                                                        <td class="pager-cell" style="width: 60%; padding-left: 5px;" valign="middle" align="left"></td>
                                                                                                                                    </tr>
                                                                                                                                    <tr id="CST" runat="server">
                                                                                                                                        <td colspan="2">
                                                                                                                                            <asp:HiddenField ID="Hftype" runat="server" />
                                                                                                                                            <asp:GridView ID="Grdeditpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                DataKeyNames="id" ShowFooter="true" Width="90%" OnRowDataBound="Grdeditpopup_RowDataBound1">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <HeaderTemplate>
                                                                                                                                                            Serial No.
                                                                                                                                                        </HeaderTemplate>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
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
                                                                                                                                                    <asp:BoundField DataField="Raised Qty" HeaderText="Requested Qty" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:BoundField DataField="issued qty" HeaderText="Issued Stock Qty" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:TemplateField HeaderText="Issued New Stock">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:TextBox ID="txtqty" runat="server" onkeyup="calpurchase();" onkeydown="checkpm();"
                                                                                                                                                                onblur="Checkqty1();" Width="50px"></asp:TextBox>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:TemplateField HeaderText="Amount">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblamount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
                                                                                                                                                        </FooterTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="Available Qty" HeaderText="New Stock" />
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="uncheckqty();" />
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                </Columns>
                                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                <HeaderStyle CssClass="grid-header" />
                                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                            </asp:GridView>
                                                                                                                                            <asp:GridView ID="gridcentral" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                DataKeyNames="id" ShowFooter="true" Width="90%" OnRowDataBound="gridcentral_RowDataBound"
                                                                                                                                                FooterStyle-HorizontalAlign="Right">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <HeaderTemplate>
                                                                                                                                                            Serial No.
                                                                                                                                                        </HeaderTemplate>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                                                                                    <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                                                                    <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="100px"
                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                    <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                    <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                                                                    <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                                                                    <asp:BoundField DataField="basic_price" HeaderText="Basic Price" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                    <asp:BoundField DataField="Quantity" HeaderText="Raised Qty" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:TemplateField HeaderText="Issued Qty">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:TextBox ID="txtqty" runat="server" Width="50px" Style="text-align: right" onkeyup="calcentral();"
                                                                                                                                                                onblur="Checkqty();"></asp:TextBox>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblamount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
                                                                                                                                                        </FooterTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="Availqty" HeaderText="Stock" />
                                                                                                                                                    <asp:BoundField DataField="Newqty" HeaderText="New Stock" />
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="uncheckqty();" />
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
                                                                                                                                    <tr id="SI" runat="server">
                                                                                                                                        <td colspan="2">
                                                                                                                                            <asp:GridView ID="grideditpopup2" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                DataKeyNames="id" ShowFooter="true" Width="90%" FooterStyle-HorizontalAlign="Right" OnRowDataBound="grideditpopup2_RowDataBound">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <HeaderTemplate>
                                                                                                                                                            Serial No.
                                                                                                                                                        </HeaderTemplate>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                                                                                    <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                                                                    <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Left"
                                                                                                                                                        ItemStyle-Width="100px" />
                                                                                                                                                    <asp:BoundField DataField="Specification" ItemStyle-HorizontalAlign="Left" HeaderText="Specification" />
                                                                                                                                                    <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                                                                    <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                                                                    <asp:BoundField DataField="basic_price" HeaderText="Basic Price" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:TextBox ID="txtqty" runat="server" Width="50px" Style="text-align: right" onkeyup="calincharge();"
                                                                                                                                                                onkeypress='IsNumeric3(event)' Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:TemplateField HeaderText="Amount">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:TextBox ID="txtamount" runat="server" Width="75px" onkeyup="calincharge();"
                                                                                                                                                                onkeypress='IsNumeric3(event)' Style="text-align: right" Text='<%#Bind("Amount") %> '></asp:TextBox>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:Label ID="lblTotal" Style="text-align: right" runat="server" Text="0.00"></asp:Label>
                                                                                                                                                        </FooterTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="Availqty" HeaderText="Available Qty" />
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="uncheckqty();" />
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
                                                                                                                                        <td>
                                                                                                                                            <asp:GridView ID="grdcmc" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                DataKeyNames="id" ShowFooter="true" Width="90%" OnRowDataBound="grdcmc_RowDataBound">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <HeaderTemplate>
                                                                                                                                                            Serial No.
                                                                                                                                                        </HeaderTemplate>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
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
                                                                                                                                                    <asp:BoundField DataField="quantity" HeaderText="Raised Qty" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:TextBox ID="txtqty" runat="server" onkeyup="calCMC();" onkeypress='IsNumeric4(event)'
                                                                                                                                                                Width="50px"></asp:TextBox>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:TemplateField HeaderText="Amount">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:Label ID="lblamount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
                                                                                                                                                        </FooterTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="IssuedStock" HeaderText="Issued From Stock" />
                                                                                                                                                    <asp:BoundField DataField="IssuedNewStock" HeaderText="Issued From New Stock" />
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="uncheckqty();" />
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
                                                                                                                                            <asp:GridView ID="grideviewpopup" runat="server" CssClass="grid-content"
                                                                                                                                                HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                BorderColor="White" Width="90%" RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar">
                                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                                                            </asp:GridView>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <caption>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table id="tb2" runat="server" border="0" class="fields" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td align="left" class="item item-selection">
                                                                                                                                                <asp:Label ID="lblremarks" runat="server" Font-Size="12pt" Font-Bold="True" Text="Remarks"
                                                                                                                                                    Width="150"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="left" class="item item-selection" valign="middle">
                                                                                                                                                <asp:Label ID="lbl1" runat="server" Text="" Width="25px"></asp:Label>
                                                                                                                                                <asp:Label ID="lblskremark" runat="server" Text="Store Keeper" Width="150px" Font-Size="Small"></asp:Label>
                                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                                                <asp:TextBox ID="txtskremark" runat="server" CssClass="filter_item" MaxLength="50"
                                                                                                                                                    TextMode="MultiLine" ToolTip="Description" Width="450px" Font-Size="Small" onkeypress="return rectionDoller()"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right">
                                                                                                                                                <asp:Label ID="Lblskname" runat="server" Font-Size="Small"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="left" class="item item-selection" valign="middle">
                                                                                                                                                <asp:Label ID="lbl2" runat="server" Text="" Width="25px"></asp:Label>
                                                                                                                                                <asp:Label ID="lblpmremark" runat="server" Text="Project Manager" Width="150px" Font-Size="Small"></asp:Label>
                                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                                                <asp:TextBox ID="txtpmremark" runat="server" CssClass="filter_item" MaxLength="50"
                                                                                                                                                    TextMode="MultiLine" ToolTip="Description" Width="450px" Font-Size="Small" onkeypress="return rectionDoller()"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right">
                                                                                                                                                <asp:Label ID="Lblpmname" runat="server" Font-Size="Small"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="left" class="item item-selection" valign="middle">
                                                                                                                                                <asp:Label ID="lbl3" runat="server" Text="" Width="25px"></asp:Label>
                                                                                                                                                <asp:Label ID="lblcskremark" runat="server" Text="Central Store Keeper" Width="150px"
                                                                                                                                                    Font-Size="Small"></asp:Label>
                                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                                                <asp:TextBox ID="txtcskremark" runat="server" CssClass="filter_item" MaxLength="50"
                                                                                                                                                    TextMode="MultiLine" ToolTip="Description" Width="450px" Font-Size="Small" onkeypress="return rectionDoller()"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right">
                                                                                                                                                <asp:Label ID="Lblcskname" runat="server" Font-Size="Small"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="left" class="item item-selection" valign="middle">
                                                                                                                                                <asp:Label ID="lbl4" runat="server" Text="" Width="25px"></asp:Label>
                                                                                                                                                <asp:Label ID="lblpurmremark" runat="server" CssClass="char" Text="Purchase Manager"
                                                                                                                                                    Width="150px" Font-Size="Small"></asp:Label>
                                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                                                <asp:TextBox ID="txtpurmremark" runat="server" CssClass="filter_item" MaxLength="50"
                                                                                                                                                    TextMode="MultiLine" ToolTip="Description" Width="450px" Font-Size="Small" onkeypress="return rectionDoller()"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right">
                                                                                                                                                <asp:Label ID="Lblpurname" runat="server" Font-Size="Small"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="left" class="item item-selection" valign="middle">
                                                                                                                                                <asp:Label ID="lbl5" runat="server" Text="" Width="25px"></asp:Label>
                                                                                                                                                <asp:Label ID="lblcmcremark" runat="server" CssClass="char" Text="Chief Material Controller"
                                                                                                                                                    Width="150px" Font-Size="Small"></asp:Label>
                                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                                                <asp:TextBox ID="txtcmcremark" runat="server" CssClass="filter_item" MaxLength="50"
                                                                                                                                                    TextMode="MultiLine" ToolTip="Description" Width="450px" Font-Size="Small" onkeypress="return rectionDoller()"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="right">
                                                                                                                                                <asp:Label ID="Lblcmcname" runat="server" Font-Size="Small"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="left" class="item item-selection" valign="middle">
                                                                                                                                                <asp:Label ID="lbl6" runat="server" Text="" Width="25px"></asp:Label>
                                                                                                                                                <asp:Label ID="lblsaremark" runat="server" CssClass="char" Text="Super Admin" Width="150px"
                                                                                                                                                    Font-Size="Small"></asp:Label>
                                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                                                <asp:TextBox ID="txtsaremark" runat="server" CssClass="filter_item" TextMode="MultiLine"
                                                                                                                                                    ToolTip="Description" Width="450px" Font-Size="Small" onkeypress="return rectionDoller()"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <tr id="button" runat="server">
                                                                                                                                            <td align="center">
                                                                                                                                                <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                                                                                                                                <asp:Button ID="btnmdlupd" runat="server" Text="" CssClass="button" OnClientClick="javascript:return validate();"
                                                                                                                                                    OnClick="btnmdlupd_Click" />
                                                                                                                                                <asp:Button ID="btndeleteitemcodes" runat="server" Text="Delete ItemCodes" BackColor="Red" CssClass="button" OnClientClick="javascript:return validatedeleteitems();" OnClick="btndelteitemcodes_Click" />
                                                                                                                                              
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </tr>
                                                                                                                            </tr>
                                                                                                                        </caption>
                                                                                                                    </table>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td bgcolor="#FFFFFF">&nbsp;
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
                                                                                    <td class="pagerbar-cell" align="right"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <%--</ContentTemplate>
                                                                </asp:UpdatePanel>--%>
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
