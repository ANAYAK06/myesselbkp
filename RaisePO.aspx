<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="RaisePO.aspx.cs"
    Inherits="RaisePO" EnableEventValidation="false" Title="Raise PO - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript">
        function Print() {

            document.body.offsetHeight;
            window.print();
        }
        function validate() {
            var objs = new Array("<%=ddlvendor.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }

            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
        function validatepo() {
            var objs = new Array("<%=ddlvendor.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }

            document.getElementById("<%=btnsubmitpo.ClientID %>").style.display = 'none';
            return true;
        }
        function confirmvalidatepo() {
            debugger;
            var objs = new Array("<%=txtpodatepo.ClientID %>", "<%=txtrefnopo.ClientID %>", "<%=txtrefdatepo.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }

            var GridView = document.getElementById("<%=grdbillpo.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(8).children[0].value == "") {
                        window.alert("Please Insert Purchase Price for itemcode " + GridView.rows(rowCount).cells(1).innerHTML);
                        return false;
                    }
                }
            }
            //debugger;
            var GridView1 = document.getElementById("<%=grdterms.ClientID %>");
            if (GridView1 != null) {

                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (rowCount == 1) {
                        if (GridView1.rows(rowCount).cells(2).children[0].value == "") {
                            window.alert("Please Add Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                            window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                            window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify Terms and Conditions");
                            return false;
                        }

                    }
                    if (rowCount > 1) {
                        if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(2).children[0].value == "") {
                            window.alert("Please Add Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                            window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                            window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                            return false;
                        }
                    }
                }

            }

            document.getElementById("<%=btnupdateprintpo.ClientID %>").style.display = 'none';
            return true;
        }
        function confirmvalidate() {
            var objs = new Array("<%=txtpodate.ClientID %>", "<%=txtrefno.ClientID %>", "<%=txtrefdate.ClientID %>", "<%=txtremarks.ClientID %>", "<%=txtrecieved_cc.ClientID %>", "<%=txtrecieved_date.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }

            document.getElementById("<%=btnupdateprint.ClientID %>").style.display = 'none';
            return true;
        }
        function closepopup() {
            $find('mdlitems').hide();
            timedRefresh();

        }
        function timedRefresh() {
            setTimeout("location.reload(true);");
        }
    </script>
    <script language="javascript">
        function validate1() {
            var count = 0;
            var GridView = document.getElementById("<%=Grditemspopup.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(10).children(0).checked == true) {
                        count = count + 1;
                        if (GridView.rows(rowCount).cells(9).children[0].value == "") {
                            alert("Please enter quantity");
                            return false;
                        }

                    }

                }
                if (count == 0) {
                    alert("Please select atleast one item");
                    return false;
                }
            }
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
            if (grid_obj != null) {
                var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
                new_window.document.write(grid_obj.outerHTML);
                new_window.print();
                // new_window.close();
            }
        }
    </script>
    <script language="javascript">
        function Checkqty1() {
            GridView = document.getElementById("<%=Grditemspopup.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var rqty = GridView.rows(rowCount).cells(8).innerText;
                var iqty = GridView.rows(rowCount).cells(9).children[0].value;
                if (parseFloat(rqty) < parseFloat(iqty)) {
                    window.alert("You are not able to issue more quantity");
                    GridView.rows(rowCount).cells(9).children[0].focus();
                    GridView.rows(rowCount).cells(9).children[0].value = "";
                    return false;
                }
                if (iqty <= 0 && iqty != "") {
                    window.alert("You can't Raise PO with this quantity");
                    GridView.rows(rowCount).cells(9).children[0].focus();
                    GridView.rows(rowCount).cells(9).children[0].value = "";
                    return false;
                }
            }
        }
    
    </script>
    <style type="text/css">
        .popup-div-background
        {
            position: absolute;
            top: 0;
            left: 0;
            background-color: #ccc;
            filter: alpha(opacity=90);
            opacity: 0.9; /* the following two line will make sure
             /* that the whole screen is covered by
                 /* this transparent layer */
            height: 100%;
            width: 100%;
            min-height: 100%;
            min-width: 100%;
        }
    </style>
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
    <style type="text/css">
        #overlay
        {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: #000;
            filter: alpha(opacity=70);
            -moz-opacity: 0.7;
            -khtml-opacity: 0.7;
            opacity: 0.7;
            z-index: 100;
            display: none;
        }
        .content a
        {
            text-decoration: none;
        }
        .popup
        {
            width: 100%;
            margin: 0 auto;
            display: none;
            position: fixed;
            z-index: 101;
        }
        .content
        {
            min-width: 600px;
            width: 600px;
            min-height: 150px;
            margin: 100px auto;
            background: #f3f3f3;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
        }
        .content p
        {
            clear: both;
            color: #555555;
            text-align: justify;
        }
        .content p a
        {
            color: #d91900;
            font-weight: bold;
        }
        .content .x
        {
            float: right;
            height: 35px;
            left: 22px;
            position: relative;
            top: -25px;
            width: 34px;
        }
        .content .x:hover
        {
            cursor: pointer;
        }
    </style>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function showmodalpopup() {
            //debugger;
            $find('mdlitems').show();
            var GridView = document.getElementById("<%=Grditemspopup.ClientID %>");
            if (document.getElementById("<%=ddlindent.ClientID %>").selectedIndex != 0) {
                //var CCcode = document.getElementById("<%=ddlindent.ClientID %>").value.substring(0, 5);
                var CCcode = document.getElementById("<%=ddlindent.ClientID %>").value.split('/')[0];
                var Length = 0;
                var ddlArray = new Array();
                var ddl = document.getElementById("<%=ddlindentno.ClientID%>");
                for (i = 0; i < ddl.options.length; i++) {
                    if (ddl.options[i].value.split('/')[0] == CCcode) {
                        Length = Length + 1;
                    }
                }


                if (Length != 0) {

                    document.getElementById("<%=trpartind.ClientID %>").style.display = 'block';
                    document.getElementById("<%=trpartind1.ClientID %>").style.display = 'none';
                    document.getElementById("<%=trcancelind.ClientID %>").style.display = 'none';
                    document.getElementById("<%=button.ClientID %>").style.display = 'none';
                    document.getElementById("<%=popindentno.ClientID %>").style.display = 'none';
                    //                    if (document.getElementById("<%=ddlindent.ClientID %>").value.substring(0, 3) != 'CCC')
                    //                        document.getElementById("<%=lblcc.ClientID %>").innerHTML = document.getElementById("<%=ddlindent.ClientID %>").value.split('/')[0];
                    //                    else
                    document.getElementById("<%=lblcc.ClientID %>").innerHTML = document.getElementById("<%=ddlindent.ClientID %>").value.split('/')[0];
                    document.getElementById("<%=Label13.ClientID %>").innerHTML = "Some Part Indents are already waiting for raising PO against this CC, Do you want to raise a PO against Part Indent..? ";
                }
                else {
                    document.getElementById("<%=trpartind1.ClientID %>").style.display = 'none';
                    document.getElementById("<%=trpartind.ClientID %>").style.display = 'block';
                    document.getElementById("<%=trcancelind.ClientID %>").style.display = 'none';
                    document.getElementById("<%=button.ClientID %>").style.display = 'none';
                    document.getElementById("<%=popindentno.ClientID %>").style.display = 'none';
                    document.getElementById("<%=Label13.ClientID %>").innerHTML = "Do you really want to raise a PO against this New Indent No :-  " + document.getElementById("<%=ddlindent.ClientID %>").value;
                    //                    if (document.getElementById("<%=ddlindent.ClientID %>").value.substring(0, 3) != 'CCC')
                    //                        document.getElementById("<%=lblcc.ClientID %>").innerHTML = document.getElementById("<%=ddlindent.ClientID %>").value.split('/')[0];
                    //                    else
                    document.getElementById("<%=lblcc.ClientID %>").innerHTML = document.getElementById("<%=ddlindent.ClientID %>").value.split('/')[0];
                    document.getElementById("<%=btnremaindmelater.ClientID %>").value = "yes";
                    document.getElementById("<%=btnyesnew.ClientID %>").value = "No";
                }
            }
            else if (document.getElementById("<%=ddlindentno.ClientID %>").selectedIndex != 0) {

                document.getElementById("<%=trpartind.ClientID %>").style.display = 'none';
                document.getElementById("<%=trpartind1.ClientID %>").style.display = 'block';
                document.getElementById("<%=trcancelind.ClientID %>").style.display = 'none';
                document.getElementById("<%=button.ClientID %>").style.display = 'none';
                document.getElementById("<%=popindentno.ClientID %>").style.display = 'none';
                document.getElementById("<%=Label13.ClientID %>").innerHTML = "Do you really want to raise a PO against this Part Indent No :-  " + document.getElementById("<%=ddlindentno.ClientID %>").value;
                //                if (document.getElementById("<%=ddlindent.ClientID %>").value.substring(0, 3) != 'CCC')
                //                    document.getElementById("<%=lblcc.ClientID %>").innerHTML = document.getElementById("<%=ddlindentno.ClientID %>").value.split('/')[0];
                //                else
                document.getElementById("<%=lblcc.ClientID %>").innerHTML = document.getElementById("<%=ddlindentno.ClientID %>").value.split('/')[0];
            }
        }

    </script>
    <script language="javascript">
        function showtr() {
            document.getElementById("<%=trpartind.ClientID %>").style.display = 'none';
            document.getElementById("<%=trpartind1.ClientID %>").style.display = 'none';
            document.getElementById("<%=trcancelind.ClientID %>").style.display = 'block';
            document.getElementById("<%=button.ClientID %>").style.display = 'none';
            document.getElementById("<%=popindentno.ClientID %>").style.display = 'none';
            document.getElementById("<%=Label13.ClientID %>").innerHTML = "Do you want to cancel this Indent..?";

            return false;
        }
        function hidepopup() {
            if (document.getElementById("<%=btnyesnew.ClientID %>").value == "No") {
                $find('mdlitems').hide();
                document.getElementById("<%=ddlindent.ClientID %>").focus();
                document.getElementById("<%=ddlindent.ClientID %>").selectedIndex = 0;
                document.getElementById("<%=lblcc.ClientID %>").innerHTML = "";

            }
            else {
                $find('mdlitems').hide();
                document.getElementById("<%=ddlindentno.ClientID %>").focus();
                document.getElementById("<%=ddlindent.ClientID %>").selectedIndex = 0;
                document.getElementById("<%=lblcc.ClientID %>").innerHTML = "";
            }
        }
    </script>
    <script type="text/javascript">
        function checkDateref(sender, args) {
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
            var str1 = document.getElementById("<%=txtrefdate.ClientID %>").value;
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
                document.getElementById("<%=txtrefdate.ClientID %>").value = "";
                return false;
            }
        }
        function checkDaterefpo(sender, args) {
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
            var str1 = document.getElementById("<%=txtrefdatepo.ClientID %>").value;
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
                document.getElementById("<%=txtrefdatepo.ClientID %>").value = "";
                return false;
            }
        }
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
        function checkDatepopo(sender, args) {
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
            var str1 = document.getElementById("<%=txtpodatepo.ClientID %>").value;
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
                document.getElementById("<%=txtpodatepo.ClientID %>").value = "";
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
    <script type="text/javascript">
        var iRowIndex;
        function MouseEvents(objRef, evt, desc) {

            if (evt.type == "mouseover") {
                objRef.style.cursor = 'pointer'
                objRef.style.backgroundColor = "#EEE";
                ShowDiv(desc, -100);

            }
            else {
                objRef.style.backgroundColor = "#FFF";
                iRowIndex = objRef.rowIndex;
                HideDiv();
            }
        }
        function ShowDiv(desc, pos) {
            document.getElementById("<%=divDetail.ClientID %>").style.display = 'block';
            document.getElementById("<%=divDetail.ClientID %>").innerHTML = desc;
            document.getElementById("<%=divDetail.ClientID %>").style.marginTop = pos.toString() + "px";
        }
        function HideDiv() {
            document.getElementById("<%=divDetail.ClientID %>").style.display = 'none';
        }
        function highlight(objRef, evt) {
            if (evt.type == "mouseover") {
                objRef.style.display = 'block';
                document.getElementById("<%=grdbillpo.ClientID %>").rows[iRowIndex].style.backgroundColor = "#641E16";
            }
            else {
                if (evt.type == "mouseout") {
                    document.getElementById("<%=grdbillpo.ClientID %>").rows[iRowIndex].style.backgroundColor = "#FFF";
                    objRef.style.display = 'none';
                }
            }
        }
    </script>
    <style type="text/css">
        body
        {
            font-family: Arial, Tahoma;
            font-size: 15px;
        }
        .grid
        {
            width: 100%;
            font: inherit;
            background-color: #641E16;
            border: solid 1px #525252;
        }
        .grid td
        {
            font: inherit;
            padding: 3px 5px;
            border: solid 1px #C1C1C1;
            color: #333;
            text-align: left;
        }
        .grid th
        {
            padding: 3px;
            color: #FFF;
            background: #424242;
            border-left: solid 1px #525252;
            font: inherit;
            text-align: center;
            text-transform: uppercase;
        }
        h5
        {
            color: #7B241C;
            text-decoration: underline;
        }
        .divDetail
        {
            float: right;
            font: inherit;
            font-size: 13px;
            padding: 2px 5px;
            width: 300px;
            border: solid 2px #A93226;
            -moz-border-radius: 0 7px 7px 0;
            -webkit-border-radius: 0 7px 7px 0;
            border-radius: 0 7px 7px 0;
            display: none;
            color: #78281F;
            background-color: #D5DBDB;
        }
        .divDetail p
        {
            font: inherit;
        }
        .divDetail a
        {
            font: inherit;
            float: right;
            background-color: #A93226;
            color: #2E4053;
            text-decoration: none;
            border: solid 1px #2F5BB7;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            padding: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table style="vertical-align: left;" align="center">
                                <tr>
                                    <td>
                                        <table style="vertical-align: middle;" align="left" class="fields" width="100%">
                                            <tr>
                                                <td>
                                                    <table id="Table6" runat="server" style="width: 700px">
                                                        <tr>
                                                            <td colspan="4" valign="top" class=" item-notebook" width="100%">
                                                                <div id="Div1" class="notebook" style="display: block;">
                                                                    <div class="notebook-tabs">
                                                                        <div class="right scroller">
                                                                        </div>
                                                                        <div class="left scroller">
                                                                        </div>
                                                                        <ul class="notebook-tabs-strip">
                                                                            <li class="notebook-tab notebook-page notebook-tab-active" title="" id="Li2"><span
                                                                                class="tab-title"><span>Raise PO/DO</span></span></li><li class="notebook-tab notebook-page"
                                                                                    title="" style="display: none;"><span class="tab-title"><span></span></span>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="notebook-pages">
                                                                        <div class="notebook-page notebook-page-active">
                                                                            <div>
                                                                                <table border="0" class="fields" width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table border="0" class="fields" width="100%">
                                                                                                    <tr>
                                                                                                        <td class="label" width="1%">
                                                                                                            <label class="help">
                                                                                                                New Indent:
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td width="" valign="middle" class="item item-char">
                                                                                                            <asp:DropDownList ID="ddlindent" runat="server" class="click" ToolTip="Indent" onchange="showmodalpopup();">
                                                                                                            </asp:DropDownList>
                                                                                                            <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlindent"
                                                                                                                ServicePath="cascadingDCA.asmx" Category="ind" LoadingText="Please Wait" ServiceMethod="indent">
                                                                                                            </cc1:CascadingDropDown>
                                                                                                        </td>
                                                                                                        <td class="label" width="1%">
                                                                                                            <label class="help">
                                                                                                                Part Indent:
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td width="" valign="middle" class="item item-char">
                                                                                                            <asp:DropDownList ID="ddlindentno" runat="server" class="click" ToolTip="Indent"
                                                                                                                onchange="showmodalpopup();">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="label" width="1%">
                                                                                                            <label class="help">
                                                                                                                CC Code:
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td width="31%" valign="middle" class="item item-char">
                                                                                                            <span class="filter_item">
                                                                                                                <asp:Label ID="lblcc" runat="server" Width="50px"></asp:Label>
                                                                                                                <%--    <asp:DropDownList ID="ddlcc" ToolTip="CC Code" CssClass="filter_item" runat="server"
                                                                                                                    OnSelectedIndexChanged="ddlcc_SelectedIndexChanged">
                                                                                                                </asp:DropDownList>--%>
                                                                                                                <%--     <cc1:CascadingDropDown ID="cascashcc" runat="server" TargetControlID="ddlcc" ServicePath="cascadingDCA.asmx"
                                                                                                                    ParentControlID="ddlindent" Category="dd1" LoadingText="Please Wait" ServiceMethod="IndentCC">
                                                                                                                </cc1:CascadingDropDown>--%>
                                                                                                            </span>
                                                                                                        </td>
                                                                                                        <td class="label" width="1%">
                                                                                                            <label class="help">
                                                                                                                Vendor Code:
                                                                                                            </label>
                                                                                                        </td>
                                                                                                        <td class="item item-char" valign="middle">
                                                                                                            <span class="filter_item">
                                                                                                                <asp:DropDownList ID="ddlvendor" CssClass="filter_item" ToolTip="Vendor" runat="server">
                                                                                                                </asp:DropDownList>
                                                                                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="ven" TargetControlID="ddlvendor"
                                                                                                                    ServiceMethod="vendorid" ServicePath="cascadingDCA.asmx">
                                                                                                                </cc1:CascadingDropDown>
                                                                                                            </span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr align="center">
                                                                                                        <td align="center" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr align="center">
                                                                                                        <td align="center" colspan="4">
                                                                                                            <asp:Button ID="btnsubmit" Width="120px" Height="20px" runat="server" Text="Submit DO"
                                                                                                                CssClass="submitbtn" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />
                                                                                                            <asp:Button ID="btnsubmitpo" Width="120px" Height="20px" runat="server" Text="Submit PO"
                                                                                                                CssClass="submitbtn" OnClick="btnsubmitpo_Click" OnClientClick="javascript:return validatepo()" />
                                                                                                            <%--OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()"--%>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
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
                        </td>
                    </tr>
                </table>
                <table id="challantabledo" runat="server">
                    <tr>
                        <td width="10px">
                        </td>
                        <td>
                            <table style="width: 90%">
                                <tr valign="bottom">
                                    <td align="center">
                                        <table width="100%" id="tblpo" runat="server" class="pestbl" style="border: 1px solid #000">
                                            <tr>
                                                <td>
                                                    <table id="Table1" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                        <tr style="border: 1px solid  #000;">
                                                            <td align="left" colspan="2">
                                                                <asp:Image ID="imglogo" runat="server" ImageUrl="~/images/essellogo1.jpg" Height="55px"
                                                                    Width="89px" />
                                                                <asp:Label ID="Label34" runat="server" Width="115px"></asp:Label>
                                                                <asp:Label ID="Label1" runat="server" CssClass="peslbl" Font-Bold="True" Font-Underline="True"
                                                                    Text="ESSEL PROJECTS PVT LTD." Font-Size="X-Large" Font-Names="Rockwell"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblspace" runat="server" Width="200px"></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                    Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label31" runat="server" Width="250px"></asp:Label>
                                                                <asp:Label ID="lblpo" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                    Text="Durg- 490026 (Chhattisgarh)"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label32" runat="server" Width="250px"></asp:Label>
                                                                <asp:Label ID="Label15" runat="server" CssClass="peslbl" Font-Bold="False" Font-Underline="False"
                                                                    Text="Tel/Fax:0771-4268469/4075401." Font-Size="Smaller"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label33" runat="server" Width="260px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                    Text="DELIVERY ORDER"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid #000">
                                                            <td width="50%" align="left" style="border: 1px solid #000">
                                                                <%--<asp:TextBox ID="txtaddress" runat="server" Width="100%" TextMode="MultiLine" Style="border: None;"></asp:TextBox>--%>
                                                                <asp:Label ID="lblname" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                <asp:Label ID="lbladdress" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                            </td>
                                                            <td align="left" width="50%" style="border: 1px solid #000">
                                                                <asp:Label ID="Label4" Width="25%" CssClass="peslbl1" runat="server" Text="DO No:-">
                                                                </asp:Label>
                                                                <asp:TextBox ID="txtpono" Width="60%" CssClass="pestbox" ToolTip="DO NO" Style="border: None;
                                                                    border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox><br />
                                                                <asp:Label ID="Label5" Width="25%" CssClass="peslbl1" runat="server" Text="DO Date:-">
                                                                </asp:Label>
                                                                <asp:TextBox ID="txtpodate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                    onkeypress="return false;" Width="60%" CssClass="pestbox" ToolTip="DO Date" Style="border: None;
                                                                    border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label6" Width="25%" CssClass="peslbl1" runat="server" Text="Ref No:-">
                                                                </asp:Label>
                                                                <asp:TextBox ID="txtrefno" Width="60%" CssClass="pestbox" MaxLength="50" ToolTip="Ref No"
                                                                    Style="border: None; border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox><br />
                                                                <asp:Label ID="Label7" Width="25%" CssClass="peslbl1" runat="server" Text="Ref Date:-">
                                                                </asp:Label>
                                                                <asp:TextBox ID="txtrefdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                    onkeypress="return false;" Width="60%" CssClass="pestbox" ToolTip="Ref Date"
                                                                    Style="border: None; border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Table3" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                        <tr style="border: 1px solid #000">
                                                            <td colspan="2" style="border: 1px solid #000">
                                                                <asp:GridView CssClass="" ID="grdbill" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                    OnRowDataBound="grdbill_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                            ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="id" HeaderText="" />
                                                                        <asp:BoundField DataField="item_code" ItemStyle-CssClass="peslbl1" HeaderText="" />
                                                                        <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                        <asp:BoundField DataField="Specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                            ItemStyle-Width="150px" HeaderText="Specification" />
                                                                        <asp:BoundField DataField="basic_price" ItemStyle-CssClass="peslbl1" HeaderText="" />
                                                                        <asp:BoundField DataField="Units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                            ItemStyle-Width="50px" HeaderText="Units" />
                                                                        <asp:BoundField DataField="Quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                            ItemStyle-Width="50px" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid #000">
                                                            <td>
                                                                <asp:Label ID="lblremarks" runat="server" Text="Remarks" CssClass="peslbl1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtremarks" MaxLength="150" ToolTip="Remarks" CssClass="peslbl1"
                                                                    runat="server" Width="600px" Text="" Style="border: None; border-bottom: 1px solid #000">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid #000" align="left">
                                                            <td colspan="2" style="border: 1px solid #000">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="Label8" CssClass="peslbl1" runat="server" Text="">You are requested to supply/deliver above item/s to our work site / Central store  at</asp:Label>
                                                                <asp:TextBox ID="txtrecieved_cc" Width="25%" ToolTip="To" CssClass="pestbox" Style="border: None;
                                                                    border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox>
                                                                <asp:Label ID="Label10" CssClass="peslbl1" runat="server" Text="">by</asp:Label>
                                                                <asp:TextBox ID="txtrecieved_date" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                    onkeypress="return false;" Width="25%" ToolTip="Date" CssClass="pestbox" Style="border: None;
                                                                    border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox>
                                                                <asp:Label ID="Label12" CssClass="peslbl1" runat="server" Text="">on credit basis which should at par with the specifications, make etc. Further it is cleared that if   the item(s) supplied by you is/are found inferior/defective, the same will be return to you or deduct the amount of such items from your invoice without any notice.</asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label14" CssClass="peslbl1" runat="server" Text=""> Validity of this DO :-&nbsp&nbsp This delivery order valid only if the above material delivered at below mentioned address/ location  on or before above specified date.</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid #000">
                                                            <td colspan="2" align="left" style="border: 1px solid #000">
                                                                <br />
                                                                <asp:Label ID="Label9" runat="server" CssClass="peslblfooter" Width="170px" Text="">For Essel Projects Pvt Ltd</asp:Label>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblinv" runat="server" CssClass="peslblfooter" Width="223px" Text="INVOICE ADDRESS :"
                                                                    Font-Underline="True"></asp:Label>
                                                                <asp:Label ID="Label16" runat="server" CssClass="peslblfooter" Text="DELIVERY SITE ADDRESS :"
                                                                    Font-Underline="True"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label18" CssClass="peslblfooter" runat="server" Width="180px" Text="">
                                                                </asp:Label>
                                                                <asp:Label ID="lblinvadd1" runat="server" CssClass="peslbl1" Width="220px" Text="INV ADDRESS1 :"></asp:Label>
                                                                <asp:Label ID="Label20" runat="server" CssClass="peslbl1" Text="SITE ADDRESS1 :"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label19" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:TextBox ID="txtinvadd1" runat="server" CssClass="filter_item" ToolTip="Inv Address"
                                                                    TextMode="MultiLine" Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtSaddress" runat="server" CssClass="filter_item" ToolTip="Site Address"
                                                                    TextMode="MultiLine" Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label21" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:Label ID="lblinvadd2" runat="server" CssClass="peslbl1" Width="220px" Text="INV ADDRESS2 :"></asp:Label>
                                                                <asp:Label ID="Label22" runat="server" CssClass="peslbl1" Text="SITE ADDRESS2 :"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label23" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:TextBox ID="txtinvadd2" runat="server" CssClass="filter_item" ToolTip="Inv Address"
                                                                    TextMode="MultiLine" Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtaddress2" runat="server" CssClass="filter_item" ToolTip="Site Address"
                                                                    TextMode="MultiLine" Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label24" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:Label ID="Label11" runat="server" CssClass="peslbl1" Width="220px" Text="GST No :"></asp:Label>
                                                                <asp:Label ID="Label25" runat="server" CssClass="peslbl1" Text="Contact Person :"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label26" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:TextBox ID="txtinvgst" runat="server" CssClass="filter_item" ToolTip="Inv GST No"
                                                                    Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtCPerson" runat="server" CssClass="filter_item" ToolTip="Contact Person"
                                                                    Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label27" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:Label ID="lblinvmobileno" runat="server" CssClass="peslbl1" Width="220px" Text="Mobile No :"></asp:Label>
                                                                <asp:Label ID="Label28" runat="server" CssClass="peslbl1" Text="Mobile Number :"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label29" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:TextBox ID="txtinvmobileno" runat="server" CssClass="filter_item" ToolTip="Invoice Mobile Number"
                                                                    Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtMobileNum" runat="server" CssClass="filter_item" ToolTip="Mobile Number"
                                                                    Width="200px" Height="22px" onkeypress="javascript:return IsNumeric(event);">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label17" CssClass="peslblfooter" runat="server" Text=""> Authorized Signatory</asp:Label>
                                                                <br />
                                                                &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblpurchasemanagername" runat="server" Style="vertical-align: middle"
                                                                    Text=""></asp:Label>)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="3">
                                                                <asp:Label ID="Label30" runat="server" CssClass="peslblfooter" Text="*    It is an electronically generated DO
                                                                " Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" class="pestbl">
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:Button ID="btnupdateprint" Width="120px" Height="20px" runat="server" Text="Confirm"
                                                        CssClass="updatebtn" OnClick="btnupdateprint_Click" OnClientClick="javascript:return confirmvalidate()" />
                                                    <asp:Button ID="btncancelprint" Width="120px" Height="20px" runat="server" Text="Cancel"
                                                        CssClass="cancelbtn" OnClick="btncancelprint_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtrefdate"
                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                    PopupButtonID="txtrefdate" OnClientDateSelectionChanged="checkDateref">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtpodate"
                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                    PopupButtonID="txtpodate" OnClientDateSelectionChanged="checkDatepo">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtrecieved_date"
                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                    PopupButtonID="txtrecieved_date">
                </cc1:CalendarExtender>
                <table id="challantablepo" runat="server">
                    <tr>
                        <td width="10px">
                        </td>
                        <td>
                            <table style="width: 90%">
                                <tr valign="bottom">
                                    <td align="center">
                                        <table width="100%" id="Table4" runat="server" class="pestbl" style="border: 1px solid #000">
                                            <tr>
                                                <td>
                                                    <table id="Table2" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                        <tr style="border: 1px solid  #000;">
                                                            <td align="center" colspan="2">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/essellogo1.jpg" Height="40px"
                                                                    Width="89px" />
                                                                &nbsp&nbsp&nbsp&nbsp&nbsp
                                                                <asp:Label ID="Label35" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                    Text="ESSEL PROJECTS PVT LTD." Font-Size="XX-Large" Font-Names="Rockwell"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label36" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                    Text="Plot No.6/D, Heavy Industrial Area, Hatkhoj, Bhilai,Durg- 490026 (Chhattisgarh)"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label37" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                                                                    Text="Tel/Fax:0771-4268469/4075401." Font-Size="Smaller"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label38" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                                                                    Text="PURCHASE ORDER"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Table5" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                        <tr style="border: 1px solid #000">
                                                            <td width="50%" align="left" style="border: 1px solid #000">
                                                                <%--<asp:TextBox ID="txtaddress" runat="server" Width="100%" TextMode="MultiLine" Style="border: None;"></asp:TextBox>--%>
                                                                <asp:Label ID="lblnamepo" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                                <asp:Label ID="lbladdresspo" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                                                            </td>
                                                            <td align="left" width="50%" style="border: 1px solid #000">
                                                                <asp:Label ID="Label4po" Width="25%" CssClass="peslbl1" runat="server" Text="PO No:-">
                                                                </asp:Label>
                                                                <asp:TextBox ID="txtponopo" Width="60%" CssClass="pestbox" ToolTip="DO NO" Style="border: None;
                                                                    border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox><br />
                                                                <asp:Label ID="Label5po" Width="25%" CssClass="peslbl1" runat="server" Text="PO Date:-">
                                                                </asp:Label>
                                                                <asp:TextBox ID="txtpodatepo" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                    onkeypress="return false;" Width="60%" CssClass="pestbox" ToolTip="PO Date" Style="border: None;
                                                                    border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label6po" Width="25%" CssClass="peslbl1" runat="server" Text="Ref No:-">
                                                                </asp:Label>
                                                                <asp:TextBox ID="txtrefnopo" Width="60%" CssClass="pestbox" MaxLength="50" ToolTip="Ref No"
                                                                    Style="border: None; border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox><br />
                                                                <asp:Label ID="Label7po" Width="25%" CssClass="peslbl1" runat="server" Text="Ref Date:-">
                                                                </asp:Label>
                                                                <asp:TextBox ID="txtrefdatepo" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                    onkeypress="return false;" Width="60%" CssClass="pestbox" ToolTip="Ref Date"
                                                                    Style="border: None; border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Table7" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
                                                        <tr style="border: 1px solid #000">
                                                            <td colspan="2" style="border: 1px solid #000">
                                                                <div id="divGrid" style="width: auto; float: left;">
                                                                    <asp:GridView CssClass="" ID="grdbillpo" Width="100%" runat="server" OnRowDataBound="grdbillpo_RowDataBound"
                                                                        AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="item_code">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                                                ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="id" HeaderText="" />
                                                                            <asp:BoundField DataField="item_code" ItemStyle-CssClass="peslbl1" HeaderText="Item Code"
                                                                                HeaderStyle-BackColor="White" />
                                                                            <asp:BoundField DataField="item_name" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px" HeaderText="Item Name" />
                                                                            <asp:BoundField DataField="Specification" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                ItemStyle-Width="150px" HeaderText="Specification" />
                                                                            <asp:BoundField DataField="Quantity" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                ItemStyle-Width="50px" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Units" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                ItemStyle-Width="50px" HeaderText="Units" />
                                                                            <asp:BoundField DataField="basic_price" ItemStyle-CssClass="peslbl1" HeaderStyle-BackColor="White"
                                                                                HeaderText="Standard Price" />
                                                                            <asp:TemplateField HeaderText="Quoted Price" HeaderStyle-BackColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtquprice" runat="server" Width="75px" onkeypress="return numericValidation(this);"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Purchase Price" HeaderStyle-BackColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtpurprice" runat="server" Width="75px" Text='<%#Eval("basic_price") %>'
                                                                                        onkeyup="Calculate();" onkeypress="return numericValidation(this);"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txttotamt" runat="server" Width="75px" Enabled="false"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                                <%--THE FLOATING DIV TO SHOW EMPLOYEE DETAILS.--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div runat="server" id="divDetail" class="divDetail" onmouseover="highlight(this, event)"
                                                                    onmouseout="highlight(this, event)">
                                                                </div>
                                                                <asp:HiddenField ID="hftotalpo" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid #000">
                                                            <td colspan="2">
                                                                <asp:GridView ID="grdterms" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                    Width="100%" GridLines="None" ShowFooter="true" OnRowDeleting="grdterms_RowDeleting">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelectterms" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                                            ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Terms & Conditions" ItemStyle-Width="100%" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtterms" runat="server" onkeypress="return isNumberKey(event)"
                                                                                    Width="500px" onblur="chksplcharactersdesc()" /><br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Terms and Conditions Required"
                                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup11" ControlToValidate="txtterms"
                                                                                    runat="server" />
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                            <FooterTemplate>
                                                                                <asp:ImageButton ID="btnAddterm" runat="server" ValidationGroup="valGroup11" ImageUrl="~/images/imgadd1.gif"
                                                                                    OnClick="btnAddterm_Click" />
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="100px" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <%-- <tr style="border: 1px solid #000" align="left">
                                                            <td colspan="2" style="border: 1px solid #000">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="Label8po" CssClass="peslbl1" runat="server" Text="">You are requested to supply/deliver above item/s to our work site / Central store  at</asp:Label>
                                                                <asp:TextBox ID="txtrecieved_ccpo" Width="25%" ToolTip="To" CssClass="pestbox" Style="border: None;
                                                                    border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox>
                                                                <asp:Label ID="Label10po" CssClass="peslbl1" runat="server" Text="">by</asp:Label>
                                                                <asp:TextBox ID="txtrecieved_datepo" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                    onkeypress="return false;" Width="25%" ToolTip="Date" CssClass="pestbox" Style="border: None;
                                                                    border-bottom: 1px solid #000" runat="server">
                                                                </asp:TextBox>
                                                                <asp:Label ID="Label12po" CssClass="peslbl1" runat="server" Text="">on credit basis which should at par with the specifications, make etc. Further it is cleared that if   the item(s) supplied by you is/are found inferior/defective, the same will be return to you or deduct the amount of such items from your invoice without any notice.</asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label14po" CssClass="peslbl1" runat="server" Text=""> Validity of this PO :-&nbsp&nbsp This delivery order valid only if the above material delivered at below mentioned address/ location  on or before above specified date.</asp:Label>
                                                            </td>
                                                        </tr>--%>
                                                        <tr style="border: 1px solid #000">
                                                            <td colspan="2" align="left" style="border: 1px solid #000">
                                                                <br />
                                                                <asp:Label ID="Label9po" runat="server" CssClass="peslblfooter" Width="170px" Text="">For Essel Projects Pvt Ltd</asp:Label>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblinvpo" runat="server" CssClass="peslblfooter" Width="223px" Text="INVOICE ADDRESS :"
                                                                    Font-Underline="True"></asp:Label>
                                                                <asp:Label ID="Label16po" runat="server" CssClass="peslblfooter" Text="DELIVERY SITE ADDRESS :"
                                                                    Font-Underline="True"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label18po" CssClass="peslblfooter" runat="server" Width="180px" Text="">
                                                                </asp:Label>
                                                                <asp:Label ID="lblinvadd1po" runat="server" CssClass="peslbl1" Width="220px" Text="INV ADDRESS1 :"></asp:Label>
                                                                <asp:Label ID="Label20po" runat="server" CssClass="peslbl1" Text="SITE ADDRESS1 :"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label19po" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:TextBox ID="txtinvadd1po" runat="server" CssClass="filter_item" ToolTip="Inv Address"
                                                                    TextMode="MultiLine" Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtSaddresspo" runat="server" CssClass="filter_item" ToolTip="Site Address"
                                                                    TextMode="MultiLine" Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label21po" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:Label ID="lblinvadd2po" runat="server" CssClass="peslbl1" Width="220px" Text="INV ADDRESS2 :"></asp:Label>
                                                                <asp:Label ID="Label22po" runat="server" CssClass="peslbl1" Text="SITE ADDRESS2 :"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label23po" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:TextBox ID="txtinvadd2po" runat="server" CssClass="filter_item" ToolTip="Inv Address"
                                                                    TextMode="MultiLine" Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtaddress2po" runat="server" CssClass="filter_item" ToolTip="Site Address"
                                                                    TextMode="MultiLine" Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label24po" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:Label ID="Label11po" runat="server" CssClass="peslbl1" Width="220px" Text="GST No :"></asp:Label>
                                                                <asp:Label ID="Label25po" runat="server" CssClass="peslbl1" Text="Contact Person :"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label26po" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:TextBox ID="txtinvgstpo" runat="server" CssClass="filter_item" ToolTip="Inv GST No"
                                                                    Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtCPersonpo" runat="server" CssClass="filter_item" ToolTip="Contact Person"
                                                                    Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label27po" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:Label ID="lblinvmobilenopo" runat="server" CssClass="peslbl1" Width="220px"
                                                                    Text="Mobile No :"></asp:Label>
                                                                <asp:Label ID="Label28po" runat="server" CssClass="peslbl1" Text="Mobile Number :"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label29po" CssClass="peslblfooter" runat="server" Width="180px"></asp:Label>
                                                                <asp:TextBox ID="txtinvmobilenopo" runat="server" CssClass="filter_item" ToolTip="Invoice Mobile Number"
                                                                    Width="200px" Height="22px">
                                                                </asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtMobileNumpo" runat="server" CssClass="filter_item" ToolTip="Mobile Number"
                                                                    Width="200px" Height="22px" onkeypress="javascript:return IsNumeric(event);">
                                                                </asp:TextBox>
                                                                <br />
                                                                <asp:Label ID="Label17po" CssClass="peslblfooter" runat="server" Text=""> Authorized Signatory</asp:Label>
                                                                <br />
                                                                &nbsp;&nbsp;&nbsp;(<asp:Label ID="lblpurchasemanagernamepo" runat="server" Style="vertical-align: middle"
                                                                    Text=""></asp:Label>)
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="3">
                                                                <asp:Label ID="Label30po" runat="server" CssClass="peslblfooter" Text="*    It is an electronically generated DO
                                                                " Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" class="pestbl">
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:Button ID="btnupdateprintpo" Width="120px" Height="20px" runat="server" Text="Confirm"
                                                        CssClass="updatebtn" OnClientClick="javascript:return confirmvalidatepo()" OnClick="btnupdateprintpo_Click" />
                                                    <asp:Button ID="btncancelprintpo" Width="120px" Height="20px" runat="server" Text="Cancel"
                                                        CssClass="cancelbtn" OnClick="btncancelprintpo_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <cc1:CalendarExtender ID="CalendarExtender2po" runat="server" TargetControlID="txtrefdatepo"
                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                    PopupButtonID="txtrefdatepo" OnClientDateSelectionChanged="checkDaterefpo">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender1po" runat="server" TargetControlID="txtpodatepo"
                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                    PopupButtonID="txtpodatepo" OnClientDateSelectionChanged="checkDatepopo">
                </cc1:CalendarExtender>
                <%--    <cc1:CalendarExtender ID="CalendarExtender3po" runat="server" TargetControlID="txtrecieved_datepo"
                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                    PopupButtonID="txtrecieved_datepo">
                </cc1:CalendarExtender>--%>
            </td>
        </tr>
    </table>
    <cc1:ModalPopupExtender ID="popitems" BehaviorID="mdlitems" runat="server" TargetControlID="btnModalPopUp"
        PopupControlID="pnlindent" BackgroundCssClass="modalBackground1" DropShadow="false" />
    <asp:Panel ID="pnlindent" runat="server" Style="display: none;">
        <table width="700px" border="0" align="center" cellpadding="0" cellspacing="0">
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
                                <table style="vertical-align: middle;" align="center" id="trgrid" runat="server">
                                    <tr>
                                        <td style="height: 15px;">
                                        </td>
                                    </tr>
                                    <tr align="center" id="popindentno" runat="server">
                                        <td style="width: 670px;">
                                            <asp:Label ID="lblidentno" runat="server" CssClass="red" Text="Indent No:" Height="20px"></asp:Label>
                                            &nbsp&nbsp&nbsp&nbsp
                                            <asp:TextBox ID="txtindent" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 15px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="Grditemspopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                DataKeyNames="id" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Serial No.</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id" Visible="false" />
                                                    <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                    <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="100px" />
                                                    <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                    <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                    <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" ItemStyle-Width="80px" />
                                                    <asp:BoundField DataField="basic_price" HeaderText="Basic Price" ItemStyle-Width="25px" />
                                                    <asp:BoundField DataField="units" HeaderText="Units" />
                                                    <asp:BoundField DataField="quantity" HeaderText="Requested Qty" ItemStyle-Width="25px" />
                                                    <asp:TemplateField HeaderText=" PO Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtqty" runat="server" onblur="Checkqty1();" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
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
                                        <td style="height: 20px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="vertical-align: middle;" align="center" id="trremarks" runat="server">
                                                <tr style="outline-width: thin; outline-style: solid;" valign="middle" id="trskremarks"
                                                    runat="server">
                                                    <td align="left">
                                                        <asp:Label ID="lbl1" runat="server" Width="25px"></asp:Label>
                                                        <asp:Label ID="lblskremark" runat="server" Text="Store Keeper:" Width="150px" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="LblSkremarks" Font-Size="Small" Width="450px" MaxLength="50"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="Lblskname" runat="server" Font-Size="x-small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="outline-width: thin; outline-style: solid;" valign="middle" id="trpmremarks"
                                                    runat="server">
                                                    <td align="left">
                                                        <asp:Label ID="lbl2" runat="server" Width="25px"></asp:Label>
                                                        <asp:Label ID="lblpmremark" runat="server" Text="Project Manager:" Width="150px"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="LblPmremarks" Font-Size="Small" Width="450px" MaxLength="50"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="Lblpmname" runat="server" Font-Size="x-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="outline-width: thin; outline-style: solid;" valign="middle" id="trcskremarks"
                                                    runat="server">
                                                    <td align="left">
                                                        <asp:Label ID="lbl3" runat="server" Width="25px"></asp:Label>
                                                        <asp:Label ID="lblcskremark" runat="server" Text="Central Store Keeper:" Width="150px"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="LblCskremarks" Font-Size="Small" Width="450px" MaxLength="50"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="Lblcskname" runat="server" Font-Size="x-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="outline-width: thin; outline-style: solid;" valign="middle" id="trpuremarks"
                                                    runat="server">
                                                    <td align="left">
                                                        <asp:Label ID="lbl4" runat="server" Width="25px"></asp:Label>
                                                        <asp:Label ID="lblpurmremark1" runat="server" Text="Purchase Manager:" Width="150px"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Lblpurmremark" Font-Size="Small" Width="450px" MaxLength="50"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="Lblpurname" runat="server" Font-Size="x-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="outline-width: thin; outline-style: solid;" valign="middle" id="trcmcremarks"
                                                    runat="server">
                                                    <td align="left">
                                                        <asp:Label ID="lbl5" runat="server" Width="25px"></asp:Label>
                                                        <asp:Label ID="lblcmcremark" runat="server" CssClass="char" Text="Chief Material Controller:"
                                                            Width="150px" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="LdlCmcremarks" Font-Size="Small" Width="450px" MaxLength="50"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="Lblcmcname" runat="server" Font-Size="x-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="outline-width: thin; outline-style: solid;" valign="middle" id="trsaremarks"
                                                    runat="server">
                                                    <td align="left">
                                                        <asp:Label ID="lbl6" runat="server" Width="25px"></asp:Label>
                                                        <asp:Label ID="lblsaremark" runat="server" CssClass="char" Text="Super Admin:" Width="150px"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="LblSaremarks" Font-Size="Small" Width="450px" MaxLength="50"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="Lblsaname" runat="server" Font-Size="x-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px;">
                                        </td>
                                    </tr>
                                    <tr id="button" runat="server">
                                        <td align="center">
                                            <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                            <asp:Button ID="btnmdlupd" runat="server" Text="Save" CssClass="button" OnClick="btnmdlupd_Click"
                                                OnClientClick="javascript:return validate1();" />
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblbtns" runat="server" align="center">
                                    <tr id="trheading" runat="server">
                                        <td style="color: #000000; font-size: 15px; font-style: normal; font-weight: normal;">
                                            <asp:Label ID="Label13" runat="server" CssClass="red" Text="" Font-Size="Smaller"
                                                ForeColor="#CC3300"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 15px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 15px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr id="trpartind" runat="server">
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnremaindmelater" runat="server" Text="Remaind me later" CssClass="button"
                                                OnClick="btnremaindmelater_Click" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnyesnew" runat="server" Text="Yes" CssClass="button" OnClientClick="hidepopup();" />&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr id="trpartind1" runat="server">
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnyes" runat="server" Text="Yes" CssClass="button" OnClick="btnyes_Click" />
                                            <asp:Button ID="btnno" runat="server" Text="No" CssClass="button" OnClientClick="return showtr();" />
                                        </td>
                                    </tr>
                                    <tr id="trcancelind" runat="server">
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btncancel" runat="server" Text="Yes" CssClass="button" OnClick="btncancel_Click" />
                                            <asp:Button ID="buttonno" runat="server" Text="No" CssClass="button" OnClientClick="hidepopup();" />
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
        </table>
        <script type="text/javascript">
            function Calculate() {
                //debugger;
                grd = document.getElementById("<%=grdbillpo.ClientID %>");
                var total = 0;
                var amt = 0;
                if (grd != null) {
                    for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                        if (Number(grd.rows(rowCount).cells(4).innerHTML) != 0.00) {
                            if (!isNaN(grd.rows(rowCount).cells(4).innerHTML)) {
                                amt = (Number(grd.rows(rowCount).cells(4).innerHTML)) * (Number(grd.rows(rowCount).cells(8).children[0].value));
                                grd.rows(rowCount).cells(9).innerHTML = Math.round(amt * Math.pow(10, 2)) / Math.pow(10, 2);
                                total += Number(grd.rows(rowCount).cells(9).innerHTML);
                            }
                        }
                        else {
                            total += Number(grd.rows(rowCount).cells(9).innerHTML);
                        }

                    }
                    grd.rows[grd.rows.length - 1].cells[9].innerHTML = total;
                    document.getElementById("<%=hftotalpo.ClientID %>").value = total;
                }
            }
            function chksplcharactersdesc() {
                //debugger;
                var gvdterms = document.getElementById("<%=grdterms.ClientID %>");
                if (gvdterms != null) {
                    for (var rowCount = 1; rowCount < gvdterms.rows.length - 1; rowCount++) {
                        if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
                            if (gvdterms.rows(rowCount).cells(2).children[0].value != "") {
                                if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                                    gvdterms.rows(rowCount).cells(2).children[0].value = "";
                                    window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                                    return false;
                                }
                                if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                                    gvdterms.rows(rowCount).cells(2).children[0].value = "";
                                    window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            function isNumberKey(evt) {
                //debugger;
                grd = document.getElementById("<%=grdterms.ClientID %>");
                if (grd != null) {
                    for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                        var charCode = (evt.which) ? evt.which : evt.keyCode;
                        if (charCode == 36) {
                            grd.rows(rowCount).cells(2).children[0].value.replace('$', '');
                            alert('Dollar($) not allowed');
                            return false;
                        }
                        if (event.keyCode == 39) {
                            grd.rows(rowCount).cells(2).children[0].value.replace('$', '');
                            event.keyCode = 0;
                        }
                        else {
                            return true;
                        }
                    }
                }
            }
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
    </asp:Panel>
</asp:Content>
