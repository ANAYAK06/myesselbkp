<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="UploadItems.aspx.cs"
    Inherits="UploadItems" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <%--<td>
                Vendor Id
            </td>
            <td>
                <asp:DropDownList ID="ddlvendors" runat="server" OnSelectedIndexChanged="ddlvendors_SelectedIndexChanged">
                </asp:DropDownList>
                
            </td>--%>
            <%-- <td>
                <asp:DropDownList ID="ddlcccode" CssClass="filter_item" runat="server">
                </asp:DropDownList>
                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                    ServicePath="cascadingDCA.asmx" Category="dd2" LoadingText="Please Wait" ServiceMethod="costcode"
                    PromptText="Select Cost Center">
                </cc1:CascadingDropDown>
            </td>--%>
        </tr>
        <tr>
            <td>
                Excel
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnupld" runat="server" OnClick="btnupld_Click" Text="Upload" />
            </td>
        </tr>
            <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </td>
        </tr>
            <%-- <td>
                <asp:GridView ID="GridView2" DataKeyNames="id" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSelect1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>--%>
        </tr>
        <%--<tr>
            <td>
                <asp:TextBox ID="txtamount" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <asp:Button ID="btninsert" runat="server" Text="Insert" OnClick="btninsert_Click1" />
            </td>
        </tr>
    </table>
</asp:Content>
