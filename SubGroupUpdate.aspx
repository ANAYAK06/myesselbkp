<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="SubGroupUpdate.aspx.cs" Inherits="SubGroupUpdate" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript">
          function validate() {
              var GridView = document.getElementById("<%=gvsubgroups.ClientID %>");
              if (GridView != null) {
                  for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                      if (GridView.rows(rowCount).cells(3).children[0].value == "") {
                          window.alert("Please Insert Sub-Group Name");
                          return false;
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
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="700px">
                            <tr>
                                <td align="center">
                                    <table class="estbl" width="600px">
                                        <tr>
                                            <th valign="top" align="center">
                                                <asp:Label ID="lblheader" CssClass="esfmhead" runat="server" Text="Update Sub-Groups"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="estbl" width="600px">
                                                    <tr id="tr" runat="server">
                                                        <td align="right" style="width: 150px">
                                                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="Sub-Group"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 250px">
                                                            <asp:DropDownList ID="ddlsubgroup" CssClass="esddown" runat="server" Width="250px"
                                                                ToolTip="Sub-Group" OnSelectedIndexChanged="ddlsubgroup_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="group" TargetControlID="ddlsubgroup"
                                                                ServiceMethod="ChildGroups" ServicePath="cascadingDCA.asmx" PromptText="Select Sub-Group">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tbladdgroups" style="border: 1px solid #000" runat="server" width="610px">
                                        <tr >
                                            <td>
                                                <asp:GridView ID="gvsubgroups" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="600px" GridLines="None" DataKeyNames="id" ShowFooter="true">
                                                    <Columns>
                                                       <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:BoundField DataField="NatureGroupName"  HeaderText="Nature Group Name" />
                                                        <asp:BoundField DataField="Group_Name" HeaderText="Group Name"  />
                                                        <asp:TemplateField HeaderText="Sub-Group Name" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSName" Text='<%# Bind("name") %>' runat="server" Width="190px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="center" id="btn" runat="server">
                                            <td align="center">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="Submit" onclick="btnAssign_Click" OnClientClick="javascript:return validate();" />
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
