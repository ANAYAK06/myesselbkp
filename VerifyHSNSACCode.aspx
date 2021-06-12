<%@ Page Title="Verify HSN/SAC Code" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="VerifyHSNSACCode.aspx.cs" Inherits="VerifyHSNSACCode" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript">
        function validate() {         
                var objs = new Array("<%=txtcgstrate.ClientID %>","<%=txtsgstrate.ClientID %>","<%=txtigstrate.ClientID %>","<%=txtremarks.ClientID %>")
                if (!CheckInputs(objs)) {
                    return false;
                }           
        }
    </script>
    <script type="text/Javascript">
        function isNumberKeycgst(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = document.getElementById("<%=txtcgstrate.ClientID %>").value.length;
                var index = document.getElementById("<%=txtcgstrate.ClientID %>").value.indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
        function isNumberKeysgst(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = document.getElementById("<%=txtsgstrate.ClientID %>").value.length;
                var index = document.getElementById("<%=txtsgstrate.ClientID %>").value.indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
        function isNumberKeyigst(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = document.getElementById("<%=txtigstrate.ClientID %>").value.length;
                var index = document.getElementById("<%=txtigstrate.ClientID %>").value.indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table style="width: 750px">
                            <tr align="center">
                                <td>
                                    <table style="width: 750px;">
                                        <tr align="center">
                                            <th>
                                                <asp:Label ID="Label5" CssClass="esfmhead" runat="server" Text=" Verify HSN/SAC"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="750px" AutoGenerateColumns="false"
                                                    HorizontalAlign="Center" CssClass="grid-content" BorderColor="White" GridLines="None"
                                                    HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    RowStyle-CssClass=" grid-row char grid-row-odd" DataKeyNames="Id,HSN_SAC_Code" FooterStyle-BackColor="DarkGray"
                                                    EmptyDataText="There is no Records" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" >
                                                    <%-- --%>
                                                    <Columns>
                                                        <asp:CommandField HeaderText="Verify" ButtonType="Image" ItemStyle-Width="30px" ShowEditButton="true"
                                                            EditImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField DataField="Id" HeaderText="" Visible="false" />
                                                        <asp:BoundField DataField="CodeCategory" ItemStyle-Width="100px" HeaderText="Category" />
                                                        <asp:BoundField DataField="HSN_SAC_Code" ItemStyle-Width="100px" HeaderText="HSN/SAC Code" />
                                                        <asp:BoundField DataField="Remarks" ItemStyle-Width="100px" HeaderText="Description" />
                                                        <asp:CommandField HeaderText="Delete" ItemStyle-Width="100px" ShowDeleteButton="true" DeleteText="Delete"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="745px" id="tblcode" runat="server">
                                        <tr>
                                            <th align="center" colspan="4">Verify HSN/SAC Code
                                            </th>
                                        </tr>

                                        <tr id="Trtypecategory" runat="server">
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Type Category" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtcodecategory" runat="server" Width="200px" Enabled="false" ToolTip="Type Category"
                                                    CssClass="esddown"></asp:TextBox>

                                            </td>
                                        </tr>
                                       
                                        <tr id="Trhsncategory" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Text="HSN Category" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txthsncategory" runat="server" Width="200px" Enabled="false" ToolTip="HSN Category"
                                                    CssClass="esddown"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trhsncode" runat="server">
                                            <td style="width: 150px" align="center">
                                                <asp:Label ID="Label9" runat="server" Text="HSN/SAC Code" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="center" style="width: 400px;">
                                                <asp:TextBox ID="txthsncode" runat="server" Width="200px" Enabled="false" ToolTip="HSN/SAC Code"
                                                    CssClass="esddown"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trcgstrate" runat="server">
                                            <td style="width: 250px" align="center">
                                                <asp:Label ID="Label2" runat="server" Text="CGST Rate" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="center" style="width: 400px;">
                                                <asp:TextBox ID="txtcgstrate" runat="server" Width="200px" onkeypress="return isNumberKeycgst(event)"
                                                    ToolTip="CGST Rate" CssClass="esddown"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trsgstrate" runat="server">
                                            <td style="width: 150px" align="center">
                                                <asp:Label ID="Label1" runat="server" Text="SGST Rate" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="center" style="width: 400px;">
                                                <asp:TextBox ID="txtsgstrate" runat="server" Width="200px" ToolTip="SGST Rate" onkeypress="return isNumberKeysgst(event)"
                                                    CssClass="esddown"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trigstrate" runat="server">
                                            <td style="width: 150px" align="center">
                                                <asp:Label ID="Label6" runat="server" Text="IGST Rate" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="center" style="width: 400px;">
                                                <asp:TextBox ID="txtigstrate" runat="server" Width="200px" ToolTip="IGST Rate" onkeypress="return isNumberKeyigst(event)" CssClass="esddown"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trhsnrem" runat="server">
                                            <td style="width: 250px" align="center">
                                                <asp:Label ID="Label4" runat="server" Text="Remarks" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td valign="top" align="center" style="width: 400px;">
                                                <asp:TextBox ID="txtremarks" runat="server" Width="200px" TextMode="MultiLine" ToolTip="Remarks"
                                                    CssClass="esddown"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="btn" runat="server">
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAdd" runat="server" Text="Verify" OnClientClick="javascript:return validate();"
                                                    CssClass="esbtn" OnClick="btnAdd_Click" />
                                                <%----%>
                                                
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

