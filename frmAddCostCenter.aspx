<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" CodeFile="frmAddCostCenter.aspx.cs"
    Inherits="Admin_frmAddCostCenter" EnableEventValidation="false" Title="Add Cost Center - Essel Projects Pvt. Ltd." %>

<%@ Register Src="~/AccountVerticalMenu.ascx" TagName="Menu" TagPrefix="AccountMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validate() {
            var cctype = document.getElementById("<%=ddlcctype.ClientID%>").value;
            var subtype = document.getElementById("<%=ddltype.ClientID%>").value;
            var oftrno = document.getElementById("<%=Txtfinalofrno.ClientID%>").value;
            var oftrctrl = document.getElementById("<%=Txtfinalofrno.ClientID%>");
            var date = document.getElementById("<%=Txtfinalofrdate.ClientID%>").value;
            var datectrl = document.getElementById("<%=Txtfinalofrdate.ClientID%>");
            var refno = document.getElementById("<%=txtrefno.ClientID%>").value;
            var refnoctrl = document.getElementById("<%=txtrefno.ClientID%>");
            var refdate = document.getElementById("<%=txtrefdate.ClientID%>").value;
            var refdatectrl = document.getElementById("<%=txtrefdate.ClientID%>");
            var objs = new Array("<%=ddlcctype.ClientID %>", "<%=txtccCode.ClientID %>", "<%=txtccName.ClientID %>", "<%=txtinname.ClientID %>", "<%=incphone.ClientID %>", "<%=address.ClientID %>", "<%=phoneno.ClientID %>", "<%=txtvoucher.ClientID %>", "<%=txtday.ClientID %>", "<%=ddlstate.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (cctype == "Performing") {

                if (subtype == "Select") {
                    window.alert("Select Sub Type");
                    return false;

                }
                else if (subtype == "Service") {
                    var rbl = document.getElementById("<%=ddlservicetype.ClientID %>");
                    var models = rbl.getElementsByTagName("input");
                    var checkBoxCount = 0;
                    var i;
                    var j = 0;
                    for (var i = 0; i < models.length; i++) {
                        if (models[i].checked == true) {
                            j++;
                        }
                    }
                    if (j == 0) {
                        window.alert("Please select at least one Service Type.");
                        return false;
                    }

                    else if (oftrno == "") {
                        window.alert("Enter offerno");
                        oftrctrl.focus();
                        return false;
                    }
                    else if (date == "") {
                        window.alert("Enter date");
                        datectrl.focus();
                        return false;


                    }
                    else if (refno == "") {
                        window.alert("Enter refno");
                        refnoctrl.focus();
                        return false;
                    }
                    else if (refdate == "") {
                        window.alert("Enter date");
                        refdatectrl.focus();
                        return false;


                    }
                }
                else if (subtype == "Trading" || subtype == "Manufacturing") {

                    if (oftrno == "") {
                        window.alert("Enter offerno");
                        oftrctrl.focus();
                        return false;
                    }
                    else if (date == "") {
                        window.alert("Enter date");
                        datectrl.focus();
                        return false;


                    }
                    else if (refno == "") {
                        window.alert("Enter refno");
                        refnoctrl.focus();
                        return false;
                    }
                    else if (refdate == "") {
                        window.alert("Enter date");
                        refdatectrl.focus();
                        return false;


                    }
                }
            }
            document.getElementById("<%=btnAddCC.ClientID %>").style.display = 'none';
            return true;

        }
        
    </script>
    <script language="javascript" type="text/javascript">
        function check() {
            var cctype = document.getElementById("<%=Label13.ClientID%>").innerHTML;
            var objs = new Array("<%=ddltype.ClientID %>", "<%=txtccCode.ClientID %>", "<%=txtccName.ClientID %>", "<%=txtinname.ClientID %>", "<%=incphone.ClientID %>", "<%=address.ClientID %>", "<%=phoneno.ClientID %>", "<%=txtvoucher.ClientID %>", "<%=txtday.ClientID %>", "<%=ddlstate.ClientID %>");
            if (!CheckInputs(objs)) {
                return false;
            }
            if (cctype == "Performing") {
                var epploftrno = document.getElementById("<%=Txtfinalofrno.ClientID%>").value;
                var epploftrctrl = document.getElementById("<%=Txtfinalofrno.ClientID%>");
                var date = document.getElementById("<%=Txtfinalofrdate.ClientID%>").value;
                var datectrl = document.getElementById("<%=Txtfinalofrdate.ClientID%>");
                var refno = document.getElementById("<%=txtrefno.ClientID%>").value;
                var refnoctrl = document.getElementById("<%=txtrefno.ClientID%>");
                var refdate = document.getElementById("<%=txtrefdate.ClientID%>").value;
                var refdatectrl = document.getElementById("<%=txtrefdate.ClientID%>");
                if (epploftrno == "") {
                    window.alert("Enter EPPLfinalofferno");
                    epploftrctrl.focus();
                    return false;
                }
                else if (date == "") {
                    window.alert("Enter finalofferdate");
                    datectrl.focus();
                    return false;


                }
                else if (refno == "") {
                    window.alert("Enter refno");
                    refnoctrl.focus();
                    return false;
                }
                else if (refdate == "") {
                    window.alert("Enter date");
                    refdatectrl.focus();
                    return false;


                }


            }

        }
        function Change(Invoicetype) {
            if (Invoicetype == "Service") {
                document.getElementById("<%=tdservicetype.ClientID%>").style.display = 'block';
                document.getElementById("<%=tdddlservicetype.ClientID%>").style.display = 'block';
            }
            else {
                // document.getElementById("<%=ddlservicetype.ClientID%>").selectedIndex = 0;
                document.getElementById("<%=tdservicetype.ClientID%>").style.display = 'none';
                document.getElementById("<%=tdddlservicetype.ClientID%>").style.display = 'none';
            }
        }
    </script>
    <script type="text/javascript">
        function checkDate(sender, args) {
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
            var str1 = document.getElementById("<%=Txtfinalofrdate.ClientID %>").value;
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
                document.getElementById("<%=Txtfinalofrdate.ClientID %>").value = "";
                return false;
            }
        }
        function checkDateref(sender, args) {
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
            var str1 = document.getElementById("<%=txtrefdate.ClientID %>").value;
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
                document.getElementById("<%=txtrefdate.ClientID %>").value = "";
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
                        <table style="width: 750px">
                            <tr valign="top">
                                <td align="center">
                                    <table id="list_grid" class="gridview" width="75%" align="center" cellspacing="0"
                                        cellpadding="0">
                                        <tr>
                                            <td class="grid-content" align="center">
                                                <asp:GridView ID="GridView1" AutoGenerateColumns="false" CssClass="grid-content"
                                                    DataKeyNames="cc_code" AllowPaging="false" HeaderStyle-CssClass="grid-header"
                                                    AlternatingRowStyle-CssClass="grid-row grid-row-even" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    PagerStyle-CssClass="grid pagerbar" EmptyDataText="There is no Records" runat="server"
                                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting"
                                                    Width="700px">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" HeaderText="View" ShowSelectButton="true" SelectImageUrl="~/images/iconset-b-edit.gif" />
                                                        <asp:BoundField HeaderText="CC Code" ItemStyle-Width="150px" DataField="cc_code"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="CC Name" ItemStyle-Width="180px" DataField="cc_name"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="InchargeName" ItemStyle-Width="180px" DataField="cc_inchargename"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Address" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"
                                                            DataField="address" ItemStyle-Width="400px">
                                                            <ItemStyle HorizontalAlign="Center" Width="400px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ButtonType="Image" HeaderText="Delete" ShowDeleteButton="true"
                                                            DeleteImageUrl="~/images/Delete.jpg" />
                                                    </Columns>
                                                    <RowStyle CssClass=" grid-row char grid-row-odd"></RowStyle>
                                                    <PagerStyle CssClass="grid pagerbar"></PagerStyle>
                                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle CssClass="grid-header"></HeaderStyle>
                                                    <AlternatingRowStyle CssClass="grid-row grid-row-even"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="estbl" width="700px" id="tbl" runat="server">
                                        <tr style="border: 1px solid #000">
                                            <th valign="top" colspan="4" align="center">
                                                <asp:Label ID="Label1" runat="server" Width="200px" Font-Bold="true" Text="Cost Center Form"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" CssClass="eslbl" runat="server" Text="CC Type"></asp:Label>
                                            </td>
                                            <td valign="top" align="left" colspan="3">
                                                <asp:DropDownList ID="ddlcctype" runat="server" CssClass="esddown" ToolTip="CC Type"
                                                    Style="margin-left: 0px" Width="180px" onchange="javascript:SetContextKey('dlv',this.value);fncheckvalidation();">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Non-Performing</asp:ListItem>
                                                    <asp:ListItem>Performing</asp:ListItem>
                                                    <asp:ListItem>Capital</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="Label13" runat="server" CssClass="eslbl" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trtype" runat="server">
                                            <td>
                                                <asp:Label ID="Label16" CssClass="eslbl" runat="server" Text="Sub Type"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddltype" runat="server" ToolTip="Sub Type" Width="180px" CssClass="esddown"
                                                    onchange="Change(this.value);">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Service</asp:ListItem>
                                                    <asp:ListItem>Trading</asp:ListItem>
                                                    <asp:ListItem>Manufacturing</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td id="tdservicetype" runat="server" style="display: none">
                                                <asp:Label ID="Label17" CssClass="eslbl" runat="server" Text="Service Type"></asp:Label>
                                            </td>
                                            <td align="left" id="tdddlservicetype" runat="server" style="display: none">
                                                <asp:CheckBoxList ID="ddlservicetype" CssClass="esrbtn" Style="font-size: small"
                                                    ToolTip="Service Type" RepeatDirection="Horizontal" RepeatColumns="3" runat="server"
                                                    CellPadding="0" CellSpacing="0">
                                                    <asp:ListItem>Service Tax Invoice</asp:ListItem>
                                                    <asp:ListItem>SEZ/Service Tax exumpted Invoice</asp:ListItem>
                                                    <asp:ListItem>VAT/Material Supply</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr id="trcc" runat="server">
                                            <td>
                                                <asp:Label ID="Label11" runat="server" CssClass="eslbl" Text="EPPL Final OfferNo"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txtfinalofrno" Width="200px" ToolTip="Offer No" CssClass="estbox"
                                                    MaxLength="50" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" CssClass="eslbl" runat="server" Text="Final OfferDate"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txtfinalofrdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" CssClass="estbox" ToolTip="Final Offer Date"
                                                    Width="200px" MaxLength="50"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Txtfinalofrdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    OnClientDateSelectionChanged="checkDate" PopupButtonID="Txtfinalofrdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr id="trrefno" runat="server">
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="eslbl" Text="Client Acceptance Reference No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtrefno" ToolTip="Reference No" Width="200px" CssClass="estbox"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label15" CssClass="eslbl" runat="server" Text="Date"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtrefdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" runat="server" CssClass="estbox" ToolTip="Date" Width="200px"
                                                    MaxLength="50"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtrefdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    OnClientDateSelectionChanged="checkDateref" PopupButtonID="txtrefdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" CssClass="eslbl" runat="server" Text="CC Code"></asp:Label>
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtccCode" runat="server" CssClass="estbox" ToolTip="CC Code" Width="200px"
                                                    onkeyup="CCcodeChecker(this.value);" MaxLength="50"></asp:TextBox><br />
                                                <span id="spanAvailability" class="esspan"></span>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" CssClass="eslbl" runat="server" Text="CC Name"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtccName" runat="server" Width="200px" ToolTip="CC Name" CssClass="estbox"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Text="CC Incharge Name"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtinname" runat="server" CssClass="estbox" ToolTip="Incharge Name"
                                                    Width="200px" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="eslbl" Text="Incharge PhNo"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="incphone" runat="server" Width="200px" CssClass="estbox" ToolTip="Incharge Phone No"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="Site Address"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="address" runat="server" CssClass="estbox" ToolTip="Address" Width="200px"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="Phone No"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="phoneno" runat="server" CssClass="estbox" Width="200px" ToolTip="Phone No"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="eslbl" Text="Voucher Limit"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtvoucher" runat="server" Width="200px" ToolTip="Voucher Limit"
                                                    CssClass="estbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" CssClass="eslbl" Text="Day Limit"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtday" runat="server" Width="200px" ToolTip="Day Limit" CssClass="estbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trstate" runat="server">
                                            <td colspan="2">
                                                <asp:Label ID="Label18" runat="server" Text="State" CssClass="eslbl"></asp:Label>
                                            </td>
                                            <td colspan="2" align="center">
                                                <asp:DropDownList ID="ddlstate" Width="200px" ToolTip="State" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAddCC" runat="server" CssClass="esbtn" Text="" OnClick="btnAddCC_Click" />
                                            </td>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="esbtn" OnClick="btnCancel_Click" />
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
    <script type="text/javascript">
        var CCcodeCheckerTimer;
        var spanAvailability = $get("spanAvailability");

        function CCcodeChecker(cccode) {
            clearTimeout(CCcodeCheckerTimer);
            if (cccode.length == 0)
                spanAvailability.innerHTML = "";
            else {
                spanAvailability.innerHTML = "<span style='color: #ccc;'>checking...</span>";
                CCcodeCheckerTimer = setTimeout("checkcc('" + cccode + "');", 750);
            }
        }

        function checkcc(cccode) {
            // initiate the ajax pagemethod call
            // upon completion, the OnSucceded callback will be executed
            PageMethods.IsCCAvailable(cccode, OnSucceeded);
        }

        // Callback function invoked on successful completion of the page method.
        function OnSucceeded(result, userContext, methodName) {
            if (methodName == "IsCCAvailable") {
                var btnAddCC = document.getElementById('ctl00_ContentPlaceHolder1_btnAddCC');


                if (result == "CC Code is already exists") {
                    spanAvailability.innerHTML = result;
                    btnAddCC.disabled = true;
                }

                else {
                    spanAvailability.innerHTML = result;
                    btnAddCC.disabled = false;
                }

            }
        }


       

    </script>
    <script type="text/javascript">
        function fncheckvalidation() {
            var cctype = document.getElementById("<%=ddlcctype.ClientID%>").value;
            if (cctype == "Performing") {
                document.getElementById("<%=trcc.ClientID %>").style.display = 'block';
                document.getElementById("<%=trrefno.ClientID %>").style.display = 'block';
                document.getElementById("<%=trtype.ClientID %>").style.display = 'block';

            }
            else {
                document.getElementById("<%=trcc.ClientID %>").style.display = 'none';
                document.getElementById("<%=trrefno.ClientID %>").style.display = 'none';
                document.getElementById("<%=trtype.ClientID %>").style.display = 'none';
            }
        }
 
    </script>
</asp:Content>
