<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Lost or Damaged Report.aspx.cs"
    Inherits="Lost_or_Damaged_Report" Title="Lost or Damaged Report" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript">

        function showToolTip(e, text) {
            if (document.all) e = event;
            var obj = document.getElementById('bubble_tooltip');
            var obj2 = document.getElementById('bubble_tooltip_content');
            var obj3 = document.getElementById('bubble_tooltip_content');
            obj2.innerHTML = text;
            // obj3.innerHTML=text2;
            obj.style.display = 'block';
            var st = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
            if (navigator.userAgent.toLowerCase().indexOf('safari') >= 0) st = 0;
            var leftPos = e.clientX - 100;
            if (leftPos < 0) leftPos = 0;
            obj.style.left = leftPos + 'px';
            obj.style.top = e.clientY - obj.offsetHeight - 1 + st + 'px';
        }

        function hideToolTip() {
            document.getElementById('bubble_tooltip').style.display = 'none';

        }
        function extender(source, eventArgs) {
            //alert(eventArgs.get_text())
            var text = eventArgs.get_value();

            if (text != "") {
                showToolTip(event, eventArgs.get_value());
                CCcodeCheckerTimer = setTimeout("hideToolTip();", 4000);
            }
            else {
                showToolTip(event, "0");
                CCcodeCheckerTimer = setTimeout("hideToolTip();", 4000);
            }
        }
        
        
    </script>
    <script type="text/javascript" language="javascript">
        function rectionDoller() {
            if (event.keyCode == 50 && event.keyCode == 189) {
                alert("Can't Enter @ and underscore");
                event.returnvalue = false;
                return false;
            }
        }
   
    </script>
    <script language="javascript">
        function searchvalidate() {
            var Search = document.getElementById("<%=txtSearch.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=txtSearch.ClientID %>");
            if (Search == "Please search here..") {
                window.alert("Please Select an item");
                Searchctrl.focus();
                return false;
            }
            GridView = document.getElementById("<%=GridView1.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(1).innerHTML == Search) {
                        window.alert("Selected Item is already added");
                        Search = "";
                        Searchctrl.focus();
                        return false;
                    }
                }
            }
            var itemcode = document.getElementById("<%=txtSearch.ClientID %>").value.substring(0, 1);
            if (GridView != null) {
                var itemtype = GridView.rows(1).cells(1).innerHTML.substring(0, 1);
                if (itemtype == 1) {
                    if (itemcode != 1) {
                        alert("You are not able to add multiple DCA items");
                        document.getElementById("<%=txtSearch.ClientID %>").value = "";
                        return false;
                    }
                }
                if (itemtype != 1) {
                    if (itemcode == 1) {
                        alert("You are not able to add multiple DCA items");
                        document.getElementById("<%=txtSearch.ClientID %>").value = "";
                        return false;
                    }
                }
            }
            document.getElementById("<%=btnadd.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script language="javascript">
        function validate() {
            var itemtypes = document.getElementById("<%=ddlindenttype.ClientID %>").value;

            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    var itemcode = GridView.rows(1).cells(1).innerHTML.substring(0, 1);
                    if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                        window.alert("verify");
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(8).children[0].selectedIndex == 0) {
                        window.alert("Select Item Type");
                        GridView.rows(rowCount).cells(8).children[0].focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(9).children[0].selectedIndex == 0) {
                        window.alert("Select Item Status");
                        GridView.rows(rowCount).cells(9).children[0].focus();
                        return false;
                    }
                    else if (GridView.rows(rowCount).cells(10).children[0].value == "") {
                        window.alert("Enter Remarks");
                        GridView.rows(rowCount).cells(10).children[0].focus();
                        return false;
                    }
                    else if (itemtypes != "1") {
                        if (GridView.rows(rowCount).cells(7).children[0].value == "" || GridView.rows(rowCount).cells(7).children[0].value == 0) {
                            window.alert("Enter Quantity");
                            GridView.rows(rowCount).cells(7).children[0].value = "";
                            GridView.rows(rowCount).cells(7).children[0].focus();
                            return false;
                        }
                    }
                    if (itemcode == "1") {
                        if (itemtypes != "1") {
                            window.alert("Select Asset type");
                            document.getElementById("<%=ddlindenttype.ClientID %>").focus();
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(7).children[0].value != 1) {
                            window.alert("Asset Item is one only");
                            GridView.rows(rowCount).cells(7).children[0].value = "1";
                            GridView.rows(rowCount).cells(7).children[0].disabled = true;
                            return false;
                        }
                    }
                }
            }
            var date = document.getElementById("<%=txtdate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdate.ClientID %>");
            if (date == "Enter Date..") {
                window.alert("Enter Date");
                datectrl.focus();
                return false;
            }

            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;

        }
        function Deletevalidate() {
            if (confirm("Are you sure,you want to delete selected records ?") == true)
                return true;
            else
                return false;
        }
        function checkqty() {
            //debugger;
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
            for (var rowCount = 1; rowCount < GridView1.rows.length-1; rowCount++) {
                var qty = GridView1.rows(rowCount).cells(7).children[0].value;
                var Avlqty = GridView1.rows(rowCount).cells(11).children[0].value;
                if (parseFloat(qty) > parseFloat(Avlqty)) {
                    window.alert("You are not able to put excess quantity than avaliable quantity");
                    GridView1.rows(rowCount).cells(7).children[0].value = "";
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript">
        function checkDate(sender, args) {
            //debugger;
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }
            var month = new Array();
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";
            var mmm = month[today.getMonth()];
            today = dd + '-' + mmm + '-' + yyyy;
            var str1 = document.getElementById("<%=txtdate.ClientID %>").value;
            var str2 = today;
            var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
            var dt1 = str1.substring(0, 2);
            var dt2 = str2.substring(0, 2);
            var yr1 = str1.substring(7, 11);
            var yr2 = str2.substring(7, 11);
            for (var i = 0; i < args.length; i++) {
                var month = str2.substring(3, 6);
                var month1 = str1.substring(3, 6);
                if (args[i] == month) {
                    var month = parseInt(i + 1);
                    var date2 = yr2 + "-" + month + "-" + dt2;

                }
                if (args[i] == month1) {
                    var month1 = parseInt(i + 1);
                    var date1 = yr1 + "-" + month1 + "-" + dt1;
                }

            }
            var one_day = 1000 * 60 * 60 * 24;
            var x = date1.split("-");
            var y = date2.split("-");

            var date4 = new Date(x[0], (x[1] - 1), x[2]);
            var date3 = new Date(y[0], (y[1] - 1), y[2]);

            var month1 = x[1] - 1;
            var month2 = y[1] - 1;
            //debugger;
            _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
            if (date4 > date3) {
                alert("Invalid Future Date Selection");
                document.getElementById("<%=txtdate.ClientID %>").value = "";
                return false;
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="up" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                    <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                        left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                        <%-- <div id="divFeedback" runat="server" class="popup-div-front">--%>
                                        <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                    </div>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table width="100%">
                            <tr>
                                <td>
                                    <h1>
                                        <a id="shortcut_add_remove" class="shortcut-remove"></a>Lost or Damaged Report<a
                                            class="help" href="" title=""><small>Help</small> </a>
                                    </h1>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <div id="body_form">
                                                    <div>
                                                        <div id="server_logs">
                                                        </div>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <div id="search_filter_data">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr>
                                                                                    <td class="item search_filters item-filtersgroup" valign="top">
                                                                                        <div class="filters-group">
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="label search_filters search_fields" colspan="6" align="center">
                                                                                        <table class="search_table">
                                                                                            <tbody>
                                                                                                <tr height="73px">
                                                                                                    <td colspan="1">
                                                                                                    </td>
                                                                                                    <td colspan="1">
                                                                                                        <div id="bubble_tooltip">
                                                                                                            <div class="bubble_top">
                                                                                                            </div>
                                                                                                            <div class="bubble_middle">
                                                                                                                <span id="bubble_tooltip_content"></span>
                                                                                                            </div>
                                                                                                            <div class="bubble_bottom">
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 100px">
                                                                                                        <asp:DropDownList ID="ddlindenttype" CssClass="char" runat="server" OnSelectedIndexChanged="ddlindenttype_SelectedIndexChanged"
                                                                                                            AutoPostBack="true">
                                                                                                            <asp:ListItem Value="Select Type">Select Type</asp:ListItem>
                                                                                                            <asp:ListItem Value="1">Assets</asp:ListItem>
                                                                                                            <asp:ListItem Value="2">Semi Assets/Consumables</asp:ListItem>
                                                                                                            <asp:ListItem Value="3">Consumables</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle" style="width: 600px">
                                                                                                        <asp:TextBox ID="txtSearch" CssClass="m2o_search" Width="600px" Height="20px" runat="server"
                                                                                                            Style="background-image: url(images/search_grey.png); background-position: right;
                                                                                                            background-repeat: no-repeat; border-color: #CBCCCC; font-size: smaller;"></asp:TextBox>
                                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="TransferOutSearch"
                                                                                                            ServicePath="cascadingDCA.asmx" TargetControlID="txtSearch" UseContextKey="True"
                                                                                                            CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                                            MinimumPrefixLength="1" CompletionListElementID="listPlacement" OnClientItemSelected="extender">
                                                                                                        </cc1:AutoCompleteExtender>
                                                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="Please search here.."
                                                                                                            WatermarkCssClass="watermarked" TargetControlID="txtSearch" runat="server">
                                                                                                        </cc1:TextBoxWatermarkExtender>
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txtdate" Font-Size="Small" ToolTip="Date" runat="server" Style="width: 130px;
                                                                                                            height: 20px; vertical-align: middle"></asp:TextBox>
                                                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Enter Date.."
                                                                                                            WatermarkCssClass="watermarked" TargetControlID="txtdate" runat="server">
                                                                                                        </cc1:TextBoxWatermarkExtender>
                                                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
                                                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                                            PopupButtonID="txtdate" OnClientDateSelectionChanged="checkDate" >
                                                                                                        </cc1:CalendarExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td class="label search_filters search_fields" colspan="4">
                                                                                        <table class="search_table">
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="view_form_options" width="100%">
                                                                        <table width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <div class="box-a list-a">
                                                                            <div class="inner">
                                                                                <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr class="pagerbar" id="trbtns" runat="server">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                                <table class="pager-table">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td class="pager-cell">
                                                                                                                <h2>
                                                                                                                    Lost/Damaged Report</h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                                <img src="/images/load.gif" width="16" height="16" title="loading...">
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button">
                                                                                                                <asp:Button ID="btnadd" Width="50px" PostBackUrl="" Height="" runat="server" Text="Add"
                                                                                                                    CssClass="button" OnClientClick="javascript:return searchvalidate()" OnClick="btnadd_Click" />
                                                                                                            </td>
                                                                                                            <td class="pager-cell-button">
                                                                                                                <asp:Button ID="btndelete" Width="60px" OnClick="btnDelete_Click" OnClientClick="return Deletevalidate();"
                                                                                                                    Height="" runat="server" Text="Delete" CssClass="button" />
                                                                                                            </td>
                                                                                                            <td class="pager-cell" style="width: 90%" valign="middle">
                                                                                                                <div class="pager">
                                                                                                                    <div align="right">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="grid-content">
                                                                                                <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" cellpadding="0"
                                                                                                    style="background: none;">
                                                                                                    <asp:GridView ID="GridView1" Width="100%" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        DataKeyNames="id" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
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
                                                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" />
                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" />
                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" />
                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtqty" runat="server" onkeyup="checkqty();" Width="50px" Text='<%#Eval("Quantity")%>'></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Item Type">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:DropDownList ID="ddltype" runat="server" Width="60px">
                                                                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                                                        <asp:ListItem Value="1">New Stock</asp:ListItem>
                                                                                                                        <asp:ListItem Value="2">Stock</asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Item Status">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:DropDownList ID="ddlreporttype" runat="server" Width="60px" SelectedValue='<%# Bind("type") %>'>
                                                                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                                                                        <asp:ListItem>Lost</asp:ListItem>
                                                                                                                        <asp:ListItem>Damaged</asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtremarks" runat="server" Width="200px" onkeypress="return rectionDoller()"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="h2" runat="server" Value='<%#Eval("avlqty")%>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="pagerbar" id="trbtn" runat="server">
                                                                                            <td class="pager-cell-button" align="center">
                                                                                                <asp:Button ID="btnsubmit" Width="120px" Height="20px" runat="server" Text="Submit"
                                                                                                    CssClass="button" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
