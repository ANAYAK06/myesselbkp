<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="frmUpdateDCABudget.aspx.cs"  Inherits="Admin_frmUpdateDCABudget" Title="Update DCA Budget - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
    function dcabudget()
    {
        if(document.getElementById("<%=ddlCCcode.ClientID %>").selectedIndex!=0&&document.getElementById("<%=ddlDCAcode.ClientID %>").selectedIndex!=0&&document.getElementById("<%=ddlyear.ClientID %>").selectedIndex!=0)
        {
        var argu=document.getElementById("<%=ddlCCcode.ClientID %>").value+"|"+document.getElementById("<%=ddlDCAcode.ClientID %>").value+"|"+document.getElementById("<%=ddlyear.ClientID %>").value;
        SetDynamicKey('dp5',argu);
        }
        else
        {
        document.getElementById("<%=lbldcabud.ClientID %>").innerHTML="";
        }
    }
    function ccbudget()
    {
        if(document.getElementById("<%=ddlCCcode.ClientID %>").selectedIndex!=0&&document.getElementById("<%=ddlyear.ClientID %>").selectedIndex!=0)
        {
            var argu=document.getElementById("<%=ddlCCcode.ClientID %>").value+"|"+document.getElementById("<%=ddlyear.ClientID %>").value;
            SetDynamicKey('dp2',argu);
            dcabudget();
        }
        else
        {
            document.getElementById("<%=lblccbud.ClientID %>").innerHTML="";
        }
    }
    
    function validate()
    {
        if(!ChceckRBL("<%=rbtntype.ClientID %>"))
            return false;
            
        var objs=new Array("<%=ddlyear.ClientID %>","<%=ddlCCcode.ClientID %>","<%=ddlDCAcode.ClientID %>","<%=txtBudget.ClientID %>");        
        if(!CheckInputs(objs))
        {
        return false;
        }
    }
       function change()
    {
        var rbtn=document.getElementById("<%=rbtntype.ClientID %>");
        var models = rbtn.getElementsByTagName("input");
        for (var x = 0; x < models.length; x ++)
        {
        if (models[0].checked) 
        {
        document.getElementById("<%=btnAssign.ClientID %>").value="Add";
        }
        else 
        {
        document.getElementById("<%=btnAssign.ClientID %>").value="Subtract";
        } 
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
                    <tr valign="top">
                        <td align="center">
                            <table class="estbl" width="400px">
                                <tr style="border: 1px solid #000">
                                    <th valign="top" colspan="2" align="center">
                                        <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Update DCA Budget"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:RadioButtonList ID="rbtntype" ToolTip="Add or Subtract" CssClass="esrbtn" runat="server"
                                            CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" onclick="change();">
                                            <asp:ListItem Value="Add">Add Budget</asp:ListItem>
                                            <asp:ListItem Value="Subtract">Subtract Budget</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 122px">
                                        <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Year"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlyear" CssClass="esddown" Width="105px" runat="server" ToolTip="Year"
                                            onchange="ccbudget();">
                                        </asp:DropDownList>
                                        <span class="starSpan">*</span>
                                        <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlyear"
                                            ServicePath="EsselServices.asmx" LoadingText="Please Wait" Category="Year" ServiceMethod="FinancialYear"
                                            PromptText="Select Year">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="center" style="width: 122px">
                                        <asp:Label ID="Label1" CssClass="eslbl" runat="server" Text="Cost Center"></asp:Label>
                                    </td>
                                    <br />
                                    <td valign="top" align="left">
                                        <asp:DropDownList ID="ddlCCcode" CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);ccbudget();"
                                            runat="server" ToolTip="Cost Center">
                                        </asp:DropDownList>
                                        <span class="starSpan">*</span><asp:Label ID="lblcc" class="ajaxspan" runat="server"></asp:Label>
                                        <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlCCcode"
                                            ParentControlID="ddlyear" ServicePath="EsselServices.asmx" Category="costcenter"
                                            LoadingText="Please Wait" ServiceMethod="BudgetCC" PromptText="Select Cost Center">
                                        </cc1:CascadingDropDown>
                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender1" BehaviorID="dp1" runat="server"
                                            TargetControlID="lblcc" ClearContentsDuringUpdate="true" ServicePath="~/EsselServices.asmx"
                                            ServiceMethod="GetCCName">
                                        </cc1:DynamicPopulateExtender>
                                        <br />
                                        <asp:Label ID="lblccbud" runat="server" class="ajaxspan"></asp:Label>
                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender2" BehaviorID="dp2" runat="server"
                                            TargetControlID="lblccbud" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                            ServiceMethod="GetCCBudget">
                                        </cc1:DynamicPopulateExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 122px">
                                        <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="DCA Code"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlDCAcode" CssClass="esddown" onchange="SetDynamicKey('dp3',this.value);dcabudget();"
                                            runat="server" ToolTip="DCA">
                                        </asp:DropDownList>
                                        <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlDCAcode"
                                            ServicePath="EsselServices.asmx" Category="DCA" LoadingText="please Wait" ServiceMethod="BudgetDCA"
                                            PromptText="Select DCA" ParentControlID="ddlCCcode">
                                        </cc1:CascadingDropDown>
                                        <span class="starSpan">*</span>
                                        <asp:Label ID="lbldca" class="ajaxspan" runat="server"></asp:Label>
                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                            TargetControlID="lbldca" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                            ServiceMethod="GetDCAName">
                                        </cc1:DynamicPopulateExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px;" align="center">
                                        <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="DCA Budget Yearly"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBudget" CssClass="estbox" runat="server" Width="176px" ToolTip="Budget"></asp:TextBox><span
                                            class="starSpan">*</span><br />
                                        <asp:Label ID="lbldcabud" class="ajaxspan" runat="server"></asp:Label>
                                        <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender5" BehaviorID="dp5" runat="server"
                                            TargetControlID="lbldcabud" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                            ServiceMethod="GetDCABudget">
                                        </cc1:DynamicPopulateExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Text="Update Budget" OnClientClick="javascript:return validate();"
                                            OnClick="btnAssign_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server"
                                                CssClass="esbtn" Text="Reset" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblAlert" CssClass="eslblalert" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
