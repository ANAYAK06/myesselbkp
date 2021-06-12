<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDetailedStatusReport.aspx.cs"
    Inherits="ViewDetailedStatusReport" Title="Essel Projects Pvt Ltd." %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr align="center">
            <td>
                <asp:Label ID="lbltransctiontype" runat="server" Font-Bold="True" ForeColor="Black"
                    BackColor="#99CCFF" Width="30%"></asp:Label>
                <div align="center" style="border: 1px solid black">
                    <asp:GridView ID="grdstatus" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                        AutoGenerateColumns="False" CssClass="mGrid" ShowFooter="true" HeaderStyle-CssClass="grid-header"
                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                        Width="800px" CellPadding="4" ForeColor="#333333" GridLines="None" FooterStyle-HorizontalAlign="Right" OnRowDataBound="grdstatus_RowDataBound">
                        <AlternatingRowStyle CssClass="grid-row grid-row-even" BackColor="White"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Font-Bold="True" Width="50px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CC_Code" HeaderText="CC Code" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="TypeofTransaction" HeaderText="Transaction Type" />--%>
                            <asp:BoundField DataField="DCAName" HeaderText="DCAName" Visible="False" />
                            <asp:BoundField DataField="RefNumber" HeaderText="Reference No" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Center">                               
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" DataFormatString="{0:n}" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">                               
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <%--<FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />--%>
                        <HeaderStyle CssClass="grid-header" BackColor="#1C5E55" Font-Bold="True" ForeColor="White">
                        </HeaderStyle>
                        <PagerStyle CssClass="grid pagerbar" BackColor="#666666" ForeColor="White" HorizontalAlign="Center">
                        </PagerStyle>
                        <RowStyle CssClass=" grid-row char grid-row-odd" BackColor="#E3EAEB"></RowStyle>
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
