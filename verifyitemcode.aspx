<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="verifyitemcode.aspx.cs"
    Inherits="verifyitemcode" Title="Item Code Creation - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function restrictComma() {
            if (event.keyCode == 188) {
                alert("Can't Enter Comma and Less-than sign");
                event.returnValue = false;
            }
        }
    </script>
    <script type="text/javascript">
        function validate() {
            var objs = new Array("<%=txtspecifcationname.ClientID %>", "<%=txtitemname.ClientID %>", "<%=txtunits.ClientID %>", "<%=txtbasicprice.ClientID %>", "<%=txtgcode.ClientID %>", "<%=txtsubgroupcode.ClientID %>", "<%=txtspecification.ClientID %>", "<%=txtremarks.ClientID %>","<%=ddlhsncode.ClientID %>");
            var bprice = document.getElementById("<%=txtbasicprice.ClientID %>").value;
            var gcode = document.getElementById("<%=txtgcode.ClientID %>").value.length;
            var subgcode = document.getElementById("<%=txtsubgroupcode.ClientID %>").value.length;
            var spec = document.getElementById("<%=txtspecification.ClientID %>").value.length;
            var subdca = document.getElementById("<%=ddlSub.ClientID %>").value;

            if (!CheckInputs(objs)) {
                return false;
            }

            if (bprice <= 0) {
                window.alert("basic price must not be zero");
                return false;
            }
            if (subdca == "Select Sub DCA") {
                window.alert("Please select SubDCA");
                return false;
            }


            if (gcode == 2) {
                if (subgcode == 3) {
                    if (spec == 2) {
                        return true;
                    }
                    else {
                        window.alert("Minimum 2 characters need");
                        return false;
                    }
                    return true;
                }
                else {
                    window.alert("Minimum 3 characters need");
                    return false;
                }
                return true;
            }
            else {
                window.alert("Minimum 2 characters need");
                return false;
            }


            document.getElementById("<%=btnupd.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">

        function closepopup() {
            $find('mdlitems').hide();

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
                                    <tr class="pagerbar">
                                        <td class="pagerbar-cell" align="right">
                                            <table class="pager-table">
                                                <tbody>
                                                    <tr>
                                                        <td class="pager-cell" style="width: 90%" valign="middle">
                                                            <div class="pager">
                                                                <div align="right">
                                                                    <asp:Label ID="Label20" CssClass="item item-char" runat="server" Text="Change Limit:"></asp:Label>
                                                                    <asp:DropDownList ID="ddlpagecount" runat="server" OnSelectedIndexChanged="ddlpagecount_SelectedIndexChanged"
                                                                        AutoPostBack="true">
                                                                        <asp:ListItem Selected="True">10</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>50</asp:ListItem>
                                                                        <asp:ListItem>100</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 100%;">
                                            <h1 style="font-size: medium;">
                                                <asp:Label ID="newitem" runat="server" Text="New Item Codes" Font-Bold="True" Font-Overline="False"
                                                    Font-Underline="True"></asp:Label></h1>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="grid-content">
                                            <table id="Table1" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                                style="background: none;" width="100%">
                                                <asp:HiddenField ID="hfdebit" runat="server" />
                                                <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="id,transaction_no"
                                                    EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" OnSelectedIndexChanged="GridView2_OnSelectedIndexChanged"
                                                    PagerStyle-CssClass="grid pagerbar" PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="100%" OnRowDeleting="GridView2_RowDeleting" OnRowEditing="Gridview2_Rowediting">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif"
                                                            ShowSelectButton="true" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:BoundField DataField="Item_code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="100px" />
                                                        <asp:BoundField DataField="Basic_Price" HeaderText="Basic Price" DataFormatString="{0:0.00}"
                                                            ItemStyle-HorizontalAlign="right" />
                                                        <asp:BoundField DataField="specification_code" HeaderText="Specification Code" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="75px" />
                                                        <asp:BoundField DataField="specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="majorgroup_code" HeaderText="Majorgroup Code" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="majorgroup_name" HeaderText="Majorgroup Name" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:CommandField ButtonType="Image" HeaderText="reject" ShowDeleteButton="true"
                                                            ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                </asp:GridView>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px;"></td>
                                    </tr>
                                    <tr id="tramend" runat="server">
                                        <td align="center" style="width: 100%;">
                                            <h1 style="font-size: medium;">
                                                <asp:Label ID="upditem" runat="server" Text="Ammended Item Codes" align="left" Font-Bold="True"
                                                    Font-Underline="True"></asp:Label></h1>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="grid-content">
                                            <table id="Table2" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                                style="background: none;" width="100%">
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="id"
                                                    EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    OnSelectedIndexChanged="GridView1_OnSelectedIndexChanged" PagerStyle-CssClass="grid pagerbar"
                                                    PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd" Width="100%" OnDataBound="GridView1_DataBound"
                                                    OnRowEditing="Gridview1_Rowediting">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Edit" ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif"
                                                            ShowSelectButton="true" />
                                                        <asp:BoundField DataField="id" Visible="false" />
                                                        <asp:BoundField DataField="Item_code" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="100px" />
                                                        <asp:BoundField DataField="Basic_Price" HeaderText="Basic Price" DataFormatString="{0:0.00}"
                                                            ItemStyle-HorizontalAlign="right" />
                                                        <asp:BoundField DataField="specification_code" HeaderText="Specification Code" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="75px" />
                                                        <asp:BoundField DataField="specification" HeaderText="Specification" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="majorgroup_code" HeaderText="Majorgroup Code" ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                    <PagerStyle CssClass="grid pagerbar" />
                                                    <HeaderStyle CssClass="grid-header" />
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                                    <PagerTemplate>
                                                        <asp:ImageButton ID="btnFirst2" runat="server" CommandArgument="First" CommandName="Page"
                                                            Height="15px" ImageUrl="~/images/pager_first.png" OnCommand="btnFirst2_Command" />
                                                        &nbsp;
                                                        <asp:ImageButton ID="btnPrev2" runat="server" CommandArgument="Prev" CommandName="Page"
                                                            Height="15px" ImageUrl="~/images/pager_left.png" OnCommand="btnPrev2_Command" />
                                                        <asp:Label ID="lblpages" runat="server" CssClass="item item-char" Height="15px" Text=""></asp:Label>
                                                        of
                                                        <asp:Label ID="lblCurrent" runat="server" CssClass="item item-char" Height="15px"
                                                            Text="Label"></asp:Label>
                                                        <asp:ImageButton ID="btnNext2" runat="server" CommandArgument="Next" CommandName="Page"
                                                            Height="15px" ImageUrl="~/images/pager_right.png" OnCommand="btnNext2_Command" />
                                                        &nbsp;
                                                        <asp:ImageButton ID="btnLast2" runat="server" CommandArgument="Last" CommandName="Page"
                                                            Height="15px" ImageUrl="~/images/pager_last.png" OnCommand="btnLast2_Command" />
                                                    </PagerTemplate>
                                                </asp:GridView>
                                            </table>
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
    <cc1:ModalPopupExtender ID="popitems" BehaviorID="mdlitems" runat="server" TargetControlID="btnModalPopUp"
        PopupControlID="pnlitems" BackgroundCssClass="modalBackground1" DropShadow="false" />
    <asp:Panel ID="pnlitems" runat="server" Style="display: none;">
        <table width="700px" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="13" valign="bottom">
                    <img src="images/leftc.jpg" />
                </td>
                <td class="pop_head" align="left" id="approveind" runat="server">
                    <div class="popclose">
                        <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png" />
                    </div>
                    <asp:Label ID="lbltitle" runat="server" Text="Approve Item Codes"></asp:Label>
                </td>
                <td width="13" valign="bottom">
                    <img src="images/rightc.jpg" />
                </td>
            </tr>
            <tr id="tritemcode" runat="server">
                <td bgcolor="#FFFFFF">&nbsp;
                </td>
                <td height="180" valign="top" class="popcontent">
                    <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px; height: 280px;">
                        <table style="vertical-align: middle;" align="center" width="900px">
                            <tr>
                                <td width="100%" colspan="6">
                                    <div style="padding-left: 20px; padding-right: 20px;">
                                        <center>
                                            <div class="notebook-pages">
                                                <div class="notebook-page notebook-page-active">
                                                    <asp:UpdatePanel ID="Upd" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="Upd" runat="server">
                                                                <ProgressTemplate>
                                                                    <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                                        <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px; left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                            <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                        </div>
                                                                    </asp:Panel>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <table border="0" class="fields" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td width="50%" valign="top" class="item-group">
                                                                            <table border="0" class="fields" width="750px">
                                                                                <tr align="center" style="text-decoration: underline;">
                                                                                    <td colspan="2" valign="middle" class="item-separator" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Category
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="30%" align="center">
                                                                                        <asp:Label ID="lblccode" runat="server" Text="Category Code"></asp:Label>
                                                                                    </td>
                                                                                    <td class="item item-char" valign="middle">
                                                                                        <span class="filter_item">
                                                                                            <asp:TextBox ID="txtcode" runat="server" CssClass="char" ToolTip="Category Code"
                                                                                                MaxLength="2"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars="0123456789"
                                                                                                TargetControlID="txtcode" />
                                                                                            <asp:ListBox ID="lstcategory" runat="server" Height="100" Width="60" class="selection selection_search readonlyfield"
                                                                                                OnSelectedIndexChanged="lstcategory_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                                            <cc1:DropDownExtender runat="server" ID="DDExtender3" TargetControlID="txtcode" DropDownControlID="lstcategory" />
                                                                                        </span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="lblcname" runat="server" Text="Category Name"></asp:Label>
                                                                                    </td>
                                                                                    <td width="" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtcname" runat="server" CssClass="char" ToolTip="Category Name"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label10" runat="server" Text="HSN Code"></asp:Label>
                                                                                    </td>
                                                                                    <td width="99%" class="item item-char" valign="middle">
                                                                                        <asp:DropDownList ID="ddlhsncode" runat="server" class="selection selection_search readonlyfield"
                                                                                            ToolTip="HSN Code" OnSelectedIndexChanged="ddlhsncode_SelectedIndexChanged" AutoPostBack="true">
                                                                                            
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <table border="0" class="fields">
                                                                                <tr align="center" style="text-decoration: underline;">
                                                                                    <td colspan="2" valign="middle" class="item-separator" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            DCA
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="30%" align="center">
                                                                                        <asp:Label ID="lbldca" runat="server" Text="DCA" CssClass="char"></asp:Label>
                                                                                    </td>
                                                                                    <td width="" class="item item-selection" valign="middle">
                                                                                        <asp:DropDownList ID="ddldca" runat="server" class="selection selection_search readonlyfield"
                                                                                            OnSelectedIndexChanged="ddldca_SelectedIndexChanged" AutoPostBack="true" ToolTip="DCA">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" align="center">
                                                                                        <asp:Label ID="lblsdca" runat="server" Text="Sub DCA"></asp:Label>
                                                                                    </td>
                                                                                    <td width="" class="item item-selection" valign="middle">
                                                                                        <asp:DropDownList ID="ddlSub" runat="server" class="selection selection_search readonlyfield"
                                                                                            ToolTip="Sub DCA">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center"></td>
                                                                                    <td width="99%" class="item item-selection" valign="middle">
                                                                                        <asp:TextBox ID="txthsnremarks" class="char" runat="server" CssClass="char" TextMode="MultiLine" Width="200px"></asp:TextBox>
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
                                                                                <tr align="center" style="text-decoration: underline;">
                                                                                    <td colspan="2" valign="middle" class="" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Major Group Code
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="30%" align="center">
                                                                                        <asp:Label ID="Label1" runat="server" Text="Group Code"></asp:Label>
                                                                                    </td>
                                                                                    <td width="%" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtgcode" runat="server" CssClass="char" ToolTip="Group Code" MaxLength="2"
                                                                                            onchange="javascript:return isNumberKeymg(event,this);"></asp:TextBox>
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
                                                                                    <td width="" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtgname" runat="server" CssClass="char" ToolTip="Group Name"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <table border="0" class="fields">
                                                                                <tr align="center" style="text-decoration: underline;">
                                                                                    <td colspan="2" valign="middle" class=" item-separator" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Sub Group Code
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="30%" align="center">
                                                                                        <asp:Label ID="Label3" runat="server" Text="Sub-Group Code" CssClass="char"></asp:Label>
                                                                                    </td>
                                                                                    <td width="" class="item item-selection" valign="middle">
                                                                                        <asp:TextBox ID="txtsubgroupcode" runat="server" CssClass="char" ToolTip="Sub-Group Code"
                                                                                            MaxLength="3" onchange="javascript:return isNumberKeysgcode(event,this);"></asp:TextBox>
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
                                                            <table border="0" class="fields">
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2" valign="top" class="item-group" width="50%">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr align="center" style="text-decoration: underline;">
                                                                                    <td colspan="4" valign="middle" class="" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Specification
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="30%" align="center">
                                                                                        <asp:Label ID="Label6" runat="server" Text="Specification Code"></asp:Label>
                                                                                    </td>
                                                                                    <td width="" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtspecification" runat="server" CssClass="char" ToolTip="Specification"
                                                                                            MaxLength="2" onchange="javascript:return isNumberKeymg(event,this);"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtspecification"
                                                                                            ValidChars="1234567890" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr>
                                                                                    <td class="label" width="30%" align="center">
                                                                                        <asp:Label ID="Label5" runat="server" Text="Specification"></asp:Label>
                                                                                    </td>
                                                                                    <td width="" class="item item-selection" valign="middle">
                                                                                        <asp:TextBox ID="txtspecifcationname" runat="server" CssClass="char" onkeydown="restrictComma();"
                                                                                            ToolTip="Specification Name"></asp:TextBox>
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
                                                                                <tr align="center" style="text-decoration: underline;">
                                                                                    <td colspan="2" valign="middle" class="" width="100%">
                                                                                        <div class="separator horizontal">
                                                                                            Item Name
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label" width="30%" align="center">
                                                                                        <asp:Label ID="Label7" runat="server" Text="Item Name"></asp:Label>
                                                                                    </td>
                                                                                    <td width="" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtitemname" runat="server" CssClass="char" onkeydown="restrictComma();"
                                                                                            ToolTip="Item Name"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" class="item-group">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr>
                                                                                    <td class="label" width="1%" align="center">
                                                                                        <asp:Label ID="Label19" runat="server" Text="Units"></asp:Label>
                                                                                    </td>
                                                                                    <td width="99%" class="item item-char" valign="middle">
                                                                                        <asp:TextBox ID="txtunits" runat="server" CssClass="char" ToolTip="Units" MaxLength="10"
                                                                                            Height="20px"></asp:TextBox>
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
                                                                                            onkeypress='return IsNumeric(event)'></asp:TextBox>
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
                                                                                <tr align="left">
                                                                                    <td colspan="2" class="item item-char" valign="middle">
                                                                                        <div class="separator horizontal">
                                                                                            Remarks
                                                                                        </div>
                                                                                        <asp:TextBox ID="txtremarks" runat="server" CssClass="char" onkeydown="restrictComma();"
                                                                                            ToolTip="Remarks"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <table id="tblrem" runat="server">
                                                                <tr align="center" style="text-decoration: underline;">
                                                                    <td colspan="2" valign="middle" class="" width="100%">
                                                                        <div class="separator horizontal">
                                                                            Price Reference
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 750px">
                                                                    <td colspan="2">
                                                                        <asp:GridView ID="GVSupplier" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                            DataKeyNames="id" Width="750px" ShowFooter="true" OnRowDataBound="GVSupplier_RowDataBound">
                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                            <Columns>
                                                                                <asp:BoundField DataField="supplier_name" HeaderText="Supplier Name" />
                                                                                <asp:BoundField DataField="Make" HeaderText="Make" />
                                                                                <asp:BoundField DataField="Rate" HeaderText="Rate" DataFormatString="{0:0.00}" />
                                                                                <asp:BoundField DataField="Contract_no" HeaderText="Contract No" />
                                                                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center" style="text-decoration: underline;">
                                                                    <td colspan="2" valign="middle" class="" width="100%">
                                                                        <div class="separator horizontal">
                                                                            Remarks
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:GridView ID="GVUsers" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                            RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                            DataKeyNames="Users" Width="750px" ShowFooter="true" OnRowDataBound="GVUsers_RowDataBound">
                                                                            <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                            <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                            <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                            <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                                                <asp:BoundField DataField="Role" HeaderText="Role" />
                                                                                <asp:BoundField DataField="" HeaderText="Name" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <tr id="button" runat="server" align="center">
                                                    <td width="90%" colspan="4">
                                                        <asp:Button ID="btnupd" Height="18px" runat="server" Text="Update" CssClass="button"
                                                            OnClick="btnUpdate_Click" OnClientClick="javascript:return validate();" />
                                                        <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                                    </td>
                                                </tr>
                                            </div>
                                        </center>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td bgcolor="#FFFFFF">&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
