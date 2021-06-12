<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Closepoprint.aspx.cs" Inherits="Closepoprint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page </title>
    <script language="javascript">
        function Print() {

            document.body.offsetHeight;
            window.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1" id="tblpodata" runat="server">
            <tr>
                <th align="left" style="width: 750px; font-size: x-large;" colspan="4">
                    Close SPPO
                </th>
            </tr>
            <tr>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                    <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="PO No"></asp:Label>&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp
                    <asp:TextBox ID="txtpono" CssClass="estbox" runat="server" ToolTip="PO NO" Enabled="false"></asp:TextBox>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                    <asp:Label ID="Label11" CssClass="eslbl" runat="server" Text="PO Date"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:TextBox ID="txtpodate" CssClass="estbox" runat="server" ToolTip="PO Date" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                    <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                    &nbsp; &nbsp;
                    <asp:TextBox ID="txtcccode" CssClass="estbox" runat="server" ToolTip="CC Code" Enabled="false"></asp:TextBox>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                    <asp:Label ID="Label13" CssClass="eslbl" runat="server" Text="DCA Code"></asp:Label>
                    &nbsp; &nbsp;
                    <asp:TextBox ID="txtdcacode" CssClass="estbox" runat="server" ToolTip="DCA Code"
                        Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                    <asp:Label ID="Label14" CssClass="eslbl" runat="server" Text="PO Value"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtpovalue" CssClass="estbox" runat="server" ToolTip="PO Value"
                        Enabled="false"></asp:TextBox>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                    <asp:Label ID="Label15" CssClass="eslbl" runat="server" Text="SubDca Code"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtsdca" CssClass="estbox" runat="server" ToolTip="SubDca Code"
                        Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                    <asp:Label ID="Label16" CssClass="eslbl" runat="server" Text="Balance"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtbalance" CssClass="estbox" runat="server" ToolTip="Balance" Enabled="false"></asp:TextBox>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="2">
                    <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Closing Date"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtclsdate" CssClass="estbox" runat="server" ToolTip="Closing Date"
                        Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                    <asp:Label ID="Label17" CssClass="eslbl" runat="server" Text="PO Remarks" Style="color: Black;
                        font-family: Arial; font-weight: bold"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                    <asp:Label ID="lblremarks" CssClass="eslbl" runat="server" Text="" ToolTip="Remarks"
                        Style="color: #000000; font-family: Tahoma; text-decoration: none;" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                    <asp:Label ID="Label18" CssClass="eslbl" runat="server" Text="PO Closing Remarks"
                        Style="color: Black; font-family: Arial; font-weight: bold"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: .3em; border: 1px #000000 solid;" align="center" colspan="4">
                    <asp:Label ID="lbldesc" CssClass="eslbl" runat="server" Text="" ToolTip="Description"
                        Style="color: #000000; font-family: Tahoma; text-decoration: none;" Width="100%"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="750px">
            <tr align="center">
                <td>
                    <input class="buttonSubmit" onclick="Print();" type="button" value="Print" title="Print Report" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
