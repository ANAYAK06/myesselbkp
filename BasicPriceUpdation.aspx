<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="BasicPriceUpdation.aspx.cs"
    Inherits="BasicPriceUpdation" Title="Basic Price Updation" EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript">
        function closepopup() {
            $find('mdlreport').hide();
            var myMessage = document.getElementById("<%=hfprint.ClientID %>").value;
            if (myMessage == 'show')
            document.getElementById("<%=tblprint.ClientID %>").style.display = 'none';

        }

        function showpopup() {
            $find('mdlreport').show();

        }
        
       

    </script>
    <script language="javascript">
        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=gridcmc.ClientID %>");
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
            GridView = document.getElementById("<%=grdcentral.ClientID %>");
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
    <script language="javascript">
        function validate() {
            var GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");
            var GridView2 = document.getElementById("<%=grdcentral.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID%>").value;
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }


                }
            }
            else if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(8).children[0].value == "") {
                        window.alert("Enter new basic price");

                        return false;
                    }
                }
                var excisecheck1 = document.getElementById("<%=excisecheck.ClientID %>");              
                var VATcheck1 = document.getElementById("<%=VATcheck.ClientID %>");
                if (excisecheck1.checked == true) {
                    var response = confirm("Are you sure you have selected correct Excise/Service tax registration Number? ");
                    if (response) {
                       
                    }
                    else {
                        document.getElementById("<%=ddlExcno.ClientID %>").focus();
                        return false;
                    }
                }
                if (VATcheck1.checked == true) {
                    var response = confirm("Are you sure you have selected correct VAT tax registration Number? ");
                    if (response) {
                       
                    }
                    else {
                        document.getElementById("<%=VATcheck.ClientID %>").focus();
                        return false;
                    }
                }
            }
            else if (GridView2 != null) {
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    var itemcode = GridView2.rows(rowCount).cells(1).innerText;
                    if (GridView2.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(9).children[0].value == "") {
                        window.alert("Enter new invoice price");
                        GridView2.rows(rowCount).cells(9).children[0].focus();
                        return false;
                    }


                }
                var role = document.getElementById("<%=hfrole.ClientID%>").value;
                var excisecheck1 = document.getElementById("<%=excisecheck.ClientID %>");
                var VATcheck1 = document.getElementById("<%=VATcheck.ClientID %>");
                var excduty = document.getElementById("<%=txtexduty.ClientID%>").value;
                var Edcamt = document.getElementById("<%=txtEdc.ClientID%>").value;
                var HEdcamt = document.getElementById("<%=txtHEdc.ClientID%>").value;
                var ddlexc = document.getElementById("<%=ddlExcno.ClientID %>").value;
                var ddlvat = document.getElementById("<%=ddlvatno.ClientID %>").value;
                var vatamt = document.getElementById("<%=txtvatamt.ClientID%>").value;
                if (excisecheck1.checked == true) {
                    if (ddlexc == "Select") {
                        window.alert("Do you want to change Excise no");
                        return false;
                    }
                    else if (excduty == "") {
                        window.alert("Enter Excise duty");
                        return false;
                    }
                    else if (Edcamt == "") {
                        window.alert("Enter Edcess ");
                        return false;
                    }
                    else if (HEdcamt == "") {
                        window.alert("Enter HEdcess ");
                        return false;
                    }

                }
                if (VATcheck1.checked == true) {
                    if (ddlvat == "Select") {
                        window.alert("Do you want to change Vat no");
                        return false;
                    }
                    else if (vatamt == "") {
                        window.alert("Enter Vat Amount");
                        return false;
                    }

                }


            }
            if (role != "Project Manager") {
                var str1 = document.getElementById("<%=txtindt.ClientID %>").value;
                var str2 = document.getElementById("<%=txtindtmk.ClientID %>").value;
                if (str2 == "") {
                    window.alert("Enter Invoice Making Date");
                    return false;
                }
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
                    document.getElementById("<%=txtindtmk.ClientID %>").value = "";
                    document.getElementById("<%=txtindtmk.ClientID %>").focus();
                    return false;
                }
            }
            document.getElementById("<%=btnmdlupd.ClientID %>").style.display = 'none';

            return true;
        }

        function validatebasicprice() {
            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                if (parseInt(GridView1.rows(rowCount).cells(8).innerHTML) != parseInt(GridView1.rows(rowCount).cells(9).children[0].value)) {
                    if (GridView1.rows(rowCount).style.background == "") {
                        var response = confirm("Do you want to Change the basic price of " + itemcode + "?");
                        if (response) {
                            GridView1.rows(rowCount).style.background = '#FBE5E6';

                            return false;
                        }
                        else {

                            return false;
                        }
                        return true;
                    }
                }
            }
        }
    </script>
    <script language="javascript">

        function VAT() {

            var role = document.getElementById("<%=hfrole.ClientID%>").value;
            var excisecheck1 = document.getElementById("<%=excisecheck.ClientID %>");
            var VATcheck1 = document.getElementById("<%=VATcheck.ClientID %>");
            var excduty = document.getElementById("<%=txtexduty.ClientID%>").value;
            var Edcamt = document.getElementById("<%=txtEdc.ClientID%>").value;
            var HEdcamt = document.getElementById("<%=txtHEdc.ClientID%>").value;
            var ddlexc = document.getElementById("<%=ddlExcno.ClientID %>").value;
            var ddlvat = document.getElementById("<%=ddlvatno.ClientID %>").value;
            var vatamt = document.getElementById("<%=txtvatamt.ClientID%>").value;
            if (excisecheck1.checked == true) {
                if (ddlexc == "Select") {
                    window.alert("Do you want to change Excise no");
                    return false;
                }
                else if (excduty == "") {
                    window.alert("Enter Excise duty");
                    return false;
                }
                else if (Edcamt == "") {
                    window.alert("Enter Edcess ");
                    return false;
                }
                else if (HEdcamt == "") {
                    window.alert("Enter HEdcess ");
                    return false;
                }
                return false;

            }
            if (VATcheck1.checked == true) {
                if (ddlvat == "Select") {
                    window.alert("Do you want to change Vat no");
                    return false;
                }
                else if (vatamt == "") {
                    window.alert("Enter Vat Amount");
                    return false;
                }
                return false;
            }


        }
      
   
    </script>
    <script language="javascript">
        function btnvalidate() {
            validate();
            VAT();
        }
    
    </script>
    <script type="text/javascript">
        function cleartext() {
            var excisecheck1 = document.getElementById("<%=excisecheck.ClientID %>");

            if (excisecheck1.checked == false) {
                document.getElementById("<%=ddlExcno.ClientID %>").value = "Select";
                document.getElementById("<%=txtexduty.ClientID%>").value = "";
                document.getElementById("<%=txtEdc.ClientID%>").value = "";
                document.getElementById("<%=txtHEdc.ClientID%>").value = "";
                document.getElementById("<%=ddlExcno.ClientID %>").disabled = true;
                document.getElementById("<%=txtexduty.ClientID%>").disabled = true;
                document.getElementById("<%=txtEdc.ClientID%>").disabled = true;
                document.getElementById("<%=txtHEdc.ClientID%>").disabled = true;
            }
            else if (excisecheck1.checked == true) {
                document.getElementById("<%=ddlExcno.ClientID %>").disabled = false;
                document.getElementById("<%=txtexduty.ClientID%>").disabled = false;
                document.getElementById("<%=txtEdc.ClientID%>").disabled = false;
                document.getElementById("<%=txtHEdc.ClientID%>").disabled = false;
                document.getElementById("<%=ddlExcno.ClientID%>").value = '<%=ExciseNo %>';
                document.getElementById("<%=txtexduty.ClientID%>").value = '<%=Exciseduty %>';
                document.getElementById("<%=txtEdc.ClientID%>").value = '<%=Edcess %>';
                document.getElementById("<%=txtHEdc.ClientID%>").value = '<%=Hedcess %>';
            }
            Total();
            labelTotal();
        }
    </script>
    <script type="text/javascript">
        function cleartext1() {
            var VATcheck1 = document.getElementById("<%=VATcheck.ClientID %>");
            if (VATcheck1.checked == false) {
                document.getElementById("<%=ddlvatno.ClientID %>").value = "Select";
                document.getElementById("<%=txtvatamt.ClientID%>").value = "";
                document.getElementById("<%=ddlvatno.ClientID %>").disabled = true;
                document.getElementById("<%=txtvatamt.ClientID%>").disabled = true;
            }
            else if (VATcheck1.checked == true) {
                document.getElementById("<%=ddlvatno.ClientID %>").disabled = false;
                document.getElementById("<%=txtvatamt.ClientID%>").disabled = false;
                document.getElementById("<%=ddlvatno.ClientID%>").value = '<%=vatno %>';
                document.getElementById("<%=txtvatamt.ClientID%>").value = '<%=vatamount %>';
            }
            Total();
            labelTotal();
        }
    </script>
    <script type="text/javascript">
        function verification() {
            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                if (GridView1.rows(rowCount).cells(0).children(0).checked == true && GridView1.rows(rowCount).cells(9).children[0].disabled == true) {
                    var response = confirm("Do you want to Change the basic price of " + itemcode + "?");
                    if (response) {
                        GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(9).children[0].value;
                        GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                        GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                        return false;
                    }
                    else {
                        GridView1.rows(rowCount).cells(8).children[0].disabled = false;
                    }
                    return false;
                }

            }

        }
       
    
    </script>
    <script type="text/javascript">
        function selectcheckbox() {

            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                if (GridView1.rows(rowCount).cells(0).children(0).checked == true && GridView1.rows(rowCount).cells(9).children[0].disabled == true) {
                    var response1 = confirm("Do you want to Change the Invoice price of " + itemcode + "?");
                    if (response1) {
                        GridView1.rows(rowCount).cells(9).children[0].style.color = 'white';
                        GridView1.rows(rowCount).cells(9).children[0].focus();
                        GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                        GridView1.rows(rowCount).cells(9).children[0].disabled = false;

                        verification();

                        return false;
                    }


                    else {
                    }
                    return false;
                }
                else {


                }
            }

        }
    
    </script>
    <script type="text/javascript">
        function CMCCalculate(val) {
            var TaxTotal = 0;
            var amt = 0;
            var Frieght = 0;
            var Insurance = 0;
            var total = 0;
            var roundtotal = 0;
            var roundtaxtotal = 0;
            var valid = val;
            var budgetbal = document.getElementById('<%= hbasiprice.ClientID %>').value;
            var Frieght = document.getElementById("<%=txtfre.ClientID %>").value;
            var Insurance = document.getElementById("<%=txtinsurance.ClientID %>").value;
            grd = document.getElementById("<%=gridcmc.ClientID %>");
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                if (Number(grd.rows(rowCount).cells[9].children[0].value) != 0.00) {

                    roundtotal = Number(grd.rows(rowCount).cells[7].innerHTML) * Number(grd.rows(rowCount).cells(9).children[0].value);
                    grd.rows(rowCount).cells(10).innerHTML = Math.round(roundtotal * Math.pow(10, 2)) / Math.pow(10, 2);
                    if (document.getElementById("<%=VATcheck.ClientID %>").checked == false) {
                        roundtaxtotal = Number(grd.rows(rowCount).cells(10).innerHTML) * (Number(grd.rows(rowCount).cells(11).innerHTML) / 100);
                        grd.rows(rowCount).cells(12).innerHTML = Math.round(roundtaxtotal * Math.pow(10, 2)) / Math.pow(10, 2);
                        TaxTotal += Number(grd.rows(rowCount).cells(12).innerHTML);
                    }
                    total += Number(grd.rows(rowCount).cells(10).innerHTML);

                }
                else {
                    total += Number(grd.rows(rowCount).cells(10).innerHTML);
                    if (document.getElementById("<%=VATcheck.ClientID %>").checked == false) {
                        TaxTotal += Number(grd.rows(rowCount).cells(12).innerHTML);
                    }
                }
                grd.rows[grd.rows.length - 1].cells[10].innerHTML = total;

                document.getElementById("<%=txtbasic.ClientID %>").value = total;
                if (document.getElementById("<%=VATcheck.ClientID %>").checked == false) {
                    grd.rows[grd.rows.length - 1].cells[12].innerHTML = TaxTotal;
                    document.getElementById("<%=txttax.ClientID %>").value = TaxTotal;
                }
                //                document.getElementById("<%=txtbasic.ClientID %>").value = total;
                //                document.getElementById("<%=txttotal.ClientID %>").value = parseFloat(total) + parseFloat(Frieght) + parseFloat(Insurance);

                Total();
                labelTotal();

                if (valid != 1) {
                    //                    if (document.getElementById("<%=btnmdlupd.ClientID %>").disabled == false) {
                    var Netamount = document.getElementById("<%=txttotal.ClientID %>").value;
                    var hfpoamount = document.getElementById("<%=hfpoamount.ClientID %>").value;
                    var invamt = Netamount - hfpoamount;
                    if (parseFloat(budgetbal) < parseFloat(invamt)) {
                        alert("Insufficient Balance");
                        document.getElementById("<%=btnmdlupd.ClientID %>").disabled = true;
                        return false;
                    }
                    else
                        document.getElementById("<%=btnmdlupd.ClientID %>").disabled = false;
                }
                //                }
            }
        }
    </script>
    <script language="javascript">
        function SelectAll() {
            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID %>").value;
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {

                var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                var oldprice = GridView1.rows(rowCount).cells(8).children[0].value;
                var newprice = GridView1.rows(rowCount).cells(9).children[0].value;
                var hprice = GridView1.rows(rowCount).cells(14).children[0].value;
                var hbasicprice = GridView1.rows(rowCount).cells(13).children[0].value;
                var itemtype = GridView1.rows(1).cells(1).innerHTML.substring(0, 1);
                if (GridView1.rows(rowCount).cells(0).children(0).checked == true && GridView1.rows(rowCount).cells(8).children[0].disabled == false) {
                    if (GridView1.rows(rowCount).cells(8).children[0].style.color == "black") {
                        if ((parseFloat(GridView1.rows(rowCount).cells(8).children[0].value) > parseFloat(GridView1.rows(rowCount).cells(9).children[0].value)) && (role == "Chief Material Controller") && (GridView1.rows(rowCount).cells(9).children[0].value != 0) && (itemtype != "1")) {
                            var response = confirm("Do you want to Change the basic price of " + itemcode + "?");
                            if (response) {
                                GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                                GridView1.rows(rowCount).cells(8).children[0].focus();
                                GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                                GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(9).children[0].value;
                                return false;
                            }
                            else {
                                GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                                GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                            }
                            return false;
                        }

                        else if ((role == "Chief Material Controller" || role == "SuperAdmin") && (GridView1.rows(rowCount).cells(9).children[0].value == 0) && (GridView1.rows(rowCount).cells(8).children[0].value != 0)) {
                            window.alert("New Purchase Price cannot be Empty");
                            GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(14).children[0].value;
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                            CMCCalculate();
                            return false;


                        }
                        else if ((role == "Chief Material Controller" || role == "SuperAdmin") && (GridView1.rows(rowCount).cells(8).children[0].value == 0) && (GridView1.rows(rowCount).cells(9).children[0].value != 0)) {
                            window.alert("Standard Price cannot be Empty");
                            GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                            CMCCalculate();
                            return false;

                        }
                        else if ((role == "Chief Material Controller" || role == "SuperAdmin") && (GridView1.rows(rowCount).cells(8).children[0].value == 0) && (GridView1.rows(rowCount).cells(9).children[0].value == 0)) {
                            window.alert("New Purchase Price and Standard Price cannot be Empty");
                            GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                            GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(14).children[0].value;
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                            CMCCalculate();
                            return false;

                        }

                        else if (parseFloat(GridView1.rows(rowCount).cells(8).children[0].value) != parseFloat(GridView1.rows(rowCount).cells(9).children[0].value) && (role == "SuperAdmin")) {
                            if (GridView1.rows(rowCount).cells(9).children[0].value == "") {
                                window.alert("New Purchase Price cannot be Empty");
                                GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(14).children[0].value;
                            }
                            else {
                                var response = confirm("Do you want to Change the basic price of " + itemcode + "?");

                                if (response) {
                                    GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                                    GridView1.rows(rowCount).cells(8).children[0].focus();
                                    GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                    GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                                    GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(9).children[0].value;
                                    CMCCalculate();
                                    return false;
                                }
                                else {

                                    GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                                    GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                    GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                                            CMCCalculate();
                                }
                            }

                            return false;
                        }
                        else {

                            GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                            // GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                            CMCCalculate();

                        }


                    }
                }
                else if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        document.getElementById("<%=btnmdlupd.ClientID %>").disabled = false;
                    GridView1.rows(rowCount).cells(8).children[0].disabled = false;
                    GridView1.rows(rowCount).cells(8).children[0].style.color = 'black';
                    GridView1.rows(rowCount).cells(9).children[0].disabled = false;
                    GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                    GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(14).children[0].value;
                        var valid = 1;                       
                        CMCCalculate(valid);
              
                }
            }
        }
       
    
    </script>
    <script language="javascript" type="text/javascript">
        function Calculate(val) {
            var TaxTotal = 0;
            var amt = 0;
            var total = 0;
            var roundtotal = 0;
            var roundtaxtotal = 0;
            var valid = val;
            var budgetbal = document.getElementById('<%= hbasiprice.ClientID %>').value;
            grd = document.getElementById("<%=grdcentral.ClientID %>");
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                if (Number(grd.rows(rowCount).cells[9].children[0].value) != 0.00) {

                    roundtotal = Number(grd.rows(rowCount).cells[7].innerHTML) * Number(grd.rows(rowCount).cells(9).children[0].value);
                    grd.rows(rowCount).cells(10).innerHTML = Math.round(roundtotal * Math.pow(10, 2)) / Math.pow(10, 2);
                    if (document.getElementById("<%=VATcheck.ClientID %>").checked == false) {
                        roundtaxtotal = Number(grd.rows(rowCount).cells(10).innerHTML) * (Number(grd.rows(rowCount).cells(11).innerHTML) / 100);
                        grd.rows(rowCount).cells(12).innerHTML = Math.round(roundtaxtotal * Math.pow(10, 2)) / Math.pow(10, 2);
                        TaxTotal += Number(grd.rows(rowCount).cells(12).innerHTML);
                    }
                    total += Number(grd.rows(rowCount).cells(10).innerHTML);

                }
                else {
                    grd.rows(rowCount).cells(10).innerHTML = "";
                    grd.rows(rowCount).cells(12).innerHTML = "";
                    total += Number(grd.rows(rowCount).cells(10).innerHTML);
                    if (document.getElementById("<%=VATcheck.ClientID %>").checked == false) {
                        TaxTotal += Number(grd.rows(rowCount).cells(12).innerHTML);
                    }
                }
                grd.rows[grd.rows.length - 1].cells[10].innerHTML = total;
                document.getElementById("<%=txtbasic.ClientID %>").value = total;
                if (document.getElementById("<%=VATcheck.ClientID %>").checked == false) {
                    grd.rows[grd.rows.length - 1].cells[12].innerHTML = TaxTotal;
                    document.getElementById("<%=txttax.ClientID %>").value = TaxTotal;
                }
                Total();
                labelTotal();
                if (valid != 1) {
                    var Netamount = document.getElementById("<%=txttotal.ClientID %>").value;
                    var hfpoamount = document.getElementById("<%=hfpoamount.ClientID %>").value;
                    var invamt = Netamount - hfpoamount;
                    if (parseFloat(budgetbal) < parseFloat(invamt)) {
                        alert("Insufficient Balance");
                        document.getElementById("<%=btnmdlupd.ClientID %>").disabled = true;
                        return false;
                    }
                }
                else
                    document.getElementById("<%=btnmdlupd.ClientID %>").disabled = false;                    
            }
        }
    </script>
    <script language="javascript">
        function SelectAll1() {
            var GridView1 = document.getElementById("<%=grdcentral.ClientID %>");

            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                if (GridView1.rows(rowCount).cells(0).children(0).checked == true && GridView1.rows(rowCount).cells(9).children[0].disabled == false) {
                    if (GridView1.rows(rowCount).cells(9).children[0].style.color != 'black') {
                        //Calculate();
                        var response = confirm("Do you want to Change the Invoice Price of " + itemcode + "?");
                        if (response) {

                            GridView1.rows(rowCount).cells(9).children[0].value = "";
                            GridView1.rows(rowCount).cells(9).children[0].style.color = 'black';
                            if (GridView1.rows(rowCount).cells(9).children[0].value == "") {
                                window.alert("Enter new Invoice price");
                                GridView1.rows(rowCount).cells(9).children[0].focus();
                                GridView1.rows(rowCount).cells(10).innerHTML = 0;
                                GridView1.rows(rowCount).cells(12).innerHTML = 0;
                                Calculate();
                                return false;
                            }
                            return false;
                        }
                        else {
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                Calculate();
                            return false;
                        }
                        return true;
                    }
                }
                else if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                    document.getElementById("<%=btnmdlupd.ClientID %>").disabled = false;
                    GridView1.rows(rowCount).cells(9).children[0].disabled = false;
                    GridView1.rows(rowCount).cells(9).children[0].style.color = 'red';
                    GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                    var valid = 1;
                    Calculate(valid);
                }
            }
        }
    
    </script>
    <script language="javascript">
        function Total() {
            var total = 0;
            var adv = document.getElementById("<%=txtAdvance.ClientID %>").value;
            var hold = document.getElementById("<%=txthold.ClientID %>").value;
            var other = document.getElementById("<%=txtother.ClientID %>").value;
            var originalValue1 = 0;
            if (adv == "") {
                adv = 0;
            }
            if (hold == "") {
                hold = 0;
            }
            if (other == "") {
                other = 0;
            }
            var i = parseFloat(Number(document.getElementById('<%= txtbasic.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txttax.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txtfre.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txtinsurance.ClientID%>').value));
            originalValue1 = eval(parseFloat(i - (parseFloat(adv) + parseFloat(hold) + parseFloat(other))));
            var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtnetAmount.ClientID%>').value = roundValue1;
            document.getElementById('<%= txttotal.ClientID%>').value = i;
        }


    </script>
    <script language="javascript">
        function labelTotal() {
            var adv = document.getElementById("<%=txtAdvance.ClientID %>").value;
            var hold = document.getElementById("<%=txthold.ClientID %>").value;
            var other = document.getElementById("<%=txtother.ClientID %>").value;
            var originalValue1 = 0;
            if (adv == "") {
                adv = 0;
            }
            if (hold == "") {
                hold = 0;
            }
            if (other == "") {
                other = 0;
            }
            var i = parseFloat(Number(document.getElementById('<%= txtbasic.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txttax.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txtfre.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txtinsurance.ClientID%>').value));
            originalValue1 = eval(parseFloat(i - (parseFloat(adv) + parseFloat(hold) + parseFloat(other))));
            var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);

            var VATcheck1 = document.getElementById("<%=VATcheck.ClientID %>");
            if (VATcheck1.checked == true) {
                var vatamt = document.getElementById("<%=txtvatamt.ClientID %>").value;
            }
            else
                vatamt = 0;

            var excisecheck1 = document.getElementById("<%=excisecheck.ClientID %>");

            if (excisecheck1.checked == true) {
                var exciseduty = document.getElementById("<%=txtexduty.ClientID %>").value;
                var edc = document.getElementById("<%=txtEdc.ClientID %>").value;
                var hedc = document.getElementById("<%=txtHEdc.ClientID %>").value;
            }
            else {
                exciseduty = 0;
                edc = 0;
                hedc = 0;
            }
            var E = parseFloat(roundValue1) + parseFloat(vatamt) + parseFloat(exciseduty) + parseFloat(edc) + parseFloat(hedc);
            document.getElementById("<%=nettxt.ClientID%>").value = E;

        }
    
    </script>
    <script language="javascript">
        function invoicecalcentral() {
            grd = document.getElementById("<%=grdcentral.ClientID %>");
            var amt = 0;
            var total = 0;
            for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                if (!isNaN(grd.rows(rowCount).cells(9).children[0].value)) {
                    total += (Number(grd.rows(rowCount).cells[7].innerHTML)) * ((Number(grd.rows(rowCount).cells(9).children[0].value)));


                }
            }

            grd.rows[grd.rows.length - 1].cells[9].innerHTML = total;
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
    <script language="javascript" type="text/javascript">

        function checkroles() {
            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID %>").value;
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
              
                    var hprice = GridView1.rows(rowCount).cells(14).children[0].value;
                    var newprice = GridView1.rows(rowCount).cells(9).children[0].value;
                    var hbasicprice = GridView1.rows(rowCount).cells(13).children[0].value;
                    var newbasicprice = GridView1.rows(rowCount).cells(8).children[0].value;
                    var itemtype = GridView1.rows(1).cells(1).innerHTML.substring(0, 1);
                    if (role == "Chief Material Controller") {
                        if (parseFloat(newbasicprice) > parseFloat(hbasicprice)) {
                            window.alert("You are not able to increase standard price");
                            GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                            return false;
                        }
                    }

                    if (parseFloat(newprice) > parseFloat(hprice)) {
                        window.alert("You are not able to increase new purchase price");
                        GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(14).children[0].value;
                        return false;
                    }
                    else if (parseFloat(newbasicprice) > parseFloat(hbasicprice)) {
                        window.alert("You are not able to increase standard price");
                        GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                        return false;
                    }
                    else if ((itemtype == "1") && ((parseFloat(newprice)) > (parseFloat(hprice)))) {
                        window.alert("You are not able to increase the new purchase price for assets");
                        GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(14).children[0].value;
                        return false;
                    }
                    else if ((itemtype == "1") && ((parseFloat(newbasicprice)) != (parseFloat(hbasicprice)))) {
                        window.alert("You are not able to increase or decrease the standard price for assets");
                        GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                        return false;
                    }
            }
        }
    </script>
    <script language="javascript">
        function btnvalidate() {
            var b = validate();
            if (b)
                VAT();
            return b;

        }
    </script>
    <script language="javascript">
        function validate1() {

            document.getElementById("<%=btnreject.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblprint.ClientID %>");
            var printWindow = window.open('', '', 'height=800,width=990');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        function myFunction() {
            if (confirm("Do you want print") == true) {
                var panel = document.getElementById("<%=tblprint.ClientID %>");
                var printWindow = window.open('', '', 'height=800,width=990');
                printWindow.document.write('<html><head><title>DIV Contents</title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(panel.innerHTML);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                setTimeout(function () {
                    printWindow.print();
                }, 500);
                document.getElementById("<%=tblprint.ClientID %>").style.display = 'none';
                return false;
            }
            else {
                document.getElementById("<%=tblprint.ClientID %>").style.display = 'none';
            }
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
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>Invoice Verification <a class="help"
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
                                                                                            <%--<br />
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
                                                                        <%--<div class="box-a list-a">
                                                                            <div class="inner">--%>
                                                                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                            <tbody>
                                                                                <tr class="pagerbar">
                                                                                    <td class="pagerbar-cell" align="right">
                                                                                        <table class="pager-table">
                                                                                            <tbody>
                                                                                                <tr>
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
                                                                                            <asp:HiddenField ID="hfrole" runat="server" />
                                                                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                PagerStyle-CssClass="grid pagerbar" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                                                OnDataBound="GridView1_DataBound" DataKeyNames="po_no" OnRowEditing="GridView1_RowEditing2"
                                                                                                OnRowDataBound="GridView1_RowDataBound" EmptyDataText="There Is No Records" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                                                                <Columns>
                                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                        SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                                                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                                                                        EditImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                    <asp:TemplateField HeaderText="MRR No" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                                                                        ItemStyle-VerticalAlign="Bottom">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("MRR_no") %>
                                                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="po_no" HeaderText="PO No" />
                                                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                                                                    <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice Date" />
                                                                                                    <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                                                                    <asp:BoundField DataField="description" HeaderText="Description" />
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
                                                                                        <cc1:ModalPopupExtender ID="popreports" BehaviorID="mdlreport" runat="server" TargetControlID="btnModalPopUp"
                                                                                            PopupControlID="pnlreport" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                        <asp:Panel ID="pnlreport" runat="server" Style="display: none;">
                                                                                            <table width="800px" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td width="13" valign="bottom">
                                                                                                        <img src="images/leftc.jpg">
                                                                                                    </td>
                                                                                                    <td class="pop_head" align="left" id="viewrep" runat="server">
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
                                                                                                    <td bgcolor="#FFFFFF">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td height="180" valign="top" class="popcontent">
                                                                                                        <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                                                            height: 500px;">
                                                                                                            <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
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
                                                                                                                                <table>
                                                                                                                                    <tr id="CST" runat="server">
                                                                                                                                        <td colspan="2">
                                                                                                                                            <asp:GridView ID="Grdeditpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                DataKeyNames="id" ShowFooter="true" OnRowDataBound="Grdeditpopup_RowDataBound">
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
                                                                                                                                                    <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                    <asp:BoundField DataField="quantity" HeaderText="Recieved Qty" ItemStyle-Width="25px" />
                                                                                                                                                    <%-- <asp:BoundField DataField="basic_price" HeaderText="Last Purchased Price" HeaderStyle-Width="50px"
                                                                                                                                                                ItemStyle-Width="50px" />--%>
                                                                                                                                                    <asp:BoundField DataField="newbasicprice" HeaderText="New Purchased Price" HeaderStyle-Width="50px"
                                                                                                                                                        ItemStyle-Width="50px" />
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("basic_price")%>' />
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="" HeaderText="Total Amount" />
                                                                                                                                                </Columns>
                                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                <HeaderStyle CssClass="grid-header" />
                                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                            </asp:GridView>
                                                                                                                                            <asp:HiddenField ID="hbasiprice"  Value="0" runat="server" />
                                                                                                                                           
                                                                                                                                            <asp:GridView ID="grdcentral" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                DataKeyNames="id" OnRowDataBound="grdcentral_RowDataBound" ShowFooter="true">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll1();" />
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                                                                                    <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                                                                    <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="100px" />
                                                                                                                                                    <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                                                                    <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                                                                    <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                                                                    <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                    <asp:BoundField DataField="Quantity" HeaderText="Receieved Qty" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:BoundField DataField="basic_price" HeaderText="Standard Price" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:TemplateField HeaderText="New Purchased Price">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:TextBox ID="txtbasic" onkeyup="Calculate();" runat="server" Width="100px" Text='<%#Bind("newbasicprice") %>'
                                                                                                                                                                onkeypress='IsNumeric2(event)'></asp:TextBox>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                                                                                                    <asp:BoundField DataField="STaxPercent" HeaderText="% Of Tax" />
                                                                                                                                                    <asp:BoundField DataField="Tax Amount" HeaderText="Tax Amount" />
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("newbasicprice")%>' />
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                </Columns>
                                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                <HeaderStyle CssClass="grid-header" />
                                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                                            </asp:GridView>
                                                                                                                                            <asp:GridView ID="gridcmc" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                DataKeyNames="item_code" ShowFooter="true" OnRowDataBound="gridcmc_RowDataBound1">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll();" />
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <%-- <asp:BoundField DataField="id" Visible="false" />--%>
                                                                                                                                                    <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                                                                    <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="100px" />
                                                                                                                                                    <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                                                                    <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                                                                    <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                                                                    <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                                                                    <asp:BoundField DataField="Quantity" HeaderText="Receieved Qty" ItemStyle-Width="25px" />
                                                                                                                                                    <asp:TemplateField HeaderText="Standard Price">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:TextBox ID="txtbasic" runat="server" Width="100px" Text='<%#Eval("basic_price") %>'
                                                                                                                                                                onkeypress='IsNumeric1(event)' ForeColor="Black" onkeyup="checkroles();"></asp:TextBox>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:TemplateField HeaderText="New Purchased Price">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:TextBox ID="txtinvoiceprice" runat="server" onkeyup="checkroles(); CMCCalculate();" 
                                                                                                                                                                Width="100px" Text='<%#Eval("newbasicprice") %>' onkeypress='IsNumeric1(event)'></asp:TextBox>                                                                                                                                                         
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                                                                                                    <asp:BoundField DataField="STaxPercent" HeaderText="% Of Tax" />
                                                                                                                                                    <asp:BoundField DataField="Tax Amount" HeaderText="Tax Amount" />
                                                                                                                                                    <asp:TemplateField ItemStyle-Width="1px" HeaderStyle-Width="1px">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("basic_price")%>' />
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                    <asp:TemplateField ItemStyle-Width="1px" HeaderStyle-Width="1px">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <asp:HiddenField ID="h3" runat="server" Value='<%#Eval("newbasicprice")%>' />
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
                                                                                                                                    <tr id="InvoiceiInfo" runat="server">
                                                                                                                                        <td colspan="4" valign="top" class=" item-notebook" width="100%">
                                                                                                                                            <div id="_notebook_1" class="notebook" style="display: block;">
                                                                                                                                                <div class="notebook-tabs">
                                                                                                                                                    <div class="right scroller">
                                                                                                                                                    </div>
                                                                                                                                                    <div class="left scroller">
                                                                                                                                                    </div>
                                                                                                                                                    <ul class="notebook-tabs-strip">
                                                                                                                                                        <li class="notebook-tab notebook-page notebook-tab-active" title="" id="none"><span
                                                                                                                                                            class="tab-title"><span>Invoice Info</span></span></li><li class="notebook-tab notebook-page"
                                                                                                                                                                title="" style="display: none;"><span class="tab-title"><span>Journal Items</span></span></li></ul>
                                                                                                                                                </div>
                                                                                                                                                <div class="notebook-pages">
                                                                                                                                                    <div class="notebook-page notebook-page-active">
                                                                                                                                                        <div>
                                                                                                                                                            <table border="0" class="fields" width="100%">
                                                                                                                                                                <tbody>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Po NO:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtpo" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Invoice No:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtin" Enabled="false" runat="server" ToolTip="Invoice No" CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Invoice Date:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtindt" runat="server" ToolTip="Invoice Date" CssClass="char"></asp:TextBox>
                                                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtindt"
                                                                                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                                                                                                PopupButtonID="txtindt">
                                                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Invoice Making Date:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtindtmk" runat="server" ToolTip="Invoice Making Date" CssClass="char"></asp:TextBox>
                                                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindtmk"
                                                                                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                                                                                                PopupButtonID="txtindtmk">
                                                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Basic Value:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtbasic" onKeyDown="javascript: return false;" runat="server" ToolTip="Basic Value"
                                                                                                                                                                                CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Freight :
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtfre" runat="server" CssClass="char" onkeyup="Total(); labelTotal();"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Insurance :
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtinsurance" runat="server" CssClass="char" onkeyup="Total(); labelTotal();"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <asp:Label ID="lbltax" CssClass="label" runat="server" Text="Sales Tax:"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txttax" runat="server" onKeyDown="javascript: return false;" onKeyPress="javascript: return false;"
                                                                                                                                                                                CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Total:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txttotal" onKeyDown="javascript: return false;" onKeyPress="javascript: return false;"
                                                                                                                                                                                runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                                                                                            <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                                                                                Deductions</div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Advance:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtAdvance" runat="server" onkeyup="Total(); labelTotal();" CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Hold:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txthold" runat="server" onkeyup="Total(); labelTotal();" CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Anyother:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtother" runat="server" onkeyup="Total(); labelTotal();" CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Vendor Name:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtname" Enabled="false" runat="server" ToolTip="Vendor Name" CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Description:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtindesc" runat="server" CssClass="char" ToolTip="Description"
                                                                                                                                                                                Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                            <label class="help">
                                                                                                                                                                                Net Amount:
                                                                                                                                                                            </label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td valign="middle" class="item item-char">
                                                                                                                                                                            <asp:TextBox ID="txtnetAmount" runat="server" onKeyDown="javascript: return false;"
                                                                                                                                                                                onKeyPress="javascript: return false;" ToolTip="NetAmount" CssClass="char"></asp:TextBox>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                                                                                            <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                                                                                <asp:CheckBox ID="excisecheck" runat="server" onclick="javascript:return false;" />
                                                                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                                                                                <asp:Label ID="Label3" runat="server" Text="ExciseDuty Invoice"></asp:Label>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td align="right">
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        Excise No:
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:DropDownList ID="ddlExcno" runat="server">
                                                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                                                    <%--<asp:TextBox ID="txtExciseno" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                                    <asp:HiddenField ID="hfexcno" runat="server" />--%>
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td align="center">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        Excise Duty :
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:TextBox ID="txtexduty" runat="server" CssClass="char" onkeyup="Total(); labelTotal();"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td align="center">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        EDCess :
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:TextBox ID="txtEdc" runat="server" CssClass="char" onkeyup="Total(); labelTotal();"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td align="center">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        HEDCess :
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:TextBox ID="txtHEdc" runat="server" CssClass="char" onkeyup="Total(); labelTotal();"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td colspan="6" valign="middle" width="100%" style="height: 10px">
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                                                                                        <asp:CheckBox ID="VATcheck" runat="server" onclick="javascript:return false;" />&nbsp;&nbsp;&nbsp;
                                                                                                                                                                                        <asp:Label ID="Label1" runat="server" Text="VAT Invoice"></asp:Label>
                                                                                                                                                                                    </div>
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <asp:Label ID="Label6" runat="server" Text="VAT No :"></asp:Label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:DropDownList ID="ddlvatno" runat="server">
                                                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                                                    <%--<asp:TextBox ID="txtVATno" runat="server" CssClass="char" Enabled="False"></asp:TextBox>
                                                                                                                                                                                    <asp:HiddenField ID="hfvatno" runat="server" />--%>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td align="center">
                                                                                                                                                                                    <asp:Label ID="Label8" runat="server" Text="VAT Amount :"></asp:Label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:TextBox ID="txtvatamt" runat="server" CssClass="char" onkeyup="Total(); labelTotal();"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td colspan="6" valign="middle" width="100%" style="height: 10px">
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
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
                                                                                                                                    <tr>
                                                                                                                                        <td height="10px">
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr id="netamount" runat="server">
                                                                                                                                        <td align="center" colspan="4">
                                                                                                                                            <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="Total Net Amount:"></asp:Label>
                                                                                                                                            <asp:TextBox ID="nettxt" runat="server" Enabled="false" CssClass="char" onkeyup="Total(); labelTotal();"></asp:TextBox>
                                                                                                                                            <asp:HiddenField ID="hfpoamount" runat="server" />
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td height="10px">
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr id="trbtn" runat="server">
                                                                                                                                        <td align="center" colspan="4">
                                                                                                                                            <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                                                                                                                            <asp:Button ID="btnmdlupd" runat="server" Text="" CssClass="button" OnClick="btnmdlupd_Click"
                                                                                                                                                OnClientClick="javascript:return validate();" />
                                                                                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                                                            <asp:Button ID="btnreject" runat="server" Text="Reject" CssClass="button" OnClick="btnreject_Click"
                                                                                                                                                OnClientClick="javascript:return validate1();" />
                                                                                                                                            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();" />
                                                                                                                                            <asp:HiddenField ID="hfprint" runat="server" />
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
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
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="pagerbar">
                                                                                    <td class="pagerbar-cell" align="right">
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <%-- </div>
                                                                        </div>--%>
                                                                        <table id="tblprint" runat="server" visible="false" style="border: 1px solid #000;">
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="pnlContents" runat="server">
                                                                                        <tr>
                                                                                            <td style="width: 100%">
                                                                                                <div class="notebook-tabs" align="center">
                                                                                                    <span class="tab-title"><span style="font-weight: bold;">Invoice Details</span></span>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%">
                                                                                                <asp:GridView ID="Grdeditpopupp" Width="100%" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                    DataKeyNames="id" OnRowDataBound="Grdeditpopupp_RowDataBound">
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
                                                                                                        <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                        <asp:BoundField DataField="quantity" HeaderText="Recieved Qty" ItemStyle-Width="25px" />
                                                                                                        <%-- <asp:BoundField DataField="basic_price" HeaderText="Last Purchased Price" HeaderStyle-Width="50px"
                                                                                                                                                                ItemStyle-Width="50px" />--%>
                                                                                            <asp:BoundField DataField="newbasicprice" HeaderText="New Purchased Price" HeaderStyle-Width="50px"
                                                                                                ItemStyle-Width="50px" />
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("basic_price")%>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="" HeaderText="Total Amount" />
                                                                                        </Columns>
                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="InvoiceiInfop" runat="server" style="width: 100%">
                                                                                <td colspan="4" valign="top" class=" item-notebook" width="100%">
                                                                                    <div id="Div1" class="notebook" style="display: block;">
                                                                                        <div class="notebook-tabs" align="center">
                                                                                            <span class="tab-title"><span style="font-weight: bold;">Invoice Info</span></span>
                                                                                        </div>
                                                                                        <div>
                                                                                            <div>
                                                                                                <div>
                                                                                                    <table border="0" width="100%">
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        PO NO:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtpop" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        Enabled="false" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                    </tr>
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Invoice No:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtinp" Enabled="false" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server" ToolTip="Invoice No"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="100px">
                                                                                                                    <label>
                                                                                                                        Invoice Date:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtindtp" runat="server" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" ToolTip="Invoice Date"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                             <td width="100px">
                                                                                                                                <label>
                                                                                                                                    Invoice Making Date:
                                                                                                                                </label>
                                                                                                                            </td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:TextBox ID="txtindtmkp" runat="server" CssClass="pestbox" Style="border: None;
                                                                                                                                    border-bottom: 1px solid #000" ToolTip="Invoice Making Date"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Basic Value:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtbasicp" runat="server" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" ToolTip="Basic Value"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Freight :
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtfrep" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Insurance :
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtinsurancep" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td width="75px">
                                                                                                                    <asp:Label ID="lbltaxp" CssClass="peslbl1" runat="server" Text="Sales Tax:"></asp:Label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txttaxp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="75px">
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                </td>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Total:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txttotalp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr align="center">
                                                                                                                <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                                    <div class="separator horizontal" style="font-size: 10pt; font-weight: bold">
                                                                                                                        Deductions</div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Advance:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtAdvancep" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Hold:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtholdp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Anyother:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtotherp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td width="100px">
                                                                                                                    <label>
                                                                                                                        Vendor Name:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtnamep" Enabled="false" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" runat="server" ToolTip="Vendor Name"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Description:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtindescp" runat="server" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" ToolTip="Description" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="75px">
                                                                                                                    <label>
                                                                                                                        Net Amt:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtnetAmountp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr align="center">
                                                                                                                <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="ExciseDuty Invoice"></asp:Label>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td width="100px">
                                                                                                                    <label>
                                                                                                                        Excise No:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <%--<asp:DropDownList ID="ddlExcno" runat="server"></asp:DropDownList>--%>
                                                                                                                    <asp:TextBox ID="ddlExcnop" runat="server" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" Enabled="false"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td width="100px">
                                                                                                                    <label>
                                                                                                                        Excise Duty :
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtexdutyp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td align="center">
                                                                                                                    <label>
                                                                                                                        EDCess :
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtEdcp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td align="center">
                                                                                                                    <label>
                                                                                                                        HEDCess :
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtHEdcp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="6" valign="middle" width="100%" style="height: 10px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr align="center">
                                                                                                                <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                        <asp:Label ID="Label5" runat="server" CssClass="peslbl1" Font-Bold="true" Text="VAT Invoice"></asp:Label>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td width="100px">
                                                                                                                    <asp:Label ID="Label7" runat="server" CssClass="peslbl1" Text="VAT No:"></asp:Label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <%--  <asp:DropDownList ID="ddlvatno" runat="server"></asp:DropDownList>--%>
                                                                                                                    <asp:TextBox ID="ddlvatnop" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server" Enabled="False"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td width="100px">
                                                                                                                    <asp:Label ID="Label10" runat="server" CssClass="peslbl1" Text="VAT Amount :"></asp:Label>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox ID="txtvatamtp" CssClass="pestbox" Style="border: None; border-bottom: 1px solid #000"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="6" valign="middle" width="100%" style="height: 10px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="Tr1" runat="server">
                                                                                                                <td align="center" colspan="8">
                                                                                                                    <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="Total Net Amount:"></asp:Label>&nbsp;&nbsp;
                                                                                                                    <asp:TextBox ID="nettxtp" runat="server" CssClass="pestbox" Style="border: None;
                                                                                                                        border-bottom: 1px solid #000" Width="300px" Enabled="false"></asp:TextBox>
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
