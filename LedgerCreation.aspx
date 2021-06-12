<%@ Page Title="Ledger Creation" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="LedgerCreation.aspx.cs" Inherits="LedgerCreation" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Display() {

            var invtype = document.getElementById("<%=trinvoicetype.ClientID %>");
            var paymenttype = document.getElementById("<%=trpaymenttype.ClientID %>");
            var btnsubmit = document.getElementById("<%=tblbtnupdate.ClientID %>");
            var ventype = document.getElementById("<%=trventype.ClientID %>");
            var venname = document.getElementById("<%=trvendorname.ClientID %>");
            var clientname = document.getElementById("<%=trclient.ClientID %>");
            var itcode = document.getElementById("<%=tritcode.ClientID %>");
            var trbankname = document.getElementById("<%=trbankname.ClientID %>");
            var trrbtnledgers = document.getElementById("<%=trotherledger.ClientID %>");
            var trtermloan = document.getElementById("<%=trtermloan.ClientID %>");
            var trtypeofinvoice = document.getElementById("<%=trtypeofinvoice.ClientID %>");
            var ddlsubgroup = document.getElementById("<%=ddlsubgroup.ClientID %>");
            var trGST = document.getElementById("<%=trGST.ClientID %>");
            var ITCodes = document.getElementById("<%=ddlitcode.ClientID %>");
            var select1 = document.getElementById("<%=ddlsubgroup.ClientID %>");
            for (var i = 0; i < select1.length; i++) {
                var option = select1.options[i];

                document.getElementById("<%=ddlsubgroup.ClientID %>").options[i].disabled = false;

            }

            if (SelectedIndex("<%=rbtntype.ClientID %>") == 2) {
                document.getElementById("<%=ddlsubgroup.ClientID %>").selectedIndex = 0;

                trGST.style.display = 'none';
                invtype.style.display = 'block';
                paymenttype.style.display = 'block';
                btnsubmit.style.display = 'block';
                itcode.style.display = 'none';
                trbankname.style.display = 'none';
                trrbtnledgers.style.display = 'none';
                trtermloan.style.display = 'none';
                trtypeofinvoice.style.display = 'none';
                if (SelectedIndex("<%=rbtninvoicetype.ClientID %>") == 0) {
                    ventype.style.display = 'block';
                    venname.style.display = 'block';
                    clientname.style.display = 'none';
                    itcode.style.display = 'none';
                }
                if (SelectedIndex("<%=rbtninvoicetype.ClientID %>") == 1) {
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'block';
                    itcode.style.display = 'none';
                }
            }
            else if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                document.getElementById("<%=ddlsubgroup.ClientID %>").selectedIndex = 0;

                trGST.style.display = 'none';
                invtype.style.display = 'none';
                paymenttype.style.display = 'block';
                btnsubmit.style.display = 'block';
                itcode.style.display = 'block';
                ventype.style.display = 'none';
                venname.style.display = 'none';
                clientname.style.display = 'none';
                trbankname.style.display = 'none';
                trrbtnledgers.style.display = 'none';
                trtermloan.style.display = 'none';
                trtypeofinvoice.style.display = 'none';
            }
            else if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                var ITCodes = document.getElementById("<%=ddlitcode.ClientID %>");
                for (var i = 0; i < ITCodes.length; i++) {
                    var option = ITCodes.options[i];

                    document.getElementById("<%=ddlitcode.ClientID %>").options[i].disabled = false;

                }
                document.getElementById("<%=ddlsubgroup.ClientID %>").selectedIndex = 0;
                trGST.style.display = 'none';
                invtype.style.display = 'none';
                paymenttype.style.display = 'block';
                btnsubmit.style.display = 'block';
                itcode.style.display = 'block';
                ventype.style.display = 'none';
                venname.style.display = 'none';
                clientname.style.display = 'none';
                trbankname.style.display = 'none';
                trrbtnledgers.style.display = 'none';
                trtermloan.style.display = 'none';
                trtypeofinvoice.style.display = 'none';
            }
            else if (SelectedIndex("<%=rbtntype.ClientID %>") == 3) {

                var select = document.getElementById("<%=ddlsubgroup.ClientID %>");

                for (var i = 0; i < select.length; i++) {
                    var option = select.options[i];

                    if (option.text == "OTHER LIABILITIES") {
                        document.getElementById("<%=ddlsubgroup.ClientID %>").value = option.value;
                        document.getElementById("<%=ddlsubgroup.ClientID %>").options[i].disabled = false;

                    }
                    else {
                        document.getElementById("<%=ddlsubgroup.ClientID %>").options[i].disabled = true;
                    }
                }
                var ITCodes = document.getElementById("<%=ddlitcode.ClientID %>");
                for (var i = 0; i < ITCodes.length; i++) {
                    var option = ITCodes.options[i];

                    if (option.value == "SGST" || option.value == "IGST" || option.value == "CGST") {

                        // document.getElementById("<%=ddlsubgroup.ClientID %>").disabled = true;
                    }
                    else {
                        document.getElementById("<%=ddlitcode.ClientID %>").options[i].disabled = true;
                    }
                }

                trGST.style.display = 'block';
                invtype.style.display = 'none';
                paymenttype.style.display = 'block';
                btnsubmit.style.display = 'block';
                itcode.style.display = 'block';
                ventype.style.display = 'none';
                venname.style.display = 'none';
                clientname.style.display = 'none';
                trbankname.style.display = 'none';
                trrbtnledgers.style.display = 'none';
                trtermloan.style.display = 'none';
                trtypeofinvoice.style.display = 'none';
            }
            else if (SelectedIndex("<%=rbtntype.ClientID %>") == 4) {
                document.getElementById("<%=ddlsubgroup.ClientID %>").selectedIndex = 0;

                trGST.style.display = 'none';
                invtype.style.display = 'none';
                trrbtnledgers.style.display = 'block';
                btnsubmit.style.display = 'block';
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 0) {
                    invtype.style.display = 'none';
                    paymenttype.style.display = 'block';
                    btnsubmit.style.display = 'block';
                    itcode.style.display = 'none';
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'none';
                    trbankname.style.display = 'none';
                    trtermloan.style.display = 'none';
                    trtypeofinvoice.style.display = 'none';
                }
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 1) {
                    invtype.style.display = 'none';
                    paymenttype.style.display = 'block';
                    btnsubmit.style.display = 'block';
                    itcode.style.display = 'none';
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'none';
                    trbankname.style.display = 'block';
                    trtermloan.style.display = 'none';
                    trtypeofinvoice.style.display = 'none';
                }
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 2) {
                    invtype.style.display = 'none';
                    paymenttype.style.display = 'block';
                    btnsubmit.style.display = 'block';
                    itcode.style.display = 'none';
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'none';
                    trbankname.style.display = 'none';
                    trtermloan.style.display = 'block';
                    trtypeofinvoice.style.display = 'none';
                }
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 3) {
                    invtype.style.display = 'none';
                    paymenttype.style.display = 'block';
                    btnsubmit.style.display = 'block';
                    itcode.style.display = 'none';
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'none';
                    trbankname.style.display = 'none';
                    trtermloan.style.display = 'none';
                    trtypeofinvoice.style.display = 'block';
                }
            }
            else {
                trGST.style.display = 'none';
                invtype.style.display = 'none';
                paymenttype.style.display = 'none';
                btnsubmit.style.display = 'none';
                itcode.style.display = 'none';
                ventype.style.display = 'none';
                venname.style.display = 'none';
                clientname.style.display = 'none';
                trbankname.style.display = 'none';
                trrbtnledgers.style.display = 'none';
                clientname.style.display = 'none';
                trbankname.style.display = 'none';
                trtermloan.style.display = 'none';
                trtypeofinvoice.style.display = 'none';
            }
        }

        function isNumber(evt) {
            myFunction();
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function myTrim(x) {
            return x.replace(/^\s+|\s+$/gm, '');
        }

        function myFunction() {
            var txtbox = document.getElementById("<%=txtopeningbal.ClientID %>");
            var str = myTrim(txtbox.value);
        }
       
         
    </script>
    <script type="text/javascript" language="javascript">
        String.prototype.startsWith = function (str) {
            return (this.indexOf(str) === 0);
        }
        function ChkValidChar() {
            var txtbx = document.getElementById("<%=txtopeningbal.ClientID %>").value;
            if (txtbx.startsWith(".") || txtbx.startsWith("0")) // true
            {
                document.getElementById("<%=txtopeningbal.ClientID %>").value = "";
                alert("You can not insert dot and zero as first character");
            }
        }
    </script>
    <script type="text/javascript">
        function validate() {
            //debugger;
            var invtype = document.getElementById("<%=trinvoicetype.ClientID %>");
            var paymenttype = document.getElementById("<%=trpaymenttype.ClientID %>");
            var btnsubmit = document.getElementById("<%=tblbtnupdate.ClientID %>");
            var ventype = document.getElementById("<%=trventype.ClientID %>");
            var venname = document.getElementById("<%=trvendorname.ClientID %>");
            var clientname = document.getElementById("<%=trclient.ClientID %>");
            var itcode = document.getElementById("<%=tritcode.ClientID %>");
            var ledgername = document.getElementById("<%=txtledgername.ClientID %>");
            var subgroup = document.getElementById("<%=ddlsubgroup.ClientID %>");
            var ddlitcode = document.getElementById("<%=ddlitcode.ClientID %>");
            var date = document.getElementById("<%=txtbaldate.ClientID %>");
            var balance = document.getElementById("<%=txtopeningbal.ClientID %>");
            var trbankname = document.getElementById("<%=trbankname.ClientID %>");
            var trtermloan = document.getElementById("<%=trtermloan.ClientID %>");
            var trtypeofinvoice = document.getElementById("<%=trtypeofinvoice.ClientID %>");
            if ((SelectedIndex("<%=rbtntype.ClientID %>") == 0) || (SelectedIndex("<%=rbtntype.ClientID %>") == 1)) {
                if (ledgername.value == "") {
                    alert("Ledger Name Required");
                    ledgername.focus();
                    return false;
                }
                
                else if (ddlitcode.value == "Select IT Code") {
                    alert("Please Select IT Code");
                    ddlitcode.focus();
                    return false;
                }
                else if (subgroup.value == "Select Sub-Groups") {
                    alert("Please Select Sub-Group");
                    subgroup.focus();
                    return false;
                }
                else if (date.value == "") {
                    alert("Please Insert Date");
                    date.focus();
                    return false;
                }
                else if (balance.value == "") {
                    alert("Please Insert Opening Balance");
                    balance.focus();
                    return false;
                }
                else if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>"))
                    return false;
                invtype.style.display = 'none';
                paymenttype.style.display = 'block';
                btnsubmit.style.display = 'block';
                itcode.style.display = 'block';
                ventype.style.display = 'none';
                venname.style.display = 'none';
                clientname.style.display = 'none';
            }
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 2) {
                if (!ChceckRBL("<%=rbtninvoicetype.ClientID %>"))
                    return false;
                if (SelectedIndex("<%=rbtninvoicetype.ClientID %>") == 0) {
                    var objs = new Array("<%=txtledgername.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=ddlvendortype.ClientID %>", "<%=ddlvendorname.ClientID %>", "<%=txtbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                    if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>"))
                        return false;
                    ventype.style.display = 'block';
                    venname.style.display = 'block';
                    clientname.style.display = 'none';
                    itcode.style.display = 'none';
                }
            }
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 3) {

                var objs = new Array("<%=ddlgstnos.ClientID %>", "<%=txtledgername.ClientID %>", "<%=ddlitcode.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=txtbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>"))
                    return false;
                invtype.style.display = 'none';
                paymenttype.style.display = 'block';
                btnsubmit.style.display = 'block';
                itcode.style.display = 'block';
                ventype.style.display = 'none';
                venname.style.display = 'none';
                clientname.style.display = 'none';
            }
            if (SelectedIndex("<%=rbtninvoicetype.ClientID %>") == 1) {
                var objs = new Array("<%=txtledgername.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=ddlclient.ClientID %>", "<%=txtbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>"))
                    return false;
                ventype.style.display = 'none';
                venname.style.display = 'none';
                clientname.style.display = 'block';
                itcode.style.display = 'none';
            }
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 4) {
                if (!ChceckRBL("<%=rbtnledgers.ClientID %>"))
                    return false;
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 0) {
                    var objs = new Array("<%=txtledgername.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=txtbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                    if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>"))
                        return false;
                    invtype.style.display = 'none';
                    paymenttype.style.display = 'block';
                    btnsubmit.style.display = 'block';
                    itcode.style.display = 'none';
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'none';
                    trbankname.style.display = 'none';
                    trtermloan.style.display = 'none';
                }
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 1) {
                    var objs = new Array("<%=txtledgername.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=ddlbankname.ClientID %>", "<%=txtbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                    if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>"))
                        return false;
                    invtype.style.display = 'none';
                    paymenttype.style.display = 'block';
                    btnsubmit.style.display = 'block';
                    itcode.style.display = 'none';
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'none';
                    trbankname.style.display = 'block';
                    trtermloan.style.display = 'none';
                    trtypeofinvoice.style.display = 'none';
                }
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 2) {
                    var objs = new Array("<%=txtledgername.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=ddltermloan.ClientID %>", "<%=txtbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                    if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>"))
                        return false;
                    invtype.style.display = 'none';
                    paymenttype.style.display = 'block';
                    btnsubmit.style.display = 'block';
                    itcode.style.display = 'none';
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'none';
                    trbankname.style.display = 'none';
                    trtermloan.style.display = 'block';
                    trtypeofinvoice.style.display = 'none';
                }
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 3) {
                    var objs = new Array("<%=txtledgername.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=ddltype.ClientID %>", "<%=txtbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                    if (!ChceckRBL("<%=rbtnpaymenttype.ClientID %>"))
                        return false;
                    invtype.style.display = 'none';
                    paymenttype.style.display = 'block';
                    btnsubmit.style.display = 'block';
                    itcode.style.display = 'none';
                    ventype.style.display = 'none';
                    venname.style.display = 'none';
                    clientname.style.display = 'none';
                    trbankname.style.display = 'none';
                    trtermloan.style.display = 'none';
                    trtypeofinvoice.style.display = 'block';
                }
            }
        }
    </script>
    <script type="text/javascript">
        function capitalize(textboxid, str) {
            // string with alteast one character
            if (str && str.length >= 1) {
                var firstChar = str.charAt(0);
                var remainingStr = str.slice(1);
                str = firstChar.toUpperCase() + remainingStr;
            }
            document.getElementById(textboxid).value = str;
        }
    </script>
    <script type="text/javascript">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="700px">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="600px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text="Ledger Creation"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="trtype" class="estbl" width="600px">
                                                    <tr align="center">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" onclick="Display();" Style="font-size: small"
                                                                ToolTip="Type" RepeatDirection="Horizontal" runat="server" CellPadding="0" CellSpacing="0">
                                                                <asp:ListItem Value="0">General</asp:ListItem>
                                                                <asp:ListItem Value="1">General Payable</asp:ListItem>
                                                                <asp:ListItem Value="2">Client/Vendor Ledger</asp:ListItem>
                                                                <asp:ListItem Value="3">Tax Ledger</asp:ListItem>
                                                                <asp:ListItem Value="4">Other Ledgers</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trinvoicetype" align="center" runat="server" style="display: none">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtninvoicetype" onclick="Display();" CssClass="esrbtn"
                                                                Style="font-size: small" ToolTip="Invoice Type" RepeatDirection="Horizontal"
                                                                runat="server" CellPadding="0" CellSpacing="0">
                                                                <asp:ListItem Value="0">Creditor</asp:ListItem>
                                                                <asp:ListItem Value="1">Debitor</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trotherledger" align="center" runat="server" style="display: none">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtnledgers" onclick="Display();" CssClass="esrbtn" Style="font-size: small"
                                                                ToolTip="Other Ledger Type" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                                CellSpacing="0">
                                                                <asp:ListItem Value="0">Cash Ledger</asp:ListItem>
                                                                <asp:ListItem Value="1">Bank Ledger</asp:ListItem>
                                                                <asp:ListItem Value="2">Term Loan Ledger</asp:ListItem>
                                                                <asp:ListItem Value="3">Invoice Ledger</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trGST" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="GST Nos"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlgstnos" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="GST Nos">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="GST" TargetControlID="ddlgstnos"
                                                                ServiceMethod="GetGSTNos" ServicePath="cascadingDCA.asmx" PromptText="Select GST No">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr1" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Ledger Name"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:TextBox ID="txtledgername" CssClass="estbox" runat="server" onkeyup="javascript:capitalize(this.id, this.value);"
                                                                Width="200px" ToolTip="Ledger Name" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="tritcode" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="IT Code"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlitcode" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="IT Code" OnSelectedIndexChanged="ddlitcode_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <%--<cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="group" TargetControlID="ddlitcode"
                                                                ServiceMethod="itcodename" ServicePath="cascadingDCA.asmx" PromptText="Select IT Code">
                                                            </cc1:CascadingDropDown>--%>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Sub-Groups"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlsubgroup" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Sub-Groups">
                                                            </asp:DropDownList>
                                                            <%--   <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="group" TargetControlID="ddlsubgroup"
                                                                ServiceMethod="AllGroups" ServicePath="cascadingDCA.asmx" PromptText="Select Sub-Groups">
                                                            </cc1:CascadingDropDown>--%>
                                                        </td>
                                                    </tr>
                                                    <tr id="trventype" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Vendor Type"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlvendortype" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Vendor Type">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="vendor_type"
                                                                TargetControlID="ddlvendortype" ServiceMethod="vendortype" ServicePath="cascadingDCA.asmx"
                                                                PromptText="Select Vendor Type">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                    <tr id="trvendorname" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label6" CssClass="eslbl" runat="server" Text="Vendor Name"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlvendorname" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Vendor Name">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" Category="group" TargetControlID="ddlvendorname"
                                                                ServiceMethod="vendornames" UseContextKey="true" ParentControlID="ddlvendortype"
                                                                ServicePath="cascadingDCA.asmx" PromptText="Select Vendor Name">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                    <tr id="trclient" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label7" CssClass="eslbl" runat="server" Text="Client Name"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlclient" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Client Name">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown5" runat="server" Category="group" TargetControlID="ddlclient"
                                                                ServiceMethod="clientnames" UseContextKey="true" ServicePath="cascadingDCA.asmx"
                                                                PromptText="Select Client Name">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                    <tr id="trbankname" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="Bank Name"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlbankname" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Bank Name">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trtermloan" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Term Loan"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddltermloan" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Term Loan">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trtypeofinvoice" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="Type of Invoice"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddltype" runat="server" ToolTip="Type of Invoice" Width="200px"
                                                                CssClass="esddown">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Service Tax Invoice</asp:ListItem>
                                                                <asp:ListItem>SEZ/Service Tax exumpted Invoice</asp:ListItem>
                                                                <asp:ListItem>VAT/Material Supply</asp:ListItem>
                                                                <asp:ListItem>Trading Supply</asp:ListItem>
                                                                <asp:ListItem>Manufacturing</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="Balance As On "></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:TextBox ID="txtbaldate" CssClass="estbox" Width="200px" ToolTip="Balance As On"
                                                                onKeyDown="preventBackspace();" onpaste="return false;" onkeypress="return false;"
                                                                runat="server"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtbaldate"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                PopupButtonID="txtdate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr2" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="Opening Balance"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:TextBox ID="txtopeningbal" CssClass="estbox" onkeypress="return isNumber(event)"
                                                                runat="server" Width="200px" ToolTip="Opening Balance" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trpaymenttype" align="center" runat="server" style="display: none">
                                                        <td colspan="2" align="center">
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
                                    </table>
                                    <table id="tblbtnupdate" runat="server" width="400px" style="display: none">
                                        <tr align="center">
                                            <td align="center">
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="esbtn" OnClientClick="javascript:return validate();"
                                                    Style="font-size: small" Text="Submit" OnClick="btnsubmit_Click" />
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
    </table>
</asp:Content>
