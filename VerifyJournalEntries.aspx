<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="VerifyJournalEntries.aspx.cs" Inherits="VerifyJournalEntries" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr style="border: 1px solid #000">
                                <th valign="top" align="center" style="background: #D3D3D3">
                                    <asp:Label ID="lblheading" CssClass="esfmhead" runat="server" Text=""></asp:Label>
                                </th>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:GridView ID="gvjournals" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="False" BorderColor="white" CssClass="grid-content" DataKeyNames="Transaction_id"
                                        GridLines="None" HeaderStyle-CssClass="grid-header"
                                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        ShowFooter="false" Width="680px" OnSelectedIndexChanged="gvjournals_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Transaction_id" ItemStyle-HorizontalAlign="Center" HeaderText="JV No" />                                           
                                            <asp:BoundField DataField="Debit" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}"
                                                HeaderText="Debit Amount" ItemStyle-ForeColor="DarkBlue" />
                                             <asp:BoundField DataField="Credit" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}" HeaderText="Credit Amount"
                                                ItemStyle-ForeColor="DarkBlue" />
                                            <asp:BoundField DataField="Date" ItemStyle-HorizontalAlign="Center" HeaderText="Date" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table><tr><td  style="height:25px"></td></tr></table>
                        <table cellpadding="0" id="tblverifyjornals" runat="server" cellspacing="0" width="680px">
                            <tr align="center">
                                <td align="center">
                                    <table class="estbl" width="550px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" width="590px" CssClass="esfmhead" runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                     </table>
                                     <table>
                                        <tr>
                                            <td>
                                                <table id="trtype"  width="600px">
                                                    <asp:GridView ID="gvjournalapproval" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                        AutoGenerateColumns="False" BorderColor="white" CssClass="grid-content"
                                                        GridLines="None" HeaderStyle-CssClass="grid-header"
                                                        PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                        ShowFooter="True" Width="600px" OnRowDataBound="gvjournalapproval_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                                ItemStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Transaction_id" ItemStyle-HorizontalAlign="Center" HeaderText="JV No" />
                                                            <asp:BoundField DataField="Ledgername" ItemStyle-HorizontalAlign="Center" HeaderText="Ledger Name" />                                                           
                                                            <asp:BoundField DataField="Debit" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}"
                                                                HeaderText="Debit Amount" ItemStyle-ForeColor="DarkBlue" />
                                                             <asp:BoundField DataField="Credit" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}" HeaderText="Credit Amount"
                                                                ItemStyle-ForeColor="DarkBlue" />
                                                            <asp:BoundField DataField="Date" ItemStyle-HorizontalAlign="Center" HeaderText="Date" />
                                                        </Columns>
                                                        <FooterStyle BackColor="gray" Font-Bold="True" ForeColor="white" />
                                                    </asp:GridView>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tblbtnupdate" runat="server" width="700px">
                                        <tr align="center">
                                            <td align="center">
                                                 <asp:Button ID="btnapprove" runat="server" CssClass="esbtn"
                                                    Style="font-size: small" Text="" OnClick="btnapprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnreject" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Reject" OnClick="btnreject_Click" />
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

