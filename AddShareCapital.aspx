<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="AddShareCapital.aspx.cs" Inherits="AddShareCapital" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<%@ Register Src="~/LedgerCreationUserControl.ascx" TagName="LedgerCreation" TagPrefix="Ledger"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <%--<script type="text/javascript">
        function validate() {

            var type = document.getElementById("<%=ddltype.ClientID %>");
            var objs = new Array("<%=ddltype.ClientID %>", "<%=txtsharedate.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (type.selectedIndex == 1) {
                var objs = new Array("<%=txtshareholdername.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            else if (type.selectedIndex == 2) {
                var objs = new Array("<%=ddlshareholdername.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            var objs = new Array("<%=txtamount.ClientID %>", "<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>");
            if (!CheckInputs(objs)) {

                return false;
              
            }
            else {

                var btn = document.getElementById("<%=hdnbtn.ClientID %>");
                btn.click();
              
            }
            
            var bank = document.getElementById("<%=ddlfrom.ClientID %>").value;
            var response = confirm("Do you want to Continue with the " + bank);

            if (response) {
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                return true;
            }
            else {
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'block';
                return false;
            }
        }
    </script>--%>
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
        function checkDate1(sender, args) {
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
            var str1 = document.getElementById("<%=txtsharedate.ClientID %>").value;
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
                document.getElementById("<%=txtsharedate.ClientID %>").value = "";
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
        function Total() {
            var amt = document.getElementById("<%=txtamount.ClientID %>").value;
            if (amt == "") {
                document.getElementById('<%= txtamt.ClientID%>').value = 0;
            }
            else {
                document.getElementById('<%= txtamt.ClientID%>').value = amt;
            }
        }
    </script>
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
                        <table class="estbl" width="745px">
                            <tr>
                                <th align="center">
                                    Add Share Capital
                                </th>
                            </tr>
                            <tr id="paytype" runat="server">
                                <td>
                                    <table class="innertab">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Width="150px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="Share Holder Type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddltype" runat="server" CssClass="esddown" Width="200px" ToolTip="Type"
                                                    OnSelectedIndexChanged="ddltype_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="New">New</asp:ListItem>
                                                    <asp:ListItem Value="Additional">Additional</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtsharedate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="Share Capital Date"
                                                    Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtsharedate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtsharedate" OnClientDateSelectionChanged="checkDate1">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="vename" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="150px"
                                                    Text="Share Holder Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtshareholdername" runat="server" ToolTip="Share Holder Name" Width="200px"></asp:TextBox>
                                                <asp:DropDownList ID="ddlshareholdername" CssClass="esddown" ToolTip="Share Holder Name"
                                                    Width="200px" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                    Text="Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamount" runat="server" onkeypress="return numericValidation(this);"
                                                    ToolTip="Amount" onkeyup="Total();" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Trledger" runat="server">
                                <td>
                                   <Ledger:LedgerCreation ID="Ledger" runat="server" />
                                </td>
                            </tr>
                            <tr id="trpaymentdetails" runat="server">
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="paymentdetails">
                                        <tr>
                                            <th align="center" colspan="4">
                                                Payment Details
                                            </th>
                                        </tr>
                                        <tr id="bank" runat="server">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" 
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr id="ModeofPay" runat="server">
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Mode Of Pay:"></asp:Label>
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
                                                <asp:TextBox ID="txtdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                    Width="80px"></asp:TextBox><span class="starSpan">*</span>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    PopupButtonID="txtdate" OnClientDateSelectionChanged="checkDate">
                                                </cc1:CalendarExtender>
                                                <%----%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" Enabled="false" ToolTip="Amount"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="tblbtn" runat="server" rules="estbl" width="660px">
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return Newvalidate();" OnClick="btnsubmit_Click" />
                                      
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
    
</asp:Content>
