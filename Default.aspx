<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    Title="Login - Essel Projects Pvt. Ltd." %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/style.css" rel="stylesheet" type="text/css" />
    <link href="Css/screen.css" rel="stylesheet" type="text/css" />
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script src="Java_Script/validations.js" type="text/javascript"></script>
    <script src="Java_Script/JScript.js" type="text/javascript"></script>

    <style type="text/css">
        /*MODAL POPUP STARTS*/.modalBackground1
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .modalPopup
        {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 350px;
        }
    </style>

    <script type="text/javascript">
        function closepopup() {
            $find('ModalPopupBehavior').hide();
        }
        function closepopup1() {
            $find('ModalPopupBehavior2').hide();
        }
        function ShowPopup() {
            $find('ModalPopupBehavior').show();
        }
        function ShowPopup2() {
            $find('ModalPopupBehavior2').show();
        }

    </script>
    <script language="javascript" type="text/javascript">
        function popvalidate() {
            var pattern = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
            var emailid = document.getElementById("txtemailid").value;
            if (document.getElementById("txtemailid").value == "") {
                alert("Please Enter Email Id");
                document.getElementById("txtemailid").focus();
                return false;
            }
            else if (pattern.test(emailid) == false) {
                alert('Invalid Email Address');
                return false;
            }
            else if (document.getElementById("txtuserid").value == "") {
                alert("Please Enter User Id");
                document.getElementById("txtuserid").focus();
                return false;
            }
        }
    </script>
    
<script language="javascript">
    function validation() {
        var role = document.getElementById("<%=ddlrole.ClientID %>");
        var rolectrl = document.getElementById("<%=ddlrole.ClientID %>").value;
            if (rolectrl == "Select Role") {
            window.alert("Please Select Role");
            role.focus();
            return false;
        }
    
    }
</script>
<script type="text/javascript">
     var BrowserDetect = {
         init: function() {

             this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
             this.version = this.searchVersion(navigator.userAgent)
			|| this.searchVersion(navigator.appVersion)
			|| "an unknown version";
             //               this.OS = this.searchString(this.dataOS) || "an unknown OS";

             this.id = this.searchid(this.dataBrowser) || "An unknown id";
         },
         searchString: function(data) {

             for (var i = 0; i < data.length; i++) {
                 var dataString = data[i].string;
                 var dataProp = data[i].prop;
                 this.versionSearchString = data[i].versionSearch || data[i].identity;

                 if (dataString) {
                     if (dataString.indexOf(data[i].subString) != -1)

                         return data[i].identity;
                 }
                 else if (dataProp)

                     return data[i].identity;
             }
         },
         searchid: function(data) {

             for (var i = 0; i < data.length; i++) {
                 var dataString = data[i].string;
                 var dataProp = data[i].prop;

                 this.versionSearchString = data[i].versionSearch || data[i].identity;

                 if (dataString) {
                     if (dataString.indexOf(data[i].subString) != -1)

                         return data[i].id;
                 }
                 else if (dataProp) {

                     return data[i].id;
                 }
             }
         },
         searchVersion: function(dataString) {

             var index = dataString.indexOf(this.versionSearchString);
             if (index == -1) return;
             return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
         },
         dataBrowser: [
		{
		    string: navigator.userAgent,
		    subString: "Chrome",
		    identity: "Chrome",
		    id: "no"
		},
		{ string: navigator.userAgent,
		    subString: "OmniWeb",
		    versionSearch: "OmniWeb/",
		    identity: "OmniWeb",
		    id: "no"
		},
		{
		    string: navigator.vendor,
		    subString: "Apple",
		    identity: "Safari",
		    versionSearch: "Version",
		    id: "no"
		},
		{

		    prop: window.opera,
		    identity: "Opera",
		    versionSearch: "Version",
		    id: "no"
		},
		{
		    string: navigator.vendor,
		    subString: "iCab",
		    identity: "iCab",
		    id: "no"
		},
		{
		    string: navigator.vendor,
		    subString: "KDE",
		    identity: "Konqueror",
		    id: "no"
		},
		{
		    string: navigator.userAgent,
		    subString: "Firefox",
		    identity: "Firefox",
		    id: "no"
		},
		{
		    string: navigator.vendor,
		    subString: "Camino",
		    identity: "Camino",
		    id: "no"
		},
		{		// for newer Netscapes (6+)
		    string: navigator.userAgent,
		    subString: "Netscape",
		    identity: "Netscape",
		    id: "no"
		},
		{

		    string: navigator.userAgent,
		    subString: "MSIE",
		    identity: "Explorer",
		    versionSearch: "MSIE",
		    id: "yes"
		},
		{
		    string: navigator.userAgent,
		    subString: "Gecko",
		    identity: "Mozilla",
		    versionSearch: "rv",
		    id: "no"
		},
		{ 		// for older Netscapes (4-)
		    string: navigator.userAgent,
		    subString: "Mozilla",
		    identity: "Netscape",
		    versionSearch: "Mozilla",
		    id: "no"
		}
	]

     };
     BrowserDetect.init();
     var ie = 7.0;
     var ff = 3.5;
     var browser = BrowserDetect.browser;
     var version = BrowserDetect.version;
     var id = BrowserDetect.id;
     var browser_name = BrowserDetect.browser;
     if (id == 'no') {
         window.location = "http://myessel.esselprojects.com/HTMLPage.htm";
     }
     else {
         version = version.toString();
         //  var mySplitResult = version.split(".");
         // var datasplit = mySplitResult.length;

         // if (mySplitResult[1] = null) {

         // version = mySplitResult[0] + "." + "0";
         //     version = parseFloat(version);
         // }
         // else 
         //   {
         //     version = mySplitResult[0] + "." + mySplitResult[1];
         //     version = parseFloat(version);
         //    }

         // if (browser == 'Explorer') {

         //    if (ie > version) {

         //      window.location = "http://www.esic.in/InsuranceDRStaging/ESICInsurancePortal/Compatibility.aspx";
         //  }


         // }
         //else if (browser == 'Firefox') {

         //if (parseFloat(ff).toFixed(2) > parseFloat("3.6").toFixed(2)) {

         //        window.location = "http://www.esic.in/InsuranceDRStaging/ESICInsurancePortal/Compatibility.aspx";
         //    }

         // }

     }
       
       
       
