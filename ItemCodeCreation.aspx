<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="ItemCodeCreation.aspx.cs"
    Inherits="ItemCodeCreation" Title="Item Code Creation - Essel Projects Pvt.Ltd."
    EnableEventValidation="false" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--    <script src="Java_Script/ccMessageBox.js" type="text/javascript"></script>
    --%>
    <style type="text/css">
        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function numericValidation(txtvalue) {
            //debugger;
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (!(document.getElementById(txtvalue.id).value)) {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
            else {
                var val = document.getElementById(txtvalue.id).value;
                if (charCode == 46 || (charCode > 31 && (charCode > 47 && charCode < 58))) {
                    var points = 0;
                    points = val.indexOf(".", points);
                    if (points >= 1 && charCode == 46) {
                        return false;
                    }
                    if (points >= 1) {
                        var lastdigits = val.substring(val.indexOf(".") + 1, val.length);
                        if (lastdigits.length >= 2) {
                            alert("Two decimal places only allowed");
                            return false;
                        }
                    }
                    return true;
                }
                else {
                    alert("Only Numerics allowed");
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function closepopup() {
            $find('mdlitems').hide();

        }
        function setText(majorgroupname, categorycode) {
            categorycode = document.getElementById("<%=lstcategory.ClientID %>").value;
            PageMethods.IsGroupnameAvailable(majorgroupname, categorycode, OnSucceeded);
            document.getElementById("<%=txtgcode.ClientID %>").value = majorgroupname;

        }
        function OnSucceeded(result, userContext, methodName) {
            if (methodName == "IsGroupnameAvailable") {
                if (result != "") {
                    document.getElementById("<%=txtgname.ClientID %>").value = result;

                }

                else {
                }
            }


        }
    </script>
    <script language="javascript" type="text/javascript">
        function category() {
            var category = document.getElementById("<%=txtcategory.ClientID %>").value;
            if (document.getElementById("<%=txtcategory.ClientID %>").value != "") {

            }
            else {
                document.getElementById("<%=txtcname.ClientID %>").disabled = false;
                document.getElementById("<%=txtcname.ClientID %>").value = "";
                document.getElementById("<%=ddldca.ClientID %>").disabled = false;
                document.getElementById("<%=ddldca.ClientID %>").value = "Select DCA";
                document.getElementById("<%=lstgccode.ClientID %>").options.length = null;
                document.getElementById("<%=lstsubgroup.ClientID %>").options.length = null;

            }
        }

    </script>
    <script type="text/javascript">
        function validatenew() {
            //debugger;
            GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            if (GridView1 != null) {
                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("Please verify");
                        return false;
                    }
                }
            }
            GridView2 = document.getElementById("<%=gvDetails.ClientID %>");
            if (GridView2 != null) {
                for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {
                    if (GridView2.rows(rowCount).cells(1).children[0].value == "") {
                        window.alert("Enter Supplier Name");
                        GridView2.rows(rowCount).cells(1).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(2).children[0].value == "") {
                        window.alert("Enter Make");
                        GridView2.rows(rowCount).cells(2).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(3).children[0].value == "") {
                        window.alert("Enter Rate");
                        GridView2.rows(rowCount).cells(3).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(4).children[0].value == "") {
                        window.alert("Enter Contract No");
                        GridView2.rows(rowCount).cells(4).children[0].focus();
                        return false;
                    }
                    else if (GridView2.rows(rowCount).cells(5).children[0].value == "") {
                        window.alert("Enter E-Mail");
                        GridView2.rows(rowCount).cells(5).children[0].focus();
                        return false;
                    }

                }
            }
            if (GridView2 != null) {
                if (rowCount < 3) {
                    window.alert("Enter minimum two supplier");
                    return false;
                }
            }
            var objs = new Array("<%=txtDescription.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            document.getElementById("<%=Btnsbmt.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function restrictComma() {
            if (event.keyCode == 188) {
                alert("Can't Enter Comma and Less-than sign");
                event.returnValue = false;
            }
        }
    </script>
    <style type="text/css">
        .popup-div-background {
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
    <script language="javascript" type="text/javascript">
        function setText1(subgroupcode) {
            document.getElementById("<%=txtsubgroupcode.ClientID %>").value = subgroupcode;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function validate() {
            var sub = document.getElementById("<%=ddlSub.ClientID %>").value;
            var spec = document.getElementById("<%=txtspecification.ClientID %>").value.length;
            var gcode = document.getElementById("<%=txtgcode.ClientID %>").value.length;
            var sgcode = document.getElementById("<%=txtsubgroupcode.ClientID %>").value.length;
            var bprice = document.getElementById("<%=txtbasicprice.ClientID %>").value;

            var objs = new Array("<%=txtcategory.ClientID %>", "<%=txtcname.ClientID %>", "<%=ddldca.ClientID %>", "<%=ddlSub.ClientID %>"
                , "<%=txtgcode.ClientID %>", "<%=txtgname.ClientID %>", "<%=txtsubgroupcode.ClientID %>", "<%=txtspecification.ClientID %>", "<%=txtspecifcationname.ClientID %>", "<%=txtitemname.ClientID %>", "<%=txtbasicprice.ClientID %>", "<%=txtunits.ClientID %>",,"<%=ddlhsncode.ClientID %>");



            if (!CheckInputs(objs)) {
                return false;
            }
            if (spec != 2 || gcode != 2) {
                window.alert("GroupCode/Specification Code need minimum 2 characters");
                return false;
            }
            else if (sgcode != 3) {
                window.alert("Subgroupcode need minimum 3 characters");
                return false;
            }
            if (bprice <= 0) {
                window.alert("basic price must not be zero");
                return false;
            }

            var itemcode = document.getElementById("<%=txtcategory.ClientID %>").value + document.getElementById("<%=txtgcode.ClientID %>").value + document.getElementById("<%=txtsubgroupcode.ClientID %>").value + document.getElementById("<%=txtspecification.ClientID %>").value;
            var itemname = document.getElementById("<%=txtitemname.ClientID %>").value;

            var response = confirm("Do you want to Continue with Item Code:" + itemcode + "," + "Item Name:" + itemname);
            if (response) {
                return true;
            }
            else {
                return false;
            }
        }

        function validate1() {

            var cccode = document.getElementById("<%=ddlcccode.ClientID %>").value;
            if (cccode == "Select Cost Center") {
                window.alert("Select Cost Center");
                return false;
            }
        }

    </script>
    <script type="text/javascript" language="javascript">
        function restrictComma() {
            if (event.keyCode == 188) {
                alert("Can't Enter Comma and Less-than sign");
                event.returnValue = false;
            }
        }
    </script>
    <script type="text/javascript">
        function isNumberKeymg(evt, obj) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            var txt = obj.value.length;

            if (txt == 2) {
                return true;
            }
            else {
                window.alert("Minimum 2 characters need");
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        function isNumberKeysgcode(evt, obj) {

            var ctrl = document.getElementById("<%=txtsubgroupcode.ClientID %>");
            var charCode = (evt.which) ? evt.which : event.keyCode
            var txt = obj.value.length;

            if (txt == 3) {
                return true;
            }
            else {
                window.alert("Minimum 3 characters need");
                ctrl.focus();
                return false;
            }
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
    <script type="text/javascript" language="javascript">
        function validate2() {
            var count = 0;
            var GridView = document.getElementById("<%=Grditemspopup.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(0).children(0).checked == true) {
                        count = count + 1;
                    }
                }
                if (count != 1) {
                    alert("Please select only one item");
                    return false;
                }
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
                                        <td style="width: 90%" valign="top">
                                            <h1>Item Code Creation<a class="help" href="/view_diagram/process?res_model=hr.employee&amp;res_id=False&amp;title=Employees"
                                                title="Corporate Intelligence..."> <small>Help</small></a></h1>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="90%" colspan="4">
                                            <div style="padding-left: 20px; padding-right: 20px;">
                                                <center>
                                                    <div class="notebook-pages">
                                                        <div class="notebook-page notebook-page-active">
                                                            <table border="0" class="fields">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign="top" class="item-group">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr align="left">
                                                                                    <td colspan="2" valign="middle" class="" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Item Name
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label7" runat="server" Text="Item Name"></asp:Label>
                                                                                    </td>
                                                                                    <td width="75%" class="item item-char" valign="middle" height="">
                                                                                        <%-- <asp:TextBox ID="txtitemname" runat="server" CssClass="char" onkeydown="restrictComma();"
                                                                                                    ToolTip="Item Name" Height="20px"></asp:TextBox>--%>
                                                                                        <asp:TextBox ID="txtitemname" CssClass="m2o_search" Width="80%" Height="20px" runat="server"
                                                                                            Style="background-image: url(images/search_grey.png); background-position: right; background-repeat: no-repeat; border-color: #CBCCCC; font-size: smaller; text-transform: uppercase"
                                                                                            onkeydown="restrictComma()"></asp:TextBox>
                                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Search here.."
                                                                                            WatermarkCssClass="watermarked" TargetControlID="txtitemname" runat="server">
                                                                                        </cc1:TextBoxWatermarkExtender>
                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetItemnamelist"
                                                                                            ServicePath="cascadingDCA.asmx" TargetControlID="txtitemname" UseContextKey="True"
                                                                                            CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                            MinimumPrefixLength="1" CompletionListElementID="listPlacement" BehaviorID="dp1">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btngo" runat="server" Text="Go" CssClass="button" Height="18px" OnClick="btngo_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="ddlCC" runat="server" align="left">
                                                                                    <td valign="middle" class="" width="20%" align="center">
                                                                                        <asp:Label ID="Label4" runat="server" Text=" Cost Center"></asp:Label>
                                                                                    </td>
                                                                                    <td class="item item-selection" align="left" width="20%">
                                                                                        <asp:DropDownList ID="ddlcccode" CssClass="char" runat="server" ToolTip="Cost Center"
                                                                                            Height="20px" Width="280px">
                                                                                            <asp:ListItem Value="Select Cost Center">Select Cost Center</asp:ListItem>
                                                                                            <asp:ListItem Value="CC-33">CC-33 , Bhilai Central Store</asp:ListItem>
                                                                                            <asp:ListItem Value="CCC">CCC , Company Cost Center</asp:ListItem>
                                                                                        </asp:DropDownList>
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
                                                                        <td valign="top" class="item-group">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr align="left">
                                                                                    <td colspan="2" valign="middle" class="" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Category
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="lblccode" runat="server" Text="Category Code"></asp:Label>
                                                                                    </td>
                                                                                    <td width="99%" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtcategory" runat="server" CssClass="char" onkeyup="category();"
                                                                                            ToolTip="Category Code" MaxLength="2"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars="0123456789"
                                                                                            TargetControlID="txtcategory" />
                                                                                        <asp:ListBox ID="lstcategory" runat="server" Height="100" Width="200" class="selection selection_search readonlyfield"
                                                                                            OnSelectedIndexChanged="lstcategory_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                                        <cc1:DropDownExtender runat="server" ID="DDExtender3" TargetControlID="txtcategory"
                                                                                            DropDownControlID="lstcategory" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="lblcname" runat="server" Text="Category Name"></asp:Label>
                                                                                    </td>
                                                                                    <td width="99%" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtcname" runat="server" CssClass="char" ToolTip="Category Name"
                                                                                            Width="200px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label10" runat="server" Text="HSN Code"></asp:Label>
                                                                                    </td>
                                                                                    <td width="99%" class="item item-char" valign="middle">
                                                                                        <asp:DropDownList ID="ddlhsncode" runat="server" class="selection selection_search readonlyfield"
                                                                                            ToolTip="HSN Code" OnSelectedIndexChanged="ddlhsncode_SelectedIndexChanged" AutoPostBack="true" >
                                                                                            
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <table border="0" class="fields">
                                                                                        <tr align="left">
                                                                                            <td colspan="2" valign="middle" class=" item-separator" width="100%">
                                                                                                <div class="separator horizontal">
                                                                                                    DCA
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="label" width="1%" align="center">
                                                                                                <asp:Label ID="lbldca" runat="server" Text="DCA" CssClass="char"></asp:Label>
                                                                                            </td>
                                                                                            <td width="100%" class="item item-selection" valign="middle">
                                                                                                <asp:DropDownList ID="ddldca" runat="server" class="selection selection_search readonlyfield"
                                                                                                    OnSelectedIndexChanged="ddldca_SelectedIndexChanged" AutoPostBack="true" ToolTip="DCA">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="label" align="center">
                                                                                                <asp:Label ID="lblsdca" runat="server" Text="Sub DCA"></asp:Label>
                                                                                            </td>
                                                                                            <td width="100%" class="item item-selection" valign="middle">
                                                                                                <asp:DropDownList ID="ddlSub" runat="server" class="selection selection_search readonlyfield"
                                                                                                    ToolTip="Sub DCA">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="label" width="1%" align="center">                                                                                                
                                                                                            </td>
                                                                                            <td width="99%" class="item item-selection" valign="middle">
                                                                                                <asp:TextBox ID="txthsnremarks" class="char" runat="server" TextMode="MultiLine" CssClass="char"                                                                                             Width="200px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="ddldca" EventName="SelectedIndexChanged" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <table border="0" class="fields">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign="top" class="item-group">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr align="left">
                                                                                    <td colspan="2" valign="middle" class="" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Major Group Code
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label1" runat="server" Text="Group Code"></asp:Label>
                                                                                    </td>
                                                                                    <td width="99%" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtgcode" runat="server" CssClass="char" ToolTip="Group Code" MaxLength="2"
                                                                                            onchange="javascript:return isNumberKeymg(event,this);" TabIndex="1"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtgcode"
                                                                                            ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ" />
                                                                                        <asp:ListBox ID="lstgccode" runat="server" Height="100" Width="60" class="selection selection_search readonlyfield"
                                                                                            OnSelectedIndexChanged="lstgccode_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                                        <cc1:DropDownExtender runat="server" ID="DDE" TargetControlID="txtgcode" DropDownControlID="lstgccode" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label2" runat="server" Text="Group Name"></asp:Label>
                                                                                    </td>
                                                                                    <td width="99%" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtgname" runat="server" CssClass="char" ToolTip="Group Name"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <table border="0" class="fields">
                                                                                <tr align="left">
                                                                                    <td colspan="2" valign="middle" class=" item-separator" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Sub Group Code
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label3" runat="server" Text="Sub-Group Code" CssClass="char"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100%" class="item item-selection" valign="middle">
                                                                                        <asp:TextBox ID="txtsubgroupcode" runat="server" CssClass="char" ToolTip="Sub-Group Code"
                                                                                            MaxLength="3" onchange="javascript:return isNumberKeysgcode(event,this);" TabIndex="2"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtsubgroupcode"
                                                                                            ValidChars="1234567890" />
                                                                                        <asp:ListBox ID="lstsubgroup" runat="server" Height="100" Width="60" class="selection selection_search readonlyfield"
                                                                                            OnSelectedIndexChanged="lstsubgroup_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                                        <cc1:DropDownExtender runat="server" ID="DropDownExtender1" TargetControlID="txtsubgroupcode"
                                                                                            DropDownControlID="lstsubgroup" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                <ProgressTemplate>
                                                                    <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                                        <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                            <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                                                            <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                        </div>
                                                                    </asp:Panel>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <asp:LinkButton ID="link1" runat="server" Style="display: none"></asp:LinkButton>
                                                            <table border="0" class="fields">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign="top" class="item-group">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr align="left">
                                                                                    <td colspan="2" valign="middle" class="" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Specification Code
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label6" runat="server" Text="Specification Code"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100%" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtspecification" runat="server" CssClass="char" ToolTip="Specification"
                                                                                            MaxLength="2" onchange="javascript:return isNumberKeymg(event,this);" TabIndex="3"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtspecification"
                                                                                            ValidChars="1234567890" />
                                                                                        <%-- <asp:ListBox ID="lstspecification" runat="server" Height="100" Width="60" class="selection selection_search readonlyfield"
                                                                                            OnSelectedIndexChanged="lstspecification_SelectedIndexChanged" AutoPostBack="true">
                                                                                        </asp:ListBox>
                                                                                        <cc1:DropDownExtender runat="server" ID="DropDownExtender2" TargetControlID="txtspecification"
                                                                                            DropDownControlID="lstspecification" />--%>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label5" runat="server" Text="Specification"></asp:Label>
                                                                                    </td>
                                                                                    <td width="100%" class="item item-selection" valign="middle">
                                                                                        <asp:TextBox ID="txtspecifcationname" runat="server" CssClass="char" onkeydown="restrictComma();"
                                                                                            ToolTip="Specification Code" TabIndex="4"></asp:TextBox>
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
                                                                        <td valign="top" class="item-group">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label19" runat="server" Text="Units"></asp:Label>
                                                                                    </td>
                                                                                    <td width="99%" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtunits" runat="server" CssClass="char" ToolTip="Units" MaxLength="10"
                                                                                            Height="20px" TabIndex="5"></asp:TextBox>
                                                                                        <asp:ListBox ID="lstunits" runat="server" Height="100" Width="60" class="selection selection_search readonlyfield"
                                                                                            OnSelectedIndexChanged="lstunits_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                                        <cc1:DropDownExtender runat="server" ID="DropDownExtender2" TargetControlID="txtunits"
                                                                                            DropDownControlID="lstunits" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <table border="0" class="fields">
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">Baisc Price
                                                                                    </td>
                                                                                    <td width="100%" class="item item-selection" valign="middle">
                                                                                        <asp:TextBox ID="txtbasicprice" runat="server" CssClass="char" ToolTip="Basic Price"
                                                                                            onkeypress='return IsNumeric(event)' TabIndex="6"></asp:TextBox>
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
                                                                        <td>
                                                                            <table border="0" class="fields">
                                                                                <tr align="center">
                                                                                    <td>
                                                                                        <asp:Button ID="Btnaddnew" Height="18px" runat="server" Text="ADD TO NEW" CssClass="button"
                                                                                            OnClick="Btnaddnew_Click" OnClientClick="javascript:return validate();" />
                                                                                        &nbsp&nbsp&nbsp&nbsp&nbsp
                                                                                        <asp:Button ID="Btnaddindnt" Height="18px" runat="server" Text="ADD TO INDENT" CssClass="button"
                                                                                            OnClick="Btnaddindnt_Click" OnClientClick="javascript:return validate1();" />
                                                                                    </td>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="lblitemcode" runat="server" Text="" Visible="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
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
                                <table id="tbl2" class="view" cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" width="90%" colspan="4">
                                            <h1 style="font-size: small;">
                                                <asp:Label ID="newitem" runat="server" Text="New Item Codes" Font-Bold="True" Font-Overline="false"
                                                    Font-Underline="True"></asp:Label>
                                            </h1>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 7px;"></td>
                                    </tr>
                                    <tr>
                                        <td class="grid-content" width="90%" colspan="4">
                                            <table id="_terp_list_grid" class="grid" align="center" style="background: none;">
                                                <tr align="center">
                                                    <td width="700px" colspan="4">
                                                        <asp:GridView ID="GridView1" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                            CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                            DataKeyNames="id" Width="700px" ShowFooter="true">
                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect1" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="id" Visible="false" />
                                                                <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                <asp:BoundField DataField="units" HeaderText="Units" />
                                                                <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr id="trdlbl" runat="server">
                                                    <td align="center" width="90%" colspan="4">
                                                        <h3 style="font-size: small;">
                                                            <asp:Label ID="Label9" runat="server" Text="Price Reference" Font-Bold="True" Font-Overline="false"
                                                                Font-Underline="True"></asp:Label>
                                                        </h3>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvDetails" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                            AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                            PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                            Width="700px" GridLines="None" ShowFooter="true" OnRowDeleting="gvDetails_RowDeleting"
                                                            OnRowDataBound="gvDetails_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Supplier Name" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtSupName" runat="server" Width="200px" onkeydown="restrictComma();" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorsn" ErrorMessage="Supplier Name Required"
                                                                            Display="Dynamic" ForeColor="Red" ValidationGroup='valGroup1' ControlToValidate="txtSupName"
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Make" ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtMake" runat="server" Width="100px" onkeydown="restrictComma();" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatormk" ErrorMessage="Make Required"
                                                                            Display="Dynamic" ForeColor="Red" ValidationGroup='valGroup1' ControlToValidate="txtMake"
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRate" runat="server" onkeypress="return numericValidation(this);"
                                                                            onkeydown="restrictComma();" Width="50px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorrt" ErrorMessage="Rate" Display="Dynamic"
                                                                            ForeColor="Red" ValidationGroup='valGroup1' ControlToValidate="txtRate" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Contract No" ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtContractno" runat="server" Width="100px" onkeydown="restrictComma();" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorcn" ErrorMessage="Contract No Required"
                                                                            Display="Dynamic" ForeColor="Red" ValidationGroup='valGroup1' ControlToValidate="txtContractno"
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="E-Mail" ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtEmail" runat="server" Width="100px" onkeydown="restrictComma();" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorem" ErrorMessage="Mail Required"
                                                                            Display="Dynamic" ForeColor="Red" ValidationGroup='valGroup1' ControlToValidate="txtEmail"
                                                                            runat="server" />
                                                                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                            ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationGroup='valGroup1'
                                                                            ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnAdd" runat="server" ValidationGroup="valGroup1" OnClick="btnAdd_Click"
                                                                            ImageUrl="~/images/imgadd1.gif" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField ShowDeleteButton="true" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr id="trdesc" runat="server">
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="Description" Font-Bold="True" Font-Overline="false"
                                                            Font-Underline="True"></asp:Label>
                                                        <asp:TextBox ID="txtDescription" runat="server" Width="600px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 7px;"></td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:Button ID="Btnsbmt" Height="18px" runat="server" Text="SUBMIT" CssClass="button"
                                                OnClientClick="javascript:return validatenew();" OnClick="Btnsbmt_Click" />
                                            &nbsp&nbsp&nbsp&nbsp&nbsp
                                            <asp:Button ID="Btnrmv" Height="18px" runat="server" Text="REMOVE" CssClass="button"
                                                OnClick="Btnrmv_Click" />
                                        </td>
                                    </tr>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc1:ModalPopupExtender ID="popitems" BehaviorID="mdlitems" runat="server" TargetControlID="btnModalPopUp"
        PopupControlID="pnlindent" BackgroundCssClass="modalBackground1" DropShadow="false" />
    <asp:Panel ID="pnlindent" runat="server" Style="display: none;">
        <table width="500px" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="13" valign="bottom">
                    <img src="images/leftc.jpg">
                </td>
                <td class="pop_head" align="left" id="viewind" runat="server">
                    <div class="popclose">
                        <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png">
                    </div>
                    View Item codes
                </td>
                <td width="13" valign="bottom">
                    <img src="images/rightc.jpg">
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF">&nbsp;
                </td>
                <td height="180" valign="top" class="popcontent">
                    <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px; height: 300px;">
                        <%-- <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <table style="vertical-align: middle;" align="center" id="trgrid" runat="server">
                            <tr>
                                <td style="height: 15px;"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="Grditemspopup" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                        AutoGenerateColumns="false" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                        PagerStyle-CssClass="grid pagerbar" BorderColor="White" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        DataKeyNames="id" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="id" Visible="false" />
                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="100px" />
                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                        </Columns>
                                        <RowStyle CssClass=" grid-row char grid-row-odd" />
                                        <PagerStyle CssClass="grid pagerbar" />
                                        <HeaderStyle CssClass="grid-header" />
                                        <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr id="button" runat="server">
                                <td align="center">
                                    <asp:Label ID="lblsmsg" runat="server" CssClass="red"></asp:Label>
                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="button" OnClick="btnadd_Click"
                                        OnClientClick="javascript:return validate2();" />
                                </td>
                            </tr>
                        </table>
                        <%--   </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </td>
                <td bgcolor="#FFFFFF">&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
