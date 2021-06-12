<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="ViewChequeDetails.aspx.cs" Inherits="ViewChequeDetails" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="width: 700px">
                    <tr valign="top">
                        <td align="center">
                            <table class="estbl" width="600px">
                                <tr style="border: 1px solid #000">
                                    <th colspan="3">
                                        <asp:Label ID="itform" CssClass="esfmhead" runat="server" Text="View ChequeBook Details "></asp:Label>
                                    </th>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <asp:Label ID="lblbankname" CssClass="eslbl" runat="server" Text="Bank Name"></asp:Label>
                                        <asp:DropDownList ID="ddlbankname" runat="server" ToolTip="Bank" CssClass="esddown"
                                            AutoPostBack="true" Width="200px" OnSelectedIndexChanged="ddlbankname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlbankname"
                                            ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                            PromptText="Select">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                            HorizontalAlign="Center" CssClass="grid-content" BorderColor="Black" GridLines="Both"
                                            HeaderStyle-CssClass="grid-header" EmptyDataText="There is no records" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                            RowStyle-CssClass=" grid-row char grid-row-odd" OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="Bank Name" DataField="bankname" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Cheque CreatedDate" DataField="issuedate" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Cheque Nos" DataField="chequeno" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Party Name" DataField="Party_Name" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Cheque IssueDate " DataField="Created_date" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Cheque Status" DataField="status" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
