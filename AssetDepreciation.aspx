<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AssetDepreciation.aspx.cs"
    Inherits="AssetDepreciation" EnableEventValidation="false" Title="Untitled Page" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/bubble-tooltip.css" rel="stylesheet" type="text/css" />

    <script src="Java_Script/Prototype.js" type="text/javascript"></script>

    <script src="Java_Script/Tooltip.js" type="text/javascript"></script>

    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function validate() {

            var objs = new Array("<%=ddlcccode.ClientID %>", "<%=ddlyear.ClientID %>");
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
                <table style="width: 700px">
                    <tr>
                        <td align="center">
                            <table width="500px" class="estbl eslbl">
                                <tr>
                                    <th align="center">
                                        Asset Depreciation
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="paytype" runat="server" class="innertab">
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblccode" runat="server" Text="CC Code"></asp:Label>
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="ddlcccode" CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);"
                                                                    runat="server" Width="200px" ToolTip="Cost Center">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlcccode"
                                                                    ServicePath="cascadingDCA.asmx" Category="cc" LoadingText="Loading CC" ServiceMethod="DepCC"
                                                                    PromptText="Select Cost Center">
                                                                </cc1:CascadingDropDown>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="yearly"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                            Text="View" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />&nbsp
                                        <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small"
                                            OnClick="btncancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    Font-Size="Small" ShowFooter="true" CssClass="gridviewstyle" OnRowDataBound="GridView1_RowDataBound">
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="subdca_code" HeaderText="Subdca Code" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="item_name" ItemStyle-Width="150px" HeaderText="ItemName"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="basicprice" HeaderText="UPDATED PRICE OF THE ITEM" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="DAYS FOR CURRENT FY" HeaderText="DAYS COUNTED FOR DEPRECITAION FOR CURRENT FY"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="DAYS FOR PREVIOUS FY" HeaderText="DAYS COUNTED FOR DEPRECITAIN UP TO PREVIOUS FY"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="DEPRECIATION VALUE FOR CURRENT FY" HeaderText="DEPRECIATION VALUE FOR CURRENT FY"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="DEPRECIATION VALUE FOR PREVIOUS FY" HeaderText="DEPRECIATION VALUE UP TO PREVIOUS FY"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="cudepvalue" HeaderText="CUM.DEPRICITOIN  VALUE UP TO DATE" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr id="trexcel" runat="server">
                        <td align="left">
                            <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                OnClick="btnExcel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
