<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmviewDCA.aspx.cs"
    EnableEventValidation="false" Inherits="Admin_frmviewDCA" Title="View DCA-Essel Projects.Pvt.ltd. " %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="width: 750px">
                    <tr valign="top">
                        <th>
                            View DCA
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                                DataKeyNames="dca_code" OnRowDataBound="GridView2_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="mapdca_code" ItemStyle-Width="100px" HeaderText="DCA Code" />
                                    <asp:BoundField DataField="dca_name" ItemStyle-Width="250px" HeaderText="SubDCA Name" />
                                    <asp:TemplateField HeaderText="SubDca's/IT Code">
                                        <ItemTemplate>
                                            <asp:Label ID="iblitcode" runat="server" Text='<%# Eval("it_code") %>'></asp:Label>
                                            <asp:GridView ID="GridView1" runat="server" Width="400px" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="mapsubdca_code" ItemStyle-Width="100px" HeaderText="SubDCA" />
                                                    <asp:BoundField DataField="subdca_name" HeaderText="SubDCA Name" />
                                                    <asp:BoundField DataField="it_code" ItemStyle-Width="75px" HeaderText="IT Code" />
                                                </Columns>
                                                <HeaderStyle BackColor="#336699" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle ForeColor="#000066" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnPrintCurrent" runat="server" Text="Print" OnClick="PrintAllPages" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                OnClick="btnExcel_Click" />
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
