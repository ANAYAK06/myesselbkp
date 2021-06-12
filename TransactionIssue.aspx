<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="TransactionIssue.aspx.cs"
    Inherits="TransactionIssue" Title="Daily Issue - Essel Projects Pvt.Ltd." %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
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

        function IsNumeric1(evt) {
            GridView = document.getElementById("<%=GridView1.ClientID %>");
            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                }
            }
        }
  
    </script>
    <script language="javascript">
        function validate() {
            GridView = document.getElementById("<%=GridView1.ClientID %>");
            var date = document.getElementById("<%=txtdate.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdate.ClientID %>");
            var desc = document.getElementById("<%=txtdesc.ClientID %>").value;
            var descctrl = document.getElementById("<%=txtdesc.ClientID %>");

            for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                    window.alert("Please verify");
                    return false;
                }
                else if (GridView.rows(rowCount).cells(8).children[0].value == "" || GridView.rows(rowCount).cells(8).children[0].value == 0) {
                    window.alert("Please Enter Quantity");
                    GridView.rows(rowCount).cells(8).children[0].focus();
                    GridView.rows(rowCount).cells(8).children[0].value = "";
                    return false;
                }
                else if (parseInt(GridView.rows(rowCount).cells(9).children[0].value) < parseInt(GridView.rows(rowCount).cells(8).children[0].value)) {
                    window.alert("Inavlid");
                    GridView.rows(rowCount).cells(8).children[0].focus();
                    GridView.rows(rowCount).cells(8).children[0].value = "";
                    return false;
                }



            }

            if (date == "") {
                window.alert("Enter Date");
                datectrl.focus();
                return false;
            }
            if (desc == "") {
                window.alert("Enter Description");
                descctrl.focus();
                return false;
            }

            document.getElementById("<%=btnSave.ClientID %>").style.display = 'none';
            return true;
        }
        function searchvalidate() {
            var Search = document.getElementById("<%=txtSearch.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=txtSearch.ClientID %>");
            if (Search == "Please search here..") {
                window.alert("Please Select an item");
                Searchctrl.focus();
                return false;
            }

        }
        function Deletevalidate() {
            if (confirm("Are you sure,you want to delete selected records ?") == true)
                return true;
            else
                return false;
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr>
            <td style="width: 150px; height: 100%;" valign="top">
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="wrap">
                                <table class="view" cellpadding="0" cellspacing="0" border="0" width="90%">
                                    <tr>
                                        <td align="center">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
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
                                                    <div id="body_form">
                                                        <div>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 90%" valign="top" align="left">
                                                                        <h1>
                                                                            Daily Issues <a class="help" href="" title="Raise Indent"><small>Help</small></a></h1>
                                                                        <%-- <div class="wrapper action-buttons" style="padding-left: 20px;">
                                                                </div>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <div id="search_filter_data">
                                                                            <table border="0" class="fields" width="100%">
                                                                                <tr>
                                                                                    <td class="item search_filters item-filtersgroup" valign="top">
                                                                                        <div class="filters-group">
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="label search_filters search_fields" colspan="4" align="center">
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
                                                                                                            <%--  <asp:ListItem Value="1">Assets</asp:ListItem>
                                                                                                                    <asp:ListItem Value="2">Semi Assets/Consumables</asp:ListItem>--%>
                                                                                                            <asp:ListItem Value="3">Consumables</asp:ListItem>
                                                                                                            <asp:ListItem Value="4">BOUGHT OUT ITEMS</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle" style="width: 600px">
                                                                                                        <asp:TextBox ID="txtSearch" CssClass="m2o_search" Width="100%" Height="20px" runat="server"
                                                                                                            Style="background-image: url(images/search_grey.png); background-position: right;
                                                                                                            background-repeat: no-repeat; border-color: #CBCCCC; font-size: smaller;"></asp:TextBox>
                                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionListfordailyissue"
                                                                                                            ServicePath="cascadingDCA.asmx" TargetControlID="txtSearch" UseContextKey="True"
                                                                                                            CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                                            MinimumPrefixLength="1" CompletionListElementID="listPlacement" OnClientItemSelected="extender"
                                                                                                            BehaviorID="dp1">
                                                                                                        </cc1:AutoCompleteExtender>
                                                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="Please search here.."
                                                                                                            WatermarkCssClass="watermarked" TargetControlID="txtSearch" runat="server">
                                                                                                        </cc1:TextBoxWatermarkExtender>
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
                                                                                <tr>
                                                                                    <td class="item search_filters item-filtersgroup" for="" valign="top">
                                                                                        <div class="filters-group">
                                                                                            <div style="display: none;">
                                                                                                <div class="filter-a">
                                                                                                    <button type="button" class="filter_with_icon active" title="">
                                                                                                        <img src="images/personal+.png" width="16" height="16" alt="">
                                                                                                        Active
                                                                                                    </button>
                                                                                                    <input style="display: none;" type="checkbox" id="filter_241" name="" class="grid-domain-selector"
                                                                                                        onclick="" value="" title="">
                                                                                                </div>
                                                                                            </div>
                                                                                            <table border="0" class="fields" width="100%">
                                                                                                <tbody>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <div class="box-a list-a">
                                                                            <div class="inner">
                                                                                <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr class="pagerbar">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                                <table class="pager-table">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td class="pager-cell">
                                                                                                                <h2>
                                                                                                                    Add items to indent:-</h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                            </td>
                                                                                                            <td class="pager-cell">
                                                                                                                <asp:Button ID="btnadd" runat="server" Text="Add" Height="18px" CssClass="button"
                                                                                                                    OnClientClick="javascript:return searchvalidate()" OnClick="btnadd_Click" />&nbsp;&nbsp;
                                                                                                            </td>
                                                                                                            <td class="pager-cell">
                                                                                                                <asp:Button ID="btnDelete" Height="18px" runat="server" Text="Delete" CssClass="button"
                                                                                                                    OnClick="btnDelete_Click" OnClientClick="return Deletevalidate();" />
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
                                                                                                <table id="_terp_list_grid" class="grid" width="100%" align="center" style="background: none;">
                                                                                                    <asp:GridView ID="GridView1" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        DataKeyNames="id" ShowFooter="true">
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
                                                                                                            <asp:BoundField DataField="basic_price" HeaderText="Basic Price" />
                                                                                                            <asp:BoundField DataField="units" HeaderText="Units" />
                                                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtqty" runat="server" Width="50px" onkeypress='IsNumeric1(event)'></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="h1" runat="server" Value='<%#Eval("Available qty")%>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tbldesc" runat="server">
                                                                                            <td>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td width="150px">
                                                                                                            <table class="search_table" width="80%">
                                                                                                                <tr>
                                                                                                                    <td class="item item-selection" valign="middle" width="">
                                                                                                                        <asp:Label ID="Label3" runat="server" Text="Issued Date"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                                        <asp:TextBox ID="txtdate" runat="server" Font-Size="Small" Style="width: 130px; height: 20px;
                                                                                                                            vertical-align: middle"></asp:TextBox>
                                                                                                                        <%-- <img alt="" src="images/stock_calendar.png" style="width: 13px; height: 15px; vertical-align: middle;"
                                                                                                    id="Img2" />--%>
                                                                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" CssClass="cal_Theme1"
                                                                                                                            FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" PopupButtonID="txtdate" TargetControlID="txtdate">
                                                                                                                        </cc1:CalendarExtender>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td width="200px">
                                                                                                            <table align="center" class="search_table" width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left" class="item item-selection" colspan="2" width="">
                                                                                                                        <asp:Label ID="lbldate" runat="server" Text="Description"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" class="item item-selection" valign="middle">
                                                                                                                        <asp:TextBox ID="txtdesc" runat="server" CssClass="filter_item" MaxLength="50" TextMode="MultiLine"
                                                                                                                            ToolTip="Description" Width="450px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="pagerbar">
                                                                                            <td class="pagerbar-cell" align="right">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="btnvisible" runat="server">
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnSave" Height="18px" runat="server" Text="Save" CssClass="button"
                                                                                                    OnClick="btnSave_Click" OnClientClick="javascript:return validate()" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
