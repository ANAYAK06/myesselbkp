<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyShareCapital.aspx.cs" Inherits="VerifyShareCapital" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<%@ Register Src="~/LedgerCreationUserControl.ascx" TagName="LedgerCreation" TagPrefix="Ledger"  %>

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
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table style="width: 750px">
                            <tr align="center">
                                <td>
                                    <table style="width: 750px;">
                                        <tr align="center">
                                            <th>
                                                <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text=" Verify Share Capital"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" GridLines="None"
                                                    HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="transaction_no"
                                                    FooterStyle-BackColor="DarkGray" EmptyDataText="There is no Records" OnRowEditing="GridView1_RowEditing"
                                                    OnRowDeleting="GridView1_RowDeleting">
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Edit" ButtonType="Image" ItemStyle-Width="30px" ShowEditButton="true"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="name" ItemStyle-Width="100px" HeaderText="Name" />
                                                        <asp:BoundField DataField="date" ItemStyle-Width="100px" HeaderText="Date" />
                                                        <asp:BoundField DataField="Amount" ItemStyle-Width="100px" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="bank_name" ItemStyle-Width="100px" HeaderText="Bank Name"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="description" ItemStyle-Width="200px" HeaderText="Description"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" DeleteText="Delete"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="745px" id="tblinvoice" runat="server">
                                        <tr>
                                            <th align="center">
                                                Verify Share Capital
                                            </th>
                                        </tr>
                                        <tr id="paytype" runat="server">
                                            <td>
                                                <table class="innertab">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Width="150px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="Share Holder Type"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txttype" Enabled="false" CssClass="estbox" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtsharedate" Enabled="false" CssClass="estbox" runat="server" ToolTip="Share Capital Date"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="vename" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="150px"
                                                                Text="Share Holder Name"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtshareholdername" runat="server" Enabled="false" ToolTip="Share Holder Name"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                                Text="Amount"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtamount" runat="server" ToolTip="Amount" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="Trledger" runat="server">
                                            <td>
                                                <ledger:ledgercreation id="Ledger" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="trpaymentdetails" runat="server">
                                            <td>
                                                <table align="center" class="estbl" width="100%" runat="server" id="paymentdetails">
                                                    <tr>
                                                        <th align="center" colspan="4">
                                                            Payment Details
                                                        </th>
                                                    </tr>
                                                    <tr id="bank" runat="server">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                                            <asp:TextBox ID="txtbankname" runat="server" ToolTip="Amount" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="ModeofPay" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label66" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Mode Of Pay:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtmodeofpay" runat="server" ToolTip="Amount" Enabled="false" ForeColor="Black"
                                                                Width="200px"></asp:TextBox>
                                                            <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                            <asp:TextBox ID="txtbankdate" Enabled="false" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                                Width="80px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="No:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" Enabled="false" ToolTip="No"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" Enabled="false" ToolTip="Description"
                                                                Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" Enabled="false" ToolTip="Amount"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td colspan="4" align="center" style="padding: .3em; border: 1px #000000 solid; border-right-color: Black;">
                                                <asp:Button CssClass="esbtn" Style="font-size: small; height: 26px;" ID="btnupdate"
                                                    runat="server" Text="Verify" OnClick="btnupdate_Click" />
                                                <%-- OnClientClick="javascript:return validate()"--%>
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
