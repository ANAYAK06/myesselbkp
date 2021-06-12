﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bankpaymentprint.aspx.cs"
    Inherits="Bankpaymentprint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ESSEL PROJECTS PVT. LTD.</title>

    <script language="javascript">
        function Print() {
            // The following line is required to show the print dialog after
            // mail is rendered. Affects IE browser.
            document.body.offsetHeight;
            window.print();
        }
    </script>

    <style>
        DIV.crystalstyle DIV
        {
            z-index: 25;
            position: absolute;
        }
        DIV.crystalstyle A
        {
            text-decoration: none;
        }
        DIV.crystalstyle A IMG
        {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
        }
        .buttonSubmit
        {
            background-color: #4C99CC;
            border-bottom: medium none;
            border-left: medium none;
            border-right: medium none;
            border-top: medium none;
            color: white;
            cursor: pointer;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: bold;
            height: 18px;
            text-decoration: none;
        }
        .style1
        {
            font-weight: normal;
            font-size: 23pt;
            color: #000000;
            font-family: Arial Black;
        }
        .style2
        {
            font-weight: bold;
            font-size: 10pt;
            color: #000000;
            font-family: Arial;
        }
        .style3
        {
            font-weight: bold;
            font-size: 15pt;
            color: #000000;
            font-family: Arial;
        }
        .style4
        {
            width: 67px;
        }
        .style6
        {
            font-weight: normal;
            font-size: 11pt;
            color: #000000;
            font-family: Arial;
        }
        .style7
        {
            height: 36px;
        }
        .tbl
        {
            border-left: 1px solid #000000;
            border-right: 0 solid #000000;
            border-top: 0 solid #000000;
            border-bottom: 1px solid #000000;
            border-spacing: 0;
            border-collapse: collapse;
            margin-right: 0px;
        }
        .tbl td
        {
            margin: 0;
            padding: 4px;
            border-width: 1px 1px 0 0;
            border-color: #000000;
            border-style: outset;
            table-layout: fixed;
        }
        .style9
        {
            height: 36px;
            width: 65px;
        }
        .style10
        {
            width: 65px;
        }
        .style11
        {
            width: 127px;
        }
        .style12
        {
            height: 36px;
            width: 127px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 2px solid #000000; width: 730px;">
        <table cellspacing="0" cellpadding="0" width="703px">
            <tr>
                <td class="style1" align="center">
                    ESSEL&nbsp;PROJECTS&nbsp;PVT.&nbsp;LTD.
                </td>
                <td rowspan="3" valign="bottom">
                    <table class="style2" cellspacing="0" width="250">
                        <tr>
                            <td align="right">
                                Transaction No:
                            </td>
                            <td align="center" style="border: 1px solid #000000; border-bottom: #000000 0px solid;">
                                <asp:Label ID="lblvoucherid" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Payment Date:
                            </td>
                            <td align="center" style="border: 1px solid #000000; border-bottom: #000000 0px solid;">
                                <asp:Label ID="lbldate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                CC Code :
                            </td>
                            <td align="center" style="border: 1px solid #000000; border-bottom: #000000 1px solid;">
                                <asp:Label ID="lblcccode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <%-- Vendor Code :--%>
                            </td>
                            <td align="center">
                                <%--<asp:Label ID="lblvendorid" runat="server"></asp:Label>
                                &nbsp;--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style3" align="center" height="50px">
                    BANK&nbsp;PAYMENT&nbsp;ADVICE
                </td>
            </tr>
            <tr>
                <td>
                    <table align="right" style="width: 407px">
                        <tr>
                            <td class="style2" align="right">
                                DCA Code:
                            </td>
                            <td style="border: 1px solid #000000; width: 100px;" class="style13">
                                <asp:Label ID="lblDca" CssClass="style7" runat="server"></asp:Label>
                            </td>
                            <td class="style2" align="right">
                                SDCA Code:
                            </td>
                            <td style="border: 1px solid #000000; width: 100px;">
                                <asp:Label ID="lblsubdca" CssClass="style7" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="style2" style="position: relative; left: 24px; width: 662px;">
                        <tr>
                            <td class="style2" align="right">
                                Mode Of Pay:
                            </td>
                            <td style="border: 1px solid #000000; width: 100px;" class="style13">
                                <asp:Label ID="lblmodeofapy" CssClass="style7" runat="server"></asp:Label>
                            </td>
                            <td class="style2" align="right">
                                Bank Name:
                            </td>
                            <td style="border: 1px solid #000000; width: 100px;" class="style13">
                                <asp:Label ID="lblbname" CssClass="style7" runat="server"></asp:Label>
                            </td>
                            <td class="style2" align="right">
                                Cheque No :
                            </td>
                            <td style="border: 1px solid #000000; width: 100px;" class="style13">
                                <asp:Label ID="lblcno" CssClass="style7" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="style2" style="position: relative; left: 24px; width: 662px;">
                        <tr>
                            <td class="style4">
                                Pay To,
                            </td>
                            <td width="600px" style="border-bottom: #000000 1px solid;">
                                <asp:Label ID="lblpayto" CssClass="style2" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table class="tbl" style="position: relative; left: 24px; width: 662px; height: 111px;">
                        <tr>
                            <td class="style2">
                                <span style="font-style: italic">Being Paid Against</span>
                            </td>
                            <td align="center" class="style11">
                                &nbsp;
                            </td>
                            <%-- <td class="style10">
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="style7">
                                <asp:Label ID="lblperticular" Style="position: relative; left: 40px;" CssClass="style6"
                                    runat="server" Width="300px"></asp:Label>
                            </td>
                            <td class="style12" align="center">
                                <asp:Label ID="lblamount" CssClass="style2" runat="server"></asp:Label>
                            </td>
                            <%--<td class="style9">
                                &nbsp;
                            </td>--%>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="style2">TOTAL</span><span class="style5">-‎</span>
                            </td>
                            <td align="center" class="style12">
                                <asp:Label ID="lblTotal" CssClass="style2" runat="server"></asp:Label>
                            </td>
                            <%--<td class="style10">
                                &nbsp;
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <%--<td>
                </td>--%>
            </tr>
            
            <tr class="style2" height="30px">
                <td align="center">
                    <table style="width: 346px">
                    </table>
                </td>
                <td align="center">
                    <table>
                        <tr>
                            <td>
                                Submitted By
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                <asp:Label ID="lblapproveby" CssClass="style2" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="style2" height="30px">
                <td align="center" colspan="2">
                    <input class="buttonSubmit" onclick="Print();" type="button" value="Print" title="Print Report">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
