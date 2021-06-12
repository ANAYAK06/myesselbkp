<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Termloanpayment.aspx.cs"
    Inherits="Termloanpayment" Title="Term Loan Payment" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function validate1() {
            var objs = new Array("<%=ddlvendor.ClientID %>", "<%=ddlpo.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>
    <script language="javascript">
        function print() {

            var grid_obj = document.getElementById("<%=printformat.ClientID %>");

            if (grid_obj != null) {
                var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
                new_window.document.write(grid_obj.outerHTML);
                new_window.print();

            }
        }
    </script>
    <script language="javascript">
        function InterestCal() {
            if (parseInt(document.getElementById("<%=txtdprincple.ClientID %>").value) > parseInt(document.getElementById("<%=lblbalance.ClientID %>").innerHTML)) {
                alert("Insufficient Balance");
                document.getElementById("<%=txtdprincple.ClientID %>").value = "";
                document.getElementById('<%= txtcramount.ClientID%>').value = "";
            }
            else {
                var basic = document.getElementById("<%=txtdprincple.ClientID %>").value;
                var tax = document.getElementById("<%=txtdinterestamount.ClientID %>").value;
                var originalValue = 0;
                if (basic == "") {
                    basic = 0;
                }
                if (tax == "") {
                    tax = 0;
                }
                originalValue = eval((parseFloat(basic) + parseFloat(tax)));
                var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById('<%= txtcramount.ClientID%>').value = roundValue;
            }
        }
    
    </script>
    <script language="javascript">
        function validate() {
            var objs = new Array("<%=ddlcrloanno.ClientID %>", "<%=txtcragencycode.ClientID %>", "<%=txtdprincple.ClientID %>", "<%=txtdinterestamount.ClientID %>", "<%=ddldnoofinst.ClientID %>", "<%=txtinstdesc.ClientID %>", "<%=ddlvendor.ClientID %>", "<%=ddlpo.ClientID %>", "<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=ddlcheque.ClientID %>", "<%=txtcrloanpurpose.ClientID %>", "<%=txtcramount.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var GridView = document.getElementById("<%=grd.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {

                    if (GridView.rows(rowCount).cells(9).children(0).checked == true) {
                        var str1 = GridView.rows(rowCount).cells(10).children[0].value;

                        var str2 = document.getElementById("<%=txtdate.ClientID %>").value;
                        var str3 = document.getElementById("<%=hfdate.ClientID %>").value;
                        var str4 = document.getElementById("<%=hfinstdate.ClientID %>").value;
                        var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");

                        var dt1 = str1.substring(0, 2);
                        var dt2 = str2.substring(0, 2);
                        var dt3 = str3.substring(0, 2);
                        var dt4 = str4.substring(0, 2);
                        var yr1 = str1.substring(7, 11);
                        var yr2 = str2.substring(7, 11);
                        var yr3 = str3.substring(7, 11);
                        var yr4 = str4.substring(7, 11);
                        for (var i = 0; i < args.length; i++) {
                            var month = str2.substring(3, 6);
                            var month1 = str1.substring(3, 6);
                            var month2 = str3.substring(3, 6);
                            var month4 = str4.substring(3, 6);
                            if (args[i] == month) {
                                var month = parseInt(i + 1);
                                var date2 = yr2 + "-" + month + "-" + dt2;

                            }
                            if (args[i] == month1) {
                                var month1 = parseInt(i + 1);
                                var date1 = yr1 + "-" + month1 + "-" + dt1;
                            }
                            if (args[i] == month2) {
                                var month2 = parseInt(i + 1);
                                var date5 = yr3 + "-" + month2 + "-" + dt3;
                            }
                            if (args[i] == month4) {
                                var month4 = parseInt(i + 1);
                                var date7 = yr4 + "-" + month4 + "-" + dt4;
                            }

                        }
                        var one_day = 1000 * 60 * 60 * 24;
                        var x = date1.split("-");
                        var y = date2.split("-");
                        var Z = date5.split("-");
                        var xx = date7.split("-");
                        var date4 = new Date(x[0], (x[1] - 1), x[2]);
                        var date3 = new Date(y[0], (y[1] - 1), y[2]);
                        var date6 = new Date(Z[0], (Z[1] - 1), Z[2]);
                        var date8 = new Date(xx[0], (xx[1] - 1), xx[2]);

                        var month1 = x[1] - 1;
                        var month2 = y[1] - 1;
                        var month3 = Z[1] - 1;
                        var month5 = xx[1] - 1;
                        _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
                        _Diff1 = Math.ceil((date3.getTime() - date6.getTime()) / (one_day));
                        _Diff2 = Math.ceil((date8.getTime() - date3.getTime()) / (one_day));
                        if (parseInt(_Diff) < 0) {
                            alert("You are not able to put before apply date");
                            document.getElementById("<%=txtdate.ClientID %>").focus();
                            return false;
                        }
                        if (parseInt(_Diff1) < 0) {
                            alert("You are not able to make payment before approval date");
                            document.getElementById("<%=txtdate.ClientID %>").focus();
                            return false;
                        }
                        if (parseInt(_Diff2) < 0) {
                            alert("You are not able to make payment before Installment date");
                            document.getElementById("<%=txtdate.ClientID %>").focus();
                            return false;
                        }
                    }
                }
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
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
                        <table class="estbl" width="600px">
                            <tr>
                                <th align="center">
                                    Term Loan Payment
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <table class="innertab" align="center">
                                        <tr>
                                            <td>
                                                Type:
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Transction Type"
                                                    AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                    CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                                    <%--<asp:ListItem>Credit</asp:ListItem>--%>
                                                    <asp:ListItem>Debit</asp:ListItem>
                                                    <asp:ListItem>Third Party</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="estbl" runat="server" width="100%">
                                        <tr id="agency" runat="server">
                                            <td style="width: 100px">
                                                <asp:Label ID="lblagencycode" CssClass="eslbl" runat="server" Text="Loan No"></asp:Label>
                                                <asp:HiddenField ID="hfdate" runat="server" />
                                                <asp:HiddenField ID="hfinstdate" runat="server" />
                                            </td>
                                            <td style="width: 500px" colspan="3">
                                                <asp:DropDownList ID="ddlcrloanno" Width="150px" ToolTip="Loan No" runat="server"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlcrloanno_SelectedIndexChanged"
                                                    onchange="SetDynamicKey('dip',this.value);">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblcragencyname" class="ajaxspan" runat="server"></asp:Label>
                                                <%-- <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dip" runat="server"
                                                    TargetControlID="lblcragencyname" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                    ServiceMethod="GetAgencyNameusingloanno">
                                                </cc1:DynamicPopulateExtender>--%>
                                            </td>
                                        </tr>
                                        <tr id="loan" runat="server">
                                            <td>
                                                <asp:Label ID="lblcrloanno" CssClass="eslbl" runat="server" Text="Agency Code"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtcragencycode" CssClass="estbox" Enabled="false" ToolTip="Agency Code"
                                                    Width="75%" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Principle" runat="server">
                                            <td>
                                                <asp:Label ID="Label6" CssClass="eslbl" runat="server" Text="Principle"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtdprincple" CssClass="estbox" ToolTip="Principal Amount" Width="75%"
                                                    runat="server" onkeyup="InterestCal(this.value);" MaxLength="50"></asp:TextBox>
                                                <asp:Label ID="lblbalance" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="Interest" runat="server">
                                            <td>
                                                <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="Interest"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdinterestamount" onkeyup="InterestCal(this.value);" CssClass="estbox"
                                                    ToolTip="Interest Amount" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="No of Installments"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldnoofinst" Width="140px" runat="server" ToolTip="Installment No">
                                                </asp:DropDownList>
                                            </td>
                                            <%----%>
                                        </tr>
                                        <tr id="Installmentno" runat="server">
                                            <td>
                                                <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="Interest Description"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtinstdesc" TextMode="MultiLine" CssClass="estbox" ToolTip="Interest Description"
                                                    Width="75%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="vednor" runat="server">
                                            <td>
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Vendor Type"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlvendor" CssClass="esddown" Width="250px" ToolTip="Vendor"
                                                    runat="server" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlpo" runat="server" ToolTip="Po" Width="105px" CssClass="esddown">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="Button1" runat="server" CssClass="" Text="go" ToolTip="Go" Height="20px"
                                                    align="center" OnClick="Button1_Click" OnClientClick="javascript:return validate1()" />
                                            </td>
                                        </tr>
                                        <tr id="grid" runat="server">
                                            <td align="center" colspan="4">
                                                <asp:GridView ID="grd" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                                                    AutoGenerateColumns="False" DataKeyNames="InvoiceNo" Width="100%" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" ShowFooter="true" Font-Size="Small" OnRowDataBound="grd_RowDataBound1">
                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <Columns>
                                                        <%--<asp:CommandField ShowSelectButton="True" />--%>
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
                                                        <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:#,##,##,###.00}"
                                                            HtmlEncode="false" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("invoice_date")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                    <%-- <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />--%>
                                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblvendorname" Width="100%" class="ajaxspan" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="Paymentdetail">
                                <td>
                                    <table align="center" class="estbl" width="100%">
                                        <tr>
                                            <th align="center" colspan="4">
                                                Payment Details
                                            </th>
                                        </tr>
                                        <tr id="trbankname" runat="server">
                                            <td>
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="Bank:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtbankname" Enabled="false" Width="150px" runat="server" ToolTip="Bankname"></asp:TextBox>
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" Width="200px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlfrom_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Payment Mode"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpayment" runat="server" ToolTip="Mode Of Pay" CssClass="esddown"
                                                    Width="100%" OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbldate" CssClass="eslbl" runat="server" Text="Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="Invoice Date"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                    Animated="true" PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td id="tdlblmode" runat="server" align="center">
                                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Text="No"></asp:Label>
                                            </td>
                                            <td id="tdtxtmode" runat="server">
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="150px"></asp:TextBox>
                                                <asp:DropDownList ID="ddlcheque" runat="server" ToolTip="Cheque No" CssClass="esddown"
                                                    Width="100">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label7" CssClass="eslbl" runat="server" Text="Loan Description"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcrloanpurpose" CssClass="estbox" ToolTip="Loan Purpose" TextMode="MultiLine"
                                                    Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="lblcramount" CssClass="eslbl" runat="server" Text="Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcramount" CssClass="estbox" ToolTip="Amount" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <asp:HiddenField ID="hf1" runat="server" />
                                    </table>
                                </td>
                            </tr>
                            <tr id="trbutton" runat="server">
                                <td align="center">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validate();" OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Reset" />
                                </td>
                            </tr>
                        </table>
                        <table id="printformat" runat="server" style="margin: 1em; border-right: 1px black;"
                            width="800px">
                            <tr>
                                <th colspan="8" style="padding: padding: .3em; border: 1px #000000 solid; font-size: small;
                                    background: #808080;" align="center">
                                    SELF MADE INVOICE/VOUCHER FOR TERM LOAN INSTALLMENT BANK PAYMENT
                                </th>
                            </tr>
                            <tr>
                                <th colspan="8" style="padding: padding: .3em; border: 1px #000000 solid; font-size: small;
                                    background: lightbrown;" align="center">
                                    NORMAL TERM LOAN EMI
                                </th>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="2" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label12" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Trasaction Id"></asp:Label>
                                </td>
                                <td colspan="4" style="padding: .3em; border: 1px #000000 solid; border-right-color: black;"
                                    align="left">
                                    <asp:Label ID="lbltranid" runat="server" Width="100%" Style="color: #000000; font-family: Tahoma;
                                        text-decoration: none"></asp:Label>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="2" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label3" runat="server" Style="color: Black; font-family: Tahoma; font-weight: lighter"
                                        Text="Agency Code"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtagencycode" runat="server" Style="color: #000000; font-family: Tahoma;
                                        text-decoration: none"></asp:TextBox>
                                </td>
                                <%-- <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label4" runat="server" Style="color: Black; font-family: Arial; font-weight: lighter"
                                        Text="Agency Name"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtagencyname" runat="server" Style="color: #000000; font-family: Tahoma;
                                        text-decoration: none"></asp:TextBox>
                                </td>--%>
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="lbldca" runat="server" Style="color: Black; font-family: Tahoma; font-weight: lighter"
                                        Text="Term Loan No"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txttermloanno" runat="server" Width="99%" Style="color: #000000;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>
                                <td colspan="2" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="lblsdca" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Installment No"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtinstallmentno" runat="server" Style="color: #000000; font-family: Tahoma;
                                        text-decoration: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label10" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Party Name"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtpartyname" runat="server" Width="99%" Style="color: #000000;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center" colspan="3">
                                    <asp:Label ID="Label11" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Total Outstanding Principle Amount After Last Installment"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtoutstandingprinciple" runat="server" Style="color: #000000; font-family: Tahoma;
                                        text-decoration: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <%--  <td colspan="3" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label12" runat="server" Style="color: Black; font-family: Arial; font-weight: lighter"
                                        Text="Installment Total Amount"></asp:Label>
                                </td>
                                <td colspan="1" style="border-top: solid 1px black">
                                    <asp:TextBox ID="txttotalinstamount" runat="server"  Style="font-size: 11px;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>--%>
                                <td colspan="5" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label13" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Part Of Principle Amount In Installment"></asp:Label>
                                </td>
                                <td colspan="3" style="border-top: solid 1px black; border-right: solid 1px black">
                                    <asp:TextBox ID="txtprincipleamountininst" runat="server" Style="color: #000000;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="3" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label18" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Installment Date"></asp:Label>
                                </td>
                                <td colspan="1" style="border-top: solid 1px black">
                                    <asp:TextBox ID="txtinstalldate" runat="server" Style="font-size: 11px; font-family: Tahoma;
                                        text-decoration: none"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtinstalldate"
                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                        PopupButtonID="txtinstalldate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td colspan="3" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label19" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Part Of Intrest  Amount In Installment"></asp:Label>
                                </td>
                                <td colspan="1" style="border-top: solid 1px black; border-right: solid 1px black">
                                    <asp:TextBox ID="txtintamountinst" runat="server" Style="color: #000000; font-family: Tahoma;
                                        text-decoration: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="3" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label14" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Priciple Amount Debit CC"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtprincipleamountdebitcc" runat="server" Style="font-size: 11px;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>
                                <td colspan="3" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label16" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Priciple Amount Debit DCA"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtprincipleamountdebitdca" runat="server" Style="color: #000000;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="3" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label5" runat="server" Style="color: Black; font-family: Tahoma; font-weight: lighter"
                                        Text="Interest Amount Debit CC"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtinterestamountdebitcc" runat="server" Style="font-size: 11px;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>
                                <td colspan="3" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label15" runat="server" Style="color: Black; font-family: Arial; font-weight: lighter"
                                        Text="Interest Amount Debit DCA"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtinterestamountdebitdca" runat="server" Style="color: #000000;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White; border-left-color: Black;"
                                    align="center" colspan="4">
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center" colspan="3">
                                    <asp:Label ID="Label27" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Intrest Amount Debit SubDCa"></asp:Label>
                                </td>
                                <td style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtinterestamountdebitsdca" runat="server" Style="color: #000000;
                                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="6" align="center" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;">
                                    <asp:Label ID="Label31" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Balance Due Principle After This Installment"></asp:Label>
                                </td>
                                <td colspan="2" style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtbalanceaftertheinst" runat="server" Style="color: #000000; font-family: Tahoma;
                                        text-decoration: none;" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="6" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label32" runat="server" Style="color: Black; font-family: Tahoma;
                                        font-weight: lighter" Text="Total Debit Amount Now Installment"></asp:Label>
                                </td>
                                <td colspan="2" style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:TextBox ID="txtdebitamountnowinst" runat="server" Style="color: #000000; font-family: Tahoma;
                                        text-decoration: none" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                            <%--    <tr style="padding: .3em; border: 1px #000000 solid;">
                                <td colspan="6" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                    align="center">
                                    <asp:Label ID="Label17" runat="server" Style="color: Black; font-family: Arial; font-weight: lighter"
                                        Text="Self Made Invoice/Voucher  For Term Loan   Submitted By:-  "></asp:Label>
                                </td>
                                <td colspan="2" style="padding: .3em; border: 1px #000000 solid;">
                                    <asp:Label ID="lblsubmittetby" runat="server" Width="90%" Style="color: Black; font-family: Arial;
                                        font-weight: lighter"></asp:Label>
                                </td>
                            </tr>--%>
                        </table>
                        <table id="print" runat="server" width="100%">
                            <tr align="center">
                                <td align="center">
                                    <input class="buttonprint" type="button" onclick="print1();" value="Print" title="Print Report">
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
        function print1() {

            var grid_obj = document.getElementById("<%=printformat.ClientID %>");

            if (grid_obj != null) {
                var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
                new_window.document.write(grid_obj.outerHTML);
                new_window.print();

            }
        }
    </script>
</asp:Content>
