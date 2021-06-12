<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEmployeeData.aspx.cs"
    Inherits="Admin_frmEmployeeData" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    
    <link rel="stylesheet" type="text/css" href="Css/style.css" />
    <link rel="stylesheet" type="text/css" href="Css/screen.css" />
    <link rel="stylesheet" type="text/css" href="Css/calender-blue.css" />
    <link rel="stylesheet" type="text/css" href="Css/notebook.css" />
    <script src="Java_Script/newcalendar.js" type="text/javascript"></script>
    <script src="Java_Script/JScript.js" type="text/javascript"></script>
 
    <style type="text/css">
        .buttonreg
        {
            /* Disables default styling */
            border: none;
            background: #858686;
            color: #fff;
            height: 15px; /* You need to set font, font size and cursor for buttons */
            font-size: 15px;
            font-family: "Helvetica Neue" , Arial, Helvetica, Geneva, sans-serif;
            cursor: hand;
            padding: 8px 13px;
            font-weight: bold;
        }
        .buttonup
        {
            /* Disables default styling */
            border: none;
            background: #858686;
            color: #fff;
            height: 15px; /* You need to set font, font size and cursor for buttons */
            font-size: 10px;
            font-family: Verdana;
            cursor: hand; /*padding: 8px 13px;*/
            font-weight: bold;
        }
    </style>

    <script language="javascript">
    function Enable()
    {
    var role=document.getElementById("<%=ddlrole.ClientID %>").value;
    var cc=document.getElementById("<%=ddlcccode.ClientID %>");
    if(role=="Accountant" || role=="StoreKeeper")
    {
       cc.disabled=true;
    }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <center>
        <div style="padding-left: 20px; padding-right: 20px;">
            <center>
                <table width="750px" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td colspan="4" valign="top" class=" item-group" width="70%">
                                <table border="0" class="fields">
                                    <tbody>
                                        <tr>
                                            <td colspan="6" valign="top" class=" item-group">
                                                <table border="0" class="fields">
                                                    <tr id="uid" runat="server">
                                                        <td class="label" width="1%" align="center">
                                                            <asp:Label ID="Label2" runat="server" Text="Employee Id :"></asp:Label>
                                                        </td>
                                                        <td class="item item-char" valign="middle" colspan="4">
                                                            <asp:TextBox ID="txtuserid" runat="server" CssClass="char" ToolTip="Employee id"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label" width="1%" align="center">
                                                            <asp:Label ID="lblccode" runat="server" Text="First Name :"></asp:Label>
                                                        </td>
                                                        <td class="item item-char" valign="middle" colspan="2">
                                                            <asp:TextBox ID="txtfname" runat="server" CssClass="char" ToolTip="First Name"></asp:TextBox>
                                                            <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtfname"
                                                                WatermarkText="Mr/Ms">
                                                            </asp:TextBoxWatermarkExtender>--%>
                                                        </td>
                                                        <td class="label" width="1%" align="center">
                                                            <asp:Label ID="Label5" runat="server" Text="Last Name :"></asp:Label>
                                                        </td>
                                                        <td class="item item-char" valign="middle" colspan="2">
                                                            <asp:TextBox ID="txtlname" runat="server" CssClass="char" ToolTip="Last Name"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label" width="1%" align="center">
                                                            <asp:Label ID="Label6" runat="server" Text="Middle Name :"></asp:Label>
                                                        </td>
                                                        <td class="item item-char" valign="middle" colspan="2">
                                                            <asp:TextBox ID="txtmidddle" runat="server" CssClass="char" ToolTip="Middle Name"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="pwd" runat="server">
                                                        <td class="label" width="1%" align="center">
                                                            <asp:Label ID="Label3" runat="server" Text="Password :" CssClass="char"></asp:Label>
                                                        </td>
                                                        <td colspan="2" class="item item-selection" valign="middle">
                                                            <asp:TextBox ID="txtpwd" runat="server" CssClass="char" ToolTip="Password" TextMode="Password"></asp:TextBox>
                                                        </td>
                                                        <td class="label" width="1%" align="center">
                                                            <asp:Label ID="Label4" runat="server" Text="Confirm Password :" CssClass="char"></asp:Label>
                                                        </td>
                                                        <td class="item item-selection" valign="middle">
                                                            <asp:TextBox ID="txtcpwd" runat="server" CssClass="char" ToolTip="Confirm Password"
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td colspan="2" valign="top">
                                <table border="0" width="50%">
                                    <tbody>
                                        <tr>
                                            <td align="center">
                                                <div id="photo_binary_buttons">
                                                    <asp:Image ID="img" runat="server" ImageAlign="Middle" ImageUrl="images/photo.gif" />
                                                    <br />
                                                    <asp:FileUpload ID="fluImage" runat="server" Style="" Font-Size="Small" />
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </center>
        </div>
        <div class="notebook-pages">
            <div class="notebook-page notebook-page-active">
                <div style="padding-left: 20px; padding-right: 20px;">
                    <div>
                        <table border="0" align="center" width="750px">
                            <tbody>
                                <tr>
                                    <td colspan="2" valign="top" class=" item-group" width="50%">
                                        <table border="0" width="93%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2" valign="middle" width="100%" style="border-bottom: 1px solid #525254;">
                                                        <div class="separator horizontal" style="font-size: 10pt">
                                                            Personal Information</div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="ssnid" class="help">
                                                            Father's Name
                                                        </label>
                                                    </td>
                                                    <td class="item item-char" valign="middle">
                                                        <asp:TextBox ID="txtfathername" runat="server" CssClass="char" ToolTip="Father Name"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="passport_id">
                                                            Nominee's Name
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-char" valign="middle">
                                                        <asp:TextBox ID="txtnname" runat="server" CssClass="char" ToolTip="Nominee's Name"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="passport_id">
                                                            Relation
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-char" valign="middle">
                                                        <asp:TextBox ID="txtrelation" runat="server" CssClass="char" ToolTip="Relation"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="birthday">
                                                            Date of Birth
                                                        </label>
                                                        :
                                                    </td>
                                                    <td width="75%" class="item item-datetime" align="left">
                                                        <asp:TextBox ID="txtdate" runat="server" CssClass="char" ToolTip="Date of birth"
                                                            Width="225px"></asp:TextBox>
                                                        <img onclick="scwShow(document.getElementById('txtdate'),this);" alt="" src="images/stock_calendar.png"
                                                            style="width: 15px; height: 15px; vertical-align: middle;" id="Img2" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="marital">
                                                            Marital Status
                                                        </label>
                                                        :
                                                    </td>
                                                    <td width="99%" class="item item-selection" valign="middle">
                                                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="selection selection_search readonlyfield"
                                                            ToolTip="Martial Status">
                                                            <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                                                            <asp:ListItem Value="Single" Text="Single"></asp:ListItem>
                                                            <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                                                            <asp:ListItem Value="Divorced" Text="Divorced"></asp:ListItem>
                                                            <asp:ListItem Value="Widower" Text="Widower"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td colspan="2" valign="top" class=" item-group" width="50%">
                                        <table border="0" width="85%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2" valign="middle" class="item" width="100%" style="border-bottom: 1px solid #525254;">
                                                        <div class="separator horizontal" style="font-size: 10pt">
                                                            Status</div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="gender">
                                                            User Status
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-selection" valign="middle">
                                                        <asp:DropDownList ID="ddlgen" runat="server" CssClass="selection selection_search readonlyfield"
                                                            ToolTip="Status">
                                                            <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                                                            <asp:ListItem Value="New" Text="New"></asp:ListItem>
                                                            <asp:ListItem Value="Existing" Text="Existing"></asp:ListItem>
                                                            <asp:ListItem Value="Re-Joining" Text="Re-Joining"></asp:ListItem>
                                                            <asp:ListItem Value="Left" Text="Left"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="birthday">
                                                            Date of joining
                                                        </label>
                                                        :
                                                    </td>
                                                    <td width="75%" class="item item-datetime" align="left">
                                                        <asp:TextBox ID="txtjoin" runat="server" CssClass="char" ToolTip="Date of birth"
                                                            Width="200px"></asp:TextBox>
                                                        <img onclick="scwShow(document.getElementById('txtjoin'),this);" alt="" src="images/stock_calendar.png"
                                                            style="width: 15px; height: 15px; vertical-align: middle;" id="Img1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="passport_id">
                                                            Referred By
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-char" valign="middle">
                                                        <asp:TextBox ID="txtreferrred" runat="server" CssClass="char" ToolTip="Referred By"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="birthday">
                                                            Date of leaving
                                                        </label>
                                                        :
                                                    </td>
                                                    <td width="75%" class="item item-datetime" align="left">
                                                        <asp:TextBox ID="txtleave" runat="server" CssClass="char" ToolTip="Date of birth"
                                                            Width="200px"></asp:TextBox>
                                                        <img onclick="scwShow(document.getElementById('txtleave'),this);" alt="" src="images/stock_calendar.png"
                                                            style="width: 15px; height: 15px; vertical-align: middle;" id="Img3" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top" class=" item-group" width="50%">
                                        <table border="0" class="fields" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2" valign="middle" class=" item-separator" width="100%" style="border-bottom: 1px solid #525254;">
                                                        <div class="separator horizontal" style="font-size: 10pt">
                                                            Contact Information</div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="address_home_id_text">
                                                            Home Address
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-char" valign="middle">
                                                        <asp:TextBox ID="txtadd" runat="server" CssClass="char" ToolTip="Address"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="work_phone">
                                                            Work Phone
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-char" align="left">
                                                        <asp:TextBox ID="txtph" runat="server" CssClass="char" MaxLength="5" Width="85px"
                                                            ToolTip="STD Code"></asp:TextBox>&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtph2" runat="server" CssClass="char" MaxLength="8" Width="80px"
                                                            ToolTip="Land Line No"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="mobile_phone">
                                                            Mobile
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-char" align="left">
                                                        <span style="border: 1px solid #d8d8d8; font-size: small;">
                                                            <asp:Label ID="Lbl91" Font-Size="Small" runat="server" Text=" +91"></asp:Label>
                                                            <asp:TextBox ID="txtph1" runat="server" ToolTip="" Style="width: 160px" CssClass="char"
                                                                MaxLength="15" BorderStyle="None"></asp:TextBox></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="work_email">
                                                            Work E-mail
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-char" valign="middle">
                                                        <asp:TextBox ID="txtemail" runat="server" CssClass="char" ToolTip="Email id"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td colspan="2" valign="top" class=" item-group" for="" width="50%">
                                        <table border="0" width="85%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2" valign="middle" class=" item-separator" for="" width="100%" style="border-bottom: 1px solid #525254;">
                                                        <div class="separator horizontal" style="font-size: 10pt">
                                                            Position</div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%" align="center">
                                                        <asp:Label ID="lbldca" runat="server" Text="Department" CssClass="char"></asp:Label>
                                                    </td>
                                                    <td colspan="4" class="item item-selection" valign="middle">
                                                        <asp:DropDownList ID="ddldep" runat="server" CssClass="selection selection readonlyfield"
                                                            ToolTip="Department">
                                                            <asp:ListItem Value="Select">Seelct</asp:ListItem>
                                                            <asp:ListItem Value="Project Management" Text="Project Management"></asp:ListItem>
                                                            <asp:ListItem Value="Safety" Text="Safety"></asp:ListItem>
                                                            <asp:ListItem Value="Adminstration" Text="Adminstration"></asp:ListItem>
                                                            <asp:ListItem Value="Human Resource" Text="Human Resource"></asp:ListItem>
                                                            <asp:ListItem Value="Finance/Accouns" Text="Finance/Accouns"></asp:ListItem>
                                                            <asp:ListItem Value="Training" Text="Training"></asp:ListItem>
                                                            <asp:ListItem Value="Stores" Text="Stores"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%" align="center">
                                                        <asp:Label ID="Label1" runat="server" Text="Category" CssClass="char"></asp:Label>
                                                    </td>
                                                    <td class="item item-selection" valign="middle">
                                                        <asp:DropDownList ID="ddlcat" runat="server" CssClass="selection selection_search readonlyfield"
                                                            ToolTip="Category">
                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                            <asp:ListItem Value="Permanent" Text="Permanent"></asp:ListItem>
                                                            <asp:ListItem Value="Semi Permanent/Contractual" Text="Semi Permanent/Contractual"></asp:ListItem>
                                                            <asp:ListItem Value="Temporary" Text="Temporary"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="job_id_text">
                                                            Role
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-selection" valign="middle">
                                                        <asp:DropDownList ID="ddlrole" runat="server" CssClass="selection selection_search readonlyfield"
                                                            ToolTip="Role">
                                                        </asp:DropDownList>
                                                        <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlrole"
                                                            ServiceMethod="role" ServicePath="cascadingDCA.asmx" Category="dd" PromptText="Select Role">
                                                        </cc1:CascadingDropDown>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label" width="1%">
                                                        <label for="coach_id_text">
                                                            CC Code
                                                        </label>
                                                        :
                                                    </td>
                                                    <td class="item item-selection" valign="middle">
                                                        <asp:DropDownList ID="ddlcccode" runat="server" CssClass="selection selection_search readonlyfield"
                                                            ToolTip="DCA">
                                                        </asp:DropDownList>
                                                        <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlcccode"
                                                            ServiceMethod="WebCC" ServicePath="cascadingDCA.asmx" Category="cc">
                                                        </cc1:CascadingDropDown>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" valign="top" style="height: 35px" align="center">
                                        <table border="0">
                                            <tbody>
                                                <tr style="height: 30px" align="center">
                                                    <td align="left">
                                                        <asp:Button ID="Edit" runat="server" Width="80px" Height="30px" CssClass="buttonreg"
                                                            Text="Update" OnClick="Edit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="Submit" runat="server" Width="90px" Height="30px" CssClass="buttonreg"
                                                            Text="Confirm" OnClick="Submit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="Cancel" runat="server" Text="Reset" Width="80px" CssClass="buttonreg"
                                                            Height="30px" OnClick="Cancel_Click" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </center>
    </form>
</body>
</html>
