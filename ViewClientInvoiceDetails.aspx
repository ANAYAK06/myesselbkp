<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewClientInvoiceDetails.aspx.cs"
    Inherits="ViewClientInvoiceDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/listgrid.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script language="javascript" type="text/javascript">

        function print() {

            var grid_obj = document.getElementById("<%=print.ClientID %>");

            var new_window = window.open('print.html'); //print.html is just a dummy page with no content in it.
            new_window.document.write(grid_obj.outerHTML);
            new_window.print();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="900px">
            <tr valign="top" align="center">
                <td>
                    <table id="_terp_list_grid" class="" width="100%" align="center" style="background: none;
                        border-color: black;">
                        <tr id="print" runat="server" align="center">
                            <td>
                                <table>
                                    <tr style="border: 1px solid #000">
                                        <th valign="top" align="center">
                                            <asp:Label ID="lblbudget" CssClass="esfmhead" runat="server" Text="Client Taxes"
                                                Font-Size="Small"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr id="gridprint" runat="server" align="center">
                                        <td style="border: thin solid #000000">
                                            <asp:GridView ID="gvclienttaxes" runat="server" GridLines="Both" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                AutoGenerateColumns="true" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                Width="100%" ShowFooter="true" OnRowDataBound="gvclienttaxes_RowDataBound" EmptyDataText="There Are No Records">
                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                <PagerStyle CssClass="grid pagerbar" />
                                                <HeaderStyle CssClass="grid-header" />
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                                    LastPageText="Last" />
                                            </asp:GridView>
                                            <br />
                                            <asp:Label ID="Label1" CssClass="esfmhead" runat="server" Text="Client Deductions"
                                                Font-Size="Small"></asp:Label><br />
                                            <asp:GridView ID="gvclientdeduction" runat="server" GridLines="Both" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                AutoGenerateColumns="true" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                Width="100%" ShowFooter="true" OnRowDataBound="gvclientdeduction_RowDataBound" EmptyDataText="There Are No Records">
                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                <PagerStyle CssClass="grid pagerbar" />
                                                <HeaderStyle CssClass="grid-header" />
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                                    LastPageText="Last" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="style2" width="400px" height="30px" id="btnprint" runat="server" align="center">
                            <td align="center">
                                <input class="buttonSubmit" onclick="print();" type="button" value="Print" title="Print Report" />
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trexcel" runat="server">
                            <td style="height: 20px" align="left">
                                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"
                                    Font-Size="Small"></asp:Label>
                                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                    OnClick="btnExcel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
