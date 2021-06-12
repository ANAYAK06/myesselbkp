<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="EditItemCode.aspx.cs"
    Inherits="EditItemCode" Title="Edit Item Code - Essel Projects Pvt.Ltd." EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <%--    <script src="Java_Script/ccMessageBox.js" type="text/javascript"></script>
    --%>
    <style type="text/css">
        .modalBackground
        {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <style type="text/css">
        .popup-div-background
        {
            position: absolute;
            top: 0;
            left: 0;
            background-color: #ccc;
            filter: alpha(opacity=90);
            opacity: 0.9; /* the following two line will make sure
             /* that the whole screen is covered by
                 /* this transparent layer */
            height: 100%;
            width: 100%;
            min-height: 100%;
            min-width: 100%;
        }
    </style>
    <script type="text/javascript">
        function validation() {
            var bprice = document.getElementById("<%=txtbasicpricep.ClientID %>").value;
            var objs = new Array("<%=txtspecificationp.ClientID %>", "<%=txtname.ClientID %>", "<%=txtunitsp.ClientID %>", "<%=txtbasicpricep.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (bprice <= 0) {
                window.alert("basic price must not be zero");
                return false;
            }
            var basic = document.getElementById("<%=hfbasic.ClientID %>").value;
            var newprice = document.getElementById("<%=txtbasicpricep.ClientID %>").value;
//            if (parseFloat(newprice) > parseFloat(basic)) {
//                window.alert("You are not able to increase Basic price");
//                return false;
//            }
            document.getElementById("<%=btnUpdate.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript">
        var checkContents = function (input) {
            var text = input.value;
            if (!/[0-9]/.test(text))
                input.value = "";
            return false;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function restrictComma() {
            if (event.keyCode == 188) {
                alert("Can't Enter Comma");
                event.returnValue = false;
            }
        }
        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode == 8 || charCode == 46) return false;
            return true;
        }    
               
    </script>
    <script type="text/javascript">
        function IsNumeric(event) {
            var theEvent = event || window.event;
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);
            var regex = /[0-9]|\./;
            if (!regex.test(key)) {
                theEvent.returnValue = false;
                theEvent.preventDefault();
            }
        }

    </script>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%" style="vertical-align: top">
                    <tr valign="top">
                        <td style="vertical-align: top">
                            <div class="wrap">
                                <table class="view" cellpadding="0" cellspacing="0" border="0" width="90%">
                                    <tr>
                                        <td width="90%" colspan="4">
                                            <div class="notebook-page notebook-page-active">
                                                <asp:Panel ID="pnlitems" runat="server" Style="">
                                                    <table width="400px" border="0" align="center" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="left">
                                                                <h1>
                                                                    Edit Master Data Items
                                                                </h1>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="800px" valign="top" class="popcontent">
                                                                <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                    height: 500px;">
                                                                    <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
                                                                                <ProgressTemplate>
                                                                                    <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                                                        <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                                                            left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                                            <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                            <table style="vertical-align: middle;" align="center" width="900px">
                                                                                <tr style="below">
                                                                                    <td class="label" width="30%" align="center">
                                                                                        <asp:Label ID="Label17" runat="server" Text="Item Name" CssClass="char"></asp:Label>
                                                                                    </td>
                                                                                    <td class="item item-char" valign="middle" style="width: 300px">
                                                                                        <asp:TextBox ID="txtSearch" CssClass="m2o_search" Width="100%" Height="20px" runat="server"
                                                                                            Style="background-image: url(images/search_grey.png); background-position: right;
                                                                                            background-repeat: no-repeat; border-color: #CBCCCC; font-size: smaller; text-transform: uppercase"
                                                                                            onkeydown="return restrictComma()" onkeypress="javascript:checkContents(this);"></asp:TextBox>
                                                                                        <cc1:AutoCompleteExtender ServiceMethod="Searchitemcodes" MinimumPrefixLength="1"
                                                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtSearch"
                                                                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="Search here.."
                                                                                            WatermarkCssClass="watermarked" TargetControlID="txtSearch" runat="server">
                                                                                        </cc1:TextBoxWatermarkExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btngo" runat="server" Text="Go" CssClass="button" OnClick="btngo_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="100%" colspan="6">
                                                                                        <div style="padding-left: 20px; padding-right: 20px;">
                                                                                            <center>
                                                                                                <div class="notebook-pages">
                                                                                                    <div class="notebook-page notebook-page-active">
                                                                                                        <table border="0" class="fields" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td width="50%" valign="top" class="item-group">
                                                                                                                        <table border="0" class="fields" width="750px">
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="30%" align="center">
                                                                                                                                    <asp:Label ID="Label8" runat="server" Text="Category Code"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                    <span class="filter_item">
                                                                                                                                        <asp:TextBox ID="txtcategorycode" runat="server" CssClass="char" Enabled="false"
                                                                                                                                            ToolTip="Category Name"></asp:TextBox>
                                                                                                                                    </span>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="1%" align="center">
                                                                                                                                    <asp:Label ID="Label9" runat="server" Text="Category Name"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-char" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtcategoryname" runat="server" Enabled="false" CssClass="char"
                                                                                                                                        ToolTip="Category Name"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <table border="0" class="fields">
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="30%" align="center">
                                                                                                                                    <asp:Label ID="Label10" runat="server" Text="DCA" CssClass="char"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-selection" valign="middle">
                                                                                                                                    <asp:DropDownList ID="ddldetailhead" CssClass="esddown" Width="" ToolTip="DCA" Enabled="false"
                                                                                                                                        runat="server">
                                                                                                                                    </asp:DropDownList>
                                                                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="dca" TargetControlID="ddldetailhead"
                                                                                                                                        ServiceMethod="cash" ServicePath="cascadingDCA.asmx" PromptText="Select DCA">
                                                                                                                                    </cc1:CascadingDropDown>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="label" align="center">
                                                                                                                                    <asp:Label ID="Label11" runat="server" Text="Sub DCA"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-selection" valign="middle">
                                                                                                                                    <asp:DropDownList ID="ddlsubdetail" CssClass="esddown" Width="" runat="server" Enabled="false"
                                                                                                                                        ToolTip="Sub DCA">
                                                                                                                                    </asp:DropDownList>
                                                                                                                                    <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="subdca" TargetControlID="ddlsubdetail"
                                                                                                                                        ParentControlID="ddldetailhead" ServiceMethod="SUBDCA" ServicePath="cascadingDCA.asmx"
                                                                                                                                        PromptText="Select Sub DCA">
                                                                                                                                    </cc1:CascadingDropDown>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                        <table border="0" class="fields">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td colspan="2" valign="top" class="item-group" width="50%">
                                                                                                                        <table border="0" class="fields" width="100%">
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="30%" align="center">
                                                                                                                                    <asp:Label ID="Label12" runat="server" Text="Group Code"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="%" class="item item-char" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtgroupcode" runat="server" CssClass="char" Enabled="false" ToolTip="Group Code"
                                                                                                                                        MaxLength="2"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="1%" align="center">
                                                                                                                                    <asp:Label ID="Label13" runat="server" Text="Group Name"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-char" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtgroupname" runat="server" CssClass="char" Enabled="false" ToolTip="Group Name"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <table border="0" class="fields">
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="" align="center">
                                                                                                                                    <asp:Label ID="Label14" runat="server" Text="Sub-Group Code" CssClass="char"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-selection" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtsubgroupcodep" runat="server" CssClass="char" Enabled="false"
                                                                                                                                        ToolTip="Sub-Group Code" MaxLength="3"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                        <table border="0" class="fields">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td colspan="2" valign="top" class="item-group" width="50%">
                                                                                                                        <table border="0" class="fields" width="100%">
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="30%" align="center">
                                                                                                                                    <asp:Label ID="Label16" runat="server" Text="Specification Code"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-char" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtspecificationCode" runat="server" CssClass="char" Enabled="false"
                                                                                                                                        ToolTip="Specification" MaxLength="3"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="30%" align="center">
                                                                                                                                    <asp:Label ID="lblbasic" runat="server" Text="Basic Price"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-char" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtbasicpricep" runat="server" CssClass="char" ToolTip="basicprice"
                                                                                                                                        onkeypress='return IsNumeric(event)'></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <table border="0" class="fields" width="100%">
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="30%" align="center">
                                                                                                                                    Specification
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-selection" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtspecificationp" runat="server" CssClass="char" ToolTip="Specification"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="30%" align="center" valign="top">
                                                                                                                                    <asp:Label ID="lblunit" runat="server" Text="Units" Style="vertical-align: top"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-char" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtunitsp" runat="server" CssClass="char" ToolTip="Units"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                        <table border="0" class="fields">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td colspan="2" valign="top" class="item-group">
                                                                                                                        <table border="0" class="fields" width="100%">
                                                                                                                            <tr>
                                                                                                                                <td class="label" width="30%" align="center">
                                                                                                                                    <asp:Label ID="Label18" runat="server" Text="Item Name"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td width="" class="item item-char" valign="middle">
                                                                                                                                    <asp:TextBox ID="txtname" runat="server" Width="620px" CssClass="char" ToolTip="Item Name"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr align="center">
                                                                                                                    <td colspan="4">
                                                                                                                        <asp:HiddenField ID="hfbasic" runat="server" />
                                                                                                                        <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" OnClick="btnUpdate_Click"
                                                                                                                            OnClientClick="return validation();" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </center>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </td>
                                                            <td bgcolor="#FFFFFF">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
