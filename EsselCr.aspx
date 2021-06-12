<%@ Page Title="Add Cr/Issue" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="EsselCr.aspx.cs" Inherits="EsselCr"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=ddltype.ClientID %>", "<%=txtdesc.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
        }
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= "65") && (keyEntry <= "90")) || ((keyEntry >= "97") && (keyEntry <= "122")) || (keyEntry == "46") || (keyEntry == "32") || keyEntry == "45")
                return true;
            else {
                alert("Please Enter Only Character values.");
                return false;
            }
        }

        function deleteSpecialChar(txtdesc) {
            if (txtdesc.value != '' && txtdesc.value.match(/^[\w ]+$/) == null) 
            {
                txtdesc.value = txtdesc.value.replace(/[\W]/g, '');
            }
        }
  
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td align="center">
                <table class="estbl" width="600px">
                    <tr style="border: 1px solid #000">
                        <th valign="top" align="center">
                            <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="ESSEL CR'S"></asp:Label>
                        </th>
                    </tr>
                    <tr align="center">
                        <td>
                            <table class="estbl">
                                <tr>
                                    <td style="width:100px">
                                        <asp:Label ID="lbltype" CssClass="eslbl" runat="server" Text="CR Type"></asp:Label>
                                    </td>
                                    <td style="width:500px">
                                        <asp:DropDownList ID="ddltype" runat="server" ToolTip="Type" Width="130px" CssClass="esddown">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">CR</asp:ListItem>
                                            <asp:ListItem Value="2">ISSUE</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100px">
                                        <asp:Label ID="lbldesc" CssClass="eslbl" runat="server" Text="Description"></asp:Label>
                                    </td>
                                    <td style="width:500px;" >
                                        <asp:TextBox ID="txtdesc" Width="490px" Height="40px" runat="server" TextMode="MultiLine"   ToolTip="Description"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnsubmit" runat="server" CssClass="esbtn" Style="font-size: small"
                                            Text="Submit"  OnClientClick="javascript:return validate();" onclick="btnsubmit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <asp:Label ID="lbllabel" runat="server" Visible="false"></asp:Label>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
