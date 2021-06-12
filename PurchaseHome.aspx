<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="PurchaseHome.aspx.cs"
    Inherits="PurchaseHome" Title="Purchase Home - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    <script language="javascript">
  

function Getstatus(status)
{ 
var btn1=document.getElementById("<%=Button1.ClientID %>");  
var btn2=document.getElementById("<%=Button2.ClientID %>");  

var btn3=document.getElementById("<%=Button3.ClientID %>");  

   if(status=="1")
   {
    btn1.click(); 
   }
   else if(status=="2")
   {
     btn2.click();
   }
   else if(status=="3")
   {
     btn3.click();
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
            <td valign="top">
                <table id="_terp_list_grid" class="grid" width="740px" cellspacing="0" cellpadding="0">
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
                                    <asp:LinkButton ID="lnkScrap" CommandName="Scrap" CssClass="specialAnchor" runat="server"
                                        Text='<%# Bind("remarks") %>'></asp:LinkButton>
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
                    <asp:Button ID="Button1" runat="server" Text="Button" PostBackUrl="~/Indent.aspx"
                        Style="display: none;" />
                    <asp:Button ID="Button2" runat="server" Text="Button" PostBackUrl="~/VendorPO.aspx"
                        Style="display: none;" />
                    <asp:Button ID="Button3" runat="server" Text="Button" PostBackUrl="~/RaisePO.aspx"
                        Style="display: none;" />
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
