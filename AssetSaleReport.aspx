<%@ Page Title="Asset Sold Report" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="AssetSaleReport.aspx.cs" Inherits="AssetSaleReport" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {

            var objs = new Array("<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");

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
                 <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <div style="width: 750px; padding-top: 5px;" align="center">
                    <table>
                        <tr align="center">
                            <td align="center">
                                <table width="500px" style="border: 1px solid #000" class="estbl">
                                    <tr>
                                        <th align="center" class="style9">
                                            Asset Sold Report
                                        </th>
                                    </tr>
                                    <tr id="trdates" runat="server" align="center">
                                        <td>
                                            <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table class="innertab" visible="false">
                                                        <tr align="center">
                                                            <td align="center">
                                                                <table id="Statement" runat="server">
                                                                    <tr id="paytype" runat="server" align="center">
                                                                        <td>
                                                                            <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="From Date"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtfrom" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                onkeypress="return false;" runat="server" ToolTip="From Date" Width="100px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                PopupButtonID="txtfrom">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="To Date"></asp:Label>
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr id="trbtns" runat="server">
                                        <td align="center">
                                            <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                                Text="View" OnClientClick="javascript:return validate()" OnClick="btnsubmit_Click" />&nbsp
                                            <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small" OnClick="btncancel_Click" />
                                            <%----%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trgrid" runat="server">
                            <td colspan="2">
                                <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true"
                                    GridLines="None" Font-Size="Small" Width="700px" OnRowDataBound="GridView1_RowDataBound" >
                                    <Columns>
                                        <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="buyer_name" HeaderText="Buyer Name" />
                                        <asp:BoundField DataField="request_no" HeaderText="Request No" />
                                        <asp:BoundField DataField="item_code" HeaderText="Item Code" />
                                        <asp:BoundField DataField="payment_type" HeaderText="Payment Type" />
                                        <asp:BoundField DataField="amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}"
                                            HtmlEncode="False" />
                                        <asp:BoundField DataField="status" HeaderText="Status" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg" OnClick="btnExcel_Click" />              
            </td>
        </tr>
    </table>
</asp:Content>
