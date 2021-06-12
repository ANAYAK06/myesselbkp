<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmviewbankbook.aspx.cs"
    Inherits="frmviewbankbook" EnableEventValidation="false" Title="View Bank Book - Essel Project Pvt.Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validate() {

            var objs = new Array("<%=txtminbal.ClientID %>");

            if (!CheckInputs(objs)) {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top" id="bankbook" runat="server">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table class="estbl" width="500px" id="tbldetails" runat="server" >
                    <tr style="border: 1px solid #000">
                        <th valign="top" colspan="4" align="center">
                            <asp:Label ID="Label7" runat="server" Text="" CssClass="eslbl"></asp:Label>
                        </th>
                    </tr>
                    <tr style="height: 20px" id="trtxtbankname" runat="server">
                        <td style="width: 100px">
                            <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Bank Name"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblbankname" runat="server" CssClass="eslbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="A/C Holder Name"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblacctholder" CssClass="eslbl" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="A/C No"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblacctno" CssClass="eslbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="A/C Opening Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblacopeningdate" runat="server" CssClass="eslbl" Text="A/C Opening Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblminbalance" runat="server" CssClass="eslbl" Text="Minimum Balance"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtminbal" runat="server" ToolTip="Minimum Balance" CssClass="estbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="Location"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:Label ID="lbllocation" runat="server" CssClass="eslbl" Text="Location"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="update" runat="server" Text="Update" OnClientClick="javascript:return validate()"
                                CssClass="esbtn" onclick="update_Click" />
                        </td>
                    </tr>
                    <tr id="trnotice" runat="server">
                        <td colspan="4">
                            <asp:Label ID="lblnotification" runat="server" CssClass="eslbl" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                runat="server" AutoGenerateColumns="false" Width="700px" AllowPaging="false"
                                OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" OnRowEditing="GridView1_RowEditing" DataKeyNames="bank_id">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowEditButton="true" ItemStyle-Width="15px"
                                        EditImageUrl="~/images/iconset-b-edit.gif" />
                                    <asp:BoundField HeaderText="Bank Name" DataField="bank_name" />
                                    <asp:BoundField HeaderText="A/C Holder Name" DataField="accholder_name" />
                                    <asp:BoundField HeaderText="A/C No" DataField="acc_no" />
                                    <asp:BoundField HeaderText="A/C Opening Date" DataField="date" />
                                    <asp:BoundField HeaderText="Minimum Balance" DataField="minimum_balance" />
                                    <asp:BoundField HeaderText="Bank Location" DataField="bank_location" />
                                    <%-- <asp:BoundField HeaderText="Current Balance" DataField="balance" />--%>
                                    <asp:TemplateField HeaderText="Current Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal1" runat="server" Text='<%#Bind("balance") %>' ForeColor='<%# Convert.ToDecimal(Eval("balance"))>-1?System.Drawing.Color.Black:System.Drawing.Color.Red %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                OnClick="btnExcel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
