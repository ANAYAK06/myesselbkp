<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="VendorInvoice.aspx.cs"
    Inherits="VendorInvoice" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Java_Script/validations.js" type="text/javascript"></script>
    <script language="javascript">
        function STaxcalculate(itemcode, amount) {
            grd = document.getElementById("<%=gridcmc.ClientID %>");

            var samt = 0;
            var amt = 0;
            var stotal = 0;
            if (itemcode.substring(0, 1) == "1") {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (grd.rows(rowCount).cells[1].innerHTML.substring(0, 8) == itemcode.substring(0, 8)) {

                        grd.rows(rowCount).cells(10).children[0].value = amount;
                        samt = parseFloat(grd.rows(rowCount).cells(8).children[0].value) * Number((Number(grd.rows(rowCount).cells[10].children[0].value)) / 100);
                        grd.rows(rowCount).cells(11).children[0].innerHTML = samt;
                        stotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);

                    }
                    else {
                        stotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);


                    }

                }
                grd.rows[grd.rows.length - 1].cells[11].innerHTML = stotal;
                document.getElementById("<%=txttax.ClientID %>").value = stotal;
                document.getElementById("<%=txttotal.ClientID %>").value = parseFloat(stotal) + Number(grd.rows[grd.rows.length - 1].cells[9].innerHTML);
                document.getElementById("<%=txtnetAmount.ClientID %>").value = parseFloat(stotal) + Number(grd.rows[grd.rows.length - 1].cells[9].innerHTML);

            }
            else {


                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                    if (Number(grd.rows(rowCount).cells[10].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(10).children[0].value)) {
                            amt = (Number(grd.rows(rowCount).cells[7].innerHTML)) * (Number(grd.rows(rowCount).cells[8].children[0].value));

                            samt = amt * (Number(grd.rows(rowCount).cells[10].children[0].value) / 100);
                            grd.rows(rowCount).cells(11).children[0].innerHTML = samt;
                            stotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);
                        }
                    }
                    else {
                        stotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);
                    }

                }
                grd.rows[grd.rows.length - 1].cells[11].innerHTML = stotal;
                document.getElementById("<%=txttax.ClientID %>").value = stotal;
                document.getElementById("<%=txttotal.ClientID %>").value = parseFloat(stotal) + Number(grd.rows[grd.rows.length - 1].cells[9].innerHTML);
                document.getElementById("<%=txtnetAmount.ClientID %>").value = parseFloat(stotal) + Number(grd.rows[grd.rows.length - 1].cells[9].innerHTML);

            }
        }


    </script>
    <script language="javascript">
        function calculate(itemcode, amount) {
            grd = document.getElementById("<%=gridcmc.ClientID %>");
            var amt = 0;

            var total = 0;
            var samt = 0;

            var stotal = 0;
            if (itemcode.substring(0, 1) == "1") {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (grd.rows(rowCount).cells[1].innerHTML.substring(0, 8) == itemcode.substring(0, 8)) {

                        grd.rows(rowCount).cells(8).children[0].value = amount;
                        amt = Number(grd.rows(rowCount).cells[7].innerHTML) * amount;
                        grd.rows(rowCount).cells(9).children[0].value = amt;
                        total += Number(grd.rows(rowCount).cells(9).children[0].value);
                        samt = parseFloat(grd.rows(rowCount).cells(8).children[0].value) * Number((Number(grd.rows(rowCount).cells[10].children[0].value)) / 100);
                        grd.rows(rowCount).cells(11).children[0].innerHTML = samt;
                        stotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);

                    }
                    else {
                        total += Number(grd.rows(rowCount).cells(9).children[0].value);

                        stotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);

                    }

                }
                grd.rows[grd.rows.length - 1].cells[9].innerHTML = total;
                document.getElementById("<%=txtbasic.ClientID %>").value = total;
                document.getElementById("<%=txttotal.ClientID %>").value = parseFloat(total) + parseFloat(stotal);
                document.getElementById("<%=txtnetAmount.ClientID %>").value = parseFloat(total) + parseFloat(stotal);
                grd.rows[grd.rows.length - 1].cells[11].innerHTML = stotal;
                document.getElementById("<%=txttax.ClientID %>").value = stotal;
            }
            else {


                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                    if (Number(grd.rows(rowCount).cells[9].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(8).children[0].value)) {
                            amt = (Number(grd.rows(rowCount).cells[7].innerHTML)) * (Number(grd.rows(rowCount).cells[8].children[0].value));
                            grd.rows(rowCount).cells(9).children[0].value = amt;
                            total += Number(grd.rows(rowCount).cells(9).children[0].value);
                            samt = amt * (Number(grd.rows(rowCount).cells[10].children[0].value) / 100);
                            grd.rows(rowCount).cells(11).children[0].innerHTML = samt;
                            stotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);

                        }
                    }
                    else {
                        total += Number(grd.rows(rowCount).cells(9).children[0].value);
                        stotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);

                    }

                }
                grd.rows[grd.rows.length - 1].cells[9].innerHTML = total;
                document.getElementById("<%=txtbasic.ClientID %>").value = total;
                grd.rows[grd.rows.length - 1].cells[11].innerHTML = stotal;
                document.getElementById("<%=txttax.ClientID %>").value = stotal;
                document.getElementById("<%=txttotal.ClientID %>").value = parseFloat(total) + parseFloat(stotal);
                document.getElementById("<%=txtnetAmount.ClientID %>").value = parseFloat(total) + parseFloat(stotal);
            }
        }


    </script>
    <script language="javascript">
        function validateexcise() {
            document.getElementById("<%=VATcheck.ClientID %>").checked = false;
            document.getElementById("<%=excisecheck.ClientID %>").checked = false;
            var response = confirm("IF Excise Invoice Exist check EXcise Invoice ");
            if (response) {

                document.getElementById("<%=ddlExcno.ClientID %>").disabled = false;
                document.getElementById("<%=txtexduty.ClientID%>").disabled = false;
                document.getElementById("<%=txtEdc.ClientID%>").disabled = false;
                document.getElementById("<%=txtHEdc.ClientID%>").disabled = false;
                document.getElementById("<%=excisecheck.ClientID %>").checked = true;
                var response = confirm("IF VAT Invoice Exist check VAT Invoice ");
                if (response) {

                    document.getElementById("<%=ddlvatno.ClientID %>").disabled = false;
                    document.getElementById("<%=txtvatamt.ClientID%>").disabled = false;
                    document.getElementById("<%=VATcheck.ClientID %>").checked = true;
                    return false;
                }

                return false;
            }
            else {
                var response = confirm("IF VAT Invoice Exist check VAT Invoice ");
                if (response) {

                    document.getElementById("<%=ddlvatno.ClientID %>").disabled = false;
                    document.getElementById("<%=txtvatamt.ClientID%>").disabled = false;
                    document.getElementById("<%=VATcheck.ClientID %>").checked = true;
                    return false;
                }
                return false;
            }

        }
     
    
    </script>
    <script language="javascript">
        function validate() {


            var objs = new Array("<%=ddlvendortype.ClientID %>", "<%=ddlmrr.ClientID %>");


            if (!CheckInputs(objs)) {
                return false;
            }
            var GridView1 = document.getElementById("<%=gridcmc.ClientID %>");



            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(8).children[0].value == "") {
                        window.alert("Enter new basic price");
                        GridView1.rows(rowCount).cells(8).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(10).children[0].value == "" && document.getElementById("<%=VATcheck.ClientID %>").checked == false) {
                        window.alert("Enter Sales Tax");
                        GridView1.rows(rowCount).cells(10).children[0].focus();
                        return false;
                    }
                }
            }

            var obj = new Array("<%=txtin.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtfre.ClientID %>", "<%=txtfre.ClientID %>", "<%=txtinsurance.ClientID %>");


            if (!CheckInputs(obj)) {
                return false;
            }
            var str1 = document.getElementById("<%=txtindt.ClientID %>").value;
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
                document.getElementById("<%=txtindtmk.ClientID %>").value = "";
                document.getElementById("<%=txtindtmk.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=excisecheck.ClientID %>").checked == true) {
                var excise = new Array("<%=ddlExcno.ClientID %>", "<%=txtexduty.ClientID %>", "<%=txtEdc.ClientID %>", "<%=txtHEdc.ClientID %>");


                if (!CheckInputs(excise)) {
                    return false;
                }
            }
            if (document.getElementById("<%=VATcheck.ClientID %>").checked == true) {
                var VAT = new Array("<%=ddlvatno.ClientID %>", "<%=txtvatamt.ClientID %>");


                if (!CheckInputs(VAT)) {
                    return false;
                }
            }

            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
    
    </script>
    <script language="javascript">

        function Restrictcomma(evt) {

            var e = evt ? evt : event;
            var charCode = e.keyCode ? e.keyCode : e.charCode;

            if (charCode == 44 || charCode == 39)
                return false;
            else
                return true;
        }


    </script>
    <script language="javascript">
        function Total() {
            var adv = document.getElementById("<%=txtAdvance.ClientID %>").value;
            var hold = document.getElementById("<%=txthold.ClientID %>").value;
            var other = document.getElementById("<%=txtother.ClientID %>").value;
            var originalValue1 = 0;
            if (adv == "") {
                adv = 0;
            }
            if (hold == "") {
                hold = 0;
            }
            if (other == "") {
                other = 0;
            }

            var i = parseFloat(Number(document.getElementById('<%= txtbasic.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txttax.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txtfre.ClientID%>').value)) + parseFloat(Number(document.getElementById('<%= txtinsurance.ClientID%>').value));
            originalValue1 = eval(parseFloat(i - (parseFloat(adv) + parseFloat(hold) + parseFloat(other))));
            var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= txtnetAmount.ClientID%>').value = roundValue1;
            document.getElementById('<%= txttotal.ClientID%>').value = i;
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
                <table style="vertical-align: middle;" align="center">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top">
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
                                                    <table border="0" class="fields" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="4" valign="top" class=" item-group" for="" width="100%">
                                                                    <table border="0" class="fields" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td width="1%">
                                                                                    <td class="label" width="1%">
                                                                                        <asp:Label ID="Label11" runat="server" Text="ExciseDuty No :"></asp:Label>
                                                                                    </td>
                                                                                </td>
                                                                                <td valign="middle" class="item item-char">
                                                                                    <asp:CheckBox ID="excisecheck" runat="server" onclick="javascript: return false;" />
                                                                                </td>
                                                                                <td class="label" width="1%">
                                                                                    <asp:Label ID="Label6" runat="server" Text="VAT No :"></asp:Label>
                                                                                </td>
                                                                                <td valign="middle" class="item item-char">
                                                                                    <asp:CheckBox ID="VATcheck" runat="server" onclick="javascript: return false;" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label>
                                                                                        Vendor Type
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td width="31%" valign="middle" class="item item-m2o">
                                                                                    <asp:DropDownList ID="ddlvendortype" CssClass="filter_item" ToolTip="Vendor Type"
                                                                                        runat="server">
                                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Trade Purchasing</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Supplier</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="label" width="1%">
                                                                                    <label>
                                                                                        MRR No
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td width="31%" valign="middle" class="item item-m2o">
                                                                                    <asp:DropDownList ID="ddlmrr" CssClass="filter_item" ToolTip="MRR No" runat="server"
                                                                                        OnSelectedIndexChanged="ddlmrr_SelectedIndexChanged" AutoPostBack="true" onchange="validateexcise();">
                                                                                    </asp:DropDownList>
                                                                                    <cc1:CascadingDropDown ID="cascmrr" runat="server" TargetControlID="ddlmrr" ServicePath="cascadingDCA.asmx"
                                                                                        Category="mrr" LoadingText="Please Wait" ServiceMethod="MRRNo" PromptText="Select MRR No">
                                                                                    </cc1:CascadingDropDown>
                                                                                </td>
                                                                                <td class="label" width="1%">
                                                                                    <label class="help">
                                                                                        Vendor Id
                                                                                    </label>
                                                                                </td>
                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                    <asp:TextBox ID="txtvendorid" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label for="journal_id">
                                                                                        Vendor Name
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                    <asp:TextBox ID="txtvendorname" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                </td>
                                                                                <td class="label" width="1%">
                                                                                    <label for="name">
                                                                                        DCA Code
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td class="item item-char" valign="middle">
                                                                                    <span class="filter_item">
                                                                                        <asp:TextBox ID="txtdca" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
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
                                                                                        <asp:TextBox ID="txtsubdca" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="CST" runat="server">
                                                                <td colspan="2">
                                                                    <asp:GridView ID="gridcmc" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                        PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                        DataKeyNames="id" ShowFooter="true" OnRowDataBound="gridcmc_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="100px" />
                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                            <asp:BoundField DataField="Quantity" HeaderText="Receieved Qty" ItemStyle-Width="25px" />
                                                                            <asp:TemplateField HeaderText="New Purchased Price">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtbasic" runat="server" Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtamount" runat="server" Width="75px" Enabled="false"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sales Tax %">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtsalestax" runat="server" Width="75px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sales Tax Amt">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblamt" runat="server" Text=""></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                        <HeaderStyle CssClass="grid-header" />
                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                                    </asp:GridView>
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
                                                                                            <tr align="left">
                                                                                                <td class="label" width="1%" colspan="1">
                                                                                                    <label class="help">
                                                                                                        Po NO:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char" colspan="2">
                                                                                                    <asp:TextBox ID="txtpo" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Invoice No:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtin" runat="server" ToolTip="Invoice No" onkeypress="javascript:return Restrictcomma(event);"
                                                                                                        CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Invoice Date:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtindt" runat="server" ToolTip="Invoice Date" CssClass="char"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtindt"
                                                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                        PopupButtonID="txtindt">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Invoice Making Date:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtindtmk" runat="server" ToolTip="Invoice Making Date" CssClass="char"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindtmk"
                                                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                        PopupButtonID="txtindtmk">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Basic Value:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtbasic" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                                                        runat="server" ToolTip="Basic Value" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Freight :
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtfre" runat="server" CssClass="char" ToolTip="Freight" onkeyup="Total();"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Insurance :
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtinsurance" runat="server" CssClass="char" ToolTip="Insurance"
                                                                                                        onkeyup="Total();"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <asp:Label ID="lbltax" CssClass="label" runat="server" Text="Sales Tax:"></asp:Label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txttax" runat="server" onkeyup="Total();" onKeyPress="javascript: return false;"
                                                                                                        onKeyDown="javascript: return false;" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Total:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txttotal" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                                                        runat="server" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                        Deductions</div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Advance:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtAdvance" runat="server" onkeyup="Total();" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Hold:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txthold" runat="server" onkeyup="Total();" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Anyother:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtother" runat="server" onkeyup="Total();" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Description:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="item item-char" colspan="3" valign="middle">
                                                                                                    <asp:TextBox ID="txtindesc" runat="server" CssClass="char" TextMode="MultiLine" ToolTip="Description"
                                                                                                        Width="320px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Net Amount:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtnetAmount" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                                                        runat="server" ToolTip="NetAmount" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                        <asp:Label ID="Label3" runat="server" Text="ExciseDuty Invoice"></asp:Label>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td width="1%">
                                                                                                    <label class="help">
                                                                                                        Excise No:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="item item-char" valign="middle">
                                                                                                    <asp:DropDownList ID="ddlExcno" runat="server" ToolTip="Excise No">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <label class="help">
                                                                                                        Excise Duty :
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="item item-char" valign="middle">
                                                                                                    <asp:TextBox ID="txtexduty" runat="server" CssClass="char" onkeyup="Total();" ToolTip="Excise Duty"></asp:TextBox>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <label class="help">
                                                                                                        EDCess :
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="item item-char" valign="middle">
                                                                                                    <asp:TextBox ID="txtEdc" runat="server" CssClass="char" onkeyup="Total();" ToolTip="EDcess"></asp:TextBox>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <label class="help">
                                                                                                        HEDCess :
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="item item-char" valign="middle">
                                                                                                    <asp:TextBox ID="txtHEdc" runat="server" CssClass="char" onkeyup="Total();" ToolTip="HEDCess"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6" style="height: 10px" valign="middle" width="100%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6" style="border-bottom: 1px solid #525254;" valign="middle" width="100%">
                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                        <asp:Label ID="Label5" runat="server" Text="VAT Invoice"></asp:Label>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <asp:Label ID="Label7" runat="server" Text="VAT No :"></asp:Label>
                                                                                                </td>
                                                                                                <td class="item item-char" valign="middle">
                                                                                                    <asp:DropDownList ID="ddlvatno" runat="server" ToolTip="VAT No">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:Label ID="Label8" runat="server" Text="VAT Amount :"></asp:Label>
                                                                                                </td>
                                                                                                <td class="item item-char" valign="middle">
                                                                                                    <asp:TextBox ID="txtvatamt" runat="server" CssClass="char" onkeyup="Total();" ToolTip="VAT Amount"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6" style="height: 10px" valign="middle" width="100%">
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
                                                    <table id="tblsbmt" align="center" class="estbl" width="100%" runat="server">
                                                        <tr align="center">
                                                            <td align="center">
                                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnsubmit_Click"
                                                                    OnClientClick="javascript:return validate();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
