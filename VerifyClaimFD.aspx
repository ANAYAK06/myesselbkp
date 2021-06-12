<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyClaimFD.aspx.cs" Inherits="VerifyClaimFD" %>

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
                                                <asp:Label ID="Label18" CssClass="esfmhead" runat="server" Text=" Verify FDR"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" GridLines="None"
                                                    HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="transaction_no,FDR"
                                                    FooterStyle-BackColor="DarkGray" EmptyDataText="There is no Records" OnRowEditing="GridView1_RowEditing"
                                                    OnRowDeleting="GridView1_RowDeleting">
                                                    <%-- --%>
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Edit" ButtonType="Image" ItemStyle-Width="30px" ShowEditButton="true"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="FDR" ItemStyle-Width="100px" HeaderText="FDR" />
                                                        <asp:BoundField DataField="date" ItemStyle-Width="100px" HeaderText="Date" />
                                                        <asp:BoundField DataField="Amount" ItemStyle-Width="100px" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="bank_name" ItemStyle-Width="100px" HeaderText="Bank Name"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="status" ItemStyle-Width="200px" HeaderText="Type" ItemStyle-HorizontalAlign="Center" />
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
                                    <table class="estbl" id="tblclaimfd" runat="server" width="745px">
                                        <tr>
                                            <th align="center">
                                                Claim Fixed Deposits(FD)
                                            </th>
                                        </tr>
                                        <tr id="paytype" runat="server">
                                            <td>
                                                <table class="innertab">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Width="150px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="Fixed Deposit Type"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txttype" Enabled="false" CssClass="estbox" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="vename" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="150px"
                                                                Text="FDR No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtfdrno" runat="server" Enabled="false" ToolTip="FD/RD No" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtfddate" Enabled="false" CssClass="estbox" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblclosingdate" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="Closing Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtclosingdate" Enabled="false" CssClass="estbox" runat="server"
                                                                ToolTip="FD Date" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="From Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtfromdate" Enabled="false" CssClass="estbox" runat="server" ToolTip="Share Capital Date"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Width="100px" CssClass="eslbl" Font-Size="Smaller"
                                                                Text="To Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txttodate" Enabled="false" CssClass="estbox" runat="server" ToolTip="Share Capital Date"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                                Text="Rate Of Intrest"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtrateofintrest" runat="server" Enabled="false" ToolTip="Rate Of Intrest"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                                Text="Amount"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtamount" runat="server" Enabled="false" ToolTip="Amount" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trbalamt" runat="server">
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label35" runat="server" CssClass="eslbl" Font-Size="Smaller" Width="100px"
                                                                Text="Balance Amount"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbalamt" runat="server" ToolTip="Balance Amount" onkeyup="Total();"
                                                                Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trviewpaymentdetails" runat="server">
                                            <td>
                                                <table align="center" class="estbl" width="100%" runat="server" id="Table1">
                                                    <tr>
                                                        <th align="center" colspan="4">
                                                            Bank Details
                                                        </th>
                                                    </tr>
                                                    <tr id="Tr2" runat="server">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="Label10" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                                            <asp:TextBox ID="txtfrombank1" Enabled="false" CssClass="estbox" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr3" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Mode Of Pay:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpayment1" Enabled="false" CssClass="estbox" runat="server" Width="100px"></asp:TextBox>
                                                            <asp:TextBox ID="txtdate1" Enabled="false" CssClass="estbox" runat="server" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="No:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcheque1" CssClass="estbox" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdesc1" CssClass="estbox" runat="server" Enabled="false" Width="200px"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtamt1" CssClass="estbox" runat="server" Enabled="false" ToolTip="Amount"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trmaturity" runat="server">
                                            <td>
                                                <table align="center" class="estbl" width="100%" runat="server" id="Table3">
                                                    <tr id="tr" runat="server">
                                                        <td colspan="1">
                                                            <asp:Label ID="Label15" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Maturity Amount:"></asp:Label>
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:TextBox ID="txtmaturity" CssClass="estbox" Enabled="false" ToolTip="Maturity"
                                                                runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:Label ID="Label16" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Interest:"></asp:Label>
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:TextBox ID="txtintrest" CssClass="estbox" Enabled="false" runat="server" ToolTip="Intrest"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trdeductionDetails" runat="server">
                                            <td>
                                                <table align="center" class="estbl" width="100%" runat="server" id="Table2">
                                                    <tr>
                                                        <th align="center" colspan="4">
                                                            Deduction Details
                                                        </th>
                                                    </tr>
                                                    <tr id="tranyothergrid" runat="server">
                                                        <td colspan="4">
                                                            <asp:GridView ID="gvanyother" runat="server" HeaderStyle-HorizontalAlign="Center"
                                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" AutoGenerateColumns="false"
                                                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                                BorderColor="White" DataKeyNames="dca_code" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                ShowFooter="false" Width="790px">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelectother" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="DCA" HeaderText="Dca Code and Name" />
                                                                    <asp:BoundField DataField="SubDca" HeaderText="SubDca Code and Name" />
                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                </Columns>
                                                                <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                <PagerStyle CssClass="grid pagerbar" />
                                                                <HeaderStyle CssClass="grid-header" />
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <asp:Label ID="Label17" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Deduction Amount"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdedvalue" CssClass="estbox" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trpaymentdetails" runat="server">
                                            <td>
                                                <table align="center" class="estbl" width="100%" runat="server" id="paymentdetails">
                                                    <tr>
                                                        <th align="center" colspan="4">
                                                            Reciept Details
                                                        </th>
                                                    </tr>
                                                    <tr id="bank" runat="server">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblfrombank" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                                            <asp:TextBox ID="txtfrom" Enabled="false" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="ModeofPay" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Mode Of Pay:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpayment" Enabled="false" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                                Width="80px"></asp:TextBox>
                                                            <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                            <asp:TextBox ID="txtdate" Enabled="false" runat="server" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="No:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" Enabled="false" ToolTip="No"
                                                                Width="200px"></asp:TextBox><span class="starSpan">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-top: none;">
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" Enabled="false" ToolTip="Description"
                                                                Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
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
                                                    runat="server" Text="Verify" OnClick="btnupdate_Click" OnClientClick="javascript:return validate()" />
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
    <script type="text/javascript">
        function validate() {
            var GridView1 = document.getElementById("<%=gvanyother.ClientID %>");
            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                }
            }
        }


    </script>
</asp:Content>
