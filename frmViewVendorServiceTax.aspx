<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmViewVendorServiceTax.aspx.cs"
    Inherits="Admin_frmViewVendorServiceTax" EnableEventValidation="false" Title="View Vendor ServiceTax - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function searchvalidate() {

            var objs = new Array("<%=ddltype.ClientID %>", "<%=ddlyear.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <table class="estbl" width="700px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" colspan="5" align="center">
                                        <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View Vendor ServiceTax/Excise Duty Payment"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" valign="middle">
                                        <asp:RadioButton ID="rbtnpaiddate" runat="server" Text="Paid Date" GroupName="dt"
                                            Font-Bold="true" Font-Names="Courier New" Font-Size="Small" Checked="true" ForeColor="Black" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbtninvmkdate" runat="server" Text="Invoice Entry Date" GroupName="dt"
                                            Font-Bold="true" Font-Names="Courier New" Font-Size="Small" ForeColor="Black" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center">
                                        <asp:DropDownList ID="ddltype" CssClass="esddown" Width="200px" runat="server" ToolTip="Type">
                                            <asp:ListItem Value="Select Month">Select Type</asp:ListItem>
                                            <asp:ListItem Value="Excise/Service Tax against Service">Excise/Service Tax against Service</asp:ListItem>
                                            <asp:ListItem Value="Excise/Service Tax against Trading">Excise/Service Tax against Trading</asp:ListItem>
                                            <asp:ListItem Value="Excise/Service Tax against Manufacturing">Excise/Service Tax against Manufacturing</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Month"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlmonth" CssClass="esddown" Width="105px" runat="server" ToolTip="PO">
                                            <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                            <asp:ListItem Value="1">Jan</asp:ListItem>
                                            <asp:ListItem Value="2">Feb</asp:ListItem>
                                            <asp:ListItem Value="3">Mar</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">Sep</asp:ListItem>
                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="starSpan">*</span>
                                    </td>
                                    <td valign="center" class="style10">
                                        <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                    </td>
                                    <td align="left" class="style11">
                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="130px">
                                        <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                            Text="View Report" OnClick="btnAssign_Click" OnClientClick="javascript:return searchvalidate()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView1" runat="server" Width="700px" AutoGenerateColumns="False"
                                Font-Size="Small" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" CssClass="mGrid">
                                <FooterStyle Font-Bold="true" ForeColor="White" BackColor="#424242" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" Visible="false" />
                                    <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"
                                        FooterText="Total:" />
                                    <asp:BoundField DataField="po_no" HeaderText="PO No" />
                                    <asp:BoundField DataField="vendor_id" HeaderText="Vendor Id" />
                                    <asp:BoundField DataField="invoiceno" HeaderText="Invoice No" />
                                    <asp:BoundField DataField="Servicetax" HeaderText="Servicetax" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Edcess" HeaderText="Edcess" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Hedcess" HeaderText="Hedcess" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Exciseduty" HeaderText="Excise Duty" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:#,##,##,###.00}" />
                                </Columns>
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Left" />
                            </asp:GridView>
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                TabIndex="1" Width="100%" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound"
                                CellPadding="4" ForeColor="#333333" GridLines="Both">
                                <Columns>
                                    <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" Visible="false" />
                                    <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"
                                        FooterText="Total:" />
                                    <asp:BoundField DataField="po_no" HeaderText="PO No" />
                                    <asp:BoundField DataField="vendor_id" HeaderText="Vendor Id" />
                                    <asp:BoundField DataField="invoiceno" HeaderText="Invoice No" />
                                    <asp:BoundField DataField="Servicetax" HeaderText="Servicetax" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Edcess" HeaderText="Edcess" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Hedcess" HeaderText="Hedcess" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Exciseduty" HeaderText="Excise Duty" DataFormatString="{0:#,##,##,###.00}" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:#,##,##,###.00}" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
