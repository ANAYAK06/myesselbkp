<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmviewvendor.aspx.cs"
    Inherits="Admin_frmviewvendor" EnableEventValidation="false" Title="View Vendors - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style9
        {
            width: 203px;
        }
        .style10
        {
            width: 154px;
        }
        .style11
        {
            width: 10px;
        }
        .style12
        {
            width: 181px;
        }
    </style>
    <script type="text/javascript">
        function check() {
            var tvatpan = document.getElementById("<%=txtvatpan.ClientID %>");
            var lvatpan = document.getElementById("<%=lblvatpan.ClientID %>");
            var ttintax = document.getElementById("<%=txttintax.ClientID %>");
            var ltintax = document.getElementById("<%=lbltintax.ClientID %>");
            var tcstpf = document.getElementById("<%=txtcstpf.ClientID %>");
            var lcstpf = document.getElementById("<%=lblcstpf.ClientID %>");
            if (type == 'Service Provider') {
                tvatpan.title = lvatpan.innerHTML = "PAN No";
                ttintax.title = ltintax.innerHTML = "ServiceTax No";
                tcstpf.title = lcstpf.innerHTML = "PF Reg No";
            }
            else {
                tvatpan.title = lvatpan.innerHTML = "VAT No";
                ttintax.title = ltintax.innerHTML = "TIN No";
                tcstpf.title = lcstpf.innerHTML = "CST No";
            }
        }
        function validate() {
            var objs = new Array("<%=txtVName.ClientID %>", "<%=txtAddress.ClientID %>");
            return CheckInputs(objs);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="width: 700px">
                    <tr valign="middle">
                        <td>
                            <table align="center" width="750px">
                                <tr>
                                    <th valign="top" align="center">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="View Vendors  " CssClass="eslbl"></asp:Label>
                                        <asp:DropDownList ID="ddlvtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvtype_SelectedIndexChanged">
                                            <asp:ListItem>All Vendors</asp:ListItem>
                                            <asp:ListItem Value="Supplier">Suppliers</asp:ListItem>
                                            <asp:ListItem Value="Service Provider">Service Providers</asp:ListItem>
                                            <asp:ListItem Value="Trading Supply">Trading Supply</asp:ListItem>
                                        </asp:DropDownList>
                                    </th>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                            AlternatingRowStyle-CssClass="alt" Width="700px" OnPageIndexChanging="GridView1_PageIndexChanging"
                                            AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                            OnRowDataBound="GridView1_RowDataBound1" OnRowEditing="GridView1_RowEditing">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" Width="30px" Height="30px" ImageUrl="images/magnify.gif" />
                                                        <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="Panel1"
                                                            DynamicServicePath="EsselServices.asmx" TargetControlID="Image1" DynamicContextKey='<%# Eval("id") %>'
                                                            DynamicControlID="Panel1" DynamicServiceMethod="VendorDetails" Position="Bottom">
                                                        </cc1:PopupControlExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:CommandField ButtonType="Image" HeaderText="View GST Nos" ShowEditButton="true"
                                                    ItemStyle-Width="15px" EditImageUrl="~/images/fields-a-lookup-a.gif" />--%>
                                                <asp:TemplateField HeaderText="View GST">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="id" CommandName="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Vendor_Id" ReadOnly="true" HeaderText="ID" />
                                                <asp:BoundField DataField="Vendor_name" ReadOnly="true" HeaderText="Name" />
                                                <asp:BoundField DataField="Address" ReadOnly="true" HeaderText="Address" />
                                                <asp:BoundField DataField="vendor_phone" ReadOnly="true" HeaderText="PhoneNO" />
                                                <asp:BoundField DataField="vendor_mobile" ReadOnly="true" HeaderText="MobileNO" />
                                                <asp:CommandField HeaderText="Edit" ButtonType="Image" Visible="false" ItemStyle-Width="20px"
                                                    ShowSelectButton="true" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                <%--  <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" CssClass="eslkbtn" runat="server" NavigateUrl='<%#"frmAddVendor.aspx?Vendorid="+ Eval("vendor_id") %>'>Edit</asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <asp:Panel ID="Panel1" runat="server">
                                    </asp:Panel>
                                </tr>
                            </table>
                            <table id="tblvendor" runat="server" width="750px">
                                <tr>
                                    <td>
                                        <table id="Table1" align="center" runat="server">
                                            <tr valign="top">
                                                <td align="center">
                                                    <table class="estbl" width="350px" align="center">
                                                        <tr>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Vendor Type"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblvtype" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblvatpan" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtvatpan" CssClass="estbox" runat="server" ToolTip="VAT No" MaxLength="50"></asp:TextBox><span
                                                                    class="starSpan">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbltintax" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txttintax" CssClass="estbox" runat="server" ToolTip="TIN No" MaxLength="50"></asp:TextBox>
                                                                <span class="starSpan">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblcstpf" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtcstpf" CssClass="estbox" runat="server" ToolTip="CST No" MaxLength="50"></asp:TextBox>
                                                                <span class="starSpan">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Vendor Name"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtVName" CssClass="estbox" runat="server" ToolTip="Vendor Name"
                                                                    MaxLength="50"></asp:TextBox><span class="starSpan">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" CssClass="eslbl" Text="Phone No"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtpno" CssClass="estbox" runat="server" ToolTip="Phone No" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text="Mobile No"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtmno" CssClass="estbox" runat="server" ToolTip="Mobile No" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Address"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="estbox" ToolTip="Address" TextMode="MultiLine"></asp:TextBox><span
                                                                    class="starSpan">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Button ID="update" runat="server" CssClass="esbtn" Text="Update" OnClientClick="javascript:return validate()"
                                                                    OnClick="update_Click" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnCancel" CssClass="esbtn" runat="server" Text="Back" OnClick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc1:ModalPopupExtender ID="popitems" BehaviorID="mdlitems" runat="server" TargetControlID="btnModalPopUp"
        PopupControlID="pnlreport" BackgroundCssClass="modalBackground1" DropShadow="false" />
    <asp:Panel ID="pnlreport" runat="server" Style="display: none;">
        <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
            height: 500px;" align="center">
            <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="Grdviewpopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                        PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                        ShowFooter="false">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Serial No.</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="state" HeaderText="State" />
                            <asp:BoundField DataField="Gst_No" HeaderText="GST No" />
                        </Columns>
                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                        <PagerStyle CssClass="grid pagerbar" />
                        <HeaderStyle CssClass="grid-header" />
                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                    </asp:GridView>
                    <button class="button-error pure-button" onclick="closepopup();">
                        Close</button>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
    </asp:Panel>
    <script type="text/javascript">
        function closepopup() {
            $find('mdlitems').hide();
        }
    </script>
</asp:Content>
