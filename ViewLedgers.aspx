<%@ Page Title="" Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true"
    CodeFile="ViewLedgers.aspx.cs" Inherits="ViewLedgers" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/62gvam81_styles.css" rel="stylesheet" type="text/css" />
   <%-- <script src="Java_Script/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Java_Script/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <%--<script src="Java_Script/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>--%>
    <%--<script src="https://code.jquery.com/jquery-3.3.1.js" type="text/javascript"></script>--%>
   
    <script language="javascript">
       
        function searchvalidate() {
            var objs = new Array("<%=ddlledgers.ClientID %>", "<%=ddlyear.ClientID %>", "<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
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
        
    </script>
    <script language="javascript">
        function DateDiff(FromDate, ToDate) {
            var str1 = FromDate;
            var str2 = ToDate;
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
            if (parseInt(_Diff) <= 0) {

                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">

        ///Restricts the From Date Calendar Extender
        function ISvalidateDate(sender, args) {

            selectedDate = sender.get_selectedDate();
            var Fy1 = "01-Apr-" + document.getElementById("<%=ddlyear.ClientID %>").value;
            var Fy2 = "01-Apr-" + parseInt(parseInt(document.getElementById("<%=ddlyear.ClientID %>").value) + 1);
            var m = moment(selectedDate);
            var date = m.format('DD-MMM-YYYY');
            if (DateDiff(date, Fy1) == true && DateDiff(date, Fy2) == false) {

            }
            else {
                alert("Invalid Date Selection");
                document.getElementById("<%=txtfrom.ClientID %>").value = "";
                document.getElementById("<%=txtto.ClientID %>").value = "";
            }
        }

    

    </script>
    <style>
        /* unvisited link */
        a:link
        {
            color: blue;
        }
        
        /* visited link */
        a:visited
        {
            color: green;
        }
        
        /* mouse over link */
        a:hover
        {
            color: hotpink;
        }
        
        /* selected link */
        a:active
        {
            color: blue;
        }
     .details-control {
    background: url('..images/details_open.png') no-repeat center center;
    cursor: pointer;
}
tr.shown td.details-control {
    background: url('..images/details_close.png') no-repeat center center;
}
    </style>
    <style type="text/css">
        #tooltip
        {
            position: absolute;
            z-index: 1000;
            border: 1px solid #111;
            padding: 5px;
            opacity: 0.85;
        }
        #tooltip h3, #tooltip div
        {
            margin: 0;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table width="900px">
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <a id="shortcut_add_remove" class="shortcut-remove"></a>View Ledgers <a class="help"
                                    href="" title=""><small>Help</small> </a>
                            </h1>
                            <%--  <a href='#' class='gridViewToolTip' id="gridViewToolTipid">Sudheer</a><div id='tooltip' style='display: none;'></div>--%>
                            <table width="90%">
                                <tr>
                                    <td align="center">
                                        <div id="body_form">
                                            <div>
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="search_filter_data">
                                                                    <table border="0" class="fields" width="100%">
                                                                        <tr id="filter" runat="server">
                                                                            <td class="label search_filters search_fields" id="tdstock" runat="server">
                                                                                <table class="search_table">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle" width="" colspan="1">
                                                                                                <asp:Label ID="Label3" runat="server" Text="Financial Year"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle">
                                                                                                <span class="filter_item">
                                                                                                    <asp:DropDownList ID="ddlyear" CssClass="char" runat="server">
                                                                                                    </asp:DropDownList>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="label search_filters search_fields" id="td2" runat="server">
                                                                                <table class="search_table">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle" width="" colspan="1">
                                                                                                <asp:Label ID="Label4" runat="server" Text="From Date"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="item item-char" valign="middle">
                                                                                                <asp:TextBox ID="txtfrom" MaxLength="11" ToolTip="From Date" runat="server" CssClass="char"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="caltxtFrom" runat="server" TargetControlID="txtfrom" OnClientDateSelectionChanged="ISvalidateDate"
                                                                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtfrom">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table">
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle" width="" colspan="2">
                                                                                            <asp:Label ID="Label5" runat="server" Text="To Date"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle">
                                                                                            <asp:TextBox ID="txtto" MaxLength="11" ToolTip="To Date" runat="server" CssClass="char"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="caltxtToDate" runat="server" TargetControlID="txtto" OnClientDateSelectionChanged="ISvalidateDate"
                                                                                                CssClass="cal_Theme1" Format="dd-MMM-yyyy " Animated="true" PopupButtonID="txtto">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="search_filter_data">
                                                                    <table border="0" class="fields" width="100%">
                                                                        <tr id="Tr1" runat="server">
                                                                            <td class="label search_filters search_fields">
                                                                                <table class="search_table">
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle" width="" colspan="2">
                                                                                            <asp:Label ID="Label1" runat="server" Text="Ledgers"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="item item-selection" valign="middle">
                                                                                            <asp:DropDownList ID="ddlledgers" CssClass="filter_item" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
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
                                                                            <td align="center">
                                                                                <asp:Button ID="btnSearch" runat="server" Text="Submit" Height="22px" CssClass="button"
                                                                                    OnClick="btnSearch_Click" OnClientClick="javascript:return searchvalidate()" />&nbsp;&nbsp;
                                                                                <asp:Button ID="btnReset" runat="server" Text="Reset" Height="22px" CssClass="button" />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr id="trexcel" runat="server">
                                    <td align="left">
                                        <asp:Label ID="Label11" runat="server" CssClass="esddown" Text=" Convert To:"></asp:Label>
                                        <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/images/ExcelImage.jpg"
                                            OnClick="btnExcel_Click" />
                                        <asp:ImageButton ID="btnpdf" runat="server" ImageUrl="~/images/pdf1.png"
                                            OnClick="btnpdf_Click" />
                                            <asp:ImageButton ID="btnword" runat="server" ImageUrl="~/images/word.png"
                                            OnClick="btnword_Click" />
                                    </td>
                                </tr>
                            </table>
                            <div id="htmlledger" runat="server">
                            </div>
                             <div id="Div1" runat="server">
                            </div>
                            <asp:HiddenField ID="hf" runat="server" />
                            <asp:HiddenField ID="hfledgername" runat="server" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <div id="div" style="display: none; border-collapse: collapse">
    </div>
    <script type="text/javascript">

        $(document).ready(function () {

            var table = $('#example').DataTable({
                "lengthMenu": [[100, 200, 300, -1], [100, 200, 300, "All"]]
            });
            $('#example tbody').on('click', 'td.details-control', function (e) {
                var tr = $(this).closest('tr');
                var row = table.row(tr);
                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    $(tr.find("img")).attr('src', 'images/details_open.png');
                    tr.removeClass('shown');
                }
                else {
                    // Open this row
                    var Ledgertype = $("#ctl00_ContentPlaceHolder1_hf").val();
                    var LedgerName = $("#ctl00_ContentPlaceHolder1_hfledgername").val();
                    $.ajax({

                        type: "POST",
                        url: "ViewLedgers.aspx/GetTransactiondetails",

                        data: "{'Ledgertype':'" + Ledgertype + "','LedgerName':'" + LedgerName + "' ,'RecordedLedgerName': '" + $(tr.find(".details-control")).data('hidden') + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,

                        success: function (response) {

                            //  $("#div").append('');

                            //$("#div").html(response.d);
                            row.child(response.d).show();
                            $(tr.find("img")).attr('src', 'images/details_close.png');
                            tr.addClass('shown');

                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                   
                   
                }
            });

        });
    </script>
    
</asp:Content>
