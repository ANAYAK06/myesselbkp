<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="InvoiceVerfication.aspx.cs"
    Inherits="InvoiceVerfication" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function closepopup() {
            $find('mdlbank').hide();


        }
        function closepopup1() {
            $find('mdlbank').hide();


        }

    </script>
    <script language="javascript">
        function validate() {
            
            var objs = new Array("<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>", "<%=txtindesc.ClientID %>", "<%= txtexcise.ClientID%>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var type = document.getElementById("<%=txtvendortype.ClientID %>").value;
            if (type == "Service provider VAT") {
                var vat = document.getElementById("<%=ddlvatno.ClientID %>").value;
                if (vat == "Select") {
                    alert("VAT No Required");
                    document.getElementById("<%=ddlvatno.ClientID %>").focus();
                    return false;
                }
            }
            if (type == "Service Tax" || type == "Trading Service Tax") {
                var STaxno = document.getElementById("<%=ddlservicetax.ClientID %>").value;
                if (STaxno == "Select") {
                    alert("ServiceTax No Required");
                    document.getElementById("<%=ddlservicetax.ClientID %>").focus();
                    return false;
                }
            }
            if (type == "Manufacturing Service Tax") {
                var Exno = document.getElementById("<%=ddlExcno.ClientID %>").value;
                if (Exno == "Select") {
                    alert("Excise No Required");
                    document.getElementById("<%=ddlExcno.ClientID %>").focus();
                    return false;
                }
            }
            if (type == "Service Provider") {
                var str1 = document.getElementById("<%=txtspinvoicedate.ClientID %>").value;
                var str2 = document.getElementById("<%=txtindtmk.ClientID %>").value;
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
                    alert("Invalid Invoice Making Date");
                    document.getElementById("<%=txtindtmk.ClientID %>").focus();
                    return false;
                } 
            }
            if (document.getElementById("<%=ddlspexcise.ClientID %>") != null) {

                if (document.getElementById("<%=ddlspexcise.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=ddlspexcise.ClientID %>").value == "Select") {
                        alert(" Select  Exices No Required");
                        document.getElementById("<%=ddlspexcise.ClientID %>").focus();

                    }
                }
            }

             if (document.getElementById("<%=txtspexcise.ClientID %>") != null) {

                if (document.getElementById("<%=txtspexcise.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtspexcise.ClientID %>").value == "") {
                        alert("Exicse Duty Required");
                        document.getElementById("<%=txtspexcise.ClientID %>").focus();

                    }
                }
            }
             if (document.getElementById("<%=ddlspservice.ClientID %>") != null) {

                if (document.getElementById("<%=ddlspservice.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=ddlspservice.ClientID %>").value == "Select") {
                        alert(" Select  Service Tax No Required");
                        document.getElementById("<%=ddlspservice.ClientID %>").focus();
                        return false;
                    }
                }
            }
             if (document.getElementById("<%=txtspserviceno.ClientID %>") != null) {

                if (document.getElementById("<%=txtspserviceno.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtspserviceno.ClientID %>").value == "") {
                        alert("Service Tax Required");
                        document.getElementById("<%=txtspserviceno.ClientID %>").focus();
                        return false;
                    }
                }
            }
             if (document.getElementById("<%=ddlspvat.ClientID %>") != null) {

                if (document.getElementById("<%=ddlspvat.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=ddlspvat.ClientID %>").value == "Select") {
                        alert(" Select  VAT Tax No Required");
                        document.getElementById("<%=ddlspvat.ClientID %>").focus();
                        return false;
                    }
                }
            }
             if (document.getElementById("<%=txtspvat.ClientID %>") != null) {

                if (document.getElementById("<%=txtspvat.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtspvat.ClientID %>").value == "") {
                        alert("VAT Tax Required");
                        document.getElementById("<%=txtspvat.ClientID %>").focus();
                        return false;
                    }
                }
            }
             if (document.getElementById("<%=txtserviceedcess.ClientID %>") != null) {

                if (document.getElementById("<%=txtserviceedcess.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtserviceedcess.ClientID %>").value == "") {
                        alert("Edcess  Required");
                        document.getElementById("<%=txtserviceedcess.ClientID %>").focus();
                        return false;
                    }
                }
            }
             if (document.getElementById("<%=txtservichedcess.ClientID %>") != null) {

                if (document.getElementById("<%=txtservichedcess.ClientID %>").disabled == false) {
                    if (document.getElementById("<%=txtservichedcess.ClientID %>").value == "") {
                        alert("EHdcess  Required");
                        document.getElementById("<%=txtservichedcess.ClientID %>").focus();
                        return false;

                    }
                }
            } 
            document.getElementById("<%=btninApprove.ClientID %>").style.display = 'none';
            return true;
        }
        
    </script>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="750px">
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
                                            <tr>
                                                <td valign="top">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="box-a list-a">
                                                                <div class="inner" align="center">
                                                                    <table id="Table2" class="gridview" width="75%" cellspacing="0" cellpadding="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="grid-content" align="center">
                                                                                    <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" align="center"
                                                                                        cellpadding="0" style="background: none;">
                                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grid-content"
                                                                                            HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                            AllowPaging="false" DataKeyNames="id" BorderColor="White" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                                                            EmptyDataText="There is no records" OnRowDeleting="GridView1_RowDeleting1">
                                                                                            <Columns>
                                                                                                <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                                                                <asp:BoundField DataField="id" Visible="false" />
                                                                                                <asp:BoundField DataField="cc_code" ItemStyle-Width="150px" HeaderText="CC Code"
                                                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                                                <asp:BoundField DataField="Description" ItemStyle-HorizontalAlign="Left" HeaderText="Description"
                                                                                                    ItemStyle-Width="400px" />
                                                                                                <asp:BoundField DataField="Date" ItemStyle-HorizontalAlign="Right" HeaderText="Date"
                                                                                                    ItemStyle-Width="100px" />
                                                                                                <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                                                                    DeleteImageUrl="~/images/Delete.jpg" />
                                                                                            </Columns>
                                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                        </asp:GridView>
                                                                                        <cc1:ModalPopupExtender ID="mdlpopbank" BehaviorID="mdlbank" runat="server" TargetControlID="btnbank"
                                                                                            PopupControlID="pnlbank" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                                        <asp:Panel ID="pnlbank" runat="server" Style="display: none;">
                                                                                            <table id="tblindent" runat="server" width="920px" border="0" align="center" cellpadding="0"
                                                                                                cellspacing="0">
                                                                                                <tr>
                                                                                                    <td width="13" valign="bottom">
                                                                                                        <img src="images/leftc.jpg">
                                                                                                    </td>
                                                                                                    <td class="pop_head" align="left" id="approveind" runat="server">
                                                                                                        <div class="popclose">
                                                                                                            <img width="20" height="20" border="0" onclick="closepopup1();" src="images/mpcancel.png">
                                                                                                        </div>
                                                                                                        Verify Invoice
                                                                                                    </td>
                                                                                                    <td width="13" valign="bottom">
                                                                                                        <img src="images/rightc.jpg">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td bgcolor="#FFFFFF">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td height="200" valign="top" class="popcontent">
                                                                                                        <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                                                            height: 350px;">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                                                                                                                        <ProgressTemplate>
                                                                                                                            <asp:Panel ID="pnl" runat="server" CssClass="popup-div-background">
                                                                                                                                <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                                                                                                    left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                                                                                    <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                                                                                </div>
                                                                                                                            </asp:Panel>
                                                                                                                        </ProgressTemplate>
                                                                                                                    </asp:UpdateProgress>
                                                                                                                    <table style="vertical-align: middle;" align="center">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                    <tbody>
                                                                                                                                        <tr>
                                                                                                                                            <td valign="top">
                                                                                                                                                <table border="0" class="fields" width="100%">
                                                                                                                                                    <tbody>
                                                                                                                                                        <tr>
                                                                                                                                                            <td colspan="4" valign="top" class=" item-group" for="" width="100%">
                                                                                                                                                                <table border="0" class="fields" width="100%">
                                                                                                                                                                    <tbody>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label>
                                                                                                                                                                                    Vendor Type
                                                                                                                                                                                </label>
                                                                                                                                                                                :
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="31%" valign="middle" class="item item-m2o">
                                                                                                                                                                                <asp:TextBox ID="txtvendortype" runat="server" Width="150px" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label class="help">
                                                                                                                                                                                    Vendor Id
                                                                                                                                                                                </label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                <asp:TextBox ID="txtvendorid" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label for="journal_id">
                                                                                                                                                                                    Vendor Name
                                                                                                                                                                                </label>
                                                                                                                                                                                :
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                <asp:TextBox ID="txtvendorname" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label for="type">
                                                                                                                                                                                    CC Code
                                                                                                                                                                                </label>
                                                                                                                                                                                :
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="item item-char" valign="middle">
                                                                                                                                                                                <span class="filter_item">
                                                                                                                                                                                    <asp:TextBox ID="ddlinvoicecc" runat="server" Enabled="false"></asp:TextBox>
                                                                                                                                                                                </span>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label for="name">
                                                                                                                                                                                    DCA Code
                                                                                                                                                                                </label>
                                                                                                                                                                                :
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="item item-char" valign="middle">
                                                                                                                                                                                <span class="filter_item">
                                                                                                                                                                                    <asp:TextBox ID="ddlinvoicedca" runat="server" Enabled="false"></asp:TextBox>
                                                                                                                                                                                </span>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label for="reference" class="help">
                                                                                                                                                                                    Sub DCA Code
                                                                                                                                                                                </label>
                                                                                                                                                                                :
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="item item-char" valign="middle">
                                                                                                                                                                                <span class="filter_item">
                                                                                                                                                                                    <asp:TextBox ID="ddlinvoicesub" runat="server" Enabled="false"></asp:TextBox>
                                                                                                                                                                                </span>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </tbody>
                                                                                                                                                                </table>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td colspan="4" valign="top" class=" item-notebook" width="100%">
                                                                                                                                                                <div id="_notebook_1" class="notebook" style="display: block;">
                                                                                                                                                                    <div class="notebook-tabs">
                                                                                                                                                                        <div class="right scroller">
                                                                                                                                                                        </div>
                                                                                                                                                                        <div class="left scroller">
                                                                                                                                                                        </div>
                                                                                                                                                                        <ul class="notebook-tabs-strip">
                                                                                                                                                                            <li class="notebook-tab notebook-page notebook-tab-active" title="" id="none"><span
                                                                                                                                                                                class="tab-title"><span>Invoice Info</span></span></li><li class="notebook-tab notebook-page"
                                                                                                                                                                                    title="" style="display: none;"><span class="tab-title"><span>Journal Items</span></span></li></ul>
                                                                                                                                                                    </div>
                                                                                                                                                                    <div class="notebook-pages">
                                                                                                                                                                        <div class="notebook-page notebook-page-active">
                                                                                                                                                                            <div>
                                                                                                                                                                                <table border="0" class="fields" width="100%">
                                                                                                                                                                                    <tbody>
                                                                                                                                                                                        <tr id="trsernum" runat="server">
                                                                                                                                                                                            <td align="center" style="width: 250px;" id="Td1" runat="server">
                                                                                                                                                                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                                                                                                                ServiceTax No:
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td align="center" style="width: 200px;">
                                                                                                                                                                                                <asp:DropDownList ID="ddlservicetax" runat="server" ToolTip="ServiceTax No" Width="200px"
                                                                                                                                                                                                    CssClass="esddown">
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="other" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Po NO:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtpo" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Invoice No:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtin" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Invoice Date:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtindt" ToolTip="Invoice Date" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtindt"
                                                                                                                                                                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtindt">
                                                                                                                                                                                                </cc1:CalendarExtender>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="Serviceprovider" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Basic Value:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtSpbasic" runat="server" onkeyup="SPTotal();" CssClass="char"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="trexcno" runat="server">
                                                                                                                                                                                            <td align="center" style="width: 250px;" id="lblex" runat="server">
                                                                                                                                                                                                Excise No :
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td align="center" style="width: 200px;">
                                                                                                                                                                                                <asp:DropDownList ID="ddlExcno" runat="server" ToolTip="Excise No" Width="200px"
                                                                                                                                                                                                    CssClass="esddown">
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="Supplier" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Basic Value:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtSupBasic" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Sales Tax:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtsalestax" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Total:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txttotal" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="Excise" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Exice Duty:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtexcise" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="trvatno" runat="server">
                                                                                                                                                                                            <td align="center" style="width: 250px;" id="Td2" runat="server">
                                                                                                                                                                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                                                                                                                VAT No:
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td align="center" style="width: 200px;">
                                                                                                                                                                                                <asp:DropDownList ID="ddlvatno" runat="server" ToolTip="VAT No" Width="200px" CssClass="esddown">
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="Trvattotal" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <asp:Label ID="Label3" runat="server" Text="VAT Tax:"></asp:Label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtvattotal" runat="server" CssClass="char" onkeyup="SPTotal();"
                                                                                                                                                                                                    ToolTip="Total"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                      
                                                                                                                                                                                        <tr id="Servicetax" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtservicetax" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Edcess:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="25%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txted" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Hedcess:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="25%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txthed" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="deduction" runat="server">
                                                                                                                                                                                            <td colspan="2" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                                                                                                                <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                                                                                                    Deductions</div>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="deduction1" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Tds:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txttds" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Retention:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtretention" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Edc:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtedc" runat="server" CssClass="char" Enabled="false" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="deduction2" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Advance:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtAdvance" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Hold:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txthold" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Anyother:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtother" runat="server" CssClass="char" onkeyup="SPTotal();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="deduction3" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Description:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td valign="middle" class="item item-char" colspan="3">
                                                                                                                                                                                                <asp:TextBox ID="txtindesc" runat="server" Width="320px" CssClass="char" TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Net Amount:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtnetAmount" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                    </tbody>
                                                                                                                                                                                </table>
                                                                                                                                                                                <table id="serviceproviderpo" border="0" class="fields" width="100%">
                                                                                                                                                                                    <tbody>
                                                                                                                                                                                        <tr id="sppono" runat="server">
                                                                                                                                                                                            <td colspan="6">
                                                                                                                                                                                                <table>
                                                                                                                                                                                                    <tr>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Po NO:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                                        <td width="25%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtsppono" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                                    </tr>
                                                                                                                                                                                                    <tr>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Invoice No:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspinvoice" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Invoice Date:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspinvoicedate" ToolTip="Invoice Date" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindt"
                                                                                                                                                                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtindt">
                                                                                                                                                                                                </cc1:CalendarExtender>
                                                                                                                                                                                                        </td>
                                                                                                                                                                                                        <td class="label" width="1%">
                                                                                                                                                                                                            <label class="help">
                                                                                                                                                                                                                Inv Making Date:
                                                                                                                                                                                                            </label>
                                                                                                                                                                                                        </td>
                                                                                                                                                                                                        <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                            <asp:TextBox ID="txtindtmk" ToolTip="Inv Making Date" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtindtmk"
                                                                                                                                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtindtmk">
                                                                                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                                                                                        </td>
                                                                                                                                                                                                    </tr>
                                                                                                                                                                                                </table>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="spbasic" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Basic Value:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspbacis" runat="server" onkeyup="SPTotal1();" CssClass="char"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="spservice" runat="server">
                                                                                                                                                                                            <td align="center" style="width: 250px;" id="Td4" runat="server">
                                                                                                                                                                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                                                                                                                ServiceTax No:
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td align="center" style="width: 200px;">
                                                                                                                                                                                                <asp:DropDownList ID="ddlspservice" runat="server" ToolTip="ServiceTax No" Width="200px"
                                                                                                                                                                                                    CssClass="esddown">
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td id="Td3" align="center" style="width: 250px;" runat="server">
                                                                                                                                                                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                                                                                                                ServiceTax:
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspserviceno" runat="server" CssClass="char"  onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="spexcise" runat="server">
                                                                                                                                                                                            <td align="center" style="width: 250px;" id="Td6" runat="server">
                                                                                                                                                                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                                                                                                                ExiciseTax No:
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td align="center" style="width: 200px;">
                                                                                                                                                                                                <asp:DropDownList ID="ddlspexcise" runat="server" ToolTip="ServiceTax No" Width="200px"
                                                                                                                                                                                                    CssClass="esddown">
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td id="Td7" align="center" style="width: 250px;" runat="server">
                                                                                                                                                                                                <asp:Label ID="Label11" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                                                                                                                ExiciseTax:
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspexcise" runat="server" CssClass="char" onkeyup="SPTotal1();"
                                                                                                                                                                                                    ToolTip="ServiceTax"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="spvat" runat="server">
                                                                                                                                                                                            <td align="center" style="width: 250px;" id="Td5" runat="server">
                                                                                                                                                                                                <asp:Label ID="Label8" runat="server" CssClass="eslbl"></asp:Label>
                                                                                                                                                                                                VAT No:
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td align="center" style="width: 200px;">
                                                                                                                                                                                                <asp:DropDownList ID="ddlspvat" runat="server" ToolTip="VAT No" Width="200px" CssClass="esddown">
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <asp:Label ID="Label9" runat="server" Text="VAT Tax:"></asp:Label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspvat" runat="server" CssClass="char" onkeyup="SPTotal1();" ToolTip="Total"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="servicedecess" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                              <asp:Label ID="lblspedcess" runat="server" Text=" Edcess:"></asp:Label>
                                                                                                                                                                                    
                                                                                                                                                                                           </td>
                                                                                                                                                                                            <td width="25%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtserviceedcess" runat="server" CssClass="char" onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <asp:Label ID="lblsphedcess" runat="server" Text="Hedcess:"></asp:Label>
                                                                                                                                                                                   
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="25%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtservichedcess" runat="server" CssClass="char" onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="spdecess" runat="server">
                                                                                                                                                                                            <td colspan="2" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                                                                                                                <div class="separator horizontal" style="font-size: 10pt">
                                                                                                                                                                                                    Deductions</div>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="spedecess1" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Tds:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtsptds" runat="server" CssClass="char" onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Retention:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtsprention" runat="server" CssClass="char" onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Edc:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspedc" runat="server" CssClass="char" Enabled="false" onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="spdecess2" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Advance:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspadvance" runat="server" CssClass="char" onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Hold:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtsphold" runat="server" CssClass="char" onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Anyother:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspanother" runat="server" CssClass="char" onkeyup="SPTotal1();"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="spedcess3" runat="server">
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Description:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td valign="middle" class="item item-char" colspan="3">
                                                                                                                                                                                                <asp:TextBox ID="txtspdeccription" runat="server" Width="320px" CssClass="char" TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                                <label class="help">
                                                                                                                                                                                                    Net Amount:
                                                                                                                                                                                                </label>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td valign="middle" class="item item-char">
                                                                                                                                                                                                <asp:TextBox ID="txtspnetamount" Enabled="false" runat="server" onkeyup="SPTotal1();"
                                                                                                                                                                                                    CssClass="char"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                    </tbody>
                                                                                                                                                                                </table>
                                                                                                                                                                            </div>
                                                                                                                                                                        </div>
                                                                                                                                                                    </div>
                                                                                                                                                                </div>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                    </tbody>
                                                                                                                                                </table>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </tbody>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td align="center">
                                                                                                                                <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                                                                                                                <asp:Button ID="btninApprove" runat="server" Text="Verify" CssClass="button" OnClick="btninApprove_Click"
                                                                                                                                    OnClientClick=" return validate();"/>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td bgcolor="#FFFFFF">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Button runat="server" ID="btnbank" Style="display: none" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="pagerbar">
                                                                                <td class="pagerbar-cell" align="right">
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
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
            </td>
        </tr>
    </table>
   
    <script language="javascript" type="text/javascript">


        function SPTotal1() {
            var category = document.getElementById("<%=txtvendortype.ClientID %>").value;

            if (category == "Service Provider") {
                var originalValue = 0;
                var originalValue1 = 0;
                var spservice1 = 0;
                var spexicse1 = 0;
                var spdecdess = document.getElementById("<%=txtserviceedcess.ClientID %>").value;
                var sphdeess = document.getElementById("<%=txtservichedcess.ClientID %>").value;
                var spvat = document.getElementById("<%=txtspvat.ClientID %>").value;
                if (document.getElementById("<%=txtspserviceno.ClientID %>") != null) {
                    spservice1 = document.getElementById("<%=txtspserviceno.ClientID %>").value;
                }
                if (document.getElementById("<%=txtspexcise.ClientID %>") != null) {
                    spexicse1 = document.getElementById("<%=txtspexcise.ClientID %>").value;
                        
                }
                var sptds = document.getElementById("<%=txtsptds.ClientID %>").value;
                var sprett = document.getElementById("<%=txtsprention.ClientID %>").value;
                var spadv = document.getElementById("<%=txtspadvance.ClientID %>").value;
                var spholld = document.getElementById("<%=txtsphold.ClientID %>").value;
                var spother = document.getElementById("<%=txtspanother.ClientID %>").value;

                var spbasic = document.getElementById("<%=txtspbacis.ClientID %>").value;


                if (spbasic == "") {
                    spbasic = 0;
                }
                if (sptds == "") {
                    sptds = 0;
                }
                if (sprett == "") {
                    sprett = 0;
                }

                if (spadv == "") {
                    spadv = 0;
                }
                if (spholld == "") {
                    spholld = 0;
                }
                if (spother == "") {
                    spother = 0;
                }

                if (spdecdess == "") {
                    spdecdess = 0;
                }
                if (sphdeess == "") {
                    sphdeess = 0;
                }
                if (spvat == "") {
                    spvat = 0;
                }

                if (spexicse1 == "") {
                    spexicse1 = 0;
                }


                if (spservice1 == "") {
                    spservice1 = 0;
                }

                originalValue1 = eval(((parseInt(spbasic)) + parseInt(spdecdess) + parseInt(sphdeess) + parseInt(spvat) + parseInt(spservice1) + parseInt(spexicse1) - (parseInt(sptds) + parseInt(sprett) + parseInt(spadv) + parseInt(spholld) + parseInt(spother))));
                var roundValue3 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);

                document.getElementById('<%=txtspnetamount.ClientID%>').value = roundValue3;
            }
        }
                    
        
    </script>
    <script language="javascript" type="text/javascript">

        function SPTotal() {
            var category = document.getElementById("<%=txtvendortype.ClientID %>").value;
            var originalValue = 0;
            var originalValue1 = 0;

            if (category == "Trading Service Tax") {
                var Servicetax = document.getElementById("<%=txtservicetax.ClientID %>").value;
                var edcess = document.getElementById("<%=txted.ClientID %>").value;
                var hedcess = document.getElementById("<%=txthed.ClientID %>").value;
                if (Servicetax == "") {
                    Servicetax = 0;
                }
                if (edcess == "") {
                    edcess = 0;
                }
                if (hedcess == "") {
                    hedcess = 0;
                }
                originalValue = eval((parseInt(Servicetax) + parseInt(edcess) + parseInt(hedcess)));
                var roundValue2 = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);

                document.getElementById('<%= txtnetAmount.ClientID%>').value = roundValue2;

            }
           
            
            else if (category == "Supplier") {

                var Basic = document.getElementById("<%=txtSupBasic.ClientID %>").value;
                var Salestax = document.getElementById("<%=txtsalestax.ClientID %>").value;
                var total = document.getElementById("<%=txttotal.ClientID %>").value;
                var tds1 = document.getElementById("<%=txttds.ClientID %>").value;
                var ret1 = document.getElementById("<%=txtretention.ClientID %>").value;
                var adv1 = document.getElementById("<%=txtAdvance.ClientID %>").value;
                var hold1 = document.getElementById("<%=txthold.ClientID %>").value;
                var other1 = document.getElementById("<%=txtother.ClientID %>").value;
                if (Basic == "") {
                    Basic = 0;
                }
                if (Salestax == "") {
                    Salestax = 0;
                }
                if (total == "") {
                    total = 0;
                }
                if (tds1 == "") {
                    tds1 = 0;
                }
                if (ret1 == "") {
                    ret1 = 0;
                }
                if (adv1 == "") {
                    adv1 = 0;
                }
                if (hold1 == "") {
                    hold1 = 0;
                }
                if (other1 == "") {
                    other1 = 0;
                }
                originalValue1 = eval(((parseInt(Basicvalue)) - (parseInt(tds1) + parseInt(ret1) + parseInt(adv1) + parseInt(hold1) + parseInt(Salestax) + parseInt(other1))));
                var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
                var roundValue5 = eval((parseInt(Basicvalue)) + (parseInt(Salestax)));
                document.getElementById('<%= txtnetAmount.ClientID%>').value = roundValue1;
                document.getElementById('<%= txttotal.ClientID%>').value = roundValue5;

            }
            else {

                document.getElementById('<%= txtnetAmount.ClientID%>').value = document.getElementById('<%= txtexcise.ClientID%>').value;

            }
        }
                   
                                                 
                                         
    </script>
</asp:Content>
