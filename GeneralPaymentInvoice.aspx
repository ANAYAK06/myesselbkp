<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="GeneralPaymentInvoice.aspx.cs"
    Inherits="GeneralPaymentInvoice" Title="Invoice For General Payment" EnableEventValidation="false" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function validate() {
            var sdca = document.getElementById("<%=ddlsubdetail.ClientID %>").value;
            var subdca = document.getElementById("<%=ddlsubdetail.ClientID %>");
            var cctype = document.getElementById("<%=ddlcctype.ClientID %>").value;

            var objs = new Array("<%=ddlcctype.ClientID %>", "<%=ddltype.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddldetailhead.ClientID %>", "<%=ddlsubdetail.ClientID %>", "<%=ddlmodeofpay.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (cctype == "Performing") {
                var type = document.getElementById("<%=ddltype.ClientID %>").value;
                if (type == "Select") {
                    window.alert("Select Sub Type");
                    return false;
                }
            }
            if (sdca == "" && subdca.disabled == false) {
                window.alert("Select Sub DCA");
                return false;
            }
            if (document.getElementById("<%=ddldetailhead.ClientID %>").value == "DCA-Excise" && document.getElementById("<%=ddlno.ClientID %>").value == "Select") {
                window.alert("Select Excise No");
                return false;
            }
            if (document.getElementById("<%=ddldetailhead.ClientID %>").value == "DCA-Vat" && document.getElementById("<%=ddlno.ClientID %>").value == "Select") {
                window.alert("Select Vat No");
                return false;
            }
            if (document.getElementById("<%=ddldetailhead.ClientID %>").value == "DCA-SRTX" && document.getElementById("<%=ddlno.ClientID %>").value == "Select") {
                window.alert("Select Service Tax No");
                return false;
            }
            var objs = new Array("<%=txtname.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtremarks.ClientID %>", "<%=txtamount.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;

        }
              
    </script>
    <script language="javascript">
        function numericFilter(txb) {
            txb.value = txb.value.replace(/[^\0-9]/ig, "");
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
                <table style="margin: 1em; border-collapse: collapse;" width="500px">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <th colspan="4" style="padding: padding: .3em; border: 1px #000000 solid; font-size: small;
                                                background: #808080;" align="center">
                                                Self Invoice For General Payment
                                            </th>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <asp:Label ID="Label1" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Cost Center Type"></asp:Label>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                <asp:DropDownList ID="ddlcctype" runat="server" AutoPostBack="true" CssClass="esddown"
                                                    ToolTip="CC Type" Width="130px" OnSelectedIndexChanged="ddlcctype_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Performing</asp:ListItem>
                                                    <asp:ListItem Value="2">Non-Performing</asp:ListItem>
                                                    <asp:ListItem Value="3">Capital</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td id="tdsubtype" runat="server" style="padding: .3em; border: 1px #000000 solid;
                                                border-right-color: White;" align="center">
                                                <asp:Label ID="lblsubtype" runat="server" Style="color: Black; font-family: Arial;
                                                    font-weight: bold" Text="Sub Type"></asp:Label>
                                            </td>
                                            <td id="tdtype" runat="server" style="padding: .3em; border: 1px #000000 solid; border-right-color: Black;"
                                                align="center">
                                                <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="130px" CssClass="esddown"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Service</asp:ListItem>
                                                    <asp:ListItem Value="2">Trading</asp:ListItem>
                                                    <asp:ListItem Value="3">Manufacturing</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <asp:Label ID="Label3" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="CC Code"></asp:Label>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="175px"
                                                    CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%-- <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="newcostcode"
                                PromptText="Select Cost Center">
                            </cc1:CascadingDropDown>--%>
                                                <%-- <asp:TextBox ID="txtcccode" runat="server" Style="color: #000000; font-family: Tahoma;
                                text-decoration: none"></asp:TextBox>--%>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <%--<asp:Label ID="Label1" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                Text="Invoice No"></asp:Label>--%>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid; border-right-color: Black;"
                                                align="center">
                                                <%--<asp:TextBox ID="txtadviceno" runat="server" Style="color: #000000; font-family: Tahoma;
                                text-decoration: none" Width="90%"></asp:TextBox>--%>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid; height: 50px;">
                                            <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <asp:Label ID="Label5" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Dca"></asp:Label>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid; width: 150px;" align="center"
                                                valign="top">
                                                <asp:DropDownList ID="ddldetailhead" CssClass="esddown" Width="150px" ToolTip="DCA"
                                                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldetailhead_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%-- <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="dca" TargetControlID="ddldetailhead"
                                ServiceMethod="cash" ServicePath="cascadingDCA.asmx" PromptText="Select DCA">
                            </cc1:CascadingDropDown>--%>
                                                <br />
                                                <asp:Label ID="lbldca" Font-Size="Smaller" runat="server"></asp:Label>
                                                <%--<cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                TargetControlID="lbldca" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                ServiceMethod="GetDCAName">
                            </cc1:DynamicPopulateExtender>--%>
                                            </td>
                                            <td id="tdsub" runat="server" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <asp:Label ID="Label7" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="SubDca"></asp:Label>
                                            </td>
                                            <td id="tdsub1" runat="server" style="padding: .3em; border: 1px #000000 solid;"
                                                align="center" valign="top">
                                                <asp:DropDownList ID="ddlsubdetail" CssClass="esddown" Width="90%" runat="server"
                                                    ToolTip="Sub DCA" AutoPostBack="true" OnSelectedIndexChanged="ddlsubdetail_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%-- <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="subdca" TargetControlID="ddlsubdetail"
                                ParentControlID="ddldetailhead" ServiceMethod="SUBDCA" ServicePath="cascadingDCA.asmx"
                                PromptText="Select Sub DCA">
                            </cc1:CascadingDropDown>--%>
                                                <br />
                                                <asp:Label ID="lblsubdca" Font-Size="Smaller" runat="server"></asp:Label>
                                                <%--  <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender4" BehaviorID="dp4" runat="server"
                                TargetControlID="lblsubdca" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                ServiceMethod="GetSubDCAName">
                            </cc1:DynamicPopulateExtender>--%>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid; height: 50px;">
                                            <td style="padding: .3em; border: 1px #000000 solid; width: 150px;" align="center"
                                                valign="top" colspan="2">
                                                <asp:DropDownList ID="ddlno" CssClass="esddown" Width="150px" ToolTip="Excise" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <asp:Label ID="Label6" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Mode of Pay"></asp:Label>
                                            </td>
                                            <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-left-color: White;"
                                                align="center">
                                                <asp:DropDownList ID="ddlmodeofpay" runat="server" ToolTip="Mode Of Pay" Width="130px"
                                                    CssClass="esddown">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                    <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <asp:Label ID="Label2" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Party Name"></asp:Label>
                                            </td>
                                            <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-left-color: White;"
                                                align="center">
                                                <asp:TextBox ID="txtname" runat="server" Width="90%" ToolTip="Name" Style="color: #000000;
                                                    font-family: Tahoma; text-decoration: none"></asp:TextBox>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                                                align="center">
                                                <asp:Label ID="Label24" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Invoice Date"></asp:Label>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                <asp:TextBox ID="txtdate" runat="server" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" ToolTip="Date" Style="color: #000000; font-family: Tahoma;
                                                    text-decoration: none" Width="90%"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                    Animated="true" PopupButtonID="txtdate" >
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td align="right" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;">
                                                <asp:Label ID="Label4" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Remarks"></asp:Label>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                <asp:TextBox ID="txtremarks" runat="server" ToolTip="Remarks" Style="color: #000000;
                                                    font-family: Tahoma; text-decoration: none;" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                            </td>
                                            <td align="right" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;">
                                                <asp:Label ID="Label28" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                    Text="Invoice Amount"></asp:Label>
                                            </td>
                                            <td style="padding: .3em; border: 1px #000000 solid;" align="center">
                                                <asp:TextBox ID="txtamount" runat="server" ToolTip="Amount" Style="color: #000000;
                                                    font-family: Tahoma; text-decoration: none;" Width="90%" onKeyUp="numericFilter(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%--<tr style="padding: .3em; border: 1px #000000 solid;">
                                        <td colspan="6" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;
                                            border-bottom-color: Black;" align="center">
                                            <asp:Label ID="Label29" runat="server" Style="color: Black; font-family: Arial; font-weight: bold"
                                                Text="Payment Finally Released By"></asp:Label>
                                        </td>
                                        <td colspan="2" style="padding: .3em; border: 1px #000000 solid;">
                                            <asp:TextBox ID="txtfpayrelease" runat="server" Style="color: #000000; font-family: Tahoma;
                                                text-decoration: none" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr style="padding: .3em; border: 1px #000000 solid;">
                        <td colspan="4" align="center">
                            <asp:Button CssClass="esbtn" Style="font-size: small; height: 26px;" ID="btnsubmit"
                                runat="server" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />
                            <asp:Button CssClass="esbtn" Style="font-size: small; height: 26px;" ID="btnreset"
                                runat="server" Text="Reset" OnClick="btnreset_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
