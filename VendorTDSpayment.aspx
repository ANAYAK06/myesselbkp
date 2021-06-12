<%@ Page Title="Vendor TDS Payment" Language="C#" MasterPageFile="~/Essel.master"
    AutoEventWireup="true" CodeFile="VendorTDSpayment.aspx.cs" Inherits="VendorTDSpayment" %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function searchvalidate() {
            //debugger;
            var objs = new Array("<%=ddlCCcode.ClientID %>", "<%=ddlcategory.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (document.getElementById("<%=ddlcategory.ClientID %>").value == "SubDca") {
                if (document.getElementById("<%=ddsdcaitcode.ClientID %>").value == "Select") {
                    alert("Please Select SubDca");
                    return false;
                }
            }
            else if (document.getElementById("<%=ddlcategory.ClientID %>").value == "ItCode") {
                if (document.getElementById("<%=ddsdcaitcode.ClientID %>").value == "Select") {
                    alert("Please Select IT Code");
                    return false;
                }
            }
            var objs = new Array("<%=txtfrom.ClientID %>", "<%=txtto.ClientID %>");
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
            var str1 = document.getElementById("<%=txtfrom.ClientID %>").value;
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
                document.getElementById("<%=txtfrom.ClientID %>").value = "";
                document.getElementById("<%=txtto.ClientID %>").value = "";
                return false;
            }
        }
        function checkDateto(sender, args) {
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
            var str1 = document.getElementById("<%=txtto.ClientID %>").value;
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
                document.getElementById("<%=txtto.ClientID %>").value = "";
                return false;
            }
        }
        function checkDatepayment(sender, args) {
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
        <tr valign="top">
            <td style="width: 150px; height: 100%;" valign="top">
                <AccountMenu:Menu ID="ww" runat="server" />
            </td>
            <td>
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
                        <table>
                            <tr>
                                <td>
                                    <table class="estbl" width="750px">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="5" align="center">
                                                <asp:Label ID="Label4" CssClass="esfmhead" runat="server" Font-Underline="true" Text="View Vendor TDS"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center" valign="middle">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Smaller" Text="Cost Center"></asp:Label>
                                                <asp:DropDownList ID="ddlCCcode" runat="server" ToolTip="Cost Center" Width="300px"
                                                    CssClass="esddown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="3" valign="middle">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="Smaller" Text="Category"></asp:Label>
                                                <asp:DropDownList ID="ddlcategory" CssClass="esddown" Width="300px" runat="server"
                                                    ToolTip="Category" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="Select">Select Category</asp:ListItem>
                                                    <asp:ListItem Value="SelectAll">Select All</asp:ListItem>
                                                    <asp:ListItem Value="SubDca">By Sub Dca</asp:ListItem>
                                                    <asp:ListItem Value="ItCode">By It Code</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trcategory" runat="server">
                                            <td align="center" colspan="6" valign="middle">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="Smaller" Text="SDCA/IT Code"></asp:Label>
                                                <asp:DropDownList ID="ddsdcaitcode" CssClass="esddown" Width="300px" runat="server"
                                                    ToolTip="Type">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle">
                                                <asp:Label ID="lblfrombank" runat="server" Font-Bold="true" Font-Size="Smaller" Text="From Date"></asp:Label>
                                            </td>
                                            <td align="center" valign="middle">
                                                <asp:TextBox ID="txtfrom" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="From Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrom"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDate"
                                                    Animated="true" PopupButtonID="txtfrom">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="center" valign="middle">
                                                <asp:Label ID="Label17" runat="server" Font-Bold="true" Font-Size="Smaller" Text="To Date"></asp:Label>
                                            </td>
                                            <td align="center" valign="middle">
                                                <asp:TextBox ID="txtto" CssClass="estbox" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" ToolTip="To Date" Width="100px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtto"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true" OnClientDateSelectionChanged="checkDateto"
                                                    PopupButtonID="txtto">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td width="130px" align="center" valign="middle">
                                                <asp:Button ID="btnAssign" runat="server" CssClass="esbtn" Style="font-size: small"
                                                    Text="View Report" OnClick="btnAssign_Click" OnClientClick="javascript:return searchvalidate()" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trgrid" runat="server">
                                <td align="center" colspan="6">
                                    <asp:GridView ID="gvvendortds" runat="server" Width="750px" AutoGenerateColumns="False"
                                        Font-Size="X-Small" CssClass="grid-content" BorderColor="Black" HeaderStyle-CssClass="grid-header"
                                        AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                        PagerStyle-CssClass="grid pagerbar" ShowFooter="true" FooterStyle-BackColor="White"
                                        FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" OnRowDataBound="gvvendortds_RowDataBound"
                                        DataKeyNames="Id">
                                        <%-- --%>
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="PaymentDate" HeaderText="Date of Payment/Credit" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="date" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="invoiceno" HeaderText="Invoice No" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="name" HeaderText="Vendor Name" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="cc_code" HeaderText="CC Code" />
                                            <asp:BoundField DataField="it_code" HeaderText="IT Code" />
                                            <asp:BoundField DataField="Basicvalue" HeaderText="Basic Amount" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" DataFormatString="{0:0.00}" />
                                            <asp:BoundField DataField="TdsAmount" HeaderText="TDS Amount" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" DataFormatString="{0:0.00}" />
                                            <asp:TemplateField HeaderText="Deduct Amt" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TdsDedAmount" runat="server" DataFormatString="{0:0.00}" Width="55px"
                                                        onkeyup="checkamt();" onkeypress='return numericValidation(this);' Text='<%# Eval("TdsAmount") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll();" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                        onclick="javascript:SelectAllCheckboxes(this);" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr id="trpaymentdetails" runat="server">
                                <td>
                                    <table align="center" class="estbl" width="100%" runat="server" id="paymentdetails">
                                        <tr>
                                            <th align="center" colspan="4">
                                                Payment Details
                                            </th>
                                        </tr>
                                        <tr id="bank" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Bank:"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="ddlfrom" runat="server" ToolTip="Bank" CssClass="esddown" AutoPostBack="true"
                                                    Width="200px" OnSelectedIndexChanged="ddlfrom_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span class="starSpan">*</span>
                                                <cc1:CascadingDropDown ID="CascadingDropDown9" runat="server" TargetControlID="ddlfrom"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="from"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr id="ModeofPay" runat="server">
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Mode Of Pay:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpayment" runat="server" ToolTip="Mode Of Pay" CssClass="esddown"
                                                    Width="70px" OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged" AutoPostBack="true">
                                                    <%-- --%>
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlpayment"
                                                    ServicePath="cascadingDCA.asmx" Category="dd" LoadingText="Please Wait" ServiceMethod="payment"
                                                    PromptText="Select">
                                                </cc1:CascadingDropDown>
                                                <asp:Label ID="lbldate" runat="server" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtdate" CssClass="estbox" runat="server" ToolTip="Invoice Date"
                                                    Width="80px"></asp:TextBox><span class="starSpan">*</span>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" OnClientDateSelectionChanged="checkDatepayment"
                                                    Animated="true" PopupButtonID="txtdate">
                                                </cc1:CalendarExtender>
                                                <%--<img onclick="scwShow(document.getElementById('<%=txtdate.ClientID %>'),this);" alt=""
                                                    src="images/cal.gif" style="width: 15px; height: 15px;" id="Img2" />--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblmode" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="No:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcheque" CssClass="estbox" runat="server" ToolTip="No" Width="200px"></asp:TextBox><span
                                                    class="starSpan">*</span>
                                                <asp:DropDownList ID="ddlcheque" runat="server" ToolTip="Cheque No" CssClass="esddown"
                                                    Width="100">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Remarks:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdesc" CssClass="estbox" runat="server" ToolTip="Description"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox><span class="starSpan">*</span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="eslbl" Font-Size="Smaller" Text="Amount:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtamt" CssClass="estbox" runat="server" ToolTip="Amount" onKeyDown="preventBackspace();"
                                                    onpaste="return false;" onkeypress="return false;" Width="200px"></asp:TextBox>
                                                <asp:HiddenField ID="hf1" runat="server" />
                                                <%----%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="tblbtn" runat="server" rules="estbl" width="660px">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnsubmit" OnClientClick="javascript:return validate()" OnClick="btnsubmit_Click"
                                        CssClass="esbtn" runat="server" Style="font-size: small" Text="Submit" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function checkamt() {
            GridView1 = document.getElementById("<%=gvvendortds.ClientID %>");
            //debugger;
            var stotal = 0;
            for (var i = 1; i < GridView1.rows.length - 1; i++) {
                if (!isNaN(GridView1.rows(i).cells(8).children[0].value)) {
                    if (parseFloat(GridView1.rows(i).cells(7).innerHTML) < parseFloat(GridView1.rows(i).cells(8).children[0].value)) {
                        alert("TDS Amt is not more than Actuall Amt");
                        GridView1.rows[i].cells[8].children[0].value = GridView1.rows(i).cells(7).innerHTML;
                        stotal += Number(GridView1.rows(i).cells(8).children[0].value);
                    }
                    if (parseFloat(GridView1.rows(i).cells(7).innerHTML) >= parseFloat(GridView1.rows(i).cells(8).children[0].value)) {
                        stotal += Number(GridView1.rows(i).cells(8).children[0].value);
                    }

                }
                else {
                    stotal += Number(GridView1.rows(i).cells(8).children[0].value);
                }
                GridView1.rows[GridView1.rows.length - 1].cells[8].innerHTML = stotal;
            }
        }

        function validate() {
            //debugger;
            var objs = new Array("<%=ddlfrom.ClientID %>", "<%=ddlpayment.ClientID %>", "<%=txtdate.ClientID %>", "<%=txtcheque.ClientID %>", "<%=ddlcheque.ClientID %>", "<%=txtdesc.ClientID %>", "<%=txtamt.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            var netamount = document.getElementById("<%= txtamt.ClientID%>").value;
            if (netamount <= 0) {
                alert("Invalid Amount");
                return false;
            }

            var GridView = document.getElementById("<%=gvvendortds.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (GridView.rows(rowCount).cells(9).children(0).checked == true) {
                        var str1 = GridView.rows(rowCount).cells(0).innerHTML;
                        var str2 = document.getElementById("<%=txtdate.ClientID %>").value;
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
                            alert("You are not able to make payment to before invoice PaymentDate " + str1);
                            document.getElementById("<%=txtdate.ClientID %>").focus();
                            return false;
                        }
                    }
                }
            }
            var bank = document.getElementById("<%=ddlfrom.ClientID %>").value;
            var response = confirm("Do you want to Continue with the " + bank);
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            if (response) {
                return true;
            }
            else {
                document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'block';
                return false;
            }
        }
    </script>
    <%--<script type="text/javascript" language="javascript">
        function Amounvalidation(amount) {
            //debugger;
            var GridView2 = document.getElementById("<%=gvvendortds.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {

                if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                    if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                        //var value = GridView2.rows(rowCount).cells(7).innerText.replace(/,/g, "");
                        var value = GridView2.rows(rowCount).cells(8).children[0].value;
                        if (value != "") {

                            originalValue += parseFloat(value);

                        }
                    }
                }
            }
            var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
            if (parseFloat(originalValue) < parseFloat(document.getElementById('<%= txtamt.ClientID%>').value)) {
                window.alert("Invalid");
                document.getElementById('<%= txtamt.ClientID%>').value = document.getElementById('<%= hf1.ClientID%>').value;
                return false;
            }
        }
    </script>--%>
    <script type="text/javascript" language="javascript">
        function SelectAll() {
            //debugger;
            var GridView2 = document.getElementById("<%=gvvendortds.ClientID %>");
            var originalValue = 0;
            for (var rowCount = 1; rowCount < GridView2.rows.length - 1; rowCount++) {

                if (GridView2.rows(rowCount).cells(9).children(0) != null) {
                    if (GridView2.rows(rowCount).cells(9).children(0).checked == true) {
                        if ((GridView2.rows(rowCount).cells(8).children[0].value != 0) && (GridView2.rows(rowCount).cells(8).children[0].value != "")) {
                            var value = GridView2.rows(rowCount).cells(8).children[0].value;
                            if (value != "") {
                                GridView2.rows(rowCount).cells(8).children[0].disabled = true;
                                originalValue += parseFloat(value);
                            }
                        }
                        else {
                            GridView2.rows(rowCount).cells(9).children(0).checked = false
                            GridView2.rows(rowCount).cells(8).children[0].disabled = false;
                            alert("Amount should not be empty");
                        }
                    }

                    else {
                        GridView2.rows(rowCount).cells(8).children[0].disabled = false;

                    }
                }
            }
            //var roundValue = Math.round(originalValue * Math.pow(10, 2)) / Math.pow(10, 2);
            document.getElementById('<%= hf1.ClientID%>').value = originalValue;
            document.getElementById('<%= txtamt.ClientID%>').value = originalValue;

        }
        function Check() {
            //debugger;
            var Grd = document.getElementById("<%=gvvendortds.ClientID %>");
            for (var rowCount = 1; rowCount < Grd.rows.length - 1; rowCount++) {
                if (Grd.rows(rowCount).cells(9).children(0).checked == true) {
                    Grd.rows(rowCount).cells(8).children[0].disabled = true;
                }
                else {
                    Grd.rows(rowCount).cells(8).children[0].disabled = false;
                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function SelectAllCheckboxes(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
                  spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
                elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
    }
    function numericValidation(txtvalue) {

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
                if (points == 1) {
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
</asp:Content>
