<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovedCashVoucher.aspx.cs"
    Inherits="ApprovedCashVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Css/calender-blue.css" rel="stylesheet" type="text/css" />
    <link href="Css/listgrid.css" rel="stylesheet" type="text/css" />
    <link href="Css/notebook.css" rel="stylesheet" type="text/css" />
    <link href="Css/pager.css" rel="stylesheet" type="text/css" />

    <script src="Java_Script/validations.js" type="text/javascript"></script>

    <script type="text/javascript">


        function validate() {
            var objs = new Array('txtdate', 'txtpaiddate', 'txtcashname', 'ddlcashdca', 'ddltype', 'ddlvenpo1', 'txtcashdebit');
            if (!CheckInputs(objs)) {
                return false;
            }
            var tran = document.getElementById('txttransaction').innerHTML;
            var date = document.getElementById('Chkdate');
            var cc = document.getElementById('chkcc');
            var cc1 = document.getElementById('Chkcc1');
            var dca = document.getElementById('Chkdca');
            var sub = document.getElementById('Chksub');
            var desc = document.getElementById('Chkdesc');
            var amt = document.getElementById('Chkamt');
            var vendor = document.getElementById('ddlvendor');
            var po1ctrl = document.getElementById('ddlvenpo1');
            var GridView = document.getElementById('grd');
            var sdca = document.getElementById('ddlcashsub').value;
            var subdca = document.getElementById('ddlcashsub');
            var Amount = document.getElementById('txtcashdebit').value;
            var Sum = 0;
            if (sdca == "" && subdca.disabled == false) {
                window.alert("Select Sub DCA");
                return false;
            }
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
                for (var rowCount1 = 1; rowCount1 < GridView.rows.length; rowCount1++) {

                    if (GridView.rows(rowCount1).cells(4).children(0).checked == true) {
                        Sum += Number(GridView.rows(rowCount1).cells(3).innerHTML);
                    }

                }
                if (Amount > Sum) {
                    alert("Invalid Amount");

                    return false;
                }
                for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {

                    if (GridView.rows(rowCount).cells(4).children(0).checked == true) {

                        var str1 = GridView.rows(rowCount).cells(5).children[0].value;

                        var str2 = document.getElementById('txtdate').value;
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
                            alert("You are not able to make payment to before invoice date");
                            document.getElementById("txtdate").focus();
                            return false;
                        }
                    }
                }
            }

            if (tran == "Debit" && sdca == "") {
                if (!(date.checked && cc.checked && dca.checked && desc.checked && amt.checked)) {
                    alert("You are not Verified");

                    return false;
                }



            }
            else if (tran == "Debit" && sdca != "") {
                if (!(date.checked && cc.checked && dca.checked && desc.checked && amt.checked && sub.checked)) {
                    alert("You are not Verified");
                    return false;
                }


            }
            else if (tran == "Paid Against" && sdca == "") {
                if (!(date.checked && cc.checked && dca.checked && desc.checked && amt.checked && sub.checked && cc1.checked)) {
                    alert("You are not Verified");
                    return false;
                }


            }
            else if (tran == "Paid Against" && sdca != "") {
                if (!(date.checked && cc.checked && dca.checked && desc.checked && amt.checked && sub.checked && cc1.checked && sub.checked)) {
                    alert("You are not Verified");
                    return false;
                }



            }
            return true;

        }
        function GetPO() {
            var btn1 = document.getElementById('Button2');
            btn1.click();
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

    <script language="JavaScript">

        function win() {
            window.opener.location.href = "window-refreshing.php";
            self.close();

        }
    </script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 700px">
                    <tr>
                        <td align="center">
                            <table width="653px" style="border: 1px solid #000">
                                <tr>
                                    <asp:HiddenField ID="hf" runat="server" />
                                    <th valign="top" style="background-color: #8B8A8A;" align="center">
                                        <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Approved Cash Voucher"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="estbl" width="653px">
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lbltypetra" CssClass="eslbl" runat="server" Text="Mode of Transaction"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="txttransaction" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbldate" runat="server" CssClass="eslbl" Text="Date"></asp:Label>
                                                </td>
                                                <td style="width: 200px">
                                                    <asp:TextBox ID="txtdate" runat="server" ToolTip="Date" CssClass="estbox"></asp:TextBox><span
                                                        class="starSpan">*</span>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtdate">
                                                    </cc1:CalendarExtender>
                                                    <asp:CheckBox ID="Chkdate" runat="server" CssClass="check_box" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblpaiddate" runat="server" CssClass="eslbl" Text="Paid Date"></asp:Label>
                                                </td>
                                                <td style="width: 200px">
                                                    <asp:TextBox ID="txtpaiddate" runat="server" ToolTip="Date" CssClass="estbox"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtpaiddate"
                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtpaiddate">
                                                    </cc1:CalendarExtender>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="check_box" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcccode" runat="server" CssClass="eslbl" Text="CC Code"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtcashcc" runat="server" Text=""></asp:Label>
                                                    <asp:CheckBox ID="chkcc" runat="server" CssClass="check_box" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblpaidagainst" runat="server" CssClass="eslbl" Text="Paid Against"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcashcc" runat="server" ToolTip="Cost Center" Width="105px"
                                                        CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlcashcc_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--    <cc1:CascadingDropDown ID="cascashcc" runat="server" TargetControlID="ddlcashcc"
                                                        ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="costcode"
                                                        PromptText="Select Cost Center">
                                                    </cc1:CascadingDropDown>--%>
                                                    <asp:CheckBox ID="Chkcc1" runat="server" CssClass="check_box" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbldca" runat="server" CssClass="eslbl" Text="DCA Code"></asp:Label>
                                                </td>
                                                <td height="30">
                                                    <asp:DropDownList ID="ddlcashdca" CssClass="esddown" onchange="SetDynamicKey('dp3',this.value);"
                                                        runat="server" ToolTip="DCA Code">
                                                    </asp:DropDownList>
                                                    <asp:CheckBox ID="Chkdca" runat="server" CssClass="check_box" />
                                                    <cc1:CascadingDropDown ID="casccashdca" runat="server" Category="dca" TargetControlID="ddlcashdca"
                                                        ServiceMethod="cash" ServicePath="cascadingDCA.asmx" PromptText="Select DCA">
                                                    </cc1:CascadingDropDown>
                                                    <br />
                                                    <asp:Label ID="lbdca" class="ajaxspan" runat="server"></asp:Label>
                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                                        TargetControlID="lbdca" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                        ServiceMethod="GetDCAName">
                                                    </cc1:DynamicPopulateExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsubdca" CssClass="eslbl" runat="server" Text="Sub DCA"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcashsub" CssClass="esddown" onchange="SetDynamicKey('dp4',this.value);"
                                                        runat="server" ToolTip="Sub DCA">
                                                    </asp:DropDownList>
                                                    <span class="starSpan">*</span>
                                                    <asp:CheckBox ID="Chksub" runat="server" CssClass="check_box" />
                                                    <cc1:CascadingDropDown ID="casccashsub" runat="server" Category="subdca" TargetControlID="ddlcashsub"
                                                        ParentControlID="ddlcashdca" ServiceMethod="SUBDCA" ServicePath="cascadingDCA.asmx"
                                                        PromptText="Select Sub DCA">
                                                    </cc1:CascadingDropDown>
                                                    <br />
                                                    <asp:Label ID="lbsubdca" class="ajaxspan" runat="server"></asp:Label>
                                                    <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender4" BehaviorID="dp4" runat="server"
                                                        TargetControlID="lbsubdca" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                        ServiceMethod="GetSubDCAName">
                                                    </cc1:DynamicPopulateExtender>
                                                </td>
                                            </tr>
                                            <tr id="tblvendor" runat="server">
                                                <td>
                                                    <asp:Label ID="lblpo" runat="server" Text="Payment" CssClass="eslbl"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddltype" CssClass="esddown" ToolTip="Type" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblms" CssClass="eslbl" runat="server" Text="Vendor Id"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlvendor" CssClass="esddown" ToolTip="Vendor" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--  <cc1:CascadingDropDown ID="cascvendor" runat="server" Category="vendor" TargetControlID="ddlvendor"
                                                        ServiceMethod="vendor1" ServicePath="cascadingDCA.asmx">
                                                    </cc1:CascadingDropDown>
                                                    <asp:Button ID="Button2" runat="server" Width="100%" Text="Go" BackColor="Gray" ForeColor="White"
                                                        Style="cursor: hand; display: none" CssClass="button_wid_string" OnClick="Button2_Click" />--%>
                                                    <asp:CheckBox ID="chkvendor" CssClass="check_box" runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="tblpo" runat="server">
                                                <td>
                                                    <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Vendor PO"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="ddlvenpo1" Width="250px" ToolTip="PO" runat="server" CssClass="esddown"
                                                        OnSelectedIndexChanged="ddlvenpo1_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" CssClass="check_box" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtcashname" runat="server" ToolTip="Name" CssClass="estbox"></asp:TextBox>
                                                    <%--  <asp:Label ID="txtcashname" runat="server" Text=""></asp:Label>--%>
                                                    <asp:CheckBox ID="Chkname" runat="server" CssClass="check_box" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Grid" runat="server">
                                    <td align="center">
                                        <asp:GridView ID="grd" Width="93%" runat="server" AutoGenerateColumns="False" CssClass="grid-content"
                                            HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                            AllowPaging="false" DataKeyNames="InvoiceNo">
                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" InsertVisible="False"
                                                    ReadOnly="True" FooterText="Total" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:#,##,##,###.00}"
                                                    HtmlEncode="false" />
                                                <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" DataFormatString="{0:#,##,##,###.00}"
                                                    HtmlEncode="false" />
                                                <asp:BoundField DataField="Balance" HeaderText="Balance" HtmlEncode="false" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("invoice_date")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table class="estbl" width="653px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbldesc" runat="server" CssClass="eslbl" Text="Description"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcashdesc" runat="server" CssClass="estbox" Height="64px" TextMode="MultiLine"
                                                        ToolTip="Decription" Width="184px"></asp:TextBox>
                                                    <asp:CheckBox ID="Chkdesc" runat="server" CssClass="check_box" />
                                                </td>
                                            </tr>
                                            <tr id="amt">
                                                <td>
                                                    <asp:Label ID="lblamoutn1" CssClass="eslbl" runat="server" Text="Amount"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcashdebit" CssClass="estbox" runat="server" Width="182px" ToolTip="Amount"></asp:TextBox>
                                                    <asp:CheckBox ID="Chkamt" runat="server" CssClass="check_box" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btncashApprove" runat="server" Text="Approved" CssClass="esbtn" OnClientClick="javascript:return validate(); "
                                                        OnClick="btncashApprove_Click1" />
                                                </td>
                                            </tr>
                                        </table>
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
            </ContentTemplate>
            <%-- <Triggers>
                <asp:PostBackTrigger ControlID="ddlvenpo1" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
