<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmAddDCA.aspx.cs"
    Inherits="Admin_frmAddDCA" EnableEventValidation="false" Title="Add DCA/SDCA - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function closepopup() {
            $find('mdledit').hide();
            window.location.reload();
            
        }
        function showpopup() {
            Popload();
            $find('mdledit').show();
           
        }
    </script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>
    <script language="javascript">
        function validate() {
            if (!ChceckRBL("<%=rbtntype.ClientID %>"))
                return false;

            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                var objs = new Array("<%=ddldca.ClientID %>", "<%=txtsubdca.ClientID %>", "<%=txtsdname.ClientID %>", "<%=ddlit.ClientID %>");
            }
            else {
                if (!ChceckRBL("<%=rbtndcatype.ClientID %>"))
                    return false;
                if (SelectedIndex("<%=rbtndcatype.ClientID %>") == 0) {
                    var objs = new Array("<%=txtDcaCode.ClientID %>", "<%=txtDcaName.ClientID %>", "<%=ddlit.ClientID %>");
                }
                else if (SelectedIndex("<%=rbtndcatype.ClientID %>") == 1) {
                    var objs = new Array("<%=ddltaxtype.ClientID %>", "<%=ddlnatureoftax.ClientID %>", "<%=txtDcaCode.ClientID %>", "<%=txtDcaName.ClientID %>", "<%=ddlit.ClientID %>");
                }
        }

        if (!CheckInputs(objs)) {
            return false;
        }

        if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
            if (SelectedIndex("<%=rbtndcatype.ClientID %>") == 1) {
                var rbl = document.getElementById("<%=chktaxnos.ClientID %>");
                var models = rbl.getElementsByTagName("input");
                var checkBoxCount = 0;
                var a;
                var b = 0;
                for (var a = 0; a < models.length; a++) {
                    if (models[a].checked == true) {
                        b++;
                    }
                }
                if (b == 0) {
                    window.alert("Please select at least one Tax no.");
                    return false;
                }
            }
            var rbl = document.getElementById("<%=chklist.ClientID %>");
            var models = rbl.getElementsByTagName("input");
            var checkBoxCount = 0;
            var i;
            var j = 0;
            for (var i = 0; i < models.length; i++) {
                if (models[i].checked == true) {
                    j++;
                }
            }
            if (j == 0) {
                window.alert("Please select at least one CC Type.");
                return false;
            }
            var pay = document.getElementById("<%=chkpaymenttype.ClientID %>");
            var paymodels = pay.getElementsByTagName("input");
            var checkBoxCount1 = 0;
            var k;
            var l = 0;
            for (var k = 0; k < paymodels.length; k++) {
                if (paymodels[k].checked == true) {
                    l++;
                }
            }
            if (l == 0) {
                window.alert("Please select at least one payment type.");
                return false;
            }

        }
        document.getElementById("<%=btnAddDCA.ClientID %>").style.display = 'none';
            return true;
        }

    </script>
    <script language="javascript"> //working
        function updatevalidate() {
            if (!ChceckRBL("<%=rbtnedit.ClientID %>"))
                return false;

            if (SelectedIndex("<%=rbtnedit.ClientID %>") == 1) {
                var objs = new Array("<%=ddleditdca.ClientID %>", "<%=ddlsdcode.ClientID %>", "<%=txtname.ClientID %>");
            }
            else {
                var objs = new Array("<%=ddleditdca.ClientID %>", "<%=txteditdcaname.ClientID %>");
            }

            if (!CheckInputs(objs)) {
                return false;
            }
            if (SelectedIndex("<%=rbtnedit.ClientID %>") == 0) {

                var rbl = document.getElementById("<%=chkedit.ClientID %>");
                var models = rbl.getElementsByTagName("input");
                var checkBoxCount = 0;
                var i;
                var j = 0;
                for (var i = 0; i < models.length; i++) {
                    if (models[i].checked == true) {
                        j++;
                    }
                }
                if (j == 0) {
                    window.alert("Please select at least one CC Type.");
                    return false;
                }

                var pay = document.getElementById("<%=chkeditttype.ClientID %>");
                var paymodels = pay.getElementsByTagName("input");
                var checkBoxCount = 0;
                var k;
                var l = 0;
                for (var k = 0; k < paymodels.length; k++) {
                    if (paymodels[k].checked == true) {
                        l++;
                    }
                }
                if (l == 0) {
                    window.alert("Please select at least one payment type.");
                    return false;
                }
                if (SelectedIndex("<%=rbtndcatypep.ClientID %>") == 1) {
                    document.getElementById("<%=ddltaxtypep.ClientID %>").disabled = true
                    document.getElementById("<%=ddlnatureoftaxp.ClientID %>").disabled = true
                    var taxes = document.getElementById("<%=chktaxnosp.ClientID %>");
                    var taxmodels = taxes.getElementsByTagName("input");
                    var checkBoxCount = 0;
                    var m;
                    var n = 0;
                    for (var m = 0; m < taxmodels.length; m++) {
                        if (taxmodels[m].checked == true) {
                            n++;
                        }
                    }
                    if (n == 0) {
                        window.alert("Please select at least one Tax No.");
                        return false;
                    }
                }
                var rblcheck = document.getElementById("<%=chkedit.ClientID %>");
                var rblpaycheck = document.getElementById("<%=chkeditttype.ClientID %>");
                var EditDCA = document.getElementById("<%=ddleditdca.ClientID %>").value;
                var modelscheck = rblcheck.getElementsByTagName("input");
                var paymodelscheck = rblpaycheck.getElementsByTagName("input");
                if (EditDCA == "DCA-11" || EditDCA == "DCA-27") {
                    for (var n = 0; n < modelscheck.length; n++) {
                        var checkRef = modelscheck[n];
                        var lblArray = checkRef.parentNode.getElementsByTagName('label');
                        if (modelscheck[n].checked == true && lblArray[0].innerHTML == "Capital" && EditDCA == "DCA-11") {

                            window.alert("Please UnCheck " + lblArray[0].innerHTML);
                            //                            modelscheck[n].checked = false;
                            return false;
                        }
                        if (modelscheck[n].checked == false && EditDCA == "DCA-11" && (lblArray[0].innerHTML == "Service" || lblArray[0].innerHTML == "Manufacturing" || lblArray[0].innerHTML == "Non-Performing")) {

                            window.alert("Please Check " + lblArray[0].innerHTML);
                            return false;
                        }
                        if (modelscheck[n].checked == true && EditDCA == "DCA-27" && (lblArray[0].innerHTML == "Service" || lblArray[0].innerHTML == "Trading" || lblArray[0].innerHTML == "Manufacturing" || lblArray[0].innerHTML == "Non-Performing")) {

                            window.alert("Please UnCheck " + lblArray[0].innerHTML);
                            //                            modelscheck[n].checked = false;
                            return false;
                        }
                        if (modelscheck[n].checked == false && lblArray[0].innerHTML == "Capital" && EditDCA == "DCA-27") {

                            window.alert("Please Check " + lblArray[0].innerHTML);
                            return false;
                        }
                    }
                }
                if (EditDCA == "DCA-11" || EditDCA == "DCA-27") {
                    for (var A = 0; A < paymodelscheck.length; A++) {
                        var paycheckRef = paymodelscheck[A];
                        var paylblArray = paycheckRef.parentNode.getElementsByTagName('label');
                        if (EditDCA == "DCA-11") {
                            if (paymodelscheck[A].checked == false && (paylblArray[0].innerHTML == "Direct payment" || paylblArray[0].innerHTML == "Vendor through Payment")) {

                                window.alert("Please Select " + paylblArray[0].innerHTML);
                                return false;
                            }
                        }
                        if (EditDCA == "DCA-27") {
                            if (paymodelscheck[A].checked == false && (paylblArray[0].innerHTML == "Vendor through Payment")) {

                                window.alert("Please Select " + paylblArray[0].innerHTML);
                                return false;
                            }

                        }
                    }
                }
            }
        }

    </script>
    <script type="text/javascript" language="javascript">
        function check() {
            var txtdca = document.getElementById("<%=dcacode.ClientID %>");
            var ddldca = document.getElementById("<%=trdca.ClientID %>");
            var sdca = document.getElementById("<%=sdca.ClientID %>");
            var sdname = document.getElementById("<%=sdname.ClientID %>");
            var dname = document.getElementById("<%=dcaname.ClientID %>");
            var it = document.getElementById("<%=it.ClientID %>");
            var btn = document.getElementById("<%=btn.ClientID %>");
            var SubType = document.getElementById("<%=SubType.ClientID %>");
            var paymenttype = document.getElementById("<%=trpaymenttype.ClientID %>");

            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                txtdca.style.display = 'block';
                sdca.style.display = 'none';
                sdname.style.display = 'none';
                dname.style.display = 'block';
                ddldca.style.display = 'none';
                it.style.display = 'block';
                btn.style.display = 'block';
                SubType.style.display = 'block';
                paymenttype.style.display = 'block';
            }
            else if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                txtdca.style.display = 'none';
                sdca.style.display = 'block';
                sdname.style.display = 'block';
                dname.style.display = 'none';
                ddldca.style.display = 'block';
                it.style.display = 'block';
                btn.style.display = 'block';
                SubType.style.display = 'none';
                paymenttype.style.display = 'none';
            }

        }
    </script>
    <script type="text/javascript" language="javascript">
        function editcheck() {
            //debugger;
            //var editsubdca = document.getElementById("<%=treditsudca.ClientID %>");
            //var type = document.getElementById("<%=tredittype.ClientID %>");
            //var sdca = document.getElementById("<%=treditsudca.ClientID %>");
            //var sdname = document.getElementById("<%=treditsubdcaname.ClientID %>");
            var dca = document.getElementById("<%=treditdca.ClientID %>");
            var editbtn = document.getElementById("<%=trbtnupdate.ClientID %>");
            var lbl = document.getElementById("<%=Label14.ClientID %>");
            //var dcaname = document.getElementById("<%=trdcaname.ClientID %>");
            //var paymenttype = document.getElementById("<%=treditpaymenttype.ClientID %>");
            //var trdcatype_p = document.getElementById("<%=trdcatype_p.ClientID %>");
            //var trtaxtype_p = document.getElementById("<%=trtaxtype_p.ClientID %>");
            //var trnatureoftax_p = document.getElementById("<%=trnatureoftax_p.ClientID %>");
            //var trtaxnos_p = document.getElementById("<%=trtaxnos_p.ClientID %>");
            if (SelectedIndex("<%=rbtnedit.ClientID %>") == 0) {
                //editsubdca.style.display = 'none';
                //sdca.style.display = 'none';
                //sdname.style.display = 'none';
                //type.style.display = 'none';
                dca.style.display = 'none';
                //dcaname.style.display = 'none';
                editbtn.style.display = 'none';
                //paymenttype.style.display = 'none';
                //trdcatype_p.style.display = 'block';
                //trtaxtype_p.style.display = 'none';
                //trnatureoftax_p.style.display = 'none';
                //trtaxnos_p.style.display = 'none';
                if (SelectedIndex("<%=rbtndcatypep.ClientID %>") == 0) {
                    //editsubdca.style.display = 'none';
                    //sdca.style.display = 'none';
                    //sdname.style.display = 'none';
                    //type.style.display = 'block';
                    dca.style.display = 'block';
                    //dcaname.style.display = 'block';
                    editbtn.style.display = 'block';
                    //paymenttype.style.display = 'block';
                    document.getElementById("<%=txtname.ClientID %>").value = "";
                    document.getElementById("<%=txteditdcaname.ClientID %>").value = "";
                    document.getElementById("<%=ddleditdca.ClientID %>").selectedIndex = 0;
                    document.getElementById("<%=ddlsdcode.ClientID %>").selectedIndex = 0;
                    //trdcatype_p.style.display = 'block';
                    //trtaxtype_p.style.display = 'none';
                    //trnatureoftax_p.style.display = 'none';
                    //trtaxnos_p.style.display = 'none';
                }
                else if (SelectedIndex("<%=rbtndcatypep.ClientID %>") == 1) {
                    //editsubdca.style.display = 'none';
                    //sdca.style.display = 'none';
                    //sdname.style.display = 'none';
                    //type.style.display = 'block';
                    dca.style.display = 'block';
                    //dcaname.style.display = 'block';
                    editbtn.style.display = 'block';
                    //paymenttype.style.display = 'block';
                    document.getElementById("<%=txtname.ClientID %>").value = "";
                    document.getElementById("<%=txteditdcaname.ClientID %>").value = "";
                    document.getElementById("<%=ddleditdca.ClientID %>").selectedIndex = 0;
                    document.getElementById("<%=ddlsdcode.ClientID %>").selectedIndex = 0;
                    //trdcatype_p.style.display = 'block';
                    //trtaxtype_p.style.display = 'block';
                    //trnatureoftax_p.style.display = 'block';
                    //trtaxnos_p.style.display = 'block';
                }

        }
        else if (SelectedIndex("<%=rbtnedit.ClientID %>") == 1) {
                //editsubdca.style.display = 'block';
                //type.style.display = 'none';
                //sdca.style.display = 'block';
                //sdname.style.display = 'block';
                //dcaname.style.display = 'none';
                dca.style.display = 'block';
                //dcaname.style.display = 'block';
                editbtn.style.display = 'block';
                //paymenttype.style.display = 'none';
                document.getElementById("<%=txtname.ClientID %>").value = "";
                //document.getElementById("<%=txteditdcaname.ClientID %>").value = "";
            document.getElementById("<%=ddleditdca.ClientID %>").selectedIndex = 0;
            document.getElementById("<%=ddlsdcode.ClientID %>").selectedIndex = 0;
            //trdcatype_p.style.display = 'none';
            //trtaxtype_p.style.display = 'none';
            //trnatureoftax_p.style.display = 'none';
            //trtaxnos_p.style.display = 'none';
        }
    CheckboxClear();
}
    </script>
    <script language="javascript">

        var POCheckerTimer;
        function GetDCA() {
            
            var DCA = document.getElementById("<%=ddleditdca.ClientID %>").value;
            PageMethods.GetDCADetails(DCA, OnSuccess);
        }
        function OnSuccess(result, userContext, methodName) {
            if (methodName == "GetDCADetails") {
                var strBatchNo = result;
                var arrayItems = strBatchNo.split('|');
                //var tableBody = document.getElementById("<%=chkedit.ClientID %>").childNodes[0];
                CheckboxClear();

                var elementRef = document.getElementById("<%=chkedit.ClientID %>");
                var checkBoxArray = elementRef.getElementsByTagName('input');
                var checkedValues = "";
                for (var i = 0; i < checkBoxArray.length; i++) {
                    var checkBoxRef = checkBoxArray[i];
                    var labelArray = checkBoxRef.parentNode.getElementsByTagName('label');
                    if (labelArray.length > 0) {

                        if (labelArray[0].innerHTML == arrayItems[1]) {
                            checkBoxArray[i].checked = true;
                        }
                        if (labelArray[0].innerHTML == arrayItems[2]) {
                            checkBoxArray[i].checked = true;
                        }
                        if (labelArray[0].innerHTML == arrayItems[3]) {
                            checkBoxArray[i].checked = true;
                        }
                        if (labelArray[0].innerHTML == arrayItems[4]) {
                            checkBoxArray[i].checked = true;
                        }
                        if (labelArray[0].innerHTML == arrayItems[5]) {
                            checkBoxArray[i].checked = true;
                        }
                    }
                }
                Getpaymentdetails();

            }

        }

    </script>
    <script language="javascript">
        function Getpaymentdetails() {
           var DCACode = document.getElementById("<%=ddleditdca.ClientID %>").value;
            PageMethods.GetPaymentDetails(DCACode, OnSuccess1);

        }
        function OnSuccess1(result1, userContext, methodName) {
            if (methodName == "GetPaymentDetails") {
                var strBatch = result1;
                var array = strBatch.split('|');
                var paymenttype = document.getElementById("<%=chkeditttype.ClientID %>");
                var checkBoxpay = paymenttype.getElementsByTagName('input');
                for (var j = 0; j < checkBoxpay.length; j++) {
                    var paymenttype = checkBoxpay[j];
                    var label = paymenttype.parentNode.getElementsByTagName('label');
                    if (label.length > 0) {

                        if (label[0].innerHTML == array[1]) {
                            checkBoxpay[j].checked = true;
                        }
                        if (label[0].innerHTML == array[2]) {
                            checkBoxpay[j].checked = true;
                        }

                    }
                }
                if (SelectedIndex("<%=rbtndcatypep.ClientID %>") == 1) {
                    Gettaxnos();
                }
                if (SelectedIndex("<%=rbtndcatypep.ClientID %>") == 0) {
                    Getdcaname();
                }
            }
        }
        function Gettaxnos() {
            var DCACode = document.getElementById("<%=ddleditdca.ClientID %>").value;
            PageMethods.Gettaxes(DCACode, OnSuccess2);

        }
        function OnSuccess2(result2, userContext, methodName) {

            if (methodName == "Gettaxes") {
                var strBatch = result2;
                var array = strBatch.split('|');
                var arrayLength = array.length;
                var ttype = document.getElementById("<%=chktaxnosp.ClientID %>");
                var checkBoxpay = ttype.getElementsByTagName('input');
                for (var j = 0; j < checkBoxpay.length; j++) {
                    var ttype = checkBoxpay[j];
                    var label = ttype.parentNode.getElementsByTagName('label');
                    if (label.length > 0) {
                        for (var i = 0; i < arrayLength; i++) {
                            if (label[0].innerHTML == array[i]) {
                                checkBoxpay[j].checked = true;
                            }
                        }

                    }
                }
                if (SelectedIndex("<%=rbtndcatypep.ClientID %>") == 1) {
                    Gettaxtypes();
                    Getdcaname();
                }
               
            }
        }

        function Gettaxtypes() {
            var DCACode = document.getElementById("<%=ddleditdca.ClientID %>").value;
            PageMethods.Gettaxetypes(DCACode, OnSuccess3);

        }
        function OnSuccess3(result3, userContext, methodName) {

            var ddltaxtypep = document.getElementById("<%=ddltaxtypep.ClientID %>");
            var ddlnatureoftaxp = document.getElementById("<%=ddlnatureoftaxp.ClientID %>");
            if (methodName == "Gettaxetypes") {
                var strBatch = result3;
                var array = strBatch.split('|');
                ddltaxtypep.value = array[0];
                ddlnatureoftaxp.value = array[1];
                document.getElementById('<%= ddltaxtypep.ClientID %>').disabled = true;
                document.getElementById('<%= ddlnatureoftaxp.ClientID %>').disabled = true;
            }
        }
        function Getdcaname() {
            
            var DCACode = document.getElementById("<%=ddleditdca.ClientID %>").value;
            PageMethods.Getdcaname(DCACode, OnSuccess4);

        }
        function OnSuccess4(result4, userContext, methodName) {

            if (methodName == "Getdcaname") {
                var strBatch = result4;
                var DCAname = document.getElementById("<%=txteditdcaname.ClientID %>");
                DCAname.value = strBatch;
            }
        }
    </script>
    <script language="javascript">
        function CheckboxClear() {

            if (SelectedIndex("<%=rbtnedit.ClientID %>") == 0) {
                var chkBoxList = document.getElementById("<%=chkedit.ClientID %>");
                var chkBoxCount = chkBoxList.getElementsByTagName("input");
                var chklistpaytype = document.getElementById("<%=chkeditttype.ClientID %>");
                var chklistpaytypeBoxCount = chklistpaytype.getElementsByTagName("input");
                var chktaxnosp = document.getElementById("<%=chktaxnosp.ClientID %>");
                if (SelectedIndex("<%=rbtndcatypep.ClientID %>") == 1) {
                    var chktaxnospBoxCount = chktaxnosp.getElementsByTagName("input");
                }
                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
                for (var j = 0; j < chklistpaytypeBoxCount.length; j++) {
                    chklistpaytypeBoxCount[j].checked = false;
                }
                if (SelectedIndex("<%=rbtndcatypep.ClientID %>") == 1) {
                    for (var k = 0; k < chktaxnospBoxCount.length; k++) {
                        chktaxnospBoxCount[k].checked = false;
                    }
                }
                return false;
            }
        }
        
    </script>
  
    <style>
        .buttonlnk {
            font: bold 11px Arial;
            text-decoration: none;
            background-color: #EEEEEE;
            color: #333333;
            padding: 2px 6px 2px 6px;
            border-top: 1px solid #CCCCCC;
            border-right: 1px solid #333333;
            border-bottom: 1px solid #333333;
            border-left: 1px solid #CCCCCC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnl" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table style="width: 700px; padding-top: 10px">
                            <tr valign="top">
                                <td align="center">
                                    <table width="500px" style="background-color: gray; vertical-align: middle">
                                        <tr style="border: 1px solid #000; width: 400px; height: 10px">
                                            <td colspan="2px">
                                                <div style="vertical-align: middle">
                                                    <div style="text-align: center; vertical-align: middle;">
                                                        <asp:Label ID="Label1" runat="server" Text="Add DCA/Sub DCA Form" CssClass="eslbl"></asp:Label>
                                                    </div>
                                                    <div style="text-align: right; vertical-align: middle;">
                                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="Popload();" CssClass="buttonlnk">Edit</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                    <table class="estbl" width="500px">
                                        <tr style="width: 400px; align-content: center">
                                            <td style="vertical-align: middle;" colspan="2">
                                                <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Transction Type" Width="100%"
                                                    RepeatDirection="Horizontal" runat="server" AutoPostBack="true" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                                    <asp:ListItem>Add DCA</asp:ListItem>
                                                    <asp:ListItem>Add SubDCA</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trdcatype" runat="server" align="center">
                                            <td style="width: 250px;" colspan="2">
                                                <asp:RadioButtonList ID="rbtndcatype" CssClass="esrbtn" Style="font-size: small" ToolTip="DCA Type"
                                                    RepeatDirection="Horizontal" runat="server" OnSelectedIndexChanged="rbtndcatype_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="Expense">General DCA</asp:ListItem>
                                                    <asp:ListItem Value="Tax">Tax DCA</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trtaxtype" runat="server">
                                            <td style="width: 200px" align="center">
                                                <asp:Label ID="Label10" runat="server" Text="Type of Tax" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="left" style="width: 420px; height: 29px;">
                                                <asp:DropDownList ID="ddltaxtype" runat="server" ToolTip="Type of Tax" Width="50%" AutoPostBack="true"
                                                    CssClass="esddown">
                                                    <Items>
                                                        <asp:ListItem Text="Select" Value="Select" />
                                                        <asp:ListItem Text="Creditable" Value="Creditable" />
                                                        <asp:ListItem Text="Non-Creditable" Value="Non-Creditable" />
                                                    </Items>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trnatureoftax" runat="server">
                                            <td style="width: 200px" align="center">
                                                <asp:Label ID="Label17" runat="server" Text="Nature Of Tax" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="left" style="width: 420px; height: 29px;">
                                                <asp:DropDownList ID="ddlnatureoftax" runat="server" ToolTip="Nature of Tax" Width="50%" AutoPostBack="true"
                                                    CssClass="esddown">
                                                    <Items>
                                                        <asp:ListItem Text="Select" Value="Select" />
                                                        <asp:ListItem Text="Basic Tax" Value="BasicTax" />
                                                        <asp:ListItem Text="Cess" Value="Cess" />
                                                    </Items>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="dcacode" runat="server">
                                            <td style="width: 200px" align="center">
                                                <asp:Label ID="Label2" runat="server" Text="DCA Code" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="left" style="width: 420px; height: 29px;">
                                                <asp:TextBox ID="txtDcaCode" runat="server" style="text-transform: uppercase" ToolTip="DCA Code"
                                                    CssClass="estbox" MaxLength="50"></asp:TextBox>
                                                <span class="starSpan" id="ccspan" runat="server">*</span><span class="esspan" id="spanAvailability"></span>
                                            </td>
                                        </tr>
                                        <tr id="trdca" runat="server">
                                            <td style="width: 200px" align="center">
                                                <asp:Label ID="Label9" runat="server" Text="DCACode" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="left" style="width: 420px; height: 29px;">
                                                <asp:DropDownList ID="ddldca" runat="server" ToolTip="DCA Code" Width="150px" AutoPostBack="true"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddldca_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddldca"
                                                    ServicePath="cascadingDCA.asmx" Category="dca" LoadingText="Please Wait" ServiceMethod="ISdcnew">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr id="dcaname" runat="server">
                                            <td valign="top" style="width: 200px; height: 23px">
                                                <asp:Label ID="Label3" runat="server" Text="DCA Name" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" colspan="3" align="left" style="height: 23px">
                                                <asp:TextBox ID="txtDcaName" runat="server" Width="100%" ToolTip="DCA Name" TextMode="MultiLine"
                                                    CssClass="esddown" MaxLength="80"></asp:TextBox>
                                                <span id="span1" class="ajaxspan"></span>
                                            </td>
                                        </tr>
                                        <tr id="sdca" runat="server">
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Sub-DCA Code" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtsubdca" runat="server" Width="99px" reToolTip="Sub DCA" ReadOnly="true"
                                                    onkeypress="return IsNumeric(event);" CssClass="estbox" MaxLength="50"></asp:TextBox><span
                                                        class="starSpan" id="Span2" runat="server">*</span><span class="esspan" id="span3"></span>
                                            </td>
                                        </tr>
                                        <tr id="sdname" runat="server">
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="SubDCAName" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtsdname" runat="server" Width="100px" ToolTip="SubDCA Name" CssClass="esddown"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trtaxnos" runat="server">
                                            <td>
                                                <asp:Label ID="Label18" runat="server" Text="Tax No's" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:CheckBoxList ID="chktaxnos" CssClass="esrbtn" Style="font-size: smaller" ToolTip="Tax Nos"
                                                    RepeatDirection="Horizontal" RepeatColumns="3" runat="server" CellPadding="0"
                                                    CellSpacing="0">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr id="SubType" runat="server">
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="CC Type" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="4" align="left">
                                                <asp:CheckBoxList ID="chklist" CssClass="esrbtn" Style="font-size: small" ToolTip="Sub Type"
                                                    RepeatDirection="Horizontal" RepeatColumns="3" runat="server" CellPadding="0"
                                                    CellSpacing="0">
                                                    <asp:ListItem>Service</asp:ListItem>
                                                    <asp:ListItem>Trading</asp:ListItem>
                                                    <asp:ListItem>Capital</asp:ListItem>
                                                    <asp:ListItem>Manufacturing</asp:ListItem>
                                                    <asp:ListItem>Non-Performing</asp:ListItem>

                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr id="trpaymenttype" runat="server">
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="Payment Type" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="4" align="left">
                                                <asp:CheckBoxList ID="chkpaymenttype" CssClass="esrbtn" Style="font-size: small"
                                                    ToolTip="Payment Type" RepeatDirection="Horizontal" RepeatColumns="0" runat="server"
                                                    CellPadding="0" CellSpacing="0">
                                                    <asp:ListItem>Direct payment</asp:ListItem>
                                                    <asp:ListItem>Vendor through Payment</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr id="it" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="IT Code" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="4" align="left">
                                                <asp:DropDownList ID="ddlit" runat="server" ToolTip="IT Code" Width="180px" CssClass="esddown">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlit"
                                                    ServicePath="cascadingDCA.asmx" Category="sub1" LoadingText="Please Wait" ServiceMethod="itname">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr id="btn" runat="server">
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnAddDCA" runat="server" Text="Add" CssClass="esbtn" OnClientClick="return validate()"
                                                    OnClick="btnAddDCA_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    CssClass="esbtn" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td align="center" style="width: 119px" colspan="4">
                                                <asp:Label ID="lblAlert" CssClass="eslblalert" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <cc1:ModalPopupExtender ID="popedit" BehaviorID="mdledit" runat="server" TargetControlID="LinkButton1"
                                        PopupControlID="pnledit" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                    <asp:Panel ID="pnledit" runat="server" Style="display: none;">
                                        <table width="600px" border="0" align="center" cellpadding="0" cellspacing="0" id="tblbank"
                                            runat="server">
                                            <tr>
                                                <td width="13" valign="bottom">
                                                    <img src="images/leftc.jpg">
                                                </td>
                                                <td class="pop_head" align="left" id="Td2" runat="server">
                                                    <div class="popclose">
                                                        <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                    </div>
                                                </td>
                                                <td width="13" valign="bottom">
                                                    <img src="images/rightc.jpg">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#FFFFFF">&nbsp;
                                                </td>
                                                <td height="180" valign="top" class="popcontent">
                                                    <div style="overflow: auto; margin-left: 10px; margin-right: 10px; height: 450px;">
                                                        <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
                                                                    <ProgressTemplate>
                                                                        <div>
                                                                            <img src="images/load.gif" />
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                                <table class="estbl" width="500px" align="center">
                                                                    <tr style="border: 1px solid #000">
                                                                        <th valign="top" colspan="4" align="center">
                                                                            <asp:Label ID="Label4" runat="server" Text="Edit DCA/SDCA" CssClass="eslbl"></asp:Label>
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="center">
                                                                            <asp:RadioButtonList ID="rbtnedit" CssClass="esrbtn" Style="font-size: small" ToolTip="Type"
                                                                                 RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                                                CellSpacing="0" OnSelectedIndexChanged="rbtnedit_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem>DCA</asp:ListItem>
                                                                                <asp:ListItem>SubDCA</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trdcatype_p" runat="server" align="center">
                                                                        <td style="width: 250px;" colspan="2">
                                                                            <asp:RadioButtonList ID="rbtndcatypep" CssClass="esrbtn" Style="font-size: small" ToolTip="DCA Type"
                                                                                RepeatDirection="Horizontal"   OnSelectedIndexChanged="rbtndcatypep_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                                <asp:ListItem Value="Expense">General DCA</asp:ListItem>
                                                                                <asp:ListItem Value="Tax">Tax DCA</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="treditdca" runat="server">
                                                                        <td style="width: 80px" align="center">
                                                                            <asp:Label ID="Label11" runat="server" Text="DCACode" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="left" style="width: 420px; height: 29px;">
                                                                            <asp:DropDownList ID="ddleditdca" runat="server" ToolTip="DCA Code"
                                                                                Width="150px" CssClass="esddown" AutoPostBack = "true" OnSelectedIndexChanged = "OnSelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                           <%-- <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID=""
                                                                                ServicePath="cascadingDCA.asmx" Category="dca" LoadingText="Please Wait" ServiceMethod="ISdc">
                                                                            </cc1:CascadingDropDown>--%>
                                                                            <br />
                                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                                                                TargetControlID="txteditdcaname" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                ServiceMethod="GetDCAName">
                                                                            </cc1:DynamicPopulateExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trtaxtype_p" runat="server">
                                                                        <td style="width: 200px" align="center">
                                                                            <asp:Label ID="Label19" runat="server" Text="Type of Tax" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="left" style="width: 420px; height: 29px;">
                                                                            <asp:DropDownList ID="ddltaxtypep" runat="server" ToolTip="Type of Tax" Width="50%"
                                                                                CssClass="esddown">
                                                                                <Items>
                                                                                    <asp:ListItem Text="Select" Value="Select" />
                                                                                    <asp:ListItem Text="Creditable" Value="Creditable" />
                                                                                    <asp:ListItem Text="Non-Creditable" Value="Non-Creditable" />
                                                                                </Items>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trnatureoftax_p" runat="server">
                                                                        <td style="width: 200px" align="center">
                                                                            <asp:Label ID="Label20" runat="server" Text="Nature Of Tax" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="left" style="width: 420px; height: 29px;">
                                                                            <asp:DropDownList ID="ddlnatureoftaxp" runat="server" ToolTip="Nature of Tax" Width="50%"
                                                                                CssClass="esddown">
                                                                                <Items>
                                                                                    <asp:ListItem Text="Select" Value="Select" />
                                                                                    <asp:ListItem Text="Basic Tax" Value="BasicTax" />
                                                                                    <asp:ListItem Text="Cess" Value="Cess" />
                                                                                </Items>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>

                                                                    <tr id="treditsudca" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="Label13" runat="server" Text="SubDCACode" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlsdcode" runat="server" Width="100px" ToolTip="Sub-Dca Code" onchange="SetDynamicKey('dp4',this.value);"
                                                                                CssClass="esddown">
                                                                            </asp:DropDownList>
                                                                           <%-- <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlsdcode"
                                                                                ServicePath="cascadingDCA.asmx" Category="sub" LoadingText="Please Wait" ServiceMethod="subDCA"
                                                                                ParentControlID="ddleditdca" PromptText="Select Sub DCA">
                                                                            </cc1:CascadingDropDown>--%>
                                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender4" BehaviorID="dp4" runat="server"
                                                                                TargetControlID="txtname" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                                ServiceMethod="GetSubDCAName">
                                                                            </cc1:DynamicPopulateExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trdcaname" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="lbldcaname" runat="server" Text="DCA Name" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td colspan="3" align="left">
                                                                            <asp:TextBox ID="txteditdcaname" runat="server" Width="200px" TextMode="MultiLine"
                                                                                ToolTip="DCA Name" CssClass="esddown" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="treditsubdcaname" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="Label14" runat="server" Text="SubDCA Name" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td colspan="3" align="left">
                                                                            <asp:TextBox ID="txtname" runat="server" Width="200px" TextMode="MultiLine" ToolTip="Name"
                                                                                CssClass="esddown" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trtaxnos_p" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="Label21" runat="server" Text="Tax No's" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td colspan="3" align="left">
                                                                            <asp:CheckBoxList ID="chktaxnosp" CssClass="esrbtn" Style="font-size: smaller" ToolTip="Tax Nos"
                                                                                RepeatDirection="Horizontal" RepeatColumns="3" runat="server" CellPadding="0"
                                                                                CellSpacing="0">
                                                                            </asp:CheckBoxList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tredittype" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="Label15" runat="server" Text="CC Type" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td colspan="4" align="left">
                                                                            <asp:CheckBoxList ID="chkedit" CssClass="esrbtn" Style="font-size: small" ToolTip="Sub Type"
                                                                                RepeatDirection="Horizontal" RepeatColumns="3" runat="server" CellPadding="0"
                                                                                CellSpacing="0">
                                                                                <asp:ListItem>Service</asp:ListItem>
                                                                                <asp:ListItem>Trading</asp:ListItem>
                                                                                <asp:ListItem>Manufacturing</asp:ListItem>
                                                                                <asp:ListItem>Non-Performing</asp:ListItem>
                                                                                <asp:ListItem>Capital</asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="treditpaymenttype" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="Label16" runat="server" Text="Payment Type" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td colspan="4" align="left">
                                                                            <asp:CheckBoxList ID="chkeditttype" CssClass="esrbtn" Style="font-size: small" ToolTip="Payment Type"
                                                                                RepeatDirection="Horizontal" RepeatColumns="2" runat="server" CellPadding="0"
                                                                                CellSpacing="0">
                                                                                <asp:ListItem>Direct payment</asp:ListItem>
                                                                                <asp:ListItem>Vendor through Payment</asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trbtnupdate" runat="server">
                                                                        <td align="center" colspan="4">
                                                                            <asp:Button ID="btnupdate" runat="server" Width="50px" Text="Update" CssClass="esbtn"
                                                                                OnClientClick="return updatevalidate();" OnClick="btnupdate_Click"  />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                                <td bgcolor="#FFFFFF">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script language="javascript">
        function Pageload() {
            var txtdca = document.getElementById("<%=dcacode.ClientID %>");
            var ddldca = document.getElementById("<%=trdca.ClientID %>");
            var sdca = document.getElementById("<%=sdca.ClientID %>");
            var sdname = document.getElementById("<%=sdname.ClientID %>");
            var dname = document.getElementById("<%=dcaname.ClientID %>");
            var it = document.getElementById("<%=it.ClientID %>");
            var btn = document.getElementById("<%=btn.ClientID %>");
            var SubType = document.getElementById("<%=SubType.ClientID %>");
            var paymenttype = document.getElementById("<%=trpaymenttype.ClientID %>");
            var trdcatype = document.getElementById("<%=trdcatype.ClientID %>");
            var trtaxtype = document.getElementById("<%=trtaxtype.ClientID %>");
            var trnatureoftax = document.getElementById("<%=trnatureoftax.ClientID %>");
            var trtaxnos = document.getElementById("<%=trtaxnos.ClientID %>");
            txtdca.style.display = 'none';
            sdca.style.display = 'none';
            sdname.style.display = 'none';
            dname.style.display = 'none';
            ddldca.style.display = 'none';
            it.style.display = 'none';
            btn.style.display = 'none';
            SubType.style.display = 'none';
            paymenttype.style.display = 'none';
            trdcatype.style.display = 'none';
            trtaxtype.style.display = 'none';
            trnatureoftax.style.display = 'none';
            trtaxnos.style.display = 'none';
        }
        Pageload();
    </script>
    <script language="javascript">
        function Popload() {
            //var editsubdca = document.getElementById("<%=treditsudca.ClientID %>");
            var type = document.getElementById("<%=tredittype.ClientID %>");
            var sdca = document.getElementById("<%=treditsudca.ClientID %>");
            var sdname = document.getElementById("<%=treditsubdcaname.ClientID %>");
            var dca = document.getElementById("<%=treditdca.ClientID %>");
            var editbtn = document.getElementById("<%=trbtnupdate.ClientID %>");
            var dcaname = document.getElementById("<%=trdcaname.ClientID %>");
            var edittype = document.getElementById("<%=treditpaymenttype.ClientID %>");
            var trdcatype_p = document.getElementById("<%=trdcatype_p.ClientID %>");
            var trtaxtype_p = document.getElementById("<%=trtaxtype_p.ClientID %>");
            var trnatureoftax_p = document.getElementById("<%=trnatureoftax_p.ClientID %>");
            var trtaxnos_p = document.getElementById("<%=trtaxnos_p.ClientID %>");
           // editsubdca.style.display = 'none';
            type.style.display = 'none';
            sdca.style.display = 'none';
            sdname.style.display = 'none';
            dca.style.display = 'none';
            editbtn.style.display = 'none';
            dcaname.style.display = 'none';
            edittype.style.display = 'none';
            trdcatype_p.style.display = 'none';
            trtaxtype_p.style.display = 'none';
            trnatureoftax_p.style.display = 'none';
            trtaxnos_p.style.display = 'none';
        }
        Popload();
    </script>
</asp:Content>
