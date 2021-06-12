<%@ Page Title="Vat Report" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VATReport.aspx.cs" Inherits="VATReport" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function searchvalidate() {
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                var objs = new Array("<%=ddlvatno.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
            }
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                var objs = new Array("<%=ddltype.ClientID %>", "<%=ddlvatno.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
                if (!CheckInputs(objs)) {
                    return false;
                }
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
    <script type="text/javascript">
        function checkDate(sender, args) {
            // debugger;
            var str1 = document.getElementById("<%=txtfrom.ClientID %>").value;
            var str2 = "01-Apr-2017";
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
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                if (date4 > date3) {
                    alert("Please Select on OR Before[30-MAR-2017] Date ");
                    document.getElementById("<%=txtfrom.ClientID %>").value = "";
                    return false;
                }
            }
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                if (date3 > date4) {
                    alert("Please Select on OR After[01-APR-2017] Date ");
                    document.getElementById("<%=txtfrom.ClientID %>").value = "";
                    document.getElementById("<%=txtto.ClientID %>").value = "";
                    return false;
                }
            }
            document.getElementById("<%=txtto.ClientID %>").value = "";
            return true;
        }
    </script>
    <script type="text/javascript">
        function checktoDate(sender, args) {
            // debugger;
            var str1 = document.getElementById("<%=txtto.ClientID %>").value;
            var str2 = "01-Apr-2017";
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
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 0) {
                if (date4 > date3) {
                    alert("Please Select on OR Before[30-MAR-2017] Date ");
                    document.getElementById("<%=txtto.ClientID %>").value = "";
                    return false;
                }
            }
            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                if (date3 > date4) {
                    alert("Please Select on OR After[01-APR-2017] Date ");
                    document.getElementById("<%=txtto.ClientID %>").value = "";
                    return false;
                }
            }
            //document.getElementById("<%=txtto.ClientID %>").value = "";
            return true;
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
                        <table>
                            <tr align="center">
                                <td>
                                    <table class="estbl" width="700px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="5" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="VIEW SALES TAX/VAT REPORT"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="6" align="center" valign="middle">
                                                <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Font-Bold="true" Font-Names="Courier New"
                                                    Font-Size="Small" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                    CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="old">Old Report</asp:ListItem>
                                                    <asp:ListItem Value="New">New Report</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trtypeselection" runat="server">
                                            <td colspan="6" align="center" valign="middle">
                                                <asp:Label ID="Label2" runat="server" CssClass="esrbtn" Text="Report Type"></asp:Label>
                                                <asp:DropDownList ID="ddltype" CssClass="esddown" ToolTip="Report Type" runat="server">
                                                    <asp:ListItem>select</asp:ListItem>
                                                    <asp:ListItem Value="Creditable">Creditable</asp:ListItem>
                                                    <asp:ListItem Value="Non-Creditable">Non-Creditable</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="dateselection" runat="server">
                                            <td colspan="6" align="center" valign="middle">
                                                <asp:RadioButton ID="rbtninvdate" runat="server" Text="Paid Date" GroupName="dt"
                                                    Font-Bold="true" Font-Names="Courier New" Font-Size="Small" Checked="true" ForeColor="Black" />&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rbtninvmkdate" runat="server" Text="Invoice Making Date" GroupName="dt"
                                                    Font-Bold="true" Font-Names="Courier New" Font-Size="Small" ForeColor="Black" />
                                            </td>
                                        </tr>
                                        <tr id="trvatnoselection" runat="server">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Vat No"></asp:Label>
                                            </td>
                                            <td colspan="6" align="center">
                                                <asp:DropDownList ID="ddlvatno" CssClass="esddown" Width="200px" runat="server" ToolTip="Vat No">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="date" runat="server">
                                            <td>
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="From Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfrom" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="From Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="checkDate"
                                                    FirstDayOfWeek="Monday" Animated="true" PopupButtonID="txtfrom">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="To Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtto" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="To Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtto"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checktoDate"
                                                    Animated="true" PopupButtonID="txtto">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr align="center" id="trbtn" runat="server">
                                            <td colspan="4">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View Report" OnClientClick="javascript:return searchvalidate()" OnClick="btnAssign_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowCreated="GridView1_RowCreated" Font-Size="X-Small" CssClass="grid-content"
                                        BorderColor="Black" HeaderStyle-CssClass="grid-header" EmptyDataText="No Data Avaliable for Selected Criteria.."
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        PagerStyle-CssClass="grid pagerbar" ShowFooter="true" FooterStyle-BackColor="White"
                                        OnRowDataBound="GridView1_RowDataBound" FooterStyle-Font-Bold="true">
                                        <Columns>
                                            <asp:BoundField DataField="Date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date"
                                                ItemStyle-Wrap="false" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="ClientInvoiceNo" HeaderText="InvoiceNo" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="Client Name" HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="ClientReg No" HeaderText="Customer VAT" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="Client Basic" HeaderText="Basic Value" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="Client Excise" HeaderText="Excise duty" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="Client VAT" HeaderText="Net Sales Tax/VAT" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="VendorInvoiceNo" HeaderText="InvoiceNo" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="Vendor Name" HeaderText="Vendor Name" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="VendorReg No" HeaderText="Vendor VAT" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Vendor Basic" HeaderText="Basic Value" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="Vendor Excise" HeaderText="Excise duty" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="Vendor VAT" HeaderText="VAT (ITR)" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="Govt" HeaderText="Sales Tax/VAT" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="PTax" HeaderText="Penal Interest" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" OnClick="btnExcel_Click" runat="server" ImageUrl="~/images/ExcelImage.jpg" />
            </td>
        </tr>
    </table>
</asp:Content>
