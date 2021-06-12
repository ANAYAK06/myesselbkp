<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewInvoiceDetails.aspx.cs"
    Inherits="ViewInvoiceDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr id="grid" runat="server">
                <td align="center" colspan="2">
                    <asp:GridView ID="grd" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                        AutoGenerateColumns="False" Width="366px" CellPadding="4" ForeColor="#333333"
                        GridLines="None" ShowFooter="true" Font-Size="Small">
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <%--<asp:CommandField ShowSelectButton="True" />--%>
                            <asp:BoundField DataField="invoiceno" HeaderText="InvoiceNo" InsertVisible="False"
                                ReadOnly="True" />
                            <asp:BoundField DataField="cc_code" HeaderText="CC CODE" />
                            <asp:BoundField DataField="dca_code" HeaderText="DCA CODE" />
                            <asp:BoundField DataField="vendor_id" HeaderText="Vendor ID" />
                            <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:#,##,##,###.00}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="netamount" HeaderText="Net Amount" DataFormatString="{0:#,##,##,###.00}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="tds" HeaderText="TDS" DataFormatString="{0:#,##,##,###.00}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="retention" HeaderText="Retention" DataFormatString="{0:#,##,##,###.00}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="hold" HeaderText="Hold" DataFormatString="{0:#,##,##,###.00}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="debit" HeaderText="Paid" DataFormatString="{0:#,##,##,###.00}"
                                HtmlEncode="false" />
                        </Columns>
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <%-- <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />--%>
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:GridView ID="gvtds" runat="server" CssClass="mGrid" AllowPaging="false" AllowSorting="True"
                        AutoGenerateColumns="False" Width="530px" CellPadding="4" ForeColor="#333333"
                        GridLines="None" ShowFooter="true" Font-Size="Small" OnRowDataBound="gvtds_RowDataBound"  >
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <%--<asp:CommandField ShowSelectButton="True" />--%>
                            <asp:BoundField DataField="invoiceno" HeaderText="InvoiceNo" InsertVisible="False"
                                ReadOnly="True" />
                            <asp:BoundField DataField="Po_No" HeaderText="PO NO" InsertVisible="False"
                            ReadOnly="True" />
                            <asp:BoundField DataField="cc_code" HeaderText="CC CODE" />
                            <asp:BoundField DataField="dca_code" HeaderText="DCA CODE" />
                            <asp:BoundField DataField="Subdca_Code" HeaderText="SDCA CODE" />
                            <asp:BoundField DataField="vendor_id" HeaderText="Vendor ID" />
                            <asp:BoundField DataField="Amount" HeaderText="Total" DataFormatString="{0:#,##,##,###.00}"
                                HtmlEncode="false" />                           
                            <asp:BoundField DataField="DebitAmount" HeaderText="Debit Amount" DataFormatString="{0:#,##,##,###.00}"
                                HtmlEncode="false" />                          
                        </Columns>
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <%-- <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />--%>
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
