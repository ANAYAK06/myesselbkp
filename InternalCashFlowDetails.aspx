<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InternalCashFlowDetails.aspx.cs"
    Inherits="InternalCashFlowDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="730px">
            <tr id="trcccode" runat="server">
                <td class="item item-selection" valign="middle" id="tdcccode" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="CC Code"></asp:Label>
                    <asp:DropDownList ID="ddlpopcccode" AutoPostBack="true" ToolTip="CC Code" CssClass="filter_item" 
                        runat="server" onselectedindexchanged="ddlpopcccode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="grid-content" align="center">
                    <table id="_terp_list_grid" class="" width="100%" align="center" style="background: none;"
                        width="500px">
                        <asp:GridView ID="GridView1" BorderColor="White" runat="server" AutoGenerateColumns="False"
                            CssClass="mGrid" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="date" ItemStyle-Width="100px" HeaderText="Date" />
                                <asp:BoundField DataField="cc_code" ItemStyle-Width="75px" HeaderText="CC Code" />
                                <asp:BoundField DataField="description" HeaderText="Description" />
                                <asp:BoundField DataField="amount" ItemStyle-Width="100px" HeaderText="Amount" />
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
