<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VendorInvoiceNew.aspx.cs" Inherits="VendorInvoiceNew" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Java_Script/validations.js" type="text/javascript"></script>
    <script language="javascript">
        var maingridtotal = 0;
        function calculate() {
        //debugger;
            grd = document.getElementById("<%=gridcmc.ClientID %>");
            grdother = document.getElementById("<%=gvanyother.ClientID %>");
            grddeduction = document.getElementById("<%=gvdeduction.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value;
            var ftexcise = document.getElementById("<%=hfexcisefooter.ClientID %>").value;
            var ftsales = document.getElementById("<%=hfsalestaxamt.ClientID %>").value;
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value;
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value;
            var amt = 0;
            var total = 0;
            var samt = 0;
            var eamt = 0;
            var etotal = 0;
            var stotal = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[9].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(8).children[0].value)) {
                            amt = (Number(grd.rows(rowCount).cells[7].innerHTML)) * (Number(grd.rows(rowCount).cells[8].children[0].value));
                            grd.rows(rowCount).cells(9).children[0].value = Math.round(amt * Math.pow(10, 2)) / Math.pow(10, 2);
                            total += Number(grd.rows(rowCount).cells(9).children[0].value);

                            eamt = amt * (Number(grd.rows(rowCount).cells[10].children[0].value) / 100);
                            grd.rows(rowCount).cells(11).children[0].innerHTML = Math.round(eamt * Math.pow(10, 2)) / Math.pow(10, 2);

                            etotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);

                            samt = ((amt + eamt) * (Number(grd.rows(rowCount).cells[12].children[0].value) / 100));
                            grd.rows(rowCount).cells(13).children[0].innerHTML = Math.round(samt * Math.pow(10, 2)) / Math.pow(10, 2);
                            stotal += Number(grd.rows(rowCount).cells(13).children[0].innerHTML);
                        }
                    }
                    else {
                        total += Number(grd.rows(rowCount).cells(9).children[0].value);
                        etotal += Number(grd.rows(rowCount).cells(11).children[0].innerHTML);
                        stotal += Number(grd.rows(rowCount).cells(13).children[0].innerHTML);
                    }

                }
                grd.rows[grd.rows.length - 1].cells[9].innerHTML = Math.round(total * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtbasic.ClientID %>").value = Math.round(total * Math.pow(10, 2)) / Math.pow(10, 2);
                grd.rows[grd.rows.length - 1].cells[11].innerHTML = Math.round(etotal * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtexduty.ClientID %>").value = Math.round(etotal * Math.pow(10, 2)) / Math.pow(10, 2);
                grd.rows[grd.rows.length - 1].cells[13].innerHTML = Math.round(stotal * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txttax.ClientID %>").value = Math.round(stotal * Math.pow(10, 2)) / Math.pow(10, 2);
                <%--document.getElementById("<%=txttotal.ClientID %>").value = Math.round(parseFloat(total) * Math.pow(10, 2)) / Math.pow(10, 2);--%>
                document.getElementById("<%=txttotal.ClientID %>").value = Math.round((parseFloat(total) + parseFloat(stotal) + parseFloat(etotal)) * Math.pow(10, 2)) / Math.pow(10, 2);
                ftamount = total;
                ftexcise = etotal;
                ftsales = stotal;
                document.getElementById("<%=hfamount.ClientID %>").value = total;
                document.getElementById("<%=hfexcisefooter.ClientID %>").value = etotal;
                document.getElementById("<%=hfsalestaxamt.ClientID %>").value = stotal;

                maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsales) + parseFloat(ftexcise)) * Math.pow(10, 2)) / Math.pow(10, 2);
                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) - parseFloat(hfdeduction);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
                if ((hfothers == "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) - parseFloat(hfdeduction);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
            }

        }
    </script>
    <script language="javascript">
        var maingridtotal = 0;
        function calculateother() {
            grd = document.getElementById("<%=gvanyother.ClientID %>");
            maingrd = document.getElementById("<%=gridcmc.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value;
            var ftexcise = document.getElementById("<%=hfexcisefooter.ClientID %>").value;
            var ftsales = document.getElementById("<%=hfsalestaxamt.ClientID %>").value;
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value;
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value;
            var totalother = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                    if (Number(grd.rows(rowCount).cells[3].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                            totalother += Number(grd.rows(rowCount).cells(3).children[0].value);
                        }
                    }
                    else {
                        totalother += Number(grd.rows(rowCount).cells(3).children[0].value);
                    }
                }
                grd.rows[grd.rows.length - 1].cells[3].innerHTML = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                hfothers = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                if (maingrd != null) {
                    calculate();
                    maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsales) + parseFloat(ftexcise)) * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                else if (maingrd == null) {
                    maingridtotal = 0;
                }
                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(hfothers) + parseFloat(maingridtotal);
                    document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2); ;
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) - parseFloat(hfdeduction);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }

            }
        }


        function calculatededuction() {
            //debugger;
            grd = document.getElementById("<%=gvdeduction.ClientID %>");
            maingrd = document.getElementById("<%=gridcmc.ClientID %>");
            grdother = document.getElementById("<%=gvanyother.ClientID %>");
            var ftamount = document.getElementById("<%=hfamount.ClientID %>").value;
            var ftexcise = document.getElementById("<%=hfexcisefooter.ClientID %>").value;
            var ftsales = document.getElementById("<%=hfsalestaxamt.ClientID %>").value;
            var hfothers = document.getElementById("<%=hfothers.ClientID %>").value;
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var hfnetamt = document.getElementById("<%=hfnetamt.ClientID %>").value;
            var totalded = 0;
            var sumtotalnet = 0;
            var otheramt = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {

                    if (Number(grd.rows(rowCount).cells[3].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                            totalded += Number(grd.rows(rowCount).cells(3).children[0].value);
                        }
                    }
                    else {
                        totalded += Number(grd.rows(rowCount).cells(3).children[0].value);
                    }

                }
                grd.rows[grd.rows.length - 1].cells[3].innerHTML = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=hfdeduction.ClientID %>").value = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                hfdeduction = Math.round(totalded * Math.pow(10, 2)) / Math.pow(10, 2);
                if (maingrd != null) {
                    calculate();
                    maingridtotal = Math.round((parseFloat(ftamount) + parseFloat(ftsales) + parseFloat(ftexcise)) * Math.pow(10, 2)) / Math.pow(10, 2);
                }
                else if (maingrd == null) {
                    maingridtotal = 0;
                }
                if (grdother != null) {
                    calculateother();
                    otheramt = hfothers;
                    document.getElementById("<%=hfothers.ClientID %>").value = otheramt;
                }
                else if (grdother == null) {
                    otheramt = 0;
                }
                if ((hfothers == "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(maingridtotal);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
                if ((hfothers != "") && (hfdeduction == "")) {
                    hfnetamt = parseFloat(hfothers) + parseFloat(maingridtotal);
                    document.getElementById("<%=hfothers.ClientID %>").value = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2); ;
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
                if ((hfothers != "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(hfothers) - parseFloat(hfdeduction);
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
                if ((hfothers == "") && (hfdeduction != "")) {
                    hfnetamt = parseFloat(maingridtotal) + parseFloat(otheramt) - parseFloat(hfdeduction);
                    document.getElementById("<%=hfdeduction.ClientID %>").value = hfdeduction;
                    document.getElementById("<%=txtnetAmount.ClientID %>").value = hfnetamt;
                }
            }
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
        function checkanyother() {
            var txt1 = document.getElementById("<%=TextBox1.ClientID %>").value;
            if (txt1 == "") {
                alert("Please Check DCA Codes");
                return false;
            }
        }
        function checkdeduction() {
            var txt2 = document.getElementById("<%=TextBox2.ClientID %>").value;
            if (txt2 == "") {

                alert("Please Check DCA Codes");
                return false;
            }
        }

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
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lable1
        {
            width: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <input type="button" id="btn" />
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
                                                                <td colspan="2" valign="top" class=" item-group" for="" width="100%">
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
                                                                                        OnSelectedIndexChanged="ddlmrr_SelectedIndexChanged" onchange="validateexcise();"
                                                                                        AutoPostBack="true">
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
                                                                                <td class="label" align="center">
                                                                                    <label for="journal_id">
                                                                                        Vendor Name
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td valign="middle" class="item item-char">
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
                                                                                        <%--Sub DCA Code--%>
                                                                                    </label>
                                                                                </td>
                                                                                <td class="item item-char" valign="middle">
                                                                                    <span class="filter_item">
                                                                                        <%--<asp:TextBox ID="txtsubdca" runat="server" CssClass="char" Enabled="false"></asp:TextBox>--%>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label for="reference" class="help">
                                                                                        Sales Tax
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rbtnsalestype" CssClass="esrbtn" Style="font-size: x-small"
                                                                                        Width="170px" ToolTip="Sales Type" RepeatDirection="Horizontal" runat="server"
                                                                                        OnSelectedIndexChanged="rbtnsalestype_SelectedIndexChanged" AutoPostBack="true">
                                                                                        <asp:ListItem Value="Creditable">Creditable</asp:ListItem>
                                                                                        <asp:ListItem Value="Non-Creditable">Non-Creditable</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                                <td colspan="2" class="label" width="15px">
                                                                                    <label>
                                                                                        DCA
                                                                                    </label>
                                                                                    :
                                                                                    <asp:DropDownList ID="ddlvatdca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                                        ToolTip="Sales Dca" runat="server">
                                                                                        <asp:ListItem Enabled="true" Selected="True" Text="select"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:Label runat="server" Text="SDCA :"></asp:Label>
                                                                                    <asp:DropDownList ID="ddlvatsdca" Font-Size="7" CssClass="filter_item" ToolTip="Sales Sub-Dca"
                                                                                        runat="server" Width="120px">
                                                                                        <asp:ListItem Enabled="true" Selected="True" Text="select"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td colspan="2" style="width: 390px">
                                                                                    <asp:DropDownList ID="ddlvattaxnos" Font-Size="7" Width="130px" CssClass="filter_item"
                                                                                        ToolTip="Sales Tax Nos" runat="server">
                                                                                        <asp:ListItem Enabled="true" Selected="True" Text="Select Tax No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label for="reference" class="help">
                                                                                        Excise Tax
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td>
                                                                                    <span class="filter_item">
                                                                                        <asp:RadioButtonList ID="rbtnexcisetype" CssClass="esrbtn" Style="font-size: x-small"
                                                                                            Width="170px" ToolTip="Excise Type" RepeatDirection="Horizontal" runat="server"
                                                                                            OnSelectedIndexChanged="rbtnexcisetype_SelectedIndexChanged" AutoPostBack="true">
                                                                                            <asp:ListItem Value="Creditable">Creditable</asp:ListItem>
                                                                                            <asp:ListItem Value="Non-Creditable">Non-Creditable</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </span>
                                                                                </td>
                                                                                <td colspan="2" class="label" width="1%">
                                                                                    <label>
                                                                                        DCA
                                                                                    </label>
                                                                                    :
                                                                                    <asp:DropDownList ID="ddlexcisedca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                                        ToolTip="Excise Dca" runat="server">
                                                                                        <asp:ListItem Enabled="true" Selected="True" Text="select"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:Label runat="server" Text="SDCA :"></asp:Label>
                                                                                    <asp:DropDownList ID="ddlexcisesdca" Font-Size="7" CssClass="filter_item" ToolTip="Excise Sub-Dca"
                                                                                        runat="server" Width="120px">
                                                                                        <asp:ListItem Enabled="true" Selected="True" Text="select"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td colspan="2" style="width: 350px">
                                                                                    <asp:DropDownList ID="ddlexcisetaxnos" Font-Size="7" Width="130px" CssClass="filter_item"
                                                                                        ToolTip="Excise Tax Nos" runat="server">
                                                                                        <asp:ListItem Enabled="true" Selected="True" Text="Select Tax No"></asp:ListItem>
                                                                                    </asp:DropDownList>
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
                                                                                    <asp:TextBox ID="txtbasic" runat="server" onkeyup="calculate();" onkeypress='return numericValidation(this);'
                                                                                        Width="100px"></asp:TextBox>
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
                                                                            <asp:TemplateField HeaderText="Excise Tax %">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtexcisetax" runat="server" onkeyup="calculate();" onkeypress='return numericValidation(this);'
                                                                                        Width="75px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblfooterexcisefooter" runat="server" Text=""></asp:Label>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Excise Tax Amt">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblexamt" runat="server" Text=""></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblexamountfooter" runat="server" Text=""></asp:Label>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sales Tax %">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtsalestax" runat="server" onkeyup="calculate();" onkeypress='return numericValidation(this);'
                                                                                        Width="75px"></asp:TextBox>
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
                                                                <asp:HiddenField ID="hfamount" runat="server" />
                                                                <asp:HiddenField ID="hfexcisefooter" runat="server" />
                                                                <asp:HiddenField ID="hfsalestaxamt" runat="server" />
                                                                <asp:HiddenField ID="hfothers" runat="server" />
                                                                <asp:HiddenField ID="hfdeduction" runat="server" />
                                                                <asp:HiddenField ID="hfexciseapplicable" runat="server" />
                                                                <asp:HiddenField ID="hfvatapplicable" runat="server" />
                                                                <td valign="top" class=" item-notebook" width="100%">
                                                                    <div id="_notebook_1" class="notebook" style="display: block;">
                                                                        <div class="notebook-tabs">
                                                                            <div class="right scroller">
                                                                            </div>
                                                                            <div class="left scroller">
                                                                            </div>
                                                                            <ul class="notebook-tabs-strip">
                                                                                <li class="notebook-tab notebook-page notebook-tab-active" title="" id="none"><span
                                                                                    class="tab-title"><span>Invoice Info</span></span></li>
                                                                                <li class="notebook-tab notebook-page" title="" style="display: none;"><span class="tab-title">
                                                                                    <span>Journal Items</span></span></li>
                                                                            </ul>
                                                                        </div>
                                                                        <div class="notebook-pages">
                                                                            <div class="notebook-page notebook-page-active">
                                                                                <div>
                                                                                    <table border="0" width="100%">
                                                                                        <tbody>
                                                                                            <tr align="center">
                                                                                                <td class="label1">
                                                                                                    <label class="help">
                                                                                                        PO NO:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtpo" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Invoice No:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char" colspan="3">
                                                                                                    <asp:TextBox ID="txtin" runat="server" ToolTip="Invoice No" onkeypress="javascript:return Restrictcomma(event);"
                                                                                                        CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center">
                                                                                                <td class="label1">
                                                                                                    <label class="help">
                                                                                                        Invoice Date:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtindt" runat="server" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                                        onkeypress="return false;" ToolTip="Invoice Date" CssClass="char"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtindt"
                                                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                        OnClientDateSelectionChanged="checkDate" PopupButtonID="txtindt">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Invoice Making Date:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtindtmk" runat="server" onKeyPress="javascript: return false;"
                                                                                                        onKeyDown="javascript: return false;" ToolTip="Invoice Making Date" CssClass="char"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindtmk"
                                                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                        PopupButtonID="txtindtmk">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center">
                                                                                                <td class="label1">
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
                                                                                                        Excise :
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtexduty" runat="server" CssClass="char" onKeyPress="javascript: return false;"
                                                                                                        onKeyDown="javascript: return false;"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        SalesTax :
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txttax" runat="server" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                                                        CssClass="char"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center">
                                                                                                <td class="label1">
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Invoice Total :
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txttotal" runat="server" CssClass="char" onKeyPress="javascript: return false;"
                                                                                                        ReadOnly="true" onKeyDown="javascript: return false;" ToolTip="Invoice Total"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                        Other Charges
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <asp:RadioButtonList ID="rbtnothercharges" CssClass="esrbtn" Style="font-size: x-small"
                                                                                                        Width="170px" ToolTip="Other Charges Yes or No" RepeatDirection="Horizontal"
                                                                                                        runat="server" OnSelectedIndexChanged="rbtnothercharges_SelectedIndexChanged"
                                                                                                        AutoPostBack="true" ClientIDMode="AutoID">
                                                                                                        <asp:ListItem>Yes</asp:ListItem>
                                                                                                        <asp:ListItem>No</asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </td>
                                                                                                <td id="tdddlanyotherdcas" runat="server" valign="middle" colspan="4">
                                                                                                    <%--   <asp:DropDownCheckBoxes ID="ddlanyotherdcas" runat="server" Width="180px" UseSelectAllNode="false">
                                                                                                        
                                                                                                    </asp:DropDownCheckBoxes>--%>
                                                                                                    <asp:TextBox ID="TextBox1" Width="400px" onKeyPress="javascript: return false;" ReadOnly="true"
                                                                                                        onKeyDown="javascript: return false;" ToolTip="Other DCA" runat="server"></asp:TextBox>
                                                                                                    <cc1:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" DynamicServicePath=""
                                                                                                        Enabled="True" ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1"
                                                                                                        OffsetY="22">
                                                                                                    </cc1:PopupControlExtender>
                                                                                                    <asp:Panel ID="Panel1" runat="server" Height="180px" Width="400px" BorderStyle="Solid"
                                                                                                        BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" Style="display: none"
                                                                                                        BackColor="LightGray">
                                                                                                        <asp:CheckBoxList ID="ddlanyotherdcas" Width="400px" CellPadding="5" CellSpacing="5"
                                                                                                            RepeatColumns="4" RepeatDirection="Horizontal" BackColor="LightGray" runat="server"
                                                                                                            AutoPostBack="True" OnDataBound="ddlanyotherdcas_DataBound" OnSelectedIndexChanged="ddlanyotherdcas_SelectedIndexChanged">
                                                                                                        </asp:CheckBoxList>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                                <td id="tdaddanyother" runat="server">
                                                                                                    <asp:Button ID="txtaddothers" Text="Add" runat="server" CssClass="button" OnClientClick="javascript:return checkanyother();"
                                                                                                        OnClick="Submit" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="tranyothergrid" runat="server">
                                                                                                <td colspan="6">
                                                                                                    <asp:GridView ID="gvanyother" runat="server" HeaderStyle-HorizontalAlign="Center"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                                                                        BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd" OnRowDataBound="OnRowDataBound"
                                                                                                        DataKeyNames="dca_code" ShowFooter="true" Width="780px">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkSelectother" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="dca_code" Visible="false" />
                                                                                                            <asp:BoundField DataField="mapdca_code" HeaderText="Dca Code" />
                                                                                                            <asp:TemplateField HeaderText="Sub Dca">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:DropDownList ID="ddlsdca" Width="200px" OnDataBound="ddlsdca_DataBound" ToolTip="SubDCA"
                                                                                                                        runat="server">
                                                                                                                    </asp:DropDownList>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtotheramount" runat="server" onpaste="return false;" onkeyup="calculateother();"
                                                                                                                        onkeypress="return numericValidation(this);" Width="100px"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <FooterTemplate>
                                                                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                                                                </FooterTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                                                    </asp:GridView>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                                                                    <div class="separator horizontal" style="font-size: 10pt">
                                                                                                        <asp:Label ID="Label3" runat="server" Text="Any Deduction"></asp:Label>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="6" style="height: 10px" valign="middle" width="100%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                    <asp:RadioButtonList ID="rbtndeduction" CssClass="esrbtn" Style="font-size: x-small"
                                                                                                        Width="170px" ToolTip="Deduction Type Yes or No" RepeatDirection="Horizontal"
                                                                                                        runat="server" OnSelectedIndexChanged="rbtndeduction_SelectedIndexChanged" AutoPostBack="true">
                                                                                                        <asp:ListItem>Yes</asp:ListItem>
                                                                                                        <asp:ListItem>No</asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </td>
                                                                                                <td id="tddeduction" runat="server" valign="middle" colspan="4">
                                                                                                    <asp:TextBox ID="TextBox2" Width="400px" onKeyPress="javascript: return false;" ReadOnly="true"
                                                                                                        onKeyDown="javascript: return false;" ToolTip="Deduction DCA" runat="server"></asp:TextBox>
                                                                                                    <cc1:PopupControlExtender ID="TextBox2_PopupControlExtender" runat="server" DynamicServicePath=""
                                                                                                        Enabled="True" ExtenderControlID="" TargetControlID="TextBox2" PopupControlID="Panel2"
                                                                                                        OffsetY="22">
                                                                                                    </cc1:PopupControlExtender>
                                                                                                    <asp:Panel ID="Panel2" runat="server" Height="180px" Width="400px" BorderStyle="Solid"
                                                                                                        BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" Style="display: none"
                                                                                                        BackColor="LightGray">
                                                                                                        <asp:CheckBoxList ID="ddldeduction" Width="400px" BackColor="LightGray" CellPadding="5"
                                                                                                            CellSpacing="5" RepeatColumns="4" RepeatDirection="Horizontal" runat="server"
                                                                                                            AutoPostBack="True" OnDataBound="ddldeduction_DataBound" OnSelectedIndexChanged="ddldeduction_SelectedIndexChanged">
                                                                                                        </asp:CheckBoxList>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                                <td id="tddedadd" runat="server">
                                                                                                    <asp:Button ID="btndeduction" Text="Add" runat="server" CssClass="button" OnClick="Submitdeduction"
                                                                                                        OnClientClick="javascript:return checkdeduction();" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="trdeductiongrid" runat="server">
                                                                                                <td colspan="6">
                                                                                                    <asp:GridView ID="gvdeduction" runat="server" HeaderStyle-HorizontalAlign="Center"
                                                                                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                                                                        BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd" OnRowDataBound="OnRowDataBoundgvdeduction"
                                                                                                        DataKeyNames="dca_code" ShowFooter="true" Width="780px">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkSelectdeduction" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="dca_code" Visible="false" />
                                                                                                            <asp:BoundField DataField="mapdca_code" HeaderText="Dca Code" />
                                                                                                            <asp:TemplateField HeaderText="Sub Dca">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:DropDownList ID="ddlsdca" widht="400px" ToolTip="SubDCA" OnDataBound="ddlsdcaded_DataBound"
                                                                                                                        runat="server" Width="200px">
                                                                                                                    </asp:DropDownList>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtamountdeduction" runat="server" onpaste="return false;" onkeyup="calculatededuction();"
                                                                                                                        onkeypress="return numericValidation(this);" Width="100px"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <FooterTemplate>
                                                                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                                                                </FooterTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                                                        <PagerStyle CssClass="grid pagerbar" />
                                                                                                        <HeaderStyle CssClass="grid-header" />
                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                                                    </asp:GridView>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label" width="1%">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                </td>
                                                                                                <td class="label" width="1%">
                                                                                                    <label class="help">
                                                                                                        Net Amount:
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td valign="middle" class="item item-char">
                                                                                                    <asp:TextBox ID="txtnetAmount" onKeyPress="javascript: return false;" onKeyDown="javascript: return false;"
                                                                                                        runat="server" ToolTip="NetAmount" CssClass="char"></asp:TextBox>
                                                                                                    <asp:HiddenField ID="hfnetamt" runat="server" />
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
    <script language="javascript" type="text/javascript">
        function validateexcise() {
            //debugger;
            document.getElementById("<%=ddlvatdca.ClientID %>").value = "select";
            document.getElementById("<%=ddlvatsdca.ClientID %>").value = "select";
            document.getElementById("<%=ddlvattaxnos.ClientID %>").value = "Select Tax No";
            document.getElementById("<%=ddlexcisedca.ClientID %>").value = "select";
            document.getElementById("<%=ddlexcisesdca.ClientID %>").value = "select";
            document.getElementById("<%=ddlexcisetaxnos.ClientID %>").value = "Select TaxNo";
            var response = confirm("Is Vat Tax Applicable? ");
            if (response) {
                document.getElementById("<%=rbtnsalestype.ClientID %>").disabled = false;
                document.getElementById("<%=ddlvatdca.ClientID%>").disabled = false;
                document.getElementById("<%=ddlvatsdca.ClientID%>").disabled = false;
                document.getElementById("<%=ddlvattaxnos.ClientID%>").disabled = false;
                document.getElementById("<%=hfvatapplicable.ClientID %>").value = "Yes";
                var response = confirm("Is Excise Tax Applicable? ");
                if (response) {
                    document.getElementById("<%=rbtnexcisetype.ClientID %>").disabled = false;
                    document.getElementById("<%=ddlexcisedca.ClientID%>").disabled = false;
                    document.getElementById("<%=ddlexcisesdca.ClientID %>").disabled = false;
                    document.getElementById("<%=ddlexcisetaxnos.ClientID %>").disabled = false;
                    document.getElementById("<%=hfexciseapplicable.ClientID %>").value = "Yes";

                }
                else {

                    document.getElementById("<%=rbtnexcisetype.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlexcisedca.ClientID%>").disabled = true;
                    document.getElementById("<%=ddlexcisesdca.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlexcisetaxnos.ClientID %>").disabled = true;
                    document.getElementById("<%=hfexciseapplicable.ClientID %>").value = "No";
                }
            }
            else {
                document.getElementById("<%=rbtnsalestype.ClientID %>").disabled = true;
                document.getElementById("<%=ddlvatdca.ClientID%>").disabled = true;
                document.getElementById("<%=ddlvatsdca.ClientID%>").disabled = true;
                document.getElementById("<%=ddlvattaxnos.ClientID%>").disabled = true;
                document.getElementById("<%=hfvatapplicable.ClientID %>").value = "No";
                var response = confirm("Is Excise Tax Applicable? ");
                if (response) {
                    document.getElementById("<%=rbtnexcisetype.ClientID %>").disabled = false;
                    document.getElementById("<%=ddlexcisedca.ClientID%>").disabled = false;
                    document.getElementById("<%=ddlexcisesdca.ClientID %>").disabled = false;
                    document.getElementById("<%=ddlexcisetaxnos.ClientID %>").disabled = false;
                    document.getElementById("<%=hfexciseapplicable.ClientID %>").value = "Yes";
                }
                else {

                    document.getElementById("<%=rbtnexcisetype.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlexcisedca.ClientID%>").disabled = true;
                    document.getElementById("<%=ddlexcisesdca.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlexcisetaxnos.ClientID %>").disabled = true;
                    document.getElementById("<%=hfexciseapplicable.ClientID %>").value = "No";
                }
            }
            return true;
        }
    </script>
    <script language="javascript">
        function validate() {
        //debugger;
            var vattax = document.getElementById("<%=hfvatapplicable.ClientID %>").value;
            var excisetax = document.getElementById("<%=hfexciseapplicable.ClientID %>").value;
            var objs = new Array("<%=ddlvendortype.ClientID %>", "<%=ddlmrr.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (vattax == "Yes") {
                if (!ChceckRBL("<%=rbtnsalestype.ClientID %>")) {
                    return false;
                }
                else {
                    var objs = new Array("<%=ddlvatdca.ClientID %>", "<%=ddlvatsdca.ClientID %>", "<%=ddlvattaxnos.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }
            }
            if (excisetax == "Yes") {
                if (!ChceckRBL("<%=rbtnexcisetype.ClientID %>")) {
                    return false;
                }
                else {
                    var objs = new Array("<%=ddlexcisedca.ClientID %>", "<%=ddlexcisesdca.ClientID %>", "<%=ddlexcisetaxnos.ClientID %>");
                    if (!CheckInputs(objs)) {
                        return false;
                    }
                }
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
                    else if (GridView1.rows(rowCount).cells(10).children[0].value == "") {
                        window.alert("Enter Excise Tax %");
                        GridView1.rows(rowCount).cells(10).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(12).children[0].value == "") {
                        window.alert("Enter Sales Tax %");
                        GridView1.rows(rowCount).cells(12).children[0].focus();
                        return false;
                    }
                }
            }

            var obj = new Array("<%=txtin.ClientID %>", "<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>");
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

            if (!ChceckRBL("<%=rbtnothercharges.ClientID %>")) {
                return false;
            }
            if (SelectedIndex("<%=rbtnothercharges.ClientID %>") == 0) {
                <%--var obj = new Array("<%=TextBox1.ClientID %>");
                if (!CheckInputs(obj)) {
                    return false;
                }--%>

                var GridView1 = document.getElementById("<%=gvanyother.ClientID %>");
                <%--if (document.getElementById("<%=TextBox1.ClientID %>").value == "" && GridView1 == null) {
                    alert("Click on Add Button")
                    return false;
                }--%>
                if (GridView1 != null) {
                    for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                        if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView1.rows(rowCount).cells(2).children[0].value == "Select SDCA") {
                            window.alert("Select SubDca");
                            GridView1.rows(rowCount).cells(2).children[0].focus();
                            return false;
                        }
                        else if (GridView1.rows(rowCount).cells(3).children[0].value == "") {
                            window.alert("Enter Amount");
                            GridView1.rows(rowCount).cells(3).children[0].focus();
                            return false;
                        }

                    }
                }
                else {
                    window.alert("Please Select DCAs");
                    return false;
                }
            }
            if (!ChceckRBL("<%=rbtndeduction.ClientID %>")) {
                return false;
            }
            if (SelectedIndex("<%=rbtndeduction.ClientID %>") == 0) {
               <%-- var obj = new Array("<%=TextBox2.ClientID %>");
                if (!CheckInputs(obj)) {
                    return false;
                }--%>
                var GridView1 = document.getElementById("<%=gvdeduction.ClientID %>");
                <%--if (document.getElementById("<%=TextBox2.ClientID %>").value == "" && GridView1 == null) {
                    alert("Click on Add Button")
                    return false;
                }--%>
                if (GridView1 != null) {
                    for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                        if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView1.rows(rowCount).cells(2).children[0].value == "Select SDCA") {
                            window.alert("Select SubDCA");
                            GridView1.rows(rowCount).cells(2).children[0].focus();
                            return false;
                        }
                        else if (GridView1.rows(rowCount).cells(3).children[0].value == "") {
                            window.alert("Enter Amount");
                            GridView1.rows(rowCount).cells(3).children[0].focus();
                            return false;
                        }

                    }
                }
                else {
                    window.alert("Please Select DCAs");
                    return false;
                }
            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
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
            var str1 = document.getElementById("<%=txtindt.ClientID %>").value;
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
                document.getElementById("<%=txtindt.ClientID %>").value = "";
                document.getElementById("<%=txtindtmk.ClientID %>").value = "";
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
