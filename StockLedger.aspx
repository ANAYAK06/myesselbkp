<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="StockLedger.aspx.cs" Inherits="StockLedger" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="WarehouseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function restrictComma() {
            if (event.keyCode == 188) {
                alert("Can't Enter Comma and Less-than sign");
                event.returnValue = false;
            }
        } 
    </script>
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function check() {
            var CCCode = document.getElementById("<%=ddlcccode.ClientID %>").value;
            var CCCodectrl = document.getElementById("<%=ddlcccode.ClientID %>");
            var Search = document.getElementById("<%=txtitemname.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=txtitemname.ClientID %>");
            var objs = new Array("<%=txtitemname.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtTo.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (CCCode == "Select Cost Center" || CCCode == "") {
                window.alert("Select Cost Center");
                CCCodectrl.focus();
                return false;
            }
            if (Search == "Search here..") {
                window.alert("Please Select an item");
                Searchctrl.focus();
                return false;
            }
        }       
 
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr id="Tr1" runat="server">
            <td style="width: 150px; height: 100%;" valign="top">
                <WarehouseMenu:Menu ID="ww" runat="server" />
            </td>
            <td valign="top" align="center">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                        <ProgressTemplate>
                                            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                                                <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                    left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                    <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                </div>
                                            </asp:Panel>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <div class="wrap">
                                        <table class="view" cellpadding="0" cellspacing="0" border="0" width="90%">
                                            <tr align="center">
                                                <td align="center">
                                                    <div id="body_form">
                                                        <div>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 25%" valign="top" align="left">
                                                                        <h1>
                                                                            Stock Ledger<a class="help" href="" title="Stock Ledger"></a></h1>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="item item-char" valign="middle" align="center" height="25px" style="width: 119px">
                                                                        <asp:Label ID="Label1" runat="server" Text="Select Date: " CssClass="eslbl" Font-Size="Medium"
                                                                            Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" height="25px" align="center" style="width: 40px">
                                                                        <asp:Label ID="Label2" runat="server" Text="From " CssClass="eslbl" Font-Size="Small"
                                                                            Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-selection" valign="middle" align="left" style="width: 100px">
                                                                        <asp:TextBox ID="txtfrom" runat="server" Style="width: 130px; height: 20px; vertical-align: middle"
                                                                            ToolTip="From Date"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                            PopupButtonID="txtfrom">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" height="25px" align="center">
                                                                        <asp:Label ID="Label3" runat="server" Text="To " CssClass="eslbl" Font-Size="Small"
                                                                            Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-selection" valign="middle" align="left">
                                                                        <asp:TextBox ID="txtTo" runat="server" Style="width: 130px; height: 20px; vertical-align: middle"
                                                                            ToolTip="To Date"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                            PopupButtonID="txtTo">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="item item-selection" valign="middle" height="25px" style="width: 119px"
                                                                        align="center">
                                                                        <asp:Label ID="lblcccode" runat="server" Text="CC Code:" Font-Size="Small" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-selection" valign="middle" height="25px" colspan="2">
                                                                        <asp:DropDownList ID="ddlcccode" CssClass="filter_item" runat="server" Height="20px"
                                                                            Width="280px" OnSelectedIndexChanged="ddlcccode_SelectedIndexChanged" AutoPostBack="true"
                                                                            ToolTip="Cost Centre">
                                                                        </asp:DropDownList>
                                                                        <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                            ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="codename"
                                                                            PromptText="Select Cost Center">
                                                                        </cc1:CascadingDropDown>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="item item-selection" valign="middle" height="25px" style="width: 119px"
                                                                        align="center">
                                                                        <asp:Label ID="Label4" runat="server" Text="Select Item:" Font-Size="Small" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td colspan="2" class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtitemname" CssClass="m2o_search" Width="135%" Height="22px" runat="server"
                                                                            Style="background-image: url(images/search_grey.png); background-position: right;
                                                                            background-repeat: no-repeat; border-color: #CBCCCC; font-size: smaller; text-transform: uppercase"
                                                                            onkeydown="restrictComma()" ToolTip="ItemName"></asp:TextBox>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Search here.."
                                                                            WatermarkCssClass="watermarked" TargetControlID="txtitemname" runat="server">
                                                                        </cc1:TextBoxWatermarkExtender>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetStock"
                                                                            ServicePath="cascadingDCA.asmx" TargetControlID="txtitemname" UseContextKey="True"
                                                                            CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                            MinimumPrefixLength="1" CompletionListElementID="listPlacement" BehaviorID="dp1">
                                                                        </cc1:AutoCompleteExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="item item-selection" valign="middle" height="25px" style="width: 119px"
                                                                        align="center">
                                                                        <asp:Label ID="Label5" runat="server" Text="Transaction Type:" Font-Size="Small"
                                                                            Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Transction Type"
                                                                            AutoPostBack="true" RepeatDirection="Horizontal" runat="server" onclick="javascript:return check();"
                                                                            CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                                                            <asp:ListItem>With Transfer</asp:ListItem>
                                                                            <asp:ListItem>Without Transfer</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trnorcrds" runat="server" valign="middle">
                                                                    <td class="item item-selection" valign="middle" height="25px" align="center" colspan="4">
                                                                        <asp:Label ID="Label6" runat="server" Text="No Records Found" Font-Size="Medium"
                                                                            Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                                <tr id="grid" runat="server" valign="middle">
                                                                    <td align="center" colspan="5">
                                                                        <asp:GridView ID="grd" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False"
                                                                            DataKeyNames="item_code" Width="500px" CellPadding="4" Font-Size="X-Small" CssClass="gridviewstyle"
                                                                            BorderColor="Black" HeaderStyle-CssClass="GridViewHeaderStyle" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                            RowStyle-CssClass="grid-row char grid-row-odd" PagerStyle-CssClass="GridViewPagerStyle"
                                                                            FooterStyle-BackColor="White" OnRowDataBound="grd_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="item_code" HeaderText="Item Code" />
                                                                                <asp:BoundField DataField="item_name" HeaderText="Item Name" />
                                                                                <asp:BoundField DataField="specification" HeaderText="Specification" />
                                                                                <asp:BoundField DataField="units" HeaderText="Unit" />
                                                                                <asp:BoundField HeaderText="Opening Stock" />
                                                                                <asp:BoundField HeaderText="Closing Stock" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr2" runat="server" valign="middle">
                                                                    <td align="center" colspan="5">
                                                                        <asp:GridView ID="grddetail" runat="server" AllowPaging="false" AllowSorting="True"
                                                                            AutoGenerateColumns="False" DataKeyNames="item_code" Width="800px" CellPadding="4"
                                                                            Font-Size="X-Small" CssClass="gridviewstyle" BorderColor="Black" HeaderStyle-CssClass="GridViewHeaderStyle"
                                                                            AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass="grid-row char grid-row-odd"
                                                                            PagerStyle-CssClass="GridViewPagerStyle" FooterStyle-BackColor="White" OnRowDataBound="grddetail_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="recieved_date" HeaderText="Date" HeaderStyle-Width="150px" />
                                                                                <asp:BoundField DataField="CC_code" HeaderText="CC Code" HeaderStyle-Width="100px" />
                                                                                <asp:BoundField DataField="remarks" HeaderText="Particulars" ItemStyle-Width="250px" />
                                                                                <asp:BoundField DataField="No" HeaderText="MRR/Ref No" HeaderStyle-Width="100px" />
                                                                                <asp:BoundField DataField="basic_price" HeaderText="Rate" HeaderStyle-Width="100px" />
                                                                                <asp:BoundField DataField="Recievedqty" HeaderText="Recived Qty" />
                                                                                <asp:BoundField DataField="Issuedqty" HeaderText="Issued Qty" />
                                                                                <asp:BoundField HeaderText="Balance Qty" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="5">
                            <asp:Label ID="Label16" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                OnClick="btnExcel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
