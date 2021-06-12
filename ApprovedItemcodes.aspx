<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ApprovedItemcodes.aspx.cs"
    Inherits="ApprovedItemcodes" Title="Approved Item Code - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground
        {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table width="100%" style="vertical-align: top">
                    <tr valign="top">
                        <td style="vertical-align: top">
                            <div class="wrap">
                                <table class="view" cellpadding="0" cellspacing="0" border="0" width="90%">
                                    <tr>
                                        <td style="width: 90%" valign="top">
                                            <h1>
                                                Item code Status<a class="help" href="/view_diagram/process?res_model=hr.employee&amp;res_id=False&amp;title=Employees"
                                                    title="Corporate Intelligence..."> <small>Help</small></a></h1>
                                        </td>
                                    </tr>
                                    <tr align="center" style="width: 100%" height="20px" id="trvouchertype" runat="server">
                                        <td style="" align="center" valign="middle">
                                            <asp:DropDownList ID="ddlitemstatus" runat="server" CssClass="char" Height="20px"
                                                Width="162px" AutoPostBack="true" OnSelectedIndexChanged="ddlitemstatus_SelectedIndexChanged">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Pending for Approval</asp:ListItem>
                                                <asp:ListItem>Approved Item Codes</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp
                                            <asp:DropDownList ID="ddlitemtype" runat="server" CssClass="char" Height="20px" Width="162px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Amended Item Codes</asp:ListItem>
                                                <asp:ListItem>New Item Codes</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 100%;">
                                            <h1 style="font-size: medium;">
                                                <asp:Label ID="newitem" runat="server" Text="" Font-Bold="True" Font-Overline="False"
                                                    Font-Underline="True"></asp:Label></h1>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="grid-content">
                                            <table id="Table1" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                                style="background: none;" width="100%">
                                                <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="id"
                                                    EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                    PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd" OnRowEditing="GridView1_RowEditing2"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:BoundField DataField="Item_code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="item_name" HeaderText="item Name" ItemStyle-HorizontalAlign="center"
                                                            ItemStyle-Width="150px" />
                                                        <asp:BoundField DataField="Basic_Price" HeaderText="Basic Price" DataFormatString="{0:0.00}"
                                                            ItemStyle-HorizontalAlign="right" />
                                                        <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Subdca_Code" HeaderText="Subdca Code" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:CommandField ButtonType="button" ShowEditButton="true" EditText="OK" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("status")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                </asp:GridView>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="grid-content">
                                            <table id="Table2" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                                style="background: none;" width="100%">
                                                <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="id"
                                                    EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                    PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:BoundField DataField="Item_code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="item_name" HeaderText="item Name" ItemStyle-HorizontalAlign="center"
                                                            ItemStyle-Width="150px" />
                                                        <asp:BoundField DataField="Basic_Price" HeaderText="Basic Price" DataFormatString="{0:0.00}"
                                                            ItemStyle-HorizontalAlign="right" />
                                                        <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Subdca_Code" HeaderText="Subdca Code" ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                </asp:GridView>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="50px"> 
                                            <asp:Label runat="server" Width="50px" Visible="false" Text=""></asp:Label>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="Small" 
                                                ForeColor="#33CC33" onclick="LinkButton1_Click">Click here for Available Itemscodes</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
