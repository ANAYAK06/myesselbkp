<%@ Page Title="APPROVAL NOTE FOR SALE OF ASSET" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="AssetSale.aspx.cs" Inherits="AssetSale" %>

<%@ Register Src="~/WareHouseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            var str1 = document.getElementById("<%=txtdates.ClientID %>").value;
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
                document.getElementById("<%=txtdates.ClientID %>").value = "";
                return false;
            }
        }
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    }
                    else {
                        evt.returnValue = false;
                    }
                }
            }
        }
        function searchvalidate() {
            var Search = document.getElementById("<%=ddlasset.ClientID %>").value;
            var Searchctrl = document.getElementById("<%=ddlasset.ClientID %>");
            if (Search == "Select") {
                window.alert("Please Select Asset Item");
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
        function AssetCalc() {
            //debugger;
            var Actassetamt = document.getElementById("<%=txtassetamount.ClientID %>").value;
            var ActSellamt = document.getElementById("<%=txtasstsellingamt.ClientID %>").value;
            var Profit = document.getElementById("<%=lblprofitsaleamount.ClientID %>").value;
            var Loss = document.getElementById("<%=lbllosssaleamount.ClientID %>").value;
            var Basic = document.getElementById("<%=hfbasic.ClientID %>").value;
            if (Actassetamt == "") {
                Actassetamt = 0;
            }
            if (ActSellamt == "") {
                ActSellamt = 0;
            }
            if (parseFloat(Actassetamt) > parseFloat(Basic)) {
                alert("Invalid Actuall Asset Value");
                document.getElementById("<%=txtassetamount.ClientID %>").value = "";
                document.getElementById("<%=lbllosssaleamount.ClientID %>").value = "";
                document.getElementById("<%=lblprofitsaleamount.ClientID %>").value = "";
            }
            else {
                if (parseFloat(Actassetamt) > parseFloat(ActSellamt)) {
                    if (ActSellamt != 0) {
                        document.getElementById("<%=lbllosssaleamount.ClientID %>").value = (parseFloat(Actassetamt) - parseFloat(ActSellamt))
                        document.getElementById("<%=lblprofitsaleamount.ClientID %>").value = "";
                    }
                    else {
                        document.getElementById("<%=lblprofitsaleamount.ClientID %>").value = (parseFloat(Actassetamt) - parseFloat(ActSellamt))
                        document.getElementById("<%=lbllosssaleamount.ClientID %>").value = "";
                    }
                }
                else if (parseFloat(Actassetamt) < parseFloat(ActSellamt)) {
                    if (Actassetamt != 0) {
                        document.getElementById("<%=lblprofitsaleamount.ClientID %>").value = (parseFloat(ActSellamt) - parseFloat(Actassetamt))
                        document.getElementById("<%=lbllosssaleamount.ClientID %>").value = "";
                    }
                    else {
                        document.getElementById("<%=lbllosssaleamount.ClientID %>").value = (parseFloat(ActSellamt) - parseFloat(Actassetamt))
                        document.getElementById("<%=lblprofitsaleamount.ClientID %>").value = "";
                    }


                }
                else {
                    document.getElementById("<%=lbllosssaleamount.ClientID %>").value = "";
                    document.getElementById("<%=lblprofitsaleamount.ClientID %>").value = "";
                }
            }

        }
        function validate() {
            //debugger;
            GridView = document.getElementById("<%=GridView1.ClientID %>");
            var date = document.getElementById("<%=txtdates.ClientID %>").value;
            var datectrl = document.getElementById("<%=txtdates.ClientID %>");
            var assvaluedate = document.getElementById("<%=txtassvaluedate.ClientID %>").value;
            var assvaluedatectrl = document.getElementById("<%=txtassvaluedate.ClientID %>");
            var assetamount = document.getElementById("<%=txtassetamount.ClientID %>").value;
            var assetamountctrl = document.getElementById("<%=txtassetamount.ClientID %>");
            var asstsellingamt = document.getElementById("<%=txtasstsellingamt.ClientID %>").value;
            var asstsellingamtctrl = document.getElementById("<%=txtasstsellingamt.ClientID %>");
            var name = document.getElementById("<%=txtname.ClientID %>").value;
            var namectrl = document.getElementById("<%=txtname.ClientID %>");
            var desc = document.getElementById("<%=txtaddress.ClientID %>").value;
            var descctrl = document.getElementById("<%=txtaddress.ClientID %>");
            if (date == "") {
                window.alert("Enter Date");
                datectrl.focus();
                return false;
            }
            if (GridView == null) {
                alert("Please Add Asset Item");
                return false;
            }
            if (assvaluedate == "") {
                alert("Please Select Asset Book Value Date As on");
                assvaluedatectrl.focus();
                return false;
            }
            if (assetamount == "" || assetamount == "0") {
                alert("Add Asset As on Value");
                assetamountctrl.focus();
                return false;
            }
            if (asstsellingamt == "" || asstsellingamt == "0") {
                alert("Add Asset Selling Value");
                asstsellingamtctrl.focus();
                return false;
            }
            if (name == "") {
                window.alert("Enter Name of the Buyer");
                namectrl.focus();
                return false;
            }
            if (desc == "") {
                window.alert("Enter Address of the Buyer");
                descctrl.focus();
                return false;
            }

            document.getElementById("<%=btnSave.ClientID %>").style.display = 'none';
            return true;
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
                                                                        <h3>
                                                                            <a class="help" href="" title=""><small>Approval Note For Sale Of Asset</small></a></h3>
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
                                                                                                <tr>
                                                                                                    <td class="item item-selection" valign="middle" width="">
                                                                                                        <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle">
                                                                                                        <asp:TextBox ID="txtdates" Font-Size="Small" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                                            onkeypress="return false;" runat="server" Style="width: 130px; height: 20px;
                                                                                                            vertical-align: middle"></asp:TextBox>
                                                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdates"
                                                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                                                                            Animated="true" PopupButtonID="txtdates">
                                                                                                        </cc1:CalendarExtender>
                                                                                                    </td>
                                                                                                    <td class="item item-selection" valign="middle" width="">
                                                                                                        <asp:Label ID="Label2" runat="server" Text="Select Item"></asp:Label>
                                                                                                    </td>
                                                                                                    <td class="item item-char" valign="middle" style="width: 650px">
                                                                                                        <asp:DropDownList ID="ddlasset" CssClass="char" runat="server">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
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
                                                                                                                    Add Asset Items:-</h2>
                                                                                                            </td>
                                                                                                            <td class="loading-list" style="display: none;">
                                                                                                            </td>
                                                                                                            <td class="pager-cell">
                                                                                                                <asp:Button ID="btnadd" runat="server" OnClientClick="javascript:return searchvalidate()"
                                                                                                                    OnClick="btnadd_Click" Text="Add" Height="18px" CssClass="button" />&nbsp;&nbsp;
                                                                                                            </td>
                                                                                                            <td class="pager-cell">
                                                                                                                <asp:Button ID="btnDelete" Height="18px" runat="server" Text="Delete" CssClass="button"
                                                                                                                    OnClientClick="return Deletevalidate();" OnClick="btnDelete_Click" />
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
                                                                                                <table id="_terp_list_grid" class="grid" width="750px" align="center" style="background: none;">
                                                                                                    <asp:GridView ID="GridView1" BorderColor="White" runat="server" AutoGenerateColumns="False"
                                                                                                        CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                                        RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                                        DataKeyNames="id" ShowFooter="false">
                                                                                                        <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                                        <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                                        <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                                        <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="id" Visible="false" />
                                                                                                            <asp:BoundField DataField="Item_code" HeaderText="Item Code" ItemStyle-Width="75px" />
                                                                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-Width="150px" />
                                                                                                            <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-Width="175px" />
                                                                                                            <asp:BoundField DataField="dca_code" HeaderText="DCA Code" ItemStyle-Width="100px" />
                                                                                                            <asp:BoundField DataField="Subdca_code" HeaderText="SDCA Code" ItemStyle-Width="100px" />
                                                                                                            <asp:BoundField DataField="Basicprice" HeaderText="Basic Price" ItemStyle-Width="100px" />
                                                                                                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="50px" />
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <asp:HiddenField ID="hfbasic" runat="server" />
                                                                                        <tr id="tbldesc" style="border-collapse: separate; border-spacing: 0 15px; margin-bottom: -15px;"
                                                                                            runat="server">
                                                                                            <td>
                                                                                                <table style="border-collapse: separate; border-spacing: 0 15px; margin-top: -15px;">
                                                                                                    <tr align="left">
                                                                                                        <td class="item item-selection" valign="middle" width="190px">
                                                                                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Asset Book value as on "></asp:Label>
                                                                                                        </td>
                                                                                                        <td class="item item-selection" valign="middle">
                                                                                                            <asp:TextBox ID="txtassvaluedate" runat="server" Font-Size="Small" Style="width: 90px;
                                                                                                                height: 20px; vertical-align: middle"></asp:TextBox>
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" CssClass="cal_Theme1"
                                                                                                                FirstDayOfWeek="Monday" Format="dd-MMM-yyyy" PopupButtonID="txtassvaluedate"
                                                                                                                TargetControlID="txtassvaluedate">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        <td align="left" class="item item-selection" width="160px">
                                                                                                            <asp:Label ID="lblassetamount" runat="server" Font-Bold="true" Text="Amount"></asp:Label>
                                                                                                        </td>
                                                                                                        <td align="left" class="item item-selection" valign="middle">
                                                                                                            <asp:TextBox ID="txtassetamount" runat="server" CssClass="filter_item" MaxLength="50"
                                                                                                                ToolTip="Assest As On Value" Width="110px" onkeyup="AssetCalc();"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td align="left" class="item item-selection" width="160px">
                                                                                                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Asset Selling Amount"></asp:Label>
                                                                                                        </td>
                                                                                                        <td align="left" class="item item-selection" valign="middle">
                                                                                                            <asp:TextBox ID="txtasstsellingamt" runat="server" CssClass="filter_item" MaxLength="50"
                                                                                                                ToolTip="Assest Selling Value" Width="110px" onkeyup="AssetCalc();"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="item item-selection" valign="middle" width="180px">
                                                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Profit on Sale of Asset"></asp:Label>
                                                                                                        </td>
                                                                                                        <td class="item item-selection" valign="middle" colspan="2" width="270px">
                                                                                                            <asp:TextBox ID="lblprofitsaleamount" Width="160px" ForeColor="White" Font-Bold="true"
                                                                                                                BackColor="Green" BorderWidth="3px" BorderStyle="2" BorderColor="Black" runat="server"
                                                                                                                Text=""></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td align="left" class="item item-selection" width="150px">
                                                                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Loss on Sale of Asset "></asp:Label>
                                                                                                        </td>
                                                                                                        <td class="item item-selection" colspan="2" valign="middle" width="270px">
                                                                                                            <asp:TextBox ID="lbllosssaleamount" Width="160px" ForeColor="White" Font-Bold="true"
                                                                                                                BorderWidth="3px" BorderStyle="2" BackColor="Red" BorderColor="Black" runat="server"
                                                                                                                Text=""></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width="800px" colspan="6">
                                                                                                            <table align="center" class="search_table" width="800px">
                                                                                                                <tr>
                                                                                                                    <td align="left" class="item item-selection" colspan="2" width="300px">
                                                                                                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Name of Buyer"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td align="left" class="item item-selection" colspan="4" valign="middle">
                                                                                                                        <asp:TextBox ID="txtname" runat="server" Font-Bold="true" Font-Size="Small" CssClass="filter_item"
                                                                                                                            MaxLength="200"  ToolTip="Buyer Name" Width="500px"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" class="item item-selection" colspan="2" width="300px">
                                                                                                                        <asp:Label ID="lbldate" runat="server" Font-Bold="true" Text="Address of Buyer"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td align="left" class="item item-selection" colspan="4" valign="middle">
                                                                                                                        <asp:TextBox ID="txtaddress" runat="server" Font-Bold="true" Font-Size="Small"
                                                                                                                            CssClass="filter_item"  TextMode="MultiLine" ToolTip="Description"
                                                                                                                            Width="500px" Height="150px"></asp:TextBox>
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
                                                                                                <asp:Button ID="btnSave" Height="18px" runat="server" Text="Save" OnClick="btnSave_Click"
                                                                                                    OnClientClick="javascript:return validate()" CssClass="button" />
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
