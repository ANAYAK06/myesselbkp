<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmpendinginvoice.aspx.cs"
    Inherits="frmpendinginvoice" EnableEventValidation="false" Title="Pending Invoice - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script src="Java_Script/validations.js" type="text/javascript"></script>
    <script language="javascript">
        function SPPOTax() {

            if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Service Provider") {

                if (document.getElementById("<%=hfspccsubtype.ClientID%>").value == "Service" && document.getElementById("<%=hfspcctype.ClientID%>").value == "Performing") {
                    var response = confirm("Do you want to continue with Service Tax");
                    if (response) {
                        document.getElementById("<%=ddlspservice.ClientID %>").disabled = false;
                        document.getElementById("<%=txtspservice.ClientID%>").disabled = false;
                        document.getElementById("<%=ddlSPExcno.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspexices.ClientID%>").disabled = true;
                    }
                    else {
                        document.getElementById("<%=ddlspservice.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspservice.ClientID%>").disabled = true;
                        document.getElementById("<%=ddlSPExcno.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspexices.ClientID%>").disabled = true;
                        document.getElementById("<%=txtspedc.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspheds.ClientID%>").disabled = true;
                    }
                    var response = confirm("Do you want to continue with VAT Tax ");
                    if (response) {
                        document.getElementById("<%=ddlSPvatno.ClientID %>").disabled = false;
                        document.getElementById("<%=txtspsales.ClientID%>").disabled = false;
                    }
                    else {
                        document.getElementById("<%=ddlSPvatno.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspsales.ClientID%>").disabled = true;
                        return false;
                    }
                }

                else if (document.getElementById("<%=hfspccsubtype.ClientID%>").value != "Service" && document.getElementById("<%=hfspcctype.ClientID%>").value == "Performing") {
                    var response = confirm("Do you want to continue with Excise Tax");
                    if (response) {
                        document.getElementById("<%=ddlspservice.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspservice.ClientID%>").disabled = true;
                        document.getElementById("<%=ddlSPExcno.ClientID %>").disabled = false;
                        document.getElementById("<%=txtspexices.ClientID%>").disabled = false;
                    }
                    else {
                        document.getElementById("<%=ddlspservice.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspservice.ClientID%>").disabled = true;
                        document.getElementById("<%=ddlSPExcno.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspexices.ClientID%>").disabled = true;
                        document.getElementById("<%=txtspedc.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspheds.ClientID%>").disabled = true;
                    }
                    var response = confirm("Do you want to continue with Vat Tax ");
                    if (response) {
                        document.getElementById("<%=ddlSPvatno.ClientID %>").disabled = false;
                        document.getElementById("<%=txtspsales.ClientID%>").disabled = false;
                    }
                    else {
                        document.getElementById("<%=ddlSPvatno.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspsales.ClientID%>").disabled = true;
                        return false;
                    }
                }

                else if (document.getElementById("<%=hfspcctype.ClientID%>").value == "Non-Performing" || document.getElementById("<%=hfspcctype.ClientID%>").value == "Capital") {

                    var response = confirm("Do you want  to continue with Service Tax");
                    if (response) {
                        document.getElementById("<%=ddlspservice.ClientID %>").disabled = false;
                        document.getElementById("<%=txtspservice.ClientID%>").disabled = false;
                        document.getElementById("<%=ddlSPExcno.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspexices.ClientID%>").disabled = true;
                    }
                    else {
                        var response = confirm("Do you want to continue with Excise Tax");
                        if (response) {
                            document.getElementById("<%=ddlspservice.ClientID %>").disabled = true;
                            document.getElementById("<%=txtspservice.ClientID%>").disabled = true;
                            document.getElementById("<%=ddlSPExcno.ClientID %>").disabled = false;
                            document.getElementById("<%=txtspexices.ClientID%>").disabled = false;
                        }
                        else {
                            document.getElementById("<%=ddlspservice.ClientID %>").disabled = true;
                            document.getElementById("<%=txtspservice.ClientID%>").disabled = true;
                            document.getElementById("<%=ddlSPExcno.ClientID %>").disabled = true;
                            document.getElementById("<%=txtspexices.ClientID%>").disabled = true;
                            document.getElementById("<%=txtspedc.ClientID %>").disabled = true;
                            document.getElementById("<%=txtspheds.ClientID%>").disabled = true;
                        }
                    }
                    var response = confirm("Do you want to continue with VAT Tax ");
                    if (response) {
                        document.getElementById("<%=ddlSPvatno.ClientID %>").disabled = false;
                        document.getElementById("<%=txtspsales.ClientID%>").disabled = false;
                        return false;
                    }
                    else {
                        document.getElementById("<%=ddlSPvatno.ClientID %>").disabled = true;
                        document.getElementById("<%=txtspsales.ClientID%>").disabled = true;
                        return false;
                    }
                }

            }
        }
        function verification() {
            if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Trading Supply" || document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Manufacturing") {
                var excise = document.getElementById("<%=ddlExcno.ClientID %>").value;
                var vat = document.getElementById("<%=ddlvatno.ClientID %>").value;
                var i = 0;
                var k = 0;
                if (excise == "Select" && document.getElementById("<%=ddlExcno.ClientID %>").disabled == false) {
                    var j = 0;
                    i = j + 1;
                    var response = confirm("Is it Excise Payment");
                    if (response) {
                        alert("Excise No Required");
                        document.getElementById("<%=ddlExcno.ClientID %>").disabled = false;
                        document.getElementById("<%=ddlExcno.ClientID %>").focus();

                    }
                    else {
                        document.getElementById("<%=ddlExcno.ClientID %>").disabled = true;
                        document.getElementById("<%=txtmex.ClientID %>").disabled = true;
                        document.getElementById("<%=txtmed.ClientID %>").disabled = true;
                        document.getElementById("<%=txtmhed.ClientID %>").disabled = true;
                        if (vat == "Select" && document.getElementById("<%=ddlvatno.ClientID %>").disabled == false) {
                            vatvalidate();
                        }

                    }

                }

            }

        }
    </script>
    <script language="javascript">
        function vatvalidate() {
            var excise = document.getElementById("<%=ddlExcno.ClientID %>").value;
            var vat = document.getElementById("<%=ddlvatno.ClientID %>").value;
            if (vat == "Select" && document.getElementById("<%=ddlvatno.ClientID %>").disabled == false) {
                var l = 0;
                k = l + 1;
                var response = confirm("Is it Vat Payment");
                if (response) {
                    alert("Vat No Required");
                    document.getElementById("<%=ddlvatno.ClientID %>").disabled = false;
                    document.getElementById("<%=ddlvatno.ClientID %>").focus();
                    return false;
                }
                else {
                    document.getElementById("<%=ddlvatno.ClientID %>").disabled = true;
                    document.getElementById("<%=txtmtax.ClientID %>").disabled = true;
                }
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function checkadvance() {
            var totaldeduct = document.getElementById("<%=hftotaldeduct.ClientID %>").value;
            var totalcredit = document.getElementById("<%=hftotalcredit.ClientID %>").value;
            var advance = document.getElementById("<%=txtadvance.ClientID %>").value;
            var total = parseFloat(totaldeduct) + parseFloat(advance)
            if (totalcredit < total) {
                alert("You are excessing the Recieved Advance");
                document.getElementById("<%=txtadvance.ClientID %>").focus();
                document.getElementById("<%=txtadvance.ClientID %>").value = "";
                return false;
            }
        }

        function validate() {
            if (!ChceckRBL("<%=rbtntype.ClientID %>"))
                return false;

            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {

                var objs = new Array("<%=ddltypeofpay.ClientID %>", "<%=ddlvendor.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=txtdate.ClientID %>"
                        , "<%=ddlpo.ClientID %>", "<%=ddlsubclientid.ClientID %>", "<%=txtin.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>", "<%=txtSPbasic.ClientID %>", "<%=txtdesc.ClientID %>", "<%=ddlExcno.ClientID %>", "<%=ddlservicetax.ClientID %>");

            }
            else {
                var type = document.getElementById("<%=ddltypeofpay.ClientID %>").value;
                if (type == "Invoice Service") {
                    var objs = new Array("<%=ddltypeofpay.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlclientid.ClientID %>", "<%=ddlservicetax.ClientID %>"
                        , "<%=ddlpono.ClientID %>", "<%=ddlsubclientid.ClientID %>", "<%=txtin.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>", "<%=txtra.ClientID %>", "<%=txtbasic.ClientID %>", "<%=txtdesc.ClientID %>");
                }
                else {
                    var objs = new Array("<%=ddltypeofpay.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlclientid.ClientID %>"
                        , "<%=ddlpono.ClientID %>", "<%=ddlsubclientid.ClientID %>", "<%=txtin.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>", "<%=txtmra.ClientID %>", "<%=txtmbasic.ClientID %>", "<%=txtdesc.ClientID %>");

                }

            }

            if (!CheckInputs(objs)) {
                return false;
            }
            if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Service Provider") {
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

                var netamount = document.getElementById("<%= txtamt.ClientID%>").value;
                if (netamount < 0) {
                    alert("You are excessing the Invoice Amount");
                    return false;
                }
            }


            if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Trading Supply" || document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Manufacturing") {
                var excise = document.getElementById("<%=ddlExcno.ClientID %>").value;
                var vat = document.getElementById("<%=ddlvatno.ClientID %>").value;
                if (excise == "Select" && document.getElementById("<%=ddlExcno.ClientID %>").disabled == false) {
                    alert("Excise No Required");
                    document.getElementById("<%=ddlExcno.ClientID %>").focus();
                    return false;
                }
                else if (vat == "Select" && document.getElementById("<%=ddlvatno.ClientID %>").disabled == false) {
                    alert("VAT No Required");
                    document.getElementById("<%=ddlvatno.ClientID %>").focus();
                    return false;
                }
                else if (document.getElementById("<%=ddlvatno.ClientID %>").disabled == false && document.getElementById("<%=txtmtax.ClientID %>").value == "") {
                    alert("VAT value Required");
                    document.getElementById("<%=txtmtax.ClientID %>").focus();
                    return false;
                }
                else if (document.getElementById("<%=ddlExcno.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtmex.ClientID %>").value == "") {
                        alert("Excise value Required");
                        document.getElementById("<%=txtmex.ClientID %>").focus();
                        return false;
                    }
                    else if (document.getElementById("<%=txtmed.ClientID %>").value == "") {
                        alert("Edcess Required");
                        document.getElementById("<%=txtmed.ClientID %>").focus();
                        return false;
                    }
                    else if (document.getElementById("<%=txtmhed.ClientID %>").value == "") {
                        alert("Hedcess Required");
                        document.getElementById("<%=txtmhed.ClientID %>").focus();
                        return false;
                    }
                }
            }
            if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Invoice Service") {
                if (document.getElementById("<%=ddltype.ClientID %>").value == "Service Tax Invoice") {
                    if (document.getElementById("<%=txttax.ClientID %>").value == "") {
                        alert("ServiceTax value Required");
                        document.getElementById("<%=txttax.ClientID %>").focus();
                        return false;
                    }
                    else if (document.getElementById("<%=txted.ClientID %>").value == "") {
                        alert("Edcess Required");
                        document.getElementById("<%=txted.ClientID %>").focus();
                        return false;
                    }
                    else if (document.getElementById("<%=txthed.ClientID %>").value == "") {
                        alert("Hedcess Required");
                        document.getElementById("<%=txthed.ClientID %>").focus();
                        return false;
                    }
                }
                if (document.getElementById("<%=ddltype.ClientID %>").value == "VAT/Material Supply") {
                    var vat = document.getElementById("<%=ddlservicetax.ClientID %>").value;
                    if (vat == "Select" && document.getElementById("<%=ddlservicetax.ClientID %>").disabled == false) {
                        alert("VAT No Required");
                        document.getElementById("<%=ddlservicetax.ClientID %>").focus();
                        return false;
                    }
                    else if (document.getElementById("<%=ddlservicetax.ClientID %>").disabled == false && document.getElementById("<%=txtmtax.ClientID %>").value == "") {
                        alert("VAT value Required");
                        document.getElementById("<%=txtmtax.ClientID %>").focus();
                        return false;
                    }
                }
            }
            if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Invoice Service") {
                if (document.getElementById("<%=ddltype.ClientID %>").value == "Select") {
                    alert("Select service type");
                    document.getElementById("<%=ddltype.ClientID %>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Service Provider") {
                var str1 = document.getElementById("<%=hfdate.ClientID %>").value;

                var str2 = document.getElementById("<%=txtindt.ClientID %>").value;
                var Pobalance = document.getElementById("<%=hfbalance.ClientID %>").value;
                var basic = document.getElementById("<%=txtSPbasic.ClientID %>").value;
                //                var str3 = document.getElementById('txtdate').value;
                var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");

                var dt1 = str1.substring(0, 2);
                var dt2 = str2.substring(0, 2);
                var yr1 = str1.substring(7, 11);
                var yr2 = str2.substring(7, 11);
                for (var i = 0; i < args.length; i++) {
                    var month = str2.substring(3, 6);
                    var month1 = str1.substring(3, 6);
                    if (args[i] == month) {
                        var month = parseFloat(i + 1);
                        var date2 = yr2 + "-" + month + "-" + dt2;

                    }
                    if (args[i] == month1) {
                        var month1 = parseFloat(i + 1);
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
                if (parseFloat(_Diff) < 0) {
                    alert("Invoice date should be after the PO date");
                    document.getElementById("<%=txtindt.ClientID %>").focus();
                    return false;
                }
                if (parseFloat(Pobalance) < parseFloat(basic)) {
                    alert("Insufficient PO Balance");
                    return false;
                }
                if (document.getElementById("<%=ddlSPExcno.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=ddlSPExcno.ClientID %>").value == "Select") {
                        alert(" Select  Exices No Required");
                        document.getElementById("<%=ddlSPExcno.ClientID %>").focus();
                        return false;
                    }
                }
                if (document.getElementById("<%=txtspexices.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtspexices.ClientID %>").value == "") {
                        alert("Exicse Duty Required");
                        document.getElementById("<%=txtspexices.ClientID %>").focus();
                        return false;
                    }
                }
                if (document.getElementById("<%=ddlspservice.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=ddlspservice.ClientID %>").value == "Select") {
                        alert(" Select  Service Tax No Required");
                        document.getElementById("<%=ddlspservice.ClientID %>").focus();
                        return false;
                    }
                }
                if (document.getElementById("<%=txtspservice.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtspservice.ClientID %>").value == "") {
                        alert("Service Tax Required");
                        document.getElementById("<%=txtspservice.ClientID %>").focus();
                        return false;
                    }
                }
                if (document.getElementById("<%=txtspedc.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtspedc.ClientID %>").value == "") {
                        alert("Edcess  Required");
                        document.getElementById("<%=txtspedc.ClientID %>").focus();
                        return false;
                    }
                }
                if (document.getElementById("<%=txtspheds.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtspheds.ClientID %>").value == "") {
                        alert("EHdcess  Required");
                        document.getElementById("<%=txtspheds.ClientID %>").focus();
                        return false;
                    }
                }
                if (document.getElementById("<%=ddlSPvatno.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=ddlSPvatno.ClientID %>").value == "Select") {
                        alert(" Select VAT No Required");
                        document.getElementById("<%=ddlSPvatno.ClientID %>").focus();
                        return false;
                    }
                }
                if (document.getElementById("<%=txtspsales.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtspsales.ClientID %>").value == "") {
                        alert("VAT Tax Required");
                        document.getElementById("<%=txtspsales.ClientID %>").focus();
                        return false;
                    }
                }
            }

            if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Salary") {

                GridView2 = document.getElementById("<%=GridView1.ClientID %>");
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    var j = 0;
                    if (GridView2.rows(rowCount).cells(1).children(0).selectedIndex == 0) {
                        j = j + 1;
                    }
                    if (GridView2.rows(rowCount).cells(2).children(0).selectedIndex == 0) {
                        j = j + 1;
                    }
                    if (GridView2.rows(rowCount).cells(3).children(0).value == 0) {
                        j = j + 1;
                    }
                    if (j == 1 || j == 2 || j == 3) {
                        if (rowCount == 1) {
                            if (GridView2.rows(rowCount).cells(1).children(0).selectedIndex == 0) {
                                alert("Please Select Cost Center");
                                GridView2.rows(rowCount).cells(1).children(0).focus();
                                return false;
                            }
                            else if (GridView2.rows(rowCount).cells(2).children(0).selectedIndex == 0) {
                                alert("Please Select Bank");
                                GridView2.rows(rowCount).cells(2).children(0).focus();
                                return false;
                            }
                            else if (GridView2.rows(rowCount).cells(3).children(0).value == 0) {
                                alert("Please Enter Amount");
                                GridView2.rows(rowCount).cells(3).children(0).focus();
                                return false;
                            }
                        }

                    }
                }
            }

            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }

        function isrepeted() {
            gridview = document.getElementById("<%=GridView1.ClientID %>");
            var sel = "|";
            var cel = "|";
            for (var i = 1; i < gridview.rows.length - 1; i++) {
                var idx = gridview.rows[i].cells[1].children[0].selectedIndex;
                var idy = gridview.rows[i].cells[2].children[0].selectedIndex;
                if (idy != 0) {
                    if (i >= 2) {
                        if ((idx != 0) && (idy != 0) && (gridview.rows[i - 1].cells[3].children[0].value == "")) {
                            alert("Please Insert Amount For Cost Center" + gridview.rows[i - 1].cells[1].children[0].value + " and Bank  " + gridview.rows[i - 1].cells[2].children[0].value);
                            gridview.rows[i - 1].cells[3].children[0].focus();
                            return false;
                        }
                    }
                    if (sel.indexOf("|" + idx + idy + "|") == -1) {
                        sel = sel + idx + idy + "|";
                    }
                    else {
                        alert(gridview.rows[i].cells[2].children[0].value + " Already Selected to " + gridview.rows[i].cells[1].children[0].value)
                        gridview.rows[i].cells[2].children[0].focus();
                        gridview.rows[i].cells[2].children[0].selectedIndex = 0;
                        return false;
                    }
                }
            }
            return true;
        }
        function valid() {
            var typeofpay = document.getElementById("<%=ddltypeofpay.ClientID %>").value;
            if (typeofpay == "Service Provider") {
                var tax = document.getElementById("<%=txttax.ClientID %>").value;
                var basic = document.getElementById("<%=txtSPbasic.ClientID %>").value
                if (tax != "" && parseFloat(basic) != 0) {
                    alert("You Should make Basic Value Zero");
                    document.getElementById("<%=txtSPbasic.ClientID %>").value = 0;
                    Total();
                    return false;
                }

            }


        }
        function CleartextBoxes(sType) {
            a = document.getElementsByTagName("input");
            for (i = 0; i < a.length; i++) {
                if (a[i].type == sType) {
                    a[i].value = "";
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
        function checkDatepaybill(sender, args) {
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
                                        <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table class="estbl eslbl" width="750px">
                            <tr>
                                <th align="center">
                                    Bank Flow Form
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <table class="innertab" align="center">
                                        <tr>
                                            <td>
                                                Transaction Type:
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Transction Type"
                                                    AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                    CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged" onclick="CleartextBoxes('text');">
                                                    <asp:ListItem>Credit</asp:ListItem>
                                                    <asp:ListItem>Debit</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="paytype" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td style="width: 250px;">
                                                Category of Payment:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddltypeofpay" runat="server" ToolTip="Type of Payment" AutoPostBack="true"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddltypeofpay_SelectedIndexChanged"
                                                    onchange="CleartextBoxes('text');">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="200px" CssClass="esddown"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Service Tax Invoice</asp:ListItem>
                                                    <asp:ListItem>SEZ/Service Tax exumpted Invoice</asp:ListItem>
                                                    <asp:ListItem>VAT/Material Supply</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlvendor" Width="250px" AutoPostBack="true" CssClass="esddown"
                                                    ToolTip="Select Vendor" runat="server" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hfspcctype" runat="server" />
                                                <asp:HiddenField ID="hfspccsubtype" runat="server" />
                                                <asp:DropDownList ID="ddlpo" runat="server" AutoPostBack="true" ToolTip="Po" Width="105px"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddlpo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdebit" runat="server" style="height: 40px;">
                                <td>
                                    <table class="innertab" width="100%" runat="server" id="cc">
                                        <tr>
                                            <td>
                                                CC-Code:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="150px"
                                                    Onchange="verification();" CssClass="esddown" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Client ID:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlclientid" onchange="SetDynamicKey('dp3',this.value);" runat="server"
                                                    ToolTip="ClientID" Width="105px" CssClass="esddown">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown7" runat="server" TargetControlID="ddlclientid"
                                                    ServicePath="cascadingDCA.asmx" Category="client" LoadingText="Please Wait" ServiceMethod="clientid"
                                                    PromptText="Select Client Id">
                                                </cc1:CascadingDropDown>
                                                <br />
                                                <asp:Label ID="lblcccode" class="ajaxspan" runat="server"></asp:Label>
                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp3" runat="server"
                                                    TargetControlID="lblcccode" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                    ServiceMethod="GetClientName">
                                                </cc1:DynamicPopulateExtender>
                                            </td>
                                            <td colspan="2">
                                                Subclient ID:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlsubclientid" onchange="SetDynamicKey('dp9',this.value);"
                                                    runat="server" ToolTip="ClientID" Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown8" runat="server" TargetControlID="ddlsubclientid"
                                                    ServicePath="cascadingDCA.asmx" ParentControlID="ddlclientid" Category="subclient"
                                                    LoadingText="Please Wait" ServiceMethod="subclientid" PromptText="Select SubClient">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="Label8" class="ajaxspan" runat="server"></asp:Label>
                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp9" runat="server"
                                                    TargetControlID="txtname" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                    ServiceMethod="GetSubClientName">
                                                </cc1:DynamicPopulateExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Srtxregno" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td align="center" style="width: 130px;" id="Td1" runat="server">
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                            <td align="center" style="width: 200px;">
                                                <asp:DropDownList ID="ddlservicetax" runat="server" ToolTip="ServiceTax No" Width="200px"
                                                    CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td align="center" style="width: 130px;" id="lblex" runat="server">
                                                Excise No :
                                            </td>
                                            <td align="center" style="width: 200px;">
                                                <asp:DropDownList ID="ddlExcno" runat="server" ToolTip="Excise No" Width="200px"
                                                    onchange="vatvalidate();" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 130px;" id="lblvat" runat="server">
                                                <asp:Label ID="Label18" runat="server" CssClass="eslbl" Text="VAT NO:"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 200px;">
                                                <asp:DropDownList ID="ddlvatno" runat="server" Width="200px" ToolTip="Vat No" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trcredit" runat="server">
                                <td>
                                    <table class="innertab" width="100%" runat="server" id="Table1">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblccode" Width="80px" Enabled="false" runat="server"> </asp:TextBox>
                                            </td>
                                            <td width="400px">
                                                <table width="100%" class="innertab" runat="server" id="Table2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="DCA"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lbldcacode" Width="80px" Enabled="false" runat="server"> </asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="Sub DCA"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblsdcacode" Width="80px" Enabled="false" runat="server"> </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Invoice" runat="server">
                                <td>
                                    <table class="" runat="server" style="border-style: hidden;" width="100%">
                                        <tr id="tr2" runat="server">
                                            <td colspan="6">
                                                <table width="100%" style="border-style: hidden;">
                                                    <tr style="border-style: hidden;">
                                                        <td style="border-style: hidden; font-weight: normal">
                                                            <asp:Label ID="lblpo1" runat="server" Width="75px" Font-Size="Small" Text="PO NO:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtpo" CssClass="estbox" runat="server" ToolTip="Po No" Width="100px"></asp:TextBox>
                                                            <br />
                                                            <asp:DropDownList ID="ddlpono" runat="server" CssClass="esddown" ToolTip="Po No"
                                                                Width="100px" OnSelectedIndexChanged="ddlpono_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-style: hidden; font-weight: normal">
                                                            Invoice No:
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtin" CssClass="estbox" runat="server" ToolTip="Invoice No" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden; font-weight: normal">
                                                            Invoice Date:
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtindt" CssClass="estbox" runat="server" onKeyDown="preventBackspace();"
                                                                onpaste="return false;" onkeypress="return false;" ToolTip="Invoice Date" Width="85px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindt"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                OnClientDateSelectionChanged="checkDate" PopupButtonID="txtindt">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="border-style: hidden; font-weight: normal">
                                                            Inv Making Date:
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtindtmk" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                onkeypress="return false;" runat="server" ToolTip="Invoice Making Date" Width="85px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtindtmk"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                PopupButtonID="txtindtmk">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trnonmanufaturing" runat="server">
                                            <td colspan="6">
                                                <table width="100%" style="border-style: hidden;">
                                                    <tr id="basic" runat="server">
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="lblrano" CssClass="eslbl" runat="server" Text="RA No:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtra" CssClass="estbox" runat="server" ToolTip="RA No:" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Basic Value"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtbasic" CssClass="estbox" runat="server" ToolTip="Basic Value"
                                                                Width="100px" onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="lbltax" CssClass="eslbl" runat="server" Text="Service Tax:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txttax" CssClass="estbox" runat="server" ToolTip="Tax" Width="100px"
                                                                onkeyup="Total();" onkeydown="valid();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="ex" runat="server">
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Excise duty"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtex" CssClass="estbox" runat="server" ToolTip="Excise duty" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Freight"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtfre" CssClass="estbox" runat="server" ToolTip="Freight" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="Insurance"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtins" CssClass="estbox" runat="server" ToolTip="Insurance" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="SPBasic" runat="server" visible="True">
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtSPbasic" CssClass="estbox" onkeyup="Total();" runat="server"
                                                                ToolTip="Basic Value" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                        </td>
                                                    </tr>
                                                    <tr id="ed" runat="server" visible="True">
                                                        <td style="border-style: hidden;" id="tded" runat="server">
                                                            <asp:Label ID="lblnewed" CssClass="eslbl" runat="server" Text="EDCess:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txted" CssClass="estbox" runat="server" ToolTip="EDCess" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;" id="tdhed" runat="server">
                                                            <asp:Label ID="lblnewhed" CssClass="eslbl" runat="server" Text="HEDCess:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txthed" CssClass="estbox" runat="server" ToolTip="HEDCess" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            Total:
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txttotal" CssClass="estbox" runat="server" ToolTip="Total" Width="100px"></asp:TextBox><span
                                                                id="spanAvailability" class="esspan"></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trmanufaturing" runat="server">
                                            <td colspan="6">
                                                <table style="border-style: hidden;" width="100%">
                                                    <tr id="Mbasic" runat="server" style="border-style: hidden;">
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="RA No:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmra" CssClass="estbox" runat="server" ToolTip="RA No:" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="Basic Value"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmbasic" CssClass="estbox" runat="server" ToolTip="Basic Value"
                                                                Width="100px" onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label13" CssClass="eslbl" runat="server" Text="Excise duty:"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmex" CssClass="estbox" runat="server" ToolTip="Excise duty" Width="100px"
                                                                onkeyup="Total();" onkeydown="valid();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="Mex" runat="server" style="border-style: hidden;">
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="EDCess"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmed" CssClass="estbox" runat="server" ToolTip="EDCess" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="HEDCess"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmhed" CssClass="estbox" runat="server" ToolTip="HEDCess" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="Sales/Vat Tax"></asp:Label>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmtax" CssClass="estbox" runat="server" ToolTip="Sales/Vat Tax"
                                                                Width="100px" onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="Med" runat="server" style="border-style: hidden;">
                                                        <td style="border-style: hidden;">
                                                            Freight:
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmfre" CssClass="estbox" runat="server" ToolTip="Freight" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            Insurance:
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmins" CssClass="estbox" runat="server" ToolTip="Insurance" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            Total:
                                                        </td>
                                                        <td style="border-style: hidden;">
                                                            <asp:TextBox ID="txtmtotal" CssClass="estbox" runat="server" ToolTip="Total" Width="100px"></asp:TextBox><span
                                                                id="span1" class="esspan"></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="sppoinvoice" runat="server">
                                            <td colspan="6">
                                                <table style="border-style: hidden;" width="100%">
                                                    <tr id="sppotax" runat="server">
                                                        <td id="Td2" align="center" style="width: 130px;" runat="server">
                                                            <asp:Label ID="lblspexiceno" CssClass="eslbl" runat="server" Text="Excise No:"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 200px;">
                                                            <asp:DropDownList ID="ddlSPExcno" runat="server" ToolTip="Excise No" Width="200px"
                                                                CssClass="esddown">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td id="Td4" align="center" style="width: 130px;" runat="server">
                                                            <asp:Label ID="lblspexise" CssClass="eslbl" runat="server" Text="Excise Duty :"></asp:Label>
                                                        </td>
                                                        <td id="Td5" align="center" style="width: 150px;" runat="server">
                                                            <asp:TextBox ID="txtspexices" CssClass="estbox" runat="server" ToolTip="Excise duty"
                                                                Width="100px" onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="spservice" runat="server">
                                                        <td id="Td8" align="center" style="width: 160px;" runat="server">
                                                            <asp:Label ID="lblspserviceno" CssClass="eslbl" runat="server" Text="Service Tax No :"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 200px;">
                                                            <asp:DropDownList ID="ddlspservice" runat="server" ToolTip="Service No" Width="200px"
                                                                CssClass="esddown">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td id="Td9" align="center" style="width: 130px;" runat="server">
                                                            <asp:Label ID="lblspservice" runat="server" CssClass="eslbl" Text="Service Tax:"></asp:Label>
                                                        </td>
                                                        <td id="Td10" align="center" style="width: 150px;" runat="server">
                                                            <asp:TextBox ID="txtspservice" CssClass="estbox" runat="server" ToolTip="service duty"
                                                                Width="100px" onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="spexice" runat="server">
                                                        <td id="Td3" align="center" style="width: 130px;" runat="server">
                                                            <asp:Label ID="lblspvat" runat="server" CssClass="eslbl" Text="VAT NO:"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 200px;">
                                                            <asp:DropDownList ID="ddlSPvatno" runat="server" Width="200px" ToolTip="Vat No" CssClass="esddown">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td id="Td6" align="center" style="width: 130px;" runat="server">
                                                            <asp:Label ID="lblsles" runat="server" CssClass="eslbl" Text="Vat Tax :"></asp:Label>
                                                        </td>
                                                        <td id="Td7" align="center" style="width: 150px;" runat="server">
                                                            <asp:TextBox ID="txtspsales" CssClass="estbox" runat="server" ToolTip="Sales/Vat Tax"
                                                                Width="100px" onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="spedess" runat="server">
                                                        <td id="Td11" align="center" style="width: 130px;" runat="server">
                                                            <asp:Label ID="lbleess" runat="server" CssClass="eslbl" Text="EDCess:"></asp:Label>
                                                        </td>
                                                        <td id="Td12" align="center" style="width: 150px;" runat="server">
                                                            <asp:TextBox ID="txtspedc" CssClass="estbox" runat="server" ToolTip="EDCess" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                        <td id="Td13" align="center" style="width: 130px;" runat="server">
                                                            <asp:Label ID="lblsphedes" runat="server" CssClass="eslbl" Text="HEDCess:"></asp:Label>
                                                        </td>
                                                        <td id="Td14" align="center" style="width: 150px;" runat="server">
                                                            <asp:TextBox ID="txtspheds" CssClass="estbox" runat="server" ToolTip="HEDCess" Width="100px"
                                                                onkeyup="Total();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="deductionhead" runat="server">
                                            <th colspan="6" align="center">
                                                Deductions
                                            </th>
                                        </tr>
                                        <tr id="deductionbody1" runat="server">
                                            <td style="border-style: hidden;">
                                                <asp:Label ID="lbltds" runat="server" CssClass="eslbl" Text="TDS:"></asp:Label>
                                            </td>
                                            <td style="border-style: hidden;">
                                                <asp:TextBox ID="txttds" CssClass="estbox" runat="server" ToolTip="TDS" Width="100px"
                                                    onkeyup="Total();"></asp:TextBox>
                                            </td>
                                            <td style="border-style: hidden;">
                                                <asp:Label ID="lblretention" runat="server" CssClass="eslbl" Text="Retention:"></asp:Label>
                                            </td>
                                            <td style="border-style: hidden;">
                                                <asp:TextBox ID="txtretention" CssClass="estbox" runat="server" ToolTip="Retention"
                                                    Width="100px" onkeyup="Total();"></asp:TextBox>
                                            </td>
                                            <td style="border-style: hidden;">
                                                <asp:Label ID="lbledc" runat="server" CssClass="eslbl" Text="EDC:"></asp:Label>
                                            </td>
                                            <td style="border-style: hidden;">
                                                <asp:TextBox ID="txtedc" CssClass="estbox" runat="server" ToolTip="EDC" Width="100px"
                                                    onkeyup="Total();"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="deductionbody2" runat="server">
                                            <td style="border-style: hidden;">
                                                Advance:
                                            </td>
                                            <td style="border-style: hidden;">
                                                <asp:HiddenField ID="hftotalcredit" runat="server" />
                                                <asp:HiddenField ID="hftotaldeduct" runat="server" />
                                                <asp:TextBox ID="txtadvance" CssClass="estbox" runat="server" ToolTip="Advance" Width="100px"
                                                    onkeyup="Total();" onblur="checkadvance();"></asp:TextBox>
                                            </td>
                                            <td style="border-style: hidden;">
                                                Hold:
                                            </td>
                                            <td style="border-style: hidden;">
                                                <asp:TextBox ID="txthold" CssClass="estbox" runat="server" ToolTip="Hold" Width="100px"
                                                    onkeyup="Total();"></asp:TextBox>
                                            </td>
                                            <td style="border-style: hidden;">
                                                Any Other:
                                            </td>
                                            <td style="border-style: hidden;">
                                                <asp:TextBox ID="txtother" CssClass="estbox" runat="server" ToolTip="Any Other" Width="100px"
                                                    onkeyup="Total();"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="paybill" runat="server">
                                <td>
                                    <table id="grid" runat="server" class="innertab" align="center">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Due Date:" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="Due Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true" OnClientDateSelectionChanged="checkDatepaybill"
                                                    PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:GridView CssClass="mGrid" PagerStyle-CssClass="pgr" ID="GridView1" runat="server"
                                                    AutoGenerateColumns="False" DataKeyNames="id" Width="400px" ShowFooter="True"
                                                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                                                    <FooterStyle Font-Bold="true" ForeColor="black" BackColor="White" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" HeaderStyle-BackColor="White"
                                                            ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlcccode1" runat="server" onchange="isrepeted();" CssClass="esddown">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode1"
                                                                    ServicePath="cascadingDCA.asmx" Category="ddd" LoadingText="Please Wait" ServiceMethod="costcode"
                                                                    PromptText="Select Cost Center">
                                                                </cc1:CascadingDropDown>
                                                                <br />
                                                                <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle ForeColor="White" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlfrom" runat="server" onchange="isrepeted();" ToolTip="Bank"
                                                                    CssClass="esddown" Width="100px">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                                    PromptText="Select">
                                                                </cc1:CascadingDropDown>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" FooterText="0">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtamount" runat="server" Text='<%#Bind("debit") %>' onkeyup="sumgridview();"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle ForeColor="black" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                            ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("CCCode")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("bank_name")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="name" align="center" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td>
                                                Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtname" CssClass="estbox" runat="server" ToolTip="Name" Width="200px"></asp:TextBox>
                                                <script type="text/javascript">
                                                    var vendorCheckerTimer;
                                                    var span2 = $get("span2");

                                                    function vendorchecker(cc_code) {

                                                        clearTimeout(vendorCheckerTimer);
                                                        if (cc_code.length == 0)

                                                            span2.innerHTML = "";
                                                        else {
                                                            //span2.innerHTML = "<span style='color: #ccc;'>Matching....</span>";
                                                            vendorCheckerTimer1 = setTimeout("vendorcodeUsage('" + cc_code + "');", 750);
                                                        }
                                                    }
                                                    function vendorcodeUsage(cc_code) {
                                                        // initiate the ajax pagemethod call
                                                        // upon completion, the OnSucceded callback will be executed
                                                        PageMethods.IsvendorAvailable(cc_code, OnSucceeded);
                                                    }

                                                    // Callback function invoked on successful completion of the page method.
                                                    function OnSucceeded(result, userContext, methodName) {
                                                        var txt = document.getElementById("<%=txtname.ClientID %>");
                                                        if (result == true) {
                                                            txt.value = result;
                                                        }
                                                        else {

                                                            txt.value = result;
                                                        }
                                                    }
                                                </script>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Description
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Net Amount
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                <asp:HiddenField ID="h1" runat="server" />
                                                <asp:HiddenField ID="hfdate" runat="server" />
                                                <asp:HiddenField ID="hfbalance" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="btn" runat="server">
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClick="btnsubmit_Click1" OnClientClick="javascript:return validate();" />
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
    <script language="javascript" type="text/javascript">

        function Total() {
            var category = document.getElementById("<%=ddltypeofpay.ClientID %>").value;
            var originalValue = 0;
            var originalValue1 = 0;
            if (category == "Manufacturing" || category == "Trading Supply") {

                var basic = document.getElementById("<%=txtmbasic.ClientID %>").value;
                var tax = document.getElementById("<%=txtmex.ClientID %>").value;
                var ed = document.getElementById("<%=txtmed.ClientID %>").value;
                var hed = document.getElementById("<%=txtmhed.ClientID %>").value;
                var stax = document.getElementById("<%=txtmtax.ClientID %>").value;
                var frieght = document.getElementById("<%=txtmfre.ClientID %>").value;
                var insurance = document.getElementById("<%=txtmins.ClientID %>").value;

                var tds = document.getElementById("<%=txttds.ClientID %>").value;
                var ret = document.getElementById("<%=txtretention.ClientID %>").value;
                var adv = document.getElementById("<%=txtadvance.ClientID %>").value;
                var hold = document.getElementById("<%=txthold.ClientID %>").value;
                var other = document.getElementById("<%=txtother.ClientID %>").value;

                if (basic == "") {
                    basic = 0;
                }
                if (tax == "") {
                    tax = 0;
                }
                if (ed == "") {
                    ed = 0;
                }
                if (hed == "") {
                    hed = 0;
                }
                if (stax == "") {
                    stax = 0;
                }
                if (frieght == "") {
                    frieght = 0;
                }
                if (insurance == "") {
                    insurance = 0;
                }
                if (tds == "") {
                    tds = 0;
                }
                if (ret == "") {
                    ret = 0;
                }
                if (adv == "") {
                    adv = 0;
                }
                if (hold == "") {
                    hold = 0;
                }
                if (other == "") {
                    other = 0;
                }
                originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(stax) + parseFloat(frieght) + parseFloat(insurance)));
                originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(stax) + parseFloat(frieght) + parseFloat(insurance)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(adv) + parseFloat(hold) + parseFloat(other))));

                var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById('<%= txtmtotal.ClientID%>').value = roundValue;
                document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;
                document.getElementById('<%= h1.ClientID%>').value = roundValue1;

            }
            else if (category == "Invoice Service") {
                var tds = document.getElementById("<%=txttds.ClientID %>").value;
                var ret = document.getElementById("<%=txtretention.ClientID %>").value;
                var adv = document.getElementById("<%=txtadvance.ClientID %>").value;
                var hold = document.getElementById("<%=txthold.ClientID %>").value;
                var other = document.getElementById("<%=txtother.ClientID %>").value;

                if (document.getElementById("<%=ddltype.ClientID %>").value == "VAT/Material Supply") {
                    var basic = document.getElementById("<%=txtmbasic.ClientID %>").value;
                    var tax = document.getElementById("<%=txtmex.ClientID %>").value;
                    var ed = document.getElementById("<%=txtmed.ClientID %>").value;
                    var hed = document.getElementById("<%=txtmhed.ClientID %>").value;
                    var stax = document.getElementById("<%=txtmtax.ClientID %>").value;
                    var frieght = document.getElementById("<%=txtmfre.ClientID %>").value;
                    var insurance = document.getElementById("<%=txtmins.ClientID %>").value;

                    if (basic == "") {
                        basic = 0;
                    }
                    if (tax == "") {
                        tax = 0;
                    }
                    if (ed == "") {
                        ed = 0;
                    }
                    if (hed == "") {
                        hed = 0;
                    }
                    if (stax == "") {
                        stax = 0;
                    }
                    if (frieght == "") {
                        frieght = 0;
                    }
                    if (insurance == "") {
                        insurance = 0;
                    }
                    if (tds == "") {
                        tds = 0;
                    }
                    if (ret == "") {
                        ret = 0;
                    }
                    if (adv == "") {
                        adv = 0;
                    }
                    if (hold == "") {
                        hold = 0;
                    }
                    if (other == "") {
                        other = 0;
                    }
                    originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(stax) + parseFloat(frieght) + parseFloat(insurance)));
                    originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(stax) + parseFloat(frieght) + parseFloat(insurance)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(adv) + parseFloat(hold) + parseFloat(other))));

                    var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                    var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                    document.getElementById('<%= txtmtotal.ClientID%>').value = roundValue;
                    document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;
                    document.getElementById('<%= h1.ClientID%>').value = roundValue1;
                }
                else {
                    var basic = document.getElementById("<%=txtbasic.ClientID %>").value;

                    if (basic == "") {
                        basic = 0;
                    }
                    if (document.getElementById("<%=ddltype.ClientID %>").value == "Service Tax Invoice") {
                        var tax = document.getElementById("<%=txttax.ClientID %>").value;
                        var ed = document.getElementById("<%=txted.ClientID %>").value;
                        var hed = document.getElementById("<%=txthed.ClientID %>").value;

                        if (tax == "") {
                            tax = 0;
                        }
                        if (ed == "") {
                            ed = 0;
                        }
                        if (hed == "") {
                            hed = 0;
                        }
                    }
                    if (tds == "") {
                        tds = 0;
                    }
                    if (ret == "") {
                        ret = 0;
                    }
                    if (adv == "") {
                        adv = 0;
                    }
                    if (hold == "") {
                        hold = 0;
                    }
                    if (other == "") {
                        other = 0;
                    }
                    if (document.getElementById("<%=ddltype.ClientID %>").value == "Service Tax Invoice") {
                        originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed)));
                        originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(adv) + parseFloat(hold) + parseFloat(other))));
                    }
                    else {
                        originalValue = eval((parseFloat(basic)));
                        originalValue1 = eval(((parseFloat(basic)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(adv) + parseFloat(hold) + parseFloat(other))));
                    }
                    var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                    var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                    document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
                    document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;
                    document.getElementById('<%= h1.ClientID%>').value = roundValue1;
                }
            }

            else if (category == "Service Excise Duty" || category == "Trading Excise Duty") {
                document.getElementById('<%= txtamt.ClientID%>').value = document.getElementById("<%=txtex.ClientID %>").value;
                document.getElementById('<%= h1.ClientID%>').value = document.getElementById("<%=txtex.ClientID %>").value;
            }
            else if (category == "Service Provider") {
                var tds1 = document.getElementById("<%=txttds.ClientID %>").value;
                var ret1 = document.getElementById("<%=txtretention.ClientID %>").value;
                var adv1 = document.getElementById("<%=txtadvance.ClientID %>").value;
                var hold1 = document.getElementById("<%=txthold.ClientID %>").value;
                var other1 = document.getElementById("<%=txtother.ClientID %>").value;
                var Basicvalue = document.getElementById("<%=txtSPbasic.ClientID %>").value;
                var edcess = document.getElementById("<%=txtspedc.ClientID %>").value;
                var hedcess = document.getElementById("<%=txtspheds.ClientID %>").value;
                var spservice = document.getElementById("<%=txtspservice.ClientID %>").value;
                var spexcise = document.getElementById("<%=txtspexices.ClientID %>").value;
                var spsales = document.getElementById("<%=txtspsales.ClientID %>").value;

                if (edcess == "") {
                    edcess = 0;
                }
                if (hedcess == "") {
                    hedcess = 0;
                }
                if (spservice == "") {
                    spservice = 0;
                }
                if (spexcise == "") {
                    spexcise = 0;
                }
                if (spsales == "") {
                    spsales = 0;
                }
                if (Basicvalue == "") {
                    Basicvalue = 0;
                }
                if (tds1 == "") {
                    tds1 = 0;
                }
                if (ret1 == "") {
                    ret1 = 0;
                }
                if (adv1 == "") {
                    adv1 = 0;
                }
                if (hold1 == "") {
                    hold1 = 0;
                }
                if (other1 == "") {
                    other1 = 0;
                }
                originalValue1 = eval(((parseFloat(Basicvalue) + parseFloat(edcess) + parseFloat(hedcess) + parseFloat(spservice) + parseFloat(spexcise) + parseFloat(spsales)) - (parseFloat(tds1) + parseFloat(ret1) + parseFloat(adv1) + parseFloat(hold1) + parseFloat(other1))));
                var roundValue3 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);

                document.getElementById('<%= txtamt.ClientID%>').value = roundValue3;
                document.getElementById('<%= h1.ClientID%>').value = roundValue3;
            }
        }                 
                                                 
                                         
    </script>
    <script language="javascript">


        function sumgridview() {
            sumgrid = document.getElementById("<%=GridView1.ClientID %>");
            var amt = 0;
            for (var i = 1; i < sumgrid.rows.length - 1; i++) {
                if (!isNaN(sumgrid.rows[i].cells[3].children[0].value)) {
                    amt += Number(sumgrid.rows[i].cells[3].children[0].value);
                }
            }
            sumgrid.rows[sumgrid.rows.length - 1].cells[3].innerHTML = amt;

        }
    </script>
</asp:Content>
