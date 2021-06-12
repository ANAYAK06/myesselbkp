<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ITSMRegister.aspx.cs" Inherits="ITSMRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../js/JScript.js" type="text/javascript"></script>

    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         function validate() {
             var objs = new Array("<%=ddlusertype.ClientID %>", "<%=txtUsername.ClientID %>", "<%=txtPwd.ClientID %>", "<%=txtCPwd.ClientID %>", "<%=txtEmail.ClientID %>", "<%=txtphno.ClientID %>");
             if (!CheckInputs(objs)) {
                 return false;
             }
             var psw = document.getElementById("<%=txtPwd.ClientID %>").value;
             var cpsw = document.getElementById("<%=txtCPwd.ClientID %>").value;
             if (psw != cpsw) {
                 alert("Confirm password is not matching");
                 document.getElementById("<%=txtCPwd.ClientID %>").value = "";
                 document.getElementById("<%=txtCPwd.ClientID %>").focus();
                 return false;
             }
             var emailid = document.getElementById("<%=txtEmail.ClientID %>").value;
             if (emailid == "") {
                 window.alert("Enter E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             var atPos = emailid.indexOf('@', 0);
             //CHECKS THAT THERE IS AN '@' CHARACTER IN THE STRING 
             if (atPos == -1) {
                 alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             //CHECKS THAT THERE IS AT LEAST ONE CHARACTER BEFORE THE '@' CHARACTER 
             if (atPos == 0) {
                 window.alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             //CHECKS ATLEAST ONE CHAR BEFORE '.'
             var dot = emailid.indexOf('.', 0);
             if (dot == 0) {
                 window.alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             //CHECKS THAT THERE IS a PERIOD IN THE DOMAIN NAME 
             if (emailid.indexOf('.', atPos) == -1) {
                 alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             //CHECKS THAT THERE IS ONLY ONE '@' IN THE STRING 
             if (emailid.indexOf('@', atPos + 1) != -1) {
                 window.alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             //CHECKS THAT THERE IS NO PERIOD IMMEDIATELY AFTER '@' i
             if (emailid.indexOf('@.', 0) != -1) {
                 window.alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             //CHECKS THAT THERE IS NO PEROID IMMEDIATELY BEFORE '@' 
             if (emailid.indexOf('.@', 0) != -1) {
                 window.alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             //CHECKS THAT TWO PERIODS MUST NOT BE ADJACENT 
             if (emailid.indexOf('..', 0) != -1) {
                 window.alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
             //CHECKS THAT THERE NO SPECIAL CHARACTERS ENTERED 
             var invalidChars = '\/\'\\ ";:?!()[]\{\}^|#*$%&';
             for (i = 0; i < invalidChars.length; i++) {
                 if (emailid.indexOf(invalidChars.charAt(i), 0) != -1) {
                     window.alert("Invalid E-mail ID");
                     document.getElementById("<%=txtEmail.ClientID %>").focus();
                     return false;
                 }
             }
             //CHECKS THAT THERE IS ATLEAST ONE CHARACTER AFTER THE LAST PERIOD 
             var suffix = emailid.substring(emailid.lastIndexOf('.') + 1);
             if (suffix.length < 1) {
                 window.alert("Invalid E-mail ID");
                 document.getElementById("<%=txtEmail.ClientID %>").focus();
                 return false;
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="scriptmanagerproxy1" runat="server">
    </asp:ScriptManagerProxy>
    <table style="width: 900px">
        <tr valign="top">
            <td align="center">
                <table class="estbl" width="300px">
                    <tr style="border: 1px solid #000">
                        <th valign="top" colspan="2" align="center">
                            <asp:Label ID="lblRegister" runat="server" Text="Registration" CssClass="esfmhead"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="User Type">
                            </asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlusertype" runat="server" Width="120px" ToolTip="User Type" CssClass="esddown">
                            </asp:DropDownList>
                            <span class="starSpan">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            <asp:Label ID="lblUsername" runat="server" CssClass="eslbl" Text="User Name" AssociatedControlID="txtUsername"></asp:Label>
                        </td>
                        <td style="width: 250px" align="left">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="estbox" ToolTip="UserName" MaxLength="50"></asp:TextBox><span
                                class="starSpan">* </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPwd" CssClass="eslbl" runat="server" Text="Password"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPwd" runat="server" CssClass="estbox" ToolTip="Password" TextMode="Password" MaxLength="20"></asp:TextBox><span
                                class="starSpan">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCPwd" runat="server" CssClass="eslbl" Text="Confirm Password">
                            </asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCPwd" CssClass="estbox" runat="server" ToolTip="Confirm Password" TextMode="Password" MaxLength="20"></asp:TextBox><span
                                class="starSpan">*</span>
                        </td>
                    </tr>
                   
                    <tr>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="E-mail" CssClass="eslbl"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="estbox" ToolTip="Email" Width="" MaxLength="80" ></asp:TextBox><span
                                class="starSpan">*</span>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="lblphno" runat="server" Text="Phone No" CssClass="eslbl"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtphno" runat="server" CssClass="estbox" ToolTip="Phone No" Width="" MaxLength="80" ></asp:TextBox><span
                                class="starSpan">*</span>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnRegister" runat="server" CssClass="esbtn" Text="Register" OnClientClick="javascript:return validate();"  Font-Underline="False"
                                OnClick="btnRegister_Click"></asp:Button>&nbsp;<asp:Button ID="btnCancel" runat="server"
                                    CssClass="esbtn" Text="Reset" onclick="btnCancel_Click" ></asp:Button>
                        </td>
                    </tr>
                </table>
              
            </td>
        </tr>
    </table>
</asp:Content>
