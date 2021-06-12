<%@ Page Title="Customer Hold Verification" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="CustomerHoldVerification.aspx.cs" Inherits="CustomerHoldVerification" %>
<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            GridView2 = document.getElementById("<%=Gvverification.ClientID %>");
            if (GridView2 != null) {
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    if (GridView2.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                                <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text=" Verify Client Hold Payments"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" GridLines="None"
                                                    HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="Transaction_No"
                                                    FooterStyle-BackColor="DarkGray" EmptyDataText="There is no Records" OnRowDeleting="GridView1_RowDeleting"
                                                    OnRowEditing="GridView1_RowEditing">
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Edit" ButtonType="Image" ItemStyle-Width="30px" ShowEditButton="true"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="Transaction_No" ItemStyle-Width="100px" HeaderText="Transaction No" />
                                                        <asp:BoundField DataField="date" ItemStyle-Width="100px" HeaderText="Date" />
                                                        <asp:BoundField DataField="Amount" ItemStyle-Width="100px" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="bank_name" ItemStyle-Width="100px" HeaderText="Bank Name"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="description" ItemStyle-Width="200px" HeaderText="Description"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:CommandField HeaderText="Reject" ShowDeleteButton="true" DeleteText="Reject"
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
                                                Verify Client Hold Invoices
                                            </th>
                                        </tr>
                                        <tr id="paytype" runat="server">
                                            <td>
                                                <table>
                                                    <asp:GridView ID="Gvverification" Width="100%" runat="server" AutoGenerateColumns="false"
                                                        CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                        DataKeyNames="InvoiceNo" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                        RowStyle-CssClass=" grid-row char grid-row-odd" ShowFooter="true" EmptyDataText="There is no records"
                                                        GridLines="None" OnRowDataBound="Gvverification_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="PO_NO" HeaderText="PO NO" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Invoice_Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Hold_Balance" HeaderText="Hold Amount" DataFormatString="{0:n}"
                                                                ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Credit" HeaderText="Hold Paid Amount" DataFormatString="{0:n}"
                                                                ItemStyle-HorizontalAlign="Center" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </table>
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
                                                    runat="server" Text="Verify" OnClick="btnupdate_Click" OnClientClick="javascript:return validate();" />
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

