<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyFDInterest.aspx.cs" Inherits="VerifyFDInterest" %>

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
                                                <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text=" Verify FD Intrest"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" GridLines="None"
                                                    HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="Tran_no,FDR" FooterStyle-BackColor="DarkGray"
                                                    EmptyDataText="There is no Records" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting">                                                    
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Edit" ButtonType="Image" ItemStyle-Width="30px" ShowEditButton="true"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="FDR" ItemStyle-Width="100px" HeaderText="FDR No" />
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
                                            <th align="center" colspan="4">
                                                Verify FD Interst
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="FDR No"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtfdrnoInt" Enabled="false" CssClass="estbox" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label23" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="Intrest Date"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtintdate" Enabled="false" CssClass="estbox" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label18" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                    Text="Intrest Amount"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox runat="server" ID="txtintrestamt" Enabled="false" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="center" colspan="4">
                                                Deduction
                                            </th>
                                        </tr>
                                        <tr>
                                            <td width="750px" colspan="4">
                                                <table class="innertab" width="100%" runat="server" id="Table4">
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" CssClass="eslbl" Width="50px" Font-Size="XX-Small"
                                                                Text="CC Code"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label20" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="DCA Code"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label21" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="SDCA Code"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label22" CssClass="eslbl" runat="server" Font-Size="XX-Small" Text="Amount"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="750px" colspan="4">
                                                <table class="innertab" width="100%" runat="server" id="Table5">
                                                    <tr align="center" style="background-color: white">
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtcccodeded" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtdcaded" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtsdcacodeded" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdedamount" CssClass="estbox" runat="server" Enabled="false" Width="100px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="center" colspan="4">
                                                Payment Details
                                            </th>
                                        </tr>
                                        <tr id="Tr1" runat="server">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                                <asp:TextBox ID="txtbankname" runat="server" ToolTip="Amount" Enabled="false" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr4" runat="server">
                                            <td>
                                                <asp:Label ID="Label25" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Mode Of Pay:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtmodeofpay" runat="server" ToolTip="Amount" Enabled="false" ForeColor="Black"
                                                    Width="200px"></asp:TextBox>
                                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtbankdate" Enabled="false" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label27" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" Enabled="false" ToolTip="No"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label28" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" Enabled="false" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label29" runat="server" CssClass="eslbl" Font-Size="XX-Small" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" Enabled="false" ToolTip="Amount"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="padding: .3em; border: 1px #000000 solid;">
                                            <td colspan="4" align="center" style="padding: .3em; border: 1px #000000 solid; border-right-color: Black;">
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
