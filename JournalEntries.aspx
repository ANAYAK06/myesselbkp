<%@ Page Title="Journal Entry" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="JournalEntries.aspx.cs" Inherits="JournalEntries" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .FormatRadioButtonList label {
            margin-right: 30px;
        }

        .bottomBorder td {
            border-color: White;
            border-style: solid;
            border-bottom-width: 100px;
        }
    </style>
    <script type="text/javascript">
        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=gvDetails.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                }
            }
        }
        function Checking() {
            //debugger;
            var grid = document.getElementById("<%= gvDetails.ClientID %>");
            if (grid != null) {
                for (var rowCount = 1; rowCount < grid.rows.length - 1; rowCount++) {
                    if (grid.rows(rowCount).cells(1).children[0].value == "0") {
                        grid.rows(rowCount).cells(3).children[0].value = "";
                        window.alert("Please Select Ledger");
                        grid.rows(rowCount).cells(1).children[0].focus();
                        return false;
                    }
                    if (grid.rows(rowCount).cells(2).children[0].value == "Select") {
                        grid.rows(rowCount).cells(3).children[0].value = "";
                        window.alert("Please Select Ledger Type");
                        grid.rows(rowCount).cells(2).children[0].focus();
                        return false;
                    }
                }
            }
        }
        function Cleartxtamt(val) {
            //debugger;
            var grid = document.getElementById("<%= gvDetails.ClientID %>");
             var currentDropDownValue = document.getElementById(val.id).value;
             var rowData = val.parentNode.parentNode;
             var rowIndex = rowData.rowIndex;
             if (grid != null) {
                 for (var rowCount = 1; rowCount < grid.rows.length - 1; rowCount++) {
                     if (currentDropDownValue == "Select") {
                         grid.rows(rowIndex).cells(3).children[0].value = "";
                         window.alert("Please Select Ledger Type");
                         grid.rows(rowIndex).cells(2).children[0].focus();
                         calculateamt();
                         return false;
                     }
                     else {
                         calculateamt();
                         return true;
                     }
                 }
             }
         }
         function calculateamt() {
             //debugger;
             grd = document.getElementById("<%=gvDetails.ClientID %>");
            var hfcredit = document.getElementById("<%=hfcreditamt.ClientID %>").value;
            var hfdebit = document.getElementById("<%=hfdebitamt.ClientID %>").value;
            var credit = 0;
            var debit = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells[3].innerHTML) != 0.00) {
                        if (grd.rows(rowCount).cells(2).children[0].value == "Credit") {
                            if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                                credit += Number(grd.rows(rowCount).cells(3).children[0].value);
                                hfcredit = credit;

                            }
                        }
                        if (grd.rows(rowCount).cells(2).children[0].value == "Debit") {
                            if (!isNaN(grd.rows(rowCount).cells(3).children[0].value)) {
                                debit += Number(grd.rows(rowCount).cells(3).children[0].value);
                                hfdebit = debit

                            }
                        }
                        if (hfcredit == "")
                            hfcredit = 0;
                        if (hfdebit == "")
                            hfdebit = 0;
                        document.getElementById("<%=lblcreditamt.ClientID %>").value = hfcredit;
                        document.getElementById("<%=lbldebitamt.ClientID %>").value = hfdebit;
                    }
                }
            }
            else {
                document.getElementById("<%=lblcreditamt.ClientID %>").value = 0;
                document.getElementById("<%=lbldebitamt.ClientID %>").value = 0;
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="700px">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="700px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text="Accounting Journal Entries"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                    <table id="tblled" style="border: 1px solid #000" runat="server" width="700px">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvDetails" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="700px" GridLines="None" ShowFooter="true" OnRowDataBound="gvDetails_RowDataBound" OnRowDeleting="gvDetails_RowDeleting">
                                                    <%-- OnRowCreated="gvDetails_RowCreated"--%>
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ledger Name" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlledgername" runat="server">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit/Debit" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlledgertype" Width="100px" runat="server" onchange="Cleartxtamt(this)">
                                                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                                    <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                                                    <asp:ListItem Text="Debit" Value="Debit"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmt" runat="server" Width="100px" onkeyup="Checking();calculateamt();" onkeypress='IsNumeric1(event)' />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Enter Amount"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup='valGroup1' ControlToValidate="txtAmt"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAdd" runat="server" ValidationGroup="valGroup1" OnClick="btnAdd_Click"
                                                                    ImageUrl="~/images/imgadd1.gif" />
                                                                <%--  <asp:Button ID="btnAdd" runat="server" ValidationGroup='valGroup1' Text="Add Group"  OnClick="btnAdd_Click" />--%>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ControlStyle-Width="50px" ShowDeleteButton="true" DeleteText="Remove" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr class="pagerbar" id="description" runat="server">
                                            <td width="700px">
                                                <table class="search_table" width="100%" align="center">
                                                    <tr>
                                                        <td class="item item-selection" valign="middle">
                                                            <asp:TextBox ID="txtdate" Font-Size="Small" runat="server" ToolTip="Date" Style="width: 130px; vertical-align: middle" onkeydown="return false;" onpaste ="return false;" ></asp:TextBox>  <%--onKeyDown="preventBackspace();"--%>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                PopupButtonID="txtdate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td class="item item-selection" align="left" width="" colspan="2">
                                                            <asp:Label ID="lbldate" Font-Bold="true" Font-Size="Smaller" runat="server" Text="Narration"></asp:Label>
                                                        </td>

                                                        <td class="item item-selection" valign="middle" align="left">
                                                            <asp:TextBox ID="txtdesc" runat="server" CssClass="filter_item" ToolTip="Narration"
                                                                TextMode="MultiLine" Width="450px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="pagerbar" id="Tr1" runat="server">
                                            <td width="700px">
                                                <table class="search_table" width="100%" align="center">
                                                    <tr>
                                                        <td class="item item-selection" align="left" width="" colspan="2">
                                                            <asp:Label ID="Label1" Font-Bold="true" Font-Size="Smaller" runat="server" Text="Credit"></asp:Label>
                                                        </td>
                                                        <td class="item item-selection" align="left" width="" colspan="2">
                                                            <asp:HiddenField ID="hfcreditamt" runat="server" />
                                                            <asp:TextBox ID="lblcreditamt" Width="50px" onkeydown="return false;" onpaste ="return false;" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="item item-selection" align="left" width="" colspan="2">
                                                            <asp:Label ID="Label3" Font-Bold="true" Font-Size="Smaller" runat="server" Text="Debit"></asp:Label>
                                                        </td>
                                                        <td class="item item-selection" align="left" width="" colspan="2">
                                                            <asp:HiddenField ID="hfdebitamt" runat="server" />
                                                            <asp:TextBox ID="lbldebitamt" Width="50px" onkeydown="return false;" onpaste ="return false;" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" OnClientClick="javascript:return validate();"
                                                    Style="font-size: small" Text="Submit" OnClick="btnAssign_Click" />
                                                <%-- --%>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
    function preventBackspace(e) {
        var evt = e || window.event;
        if (evt) {
            var keyCode = evt.charCode || evt.keyCode;
            if (keyCode === 8) {
                if (evt.preventDefault) {
                    evt.preventDefault();
                } else {
                    evt.returnValue = false;
                }
            }
        }
    }
</script>
    <script type="text/javascript">

        function validate() {
            GridView = document.getElementById("<%=gvDetails.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(1).children[0].value == "0") {
                        window.alert("Please Select Ledger Type");
                        GridView.rows(rowCount).cells(1).children[0].focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(2).children[0].value == "Select") {
                        window.alert("Please Select Ledger Type");
                        GridView.rows(rowCount).cells(2).children[0].focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(3).children[0].value == "") {
                        window.alert("Please Enter Amount");
                        GridView.rows(rowCount).cells(3).children[0].focus();
                        return false;
                    }

                }
            }
            var objs = new Array("<%=txtdate.ClientID %>", "<%=txtdesc.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=btnAssign.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
</asp:Content>

