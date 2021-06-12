<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCADetailviewReport.aspx.cs"
    Inherits="DCADetailviewReport" %>

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
                        <tr id="print" runat="server">
                            <td>
                                <table>
                                    <tr style="border: 1px solid #000">
                                        <th valign="top" align="center">
                                            <asp:Label ID="lblbudget" CssClass="esfmhead" runat="server" Text="BUDGET DETAILVIEW OF "
                                                Font-Size="Small"></asp:Label>
                                            <asp:Label ID="lbldcacode" CssClass="esfmhead" runat="server" Text="" Font-Size="Small"></asp:Label>
                                        </th>
                                    </tr>
                                     <tr style="border: 1px solid #000">
                                        <th valign="top" align="center">
                                            <asp:Label ID="lblbasicsubmited" CssClass="esfmhead" runat="server" Text="CUMULATIVE BASIC INVOICE SUBMITTED AGAINST CC"
                                                Font-Size="Small"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr style="border: 1px solid #000">
                                        <th valign="top" align="center">
                                            <asp:Label ID="lblbasicrecipt" CssClass="esfmhead" runat="server" Text="CUMULATIVE BASIC INVOICE RECEIPT TILL DATE AGAINST CC"
                                                Font-Size="Small"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label1" CssClass="esfmhead" runat="server" Text="CC Code : " Font-Size="Small"></asp:Label>
                                            <asp:Label ID="lblcccode" CssClass="esfmhead" runat="server" Text="" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblnodata" CssClass="esfmhead" runat="server" Text=" " Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="gridprint" runat="server" align="center">
                                        <td style="border: thin solid #000000">
                                            <asp:GridView ID="GridView1" runat="server" GridLines="Both" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                AutoGenerateColumns="true" BorderColor="Black" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                Width="100%" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="There Are No Records" Font-Size="X-Small" Font-Names="tahoma">
                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                <PagerStyle CssClass="grid pagerbar" />
                                                <HeaderStyle CssClass="grid-header" />
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                                    LastPageText="Last" />
                                            </asp:GridView>
                                            <asp:GridView ID="GridView2" runat="server" GridLines="Both" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                AutoGenerateColumns="true" BorderColor="black" HeaderStyle-CssClass="grid-header"
                                                PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                Width="100%" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound" EmptyDataText="There Are No Records" >
                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                <PagerStyle CssClass="grid pagerbar" />
                                                <HeaderStyle CssClass="grid-header" />
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                                    LastPageText="Last" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr id="Trnet" runat="server">
                                        <td>
                                            <asp:Label ID="Label12" runat="server" CssClass="esfmhead" Text="Total Net DCA budget consumption up to date:- "></asp:Label>
                                            <asp:Label ID="Label14" runat="server" CssClass="esfmhead" Text="Total :- "></asp:Label>
                                            <asp:Label ID="Label13" runat="server" Text="" CssClass="eslblalert"></asp:Label>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                     
                        <tr class="style2" width="400px" height="30px" id="btnprint" runat="server">
                            <td align="center">
                                <input class="buttonSubmit" onclick="print();" type="button" value="Print" title="Print Report" />
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trexcel" runat="server">
                            <td style="height: 20px" align="right">
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
