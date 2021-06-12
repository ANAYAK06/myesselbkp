<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="VerifyclientPO.aspx.cs" Inherits="VerifyclientPO" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .style1
        {
            color: #4C4C4C;
            text-align: right;
            white-space: nowrap;
            width: 180px;
            padding: 2px 6px;
        }
        .style6
        {
            width: 129px;
        }
        .style8
        {
            width: 112px;
        }
        .style9
        {
            width: 152px;
        }
        .ajax__calendar_days table
        {
            width: 170px !important;
            height: 120px !important;
        }
        .ajax__calendar_months table
        {
            width: 170px !important;
            height: 120px !important;
        }
        .ajax__calendar_years table
        {
            width: 170px !important;
            height: 120px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function closepopup() {
            $find('mdlitems').hide();

        }
       
    </script>
    <script type="text/javascript">
        function checkNumeric(event) {
            var kCode = event.keyCode || event.charCode; // for cross browser check

            //FF and Safari use e.charCode, while IE use e.keyCode that returns the ASCII value 
            if ((kCode > 57 || kCode < 48) && (kCode != 46 && kCode != 45)) {
                //code for IE
                if (window.ActiveXObject) {
                    event.keyCode = 0
                    return false;
                }
                else {
                    event.charCode = 0
                }
            }
        }
    </script>
    <script type="text/javascript">

        function CalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        function validate() {
            var mob = document.getElementById("<%=ddlmob.ClientID %>").value;
            if (mob == "Select") {
                alert("Select Mobalisation");
                document.getElementById("<%=ddlmob.ClientID %>").focus();
                return false;
            }
            else if (mob == "Yes") {
                if (document.getElementById("<%=txtadvsett.ClientID %>").value == "") {
                    alert("Advance Settlement Required");
                document.getElementById("<%=txtadvsett.ClientID %>").focus();
                return false;
                }
            }
            var str1 = document.getElementById("<%=txtStartdate.ClientID %>").value;

            var str2 = document.getElementById("<%=txtEnddate.ClientID %>").value;
            var args = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
            var dt1 = str1.substring(0, 2);
            var dt2 = str2.substring(0, 2);
            var yr1 = str1.substring(7, 11);
            var yr2 = str2.substring(7, 11);
            for (var i = 0; i < args.length; i++) {
                var month = str2.substring(3, 6);
                var month1 = str1.substring(3, 6);
                if (args[i] == month) {
                    var month = parseFloat(i + 1);
                    var date2 = yr2 + "-" + month + "-" + dt2;

                }
                if (args[i] == month1) {
                    var month1 = parseFloat(i + 1);
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

            _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
            if (parseFloat(_Diff) < 0) {
                alert("Invalid To date");
                document.getElementById("<%=txtEnddate.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlmob.ClientID %>").value == "Yes") {
                if (document.getElementById("<%=txtadvsett.ClientID %>").value == "") {
                    alert("Advance Settlement Required");
                    document.getElementById("<%=txtadvsett.ClientID %>").focus();
                    return false;
                }
            }      
            var objs = new Array("<%=txtStartdate.ClientID %>", "<%=txtPO.ClientID %>", "<%=txtBasic.ClientID %>", "<%=txtTax.ClientID %>", "<%=txtEnddate.ClientID %>", "<%=txtRabill.ClientID %>", "<%=txtpaybills.ClientID %>",
                                "<%=txtgst.ClientID %>");
            return CheckInputs(objs);

        }
    </script>
    <script type="text/javascript">
        function validateamend() {
            var objs = new Array("<%=txtAmdDate.ClientID %>", "<%=txtamPOvalue.ClientID %>", "<%=txtAmdserv.ClientID %>", "<%=txtAmgst.ClientID %>");
            return CheckInputs(objs);
        }
    </script>
    <script language="javascript" type="text/javascript">

        function Total() {
            var originalValue = 0;

            var basic = document.getElementById("<%=txtBasic.ClientID %>").value;
            var tax = document.getElementById("<%=txtTax.ClientID %>").value;
            var sal = document.getElementById("<%=txtgst.ClientID %>").value;
            if (basic == "") {
                basic = 0;
            }
            if (tax == "") {
                tax = 0;
            }
            if (sal == "") {
                sal = 0;
            }
            originalValue = eval(parseFloat(basic) + parseFloat(tax) + parseFloat(sal));
            document.getElementById('<%= txtTotal.ClientID%>').value = originalValue;
        }
        function Totalamend() {
            var originalValue = 0;
            var originalValue1 = 0;

            var Amendbasic = document.getElementById("<%=txtamPOvalue.ClientID %>").value;
            var Amendtax = document.getElementById("<%=txtAmdserv.ClientID %>").value;
            var Amendsal = document.getElementById("<%=txtAmgst.ClientID %>").value;
            var prepovalue = document.getElementById("<%=txtprPOvalue.ClientID %>").value;
            if (Amendbasic == "") {
                Amendbasic = 0;
            }
            if (Amendtax == "") {
                Amendtax = 0;
            }
            if (Amendsal == "") {
                Amendsal = 0;
            }
            originalValue = eval(parseFloat(Amendbasic) + parseFloat(Amendtax) + parseFloat(Amendsal));
            originalValue1 = eval(parseFloat(Amendbasic) + parseFloat(Amendtax) + parseFloat(Amendsal) + parseFloat(prepovalue));

            var roundValue1 = Math.round(originalValue1 * Math.pow(10, 2)) / Math.pow(10, 2);
            var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);

            document.getElementById('<%= txtrevPOValue.ClientID%>').value = roundValue1;
            document.getElementById('<%= txttotalamend.ClientID%>').value = roundValue;
        }
        
                                              
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%" style="vertical-align: top">
                    <tr valign="top">
                        <td align="center" class="style1">
                            <table width="100%">
                                <tr>
                                    <td align="center" style="width: 100%;">
                                        <h1 style="font-size: medium;">
                                            <asp:Label ID="newpo" runat="server" Text="New Client PO" Font-Bold="True" align="left"
                                                Font-Overline="False" Font-Underline="True"></asp:Label></h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="grid-content">
                                        <table id="Table1" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                            style="background: none;" width="100%">
                                            <asp:GridView ID="gridnewpo" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="contract_id"
                                                EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd" Width="100%" OnSelectedIndexChanged="gridnewpo_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif"
                                                        ShowSelectButton="true" />
                                                    <asp:BoundField DataField="contract_id" Visible="false" />
                                                    <asp:BoundField DataField="po_no" HeaderText="PO NO" />
                                                    <asp:BoundField DataField="CC_code" HeaderText="CC CODE" />
                                                    <asp:BoundField DataField="po_totalvalue" DataFormatString="{0:0.00}" HeaderText="PO VALUE" />
                                                    <asp:BoundField DataField="po_date" HeaderText="PO DATE" ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                <PagerStyle CssClass="grid pagerbar" />
                                                <HeaderStyle CssClass="grid-header" />
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                            </asp:GridView>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="tramend" runat="server">
                                    <td align="center" style="width: 100%;">
                                        <h1 style="font-size: medium;">
                                            <asp:Label ID="updpo" runat="server" Text="Ammended Client PO" align="left" Font-Bold="True"
                                                Font-Underline="True"></asp:Label></h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="grid-content">
                                        <table id="Table2" align="center" cellpadding="0" cellspacing="0" class="grid-content"
                                            style="background: none;" width="100%">
                                            <asp:GridView ID="gridamendpo" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" DataKeyNames="contract_id"
                                                EmptyDataText="There is no records" HeaderStyle-CssClass="grid-header" PagerStyle-CssClass="grid pagerbar"
                                                PageSize="10" RowStyle-CssClass=" grid-row char grid-row-odd" Width="100%" OnSelectedIndexChanged="gridamendpo_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" HeaderText="Edit" ItemStyle-Width="15px" SelectImageUrl="~/images/iconset-b-edit.gif"
                                                        ShowSelectButton="true" />
                                                    <asp:BoundField DataField="contract_id" Visible="false" />
                                                    <asp:BoundField DataField="po_amendedno" HeaderText="AMENDED PONO" />
                                                    <asp:BoundField DataField="po_no" HeaderText="PO NO" />
                                                    <asp:BoundField DataField="po_amendedtotalvalue" HeaderText="AMENDED VALUE" DataFormatString="{0:0.00}" />
                                                    <asp:BoundField DataField="po_ammendedate" HeaderText="AMENDED DATE" ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                                <RowStyle CssClass=" grid-row char grid-row-odd" />
                                                <PagerStyle CssClass="grid pagerbar" />
                                                <HeaderStyle CssClass="grid-header" />
                                                <AlternatingRowStyle CssClass="grid-row grid-row-even" />
                                            </asp:GridView>
                                        </table>
                                    </td>
                                </tr>
                            </table>
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
                    <asp:Label ID="lbltitle" runat="server" Text="Verfiy Client PO"></asp:Label>
                </td>
                <td width="13" valign="bottom">
                    <img src="images/rightc.jpg" />
                </td>
            </tr>
            <tr id="tritemcode" runat="server">
                <td bgcolor="#FFFFFF">
                    &nbsp;
                </td>
                <td height="180" valign="top" class="popcontent">
                    <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                        height: 300px;">
                        <table style="vertical-align: middle;" align="center" width="750px">
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
                                                                        <div id="divprogress" align="center" style="padding-right: 25px; padding-left: 25px;
                                                                            left: 45%; top: 45%; padding-bottom: 15px; padding-top: 15px; position: absolute;">
                                                                            <img alt="Loading..." id="Img1" runat="server" src="~/images/scs_progress_bar.gif" />
                                                                        </div>
                                                                    </asp:Panel>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <table runat="server" id="tblnewpo" class="estbl">
                                                                <tr>
                                                                    <th valign="top" colspan="6" align="left" style="font-size: medium;">
                                                                        <asp:Label ID="Label1" runat="server" CssClass="eslbl" Text="Purchase Order Information"
                                                                            Font-Underline="True"></asp:Label>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" class="style6">
                                                                        <asp:Label ID="Label10" runat="server" CssClass="peslbl" Text="Client ID"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:DropDownList ID="ddlclientid" AutoPostBack="true" onchange="SetDynamicKey('dp2',this.value);"
                                                                            Width="100px" runat="server" OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged"
                                                                            ToolTip="ClientID" CssClass="esddown">
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                        <asp:Label ID="lblclientid" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" valign="middle" class="style8">
                                                                        <asp:Label ID="Label18" runat="server" CssClass="peslbl" Text="Subclient ID"></asp:Label>
                                                                    </td>
                                                                    <td class="style9" valign="middle">
                                                                        <asp:DropDownList ID="ddlsubclientid" AutoPostBack="true" runat="server" ToolTip="ClientID"
                                                                            Width="105px" CssClass="esddown" OnSelectedIndexChanged="ddlsubclientid_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                        <asp:Label ID="lblsubclientid" CssClass="eslbl" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label13" runat="server" CssClass="peslbl" Text="PO No"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtPO" runat="server" Width="120px" CssClass="estbox" ToolTip="PO No"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label20" runat="server" CssClass="peslbl" Text="PO Start Date"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtStartdate" runat="server" CssClass="estbox" Width="120px" ToolTip="Strat Date"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartdate"
                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                            PopupButtonID="txtdate" OnClientShown="CalendarShown">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="center" valign="middle" class="style8">
                                                                        <asp:Label ID="Label25" runat="server" CssClass="peslbl" Text="Completion Date"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" style="width: 152px">
                                                                        <asp:TextBox ID="txtEnddate" runat="server" Width="120px" CssClass="estbox" ToolTip="Compeltion Date"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEnddate"
                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                            PopupButtonID="txtdate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label4" runat="server" CssClass="peslbl" Text="PO Value"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtBasic" runat="server" CssClass="estbox" Width="120px" ToolTip="PO Value"
                                                                            onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" class="style6">
                                                                        <asp:Label ID="Lblsertax" runat="server" CssClass="peslbl" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtTax" runat="server" CssClass="estbox" Width="120px" ToolTip="Service/Excise Tax"
                                                                            onkeyup="Total();"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle" class="style8">
                                                                        <asp:Label ID="lblgst" runat="server" CssClass="peslbl" Text="GST"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" style="width: 152px">
                                                                        <asp:TextBox ID="txtgst" runat="server" Width="120px" CssClass="estbox" ToolTip="GST"
                                                                            onkeyup="Total();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label2" runat="server" Text="Total " CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtTotal" runat="server" Width="120px" onkeydown="return DateReadonly();"
                                                                            ToolTip="Total" CssClass="estbox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th valign="top" colspan="6" align="left" style="font-size: medium;">
                                                                        <asp:Label ID="Label5" runat="server" Text="Payment Term" CssClass="eslbl" Font-Underline="True"></asp:Label>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" class="style6">
                                                                        <asp:Label ID="Label28" runat="server" Text="Mobilisation Advance " CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td colspan="4" class="item item-char" valign="middle" align="left">
                                                                        <asp:DropDownList ID="ddlmob" runat="server" CssClass="esddown" ToolTip="mob. Advance"
                                                                            Font-Bold="True" Height="18px" Width="100px" OnSelectedIndexChanged="ddlmob_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" class="style6">
                                                                        <asp:Label ID="Label30" runat="server" Text="RA Bill " CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtRabill" runat="server" ToolTip="RA Bill" Width="120px" CssClass="estbox"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle" class="style8">
                                                                        <asp:Label ID="Label31" runat="server" Text="Payment due of RA Bills " CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle" style="width: 152px">
                                                                        <asp:TextBox ID="txtpaybills" runat="server" ToolTip="Payment due of RA Bills" Width="120px"
                                                                            CssClass="estbox"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label32" runat="server" Text="Advance Settlement " CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtadvsett" runat="server" ToolTip="Advance Settlement" Width="120px"
                                                                            CssClass="estbox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="3">
                                                                        <asp:Button ID="btnverifyPO" runat="server" CssClass="esbtn" Text="Verify PO" OnClientClick="return validate();"
                                                                            OnClick="btnverifyPO_Click" Font-Bold="True" />
                                                                        <asp:Label ID="Label3" Width="50px" runat="server" Text="" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                   
                                                                    <td align="left" colspan="2">
                                                                        <asp:Button ID="btnReject" runat="server" CssClass="esbtn" Text="Reject PO" Font-Bold="True"
                                                                            OnClick="btnReject_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="tblAmendpo" class="estbl">
                                                                <tr>
                                                                    <th valign="top" colspan="6" align="left" style="font-size: medium;">
                                                                        <asp:Label ID="Label34" runat="server" Text="PO Amended Information" CssClass="eslbl"></asp:Label>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label35" runat="server" Text="PO No" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtpono" runat="server" Width="120px" onkeydown="return DateReadonly();"
                                                                            CssClass="estbox" ToolTip="Amend po"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label41" runat="server" Text="Amend PO No" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="Txtamndpo" runat="server" Width="120px" onkeydown="return DateReadonly();"
                                                                            CssClass="estbox" ToolTip="Amend po"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label36" runat="server" Text=" Completion Date" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtAmdDate" runat="server" Width="110px" onkeydown="return DateReadonly();"
                                                                            CssClass="estbox" ToolTip="PO Completion date"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAmdDate"
                                                                            CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                            PopupButtonID="txtdate" OnClientShown="CalendarShown">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label37" runat="server" Text="Present PO Value" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtprPOvalue" runat="server" Width="120px" CssClass="estbox" onkeydown="return DateReadonly();"
                                                                            onkeyup="Totalamend();"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label38" runat="server" Text="Amend PO Value" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtamPOvalue" runat="server" Width="120px" CssClass="estbox" onkeyup="Totalamend();"
                                                                            ToolTip="Amend PO Value" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="lblamsertax" runat="server" Text="" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtAmdserv" runat="server" Width="120px" CssClass="estbox" onkeyup="Totalamend();"
                                                                            ToolTip="Amend tax" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label42" runat="server" Text="GST" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtAmgst" runat="server" Width="120px" ToolTip="GST" CssClass="estbox"
                                                                            onkeyup="Totalamend();" onkeypress='javascript:return checkNumeric(event);'></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label40" runat="server" Text="Total Amend Value" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txttotalamend" runat="server" onkeydown="return DateReadonly();"
                                                                            CssClass="estbox"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="Label39" runat="server" Text="Revised PO Value" CssClass="peslbl"></asp:Label>
                                                                    </td>
                                                                    <td class="item item-char" valign="middle">
                                                                        <asp:TextBox ID="txtrevPOValue" runat="server" onkeydown="return DateReadonly();"
                                                                            CssClass="estbox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="8">
                                                                        <asp:Button ID="btnverifyAmended" runat="server" Text="Verify Amend PO" CssClass="esbtn"
                                                                            OnClientClick="return validateamend();" OnClick="btnverifyAmended_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </center>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td bgcolor="#FFFFFF">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
