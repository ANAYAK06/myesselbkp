<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ITDepreciationReport.aspx.cs" Inherits="ITDepreciationReport" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/bubble-tooltip.css" rel="stylesheet" type="text/css" />
    <script src="Java_Script/Prototype.js" type="text/javascript"></script>
    <script src="Java_Script/Tooltip.js" type="text/javascript"></script>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validate() {

            var objs = new Array("<%=ddlyear.ClientID %>");
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
                                        IT Depreciation Report
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Financial Year:" Font-Bold="True"
                                            Font-Size="Small"></asp:Label>
                                        <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                            Text="View" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border-left:1px solid black; border-right:1px solid black; border-top:1px solid black; background-color:White">
                        <asp:Label runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Black" ID="lblheader" ></asp:Label>                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    Font-Size="Small" ShowFooter="true" CssClass="gridviewstyle" 
                                    onrowdatabound="GridView1_RowDataBound">
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SL No.</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Transaction_id" HeaderText="Transaction Id" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Actual_Invoice" HeaderText="Actual Invoice" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Item_code" ItemStyle-Width="150px" HeaderText="Item Code"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Assetname"  HeaderText="Asset Name"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="true"/>
                                        <asp:BoundField DataField="Subdca_code" HeaderText="Subdca Code" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Asset_category" HeaderText="Asset Category" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Purcahse_Price" HeaderText="Purchased Price" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}" />
                                        <asp:BoundField DataField="FY_Opening_bal" HeaderText="FY Opening Balance" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}" />
                                        <asp:BoundField DataField="Current_FY_Addition" HeaderText="Current FY Addition"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}"/>
                                        <asp:BoundField DataField="Total_value" HeaderText="Total Value" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}" />
                                        <asp:BoundField DataField="Purchased_date" HeaderText="Date of Purchase" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Deletion_value" HeaderText="Deletion Value" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}" />
                                        <asp:BoundField DataField="Deletion_date" HeaderText="Deletion Date" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="remaining_days" HeaderText="Remaining Days" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="valid_days" HeaderText="Valid Days" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Deppercentage" HeaderText="Dep Percentage" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Dep_Amount" HeaderText="Depreciation Value" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}"/>
                                        <asp:BoundField DataField="Bal_value" HeaderText="Balance Value " ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}"/>                                      
                                        <asp:BoundField DataField="Sold_price" HeaderText="Sold Price" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}" />
                                        <asp:BoundField DataField="" HeaderText="Gain/Loss Value" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}"/>
                                        <asp:BoundField DataField="" HeaderText="FY Closing Balance" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##,##,###.00}"/>
                                      <%--  <asp:BoundField DataField="Opening_date" HeaderText="Opening Date" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Closing_date" HeaderText="Closing Date" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                           --%>         </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr id="trexcel" runat="server">
                        <td align="left">
                            <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                            <asp:ImageButton ID="btnExcel" runat="server" 
                                ImageUrl="~/images/ExcelImage.jpg" onclick="btnExcel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
