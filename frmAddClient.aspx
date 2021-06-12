<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmAddClient.aspx.cs"
    Inherits="Admin_frmAddClient" EnableEventValidation="false" Title="Add Client - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validate() {
            var objs = new Array("<%=ddlcategory.ClientID %>", "<%=txtClientName.ClientID %>", "<%=txttin.ClientID %>", "<%=txtpan.ClientID %>", "<%=txttan.ClientID %>", "<%=txtPersonname.ClientID %>", "<%=txtPsnphoneno.ClientID %>", "<%=txtAddress.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (!ChceckRBL("<%=rbtngst.ClientID %>")) {
                return false;
            }
            //debugger;
            if (SelectedIndex("<%=rbtngst.ClientID %>") == 0) {
                GridView3 = document.getElementById("<%=gvother.ClientID %>");
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
            document.getElementById("<%=btnAddClient.ClientID %>").style.display = 'none';
            return true;
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
                    <tr valign="top">
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                        <ProgressTemplate>
                                            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                    left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                    <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                </div>
                                            </asp:Panel>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <table class="estbl" width="600px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="4" align="center">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Add Client Form" CssClass="eslbl"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr id="Category" runat="server">
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="Category" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcategory" runat="server" Width="150px" ToolTip="Category"
                                                    CssClass="esddown">
                                                    <asp:ListItem>select Category </asp:ListItem>
                                                    <asp:ListItem>Service Customer</asp:ListItem>
                                                    <asp:ListItem>Trading Customer </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Client Name" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtClientName" runat="server" CssClass="estbox" MaxLength="50"></asp:TextBox><span
                                                    class="starSpan">*</span><span id="spanAvailability"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1">
                                                <asp:Label ID="Label3" runat="server" Text="TIN NO" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttin" runat="server" CssClass="estbox" MaxLength="50"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="PAN NO" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtpan" runat="server" CssClass="estbox" MaxLength="50"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label8" runat="server" Text="TAN NO" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttan" runat="server" CssClass="estbox" MaxLength="50"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="Label5" runat="server" Text="Contact Person Name" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left" colspan="1" valign="top">
                                                <asp:TextBox ID="txtPersonname" runat="server" CssClass="estbox" MaxLength="50"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                            <%-- <td>
                                        <asp:Label ID="Label8" runat="server" Text="Branch" CssClass="eslbl"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBranch" runat="server" CssClass="estbox" MaxLength="50"></asp:TextBox><span
                                            class="starSpan">*</span>
                                    </td>--%>
                                        </tr>
                                        <tr>
                                            <td colspan="1">
                                                <asp:Label ID="Label6" runat="server" Text="Person Phone No" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPsnphoneno" runat="server" CssClass="estbox" MaxLength="50"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Address" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="estbox"
                                                    MaxLength="50"></asp:TextBox>
                                                <span class="starSpan">*</span>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="600px" style="background-color: #DCDCDC">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="lblother" runat="server" Font-Size="10px" Width="600px" 
                                                    CssClass="eslbl" Text="GST Yes/No"></asp:Label>
                                                <asp:RadioButtonList ID="rbtngst" runat="server" Width="100px" 
                                                    AutoPostBack="true" ClientIDMode="AutoID" OnSelectedIndexChanged="rbtngst_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" Style="font-size: x-small" ToolTip="GST Yes or No">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trothergrid" runat="server" style="display: none">
                                            <td>
                                                <asp:GridView runat="server" ID="gvother" HeaderStyle-HorizontalAlign="Center" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" BorderColor="black" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    DataKeyNames="" GridLines="Both" ShowFooter="true" Width="600px" ShowHeaderWhenEmpty="true"
                                                    OnRowDeleting="gvother_RowDeleting" OnRowDataBound="gvother_RowDataBound">
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
                                                                <asp:CheckBox ID="chkgst" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlstates" Font-Size="7" Width="250px" CssClass="filter_item"
                                                                    ToolTip="State" runat="server">
                                                                    <asp:ListItem Value="Select" Enabled="true" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST No" ItemStyle-Width="125px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtregno" runat="server" CssClass="filter_item" Width="125px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnaddgst" runat="server" Text="Add GST No's" OnClick="btnaddgst_Click" />
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
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnAddClient" runat="server" Text="Add Client" OnClientClick="javascript:return validate()"
                                                    OnClick="btnAddClient_Click" CssClass="esbtn" />
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
