<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyClientInvoice.aspx.cs" Inherits="VerifyClientInvoice" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script src="Java_Script/validations.js" type="text/javascript"></script>
    <script type="text/javascript">
        function AddTaxes() {
            //debugger;
            var date = document.getElementById("<%=txtindt.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtindt.ClientID %>");
            var trAddTaxes = document.getElementById("<%=traddtaxes.ClientID %>");
            if (date == "") {
                alert("Please Select Invoice date before Add Taxes");
                datectrl.focus();
                return false;
            }
            else {
                document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                document.getElementById("<%=traddtaxes.ClientID %>").style.display = "block";
                document.getElementById("<%=btnaddtaxes.ClientID %>").click();
                return true;
            }

        }
        function RemoveTaxes() {
            document.getElementById("<%=btnremovetaxes.ClientID %>").click();
        }
        function calculatetaxes() {
            //debugger;
            grd = document.getElementById("<%=gvtaxes.ClientID %>");
            var hftaxtotal = document.getElementById("<%=hftaxtotal.ClientID %>").value;
            var totalother = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[6].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(6).children[0].value)) {
                            totalother += Number(grd.rows(rowCount).cells(6).children[0].value);
                        }
                    }
                    else {
                        totalother += Number(grd.rows(rowCount).cells(6).children[0].value);
                    }
                }
                hftaxtotal = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=lbltaxes.ClientID %>").value = hftaxtotal;
            }
            else {
                hftaxtotal = 0;
                document.getElementById("<%=lbltaxes.ClientID %>").value = hftaxtotal;
            }
        }
        function checktaxdca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvtaxes.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            for (i = 1; i < grid.rows.length - 1; i++) {
                var dropdownoldValue = grid.rows[i].cells[3].childNodes[0].value;
                if (rowIndex != i) {
                    if (currentDropDownValue == grid.rows(i).cells(3).children[0].value) {
                        window.alert("Already Dca Selected");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                    else if (currentDropDownValue == "DCA-44") {
                        if (grid.rows(i).cells(3).children[0].value.substring(0, 6) == "DCA-SR" && currentDropDownValue == "DCA-44") {
                            window.alert("Invalid");
                            document.getElementById(val.id).value = "Select";
                            return false;
                        }
                    }
                    else if (currentDropDownValue == "DCA-SRTX") {
                        if (grid.rows(i).cells(3).children[0].value.substring(0, 6) == "DCA-44" && currentDropDownValue == "DCA-SRTX") {
                            window.alert("Invalid");
                            document.getElementById(val.id).value = "Select";
                            return false;
                        }
                    }
                    else if (currentDropDownValue != "DCA-SRTX" && currentDropDownValue != "DCA-44") {
                        if (currentDropDownValue.substring(0, 7) == grid.rows(i).cells(3).children[0].value.substring(0, 7)) {
                            window.alert("Invalid");
                            document.getElementById(val.id).value = "Select";
                            return false;
                        }
                    }
                }
            }
        }
        function checktaxsdca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvtaxes.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            for (i = 1; i < grid.rows.length - 1; i++) {
                var dropdownoldValue = grid.rows[i].cells[4].childNodes[0].value;
                if (rowIndex != i) {
                    if (currentDropDownValue == grid.rows(i).cells(4).children[0].value) {
                        window.alert("Already SDCA Selected");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                }
            }
        }
        function verifytaxsdca() {
            //debugger;
            var grid = document.getElementById("<%= gvtaxes.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (grid.rows(i).cells(2).children[0].value == "Select") {
                    window.alert("Select Type");
                    return false;
                }
                else if (grid.rows(i).cells(3).children[0].value == "Select") {
                    window.alert("Select DCA");
                    return false;
                }
                else if (grid.rows(i).cells(4).children[0].value == "Select") {
                    window.alert("Select SDCA");
                    return false;
                }
                else if (grid.rows(i).cells(5).children[0].value == "Select") {
                    window.alert("Select Tax Nos");
                    return false;
                }
                else if (grid.rows(i).cells(6).children[0].value == "") {
                    window.alert("Enter Amount");
                    return false;
                }

            }
        }
        
     
    </script>
    <script type="text/javascript">
        function Addcess() {
            //debugger;
            var date = document.getElementById("<%=txtindt.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtindt.ClientID %>");
            var traddcess = document.getElementById("<%=traddcess.ClientID %>");
            if (date == "") {
                alert("Please Select Invoice date before Add Cess Taxes");
                datectrl.focus();
                return false;
            }
            else {
                document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                document.getElementById("<%=traddcess.ClientID %>").style.display = "block";
                document.getElementById("<%=btnaddcesss.ClientID %>").click();
                return true;
            }

        }
        function Removecess() {
            document.getElementById("<%=btnremovecesss.ClientID %>").click();
        }
        function calculatecess() {
            grd = document.getElementById("<%=gvcess.ClientID %>");
            var hfcesstotal = document.getElementById("<%=hfcesstotal.ClientID %>").value;
            var totalcess = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[6].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(6).children[0].value)) {
                            totalcess += Number(grd.rows(rowCount).cells(6).children[0].value);
                        }
                    }
                    else {
                        totalcess += Number(grd.rows(rowCount).cells(6).children[0].value);
                    }
                }
                hfcesstotal = Math.round(totalcess * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtcess.ClientID %>").value = hfcesstotal;
            }
            else {
                hfcesstotal = 0;
                document.getElementById("<%=txtcess.ClientID %>").value = hfcesstotal;
            }
        }
        function checkcesssdca(val) {
            //debugger;
            var grid = document.getElementById("<%= gvcess.ClientID %>");
            var currentDropDownValue = document.getElementById(val.id).value;
            var rowData = val.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (rowIndex != i) {
                    if (currentDropDownValue == grid.rows(i).cells(4).children[0].value) {
                        window.alert("Invalid SDCA");
                        document.getElementById(val.id).value = "Select";
                        return false;
                    }
                }

            }
        }

        function verifycessdca() {
            //debugger;
            var grid = document.getElementById("<%= gvcess.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (grid.rows(i).cells(2).children[0].value == "Select") {
                    window.alert("Select Type");
                    return false;
                }
                else if (grid.rows(i).cells(3).children[0].value == "Select") {
                    window.alert("Select DCA");
                    return false;
                }
                else if (grid.rows(i).cells(4).children[0].value == "Select") {
                    window.alert("Select SDCA");
                    return false;
                }
                else if (grid.rows(i).cells(5).children[0].value == "Select") {
                    window.alert("Select Tax Nos");
                    return false;
                }
                else if (grid.rows(i).cells(6).children[0].value == "") {
                    window.alert("Enter Amount");
                    return false;
                }

            }
        }
       
    </script>
    <script type="text/javascript" language="javascript">        //FOR OTHER GRID Starts
        function getotherIndex(r) {
            //debugger;

            var date = document.getElementById("<%=txtindt.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtindt.ClientID %>");

            var radio = rbs.getElementsByTagName("input");
            var label = rbs.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    var value = radio[i].value;
                    if (value == "Yes") {
                        if (date == "") {
                            alert("Please Select Invoice date before Add Taxes");
                            //document.getElementById("<%=txtindt.ClientID %>").disabled = false;
                            datectrl.focus();
                            return false;
                        }
                        else {
                            if (date != "")
                                document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                            else
                                document.getElementById("<%=txtindt.ClientID %>").disabled = false;


                            return true;
                        }
                    }
                    else if (value == "No") {
                        if (date != "")
                            document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                        else
                            document.getElementById("<%=txtindt.ClientID %>").disabled = false;

                        hfother = 0;
                        return true;
                    }
                    else {
                        if (date != "")
                            document.getElementById("<%=txtindt.ClientID %>").disabled = true;
                        else
                            document.getElementById("<%=txtindt.ClientID %>").disabled = false;

                        hfother = 0;
                        return false;
                    }
                }
            }

        }

        function calculateother() {


            var totalother = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[4].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(4).children[0].value)) {
                            totalother += Number(grd.rows(rowCount).cells(4).children[0].value);
                        }
                    }
                    else {
                        totalother += Number(grd.rows(rowCount).cells(4).children[0].value);
                    }
                }
                hfother = Math.round(totalother * Math.pow(10, 2)) / Math.pow(10, 2);

            }
            else {
                hfother = 0;

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
                        <table class="estbl" width="750px">
                            <tr>
                                <td>
                                    <asp:GridView runat="server" ID="GvdInvoices" HeaderStyle-HorizontalAlign="Center"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                        BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="Invoice_Id"
                                        GridLines="Both" ShowFooter="false" Width="740px" ShowHeaderWhenEmpty="true"
                                        OnRowEditing="GvdInvoices_RowEditing" OnSelectedIndexChanged="GvdInvoices_SelectedIndexChanged">
                                        <HeaderStyle CssClass="headerstyle" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                SelectImageUrl="~/images/fields-a-lookup-a.gif" />
                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                                EditImageUrl="~/images/iconset-b-edit.gif" />
                                            <asp:BoundField DataField="po_no" HeaderText="PO No" />
                                            <asp:BoundField DataField="Invoiceno" HeaderText="Invoice No" />
                                            <asp:BoundField DataField="Invoice_date" HeaderText="Invoice Date" />
                                            <asp:BoundField DataField="CCName" HeaderText="CC Name" />
                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" />
                                            <asp:BoundField DataField="branch" HeaderText="Sub Client Name" />
                                            <asp:BoundField DataField="Total" HeaderText="Invoice Value" />
                                        </Columns>
                                        <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                        <PagerStyle CssClass="grid pagerbar" />
                                        <HeaderStyle CssClass="grid-header" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table class="estbl eslbl" width="750px" id="tblinv" runat="server" visible="false">
                        </table>
                        <table id="tbldetails" class="estbl" width="750px" runat="server" visible="false">
                            <tr>
                                <th align="center" colspan="6">
                                    Customer Invoice Creation
                                </th>
                            </tr>
                            <tr id="paytype" runat="server">
                                <td colspan="6">
                                    <table class="innertab">
                                        <tr>
                                            <td style="width: 150px;" align="right">
                                                Invoice Category:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblInvoiceCategory" CssClass="eslbl" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 150px;" align="right" id="tdinvoice" runat="server" visible="true">
                                                Types of Invoices:
                                            </td>
                                            <td align="right" id="tdtypeinvoice" runat="server" visible="true">
                                                <asp:Label ID="lblInvoicetype" CssClass="eslbl" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdebit" runat="server">
                                <td style="width: 80px">
                                    CC-Code:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblCCCode" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    Client ID:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblClientid" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    Subclient ID:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblsubclientid" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="Invoice" runat="server">
                                <td colspan="6">
                                    <table id="Table1" class="" runat="server" style="border-style: hidden;" width="100%">
                                        <tr id="trpo" runat="server" style="height: 40px;">
                                            <td>
                                                <table class="innertab" width="100%">
                                                    <tr>
                                                        <td style="width: 80px">
                                                            PO No:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblpono" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px">
                                                            Invoice No:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblInvNo" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px">
                                                            RA No:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtra" CssClass="estbox" runat="server" ToolTip="RA No:" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trinv" runat="server" style="height: 40px;">
                                            <td>
                                                <table class="innertab" width="100%">
                                                    <tr>
                                                        <td style="width: 80px">
                                                            Invoice Date:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtindt" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                                Width="150px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtindt"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                PopupButtonID="txtindt">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="width: 80px">
                                                            Inv Making Date:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtindtmk" CssClass="estbox" runat="server" ToolTip="Invoice Making Date"
                                                                Width="150px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtindtmk"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                PopupButtonID="txtindtmk">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="width: 80px">
                                                            Basic Value:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbasic" CssClass="estbox" runat="server" ToolTip="Basic Value"
                                                                onkeypress="return numericValidation(this);" onkeyup="Total();" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trtaxes" runat="server">
                                <td colspan="6">
                                    <table id="tblgridtaxes" runat="server" class="innertab" align="center" width="750px">
                                        <tr align="left">
                                            <td colspan="6" align="left">
                                                <button name="button" onclick="AddTaxes()" id="btnaddtax" runat="server" type="button">
                                                    Add Tax</button>
                                                <button name="button" id="btnremovetax" runat="server" onclick="RemoveTaxes()" type="button">
                                                    Remove All Taxes</button>
                                                <asp:Button ID="btnremovetaxes" runat="server" Style="display: none;" Text="" OnClick="btnremovetaxes_Click" />
                                                <asp:Button ID="btnaddtaxes" runat="server" Style="display: none;" Text="" OnClick="btnAddTax_Click" />
                                            </td>
                                        </tr>
                                        <tr id="traddtaxes" runat="server" style="display: none">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvtaxes" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true"
                                                    OnRowDeleting="gvtaxes_RowDeleting" OnRowDataBound="gvtaxes_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelecttaxes" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltype" Font-Size="7" Width="90px" CssClass="filter_item"
                                                                    ToolTip="Type" runat="server" OnSelectedIndexChanged="ddltaxtype_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltaxdca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                    ToolTip="DCA" OnSelectedIndexChanged="ddltaxdca_SelectedIndexChanged" AutoPostBack="true"
                                                                    runat="server">
                                                                    <%--onchange="checktaxdca(this)"--%>
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SDCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltaxsdca" Font-Size="7" onchange="checktaxsdca(this)" Width="120px"
                                                                    CssClass="filter_item" ToolTip="SDCA" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tax No">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltaxnos" Font-Size="7" Width="80px" CssClass="filter_item"
                                                                    ToolTip="Tax Nos" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txttaxamount" runat="server" Font-Size="7" CssClass="filter_item"
                                                                    onkeypress="return numericValidation(this);" onkeyup="calculatetaxes(); Total();"
                                                                    Width="75px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAdd" runat="server" CssClass="esbtn" OnClick="btnAdd_Click" OnClientClick="javascript:return verifytaxsdca();"
                                                                    Text="Add More Taxes" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" DeleteText="Remove" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="right">
                                            <td colspan="6" style="font-weight: normal">
                                                <asp:HiddenField ID="hftaxtotal" runat="server" />
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Tax Amount:"></asp:Label>
                                                <asp:TextBox ID="lbltaxes" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trcess" runat="server">
                                <td colspan="6">
                                    <table id="tblgridcess" runat="server" class="innertab" align="center" width="750px">
                                        <tr align="left">
                                            <td colspan="6" align="left">
                                                <button name="button" onclick="Addcess()" id="btnaddcess" runat="server" type="button">
                                                    Add Cess</button>
                                                <button name="button" id="btnremovecess" runat="server" onclick="Removecess()" type="button">
                                                    Remove All Cess Taxes</button>
                                                <asp:Button ID="btnremovecesss" runat="server" Style="display: none;" Text="" OnClick="btnremovecesss_Click" />
                                                <asp:Button ID="btnaddcesss" runat="server" Style="display: none;" Text="" OnClick="btnaddcesss_Click" />
                                            </td>
                                        </tr>
                                        <tr id="traddcess" runat="server" style="display: none">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvcess" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true"
                                                    OnRowDeleting="gvcess_RowDeleting" OnRowDataBound="gvcess_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectcess" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltypecess" Font-Size="7" Width="90px" CssClass="filter_item"
                                                                    ToolTip="Cess Type" runat="server" OnSelectedIndexChanged="ddltypecess_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlcessdca" Font-Size="7" Width="120px" CssClass="filter_item"
                                                                    ToolTip="Cess DCA" OnSelectedIndexChanged="ddlcessdca_SelectedIndexChanged" AutoPostBack="true"
                                                                    runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SDCA">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlcesssdca" Font-Size="7" Width="130px" CssClass="filter_item"
                                                                    ToolTip="Cess SDCA" runat="server" onchange="checkcesssdca(this)">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tax No">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlcessnos" Font-Size="7" Width="100px" CssClass="filter_item"
                                                                    ToolTip="Cess Nos" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtcessamount" runat="server" CssClass="filter_item" onkeypress="return numericValidation(this);"
                                                                    onkeyup="calculatecess(); Total();" Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAddcess" runat="server" CssClass="esbtn" OnClick="btnaddcess_Click"
                                                                    OnClientClick="javascript:return verifycessdca();" Text="Add More Cess" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" DeleteText="Remove" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="right">
                                            <td colspan="6" style="font-weight: normal">
                                                <asp:HiddenField ID="hfcesstotal" runat="server" />
                                                <asp:Label ID="lbl" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Cess Amount:"></asp:Label>
                                                <asp:TextBox ID="txtcess" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdescription" runat="server">
                                <td style="border-right: none">
                                    <asp:Label ID="Label11" runat="server" Font-Size="10px" CssClass="eslbl" Text="Invoice Value"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="txtinvvalue" Width="150px" ToolTip="Invoice Value" onKeyPress="javascript: return false;"
                                        onKeyDown="javascript: return false;" runat="server"> </asp:TextBox>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label1" runat="server" Font-Size="10px" CssClass="eslbl" Text="Status"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:DropDownList ID="ddlstatus" Font-Size="7" Width="90px" CssClass="filter_item"
                                        ToolTip="Status" runat="server">
                                        <asp:ListItem>Approve</asp:ListItem>
                                        <asp:ListItem>Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label12" runat="server" Font-Size="10px" CssClass="eslbl" Text="Description"></asp:Label>
                                </td>
                                <td style="border-right: none" colspan="3">
                                    <asp:TextBox ID="txtdescription" ToolTip="Description" Width="350px" runat="server"
                                        TextMode="MultiLine"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr id="btn" runat="server">
                                <td align="center" colspan="6">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validate();" OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Reset" OnClick="btnCancel1_Click" />
                                </td>
                            </tr>
                        </table>
                        <table class="estbl eslbl" width="750px" id="tblview" runat="server" visible="false">
                            <tr>
                                <td colspan="6" align="center" style="font-weight: bold">
                                    View Customer Invoice
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td colspan="6">
                                    <table class="innertab">
                                        <tr>
                                            <td style="width: 150px;" align="right">
                                                Invoice Category:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblviewinvoicecategory" CssClass="eslbl" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 150px;" align="right" id="td1" runat="server" visible="false">
                                                Types of Invoices:
                                            </td>
                                            <td align="right" id="td2" runat="server" visible="false">
                                                <asp:Label ID="lblviewinvoicetype" CssClass="eslbl" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tr2" runat="server">
                                <td style="width: 80px">
                                    CC-Code:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblviewcccode" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    Client ID:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblviewclientid" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    Subclient ID:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblviewsubclientid" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td colspan="6">
                                    <table id="Table2" class="" runat="server" style="border-style: hidden;" width="100%">
                                        <tr id="tr4" runat="server" style="height: 40px;">
                                            <td>
                                                <table class="innertab" width="100%">
                                                    <tr>
                                                        <td style="width: 80px">
                                                            PO No:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblviewpono" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px">
                                                            Invoice No:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblviewinvoiceno" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px">
                                                            RA No:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblviewrano" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="tr5" runat="server" style="height: 40px;">
                                            <td>
                                                <table class="innertab" width="100%">
                                                    <tr>
                                                        <td style="width: 80px">
                                                            Invoice Date:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lvlinvdate" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px">
                                                            Inv Making Date:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblinvmdate" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px">
                                                            Basic Value:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblbasic" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tr6" runat="server">
                                <td colspan="6">
                                    <table id="Table3" runat="server" class="innertab" align="center" width="750px">
                                        <tr id="trviewtax" runat="server" style="display: none">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="GridView1" HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                    BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames=""
                                                    GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ISCreditableTax" HeaderText="Type" />
                                                        <asp:BoundField DataField="DCA_Code" HeaderText="DCA Code" />
                                                        <asp:BoundField DataField="SubDCA_Code" HeaderText="Sub DCA Code" />
                                                        <asp:BoundField DataField="TaxNo" HeaderText="Tax No" />
                                                        <asp:BoundField DataField="TaxValue" HeaderText="Amount" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="right">
                                            <td colspan="6" style="font-weight: normal">
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Tax Amount:"></asp:Label>
                                                <asp:TextBox ID="txttaxtotalview" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tr8" runat="server">
                                <td colspan="6">
                                    <table id="Table4" runat="server" class="innertab" align="center" width="750px">
                                        <tr id="trviewcess" runat="server" style="display: none">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="GridView2" HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                    BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames=""
                                                    GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ISCreditableTax" HeaderText="Type" />
                                                        <asp:BoundField DataField="DCA_Code" HeaderText="DCA Code" />
                                                        <asp:BoundField DataField="SubDCA_Code" HeaderText="Sub DCA Code" />
                                                        <asp:BoundField DataField="TaxNo" HeaderText="Tax No" />
                                                        <asp:BoundField DataField="TaxValue" HeaderText="Amount" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="right">
                                            <td colspan="6" style="font-weight: normal">
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Cess Amount:"></asp:Label>
                                                <asp:TextBox ID="txtcesstotalview" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-right: none">
                                    <asp:Label ID="Label14" runat="server" Font-Size="10px" CssClass="eslbl" Text="Invoice Value"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="lblinvoicevalue" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label16" runat="server" Font-Size="10px" CssClass="eslbl" Text="Description"></asp:Label>
                                </td>
                                <td style="border-right: none" colspan="3">
                                    <asp:TextBox ID="txtviewdescriptrion" ToolTip="Description" Width="350px" runat="server"
                                        TextMode="MultiLine" ReadOnly="true"> </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var anytaxes = 0;
        var cesstaxes = 0;
        function validate() {
            var objs = new Array("<%=txtindt.ClientID %>", "<%=txtindtmk.ClientID %>", "<%=txtra.ClientID %>", "<%=txtbasic.ClientID %>");
            if (!CheckInputs(objs)) {
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
                document.getElementById("<%=txtindtmk.ClientID %>").focus();
                return false;
            }
            var objs = new Array("<%=txtbasic.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            //debugger;
            GridView1 = document.getElementById("<%=gvtaxes.ClientID %>");
            if (GridView1 == null) {
                if (document.getElementById("<%=btnaddtax.ClientID %>").style.display == "") {
                    if (anytaxes == 0) {
                        var response = confirm("Do you want to add Taxes");
                        if (response) {
                            anytaxes = 1;
                            return false;
                        }
                        else {
                            anytaxes = 1;
                            var objs = new Array("<%=txtinvvalue.ClientID %>", "<%=txtdescription.ClientID %>");
                            if (!CheckInputs(objs)) {
                                return false;
                            }
                            var response = confirm("Do you want to" + document.getElementById("<%=ddlstatus.ClientID %>").value);
                            if (response) {
                                return true;
                                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                            }
                            else {
                                return false;
                            }

                        }
                    }
                }
            }
            //debugger;
            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(1).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(2).children[0].value == "Select") {
                        window.alert("Select Type");
                        GridView1.rows(rowCount).cells(2).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(3).children[0].value == "Select") {
                        window.alert("Select Dca");
                        GridView1.rows(rowCount).cells(3).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(4).children[0].value == "Select") {
                        window.alert("Select Sub Dca");
                        GridView1.rows(rowCount).cells(4).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(5).children[0].value == "Select") {
                        window.alert("Select Tax No");
                        GridView1.rows(rowCount).cells(5).children[0].focus();
                        return false;
                    }
                    else if (GridView1.rows(rowCount).cells(6).children[0].value == "") {
                        window.alert("Enter Amount");
                        GridView1.rows(rowCount).cells(6).children[0].focus();
                        return false;
                    }
                }
            }

            GridView2 = document.getElementById("<%=gvcess.ClientID %>");
            if (GridView2 == null) {
                if (document.getElementById("<%=btnaddcess.ClientID %>").style.display == "") {
                    if (cesstaxes == 0) {
                        var response1 = confirm("Do you want to add Cess");
                        if (response1) {
                            cesstaxes = 1;
                            return false;
                        }
                    }
                    else {
                        cesstaxes = 1;
                        var objs = new Array("<%=txtinvvalue.ClientID %>", "<%=txtdescription.ClientID %>");
                        if (!CheckInputs(objs)) {
                            return false;
                        }
                        var response = confirm("Do you want to" + document.getElementById("<%=ddlstatus.ClientID %>").value);
                        if (response) {
                            return true;
                            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
                        }
                        else {
                            return false;
                        }

                    }
                }
            }
            if (GridView2 != null) {
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    if (GridView2.rows(rowCount).cells(1).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(2).children[0].value == "Select") {
                        window.alert("Select Type");
                        GridView2.rows(rowCount).cells(2).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(3).children[0].value == "Select") {
                        window.alert("Select Dca");
                        GridView2.rows(rowCount).cells(3).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(4).children[0].value == "Select") {
                        window.alert("Select Sub Dca");
                        GridView2.rows(rowCount).cells(4).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(5).children[0].value == "Select") {
                        window.alert("Select Tax No");
                        GridView2.rows(rowCount).cells(5).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(6).children[0].value == "") {
                        window.alert("Enter Amount");
                        GridView2.rows(rowCount).cells(6).children[0].focus();
                        return false;
                    }
                }
            }

            var objs = new Array("<%=txtinvvalue.ClientID %>", "<%=txtdescription.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var response = confirm("Do you want to" + document.getElementById("<%=ddlstatus.ClientID %>").value);
            if (response) {
                return true;
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            }
            else {
                return false;
            }


        }
    </script>
    <script language="javascript" type="text/javascript">
        function Total() {
            var invoiceval = 0;
            var Netval = 0;
            var basic = document.getElementById("<%=txtbasic.ClientID %>").value;
            var taxes = document.getElementById("<%=lbltaxes.ClientID %>").value;
            var cess = document.getElementById("<%=txtcess.ClientID %>").value;


            if (basic == "") {
                basic = 0;
            }
            if (taxes == "") {
                tax = 0;
            }
            if (cess == "") {
                cess = 0;
            }


            invoiceval = eval((parseFloat(basic) + parseFloat(taxes) + parseFloat(cess)));

            var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);

            document.getElementById('<%= txtinvvalue.ClientID%>').value = roundinvvalue;

        }

    </script>
</asp:Content>
