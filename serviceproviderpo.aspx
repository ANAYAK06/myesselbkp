<%@ Page Language="C#" MasterPageFile="~/Essel.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="serviceproviderpo.aspx.cs" Inherits="serviceproviderpo" Title="Service Provider PO" %>

<%@ Register Src="~/PurchaseVerticalMenu.ascx" TagName="Menu" TagPrefix="PurchaseMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Css/esselCssStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function chksplcharacters() {
            //debugger;
            var Grid = document.getElementById("<%=gvDetails.ClientID %>");
            if (Grid != null) {
                for (var rowCount = 1; rowCount < Grid.rows.length - 1; rowCount++) {
                    if (Grid.rows(rowCount).cells(2).children[0].value != "") {
                        if (Grid.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
                            Grid.rows(rowCount).cells(2).children[0].value = "";
                            window.alert("Comma (,) are not allowed in Item Description");
                            return false;
                        }
                    }
                    if (Grid.rows(rowCount).cells(3).children[0].value != "") {
                        if (Grid.rows(rowCount).cells(3).children[0].value.indexOf(',') > -1) {
                            Grid.rows(rowCount).cells(3).children[0].value = "";
                            window.alert("Comma (,) are not allowed in Item Units");
                            return false;
                        }
                    }             
                }
            }
        }
        function chksplcharactersdesc() {
            //debugger;
            var gvdterms = document.getElementById("<%=grdterms.ClientID %>");
            if (gvdterms != null) {
                for (var rowCount = 1; rowCount < gvdterms.rows.length - 1; rowCount++) {
                    if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
                        if (gvdterms.rows(rowCount).cells(2).children[0].value != "") {
                            if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                                gvdterms.rows(rowCount).cells(2).children[0].value = "";
                                window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                                return false;
                            }
                            if (gvdterms.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                                gvdterms.rows(rowCount).cells(2).children[0].value = "";
                                window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                                return false;
                            }
                        }
                    }                    
                }
            }
        }
        function validate() {
            //debugger;
            var objs = new Array("<%=ddlVType.ClientID %>", "<%=ddlcccode.ClientID %>", "<%=ddldca.ClientID %>");

            if (!CheckInputs(objs)) {
                return false;
            }
            var sdca = document.getElementById("<%=ddlsubdca.ClientID %>").value;
            var subdca = document.getElementById("<%=ddlsubdca.ClientID %>");

            if (sdca == "" && subdca.disabled == false) {
                window.alert("Select Sub DCA");
                return false;
            }
            var objs = new Array("<%=txtpodate.ClientID %>", "<%=txtcompdate.ClientID %>"); 

            if (!CheckInputs(objs)) {
                return false;
            }
            //debugger;
            var GridView = document.getElementById("<%=gvDetails.ClientID %>");
            if (GridView != null) {
                for (var rowCount = 1; rowCount < GridView.rows.length - 1; rowCount++) {
                    if (rowCount == 1) {
                        if (GridView.rows(rowCount).cells(2).children[0].value == "" || GridView.rows(rowCount).cells(3).children[0].value == "" || GridView.rows(rowCount).cells(4).children[0].value == "" || GridView.rows(rowCount).cells(5).children[0].value == "") {
                            window.alert("Please Insert Atleast One Item");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
                            window.alert("Comma (,) are not allowed in Add Items");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(3).children[0].value.indexOf(',') > -1) {
                            window.alert("Comma (,) are not allowed in Add Items");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify Items");
                            return false;
                        }
                    }
                    if (rowCount > 1) {
                        if (GridView.rows(rowCount).cells(2).children[0].value == "" || GridView.rows(rowCount).cells(3).children[0].value == "" || GridView.rows(rowCount).cells(4).children[0].value == "" || GridView.rows(rowCount).cells(5).children[0].value == "") {
                            window.alert("Please Insert Item Details");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(2).children[0].value.indexOf(',') > -1) {
                            window.alert("Comma (,) are not allowed in Add Items");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(3).children[0].value.indexOf(',') > -1) {
                            window.alert("Comma (,) are not allowed in Add Items");
                            return false;
                        }
                        if (GridView.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify Items");
                            return false;
                        }
                    }
                }
            }
            var GridView1 = document.getElementById("<%=grdterms.ClientID %>");
            if (GridView1 != null) {

                for (var rowCount = 1; rowCount < GridView1.rows.length - 1; rowCount++) {
                    if (rowCount == 1) {
                        if (GridView1.rows(rowCount).cells(2).children[0].value == "") {
                            window.alert("Please Add Terms and Conditions");
                            return false;
                        }                        
                        if (GridView1.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                            window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                            window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify Terms and Conditions");
                            return false;
                        }

                    }
                    if (rowCount > 1) {
                        if (GridView1.rows(rowCount).cells(0).children(0).checked == false) {
                            window.alert("Please verify Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(2).children[0].value == "") {
                            window.alert("Please Add Terms and Conditions");
                            return false;
                        }
                        if (GridView1.rows(rowCount).cells(2).children[0].value.indexOf("'") > -1) {
                            window.alert("Apostrophe (') are not allowed in Terms and Conditions");
                            return false;
                        }                        
                        if (GridView1.rows(rowCount).cells(2).children[0].value.indexOf('$') > -1) {
                            window.alert("Dollar ($) are not allowed check in Terms and Conditions");
                            return false;
                        }
                    }
                }

            }
            document.getElementById("<%=btnsubmit.ClientID %>").style.display = 'none';
            return true;
        }
    </script>
    <script type="text/javascript">

        function checkdca() {

            var cccode = document.getElementById("<%=ddlcccode.ClientID %>").value;
            var dcacode = document.getElementById("<%=ddldca.ClientID %>").value;
            if (cccode != "CCC") {
                if (document.getElementById("<%=ddldca.ClientID %>").value == "DCA-Excise" || document.getElementById("<%=ddldca.ClientID %>").value == "DCA-SRTX") {
                    window.alert("Invalid selection");

                    document.getElementById("<%=ddldca.ClientID %>").selectedIndex = 0;
                    return false;
                }
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
            var str1 = document.getElementById("<%=txtpodate.ClientID %>").value;
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
                document.getElementById("<%=txtpodate.ClientID %>").value = "";
                return false;
            }
        }
        function checkDatecompletion(sender, args) {
            //debugger;         
            var str1 = document.getElementById("<%=txtcompdate.ClientID %>").value;
            var str2 = document.getElementById("<%=txtpodate.ClientID %>").value;
            if (str2 != "") {
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
                if (date4 < date3) {
                    alert("Invalid Completion Date Selection");
                    document.getElementById("<%=txtcompdate.ClientID %>").value = "";
                    return false;
                }
            }
            else {
                alert("Please Select PO Date first");
                document.getElementById("<%=txtcompdate.ClientID %>").value = "";
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
                <PurchaseMenu:Menu ID="ww" runat="server" />
            </td>
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
                        <table style="width: 900px">
                            <tr valign="top">
                                <td align="center">
                                    <table class="estbl" width="600px">
                                        <tr>
                                            <td colspan="6" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <th colspan="6" align="center">
                                                <asp:Label CssClass="eslbl" ID="lblvendor" runat="server" Text="Add Service Provider PO"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="Label3" runat="server" CssClass="eslbl" Text="Vendor Type"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlVType" Width="200px" runat="server" 
                                                    ToolTip="Vendor Type" CssClass="esddown">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown22" runat="server" TargetControlID="ddlVType"
                                                    ServicePath="cascadingDCA.asmx" LoadingText="Please Wait" ServiceMethod="venid"
                                                    Category="VendorType" PromptText="Select Vendor ID">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="Label2" runat="server" CssClass="eslbl" Text="CC Code"></asp:Label>
                                                <asp:DropDownList ID="ddlcccode" runat="server" ToolTip="Cost Center" Width="200px"
                                                    CssClass="esddown" onchange="SetDynamicKey('dp1',this.value);checkdca();">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="ddlcccode"
                                                    ServicePath="cascadingDCA.asmx" Category="dd1" LoadingText="Please Wait" ServiceMethod="newcostcode"
                                                    PromptText="Select Cost Center">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lbldca" runat="server" CssClass="eslbl" Text="DCA"></asp:Label>
                                                <asp:DropDownList ID="ddldca" CssClass="esddown" Width="200px" ToolTip="DCA" runat="server"
                                                    onchange="SetDynamicKey('dp3',this.value);checkdca();">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="dca" TargetControlID="ddldca"
                                                    ServiceMethod="SPDCA1" ParentControlID="ddlcccode" ServicePath="cascadingDCA.asmx"
                                                    PromptText="Select DCA">
                                                </cc1:CascadingDropDown>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblsubdca" CssClass="eslbl" runat="server" Text="Sub DCA"></asp:Label>
                                                <asp:DropDownList ID="ddlsubdca" CssClass="esddown" Width="200px" runat="server"
                                                    ToolTip="Sub DCA" onchange="SetDynamicKey('dp4',this.value);">
                                                </asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="subdca" TargetControlID="ddlsubdca"
                                                    ParentControlID="ddldca" ServiceMethod="SUBDCA" ServicePath="cascadingDCA.asmx"
                                                    PromptText="Select Sub DCA">
                                                </cc1:CascadingDropDown>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="Label5" class="ajaxspan" runat="server"></asp:Label>
                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender3" BehaviorID="dp3" runat="server"
                                                    TargetControlID="Label5" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                    ServiceMethod="GetDCAName">
                                                </cc1:DynamicPopulateExtender>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="Label1" class="ajaxspan" runat="server"></asp:Label>
                                                <cc1:DynamicPopulateExtender ID="DynamicPopulateExtender4" BehaviorID="dp4" runat="server"
                                                    TargetControlID="Label1" ClearContentsDuringUpdate="true" ServicePath="EsselServices.asmx"
                                                    ServiceMethod="GetSubDCAName">
                                                </cc1:DynamicPopulateExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="right">
                                                <asp:Label ID="Label6" runat="server" CssClass="eslbl" Text="PO Date"></asp:Label>
                                                <asp:TextBox ID="txtpodate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="PO Date" Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtpodate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    OnClientDateSelectionChanged="checkDate" PopupButtonID="txtpodate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="left" colspan="3">
                                             <asp:Label ID="Label7" runat="server" CssClass="eslbl" Text="PO Completion Date"></asp:Label>
                                                <asp:TextBox ID="txtcompdate" onKeyDown="preventBackspace();" onpaste="return false;"
                                                    onkeypress="return false;" CssClass="estbox" runat="server" ToolTip="Date" Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtcompdate"
                                                    CssClass="cal_Theme1" Format="dd-MMM-yyyy" FirstDayOfWeek="Monday" Animated="true"
                                                    OnClientDateSelectionChanged="checkDatecompletion" PopupButtonID="txtcompdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView ID="gvDetails" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="100%" GridLines="None" ShowFooter="true" OnRowDeleting="gvDetails_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="450px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtitemdesc" runat="server" onkeyup="this.value = this.value.replace(/^[,/]+$/, '')"
                                                                    Width="450px" onblur="chksplcharacters()" /><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Item Description Required"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtitemdesc"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtunit" runat="server" onkeyup="this.value = this.value.replace(/^[0-9 ,]+$/, '')"
                                                                    Width="50px" onblur="chksplcharacters()" /><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Unit Req"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtunit"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtquantity" runat="server" Width="70px" onkeypress="return numericValidationu(this);"
                                                                    onkeyup="Calculate();"  /><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Qty Req" Display="Dynamic"
                                                                    ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtquantity"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Our Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtourrate" runat="server" Width="50px" onkeypress="return numericValidationourrate(this);"
                                                                     /><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Rate Req"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtourrate"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="PRW Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtprwrate" runat="server" Width="50px" onkeypress="return numericValidationprw(this);"
                                                                     /><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="Rate Req"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtprwrate"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtrate" runat="server" Width="50px" onkeypress="return numericValidation(this);"
                                                                    onkeyup="Calculate();" /><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Rate Req"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtrate"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtamount" runat="server" Width="100px" onKeyPress="javascript: return false;"
                                                                    onKeyDown="javascript: return false;" /><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Amount Req"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup12" ControlToValidate="txtamount"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAdd" runat="server" ValidationGroup="valGroup12" ImageUrl="~/images/imgadd1.gif"
                                                                    OnClick="btnAdd_Click"  />                                             
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="100px" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="right">
                                            <td colspan="6" style="font-weight: normal">
                                                <asp:HiddenField ID="hftotalpo" runat="server" />
                                                <asp:Label ID="Label4" runat="server" CssClass="eslbl" Font-Size="10px" Text="Total Amount:"></asp:Label>
                                                <asp:TextBox ID="txttotalamt" CssClass="estbox" runat="server" onKeyPress="javascript: return false;"
                                                    onKeyDown="javascript: return false;" Text="0" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%--   <tr>
                                            <td colspan="6" align="center">
                                                <asp:Label ID="lblremarks" runat="server" CssClass="eslbl" Text="Terms & Conditions"></asp:Label>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="6">
                                                <%--   <asp:TextBox ID="txtremarks" Width="1000px" Height="280px" runat="server" CssClass="estbox"
                                                    ToolTip="Remarks" TextMode="MultiLine"></asp:TextBox>--%>
                                                <asp:GridView ID="grdterms" runat="server" AlternatingRowStyle-CssClass="grid-row grid-row-even"
                                                    AutoGenerateColumns="False" BorderColor="White" CssClass="grid-content" HeaderStyle-CssClass="grid-header"
                                                    PagerStyle-CssClass="grid pagerbar" RowStyle-CssClass=" grid-row char grid-row-odd"
                                                    Width="100%" GridLines="None" ShowFooter="true" OnRowDeleting="grdterms_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectterms" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Font-Bold="true" HeaderText="S.No" ItemStyle-Width="50px"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Terms & Conditions" ItemStyle-Width="850px" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtterms" runat="server" onkeypress="return isNumberKey(event)"
                                                                    Width="850px" onblur="chksplcharactersdesc()" /><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Terms and Conditions Required"
                                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="valGroup11" ControlToValidate="txtterms"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnAddterm" runat="server" ValidationGroup="valGroup11" ImageUrl="~/images/imgadd1.gif"
                                                                    OnClick="btnAddterm_Click" />
                                                                <%----%>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="100px" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="6">
                                                <asp:Button ID="btnsubmit" OnClientClick="javascript:return validate()" runat="server"
                                                    CssClass="esbtn" Text="Submit" OnClick="btnsubmit_Click" />
                                                <asp:Button ID="btnreset" CssClass="esbtn" runat="server" Text="Reset" OnClick="btnreset_Click" />
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
        function numericValidation(txtvalue) {
            //debugger;
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
                    if (points >= 1) {
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
        function numericValidationourrate(txtvalue) {
            //debugger;
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
                    if (points >= 1) {
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
        function numericValidationprw(txtvalue) {
            //debugger;
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
                    if (points >= 1) {
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
        function numericValidationu(txtvalue) {
            //debugger;
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
                    if (points >= 1) {
                        var lastdigits = val.substring(val.indexOf(".") + 1, val.length);
                        if (lastdigits.length >= 6) {
                            alert("Six decimal places only allowed");
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
        
        function Calculate() {
            //debugger;
            grd = document.getElementById("<%=gvDetails.ClientID %>");
            var hfnetamt = document.getElementById("<%=hftotalpo.ClientID %>").value.replace(/,/g, "");
            var total = 0;
            var amt = 0;
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    if (Number(grd.rows(rowCount).cells(7).children[0].value) != 0.00) {
                        if (!isNaN(grd.rows(rowCount).cells(4).children[0].value)) {
                            amt = (Number(grd.rows(rowCount).cells(4).children[0].value)) * (Number(grd.rows(rowCount).cells(7).children[0].value));
                            grd.rows(rowCount).cells(8).children[0].value = Math.round(amt * Math.pow(10, 2)) / Math.pow(10, 2);
                            total += Number(grd.rows(rowCount).cells(8).children[0].value);
                        }
                        if (!isNaN(grd.rows(rowCount).cells(6).children[0].value)) {
                            if((Number(grd.rows(rowCount).cells(7).children[0].value)) > (Number(grd.rows(rowCount).cells(6).children[0].value)))
                                grd.rows(rowCount).cells(7).children[0].style.color = 'red';
                            else
                                grd.rows(rowCount).cells(7).children[0].style.color = 'black';
                        }
                    }
                    else {
                        total += Number(grd.rows(rowCount).cells(8).children[0].value);
                    }

                }
                document.getElementById("<%=hftotalpo.ClientID %>").value = total;
                document.getElementById("<%=txttotalamt.ClientID %>").value = total;
            }
        }
        function isNumberKey(evt) {
            //debugger;
            grd = document.getElementById("<%=grdterms.ClientID %>");
            if (grd != null) {
                for (var rowCount = 1; rowCount < grd.rows.length - 1; rowCount++) {
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode == 36) {                        
                        grd.rows(rowCount).cells(2).children[0].value.replace('$', '');
                        alert('Dollar($) not allowed');
                        return false;
                    }
                    if (event.keyCode == 39) {
                        grd.rows(rowCount).cells(2).children[0].value.replace('$', '');
                        event.keyCode = 0;
                    }
                    else {
                        return true;
                    }
                }
            }
        }
    </script>
</asp:Content>
<%--, "<%=txtpovalue.ClientID %>"--%>