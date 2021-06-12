<%@ Page Title="Other Receipts" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="CreditPayments.aspx.cs" Inherits="CreditPayments" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function numericValidation(txtvalue) {
            //debugger;
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
        function isValidKey(e) {
            var charCode = e.keyCode || e.which;
            if (charCode == 8 || charCode == 46)
                return false;
            else
                return true;
        }
    </script>
    <script type="text/javascript">
        function checkDatecredit(sender, args) {
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
            var str1 = document.getElementById("<%=txtchkdate.ClientID %>").value;
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
                document.getElementById("<%=txtchkdate.ClientID %>").value = "";
                return false;
            }
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
                        <%--<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">

                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
                        <table class="estbl" width="750px">
                            <tr>
                                <th align="center">
                                    Credit Payment Form
                                </th>
                            </tr>
                            <tr id="paytype" runat="server">
                                <td align="center">
                                    <table class="innertab">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtchkdate" CssClass="estbox" runat="server" onKeyDown="preventBackspace();"
                                                    onpaste="return false;" onkeypress="return false;" ToolTip="Date" Width="85px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtchkdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtchkdate" OnClientDateSelectionChanged="checkDatecredit">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Category of Payment:"></asp:Label>
                                                <asp:DropDownList ID="ddltypeofpay" runat="server" ToolTip="Type of Payment" AutoPostBack="true"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddltypeofpay_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsubtype" runat="server" ToolTip="Type" AutoPostBack="true"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddlsubtype_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td id="fdr" runat="server">
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="FDR No:"></asp:Label>
                                                <asp:TextBox ID="txtfdr" CssClass="estbox" runat="server" ToolTip="FDR No" Width="100px"></asp:TextBox>
                                            </td>
                                            <td id="Tdmisc" runat="server">
                                                <asp:Label ID="Label18" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Intrest Type:"></asp:Label>
                                                <asp:DropDownList ID="ddlintresttype" runat="server" CssClass="esddown" ToolTip="Intrest Type" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlintresttype_SelectedIndexChanged">
                                                    <asp:ListItem Enabled="true" Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Intrest From Clients" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Intrest From  Others" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Trmisc" runat="server">
                                <td width="750px">
                                    <table class="innertab" width="100%" runat="server" id="Table4">
                                        <tr align="center">
                                            <td colspan="3">
                                                <asp:Label ID="Label16" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Client ID:"></asp:Label>
                                                <asp:DropDownList ID="ddlclientid" runat="server" ToolTip="Client Id" AutoPostBack="true"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Sub Client ID:"></asp:Label>
                                                <asp:DropDownList ID="ddlsubclientid" runat="server" ToolTip="Sub Client ID" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged" AutoPostBack="true" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="750px">
                                    <table class="innertab" width="100%" runat="server" id="Table2">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Width="50px" CssClass="eslbl" Font-Size="XX-Small"
                                                    Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="DCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label11" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="SDCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="Amount"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="750px">
                                    <table class="innertab" width="100%" runat="server" id="cc">
                                        <tr align="center">
                                            <td>
                                                <asp:DropDownList ID="ddlcccodepr" runat="server" ToolTip="Principle Cost Center"
                                                    Width="150px" CssClass="esddown" OnSelectedIndexChanged="ddlcccodepr_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldcapr" runat="server" ToolTip="Principle DCA Code" Width="150px"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddldcapr_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsdcacodepr" runat="server" ToolTip="Principle SDCA Code"
                                                    Width="150px" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtprinciple" CssClass="estbox" runat="server" ToolTip="Principle Amount"
                                                    Width="100px" onkeypress="return numericValidation(this);" onkeyup="Total();"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trintrestheader" runat="server">
                                <th align="center">
                                    Interest If Any
                                </th>
                            </tr>
                            <tr id="trintrestselection" runat="server">
                                <td align="center">
                                    <asp:DropDownList ID="ddlselection" runat="server" CssClass="esddown" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlselection_SelectedIndexChanged">
                                        <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trintrestlables" runat="server">
                                <td width="750px">
                                    <table class="innertab" width="100%" runat="server" id="Table3">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Width="50px" Font-Size="XX-Small"
                                                    Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="DCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="SDCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label15" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="Amount"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trintrestintcontrols" runat="server">
                                <td width="750px">
                                    <table class="innertab" width="100%" runat="server" id="Table1">
                                        <tr align="center" style="background-color: white">
                                            <td>
                                                <asp:DropDownList ID="ddlcccodeint" runat="server" ToolTip="Interest Cost Center"
                                                    Width="150px" CssClass="esddown" OnSelectedIndexChanged="ddlcccodeint_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldcaint" runat="server" ToolTip="Interest DCA Code" Width="150px"
                                                    CssClass="esddown" OnSelectedIndexChanged="ddldcaint_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsdcacodeint" runat="server" ToolTip="Interest SDCA Code"
                                                    Width="150px" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtintrest" CssClass="estbox" runat="server" ToolTip="Interest Amount"
                                                    Width="100px" onkeypress="return numericValidation(this);" onkeyup="Total();"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdedheader" runat="server">
                                <th align="center" colspan="4">
                                    Deduction
                                </th>
                            </tr>
                            <tr id="trdedtitles" runat="server">
                                <td width="750px" colspan="4">
                                    <table class="innertab" width="100%" runat="server" id="Table5">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label19" runat="server" CssClass="eslbl" Width="50px" Font-Size="XX-Small"
                                                    Text="CC Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="DCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label21" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="SDCA Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label22" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="Amount"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdeductioncontrols" runat="server">
                                <td width="750px" colspan="4">
                                    <table class="innertab" width="100%" runat="server" id="Table6">
                                        <tr align="center" style="background-color: white">
                                            <td>
                                                <asp:DropDownList ID="ddlcccodeded" runat="server" ToolTip="Intrest Deduction Cost Center"
                                                    Width="150px" CssClass="esddown" OnSelectedIndexChanged="ddlcccodeded_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldcaded" runat="server" ToolTip="Intrest Deduction DCA Code"
                                                    Width="150px" CssClass="esddown" OnSelectedIndexChanged="ddldcaded_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsdcacodeded" runat="server" ToolTip="Intrest Deduction SDCA Code"
                                                    Width="150px" CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdedamount" CssClass="estbox" runat="server" ToolTip="Intrest Deduction Amount"
                                                    Width="100px" onkeypress="return numericValidation(this);" onkeyup="Total();"></asp:TextBox>
                                                <%----%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trname" runat="server">
                                <td>
                                    <table class="innertab" runat="server" width="100%">
                                        <tr align="center">
                                            <%--<td>
                                                <asp:Label ID="Label2" class="eslbl" runat="server" Text="Due Date:"></asp:Label>
                                            </td>
                                            <td style="width: 150px">
                                                <asp:Label ID="lblduedate" class="ajaxspan" runat="server"></asp:Label>
                                            </td>--%>
                                            <td>
                                                <asp:Label ID="Label3" class="eslbl" runat="server" Text="Name:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtname" CssClass="estbox" runat="server" ToolTip="Name" Width="200px"></asp:TextBox>
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
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Font-Size="XX-Small"
                                                    Text="Bank:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" Width="200px">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr id="ModeofPay" runat="server">
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Mode Of Pay:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpayment" runat="server" ToolTip="Mode Of Pay" CssClass="esddown"
                                                    Width="70">
                                                    <%--OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged"--%>
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="paymentrefund"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                    Width="80px"></asp:TextBox><span class="starSpan">*</span>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                    Animated="true" PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                                <%-- <img onclick="scwShow(document.getElementById('<%=txtdate.ClientID %>'),this);" alt=""
                                                    src="images/cal.gif" style="width: 15px; height: 15px;" id="Img2" />--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                                <%-- <asp:DropDownList ID="ddlcheque" runat="server" ToolTip="Cheque No" CssClass="esddown"
                                                    Width="100">
                                                </asp:DropDownList>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" Width="200px"
                                                    onkeydown="return isValidKey(event)"></asp:TextBox>
                                                <asp:HiddenField ID="hf1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table class="estbl" width="750px">
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validate()" OnClick="btnsubmit_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        function Total() {
            //debugger;
            var invoiceval = 0;
            var intrest = 0;
            var deduction = 0;
            var principle = document.getElementById("<%=txtprinciple.ClientID %>").value;   
            var type = document.getElementById("<%=ddltypeofpay.ClientID %>").value;
            if (type == "Misc Taxable Receipt") {
                var intresttype = document.getElementById("<%=ddlintresttype.ClientID %>").value;                            
                    if (intresttype != "1") {
                        intrest = document.getElementById("<%=txtintrest.ClientID %>").value;
                    }
                    else if (intresttype == "1") {
                        deduction = document.getElementById("<%=txtdedamount.ClientID %>").value;
                    }
                }
                if (type != "Misc Taxable Receipt") {
                    var YN = document.getElementById("<%=ddlselection.ClientID %>").value;
                    if (YN == "Yes") {
                        intrest = document.getElementById("<%=txtintrest.ClientID %>").value;
                    }
                }
            if (type == "Refund" || type == "Other Refunds" || type == "Unsecured Loan" || type == "Misc Taxable Receipt") {
                if (principle == "") {
                    principle = 0;
                }
                if (intrest == "") {
                    intrest = 0;
                }
                if (deduction == "") {
                    deduction = 0;
                }
                invoiceval = eval(((parseFloat(principle) + parseFloat(intrest))-parseFloat(deduction)));
                var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById('<%= txtamt.ClientID%>').value = roundinvvalue;
            }
            if (type == "-Select-") {
                window.alert("Please Select Payment Type");
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function validate() {
           // debugger;
            var objs = new Array("<%=txtchkdate.ClientID %>", "<%=ddltypeofpay.ClientID %>", "<%=ddlsubtype.ClientID %>", "<%=txtfdr.ClientID %>", "<%=ddlintresttype.ClientID %>", "<%=ddlclientid.ClientID %>", "<%=ddlsubclientid.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var objs = new Array("<%=ddlcccodepr.ClientID %>", "<%=ddldcapr.ClientID %>", "<%=ddlsdcacodepr.ClientID %>"
                   , "<%=txtprinciple.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var objs = new Array("<%=ddlcccodeded.ClientID %>", "<%=ddldcaded.ClientID %>", "<%=ddlsdcacodeded.ClientID %>"
                   , "<%=txtdedamount.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var paymenttype = document.getElementById("<%=ddltypeofpay.ClientID %>").value;
            if (paymenttype == "Misc Taxable Receipt") {
                var intresttype = document.getElementById("<%=ddlintresttype.ClientID %>").value;
                if (intresttype != "1") {
                    var selection = document.getElementById("<%=ddlselection.ClientID %>").value;
                    if ((selection != null) && (document.getElementById("<%=ddlselection.ClientID %>").disabled == false)) {
                        if (selection == "Select") {
                            alert('Please select Interest Yes/No');
                            return false;
                        }
                        if (selection == "Yes") {
                            var objs = new Array("<%=ddlcccodeint.ClientID %>", "<%=ddldcaint.ClientID %>", "<%=ddlsdcacodeint.ClientID %>", "<%=txtintrest.ClientID %>");
                            if (!CheckInputs(objs)) {
                                return false;
                            }
                        }
                    }
                }
            }
            var objs = new Array("<%=txtname.ClientID %>", "<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
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
</asp:Content>
