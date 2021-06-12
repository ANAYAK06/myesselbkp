<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUpdateCashVoucher.aspx.cs"
    Inherits="Admin_frmUpdateCashVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
    function validate()
    {
        if(document.getElementById("<%=txtAmount.ClientID %>").value!='0')
        {
        alert("You Should Enter Zero");
        document.getElementById("<%=txtAmount.ClientID %>").focus();
        return false;
        
        }
    }
    
    
    </script>
</head>
<body>
    <center>
        <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        </cc1:ToolkitScriptManager>
        <table>
            <tr valign="top">
                <td align="center">
                    <table width="500px" style="border: 1px solid #000" class="estbl">
                        <tr style="border: 1px solid #000">
                            <th valign="top" colspan="4" align="center">
                                <asp:Label ID="Label1" runat="server" Text="Delete Cash Voucher" CssClass="eslbl"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="Voucher ID "></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtvoucherid" CssClass="estbox" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="label4" CssClass="eslbl" runat="server" Text="Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" onkeydown="return DateReadonly();" Enabled="false" ></asp:TextBox>
                                <img onclick="scwShow(document.getElementById('ctl00_ContentPlaceHolder1_txtdate'),this);"
                                    alt="" src="../images/cal.gif" style="width: 15px; height: 15px;" id="cldrDob1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="DCA"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddldetailhead" CssClass="esddown" onchange="SetDynamicKey('dp3',this.value);"
                                    runat="server" ToolTip="DCA Code">
                                </asp:DropDownList>
                                <span class="starSpan">*</span>
                                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="dca" TargetControlID="ddldetailhead"
                                    ServiceMethod="cash" ServicePath="~/cascadingDCA.asmx" PromptText="Select DCA">
                                </cc1:CascadingDropDown>
                                <br />
                                <asp:Label ID="lbldca" class="ajaxspan" runat="server"></asp:Label>
                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                    TargetControlID="lbldca" ClearContentsDuringUpdate="true" ServicePath="~/EsselServices.asmx"
                                    ServiceMethod="GetDCAName">
                                </cc1:DynamicPopulateExtender>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Sub DCA"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlsubdetail" CssClass="esddown" runat="server" onchange="SetDynamicKey('dp4',this.value);"
                                    ToolTip="Sub DCA">
                                </asp:DropDownList>
                                <span class="starSpan">*</span>
                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="subdca" TargetControlID="ddlsubdetail"
                                    ParentControlID="ddldetailhead" ServiceMethod="SUBDCA" ServicePath="~/cascadingDCA.asmx"
                                    PromptText="Select Sub DCA">
                                </cc1:CascadingDropDown>
                                <br />
                                <asp:Label ID="lblsubdca" class="ajaxspan" runat="server"></asp:Label>
                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender4" BehaviorID="dp4" runat="server"
                                    TargetControlID="lblsubdca" ClearContentsDuringUpdate="true" ServicePath="~/EsselServices.asmx"
                                    ServiceMethod="GetSubDCAName">
                                </cc1:DynamicPopulateExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="label3" CssClass="eslbl" runat="server" Text="Name"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtname" CssClass="estbox" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Pariculars"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtparticular" CssClass="estbox" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                              
                                <asp:Label ID="lblamoutn1" runat="server" CssClass="eslbl" Text="Amount"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtAmount" CssClass="estbox" runat="server"
                                    Width="182px"></asp:TextBox>
                                <asp:Label ID="lblcc" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="update" CssClass="esbtn" runat="server" Text="Update" OnClick="update_Click" OnClientClick="javascript:return validate()" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="reset" CssClass="esbtn" runat="server" Text="Reset" OnClick="reset_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </form>
    </center>
</body>
</html>
