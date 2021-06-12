<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="frmviewcc.aspx.cs" Inherits="Admin_frmviewcc" Title="View Cost Center-Essel Projects Pvt.Ltd." %>

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
                            <asp:Panel ID="panel3" runat="server">
                                <table class="estbl" width="400px">
                                    <tr style="border: 1px solid #000">
                                        <th valign="top" align="center" colspan="2">
                                            <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View Cost Center Details"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 150px">
                                            <asp:Label ID="Label26" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                        </td>
                                        <td style="width: 250px" align="right">
                                            <asp:DropDownList ID="ddl" runat="server" CssClass="esddown" OnSelectedIndexChanged="ddl_SelectedIndexChanged"
                                                Width="200px" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <span id="s1" runat="server" class="starSpan">*</span>
                                            <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="dd" LoadingText="Please Wait"
                                                PromptText="Select Cost Center" ServiceMethod="codename" ServicePath="cascadingDCA.asmx"
                                                TargetControlID="ddl">
                                            </cc1:CascadingDropDown>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table class="estbl" width="750px">
                                            <tr style="border: 1px solid #000">
                                                <th valign="top" colspan="4" align="center">
                                                    <asp:Label ID="Label8" runat="server" CssClass="esfmhead" Text="View Cost Center Details"></asp:Label>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" CssClass="eslbl" Text="CC Code:-"></asp:Label>
                                                </td>
                                                <td style="width: 180px">
                                                    <asp:Label ID="ccode" CssClass="eslbltext" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" CssClass="eslbl" Text="CC Name:-"></asp:Label>
                                                </td>
                                                <td style="width: 180px">
                                                    <asp:Label ID="ccname" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="kk" runat="server">
                                                <td>
                                                    <asp:Label ID="Label27" runat="server" CssClass="eslbl" Text="CC Type"></asp:Label>
                                                </td>
                                                <td style="width: 180px">
                                                    <asp:Label ID="lblcctype" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label29" CssClass="eslbl" runat="server" Text="CC Sub Type"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblccsubtype" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trcc" runat="server">
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="EPPL Final OfferNo"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="epplfinalofferno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="Final OfferDate"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="finalofferdate" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trrefno" runat="server">
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="Client Acceptance Reference No"></asp:Label>
                                                </td>
                                                <td style="width: 180px">
                                                    <asp:Label ID="refno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label9" CssClass="eslbl" runat="server" Text="Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="date" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trdl" runat="server">
                                                <td colspan="4">
                                                    <asp:DataList ID="dl" runat="server" RepeatColumns="1">
                                                        <ItemTemplate>
                                                            <table class="estbl" width="750px">
                                                                <tr id="trclient" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Client:-"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="client" runat="server" CssClass="eslbltext" Text='<%#Bind("client_name") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Customer:-"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="cust" runat="server" CssClass="eslbltext" Text='<%#Bind("customer_name") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="po" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="Label18" runat="server" CssClass="eslbl" Text="Po No:-"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="pono" runat="server" CssClass="eslbltext" Text='<%#Bind("po_no") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Project Name:-"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="pname" runat="server" CssClass="eslbltext" Text='<%#Bind("project_name") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="podate" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="Label10" runat="server" CssClass="eslbl" Text="Po Date:-"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label13" runat="server" CssClass="eslbltext" Text='<%#Bind("po_date") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label20" runat="server" CssClass="eslbl" Text="PO Value:-"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label25" runat="server" CssClass="eslbltext" Text='<%#Bind("po_basicvalue") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trnoj" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Nature of Job:-"></asp:Label>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="noj" runat="server" CssClass="eslbltext" Text='<%#Bind("natureofjob") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                            <tr id="trpovalue" runat="server">
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Total PO Value:-"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lbltotalPOvalue" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="4">
                                                    <asp:Label ID="Label3" runat="server" CssClass="esfmhead" Text="Project Incharge"></asp:Label>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Name:-"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="piname" CssClass="eslbltext" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Contact No:-"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="cno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" CssClass="eslbl" Text="Address:-"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="add" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="4">
                                                    <asp:Label ID="Label19" runat="server" CssClass="esfmhead" Text="CC Admin Incharge"></asp:Label>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label21" runat="server" CssClass="eslbl" Text="Incharge Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="incname" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label22" runat="server" CssClass="eslbl" Text="Incharge Phone No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="inphno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label23" runat="server" CssClass="eslbl" Text="Address"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="addres" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label24" runat="server" CssClass="eslbl" Text="Phone No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="phno" runat="server" CssClass="eslbltext" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
