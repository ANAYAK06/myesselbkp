<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertificatePreview.aspx.cs"
    Inherits="CertificatePreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../Css/style.css" />
<link rel="stylesheet" type="text/css" href="../Css/menu.css" />
<link href="../Css/screen.css" rel="stylesheet" type="text/css" />
<link href="../Css/V-Menu.css" rel="stylesheet" type="text/css" />
<link href="../Css/FancyTab.css" rel="stylesheet" type="text/css" />
<link href="../Css/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../Css/AjaxStyles.css" rel="stylesheet" type="text/css" />
<link href="../Css/buttons.css" rel="stylesheet" type="text/css" />
<link href="../Css/print.css" rel="stylesheet" type="text/css" />
<link href="../Css/esselCssStyle.css" rel="stylesheet" type="text/css" />

<script src="Java_Script/bubble-tooltip.js" type="text/javascript"></script>

<script src="../Java_Script/newcalendar.js" type="text/javascript"></script>

<script src="../Java_Script/MochiKit.js" type="text/javascript"></script>

<script src="../Java_Script/validations.js" type="text/javascript"></script>

<script src="../Java_Script/JScript.js" type="text/javascript"></script>

<script src="Java_Script/calendar.js" type="text/javascript"></script>

<script src="Java_Script/calendar-setup.js" type="text/javascript"></script>

<script src="../Java_Script/accordion.js" type="text/javascript"></script>
    <script language="javascript">
        function validate() {
            var file = document.getElementById("fudbankdetails").value;
            if (file == "") {
                window.alert("Please select image");
                return false;
            }
            showProgress();
        }
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress1.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <style type="text/css">
        #overlay
        {
            position: fixed;
            z-index: 99;
            top: 0px;
            left: 0px;
            background-color: #f8f8f8;
            width: 100%;
            height: 100%;
            filter: Alpha(Opacity=90);
            opacity: 0.9;
            -moz-opacity: 0.9;
        }
        #theprogress
        {
            background-color: #fff;
            border: 1px solid #ccc;
            padding: 10px;
            width: 300px;
            height: 30px;
            line-height: 30px;
            text-align: center;
            filter: Alpha(Opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        #modalprogress
        {
            position: absolute;
            top: 40%;
            left: 50%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div id="overlay">
                            <div id="modalprogress">
                                <div id="theprogress">
                                    <img alt="" src="../images/load.gif" />
                                    Loading.. Please Wait...
                                </div>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div>
                    <%-- <img id="img1" runat="server" src="ShowImageHandler.ashx?ID=<%# Eval("ID").ToString() %>"
            width="150" height="100" />--%>
                    <asp:Image ID="Image1" runat="server" Width="400px" Height="500px" />
                </div>
                <div>
                    <center>
                        <div class="notebook-pages" style="vertical-align: middle">
                            <div class="notebook-page notebook-page-active">
                                <table border="0" align="center" style="height: 150px" width="700px" id="tbl" runat="server">
                                    <tbody>
                                        <tr bgcolor="#990033">
                                            <th valign="top" style="background-color: #8B8A8A;" align="center">
                                                <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Upload New Certificate"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td valign="top" align="center" class=" item-group" width="50%">
                                                            <table border="0" width="100%" style="vertical-align: middle">
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border-collapse: separate; border-bottom: 3px; padding-bottom: 3px; height: 10px;">
                                                                        <td>
                                                                            <asp:Label ID="lblbankdetails" CssClass="eslbl" runat="server" Text="Upload document"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="height: 20px">
                                                                            <asp:FileUpload ID="fudbankdetails" Height="20px" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100%" colspan="2" align="center" valign="middle">
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="esbtn" Width="80px" Height="30px"
                                                    Text="Submit" OnClick="btnsubmit_Click" OnClientClick="return validate();" />&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btncancel" CssClass="esbtn" Width="80px" Height="30px" runat="server"
                                                    Text="Cancel" OnClick="btncancel_Click" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </center>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsubmit" />
                <asp:PostBackTrigger ControlID="btncancel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
