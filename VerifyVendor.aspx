<%@ Page Title="VerifyVendor" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyVendor.aspx.cs" Inherits="VerifyVendor" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=txtvatpan.ClientID %>", "<%=txttintax.ClientID %>", "<%=txtcstpf.ClientID %>",
                                "<%=txtVName.ClientID %>", "<%=txtAddress.ClientID %>");
            return CheckInputs(objs);
            if (SelectedIndex("<%=rbtngstu.ClientID %>") == 0) {
                GridView3 = document.getElementById("<%=gvotheru.ClientID %>");
                if (GridView3 != null) {
                    for (var rowCount = 1; rowCount < GridView3.rows.length - 1; rowCount++) {
                        if (GridView3.rows(rowCount).cells(1).children(0).checked == false) {
                            window.alert("Please verify");
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(2).children[0].value == "Select") {
                            window.alert("Select State");
                            GridView3.rows(rowCount).cells(2).children[0].focus();
                            return false;
                        }
                        else if (GridView3.rows(rowCount).cells(3).children[0].value == "") {
                            window.alert("GST No Required");
                            GridView3.rows(rowCount).cells(3).children[0].focus();
                            return false;
                        }
                    }
                }

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td style="width: 700px; height: 100%;">
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                        <table style="width: 700px;">
                            <tr align="center">
                                <th>
                                    <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text="Verify Vendor"></asp:Label>
                                </th>
                            </tr>
                            <tr align="center">
                                <td align="center">
                                    <asp:GridView ID="GridView1" runat="server" Width="700px" AutoGenerateColumns="false"
                                        HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" GridLines="None"
                                        HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        RowStyle-CssClass=" grid-row char grid-row-odd" FooterStyle-BackColor="DarkGray"
                                        EmptyDataText="There is no Records" DataKeyNames="id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField HeaderText="Edit" ButtonType="Image" ItemStyle-Width="20px" ShowSelectButton="true"
                                                SelectImageUrl="~/images/iconset-b-edit.gif" />
                                            <asp:BoundField DataField="vendor_name" ItemStyle-Width="150px" HeaderText="Vendor Name"
                                                ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="vendor_phone" ItemStyle-Width="50px" HeaderText="Vendor Phone" />
                                            <%-- <asp:BoundField DataField="servicetax_no" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="vat_no" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="pan_no" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="tin_no" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="cst_no" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />--%>
                                            <asp:BoundField DataField="address" ItemStyle-Width="300px" HeaderText="Address" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table id="tblvendor" runat="server">
                            <tr valign="top">
                                <td align="center">
                                    <table class="estbl" width="700px">
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
                                                <asp:TextBox ID="txtVName" CssClass="estbox" runat="server" Enabled="false" ToolTip="Vendor Name"
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
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Bank Name"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtbankname" CssClass="estbox" runat="server" ToolTip="Bank Name"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Bank Account No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtacno" CssClass="estbox" runat="server" ToolTip="Bank Account No"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="IFSC Code"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtifsc" CssClass="estbox" runat="server" ToolTip="IFSC Code" MaxLength="50"></asp:TextBox>
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
                                    </table>
                                    <table id="tblstateu" runat="server" width="600px" style="background-color: #DCDCDC">
                                        <tr align="center">
                                            <td colspan="2">
                                                <asp:Label ID="Label15" runat="server" Font-Size="10px" Width="600px" CssClass="eslbl"
                                                    Text="GST Yes/No"></asp:Label>
                                                <asp:RadioButtonList ID="rbtngstu" runat="server" Width="100px" AutoPostBack="true"
                                                    ClientIDMode="AutoID" OnSelectedIndexChanged="rbtngstu_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" Style="font-size: x-small" ToolTip="GST Yes or No">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                    <%--onclick="javascript:return getotherIndex(this)"--%>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trothergridu" runat="server" style="display: none">
                                            <td colspan="2">
                                                <asp:GridView runat="server" ID="gvotheru" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" ShowFooter="true" Width="600px" ShowHeaderWhenEmpty="true"
                                                    OnRowDeleting="gvotheru_RowDeleting" OnRowDataBound="gvotheru_RowDataBound">
                                                    <HeaderStyle CssClass="headerstyle" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="No" ItemStyle-Width="5px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkgstu" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlstatesu" Font-Size="7" Width="250px" CssClass="filter_item"
                                                                    ToolTip="State" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST No" ItemStyle-Width="125px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtregnou" runat="server" CssClass="filter_item" Width="125px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnaddgstu" runat="server" Text="Add GST No's" OnClick="btnaddgstu_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="50px" DeleteText="Remove" />
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
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnUpdate" runat="server" CssClass="esbtn" Text="Update" OnClientClick="javascript:return validate()"
                                                    OnClick="btnUpdate_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btndelete" runat="server" CssClass="esbtn" Text="Delete" OnClick="btndelete_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" CssClass="esbtn" runat="server" Text="Back" OnClick="btnCancel_Click" />
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
