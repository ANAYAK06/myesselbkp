<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="HRHome.aspx.cs"
    Inherits="HRHome" Title="Untitled Page" %>

<%@ Register Src="~/HR/HRVerticalMenu.ascx" TagName="Menu" TagPrefix="HRMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script src="../Java_Script/MochiKit.js" type="text/javascript"></script>
    <script src="../Java_Script/JScript.js" type="text/javascript"></script>
    <script src="../Java_Script/accordion.js" type="text/javascript"></script>
    <style type="text/css">
        a.specialAnchor
        {
            color: black;
            text-decoration: none;
        }
        a.specialAnchor:hover
        {
            color: blue;
            text-decoration: underline;
        }
    </style>
    <script type="text/javascript" language="javascript">


        function Getstatus(status) {
            var btn1 = document.getElementById("<%=Button1.ClientID %>");
            var btn2 = document.getElementById("<%=Button2.ClientID %>");
            var btn3 = document.getElementById("<%=Button3.ClientID %>");
            var btn6 = document.getElementById("<%=Button6.ClientID %>");



            if (status == "1") {
                btn1.click();
            }
            else if (status == "2") {
                btn2.click();
            }
            else if (status == "3") {
                btn3.click();
            }
            else if (status == "4") {
                btn6.click();
            }
        }


        function Getsalstatus(status) {
            var btn4 = document.getElementById("<%=Button4.ClientID %>");
            var btn5 = document.getElementById("<%=Button5.ClientID %>");
            //            var btn3 = document.getElementById("<%=Button3.ClientID %>");



            if (status == "4") {
                btn4.click();
            }
            //            else if (status == "2") {
            //                btn5.click();
            //            }
            //            else if (status == "3") {
            //                btn3.click();
            //            }
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px">
                <HRMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table id="_terp_list_grid" width="740px" cellspacing="0" cellpadding="0">
                    <tr>
                        <td bgcolor="#CCCCCC">
                            <asp:Label ID="Label1" runat="server" Text="Employee's Registration For Approval"
                                BackColor="#CCCCCC" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" Width="100%" BorderColor="White" runat="server" AutoGenerateColumns="false"
                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" ShowFooter="false"
                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                PagerStyle-CssClass="grid pagerbar" DataKeyNames="status" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="4px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="4px" ShowHeader="false">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Bottom"
                                        ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkScrap" CommandName="Scrap" CssClass="specialAnchor" ForeColor="RoyalBlue"
                                                Font-Bold="true" runat="server" Text='<%# Bind("remarks") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="status" Visible="false" />--%>
                                    <asp:TemplateField ItemStyle-Width="1px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Bottom"
                                        ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hf1" runat="server" Value='<%#Bind("status")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <asp:Button ID="Button1" runat="server" Text="Button" PostBackUrl="~/HR/AproveEmployeeRegisterForm1.aspx"
                        Style="display: none;" />
                    <asp:Button ID="Button2" runat="server" Text="Button" PostBackUrl="~/HR/AproveEmployeeRegisterForm1.aspx"
                        Style="display: none;" />
                    <asp:Button ID="Button3" runat="server" Text="Button" PostBackUrl="~/HR/AproveEmployeeRegisterForm1.aspx"
                        Style="display: none;" />
                    <asp:Button ID="Button6" runat="server" Text="Button" PostBackUrl="~/HR/AproveEmployeeRegisterForm1.aspx"
                        Style="display: none;" />
                </table>
                <br />
                <table id="tblsalary" width="740px" cellspacing="0" cellpadding="0">
                    <tr>
                        <td bgcolor="#CCCCCC">
                            <asp:Label ID="Label2" runat="server" Text="Employee's Salary Structure For Approval"
                                BackColor="#CCCCCC" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView2" Width="100%" BorderColor="White" runat="server" AutoGenerateColumns="false"
                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" ShowFooter="false"
                                AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                PagerStyle-CssClass="grid pagerbar" DataKeyNames="status" OnRowDataBound="GridView2_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="4px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="4px" ShowHeader="false">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Bottom"
                                        ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkScrap1" CommandName="Scrap" CssClass="specialAnchor" ForeColor="RoyalBlue"
                                                Font-Bold="true" runat="server" Text='<%# Bind("remarks") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="status" Visible="false" />--%>
                                    <asp:TemplateField ItemStyle-Width="1px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Bottom"
                                        ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hf2" runat="server" Value='<%#Bind("status")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <asp:Button ID="Button4" runat="server" Text="Button" PostBackUrl="~/HR/AproveSalaryBreakup.aspx"
                        Style="display: none;" />
                    <asp:Button ID="Button5" runat="server" Text="Button" PostBackUrl="~/HR/AproveSalaryBreakup.aspx"
                        Style="display: none;" />
                    <%--<asp:Button ID="Button6" runat="server" Text="Button" PostBackUrl="~/AproveEmployeeRegisterForm1.aspx"
                        Style="display: none;" />--%>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