</script>
</head>
<body>
    <center>
        <table width="990px" style="border-left: 1px solid Black; border-bottom: 1px solid Black;
            border-right: 1px solid Black; border-top: 1px solid Black">
            <tr>
                <td>
                    <table width="100%">
                        <tbody>
                            <tr align="center">
                                <td id="top" colspan="3">
                                    <p id="cmp_logo">
                                        <a href="/" target="_top">
                                            <img alt="" id="" src="images/logo1.jpg" height="65px" width="180px" />
                                        </a>
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <%-- <table width="900px">
            <tr align="center">
                <td id="top" colspan="4">
                    <p id="cmp_logo">
                        <a href="/" target="_top">
                            <img alt="" id="company_logo" src="images/essellogo.jpg.jpg">
                        </a>
                    </p>
                </td>
            </tr>
        </table>--%>
                    <table class="view" cellpadding="0" cellspacing="0" style="border: none; height: 500px;">
                        <tbody>
                            <tr align="center">
                                <td style="padding: 150px 35px 5px 35px; min-width: 100px;" valign="top" width="450px">
                                    <form name="loginform" id="loginform" style="padding-bottom: 5px; min-width: 100px;
                                    width: 350px;" runat="server">
                                    <cc1:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server">
                                    </cc1:toolkitscriptmanager>
                                    <fieldset class="box">
                                        <legend style="padding: 4px;">
                                            <img src="images/stock_person.png" alt="">
                                        </legend>
                                        <div class="box2" style="padding: 5px 5px 20px 5px">
                                            <table width="100%" align="center" cellspacing="2px" cellpadding="0" style="border: none;">
                                                <tbody>
                                                    <tr>
                                                        <td class="label">
                                                            <label for="user">
                                                                User:</label>
                                                        </td>
                                                        <td style="padding: 3px;">
                                                            <asp:TextBox ID="txtusername" runat="server" CssClass="db_user_pass"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <label for="password">
                                                                Password:</label>
                                                        </td>
                                                        <td style="padding: 3px;">
                                                            <asp:TextBox ID="txtpwd" runat="server" CssClass="db_user_pass" TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="db_login_buttons">
                                                            <%-- <button type="submit" class="static_boxes">
                                                Login</button>--%>
                                                            <asp:Button ID="btnsubmit" runat="server" Text="LogIn" CssClass="static_boxes" OnClick="btnsubmit_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="item item-char">
                                                            <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
                                                            <asp:Label ID="lblAlert1" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" id="tdcrissue" runat="server" colspan="2">
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel4"
                                                                TargetControlID="HyperLink1" BackgroundCssClass="modalBackground1" BehaviorID="ModalPopupBehavior2"
                                                                 DropShadow="true" />
                                                            <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Style="display: none">
                                                                <table class="estbl" style="width: 350px" id="Table1" runat="server">
                                                                    <tr style="border: 1px solid #000">
                                                                        <th valign="top" colspan="2" align="center">
                                                                            `
                                                                            <asp:ImageButton ID="ImageButton3" align="right" runat="server" ImageUrl="~/images/mpcancel.png"
                                                                                OnClientClick="closepopup1()" />
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" style="width: 230px">
                                                                            <asp:Label ID="Label6" CssClass="eslbl" runat="server" Text="Email Id"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="left">
                                                                            <asp:TextBox ID="txtemailid" CssClass="estbox" runat="server" Width="150px"></asp:TextBox><span
                                                                                class="starSpan">*</span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 120px">
                                                                            <asp:Label ID="Label7" CssClass="eslbl" runat="server" Text="User Id"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 230px">
                                                                            <asp:TextBox ID="txtuserid" CssClass="estbox" runat="server" Width="150px"></asp:TextBox><span
                                                                                class="starSpan">*</span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2" class="style9">
                                                                            <asp:Button ID="Button3" CssClass="esbtn" OnClientClick="javascript:return popvalidate()"
                                                                                runat="server" Text="OK" Width="30px" OnClick="Button3_Click" />
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblvmsg" ForeColor="Red"  runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:Label ID="Label9" runat="server"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:HyperLink ID="HyperLink1" Font-Underline="false" Font-Bold="true" ForeColor="Blue" style="cursor:pointer"
                                                                runat="server">I Forgot Password ?</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <asp:UpdatePanel ID="upslots" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table class="estbl" style="width: 350px" id="tbcpwd" runat="server">
                                                                <tr style="border: 1px solid #000">
                                                                    <th valign="top" colspan="2" align="center">
                                                                        <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Text="Change Password"></asp:Label>
                                                                        <asp:ImageButton ID="ImageButton1" align="right" runat="server" ImageUrl="~/images/mpcancel.png"
                                                                            OnClientClick="closepopup()" />
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" style="width: 230px">
                                                                        <asp:Label ID="lblpasswd" CssClass="eslbl" runat="server" Text="Old Password"></asp:Label>
                                                                    </td>
                                                                    <td valign="top" align="left">
                                                                        <asp:TextBox ID="txtold" CssClass="estbox" runat="server" Width="150px" TextMode="Password"></asp:TextBox><span
                                                                            class="starSpan">*</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 120px">
                                                                        <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="New Password"></asp:Label>
                                                                    </td>
                                                                    <td align="left" style="width: 230px">
                                                                        <asp:TextBox ID="txtnew" CssClass="estbox" runat="server" Width="150px" TextMode="Password"></asp:TextBox><span
                                                                            class="starSpan">*</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 140px">
                                                                        <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="Confirm Password"></asp:Label>
                                                                    </td>
                                                                    <td align="left" style="width: 230px">
                                                                        <asp:TextBox ID="txtconfirm" CssClass="estbox" runat="server" Width="150px" TextMode="Password"></asp:TextBox><span
                                                                            class="starSpan">*</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" class="style9">
                                                                        <asp:Button ID="OkButton" CssClass="esbtn" runat="server" Text="OK" OnClick="OkButton_Click" />&nbsp;
                                                                        <asp:Button ID="CancelButton" CssClass="esbtn" runat="server" Text="Reset" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                                        <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                                    </asp:Panel>
                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="Panel1"
                                                        TargetControlID="btnModalPopUp" BackgroundCssClass="modalBackground1" BehaviorID="ModalPopupBehavior"
                                                        CancelControlID="CancelButton" DropShadow="true" />
                                                    <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Style="display: none">
                                                        <table class="estbl">
                                                            <tr style="border: 1px solid #000">
                                                                <th valign="top" colspan="2" align="center">
                                                                    <asp:ImageButton ID="ImageButton2" align="right" runat="server" ImageUrl="~/images/mpcancel.png"
                                                                        OnClientClick="closepopup()" />
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="width: 230px">
                                                                    <asp:Label ID="Label5" CssClass="eslbl" runat="server" Text="Role"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="left">
                                                                    <asp:DropDownList ID="ddlrole" runat="server" ToolTip="Role">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2" class="style9">
                                                                    <asp:Button ID="btnmdlsubmit" CssClass="esbtn" runat="server" Text="Submit" OnClick="btnmdlsubmit_Click" OnClientClick="javascript:return validation();" />&nbsp;
                                                                    <asp:Button ID="Button2" CssClass="esbtn" runat="server" Text="Reset" />
                                                                </td>
                                                            </tr>
                                                            <asp:Button runat="server" ID="Button1" Style="display: none" />
                                                        </table>
                                                    </asp:Panel>
                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel3"
                                                        TargetControlID="Button1" BackgroundCssClass="modalBackground1" BehaviorID="ModalPopupBehavior1"
                                                        CancelControlID="CancelButton" DropShadow="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </fieldset>
                                    </form>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="footer_a">
                        <div align="center">
                            Powered by <a href="#" target="_blank">SL Touch</a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</body>
</html>
