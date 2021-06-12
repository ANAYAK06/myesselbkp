<%@ Page Title="Asset Sale Payment" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="AssetSalePayment.aspx.cs" Inherits="AssetSalePayment" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
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
            var str1 = document.getElementById("<%=txtdates.ClientID %>").value;
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
                document.getElementById("<%=txtdates.ClientID %>").value = "";
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
        function searchvalidate() {
            var Search = document.getElementById("<%=ddlTranNo.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=ddlTranNo.ClientID %>");
            if (Search == "Select") {
                window.alert("Please Select Transaction No");
                Searchctrl.focus();
                return false;
            }

        }
        function Deletevalidate() {
            if (confirm("Are you sure,you want to delete selected records ?") == true)
                return true;
            else
                return false;
        }

        function validate() {
            //debugger;
            GridView = document.getElementById("<%=GridView1.ClientID %>");
            var date = document.getElementById("<%=txtdates.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdates.ClientID %>");
            var name = document.getElementById("<%=txtbuyername.ClientID %>").value;
            var namectrl = document.getElementById("<%=txtbuyername.ClientID %>");
            var desc = document.getElementById("<%=txtbuyeraddress.ClientID %>").value;
            var descctrl = document.getElementById("<%=txtbuyeraddress.ClientID %>");
            if (date == "") {
                window.alert("Enter Date");
                datectrl.focus();
                return false;
            }
            if (GridView == null) {
                alert("Please Add Asset Item");
                return false;
            }
            if (name == "") {
                window.alert("Enter Name of the Buyer");
                namectrl.focus();
                return false;
            }
            if (desc == "") {
                window.alert("Enter Address of the Buyer");
                descctrl.focus();
                return false;
            }

            document.getElementById("<%=btnSave.ClientID %>").style.display = 'none';
            return true;
        }
       
    </script>
    <script language="javascript">
        function closepopup() {
            $find('mdlreport').hide();
        }
        function showpopup() {
            $find('mdlreport').show();

        }
    </script>
    <script language="javascript" type="text/javascript">
        function SelectAllCheckboxes(spanChk) {
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
        function SelectAll() {
            //debugger;
            var GridView2 = document.getElementById("<%=grd.ClientID %>");
            var service = document.getElementById("<%=ddlservice.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                if (service.selectedIndex == 1 || service.selectedIndex == 2) {
                    if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");

                            if (value != "") {
                                originalValue += parseFloat(value);

                            }
                        }
                    }
                }
                if (service.selectedIndex == 3 || service.selectedIndex == 4) {
                    if (GridView2.rows(rowCount).cells(10).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(10).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(7).innerText.replace(/,/g, "");

                            if (value != "") {
                                originalValue += parseFloat(value);

                            }
                        }
                    }
                }
            }
            //var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtamount.ClientID%>').value = originalValue;

        }    
    </script>
    <script language="javascript" type="text/javascript">
        function SelectAllCheckboxessup(spanChk) {
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
        function SelectAllsup() {
            //debugger;
            var GridView2 = document.getElementById("<%=grdsup.ClientID %>");
            var service = document.getElementById("<%=ddlservicesup.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                if (service.selectedIndex == 1 || service.selectedIndex == 2) {
                    if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");

                            if (value != "") {
                                originalValue += parseFloat(value);

                            }
                        }
                    }
                }
                if (service.selectedIndex == 3 || service.selectedIndex == 4) {
                    if (GridView2.rows(rowCount).cells(10).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(10).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(7).innerText.replace(/,/g, "");

                            if (value != "") {
                                originalValue += parseFloat(value);

                            }
                        }
                    }
                }
            }
            //var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtamount.ClientID%>').value = originalValue;

        }
        function isValidKey(e) {
            var charCode = e.keyCode || e.which;
            if (charCode == 8 || charCode == 46)
                return false;
            else
                return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="wrap">
                                <table class="view" cellpadding="0" cellspacing="0" border="0" width="90%">
                                    <tr>
                                        <td align="center">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                                        <ProgressTemplate>
                                                            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                                <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                                    left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                    <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                </div>
                                                            </asp:Panel>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <div id="body_form">
                                                        <div>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 90%" valign="top" align="left">
                                                                        <h3>
                                                                            <a class="help" href="" title=""><small>Payment Note For Sale Of Asset</small></a></h3>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <div id="search_filter_data">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr>
                                                                                    <td class="item search_filters item-filtersgroup" valign="top">
                                                                                        <div class="filters-group">
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="label search_filters search_fields" colspan="4" align="center">
                                                                                        <table class="search_table">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="item item-selection" valign="middle" width="">
                                                                                                        <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txtdates" Font-Size="Small" onKeyDown="preventBackspace();" ToolTip="Date"
                                                                                                            onpaste="return false;" onkeypress="return false;" runat="server" Style="width: 130px;
                                                                                                            height: 20px; vertical-align: middle"></asp:TextBox>
                                                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdates"
                                                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                                                                            Animated="true" PopupButtonID="txtdates">
                                                                                                        </cc1:CalendarExtender>
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle" width="">
                                                                                                        <asp:Label ID="Label2" runat="server" Text="Select Item"></asp:Label>
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle" style="width: 625px">
                                                                                                        <asp:DropDownList ID="ddlTranNo" CssClass="char" runat="server">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <div class="box-a list-a">
                                                                            <div class="inner">
                                                                                <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr class="pagerbar">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                                <table class="pager-table">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td class="pager-cell">
                                                                                                                <h2>
                                                                                                                    Select Transaction No:-</h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                            </td>
                                                                                                            <td class="pager-cell">
                                                                                                                <asp:Button ID="btnadd" runat="server" OnClientClick="javascript:return searchvalidate()"
                                                                                                                    Text="Add" Height="18px" CssClass="button" OnClick="btnadd_Click" />&nbsp;&nbsp;
                                                                                                            </td>
                                                                                                            <td class="pager-cell">
                                                                                                            </td>
                                                                                                            <td class="pager-cell" style="width: 90%" valign="middle">
                                                                                                                <div class="pager">
                                                                                                                    <div align="right">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="grid-content">
                                                                                                <table id="_terp_list_grid" class="grid" width="750px" align="center" style="background: none;">
                                                                                                    <asp:GridView ID="GridView1" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        DataKeyNames="id" ShowFooter="false">
                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                                                            <asp:BoundField DataField="Request_No" HeaderText="Transaction No" ItemStyle-Width="150px" />
                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" ItemStyle-Width="150px" />
                                                                                                            <asp:BoundField DataField="Date" HeaderText="Invoice Date" ItemStyle-Width="150px" />
                                                                                                            <asp:BoundField DataField="BookValue_Date" HeaderText="Book Value Date" ItemStyle-Width="150px" />
                                                                                                            <asp:BoundField DataField="Selling_Amt" HeaderText="Selling Amount" ItemStyle-Width="150px" />
                                                                                                            <asp:BoundField DataField="Balance_Amt" HeaderText="Balance Amount" ItemStyle-Width="150px" />
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <asp:HiddenField ID="hfremain" runat="server" />
                                                                                        <asp:HiddenField ID="hfbookvaluedate" runat="server" />
                                                                                        <tr id="tbldesc" style="border-collapse: separate; border-spacing: 0 15px; margin-bottom: -15px;"
                                                                                            runat="server">
                                                                                            <td>
                                                                                                <table style="border-collapse: separate; border-spacing: 0 15px; margin-top: -15px;">
                                                                                                    <tr>
                                                                                                        <td width="800px" colspan="6">
                                                                                                            <table align="center" class="search_table" width="800px">
                                                                                                                <tr>
                                                                                                                    <td align="left" class="item item-selection" colspan="2" width="300px">
                                                                                                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Name of Buyer"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td align="left" class="item item-selection" colspan="4" valign="middle">
                                                                                                                        <asp:TextBox ID="txtbuyername" runat="server" Font-Bold="true" Font-Size="Small"
                                                                                                                            Enabled="false" CssClass="filter_item" MaxLength="200" ToolTip="Buyer Name" Width="500px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" class="item item-selection" colspan="2" width="300px">
                                                                                                                        <asp:Label ID="lbldate" runat="server" Font-Bold="true" Text="Address of Buyer"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td align="left" class="item item-selection" colspan="4" valign="middle">
                                                                                                                        <asp:TextBox ID="txtbuyeraddress" runat="server" Font-Bold="true" Font-Size="Small"
                                                                                                                            Enabled="false" CssClass="filter_item" TextMode="MultiLine" ToolTip="Description"
                                                                                                                            Width="500px" Height="150px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="trpaymentselection" runat="server">
                                                                                            <td align="center">
                                                                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Payment Type:- "></asp:Label>
                                                                                                <asp:DropDownList ID="ddlselection" runat="server" CssClass="esddown" ToolTip="Payment Type"
                                                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlselection_SelectedIndexChanged">
                                                                                                    <asp:ListItem Enabled="true" Text="Select" Value="Select"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Bank Payment" Value="BankPayment"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Service Provider Payment" Value="ServiceProvider"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Supplier Payment" Value="Supplier"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px">
                                                                                                <table align="center" class="estbl" width="100%" runat="server" id="tblbankpayment">
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
                                                                                                            <asp:Label ID="Label4" runat="server" Text="Date"></asp:Label>
                                                                                                            <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                                                                                Width="80px"></asp:TextBox><span class="starSpan">*</span>
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                                PopupButtonID="txtdate">
                                                                                                                <%--OnClientDateSelectionChanged="checkDate"--%>
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="No:"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                                                                                                class="starSpan">*</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label5" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Remarks:"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                                                                                Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label8" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Amount:"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtamt" runat="server" Font-Bold="true" Font-Size="Small" CssClass="filter_item"
                                                                                                                ToolTip="Amount" Width="150px" onkeyup="Amounvalidation(this.value);" onkeypress='return numericValidation(this);'></asp:TextBox>
                                                                                                            <%--onkeydown="return isValidKey(event)"  --%>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <table class="estbl" width="745px" id="tblserviceprovider" runat="server">
                                                                                                    <tr>
                                                                                                        <th align="center">
                                                                                                            Service Provider Payment
                                                                                                        </th>
                                                                                                    </tr>
                                                                                                    <tr id="paytype" runat="server">
                                                                                                        <td>
                                                                                                            <table class="innertab">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="Label9" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                                                                            Text="Payment Type"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddlservice" Width="150px" runat="server" ToolTip="ServiceType"
                                                                                                                            CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlservice_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="vename" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                                                                                            Text="Vendor Name"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddlvendor" CssClass="esddown" Width="300px" ToolTip="Vendor"
                                                                                                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trselection" runat="server">
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label10" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                                                                Text="View Records of:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:DropDownList ID="ddlmonth" runat="server" CssClass="esddown" ToolTip="Month">
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
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:DropDownList ID="ddlyear" runat="server" CssClass="esddown" ToolTip="Year" />
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:DropDownList ID="ddlpo" runat="server" ToolTip="Po" Width="105px" CssClass="esddown">
                                                                                                            </asp:DropDownList>
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:Button ID="Button1" runat="server" CssClass="esbtn" Text="Go" ToolTip="Go" Height="20px"
                                                                                                                Width="30px" align="center" OnClick="Button1_Click" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trgrid" runat="server">
                                                                                                        <td align="center" colspan="2">
                                                                                                            <asp:GridView ID="grd" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                                                                                                                AutoGenerateColumns="False" Width="745px" CellPadding="4" ForeColor="#333333"
                                                                                                                GridLines="None" ShowFooter="true" Font-Size="Small" DataKeyNames="InvoiceNo,po_no"
                                                                                                                OnRowDataBound="grd_RowDataBound" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                                                                                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                                                                <Columns>
                                                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                                        SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" InsertVisible="False"
                                                                                                                        ReadOnly="True" FooterText="Total" />
                                                                                                                    <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" HtmlEncode="false" />
                                                                                                                    <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                                                                                    <asp:BoundField DataField="Dca_code" HeaderText="DCA Code" />
                                                                                                                    <asp:BoundField DataField="Vendor_id" HeaderText="Vendor ID" />
                                                                                                                    <asp:BoundField DataField="BasicValue" HeaderText="Basic" DataFormatString="{0:#,##,##,###.00}"
                                                                                                                        HtmlEncode="false" />
                                                                                                                    <asp:BoundField DataField="Retention" HeaderText="Retention" DataFormatString="{0:#,##,##,###.00}"
                                                                                                                        HtmlEncode="false" />
                                                                                                                    <asp:BoundField DataField="Hold" HeaderText="Hold" DataFormatString="{0:#,##,##,###.00}"
                                                                                                                        HtmlEncode="false" />
                                                                                                                    <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" DataFormatString="{0:#,##,##,###.00}"
                                                                                                                        HtmlEncode="false" />
                                                                                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:#,##,##,###.00}"
                                                                                                                        HtmlEncode="false" />
                                                                                                                    <asp:TemplateField>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll();" />
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderTemplate>
                                                                                                                            <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                                                                                                onclick="javascript:SelectAllCheckboxes(this);" />
                                                                                                                        </HeaderTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField ItemStyle-Width="2px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("invoice_date")%>' />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField ItemStyle-Width="2px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("status")%>' />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                                            </asp:GridView>
                                                                                                            <cc1:ModalPopupExtender ID="popview" BehaviorID="mdlreport" runat="server" TargetControlID="btnModalPopUp"
                                                                                                                PopupControlID="pnlreport" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                                            <asp:Panel ID="pnlreport" runat="server" Style="display: none;">
                                                                                                                <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                                                                    height: 500px;">
                                                                                                                    <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                                                                        <ContentTemplate>
                                                                                                                            <asp:GridView ID="Grdviewpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                DataKeyNames="Dca_Code,Subdca_Code" OnRowDataBound="Grdviewpopup_RowDataBound"
                                                                                                                                ShowFooter="true">
                                                                                                                                <Columns>
                                                                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                                                                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC Code" ItemStyle-Width="100px" />
                                                                                                                                    <asp:BoundField DataField="" HeaderText="Dca Code" />
                                                                                                                                    <asp:BoundField DataField="" HeaderText="Subdca Code" />
                                                                                                                                    <asp:BoundField DataField="RegdNo" HeaderText="Regd No" />
                                                                                                                                    <asp:BoundField DataField="Tax_Type" HeaderText="Tax Type" />
                                                                                                                                    <asp:BoundField DataField="Type" HeaderText="Type" />
                                                                                                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-Width="100px"
                                                                                                                                        DataFormatString="{0:#,##,##,###.00}" />
                                                                                                                                    <asp:BoundField DataField="Amount" HeaderText="Total Amount" ItemStyle-Width="100px"
                                                                                                                                        DataFormatString="{0:#,##,##,###.00}" />
                                                                                                                                </Columns>
                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                <HeaderStyle CssClass="grid-header" />
                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                            </asp:GridView>
                                                                                                                            <button class="button-error pure-button" onclick="closepopup();">
                                                                                                                                Close</button>
                                                                                                                        </ContentTemplate>
                                                                                                                    </asp:UpdatePanel>
                                                                                                                </div>
                                                                                                                <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trname" align="center" runat="server">
                                                                                                        <td>
                                                                                                            <table class="innertab">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="Label11" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Name:"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtname" CssClass="estbox" runat="server" Enabled="false" ToolTip="Name"
                                                                                                                            Width="200px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <table class="estbl" id="tblsupplier" runat="server" width="745px">
                                                                                                    <tr>
                                                                                                        <th align="center">
                                                                                                            Supplier Payment
                                                                                                        </th>
                                                                                                    </tr>
                                                                                                    <tr id="Tr1" runat="server">
                                                                                                        <td>
                                                                                                            <table class="innertab">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="Label12" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                                                                            Text="Payment Type"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddlservicesup" Width="150px" runat="server" ToolTip="ServiceType"
                                                                                                                            CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlservicesup_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="venamesup" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                                                                                            Text="Vendor Name"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddlvendorsup" CssClass="esddown" Width="300px" ToolTip="Vendor"
                                                                                                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvendorsup_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trselectionsup" runat="server">
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label14" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                                                                Text="View Records of:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:DropDownList ID="ddlmonthsup" runat="server" CssClass="esddown" ToolTip="Month">
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
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:DropDownList ID="ddlyearsup" runat="server" CssClass="esddown" ToolTip="Year" />
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:DropDownList ID="ddlposup" runat="server" ToolTip="Po" Width="120px" CssClass="esddown">
                                                                                                            </asp:DropDownList>
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:Button ID="Button1sup" runat="server" CssClass="esbtn" Text="Go" ToolTip="Go"
                                                                                                                Height="20px" Width="30px" align="center" OnClick="Button1sup_Click" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trgridsup" runat="server">
                                                                                                        <td align="center" colspan="2">
                                                                                                            <asp:GridView ID="grdsup" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                                                                                                                AutoGenerateColumns="False" DataKeyNames="InvoiceNo,po_no" Width="745px" CellPadding="4"
                                                                                                                ForeColor="#333333" GridLines="None" OnRowDataBound="grdsup_RowDataBound" ShowFooter="true"
                                                                                                                Font-Size="Small" OnSelectedIndexChanged="grdsup_SelectedIndexChanged">
                                                                                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                                                                <Columns>
                                                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                                                        SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                                                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" InsertVisible="False"
                                                                                                                        ReadOnly="True" FooterText="Total" />
                                                                                                                    <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" HtmlEncode="false" />
                                                                                                                    <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                                                                                                    <asp:BoundField DataField="Dca_code" HeaderText="DCA Code" />
                                                                                                                    <asp:BoundField DataField="Vendor_id" HeaderText="Vendor ID" />
                                                                                                                    <asp:BoundField DataField="BasicValue" HeaderText="Basic" DataFormatString="{0:#,##,##,###.00}"
                                                                                                                        HtmlEncode="false" />
                                                                                                                    <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" DataFormatString="{0:#,##,##,###.00}"
                                                                                                                        HtmlEncode="false" />
                                                                                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:#,##,##,###.00}"
                                                                                                                        HtmlEncode="false" />
                                                                                                                    <asp:TemplateField>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAllsup();" />
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderTemplate>
                                                                                                                            <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAllsup_CheckedChanged"
                                                                                                                                onclick="javascript:SelectAllCheckboxessup(this);" />
                                                                                                                        </HeaderTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField ItemStyle-Width="2px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("invoice_date")%>' />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField ItemStyle-Width="2px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("status")%>' />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                                            </asp:GridView>
                                                                                                            <cc1:ModalPopupExtender ID="popviewsup" BehaviorID="mdlreport" runat="server" TargetControlID="btnModalPopUpsup"
                                                                                                                PopupControlID="pnlreportsup" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                                            <asp:Panel ID="pnlreportsup" runat="server" Style="display: none;">
                                                                                                                <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                                                                    height: 500px;">
                                                                                                                    <asp:UpdatePanel ID="upindentsup" runat="server" UpdateMode="Conditional">
                                                                                                                        <ContentTemplate>
                                                                                                                            <asp:GridView ID="Grdviewpopupsup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                                                DataKeyNames="Dca_Code,Subdca_Code" OnRowDataBound="Grdviewpopupsup_RowDataBound"
                                                                                                                                ShowFooter="true">
                                                                                                                                <Columns>
                                                                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                                                                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC Code" ItemStyle-Width="100px" />
                                                                                                                                    <asp:BoundField DataField="" HeaderText="Dca Code" />
                                                                                                                                    <asp:BoundField DataField="" HeaderText="Subdca Code" />
                                                                                                                                    <asp:BoundField DataField="RegdNo" HeaderText="Regd No" />
                                                                                                                                    <asp:BoundField DataField="Tax_Type" HeaderText="Tax Type" />
                                                                                                                                    <asp:BoundField DataField="Type" HeaderText="Type" />
                                                                                                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-Width="100px"
                                                                                                                                        DataFormatString="{0:#,##,##,###.00}" />
                                                                                                                                    <asp:BoundField DataField="Amount" HeaderText="Total Amount" ItemStyle-Width="100px"
                                                                                                                                        DataFormatString="{0:#,##,##,###.00}" />
                                                                                                                                </Columns>
                                                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                                                                                <HeaderStyle CssClass="grid-header" />
                                                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                                                                            </asp:GridView>
                                                                                                                            <button class="button-error pure-button" onclick="closepopup();">
                                                                                                                                Close</button>
                                                                                                                        </ContentTemplate>
                                                                                                                    </asp:UpdatePanel>
                                                                                                                </div>
                                                                                                                <asp:Button runat="server" ID="btnModalPopUpsup" Style="display: none" />
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trnamesup" align="center" runat="server">
                                                                                                        <td>
                                                                                                            <table class="innertab">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="Label15" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Name:"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtnamesup" CssClass="estbox" runat="server" Enabled="false" ToolTip="Name"
                                                                                                                            Width="200px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="pagerbar">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tramount" runat="server" align="right" style="padding-top: -10px; width: 600px;">
                                                                                            <td>
                                                                                                <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Amount:- "></asp:Label>
                                                                                                <asp:TextBox ID="txtamount" runat="server" Font-Bold="true" Font-Size="Small" CssClass="filter_item"
                                                                                                    ToolTip="Amount" Width="150px" onkeyup="Amounvalidation(this.value);" onkeypress='return numericValidation(this);'></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="btnvisible" runat="server" style="padding-top: -10px;">
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnSave" runat="server" CssClass="button" Height="18px" OnClick="btnSave_Click"
                                                                                                    OnClientClick="javascript:return validate()" Text="Save" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function validate() {
            //debugger;

            var objs = new Array("<%=txtdates.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            if (GridView1 == null) {
                alert("Please slect transaction no for asset sale");
                return false
            }

            var objs = new Array("<%=ddlselection.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var paymenttype = document.getElementById("<%=ddlselection.ClientID %>").value;
            if (paymenttype == "ServiceProvider") {
                var SellingAmt = document.getElementById("<%=hfremain.ClientID %>").value;
                var objs = new Array("<%=ddlvendor.ClientID %>", "<%=ddlpo.ClientID %>"
                        , "<%=txtname.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                var netamount = document.getElementById("<%= txtamount.ClientID%>").value;
                if ((parseFloat(netamount) < 0)) {
                    alert("You are excessing the Invoice Amount");
                    return false;
                }

                if ((parseFloat(netamount)) > (parseFloat(SellingAmt))) {
                    alert("Invoice Amount is not more than asset selling amount");
                    return false;
                }
                var service = document.getElementById("<%=ddlservice.ClientID %>");
                var GridView = document.getElementById("<%=grd.ClientID %>");
                if (GridView != null) {
                    for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                        if (service.selectedIndex == 1) {
                            if (GridView.rows(rowCount).cells(9).children(0).checked == true) {
                                var str1 = GridView.rows(rowCount).cells(2).innerHTML;
                                var str2 = document.getElementById("<%=hfbookvaluedate.ClientID %>").value;
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
                                var service = document.getElementById("<%=ddlservice.ClientID %>").value;
                                if (service != "Advance Payment") {
                                    if (parseFloat(_Diff) < 0) {
                                        alert("You are not able to make payment to before invoice date");
                                        document.getElementById("<%=txtdates.ClientID %>").focus();
                                        return false;
                                    }
                                }
                                if (GridView.rows(rowCount).cells(11).children[0].value == "1" || GridView.rows(rowCount).cells(11).children[0].value == "2" || GridView.rows(rowCount).cells(11).children[0].value == "2A") {
                                    alert("Invoice is not approved:" + GridView.rows(rowCount).cells(0).innerHTML);
                                    return false;
                                }
                            }
                        }
                        else if (service.selectedIndex == 2 || service.selectedIndex == 3) {
                            if (GridView.rows(rowCount).cells(10).children(0).checked == true) {
                                var str1 = GridView.rows(rowCount).cells(2).innerHTML;
                                var str2 = document.getElementById("<%=hfbookvaluedate.ClientID %>").value;
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
                                    alert("You are not able to make payment to before invoice date");
                                    document.getElementById("<%=txtdate.ClientID %>").focus();
                                    return false;
                                }
                                if (GridView.rows(rowCount).cells(12).children[0].value == "1" || GridView.rows(rowCount).cells(12).children[0].value == "2" || GridView.rows(rowCount).cells(12).children[0].value == "2A") {
                                    alert("Invoice is not approved:" + GridView.rows(rowCount).cells(1).innerHTML);
                                    return false;
                                }
                            }
                        }
                    }
                }
                if (document.getElementById('<%= txtamount.ClientID%>').value == "0" || document.getElementById('<%= txtamount.ClientID%>').value == "") {
                    alert("Amount Required");
                    return false;
                }
            }
            else if (paymenttype == "Supplier") {
                var SellingAmt = document.getElementById("<%=hfremain.ClientID %>").value;
                var objs = new Array("<%=ddlservicesup.ClientID %>", "<%=ddlvendorsup.ClientID %>", "<%=ddlposup.ClientID %>", "<%=txtnamesup.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                var netamount = document.getElementById("<%= txtamount.ClientID%>").value;
                if ((parseFloat(netamount)) < 0) {
                    alert("You are excessing the Invoice Amount");
                    return false;
                }
                if ((parseFloat(netamount)) > (parseFloat(SellingAmt))) {
                    alert("Invoice Amount is not more than asset selling amount");
                    return false;
                }
                var GridViewsup = document.getElementById("<%=grdsup.ClientID %>");
                if (GridViewsup != null) {
                    for (var rowCount = 1; rowCount < GridViewsup.rows.length - 1; rowCount++) {
                        if (GridViewsup.rows(rowCount).cells(9).children(0).checked == true) {
                            var str1 = GridViewsup.rows(rowCount).cells(2).innerHTML;
                            var str2 = document.getElementById("<%=hfbookvaluedate.ClientID %>").value;
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
                            var service = document.getElementById("<%=ddlservicesup.ClientID %>").value;
                            if (service != "Advance Payment") {
                                if (parseFloat(_Diff) < 0) {
                                    alert("You are not able to make payment to before invoice date");
                                    document.getElementById("<%=txtdates.ClientID %>").focus();
                                    return false;
                                }
                            }
                            if (GridViewsup.rows(rowCount).cells(11).children[0].value == "1" || GridViewsup.rows(rowCount).cells(11).children[0].value == "2" || GridViewsup.rows(rowCount).cells(11).children[0].value == "2A") {
                                alert("Invoice is not approved:" + GridViewsup.rows(rowCount).cells(1).innerHTML);
                                return false;
                            }
                        }

                    }
                }
                if (document.getElementById('<%= txtamount.ClientID%>').value == "0" || document.getElementById('<%= txtamount.ClientID%>').value == "") {
                    alert("Amount Required");
                    return false;
                }
            }
            else if (paymenttype == "BankPayment") {
                var SellingAmt = document.getElementById("<%=hfremain.ClientID %>").value;
                var objs = new Array("<%=txtname.ClientID %>", "<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
                var netamount = document.getElementById("<%= txtamt.ClientID%>").value;
                if ((parseFloat(netamount)) <= 0) {
                    alert("Invalid Amount");
                    document.getElementById("<%= txtamt.ClientID%>").value = "";
                    return false;
                }
                if ((parseFloat(netamount)) > (parseFloat(SellingAmt))) {
                    alert("Invoice Amount is not more than asset selling amount");
                    return false;
                }
            }
            document.getElementById("<%=btnSave.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function Amounvalidation(amount) {
            //debugger;
            var paymenttype = document.getElementById("<%=ddlselection.ClientID %>").value;
            var SellingAmt = document.getElementById("<%=hfremain.ClientID %>").value;
            if (paymenttype == "ServiceProvider") {
                var GridView2 = document.getElementById("<%=grd.ClientID %>");
                var originalValue = 0;
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                        if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                            var value = GridView2.rows(rowCount).cells(8).innerText.replace(/,/g, "");
                            if (value != "") {
                                originalValue += parseFloat(value);
                            }
                        }
                    }
                }
                var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                if (parseFloat(SellingAmt) < parseFloat(document.getElementById('<%= txtamount.ClientID%>').value)) {
                    window.alert("Amount is not more than selling Amount");
                    document.getElementById('<%= txtamount.ClientID%>').value = roundValue;
                    return false;
                }
                if (parseFloat(originalValue) < parseFloat(document.getElementById('<%= txtamount.ClientID%>').value)) {
                    window.alert("Invalid");
                    document.getElementById('<%= txtamount.ClientID%>').value = "";
                    return false;
                }
            }
            if (paymenttype == "Supplier") {
                var GridView3 = document.getElementById("<%=grdsup.ClientID %>");
                var originalValue = 0;
                for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                    if (GridView3.rows(rowCount).cells(9).children(0) != null) {
                        if (GridView3.rows(rowCount).cells(9).children(0).checked == true) {
                            var value = GridView3.rows(rowCount).cells(8).innerText.replace(/,/g, "");
                            if (value != "") {
                                originalValue += parseFloat(value);
                            }
                        }
                    }
                }
                var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
                if (parseFloat(SellingAmt) < parseFloat(document.getElementById('<%= txtamount.ClientID%>').value)) {
                    window.alert("Amount is not more than selling Amount");
                    document.getElementById('<%= txtamount.ClientID%>').value = roundValue;
                    return false;
                }
                if (parseFloat(originalValue) < parseFloat(document.getElementById('<%= txtamount.ClientID%>').value)) {
                    window.alert("Invalid");
                    document.getElementById('<%= txtamount.ClientID%>').value = "";
                    return false;
                }
            }
            if (paymenttype == "BankPayment") {
                if (parseFloat(SellingAmt) < parseFloat(document.getElementById('<%= txtamt.ClientID%>').value)) {
                    window.alert("Amount is not more than selling Amount");
                    document.getElementById('<%= txtamt.ClientID%>').value = "";
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript">
        function numericValidation(txtvalue) {

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
                    if (points == 1) {
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
