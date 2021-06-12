<%@ Page Title="ClientAdvanceReciept" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="ClientAdvanceReceipt.aspx.cs" Inherits="ClientAdvanceReceipt" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
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
    </script>
    <script language="javascript" type="text/javascript">
        function Total() {
            var invoiceval = 0;
            var Netval = 0;
            var basic = document.getElementById("<%=txtbasic.ClientID %>").value;
            var taxes = document.getElementById("<%=lbltaxes.ClientID %>").value;
            var cess = document.getElementById("<%=txtcess.ClientID %>").value;
            var deduction = document.getElementById("<%=txtdeduction.ClientID %>").value;
            if (basic == "") {
                basic = 0;
            }
            if (taxes == "") {
                tax = 0;
            }
            if (cess == "") {
                cess = 0;
            }          
            if (deduction == "") {
                deduction = 0;
            }          
            invoiceval = eval((parseFloat(basic) + parseFloat(taxes) + parseFloat(cess) - parseFloat(deduction)));
            //Netval = eval((parseFloat(invoiceval) - parseFloat(Retention) - parseFloat(Hold)));
            //var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);
            //var roundNetVal = Math.round(Netval * Math.pow(10, 2)) / Math.pow(10, 2);
           <%-- document.getElementById('<%= txtinvvalue.ClientID%>').value = roundinvvalue;
            document.getElementById('<%= txtnetamt.ClientID%>').value = roundNetVal;--%>
        }

    </script>
    <script type="text/javascript">
        function AddTaxes() {
            //debugger;
            var date = document.getElementById("<%=txtpodate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtpodate.ClientID %>");
            var trAddTaxes = document.getElementById("<%=traddtaxes.ClientID %>");
            if (date == "") {
                alert("Please Select Invoice date before Add Taxes");
                datectrl.focus();
                return false;
            }
            else {
                document.getElementById("<%=txtpodate.ClientID %>").disabled = true;
                document.getElementById("<%=traddtaxes.ClientID %>").style.display = "block";
                document.getElementById("<%=btnaddtaxes.ClientID %>").click();
                return true;
            }

        }
        function RemoveTaxes() {
            document.getElementById("<%=btnremovetaxes.ClientID %>").click();
        }
        function calculatetaxes() {
            //debugger;
            grd = document.getElementById("<%=gvtaxes.ClientID %>");
            var hftaxtotal = document.getElementById("<%=hftaxtotal.ClientID %>").value;
            var totalother = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[6].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(6).children[0].value)) {
                            totalother += Number(grd.rows(rowCount).cells(6).children[0].value);
                        }
                    }
                    else {
                        totalother += Number(grd.rows(rowCount).cells(6).children[0].value);
                    }
                }
                hftaxtotal = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=lbltaxes.ClientID %>").value = hftaxtotal;
            }
            else {
                hftaxtotal = 0;
                document.getElementById("<%=lbltaxes.ClientID %>").value = hftaxtotal;
            }
        }
        function checktaxdca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvtaxes.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            for (i = 1; i < grid.rows.length - 1; i++) {
                var dropdownoldValue = grid.rows[i].cells[3].childNodes[0].value;
                if (rowIndex != i) {
                    if (currentDropDownValue == grid.rows(i).cells(3).children[0].value) {
                        window.alert("Already Dca Selected");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                    else if (currentDropDownValue == "DCA-44") {
                        if (grid.rows(i).cells(3).children[0].value.substring(0, 6) == "DCA-SR" && currentDropDownValue == "DCA-44") {
                            window.alert("Invalid");
                            document.getElementById(val.id).value = "Select";
                            return false;
                        }
                    }
                    else if (currentDropDownValue == "DCA-SRTX") {
                        if (grid.rows(i).cells(3).children[0].value.substring(0, 6) == "DCA-44" && currentDropDownValue == "DCA-SRTX") {
                            window.alert("Invalid");
                            document.getElementById(val.id).value = "Select";
                            return false;
                        }
                    }
                    else if (currentDropDownValue != "DCA-SRTX" && currentDropDownValue != "DCA-44") {
                        if (currentDropDownValue.substring(0, 7) == grid.rows(i).cells(3).children[0].value.substring(0, 7)) {
                            window.alert("Invalid");
                            document.getElementById(val.id).value = "Select";
                            return false;
                        }
                    }
                }
            }
        }
        function verifytaxdca() {
            //debugger;
            var grid = document.getElementById("<%= gvtaxes.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (grid.rows(i).cells(2).children[0].value == "Select") {
                    window.alert("Select Type");
                    return false;
                }
                else if (grid.rows(i).cells(3).children[0].value == "Select") {
                    window.alert("Select DCA");
                    return false;
                }
                else if (grid.rows(i).cells(4).children[0].value == "Select") {
                    window.alert("Select SDCA");
                    return false;
                }
                else if (grid.rows(i).cells(5).children[0].value == "Select") {
                    window.alert("Select Tax Nos");
                    return false;
                }
                else if (grid.rows(i).cells(6).children[0].value == "") {
                    window.alert("Enter Amount");
                    return false;
                }

            }
        }
        
     
    </script>
    <script type="text/javascript">
        function Addcess() {
            //debugger;
            var date = document.getElementById("<%=txtpodate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtpodate.ClientID %>");
            var traddcess = document.getElementById("<%=traddcess.ClientID %>");
            if (date == "") {
                alert("Please Select Invoice date before Add Cess Taxes");
                datectrl.focus();
                return false;
            }
            else {
                document.getElementById("<%=txtpodate.ClientID %>").disabled = true;
                document.getElementById("<%=traddcess.ClientID %>").style.display = "block";
                document.getElementById("<%=btnaddcesss.ClientID %>").click();
                return true;
            }

        }
        function Removecess() {
            document.getElementById("<%=btnremovecesss.ClientID %>").click();
        }
        function calculatecess() {
            grd = document.getElementById("<%=gvcess.ClientID %>");
            var hfcesstotal = document.getElementById("<%=hfcesstotal.ClientID %>").value;
            var totalcess = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[6].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(6).children[0].value)) {
                            totalcess += Number(grd.rows(rowCount).cells(6).children[0].value);
                        }
                    }
                    else {
                        totalcess += Number(grd.rows(rowCount).cells(6).children[0].value);
                    }
                }
                hfcesstotal = Math.round(totalcess * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtcess.ClientID %>").value = hfcesstotal;
            }
            else {
                hfcesstotal = 0;
                document.getElementById("<%=txtcess.ClientID %>").value = hfcesstotal;
            }
        }
        function checkcesssdca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvcess.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (rowIndex != i) {
                    if (currentDropDownValue == grid.rows(i).cells(4).children[0].value) {
                        window.alert("Invalid SDCA");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                }

            }
        }
        function checkcessdca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvcess.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (rowIndex != i) {
                    if (currentDropDownValue == "DCA-CESS-CR" && grid.rows(i).cells(3).children[0].value == "DCA-CESS-NON-CR") {
                        window.alert("Invalid DCA");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                    else if (currentDropDownValue == "DCA-CESS-NON-CR" && grid.rows(i).cells(3).children[0].value == "DCA-CESS-CR") {
                        window.alert("Invalid DCA");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                }
            }
        }
        function verifycessdca() {
            //debugger;
            var grid = document.getElementById("<%= gvcess.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (grid.rows(i).cells(2).children[0].value == "Select") {
                    window.alert("Select Type");
                    return false;
                }
                else if (grid.rows(i).cells(3).children[0].value == "Select") {
                    window.alert("Select DCA");
                    return false;
                }
                else if (grid.rows(i).cells(4).children[0].value == "Select") {
                    window.alert("Select SDCA");
                    return false;
                }
                else if (grid.rows(i).cells(5).children[0].value == "Select") {
                    window.alert("Select Tax Nos");
                    return false;
                }
                else if (grid.rows(i).cells(6).children[0].value == "") {
                    window.alert("Enter Amount");
                    return false;
                }

            }
        }
       
    </script>
    <script type="text/javascript" language="javascript">        //FOR Deduction Starts
        function getdeductionIndex(r) {
            //debugger;
            var rbs = document.getElementById("<%=rbtndeductioncharges.ClientID%>");
            var date = document.getElementById("<%=txtpodate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtpodate.ClientID %>");
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var radio = rbs.getElementsByTagName("input");
            var label = rbs.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    var value = radio[i].value;
                    if (value == "Yes") {
                        if (date == "") {
                            alert("Please Select Invoice date before Add Deduction DCAs");
                            document.getElementById("<%=txtpodate.ClientID %>").disabled = false;
                            datectrl.focus();
                            return false;
                        }
                        else {
                            if (date != "")
                                document.getElementById("<%=txtpodate.ClientID %>").disabled = true;
                            else
                                document.getElementById("<%=txtpodate.ClientID %>").disabled = false;
                            document.getElementById("<%=trdeductiongrid.ClientID %>").style.display = "block";
                            return true;
                        }
                    }
                    else if (value == "No") {
                        if (date != "")
                            document.getElementById("<%=txtpodate.ClientID %>").disabled = true;
                        else
                            document.getElementById("<%=txtpodate.ClientID %>").disabled = false;
                        document.getElementById("<%=trdeductiongrid.ClientID %>").style.display = "none";
                        hfdeduction = 0;
                        return true;
                    }
                    else {
                        if (date != "")
                            document.getElementById("<%=txtpodate.ClientID %>").disabled = true;
                        else
                            document.getElementById("<%=txtpodate.ClientID %>").disabled = false;
                        document.getElementById("<%=trdeductiongrid.ClientID %>").style.display = "none";
                        hfdeduction = 0;
                        return false;
                    }
                }
            }

        }

        function calculatededuction() {
            grd = document.getElementById("<%=gvdeduction.ClientID %>");
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var totaldeduction = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[6].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(6).children[0].value)) {
                            totaldeduction += Number(grd.rows(rowCount).cells(6).children[0].value);
                        }
                    }
                    else {
                        totaldeduction += Number(grd.rows(rowCount).cells(6).children[0].value);
                    }
                }
                hfdeduction = Math.round(totaldeduction * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtdeduction.ClientID %>").value = hfdeduction;
            }
            else {
                hfdeduction = 0;
                document.getElementById("<%=txtdeduction.ClientID %>").value = hfdeduction;
            }
        }
        function checkdeductiondca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvdeduction.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            var currentDropDownValueindex = document.getElementById(val.id).value;
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (rowIndex != i) {
                    if (currentDropDownValue == grid.rows(i).cells(2).children[0].value) {
                        window.alert("Already Dca Selected");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                }
            }
        }
        function verifydeddca() {
            //debugger;
            var grid = document.getElementById("<%= gvdeduction.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (grid.rows(i).cells(2).children[0].value == "Select") {
                    window.alert("Select Yes/No");
                    return false;
                }
                else if (grid.rows(i).cells(3).children[0].value == "Select") {
                    window.alert("Select Cost Center");
                    return false;
                }
                else if (grid.rows(i).cells(4).children[0].value == "Select") {
                    window.alert("Select Dca");
                    return false;
                }
                else if (grid.rows(i).cells(5).children[0].value == "Select") {
                    window.alert("Select Sub-Dca");
                    return false;
                }

            }
        }
    </script>
    <script type="text/javascript">
        function checkDatepo(sender, args) {
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
            var str1 = document.getElementById("<%=txtpodate.ClientID %>").value;
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
                document.getElementById("<%=txtpodate.ClientID %>").value = "";
                return false;
            }
        }
        function checkDate(sender, args) {
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
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%-- <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
                        <table id="tbldetails" class="estbl" width="750px" runat="server">
                            <tr>
                                <th align="center" colspan="8" class="eslbl">
                                    Client Advance Reciept
                                </th>
                            </tr>
                            <tr id="trdebit" runat="server">
                                <td style="width: 80px">
                                    Client ID:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlclientid" onchange="SetDynamicKey('dp3',this.value);" runat="server"
                                        ToolTip="ClientID" Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 80px">
                                    Subclient ID:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlsubclientid" onchange="SetDynamicKey('dp9',this.value);"
                                        runat="server" ToolTip="ClientID" Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <br />
                                </td>
                                <td>
                                    CC-Code:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="150px"
                                        CssClass="esddown" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="lblcccode" class="ajaxspan" runat="server"></asp:Label>
                                </td>
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label9" class="ajaxspan" runat="server"></asp:Label>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr id="Invoice" runat="server">
                                <td colspan="6">
                                    <table id="Table1" class="" runat="server" style="border-style: hidden;" width="100%">
                                        <tr id="trpo" runat="server" style="height: 40px;" visible="false">
                                            <td>
                                                <table class="estbl" width="100%">
                                                    <tr>
                                                        <td style="width: 50px">
                                                            PO No:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlpo" runat="server" Width="120px" CssClass="esddown" OnSelectedIndexChanged="ddlpo_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 150px">
                                                            Advance Requested Date:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpodate" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                onkeypress="return false;" runat="server" ToolTip="Advance Requested Date" Width="120px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtpodate"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                OnClientDateSelectionChanged="checkDatepo" PopupButtonID="txtpodate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="width: 50px">
                                                            RA No:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtra" CssClass="estbox" runat="server" ToolTip="RA No" Width="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 80px">
                                                            Basic Value:
                                                        </td>
                                                        <td colspan="5">
                                                            <asp:TextBox ID="txtbasic" CssClass="estbox" runat="server" ToolTip="Basic Value"
                                                                onkeypress="return numericValidation(this);" onkeyup="Total();" Width="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trtaxes" runat="server" visible="false">
                                <td colspan="6">
                                    <table id="tblgridtaxes" runat="server" class="estbl" align="left" width="750px">
                                        <tr align="left">
                                            <td colspan="6" align="left">
                                                <button name="button" onclick="AddTaxes()" id="btnaddtax" runat="server" type="button">
                                                    Add Tax</button>
                                                <button name="button" id="btnremovetax" runat="server" onclick="RemoveTaxes()" type="button">
                                                    Remove All Taxes</button>
                                                <asp:Button ID="btnremovetaxes" runat="server" Style="display: none;" Text="" OnClick="btnremovetaxes_Click" />
                                                <asp:Button ID="btnaddtaxes" runat="server" Style="display: none;" Text="" OnClick="btnAddTax_Click" />
                                                <%-- --%>
                                            </td>
                                        </tr>
                                        <tr id="traddtaxes" runat="server" style="display: none">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvtaxes" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true"
                                                    OnRowDeleting="gvtaxes_RowDeleting" OnRowDataBound="gvtaxes_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelecttaxes" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltype" Font-Size="7" Width="90px" CssClass="filter_item"
                                                                    ToolTip="Type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltaxdca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                    ToolTip="DCA" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddltaxdca_SelectedIndexChanged"
                                                                    onchange="checktaxdca(this)">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SDCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltaxsdca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                    ToolTip="SDCA" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tax No">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltaxnos" Font-Size="7" Width="80px" CssClass="filter_item"
                                                                    ToolTip="Tax Nos" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txttaxamount" runat="server" Font-Size="7" CssClass="filter_item"
                                                                    onkeypress="return numericValidation(this);" onkeyup="calculatetaxes(); Total();"
                                                                    Width="75px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAdd" runat="server" CssClass="esbtn" OnClick="btnAdd_Click" OnClientClick="javascript:return verifytaxdca();"
                                                                    Text="Add More Taxes" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" DeleteText="Remove" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="right">
                                            <td colspan="6" style="font-weight: normal">
                                                <asp:HiddenField ID="hftaxtotal" runat="server" />
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Tax Amount:"></asp:Label>
                                                <asp:TextBox ID="lbltaxes" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trcess" runat="server" visible="false">
                                <td colspan="6">
                                    <table id="tblgridcess" runat="server" class="estbl" align="left" width="750px">
                                        <tr align="left">
                                            <td colspan="6" align="left">
                                                <button name="button" onclick="Addcess()" id="btnaddcess" runat="server" type="button">
                                                    Add Cess</button>
                                                <button name="button" id="btnremovecess" runat="server" onclick="Removecess()" type="button">
                                                    Remove All Cess Taxes</button>
                                                <asp:Button ID="btnremovecesss" runat="server" Style="display: none;" Text="" OnClick="btnremovecesss_Click" />
                                                <asp:Button ID="btnaddcesss" runat="server" Style="display: none;" Text="" OnClick="btnaddcesss_Click" />
                                            </td>
                                        </tr>
                                        <tr id="traddcess" runat="server" style="display: none">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvcess" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true"
                                                    OnRowDeleting="gvcess_RowDeleting" OnRowDataBound="gvcess_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectcess" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltypecess" Font-Size="7" Width="90px" CssClass="filter_item"
                                                                    ToolTip="Cess Type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltypecess_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlcessdca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                    ToolTip="Cess DCA" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcessdca_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SDCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlcesssdca" Font-Size="7" Width="130px" CssClass="filter_item"
                                                                    ToolTip="Cess SDCA" runat="server" onchange="checkcesssdca(this)">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tax No">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlcessnos" Font-Size="7" Width="100px" CssClass="filter_item"
                                                                    ToolTip="Cess Nos" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtcessamount" runat="server" CssClass="filter_item" onkeypress="return numericValidation(this);"
                                                                    onkeyup="calculatecess(); Total();" Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAddcess" runat="server" CssClass="esbtn" OnClientClick="javascript:return verifycessdca();"
                                                                    Text="Add More Cess" OnClick="btnaddcess_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" DeleteText="Remove" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="right">
                                            <td colspan="6" style="font-weight: normal">
                                                <asp:HiddenField ID="hfcesstotal" runat="server" />
                                                <asp:Label ID="lbl" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Cess Amount:"></asp:Label>
                                                <asp:TextBox ID="txtcess" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdeduction" runat="server" visible="false">
                                <td colspan="6">
                                    <table id="Table2" runat="server" class="estbl" align="left" width="750px">
                                        <tr align="left">
                                            <td style="width: 750px" align="center">
                                                <asp:Label ID="Label8" runat="server" Font-Size="10px" Width="150px" CssClass="eslbl"
                                                    Text="Deduction DCA Selection"></asp:Label>
                                                <asp:RadioButtonList ID="rbtndeductioncharges" runat="server" Width="200px" AutoPostBack="true"
                                                    CssClass="eslbl" ClientIDMode="AutoID" RepeatDirection="Horizontal" Style="font-size: x-small"
                                                    ToolTip="Deduction Charges Yes or No" OnSelectedIndexChanged="rbtndeductioncharges_SelectedIndexChanged"
                                                    onclick="javascript:return getdeductionIndex(this)">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trdeductiongrid" runat="server" style="display: none">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvdeduction" HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                    BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames=""
                                                    GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true" OnRowDeleting="gvdeduction_RowDeleting"
                                                    OnRowDataBound="gvdeduction_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkdeduction" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is dedcuted from other CC" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlothercc" Font-Size="7" Width="50px" CssClass="filter_item"
                                                                    runat="server" OnSelectedIndexChanged="ddlothercc_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CC Code" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlccode" Font-Size="7" Width="150px" CssClass="filter_item"
                                                                    runat="server" OnSelectedIndexChanged="ddlccode_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA Code" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddldeductiondca" Font-Size="7" Width="150px" CssClass="filter_item"
                                                                    ToolTip="Other DCA" runat="server" OnSelectedIndexChanged="ddldeductiondca_SelectedIndexChanged"
                                                                    AutoPostBack="true" onchange="checkdeductiondca(this)">
                                                                    <%----%>
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUBDCA Code" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddldeductionsdca" Font-Size="7" Width="150px" CssClass="filter_item"
                                                                    ToolTip="Other SDCA" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtdeductionamount" runat="server" CssClass="filter_item" Width="75px"
                                                                    onkeypress="return numericValidation(this);" onkeyup="calculatededuction(); Total();"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnadddeduction" runat="server" Text="Add More Deduction Dca" OnClientClick="javascript:return verifydeddca();"
                                                                    OnClick="btnadddeduction_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" DeleteText="Remove" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="right">
                                            <td colspan="6" style="font-weight: normal">
                                                <asp:HiddenField ID="hfdeduction" runat="server" />
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Deduction Amount:"></asp:Label>
                                                <asp:TextBox ID="txtdeduction" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="paymentdetails" visible="false">
                                <td colspan="6">
                                    <table align="left" class="estbl" width="100%">
                                        <tr>
                                            <th align="center" colspan="4">
                                                Payment Details
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblfrombank" runat="server" Text="Bank"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" Width="140px">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td>
                                                Date
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" ToolTip="Credited Date"
                                                    Width="140px"></asp:TextBox><span class="starSpan">*</span>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                    Animated="true" PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                                <%--<img onclick="scwShow(document.getElementById('<%=txtdate.ClientID %>'),this);" alt=""
                                                    src="images/cal.gif" style="width: 15px; height: 15px;" id="Img2" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblmode" runat="server" Text="No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="140px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                Remarks
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Comments" Width="200px"
                                                    TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Amount
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" onkeyup="Amounvalidation(this.value);"
                                                    ToolTip="Amount" Width="140px"></asp:TextBox><span class="starSpan">*</span>
                                                <asp:HiddenField ID="hf1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="btn" runat="server" visible="false">
                                <td align="center" colspan="6">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validate();" OnClick="btnsubmit_Click" />
                                    <%--OnClientClick="javascript:return validate();" OnClick="btnsubmit_Click" --%>
                                    <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Reset" />
                                    <%--OnClick="btnCancel1_Click"--%>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var anytaxes = 0;
        var cesstaxes = 0;

        function validate() {
            var objs = new Array("<%=txtpodate.ClientID %>", "<%=txtra.ClientID %>", "<%=txtbasic.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            //debugger;
            GridView1 = document.getElementById("<%=gvtaxes.ClientID %>");
            if (GridView1 == null) {
                if (document.getElementById("<%=btnaddtax.ClientID %>").style.display == "") {
                    if (anytaxes == 0) {
                        var response = confirm("Do you want to add Taxes");
                        if (response) {
                            anytaxes = 0;
                            return false;
                        }
                        else {
                            anytaxes = 1;
                            return false;
                        }
                    }
                }
            }
            // debugger;
            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(1).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(2).children[0].value == "Select") {
                        window.alert("Select Type");
                        GridView1.rows(rowCount).cells(2).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(3).children[0].value == "Select") {
                        window.alert("Select Dca");
                        GridView1.rows(rowCount).cells(3).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(4).children[0].value == "Select") {
                        window.alert("Select Sub Dca");
                        GridView1.rows(rowCount).cells(4).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(5).children[0].value == "Select") {
                        window.alert("Select Tax No");
                        GridView1.rows(rowCount).cells(5).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(6).children[0].value == "") {
                        window.alert("Enter Amount");
                        GridView1.rows(rowCount).cells(6).children[0].focus();
                        return false;
                    }
                }
            }

            GridView2 = document.getElementById("<%=gvcess.ClientID %>");
            if (GridView2 == null) {
                if (document.getElementById("<%=btnaddcess.ClientID %>").style.display == "") {
                    if (cesstaxes == 0) {
                        var response1 = confirm("Do you want to add Cess");
                        if (response1) {
                            cesstaxes = 0;
                            return false;
                        }
                        else {
                            cesstaxes = 1;
                            return false;
                        }
                    }

                }
            }
            if (GridView2 != null) {
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    if (GridView2.rows(rowCount).cells(1).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(2).children[0].value == "Select") {
                        window.alert("Select Type");
                        GridView2.rows(rowCount).cells(2).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(3).children[0].value == "Select") {
                        window.alert("Select Dca");
                        GridView2.rows(rowCount).cells(3).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(4).children[0].value == "Select") {
                        window.alert("Select Sub Dca");
                        GridView2.rows(rowCount).cells(4).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(5).children[0].value == "Select") {
                        window.alert("Select Tax No");
                        GridView2.rows(rowCount).cells(5).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(6).children[0].value == "") {
                        window.alert("Enter Amount");
                        GridView2.rows(rowCount).cells(6).children[0].focus();
                        return false;
                    }
                }
            }

            if (!ChceckRBL("<%=rbtndeductioncharges.ClientID %>")) {
                return false;
            }
            if (SelectedIndex("<%=rbtndeductioncharges.ClientID %>") == 0) {
                GridView3 = document.getElementById("<%=gvdeduction.ClientID %>");
                if (GridView3 != null) {
                    for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                        if (GridView3.rows(rowCount).cells(1).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(2).children[0].value == "Select") {
                            window.alert("Select");
                            GridView3.rows(rowCount).cells(2).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(3).children[0].value == "Select") {
                            window.alert("Select Cost Center");
                            GridView3.rows(rowCount).cells(3).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(4).children[0].value == "Select") {
                            window.alert("Select DCA");
                            GridView3.rows(rowCount).cells(4).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(5).children[0].value == "Select") {
                            window.alert("Select Sub DCA");
                            GridView3.rows(rowCount).cells(5).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(6).children[0].value == "") {
                            window.alert("Enter Amount");
                            GridView3.rows(rowCount).cells(6).children[0].focus();
                            return false;
                        }
                    }
                }
            }
            var objs = new Array("<%=ddlfrom.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }

            var str1 = document.getElementById("<%=txtpodate.ClientID %>").value;
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
            _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
            if (parseInt(_Diff) < 0) {
                alert("Invalid Credited Date");
                document.getElementById("<%=txtdate.ClientID %>").focus();
                return false;
            }

            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function Total() {
            var invoiceval = 0;
            var Netval = 0;
            var basic = document.getElementById("<%=txtbasic.ClientID %>").value;
            var taxes = document.getElementById("<%=lbltaxes.ClientID %>").value;
            var cess = document.getElementById("<%=txtcess.ClientID %>").value;

            var deduction = document.getElementById("<%=txtdeduction.ClientID %>").value;

            if (basic == "") {
                basic = 0;
            }
            if (taxes == "") {
                tax = 0;
            }
            if (cess == "") {
                cess = 0;
            }

            if (deduction == "") {
                deduction = 0;
            }

            invoiceval = eval((parseFloat(basic) + parseFloat(taxes) + parseFloat(cess) - parseFloat(deduction)));

            var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);

            if (roundinvvalue >= 0) {
                document.getElementById('<%= txtamt.ClientID%>').value = roundinvvalue;

            }
            else {
                alert("Net Amount is not less than Zero");
                return false;
            }
        }

    </script>
</asp:Content>
