<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Addloandetails.aspx.cs"
    Inherits="Addloandetails" Title="Add Loan" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validate() {
            var objs = new Array("<%=ddlloantype.ClientID %>", "<%=txtloanno.ClientID %>", "<%=ddlagencycode.ClientID %>", "<%=txtapplydate.ClientID %>", "<%=txtdisposalamt.ClientID %>", "<%=txtprocessingcrg.ClientID %>", "<%=txtinststartdate.ClientID %>", "<%=txtinterestrate.ClientID %>", "<%=txtinstenddate.ClientID %>", "<%=txtnoofinst.ClientID %>", "<%= txtchequeno.ClientID %>", "<%=ddlbankname.ClientID %>", "<%=txtdate.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtloanpurpose.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (document.getElementById("<%=ddlloantype.ClientID %>").value == "For Capital") {
                var str1 = document.getElementById("<%=txtapplydate.ClientID %>").value;

                var str2 = document.getElementById("<%=txtinststartdate.ClientID %>").value;
                var str3 = document.getElementById("<%=txtdate.ClientID %>").value;
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
                    document.getElementById("<%=txtinststartdate.ClientID %>").focus();
                    return false;
                }
                if (parseInt(_Diff1) < 0) {
                    alert("You are not able to make credited date before approved date");
                    document.getElementById("<%=txtapplydate.ClientID %>").focus();
                    return false;
                }
                if (parseInt(_Diff2) < 0) {
                    alert("You are not able to make credited  before Installment date");
                    document.getElementById("<%=txtinststartdate.ClientID %>").focus();
                    return false;
                }
            }
            else {

                var str1 = document.getElementById("<%=txtapplydate.ClientID %>").value;

                var str2 = document.getElementById("<%=txtinststartdate.ClientID %>").value;
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
                    document.getElementById("<%=txtinststartdate.ClientID %>").focus();
                    return false;
                }
            }
            document.getElementById("<%=btnAddagency.ClientID %>").style.display = 'none';
            return true;
        }

    </script>
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

        function TotalAmt() {
            var originalValue = 0;
            var dispamt = document.getElementById("<%=txtdisposalamt.ClientID %>").value;
            var prccharge = document.getElementById("<%=txtprocessingcrg.ClientID %>").value;
            if (dispamt == "") {
                dispamt = 0;
            }
            if (prccharge == "") {
                prccharge = 0;
            }
            originalValue = eval(parseFloat(dispamt) + parseFloat(prccharge));          
            //var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtamount.ClientID%>').value = originalValue;

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
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table style="width: 700px">
                            <tr valign="top">
                                <td align="center">
                                    <table class="estbl" width="700px">
                                        <tr style="border: 1px solid #000">
                                            <th colspan="4">
                                                <asp:Label ID="itform" CssClass="esfmhead" Width="550px" runat="server" Text="Add Loan Agency"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Loan Type"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlloantype" Width="200px" ToolTip="Loan Type" runat="server"
                                                    OnSelectedIndexChanged="ddlloantype_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>Select Loan Type</asp:ListItem>
                                                    <asp:ListItem>For Capital</asp:ListItem>
                                                    <asp:ListItem>For Purchase Of Assets</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="lblloanno" CssClass="eslbl" runat="server" Text="Loan No"></asp:Label>
                                            </td>
                                            <td style="width: 300px" >
                                                <asp:TextBox ID="txtloanno" CssClass="estbox" ToolTip="Loan No" Width="100%" runat="server"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label ID="lblagencycode" CssClass="eslbl" runat="server" Text="Agency Code"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:DropDownList ID="ddlagencycode" Width="100%" ToolTip="Agency Code" onchange="SetDynamicKey('dpp',this.value);"
                                                    runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblagencyname" class="ajaxspan" runat="server"></asp:Label>
                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dpp" runat="server"
                                                    TargetControlID="lblagencyname" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                    ServiceMethod="GetAgencyName">
                                                </cc1:DynamicPopulateExtender>
                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlagencycode"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="Agencycode"
                                                    PromptText="Select Agency Code">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Loan Applied Date"></asp:Label>
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
                                            <td style="width: 150px">
                                                <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="Disbursal Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdisposalamt" CssClass="estbox" onkeyup="TotalAmt();" onkeypress='javascript:return checkNumeric(event);' ToolTip="Disbursal Amount" Width="100%"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="Processing Charge"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtprocessingcrg" CssClass="estbox" onkeyup="TotalAmt();" onkeypress='javascript:return checkNumeric(event);' ToolTip="Processing Charge"
                                                    Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="right">
                                                <asp:Label ID="lblamount" CssClass="eslbl" runat="server" Text="Amount"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:TextBox ID="txtamount" CssClass="estbox" ToolTip="Amount" Enabled="false" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
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
                                            <td style="width: 150px">
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
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="No of Installments"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtnoofinst" CssClass="estbox" ToolTip="No Of Installments" Width="100%"
                                                    runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Interest Rate"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:TextBox ID="txtinterestrate" CssClass="estbox" ToolTip="Interest Rate" Width="100%"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Paymentdetails" runat="server">
                                            <td style="width: 150px">
                                                <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="Bank Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlbankname" ToolTip="Bank Name" Width="100%" runat="server">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlbankname"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="to"
                                                    PromptText="Select Bank Name">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdate" ToolTip="Date" CssClass="estbox" Width="100%" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr id="Pay" runat="server">
                                            <td style="width: 150px">
                                                <asp:Label ID="Label8" CssClass="eslbl" runat="server" Text="Cheque No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtchequeno" CssClass="estbox" ToolTip="Cheque No" Width="100%"
                                                    runat="server"></asp:TextBox>
                                                <%----%>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="PayMode" runat="server" CssClass="eslbl" Text="Mode Of Pay"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:DropDownList ID="ddlpayment" Width="100%" runat="server" ToolTip="Mode Of Pay"
                                                    CssClass="esddown">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlpayment"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label7" CssClass="eslbl" runat="server" Text="Loan Purpose"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtloanpurpose" CssClass="estbox" ToolTip="Loan Purpose" TextMode="MultiLine"
                                                    Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Button ID="btnAddagency" CssClass="esbtn" OnClientClick="javascript:return validate()"
                                                    runat="server" Text="Add Loan Details" OnClick="btnAddagency_Click" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnresetagency" runat="server" CssClass="esbtn" Text="Reset" OnClick="btnresetagency_Click" />
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
