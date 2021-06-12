<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="AccountCreation.aspx.cs"
    Inherits="AccountCreation" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/ToolsVerticalMenu.ascx" TagName="Menu" TagPrefix="Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript">
        function Enable() {
            var role = document.getElementById("<%=ddlrole.ClientID %>").value;
            var cc = document.getElementById("<%=ddlcccode.ClientID %>");
            if (role != "Accountant") {
                cc.disabled = true;
            }
        }
    </script>

    <script language="javascript">

        function checkpassword() {
             
            //var objs = new Array("<%=ddlemployeesid.ClientID %>", "<%=txtloginid.ClientID %>");
            var txtpass1 = document.getElementById("<%=txtpassword.ClientID %>").value;
            var txtpass1ctrl = document.getElementById("<%=txtpassword.ClientID %>");
            var txtpass2 = document.getElementById("<%=txtcpassword.ClientID %>").value;
            var txtpass2ctrl = document.getElementById("<%=txtcpassword.ClientID %>");
            var GridView=document.getElementById("<%=GridView1.ClientID %>");
            var objs1 = new Array("<%=ddlrole.ClientID %>","<%=ddlcccode.ClientID %>");
            
//            if (!CheckInputs(objs)) 
//            {
//                  return false;
//            }
            
            if(txtpass1=="")
            {
                window.alert("Enter password");
                txtpass1ctrl.focus();
                return false;
            }
            if(txtpass2=="")
            {
                window.alert("Enter confirm password");
                txtpass2ctrl.focus();
                
                return false;
            }
            if (txtpass1 != txtpass2) {
                window.alert("confirm Password Not Match");
                txtpass2ctrl.focus();
                return false;
            }
            if (!CheckInputs(objs1)) 
            {
                  return false;
            }
             
//            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
//                if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
//                
//                }
//                }
        }
    
    
    </script>

    <script language="javascript">
    
        function popvalidate() {
            var newpass = document.getElementById("<%=txtnewpassword.ClientID %>").value;
            var newpassctrl = document.getElementById("<%=txtnewpassword.ClientID %>");
            var confirm = document.getElementById("<%=txtmdlconfirmpass.ClientID %>").value;
            var confirmpassctrl = document.getElementById("<%=txtmdlconfirmpass.ClientID %>");
            if (newpass == "") {
                window.alert("Enter New Password");
                newpassctrl.focus();
                return false;
            }
            else if (confirm != newpass) {
            window.alert("confirm Password Not Match");
            confirmpassctrl.focus();
            return false;
            
            }
        }

    </script>

    <script type="text/javascript">
        function closepopup() {
            $find('mdledit').hide();


        }
    </script>

    <style type="text/css">
        Editbutton
        {
            font-weight: bold;
            font-size: 10px;
            text-decoration: none;
            color: inherit;
            padding-left: 250px;
        }
    </style>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <Tools:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table style="border-bottom-style: none">
                    <tr>
                        <td>
                            <table style="border-bottom-style: none">
                                <tr>
                                    <td width="100%">
                                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>--%>
                                        <div style="height: 700px; padding-top: 100px; padding-left: 150px;" align="left">
                                            <div>
                                                <div class="notebook-pages">
                                                    <div class="notebook-page notebook-page-active">
                                                        <table style="border-bottom-style: none" align="center" width="400px">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="top" class=" item-group" width="100%">
                                                                        <table style="border-bottom-style: none" width="100%">
                                                                            <tr>
                                                                                <th colspan="2" align="right">
                                                                                    <asp:LinkButton ID="lbtnedit" CssClass="Editbutton" Text="[Reset Password]" runat="server"></asp:LinkButton>
                                                                                </th>
                                                                            </tr>
                                                                            <tr runat="server" id="trcustomerid">
                                                                                <td class="label" width="1%">
                                                                                    <label for="ssnid" class="help">
                                                                                        Employee Name
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td class="item item-char" valign="middle">
                                                                                    <asp:DropDownList ID="ddlemployeesid" runat="server" TabIndex="1" CssClass="selection selection_search readonlyfield"
                                                                                        ToolTip="Employee Id">
                                                                                    </asp:DropDownList>
                                                                                    <cc1:CascadingDropDown ID="CascadingCCode" runat="server" TargetControlID="ddlemployeesid"
                                                                                        ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="Deactiveemployees"
                                                                                        PromptText=" --Select-- ">
                                                                                    </cc1:CascadingDropDown>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label for="passport_id">
                                                                                        Login Id
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td class="item item-char" valign="middle">
                                                                                    <asp:TextBox ID="txtloginid" runat="server" CssClass="char" TabIndex="2" ToolTip="Login Id"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label for="passport_id">
                                                                                        Password
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td class="item item-char" valign="middle">
                                                                                    <asp:TextBox ID="txtpassword" runat="server" CssClass="char" TabIndex="3" ToolTip="Password"
                                                                                        TextMode="Password"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label for="passport_id">
                                                                                        Confirm Password
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td class="item item-char" valign="middle">
                                                                                    <asp:TextBox ID="txtcpassword" runat="server" CssClass="char" TabIndex="4" ToolTip="Confirm Password"
                                                                                        TextMode="Password"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label" width="1%">
                                                                                    <label for="passport_id">
                                                                                        Role
                                                                                    </label>
                                                                                    :
                                                                                </td>
                                                                                <td class="item item-char" valign="middle">
                                                                                    <asp:DropDownList ID="ddlrole" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                        ToolTip="Role" OnSelectedIndexChanged="ddlrole_SelectedIndexChanged" AutoPostBack="true">
                                                                                    </asp:DropDownList>
                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlrole"
                                                                                        ServiceMethod="role" ServicePath="cascadingDCA.asmx" Category="cc" PromptText="Select Role">
                                                                                    </cc1:CascadingDropDown>
                                                                                </td>
                                                                            </tr>
                                                                            <tr >
                                                                                <td colspan="2" align="center">
                                                                                    <div class="box-a list-a">
                                                                                        <div class="inner">
                                                                                            <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td class="grid-content">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <table id="_terp_list_grid" class="grid" width="100%" align="center">
                                                                                                                        <tr id="cc" runat="server">
                                                                                                                            <td class="label" width="1%">
                                                                                                                                <label for="passport_id">
                                                                                                                                    CC Code
                                                                                                                                </label>
                                                                                                                                :
                                                                                                                            </td>
                                                                                                                            <td class="item item-char" valign="middle">
                                                                                                                                <asp:DropDownList ID="ddlcccode" runat="server" CssClass="selection selection_search readonlyfield"
                                                                                                                                    ToolTip="CC Code">
                                                                                                                                </asp:DropDownList>
                                                                                                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlcccode"
                                                                                                                                    ServiceMethod="WebCC" ServicePath="cascadingDCA.asmx" Category="cc">
                                                                                                                                </cc1:CascadingDropDown>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr id="grid" runat="server">
                                                                                                                            <td>
                                                                                                                                <asp:GridView ID="GridView1" BorderColor="White" runat="server" AutoGenerateColumns="false"
                                                                                                                                    CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                                                    RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                                                    OnRowDataBound="GridView1_RowDataBound" DataKeyNames="cc_code">
                                                                                                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                                                    <Columns>
                                                                                                                                        <asp:TemplateField>
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:BoundField DataField="cc_code" HeaderText="CC Code" ItemStyle-HorizontalAlign="Center"
                                                                                                                                            ItemStyle-Width="200px" />
                                                                                                                                        <asp:BoundField DataField="cc_name" HeaderText="CC Name" ItemStyle-Width="200px" />
                                                                                                                                        <asp:BoundField DataField="Name" HeaderText="PM Name" ItemStyle-Width="200px" />
                                                                                                                                    </Columns>
                                                                                                                                </asp:GridView>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </ContentTemplate>
                                                                                                                <Triggers>
                                                                                                                    <asp:AsyncPostBackTrigger ControlID="ddlrole" EventName="SelectedIndexChanged" />
                                                                                                                </Triggers>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" valign="middle" style="height: 35px" align="center">
                                                                                    <table border="0">
                                                                                        <tbody>
                                                                                            <tr align="center">
                                                                                                <td align="left">
                                                                                                    <asp:Button ID="btnsubmit" BackColor="Gray" runat="server" Width="80px" CssClass="button"
                                                                                                        Text="Submit" OnClick="btnsubmit_Click" OnClientClick="javascript:return checkpassword();" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                    <asp:Button ID="btnreset" BackColor="Gray" runat="server" Width="80px" CssClass="button"
                                                                                                        Text="Reset" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <cc1:ModalPopupExtender ID="popedit" BehaviorID="mdledit" runat="server" TargetControlID="lbtnedit"
                                                            PopupControlID="pnledit" BackgroundCssClass="modalBackground1" />
                                                        <asp:Panel ID="pnledit" runat="server" Style="display: none;">
                                                            <table width="420px" border="0" align="center" cellpadding="0" cellspacing="0" id="tblbank"
                                                                runat="server">
                                                                <tr>
                                                                    <td width="13" valign="bottom">
                                                                        <img src="images/leftc.jpg">
                                                                    </td>
                                                                    <td class="pop_head" align="left" id="Td2" runat="server">
                                                                        <div class="popclose">
                                                                            <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                                                                        </div>
                                                                    </td>
                                                                    <td width="13" valign="bottom">
                                                                        <img src="images/rightc.jpg">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td height="180" valign="top" class="popcontent">
                                                                        <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                            height: 150px;">
                                                                            <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
                                                                                        <ProgressTemplate>
                                                                                            <div>
                                                                                                <img src="images/load.gif" /></div>
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                                    <table style="vertical-align: middle;" align="center">
                                                                                        <tr runat="server" id="tr2">
                                                                                            <td class="label" width="1%">
                                                                                                <label for="ssnid" class="help">
                                                                                                    Employee Name
                                                                                                </label>
                                                                                                :
                                                                                            </td>
                                                                                            <td class="item item-char" valign="middle">
                                                                                                <asp:DropDownList ID="ddlmdlemployee" runat="server" TabIndex="1" CssClass="selection selection_search readonlyfield"
                                                                                                    ToolTip="Employee Id">
                                                                                                </asp:DropDownList>
                                                                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ddlmdlemployee"
                                                                                                    ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="Activeemployees"
                                                                                                    PromptText=" --Select-- ">
                                                                                                </cc1:CascadingDropDown>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr runat="server" id="tr1">
                                                                                            <td class="label" width="1%">
                                                                                                <label for="ssnid" class="help">
                                                                                                    New Password
                                                                                                </label>
                                                                                                :
                                                                                            </td>
                                                                                            <td width="31%" align="center" class="item item-char">
                                                                                                <asp:TextBox ID="txtnewpassword" runat="server" CssClass="char" TextMode="Password"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr runat="server" id="tr3">
                                                                                            <td class="label" width="1%">
                                                                                                <label for="ssnid" class="help">
                                                                                                    Confirm Password
                                                                                                </label>
                                                                                                :
                                                                                            </td>
                                                                                            <td width="31%" align="center" class="item item-char">
                                                                                                <asp:TextBox ID="txtmdlconfirmpass" runat="server" CssClass="char" TextMode="Password"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" colspan="2">
                                                                                                <asp:Button ID="btnupdate" runat="server" Text="Reset" CssClass="button" OnClientClick="javascript:return popvalidate();"
                                                                                                    OnClick="btnupdate_Click" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td bgcolor="#FFFFFF">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
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
