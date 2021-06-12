<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="SrAccountantInbox.aspx.cs" Inherits="SrAccountantInbox" Title="Inbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="Java_Script/newcalendar.js" type="text/javascript"></script>
    <style type="text/css">
        .popup-div-background
        {
            position: absolute;
            top: 0;
            left: 0;
            background-color: #ccc;
            filter: alpha(opacity=90);
            opacity: 0.9; /* the following two line will make sure
             /* that the whole screen is covered by
                 /* this transparent layer */
            height: 100%;
            width: 100%;
            min-height: 100%;
            min-width: 100%;
        }
        .style1
        {
            width: 771px;
        }
    </style>
    <script type="text/javascript">


        function validate() {
            debugger;
            var paymentcategory = document.getElementById("<%=hfpaymentcategory.ClientID%>").value;
            var paymenttype = document.getElementById("<%=hfpaymenttype.ClientID%>").value;
            if (paymentcategory != "Service Provider" && paymentcategory != "Supplier") {
                var objs = new Array("<%=txtdate.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=txtcashname.ClientID %>", "<%=ddlcashdca.ClientID %>", "<%=ddltype.ClientID %>", "<%=txtcashdebit.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            else {
                var objs = new Array("<%=txtdate.ClientID %>", "<%=txtpaiddate.ClientID %>", "<%=txtcashname.ClientID %>", "<%=ddltype.ClientID %>", "<%=txtcashdebit.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            var tran = document.getElementById("<%=txttransaction.ClientID %>").innerHTML;
            var date = document.getElementById("<%=Chkdate.ClientID %>");
            var cc = document.getElementById("<%=chkcc.ClientID %>");
            var cc1 = document.getElementById("<%=Chkcc1.ClientID %>");
            var dca = document.getElementById("<%=Chkdca.ClientID %>");
            var sub = document.getElementById("<%=Chksub.ClientID %>");
            var desc = document.getElementById("<%=Chkdesc.ClientID %>");
            var amt = document.getElementById("<%=Chkamt.ClientID %>");
            var vendor = document.getElementById("<%=ddlvendor.ClientID %>");
            var GridView = document.getElementById("<%=grd.ClientID %>");
            if (paymentcategory != "Service Provider" && paymentcategory != "Supplier" && paymenttype != "Retention" && paymenttype != "Hold") {
                var sdca = document.getElementById("<%=ddlcashsub.ClientID %>").value;
                var subdca = document.getElementById("<%=ddlcashsub.ClientID %>");
                if (sdca == "" && subdca.disabled == false) {
                    window.alert("Select Sub DCA");
                    return false;
                }
            }
            else {
                var sdca = document.getElementById("<%=lblcashsub.ClientID %>").innerHTML;
            }
            var Amount = document.getElementById("<%=txtcashdebit.ClientID %>").value;
            var Sum = 0;

            if (paymentcategory != "Service Provider" && paymentcategory != "Supplier" && paymenttype != "Retention" && paymenttype != "Hold") {
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
            }
            else {
                if (tran == "Debit" && sdca == "") {
                    if (!(date.checked && desc.checked && amt.checked)) {
                        alert("You are not Verified");

                        return false;
                    }
                }
                else if (tran == "Debit" && sdca != "") {
                    if (!(date.checked && desc.checked && amt.checked)) {
                        alert("You are not Verified");
                        return false;
                    }

                }
                else if (tran == "Paid Against" && sdca == "") {
                    if (!(date.checked && desc.checked && amt.checked && cc1.checked)) {
                        alert("You are not Verified");
                        return false;
                    }
                }
                else if (tran == "Paid Against" && sdca != "") {
                    if (!(date.checked && desc.checked && amt.checked && cc1.checked)) {
                        alert("You are not Verified");
                        return false;
                    }
                }
            }
            document.getElementById("<%=btncashApprove.ClientID %>").style.display = 'none';
            return true;
        }

    </script>
    <script type="text/javascript">

        function compare() {
            var amount = document.getElementById("<%=txtdebit.ClientID %>");
            var debit = document.getElementById("<%=hfdebit.ClientID %>").value;

            if (amount > debit) {
                alert("You are not allowed to increase the amount");
                return false;
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
    <script language="javascript">
        function validation() {
            var objs = new Array("<%=txtVoucherDate.ClientID %>", "<%=txtdebit.ClientID %>", "<%=txtDesc.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=Btntransfer.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <%-- <script language="JavaScript">

        function win() {
            window.opener.location.href = "window-refreshing.php";
            self.close();

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
            var str1 = document.getElementById("<%=txtVoucherDate.ClientID %>").value;
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
                document.getElementById("<%=txtVoucherDate.ClientID %>").value = "";
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
        function checkDate2(sender, args) {
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
    <table width="990px">
        <tr>
            <td valign="top" align="center">
                <div class="box-a list-a">
                    <div class="inner">
                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr align="center" style="width: 100%" height="20px" id="trvouchertype" runat="server">
                                    <td style="" align="center" valign="middle">
                                        <asp:DropDownList ID="ddlvouchertype" runat="server" CssClass="char" Height="20px"
                                            Width="162px" OnSelectedIndexChanged="ddlvouchertype_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="1">Voucher for Approval</asp:ListItem>
                                            <asp:ListItem Value="2">Transfer for Verification</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trgrid1" runat="server" class="pagerbar" align="left">
                                    <td class="pagerbar-cell">
                                        <table class="pager-table">
                                            <tbody>
                                                <tr align="left">
                                                    <asp:HiddenField ID="h1" runat="server" />
                                                    <td class="pager-cell" valign="middle" width="100px">
                                                        <div class="pager" align="right">
                                                            <asp:Label ID="lblmonth" runat="server" Text="Filter Type:"></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 2%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" align="left" valign="middle" width="25px">
                                                        <div class="pager">
                                                            <div align="left">
                                                                <asp:DropDownList ID="ddlcccode" runat="server">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="newcostcode"
                                                                    PromptText="Select Cost Center">
                                                                </cc1:CascadingDropDown>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 2%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" valign="middle" width="15px">
                                                        <div class="pager">
                                                            <div align="left">
                                                                <asp:DropDownList ID="ddldetailhead" runat="server">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="dca" TargetControlID="ddldetailhead"
                                                                    ServiceMethod="cash" ServicePath="cascadingDCA.asmx" PromptText="Select DCA">
                                                                </cc1:CascadingDropDown>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 1%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" valign="middle" width="15px">
                                                        <div class="pager">
                                                            <div align="right">
                                                                <asp:DropDownList ID="ddlsubdetail" runat="server">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="subdca" TargetControlID="ddlsubdetail"
                                                                    ParentControlID="ddldetailhead" ServiceMethod="SUBDCA" ServicePath="cascadingDCA.asmx"
                                                                    PromptText="Select Sub DCA">
                                                                </cc1:CascadingDropDown>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 1%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" valign="middle" width="15px">
                                                        <div class="pager">
                                                            <div align="right">
                                                                <asp:DropDownList ID="ddlDate" runat="server" CssClass="char">
                                                                    <asp:ListItem Value="Select Date">Select Date</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                    <asp:ListItem>8</asp:ListItem>
                                                                    <asp:ListItem>9</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" valign="middle" width="15px">
                                                        <div class="pager">
                                                            <div align="right">
                                                                <asp:DropDownList ID="ddlMonth" CssClass="char" runat="server">
                                                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
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
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 1%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" valign="middle" width="15px">
                                                        <div class="pager">
                                                            <div align="right">
                                                                <asp:DropDownList ID="ddlyear" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 3%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 5%" valign="middle">
                                                        <div class="pager">
                                                            <div align="center">
                                                                <asp:ImageButton ID="gobtn" ImageUrl="~/images/go-button.gif" runat="server" OnClick="gobtn_Click" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 12%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
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
                                                <div class="box-a list-a">
                                                    <div class="inner" align="center">
                                                        <table id="tblpayment" runat="server" class="gridview" width="90%" cellspacing="0"
                                                            cellpadding="0">
                                                            <tbody>
                                                                <tr class="pagerbar">
                                                                    <td class="pagerbar-cell" align="right">
                                                                        <table class="pager-table">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="pager-cell" style="width: 90%" valign="middle">
                                                                                        <div class="pager">
                                                                                            <div align="right">
                                                                                                <asp:Label ID="Label2" CssClass="item item-char" runat="server" Text="Change Limit:"></asp:Label>
                                                                                                <asp:DropDownList ID="ddlpagecount" runat="server" OnSelectedIndexChanged="ddlpagecount_SelectedIndexChanged"
                                                                                                    AutoPostBack="true">
                                                                                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                                                                                    <asp:ListItem>20</asp:ListItem>
                                                                                                    <asp:ListItem>50</asp:ListItem>
                                                                                                    <asp:ListItem>100</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="grid-content" align="center">
                                                                        <table id="_terp_list_grid" class="grid-content" width="100%" cellspacing="0" align="center"
                                                                            cellpadding="0" style="background: none;">
                                                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                                                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                AllowPaging="True" DataKeyNames="id,Transaction_no" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                                OnDataBound="GridView1_DataBound" BorderColor="White" EmptyDataText="There is no records"
                                                                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                                                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                                                <Columns>
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                        SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Left" />
                                                                                    <asp:BoundField DataField="Description" ItemStyle-HorizontalAlign="Left" HeaderText="Description" />
                                                                                    <asp:BoundField DataField="cc_code" ItemStyle-HorizontalAlign="Center" HeaderText="CC Code" />
                                                                                    <asp:BoundField DataField="dca_code" ItemStyle-HorizontalAlign="Center" HeaderText="DCA Code" />
                                                                                    <asp:BoundField DataField="sub_dca" ItemStyle-HorizontalAlign="Center" HeaderText="Sub DCA Code" />
                                                                                    <asp:BoundField DataField="PaymentCategory" ItemStyle-HorizontalAlign="Center" HeaderText="Payment Category" />
                                                                                    <asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                                                        ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                                                                </Columns>
                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                <PagerTemplate>
                                                                                    <asp:ImageButton ID="btnFirst" runat="server" Height="15px" ImageUrl="~/images/pager_first.png"
                                                                                        CommandArgument="First" CommandName="Page" OnCommand="btnFirst_Command" />&nbsp;
                                                                                    <asp:ImageButton ID="btnPrev" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_left.png"
                                                                                        CommandArgument="Prev" OnCommand="btnPrev_Command" />
                                                                                    <asp:Label ID="lblpages" runat="server" Text="" Height="15px" CssClass="item item-char"></asp:Label>
                                                                                    of
                                                                                    <asp:Label ID="lblCurrent" runat="server" Text="Label" Height="15px" CssClass="item item-char"></asp:Label>
                                                                                    <asp:ImageButton ID="btnNext" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_right.png"
                                                                                        CommandArgument="Next" OnCommand="btnNext_Command" />&nbsp;
                                                                                    <asp:ImageButton ID="btnLast" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_last.png"
                                                                                        CommandArgument="Last" OnCommand="btnLast_Command" />
                                                                                </PagerTemplate>
                                                                            </asp:GridView>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr class="pagerbar">
                                                                    <td class="pagerbar-cell" align="right">
                                                                    </td>
                                                                </tr>
                                                                <tr id="trpopup" runat="server">
                                                                    <td>
                                                                        <table style="width: 700px">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <table width="653px" style="border: 1px solid #000">
                                                                                        <asp:HiddenField ID="hf" runat="server" />
                                                                                        <tr>
                                                                                            <th valign="top" style="background-color: #8B8A8A;" align="center">
                                                                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Approved Cash Voucher"></asp:Label>
                                                                                            </th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <asp:HiddenField ID="hfpaymentcategory" runat="server" />
                                                                                            <asp:HiddenField ID="hfpaymenttype" runat="server" />
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
                                                                                                            <asp:TextBox ID="txtdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                                                onkeypress="return false;" runat="server" ToolTip="Date" CssClass="estbox"></asp:TextBox><span
                                                                                                                    class="starSpan">*</span>
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtdate"
                                                                                                                OnClientDateSelectionChanged="checkDate1">
                                                                                                            </cc1:CalendarExtender>
                                                                                                            <asp:CheckBox ID="Chkdate" runat="server" CssClass="check_box" />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblpaiddate" runat="server" CssClass="eslbl" Text="Paid Date"></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="width: 200px">
                                                                                                            <asp:TextBox ID="txtpaiddate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                                                onkeypress="return false;" runat="server" ToolTip="Date" CssClass="estbox"></asp:TextBox>
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtpaiddate"
                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtpaiddate"
                                                                                                                OnClientDateSelectionChanged="checkDate2">
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
                                                                                                            <asp:CheckBox ID="Chkcc1" runat="server" CssClass="check_box" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trdropdowndca" runat="server">
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbldca" runat="server" CssClass="eslbl" Text="DCA Code"></asp:Label>
                                                                                                        </td>
                                                                                                        <td height="30">
                                                                                                            <%--<asp:Label ID="lblcashdca" runat="server" CssClass="eslbl" Text=""></asp:Label>--%>
                                                                                                            <asp:DropDownList ID="ddlcashdca" CssClass="esddown" onchange="SetDynamicKey('dp3',this.value);"
                                                                                                                runat="server" ToolTip="DCA Code">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:CheckBox ID="Chkdca" runat="server" CssClass="check_box" />
                                                                                                            <cc1:CascadingDropDown ID="casccashdca" runat="server" Category="dca" TargetControlID="ddlcashdca"
                                                                                                                ServiceMethod="newcash" ServicePath="cascadingDCA.asmx" PromptText="Select DCA">
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
                                                                                                            <%--<asp:Label ID="lblcashsub" CssClass="eslbl" runat="server" Text=""></asp:Label>--%>
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
                                                                                                    <tr id="trlabeldca" runat="server">
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="DCA Code"></asp:Label>
                                                                                                        </td>
                                                                                                        <td height="30">
                                                                                                            <asp:Label ID="lblcashdca" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="Sub DCA"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblcashsub" CssClass="eslbl" runat="server" Text=""></asp:Label>
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
                                                                                                            <asp:DropDownList ID="ddlvendor" CssClass="esddown" ToolTip="Vendor" runat="server">
                                                                                                            </asp:DropDownList>
                                                                                                            <%--AutoPostBack="true" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged"--%>
                                                                                                            <asp:CheckBox ID="chkvendor" CssClass="check_box" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <%--<tr id="tblpo" runat="server">
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Vendor PO"></asp:Label>
                                                                                                        </td>
                                                                                                        <td colspan="3">
                                                                                                            <asp:DropDownList ID="ddlvenpo1" Width="250px" ToolTip="PO" runat="server" CssClass="esddown"
                                                                                                                OnSelectedIndexChanged="ddlvenpo1_SelectedIndexChanged" AutoPostBack="true">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:CheckBox ID="CheckBox2" runat="server" CssClass="check_box" />
                                                                                                        </td>
                                                                                                    </tr>--%>
                                                                                                    <tr>
                                                                                                        <td colspan="4">
                                                                                                            <asp:TextBox ID="txtcashname" runat="server" ToolTip="Name" Width="400px" CssClass="estbox"></asp:TextBox>
                                                                                                            <asp:CheckBox ID="Chkname" runat="server" CssClass="check_box" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="Grid" runat="server" style="width: 500px">
                                                                                            <td align="center" class="grid">
                                                                                                <%--  <asp:GridView ID="grd" Width="93%" runat="server" AutoGenerateColumns="False" CssClass="grid-content"
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
                                                                                                </asp:GridView>--%>
                                                                                                <asp:GridView ID="grd" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                                                                                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                                                                                    GridLines="None" ShowFooter="true" Font-Size="Small">
                                                                                                    <%--  <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />--%>
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="invoiceno" HeaderText="InvoiceNo" InsertVisible="False"
                                                                                                            ReadOnly="True" />
                                                                                                        <asp:BoundField DataField="cc_code" HeaderText="CC CODE" />
                                                                                                        <asp:BoundField DataField="dca_code" HeaderText="DCA CODE" />
                                                                                                        <asp:BoundField DataField="vendor_id" HeaderText="Vendor ID" />
                                                                                                        <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:#,##,##,###.00}"
                                                                                                            HtmlEncode="false" />
                                                                                                        <asp:BoundField DataField="netamount" HeaderText="Net Amount" DataFormatString="{0:#,##,##,###.00}"
                                                                                                            HtmlEncode="false" />
                                                                                                        <asp:BoundField DataField="tds" HeaderText="TDS" DataFormatString="{0:#,##,##,###.00}"
                                                                                                            HtmlEncode="false" />
                                                                                                        <asp:BoundField DataField="retention" HeaderText="Retention" DataFormatString="{0:#,##,##,###.00}"
                                                                                                            HtmlEncode="false" />
                                                                                                        <asp:BoundField DataField="hold" HeaderText="Hold" DataFormatString="{0:#,##,##,###.00}"
                                                                                                            HtmlEncode="false" />
                                                                                                        <asp:BoundField DataField="debit" HeaderText="Paid" DataFormatString="{0:#,##,##,###.00}"
                                                                                                            HtmlEncode="false" />
                                                                                                    </Columns>
                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
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
                                                                                                            <asp:Button ID="btncashApprove" runat="server" Text="Approved" CssClass="esbtn" OnClientClick="javascript:return validate();"
                                                                                                                OnClick="btncashApprove_Click1" />
                                                                                                            <%----%>
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
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table class="gridview" width="70%" cellspacing="0" cellpadding="0" id="tblCashtransfer"
                                                            runat="server">
                                                            <tr>
                                                                <td align="center" class="grid-content">
                                                                    <table id="Table1" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                                                        style="background: none;" width="100%">
                                                                        <asp:HiddenField ID="hfdebit" runat="server" />
                                                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                            AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="id"
                                                                            EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" OnPageIndexChanging="GridView2_PageIndexChanging"
                                                                            OnSelectedIndexChanged="GridView2_OnSelectedIndexChanged" PagerStyle-CssClass="grid pagerbar"
                                                                            PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd" Width="100%" OnDataBound="GridView2_DataBound"
                                                                            OnRowDeleting="GridView2_RowDeleting">
                                                                            <Columns>
                                                                                <asp:CommandField ButtonType="Image" HeaderText="Edit" ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif"
                                                                                    ShowSelectButton="true" />
                                                                                <asp:BoundField DataField="id" Visible="false" />
                                                                                <asp:BoundField DataField="voucherdate" HeaderText="Voucher Date" ItemStyle-HorizontalAlign="Left" />
                                                                                <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-HorizontalAlign="Left" />
                                                                                <asp:BoundField DataField="Category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="Debit" HeaderText="debit" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                                                    ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                                                            </Columns>
                                                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                            <PagerStyle CssClass="grid pagerbar" />
                                                                            <HeaderStyle CssClass="grid-header" />
                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                            <PagerTemplate>
                                                                                <asp:ImageButton ID="btnFirst1" runat="server" CommandArgument="First" CommandName="Page"
                                                                                    Height="15px" ImageUrl="~/images/pager_first.png" OnCommand="btnFirst1_Command" />
                                                                                &nbsp;
                                                                                <asp:ImageButton ID="btnPrev1" runat="server" CommandArgument="Prev" CommandName="Page"
                                                                                    Height="15px" ImageUrl="~/images/pager_left.png" OnCommand="btnPrev1_Command" />
                                                                                <asp:Label ID="lblpages" runat="server" CssClass="item item-char" Height="15px" Text=""></asp:Label>
                                                                                of
                                                                                <asp:Label ID="lblCurrent" runat="server" CssClass="item item-char" Height="15px"
                                                                                    Text="Label"></asp:Label>
                                                                                <asp:ImageButton ID="btnNext1" runat="server" CommandArgument="Next" CommandName="Page"
                                                                                    Height="15px" ImageUrl="~/images/pager_right.png" OnCommand="btnNext1_Command" />
                                                                                &nbsp;
                                                                                <asp:ImageButton ID="btnLast1" runat="server" CommandArgument="Last" CommandName="Page"
                                                                                    Height="15px" ImageUrl="~/images/pager_last.png" OnCommand="btnLast1_Command" />
                                                                            </PagerTemplate>
                                                                        </asp:GridView>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table width="350px" style="border: 1px solid #000" id="tblvoucher" runat="server">
                                                                        <tr>
                                                                            <th valign="top" style="background-color: #8B8A8A;" align="center">
                                                                                <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table class="estbl" width="350px">
                                                                                    <tr id="Tr1">
                                                                                        <td>
                                                                                            <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="VoucherDate"></asp:Label>
                                                                                        </td>
                                                                                        <td colspan="3" align="left">
                                                                                            <asp:TextBox ID="txtVoucherDate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                                onkeypress="return false;" runat="server" CssClass="estbox" ToolTip="Date"></asp:TextBox>
                                                                                            <span class="starSpan" style="cursor: not-allowed;">*</span>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Animated="true" CssClass="cal_Theme1"
                                                                                                FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" PopupButtonID="txtVoucherDate" OnClientDateSelectionChanged="checkDate"
                                                                                                TargetControlID="txtVoucherDate">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="Tr2">
                                                                                        <td>
                                                                                            <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="category"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="txtcategory" runat="server" CssClass="estbox" Enabled="false" ToolTip="category"
                                                                                                Width="182px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trsendcc">
                                                                                        <td>
                                                                                            <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Transfer From"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:Label ID="lbltransfercc" runat="server" CssClass="eslbl"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trlist" runat="server">
                                                                                        <td>
                                                                                            <asp:Label ID="lbllist" runat="server" CssClass="eslbl" Text="Transfer To"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:DropDownList ID="ddllist1" runat="server" CssClass="esddown" ToolTip="Type"
                                                                                                Width="175px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="Tr3">
                                                                                        <td>
                                                                                            <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Transfered Amount"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="txtdebit" runat="server" CssClass="estbox" ToolTip="Transfered Amount"
                                                                                                Width="182px" OnKeyPress="compare()" OnKeyUp="compare()" onKeyDown="compare()"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Description"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="txtDesc" runat="server" CssClass="estbox" ToolTip="Description"
                                                                                                TextMode="MultiLine" Width="182px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" colspan="2">
                                                                                            <asp:Button ID="Btntransfer" runat="server" CssClass="esbtn" OnClick="Btntransfer_Click1"
                                                                                                OnClientClick="javascript:return validation();" Text="" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
