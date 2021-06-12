<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="CustomerHoldandRetention.aspx.cs"
    Inherits="CustomerHoldandRetention" Title="Customer Hold and Retention" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Retentionvalidation() {
            //debugger;
            var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length; rowCount++) {
                if (GridView2.rows(rowCount).cells(0).children(0) != null) {
                    if (GridView2.rows(rowCount).cells(0).children(0).checked == true) {
                        var value = GridView2.rows(rowCount).cells(4).innerText.replace(/,/g, "");
                        if (value != "") {
                            originalValue += parseFloat(value);
                        }
                    }

                }
            }
            document.getElementById("<%=txtamt.ClientID %>").value = originalValue;
            document.getElementById("<%=hfamt.ClientID %>").value = originalValue;
        }

        function numericValidation(txtvalue) {
            //debugger;
            var e = event || evt;
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
    <script language="javascript" type="text/javascript">

        function SelectAllCheckboxes(spanChk) {
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
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddltypeofpay.ClientID %>", "<%=ddlclientid.ClientID %>", "<%=ddlsubclientid.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlpo.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
      
    </script>
    <script type="text/javascript">

        function validation() {
            debugger;            
            var objs = new Array("<%=ddlbankname.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            if (GridView != null) {
                var isValid = false;
                var j = 0;
                for (var i = 1; i < GridView.rows.length; i++) {
                    var inputs = GridView.rows[i].getElementsByTagName('input');

                    if (inputs != null) {
                        if (inputs[0].type == "checkbox") {
                            if (inputs[0].checked) {
                                isValid = true;
                                j = j + 1;

                            }
                        }
                    }
                }
                if (parseInt(j) == 0) {
                    alert("Please select atleast one checkbox");
                    return false;
                }
            }
            var ActuallAmt = document.getElementById("<%=txtamt.ClientID %>").value;
            var hfamt = document.getElementById("<%=hfamt.ClientID %>").value
            if (parseFloat(ActuallAmt) <= 0) {
                alert("Final Amount Can not in Negative Value");
                return false;
            }
            if (parseFloat(ActuallAmt) > parseFloat(hfamt)) {
                alert("Final Amount Can not more than selected invoice Amount");
                return false;
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
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
                        <table class="estbl" width="660px">
                            <tr align="center">
                                <th colspan="4">
                                    Customer Hold and Retention
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    Category of Payment:
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddltypeofpay" runat="server" ToolTip="Type of Payment" CssClass="esddown"
                                        Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddltypeofpay_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Retention</asp:ListItem>
                                        <asp:ListItem Value="2">Hold</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Client ID:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlclientid" runat="server" AutoPostBack="true" ToolTip="ClientID"
                                        Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Subclient ID:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsubclientid" AutoPostBack="true" runat="server" ToolTip="SubClientID"
                                        Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="CC Code:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" AutoPostBack="true"
                                        Width="180px" CssClass="esddown" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="PO NO:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlpo" runat="server" ToolTip="PO No" AutoPostBack="true" Width="110px"
                                        CssClass="esddown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnview" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="View" OnClientClick="javascript:return validate();" OnClick="btnview_Click" />
                                </td>
                            </tr>
                        </table>
                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td class="grid-content">
                                        <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                DataKeyNames="InvoiceNo" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                RowStyle-CssClass=" grid-row char grid-row-odd" EmptyDataText="There is no records">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="Retentionvalidation();" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="PO_NO" HeaderText="PO NO" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Invoice_Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                            </asp:GridView>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class="estbl" width="100%" id="paymentdetails" runat="server">
                            <tr align="center">
                                <th align="center" colspan="4">
                                    Payment Details
                                </th>
                            </tr>
                            <tr id="bank" runat="server">
                                <td>
                                    <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="Bank:"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlbankname" runat="server" ToolTip="Bank Name" CssClass="esddown"
                                        AutoPostBack="true" Width="200px">
                                    </asp:DropDownList>
                                    <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlbankname"
                                        ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                        PromptText="Select">
                                    </cc1:CascadingDropDown>
                                </td>
                            </tr>
                            <tr id="ModeofPay" runat="server">
                                <td>
                                    Mode Of Pay:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlpayment" runat="server" ToolTip="Mode Of Pay" CssClass="esddown"
                                        Width="70">
                                    </asp:DropDownList>
                                    <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                        ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                        PromptText="Select">
                                    </cc1:CascadingDropDown>
                                    <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtdate" CssClass="estbox" onKeyDown="preventBackspace();" runat="server" ToolTip="Date" onpaste="return false;"
                                        onkeypress="return false;" Width="80px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                        PopupButtonID="txtdate" OnClientDateSelectionChanged="checkDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Text="No:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                        class="starSpan">*</span>
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
                                    <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" onkeypress="return numericValidation(this);"
                                        Width="200px"></asp:TextBox><span class="starSpan">*</span>
                                    <asp:HiddenField ID="hfamt" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validation();" OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnreset" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Reset" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
