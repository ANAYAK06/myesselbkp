<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyUnsecuredLoan.aspx.cs" Inherits="VerifyUnsecuredLoan" %>

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
                                                <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text=" Verify Unsecured Loan"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" GridLines="None"
                                                    HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="transaction_no,type"
                                                    FooterStyle-BackColor="DarkGray" EmptyDataText="There is no Records" OnRowEditing="GridView1_RowEditing"
                                                    OnRowDeleting="GridView1_RowDeleting">
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Edit" ButtonType="Image" ItemStyle-Width="30px" ShowEditButton="true"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="loan_no" ItemStyle-Width="100px" HeaderText="Loan No" />
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
                                                Verify Unsecured Loan
                                            </th>
                                        </tr>
                                        <tr id="paytype" runat="server">
                                            <td>
                                                <table class="innertab">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Width="150px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="Unsecured Loan Type"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtloantype" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Width="150px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="Unsecured Loan No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtunsecurednoumber" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="Loan Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtloandate" CssClass="estbox" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblname" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="150px"
                                                                Text="Name"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtname" runat="server" ToolTip="Name" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                                Text="Rate of Intrest"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtintrestrate" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                                Text="Amount"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtamount" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trdedhead" runat="server">
                                                        <td>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label10" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Deduction If Any"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="lbldedcheck" runat="server" Enabled="false" Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr id="trdedbody" runat="server">
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdeddca" runat="server" Enabled="false" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdedsdca" runat="server" Enabled="false" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdedamt" runat="server" Enabled="false" Width="100px"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtbank" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="ModeofPay" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Mode Of Pay:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpaymenttype" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                            <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                            <asp:TextBox ID="txtdate" Enabled="false" CssClass="estbox" runat="server" Width="80px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="No:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcheque" CssClass="estbox" Enabled="false" runat="server" ToolTip="No"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" Enabled="false" Width="200px"
                                                                TextMode="MultiLine"></asp:TextBox>
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
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Button CssClass="esbtn" Style="font-size: small; height: 26px;" ID="btnupdate"
                                                    runat="server" Text="Verify" OnClick="btnupdate_Click" />
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
