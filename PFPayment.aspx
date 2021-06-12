<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="PFPayment.aspx.cs" Inherits="PFPayment" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddlmonth.ClientID %>", "<%=ddlyear.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }


        function validation() {

            var objs = new Array("<%=ddlbank.ClientID %>", "<%=txtdate.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=ddlcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
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
                for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
                    if (GridView.rows(rowCount).cells(4).children(0).checked == true) {

                        var str1 = GridView.rows(rowCount).cells(0).innerText;

                        var str2 = document.getElementById("<%=txtdate.ClientID %>").value;
                        var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                        var dt1 = str1.substring(0, 2);
                        var dt2 = str2.substring(0, 2);
                        var yr1 = str1.substring(7, 11);
                        var yr2 = str2.substring(7, 11);
                        for (var i = 0; i < args.length; i++) {
                            var month = str2.substring(3, 6);
                            var month1 = str1.substring(3, 6);
                            if (args[i] == month) {
                                var month = parseFloat(i + 1);
                                var date2 = yr2 + "-" + month + "-" + dt2;

                            }
                            if (args[i] == month1) {
                                var month1 = parseFloat(i + 1);
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
                        if (parseFloat(_Diff) < 0) {
                            alert("You are not able to make payment to before Due date");
                            document.getElementById("<%=txtdate.ClientID %>").focus();
                            return false;
                        }

                    }
                }
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }

        function pay() {
            var payment = document.getElementById("<%=ddlpayment.ClientID %>");
            var cheque = document.getElementById("<%=ddlcheque.ClientID %>");
            if (payment.selectedIndex != 1) {
                window.alert("Invalid");
                document.getElementById("<%=ddlpayment.ClientID %>").selectedIndex = 1;
            }
        }
       
    </script>
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
    <script type="text/javascript" language="javascript">
        function SelectAll() {
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            var bank = document.getElementById("<%=ddlbank.ClientID %>").value;
            var originalValue = 0;
            for (var rowCount = 1; rowCount <= GridView1.rows.length - 1; rowCount++) {
                if (GridView1.rows(rowCount).cells(4).children(0) != null) {


                    if (GridView1.rows(rowCount).cells(4).children(0).checked == true) {

                        var value = GridView1.rows(rowCount).cells(3).innerText.replace(/,/g, "");

                        if (value != "") {
                            originalValue += parseInt(value);
                        }
                    }
                }
            }
            document.getElementById('<%= txtamt.ClientID%>').value = originalValue;
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
                <AccountMenu:Menu ID="Menu1" runat="server" />
            </td>
            <td>
                <asp:Panel runat="server" ID="viewreportpanel">
                    <table>
                        <tr>
                            <td align="center">
                                <table class="estbl">
                                    <tr>
                                        <th valign="top" style="background-color: #8B8A8A;" align="center">
                                            <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="PF Payment"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="paytype" runat="server" class="innertab">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server" ToolTip="Month">
                                                                        <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="year"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btngo" runat="server" CssClass="" Text="go" ToolTip="Go" Height="20px"
                                                                        align="center" OnClientClick="javascript:return validate()" OnClick="btngo_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="None"
                                                            Font-Size="Small" DataKeyNames="id">
                                                            <Columns>
                                                                <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />
                                                                <asp:BoundField DataField="date" HeaderText="Date" />
                                                                <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                                                <asp:BoundField DataField="sub_dca" HeaderText="SDCA Code" />
                                                                <asp:BoundField DataField="Debit" HeaderText="Amount" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll();" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="payment" runat="server">
                                                            <ContentTemplate>
                                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="payment" runat="server">
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
                                                                <table id="tblpayment" align="center" class="estbl" width="100%" runat="server">
                                                                    <tr>
                                                                        <th align="center" colspan="4">
                                                                            Payment Details
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="bank" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="Bank:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlbank" runat="server" ToolTip="Bank" CssClass="esddown" Width="200px"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <span class="starSpan">*</span>
                                                                            <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlbank"
                                                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                                                PromptText="Select">
                                                                            </cc1:CascadingDropDown>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" onKeyDown="preventBackspace();"
                                                                                onpaste="return false;" onkeypress="return false;" ToolTip="Paid Date" Width="120px"></asp:TextBox><span
                                                                                    class="starSpan">*</span>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                                                Animated="true" PopupButtonID="txtdate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="ModeofPay" runat="server">
                                                                        <td>
                                                                            Mode Of Pay:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlpayment" runat="server" AutoPostBack="true" ToolTip="Mode Of Pay"
                                                                                CssClass="esddown" Width="100" OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlpayment"
                                                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                                                                PromptText="Select">
                                                                            </cc1:CascadingDropDown>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Text="No:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlcheque" runat="server" ToolTip="Cheque No" CssClass="esddown"
                                                                                Width="100">
                                                                                <asp:ListItem Value="0" Text="select"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="txtcheque" runat="server" ToolTip="Cheque No" CssClass="esddown"
                                                                                Width="180"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Remarks:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Remarks" Width="200px"
                                                                                TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                                                        </td>
                                                                        <td>
                                                                            Amount:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" Width="200px"></asp:TextBox><span
                                                                                class="starSpan">*</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table id="tblsubmit" align="center" class="estbl" width="100%" runat="server">
                                                                    <tr id="btn" runat="server">
                                                                        <td align="center">
                                                                            <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                                                                Text="Submit" OnClientClick="javascript:return validation()" OnClick="btnsubmit_Click" />&nbsp
                                                                            <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small"
                                                                                OnClick="btncancel_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
