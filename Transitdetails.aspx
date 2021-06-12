<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Transitdetails.aspx.cs" Inherits="Transitdetails" %>

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
                        <asp:GridView ID="indentgrid" BorderColor="Black" GridLines="Both" runat="server"
                            AutoGenerateColumns="False" Font-Size="Small" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                            AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                            PagerStyle-CssClass="grid pagerbar" FooterStyle-BackColor="LightGray" FooterStyle-Font-Bold="true"
                            ShowFooter="true">
                            <Columns>
                                <asp:BoundField DataField="item_code" ItemStyle-Width="100px" HeaderText="Item Code"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="item_name" ItemStyle-Width="100px" HeaderText="Item Name"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="quantity" HeaderText="Quantity" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Transfer_Date" HeaderText="Dispatch date" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="cc_code" HeaderText="Dispatched CC " ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Recieved_cc" HeaderText="Receving CC" ItemStyle-Wrap="false" />
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
