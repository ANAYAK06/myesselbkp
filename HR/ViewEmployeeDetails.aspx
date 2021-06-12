<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewEmployeeDetails.aspx.cs" Inherits="ViewEmployeeDetails" %>

<%@ Register Src="~/HR/HRVerticalMenu.ascx" TagName="Menu" TagPrefix="HRMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<link href="../Css/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
<link href="../Css/buttons.css" rel="stylesheet" type="text/css" />
<link href="../Css/print.css" rel="stylesheet" type="text/css" />
<link href="../Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function report(addr) {
            window.location(addr);
            return false;
        }
        function report1(addr) {
            var employeeid = 0;
            var Type = 3;
            window.location("EmployeeRegister.aspx?employeeid=" + employeeid + "&Type=" + Type);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <HRMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>View Employees <a class="help"
                                    href="" title=""><small>Help</small> </a>
                            </h1>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <div id="body_form">
                                            <div>
                                                <div id="server_logs">
                                                </div>
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top">
                                                                <div class="box-a list-a">
                                                                    <div class="inner">
                                                                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                            <tbody>
                                                                                <tr class="pagerbar">
                                                                                    <td class="pagerbar-cell" align="right">
                                                                                        <table class="pager-table">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="pager-cell">
                                                                                                        <h2>
                                                                                                        </h2>
                                                                                                    </td>
                                                                                                    <td class="loading-list">
                                                                                                        <asp:Button ID="btnadd" runat="server" CssClass="button" Text="Add" OnClientClick="javascript:return report1(this.href);" />
                                                                                                    </td>
                                                                                                    <td class="pager-cell" style="width: 90%" valign="middle">
                                                                                                        <div class="pager">
                                                                                                            <div align="right">
                                                                                                                <asp:Label ID="Label2" CssClass="item item-char" runat="server" Text="Change Limit:"></asp:Label>
                                                                                                                <asp:DropDownList ID="ddlpagecount" runat="server">
                                                                                                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                                                                                                    <asp:ListItem>20</asp:ListItem>
                                                                                                                    <asp:ListItem>50</asp:ListItem>
                                                                                                                    <asp:ListItem>100</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="grid-content">
                                                                                        <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0">
                                                                                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false"
                                                                                                CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                                                                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                                                                PagerStyle-CssClass="grid pagerbar" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                                                OnDataBound="GridView1_DataBound" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="View">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HyperLink ID="HyperLink2" onclick="javascript:return report(this.href);" NavigateUrl='<%#"~/HR/EmployeeRegister.aspx?id="+Eval("id")+"&Type=1" %>'
                                                                                                                runat="server">
                                                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/fields-a-lookup-a.gif" Width="20px"
                                                                                                                    Height="20px" AlternateText="Print" />
                                                                                                            </asp:HyperLink>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Edit">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HyperLink ID="HyperLink1" onclick="javascript:return report(this.href);" NavigateUrl='<%#"~/HR/EmployeeRegister.aspx?id="+Eval("id")+"&Type=2&status="+Eval("status")+"" %>'
                                                                                                                runat="server">
                                                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/iconset-b-edit.gif" AlternateText="Print"
                                                                                                                    Width="10px" />
                                                                                                            </asp:HyperLink>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Employee Id" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left"
                                                                                                        ItemStyle-VerticalAlign="Bottom">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("Employee_id")%>
                                                                                                          <%--  <asp:Image ID="Image1" runat="server" ImageUrl="~/images/new_blinking.gif" />--%>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                                                    <asp:BoundField DataField="status" HeaderText="Employee status" />
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("status")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                <PagerTemplate>
                                                                                                    <asp:ImageButton ID="btnFirst" runat="server" Height="15px" ImageUrl="~/images/pager_first.png"
                                                                                                        CommandArgument="First" CommandName="Page" OnCommand="btnFirst_Command" />&nbsp;
                                                                                                    <asp:ImageButton ID="btnPrev" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_left.png"
                                                                                                        CommandArgument="Prev" OnCommand="btnPrev_Command" />
                                                                                                    <asp:Label ID="lblpages" runat="server" Text="" Height="15px" CssClass="item item-char"></asp:Label>
                                                                                                    of
                                                                                                    <asp:Label ID="lblCurrent" runat="server" Text="Label" Height="15px" CssClass="item item-char"></asp:Label>
                                                                                                    <asp:ImageButton ID="btnNext" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_right.png"
                                                                                                        CommandArgument="Next" OnCommand="btnNext_Command" />&nbsp;
                                                                                                    <asp:ImageButton ID="btnLast" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_last.png"
                                                                                                        CommandArgument="Last" OnCommand="btnLast_Command" />
                                                                                                </PagerTemplate>
                                                                                            </asp:GridView>
                                                                                        </table>
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
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
