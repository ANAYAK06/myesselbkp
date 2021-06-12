<%@ Page Title="SupplierInvoiceVerificationGST" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="BasicPriceUpdationNewGST.aspx.cs" Inherits="BasicPriceUpdationNewGST" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .container {
            display: flex;
            justify-content: center;
        }

        .center {
            width: 800px;
        }
    </style>
    <script language="javascript">

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
        function numericValidation(txtvalue) {

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
                    if (points == 1) {
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

        function Calculate() {
            //debugger;
            var maingridtotal = 0;
            var transporttotal = 0;
            grd = document.getElementById("<%=grdcentral.ClientID %>");
            grdother = document.getElementById("<%=gvanyother.ClientID %>");
            grddeduction = document.getElementById("<%=gvdeduction.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value.replace(/,/g, "");
            var ftcgst = document.getElementById("<%=hfcgstfooter.ClientID %>").value.replace(/,/g, "");
            var ftsgst = document.getElementById("<%=hfsgsttaxamt.ClientID %>").value.replace(/,/g, "");
            var ftigst = document.getElementById("<%=hfigstfooter.ClientID %>").value.replace(/,/g, "");
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value.replace(/,/g, "");
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value.replace(/,/g, "");
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value.replace(/,/g, "");
            var hftranamount = document.getElementById("<%=hftranamount.ClientID %>").value.replace(/,/g, "");
            var hftrancgst = document.getElementById("<%=hftrancgst.ClientID %>").value.replace(/,/g, "");
            var hftransgst = document.getElementById("<%=hftransgst.ClientID %>").value.replace(/,/g, "");
            var hftranigst = document.getElementById("<%=hftranigst.ClientID %>").value.replace(/,/g, "");
            var hftcs = document.getElementById("<%=txttcsamount.ClientID %>").value.replace(/,/g, "");

            var amt = 0;
            var total = 0;
            var sgstamt = 0;
            var cgstamt = 0;
            var cgsttotal = 0;
            var sgsttotal = 0;
            var igstamt = 0;
            var igsttotal = 0;
            if (hftrancgst == "")
                hftrancgst = 0;
            if (hftransgst == "")
                hftransgst = 0;
            if (hftranigst == "")
                hftranigst = 0;
            if (hftranamount == "")
                hftranamount = 0;
            if (hftcs == "")
                hftcs = 0;

            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[10].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(9).children[0].value)) {
                            amt = (Number(grd.rows(rowCount).cells[7].innerHTML)) * (Number(grd.rows(rowCount).cells[9].children[0].value));
                            grd.rows(rowCount).cells(10).children[0].value = Math.round(amt * Math.pow(10, 2)) / Math.pow(10, 2);
                            total += Number(grd.rows(rowCount).cells(10).children[0].value);

                            cgstamt = amt * (Number(grd.rows(rowCount).cells[11].children[0].value) / 100);
                            grd.rows(rowCount).cells(12).children[0].value = Math.round(cgstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                            cgsttotal += Number(grd.rows(rowCount).cells(12).children[0].value);

                            //samt = ((amt + eamt) * (Number(grd.rows(rowCount).cells[13].children[0].value) / 100));
                            sgstamt = amt * (Number(grd.rows(rowCount).cells[13].children[0].value) / 100);
                            grd.rows(rowCount).cells(14).children[0].value = Math.round(sgstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                            sgsttotal += Number(grd.rows(rowCount).cells(14).children[0].value);

                            igstamt = amt * (Number(grd.rows(rowCount).cells[15].children[0].value) / 100);
                            grd.rows(rowCount).cells(16).children[0].value = Math.round(igstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                            igsttotal += Number(grd.rows(rowCount).cells(16).children[0].value);

                        }
                    }
                    else {
                        total += Number(grd.rows(rowCount).cells(10).children[0].value);
                        cgsttotal += parseFloat(Number(grd.rows(rowCount).cells(12).children[0].value) + hftrancgst);
                        sgsttotal += parseFloat(Number(grd.rows(rowCount).cells(14).children[0].value) + hftransgst);
                        igsttotal += parseFloat(Number(grd.rows(rowCount).cells(16).children[0].value) + hftranigst);
                    }

                }
                grd.rows[grd.rows.length - 1].cells[10].children[0].innerHTML = Math.round(total * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtbasic.ClientID %>").value = Math.round(total * Math.pow(10, 2)) / Math.pow(10, 2);

                grd.rows[grd.rows.length - 1].cells[12].children[0].innerHTML = Math.round(cgsttotal * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtcgst.ClientID %>").value = Math.round((parseFloat(cgsttotal) + parseFloat(hftrancgst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                grd.rows[grd.rows.length - 1].cells[14].children[0].innerHTML = Math.round(sgsttotal * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtsgst.ClientID %>").value = Math.round((parseFloat(sgsttotal) + parseFloat(hftransgst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                grd.rows[grd.rows.length - 1].cells[16].children[0].innerHTML = Math.round(igsttotal * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtigst.ClientID %>").value = Math.round((parseFloat(igsttotal) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                //document.getElementById("<%=txtgst.ClientID %>").value = Math.round((parseFloat(sgsttotal) + parseFloat(cgsttotal) + parseFloat(igsttotal)) * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtgst.ClientID %>").value = Math.round((parseFloat(sgsttotal) + parseFloat(cgsttotal) + parseFloat(igsttotal) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txttotal.ClientID %>").value = Math.round((parseFloat(total) + parseFloat(sgsttotal) + parseFloat(cgsttotal) + parseFloat(igsttotal) + parseFloat(hftranamount) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)+ parseFloat(hftcs)) * Math.pow(10, 2)) / Math.pow(10, 2);

                //document.getElementById("<%=txttotal.ClientID %>").value = Math.round((parseFloat(total) + parseFloat(sgsttotal) + parseFloat(cgsttotal) + parseFloat(igsttotal)) * Math.pow(10, 2)) / Math.pow(10, 2);
                ftamount = total;
                ftcgst = cgsttotal;
                ftsgst = sgsttotal;
                ftigst = igsttotal;
                document.getElementById("<%=hfamount.ClientID %>").value = total;
                document.getElementById("<%=hfcgstfooter.ClientID %>").value = cgsttotal;
                document.getElementById("<%=hfsgsttaxamt.ClientID %>").value = sgsttotal;
                document.getElementById("<%=hfigstfooter.ClientID %>").value = igsttotal;

                maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsgst) + parseFloat(ftcgst) + parseFloat(ftigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                transporttotal = Math.round((parseFloat(hftranamount) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal) - parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers == "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(transporttotal) - parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                calculateother();
                calculatededuction();
            }

        }

        function insufficient(lnk) {
            //debugger;
            var budgetbal = document.getElementById('<%= hbasiprice.ClientID %>').value;
            var Netamount = document.getElementById("<%=txtbasic.ClientID %>").value;
            var hfpoamount = document.getElementById("<%=hfpoamount.ClientID %>").value;
            grd = document.getElementById("<%=grdcentral.ClientID %>");
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var invamt = Netamount - hfpoamount;
            if (parseFloat(budgetbal) < parseFloat(invamt)) {
                if (grd != null) {
                    for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                        if (grd.rows(rowCount).cells(9).children[0].value != grd.rows(rowCount).cells(17).children[0].value) {
                            if (rowCount - 1 == rowIndex) {
                                grd.rows(rowCount).cells(9).children[0].value = grd.rows(rowCount).cells(17).children[0].value;
                                alert("Insufficient Balance");
                                Calculate();
                                //                                document.getElementById("<%=btnmdlupd.ClientID %>").disabled = true;
                                return false;
                            }
                        }
                    }
                }
            }
            else
                document.getElementById("<%=btnmdlupd.ClientID %>").disabled = false;
        }
    </script>
    <script language="javascript">
        function SelectAll1() {
            //debugger;
            var GridView1 = document.getElementById("<%=grdcentral.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                if (GridView1.rows(rowCount).cells(0).children(0).checked == true && GridView1.rows(rowCount).cells(9).children[0].disabled == false) {
                    if (GridView1.rows(rowCount).cells(9).children[0].style.color != 'black') {
                        var response = confirm("Do you want to Change the Invoice Price of " + itemcode + "?");
                        if (response) {

                            GridView1.rows(rowCount).cells(9).children[0].value = "";
                            GridView1.rows(rowCount).cells(9).children[0].style.color = 'black';
                            if (GridView1.rows(rowCount).cells(9).children[0].value == "") {
                                window.alert("Enter new Invoice price");
                                GridView1.rows(rowCount).cells(9).children[0].focus();
                                Calculate();
                                return false;
                            }
                            return false;
                        }
                        else {
                            Calculate();
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

    </script>
    <script language="javascript">
        var maingridtotal = 0;
        var transporttotal = 0;
        function calculateother() {
            ////////debugger            
            grd = document.getElementById("<%=gvanyother.ClientID %>");
            maingrd = document.getElementById("<%=grdcentral.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value.replace(/,/g, "");
            var ftcgst = document.getElementById("<%=hfcgstfooter.ClientID %>").value.replace(/,/g, "");
            var ftsgst = document.getElementById("<%=hfsgsttaxamt.ClientID %>").value.replace(/,/g, "");
            var ftigst = document.getElementById("<%=hfigstfooter.ClientID %>").value.replace(/,/g, "");
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value.replace(/,/g, "");
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value.replace(/,/g, "");
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value.replace(/,/g, "");
            var totalother = 0;
            var hftranamount = document.getElementById("<%=hftranamount.ClientID %>").value.replace(/,/g, "");
            var hftrancgst = document.getElementById("<%=hftrancgst.ClientID %>").value.replace(/,/g, "");
            var hftransgst = document.getElementById("<%=hftransgst.ClientID %>").value.replace(/,/g, "");
            var hftranigst = document.getElementById("<%=hftranigst.ClientID %>").value.replace(/,/g, "");
            var hftcs = document.getElementById("<%=txttcsamount.ClientID %>").value.replace(/,/g, "");
            if (hftrancgst == "")
                hftrancgst = 0;
            if (hftransgst == "")
                hftransgst = 0;
            if (hftranigst == "")
                hftranigst = 0;
            if (hftranamount == "")
                hftranamount = 0;
            if (hftcs == "")
                hftcs = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                    if (Number(grd.rows(rowCount).cells[3].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                            totalother += Number(grd.rows(rowCount).cells(3).children[0].value);
                        }
                    }
                    else {
                        totalother += Number(grd.rows(rowCount).cells(3).children[0].value);
                    }
                }
                grd.rows[grd.rows.length - 1].cells[3].innerHTML = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                hfothers = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                if (maingrd != null) {
                    //Calculate();
                    maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsgst) + parseFloat(ftcgst) + parseFloat(ftigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                    transporttotal = Math.round((parseFloat(hftranamount) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                }
                else if (maingrd == null) {
                    maingridtotal = 0;
                    transporttotal = 0;
                }
                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);

                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(hfothers) + parseFloat(maingridtotal) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal) - parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }

            }
        }


        function calculatededuction() {
            ////////debugger
            grd = document.getElementById("<%=gvdeduction.ClientID %>");
            maingrd = document.getElementById("<%=grdcentral.ClientID %>");
            grdother = document.getElementById("<%=gvanyother.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value.replace(/,/g, "");
            var ftcgst = document.getElementById("<%=hfcgstfooter.ClientID %>").value.replace(/,/g, "");
            var ftsgst = document.getElementById("<%=hfsgsttaxamt.ClientID %>").value.replace(/,/g, "");
            var ftigst = document.getElementById("<%=hfigstfooter.ClientID %>").value.replace(/,/g, "");
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value.replace(/,/g, "");
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value.replace(/,/g, "");
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value.replace(/,/g, "");
            var hftranamount = document.getElementById("<%=hftranamount.ClientID %>").value.replace(/,/g, "");
            var hftrancgst = document.getElementById("<%=hftrancgst.ClientID %>").value.replace(/,/g, "");
            var hftransgst = document.getElementById("<%=hftransgst.ClientID %>").value.replace(/,/g, "");
            var hftranigst = document.getElementById("<%=hftranigst.ClientID %>").value.replace(/,/g, "");
            var hftcs = document.getElementById("<%=txttcsamount.ClientID %>").value.replace(/,/g, "");
            if (hftrancgst == "")
                hftrancgst = 0;
            if (hftransgst == "")
                hftransgst = 0;
            if (hftranigst == "")
                hftranigst = 0;
            if (hftranamount == "")
                hftranamount = 0;
            if (hftcs == "")
                hftcs = 0;
            var totalded = 0;
            var sumtotalnet = 0;
            var otheramt = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                    if (Number(grd.rows(rowCount).cells[3].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                            totalded += Number(grd.rows(rowCount).cells(3).children[0].value);
                        }
                    }
                    else {
                        totalded += Number(grd.rows(rowCount).cells(3).children[0].value);
                    }

                }
                grd.rows[grd.rows.length - 1].cells[3].innerHTML = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=hfdeduction.ClientID %>").value = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                hfdeduction = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                if (maingrd != null) {
                    //Calculate();
                    maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsgst) + parseFloat(ftcgst) + parseFloat(ftigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                    transporttotal = Math.round((parseFloat(hftranamount) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                }
                else if (maingrd == null) {
                    maingridtotal = 0;
                    transporttotal = 0;
                }
                if (grdother != null) {
                    //calculateother();
                    otheramt = hfothers;
                    document.getElementById("<%=hfothers.ClientID %>").value = otheramt;
                }
                else if (grdother == null) {
                    otheramt = 0;
                }
                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(hfothers) + parseFloat(maingridtotal) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal) - parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers == "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(otheramt) + parseFloat(transporttotal) - parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=hfdeduction.ClientID %>").value = hfdeduction;
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
            }
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
    <script language="javascript">
        function Restrictcomma(evt) {
            var e = evt ? evt : event;
            var charCode = e.keyCode ? e.keyCode : e.charCode;
            if (charCode == 44 || charCode == 39)
                return false;
            else
                return true;
        }
    </script>
    <script type="text/javascript">
        function myFunction() {
            ////debugger
            if (confirm("Do you want print") == true) {

                var panel = document.getElementById("<%=pnlprint.ClientID %>");
                var printWindow = window.open('', '', 'left=0,top=0,width=800,height=990,toolbar=0,scrollbars=yes,status =0');
                printWindow.document.write('<html><head><title>DIV Contents</title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(panel.innerHTML);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                setTimeout(function () {
                    printWindow.print();
                }, 500);
                document.getElementById("<%=btnhide.ClientID %>").click();
            }
            else {
                document.getElementById("<%=btnhide.ClientID %>").click();
            }
        }
    </script>
    <script type="text/javascript">
        //debugger;
        var iRowIndex;
        function MouseEvents(objRef, evt, desc) {

            if (evt.type == "mouseover") {
                objRef.style.cursor = 'pointer'
                objRef.style.backgroundColor = "#EEE";
                ShowDiv(desc, 0);

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
            var role = document.getElementById("<%=hfrole1.ClientID %>").value;
            if (evt.type == "mouseover") {
                objRef.style.display = 'block';
                if (role = "Project Manager") {
                    document.getElementById("<%=Grdeditpopup.ClientID %>").rows[iRowIndex].style.backgroundColor = "#641E16";
                }
                if (role == "PurchaseManager" || role == "SuperAdmin") {
                    document.getElementById("<%=gridcmc.ClientID %>").rows[iRowIndex].style.backgroundColor = "#641E16";
                }
                else {
                    document.getElementById("<%=grdcentral.ClientID %>").rows[iRowIndex].style.backgroundColor = "#641E16";
                }
            }
            else {
                if (evt.type == "mouseout") {
                    if (role = "Project Manager") {
                        document.getElementById("<%=Grdeditpopup.ClientID %>").rows[iRowIndex].style.backgroundColor = "#FFF";
                    }
                    else if (role == "PurchaseManager" || role == "SuperAdmin") {
                        document.getElementById("<%=gridcmc.ClientID %>").rows[iRowIndex].style.backgroundColor = "#FFF";
                    }
                    else {
                        document.getElementById("<%=grdcentral.ClientID %>").rows[iRowIndex].style.backgroundColor = "#FFF";
                    }
                    objRef.style.display = 'none';
                }
            }
        }
    </script>
    <style type="text/css">
        .divDetail {
            float: right;
            font: inherit;
            font-size: 13px;
            padding: 2px 5px;
            width: 500px;
            border: solid 2px #A93226;
            -moz-border-radius: 0 7px 7px 0;
            -webkit-border-radius: 0 7px 7px 0;
            border-radius: 0 7px 7px 0;
            display: block;
            color: #78281F;
            background-color: #D5DBDB;
        }

            .divDetail p {
                font: inherit;
            }

            .divDetail a {
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
                                                                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                                                            <ProgressTemplate>
                                                                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                                        <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                                                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
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
                                                                                            <asp:HiddenField ID="hfrole1" runat="server" />
                                                                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                PagerStyle-CssClass="grid pagerbar" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                                                OnDataBound="GridView1_DataBound" DataKeyNames="po_no,MRR_no,cc_code" OnRowEditing="GridView1_RowEditing2"
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
                                                                                        <table id="popup" runat="server" style="border-color: black; border-style: solid; border-width: 1px;">
                                                                                            <tr valign="top">
                                                                                                <td>
                                                                                                    <asp:Panel ID="pnlprint" runat="server">
                                                                                                        <table border="0" width="100%">
                                                                                                            <tr align="center" style="background-color: maroon; height: 5px">
                                                                                                                <td colspan="9">
                                                                                                                    <asp:Label ID="lbltitle" runat="server" Font-Bold="true" ForeColor="White"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="2" width="100%">
                                                                                                                    <table border="0" class="fields" width="100%">
                                                                                                                        <tbody>
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="1%">
                                                                                                                                    <label>
                                                                                                                                        Vendor Type
                                                                                                                                    </label>
                                                                                                                                    :
                                                                                                                                </td>
                                                                                                                                <td class="item item-m2o" valign="middle" width="31%">
                                                                                                                                    <asp:DropDownList ID="ddlvendortype" runat="server" CssClass="filter_item" ToolTip="Vendor Type">
                                                                                                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                                                                        <asp:ListItem Value="Trade Purchasing">Trade Purchasing</asp:ListItem>
                                                                                                                                        <asp:ListItem Value="Supplier">Supplier</asp:ListItem>
                                                                                                                                    </asp:DropDownList>
                                                                                                                                </td>
                                                                                                                                <td class="label" width="1%">
                                                                                                                                    <label>
                                                                                                                                        MRR No
                                                                                                                                    </label>
                                                                                                                                    :
                                                                                                                                </td>
                                                                                                                                <td class="item item-m2o" valign="middle" width="31%">
                                                                                                                                    <asp:TextBox ID="txtmrr" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td class="label" width="1%">
                                                                                                                                    <label class="help">
                                                                                                                                        Vendor Id
                                                                                                                                    </label>
                                                                                                                                </td>
                                                                                                                                <td class="item item-char" valign="middle" width="31%">
                                                                                                                                    <asp:TextBox ID="txtvendorid" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td align="center" class="label">
                                                                                                                                    <label for="journal_id">
                                                                                                                                        Vendor Name
                                                                                                                                    </label>
                                                                                                                                    :
                                                                                                                                </td>
                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtvendorname" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td class="label" width="1%">
                                                                                                                                    <label for="name">
                                                                                                                                        DCA Code
                                                                                                                                    </label>
                                                                                                                                    :
                                                                                                                                </td>
                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                    <span class="filter_item">
                                                                                                                                        <asp:TextBox ID="txtdca" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                    </span>
                                                                                                                                </td>
                                                                                                                                <td class="label" width="1%">
                                                                                                                                    <label class="help" for="reference">
                                                                                                                                        Vendor GST No
                                                                                                                                    </label>
                                                                                                                                </td>
                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                    <span class="filter_item">
                                                                                                                                        <asp:TextBox ID="txtvendorgst" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="1%">
                                                                                                                                    <label class="help" for="reference">
                                                                                                                                        GST Type
                                                                                                                                    </label>
                                                                                                                                    :
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:RadioButtonList ID="rbtngsttype" CssClass="esrbtn" Style="font-size: x-small"
                                                                                                                                        Width="170px" ToolTip="Gst Type" RepeatDirection="Horizontal" runat="server"
                                                                                                                                        OnSelectedIndexChanged="rbtngsttype_SelectedIndexChanged" AutoPostBack="true">
                                                                                                                                        <asp:ListItem Value="Creditable" Enabled="false">Creditable</asp:ListItem>
                                                                                                                                        <asp:ListItem Value="Non-Creditable" Selected="False" Enabled="false">Non-Creditable</asp:ListItem>
                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                </td>
                                                                                                                                <td class="label" colspan="2" width="15px">
                                                                                                                                    <label>
                                                                                                                                        GST Nos
                                                                                                                                    </label>
                                                                                                                                    :
                                                                                                                                    <asp:TextBox ID="txtgstnos" runat="server" CssClass="char" Width="250px" Enabled="false"></asp:TextBox>
                                                                                                                                    <%-- <asp:DropDownList ID="ddlgstnos" Font-Size="7" Width="200px" CssClass="filter_item"
                                                                                                                                        ToolTip="GST No" runat="server">
                                                                                                                                        <asp:ListItem Enabled="true" Selected="True" Text="select"></asp:ListItem>
                                                                                                                                    </asp:DropDownList>--%>
                                                                                                                                </td>
                                                                                                                                <td colspan="2" style="width: 390px"></td>
                                                                                                                            </tr>
                                                                                                                        </tbody>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="CST" runat="server">
                                                                                                                <td colspan="2">
                                                                                                                    <asp:GridView ID="Grdeditpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                        AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" DataKeyNames="id,item_code"
                                                                                                                        HeaderStyle-CssClass="grid-header" OnRowDataBound="Grdeditpopup_RowDataBound"
                                                                                                                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                        ShowFooter="true">
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
                                                                                                                            <asp:BoundField DataField="Quantity" HeaderText="Receieved Qty" ItemStyle-Width="25px" />
                                                                                                                            <asp:BoundField DataField="newbasicprice" HeaderStyle-Width="50px" HeaderText="New Purchased Price"
                                                                                                                                ItemStyle-Width="50px" />
                                                                                                                            <asp:BoundField DataField="Amount" HeaderStyle-Width="50px" HeaderText="Amount" ItemStyle-Width="50px" />
                                                                                                                            <asp:BoundField DataField="CGSTPercent" HeaderStyle-Width="50px" HeaderText="CGST %"
                                                                                                                                ItemStyle-Width="50px" />
                                                                                                                            <asp:BoundField DataField="CGSTAmount" HeaderStyle-Width="50px" HeaderText="CGST Amt"
                                                                                                                                ItemStyle-Width="50px" />
                                                                                                                            <asp:BoundField DataField="SGSTPercent" HeaderStyle-Width="50px" HeaderText="SGST %"
                                                                                                                                ItemStyle-Width="50px" />
                                                                                                                            <asp:BoundField DataField="SGSTAmount" HeaderStyle-Width="50px" HeaderText="SGST Amt"
                                                                                                                                ItemStyle-Width="50px" />
                                                                                                                            <asp:BoundField DataField="IGSTPercent" HeaderStyle-Width="50px" HeaderText="IGST %"
                                                                                                                                ItemStyle-Width="50px" />
                                                                                                                            <asp:BoundField DataField="IGSTAmount" HeaderStyle-Width="50px" HeaderText="IGST Amt"
                                                                                                                                ItemStyle-Width="50px" />
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("basic_price")%>' />
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                    </asp:GridView>
                                                                                                                    <asp:HiddenField ID="hbasiprice" runat="server" Value="0" />
                                                                                                                    <asp:GridView ID="grdcentral" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                        AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" DataKeyNames="id,item_code"
                                                                                                                        HeaderStyle-CssClass="grid-header" OnRowDataBound="grdcentral_RowDataBound" PagerStyle-CssClass="grid pagerbar"
                                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" ShowFooter="true">
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
                                                                                                                                    <asp:TextBox ID="txtbasic" runat="server" onkeypress="IsNumeric2(event)" onkeyup="checkrolescentralgrid();Calculate();insufficient(this);"
                                                                                                                                        Text='<%#Bind("newbasicprice") %>' Width="60px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="txtamount" runat="server" Enabled="false" Text='<%#Bind("Amount") %>'
                                                                                                                                        Width="50px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CGST %">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="txtcgst" runat="server" Enabled="false" onkeypress="return numericValidation(this);"
                                                                                                                                        onkeyup="Calculate();" Text='<%#Bind("CGSTPercent") %>' Width="50px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lblfootercgstfooter" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CGST Amt">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="lblcgstamt" runat="server" Enabled="false" onkeypress="return numericValidation(this);"
                                                                                                                                        onkeyup="Calculate();" Text='<%#Bind("CGSTAmount") %>' Width="50px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lblcgstamountfooter" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="SGST %">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="txtsgst" runat="server" Enabled="false" onkeypress="return numericValidation(this);"
                                                                                                                                        onkeyup="Calculate();" Text='<%#Bind("SGSTPercent") %>' Width="50px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="SGST Amt">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="lblamt" runat="server" Enabled="false" onkeypress="return numericValidation(this);"
                                                                                                                                        onkeyup="Calculate();" Text='<%#Bind("SGSTAmount") %>' Width="50px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="IGST%" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="txtigst" runat="server" Enabled="false" Text='<%#Bind("IGSTPercent") %>'
                                                                                                                                        onkeyup="Calculate();" onkeypress='return numericValidation(this);' Width="50px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lblfooterigstfooter" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText=" IGST Amt" ItemStyle-HorizontalAlign="Center">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lbligstamt" onkeyup="Calculate();" Enabled="false" Text='<%#Bind("IGSTAmount") %>'
                                                                                                                                        runat="server"></asp:Label>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lbligstamountfooter" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
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
                                                                                                                        AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" DataKeyNames="item_code"
                                                                                                                        HeaderStyle-CssClass="grid-header" OnRowDataBound="gridcmc_RowDataBound1" PagerStyle-CssClass="grid pagerbar"
                                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" ShowFooter="true">
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
                                                                                                                                    <asp:TextBox ID="txtbasic" runat="server" ForeColor="Black" onkeypress="IsNumeric1(event)"
                                                                                                                                        onkeyup="checkroles();CalculateCMC_SA();" Text='<%#Eval("basic_price") %>' Width="50px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="New Purchased Price">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="txtinvoiceprice" runat="server" onkeypress="return numericValidation(this);"
                                                                                                                                        onkeyup="checkroles();CalculateCMC_SA();insufficient(this);" Text='<%#Eval("newbasicprice") %>'
                                                                                                                                        Width="50px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                                                                            <asp:TemplateField HeaderText="CGST% ">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="txtcgst" runat="server" onkeypress="return numericValidation(this);"
                                                                                                                                        onkeyup="CalculateCMC_SA();" Text='<%#Bind("CGSTPercent") %>' Width="40px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lblfootercgstfooter" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CGST Amt">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="lblcgstamt" Enabled="false" runat="server" onkeypress="return numericValidation(this);"
                                                                                                                                        Text='<%#Bind("CGSTAmount") %>' Width="40px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lblcgstamountfooter" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="SGST%">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="txtsgst" runat="server" onkeypress="return numericValidation(this);"
                                                                                                                                        onkeyup="CalculateCMC_SA();" Text='<%#Bind("SGSTPercent") %>' Width="40px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <%-- <asp:BoundField DataField="STaxPercent" HeaderText="Sales Tax %" />--%>
                                                                                                                            <asp:BoundField DataField="SGSTAmount" HeaderText="SGST Amt" />
                                                                                                                            <asp:TemplateField HeaderText="IGST% ">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="txtigst" runat="server" onkeypress="return numericValidation(this);"
                                                                                                                                        onkeyup="CalculateCMC_SA();" Text='<%#Bind("IGSTPercent") %>' Width="40px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lblfooterigstfooter" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="IGST Amt">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:TextBox ID="lbligstamt" runat="server" onkeypress="return numericValidation(this);"
                                                                                                                                        Enabled="false" Text='<%#Bind("IGSTAmount") %>' Width="40px"></asp:TextBox>
                                                                                                                                </ItemTemplate>
                                                                                                                                <FooterTemplate>
                                                                                                                                    <asp:Label ID="lbligstamountfooter" runat="server" Text=""></asp:Label>
                                                                                                                                </FooterTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderStyle-Width="1px" ItemStyle-Width="1px">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("basic_price")%>' />
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderStyle-Width="1px" ItemStyle-Width="1px">
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
                                                                                                            <tr>
                                                                                                                <td colspan="2">
                                                                                                                    <div runat="server" id="divDetail" class="divDetail" onmouseover="highlight(this, event)"
                                                                                                                        onmouseout="highlight(this, event)">
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="trtransportheader" runat="server" style="background-color: White;">
                                                                                                                <td colspan="2" valign="top" class=" item-group" width="100%">
                                                                                                                    <label class="help" style="font-weight: bold; font-size: 13px">
                                                                                                                        Transportation:
                                                                                                                    </label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="trtransportlbl" runat="server">
                                                                                                                <td colspan="2" valign="top" class=" item-group" width="100%">
                                                                                                                    <table>
                                                                                                                        <tr align="center">
                                                                                                                            <td style="width: 150px;">
                                                                                                                                <label class="help">
                                                                                                                                    DCA
                                                                                                                                </label>
                                                                                                                            </td>
                                                                                                                            <td style="width: 200px;">
                                                                                                                                <label class="help">
                                                                                                                                    SDCA
                                                                                                                                </label>
                                                                                                                            </td>
                                                                                                                            <td style="width: 150px">
                                                                                                                                <label class="help">
                                                                                                                                    Transport Amount
                                                                                                                                </label>
                                                                                                                            </td>
                                                                                                                            <td style="width: 150px">
                                                                                                                                <label class="help">
                                                                                                                                    CGST Amt
                                                                                                                                </label>
                                                                                                                            </td>
                                                                                                                            <td style="width: 150px">
                                                                                                                                <label class="help">
                                                                                                                                    SGST Amt
                                                                                                                                </label>
                                                                                                                            </td>
                                                                                                                            <td style="width: 150px">
                                                                                                                                <label class="help">
                                                                                                                                    IGST Amt
                                                                                                                                </label>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="trtransportdata" runat="server">
                                                                                                                <td colspan="2" valign="top" class=" item-group" width="100%">
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="txttrandca" runat="server" Width="150px" CssClass="char" Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="txttransdca" runat="server" Width="150px" CssClass="char" Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                                &nbsp;&nbsp;&nbsp;
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <span class="filter_item">
                                                                                                                                    <asp:TextBox ID="txttransamt" runat="server" Width="150px" CssClass="char" Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                                                                                </span>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <span class="filter_item">
                                                                                                                                    <asp:TextBox ID="transcgstAmt" runat="server" CssClass="char" Width="150px" Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                                                                                </span>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <span class="filter_item">
                                                                                                                                    <asp:TextBox ID="transsgstamt" runat="server" CssClass="char" Width="150px" Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                                                                                </span>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <span class="filter_item">
                                                                                                                                    <asp:TextBox ID="transigstamount" runat="server" CssClass="char" Width="150px" Enabled="false"></asp:TextBox>
                                                                                                                                </span>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class=" item-notebook" valign="top" width="100%">
                                                                                                                    <div id="_notebook_1" class="notebook" style="display: block;">
                                                                                                                        <div class="notebook-tabs">
                                                                                                                            <div class="right scroller">
                                                                                                                            </div>
                                                                                                                            <div class="left scroller">
                                                                                                                            </div>
                                                                                                                            <ul class="notebook-tabs-strip">
                                                                                                                                <li id="none" class="notebook-tab notebook-page notebook-tab-active" title=""><span
                                                                                                                                    class="tab-title"><span>Invoice Info</span></span></li>
                                                                                                                                <li class="notebook-tab notebook-page" style="display: none;" title=""><span class="tab-title">
                                                                                                                                    <span>Journal Items</span></span></li>
                                                                                                                            </ul>
                                                                                                                        </div>
                                                                                                                        <div class="notebook-pages">
                                                                                                                            <div class="notebook-page notebook-page-active">
                                                                                                                                <div>
                                                                                                                                    <table border="0" width="100%">
                                                                                                                                        <tbody>
                                                                                                                                            <tr align="center">
                                                                                                                                                <td class="label1">
                                                                                                                                                    <label class="help">
                                                                                                                                                        PO NO:
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtpo" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        Invoice No:
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" colspan="3" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtin" runat="server" CssClass="char" onKeyDown="javascript: return false;"
                                                                                                                                                        onkeypress="javascript:return Restrictcomma(event);" ToolTip="Invoice No"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr align="center">
                                                                                                                                                <td class="label1">
                                                                                                                                                    <label class="help">
                                                                                                                                                        Invoice Date:
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtindt" runat="server" CssClass="char" onKeyDown="javascript: return false;"
                                                                                                                                                        onKeyPress="javascript: return false;" ToolTip="Invoice Date"></asp:TextBox>
                                                                                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" CssClass="cal_Theme1"
                                                                                                                                                        FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" PopupButtonID="txtindt" TargetControlID="txtindt">
                                                                                                                                                    </cc1:CalendarExtender>
                                                                                                                                                </td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        Invoice Making Date:
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtindtmk" runat="server" CssClass="char" onKeyDown="javascript: return false;"
                                                                                                                                                        onKeyPress="javascript: return false;" ToolTip="Invoice Making Date"></asp:TextBox>
                                                                                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" CssClass="cal_Theme1"
                                                                                                                                                        FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" PopupButtonID="txtindtmk" TargetControlID="txtindtmk">
                                                                                                                                                    </cc1:CalendarExtender>
                                                                                                                                                </td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        Basic Value:
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtbasic" runat="server" Enabled="false" CssClass="char" onKeyDown="javascript: return false;"
                                                                                                                                                        onKeyPress="javascript: return false;" ToolTip="Basic Value"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr align="center">
                                                                                                                                                <td class="label1">
                                                                                                                                                    <label class="help">
                                                                                                                                                        CGST :
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtcgst" runat="server" CssClass="char" Enabled="false" onKeyPress="javascript: return false;"
                                                                                                                                                        onKeyDown="javascript: return false;"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        SGST :
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtsgst" runat="server" onKeyPress="javascript: return false;" Enabled="false"
                                                                                                                                                        onKeyDown="javascript: return false;" CssClass="char"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        IGST :
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtigst" runat="server" onKeyPress="javascript: return false;" Enabled="false"
                                                                                                                                                        onKeyDown="javascript: return false;" CssClass="char"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr align="center">
                                                                                                                                                <td class="label1">
                                                                                                                                                    <label class="help">
                                                                                                                                                        GST Value :
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtgst" runat="server" onKeyPress="javascript: return false;" Enabled="false"
                                                                                                                                                        onKeyDown="javascript: return false;" CssClass="char"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        Is TCS Applicable ? :
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txttcsapplicable" runat="server" onKeyPress="javascript: return false;" Enabled="false"
                                                                                                                                                        onKeyDown="javascript: return false;" CssClass="char"></asp:TextBox>

                                                                                                                                                </td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        TCS Amount :
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txttcsamount" runat="server" onKeyPress="javascript: return false;" Enabled="false"
                                                                                                                                                        onKeyDown="javascript: return false;" CssClass="char"></asp:TextBox>

                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr align="center">
                                                                                                                                                <td class="label1"></td>
                                                                                                                                                <td class="item item-char" valign="middle"></td>
                                                                                                                                                <td class="label" width="1%"></td>
                                                                                                                                                <td class="item item-char" valign="middle"></td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        Invoice Total :
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txttotal" runat="server" Enabled="false" CssClass="char" onKeyDown="javascript: return false;"
                                                                                                                                                        onKeyPress="javascript: return false;" ToolTip="Invoice Total"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr align="left">
                                                                                                                                                <td colspan="6" style="border-bottom: 1px solid #525254;" valign="middle" width="100%">
                                                                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                                                        Other Charges
                                                                                                                                                    </div>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <div class="container">
                                                                                                                                                <div class="center">
                                                                                                                                                    <tr>
                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                            <asp:RadioButtonList ID="rbtnothercharges" runat="server" AutoPostBack="true" ClientIDMode="AutoID"
                                                                                                                                                                CssClass="esrbtn" OnSelectedIndexChanged="rbtnothercharges_SelectedIndexChanged"
                                                                                                                                                                RepeatDirection="Horizontal" Style="font-size: x-small" ToolTip="Other Charges Yes or No"
                                                                                                                                                                Width="170px">
                                                                                                                                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                                                                                                                <asp:ListItem Value="No">No</asp:ListItem>
                                                                                                                                                            </asp:RadioButtonList>
                                                                                                                                                        </td>
                                                                                                                                                        <td id="tdddlanyotherdcas" runat="server" valign="middle">
                                                                                                                                                            <asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                                                                                                                                                            <asp:TextBox ID="TextBox1" runat="server" onKeyDown="javascript: return false;" onKeyPress="javascript: return false;"
                                                                                                                                                                ReadOnly="true" ToolTip="Other DCA" Width="400px"></asp:TextBox>
                                                                                                                                                            <cc1:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True"
                                                                                                                                                                ExtenderControlID="" OffsetY="22" PopupControlID="Panel1" TargetControlID="TextBox1">
                                                                                                                                                            </cc1:PopupControlExtender>
                                                                                                                                                            <asp:Panel ID="Panel1" runat="server" BackColor="LightGray" BorderStyle="Solid" BorderWidth="2px"
                                                                                                                                                                Direction="LeftToRight" Height="180px" ScrollBars="Auto" Style="display: none"
                                                                                                                                                                Width="400px">
                                                                                                                                                                <asp:CheckBoxList ID="ddlanyotherdcas" runat="server" AutoPostBack="True" BackColor="LightGray"
                                                                                                                                                                    CellPadding="5" CellSpacing="5" OnSelectedIndexChanged="ddlanyotherdcas_SelectedIndexChanged"
                                                                                                                                                                    RepeatColumns="4" RepeatDirection="Horizontal" Width="400px">
                                                                                                                                                                </asp:CheckBoxList>
                                                                                                                                                            </asp:Panel>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr id="tranyothergrid" runat="server">
                                                                                                                                                        <td colspan="7">
                                                                                                                                                            <asp:GridView ID="gvanyother" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                                AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" DataKeyNames="dca_code"
                                                                                                                                                                HeaderStyle-CssClass="grid-header" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="OnRowDataBound"
                                                                                                                                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                                ShowFooter="true" Width="780px">
                                                                                                                                                                <Columns>
                                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:CheckBox ID="chkSelectother" runat="server" />
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:BoundField DataField="mapdca_code" Visible="True" />
                                                                                                                                                                    <asp:TemplateField HeaderText="Sub Dca">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:DropDownList ID="ddlsdca" runat="server" ToolTip="SubDCA" Width="200px">
                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Amount">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtotheramount" runat="server" onkeypress="return numericValidation(this);"
                                                                                                                                                                                onkeyup="CalculateCMC_SA();" onpaste="return false;" Text='<%# String.Format("{0:f2}",DataBinder.Eval(Container.DataItem,"amount")) %>'
                                                                                                                                                                                Width="100px"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                        <FooterTemplate>
                                                                                                                                                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                                                                                                                        </FooterTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField>
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:HiddenField ID="hsubdcaother" runat="server" Value='<%#Eval("subdca_code")%>' />
                                                                                                                                                                        </ItemTemplate>
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
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                            <tr align="left">
                                                                                                                                                <td colspan="6" style="border-bottom: 1px solid #525254;" valign="middle" width="100%">
                                                                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                                                        <asp:Label ID="Label3" runat="server" Text="Any Deduction"></asp:Label>
                                                                                                                                                    </div>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="6" style="height: 10px" valign="middle" width="100%"></td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <asp:RadioButtonList ID="rbtndeduction" runat="server" AutoPostBack="true" CssClass="esrbtn"
                                                                                                                                                        OnSelectedIndexChanged="rbtndeduction_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                                                                                                        Style="font-size: x-small" ToolTip="Deduction Type Yes or No" Width="170px">
                                                                                                                                                        <asp:ListItem>Yes</asp:ListItem>
                                                                                                                                                        <asp:ListItem>No</asp:ListItem>
                                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                                </td>
                                                                                                                                                <td id="tddeduction" runat="server" valign="middle">
                                                                                                                                                    <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
                                                                                                                                                    <asp:TextBox ID="TextBox2" runat="server" onKeyDown="javascript: return false;" onKeyPress="javascript: return false;"
                                                                                                                                                        ReadOnly="true" ToolTip="Deduction DCA" Width="400px"></asp:TextBox>
                                                                                                                                                    <cc1:PopupControlExtender ID="TextBox2_PopupControlExtender" runat="server" Enabled="True"
                                                                                                                                                        ExtenderControlID="" OffsetY="22" PopupControlID="Panel2" TargetControlID="TextBox2">
                                                                                                                                                    </cc1:PopupControlExtender>
                                                                                                                                                    <asp:Panel ID="Panel2" runat="server" BackColor="LightGray" BorderStyle="Solid" BorderWidth="2px"
                                                                                                                                                        Direction="LeftToRight" Height="180px" ScrollBars="Auto" Style="display: none"
                                                                                                                                                        Width="400px">
                                                                                                                                                        <asp:CheckBoxList ID="ddldeduction" runat="server" AutoPostBack="True" BackColor="LightGray"
                                                                                                                                                            CellPadding="5" CellSpacing="5" OnSelectedIndexChanged="ddldeduction_SelectedIndexChanged"
                                                                                                                                                            RepeatColumns="4" RepeatDirection="Horizontal" Width="400px">
                                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                                    </asp:Panel>
                                                                                                                                                </td>
                                                                                                                                                <%-- <td id="tddedadd" runat="server">
                                                                                                                                                    <asp:Button ID="btndeduction" Text="Add" runat="server" CssClass="button" OnClick="Submitdeduction"
                                                                                                                                                        OnClientClick="javascript:return checkdeduction();" />
                                                                                                                                                </td>--%>
                                                                                                                                            </tr>
                                                                                                                                            <tr id="trdeductiongrid" runat="server">
                                                                                                                                                <td colspan="7">
                                                                                                                                                    <asp:GridView ID="gvdeduction" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                        AutoGenerateColumns="false" BorderColor="White" CssClass="grid-content" DataKeyNames="dca_code"
                                                                                                                                                        HeaderStyle-CssClass="grid-header" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="OnRowDataBoundgvdeduction"
                                                                                                                                                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                        ShowFooter="true" Width="780px">
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:CheckBox ID="chkSelectdeduction" runat="server" />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:BoundField DataField="mapdca_code" Visible="True" />
                                                                                                                                                            <asp:TemplateField HeaderText="Sub Dca">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:DropDownList ID="ddlsdca" runat="server" ToolTip="SubDCA" widht="400px" Width="200px">
                                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtamountdeduction" runat="server" onkeypress="return numericValidation(this);"
                                                                                                                                                                        onkeyup="CalculateCMC_SA();" onpaste="return false;" Text='<%# String.Format("{0:f2}",DataBinder.Eval(Container.DataItem,"amount")) %>'
                                                                                                                                                                        Width="100px"></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                <FooterTemplate>
                                                                                                                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                                                                                                                </FooterTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:HiddenField ID="hsubdcadeduction" runat="server" Value='<%#Eval("subdca_code")%>' />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                        </Columns>
                                                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="label" width="1%"></td>
                                                                                                                                                <td class="label" width="1%"></td>
                                                                                                                                                <td class="label" width="1%"></td>
                                                                                                                                                <td class="label" width="1%"></td>
                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                    <label class="help">
                                                                                                                                                        Net Amount:
                                                                                                                                                    </label>
                                                                                                                                                </td>
                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                    <asp:TextBox ID="txtnetAmount" runat="server" CssClass="char" onKeyDown="javascript: return false;"
                                                                                                                                                        onKeyPress="javascript: return false;" Enabled="false" ToolTip="NetAmount"></asp:TextBox>
                                                                                                                                                    <asp:HiddenField ID="hfnetamt" runat="server" />
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="6" style="height: 10px" valign="middle" width="100%"></td>
                                                                                                                                            </tr>
                                                                                                                                            <tr id="Trgvusers" runat="server">
                                                                                                                                                <td colspan="7">
                                                                                                                                                    <asp:GridView runat="server" ID="gvusers" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                                        PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                                        DataKeyNames="" GridLines="none" Width="970px" ShowHeaderWhenEmpty="true" OnRowDataBound="gvusers_RowDataBound">
                                                                                                                                                        <HeaderStyle CssClass="headerstyle" />
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:BoundField DataField="" ItemStyle-Width="300px" HeaderText="Status" ItemStyle-Wrap="false" />
                                                                                                                                                            <asp:BoundField DataField="" ItemStyle-Width="300px" HeaderText="Role" ItemStyle-Wrap="false" />
                                                                                                                                                            <asp:BoundField DataField="" ItemStyle-Width="400px" HeaderText="Name" ItemStyle-Wrap="false" />
                                                                                                                                                            <%--  <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="UserID" ItemStyle-Wrap="false" />--%>
                                                                                                                                                        </Columns>
                                                                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                                                                                                    </asp:GridView>
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
                                                                                                        <table id="tblsbmt" runat="server" align="center" class="estbl" width="100%">
                                                                                                            <tr align="center">
                                                                                                                <td align="center">
                                                                                                                    <asp:Button ID="btnmdlupd" runat="server" CssClass="button" OnClick="btnmdlupd_Click"
                                                                                                                        Text="" OnClientClick="javascript:return validate();" />
                                                                                                                    <asp:Button ID="btnreject" runat="server" CssClass="button" Text="Reject" OnClientClick="javascript:return validate1();"
                                                                                                                        OnClick="btnreject_Click" />
                                                                                                                    <asp:HiddenField ID="hfpoamount" runat="server" />
                                                                                                                    <asp:Button ID="btnPrint" runat="server" OnClientClick="return PrintPanel();" Text="Print" />
                                                                                                                    <asp:Button runat="server" ID="btnhide" Text="" Style="display: none;" OnClick="btnhide_Click" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="pagerbar">
                                                                                    <td class="pagerbar-cell" align="right"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <asp:HiddenField ID="hftranamount" runat="server" />
                                                                        <asp:HiddenField ID="hftrancgst" runat="server" />
                                                                        <asp:HiddenField ID="hftransgst" runat="server" />
                                                                        <asp:HiddenField ID="hftranigst" runat="server" />

                                                                        <asp:HiddenField ID="hfamount" runat="server" />
                                                                        <asp:HiddenField ID="hfcgstfooter" runat="server" />
                                                                        <asp:HiddenField ID="hfsgsttaxamt" runat="server" />
                                                                        <asp:HiddenField ID="hfigstfooter" runat="server" />
                                                                        <asp:HiddenField ID="hfothers" runat="server" />
                                                                        <asp:HiddenField ID="hfdeduction" runat="server" />
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
    <script language="javascript">
        function validate() {
            //debugger
            var GridView = document.getElementById("<%=Grdeditpopup.ClientID %>");
            var GridView1 = document.getElementById("<%=grdcentral.ClientID %>");
            var GridView2 = document.getElementById("<%=gridcmc.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID%>").value;
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                }
            }
            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    if (GridView1.rows(rowCount).cells[9].children[0].value == "") {
                        window.alert("Enter new basic price");
                        return false;
                    }
                }
            }
            if (GridView2 != null) {

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
                    else if (GridView2.rows(rowCount).cells(11).children[0].disabled == false) {
                        if (GridView2.rows(rowCount).cells(11).children[0].value == "") {
                            window.alert("Enter CGST Percent");
                            GridView2.rows(rowCount).cells(11).children[0].focus();
                            return false;
                        }
                        else if (GridView2.rows(rowCount).cells(13).children[0].disabled == false) {
                            if (GridView2.rows(rowCount).cells(13).children[0].value == "") {
                                window.alert("Enter SGST Percent");
                                GridView2.rows(rowCount).cells(13).children[0].focus();
                                return false;
                            }
                        }
                    }
                    else if (GridView2.rows(rowCount).cells(13).children[0].disabled == false) {
                        if (GridView2.rows(rowCount).cells(13).children[0].value == "") {
                            window.alert("Enter SGST Percent");
                            GridView2.rows(rowCount).cells(13).children[0].focus();
                            return false;
                        }
                    }
                    else if (GridView2.rows(rowCount).cells(15).children[0].disabled == false) {
                        if (GridView2.rows(rowCount).cells(15).children[0].value == "") {
                            window.alert("Enter IGST Percent");
                            GridView2.rows(rowCount).cells(15).children[0].focus();
                            return false;
                        }
                    }

                }
                var obj = new Array("<%=txtindtmk.ClientID %>");
                if (!CheckInputs(obj)) {
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
                    document.getElementById("<%=txtindtmk.ClientID %>").value = "";
                    document.getElementById("<%=txtindtmk.ClientID %>").focus();
                    return false;
                }
                if (!ChceckRBL("<%=rbtnothercharges.ClientID %>")) {
                    return false;
                }
                //debugger;
                if (SelectedIndex("<%=rbtnothercharges.ClientID %>") == 0) {
                    var gvother = document.getElementById("<%=gvanyother.ClientID %>");
                    if (gvother != null) {
                        for (var rowCount = 1; rowCount < gvother.rows.length - 1; rowCount++) {
                            if (gvother.rows(rowCount).cells(0).children(0).checked == false) {
                                window.alert("Please verify");
                                return false;
                            }
                            else if (gvother.rows(rowCount).cells(2).children[0].value == "Select SDCA") {
                                window.alert("Select SubDca");
                                gvother.rows(rowCount).cells(2).children[0].focus();
                                return false;
                            }
                            else if (gvother.rows(rowCount).cells(3).children[0].value == "0.00") {
                                window.alert("Enter Amount");
                                gvother.rows(rowCount).cells(3).children[0].focus();
                                return false;
                            }
                            else if (gvother.rows(rowCount).cells(3).children[0].value == "") {
                                window.alert("Enter Amount");
                                gvother.rows(rowCount).cells(3).children[0].focus();
                                return false;
                            }

                        }
                    }
                    else {
                        window.alert("Select Other DCAs");
                        return false;
                    }

                }
                if (!ChceckRBL("<%=rbtndeduction.ClientID %>")) {
                    return false;
                }
                ///debugger
                if (SelectedIndex("<%=rbtndeduction.ClientID %>") == 0) {
                    var gvded = document.getElementById("<%=gvdeduction.ClientID %>");
                    if (gvded != null) {
                        for (var rowCount = 1; rowCount < gvded.rows.length - 1; rowCount++) {
                            if (gvded.rows(rowCount).cells(0).children(0).checked == false) {
                                window.alert("Please verify");
                                return false;
                            }
                            else if (gvded.rows(rowCount).cells(2).children[0].value == "Select SDCA") {
                                window.alert("Select SubDCA");
                                gvded.rows(rowCount).cells(2).children[0].focus();
                                return false;
                            }
                            else if (gvded.rows(rowCount).cells(3).children[0].value == "0.00") {
                                window.alert("Enter Amount");
                                gvded.rows(rowCount).cells(3).children[0].focus();
                                return false;
                            }
                            else if (gvded.rows(rowCount).cells(3).children[0].value == "") {
                                window.alert("Enter Amount");
                                gvded.rows(rowCount).cells(3).children[0].focus();
                                return false;
                            }

                        }
                    }
                    else {
                        window.alert("Select Deduction DCAs");
                        return false;
                    }

                }
            }
            document.getElementById("<%=btnmdlupd.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        var maingridtotal = 0;
        var transporttotal = 0;
        function CalculateCMC_SA() {
            //debugger           
            grd = document.getElementById("<%=gridcmc.ClientID %>");
            grdother = document.getElementById("<%=gvanyother.ClientID %>");
            grddeduction = document.getElementById("<%=gvdeduction.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value;
            var ftcgst = document.getElementById("<%=hfcgstfooter.ClientID %>").value;
            var ftsgst = document.getElementById("<%=hfsgsttaxamt.ClientID %>").value;
            var ftigst = document.getElementById("<%=hfigstfooter.ClientID %>").value;
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value;
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value.replace(/,/g, "");
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value;
            var role = document.getElementById("<%=hfrole.ClientID %>").value;

            var hftranamount = document.getElementById("<%=hftranamount.ClientID %>").value.replace(/,/g, "");
            var hftrancgst = document.getElementById("<%=hftrancgst.ClientID %>").value.replace(/,/g, "");
            var hftransgst = document.getElementById("<%=hftransgst.ClientID %>").value.replace(/,/g, "");
            var hftranigst = document.getElementById("<%=hftranigst.ClientID %>").value.replace(/,/g, "");
            var hftcs = document.getElementById("<%=txttcsamount.ClientID %>").value.replace(/,/g, "");
            var amt = 0;
            var total = 0;
            var sgstamt = 0;
            var cgstamt = 0;
            var cgsttotal = 0;
            var sgsttotal = 0;
            var igstamt = 0;
            var igsttotal = 0;
            if (hftrancgst == "")
                hftrancgst = 0;
            if (hftransgst == "")
                hftransgst = 0;
            if (hftranigst == "")
                hftranigst = 0;
            if (hftranamount == "")
                hftranamount = 0;
            if (hftcs == "")
                hftcs = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (parseFloat(grd.rows(rowCount).cells(8).children[0].value) > parseFloat(grd.rows(rowCount).cells(9).children[0].value)) {
                        grd.rows(rowCount).cells(9).children[0].style.backgroundColor = 'Green'
                        grd.rows(rowCount).cells(9).children[0].style.color = 'White'
                    }
                    if (parseFloat(grd.rows(rowCount).cells(8).children[0].value) < parseFloat(grd.rows(rowCount).cells(9).children[0].value)) {
                        grd.rows(rowCount).cells(9).children[0].style.backgroundColor = 'Red'
                        grd.rows(rowCount).cells(9).children[0].style.color = 'White'
                    }
                    if (parseFloat(grd.rows(rowCount).cells(8).children[0].value) == parseFloat(grd.rows(rowCount).cells(9).children[0].value)) {
                        grd.rows(rowCount).cells(9).children[0].style.backgroundColor = 'White'
                        grd.rows(rowCount).cells(9).children[0].style.color = 'Black'
                    }
                    if (Number(grd.rows(rowCount).cells[10].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(9).children[0].value)) {
                            amt = (Number(grd.rows(rowCount).cells[7].innerHTML)) * (Number(grd.rows(rowCount).cells[9].children[0].value));
                            grd.rows(rowCount).cells[10].innerHTML = Math.round(amt * Math.pow(10, 2)) / Math.pow(10, 2);
                            total += Number(grd.rows(rowCount).cells[10].innerHTML);

                            cgstamt = amt * (Number(grd.rows(rowCount).cells[11].children[0].value) / 100);
                            grd.rows(rowCount).cells(12).children[0].value = Math.round(cgstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                            cgsttotal += Number(grd.rows(rowCount).cells(12).children[0].value);

                            sgstamt = amt * (Number(grd.rows(rowCount).cells[13].children[0].value) / 100);
                            grd.rows(rowCount).cells[14].innerHTML = Math.round(sgstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                            sgsttotal += Number(grd.rows(rowCount).cells[14].innerHTML);

                            igstamt = amt * (Number(grd.rows(rowCount).cells[15].children[0].value) / 100);
                            grd.rows(rowCount).cells(16).children[0].value = Math.round(igstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                            igsttotal += Number(grd.rows(rowCount).cells(16).children[0].value);

                        }

                    }
                    else if (Number(grd.rows(rowCount).cells[10].innerHTML) == 0.00) {
                        amt = (Number(grd.rows(rowCount).cells[7].innerHTML)) * (Number(grd.rows(rowCount).cells[9].children[0].value));
                        grd.rows(rowCount).cells[10].innerHTML = Math.round(amt * Math.pow(10, 2)) / Math.pow(10, 2);
                        total += Number(grd.rows(rowCount).cells[10].innerHTML);

                        cgstamt = amt * (Number(grd.rows(rowCount).cells[11].children[0].value) / 100);
                        grd.rows(rowCount).cells(12).children[0].value = Math.round(cgstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                        cgsttotal += Number(grd.rows(rowCount).cells(12).children[0].value);

                        sgstamt = amt * (Number(grd.rows(rowCount).cells[13].children[0].value) / 100);
                        grd.rows(rowCount).cells[14].innerHTML = Math.round(sgstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                        sgsttotal += Number(grd.rows(rowCount).cells[14].innerHTML);

                        igstamt = amt * (Number(grd.rows(rowCount).cells[15].children[0].value) / 100);
                        grd.rows(rowCount).cells(16).children[0].value = Math.round(igstamt * Math.pow(10, 2)) / Math.pow(10, 2);
                        igsttotal += Number(grd.rows(rowCount).cells(16).children[0].value);


                    }

                    else {
                        total += Number(grd.rows(rowCount).cells[10].innerHTML);
                        cgsttotal += parseFloat(Number(grd.rows(rowCount).cells[12].innerHTML) + hftrancgst);
                        sgsttotal += parseFloat(Number(grd.rows(rowCount).cells[14].innerHTML) + hftransgst);
                        igsttotal += parseFloat(Number(grd.rows(rowCount).cells[16].innerHTML) + hftranigst);

                    }



                }
                grd.rows[grd.rows.length - 1].cells[10].innerHTML = Math.round(total * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtbasic.ClientID %>").value = Math.round(total * Math.pow(10, 2)) / Math.pow(10, 2);

                grd.rows[grd.rows.length - 1].cells[12].innerHTML = Math.round(cgsttotal * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtcgst.ClientID %>").value = Math.round((parseFloat(cgsttotal) + parseFloat(hftrancgst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                grd.rows[grd.rows.length - 1].cells[14].innerHTML = Math.round(sgsttotal * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtsgst.ClientID %>").value = Math.round((parseFloat(sgsttotal) + parseFloat(hftransgst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                grd.rows[grd.rows.length - 1].cells[16].innerHTML = Math.round(igsttotal * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtigst.ClientID %>").value = Math.round((parseFloat(igsttotal) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                //document.getElementById("<%=txtgst.ClientID %>").value = Math.round((parseFloat(sgsttotal) + parseFloat(cgsttotal) + parseFloat(igsttotal)) * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtgst.ClientID %>").value = Math.round((parseFloat(sgsttotal) + parseFloat(cgsttotal) + parseFloat(igsttotal) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txttotal.ClientID %>").value = Math.round((parseFloat(total) + parseFloat(sgsttotal) + parseFloat(cgsttotal) + parseFloat(igsttotal) + parseFloat(hftranamount) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)+ parseFloat(hftcs)) * Math.pow(10, 2)) / Math.pow(10, 2);


                //document.getElementById("<%=txttotal.ClientID %>").value = Math.round((parseFloat(total) + parseFloat(sgsttotal) + parseFloat(cgsttotal) + parseFloat(igsttotal)) * Math.pow(10, 2)) / Math.pow(10, 2);
                ftamount = total;
                ftcgst = cgsttotal;
                ftsgst = sgsttotal;
                ftigst = igsttotal;
                document.getElementById("<%=hfamount.ClientID %>").value = total;
                document.getElementById("<%=hfcgstfooter.ClientID %>").value = cgsttotal;
                document.getElementById("<%=hfsgsttaxamt.ClientID %>").value = sgsttotal;
                document.getElementById("<%=hfigstfooter.ClientID %>").value = igsttotal;

                maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsgst) + parseFloat(ftcgst) + parseFloat(ftigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                transporttotal = Math.round((parseFloat(hftranamount) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                ////////debugger
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal) - parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                if ((hfothers == "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(transporttotal) - parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
                }
                calculateotherCMC_SA();
                calculatedeductionCMC_SA();
            }

        }

    </script>
    <script language="javascript" type="text/javascript">
        var maingridtotal = 0;
        var transporttotal = 0;
        function calculateotherCMC_SA() {
            ////debugger
            grd = document.getElementById("<%=gvanyother.ClientID %>");
            maingrd = document.getElementById("<%=gridcmc.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value;
            var ftcgst = document.getElementById("<%=hfcgstfooter.ClientID %>").value;
            var ftsgst = document.getElementById("<%=hfsgsttaxamt.ClientID %>").value;
            var ftigst = document.getElementById("<%=hfigstfooter.ClientID %>").value;
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value;
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value;
            var totalother = 0;
            var hftranamount = document.getElementById("<%=hftranamount.ClientID %>").value;
            var hftrancgst = document.getElementById("<%=hftrancgst.ClientID %>").value;
            var hftransgst = document.getElementById("<%=hftransgst.ClientID %>").value;
            var hftranigst = document.getElementById("<%=hftranigst.ClientID %>").value;
            var hftcs = document.getElementById("<%=txttcsamount.ClientID %>").value.replace(/,/g, "");
            if (hftrancgst == "")
                hftrancgst = 0;
            if (hftransgst == "")
                hftransgst = 0;
            if (hftranigst == "")
                hftranigst = 0;
            if (hftranamount == "")
                hftranamount = 0;
            if (hftcs == "")
                hftcs = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                    if (Number(grd.rows(rowCount).cells[3].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                            totalother += Number(grd.rows(rowCount).cells(3).children[0].value);
                        }
                    }
                    else {
                        totalother += Number(grd.rows(rowCount).cells(3).children[0].value);
                    }
                }
                grd.rows[grd.rows.length - 1].cells[3].innerHTML = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                hfothers = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                if (maingrd != null) {
                    //Calculate();
                    maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsgst) + parseFloat(ftcgst) + parseFloat(ftigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                    transporttotal = Math.round((parseFloat(hftranamount) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                }
                else if (maingrd == null) {
                    maingridtotal = 0;
                    transporttotal = 0;
                }
                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(hfothers) + parseFloat(maingridtotal) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);;
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
                }
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal); -parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
                }

            }
            else {
                ////debugger
                document.getElementById("<%=hfothers.ClientID %>").value = 0;
                if (isNaN(hfdeduction)) {
                    hfdeduction = 0;
                }
                hfnetamt = parseFloat(maingridtotal) + parseFloat(transporttotal) -parseFloat(hfdeduction)+ parseFloat(hftcs);
                document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
            }
        }


        function calculatedeductionCMC_SA() {
            //debugger
            grd = document.getElementById("<%=gvdeduction.ClientID %>");
            maingrd = document.getElementById("<%=gridcmc.ClientID %>");
            grdother = document.getElementById("<%=gvanyother.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value;
            var ftcgst = document.getElementById("<%=hfcgstfooter.ClientID %>").value;
            var ftsgst = document.getElementById("<%=hfsgsttaxamt.ClientID %>").value;
            var ftigst = document.getElementById("<%=hfigstfooter.ClientID %>").value;
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value;
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value;
            var hftranamount = document.getElementById("<%=hftranamount.ClientID %>").value;
            var hftrancgst = document.getElementById("<%=hftrancgst.ClientID %>").value;
            var hftransgst = document.getElementById("<%=hftransgst.ClientID %>").value;
            var hftranigst = document.getElementById("<%=hftranigst.ClientID %>").value;
            var hftcs = document.getElementById("<%=txttcsamount.ClientID %>").value.replace(/,/g, "");
            if (hftrancgst == "")
                hftrancgst = 0;
            if (hftransgst == "")
                hftransgst = 0;
            if (hftranigst == "")
                hftranigst = 0;
            if (hftranamount == "")
                hftranamount = 0;
             if (hftcs == "")
                hftcs = 0;
            var totalother = 0;
            var totalded = 0;
            var sumtotalnet = 0;
            var otheramt = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                    if (Number(grd.rows(rowCount).cells[3].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                            totalded += Number(grd.rows(rowCount).cells(3).children[0].value);
                        }
                    }
                    else {
                        totalded += Number(grd.rows(rowCount).cells(3).children[0].value);
                    }

                }
                grd.rows[grd.rows.length - 1].cells[3].innerHTML = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=hfdeduction.ClientID %>").value = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                hfdeduction = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                if (maingrd != null) {
                    //Calculate();
                    maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsgst) + parseFloat(ftcgst) + parseFloat(ftigst)) * Math.pow(10, 2)) / Math.pow(10, 2);
                    transporttotal = Math.round((parseFloat(hftranamount) + parseFloat(hftrancgst) + parseFloat(hftransgst) + parseFloat(hftranigst)) * Math.pow(10, 2)) / Math.pow(10, 2);

                }
                else if (maingrd == null) {
                    maingridtotal = 0;
                    transporttotal = 0;
                }
                if (grdother != null) {
                    //calculateother();
                    otheramt = hfothers;
                    document.getElementById("<%=hfothers.ClientID %>").value = otheramt;
                }
                else if (grdother == null) {
                    otheramt = 0;
                }
                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(hfothers) + parseFloat(maingridtotal) + parseFloat(transporttotal)+ parseFloat(hftcs);
                    document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);;
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
                }
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal) - parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
                }
                if ((hfothers == "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(otheramt) + parseFloat(transporttotal)-parseFloat(hfdeduction)+ parseFloat(hftcs);
                    document.getElementById("<%=hfdeduction.ClientID %>").value = hfdeduction;
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
                }
            }
            else {
                ////debugger
                document.getElementById("<%=hfdeduction.ClientID %>").value = 0;
                if (isNaN(hfothers)) {
                    hfothers = 0;
                }
                hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) + parseFloat(transporttotal)+ parseFloat(hftcs);
                document.getElementById("<%=txtnetAmount.ClientID %>").value = Math.round(hfnetamt * Math.pow(10, 2)) / Math.pow(10, 2);;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function validate1() {

            document.getElementById("<%=btnreject.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">

        function checkroles() {
            //debugger;
            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID %>").value;
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {

                var hprice = GridView1.rows(rowCount).cells(18).children[0].value;
                var newprice = GridView1.rows(rowCount).cells(9).children[0].value;
                var hbasicprice = GridView1.rows(rowCount).cells(17).children[0].value;
                var newbasicprice = GridView1.rows(rowCount).cells(8).children[0].value;
                var itemtype = GridView1.rows(1).cells(1).innerHTML.substring(0, 1);
                //                if (role == "PurchaseManager") {
                if (parseFloat(newbasicprice) > parseFloat(hbasicprice)) {
                    window.alert("You are not able to increase standard price");
                    GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(17).children[0].value;
                    return false;
                }
                //                }

                if (parseFloat(newprice) > parseFloat(hprice)) {
                    window.alert("You are not able to increase new purchase price");
                    GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(18).children[0].value;
                    return false;
                }
                else if (parseFloat(newbasicprice) > parseFloat(hbasicprice)) {
                    window.alert("You are not able to increase standard price");
                    GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(17).children[0].value;
                    return false;
                }
                else if ((itemtype == "1") && ((parseFloat(newprice)) > (parseFloat(hprice)))) {
                    window.alert("You are not able to increase the new purchase price for assets");
                    GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(18).children[0].value;
                    return false;
                }
                else if ((itemtype == "1") && ((parseFloat(newbasicprice)) != (parseFloat(hbasicprice)))) {
                    window.alert("You are not able to increase or decrease the standard price for assets");
                    GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(17).children[0].value;
                    return false;
                }
            }
        }
        function checkrolescentralgrid() {
            //debugger;
            var GridView1 = document.getElementById("<%=grdcentral.ClientID %>");
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                var hprice = GridView1.rows(rowCount).cells(17).children[0].value;
                var newprice = GridView1.rows(rowCount).cells(9).children[0].value;
                if (parseFloat(newprice) > parseFloat(hprice)) {
                    window.alert("You are not able to increase new purchase price");
                    GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(17).children[0].value;
                    return false;
                }
            }
        }
    </script>
    <script language="javascript">
        function SelectAll() {
            //debugger
            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");
            var role = document.getElementById("<%=hfrole.ClientID %>").value;
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                var itemcode = GridView1.rows(rowCount).cells(1).innerText;
                var oldprice = GridView1.rows(rowCount).cells(8).children[0].value;
                var newprice = GridView1.rows(rowCount).cells(9).children[0].value;
                var hprice = GridView1.rows(rowCount).cells(18).children[0].value;
                var hbasicprice = GridView1.rows(rowCount).cells(17).children[0].value;
                var itemtype = GridView1.rows(1).cells(1).innerHTML.substring(0, 1);
                if (GridView1.rows(rowCount).cells(0).children(0).checked == true && GridView1.rows(rowCount).cells(8).children[0].disabled == false) {
                    if (GridView1.rows(rowCount).cells(8).children[0].style.color == "black") {
                        if ((parseFloat(GridView1.rows(rowCount).cells(8).children[0].value) > parseFloat(GridView1.rows(rowCount).cells(9).children[0].value)) && (role == "PurchaseManager") && (GridView1.rows(rowCount).cells(9).children[0].value != 0) && (itemtype != "1")) {

                            var response = confirm("Do you want to Change the basic price of " + itemcode + "?");
                            if (response) {
                                GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                                GridView1.rows(rowCount).cells(8).children[0].focus();
                                GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                                CalculateCMC_SA();
                                GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(9).children[0].value;
                                return false;
                            }
                            else {
                                GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                                GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                            }
                            CalculateCMC_SA();
                            return false;
                        }

                        else if ((role == "PurchaseManager" || role == "SuperAdmin") && (GridView1.rows(rowCount).cells(9).children[0].value == 0) && (GridView1.rows(rowCount).cells(8).children[0].value != 0)) {
                            window.alert("New Purchase Price cannot be Empty");
                            GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(18).children[0].value;
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                            //CMCCalculate();
                            CalculateCMC_SA();
                            return false;


                        }
                        else if ((role == "PurchaseManager" || role == "SuperAdmin") && (GridView1.rows(rowCount).cells(8).children[0].value == 0) && (GridView1.rows(rowCount).cells(9).children[0].value != 0)) {
                            window.alert("Standard Price cannot be Empty");
                            GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(17).children[0].value;
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                            //CMCCalculate();
                            CalculateCMC_SA();
                            return false;

                        }
                        else if ((role == "PurchaseManager" || role == "SuperAdmin") && (GridView1.rows(rowCount).cells(8).children[0].value == 0) && (GridView1.rows(rowCount).cells(9).children[0].value == 0)) {
                            window.alert("New Purchase Price and Standard Price cannot be Empty");
                            GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(17).children[0].value;
                            GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(18).children[0].value;
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                            //CMCCalculate();      
                            CalculateCMC_SA();
                            return false;

                        }

                        else if (parseFloat(GridView1.rows(rowCount).cells(8).children[0].value) != parseFloat(GridView1.rows(rowCount).cells(9).children[0].value) && (role == "SuperAdmin")) {
                            if (GridView1.rows(rowCount).cells(9).children[0].value == "") {
                                window.alert("New Purchase Price cannot be Empty");
                                GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(18).children[0].value;
                            }
                            else {
                                var response = confirm("Do you want to Change the basic price of " + itemcode + "?");

                                if (response) {
                                    GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                                    GridView1.rows(rowCount).cells(8).children[0].focus();
                                    GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                    GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                                    GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(9).children[0].value;
                                    //CMCCalculate();
                                    CalculateCMC_SA();
                                    return false;
                                }
                                else {

                                    GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                                    GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                                    GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                                    //CMCCalculate();
                                    CalculateCMC_SA();
                                }
                            }

                            return false;
                        }
                        else {

                            GridView1.rows(rowCount).cells(8).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(9).children[0].disabled = true;
                            GridView1.rows(rowCount).cells(8).children[0].style.color = '#257972';
                            // GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(13).children[0].value;
                            //CMCCalculate();
                            CalculateCMC_SA();

                        }


                    }
                }
                else if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                    document.getElementById("<%=btnmdlupd.ClientID %>").disabled = false;
                    GridView1.rows(rowCount).cells(8).children[0].disabled = false;
                    GridView1.rows(rowCount).cells(8).children[0].style.color = 'black';
                    GridView1.rows(rowCount).cells(9).children[0].disabled = false;
                    GridView1.rows(rowCount).cells(8).children[0].value = GridView1.rows(rowCount).cells(17).children[0].value;
                    GridView1.rows(rowCount).cells(9).children[0].value = GridView1.rows(rowCount).cells(18).children[0].value;
                    //var valid = 1;                       
                    //CMCCalculate(valid);
                    CalculateCMC_SA();

                }

            }
        }


    </script>
</asp:Content>
