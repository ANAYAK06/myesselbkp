<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="frmDelete.aspx.cs" EnableEventValidation="false" Inherits="Admin_frmDelete" Title="Invoice Rollback - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
    <table border="solid 1px #000">
        <tr>
          <td>
             <table class="estbl" style="width:900px">
                <tr style="border:1px solid #000">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="CC Code" CssClass="eslbl"></asp:Label>   
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="105px"
                                CssClass="esddown">
                            </asp:DropDownList>
                            <span class="starSpan">*</span>
                            <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait"
                                ServiceMethod="CC" PromptText="Select Cost Center">
                            </cc1:CascadingDropDown>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblpono" CssClass="eslbl" runat="server" Text="PO NO"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlpo" CssClass="esddown" Width="105px" runat="server" ToolTip="PO">
                            </asp:DropDownList>
                            <span class="starSpan">*</span>
                            <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="ddd" TargetControlID="ddlpo"
                                ServiceMethod="PoNo" ParentControlID="ddlcccode" ServicePath="cascadingDCA.asmx"
                                PromptText="Select PO">
                            </cc1:CascadingDropDown>
                        </td>
                        <td>
                            <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="yearly"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="PO"
                                OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <span class="starSpan">*</span>
                        </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
               
      
        <tr><td colspan="6" align="center">
            <asp:GridView ID="mgrid" runat="server" AutoGenerateColumns="true" AllowPaging="True"
                GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                ShowFooter="True" Width="400px" EmptyDataText="No Transactions Occured" 
                DataKeyNames="InvoiceNo" onpageindexchanging="mgrid_PageIndexChanging" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                        <%-- <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAll_CheckedChanged" onclick="javascript:SelectAllCheckboxes(this);"
                                runat="server" />
                        </HeaderTemplate>--%>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr"></PagerStyle>
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            </asp:GridView>
           </td>
        </tr>
        <tr>
        <td>
        <table class="estbl" style="width:900px"><tr style="border:1px solid #000">
        <td align="center" colspan="6">
            <asp:Button ID="btnupdate" runat="server" Text="Delete" CssClass="esbtn" OnClick="btnupdate_Click" />
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
