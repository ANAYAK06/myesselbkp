<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ITDepPercentage.aspx.cs" Inherits="ITDepPercentage" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script language="javascript">
    function validate() {
        GridView = document.getElementById("<%=GridView1.ClientID %>");

        

        for (var rowCount = 1; rowCount < GridView.rows.length; rowCount++) {
         
             if (GridView.rows(rowCount).cells(3).children[0].value == "") {
                window.alert("Please Enter Percentage");
                GridView.rows(rowCount).cells(3).children[0].focus();
                return false;
            }
           
        }
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr id="Tr1" runat="server">
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td style="width: 90%" valign="top">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnl" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <h1>
                            Company Depreciation<a class="help" href="/view_diagram/process?res_model=hr.employee&amp;res_id=False&amp;title=Employees"
                                title="Corporate Intelligence..."> <small>Help</small></a></h1>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="Financial Year:" Font-Bold="True"
                                        Font-Size="Small"></asp:Label>
                                    <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" Width="50%" AutoGenerateColumns="false"
                                        HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" HeaderStyle-CssClass="grid-header"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        PagerStyle-CssClass="grid pagerbar" FooterStyle-BackColor="DarkGray" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Serial No." ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SubDCA" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="subdca" runat="server" Text='<%# Bind("Subdca_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="Desc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Percentage" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="percentage" runat="server" Text='<%# Bind("Percentage") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="previousyearpercentage" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                                                HeaderText="previous year percentage" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSave" Height="18px" runat="server" Text="Submit" CssClass="button"
                                        OnClientClick="javascript:return validate() " OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
