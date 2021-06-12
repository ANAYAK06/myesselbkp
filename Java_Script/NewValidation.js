function Newvalidate() {
    //            debugger;
    var type = document.getElementById('ctl00_ContentPlaceHolder1_ddltype');
    var objs = new Array('ctl00_ContentPlaceHolder1_ddltype', 'ctl00_ContentPlaceHolder1_txtsharedate');
    if (!CheckInputs(objs)) {
        return false;
    }
    if (type.selectedIndex == 1) {
        var objs = new Array('ctl00_ContentPlaceHolder1_txtshareholdername');
        if (!CheckInputs(objs)) {
            return false;
        }
    }
    else if (type.selectedIndex == 2) {
        var objs = new Array('ctl00_ContentPlaceHolder1_ddlshareholdername');
        if (!CheckInputs(objs)) {
            return false;
        }
    }
    var objs = new Array('ctl00_ContentPlaceHolder1_txtamount', 'ctl00_ContentPlaceHolder1_Ledger_txtledgername', 'ctl00_ContentPlaceHolder1_Ledger_ddlsubgroup', 'ctl00_ContentPlaceHolder1_Ledger_txtledbaldate', 'ctl00_ContentPlaceHolder1_Ledger_txtopeningbal', 'ctl00_ContentPlaceHolder1_ddlfrom', 'ctl00_ContentPlaceHolder1_ddlpayment', 'ctl00_ContentPlaceHolder1_txtdate', 'ctl00_ContentPlaceHolder1_txtcheque', 'ctl00_ContentPlaceHolder1_txtdesc');

    if (!CheckInputs(objs)) {

        return false;

    }
    if (!ChceckRBL('ctl00_ContentPlaceHolder1_Ledger_rbtnpaymenttype')) {
        return false;
    }
    var bank = document.getElementById('ctl00_ContentPlaceHolder1_ddlfrom').value;
    var response = confirm("Do you want to Continue with the " + bank);

    if (response) {
        document.getElementById('ctl00_ContentPlaceHolder1_btnsubmit').style.display = 'none';
        return true;
    }
    else {
        document.getElementById('ctl00_ContentPlaceHolder1_btnsubmit').style.display = 'block';
        return false;
    }
}
function ValidateUnsecuredLoan() {
    //debugger;
    var objs = new Array('ctl00_ContentPlaceHolder1_ddltype');
    if (!CheckInputs(objs)) {
        return false;
    }
    var type = document.getElementById("ctl00_ContentPlaceHolder1_ddltype");
    if (type.selectedIndex == 1) {
        var objs = new Array("ctl00_ContentPlaceHolder1_txtunsecurednoumber", "ctl00_ContentPlaceHolder1_txtloandate", "ctl00_ContentPlaceHolder1_txtname", "ctl00_ContentPlaceHolder1_txtintrestrate");
        if (!CheckInputs(objs)) {
            return false;
        }
    }
    else if (type.selectedIndex == 2 || type.selectedIndex == 3) {
        var objs = new Array("ctl00_ContentPlaceHolder1_ddlunsecuredloanno", "ctl00_ContentPlaceHolder1_txtloandate");
        if (!CheckInputs(objs)) {
            return false;
        }
        if (type.selectedIndex == 3) {
            var returnconfirm = document.getElementById("ctl00_ContentPlaceHolder1_ddlselection").value;
            if (returnconfirm == "Select") {
                alert("Please Select Deduction Yes/No");
                return false;
            }
            if (returnconfirm == "Yes") {
                var objs = new Array("ctl00_ContentPlaceHolder1_ddldeddca", "ctl00_ContentPlaceHolder1_ddldedsdca", "ctl00_ContentPlaceHolder1_txtdedamount");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
        }
    }
    var objs = new Array('ctl00_ContentPlaceHolder1_txtamount', 'ctl00_ContentPlaceHolder1_Ledger_txtledgername', 'ctl00_ContentPlaceHolder1_Ledger_ddlsubgroup', 'ctl00_ContentPlaceHolder1_Ledger_txtledbaldate', 'ctl00_ContentPlaceHolder1_Ledger_txtopeningbal', "ctl00_ContentPlaceHolder1_ddlfrom", "ctl00_ContentPlaceHolder1_ddlpayment", "ctl00_ContentPlaceHolder1_txtdate", "ctl00_ContentPlaceHolder1_txtcheque", "ctl00_ContentPlaceHolder1_txtdesc");
    if (!CheckInputs(objs)) {
        return false;
    }
    if (type.selectedIndex == 1) {
        if (!ChceckRBL('ctl00_ContentPlaceHolder1_Ledger_rbtnpaymenttype')) {
            return false;
        }
    }
    var ActuallAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtamt").value;
    if (parseFloat(ActuallAmt) <= -1) {
        alert("Final Amount Can not in Negative Value");
        return false;
    }
    var bank = document.getElementById("ctl00_ContentPlaceHolder1_ddlfrom").value;
    var response = confirm("Do you want to Continue with the " + bank);

    if (response) {
        return true;
    }
    else {
        return false;
    }
    document.getElementById("ctl00_ContentPlaceHolder1_btnsubmit").style.display = 'none';
}


function validateVendor() {
    var objs = new Array("ctl00_ContentPlaceHolder1_ddlVType", "ctl00_ContentPlaceHolder1_txtvatpan", "ctl00_ContentPlaceHolder1_txttintax", "ctl00_ContentPlaceHolder1_txtcstpf",
                                "ctl00_ContentPlaceHolder1_txtVName", "ctl00_ContentPlaceHolder1_txtAddress");
    if (!CheckInputs(objs)) {
        return false;
    }
    if (!ChceckRBL("ctl00_ContentPlaceHolder1_rbtngst")) {
        return false;
    }
    if (SelectedIndex("ctl00_ContentPlaceHolder1_rbtngst") == 0) {
        GridView3 = document.getElementById("ctl00_ContentPlaceHolder1_gvother");
        if (GridView3 != null) {
            for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                if (GridView3.rows(rowCount).cells(1).children(0).checked == false) {
                    window.alert("Please verify");
                    return false;
                }
                else if (GridView3.rows(rowCount).cells(2).children[0].value == "Select") {
                    window.alert("Select State");
                    GridView3.rows(rowCount).cells(2).children[0].focus();
                    return false;
                }
                else if (GridView3.rows(rowCount).cells(3).children[0].value == "") {
                    window.alert("GST No Required");
                    GridView3.rows(rowCount).cells(3).children[0].focus();
                    return false;
                }
            }
        }
    }
    if (document.getElementById("ctl00_ContentPlaceHolder1_rbtnadd").checked) {
        var objs = new Array('ctl00_ContentPlaceHolder1_Ledger_txtledgername', 'ctl00_ContentPlaceHolder1_Ledger_ddlsubgroup', 'ctl00_ContentPlaceHolder1_Ledger_txtledbaldate', 'ctl00_ContentPlaceHolder1_Ledger_txtopeningbal');
        if (!CheckInputs(objs)) {
            return false;
        }
        if (!ChceckRBL('ctl00_ContentPlaceHolder1_Ledger_rbtnpaymenttype')) {
            return false;
        }
    }
    document.getElementById("ctl00_ContentPlaceHolder1_btnAddVendor").style.display = 'none';
    return true;

}