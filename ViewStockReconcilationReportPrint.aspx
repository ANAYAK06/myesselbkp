<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStockReconcilationReportPrint.aspx.cs"
    Inherits="ViewStockReconcilationReportPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function Print() {
            // The following line is required to show the print dialog after
            // mail is rendered. Affects IE browser.
            document.body.offsetHeight;
            window.print();
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="align: Center">
        <asp:Panel ID="pnl" runat="server">
            <table width="900px" id="tbldetail" runat="server">
                <tr align="center">
                    <td>
                        <asp:Label runat="server" ID="lblheader" Font-Bold="true" BackColor="Black" ForeColor="White"
                            Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr align="center" id="trgrd" runat="server">
                    <td>
                        <asp:GridView ID="Gvdetails" runat="server" Width="100%" AutoGenerateColumns="False"
                            Font-Size="Small" CssClass="gridviewstyle" BorderColor="Black" HeaderStyle-CssClass="GridViewHeaderStyle"
                            AlternatingRowStyle-CssClass="GridViewAlternatingRowStyle" RowStyle-CssClass="GridViewRowStyle"
                            PagerStyle-CssClass="GridViewPagerStyle" OnRowDataBound="Gvdetails_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="item_code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[0] --%>
                                <asp:BoundField DataField="recieved_cc" HeaderText="" ItemStyle-HorizontalAlign="Left" />
                                <%--Cell[1] --%>
                                <asp:BoundField DataField="quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[2] --%>
                                <asp:BoundField DataField="No" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[3] --%>
                                <asp:BoundField DataField="po_no" HeaderText="Purchase Order No" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[4] --%>
                                <asp:BoundField DataField="Date" HeaderText="Recieved Date" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[5] --%>
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[6] --%>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="left" id="trgrdasset" runat="server">
                    <td align="left">
                        <asp:GridView ID="Grdassets" runat="server" Width="80%" AutoGenerateColumns="False"
                            Font-Size="Small" CssClass="gridviewstyle" BorderColor="Black" HeaderStyle-CssClass="GridViewHeaderStyle"
                            AlternatingRowStyle-CssClass="GridViewAlternatingRowStyle" RowStyle-CssClass="GridViewRowStyle"
                            PagerStyle-CssClass="GridViewPagerStyle">
                            <Columns>
                                <asp:BoundField DataField="item_code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[0] --%>
                                <asp:BoundField DataField="ref_no" HeaderText="Reference No" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[1] --%>
                                <asp:BoundField DataField="transfer_date" HeaderText="Dispatched Date" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[2] --%>
                                <asp:BoundField DataField="RecievedDate" HeaderText="Recieved Date" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[3] --%>
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" />
                                <%--Cell[4] --%>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center" id="trprint" runat="server">
                    <td>
                        <input class="buttonSubmit" onclick="print();" type="button" value="Print" title="Print Report" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                        <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                            OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
