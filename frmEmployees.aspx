<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmEmployees.aspx.cs"
    Inherits="Admin_frmEmployees" EnableEventValidation="false" Title="Employees - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/HRVerticalMenu.ascx" TagName="Menu" TagPrefix="HRMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/pager.css" />

    <script language="javascript">

     function report(addr)
    {   
    window.open(addr,'Report','width=900,height=500,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');
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
                            <asp:Label ID="Label1" runat="server" Text="Add" CssClass="eslbl"></asp:Label>&nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">New</asp:LinkButton>&nbsp;<asp:Label
                                ID="Label2" runat="server" Text="Employee" CssClass="eslbl"></asp:Label>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/iconset-a-help.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table>
                                        <tr class="pagerbar">
                                            <td class="pagerbar-cell" align="right">
                                                <table class="pager-table">
                                                    <tbody>
                                                        <tr>
                                                            <td class="pager-cell" style="width: 90%" valign="middle">
                                                                <div class="pager">
                                                                    <div align="right">
                                                                        <asp:Label ID="lbllimit" CssClass="item item-char" runat="server" Text="Change Limit:"></asp:Label>
                                                                        <asp:DropDownList ID="ddlpagecount" runat="server" OnSelectedIndexChanged="ddlpagecount_SelectedIndexChanged"
                                                                            AutoPostBack="true" CssClass="item item-selection">
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
                                            <td align="center">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true"
                                                    PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnDataBound="GridView1_DataBound"
                                                    GridLines="None" Width="100%" Font-Size="Small" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                                                    OnRowDeleting="GridView1_RowDeleting">
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
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" onclick="javascript:return report(this.href);" NavigateUrl='<%#"~/frmEmployeeData.aspx?id="+Eval("id")+"&type=v"%>'
                                                                    runat="server">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/view.jpg" AlternateText="Print"
                                                                        Width="20px" />
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink2" runat="server" onclick="javascript:return report(this.href);"
                                                                    NavigateUrl='<%#"~/frmEmployeeData.aspx?id="+Eval("id")%>'>
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/iconset-b-edit.gif" AlternateText="Print"
                                                                        CssClass="listImage" />
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" HeaderText="id" Visible="false" />
                                                        <asp:BoundField DataField="user_name" HeaderText="user name" Visible="false" />
                                                        <asp:BoundField DataField="first_name" HeaderText="First Name" />
                                                        <asp:BoundField DataField="middle_name" HeaderText="Middle Name" />
                                                        <asp:BoundField DataField="last_name" HeaderText="Last Name" />
                                                        <asp:BoundField DataField="date_of_joining" HeaderText="DateOfJoin" HeaderStyle-Width="100px" />
                                                        <asp:BoundField DataField="Category" HeaderText="Category" />
                                                        <asp:BoundField DataField="Department" HeaderText="Department" />
                                                        <asp:BoundField DataField="mail_id" HeaderText="Email id" />
                                                        <asp:BoundField DataField="Mobile_no" HeaderText="Mobile No" />
                                                        <asp:BoundField DataField="roles" HeaderText="Role" />
                                                        <asp:BoundField DataField="cc_code" HeaderText="CCCode" />
                                                        <asp:BoundField DataField="Employee_status" HeaderText="Status" />
                                                        <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/images/attachments-a-close.png" />
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
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
