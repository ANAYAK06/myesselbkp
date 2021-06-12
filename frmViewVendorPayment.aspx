<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmViewVendorPayment.aspx.cs"
    Inherits="Admin_frmViewVendorPayment" EnableEventValidation="false" Title="View Vendor Payment- Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function validate() {


            var objs = new Array("<%=ddltypeofpay.ClientID %>", "<%=ddlsubtype.ClientID %>", "<%=ddlvendor.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddlpo.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;

            }
            var str1 = document.getElementById("<%=txtfrom.ClientID %>").value;
            var str2 = document.getElementById("<%=txtto.ClientID %>").value;
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
                alert("Invalid date");
                document.getElementById("<%=txtto.ClientID %>").focus();
                document.getElementById("<%=txtto.ClientID %>").value = "";
                return false;
            }
            return true;
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
    <script language="javascript" type="text/javascript">
        function expandcollapse(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "images/minus.png";
                }
                else {
                    img.src = "images/minus.png";
                }
                img.alt = "Close to view other Vendor Details";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "images/plus.png";
                }
                else {
                    img.src = "images/plus.png";
                }
                img.alt = "Expand to show Vendor Details";
            }
        } 
    </script>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="Menu1" runat="server" />
            </td>
            <td>
                <table style="width: 750px">
                    <tr>
                        <td align="center">
                            <table class="estbl eslbl" width="700px">
                                <tr>
                                    <th align="center" class="style9">
                                        View vendor Payment
                                    </th>
                                </tr>
                                <tr>
                                    <td class="innertab">
                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    <tr id="payment" runat="server" visible="true">
                                                        <td>
                                                            Type of Payment:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddltypeofpay" runat="server" ToolTip="Type of Payment" AutoPostBack="true"
                                                                CssClass="esddown" OnSelectedIndexChanged="ddltypeofpay_SelectedIndexChanged">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <%--<asp:ListItem>Trade Purchasing</asp:ListItem>--%>
                                                                <asp:ListItem>Service</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlsubtype" runat="server" ToolTip="Service Type" AutoPostBack="true"
                                                                CssClass="esddown" OnSelectedIndexChanged="ddlsubtype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlvendor" Width="275px" CssClass="esddown" ToolTip="Vendor"
                                                                runat="server" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged" AutoPostBack="true"
                                                                onchange="SetDynamicKey('dp5',this.value);">
                                                            </asp:DropDownList>
                                                            <%--<br />
                                                            <asp:Label ID="lblvendor" class="ajaxspan" runat="server"></asp:Label>
                                                            <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp5" runat="server"
                                                                TargetControlID="lblvendor" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                                ServiceMethod="GetVendorName">
                                                            </cc1:DynamicPopulateExtender>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <table class="innertab" visible="false" runat="server" id="cc">
                                                                <tr>
                                                                    <td width="100px">
                                                                        CC-Code:
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="250px"
                                                                            CssClass="esddown" AutoPostBack="true" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <%--<span class="starSpan">*</span>--%>
                                                                        <%--  <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                            ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="CC"
                                                                            PromptText="Select Cost Center">
                                                                        </cc1:CascadingDropDown>--%>
                                                                    </td>
                                                                    <td>
                                                                        <table width="100%" class="innertab" runat="server" id="Dca">
                                                                            <tr>
                                                                                <td align="left" width="100px">
                                                                                    <asp:Label ID="lblpono" CssClass="eslbl" runat="server" Text="PO NO"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlpo" CssClass="esddown" Width="200px" runat="server" ToolTip="PO">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table class="innertab" visible="false" runat="server" id="year">
                                                                <tr align="center">
                                                                    <td>
                                                                        <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="From Date"></asp:Label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="txtfrom" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                            onkeypress="return false;" runat="server" ToolTip="From Date" Width="100px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                            PopupButtonID="txtfrom">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <table width="100%" class="innertab" runat="server" id="Table1">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="To Date"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtto" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                        onkeypress="return false;" runat="server" ToolTip="To Date" Width="100px"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtto"
                                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                        PopupButtonID="txtto">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddltypeofpay" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                            Text="View" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />&nbsp
                                        <asp:Button ID="btnCancel1" CssClass="esbtn" runat="server" Style="font-size: small;"
                                            Text="Reset" OnClick="btnCancel1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellspacing="0" cellpadding="0">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                    OnRowDataBound="GridView1_RowDataBound" BorderColor="White" CssClass="mGrid"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Font-Size="Small"
                                    GridLines="Vertical" ShowFooter="true" DataKeyNames="InvoiceNo">
                                    <%--   <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>--%>
                                    <%--   <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>--%>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="javascript:expandcollapse('div<%# Eval("InvoiceNo") %>', 'one');">
                                                    <img id="imgdiv<%# Eval("InvoiceNo") %>" alt="Click to show/hide Orders for Customer <%# Eval("InvoiceNo") %>"
                                                        width="9px" border="0" src="images/plus.png" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="invoice_date" HeaderText="Bill Date" ItemStyle-Wrap="false" />
                                         <asp:BoundField DataField="vendorname" HeaderText="Name" ItemStyle-Wrap="true" />
                                        <asp:BoundField DataField="invoiceno" HeaderText="InvoiceNo" ItemStyle-Wrap="false"
                                            ItemStyle-Width="75px" />
                                        <asp:BoundField DataField="cc_code" HeaderText="CC Code" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="dca_code" HeaderText="DCA Code" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Basicvalue" HeaderText="Basic Value" />
                                        <asp:BoundField DataField="Servicetax" HeaderText="Service Tax" />
                                        <asp:BoundField DataField="total" HeaderText="Total" />
                                        <asp:BoundField DataField="TaxAmt" HeaderText="TaxAmt" />
                                        <asp:BoundField DataField="tds" HeaderText="Tds" />
                                        <asp:BoundField DataField="retention" HeaderText="Retention" />
                                        <asp:BoundField DataField="advance" HeaderText="Advance" />
                                        <asp:BoundField DataField="hold" HeaderText="Hold" />
                                        <asp:BoundField DataField="anyother" HeaderText="Any Other" />
                                        <asp:BoundField DataField="netamount" HeaderText="Net Amount" />
                                        <asp:BoundField DataField="tds balance" HeaderText="TDS Balance" />
                                        <asp:BoundField DataField="retention balance" HeaderText="Retention Balance" />
                                        <asp:BoundField DataField="hold balance" HeaderText="Hold Balance" />                                        
                                        <asp:BoundField DataField="paid" HeaderText="Paid" />
                                        <asp:BoundField DataField="invoice balance" HeaderText="Invoice Balance" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" align="center">
                                                        <div id="div<%# Eval("InvoiceNo") %>" style="display: none; position: relative; left: 15px;
                                                            overflow: auto; width: 97%">
                                                            <asp:GridView ID="Grdviewpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                OnRowDataBound="Grdviewpopup_RowDataBound"
                                                                ShowFooter="true">
                                                                <Columns>
                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                                    <asp:BoundField DataField="CC_Code" HeaderText="CC Code" ItemStyle-Width="100px" />
                                                                    <asp:BoundField DataField="Dca_Code" HeaderText="Dca Code"/>
                                                                    <asp:BoundField DataField="Subdca_Code" HeaderText="Subdca Code"/>                                                                  
                                                                    <asp:BoundField DataField="RegdNo" HeaderText="Regd No" />
                                                                    <asp:BoundField DataField="Tax_Type" HeaderText="Tax Type" />
                                                                    <asp:BoundField DataField="Type" HeaderText="Type" />
                                                                    <asp:BoundField DataField="tbal" HeaderText="Balance" ItemStyle-Width="100px"
                                                                        DataFormatString="{0:#,##,##,###.00}" />
                                                                    <asp:BoundField DataField="tamt" HeaderText="Total Amount" ItemStyle-Width="100px"
                                                                        DataFormatString="{0:#,##,##,###.00}" />
                                                                </Columns>
                                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                <HeaderStyle CssClass="grid-header" />
                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                            </asp:GridView>
                                                            <asp:GridView ID="GridView2" Width="50%" BorderColor="White" AutoGenerateColumns="False"
                                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                Font-Size="Small" GridLines="Vertical" runat="server" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Date" HeaderText="Paid Date" />
                                                                    <asp:BoundField DataField="description" HeaderText="Description" />
                                                                    <asp:BoundField DataField="Bank_name" HeaderText="Bank Name" />
                                                                    <asp:BoundField DataField="modeofpay" HeaderText="Mode Of Pay" />
                                                                    <%-- <asp:BoundField DataField="Debit" HeaderText="Paid Amount" />--%>
                                                                    <asp:TemplateField HeaderText="Net Paid">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("debit").ToString()%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnPrintCurrent" runat="server" Text="Print" OnClick="PrintAllPages" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
