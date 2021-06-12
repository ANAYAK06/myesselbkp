<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmBankStatement.aspx.cs"
    Inherits="Admin_frmBankStatement" EnableEventValidation="false" Title="Bank Statement - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left: -10px;
            width: auto;
            height: 50px;
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
            width: 150px;
            text-align: left;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        function validate() {
            if (!ChceckRBL("<%=rbtntype.ClientID %>"))
                return false;

            if (SelectedIndex("<%=rbtntype.ClientID %>") == 1) {
                var objs = new Array("<%=ddlfrom.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>", "<%=txtSearch.ClientID %>");
            }
            else {
                var objs = new Array("<%=ddlfrom.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
            }

            if (!CheckInputs(objs)) {
                return false;
            }
            var str1 = document.getElementById("<%=txtfrom.ClientID %>").value;
            var str2 = document.getElementById("<%=txtto.ClientID %>").value;
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

            _Diff = Math.ceil((date3.getTime() - date4.getTime()) / (one_day));
            if (parseInt(_Diff) < 0) {
                alert("Invalid date");
                document.getElementById("<%=txtto.ClientID %>").focus();
                document.getElementById("<%=txtto.ClientID %>").value = "";
                return false;
            }
            return true;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <div style="width:750px; padding-top:5px;" align="center">
                    <table>
                        <tr align="center">
                            <td align="center">
                                <table width="500px" style="border: 1px solid #000" class="estbl">
                                    <tr>
                                        <th align="center" class="style9">
                                            View Bank Statement
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="innertab" align="center">
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="rbtntype" CssClass="esrbtn" Style="font-size: small" ToolTip="Transction Type"
                                                            AutoPostBack="true" RepeatDirection="Horizontal" runat="server" CellPadding="0"
                                                            CellSpacing="0" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                                            <asp:ListItem>Normal View</asp:ListItem>
                                                            <asp:ListItem>Detail View</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trdates" runat="server">
                                        <td>
                                            <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table class="innertab" visible="false">
                                                        <tr align="center">
                                                            <td>
                                                                <table id="Statement" runat="server">
                                                                    <tr id="paytype" runat="server" align="center">
                                                                        <td>
                                                                            <asp:Label ID="lblbank" runat="server" Text="Bank" CssClass="eslbl"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" Width="100px">
                                                                            </asp:DropDownList>
                                                                            <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                                                ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                                                PromptText="Select">
                                                                            </cc1:CascadingDropDown>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblmonth" CssClass="eslbl" runat="server" Text="From Date"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtfrom" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                onkeypress="return false;" runat="server" ToolTip="From Date" Width="100px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                PopupButtonID="txtfrom">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblyear" CssClass="eslbl" runat="server" Text="To Date"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtto" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                                                onkeypress="return false;" runat="server" ToolTip="To Date" Width="100px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtto"
                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                                                PopupButtonID="txtto">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Search" runat="server">
                                                                        <td align="center" colspan="6">
                                                                            <asp:Label ID="Label1" runat="server" Text="Search" CssClass="eslbl"></asp:Label>
                                                                            <asp:TextBox ID="txtSearch" ToolTip="Search" runat="server"></asp:TextBox>
                                                                            <div id="listPlacement" style="overflow-x: scroll; overflow-y: scroll; height: 315px;">
                                                                            </div>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="paymenttypesearch"
                                                                                ServicePath="cascadingDCA.asmx" TargetControlID="txtSearch" UseContextKey="True"
                                                                                CompletionInterval="1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="5"
                                                                                MinimumPrefixLength="1" CompletionListElementID="listPlacement">
                                                                            </cc1:AutoCompleteExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="rbtntype" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr id="trbtns" runat="server">
                                        <td align="center">
                                            <asp:Button ID="btnsubmit" CssClass="esbtn" runat="server" Style="font-size: small"
                                                Text="View" OnClientClick="javascript:return validate()" OnClick="btnsubmit_Click" />&nbsp
                                            <asp:Button ID="btncancel" runat="server" Text="Reset" CssClass="esbtn" Style="font-size: small"
                                                OnClick="btncancel_Click" />
                                        </td>
                                    </tr>
                                    <tr id="trlbl" runat="server">
                                        <td align="center">
                                            <asp:Label ID="lbltot" runat="server" Text="" CssClass="eslblalert"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trgrid" runat="server">
                            <td colspan="2">
                                <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true"
                                    GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging1" Font-Size="Small">
                                    <Columns>
                                        <asp:BoundField DataField="modifieddate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Paymenttype" HeaderText="Payment Type" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Modeofpay" HeaderText="Mode Of Pay" />
                                        <asp:BoundField DataField="No" HeaderText="No" />
                                        <asp:BoundField DataField="CC_Code" HeaderText="CC_Code" />
                                        <asp:BoundField DataField="DCA_Code" HeaderText="DCA_Code" />
                                        <asp:BoundField DataField="Credit" HeaderText="Credit" />
                                        <asp:BoundField DataField="Debit" HeaderText="Debit" />
                                        <asp:BoundField DataField="Balance" HeaderText="Balance" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="GridView3" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                    TabIndex="1" Width="100%" ShowFooter="False" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both">
                                    <Columns>
                                        <asp:BoundField DataField="modifieddate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Paymenttype" HeaderText="Payment Type" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Modeofpay" HeaderText="Mode Of Pay" />
                                        <asp:BoundField DataField="No" HeaderText="No" />
                                        <asp:BoundField DataField="CC_Code" HeaderText="CC_Code" />
                                        <asp:BoundField DataField="DCA_Code" HeaderText="DCA_Code" />
                                        <asp:BoundField DataField="Credit" HeaderText="Credit" />
                                        <asp:BoundField DataField="Debit" HeaderText="Debit" />
                                        <asp:BoundField DataField="Balance" HeaderText="Balance" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                    GridLines="None">
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr id="trexcel" runat="server">
            <td style="width: 150px;" valign="top">
            </td>
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To Excel:"></asp:Label>
                <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                    OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
