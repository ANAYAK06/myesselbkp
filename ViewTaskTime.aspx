<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewTaskTime.aspx.cs" Inherits="ViewTaskTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript">
          function validate() {
              var month = document.getElementById("<%=ddlMonth.ClientID %>").value;
              var year = document.getElementById("<%=ddlyear.ClientID %>").value;
            var objs = new Array("<%=ddlnames.ClientID%>", "<%=ddlno.ClientID%>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (month != "Select Month" && year == "Select Year") {
                window.alert("Select Year");
                document.getElementById("<%=ddlyear.ClientID %>").focus();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr align="center">
            <td align="center">
                <table class="estbl" width="400px">
                    <tr>
                        <th valign="top" align="center" colspan="6">
                            <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="View Task Details"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td align="center" style="">
                            <asp:DropDownList ID="ddlnames" runat="server" ToolTip="SupportUser" Width="140px" AutoPostBack="true" 
                                CssClass="esddown" onselectedindexchanged="ddlnames_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="">
                            <asp:DropDownList ID="ddlno" runat="server" ToolTip="No" Width="140px" CssClass="esddown">
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            <span class="filter_item">
                                <asp:DropDownList ID="ddlMonth" Width="120px" CssClass="esddown" runat="server">
                                    <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="120px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnok" runat="server" Text="OK" OnClientClick="javascript:return validate();" OnClick="btnok_Click" />
                        </td>
                    </tr>
                </table>
                <table width="900px">
                    <tr align="center">
                        <td>
                            <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                                        GridLines="None" EmptyDataText="No Data Avaliable" 
                                        HorizontalAlign="Center" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" CssClass="mGrid" 
                                        onrowcreated="GridView1_RowCreated">
                                        <Columns>
                                            <asp:BoundField DataField="id" Visible="false" />
                                            <asp:TemplateField ItemStyle-Width="70" HeaderText="NO" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" Style="cursor: hand;" runat="server" Width="70" Text='<%# Bind("no") %>'></asp:Label>
                                                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="Panel1"
                                                        TargetControlID="lblno" DynamicContextKey='<%# Eval("no") %>' DynamicControlID="Panel1"
                                                        DynamicServiceMethod="GetDynamicContent" Position="Bottom">
                                                    </cc1:PopupControlExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px" />
                                            
                                            <asp:BoundField DataField="time" HeaderText="Time" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="supportuser" HeaderText="SupportUser" ItemStyle-Width="30px"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="Panel1" runat="server">
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
