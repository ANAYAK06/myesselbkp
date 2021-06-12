<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="StockUpdation.aspx.cs"
    Inherits="StockUpdation" Title="Stock Updation - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Java_Script/validations.js" type="text/javascript"></script>
    <script language="javascript">
        function validate() {
            var objs = new Array("<%=txtpo.ClientID %>", "<%=txtrefno.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
        function validation() {
            var GridView = document.getElementById("<%=Vengrid.ClientID %>");
            var GridView1 = document.getElementById("<%=grid.ClientID %>");
            var GridView2 = document.getElementById("<%=CentralGrid.ClientID %>");

            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(10).children[0].value == "") {
                        window.alert("Enter Recieved Quantity");
                        GridView.rows(rowCount).cells(10).children[0].focus();
                        return false;
                    }

                }
            }
            else if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    var qty = GridView1.rows(rowCount).cells(7).innerText;
                    var rqty = GridView1.rows(rowCount).cells(10).children[0].value;
                    var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(10).children[0].value == "") {
                        window.alert("Enter Recieved Quantity");
                        GridView1.rows(rowCount).cells(10).children[0].focus();
                        return false;
                    }

                    else if (parseFloat(qty) > parseFloat(rqty) && GridView1.rows(rowCount).style.background == '') {
                        var response = confirm("Do you want to mention " + itemcode + " as lost or damaged item?");
                        if (response) {
                            GridView1.rows(rowCount).style.background = '#FBE5E6';
                            if (GridView1.rows(rowCount).cells(11).children(0).selectedIndex == 0) {
                                window.alert("Please Select Damage Type");
                                GridView1.rows(rowCount).cells(11).children(0).focus();
                                return false;
                            }
                            return false;
                        }
                        else {
                            GridView1.rows(rowCount).cells(10).children[0].focus();
                            GridView1.rows(rowCount).cells(10).children[0].value = "";
                            return false;
                        }
                        return true;
                    }



                }
            }
            else if (GridView2 != null) {
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    var qty = GridView2.rows(rowCount).cells(7).innerText;
                    var rqty = GridView2.rows(rowCount).cells(9).children[0].value;
                    var itemcode = GridView2.rows(rowCount).cells(1).innerText;
                    if (GridView2.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(9).children[0].value == "") {
                        window.alert("Enter Recieved Quantity");
                        GridView2.rows(rowCount).cells(9).children[0].focus();
                        return false;
                    }

                    else if (parseFloat(qty) > parseFloat(rqty) && GridView2.rows(rowCount).style.background == '') {
                        var response = confirm("Do you want to mention " + itemcode + " as lost or damaged item?");
                        if (response) {
                            GridView2.rows(rowCount).style.background = '#FBE5E6';
                            if (GridView2.rows(rowCount).cells(10).children(0).selectedIndex == 0) {
                                window.alert("Please Select Damage Type");
                                GridView2.rows(rowCount).cells(10).children(0).focus();
                                return false;
                            }
                            return false;
                        }
                        else {
                            GridView2.rows(rowCount).cells(9).children[0].focus();
                            GridView2.rows(rowCount).cells(9).children[0].value = "";
                            return false;
                        }
                        return true;
                    }



                }
            }

            var updationtype = document.getElementById("<%=ddlType.ClientID %>").value;
            if (updationtype == '3') {
                var objs = new Array("<%=txtdate.ClientID %>", "<%=txtremarks.ClientID %>");
            }
            if (!CheckInputs(objs)) {
                return false;
            }

            document.getElementById("<%=btnupdateprint.ClientID %>").style.display = 'none';
            return true;
        }
        function closepopup1() {
            $find('mdlindent1').hide();
        }
        function Openwindow() {
            $find('mdlindent1').show();
        }
        function Checkqty() {

            GridView = document.getElementById("<%=grid.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var qty = GridView.rows(rowCount).cells(7).innerText;
                var rqty = GridView.rows(rowCount).cells(10).children[0].value;
                var itemcode = GridView.rows(rowCount).cells(1).innerText;


                if (parseFloat(qty) < parseFloat(rqty)) {
                    window.alert("Invalid");
                    GridView.rows(rowCount).cells(10).children[0].focus();
                    GridView.rows(rowCount).cells(10).children[0].value = "";
                    return false;

                }

            }

        }
        function Checkqty1() {
            debugger;
            GridView = document.getElementById("<%=Vengrid.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var qty = GridView.rows(rowCount).cells(7).innerText;
                var rqty = GridView.rows(rowCount).cells(10).children[0].value;

                if (parseFloat(qty) < parseFloat(rqty)) {
                    window.alert("Invalid");
                    GridView.rows(rowCount).cells(10).children[0].focus();
                    GridView.rows(rowCount).cells(10).children[0].value = "";
                    return false;
                }
                else if (parseFloat(qty) > parseFloat(rqty)) {
                    var itemcode = GridView.rows(rowCount).cells(1).innerText;
                    var response = confirm(" " + itemcode + "  is receving less quantity than actual do you want to continue?");
                    if (response) {
                        return false;
                    }

                    else {
                        GridView.rows(rowCount).cells(10).children[0].focus();
                        GridView.rows(rowCount).cells(10).children[0].value = "";
                        return false;
                    }
                }
            }
        }
        function Checkqty2() {

            GridView = document.getElementById("<%=CentralGrid.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var qty = GridView.rows(rowCount).cells(7).innerText;
                var rqty = GridView.rows(rowCount).cells(9).children[0].value;
                var itemcode = GridView.rows(rowCount).cells(1).innerText;


                if (parseFloat(qty) < parseFloat(rqty)) {
                    window.alert("Invalid");
                    GridView.rows(rowCount).cells(9).children[0].focus();
                    GridView.rows(rowCount).cells(9).children[0].value = "";
                    return false;

                }

            }

        }

        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=Vengrid.ClientID %>");
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
        function IsNumeric2(evt) {
            GridView = document.getElementById("<%=grid.ClientID %>");
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

        function IsNumeric3(evt) {
            GridView = document.getElementById("<%=CentralGrid.ClientID %>");
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


        function CalculateTotals() {
            var gv = document.getElementById("<%=Vengrid.ClientID %>");
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

                    totalQty += parseFloat(qty);
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
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Stock Updation<a class="help"
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
                                                                                                <asp:Label ID="lbl" runat="server" Text="Stock Updation Types"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle">
                                                                                                <span class="filter_item">
                                                                                                    <asp:DropDownList ID="ddlType" CssClass="char" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                                                                        AutoPostBack="true">
                                                                                                        <asp:ListItem Value="Select Updation Type">Select Updation Type</asp:ListItem>
                                                                                                        <asp:ListItem Value="1">From Central Store</asp:ListItem>
                                                                                                        <asp:ListItem Value="2">From Other Site Store</asp:ListItem>
                                                                                                        <asp:ListItem Value="3">From Vendor</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
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
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="tbl" runat="server">
                                                            <td valign="top">
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
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
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button" style="display: none;">
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
                                                                                                <table class="grid" cellspacing="0" cellpadding="0">
                                                                                                    <asp:GridView ID="Vengrid" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        ShowFooter="true" DataKeyNames="id" OnRowDataBound="Vengrid_RowDataBound">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                            <asp:BoundField DataField="Item_name" HeaderText="Item Name" HeaderStyle-Width="250px" />
                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="Sub DCA Code" />
                                                                                                            <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                                                            <asp:BoundField DataField="Raised Qty" HeaderText="PO Qty" />
                                                                                                            <asp:BoundField DataField="Amount" HeaderText="Offer Amount" />
                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                            <asp:TemplateField HeaderText="Received Qty">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtqty" runat="server" Width="50px" onblur="Checkqty1();" onkeypress='IsNumeric1(event)'></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                    </asp:GridView>
                                                                                                </table>
                                                                                                <asp:GridView ID="grid" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="grid-content"
                                                                                                    HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                    RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                    DataKeyNames="id" OnRowDataBound="grid_RowDataBound" ShowFooter="true">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="id" Visible="false" />
                                                                                                        <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                        <asp:BoundField DataField="Item_name" HeaderText="Item Name" HeaderStyle-Width="250px" />
                                                                                                        <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                        <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                        <asp:BoundField DataField="Subdca_code" HeaderText="Sub DCA Code" HeaderStyle-Width="75px" />
                                                                                                        <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                        <asp:BoundField DataField="Issued Qty" HeaderText="Issued/Transfer Qty" />
                                                                                                        <asp:BoundField DataField="Amount" HeaderText="Depreciation value" />
                                                                                                        <asp:BoundField DataField="Item_status" HeaderText="Item Status" />
                                                                                                        <asp:TemplateField HeaderText="Received Qty">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtqty" runat="server" Width="50px" onblur="Checkqty();" onkeypress='IsNumeric2(event)'></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Damage Type">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:DropDownList ID="ddltype" runat="server">
                                                                                                                    <asp:ListItem>Type</asp:ListItem>
                                                                                                                    <asp:ListItem>Lost</asp:ListItem>
                                                                                                                    <asp:ListItem>Damaged</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="CentralGrid" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                    RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                    DataKeyNames="id" OnRowDataBound="grid_RowDataBound" ShowFooter="true">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="id" Visible="false" />
                                                                                                        <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                        <asp:BoundField DataField="Item_name" HeaderText="Item Name" HeaderStyle-Width="250px" />
                                                                                                        <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                        <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                        <asp:BoundField DataField="Subdca_code" HeaderText="Sub DCA Code" HeaderStyle-Width="75px" />
                                                                                                        <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                        <asp:BoundField DataField="Issued Qty" HeaderText="Issued/Transfer Qty" />
                                                                                                        <asp:BoundField DataField="Amount" HeaderText="Depreciation value" />
                                                                                                        <asp:TemplateField HeaderText="Received Qty">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtqty" runat="server" Width="50px" onblur="Checkqty2();" onkeypress='IsNumeric3(event)'></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Damage Type">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:DropDownList ID="ddltype" runat="server">
                                                                                                                    <asp:ListItem>Type</asp:ListItem>
                                                                                                                    <asp:ListItem>Lost</asp:ListItem>
                                                                                                                    <asp:ListItem>Damaged</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                </asp:GridView>
                                                                                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlindent1" runat="server"
                                                                                                    TargetControlID="btnModalPopUp" PopupControlID="Panel1" BackgroundCssClass="modalBackground1"
                                                                                                    DropShadow="false" />
                                                                                                <asp:Panel ID="Panel1" runat="server" Style="display: none;">
                                                                                                    <div id="fancybox-wrap" style="width: 165px; height: 80px; display: block; opacity: 1;
                                                                                                        top: 99.5px;">
                                                                                                        <div id="fancybox-outer">
                                                                                                            <div class="fancy-bg" id="fancy-bg-n">
                                                                                                            </div>
                                                                                                            <div class="fancy-bg" id="fancy-bg-ne">
                                                                                                            </div>
                                                                                                            <div class="fancy-bg" id="fancy-bg-e">
                                                                                                            </div>
                                                                                                            <div class="fancy-bg" id="fancy-bg-se">
                                                                                                            </div>
                                                                                                            <div class="fancy-bg" id="fancy-bg-s">
                                                                                                            </div>
                                                                                                            <div class="fancy-bg" id="fancy-bg-sw">
                                                                                                            </div>
                                                                                                            <div class="fancy-bg" id="fancy-bg-w">
                                                                                                            </div>
                                                                                                            <div class="fancy-bg" id="fancy-bg-nw">
                                                                                                            </div>
                                                                                                            <div id="fancybox-inner" style="top: 10px; left: 10px; width: 131px; height: 80px;
                                                                                                                overflow-x: visible; overflow-y: visible;">
                                                                                                                <table class="view" border="0" width="100%">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td valign="top" style="padding: 0px;">
                                                                                                                                <table class="errorbox" align="center">
                                                                                                                                    <tbody>
                                                                                                                                        <tr>
                                                                                                                                            <td style="padding: 4px;">
                                                                                                                                                <%--  <img src="images/warning.png">--%>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="padding: 2px;">
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr id="pono" runat="server">
                                                                                                                                            <td style="padding: 4px;">
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <span class="filter_item">
                                                                                                                                                    <asp:TextBox ID="txtpo" runat="server" CssClass="char" Width=""></asp:TextBox>
                                                                                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Enter Po No.."
                                                                                                                                                        WatermarkCssClass="watermarked" TargetControlID="txtpo" runat="server">
                                                                                                                                                    </cc1:TextBoxWatermarkExtender>
                                                                                                                                                </span>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="padding: 2px;">
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr id="refno" runat="server">
                                                                                                                                            <td style="padding: 4px;">
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <span class="filter_item">
                                                                                                                                                    <asp:TextBox ID="txtrefno" runat="server" CssClass="char" Width=""></asp:TextBox>
                                                                                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="Enter Ref No.."
                                                                                                                                                        WatermarkCssClass="watermarked" TargetControlID="txtrefno" runat="server">
                                                                                                                                                    </cc1:TextBoxWatermarkExtender>
                                                                                                                                                </span>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="padding: 2px;">
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="2" align="center" style="padding: 1px">
                                                                                                                                                <asp:Button ID="btnok" runat="server" Text="OK" CssClass="button" OnClick="btnok_Click" />
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </tbody>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                            <a id="fancybox-close"></a>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="pagerbar">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr align="left">
                                                                                            <td>
                                                                                                <asp:Label ID="lblvenname" Font-Bold="true" Font-Size="Small" runat="server"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="width: 100%" id="trdatedesc" runat="server">
                                                                                            <td class="label search_filters search_fields">
                                                                                                <table class="search_table">
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle" width="">
                                                                                                            Date
                                                                                                        </td>
                                                                                                        <td class="item item-selection" valign="middle" width="">
                                                                                                            Remarks
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle">
                                                                                                            <asp:TextBox ID="txtdate" runat="server" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                                                onkeypress="return false;" ToolTip="Date" CssClass="char" Width="100px"></asp:TextBox>
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                                                                                Animated="true" PopupButtonID="txtdate">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        <td class="item item-selection" valign="middle">
                                                                                                            <asp:TextBox ID="txtremarks" runat="server" Width="500px" TextMode="MultiLine" CssClass="char"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="btn" runat="server">
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnupdateprint" Width="100px" Height="20px" runat="server" Text="Confirm"
                                                                                                    CssClass="button" OnClientClick="javascript:return validation();" OnClick="btnupdateprint_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                <asp:Button ID="btncancelprint" Width="100px" Height="20px" runat="server" Text="Cancel"
                                                                                                    CssClass="button" />
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
