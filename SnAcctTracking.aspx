<%@ Page Title="Tracking Reports" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="SnAcctTracking.aspx.cs" Inherits="SnAcctTracking" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function OpenNewPage(Type, Role) {
            window.open("ViewDetailedStatusReport.aspx?type=" + Type + "&Name=" + Role, "NewWindow", "toolbar=no,menubar=no,top=100,left=100,titlebar=no,scrollbars=yes,location=0, directories=0,'_blank'");
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="700px">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="600px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text="Tracking Report"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                    <table id="tbladdgroups" style="border: 1px solid #000" runat="server" width="600px">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdstatus" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="600px" GridLines="None" OnRowDataBound="grdstatus_RowDataBound" ShowFooter="True">
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="typeoftransaction" HeaderText="Transaction Type" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SrAccountant" HeaderText="SrAccountant" ItemStyle-HorizontalAlign="Center"
                                                            Visible="False">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PM" HeaderText="PM" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CSK" HeaderText="CSK" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PUM" HeaderText="PUM" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CMC" HeaderText="CMC" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="HoAdmin" HeaderText="Ho-Admin" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SAdmin" HeaderText="Super Admin" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CMD" HeaderText="CMD" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
