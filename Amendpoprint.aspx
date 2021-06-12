﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Amendpoprint.aspx.cs" Inherits="Amendpoprint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ESSEL PROJECTS PVT. LTD.</title>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

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
    <div>
        <table id="Table2" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
            <tr style="border: 1px solid  #000;">
                <td align="center" colspan="2">
                    <asp:Label ID="Label1" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"
                        Text="Essel Projects Pvt Ltd."></asp:Label>
                    <br />
                    <asp:Label ID="Label13" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                        Text="Corp.Office:No-5,First Floor,Maruti Heritage,Near MMI,Pachpedi Naka,Raipur,492001(CG)."></asp:Label>
                    <br />
                    <asp:Label ID="Label15" runat="server" CssClass="peslbl" Font-Bold="false" Font-Underline="false"
                        Text="Tel/Fax:0771-4268469/4075401."></asp:Label>
                    <%--<br />
                                                                                                <asp:Label ID="lblpo" runat="server" CssClass="peslbl" Font-Bold="true" Font-Underline="true"></asp:Label>--%>
                    <br />
                    <asp:Label ID="Label10" runat="server" CssClass="peslbl" Font-Bold="true" Text="INTERNAL WORK ORDER FORMAT FOR PIECE RATE WORKER WHO HAVING PAN NO"
                        Font-Underline="true"></asp:Label>
                    <br />
                    <asp:Label ID="Label12" runat="server" CssClass="peslbl" Font-Bold="true" Text="WORK ORDER"
                        Font-Underline="true"></asp:Label>
                </td>
            </tr>
            <tr style="border: 1px solid #000">
                <td width="50%" align="left" style="border: 1px solid #000">
                    <%--<asp:TextBox ID="txtaddress" runat="server" Width="100%" TextMode="MultiLine" Style="border: None;"></asp:TextBox>--%>
                    <asp:Label ID="lblvenname" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label><br />
                    <asp:Label ID="lblvenaddress" runat="server" Text="" Width="99%" CssClass="pestbox"></asp:Label><br />
                    <asp:Label ID="lblphone" runat="server" Text="" Width="100%" CssClass="pestbox"></asp:Label>
                </td>
                <td align="left" width="100%" style="border: 1px solid #000">
                    <asp:Label ID="Label4" Width="35%" CssClass="peslbl1" runat="server" Text="PO No:-"></asp:Label>
                    <asp:Label ID="lblpono" Width="25%" CssClass="peslbl1" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label5" Width="35%" CssClass="peslbl1" runat="server" Text="Amended Date:-"></asp:Label>
                    <asp:Label ID="lblpodate" Width="25%" CssClass="peslbl1" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label6" Width="35%" CssClass="peslbl1" runat="server" Text="CC Code:-"></asp:Label>
                    <asp:Label ID="lblcccode" Width="25%" CssClass="peslbl1" runat="server"></asp:Label><br />
                    <asp:Label ID="Label7" Width="35%" CssClass="peslbl1" runat="server" Text="DCA Code:-"></asp:Label>
                    <asp:Label ID="lbldcacode" Width="25%" CssClass="peslbl1" runat="server"></asp:Label><br />
                    <asp:Label ID="Label8" Width="35%" CssClass="peslbl1" runat="server" Text="Vendor Code:-"></asp:Label>
                    <asp:Label ID="lblvendorcode" Width="25%" CssClass="peslbl1" runat="server"></asp:Label><br />
                    <asp:Label ID="Label2" Width="35%" CssClass="peslbl1" runat="server" Text="Amend No:-"></asp:Label>
                    <asp:Label ID="lblpocount" Width="25%" CssClass="peslbl1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <%--<table id="Table5" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
            <tr style="border: 1px solid #000">
                
                <td>
                    <asp:Label ID="Label2" runat="server" CssClass="peslbl" Font-Bold="true" Text="PO NO : "></asp:Label>
                    <asp:Label ID="lblpono" runat="server" CssClass="peslbl" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" CssClass="peslbl" Font-Bold="true" Text="PO Date : "></asp:Label>
                     <asp:Label ID="lblpodate" runat="server" CssClass="peslbl" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" CssClass="peslbl" Font-Bold="true" Text="Old PO Value : "></asp:Label>
                    <asp:Label ID="lbloldpovalue" runat="server" CssClass="peslbl" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" CssClass="peslbl" Font-Bold="true" Text="New PO Value : "></asp:Label>
                    <asp:Label ID="lblnewpovalue" runat="server" CssClass="peslbl" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" CssClass="peslbl" Font-Bold="true" Text="Total PO Value : "></asp:Label>
                    <asp:Label ID="lbltotalpovalue" runat="server" CssClass="peslbl" Font-Bold="true"></asp:Label>
                </td>
                <td >
                    <asp:Label ID="Label14" runat="server" CssClass="peslbl" Font-Bold="true" Text="Remarks : "></asp:Label>
                     <asp:Label ID="lblremarks" runat="server" CssClass="peslbl" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            
        </table>--%>
        <table id="Table5" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
            <tr style="border: 1px solid  #000;">
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" Width="100%"
                        GridLines="Both" EmptyDataText="No Data Avaliable" RowStyle-HorizontalAlign="Center"
                        HorizontalAlign="Center" CssClass="grid-content" BorderColor="White">
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table id="Table6" width="100%" runat="server" class="pestbl" style="border: 1px solid #000">
            <tr style="border: 1px solid #000">
                <td colspan="2" align="left">
                    <br />
                    <asp:Label ID="Label9" runat="server" CssClass="peslblfooter" Text="">For Essel Projects Pvt Ltd</asp:Label><br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label11" CssClass="peslblfooter" runat="server"
                        Text=""> Authorized Signatory</asp:Label><br />
                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblpurchasemanagername" runat="server" Style="vertical-align: middle"
                        Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
