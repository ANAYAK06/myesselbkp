<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="frmpendinginvoicenew.aspx.cs" Inherits="frmpendinginvoicenew" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script src="Java_Script/validations.js" type="text/javascript"></script>
    <script type="text/javascript">
        function AddTaxes() {
            //debugger;
            var date = document.getElementById("<%=txtindt.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtindt.ClientID %>");
            var trAddTaxes = document.getElementById("<%=traddtaxes.ClientID %>");
            if (date == "") {
                alert("Please Select Invoice date before Add Taxes");
                datectrl.focus();
                return false;
            }
            else {
                document.getElementById("<%=txtindt.ClientID %>").disabled = true;
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
        function checktaxsdca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvtaxes.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            for (i = 1; i < grid.rows.length - 1; i++) {
                var dropdownoldValue = grid.rows[i].cells[4].childNodes[0].value;
                if (rowIndex != i) {
                    if (currentDropDownValue == grid.rows(i).cells(4).children[0].value) {
                        window.alert("Already SDCA Selected");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                }
            }
        }
        function verifytaxsdca() {
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
            var date = document.getElementById("<%=txtindt.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtindt.ClientID %>");
            var traddcess = document.getElementById("<%=traddcess.ClientID %>");
            if (date == "") {
                alert("Please Select Invoice date before Add Cess Taxes");
                datectrl.focus();
                return false;
            }
            else {
                document.getElementById("<%=txtindt.ClientID %>").disabled = true;
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
        //         function checkcessdca(val) {
        //            debugger;
        //            var grid = document.getElementById("<%= gvcess.ClientID %>");
        //            var currentDropDownValue = document.getElementById(val.id).value;
        //            var rowData = val.parentNode.parentNode;
        //            var rowIndex = rowData.rowIndex;
        //            for (i = 1; i < grid.rows.length - 1; i++) {
        //                if (rowIndex != i) {                   
        //                        if (currentDropDownValue == "DCA-CESS-CR" && grid.rows(i).cells(3).children[0].value == "DCA-CESS-NON-CR") {
        //                            window.alert("Invalid DCA");
        //                            document.getElementById(val.id).value = "Select";
        //                            return false;
        //                        }
        //                        else if (currentDropDownValue == "DCA-CESS-NON-CR" && grid.rows(i).cells(3).children[0].value == "DCA-CESS-CR") {
        //                            window.alert("Invalid DCA");
        //                            document.getElementById(val.id).value = "Select";
        //                            return false;
        //                        }
        //                }
        //            }
        //        }
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
    <script type="text/javascript" language="javascript">        //FOR OTHER GRID Starts
        function getotherIndex(r) {
            //debugger;
            var rbs = document.getElementById("<%=rbtnothercharges.ClientID%>");
            var date = document.getElementById("<%=txtindt.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtindt.ClientID %>");
            var hfother = document.getElementById("<%=hfother.ClientID %>").value;
            var radio = rbs.getElementsByTagName("input");
            var label = rbs.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    var value = radio[i].value;
                    if (value == "Yes") {
                        if (date == "") {
                            alert("Please Select Invoice date before Add Taxes");
                            //document.getElementById("<%=txtindt.ClientID %>").disabled = false;
                            datectrl.focus();
                            return false;
                        }
                        else {
                            if (date != "")
                                document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                            else
                                document.getElementById("<%=txtindt.ClientID %>").disabled = false;

                            document.getElementById("<%=trothergrid.ClientID %>").style.display = "block";
                            return true;
                        }
                    }
                    else if (value == "No") {
                        if (date != "")
                            document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                        else
                            document.getElementById("<%=txtindt.ClientID %>").disabled = false;
                        document.getElementById("<%=trothergrid.ClientID %>").style.display = "none";
                        hfother = 0;
                        return true;
                    }
                    else {
                        if (date != "")
                            document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                        else
                            document.getElementById("<%=txtindt.ClientID %>").disabled = false;
                        document.getElementById("<%=trothergrid.ClientID %>").style.display = "none";
                        hfother = 0;
                        return false;
                    }
                }
            }

        }

        function calculateother() {
            grd = document.getElementById("<%=gvother.ClientID %>");
            var hfother = document.getElementById("<%=hfother.ClientID %>").value;
            var totalother = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[4].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(4).children[0].value)) {
                            totalother += Number(grd.rows(rowCount).cells(4).children[0].value);
                        }
                    }
                    else {
                        totalother += Number(grd.rows(rowCount).cells(4).children[0].value);
                    }
                }
                hfother = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtother.ClientID %>").value = hfother;
            }
            else {
                hfother = 0;
                document.getElementById("<%=txtother.ClientID %>").value = hfother;
            }
        }
        function checkotherdca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvother.ClientID %>");
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
        function verifyotherdca() {
            //debugger;
            var grid = document.getElementById("<%= gvother.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (grid.rows(i).cells(2).children[0].value == "Select") {
                    window.alert("Select DCA");
                    return false;
                }
                else if (grid.rows(i).cells(3).children[0].value == "Select") {
                    window.alert("Select SDCA");
                    return false;
                }
                else if (grid.rows(i).cells(4).children[0].value == "") {
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
            var date = document.getElementById("<%=txtindt.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtindt.ClientID %>");
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var radio = rbs.getElementsByTagName("input");
            var label = rbs.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    var value = radio[i].value;
                    if (value == "Yes") {
                        if (date == "") {
                            alert("Please Select Invoice date before Add Deduction DCAs");
                            document.getElementById("<%=txtindt.ClientID %>").disabled = false;
                            datectrl.focus();
                            return false;
                        }
                        else {
                            if (date != "")
                                document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                            else
                                document.getElementById("<%=txtindt.ClientID %>").disabled = false;
                            document.getElementById("<%=trdeductiongrid.ClientID %>").style.display = "block";
                            return true;
                        }
                    }
                    else if (value == "No") {
                        if (date != "")
                            document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                        else
                            document.getElementById("<%=txtindt.ClientID %>").disabled = false;
                        document.getElementById("<%=trdeductiongrid.ClientID %>").style.display = "none";
                        hfdeduction = 0;
                        return true;
                    }
                    else {
                        if (date != "")
                            document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                        else
                            document.getElementById("<%=txtindt.ClientID %>").disabled = false;
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
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[4].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(4).children[0].value)) {
                            totaldeduction += Number(grd.rows(rowCount).cells(4).children[0].value);
                        }
                    }
                    else {
                        totaldeduction += Number(grd.rows(rowCount).cells(4).children[0].value);
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
        <%--function checkdeductiondca(val) {
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
        }--%>

         function checkdeductiondca() {
            //debugger;
            var grid = document.getElementById("<%= gvdeduction.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {

                if (i > 1) {
                    for (j = 1; j < grid.rows.length - 1; j++) {
                        if (i != j) {
                            if ((grid.rows(i).cells(2).children[0].value == grid.rows(j).cells(2).children[0].value) && (grid.rows(i).cells(3).children[0].value == grid.rows(j).cells(3).children[0].value)) {
                                window.alert("Invalid SDCA Selection");
                                return false;
                            }
                        }
                    }
                }
            }
        }
        function verifydeddca() {
            //debugger;
            var grid = document.getElementById("<%= gvdeduction.ClientID %>");

            for (i = 1; i < grid.rows.length - 1; i++) {
                if (i > 1) {
                    for (j = 1; j < grid.rows.length - 1; j++) {
                        if (i != j) {
                            if ((grid.rows(i).cells(2).children[0].value == grid.rows(j).cells(2).children[0].value) && (grid.rows(i).cells(3).children[0].value == grid.rows(j).cells(3).children[0].value)) {
                                window.alert("Invalid SDCA Selection");                                
                                return false;
                            }
                        }
                    }
                }
                if (grid.rows(i).cells(2).children[0].value == "Select") {
                    window.alert("Select DCA");
                    return false;
                }
                else if (grid.rows(i).cells(3).children[0].value == "Select") {
                    window.alert("Select SDCA");
                    return false;
                }
                else if (grid.rows(i).cells(4).children[0].value == "") {
                    window.alert("Enter Amount");
                    return false;
                }

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
            var str1 = document.getElementById("<%=txtindt.ClientID %>").value;
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
                document.getElementById("<%=txtindt.ClientID %>").value = "";
                document.getElementById("<%=txtindtmk.ClientID %>").value = "";
                return false;
            }
        }
        function checkDatefrom(sender, args) {
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
            var str1 = document.getElementById("<%=txtindtmk.ClientID %>").value;
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
                document.getElementById("<%=txtindtmk.ClientID %>").value = "";
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
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table class="estbl eslbl" width="750px">
                            <tr>
                                <th align="center">Service Provider Invoice Creation
                                </th>
                            </tr>
                            <tr id="paytype" runat="server">
                                <td colspan="6">
                                    <table class="innertab">
                                        <tr>
                                            <td style="width: 200px;">Vendor Name:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlvendor" Width="300px" AutoPostBack="true" CssClass="esddown"
                                                    ToolTip="Select Vendor" runat="server" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px;">Po No:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpo" runat="server" AutoPostBack="true" ToolTip="Po" Width="150px"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddlpo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="tbldetails" class="estbl" width="750px" runat="server">
                            <tr id="trcredit" runat="server">
                                <td style="border-right: none">
                                    <asp:Label ID="Label5" runat="server" Font-Size="10px" CssClass="eslbl" Text="CC Code"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="lblccode" Width="80px" ReadOnly="true" onKeyPress="javascript: return false;"
                                        onKeyDown="javascript: return false;" runat="server"> </asp:TextBox>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label7" runat="server" Font-Size="10px" CssClass="eslbl" Text="DCA"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="lbldcacode" Width="80px" ReadOnly="true" onKeyPress="javascript: return false;"
                                        onKeyDown="javascript: return false;" runat="server"> </asp:TextBox>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label9" CssClass="eslbl" Font-Size="10px" runat="server" Text="Sub DCA"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="lblsdcacode" Width="80px" ReadOnly="true" onKeyPress="javascript: return false;"
                                        onKeyDown="javascript: return false;" runat="server"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr id="Invoice" runat="server">
                                <td colspan="6">
                                    <table class="" runat="server" style="border-style: hidden;" width="100%">
                                        <tr id="tr2" runat="server">
                                            <td colspan="6">
                                                <table width="100%" style="border-style: hidden;">
                                                    <tr style="border-style: hidden;">
                                                        <td style="border-style: hidden; font-weight: normal">
                                                            <asp:Label ID="Label1" runat="server" CssClass="eslbl" Font-Size="10px" Text="Invoice No:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtin" CssClass="estbox" runat="server" ToolTip="Invoice No" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden; font-weight: normal">
                                                            <asp:Label ID="Label2" runat="server" CssClass="eslbl" Font-Size="10px" Text="Invoice Date:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtindt" CssClass="estbox" onKeyDown="preventBackspace();" runat="server"
                                                                onpaste="return false;" onkeypress="return false;" ToolTip="Invoice Date" Width="85px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindt"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                OnClientDateSelectionChanged="checkDate" PopupButtonID="txtindt">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="border-style: hidden; font-weight: normal">
                                                            <asp:Label ID="Label3" runat="server" CssClass="eslbl" Font-Size="10px" Text="Inv Making Date:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtindtmk" CssClass="estbox" onKeyDown="preventBackspace();" runat="server"
                                                                onpaste="return false;" onkeypress="return false;" ToolTip="Invoice Making Date"
                                                                Width="85px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtindtmk"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="checkDatefrom" FirstDayOfWeek="Monday" Animated="true"
                                                                PopupButtonID="txtindtmk">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="border-style: hidden; font-weight: normal">
                                                            <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="10px" Text="Basic Value:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtbasic" CssClass="estbox" runat="server" ToolTip="Basic Value"
                                                                Width="85px" onkeypress="return numericValidation(this);" onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trtaxes" runat="server">
                                <td colspan="6">
                                    <table id="tblgridtaxes" runat="server" class="innertab" align="center" width="750px">
                                        <tr align="left">
                                            <td colspan="6" align="left">
                                                <button name="button" onclick="AddTaxes()" id="btnaddtax" runat="server" type="button">
                                                    Add Tax</button>
                                                <button name="button" id="btnremovetax" runat="server" onclick="RemoveTaxes()" type="button">
                                                    Remove All Taxes</button>
                                                <asp:Button ID="btnremovetaxes" runat="server" Style="display: none;" Text="" OnClick="btnremovetaxes_Click" />
                                                <asp:Button ID="btnaddtaxes" runat="server" Style="display: none;" Text="" OnClick="btnAddTax_Click" />
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
                                                                    ToolTip="Type" runat="server" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltaxdca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                    ToolTip="DCA" OnSelectedIndexChanged="ddltaxdca_SelectedIndexChanged" AutoPostBack="true"
                                                                    runat="server">
                                                                    <%--onchange="checktaxdca(this)"--%>
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SDCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltaxsdca" Font-Size="7" onchange="checktaxsdca(this)" Width="120px"
                                                                    CssClass="filter_item" ToolTip="SDCA" runat="server">
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
                                                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" OnClientClick="javascript:return verifytaxsdca();"
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
                            <tr id="trcess" runat="server">
                                <td colspan="6">
                                    <table id="tblgridcess" runat="server" class="innertab" align="center" width="750px">
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
                                                                    ToolTip="Cess Type" runat="server" OnSelectedIndexChanged="ddltypecess_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlcessdca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                    ToolTip="Cess DCA" OnSelectedIndexChanged="ddlcessdca_SelectedIndexChanged" AutoPostBack="true"
                                                                    runat="server">
                                                                    <%-- onchange="checkcessdca(this)" --%>
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
                                                                <asp:Button ID="btnAddcess" runat="server" OnClick="btnaddcess_Click" OnClientClick="javascript:return verifycessdca();"
                                                                    Text="Add More Cess" />
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
                            <tr id="trother" runat="server">
                                <td colspan="6">
                                    <table id="Table1" runat="server" class="innertab" align="center" width="750px">
                                        <tr align="left">
                                            <td style="width: 750px" align="center">
                                                <asp:Label ID="lblother" runat="server" Font-Size="10px" Width="150px" CssClass="eslbl"
                                                    Text="Other Charges"></asp:Label>
                                                <asp:RadioButtonList ID="rbtnothercharges" runat="server" Width="200px" AutoPostBack="true"
                                                    CssClass="eslbl" ClientIDMode="AutoID" OnSelectedIndexChanged="rbtnothercharges_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" Style="font-size: x-small" onclick="javascript:return getotherIndex(this)"
                                                    ToolTip="Other Charges Yes or No">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trothergrid" runat="server" style="display: none">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvother" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true"
                                                    OnRowDeleting="gvother_RowDeleting" OnRowDataBound="gvother_RowDataBound">
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
                                                                <asp:CheckBox ID="chkother" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA Code" ItemStyle-Width="250px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlotherdca" Font-Size="7" Width="250px" CssClass="filter_item"
                                                                    ToolTip="Other DCA" runat="server" OnSelectedIndexChanged="ddlotherdca_SelectedIndexChanged"
                                                                    AutoPostBack="true" onchange="checkotherdca(this)">
                                                                    <%----%>
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUBDCA Code" ItemStyle-Width="250px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlothersdca" Font-Size="7" Width="250px" CssClass="filter_item"
                                                                    ToolTip="Other SDCA" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtotheramount" runat="server" CssClass="filter_item" Width="75px"
                                                                    onkeypress="return numericValidation(this);" onkeyup="calculateother(); Total();"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnaddothers" runat="server" Text="Add More Other Dca" OnClientClick="javascript:return verifyotherdca();"
                                                                    OnClick="btnaddothers_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="50px" DeleteText="Remove" />
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
                                                <asp:HiddenField ID="hfother" runat="server" />
                                                <asp:Label ID="lbl2" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Other Amount:"></asp:Label>
                                                <asp:TextBox ID="txtother" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdeduction" runat="server">
                                <td colspan="6">
                                    <table id="Table2" runat="server" class="innertab" align="center" width="750px">
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
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkdeduction" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA Code" ItemStyle-Width="250px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddldeductiondca" Font-Size="7" Width="250px" CssClass="filter_item"
                                                                    ToolTip="Other DCA" runat="server" OnSelectedIndexChanged="ddldeductiondca_SelectedIndexChanged"
                                                                    AutoPostBack="true" >
                                                                    <%--onchange="checkdeductiondca(this)"--%>
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUBDCA Code" ItemStyle-Width="250px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddldeductionsdca" Font-Size="7" Width="250px" CssClass="filter_item"
                                                                    ToolTip="Other SDCA" onchange="checkdeductiondca()" runat="server">
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
                            <tr id="tr1" runat="server">
                                <td style="border-right: none">
                                    <asp:Label ID="Label11" runat="server" Font-Size="10px" CssClass="eslbl" Text="Invoice Value"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="txtinvvalue" Width="150px" ToolTip="Invoice Value" onKeyPress="javascript: return false;"
                                        onKeyDown="javascript: return false;" runat="server"> </asp:TextBox>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label12" runat="server" Font-Size="10px" CssClass="eslbl" Text="Description"></asp:Label>
                                </td>
                                <td style="border-right: none" colspan="3">
                                    <asp:TextBox ID="txtdescription" ToolTip="Description" Width="350px" runat="server"
                                        TextMode="MultiLine"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr id="tr3" runat="server">
                                <td style="border-right: none">
                                    <asp:Label ID="Label13" runat="server" Font-Size="10px" CssClass="eslbl" Text="Rentention"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="txtretention" Width="140px" ToolTip="Retention" onkeyup="Total();"
                                        onkeypress="return numericValidation(this);" runat="server"> </asp:TextBox>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label14" runat="server" Font-Size="10px" CssClass="eslbl" Text="Hold"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="txthold" Width="140px" ToolTip="Hold" onkeyup="Total();" onkeypress="return numericValidation(this);"
                                        runat="server"> </asp:TextBox>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label15" CssClass="eslbl" Font-Size="10px" runat="server" Text="Net Amount"></asp:Label>
                                </td>
                                <td style="border-right: none" align="right">
                                    <asp:TextBox ID="txtnetamt" Width="140px" onKeyPress="javascript: return false;"
                                        onKeyDown="javascript: return false;" Enabled="false" runat="server"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr id="btn" runat="server">
                                <td align="center" colspan="6">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validate();" OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Reset" OnClick="btnCancel1_Click" />
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
            var objs = new Array("<%=txtin.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var str1 = document.getElementById("<%=txtindt.ClientID %>").value;
            var str2 = document.getElementById("<%=txtindtmk.ClientID %>").value;
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
                alert("Invalid Invoice Making Date");
                document.getElementById("<%=txtindtmk.ClientID %>").focus();
                return false;
            }
            var objs = new Array("<%=txtbasic.ClientID %>");
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
            //debugger;
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
            //debugger;
            if (!ChceckRBL("<%=rbtnothercharges.ClientID %>")) {
                return false;
            }

            if (SelectedIndex("<%=rbtnothercharges.ClientID %>") == 0) {
                GridView3 = document.getElementById("<%=gvother.ClientID %>");
                if (GridView3 != null) {
                    for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                        if (GridView3.rows(rowCount).cells(1).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(2).children[0].value == "Select") {
                            window.alert("Select Dca");
                            GridView3.rows(rowCount).cells(2).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(3).children[0].value == "Select") {
                            window.alert("Select Sub Dca");
                            GridView3.rows(rowCount).cells(3).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(4).children[0].value == "") {
                            window.alert("Enter Amount");
                            GridView3.rows(rowCount).cells(4).children[0].focus();
                            return false;
                        }
                    }
                }
            }
            if (!ChceckRBL("<%=rbtndeductioncharges.ClientID %>")) {
                return false;
            }
            if (SelectedIndex("<%=rbtndeductioncharges.ClientID %>") == 0) {
                GridView4 = document.getElementById("<%=gvdeduction.ClientID %>");
                if (GridView4 != null) {
                    for (var rowCount = 1; rowCount < GridView4.rows.length - 1; rowCount++) {
                        if (GridView4.rows(rowCount).cells(1).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView4.rows(rowCount).cells(2).children[0].value == "Select") {
                            window.alert("Select Dca");
                            GridView4.rows(rowCount).cells(2).children[0].focus();
                            return false;
                        }
                        else if (GridView4.rows(rowCount).cells(3).children[0].value == "Select") {
                            window.alert("Select Sub Dca");
                            GridView4.rows(rowCount).cells(3).children[0].focus();
                            return false;
                        }
                        else if (GridView4.rows(rowCount).cells(4).children[0].value == "") {
                            window.alert("Enter Amount");
                            GridView4.rows(rowCount).cells(4).children[0].focus();
                            return false;
                        }
                    }
                }
            }
            var objs = new Array("<%=txtinvvalue.ClientID %>", "<%=txtdescription.ClientID %>", "<%=txtinvvalue.ClientID %>", "<%=txtretention.ClientID %>", "<%=txthold.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var basic = document.getElementById("<%=txtbasic.ClientID %>").value;
            var deduction = document.getElementById("<%=txtdeduction.ClientID %>").value;
            var Retention = document.getElementById("<%=txtretention.ClientID %>").value;
            var Hold = document.getElementById("<%=txthold.ClientID %>").value;
            if (basic == "") {
                basic = 0;
            } if (deduction == "") {
                deduction = 0;
            }
            if (Retention == "") {
                Retention = 0;
            }
            if (Hold == "") {
                Hold = 0;
            }
            if (eval(parseFloat(basic) < (parseFloat(deduction) + parseFloat(Retention) + parseFloat(Hold)))) {
                window.alert("ATTENTION:- Sum of [Deduction + Retention + Hold] Amount is not more than BasicAmount");
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
            var other = document.getElementById("<%=txtother.ClientID %>").value;
            var deduction = document.getElementById("<%=txtdeduction.ClientID %>").value;
            var Retention = document.getElementById("<%=txtretention.ClientID %>").value;
            var Hold = document.getElementById("<%=txthold.ClientID %>").value;
            if (basic == "") {
                basic = 0;
            }
            if (taxes == "") {
                tax = 0;
            }
            if (cess == "") {
                cess = 0;
            }
            if (other == "") {
                other = 0;
            }
            if (deduction == "") {
                deduction = 0;
            }
            if (Retention == "") {
                Retention = 0;
            }
            if (Hold == "") {
                Hold = 0;
            }
            //debugger;
            if (eval(parseFloat(basic) < (parseFloat(deduction) + parseFloat(Retention) + parseFloat(Hold)))) {                
                window.alert("ATTENTION:- Sum of [Deduction + Retention + Hold] Amount is not more than BasicAmount");
                return false;
            }
            invoiceval = eval((parseFloat(basic) + parseFloat(taxes) + parseFloat(cess) + parseFloat(other) - parseFloat(deduction)));
            Netval = eval((parseFloat(invoiceval) - parseFloat(Retention) - parseFloat(Hold)));
            var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);
            var roundNetVal = Math.round(Netval * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtinvvalue.ClientID%>').value = roundinvvalue;
            document.getElementById('<%= txtnetamt.ClientID%>').value = roundNetVal;
        }        
        
    </script>
</asp:Content>
