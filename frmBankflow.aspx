<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmBankflow.aspx.cs"
    EnableEventValidation="false" Inherits="frmBankflow" Title="Bank Voucher- Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function checkNumeric(event) {
            var kCode = event.keyCode || event.charCode; // for cross browser check

            //FF and Safari use e.charCode, while IE use e.keyCode that returns the ASCII value 
            if ((kCode > 57 || kCode < 48) && (kCode != 46 && kCode != 45)) {
                //code for IE
                if (window.ActiveXObject) {
                    event.keyCode = 0
                    return false;
                }
                else {
                    event.charCode = 0
                }
            }
        }
    </script>
    <script language="javascript">
        function pay() {
            var payment = document.getElementById("<%=ddlpayment.ClientID %>");
            if (payment.value != "Cash" && payment.selectedIndex != 0) {
                document.getElementById("<%=lblmode.ClientID %>").innerHTML = payment.value + " No:";
                document.getElementById("<%=txtcheque.ClientID %>").style.display = "block";
            }
            else {
                document.getElementById("<%=lblmode.ClientID %>").innerHTML = "";
                document.getElementById("<%=txtcheque.ClientID %>").style.display = "none";
            }
        }
        function checkadvance() {
            var hfadvance = document.getElementById("<%=hfadvance.ClientID %>").value;
            if (hfadvance != "Advance") {
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
        }

        function validate() {
            var hfadvance = "";
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                hfadvance = document.getElementById("<%=hfadvance.ClientID %>").value;
            }

            if (hfadvance != "Advance") {
                var objs = new Array("<%=ddltypeofpay.ClientID %>", "<%=ddlsubtype.ClientID %>", "<%=ddlclientid.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlservice.ClientID %>", "<%=ddlvendor.ClientID %>", "<%=ddlpono.ClientID %>"
                        , "<%=txtpo.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtra.ClientID %>", "<%=txtbasic.ClientID %>", "<%=txttax.ClientID %>"
                        , "<%=txtex.ClientID %>", "<%=txtfre.ClientID %>", "<%=txtins.ClientID %>", "<%=txted.ClientID %>", "<%=txthed.ClientID %>", "<%=txttotal.ClientID %>"
                        , "<%=txtretention.ClientID %>", "<%=txttds.ClientID %>", "<%=txtedc.ClientID %>", "<%=txtadvance.ClientID %>", "<%=txthold.ClientID %>", "<%=txtother.ClientID %>"
                        , "<%=txtnettax.ClientID %>", "<%=txtNetED.ClientID %>", "<%=txtNetHED.ClientID %>", "<%=txtfdr.ClientID %>", "<%=txtprinciple.ClientID %>", "<%=txtintrest.ClientID %>"
                        , "<%=txtname.ClientID %>", "<%=ddlfrom.ClientID %>", "<%=ddltobank.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=ddlcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                var netamount = document.getElementById("<%= txtamt.ClientID%>").value;
                if (netamount < 0) {
                    alert("You are excessing the Invoice Amount");
                    return false;
                }

                var GridView = document.getElementById("<%=grd.ClientID %>");
                if (GridView != null) {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {

                        if (GridView.rows(rowCount).cells(10).children(0).checked == true) {
                            var str1 = GridView.rows(rowCount).cells(11).children[0].value;

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
                                alert("You are not able to make payment to before invoice date");
                                document.getElementById("<%=txtdate.ClientID %>").focus();
                                return false;
                            }
                            if (GridView.rows(rowCount).cells(12).children[0].value == "1" || GridView.rows(rowCount).cells(12).children[0].value == "2" || GridView.rows(rowCount).cells(12).children[0].value == "2A") {
                                alert("Invoice is not approved:" + GridView.rows(rowCount).cells(0).innerHTML);
                                return false;
                            }
                        }
                    }
                }
            }
            else {
                var objs = new Array("<%=ddltypeofpay.ClientID %>", "<%=ddlclientid.ClientID %>", "<%=ddlsubclientid.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlpono.ClientID %>"
                        , "<%=txtpo.ClientID %>", "<%=txted.ClientID %>", "<%=txthed.ClientID %>", "<%=txtNetED.ClientID %>", "<%=txtNetHED.ClientID %>", "<%=txtex.ClientID %>", "<%=txtnetex.ClientID %>"
                        , "<%=txtnetadvsevtax.ClientID %>", "<%=txtAdvStax.ClientID %>", "<%=txtnetadvsaltax.ClientID %>", "<%=txtAdvsaltax.ClientID %>", "<%=txtadvbasic.ClientID %>", "<%=TxtAdvtds.ClientID %>", "<%=TxtAdvwct.ClientID %>", "<%=TxtAdvother.ClientID %>", "<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                var AdvStax = document.getElementById("<%=txtAdvStax.ClientID %>").value;
                var Advsaltax = document.getElementById("<%=txtAdvsaltax.ClientID %>").value;
                var AdvExtax = document.getElementById("<%=txtex.ClientID %>").value;




                if (AdvStax != "" && AdvStax != "0") {
                    var service = document.getElementById("<%=ddlservicetax.ClientID %>").value;
                    if (service == "Select") {
                        alert("ServiceTax No Required");
                        document.getElementById("<%=ddlservicetax.ClientID %>").focus();
                        return false;
                    }
                }
                if (Advsaltax != "" && Advsaltax != "0") {
                    var vat = document.getElementById("<%=ddlvatno.ClientID %>").value;
                    if (vat == "Select") {
                        alert("VAT No Required");
                        document.getElementById("<%=ddlvatno.ClientID %>").focus();
                        return false;
                    }
                }
                if (AdvExtax != "" && AdvExtax != "0") {
                    var excise = document.getElementById("<%=ddlExcno.ClientID %>").value;
                    if (excise == "Select") {
                        alert("Excise No Required");
                        document.getElementById("<%=ddlExcno.ClientID %>").focus();
                        return false;
                    }
                }
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
       
    </script>
    <script language="javascript">
        function Total() {

            var hfadvance = document.getElementById("<%=hfadvance.ClientID %>").value;

            if (hfadvance != "Advance") {
                var originalValue = 0;
                var originalValue1 = 0;
                var originalValue2 = 0;
                var basic = document.getElementById("<%=txtbasic.ClientID %>").value;
                var tax = document.getElementById("<%=txttax.ClientID %>").value;
                var hftype = document.getElementById("<%=hf2.ClientID %>").value;

                var ed = document.getElementById("<%=txted.ClientID %>").value;
                var hed = document.getElementById("<%=txthed.ClientID %>").value;
                var tds = document.getElementById("<%=txttds.ClientID %>").value;
                var ret = document.getElementById("<%=txtretention.ClientID %>").value;
                var WCT = document.getElementById("<%=txtedc.ClientID %>").value;
                var adv = document.getElementById("<%=txtadvance.ClientID %>").value;
                var hold = document.getElementById("<%=txthold.ClientID %>").value;
                var other = document.getElementById("<%=txtother.ClientID %>").value;

                if (document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Trading Supply" || document.getElementById("<%=ddltypeofpay.ClientID %>").value == "Manufacturing") {
                    var ex = document.getElementById("<%=txtex.ClientID %>").value;
                    var fre = document.getElementById("<%=txtfre.ClientID %>").value;
                    var ins = document.getElementById("<%=txtins.ClientID %>").value;
                    var nex = document.getElementById("<%=txtnetex.ClientID %>").value;
                    var nfre = document.getElementById("<%=txtnetfre.ClientID %>").value;
                    var nins = document.getElementById("<%=txtnetins.ClientID %>").value;
                    var ntax = document.getElementById("<%=txtnettax.ClientID %>").value;
                    var ned = document.getElementById("<%=txtNetED.ClientID %>").value;
                    var nhed = document.getElementById("<%=txtNetHED.ClientID %>").value;
                    if (basic == "") {
                        basic = 0;
                    }
                    if (tax == "") {
                        tax = 0;
                    }
                    if (ex == "") {
                        ex = 0;
                    }
                    if (fre == "") {
                        fre = 0;
                    }
                    if (ins == "") {
                        ins = 0;
                    }
                    if (ed == "") {
                        ed = 0;
                    }
                    if (hed == "") {
                        hed = 0;
                    }
                    if (tds == "") {
                        tds = 0;
                    }
                    if (ret == "") {
                        ret = 0;
                    }
                    if (WCT == "") {
                        WCT = 0;
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
                    if (nex == "") {
                        nex = 0;
                    }
                    if (nfre == "") {
                        nfre = 0;
                    }
                    if (nins == "") {
                        nins = 0;
                    }
                    var taxbal = 0;
                    var edbal = 0;
                    var hedbal = 0;
                    var exbal = 0;
                    var frebal = 0;
                    var insbal = 0;
                    if ((tax != ntax) && ntax != "") {
                        taxbal = ntax - tax;
                    }
                    if ((ed != ned) && ned != "") {
                        edbal = ned - ed;
                    }
                    if ((hed != nhed) && nhed != "") {
                        hedbal = nhed - hed;
                    }
                    if ((ex != nex) && nex != "") {
                        exbal = nex - ex;
                    }
                    if ((fre != nfre) && nfre != "") {
                        frebal = nfre - fre;
                    }
                    if ((ins != nins) && nins != "") {
                        insbal = nins - ins;
                    }
                    originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(ex) + parseFloat(fre) + parseFloat(ins)));
                    originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed) + parseFloat(ex) + parseFloat(fre) + parseFloat(ins)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(WCT) + parseFloat(adv) + parseFloat(hold) + parseFloat(other)) + (parseFloat(taxbal) + parseFloat(edbal) + parseFloat(hedbal) + parseFloat(exbal) + parseFloat(frebal) + parseFloat(insbal))));

                    var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                    var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                    document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
                    document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;

                }
                else if (hftype == "VAT/Material Supply") {
                    var ex = document.getElementById("<%=txtex.ClientID %>").value;
                    var fre = document.getElementById("<%=txtfre.ClientID %>").value;
                    var ins = document.getElementById("<%=txtins.ClientID %>").value;
                    var nex = document.getElementById("<%=txtnettax.ClientID %>").value;
                    var nfre = document.getElementById("<%=txtnetfre.ClientID %>").value;
                    var nins = document.getElementById("<%=txtnetins.ClientID %>").value;
                    if (basic == "") {
                        basic = 0;
                    }
                    if (tax == "") {
                        tax = 0;
                    }
                    if (ex == "") {
                        ex = 0;
                    }
                    if (fre == "") {
                        fre = 0;
                    }
                    if (ins == "") {
                        ins = 0;
                    }
                    if (ed == "") {
                        ed = 0;
                    }
                    if (hed == "") {
                        hed = 0;
                    }
                    if (tds == "") {
                        tds = 0;
                    }
                    if (ret == "") {
                        ret = 0;
                    }
                    if (WCT == "") {
                        WCT = 0;
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
                    if (nex == "") {
                        nex = 0;
                    }
                    if (nfre == "") {
                        nfre = 0;
                    }
                    if (nins == "") {
                        nins = 0;
                    }
                    var taxbal = 0;
                    var frebal = 0;
                    var insbal = 0;
                    if ((tax != nex) && nex != "") {
                        taxbal = nex - tax;
                    }

                    if ((fre != nfre) && nfre != "") {
                        frebal = nfre - fre;
                    }
                    if ((ins != nins) && nins != "") {
                        insbal = nins - ins;
                    }
                    originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(fre) + parseFloat(ins)));
                    originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(fre) + parseFloat(ins)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(WCT) + parseFloat(adv) + parseFloat(hold) + parseFloat(other)) + (parseFloat(taxbal) + parseFloat(frebal) + parseFloat(insbal))));

                    var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                    var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                    document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
                    document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;
                }
                else {
                    var ntax = document.getElementById("<%=txtnetex.ClientID %>").value;
                    var ned = document.getElementById("<%=txtNetED.ClientID %>").value;
                    var nhed = document.getElementById("<%=txtNetHED.ClientID %>").value;
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
                    if (tds == "") {
                        tds = 0;
                    }
                    if (ret == "") {
                        ret = 0;
                    }
                    if (WCT == "") {
                        WCT = 0;
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
                    var taxbal = 0;
                    var edbal = 0;
                    var hedbal = 0;
                    if ((tax != ntax) && ntax != "") {
                        taxbal = ntax - tax;
                    }
                    if ((ed != ned) && ned != "") {
                        edbal = ned - ed;
                    }
                    if ((hed != nhed) && nhed != "") {
                        hedbal = nhed - hed;
                    }
                    originalValue = eval((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed)));
                    originalValue1 = eval(((parseFloat(basic) + parseFloat(tax) + parseFloat(ed) + parseFloat(hed)) - (parseFloat(tds) + parseFloat(ret) + parseFloat(WCT) + parseFloat(adv) + parseFloat(hold) + parseFloat(other)) + (parseFloat(taxbal) + parseFloat(edbal) + parseFloat(hedbal))));

                    var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                    var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                    document.getElementById('<%= txttotal.ClientID%>').value = roundValue;
                    document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;
                }

            }
        }
        function TotalAdv() {
            var originalValue = 0;
            var originalValue1 = 0;

            var Advbasic = document.getElementById("<%=txtadvbasic.ClientID %>").value;
            var AdvStax = document.getElementById("<%=txtAdvStax.ClientID %>").value;
            var Advex = document.getElementById("<%=txtex.ClientID %>").value;
            var Adved = document.getElementById("<%=txted.ClientID %>").value;
            var Advhed = document.getElementById("<%=txthed.ClientID %>").value;
            var Advsaltax = document.getElementById("<%=txtAdvsaltax.ClientID %>").value;
            var Advtds = document.getElementById("<%=TxtAdvtds.ClientID %>").value;
            var Advwct = document.getElementById("<%=TxtAdvwct.ClientID %>").value;
            var advhold = document.getElementById("<%=Txtadvhold.ClientID %>").value;
            var Advother = document.getElementById("<%=TxtAdvother.ClientID %>").value;
            var netex = document.getElementById("<%=txtnetex.ClientID %>").value;
            var NetED = document.getElementById("<%=txtNetED.ClientID %>").value;
            var NetHED = document.getElementById("<%=txtNetHED.ClientID %>").value;
            var netadvsaltax = document.getElementById("<%=txtnetadvsaltax.ClientID %>").value;
            var netadvsevtax = document.getElementById("<%=txtnetadvsevtax.ClientID %>").value;


            if (Advbasic == "") {
                Advbasic = 0;
            }
            if (AdvStax == "") {
                AdvStax = 0;
            }
            if (Advex == "") {
                Advex = 0;
            }
            if (Adved == "") {
                Adved = 0;
            }
            if (Advhed == "") {
                Advhed = 0;
            }
            if (Advsaltax == "") {
                Advsaltax = 0;
            }
            if (Advtds == "") {
                Advtds = 0;
            }
            if (Advwct == "") {
                Advwct = 0;
            }
            if (advhold == "") {
                advhold = 0;
            }
            if (Advother == "") {
                Advother = 0;
            }

            if (netadvsaltax == "") {
                netadvsaltax = 0;
            }
            if (NetED == "") {
                NetED = 0;
            }
            if (NetHED == "") {
                NetHED = 0;
            }
            if (netadvsevtax == "") {
                netadvsevtax = 0;
            }
            if (netex == "") {
                netex = 0;
            }

            var Staxbal = 0;
            var edbal = 0;
            var hedbal = 0;
            var salbal = 0;
            var exbal = 0;

            if ((AdvStax != netadvsevtax) && netadvsevtax != "") {
                Staxbal = netadvsevtax - AdvStax;
            }
            if ((Adved != NetED) && NetED != "") {
                edbal = NetED - Adved;
            }
            if ((Advhed != NetHED) && NetHED != "") {
                hedbal = NetHED - Advhed;
            }
            if ((Advex != netex) && netex != "") {
                exbal = netex - Advex;
            }
            if ((Advsaltax != netadvsaltax) && netadvsaltax != "") {
                salbal = netadvsaltax - Advsaltax;
            }
            originalValue = eval(parseFloat(Advbasic) + parseFloat(AdvStax) + parseFloat(Advex) + parseFloat(Adved) + parseFloat(Advhed) + parseFloat(Advsaltax));
            originalValue1 = eval(((parseFloat(Advbasic) + parseFloat(AdvStax) + parseFloat(Advex) + parseFloat(Adved) + parseFloat(Advhed) + parseFloat(Advsaltax)) - (parseFloat(Advtds) + parseFloat(Advwct) + parseFloat(advhold) + parseFloat(Advother))) + (parseFloat(Staxbal) + parseFloat(edbal) + parseFloat(hedbal) + +parseFloat(exbal) + +parseFloat(salbal)));

            var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
            var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);

            document.getElementById('<%= TxtAdvtotal.ClientID%>').value = roundValue;
            document.getElementById('<%= txtamt.ClientID%>').value = roundValue1;

        }
    
    </script>
    <style type="text/css">
        .style9
        {
            width: 70px;
        }
    </style>
    <script type="text/javascript">
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
                return false;
            }
        }
        function checkDatepayment(sender, args) {
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
                        <table class="estbl" width="660px">
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
                                                    AutoPostBack="true" RepeatDirection="Horizontal" runat="server" onclick="check();"
                                                    CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                                    <asp:ListItem Enabled="false">Credit</asp:ListItem>
                                                    <asp:ListItem>Debit</asp:ListItem>
                                                    <asp:ListItem>Transfer</asp:ListItem>
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
                                            <td>
                                                Category of Payment:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddltypeofpay" runat="server" ToolTip="Type of Payment" AutoPostBack="true"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddltypeofpay_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsubtype" runat="server" ToolTip="Type" AutoPostBack="true"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddlsubtype_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlservice" runat="server" ToolTip="ServiceType" CssClass="esddown"
                                                    OnSelectedIndexChanged="ddlservice_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlvendor" CssClass="esddown" ToolTip="Vendor" runat="server"
                                                    OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trclient" runat="server">
                                            <td width="500px" colspan="4">
                                                <table width="100%" class="innertab" runat="server" id="Table2">
                                                    <tr>
                                                        <td>
                                                            Client ID:
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlclientid" onchange="SetDynamicKey('dp3',this.value);" runat="server"
                                                                ToolTip="ClientID" Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <span class="starSpan">*</span>
                                                            <%-- <cc1:CascadingDropDown ID="CascadingDropDown7" runat="server" TargetControlID="ddlclientid"
                                                                ServicePath="cascadingDCA.asmx" Category="client" LoadingText="Please Wait" ServiceMethod="clientid"
                                                                PromptText="Select Client Id">
                                                            </cc1:CascadingDropDown>--%>
                                                            <br />
                                                            <asp:Label ID="lblcccode" class="ajaxspan" runat="server"></asp:Label>
                                                            <%--      <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp3" runat="server"
                                                                TargetControlID="lblcccode" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                ServiceMethod="GetClientName">
                                                            </cc1:DynamicPopulateExtender>--%>
                                                        </td>
                                                        <td>
                                                            Subclient ID:
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlsubclientid" onchange="SetDynamicKey('dp9',this.value);"
                                                                runat="server" ToolTip="ClientID" Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <%--    <cc1:CascadingDropDown ID="CascadingDropDown8" runat="server" TargetControlID="ddlsubclientid"
                                                                ServicePath="cascadingDCA.asmx" ParentControlID="ddlclientid" Category="subclient"
                                                                LoadingText="Please Wait" ServiceMethod="subclientid" PromptText="Select SubClient">
                                                            </cc1:CascadingDropDown>--%>
                                                            <asp:Label ID="Label9" class="ajaxspan" runat="server"></asp:Label>
                                                            <%-- <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp9" runat="server"
                                                                TargetControlID="Label9" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                ServiceMethod="GetSubClientName">
                                                            </cc1:DynamicPopulateExtender>--%>
                                                            <span class="starSpan">*</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="innertab" width="100%" runat="server" id="cc">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="CC Code:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="130px"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <%-- <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                    ServicePath="cascadingDCA.asmx" ParentControlID="ddlsubclientid" Category="dd1"
                                                    LoadingText="Please Wait" ServiceMethod="CreditCC">
                                                </cc1:CascadingDropDown>--%>
                                                <br />
                                                <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                            </td>
                                            <td width="350px">
                                                <table width="100%" class="innertab" runat="server" id="Dca">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl5" runat="server" CssClass="eslbl" Text="DCA Code:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbldca" class="ajaxspan" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsdca" CssClass="eslbl" runat="server" Text="SDCA Code:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsubdca" class="ajaxspan" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" class="eslbl" runat="server" Text="Due Date:"></asp:Label>
                                                <asp:Label ID="lblduedate" class="ajaxspan" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Trtaxnums" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td align="center" style="width: 130px;" id="Td1" runat="server">
                                                <asp:Label ID="lblservtaxnum" runat="server" CssClass="eslbl" Text="ServiceTax No"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 200px;">
                                                <asp:DropDownList ID="ddlservicetax" runat="server" ToolTip="ServiceTax No" Width="150px"
                                                    CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 130px;" id="lblex" runat="server">
                                                <asp:Label ID="lblExctaxnum" runat="server" CssClass="eslbl" Text="Excise No:"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 200px;">
                                                <asp:DropDownList ID="ddlExcno" runat="server" ToolTip="Excise No" Width="150px"
                                                    onchange="vatvalidate();" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 130px;" id="lblvat" runat="server">
                                                <asp:Label ID="lblvattaxnum" runat="server" CssClass="eslbl" Text="VAT NO:"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 200px;">
                                                <asp:DropDownList ID="ddlvatno" runat="server" Width="150px" ToolTip="Vat No" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="estbl" runat="server" id="Invoice" width="100%">
                                        <tr>
                                            <td>
                                                PO No:
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hfadvance" runat="server" />
                                                <asp:TextBox ID="txtpo" CssClass="estbox" runat="server" ToolTip="Po No" Width="100px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                                <asp:DropDownList ID="ddlpono" runat="server" ToolTip="Po No" Width="105px" CssClass="esddown"
                                                    OnSelectedIndexChanged="ddlpono_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style9" id="tdinv1" runat="server" colspan="1">
                                                Invoice No:
                                            </td>
                                            <td style="width: 125px" id="tdinv" runat="server">
                                                <asp:HiddenField ID="hf2" runat="server" />
                                                <asp:DropDownList ID="ddlinno" runat="server" ToolTip="Invoice No" Width="100px"
                                                    CssClass="esddown" AutoPostBack="True" OnSelectedIndexChanged="ddlinno_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtinvoice" Width="100px" CssClass="estbox" runat="server"></asp:TextBox>
                                            </td>
                                            <td id="tdinv2" runat="server">
                                                Invoice Date:
                                            </td>
                                            <td id="tdinvdate" runat="server">
                                                <asp:TextBox ID="txtindt" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="Invoice Date"
                                                    Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindt"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                    Animated="true" PopupButtonID="txtindt">
                                                </cc1:CalendarExtender>
                                                <%--   <img onclick="scwShow(document.getElementById('<%=txtindt.ClientID %>'),this);" alt=""
                                                    src="images/cal.gif" style="width: 15px; height: 15px;" onmouseout="checkDate(this)"
                                                    id="Img1" />--%>
                                            </td>
                                        </tr>
                                        <tr id="trbasic" runat="server">
                                            <td>
                                                <asp:Label ID="lblrano" CssClass="eslbl" runat="server" Text="RA No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtra" CssClass="estbox" runat="server" ToolTip="RA No:" Width="100px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                Basic Value:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtbasic" CssClass="estbox" runat="server" ToolTip="Basic Value"
                                                    Width="100px" onkeyup="Total();"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltax" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttax" CssClass="estbox" runat="server" ToolTip="Tax" Width="100px"
                                                    onkeyup="Total();"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="Advance" runat="server">
                                            <td colspan="2" align="center">
                                                <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="Basic Advance Amount"></asp:Label>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="txtadvbasic" CssClass="estbox" runat="server" ToolTip="Basic Value"
                                                    Width="100px" onkeyup="TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="Service Tax"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtAdvStax" CssClass="estbox" runat="server" ToolTip="Tax" Width="100px"
                                                    onkeyup="TotalAdv();"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="ex" runat="server">
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Excise duty"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtex" CssClass="estbox" runat="server" ToolTip="Excise duty" Width="100px"
                                                    onkeyup="Total();TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text=" EDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txted" CssClass="estbox" runat="server" ToolTip="EDCess" Width="100px"
                                                    onkeyup="Total();TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="HEDCess:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txthed" CssClass="estbox" runat="server" ToolTip="HEDCess" Width="100px"
                                                    onkeyup="Total();TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="trfreigt">
                                            <td class="style9">
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Freight"></asp:Label>
                                            </td>
                                            <td colspan="0" rowspan="1" width="75px">
                                                <asp:TextBox ID="txtfre" CssClass="estbox" runat="server" ToolTip="Freight" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="Insurance"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtins" CssClass="estbox" runat="server" ToolTip="Insurance" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                Total:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttotal" CssClass="estbox" runat="server" ToolTip="Total" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="advtotal" runat="server">
                                            <td colspan="2" align="center">
                                                <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Sales Tax/VAT"></asp:Label>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="txtAdvsaltax" CssClass="estbox" runat="server" ToolTip="Tax" Width="100px"
                                                    onkeyup="TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td colspan="2" align="center">
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="Total Advance Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtAdvtotal" CssClass="estbox" runat="server" ToolTip="Total" Width="100px"
                                                    onkeyup="TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th colspan="6" align="center">
                                                Deductions
                                            </th>
                                        </tr>
                                        <tr id="trtds" runat="server">
                                            <td class="style9">
                                                <asp:Label ID="lbltds" runat="server" CssClass="eslbl" Text="TDS:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttds" CssClass="estbox" runat="server" ToolTip="TDS" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblretention" runat="server" CssClass="eslbl" Text="Retention:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtretention" CssClass="estbox" runat="server" ToolTip="Retention"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbledc" runat="server" CssClass="eslbl" Text="WCT:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtedc" CssClass="estbox" runat="server" ToolTip="WCT" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="tradv" runat="server">
                                            <td>
                                                Advance:
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hftotalcredit" runat="server" />
                                                <asp:HiddenField ID="hftotaldeduct" runat="server" />
                                                <asp:TextBox ID="txtadvance" CssClass="estbox" runat="server" ToolTip="Advance" Width="100px"
                                                    onkeyup="Total();" onblur="checkadvance();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                Hold:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txthold" CssClass="estbox" runat="server" ToolTip="Hold" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                Any Other:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtother" CssClass="estbox" runat="server" ToolTip="Any Other" Width="100px"
                                                    onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="tradvtds" runat="server">
                                            <td class="style9">
                                                <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="TDS:"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="TxtAdvtds" CssClass="estbox" runat="server" ToolTip="TDS" Width="100px"
                                                    onkeyup="TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="WCT:"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="TxtAdvwct" CssClass="estbox" runat="server" ToolTip="WCT" Width="100px"
                                                    onkeyup="TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="tradvhold" runat="server">
                                            <td class="style9">
                                                <asp:Label ID="Label18" runat="server" CssClass="eslbl" Text="Hold:"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="Txtadvhold" CssClass="estbox" runat="server" ToolTip="Hold" Width="100px"
                                                    onkeyup="TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" CssClass="eslbl" Text="Any Other:"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="TxtAdvother" CssClass="estbox" runat="server" ToolTip="Any Other"
                                                    Width="100px" onkeyup="TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="NET" runat="server">
                                            <th colspan="6" align="center">
                                                Net Receipt Against Taxes
                                            </th>
                                        </tr>
                                        <tr id="netex" runat="server">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Excise duty"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetex" CssClass="estbox" runat="server" ToolTip="Net Excise duty"
                                                    Width="100px" onkeyup="Total();TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                EDCess:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNetED" CssClass="estbox" runat="server" ToolTip="Net Receipt EDCess"
                                                    Width="100px" onkeyup="Total();TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                HEDCess:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNetHED" CssClass="estbox" runat="server" ToolTip="Net Receipt HEDCess"
                                                    Width="100px" onkeyup="Total();TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="tradvexc" runat="server">
                                            <td colspan="2" align="center">
                                                <asp:Label ID="Label21" CssClass="eslbl" runat="server" Text="Service Tax"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetadvsevtax" CssClass="estbox" runat="server" ToolTip="Net Receipt Tax"
                                                    Width="100px" onkeyup="TotalAdv();"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td colspan="2" align="center">
                                                <asp:Label ID="Label20" CssClass="eslbl" runat="server" Text="Sales Tax:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetadvsaltax" CssClass="estbox" runat="server" ToolTip="Net Receipt sales Tax"
                                                    Width="100px" onkeyup="TotalAdv();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr id="NETDetails" runat="server">
                                            <td>
                                                <asp:Label ID="lblnettax" CssClass="eslbl" runat="server" Text="Sales Tax:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnettax" CssClass="estbox" runat="server" ToolTip="Net Receipt Tax"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td class="style9">
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Freight"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetfre" CssClass="estbox" runat="server" ToolTip="Net Freight"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Insurance"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnetins" CssClass="estbox" runat="server" ToolTip="Net Insurance"
                                                    Width="100px" onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox><span
                                                        class="starSpan">*</span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="month" runat="server">
                                <td>
                                    View Records of:&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlmonth" runat="server" CssClass="esddown" ToolTip="Month">
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
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="esddown" ToolTip="Year" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlpo" runat="server" ToolTip="Po" Width="105px" CssClass="esddown">
                                    </asp:DropDownList>
                                    <asp:Button ID="Button1" runat="server" CssClass="" Text="go" ToolTip="Go" Height="20px"
                                        align="center" OnClick="Button1_Click" />
                                </td>
                            </tr>
                            <tr id="grid" runat="server">
                                <td align="center" colspan="2">
                                    <asp:GridView ID="grd" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="InvoiceNo" Width="366px" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" OnRowDataBound="grd_RowDataBound" ShowFooter="true"
                                        OnSelectedIndexChanged="grd_SelectedIndexChanged" Font-Size="Small">
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" InsertVisible="False"
                                                ReadOnly="True" FooterText="Total" />
                                            <asp:BoundField DataField="cc_code" HeaderText="CC CODE" />
                                            <asp:BoundField DataField="Dca_code" HeaderText="DCA CODE" />
                                            <asp:BoundField DataField="Vendor_id" HeaderText="Vendor ID" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="tds" HeaderText="TDS" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="Retention" HeaderText="Retention" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="Hold" HeaderText="Hold" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:#,##,##,###.00}"
                                                HtmlEncode="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll();" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAll_CheckedChanged" onclick="javascript:SelectAllCheckboxes(this);"
                                                        runat="server" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("invoice_date")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("status")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="innertab" runat="server" id="FD" width="100%">
                                        <tr>
                                            <td id="fdr" runat="server">
                                                FDR No:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfdr" CssClass="estbox" runat="server" ToolTip="FDR No" Width="100px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                Principle:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtprinciple" CssClass="estbox" runat="server" ToolTip="Principle"
                                                    Width="100px"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                Interest:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtintrest" CssClass="estbox" runat="server" ToolTip="Interest"
                                                    Width="100px"></asp:TextBox><span class="starSpan">*</span>
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
                                                <asp:TextBox ID="txtname" CssClass="estbox" runat="server" ToolTip="Name" Width="200px"></asp:TextBox><span
                                                    class="starSpan">*</span><span id="span2" class="esaxspan"></span> ``````
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="paymentdetails">
                                        <tr>
                                            <th align="center" colspan="4">
                                                Payment Details
                                            </th>
                                        </tr>
                                        <tr id="bank" runat="server">
                                            <td>
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="Bank:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" AutoPostBack="true"
                                                    Width="200px" OnSelectedIndexChanged="ddlfrom_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltobank" runat="server" CssClass="eslbl" Text="To:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddltobank" runat="server" ToolTip="Bank To Transfer" CssClass="esddown"
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown10" runat="server" TargetControlID="ddltobank"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="to"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr id="ModeofPay" runat="server">
                                            <td>
                                                Mode Of Pay:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpayment" AutoPostBack="true" runat="server" ToolTip="Mode Of Pay"
                                                    CssClass="esddown" Width="70" OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="Invoice Date"
                                                    Width="80px"></asp:TextBox><span class="starSpan">*</span>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDatepayment"
                                                    Animated="true" PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                                <%-- <img onclick="scwShow(document.getElementById('<%=txtdate.ClientID %>'),this);" alt=""
                                                    src="images/cal.gif" style="width: 15px; height: 15px;" id="Img2" />--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Text="No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                                <asp:DropDownList ID="ddlcheque" runat="server" ToolTip="Cheque No" CssClass="esddown"
                                                    Width="100">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Remarks:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                Amount:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" onkeyup="Amounvalidation(this.value);"
                                                    ToolTip="Amount" Width="200px"></asp:TextBox><span class="starSpan">*</span>
                                                <asp:HiddenField ID="hf1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table class="estbl" width="660px">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />
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

        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
            spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {

                    if (elm[i].checked != xState)
                        elm[i].click();


                }
    }
    </script>
    <script language="javascript">

        function check() {
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                SetContextKey('dp8', '');
            }
            else {

            }
        }
        check();
 
 
    </script>
    <script language="javascript">
        function SelectAll() {
            var GridView2 = document.getElementById("<%=grd.ClientID %>");
            var service = document.getElementById("<%=ddlservice.ClientID %>");
            var subtype = document.getElementById("<%=ddlsubtype.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                if (GridView2.rows(rowCount).cells(10).children(0) != null) {
                    if (GridView2.rows(rowCount).cells(10).children(0).checked == true) {
                        if (subtype.selectedIndex == 1) {
                            if (service.selectedIndex == 2) {
                                var value = GridView2.rows(rowCount).cells(6).innerText.replace(/,/g, "");
                            }
                            else if (service.selectedIndex == 3) {
                                var value = GridView2.rows(rowCount).cells(7).innerText.replace(/,/g, "");

                            }
                            else if (service.selectedIndex == 4) {
                                var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");
                            }
                            else if (service.selectedIndex == 1 || service.selectedIndex == 5 || service.selectedIndex == 6 || service.selectedIndex == 7 || service.selectedIndex == 8) {
                                var value = GridView2.rows(rowCount).cells(9).innerText.replace(/,/g, "");

                            }
                        }
                        else {
                            if (service.selectedIndex == 1 || service.selectedIndex == 3 || service.selectedIndex == 4 || service.selectedIndex == 5) {
                                var value = GridView2.rows(rowCount).cells(9).innerText.replace(/,/g, "");
                            }
                            else if (service.selectedIndex == 2) {
                                var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");
                            }
                        }
                        if (value != "") {
                            originalValue += parseFloat(value);

                        }
                    }
                }
            }
            //var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtamt.ClientID%>').value = originalValue;

        }
    </script>
    <script language="javascript">
        function Amounvalidation(amount) {
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                var subtype1 = document.getElementById("<%=ddlsubtype.ClientID %>").value;
                if (subtype1 == "Service Provider" || subtype1 == "Supplier") {
                    var GridView2 = document.getElementById("<%=grd.ClientID %>");
                    var service = document.getElementById("<%=ddlservice.ClientID %>");
                    var subtype = document.getElementById("<%=ddlsubtype.ClientID %>");
                    var originalValue = 0;
                    for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                        if (GridView2.rows(rowCount).cells(10).children(0) != null) {
                            if (GridView2.rows(rowCount).cells(10).children(0).checked == true) {
                                if (subtype.selectedIndex == 1) {
                                    if (service.selectedIndex == 2) {
                                        var value = GridView2.rows(rowCount).cells(6).innerText.replace(/,/g, "");
                                    }
                                    else if (service.selectedIndex == 3) {
                                        var value = GridView2.rows(rowCount).cells(7).innerText.replace(/,/g, "");

                                    }
                                    else if (service.selectedIndex == 4) {
                                        var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");
                                    }
                                    else if (service.selectedIndex == 1 || service.selectedIndex == 5 || service.selectedIndex == 6 || service.selectedIndex == 7 || service.selectedIndex == 8) {
                                        var value = GridView2.rows(rowCount).cells(9).innerText.replace(/,/g, "");

                                    }
                                }
                                else {
                                    if (service.selectedIndex == 1 || service.selectedIndex == 3 || service.selectedIndex == 4 || service.selectedIndex == 5) {
                                        var value = GridView2.rows(rowCount).cells(9).innerText.replace(/,/g, "");
                                    }
                                    else if (service.selectedIndex == 2) {
                                        var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");
                                    }
                                }
                                if (value != "") {
                                    originalValue += parseFloat(value);
                                }
                            }
                        }
                    }

                    var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);

                    if (parseFloat(originalValue) < parseFloat(document.getElementById('<%= txtamt.ClientID%>').value)) {
                        window.alert("Invalid");
                        document.getElementById('<%= txtamt.ClientID%>').value = "";
                        return false;
                    }

                }

                else if (subtype1 == "General") {
                    document.getElementById("<%=txtamt.ClientID %>").value = document.getElementById("<%=hf1.ClientID %>").value
                    return false;
                }
            }
        }
    </script>
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
</asp:Content>
