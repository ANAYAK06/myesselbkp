<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankCreditSummary.aspx.cs"
    Inherits="BankCreditSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/listgrid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /*To Set Vertical and Horzantal Scrollbars*/
        .scrollcss
        {
            overflow: auto;
            background-color: #21b81e;
            width: 350px;
            height: 150px;
        }
        /*To Set Only Vertical Scrollbar*/
        .verticalscroll
        {
            overflow-x: hidden;
            overflow-y: auto;
          
            width: 700px;
            height: 500px;
        }
        /*To Set only Horizontal Scrollbar*/
        .horizontalscroll
        {
            overflow-x: auto;
            overflow-y: hidden;
            background-color: #21b81e;
            width: 350px;
            height: 150px;
        }
    </style>
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
                            OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
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
                                        <asp:BoundField DataField="Invoice_Date" ItemStyle-Width="100px" HeaderText="Invoice Date"  />
                                        <asp:BoundField DataField="Date" ItemStyle-Width="100px" HeaderText="Recieved Date" />
                                        <asp:BoundField DataField="po_no" HeaderText="PO No" />
                                        <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                        <asp:BoundField DataField="CC_Code" HeaderText="CC Code" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                              
                            </table>
                            <div id="divgrd" runat="server"></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" CssClass="esddown" Text="Total Net DCA Debit In Cash Flow Up-To-Date:- "></asp:Label>
                        <asp:Label ID="lbltotal" runat="server" Text="" CssClass="eslblalert"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
