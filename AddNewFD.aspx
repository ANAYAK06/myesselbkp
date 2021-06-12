<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="AddNewFD.aspx.cs" Inherits="AddNewFD" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function checkDate(sender, args) {
            //debugger;
            var type = document.getElementById("<%=ddltype.ClientID %>");
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
            var type = document.getElementById("<%=ddltype.ClientID %>");
            if (type.selectedIndex == 2) {
                var str1 = document.getElementById("<%=txtfromdate.ClientID %>").value;
                var str2 = document.getElementById("<%=txtdate.ClientID %>").value;
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
                    alert("Invalid Reciept Date");
                    document.getElementById("<%=txtdate.ClientID %>").value = "";
                    return false;
                }
            }
            if (type.selectedIndex == 1) {
                checkpaymentdate();
            }
            if (type.selectedIndex == 2) {
                checkwithclosingdate();
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
        function Total() {
            var amt = document.getElementById("<%=txtamount.ClientID %>").value;
            if (amt == "") {
                document.getElementById('<%= txtamt.ClientID%>').value = 0;
            }
            else {
                document.getElementById('<%= txtamt.ClientID%>').value = amt;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function numericValidation(txtvalue) {
            //debugger;
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (!(document.getElementById(txtvalue.id).value)) {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
            else {
                var val = document.getElementById(txtvalue.id).value;
                if (charCode == 46 || (charCode > 31 && (charCode > 47 && charCode < 58))) {
                    var points = 0;
                    points = val.indexOf(".", points);
                    if (points >= 1 && charCode == 46) {
                        return false;
                    }
                    if (points >= 1) {
                        var lastdigits = val.substring(val.indexOf(".") + 1, val.length);
                        if (lastdigits.length >= 2) {
                            alert("Two decimal places only allowed");
                            return false;
                        }
                    }
                    return true;
                }
                else {
                    alert("Only Numerics allowed");
                    return false;
                }
            }
        }
        function checkfromtoDate(sender, args) {
            //debugger;
            var str1 = document.getElementById("<%=txtfromdate.ClientID %>").value;
            var str2 = document.getElementById("<%=txttodate.ClientID %>").value;
            if (str1 != "") {
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

                _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
                if (parseInt(_Diff) < 0) {
                    alert("Invalid date");
                    document.getElementById("<%=txttodate.ClientID %>").focus();
                    document.getElementById("<%=txttodate.ClientID %>").value = "";
                    return false;
                }
            }
            else {
                alert("Select From Date");
                document.getElementById("<%=txttodate.ClientID %>").focus();
                document.getElementById("<%=txttodate.ClientID %>").value = "";
                return false;
            }
        }

        function checkpaymentdate(sender, args) {
            //debugger;
            var str1 = document.getElementById("<%=txtfromdate.ClientID %>").value;
            var str2 = document.getElementById("<%=txtdate.ClientID %>").value;
            if (str1 != "") {
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

                _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
                if (parseInt(_Diff) > 0) {
                    alert("Invalid date");
                    document.getElementById("<%=txtdate.ClientID %>").focus();
                    document.getElementById("<%=txtdate.ClientID %>").value = "";
                    return false;
                }
            }
            else {
                alert("Select From Date");
                document.getElementById("<%=txtdate.ClientID %>").focus();
                document.getElementById("<%=txtdate.ClientID %>").value = "";
                return false;
            }
        }
        function closingdatecheck(sender, args) {
            //debugger;
            var type = document.getElementById("<%=ddltype.ClientID %>");
            if (type.selectedIndex == 2) {
                document.getElementById("<%=txtdate.ClientID %>").value = "";
                var str1 = document.getElementById("<%=txtclosingdate.ClientID %>").value;
                var str2 = document.getElementById("<%=txttodate.ClientID %>").value;
                if (str1 != "") {
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

                    _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
                    if (parseInt(_Diff) > 0) {
                        var response = confirm("Your are closing the FD before the maturity Date " + document.getElementById("<%=txtclosingdate.ClientID %>").value);
                        if (response) {
                            return true;
                        }
                        else {
                            document.getElementById("<%=txtclosingdate.ClientID %>").value = "";
                            return false;
                        }
                    }
                }
            }
        }
        function checkwithclosingdate(sender, args) {
            //debugger;
            var type = document.getElementById("<%=ddltype.ClientID %>");
            if (type.selectedIndex == 2) {
                var str1 = document.getElementById("<%=txtclosingdate.ClientID %>").value;
                var str2 = document.getElementById("<%=txtdate.ClientID %>").value;
                if (str1 != "") {
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

                    _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
                    if (parseInt(_Diff) < 0) {
                        alert("Invalid date");
                        document.getElementById("<%=txtdate.ClientID %>").focus();
                        document.getElementById("<%=txtdate.ClientID %>").value = "";
                        return false;
                    }
                }
                else {
                    alert("Select Closing Date");
                    document.getElementById("<%=txtdate.ClientID %>").focus();
                    document.getElementById("<%=txtdate.ClientID %>").value = "";
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
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table class="estbl" width="745px">
                            <tr>
                                <th align="center">
                                    Fixed Deposits(FD)
                                </th>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" style="font-weight: bold; width: 500px; font-size: small;">
                                    Fixed Deposit Type:
                                    <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small; font-weight: lighter"
                                        ToolTip="FD Type" AutoPostBack="true" RepeatDirection="Horizontal" runat="server"
                                        CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                        <asp:ListItem Value="Openclose">Open/Close</asp:ListItem>
                                        <asp:ListItem Value="Intrest">Interest</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <table class="estbl" width="745px" runat="server" id="tblopenclose">
                            <tr id="paytype" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Width="150px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="Transaction Type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddltype" runat="server" CssClass="esddown" Width="200px" ToolTip="FD Type"
                                                    OnSelectedIndexChanged="ddltype_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Open">Open FD</asp:ListItem>
                                                    <asp:ListItem Value="Closed">Close FD</asp:ListItem>
                                                    <asp:ListItem Value="Partially">Partially FD</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="vename" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="150px"
                                                    Text="FDR No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfdrno" runat="server" ToolTip="FD/RD No" Width="200px"></asp:TextBox>
                                                <asp:DropDownList ID="ddlfdrno" CssClass="esddown" ToolTip="FD/RD No" Width="200px"
                                                    runat="server" OnSelectedIndexChanged="ddlfdrno_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                <%--<asp:Label ID="Label1" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="Date"></asp:Label>--%>
                                            </td>
                                            <td style="width: 200px">
                                                <%-- <asp:TextBox ID="txtfddate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="FD Date"
                                                    Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfddate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtfddate" OnClientDateSelectionChanged="checkDate1">
                                                </cc1:CalendarExtender>--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblclosingdate" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="Closing Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtclosingdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="FD Date"
                                                    Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtclosingdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtclosingdate" OnClientDateSelectionChanged="closingdatecheck">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="From Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfromdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="From Date"
                                                    Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfromdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtfromdate">
                                                    <%--OnClientDateSelectionChanged="checkDate1"--%>
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="To Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttodate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="To Date"
                                                    Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txttodate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txttodate" OnClientDateSelectionChanged="checkfromtoDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                    Text="Rate Of Intrest"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtrateofintrest" runat="server" onkeypress="return numericValidation(this);"
                                                    ToolTip="Rate Of Intrest" Width="200px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                    Text="Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamount" runat="server" onkeypress="return numericValidation(this);"
                                                    ToolTip="Amount" onkeyup="Total();" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trbalamt" runat="server">
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label35" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                    Text="Balance Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtbalamt" runat="server" 
                                                    ToolTip="Balance Amount" onkeyup="Total();" Enabled="false" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trviewpaymentdetails" runat="server">
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="Table1">
                                        <tr>
                                            <th align="center" colspan="4">
                                                Bank Details
                                            </th>
                                        </tr>
                                        <tr id="Tr2" runat="server">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                                <asp:TextBox ID="txtfrombank1" Enabled="false" CssClass="estbox" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr3" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Mode Of Pay:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtpayment1" Enabled="false" CssClass="estbox" runat="server" Width="80px"></asp:TextBox>
                                                <asp:TextBox ID="txtdate1" Enabled="false" CssClass="estbox" runat="server" Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque1" CssClass="estbox" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc1" CssClass="estbox" runat="server" Enabled="false" Width="200px"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt1" CssClass="estbox" runat="server" Enabled="false" ToolTip="Amount"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trmaturity" runat="server">
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="Table3">
                                        <tr id="tr" runat="server">
                                            <td colspan="1">
                                                <asp:Label ID="Label15" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Maturity Amount:"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:TextBox ID="txtmaturity" CssClass="estbox" onkeyup="Total();" onkeypress="return numericValidation(this);"
                                                    ToolTip="Maturity" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label16" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Interest:"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:TextBox ID="txtintrest" CssClass="estbox" runat="server" onkeyup="Total();"
                                                    onkeypress="return numericValidation(this);" ToolTip="Intrest" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdeductionDetails" runat="server">
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="Table2">
                                        <tr>
                                            <th align="center" colspan="4">
                                                Deduction Details
                                            </th>
                                        </tr>
                                        <tr>
                                            <td class="label" width="1%">
                                                <asp:RadioButtonList ID="rbtnothercharges" CssClass="esrbtn" Style="font-size: x-small"
                                                    Width="170px" ToolTip="Other Charges Yes or No" RepeatDirection="Horizontal"
                                                    runat="server" OnSelectedIndexChanged="rbtnothercharges_SelectedIndexChanged"
                                                    AutoPostBack="true" ClientIDMode="AutoID">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td id="tdddlanyotherdcas" runat="server" valign="middle" colspan="2">
                                                <asp:TextBox ID="TextBox1" Width="400px" onKeyPress="javascript: return false;" ReadOnly="true"
                                                    onKeyDown="javascript: return false;" ToolTip="Other DCA" runat="server"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" DynamicServicePath=""
                                                    Enabled="True" ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1"
                                                    OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="Panel1" runat="server" Height="180px" Width="400px" BorderStyle="Solid"
                                                    BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" Style="display: none"
                                                    BackColor="LightGray">
                                                    <asp:CheckBoxList ID="ddlanyotherdcas" Width="400px" CellPadding="5" CellSpacing="5"
                                                        RepeatColumns="1" RepeatDirection="Horizontal" BackColor="LightGray" runat="server"
                                                        AutoPostBack="True" OnDataBound="ddlanyotherdcas_DataBound" OnSelectedIndexChanged="ddlanyotherdcas_SelectedIndexChanged">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                            <td id="tdaddanyother" runat="server" align="center">
                                                <asp:Button ID="txtaddothers" Text="Add" runat="server" CssClass="button" OnClientClick="javascript:return checkanyother();"
                                                    OnClick="Submit" />
                                            </td>
                                        </tr>
                                        <tr id="tranyothergrid" runat="server">
                                            <td colspan="4">
                                                <asp:GridView ID="gvanyother" runat="server" HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                    BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="dca_code"
                                                    ShowFooter="false" Width="790px" OnRowDataBound="OnRowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectother" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="dca_code" Visible="false" />
                                                        <asp:BoundField DataField="mapdca_code" HeaderText="Dca Code and Name" />
                                                        <asp:TemplateField HeaderText="Sub Dca">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlsdca" Width="200px" OnDataBound="ddlsdca_DataBound" ToolTip="SubDCA"
                                                                    runat="server">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtotheramount" runat="server" onpaste="return false;" onkeypress="return numericValidation(this);"
                                                                    onkeyup="calculatedeductions();Total();" Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="right">
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Deduction Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdedvalue" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" Text="0" ToolTip="Invoice Date"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Trledger" runat="server">
                                <td>
                                    <%-- <ledger:ledgercreation id="Ledger" runat="server" />--%>
                                    <table id="Table6" align="center" class="estbl" width="100%" runat="server">
                                        <tr id="tr5" runat="server">
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label30" CssClass="eslbl" runat="server" Text="Ledger Name"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:TextBox ID="txtledgername" CssClass="estbox" runat="server" onkeyup="javascript:capitalize(this.id, this.value);"
                                                    Width="200px" ToolTip="Ledger Name" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label31" CssClass="eslbl" runat="server" Text="Sub-Groups"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:DropDownList ID="ddlsubgroup" CssClass="esddown" Width="200px" runat="server"
                                                    ToolTip="Sub-Groups">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label32" CssClass="eslbl" runat="server" Text="Balance As On "></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:TextBox ID="txtledbaldate" CssClass="estbox" Width="200px" ToolTip="Balance As On"
                                                    onKeyDown="preventBackspace();" onpaste="return false;" onkeypress="return false;"
                                                    runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtledbaldate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtledbaldate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="right" style="width: 150px">
                                                <asp:Label ID="Label33" CssClass="eslbl" runat="server" Text="Opening Balance"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:TextBox ID="txtopeningbal" CssClass="estbox" onkeypress="return isNumber(event)"
                                                    runat="server" Width="200px" ToolTip="Opening Balance" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td colspan="4" align="center">
                                                <asp:RadioButtonList ID="rbtnpaymenttype" CssClass="esrbtn" Style="font-size: small"
                                                    ToolTip="Credit or Debit Type" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                    CellSpacing="0">
                                                    <asp:ListItem Value="0">Debit</asp:ListItem>
                                                    <asp:ListItem Value="1">Credit</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trpaymentdetails" runat="server">
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="paymentdetails">
                                        <tr>
                                            <th align="center" id="thpaymendetails" runat="server" colspan="4">
                                                Reciept Details
                                            </th>
                                        </tr>
                                        <tr id="bank" runat="server">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                                <asp:TextBox ID="txtfrombank" CssClass="estbox" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" Width="200px"
                                                    OnSelectedIndexChanged="ddlfrom_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr id="ModeofPay" runat="server">
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Mode Of Pay:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpayment" runat="server" OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged"
                                                    AutoPostBack="true" ToolTip="Mode Of Pay" CssClass="esddown" Width="70">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="paymentFD"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                    Width="80px"></asp:TextBox><span class="starSpan">*</span>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtdate" OnClientDateSelectionChanged="checkDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                                <asp:DropDownList ID="ddlcheque" runat="server" ToolTip="Cheque No" CssClass="esddown"
                                                    Width="100">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="border-top: none;">
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" Text="0" Enabled="false"
                                                    ToolTip="Amount" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table class="estbl" width="745px" runat="server" id="tblintrest">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="FDR No"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddlfdrnoInt" CssClass="esddown" ToolTip="Intrest FD/RD No"
                                        runat="server" OnSelectedIndexChanged="ddlfdrnoInt_SelectedIndexChanged" AutoPostBack="true" >
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label23" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                        Text="Intrest Date"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtintdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                        onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="FD Intrest Date"
                                        Width="200px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtintdate"
                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                        PopupButtonID="txtintdate" OnClientDateSelectionChanged="Intrestdatecheck">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label18" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                        Text="Intrest Amount"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:TextBox runat="server" ID="txtintrestamt" onkeypress="return numericValidation(this);"
                                        ToolTip="Intrest Amount" onkeyup="TotalIntrest();" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th align="center" colspan="4">
                                    Deduction
                                </th>
                            </tr>
                            <tr>
                                <td width="750px" colspan="4">
                                    <table class="innertab" width="100%" runat="server" id="Table4">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label19" runat="server" CssClass="eslbl" Width="50px" Font-Size="XX-Small"
                                                    Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="DCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label21" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="SDCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label22" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="Amount"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="750px" colspan="4">
                                    <table class="innertab" width="100%" runat="server" id="Table5">
                                        <tr align="center" style="background-color: white">
                                            <td>
                                                <asp:DropDownList ID="ddlcccodeded" runat="server" ToolTip="Intrest Deduction Cost Center"
                                                    Width="150px" CssClass="esddown" OnSelectedIndexChanged="ddlcccodeded_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldcaded" runat="server" ToolTip="Intrest Deduction DCA Code"
                                                    Width="150px" CssClass="esddown" OnSelectedIndexChanged="ddldcaded_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsdcacodeded" runat="server" ToolTip="Intrest Deduction SDCA Code"
                                                    Width="150px" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdedamount" CssClass="estbox" runat="server" ToolTip="Intrest Deduction Amount"
                                                    Width="100px" onkeypress="return numericValidation(this);" onkeyup="TotalIntrest();"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <th align="center" colspan="4">
                                    Payment Details
                                </th>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td colspan="4" align="center">
                                    <asp:Label ID="Label24" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                    <asp:DropDownList ID="ddldedfrombank" runat="server" ToolTip="Bank" CssClass="esddown"
                                        Width="200px">
                                    </asp:DropDownList>
                                    <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddldedfrombank"
                                        ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                        PromptText="Select">
                                    </cc1:CascadingDropDown>
                                </td>
                            </tr>
                            <tr id="Tr4" runat="server">
                                <td>
                                    <asp:Label ID="Label25" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Mode Of Pay:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlmodeofpay" runat="server" ToolTip="Mode Of Pay" CssClass="esddown"
                                        Width="70">
                                    </asp:DropDownList>
                                    <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlmodeofpay"
                                        ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="paymentrefund"
                                        PromptText="Select">
                                    </cc1:CascadingDropDown>
                                    <asp:Label ID="Label26" runat="server" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtbankdate" CssClass="estbox" runat="server" ToolTip="Payment Date"
                                        Width="80px"></asp:TextBox><span class="starSpan">*</span>
                                    <cc1:CalendarExtender ID="CalendarExtender20" runat="server" TargetControlID="txtbankdate"
                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                        PopupButtonID="txtbankdate" OnClientDateSelectionChanged="checkbankDateded">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label27" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="No:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtno" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                        class="starSpan">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label28" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Remarks:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdeddescription" CssClass="estbox" runat="server" ToolTip="Description"
                                        Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label29" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Amount:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttotalamt" CssClass="estbox" runat="server" Enabled="false" ToolTip="Amount"
                                        Width="200px" onkeydown="return isValidKey(event)"></asp:TextBox>
                                    <asp:HiddenField ID="hf1" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table id="tblbtn" runat="server" rules="estbl" width="660px">
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validate()" OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnCancel" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Reset" OnClick="btnCancel_Click" />
                                    <asp:HiddenField ID="hfdedtotal" Value="0" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function TotalIntrest() {
            //debugger;
            var invoiceval = 0;
            var intrest = document.getElementById("<%=txtintrestamt.ClientID %>").value;
            var deduction = document.getElementById("<%=txtdedamount.ClientID %>").value;

            if (intrest == "") {
                intrest = 0;
            }
            if (deduction == "") {
                deduction = 0;
            }
            invoiceval = eval((parseFloat(intrest) - parseFloat(deduction)));
            var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txttotalamt.ClientID%>').value = roundinvvalue;
        }
        function calculatedeductions() {
            //debugger;
            grd = document.getElementById("<%=gvanyother.ClientID %>");
            var hfdedtotal = document.getElementById("<%=hfdedtotal.ClientID %>").value;
            var totalother = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[3].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                            totalother += Number(grd.rows(rowCount).cells(3).children[0].value);
                        }
                    }
                    else {
                        totalother += Number(grd.rows(rowCount).cells(3).children[0].value);
                    }
                }
                hfdedtotal = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtdedvalue.ClientID %>").value = hfdedtotal;
            }
            else {
                hfdedtotal = 0;
                document.getElementById("<%=txtdedvalue.ClientID %>").value = hfdedtotal;
            }
        }
        function Total() {
            var invoiceval = 0;
             
            var type = document.getElementById("<%=ddltype.ClientID %>");
            if (type.selectedIndex == 2 || type.selectedIndex == 3) {
                var maturity = document.getElementById("<%=txtmaturity.ClientID %>").value;
                var balamt = document.getElementById("<%=txtbalamt.ClientID %>").value;
                var intrest = document.getElementById("<%=txtintrest.ClientID %>").value;
                var ded = document.getElementById("<%=txtdedvalue.ClientID %>").value;

                if (maturity == "") {
                    maturity = 0;
                }
                if (intrest == "") {
                    intrest = 0;
                }
                if (ded == "") {
                    ded = 0;
                }
                if (balamt == "") {
                    balamt = 0;
                }
                if ((parseFloat(maturity) > parseFloat(balamt))) {
                    alert("Invalid Maturity Amount")
                    document.getElementById("<%=txtmaturity.ClientID %>").value = "";
                    return false;
                }
                invoiceval = eval((parseFloat(maturity) + parseFloat(intrest) - parseFloat(ded)));
                var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById('<%= txtamt.ClientID%>').value = roundinvvalue;
            }
            else if (type.selectedIndex == 1) {
                var Amount = document.getElementById("<%=txtamount.ClientID %>").value;
                if (Amount == "") {
                    Amount = 0;
                }

                invoiceval = eval(parseFloat(Amount));
                var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById('<%= txtamt.ClientID%>').value = roundinvvalue;
            }
        }
        function validate() {
            debugger;
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                var type = document.getElementById("<%=ddltype.ClientID %>");
                var objs = new Array("<%=ddltype.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                if (type.selectedIndex == 1) {
                    var objs = new Array("<%=txtfdrno.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }
                else if (type.selectedIndex == 2 || type.selectedIndex == 3) {
                    var objs = new Array("<%=ddlfdrno.ClientID %>", "<%=txtmaturity.ClientID %>", "<%=txtintrest.ClientID %>", "<%=txtclosingdate.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                    var rbs = document.getElementById("<%=rbtnothercharges.ClientID%>");
                    var radio = rbs.getElementsByTagName("input");
                    var label = rbs.getElementsByTagName("label");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[0].checked == false && radio[1].checked == false) {
                            alert("Please select deduction yes or no");
                            return false
                        }
                        if (radio[i].checked) {
                            var value = radio[i].value;
                            if (value == "Yes") {
                                var GridView1 = document.getElementById("<%=gvanyother.ClientID %>");
                                if (GridView1 != null) {
                                    for (var rowCount = 1; rowCount < GridView1.rows.length; rowCount++) {
                                        if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                                            window.alert("Please verify");
                                            return false;
                                        }
                                        else if (GridView1.rows(rowCount).cells(2).children[0].value == "Select SDCA") {
                                            window.alert("Select SubDca");
                                            GridView1.rows(rowCount).cells(2).children[0].focus();
                                            return false;
                                        }
                                        else if (GridView1.rows(rowCount).cells(3).children[0].value == "") {
                                            window.alert("Enter Amount");
                                            GridView1.rows(rowCount).cells(3).children[0].focus();
                                            return false;
                                        }
                                    }
                                }
                                else {
                                    window.alert("Please Select Deduction DCAs");
                                    return false;
                                }
                            }
                        }
                    }
                }

                var objs = new Array("<%=txtfdrno.ClientID %>", "<%=txtfromdate.ClientID %>", "<%=txttodate.ClientID %>", "<%=txtrateofintrest.ClientID %>",
                                         "<%=txtamount.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                if (type.selectedIndex == 1) {
                    var objs = new Array("<%=txtledgername.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=txtledbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                    if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>")) {
                        return false;
                    }
                }

                var objs = new Array("<%=ddldedfrombank.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                var bank = document.getElementById("<%=ddlfrom.ClientID %>").value;               
                var response = confirm("Do you want to Continue with the " + bank);
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                if (response) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                var objs = new Array("<%=ddlfdrnoInt.ClientID %>", "<%=txtintdate.ClientID %>", "<%=txtintrestamt.ClientID %>", "<%=ddlcccodeded.ClientID %>", "<%=ddldcaded.ClientID %>", "<%=ddlsdcacodeded.ClientID %>", "<%=txtdedamount.ClientID %>", "<%=ddldedfrombank.ClientID %>", "<%=ddlmodeofpay.ClientID %>", "<%=txtbankdate.ClientID %>", "<%=txtno.ClientID %>", "<%=txtdeddescription.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                var ActuallAmt = document.getElementById("<%=txttotalamt.ClientID %>").value;
                if (parseFloat(ActuallAmt) < 1) {
                    alert("Final Amount Can not in Negative Value");
                    return false;
                }
                var bank = document.getElementById("<%=ddldedfrombank.ClientID %>").value;
                var response = confirm("Do you want to Continue with the " + bank);
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                if (response) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        function checkanyother() {
            var txt1 = document.getElementById("<%=TextBox1.ClientID %>").value;
            if (txt1 == "") {
                alert("Please Check DCA Codes");
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function Intrestdatecheck(sender, args) {
            //debugger;
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
            var str1 = document.getElementById("<%=txtintdate.ClientID %>").value;
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
                document.getElementById("<%=txtintdate.ClientID %>").value = "";
                return false;
            }
        }
        function checkbankDateded(sender, args) {
            //debugger;
            if (document.getElementById("<%=txtintdate.ClientID %>").value != "") {
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
                var str1 = document.getElementById("<%=txtbankdate.ClientID %>").value;
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
                    document.getElementById("<%=txtbankdate.ClientID %>").value = "";
                    return false;
                }
                if (document.getElementById("<%=txtintdate.ClientID %>").value != "" && document.getElementById("<%=txtbankdate.ClientID %>").value != "") {
                    //debugger;
                    var str1 = document.getElementById("<%=txtintdate.ClientID %>").value;
                    var str2 = document.getElementById("<%=txtbankdate.ClientID %>").value;
                    if (str1 != "") {
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

                        _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
                        if (parseInt(_Diff) < 0) {
                            alert("Invalid date");
                            document.getElementById("<%=txtbankdate.ClientID %>").focus();
                            document.getElementById("<%=txtbankdate.ClientID %>").value = "";
                            return false;
                        }
                    }
                }

            }

            else {
                alert("Please Select Intrest date before bank payment date");
                document.getElementById("<%=txtbankdate.ClientID %>").value = "";
                return false;
            }
        }
    </script>
    <script language="javascript">
        //debugger;
        function capitalize(textboxid, str) {
            // string with alteast one character
            if (str && str.length >= 1) {
                var firstChar = str.charAt(0);
                var remainingStr = str.slice(1);
                str = firstChar.toUpperCase() + remainingStr;
            }
            document.getElementById(textboxid).value = str;
        }
        function isNumber(evt) {
            //myFunction();
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
    </script>
</asp:Content>
