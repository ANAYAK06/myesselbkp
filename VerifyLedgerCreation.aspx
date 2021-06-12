<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyLedgerCreation.aspx.cs" Inherits="VerifyLedgerCreation" %>

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
            var trGST = document.getElementById("<%=trGST.ClientID %>");
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 2) {
                trGST.style.display = 'none';
                invtype.style.display = 'block';
                paymenttype.style.display = 'block';
                btnsubmit.style.display = 'block';
                itcode.style.display = 'none';
                trbankname.style.display = 'none';
                trrbtnledgers.style.display = 'none';
                trtypeofinvoice.style.display = 'none';
                trtermloan.style.display = 'none';
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
                else if (subgroup.value == "") {
                    alert("Please Select Sub-Group");
                    subgroup.focus();
                    return false;
                }
                else if (ddlitcode.value == "") {
                    alert("Please Select IT Code");
                    ddlitcode.focus();
                    return false;
                }
                else if (date.value == "") {
                    alert("Please Insert Date");
                    date.focus();
                    return false;
                }
                else if (balance.value == "") {
                    alert("Please Insert Opening Balance");
                    itcode.focus();
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
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 3) {

                var objs = new Array("<%=ddlgstnos.ClientID %>", "<%=txtledgername.ClientID %>", "<%=ddlsubgroup.ClientID %>", "<%=ddlitcode.ClientID %>", "<%=txtbaldate.ClientID %>", "<%=txtopeningbal.ClientID %>");
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
                    trbankname.style.display = 'block';
                    trtermloan.style.display = 'none';
                    trtypeofinvoice.style.display = 'none';
                }
                if (SelectedIndex("<%=rbtnledgers.ClientID %>") == 2) {
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
                        <table>
                            <tr style="border: 1px solid #000">
                                <th valign="top" align="center" style="background: #D3D3D3">
                                    <asp:Label ID="lblheading" CssClass="esfmhead" runat="server" Text=""></asp:Label>
                                </th>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:GridView ID="gvledgers" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="False" BorderColor="white" CssClass="grid-content" DataKeyNames="id"
                                        GridLines="None" HeaderStyle-CssClass="grid-header" OnRowDeleting="gvledgers_RowDeleting"
                                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        ShowFooter="false" Width="680px" OnSelectedIndexChanged="gvledgers_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="id" Visible="false" />
                                            <asp:BoundField DataField="Ledger_Type" ItemStyle-HorizontalAlign="Center" HeaderText="LedgerType" />
                                            <asp:BoundField DataField="Name" ItemStyle-HorizontalAlign="Center" HeaderText="Ledger Name"
                                                ItemStyle-ForeColor="DarkBlue" />
                                            <asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}"
                                                HeaderText="Amount" />
                                            <asp:CommandField ButtonType="Image" ItemStyle-HorizontalAlign="Center" DeleteImageUrl="~/images/Delete.jpg"
                                                HeaderText="Reject" ItemStyle-Width="15px" ShowDeleteButton="true" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" id="tblledger" runat="server" cellspacing="0" width="700px">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="400px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="trtype" class="estbl" width="400px">
                                                    <tr align="center">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" onclick="Display();" Style="font-size: small"
                                                                ToolTip="Type" RepeatDirection="Horizontal" runat="server" CellPadding="0" CellSpacing="0">
                                                                <asp:ListItem Value="General">General</asp:ListItem>
                                                                <asp:ListItem Value="General Payable">General Payable</asp:ListItem>
                                                                <asp:ListItem Value="Client/Vendor Ledger">Client/Vendor Ledger</asp:ListItem>
                                                                <asp:ListItem Value="Tax Ledger">Tax Ledger</asp:ListItem>
                                                                <asp:ListItem Value="Other Ledgers">Other Ledgers</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trinvoicetype" align="center" runat="server" style="display: none">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtninvoicetype" onclick="Display();" CssClass="esrbtn"
                                                                Style="font-size: small" ToolTip="Invoice Type" RepeatDirection="Horizontal"
                                                                runat="server" CellPadding="0" CellSpacing="0">
                                                                <asp:ListItem Value="Creditor">Creditor</asp:ListItem>
                                                                <asp:ListItem Value="Debitor">Debitor</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trotherledger" align="center" runat="server" style="display: none">
                                                        <td colspan="2" align="center">
                                                            <asp:RadioButtonList ID="rbtnledgers" onclick="Display();" CssClass="esrbtn" Style="font-size: small"
                                                                ToolTip="Other Ledger Type" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                                CellSpacing="0">
                                                                <asp:ListItem Value="Cash Ledger">Cash Ledger</asp:ListItem>
                                                                <asp:ListItem Value="Bank Ledger">Bank Ledger</asp:ListItem>
                                                                <asp:ListItem Value="Term Loan Ledger">Term Loan Ledger</asp:ListItem>
                                                                <asp:ListItem Value="Invoice Ledger">Invoice Ledger</asp:ListItem>
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
                                                            <cc1:CascadingDropDown ID="CascadingDropDown6" runat="server" Category="GST" TargetControlID="ddlgstnos"
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
                                                    <tr id="tr" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Sub-Groups"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlsubgroup" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Sub-Groups">
                                                            </asp:DropDownList>
                                                        <%--    <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="group" TargetControlID="ddlsubgroup"
                                                                ServiceMethod="AllGroups" ServicePath="cascadingDCA.asmx" PromptText="Select Sub-Groups">
                                                            </cc1:CascadingDropDown>--%>
                                                        </td>
                                                    </tr>
                                                    <tr id="tritcode" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="IT Code"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlitcode" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="IT Code">
                                                            </asp:DropDownList>
                                                          <%--  <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="group" TargetControlID="ddlitcode"
                                                                ServiceMethod="itcodename" ServicePath="cascadingDCA.asmx" PromptText="Select IT Code">
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
                                                            <asp:Label ID="lblbankid" runat="server"></asp:Label>
                                                            <asp:Label ID="lblbankname" CssClass="esddown" Width="200px" runat="server" Text=""></asp:Label>
                                                            <%-- <asp:DropDownList ID="ddlbankname" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Bank Name">
                                                            </asp:DropDownList>--%>
                                                        </td>
                                                    </tr>
                                                    <tr id="trtermloan" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Term Loan"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:Label ID="lbltermloan" CssClass="esddown" Width="200px" runat="server" Text=""></asp:Label>
                                                            <%--  <asp:DropDownList ID="ddltermloan" CssClass="esddown" Width="200px" runat="server"
                                                                ToolTip="Term Loan">
                                                            </asp:DropDownList>--%>
                                                        </td>
                                                    </tr>
                                                    <tr id="trtypeofinvoice" runat="server" style="display: none">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="Type of Invoice"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddltype" runat="server" ToolTip="Type of Invoice" Width="200px"
                                                                CssClass="esddown">
                                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                <asp:ListItem Value="Service Tax Invoice">Service Tax Invoice</asp:ListItem>
                                                                <asp:ListItem Value="SEZ/Service Tax exumpted Invoice">SEZ/Service Tax exumpted Invoice</asp:ListItem>
                                                                <asp:ListItem Value="VAT/Material Supply">VAT/Material Supply</asp:ListItem>
                                                                <asp:ListItem Value="Trading Supply">Trading Supply</asp:ListItem>
                                                                <asp:ListItem Value="Manufacturing">Manufacturing</asp:ListItem>
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
                                                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
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
                                                    Style="font-size: small" Text="" OnClick="btnsubmit_Click" />
                                                <asp:Button ID="btnclose" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Close" OnClick="btnclose_Click" />
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
