<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selfinvoiceformatforgeneralpayment.aspx.cs"
    Inherits="selfinvoiceformatforgeneralpayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

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
        <table style="margin: 1em; border-collapse: collapse;" width="500px">
            <tr>
                <th colspan="4" style="padding: padding: .3em; border: 1px #000000 solid; font-size: small;
                    background: #808080;" align="center">
                    Self Invoice For General Payment
                </th>
            </tr>
            <tr style="padding: .3em; border: 1px #000000 solid;">
                <td colspan="4" style="padding: .3em; border: 1px #000000 solid; border-right-color: black;"
                    align="center">
                <asp:Label ID="lblpaymenttype" runat="server" Style="color: Black; font-family: Arial; font-weight: bold;
                        font-size: small;" ></asp:Label>
                </td>
            </tr>
            <tr style="padding: .3em; border: 1px #000000 solid;">
                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                    align="center">
                    <asp:Label ID="Label1" runat="server" Style="color: Black; font-family: Arial; font-weight: normal;
                        font-size: small;" Text="Invoice No:-"></asp:Label>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;">
                    <asp:TextBox ID="txtadviceno" runat="server" Style="color: #000000; font-family: Tahoma;
                        text-decoration: none"></asp:TextBox>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                    align="center">
                    <asp:Label ID="Label3" runat="server" Style="color: Black; font-family: Arial; font-weight: normal;
                        font-size: small;" Text="CC Code:-"></asp:Label>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;">
                    <asp:TextBox ID="txtcccode" runat="server" Style="color: #000000; font-family: Tahoma;
                        text-decoration: none"></asp:TextBox>
                </td>
            </tr>
            <tr style="padding: .3em; border: 1px #000000 solid;">
                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                    align="center">
                    <asp:Label ID="Label5" runat="server" Style="color: Black; font-family: Arial; font-weight: normal;
                        font-size: small;" Text="Dca:-"></asp:Label>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;">
                    <asp:TextBox ID="txtdca" runat="server" Style="color: #000000; font-family: Tahoma;
                        text-decoration: none"></asp:TextBox>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                    align="center">
                    <asp:Label ID="Label7" runat="server" Style="color: Black; font-family: Arial; font-weight: normal;
                        font-size: small;" Text="SubDca:-"></asp:Label>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;">
                    <asp:TextBox ID="txtsubdca" runat="server" Style="color: #000000; font-family: Tahoma;
                        text-decoration: none"></asp:TextBox>
                </td>
            </tr>
            <tr style="padding: .3em; border: 1px #000000 solid;">
                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                    align="center">
                    <asp:Label ID="Label2" runat="server" Style="color: Black; font-family: Arial; font-weight: normal;
                        font-size: small;" Text="Party Name:-"></asp:Label>
                </td>
                <td colspan="1" style="padding: .3em; border: 1px #000000 solid; border-left-color: White;"
                    align="left">
                    <asp:TextBox ID="txtpartyname" runat="server" Width="80%" Style="color: #000000;
                        font-family: Tahoma; text-decoration: none"></asp:TextBox>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                    align="center">
                    <asp:Label ID="Label24" runat="server" Style="color: Black; font-family: Arial; font-weight: normal;
                        font-size: small;" Text="Invoice Date:-"></asp:Label>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;">
                    <asp:TextBox ID="txtpaymentdate" runat="server" Style="color: #000000; font-family: Tahoma;
                        text-decoration: none"></asp:TextBox>
                </td>
            </tr>
            <tr style="padding: .3em; border: 1px #000000 solid;">
                <td style="padding: .3em; border: 1px #000000 solid; border-right-color: White;"
                    align="center">
                    <asp:Label ID="Label4" runat="server" Style="color: Black; font-family: Arial; font-weight: normal;
                        font-size: small;" Text="Description:-"></asp:Label>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;">
                    <asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine" Style="color: #000000;
                        font-family: Tahoma; text-decoration: none" Width="100%"></asp:TextBox>
                </td>
                <td align="right" style="padding: .3em; border: 1px #000000 solid; border-right-color: White;">
                    <asp:Label ID="Label28" runat="server" Style="color: Black; font-family: Arial; font-weight: normal;
                        font-size: small;" Text="Invoice Amount:-"></asp:Label>
                </td>
                <td style="padding: .3em; border: 1px #000000 solid;">
                    <asp:TextBox ID="txtpaidamount" runat="server" Style="color: #000000; font-family: Tahoma;
                        text-decoration: none;" Width="70%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="500px">
            <tr align="center">
                <td>
                    <input class="buttonSubmit" onclick="Print();" type="button" value="Print" title="Print Report">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
