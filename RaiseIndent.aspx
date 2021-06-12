<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="RaiseIndent.aspx.cs"
    Inherits="RaiseIndent" Title="Raise Indent - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/bubble-tooltip.css" rel="stylesheet" type="text/css" />
    <link href="Css/style.css" rel="stylesheet" type="text/css" />
    <link href="Css/buttons.css" rel="stylesheet" type="text/css" />
    <link href="Css/screen.css" rel="stylesheet" type="text/css" />
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

        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=GridView1.ClientID %>");
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
    <script type="text/javascript">

        function CalculateTotals() {
            var gv = document.getElementById("<%=GridView1.ClientID %>");
            var tb = gv.getElementsByTagName("input");
            var lb = gv.getElementsByTagName("span");

            var sub = 0;
            var total = 0;
            var indexQ = 1;
            var indexP = 0;
            var price = 0;
            var qty = 0;
            var totalQty = 0;
            var tbCount = tb.length / 3;

            for (var i = 1; i <= tbCount; i++) {

                if (tb[i].type == "text" || tb[i].type == "checkbox") {
                    ValidateNumber(tb[i + indexQ]);

                    sub = parseFloat(tb[i + indexP].value) * parseFloat(tb[i + indexQ].value);

                    if (isNaN(sub)) {
                        lb[i - 1].innerHTML = "0.00";
                        sub = 0;
                    }
                    else {
                        lb[i - 1].innerHTML = FormatToMoney(sub, "", ",", "."); ;
                    }

                    if (isNaN(tb[i + indexQ].value) || tb[i + indexQ].value == "") {
                        qty = 0;
                    }
                    else {
                        qty = tb[i + indexQ].value;
                    }

                    totalQty += parseInt(qty);
                    total += parseFloat(sub);

                    indexQ = indexQ + 2;
                    indexP = indexP + 2;
                }
            }

            // lb[lb.length - 2].innerHTML = totalQty;
            lb[lb.length - 1].innerHTML = FormatToMoney(total, "Rs", ",", ".");
        }

        function ValidateNumber(o) {
            if (o.value.length > 0) {
                o.value = o.value.replace(/[^\d]+/g, ''); //Allow only whole numbers
            }
        }
        function isThousands(position) {
            if (Math.floor(position / 3) * 3 == position) return true;
            return false;
        };

        function FormatToMoney(theNumber, theCurrency, theThousands, theDecimal) {
            var theDecimalDigits = Math.round((theNumber * 100) - (Math.floor(theNumber) * 100));
            theDecimalDigits = "" + (theDecimalDigits + "0").substring(0, 2);
            theNumber = "" + Math.floor(theNumber);
            var theOutput = theCurrency;
            for (x = 0; x < theNumber.length; x++) {
                theOutput += theNumber.substring(x, x + 1);
                if (isThousands(theNumber.length - x - 1) && (theNumber.length - x - 1 != 0)) {
                    theOutput += theThousands;
                };
            };
            theOutput += theDecimal + theDecimalDigits;
            return theOutput;
        }
    </script>
    <script language="javascript">
        function validate() {
            GridView = document.getElementById("<%=GridView1.ClientID %>");
            var date = document.getElementById("<%=txtdate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdate.ClientID %>");
            var desc = document.getElementById("<%=txtdesc.ClientID %>").value;
            var descctrl = document.getElementById("<%=txtdesc.ClientID %>");

            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                    window.alert("Please verify");
                    return false;
                }
                else if (GridView.rows(rowCount).cells(8).children[0].value == "" || GridView.rows(rowCount).cells(9).children[0].value == 0) {
                    window.alert("Please Enter Quantity");
                    GridView.rows(rowCount).cells(8).children[0].focus();
                    return false;
                }
                else if (GridView.rows(rowCount).cells(9).children[0].value == "" || GridView.rows(rowCount).cells(9).children[0].value == 0) {
                    window.alert("Please Enter Amount");
                    GridView.rows(rowCount).cells(9).children[0].focus();
                    return false;
                }

                //                  else if (GridView.rows(1).cells(4).innerHTML != GridView.rows(rowCount).cells(4).innerHTML || GridView.rows(1).cells(5).innerHTML != GridView.rows(rowCount).cells(5).innerHTML) {
                //                        window.alert("You are not able to add multiple DCA and SubDCA items");
                //                        GridView.rows(rowCount).cells(9).children[0].focus();
                //                        return false;
                //              
                //                }


            }


            if (date == "") {
                window.alert("Enter Date");
                datectrl.focus();
                return false;
            }
            if (desc == "") {
                window.alert("Enter Description");
                descctrl.focus();
                return false;
            }

            document.getElementById("<%=btnSave.ClientID %>").style.display = 'none';
            return true;
        }
        function searchvalidate() {

            GridView = document.getElementById("<%=GridView1.ClientID %>");
            var Search = document.getElementById("<%=txtSearch.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=txtSearch.ClientID %>");
            var CCCodectrl = document.getElementById("<%=ddlcccode.ClientID %>");
            var itemtype = document.getElementById("<%=ddlindenttype.ClientID %>").value;
            var itemtypectrl = document.getElementById("<%=ddlindenttype.ClientID %>");
            if (Search == "Search here.." || Search == "") {
                window.alert("Please Select an item");
                Searchctrl.focus();
                return false;
            }
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(1).innerHTML == Search || (GridView.rows(rowCount).cells(2).innerHTML + ',' + GridView.rows(rowCount).cells(3).innerHTML) == Search) {
                        window.alert("Selected Item is already added");
                        Search = "";
                        Searchctrl.focus();
                        return false;
                    }
