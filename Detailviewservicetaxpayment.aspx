<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detailviewservicetaxpayment.aspx.cs"
    Inherits="detailviewservicetaxpayment" %>

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
        <table width="800px">
            <tr>
                <td style="width: 70px">
                </td>
                <td align="center" style="width: 350px">
                    <asp:Label ID="Label1" runat="server" CssClass="esfmhead" Text="" Font-Size="Medium"
                        Font-Names="Times New Roman" Font-Underline="True"></asp:Label>
                    <asp:Label ID="Label2" runat="server" CssClass="esfmhead" Text=" DetailView Report"
                        Font-Size="Medium" Font-Names="Times New Roman" Font-Underline="True"></asp:Label>
                </td>
                <td style="width: 50px">
                </td>
                <td align="center" style="width: 90px">
                    <asp:Label ID="lbldate" runat="server" CssClass="esfmhead" Font-Size="Medium" Font-Names="Times New Roman"
                        Font-Underline="True"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="730px">
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
                            ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="cc_code" ItemStyle-Width="100px" HeaderText="CC Code"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="date" ItemStyle-Width="100px" HeaderText="Date"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="invoiceno" HeaderText="Invoice No" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="name" HeaderText="" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Tax" HeaderText="" ItemStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
