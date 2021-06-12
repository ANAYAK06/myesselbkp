<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" Title="Registration Form - Essel Projects Pvt. Ltd." %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="../js/JScript.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
  
 <table style="width:900px" >
  <tr valign="top">
  <td align="center">
    
      
      <table class="estbl" width="400px">
   <tr style="border:1px solid #000">
   <th valign="top"  colspan="4" align="center" >
   <asp:Label id="lblRegister" runat="server" Text="Registration" CssClass="esfmhead"></asp:Label>
   </th>
   </tr>
    <tr>
    <td style="width:130px" >
    <asp:Label ID="lblUsername" runat="server" CssClass="eslbl" Text="User Name" AssociatedControlID="txtUsername"></asp:Label></td>
    <td style="width:250px" align="left" ><asp:TextBox ID="txtUsername" runat="server" 
            CssClass="estbox" onkeyup="usernameChecker(this.value);" MaxLength="50" ></asp:TextBox><span class="starSpan">* </span>
    <span id="spanAvailability"></span>
     <script type="text/javascript">
            var usernameCheckerTimer;
            var spanAvailability = $get("spanAvailability");

            function usernameChecker(username) 
            {
                clearTimeout(usernameCheckerTimer);
                if (username.length == 0)
                    spanAvailability.innerHTML = "";
                else
                {
                    spanAvailability.innerHTML = "<span style='color: #ccc;'>checking...</span>";
                    usernameCheckerTimer = setTimeout("checkUsernameUsage('" + username + "');", 750);
                }
            }

            function checkUsernameUsage(username) 
            {
                // initiate the ajax pagemethod call
                // upon completion, the OnSucceded callback will be executed
                PageMethods.IsUserAvailable(username, OnSucceeded);
            }

            // Callback function invoked on successful completion of the page method.
            function OnSucceeded(result, userContext, methodName) 
            {
                if (methodName == "IsUserAvailable")
                {
                     var btnReg = document.getElementById('ctl00_ContentPlaceHolder1_btnRegister');
                     if (result == true)
                    {
                        spanAvailability.innerHTML = "<span style='color: DarkGreen;'>Available</span>";
                        btnReg.disabled = false;
                    }

                    else
                    {
                       spanAvailability.innerHTML = "<span style='color: Red;'>Try Another...</span>";
                        btnReg.disabled = true;
                    }
                     
                }
            }
        </script>    
    </td>
    
    </tr>
    <tr>
        <td >
            <asp:Label ID="lblFName" runat="server" CssClass="eslbl" Text="First Name"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtFName" runat="server" CssClass="estbox" 
                CausesValidation="True" MaxLength="50"></asp:TextBox><span class="starSpan">*</span>
            
            
            </td>
    </tr>

<tr><td   ><asp:Label id="lblLName" runat="server"  Text="Last Name"  CssClass="eslbl"></asp:Label></td>
    <td align="left" >
        <asp:TextBox id="txtLName" runat="server" CssClass="estbox" MaxLength="50"></asp:TextBox><span class="starSpan">*</span></td>
        
    
</tr> 
<tr><td ><asp:Label ID="lblPwd" CssClass="eslbl" runat="server" Text="Password"></asp:Label>
</td>
<td align="left" ><asp:TextBox ID="txtPwd" runat="server" CssClass="estbox" 
        TextMode="Password" MaxLength="20" ></asp:TextBox><span class="starSpan">*</span>
</td>
</tr>
<tr><td ><asp:Label ID="lblCPwd" runat="server" CssClass="eslbl" Text="Confirm Password">
</asp:Label>
</td>
<td align="left"><asp:TextBox ID="txtCPwd" CssClass="estbox" runat="server" 
        TextMode="Password" MaxLength="20"></asp:TextBox><span class="starSpan">*</span>
</td>
</tr>
<tr><td  ><asp:Label id="lblPhoneno" runat="server"  Text="Phone No" CssClass="eslbl"></asp:Label></td>
<td align="left" >
    <asp:TextBox id="txtPhoneno" runat="server" CssClass="estbox" MaxLength="50" ></asp:TextBox><%--<span class="starSpan">*</span>--%>
    
    
    
        </td>
    </tr>
    <tr><td   ><asp:Label id="lblEmail" runat="server"  Text="e-mail" CssClass="eslbl"></asp:Label></td>
<td align="left">
    <asp:TextBox id="txtEmail" runat="server" CssClass="estbox"  Width="" 
        MaxLength="50"></asp:TextBox><span class="starSpan">*</span></td>
    </tr>
    <tr><td >
    <asp:Label ID="lblSex" runat="server" Text="Sex" CssClass="eslbl"></asp:Label>
    
    </td>
    <td align="left">
    
    <asp:DropDownList ID="ddlSex" runat="server" CssClass="esddown">
        <asp:ListItem Value="0">-- Select --</asp:ListItem>
        <asp:ListItem Value="1">Male</asp:ListItem>
        <asp:ListItem Value="2">Female</asp:ListItem>
        </asp:DropDownList><%--<span class="starSpan">*</span>--%>
    </td>
    </tr>
    
    <tr><td align="center"colspan="2">
        <asp:Button id="btnRegister" runat="server" CssClass="esbtn" Text="Register" 
            Font-Underline="False" onclick="btnRegister_Click" ></asp:Button>&nbsp;<asp:Button 
            id="btnCancel"  runat="server" CssClass="esbtn" 
            Text="Reset" onclick="btnCancel_Click"  ></asp:Button> </td>
    </tr>
       
   </table>
  <table>
  <tr>
           <td align="center" colspan="2" >
               <asp:Label id="lblAlert" runat="server" CssClass="eslblalert"></asp:Label></td>
       </tr>
  </table>
    
         </td> </tr>
       </table>




</asp:Content>

