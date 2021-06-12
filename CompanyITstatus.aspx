<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="CompanyITstatus.aspx.cs"
    Inherits="CompanyITstatus" Title="Company IT Status" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function validate() {
            var objs = new Array("<%=ddlyear.ClientID %>");
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
                return false;
            }

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
                <table style="width: 700px">
                    <tr>
                        <td align="center">
                            <table width="400px" class="estbl">
                                <tr>
                                    <th align="center">
                                        Overall IT Status Per Year
                                    </th>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table id="paytype" runat="server" class="innertab">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr id="year" runat="server" align="center">
                                                                    <%--<td align="center">
                                                                        <asp:Label ID="lblyear" CssClass="esddown" runat="server" Text="yearly"></asp:Label>
                                                                    </td>--%>
                                                                    <td align="center" colspan="4">
                                                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="date" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Text="Start Date"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtfrom" CssClass="estbox" runat="server" ToolTip="Start Date" Width="100px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                            PopupButtonID="txtfrom">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="End Date"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtto" CssClass="estbox" runat="server" ToolTip="End Date" Width="100px"></asp:TextBox>
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
                                <tr id="btn" runat="server">
                                    <td align="center">
                                        <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                            Text="View" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />&nbsp
                                        <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="griditstatus" runat="server" Width="100%" AutoGenerateColumns="False"
                                Font-Size="Small" ShowFooter="true" CssClass="gridviewstyle" OnRowDataBound="griditstatus_RowDataBound"
                                OnRowCreated="griditstatus_RowCreated">
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <AlternatingRowStyle BackColor="LightGray" />
                                <Columns>
                                    <asp:BoundField DataField="it_name" HeaderText="IT Name" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="dca" HeaderText="IT Code" HeaderStyle-Width="100px" />
                                    <asp:TemplateField HeaderText="Cash">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" ForeColor='<%# Convert.ToInt32(Eval("cash"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("cash") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="cash" HeaderText="Cash" />--%>
                                    <asp:TemplateField HeaderText="Cheque">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" ForeColor='<%# Convert.ToInt32(Eval("cheque"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("cheque") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="cheque" HeaderText="Cheque" />--%>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" ForeColor='<%# Convert.ToInt32(Eval("paidtotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("paidtotal") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="paidtotal" HeaderText="Paid Total" />--%>
                                    <asp:TemplateField HeaderText="Cash">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" ForeColor='<%# Convert.ToInt32(Eval("CashPending"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("CashPending") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="CashPending" HeaderText="Cash Pending" />--%>
                                    <asp:TemplateField HeaderText="Cheque">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" ForeColor='<%# Convert.ToInt32(Eval("ChequePending"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("ChequePending") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="ChequePending" HeaderText="Cheque Pending" />--%>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" ForeColor='<%# Convert.ToInt32(Eval("PendingTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("PendingTotal") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="PendingTotal" HeaderText="Pending Total" />--%>
                                    <asp:TemplateField HeaderText="Cash">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" ForeColor='<%# Convert.ToInt32(Eval("CashTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("CashTotal") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="CashTotal" HeaderText="Cash Total" />--%>
                                    <asp:TemplateField HeaderText="Cheque">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" ForeColor='<%# Convert.ToInt32(Eval("chequeTotal"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("chequeTotal") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="chequeTotal" HeaderText="Cheque Total" />--%>
                                    <asp:TemplateField HeaderText="Gross Total">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" ForeColor='<%# Convert.ToInt32(Eval("Total"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                                Text='<%# Eval("Total") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="Total" HeaderText="Cu Total" />--%>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="tblexpences" runat="server">
                        <td align="center">
                            <table class="estbl">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="BASIC INVOICE VALUE DURING THE CURRENT YEAR"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="DETAILS"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlReceivables" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="CURRENT YEAR WORK IN PROGRESS"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="MISC AND OTHER TAXABLE RECEIPT"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label5" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="TOTAL RECIEPT OF THE YEAR"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="LESS WORK IN PROGRESS CONSIDERED PREVIOUS FINANCIAL YEAR"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="LESS IT DEPRECIATION VALUE"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label8" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="NET PROFIT BEFORE TAX(CONSIDERDED AFTER DEDUCTING THE EXPENSES)"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>

    

</asp:Content>
