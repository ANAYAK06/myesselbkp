<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyClientInvoicePayment.aspx.cs" Inherits="VerifyClientInvoicePayment" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script src="Java_Script/validations.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">        //FOR Deduction Starts
        function getdeductionIndex(r) {
            //debugger;
            var rbs = document.getElementById("<%=rbtndeductioncharges.ClientID%>");
            var date = document.getElementById("<%=lblindt.ClientID %>").innerHTML;
            var datectrl = document.getElementById("<%=lblindt.ClientID %>").innerHTML;
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var radio = rbs.getElementsByTagName("input");
            var label = rbs.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    var value = radio[i].value;
                    if (value == "Yes") {
                        if (date == "") {
                            alert("Please Select Invoice date before Add Deduction DCAs");

                            datectrl.focus();
                            return false;
                        }
                        else {

                            document.getElementById("<%=trdeductiongrid.ClientID %>").style.display = "block";
                            return true;
                        }
                    }
                    else if (value == "No") {

                        document.getElementById("<%=trdeductiongrid.ClientID %>").style.display = "none";
                        hfdeduction = 0;
                        return true;
                    }
                    else {

                        document.getElementById("<%=trdeductiongrid.ClientID %>").style.display = "none";
                        hfdeduction = 0;
                        return false;
                    }
                }
            }

        }

        function calculatededuction() {
            grd = document.getElementById("<%=gvdeduction.ClientID %>");
            var hfdeduction = document.getElementById("<%=hfdeduction.ClientID %>").value;
            var totaldeduction = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[6].innerHTML) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(6).children[0].value)) {
                            totaldeduction += Number(grd.rows(rowCount).cells(6).children[0].value);
                        }
                    }
                    else {
                        totaldeduction += Number(grd.rows(rowCount).cells(6).children[0].value);
                    }
                }
                hfdeduction = Math.round(totaldeduction * Math.pow(10, 2)) / Math.pow(10, 2);
                document.getElementById("<%=txtdeduction.ClientID %>").value = hfdeduction;
            }
            else {
                hfdeduction = 0;
                document.getElementById("<%=txtdeduction.ClientID %>").value = hfdeduction;
            }
        }
        function checkdeductiondca() {
            debugger;
            var grid = document.getElementById("<%= gvdeduction.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {

                if (i > 1) {
                    for (j = 1; j < grid.rows.length - 1; j++) {
                        if (i != j) {
                            if ((grid.rows(i).cells(2).children[0].value == grid.rows(j).cells(2).children[0].value) && (grid.rows(i).cells(3).children[0].value == grid.rows(j).cells(3).children[0].value) && (grid.rows(i).cells(4).children[0].value == grid.rows(j).cells(4).children[0].value) && (grid.rows(i).cells(5).children[0].value == grid.rows(j).cells(5).children[0].value)) {
                                window.alert("Invalid Selection");
                                grid.rows(i).cells(5).children[0].value = "Select";
                                return false;
                            }
                        }
                    }
                }
            }
        }
        function verifydeddca() {
            //debugger;
            var grid = document.getElementById("<%= gvdeduction.ClientID %>");
            for (i = 1; i < grid.rows.length - 1; i++) {
                if (i > 1) {
                    for (j = 1; j < grid.rows.length - 1; j++) {
                        if (i != j) {
                            if ((grid.rows(i).cells(2).children[0].value == grid.rows(j).cells(2).children[0].value) && (grid.rows(i).cells(3).children[0].value == grid.rows(j).cells(3).children[0].value) && (grid.rows(i).cells(4).children[0].value == grid.rows(j).cells(4).children[0].value) && (grid.rows(i).cells(5).children[0].value == grid.rows(j).cells(5).children[0].value)) {
                                window.alert("Invalid Selection");
                                return false;
                            }
                        }
                    }
                }
                if (grid.rows(i).cells(2).children[0].value == "Select") {
                    window.alert("Select Yes/No");
                    return false;
                }
                else if (grid.rows(i).cells(3).children[0].value == "Select") {
                    window.alert("Select Cost Center");
                    return false;
                }
                else if (grid.rows(i).cells(4).children[0].value == "Select") {
                    window.alert("Select Dca");
                    return false;
                }
                else if (grid.rows(i).cells(5).children[0].value == "Select") {
                    window.alert("Select Sub-Dca");
                    return false;
                }

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
                                        BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="Id"
                                        GridLines="Both" ShowFooter="false" Width="740px" ShowHeaderWhenEmpty="true"
                                        OnSelectedIndexChanged="GvdInvoices_SelectedIndexChanged">
                                        <HeaderStyle CssClass="headerstyle" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-Width="15px"
                                                SelectImageUrl="~/images/iconset-b-edit.gif" />
                                            <asp:BoundField DataField="po_no" HeaderText="PO No" />
                                            <asp:BoundField DataField="Invoiceno" HeaderText="Invoice No" />
                                            <asp:BoundField DataField="Date" HeaderText="Payment Recieving Date" />
                                            <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                            <asp:BoundField DataField="Credit" HeaderText="Amount" />
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
                        <table class="estbl eslbl" width="750px" id="paytype" runat="server" visible="false">
                            <tr>
                                <th align="center">
                                    Customer Invoice Payment Recieving
                                </th>
                            </tr>
                        </table>
                        <table id="tbldetails" class="innertab" width="750px" runat="server">
                            <tr id="trdebit" runat="server" visible="false">
                                <td style="width: 80px">
                                    CC-Code:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblcccode" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    PO No:
                                </td>
                                <td>
                                    <asp:Label ID="lblpo" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    Invoice No:
                                </td>
                                <td>
                                    <asp:Label ID="lblinno" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trclient" runat="server" visible="false">
                                <td style="width: 80px">
                                    Client ID:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblclientid" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    Subclient ID:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblsubclient" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    RA No:
                                </td>
                                <td>
                                    <asp:Label ID="lblrano" CssClass="eslbl" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="Invoice" runat="server" visible="false">
                                <td colspan="6">
                                    <table id="Table1" class="" runat="server" style="border-style: hidden;" width="100%">
                                        <tr id="trinv" runat="server" style="height: 40px;">
                                            <td>
                                                <table class="innertab" width="100%">
                                                    <tr>
                                                        <td style="width: 80px">
                                                            Invoice Date:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblindt" CssClass="eslbl" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px">
                                                            Inv Making Date:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblinmkgdate" CssClass="eslbl" runat="server"></asp:Label>
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
                            <tr id="trviewtax" runat="server" visible="false">
                                <td colspan="6">
                                    <table id="tblgridtaxes" runat="server" class="innertab" align="center" width="750px">
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvtaxes" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" Width="740px" ShowHeaderWhenEmpty="true">
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
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Tax Amount:"></asp:Label>
                                                <asp:TextBox ID="lbltaxes" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trviewcess" runat="server" visible="false">
                                <td colspan="6">
                                    <table id="tblgridcess" runat="server" class="innertab" align="center" width="750px">
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvcess" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" Width="740px" ShowHeaderWhenEmpty="true">
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
                                                <asp:Label ID="lbl" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Cess Amount:"></asp:Label>
                                                <asp:TextBox ID="txtcess" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trdeduction" runat="server" visible="false">
                                <td colspan="6">
                                    <table id="Table3" runat="server" class="innertab" align="center" width="750px">
                                        <tr align="left">
                                            <td style="width: 750px" align="center">
                                                <asp:Label ID="Label8" runat="server" Font-Size="10px" Width="150px" CssClass="eslbl"
                                                    Text="Deduction DCA Selection"></asp:Label>
                                                <asp:RadioButtonList ID="rbtndeductioncharges" runat="server" Width="200px" AutoPostBack="true"
                                                    CssClass="eslbl" ClientIDMode="AutoID" RepeatDirection="Horizontal" Style="font-size: x-small"
                                                    ToolTip="Deduction Charges Yes or No" OnSelectedIndexChanged="rbtndeductioncharges_SelectedIndexChanged"
                                                    onclick="javascript:return getdeductionIndex(this)">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trdeductiongrid" runat="server">
                                            <td colspan="6">
                                                <asp:GridView runat="server" ID="gvdeduction" HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                    BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames=""
                                                    GridLines="Both" ShowFooter="true" Width="740px" ShowHeaderWhenEmpty="true" OnRowDeleting="gvdeduction_RowDeleting"
                                                    OnRowDataBound="gvdeduction_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkdeduction" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is dedcuted from other CC"  ItemStyle-Width="50px" >
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlothercc" Font-Size="7" Width="50px" CssClass="filter_item"
                                                                    runat="server" OnSelectedIndexChanged="ddlothercc_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CC Code" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlccode" Font-Size="7" Width="150px" OnSelectedIndexChanged="ddlccode_SelectedIndexChanged" AutoPostBack="true" CssClass="filter_item"
                                                                    runat="server">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCA Code" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddldeductiondca" Font-Size="7" Width="150px" CssClass="filter_item"
                                                                    ToolTip="Other DCA" runat="server" OnSelectedIndexChanged="ddldeductiondca_SelectedIndexChanged"
                                                                    AutoPostBack="true" >
                                                                    <%--onchange="checkdeductiondca(this)"--%>
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUBDCA Code" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddldeductionsdca" Font-Size="7" onchange="checkdeductiondca()" Width="150px" CssClass="filter_item"
                                                                    ToolTip="Other SDCA" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtdeductionamount" runat="server" CssClass="filter_item" Width="75px"
                                                                    onkeypress="return numericValidation(this);" onkeyup="calculatededuction(); Total();"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnadddeduction" runat="server" Text="Add More Deduction Dca" OnClientClick="javascript:return verifydeddca();"
                                                                    OnClick="btnadddeduction_Click" />
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
                                                <asp:HiddenField ID="hfdeduction" runat="server" />
                                                <asp:Label ID="Label10" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Deduction Amount:"></asp:Label>
                                                <asp:TextBox ID="txtdeduction" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trret" runat="server" visible="false">
                                <td style="border-right: none">
                                    <asp:Label ID="Label1" runat="server" Font-Size="10px" Text="Advance"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="txtadvance" Width="140px" ToolTip="Advance" onkeyup="Total();" onkeypress="return numericValidation(this);"
                                        runat="server"> </asp:TextBox>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label13" runat="server" Font-Size="10px" Text="Rentention"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="txtretention" Width="140px" ToolTip="Retention" onkeyup="Total();"
                                        onkeypress="return numericValidation(this);" runat="server"> </asp:TextBox>
                                </td>
                                <td style="border-right: none">
                                    <asp:Label ID="Label14" runat="server" Font-Size="10px" Text="Hold"></asp:Label>
                                </td>
                                <td style="border-right: none">
                                    <asp:TextBox ID="txthold" Width="140px" ToolTip="Hold" onkeyup="Total();" onkeypress="return numericValidation(this);"
                                        runat="server"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr id="bank" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblfrombank" runat="server" Text="Bank"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" Width="140px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdate" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                        onkeypress="return false;" runat="server" ToolTip="Credited Date" Width="140px"></asp:TextBox><span
                                            class="starSpan">*</span>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate"
                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                        Animated="true" PopupButtonID="txtdate">
                                    </cc1:CalendarExtender>
                                    <%-- <img onclick="scwShow(document.getElementById('<%=txtdate.ClientID %>'),this);" alt=""
                                        src="images/cal.gif" style="width: 15px; height: 15px;" id="Img2" />--%>
                                </td>
                            </tr>
                            <tr id="bank1" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblmode" runat="server" Text="No"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="140px"></asp:TextBox><span
                                        class="starSpan">*</span>
                                </td>
                                <td>
                                    Amount
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" onkeyup="Amounvalidation(this.value);"
                                        ToolTip="Amount" Width="140px"></asp:TextBox><span class="starSpan">*</span>
                                    <asp:HiddenField ID="hf1" runat="server" />
                                </td>
                            </tr>
                            <tr id="tramt" runat="server" visible="false">
                                <td>
                                    status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlstatus" Font-Size="7" CssClass="filter_item" ToolTip="Status"
                                        runat="server" Width="140px">
                                        <asp:ListItem>Approve</asp:ListItem>
                                        <asp:ListItem>Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Remarks
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Comments" Width="200px"
                                        TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                </td>
                            </tr>
                            <tr id="btn" runat="server" visible="false">
                                <td align="center" colspan="6">
                                    <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                        Text="Submit" OnClientClick="javascript:return validate();" OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                        Text="Reset" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
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
    <script type="text/javascript">
        var anytaxes = 0;
        var cesstaxes = 0;
        function validate() {
            if (document.getElementById("<%=ddlstatus.ClientID %>").value == 'Approve') {

                if (!ChceckRBL("<%=rbtndeductioncharges.ClientID %>")) {
                    return false;
                }
                if (SelectedIndex("<%=rbtndeductioncharges.ClientID %>") == 0) {
                    GridView3 = document.getElementById("<%=gvdeduction.ClientID %>");
                    if (GridView3 != null) {
                        for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                            if (GridView3.rows(rowCount).cells(1).children(0).checked == false) {
                                window.alert("Please verify");
                                return false;
                            }
                            else if (GridView3.rows(rowCount).cells(2).children[0].value == "Select") {
                                window.alert("Select");
                                GridView3.rows(rowCount).cells(2).children[0].focus();
                                return false;
                            }
                            else if (GridView3.rows(rowCount).cells(3).children[0].value == "Select") {
                                window.alert("Select Cost Center");
                                GridView3.rows(rowCount).cells(3).children[0].focus();
                                return false;
                            }
                            else if (GridView3.rows(rowCount).cells(4).children[0].value == "Select") {
                                window.alert("Select DCA");
                                GridView3.rows(rowCount).cells(4).children[0].focus();
                                return false;
                            }
                            else if (GridView3.rows(rowCount).cells(5).children[0].value == "Select") {
                                window.alert("Select Sub DCA");
                                GridView3.rows(rowCount).cells(5).children[0].focus();
                                return false;
                            }
                            else if (GridView3.rows(rowCount).cells(6).children[0].value == "") {
                                window.alert("Enter Amount");
                                GridView3.rows(rowCount).cells(6).children[0].focus();
                                return false;
                            }
                        }
                    }
                }
                var objs = new Array("<%=txtretention.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
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
            else {
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
    </script>
    <script language="javascript" type="text/javascript">
        function Total() {
            //debugger;
            var invoiceval = 0;
            var Netval = 0;
            var basic = document.getElementById("<%=lblbasic.ClientID %>").innerHTML;
            if (document.getElementById("<%=lbltaxes.ClientID %>") != null) {
                var taxes = document.getElementById("<%=lbltaxes.ClientID %>").value;
            }
            else {
                var taxes = "";
            }
            if (document.getElementById("<%=txtcess.ClientID %>") != null) {
                var cess = document.getElementById("<%=txtcess.ClientID %>").value;
            }
            else {
                var cess = "";
            }
            var Advance = document.getElementById("<%=txtadvance.ClientID %>").value;
            if (document.getElementById("<%=txtdeduction.ClientID %>") != null) {
                var deduction = document.getElementById("<%=txtdeduction.ClientID %>").value;
            }
            else {
                var deduction = "";
            }
            var Retention = document.getElementById("<%=txtretention.ClientID %>").value;
            var Hold = document.getElementById("<%=txthold.ClientID %>").value;
            if (basic == "") {
                basic = 0;
            }
            if (taxes == "") {
                taxes = 0;
            }
            if (cess == "") {
                cess = 0;
            }

            if (deduction == "") {
                deduction = 0;
            }
            if (Advance == "") {
                Advance = 0;
            }
            if (Retention == "") {
                Retention = 0;
            }
            if (Hold == "") {
                Hold = 0;
            }
            invoiceval = eval((parseFloat(basic) + parseFloat(taxes) + parseFloat(cess) - parseFloat(deduction)));
            Netval = eval((parseFloat(invoiceval) - parseFloat(Advance) - parseFloat(Retention) - parseFloat(Hold)));
            var roundinvvalue = Math.round(invoiceval * Math.pow(10, 2)) / Math.pow(10, 2);
            var roundNetVal = Math.round(Netval * Math.pow(10, 2)) / Math.pow(10, 2);
            if (roundNetVal >= 0) {
                document.getElementById('<%= txtamt.ClientID%>').value = roundNetVal;

            }
            else {
                alert("Net Amount is not less than Zero");
                return false;
            }
        }

    </script>
</asp:Content>