//                    else if (GridView.rows(rowCount).cells(4).innerHTML != "DCA Code") {
//                        if (GridView.rows(rowCount).cells(4).innerHTML != GridView.rows(rowCount +1).cells(4).innerHTML) {
//                            window.alert("Selected Item is already added");
//                            Search = "";
//                            Searchctrl.focus();
//                            return false;
//                        }
//                    }               
                }
            }

            if (document.getElementById("<%=hf1.ClientID %>").value == "Central Store Keeper") {
                var CCCode = document.getElementById("<%=ddlcccode.ClientID %>").value;
                if (CCCode == "Select Cost Center") {
                    window.alert("Select Cost Center");
                    CCCodectrl.focus();
                    return false;
                }
                if (itemtype == "1" && CCCode == 'CC-33') {
                    window.alert("You are unable to add an item");
                    itemtypectrl.focus();
                    CCCodectrl.focus();
                    return false;
                }
            }

            var itemcode = document.getElementById("<%=txtSearch.ClientID %>").value.substring(0, 1);
            var itemtypectrl = document.getElementById("<%=ddlindenttype.ClientID %>");
            if (GridView != null) {
                if (itemcode > 0 && itemcode < 9) {
                    var itemtype = GridView.rows(1).cells(1).innerHTML.substring(0, 1);
                    if (itemtype == 1) {
                        if (itemcode != 1) {
                            alert("You are not able to add multiple DCA items");
                            itemtypectrl.focus();
                            document.getElementById("<%=txtSearch.ClientID %>").value = "";
                            return false;
                        }
                    }
                    if (itemtype != 1) {
                        if (itemcode == 1) {
                            alert("You are not able to add multiple DCA items");
                            itemtypectrl.focus();
                            document.getElementById("<%=txtSearch.ClientID %>").value = "";
                            return false;
                        }                        
                    }
                    return true;
                }
            }

        }
        function Deletevalidate() {
            if (confirm("Are you sure,you want to delete selected records ?") == true)
                return true;
            else
                return false;
        }
        function calculate() {
            grd = document.getElementById("<%=GridView1.ClientID %>");
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
    <script type="text/javascript" language="javascript">
        function rectionDoller() {
            if (event.keyCode == 36) {
                alert("Can't Enter Doller");
                event.returnvalue = false;
                return false;
            }
        }
   
    </script>
    <script language="javascript">
        function Loading1() {
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                $find('mpp').show();
            }
            function EndRequestHandler(sender, args) {
                $find('mpp').hide();
            }
        }


    </script>
    <script language="javascript">
        function checkitemtype() {
            GridView = document.getElementById("<%=GridView1.ClientID %>");
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
    <script type="text/javascript">
        function checkDate(sender, args) {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }
            var month = new Array();
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";
            var mmm = month[today.getMonth()];
            today = dd + '-' + mmm + '-' + yyyy;
            var str1 = document.getElementById("<%=txtdate.ClientID %>").value;
            var str2 = today;
            var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
            var dt1 = str1.substring(0, 2);
            var dt2 = str2.substring(0, 2);
            var yr1 = str1.substring(7, 11);
            var yr2 = str2.substring(7, 11);
            for (var i = 0; i < args.length; i++) {
                var month = str2.substring(3, 6);
                var month1 = str1.substring(3, 6);
                if (args[i] == month) {
                    var month = parseInt(i + 1);
                    var date2 = yr2 + "-" + month + "-" + dt2;

                }
                if (args[i] == month1) {
                    var month1 = parseInt(i + 1);
                    var date1 = yr1 + "-" + month1 + "-" + dt1;
                }

            }
            var one_day = 1000 * 60 * 60 * 24;
            var x = date1.split("-");
            var y = date2.split("-");

            var date4 = new Date(x[0], (x[1] - 1), x[2]);
            var date3 = new Date(y[0], (y[1] - 1), y[2]);

            var month1 = x[1] - 1;
            var month2 = y[1] - 1;
            //debugger;
            _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
            if (date4 > date3) {
                alert("Invalid Future Date Selection");
                document.getElementById("<%=txtdate.ClientID %>").value = "";
                return false;
            }
        }
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    }
                    else {
                        evt.returnValue = false;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top" align="center">
                <table width="100%">
                    <tr>
                        <td>
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
                                    <div class="wrap">
                                        <table class="view" cellpadding="0" cellspacing="0" border="0" width="90%">
                                            <tr align="center">
                                                <td align="center">
                                                    <div id="body_form">
                                                        <div>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 90%" valign="top" align="left">
                                                                        <h1>
                                                                            Indent Raise<a class="help" href="" title="Raise Indent"> <small>Help</small></a></h1>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <div id="search_filter_data">
                                                                                    <table border="0" class="fields" width="100%">
                                                                                        <tr>
                                                                                            <td class="item search_filters item-filtersgroup" valign="top">
                                                                                                <div class="filters-group">
                                                                                                </div>
                                                                                            </td>
                                                                                            <td class="label search_filters search_fields" align="center" colspan="4">
                                                                                                <asp:HiddenField ID="hf1" runat="server" />
                                                                                                <table class="search_table">
                                                                                                    <tbody>
                                                                                                        <tr height="73px">
                                                                                                            <td colspan="1">
                                                                                                            </td>
                                                                                                            <td colspan="1">
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
                                                                                                            <td width="150px">
                                                                                                                <table class="search_table" width="80%">
                                                                                                                    <tr>
                                                                                                                        <td class="item item-selection" valign="middle" width="">
                                                                                                                            <asp:Label ID="Label3" runat="server" Text="Date"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td class="item item-selection" valign="middle">
                                                                                                                            <asp:TextBox ID="txtdate" Font-Size="Small" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                                                                onkeypress="return false;" runat="server" Style="width: 130px; height: 20px;
                                                                                                                                vertical-align: middle"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                                                                                                Animated="true" PopupButtonID="txtdate">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                            <td style="width: 100px">
                                                                                                                <asp:DropDownList ID="ddlindenttype" CssClass="char" runat="server" OnSelectedIndexChanged="ddlindenttype_SelectedIndexChanged"
                                                                                                                    AutoPostBack="true" onchange="checkitemtype();">
                                                                                                                    <asp:ListItem Value="Select Type">Select Type</asp:ListItem>
                                                                                                                    <asp:ListItem Value="1">Assets</asp:ListItem>
                                                                                                                    <asp:ListItem Value="2">Semi Assets/Consumables</asp:ListItem>
                                                                                                                    <asp:ListItem Value="3">Consumables</asp:ListItem>
                                                                                                                    <asp:ListItem Value="4">Bought Out Items</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td class="item item-char" valign="middle" style="width: 600px">
                                                                                                                <asp:TextBox ID="txtSearch" CssClass="m2o_search" Width="100%" Height="20px" runat="server"
                                                                                                                    Style="background-image: url(images/search_grey.png); background-position: right;
                                                                                                                    background-repeat: no-repeat; border-color: #CBCCCC; font-size: smaller; text-transform: uppercase"
                                                                                                                    onkeydown="restrictComma();return isNumberKey(event,this);"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList"
                                                                                                                    ServicePath="cascadingDCA.asmx" TargetControlID="txtSearch" UseContextKey="True"
                                                                                                                    CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                                                    MinimumPrefixLength="1" CompletionListElementID="listPlacement" OnClientItemSelected="extender"
                                                                                                                    BehaviorID="dp1">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="Search here.."
                                                                                                                    WatermarkCssClass="watermarked" TargetControlID="txtSearch" runat="server">
                                                                                                                </cc1:TextBoxWatermarkExtender>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td class="label search_filters search_fields">
                                                                                                <table class="search_table">
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="item search_filters item-filtersgroup" for="" valign="top">
                                                                                                <div class="filters-group">
                                                                                                    <div style="display: none;">
                                                                                                        <div class="filter-a">
                                                                                                            <button type="button" class="filter_with_icon active" title="">
                                                                                                                <img src="images/personal+.png" width="16" height="16" alt="">
                                                                                                                Active
                                                                                                            </button>
                                                                                                            <input style="display: none;" type="checkbox" id="filter_241" name="" class="grid-domain-selector"
                                                                                                                onclick="" value="" title="">
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <table border="0" class="fields" width="100%">
                                                                                                        <tbody>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </td>
                                                                                            <td width="500px" id="cc" runat="server">
                                                                                                <table class="search_table" width="100%" align="center">
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" align="left" width="" colspan="2">
                                                                                                            <asp:Label ID="Label1" runat="server" Text="Cost Center"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle" align="left">
                                                                                                            <asp:DropDownList ID="ddlcccode" CssClass="char" runat="server">
                                                                                                                <asp:ListItem Value="Select Cost Center">Select Cost Center</asp:ListItem>
                                                                                                                <asp:ListItem Value="CC-33">CC-33 , Bhilai Central Store</asp:ListItem>
                                                                                                                <asp:ListItem Value="CCC">CCC , Company Cost Center</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
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
                                                                                                                    Add items to indent:-</h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                            </td>
                                                                                                            <td class="pager-cell">
                                                                                                                <asp:Button ID="btnadd" runat="server" Text="Add" Height="18px" CssClass="button"
                                                                                                                    OnClientClick="javascript:return searchvalidate()" OnClick="btnadd_Click" />&nbsp;&nbsp;
                                                                                                            </td>
                                                                                                            <td class="pager-cell">
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
                                                                                                <table id="_terp_list_grid" class="grid" width="100%" align="center" style="background: none;">
                                                                                                    <asp:GridView ID="GridView1" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        OnRowDataBound="GridView1_RowDataBound1" DataKeyNames="id" ShowFooter="true">
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
                                                                                                            <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtqty" runat="server" Width="50px" onkeyup="calculate();" onkeypress='IsNumeric1(event)'
                                                                                                                        Text='<%#Bind("Quantity")  %>'></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtamount" runat="server" Width="75px" onkeyup="calculate();" Text='<%#Bind("Amount") %>'></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <FooterTemplate>
                                                                                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                                                                </FooterTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="pagerbar" id="description" runat="server">
                                                                                            <td width="500px">
                                                                                                <table class="search_table" width="100%" align="center">
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" align="left" width="" colspan="2">
                                                                                                            <asp:Label ID="lbldate" runat="server" Text="Description"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle" align="left">
                                                                                                            <asp:TextBox ID="txtdesc" runat="server" CssClass="filter_item" ToolTip="Description"
                                                                                                                TextMode="MultiLine" Width="450px" onkeypress="return rectionDoller()" MaxLength="50"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="btnvisible" runat="server">
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnSave" Height="18px" runat="server" Text="Submit" CssClass="button"
                                                                                                    OnClick="btnSave_Click" OnClientClick="javascript:return validate()" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
