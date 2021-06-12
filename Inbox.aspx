<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="Inbox.aspx.cs"
    Inherits="Inbox" Title="Inbox" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function check() {
            var objs = new Array("<%=txtpaymentdate.ClientID %>", "<%=txtdebitamount.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }


            var date = document.getElementById("<%=Chkdate.ClientID %>");
            var mode = document.getElementById("<%=chkpaymode.ClientID %>");
            var no = document.getElementById("<%=chkno.ClientID %>");
            var remarks = document.getElementById("<%=chkremarks.ClientID %>");
            var amount = document.getElementById("<%=chkdebitamount.ClientID %>");
            var paymenttype = document.getElementById("<%=txttype.ClientID %>").value;

            if (paymenttype != "Transfer") {
                var bank = document.getElementById("<%=Chkbank.ClientID %>");
                if (!(bank.checked && date.checked && mode.checked && no.checked && remarks.checked && amount.checked)) {
                    alert("You are not Verified");
                    return false;
                }
            }
            else {
                var bankfrom = document.getElementById("<%=Checkfrom.ClientID %>");
                var bankto = document.getElementById("<%=Checkto.ClientID %>");
                if (!(bankfrom.checked && date.checked && mode.checked && no.checked && remarks.checked && amount.checked && bankto.checked)) {
                    alert("You are not Verified");
                    return false;
                }
            }



            if (paymenttype != "Withdraw") {
                var str1 = document.getElementById("<%=txtVindt.ClientID %>").value;

                var str2 = document.getElementById("<%=txtpaymentdate.ClientID %>").value;
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
                    alert("You are not able to make payment to before Due date");
                    document.getElementById("<%=txtpaymentdate.ClientID %>").focus();
                    return false;
                }
            }
            document.getElementById("<%=btnbankApprove.ClientID %>").style.display = 'none';
            return true;

        }
    
    
    
    </script>
    <script language="javascript">
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.keyCode != 46) {
                        if (evt.preventDefault) {
                            evt.preventDefault();
                        } else {
                            evt.returnValue = false;
                        }
                    }
                }
            }
        }
    </script>
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
    <script language="javascript">

        function report1(addr1) {

            var transactionno = document.getElementById("<%=txttransaction.ClientID %>").value;
            window.open("ViewInvoiceDetails.aspx?transactionno=" + transactionno + "", 'Report', 'width=550,height=230,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="990px">
        <tr>
            <td valign="top" align="center">
                <div class="box-a list-a">
                    <div class="inner">
                        <table id="_terp_list" class="gridview" width="100%" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr class="pagerbar" align="left">
                                    <td class="pagerbar-cell">
                                        <table class="pager-table">
                                            <tbody>
                                                <tr align="left">
                                                    <td class="pager-cell" valign="middle" width="100px">
                                                        <div class="pager" align="right">
                                                            <asp:Label ID="lblmonth" runat="server" Text="Filter Type:"></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 2%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" align="left" valign="middle" width="12px">
                                                        <div class="pager">
                                                            <div align="left">
                                                                <asp:DropDownList ID="ddltype" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" valign="middle" width="20px">
                                                        <div class="pager">
                                                            <div align="right">
                                                                <asp:DropDownList ID="ddlcccode" runat="server">
                                                                </asp:DropDownList>
                                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="newcostcode"
                                                                    PromptText="Select Cost Center">
                                                                </cc1:CascadingDropDown>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 1%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" valign="middle" width="10px">
                                                        <div class="pager">
                                                            <div align="right">
                                                                <asp:DropDownList ID="ddlMonth" CssClass="char" runat="server">
                                                                    <asp:ListItem Value="Select Month">Select Month</asp:ListItem>
                                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 1%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="" valign="middle" width="15px">
                                                        <div class="pager">
                                                            <div align="right">
                                                                <asp:DropDownList ID="ddlyear" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 3%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 5%" valign="middle">
                                                        <div class="pager">
                                                            <div align="center">
                                                                <asp:ImageButton ID="gobtn" ImageUrl="~/images/go-button.gif" runat="server" OnClick="gobtn_Click" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="pager-cell" style="width: 12%" valign="middle">
                                                        <div class="pager">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="box-a list-a">
                                                    <div class="inner" align="center">
                                                        <table id="Table2" class="gridview" width="75%" cellspacing="0" cellpadding="0">
                                                            <tbody>
                                                                <tr class="pagerbar">
                                                                    <td class="pagerbar-cell">
                                                                        <table class="pager-table">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="Label1" CssClass="item item-char" runat="server" Text=""></asp:Label>
                                                                                    </td>
                                                                                    <td class="pager-cell" style="width: 90%" valign="middle">
                                                                                        <div class="pager">
                                                                                            <div align="right" style="padding-right: 30px">
                                                                                                <asp:Label ID="Label2" CssClass="item item-char" runat="server" Text="Change Limit:"></asp:Label>
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
                                                                    <td class="grid-content" align="center">
                                                                        <table id="_terp_list_grid" class="grid" width="100%" cellspacing="0" align="center"
                                                                            cellpadding="0" style="background: none;">
                                                                            <asp:GridView ID="GridView1" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                                                CssClass="grid-content" HeaderStyle-CssClass="grid-header" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                                                RowStyle-CssClass=" grid-row char grid-row-odd" PagerStyle-CssClass="grid pagerbar"
                                                                                AllowPaging="True" DataKeyNames="id" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                                OnDataBound="GridView1_DataBound" BorderColor="White" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                                                EmptyDataText="There is no records" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" ItemStyle-Width="15px"
                                                                                        SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                                                    <asp:BoundField DataField="id" Visible="true" ItemStyle-Width="100px" HeaderText="Transaction No" />
                                                                                    <asp:BoundField DataField="cc_code" ItemStyle-Width="50px" HeaderText="CC Code" ItemStyle-HorizontalAlign="Left" />
                                                                                    <asp:BoundField DataField="Description" ItemStyle-HorizontalAlign="Center" HeaderText="Description"
                                                                                        ItemStyle-Width="400px" />
                                                                                    <asp:BoundField DataField="Date" ItemStyle-HorizontalAlign="Right" HeaderText="Date"
                                                                                        ItemStyle-Width="75px" />
                                                                                    <asp:BoundField DataField="amount" ItemStyle-HorizontalAlign="Right" HeaderText="Amount"
                                                                                        ItemStyle-Width="75px" />
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="Reject" ShowDeleteButton="true"
                                                                                        ItemStyle-Width="15px" DeleteImageUrl="~/images/Delete.jpg" />
                                                                                    <asp:TemplateField ItemStyle-Width="1px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Bottom"
                                                                                        ShowHeader="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:HiddenField ID="hf1" runat="server" Value='<%#Bind("Type")%>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                                                <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                                                <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                                                <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                                                <PagerTemplate>
                                                                                    <asp:ImageButton ID="btnFirst" runat="server" Height="15px" ImageUrl="~/images/pager_first.png"
                                                                                        CommandArgument="First" CommandName="Page" OnCommand="btnFirst_Command" />&nbsp;
                                                                                    <asp:ImageButton ID="btnPrev" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_left.png"
                                                                                        CommandArgument="Prev" OnCommand="btnPrev_Command" />
                                                                                    <asp:Label ID="lblpages" runat="server" Text="" Height="15px" CssClass="item item-char"></asp:Label>
                                                                                    of
                                                                                    <asp:Label ID="lblCurrent" runat="server" Text="Label" Height="15px" CssClass="item item-char"></asp:Label>
                                                                                    <asp:ImageButton ID="btnNext" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_right.png"
                                                                                        CommandArgument="Next" OnCommand="btnNext_Command" />&nbsp;
                                                                                    <asp:ImageButton ID="btnLast" CommandName="Page" Height="15px" runat="server" ImageUrl="~/images/pager_last.png"
                                                                                        CommandArgument="Last" OnCommand="btnLast_Command" />
                                                                                </PagerTemplate>
                                                                            </asp:GridView>
                                                                            <cc1:ModalPopupExtender ID="mdlpopbank" BehaviorID="mdlbank" runat="server" TargetControlID="btnbank"
                                                                                PopupControlID="pnlbank" BackgroundCssClass="modalBackground1" DropShadow="false" />
                                                                            <asp:Panel ID="pnlbank" runat="server" Style="display: none;">
                                                                                <table width="800px" border="0" align="center" cellpadding="0" cellspacing="0" id="tblbank"
                                                                                    runat="server">
                                                                                    <tr>
                                                                                        <td width="13" valign="bottom">
                                                                                            <img src="images/leftc.jpg" />
                                                                                        </td>
                                                                                        <td class="pop_head" align="left" id="Td2" runat="server">
                                                                                            <div class="popclose">
                                                                                                <img width="20" height="20" border="0" onclick="closepopup();" src="images/mpcancel.png" />
                                                                                            </div>
                                                                                            Approve Bank Voucher
                                                                                        </td>
                                                                                        <td width="13" valign="bottom">
                                                                                            <img src="images/rightc.jpg">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td bgcolor="#FFFFFF">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td height="180" valign="top" class="popcontent">
                                                                                            <div style="overflow: auto; overflow-x: hidden; margin-left: 10px; margin-right: 10px;
                                                                                                height: 250px;">
                                                                                                <asp:UpdatePanel ID="upindent" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="upindent" runat="server">
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
                                                                                                        <table style="vertical-align: middle;" align="center">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                        <tbody>
                                                                                                                            <tr>
                                                                                                                                <td valign="top">
                                                                                                                                    <table border="0" class="fields" width="100%">
                                                                                                                                        <tbody>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="4" valign="top" class=" item-group" for="" width="100%">
                                                                                                                                                    <table border="0" class="fields" width="100%">
                                                                                                                                                        <tbody>
                                                                                                                                                            <tr>
                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                    <label>
                                                                                                                                                                        Transaction No
                                                                                                                                                                    </label>
                                                                                                                                                                    :
                                                                                                                                                                </td>
                                                                                                                                                                <asp:HiddenField ID="HiddenField" runat="server" />
                                                                                                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                    <asp:TextBox ID="txttransaction" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                </td>
                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                    <label>
                                                                                                                                                                        Payment Type
                                                                                                                                                                    </label>
                                                                                                                                                                    :
                                                                                                                                                                </td>
                                                                                                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                    <asp:TextBox ID="txttype" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                </td>
                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                    <label for="journal_id">
                                                                                                                                                                        Name
                                                                                                                                                                    </label>
                                                                                                                                                                    :
                                                                                                                                                                </td>
                                                                                                                                                                <td valign="middle" class="item item-char" colspan="4">
                                                                                                                                                                    <asp:TextBox ID="txtname" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td colspan="6" align="center">
                                                                                                                                                                    <%--   <asp:LinkButton ID="lnkViewreport" runat="server" OnClientClick="ViewDetails();" ForeColor="Blue"
                                                                                                                                                                        Style="text-decoration: underline;">View Report</asp:LinkButton>--%>
                                                                                                                                                                    <asp:HyperLink ID="HyperLink2" Text="View Details" Style="cursor: pointer" onclick="javascript:return report1(this.href);"
                                                                                                                                                                        runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="True">
                                                                                                                                                                     
                                                                                                                                                                    </asp:HyperLink>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr id="generalPay" runat="server">
                                                                                                                                                                <td colspan="6">
                                                                                                                                                                    <table>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label>
                                                                                                                                                                                    CC Code
                                                                                                                                                                                </label>
                                                                                                                                                                                :
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="item item-char" valign="middle">
                                                                                                                                                                                <span class="filter_item">
                                                                                                                                                                                    <asp:TextBox ID="ddlbankcc" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                </span>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label class="help">
                                                                                                                                                                                    DCA Code
                                                                                                                                                                                </label>
                                                                                                                                                                                :
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="item item-char" valign="middle">
                                                                                                                                                                                <span class="filter_item">
                                                                                                                                                                                    <asp:TextBox ID="ddlbankdca" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                </span>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label for="reference" class="help">
                                                                                                                                                                                    Sub DCA Code
                                                                                                                                                                                </label>
                                                                                                                                                                                :
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="item item-char" valign="middle">
                                                                                                                                                                                <span class="filter_item">
                                                                                                                                                                                    <asp:TextBox ID="ddlbanksub" Enabled="false" runat="server" CssClass="char"></asp:TextBox>
                                                                                                                                                                                </span>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr id="General" runat="server">
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <asp:Label ID="Label8" CssClass="help" runat="server" Text=""></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                <asp:TextBox ID="txtVPO" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <asp:Label ID="Label4" CssClass="help" runat="server" Text=""></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                <asp:TextBox ID="txtVInno" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="label" width="1%">
                                                                                                                                                                                <label class="help">
                                                                                                                                                                                    Entry Date:
                                                                                                                                                                                </label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                <asp:TextBox ID="txtVindt" runat="server" CssClass="char" Enabled="false"></asp:TextBox>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </table>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </tbody>
                                                                                                                                                    </table>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="4" valign="top" class=" item-notebook" width="100%">
                                                                                                                                                    <div id="Div2" class="notebook" style="display: block;">
                                                                                                                                                        <div class="notebook-pages">
                                                                                                                                                            <div class="notebook-page notebook-page-active">
                                                                                                                                                                <div>
                                                                                                                                                                    <span class="tab-title">Payment Details</span>
                                                                                                                                                                    <table border="0" class="fields" width="100%">
                                                                                                                                                                        <tbody>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <label class="help" id="lblbank" runat="server">
                                                                                                                                                                                        Bank
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="item item-char" width="31%"  valign="middle">
                                                                                                                                                                                    <span class="filter_item">
                                                                                                                                                                                        <asp:TextBox ID="txtcascbank" runat="server" Enabled="false" CssClass="char">
                                                                                                                                                                                        </asp:TextBox>
                                                                                                                                                                                    </span>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <asp:CheckBox ID="Chkbank" runat="server" />
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        Payment Date
                                                                                                                                                                                    </label>
                                                                                                                                                                                    :
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="21%" valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:TextBox ID="txtpaymentdate" MaxLength="11" ToolTip="Paid Date" runat="server"
                                                                                                                                                                                        CssClass="char"></asp:TextBox>
                                                                                                                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtpaymentdate"
                                                                                                                                                                                        CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtpaymentdate">
                                                                                                                                                                                    </cc1:CalendarExtender>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <asp:CheckBox ID="Chkdate" runat="server" />
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        Mode Of Pay:
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                    <span class="filter_item">
                                                                                                                                                                                        <asp:TextBox ID="ddlmodeofpay" runat="server" Enabled="false" CssClass="char"></asp:TextBox>
                                                                                                                                                                                    </span>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <asp:CheckBox ID="chkpaymode" runat="server" />
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        No
                                                                                                                                                                                    </label>
                                                                                                                                                                                    :
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:TextBox ID="txtno" runat="server" Enabled="false" CssClass="char"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <asp:CheckBox ID="chkno" runat="server" />
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        Remarks:
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:TextBox ID="txtremarks" runat="server" CssClass="char" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <asp:CheckBox ID="chkremarks" runat="server" />
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        Amount:
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="31%" valign="middle" class="item item-char">
                                                                                                                                                                                    <asp:TextBox ID="txtdebitamount" ToolTip="Amount" Enabled="false" runat="server"
                                                                                                                                                                                        CssClass="char"></asp:TextBox>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <asp:CheckBox ID="chkdebitamount" runat="server" />
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr id="trbank" runat="server">
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        Bank From
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="item item-char" width="31%" valign="middle">
                                                                                                                                                                                    <span class="filter_item">
                                                                                                                                                                                        <asp:TextBox ID="Txtfrom" runat="server" Enabled="false" CssClass="char">
                                                                                                                                                                                        </asp:TextBox>
                                                                                                                                                                                    </span>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <asp:CheckBox ID="Checkfrom" runat="server" />
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="label" width="1%">
                                                                                                                                                                                    <label class="help">
                                                                                                                                                                                        Bank To
                                                                                                                                                                                    </label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="item item-char" valign="middle">
                                                                                                                                                                                    <span class="filter_item">
                                                                                                                                                                                        <asp:TextBox ID="TxtTo" runat="server" Enabled="false" CssClass="char">
                                                                                                                                                                                        </asp:TextBox>
                                                                                                                                                                                    </span>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td width="1%">
                                                                                                                                                                                    <asp:CheckBox ID="Checkto" runat="server" />
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                            </tr>
                                                                                                                                                                        </tbody>
                                                                                                                                                                    </table>
                                                                                                                                                                </div>
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                    </div>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td align="center">
                                                                                                                                                    <asp:Label ID="Label3" runat="server" CssClass="red"></asp:Label>
                                                                                                                                                    <asp:Button ID="btnbankApprove" runat="server" Text="Approve" CssClass="button" OnClientClick="javascript:return check();"
                                                                                                                                                        OnClick="btnbankApprove_Click" />
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </tbody>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </tbody>
                                                                                                                    </table>
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
                                                                                <asp:Button runat="server" ID="btnbank" Style="display: none" />
                                                                            </asp:Panel>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr class="pagerbar">
                                                                    <td class="pagerbar-cell" align="right">
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function closepopup() {
            $find('mdlbank').hide();


        }

        function redirect() {
            window.location.href = "Raise Indent.aspx";

        }

    </script>
    <script language="javascript">
        function display() {
            document.getElementById("<%=tblbank.ClientID %>").style.display = 'none';
        }
        display();
    </script>
</asp:Content>
