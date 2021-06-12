<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmCashflow.aspx.cs"
    Inherits="Admin_frmCashflow" EnableEventValidation="false" Title="Cash Flow - Essel projects Pvt. Ltd " %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function checkdca() {
            var vendor = document.getElementById("<%=ddlvendortype.ClientID %>").value;
            if (vendor != "General Invoice") {
                var dcacode = document.getElementById("<%=ddldetailhead.ClientID %>").value;
                var paytype = document.getElementById("<%=ddlvendortype.ClientID %>").value;
                if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                    if ((paytype == "General") && (dcacode != "")) {
                        var cccode = document.getElementById("<%=ddlcccode.ClientID %>").value;
                        PageMethods.IsDCAPayAvailable(dcacode, cccode, SelectedIndex("<%=rbtntype.ClientID %>"), OnDCASucceeded);

                    }
                }
                else if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                    if ((paytype == "General") && (dcacode != "")) {
                        PageMethods.IsDCAPayAvailable(dcacode, "", SelectedIndex("<%=rbtntype.ClientID %>"), OnDCASucceeded);
                    }

                }
                function OnDCASucceeded(result, userContext, methodName) {
                    if (methodName == "IsDCAPayAvailable") {
                        if (result == "Invalid") {
                            window.alert("Invalid DCA");

                            document.getElementById("<%=Label2.ClientID %>").innerHTML = "";
                            document.getElementById("<%=ddldetailhead.ClientID %>").selectedIndex = 0;
                            return false;
                        }
                        else {

                        }
                    }
                }
            }
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
            var str1 = document.getElementById("<%=txtdt.ClientID %>").value;
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
                document.getElementById("<%=txtdt.ClientID %>").value = "";
               
                return false;
            }
        }
        function checkDatepaid(sender, args) {
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
            var str1 = document.getElementById("<%=txtpaiddate.ClientID %>").value;
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
                document.getElementById("<%=txtpaiddate.ClientID %>").value = "";
              
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
    <asp:Panel runat="server" ID="viewreportpanel">
        <table width="900px">
            <tr valign="top">
                <td style="width: 150px; height: 100%;" valign="top">
                    <AccountMenu:Menu ID="ww" runat="server" />
                </td>
                <td>
                    <asp:LinkButton ID="lnkViewreport" runat="server" OnClick="lnkViewreport_Click" ForeColor="Blue"
                        Style="text-decoration: underline;">View Report</asp:LinkButton>
                    <table runat="server" style="width: 700px">
                        <tr>
                            <td align="center">
                                <table width="653px" style="border: 1px solid #000">
                                    <tr>
                                        <th valign="top" style="background-color: #8B8A8A;" align="center">
                                            <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Cash Flow Form"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table class="estbl" width="653px">
                                                        <tr>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="lbltypetra" CssClass="eslbl" runat="server" Text="Mode of Transaction"></asp:Label>
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" onclick="check();"
                                                                    ToolTip="Mode of Transction" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                                    CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged" AutoPostBack="true">
                                                                    <%--<asp:ListItem Value="Credit">Credit</asp:ListItem>--%>
                                                                    <asp:ListItem Value="Debit">Self Debit</asp:ListItem>
                                                                    <asp:ListItem Value="Paid Against">Debit From Other CC</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td height="30">
                                                                <asp:DropDownList ID="ddlcccode" runat="server" onchange="javascript:SetDynamicKey('dp1',this.value);SetContextKey('dp8',this.value);checkdca();"
                                                                    CssClass="esddown" ToolTip="CC Code" Width="175px">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="newcostcode"
                                                                    PromptText="Select Other CC">
                                                                </cc1:CascadingDropDown>
                                                                <%--  <br />
                                                        <asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp1" runat="server"
                                                            TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                            ServiceMethod="GetCCName">
                                                        </cc1:DynamicPopulateExtender>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Payment Category"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlvendortype" runat="server" onchange="javascript:SetContextKey('dlv',this.value); Dropvalidate();checkdca();"
                                                                    CssClass="esddown" ToolTip="Payment Category" OnSelectedIndexChanged="ddlvendortype_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem>General</asp:ListItem>
                                                                   <%-- <asp:ListItem>Service Provider</asp:ListItem>
                                                                    <asp:ListItem>Supplier</asp:ListItem>--%>
                                                                    <asp:ListItem>General Invoice</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Payment Type"></asp:Label>
                                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Invoice No"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:DropDownList ID="ddltype" CssClass="esddown" ToolTip="Payment Type" runat="server"
                                                                    OnSelectedIndexChanged="ddltype_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem>select</asp:ListItem>
                                                                    <asp:ListItem Value="Invoice">Invoice</asp:ListItem>
                                                                    <asp:ListItem Value="Retention">Retention</asp:ListItem>
                                                                    <asp:ListItem Value="Hold">Hold</asp:ListItem>
                                                                    <asp:ListItem Value="Service Tax">Service Tax</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlinvoiceno" CssClass="esddown" ToolTip="Invoice No" runat="server"
                                                                    OnSelectedIndexChanged="ddlinvoiceno_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbldate" runat="server" CssClass="eslbl" Text="Date"></asp:Label>
                                                            </td>
                                                            <td style="width: 200px">
                                                                <asp:TextBox ID="txtdt" runat="server" ToolTip="Date" onKeyDown="preventBackspace();"
                                                                    onpaste="return false;" onkeypress="return false;" CssClass="estbox"></asp:TextBox><span
                                                                        class="starSpan" style="cursor: not-allowed;">*</span>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdt"
                                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate" Animated="true"
                                                                    PopupButtonID="txtdt">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbltype" runat="server" CssClass="eslbl" Text="Paid Date"></asp:Label>
                                                            </td>
                                                            <td style="width: 200px">
                                                                <asp:TextBox ID="txtpaiddate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                    onkeypress="return false;" runat="server" ToolTip="Paid Date" CssClass="estbox"></asp:TextBox><span
                                                                        class="starSpan">*</span>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtpaiddate"
                                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true" OnClientDateSelectionChanged="checkDatepaid"
                                                                    PopupButtonID="txtpaiddate">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr id="Ven" runat="server">
                                                            <td align="left" width="50px">
                                                                <asp:Label ID="lblms" CssClass="eslbl" runat="server" Text="Select Vendor"></asp:Label>
                                                            </td>
                                                            <td colspan="3" align="left">
                                                                <asp:DropDownList ID="ddlvendor" CssClass="esddown" runat="server" ToolTip="Vendor"
                                                                    OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged" Width="320px" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="Label4" CssClass="eslbl" runat="server" Text="PO"></asp:Label>&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlvenpo" ToolTip="PO" OnSelectedIndexChanged="ddlvenpo_SelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" CssClass="esddown" Width="100px">
                                                                    <%--onchange="javascript:PaybleCheck(this.value); return false;"--%>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr id="trdcasdca" runat="server">
                                                            <td>
                                                                <asp:Label ID="labeldca" runat="server" CssClass="eslbl" Text="DCA Code"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="labeldcacode" runat="server" CssClass="eslbl"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsubdcaa" runat="server" CssClass="eslbl" Text="SDCA Code"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsubdcacode" runat="server" CssClass="eslbl"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="trcascadingdcasdca" runat="server">
                                                            <td>
                                                                <asp:Label ID="lbldca" runat="server" CssClass="eslbl" Text="DCA Code"></asp:Label>
                                                            </td>
                                                            <td height="30">
                                                                <asp:DropDownList ID="ddldetailhead" CssClass="esddown" onchange="javascript:SetDynamicKey('dp3',this.value);Dropvalidate();checkdca();"
                                                                    runat="server" ToolTip="DCA Code">
                                                                </asp:DropDownList>
                                                                <span class="starSpan">*</span>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="dca" TargetControlID="ddldetailhead"
                                                                    ServiceMethod="cash" ServicePath="cascadingDCA.asmx" PromptText="Select DCA">
                                                                </cc1:CascadingDropDown>
                                                                <br />
                                                                <asp:Label ID="Label2" class="ajaxspan" runat="server"></asp:Label>
                                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                                                    TargetControlID="Label2" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                    ServiceMethod="GetDCAName">
                                                                </cc1:DynamicPopulateExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsubdca" CssClass="eslbl" runat="server" Text="Sub DCA"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlsubdetail" CssClass="esddown" runat="server" onchange="SetDynamicKey('dp4',this.value);"
                                                                    ToolTip="Sub DCA">
                                                                </asp:DropDownList>
                                                                <span class="starSpan">*</span>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="subdca" TargetControlID="ddlsubdetail"
                                                                    ParentControlID="ddldetailhead" UseContextKey="true" ServiceMethod="SUBDCA" ServicePath="cascadingDCA.asmx"
                                                                    PromptText="Select Sub DCA">
                                                                </cc1:CascadingDropDown>
                                                                <br />
                                                                <asp:Label ID="Label3" class="ajaxspan" runat="server"></asp:Label>
                                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender4" BehaviorID="dp4" runat="server"
                                                                    TargetControlID="Label3" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                    ServiceMethod="GetSubDCAName">
                                                                </cc1:DynamicPopulateExtender>
                                                            </td>
                                                        </tr>
                                                        <tr id="trlbldcasdca" runat="server">
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="DCA Code"></asp:Label>
                                                            </td>
                                                            <td height="30">
                                                                <asp:Label ID="lbldcacode" class="ajaxspan" runat="server"></asp:Label><br />
                                                                <asp:Label ID="lbldcadesc" class="ajaxspan" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="Sub DCA"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsdcacode" class="ajaxspan" runat="server"></asp:Label><br />
                                                                <asp:Label ID="lblsdcadesc" class="ajaxspan" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="trspan" runat="server">
                                                            <td colspan="4">
                                                                <span class="ajaxspan" id="Span" runat="server"></span>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="trspanlbl" runat="server">
                                                            <td colspan="4">
                                                                <asp:Label ID="lblspan" class="ajaxspan" runat="server"></asp:Label>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblname" CssClass="eslbl" runat="server" Text="Name"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtname" CssClass="estbox" runat="server" Width="189px" MaxLength="50"
                                                                    ToolTip="Name"></asp:TextBox>
                                                                <span class="starSpan">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbldesc" CssClass="eslbl" runat="server" Text="Description"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" TextMode="MultiLine" Height="64px"
                                                                    Width="184px" ToolTip="Decription"></asp:TextBox>
                                                                <span class="starSpan">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr id="amt">
                                                            <td>
                                                                <span id="lblamoutn" class="eslbl"></span>
                                                                <asp:Label ID="lblamoutn1" runat="server" CssClass="eslbl" Text="Amount"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtamt" CssClass="estbox" onkeypress='IsNumeric(event)' runat="server"
                                                                    Width="182px" ToolTip="Amount"></asp:TextBox>
                                                                <span class="starSpan">*</span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%-- <asp:Button ID="btn" runat="server" Text="" CssClass="esbtn" OnClick="btn_Click"
                                                Style="display: none" />&nbsp;
                                            <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Text="Submit"  OnClick="btnsubmit_Click" OnClientClick="javascript:validate(); return false;" />&nbsp;--%>
                                            <asp:Button ID="btn" runat="server" Text="" CssClass="esbtn" OnClick="btn_Click"
                                                Style="display: none" />&nbsp;
                                            <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Text="Submit" OnClientClick="javascript:validate(); return false;" />&nbsp;
                                            <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Text="Cancel" OnClick="btnCancel1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblAlert" CssClass="eslblalert" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <script type="text/javascript">
        function check() {
            var cc = document.getElementById("<%=ddlcccode.ClientID %>");
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                cc.style.display = 'block';
            }
            else if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                cc.style.display = 'none';
                cc.selectedIndex = 0;
                SetDynamicKey('dp1', '');
                SetContextKey('dp8', '');

            }
            else {
                cc.style.display = 'none';
                cc.selectedIndex = 0;

            }
        }
        check();
        function Display() {
            ////debugger;
            var Tr = document.getElementById("<%=Ven.ClientID %>");

            var Span = document.getElementById("<%=Span.ClientID %>");
            var vendor = document.getElementById("<%=ddlvendortype.ClientID %>").value;
            var paymenttype = document.getElementById("<%=ddltype.ClientID %>");
            var lblpaymenttype = document.getElementById("<%=Label5.ClientID %>");
            var invddown = document.getElementById("<%=ddlinvoiceno.ClientID %>");
            var invlabel = document.getElementById("<%=Label7.ClientID %>");
            var cccode = document.getElementById("<%=ddlcccode.ClientID %>");
            var trcascdca = document.getElementById("<%=trcascadingdcasdca.ClientID %>");
            var trlbldca = document.getElementById("<%=trlbldcasdca.ClientID %>");
            if (vendor == "Select" || vendor == "General") {
                Tr.style.display = 'none';

                Span.innerHTML = "";
                SetContextKey('dp8', '');

                document.getElementById("<%=txtpaiddate.ClientID %>").value = "";
                paymenttype.style.display = 'none';
                lblpaymenttype.style.display = 'none';
                invddown.style.display = 'none';
                invlabel.style.display = 'none';

                if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                    cccode.style.display = 'block';
                    trlbldca.style.display = 'none';
                    trcascdca.style.display = 'block';
                    Tr.style.display = 'none';
                }
                else if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                    cccode.style.display = 'none';
                    trlbldca.style.display = 'none';
                    trcascdca.style.display = 'block';
                    Tr.style.display = 'none';
                }
                else {
                    cccode.style.display = 'none';
                    trlbldca.style.display = 'none';
                    trcascdca.style.display = 'block';
                    Tr.style.display = 'none';
                }
            }
            else if (vendor == "General Invoice") {
                Tr.style.display = 'none';
                Span.innerHTML = "";
                document.getElementById("<%=txtpaiddate.ClientID %>").value = "";
                paymenttype.style.display = 'none';
                lblpaymenttype.style.display = 'none';
                invddown.style.display = 'block';
                invlabel.style.display = 'block';
                if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                    cccode.style.display = 'block';
                    trlbldca.style.display = 'block';
                    trcascdca.style.display = 'none';
                }
                else if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                    cccode.style.display = 'none';
                    trlbldca.style.display = 'block';
                    trcascdca.style.display = 'none';
                }
                else {
                    cccode.style.display = 'none';
                    trlbldca.style.display = 'block';
                    trcascdca.style.display = 'none';
                }

            }
            else {
                Tr.style.display = 'block';
                paymenttype.style.display = 'block';
                lblpaymenttype.style.display = 'block';
                invddown.style.display = 'none';
                invlabel.style.display = 'none';
                Span.innerHTML = "";
                document.getElementById("<%=txtpaiddate.ClientID %>").value = document.getElementById("<%=txtdt.ClientID %>").value;
                if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                    cccode.style.display = 'block';
                    trlbldca.style.display = 'none';
                    trcascdca.style.display = 'block';
                }
                else if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                    cccode.style.display = 'none';
                    trlbldca.style.display = 'none';
                    trcascdca.style.display = 'block';
                }
                else {
                    cccode.style.display = 'none';
                    trlbldca.style.display = 'none';
                    trcascdca.style.display = 'block';
                }

            }
        }
        Display();
        function fnvalidate() {
            //debugger;
            var vendor = document.getElementById("<%=ddlvendortype.ClientID %>").value;
            if (!ChceckRBL("<%=rbtntype.ClientID %>"))
                return false;

            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                //var objs = new Array("<%=ddlcccode.ClientID %>", "<%=ddlvendortype.ClientID %>", "<%=txtdt.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=ddldetailhead.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                if (document.getElementById("<%=ddlcccode.ClientID %>").value == "") {
                    var objs = new Array("<%=ddlcccode.ClientID %>")
                }
                else if (vendor == "Select") {
                    var objs = new Array("<%=ddlvendortype.ClientID %>");
                }
                else if (vendor == "General") {
                    var objs = new Array("<%=txtdt.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=ddldetailhead.ClientID %>", "<%=ddlsubdetail.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                }
                else if (vendor == "Service Provider" || vendor == "Supplier") {
                    var objs = new Array("<%=ddltype.ClientID %>", "<%=txtdt.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=ddlvendor.ClientID %>", "<%=ddlvenpo.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                }
                else if (vendor == "General Invoice") {
                    var objs = new Array("<%=ddlinvoiceno.ClientID %>", "<%=txtdt.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                }
            }
            else {
                if (vendor == "Select") {
                    var objs = new Array("<%=ddlvendortype.ClientID %>");
                }
                else if (vendor == "General") {
                    var objs = new Array("<%=txtdt.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=ddldetailhead.ClientID %>", "<%=ddlsubdetail.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                }
                else if (vendor == "Service Provider" || vendor == "Supplier") {
                    var objs = new Array("<%=ddltype.ClientID %>", "<%=txtdt.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=ddlvendor.ClientID %>", "<%=ddlvenpo.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                }
                else if (vendor == "General Invoice") {
                    var objs = new Array("<%=ddlinvoiceno.ClientID %>", "<%=txtdt.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=txtname.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                }

            }

            if (!CheckInputs(objs)) {
                return false;
            }

            var sdca = document.getElementById("<%=ddlsubdetail.ClientID %>").value;
            var subdca = document.getElementById("<%=ddlsubdetail.ClientID %>");
            var paymentcategory = document.getElementById("<%=ddlvendortype.ClientID %>").value;

            if (paymentcategory == "Service Provider" || paymentcategory == "Supplier") {
                var Paymanttype = document.getElementById("<%=ddltype.ClientID %>").value;
                var Paymanttypectrl = document.getElementById("<%=ddltype.ClientID %>");
                var vendor = document.getElementById("<%=ddlvendor.ClientID %>").value;
                var vendorctrl = document.getElementById("<%=ddlvendor.ClientID %>");
                if (Paymanttype == "select") {
                    window.alert("Select Payment Type");
                    Paymanttypectrl.focus();
                    return false;
                }
                else if (vendor == "") {
                    window.alert("Select Vendor");
                    vendorctrl.focus();
                    return false;

                }
            }
            else {
                if (sdca == "" && subdca.disabled == false) {
                    window.alert("Select Sub 11DCA");
                    return false;
                }
            }
            var str1 = document.getElementById("<%=txtdt.ClientID %>").value;

            var str2 = document.getElementById("<%=txtpaiddate.ClientID %>").value;
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
                alert("You can't make payment to before Voucher date");
                document.getElementById("<%=txtpaiddate.ClientID %>").focus();
                return false;
            }

            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript">
        function validate() {
            ////debugger;
            if (fnvalidate()) {
                var dca_code = document.getElementById("<%=ddldetailhead.ClientID %>").value;
                var date = document.getElementById("<%=txtdt.ClientID %>").value;
                var Amount = document.getElementById("<%=txtamt.ClientID %>").value;
                var name = document.getElementById("<%=txtname.ClientID %>").value;
                var desc = document.getElementById("<%=txtdesc.ClientID %>").value;
                var vtype = document.getElementById("<%=ddlvendortype.ClientID %>").value;


            }
            else {
                return false;

            }
            if (dca_code.selectedIndex == 0 && date.value == "" && Amount.value == "" && name.value == "" && vtype.selectedIndex == 0 && desc.value == 0) {
                return false;
            }
            else {
                PageMethods.IsVoucherAvailable(dca_code, date, Amount, name, OnSucceeded);
            }
        }

        function OnSucceeded(result, userContext, methodName) {
            if (methodName == "IsVoucherAvailable") {
                var btn = document.getElementById("<%=btn.ClientID  %>");
                if (result != "") {

                    var response = confirm(result);
                    if (response) {
                        btn.click();
                    }
                    else {
                        window.location = window.location;
                        return false;
                    }
                }
                else {


                    btn.click();
                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function Dropvalidate() {


            var detailhead = document.getElementById("<%=ddldetailhead.ClientID%>").value;
            var detailheadctrl = document.getElementById("<%=ddldetailhead.ClientID%>");
            var paymenttype = document.getElementById("<%=ddlvendortype.ClientID%>").value;
            var paymenttypectrl = document.getElementById("<%=ddlvendortype.ClientID%>");
            var lbl12 = document.getElementById("<%=Label2.ClientID%>");

            if (detailhead == "DCA-03" && paymenttype == "General") {
                window.alert("you can't make payment under DCA-03");
                document.getElementById("<%=ddldetailhead.ClientID%>").selectedIndex = 0;
                lbl12.innerHTML = "";

                return false;
            }
        }
            
            
            
    </script>
    <script language="javascript">
        var POCheckerTimer;
        function PaybleCheck(PONO) {
            clearTimeout(POCheckerTimer);
            CCcodeCheckerTimer = setTimeout("checkcc('" + PONO + "');", 750);
            var paymenttype = document.getElementById("<%=ddltype.ClientID %>").value;
            PageMethods.IsPOPayAvailable(PONO, paymenttype, OnSuccess);
        }
        function OnSuccess(result, userContext, methodName) {
            if (methodName == "IsPOPayAvailable") {

                var btn = document.getElementById("<%=btnsubmit.ClientID %>");
                var Span = document.getElementById("<%=Span.ClientID %>");


                if (result == "There is no payble") {

                    alert(result);
                    window.location = window.location;
                    return false;

                }
                else {
                    var values = result.split('|');


                    Span.innerHTML = "Net Payable is: " + values[2];


                }

            }

        }
    </script>
</asp:Content>
