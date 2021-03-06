<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="StockPurchaseReport.aspx.cs" Inherits="StockPurchaseReport" Title="Purchase Report - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function searchvalidate() {
            var objs = new Array("<%=ddltaxno.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
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
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="VIEW ITEMS PURCHASE REPORT"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="trtaxnoselection" runat="server">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Tax No"></asp:Label>
                                            </td>
                                            <td colspan="6" align="center">
                                                <asp:DropDownList ID="ddltaxno" CssClass="esddown" Width="200px" runat="server" ToolTip="Tax No">
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
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy"
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
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday"
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
                                <td></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                        Font-Size="X-Small" CssClass="grid-content"
                                        BorderColor="Black" HeaderStyle-CssClass="grid-header" EmptyDataText="No Data Avaliable for Selected Criteria.."
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        PagerStyle-CssClass="grid pagerbar" ShowFooter="true" FooterStyle-BackColor="White"
                                        FooterStyle-Font-Bold="true" OnRowDataBound="GridView1_RowDataBound" GridLines="Both">
                                        <Columns>
                                            <asp:BoundField DataField="TComanyGSTIN" HeaderText="Our GSTIN" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="TSupplierName" HeaderText="Supplier Name" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="true" />
                                            <asp:BoundField DataField="TSupplierGSTIN" HeaderText="GSTIN" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TInvoiceNo" HeaderText="InvoiceNo" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="TInvoice_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Invoice Date"
                                                ItemStyle-Wrap="false" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="TItemCode" HeaderText="Item Code" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TItemDescription" HeaderText="Item Description" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="THSN" HeaderText="HSN" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TUOM" HeaderText="UOM" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TQuantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TRate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"
                                                DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="TIGSTRate" HeaderText="IGST%" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="" HeaderText="IGST Amt" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:n2}" />
                                            <asp:BoundField DataField="TCGSTRate" HeaderText="CGST%" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="" HeaderText="CGST Amt" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="TSGSTRate" HeaderText="SGST%" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="" HeaderText="SGST Amt" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="" HeaderText="Total Amount" ItemStyle-HorizontalAlign="Right"
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

    </table>
    <table>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top"></td>
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" OnClick="btnExcel_Click" ImageUrl="~/images/ExcelImage.jpg" />
            </td>
        </tr>
    </table>
</asp:Content>
