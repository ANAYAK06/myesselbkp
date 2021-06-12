<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITSummary.aspx.cs" Inherits="ITSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/listgrid.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="850px">
            <tr>
                <td align="right">
                    <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                    <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                        OnClick="btnExcel_Click" />
                </td>
            </tr>
            <tr>
                <td class="grid-content" align="center">
                    <table id="_terp_list_grid" class="" width="100%" align="center" style="background: none;
                        border-color: black;">
                        <asp:GridView ID="GridView1" BorderColor="Black" GridLines="Both" runat="server"
                            AutoGenerateColumns="False" Font-Size="Small" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                            AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                            PagerStyle-CssClass="grid pagerbar" FooterStyle-BackColor="LightGray" FooterStyle-Font-Bold="true"
                            ShowFooter="true" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Invoice Date" ItemStyle-Width="100px" HeaderText="Invoice Date" />
                                <asp:BoundField DataField="Paid Date" ItemStyle-Width="100px" HeaderText="Paid Date" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="internalcredit" HeaderText="Credit" />
                                <asp:BoundField DataField="internaldebit" HeaderText="Debit" />
                                <asp:BoundField DataField="cashpaid" HeaderText="Paid" />
                                <asp:BoundField DataField="cashpayable" HeaderText="Payable" />
                                <asp:BoundField DataField="invoicepaid" HeaderText="Paid" />
                                <asp:BoundField DataField="invoicepayable" HeaderText="Payable" />
                                <asp:BoundField DataField="Total Debit" HeaderText="Total Debit" />
                            </Columns>
                        </asp:GridView>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" CssClass="esddown" Text="Total Net IT Debit :- "></asp:Label>
                    <asp:Label ID="lbltotal" runat="server" Text="" CssClass="eslblalert"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
