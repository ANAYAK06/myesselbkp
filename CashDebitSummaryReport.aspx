<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashDebitSummaryReport.aspx.cs"
    Inherits="CashDebitSummaryReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table width="730px">
                <tr>
                    <td align="right">
                        <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                        <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                            OnClick="btnExcel_Click" />
                        <asp:ImageButton ID="btnpdf" runat="server" ImageUrl="~/images/pdf-icon-resized-sml-copy-2.gif"
                            OnClick="btnpdf_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="grid-content" align="center">
                        <div class="verticalscroll">
                            <table id="_terp_list_grid" class="" width="100%" align="center" style="background: none;
                                border-color: black;">
                                <asp:GridView ID="GridView1" BorderColor="Black" GridLines="Both" runat="server"
                                    AutoGenerateColumns="False" Font-Size="Small" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                    PagerStyle-CssClass="grid pagerbar" FooterStyle-BackColor="LightGray" FooterStyle-Font-Bold="true"
                                    ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="dca_code" ItemStyle-Width="100px" HeaderText="DCA Code" />
                                        <asp:BoundField DataField="Invoice Date" ItemStyle-Width="100px" HeaderText="Invoice Date" />
                                        <asp:BoundField DataField="date" HeaderText="Paid Date" />
                                        <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                        <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </table>
                             <div id="divgrd" runat="server"></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
