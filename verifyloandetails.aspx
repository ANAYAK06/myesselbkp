<%@ Page Language="C#" AutoEventWireup="true" CodeFile="verifyloandetails.aspx.cs"
    Inherits="verifyloandetails" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verify Loan</title>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/calender-blue.css" rel="stylesheet" type="text/css" />

    <script src="Java_Script/validations.js" type="text/javascript"></script>

    <%--<script type="text/javascript">
        function validate() {

            var type = document.getElementById('ddlloantype').value;
            var agencycode = document.getElementById('ddlagencycode').value;
            var date = document.getElementById('txtdate').value;
            var loanno = document.getElementById('txtloanno').value;
            var amount = document.getElementById('txtamount').value;
            var intrate = document.getElementById('txtinterestrate').value;
            var noofint = document.getElementById('txtnoofinst').value;
            var stdate = document.getElementById('txtinststartdate').value;
            var enddate = document.getElementById('txtinstenddate').value;
            var purpose = document.getElementById('txtloanpurpose').value;

            if (type == "") {
                window.alert("Check Type");
                document.getElementById('ddlloantype').focus();
                return false;
            }
            else if (agencycode == "") {
                window.alert("Select Agency Code");
                document.getElementById('ddlagencycode').focus();
                return false;
            }

            else if (loanno == "") {
                window.alert("Select Loan No");
                document.getElementById('txtloanno').focus();
                return false;
            }
            else if (amount == "") {
                window.alert("Check Loan Amount");
                document.getElementById('txtamount').focus();
                return false;
            }

            else if (intrate == "") {
                window.alert("Check Interest Rate");
                document.getElementById('txtinterestrate').focus();
                return false;
            }
            else if (noofint == "") {
                window.alert("Check No Of Interest");
                document.getElementById('txtnoofinst').focus();
                return false;
            }

            else if (stdate == "") {
                window.alert("Check Interest Start Date");
                document.getElementById('txtinststartdate').focus();
                return false;
            }
            else if (enddate == "") {
                window.alert("Check Interest End Date");
                document.getElementById('txtinstenddate').focus();
                return false;
            }
            else if (date == "") {
                window.alert("Check Date");
                document.getElementById('txtdate').focus();
                return false;
            }
            else if (purpose == "") {
                window.alert("Check Loan Purpose");
                document.getElementById('txtloanpurpose').focus();
                return false;
            }

            else if (type == "For Capital") {
                var cno = document.getElementById('txtchequeno').value;
                var bankname = document.getElementById('ddlbankname').value;
                if (bankname == "") {
                    window.alert("Check Bank Name");
                    document.getElementById('ddlbankname').focus();
                    return false;
                }
                else if (cno == "") {
                    window.alert("Check Cheque No");
                    document.getElementById('txtchequeno').focus();
                    return false;
                }
            }






        }
    </script>--%>

    <script type="text/javascript">


        function validate() {
            var objs = new Array('ddlloantype', 'ddlagencycode', 'txtloanno', 'txtamount', 'txtinterestrate', 'txtnoofinst', 'txtapplydate', 'txtcheque', 'txtinststartdate', 'txtinstenddate', 'ddlbankname','txtdate', 'ddlpayment', 'txtloanpurpose');
            if (!CheckInputs(objs)) {
                return false;
            }
            if (document.getElementById('ddlloantype').value == "For Capital") {
                var str1 = document.getElementById('txtapplydate').value;

                var str2 = document.getElementById('txtinststartdate').value;
                var str3 = document.getElementById('txtdate').value;
                var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");

                var dt1 = str1.substring(0, 2);
                var dt2 = str2.substring(0, 2);
                var dt3 = str3.substring(0, 2);
                var yr1 = str1.substring(7, 11);
                var yr2 = str2.substring(7, 11);
                var yr3 = str3.substring(7, 11);
                for (var i = 0; i < args.length; i++) {
                    var month = str2.substring(3, 6);
                    var month1 = str1.substring(3, 6);
                    var month2 = str3.substring(3, 6);
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

                }
                var one_day = 1000 * 60 * 60 * 24;
                var x = date1.split("-");
                var y = date2.split("-");
                var Z = date5.split("-");
                var date4 = new Date(x[0], (x[1] - 1), x[2]);
                var date3 = new Date(y[0], (y[1] - 1), y[2]);
                var date6 = new Date(Z[0], (Z[1] - 1), Z[2]);

                var month1 = x[1] - 1;
                var month2 = y[1] - 1;
                var month3 = Z[1] - 1;
                _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
                _Diff1 = Math.ceil((date6.getTime() - date4.getTime()) / (one_day));
                _Diff2 = Math.ceil((date3.getTime() - date6.getTime()) / (one_day));
                if (parseInt(_Diff) < 0) {
                    alert("Installment date should be after the loan date only.");
                    document.getElementById('txtinststartdate').focus();
                    return false;
                }
                if (parseInt(_Diff1) < 0) {
                    alert("You are not able to make credited date before approved date");
                    document.getElementById('txtapplydate').focus();
                    return false;
                }
                if (parseInt(_Diff2) < 0) {
                    alert("You are not able to make credited  before Installment date");
                    document.getElementById('txtinststartdate').focus();
                    return false;
                }
            }
            else {

                var str1 = document.getElementById('txtapplydate').value;

                var str2 = document.getElementById('txtinststartdate').value;
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
                    alert("You are not able to put before apply date");
                    document.getElementById('txtinststartdate').focus();
                    return false;
                }
            }
        }

    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div style="width: 700px">
        <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
        <table style="width: 700px; border: 1px solid #000">
            <tr valign="top">
                <td align="center">
                    <table class="estbl" width="700px">
                        <tr style="border: 1px solid #000">
                            <th colspan="4">
                                <asp:Label ID="itform" CssClass="esfmhead" Width="550px" runat="server" Text="Verify Loan Details"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Loan Type"></asp:Label>
                            </td>
                            <td colspan="1">
                                <asp:DropDownList ID="ddlloantype" Width="200px" AutoPostBack="true" ToolTip="Loan Type"
                                    runat="server" OnSelectedIndexChanged="ddlloantype_SelectedIndexChanged">
                                    <asp:ListItem>Select Loan Type</asp:ListItem>
                                    <asp:ListItem>For Capital</asp:ListItem>
                                    <asp:ListItem>For Purchase Of Assets</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="lblagencycode" CssClass="eslbl" runat="server" Text="Agency Code"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddlagencycode" Width="100%" ToolTip="Agency Code" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlagencycode_SelectedIndexChanged">
                                </asp:DropDownList>
                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlagencycode"
                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="Agencycode"
                                    PromptText="Select Agency Code">
                                </cc1:CascadingDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="Agency Name"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtagencyname" Enabled="false" CssClass="estbox" ToolTip="Agency Name"
                                    Width="100%" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="lblloanno" CssClass="eslbl" runat="server" Text="Loan No"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtloanno" CssClass="estbox" ToolTip="Loan No" Width="100%" runat="server"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="lblamount" CssClass="eslbl" runat="server" Text="Amount"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtamount" CssClass="estbox" ToolTip="Amount" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Interest Rate"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtinterestrate" CssClass="estbox" ToolTip="Interest Rate" Width="100%"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="No of Installments"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtnoofinst" CssClass="estbox" ToolTip="No Of Installments" Width="100%"
                                    runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="Loan Applied Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtapplydate" ToolTip="Apply Date" CssClass="estbox" Width="100%"
                                    runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtapplydate"
                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                    PopupButtonID="txtapplydate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Installment Start Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtinststartdate" ToolTip="Installment Start Date" CssClass="estbox"
                                    Width="100%" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtinststartdate"
                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                    PopupButtonID="txtinststartdate">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label6" CssClass="eslbl" runat="server" Text="Installment End Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtinstenddate" ToolTip="Installment End Date" CssClass="estbox"
                                    Width="100%" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtinstenddate"
                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                    PopupButtonID="txtinstenddate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr id="ModeofPay" runat="server">
                            <td>
                                <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="Bank Name"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlbankname" ToolTip="Bank Name" Width="100%" runat="server">
                                </asp:DropDownList>
                                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlbankname"
                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="to"
                                    PromptText="Select Bank Name">
                                </cc1:CascadingDropDown>
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdate" ToolTip="Installment End Date" CssClass="estbox" Width="100%"
                                    runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate"
                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                    PopupButtonID="txtdate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr id="pay" runat="server">
                            <td>
                                <asp:Label ID="Label7" CssClass="eslbl" runat="server" Text="Mode Of Pay"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlpayment" runat="server" ToolTip="Mode Of Pay" CssClass="esddown"
                                    Width="100%">
                                </asp:DropDownList>
                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                    PromptText="Select">
                                </cc1:CascadingDropDown>
                            </td>
                            <td>
                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Text="No:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="100%"></asp:TextBox><span
                                    class="starSpan">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="Loan Purpose"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtloanpurpose" CssClass="estbox" ToolTip="Loan Purpose" TextMode="MultiLine"
                                    Width="100%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnAddagency" CssClass="esbtn" OnClientClick="return validate();"
                                    runat="server" Text="Verify Loan" OnClick="btnAddagency_Click" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnresetagency" runat="server" CssClass="esbtn" Text="Close" OnClick="btnresetagency_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    </form>
</body>
</html>
