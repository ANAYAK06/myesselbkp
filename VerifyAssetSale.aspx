<%@ Page Title="Verify Asset Sale" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="VerifyAssetSale.aspx.cs" Inherits="VerifyAssetSale" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
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
                                    <asp:GridView ID="gvassetsales" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="False" BorderColor="white" CssClass="grid-content" DataKeyNames="Id"
                                        GridLines="None" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                        RowStyle-CssClass=" grid-row char grid-row-odd" ShowFooter="false" Width="740px"
                                        OnSelectedIndexChanged="gvassetsales_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="Verify" ShowSelectButton="true"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Req_no" ItemStyle-HorizontalAlign="Center" HeaderText="Transaction No" />
                                            <asp:BoundField DataField="Date" ItemStyle-HorizontalAlign="Center" HeaderText="Date" />
                                            <asp:BoundField DataField="Item_code" ItemStyle-HorizontalAlign="Center" HeaderText="Item Code" />
                                            <asp:BoundField DataField="selling_amt" ItemStyle-HorizontalAlign="Center" HeaderText="Selling Amount" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="height: 25px">
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" id="tblverifyasset" runat="server" cellspacing="0" border="2"
                            width="670px">
                            <tr align="center">
                                <td align="center">
                                    <table class="estbl">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" Width="760px" CssClass="esfmhead" runat="server" Text="Verify Asset Sales"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                    <table border="2">
                                        <tr>
                                            <td valign="top">
                                                <div class="box-a list-a">
                                                    <div class="inner">
                                                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="grid-content">
                                                                        <table id="_terp_list_grid" class="grid" width="750px" align="center" style="background: none;">
                                                                            <asp:GridView ID="GridView1" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                DataKeyNames="id" ShowFooter="false">
                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="id" Visible="false" />
                                                                                    <asp:BoundField DataField="Item_code" HeaderText="Item Code" ItemStyle-Width="75px" />
                                                                                    <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="150px" />
                                                                                    <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-Width="175px" />
                                                                                    <asp:BoundField DataField="dca_code" HeaderText="DCA Code" ItemStyle-Width="100px" />
                                                                                    <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" ItemStyle-Width="100px" />
                                                                                    <asp:BoundField DataField="Basicprice" HeaderText="Basic Price" ItemStyle-Width="100px" />
                                                                                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="50px" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <asp:HiddenField ID="hfbasic" runat="server" />
                                                                <tr id="tbldesc" style="border-collapse: separate; border-spacing: 0 15px; margin-bottom: -15px;"
                                                                    runat="server">
                                                                    <td colspan="8">
                                                                        <table style="border-collapse: separate; border-spacing: 0 15px; margin-top: -15px;">
                                                                            <tr align="left">
                                                                                <td class="item item-selection" valign="middle">
                                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Date" Width="30px"></asp:Label>
                                                                                </td>
                                                                                <td class="item item-selection" valign="middle">
                                                                                    <asp:TextBox ID="txtdates" Font-Size="Small" runat="server" Enabled="false" Style="width: 75px;
                                                                                        height: 20px; vertical-align: middle"></asp:TextBox>
                                                                                </td>
                                                                                <td class="item item-selection" valign="middle">
                                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Width="100px" Text="Asset Book value as on "></asp:Label>
                                                                                </td>
                                                                                <td class="item item-selection" valign="middle">
                                                                                    <asp:TextBox ID="txtassvaluedate" runat="server" Enabled="false" Font-Size="Small"
                                                                                        Style="width: 75px; height: 20px; vertical-align: middle"></asp:TextBox>
                                                                                </td>
                                                                                <td align="left" class="item item-selection">
                                                                                    <asp:Label ID="lblassetamount" runat="server" Width="100px" Font-Bold="true" Text="Amount"></asp:Label>
                                                                                </td>
                                                                                <td align="left" class="item item-selection" valign="middle">
                                                                                    <asp:TextBox ID="txtassetamount" runat="server" Enabled="false" CssClass="filter_item"
                                                                                        MaxLength="50" ToolTip="Assest As On Value" Width="75px" onkeyup="AssetCalc();"></asp:TextBox>
                                                                                </td>
                                                                                <td align="left" class="item item-selection">
                                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Width="100px" Text="Asset Selling Amount"></asp:Label>
                                                                                </td>
                                                                                <td align="left" class="item item-selection" valign="middle">
                                                                                    <asp:TextBox ID="txtasstsellingamt" runat="server" Enabled="false" CssClass="filter_item"
                                                                                        MaxLength="50" ToolTip="Assest Selling Value" Width="75px" onkeyup="AssetCalc();"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr align="center">
                                                                                <td class="item item-selection" colspan="2">
                                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Width="150px" Text="Profit on Sale of Asset"></asp:Label>
                                                                                </td>
                                                                                <td class="item item-selection" valign="middle" colspan="2">
                                                                                    <asp:TextBox ID="lblprofitsaleamount" Width="160px" ReadOnly="true" ForeColor="White"
                                                                                        Font-Bold="true" BackColor="Green" BorderWidth="3px" BorderStyle="2" BorderColor="Black"
                                                                                        runat="server" Text="" align="center"></asp:TextBox>
                                                                                </td>
                                                                                <td align="left" class="item item-selection" valign="middle" colspan="2">
                                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Width="150px" Text="Loss on Sale of Asset "></asp:Label>
                                                                                </td>
                                                                                <td class="item item-selection" colspan="2" valign="middle" align="center">
                                                                                    <asp:TextBox ID="lbllosssaleamount" Width="160px" ReadOnly="true" ForeColor="White"
                                                                                        Font-Bold="true" BorderWidth="3px" BorderStyle="2" BackColor="Red" BorderColor="Black"
                                                                                        runat="server" Text="" align="center"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="800px" colspan="8">
                                                                                    <table align="center" class="search_table" width="800px">
                                                                                        <tr>
                                                                                            <td align="right" class="item item-selection" colspan="2" width="300px">
                                                                                                <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Name of the Buyer"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" class="item item-selection" colspan="4" valign="middle">
                                                                                                <asp:TextBox ID="txtname" runat="server" Font-Bold="true" Enabled="false" Font-Size="Small" CssClass="filter_item"
                                                                                                    MaxLength="200" ToolTip="Buyer Name" Width="500px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" class="item item-selection" colspan="2" width="300px">
                                                                                                <asp:Label ID="lbldate" runat="server" Font-Bold="true" Text="Address of the Buyer"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" class="item item-selection" colspan="4" valign="middle">
                                                                                                <asp:TextBox ID="txtaddress" runat="server" Font-Bold="true" Enabled="false" Font-Size="Small" CssClass="filter_item"
                                                                                                    TextMode="MultiLine" ToolTip="Description" Width="500px" Height="150px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Trgvusers" runat="server">
                                                                    <td colspan="8">
                                                                        <asp:GridView runat="server" ID="gvusers" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                                            PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                            DataKeyNames="" GridLines="none" Width="765px" ShowHeaderWhenEmpty="true" OnRowDataBound="gvusers_RowDataBound">
                                                                            <HeaderStyle CssClass="headerstyle" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="" ItemStyle-Width="300px" HeaderText="Status" ItemStyle-Wrap="false" />
                                                                                <asp:BoundField DataField="" ItemStyle-Width="300px" HeaderText="Role" ItemStyle-Wrap="false" />
                                                                                <asp:BoundField DataField="" ItemStyle-Width="400px" HeaderText="Name" ItemStyle-Wrap="false" />
                                                                                <%--  <asp:BoundField DataField="" ItemStyle-Width="100px" HeaderText="UserID" ItemStyle-Wrap="false" />--%>
                                                                            </Columns>
                                                                            <RowStyle CssClass=" grid-row char grid-row-odd" HorizontalAlign="Center" />
                                                                            <PagerStyle CssClass="grid pagerbar" />
                                                                            <HeaderStyle CssClass="grid-header" />
                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" HorizontalAlign="Center" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr class="pagerbar">
                                                                    <td class="pagerbar-cell" align="right">
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tblbtnupdate" runat="server" width="700px">
                                        <tr align="center">
                                            <td align="center">
                                                <asp:Button ID="btnapprove" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Approval" OnClick="btnapprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
