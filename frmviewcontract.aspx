<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="frmviewcontract.aspx.cs" Inherits="Admin_frmviewcontract" Title="View Contract - Essel Projects Pvt. Ltd." %>

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
                <table style="width: 700px">
                    <tr valign="top">
                        <td align="center">
                            <table>
                                <tr style="border: 1px solid #000">
                                    <td>
                                        <asp:Label ID="Label26" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                        <asp:DropDownList ID="ddlCC" runat="server" CssClass="esddown" Width="200px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCC_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <span id="s1" runat="server" class="starSpan">*</span>
                                        <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="dd" LoadingText="Please Wait"
                                            ServiceMethod="performingcc" ServicePath="cascadingDCA.asmx" TargetControlID="ddlCC">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                                <tr style="border: 1px solid #000">
                                    <td valign="middle">
                                        <asp:Label ID="Label16" CssClass="eslbl" runat="server" Text="Select PO No:-"></asp:Label>
                                        <asp:DropDownList ID="ddlpo" runat="server" CssClass="esddown">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnView" runat="server" CssClass="esbtn" Text="View" Height="20px"
                                            Width="42px" OnClick="btnView_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="panel1" runat="server">
                                <table class="estbl" width="700px">
                                    <tr style="border: 1px solid #000">
                                        <th valign="top" colspan="4" align="center">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Project Information"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Project Name"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="pname" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Client Name"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="clname" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Division"></asp:Label>
                                        </td>
                                        <td colspan="1" style="">
                                            <asp:Label ID="div" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" CssClass="eslbl" Text="CC Code"></asp:Label>
                                        </td>
                                        <td style="">
                                            <asp:Label ID="lblcccode" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Start Date"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="sdate" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="End Date"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="edate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Nature Of Job"></asp:Label>
                                        </td>
                                        <td colspan="3" style="">
                                            <asp:Label ID="noj" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Project Manager Name"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="pmname" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="Project Manager Contact No"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="pmcontact" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="Customer Name"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="custname" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="PO Basic Value"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="pobvalue" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" CssClass="eslbl" Text="PO Service Tax"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="poaservicetaxval" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" CssClass="eslbl" Text="PO Total"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="pototal" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Panel ID="p1" runat="server">
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th valign="top" colspan="4" align="center">
                                            <asp:Label ID="Label17" runat="server" ForeColor="Black" Font-Bold="true" Text="Total Details"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="Total Basic Value"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="totbvalue" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="Total ServiceTax Value"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label ID="tstvalue" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="Total"></asp:Label>
                                        </td>
                                        <td colspan="3" style="">
                                            <asp:Label ID="total" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th valign="top" colspan="4" align="center">
                                            <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                                        </th>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
