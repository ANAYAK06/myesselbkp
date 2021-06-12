<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AddTaxNos.aspx.cs"
    Inherits="AddTaxNos" Title="Add Vat/Tin/Sales Tax Number  - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function validate() {
            var vat = document.getElementById("<%=txtvat.ClientID %>").value;
         
            var stax = document.getElementById("<%=txtstax.ClientID %>").value;
            var ddlstate = document.getElementById("<%=ddlstate.ClientID %>").value;
            var area = document.getElementById("<%=txtarea.ClientID %>").value;
            var role = document.getElementById("<%=hfrole.ClientID %>").value;

            if (role == "Sr.Accountant") {
                var ddltax = document.getElementById("<%=ddlCCcode.ClientID %>").value;
                if (ddltax == "Select Cost Center") {
                    window.alert("Select Cost Center");
                    document.getElementById("<%=ddlCCcode.ClientID %>").focus();
                    return false;
                }
            }
            if (vat == "" && stax == "") {
                window.alert("Enter VAT/TIN/STAX No");
                return false;
            }
            else if (ddlstate == "Select State") {
                window.alert("Select State");
                document.getElementById("<%=ddlstate.ClientID %>").focus();
                return false;
            }

            else if (area == "") {
                window.alert("Enter Area");
                document.getElementById("<%=txtarea.ClientID %>").focus();
                return false;
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
                <table style="width: 700px">
                    <tr align="center">
                        <td>
                            <table class="estbl" width="370px">
                                <tr style="border: 1px solid #000" >
                                <asp:HiddenField ID="hfrole" runat="server" />
                                    <th valign="top" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="" CssClass="eslbl"></asp:Label>
                                    </th>
                                </tr>
                                <tr id="trgrid" runat="server" width="370px">
                                    <td align="center">
                                        <asp:GridView ID="gridupdate" runat="server" Width="370px" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                            AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                            PagerStyle-CssClass="grid pagerbar" BorderColor="Black" EmptyDataText="No Records to verify" RowStyle-CssClass=" grid-row char grid-row-odd"
                                            DataKeyNames="Id" onselectedindexchanged="gridupdate_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true"  ItemStyle-Width="15px"
                                                    SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                <asp:BoundField DataField="Id"  Visible="false" />
                                                <asp:BoundField DataField="cc_code"  HeaderText="CC Code" />
                                                <asp:BoundField DataField="vat"  HeaderText="Vat No" />
                                                
                                                <asp:BoundField DataField="Excise"  HeaderText="Sales Tax No" />
                                            </Columns>
                                            <RowStyle CssClass=" grid-row char grid-row-odd" />
                                            <PagerStyle CssClass="grid pagerbar" />
                                            <HeaderStyle CssClass="grid-header" />
                                            <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr valign="top" id="trtable" runat="server">
                                    
                                    <td align="center">
                                        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="estbl" width="370px">
                                                    <tr id="traddcc" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label9" runat="server" Text="CC Code" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 420px;">
                                                            <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" Width="200px"
                                                                CssClass="esddown">
                                                            </asp:DropDownList>
                                                            <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlCCcode"
                                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="TAXCC">
                                                            </cc1:CascadingDropDown>
                                                        </td>
                                                    </tr>
                                                    <tr id="trupdatecc" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label4" runat="server" Text="CC Code" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 420px;">
                                                            <asp:TextBox ID="txtcccode" runat="server" Width="200px" Enabled="false" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="dcacode" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label2" runat="server" Text="VAT/TIN" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 420px;">
                                                            <asp:TextBox ID="txtvat" runat="server" Width="200px" ToolTip="VAT" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr id="Tr2" runat="server">
                                                        <td style="width: 150px" align="center">
                                                            <asp:Label ID="Label6" runat="server" Text="Excise" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="center" style="width: 420px;">
                                                            <asp:TextBox ID="txtstax" runat="server" Width="200px" ToolTip="Excise" CssClass="esddown"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="sdca" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="State" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlstate" Width="200px" ToolTip="State" runat="server">
                                                                <asp:ListItem>Select State</asp:ListItem>
                                                                <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                                <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                                <asp:ListItem>Assam</asp:ListItem>
                                                                <asp:ListItem>Bihar</asp:ListItem>
                                                                <asp:ListItem>Chhatisgarh</asp:ListItem>
                                                                <asp:ListItem>Goa</asp:ListItem>
                                                                <asp:ListItem>Gujarat</asp:ListItem>
                                                                <asp:ListItem>Haryana</asp:ListItem>
                                                                <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                                <asp:ListItem>Jammu & Kashmir</asp:ListItem>
                                                                <asp:ListItem>Jharkhand</asp:ListItem>
                                                                <asp:ListItem>Karnataka</asp:ListItem>
                                                                <asp:ListItem>Kerala</asp:ListItem>
                                                                <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                                <asp:ListItem>Maharashtra</asp:ListItem>
                                                                <asp:ListItem>Manipur</asp:ListItem>
                                                                <asp:ListItem>Meghalaya</asp:ListItem>
                                                                <asp:ListItem>Mizoram</asp:ListItem>
                                                                <asp:ListItem>Nagaland</asp:ListItem>
                                                                <asp:ListItem>Orissa</asp:ListItem>
                                                                <asp:ListItem>Punjab</asp:ListItem>
                                                                <asp:ListItem>Rajasthan</asp:ListItem>
                                                                <asp:ListItem>Sikkim</asp:ListItem>
                                                                <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                                <asp:ListItem>Tripura</asp:ListItem>
                                                                <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                                <asp:ListItem>Uttaranchal</asp:ListItem>
                                                                <asp:ListItem>West Bengal</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="sdname" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Area" CssClass="eslbl"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtarea" runat="server" Width="200px" ToolTip="Area Name" CssClass="esddown"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="btn" runat="server">
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnAdd" runat="server" Text="" CssClass="esbtn" OnClick="btnAdd_Click"
                                                                OnClientClick="javascript:return validate();" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="" CssClass="esbtn" OnClick="btnCancel_Click" />
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
            </td>
        </tr>
    </table>
</asp:Content>
